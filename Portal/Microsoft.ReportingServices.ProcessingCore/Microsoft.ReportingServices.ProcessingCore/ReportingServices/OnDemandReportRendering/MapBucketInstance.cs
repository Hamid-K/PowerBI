using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001CE RID: 462
	public sealed class MapBucketInstance : BaseInstance
	{
		// Token: 0x060011F5 RID: 4597 RVA: 0x0004A27C File Offset: 0x0004847C
		internal MapBucketInstance(MapBucket defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0004A298 File Offset: 0x00048498
		public object StartValue
		{
			get
			{
				if (this.m_startValue == null)
				{
					this.m_startValue = this.m_defObject.MapBucketDef.EvaluateStartValue(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext).Value;
				}
				return this.m_startValue;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0004A2EC File Offset: 0x000484EC
		public object EndValue
		{
			get
			{
				if (this.m_endValue == null)
				{
					this.m_endValue = this.m_defObject.MapBucketDef.EvaluateEndValue(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext).Value;
				}
				return this.m_endValue;
			}
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0004A33D File Offset: 0x0004853D
		protected override void ResetInstanceCache()
		{
			this.m_startValue = null;
			this.m_endValue = null;
		}

		// Token: 0x04000885 RID: 2181
		private MapBucket m_defObject;

		// Token: 0x04000886 RID: 2182
		private object m_startValue;

		// Token: 0x04000887 RID: 2183
		private object m_endValue;
	}
}
