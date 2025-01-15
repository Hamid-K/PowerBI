using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000190 RID: 400
	public sealed class MapColorScaleTitle : IROMStyleDefinitionContainer
	{
		// Token: 0x06001043 RID: 4163 RVA: 0x0004548B File Offset: 0x0004368B
		internal MapColorScaleTitle(MapColorScaleTitle defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x000454A1 File Offset: 0x000436A1
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

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x000454DE File Offset: 0x000436DE
		public ReportStringProperty Caption
		{
			get
			{
				if (this.m_caption == null && this.m_defObject.Caption != null)
				{
					this.m_caption = new ReportStringProperty(this.m_defObject.Caption);
				}
				return this.m_caption;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x00045511 File Offset: 0x00043711
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x00045519 File Offset: 0x00043719
		internal MapColorScaleTitle MapColorScaleTitleDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x00045521 File Offset: 0x00043721
		public MapColorScaleTitleInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapColorScaleTitleInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00045551 File Offset: 0x00043751
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
		}

		// Token: 0x04000798 RID: 1944
		private Map m_map;

		// Token: 0x04000799 RID: 1945
		private MapColorScaleTitle m_defObject;

		// Token: 0x0400079A RID: 1946
		private MapColorScaleTitleInstance m_instance;

		// Token: 0x0400079B RID: 1947
		private Style m_style;

		// Token: 0x0400079C RID: 1948
		private ReportStringProperty m_caption;
	}
}
