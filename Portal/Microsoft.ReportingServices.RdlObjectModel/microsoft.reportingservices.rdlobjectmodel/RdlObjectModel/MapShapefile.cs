using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200018D RID: 397
	public class MapShapefile : MapSpatialData
	{
		// Token: 0x06000CB0 RID: 3248 RVA: 0x00021699 File Offset: 0x0001F899
		public MapShapefile()
		{
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000216A1 File Offset: 0x0001F8A1
		internal MapShapefile(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x000216AA File Offset: 0x0001F8AA
		// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x000216B8 File Offset: 0x0001F8B8
		public ReportExpression Source
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x000216CC File Offset: 0x0001F8CC
		// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x000216DF File Offset: 0x0001F8DF
		[XmlElement(typeof(RdlCollection<ReportExpression>))]
		[XmlArrayItem("MapFieldName", typeof(ReportExpression))]
		public IList<ReportExpression> MapFieldNames
		{
			get
			{
				return (IList<ReportExpression>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x000216EE File Offset: 0x0001F8EE
		public override void Initialize()
		{
			base.Initialize();
			this.MapFieldNames = new RdlCollection<ReportExpression>();
		}

		// Token: 0x020003BA RID: 954
		internal class Definition : DefinitionStore<MapShapefile, MapShapefile.Definition.Properties>
		{
			// Token: 0x0600185E RID: 6238 RVA: 0x0003B671 File Offset: 0x00039871
			private Definition()
			{
			}

			// Token: 0x020004D2 RID: 1234
			internal enum Properties
			{
				// Token: 0x04000F25 RID: 3877
				Source,
				// Token: 0x04000F26 RID: 3878
				MapFieldNames,
				// Token: 0x04000F27 RID: 3879
				PropertyCount
			}
		}
	}
}
