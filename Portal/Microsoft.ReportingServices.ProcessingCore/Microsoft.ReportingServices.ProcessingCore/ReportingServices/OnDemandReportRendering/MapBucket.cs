using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001C0 RID: 448
	public sealed class MapBucket : MapObjectCollectionItem
	{
		// Token: 0x06001187 RID: 4487 RVA: 0x00048F02 File Offset: 0x00047102
		internal MapBucket(MapBucket defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00048F18 File Offset: 0x00047118
		public ReportVariantProperty StartValue
		{
			get
			{
				if (this.m_startValue == null && this.m_defObject.StartValue != null)
				{
					this.m_startValue = new ReportVariantProperty(this.m_defObject.StartValue);
				}
				return this.m_startValue;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x00048F4B File Offset: 0x0004714B
		public ReportVariantProperty EndValue
		{
			get
			{
				if (this.m_endValue == null && this.m_defObject.EndValue != null)
				{
					this.m_endValue = new ReportVariantProperty(this.m_defObject.EndValue);
				}
				return this.m_endValue;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x00048F7E File Offset: 0x0004717E
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x00048F86 File Offset: 0x00047186
		internal MapBucket MapBucketDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00048F8E File Offset: 0x0004718E
		public MapBucketInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapBucketInstance(this);
				}
				return (MapBucketInstance)this.m_instance;
			}
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00048FC3 File Offset: 0x000471C3
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x0400084F RID: 2127
		private Map m_map;

		// Token: 0x04000850 RID: 2128
		private MapBucket m_defObject;

		// Token: 0x04000851 RID: 2129
		private ReportVariantProperty m_startValue;

		// Token: 0x04000852 RID: 2130
		private ReportVariantProperty m_endValue;
	}
}
