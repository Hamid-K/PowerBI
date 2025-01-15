using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000836 RID: 2102
	internal abstract class ReportParameterDataSetCache
	{
		// Token: 0x060075CA RID: 30154 RVA: 0x001E89C0 File Offset: 0x001E6BC0
		internal ReportParameterDataSetCache(ProcessReportParameters aParamProcessor, ParameterInfo aParameter, IParameterDef aParamDef, bool aProcessValidValues, bool aProcessDefaultValues)
		{
			this.m_paramProcessor = aParamProcessor;
			this.m_parameter = aParameter;
			this.m_parameterDef = aParamDef;
			this.m_processDefaultValues = aProcessDefaultValues;
			this.m_processValidValues = aProcessValidValues;
			if (this.m_processDefaultValues)
			{
				this.m_defaultValues = new List<object>();
			}
			if (this.m_processValidValues)
			{
				this.m_parameter.ValidValues = new ValidValueList();
			}
		}

		// Token: 0x060075CB RID: 30155 RVA: 0x001E8A24 File Offset: 0x001E6C24
		internal void NextRow(object aRow)
		{
			if (this.m_processValidValues)
			{
				IParameterDataSource validValuesDataSource = this.m_parameterDef.ValidValuesDataSource;
				object obj = null;
				string text = null;
				bool flag = false;
				try
				{
					flag = false;
					object fieldValue = this.GetFieldValue(aRow, validValuesDataSource.ValueFieldIndex);
					if (validValuesDataSource.LabelFieldIndex >= 0)
					{
						flag = true;
						obj = this.GetFieldValue(aRow, validValuesDataSource.LabelFieldIndex);
					}
					if (!Microsoft.ReportingServices.RdlExpressions.ReportRuntime.ProcessObjectToString(obj, true, out text))
					{
						this.m_paramProcessor.ProcessingContext.ErrorContext.Register(ProcessingErrorCode.rsParameterPropertyTypeMismatch, Severity.Warning, ObjectType.ReportParameter, this.m_parameterDef.Name, "Label", Array.Empty<string>());
					}
					this.m_paramProcessor.ConvertAndAddValidValue(this.m_parameter, this.m_parameterDef, fieldValue, text);
				}
				catch (ReportProcessingException_FieldError reportProcessingException_FieldError)
				{
					int num = (flag ? validValuesDataSource.LabelFieldIndex : validValuesDataSource.ValueFieldIndex);
					this.m_paramProcessor.ThrowExceptionForQueryBackedParameter(reportProcessingException_FieldError, this.m_parameterDef.Name, validValuesDataSource.DataSourceIndex, validValuesDataSource.DataSetIndex, num, "ValidValue");
				}
			}
			if (this.m_processDefaultValues)
			{
				IParameterDataSource defaultDataSource = this.m_parameterDef.DefaultDataSource;
				try
				{
					if (this.m_parameterDef.MultiValue || this.m_defaultValues.Count == 0)
					{
						object obj2 = this.GetFieldValue(aRow, defaultDataSource.ValueFieldIndex);
						obj2 = this.m_paramProcessor.ConvertValue(obj2, this.m_parameterDef, true);
						this.m_defaultValues.Add(obj2);
					}
				}
				catch (ReportProcessingException_FieldError reportProcessingException_FieldError2)
				{
					this.m_paramProcessor.ThrowExceptionForQueryBackedParameter(reportProcessingException_FieldError2, this.m_parameterDef.Name, defaultDataSource.DataSourceIndex, defaultDataSource.DataSetIndex, defaultDataSource.ValueFieldIndex, "DefaultValue");
				}
			}
		}

		// Token: 0x060075CC RID: 30156
		internal abstract object GetFieldValue(object aRow, int col);

		// Token: 0x170027A0 RID: 10144
		// (get) Token: 0x060075CD RID: 30157 RVA: 0x001E8BD4 File Offset: 0x001E6DD4
		internal List<object> DefaultValues
		{
			get
			{
				return this.m_defaultValues;
			}
		}

		// Token: 0x04003BA0 RID: 15264
		private ProcessReportParameters m_paramProcessor;

		// Token: 0x04003BA1 RID: 15265
		private ParameterInfo m_parameter;

		// Token: 0x04003BA2 RID: 15266
		private IParameterDef m_parameterDef;

		// Token: 0x04003BA3 RID: 15267
		private List<object> m_defaultValues;

		// Token: 0x04003BA4 RID: 15268
		private bool m_processValidValues;

		// Token: 0x04003BA5 RID: 15269
		private bool m_processDefaultValues;
	}
}
