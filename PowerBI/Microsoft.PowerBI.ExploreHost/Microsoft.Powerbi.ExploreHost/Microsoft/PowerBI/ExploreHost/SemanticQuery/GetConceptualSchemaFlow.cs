using System;
using System.Collections.Generic;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.Errors;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.PowerBI.Query.Contracts;
using Microsoft.ReportingServices.Common;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x02000040 RID: 64
	internal sealed class GetConceptualSchemaFlow : ExploreClientHandlerBaseFlow
	{
		// Token: 0x06000215 RID: 533 RVA: 0x000068F6 File Offset: 0x00004AF6
		internal GetConceptualSchemaFlow(string jsonRequest, string databaseID, ExploreClientHandlerContext context, IModel model, bool isExtendable, TranslationsBehavior translationsBehavior)
			: base(context, databaseID)
		{
			this.jsonRequest = jsonRequest;
			if (model != null)
			{
				this.clientConceptualSchemaHelper = new ClientConceptualSchemaExtender(model, this.GetModelLocation());
			}
			this.isExtendable = isExtendable;
			this.translationsBehavior = translationsBehavior;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000692E File Offset: 0x00004B2E
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00006936 File Offset: 0x00004B36
		public string SerializedConceptualSchema { get; private set; }

		// Token: 0x06000218 RID: 536 RVA: 0x00006940 File Offset: 0x00004B40
		protected override void InternalRun()
		{
			ExploreHostServiceErrorExtractorFactory instance = ExploreHostServiceErrorExtractorFactory.Instance;
			ConceptualSchemasRequest conceptualSchemasRequest = null;
			try
			{
				conceptualSchemasRequest = this.DeserializeConceptualSchemasRequest();
			}
			catch (ArgumentException ex)
			{
				ExploreHostUtils.TraceClientRequestStreamException(ex);
				this.SerializedConceptualSchema = GetConceptualSchemaFlow.GetConceptualSchemaResponseFromException(ex, instance.Create(), ServiceErrorStatusCode.GeneralError, 0L, null);
				return;
			}
			long num = conceptualSchemasRequest.ModelIds[0];
			string text = "2.0";
			IConceptualSchema conceptualSchema;
			ServiceError serviceError;
			bool flag = ExploreHostUtils.TryGetConceptualSchema(this.Context, num, instance.Create(), base.DatabaseID, text, new TranslationsBehavior?(this.translationsBehavior), out conceptualSchema, out serviceError);
			try
			{
				ConceptualSchemaInfo conceptualSchemaInfo = new ConceptualSchemaInfo
				{
					ModelId = num
				};
				if (!flag)
				{
					conceptualSchemaInfo.Error = serviceError;
				}
				else
				{
					bool flag2 = this.GetModelLocation() == ModelLocation.Internal;
					conceptualSchemaInfo.Schema = ClientConceptualSchemaFactory.Create(conceptualSchema, flag2, this.clientConceptualSchemaHelper, this.isExtendable);
				}
				this.SerializedConceptualSchema = GetConceptualSchemaFlow.SerializeConceptualSchema(conceptualSchemaInfo);
			}
			catch (ConceptualSchemaCreationException ex2)
			{
				ExploreHostUtils.TraceGetConceptualSchemaException(ex2);
				throw;
			}
			catch (Exception ex3)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex3))
				{
					throw;
				}
				ExploreHostUtils.TraceGetConceptualSchemaException(ex3);
				throw new ConceptualSchemaCreationException("Unexpected Exception.", ex3, ErrorSource.PowerBI);
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006A64 File Offset: 0x00004C64
		public static string CreateConceptualSchemaResponseFromException(Exception ex, ServiceErrorStatusCode statusCode, string powerBIErrorCode)
		{
			return GetConceptualSchemaFlow.GetConceptualSchemaResponseFromException(ex, ExploreHostServiceErrorExtractorFactory.Instance.Create(), statusCode, 0L, powerBIErrorCode);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00006A7A File Offset: 0x00004C7A
		public static string CreateConceptualSchemaResponseFromException(Exception ex, ServiceErrorStatusCode statusCode, string powerBIErrorCode, IServiceErrorExtractorFactory serviceErrorFactory)
		{
			return GetConceptualSchemaFlow.GetConceptualSchemaResponseFromException(ex, serviceErrorFactory.Create(), statusCode, 0L, powerBIErrorCode);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006A8C File Offset: 0x00004C8C
		private static string GetConceptualSchemaResponseFromException(Exception e, ServiceErrorExtractor extractor, ServiceErrorStatusCode statusCode, long modelID, string powerBIErrorCode = null)
		{
			ConceptualSchemaInfo conceptualSchemaInfo = ConceptualSchemaInfo.CreateFromException(e, extractor, statusCode, modelID, null);
			if (powerBIErrorCode != null)
			{
				conceptualSchemaInfo.Error.PowerBIErrorCode = powerBIErrorCode;
			}
			return GetConceptualSchemaFlow.SerializeConceptualSchema(conceptualSchemaInfo);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006ABC File Offset: 0x00004CBC
		private ConceptualSchemasRequest DeserializeConceptualSchemasRequest()
		{
			if (string.IsNullOrWhiteSpace(this.jsonRequest))
			{
				throw new ArgumentException("SemanticQuery is null or whitespace.");
			}
			ConceptualSchemasRequest conceptualSchemasRequest;
			try
			{
				conceptualSchemasRequest = JsonConvert.DeserializeObject<ConceptualSchemasRequest>(this.jsonRequest);
			}
			catch (JsonException ex)
			{
				throw new ArgumentException("Failed to deserialize ConceptualSchemasRequest from JSON.", ex);
			}
			return conceptualSchemasRequest;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00006B10 File Offset: 0x00004D10
		private static string SerializeConceptualSchema(ConceptualSchemaInfo conceptualSchemaInfo)
		{
			ConceptualSchemas conceptualSchemas = new ConceptualSchemas
			{
				Schemas = new List<ConceptualSchemaInfo>()
			};
			conceptualSchemas.Schemas.Add(conceptualSchemaInfo);
			string text2;
			try
			{
				string text = JsonConvert.SerializeObject(conceptualSchemas);
				if (string.IsNullOrWhiteSpace(text))
				{
					throw new ArgumentException("Newtonsoft failed to serialize ConceptualSchemaInfo.");
				}
				text2 = text;
			}
			catch (JsonException ex)
			{
				throw new ArgumentException("Failed to serialize ConceptualSchemaInfo to JSON.", ex);
			}
			return text2;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006B74 File Offset: 0x00004D74
		private ModelLocation GetModelLocation()
		{
			return this.Context.PowerViewHandler.GetModelLocation(base.DatabaseID);
		}

		// Token: 0x040000CB RID: 203
		private readonly ClientConceptualSchemaExtender clientConceptualSchemaHelper;

		// Token: 0x040000CC RID: 204
		private readonly bool isExtendable;

		// Token: 0x040000CD RID: 205
		private readonly string jsonRequest;

		// Token: 0x040000CE RID: 206
		private readonly TranslationsBehavior translationsBehavior;
	}
}
