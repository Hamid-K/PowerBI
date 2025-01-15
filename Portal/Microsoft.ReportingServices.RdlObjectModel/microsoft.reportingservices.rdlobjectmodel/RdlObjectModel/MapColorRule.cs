using System;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000180 RID: 384
	[XmlElementClass("MapColorRangeRule", typeof(MapColorRangeRule))]
	[XmlElementClass("MapColorPaletteRule", typeof(MapColorPaletteRule))]
	[XmlElementClass("MapCustomColorRule", typeof(MapCustomColorRule))]
	public abstract class MapColorRule : MapAppearanceRule
	{
		// Token: 0x06000C44 RID: 3140 RVA: 0x00020F6C File Offset: 0x0001F16C
		public MapColorRule()
		{
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00020F74 File Offset: 0x0001F174
		internal MapColorRule(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00020F7D File Offset: 0x0001F17D
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x00020F8C File Offset: 0x0001F18C
		public ReportExpression<bool> ShowInColorScale
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00020FA1 File Offset: 0x0001F1A1
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003AE RID: 942
		internal new class Definition : DefinitionStore<MapColorRule, MapColorRule.Definition.Properties>
		{
			// Token: 0x06001852 RID: 6226 RVA: 0x0003B611 File Offset: 0x00039811
			private Definition()
			{
			}

			// Token: 0x020004C6 RID: 1222
			internal enum Properties
			{
				// Token: 0x04000E84 RID: 3716
				DataValue,
				// Token: 0x04000E85 RID: 3717
				DistributionType,
				// Token: 0x04000E86 RID: 3718
				BucketCount,
				// Token: 0x04000E87 RID: 3719
				StartValue,
				// Token: 0x04000E88 RID: 3720
				EndValue,
				// Token: 0x04000E89 RID: 3721
				MapBuckets,
				// Token: 0x04000E8A RID: 3722
				LegendName,
				// Token: 0x04000E8B RID: 3723
				LegendText,
				// Token: 0x04000E8C RID: 3724
				DataElementName,
				// Token: 0x04000E8D RID: 3725
				DataElementOutput,
				// Token: 0x04000E8E RID: 3726
				ShowInColorScale
			}
		}
	}
}
