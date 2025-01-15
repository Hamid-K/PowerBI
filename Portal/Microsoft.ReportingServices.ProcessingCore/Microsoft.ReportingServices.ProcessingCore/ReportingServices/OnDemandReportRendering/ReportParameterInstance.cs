using System;
using System.Collections.ObjectModel;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200030F RID: 783
	public sealed class ReportParameterInstance
	{
		// Token: 0x06001CDF RID: 7391 RVA: 0x00072CA0 File Offset: 0x00070EA0
		internal ReportParameterInstance(ReportParameter paramDef)
		{
			this.m_paramDef = paramDef;
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x00072CAF File Offset: 0x00070EAF
		internal ReportParameterInstance(ReportParameter paramDef, ReportParameter paramValue)
		{
			this.m_paramDef = paramDef;
			this.m_renderParamValue = paramValue;
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x00072CC5 File Offset: 0x00070EC5
		public string Prompt
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return this.m_renderParamValue.Prompt;
				}
				return this.ReportOMParam.Prompt;
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x00072CE8 File Offset: 0x00070EE8
		public object Value
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return this.m_renderParamValue.Value;
				}
				object[] values = this.ReportOMParam.GetValues();
				if (values == null || values.Length == 0)
				{
					return null;
				}
				return values[0];
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x00072D24 File Offset: 0x00070F24
		public ReadOnlyCollection<object> Values
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return Array.AsReadOnly<object>(this.m_renderParamValue.Values);
				}
				object[] values = this.ReportOMParam.GetValues();
				if (values == null || values.Length == 0)
				{
					return null;
				}
				return Array.AsReadOnly<object>(values);
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x00072D68 File Offset: 0x00070F68
		public string Label
		{
			get
			{
				string[] array;
				if (this.IsOldSnapshot)
				{
					array = this.m_renderParamValue.UnderlyingParam.Labels;
				}
				else
				{
					array = this.ReportOMParam.GetLabels();
				}
				if (array == null || array.Length == 0)
				{
					return null;
				}
				return array[0];
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x00072DA8 File Offset: 0x00070FA8
		public ReadOnlyCollection<string> Labels
		{
			get
			{
				string[] array;
				if (this.IsOldSnapshot)
				{
					array = this.m_renderParamValue.UnderlyingParam.Labels;
				}
				else
				{
					array = this.ReportOMParam.GetLabels();
				}
				if (array == null || array.Length == 0)
				{
					return null;
				}
				return Array.AsReadOnly<string>(array);
			}
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x00072DEB File Offset: 0x00070FEB
		internal void SetNewContext()
		{
			this.m_paramValue = null;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x00072DF4 File Offset: 0x00070FF4
		internal void UpdateRenderReportItem(ReportParameter paramValue)
		{
			this.m_renderParamValue = paramValue;
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x00072DFD File Offset: 0x00070FFD
		internal bool IsOldSnapshot
		{
			get
			{
				return this.m_renderParamValue != null;
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x00072E08 File Offset: 0x00071008
		internal ParameterImpl ReportOMParam
		{
			get
			{
				if (this.m_paramValue == null)
				{
					ParametersImpl parametersImpl = this.m_paramDef.OdpContext.ReportObjectModel.ParametersImpl;
					this.m_paramValue = (ParameterImpl)parametersImpl[this.m_paramDef.Name];
				}
				return this.m_paramValue;
			}
		}

		// Token: 0x04000F24 RID: 3876
		private ReportParameter m_paramDef;

		// Token: 0x04000F25 RID: 3877
		private ReportParameter m_renderParamValue;

		// Token: 0x04000F26 RID: 3878
		private ParameterImpl m_paramValue;
	}
}
