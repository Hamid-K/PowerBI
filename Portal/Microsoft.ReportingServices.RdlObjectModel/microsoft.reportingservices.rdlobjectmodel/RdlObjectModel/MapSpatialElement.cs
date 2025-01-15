using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000198 RID: 408
	public abstract class MapSpatialElement : ReportObject
	{
		// Token: 0x06000D4B RID: 3403 RVA: 0x0002226C File Offset: 0x0002046C
		public MapSpatialElement()
		{
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00022274 File Offset: 0x00020474
		internal MapSpatialElement(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x0002227D File Offset: 0x0002047D
		// (set) Token: 0x06000D4E RID: 3406 RVA: 0x0002228B File Offset: 0x0002048B
		public VectorData VectorData
		{
			get
			{
				return base.PropertyStore.GetObject<VectorData>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0002229A File Offset: 0x0002049A
		// (set) Token: 0x06000D50 RID: 3408 RVA: 0x000222AD File Offset: 0x000204AD
		[XmlElement(typeof(RdlCollection<MapField>))]
		public IList<MapField> MapFields
		{
			get
			{
				return (IList<MapField>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x000222BC File Offset: 0x000204BC
		public override void Initialize()
		{
			base.Initialize();
			this.MapFields = new RdlCollection<MapField>();
		}

		// Token: 0x020003C4 RID: 964
		internal class Definition : DefinitionStore<MapSpatialElement, MapSpatialElement.Definition.Properties>
		{
			// Token: 0x06001868 RID: 6248 RVA: 0x0003B6C1 File Offset: 0x000398C1
			private Definition()
			{
			}

			// Token: 0x020004DC RID: 1244
			internal enum Properties
			{
				// Token: 0x04000FB4 RID: 4020
				VectorData,
				// Token: 0x04000FB5 RID: 4021
				MapFields
			}
		}
	}
}
