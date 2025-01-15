using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000174 RID: 372
	public sealed class MapDataRegion : DataRegion, IMapObjectCollectionItem
	{
		// Token: 0x06000F9F RID: 3999 RVA: 0x00043BEE File Offset: 0x00041DEE
		internal MapDataRegion(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, MapDataRegion reportItemDef, RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollectionDef, reportItemDef, renderingContext)
		{
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00043BFB File Offset: 0x00041DFB
		void IMapObjectCollectionItem.SetNewContext()
		{
			this.SetNewContext();
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00043C03 File Offset: 0x00041E03
		public MapMember MapMember
		{
			get
			{
				if (this.m_mapMember == null)
				{
					this.m_mapMember = new MapMember(this.ReportScope, this, this, null, this.MapDataRegionDef.MapMember);
				}
				return this.m_mapMember;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00043C32 File Offset: 0x00041E32
		internal MapMember InnerMostMapMember
		{
			get
			{
				if (this.m_innerMostMampMember == null)
				{
					this.m_innerMostMampMember = this.MapMember;
					while (this.m_innerMostMampMember.ChildMapMember != null)
					{
						this.m_innerMostMampMember = this.m_innerMostMampMember.ChildMapMember;
					}
				}
				return this.m_innerMostMampMember;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00043C6E File Offset: 0x00041E6E
		internal MapDataRegion MapDataRegionDef
		{
			get
			{
				return this.m_reportItemDef as MapDataRegion;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00043C7B File Offset: 0x00041E7B
		internal override bool HasDataCells
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00043C7E File Offset: 0x00041E7E
		internal override IDataRegionRowCollection RowCollection
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x00043C81 File Offset: 0x00041E81
		public new MapDataRegionInstance Instance
		{
			get
			{
				return (MapDataRegionInstance)this.GetOrCreateInstance();
			}
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00043C90 File Offset: 0x00041E90
		internal List<MapVectorLayer> GetChildLayers()
		{
			ReportElementCollectionBase<MapLayer> mapLayers = ((Map)this.m_parentDefinitionPath).MapLayers;
			List<MapVectorLayer> list = new List<MapVectorLayer>();
			foreach (MapLayer mapLayer in mapLayers)
			{
				if (!(mapLayer is MapTileLayer) && ((MapVectorLayer)mapLayer).MapDataRegion == this)
				{
					list.Add((MapVectorLayer)mapLayer);
				}
			}
			return list;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00043D0C File Offset: 0x00041F0C
		internal override ReportItemInstance GetOrCreateInstance()
		{
			if (this.m_instance == null)
			{
				this.m_instance = new MapDataRegionInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00043D28 File Offset: 0x00041F28
		internal override void SetNewContextChildren()
		{
			if (this.m_mapMember != null)
			{
				this.m_mapMember.ResetContext();
			}
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000745 RID: 1861
		private MapMember m_innerMostMampMember;

		// Token: 0x04000746 RID: 1862
		private MapMember m_mapMember;
	}
}
