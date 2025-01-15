using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200021E RID: 542
	internal sealed class ODataJsonLightWriter : ODataWriterCore
	{
		// Token: 0x060015FA RID: 5626 RVA: 0x00043564 File Offset: 0x00041764
		internal ODataJsonLightWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool writingResourceSet, bool writingParameter = false, bool writingDelta = false, IODataReaderWriterListener listener = null)
			: base(jsonLightOutputContext, navigationSource, resourceType, writingResourceSet, writingDelta, listener)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightResourceSerializer = new ODataJsonLightResourceSerializer(this.jsonLightOutputContext);
			this.jsonLightValueSerializer = new ODataJsonLightValueSerializer(this.jsonLightOutputContext, false);
			this.writingParameter = writingParameter;
			this.jsonWriter = this.jsonLightOutputContext.JsonWriter;
			this.odataAnnotationWriter = new JsonLightODataAnnotationWriter(this.jsonWriter, this.jsonLightOutputContext.ODataSimplifiedOptions.EnableWritingODataAnnotationWithoutPrefix);
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x000435E4 File Offset: 0x000417E4
		private ODataJsonLightWriter.JsonLightResourceScope CurrentResourceScope
		{
			get
			{
				return base.CurrentScope as ODataJsonLightWriter.JsonLightResourceScope;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x00043600 File Offset: 0x00041800
		private ODataJsonLightWriter.JsonLightResourceSetScope CurrentResourceSetScope
		{
			get
			{
				return base.CurrentScope as ODataJsonLightWriter.JsonLightResourceSetScope;
			}
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0004361A File Offset: 0x0004181A
		protected override void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00043627 File Offset: 0x00041827
		protected override void FlushSynchronously()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00043634 File Offset: 0x00041834
		protected override void StartPayload()
		{
			this.jsonLightResourceSerializer.WritePayloadStart();
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00043641 File Offset: 0x00041841
		protected override void EndPayload()
		{
			this.jsonLightResourceSerializer.WritePayloadEnd();
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00043650 File Offset: 0x00041850
		protected override void PrepareResourceForWriteStart(ODataWriterCore.ResourceScope resourceScope, ODataResource resource, bool writingResponse, SelectedPropertiesNode selectedProperties)
		{
			ODataResourceTypeContext orCreateTypeContext = resourceScope.GetOrCreateTypeContext(writingResponse);
			if (this.jsonLightOutputContext.MetadataLevel is JsonNoMetadataLevel)
			{
				this.InnerPrepareResourceForWriteStart(resource, orCreateTypeContext, selectedProperties);
				return;
			}
			if (this.jsonLightOutputContext.Model.IsUserModel() || resourceScope.SerializationInfo != null)
			{
				this.InnerPrepareResourceForWriteStart(resource, orCreateTypeContext, selectedProperties);
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x000436A8 File Offset: 0x000418A8
		protected override void StartResource(ODataResource resource)
		{
			ODataNestedResourceInfo parentNestedResourceInfo = base.ParentNestedResourceInfo;
			if (parentNestedResourceInfo != null)
			{
				if (resource == null)
				{
					if (parentNestedResourceInfo.TypeAnnotation != null && parentNestedResourceInfo.TypeAnnotation.TypeName != null)
					{
						this.jsonLightResourceSerializer.ODataAnnotationWriter.WriteODataTypePropertyAnnotation(parentNestedResourceInfo.Name, parentNestedResourceInfo.TypeAnnotation.TypeName);
					}
					this.jsonLightResourceSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(parentNestedResourceInfo.GetInstanceAnnotations(), parentNestedResourceInfo.Name, false);
				}
				this.jsonWriter.WriteName(parentNestedResourceInfo.Name);
			}
			if (resource == null)
			{
				this.jsonWriter.WriteValue(null);
				return;
			}
			this.jsonWriter.StartObjectScope();
			ODataJsonLightWriter.JsonLightResourceScope currentResourceScope = this.CurrentResourceScope;
			if (base.IsTopLevel)
			{
				ODataContextUrlInfo odataContextUrlInfo = this.jsonLightResourceSerializer.WriteResourceContextUri(currentResourceScope.GetOrCreateTypeContext(this.jsonLightOutputContext.WritingResponse), null);
				if (odataContextUrlInfo != null)
				{
					currentResourceScope.IsUndeclared = odataContextUrlInfo.IsUndeclared != null && odataContextUrlInfo.IsUndeclared.Value;
				}
			}
			this.jsonLightResourceSerializer.WriteResourceStartMetadataProperties(currentResourceScope);
			this.jsonLightResourceSerializer.WriteResourceMetadataProperties(currentResourceScope);
			this.jsonLightResourceSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(resource.InstanceAnnotations, currentResourceScope.InstanceAnnotationWriteTracker, false, null);
			this.jsonLightOutputContext.PropertyCacheHandler.SetCurrentResourceScopeLevel(base.ScopeLevel);
			this.jsonLightResourceSerializer.WriteProperties(base.ResourceType, resource.Properties, false, base.DuplicatePropertyNameChecker);
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00043804 File Offset: 0x00041A04
		protected override void EndResource(ODataResource resource)
		{
			if (resource == null)
			{
				return;
			}
			ODataJsonLightWriter.JsonLightResourceScope currentResourceScope = this.CurrentResourceScope;
			this.jsonLightResourceSerializer.WriteResourceMetadataProperties(currentResourceScope);
			this.jsonLightResourceSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(resource.InstanceAnnotations, currentResourceScope.InstanceAnnotationWriteTracker, false, null);
			this.jsonLightResourceSerializer.WriteResourceEndMetadataProperties(currentResourceScope, currentResourceScope.DuplicatePropertyNameChecker);
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00043864 File Offset: 0x00041A64
		protected override void StartResourceSet(ODataResourceSet resourceSet)
		{
			if (base.ParentNestedResourceInfo == null && (this.writingParameter || base.ParentScope.State == ODataWriterCore.WriterState.ResourceSet))
			{
				this.jsonWriter.StartArrayScope();
			}
			else if (base.ParentNestedResourceInfo == null)
			{
				this.jsonWriter.StartObjectScope();
				this.jsonLightResourceSerializer.WriteResourceSetContextUri(this.CurrentResourceSetScope.GetOrCreateTypeContext(this.jsonLightOutputContext.WritingResponse));
				if (this.jsonLightOutputContext.WritingResponse)
				{
					IEnumerable<ODataAction> actions = resourceSet.Actions;
					if (actions != null && Enumerable.Any<ODataAction>(actions))
					{
						this.jsonLightResourceSerializer.WriteOperations(Enumerable.Cast<ODataOperation>(actions), true);
					}
					IEnumerable<ODataFunction> functions = resourceSet.Functions;
					if (functions != null && Enumerable.Any<ODataFunction>(functions))
					{
						this.jsonLightResourceSerializer.WriteOperations(Enumerable.Cast<ODataOperation>(functions), false);
					}
					this.WriteResourceSetCount(resourceSet, null);
					this.WriteResourceSetNextLink(resourceSet, null);
					this.WriteResourceSetDeltaLink(resourceSet);
				}
				this.jsonLightResourceSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(resourceSet.InstanceAnnotations, this.CurrentResourceSetScope.InstanceAnnotationWriteTracker, false, null);
				this.jsonWriter.WriteValuePropertyName();
				this.jsonWriter.StartArrayScope();
			}
			else
			{
				base.ValidateNoDeltaLinkForExpandedResourceSet(resourceSet);
				this.ValidateNoCustomInstanceAnnotationsForExpandedResourceSet(resourceSet);
				string name = base.ParentNestedResourceInfo.Name;
				bool isUndeclared = (base.CurrentScope as ODataJsonLightWriter.JsonLightResourceSetScope).IsUndeclared;
				string expectedResourceTypeName = this.CurrentResourceSetScope.GetOrCreateTypeContext(this.jsonLightOutputContext.WritingResponse).ExpectedResourceTypeName;
				if (this.jsonLightOutputContext.WritingResponse)
				{
					this.WriteResourceSetCount(resourceSet, name);
					this.WriteResourceSetNextLink(resourceSet, name);
					this.jsonLightResourceSerializer.WriteResourceSetStartMetadataProperties(resourceSet, name, expectedResourceTypeName, isUndeclared);
					this.jsonWriter.WriteName(name);
					this.jsonWriter.StartArrayScope();
				}
				else
				{
					ODataJsonLightWriter.JsonLightNestedResourceInfoScope jsonLightNestedResourceInfoScope = (ODataJsonLightWriter.JsonLightNestedResourceInfoScope)base.ParentNestedResourceInfoScope;
					if (!jsonLightNestedResourceInfoScope.ResourceSetWritten)
					{
						if (jsonLightNestedResourceInfoScope.EntityReferenceLinkWritten)
						{
							this.jsonWriter.EndArrayScope();
						}
						this.jsonLightResourceSerializer.WriteResourceSetStartMetadataProperties(resourceSet, name, expectedResourceTypeName, isUndeclared);
						this.jsonWriter.WriteName(name);
						this.jsonWriter.StartArrayScope();
						jsonLightNestedResourceInfoScope.ResourceSetWritten = true;
					}
				}
			}
			this.jsonLightOutputContext.PropertyCacheHandler.EnterResourceSetScope(this.CurrentResourceSetScope.ResourceType, base.ScopeLevel);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00043A8C File Offset: 0x00041C8C
		protected override void EndResourceSet(ODataResourceSet resourceSet)
		{
			if (base.ParentNestedResourceInfo == null && (this.writingParameter || base.ParentScope.State == ODataWriterCore.WriterState.ResourceSet))
			{
				this.jsonWriter.EndArrayScope();
			}
			else if (base.ParentNestedResourceInfo == null)
			{
				this.jsonWriter.EndArrayScope();
				this.jsonLightResourceSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(resourceSet.InstanceAnnotations, this.CurrentResourceSetScope.InstanceAnnotationWriteTracker, false, null);
				if (this.jsonLightOutputContext.WritingResponse)
				{
					this.WriteResourceSetNextLink(resourceSet, null);
					this.WriteResourceSetDeltaLink(resourceSet);
				}
				this.jsonWriter.EndObjectScope();
			}
			else
			{
				string name = base.ParentNestedResourceInfo.Name;
				base.ValidateNoDeltaLinkForExpandedResourceSet(resourceSet);
				this.ValidateNoCustomInstanceAnnotationsForExpandedResourceSet(resourceSet);
				if (this.jsonLightOutputContext.WritingResponse)
				{
					this.jsonWriter.EndArrayScope();
					this.WriteResourceSetNextLink(resourceSet, name);
				}
			}
			this.jsonLightOutputContext.PropertyCacheHandler.LeaveResourceSetScope();
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00043B70 File Offset: 0x00041D70
		protected override void WritePrimitiveValue(ODataPrimitiveValue primitiveValue)
		{
			this.jsonLightValueSerializer.WritePrimitiveValue((primitiveValue == null) ? null : primitiveValue.Value, null);
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00043B8A File Offset: 0x00041D8A
		protected override void WriteDeferredNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo)
		{
			this.jsonLightResourceSerializer.WriteNavigationLinkMetadata(nestedResourceInfo, base.DuplicatePropertyNameChecker);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00043BA0 File Offset: 0x00041DA0
		protected override void StartNestedResourceInfoWithContent(ODataNestedResourceInfo nestedResourceInfo)
		{
			if (this.jsonLightOutputContext.WritingResponse)
			{
				IEdmContainedEntitySet edmContainedEntitySet = base.CurrentScope.NavigationSource as IEdmContainedEntitySet;
				if (edmContainedEntitySet != null)
				{
					ODataContextUrlInfo odataContextUrlInfo = ODataContextUrlInfo.Create(base.CurrentScope.NavigationSource, base.CurrentScope.ResourceType.FullTypeName(), edmContainedEntitySet.NavigationProperty.Type.TypeKind() != EdmTypeKind.Collection, base.CurrentScope.ODataUri);
					this.jsonLightResourceSerializer.WriteNestedResourceInfoContextUrl(nestedResourceInfo, odataContextUrlInfo);
				}
				this.jsonLightResourceSerializer.WriteNavigationLinkMetadata(nestedResourceInfo, base.DuplicatePropertyNameChecker);
				return;
			}
			this.WriterValidator.ValidateNestedResourceInfoHasCardinality(nestedResourceInfo);
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00043C3C File Offset: 0x00041E3C
		protected override void EndNestedResourceInfoWithContent(ODataNestedResourceInfo nestedResourceInfo)
		{
			if (!this.jsonLightOutputContext.WritingResponse)
			{
				ODataJsonLightWriter.JsonLightNestedResourceInfoScope jsonLightNestedResourceInfoScope = (ODataJsonLightWriter.JsonLightNestedResourceInfoScope)base.CurrentScope;
				if (jsonLightNestedResourceInfoScope.EntityReferenceLinkWritten && !jsonLightNestedResourceInfoScope.ResourceSetWritten && nestedResourceInfo.IsCollection.Value)
				{
					this.jsonWriter.EndArrayScope();
				}
				if (jsonLightNestedResourceInfoScope.ResourceSetWritten)
				{
					this.jsonWriter.EndArrayScope();
				}
			}
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00043CA0 File Offset: 0x00041EA0
		protected override void WriteEntityReferenceInNavigationLinkContent(ODataNestedResourceInfo parentNestedResourceInfo, ODataEntityReferenceLink entityReferenceLink)
		{
			ODataJsonLightWriter.JsonLightNestedResourceInfoScope jsonLightNestedResourceInfoScope = (ODataJsonLightWriter.JsonLightNestedResourceInfoScope)base.CurrentScope;
			if (jsonLightNestedResourceInfoScope.ResourceSetWritten)
			{
				throw new ODataException(Strings.ODataJsonLightWriter_EntityReferenceLinkAfterResourceSetInRequest);
			}
			if (!jsonLightNestedResourceInfoScope.EntityReferenceLinkWritten)
			{
				this.odataAnnotationWriter.WritePropertyAnnotationName(parentNestedResourceInfo.Name, "odata.bind");
				if (parentNestedResourceInfo.IsCollection.Value)
				{
					this.jsonWriter.StartArrayScope();
				}
				jsonLightNestedResourceInfoScope.EntityReferenceLinkWritten = true;
			}
			this.jsonWriter.WriteValue(this.jsonLightResourceSerializer.UriToString(entityReferenceLink.Url));
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00043D28 File Offset: 0x00041F28
		protected override ODataWriterCore.ResourceSetScope CreateResourceSetScope(ODataResourceSet resourceSet, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared)
		{
			return new ODataJsonLightWriter.JsonLightResourceSetScope(resourceSet, navigationSource, resourceType, skipWriting, selectedProperties, odataUri, isUndeclared);
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00043D3C File Offset: 0x00041F3C
		protected override ODataWriterCore.ResourceScope CreateResourceScope(ODataResource resource, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared)
		{
			return new ODataJsonLightWriter.JsonLightResourceScope(resource, base.GetResourceSerializationInfo(resource), navigationSource, resourceType, skipWriting, this.jsonLightOutputContext.MessageWriterSettings, selectedProperties, odataUri, isUndeclared);
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00043D6B File Offset: 0x00041F6B
		protected override ODataWriterCore.NestedResourceInfoScope CreateNestedResourceInfoScope(ODataWriterCore.WriterState writerState, ODataNestedResourceInfo navLink, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightWriter.JsonLightNestedResourceInfoScope(writerState, navLink, navigationSource, resourceType, skipWriting, selectedProperties, odataUri);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00043D80 File Offset: 0x00041F80
		private void InnerPrepareResourceForWriteStart(ODataResource resource, ODataResourceTypeContext typeContext, SelectedPropertiesNode selectedProperties)
		{
			ODataWriterCore.ResourceScope resourceScope = (ODataWriterCore.ResourceScope)base.CurrentScope;
			ODataResourceMetadataBuilder odataResourceMetadataBuilder = this.jsonLightOutputContext.MetadataLevel.CreateResourceMetadataBuilder(resource, typeContext, resourceScope.SerializationInfo, resourceScope.ResourceType, selectedProperties, this.jsonLightOutputContext.WritingResponse, this.jsonLightOutputContext.ODataSimplifiedOptions.EnableWritingKeyAsSegment, resourceScope.ODataUri);
			if (odataResourceMetadataBuilder != null)
			{
				odataResourceMetadataBuilder.NameAsProperty = ((base.BelongingNestedResourceInfo != null) ? base.BelongingNestedResourceInfo.Name : null);
				odataResourceMetadataBuilder.IsFromCollection = base.BelongingNestedResourceInfo != null && base.BelongingNestedResourceInfo.IsCollection == true;
				if (odataResourceMetadataBuilder is ODataConventionalResourceMetadataBuilder)
				{
					odataResourceMetadataBuilder.ParentMetadataBuilder = this.FindParentResourceMetadataBuilder();
				}
				this.jsonLightOutputContext.MetadataLevel.InjectMetadataBuilder(resource, odataResourceMetadataBuilder);
			}
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00043E54 File Offset: 0x00042054
		private ODataResourceMetadataBuilder FindParentResourceMetadataBuilder()
		{
			ODataWriterCore.ResourceScope parentResourceScope = base.GetParentResourceScope();
			if (parentResourceScope != null)
			{
				ODataResource odataResource = parentResourceScope.Item as ODataResource;
				if (odataResource != null)
				{
					return odataResource.MetadataBuilder;
				}
			}
			return null;
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00043E84 File Offset: 0x00042084
		private void WriteResourceSetCount(ODataResourceSet resourceSet, string propertyName)
		{
			long? count = resourceSet.Count;
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

		// Token: 0x06001611 RID: 5649 RVA: 0x00043EDC File Offset: 0x000420DC
		private void WriteResourceSetNextLink(ODataResourceSet resourceSet, string propertyName)
		{
			Uri nextPageLink = resourceSet.NextPageLink;
			if (nextPageLink != null && !this.CurrentResourceSetScope.NextPageLinkWritten)
			{
				if (propertyName == null)
				{
					this.odataAnnotationWriter.WriteInstanceAnnotationName("odata.nextLink");
				}
				else
				{
					this.odataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.nextLink");
				}
				this.jsonWriter.WriteValue(this.jsonLightResourceSerializer.UriToString(nextPageLink));
				this.CurrentResourceSetScope.NextPageLinkWritten = true;
			}
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00043F50 File Offset: 0x00042150
		private void WriteResourceSetDeltaLink(ODataResourceSet resourceSet)
		{
			Uri deltaLink = resourceSet.DeltaLink;
			if (deltaLink != null && !this.CurrentResourceSetScope.DeltaLinkWritten)
			{
				this.odataAnnotationWriter.WriteInstanceAnnotationName("odata.deltaLink");
				this.jsonWriter.WriteValue(this.jsonLightResourceSerializer.UriToString(deltaLink));
				this.CurrentResourceSetScope.DeltaLinkWritten = true;
			}
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00043FAD File Offset: 0x000421AD
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "An instance field is used in a debug assert.")]
		private void ValidateNoCustomInstanceAnnotationsForExpandedResourceSet(ODataResourceSet resourceSet)
		{
			if (resourceSet.InstanceAnnotations.Count > 0)
			{
				throw new ODataException(Strings.ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedResourceSet);
			}
		}

		// Token: 0x04000A45 RID: 2629
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000A46 RID: 2630
		private readonly ODataJsonLightResourceSerializer jsonLightResourceSerializer;

		// Token: 0x04000A47 RID: 2631
		private readonly ODataJsonLightValueSerializer jsonLightValueSerializer;

		// Token: 0x04000A48 RID: 2632
		private readonly bool writingParameter;

		// Token: 0x04000A49 RID: 2633
		private readonly IJsonWriter jsonWriter;

		// Token: 0x04000A4A RID: 2634
		private readonly JsonLightODataAnnotationWriter odataAnnotationWriter;

		// Token: 0x02000361 RID: 865
		private sealed class JsonLightResourceSetScope : ODataWriterCore.ResourceSetScope
		{
			// Token: 0x06001B15 RID: 6933 RVA: 0x0004CB48 File Offset: 0x0004AD48
			internal JsonLightResourceSetScope(ODataResourceSet resourceSet, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared)
				: base(resourceSet, navigationSource, resourceType, skipWriting, selectedProperties, odataUri)
			{
				this.isUndeclared = isUndeclared;
			}

			// Token: 0x170005DF RID: 1503
			// (get) Token: 0x06001B16 RID: 6934 RVA: 0x0004CB61 File Offset: 0x0004AD61
			// (set) Token: 0x06001B17 RID: 6935 RVA: 0x0004CB69 File Offset: 0x0004AD69
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

			// Token: 0x170005E0 RID: 1504
			// (get) Token: 0x06001B18 RID: 6936 RVA: 0x0004CB72 File Offset: 0x0004AD72
			// (set) Token: 0x06001B19 RID: 6937 RVA: 0x0004CB7A File Offset: 0x0004AD7A
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

			// Token: 0x170005E1 RID: 1505
			// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0004CB83 File Offset: 0x0004AD83
			internal bool IsUndeclared
			{
				get
				{
					return this.isUndeclared;
				}
			}

			// Token: 0x04000DB2 RID: 3506
			private bool nextLinkWritten;

			// Token: 0x04000DB3 RID: 3507
			private bool deltaLinkWritten;

			// Token: 0x04000DB4 RID: 3508
			private bool isUndeclared;
		}

		// Token: 0x02000362 RID: 866
		private sealed class JsonLightResourceScope : ODataWriterCore.ResourceScope, IODataJsonLightWriterResourceState
		{
			// Token: 0x06001B1B RID: 6939 RVA: 0x0004CB8C File Offset: 0x0004AD8C
			internal JsonLightResourceScope(ODataResource resource, ODataResourceSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, ODataMessageWriterSettings writerSettings, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared)
				: base(resource, serializationInfo, navigationSource, resourceType, skipWriting, writerSettings, selectedProperties, odataUri)
			{
				this.isUndeclared = isUndeclared;
			}

			// Token: 0x170005E2 RID: 1506
			// (get) Token: 0x06001B1C RID: 6940 RVA: 0x0004CBB4 File Offset: 0x0004ADB4
			public ODataResource Resource
			{
				get
				{
					return (ODataResource)base.Item;
				}
			}

			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0004CBC1 File Offset: 0x0004ADC1
			// (set) Token: 0x06001B1E RID: 6942 RVA: 0x0004CBC9 File Offset: 0x0004ADC9
			public bool IsUndeclared
			{
				get
				{
					return this.isUndeclared;
				}
				set
				{
					this.isUndeclared = value;
				}
			}

			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0004CBD2 File Offset: 0x0004ADD2
			// (set) Token: 0x06001B20 RID: 6944 RVA: 0x0004CBDB File Offset: 0x0004ADDB
			public bool EditLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty.EditLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty.EditLink);
				}
			}

			// Token: 0x170005E5 RID: 1509
			// (get) Token: 0x06001B21 RID: 6945 RVA: 0x0004CBE4 File Offset: 0x0004ADE4
			// (set) Token: 0x06001B22 RID: 6946 RVA: 0x0004CBED File Offset: 0x0004ADED
			public bool ReadLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty.ReadLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty.ReadLink);
				}
			}

			// Token: 0x170005E6 RID: 1510
			// (get) Token: 0x06001B23 RID: 6947 RVA: 0x0004CBF6 File Offset: 0x0004ADF6
			// (set) Token: 0x06001B24 RID: 6948 RVA: 0x0004CBFF File Offset: 0x0004ADFF
			public bool MediaEditLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty.MediaEditLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty.MediaEditLink);
				}
			}

			// Token: 0x170005E7 RID: 1511
			// (get) Token: 0x06001B25 RID: 6949 RVA: 0x0004CC08 File Offset: 0x0004AE08
			// (set) Token: 0x06001B26 RID: 6950 RVA: 0x0004CC11 File Offset: 0x0004AE11
			public bool MediaReadLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty.MediaReadLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty.MediaReadLink);
				}
			}

			// Token: 0x170005E8 RID: 1512
			// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0004CC1A File Offset: 0x0004AE1A
			// (set) Token: 0x06001B28 RID: 6952 RVA: 0x0004CC24 File Offset: 0x0004AE24
			public bool MediaContentTypeWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty.MediaContentType);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty.MediaContentType);
				}
			}

			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0004CC2E File Offset: 0x0004AE2E
			// (set) Token: 0x06001B2A RID: 6954 RVA: 0x0004CC38 File Offset: 0x0004AE38
			public bool MediaETagWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty.MediaETag);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty.MediaETag);
				}
			}

			// Token: 0x06001B2B RID: 6955 RVA: 0x0004CC42 File Offset: 0x0004AE42
			private void SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty jsonLightMetadataProperty)
			{
				this.alreadyWrittenMetadataProperties |= (int)jsonLightMetadataProperty;
			}

			// Token: 0x06001B2C RID: 6956 RVA: 0x0004CC52 File Offset: 0x0004AE52
			private bool IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty jsonLightMetadataProperty)
			{
				return (this.alreadyWrittenMetadataProperties & (int)jsonLightMetadataProperty) == (int)jsonLightMetadataProperty;
			}

			// Token: 0x04000DB5 RID: 3509
			private int alreadyWrittenMetadataProperties;

			// Token: 0x04000DB6 RID: 3510
			private bool isUndeclared;

			// Token: 0x02000376 RID: 886
			[Flags]
			private enum JsonLightEntryMetadataProperty
			{
				// Token: 0x04000DD6 RID: 3542
				EditLink = 1,
				// Token: 0x04000DD7 RID: 3543
				ReadLink = 2,
				// Token: 0x04000DD8 RID: 3544
				MediaEditLink = 4,
				// Token: 0x04000DD9 RID: 3545
				MediaReadLink = 8,
				// Token: 0x04000DDA RID: 3546
				MediaContentType = 16,
				// Token: 0x04000DDB RID: 3547
				MediaETag = 32
			}
		}

		// Token: 0x02000363 RID: 867
		private sealed class JsonLightNestedResourceInfoScope : ODataWriterCore.NestedResourceInfoScope
		{
			// Token: 0x06001B2D RID: 6957 RVA: 0x0004CC5F File Offset: 0x0004AE5F
			internal JsonLightNestedResourceInfoScope(ODataWriterCore.WriterState writerState, ODataNestedResourceInfo navLink, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(writerState, navLink, navigationSource, resourceType, skipWriting, selectedProperties, odataUri)
			{
			}

			// Token: 0x170005EA RID: 1514
			// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0004CC72 File Offset: 0x0004AE72
			// (set) Token: 0x06001B2F RID: 6959 RVA: 0x0004CC7A File Offset: 0x0004AE7A
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

			// Token: 0x170005EB RID: 1515
			// (get) Token: 0x06001B30 RID: 6960 RVA: 0x0004CC83 File Offset: 0x0004AE83
			// (set) Token: 0x06001B31 RID: 6961 RVA: 0x0004CC8B File Offset: 0x0004AE8B
			internal bool ResourceSetWritten
			{
				get
				{
					return this.resourceSetWritten;
				}
				set
				{
					this.resourceSetWritten = value;
				}
			}

			// Token: 0x06001B32 RID: 6962 RVA: 0x0004CC94 File Offset: 0x0004AE94
			internal override ODataWriterCore.NestedResourceInfoScope Clone(ODataWriterCore.WriterState newWriterState)
			{
				return new ODataJsonLightWriter.JsonLightNestedResourceInfoScope(newWriterState, (ODataNestedResourceInfo)base.Item, base.NavigationSource, base.ResourceType, base.SkipWriting, base.SelectedProperties, base.ODataUri)
				{
					EntityReferenceLinkWritten = this.entityReferenceLinkWritten,
					ResourceSetWritten = this.resourceSetWritten
				};
			}

			// Token: 0x04000DB7 RID: 3511
			private bool entityReferenceLinkWritten;

			// Token: 0x04000DB8 RID: 3512
			private bool resourceSetWritten;
		}
	}
}
