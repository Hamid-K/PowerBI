using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200083A RID: 2106
	internal sealed class SharedDataSetProcessReportParameters : ProcessReportParameters
	{
		// Token: 0x060075D7 RID: 30167 RVA: 0x001E8D68 File Offset: 0x001E6F68
		internal SharedDataSetProcessReportParameters(DataSetCore dataSetCore, OnDemandProcessingContext odpContext)
			: base(odpContext)
		{
			this.m_dataSetCore = dataSetCore;
			if (odpContext.IsRdlSandboxingEnabled())
			{
				IRdlSandboxConfig rdlSandboxing = odpContext.Configuration.RdlSandboxing;
				this.m_maxStringResultLength = rdlSandboxing.MaxStringResultLength;
			}
		}

		// Token: 0x170027A1 RID: 10145
		// (get) Token: 0x060075D8 RID: 30168 RVA: 0x001E8DA3 File Offset: 0x001E6FA3
		internal override bool IsReportParameterProcessing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060075D9 RID: 30169 RVA: 0x001E8DA6 File Offset: 0x001E6FA6
		internal OnDemandProcessingContext GetOnDemandContext()
		{
			return (OnDemandProcessingContext)base.ProcessingContext;
		}

		// Token: 0x060075DA RID: 30170 RVA: 0x001E8DB3 File Offset: 0x001E6FB3
		internal override IParameterDef GetParameterDef(int aParamIndex)
		{
			return this.m_dataSetCore.Query.Parameters[aParamIndex] as DataSetParameterValue;
		}

		// Token: 0x060075DB RID: 30171 RVA: 0x001E8DD0 File Offset: 0x001E6FD0
		protected override void AssertAreSameParameterByName(ParameterInfo paramInfo, IParameterDef paramDef)
		{
			DataSetParameterValue dataSetParameterValue = (DataSetParameterValue)paramDef;
			Global.Tracer.Assert(string.Compare(paramInfo.Name, dataSetParameterValue.UniqueName, StringComparison.OrdinalIgnoreCase) == 0, "paramInfo.Name == dataSetParamDef.UniqueName, parameter {0}", new object[] { paramInfo.Name });
		}

		// Token: 0x060075DC RID: 30172 RVA: 0x001E8E17 File Offset: 0x001E7017
		protected override string ApplySandboxStringRestriction(string value, string paramName, string propertyName)
		{
			return ProcessReportParameters.ApplySandboxRestriction(ref value, paramName, propertyName, this.GetOnDemandContext(), this.m_maxStringResultLength);
		}

		// Token: 0x060075DD RID: 30173 RVA: 0x001E8E30 File Offset: 0x001E7030
		internal override void InitParametersContext(ParameterInfoCollection parameters)
		{
			OnDemandProcessingContext onDemandContext = this.GetOnDemandContext();
			Global.Tracer.Assert(onDemandContext.ReportObjectModel != null && onDemandContext.ReportRuntime != null);
			if (onDemandContext.ReportRuntime.ReportExprHost != null)
			{
				this.m_dataSetCore.SetExprHost(onDemandContext.ReportRuntime.ReportExprHost, onDemandContext.ReportObjectModel);
			}
		}

		// Token: 0x060075DE RID: 30174 RVA: 0x001E8E8B File Offset: 0x001E708B
		internal override void Cleanup()
		{
		}

		// Token: 0x060075DF RID: 30175 RVA: 0x001E8E90 File Offset: 0x001E7090
		internal override void AddToRuntime(ParameterInfo aParamInfo)
		{
			ParameterImpl parameterImpl = new ParameterImpl(aParamInfo);
			this.GetOnDemandContext().ReportObjectModel.ParametersImpl.Add(aParamInfo.Name, parameterImpl);
		}

		// Token: 0x060075E0 RID: 30176 RVA: 0x001E8EC0 File Offset: 0x001E70C0
		internal override void SetupExprHost(IParameterDef aParamDef)
		{
		}

		// Token: 0x060075E1 RID: 30177 RVA: 0x001E8EC2 File Offset: 0x001E70C2
		internal override object EvaluateDefaultValueExpr(IParameterDef aParamDef, int aIndex)
		{
			return (aParamDef as DataSetParameterValue).EvaluateQueryParameterValue(this.GetOnDemandContext(), this.m_dataSetCore.ExprHost);
		}

		// Token: 0x060075E2 RID: 30178 RVA: 0x001E8EE0 File Offset: 0x001E70E0
		internal override object EvaluateValidValueExpr(IParameterDef aParamDef, int aIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060075E3 RID: 30179 RVA: 0x001E8EE7 File Offset: 0x001E70E7
		internal override object EvaluateValidValueLabelExpr(IParameterDef aParamDef, int aIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060075E4 RID: 30180 RVA: 0x001E8EEE File Offset: 0x001E70EE
		internal override string EvaluatePromptExpr(ParameterInfo aParamInfo, IParameterDef aParamDef)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060075E5 RID: 30181 RVA: 0x001E8EF5 File Offset: 0x001E70F5
		internal override bool NeedPrompt(IParameterDataSource paramDS)
		{
			return false;
		}

		// Token: 0x060075E6 RID: 30182 RVA: 0x001E8EF8 File Offset: 0x001E70F8
		internal override void ThrowExceptionForQueryBackedParameter(ReportProcessingException_FieldError aError, string aParamName, int aDataSourceIndex, int aDataSetIndex, int aFieldIndex, string propertyName)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060075E7 RID: 30183 RVA: 0x001E8EFF File Offset: 0x001E70FF
		internal override ReportParameterDataSetCache ProcessReportParameterDataSet(ParameterInfo aParam, IParameterDef aParamDef, IParameterDataSource paramDS, bool aRetrieveValidValues, bool aRetrievalDefaultValues)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04003BA7 RID: 15271
		private DataSetCore m_dataSetCore;
	}
}
