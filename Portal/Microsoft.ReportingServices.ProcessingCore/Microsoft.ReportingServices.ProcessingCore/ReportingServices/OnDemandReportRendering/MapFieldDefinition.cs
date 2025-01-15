using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001A5 RID: 421
	public sealed class MapFieldDefinition : MapObjectCollectionItem
	{
		// Token: 0x060010DD RID: 4317 RVA: 0x00047614 File Offset: 0x00045814
		internal MapFieldDefinition(MapFieldDefinition defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0004762A File Offset: 0x0004582A
		public string Name
		{
			get
			{
				return this.m_defObject.Name;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x00047637 File Offset: 0x00045837
		public MapDataType DataType
		{
			get
			{
				return this.m_defObject.DataType;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x00047644 File Offset: 0x00045844
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0004764C File Offset: 0x0004584C
		internal MapFieldDefinition MapFieldDefinitionDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x00047654 File Offset: 0x00045854
		public MapFieldDefinitionInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapFieldDefinitionInstance(this);
				}
				return (MapFieldDefinitionInstance)this.m_instance;
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00047689 File Offset: 0x00045889
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x040007FE RID: 2046
		private Map m_map;

		// Token: 0x040007FF RID: 2047
		private MapFieldDefinition m_defObject;
	}
}
