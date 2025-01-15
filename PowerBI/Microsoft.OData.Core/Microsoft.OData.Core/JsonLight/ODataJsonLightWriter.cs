using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000257 RID: 599
	internal sealed class ODataJsonLightWriter : ODataWriterCore
	{
		// Token: 0x06001ADA RID: 6874 RVA: 0x00051C54 File Offset: 0x0004FE54
		internal ODataJsonLightWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool writingResourceSet, bool writingParameter = false, bool writingDelta = false, IODataReaderWriterListener listener = null)
			: base(jsonLightOutputContext, navigationSource, resourceType, writingResourceSet, writingDelta, listener)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightResourceSerializer = new ODataJsonLightResourceSerializer(this.jsonLightOutputContext);
			this.jsonLightValueSerializer = new ODataJsonLightValueSerializer(this.jsonLightOutputContext, false);
			this.jsonLightPropertySerializer = new ODataJsonLightPropertySerializer(this.jsonLightOutputContext, false);
			this.writingParameter = writingParameter;
			this.jsonWriter = this.jsonLightOutputContext.JsonWriter;
			this.jsonStreamWriter = this.jsonWriter as IJsonStreamWriter;
			this.odataAnnotationWriter = new JsonLightODataAnnotationWriter(this.jsonWriter, this.jsonLightOutputContext.OmitODataPrefix, this.jsonLightOutputContext.MessageWriterSettings.Version);
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x00051D04 File Offset: 0x0004FF04
		private ODataJsonLightWriter.JsonLightResourceScope CurrentResourceScope
		{
			get
			{
				return base.CurrentScope as ODataJsonLightWriter.JsonLightResourceScope;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x00051D20 File Offset: 0x0004FF20
		private ODataJsonLightWriter.JsonLightDeletedResourceScope CurrentDeletedResourceScope
		{
			get
			{
				return base.CurrentScope as ODataJsonLightWriter.JsonLightDeletedResourceScope;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x00051D3C File Offset: 0x0004FF3C
		private ODataJsonLightWriter.JsonLightDeltaLinkScope CurrentDeltaLinkScope
		{
			get
			{
				return base.CurrentScope as ODataJsonLightWriter.JsonLightDeltaLinkScope;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x00051D58 File Offset: 0x0004FF58
		private ODataJsonLightWriter.JsonLightResourceSetScope CurrentResourceSetScope
		{
			get
			{
				return base.CurrentScope as ODataJsonLightWriter.JsonLightResourceSetScope;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x00051D74 File Offset: 0x0004FF74
		private ODataJsonLightWriter.JsonLightDeltaResourceSetScope CurrentDeltaResourceSetScope
		{
			get
			{
				return base.CurrentScope as ODataJsonLightWriter.JsonLightDeltaResourceSetScope;
			}
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00051D8E File Offset: 0x0004FF8E
		protected override void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00051D9B File Offset: 0x0004FF9B
		protected override void FlushSynchronously()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00051DA8 File Offset: 0x0004FFA8
		protected override Task FlushAsynchronously()
		{
			return this.jsonLightOutputContext.FlushAsync();
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x00051DB5 File Offset: 0x0004FFB5
		protected override void StartPayload()
		{
			this.jsonLightResourceSerializer.WritePayloadStart();
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00051DC2 File Offset: 0x0004FFC2
		protected override void EndPayload()
		{
			this.jsonLightResourceSerializer.WritePayloadEnd();
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00051DD0 File Offset: 0x0004FFD0
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

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00051E28 File Offset: 0x00050028
		protected override void PrepareDeletedResourceForWriteStart(ODataWriterCore.DeletedResourceScope resourceScope, ODataDeletedResource deletedResource, bool writingResponse, SelectedPropertiesNode selectedProperties)
		{
			if (this.jsonLightOutputContext.MessageWriterSettings.Version > ODataVersion.V4)
			{
				ODataResourceTypeContext orCreateTypeContext = resourceScope.GetOrCreateTypeContext(writingResponse);
				if (this.jsonLightOutputContext.MetadataLevel is JsonNoMetadataLevel)
				{
					this.InnerPrepareResourceForWriteStart(deletedResource, orCreateTypeContext, selectedProperties);
					return;
				}
				if (this.jsonLightOutputContext.Model.IsUserModel() || resourceScope.SerializationInfo != null)
				{
					this.InnerPrepareResourceForWriteStart(deletedResource, orCreateTypeContext, selectedProperties);
				}
			}
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00051EA8 File Offset: 0x000500A8
		protected override void StartProperty(ODataPropertyInfo property)
		{
			ODataWriterCore.ResourceBaseScope resourceBaseScope = base.ParentScope as ODataWriterCore.ResourceBaseScope;
			ODataResource odataResource = resourceBaseScope.Item as ODataResource;
			ODataProperty odataProperty = property as ODataProperty;
			if (odataProperty != null)
			{
				this.jsonLightPropertySerializer.WriteProperty(odataProperty, resourceBaseScope.ResourceType, false, base.DuplicatePropertyNameChecker, odataResource.MetadataBuilder);
				return;
			}
			this.jsonLightPropertySerializer.WritePropertyInfo(property, resourceBaseScope.ResourceType, false, base.DuplicatePropertyNameChecker, odataResource.MetadataBuilder);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0000239D File Offset: 0x0000059D
		protected override void EndProperty(ODataPropertyInfo property)
		{
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00051F18 File Offset: 0x00050118
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
			if (base.IsTopLevel && !(this.jsonLightOutputContext.MetadataLevel is JsonNoMetadataLevel))
			{
				ODataContextUrlInfo odataContextUrlInfo = this.jsonLightResourceSerializer.WriteResourceContextUri(currentResourceScope.GetOrCreateTypeContext(this.jsonLightOutputContext.WritingResponse), null);
				if (odataContextUrlInfo != null)
				{
					currentResourceScope.IsUndeclared = odataContextUrlInfo.IsUndeclared != null && odataContextUrlInfo.IsUndeclared.Value;
				}
			}
			else if (base.ParentScope.State == ODataWriterCore.WriterState.DeltaResourceSet && base.ScopeLevel == 3)
			{
				ODataWriterCore.DeltaResourceSetScope deltaResourceSetScope = base.ParentScope as ODataWriterCore.DeltaResourceSetScope;
				string text = ((deltaResourceSetScope.NavigationSource == null) ? null : deltaResourceSetScope.NavigationSource.Name);
				string text2 = ((resource.SerializationInfo != null) ? resource.SerializationInfo.NavigationSourceName : ((currentResourceScope.NavigationSource == null) ? null : currentResourceScope.NavigationSource.Name));
				if (string.IsNullOrEmpty(text2) || text2 != text)
				{
					this.jsonLightResourceSerializer.WriteDeltaContextUri(this.CurrentResourceScope.GetOrCreateTypeContext(true), ODataDeltaKind.Resource, deltaResourceSetScope.ContextUriInfo);
				}
			}
			this.jsonLightResourceSerializer.WriteResourceStartMetadataProperties(currentResourceScope);
			this.jsonLightResourceSerializer.WriteResourceMetadataProperties(currentResourceScope);
			this.jsonLightOutputContext.PropertyCacheHandler.SetCurrentResourceScopeLevel(base.ScopeLevel);
			this.jsonLightResourceSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(resource.InstanceAnnotations, currentResourceScope.InstanceAnnotationWriteTracker, false, null);
			if (resource.NonComputedProperties != null)
			{
				this.jsonLightResourceSerializer.WriteProperties(base.ResourceType, resource.NonComputedProperties, false, base.DuplicatePropertyNameChecker, resource.MetadataBuilder);
			}
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00052140 File Offset: 0x00050340
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

		// Token: 0x06001AEB RID: 6891 RVA: 0x0005219F File Offset: 0x0005039F
		protected override void EndDeletedResource(ODataDeletedResource deletedResource)
		{
			if (deletedResource == null)
			{
				return;
			}
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x000521B0 File Offset: 0x000503B0
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
					if (actions != null && actions.Any<ODataAction>())
					{
						this.jsonLightResourceSerializer.WriteOperations(actions.Cast<ODataOperation>(), true);
					}
					IEnumerable<ODataFunction> functions = resourceSet.Functions;
					if (functions != null && functions.Any<ODataFunction>())
					{
						this.jsonLightResourceSerializer.WriteOperations(functions.Cast<ODataOperation>(), false);
					}
					this.WriteResourceSetCount(resourceSet.Count, null);
					this.WriteResourceSetNextLink(resourceSet.NextPageLink, null);
					this.WriteResourceSetDeltaLink(resourceSet.DeltaLink);
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
					this.WriteResourceSetCount(resourceSet.Count, name);
					this.WriteResourceSetNextLink(resourceSet.NextPageLink, name);
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

		// Token: 0x06001AED RID: 6893 RVA: 0x000523F0 File Offset: 0x000505F0
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
					this.WriteResourceSetNextLink(resourceSet.NextPageLink, null);
					this.WriteResourceSetDeltaLink(resourceSet.DeltaLink);
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
					this.WriteResourceSetNextLink(resourceSet.NextPageLink, name);
				}
			}
			this.jsonLightOutputContext.PropertyCacheHandler.LeaveResourceSetScope();
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000524E4 File Offset: 0x000506E4
		protected override void StartDeltaResourceSet(ODataDeltaResourceSet deltaResourceSet)
		{
			if (base.ParentNestedResourceInfo == null)
			{
				this.jsonWriter.StartObjectScope();
				this.CurrentDeltaResourceSetScope.ContextUriInfo = this.jsonLightResourceSerializer.WriteDeltaContextUri(this.CurrentDeltaResourceSetScope.GetOrCreateTypeContext(this.jsonLightOutputContext.WritingResponse), ODataDeltaKind.ResourceSet, null);
				this.WriteResourceSetCount(deltaResourceSet.Count, null);
				this.WriteResourceSetNextLink(deltaResourceSet.NextPageLink, null);
				this.WriteResourceSetDeltaLink(deltaResourceSet.DeltaLink);
				this.jsonLightResourceSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(deltaResourceSet.InstanceAnnotations, this.CurrentDeltaResourceSetScope.InstanceAnnotationWriteTracker, false, null);
				this.jsonWriter.WriteValuePropertyName();
				this.jsonWriter.StartArrayScope();
				return;
			}
			string name = base.ParentNestedResourceInfo.Name;
			this.WriteResourceSetCount(deltaResourceSet.Count, name);
			this.WriteResourceSetNextLink(deltaResourceSet.NextPageLink, name);
			this.jsonWriter.WritePropertyAnnotationName(name, "delta");
			this.jsonWriter.StartArrayScope();
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x000525D8 File Offset: 0x000507D8
		protected override void EndDeltaResourceSet(ODataDeltaResourceSet deltaResourceSet)
		{
			if (base.ParentNestedResourceInfo == null)
			{
				this.jsonWriter.EndArrayScope();
				this.jsonLightResourceSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(deltaResourceSet.InstanceAnnotations, this.CurrentDeltaResourceSetScope.InstanceAnnotationWriteTracker, false, null);
				this.WriteResourceSetNextLink(deltaResourceSet.NextPageLink, null);
				this.WriteResourceSetDeltaLink(deltaResourceSet.DeltaLink);
				this.jsonWriter.EndObjectScope();
				return;
			}
			this.jsonWriter.EndArrayScope();
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0005264C File Offset: 0x0005084C
		protected override void StartDeletedResource(ODataDeletedResource resource)
		{
			ODataWriterCore.DeletedResourceScope currentDeletedResourceScope = this.CurrentDeletedResourceScope;
			ODataNestedResourceInfo parentNestedResourceInfo = base.ParentNestedResourceInfo;
			if (parentNestedResourceInfo != null)
			{
				if (base.Version == null || base.Version < ODataVersion.V401)
				{
					throw new ODataException(Strings.ODataWriterCore_NestedContentNotAllowedIn40DeletedEntry);
				}
				this.jsonWriter.WriteName(parentNestedResourceInfo.Name);
				this.jsonWriter.StartObjectScope();
				this.WriteDeletedEntryContents(resource);
				return;
			}
			else
			{
				ODataWriterCore.DeltaResourceSetScope deltaResourceSetScope = base.ParentScope as ODataWriterCore.DeltaResourceSetScope;
				this.jsonWriter.StartObjectScope();
				if (base.Version == null || base.Version < ODataVersion.V401)
				{
					this.jsonLightResourceSerializer.WriteDeltaContextUri(this.CurrentDeletedResourceScope.GetOrCreateTypeContext(this.jsonLightOutputContext.WritingResponse), ODataDeltaKind.DeletedEntry, deltaResourceSetScope.ContextUriInfo);
					this.WriteV4DeletedEntryContents(resource);
					return;
				}
				string text = ((deltaResourceSetScope.NavigationSource == null) ? null : deltaResourceSetScope.NavigationSource.Name);
				string text2 = ((resource.SerializationInfo != null) ? resource.SerializationInfo.NavigationSourceName : ((currentDeletedResourceScope.NavigationSource == null) ? null : currentDeletedResourceScope.NavigationSource.Name));
				if (string.IsNullOrEmpty(text2) || text2 != text)
				{
					this.jsonLightResourceSerializer.WriteDeltaContextUri(this.CurrentDeletedResourceScope.GetOrCreateTypeContext(this.jsonLightOutputContext.WritingResponse), ODataDeltaKind.DeletedEntry, deltaResourceSetScope.ContextUriInfo);
				}
				this.WriteDeletedEntryContents(resource);
				return;
			}
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x000527D0 File Offset: 0x000509D0
		protected override void StartDeltaLink(ODataDeltaLinkBase link)
		{
			this.jsonWriter.StartObjectScope();
			if (link is ODataDeltaLink)
			{
				this.WriteDeltaLinkContextUri(ODataDeltaKind.Link);
			}
			else
			{
				this.WriteDeltaLinkContextUri(ODataDeltaKind.DeletedLink);
			}
			this.WriteDeltaLinkSource(link);
			this.WriteDeltaLinkRelationship(link);
			this.WriteDeltaLinkTarget(link);
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00052820 File Offset: 0x00050A20
		protected override void WritePrimitiveValue(ODataPrimitiveValue primitiveValue)
		{
			ODataPropertyInfo odataPropertyInfo;
			if (base.ParentScope != null && (odataPropertyInfo = base.ParentScope.Item as ODataPropertyInfo) != null)
			{
				this.jsonWriter.WriteName(odataPropertyInfo.Name);
			}
			if (primitiveValue == null)
			{
				this.jsonLightValueSerializer.WriteNullValue();
				return;
			}
			this.jsonLightValueSerializer.WritePrimitiveValue(primitiveValue.Value, null);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0005287C File Offset: 0x00050A7C
		protected override Stream StartBinaryStream()
		{
			ODataPropertyInfo odataPropertyInfo;
			if (base.ParentScope != null && (odataPropertyInfo = base.ParentScope.Item as ODataPropertyInfo) != null)
			{
				this.jsonWriter.WriteName(odataPropertyInfo.Name);
				this.jsonWriter.Flush();
			}
			Stream stream;
			if (this.jsonStreamWriter == null)
			{
				this.jsonLightOutputContext.BinaryValueStream = new MemoryStream();
				stream = this.jsonLightOutputContext.BinaryValueStream;
			}
			else
			{
				stream = this.jsonStreamWriter.StartStreamValueScope();
			}
			return stream;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x000528F4 File Offset: 0x00050AF4
		protected sealed override void EndBinaryStream()
		{
			if (this.jsonStreamWriter == null)
			{
				this.jsonWriter.WriteValue(this.jsonLightOutputContext.BinaryValueStream.ToArray());
				this.jsonLightOutputContext.BinaryValueStream.Flush();
				this.jsonLightOutputContext.BinaryValueStream.Dispose();
				this.jsonLightOutputContext.BinaryValueStream = null;
				return;
			}
			this.jsonStreamWriter.EndStreamValueScope();
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0005295C File Offset: 0x00050B5C
		protected override TextWriter StartTextWriter()
		{
			ODataPropertyInfo odataPropertyInfo = null;
			if (base.ParentScope != null && (odataPropertyInfo = base.ParentScope.Item as ODataPropertyInfo) != null)
			{
				this.jsonWriter.WriteName(odataPropertyInfo.Name);
				this.jsonWriter.Flush();
			}
			TextWriter textWriter;
			if (this.jsonStreamWriter == null)
			{
				this.jsonLightOutputContext.StringWriter = new StringWriter(CultureInfo.InvariantCulture);
				textWriter = this.jsonLightOutputContext.StringWriter;
			}
			else
			{
				string text = "text/plain";
				ODataStreamPropertyInfo odataStreamPropertyInfo = odataPropertyInfo as ODataStreamPropertyInfo;
				if (odataStreamPropertyInfo != null && odataStreamPropertyInfo.ContentType != null)
				{
					text = odataStreamPropertyInfo.ContentType;
				}
				textWriter = this.jsonStreamWriter.StartTextWriterValueScope(text);
			}
			return textWriter;
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x000529FC File Offset: 0x00050BFC
		protected sealed override void EndTextWriter()
		{
			if (this.jsonStreamWriter == null)
			{
				this.jsonLightOutputContext.StringWriter.Flush();
				this.jsonWriter.WriteValue(this.jsonLightOutputContext.StringWriter.GetStringBuilder().ToString());
				this.jsonLightOutputContext.StringWriter.Dispose();
				this.jsonLightOutputContext.StringWriter = null;
				return;
			}
			this.jsonStreamWriter.EndTextWriterValueScope();
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00052A69 File Offset: 0x00050C69
		protected override void WriteDeferredNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo)
		{
			this.jsonLightResourceSerializer.WriteNavigationLinkMetadata(nestedResourceInfo, base.DuplicatePropertyNameChecker);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00052A80 File Offset: 0x00050C80
		protected override void StartNestedResourceInfoWithContent(ODataNestedResourceInfo nestedResourceInfo)
		{
			if (this.jsonLightOutputContext.WritingResponse)
			{
				IEdmContainedEntitySet edmContainedEntitySet = base.CurrentScope.NavigationSource as IEdmContainedEntitySet;
				if (edmContainedEntitySet != null && this.jsonLightOutputContext.MessageWriterSettings.LibraryCompatibility < ODataLibraryCompatibility.Version7 && this.jsonLightOutputContext.MessageWriterSettings.Version < ODataVersion.V401)
				{
					ODataContextUrlInfo odataContextUrlInfo = ODataContextUrlInfo.Create(base.CurrentScope.NavigationSource, base.CurrentScope.ResourceType.FullTypeName(), edmContainedEntitySet.NavigationProperty.Type.TypeKind() != EdmTypeKind.Collection, base.CurrentScope.ODataUri, this.jsonLightOutputContext.MessageWriterSettings.Version ?? ODataVersion.V4);
					this.jsonLightResourceSerializer.WriteNestedResourceInfoContextUrl(nestedResourceInfo, odataContextUrlInfo);
				}
				this.jsonLightResourceSerializer.WriteNavigationLinkMetadata(nestedResourceInfo, base.DuplicatePropertyNameChecker);
				return;
			}
			this.WriterValidator.ValidateNestedResourceInfoHasCardinality(nestedResourceInfo);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00052B8C File Offset: 0x00050D8C
		protected override void EndNestedResourceInfoWithContent(ODataNestedResourceInfo nestedResourceInfo)
		{
			ODataJsonLightWriter.JsonLightNestedResourceInfoScope jsonLightNestedResourceInfoScope = (ODataJsonLightWriter.JsonLightNestedResourceInfoScope)base.CurrentScope;
			if (!this.jsonLightOutputContext.WritingResponse)
			{
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

		// Token: 0x06001AFA RID: 6906 RVA: 0x00052BF0 File Offset: 0x00050DF0
		protected override void WriteEntityReferenceInNavigationLinkContent(ODataNestedResourceInfo parentNestedResourceInfo, ODataEntityReferenceLink entityReferenceLink)
		{
			ODataJsonLightWriter.JsonLightNestedResourceInfoScope jsonLightNestedResourceInfoScope = base.CurrentScope as ODataJsonLightWriter.JsonLightNestedResourceInfoScope;
			if (jsonLightNestedResourceInfoScope == null)
			{
				jsonLightNestedResourceInfoScope = base.ParentNestedResourceInfoScope as ODataJsonLightWriter.JsonLightNestedResourceInfoScope;
			}
			if (jsonLightNestedResourceInfoScope.ResourceSetWritten)
			{
				throw new ODataException(Strings.ODataJsonLightWriter_EntityReferenceLinkAfterResourceSetInRequest);
			}
			if (!jsonLightNestedResourceInfoScope.EntityReferenceLinkWritten)
			{
				if (!this.jsonLightOutputContext.WritingResponse)
				{
					if (base.Version == null || base.Version < ODataVersion.V401)
					{
						this.odataAnnotationWriter.WritePropertyAnnotationName(parentNestedResourceInfo.Name, "odata.bind");
					}
					else
					{
						this.jsonWriter.WriteName(parentNestedResourceInfo.Name);
					}
					if (parentNestedResourceInfo.IsCollection.Value)
					{
						this.jsonWriter.StartArrayScope();
					}
				}
				else if (!parentNestedResourceInfo.IsCollection.Value)
				{
					this.jsonWriter.WriteName(parentNestedResourceInfo.Name);
				}
				jsonLightNestedResourceInfoScope.EntityReferenceLinkWritten = true;
			}
			if (!this.jsonLightOutputContext.WritingResponse && (base.Version == null || base.Version < ODataVersion.V401))
			{
				this.jsonWriter.WriteValue(this.jsonLightResourceSerializer.UriToString(entityReferenceLink.Url));
				return;
			}
			this.WriteEntityReferenceLinkImplementation(entityReferenceLink);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00052D40 File Offset: 0x00050F40
		protected override ODataWriterCore.ResourceSetScope CreateResourceSetScope(ODataResourceSet resourceSet, IEdmNavigationSource navigationSource, IEdmType itemType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared)
		{
			return new ODataJsonLightWriter.JsonLightResourceSetScope(resourceSet, navigationSource, itemType, skipWriting, selectedProperties, odataUri, isUndeclared);
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x00052D52 File Offset: 0x00050F52
		protected override ODataWriterCore.DeltaResourceSetScope CreateDeltaResourceSetScope(ODataDeltaResourceSet deltaResourceSet, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared)
		{
			return new ODataJsonLightWriter.JsonLightDeltaResourceSetScope(deltaResourceSet, navigationSource, resourceType, selectedProperties, odataUri);
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x00052D60 File Offset: 0x00050F60
		protected override ODataWriterCore.DeletedResourceScope CreateDeletedResourceScope(ODataDeletedResource deltaResource, IEdmNavigationSource navigationSource, IEdmEntityType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared)
		{
			return new ODataJsonLightWriter.JsonLightDeletedResourceScope(deltaResource, base.GetResourceSerializationInfo(deltaResource), navigationSource, resourceType, skipWriting, this.jsonLightOutputContext.MessageWriterSettings, selectedProperties, odataUri, isUndeclared);
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x00052D8F File Offset: 0x00050F8F
		protected override ODataWriterCore.PropertyInfoScope CreatePropertyInfoScope(ODataPropertyInfo property, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightWriter.JsonLightPropertyScope(property, navigationSource, resourceType, selectedProperties, odataUri);
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00052D9D File Offset: 0x00050F9D
		protected override ODataWriterCore.DeltaLinkScope CreateDeltaLinkScope(ODataDeltaLinkBase link, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightWriter.JsonLightDeltaLinkScope((link is ODataDeltaLink) ? ODataWriterCore.WriterState.DeltaLink : ODataWriterCore.WriterState.DeltaDeletedLink, link, base.GetLinkSerializationInfo(link), navigationSource, entityType, selectedProperties, odataUri);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x00052DC0 File Offset: 0x00050FC0
		protected override ODataWriterCore.ResourceScope CreateResourceScope(ODataResource resource, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared)
		{
			return new ODataJsonLightWriter.JsonLightResourceScope(resource, base.GetResourceSerializationInfo(resource), navigationSource, resourceType, skipWriting, this.jsonLightOutputContext.MessageWriterSettings, selectedProperties, odataUri, isUndeclared);
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x00052DEF File Offset: 0x00050FEF
		protected override ODataWriterCore.NestedResourceInfoScope CreateNestedResourceInfoScope(ODataWriterCore.WriterState writerState, ODataNestedResourceInfo navLink, IEdmNavigationSource navigationSource, IEdmType itemType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightWriter.JsonLightNestedResourceInfoScope(writerState, navLink, navigationSource, itemType, skipWriting, selectedProperties, odataUri);
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00052E04 File Offset: 0x00051004
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink entityReferenceLink)
		{
			WriterValidationUtils.ValidateEntityReferenceLink(entityReferenceLink);
			this.jsonWriter.StartObjectScope();
			this.odataAnnotationWriter.WriteInstanceAnnotationName("odata.id");
			Uri uri = this.jsonLightOutputContext.MessageWriterSettings.MetadataDocumentUri.MakeRelativeUri(entityReferenceLink.Url);
			this.jsonWriter.WriteValue((uri == null) ? null : this.jsonLightResourceSerializer.UriToString(uri));
			this.jsonLightResourceSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(entityReferenceLink.InstanceAnnotations, null, false);
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x00052E94 File Offset: 0x00051094
		private void InnerPrepareResourceForWriteStart(ODataResourceBase resource, ODataResourceTypeContext typeContext, SelectedPropertiesNode selectedProperties)
		{
			ODataResourceSerializationInfo odataResourceSerializationInfo;
			IEdmStructuredType edmStructuredType;
			ODataUri odataUri;
			if (resource is ODataResource)
			{
				ODataWriterCore.ResourceScope resourceScope = (ODataWriterCore.ResourceScope)base.CurrentScope;
				odataResourceSerializationInfo = resourceScope.SerializationInfo;
				edmStructuredType = resourceScope.ResourceType;
				odataUri = resourceScope.ODataUri;
			}
			else
			{
				ODataWriterCore.DeletedResourceScope deletedResourceScope = (ODataWriterCore.DeletedResourceScope)base.CurrentScope;
				odataResourceSerializationInfo = deletedResourceScope.SerializationInfo;
				edmStructuredType = deletedResourceScope.ResourceType;
				odataUri = deletedResourceScope.ODataUri;
			}
			ODataResourceMetadataBuilder odataResourceMetadataBuilder = this.jsonLightOutputContext.MetadataLevel.CreateResourceMetadataBuilder(resource, typeContext, odataResourceSerializationInfo, edmStructuredType, selectedProperties, this.jsonLightOutputContext.WritingResponse, this.jsonLightOutputContext.ODataSimplifiedOptions.EnableWritingKeyAsSegment, odataUri, this.jsonLightOutputContext.MessageWriterSettings);
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

		// Token: 0x06001B04 RID: 6916 RVA: 0x00052FB0 File Offset: 0x000511B0
		private ODataResourceMetadataBuilder FindParentResourceMetadataBuilder()
		{
			ODataWriterCore.ResourceScope parentResourceScope = base.GetParentResourceScope();
			if (parentResourceScope != null)
			{
				ODataResourceBase odataResourceBase = parentResourceScope.Item as ODataResourceBase;
				if (odataResourceBase != null)
				{
					return odataResourceBase.MetadataBuilder;
				}
			}
			return null;
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00052FE0 File Offset: 0x000511E0
		private void WriteResourceSetCount(long? count, string propertyName)
		{
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

		// Token: 0x06001B06 RID: 6918 RVA: 0x00053030 File Offset: 0x00051230
		private void WriteResourceSetNextLink(Uri nextPageLink, string propertyName)
		{
			bool flag = ((base.State == ODataWriterCore.WriterState.ResourceSet) ? this.CurrentResourceSetScope.NextPageLinkWritten : this.CurrentDeltaResourceSetScope.NextPageLinkWritten);
			if (nextPageLink != null && !flag)
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
				if (base.State == ODataWriterCore.WriterState.ResourceSet)
				{
					this.CurrentResourceSetScope.NextPageLinkWritten = true;
					return;
				}
				this.CurrentDeltaResourceSetScope.NextPageLinkWritten = true;
			}
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x000530CC File Offset: 0x000512CC
		private void WriteResourceSetDeltaLink(Uri deltaLink)
		{
			if (deltaLink == null)
			{
				return;
			}
			if (!((base.State == ODataWriterCore.WriterState.ResourceSet) ? this.CurrentResourceSetScope.DeltaLinkWritten : this.CurrentDeltaResourceSetScope.DeltaLinkWritten))
			{
				this.odataAnnotationWriter.WriteInstanceAnnotationName("odata.deltaLink");
				this.jsonWriter.WriteValue(this.jsonLightResourceSerializer.UriToString(deltaLink));
				if (base.State == ODataWriterCore.WriterState.ResourceSet)
				{
					this.CurrentResourceSetScope.DeltaLinkWritten = true;
					return;
				}
				this.CurrentDeltaResourceSetScope.DeltaLinkWritten = true;
			}
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00053151 File Offset: 0x00051351
		private void WriteV4DeletedEntryContents(ODataDeletedResource resource)
		{
			this.WriteDeletedResourceId(resource);
			this.WriteDeltaResourceReason(resource);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x00053164 File Offset: 0x00051364
		private void WriteDeletedEntryContents(ODataDeletedResource resource)
		{
			this.odataAnnotationWriter.WriteInstanceAnnotationName("odata.removed");
			this.jsonWriter.StartObjectScope();
			this.WriteDeltaResourceReason(resource);
			this.jsonWriter.EndObjectScope();
			ODataJsonLightWriter.JsonLightDeletedResourceScope currentDeletedResourceScope = this.CurrentDeletedResourceScope;
			this.jsonLightResourceSerializer.WriteResourceStartMetadataProperties(currentDeletedResourceScope);
			this.jsonLightResourceSerializer.WriteResourceMetadataProperties(currentDeletedResourceScope);
			this.jsonLightOutputContext.PropertyCacheHandler.SetCurrentResourceScopeLevel(base.ScopeLevel);
			this.jsonLightResourceSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(resource.InstanceAnnotations, currentDeletedResourceScope.InstanceAnnotationWriteTracker, false, null);
			this.WriteDeltaResourceProperties(resource);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x000531F8 File Offset: 0x000513F8
		private void WriteDeletedResourceId(ODataDeletedResource resource)
		{
			if (base.Version == null || base.Version < ODataVersion.V401)
			{
				this.jsonWriter.WriteName("id");
				this.jsonWriter.WriteValue(resource.Id.OriginalString);
				return;
			}
			Uri uri;
			if (resource.MetadataBuilder.TryGetIdForSerialization(out uri))
			{
				this.jsonWriter.WriteInstanceAnnotationName("id");
				this.jsonWriter.WriteValue(uri.OriginalString);
			}
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0005328B File Offset: 0x0005148B
		private void WriteDeltaResourceProperties(ODataResourceBase resource)
		{
			if (resource.NonComputedProperties != null)
			{
				this.jsonLightResourceSerializer.WriteProperties(base.ResourceType, resource.NonComputedProperties, false, base.DuplicatePropertyNameChecker, resource.MetadataBuilder);
			}
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x000532BC File Offset: 0x000514BC
		private void WriteDeltaResourceReason(ODataDeletedResource resource)
		{
			if (resource.Reason == null)
			{
				return;
			}
			this.jsonWriter.WriteName("reason");
			DeltaDeletedEntryReason value = resource.Reason.Value;
			if (value == DeltaDeletedEntryReason.Deleted)
			{
				this.jsonWriter.WriteValue("deleted");
				return;
			}
			if (value != DeltaDeletedEntryReason.Changed)
			{
				return;
			}
			this.jsonWriter.WriteValue("changed");
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x00053322 File Offset: 0x00051522
		private void WriteDeltaLinkContextUri(ODataDeltaKind kind)
		{
			this.jsonLightResourceSerializer.WriteDeltaContextUri(this.CurrentDeltaLinkScope.GetOrCreateTypeContext(true), kind, null);
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0005333E File Offset: 0x0005153E
		private void WriteDeltaLinkSource(ODataDeltaLinkBase link)
		{
			this.jsonWriter.WriteName("source");
			this.jsonWriter.WriteValue(UriUtils.UriToString(link.Source));
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x00053366 File Offset: 0x00051566
		private void WriteDeltaLinkRelationship(ODataDeltaLinkBase link)
		{
			this.jsonWriter.WriteName("relationship");
			this.jsonWriter.WriteValue(link.Relationship);
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00053389 File Offset: 0x00051589
		private void WriteDeltaLinkTarget(ODataDeltaLinkBase link)
		{
			this.jsonWriter.WriteName("target");
			this.jsonWriter.WriteValue(UriUtils.UriToString(link.Target));
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x000533B1 File Offset: 0x000515B1
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "An instance field is used in a debug assert.")]
		private void ValidateNoCustomInstanceAnnotationsForExpandedResourceSet(ODataResourceSet resourceSet)
		{
			if (resourceSet.InstanceAnnotations.Count > 0)
			{
				throw new ODataException(Strings.ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedResourceSet);
			}
		}

		// Token: 0x04000B62 RID: 2914
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000B63 RID: 2915
		private readonly ODataJsonLightResourceSerializer jsonLightResourceSerializer;

		// Token: 0x04000B64 RID: 2916
		private readonly ODataJsonLightValueSerializer jsonLightValueSerializer;

		// Token: 0x04000B65 RID: 2917
		private readonly ODataJsonLightPropertySerializer jsonLightPropertySerializer;

		// Token: 0x04000B66 RID: 2918
		private readonly bool writingParameter;

		// Token: 0x04000B67 RID: 2919
		private readonly IJsonWriter jsonWriter;

		// Token: 0x04000B68 RID: 2920
		private readonly IJsonStreamWriter jsonStreamWriter;

		// Token: 0x04000B69 RID: 2921
		private readonly JsonLightODataAnnotationWriter odataAnnotationWriter;

		// Token: 0x02000443 RID: 1091
		private sealed class JsonLightResourceSetScope : ODataWriterCore.ResourceSetScope
		{
			// Token: 0x060021A1 RID: 8609 RVA: 0x0005E118 File Offset: 0x0005C318
			internal JsonLightResourceSetScope(ODataResourceSet resourceSet, IEdmNavigationSource navigationSource, IEdmType itemType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared)
				: base(resourceSet, navigationSource, itemType, skipWriting, selectedProperties, odataUri)
			{
				this.isUndeclared = isUndeclared;
			}

			// Token: 0x1700065B RID: 1627
			// (get) Token: 0x060021A2 RID: 8610 RVA: 0x0005E131 File Offset: 0x0005C331
			// (set) Token: 0x060021A3 RID: 8611 RVA: 0x0005E139 File Offset: 0x0005C339
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

			// Token: 0x1700065C RID: 1628
			// (get) Token: 0x060021A4 RID: 8612 RVA: 0x0005E142 File Offset: 0x0005C342
			// (set) Token: 0x060021A5 RID: 8613 RVA: 0x0005E14A File Offset: 0x0005C34A
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

			// Token: 0x1700065D RID: 1629
			// (get) Token: 0x060021A6 RID: 8614 RVA: 0x0005E153 File Offset: 0x0005C353
			internal bool IsUndeclared
			{
				get
				{
					return this.isUndeclared;
				}
			}

			// Token: 0x04001069 RID: 4201
			private bool nextLinkWritten;

			// Token: 0x0400106A RID: 4202
			private bool deltaLinkWritten;

			// Token: 0x0400106B RID: 4203
			private bool isUndeclared;
		}

		// Token: 0x02000444 RID: 1092
		private sealed class JsonLightDeletedResourceScope : ODataWriterCore.DeletedResourceScope, IODataJsonLightWriterResourceState
		{
			// Token: 0x060021A7 RID: 8615 RVA: 0x0005E15B File Offset: 0x0005C35B
			internal JsonLightDeletedResourceScope(ODataDeletedResource resource, ODataResourceSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType resourceType, bool skipWriting, ODataMessageWriterSettings writerSettings, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared)
				: base(resource, serializationInfo, navigationSource, resourceType, writerSettings, selectedProperties, odataUri)
			{
				this.isUndeclared = isUndeclared;
			}

			// Token: 0x1700065E RID: 1630
			// (get) Token: 0x060021A8 RID: 8616 RVA: 0x0005E176 File Offset: 0x0005C376
			ODataResourceBase IODataJsonLightWriterResourceState.Resource
			{
				get
				{
					return (ODataResourceBase)base.Item;
				}
			}

			// Token: 0x1700065F RID: 1631
			// (get) Token: 0x060021A9 RID: 8617 RVA: 0x0005E183 File Offset: 0x0005C383
			public bool IsUndeclared
			{
				get
				{
					return this.isUndeclared;
				}
			}

			// Token: 0x17000660 RID: 1632
			// (get) Token: 0x060021AA RID: 8618 RVA: 0x0005E18B File Offset: 0x0005C38B
			// (set) Token: 0x060021AB RID: 8619 RVA: 0x0005E194 File Offset: 0x0005C394
			public bool EditLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightDeletedResourceScope.JsonLightEntryMetadataProperty.EditLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightDeletedResourceScope.JsonLightEntryMetadataProperty.EditLink);
				}
			}

			// Token: 0x17000661 RID: 1633
			// (get) Token: 0x060021AC RID: 8620 RVA: 0x0005E19D File Offset: 0x0005C39D
			// (set) Token: 0x060021AD RID: 8621 RVA: 0x0005E1A6 File Offset: 0x0005C3A6
			public bool ReadLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightDeletedResourceScope.JsonLightEntryMetadataProperty.ReadLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightDeletedResourceScope.JsonLightEntryMetadataProperty.ReadLink);
				}
			}

			// Token: 0x17000662 RID: 1634
			// (get) Token: 0x060021AE RID: 8622 RVA: 0x0005E1AF File Offset: 0x0005C3AF
			// (set) Token: 0x060021AF RID: 8623 RVA: 0x0005E1B8 File Offset: 0x0005C3B8
			public bool MediaEditLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightDeletedResourceScope.JsonLightEntryMetadataProperty.MediaEditLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightDeletedResourceScope.JsonLightEntryMetadataProperty.MediaEditLink);
				}
			}

			// Token: 0x17000663 RID: 1635
			// (get) Token: 0x060021B0 RID: 8624 RVA: 0x0005E1C1 File Offset: 0x0005C3C1
			// (set) Token: 0x060021B1 RID: 8625 RVA: 0x0005E1CA File Offset: 0x0005C3CA
			public bool MediaReadLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightDeletedResourceScope.JsonLightEntryMetadataProperty.MediaReadLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightDeletedResourceScope.JsonLightEntryMetadataProperty.MediaReadLink);
				}
			}

			// Token: 0x17000664 RID: 1636
			// (get) Token: 0x060021B2 RID: 8626 RVA: 0x0005E1D3 File Offset: 0x0005C3D3
			// (set) Token: 0x060021B3 RID: 8627 RVA: 0x0005E1DD File Offset: 0x0005C3DD
			public bool MediaContentTypeWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightDeletedResourceScope.JsonLightEntryMetadataProperty.MediaContentType);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightDeletedResourceScope.JsonLightEntryMetadataProperty.MediaContentType);
				}
			}

			// Token: 0x17000665 RID: 1637
			// (get) Token: 0x060021B4 RID: 8628 RVA: 0x0005E1E7 File Offset: 0x0005C3E7
			// (set) Token: 0x060021B5 RID: 8629 RVA: 0x0005E1F1 File Offset: 0x0005C3F1
			public bool MediaETagWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightDeletedResourceScope.JsonLightEntryMetadataProperty.MediaETag);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightDeletedResourceScope.JsonLightEntryMetadataProperty.MediaETag);
				}
			}

			// Token: 0x060021B6 RID: 8630 RVA: 0x0005E1FB File Offset: 0x0005C3FB
			private void SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightDeletedResourceScope.JsonLightEntryMetadataProperty jsonLightMetadataProperty)
			{
				this.alreadyWrittenMetadataProperties |= (int)jsonLightMetadataProperty;
			}

			// Token: 0x060021B7 RID: 8631 RVA: 0x0005E20B File Offset: 0x0005C40B
			private bool IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightDeletedResourceScope.JsonLightEntryMetadataProperty jsonLightMetadataProperty)
			{
				return (this.alreadyWrittenMetadataProperties & (int)jsonLightMetadataProperty) == (int)jsonLightMetadataProperty;
			}

			// Token: 0x0400106C RID: 4204
			private int alreadyWrittenMetadataProperties;

			// Token: 0x0400106D RID: 4205
			private bool isUndeclared;

			// Token: 0x02000460 RID: 1120
			[Flags]
			private enum JsonLightEntryMetadataProperty
			{
				// Token: 0x0400109D RID: 4253
				EditLink = 1,
				// Token: 0x0400109E RID: 4254
				ReadLink = 2,
				// Token: 0x0400109F RID: 4255
				MediaEditLink = 4,
				// Token: 0x040010A0 RID: 4256
				MediaReadLink = 8,
				// Token: 0x040010A1 RID: 4257
				MediaContentType = 16,
				// Token: 0x040010A2 RID: 4258
				MediaETag = 32
			}
		}

		// Token: 0x02000445 RID: 1093
		private sealed class JsonLightPropertyScope : ODataWriterCore.PropertyInfoScope
		{
			// Token: 0x060021B8 RID: 8632 RVA: 0x0005E218 File Offset: 0x0005C418
			internal JsonLightPropertyScope(ODataPropertyInfo property, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(property, navigationSource, resourceType, selectedProperties, odataUri)
			{
			}
		}

		// Token: 0x02000446 RID: 1094
		private sealed class JsonLightDeltaLinkScope : ODataWriterCore.DeltaLinkScope
		{
			// Token: 0x060021B9 RID: 8633 RVA: 0x0005E227 File Offset: 0x0005C427
			public JsonLightDeltaLinkScope(ODataWriterCore.WriterState state, ODataItem link, ODataResourceSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(state, link, serializationInfo, navigationSource, entityType, selectedProperties, odataUri)
			{
			}
		}

		// Token: 0x02000447 RID: 1095
		private sealed class JsonLightDeltaResourceSetScope : ODataWriterCore.DeltaResourceSetScope
		{
			// Token: 0x060021BA RID: 8634 RVA: 0x0005E23A File Offset: 0x0005C43A
			public JsonLightDeltaResourceSetScope(ODataDeltaResourceSet resourceSet, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(resourceSet, navigationSource, resourceType, selectedProperties, odataUri)
			{
			}

			// Token: 0x17000666 RID: 1638
			// (get) Token: 0x060021BB RID: 8635 RVA: 0x0005E249 File Offset: 0x0005C449
			// (set) Token: 0x060021BC RID: 8636 RVA: 0x0005E251 File Offset: 0x0005C451
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

			// Token: 0x17000667 RID: 1639
			// (get) Token: 0x060021BD RID: 8637 RVA: 0x0005E25A File Offset: 0x0005C45A
			// (set) Token: 0x060021BE RID: 8638 RVA: 0x0005E262 File Offset: 0x0005C462
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

			// Token: 0x0400106E RID: 4206
			private bool nextLinkWritten;

			// Token: 0x0400106F RID: 4207
			private bool deltaLinkWritten;
		}

		// Token: 0x02000448 RID: 1096
		private sealed class JsonLightResourceScope : ODataWriterCore.ResourceScope, IODataJsonLightWriterResourceState
		{
			// Token: 0x060021BF RID: 8639 RVA: 0x0005E26C File Offset: 0x0005C46C
			internal JsonLightResourceScope(ODataResource resource, ODataResourceSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, bool skipWriting, ODataMessageWriterSettings writerSettings, SelectedPropertiesNode selectedProperties, ODataUri odataUri, bool isUndeclared)
				: base(resource, serializationInfo, navigationSource, resourceType, skipWriting, writerSettings, selectedProperties, odataUri)
			{
				this.isUndeclared = isUndeclared;
			}

			// Token: 0x17000668 RID: 1640
			// (get) Token: 0x060021C0 RID: 8640 RVA: 0x0005E176 File Offset: 0x0005C376
			ODataResourceBase IODataJsonLightWriterResourceState.Resource
			{
				get
				{
					return (ODataResourceBase)base.Item;
				}
			}

			// Token: 0x17000669 RID: 1641
			// (get) Token: 0x060021C1 RID: 8641 RVA: 0x0005E294 File Offset: 0x0005C494
			// (set) Token: 0x060021C2 RID: 8642 RVA: 0x0005E29C File Offset: 0x0005C49C
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

			// Token: 0x1700066A RID: 1642
			// (get) Token: 0x060021C3 RID: 8643 RVA: 0x0005E2A5 File Offset: 0x0005C4A5
			// (set) Token: 0x060021C4 RID: 8644 RVA: 0x0005E2AE File Offset: 0x0005C4AE
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

			// Token: 0x1700066B RID: 1643
			// (get) Token: 0x060021C5 RID: 8645 RVA: 0x0005E2B7 File Offset: 0x0005C4B7
			// (set) Token: 0x060021C6 RID: 8646 RVA: 0x0005E2C0 File Offset: 0x0005C4C0
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

			// Token: 0x1700066C RID: 1644
			// (get) Token: 0x060021C7 RID: 8647 RVA: 0x0005E2C9 File Offset: 0x0005C4C9
			// (set) Token: 0x060021C8 RID: 8648 RVA: 0x0005E2D2 File Offset: 0x0005C4D2
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

			// Token: 0x1700066D RID: 1645
			// (get) Token: 0x060021C9 RID: 8649 RVA: 0x0005E2DB File Offset: 0x0005C4DB
			// (set) Token: 0x060021CA RID: 8650 RVA: 0x0005E2E4 File Offset: 0x0005C4E4
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

			// Token: 0x1700066E RID: 1646
			// (get) Token: 0x060021CB RID: 8651 RVA: 0x0005E2ED File Offset: 0x0005C4ED
			// (set) Token: 0x060021CC RID: 8652 RVA: 0x0005E2F7 File Offset: 0x0005C4F7
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

			// Token: 0x1700066F RID: 1647
			// (get) Token: 0x060021CD RID: 8653 RVA: 0x0005E301 File Offset: 0x0005C501
			// (set) Token: 0x060021CE RID: 8654 RVA: 0x0005E30B File Offset: 0x0005C50B
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

			// Token: 0x060021CF RID: 8655 RVA: 0x0005E315 File Offset: 0x0005C515
			private void SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty jsonLightMetadataProperty)
			{
				this.alreadyWrittenMetadataProperties |= (int)jsonLightMetadataProperty;
			}

			// Token: 0x060021D0 RID: 8656 RVA: 0x0005E325 File Offset: 0x0005C525
			private bool IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightResourceScope.JsonLightEntryMetadataProperty jsonLightMetadataProperty)
			{
				return (this.alreadyWrittenMetadataProperties & (int)jsonLightMetadataProperty) == (int)jsonLightMetadataProperty;
			}

			// Token: 0x04001070 RID: 4208
			private int alreadyWrittenMetadataProperties;

			// Token: 0x04001071 RID: 4209
			private bool isUndeclared;

			// Token: 0x02000461 RID: 1121
			[Flags]
			private enum JsonLightEntryMetadataProperty
			{
				// Token: 0x040010A4 RID: 4260
				EditLink = 1,
				// Token: 0x040010A5 RID: 4261
				ReadLink = 2,
				// Token: 0x040010A6 RID: 4262
				MediaEditLink = 4,
				// Token: 0x040010A7 RID: 4263
				MediaReadLink = 8,
				// Token: 0x040010A8 RID: 4264
				MediaContentType = 16,
				// Token: 0x040010A9 RID: 4265
				MediaETag = 32
			}
		}

		// Token: 0x02000449 RID: 1097
		private sealed class JsonLightNestedResourceInfoScope : ODataWriterCore.NestedResourceInfoScope
		{
			// Token: 0x060021D1 RID: 8657 RVA: 0x0005E332 File Offset: 0x0005C532
			internal JsonLightNestedResourceInfoScope(ODataWriterCore.WriterState writerState, ODataNestedResourceInfo navLink, IEdmNavigationSource navigationSource, IEdmType itemType, bool skipWriting, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(writerState, navLink, navigationSource, itemType, skipWriting, selectedProperties, odataUri)
			{
			}

			// Token: 0x17000670 RID: 1648
			// (get) Token: 0x060021D2 RID: 8658 RVA: 0x0005E345 File Offset: 0x0005C545
			// (set) Token: 0x060021D3 RID: 8659 RVA: 0x0005E34D File Offset: 0x0005C54D
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

			// Token: 0x17000671 RID: 1649
			// (get) Token: 0x060021D4 RID: 8660 RVA: 0x0005E356 File Offset: 0x0005C556
			// (set) Token: 0x060021D5 RID: 8661 RVA: 0x0005E35E File Offset: 0x0005C55E
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

			// Token: 0x060021D6 RID: 8662 RVA: 0x0005E368 File Offset: 0x0005C568
			internal override ODataWriterCore.NestedResourceInfoScope Clone(ODataWriterCore.WriterState newWriterState)
			{
				return new ODataJsonLightWriter.JsonLightNestedResourceInfoScope(newWriterState, (ODataNestedResourceInfo)base.Item, base.NavigationSource, base.ItemType, base.SkipWriting, base.SelectedProperties, base.ODataUri)
				{
					EntityReferenceLinkWritten = this.entityReferenceLinkWritten,
					ResourceSetWritten = this.resourceSetWritten,
					DerivedTypeConstraints = base.DerivedTypeConstraints
				};
			}

			// Token: 0x04001072 RID: 4210
			private bool entityReferenceLinkWritten;

			// Token: 0x04001073 RID: 4211
			private bool resourceSetWritten;
		}
	}
}
