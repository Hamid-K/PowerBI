using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008EA RID: 2282
	internal sealed class RequestInfo
	{
		// Token: 0x0600411C RID: 16668 RVA: 0x000D9F67 File Offset: 0x000D8167
		public RequestInfo(Uri uri, ODataEnvironment environment, TypeValue type, bool expectSingleton)
		{
			this.uri = uri;
			this.expectSingleton = expectSingleton;
			this.type = type;
			this.environment = environment;
			this.completeEvent = new ManualResetEvent(false);
		}

		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x0600411D RID: 16669 RVA: 0x000D9F98 File Offset: 0x000D8198
		public EventWaitHandle Complete
		{
			get
			{
				return this.completeEvent;
			}
		}

		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x0600411E RID: 16670 RVA: 0x000D9FA0 File Offset: 0x000D81A0
		public ODataEnvironment Environment
		{
			get
			{
				return this.environment;
			}
		}

		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x0600411F RID: 16671 RVA: 0x000D9FA8 File Offset: 0x000D81A8
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x06004120 RID: 16672 RVA: 0x000D9FB0 File Offset: 0x000D81B0
		public bool ExpectSingleton
		{
			get
			{
				return this.expectSingleton;
			}
		}

		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x06004121 RID: 16673 RVA: 0x000D9FB8 File Offset: 0x000D81B8
		public List<IValueReference> Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x06004122 RID: 16674 RVA: 0x000D9FC0 File Offset: 0x000D81C0
		public TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x06004123 RID: 16675 RVA: 0x000D9FC8 File Offset: 0x000D81C8
		public TypeValue ItemType
		{
			get
			{
				if (this.type.TypeKind != ValueKind.Table)
				{
					return this.type.AsListType.ItemType;
				}
				return this.type.AsTableType.ItemType;
			}
		}

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x06004124 RID: 16676 RVA: 0x000D9FFA File Offset: 0x000D81FA
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x06004125 RID: 16677 RVA: 0x000DA002 File Offset: 0x000D8202
		// (set) Token: 0x06004126 RID: 16678 RVA: 0x000DA00A File Offset: 0x000D820A
		public Uri NextPageUri { get; set; }

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x06004127 RID: 16679 RVA: 0x000DA013 File Offset: 0x000D8213
		// (set) Token: 0x06004128 RID: 16680 RVA: 0x000DA01B File Offset: 0x000D821B
		public ODataResponseMessage Response { get; set; }

		// Token: 0x06004129 RID: 16681 RVA: 0x000DA024 File Offset: 0x000D8224
		public void CreateResult()
		{
			using (IHostTrace hostTrace = TracingService.CreateTrace(this.Environment.Host, "Engine/IO/OData/CreateResult", TraceEventType.Information, this.Environment.Resource))
			{
				hostTrace.Add("RequestUri", this.Uri, true);
				try
				{
					this.result = this.environment.Settings.FallbackHandler.HandleVersionFallback<List<IValueReference>>(new Func<List<IValueReference>>(this.RequestResult), true);
				}
				catch (Exception ex)
				{
					this.exception = ex;
					SafeExceptions.TraceIsSafeException(hostTrace, ex);
				}
				finally
				{
					this.Complete.Set();
				}
			}
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x000DA0E0 File Offset: 0x000D82E0
		private List<IValueReference> RequestResult()
		{
			using (ODataResponseMessage odataResponseMessage = new ODataResponseMessage(Http.GetResult(this.Uri, this.Environment)))
			{
				this.Response = odataResponseMessage;
				if (odataResponseMessage.StatusCode != 204)
				{
					object obj = RequestInfo.creatorLockObject;
					lock (obj)
					{
						try
						{
							using (ODataMessageReader odataMessageReader = new ODataMessageReader(odataResponseMessage, ODataResponse.GetReaderSettings(this.Environment.ServiceUri), this.Environment.EdmModel))
							{
								List<ODataPayloadKindDetectionResult> list = new List<ODataPayloadKindDetectionResult>(odataMessageReader.DetectPayloadKind());
								if (list.Count >= 1)
								{
									ODataPayloadKindDetectionResult odataPayloadKindDetectionResult = list[0];
									switch (odataPayloadKindDetectionResult.PayloadKind)
									{
									case ODataPayloadKind.Feed:
									case ODataPayloadKind.Entry:
										return this.ReadFeedOrEntryResult(odataPayloadKindDetectionResult.PayloadKind, odataMessageReader);
									case ODataPayloadKind.Property:
										if (list.Count == 2 && list[1].PayloadKind == ODataPayloadKind.Collection)
										{
											return ODataMessageReaderValueConverters.CreateCollectionResult(this.ItemType, odataMessageReader.CreateODataCollectionReader(), this.environment);
										}
										return new List<IValueReference> { ODataMessageReaderValueConverters.CreatePropertyResult(this.ItemType, odataMessageReader, this.environment) };
									case ODataPayloadKind.Value:
									case ODataPayloadKind.BinaryValue:
										return new List<IValueReference> { ODataMessageReaderValueConverters.CreateRawResult(this.ItemType, odataMessageReader, this.environment) };
									case ODataPayloadKind.Collection:
										return ODataMessageReaderValueConverters.CreateCollectionResult(this.ItemType, odataMessageReader.CreateODataCollectionReader(), this.environment);
									case ODataPayloadKind.Error:
										throw ODataMessageReaderValueConverters.CreateErrorResult(this.environment.Host, odataMessageReader, this.Uri, this.Environment.Resource.Kind);
									case ODataPayloadKind.Batch:
									{
										ODataPayloadKind odataPayloadKind;
										return ODataMessageReaderValueConverters.CreateODataBatch(this.Environment.Host, this.Environment.Resource.Kind, this.Environment.Settings, odataMessageReader, odataResponseMessage, this.Uri, (ODataMessageReader innerReader, ODataPayloadKind kind) => this.ReadFeedOrEntryResult(kind, innerReader), out odataPayloadKind, null);
									}
									}
									throw ODataCommonErrors.UnsupportedPayload(odataPayloadKindDetectionResult.PayloadKind.ToString());
								}
								this.Environment.Settings.FallbackHandler.FallbackToOlderVersionIfPossible(ODataCommonErrors.UnsupportedFormat(odataResponseMessage.ContentTypes.First<string>()));
							}
						}
						catch (ODataException ex)
						{
							throw ODataCommonErrors.ODataFailedToParseODataResult(this.environment.Host, ex, this.Uri, this.Environment.Resource.Kind);
						}
						catch (IOException ex2)
						{
							throw ODataCommonErrors.ODataFailedToParseODataResult(this.environment.Host, ex2, this.Uri, this.Environment.Resource.Kind);
						}
						catch (XmlException ex3)
						{
							throw ODataCommonErrors.ODataFailedToParseODataResult(this.environment.Host, ex3, this.Uri, this.Environment.Resource.Kind);
						}
						catch (FormatException ex4)
						{
							throw ODataCommonErrors.ODataFailedToParseODataResult(this.environment.Host, ex4, this.Uri, this.Environment.Resource.Kind);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x000DA4A0 File Offset: 0x000D86A0
		private List<IValueReference> ReadFeedOrEntryResult(ODataPayloadKind kind, ODataMessageReader reader)
		{
			if (kind == ODataPayloadKind.Entry)
			{
				return ODataMessageReaderValueConverters.CreateFeedEntryResult(this, reader.CreateODataEntryReader());
			}
			Value value;
			if (this.Type.TryGetMetaField("EdmType", out value))
			{
				IEdmEntityType edmEntityType = (IEdmEntityType)this.Environment.EdmModel.FindType(value.AsString);
				if (edmEntityType != null)
				{
					return ODataMessageReaderValueConverters.CreateFeedEntryResult(this, reader.CreateODataFeedReader(edmEntityType));
				}
			}
			return ODataMessageReaderValueConverters.CreateFeedEntryResult(this, reader.CreateODataFeedReader());
		}

		// Token: 0x04002234 RID: 8756
		private static readonly object creatorLockObject = new object();

		// Token: 0x04002235 RID: 8757
		private readonly ManualResetEvent completeEvent;

		// Token: 0x04002236 RID: 8758
		private readonly Uri uri;

		// Token: 0x04002237 RID: 8759
		private readonly bool expectSingleton;

		// Token: 0x04002238 RID: 8760
		private readonly ODataEnvironment environment;

		// Token: 0x04002239 RID: 8761
		private List<IValueReference> result;

		// Token: 0x0400223A RID: 8762
		private Exception exception;

		// Token: 0x0400223B RID: 8763
		private TypeValue type;
	}
}
