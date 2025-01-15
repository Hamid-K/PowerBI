using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000215 RID: 533
	internal sealed class ODataJsonLightReader : ODataReaderCoreAsync
	{
		// Token: 0x06001595 RID: 5525 RVA: 0x00041660 File Offset: 0x0003F860
		internal ODataJsonLightReader(ODataJsonLightInputContext jsonLightInputContext, IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType, bool readingResourceSet, bool readingParameter = false, bool readingDelta = false, IODataReaderWriterListener listener = null)
			: base(jsonLightInputContext, readingResourceSet, readingDelta, listener)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightResourceDeserializer = new ODataJsonLightResourceDeserializer(jsonLightInputContext);
			this.readingParameter = readingParameter;
			this.topLevelScope = new ODataJsonLightReader.JsonLightTopLevelScope(navigationSource, expectedResourceType, new ODataUri());
			base.EnterScope(this.topLevelScope);
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x000416B3 File Offset: 0x0003F8B3
		private IODataJsonLightReaderResourceState CurrentResourceState
		{
			get
			{
				return (IODataJsonLightReaderResourceState)base.CurrentScope;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x000416C0 File Offset: 0x0003F8C0
		private ODataJsonLightReader.JsonLightResourceSetScope CurrentJsonLightResourceSetScope
		{
			get
			{
				return (ODataJsonLightReader.JsonLightResourceSetScope)base.CurrentScope;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x000416CD File Offset: 0x0003F8CD
		private ODataJsonLightReader.JsonLightNestedResourceInfoScope CurrentJsonLightNestedResourceInfoScope
		{
			get
			{
				return (ODataJsonLightReader.JsonLightNestedResourceInfoScope)base.CurrentScope;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x000416DC File Offset: 0x0003F8DC
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

		// Token: 0x0600159A RID: 5530 RVA: 0x00041704 File Offset: 0x0003F904
		protected override bool ReadAtStartImplementation()
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = this.jsonLightInputContext.CreatePropertyAndAnnotationCollector();
			ODataPayloadKind odataPayloadKind = (base.ReadingResourceSet ? ODataPayloadKind.ResourceSet : ODataPayloadKind.Resource);
			this.jsonLightResourceDeserializer.ReadPayloadStart(odataPayloadKind, propertyAndAnnotationCollector, base.IsReadingNestedPayload || this.readingParameter, false);
			this.ResolveScopeInfoFromContextUrl();
			return this.ReadAtStartImplementationSynchronously(propertyAndAnnotationCollector);
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00041756 File Offset: 0x0003F956
		protected override bool ReadAtResourceSetStartImplementation()
		{
			return this.ReadAtResourceSetStartImplementationSynchronously();
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0004175E File Offset: 0x0003F95E
		protected override bool ReadAtResourceSetEndImplementation()
		{
			return this.ReadAtResourceSetEndImplementationSynchronously();
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00041766 File Offset: 0x0003F966
		protected override bool ReadAtResourceStartImplementation()
		{
			return this.ReadAtResourceStartImplementationSynchronously();
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0004176E File Offset: 0x0003F96E
		protected override bool ReadAtResourceEndImplementation()
		{
			return this.ReadAtResourceEndImplementationSynchronously();
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00041776 File Offset: 0x0003F976
		protected override bool ReadAtPrimitiveImplementation()
		{
			return this.ReadAtPrimitiveSynchronously();
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0004177E File Offset: 0x0003F97E
		protected override bool ReadAtNestedResourceInfoStartImplementation()
		{
			return this.ReadAtNestedResourceInfoStartImplementationSynchronously();
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00041786 File Offset: 0x0003F986
		protected override bool ReadAtNestedResourceInfoEndImplementation()
		{
			return this.ReadAtNestedResourceInfoEndImplementationSynchronously();
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0004178E File Offset: 0x0003F98E
		protected override bool ReadAtEntityReferenceLink()
		{
			return this.ReadAtEntityReferenceLinkSynchronously();
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00041798 File Offset: 0x0003F998
		private bool ReadAtStartImplementationSynchronously(PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			if (this.jsonLightInputContext.ReadingResponse && !base.IsReadingNestedPayload)
			{
				ReaderValidationUtils.ValidateResourceSetOrResourceContextUri(this.jsonLightResourceDeserializer.ContextUriParseResult, base.CurrentScope, true);
			}
			string text = ((this.jsonLightResourceDeserializer.ContextUriParseResult == null) ? null : this.jsonLightResourceDeserializer.ContextUriParseResult.SelectQueryOption);
			SelectedPropertiesNode selectedPropertiesNode = SelectedPropertiesNode.Create(text);
			if (base.ReadingResourceSet)
			{
				ODataResourceSet odataResourceSet = new ODataResourceSet();
				this.topLevelScope.PropertyAndAnnotationCollector = propertyAndAnnotationCollector;
				bool flag = this.jsonLightInputContext.JsonReader is ReorderingJsonReader;
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
				return true;
			}
			this.ReadResourceStart(propertyAndAnnotationCollector, selectedPropertiesNode);
			return true;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0004186C File Offset: 0x0003FA6C
		private bool ReadAtResourceSetStartImplementationSynchronously()
		{
			this.ReadNextResourceSetItem();
			return true;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00041878 File Offset: 0x0003FA78
		private bool ReadAtResourceSetEndImplementationSynchronously()
		{
			bool isTopLevel = base.IsTopLevel;
			bool isExpandedLinkContent = base.IsExpandedLinkContent;
			base.PopScope(ODataReaderState.ResourceSetEnd);
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

		// Token: 0x060015A6 RID: 5542 RVA: 0x000418FC File Offset: 0x0003FAFC
		private bool ReadAtResourceStartImplementationSynchronously()
		{
			if (base.CurrentResource != null && !base.IsReadingNestedPayload)
			{
				this.CurrentResourceState.ResourceTypeFromMetadata = base.ParentScope.ResourceType;
				ODataResourceMetadataBuilder resourceMetadataBuilderForReader = this.jsonLightResourceDeserializer.MetadataContext.GetResourceMetadataBuilderForReader(this.CurrentResourceState, this.jsonLightInputContext.ODataSimplifiedOptions.EnableReadingKeyAsSegment);
				if (resourceMetadataBuilderForReader != base.CurrentResource.MetadataBuilder)
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
					base.CurrentResource.MetadataBuilder = resourceMetadataBuilderForReader;
					if (parentNestedInfo != null && parentNestedInfo.MetadataBuilder != null)
					{
						base.CurrentResource.MetadataBuilder.ParentMetadataBuilder = parentNestedInfo.MetadataBuilder;
					}
				}
			}
			if (base.CurrentResource == null)
			{
				this.EndEntry();
			}
			else if (this.CurrentResourceState.FirstNestedResourceInfo != null)
			{
				this.StartNestedResourceInfo(this.CurrentResourceState.FirstNestedResourceInfo);
			}
			else
			{
				this.EndEntry();
			}
			return true;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00041A38 File Offset: 0x0003FC38
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

		// Token: 0x060015A8 RID: 5544 RVA: 0x00041ABC File Offset: 0x0003FCBC
		private bool ReadAtResourceEndImplementationSynchronously()
		{
			bool isTopLevel = base.IsTopLevel;
			bool isExpandedLinkContent = base.IsExpandedLinkContent;
			base.PopScope(ODataReaderState.ResourceEnd);
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

		// Token: 0x060015A9 RID: 5545 RVA: 0x00041B24 File Offset: 0x0003FD24
		private bool ReadAtPrimitiveSynchronously()
		{
			base.PopScope(ODataReaderState.Primitive);
			this.jsonLightResourceDeserializer.JsonReader.Read();
			this.ReadNextResourceSetItem();
			return true;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00041B48 File Offset: 0x0003FD48
		private void ReadNextResourceSetItem()
		{
			switch (this.jsonLightResourceDeserializer.JsonReader.NodeType)
			{
			case JsonNodeType.StartObject:
				this.ReadResourceStart(null, this.CurrentJsonLightResourceSetScope.SelectedProperties);
				return;
			case JsonNodeType.StartArray:
				this.ReadResourceSetStart(new ODataResourceSet(), SelectedPropertiesNode.EntireSubtree);
				return;
			case JsonNodeType.EndArray:
				this.ReadResourceSetEnd();
				return;
			case JsonNodeType.PrimitiveValue:
			{
				object value = this.jsonLightResourceDeserializer.JsonReader.Value;
				if (value != null && base.CurrentResourceType.TypeKind == EdmTypeKind.Untyped)
				{
					base.EnterScope(new ODataJsonLightReader.JsonLightPrimitiveScope(new ODataPrimitiveValue(value), base.CurrentNavigationSource, base.CurrentResourceType, base.CurrentScope.ODataUri));
					return;
				}
				this.ReadResourceStart(null, this.CurrentJsonLightResourceSetScope.SelectedProperties);
				return;
			}
			}
			throw new ODataException(Strings.ODataJsonReader_CannotReadResourcesOfResourceSet(this.jsonLightResourceDeserializer.JsonReader.NodeType));
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00041C34 File Offset: 0x0003FE34
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
					ODataJsonLightReader.JsonLightResourceScope jsonLightResourceScope = (ODataJsonLightReader.JsonLightResourceScope)base.ParentScope;
					SelectedPropertiesNode selectedProperties = jsonLightResourceScope.SelectedProperties;
					this.ReadResourceSetStart(readerNestedResourceInfo.NestedResourceSet, selectedProperties.GetSelectedPropertiesForNavigationProperty(jsonLightResourceScope.ResourceType, currentNestedResourceInfo.Name));
				}
			}
			else
			{
				ReaderUtils.CheckForDuplicateNestedResourceInfoNameAndSetAssociationLink(iodataJsonLightReaderResourceState.PropertyAndAnnotationCollector, currentNestedResourceInfo);
				this.ReadNextNestedResourceInfoContentItemInRequest();
			}
			return true;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00041D3C File Offset: 0x0003FF3C
		private bool ReadAtNestedResourceInfoEndImplementationSynchronously()
		{
			base.PopScope(ODataReaderState.NestedResourceInfoEnd);
			IODataJsonLightReaderResourceState currentResourceState = this.CurrentResourceState;
			ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo;
			if (this.jsonLightInputContext.ReadingResponse && currentResourceState.ProcessingMissingProjectedNestedResourceInfos)
			{
				odataJsonLightReaderNestedResourceInfo = currentResourceState.Resource.MetadataBuilder.GetNextUnprocessedNavigationLink();
			}
			else
			{
				odataJsonLightReaderNestedResourceInfo = this.jsonLightResourceDeserializer.ReadResourceContent(currentResourceState);
			}
			if (odataJsonLightReaderNestedResourceInfo == null)
			{
				this.EndEntry();
			}
			else
			{
				this.StartNestedResourceInfo(odataJsonLightReaderNestedResourceInfo);
			}
			return true;
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x00041DA1 File Offset: 0x0003FFA1
		private bool ReadAtEntityReferenceLinkSynchronously()
		{
			base.PopScope(ODataReaderState.EntityReferenceLink);
			this.ReadNextNestedResourceInfoContentItemInRequest();
			return true;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00041DB4 File Offset: 0x0003FFB4
		private void ReadResourceSetStart(ODataResourceSet resourceSet, SelectedPropertiesNode selectedProperties)
		{
			this.jsonLightResourceDeserializer.ReadResourceSetContentStart();
			IJsonReader jsonReader = this.jsonLightResourceDeserializer.JsonReader;
			if (jsonReader.NodeType != JsonNodeType.EndArray && jsonReader.NodeType != JsonNodeType.StartObject && (jsonReader.NodeType != JsonNodeType.PrimitiveValue || jsonReader.Value != null) && (base.CurrentResourceType.TypeKind != EdmTypeKind.Untyped || (jsonReader.NodeType != JsonNodeType.PrimitiveValue && jsonReader.NodeType != JsonNodeType.StartArray)))
			{
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_InvalidNodeTypeForItemsInResourceSet(jsonReader.NodeType));
			}
			base.EnterScope(new ODataJsonLightReader.JsonLightResourceSetScope(resourceSet, base.CurrentNavigationSource, base.CurrentResourceType, selectedProperties, base.CurrentScope.ODataUri));
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00041E58 File Offset: 0x00040058
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
				this.jsonLightResourceDeserializer.ReadNextLinkAnnotationAtResourceSetEnd(base.CurrentResourceSet, odataJsonLightReaderNestedResourceInfo, this.topLevelScope.PropertyAndAnnotationCollector);
			}
			this.ReplaceScope(ODataReaderState.ResourceSetEnd);
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00041EC4 File Offset: 0x000400C4
		private void ReadExpandedNestedResourceInfoStart(ODataNestedResourceInfo nestedResourceInfo)
		{
			if (this.jsonLightResourceDeserializer.JsonReader.NodeType != JsonNodeType.PrimitiveValue)
			{
				ODataJsonLightReader.JsonLightResourceScope jsonLightResourceScope = (ODataJsonLightReader.JsonLightResourceScope)base.ParentScope;
				SelectedPropertiesNode selectedProperties = jsonLightResourceScope.SelectedProperties;
				this.ReadResourceStart(null, selectedProperties.GetSelectedPropertiesForNavigationProperty(jsonLightResourceScope.ResourceType, nestedResourceInfo.Name));
				return;
			}
			IEdmStructuralProperty structuralProperty = this.CurrentJsonLightNestedResourceInfoScope.ReaderNestedResourceInfo.StructuralProperty;
			if (structuralProperty != null && !structuralProperty.Type.IsNullable && (this.jsonLightResourceDeserializer.ReadingResponse || this.jsonLightResourceDeserializer.Model.NullValueReadBehaviorKind(structuralProperty) == ODataNullValueBehaviorKind.Default))
			{
				throw new ODataException(Strings.ReaderValidationUtils_NullNamedValueForNonNullableType(nestedResourceInfo.Name, structuralProperty.Type.FullName()));
			}
			base.EnterScope(new ODataJsonLightReader.JsonLightResourceScope(ODataReaderState.ResourceStart, null, base.CurrentNavigationSource, base.CurrentResourceType, null, null, base.CurrentScope.ODataUri));
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00041F9C File Offset: 0x0004019C
		private void ReadResourceStart(PropertyAndAnnotationCollector propertyAndAnnotationCollector, SelectedPropertiesNode selectedProperties)
		{
			if (this.jsonLightResourceDeserializer.JsonReader.NodeType != JsonNodeType.PrimitiveValue)
			{
				if (this.jsonLightResourceDeserializer.JsonReader.NodeType == JsonNodeType.StartObject)
				{
					this.jsonLightResourceDeserializer.JsonReader.Read();
				}
				if (base.ReadingResourceSet || base.IsExpandedLinkContent)
				{
					string text = this.jsonLightResourceDeserializer.ReadContextUriAnnotation(ODataPayloadKind.Resource, propertyAndAnnotationCollector, false);
					if (text != null)
					{
						text = UriUtils.UriToString(this.jsonLightResourceDeserializer.ProcessUriFromPayload(text));
						ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = ODataJsonLightContextUriParser.Parse(this.jsonLightResourceDeserializer.Model, text, ODataPayloadKind.Resource, this.jsonLightResourceDeserializer.MessageReaderSettings.ClientCustomTypeResolver, this.jsonLightInputContext.ReadingResponse, true);
						if (this.jsonLightInputContext.ReadingResponse && odataJsonLightContextUriParseResult != null)
						{
							ReaderValidationUtils.ValidateResourceSetOrResourceContextUri(odataJsonLightContextUriParseResult, base.CurrentScope, false);
						}
					}
				}
				this.StartResource(propertyAndAnnotationCollector, selectedProperties);
				this.jsonLightResourceDeserializer.ReadResourceTypeName(this.CurrentResourceState);
				base.ApplyResourceTypeNameFromPayload(base.CurrentResource.TypeName);
				if (base.CurrentResourceSetValidator != null)
				{
					base.CurrentResourceSetValidator.ValidateResource(base.CurrentResourceType);
				}
				this.CurrentResourceState.FirstNestedResourceInfo = this.jsonLightResourceDeserializer.ReadResourceContent(this.CurrentResourceState);
				return;
			}
			object value = this.jsonLightResourceDeserializer.JsonReader.Value;
			if (value == null)
			{
				base.EnterScope(new ODataJsonLightReader.JsonLightResourceScope(ODataReaderState.ResourceStart, null, base.CurrentNavigationSource, base.CurrentResourceType, null, null, base.CurrentScope.ODataUri));
				return;
			}
			if (base.CurrentResourceType.TypeKind == EdmTypeKind.Untyped)
			{
				base.EnterScope(new ODataJsonLightReader.JsonLightPrimitiveScope(new ODataPrimitiveValue(value), base.CurrentNavigationSource, base.CurrentResourceType, base.CurrentScope.ODataUri));
				return;
			}
			throw new ODataException(Strings.ODataJsonLightReader_UnexpectedPrimitiveValueForODataResource);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0004213C File Offset: 0x0004033C
		private void ReadExpandedNestedResourceInfoEnd(bool isCollection)
		{
			base.CurrentNestedResourceInfo.IsCollection = new bool?(isCollection);
			IODataJsonLightReaderResourceState iodataJsonLightReaderResourceState = (IODataJsonLightReaderResourceState)base.ParentScope;
			iodataJsonLightReaderResourceState.NavigationPropertiesRead.Add(base.CurrentNestedResourceInfo.Name);
			this.ReplaceScope(ODataReaderState.NestedResourceInfoEnd);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00042184 File Offset: 0x00040384
		private void ReadNextNestedResourceInfoContentItemInRequest()
		{
			ODataJsonLightReaderNestedResourceInfo readerNestedResourceInfo = this.CurrentJsonLightNestedResourceInfoScope.ReaderNestedResourceInfo;
			if (readerNestedResourceInfo.HasEntityReferenceLink)
			{
				base.EnterScope(new ODataReaderCore.Scope(ODataReaderState.EntityReferenceLink, readerNestedResourceInfo.ReportEntityReferenceLink(), null, null, base.CurrentScope.ODataUri));
				return;
			}
			if (!readerNestedResourceInfo.HasValue)
			{
				this.ReplaceScope(ODataReaderState.NestedResourceInfoEnd);
				return;
			}
			if (readerNestedResourceInfo.NestedResourceInfo.IsCollection == true)
			{
				SelectedPropertiesNode entireSubtree = SelectedPropertiesNode.EntireSubtree;
				this.ReadResourceSetStart(readerNestedResourceInfo.NestedResourceSet ?? new ODataResourceSet(), entireSubtree);
				return;
			}
			this.ReadExpandedNestedResourceInfoStart(readerNestedResourceInfo.NestedResourceInfo);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00042222 File Offset: 0x00040422
		private void StartResource(PropertyAndAnnotationCollector propertyAndAnnotationCollector, SelectedPropertiesNode selectedProperties)
		{
			base.EnterScope(new ODataJsonLightReader.JsonLightResourceScope(ODataReaderState.ResourceStart, ReaderUtils.CreateNewResource(), base.CurrentNavigationSource, base.CurrentResourceType, propertyAndAnnotationCollector ?? this.jsonLightInputContext.CreatePropertyAndAnnotationCollector(), selectedProperties, base.CurrentScope.ODataUri));
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00042260 File Offset: 0x00040460
		private void StartNestedResourceInfo(ODataJsonLightReaderNestedResourceInfo readerNestedResourceInfo)
		{
			ODataNestedResourceInfo nestedResourceInfo = readerNestedResourceInfo.NestedResourceInfo;
			IEdmProperty nestedProperty = readerNestedResourceInfo.NestedProperty;
			IEdmStructuredType edmStructuredType = readerNestedResourceInfo.NestedResourceType;
			if (edmStructuredType == null && nestedProperty != null)
			{
				IEdmTypeReference type = nestedProperty.Type;
				edmStructuredType = (type.IsCollection() ? type.AsCollection().ElementType().AsStructured()
					.StructuredDefinition() : type.AsStructured().StructuredDefinition());
			}
			if (this.jsonLightInputContext.ReadingResponse && !base.IsReadingNestedPayload && (edmStructuredType == null || edmStructuredType.IsStructuredOrStructuredCollectionType()))
			{
				this.CurrentResourceState.ResourceTypeFromMetadata = base.ParentScope.ResourceType;
				ODataResourceMetadataBuilder resourceMetadataBuilderForReader = this.jsonLightResourceDeserializer.MetadataContext.GetResourceMetadataBuilderForReader(this.CurrentResourceState, this.jsonLightInputContext.ODataSimplifiedOptions.EnableReadingKeyAsSegment);
				nestedResourceInfo.MetadataBuilder = resourceMetadataBuilderForReader;
			}
			IEdmNavigationProperty navigationProperty = readerNestedResourceInfo.NavigationProperty;
			ODataJsonLightReader.JsonLightResourceScope jsonLightResourceScope = base.CurrentScope as ODataJsonLightReader.JsonLightResourceScope;
			ODataUri odataUri = base.CurrentScope.ODataUri.Clone();
			ODataPath odataPath = odataUri.Path ?? new ODataPath(new ODataPathSegment[0]);
			if (jsonLightResourceScope != null && jsonLightResourceScope.ResourceTypeFromMetadata != jsonLightResourceScope.ResourceType)
			{
				odataPath.Add(new TypeSegment(jsonLightResourceScope.ResourceType, null));
			}
			IEdmNavigationSource edmNavigationSource;
			if (navigationProperty == null)
			{
				edmNavigationSource = base.CurrentNavigationSource;
			}
			else
			{
				IEdmPathExpression edmPathExpression;
				edmNavigationSource = ((base.CurrentNavigationSource == null) ? null : base.CurrentNavigationSource.FindNavigationTarget(navigationProperty, new Func<IEdmPathExpression, List<ODataPathSegment>, bool>(BindingPathHelper.MatchBindingPath), Enumerable.ToList<ODataPathSegment>(odataPath), out edmPathExpression));
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
			base.EnterScope(new ODataJsonLightReader.JsonLightNestedResourceInfoScope(readerNestedResourceInfo, edmNavigationSource, edmStructuredType, odataUri));
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00042474 File Offset: 0x00040674
		private bool TryAppendEntitySetKeySegment(ref ODataPath odataPath)
		{
			try
			{
				if (EdmExtensionMethods.HasKey(base.CurrentScope.NavigationSource, base.CurrentScope.ResourceType))
				{
					IEdmEntityType edmEntityType = base.CurrentScope.ResourceType as IEdmEntityType;
					ODataResource odataResource = base.CurrentScope.Item as ODataResource;
					KeyValuePair<string, object>[] keyProperties = ODataResourceMetadataContext.GetKeyProperties(odataResource, null, edmEntityType);
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

		// Token: 0x060015B7 RID: 5559 RVA: 0x000424FC File Offset: 0x000406FC
		private void ReplaceScope(ODataReaderState state)
		{
			base.ReplaceScope(new ODataReaderCore.Scope(state, this.Item, base.CurrentNavigationSource, base.CurrentResourceType, base.CurrentScope.ODataUri));
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00042528 File Offset: 0x00040728
		private void EndEntry()
		{
			IODataJsonLightReaderResourceState currentResourceState = this.CurrentResourceState;
			if (base.CurrentResource != null && !base.IsReadingNestedPayload)
			{
				foreach (string text in this.CurrentResourceState.NavigationPropertiesRead)
				{
					base.CurrentResource.MetadataBuilder.MarkNestedResourceInfoProcessed(text);
				}
				ODataConventionalEntityMetadataBuilder odataConventionalEntityMetadataBuilder = base.CurrentResource.MetadataBuilder as ODataConventionalEntityMetadataBuilder;
				if (odataConventionalEntityMetadataBuilder != null)
				{
					odataConventionalEntityMetadataBuilder.EndResource();
				}
			}
			this.jsonLightResourceDeserializer.ValidateMediaEntity(currentResourceState);
			if (this.jsonLightInputContext.ReadingResponse && base.CurrentResource != null)
			{
				ODataJsonLightReaderNestedResourceInfo nextUnprocessedNavigationLink = base.CurrentResource.MetadataBuilder.GetNextUnprocessedNavigationLink();
				if (nextUnprocessedNavigationLink != null)
				{
					this.CurrentResourceState.ProcessingMissingProjectedNestedResourceInfos = true;
					this.StartNestedResourceInfo(nextUnprocessedNavigationLink);
					return;
				}
			}
			base.EndEntry(new ODataJsonLightReader.JsonLightResourceScope(ODataReaderState.ResourceEnd, (ODataResource)this.Item, base.CurrentNavigationSource, base.CurrentResourceType, this.CurrentResourceState.PropertyAndAnnotationCollector, this.CurrentResourceState.SelectedProperties, base.CurrentScope.ODataUri));
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0004264C File Offset: 0x0004084C
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
						base.CurrentScope.ResourceType = edmStructuredType;
					}
				}
			}
		}

		// Token: 0x04000A34 RID: 2612
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000A35 RID: 2613
		private readonly ODataJsonLightResourceDeserializer jsonLightResourceDeserializer;

		// Token: 0x04000A36 RID: 2614
		private readonly ODataJsonLightReader.JsonLightTopLevelScope topLevelScope;

		// Token: 0x04000A37 RID: 2615
		private readonly bool readingParameter;

		// Token: 0x02000355 RID: 853
		private sealed class JsonLightTopLevelScope : ODataReaderCore.Scope
		{
			// Token: 0x06001AE9 RID: 6889 RVA: 0x0004C4CC File Offset: 0x0004A6CC
			internal JsonLightTopLevelScope(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType, ODataUri odataUri)
				: base(ODataReaderState.Start, null, navigationSource, expectedResourceType, odataUri)
			{
			}

			// Token: 0x170005D1 RID: 1489
			// (get) Token: 0x06001AEA RID: 6890 RVA: 0x0004C4D9 File Offset: 0x0004A6D9
			// (set) Token: 0x06001AEB RID: 6891 RVA: 0x0004C4E1 File Offset: 0x0004A6E1
			public PropertyAndAnnotationCollector PropertyAndAnnotationCollector { get; set; }
		}

		// Token: 0x02000356 RID: 854
		private sealed class JsonLightPrimitiveScope : ODataReaderCore.Scope
		{
			// Token: 0x06001AEC RID: 6892 RVA: 0x0004C4EA File Offset: 0x0004A6EA
			internal JsonLightPrimitiveScope(ODataValue primitiveValue, IEdmNavigationSource navigationSource, IEdmStructuredType expectedType, ODataUri odataUri)
				: base(ODataReaderState.Primitive, primitiveValue, navigationSource, expectedType, odataUri)
			{
			}
		}

		// Token: 0x02000357 RID: 855
		private sealed class JsonLightResourceScope : ODataReaderCore.Scope, IODataJsonLightReaderResourceState
		{
			// Token: 0x06001AED RID: 6893 RVA: 0x0004C4F9 File Offset: 0x0004A6F9
			internal JsonLightResourceScope(ODataReaderState readerState, ODataResource resource, IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType, PropertyAndAnnotationCollector propertyAndAnnotationCollector, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(readerState, resource, navigationSource, expectedResourceType, odataUri)
			{
				this.PropertyAndAnnotationCollector = propertyAndAnnotationCollector;
				this.SelectedProperties = selectedProperties;
			}

			// Token: 0x170005D2 RID: 1490
			// (get) Token: 0x06001AEE RID: 6894 RVA: 0x0004C518 File Offset: 0x0004A718
			// (set) Token: 0x06001AEF RID: 6895 RVA: 0x0004C520 File Offset: 0x0004A720
			public ODataResourceMetadataBuilder MetadataBuilder { get; set; }

			// Token: 0x170005D3 RID: 1491
			// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x0004C529 File Offset: 0x0004A729
			// (set) Token: 0x06001AF1 RID: 6897 RVA: 0x0004C531 File Offset: 0x0004A731
			public bool AnyPropertyFound { get; set; }

			// Token: 0x170005D4 RID: 1492
			// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x0004C53A File Offset: 0x0004A73A
			// (set) Token: 0x06001AF3 RID: 6899 RVA: 0x0004C542 File Offset: 0x0004A742
			public ODataJsonLightReaderNestedResourceInfo FirstNestedResourceInfo { get; set; }

			// Token: 0x170005D5 RID: 1493
			// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x0004C54B File Offset: 0x0004A74B
			// (set) Token: 0x06001AF5 RID: 6901 RVA: 0x0004C553 File Offset: 0x0004A753
			public PropertyAndAnnotationCollector PropertyAndAnnotationCollector { get; private set; }

			// Token: 0x170005D6 RID: 1494
			// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x0004C55C File Offset: 0x0004A75C
			// (set) Token: 0x06001AF7 RID: 6903 RVA: 0x0004C564 File Offset: 0x0004A764
			public SelectedPropertiesNode SelectedProperties { get; private set; }

			// Token: 0x170005D7 RID: 1495
			// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x0004C570 File Offset: 0x0004A770
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

			// Token: 0x170005D8 RID: 1496
			// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x0004C595 File Offset: 0x0004A795
			// (set) Token: 0x06001AFA RID: 6906 RVA: 0x0004C59D File Offset: 0x0004A79D
			public bool ProcessingMissingProjectedNestedResourceInfos { get; set; }

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x06001AFB RID: 6907 RVA: 0x0004C5A6 File Offset: 0x0004A7A6
			ODataResource IODataJsonLightReaderResourceState.Resource
			{
				get
				{
					return (ODataResource)base.Item;
				}
			}

			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x06001AFC RID: 6908 RVA: 0x0004C5B3 File Offset: 0x0004A7B3
			IEdmStructuredType IODataJsonLightReaderResourceState.ResourceType
			{
				get
				{
					return base.ResourceType;
				}
			}

			// Token: 0x170005DB RID: 1499
			// (get) Token: 0x06001AFD RID: 6909 RVA: 0x0004C5BB File Offset: 0x0004A7BB
			// (set) Token: 0x06001AFE RID: 6910 RVA: 0x0004C5C3 File Offset: 0x0004A7C3
			public IEdmStructuredType ResourceTypeFromMetadata { get; set; }

			// Token: 0x170005DC RID: 1500
			// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0004C5CC File Offset: 0x0004A7CC
			IEdmNavigationSource IODataJsonLightReaderResourceState.NavigationSource
			{
				get
				{
					return base.NavigationSource;
				}
			}

			// Token: 0x04000D8A RID: 3466
			private List<string> navigationPropertiesRead;
		}

		// Token: 0x02000358 RID: 856
		private sealed class JsonLightResourceSetScope : ODataReaderCore.Scope
		{
			// Token: 0x06001B00 RID: 6912 RVA: 0x0004C5D4 File Offset: 0x0004A7D4
			internal JsonLightResourceSetScope(ODataResourceSet resourceSet, IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataReaderState.ResourceSetStart, resourceSet, navigationSource, expectedResourceType, odataUri)
			{
				this.SelectedProperties = selectedProperties;
			}

			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x06001B01 RID: 6913 RVA: 0x0004C5EA File Offset: 0x0004A7EA
			// (set) Token: 0x06001B02 RID: 6914 RVA: 0x0004C5F2 File Offset: 0x0004A7F2
			public SelectedPropertiesNode SelectedProperties { get; private set; }
		}

		// Token: 0x02000359 RID: 857
		private sealed class JsonLightNestedResourceInfoScope : ODataReaderCore.Scope
		{
			// Token: 0x06001B03 RID: 6915 RVA: 0x0004C5FB File Offset: 0x0004A7FB
			internal JsonLightNestedResourceInfoScope(ODataJsonLightReaderNestedResourceInfo nestedResourceInfo, IEdmNavigationSource navigationSource, IEdmStructuredType expectedStructuredType, ODataUri odataUri)
				: base(ODataReaderState.NestedResourceInfoStart, nestedResourceInfo.NestedResourceInfo, navigationSource, expectedStructuredType, odataUri)
			{
				this.ReaderNestedResourceInfo = nestedResourceInfo;
			}

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x06001B04 RID: 6916 RVA: 0x0004C615 File Offset: 0x0004A815
			// (set) Token: 0x06001B05 RID: 6917 RVA: 0x0004C61D File Offset: 0x0004A81D
			public ODataJsonLightReaderNestedResourceInfo ReaderNestedResourceInfo { get; private set; }
		}
	}
}
