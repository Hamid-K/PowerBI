using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200030D RID: 781
	public sealed class ReportParameterCollection : ReportElementCollectionBase<ReportParameter>
	{
		// Token: 0x06001CC9 RID: 7369 RVA: 0x00072808 File Offset: 0x00070A08
		internal ReportParameterCollection(ParameterDefList parameterDefs, ReportParameterCollection paramValues)
		{
			this.m_parameters = new List<ReportParameter>(parameterDefs.Count);
			for (int i = 0; i < parameterDefs.Count; i++)
			{
				if (parameterDefs[i].PromptUser)
				{
					this.m_parameters.Add(new ReportParameter(parameterDefs[i]));
				}
			}
			this.UpdateRenderReportItem(paramValues);
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0007286C File Offset: 0x00070A6C
		internal ReportParameterCollection(OnDemandProcessingContext odpContext, List<Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef> parameterDefs, bool validInstance)
		{
			this.m_parameters = new List<ReportParameter>(parameterDefs.Count);
			for (int i = 0; i < parameterDefs.Count; i++)
			{
				if (parameterDefs[i].PromptUser)
				{
					this.m_parameters.Add(new ReportParameter(odpContext, parameterDefs[i]));
				}
			}
			this.SetNewContext(validInstance);
		}

		// Token: 0x17001013 RID: 4115
		public ReportParameter this[string name]
		{
			get
			{
				if (this.m_parametersByName == null)
				{
					this.m_parametersByName = new Dictionary<string, ReportParameter>(this.m_parameters.Count);
					for (int i = 0; i < this.m_parameters.Count; i++)
					{
						ReportParameter reportParameter = this.m_parameters[i];
						this.m_parametersByName.Add(reportParameter.Name, reportParameter);
					}
				}
				return this.m_parametersByName[name];
			}
		}

		// Token: 0x17001014 RID: 4116
		public override ReportParameter this[int index]
		{
			get
			{
				return this.m_parameters[index];
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x0007294A File Offset: 0x00070B4A
		public override int Count
		{
			get
			{
				return this.m_parameters.Count;
			}
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x00072958 File Offset: 0x00070B58
		internal NameValueCollection ToNameValueCollection
		{
			get
			{
				if (this.m_reportParameters == null && this.m_parameters != null)
				{
					int count = this.m_parameters.Count;
					this.m_reportParameters = new NameValueCollection(count);
					for (int i = 0; i < count; i++)
					{
						ReportParameter reportParameter = this.m_parameters[i];
						ReportParameterInstance instance = reportParameter.Instance;
						if (instance != null && instance.Values != null)
						{
							int count2 = instance.Values.Count;
							for (int j = 0; j < count2; j++)
							{
								this.m_reportParameters.Add(reportParameter.Name, Formatter.FormatWithInvariantCulture(instance.Values[j]));
							}
						}
					}
					if (count > 0)
					{
						this.m_reportParameters.Add("rs:ParameterLanguage", "");
					}
				}
				return this.m_reportParameters;
			}
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x00072A20 File Offset: 0x00070C20
		internal void SetNewContext(bool validInstance)
		{
			for (int i = 0; i < this.m_parameters.Count; i++)
			{
				this.m_parameters[i].SetNewContext(validInstance);
			}
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x00072A58 File Offset: 0x00070C58
		internal void UpdateRenderReportItem(ReportParameterCollection paramValues)
		{
			int count = this.m_parameters.Count;
			if (paramValues != null && paramValues.Count != count)
			{
				paramValues = null;
			}
			for (int i = 0; i < count; i++)
			{
				if (paramValues == null)
				{
					this.m_parameters[i].UpdateRenderReportItem(null);
				}
				else
				{
					this.m_parameters[i].UpdateRenderReportItem(paramValues[i]);
				}
			}
		}

		// Token: 0x04000F1B RID: 3867
		private List<ReportParameter> m_parameters;

		// Token: 0x04000F1C RID: 3868
		private Dictionary<string, ReportParameter> m_parametersByName;

		// Token: 0x04000F1D RID: 3869
		private NameValueCollection m_reportParameters;
	}
}
