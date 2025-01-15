using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001DD RID: 477
	[DataContract(Name = "groupingMetadata", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class GroupingMetadata
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00019164 File Offset: 0x00017364
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x0001916C File Offset: 0x0001736C
		[DataMember(Name = "version", IsRequired = true, Order = 0)]
		public int Version { get; set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00019175 File Offset: 0x00017375
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x0001917D File Offset: 0x0001737D
		[DataMember(Name = "groupedColumns", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public IList<QueryExpressionContainer> GroupedColumns { get; set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00019186 File Offset: 0x00017386
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x0001918E File Offset: 0x0001738E
		[DataMember(Name = "binningMetadata", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public BinningMetadata BinningMetadata { get; set; }

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00019198 File Offset: 0x00017398
		public static GroupingMetadata Create(GroupingDefinition definition)
		{
			EmbeddedToStandaloneQueryExpressionRewriter sourceRefRewriter = EmbeddedToStandaloneQueryExpressionRewriter.Create(definition.Sources);
			return new GroupingMetadata
			{
				Version = 0,
				GroupedColumns = definition.GroupedColumns.ConvertAll<QueryExpressionContainer>((QueryExpressionContainer c) => sourceRefRewriter.Rewrite(c.Expression)),
				BinningMetadata = GroupingMetadata.CreateBinningMetadata(definition.BinItem)
			};
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x000191F8 File Offset: 0x000173F8
		private static BinningMetadata CreateBinningMetadata(BinItem binItem)
		{
			if (binItem == null)
			{
				return null;
			}
			if (binItem.Expression.Floor == null)
			{
				return null;
			}
			QueryFloorExpression floor = binItem.Expression.Floor;
			return new BinningMetadata
			{
				BinSize = new BinSize
				{
					Value = floor.Size,
					Unit = GroupingMetadata.GetBinUnit(floor.TimeUnit)
				}
			};
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00019260 File Offset: 0x00017460
		private static ConceptualBinUnit GetBinUnit(TimeUnit? timeUnit)
		{
			if (timeUnit == null)
			{
				return ConceptualBinUnit.Number;
			}
			switch (timeUnit.Value)
			{
			case TimeUnit.Day:
				return ConceptualBinUnit.Day;
			case TimeUnit.Week:
				return ConceptualBinUnit.Week;
			case TimeUnit.Month:
				return ConceptualBinUnit.Month;
			case TimeUnit.Year:
				return ConceptualBinUnit.Year;
			case TimeUnit.Second:
				return ConceptualBinUnit.Seconds;
			case TimeUnit.Minute:
				return ConceptualBinUnit.Minutes;
			case TimeUnit.Hour:
				return ConceptualBinUnit.Hours;
			}
			throw new NotImplementedException(StringUtil.FormatInvariant("Unsupported binning type {0}", Enum.GetName(typeof(TimeUnit), timeUnit.Value)));
		}
	}
}
