using System;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000176 RID: 374
	[XmlElementClass("MapCustomView", typeof(MapCustomView))]
	[XmlElementClass("MapElementView", typeof(MapElementView))]
	[XmlElementClass("MapDataBoundView", typeof(MapDataBoundView))]
	public abstract class MapView : ReportObject
	{
		// Token: 0x06000BE0 RID: 3040 RVA: 0x000208CC File Offset: 0x0001EACC
		public MapView()
		{
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000208D4 File Offset: 0x0001EAD4
		internal MapView(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x000208DD File Offset: 0x0001EADD
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x000208EB File Offset: 0x0001EAEB
		public ReportExpression<double> Zoom
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000208FF File Offset: 0x0001EAFF
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003A4 RID: 932
		internal class Definition : DefinitionStore<MapView, MapView.Definition.Properties>
		{
			// Token: 0x06001848 RID: 6216 RVA: 0x0003B5C1 File Offset: 0x000397C1
			private Definition()
			{
			}

			// Token: 0x020004BC RID: 1212
			internal enum Properties
			{
				// Token: 0x04000E24 RID: 3620
				Zoom
			}
		}
	}
}
