using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000779 RID: 1913
	public sealed class ReportRuntime : IErrorContext
	{
		// Token: 0x06006A25 RID: 27173 RVA: 0x001AA16B File Offset: 0x001A836B
		internal ReportRuntime(ObjectModelImpl reportObjectModel, ErrorContext errorContext)
		{
			this.m_objectType = ObjectType.Report;
			this.m_reportObjectModel = reportObjectModel;
			this.m_errorContext = errorContext;
		}

		// Token: 0x06006A26 RID: 27174 RVA: 0x001AA188 File Offset: 0x001A8388
		internal ReportRuntime(ObjectModelImpl reportObjectModel, ErrorContext errorContext, ReportExprHost copyReportExprHost, ReportRuntime topLevelReportRuntime)
			: this(reportObjectModel, errorContext)
		{
			this.m_reportExprHost = copyReportExprHost;
			this.m_topLevelReportRuntime = topLevelReportRuntime;
		}

		// Token: 0x17002542 RID: 9538
		// (get) Token: 0x06006A27 RID: 27175 RVA: 0x001AA1A1 File Offset: 0x001A83A1
		internal ReportExprHost ReportExprHost
		{
			get
			{
				return this.m_reportExprHost;
			}
		}

		// Token: 0x17002543 RID: 9539
		// (get) Token: 0x06006A28 RID: 27176 RVA: 0x001AA1A9 File Offset: 0x001A83A9
		// (set) Token: 0x06006A29 RID: 27177 RVA: 0x001AA1B1 File Offset: 0x001A83B1
		internal ReportProcessing.IScope CurrentScope
		{
			get
			{
				return this.m_currentScope;
			}
			set
			{
				this.m_currentScope = value;
			}
		}

		// Token: 0x17002544 RID: 9540
		// (get) Token: 0x06006A2A RID: 27178 RVA: 0x001AA1BA File Offset: 0x001A83BA
		// (set) Token: 0x06006A2B RID: 27179 RVA: 0x001AA1C2 File Offset: 0x001A83C2
		internal ObjectType ObjectType
		{
			get
			{
				return this.m_objectType;
			}
			set
			{
				this.m_objectType = value;
			}
		}

		// Token: 0x17002545 RID: 9541
		// (get) Token: 0x06006A2C RID: 27180 RVA: 0x001AA1CB File Offset: 0x001A83CB
		// (set) Token: 0x06006A2D RID: 27181 RVA: 0x001AA1D3 File Offset: 0x001A83D3
		internal string ObjectName
		{
			get
			{
				return this.m_objectName;
			}
			set
			{
				this.m_objectName = value;
			}
		}

		// Token: 0x17002546 RID: 9542
		// (get) Token: 0x06006A2E RID: 27182 RVA: 0x001AA1DC File Offset: 0x001A83DC
		// (set) Token: 0x06006A2F RID: 27183 RVA: 0x001AA1E4 File Offset: 0x001A83E4
		internal string PropertyName
		{
			get
			{
				return this.m_propertyName;
			}
			set
			{
				this.m_propertyName = value;
			}
		}

		// Token: 0x17002547 RID: 9543
		// (get) Token: 0x06006A30 RID: 27184 RVA: 0x001AA1ED File Offset: 0x001A83ED
		internal ObjectModelImpl ReportObjectModel
		{
			get
			{
				return this.m_reportObjectModel;
			}
		}

		// Token: 0x17002548 RID: 9544
		// (get) Token: 0x06006A31 RID: 27185 RVA: 0x001AA1F5 File Offset: 0x001A83F5
		// (set) Token: 0x06006A32 RID: 27186 RVA: 0x001AA1FD File Offset: 0x001A83FD
		internal IActionOwner CurrentActionOwner
		{
			get
			{
				return this.m_currentActionOwner;
			}
			set
			{
				this.m_currentActionOwner = value;
			}
		}

		// Token: 0x06006A33 RID: 27187 RVA: 0x001AA206 File Offset: 0x001A8406
		void IErrorContext.Register(ProcessingErrorCode code, Severity severity, params string[] arguments)
		{
			this.m_errorContext.Register(code, severity, this.m_objectType, this.m_objectName, this.m_propertyName, arguments);
		}

		// Token: 0x06006A34 RID: 27188 RVA: 0x001AA229 File Offset: 0x001A8429
		void IErrorContext.Register(ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, params string[] arguments)
		{
			this.m_errorContext.Register(code, severity, objectType, objectName, propertyName, arguments);
		}

		// Token: 0x06006A35 RID: 27189 RVA: 0x001AA240 File Offset: 0x001A8440
		internal static string GetErrorName(DataFieldStatus status, string exceptionMessage)
		{
			if (exceptionMessage != null)
			{
				return exceptionMessage;
			}
			if (status == DataFieldStatus.Overflow)
			{
				return "OverflowException.";
			}
			if (status == DataFieldStatus.UnSupportedDataType)
			{
				return "UnsupportedDatatypeException.";
			}
			if (status != DataFieldStatus.IsError)
			{
				return null;
			}
			return "FieldValueException.";
		}

		// Token: 0x06006A36 RID: 27190 RVA: 0x001AA268 File Offset: 0x001A8468
		internal string EvaluateReportLanguageExpression(Report report, out CultureInfo language)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(report.Language, report.ObjectType, report.Name, "Language", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(report.Language, ref variantResult))
					{
						Global.Tracer.Assert(report.ReportExprHost != null);
						variantResult.Value = report.ReportExprHost.ReportLanguageExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateSpecificLanguage(this.ProcessStringResult(variantResult).Value, this, out language);
		}

		// Token: 0x06006A37 RID: 27191 RVA: 0x001AA2FC File Offset: 0x001A84FC
		internal VariantResult EvaluateParamDefaultValue(ParameterDef paramDef, int index)
		{
			Global.Tracer.Assert(paramDef.DefaultExpressions != null);
			ExpressionInfo expressionInfo = paramDef.DefaultExpressions[index];
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expressionInfo, ObjectType.ReportParameter, paramDef.Name, "DefaultValue", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expressionInfo, ref variantResult))
					{
						Global.Tracer.Assert(paramDef.ExprHost != null && expressionInfo.ExprHostID >= 0);
						this.m_reportObjectModel.UserImpl.IndirectQueryReference = paramDef.UsedInQuery;
						variantResult.Value = paramDef.ExprHost[expressionInfo.ExprHostID];
						this.m_reportObjectModel.UserImpl.IndirectQueryReference = false;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(expressionInfo, ref variantResult, true);
			return variantResult;
		}

		// Token: 0x06006A38 RID: 27192 RVA: 0x001AA3D8 File Offset: 0x001A85D8
		internal VariantResult EvaluateParamValidValue(ParameterDef paramDef, int index)
		{
			Global.Tracer.Assert(paramDef.ValidValuesValueExpressions != null);
			ExpressionInfo expressionInfo = paramDef.ValidValuesValueExpressions[index];
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expressionInfo, ObjectType.ReportParameter, paramDef.Name, "ValidValue", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expressionInfo, ref variantResult))
					{
						Global.Tracer.Assert(paramDef.ExprHost != null && paramDef.ExprHost.ValidValuesHost != null && expressionInfo.ExprHostID >= 0);
						this.m_reportObjectModel.UserImpl.IndirectQueryReference = paramDef.UsedInQuery;
						variantResult.Value = paramDef.ExprHost.ValidValuesHost[expressionInfo.ExprHostID];
						this.m_reportObjectModel.UserImpl.IndirectQueryReference = false;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(expressionInfo, ref variantResult, true);
			return variantResult;
		}

		// Token: 0x06006A39 RID: 27193 RVA: 0x001AA4C8 File Offset: 0x001A86C8
		internal VariantResult EvaluateParamValidValueLabel(ParameterDef paramDef, int index)
		{
			Global.Tracer.Assert(paramDef.ValidValuesLabelExpressions != null);
			ExpressionInfo expressionInfo = paramDef.ValidValuesLabelExpressions[index];
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expressionInfo, ObjectType.ReportParameter, paramDef.Name, "Label", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expressionInfo, ref variantResult))
					{
						Global.Tracer.Assert(paramDef.ExprHost != null && paramDef.ExprHost.ValidValueLabelsHost != null && expressionInfo.ExprHostID >= 0);
						this.m_reportObjectModel.UserImpl.IndirectQueryReference = paramDef.UsedInQuery;
						variantResult.Value = paramDef.ExprHost.ValidValueLabelsHost[expressionInfo.ExprHostID];
						this.m_reportObjectModel.UserImpl.IndirectQueryReference = false;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(expressionInfo, ref variantResult, true);
			if (!variantResult.ErrorOccurred && variantResult.Value is object[])
			{
				try
				{
					VariantResult variantResult2 = default(VariantResult);
					object[] array = variantResult.Value as object[];
					for (int i = 0; i < array.Length; i++)
					{
						variantResult2.Value = array[i];
						this.ProcessLabelResult(ref variantResult2);
						if (variantResult2.ErrorOccurred)
						{
							variantResult.ErrorOccurred = true;
							return variantResult;
						}
						array[i] = variantResult2.Value;
					}
					return variantResult;
				}
				catch
				{
					this.RegisterInvalidExpressionDataTypeWarning();
					variantResult.ErrorOccurred = true;
					return variantResult;
				}
			}
			if (!variantResult.ErrorOccurred)
			{
				this.ProcessLabelResult(ref variantResult);
			}
			return variantResult;
		}

		// Token: 0x06006A3A RID: 27194 RVA: 0x001AA65C File Offset: 0x001A885C
		internal object EvaluateDataValueValueExpression(DataValue value, ObjectType objectType, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(value.Value, objectType, objectName, propertyName, out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(value.Value, ref variantResult))
					{
						Global.Tracer.Assert(value.ExprHost != null);
						variantResult.Value = value.ExprHost.DataValueValueExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(value.Value, ref variantResult);
			return variantResult.Value;
		}

		// Token: 0x06006A3B RID: 27195 RVA: 0x001AA6E4 File Offset: 0x001A88E4
		internal string EvaluateDataValueNameExpression(DataValue value, ObjectType objectType, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(value.Name, objectType, objectName, propertyName, out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(value.Name, ref variantResult))
					{
						Global.Tracer.Assert(value.ExprHost != null);
						variantResult.Value = value.ExprHost.DataValueNameExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06006A3C RID: 27196 RVA: 0x001AA764 File Offset: 0x001A8964
		internal VariantResult EvaluateFilterVariantExpression(Filter filter, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(filter.Expression, objectType, objectName, "FilterExpression", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(filter.Expression, ref variantResult))
					{
						Global.Tracer.Assert(filter.ExprHost != null);
						variantResult.Value = filter.ExprHost.FilterExpressionExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(filter.Expression, ref variantResult, false);
			return variantResult;
		}

		// Token: 0x06006A3D RID: 27197 RVA: 0x001AA7EC File Offset: 0x001A89EC
		internal StringResult EvaluateFilterStringExpression(Filter filter, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(filter.Expression, objectType, objectName, "FilterExpression", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(filter.Expression, ref variantResult))
					{
						Global.Tracer.Assert(filter.ExprHost != null);
						variantResult.Value = filter.ExprHost.FilterExpressionExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult);
		}

		// Token: 0x06006A3E RID: 27198 RVA: 0x001AA86C File Offset: 0x001A8A6C
		internal VariantResult EvaluateFilterVariantValue(Filter filter, int index, ObjectType objectType, string objectName)
		{
			Global.Tracer.Assert(filter.Values != null);
			ExpressionInfo expressionInfo = filter.Values[index];
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expressionInfo, objectType, objectName, "FilterValue", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expressionInfo, ref variantResult))
					{
						Global.Tracer.Assert(filter.ExprHost != null && expressionInfo.ExprHostID >= 0);
						variantResult.Value = filter.ExprHost[expressionInfo.ExprHostID];
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(expressionInfo, ref variantResult, true);
			return variantResult;
		}

		// Token: 0x06006A3F RID: 27199 RVA: 0x001AA91C File Offset: 0x001A8B1C
		internal FloatResult EvaluateFilterIntegerOrFloatValue(Filter filter, int index, ObjectType objectType, string objectName)
		{
			Global.Tracer.Assert(filter.Values != null);
			ExpressionInfo expressionInfo = filter.Values[index];
			VariantResult variantResult;
			if (!this.EvaluateIntegerOrFloatExpression(expressionInfo, objectType, objectName, "FilterValue", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expressionInfo, ref variantResult))
					{
						Global.Tracer.Assert(filter.ExprHost != null && expressionInfo.ExprHostID >= 0);
						variantResult.Value = filter.ExprHost[expressionInfo.ExprHostID];
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessIntegerOrFloatResult(variantResult);
		}

		// Token: 0x06006A40 RID: 27200 RVA: 0x001AA9C8 File Offset: 0x001A8BC8
		internal IntegerResult EvaluateFilterIntegerValue(Filter filter, int index, ObjectType objectType, string objectName)
		{
			Global.Tracer.Assert(filter.Values != null);
			ExpressionInfo expressionInfo = filter.Values[index];
			VariantResult variantResult;
			if (!this.EvaluateIntegerExpression(expressionInfo, false, objectType, objectName, "FilterValue", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expressionInfo, ref variantResult))
					{
						Global.Tracer.Assert(filter.ExprHost != null && expressionInfo.ExprHostID >= 0);
						variantResult.Value = filter.ExprHost[expressionInfo.ExprHostID];
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessIntegerResult(variantResult);
		}

		// Token: 0x06006A41 RID: 27201 RVA: 0x001AAA74 File Offset: 0x001A8C74
		internal StringResult EvaluateFilterStringValue(Filter filter, int index, ObjectType objectType, string objectName)
		{
			Global.Tracer.Assert(filter.Values != null);
			ExpressionInfo expressionInfo = filter.Values[index];
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expressionInfo, objectType, objectName, "FilterValue", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expressionInfo, ref variantResult))
					{
						Global.Tracer.Assert(filter.ExprHost != null && expressionInfo.ExprHostID >= 0);
						variantResult.Value = filter.ExprHost[expressionInfo.ExprHostID];
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult);
		}

		// Token: 0x06006A42 RID: 27202 RVA: 0x001AAB20 File Offset: 0x001A8D20
		internal object EvaluateQueryParamValue(ExpressionInfo paramValue, IndexedExprHost queryParamsExprHost, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(paramValue, objectType, objectName, "Value", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(paramValue, ref variantResult))
					{
						this.m_reportObjectModel.UserImpl.UserProfileLocation = UserProfileState.InQuery;
						Global.Tracer.Assert(queryParamsExprHost != null && paramValue.ExprHostID >= 0);
						variantResult.Value = queryParamsExprHost[paramValue.ExprHostID];
						this.m_reportObjectModel.UserImpl.UserProfileLocation = UserProfileState.InReport;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpressionAndStop(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(paramValue, ref variantResult, true);
			return variantResult.Value;
		}

		// Token: 0x06006A43 RID: 27203 RVA: 0x001AABCC File Offset: 0x001A8DCC
		internal StringResult EvaluateConnectString(DataSource dataSource)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(dataSource.ConnectStringExpression, ObjectType.DataSource, dataSource.Name, "ConnectString", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(dataSource.ConnectStringExpression, ref variantResult))
					{
						this.m_reportObjectModel.UserImpl.UserProfileLocation = UserProfileState.InQuery;
						Global.Tracer.Assert(dataSource.ExprHost != null);
						variantResult.Value = dataSource.ExprHost.ConnectStringExpr;
						this.m_reportObjectModel.UserImpl.UserProfileLocation = UserProfileState.InReport;
					}
				}
				catch (Exception ex)
				{
					if (ex is ReportProcessingException_NonExistingParameterReference)
					{
						this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
					}
					else
					{
						variantResult = new VariantResult(true, null);
					}
				}
			}
			return this.ProcessStringResult(variantResult);
		}

		// Token: 0x06006A44 RID: 27204 RVA: 0x001AAC88 File Offset: 0x001A8E88
		internal StringResult EvaluateCommandText(DataSet dataSet)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(dataSet.Query.CommandText, ObjectType.Query, dataSet.Name, "CommandText", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(dataSet.Query.CommandText, ref variantResult))
					{
						this.m_reportObjectModel.UserImpl.UserProfileLocation = UserProfileState.InQuery;
						Global.Tracer.Assert(dataSet.ExprHost != null);
						variantResult.Value = dataSet.ExprHost.QueryCommandTextExpr;
						this.m_reportObjectModel.UserImpl.UserProfileLocation = UserProfileState.InReport;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult);
		}

		// Token: 0x06006A45 RID: 27205 RVA: 0x001AAD38 File Offset: 0x001A8F38
		internal object EvaluateFieldValueExpression(Field field)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(field.Value, ObjectType.Field, field.Name, "Value", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(field.Value, ref variantResult))
					{
						Global.Tracer.Assert(field.ExprHost != null);
						variantResult.Value = field.ExprHost.ValueExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(field.Value, ref variantResult);
			return variantResult.Value;
		}

		// Token: 0x06006A46 RID: 27206 RVA: 0x001AADCC File Offset: 0x001A8FCC
		internal VariantResult EvaluateAggregateVariantOrBinaryParamExpr(DataAggregateInfo aggregateInfo, int index, IErrorContext errorContext)
		{
			Global.Tracer.Assert(aggregateInfo.Expressions != null);
			ExpressionInfo expressionInfo = aggregateInfo.Expressions[index];
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expressionInfo, out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expressionInfo, ref variantResult))
					{
						Global.Tracer.Assert(aggregateInfo.ExpressionHosts != null && expressionInfo.ExprHostID >= 0);
						variantResult.Value = aggregateInfo.ExpressionHosts[index].ValueExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex, errorContext, false);
				}
			}
			this.ProcessVariantOrBinaryResult(expressionInfo, ref variantResult, errorContext, true);
			return variantResult;
		}

		// Token: 0x06006A47 RID: 27207 RVA: 0x001AAE6C File Offset: 0x001A906C
		internal bool EvaluateParamValueOmitExpression(ParameterValue paramVal, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateBooleanExpression(paramVal.Omit, true, objectType, objectName, "ParameterOmit", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(paramVal.Omit, ref variantResult))
					{
						Global.Tracer.Assert(paramVal.ExprHost != null);
						variantResult.Value = paramVal.ExprHost.OmitExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06006A48 RID: 27208 RVA: 0x001AAEF0 File Offset: 0x001A90F0
		internal object EvaluateParamVariantValueExpression(ParameterValue paramVal, ObjectType objectType, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(paramVal.Value, objectType, objectName, propertyName, out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(paramVal.Value, ref variantResult))
					{
						Global.Tracer.Assert(paramVal.ExprHost != null);
						variantResult.Value = paramVal.ExprHost.ValueExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(paramVal.Value, ref variantResult, true);
			return variantResult.Value;
		}

		// Token: 0x06006A49 RID: 27209 RVA: 0x001AAF7C File Offset: 0x001A917C
		internal ParameterValueResult EvaluateParameterValueExpression(ParameterValue paramVal, ObjectType objectType, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(paramVal.Value, objectType, objectName, propertyName, out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(paramVal.Value, ref variantResult))
					{
						Global.Tracer.Assert(paramVal.ExprHost != null);
						variantResult.Value = paramVal.ExprHost.ValueExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessParameterValueResult(paramVal.Value, variantResult);
		}

		// Token: 0x06006A4A RID: 27210 RVA: 0x001AB000 File Offset: 0x001A9200
		internal bool EvaluateStartHiddenExpression(Visibility visibility, IVisibilityHiddenExprHost hiddenExprHostRI, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateBooleanExpression(visibility.Hidden, true, objectType, objectName, "Hidden", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(visibility.Hidden, ref variantResult))
					{
						variantResult.Value = hiddenExprHostRI.VisibilityHiddenExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpressionAndStop(ref variantResult, ex);
				}
			}
			return this.ProcessBooleanResult(variantResult, true, objectType, objectName).Value;
		}

		// Token: 0x06006A4B RID: 27211 RVA: 0x001AB074 File Offset: 0x001A9274
		internal bool EvaluateStartHiddenExpression(Visibility visibility, IndexedExprHost hiddenExprHostIdx, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateBooleanExpression(visibility.Hidden, true, objectType, objectName, "Hidden", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(visibility.Hidden, ref variantResult))
					{
						Global.Tracer.Assert(hiddenExprHostIdx != null && visibility.Hidden.ExprHostID >= 0);
						variantResult.Value = hiddenExprHostIdx[visibility.Hidden.ExprHostID];
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpressionAndStop(ref variantResult, ex);
				}
			}
			return this.ProcessBooleanResult(variantResult, true, objectType, objectName).Value;
		}

		// Token: 0x06006A4C RID: 27212 RVA: 0x001AB114 File Offset: 0x001A9314
		internal VariantResult EvaluateReportItemLabelExpression(ReportItem reportItem)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(reportItem.Label, reportItem.ObjectType, reportItem.Name, "Label", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(reportItem.Label, ref variantResult))
					{
						Global.Tracer.Assert(reportItem.ExprHost != null);
						variantResult.Value = reportItem.ExprHost.LabelExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(reportItem.Label, ref variantResult);
			return variantResult;
		}

		// Token: 0x06006A4D RID: 27213 RVA: 0x001AB1A4 File Offset: 0x001A93A4
		internal string EvaluateReportItemBookmarkExpression(ReportItem reportItem)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(reportItem.Bookmark, reportItem.ObjectType, reportItem.Name, "Bookmark", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(reportItem.Bookmark, ref variantResult))
					{
						Global.Tracer.Assert(reportItem.ExprHost != null);
						variantResult.Value = reportItem.ExprHost.BookmarkExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06006A4E RID: 27214 RVA: 0x001AB234 File Offset: 0x001A9434
		internal string EvaluateReportItemToolTipExpression(ReportItem reportItem)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(reportItem.ToolTip, reportItem.ObjectType, reportItem.Name, "ToolTip", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(reportItem.ToolTip, ref variantResult))
					{
						Global.Tracer.Assert(reportItem.ExprHost != null);
						variantResult.Value = reportItem.ExprHost.ToolTipExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06006A4F RID: 27215 RVA: 0x001AB2C4 File Offset: 0x001A94C4
		internal string EvaluateActionLabelExpression(ActionItem actionItem, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Label", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(actionItem.ExprHost != null);
						variantResult.Value = actionItem.ExprHost.LabelExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06006A50 RID: 27216 RVA: 0x001AB340 File Offset: 0x001A9540
		internal string EvaluateReportItemHyperlinkURLExpression(ActionItem actionItem, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Hyperlink", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(actionItem.ExprHost != null);
						variantResult.Value = actionItem.ExprHost.HyperlinkExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06006A51 RID: 27217 RVA: 0x001AB3BC File Offset: 0x001A95BC
		internal string EvaluateReportItemDrillthroughReportName(ActionItem actionItem, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "DrillthroughReportName", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(actionItem.ExprHost != null);
						variantResult.Value = actionItem.ExprHost.DrillThroughReportNameExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06006A52 RID: 27218 RVA: 0x001AB438 File Offset: 0x001A9638
		internal string EvaluateReportItemBookmarkLinkExpression(ActionItem actionItem, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BookmarkLink", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(actionItem.ExprHost != null);
						variantResult.Value = actionItem.ExprHost.BookmarkLinkExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06006A53 RID: 27219 RVA: 0x001AB4B4 File Offset: 0x001A96B4
		internal string EvaluateImageStringValueExpression(Image image, out bool errorOccurred)
		{
			errorOccurred = false;
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(image.Value, image.ObjectType, image.Name, "Value", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(image.Value, ref variantResult))
					{
						Global.Tracer.Assert(image.ImageExprHost != null);
						variantResult.Value = image.ImageExprHost.ValueExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			StringResult stringResult = this.ProcessStringResult(variantResult);
			errorOccurred = stringResult.ErrorOccurred;
			return stringResult.Value;
		}

		// Token: 0x06006A54 RID: 27220 RVA: 0x001AB550 File Offset: 0x001A9750
		internal byte[] EvaluateImageBinaryValueExpression(Image image, out bool errorOccurred)
		{
			VariantResult variantResult;
			if (!this.EvaluateBinaryExpression(image.Value, image.ObjectType, image.Name, "Value", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(image.Value, ref variantResult))
					{
						Global.Tracer.Assert(image.ImageExprHost != null);
						variantResult.Value = image.ImageExprHost.ValueExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			BinaryResult binaryResult = this.ProcessBinaryResult(variantResult);
			errorOccurred = binaryResult.ErrorOccurred;
			return binaryResult.Value;
		}

		// Token: 0x06006A55 RID: 27221 RVA: 0x001AB5E8 File Offset: 0x001A97E8
		internal string EvaluateImageMIMETypeExpression(Image image)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(image.MIMEType, image.ObjectType, image.Name, "Value", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(image.MIMEType, ref variantResult))
					{
						Global.Tracer.Assert(image.ImageExprHost != null);
						variantResult.Value = image.ImageExprHost.MIMETypeExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06006A56 RID: 27222 RVA: 0x001AB678 File Offset: 0x001A9878
		internal VariantResult EvaluateTextBoxValueExpression(TextBox textBox)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(textBox.Value, textBox.ObjectType, textBox.Name, "Value", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(textBox.Value, ref variantResult))
					{
						Global.Tracer.Assert(textBox.TextBoxExprHost != null);
						variantResult.Value = textBox.TextBoxExprHost.ValueExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(textBox.Value, ref variantResult);
			return variantResult;
		}

		// Token: 0x06006A57 RID: 27223 RVA: 0x001AB708 File Offset: 0x001A9908
		internal bool EvaluateTextBoxInitialToggleStateExpression(TextBox textBox)
		{
			VariantResult variantResult;
			if (!this.EvaluateBooleanExpression(textBox.InitialToggleState, true, textBox.ObjectType, textBox.Name, "InitialState", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(textBox.InitialToggleState, ref variantResult))
					{
						Global.Tracer.Assert(textBox.ExprHost != null);
						variantResult.Value = textBox.TextBoxExprHost.ToggleImageInitialStateExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06006A58 RID: 27224 RVA: 0x001AB798 File Offset: 0x001A9998
		internal object EvaluateUserSortExpression(TextBox textBox)
		{
			int sortExpressionIndex = textBox.UserSort.SortExpressionIndex;
			ISortFilterScope sortTarget = textBox.UserSort.SortTarget;
			Global.Tracer.Assert(sortTarget.UserSortExpressions != null && 0 <= sortExpressionIndex && sortExpressionIndex < sortTarget.UserSortExpressions.Count);
			ExpressionInfo expressionInfo = sortTarget.UserSortExpressions[sortExpressionIndex];
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expressionInfo, textBox.ObjectType, textBox.Name, "SortExpression", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expressionInfo, ref variantResult))
					{
						Global.Tracer.Assert(sortTarget.UserSortExpressionsHost != null);
						variantResult.Value = sortTarget.UserSortExpressionsHost[expressionInfo.ExprHostID];
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpressionAndStop(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(expressionInfo, ref variantResult);
			if (variantResult.Value == null)
			{
				variantResult.Value = DBNull.Value;
			}
			return variantResult.Value;
		}

		// Token: 0x06006A59 RID: 27225 RVA: 0x001AB888 File Offset: 0x001A9A88
		internal VariantResult EvaluateGroupingLabelExpression(Grouping grouping, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(grouping.GroupLabel, objectType, objectName, "Label", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(grouping.GroupLabel, ref variantResult))
					{
						Global.Tracer.Assert(grouping.ExprHost != null);
						variantResult.Value = grouping.ExprHost.LabelExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(grouping.GroupLabel, ref variantResult);
			return variantResult;
		}

		// Token: 0x06006A5A RID: 27226 RVA: 0x001AB910 File Offset: 0x001A9B10
		internal object EvaluateRuntimeExpression(ReportProcessing.RuntimeExpressionInfo runtimeExpression, ObjectType objectType, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(runtimeExpression.Expression, objectType, objectName, propertyName, out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(runtimeExpression.Expression, ref variantResult))
					{
						Global.Tracer.Assert(runtimeExpression.ExpressionsHost != null && runtimeExpression.Expression.ExprHostID >= 0);
						variantResult.Value = runtimeExpression.ExpressionsHost[runtimeExpression.Expression.ExprHostID];
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpressionAndStop(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(runtimeExpression.Expression, ref variantResult);
			if (variantResult.ErrorOccurred)
			{
				if (variantResult.FieldStatus != DataFieldStatus.None)
				{
					((IErrorContext)this).Register(ProcessingErrorCode.rsFieldErrorInExpression, Severity.Error, new string[] { ReportRuntime.GetErrorName(variantResult.FieldStatus, variantResult.ExceptionMessage) });
				}
				else
				{
					((IErrorContext)this).Register(ProcessingErrorCode.rsInvalidExpressionDataType, Severity.Error, Array.Empty<string>());
				}
				throw new ReportProcessingException(this.m_errorContext.Messages);
			}
			if (variantResult.Value == null)
			{
				variantResult.Value = DBNull.Value;
			}
			return variantResult.Value;
		}

		// Token: 0x06006A5B RID: 27227 RVA: 0x001ABA24 File Offset: 0x001A9C24
		internal object EvaluateOWCChartData(OWCChart chart, ExpressionInfo chartDataExpression)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartDataExpression, chart.ObjectType, chart.Name, "Value", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(chartDataExpression, ref variantResult))
					{
						Global.Tracer.Assert(chart.OWCChartExprHost != null && chart.OWCChartExprHost.OWCChartColumnHosts != null && chartDataExpression.ExprHostID >= 0);
						variantResult.Value = chart.OWCChartExprHost.OWCChartColumnHosts[chartDataExpression.ExprHostID];
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(chartDataExpression, ref variantResult);
			return variantResult.Value;
		}

		// Token: 0x06006A5C RID: 27228 RVA: 0x001ABAD0 File Offset: 0x001A9CD0
		internal string EvaluateSubReportNoRowsExpression(SubReport subReport, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(subReport.NoRows, ObjectType.Subreport, objectName, propertyName, out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(subReport.NoRows, ref variantResult))
					{
						Global.Tracer.Assert(subReport.SubReportExprHost != null);
						variantResult.Value = subReport.SubReportExprHost.NoRowsExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06006A5D RID: 27229 RVA: 0x001ABB50 File Offset: 0x001A9D50
		internal string EvaluateDataRegionNoRowsExpression(DataRegion region, ObjectType objectType, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(region.NoRows, objectType, objectName, propertyName, out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(region.NoRows, ref variantResult))
					{
						Global.Tracer.Assert(region.ExprHost != null);
						variantResult.Value = ((DataRegionExprHost)region.ExprHost).NoRowsExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06006A5E RID: 27230 RVA: 0x001ABBD8 File Offset: 0x001A9DD8
		internal object EvaluateChartDataPointDataValueExpression(ChartDataPoint dataPoint, ExpressionInfo dataPointDataValueExpression, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(dataPointDataValueExpression, ObjectType.Chart, objectName, "DataPoint", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(dataPointDataValueExpression, ref variantResult))
					{
						Global.Tracer.Assert(dataPoint.ExprHost != null && dataPointDataValueExpression.ExprHostID >= 0);
						variantResult.Value = dataPoint.ExprHost[dataPointDataValueExpression.ExprHostID];
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(dataPointDataValueExpression, ref variantResult);
			return variantResult.Value;
		}

		// Token: 0x06006A5F RID: 27231 RVA: 0x001ABC6C File Offset: 0x001A9E6C
		internal object EvaluateChartStaticHeadingLabelExpression(ChartHeading chartHeading, ExpressionInfo expression, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, ObjectType.Chart, objectName, "HeadingLabel", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Chart chart = (Chart)chartHeading.DataRegionDef;
						IndexedExprHost indexedExprHost = null;
						if (chart.ChartExprHost != null)
						{
							if (chartHeading.IsColumn)
							{
								indexedExprHost = chart.ChartExprHost.StaticColumnLabelsHost;
							}
							else
							{
								indexedExprHost = chart.ChartExprHost.StaticRowLabelsHost;
							}
						}
						Global.Tracer.Assert(indexedExprHost != null && expression.ExprHostID >= 0);
						variantResult.Value = indexedExprHost[expression.ExprHostID];
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(expression, ref variantResult);
			return variantResult.Value;
		}

		// Token: 0x06006A60 RID: 27232 RVA: 0x001ABD30 File Offset: 0x001A9F30
		internal object EvaluateChartDynamicHeadingLabelExpression(ChartHeading chartHeading, ExpressionInfo expression, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, ObjectType.Chart, objectName, "HeadingLabel", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(chartHeading.ExprHost != null);
						variantResult.Value = chartHeading.ExprHost.HeadingLabelExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(expression, ref variantResult);
			return variantResult.Value;
		}

		// Token: 0x06006A61 RID: 27233 RVA: 0x001ABDB0 File Offset: 0x001A9FB0
		internal string EvaluateChartTitleCaptionExpression(ChartTitle title, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(title.Caption, ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(title.Caption, ref variantResult))
					{
						Global.Tracer.Assert(title.ExprHost != null);
						variantResult.Value = title.ExprHost.CaptionExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06006A62 RID: 27234 RVA: 0x001ABE30 File Offset: 0x001AA030
		internal string EvaluateChartDataLabelValueExpression(ChartDataPoint dataPoint, string objectName, object[] dataLabelStyleAttributeValues)
		{
			Global.Tracer.Assert(dataPoint.DataLabel != null);
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(dataPoint.DataLabel.Value, ObjectType.Chart, objectName, "DataLabel", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(dataPoint.DataLabel.Value, ref variantResult))
					{
						Global.Tracer.Assert(dataPoint.ExprHost != null);
						variantResult.Value = dataPoint.ExprHost.DataLabelValueExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(dataPoint.DataLabel.Value, ref variantResult);
			if (variantResult.Value != null)
			{
				string text = null;
				if (dataPoint.DataLabel.StyleClass != null)
				{
					AttributeInfo attributeInfo = dataPoint.DataLabel.StyleClass.StyleAttributes["Format"];
					if (attributeInfo != null)
					{
						if (attributeInfo.IsExpression)
						{
							text = (string)dataLabelStyleAttributeValues[attributeInfo.IntValue];
						}
						else
						{
							text = attributeInfo.Value;
						}
					}
				}
				string text2 = ((variantResult.Value is IFormattable) ? ((IFormattable)variantResult.Value).ToString(text, Thread.CurrentThread.CurrentCulture) : variantResult.Value.ToString());
				variantResult.Value = text2;
			}
			return (string)variantResult.Value;
		}

		// Token: 0x06006A63 RID: 27235 RVA: 0x001ABF80 File Offset: 0x001AA180
		internal object EvaluateChartAxisValueExpression(AxisExprHost exprHost, ExpressionInfo expression, string objectName, string propertyName, Axis.ExpressionType type)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(exprHost != null);
						switch (type)
						{
						case Axis.ExpressionType.Min:
							variantResult.Value = exprHost.AxisMinExpr;
							break;
						case Axis.ExpressionType.Max:
							variantResult.Value = exprHost.AxisMaxExpr;
							break;
						case Axis.ExpressionType.CrossAt:
							variantResult.Value = exprHost.AxisCrossAtExpr;
							break;
						case Axis.ExpressionType.MajorInterval:
							variantResult.Value = exprHost.AxisMajorIntervalExpr;
							break;
						case Axis.ExpressionType.MinorInterval:
							variantResult.Value = exprHost.AxisMinorIntervalExpr;
							break;
						}
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(expression, ref variantResult);
			return variantResult.Value;
		}

		// Token: 0x06006A64 RID: 27236 RVA: 0x001AC04C File Offset: 0x001AA24C
		internal string EvaluateStyleBorderColor(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderColor", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderColorExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A65 RID: 27237 RVA: 0x001AC0CC File Offset: 0x001AA2CC
		internal string EvaluateStyleBorderColorLeft(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderColor", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderColorLeftExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A66 RID: 27238 RVA: 0x001AC14C File Offset: 0x001AA34C
		internal string EvaluateStyleBorderColorRight(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderColor", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderColorRightExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A67 RID: 27239 RVA: 0x001AC1CC File Offset: 0x001AA3CC
		internal string EvaluateStyleBorderColorTop(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderColor", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderColorTopExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A68 RID: 27240 RVA: 0x001AC24C File Offset: 0x001AA44C
		internal string EvaluateStyleBorderColorBottom(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderColor", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderColorBottomExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A69 RID: 27241 RVA: 0x001AC2CC File Offset: 0x001AA4CC
		internal string EvaluateStyleBorderStyle(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderStyle", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderStyleExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateBorderStyle(this.ProcessStringResult(variantResult).Value, objectType, this);
		}

		// Token: 0x06006A6A RID: 27242 RVA: 0x001AC350 File Offset: 0x001AA550
		internal string EvaluateStyleBorderStyleLeft(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderStyle", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderStyleLeftExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateBorderStyle(this.ProcessStringResult(variantResult).Value, objectType, this);
		}

		// Token: 0x06006A6B RID: 27243 RVA: 0x001AC3D4 File Offset: 0x001AA5D4
		internal string EvaluateStyleBorderStyleRight(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderStyle", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderStyleRightExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateBorderStyle(this.ProcessStringResult(variantResult).Value, objectType, this);
		}

		// Token: 0x06006A6C RID: 27244 RVA: 0x001AC458 File Offset: 0x001AA658
		internal string EvaluateStyleBorderStyleTop(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderStyle", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderStyleTopExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateBorderStyle(this.ProcessStringResult(variantResult).Value, objectType, this);
		}

		// Token: 0x06006A6D RID: 27245 RVA: 0x001AC4DC File Offset: 0x001AA6DC
		internal string EvaluateStyleBorderStyleBottom(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderStyle", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderStyleBottomExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateBorderStyle(this.ProcessStringResult(variantResult).Value, objectType, this);
		}

		// Token: 0x06006A6E RID: 27246 RVA: 0x001AC560 File Offset: 0x001AA760
		internal string EvaluateStyleBorderWidth(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderWidth", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderWidthExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateBorderWidth(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A6F RID: 27247 RVA: 0x001AC5E0 File Offset: 0x001AA7E0
		internal string EvaluateStyleBorderWidthLeft(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderWidth", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderWidthLeftExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateBorderWidth(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A70 RID: 27248 RVA: 0x001AC660 File Offset: 0x001AA860
		internal string EvaluateStyleBorderWidthRight(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderWidth", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderWidthRightExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateBorderWidth(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A71 RID: 27249 RVA: 0x001AC6E0 File Offset: 0x001AA8E0
		internal string EvaluateStyleBorderWidthTop(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderWidth", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderWidthTopExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateBorderWidth(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A72 RID: 27250 RVA: 0x001AC760 File Offset: 0x001AA960
		internal string EvaluateStyleBorderWidthBottom(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderWidth", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BorderWidthBottomExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateBorderWidth(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A73 RID: 27251 RVA: 0x001AC7E0 File Offset: 0x001AA9E0
		internal string EvaluateStyleBackgroundColor(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BackgroundColor", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BackgroundColorExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A74 RID: 27252 RVA: 0x001AC860 File Offset: 0x001AAA60
		internal string EvaluateStyleBackgroundGradientEndColor(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BackgroundGradientEndColor", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BackgroundGradientEndColorExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A75 RID: 27253 RVA: 0x001AC8E0 File Offset: 0x001AAAE0
		internal string EvaluateStyleBackgroundGradientType(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BackgroundGradientType", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BackgroundGradientTypeExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateBackgroundGradientType(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A76 RID: 27254 RVA: 0x001AC960 File Offset: 0x001AAB60
		internal string EvaluateStyleBackgroundRepeat(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BackgroundRepeat", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BackgroundRepeatExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateBackgroundRepeat(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A77 RID: 27255 RVA: 0x001AC9E0 File Offset: 0x001AABE0
		internal string EvaluateStyleFontStyle(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "FontStyle", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.FontStyleExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateFontStyle(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A78 RID: 27256 RVA: 0x001ACA60 File Offset: 0x001AAC60
		internal string EvaluateStyleFontFamily(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "FontFamily", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.FontFamilyExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06006A79 RID: 27257 RVA: 0x001ACADC File Offset: 0x001AACDC
		internal string EvaluateStyleFontSize(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "FontSize", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.FontSizeExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateFontSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A7A RID: 27258 RVA: 0x001ACB5C File Offset: 0x001AAD5C
		internal string EvaluateStyleFontWeight(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "FontWeight", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.FontWeightExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateFontWeight(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A7B RID: 27259 RVA: 0x001ACBDC File Offset: 0x001AADDC
		internal string EvaluateStyleFormat(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Format", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.FormatExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06006A7C RID: 27260 RVA: 0x001ACC58 File Offset: 0x001AAE58
		internal string EvaluateStyleTextDecoration(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "TextDecoration", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.TextDecorationExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateTextDecoration(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A7D RID: 27261 RVA: 0x001ACCD8 File Offset: 0x001AAED8
		internal string EvaluateStyleTextAlign(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "TextAlign", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.TextAlignExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateTextAlign(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A7E RID: 27262 RVA: 0x001ACD58 File Offset: 0x001AAF58
		internal string EvaluateStyleVerticalAlign(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "VerticalAlign", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.VerticalAlignExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateVerticalAlign(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A7F RID: 27263 RVA: 0x001ACDD8 File Offset: 0x001AAFD8
		internal string EvaluateStyleColor(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Color", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.ColorExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A80 RID: 27264 RVA: 0x001ACE58 File Offset: 0x001AB058
		internal string EvaluateStylePaddingLeft(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "PaddingLeft", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.PaddingLeftExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidatePadding(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A81 RID: 27265 RVA: 0x001ACED8 File Offset: 0x001AB0D8
		internal string EvaluateStylePaddingRight(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "PaddingRight", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.PaddingRightExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidatePadding(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A82 RID: 27266 RVA: 0x001ACF58 File Offset: 0x001AB158
		internal string EvaluateStylePaddingTop(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "PaddingTop", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.PaddingTopExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidatePadding(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A83 RID: 27267 RVA: 0x001ACFD8 File Offset: 0x001AB1D8
		internal string EvaluateStylePaddingBottom(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "PaddingBottom", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.PaddingBottomExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidatePadding(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A84 RID: 27268 RVA: 0x001AD058 File Offset: 0x001AB258
		internal string EvaluateStyleLineHeight(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "LineHeight", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.LineHeightExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateLineHeight(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A85 RID: 27269 RVA: 0x001AD0D8 File Offset: 0x001AB2D8
		internal string EvaluateStyleDirection(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Direction", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.DirectionExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateDirection(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A86 RID: 27270 RVA: 0x001AD158 File Offset: 0x001AB358
		internal string EvaluateStyleWritingMode(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "WritingMode", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.WritingModeExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateWritingMode(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A87 RID: 27271 RVA: 0x001AD1D8 File Offset: 0x001AB3D8
		internal string EvaluateStyleLanguage(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Language", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.LanguageExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			CultureInfo cultureInfo;
			return ProcessingValidator.ValidateSpecificLanguage(this.ProcessStringResult(variantResult).Value, this, out cultureInfo);
		}

		// Token: 0x06006A88 RID: 27272 RVA: 0x001AD25C File Offset: 0x001AB45C
		internal string EvaluateStyleUnicodeBiDi(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "UnicodeBiDi", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.UnicodeBiDiExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateUnicodeBiDi(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A89 RID: 27273 RVA: 0x001AD2DC File Offset: 0x001AB4DC
		internal string EvaluateStyleCalendar(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Calendar", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.CalendarExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateCalendar(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A8A RID: 27274 RVA: 0x001AD35C File Offset: 0x001AB55C
		internal string EvaluateStyleNumeralLanguage(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "NumeralLanguage", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.NumeralLanguageExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			CultureInfo cultureInfo;
			return ProcessingValidator.ValidateLanguage(this.ProcessStringResult(variantResult).Value, this, out cultureInfo);
		}

		// Token: 0x06006A8B RID: 27275 RVA: 0x001AD3E0 File Offset: 0x001AB5E0
		internal object EvaluateStyleNumeralVariant(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateIntegerExpression(expression, true, objectType, objectName, "NumeralVariant", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.NumeralVariantExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			IntegerResult integerResult = this.ProcessIntegerResult(variantResult);
			if (integerResult.ErrorOccurred)
			{
				return null;
			}
			return ProcessingValidator.ValidateNumeralVariant(integerResult.Value, this);
		}

		// Token: 0x06006A8C RID: 27276 RVA: 0x001AD470 File Offset: 0x001AB670
		internal string EvaluateStyleBackgroundUrlImageValue(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateIntegerExpression(expression, true, objectType, objectName, "BackgroundImageValue", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BackgroundImageValueExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06006A8D RID: 27277 RVA: 0x001AD4EC File Offset: 0x001AB6EC
		internal string EvaluateStyleBackgroundEmbeddedImageValue(Style style, ExpressionInfo expression, EmbeddedImageHashtable embeddedImages, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateIntegerExpression(expression, true, objectType, objectName, "BackgroundImageValue", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BackgroundImageValueExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateEmbeddedImageName(this.ProcessStringResult(variantResult).Value, embeddedImages, this);
		}

		// Token: 0x06006A8E RID: 27278 RVA: 0x001AD570 File Offset: 0x001AB770
		internal byte[] EvaluateStyleBackgroundDatabaseImageValue(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateIntegerExpression(expression, true, objectType, objectName, "BackgroundImageValue", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BackgroundImageValueExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessBinaryResult(variantResult).Value;
		}

		// Token: 0x06006A8F RID: 27279 RVA: 0x001AD5EC File Offset: 0x001AB7EC
		internal string EvaluateStyleBackgroundImageMIMEType(Style style, ExpressionInfo expression, ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateIntegerExpression(expression, true, objectType, objectName, "BackgroundImageMIMEType", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(expression, ref variantResult))
					{
						Global.Tracer.Assert(style.ExprHost != null);
						variantResult.Value = style.ExprHost.BackgroundImageMIMETypeExpr;
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return ProcessingValidator.ValidateMimeType(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06006A90 RID: 27280 RVA: 0x001AD670 File Offset: 0x001AB870
		private bool EvaluateSimpleExpression(ExpressionInfo expression, ObjectType objectType, string objectName, string propertyName, out VariantResult result)
		{
			this.m_objectType = objectType;
			this.m_objectName = objectName;
			this.m_propertyName = propertyName;
			if (this.m_topLevelReportRuntime != null)
			{
				this.m_topLevelReportRuntime.ObjectType = objectType;
				this.m_topLevelReportRuntime.ObjectName = objectName;
				this.m_topLevelReportRuntime.PropertyName = propertyName;
			}
			return this.EvaluateSimpleExpression(expression, out result);
		}

		// Token: 0x06006A91 RID: 27281 RVA: 0x001AD6CC File Offset: 0x001AB8CC
		private bool EvaluateSimpleExpression(ExpressionInfo expression, out VariantResult result)
		{
			result = default(VariantResult);
			if (expression == null)
			{
				return true;
			}
			switch (expression.Type)
			{
			case ExpressionInfo.Types.Expression:
				return false;
			case ExpressionInfo.Types.Field:
				try
				{
					FieldImpl fieldImpl = this.m_reportObjectModel.FieldsImpl[expression.IntValue];
					if (fieldImpl.IsMissing)
					{
						result.Value = null;
						return true;
					}
					if (fieldImpl.FieldStatus != DataFieldStatus.None)
					{
						result.ErrorOccurred = true;
						result.FieldStatus = fieldImpl.FieldStatus;
						result.ExceptionMessage = fieldImpl.ExceptionMessage;
						result.Value = null;
						return true;
					}
					result.Value = this.m_reportObjectModel.FieldsImpl[expression.IntValue].Value;
				}
				catch (ReportProcessingException_NoRowsFieldAccess reportProcessingException_NoRowsFieldAccess)
				{
					this.RegisterRuntimeWarning(reportProcessingException_NoRowsFieldAccess, this);
					result.Value = null;
					return true;
				}
				return true;
			case ExpressionInfo.Types.Aggregate:
				return false;
			case ExpressionInfo.Types.Constant:
				result.Value = expression.Value;
				return true;
			case ExpressionInfo.Types.Token:
			{
				DataSet dataSet = this.m_reportObjectModel.DataSetsImpl[expression.Value];
				result.Value = dataSet.RewrittenCommandText;
				return true;
			}
			default:
				Global.Tracer.Assert(false);
				return true;
			}
		}

		// Token: 0x06006A92 RID: 27282 RVA: 0x001AD800 File Offset: 0x001ABA00
		private bool EvaluateComplexExpression(ExpressionInfo expression, ref VariantResult result)
		{
			if (expression == null)
			{
				return true;
			}
			ExpressionInfo.Types type = expression.Type;
			if (type == ExpressionInfo.Types.Expression)
			{
				return false;
			}
			if (type == ExpressionInfo.Types.Aggregate)
			{
				result.Value = this.m_reportObjectModel.AggregatesImpl[expression.Value];
				return true;
			}
			Global.Tracer.Assert(false);
			return true;
		}

		// Token: 0x06006A93 RID: 27283 RVA: 0x001AD84C File Offset: 0x001ABA4C
		private void RegisterRuntimeWarning(Exception e, IErrorContext iErrorContext)
		{
			if (e is ReportProcessingException_NoRowsFieldAccess)
			{
				iErrorContext.Register(ProcessingErrorCode.rsRuntimeErrorInExpression, Severity.Warning, new string[] { e.Message });
				return;
			}
			if (Global.Tracer.TraceError)
			{
				Global.Tracer.Trace("Caught unexpected exception inside of RegisterRuntimeWarning.");
			}
			throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
		}

		// Token: 0x06006A94 RID: 27284 RVA: 0x001AD8A2 File Offset: 0x001ABAA2
		private void RegisterRuntimeErrorInExpressionAndStop(ref VariantResult result, Exception e)
		{
			this.RegisterRuntimeErrorInExpression(ref result, e, this, true);
		}

		// Token: 0x06006A95 RID: 27285 RVA: 0x001AD8AE File Offset: 0x001ABAAE
		private void RegisterRuntimeErrorInExpression(ref VariantResult result, Exception e)
		{
			this.RegisterRuntimeErrorInExpression(ref result, e, this, false);
		}

		// Token: 0x06006A96 RID: 27286 RVA: 0x001AD8BC File Offset: 0x001ABABC
		private void RegisterRuntimeErrorInExpression(ref VariantResult result, Exception e, IErrorContext iErrorContext, bool isError)
		{
			if (e is ReportProcessingException_FieldError)
			{
				result.FieldStatus = ((ReportProcessingException_FieldError)e).Status;
				if (DataFieldStatus.IsMissing == result.FieldStatus)
				{
					result = new VariantResult(false, null);
					return;
				}
				result = new VariantResult(true, null);
				return;
			}
			else
			{
				if (e is ReportProcessingException_InvalidOperationException)
				{
					result = new VariantResult(true, null);
					return;
				}
				if (e is ReportProcessingException_UserProfilesDependencies)
				{
					iErrorContext.Register(ProcessingErrorCode.rsRuntimeUserProfileDependency, Severity.Error, null);
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
				string text;
				if (e != null)
				{
					try
					{
						text = ((e.InnerException == null) ? e.Message : e.InnerException.Message);
						goto IL_00AA;
					}
					catch
					{
						text = RPRes.NonClsCompliantException;
						goto IL_00AA;
					}
				}
				text = RPRes.NonClsCompliantException;
				IL_00AA:
				iErrorContext.Register(ProcessingErrorCode.rsRuntimeErrorInExpression, isError ? Severity.Error : Severity.Warning, new string[] { text });
				if (e is ReportProcessingException_NoRowsFieldAccess)
				{
					result = new VariantResult(false, null);
					return;
				}
				if (isError)
				{
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
				result = new VariantResult(true, null);
				return;
			}
		}

		// Token: 0x06006A97 RID: 27287 RVA: 0x001AD9D8 File Offset: 0x001ABBD8
		private bool EvaluateBooleanExpression(ExpressionInfo expression, bool canBeConstant, ObjectType objectType, string objectName, string propertyName, out VariantResult result)
		{
			if (expression != null && expression.Type == ExpressionInfo.Types.Constant)
			{
				result = default(VariantResult);
				if (canBeConstant)
				{
					result.Value = expression.BoolValue;
				}
				else
				{
					result.ErrorOccurred = true;
					this.RegisterInvalidExpressionDataTypeWarning();
				}
				return true;
			}
			return this.EvaluateSimpleExpression(expression, objectType, objectName, propertyName, out result);
		}

		// Token: 0x06006A98 RID: 27288 RVA: 0x001ADA2E File Offset: 0x001ABC2E
		private BooleanResult ProcessBooleanResult(VariantResult result)
		{
			return this.ProcessBooleanResult(result, false, ObjectType.Report, null);
		}

		// Token: 0x06006A99 RID: 27289 RVA: 0x001ADA3C File Offset: 0x001ABC3C
		private BooleanResult ProcessBooleanResult(VariantResult result, bool stopOnError, ObjectType objectType, string objectName)
		{
			BooleanResult booleanResult = default(BooleanResult);
			if (result.ErrorOccurred)
			{
				booleanResult.ErrorOccurred = true;
				if (stopOnError && result.FieldStatus != DataFieldStatus.None)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsFieldErrorInExpression, Severity.Error, objectType, objectName, "Hidden", new string[] { ReportRuntime.GetErrorName(result.FieldStatus, result.ExceptionMessage) });
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
			}
			else if (result.Value is bool)
			{
				booleanResult.Value = (bool)result.Value;
			}
			else if (result.Value == null || DBNull.Value == result.Value)
			{
				booleanResult.Value = false;
			}
			else
			{
				booleanResult.ErrorOccurred = true;
				if (stopOnError)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidExpressionDataType, Severity.Error, objectType, objectName, "Hidden", Array.Empty<string>());
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
				this.RegisterInvalidExpressionDataTypeWarning();
			}
			return booleanResult;
		}

		// Token: 0x06006A9A RID: 27290 RVA: 0x001ADB39 File Offset: 0x001ABD39
		private bool EvaluateBinaryExpression(ExpressionInfo expression, ObjectType objectType, string objectName, string propertyName, out VariantResult result)
		{
			return this.EvaluateNoConstantExpression(expression, objectType, objectName, propertyName, out result);
		}

		// Token: 0x06006A9B RID: 27291 RVA: 0x001ADB48 File Offset: 0x001ABD48
		private BinaryResult ProcessBinaryResult(VariantResult result)
		{
			BinaryResult binaryResult = default(BinaryResult);
			if (result.ErrorOccurred)
			{
				binaryResult.ErrorOccurred = true;
				binaryResult.FieldStatus = result.FieldStatus;
			}
			else if (result.Value is byte[])
			{
				binaryResult.Value = (byte[])result.Value;
			}
			else if (result.Value == null || DBNull.Value == result.Value)
			{
				binaryResult.Value = null;
			}
			else
			{
				binaryResult.ErrorOccurred = true;
				this.RegisterInvalidExpressionDataTypeWarning();
			}
			return binaryResult;
		}

		// Token: 0x06006A9C RID: 27292 RVA: 0x001ADBCC File Offset: 0x001ABDCC
		private StringResult ProcessStringResult(VariantResult result)
		{
			StringResult stringResult = default(StringResult);
			if (result.ErrorOccurred)
			{
				stringResult.ErrorOccurred = true;
				stringResult.FieldStatus = result.FieldStatus;
			}
			else if (result.Value is string)
			{
				stringResult.Value = (string)result.Value;
			}
			else if (result.Value is char)
			{
				stringResult.Value = new string((char)result.Value, 1);
			}
			else if (result.Value == null || DBNull.Value == result.Value)
			{
				stringResult.Value = null;
			}
			else if (result.Value is Guid)
			{
				result.Value = ((Guid)result.Value).ToString();
			}
			else
			{
				stringResult.ErrorOccurred = true;
				this.RegisterInvalidExpressionDataTypeWarning();
			}
			return stringResult;
		}

		// Token: 0x06006A9D RID: 27293 RVA: 0x001ADCAC File Offset: 0x001ABEAC
		private void ProcessLabelResult(ref VariantResult result)
		{
			if (result.ErrorOccurred)
			{
				return;
			}
			if (result.Value is string)
			{
				return;
			}
			if (result.Value is char)
			{
				result.Value = new string((char)result.Value, 1);
				return;
			}
			if (result.Value == null || DBNull.Value == result.Value)
			{
				result.Value = null;
				return;
			}
			if (result.Value is Guid)
			{
				result.Value = ((Guid)result.Value).ToString();
				return;
			}
			result.ErrorOccurred = true;
			this.RegisterInvalidExpressionDataTypeWarning();
		}

		// Token: 0x06006A9E RID: 27294 RVA: 0x001ADD4C File Offset: 0x001ABF4C
		private bool EvaluateIntegerExpression(ExpressionInfo expression, bool canBeConstant, ObjectType objectType, string objectName, string propertyName, out VariantResult result)
		{
			if (expression != null && expression.Type == ExpressionInfo.Types.Constant)
			{
				result = default(VariantResult);
				if (canBeConstant)
				{
					result.Value = expression.IntValue;
				}
				else
				{
					result.ErrorOccurred = true;
					this.RegisterInvalidExpressionDataTypeWarning();
				}
				return true;
			}
			return this.EvaluateSimpleExpression(expression, objectType, objectName, propertyName, out result);
		}

		// Token: 0x06006A9F RID: 27295 RVA: 0x001ADDA4 File Offset: 0x001ABFA4
		private IntegerResult ProcessIntegerResult(VariantResult result)
		{
			IntegerResult integerResult = default(IntegerResult);
			if (result.ErrorOccurred)
			{
				integerResult.ErrorOccurred = true;
				integerResult.FieldStatus = result.FieldStatus;
			}
			else if (result.Value is int)
			{
				integerResult.Value = (int)result.Value;
			}
			else if (result.Value is byte)
			{
				integerResult.Value = Convert.ToInt32((byte)result.Value);
			}
			else if (result.Value is sbyte)
			{
				integerResult.Value = Convert.ToInt32((sbyte)result.Value);
			}
			else if (result.Value is short)
			{
				integerResult.Value = Convert.ToInt32((short)result.Value);
			}
			else if (result.Value is ushort)
			{
				integerResult.Value = Convert.ToInt32((ushort)result.Value);
			}
			else
			{
				if (result.Value is uint)
				{
					try
					{
						integerResult.Value = Convert.ToInt32((uint)result.Value);
						return integerResult;
					}
					catch (OverflowException)
					{
						integerResult.ErrorOccurred = true;
						return integerResult;
					}
				}
				if (result.Value is long)
				{
					try
					{
						integerResult.Value = Convert.ToInt32((long)result.Value);
						return integerResult;
					}
					catch (OverflowException)
					{
						integerResult.ErrorOccurred = true;
						return integerResult;
					}
				}
				if (result.Value is ulong)
				{
					try
					{
						integerResult.Value = Convert.ToInt32((ulong)result.Value);
						return integerResult;
					}
					catch (OverflowException)
					{
						integerResult.ErrorOccurred = true;
						return integerResult;
					}
				}
				if (result.Value is TimeSpan)
				{
					try
					{
						integerResult.Value = Convert.ToInt32(((TimeSpan)result.Value).Ticks);
						return integerResult;
					}
					catch (OverflowException)
					{
						integerResult.ErrorOccurred = true;
						return integerResult;
					}
				}
				integerResult.ErrorOccurred = true;
				this.RegisterInvalidExpressionDataTypeWarning();
			}
			return integerResult;
		}

		// Token: 0x06006AA0 RID: 27296 RVA: 0x001ADFBC File Offset: 0x001AC1BC
		private bool EvaluateIntegerOrFloatExpression(ExpressionInfo expression, ObjectType objectType, string objectName, string propertyName, out VariantResult result)
		{
			return this.EvaluateNoConstantExpression(expression, objectType, objectName, propertyName, out result);
		}

		// Token: 0x06006AA1 RID: 27297 RVA: 0x001ADFCC File Offset: 0x001AC1CC
		private FloatResult ProcessIntegerOrFloatResult(VariantResult result)
		{
			FloatResult floatResult = default(FloatResult);
			if (result.ErrorOccurred)
			{
				floatResult.ErrorOccurred = true;
				floatResult.FieldStatus = result.FieldStatus;
			}
			else if (result.Value is int)
			{
				floatResult.Value = (double)((int)result.Value);
			}
			else if (result.Value is byte)
			{
				floatResult.Value = (double)Convert.ToInt32((byte)result.Value);
			}
			else if (result.Value is sbyte)
			{
				floatResult.Value = (double)Convert.ToInt32((sbyte)result.Value);
			}
			else if (result.Value is short)
			{
				floatResult.Value = (double)Convert.ToInt32((short)result.Value);
			}
			else if (result.Value is ushort)
			{
				floatResult.Value = (double)Convert.ToInt32((ushort)result.Value);
			}
			else
			{
				if (result.Value is uint)
				{
					try
					{
						floatResult.Value = (double)Convert.ToInt32((uint)result.Value);
						return floatResult;
					}
					catch (OverflowException)
					{
						floatResult.ErrorOccurred = true;
						return floatResult;
					}
				}
				if (result.Value is long)
				{
					try
					{
						floatResult.Value = (double)Convert.ToInt32((long)result.Value);
						return floatResult;
					}
					catch (OverflowException)
					{
						floatResult.ErrorOccurred = true;
						return floatResult;
					}
				}
				if (result.Value is ulong)
				{
					try
					{
						floatResult.Value = (double)Convert.ToInt32((ulong)result.Value);
						return floatResult;
					}
					catch (OverflowException)
					{
						floatResult.ErrorOccurred = true;
						return floatResult;
					}
				}
				if (result.Value is TimeSpan)
				{
					try
					{
						floatResult.Value = (double)Convert.ToInt32(((TimeSpan)result.Value).Ticks);
						return floatResult;
					}
					catch (OverflowException)
					{
						floatResult.ErrorOccurred = true;
						return floatResult;
					}
				}
				if (result.Value is double)
				{
					floatResult.Value = (double)result.Value;
				}
				else if (result.Value is float)
				{
					floatResult.Value = Convert.ToDouble((float)result.Value);
				}
				else
				{
					if (result.Value is decimal)
					{
						try
						{
							floatResult.Value = Convert.ToDouble((decimal)result.Value);
							return floatResult;
						}
						catch (OverflowException)
						{
							floatResult.ErrorOccurred = true;
							return floatResult;
						}
					}
					floatResult.ErrorOccurred = true;
					this.RegisterInvalidExpressionDataTypeWarning();
				}
			}
			return floatResult;
		}

		// Token: 0x06006AA2 RID: 27298 RVA: 0x001AE280 File Offset: 0x001AC480
		private void ProcessVariantResult(ExpressionInfo expression, ref VariantResult result)
		{
			this.ProcessVariantResult(expression, ref result, false);
		}

		// Token: 0x06006AA3 RID: 27299 RVA: 0x001AE28C File Offset: 0x001AC48C
		private void ProcessVariantResult(ExpressionInfo expression, ref VariantResult result, bool isArrayAllowed)
		{
			if (expression != null && expression.Type != ExpressionInfo.Types.Constant && !result.ErrorOccurred && !ReportRuntime.IsVariant(result.Value))
			{
				if (result.Value == null || result.Value == DBNull.Value)
				{
					result.Value = null;
					return;
				}
				if (isArrayAllowed && result.Value is ICollection)
				{
					return;
				}
				if (result.Value is Guid)
				{
					result.Value = ((Guid)result.Value).ToString();
					return;
				}
				result.ErrorOccurred = true;
				result.Value = null;
				this.RegisterInvalidExpressionDataTypeWarning();
			}
		}

		// Token: 0x06006AA4 RID: 27300 RVA: 0x001AE330 File Offset: 0x001AC530
		private void ProcessVariantOrBinaryResult(ExpressionInfo expression, ref VariantResult result, IErrorContext iErrorContext, bool isAggregate)
		{
			if (expression != null && expression.Type != ExpressionInfo.Types.Constant && !result.ErrorOccurred && !ReportRuntime.IsVariant(result.Value) && !(result.Value is byte[]))
			{
				if (result.Value == null || result.Value == DBNull.Value)
				{
					result.Value = null;
					return;
				}
				if (result.Value is Guid)
				{
					result.Value = ((Guid)result.Value).ToString();
					return;
				}
				result.ErrorOccurred = true;
				result.Value = null;
				if (!isAggregate)
				{
					this.RegisterInvalidExpressionDataTypeWarning();
				}
			}
		}

		// Token: 0x06006AA5 RID: 27301 RVA: 0x001AE3D4 File Offset: 0x001AC5D4
		private ParameterValueResult ProcessParameterValueResult(ExpressionInfo expression, VariantResult result)
		{
			ParameterValueResult parameterValueResult = default(ParameterValueResult);
			DataAggregate.DataTypeCode dataTypeCode = DataAggregate.DataTypeCode.Null;
			if (result.Value is Guid)
			{
				result.Value = ((Guid)result.Value).ToString();
			}
			if (!(result.Value is object[]))
			{
				dataTypeCode = DataAggregate.GetTypeCode(result.Value);
			}
			if (expression != null)
			{
				if (expression.Type == ExpressionInfo.Types.Constant)
				{
					parameterValueResult.Value = expression.Value;
					parameterValueResult.Type = DataType.String;
				}
				else if (result.ErrorOccurred)
				{
					parameterValueResult.ErrorOccurred = true;
				}
				else if (dataTypeCode == DataAggregate.DataTypeCode.Boolean)
				{
					parameterValueResult.Value = result.Value;
					parameterValueResult.Type = DataType.Boolean;
				}
				else if (dataTypeCode == DataAggregate.DataTypeCode.DateTime)
				{
					parameterValueResult.Value = result.Value;
					parameterValueResult.Type = DataType.DateTime;
				}
				else if (dataTypeCode == DataAggregate.DataTypeCode.Double || dataTypeCode == DataAggregate.DataTypeCode.Single || dataTypeCode == DataAggregate.DataTypeCode.Decimal)
				{
					parameterValueResult.Value = Convert.ToDouble(result.Value, CultureInfo.CurrentCulture);
					parameterValueResult.Type = DataType.Float;
				}
				else if (dataTypeCode == DataAggregate.DataTypeCode.String || dataTypeCode == DataAggregate.DataTypeCode.Char)
				{
					parameterValueResult.Value = Convert.ToString(result.Value, CultureInfo.CurrentCulture);
					parameterValueResult.Type = DataType.String;
				}
				else if (dataTypeCode == DataAggregate.DataTypeCode.Int32 || dataTypeCode == DataAggregate.DataTypeCode.Int16 || dataTypeCode == DataAggregate.DataTypeCode.Byte || dataTypeCode == DataAggregate.DataTypeCode.SByte || dataTypeCode == DataAggregate.DataTypeCode.UInt16)
				{
					parameterValueResult.Value = Convert.ToInt32(result.Value, CultureInfo.CurrentCulture);
					parameterValueResult.Type = DataType.Integer;
				}
				else
				{
					if (dataTypeCode == DataAggregate.DataTypeCode.TimeSpan)
					{
						try
						{
							parameterValueResult.Value = Convert.ToInt32(((TimeSpan)result.Value).Ticks);
							parameterValueResult.Type = DataType.Integer;
							return parameterValueResult;
						}
						catch (OverflowException)
						{
							parameterValueResult.ErrorOccurred = true;
							return parameterValueResult;
						}
					}
					if (dataTypeCode == DataAggregate.DataTypeCode.UInt32 || dataTypeCode == DataAggregate.DataTypeCode.Int64 || dataTypeCode == DataAggregate.DataTypeCode.UInt64)
					{
						try
						{
							parameterValueResult.Value = Convert.ToInt32(result.Value, CultureInfo.CurrentCulture);
							parameterValueResult.Type = DataType.Integer;
							return parameterValueResult;
						}
						catch (OverflowException)
						{
							parameterValueResult.ErrorOccurred = true;
							return parameterValueResult;
						}
					}
					if (result.Value == null || DBNull.Value == result.Value)
					{
						parameterValueResult.Value = null;
						parameterValueResult.Type = DataType.String;
					}
					else if (result.Value is object[])
					{
						parameterValueResult.Value = result.Value;
						object[] array = result.Value as object[];
						Global.Tracer.Assert(array != null);
						parameterValueResult.Type = this.GetDataType(DataAggregate.GetTypeCode(array[0]));
					}
					else
					{
						parameterValueResult.ErrorOccurred = true;
						this.RegisterInvalidExpressionDataTypeWarning();
					}
				}
			}
			return parameterValueResult;
		}

		// Token: 0x06006AA6 RID: 27302 RVA: 0x001AE680 File Offset: 0x001AC880
		private DataType GetDataType(DataAggregate.DataTypeCode typecode)
		{
			switch (typecode)
			{
			case DataAggregate.DataTypeCode.Null:
			case DataAggregate.DataTypeCode.String:
			case DataAggregate.DataTypeCode.Char:
				return DataType.String;
			case DataAggregate.DataTypeCode.Boolean:
				return DataType.Boolean;
			case DataAggregate.DataTypeCode.Int16:
			case DataAggregate.DataTypeCode.Int32:
			case DataAggregate.DataTypeCode.Int64:
			case DataAggregate.DataTypeCode.UInt16:
			case DataAggregate.DataTypeCode.UInt32:
			case DataAggregate.DataTypeCode.UInt64:
			case DataAggregate.DataTypeCode.Byte:
			case DataAggregate.DataTypeCode.SByte:
			case DataAggregate.DataTypeCode.TimeSpan:
				return DataType.Integer;
			case DataAggregate.DataTypeCode.DateTime:
				return DataType.DateTime;
			case DataAggregate.DataTypeCode.Single:
			case DataAggregate.DataTypeCode.Double:
			case DataAggregate.DataTypeCode.Decimal:
				return DataType.Float;
			default:
				return DataType.String;
			}
		}

		// Token: 0x06006AA7 RID: 27303 RVA: 0x001AE6E9 File Offset: 0x001AC8E9
		private bool EvaluateNoConstantExpression(ExpressionInfo expression, ObjectType objectType, string objectName, string propertyName, out VariantResult result)
		{
			if (expression != null && expression.Type == ExpressionInfo.Types.Constant)
			{
				result = new VariantResult(true, null);
				this.RegisterInvalidExpressionDataTypeWarning();
				return true;
			}
			return this.EvaluateSimpleExpression(expression, objectType, objectName, propertyName, out result);
		}

		// Token: 0x06006AA8 RID: 27304 RVA: 0x001AE71C File Offset: 0x001AC91C
		internal static bool IsVariant(object o)
		{
			return o is string || o is int || o is decimal || o is DateTime || o is double || o is float || o is short || o is bool || o is byte || o is TimeSpan || o is sbyte || o is long || o is ushort || o is uint || o is ulong || o is char;
		}

		// Token: 0x06006AA9 RID: 27305 RVA: 0x001AE7AC File Offset: 0x001AC9AC
		private void RegisterInvalidExpressionDataTypeWarning()
		{
			((IErrorContext)this).Register(ProcessingErrorCode.rsInvalidExpressionDataType, Severity.Warning, Array.Empty<string>());
		}

		// Token: 0x06006AAA RID: 27306 RVA: 0x001AE7BF File Offset: 0x001AC9BF
		internal bool InScope(string scope)
		{
			return this.m_currentScope != null && this.m_currentScope.InScope(scope);
		}

		// Token: 0x06006AAB RID: 27307 RVA: 0x001AE7D8 File Offset: 0x001AC9D8
		internal int RecursiveLevel(string scope)
		{
			if (this.m_currentScope == null)
			{
				return 0;
			}
			int num = this.m_currentScope.RecursiveLevel(scope);
			if (-1 == num)
			{
				return 0;
			}
			return num;
		}

		// Token: 0x06006AAC RID: 27308 RVA: 0x001AE803 File Offset: 0x001ACA03
		internal string CreateDrillthroughContext()
		{
			if (this.m_drillthroughContextBuilder == null)
			{
				this.m_drillthroughContextBuilder = new DrillthroughContextBuilder();
			}
			return this.m_drillthroughContextBuilder.CreateDrillthroughContext(this.m_currentActionOwner, this.m_currentScope);
		}

		// Token: 0x06006AAD RID: 27309 RVA: 0x001AE830 File Offset: 0x001ACA30
		[PermissionSet(SecurityAction.Assert, Name = "FullTrust")]
		internal void LoadCompiledCode(Report report, bool parametersOnly, ObjectModelImpl reportObjectModel, ReportRuntimeSetup runtimeSetup)
		{
			Global.Tracer.Assert(report.CompiledCode != null && this.m_exprHostAssembly == null && this.m_reportExprHost == null);
			if (report.CompiledCode.Length != 0)
			{
				ProcessingErrorCode processingErrorCode = ProcessingErrorCode.rsErrorLoadingExprHostAssembly;
				try
				{
					if (runtimeSetup.RequireExpressionHostWithRefusedPermissions && !report.CompiledCodeGeneratedWithRefusedPermissions)
					{
						if (Global.Tracer.TraceError)
						{
							Global.Tracer.Trace("Expression host generated with refused permissions is required.");
						}
						throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
					}
					if (runtimeSetup.ExprHostAppDomain == null || runtimeSetup.ExprHostAppDomain == AppDomain.CurrentDomain)
					{
						if (report.CodeModules != null)
						{
							for (int i = 0; i < report.CodeModules.Count; i++)
							{
								if (!runtimeSetup.CheckCodeModuleIsTrustedInCurrentAppDomain(report.CodeModules[i]))
								{
									this.m_errorContext.Register(ProcessingErrorCode.rsUntrustedCodeModule, Severity.Error, ObjectType.Report, null, null, new string[] { report.CodeModules[i] });
									throw new ReportProcessingException(this.m_errorContext.Messages);
								}
							}
						}
						this.m_reportExprHost = ReportRuntime.ExpressionHostLoader.LoadExprHostIntoCurrentAppDomain(report.CompiledCode, report.ExprHostAssemblyName, runtimeSetup.ExprHostEvidence, parametersOnly, reportObjectModel, report.CodeModules);
					}
					else
					{
						this.m_reportExprHost = ReportRuntime.ExpressionHostLoader.LoadExprHost(report.CompiledCode, report.ExprHostAssemblyName, parametersOnly, reportObjectModel, report.CodeModules, runtimeSetup.ExprHostAppDomain);
					}
					processingErrorCode = ProcessingErrorCode.rsErrorInOnInit;
					this.m_reportExprHost.CustomCodeOnInit();
				}
				catch (RSException)
				{
					throw;
				}
				catch (Exception ex)
				{
					this.ProcessLoadingExprHostException(ex, processingErrorCode);
				}
			}
		}

		// Token: 0x06006AAE RID: 27310 RVA: 0x001AE9E0 File Offset: 0x001ACBE0
		private void ProcessLoadingExprHostException(Exception e, ProcessingErrorCode errorCode)
		{
			if (e != null && e is TargetInvocationException && e.InnerException != null)
			{
				e = e.InnerException;
			}
			string text = null;
			string text2;
			if (e != null)
			{
				try
				{
					text2 = e.Message;
					text = e.ToString();
					goto IL_003F;
				}
				catch
				{
					text2 = RPRes.NonClsCompliantException;
					goto IL_003F;
				}
			}
			text2 = RPRes.NonClsCompliantException;
			IL_003F:
			ProcessingMessage processingMessage = this.m_errorContext.Register(errorCode, Severity.Error, ObjectType.Report, null, null, new string[] { text2 });
			if (Global.Tracer.TraceError && processingMessage != null)
			{
				Global.Tracer.Trace(TraceLevel.Error, processingMessage.Message + Environment.NewLine + text);
			}
			throw new ReportProcessingException(this.m_errorContext.Messages);
		}

		// Token: 0x06006AAF RID: 27311 RVA: 0x001AEA94 File Offset: 0x001ACC94
		internal void Close()
		{
			this.m_reportExprHost = null;
		}

		// Token: 0x040035CD RID: 13773
		private Assembly m_exprHostAssembly;

		// Token: 0x040035CE RID: 13774
		private ReportExprHost m_reportExprHost;

		// Token: 0x040035CF RID: 13775
		private ObjectType m_objectType;

		// Token: 0x040035D0 RID: 13776
		private string m_objectName;

		// Token: 0x040035D1 RID: 13777
		private string m_propertyName;

		// Token: 0x040035D2 RID: 13778
		private ObjectModelImpl m_reportObjectModel;

		// Token: 0x040035D3 RID: 13779
		private ErrorContext m_errorContext;

		// Token: 0x040035D4 RID: 13780
		private ReportProcessing.IScope m_currentScope;

		// Token: 0x040035D5 RID: 13781
		private ReportRuntime m_topLevelReportRuntime;

		// Token: 0x040035D6 RID: 13782
		private DrillthroughContextBuilder m_drillthroughContextBuilder;

		// Token: 0x040035D7 RID: 13783
		private IActionOwner m_currentActionOwner;

		// Token: 0x02000CE1 RID: 3297
		private sealed class ExpressionHostLoader : MarshalByRefObject
		{
			// Token: 0x06008D3B RID: 36155 RVA: 0x0023E950 File Offset: 0x0023CB50
			internal static ReportExprHost LoadExprHost(byte[] exprHostBytes, string exprHostAssemblyName, bool parametersOnly, ObjectModel objectModel, StringList codeModules, AppDomain targetAppDomain)
			{
				Type typeFromHandle = typeof(ReportRuntime.ExpressionHostLoader);
				return ((ReportRuntime.ExpressionHostLoader)Activator.CreateInstance(targetAppDomain, typeFromHandle.Assembly.FullName, typeFromHandle.FullName).Unwrap()).LoadExprHostRemoteEntryPoint(exprHostBytes, exprHostAssemblyName, parametersOnly, objectModel, codeModules);
			}

			// Token: 0x06008D3C RID: 36156 RVA: 0x0023E998 File Offset: 0x0023CB98
			internal static ReportExprHost LoadExprHostIntoCurrentAppDomain(byte[] exprHostBytes, string exprHostAssemblyName, Evidence evidence, bool parametersOnly, ObjectModel objectModel, StringList codeModules)
			{
				if (codeModules != null && 0 < codeModules.Count)
				{
					RevertImpersonationContext.RunFromRestrictedCasContext(delegate
					{
						for (int i = codeModules.Count - 1; i >= 0; i--)
						{
							Assembly.Load(codeModules[i]);
						}
					});
				}
				Assembly assembly = ReportRuntime.ExpressionHostLoader.LoadExprHostAssembly(exprHostBytes, exprHostAssemblyName, evidence);
				Type type = assembly.GetType("ReportExprHostImpl");
				ReportExprHost reportExprHost;
				try
				{
					reportExprHost = (ReportExprHost)type.GetConstructors()[0].Invoke(new object[] { parametersOnly, objectModel });
				}
				catch (Exception ex)
				{
					if (assembly.GetName().Version >= new Version(8, 0, 700))
					{
						throw ex;
					}
					reportExprHost = (ReportExprHost)type.GetConstructors()[0].Invoke(new object[] { parametersOnly });
				}
				return reportExprHost;
			}

			// Token: 0x06008D3D RID: 36157 RVA: 0x0023EA70 File Offset: 0x0023CC70
			private static Assembly LoadExprHostAssembly(byte[] exprHostBytes, string exprHostAssemblyName, Evidence evidence)
			{
				object syncRoot = ReportRuntime.ExpressionHostLoader.ExpressionHosts.SyncRoot;
				Assembly assembly2;
				lock (syncRoot)
				{
					Assembly assembly = (Assembly)ReportRuntime.ExpressionHostLoader.ExpressionHosts[exprHostAssemblyName];
					if (assembly == null)
					{
						if (evidence == null)
						{
							evidence = ReportRuntime.ExpressionHostLoader.CreateDefaultExpressionHostEvidence(exprHostAssemblyName);
						}
						try
						{
							new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Assert();
							assembly = Assembly.Load(exprHostBytes, null, evidence);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
						ReportRuntime.ExpressionHostLoader.ExpressionHosts.Add(exprHostAssemblyName, assembly);
					}
					assembly2 = assembly;
				}
				return assembly2;
			}

			// Token: 0x06008D3E RID: 36158 RVA: 0x0023EB0C File Offset: 0x0023CD0C
			private static Evidence CreateDefaultExpressionHostEvidence(string exprHostAssemblyName)
			{
				Evidence evidence = new Evidence();
				evidence.AddHost(new Zone(SecurityZone.MyComputer));
				evidence.AddHost(new StrongName(new StrongNamePublicKeyBlob(ReportRuntime.ExpressionHostLoader.ReportExpressionsDefaultEvidencePK), exprHostAssemblyName, new Version("1.0.0.0")));
				return evidence;
			}

			// Token: 0x06008D3F RID: 36159 RVA: 0x0023EB3F File Offset: 0x0023CD3F
			private ReportExprHost LoadExprHostRemoteEntryPoint(byte[] exprHostBytes, string exprHostAssemblyName, bool parametersOnly, ObjectModel objectModel, StringList codeModules)
			{
				return ReportRuntime.ExpressionHostLoader.LoadExprHostIntoCurrentAppDomain(exprHostBytes, exprHostAssemblyName, null, parametersOnly, objectModel, codeModules);
			}

			// Token: 0x06008D40 RID: 36160 RVA: 0x0023EB50 File Offset: 0x0023CD50
			static ExpressionHostLoader()
			{
				AppDomain.CurrentDomain.AssemblyResolve += ReportRuntime.ExpressionHostLoader.ResolveAssemblyHandler;
			}

			// Token: 0x06008D41 RID: 36161 RVA: 0x0023EBAB File Offset: 0x0023CDAB
			private static Assembly ResolveAssemblyHandler(object sender, ResolveEventArgs args)
			{
				if (args.Name != null && args.Name.StartsWith("Microsoft.ReportingServices.Processing", StringComparison.Ordinal))
				{
					return ReportRuntime.ExpressionHostLoader.ProcessingObjectModelAssembly;
				}
				return null;
			}

			// Token: 0x04004F3B RID: 20283
			private static readonly Hashtable ExpressionHosts = new Hashtable();

			// Token: 0x04004F3C RID: 20284
			private const string ExprHostRootType = "ReportExprHostImpl";

			// Token: 0x04004F3D RID: 20285
			private static readonly byte[] ReportExpressionsDefaultEvidencePK = new byte[]
			{
				0, 36, 0, 0, 4, 128, 0, 0, 148, 0,
				0, 0, 6, 2, 0, 0, 0, 36, 0, 0,
				82, 83, 65, 49, 0, 4, 0, 0, 1, 0,
				1, 0, 81, 44, 142, 135, 46, 40, 86, 158,
				115, 59, 203, 18, 55, 148, 218, 181, 81, 17,
				160, 87, 11, 59, 61, 77, 227, 121, 65, 83,
				222, 165, 239, 183, 195, 254, 169, 242, 216, 35,
				108, byte.MaxValue, 50, 12, 79, 208, 234, 213, 246, 119,
				136, 11, 246, 193, 129, 242, 150, 199, 81, 197,
				246, 230, 91, 4, 211, 131, 76, 2, 247, 146,
				254, 224, 254, 69, 41, 21, 212, 74, 254, 116,
				160, 194, 126, 13, 142, 75, 141, 4, 236, 82,
				168, 226, 129, 224, 31, 244, 126, 125, 105, 78,
				108, 114, 117, 160, 154, 252, 191, 216, 204, 130,
				112, 90, 6, 178, 15, 214, 239, 97, 235, 186,
				104, 115, 226, 156, 140, 15, 44, 174, 221, 162
			};

			// Token: 0x04004F3E RID: 20286
			private static readonly Assembly ProcessingObjectModelAssembly = typeof(ObjectModel).Assembly;
		}
	}
}
