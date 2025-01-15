using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001A3 RID: 419
	public class MapBorderSkin : ReportObject
	{
		// Token: 0x06000DC4 RID: 3524 RVA: 0x00022A46 File Offset: 0x00020C46
		public MapBorderSkin()
		{
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00022A4E File Offset: 0x00020C4E
		internal MapBorderSkin(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00022A57 File Offset: 0x00020C57
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x00022A6A File Offset: 0x00020C6A
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00022A79 File Offset: 0x00020C79
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x00022A87 File Offset: 0x00020C87
		[ReportExpressionDefaultValue(typeof(MapBorderSkinTypes), MapBorderSkinTypes.None)]
		public ReportExpression<MapBorderSkinTypes> MapBorderSkinType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapBorderSkinTypes>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00022A9B File Offset: 0x00020C9B
		public override void Initialize()
		{
			base.Initialize();
			this.MapBorderSkinType = MapBorderSkinTypes.None;
		}

		// Token: 0x020003CF RID: 975
		internal class Definition : DefinitionStore<MapBorderSkin, MapBorderSkin.Definition.Properties>
		{
			// Token: 0x06001873 RID: 6259 RVA: 0x0003B719 File Offset: 0x00039919
			private Definition()
			{
			}

			// Token: 0x020004E7 RID: 1255
			internal enum Properties
			{
				// Token: 0x0400100D RID: 4109
				Style,
				// Token: 0x0400100E RID: 4110
				MapBorderSkinType,
				// Token: 0x0400100F RID: 4111
				PropertyCount
			}
		}
	}
}
