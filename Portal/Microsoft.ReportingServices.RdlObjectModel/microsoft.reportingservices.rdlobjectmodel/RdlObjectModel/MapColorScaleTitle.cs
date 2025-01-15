using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001AD RID: 429
	public class MapColorScaleTitle : ReportObject
	{
		// Token: 0x06000E19 RID: 3609 RVA: 0x00022F21 File Offset: 0x00021121
		public MapColorScaleTitle()
		{
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00022F29 File Offset: 0x00021129
		internal MapColorScaleTitle(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00022F32 File Offset: 0x00021132
		// (set) Token: 0x06000E1C RID: 3612 RVA: 0x00022F45 File Offset: 0x00021145
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

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00022F54 File Offset: 0x00021154
		// (set) Token: 0x06000E1E RID: 3614 RVA: 0x00022F62 File Offset: 0x00021162
		public ReportExpression Caption
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00022F76 File Offset: 0x00021176
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003D9 RID: 985
		internal class Definition : DefinitionStore<MapColorScaleTitle, MapColorScaleTitle.Definition.Properties>
		{
			// Token: 0x0600187D RID: 6269 RVA: 0x0003B769 File Offset: 0x00039969
			private Definition()
			{
			}

			// Token: 0x020004F1 RID: 1265
			internal enum Properties
			{
				// Token: 0x0400103C RID: 4156
				Style,
				// Token: 0x0400103D RID: 4157
				Caption,
				// Token: 0x0400103E RID: 4158
				PropertyCount
			}
		}
	}
}
