using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001B4 RID: 436
	public sealed class MapFieldNameInstance : BaseInstance
	{
		// Token: 0x0600114F RID: 4431 RVA: 0x00048613 File Offset: 0x00046813
		internal MapFieldNameInstance(MapFieldName defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x00048630 File Offset: 0x00046830
		public string Name
		{
			get
			{
				if (this.m_name == null)
				{
					this.m_name = this.m_defObject.MapFieldNameDef.EvaluateName(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
				}
				return this.m_name;
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0004867C File Offset: 0x0004687C
		protected override void ResetInstanceCache()
		{
			this.m_name = null;
		}

		// Token: 0x0400082C RID: 2092
		private MapFieldName m_defObject;

		// Token: 0x0400082D RID: 2093
		private string m_name;
	}
}
