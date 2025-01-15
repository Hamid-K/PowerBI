using System;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200030E RID: 782
	public sealed class ReportParameter
	{
		// Token: 0x06001CD1 RID: 7377 RVA: 0x00072ABB File Offset: 0x00070CBB
		internal ReportParameter(Microsoft.ReportingServices.ReportProcessing.ParameterDef renderParam)
		{
			this.m_renderParam = renderParam;
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x00072ACA File Offset: 0x00070CCA
		internal ReportParameter(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef paramDef)
		{
			this.m_paramDef = paramDef;
			this.m_odpContext = odpContext;
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00072AE0 File Offset: 0x00070CE0
		public string Name
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return this.m_renderParam.Name;
				}
				return this.m_paramDef.Name;
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x00072B01 File Offset: 0x00070D01
		public TypeCode DataType
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return (TypeCode)this.m_renderParam.DataType;
				}
				return (TypeCode)this.m_paramDef.DataType;
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x00072B22 File Offset: 0x00070D22
		public bool Nullable
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return this.m_renderParam.Nullable;
				}
				return this.m_paramDef.Nullable;
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x00072B43 File Offset: 0x00070D43
		public bool MultiValue
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return this.m_renderParam.MultiValue;
				}
				return this.m_paramDef.MultiValue;
			}
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x00072B64 File Offset: 0x00070D64
		public bool AllowBlank
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return this.m_renderParam.AllowBlank;
				}
				return this.m_paramDef.AllowBlank;
			}
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x00072B88 File Offset: 0x00070D88
		public ReportStringProperty Prompt
		{
			get
			{
				if (this.m_prompt == null)
				{
					if (this.IsOldSnapshot)
					{
						this.m_prompt = new ReportStringProperty(false, this.m_renderParam.Prompt, this.m_renderParam.Prompt);
					}
					else
					{
						this.m_prompt = new ReportStringProperty(this.m_paramDef.PromptExpression);
					}
				}
				return this.m_prompt;
			}
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06001CD9 RID: 7385 RVA: 0x00072BE5 File Offset: 0x00070DE5
		public bool UsedInQuery
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return this.m_renderParam.UsedInQuery;
				}
				return this.m_paramDef.UsedInQuery;
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x00072C06 File Offset: 0x00070E06
		public ReportParameterInstance Instance
		{
			get
			{
				if (!this.m_validInstance)
				{
					return null;
				}
				if (this.IsOldSnapshot)
				{
					return this.m_paramInstance;
				}
				if (this.m_paramInstance == null)
				{
					this.m_paramInstance = new ReportParameterInstance(this);
				}
				return this.m_paramInstance;
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x00072C3B File Offset: 0x00070E3B
		internal bool IsOldSnapshot
		{
			get
			{
				return this.m_renderParam != null;
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x00072C46 File Offset: 0x00070E46
		internal OnDemandProcessingContext OdpContext
		{
			get
			{
				return this.m_odpContext;
			}
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x00072C4E File Offset: 0x00070E4E
		internal void SetNewContext(bool validInstance)
		{
			this.m_validInstance = validInstance;
			if (this.m_paramInstance != null)
			{
				this.m_paramInstance.SetNewContext();
			}
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x00072C6A File Offset: 0x00070E6A
		internal void UpdateRenderReportItem(ReportParameter paramValue)
		{
			if (paramValue == null)
			{
				this.m_validInstance = false;
				return;
			}
			this.m_validInstance = true;
			if (this.m_paramInstance == null)
			{
				this.m_paramInstance = new ReportParameterInstance(this, paramValue);
				return;
			}
			this.m_paramInstance.UpdateRenderReportItem(paramValue);
		}

		// Token: 0x04000F1E RID: 3870
		private Microsoft.ReportingServices.ReportProcessing.ParameterDef m_renderParam;

		// Token: 0x04000F1F RID: 3871
		private Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef m_paramDef;

		// Token: 0x04000F20 RID: 3872
		private ReportParameterInstance m_paramInstance;

		// Token: 0x04000F21 RID: 3873
		private ReportStringProperty m_prompt;

		// Token: 0x04000F22 RID: 3874
		private bool m_validInstance;

		// Token: 0x04000F23 RID: 3875
		private OnDemandProcessingContext m_odpContext;
	}
}
