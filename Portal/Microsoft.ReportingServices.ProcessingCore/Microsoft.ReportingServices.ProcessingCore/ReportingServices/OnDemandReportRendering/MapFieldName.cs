using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001A6 RID: 422
	public sealed class MapFieldName : MapObjectCollectionItem
	{
		// Token: 0x060010E4 RID: 4324 RVA: 0x0004769E File Offset: 0x0004589E
		internal MapFieldName(MapFieldName defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x000476B4 File Offset: 0x000458B4
		public ReportStringProperty Name
		{
			get
			{
				if (this.m_name == null && this.m_defObject.Name != null)
				{
					this.m_name = new ReportStringProperty(this.m_defObject.Name);
				}
				return this.m_name;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x000476E7 File Offset: 0x000458E7
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x000476EF File Offset: 0x000458EF
		internal MapFieldName MapFieldNameDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x000476F7 File Offset: 0x000458F7
		public MapFieldNameInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapFieldNameInstance(this);
				}
				return (MapFieldNameInstance)this.m_instance;
			}
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0004772C File Offset: 0x0004592C
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000800 RID: 2048
		private Map m_map;

		// Token: 0x04000801 RID: 2049
		private MapFieldName m_defObject;

		// Token: 0x04000802 RID: 2050
		private ReportStringProperty m_name;
	}
}
