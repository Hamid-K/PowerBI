using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001D4 RID: 468
	public sealed class MapSizeRuleInstance : MapAppearanceRuleInstance
	{
		// Token: 0x06001208 RID: 4616 RVA: 0x0004A5C4 File Offset: 0x000487C4
		internal MapSizeRuleInstance(MapSizeRule defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x0004A5D4 File Offset: 0x000487D4
		public ReportSize StartSize
		{
			get
			{
				if (this.m_startSize == null)
				{
					this.m_startSize = new ReportSize(((MapSizeRule)this.m_defObject.MapAppearanceRuleDef).EvaluateStartSize(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_startSize;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0004A62C File Offset: 0x0004882C
		public ReportSize EndSize
		{
			get
			{
				if (this.m_endSize == null)
				{
					this.m_endSize = new ReportSize(((MapSizeRule)this.m_defObject.MapAppearanceRuleDef).EvaluateEndSize(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_endSize;
			}
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0004A682 File Offset: 0x00048882
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_startSize = null;
			this.m_endSize = null;
		}

		// Token: 0x04000892 RID: 2194
		private MapSizeRule m_defObject;

		// Token: 0x04000893 RID: 2195
		private ReportSize m_startSize;

		// Token: 0x04000894 RID: 2196
		private ReportSize m_endSize;
	}
}
