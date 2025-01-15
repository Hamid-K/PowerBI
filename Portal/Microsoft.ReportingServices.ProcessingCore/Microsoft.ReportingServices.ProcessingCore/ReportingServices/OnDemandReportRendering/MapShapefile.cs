using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001A9 RID: 425
	public sealed class MapShapefile : MapSpatialData
	{
		// Token: 0x060010FD RID: 4349 RVA: 0x000479EF File Offset: 0x00045BEF
		internal MapShapefile(MapVectorLayer mapVectorLayer, Map map)
			: base(mapVectorLayer, map)
		{
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x000479F9 File Offset: 0x00045BF9
		public ReportStringProperty Source
		{
			get
			{
				if (this.m_source == null && this.MapShapefileDef.Source != null)
				{
					this.m_source = new ReportStringProperty(this.MapShapefileDef.Source);
				}
				return this.m_source;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00047A2C File Offset: 0x00045C2C
		public MapFieldNameCollection MapFieldNames
		{
			get
			{
				if (this.m_mapFieldNames == null && this.MapShapefileDef.MapFieldNames != null)
				{
					this.m_mapFieldNames = new MapFieldNameCollection(this.MapShapefileDef.MapFieldNames, this.m_map);
				}
				return this.m_mapFieldNames;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x00047A65 File Offset: 0x00045C65
		internal MapShapefile MapShapefileDef
		{
			get
			{
				return (MapShapefile)base.MapSpatialDataDef;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x00047A72 File Offset: 0x00045C72
		public new MapShapefileInstance Instance
		{
			get
			{
				return (MapShapefileInstance)this.GetInstance();
			}
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00047A7F File Offset: 0x00045C7F
		internal override MapSpatialDataInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapShapefileInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00047AAF File Offset: 0x00045CAF
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapFieldNames != null)
			{
				this.m_mapFieldNames.SetNewContext();
			}
		}

		// Token: 0x0400080C RID: 2060
		private ReportStringProperty m_source;

		// Token: 0x0400080D RID: 2061
		private MapFieldNameCollection m_mapFieldNames;
	}
}
