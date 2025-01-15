using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200017B RID: 379
	public sealed class MapSize
	{
		// Token: 0x06000FD3 RID: 4051 RVA: 0x0004438A File Offset: 0x0004258A
		internal MapSize(MapSize defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x000443A0 File Offset: 0x000425A0
		public ReportDoubleProperty Width
		{
			get
			{
				if (this.m_width == null && this.m_defObject.Width != null)
				{
					this.m_width = new ReportDoubleProperty(this.m_defObject.Width);
				}
				return this.m_width;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x000443D3 File Offset: 0x000425D3
		public ReportDoubleProperty Height
		{
			get
			{
				if (this.m_height == null && this.m_defObject.Height != null)
				{
					this.m_height = new ReportDoubleProperty(this.m_defObject.Height);
				}
				return this.m_height;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00044408 File Offset: 0x00042608
		public ReportEnumProperty<Unit> Unit
		{
			get
			{
				if (this.m_unit == null && this.m_defObject.Unit != null)
				{
					this.m_unit = new ReportEnumProperty<Unit>(this.m_defObject.Unit.IsExpression, this.m_defObject.Unit.OriginalText, EnumTranslator.TranslateUnit(this.m_defObject.Unit.StringValue, null));
				}
				return this.m_unit;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00044471 File Offset: 0x00042671
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00044479 File Offset: 0x00042679
		internal MapSize MapSizeDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00044481 File Offset: 0x00042681
		public MapSizeInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapSizeInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x000444B1 File Offset: 0x000426B1
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000755 RID: 1877
		private Map m_map;

		// Token: 0x04000756 RID: 1878
		private MapSize m_defObject;

		// Token: 0x04000757 RID: 1879
		private MapSizeInstance m_instance;

		// Token: 0x04000758 RID: 1880
		private ReportDoubleProperty m_width;

		// Token: 0x04000759 RID: 1881
		private ReportDoubleProperty m_height;

		// Token: 0x0400075A RID: 1882
		private ReportEnumProperty<Unit> m_unit;
	}
}
