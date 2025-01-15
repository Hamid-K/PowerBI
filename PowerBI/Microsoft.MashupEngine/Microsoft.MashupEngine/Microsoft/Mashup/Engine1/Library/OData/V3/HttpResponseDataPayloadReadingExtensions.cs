using System;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Data.OData;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008B6 RID: 2230
	internal static class HttpResponseDataPayloadReadingExtensions
	{
		// Token: 0x06003FC0 RID: 16320 RVA: 0x000D37A0 File Offset: 0x000D19A0
		public static DetectedPayloadInformationResults DetectPayloadInformation(this HttpResponseData httpResponseData, IEngineHost engineHost, ODataSettings settings, Uri requestUri, string resourceKind)
		{
			DetectedPayloadInformationResults detectInfo = new DetectedPayloadInformationResults();
			httpResponseData.DetectPriorToRead(true, delegate(Stream s, Func<long> getCurrentPosition)
			{
				using (StreamReader streamReader = new StreamReader(s))
				{
					if (httpResponseData.ContentTypes.Any((string ct) => ct.StartsWith("application/json", StringComparison.OrdinalIgnoreCase)))
					{
						detectInfo = HttpResponseDataPayloadReadingExtensions.SetJsonDetectPayloadInfo(requestUri, settings, streamReader, httpResponseData);
					}
					string text = httpResponseData.ContentTypes.SingleOrDefault((string ct) => ct.StartsWith("multipart/mixed", StringComparison.OrdinalIgnoreCase));
					if (text != null)
					{
						detectInfo = HttpResponseDataPayloadReadingExtensions.SetBatchPayloadDetectionInfo(engineHost, requestUri, resourceKind, getCurrentPosition, streamReader, requestUri, text);
					}
					if (!httpResponseData.ContentTypes.Any((string ct) => ct.StartsWith("application/xml", StringComparison.OrdinalIgnoreCase)))
					{
						if (!httpResponseData.ContentTypes.Any((string ct) => ct.StartsWith("application/atom+xml", StringComparison.OrdinalIgnoreCase)))
						{
							if (!httpResponseData.ContentTypes.Any((string ct) => ct.StartsWith("application/atomsvc+xml", StringComparison.OrdinalIgnoreCase)))
							{
								goto IL_016C;
							}
						}
					}
					HttpResponseDataPayloadReadingExtensions.SetXmlDetectPayloadInfo(engineHost, detectInfo, requestUri, resourceKind, getCurrentPosition, streamReader, httpResponseData.ResponseUri);
					IL_016C:;
				}
			});
			return detectInfo;
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x000D3800 File Offset: 0x000D1A00
		private static void SetXmlDetectPayloadInfo(IEngineHost engineHost, DetectedPayloadInformationResults results, Uri requestUri, string resourceKind, Func<long> getCurrentPosition, StreamReader reader, Uri responseUri)
		{
			using (XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(reader))
			{
				if (xmlReader.IsStartElement())
				{
					string localName = xmlReader.LocalName;
					if (localName.Equals("Edmx", StringComparison.OrdinalIgnoreCase))
					{
						results.IsMetadata = true;
					}
					else if (localName.Equals("Feed", StringComparison.OrdinalIgnoreCase))
					{
						HttpResponseDataPayloadReadingExtensions.SetDetectInfoForEntryOrFeed(engineHost, requestUri, resourceKind, xmlReader, getCurrentPosition, results);
					}
					else if (localName.Equals("Entry", StringComparison.OrdinalIgnoreCase))
					{
						HttpResponseDataPayloadReadingExtensions.SetDetectInfoForEntryOrFeed(engineHost, requestUri, resourceKind, xmlReader, getCurrentPosition, results);
					}
					else
					{
						HttpResponseDataPayloadReadingExtensions.SetServiceDocumentDetectInformation(xmlReader, responseUri, results);
					}
				}
			}
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x000D3898 File Offset: 0x000D1A98
		private static DetectedPayloadInformationResults SetJsonDetectPayloadInfo(Uri requestUri, ODataSettings settings, StreamReader reader, HttpResponseData httpResponseData)
		{
			DetectedPayloadInformationResults detectedPayloadInformationResults = new DetectedPayloadInformationResults();
			detectedPayloadInformationResults.IsJson = true;
			ODataVersion dataServiceVersion = httpResponseData.GetDataServiceVersion(ODataVersion.V3);
			if (dataServiceVersion == ODataVersion.V1 || dataServiceVersion == ODataVersion.V2)
			{
				settings.FallbackHandler.FallbackToOlderVersionIfPossible(ODataCommonErrors.InvalidUriError(requestUri));
			}
			Uri uri;
			if (JsonLightMetadata.TryGetMetadataUri(reader, out uri))
			{
				detectedPayloadInformationResults.MetadataUri = uri;
			}
			return detectedPayloadInformationResults;
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x000D38E4 File Offset: 0x000D1AE4
		private static DetectedPayloadInformationResults SetBatchPayloadDetectionInfo(IEngineHost engineHost, Uri requestUri, string resourceKind, Func<long> getCurrentPosition, StreamReader reader, Uri responseUri, string multiPartMimeContentType)
		{
			DetectedPayloadInformationResults detectedPayloadInformationResults = new DetectedPayloadInformationResults();
			detectedPayloadInformationResults.IsBatchPayload = true;
			int num = multiPartMimeContentType.IndexOf("boundary=", StringComparison.Ordinal);
			if (num < 0)
			{
				throw ODataCommonErrors.ODataBatchMultiPartMimeContentTypeHeaderInvalid(requestUri, multiPartMimeContentType);
			}
			int num2 = num + "boundary".Length + 1;
			string text = multiPartMimeContentType.Substring(num2, multiPartMimeContentType.Length - num2);
			Uri uri;
			HttpResponseDataPayloadReadingExtensions.BatchInnerPayloadType batchInnerPayloadType;
			HttpResponseDataPayloadReadingExtensions.TryGetMetadataUriInBatch(getCurrentPosition, reader, text, out uri, out batchInnerPayloadType);
			detectedPayloadInformationResults.MetadataUri = uri;
			if (batchInnerPayloadType == HttpResponseDataPayloadReadingExtensions.BatchInnerPayloadType.Xml)
			{
				HttpResponseDataPayloadReadingExtensions.SetXmlDetectPayloadInfo(engineHost, detectedPayloadInformationResults, requestUri, resourceKind, getCurrentPosition, reader, responseUri);
			}
			if (batchInnerPayloadType == HttpResponseDataPayloadReadingExtensions.BatchInnerPayloadType.Json)
			{
				detectedPayloadInformationResults.IsJson = true;
			}
			return detectedPayloadInformationResults;
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x000D3970 File Offset: 0x000D1B70
		private static bool TryGetMetadataUriInBatch(Func<long> getCurrentPosition, StreamReader reader, string batchBoundary, out Uri metadataUri, out HttpResponseDataPayloadReadingExtensions.BatchInnerPayloadType innerPayloadType)
		{
			long num = getCurrentPosition() + 1048576L;
			innerPayloadType = HttpResponseDataPayloadReadingExtensions.BatchInnerPayloadType.Other;
			if (reader.ReadUntil(batchBoundary) && reader.PeekUntilAnyChar(num, new char[] { '<', '{' }))
			{
				if (reader.Peek() == 123)
				{
					innerPayloadType = HttpResponseDataPayloadReadingExtensions.BatchInnerPayloadType.Json;
					return JsonLightMetadata.TryGetMetadataUri(reader, out metadataUri);
				}
				if (reader.Peek() == 60)
				{
					innerPayloadType = HttpResponseDataPayloadReadingExtensions.BatchInnerPayloadType.Xml;
				}
			}
			metadataUri = null;
			return false;
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x000D39DC File Offset: 0x000D1BDC
		private static void SetDetectInfoForEntryOrFeed(IEngineHost engineHost, Uri requestUri, string resourceKind, XmlReader xmlReader, Func<long> getCurrentPosition, DetectedPayloadInformationResults detectedPayloadInformationResults)
		{
			try
			{
				long num = getCurrentPosition() + 1048576L;
				Uri uri2;
				for (;;)
				{
					string attribute = xmlReader.GetAttribute("xml:base");
					Uri uri = null;
					if (attribute != null && Uri.TryCreate(attribute, UriKind.Absolute, out uri))
					{
						detectedPayloadInformationResults.BaseUri = uri;
					}
					if (xmlReader.LocalName == "id" && xmlReader.NamespaceURI == "http://www.w3.org/2005/Atom" && xmlReader.Read() && xmlReader.MoveToContent() == XmlNodeType.Text && Uri.TryCreate(xmlReader.Value, UriKind.Absolute, out uri2))
					{
						break;
					}
					if (!xmlReader.Read() || getCurrentPosition() >= num)
					{
						goto IL_0097;
					}
				}
				detectedPayloadInformationResults.IdUri = uri2;
				IL_0097:;
			}
			catch (XmlException ex)
			{
				throw ODataCommonErrors.ODataFailedToParseODataResult(engineHost, ex, requestUri, resourceKind);
			}
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x000D3AA0 File Offset: 0x000D1CA0
		private static void SetServiceDocumentDetectInformation(XmlReader xmlReader, Uri responseUri, DetectedPayloadInformationResults detectInfo)
		{
			string attribute;
			for (;;)
			{
				if (xmlReader.Name.Equals("Service", StringComparison.OrdinalIgnoreCase))
				{
					attribute = xmlReader.GetAttribute("xml:base");
					if (!string.IsNullOrEmpty(attribute) && Uri.IsWellFormedUriString(attribute, UriKind.Absolute))
					{
						break;
					}
				}
				if (!xmlReader.Read())
				{
					goto Block_3;
				}
			}
			detectInfo.BaseUri = HttpResponseDataPayloadReadingExtensions.ReuseRequestUriBase(attribute, responseUri);
			return;
			Block_3:
			Uri uri;
			if (ODataUriNormalizer.TryTerminateWithSlash(responseUri, out uri))
			{
				detectInfo.BaseUri = uri;
			}
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x000D3B04 File Offset: 0x000D1D04
		private static Uri ReuseRequestUriBase(string baseUri, Uri requestUri)
		{
			baseUri = baseUri.Trim(new char[] { '/' });
			string absoluteUri = requestUri.AbsoluteUri;
			return new Uri((absoluteUri.StartsWith(baseUri, StringComparison.OrdinalIgnoreCase) ? absoluteUri.Substring(0, baseUri.Length) : baseUri) + "/");
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x000D3B54 File Offset: 0x000D1D54
		private static ODataVersion GetDataServiceVersion(this HttpResponseData httpResponseData, ODataVersion defaultVersion)
		{
			string text = null;
			httpResponseData.Headers.TryGetValue("DataServiceVersion", out text);
			if (!string.IsNullOrEmpty(text))
			{
				return ODataUtils.StringToODataVersion(text);
			}
			return defaultVersion;
		}

		// Token: 0x0400216E RID: 8558
		private const int MaximumProbeLength = 1048576;

		// Token: 0x0400216F RID: 8559
		private const string boundary = "boundary";

		// Token: 0x020008B7 RID: 2231
		private enum BatchInnerPayloadType
		{
			// Token: 0x04002171 RID: 8561
			Json,
			// Token: 0x04002172 RID: 8562
			Xml,
			// Token: 0x04002173 RID: 8563
			Other
		}
	}
}
