using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Graph;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Lineage
{
	// Token: 0x02000A2D RID: 2605
	internal sealed class LineageModule : Module
	{
		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x060048AF RID: 18607 RVA: 0x000F30A8 File Offset: 0x000F12A8
		public override string Name
		{
			get
			{
				return "Lineage";
			}
		}

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x060048B0 RID: 18608 RVA: 0x000F30AF File Offset: 0x000F12AF
		public override Keys ExportKeys
		{
			get
			{
				if (LineageModule.exportKeys == null)
				{
					LineageModule.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Value.Lineage";
						}
						if (index != 1)
						{
							throw new InvalidOperationException();
						}
						return "Value.Traits";
					});
				}
				return LineageModule.exportKeys;
			}
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x000F30E8 File Offset: 0x000F12E8
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return LineageModule._Value.Lineage;
				}
				if (index != 1)
				{
					throw new InvalidOperationException();
				}
				return new LineageModule._Value.TraitsFunctionValue(host);
			});
		}

		// Token: 0x040026DF RID: 9951
		public static readonly Keys TraitsKeys = Keys.New("Provider", "Identifier", "Value");

		// Token: 0x040026E0 RID: 9952
		private static readonly RecordTypeValue traitsRowType = RecordTypeValue.New(RecordValue.New(LineageModule.TraitsKeys, new Value[]
		{
			RecordTypeValue.NewField(TypeValue.Text, null),
			RecordTypeValue.NewField(TypeValue.Text, null),
			RecordTypeValue.NewField(TypeValue.Any, null)
		}));

		// Token: 0x040026E1 RID: 9953
		private static Keys exportKeys;

		// Token: 0x02000A2E RID: 2606
		private enum Exports
		{
			// Token: 0x040026E3 RID: 9955
			Value_Lineage,
			// Token: 0x040026E4 RID: 9956
			Value_Traits,
			// Token: 0x040026E5 RID: 9957
			Count
		}

		// Token: 0x02000A2F RID: 2607
		private static class _Value
		{
			// Token: 0x040026E6 RID: 9958
			public static readonly FunctionValue Lineage = new LineageModule._Value.LineageGraphFunctionValue();

			// Token: 0x02000A30 RID: 2608
			private class LineageGraphFunctionValue : NativeFunctionValue1<Value, Value>
			{
				// Token: 0x060048B5 RID: 18613 RVA: 0x000F3192 File Offset: 0x000F1392
				public LineageGraphFunctionValue()
					: base(TypeValue.Any, 1, "value", TypeValue.Any)
				{
				}

				// Token: 0x060048B6 RID: 18614 RVA: 0x000F31AA File Offset: 0x000F13AA
				public override Value TypedInvoke(Value value)
				{
					return LineageModule._Value.LineageGraphFunctionValue.lineageNode;
				}

				// Token: 0x040026E7 RID: 9959
				private static readonly RecordValue lineageNode = RecordValue.New(GraphModule.NodeKeys, new Value[]
				{
					TextValue.New(string.Empty),
					LineageModule._Value.LineageNodeValue.Instance,
					ListValue.Empty
				});
			}

			// Token: 0x02000A31 RID: 2609
			public class TraitsFunctionValue : NativeFunctionValue1<TableValue, Value>
			{
				// Token: 0x060048B8 RID: 18616 RVA: 0x000F31E5 File Offset: 0x000F13E5
				public TraitsFunctionValue(IEngineHost engineHost)
					: base(TypeValue.Table, "value", TypeValue.Any)
				{
					this.traitTrackingService = engineHost.QueryService<ITraitTrackingService>();
				}

				// Token: 0x060048B9 RID: 18617 RVA: 0x000F3208 File Offset: 0x000F1408
				public override TableValue TypedInvoke(Value value)
				{
					if (value is LineageModule._Value.LineageNodeValue)
					{
						ITraitTrackingService traitTrackingService = this.traitTrackingService;
						IRecordValue[] array = ((traitTrackingService != null) ? traitTrackingService.GetTraits() : null);
						new List<IValueReference>();
						if (array != null && array.Length != 0)
						{
							return ListValue.New(array.Cast<IValueReference>()).ToTable(LineageModule._Value.TraitsFunctionValue.traitsTableType);
						}
					}
					return LineageModule._Value.TraitsFunctionValue.emptyTraitsTable;
				}

				// Token: 0x040026E8 RID: 9960
				private static readonly TableTypeValue traitsTableType = TableTypeValue.New(LineageModule.traitsRowType);

				// Token: 0x040026E9 RID: 9961
				private static readonly TableValue emptyTraitsTable = ListValue.Empty.ToTable(LineageModule._Value.TraitsFunctionValue.traitsTableType);

				// Token: 0x040026EA RID: 9962
				private readonly ITraitTrackingService traitTrackingService;
			}

			// Token: 0x02000A32 RID: 2610
			private class LineageNodeValue : DelegatingValue
			{
				// Token: 0x060048BB RID: 18619 RVA: 0x000F327D File Offset: 0x000F147D
				public LineageNodeValue()
					: base(Value.Null)
				{
				}

				// Token: 0x040026EB RID: 9963
				public static readonly Value Instance = new LineageModule._Value.LineageNodeValue();
			}
		}
	}
}
