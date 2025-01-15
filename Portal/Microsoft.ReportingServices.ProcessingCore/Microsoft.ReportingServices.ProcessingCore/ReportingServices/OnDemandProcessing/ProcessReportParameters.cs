using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000833 RID: 2099
	internal abstract class ProcessReportParameters
	{
		// Token: 0x06007590 RID: 30096 RVA: 0x001E753C File Offset: 0x001E573C
		public ProcessReportParameters(IInternalProcessingContext aContext)
		{
			this.m_processingContext = aContext;
		}

		// Token: 0x1700279E RID: 10142
		// (get) Token: 0x06007591 RID: 30097 RVA: 0x001E7552 File Offset: 0x001E5752
		internal virtual bool IsReportParameterProcessing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007592 RID: 30098 RVA: 0x001E7558 File Offset: 0x001E5758
		private void ProcessParameter(ParameterInfoCollection aParameters, int aParamIndex)
		{
			ParameterInfo parameterInfo = aParameters[aParamIndex];
			parameterInfo.MissingUpstreamDataSourcePrompt = false;
			IParameterDef parameterDef = null;
			bool flag = aParameters.UserProfileState > UserProfileState.None;
			if (this.m_processingContext.SnapshotProcessing && parameterInfo.UsedInQuery)
			{
				parameterInfo.State = ReportParameterState.HasValidValue;
				parameterInfo.StoreLabels();
				return;
			}
			if (parameterInfo.DynamicDefaultValue || parameterInfo.DynamicValidValues || parameterInfo.DynamicPrompt)
			{
				this.UpdateParametersContext(aParameters, this.m_lastDynamicParam, aParamIndex);
				this.m_lastDynamicParam = aParamIndex;
				parameterDef = this.GetParameterDef(aParamIndex);
				Global.Tracer.Assert(parameterDef != null, "null != paramDef, parameter {0}", new object[] { parameterInfo.Name });
				Global.Tracer.Assert(parameterInfo.DataType == parameterDef.DataType, "paramInfo.DataType == paramDef.DataType, parameter {0}", new object[] { parameterInfo.Name });
				this.AssertAreSameParameterByName(parameterInfo, parameterDef);
			}
			bool flag2 = this.m_dependenciesSubmitted.ContainsKey(parameterInfo.Name);
			if (parameterInfo.DynamicPrompt && (flag2 || !parameterInfo.IsUserSupplied || flag))
			{
				this.SetupExprHost(parameterDef);
				string text = this.EvaluatePromptExpr(parameterInfo, parameterDef);
				if (text == null || text.Equals(string.Empty))
				{
					text = parameterInfo.Name;
				}
				parameterInfo.Prompt = text;
			}
			switch (parameterInfo.CalculateDependencyStatus())
			{
			case ReportParameterDependencyState.AllDependenciesSpecified:
				break;
			case ReportParameterDependencyState.HasOutstandingDependencies:
				parameterInfo.State = ReportParameterState.HasOutstandingDependencies;
				parameterInfo.Values = null;
				if (parameterInfo.DynamicDefaultValue)
				{
					parameterInfo.DefaultValues = null;
				}
				if (parameterInfo.DynamicValidValues)
				{
					parameterInfo.ValidValues = null;
				}
				return;
			case ReportParameterDependencyState.MissingUpstreamDataSourcePrompt:
				parameterInfo.MissingUpstreamDataSourcePrompt = true;
				parameterInfo.State = ReportParameterState.DynamicValuesUnavailable;
				return;
			default:
				Global.Tracer.Assert(false, "Unexpected dependency state.");
				break;
			}
			bool flag3 = parameterInfo.DynamicDefaultValue && (parameterInfo.Values == null || (parameterInfo.Values != null && !parameterInfo.IsUserSupplied)) && ((this.m_processingContext.SnapshotProcessing && parameterDef.HasDefaultValuesExpressions() && (flag || (parameterInfo.DependencyList != null && (parameterInfo.Values == null || (!parameterInfo.IsUserSupplied && flag2))))) || (!this.m_processingContext.SnapshotProcessing && (flag2 || parameterInfo.Values == null)));
			if (parameterInfo.DynamicValidValues && ((this.m_processingContext.SnapshotProcessing && parameterDef.HasValidValuesValueExpressions() && (parameterInfo.DependencyList != null || (flag && flag3))) || (!this.m_processingContext.SnapshotProcessing && ((parameterInfo.ValidValues != null && flag2) || parameterInfo.ValidValues == null))) && !this.ProcessValidValues(parameterInfo, parameterDef, flag3))
			{
				parameterInfo.State = ReportParameterState.DynamicValuesUnavailable;
				return;
			}
			if (!flag3 && parameterInfo.Values != null)
			{
				if (parameterInfo.ValueIsValid())
				{
					parameterInfo.State = ReportParameterState.HasValidValue;
					parameterInfo.StoreLabels();
					return;
				}
				parameterInfo.State = ReportParameterState.InvalidValueProvided;
				parameterInfo.Values = null;
				parameterInfo.EnsureLabelsAreGenerated();
				return;
			}
			else
			{
				parameterInfo.Values = null;
				parameterInfo.State = ReportParameterState.MissingValidValue;
				if (flag3 && !this.ProcessDefaultValue(parameterInfo, parameterDef))
				{
					parameterInfo.State = ReportParameterState.DynamicValuesUnavailable;
					return;
				}
				if (parameterInfo.DefaultValues != null)
				{
					parameterInfo.Values = parameterInfo.DefaultValues;
					if (!parameterInfo.ValueIsValid())
					{
						parameterInfo.Values = null;
						parameterInfo.State = ReportParameterState.DefaultValueInvalid;
						parameterInfo.EnsureLabelsAreGenerated();
					}
					else
					{
						parameterInfo.State = ReportParameterState.HasValidValue;
						parameterInfo.StoreLabels();
					}
				}
				this.m_paramDataSetCache = null;
				return;
			}
		}

		// Token: 0x06007593 RID: 30099 RVA: 0x001E7887 File Offset: 0x001E5A87
		protected virtual void AssertAreSameParameterByName(ParameterInfo paramInfo, IParameterDef paramDef)
		{
			Global.Tracer.Assert(string.Compare(paramInfo.Name, paramDef.Name, StringComparison.OrdinalIgnoreCase) == 0, "paramInfo.Name == paramDef.Name, parameter {0}", new object[] { paramInfo.Name });
		}

		// Token: 0x06007594 RID: 30100 RVA: 0x001E78BC File Offset: 0x001E5ABC
		public ProcessingMessageList Process(ParameterInfoCollection aParameters)
		{
			this.m_parameters = aParameters;
			ProcessingMessageList messages;
			try
			{
				if (this.m_parameters.IsAnyParameterDynamic)
				{
					this.InitParametersContext(this.m_parameters);
				}
				this.m_dependenciesSubmitted = ProcessReportParameters.BuildSubmittedDependencyList(this.m_parameters);
				for (int i = 0; i < aParameters.Count; i++)
				{
					this.ProcessParameter(this.m_parameters, i);
				}
				this.m_parameters.Validated = true;
				messages = this.m_processingContext.ErrorContext.Messages;
			}
			finally
			{
				this.Cleanup();
			}
			return messages;
		}

		// Token: 0x06007595 RID: 30101 RVA: 0x001E7950 File Offset: 0x001E5B50
		internal static Dictionary<string, bool> BuildSubmittedDependencyList(ParameterInfoCollection parameters)
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			for (int i = 0; i < parameters.Count; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				if (parameterInfo.DependencyList != null)
				{
					for (int j = 0; j < parameterInfo.DependencyList.Count; j++)
					{
						ParameterInfo parameterInfo2 = parameterInfo.DependencyList[j];
						if ((parameterInfo2.IsUserSupplied && parameterInfo2.ValuesChanged) || dictionary.ContainsKey(parameterInfo2.Name))
						{
							dictionary.Add(parameterInfo.Name, true);
							break;
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06007596 RID: 30102
		internal abstract IParameterDef GetParameterDef(int aParamIndex);

		// Token: 0x06007597 RID: 30103
		internal abstract void InitParametersContext(ParameterInfoCollection parameters);

		// Token: 0x06007598 RID: 30104
		internal abstract void Cleanup();

		// Token: 0x06007599 RID: 30105
		internal abstract void AddToRuntime(ParameterInfo aParamInfo);

		// Token: 0x0600759A RID: 30106
		internal abstract void SetupExprHost(IParameterDef aParamDef);

		// Token: 0x0600759B RID: 30107
		internal abstract object EvaluateDefaultValueExpr(IParameterDef aParamDef, int aIndex);

		// Token: 0x0600759C RID: 30108
		internal abstract object EvaluateValidValueExpr(IParameterDef aParamDef, int aIndex);

		// Token: 0x0600759D RID: 30109
		internal abstract object EvaluateValidValueLabelExpr(IParameterDef aParamDef, int aIndex);

		// Token: 0x0600759E RID: 30110
		internal abstract bool NeedPrompt(IParameterDataSource paramDS);

		// Token: 0x0600759F RID: 30111
		internal abstract void ThrowExceptionForQueryBackedParameter(ReportProcessingException_FieldError aError, string aParamName, int aDataSourceIndex, int aDataSetIndex, int aFieldIndex, string propertyName);

		// Token: 0x060075A0 RID: 30112
		internal abstract string EvaluatePromptExpr(ParameterInfo aParamInfo, IParameterDef aParamDef);

		// Token: 0x060075A1 RID: 30113
		internal abstract ReportParameterDataSetCache ProcessReportParameterDataSet(ParameterInfo aParam, IParameterDef aParamDef, IParameterDataSource paramDS, bool aRetrieveValidValues, bool aRetrievalDefaultValues);

		// Token: 0x060075A2 RID: 30114
		protected abstract string ApplySandboxStringRestriction(string value, string paramName, string propertyName);

		// Token: 0x060075A3 RID: 30115 RVA: 0x001E79D9 File Offset: 0x001E5BD9
		internal bool ValidateValue(object newValue, IParameterDef paramDef, string parameterValueProperty)
		{
			return paramDef.ValidateValueForNull(newValue, this.m_processingContext.ErrorContext, parameterValueProperty) && paramDef.ValidateValueForBlank(newValue, this.m_processingContext.ErrorContext, parameterValueProperty);
		}

		// Token: 0x060075A4 RID: 30116 RVA: 0x001E7A08 File Offset: 0x001E5C08
		internal object ConvertValue(object o, IParameterDef paramDef, bool isDefaultValue)
		{
			if (o == null || DBNull.Value == o)
			{
				return null;
			}
			bool flag = false;
			object obj = null;
			try
			{
				DataType dataType = paramDef.DataType;
				if (dataType <= DataType.Integer)
				{
					if (dataType != DataType.Object)
					{
						if (dataType != DataType.Boolean)
						{
							if (dataType == DataType.Integer)
							{
								obj = Convert.ToInt32(o, Thread.CurrentThread.CurrentCulture);
							}
						}
						else
						{
							obj = (bool)o;
						}
					}
					else
					{
						obj = o;
					}
				}
				else if (dataType != DataType.Float)
				{
					if (dataType != DataType.DateTime)
					{
						if (dataType == DataType.String)
						{
							obj = Convert.ToString(o, Thread.CurrentThread.CurrentCulture);
							obj = this.ApplySandboxStringRestriction((string)obj, paramDef.Name, isDefaultValue ? "DefaultValue" : "ValidValue");
						}
					}
					else if (o is DateTimeOffset)
					{
						obj = (DateTimeOffset)o;
					}
					else
					{
						obj = (DateTime)o;
					}
				}
				else
				{
					obj = Convert.ToDouble(o, Thread.CurrentThread.CurrentCulture);
				}
			}
			catch (InvalidCastException)
			{
				flag = true;
			}
			catch (OverflowException)
			{
				flag = true;
			}
			catch (FormatException)
			{
				flag = true;
			}
			finally
			{
				if (flag)
				{
					string text;
					if (isDefaultValue)
					{
						text = "DefaultValue";
					}
					else
					{
						text = "ValidValues";
					}
					this.m_processingContext.ErrorContext.Register(ProcessingErrorCode.rsParameterPropertyTypeMismatch, Severity.Error, paramDef.ParameterObjectType, paramDef.Name, text, Array.Empty<string>());
					throw new ReportProcessingException(this.m_processingContext.ErrorContext.Messages);
				}
			}
			return obj;
		}

		// Token: 0x060075A5 RID: 30117 RVA: 0x001E7B94 File Offset: 0x001E5D94
		internal void UpdateParametersContext(ParameterInfoCollection parameters, int lastIndex, int currentIndex)
		{
			for (int i = lastIndex; i < currentIndex; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				this.AddToRuntime(parameterInfo);
			}
		}

		// Token: 0x060075A6 RID: 30118 RVA: 0x001E7BBC File Offset: 0x001E5DBC
		internal bool ProcessDefaultValue(ParameterInfo parameter, IParameterDef paramDef)
		{
			if (parameter == null || paramDef == null)
			{
				return true;
			}
			if (paramDef.HasDefaultValuesExpressions())
			{
				int num = paramDef.DefaultValuesExpressionCount;
				Global.Tracer.Assert(num != 0, "(0 != count)");
				if (!paramDef.MultiValue)
				{
					num = 1;
				}
				this.SetupExprHost(paramDef);
				ArrayList arrayList = new ArrayList(num);
				for (int i = 0; i < num; i++)
				{
					object obj = this.EvaluateDefaultValueExpr(paramDef, i);
					if (obj is object[])
					{
						foreach (object obj2 in obj as object[])
						{
							object obj3 = this.ConvertValue(obj2, paramDef, true);
							if (!this.ValidateValue(obj3, paramDef, "DefaultValue"))
							{
								return true;
							}
							arrayList.Add(obj3);
						}
					}
					else
					{
						obj = this.ConvertValue(obj, paramDef, true);
						if (!this.ValidateValue(obj, paramDef, "DefaultValue"))
						{
							return true;
						}
						arrayList.Add(obj);
					}
				}
				Global.Tracer.Assert(arrayList != null, "(null != defaultValues)");
				if (paramDef.MultiValue)
				{
					parameter.DefaultValues = new object[arrayList.Count];
					arrayList.CopyTo(parameter.DefaultValues);
				}
				else if (arrayList.Count > 0)
				{
					parameter.DefaultValues = new object[1];
					parameter.DefaultValues[0] = arrayList[0];
				}
				else
				{
					parameter.DefaultValues = new object[0];
				}
			}
			else if (paramDef.HasDefaultValuesDataSource() && this.m_processingContext.EnableDataBackedParameters)
			{
				IParameterDataSource defaultDataSource = paramDef.DefaultDataSource;
				IParameterDataSource validValuesDataSource = paramDef.ValidValuesDataSource;
				List<object> list;
				if (this.m_paramDataSetCache != null && validValuesDataSource != null && defaultDataSource.DataSourceIndex == validValuesDataSource.DataSourceIndex && defaultDataSource.DataSetIndex == validValuesDataSource.DataSetIndex)
				{
					list = this.m_paramDataSetCache.DefaultValues;
				}
				else
				{
					if (this.NeedPrompt(defaultDataSource))
					{
						parameter.MissingUpstreamDataSourcePrompt = true;
						return false;
					}
					list = this.ProcessReportParameterDataSet(parameter, paramDef, defaultDataSource, false, true).DefaultValues;
					if (Global.Tracer.TraceVerbose && (list == null || list.Count == 0))
					{
						Global.Tracer.Trace(TraceLevel.Verbose, "Parameter '{0}' default value list does not contain any values.", new object[] { parameter.Name });
					}
				}
				if (list != null)
				{
					int count = list.Count;
					parameter.DefaultValues = new object[count];
					for (int k = 0; k < count; k++)
					{
						object obj = list[k];
						if (!this.ValidateValue(obj, paramDef, "DefaultValue"))
						{
							if (Global.Tracer.TraceVerbose)
							{
								Global.Tracer.Trace(TraceLevel.Verbose, "Parameter '{0}' has a default value '{1}' which is not a valid value.", new object[]
								{
									parameter.Name,
									(obj == null) ? "null" : obj.ToString()
								});
							}
							parameter.DefaultValues = null;
							return true;
						}
						parameter.DefaultValues[k] = obj;
					}
				}
			}
			return true;
		}

		// Token: 0x060075A7 RID: 30119 RVA: 0x001E7E80 File Offset: 0x001E6080
		internal bool ProcessValidValues(ParameterInfo parameter, IParameterDef paramDef, bool aEvaluateDefaultValues)
		{
			if (parameter == null || paramDef == null)
			{
				return true;
			}
			IParameterDataSource validValuesDataSource = paramDef.ValidValuesDataSource;
			if (paramDef.HasValidValuesDataSource())
			{
				if (this.m_processingContext.EnableDataBackedParameters)
				{
					if (this.NeedPrompt(validValuesDataSource))
					{
						parameter.MissingUpstreamDataSourcePrompt = true;
						return false;
					}
					IParameterDataSource defaultDataSource = paramDef.DefaultDataSource;
					bool flag = aEvaluateDefaultValues && defaultDataSource != null && defaultDataSource.DataSourceIndex == validValuesDataSource.DataSourceIndex && defaultDataSource.DataSetIndex == validValuesDataSource.DataSetIndex;
					this.m_paramDataSetCache = this.ProcessReportParameterDataSet(parameter, paramDef, validValuesDataSource, true, flag);
					if (Global.Tracer.TraceVerbose && parameter.ValidValues != null && parameter.ValidValues.Count == 0)
					{
						Global.Tracer.Trace(TraceLevel.Verbose, "Parameter '{0}' dynamic valid value list does not contain any values.", new object[] { parameter.Name });
					}
				}
			}
			else if (paramDef.HasValidValuesValueExpressions())
			{
				int validValuesValueExpressionCount = paramDef.ValidValuesValueExpressionCount;
				Global.Tracer.Assert(validValuesValueExpressionCount != 0, "(0 != count)");
				Global.Tracer.Assert(paramDef.HasValidValuesLabelExpressions() && validValuesValueExpressionCount == paramDef.ValidValuesLabelExpressionCount);
				this.SetupExprHost(paramDef);
				parameter.ValidValues = new ValidValueList(validValuesValueExpressionCount);
				for (int i = 0; i < validValuesValueExpressionCount; i++)
				{
					object obj = this.EvaluateValidValueExpr(paramDef, i);
					object obj2 = this.EvaluateValidValueLabelExpr(paramDef, i);
					bool flag2 = obj is object[];
					bool flag3 = obj2 is object[];
					if (flag2 && (flag3 || obj2 == null))
					{
						object[] array = obj as object[];
						object[] array2 = obj2 as object[];
						if (array2 != null && array.Length != array2.Length)
						{
							this.m_processingContext.ErrorContext.Register(ProcessingErrorCode.rsInvalidValidValueList, Severity.Error, ObjectType.ReportParameter, paramDef.Name, "ValidValues", Array.Empty<string>());
							throw new ReportProcessingException(this.m_processingContext.ErrorContext.Messages);
						}
						int num = array.Length;
						for (int j = 0; j < num; j++)
						{
							obj2 = ((array2 == null) ? null : array2[j]);
							this.ConvertAndAddValidValue(parameter, paramDef, array[j], obj2);
						}
					}
					else
					{
						if (flag2 || (flag3 && obj2 != null))
						{
							this.m_processingContext.ErrorContext.Register(ProcessingErrorCode.rsInvalidValidValueList, Severity.Error, ObjectType.ReportParameter, paramDef.Name, "ValidValues", Array.Empty<string>());
							throw new ReportProcessingException(this.m_processingContext.ErrorContext.Messages);
						}
						this.ConvertAndAddValidValue(parameter, paramDef, obj, obj2);
					}
				}
			}
			return true;
		}

		// Token: 0x060075A8 RID: 30120 RVA: 0x001E80F0 File Offset: 0x001E62F0
		internal void ConvertAndAddValidValue(ParameterInfo parameter, IParameterDef paramDef, object value, object label)
		{
			value = this.ConvertValue(value, paramDef, false);
			string text = label as string;
			text = this.ApplySandboxStringRestriction(text, paramDef.Name, "Label");
			if (this.ValidateValue(value, paramDef, "ValidValues"))
			{
				parameter.AddValidValue(value, text);
			}
		}

		// Token: 0x060075A9 RID: 30121 RVA: 0x001E813A File Offset: 0x001E633A
		protected static string ApplySandboxRestriction(ref string value, string paramName, string propertyName, OnDemandProcessingContext odpContext, int maxStringResultLength)
		{
			if (maxStringResultLength != -1 && value != null && value.Length > maxStringResultLength)
			{
				value = null;
				odpContext.ErrorContext.Register(ProcessingErrorCode.rsSandboxingStringResultExceedsMaximumLength, Severity.Warning, ObjectType.ReportParameter, paramName, propertyName, Array.Empty<string>());
			}
			return value;
		}

		// Token: 0x1700279F RID: 10143
		// (get) Token: 0x060075AA RID: 30122 RVA: 0x001E8170 File Offset: 0x001E6370
		internal IInternalProcessingContext ProcessingContext
		{
			get
			{
				return this.m_processingContext;
			}
		}

		// Token: 0x04003B96 RID: 15254
		private IInternalProcessingContext m_processingContext;

		// Token: 0x04003B97 RID: 15255
		private ReportParameterDataSetCache m_paramDataSetCache;

		// Token: 0x04003B98 RID: 15256
		private Dictionary<string, bool> m_dependenciesSubmitted;

		// Token: 0x04003B99 RID: 15257
		private int m_lastDynamicParam;

		// Token: 0x04003B9A RID: 15258
		private ParameterInfoCollection m_parameters;

		// Token: 0x04003B9B RID: 15259
		protected int m_maxStringResultLength = -1;

		// Token: 0x04003B9C RID: 15260
		protected const int UnrestrictedStringResultLength = -1;
	}
}
