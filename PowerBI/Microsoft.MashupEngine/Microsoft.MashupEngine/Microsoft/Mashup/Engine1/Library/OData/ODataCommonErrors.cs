using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Xml;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Library.SharePoint;
using Microsoft.Mashup.Engine1.Library.Xml;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x0200072F RID: 1839
	internal static class ODataCommonErrors
	{
		// Token: 0x060036A3 RID: 13987 RVA: 0x000AE260 File Offset: 0x000AC460
		public static string ExtractErrorMessage(IEngineHost host, IResource resource, WebException e)
		{
			string text;
			if (ODataCommonErrors.TryExtractErrorMessageFromResponse(host, resource, e.Response, out text))
			{
				return Strings.ODataCommonError(e.Message, text);
			}
			return e.Message;
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000AE298 File Offset: 0x000AC498
		public static bool TryExtractErrorMessage(IEngineHost host, IResource resource, HttpResponseData response, out string errorMessage)
		{
			using (IHostTrace hostTrace = TracingService.CreateTrace(host, "ODataErrors/ExtractErrorMessageFromResponse", TraceEventType.Information, resource))
			{
				try
				{
					if (response != null)
					{
						if (!ODataCommonErrors.TryExtractErrorFromStream(response.ContentType, response.Stream, out errorMessage))
						{
							errorMessage = response.StatusCode.ToString(CultureInfo.InvariantCulture);
						}
						return true;
					}
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.TraceIsSafeException(hostTrace, ex))
					{
						throw;
					}
					host.LogIgnoredException(ex);
				}
			}
			errorMessage = null;
			return false;
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000AE328 File Offset: 0x000AC528
		public static ValueException ODataContextUriIsNotValid(Exception exc, string requestUri)
		{
			return ValueException.NewDataFormatError(exc.Message, TextValue.New(requestUri), exc);
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000AE33C File Offset: 0x000AC53C
		private static bool TryExtractErrorFromStream(string contentType, Stream stream, out string message)
		{
			message = null;
			if (contentType == string.Empty)
			{
				return false;
			}
			string mediaType = new ContentType(contentType).MediaType;
			if (mediaType == "application/xml")
			{
				message = ODataCommonErrors.GetXmlData(stream);
				return true;
			}
			if (mediaType == "text/plain")
			{
				message = new StreamReader(stream).ReadToEnd();
				return true;
			}
			if (mediaType == "application/json")
			{
				message = ODataCommonErrors.GetJsonData(stream);
				return true;
			}
			return false;
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x000AE3B4 File Offset: 0x000AC5B4
		private static bool TryExtractErrorMessageFromResponse(IEngineHost host, IResource resource, WebResponse response, out string errorMessage)
		{
			errorMessage = null;
			bool flag;
			using (IHostTrace hostTrace = TracingService.CreateTrace(host, "ODataErrors/ExtractErrorMessageFromResponse", TraceEventType.Information, resource))
			{
				try
				{
					if (response != null)
					{
						MashupHttpWebResponse mashupHttpWebResponse = response as MashupHttpWebResponse;
						if (mashupHttpWebResponse != null && !ODataCommonErrors.TryExtractErrorFromStream(response.ContentType, response.GetDecompressedResponseStream(), out errorMessage))
						{
							errorMessage = mashupHttpWebResponse.StatusDescription;
						}
						return true;
					}
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.TraceIsSafeException(hostTrace, ex))
					{
						throw;
					}
					host.LogIgnoredException(ex);
					errorMessage = null;
					return false;
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x000AE448 File Offset: 0x000AC648
		private static string GetXmlData(Stream stream)
		{
			string text;
			using (XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(stream, XmlModuleHelper.DefaultXmlReaderSettings))
			{
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				XmlDocument xmlDocument = XmlHelperUtility.CreateXmlDocument();
				xmlDocument.Load(xmlReader);
				foreach (XmlElement xmlElement in xmlDocument.ChildNodes.OfType<XmlElement>())
				{
					if (string.Equals(xmlElement.LocalName, "error", StringComparison.OrdinalIgnoreCase))
					{
						using (IEnumerator<XmlElement> enumerator2 = xmlElement.ChildNodes.OfType<XmlElement>().GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								XmlElement xmlElement2 = enumerator2.Current;
								if (string.Equals(xmlElement2.LocalName, "message", StringComparison.OrdinalIgnoreCase))
								{
									if (stringBuilder.Length > 0)
									{
										stringBuilder.Append(", ");
									}
									stringBuilder.Append(xmlElement2.InnerText);
								}
							}
							continue;
						}
					}
					if (string.Equals(xmlElement.LocalName, "string", StringComparison.OrdinalIgnoreCase))
					{
						if (stringBuilder2.Length > 0)
						{
							stringBuilder2.Append(", ");
						}
						stringBuilder2.Append(xmlElement.InnerText);
					}
				}
				if (stringBuilder.Length > 0)
				{
					text = stringBuilder.ToString();
				}
				else if (stringBuilder2.Length > 0)
				{
					text = stringBuilder2.ToString();
				}
				else
				{
					text = null;
				}
			}
			return text;
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x000AE5E8 File Offset: 0x000AC7E8
		private static string GetJsonData(Stream stream)
		{
			Value value = JsonParser.Parse(new StreamReader(stream), null).AsRecord[0].AsRecord["message"];
			if (value.IsText)
			{
				return value.AsString;
			}
			return value.AsRecord["value"].AsString;
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000AE640 File Offset: 0x000AC840
		public static ValueException InvalidHeaderValueType(string headerName, ValueKind invalidValueKind)
		{
			return ValueException.NewExpressionError(ODataCommonErrors.CreateDataSourceExceptionMessage(Strings.InvalidHeaderValueType(headerName, invalidValueKind)), Value.Null, null);
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000AE663 File Offset: 0x000AC863
		public static ValueException UnableToDetectRequiredPayloadInformation(Exception exc)
		{
			return ValueException.NewDataFormatError(ODataCommonErrors.CreateDataSourceExceptionMessage(Strings.HttpResponseDetectionFailed), Value.Null, exc);
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000AE67F File Offset: 0x000AC87F
		public static ValueException InvalidServiceUri(Uri uri)
		{
			return ValueException.NewDataFormatError(ODataCommonErrors.CreateDataSourceExceptionMessage(Strings.UriInvalidArgument), TextValue.New(uri.ToString()), null);
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x000AE6A4 File Offset: 0x000AC8A4
		public static ValueException InvalidUriError(Uri uri)
		{
			string text = uri.ToString();
			return ValueException.NewDataFormatError<Message1>(Strings.ODataInvalidUriError(text), TextValue.New(text), null);
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x000AE6CC File Offset: 0x000AC8CC
		public static ValueException InvalidReaderState(string state, Uri uri)
		{
			string text = uri.ToString();
			return ValueException.NewDataFormatError<Message2>(Strings.ODataInvalidReaderState(state, text), TextValue.New(text), null);
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x000AE6F3 File Offset: 0x000AC8F3
		public static ValueException UnsupportedFormat(string format)
		{
			return ValueException.NewDataFormatError<Message1>(Strings.ODataUnsupportedFormat(format), TextValue.New(format), null);
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000AE707 File Offset: 0x000AC907
		public static ValueException UnsupportedPayload(string kindString)
		{
			return ValueException.NewDataFormatError<Message1>(Strings.ODataUnsupportedPayload(kindString), TextValue.New(kindString), null);
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000AE71B File Offset: 0x000AC91B
		public static ValueException MissingMetadataDocument(WebException exception)
		{
			return ValueException.NewDataSourceNotFound(ODataCommonErrors.CreateDataSourceExceptionMessage(Strings.NoMetadataDocument), Value.Null, exception);
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000AE737 File Offset: 0x000AC937
		public static ValueException InvalidMetadataDocument(IEngineHost engineHost, Exception exception, HttpResource resource)
		{
			return DataSourceException.NewDataSourceError(engineHost, ODataCommonErrors.CreateDataSourceExceptionMessage(Strings.InvalidMetadataDocument(exception.Message)), resource.Resource, null, exception);
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x000AE75C File Offset: 0x000AC95C
		public static ValueException DuplicateProperty(IEngineHost engineHost, IResource resource, string structuredTypeName, string propertyName)
		{
			return DataSourceException.NewDataSourceError<Message1>(engineHost, Strings.InvalidMetadataDocument((structuredTypeName == null) ? Strings.ODataMetadataDuplicatePropertyNamesUnknownTypeName(propertyName).ToString() : Strings.ODataMetadataDuplicatePropertyNames(structuredTypeName, propertyName)), resource, "Property", TextValue.New(propertyName), TypeValue.Text, null);
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x000AE7AC File Offset: 0x000AC9AC
		public static ValueException UnsupportedBatchInnerPayloadKind(string payloadKind, Uri uri)
		{
			string text = uri.ToString();
			return ValueException.NewDataFormatError<Message1>(Strings.ODataBatchInnerPayloadKindNotSupported(payloadKind), TextValue.New(text), null);
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x000AE7D4 File Offset: 0x000AC9D4
		public static ValueException UnsupportedBatchReaderState(string state, Uri uri)
		{
			string text = uri.ToString();
			return ValueException.NewDataFormatError<Message1>(Strings.ODataBatchPayloadStateNotSupported(state), TextValue.New(text), null);
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x000AE7FC File Offset: 0x000AC9FC
		public static ValueException UnsupportedBatchResponseWithMoreThanOneInnerPayload(Uri uri)
		{
			string text = uri.ToString();
			return ValueException.NewDataFormatError<Message0>(Strings.ODataBatchSupportsOnlyOneInnerPayload, TextValue.New(text), null);
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x000AE824 File Offset: 0x000ACA24
		public static ValueException ODataBatchMultiPartMimeContentTypeHeaderInvalid(Uri uri, string invalidContentTypeHeaderValue)
		{
			string text = uri.ToString();
			return ValueException.NewDataFormatError<Message1>(Strings.ODataBatchMultiPartMimeContentTypeHeaderInvalid(invalidContentTypeHeaderValue), TextValue.New(text), null);
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x000AE84C File Offset: 0x000ACA4C
		public static ValueException ODataFailedToParseODataResult(IEngineHost engineHost, Exception exception, Uri uri, string resourceKind)
		{
			string absoluteUri = uri.AbsoluteUri;
			return DataSourceException.NewDataSourceError<Message1>(engineHost, Strings.ODataFailedToParseODataResult(exception.Message), Resource.New(resourceKind, absoluteUri), null, exception);
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x000AE87C File Offset: 0x000ACA7C
		public static ValueException ServiceDocumentCouldNotBeAccessed(Uri uri, Exception exc)
		{
			string text = uri.ToString();
			return ValueException.NewDataFormatError<Message1>(Strings.ODataServiceDocumentCouldNotBeAccessed(text), TextValue.New(text), exc);
		}

		// Token: 0x060036BA RID: 14010 RVA: 0x000AE8A2 File Offset: 0x000ACAA2
		public static bool TryExtractSharePointCorrelationID(WebException e, out string guid)
		{
			return SharePointUtil.TryExtractSharePointCorrelationID(e.Response as MashupHttpWebResponse, out guid);
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x000AE8B8 File Offset: 0x000ACAB8
		public static ValueException RequestFailed(IEngineHost host, WebException e, Uri uri, HttpResource resource)
		{
			resource = resource.NewUrl(uri.AbsoluteUri);
			string text = ODataCommonErrors.ExtractErrorMessage(host, resource.Resource, e);
			MashupHttpWebResponse mashupHttpWebResponse = e.Response as MashupHttpWebResponse;
			IList<RecordKeyDefinition> list = new List<RecordKeyDefinition>();
			if (mashupHttpWebResponse != null)
			{
				string text2;
				if (ODataCommonErrors.TryExtractSharePointCorrelationID(e, out text2))
				{
					list.Add(new RecordKeyDefinition("SPRequestGuid", TextValue.New(text2), TypeValue.Text));
				}
				list.Add(new RecordKeyDefinition("Url", TextValue.New(uri.ToString()), TypeValue.Text));
				if (mashupHttpWebResponse.StatusCode == HttpStatusCode.NotFound)
				{
					return DataSourceException.NewDataSourceNotFound<Message3>(host, Strings.RequestFailedWithStatusCode(resource.Kind, (int)mashupHttpWebResponse.StatusCode, text), resource.Resource, list, e);
				}
			}
			return DataSourceException.NewDataSourceError<Message2>(host, Strings.RequestFailedWithoutStatusCode(resource.Kind, text), resource.Resource, list, e);
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x000AE988 File Offset: 0x000ACB88
		public static ValueException ServiceDocumentCouldNotBeParsed(Uri uri, Exception exception)
		{
			string text = uri.ToString();
			return ValueException.NewDataFormatError<Message1>(Strings.ODataServiceDocumentCouldNotBeParsed(exception.Message), TextValue.New(text), exception);
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x000AE9B4 File Offset: 0x000ACBB4
		public static ValueException CouldNotFindNavigationUrl(string navigationPropertyName, Exception exception = null)
		{
			return ValueException.NewDataFormatError(ODataCommonErrors.CreateDataSourceExceptionMessage((exception == null) ? Strings.ODataNavigationPropertyWithoutUrl.ToString() : Strings.ODataNavigationPropertyWithoutUrlWithMessage(exception.Message)), TextValue.New(navigationPropertyName), exception);
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x000AE9FC File Offset: 0x000ACBFC
		public static ValueException MissingProperty(string propertyName, IValueReference delayedPropertyValue = null)
		{
			Value value;
			if (delayedPropertyValue != null)
			{
				value = RecordValue.New(ODataCommonErrors.MissingPropertyWithDelayedValueDetailType, new IValueReference[]
				{
					TextValue.New(propertyName),
					delayedPropertyValue
				});
			}
			else
			{
				value = RecordValue.New(ODataCommonErrors.MissingPropertyDetailKeys, new Value[] { TextValue.New(propertyName) });
			}
			return ValueException.NewDataFormatError<Message1>(Strings.ODataMissingProperty(propertyName), value, null);
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x000AEA53 File Offset: 0x000ACC53
		public static ValueException ODataNonTerminatingContextUrl(string path)
		{
			return ValueException.NewDataFormatError<Message1>(Strings.ODataNonTerminatingContextUrl(path), TextValue.New(path), null);
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000AEA67 File Offset: 0x000ACC67
		public static ValueException ODataExceptionMessage(IEngineHost engineHost, Exception e, Uri uri, string resourceKind)
		{
			return DataSourceException.NewDataSourceError(engineHost, ODataCommonErrors.CreateDataSourceExceptionMessage(e.Message), Resource.New(resourceKind, uri.AbsoluteUri), null, e);
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x000AEA88 File Offset: 0x000ACC88
		public static string CreateDataSourceExceptionMessage(string message)
		{
			return DataSourceException.DataSourceMessage("OData", message);
		}

		// Token: 0x04001C23 RID: 7203
		public const string PropertyKey = "Property";

		// Token: 0x04001C24 RID: 7204
		private static readonly Keys MissingPropertyDetailKeys = Keys.New("Property");

		// Token: 0x04001C25 RID: 7205
		private static readonly Keys MissingPropertyWithDelayedValueDetailKeys = Keys.New("Property", "Value");

		// Token: 0x04001C26 RID: 7206
		private static readonly RecordTypeValue MissingPropertyWithDelayedValueDetailType = RecordTypeValue.New(RecordValue.New(ODataCommonErrors.MissingPropertyWithDelayedValueDetailKeys, new Value[]
		{
			RecordTypeValue.NewField(TypeValue.Any, null),
			RecordTypeValue.NewField(PreviewServices.ConvertToDelayedValue(TypeValue.Any, "Value"), null)
		}));
	}
}
