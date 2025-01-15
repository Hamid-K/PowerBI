using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200017E RID: 382
	public class MapLineTemplate : MapSpatialElementTemplate
	{
		// Token: 0x06000C26 RID: 3110 RVA: 0x00020D48 File Offset: 0x0001EF48
		public MapLineTemplate()
		{
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00020D50 File Offset: 0x0001EF50
		internal MapLineTemplate(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00020D59 File Offset: 0x0001EF59
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x00020D68 File Offset: 0x0001EF68
		[ReportExpressionDefaultValue(typeof(ReportSize), "3.75pt")]
		public ReportExpression<ReportSize> Width
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00020D7D File Offset: 0x0001EF7D
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x00020D8C File Offset: 0x0001EF8C
		[ReportExpressionDefaultValue(typeof(MapLineLabelPlacements), MapLineLabelPlacements.Above)]
		public ReportExpression<MapLineLabelPlacements> LabelPlacement
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapLineLabelPlacements>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00020DA1 File Offset: 0x0001EFA1
		public override void Initialize()
		{
			base.Initialize();
			this.Width = new ReportExpression<ReportSize>("3.75pt", CultureInfo.InvariantCulture);
			this.LabelPlacement = MapLineLabelPlacements.Above;
		}

		// Token: 0x020003AC RID: 940
		internal new class Definition : DefinitionStore<MapLineTemplate, MapLineTemplate.Definition.Properties>
		{
			// Token: 0x06001850 RID: 6224 RVA: 0x0003B601 File Offset: 0x00039801
			private Definition()
			{
			}

			// Token: 0x020004C4 RID: 1220
			internal enum Properties
			{
				// Token: 0x04000E6B RID: 3691
				Style,
				// Token: 0x04000E6C RID: 3692
				ActionInfo,
				// Token: 0x04000E6D RID: 3693
				Hidden,
				// Token: 0x04000E6E RID: 3694
				OffsetX,
				// Token: 0x04000E6F RID: 3695
				OffsetY,
				// Token: 0x04000E70 RID: 3696
				Label,
				// Token: 0x04000E71 RID: 3697
				ToolTip,
				// Token: 0x04000E72 RID: 3698
				DataElementName,
				// Token: 0x04000E73 RID: 3699
				DataElementOutput,
				// Token: 0x04000E74 RID: 3700
				DataElementLabel,
				// Token: 0x04000E75 RID: 3701
				Width,
				// Token: 0x04000E76 RID: 3702
				LineLabelPlacement,
				// Token: 0x04000E77 RID: 3703
				PropertyCount
			}
		}
	}
}
