using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001C6 RID: 454
	public sealed class MapSizeRule : MapAppearanceRule
	{
		// Token: 0x060011AE RID: 4526 RVA: 0x00049629 File Offset: 0x00047829
		internal MapSizeRule(MapSizeRule defObject, MapVectorLayer mapVectorLayer, Map map)
			: base(defObject, mapVectorLayer, map)
		{
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00049634 File Offset: 0x00047834
		public ReportSizeProperty StartSize
		{
			get
			{
				if (this.m_startSize == null && this.MapSizeRuleDef.StartSize != null)
				{
					this.m_startSize = new ReportSizeProperty(this.MapSizeRuleDef.StartSize);
				}
				return this.m_startSize;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x00049667 File Offset: 0x00047867
		public ReportSizeProperty EndSize
		{
			get
			{
				if (this.m_endSize == null && this.MapSizeRuleDef.EndSize != null)
				{
					this.m_endSize = new ReportSizeProperty(this.MapSizeRuleDef.EndSize);
				}
				return this.m_endSize;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0004969A File Offset: 0x0004789A
		internal MapSizeRule MapSizeRuleDef
		{
			get
			{
				return (MapSizeRule)base.MapAppearanceRuleDef;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x000496A7 File Offset: 0x000478A7
		public new MapSizeRuleInstance Instance
		{
			get
			{
				return (MapSizeRuleInstance)this.GetInstance();
			}
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x000496B4 File Offset: 0x000478B4
		internal override MapAppearanceRuleInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapSizeRuleInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000496E4 File Offset: 0x000478E4
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000863 RID: 2147
		private ReportSizeProperty m_startSize;

		// Token: 0x04000864 RID: 2148
		private ReportSizeProperty m_endSize;
	}
}
