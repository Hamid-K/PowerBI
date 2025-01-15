using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002DE RID: 734
	public sealed class Parameter
	{
		// Token: 0x06001B66 RID: 7014 RVA: 0x0006DEBC File Offset: 0x0006C0BC
		internal Parameter(ActionDrillthrough actionDef, Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue parameterDef)
		{
			this.m_name = parameterDef.Name;
			this.m_actionDef = actionDef;
			this.m_parameterDef = parameterDef;
			this.m_instance = new ParameterInstance(this);
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0006DEEC File Offset: 0x0006C0EC
		internal Parameter(ActionDrillthrough actionDef, Microsoft.ReportingServices.ReportProcessing.ParameterValue parameterDef, ActionItemInstance actionInstance, int index)
		{
			this.m_name = parameterDef.Name;
			this.m_value = new ReportVariantProperty(parameterDef.Value);
			this.m_omit = new ReportBoolProperty(parameterDef.Omit);
			this.m_actionDef = actionDef;
			this.m_instance = new ParameterInstance(actionInstance, index);
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x0006DF42 File Offset: 0x0006C142
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x0006DF4A File Offset: 0x0006C14A
		public ReportVariantProperty Value
		{
			get
			{
				if (this.m_value == null)
				{
					this.m_value = new ReportVariantProperty(this.m_parameterDef.Value);
				}
				return this.m_value;
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x0006DF70 File Offset: 0x0006C170
		public ReportBoolProperty Omit
		{
			get
			{
				if (this.m_omit == null)
				{
					this.m_omit = new ReportBoolProperty(this.m_parameterDef.Omit);
				}
				return this.m_omit;
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x0006DF96 File Offset: 0x0006C196
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue ParameterDef
		{
			get
			{
				return this.m_parameterDef;
			}
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0006DF9E File Offset: 0x0006C19E
		internal ActionDrillthrough ActionDef
		{
			get
			{
				return this.m_actionDef;
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x0006DFA6 File Offset: 0x0006C1A6
		public ParameterInstance Instance
		{
			get
			{
				if (this.m_actionDef.Owner.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0006DFC7 File Offset: 0x0006C1C7
		internal void Update(ActionItemInstance actionInstance, int index)
		{
			if (this.m_instance != null)
			{
				this.m_instance.Update(actionInstance, index);
			}
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x0006DFDE File Offset: 0x0006C1DE
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x0006DFF4 File Offset: 0x0006C1F4
		internal void ConstructParameterDefinition()
		{
			ParameterInstance instance = this.Instance;
			Global.Tracer.Assert(instance != null);
			if (instance.Value != null)
			{
				this.m_parameterDef.Value = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression((string)instance.Value);
			}
			else
			{
				this.m_parameterDef.Value = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			}
			this.m_value = null;
			if (instance.IsOmitAssined)
			{
				this.m_parameterDef.Omit = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(instance.Omit);
			}
			else
			{
				this.m_parameterDef.Omit = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			}
			this.m_omit = null;
		}

		// Token: 0x04000D8D RID: 3469
		private string m_name;

		// Token: 0x04000D8E RID: 3470
		private ReportVariantProperty m_value;

		// Token: 0x04000D8F RID: 3471
		private ReportBoolProperty m_omit;

		// Token: 0x04000D90 RID: 3472
		private ParameterInstance m_instance;

		// Token: 0x04000D91 RID: 3473
		private ActionDrillthrough m_actionDef;

		// Token: 0x04000D92 RID: 3474
		private Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue m_parameterDef;
	}
}
