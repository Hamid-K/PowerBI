using System;
using System.Diagnostics;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000835 RID: 2101
	internal sealed class LegacyProcessReportParameters : ProcessReportParameters
	{
		// Token: 0x060075BA RID: 30138 RVA: 0x001E84BE File Offset: 0x001E66BE
		internal LegacyProcessReportParameters(Report aReport, ReportProcessing.ReportProcessingContext aContext)
			: base(aContext)
		{
			this.m_report = aReport;
		}

		// Token: 0x060075BB RID: 30139 RVA: 0x001E84CE File Offset: 0x001E66CE
		internal ReportProcessing.ReportProcessingContext GetLegacyContext()
		{
			return (ReportProcessing.ReportProcessingContext)base.ProcessingContext;
		}

		// Token: 0x060075BC RID: 30140 RVA: 0x001E84DB File Offset: 0x001E66DB
		internal override IParameterDef GetParameterDef(int aParamIndex)
		{
			return this.m_report.Parameters[aParamIndex];
		}

		// Token: 0x060075BD RID: 30141 RVA: 0x001E84F0 File Offset: 0x001E66F0
		internal override void InitParametersContext(ParameterInfoCollection parameters)
		{
			int dataSourceCount = this.m_report.DataSourceCount;
			ReportProcessing.ReportProcessingContext legacyContext = this.GetLegacyContext();
			Global.Tracer.Assert(legacyContext.ReportObjectModel == null, "(null == processingContext.ReportObjectModel)");
			legacyContext.ReportObjectModel = new ObjectModelImpl(legacyContext);
			legacyContext.ReportObjectModel.ParametersImpl = new ParametersImpl(parameters.Count);
			legacyContext.ReportObjectModel.FieldsImpl = new FieldsImpl();
			legacyContext.ReportObjectModel.ReportItemsImpl = new ReportItemsImpl();
			legacyContext.ReportObjectModel.AggregatesImpl = new AggregatesImpl(null);
			legacyContext.ReportObjectModel.GlobalsImpl = new GlobalsImpl(legacyContext.ReportContext.ItemName, legacyContext.ExecutionTime, legacyContext.ReportContext.HostRootUri, legacyContext.ReportContext.ParentPath);
			legacyContext.ReportObjectModel.UserImpl = new UserImpl(legacyContext.RequestUserName, legacyContext.UserLanguage.Name, legacyContext.AllowUserProfileState);
			legacyContext.ReportObjectModel.DataSetsImpl = new DataSetsImpl();
			legacyContext.ReportObjectModel.DataSourcesImpl = new DataSourcesImpl(dataSourceCount);
			if (legacyContext.ReportRuntime == null)
			{
				legacyContext.ReportRuntime = new ReportRuntime(legacyContext.ReportObjectModel, legacyContext.ErrorContext);
				legacyContext.ReportRuntime.LoadCompiledCode(this.m_report, true, legacyContext.ReportObjectModel, legacyContext.ReportRuntimeSetup);
			}
		}

		// Token: 0x060075BE RID: 30142 RVA: 0x001E8638 File Offset: 0x001E6838
		internal override void Cleanup()
		{
			ReportProcessing.ReportProcessingContext legacyContext = this.GetLegacyContext();
			if (legacyContext.ReportRuntime != null)
			{
				legacyContext.ReportRuntime.Close();
			}
		}

		// Token: 0x060075BF RID: 30143 RVA: 0x001E8660 File Offset: 0x001E6860
		internal override void AddToRuntime(ParameterInfo aParamInfo)
		{
			ParameterImpl parameterImpl = new ParameterImpl(aParamInfo.Values, aParamInfo.Labels, aParamInfo.MultiValue);
			this.GetLegacyContext().ReportObjectModel.ParametersImpl.Add(aParamInfo.Name, parameterImpl);
		}

		// Token: 0x060075C0 RID: 30144 RVA: 0x001E86A4 File Offset: 0x001E68A4
		internal override void SetupExprHost(IParameterDef aParamDef)
		{
			ReportProcessing.ReportProcessingContext legacyContext = this.GetLegacyContext();
			if (legacyContext.ReportRuntime.ReportExprHost != null)
			{
				((ParameterDef)aParamDef).SetExprHost(legacyContext.ReportRuntime.ReportExprHost, legacyContext.ReportObjectModel);
			}
		}

		// Token: 0x060075C1 RID: 30145 RVA: 0x001E86E1 File Offset: 0x001E68E1
		internal override string EvaluatePromptExpr(ParameterInfo aParamInfo, IParameterDef aParamDef)
		{
			Global.Tracer.Assert(false);
			return null;
		}

		// Token: 0x060075C2 RID: 30146 RVA: 0x001E86F0 File Offset: 0x001E68F0
		internal override object EvaluateDefaultValueExpr(IParameterDef aParamDef, int aIndex)
		{
			VariantResult variantResult = this.GetLegacyContext().ReportRuntime.EvaluateParamDefaultValue((ParameterDef)aParamDef, aIndex);
			if (variantResult.ErrorOccurred)
			{
				throw new ReportProcessingException(ErrorCode.rsReportParameterProcessingError, new object[] { aParamDef.Name });
			}
			return variantResult.Value;
		}

		// Token: 0x060075C3 RID: 30147 RVA: 0x001E873C File Offset: 0x001E693C
		internal override object EvaluateValidValueExpr(IParameterDef aParamDef, int aIndex)
		{
			VariantResult variantResult = this.GetLegacyContext().ReportRuntime.EvaluateParamValidValue((ParameterDef)aParamDef, aIndex);
			if (variantResult.ErrorOccurred)
			{
				throw new ReportProcessingException(ErrorCode.rsReportParameterProcessingError, new object[] { aParamDef.Name });
			}
			return variantResult.Value;
		}

		// Token: 0x060075C4 RID: 30148 RVA: 0x001E8788 File Offset: 0x001E6988
		internal override object EvaluateValidValueLabelExpr(IParameterDef aParamDef, int aIndex)
		{
			VariantResult variantResult = this.GetLegacyContext().ReportRuntime.EvaluateParamValidValueLabel((ParameterDef)aParamDef, aIndex);
			if (variantResult.ErrorOccurred)
			{
				throw new ReportProcessingException(ErrorCode.rsReportParameterProcessingError, new object[] { aParamDef.Name });
			}
			return variantResult.Value;
		}

		// Token: 0x060075C5 RID: 30149 RVA: 0x001E87D4 File Offset: 0x001E69D4
		internal override bool NeedPrompt(IParameterDataSource paramDS)
		{
			bool flag = false;
			Microsoft.ReportingServices.ReportProcessing.DataSource dataSource = this.m_report.DataSources[paramDS.DataSourceIndex];
			if (this.GetLegacyContext().DataSourceInfos != null)
			{
				DataSourceInfo byID = this.GetLegacyContext().DataSourceInfos.GetByID(dataSource.ID);
				if (byID != null)
				{
					flag = byID.NeedPrompt;
				}
			}
			return flag;
		}

		// Token: 0x060075C6 RID: 30150 RVA: 0x001E882C File Offset: 0x001E6A2C
		internal override void ThrowExceptionForQueryBackedParameter(ReportProcessingException_FieldError aError, string aParamName, int aDataSourceIndex, int aDataSetIndex, int aFieldIndex, string propertyName)
		{
			Microsoft.ReportingServices.ReportProcessing.DataSet dataSet = this.m_report.DataSources[aDataSourceIndex].DataSets[aDataSetIndex];
			throw new ReportProcessingException(ErrorCode.rsReportParameterQueryProcessingError, new object[]
			{
				aParamName,
				propertyName,
				dataSet.Fields[aFieldIndex].Name,
				dataSet.Name,
				ReportRuntime.GetErrorName(aError.Status, aError.Message)
			});
		}

		// Token: 0x060075C7 RID: 30151 RVA: 0x001E88A0 File Offset: 0x001E6AA0
		internal override ReportParameterDataSetCache ProcessReportParameterDataSet(ParameterInfo aParam, IParameterDef aParamDef, IParameterDataSource paramDS, bool aRetrieveValidValues, bool aRetrievalDefaultValues)
		{
			EventHandler eventHandler = null;
			LegacyReportParameterDataSetCache legacyReportParameterDataSetCache = new LegacyReportParameterDataSetCache(this, aParam, (ParameterDef)aParamDef, aRetrieveValidValues, aRetrievalDefaultValues);
			ReportProcessing.ReportProcessingContext legacyContext = this.GetLegacyContext();
			try
			{
				this.m_runtimeDataSourceNode = new ReportProcessing.ReportRuntimeDataSourceNode(this.m_report, this.m_report.DataSources[paramDS.DataSourceIndex], paramDS.DataSetIndex, legacyContext, legacyReportParameterDataSetCache);
				eventHandler = new EventHandler(this.AbortHandler);
				legacyContext.AbortInfo.ProcessingAbortEvent += eventHandler;
				if (Global.Tracer.TraceVerbose)
				{
					Global.Tracer.Trace(TraceLevel.Verbose, "Abort handler registered.");
				}
				this.m_runtimeDataSourceNode.InitProcessingParams(false, true);
				this.m_runtimeDataSourceNode.ProcessConcurrent(null);
				legacyContext.CheckAndThrowIfAborted();
				ReportProcessing.RuntimeDataSetNode runtimeDataSetNode = this.m_runtimeDataSourceNode.RuntimeDataSetNodes[0];
			}
			finally
			{
				if (eventHandler != null)
				{
					legacyContext.AbortInfo.ProcessingAbortEvent -= eventHandler;
				}
				if (this.m_runtimeDataSourceNode != null)
				{
					this.m_runtimeDataSourceNode.Cleanup();
				}
			}
			return legacyReportParameterDataSetCache;
		}

		// Token: 0x060075C8 RID: 30152 RVA: 0x001E8994 File Offset: 0x001E6B94
		private void AbortHandler(object sender, EventArgs e)
		{
			if (Global.Tracer.TraceInfo)
			{
				Global.Tracer.Trace(TraceLevel.Info, "Merge abort handler called. Aborting data sources ...");
			}
			this.m_runtimeDataSourceNode.Abort();
		}

		// Token: 0x060075C9 RID: 30153 RVA: 0x001E89BD File Offset: 0x001E6BBD
		protected override string ApplySandboxStringRestriction(string value, string paramName, string propertyName)
		{
			return value;
		}

		// Token: 0x04003B9E RID: 15262
		private Report m_report;

		// Token: 0x04003B9F RID: 15263
		private ReportProcessing.RuntimeDataSourceNode m_runtimeDataSourceNode;
	}
}
