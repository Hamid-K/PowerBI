using System;
using System.IO;
using System.Net;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.ContextUrl;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x02000776 RID: 1910
	internal static class ODataResponse
	{
		// Token: 0x06003838 RID: 14392 RVA: 0x000B3F6C File Offset: 0x000B216C
		public static IODataPayloadReader CreateResponseReader(IEngineHost engineHost, HttpResource resource, Uri requestUri, HttpResponseData httpResponseData, Microsoft.OData.Edm.IEdmModel model = null)
		{
			return new ODataHttpResponseReader(engineHost, resource, requestUri, httpResponseData, model);
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000B3F7C File Offset: 0x000B217C
		public static Value GetResponseValue(Uri uri, ODataEnvironment environment, TypeValue typeValue = null)
		{
			Value value;
			try
			{
				HttpResponseData response = ODataResponse.GetResponse(environment.HttpResource, environment.ServiceUri, uri, environment.Headers, environment.Credentials, "application/json;odata.metadata=minimal", true, environment.Host, environment.Settings, environment.UserSettings);
				using (IODataPayloadReader iodataPayloadReader = ODataResponse.CreateResponseReader(environment.Host, environment.HttpResource, uri, response, environment.EdmModel))
				{
					ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = ODataResponse.ParseODataContextUri(environment.Settings.EdmModel, iodataPayloadReader.ContextUrl, uri);
					value = ODataResponse.CreateValueFromPayload(environment, uri, iodataPayloadReader, odataJsonLightContextUriParseResult, typeValue);
				}
			}
			catch (WebException ex)
			{
				throw ODataCommonErrors.RequestFailed(environment.Host, ex, uri, environment.HttpResource);
			}
			return value;
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000B4040 File Offset: 0x000B2240
		public static Value Create(Uri requestUri, HttpResponseData responseData, HttpResource resource, Value headers, ResourceCredentialCollection credentials, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings)
		{
			Value value;
			using (ODataHttpResponseReader odataHttpResponseReader = new ODataHttpResponseReader(host, resource, requestUri, responseData, null))
			{
				string contextUrl = odataHttpResponseReader.ContextUrl;
				Uri uri2;
				Uri uri = ODataResponse.BuildMetadataAndServiceDocumentUri(contextUrl, out uri2);
				ODataResponse.SizedEdmModel metadataDocument = ODataResponse.GetMetadataDocument(resource, uri, headers, credentials, host, settings, userSettings);
				settings.EdmModel = metadataDocument.Model;
				settings.EdmModelSize = metadataDocument.Size;
				ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = ODataResponse.ParseODataContextUri(settings.EdmModel, contextUrl, requestUri);
				ODataEnvironment odataEnvironment = ODataEnvironment.Create(uri2, uri, headers, resource, requestUri, credentials, host, settings, userSettings, false);
				odataHttpResponseReader.EdmModel = settings.EdmModel;
				value = ODataResponse.CreateValueFromPayload(odataEnvironment, requestUri, odataHttpResponseReader, odataJsonLightContextUriParseResult, null);
			}
			return value;
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x000B40F4 File Offset: 0x000B22F4
		public static TypeValue GetTypeValueFromPayload(ODataEnvironment environment, IODataPayloadReader reader, ODataJsonLightContextUriParseResult parseResult)
		{
			switch (parseResult.PayloadKind)
			{
			case ODataPayloadKind.ResourceSet:
			case ODataPayloadKind.Resource:
			{
				Microsoft.OData.Edm.IEdmType edmType = parseResult.EdmType;
				if (parseResult.PayloadKind == ODataPayloadKind.ResourceSet || parseResult.NavigationSource is Microsoft.OData.Edm.IEdmSingleton)
				{
					edmType = new EdmCollectionType(edmType.ToTypeReference(false));
				}
				TypeValue typeValue = environment.ConvertType(edmType);
				if (parseResult.SelectItems != null)
				{
					return ContextUrlTypeAlgebra.Select(typeValue, environment.EdmModel, edmType, parseResult.SelectItems);
				}
				return typeValue;
			}
			case ODataPayloadKind.Property:
			case ODataPayloadKind.Error:
			case ODataPayloadKind.IndividualProperty:
				return TypeValue.Any;
			case ODataPayloadKind.Value:
				return TypeValue.Any;
			case ODataPayloadKind.BinaryValue:
				return TypeValue.Binary;
			case ODataPayloadKind.Collection:
				return TypeValue.List;
			case ODataPayloadKind.ServiceDocument:
				return TypeValue.Table;
			}
			throw ODataCommonErrors.UnsupportedPayload(parseResult.PayloadKind.ToString());
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000B41E0 File Offset: 0x000B23E0
		public static Value CreateValueFromPayload(ODataEnvironment environment, Uri uri, IODataPayloadReader reader, ODataJsonLightContextUriParseResult parseResult, TypeValue typeValue = null)
		{
			if (typeValue == null || typeValue.Equals(TypeValue.Any))
			{
				typeValue = ODataResponse.GetTypeValueFromPayload(environment, reader, parseResult);
			}
			switch (parseResult.PayloadKind)
			{
			case ODataPayloadKind.ResourceSet:
			{
				IODataPayloadReader iodataPayloadReader = reader.TransferOwnership();
				environment.Host.QueryService<ILifetimeService>().Register(iodataPayloadReader);
				return ODataValue.CreateResourceSetValueReference(new ODataReaderEnumerable(environment, uri, ODataTypeServices.GetItemType(typeValue), true, parseResult.NavigationSource, iodataPayloadReader), typeValue).Value;
			}
			case ODataPayloadKind.Resource:
			{
				IValueReference resourceValueReference = ODataValue.GetResourceValueReference(environment, new GetReader(environment.GetRequestReader), reader.ToResourceReader(false), ODataTypeServices.GetItemType(typeValue), parseResult.NavigationSource);
				if (typeValue.IsListType || typeValue.IsTableType)
				{
					return ODataValue.CreateResourceSetValueReference(new IValueReference[] { resourceValueReference }, typeValue).Value;
				}
				return resourceValueReference.Value;
			}
			case ODataPayloadKind.Property:
			case ODataPayloadKind.IndividualProperty:
				return ODataValue.CreatePropertyValue(environment, reader.ReadProperty(), TypeValue.Any);
			case ODataPayloadKind.Value:
				return ODataValue.CreatePrimitiveValue(environment, reader.ReadPrimitive(TypeValue.Any), TypeValue.Any);
			case ODataPayloadKind.BinaryValue:
				return ODataValue.CreatePrimitiveValue(environment, reader.ReadPrimitive(TypeValue.Binary), TypeValue.Binary);
			case ODataPayloadKind.Collection:
				return ODataValue.CreateCollectionValue(environment, reader.ReadCollection(), typeValue.AsListType.ItemType);
			case ODataPayloadKind.ServiceDocument:
				return ODataValue.CreateServiceDocumentValue(environment, reader.ReadServiceDocument());
			case ODataPayloadKind.Error:
				throw ODataValue.CreateErrorValueException(environment.Resource.Kind, uri, reader.ReadError());
			}
			throw ODataCommonErrors.UnsupportedPayload(parseResult.PayloadKind.ToString());
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000B437C File Offset: 0x000B257C
		private static ODataResponse.SizedEdmModel GetMetadataDocument(HttpResource resource, Uri uri, Value headers, ResourceCredentialCollection credentials, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings)
		{
			ODataRequest odataRequest = new ODataRequest(resource, null, uri, headers, credentials, "application/xml", true, false, host, settings, userSettings, settings.ServerVersion, true);
			IObjectCache objectCache = host.QueryService<ICacheSets>().Data.ObjectCache;
			PersistentCacheKeyBuilder persistentCacheKeyBuilder = new PersistentCacheKeyBuilder();
			persistentCacheKeyBuilder.Add(userSettings.Fingerprint);
			persistentCacheKeyBuilder.Add("SizedEdmModel");
			string cacheKey = odataRequest.GetCacheKey(persistentCacheKeyBuilder.ToString());
			object obj;
			if (objectCache.TryGetValue(cacheKey, out obj))
			{
				return (ODataResponse.SizedEdmModel)obj;
			}
			ODataResponse.SizedEdmModel sizedEdmModel2;
			try
			{
				using (HttpResponseData responseStream = odataRequest.GetResponseStream())
				{
					ODataResponse.SizedEdmModel sizedEdmModel = ODataResponse.SizedEdmModel.Read(host, resource, uri, responseStream);
					objectCache.CommitValue(cacheKey, sizedEdmModel.Size, sizedEdmModel);
					sizedEdmModel2 = sizedEdmModel;
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
			return sizedEdmModel2;
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000B4490 File Offset: 0x000B2690
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

		// Token: 0x0600383F RID: 14399 RVA: 0x000B44C8 File Offset: 0x000B26C8
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

		// Token: 0x06003840 RID: 14400 RVA: 0x000B454C File Offset: 0x000B274C
		private static HttpResponseData GetResponse(HttpResource resource, Uri serviceUri, Uri uri, Value headers, ResourceCredentialCollection credentials, string contentType, bool throwOnBadRequest, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings)
		{
			return HttpResponseHandler.GetResponseStream(resource, serviceUri, uri, headers, credentials, contentType, throwOnBadRequest, false, host, settings, userSettings, settings.ServerVersion, true);
		}

		// Token: 0x04001D15 RID: 7445
		public static readonly ODataMessageReaderSettings DefaultReaderSettings = new ODataMessageReaderSettings
		{
			MaxProtocolVersion = ODataVersion.V4,
			MessageQuotas = new ODataMessageQuotas
			{
				MaxReceivedMessageSize = long.MaxValue
			},
			ReadUntypedAsString = false
		};

		// Token: 0x02000777 RID: 1911
		private sealed class SizedEdmModel
		{
			// Token: 0x06003842 RID: 14402 RVA: 0x000B45AB File Offset: 0x000B27AB
			private SizedEdmModel(Microsoft.OData.Edm.IEdmModel model, int size)
			{
				this.model = model;
				this.size = size;
			}

			// Token: 0x17001332 RID: 4914
			// (get) Token: 0x06003843 RID: 14403 RVA: 0x000B45C1 File Offset: 0x000B27C1
			public Microsoft.OData.Edm.IEdmModel Model
			{
				get
				{
					return this.model;
				}
			}

			// Token: 0x17001333 RID: 4915
			// (get) Token: 0x06003844 RID: 14404 RVA: 0x000B45C9 File Offset: 0x000B27C9
			public int Size
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x06003845 RID: 14405 RVA: 0x000B45D4 File Offset: 0x000B27D4
			public static ODataResponse.SizedEdmModel Read(IEngineHost engineHost, HttpResource resource, Uri uri, HttpResponseData responseData)
			{
				ODataResponse.SizedEdmModel sizedEdmModel;
				try
				{
					using (HttpResponseData httpResponseData = ODataResponse.SizedEdmModel.RequireLengthAfterDispose(responseData))
					{
						using (IODataPayloadReader iodataPayloadReader = ODataResponse.CreateResponseReader(engineHost, resource, uri, httpResponseData, null))
						{
							Microsoft.OData.Edm.IEdmModel edmModel = iodataPayloadReader.ReadMetadataDocument();
							int num = (int)Math.Min(2147483647L, httpResponseData.Stream.Length);
							sizedEdmModel = new ODataResponse.SizedEdmModel(edmModel, num);
						}
					}
				}
				finally
				{
					if (responseData != null)
					{
						((IDisposable)responseData).Dispose();
					}
				}
				return sizedEdmModel;
			}

			// Token: 0x06003846 RID: 14406 RVA: 0x000B4668 File Offset: 0x000B2868
			private static HttpResponseData RequireLengthAfterDispose(HttpResponseData responseData)
			{
				Stream stream = CountingStream.New(responseData.Stream);
				return new HttpResponseData(responseData.ContentType, responseData.ContentLength, responseData.StatusCode, responseData.Headers, responseData.ResponseUri, stream.NonDisposable());
			}

			// Token: 0x04001D16 RID: 7446
			private readonly Microsoft.OData.Edm.IEdmModel model;

			// Token: 0x04001D17 RID: 7447
			private readonly int size;
		}
	}
}
