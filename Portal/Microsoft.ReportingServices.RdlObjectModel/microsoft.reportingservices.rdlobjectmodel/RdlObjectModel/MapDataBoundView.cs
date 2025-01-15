using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000179 RID: 377
	public class MapDataBoundView : MapView
	{
		// Token: 0x06000BF3 RID: 3059 RVA: 0x000209F4 File Offset: 0x0001EBF4
		public MapDataBoundView()
		{
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000209FC File Offset: 0x0001EBFC
		internal MapDataBoundView(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00020A05 File Offset: 0x0001EC05
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003A7 RID: 935
		internal new class Definition : DefinitionStore<MapDataBoundView, MapDataBoundView.Definition.Properties>
		{
			// Token: 0x0600184B RID: 6219 RVA: 0x0003B5D9 File Offset: 0x000397D9
			private Definition()
			{
			}

			// Token: 0x020004BF RID: 1215
			internal enum Properties
			{
				// Token: 0x04000E30 RID: 3632
				Zoom,
				// Token: 0x04000E31 RID: 3633
				PropertyCount
			}
		}
	}
}
