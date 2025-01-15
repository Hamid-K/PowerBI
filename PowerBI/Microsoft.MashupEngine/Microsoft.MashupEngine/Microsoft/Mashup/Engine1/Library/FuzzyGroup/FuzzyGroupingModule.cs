using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.FuzzyGroup
{
	// Token: 0x02000B62 RID: 2914
	public sealed class FuzzyGroupingModule : Module
	{
		// Token: 0x17001925 RID: 6437
		// (get) Token: 0x060050A0 RID: 20640 RVA: 0x0010DEFC File Offset: 0x0010C0FC
		public override string Name
		{
			get
			{
				return "FuzzyGrouping";
			}
		}

		// Token: 0x17001926 RID: 6438
		// (get) Token: 0x060050A1 RID: 20641 RVA: 0x0010DF03 File Offset: 0x0010C103
		public override Keys ExportKeys
		{
			get
			{
				if (FuzzyGroupingModule.exportKeys == null)
				{
					FuzzyGroupingModule.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Table.AddFuzzyClusterColumn";
						}
						if (index != 1)
						{
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
						return "Table.FuzzyGroup";
					});
				}
				return FuzzyGroupingModule.exportKeys;
			}
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x0010DF3C File Offset: 0x0010C13C
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new FuzzyGroupingModule.AddFuzzyClusterColumnFunctionValue(hostEnvironment);
				}
				if (index != 1)
				{
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
				return new FuzzyGroupingModule.FuzzyGroupFunctionValue(hostEnvironment);
			});
		}

		// Token: 0x04002B43 RID: 11075
		private static readonly OptionRecordDefinition FuzzyGroupOptionRecordDefinition = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Culture", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "FuzzyGroup"),
			new OptionItem("IgnoreCase", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, "FuzzyGroup"),
			new OptionItem("IgnoreSpace", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, "FuzzyGroup"),
			new OptionItem("SimilarityColumnName", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("Threshold", NullableTypeValue.Number, Value.Null, OptionItemOption.None, null, "FuzzyGroup"),
			new OptionItem("TransformationTable", NullableTypeValue.Table, Value.Null, OptionItemOption.None, null, "FuzzyGroup")
		});

		// Token: 0x04002B44 RID: 11076
		private static readonly RecordTypeValue fuzzyGroupOptionsRecordType = FuzzyGroupingModule.FuzzyGroupOptionRecordDefinition.CreateRecordType();

		// Token: 0x04002B45 RID: 11077
		private static Keys exportKeys;

		// Token: 0x02000B63 RID: 2915
		private enum Exports
		{
			// Token: 0x04002B47 RID: 11079
			AddFuzzyClusterColumn,
			// Token: 0x04002B48 RID: 11080
			FuzzyGroup,
			// Token: 0x04002B49 RID: 11081
			Count
		}

		// Token: 0x02000B64 RID: 2916
		public sealed class AddFuzzyClusterColumnFunctionValue : NativeFunctionValue4<TableValue, TableValue, TextValue, TextValue, Value>
		{
			// Token: 0x060050A5 RID: 20645 RVA: 0x0010E04C File Offset: 0x0010C24C
			public AddFuzzyClusterColumnFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 3, "table", TypeValue.Table, "columnName", TypeValue.Text, "newColumnName", TypeValue.Text, "options", FuzzyGroupingModule.fuzzyGroupOptionsRecordType.Nullable)
			{
				this.host = host;
			}

			// Token: 0x17001927 RID: 6439
			// (get) Token: 0x060050A6 RID: 20646 RVA: 0x0005DED2 File Offset: 0x0005C0D2
			public override IFunctionIdentity FunctionIdentity
			{
				get
				{
					return new FunctionTypeIdentity(base.GetType());
				}
			}

			// Token: 0x060050A7 RID: 20647 RVA: 0x0010E09C File Offset: 0x0010C29C
			public override TableValue TypedInvoke(TableValue table, TextValue columnName, TextValue newColumnName, Value options)
			{
				FuzzyGroupOptions fuzzyGroupOptions = FuzzyGroupOptions.ToFuzzyGroupOptions(FuzzyGroupingModule.FuzzyGroupOptionRecordDefinition.ValidateOptions(options, "Table.AddFuzzyClusterColumn", false, false), this.host);
				return new QueryTableValue(new AddFuzzyClusterColumnQuery(this.host, table.Query, columnName, newColumnName, fuzzyGroupOptions));
			}

			// Token: 0x04002B4A RID: 11082
			private readonly IEngineHost host;
		}

		// Token: 0x02000B65 RID: 2917
		public sealed class FuzzyGroupFunctionValue : NativeFunctionValue4<TableValue, TableValue, Value, ListValue, Value>
		{
			// Token: 0x060050A8 RID: 20648 RVA: 0x0010E0E4 File Offset: 0x0010C2E4
			public FuzzyGroupFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 3, "table", TypeValue.Table, "key", TypeValue.Any, "aggregatedColumns", TypeValue.List, "options", FuzzyGroupingModule.fuzzyGroupOptionsRecordType.Nullable)
			{
				this.host = host;
			}

			// Token: 0x17001928 RID: 6440
			// (get) Token: 0x060050A9 RID: 20649 RVA: 0x0005DED2 File Offset: 0x0005C0D2
			public override IFunctionIdentity FunctionIdentity
			{
				get
				{
					return new FunctionTypeIdentity(base.GetType());
				}
			}

			// Token: 0x060050AA RID: 20650 RVA: 0x0010E134 File Offset: 0x0010C334
			public override TableValue TypedInvoke(TableValue table, Value keys, ListValue aggregatedColumns, Value options)
			{
				RecordValue recordValue = FuzzyGroupingModule.FuzzyGroupOptionRecordDefinition.ValidateOptions(options, "Table.FuzzyGroup", false, false);
				int[] columns = TableValue.GetColumns(table.Columns, keys);
				ColumnConstructor[] columnConstructors = TableValue.GetColumnConstructors(aggregatedColumns);
				KeysBuilder keysBuilder = new KeysBuilder(columns.Length + columnConstructors.Length);
				for (int i = 0; i < columns.Length; i++)
				{
					keysBuilder.Add(table.Columns[columns[i]]);
				}
				Keys keys2 = keysBuilder.ToKeys();
				for (int j = 0; j < columnConstructors.Length; j++)
				{
					keysBuilder.Add(columnConstructors[j].Name);
				}
				Keys keys3 = keysBuilder.ToKeys();
				FuzzyGroupOptions fuzzyGroupOptions = FuzzyGroupOptions.ToFuzzyGroupOptions(recordValue, this.host);
				FuzzyGrouping fuzzyGrouping = new FuzzyGrouping(keys3, keys2, columns, columnConstructors, fuzzyGroupOptions);
				return new QueryTableValue(new FuzzyGroupQuery(this.host, table.Query, fuzzyGrouping));
			}

			// Token: 0x04002B4B RID: 11083
			private readonly IEngineHost host;
		}
	}
}
