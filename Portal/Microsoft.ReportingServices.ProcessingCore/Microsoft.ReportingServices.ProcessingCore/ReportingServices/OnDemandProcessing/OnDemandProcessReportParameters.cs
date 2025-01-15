using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000834 RID: 2100
	internal sealed class OnDemandProcessReportParameters : ProcessReportParameters
	{
		// Token: 0x060075AB RID: 30123 RVA: 0x001E8178 File Offset: 0x001E6378
		internal OnDemandProcessReportParameters(Microsoft.ReportingServices.ReportIntermediateFormat.Report aReport, OnDemandProcessingContext aContext)
			: base(aContext)
		{
			this.m_report = aReport;
			if (aContext.IsRdlSandboxingEnabled())
			{
				IRdlSandboxConfig rdlSandboxing = aContext.Configuration.RdlSandboxing;
				this.m_maxStringResultLength = rdlSandboxing.MaxStringResultLength;
			}
		}

		// Token: 0x060075AC RID: 30124 RVA: 0x001E81B3 File Offset: 0x001E63B3
		internal OnDemandProcessingContext GetOnDemandContext()
		{
			return (OnDemandProcessingContext)base.ProcessingContext;
		}

		// Token: 0x060075AD RID: 30125 RVA: 0x001E81C0 File Offset: 0x001E63C0
		internal override IParameterDef GetParameterDef(int aParamIndex)
		{
			Global.Tracer.Assert(aParamIndex < this.m_report.Parameters.Count, "Invalid Parameter Index.  Found: {0}.  Count: {1}", new object[]
			{
				aParamIndex,
				this.m_report.Parameters.Count
			});
			return this.m_report.Parameters[aParamIndex];
		}

		// Token: 0x060075AE RID: 30126 RVA: 0x001E8227 File Offset: 0x001E6427
		internal override void InitParametersContext(ParameterInfoCollection parameters)
		{
		}

		// Token: 0x060075AF RID: 30127 RVA: 0x001E8229 File Offset: 0x001E6429
		internal override void Cleanup()
		{
		}

		// Token: 0x060075B0 RID: 30128 RVA: 0x001E822C File Offset: 0x001E642C
		internal override void AddToRuntime(ParameterInfo aParamInfo)
		{
			ParameterImpl parameterImpl = new ParameterImpl(aParamInfo);
			this.GetOnDemandContext().ReportObjectModel.ParametersImpl.Add(aParamInfo.Name, parameterImpl);
		}

		// Token: 0x060075B1 RID: 30129 RVA: 0x001E825C File Offset: 0x001E645C
		internal override void SetupExprHost(IParameterDef aParamDef)
		{
			OnDemandProcessingContext onDemandContext = this.GetOnDemandContext();
			if (onDemandContext.ReportRuntime.ReportExprHost != null)
			{
				((Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef)aParamDef).SetExprHost(onDemandContext.ReportRuntime.ReportExprHost, onDemandContext.ReportObjectModel);
			}
		}

		// Token: 0x060075B2 RID: 30130 RVA: 0x001E829C File Offset: 0x001E649C
		internal override object EvaluateDefaultValueExpr(IParameterDef aParamDef, int aIndex)
		{
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = this.GetOnDemandContext().ReportRuntime.EvaluateParamDefaultValue((Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef)aParamDef, aIndex);
			if (variantResult.ErrorOccurred)
			{
				throw new ReportProcessingException(ErrorCode.rsReportParameterProcessingError, new object[] { aParamDef.Name });
			}
			return variantResult.Value;
		}

		// Token: 0x060075B3 RID: 30131 RVA: 0x001E82E8 File Offset: 0x001E64E8
		internal override object EvaluateValidValueExpr(IParameterDef aParamDef, int aIndex)
		{
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = this.GetOnDemandContext().ReportRuntime.EvaluateParamValidValue((Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef)aParamDef, aIndex);
			if (variantResult.ErrorOccurred)
			{
				throw new ReportProcessingException(ErrorCode.rsReportParameterProcessingError, new object[] { aParamDef.Name });
			}
			return variantResult.Value;
		}

		// Token: 0x060075B4 RID: 30132 RVA: 0x001E8334 File Offset: 0x001E6534
		internal override object EvaluateValidValueLabelExpr(IParameterDef aParamDef, int aIndex)
		{
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = this.GetOnDemandContext().ReportRuntime.EvaluateParamValidValueLabel((Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef)aParamDef, aIndex);
			if (variantResult.ErrorOccurred)
			{
				throw new ReportProcessingException(ErrorCode.rsReportParameterProcessingError, new object[] { aParamDef.Name });
			}
			return variantResult.Value;
		}

		// Token: 0x060075B5 RID: 30133 RVA: 0x001E837E File Offset: 0x001E657E
		internal override string EvaluatePromptExpr(ParameterInfo aParamInfo, IParameterDef aParamDef)
		{
			return this.GetOnDemandContext().ReportRuntime.EvaluateParamPrompt((Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef)aParamDef);
		}

		// Token: 0x060075B6 RID: 30134 RVA: 0x001E8398 File Offset: 0x001E6598
		internal override bool NeedPrompt(IParameterDataSource paramDS)
		{
			bool flag = false;
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource = this.m_report.DataSources[paramDS.DataSourceIndex];
			if (this.GetOnDemandContext().DataSourceInfos != null)
			{
				DataSourceInfo byID = this.GetOnDemandContext().DataSourceInfos.GetByID(dataSource.ID);
				if (byID != null)
				{
					flag = byID.NeedPrompt;
				}
			}
			return flag;
		}

		// Token: 0x060075B7 RID: 30135 RVA: 0x001E83F0 File Offset: 0x001E65F0
		internal override void ThrowExceptionForQueryBackedParameter(ReportProcessingException_FieldError aError, string aParamName, int aDataSourceIndex, int aDataSetIndex, int aFieldIndex, string propertyName)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = this.m_report.DataSources[aDataSourceIndex].DataSets[aDataSetIndex];
			throw new ReportProcessingException(ErrorCode.rsReportParameterQueryProcessingError, new object[]
			{
				aParamName,
				propertyName,
				dataSet.Fields[aFieldIndex].Name,
				dataSet.Name,
				Microsoft.ReportingServices.ReportProcessing.ReportRuntime.GetErrorName(aError.Status, aError.Message)
			});
		}

		// Token: 0x060075B8 RID: 30136 RVA: 0x001E8464 File Offset: 0x001E6664
		internal override ReportParameterDataSetCache ProcessReportParameterDataSet(ParameterInfo aParam, IParameterDef aParamDef, IParameterDataSource paramDS, bool aRetrieveValidValues, bool aRetrievalDefaultValues)
		{
			ReportParameterDataSetCache reportParameterDataSetCache = new OnDemandReportParameterDataSetCache(this, aParam, (Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef)aParamDef, aRetrieveValidValues, aRetrievalDefaultValues);
			new RetrievalManager(this.m_report, this.GetOnDemandContext()).FetchParameterData(reportParameterDataSetCache, paramDS.DataSourceIndex, paramDS.DataSetIndex);
			return reportParameterDataSetCache;
		}

		// Token: 0x060075B9 RID: 30137 RVA: 0x001E84A7 File Offset: 0x001E66A7
		protected override string ApplySandboxStringRestriction(string value, string paramName, string propertyName)
		{
			return ProcessReportParameters.ApplySandboxRestriction(ref value, paramName, propertyName, this.GetOnDemandContext(), this.m_maxStringResultLength);
		}

		// Token: 0x04003B9D RID: 15261
		private Microsoft.ReportingServices.ReportIntermediateFormat.Report m_report;
	}
}
