using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002B2 RID: 690
	internal sealed class InternalDataValueInstance : DataValueInstance
	{
		// Token: 0x06001A65 RID: 6757 RVA: 0x0006A6E5 File Offset: 0x000688E5
		internal InternalDataValueInstance(IReportScope reportScope, DataValue dataValueDef)
			: base(reportScope)
		{
			this.m_dataValueDef = dataValueDef;
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06001A66 RID: 6758 RVA: 0x0006A6F5 File Offset: 0x000688F5
		public override string Name
		{
			get
			{
				if (this.m_name == null)
				{
					this.EvaluateNameAndValue();
				}
				return this.m_name;
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x0006A70B File Offset: 0x0006890B
		public override object Value
		{
			get
			{
				if (this.m_value == null)
				{
					this.EvaluateNameAndValue();
				}
				return this.m_value;
			}
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0006A724 File Offset: 0x00068924
		private void EvaluateNameAndValue()
		{
			TypeCode typeCode;
			this.m_dataValueDef.DataValueDef.EvaluateNameAndValue(null, this.ReportScopeInstance, this.m_dataValueDef.InstancePath, this.m_dataValueDef.RenderingContext.OdpContext, this.m_dataValueDef.IsChart ? ObjectType.Chart : ObjectType.CustomReportItem, this.m_dataValueDef.ObjectName, out this.m_name, out this.m_value, out typeCode);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0006A78F File Offset: 0x0006898F
		protected override void ResetInstanceCache()
		{
			this.m_name = null;
			this.m_value = null;
		}

		// Token: 0x04000D28 RID: 3368
		private DataValue m_dataValueDef;

		// Token: 0x04000D29 RID: 3369
		private string m_name;

		// Token: 0x04000D2A RID: 3370
		private object m_value;
	}
}
