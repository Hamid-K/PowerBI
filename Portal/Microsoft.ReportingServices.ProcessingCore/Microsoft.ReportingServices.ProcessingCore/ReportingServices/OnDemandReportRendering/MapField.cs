using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001E0 RID: 480
	public sealed class MapField : MapObjectCollectionItem
	{
		// Token: 0x06001250 RID: 4688 RVA: 0x0004B0D2 File Offset: 0x000492D2
		internal MapField(MapField defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x0004B0E8 File Offset: 0x000492E8
		public string Name
		{
			get
			{
				return this.m_defObject.Name;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x0004B0F5 File Offset: 0x000492F5
		public string Value
		{
			get
			{
				return this.m_defObject.Value;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0004B102 File Offset: 0x00049302
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0004B10A File Offset: 0x0004930A
		internal MapField MapFieldDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x0004B112 File Offset: 0x00049312
		public MapFieldInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapFieldInstance(this);
				}
				return (MapFieldInstance)this.m_instance;
			}
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0004B147 File Offset: 0x00049347
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x040008B7 RID: 2231
		private Map m_map;

		// Token: 0x040008B8 RID: 2232
		private MapField m_defObject;
	}
}
