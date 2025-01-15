using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B45 RID: 2885
	public sealed class FuzzyMatchingModule : Module
	{
		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x06005011 RID: 20497 RVA: 0x0010C245 File Offset: 0x0010A445
		public override string Name
		{
			get
			{
				return "FuzzyMatching";
			}
		}

		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x06005012 RID: 20498 RVA: 0x0010C24C File Offset: 0x0010A44C
		public override Keys ExportKeys
		{
			get
			{
				if (FuzzyMatchingModule.exportKeys == null)
				{
					FuzzyMatchingModule.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Table.FuzzyJoin";
						}
						if (index != 1)
						{
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
						return "Table.FuzzyNestedJoin";
					});
				}
				return FuzzyMatchingModule.exportKeys;
			}
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x0010C284 File Offset: 0x0010A484
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new FuzzyMatchingModule.Table.FuzzyJoinFunctionValue(hostEnvironment);
				}
				if (index != 1)
				{
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
				return new FuzzyMatchingModule.Table.FuzzyNestedJoinFunctionValue(hostEnvironment);
			});
		}

		// Token: 0x04002AF1 RID: 10993
		private static readonly OptionRecordDefinition FuzzyJoinOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("ConcurrentRequests", NullableTypeValue.Number, Value.Null, OptionItemOption.None, null, "FuzzyMatch"),
			new OptionItem("Culture", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "FuzzyMatch"),
			new OptionItem("IgnoreCase", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, "FuzzyMatch"),
			new OptionItem("IgnoreSpace", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, "FuzzyMatch"),
			new OptionItem("NumberOfMatches", NullableTypeValue.Number, Value.Null, OptionItemOption.None, null, "FuzzyMatch"),
			new OptionItem("SimilarityColumnName", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("Threshold", NullableTypeValue.Number, Value.Null, OptionItemOption.None, null, "FuzzyMatch"),
			new OptionItem("TransformationTable", NullableTypeValue.Table, Value.Null, OptionItemOption.None, null, "FuzzyMatch")
		});

		// Token: 0x04002AF2 RID: 10994
		private static readonly TypeValue fuzzyJoinOptionsType = FuzzyMatchingModule.FuzzyJoinOptionRecord.CreateRecordType().Nullable;

		// Token: 0x04002AF3 RID: 10995
		private static Keys exportKeys;

		// Token: 0x02000B46 RID: 2886
		private enum Exports
		{
			// Token: 0x04002AF5 RID: 10997
			FuzzyJoin,
			// Token: 0x04002AF6 RID: 10998
			FuzzyNestedJoin,
			// Token: 0x04002AF7 RID: 10999
			Count
		}

		// Token: 0x02000B47 RID: 2887
		public sealed class Table
		{
			// Token: 0x02000B48 RID: 2888
			public sealed class FuzzyJoinFunctionValue : NativeFunctionValueN<TableValue>
			{
				// Token: 0x06005017 RID: 20503 RVA: 0x0010C3D8 File Offset: 0x0010A5D8
				public FuzzyJoinFunctionValue(IEngineHost hostEnvironment)
					: base(TypeValue.Table, 4, new string[] { "table1", "key1", "table2", "key2", "joinKind", "joinOptions" }, new TypeValue[]
					{
						TypeValue.Table,
						TypeValue.Any,
						TypeValue.Table,
						TypeValue.Any,
						Library.JoinKind.Type.Nullable,
						FuzzyMatchingModule.fuzzyJoinOptionsType
					})
				{
					this.host = hostEnvironment;
				}

				// Token: 0x170018F1 RID: 6385
				// (get) Token: 0x06005018 RID: 20504 RVA: 0x0005DED2 File Offset: 0x0005C0D2
				public override IFunctionIdentity FunctionIdentity
				{
					get
					{
						return new FunctionTypeIdentity(base.GetType());
					}
				}

				// Token: 0x06005019 RID: 20505 RVA: 0x0010C46C File Offset: 0x0010A66C
				protected override TableValue TypedInvokeN(Value[] arguments)
				{
					RecordValue recordValue = FuzzyMatchingModule.FuzzyJoinOptionRecord.ValidateOptions(arguments[5], "Table.FuzzyJoin", false, false);
					TableTypeAlgebra.JoinKind joinKind = (arguments[4].IsNull ? TableTypeAlgebra.JoinKind.Inner : TableTypeAlgebra.GetJoinKind(arguments[4]));
					FuzzyJoinAlgorithm fuzzy = FuzzyJoinAlgorithm.Fuzzy;
					if (!fuzzy.Supports(joinKind))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.UnsupportedJoinKindForFuzzyJoins, null, null);
					}
					FuzzyJoinOptions fuzzyJoinOptions = FuzzyJoinOptions.ToFuzzyJoinOptions(recordValue, this.host);
					return FuzzyMatchingTableValueFactory.FuzzyJoin(this.host, arguments[0].AsTable, arguments[1], arguments[2].AsTable, arguments[3], joinKind, fuzzy, Value.Null, fuzzyJoinOptions);
				}

				// Token: 0x04002AF8 RID: 11000
				private readonly IEngineHost host;
			}

			// Token: 0x02000B49 RID: 2889
			public sealed class FuzzyNestedJoinFunctionValue : NativeFunctionValueN<TableValue>
			{
				// Token: 0x0600501A RID: 20506 RVA: 0x0010C4F4 File Offset: 0x0010A6F4
				public FuzzyNestedJoinFunctionValue(IEngineHost hostEnvironment)
					: base(TypeValue.Table, 5, new string[] { "table1", "key1", "table2", "key2", "newColumnName", "joinKind", "joinOptions" }, new TypeValue[]
					{
						TypeValue.Table,
						TypeValue.Any,
						TypeValue.Table,
						TypeValue.Any,
						TypeValue.Text,
						Library.JoinKind.Type.Nullable,
						FuzzyMatchingModule.fuzzyJoinOptionsType
					})
				{
					this.host = hostEnvironment;
				}

				// Token: 0x170018F2 RID: 6386
				// (get) Token: 0x0600501B RID: 20507 RVA: 0x0005DED2 File Offset: 0x0005C0D2
				public override IFunctionIdentity FunctionIdentity
				{
					get
					{
						return new FunctionTypeIdentity(base.GetType());
					}
				}

				// Token: 0x0600501C RID: 20508 RVA: 0x0010C598 File Offset: 0x0010A798
				protected override TableValue TypedInvokeN(Value[] arguments)
				{
					RecordValue recordValue = FuzzyMatchingModule.FuzzyJoinOptionRecord.ValidateOptions(arguments[6], "Table.FuzzyNestedJoin", false, false);
					if (!arguments[5].IsNull)
					{
						TableTypeAlgebra.GetJoinKind(arguments[5]);
					}
					FuzzyJoinOptions fuzzyJoinOptions = FuzzyJoinOptions.ToFuzzyJoinOptions(recordValue, this.host);
					return FuzzyMatchingTableValueFactory.FuzzyNestedJoin(this.host, arguments[0].AsTable, arguments[1], arguments[2].AsTable, arguments[3], arguments[5], arguments[4].AsText, Value.Null, fuzzyJoinOptions);
				}

				// Token: 0x04002AF9 RID: 11001
				private readonly IEngineHost host;
			}
		}
	}
}
