using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000FF RID: 255
	internal sealed class ODataJsonLightWriter : ODataWriterCore
	{
		// Token: 0x060009A1 RID: 2465 RVA: 0x000233A4 File Offset: 0x000215A4
		internal ODataJsonLightWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool writingFeed, bool writingParameter = false, bool writingDelta = false, IODataReaderWriterListener listener = null)
			: base(jsonLightOutputContext, navigationSource, entityType, writingFeed, writingDelta, listener)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightEntryAndFeedSerializer = new ODataJsonLightEntryAndFeedSerializer(this.jsonLightOutputContext);
			this.writingParameter = writingParameter;
			this.jsonWriter = this.jsonLightOutputContext.JsonWriter;
			this.odataAnnotationWriter = new JsonLightODataAnnotationWriter(this.jsonWriter, jsonLightOutputContext.MessageWriterSettings.ODataSimplified);
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00023410 File Offset: 0x00021610
		private ODataJsonLightWriter.JsonLightEntryScope CurrentEntryScope
		{
			get
			{
				return base.CurrentScope as ODataJsonLightWriter.JsonLightEntryScope;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0002342C File Offset: 0x0002162C
		private ODataJsonLightWriter.JsonLightFeedScope CurrentFeedScope
		{
			get
			{
				return base.CurrentScope as ODataJsonLightWriter.JsonLightFeedScope;
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00023446 File Offset: 0x00021646
		protected override void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00023453 File Offset: 0x00021653
		protected override void FlushSynchronously()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00023460 File Offset: 0x00021660
		protected override void StartPayload()
		{
			this.jsonLightEntryAndFeedSerializer.WritePayloadStart();
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0002346D File Offset: 0x0002166D
		protected override void EndPayload()
		{
			this.jsonLightEntryAndFeedSerializer.WritePayloadEnd();
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002347C File Offset: 0x0002167C
		protected override void PrepareEntryForWriteStart(ODataEntry entry, ODataFeedAndEntryTypeContext typeContext, SelectedPropertiesNode selectedProperties)
		{
			if (this.jsonLightOutputContext.MessageWriterSettings.AutoComputePayloadMetadataInJson)
			{
				ODataWriterCore.EntryScope entryScope = (ODataWriterCore.EntryScope)base.CurrentScope;
				ODataEntityMetadataBuilder odataEntityMetadataBuilder = this.jsonLightOutputContext.MetadataLevel.CreateEntityMetadataBuilder(entry, typeContext, entryScope.SerializationInfo, entryScope.EntityType, selectedProperties, this.jsonLightOutputContext.WritingResponse, this.jsonLightOutputContext.MessageWriterSettings.UseKeyAsSegment, this.jsonLightOutputContext.MessageWriterSettings.ODataUri);
				if (odataEntityMetadataBuilder is ODataConventionalEntityMetadataBuilder)
				{
					odataEntityMetadataBuilder.ParentMetadataBuilder = this.FindParentEntryMetadataBuilder();
				}
				this.jsonLightOutputContext.MetadataLevel.InjectMetadataBuilder(entry, odataEntityMetadataBuilder);
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00023518 File Offset: 0x00021718
		protected override void ValidateEntryMediaResource(ODataEntry entry, IEdmEntityType entityType)
		{
			if (this.jsonLightOutputContext.MessageWriterSettings.EnableFullValidation)
			{
				if (this.jsonLightOutputContext.MessageWriterSettings.AutoComputePayloadMetadataInJson && this.jsonLightOutputContext.MetadataLevel is JsonNoMetadataLevel)
				{
					return;
				}
				base.ValidateEntryMediaResource(entry, entityType);
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00023564 File Offset: 0x00021764
		protected override void StartEntry(ODataEntry entry)
		{
			ODataNavigationLink parentNavigationLink = base.ParentNavigationLink;
			if (parentNavigationLink != null)
			{
				this.jsonWriter.WriteName(parentNavigationLink.Name);
			}
			if (entry == null)
			{
				this.jsonWriter.WriteValue(null);
				return;
			}
			this.jsonWriter.StartObjectScope();
			ODataJsonLightWriter.JsonLightEntryScope currentEntryScope = this.CurrentEntryScope;
			if (base.IsTopLevel)
			{
				this.jsonLightEntryAndFeedSerializer.WriteEntryContextUri(currentEntryScope.GetOrCreateTypeContext(this.jsonLightOutputContext.Model, this.jsonLightOutputContext.WritingResponse), null);
			}
			this.jsonLightEntryAndFeedSerializer.WriteEntryStartMetadataProperties(currentEntryScope);
			this.jsonLightEntryAndFeedSerializer.WriteEntryMetadataProperties(currentEntryScope);
			this.jsonLightEntryAndFeedSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(entry.InstanceAnnotations, currentEntryScope.InstanceAnnotationWriteTracker, false, null);
			ProjectedPropertiesAnnotation projectedPropertiesAnnotation = ODataWriterCore.GetProjectedPropertiesAnnotation(currentEntryScope);
			this.jsonLightEntryAndFeedSerializer.WriteProperties(base.EntryEntityType, entry.Properties, false, base.DuplicatePropertyNamesChecker, projectedPropertiesAnnotation);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0002363C File Offset: 0x0002183C
		protected override void EndEntry(ODataEntry entry)
		{
			if (entry == null)
			{
				return;
			}
			ODataJsonLightWriter.JsonLightEntryScope currentEntryScope = this.CurrentEntryScope;
			this.jsonLightEntryAndFeedSerializer.WriteEntryMetadataProperties(currentEntryScope);
			this.jsonLightEntryAndFeedSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(entry.InstanceAnnotations, currentEntryScope.InstanceAnnotationWriteTracker, false, null);
			this.jsonLightEntryAndFeedSerializer.WriteEntryEndMetadataProperties(currentEntryScope, currentEntryScope.DuplicatePropertyNamesChecker);
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002369C File Offset: 0x0002189C
		protected override void StartFeed(ODataFeed feed)
		{
			if (base.ParentNavigationLink == null && this.writingParameter)
			{
				this.jsonWriter.StartArrayScope();
				return;
			}
			if (base.ParentNavigationLink == null)
			{
				this.jsonWriter.StartObjectScope();
				this.jsonLightEntryAndFeedSerializer.WriteFeedContextUri(this.CurrentFeedScope.GetOrCreateTypeContext(this.jsonLightOutputContext.Model, this.jsonLightOutputContext.WritingResponse));
				if (this.jsonLightOutputContext.WritingResponse)
				{
					IEnumerable<ODataAction> actions = feed.Actions;
					if (actions != null && Enumerable.Any<ODataAction>(actions))
					{
						this.jsonLightEntryAndFeedSerializer.WriteOperations(Enumerable.Cast<ODataOperation>(actions), true);
					}
					IEnumerable<ODataFunction> functions = feed.Functions;
					if (functions != null && Enumerable.Any<ODataFunction>(functions))
					{
						this.jsonLightEntryAndFeedSerializer.WriteOperations(Enumerable.Cast<ODataOperation>(functions), false);
					}
					this.WriteFeedCount(feed, null);
					this.WriteFeedNextLink(feed, null);
					this.WriteFeedDeltaLink(feed);
				}
				this.jsonLightEntryAndFeedSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(feed.InstanceAnnotations, this.CurrentFeedScope.InstanceAnnotationWriteTracker, false, null);
				this.jsonWriter.WriteValuePropertyName();
				this.jsonWriter.StartArrayScope();
				return;
			}
			string name = base.ParentNavigationLink.Name;
			base.ValidateNoDeltaLinkForExpandedFeed(feed);
			this.ValidateNoCustomInstanceAnnotationsForExpandedFeed(feed);
			if (this.jsonLightOutputContext.WritingResponse)
			{
				this.WriteFeedCount(feed, name);
				this.WriteFeedNextLink(feed, name);
				this.jsonWriter.WriteName(name);
				this.jsonWriter.StartArrayScope();
				return;
			}
			ODataJsonLightWriter.JsonLightNavigationLinkScope jsonLightNavigationLinkScope = (ODataJsonLightWriter.JsonLightNavigationLinkScope)base.ParentNavigationLinkScope;
			if (!jsonLightNavigationLinkScope.FeedWritten)
			{
				if (jsonLightNavigationLinkScope.EntityReferenceLinkWritten)
				{
					this.jsonWriter.EndArrayScope();
				}
				this.jsonWriter.WriteName(name);
				this.jsonWriter.StartArrayScope();
				jsonLightNavigationLinkScope.FeedWritten = true;
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00023844 File Offset: 0x00021A44
		protected override void EndFeed(ODataFeed feed)
		{
			if (base.ParentNavigationLink == null && this.writingParameter)
			{
				this.jsonWriter.EndArrayScope();
				return;
			}
			if (base.ParentNavigationLink == null)
			{
				this.jsonWriter.EndArrayScope();
				this.jsonLightEntryAndFeedSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(feed.InstanceAnnotations, this.CurrentFeedScope.InstanceAnnotationWriteTracker, false, null);
				if (this.jsonLightOutputContext.WritingResponse)
				{
					this.WriteFeedNextLink(feed, null);
					this.WriteFeedDeltaLink(feed);
				}
				this.jsonWriter.EndObjectScope();
				return;
			}
			string name = base.ParentNavigationLink.Name;
			base.ValidateNoDeltaLinkForExpandedFeed(feed);
			this.ValidateNoCustomInstanceAnnotationsForExpandedFeed(feed);
			if (this.jsonLightOutputContext.WritingResponse)
			{
				this.jsonWriter.EndArrayScope();
				this.WriteFeedNextLink(feed, name);
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00023905 File Offset: 0x00021B05
		protected override void WriteDeferredNavigationLink(ODataNavigationLink navigationLink)
		{
			this.jsonLightEntryAndFeedSerializer.WriteNavigationLinkMetadata(navigationLink, base.DuplicatePropertyNamesChecker);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0002391C File Offset: 0x00021B1C
		protected override void StartNavigationLinkWithContent(ODataNavigationLink navigationLink)
		{
			if (this.jsonLightOutputContext.WritingResponse)
			{
				IEdmContainedEntitySet edmContainedEntitySet = base.CurrentScope.NavigationSource as IEdmContainedEntitySet;
				if (edmContainedEntitySet != null)
				{
					ODataContextUrlInfo odataContextUrlInfo = ODataContextUrlInfo.Create(base.CurrentScope.NavigationSource, base.CurrentScope.EntityType.FullName(), edmContainedEntitySet.NavigationProperty.Type.TypeKind() != EdmTypeKind.Collection, base.CurrentScope.ODataUri);
					this.jsonLightEntryAndFeedSerializer.WriteNavigationLinkContextUrl(navigationLink, odataContextUrlInfo);
				}
				this.jsonLightEntryAndFeedSerializer.WriteNavigationLinkMetadata(navigationLink, base.DuplicatePropertyNamesChecker);
				return;
			}
			this.WriterValidator.ValidateNavigationLinkHasCardinality(navigationLink);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x000239B8 File Offset: 0x00021BB8
		protected override void EndNavigationLinkWithContent(ODataNavigationLink navigationLink)
		{
			if (!this.jsonLightOutputContext.WritingResponse)
			{
				ODataJsonLightWriter.JsonLightNavigationLinkScope jsonLightNavigationLinkScope = (ODataJsonLightWriter.JsonLightNavigationLinkScope)base.CurrentScope;
				if (jsonLightNavigationLinkScope.EntityReferenceLinkWritten && !jsonLightNavigationLinkScope.FeedWritten && navigationLink.IsCollection.Value)
				{
					this.jsonWriter.EndArrayScope();
				}
				if (jsonLightNavigationLinkScope.FeedWritten)
				{
					this.jsonWriter.EndArrayScope();
				}
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00023A1C File Offset: 0x00021C1C
		protected override void WriteEntityReferenceInNavigationLinkContent(ODataNavigationLink parentNavigationLink, ODataEntityReferenceLink entityReferenceLink)
		{
			ODataJsonLightWriter.JsonLightNavigationLinkScope jsonLightNavigationLinkScope = (ODataJsonLightWriter.JsonLightNavigationLinkScope)base.CurrentScope;
			if (jsonLightNavigationLinkScope.FeedWritten)
			{
				throw new ODataException(Strings.ODataJsonLightWriter_EntityReferenceLinkAfterFeedInRequest);
			}
			if (!jsonLightNavigationLinkScope.EntityReferenceLinkWritten)
			{
				this.odataAnnotationWriter.WritePropertyAnnotationName(parentNavigationLink.Name, "odata.bind");
				if (parentNavigationLink.IsCollection.Value)
				{
					this.jsonWriter.StartArrayScope();
				}
				jsonLightNavigationLinkScope.EntityReferenceLinkWritten = true;
			}
			this.jsonWriter.WriteValue(this.jsonLightEntryAndFeedSerializer.UriToString(entityReferenceLink.Url));
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00023AA4 File Offset: 0x00021CA4
		protected override ODataWriterCore.FeedScope CreateFeedScope(ODataFeed feed, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightWriter.JsonLightFeedScope(feed, navigationSource, entityType, skipWriting, selectedProperties, odataUri);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00023AB4 File Offset: 0x00021CB4
		protected override ODataWriterCore.EntryScope CreateEntryScope(ODataEntry entry, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightWriter.JsonLightEntryScope(entry, base.GetEntrySerializationInfo(entry), navigationSource, entityType, skipWriting, this.jsonLightOutputContext.WritingResponse, this.jsonLightOutputContext.MessageWriterSettings.WriterBehavior, selectedProperties, odataUri, this.jsonLightOutputContext.MessageWriterSettings.EnableFullValidation);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00023B01 File Offset: 0x00021D01
		protected override ODataWriterCore.NavigationLinkScope CreateNavigationLinkScope(ODataWriterCore.WriterState writerState, ODataNavigationLink navLink, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightWriter.JsonLightNavigationLinkScope(writerState, navLink, navigationSource, entityType, skipWriting, selectedProperties, odataUri);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00023B14 File Offset: 0x00021D14
		private ODataEntityMetadataBuilder FindParentEntryMetadataBuilder()
		{
			ODataWriterCore.EntryScope parentEntryScope = base.GetParentEntryScope();
			if (parentEntryScope != null)
			{
				ODataEntry odataEntry = parentEntryScope.Item as ODataEntry;
				if (odataEntry != null)
				{
					return odataEntry.MetadataBuilder;
				}
			}
			return null;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00023B44 File Offset: 0x00021D44
		private void WriteFeedCount(ODataFeed feed, string propertyName)
		{
			long? count = feed.Count;
			if (count != null)
			{
				if (propertyName == null)
				{
					this.odataAnnotationWriter.WriteInstanceAnnotationName("odata.count");
				}
				else
				{
					this.odataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.count");
				}
				this.jsonWriter.WriteValue(count.Value);
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00023B9C File Offset: 0x00021D9C
		private void WriteFeedNextLink(ODataFeed feed, string propertyName)
		{
			Uri nextPageLink = feed.NextPageLink;
			if (nextPageLink != null && !this.CurrentFeedScope.NextPageLinkWritten)
			{
				if (propertyName == null)
				{
					this.odataAnnotationWriter.WriteInstanceAnnotationName("odata.nextLink");
				}
				else
				{
					this.odataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.nextLink");
				}
				this.jsonWriter.WriteValue(this.jsonLightEntryAndFeedSerializer.UriToString(nextPageLink));
				this.CurrentFeedScope.NextPageLinkWritten = true;
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00023C10 File Offset: 0x00021E10
		private void WriteFeedDeltaLink(ODataFeed feed)
		{
			Uri deltaLink = feed.DeltaLink;
			if (deltaLink != null && !this.CurrentFeedScope.DeltaLinkWritten)
			{
				this.odataAnnotationWriter.WriteInstanceAnnotationName("odata.deltaLink");
				this.jsonWriter.WriteValue(this.jsonLightEntryAndFeedSerializer.UriToString(deltaLink));
				this.CurrentFeedScope.DeltaLinkWritten = true;
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00023C6D File Offset: 0x00021E6D
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "An instance field is used in a debug assert.")]
		private void ValidateNoCustomInstanceAnnotationsForExpandedFeed(ODataFeed feed)
		{
			if (feed.InstanceAnnotations.Count > 0)
			{
				throw new ODataException(Strings.ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedFeed);
			}
		}

		// Token: 0x040003D4 RID: 980
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x040003D5 RID: 981
		private readonly ODataJsonLightEntryAndFeedSerializer jsonLightEntryAndFeedSerializer;

		// Token: 0x040003D6 RID: 982
		private readonly bool writingParameter;

		// Token: 0x040003D7 RID: 983
		private readonly IJsonWriter jsonWriter;

		// Token: 0x040003D8 RID: 984
		private readonly JsonLightODataAnnotationWriter odataAnnotationWriter;

		// Token: 0x02000100 RID: 256
		private sealed class JsonLightFeedScope : ODataWriterCore.FeedScope
		{
			// Token: 0x060009BA RID: 2490 RVA: 0x00023C88 File Offset: 0x00021E88
			internal JsonLightFeedScope(ODataFeed feed, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(feed, navigationSource, entityType, skipWriting, selectedProperties, odataUri)
			{
			}

			// Token: 0x17000216 RID: 534
			// (get) Token: 0x060009BB RID: 2491 RVA: 0x00023C99 File Offset: 0x00021E99
			// (set) Token: 0x060009BC RID: 2492 RVA: 0x00023CA1 File Offset: 0x00021EA1
			internal bool NextPageLinkWritten
			{
				get
				{
					return this.nextLinkWritten;
				}
				set
				{
					this.nextLinkWritten = value;
				}
			}

			// Token: 0x17000217 RID: 535
			// (get) Token: 0x060009BD RID: 2493 RVA: 0x00023CAA File Offset: 0x00021EAA
			// (set) Token: 0x060009BE RID: 2494 RVA: 0x00023CB2 File Offset: 0x00021EB2
			internal bool DeltaLinkWritten
			{
				get
				{
					return this.deltaLinkWritten;
				}
				set
				{
					this.deltaLinkWritten = value;
				}
			}

			// Token: 0x040003D9 RID: 985
			private bool nextLinkWritten;

			// Token: 0x040003DA RID: 986
			private bool deltaLinkWritten;
		}

		// Token: 0x02000101 RID: 257
		private sealed class JsonLightEntryScope : ODataWriterCore.EntryScope, IODataJsonLightWriterEntryState
		{
			// Token: 0x060009BF RID: 2495 RVA: 0x00023CBC File Offset: 0x00021EBC
			internal JsonLightEntryScope(ODataEntry entry, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, bool writingResponse, ODataWriterBehavior writerBehavior, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool enableValidation)
				: base(entry, serializationInfo, navigationSource, entityType, skipWriting, writingResponse, writerBehavior, selectedProperties, odataUri, enableValidation)
			{
			}

			// Token: 0x17000218 RID: 536
			// (get) Token: 0x060009C0 RID: 2496 RVA: 0x00023CE0 File Offset: 0x00021EE0
			public ODataEntry Entry
			{
				get
				{
					return (ODataEntry)base.Item;
				}
			}

			// Token: 0x17000219 RID: 537
			// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00023CED File Offset: 0x00021EED
			// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00023CF6 File Offset: 0x00021EF6
			public bool EditLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.EditLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.EditLink);
				}
			}

			// Token: 0x1700021A RID: 538
			// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00023CFF File Offset: 0x00021EFF
			// (set) Token: 0x060009C4 RID: 2500 RVA: 0x00023D08 File Offset: 0x00021F08
			public bool ReadLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.ReadLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.ReadLink);
				}
			}

			// Token: 0x1700021B RID: 539
			// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00023D11 File Offset: 0x00021F11
			// (set) Token: 0x060009C6 RID: 2502 RVA: 0x00023D1A File Offset: 0x00021F1A
			public bool MediaEditLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaEditLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaEditLink);
				}
			}

			// Token: 0x1700021C RID: 540
			// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00023D23 File Offset: 0x00021F23
			// (set) Token: 0x060009C8 RID: 2504 RVA: 0x00023D2C File Offset: 0x00021F2C
			public bool MediaReadLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaReadLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaReadLink);
				}
			}

			// Token: 0x1700021D RID: 541
			// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00023D35 File Offset: 0x00021F35
			// (set) Token: 0x060009CA RID: 2506 RVA: 0x00023D3F File Offset: 0x00021F3F
			public bool MediaContentTypeWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaContentType);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaContentType);
				}
			}

			// Token: 0x1700021E RID: 542
			// (get) Token: 0x060009CB RID: 2507 RVA: 0x00023D49 File Offset: 0x00021F49
			// (set) Token: 0x060009CC RID: 2508 RVA: 0x00023D53 File Offset: 0x00021F53
			public bool MediaETagWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaETag);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaETag);
				}
			}

			// Token: 0x060009CD RID: 2509 RVA: 0x00023D5D File Offset: 0x00021F5D
			private void SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty jsonLightMetadataProperty)
			{
				this.alreadyWrittenMetadataProperties |= (int)jsonLightMetadataProperty;
			}

			// Token: 0x060009CE RID: 2510 RVA: 0x00023D6D File Offset: 0x00021F6D
			private bool IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty jsonLightMetadataProperty)
			{
				return (this.alreadyWrittenMetadataProperties & (int)jsonLightMetadataProperty) == (int)jsonLightMetadataProperty;
			}

			// Token: 0x040003DB RID: 987
			private int alreadyWrittenMetadataProperties;

			// Token: 0x02000102 RID: 258
			[Flags]
			private enum JsonLightEntryMetadataProperty
			{
				// Token: 0x040003DD RID: 989
				EditLink = 1,
				// Token: 0x040003DE RID: 990
				ReadLink = 2,
				// Token: 0x040003DF RID: 991
				MediaEditLink = 4,
				// Token: 0x040003E0 RID: 992
				MediaReadLink = 8,
				// Token: 0x040003E1 RID: 993
				MediaContentType = 16,
				// Token: 0x040003E2 RID: 994
				MediaETag = 32
			}
		}

		// Token: 0x02000103 RID: 259
		private sealed class JsonLightNavigationLinkScope : ODataWriterCore.NavigationLinkScope
		{
			// Token: 0x060009CF RID: 2511 RVA: 0x00023D7A File Offset: 0x00021F7A
			internal JsonLightNavigationLinkScope(ODataWriterCore.WriterState writerState, ODataNavigationLink navLink, IEdmNavigationSource navigationSource, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(writerState, navLink, navigationSource, entityType, skipWriting, selectedProperties, odataUri)
			{
			}

			// Token: 0x1700021F RID: 543
			// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00023D8D File Offset: 0x00021F8D
			// (set) Token: 0x060009D1 RID: 2513 RVA: 0x00023D95 File Offset: 0x00021F95
			internal bool EntityReferenceLinkWritten
			{
				get
				{
					return this.entityReferenceLinkWritten;
				}
				set
				{
					this.entityReferenceLinkWritten = value;
				}
			}

			// Token: 0x17000220 RID: 544
			// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00023D9E File Offset: 0x00021F9E
			// (set) Token: 0x060009D3 RID: 2515 RVA: 0x00023DA6 File Offset: 0x00021FA6
			internal bool FeedWritten
			{
				get
				{
					return this.feedWritten;
				}
				set
				{
					this.feedWritten = value;
				}
			}

			// Token: 0x060009D4 RID: 2516 RVA: 0x00023DB0 File Offset: 0x00021FB0
			internal override ODataWriterCore.NavigationLinkScope Clone(ODataWriterCore.WriterState newWriterState)
			{
				return new ODataJsonLightWriter.JsonLightNavigationLinkScope(newWriterState, (ODataNavigationLink)base.Item, base.NavigationSource, base.EntityType, base.SkipWriting, base.SelectedProperties, base.ODataUri)
				{
					EntityReferenceLinkWritten = this.entityReferenceLinkWritten,
					FeedWritten = this.feedWritten
				};
			}

			// Token: 0x040003E3 RID: 995
			private bool entityReferenceLinkWritten;

			// Token: 0x040003E4 RID: 996
			private bool feedWritten;
		}
	}
}
