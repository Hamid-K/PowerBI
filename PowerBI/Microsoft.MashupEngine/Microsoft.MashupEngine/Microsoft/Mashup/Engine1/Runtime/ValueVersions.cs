using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016C1 RID: 5825
	internal abstract class ValueVersions
	{
		// Token: 0x170026F6 RID: 9974
		// (get) Token: 0x0600943E RID: 37950
		protected abstract IEngineHost Host { get; }

		// Token: 0x0600943F RID: 37951
		protected abstract void VerifyActionPermitted();

		// Token: 0x06009440 RID: 37952
		protected abstract bool TryCreateVersion(string identity);

		// Token: 0x06009441 RID: 37953
		protected abstract IEnumerable<ValueVersions.ValueVersion> GetVersions();

		// Token: 0x06009442 RID: 37954 RVA: 0x001E974C File Offset: 0x001E794C
		public bool TryCreateTable(out TableValue result)
		{
			List<IValueReference> list = new List<IValueReference>();
			foreach (ValueVersions.ValueVersion valueVersion in this.GetVersions())
			{
				IValueReference valueReference;
				if (!valueVersion.TryCreateValue(out valueReference))
				{
					result = null;
					return false;
				}
				list.Add(valueVersion.ToTableRow(valueReference));
			}
			result = new QueryTableValue(new ValueVersions.ValueVersionsQuery(this, ListValue.New(list).ToTable(), this.Host));
			return true;
		}

		// Token: 0x020016C2 RID: 5826
		protected abstract class ValueVersion
		{
			// Token: 0x170026F7 RID: 9975
			// (get) Token: 0x06009443 RID: 37955
			public abstract string Identity { get; }

			// Token: 0x06009444 RID: 37956
			public abstract bool TryCreateValue(out IValueReference value);

			// Token: 0x06009445 RID: 37957
			public abstract bool TryCommit();

			// Token: 0x06009446 RID: 37958 RVA: 0x001E97DC File Offset: 0x001E79DC
			public virtual RecordValue ToTableRow(IValueReference versionValue)
			{
				string identity = this.Identity;
				Value value = ((identity != null) ? TextValue.New(identity) : Value.Null);
				return RecordValue.New(Library._Value.VersionsFunctionValue.ResultTableType.ItemType, new IValueReference[]
				{
					value,
					LogicalValue.New(identity == null),
					versionValue,
					Value.Null
				});
			}
		}

		// Token: 0x020016C3 RID: 5827
		private sealed class ValueVersionsQuery : FilteredTableQuery
		{
			// Token: 0x06009448 RID: 37960 RVA: 0x001E9832 File Offset: 0x001E7A32
			public ValueVersionsQuery(ValueVersions valueVersions, TableValue table, IEngineHost engineHost)
				: base(table, engineHost)
			{
				this.valueVersions = valueVersions;
			}

			// Token: 0x06009449 RID: 37961 RVA: 0x001E9844 File Offset: 0x001E7A44
			public override ActionValue InsertRows(Query rowsToInsert)
			{
				this.valueVersions.VerifyActionPermitted();
				TextValue newVersion = null;
				foreach (IValueReference valueReference in rowsToInsert.GetRows())
				{
					RecordValue asRecord = valueReference.Value.AsRecord;
					if (newVersion != null || asRecord.Count != 1)
					{
						newVersion = null;
						break;
					}
					newVersion = asRecord["Version"].AsText;
				}
				if (newVersion != null)
				{
					return ActionValue.New(delegate
					{
						TableValue tableValue;
						if (this.valueVersions.TryCreateVersion(newVersion.AsString) && this.valueVersions.TryCreateTable(out tableValue))
						{
							return tableValue.SelectRows(new ValueVersions.VersionEqualsFunctionValue(newVersion));
						}
						throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
					}).ClearCache(this.valueVersions.Host);
				}
				throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
			}

			// Token: 0x0600944A RID: 37962 RVA: 0x001E9920 File Offset: 0x001E7B20
			public override ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector)
			{
				this.valueVersions.VerifyActionPermitted();
				RecordValue recordValue = null;
				foreach (IValueReference valueReference in base.Table.SelectRows(selector))
				{
					RecordValue asRecord = valueReference.Value.AsRecord;
					if (recordValue != null)
					{
						recordValue = null;
						break;
					}
					recordValue = asRecord;
				}
				FunctionValue functionValue;
				Value value;
				if (recordValue != null && columnUpdates.Updates.Count == 1 && columnUpdates.Updates.TryGetValue(this.Columns.IndexOfKey("Published"), out functionValue) && QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(this), functionValue).TryGetConstant(out value) && value.IsLogical && value.AsLogical.AsBoolean)
				{
					TextValue targetVersionVersion = recordValue["Version"].AsText;
					IEnumerable<ValueVersions.ValueVersion> versions = this.valueVersions.GetVersions();
					Func<ValueVersions.ValueVersion, bool> <>9__0;
					Func<ValueVersions.ValueVersion, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (ValueVersions.ValueVersion v) => v.Identity == targetVersionVersion.AsString);
					}
					using (IEnumerator<ValueVersions.ValueVersion> enumerator2 = versions.Where(func).GetEnumerator())
					{
						if (enumerator2.MoveNext())
						{
							ValueVersions.ValueVersion version = enumerator2.Current;
							return ActionValue.New(delegate
							{
								TableValue tableValue;
								if (version.TryCommit() && this.valueVersions.TryCreateTable(out tableValue))
								{
									return tableValue.SelectRows(new ValueVersions.VersionEqualsFunctionValue(Value.Null));
								}
								throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
							}).ClearCache(this.valueVersions.Host);
						}
					}
				}
				throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
			}

			// Token: 0x04004EE8 RID: 20200
			private readonly ValueVersions valueVersions;
		}

		// Token: 0x020016C7 RID: 5831
		private sealed class VersionEqualsFunctionValue : NativeFunctionValue1<LogicalValue, RecordValue>
		{
			// Token: 0x06009451 RID: 37969 RVA: 0x001E9BAF File Offset: 0x001E7DAF
			public VersionEqualsFunctionValue(Value value)
				: base(TypeValue.Logical, "row", TypeValue.Record)
			{
				this.value = value;
			}

			// Token: 0x06009452 RID: 37970 RVA: 0x001E9BCD File Offset: 0x001E7DCD
			public override LogicalValue TypedInvoke(RecordValue row)
			{
				return LogicalValue.New(row["Version"].Equals(this.value));
			}

			// Token: 0x04004EF0 RID: 20208
			private readonly Value value;
		}
	}
}
