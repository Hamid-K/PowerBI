using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000206 RID: 518
	internal sealed class ODataJsonLightDeltaWriter : ODataDeltaWriter, IODataOutputInStreamErrorListener
	{
		// Token: 0x0600143E RID: 5182 RVA: 0x0003A5E8 File Offset: 0x000387E8
		public ODataJsonLightDeltaWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmNavigationSource navigationSource, IEdmEntityType entityType)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightResourceSerializer = new ODataJsonLightResourceSerializer(this.jsonLightOutputContext);
			this.NavigationSource = navigationSource;
			this.EntityType = entityType;
			if (navigationSource != null && entityType == null)
			{
				entityType = this.jsonLightOutputContext.EdmTypeResolver.GetElementType(navigationSource);
			}
			ODataUri odataUri = this.jsonLightOutputContext.MessageWriterSettings.ODataUri.Clone();
			this.scopes.Push(new ODataJsonLightDeltaWriter.Scope(ODataJsonLightDeltaWriter.WriterState.Start, null, navigationSource, entityType, this.jsonLightOutputContext.MessageWriterSettings.SelectedProperties, odataUri));
			this.jsonWriter = jsonLightOutputContext.JsonWriter;
			this.odataAnnotationWriter = new JsonLightODataAnnotationWriter(this.jsonWriter, this.jsonLightOutputContext.ODataSimplifiedOptions.EnableWritingODataAnnotationWithoutPrefix);
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0003A6AD File Offset: 0x000388AD
		// (set) Token: 0x06001440 RID: 5184 RVA: 0x0003A6B5 File Offset: 0x000388B5
		public IEdmNavigationSource NavigationSource { get; set; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0003A6BE File Offset: 0x000388BE
		// (set) Token: 0x06001442 RID: 5186 RVA: 0x0003A6C6 File Offset: 0x000388C6
		public IEdmEntityType EntityType { get; set; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0003A6CF File Offset: 0x000388CF
		private ODataJsonLightDeltaWriter.Scope CurrentScope
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0003A6DC File Offset: 0x000388DC
		private ODataJsonLightDeltaWriter.WriterState State
		{
			get
			{
				return this.CurrentScope.State;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0003A6EC File Offset: 0x000388EC
		private ODataJsonLightDeltaWriter.JsonLightDeltaResourceSetScope CurrentDeltaResourceSetScope
		{
			get
			{
				return this.CurrentScope as ODataJsonLightDeltaWriter.JsonLightDeltaResourceSetScope;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0003A708 File Offset: 0x00038908
		private ODataJsonLightDeltaWriter.JsonLightDeltaResourceScope CurrentDeltaResourceScope
		{
			get
			{
				return this.CurrentScope as ODataJsonLightDeltaWriter.JsonLightDeltaResourceScope;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0003A724 File Offset: 0x00038924
		private ODataJsonLightDeltaWriter.JsonLightDeltaLinkScope CurrentDeltaLinkScope
		{
			get
			{
				return this.CurrentScope as ODataJsonLightDeltaWriter.JsonLightDeltaLinkScope;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0003A740 File Offset: 0x00038940
		private ODataJsonLightDeltaWriter.JsonLightNestedResourceInfoScope CurrentExpandedNavigationPropertyScope
		{
			get
			{
				return this.CurrentScope as ODataJsonLightDeltaWriter.JsonLightNestedResourceInfoScope;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0003A75A File Offset: 0x0003895A
		private bool IsTopLevel
		{
			get
			{
				return this.scopes.Count == 2;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0003A76A File Offset: 0x0003896A
		private IEdmStructuredType DeltaResourceType
		{
			get
			{
				return this.CurrentScope.ResourceType;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0003A778 File Offset: 0x00038978
		private IDuplicatePropertyNameChecker DuplicatePropertyNameChecker
		{
			get
			{
				ODataJsonLightDeltaWriter.WriterState state = this.State;
				if (state == ODataJsonLightDeltaWriter.WriterState.DeltaResource || state == ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry)
				{
					return this.CurrentDeltaResourceScope.DuplicatePropertyNameChecker;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_PropertyAndAnnotationCollector));
			}
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0003A7B0 File Offset: 0x000389B0
		public override void WriteStart(ODataDeltaResourceSet deltaResourceSet)
		{
			this.VerifyCanWriteStartDeltaResourceSet(true, deltaResourceSet);
			this.WriteStartDeltaResourceSetImplementation(deltaResourceSet);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0003A7C1 File Offset: 0x000389C1
		public override void WriteEnd()
		{
			this.VerifyCanWriteEnd(true);
			this.WriteEndImplementation();
			if (this.CurrentScope.State == ODataJsonLightDeltaWriter.WriterState.Completed)
			{
				this.Flush();
			}
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0003A7E4 File Offset: 0x000389E4
		public override void WriteStart(ODataNestedResourceInfo nestedResourceInfo)
		{
			this.VerifyCanWriteNestedResourceInfo(true, nestedResourceInfo);
			this.WriteStartNestedResourceInfoImplementation(nestedResourceInfo);
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0003A7F5 File Offset: 0x000389F5
		public override void WriteStart(ODataResourceSet expandedResourceSet)
		{
			this.VerifyCanWriteExpandedResourceSet(true, expandedResourceSet);
			this.WriteStartExpandedResourceSetImplementation(expandedResourceSet);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0003A806 File Offset: 0x00038A06
		public override void WriteStart(ODataResource deltaResource)
		{
			this.VerifyCanWriteResource(true, deltaResource);
			this.WriteStartDeltaResourceImplementation(deltaResource);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0003A817 File Offset: 0x00038A17
		public override void WriteDeltaDeletedEntry(ODataDeltaDeletedEntry deltaDeletedEntry)
		{
			this.VerifyCanWriteResource(true, deltaDeletedEntry);
			this.WriteStartDeltaResourceImplementation(deltaDeletedEntry);
			this.WriteEnd();
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0003A82E File Offset: 0x00038A2E
		public override void WriteDeltaLink(ODataDeltaLink deltaLink)
		{
			this.VerifyCanWriteLink(true, deltaLink);
			this.WriteStartDeltaLinkImplementation(deltaLink);
			this.WriteEnd();
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0003A845 File Offset: 0x00038A45
		public override void WriteDeltaDeletedLink(ODataDeltaDeletedLink deltaDeletedLink)
		{
			this.VerifyCanWriteLink(true, deltaDeletedLink);
			this.WriteStartDeltaLinkImplementation(deltaDeletedLink);
			this.WriteEnd();
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0003A85C File Offset: 0x00038A5C
		public override void Flush()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0003A86C File Offset: 0x00038A6C
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			this.VerifyNotDisposed();
			if (this.State == ODataJsonLightDeltaWriter.WriterState.Completed)
			{
				throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromCompleted(this.State.ToString(), ODataJsonLightDeltaWriter.WriterState.Error.ToString()));
			}
			this.StartPayloadInStartState();
			this.EnterScope(ODataJsonLightDeltaWriter.WriterState.Error, this.CurrentScope.Item);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0003A8CE File Offset: 0x00038ACE
		private void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0003A8DB File Offset: 0x00038ADB
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.jsonLightOutputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataWriterCore_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0003A8F8 File Offset: 0x00038AF8
		private void VerifyCanWriteStartDeltaResourceSet(bool synchronousCall, ODataDeltaResourceSet deltaResourceSet)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaResourceSet>(deltaResourceSet, "deltaResourceSet");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			this.StartPayloadInStartState();
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0003A919 File Offset: 0x00038B19
		private void VerifyCanWriteNestedResourceInfo(bool synchronousCall, ODataNestedResourceInfo nestedResourceInfo)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			ExceptionUtils.CheckArgumentNotNull<ODataNestedResourceInfo>(nestedResourceInfo, "nestedResourceInfo");
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0003A934 File Offset: 0x00038B34
		private void VerifyCanWriteExpandedResourceSet(bool synchronousCall, ODataResourceSet expandedResourceSet)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			ExceptionUtils.CheckArgumentNotNull<ODataResourceSet>(expandedResourceSet, "expandedResourceSet");
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0003A94F File Offset: 0x00038B4F
		private void VerifyCanWriteResource(bool synchronousCall, ODataItem resource)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			ExceptionUtils.CheckArgumentNotNull<ODataItem>(resource, "resource");
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0003A96A File Offset: 0x00038B6A
		private void VerifyCanWriteLink(bool synchronousCall, ODataDeltaLink deltaLink)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaLink>(deltaLink, "delta link");
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0003A985 File Offset: 0x00038B85
		private void VerifyCanWriteLink(bool synchronousCall, ODataDeltaDeletedLink deltaDeletedLink)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			ExceptionUtils.CheckArgumentNotNull<ODataDeltaDeletedLink>(deltaDeletedLink, "delta deleted link");
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0003A9A0 File Offset: 0x00038BA0
		private void VerifyCanWriteEnd(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0003A9B0 File Offset: 0x00038BB0
		private void ValidateTransition(ODataJsonLightDeltaWriter.WriterState newState)
		{
			if (!ODataJsonLightDeltaWriter.IsErrorState(this.State) && ODataJsonLightDeltaWriter.IsErrorState(newState))
			{
				return;
			}
			if (this.State != ODataJsonLightDeltaWriter.WriterState.DeltaResource && newState == ODataJsonLightDeltaWriter.WriterState.NestedResource)
			{
				throw new ODataException(Strings.ODataJsonLightDeltaWriter_InvalidTransitionToNestedResource(this.State.ToString(), newState.ToString()));
			}
			switch (this.State)
			{
			case ODataJsonLightDeltaWriter.WriterState.Start:
				if (newState != ODataJsonLightDeltaWriter.WriterState.DeltaResourceSet)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromStart(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataJsonLightDeltaWriter.WriterState.DeltaResource:
			case ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry:
			case ODataJsonLightDeltaWriter.WriterState.DeltaLink:
			case ODataJsonLightDeltaWriter.WriterState.DeltaDeletedLink:
				if (this.CurrentScope.Item == null)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromNullResource(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataJsonLightDeltaWriter.WriterState.DeltaResourceSet:
				if (newState != ODataJsonLightDeltaWriter.WriterState.DeltaResource && newState != ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry && newState != ODataJsonLightDeltaWriter.WriterState.DeltaLink && newState != ODataJsonLightDeltaWriter.WriterState.DeltaDeletedLink)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromResourceSet(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataJsonLightDeltaWriter.WriterState.NestedResource:
				throw new ODataException(Strings.ODataJsonLightDeltaWriter_InvalidTransitionFromNestedResource(this.State.ToString(), newState.ToString()));
			case ODataJsonLightDeltaWriter.WriterState.Completed:
				throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromCompleted(this.State.ToString(), newState.ToString()));
			case ODataJsonLightDeltaWriter.WriterState.Error:
				if (newState != ODataJsonLightDeltaWriter.WriterState.Error)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromError(this.State.ToString(), newState.ToString()));
				}
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_ValidateTransition_UnreachableCodePath));
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0003AB92 File Offset: 0x00038D92
		private void ValidateEntryMediaResource(ODataResource resource, IEdmEntityType entityType)
		{
			if (this.jsonLightOutputContext.MetadataLevel is JsonNoMetadataLevel)
			{
				return;
			}
			this.jsonLightOutputContext.WriterValidator.ValidateMetadataResource(resource, entityType);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0003ABBC File Offset: 0x00038DBC
		private void WriteStartDeltaResourceSetImplementation(ODataDeltaResourceSet resourceSet)
		{
			this.EnterScope(ODataJsonLightDeltaWriter.WriterState.DeltaResourceSet, resourceSet);
			this.InterceptException(delegate
			{
				this.StartDeltaResourceSet(resourceSet);
			});
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0003ABFC File Offset: 0x00038DFC
		private void WriteStartNestedResourceInfoImplementation(ODataNestedResourceInfo nestedResourceInfo)
		{
			if (!ODataJsonLightDeltaWriter.IsNestedResourceState(this.State))
			{
				this.EnterScope(ODataJsonLightDeltaWriter.WriterState.NestedResource, nestedResourceInfo);
			}
			this.InterceptException(delegate
			{
				this.CurrentExpandedNavigationPropertyScope.JsonLightExpandedNavigationPropertyWriter.WriteStart(nestedResourceInfo);
			});
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0003AC4C File Offset: 0x00038E4C
		private void WriteStartExpandedResourceSetImplementation(ODataResourceSet expandedResourceSet)
		{
			if (!ODataJsonLightDeltaWriter.IsNestedResourceState(this.State))
			{
				throw new ODataException(Strings.ODataJsonLightDeltaWriter_WriteStartExpandedResourceSetCalledInInvalidState(this.State.ToString()));
			}
			this.InterceptException(delegate
			{
				this.CurrentExpandedNavigationPropertyScope.JsonLightExpandedNavigationPropertyWriter.WriteStart(expandedResourceSet);
			});
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0003ACAC File Offset: 0x00038EAC
		private void WriteStartDeltaResourceImplementation(ODataResource resource)
		{
			if (ODataJsonLightDeltaWriter.IsNestedResourceState(this.State))
			{
				this.InterceptException(delegate
				{
					this.CurrentExpandedNavigationPropertyScope.JsonLightExpandedNavigationPropertyWriter.WriteStart(resource);
				});
				return;
			}
			this.StartPayloadInStartState();
			this.EnterScope(ODataJsonLightDeltaWriter.WriterState.DeltaResource, resource);
			this.InterceptException(delegate
			{
				this.PreStartDeltaResource(resource);
				this.StartDeltaResource(resource);
			});
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0003AD14 File Offset: 0x00038F14
		private void WriteStartDeltaResourceImplementation(ODataDeltaDeletedEntry resource)
		{
			this.StartPayloadInStartState();
			this.EnterScope(ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry, resource);
			this.InterceptException(delegate
			{
				this.StartDeltaDeletedEntry(resource);
			});
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0003AD5C File Offset: 0x00038F5C
		private void ResolveEntityType(ODataResource resource)
		{
			ODataJsonLightDeltaWriter.DeltaResourceScope currentDeltaResourceScope = this.CurrentDeltaResourceScope;
			IEdmEntityType edmEntityType = null;
			if (resource.SerializationInfo != null && this.jsonLightOutputContext.Model != null && this.jsonLightOutputContext.Model != EdmCoreModel.Instance && resource.SerializationInfo.NavigationSourceKind == EdmNavigationSourceKind.EntitySet)
			{
				IEdmEntitySet edmEntitySet = this.jsonLightOutputContext.Model.FindDeclaredEntitySet(resource.SerializationInfo.NavigationSourceName);
				if (edmEntitySet != null)
				{
					edmEntityType = edmEntitySet.EntityType();
				}
			}
			IEdmEntityType edmEntityType2 = null;
			if (!string.IsNullOrEmpty(resource.TypeName) && this.jsonLightOutputContext.Model != null && this.jsonLightOutputContext.Model != EdmCoreModel.Instance)
			{
				edmEntityType2 = TypeNameOracle.ResolveAndValidateTypeName(this.jsonLightOutputContext.Model, resource.TypeName, EdmTypeKind.Entity, new bool?(true), this.jsonLightOutputContext.WriterValidator) as IEdmEntityType;
			}
			IEdmEntityType edmEntityType3 = this.CurrentDeltaResourceScope.ResourceType as IEdmEntityType;
			currentDeltaResourceScope.ResourceTypeFromMetadata = edmEntityType3;
			if (edmEntityType2 != null)
			{
				currentDeltaResourceScope.ResourceType = edmEntityType2;
				return;
			}
			if (edmEntityType != null)
			{
				currentDeltaResourceScope.ResourceType = edmEntityType;
				return;
			}
			if (edmEntityType3 != null)
			{
				currentDeltaResourceScope.ResourceType = edmEntityType3;
				return;
			}
			currentDeltaResourceScope.ResourceType = null;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0003AE6C File Offset: 0x0003906C
		private void PreStartDeltaResource(ODataResource resource)
		{
			this.ResolveEntityType(resource);
			this.PrepareResourceForWriteStart(resource);
			this.ValidateEntryMediaResource(resource, this.CurrentDeltaResourceScope.ResourceType as IEdmEntityType);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0003AE94 File Offset: 0x00039094
		private void PrepareResourceForWriteStart(ODataResource resource)
		{
			ODataJsonLightDeltaWriter.DeltaResourceScope currentDeltaResourceScope = this.CurrentDeltaResourceScope;
			ODataResourceMetadataBuilder odataResourceMetadataBuilder = this.jsonLightOutputContext.MetadataLevel.CreateResourceMetadataBuilder(resource, currentDeltaResourceScope.GetOrCreateTypeContext(true), currentDeltaResourceScope.SerializationInfo, currentDeltaResourceScope.ResourceType, currentDeltaResourceScope.SelectedProperties, true, this.jsonLightOutputContext.ODataSimplifiedOptions.EnableWritingKeyAsSegment, this.jsonLightOutputContext.MessageWriterSettings.ODataUri);
			this.jsonLightOutputContext.MetadataLevel.InjectMetadataBuilder(resource, odataResourceMetadataBuilder);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0003AF06 File Offset: 0x00039106
		private void WriteStartDeltaLinkImplementation(ODataDeltaLink deltaLink)
		{
			this.EnterScope(ODataJsonLightDeltaWriter.WriterState.DeltaLink, deltaLink);
			this.StartDeltaLink(deltaLink);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0003AF17 File Offset: 0x00039117
		private void WriteStartDeltaLinkImplementation(ODataDeltaDeletedLink deltaDeletedLink)
		{
			this.EnterScope(ODataJsonLightDeltaWriter.WriterState.DeltaDeletedLink, deltaDeletedLink);
			this.StartDeltaLink(deltaDeletedLink);
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0003AF28 File Offset: 0x00039128
		private void WriteEndImplementation()
		{
			if (this.State == ODataJsonLightDeltaWriter.WriterState.NestedResource)
			{
				if (this.CurrentExpandedNavigationPropertyScope.JsonLightExpandedNavigationPropertyWriter.WriteEnd())
				{
					this.LeaveScope();
				}
				return;
			}
			this.InterceptException(delegate
			{
				ODataJsonLightDeltaWriter.Scope currentScope = this.CurrentScope;
				switch (currentScope.State)
				{
				case ODataJsonLightDeltaWriter.WriterState.Start:
				case ODataJsonLightDeltaWriter.WriterState.Completed:
				case ODataJsonLightDeltaWriter.WriterState.Error:
					throw new ODataException(Strings.ODataWriterCore_WriteEndCalledInInvalidState(currentScope.State.ToString()));
				case ODataJsonLightDeltaWriter.WriterState.DeltaResource:
					this.EndDeltaResource();
					goto IL_00A3;
				case ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry:
					this.EndDeltaResource();
					goto IL_00A3;
				case ODataJsonLightDeltaWriter.WriterState.DeltaResourceSet:
				{
					ODataDeltaResourceSet odataDeltaResourceSet = (ODataDeltaResourceSet)currentScope.Item;
					WriterValidationUtils.ValidateResourceSetAtEnd(ODataJsonLightDeltaWriter.DeltaConverter.ToODataResourceSet(odataDeltaResourceSet), false);
					this.EndDeltaResourceSet(odataDeltaResourceSet);
					goto IL_00A3;
				}
				case ODataJsonLightDeltaWriter.WriterState.DeltaLink:
				case ODataJsonLightDeltaWriter.WriterState.DeltaDeletedLink:
					this.EndDeltaLink();
					goto IL_00A3;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_WriteEnd_UnreachableCodePath));
				IL_00A3:
				this.LeaveScope();
			});
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0003AF60 File Offset: 0x00039160
		private void WriteDeltaResourceSetCount(ODataDeltaResourceSet resourceSet)
		{
			long? count = resourceSet.Count;
			if (count != null)
			{
				this.odataAnnotationWriter.WriteInstanceAnnotationName("odata.count");
				this.jsonWriter.WriteValue(count.Value);
			}
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0003AFA0 File Offset: 0x000391A0
		private void WriteDeltaResourceSetNextLink(ODataDeltaResourceSet resourceSet)
		{
			Uri nextPageLink = resourceSet.NextPageLink;
			if (nextPageLink != null && !this.CurrentDeltaResourceSetScope.NextPageLinkWritten)
			{
				this.odataAnnotationWriter.WriteInstanceAnnotationName("odata.nextLink");
				this.jsonWriter.WriteValue(this.jsonLightResourceSerializer.UriToString(nextPageLink));
				this.CurrentDeltaResourceSetScope.NextPageLinkWritten = true;
			}
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0003B000 File Offset: 0x00039200
		private void WriteDeltaResourceSetDeltaLink(ODataDeltaResourceSet resourceSet)
		{
			Uri deltaLink = resourceSet.DeltaLink;
			if (deltaLink != null && !this.CurrentDeltaResourceSetScope.DeltaLinkWritten)
			{
				this.odataAnnotationWriter.WriteInstanceAnnotationName("odata.deltaLink");
				this.jsonWriter.WriteValue(this.jsonLightResourceSerializer.UriToString(deltaLink));
				this.CurrentDeltaResourceSetScope.DeltaLinkWritten = true;
			}
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0003B05D File Offset: 0x0003925D
		private void WriteDeltaResourceSetContextUri()
		{
			this.CurrentDeltaResourceSetScope.ContextUriInfo = this.jsonLightResourceSerializer.WriteDeltaContextUri(this.CurrentDeltaResourceSetScope.GetOrCreateTypeContext(true), ODataDeltaKind.ResourceSet, null);
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0003B083 File Offset: 0x00039283
		private void WriteDeltaResourceSetInstanceAnnotations(ODataDeltaResourceSet resourceSet)
		{
			this.jsonLightResourceSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(resourceSet.InstanceAnnotations, this.CurrentDeltaResourceSetScope.InstanceAnnotationWriteTracker, false, null);
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0003B0A8 File Offset: 0x000392A8
		private void WriteDeltaResourceSetValueStart()
		{
			this.jsonWriter.WriteValuePropertyName();
			this.jsonWriter.StartArrayScope();
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0003B0C0 File Offset: 0x000392C0
		private void WriteDeltaResourceId(ODataDeltaDeletedEntry resource)
		{
			this.jsonWriter.WriteName("id");
			this.jsonWriter.WriteValue(resource.Id);
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0003B0E4 File Offset: 0x000392E4
		private void WriteDeltaResourceReason(ODataDeltaDeletedEntry resource)
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

		// Token: 0x06001474 RID: 5236 RVA: 0x0003B14A File Offset: 0x0003934A
		private void WriteDeltaResourceContextUri(ODataDeltaKind kind)
		{
			this.jsonLightResourceSerializer.WriteDeltaContextUri(this.CurrentDeltaResourceScope.GetOrCreateTypeContext(true), kind, this.GetParentDeltaResourceSetScope().ContextUriInfo);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0003B170 File Offset: 0x00039370
		private void WriteDeltaResourceStartMetadata()
		{
			this.jsonLightResourceSerializer.WriteResourceStartMetadataProperties(this.CurrentDeltaResourceScope);
			this.jsonLightResourceSerializer.WriteResourceMetadataProperties(this.CurrentDeltaResourceScope);
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0003B194 File Offset: 0x00039394
		private void WriteDeltaResourceEndMetadata()
		{
			this.jsonLightResourceSerializer.WriteResourceEndMetadataProperties(this.CurrentDeltaResourceScope, this.CurrentDeltaResourceScope.DuplicatePropertyNameChecker);
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0003B1B2 File Offset: 0x000393B2
		private void WriteDeltaResourceInstanceAnnotations(ODataResource resource)
		{
			this.jsonLightResourceSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(resource.InstanceAnnotations, this.CurrentDeltaResourceScope.InstanceAnnotationWriteTracker, false, null);
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0003B1D7 File Offset: 0x000393D7
		private void WriteDeltaResourceProperties(ODataResource resource)
		{
			this.jsonLightResourceSerializer.WriteProperties(this.DeltaResourceType, resource.Properties, false, this.DuplicatePropertyNameChecker);
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0003B1F7 File Offset: 0x000393F7
		private void WriteDeltaLinkContextUri(ODataDeltaKind kind)
		{
			this.jsonLightResourceSerializer.WriteDeltaContextUri(this.CurrentDeltaLinkScope.GetOrCreateTypeContext(true), kind, null);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0003B213 File Offset: 0x00039413
		private void WriteDeltaLinkSource(ODataDeltaLinkBase link)
		{
			this.jsonWriter.WriteName("source");
			this.jsonWriter.WriteValue(UriUtils.UriToString(link.Source));
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0003B23B File Offset: 0x0003943B
		private void WriteDeltaLinkRelationship(ODataDeltaLinkBase link)
		{
			this.jsonWriter.WriteName("relationship");
			this.jsonWriter.WriteValue(link.Relationship);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0003B25E File Offset: 0x0003945E
		private void WriteDeltaLinkTarget(ODataDeltaLinkBase link)
		{
			this.jsonWriter.WriteName("target");
			this.jsonWriter.WriteValue(UriUtils.UriToString(link.Target));
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0003B286 File Offset: 0x00039486
		private void StartDeltaResourceSet(ODataDeltaResourceSet resourceSet)
		{
			this.jsonWriter.StartObjectScope();
			this.WriteDeltaResourceSetContextUri();
			this.WriteDeltaResourceSetCount(resourceSet);
			this.WriteDeltaResourceSetNextLink(resourceSet);
			this.WriteDeltaResourceSetDeltaLink(resourceSet);
			this.WriteDeltaResourceSetInstanceAnnotations(resourceSet);
			this.WriteDeltaResourceSetValueStart();
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0003B2BB File Offset: 0x000394BB
		private void StartDeltaResource(ODataResource resource)
		{
			this.jsonWriter.StartObjectScope();
			this.WriteDeltaResourceContextUri(ODataDeltaKind.Resource);
			this.WriteDeltaResourceStartMetadata();
			this.WriteDeltaResourceInstanceAnnotations(resource);
			this.WriteDeltaResourceProperties(resource);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0003B2E3 File Offset: 0x000394E3
		private void StartDeltaDeletedEntry(ODataDeltaDeletedEntry resource)
		{
			this.jsonWriter.StartObjectScope();
			this.WriteDeltaResourceContextUri(ODataDeltaKind.DeletedEntry);
			this.WriteDeltaResourceId(resource);
			this.WriteDeltaResourceReason(resource);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0003B305 File Offset: 0x00039505
		private void StartDeltaLink(ODataDeltaLinkBase link)
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
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0003B340 File Offset: 0x00039540
		private void EndDeltaResourceSet(ODataDeltaResourceSet resourceSet)
		{
			this.jsonWriter.EndArrayScope();
			this.jsonLightResourceSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(resourceSet.InstanceAnnotations, this.CurrentDeltaResourceSetScope.InstanceAnnotationWriteTracker, false, null);
			this.WriteDeltaResourceSetNextLink(resourceSet);
			this.WriteDeltaResourceSetDeltaLink(resourceSet);
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0003B394 File Offset: 0x00039594
		private void EndDeltaResource()
		{
			if (this.CurrentScope.State == ODataJsonLightDeltaWriter.WriterState.DeltaResource)
			{
				this.WriteDeltaResourceEndMetadata();
			}
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0003B3B5 File Offset: 0x000395B5
		private void EndDeltaLink()
		{
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0003B3C4 File Offset: 0x000395C4
		private void EnterScope(ODataJsonLightDeltaWriter.WriterState newState, ODataItem item)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			ODataJsonLightDeltaWriter.Scope currentScope = this.CurrentScope;
			IEdmNavigationSource edmNavigationSource = null;
			IEdmEntityType edmEntityType = null;
			SelectedPropertiesNode selectedProperties = currentScope.SelectedProperties;
			ODataUri odataUri = currentScope.ODataUri.Clone();
			if (newState == ODataJsonLightDeltaWriter.WriterState.DeltaResource || newState == ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry || newState == ODataJsonLightDeltaWriter.WriterState.DeltaLink || newState == ODataJsonLightDeltaWriter.WriterState.DeltaDeletedLink || newState == ODataJsonLightDeltaWriter.WriterState.DeltaResourceSet || newState == ODataJsonLightDeltaWriter.WriterState.NestedResource)
			{
				edmNavigationSource = currentScope.NavigationSource;
				edmEntityType = currentScope.ResourceType as IEdmEntityType;
			}
			this.PushScope(newState, item, edmNavigationSource, edmEntityType, selectedProperties, odataUri);
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0003B474 File Offset: 0x00039674
		private void LeaveScope()
		{
			this.scopes.Pop();
			if (this.scopes.Count == 1)
			{
				ODataJsonLightDeltaWriter.Scope scope = this.scopes.Pop();
				this.PushScope(ODataJsonLightDeltaWriter.WriterState.Completed, null, scope.NavigationSource, scope.ResourceType, scope.SelectedProperties, scope.ODataUri);
				this.InterceptException(new Action(this.EndPayload));
			}
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0003B4DC File Offset: 0x000396DC
		private void PushScope(ODataJsonLightDeltaWriter.WriterState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			IEdmEntityType edmEntityType = resourceType as IEdmEntityType;
			ODataJsonLightDeltaWriter.Scope scope;
			switch (state)
			{
			case ODataJsonLightDeltaWriter.WriterState.Start:
			case ODataJsonLightDeltaWriter.WriterState.Completed:
			case ODataJsonLightDeltaWriter.WriterState.Error:
				scope = new ODataJsonLightDeltaWriter.Scope(state, item, navigationSource, resourceType, selectedProperties, odataUri);
				break;
			case ODataJsonLightDeltaWriter.WriterState.DeltaResource:
				scope = this.CreateDeltaResourceScope(ODataJsonLightDeltaWriter.WriterState.DeltaResource, item, navigationSource, edmEntityType, selectedProperties, odataUri);
				break;
			case ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry:
				scope = this.CreateDeltaResourceScope(ODataJsonLightDeltaWriter.WriterState.DeltaDeletedEntry, item, navigationSource, edmEntityType, selectedProperties, odataUri);
				break;
			case ODataJsonLightDeltaWriter.WriterState.DeltaResourceSet:
				scope = ODataJsonLightDeltaWriter.CreateDeltaResourceSetScope(item, navigationSource, edmEntityType, selectedProperties, odataUri);
				break;
			case ODataJsonLightDeltaWriter.WriterState.DeltaLink:
				scope = this.CreateDeltaLinkScope(ODataJsonLightDeltaWriter.WriterState.DeltaLink, item, navigationSource, edmEntityType, selectedProperties, odataUri);
				break;
			case ODataJsonLightDeltaWriter.WriterState.DeltaDeletedLink:
				scope = this.CreateDeltaLinkScope(ODataJsonLightDeltaWriter.WriterState.DeltaDeletedLink, item, navigationSource, edmEntityType, selectedProperties, odataUri);
				break;
			case ODataJsonLightDeltaWriter.WriterState.NestedResource:
				scope = this.CreateNestedResourceScope(item, navigationSource, resourceType, selectedProperties, odataUri);
				break;
			default:
			{
				string text = Strings.General_InternalError(InternalErrorCodes.ODataWriterCore_Scope_Create_UnreachableCodePath);
				throw new ODataException(text);
			}
			}
			this.scopes.Push(scope);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0003B5B4 File Offset: 0x000397B4
		private ODataJsonLightDeltaWriter.DeltaResourceSetScope GetParentDeltaResourceSetScope()
		{
			ODataJsonLightDeltaWriter.ScopeStack scopeStack = new ODataJsonLightDeltaWriter.ScopeStack();
			ODataJsonLightDeltaWriter.Scope scope = null;
			if (this.scopes.Count > 0)
			{
				scopeStack.Push(this.scopes.Pop());
			}
			while (this.scopes.Count > 0)
			{
				ODataJsonLightDeltaWriter.Scope scope2 = this.scopes.Pop();
				scopeStack.Push(scope2);
				if (scope2 is ODataJsonLightDeltaWriter.DeltaResourceSetScope)
				{
					scope = scope2;
					IL_006B:
					while (scopeStack.Count > 0)
					{
						ODataJsonLightDeltaWriter.Scope scope3 = scopeStack.Pop();
						this.scopes.Push(scope3);
					}
					return scope as ODataJsonLightDeltaWriter.DeltaResourceSetScope;
				}
			}
			goto IL_006B;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0003B63B File Offset: 0x0003983B
		private static ODataJsonLightDeltaWriter.DeltaResourceSetScope CreateDeltaResourceSetScope(ODataItem resourceSet, IEdmNavigationSource navigationSource, IEdmEntityType resourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightDeltaWriter.JsonLightDeltaResourceSetScope(resourceSet, navigationSource, resourceType, selectedProperties, odataUri);
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0003B648 File Offset: 0x00039848
		private ODataJsonLightDeltaWriter.DeltaResourceScope CreateDeltaResourceScope(ODataJsonLightDeltaWriter.WriterState state, ODataItem resource, IEdmNavigationSource navigationSource, IEdmEntityType resourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightDeltaWriter.JsonLightDeltaResourceScope(state, resource, this.GetResourceSerializationInfo(resource), navigationSource, resourceType, this.jsonLightOutputContext.MessageWriterSettings, selectedProperties, odataUri);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0003B66A File Offset: 0x0003986A
		private ODataJsonLightDeltaWriter.DeltaLinkScope CreateDeltaLinkScope(ODataJsonLightDeltaWriter.WriterState state, ODataItem link, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightDeltaWriter.JsonLightDeltaLinkScope(state, link, this.GetLinkSerializationInfo(link), navigationSource, entityType, selectedProperties, odataUri);
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0003B681 File Offset: 0x00039881
		private ODataJsonLightDeltaWriter.NestedResourceInfoScope CreateNestedResourceScope(ODataItem nestedResourceInfo, IEdmNavigationSource navigationSource, IEdmStructuredType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
		{
			return new ODataJsonLightDeltaWriter.JsonLightNestedResourceInfoScope(nestedResourceInfo, navigationSource, entityType, selectedProperties, odataUri, this.CurrentDeltaResourceScope.Resource, this.jsonLightOutputContext);
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0003B6A0 File Offset: 0x000398A0
		private void StartPayloadInStartState()
		{
			if (this.State == ODataJsonLightDeltaWriter.WriterState.Start)
			{
				this.InterceptException(new Action(this.StartPayload));
			}
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0003B6BC File Offset: 0x000398BC
		private void StartPayload()
		{
			this.jsonLightResourceSerializer.WritePayloadStart();
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0003B6C9 File Offset: 0x000398C9
		private void EndPayload()
		{
			this.jsonLightResourceSerializer.WritePayloadEnd();
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0003B6D8 File Offset: 0x000398D8
		private void InterceptException(Action action)
		{
			try
			{
				action.Invoke();
			}
			catch
			{
				if (!ODataJsonLightDeltaWriter.IsErrorState(this.State))
				{
					this.EnterScope(ODataJsonLightDeltaWriter.WriterState.Error, this.CurrentScope.Item);
				}
				throw;
			}
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0003B720 File Offset: 0x00039920
		private ODataDeltaSerializationInfo GetParentResourceSetSerializationInfo()
		{
			ODataJsonLightDeltaWriter.DeltaResourceSetScope deltaResourceSetScope = this.CurrentScope as ODataJsonLightDeltaWriter.DeltaResourceSetScope;
			if (deltaResourceSetScope != null)
			{
				ODataDeltaResourceSet odataDeltaResourceSet = (ODataDeltaResourceSet)deltaResourceSetScope.Item;
				return ODataJsonLightDeltaWriter.DeltaConverter.ToDeltaSerializationInfo(odataDeltaResourceSet.SerializationInfo);
			}
			return null;
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0003B758 File Offset: 0x00039958
		private ODataResourceSerializationInfo GetResourceSerializationInfo(ODataItem item)
		{
			ODataResourceSerializationInfo odataResourceSerializationInfo = null;
			ODataResource odataResource = item as ODataResource;
			if (odataResource != null)
			{
				odataResourceSerializationInfo = odataResource.SerializationInfo;
			}
			ODataDeltaDeletedEntry odataDeltaDeletedEntry = item as ODataDeltaDeletedEntry;
			if (odataDeltaDeletedEntry != null)
			{
				odataResourceSerializationInfo = ODataJsonLightDeltaWriter.DeltaConverter.ToResourceSerializationInfo(odataDeltaDeletedEntry.SerializationInfo);
			}
			if (odataResourceSerializationInfo == null)
			{
				odataResourceSerializationInfo = ODataJsonLightDeltaWriter.DeltaConverter.ToResourceSerializationInfo(this.GetParentResourceSetSerializationInfo());
			}
			return odataResourceSerializationInfo;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0003B7A0 File Offset: 0x000399A0
		private ODataDeltaSerializationInfo GetLinkSerializationInfo(ODataItem item)
		{
			ODataDeltaSerializationInfo odataDeltaSerializationInfo = null;
			ODataDeltaLink odataDeltaLink = item as ODataDeltaLink;
			if (odataDeltaLink != null)
			{
				odataDeltaSerializationInfo = odataDeltaLink.SerializationInfo;
			}
			ODataDeltaDeletedLink odataDeltaDeletedLink = item as ODataDeltaDeletedLink;
			if (odataDeltaDeletedLink != null)
			{
				odataDeltaSerializationInfo = odataDeltaDeletedLink.SerializationInfo;
			}
			return odataDeltaSerializationInfo ?? this.GetParentResourceSetSerializationInfo();
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00007BAD File Offset: 0x00005DAD
		private static bool IsErrorState(ODataJsonLightDeltaWriter.WriterState state)
		{
			return state == ODataJsonLightDeltaWriter.WriterState.Error;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0003B7DC File Offset: 0x000399DC
		private static bool IsNestedResourceState(ODataJsonLightDeltaWriter.WriterState state)
		{
			return state == ODataJsonLightDeltaWriter.WriterState.NestedResource;
		}

		// Token: 0x04000A11 RID: 2577
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000A12 RID: 2578
		private readonly ODataJsonLightResourceSerializer jsonLightResourceSerializer;

		// Token: 0x04000A13 RID: 2579
		private readonly ODataJsonLightDeltaWriter.ScopeStack scopes = new ODataJsonLightDeltaWriter.ScopeStack();

		// Token: 0x04000A14 RID: 2580
		private readonly JsonLightODataAnnotationWriter odataAnnotationWriter;

		// Token: 0x04000A15 RID: 2581
		private readonly IJsonWriter jsonWriter;

		// Token: 0x02000324 RID: 804
		private enum WriterState
		{
			// Token: 0x04000D09 RID: 3337
			Start,
			// Token: 0x04000D0A RID: 3338
			DeltaResource,
			// Token: 0x04000D0B RID: 3339
			DeltaDeletedEntry,
			// Token: 0x04000D0C RID: 3340
			DeltaResourceSet,
			// Token: 0x04000D0D RID: 3341
			DeltaLink,
			// Token: 0x04000D0E RID: 3342
			DeltaDeletedLink,
			// Token: 0x04000D0F RID: 3343
			NestedResource,
			// Token: 0x04000D10 RID: 3344
			Completed,
			// Token: 0x04000D11 RID: 3345
			Error
		}

		// Token: 0x02000325 RID: 805
		[Flags]
		private enum JsonLightEntryMetadataProperty
		{
			// Token: 0x04000D13 RID: 3347
			EditLink = 1,
			// Token: 0x04000D14 RID: 3348
			ReadLink = 2,
			// Token: 0x04000D15 RID: 3349
			MediaEditLink = 4,
			// Token: 0x04000D16 RID: 3350
			MediaReadLink = 8,
			// Token: 0x04000D17 RID: 3351
			MediaContentType = 16,
			// Token: 0x04000D18 RID: 3352
			MediaETag = 32
		}

		// Token: 0x02000326 RID: 806
		private class Scope
		{
			// Token: 0x06001A5E RID: 6750 RVA: 0x0004B3BA File Offset: 0x000495BA
			public Scope(ODataJsonLightDeltaWriter.WriterState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
			{
				this.state = state;
				this.item = item;
				this.ResourceType = resourceType;
				this.NavigationSource = navigationSource;
				this.selectedProperties = selectedProperties;
				this.odataUri = odataUri;
			}

			// Token: 0x170005B8 RID: 1464
			// (get) Token: 0x06001A5F RID: 6751 RVA: 0x0004B3EF File Offset: 0x000495EF
			// (set) Token: 0x06001A60 RID: 6752 RVA: 0x0004B3F7 File Offset: 0x000495F7
			public IEdmStructuredType ResourceType { get; set; }

			// Token: 0x170005B9 RID: 1465
			// (get) Token: 0x06001A61 RID: 6753 RVA: 0x0004B400 File Offset: 0x00049600
			public ODataJsonLightDeltaWriter.WriterState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x170005BA RID: 1466
			// (get) Token: 0x06001A62 RID: 6754 RVA: 0x0004B408 File Offset: 0x00049608
			public ODataItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x170005BB RID: 1467
			// (get) Token: 0x06001A63 RID: 6755 RVA: 0x0004B410 File Offset: 0x00049610
			// (set) Token: 0x06001A64 RID: 6756 RVA: 0x0004B418 File Offset: 0x00049618
			public IEdmNavigationSource NavigationSource { get; private set; }

			// Token: 0x170005BC RID: 1468
			// (get) Token: 0x06001A65 RID: 6757 RVA: 0x0004B421 File Offset: 0x00049621
			public SelectedPropertiesNode SelectedProperties
			{
				get
				{
					return this.selectedProperties;
				}
			}

			// Token: 0x170005BD RID: 1469
			// (get) Token: 0x06001A66 RID: 6758 RVA: 0x0004B429 File Offset: 0x00049629
			public ODataUri ODataUri
			{
				get
				{
					return this.odataUri;
				}
			}

			// Token: 0x04000D19 RID: 3353
			private readonly ODataJsonLightDeltaWriter.WriterState state;

			// Token: 0x04000D1A RID: 3354
			private readonly ODataItem item;

			// Token: 0x04000D1B RID: 3355
			private readonly SelectedPropertiesNode selectedProperties;

			// Token: 0x04000D1C RID: 3356
			private readonly ODataUri odataUri;
		}

		// Token: 0x02000327 RID: 807
		private abstract class DeltaResourceScope : ODataJsonLightDeltaWriter.Scope
		{
			// Token: 0x06001A67 RID: 6759 RVA: 0x0004B431 File Offset: 0x00049631
			protected DeltaResourceScope(ODataJsonLightDeltaWriter.WriterState state, ODataItem resource, ODataResourceSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType entityType, ODataMessageWriterSettings writerSettings, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(state, resource, navigationSource, entityType, selectedProperties, odataUri)
			{
				this.duplicatePropertyNameChecker = writerSettings.Validator.CreateDuplicatePropertyNameChecker();
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x170005BE RID: 1470
			// (get) Token: 0x06001A68 RID: 6760 RVA: 0x0004B45C File Offset: 0x0004965C
			// (set) Token: 0x06001A69 RID: 6761 RVA: 0x0004B464 File Offset: 0x00049664
			public IEdmStructuredType ResourceTypeFromMetadata { get; set; }

			// Token: 0x170005BF RID: 1471
			// (get) Token: 0x06001A6A RID: 6762 RVA: 0x0004B46D File Offset: 0x0004966D
			public ODataResourceSerializationInfo SerializationInfo
			{
				get
				{
					return this.serializationInfo;
				}
			}

			// Token: 0x170005C0 RID: 1472
			// (get) Token: 0x06001A6B RID: 6763 RVA: 0x0004B475 File Offset: 0x00049675
			public IDuplicatePropertyNameChecker DuplicatePropertyNameChecker
			{
				get
				{
					return this.duplicatePropertyNameChecker;
				}
			}

			// Token: 0x170005C1 RID: 1473
			// (get) Token: 0x06001A6C RID: 6764 RVA: 0x0004B47D File Offset: 0x0004967D
			public InstanceAnnotationWriteTracker InstanceAnnotationWriteTracker
			{
				get
				{
					if (this.instanceAnnotationWriteTracker == null)
					{
						this.instanceAnnotationWriteTracker = new InstanceAnnotationWriteTracker();
					}
					return this.instanceAnnotationWriteTracker;
				}
			}

			// Token: 0x06001A6D RID: 6765 RVA: 0x0004B498 File Offset: 0x00049698
			public ODataResourceTypeContext GetOrCreateTypeContext(bool writingResponse = true)
			{
				if (this.typeContext == null)
				{
					this.typeContext = ODataResourceTypeContext.Create(this.serializationInfo, base.NavigationSource, EdmTypeWriterResolver.Instance.GetElementType(base.NavigationSource), this.ResourceTypeFromMetadata ?? base.ResourceType, writingResponse);
				}
				return this.typeContext;
			}

			// Token: 0x04000D1F RID: 3359
			private readonly IDuplicatePropertyNameChecker duplicatePropertyNameChecker;

			// Token: 0x04000D20 RID: 3360
			private readonly ODataResourceSerializationInfo serializationInfo;

			// Token: 0x04000D21 RID: 3361
			private ODataResourceTypeContext typeContext;

			// Token: 0x04000D22 RID: 3362
			private InstanceAnnotationWriteTracker instanceAnnotationWriteTracker;
		}

		// Token: 0x02000328 RID: 808
		private abstract class DeltaResourceSetScope : ODataJsonLightDeltaWriter.Scope
		{
			// Token: 0x06001A6E RID: 6766 RVA: 0x0004B4EC File Offset: 0x000496EC
			protected DeltaResourceSetScope(ODataItem item, IEdmNavigationSource navigationSource, IEdmEntityType resourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataJsonLightDeltaWriter.WriterState.DeltaResourceSet, item, navigationSource, resourceType, selectedProperties, odataUri)
			{
				ODataDeltaResourceSet odataDeltaResourceSet = item as ODataDeltaResourceSet;
				this.serializationInfo = odataDeltaResourceSet.SerializationInfo;
			}

			// Token: 0x170005C2 RID: 1474
			// (get) Token: 0x06001A6F RID: 6767 RVA: 0x0004B51A File Offset: 0x0004971A
			public InstanceAnnotationWriteTracker InstanceAnnotationWriteTracker
			{
				get
				{
					if (this.instanceAnnotationWriteTracker == null)
					{
						this.instanceAnnotationWriteTracker = new InstanceAnnotationWriteTracker();
					}
					return this.instanceAnnotationWriteTracker;
				}
			}

			// Token: 0x170005C3 RID: 1475
			// (get) Token: 0x06001A70 RID: 6768 RVA: 0x0004B535 File Offset: 0x00049735
			// (set) Token: 0x06001A71 RID: 6769 RVA: 0x0004B53D File Offset: 0x0004973D
			public ODataContextUrlInfo ContextUriInfo { get; set; }

			// Token: 0x06001A72 RID: 6770 RVA: 0x0004B548 File Offset: 0x00049748
			public ODataResourceTypeContext GetOrCreateTypeContext(bool writingResponse = true)
			{
				if (this.typeContext == null)
				{
					this.typeContext = ODataResourceTypeContext.Create(ODataJsonLightDeltaWriter.DeltaConverter.ToResourceSerializationInfo(this.serializationInfo), base.NavigationSource, EdmTypeWriterResolver.Instance.GetElementType(base.NavigationSource), base.ResourceType, writingResponse);
				}
				return this.typeContext;
			}

			// Token: 0x04000D24 RID: 3364
			private readonly ODataDeltaResourceSetSerializationInfo serializationInfo;

			// Token: 0x04000D25 RID: 3365
			private InstanceAnnotationWriteTracker instanceAnnotationWriteTracker;

			// Token: 0x04000D26 RID: 3366
			private ODataResourceTypeContext typeContext;
		}

		// Token: 0x02000329 RID: 809
		private abstract class DeltaLinkScope : ODataJsonLightDeltaWriter.Scope
		{
			// Token: 0x06001A73 RID: 6771 RVA: 0x0004B596 File Offset: 0x00049796
			protected DeltaLinkScope(ODataJsonLightDeltaWriter.WriterState state, ODataItem link, ODataDeltaSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(state, link, navigationSource, entityType, selectedProperties, odataUri)
			{
				this.serializationInfo = ODataJsonLightDeltaWriter.DeltaConverter.ToResourceSerializationInfo(serializationInfo);
			}

			// Token: 0x06001A74 RID: 6772 RVA: 0x0004B5C9 File Offset: 0x000497C9
			public ODataResourceTypeContext GetOrCreateTypeContext(bool writingResponse = true)
			{
				if (this.typeContext == null)
				{
					this.typeContext = ODataResourceTypeContext.Create(this.serializationInfo, base.NavigationSource, EdmTypeWriterResolver.Instance.GetElementType(base.NavigationSource), this.fakeEntityType, writingResponse);
				}
				return this.typeContext;
			}

			// Token: 0x04000D28 RID: 3368
			private readonly ODataResourceSerializationInfo serializationInfo;

			// Token: 0x04000D29 RID: 3369
			private readonly EdmEntityType fakeEntityType = new EdmEntityType("MyNS", "Fake");

			// Token: 0x04000D2A RID: 3370
			private ODataResourceTypeContext typeContext;
		}

		// Token: 0x0200032A RID: 810
		private abstract class NestedResourceInfoScope : ODataJsonLightDeltaWriter.Scope
		{
			// Token: 0x06001A75 RID: 6773 RVA: 0x0004B607 File Offset: 0x00049807
			protected NestedResourceInfoScope(ODataItem nestedResourceInfo, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataJsonLightDeltaWriter.WriterState.NestedResource, nestedResourceInfo, navigationSource, resourceType, selectedProperties, odataUri)
			{
			}
		}

		// Token: 0x0200032B RID: 811
		private sealed class JsonLightDeltaResourceScope : ODataJsonLightDeltaWriter.DeltaResourceScope, IODataJsonLightWriterResourceState
		{
			// Token: 0x06001A76 RID: 6774 RVA: 0x0004B618 File Offset: 0x00049818
			public JsonLightDeltaResourceScope(ODataJsonLightDeltaWriter.WriterState state, ODataItem resource, ODataResourceSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType entityType, ODataMessageWriterSettings writerSettings, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(state, resource, serializationInfo, navigationSource, entityType, writerSettings, selectedProperties, odataUri)
			{
			}

			// Token: 0x170005C4 RID: 1476
			// (get) Token: 0x06001A77 RID: 6775 RVA: 0x0004B638 File Offset: 0x00049838
			public ODataResource Resource
			{
				get
				{
					return (ODataResource)base.Item;
				}
			}

			// Token: 0x170005C5 RID: 1477
			// (get) Token: 0x06001A78 RID: 6776 RVA: 0x00002500 File Offset: 0x00000700
			public bool IsUndeclared
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170005C6 RID: 1478
			// (get) Token: 0x06001A79 RID: 6777 RVA: 0x0004B645 File Offset: 0x00049845
			// (set) Token: 0x06001A7A RID: 6778 RVA: 0x0004B64E File Offset: 0x0004984E
			public bool EditLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.EditLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.EditLink);
				}
			}

			// Token: 0x170005C7 RID: 1479
			// (get) Token: 0x06001A7B RID: 6779 RVA: 0x0004B657 File Offset: 0x00049857
			// (set) Token: 0x06001A7C RID: 6780 RVA: 0x0004B660 File Offset: 0x00049860
			public bool ReadLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.ReadLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.ReadLink);
				}
			}

			// Token: 0x170005C8 RID: 1480
			// (get) Token: 0x06001A7D RID: 6781 RVA: 0x0004B669 File Offset: 0x00049869
			// (set) Token: 0x06001A7E RID: 6782 RVA: 0x0004B672 File Offset: 0x00049872
			public bool MediaEditLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaEditLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaEditLink);
				}
			}

			// Token: 0x170005C9 RID: 1481
			// (get) Token: 0x06001A7F RID: 6783 RVA: 0x0004B67B File Offset: 0x0004987B
			// (set) Token: 0x06001A80 RID: 6784 RVA: 0x0004B684 File Offset: 0x00049884
			public bool MediaReadLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaReadLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaReadLink);
				}
			}

			// Token: 0x170005CA RID: 1482
			// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0004B68D File Offset: 0x0004988D
			// (set) Token: 0x06001A82 RID: 6786 RVA: 0x0004B697 File Offset: 0x00049897
			public bool MediaContentTypeWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaContentType);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaContentType);
				}
			}

			// Token: 0x170005CB RID: 1483
			// (get) Token: 0x06001A83 RID: 6787 RVA: 0x0004B6A1 File Offset: 0x000498A1
			// (set) Token: 0x06001A84 RID: 6788 RVA: 0x0004B6AB File Offset: 0x000498AB
			public bool MediaETagWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaETag);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty.MediaETag);
				}
			}

			// Token: 0x06001A85 RID: 6789 RVA: 0x0004B6B5 File Offset: 0x000498B5
			private void SetWrittenMetadataProperty(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty jsonLightMetadataProperty)
			{
				this.alreadyWrittenMetadataProperties |= (int)jsonLightMetadataProperty;
			}

			// Token: 0x06001A86 RID: 6790 RVA: 0x0004B6C5 File Offset: 0x000498C5
			private bool IsMetadataPropertyWritten(ODataJsonLightDeltaWriter.JsonLightEntryMetadataProperty jsonLightMetadataProperty)
			{
				return (this.alreadyWrittenMetadataProperties & (int)jsonLightMetadataProperty) == (int)jsonLightMetadataProperty;
			}

			// Token: 0x04000D2B RID: 3371
			private int alreadyWrittenMetadataProperties;
		}

		// Token: 0x0200032C RID: 812
		private sealed class JsonLightNestedResourceInfoScope : ODataJsonLightDeltaWriter.NestedResourceInfoScope
		{
			// Token: 0x06001A87 RID: 6791 RVA: 0x0004B6D2 File Offset: 0x000498D2
			public JsonLightNestedResourceInfoScope(ODataItem nestedResourceInfo, IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri, ODataResource parentDeltaResource, ODataJsonLightOutputContext jsonLightOutputContext)
				: base(nestedResourceInfo, navigationSource, resourceType, selectedProperties, odataUri)
			{
				this.jsonLightExpandedNavigationPropertyWriter = new ODataJsonLightDeltaWriter.JsonLightExpandedNavigationPropertyWriter(navigationSource, resourceType, parentDeltaResource, jsonLightOutputContext);
			}

			// Token: 0x170005CC RID: 1484
			// (get) Token: 0x06001A88 RID: 6792 RVA: 0x0004B6F2 File Offset: 0x000498F2
			public ODataJsonLightDeltaWriter.JsonLightExpandedNavigationPropertyWriter JsonLightExpandedNavigationPropertyWriter
			{
				get
				{
					return this.jsonLightExpandedNavigationPropertyWriter;
				}
			}

			// Token: 0x04000D2C RID: 3372
			private ODataJsonLightDeltaWriter.JsonLightExpandedNavigationPropertyWriter jsonLightExpandedNavigationPropertyWriter;
		}

		// Token: 0x0200032D RID: 813
		private sealed class JsonLightDeltaResourceSetScope : ODataJsonLightDeltaWriter.DeltaResourceSetScope
		{
			// Token: 0x06001A89 RID: 6793 RVA: 0x0004B6FA File Offset: 0x000498FA
			public JsonLightDeltaResourceSetScope(ODataItem resourceSet, IEdmNavigationSource navigationSource, IEdmEntityType resourceType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(resourceSet, navigationSource, resourceType, selectedProperties, odataUri)
			{
			}

			// Token: 0x170005CD RID: 1485
			// (get) Token: 0x06001A8A RID: 6794 RVA: 0x0004B709 File Offset: 0x00049909
			// (set) Token: 0x06001A8B RID: 6795 RVA: 0x0004B711 File Offset: 0x00049911
			public bool NextPageLinkWritten { get; set; }

			// Token: 0x170005CE RID: 1486
			// (get) Token: 0x06001A8C RID: 6796 RVA: 0x0004B71A File Offset: 0x0004991A
			// (set) Token: 0x06001A8D RID: 6797 RVA: 0x0004B722 File Offset: 0x00049922
			public bool DeltaLinkWritten { get; set; }
		}

		// Token: 0x0200032E RID: 814
		private sealed class JsonLightDeltaLinkScope : ODataJsonLightDeltaWriter.DeltaLinkScope
		{
			// Token: 0x06001A8E RID: 6798 RVA: 0x0004B72B File Offset: 0x0004992B
			public JsonLightDeltaLinkScope(ODataJsonLightDeltaWriter.WriterState state, ODataItem link, ODataDeltaSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType entityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(state, link, serializationInfo, navigationSource, entityType, selectedProperties, odataUri)
			{
			}
		}

		// Token: 0x0200032F RID: 815
		private sealed class ScopeStack
		{
			// Token: 0x170005CF RID: 1487
			// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0004B73E File Offset: 0x0004993E
			public int Count
			{
				get
				{
					return this.scopes.Count;
				}
			}

			// Token: 0x06001A90 RID: 6800 RVA: 0x0004B74B File Offset: 0x0004994B
			public void Push(ODataJsonLightDeltaWriter.Scope scope)
			{
				this.scopes.Push(scope);
			}

			// Token: 0x06001A91 RID: 6801 RVA: 0x0004B759 File Offset: 0x00049959
			public ODataJsonLightDeltaWriter.Scope Pop()
			{
				return this.scopes.Pop();
			}

			// Token: 0x06001A92 RID: 6802 RVA: 0x0004B766 File Offset: 0x00049966
			public ODataJsonLightDeltaWriter.Scope Peek()
			{
				return this.scopes.Peek();
			}

			// Token: 0x04000D2F RID: 3375
			private readonly Stack<ODataJsonLightDeltaWriter.Scope> scopes = new Stack<ODataJsonLightDeltaWriter.Scope>();
		}

		// Token: 0x02000330 RID: 816
		private static class DeltaConverter
		{
			// Token: 0x06001A94 RID: 6804 RVA: 0x0004B786 File Offset: 0x00049986
			public static ODataResourceSerializationInfo ToResourceSerializationInfo(ODataDeltaSerializationInfo serializationInfo)
			{
				if (serializationInfo == null)
				{
					return null;
				}
				return new ODataResourceSerializationInfo
				{
					NavigationSourceName = serializationInfo.NavigationSourceName,
					NavigationSourceKind = EdmNavigationSourceKind.EntitySet,
					NavigationSourceEntityTypeName = "null",
					ExpectedTypeName = "null"
				};
			}

			// Token: 0x06001A95 RID: 6805 RVA: 0x0004B7BB File Offset: 0x000499BB
			public static ODataResourceSerializationInfo ToResourceSerializationInfo(ODataDeltaResourceSetSerializationInfo serializationInfo)
			{
				if (serializationInfo == null)
				{
					return null;
				}
				return new ODataResourceSerializationInfo
				{
					NavigationSourceName = serializationInfo.EntitySetName,
					NavigationSourceKind = EdmNavigationSourceKind.EntitySet,
					NavigationSourceEntityTypeName = serializationInfo.EntityTypeName,
					ExpectedTypeName = serializationInfo.ExpectedTypeName
				};
			}

			// Token: 0x06001A96 RID: 6806 RVA: 0x0004B7F2 File Offset: 0x000499F2
			public static ODataDeltaSerializationInfo ToDeltaSerializationInfo(ODataDeltaResourceSetSerializationInfo serializationInfo)
			{
				if (serializationInfo == null)
				{
					return null;
				}
				return new ODataDeltaSerializationInfo
				{
					NavigationSourceName = serializationInfo.EntitySetName
				};
			}

			// Token: 0x06001A97 RID: 6807 RVA: 0x0004B80C File Offset: 0x00049A0C
			public static ODataResourceSet ToODataResourceSet(ODataDeltaResourceSet deltaResourceSet)
			{
				ODataResourceSet odataResourceSet = ODataJsonLightDeltaWriter.DeltaConverter.Clone(deltaResourceSet);
				odataResourceSet.SetSerializationInfo(ODataJsonLightDeltaWriter.DeltaConverter.ToResourceSerializationInfo(deltaResourceSet.SerializationInfo));
				return odataResourceSet;
			}

			// Token: 0x06001A98 RID: 6808 RVA: 0x0004B834 File Offset: 0x00049A34
			private static ODataResourceSet Clone(ODataResourceSetBase resourceSetBase)
			{
				return new ODataResourceSet
				{
					Count = resourceSetBase.Count,
					DeltaLink = resourceSetBase.DeltaLink,
					Id = resourceSetBase.Id,
					InstanceAnnotations = resourceSetBase.InstanceAnnotations,
					NextPageLink = resourceSetBase.NextPageLink
				};
			}
		}

		// Token: 0x02000331 RID: 817
		private sealed class JsonLightExpandedNavigationPropertyWriter
		{
			// Token: 0x06001A99 RID: 6809 RVA: 0x0004B882 File Offset: 0x00049A82
			public JsonLightExpandedNavigationPropertyWriter(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType, ODataResource parentDeltaResource, ODataJsonLightOutputContext jsonLightOutputContext)
			{
				this.parentDeltaResource = parentDeltaResource;
				this.resourceWriter = new ODataJsonLightWriter(jsonLightOutputContext, navigationSource, resourceType, false, false, true, null);
			}

			// Token: 0x06001A9A RID: 6810 RVA: 0x0004B8A4 File Offset: 0x00049AA4
			public void WriteStart(ODataResource resource)
			{
				this.IncreaseResourceDepth();
				this.resourceWriter.WriteStart(resource);
			}

			// Token: 0x06001A9B RID: 6811 RVA: 0x0004B8B8 File Offset: 0x00049AB8
			public void WriteStart(ODataResourceSet resourceSet)
			{
				this.IncreaseResourceDepth();
				this.resourceWriter.WriteStart(resourceSet);
			}

			// Token: 0x06001A9C RID: 6812 RVA: 0x0004B8CC File Offset: 0x00049ACC
			public void WriteStart(ODataNestedResourceInfo nestedResourceInfo)
			{
				this.IncreaseResourceDepth();
				this.resourceWriter.WriteStart(nestedResourceInfo);
			}

			// Token: 0x06001A9D RID: 6813 RVA: 0x0004B8E0 File Offset: 0x00049AE0
			public bool WriteEnd()
			{
				this.resourceWriter.WriteEnd();
				return this.DecreaseResourceDepth();
			}

			// Token: 0x06001A9E RID: 6814 RVA: 0x0004B8F3 File Offset: 0x00049AF3
			private void IncreaseResourceDepth()
			{
				if (this.currentResourceDepth == 0)
				{
					this.resourceWriter.WriteStart(this.parentDeltaResource);
				}
				this.currentResourceDepth++;
			}

			// Token: 0x06001A9F RID: 6815 RVA: 0x0004B91C File Offset: 0x00049B1C
			private bool DecreaseResourceDepth()
			{
				this.currentResourceDepth--;
				if (this.currentResourceDepth == 0)
				{
					this.resourceWriter.WriteEnd();
					return true;
				}
				return false;
			}

			// Token: 0x04000D30 RID: 3376
			private readonly ODataWriter resourceWriter;

			// Token: 0x04000D31 RID: 3377
			private readonly ODataResource parentDeltaResource;

			// Token: 0x04000D32 RID: 3378
			private int currentResourceDepth;
		}
	}
}
