using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200084F RID: 2127
	internal static class HttpResponseDataExtensions
	{
		// Token: 0x06003D58 RID: 15704 RVA: 0x000C7628 File Offset: 0x000C5828
		public static string TryGetContextUrl(this HttpResponseData httpResponseData)
		{
			string contextUrl = null;
			httpResponseData.DetectPriorToRead(false, delegate(Stream s, Func<long> getCurrentPosition)
			{
				using (StreamReader streamReader = new StreamReader(s))
				{
					contextUrl = HttpResponseDataExtensions.GetODataContextUrl(streamReader);
				}
			});
			if (contextUrl != null)
			{
				return contextUrl;
			}
			throw ODataCommonErrors.UnableToDetectRequiredPayloadInformation(null);
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x000C766C File Offset: 0x000C586C
		public static ListValue GetCollectionResult(this HttpResponseData responseData, Microsoft.OData.Edm.IEdmModel model, Uri requestUri, string resourceKind, TypeValue itemType, ODataEnvironmentBase environment = null)
		{
			return HttpResponseDataExtensions.GetValue<ListValue>((environment != null) ? environment.Host : null, responseData, requestUri, resourceKind, delegate(ODataMessageReader messageReader)
			{
				ODataCollectionReader odataCollectionReader = messageReader.CreateODataCollectionReader();
				List<IValueReference> list = new List<IValueReference>();
				while (odataCollectionReader.Read())
				{
					if (odataCollectionReader.State == ODataCollectionReaderState.Value)
					{
						list.Add(ODataStructuralValueConverter.ConvertPropertyComplexCollectionOrPrimitive(ODataWrapperHelper.WrapValueIfNecessary(odataCollectionReader.Item), itemType, new Func<object, Value>(ODataTypeServices.MarshalFromClr), environment));
					}
				}
				return ListValue.New(list);
			}, model);
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x000C76BC File Offset: 0x000C58BC
		public static ValueException GetErrorResult(this HttpResponseData responseData, IEngineHost engineHost, Uri requestUri, string resourceKind)
		{
			return HttpResponseDataExtensions.GetValue<ValueException>(engineHost, responseData, requestUri, resourceKind, delegate(ODataMessageReader messageReader)
			{
				ODataError odataError = messageReader.ReadError();
				return DataSourceException.NewDataSourceError<Message2>(engineHost, DataSourceException.DataSourceMessage("OData", odataError.Message), Resource.New(resourceKind, requestUri.AbsoluteUri), null, null);
			}, null);
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x000C770C File Offset: 0x000C590C
		public static Value GetPropertyResult(this HttpResponseData responseData, Microsoft.OData.Edm.IEdmModel model, Uri requestUri, string resourceKind, TypeValue itemType, ODataEnvironmentBase environment = null)
		{
			return HttpResponseDataExtensions.GetValue<Value>((environment != null) ? environment.Host : null, responseData, requestUri, resourceKind, (ODataMessageReader messageReader) => ODataStructuralValueConverter.ConvertPropertyComplexCollectionOrPrimitive(new ODataPropertyWrapper(messageReader.ReadProperty()), itemType, new Func<object, Value>(ODataTypeServices.MarshalFromClr), environment), model);
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x000C775C File Offset: 0x000C595C
		public static Value GetRawResult(this HttpResponseData responseData, Uri requestUri, string resourceKind, TypeValue itemType, ODataEnvironment environment = null)
		{
			return HttpResponseDataExtensions.GetValue<Value>((environment != null) ? environment.Host : null, responseData, requestUri, resourceKind, (ODataMessageReader messageReader) => ODataStructuralValueConverter.ConvertPropertyComplexCollectionOrPrimitive(messageReader.ReadValue(ODataTypeServices.GetEdmPrimitiveTypeReference(itemType, false)), itemType, new Func<object, Value>(ODataTypeServices.MarshalFromClr), environment), null);
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x000C77AC File Offset: 0x000C59AC
		public static ServiceDocumentWrapper GetServiceDocument(this HttpResponseData responseData, Microsoft.OData.Edm.IEdmModel model, Uri requestUri)
		{
			ServiceDocumentWrapper serviceDocumentWrapper;
			try
			{
				serviceDocumentWrapper = new ServiceDocumentWrapper(HttpResponseDataExtensions.GetValueWithoutCatchingODataException<ODataServiceDocument>(responseData, (ODataMessageReader reader) => reader.ReadServiceDocument(), model), requestUri);
			}
			catch (ODataException ex)
			{
				throw ODataCommonErrors.ServiceDocumentCouldNotBeParsed(requestUri, ex);
			}
			return serviceDocumentWrapper;
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x000C7804 File Offset: 0x000C5A04
		public static Microsoft.OData.Edm.IEdmModel GetMetadataDocument(this HttpResponseData responseData, IEngineHost engineHost, HttpResource resource)
		{
			Microsoft.OData.Edm.IEdmModel valueWithoutCatchingODataException;
			try
			{
				valueWithoutCatchingODataException = HttpResponseDataExtensions.GetValueWithoutCatchingODataException<Microsoft.OData.Edm.IEdmModel>(responseData, (ODataMessageReader reader) => reader.ReadMetadataDocument(), null);
			}
			catch (ODataException ex)
			{
				throw new ODataTestConnectionFallbackException(ODataCommonErrors.InvalidMetadataDocument(engineHost, ex, resource.NewUrl(responseData.ResponseUri.AbsoluteUri)));
			}
			return valueWithoutCatchingODataException;
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x000C786C File Offset: 0x000C5A6C
		private static string GetODataContextUrl(StreamReader reader)
		{
			JsonTokenizer jsonTokenizer = new JsonTokenizer(reader, false, false, null);
			if (jsonTokenizer.GetNextToken() == JsonToken.RecordStart && jsonTokenizer.GetNextToken() == JsonToken.RecordKey && jsonTokenizer.GetTokenText() == "@odata.context" && jsonTokenizer.GetNextToken() == JsonToken.String)
			{
				return jsonTokenizer.GetTokenText();
			}
			return null;
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x000C78B8 File Offset: 0x000C5AB8
		private static T GetValue<T>(IEngineHost engineHost, HttpResponseData responseData, Uri requestUri, string resourceKind, Func<ODataMessageReader, T> getData, Microsoft.OData.Edm.IEdmModel model = null)
		{
			T valueWithoutCatchingODataException;
			try
			{
				valueWithoutCatchingODataException = HttpResponseDataExtensions.GetValueWithoutCatchingODataException<T>(responseData, getData, model);
			}
			catch (ODataException ex)
			{
				throw ODataCommonErrors.ODataFailedToParseODataResult(engineHost, ex, requestUri, resourceKind);
			}
			catch (IOException ex2)
			{
				throw ODataCommonErrors.ODataFailedToParseODataResult(engineHost, ex2, requestUri, resourceKind);
			}
			return valueWithoutCatchingODataException;
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x000C7908 File Offset: 0x000C5B08
		private static T GetValueWithoutCatchingODataException<T>(HttpResponseData responseData, Func<ODataMessageReader, T> getData, Microsoft.OData.Edm.IEdmModel model = null)
		{
			T t;
			using (ODataResponseMessage odataResponseMessage = new ODataResponseMessage(responseData))
			{
				using (ODataMessageReader odataMessageReader = new ODataMessageReader(odataResponseMessage, ODataResponse.DefaultReaderSettings, model))
				{
					t = getData(odataMessageReader);
				}
			}
			return t;
		}
	}
}
