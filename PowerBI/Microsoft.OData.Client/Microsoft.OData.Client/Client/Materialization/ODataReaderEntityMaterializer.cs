using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x0200010D RID: 269
	internal class ODataReaderEntityMaterializer : ODataEntityMaterializer
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x0002B476 File Offset: 0x00029676
		public ODataReaderEntityMaterializer(ODataMessageReader odataMessageReader, ODataReaderWrapper reader, IODataMaterializerContext materializerContext, EntityTrackingAdapter entityTrackingAdapter, QueryComponents queryComponents, Type expectedType, ProjectionPlan materializeEntryPlan)
			: base(materializerContext, entityTrackingAdapter, queryComponents, expectedType, materializeEntryPlan)
		{
			this.messageReader = odataMessageReader;
			this.feedEntryAdapter = new FeedAndEntryMaterializerAdapter(odataMessageReader, reader, materializerContext.Model, entityTrackingAdapter.MergeOption);
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0002B4A8 File Offset: 0x000296A8
		internal override ODataResourceSet CurrentFeed
		{
			get
			{
				return this.feedEntryAdapter.CurrentFeed;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0002B4B5 File Offset: 0x000296B5
		internal override ODataResource CurrentEntry
		{
			get
			{
				return this.feedEntryAdapter.CurrentEntry;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0002B4C2 File Offset: 0x000296C2
		internal override bool IsEndOfStream
		{
			get
			{
				return this.IsDisposed || this.feedEntryAdapter.IsEndOfStream;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0002B4D9 File Offset: 0x000296D9
		internal override long CountValue
		{
			get
			{
				return this.feedEntryAdapter.GetCountValue(!this.IsDisposed);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00004A70 File Offset: 0x00002C70
		internal override bool IsCountable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0002B4EF File Offset: 0x000296EF
		protected override bool IsDisposed
		{
			get
			{
				return this.messageReader == null;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0002B4FA File Offset: 0x000296FA
		protected override ODataFormat Format
		{
			get
			{
				return ODataUtils.GetReadFormat(this.messageReader);
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002B508 File Offset: 0x00029708
		internal static MaterializerEntry ParseSingleEntityPayload(IODataResponseMessage message, ResponseInfo responseInfo, Type expectedType)
		{
			ODataPayloadKind odataPayloadKind = ODataPayloadKind.Resource;
			MaterializerEntry entry;
			using (ODataMessageReader odataMessageReader = ODataMaterializer.CreateODataMessageReader(message, responseInfo, ref odataPayloadKind))
			{
				IEdmType edmType = responseInfo.TypeResolver.ResolveExpectedTypeForReading(expectedType);
				ODataReaderWrapper odataReaderWrapper = ODataReaderWrapper.Create(odataMessageReader, odataPayloadKind, edmType, responseInfo.ResponsePipeline);
				FeedAndEntryMaterializerAdapter feedAndEntryMaterializerAdapter = new FeedAndEntryMaterializerAdapter(odataMessageReader, odataReaderWrapper, responseInfo.Model, responseInfo.MergeOption);
				ODataResource odataResource = null;
				bool flag = false;
				while (feedAndEntryMaterializerAdapter.Read())
				{
					flag |= feedAndEntryMaterializerAdapter.CurrentFeed != null;
					if (feedAndEntryMaterializerAdapter.CurrentEntry != null)
					{
						if (odataResource != null)
						{
							throw new InvalidOperationException(Strings.AtomParser_SingleEntry_MultipleFound);
						}
						odataResource = feedAndEntryMaterializerAdapter.CurrentEntry;
					}
				}
				if (odataResource == null)
				{
					if (flag)
					{
						throw new InvalidOperationException(Strings.AtomParser_SingleEntry_NoneFound);
					}
					throw new InvalidOperationException(Strings.AtomParser_SingleEntry_ExpectedFeedOrEntry);
				}
				else
				{
					entry = MaterializerEntry.GetEntry(odataResource);
				}
			}
			return entry;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002B5D8 File Offset: 0x000297D8
		protected override void OnDispose()
		{
			if (this.messageReader != null)
			{
				this.messageReader.Dispose();
				this.messageReader = null;
			}
			this.feedEntryAdapter.Dispose();
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002B5FF File Offset: 0x000297FF
		protected override bool ReadNextFeedOrEntry()
		{
			return this.feedEntryAdapter.Read();
		}

		// Token: 0x04000640 RID: 1600
		private FeedAndEntryMaterializerAdapter feedEntryAdapter;

		// Token: 0x04000641 RID: 1601
		private ODataMessageReader messageReader;
	}
}
