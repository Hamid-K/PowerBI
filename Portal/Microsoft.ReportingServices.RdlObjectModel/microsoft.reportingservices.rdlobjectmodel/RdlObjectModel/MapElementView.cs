using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000178 RID: 376
	public class MapElementView : MapView
	{
		// Token: 0x06000BEC RID: 3052 RVA: 0x0002098C File Offset: 0x0001EB8C
		public MapElementView()
		{
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00020994 File Offset: 0x0001EB94
		internal MapElementView(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0002099D File Offset: 0x0001EB9D
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x000209AB File Offset: 0x0001EBAB
		public ReportExpression LayerName
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

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x000209BF File Offset: 0x0001EBBF
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x000209D2 File Offset: 0x0001EBD2
		[XmlElement(typeof(RdlCollection<MapBindingFieldPair>))]
		public IList<MapBindingFieldPair> MapBindingFieldPairs
		{
			get
			{
				return (IList<MapBindingFieldPair>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000209E1 File Offset: 0x0001EBE1
		public override void Initialize()
		{
			base.Initialize();
			this.MapBindingFieldPairs = new RdlCollection<MapBindingFieldPair>();
		}

		// Token: 0x020003A6 RID: 934
		internal new class Definition : DefinitionStore<MapElementView, MapElementView.Definition.Properties>
		{
			// Token: 0x0600184A RID: 6218 RVA: 0x0003B5D1 File Offset: 0x000397D1
			private Definition()
			{
			}

			// Token: 0x020004BE RID: 1214
			internal enum Properties
			{
				// Token: 0x04000E2B RID: 3627
				Zoom,
				// Token: 0x04000E2C RID: 3628
				LayerName,
				// Token: 0x04000E2D RID: 3629
				MapBindingFieldPairs,
				// Token: 0x04000E2E RID: 3630
				PropertyCount
			}
		}
	}
}
