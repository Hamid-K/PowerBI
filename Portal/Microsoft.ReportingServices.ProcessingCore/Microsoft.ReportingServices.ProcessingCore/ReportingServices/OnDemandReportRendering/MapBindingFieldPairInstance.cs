using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001A3 RID: 419
	public sealed class MapBindingFieldPairInstance : BaseInstance
	{
		// Token: 0x060010D5 RID: 4309 RVA: 0x0004745D File Offset: 0x0004565D
		internal MapBindingFieldPairInstance(MapBindingFieldPair defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x00047478 File Offset: 0x00045678
		public string FieldName
		{
			get
			{
				if (this.m_fieldName == null)
				{
					this.m_fieldName = this.m_defObject.MapBindingFieldPairDef.EvaluateFieldName(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
				}
				return this.m_fieldName;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x000474C4 File Offset: 0x000456C4
		public object BindingExpression
		{
			get
			{
				if (this.m_bindingExpression == null)
				{
					this.m_bindingExpression = this.m_defObject.MapBindingFieldPairDef.EvaluateBindingExpression(this.m_defObject.ReportScope.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext).Value;
				}
				return this.m_bindingExpression;
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0004751F File Offset: 0x0004571F
		protected override void ResetInstanceCache()
		{
			this.m_fieldName = null;
			this.m_bindingExpression = null;
		}

		// Token: 0x040007F8 RID: 2040
		private MapBindingFieldPair m_defObject;

		// Token: 0x040007F9 RID: 2041
		private string m_fieldName;

		// Token: 0x040007FA RID: 2042
		private object m_bindingExpression;
	}
}
