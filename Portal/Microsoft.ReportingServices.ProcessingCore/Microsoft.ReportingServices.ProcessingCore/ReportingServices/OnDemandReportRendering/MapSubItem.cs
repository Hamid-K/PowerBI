using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200018D RID: 397
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapSubItem : MapObjectCollectionItem, IROMStyleDefinitionContainer
	{
		// Token: 0x0600101B RID: 4123 RVA: 0x00044C34 File Offset: 0x00042E34
		internal MapSubItem(MapSubItem defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x00044C4A File Offset: 0x00042E4A
		public Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new Style(this.m_map, this.m_map.ReportScope, this.m_defObject, this.m_map.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x00044C87 File Offset: 0x00042E87
		public MapLocation MapLocation
		{
			get
			{
				if (this.m_mapLocation == null && this.m_defObject.MapLocation != null)
				{
					this.m_mapLocation = new MapLocation(this.m_defObject.MapLocation, this.m_map);
				}
				return this.m_mapLocation;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00044CC0 File Offset: 0x00042EC0
		public MapSize MapSize
		{
			get
			{
				if (this.m_mapSize == null && this.m_defObject.MapSize != null)
				{
					this.m_mapSize = new MapSize(this.m_defObject.MapSize, this.m_map);
				}
				return this.m_mapSize;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x00044CF9 File Offset: 0x00042EF9
		public ReportSizeProperty LeftMargin
		{
			get
			{
				if (this.m_leftMargin == null && this.m_defObject.LeftMargin != null)
				{
					this.m_leftMargin = new ReportSizeProperty(this.m_defObject.LeftMargin);
				}
				return this.m_leftMargin;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x00044D2C File Offset: 0x00042F2C
		public ReportSizeProperty RightMargin
		{
			get
			{
				if (this.m_rightMargin == null && this.m_defObject.RightMargin != null)
				{
					this.m_rightMargin = new ReportSizeProperty(this.m_defObject.RightMargin);
				}
				return this.m_rightMargin;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x00044D5F File Offset: 0x00042F5F
		public ReportSizeProperty TopMargin
		{
			get
			{
				if (this.m_topMargin == null && this.m_defObject.TopMargin != null)
				{
					this.m_topMargin = new ReportSizeProperty(this.m_defObject.TopMargin);
				}
				return this.m_topMargin;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x00044D92 File Offset: 0x00042F92
		public ReportSizeProperty BottomMargin
		{
			get
			{
				if (this.m_bottomMargin == null && this.m_defObject.BottomMargin != null)
				{
					this.m_bottomMargin = new ReportSizeProperty(this.m_defObject.BottomMargin);
				}
				return this.m_bottomMargin;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x00044DC8 File Offset: 0x00042FC8
		public ReportIntProperty ZIndex
		{
			get
			{
				if (this.m_zIndex == null && this.m_defObject.ZIndex != null)
				{
					this.m_zIndex = new ReportIntProperty(this.m_defObject.ZIndex.IsExpression, this.m_defObject.ZIndex.OriginalText, this.m_defObject.ZIndex.IntValue, 0);
				}
				return this.m_zIndex;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x00044E2C File Offset: 0x0004302C
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x00044E34 File Offset: 0x00043034
		internal MapSubItem MapSubItemDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x00044E3C File Offset: 0x0004303C
		internal MapSubItemInstance Instance
		{
			get
			{
				return this.GetInstance();
			}
		}

		// Token: 0x06001027 RID: 4135
		internal abstract MapSubItemInstance GetInstance();

		// Token: 0x06001028 RID: 4136 RVA: 0x00044E44 File Offset: 0x00043044
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			if (this.m_mapLocation != null)
			{
				this.m_mapLocation.SetNewContext();
			}
			if (this.m_mapSize != null)
			{
				this.m_mapSize.SetNewContext();
			}
		}

		// Token: 0x0400077F RID: 1919
		protected Map m_map;

		// Token: 0x04000780 RID: 1920
		internal MapSubItem m_defObject;

		// Token: 0x04000781 RID: 1921
		private Style m_style;

		// Token: 0x04000782 RID: 1922
		private MapLocation m_mapLocation;

		// Token: 0x04000783 RID: 1923
		private MapSize m_mapSize;

		// Token: 0x04000784 RID: 1924
		private ReportSizeProperty m_leftMargin;

		// Token: 0x04000785 RID: 1925
		private ReportSizeProperty m_rightMargin;

		// Token: 0x04000786 RID: 1926
		private ReportSizeProperty m_topMargin;

		// Token: 0x04000787 RID: 1927
		private ReportSizeProperty m_bottomMargin;

		// Token: 0x04000788 RID: 1928
		private ReportIntProperty m_zIndex;
	}
}
