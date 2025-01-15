using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008E0 RID: 2272
	internal static class ODataValue
	{
		// Token: 0x060040D1 RID: 16593 RVA: 0x000D898C File Offset: 0x000D6B8C
		public static Value Create(ODataEnvironment environment, TypeValue type, Uri startPageUri, bool unwrapFoldingExceptions)
		{
			if (type.TypeKind == ValueKind.Table)
			{
				if (type.AsTableType.ItemType.Fields.IsEmpty)
				{
					try
					{
						return new ODataValue.ODataListValue(environment, ListTypeValue.Record, startPageUri, unwrapFoldingExceptions, null, true).ToTableValue();
					}
					catch (FoldingFailureException ex)
					{
						throw ex.InnerException;
					}
				}
				return new CacheODataTableValue(environment, type.AsTableType, startPageUri, unwrapFoldingExceptions);
			}
			return new ODataValue.ODataListValue(environment, type, startPageUri, unwrapFoldingExceptions, null, false);
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x000D8A04 File Offset: 0x000D6C04
		public static Value Create(ODataEnvironment environment, TypeValue type, Uri startPageUri, Query originalQuery)
		{
			Value value = ODataValue.Create(environment, type, startPageUri, originalQuery == null);
			if (originalQuery != null)
			{
				value = new ODataValue.FallbackTableValue(value.AsTable, originalQuery);
			}
			return value;
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x000D8A30 File Offset: 0x000D6C30
		public static Value Create(ODataEnvironment environment, TypeValue type, Uri startPageUri, List<IValueReference> initialValues, bool unwrapFoldingExceptions)
		{
			Value value = null;
			Value value2 = null;
			if (initialValues != null)
			{
				value = ListValue.New(initialValues.ToArray());
				if (type.TypeKind == ValueKind.Table)
				{
					TableTypeValue tableTypeValue = type.AsTableType;
					if (tableTypeValue.ItemType.Fields.Keys.Length == 0 && initialValues.Count > 0)
					{
						tableTypeValue = TableTypeValue.New(initialValues[0].Value.Type.AsRecordType);
						value = ListValue.New(ODataValue.MakeConformingEnumerable(tableTypeValue.ItemType.Fields.Keys, value.AsList)).ToTable(tableTypeValue);
						type = tableTypeValue;
					}
					else if (tableTypeValue.ItemType.Fields.Keys.Length == 0)
					{
						value = value.AsList.ToTable();
					}
					else
					{
						value = value.AsList.ToTable(type.AsTableType);
					}
				}
				else if (type.TypeKind == ValueKind.List)
				{
					value = value.NewType(type);
				}
				else
				{
					value = value.NewType(ListTypeValue.New(type));
				}
			}
			if (startPageUri != null)
			{
				value2 = ODataValue.Create(environment, type, startPageUri, unwrapFoldingExceptions);
			}
			if (value != null && value2 != null)
			{
				ListValue listValue = ListValue.New(new Value[] { value, value2 });
				if (type.TypeKind == ValueKind.Table)
				{
					return TableValue.Combine(listValue, null);
				}
				return ListValue.Combine(listValue);
			}
			else
			{
				if (value != null)
				{
					return value;
				}
				return value2;
			}
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x000D8B76 File Offset: 0x000D6D76
		public static IEnumerable<IValueReference> MakeConformingEnumerable(Keys keys, ListValue list)
		{
			foreach (IValueReference valueReference in list)
			{
				yield return ODataValue.MakeConformingRecord(keys, valueReference.Value.AsRecord);
			}
			IEnumerator<IValueReference> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x000D8B90 File Offset: 0x000D6D90
		public static Value MakeConformingRecord(Keys keys, RecordValue current)
		{
			if (keys == null || keys.Length == 0 || current.Keys.Equals(keys))
			{
				return current;
			}
			return RecordValue.New(keys, delegate(int i)
			{
				Value value;
				if (current.TryGetValue(keys[i], out value))
				{
					return value;
				}
				return Value.Null;
			});
		}

		// Token: 0x020008E1 RID: 2273
		public sealed class ODataListValue : StreamedListValue
		{
			// Token: 0x060040D6 RID: 16598 RVA: 0x000D8BFC File Offset: 0x000D6DFC
			public ODataListValue(ODataEnvironment environment, TypeValue type, Uri startPageUri, bool unwrapFoldingExceptions, Func<RequestInfo, Uri> getNextPageUri = null, bool isTable = false)
			{
				this.startPageUri = startPageUri;
				this.environment = environment;
				this.isSingleton = type.TypeKind != ValueKind.List;
				this.type = (this.isSingleton ? ListTypeValue.New(type) : type.AsListType);
				this.getNextPageUri = getNextPageUri;
				this.unwrapFoldingExceptions = unwrapFoldingExceptions;
				this.isTable = isTable;
			}

			// Token: 0x170014DB RID: 5339
			// (get) Token: 0x060040D7 RID: 16599 RVA: 0x000D8C64 File Offset: 0x000D6E64
			public override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x060040D8 RID: 16600 RVA: 0x000D8C6C File Offset: 0x000D6E6C
			public TableValue ToTableValue()
			{
				if (this.isTable)
				{
					TableTypeValue tableTypeValue = TableTypeValue.New(this.InferItemType());
					return base.ToTable(tableTypeValue);
				}
				return base.ToTable();
			}

			// Token: 0x060040D9 RID: 16601 RVA: 0x000D8C9C File Offset: 0x000D6E9C
			private RecordTypeValue InferItemType()
			{
				RecordValue recordValue;
				using (IEnumerator<IValueReference> enumerator = this.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						recordValue = enumerator.Current.Value.AsRecord;
					}
					else
					{
						recordValue = RecordValue.Empty;
					}
				}
				RecordTypeValue asRecordType = recordValue.Type.AsRecordType;
				if (!asRecordType.Open)
				{
					return asRecordType;
				}
				return RecordTypeValue.New(asRecordType.Fields, false);
			}

			// Token: 0x060040DA RID: 16602 RVA: 0x000D8D10 File Offset: 0x000D6F10
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return new ODataValue.ODataListValue.ODataEnumerator(this.environment, this, this.unwrapFoldingExceptions, this.isTable);
			}

			// Token: 0x04002211 RID: 8721
			private readonly Uri startPageUri;

			// Token: 0x04002212 RID: 8722
			private readonly ODataEnvironment environment;

			// Token: 0x04002213 RID: 8723
			private readonly ListTypeValue type;

			// Token: 0x04002214 RID: 8724
			private readonly bool isSingleton;

			// Token: 0x04002215 RID: 8725
			private readonly Func<RequestInfo, Uri> getNextPageUri;

			// Token: 0x04002216 RID: 8726
			private readonly bool unwrapFoldingExceptions;

			// Token: 0x04002217 RID: 8727
			private readonly bool isTable;

			// Token: 0x020008E2 RID: 2274
			private sealed class ODataEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x060040DB RID: 16603 RVA: 0x000D8D2C File Offset: 0x000D6F2C
				public ODataEnumerator(ODataEnvironment environment, ODataValue.ODataListValue list, bool unwrapFoldingExceptions, bool isTable)
				{
					this.environment = environment;
					this.list = list;
					this.currentPageUri = list.startPageUri;
					this.nextPageUri = list.startPageUri;
					this.unwrapFoldingExceptions = unwrapFoldingExceptions;
					this.isTable = isTable;
				}

				// Token: 0x170014DC RID: 5340
				// (get) Token: 0x060040DC RID: 16604 RVA: 0x000D8D7F File Offset: 0x000D6F7F
				public IValueReference Current
				{
					get
					{
						return this.currentValue;
					}
				}

				// Token: 0x170014DD RID: 5341
				// (get) Token: 0x060040DD RID: 16605 RVA: 0x000D8D87 File Offset: 0x000D6F87
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x060040DE RID: 16606 RVA: 0x0000336E File Offset: 0x0000156E
				public void Dispose()
				{
				}

				// Token: 0x060040DF RID: 16607 RVA: 0x000D8D90 File Offset: 0x000D6F90
				public bool MoveNext()
				{
					if (this.buffer.Count == 0)
					{
						try
						{
							if (!this.ReadNextPage())
							{
								this.currentValue = Value.Null;
								return false;
							}
						}
						catch (WebException ex)
						{
							throw ODataCommonErrors.RequestFailed(this.environment.Host, ex, this.currentPageUri, this.environment.HttpResource);
						}
						catch (FoldingFailureException ex2)
						{
							if (!this.unwrapFoldingExceptions)
							{
								throw;
							}
							throw ex2.InnerException;
						}
					}
					this.currentValue = this.buffer.Dequeue();
					return true;
				}

				// Token: 0x060040E0 RID: 16608 RVA: 0x000D8E28 File Offset: 0x000D7028
				private bool ReadNextPage()
				{
					bool flag = false;
					while (this.nextPageUri != null && !flag)
					{
						List<IValueReference> pageValues = this.GetPageValues();
						if (pageValues == null)
						{
							return false;
						}
						if (this.isTable && pageValues.Count > 0 && pageValues[0].Value.Type.TypeKind == ValueKind.Record)
						{
							if (this.tableKeys == null)
							{
								this.tableKeys = pageValues[0].Value.AsRecord.Keys;
							}
							using (List<IValueReference>.Enumerator enumerator = pageValues.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									IValueReference valueReference = enumerator.Current;
									this.buffer.Enqueue(ODataValue.MakeConformingRecord(this.tableKeys, valueReference.Value.AsRecord));
								}
								goto IL_00EB;
							}
							goto IL_00B3;
						}
						goto IL_00B3;
						IL_00EB:
						flag = pageValues.Count > 0;
						continue;
						IL_00B3:
						foreach (IValueReference valueReference2 in pageValues)
						{
							this.buffer.Enqueue(valueReference2);
						}
						goto IL_00EB;
					}
					return flag;
				}

				// Token: 0x060040E1 RID: 16609 RVA: 0x000D8F5C File Offset: 0x000D715C
				private List<IValueReference> GetPageValues()
				{
					this.currentPageUri = this.nextPageUri;
					return ODataMessageReaderValueConverters.CreateSerialValues(this.list.environment, this.list.Type, this.list.isSingleton, this.nextPageUri, out this.nextPageUri, this.list.getNextPageUri);
				}

				// Token: 0x060040E2 RID: 16610 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x04002218 RID: 8728
				private readonly Queue<IValueReference> buffer = new Queue<IValueReference>();

				// Token: 0x04002219 RID: 8729
				private readonly ODataEnvironment environment;

				// Token: 0x0400221A RID: 8730
				private readonly ODataValue.ODataListValue list;

				// Token: 0x0400221B RID: 8731
				private readonly bool unwrapFoldingExceptions;

				// Token: 0x0400221C RID: 8732
				private readonly bool isTable;

				// Token: 0x0400221D RID: 8733
				private IValueReference currentValue;

				// Token: 0x0400221E RID: 8734
				private Uri nextPageUri;

				// Token: 0x0400221F RID: 8735
				private Uri currentPageUri;

				// Token: 0x04002220 RID: 8736
				private Keys tableKeys;
			}
		}

		// Token: 0x020008E3 RID: 2275
		public class OriginalQueryTableValue : TableValue
		{
			// Token: 0x060040E3 RID: 16611 RVA: 0x000D8FB2 File Offset: 0x000D71B2
			public OriginalQueryTableValue(Query query, TypeValue type)
			{
				this.query = query;
				this.type = type;
			}

			// Token: 0x170014DE RID: 5342
			// (get) Token: 0x060040E4 RID: 16612 RVA: 0x000D8FC8 File Offset: 0x000D71C8
			public override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x060040E5 RID: 16613 RVA: 0x000D8FD0 File Offset: 0x000D71D0
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return this.query.GetRows().GetEnumerator();
			}

			// Token: 0x04002221 RID: 8737
			private Query query;

			// Token: 0x04002222 RID: 8738
			private TypeValue type;
		}

		// Token: 0x020008E4 RID: 2276
		private class FallbackTableValue : TableValue
		{
			// Token: 0x060040E6 RID: 16614 RVA: 0x000D8FE2 File Offset: 0x000D71E2
			public FallbackTableValue(TableValue table, Query originalQuery)
			{
				this.table = table;
				this.originalQuery = originalQuery;
			}

			// Token: 0x170014DF RID: 5343
			// (get) Token: 0x060040E7 RID: 16615 RVA: 0x000D8FF8 File Offset: 0x000D71F8
			public override TypeValue Type
			{
				get
				{
					return this.table.Type;
				}
			}

			// Token: 0x060040E8 RID: 16616 RVA: 0x000D9005 File Offset: 0x000D7205
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return new ODataValue.FallbackTableValue.FallbackEnumerator(this.originalQuery, this.table.GetEnumerator());
			}

			// Token: 0x04002223 RID: 8739
			private TableValue table;

			// Token: 0x04002224 RID: 8740
			private Query originalQuery;

			// Token: 0x020008E5 RID: 2277
			private sealed class FallbackEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x060040E9 RID: 16617 RVA: 0x000D901D File Offset: 0x000D721D
				public FallbackEnumerator(Query query, IEnumerator<IValueReference> enumerator)
				{
					this.canRecover = true;
					this.query = query;
					this.enumerator = enumerator;
				}

				// Token: 0x170014E0 RID: 5344
				// (get) Token: 0x060040EA RID: 16618 RVA: 0x000D903A File Offset: 0x000D723A
				public IValueReference Current
				{
					get
					{
						return this.enumerator.Current;
					}
				}

				// Token: 0x060040EB RID: 16619 RVA: 0x000D9047 File Offset: 0x000D7247
				public void Dispose()
				{
					this.enumerator.Dispose();
				}

				// Token: 0x170014E1 RID: 5345
				// (get) Token: 0x060040EC RID: 16620 RVA: 0x000D903A File Offset: 0x000D723A
				object IEnumerator.Current
				{
					get
					{
						return this.enumerator.Current;
					}
				}

				// Token: 0x060040ED RID: 16621 RVA: 0x000D9054 File Offset: 0x000D7254
				public bool MoveNext()
				{
					bool flag2;
					try
					{
						bool flag = this.enumerator.MoveNext();
						this.canRecover = false;
						flag2 = flag;
					}
					catch (FoldingFailureException ex)
					{
						if (!this.canRecover)
						{
							throw ex.InnerException;
						}
						this.enumerator = this.query.GetRows().GetEnumerator();
						this.canRecover = false;
						flag2 = this.MoveNext();
					}
					return flag2;
				}

				// Token: 0x060040EE RID: 16622 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x04002225 RID: 8741
				private readonly Query query;

				// Token: 0x04002226 RID: 8742
				private bool canRecover;

				// Token: 0x04002227 RID: 8743
				private IEnumerator<IValueReference> enumerator;
			}
		}
	}
}
