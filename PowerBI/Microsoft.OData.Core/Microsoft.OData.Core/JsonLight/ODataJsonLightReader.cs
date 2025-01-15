using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200024E RID: 590
	internal sealed class ODataJsonLightReader : ODataReaderCoreAsync
	{
		// Token: 0x06001A46 RID: 6726 RVA: 0x0004F1D8 File Offset: 0x0004D3D8
		internal ODataJsonLightReader(ODataJsonLightInputContext jsonLightInputContext, IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType, bool readingResourceSet, bool readingParameter = false, bool readingDelta = false, IODataReaderWriterListener listener = null)
			: base(jsonLightInputContext, readingResourceSet, readingDelta, listener)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightResourceDeserializer = new ODataJsonLightResourceDeserializer(jsonLightInputContext);
			this.readingParameter = readingParameter;
			this.topLevelScope = new ODataJsonLightReader.JsonLightTopLevelScope(navigationSource, expectedResourceType, new ODataUri());
			base.EnterScope(this.topLevelScope);
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x0004F22B File Offset: 0x0004D42B
		private IODataJsonLightReaderResourceState CurrentResourceState
		{
			get
			{
				return (IODataJsonLightReaderResourceState)base.CurrentScope;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x0004F238 File Offset: 0x0004D438
		private ODataJsonLightReader.JsonLightResourceSetScope CurrentJsonLightResourceSetScope
		{
			get
			{
				return (ODataJsonLightReader.JsonLightResourceSetScope)base.CurrentScope;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x0004F245 File Offset: 0x0004D445
		private ODataJsonLightReader.JsonLightNestedResourceInfoScope CurrentJsonLightNestedResourceInfoScope
		{
			get
			{
				return (ODataJsonLightReader.JsonLightNestedResourceInfoScope)base.CurrentScope;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x0004F254 File Offset: 0x0004D454
		private ODataNestedResourceInfo ParentNestedInfo
		{
			get
			{
				ODataReaderCore.Scope scope = base.SeekScope<ODataJsonLightReader.JsonLightNestedResourceInfoScope>(3);
				if (scope == null)
				{
					return null;
				}
				return (ODataNestedResourceInfo)scope.Item;
			}
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0004F27C File Offset: 0x0004D47C
		protected override bool ReadAtStartImplementation()
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = this.jsonLightInputContext.CreatePropertyAndAnnotationCollector();
			ODataPayloadKind odataPayloadKind = (base.ReadingDelta ? ODataPayloadKind.Delta : (base.ReadingResourceSet ? ODataPayloadKind.ResourceSet : ODataPayloadKind.Resource));
			this.jsonLightResourceDeserializer.ReadPayloadStart(odataPayloadKind, propertyAndAnnotationCollector, base.IsReadingNestedPayload || this.readingParameter, false);
			this.ResolveScopeInfoFromContextUrl();
			ODataReaderCore.Scope currentScope = base.CurrentScope;
			if (this.jsonLightInputContext.Model.IsUserModel())
			{
				IEnumerable<string> derivedTypeConstraints = this.jsonLightInputContext.Model.GetDerivedTypeConstraints(currentScope.NavigationSource);
				if (derivedTypeConstraints != null)
				{
					currentScope.DerivedTypeValidator = new DerivedTypeValidator(currentScope.ResourceType, derivedTypeConstraints, "navigation source", currentScope.NavigationSource.Name);
				}
			}
			return this.ReadAtStartImplementationSynchronously(propertyAndAnnotationCollector);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0004F330 File Offset: 0x0004D530
		protected override Task<bool> ReadAtStartImplementationAsync()
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = this.jsonLightInputContext.CreatePropertyAndAnnotationCollector();
			ODataPayloadKind odataPayloadKind = (base.ReadingDelta ? ODataPayloadKind.Delta : (base.ReadingResourceSet ? ODataPayloadKind.ResourceSet : ODataPayloadKind.Resource));
			return this.jsonLightResourceDeserializer.ReadPayloadStartAsync(odataPayloadKind, propertyAndAnnotationCollector, base.IsReadingNestedPayload, false).FollowOnSuccessWith(delegate(Task t)
			{
				this.ResolveScopeInfoFromContextUrl();
			}).FollowOnSuccessWith((Task t) => this.ReadAtStartImplementationSynchronously(propertyAndAnnotationCollector));
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0004F3AF File Offset: 0x0004D5AF
		protected override bool ReadAtResourceSetStartImplementation()
		{
			return this.ReadAtResourceSetStartImplementationSynchronously();
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0004F3B7 File Offset: 0x0004D5B7
		protected override Task<bool> ReadAtResourceSetStartImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtResourceSetStartImplementationSynchronously));
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0004F3CA File Offset: 0x0004D5CA
		protected override bool ReadAtResourceSetEndImplementation()
		{
			return this.ReadAtResourceSetEndImplementationSynchronously();
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0004F3D2 File Offset: 0x0004D5D2
		protected override Task<bool> ReadAtResourceSetEndImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtResourceSetEndImplementationSynchronously));
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0004F3E5 File Offset: 0x0004D5E5
		protected override bool ReadAtResourceStartImplementation()
		{
			return this.ReadAtResourceStartImplementationSynchronously();
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x0004F3ED File Offset: 0x0004D5ED
		protected override Task<bool> ReadAtResourceStartImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtResourceStartImplementationSynchronously));
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x0004F400 File Offset: 0x0004D600
		protected override bool ReadAtResourceEndImplementation()
		{
			return this.ReadAtResourceEndImplementationSynchronously();
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x0004F408 File Offset: 0x0004D608
		protected override Task<bool> ReadAtResourceEndImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtResourceEndImplementationSynchronously));
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x0004F41B File Offset: 0x0004D61B
		protected override bool ReadAtPrimitiveImplementation()
		{
			return this.ReadAtPrimitiveSynchronously();
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x0004F423 File Offset: 0x0004D623
		protected override Task<bool> ReadAtPrimitiveImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtPrimitiveSynchronously));
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x0004F436 File Offset: 0x0004D636
		protected override bool ReadAtNestedPropertyInfoImplementation()
		{
			return this.ReadAtNestedPropertyInfoSynchronously();
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x0004F43E File Offset: 0x0004D63E
		protected override Task<bool> ReadAtNestedPropertyInfoImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtNestedPropertyInfoSynchronously));
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0004F451 File Offset: 0x0004D651
		protected override bool ReadAtStreamImplementation()
		{
			return this.ReadAtStreamSynchronously();
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0004F459 File Offset: 0x0004D659
		protected override Task<bool> ReadAtStreamImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtStreamSynchronously));
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0004F46C File Offset: 0x0004D66C
		protected override Stream CreateReadStreamImplementation()
		{
			IJsonStreamReader jsonReader = this.jsonLightInputContext.JsonReader;
			Stream stream;
			if (jsonReader != null)
			{
				stream = jsonReader.CreateReadStream();
			}
			else
			{
				this.jsonLightInputContext.JsonReader.Read();
				string text = this.jsonLightInputContext.JsonReader.ReadStringValue();
				stream = new MemoryStream(Convert.FromBase64String(text.Replace('_', '/').Replace('-', '+')));
			}
			return stream;
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0004F4D4 File Offset: 0x0004D6D4
		protected override TextReader CreateTextReaderImplementation()
		{
			IJsonStreamReader jsonReader = this.jsonLightInputContext.JsonReader;
			TextReader textReader;
			if (jsonReader != null)
			{
				textReader = jsonReader.CreateTextReader();
			}
			else
			{
				this.jsonLightInputContext.JsonReader.Read();
				string text = this.jsonLightInputContext.JsonReader.ReadStringValue();
				textReader = new StringReader(text);
			}
			return textReader;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x0004F523 File Offset: 0x0004D723
		protected override bool ReadAtNestedResourceInfoStartImplementation()
		{
			return this.ReadAtNestedResourceInfoStartImplementationSynchronously();
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0004F52B File Offset: 0x0004D72B
		protected override Task<bool> ReadAtNestedResourceInfoStartImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtNestedResourceInfoStartImplementationSynchronously));
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x0004F53E File Offset: 0x0004D73E
		protected override bool ReadAtNestedResourceInfoEndImplementation()
		{
			return this.ReadAtNestedResourceInfoEndImplementationSynchronously();
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0004F546 File Offset: 0x0004D746
		protected override Task<bool> ReadAtNestedResourceInfoEndImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtNestedResourceInfoEndImplementationSynchronously));
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0004F559 File Offset: 0x0004D759
		protected override bool ReadAtEntityReferenceLink()
		{
			return this.ReadAtEntityReferenceLinkSynchronously();
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0004F561 File Offset: 0x0004D761
		protected override Task<bool> ReadAtEntityReferenceLinkAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtEntityReferenceLinkSynchronously));
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0004F3AF File Offset: 0x0004D5AF
		protected override bool ReadAtDeltaResourceSetStartImplementation()
		{
			return this.ReadAtResourceSetStartImplementationSynchronously();
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0004F3B7 File Offset: 0x0004D5B7
		protected override Task<bool> ReadAtDeltaResourceSetStartImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtResourceSetStartImplementationSynchronously));
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0004F3CA File Offset: 0x0004D5CA
		protected override bool ReadAtDeltaResourceSetEndImplementation()
		{
			return this.ReadAtResourceSetEndImplementationSynchronously();
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x0004F3D2 File Offset: 0x0004D5D2
		protected override Task<bool> ReadAtDeltaResourceSetEndImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtResourceSetEndImplementationSynchronously));
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0004F574 File Offset: 0x0004D774
		protected override bool ReadAtDeletedResourceStartImplementation()
		{
			return this.ReadAtDeletedResourceStartImplementationSynchronously();
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0004F57C File Offset: 0x0004D77C
		protected override Task<bool> ReadAtDeletedResourceStartImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtDeletedResourceStartImplementationSynchronously));
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0004F400 File Offset: 0x0004D600
		protected override bool ReadAtDeletedResourceEndImplementation()
		{
			return this.ReadAtResourceEndImplementationSynchronously();
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x0004F408 File Offset: 0x0004D608
		protected override Task<bool> ReadAtDeletedResourceEndImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtResourceEndImplementationSynchronously));
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x0004F58F File Offset: 0x0004D78F
		protected override bool ReadAtDeltaLinkImplementation()
		{
			return this.ReadAtDeltaLinkImplementationSynchronously();
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x0004F597 File Offset: 0x0004D797
		protected override Task<bool> ReadAtDeltaLinkImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtDeltaLinkImplementationSynchronously));
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0004F5AA File Offset: 0x0004D7AA
		protected override bool ReadAtDeltaDeletedLinkImplementation()
		{
			return this.ReadAtDeltaDeletedLinkImplementationSynchronously();
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x0004F5B2 File Offset: 0x0004D7B2
		protected override Task<bool> ReadAtDeltaDeletedLinkImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtDeltaDeletedLinkImplementationSynchronously));
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0004F5C8 File Offset: 0x0004D7C8
		private bool ReadAtStartImplementationSynchronously(PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			if (this.jsonLightInputContext.ReadingResponse && !base.IsReadingNestedPayload)
			{
				ReaderValidationUtils.ValidateResourceSetOrResourceContextUri(this.jsonLightResourceDeserializer.ContextUriParseResult, base.CurrentScope, true);
			}
			string text = ((this.jsonLightResourceDeserializer.ContextUriParseResult == null) ? null : this.jsonLightResourceDeserializer.ContextUriParseResult.SelectQueryOption);
			SelectedPropertiesNode selectedPropertiesNode = SelectedPropertiesNode.Create(text, (base.CurrentResourceTypeReference != null) ? base.CurrentResourceTypeReference.AsStructured().StructuredDefinition() : null, this.jsonLightInputContext.Model);
			if (base.ReadingResourceSet)
			{
				this.topLevelScope.PropertyAndAnnotationCollector = propertyAndAnnotationCollector;
				bool flag = this.jsonLightInputContext.JsonReader is ReorderingJsonReader;
				if (base.ReadingDelta)
				{
					ODataDeltaResourceSet odataDeltaResourceSet = new ODataDeltaResourceSet();
					this.jsonLightResourceDeserializer.ReadTopLevelResourceSetAnnotations(odataDeltaResourceSet, propertyAndAnnotationCollector, true, flag);
					this.ReadDeltaResourceSetStart(odataDeltaResourceSet, selectedPropertiesNode);
				}
				else
				{
					ODataResourceSet odataResourceSet = new ODataResourceSet();
					if (!base.IsReadingNestedPayload)
					{
						if (!this.readingParameter)
						{
							this.jsonLightResourceDeserializer.ReadTopLevelResourceSetAnnotations(odataResourceSet, propertyAndAnnotationCollector, true, flag);
						}
						else
						{
							this.jsonLightResourceDeserializer.JsonReader.Read();
						}
					}
					this.ReadResourceSetStart(odataResourceSet, selectedPropertiesNode);
				}
				return true;
			}
			this.ReadResourceSetItemStart(propertyAndAnnotationCollector, selectedPropertiesNode);
			return true;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0004F6EF File Offset: 0x0004D8EF
		private bool ReadAtResourceSetStartImplementationSynchronously()
		{
			this.ReadNextResourceSetItem();
			return true;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0004F6F8 File Offset: 0x0004D8F8
		private bool ReadAtResourceSetEndImplementationSynchronously()
		{
			bool isTopLevel = base.IsTopLevel;
			bool isExpandedLinkContent = base.IsExpandedLinkContent;
			base.PopScope((this.State == ODataReaderState.ResourceSetEnd) ? ODataReaderState.ResourceSetEnd : ODataReaderState.DeltaResourceSetEnd);
			if ((base.IsReadingNestedPayload || this.readingParameter) && isTopLevel)
			{
				this.ReplaceScope(ODataReaderState.Completed);
				return false;
			}
			if (isTopLevel)
			{
				this.jsonLightResourceDeserializer.JsonReader.Read();
				this.jsonLightResourceDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
				this.ReplaceScope(ODataReaderState.Completed);
				return false;
			}
			if (isExpandedLinkContent)
			{
				this.ReadExpandedNestedResourceInfoEnd(true);
				return true;
			}
			this.ReadNextResourceSetItem();
			return true;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0004F788 File Offset: 0x0004D988
		private bool ReadAtResourceStartImplementationSynchronously()
		{
			ODataResourceBase odataResourceBase = this.Item as ODataResourceBase;
			if (odataResourceBase != null && !base.IsReadingNestedPayload)
			{
				this.CurrentResourceState.ResourceTypeFromMetadata = base.ParentScope.ResourceType as IEdmStructuredType;
				ODataResourceMetadataBuilder resourceMetadataBuilderForReader = this.jsonLightResourceDeserializer.MetadataContext.GetResourceMetadataBuilderForReader(this.CurrentResourceState, this.jsonLightInputContext.ODataSimplifiedOptions.EnableReadingKeyAsSegment, base.ReadingDelta);
				if (resourceMetadataBuilderForReader != odataResourceBase.MetadataBuilder)
				{
					ODataNestedResourceInfo parentNestedInfo = this.ParentNestedInfo;
					ODataConventionalResourceMetadataBuilder odataConventionalResourceMetadataBuilder = resourceMetadataBuilderForReader as ODataConventionalResourceMetadataBuilder;
					if (odataConventionalResourceMetadataBuilder != null)
					{
						if (parentNestedInfo != null)
						{
							odataConventionalResourceMetadataBuilder.NameAsProperty = parentNestedInfo.Name;
							odataConventionalResourceMetadataBuilder.IsFromCollection = parentNestedInfo.IsCollection == true;
							odataConventionalResourceMetadataBuilder.ODataUri = this.ResolveODataUriFromContextUrl(parentNestedInfo) ?? base.CurrentScope.ODataUri;
						}
						odataConventionalResourceMetadataBuilder.StartResource();
					}
					odataResourceBase.MetadataBuilder = resourceMetadataBuilderForReader;
					if (parentNestedInfo != null && parentNestedInfo.MetadataBuilder != null)
					{
						odataResourceBase.MetadataBuilder.ParentMetadataBuilder = parentNestedInfo.MetadataBuilder;
					}
				}
			}
			if (odataResourceBase == null)
			{
				this.EndEntry();
			}
			else if (this.CurrentResourceState.FirstNestedInfo != null)
			{
				this.ReadNestedInfo(this.CurrentResourceState.FirstNestedInfo);
			}
			else
			{
				this.EndEntry();
			}
			return true;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x0004F8C4 File Offset: 0x0004DAC4
		private ODataUri ResolveODataUriFromContextUrl(ODataNestedResourceInfo nestedInfo)
		{
			if (nestedInfo != null && nestedInfo.ContextUrl != null)
			{
				ODataPayloadKind odataPayloadKind = (nestedInfo.IsCollection.GetValueOrDefault() ? ODataPayloadKind.ResourceSet : ODataPayloadKind.Resource);
				ODataPath path = ODataJsonLightContextUriParser.Parse(this.jsonLightResourceDeserializer.Model, UriUtils.UriToString(nestedInfo.ContextUrl), odataPayloadKind, this.jsonLightResourceDeserializer.MessageReaderSettings.ClientCustomTypeResolver, this.jsonLightResourceDeserializer.JsonLightInputContext.ReadingResponse, true).Path;
				return new ODataUri
				{
					Path = path
				};
			}
			return null;
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0004F948 File Offset: 0x0004DB48
		private bool ReadAtResourceEndImplementationSynchronously()
		{
			bool isTopLevel = base.IsTopLevel;
			bool isExpandedLinkContent = base.IsExpandedLinkContent;
			base.PopScope((this.State == ODataReaderState.ResourceEnd) ? ODataReaderState.ResourceEnd : ODataReaderState.DeletedResourceEnd);
			this.jsonLightResourceDeserializer.JsonReader.Read();
			bool flag = true;
			if (isTopLevel)
			{
				this.jsonLightResourceDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
				this.ReplaceScope(ODataReaderState.Completed);
				flag = false;
			}
			else if (isExpandedLinkContent)
			{
				this.ReadExpandedNestedResourceInfoEnd(false);
			}
			else
			{
				this.ReadNextResourceSetItem();
			}
			return flag;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0004F9BD File Offset: 0x0004DBBD
		private bool ReadAtPrimitiveSynchronously()
		{
			base.PopScope(ODataReaderState.Primitive);
			this.jsonLightResourceDeserializer.JsonReader.Read();
			this.ReadNextResourceSetItem();
			return true;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0004F9DF File Offset: 0x0004DBDF
		private bool ReadAtDeletedResourceStartImplementationSynchronously()
		{
			if (((ODataJsonLightReader.JsonLightDeletedResourceScope)base.CurrentScope).Is40DeletedResource)
			{
				this.EndEntry();
				return true;
			}
			return this.ReadAtResourceStartImplementationSynchronously();
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x0004FA01 File Offset: 0x0004DC01
		private bool ReadAtDeltaLinkImplementationSynchronously()
		{
			return this.EndDeltaLink(ODataReaderState.DeltaLink);
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0004FA0B File Offset: 0x0004DC0B
		private bool ReadAtDeltaDeletedLinkImplementationSynchronously()
		{
			return this.EndDeltaLink(ODataReaderState.DeltaDeletedLink);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0004FA15 File Offset: 0x0004DC15
		private bool EndDeltaLink(ODataReaderState readerState)
		{
			base.PopScope(readerState);
			this.jsonLightResourceDeserializer.JsonReader.Read();
			this.ReadNextResourceSetItem();
			return true;
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0004FA38 File Offset: 0x0004DC38
		private bool ReadAtNestedResourceInfoStartImplementationSynchronously()
		{
			ODataNestedResourceInfo currentNestedResourceInfo = base.CurrentNestedResourceInfo;
			IODataJsonLightReaderResourceState iodataJsonLightReaderResourceState = (IODataJsonLightReaderResourceState)base.ParentScope;
			if (this.jsonLightInputContext.ReadingResponse)
			{
				if (iodataJsonLightReaderResourceState.ProcessingMissingProjectedNestedResourceInfos)
				{
					this.ReplaceScope(ODataReaderState.NestedResourceInfoEnd);
				}
				else if (!this.jsonLightResourceDeserializer.JsonReader.IsOnValueNode())
				{
					ReaderUtils.CheckForDuplicateNestedResourceInfoNameAndSetAssociationLink(iodataJsonLightReaderResourceState.PropertyAndAnnotationCollector, currentNestedResourceInfo);
					iodataJsonLightReaderResourceState.NavigationPropertiesRead.Add(currentNestedResourceInfo.Name);
					this.ReplaceScope(ODataReaderState.NestedResourceInfoEnd);
				}
				else if (!currentNestedResourceInfo.IsCollection.Value)
				{
					ReaderUtils.CheckForDuplicateNestedResourceInfoNameAndSetAssociationLink(iodataJsonLightReaderResourceState.PropertyAndAnnotationCollector, currentNestedResourceInfo);
					this.ReadExpandedNestedResourceInfoStart(currentNestedResourceInfo);
				}
				else
				{
					ReaderUtils.CheckForDuplicateNestedResourceInfoNameAndSetAssociationLink(iodataJsonLightReaderResourceState.PropertyAndAnnotationCollector, currentNestedResourceInfo);
					ODataJsonLightReaderNestedResourceInfo readerNestedResourceInfo = this.CurrentJsonLightNestedResourceInfoScope.ReaderNestedResourceInfo;
					ODataJsonLightReader.JsonLightResourceBaseScope jsonLightResourceBaseScope = (ODataJsonLightReader.JsonLightResourceBaseScope)base.ParentScope;
					SelectedPropertiesNode selectedProperties = jsonLightResourceBaseScope.SelectedProperties;
					ODataResourceSet odataResourceSet = readerNestedResourceInfo.NestedResourceSet as ODataResourceSet;
					if (odataResourceSet != null)
					{
						this.ReadResourceSetStart(odataResourceSet, selectedProperties.GetSelectedPropertiesForNavigationProperty(jsonLightResourceBaseScope.ResourceType, currentNestedResourceInfo.Name));
					}
					else
					{
						ODataDeltaResourceSet odataDeltaResourceSet = readerNestedResourceInfo.NestedResourceSet as ODataDeltaResourceSet;
						this.ReadDeltaResourceSetStart(odataDeltaResourceSet, selectedProperties.GetSelectedPropertiesForNavigationProperty(jsonLightResourceBaseScope.ResourceType, currentNestedResourceInfo.Name));
					}
				}
			}
			else
			{
				ReaderUtils.CheckForDuplicateNestedResourceInfoNameAndSetAssociationLink(iodataJsonLightReaderResourceState.PropertyAndAnnotationCollector, currentNestedResourceInfo);
				this.ReadNextNestedResourceInfoContentItemInRequest();
			}
			return true;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0004FB7A File Offset: 0x0004DD7A
		private bool ReadAtNestedResourceInfoEndImplementationSynchronously()
		{
			base.PopScope(ODataReaderState.NestedResourceInfoEnd);
			return this.ReadNextNestedInfo();
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0004FB8C File Offset: 0x0004DD8C
		private bool ReadAtNestedPropertyInfoSynchronously()
		{
			ODataPropertyInfo odataPropertyInfo = base.CurrentScope.Item as ODataPropertyInfo;
			ODataStreamPropertyInfo odataStreamPropertyInfo = odataPropertyInfo as ODataStreamPropertyInfo;
			if (odataStreamPropertyInfo != null && !string.IsNullOrEmpty(odataStreamPropertyInfo.ContentType))
			{
				this.StartNestedStreamInfo(new ODataJsonLightReaderStreamInfo(odataStreamPropertyInfo.PrimitiveTypeKind, odataStreamPropertyInfo.ContentType));
			}
			else
			{
				this.StartNestedStreamInfo(new ODataJsonLightReaderStreamInfo(odataPropertyInfo.PrimitiveTypeKind));
			}
			return true;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0004FBEC File Offset: 0x0004DDEC
		private bool ReadAtStreamSynchronously()
		{
			base.PopScope(ODataReaderState.Stream);
			if (this.State == ODataReaderState.ResourceSetStart || this.State == ODataReaderState.DeltaResourceSetStart)
			{
				this.ReadNextResourceSetItem();
				return true;
			}
			if (this.State == ODataReaderState.NestedProperty)
			{
				base.PopScope(ODataReaderState.NestedProperty);
			}
			return this.ReadNextNestedInfo();
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0004FC2C File Offset: 0x0004DE2C
		private bool ReadNextNestedInfo()
		{
			IODataJsonLightReaderResourceState currentResourceState = this.CurrentResourceState;
			ODataJsonLightReaderNestedInfo odataJsonLightReaderNestedInfo;
			if (this.jsonLightInputContext.ReadingResponse && currentResourceState.ProcessingMissingProjectedNestedResourceInfos)
			{
				odataJsonLightReaderNestedInfo = currentResourceState.Resource.MetadataBuilder.GetNextUnprocessedNavigationLink();
			}
			else
			{
				odataJsonLightReaderNestedInfo = this.jsonLightResourceDeserializer.ReadResourceContent(currentResourceState);
			}
			if (odataJsonLightReaderNestedInfo == null)
			{
				this.EndEntry();
			}
			else
			{
				this.ReadNestedInfo(odataJsonLightReaderNestedInfo);
			}
			return true;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0004FC8C File Offset: 0x0004DE8C
		private void ReadNestedInfo(ODataJsonLightReaderNestedInfo nestedInfo)
		{
			ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo = nestedInfo as ODataJsonLightReaderNestedResourceInfo;
			if (odataJsonLightReaderNestedResourceInfo != null)
			{
				this.StartNestedResourceInfo(odataJsonLightReaderNestedResourceInfo);
				return;
			}
			ODataJsonLightReaderNestedPropertyInfo odataJsonLightReaderNestedPropertyInfo = nestedInfo as ODataJsonLightReaderNestedPropertyInfo;
			if (odataJsonLightReaderNestedPropertyInfo != null)
			{
				this.StartNestedPropertyInfo(odataJsonLightReaderNestedPropertyInfo);
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0004FCBC File Offset: 0x0004DEBC
		private bool ReadAtEntityReferenceLinkSynchronously()
		{
			base.PopScope(ODataReaderState.EntityReferenceLink);
			this.ReadNextNestedResourceInfoContentItemInRequest();
			return true;
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0004FCCC File Offset: 0x0004DECC
		private void ReadResourceSetStart(ODataResourceSet resourceSet, SelectedPropertiesNode selectedProperties)
		{
			this.jsonLightResourceDeserializer.ReadResourceSetContentStart();
			IJsonReader jsonReader = this.jsonLightResourceDeserializer.JsonReader;
			if (jsonReader.NodeType != JsonNodeType.EndArray && jsonReader.NodeType != JsonNodeType.StartObject && jsonReader.NodeType != JsonNodeType.PrimitiveValue && jsonReader.NodeType != JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_InvalidNodeTypeForItemsInResourceSet(jsonReader.NodeType));
			}
			base.EnterScope(new ODataJsonLightReader.JsonLightResourceSetScope(resourceSet, base.CurrentNavigationSource, base.CurrentScope.ResourceTypeReference, selectedProperties, base.CurrentScope.ODataUri, false));
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0004FD54 File Offset: 0x0004DF54
		private void ReadResourceSetEnd()
		{
			this.jsonLightResourceDeserializer.ReadResourceSetContentEnd();
			ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo = null;
			ODataJsonLightReader.JsonLightNestedResourceInfoScope jsonLightNestedResourceInfoScope = (ODataJsonLightReader.JsonLightNestedResourceInfoScope)base.ExpandedLinkContentParentScope;
			if (jsonLightNestedResourceInfoScope != null)
			{
				odataJsonLightReaderNestedResourceInfo = jsonLightNestedResourceInfoScope.ReaderNestedResourceInfo;
			}
			if (!base.IsReadingNestedPayload && (base.IsExpandedLinkContent || base.IsTopLevel))
			{
				this.jsonLightResourceDeserializer.ReadNextLinkAnnotationAtResourceSetEnd(this.Item as ODataResourceSetBase, odataJsonLightReaderNestedResourceInfo, this.topLevelScope.PropertyAndAnnotationCollector);
			}
			this.ReplaceScope((this.State == ODataReaderState.ResourceSetStart) ? ODataReaderState.ResourceSetEnd : ODataReaderState.DeltaResourceSetEnd);
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0004FDD4 File Offset: 0x0004DFD4
		private void ReadExpandedNestedResourceInfoStart(ODataNestedResourceInfo nestedResourceInfo)
		{
			if (this.jsonLightResourceDeserializer.JsonReader.NodeType != JsonNodeType.PrimitiveValue)
			{
				ODataJsonLightReader.JsonLightResourceBaseScope jsonLightResourceBaseScope = (ODataJsonLightReader.JsonLightResourceBaseScope)base.ParentScope;
				SelectedPropertiesNode selectedProperties = jsonLightResourceBaseScope.SelectedProperties;
				this.ReadResourceSetItemStart(null, selectedProperties.GetSelectedPropertiesForNavigationProperty(jsonLightResourceBaseScope.ResourceType, nestedResourceInfo.Name));
				return;
			}
			IEdmStructuralProperty structuralProperty = this.CurrentJsonLightNestedResourceInfoScope.ReaderNestedResourceInfo.StructuralProperty;
			if (structuralProperty != null && !structuralProperty.Type.IsNullable && (this.jsonLightResourceDeserializer.ReadingResponse || this.jsonLightResourceDeserializer.Model.NullValueReadBehaviorKind(structuralProperty) == ODataNullValueBehaviorKind.Default))
			{
				throw new ODataException(Strings.ReaderValidationUtils_NullNamedValueForNonNullableType(nestedResourceInfo.Name, structuralProperty.Type.FullName()));
			}
			base.EnterScope(new ODataJsonLightReader.JsonLightResourceScope(ODataReaderState.ResourceStart, null, base.CurrentNavigationSource, base.CurrentResourceTypeReference, null, null, base.CurrentScope.ODataUri));
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x0004FEAC File Offset: 0x0004E0AC
		private void ReadExpandedNestedResourceInfoEnd(bool isCollection)
		{
			base.CurrentNestedResourceInfo.IsCollection = new bool?(isCollection);
			IODataJsonLightReaderResourceState iodataJsonLightReaderResourceState = (IODataJsonLightReaderResourceState)base.ParentScope;
			iodataJsonLightReaderResourceState.NavigationPropertiesRead.Add(base.CurrentNestedResourceInfo.Name);
			this.ReplaceScope(ODataReaderState.NestedResourceInfoEnd);
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0004FEF4 File Offset: 0x0004E0F4
		private void ReadResourceSetItemStart(PropertyAndAnnotationCollector propertyAndAnnotationCollector, SelectedPropertiesNode selectedProperties)
		{
			IEdmNavigationSource edmNavigationSource = base.CurrentNavigationSource;
			IEdmTypeReference edmTypeReference = base.CurrentResourceTypeReference;
			if (this.jsonLightResourceDeserializer.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
			{
				object value = this.jsonLightResourceDeserializer.JsonReader.Value;
				if (value == null)
				{
					if (edmTypeReference.IsComplex() || edmTypeReference.IsUntyped())
					{
						this.jsonLightResourceDeserializer.MessageReaderSettings.Validator.ValidateNullValue(base.CurrentResourceTypeReference, true, "", null);
					}
					base.EnterScope(new ODataJsonLightReader.JsonLightResourceScope(ODataReaderState.ResourceStart, null, base.CurrentNavigationSource, base.CurrentResourceTypeReference, null, null, base.CurrentScope.ODataUri));
					return;
				}
				if (base.CurrentResourceType.TypeKind == EdmTypeKind.Untyped)
				{
					base.EnterScope(new ODataJsonLightReader.JsonLightPrimitiveScope(new ODataPrimitiveValue(value), base.CurrentNavigationSource, base.CurrentResourceTypeReference, base.CurrentScope.ODataUri));
					return;
				}
				throw new ODataException(Strings.ODataJsonLightReader_UnexpectedPrimitiveValueForODataResource);
			}
			else
			{
				if (this.jsonLightResourceDeserializer.JsonReader.NodeType == JsonNodeType.StartObject)
				{
					this.jsonLightResourceDeserializer.JsonReader.Read();
				}
				ODataDeltaKind odataDeltaKind = ODataDeltaKind.Resource;
				if (base.ReadingResourceSet || base.IsExpandedLinkContent || base.ReadingDelta)
				{
					string text = this.jsonLightResourceDeserializer.ReadContextUriAnnotation(ODataPayloadKind.Resource, propertyAndAnnotationCollector, false);
					if (text != null)
					{
						text = UriUtils.UriToString(this.jsonLightResourceDeserializer.ProcessUriFromPayload(text));
						ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = ODataJsonLightContextUriParser.Parse(this.jsonLightResourceDeserializer.Model, text, base.ReadingDelta ? ODataPayloadKind.Delta : ODataPayloadKind.Resource, this.jsonLightResourceDeserializer.MessageReaderSettings.ClientCustomTypeResolver, this.jsonLightInputContext.ReadingResponse || base.ReadingDelta, true);
						if (odataJsonLightContextUriParseResult != null)
						{
							odataDeltaKind = odataJsonLightContextUriParseResult.DeltaKind;
							if (base.ReadingDelta && base.IsTopLevel && (odataDeltaKind == ODataDeltaKind.Resource || odataDeltaKind == ODataDeltaKind.DeletedEntry))
							{
								IEdmStructuredType edmStructuredType = odataJsonLightContextUriParseResult.EdmType as IEdmStructuredType;
								if (edmStructuredType != null)
								{
									edmTypeReference = edmStructuredType.ToTypeReference(true);
									edmNavigationSource = odataJsonLightContextUriParseResult.NavigationSource;
								}
							}
							else
							{
								ReaderValidationUtils.ValidateResourceSetOrResourceContextUri(odataJsonLightContextUriParseResult, base.CurrentScope, false);
							}
						}
					}
				}
				ODataDeletedResource odataDeletedResource = null;
				if (base.ReadingDelta && (odataDeltaKind == ODataDeltaKind.Resource || odataDeltaKind == ODataDeltaKind.DeletedEntry))
				{
					odataDeletedResource = this.jsonLightResourceDeserializer.IsDeletedResource();
					if (odataDeletedResource != null)
					{
						odataDeltaKind = ODataDeltaKind.DeletedEntry;
					}
				}
				switch (odataDeltaKind)
				{
				case ODataDeltaKind.None:
				case ODataDeltaKind.Resource:
					this.StartResource(edmNavigationSource, edmTypeReference, propertyAndAnnotationCollector, selectedProperties);
					this.StartReadingResource();
					return;
				case ODataDeltaKind.ResourceSet:
					this.ReadAtResourceSetStartImplementation();
					return;
				case ODataDeltaKind.DeletedEntry:
					if (odataDeletedResource == null)
					{
						odataDeletedResource = this.jsonLightResourceDeserializer.ReadDeletedEntry();
						this.StartDeletedResource(odataDeletedResource, edmNavigationSource, edmTypeReference, propertyAndAnnotationCollector, selectedProperties, true);
						return;
					}
					this.StartDeletedResource(odataDeletedResource, edmNavigationSource, edmTypeReference, propertyAndAnnotationCollector, selectedProperties, false);
					this.StartReadingResource();
					return;
				case ODataDeltaKind.Link:
					this.StartDeltaLink(ODataReaderState.DeltaLink);
					return;
				case ODataDeltaKind.DeletedLink:
					this.StartDeltaLink(ODataReaderState.DeltaDeletedLink);
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00050188 File Offset: 0x0004E388
		private void ReadDeltaResourceSetStart(ODataDeltaResourceSet deltaResourceSet, SelectedPropertiesNode selectedProperties)
		{
			this.jsonLightResourceDeserializer.ReadResourceSetContentStart();
			IJsonReader jsonReader = this.jsonLightResourceDeserializer.JsonReader;
			if (jsonReader.NodeType != JsonNodeType.EndArray && jsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_InvalidNodeTypeForItemsInResourceSet(jsonReader.NodeType));
			}
			base.EnterScope(new ODataJsonLightReader.JsonLightResourceSetScope(deltaResourceSet, base.CurrentNavigationSource, base.CurrentResourceTypeReference as IEdmEntityTypeReference, selectedProperties, base.CurrentScope.ODataUri, true));
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00050200 File Offset: 0x0004E400
		private void StartReadingResource()
		{
			ODataResourceBase odataResourceBase = this.Item as ODataResourceBase;
			this.jsonLightResourceDeserializer.ReadResourceTypeName(this.CurrentResourceState);
			base.ApplyResourceTypeNameFromPayload(odataResourceBase.TypeName);
			if (base.CurrentDerivedTypeValidator != null)
			{
				base.CurrentDerivedTypeValidator.ValidateResourceType(base.CurrentResourceType);
			}
			if (base.CurrentResourceSetValidator != null && (!base.ReadingDelta || base.CurrentResourceDepth != 0))
			{
				base.CurrentResourceSetValidator.ValidateResource(base.CurrentResourceType);
			}
			this.CurrentResourceState.FirstNestedInfo = this.jsonLightResourceDeserializer.ReadResourceContent(this.CurrentResourceState);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00050294 File Offset: 0x0004E494
		private void ReadNextResourceSetItem()
		{
			IEdmType resourceType = base.CurrentScope.ResourceType;
			switch (this.jsonLightResourceDeserializer.JsonReader.NodeType)
			{
			case JsonNodeType.StartObject:
				this.ReadResourceSetItemStart(null, this.CurrentJsonLightResourceSetScope.SelectedProperties);
				return;
			case JsonNodeType.StartArray:
				this.ReadResourceSetStart(new ODataResourceSet(), new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.EntireSubtree));
				return;
			case JsonNodeType.EndArray:
				this.ReadResourceSetEnd();
				return;
			case JsonNodeType.PrimitiveValue:
			{
				if (this.TryReadPrimitiveAsStream(resourceType))
				{
					return;
				}
				object value = this.jsonLightResourceDeserializer.JsonReader.Value;
				if (value != null)
				{
					base.EnterScope(new ODataJsonLightReader.JsonLightPrimitiveScope(new ODataPrimitiveValue(value), base.CurrentNavigationSource, base.CurrentResourceTypeReference, base.CurrentScope.ODataUri));
					return;
				}
				if (resourceType.TypeKind == EdmTypeKind.Primitive || resourceType.TypeKind == EdmTypeKind.Enum)
				{
					base.EnterScope(new ODataJsonLightReader.JsonLightPrimitiveScope(new ODataNullValue(), base.CurrentNavigationSource, base.CurrentResourceTypeReference, base.CurrentScope.ODataUri));
					return;
				}
				this.ReadResourceSetItemStart(null, this.CurrentJsonLightResourceSetScope.SelectedProperties);
				return;
			}
			}
			throw new ODataException(Strings.ODataJsonReader_CannotReadResourcesOfResourceSet(this.jsonLightResourceDeserializer.JsonReader.NodeType));
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x000503C8 File Offset: 0x0004E5C8
		private bool TryReadPrimitiveAsStream(IEdmType resourceType)
		{
			Func<IEdmPrimitiveType, bool, string, IEdmProperty, bool> readAsStreamFunc = this.jsonLightInputContext.MessageReaderSettings.ReadAsStreamFunc;
			if ((resourceType != null && resourceType.IsStream()) || (resourceType != null && readAsStreamFunc != null && (resourceType.IsBinary() || resourceType.IsString()) && readAsStreamFunc(resourceType as IEdmPrimitiveType, false, null, null)))
			{
				if (resourceType == null || resourceType.IsUntyped())
				{
					this.StartNestedStreamInfo(new ODataJsonLightReaderStreamInfo(EdmPrimitiveTypeKind.None));
				}
				else if (resourceType.IsString())
				{
					this.StartNestedStreamInfo(new ODataJsonLightReaderStreamInfo(EdmPrimitiveTypeKind.String));
				}
				else
				{
					if (!resourceType.IsStream() && !resourceType.IsBinary())
					{
						return false;
					}
					this.StartNestedStreamInfo(new ODataJsonLightReaderStreamInfo(EdmPrimitiveTypeKind.Binary));
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x0005046C File Offset: 0x0004E66C
		private void ReadNextNestedResourceInfoContentItemInRequest()
		{
			ODataJsonLightReaderNestedResourceInfo readerNestedResourceInfo = this.CurrentJsonLightNestedResourceInfoScope.ReaderNestedResourceInfo;
			if (readerNestedResourceInfo.HasEntityReferenceLink)
			{
				base.EnterScope(new ODataReaderCore.Scope(ODataReaderState.EntityReferenceLink, readerNestedResourceInfo.ReportEntityReferenceLink(), base.CurrentScope.ODataUri));
				return;
			}
			if (!readerNestedResourceInfo.HasValue)
			{
				this.ReplaceScope(ODataReaderState.NestedResourceInfoEnd);
				return;
			}
			if (!(readerNestedResourceInfo.NestedResourceInfo.IsCollection == true))
			{
				this.ReadExpandedNestedResourceInfoStart(readerNestedResourceInfo.NestedResourceInfo);
				return;
			}
			SelectedPropertiesNode selectedPropertiesNode = new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.EntireSubtree);
			ODataDeltaResourceSet odataDeltaResourceSet = readerNestedResourceInfo.NestedResourceSet as ODataDeltaResourceSet;
			if (odataDeltaResourceSet != null)
			{
				this.ReadDeltaResourceSetStart(odataDeltaResourceSet, selectedPropertiesNode);
				return;
			}
			ODataResourceSet odataResourceSet = readerNestedResourceInfo.NestedResourceSet as ODataResourceSet;
			this.ReadResourceSetStart(odataResourceSet ?? new ODataResourceSet(), selectedPropertiesNode);
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0005052D File Offset: 0x0004E72D
		private void StartResource(IEdmNavigationSource source, IEdmTypeReference resourceType, PropertyAndAnnotationCollector propertyAndAnnotationCollector, SelectedPropertiesNode selectedProperties)
		{
			base.EnterScope(new ODataJsonLightReader.JsonLightResourceScope(ODataReaderState.ResourceStart, ReaderUtils.CreateNewResource(), source, resourceType, propertyAndAnnotationCollector ?? this.jsonLightInputContext.CreatePropertyAndAnnotationCollector(), selectedProperties, base.CurrentScope.ODataUri));
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x00050560 File Offset: 0x0004E760
		private void StartDeletedResource(ODataDeletedResource deletedResource, IEdmNavigationSource source, IEdmTypeReference resourceType, PropertyAndAnnotationCollector propertyAndAnnotationCollector, SelectedPropertiesNode selectedProperties, bool is40DeletedResource = false)
		{
			base.EnterScope(new ODataJsonLightReader.JsonLightDeletedResourceScope(ODataReaderState.DeletedResourceStart, deletedResource, source, resourceType, propertyAndAnnotationCollector ?? this.jsonLightInputContext.CreatePropertyAndAnnotationCollector(), selectedProperties, base.CurrentScope.ODataUri, is40DeletedResource));
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000505A0 File Offset: 0x0004E7A0
		private void StartDeltaLink(ODataReaderState state)
		{
			ODataDeltaLinkBase odataDeltaLinkBase;
			if (state == ODataReaderState.DeltaLink)
			{
				odataDeltaLinkBase = new ODataDeltaLink(null, null, null);
			}
			else
			{
				odataDeltaLinkBase = new ODataDeltaDeletedLink(null, null, null);
			}
			base.EnterScope(new ODataJsonLightReader.JsonLightDeltaLinkScope(state, odataDeltaLinkBase, base.CurrentNavigationSource, base.CurrentResourceType as IEdmEntityType, base.CurrentScope.ODataUri));
			this.jsonLightResourceDeserializer.ReadDeltaLinkSource(odataDeltaLinkBase);
			this.jsonLightResourceDeserializer.ReadDeltaLinkRelationship(odataDeltaLinkBase);
			this.jsonLightResourceDeserializer.ReadDeltaLinkTarget(odataDeltaLinkBase);
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x00050614 File Offset: 0x0004E814
		private void StartNestedResourceInfo(ODataJsonLightReaderNestedResourceInfo readerNestedResourceInfo)
		{
			ODataNestedResourceInfo nestedResourceInfo = readerNestedResourceInfo.NestedResourceInfo;
			IEdmProperty nestedProperty = readerNestedResourceInfo.NestedProperty;
			IEdmTypeReference edmTypeReference = readerNestedResourceInfo.NestedResourceTypeReference;
			if (edmTypeReference == null && nestedProperty != null)
			{
				IEdmTypeReference type = nestedProperty.Type;
				edmTypeReference = (type.IsCollection() ? type.AsCollection().ElementType().AsStructured() : type.AsStructured());
			}
			if (this.jsonLightInputContext.ReadingResponse && !base.IsReadingNestedPayload && (edmTypeReference == null || edmTypeReference.Definition.IsStructuredOrStructuredCollectionType()))
			{
				this.CurrentResourceState.ResourceTypeFromMetadata = base.ParentScope.ResourceType as IEdmStructuredType;
				ODataResourceMetadataBuilder resourceMetadataBuilderForReader = this.jsonLightResourceDeserializer.MetadataContext.GetResourceMetadataBuilderForReader(this.CurrentResourceState, this.jsonLightInputContext.ODataSimplifiedOptions.EnableReadingKeyAsSegment, base.ReadingDelta);
				nestedResourceInfo.MetadataBuilder = resourceMetadataBuilderForReader;
			}
			IEdmNavigationProperty navigationProperty = readerNestedResourceInfo.NavigationProperty;
			ODataJsonLightReader.JsonLightResourceBaseScope jsonLightResourceBaseScope = base.CurrentScope as ODataJsonLightReader.JsonLightResourceBaseScope;
			ODataUri odataUri = base.CurrentScope.ODataUri.Clone();
			ODataPath odataPath = odataUri.Path ?? new ODataPath(new ODataPathSegment[0]);
			if (jsonLightResourceBaseScope != null && jsonLightResourceBaseScope.ResourceTypeFromMetadata != jsonLightResourceBaseScope.ResourceType)
			{
				odataPath.Add(new TypeSegment(jsonLightResourceBaseScope.ResourceType, null));
			}
			IEdmNavigationSource edmNavigationSource;
			if (navigationProperty == null)
			{
				edmNavigationSource = base.CurrentNavigationSource;
			}
			else
			{
				IEdmPathExpression edmPathExpression;
				edmNavigationSource = ((base.CurrentNavigationSource == null) ? null : base.CurrentNavigationSource.FindNavigationTarget(navigationProperty, new Func<IEdmPathExpression, List<ODataPathSegment>, bool>(BindingPathHelper.MatchBindingPath), odataPath.ToList<ODataPathSegment>(), out edmPathExpression));
			}
			if (navigationProperty != null)
			{
				if (edmNavigationSource is IEdmContainedEntitySet)
				{
					if (this.TryAppendEntitySetKeySegment(ref odataPath))
					{
						odataPath = odataPath.AppendNavigationPropertySegment(navigationProperty, edmNavigationSource);
					}
				}
				else if (edmNavigationSource != null && !(edmNavigationSource is IEdmUnknownEntitySet))
				{
					IEdmEntitySet edmEntitySet = edmNavigationSource as IEdmEntitySet;
					odataPath = ((edmEntitySet != null) ? new ODataPath(new ODataPathSegment[]
					{
						new EntitySetSegment(edmEntitySet)
					}) : new ODataPath(new ODataPathSegment[]
					{
						new SingletonSegment(edmNavigationSource as IEdmSingleton)
					}));
				}
				else
				{
					odataPath = new ODataPath(new ODataPathSegment[0]);
				}
			}
			else if (nestedProperty != null)
			{
				odataPath = odataPath.AppendPropertySegment(nestedProperty as IEdmStructuralProperty);
			}
			odataUri.Path = odataPath;
			ODataJsonLightReader.JsonLightNestedResourceInfoScope jsonLightNestedResourceInfoScope = new ODataJsonLightReader.JsonLightNestedResourceInfoScope(readerNestedResourceInfo, edmNavigationSource, edmTypeReference, odataUri);
			IEnumerable<string> derivedTypeConstraints = this.jsonLightInputContext.Model.GetDerivedTypeConstraints(nestedProperty);
			if (derivedTypeConstraints != null)
			{
				jsonLightNestedResourceInfoScope.DerivedTypeValidator = new DerivedTypeValidator(nestedProperty.Type.ToStructuredType(), derivedTypeConstraints, "nested resource", nestedProperty.Name);
			}
			base.EnterScope(jsonLightNestedResourceInfoScope);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0005086C File Offset: 0x0004EA6C
		private void StartNestedPropertyInfo(ODataJsonLightReaderNestedPropertyInfo readerNestedPropertyInfo)
		{
			base.EnterScope(new ODataJsonLightReader.JsonLightNestedPropertyInfoScope(readerNestedPropertyInfo, base.CurrentNavigationSource, base.CurrentScope.ODataUri));
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0005088B File Offset: 0x0004EA8B
		private void StartNestedStreamInfo(ODataJsonLightReaderStreamInfo readerStreamInfo)
		{
			base.EnterScope(new ODataJsonLightReader.JsonLightStreamScope(readerStreamInfo, base.CurrentNavigationSource, base.CurrentScope.ODataUri));
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000508AC File Offset: 0x0004EAAC
		private bool TryAppendEntitySetKeySegment(ref ODataPath odataPath)
		{
			try
			{
				if (EdmExtensionMethods.HasKey(base.CurrentScope.NavigationSource, base.CurrentScope.ResourceType as IEdmStructuredType))
				{
					IEdmEntityType edmEntityType = base.CurrentScope.ResourceType as IEdmEntityType;
					ODataResourceBase odataResourceBase = base.CurrentScope.Item as ODataResourceBase;
					KeyValuePair<string, object>[] keyProperties = ODataResourceMetadataContext.GetKeyProperties(odataResourceBase, null, edmEntityType);
					odataPath = odataPath.AppendKeySegment(keyProperties, edmEntityType, base.CurrentScope.NavigationSource);
				}
			}
			catch (ODataException)
			{
				odataPath = null;
				return false;
			}
			return true;
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0005093C File Offset: 0x0004EB3C
		private void ReplaceScope(ODataReaderState state)
		{
			base.ReplaceScope(new ODataReaderCore.Scope(state, this.Item, base.CurrentNavigationSource, base.CurrentResourceTypeReference, base.CurrentScope.ODataUri));
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00050968 File Offset: 0x0004EB68
		private void EndEntry()
		{
			IODataJsonLightReaderResourceState currentResourceState = this.CurrentResourceState;
			ODataResourceBase odataResourceBase = this.Item as ODataResourceBase;
			if (odataResourceBase != null && !base.IsReadingNestedPayload)
			{
				foreach (string text in this.CurrentResourceState.NavigationPropertiesRead)
				{
					odataResourceBase.MetadataBuilder.MarkNestedResourceInfoProcessed(text);
				}
				ODataConventionalEntityMetadataBuilder odataConventionalEntityMetadataBuilder = odataResourceBase.MetadataBuilder as ODataConventionalEntityMetadataBuilder;
				if (odataConventionalEntityMetadataBuilder != null)
				{
					odataConventionalEntityMetadataBuilder.EndResource();
				}
			}
			this.jsonLightResourceDeserializer.ValidateMediaEntity(currentResourceState);
			if (this.jsonLightInputContext.ReadingResponse && !base.ReadingDelta && odataResourceBase != null)
			{
				ODataJsonLightReaderNestedResourceInfo nextUnprocessedNavigationLink = odataResourceBase.MetadataBuilder.GetNextUnprocessedNavigationLink();
				if (nextUnprocessedNavigationLink != null)
				{
					this.CurrentResourceState.ProcessingMissingProjectedNestedResourceInfos = true;
					this.StartNestedResourceInfo(nextUnprocessedNavigationLink);
					return;
				}
			}
			if (this.State == ODataReaderState.ResourceStart)
			{
				base.EndEntry(new ODataJsonLightReader.JsonLightResourceScope(ODataReaderState.ResourceEnd, (ODataResource)this.Item, base.CurrentNavigationSource, base.CurrentResourceTypeReference, this.CurrentResourceState.PropertyAndAnnotationCollector, this.CurrentResourceState.SelectedProperties, base.CurrentScope.ODataUri));
				return;
			}
			base.EndEntry(new ODataJsonLightReader.JsonLightDeletedResourceScope(ODataReaderState.DeletedResourceEnd, (ODataDeletedResource)this.Item, base.CurrentNavigationSource, base.CurrentResourceTypeReference, this.CurrentResourceState.PropertyAndAnnotationCollector, this.CurrentResourceState.SelectedProperties, base.CurrentScope.ODataUri, false));
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00050AD8 File Offset: 0x0004ECD8
		private void ResolveScopeInfoFromContextUrl()
		{
			if (this.jsonLightResourceDeserializer.ContextUriParseResult != null)
			{
				base.CurrentScope.ODataUri.Path = this.jsonLightResourceDeserializer.ContextUriParseResult.Path;
				if (base.CurrentScope.NavigationSource == null)
				{
					base.CurrentScope.NavigationSource = this.jsonLightResourceDeserializer.ContextUriParseResult.NavigationSource;
				}
				if (base.CurrentScope.ResourceType == null)
				{
					IEdmType edmType = this.jsonLightResourceDeserializer.ContextUriParseResult.EdmType;
					if (edmType != null)
					{
						if (edmType.TypeKind == EdmTypeKind.Collection)
						{
							edmType = ((IEdmCollectionType)edmType).ElementType.Definition;
							if (!(edmType is IEdmStructuredType))
							{
								edmType = new EdmUntypedStructuredType();
								this.jsonLightResourceDeserializer.ContextUriParseResult.EdmType = new EdmCollectionType(edmType.ToTypeReference());
							}
						}
						IEdmStructuredType edmStructuredType = edmType as IEdmStructuredType;
						if (edmStructuredType == null)
						{
							edmStructuredType = new EdmUntypedStructuredType();
							this.jsonLightResourceDeserializer.ContextUriParseResult.EdmType = edmStructuredType;
						}
						base.CurrentScope.ResourceTypeReference = edmStructuredType.ToTypeReference(true).AsStructured();
					}
				}
			}
		}

		// Token: 0x04000B51 RID: 2897
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000B52 RID: 2898
		private readonly ODataJsonLightResourceDeserializer jsonLightResourceDeserializer;

		// Token: 0x04000B53 RID: 2899
		private readonly ODataJsonLightReader.JsonLightTopLevelScope topLevelScope;

		// Token: 0x04000B54 RID: 2900
		private readonly bool readingParameter;

		// Token: 0x02000430 RID: 1072
		private sealed class JsonLightTopLevelScope : ODataReaderCore.Scope
		{
			// Token: 0x0600216A RID: 8554 RVA: 0x0005D95B File Offset: 0x0005BB5B
			internal JsonLightTopLevelScope(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType, ODataUri odataUri)
				: base(ODataReaderState.Start, null, navigationSource, expectedResourceType.ToTypeReference(true), odataUri)
			{
			}

			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x0600216B RID: 8555 RVA: 0x0005D96E File Offset: 0x0005BB6E
			// (set) Token: 0x0600216C RID: 8556 RVA: 0x0005D976 File Offset: 0x0005BB76
			public PropertyAndAnnotationCollector PropertyAndAnnotationCollector { get; set; }
		}

		// Token: 0x02000431 RID: 1073
		private sealed class JsonLightPrimitiveScope : ODataReaderCore.Scope
		{
			// Token: 0x0600216D RID: 8557 RVA: 0x0005D97F File Offset: 0x0005BB7F
			internal JsonLightPrimitiveScope(ODataValue primitiveValue, IEdmNavigationSource navigationSource, IEdmTypeReference expectedTypeReference, ODataUri odataUri)
				: base(ODataReaderState.Primitive, primitiveValue, navigationSource, expectedTypeReference, odataUri)
			{
			}
		}

		// Token: 0x02000432 RID: 1074
		private abstract class JsonLightResourceBaseScope : ODataReaderCore.Scope, IODataJsonLightReaderResourceState
		{
			// Token: 0x0600216E RID: 8558 RVA: 0x0005D98E File Offset: 0x0005BB8E
			protected JsonLightResourceBaseScope(ODataReaderState readerState, ODataResourceBase resource, IEdmNavigationSource navigationSource, IEdmTypeReference expectedResourceTypeReference, PropertyAndAnnotationCollector propertyAndAnnotationCollector, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(readerState, resource, navigationSource, expectedResourceTypeReference, odataUri)
			{
				this.PropertyAndAnnotationCollector = propertyAndAnnotationCollector;
				this.SelectedProperties = selectedProperties;
			}

			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x0600216F RID: 8559 RVA: 0x0005D9AD File Offset: 0x0005BBAD
			// (set) Token: 0x06002170 RID: 8560 RVA: 0x0005D9B5 File Offset: 0x0005BBB5
			public ODataResourceMetadataBuilder MetadataBuilder { get; set; }

			// Token: 0x1700064D RID: 1613
			// (get) Token: 0x06002171 RID: 8561 RVA: 0x0005D9BE File Offset: 0x0005BBBE
			// (set) Token: 0x06002172 RID: 8562 RVA: 0x0005D9C6 File Offset: 0x0005BBC6
			public bool AnyPropertyFound { get; set; }

			// Token: 0x1700064E RID: 1614
			// (get) Token: 0x06002173 RID: 8563 RVA: 0x0005D9CF File Offset: 0x0005BBCF
			// (set) Token: 0x06002174 RID: 8564 RVA: 0x0005D9D7 File Offset: 0x0005BBD7
			public ODataJsonLightReaderNestedInfo FirstNestedInfo { get; set; }

			// Token: 0x1700064F RID: 1615
			// (get) Token: 0x06002175 RID: 8565 RVA: 0x0005D9E0 File Offset: 0x0005BBE0
			// (set) Token: 0x06002176 RID: 8566 RVA: 0x0005D9E8 File Offset: 0x0005BBE8
			public PropertyAndAnnotationCollector PropertyAndAnnotationCollector { get; private set; }

			// Token: 0x17000650 RID: 1616
			// (get) Token: 0x06002177 RID: 8567 RVA: 0x0005D9F1 File Offset: 0x0005BBF1
			// (set) Token: 0x06002178 RID: 8568 RVA: 0x0005D9F9 File Offset: 0x0005BBF9
			public SelectedPropertiesNode SelectedProperties { get; private set; }

			// Token: 0x17000651 RID: 1617
			// (get) Token: 0x06002179 RID: 8569 RVA: 0x0005DA04 File Offset: 0x0005BC04
			public List<string> NavigationPropertiesRead
			{
				get
				{
					List<string> list;
					if ((list = this.navigationPropertiesRead) == null)
					{
						list = (this.navigationPropertiesRead = new List<string>());
					}
					return list;
				}
			}

			// Token: 0x17000652 RID: 1618
			// (get) Token: 0x0600217A RID: 8570 RVA: 0x0005DA29 File Offset: 0x0005BC29
			// (set) Token: 0x0600217B RID: 8571 RVA: 0x0005DA31 File Offset: 0x0005BC31
			public bool ProcessingMissingProjectedNestedResourceInfos { get; set; }

			// Token: 0x17000653 RID: 1619
			// (get) Token: 0x0600217C RID: 8572 RVA: 0x0005DA3A File Offset: 0x0005BC3A
			// (set) Token: 0x0600217D RID: 8573 RVA: 0x0005DA42 File Offset: 0x0005BC42
			public IEdmStructuredType ResourceTypeFromMetadata { get; set; }

			// Token: 0x17000654 RID: 1620
			// (get) Token: 0x0600217E RID: 8574 RVA: 0x0005DA4B File Offset: 0x0005BC4B
			public new IEdmStructuredType ResourceType
			{
				get
				{
					return base.ResourceType as IEdmStructuredType;
				}
			}

			// Token: 0x17000655 RID: 1621
			// (get) Token: 0x0600217F RID: 8575 RVA: 0x0005DA58 File Offset: 0x0005BC58
			ODataResourceBase IODataJsonLightReaderResourceState.Resource
			{
				get
				{
					return (ODataResourceBase)base.Item;
				}
			}

			// Token: 0x17000656 RID: 1622
			// (get) Token: 0x06002180 RID: 8576 RVA: 0x0005DA65 File Offset: 0x0005BC65
			IEdmStructuredType IODataJsonLightReaderResourceState.ResourceType
			{
				get
				{
					return this.ResourceType;
				}
			}

			// Token: 0x17000657 RID: 1623
			// (get) Token: 0x06002181 RID: 8577 RVA: 0x0005DA6D File Offset: 0x0005BC6D
			IEdmNavigationSource IODataJsonLightReaderResourceState.NavigationSource
			{
				get
				{
					return base.NavigationSource;
				}
			}

			// Token: 0x0400103C RID: 4156
			private List<string> navigationPropertiesRead;
		}

		// Token: 0x02000433 RID: 1075
		private sealed class JsonLightResourceScope : ODataJsonLightReader.JsonLightResourceBaseScope
		{
			// Token: 0x06002182 RID: 8578 RVA: 0x0005DA75 File Offset: 0x0005BC75
			internal JsonLightResourceScope(ODataReaderState readerState, ODataResourceBase resource, IEdmNavigationSource navigationSource, IEdmTypeReference expectedResourceTypeReference, PropertyAndAnnotationCollector propertyAndAnnotationCollector, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(readerState, resource, navigationSource, expectedResourceTypeReference, propertyAndAnnotationCollector, selectedProperties, odataUri)
			{
			}
		}

		// Token: 0x02000434 RID: 1076
		private sealed class JsonLightDeletedResourceScope : ODataJsonLightReader.JsonLightResourceBaseScope
		{
			// Token: 0x06002183 RID: 8579 RVA: 0x0005DA88 File Offset: 0x0005BC88
			internal JsonLightDeletedResourceScope(ODataReaderState readerState, ODataDeletedResource resource, IEdmNavigationSource navigationSource, IEdmTypeReference expectedResourceType, PropertyAndAnnotationCollector propertyAndAnnotationCollector, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool is40DeletedResource = false)
				: base(readerState, resource, navigationSource, expectedResourceType, propertyAndAnnotationCollector, selectedProperties, odataUri)
			{
				this.Is40DeletedResource = is40DeletedResource;
			}

			// Token: 0x17000658 RID: 1624
			// (get) Token: 0x06002184 RID: 8580 RVA: 0x0005DAA3 File Offset: 0x0005BCA3
			internal bool Is40DeletedResource { get; }
		}

		// Token: 0x02000435 RID: 1077
		private sealed class JsonLightResourceSetScope : ODataReaderCore.Scope
		{
			// Token: 0x06002185 RID: 8581 RVA: 0x0005DAAB File Offset: 0x0005BCAB
			internal JsonLightResourceSetScope(ODataResourceSetBase resourceSet, IEdmNavigationSource navigationSource, IEdmTypeReference expectedResourceTypeReference, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isDelta)
				: base(isDelta ? ODataReaderState.DeltaResourceSetStart : ODataReaderState.ResourceSetStart, resourceSet, navigationSource, expectedResourceTypeReference, odataUri)
			{
				this.SelectedProperties = selectedProperties;
			}

			// Token: 0x17000659 RID: 1625
			// (get) Token: 0x06002186 RID: 8582 RVA: 0x0005DAC9 File Offset: 0x0005BCC9
			// (set) Token: 0x06002187 RID: 8583 RVA: 0x0005DAD1 File Offset: 0x0005BCD1
			public SelectedPropertiesNode SelectedProperties { get; private set; }
		}

		// Token: 0x02000436 RID: 1078
		private sealed class JsonLightNestedResourceInfoScope : ODataReaderCore.Scope
		{
			// Token: 0x06002188 RID: 8584 RVA: 0x0005DADA File Offset: 0x0005BCDA
			internal JsonLightNestedResourceInfoScope(ODataJsonLightReaderNestedResourceInfo nestedResourceInfo, IEdmNavigationSource navigationSource, IEdmTypeReference expectedTypeReference, ODataUri odataUri)
				: base(ODataReaderState.NestedResourceInfoStart, nestedResourceInfo.NestedResourceInfo, navigationSource, expectedTypeReference, odataUri)
			{
				this.ReaderNestedResourceInfo = nestedResourceInfo;
			}

			// Token: 0x1700065A RID: 1626
			// (get) Token: 0x06002189 RID: 8585 RVA: 0x0005DAF4 File Offset: 0x0005BCF4
			// (set) Token: 0x0600218A RID: 8586 RVA: 0x0005DAFC File Offset: 0x0005BCFC
			public ODataJsonLightReaderNestedResourceInfo ReaderNestedResourceInfo { get; private set; }
		}

		// Token: 0x02000437 RID: 1079
		private sealed class JsonLightNestedPropertyInfoScope : ODataReaderCore.Scope
		{
			// Token: 0x0600218B RID: 8587 RVA: 0x0005DB05 File Offset: 0x0005BD05
			internal JsonLightNestedPropertyInfoScope(ODataJsonLightReaderNestedPropertyInfo nestedPropertyInfo, IEdmNavigationSource navigationSource, ODataUri odataUri)
				: base(ODataReaderState.NestedProperty, nestedPropertyInfo.NestedPropertyInfo, navigationSource, EdmCoreModel.Instance.GetPrimitive(EdmPrimitiveTypeKind.Stream, true), odataUri)
			{
			}
		}

		// Token: 0x02000438 RID: 1080
		private sealed class JsonLightStreamScope : ODataReaderCore.StreamScope
		{
			// Token: 0x0600218C RID: 8588 RVA: 0x0005DB24 File Offset: 0x0005BD24
			internal JsonLightStreamScope(ODataJsonLightReaderStreamInfo streamInfo, IEdmNavigationSource navigationSource, ODataUri odataUri)
				: base(ODataReaderState.Stream, new ODataStreamItem(streamInfo.PrimitiveTypeKind, streamInfo.ContentType), navigationSource, EdmCoreModel.Instance.GetPrimitive(EdmPrimitiveTypeKind.Stream, true), odataUri)
			{
			}
		}

		// Token: 0x02000439 RID: 1081
		private sealed class JsonLightDeltaLinkScope : ODataReaderCore.Scope
		{
			// Token: 0x0600218D RID: 8589 RVA: 0x0005DB4E File Offset: 0x0005BD4E
			public JsonLightDeltaLinkScope(ODataReaderState state, ODataDeltaLinkBase link, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, ODataUri odataUri)
				: base(state, link, navigationSource, expectedEntityType.ToTypeReference(true), odataUri)
			{
			}
		}
	}
}
