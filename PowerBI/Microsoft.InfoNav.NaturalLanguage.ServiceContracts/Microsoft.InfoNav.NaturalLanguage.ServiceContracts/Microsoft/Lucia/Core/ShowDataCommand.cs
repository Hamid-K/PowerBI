using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200013F RID: 319
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ShowDataCommand
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0000B23F File Offset: 0x0000943F
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x0000B247 File Offset: 0x00009447
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 1)]
		public QueryDefinition Query { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0000B250 File Offset: 0x00009450
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x0000B258 File Offset: 0x00009458
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public IList<SuggestedVisualization> SuggestedVisualizations { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0000B261 File Offset: 0x00009461
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x0000B269 File Offset: 0x00009469
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public string Data { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0000B272 File Offset: 0x00009472
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x0000B27A File Offset: 0x0000947A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public string Binding { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0000B283 File Offset: 0x00009483
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0000B28B File Offset: 0x0000948B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 5)]
		public IList<VisualConfiguration> VisualConfigurations { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0000B294 File Offset: 0x00009494
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x0000B29C File Offset: 0x0000949C
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 6)]
		public QueryMetadata QueryMetadata { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0000B2A5 File Offset: 0x000094A5
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x0000B2AD File Offset: 0x000094AD
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 7)]
		public InsightQueryType InsightType { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0000B2B6 File Offset: 0x000094B6
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x0000B2BE File Offset: 0x000094BE
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<CommandArgument> InsightArguments { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0000B2C7 File Offset: 0x000094C7
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x0000B2CF File Offset: 0x000094CF
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string LinguisticSchemaJson { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0000B2D8 File Offset: 0x000094D8
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0000B2E0 File Offset: 0x000094E0
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<string> LinguisticSchemaItems { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0000B2E9 File Offset: 0x000094E9
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0000B2F1 File Offset: 0x000094F1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public string InferredTermMetadata { get; set; }

		// Token: 0x06000652 RID: 1618 RVA: 0x0000B2FA File Offset: 0x000094FA
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0000B304 File Offset: 0x00009504
		public string ToString(bool skipQueryDefinition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!skipQueryDefinition && this.Query != null)
			{
				stringBuilder.AppendLine(this.Query.ToString());
				stringBuilder.AppendLine();
			}
			if (!this.SuggestedVisualizations.IsNullOrEmptyCollection<SuggestedVisualization>())
			{
				foreach (SuggestedVisualization suggestedVisualization in this.SuggestedVisualizations)
				{
					stringBuilder.AppendLine(suggestedVisualization.ToString());
				}
			}
			if (!this.VisualConfigurations.IsNullOrEmptyCollection<VisualConfiguration>())
			{
				foreach (VisualConfiguration visualConfiguration in this.VisualConfigurations)
				{
					stringBuilder.AppendLine(visualConfiguration.ToString());
				}
			}
			return stringBuilder.ToString().TrimEnd(Array.Empty<char>());
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0000B3F8 File Offset: 0x000095F8
		public VisualConfiguration GetBestVisualConfiguration()
		{
			if (this.VisualConfigurations != null && this.VisualConfigurations.Count > 0)
			{
				return this.VisualConfigurations[0];
			}
			return null;
		}
	}
}
