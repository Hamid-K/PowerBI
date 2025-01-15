using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Data.Experimental.OData.Query;
using Microsoft.Data.OData;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008D1 RID: 2257
	internal class ODataResponse
	{
		// Token: 0x06004078 RID: 16504 RVA: 0x000D7070 File Offset: 0x000D5270
		private ODataResponse(Uri requestUri, ODataSettings settings, ODataUserSettings userSettings, HttpResponseData httpResponseData, Value headers, HttpResource resource, ResourceCredentialCollection credentials, IEngineHost host)
		{
			this.CreateFrom(requestUri, settings, userSettings, httpResponseData, headers, resource, credentials, host);
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x000D7098 File Offset: 0x000D5298
		private ODataResponse(ODataSettings settings, ODataUserSettings userSettings, Uri feedUri, Uri serviceUri, Value headers, HttpResource resource, ResourceCredentialCollection credentials, IEngineHost host)
		{
			this.CreateFeedEntry(settings, userSettings, feedUri, serviceUri, headers, resource, credentials, host);
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x0600407A RID: 16506 RVA: 0x000D70BE File Offset: 0x000D52BE
		public ODataServiceDocument ServiceDocument
		{
			get
			{
				return this.serviceDoc;
			}
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x0600407B RID: 16507 RVA: 0x000D70C6 File Offset: 0x000D52C6
		public Value Result
		{
			get
			{
				if (this.result == null)
				{
					return Value.Null;
				}
				return this.result.Value;
			}
		}

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x000D70E1 File Offset: 0x000D52E1
		public Uri ServiceUri
		{
			get
			{
				return this.serviceUri;
			}
		}

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x0600407D RID: 16509 RVA: 0x000D70E9 File Offset: 0x000D52E9
		public Uri ResponseUri
		{
			get
			{
				return this.responseUri;
			}
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x000D70F4 File Offset: 0x000D52F4
		public static ODataResponse Create(HttpResponseData responseData, Uri serviceUri, HttpResource resource, Uri uri, Value headers, ResourceCredentialCollection credentials, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings)
		{
			bool requiresAdditionalRequests = false;
			return settings.FallbackHandler.HandleVersionFallback<ODataResponse>(delegate
			{
				if (requiresAdditionalRequests)
				{
					responseData = Http.GetAnyResult(resource, serviceUri, uri, headers, credentials, host, settings, userSettings);
				}
				else
				{
					requiresAdditionalRequests = true;
				}
				return new ODataResponse(uri, settings, userSettings, responseData, headers, resource, credentials, host);
			}, false);
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x000D7170 File Offset: 0x000D5370
		public static ODataResponse Create(HttpResource resource, TextValue feedUrl, TextValue serviceUrl, Value headers, ResourceCredentialCollection credentials, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings)
		{
			Uri uri = ODataUriCommon.ConvertToUri(feedUrl);
			Uri uri2 = ODataUriCommon.ConvertToUri(serviceUrl);
			ODataUriCommon.ValidateHttpAbsolute(uri);
			ODataUriCommon.ValidateHttpAbsolute(uri2);
			headers = (headers.IsNull ? RequestHeaders.DefaultUserAgentHeader : RequestHeaders.DefaultUserAgentHeader.Concatenate(headers));
			return new ODataResponse(settings, userSettings, uri, uri2, headers, resource, credentials, host);
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x000D71C4 File Offset: 0x000D53C4
		private void CreateFrom(Uri requestUri, ODataSettings settings, ODataUserSettings userSettings, HttpResponseData responseData, Value headers, HttpResource resource, ResourceCredentialCollection credentials, IEngineHost host)
		{
			if (responseData.StatusCode == 204)
			{
				return;
			}
			try
			{
				DetectedPayloadInformationResults detectedPayloadInformationResults = responseData.DetectPayloadInformation(host, settings, requestUri, resource.Kind);
				ODataResponse.EntryFeedUriProcessor entryFeedUriProcessor = new ODataResponse.EntryFeedUriProcessor(host, resource.Resource, responseData.ResponseUri, requestUri);
				if (detectedPayloadInformationResults.IsJson)
				{
					if (detectedPayloadInformationResults.MetadataUri == null || !entryFeedUriProcessor.TrySetBaseUri(new Uri(detectedPayloadInformationResults.MetadataUri, ".")))
					{
						settings.FallbackHandler.FallbackToOlderVersionIfPossible(ODataCommonErrors.InvalidUriError(requestUri));
					}
					else
					{
						settings.UpdateModel(null, detectedPayloadInformationResults.MetadataUri, headers, resource, credentials, userSettings);
					}
				}
				if (detectedPayloadInformationResults.IsMetadata && !detectedPayloadInformationResults.IsBatchPayload)
				{
					using (TextReader textReader = new StreamReader(responseData.Stream))
					{
						this.Populate(TextValue.New(textReader.ReadToEnd()), null, responseData.ResponseUri);
					}
				}
				else
				{
					using (ODataResponseMessage odataResponseMessage = new ODataResponseMessage(responseData))
					{
						Uri uri = entryFeedUriProcessor.BaseUri ?? detectedPayloadInformationResults.BaseUri;
						ODataMessageReaderSettings odataMessageReaderSettings = new ODataMessageReaderSettings(ODataResponse.DefaultReaderSettings)
						{
							BaseUri = uri
						};
						using (ODataMessageReader odataMessageReader = new ODataMessageReader(odataResponseMessage, odataMessageReaderSettings, settings.EdmModel))
						{
							ODataPayloadKindDetectionResult odataPayloadKindDetectionResult = ODataResponse.DetermineODataPayloadKind(settings, odataMessageReader, odataResponseMessage.ResponseUri);
							switch (odataPayloadKindDetectionResult.PayloadKind)
							{
							case ODataPayloadKind.Feed:
								this.CreateFeedOrEntry(settings, userSettings, odataMessageReader.CreateODataFeedReader(), odataPayloadKindDetectionResult.PayloadKind, headers, resource, credentials, host, entryFeedUriProcessor, detectedPayloadInformationResults);
								goto IL_02A9;
							case ODataPayloadKind.Entry:
								this.CreateFeedOrEntry(settings, userSettings, odataMessageReader.CreateODataEntryReader(), odataPayloadKindDetectionResult.PayloadKind, headers, resource, credentials, host, entryFeedUriProcessor, detectedPayloadInformationResults);
								goto IL_02A9;
							case ODataPayloadKind.Property:
								this.Populate(ODataMessageReaderValueConverters.CreatePropertyResult(TypeValue.Any, odataMessageReader, null), odataResponseMessage.ResponseUri, odataResponseMessage.ResponseUri);
								goto IL_02A9;
							case ODataPayloadKind.Value:
								this.Populate(ODataMessageReaderValueConverters.CreateRawResult(TypeValue.Any, odataMessageReader, null), odataResponseMessage.ResponseUri, odataResponseMessage.ResponseUri);
								goto IL_02A9;
							case ODataPayloadKind.BinaryValue:
								this.Populate(ODataMessageReaderValueConverters.CreateRawResult(TypeValue.Binary, odataMessageReader, null), odataResponseMessage.ResponseUri, odataResponseMessage.ResponseUri);
								goto IL_02A9;
							case ODataPayloadKind.Collection:
								this.Populate(ListValue.New(ODataMessageReaderValueConverters.CreateCollectionResult(TypeValue.Any, odataMessageReader.CreateODataCollectionReader(), null).ToArray()), odataResponseMessage.ResponseUri, odataResponseMessage.ResponseUri);
								goto IL_02A9;
							case ODataPayloadKind.ServiceDocument:
								this.Populate(ODataServiceDocument.Create(odataMessageReader, uri, null), uri);
								goto IL_02A9;
							case ODataPayloadKind.Error:
								throw ODataMessageReaderValueConverters.CreateErrorResult(host, odataMessageReader, requestUri, resource.Kind);
							case ODataPayloadKind.Batch:
								this.ReadBatch(requestUri, settings, odataMessageReader, odataResponseMessage, userSettings, entryFeedUriProcessor, detectedPayloadInformationResults, headers, resource, credentials, host);
								goto IL_02A9;
							}
							throw ODataCommonErrors.UnsupportedPayload(odataPayloadKindDetectionResult.PayloadKind.ToString());
							IL_02A9:;
						}
					}
				}
			}
			catch (ODataException ex)
			{
				throw ODataCommonErrors.ODataFailedToParseODataResult(host, ex, responseData.ResponseUri, resource.Kind);
			}
			catch (IOException ex2)
			{
				throw ODataCommonErrors.ODataFailedToParseODataResult(host, ex2, responseData.ResponseUri, resource.Kind);
			}
			catch (XmlException ex3)
			{
				throw ODataCommonErrors.ODataFailedToParseODataResult(host, ex3, responseData.ResponseUri, resource.Kind);
			}
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x000D7578 File Offset: 0x000D5778
		internal static ODataPayloadKindDetectionResult DetermineODataPayloadKind(ODataSettings settings, ODataMessageReader reader, Uri responseUri)
		{
			IList<ODataPayloadKindDetectionResult> list = new List<ODataPayloadKindDetectionResult>(reader.DetectPayloadKind());
			if (list.Count == 2)
			{
				if (list.Any((ODataPayloadKindDetectionResult p) => p.PayloadKind == ODataPayloadKind.Collection))
				{
					if (list.Any((ODataPayloadKindDetectionResult p) => p.PayloadKind == ODataPayloadKind.Property))
					{
						if (list[0].PayloadKind != ODataPayloadKind.Collection)
						{
							return list[1];
						}
						return list[0];
					}
				}
			}
			if (list.Count != 1)
			{
				settings.FallbackHandler.FallbackToOlderVersionIfPossible(ODataCommonErrors.InvalidUriError(responseUri));
			}
			return list.First<ODataPayloadKindDetectionResult>();
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x000D7628 File Offset: 0x000D5828
		private void ReadBatch(Uri requestUri, ODataSettings settings, ODataMessageReader odataMessageReader, ODataResponseMessage response, ODataUserSettings userSettings, ODataResponse.EntryFeedUriProcessor entryFeedUriProcessor, DetectedPayloadInformationResults detectInfo, Value headers, HttpResource resource, ResourceCredentialCollection credentials, IEngineHost host)
		{
			ODataPayloadKind odataPayloadKind;
			List<IValueReference> list = ODataMessageReaderValueConverters.CreateODataBatch(host, resource.Kind, settings, odataMessageReader, response, response.ResponseUri, delegate(ODataMessageReader innerReader, ODataPayloadKind kind)
			{
				if (kind == ODataPayloadKind.Entry)
				{
					this.CreateFeedOrEntry(settings, userSettings, innerReader.CreateODataEntryReader(), kind, headers, resource, credentials, host, entryFeedUriProcessor, detectInfo);
				}
				else
				{
					this.CreateFeedOrEntry(settings, userSettings, innerReader.CreateODataFeedReader(), kind, headers, resource, credentials, host, entryFeedUriProcessor, detectInfo);
				}
				return new List<IValueReference>(new IValueReference[] { this.result });
			}, out odataPayloadKind, null);
			if (odataPayloadKind == ODataPayloadKind.Collection)
			{
				this.Populate(ListValue.New(list.ToArray()), response.ResponseUri, response.ResponseUri);
				return;
			}
			if (odataPayloadKind != ODataPayloadKind.Entry && odataPayloadKind != ODataPayloadKind.Feed)
			{
				this.Populate(list.FirstOrDefault<IValueReference>(), requestUri, response.ResponseUri);
			}
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x000D76F8 File Offset: 0x000D58F8
		private void CreateFeedOrEntry(ODataSettings settings, ODataUserSettings userSettings, ODataReader reader, ODataPayloadKind payloadKind, Value headers, HttpResource resource, ResourceCredentialCollection credentials, IEngineHost host, ODataResponse.EntryFeedUriProcessor entryFeedUriProcessor, DetectedPayloadInformationResults detectInfo)
		{
			Uri uri = null;
			if (entryFeedUriProcessor.BaseUri == null)
			{
				Uri uri2 = ODataResponse.SetBaseUriAndGetId(entryFeedUriProcessor, detectInfo, payloadKind);
				if (payloadKind == ODataPayloadKind.Feed)
				{
					uri = uri2;
				}
			}
			string text = null;
			if (uri == entryFeedUriProcessor.ResponseUri)
			{
				uri = null;
			}
			QueryDescriptorQueryToken queryDescriptorQueryToken;
			if (uri != null)
			{
				text = ODataResponse.ParseFeedName(host, resource.Resource, uri, entryFeedUriProcessor.BaseUri, true, out queryDescriptorQueryToken);
			}
			string text2 = ODataResponse.ParseFeedName(host, resource.Resource, entryFeedUriProcessor.ResponseUri, entryFeedUriProcessor.BaseUri, true, out queryDescriptorQueryToken);
			ODataServiceDocument odataServiceDocument = ODataServiceDocument.Create(entryFeedUriProcessor.BaseUri, text2, uri, text, queryDescriptorQueryToken.QueryOptions);
			ODataEnvironment odataEnvironment = ODataEnvironment.Create(odataServiceDocument, headers, resource, credentials, host, settings, userSettings, false);
			if (odataServiceDocument.FoundFeedName != null)
			{
				text2 = odataServiceDocument.FoundFeedName;
			}
			Value value = null;
			if (odataEnvironment.EdmModel == null || !ODataResponse.TryGetDirectFeedResult(text2, queryDescriptorQueryToken, odataEnvironment, host, out value))
			{
				value = ODataMessageReaderValueConverters.CreateFeedEntryResult(reader, entryFeedUriProcessor.ResponseUri, odataEnvironment, TypeValue.Table, payloadKind == ODataPayloadKind.Entry);
			}
			this.Populate(value, entryFeedUriProcessor.BaseUri, entryFeedUriProcessor.ResponseUri);
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x000D7808 File Offset: 0x000D5A08
		private void CreateFeedEntry(ODataSettings settings, ODataUserSettings userSettings, Uri feedUri, Uri serviceUri, Value headers, HttpResource resource, ResourceCredentialCollection credentials, IEngineHost host)
		{
			QueryDescriptorQueryToken queryDescriptorQueryToken;
			string text = ODataResponse.ParseFeedName(host, resource.Resource, feedUri, serviceUri, false, out queryDescriptorQueryToken);
			ODataEnvironment odataEnvironment = ODataEnvironment.Create(ODataServiceDocument.Create(serviceUri, text, null, null, queryDescriptorQueryToken.QueryOptions), headers, resource, credentials, host, settings, userSettings, false);
			if (odataEnvironment.EdmModel == null)
			{
				throw ODataCommonErrors.ServiceDocumentCouldNotBeAccessed(serviceUri, null);
			}
			Value value = null;
			if (!ODataResponse.TryGetDirectFeedResult(text, queryDescriptorQueryToken, odataEnvironment, host, out value))
			{
				throw ODataErrors.UnsupportedUri(feedUri);
			}
			this.Populate(value, serviceUri, feedUri);
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x000D7880 File Offset: 0x000D5A80
		private static string ParseFeedName(IEngineHost host, IResource resource, Uri uri, Uri serviceUri, bool catchExceptions, out QueryDescriptorQueryToken queryToken)
		{
			if (uri == null || serviceUri == null)
			{
				queryToken = null;
				return null;
			}
			uri = ODataUriNormalizer.Normalize(uri);
			if (!ODataUri.TryParseODataUri(host, resource, uri, serviceUri, out queryToken))
			{
				if (catchExceptions)
				{
					return null;
				}
				throw ODataCommonErrors.InvalidUriError(uri);
			}
			else
			{
				if (queryToken == null || queryToken.Path == null)
				{
					return null;
				}
				string text;
				try
				{
					SegmentQueryToken rootSegment = ODataResponse.GetRootSegment(queryToken.Path);
					if (rootSegment == null)
					{
						text = null;
					}
					else
					{
						text = rootSegment.Name;
					}
				}
				catch (ODataException)
				{
					if (!catchExceptions)
					{
						throw ODataCommonErrors.InvalidUriError(uri);
					}
					queryToken = null;
					text = null;
				}
				return text;
			}
		}

		// Token: 0x06004086 RID: 16518 RVA: 0x000D791C File Offset: 0x000D5B1C
		public static ODataMessageReaderSettings GetReaderSettings(Uri baseUri)
		{
			return new ODataMessageReaderSettings
			{
				BaseUri = baseUri,
				EnableAtomMetadataReading = ODataResponse.DefaultReaderSettings.EnableAtomMetadataReading,
				MaxProtocolVersion = ODataResponse.DefaultReaderSettings.MaxProtocolVersion,
				MessageQuotas = ODataResponse.DefaultReaderSettings.MessageQuotas
			};
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x000D795C File Offset: 0x000D5B5C
		private static SegmentQueryToken GetRootSegment(QueryToken path)
		{
			if (path.Kind != QueryTokenKind.Segment)
			{
				return null;
			}
			SegmentQueryToken segmentQueryToken = (SegmentQueryToken)path;
			if (segmentQueryToken.Parent != null)
			{
				return ODataResponse.GetRootSegment(segmentQueryToken.Parent);
			}
			return segmentQueryToken;
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x000D7990 File Offset: 0x000D5B90
		private static Uri SetBaseUriAndGetId(ODataResponse.EntryFeedUriProcessor processor, DetectedPayloadInformationResults detectedResults, ODataPayloadKind payloadKind)
		{
			bool flag = false;
			if (detectedResults.BaseUri != null)
			{
				flag = processor.TrySetBaseUri(detectedResults.BaseUri);
			}
			if (detectedResults.BaseUri != null && detectedResults.IdUri != null)
			{
				if (!flag)
				{
					flag = processor.TrySetBaseUri(detectedResults.BaseUri, detectedResults.IdUri);
				}
				if (flag)
				{
					return detectedResults.IdUri;
				}
			}
			if (!processor.TrySetBaseUri(new Uri(processor.ResponseUri, ".")))
			{
				throw ODataErrors.FeedContainsNoServiceUri(processor.RequestUri, payloadKind);
			}
			return null;
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x000D7A1C File Offset: 0x000D5C1C
		private void Populate(IValueReference result, Uri serviceUri, Uri responseUri)
		{
			this.result = result;
			this.serviceUri = serviceUri;
			this.responseUri = responseUri;
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x000D7A33 File Offset: 0x000D5C33
		private void Populate(ODataServiceDocument serviceDocument, Uri responseUri)
		{
			this.serviceDoc = serviceDocument;
			this.serviceUri = responseUri;
			this.responseUri = responseUri;
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x000D7A4C File Offset: 0x000D5C4C
		private static bool TryGetDirectFeedResult(string feedName, QueryDescriptorQueryToken queryToken, ODataEnvironment environment, IEngineHost host, out Value result)
		{
			result = null;
			ODataSchemaItem odataSchemaItem = new ODataSchemaItem(feedName, "table");
			TypeValue typeValue;
			if (!environment.TypeCatalog.TryGetValue(odataSchemaItem, out typeValue) && !environment.TypeCatalog.TryGetFunction(feedName, out typeValue))
			{
				return false;
			}
			result = ODataCompiler.Decompile(queryToken, feedName, environment, (typeValue.TypeKind == ValueKind.Function) ? QueryResultFunctionValue.CreateFunction(environment, feedName, host, typeValue.AsFunctionType) : new QueryResultTableValue(environment, feedName, host, typeValue.AsTableType), host);
			return result != null;
		}

		// Token: 0x040021D8 RID: 8664
		public static readonly ODataMessageReaderSettings DefaultReaderSettings = new ODataMessageReaderSettings
		{
			EnableAtomMetadataReading = true,
			MaxProtocolVersion = ODataVersion.V3,
			MessageQuotas = new ODataMessageQuotas
			{
				MaxReceivedMessageSize = long.MaxValue
			}
		};

		// Token: 0x040021D9 RID: 8665
		private ODataServiceDocument serviceDoc;

		// Token: 0x040021DA RID: 8666
		private IValueReference result;

		// Token: 0x040021DB RID: 8667
		private Uri serviceUri;

		// Token: 0x040021DC RID: 8668
		private Uri responseUri;

		// Token: 0x020008D2 RID: 2258
		private class EntryFeedUriProcessor
		{
			// Token: 0x0600408D RID: 16525 RVA: 0x000D7B03 File Offset: 0x000D5D03
			public EntryFeedUriProcessor(IEngineHost host, IResource resource, Uri responseUri, Uri requestUri)
			{
				this.requestUri = requestUri;
				this.responseUri = responseUri;
				this.host = host;
				this.resource = resource;
			}

			// Token: 0x170014CD RID: 5325
			// (get) Token: 0x0600408E RID: 16526 RVA: 0x000D7B28 File Offset: 0x000D5D28
			public Uri BaseUri
			{
				get
				{
					return this.baseUri;
				}
			}

			// Token: 0x170014CE RID: 5326
			// (get) Token: 0x0600408F RID: 16527 RVA: 0x000D7B30 File Offset: 0x000D5D30
			public Uri ResponseUri
			{
				get
				{
					return this.responseUri;
				}
			}

			// Token: 0x170014CF RID: 5327
			// (get) Token: 0x06004090 RID: 16528 RVA: 0x000D7B38 File Offset: 0x000D5D38
			public Uri RequestUri
			{
				get
				{
					return this.requestUri;
				}
			}

			// Token: 0x06004091 RID: 16529 RVA: 0x000D7B40 File Offset: 0x000D5D40
			public bool TrySetBaseUri(Uri baseUri)
			{
				return this.TrySetBaseUri(baseUri, this.responseUri) || this.TrySetBaseUri(baseUri, this.requestUri);
			}

			// Token: 0x06004092 RID: 16530 RVA: 0x000D7B60 File Offset: 0x000D5D60
			public bool TrySetBaseUri(Uri baseUri, Uri itemUri)
			{
				if (itemUri.AbsoluteUri.Length < baseUri.AbsoluteUri.Length && string.Equals(itemUri.AbsoluteUri + "/", baseUri.AbsoluteUri, StringComparison.Ordinal))
				{
					itemUri = baseUri;
				}
				Uri uri;
				return this.TrySetBaseAndResponse(baseUri, itemUri) || (ODataUriNormalizer.TryTerminateWithSlash(itemUri, out uri) && this.TrySetBaseAndResponse(baseUri, uri));
			}

			// Token: 0x06004093 RID: 16531 RVA: 0x000D7BCC File Offset: 0x000D5DCC
			private bool TrySetBaseAndResponse(Uri baseUri, Uri itemUri)
			{
				QueryDescriptorQueryToken queryDescriptorQueryToken;
				if (ODataUri.TryParseODataUri(this.host, this.resource, itemUri, baseUri, out queryDescriptorQueryToken))
				{
					this.baseUri = baseUri;
					this.responseUri = itemUri;
					return true;
				}
				return false;
			}

			// Token: 0x040021DD RID: 8669
			private Uri responseUri;

			// Token: 0x040021DE RID: 8670
			private Uri requestUri;

			// Token: 0x040021DF RID: 8671
			private Uri baseUri;

			// Token: 0x040021E0 RID: 8672
			private readonly IEngineHost host;

			// Token: 0x040021E1 RID: 8673
			private readonly IResource resource;
		}
	}
}
