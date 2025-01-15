using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000290 RID: 656
	internal sealed class ODataVerboseJsonWriter : ODataWriterCore
	{
		// Token: 0x060014DE RID: 5342 RVA: 0x0004C6FF File Offset: 0x0004A8FF
		internal ODataVerboseJsonWriter(ODataVerboseJsonOutputContext jsonOutputContext, IEdmEntitySet entitySet, IEdmEntityType entityType, bool writingFeed)
			: base(jsonOutputContext, entitySet, entityType, writingFeed)
		{
			this.verboseJsonOutputContext = jsonOutputContext;
			this.verboseJsonEntryAndFeedSerializer = new ODataVerboseJsonEntryAndFeedSerializer(this.verboseJsonOutputContext);
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x0004C724 File Offset: 0x0004A924
		private ODataVerboseJsonWriter.VerboseJsonFeedScope CurrentFeedScope
		{
			get
			{
				return base.CurrentScope as ODataVerboseJsonWriter.VerboseJsonFeedScope;
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0004C73E File Offset: 0x0004A93E
		protected override void VerifyNotDisposed()
		{
			this.verboseJsonOutputContext.VerifyNotDisposed();
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0004C74B File Offset: 0x0004A94B
		protected override void FlushSynchronously()
		{
			this.verboseJsonOutputContext.Flush();
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0004C758 File Offset: 0x0004A958
		protected override void StartPayload()
		{
			this.verboseJsonEntryAndFeedSerializer.WritePayloadStart();
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0004C765 File Offset: 0x0004A965
		protected override void EndPayload()
		{
			this.verboseJsonEntryAndFeedSerializer.WritePayloadEnd();
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0004C774 File Offset: 0x0004A974
		protected override void StartEntry(ODataEntry entry)
		{
			if (entry == null)
			{
				this.verboseJsonOutputContext.JsonWriter.WriteValue(null);
				return;
			}
			this.verboseJsonOutputContext.JsonWriter.StartObjectScope();
			ProjectedPropertiesAnnotation projectedPropertiesAnnotation = ODataWriterCore.GetProjectedPropertiesAnnotation(base.CurrentScope);
			this.verboseJsonEntryAndFeedSerializer.WriteEntryMetadata(entry, projectedPropertiesAnnotation, base.EntryEntityType, base.DuplicatePropertyNamesChecker);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0004C7CC File Offset: 0x0004A9CC
		protected override void EndEntry(ODataEntry entry)
		{
			if (entry == null)
			{
				return;
			}
			ProjectedPropertiesAnnotation projectedPropertiesAnnotation = ODataWriterCore.GetProjectedPropertiesAnnotation(base.CurrentScope);
			this.verboseJsonEntryAndFeedSerializer.WriteProperties(base.EntryEntityType, entry.Properties, false, base.DuplicatePropertyNamesChecker, projectedPropertiesAnnotation);
			this.verboseJsonOutputContext.JsonWriter.EndObjectScope();
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0004C818 File Offset: 0x0004AA18
		protected override void StartFeed(ODataFeed feed)
		{
			if (base.ParentNavigationLink == null || this.verboseJsonOutputContext.WritingResponse)
			{
				if (this.verboseJsonOutputContext.Version >= ODataVersion.V2 && this.verboseJsonOutputContext.WritingResponse)
				{
					this.verboseJsonOutputContext.JsonWriter.StartObjectScope();
					this.WriteFeedCount(feed);
					this.verboseJsonOutputContext.JsonWriter.WriteDataArrayName();
				}
				this.verboseJsonOutputContext.JsonWriter.StartArrayScope();
			}
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0004C88C File Offset: 0x0004AA8C
		protected override void EndFeed(ODataFeed feed)
		{
			if (base.ParentNavigationLink == null || this.verboseJsonOutputContext.WritingResponse)
			{
				this.verboseJsonOutputContext.JsonWriter.EndArrayScope();
				Uri nextPageLink = feed.NextPageLink;
				if (this.verboseJsonOutputContext.Version >= ODataVersion.V2 && this.verboseJsonOutputContext.WritingResponse)
				{
					this.WriteFeedCount(feed);
					if (nextPageLink != null)
					{
						this.verboseJsonOutputContext.JsonWriter.WriteName("__next");
						this.verboseJsonOutputContext.JsonWriter.WriteValue(this.verboseJsonEntryAndFeedSerializer.UriToAbsoluteUriString(nextPageLink));
					}
					this.verboseJsonOutputContext.JsonWriter.EndObjectScope();
				}
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0004C934 File Offset: 0x0004AB34
		protected override void WriteDeferredNavigationLink(ODataNavigationLink navigationLink)
		{
			WriterValidationUtils.ValidateNavigationLinkUrlPresent(navigationLink);
			this.verboseJsonOutputContext.JsonWriter.WriteName(navigationLink.Name);
			this.verboseJsonOutputContext.JsonWriter.StartObjectScope();
			this.verboseJsonOutputContext.JsonWriter.WriteName("__deferred");
			this.verboseJsonOutputContext.JsonWriter.StartObjectScope();
			this.verboseJsonOutputContext.JsonWriter.WriteName("uri");
			this.verboseJsonOutputContext.JsonWriter.WriteValue(this.verboseJsonEntryAndFeedSerializer.UriToAbsoluteUriString(navigationLink.Url));
			this.verboseJsonOutputContext.JsonWriter.EndObjectScope();
			this.verboseJsonOutputContext.JsonWriter.EndObjectScope();
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0004C9E8 File Offset: 0x0004ABE8
		protected override void StartNavigationLinkWithContent(ODataNavigationLink navigationLink)
		{
			this.verboseJsonOutputContext.JsonWriter.WriteName(navigationLink.Name);
			if (this.verboseJsonOutputContext.WritingResponse)
			{
				return;
			}
			WriterValidationUtils.ValidateNavigationLinkHasCardinality(navigationLink);
			if (navigationLink.IsCollection.Value)
			{
				this.verboseJsonOutputContext.JsonWriter.StartArrayScope();
			}
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0004CA40 File Offset: 0x0004AC40
		protected override void EndNavigationLinkWithContent(ODataNavigationLink navigationLink)
		{
			if (this.verboseJsonOutputContext.WritingResponse)
			{
				return;
			}
			if (navigationLink.IsCollection.Value)
			{
				this.verboseJsonOutputContext.JsonWriter.EndArrayScope();
			}
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0004CA7C File Offset: 0x0004AC7C
		protected override void WriteEntityReferenceInNavigationLinkContent(ODataNavigationLink parentNavigationLink, ODataEntityReferenceLink entityReferenceLink)
		{
			this.verboseJsonOutputContext.JsonWriter.StartObjectScope();
			this.verboseJsonOutputContext.JsonWriter.WriteName("__metadata");
			this.verboseJsonOutputContext.JsonWriter.StartObjectScope();
			this.verboseJsonOutputContext.JsonWriter.WriteName("uri");
			this.verboseJsonOutputContext.JsonWriter.WriteValue(this.verboseJsonEntryAndFeedSerializer.UriToAbsoluteUriString(entityReferenceLink.Url));
			this.verboseJsonOutputContext.JsonWriter.EndObjectScope();
			this.verboseJsonOutputContext.JsonWriter.EndObjectScope();
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0004CB14 File Offset: 0x0004AD14
		protected override ODataWriterCore.FeedScope CreateFeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			return new ODataVerboseJsonWriter.VerboseJsonFeedScope(feed, entitySet, entityType, skipWriting, selectedProperties);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0004CB22 File Offset: 0x0004AD22
		protected override ODataWriterCore.EntryScope CreateEntryScope(ODataEntry entry, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			return new ODataWriterCore.EntryScope(entry, base.GetEntrySerializationInfo(entry), entitySet, entityType, skipWriting, this.verboseJsonOutputContext.WritingResponse, this.verboseJsonOutputContext.MessageWriterSettings.WriterBehavior, selectedProperties);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0004CB54 File Offset: 0x0004AD54
		private void WriteFeedCount(ODataFeed feed)
		{
			long? count = feed.Count;
			if (count != null && !this.CurrentFeedScope.CountWritten)
			{
				this.verboseJsonOutputContext.JsonWriter.WriteName("__count");
				this.verboseJsonOutputContext.JsonWriter.WriteValue(count.Value);
				this.CurrentFeedScope.CountWritten = true;
			}
		}

		// Token: 0x040008BB RID: 2235
		private readonly ODataVerboseJsonOutputContext verboseJsonOutputContext;

		// Token: 0x040008BC RID: 2236
		private readonly ODataVerboseJsonEntryAndFeedSerializer verboseJsonEntryAndFeedSerializer;

		// Token: 0x02000291 RID: 657
		private sealed class VerboseJsonFeedScope : ODataWriterCore.FeedScope
		{
			// Token: 0x060014EF RID: 5359 RVA: 0x0004CBB6 File Offset: 0x0004ADB6
			internal VerboseJsonFeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
				: base(feed, entitySet, entityType, skipWriting, selectedProperties)
			{
			}

			// Token: 0x17000471 RID: 1137
			// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0004CBC5 File Offset: 0x0004ADC5
			// (set) Token: 0x060014F1 RID: 5361 RVA: 0x0004CBCD File Offset: 0x0004ADCD
			internal bool CountWritten
			{
				get
				{
					return this.countWritten;
				}
				set
				{
					this.countWritten = value;
				}
			}

			// Token: 0x040008BD RID: 2237
			private bool countWritten;
		}
	}
}
