using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000892 RID: 2194
	internal static class ODataResponse
	{
		// Token: 0x06003F03 RID: 16131 RVA: 0x000CE808 File Offset: 0x000CCA08
		public static Value GetResponseValue(Uri uri, ODataEnvironment environment)
		{
			Value value;
			try
			{
				using (HttpResponseData response = ODataResponse.GetResponse(environment.HttpResource, environment.ServiceUri, uri, environment.Headers, environment.Credentials, "application/json;odata.metadata=minimal", true, environment.Host, environment.Settings, environment.UserSettings))
				{
					string text = response.TryGetContextUrl();
					ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = ODataResponse.ParseODataContextUri(environment.Settings.EdmModel, text, uri);
					value = ODataResponse.CreateValueFromPayload(environment, uri, response, odataJsonLightContextUriParseResult);
				}
			}
			catch (WebException ex)
			{
				throw ODataCommonErrors.RequestFailed(environment.Host, ex, uri, environment.HttpResource);
			}
			return value;
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x000CE8B0 File Offset: 0x000CCAB0
		public static Value Create(Uri requestUri, HttpResponseData responseData, HttpResource resource, Value headers, ResourceCredentialCollection credentials, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings)
		{
			string text = responseData.TryGetContextUrl();
			Uri uri2;
			Uri uri = ODataResponse.BuildMetadataAndServiceDocumentUri(text, out uri2);
			settings.EdmModel = ODataResponse.GetMetadataDocument(resource, uri, headers, credentials, host, settings, userSettings);
			ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = ODataResponse.ParseODataContextUri(settings.EdmModel, text, requestUri);
			ServiceDocumentWrapper serviceDocumentWrapper;
			if (odataJsonLightContextUriParseResult.PayloadKind == ODataPayloadKind.ServiceDocument)
			{
				serviceDocumentWrapper = responseData.GetServiceDocument(settings.EdmModel, requestUri);
			}
			else
			{
				serviceDocumentWrapper = new ServiceDocumentWrapper(uri2);
			}
			return ODataResponse.CreateValueFromPayload(ODataEnvironment.Create(serviceDocumentWrapper, uri, headers, resource, requestUri, credentials, host, settings, userSettings, false), requestUri, responseData, odataJsonLightContextUriParseResult);
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x000CE934 File Offset: 0x000CCB34
		public static Value CreateValueFromPayload(ODataEnvironment environment, Uri uri, HttpResponseData responseData, ODataJsonLightContextUriParseResult parseResult)
		{
			switch (parseResult.PayloadKind)
			{
			case ODataPayloadKind.Feed:
			{
				RecordTypeValue asRecordType = environment.EdmTypeValueLookup[parseResult.EdmType].AsRecordType;
				TableValue tableValue;
				if (environment.TryCreateQueryTableValue(uri, asRecordType, out tableValue))
				{
					return tableValue;
				}
				Microsoft.OData.Edm.IEdmEntityType edmEntityType = (Microsoft.OData.Edm.IEdmEntityType)parseResult.EdmType;
				Keys keys = asRecordType.Fields.AsRecord.Keys;
				List<int> list = new List<int>();
				foreach (Microsoft.OData.Edm.IEdmStructuralProperty edmStructuralProperty in edmEntityType.Key())
				{
					int num = keys.IndexOfKey(edmStructuralProperty.Name);
					if (num == -1)
					{
						num = keys.IndexOfKey(EdmNameEncoder.Decode(edmStructuralProperty.Name));
					}
					list.Add(num);
				}
				return new FeedTableValue(environment, uri, asRecordType, new TableKey[]
				{
					new TableKey(list.ToArray(), true)
				});
			}
			case ODataPayloadKind.Entry:
			{
				TypeValue typeValue = environment.EdmTypeValueLookup[parseResult.EdmType];
				return ODataReaderEnumerator.CreateEntryResultFromResponse(environment, responseData, typeValue, uri);
			}
			case ODataPayloadKind.Property:
			case ODataPayloadKind.IndividualProperty:
				return responseData.GetPropertyResult(environment.Settings.EdmModel, uri, environment.Resource.Kind, TypeValue.Any, environment);
			case ODataPayloadKind.Value:
				return responseData.GetRawResult(uri, environment.Resource.Kind, TypeValue.Any, environment);
			case ODataPayloadKind.BinaryValue:
				return responseData.GetRawResult(uri, environment.Resource.Kind, TypeValue.Binary, environment);
			case ODataPayloadKind.Collection:
				return responseData.GetCollectionResult(environment.Settings.EdmModel, uri, environment.Resource.Kind, TypeValue.Any, environment);
			case ODataPayloadKind.ServiceDocument:
				return environment.CreateCatalogTableValue(environment.Host);
			case ODataPayloadKind.Error:
				throw responseData.GetErrorResult(environment.Host, uri, environment.Resource.Kind);
			}
			throw ODataCommonErrors.UnsupportedPayload(parseResult.PayloadKind.ToString());
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x000CEB3C File Offset: 0x000CCD3C
		private static Microsoft.OData.Edm.IEdmModel GetMetadataDocument(HttpResource resource, Uri uri, Value headers, ResourceCredentialCollection credentials, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings)
		{
			Microsoft.OData.Edm.IEdmModel metadataDocument;
			try
			{
				using (HttpResponseData response = ODataResponse.GetResponse(resource, null, uri, headers, credentials, "application/xml", true, host, settings, userSettings))
				{
					metadataDocument = response.GetMetadataDocument(host, resource);
				}
			}
			catch (WebException ex)
			{
				MashupHttpWebResponse mashupHttpWebResponse = ex.Response as MashupHttpWebResponse;
				if (mashupHttpWebResponse != null && mashupHttpWebResponse.StatusCode == HttpStatusCode.NotFound)
				{
					throw ODataCommonErrors.MissingMetadataDocument(ex);
				}
				throw ODataCommonErrors.RequestFailed(host, ex, uri, resource);
			}
			return metadataDocument;
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x000CEBC4 File Offset: 0x000CCDC4
		public static ODataJsonLightContextUriParseResult ParseODataContextUri(Microsoft.OData.Edm.IEdmModel model, string odataContext, Uri requestUri)
		{
			ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult;
			try
			{
				odataJsonLightContextUriParseResult = ODataJsonLightContextUriParser.Parse(model, odataContext, true);
			}
			catch (ODataException ex)
			{
				throw ODataCommonErrors.ODataContextUriIsNotValid(ex, requestUri.AbsoluteUri);
			}
			return odataJsonLightContextUriParseResult;
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x000CEBFC File Offset: 0x000CCDFC
		private static Uri BuildMetadataAndServiceDocumentUri(string contextUrl, out Uri serviceDocumentUri)
		{
			UriBuilder uriBuilder;
			try
			{
				uriBuilder = new UriBuilder(contextUrl)
				{
					Fragment = null
				};
			}
			catch (UriFormatException ex)
			{
				throw ODataCommonErrors.ODataContextUriIsNotValid(ex, contextUrl);
			}
			Uri uri = uriBuilder.Uri;
			string text = uri.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped);
			for (int i = 0; i < uri.Segments.Length - 1; i++)
			{
				text += uri.Segments[i];
			}
			text = text.Trim(new char[] { '/' });
			serviceDocumentUri = new Uri(text);
			return uri;
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x000CEC80 File Offset: 0x000CCE80
		private static HttpResponseData GetResponse(HttpResource resource, Uri serviceUri, Uri uri, Value headers, ResourceCredentialCollection credentials, string contentType, bool throwOnBadRequest, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings)
		{
			return HttpResponseHandler.GetResponseStream(resource, serviceUri, uri, headers, credentials, contentType, throwOnBadRequest, false, host, settings, userSettings, settings.ServerVersion, true);
		}

		// Token: 0x0400211A RID: 8474
		public static readonly ODataMessageReaderSettings DefaultReaderSettings = new ODataMessageReaderSettings
		{
			EnableAtomMetadataReading = true,
			MaxProtocolVersion = ODataVersion.V4,
			MessageQuotas = new ODataMessageQuotas
			{
				MaxReceivedMessageSize = long.MaxValue
			}
		};
	}
}
