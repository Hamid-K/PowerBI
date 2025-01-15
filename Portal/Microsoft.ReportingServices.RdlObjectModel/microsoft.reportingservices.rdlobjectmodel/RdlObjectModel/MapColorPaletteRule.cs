using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000182 RID: 386
	public class MapColorPaletteRule : MapColorRule
	{
		// Token: 0x06000C52 RID: 3154 RVA: 0x0002107A File Offset: 0x0001F27A
		public MapColorPaletteRule()
		{
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00021082 File Offset: 0x0001F282
		internal MapColorPaletteRule(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0002108B File Offset: 0x0001F28B
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x0002109A File Offset: 0x0001F29A
		[ReportExpressionDefaultValue(typeof(MapPalettes), MapPalettes.Random)]
		public ReportExpression<MapPalettes> Palette
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapPalettes>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x000210AF File Offset: 0x0001F2AF
		public override void Initialize()
		{
			base.Initialize();
			this.Palette = MapPalettes.Random;
		}

		// Token: 0x020003B0 RID: 944
		internal new class Definition : DefinitionStore<MapColorPaletteRule, MapColorPaletteRule.Definition.Properties>
		{
			// Token: 0x06001854 RID: 6228 RVA: 0x0003B621 File Offset: 0x00039821
			private Definition()
			{
			}

			// Token: 0x020004C8 RID: 1224
			internal enum Properties
			{
				// Token: 0x04000EA0 RID: 3744
				DataValue,
				// Token: 0x04000EA1 RID: 3745
				DistributionType,
				// Token: 0x04000EA2 RID: 3746
				BucketCount,
				// Token: 0x04000EA3 RID: 3747
				StartValue,
				// Token: 0x04000EA4 RID: 3748
				EndValue,
				// Token: 0x04000EA5 RID: 3749
				MapBuckets,
				// Token: 0x04000EA6 RID: 3750
				LegendName,
				// Token: 0x04000EA7 RID: 3751
				LegendText,
				// Token: 0x04000EA8 RID: 3752
				DataElementName,
				// Token: 0x04000EA9 RID: 3753
				DataElementOutput,
				// Token: 0x04000EAA RID: 3754
				ShowInColorScale,
				// Token: 0x04000EAB RID: 3755
				Palette,
				// Token: 0x04000EAC RID: 3756
				PropertyCount
			}
		}
	}
}
