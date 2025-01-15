using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000192 RID: 402
	public sealed class MapTitle : MapDockableSubItem
	{
		// Token: 0x06001051 RID: 4177 RVA: 0x0004570D File Offset: 0x0004390D
		internal MapTitle(MapTitle defObject, Map map)
			: base(defObject, map)
		{
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x00045717 File Offset: 0x00043917
		public string Name
		{
			get
			{
				return this.MapTitleDef.Name;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x00045724 File Offset: 0x00043924
		public ReportStringProperty Text
		{
			get
			{
				if (this.m_text == null && this.MapTitleDef.Text != null)
				{
					this.m_text = new ReportStringProperty(this.MapTitleDef.Text);
				}
				return this.m_text;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x00045757 File Offset: 0x00043957
		public ReportDoubleProperty Angle
		{
			get
			{
				if (this.m_angle == null && this.MapTitleDef.Angle != null)
				{
					this.m_angle = new ReportDoubleProperty(this.MapTitleDef.Angle);
				}
				return this.m_angle;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0004578A File Offset: 0x0004398A
		public ReportSizeProperty TextShadowOffset
		{
			get
			{
				if (this.m_textShadowOffset == null && this.MapTitleDef.TextShadowOffset != null)
				{
					this.m_textShadowOffset = new ReportSizeProperty(this.MapTitleDef.TextShadowOffset);
				}
				return this.m_textShadowOffset;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x000457BD File Offset: 0x000439BD
		internal MapTitle MapTitleDef
		{
			get
			{
				return (MapTitle)this.m_defObject;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x000457CA File Offset: 0x000439CA
		public new MapTitleInstance Instance
		{
			get
			{
				return (MapTitleInstance)this.GetInstance();
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x000457D7 File Offset: 0x000439D7
		internal override MapSubItemInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapTitleInstance(this);
			}
			return (MapSubItemInstance)this.m_instance;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0004580C File Offset: 0x00043A0C
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x0400079F RID: 1951
		private ReportStringProperty m_text;

		// Token: 0x040007A0 RID: 1952
		private ReportDoubleProperty m_angle;

		// Token: 0x040007A1 RID: 1953
		private ReportSizeProperty m_textShadowOffset;
	}
}
