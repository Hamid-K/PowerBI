using System;
using System.Globalization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200017C RID: 380
	[XmlElementClass("MapMarkerTemplate", typeof(MapMarkerTemplate))]
	public abstract class MapPointTemplate : MapSpatialElementTemplate
	{
		// Token: 0x06000C1A RID: 3098 RVA: 0x00020C89 File Offset: 0x0001EE89
		public MapPointTemplate()
		{
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00020C91 File Offset: 0x0001EE91
		internal MapPointTemplate(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00020C9A File Offset: 0x0001EE9A
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x00020CA9 File Offset: 0x0001EEA9
		[ReportExpressionDefaultValue(typeof(ReportSize), "5.25pt")]
		public ReportExpression<ReportSize> Size
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

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00020CBE File Offset: 0x0001EEBE
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x00020CCD File Offset: 0x0001EECD
		[ReportExpressionDefaultValue(typeof(MapPointLabelPlacements), MapPointLabelPlacements.Bottom)]
		public ReportExpression<MapPointLabelPlacements> LabelPlacement
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapPointLabelPlacements>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00020CE2 File Offset: 0x0001EEE2
		public override void Initialize()
		{
			base.Initialize();
			this.Size = new ReportExpression<ReportSize>("5.25pt", CultureInfo.InvariantCulture);
			this.LabelPlacement = MapPointLabelPlacements.Bottom;
		}

		// Token: 0x020003AA RID: 938
		internal new class Definition : DefinitionStore<MapPointTemplate, MapPointTemplate.Definition.Properties>
		{
			// Token: 0x0600184E RID: 6222 RVA: 0x0003B5F1 File Offset: 0x000397F1
			private Definition()
			{
			}

			// Token: 0x020004C2 RID: 1218
			internal enum Properties
			{
				// Token: 0x04000E4F RID: 3663
				Style,
				// Token: 0x04000E50 RID: 3664
				ActionInfo,
				// Token: 0x04000E51 RID: 3665
				Hidden,
				// Token: 0x04000E52 RID: 3666
				OffsetX,
				// Token: 0x04000E53 RID: 3667
				OffsetY,
				// Token: 0x04000E54 RID: 3668
				Label,
				// Token: 0x04000E55 RID: 3669
				ToolTip,
				// Token: 0x04000E56 RID: 3670
				DataElementName,
				// Token: 0x04000E57 RID: 3671
				DataElementOutput,
				// Token: 0x04000E58 RID: 3672
				DataElementLabel,
				// Token: 0x04000E59 RID: 3673
				Size,
				// Token: 0x04000E5A RID: 3674
				LabelPlacement
			}
		}
	}
}
