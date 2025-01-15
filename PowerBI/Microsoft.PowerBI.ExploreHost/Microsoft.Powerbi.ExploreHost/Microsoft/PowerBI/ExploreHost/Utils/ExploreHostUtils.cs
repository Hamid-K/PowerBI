using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Common;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Edm;
using Microsoft.InfoNav.Experimental.Insights.ServiceContracts.Internal;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.Errors;
using Microsoft.PowerBI.ExploreHost.Lucia;
using Microsoft.PowerBI.Insights.Hosting;
using Microsoft.PowerBI.Query.Contracts;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.Telemetry;
using Microsoft.ReportingServices.Common;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ExploreHost.Utils
{
	// Token: 0x02000034 RID: 52
	internal static class ExploreHostUtils
	{
		// Token: 0x06000178 RID: 376 RVA: 0x0000472D File Offset: 0x0000292D
		internal static void AccumulatedCancelTelemetry(bool cancelIgnored)
		{
			TelemetryService.Instance.AccumulateSummaryBinForEvent("PBI.Win.CancelSemanticQuery", cancelIgnored ? 0 : 1);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004745 File Offset: 0x00002945
		internal static void TraceGetConceptualSchemaException(Exception e)
		{
			ExploreHostUtils.TraceGetConceptualSchemaException(e.StackTrace, e.ToTraceString());
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004758 File Offset: 0x00002958
		internal static void TraceGetConceptualSchemaException(string stackTrace, string traceMessage)
		{
			TelemetryService.Instance.Log(new PBIWinGetConceptualSchemaError(stackTrace, traceMessage));
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000476B File Offset: 0x0000296B
		internal static void TraceClientRequestStreamException(Exception e)
		{
			ExploreHostUtils.TraceClientRequestStreamException(e.StackTrace, e.ToTraceString());
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000477E File Offset: 0x0000297E
		internal static void TraceClientRequestStreamException(string stackTrace, string traceMessage)
		{
			TelemetryService.Instance.Log(new PBIWinClientRequestStreamError(stackTrace, traceMessage));
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004791 File Offset: 0x00002991
		internal static void TraceSemanticQueryException(Exception e)
		{
			ExploreHostUtils.TraceSemanticQueryException(e.StackTrace, e.ToTraceString());
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000047A4 File Offset: 0x000029A4
		internal static void TraceSemanticQueryException(string stackTrace, string traceMessage)
		{
			TelemetryService.Instance.Log(new PBIWinExecuteSemanticQueryException(stackTrace, traceMessage));
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000047B8 File Offset: 0x000029B8
		internal static void TraceDeriveInsightsCompleted(string analysis, DeriveInsightsResult result, bool hasDirectQueryContent)
		{
			Error<string> hostOrAnalysisError = AnalysisResultErrorExtensions.GetHostOrAnalysisError(result);
			TelemetryService.Instance.Log(new PBIWinDeriveInsightsCompleted(analysis, (hostOrAnalysisError != null) ? hostOrAnalysisError.ErrorCode : null, (hostOrAnalysisError != null) ? hostOrAnalysisError.ErrorSource.ToString() : null, hasDirectQueryContent));
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00004803 File Offset: 0x00002A03
		internal static void TraceDeriveInsightsException(Exception e)
		{
			ExploreHostUtils.TraceDeriveInsightsException(e.ToTraceString());
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00004810 File Offset: 0x00002A10
		internal static void TraceDeriveInsightsException(string stackTrace)
		{
			TelemetryService.Instance.Log(new PBIWinDeriveInsightException(stackTrace, string.Empty));
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00004827 File Offset: 0x00002A27
		internal static void TraceInsightsCancellationError(string message, Exception e = null)
		{
			TelemetryService.Instance.Log(new PBIWinDeriveInsightsCancellationException(((e != null) ? e.ToTraceString() : null) ?? string.Empty, message));
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000484E File Offset: 0x00002A4E
		internal static void TraceInterpretUtteranceException(Exception e, LuciaSessionOptions options)
		{
			ExploreHostUtils.TraceInterpretUtteranceException(e.ToTraceString(), options.ToString());
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00004868 File Offset: 0x00002A68
		internal static void TraceInterpretUtteranceException(string stackTrace, string luciaSessionOptions)
		{
			TelemetryService.Instance.Log(new PBIWinInterpretUtteranceException(luciaSessionOptions, stackTrace, string.Empty));
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004880 File Offset: 0x00002A80
		internal static void TraceActivateLuciaSessionException(Exception e, LuciaSessionOptions options)
		{
			ExploreHostUtils.TraceActivateLuciaSessionException(e.ToTraceString(), options.ToString());
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000489A File Offset: 0x00002A9A
		internal static void TraceActivateLuciaSessionException(string stackTrace, string luciaSessionOptions)
		{
			TelemetryService.Instance.Log(new PBIWinActivateLuciaSessionException(luciaSessionOptions, stackTrace, string.Empty));
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000048B2 File Offset: 0x00002AB2
		internal static void TraceVerifyLuciaRuntimeException(Exception e, LuciaSessionOptions options)
		{
			ExploreHostUtils.TraceVerifyLuciaRuntimeException(e.ToTraceString(), options.ToString());
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000048CC File Offset: 0x00002ACC
		internal static void TraceVerifyLuciaRuntimeException(string stackTrace, string luciaSessionOptions)
		{
			TelemetryService.Instance.Log(new PBIWinVerifyLuciaRuntimeException(luciaSessionOptions, stackTrace, string.Empty));
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000048E4 File Offset: 0x00002AE4
		internal static void TraceLuciaModelChangeException(Exception e, LuciaSessionOptions options)
		{
			ExploreHostUtils.TraceLuciaModelChangeException(e.ToTraceString(), options.ToString());
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000048FE File Offset: 0x00002AFE
		internal static void TraceLuciaModelChangeException(string stackTrace, string luciaSessionOptions)
		{
			TelemetryService.Instance.Log(new PBIWinLuciaModelChangeException(luciaSessionOptions, stackTrace, string.Empty));
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00004916 File Offset: 0x00002B16
		internal static void TraceSemanticQueryTranslationException(Exception e)
		{
			ExploreHostUtils.TraceSemanticQueryTranslationException(e.StackTrace, e.ToTraceString());
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00004929 File Offset: 0x00002B29
		internal static void TraceSemanticQueryTranslationException(string stackTrace, string traceMessage)
		{
			TelemetryService.Instance.Log(new PBIWinTranslateSemanticQueryException(stackTrace, traceMessage));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000493C File Offset: 0x00002B3C
		internal static void TraceDataIndexStoreTelemetry(DataIndexStoreTelemetry telemetry)
		{
			TelemetryService instance = TelemetryService.Instance;
			string text = telemetry.ActionId.ToString();
			string text2 = telemetry.Status.ToString();
			string text3 = JsonConvert.SerializeObject(telemetry.Statistics);
			string text4 = telemetry.Warnings.ToString();
			string text5 = telemetry.Message ?? string.Empty;
			Exception exception = telemetry.Exception;
			instance.Log(new PBIWinDataIndexStoreStatistics(text, text2, text3, text4, text5, ((exception != null) ? exception.ToTraceString() : null) ?? string.Empty, telemetry.Duration.ToString(), telemetry.Options.ToString()));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000049F4 File Offset: 0x00002BF4
		internal static void TraceDataIndexBuilderTelemetry(DataIndexBuilderTelemetry telemetry)
		{
			TelemetryService instance = TelemetryService.Instance;
			string text = telemetry.Status.ToString();
			string text2 = JsonConvert.SerializeObject(telemetry.Statistics);
			string text3 = telemetry.Warnings.ToString();
			Exception exception = telemetry.Exception;
			instance.Log(new PBIWinDataIndexBuilderStatistics(text, text2, text3, ((exception != null) ? exception.ToTraceString() : null) ?? string.Empty, telemetry.Options.ToString()));
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00004A72 File Offset: 0x00002C72
		internal static void TraceLuciaSessionDisposalTelemetry(int count, long milliseconds)
		{
			TelemetryService.Instance.Log(new PBIWinLuciaSessionDisposal(count, milliseconds, string.Empty));
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00004A8A File Offset: 0x00002C8A
		internal static void TraceLuciaSessionDisposalTelemetry(Exception ex)
		{
			TelemetryService.Instance.Log(new PBIWinLuciaSessionDisposal(-1, -1L, ex.ToTraceString()));
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00004AA4 File Offset: 0x00002CA4
		internal static DataSet GetSchemaDataSet(IPowerViewHandler powerViewHandler, string databaseID, string schemaName, IReadOnlyDictionary<string, object> restrictions)
		{
			DataSet schemaDataSet;
			try
			{
				schemaDataSet = powerViewHandler.GetSchemaDataSet(databaseID, schemaName, restrictions);
			}
			catch (Exception ex) when (ex is CannotRetrieveModelException)
			{
				throw;
			}
			catch (Exception ex2)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex2))
				{
					throw;
				}
				string text = string.Format(CultureInfo.InvariantCulture, "Cannot retrieve the schema data set due to: {0}", ex2.ToTelemetrySafeString());
				ExploreHostUtils.TraceGetConceptualSchemaException(ex2.ToTraceString(), text);
				throw ExploreHostUtils.BuildCannotRetrieveModelException(ex2, ServiceErrorStatusCode.GetSchemaDataSetError);
			}
			return schemaDataSet;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00004B2C File Offset: 0x00002D2C
		internal static bool TryGetConceptualSchema(ExploreClientHandlerContext context, long modelID, ServiceErrorExtractor extractor, string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior, out IConceptualSchema conceptualSchema, out ServiceError serviceError)
		{
			return ExploreHostUtils.TryGetStructureImpl<IConceptualSchema>(modelID, extractor, () => ExploreHostUtils.GetConceptualSchema(context.PowerViewHandler, databaseID, maxModelMetadataVersion, translationsBehavior), (IConceptualSchema schema) => schema != null, out conceptualSchema, out serviceError);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00004B94 File Offset: 0x00002D94
		internal static bool TryGetSchemaDataSet(ExploreClientHandlerContext context, string schemaName, ServiceErrorExtractor extractor, string databaseID, IReadOnlyDictionary<string, object> restrictions, out DataSet dataset, out ServiceError serviceError)
		{
			dataset = null;
			serviceError = null;
			try
			{
				dataset = ExploreHostUtils.GetSchemaDataSet(context.PowerViewHandler, databaseID, schemaName, restrictions);
				if (dataset != null)
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex) || !extractor.TryExtractServiceError(ex, out serviceError))
				{
					throw;
				}
			}
			if (serviceError == null)
			{
				PowerBIExploreException ex2 = ExploreHostUtils.CreateAndTracePowerBIExploreException("ErrorGettingPerspectives", "DataSet returned null", null, ErrorSource.Unknown, context.PowerViewHandler.GetModelLocation(databaseID), ServiceErrorStatusCode.GeneralError);
				extractor.TryExtractServiceError(ex2, out serviceError);
			}
			serviceError.StatusCode = ServiceErrorStatusCode.GetSchemaDataSetError;
			return false;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004C2C File Offset: 0x00002E2C
		internal static PowerBIExploreException CreateAndTracePowerBIExploreException(string errorCode, string message, Exception innerException, ErrorSource errorSource, ModelLocation modelLocation, ServiceErrorStatusCode statusCode = ServiceErrorStatusCode.GeneralError)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string> { 
			{
				"ModelMode",
				modelLocation.ToString()
			} };
			PowerBIExploreException ex = new PowerBIExploreException(errorCode, message, innerException, errorSource, dictionary, statusCode);
			Exception ex2 = innerException ?? ex;
			if (ex2 != null)
			{
				TelemetryService.Instance.TraceError(ex2.ToTraceString());
			}
			return ex;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00004C80 File Offset: 0x00002E80
		internal static bool TryGetEngineDataModel(ExploreClientHandlerContext context, long modelID, ServiceErrorExtractor extractor, string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior, out EngineDataModel engineDataModel, out ServiceError serviceError, out IDataSourceInfo dataSourceInfo)
		{
			IDataSourceInfo lambdaDataSourceInfo = null;
			bool flag = ExploreHostUtils.TryGetStructureImpl<EngineDataModel>(modelID, extractor, () => ExploreHostUtils.GetEngineDataModel(context.PowerViewHandler, databaseID, maxModelMetadataVersion, translationsBehavior, out lambdaDataSourceInfo), (EngineDataModel model) => model != null && model.Schema != null && model.Model != null, out engineDataModel, out serviceError);
			dataSourceInfo = lambdaDataSourceInfo;
			return flag;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00004CF8 File Offset: 0x00002EF8
		private static bool TryGetStructureImpl<T>(long modelID, ServiceErrorExtractor extractor, Func<T> getStructure, Func<T, bool> validateStructure, out T structure, out ServiceError serviceError)
		{
			structure = default(T);
			serviceError = null;
			try
			{
				structure = getStructure();
				if (validateStructure(structure))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex) || !ExploreHostUtils.TryExtractGetConceptualSchemaServiceError(ex, extractor, ref serviceError))
				{
					ExploreHostUtils.TraceGetConceptualSchemaException(ex);
					throw;
				}
			}
			if (serviceError == null)
			{
				ConceptualSchemaCreationException ex2 = new ConceptualSchemaCreationException(modelID, ErrorSource.PowerBI);
				ExploreHostUtils.TryExtractGetConceptualSchemaServiceError(ex2, extractor, ref serviceError);
				ExploreHostUtils.TraceGetConceptualSchemaException(ex2);
			}
			return false;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00004D80 File Offset: 0x00002F80
		private static bool TryExtractGetConceptualSchemaServiceError(Exception e, ServiceErrorExtractor extractor, ref ServiceError serviceError)
		{
			ServiceErrorStatusCode serviceErrorStatusCode;
			if (e is CannotRetrieveModelException)
			{
				serviceErrorStatusCode = ServiceErrorStatusCode.CsdlFetching;
			}
			else
			{
				if (!(e is ConceptualSchemaCreationException))
				{
					PowerBIExploreException ex = e as PowerBIExploreException;
					if (ex == null || !(ex.ErrorCode == "DataShapeEngineModelCreationFailed"))
					{
						return false;
					}
				}
				serviceErrorStatusCode = ServiceErrorStatusCode.CsdlConvertXmlToConceptualSchema;
			}
			extractor.TryExtractServiceError(e, out serviceError);
			serviceError.StatusCode = serviceErrorStatusCode;
			return true;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00004DD8 File Offset: 0x00002FD8
		internal static IConceptualSchema GetConceptualSchema(IPowerViewHandler powerViewHandler, string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior)
		{
			ModelDaxCapabilities modelDaxCapabilities;
			return ExploreHostUtils.GetConceptualSchema(powerViewHandler, databaseID, maxModelMetadataVersion, translationsBehavior, out modelDaxCapabilities);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00004DF0 File Offset: 0x00002FF0
		internal static IConceptualSchema GetConceptualSchema(IPowerViewHandler powerViewHandler, string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior, out ModelDaxCapabilities daxCapabilities)
		{
			return ExploreHostUtils.GetConceptualSchemaAndHandleExceptions(delegate
			{
				IPowerViewHandler powerViewHandler2 = powerViewHandler;
				string databaseID2 = databaseID;
				string maxModelMetadataVersion2 = maxModelMetadataVersion;
				TranslationsBehavior? translationsBehavior2 = translationsBehavior;
				ParseConceptualSchema parseConceptualSchema;
				if ((parseConceptualSchema = ExploreHostUtils.<>O.<0>__ParseConceptualSchema) == null)
				{
					parseConceptualSchema = (ExploreHostUtils.<>O.<0>__ParseConceptualSchema = new ParseConceptualSchema(ExploreHostUtils.ParseConceptualSchema));
				}
				return powerViewHandler2.GetConceptualSchema(databaseID2, maxModelMetadataVersion2, translationsBehavior2, parseConceptualSchema);
			}, maxModelMetadataVersion, out daxCapabilities);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004E38 File Offset: 0x00003038
		internal static IConceptualSchema GetConceptualSchema(IPowerViewHandler powerViewHandler, ASConnectionInfo connectionInfo, string maxModelMetadataVersion, TranslationsBehavior translationsBehavior)
		{
			ModelDaxCapabilities modelDaxCapabilities;
			return ExploreHostUtils.GetConceptualSchemaAndHandleExceptions(delegate
			{
				IPowerViewHandler powerViewHandler2 = powerViewHandler;
				ASConnectionInfo connectionInfo2 = connectionInfo;
				string maxModelMetadataVersion2 = maxModelMetadataVersion;
				TranslationsBehavior translationsBehavior2 = translationsBehavior;
				ParseConceptualSchema parseConceptualSchema;
				if ((parseConceptualSchema = ExploreHostUtils.<>O.<0>__ParseConceptualSchema) == null)
				{
					parseConceptualSchema = (ExploreHostUtils.<>O.<0>__ParseConceptualSchema = new ParseConceptualSchema(ExploreHostUtils.ParseConceptualSchema));
				}
				return powerViewHandler2.GetConceptualSchema(connectionInfo2, maxModelMetadataVersion2, translationsBehavior2, parseConceptualSchema);
			}, maxModelMetadataVersion, out modelDaxCapabilities);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00004E80 File Offset: 0x00003080
		internal static EngineDataModel GetEngineDataModel(IPowerViewHandler powerViewHandler, string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior)
		{
			IDataSourceInfo dataSourceInfo;
			return ExploreHostUtils.GetEngineDataModel(powerViewHandler, databaseID, maxModelMetadataVersion, translationsBehavior, out dataSourceInfo);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00004E98 File Offset: 0x00003098
		internal static EngineDataModel GetEngineDataModel(IPowerViewHandler powerViewHandler, string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior, out IDataSourceInfo dataSourceInfo)
		{
			dataSourceInfo = powerViewHandler.GetDataSourceInfo(databaseID);
			return ExploreHostUtils.GetEngineDataModelAndHandleExceptions(powerViewHandler, databaseID, maxModelMetadataVersion, translationsBehavior);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00004EAD File Offset: 0x000030AD
		private static EngineDataModel GetEngineDataModelAndHandleExceptions(IPowerViewHandler powerViewHandler, string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior)
		{
			return ExploreHostUtils.HandleModelRetrievalExceptions<EngineDataModel>(() => ExploreHostUtils.GetEngineDataModelImpl(powerViewHandler, databaseID, maxModelMetadataVersion, translationsBehavior), ServiceErrorStatusCode.CsdlFetching);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00004EE4 File Offset: 0x000030E4
		private static IConceptualSchema GetConceptualSchemaAndHandleExceptions(Func<ConceptualSchemaAndCapabilities> getConceptualSchema, string maxModelMetadataVersion, out ModelDaxCapabilities daxCapabilities)
		{
			ConceptualSchemaAndCapabilities conceptualSchemaAndCapabilities = ExploreHostUtils.HandleModelRetrievalExceptions<ConceptualSchemaAndCapabilities>(getConceptualSchema, ServiceErrorStatusCode.CsdlFetching);
			daxCapabilities = conceptualSchemaAndCapabilities.Capabilities;
			return conceptualSchemaAndCapabilities.ConceptualSchema;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00004F08 File Offset: 0x00003108
		private static T HandleModelRetrievalExceptions<T>(Func<T> wrappedCall, ServiceErrorStatusCode statusCode)
		{
			T t;
			try
			{
				t = wrappedCall();
			}
			catch (Exception ex) when (ex is CannotRetrieveModelException || ex is PowerBIExploreException)
			{
				throw;
			}
			catch (Exception ex2)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex2))
				{
					throw;
				}
				string text = string.Format(CultureInfo.InvariantCulture, "Cannot retrieve the data model due to: {0}", ex2.ToTelemetrySafeString());
				ExploreHostUtils.TraceGetConceptualSchemaException(ex2.ToTraceString(), text);
				throw ExploreHostUtils.BuildCannotRetrieveModelException(ex2, statusCode);
			}
			return t;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00004F9C File Offset: 0x0000319C
		private static Exception BuildCannotRetrieveModelException(Exception e, ServiceErrorStatusCode statusCode)
		{
			return new CannotRetrieveModelException(e, ErrorSource.Unknown, statusCode, null);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00004FBA File Offset: 0x000031BA
		private static EngineDataModel GetEngineDataModelImpl(IPowerViewHandler powerViewHandler, string databaseID, string maxModelMetadataVersion, TranslationsBehavior? translationsBehavior)
		{
			Func<Stream, IFeatureSwitchProvider, EngineDataModel> func;
			if ((func = ExploreHostUtils.<>O.<1>__GetEngineDataModel) == null)
			{
				func = (ExploreHostUtils.<>O.<1>__GetEngineDataModel = new Func<Stream, IFeatureSwitchProvider, EngineDataModel>(ExploreHostUtils.GetEngineDataModel));
			}
			return powerViewHandler.GetEngineDataModel(databaseID, maxModelMetadataVersion, translationsBehavior, func);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00004FE0 File Offset: 0x000031E0
		private static EngineDataModel GetEngineDataModel(Stream modelStream, IFeatureSwitchProvider featureSwitchProvider)
		{
			EngineDataModel engineDataModel;
			try
			{
				engineDataModel = (featureSwitchProvider.IsEnabled(FeatureSwitchKind.ConceptualSchema) ? EngineDataModelParser.Parse(modelStream, featureSwitchProvider, DataShapingTracer.Instance) : EngineDataModelParser.Parse(modelStream, featureSwitchProvider));
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				string text = string.Format(CultureInfo.InvariantCulture, "{0} due to: {1}", "Failed to parse CSDL into ConceptualSchema", ex.ToTelemetrySafeString());
				ExploreHostUtils.TraceGetConceptualSchemaException(ex.ToTraceString(), text);
				throw new PowerBIExploreException("DataShapeEngineModelCreationFailed", "Failed to parse CSDL into ConceptualSchema", ex, ErrorSource.PowerBI, ServiceErrorStatusCode.GeneralError);
			}
			return engineDataModel;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005068 File Offset: 0x00003268
		internal static IConceptualSchema ParseConceptualSchema(Stream dataModel, ConceptualSchemaBuilderOptions schemaBuilderOptions, out ModelDaxCapabilities daxCapabilities)
		{
			IConceptualSchema conceptualSchema;
			try
			{
				global::System.Version version;
				IList<ConceptualSchemaLoadError> list;
				if (!ConceptualSchemaBuilder.TryCreateConceptualSchema(new List<XmlReader> { ConceptualSchemaUtils.FillAssociationSetEndRoleAttribute(XmlReader.Create(dataModel, XmlUtil.CreateSafeXmlReaderSettings()), out version) }, ExploreTracer.Instance, version, schemaBuilderOptions, out conceptualSchema, out daxCapabilities, out list))
				{
					string text = "Failed to parse CSDL into ConceptualSchema";
					if (list != null && list.Count > 0)
					{
						IEnumerable<string> enumerable = list.Select((ConceptualSchemaLoadError e) => string.Format(CultureInfo.CurrentCulture, "{0}: {1}", e.ErrorCode, e.Message));
						text = string.Format("{0} due to: {1}", "Failed to parse CSDL into ConceptualSchema", string.Join(Environment.NewLine, enumerable));
					}
					throw new ConceptualSchemaCreationException(text, ErrorSource.PowerBI);
				}
			}
			catch (PowerBIExploreException ex)
			{
				ExploreHostUtils.TraceGetConceptualSchemaException(ex);
				throw;
			}
			catch (Exception ex2)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex2))
				{
					throw;
				}
				string text2 = string.Format(CultureInfo.InvariantCulture, "{0} due to: {1}", "Failed to parse CSDL into ConceptualSchema", ex2.ToTelemetrySafeString());
				ExploreHostUtils.TraceGetConceptualSchemaException(ex2.ToTraceString(), text2);
				throw new ConceptualSchemaCreationException("Failed to parse CSDL into ConceptualSchema", ex2, ErrorSource.PowerBI);
			}
			return conceptualSchema;
		}

		// Token: 0x0400009B RID: 155
		private const string CsdlParseErrorMessage = "Failed to parse CSDL into ConceptualSchema";

		// Token: 0x020000A0 RID: 160
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000209 RID: 521
			public static ParseConceptualSchema <0>__ParseConceptualSchema;

			// Token: 0x0400020A RID: 522
			public static Func<Stream, IFeatureSwitchProvider, EngineDataModel> <1>__GetEngineDataModel;
		}
	}
}
