using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000205 RID: 517
	internal sealed class ODataJsonLightDeltaReader : ODataDeltaReader
	{
		// Token: 0x060013FE RID: 5118 RVA: 0x000398C7 File Offset: 0x00037AC7
		public ODataJsonLightDeltaReader(ODataJsonLightInputContext jsonLightInputContext, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightResourceDeserializer = new ODataJsonLightResourceDeserializer(jsonLightInputContext);
			this.topLevelScope = new ODataJsonLightDeltaReader.JsonLightTopLevelScope(navigationSource, expectedEntityType);
			this.EnterScope(this.topLevelScope);
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00039906 File Offset: 0x00037B06
		public override ODataDeltaReaderState State
		{
			get
			{
				this.jsonLightInputContext.VerifyNotDisposed();
				return this.CurrentScope.State;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x0003991E File Offset: 0x00037B1E
		public override ODataReaderState SubState
		{
			get
			{
				this.jsonLightInputContext.VerifyNotDisposed();
				if (this.State != ODataDeltaReaderState.NestedResource)
				{
					return ODataReaderState.Start;
				}
				return this.CurrentJsonLightNestedResourceInfoScope.SubState;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x00039942 File Offset: 0x00037B42
		public override ODataItem Item
		{
			get
			{
				this.jsonLightInputContext.VerifyNotDisposed();
				if (this.State != ODataDeltaReaderState.NestedResource)
				{
					return this.CurrentScope.Item;
				}
				return this.CurrentJsonLightNestedResourceInfoScope.Item;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x00039970 File Offset: 0x00037B70
		private bool IsTopLevel
		{
			get
			{
				return this.scopes.Count <= 2;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x00039983 File Offset: 0x00037B83
		private ODataJsonLightDeltaReader.Scope CurrentScope
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00039990 File Offset: 0x00037B90
		// (set) Token: 0x06001405 RID: 5125 RVA: 0x000399AF File Offset: 0x00037BAF
		private IEdmEntityType CurrentEntityType
		{
			get
			{
				return this.scopes.Peek().EntityType;
			}
			set
			{
				this.scopes.Peek().EntityType = value;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x000399C4 File Offset: 0x00037BC4
		private IEdmNavigationSource CurrentNavigationSource
		{
			get
			{
				return this.scopes.Peek().NavigationSource;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x000399E3 File Offset: 0x00037BE3
		private IODataJsonLightReaderResourceState CurrentDeltaResourceState
		{
			get
			{
				return (IODataJsonLightReaderResourceState)this.CurrentScope;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x000399F0 File Offset: 0x00037BF0
		private ODataJsonLightDeltaReader.JsonLightNestedResourceInfoScope CurrentJsonLightNestedResourceInfoScope
		{
			get
			{
				return (ODataJsonLightDeltaReader.JsonLightNestedResourceInfoScope)this.CurrentScope;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x000399FD File Offset: 0x00037BFD
		private ODataJsonLightDeltaReader.JsonLightDeltaResourceSetScope CurrentJsonLightDeltaResourceSetScope
		{
			get
			{
				return (ODataJsonLightDeltaReader.JsonLightDeltaResourceSetScope)this.CurrentScope;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00039A0A File Offset: 0x00037C0A
		private ODataResource CurrentDeltaResource
		{
			get
			{
				return (ODataResource)this.Item;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x00039A17 File Offset: 0x00037C17
		private ODataDeltaDeletedEntry CurrentDeltaDeletedEntry
		{
			get
			{
				return (ODataDeltaDeletedEntry)this.Item;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00039A24 File Offset: 0x00037C24
		private ODataDeltaLink CurrentDeltaLink
		{
			get
			{
				return (ODataDeltaLink)this.Item;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x00039A31 File Offset: 0x00037C31
		private ODataDeltaDeletedLink CurrentDeltaDeletedLink
		{
			get
			{
				return (ODataDeltaDeletedLink)this.Item;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00039A3E File Offset: 0x00037C3E
		private ODataDeltaResourceSet CurrentDeltaResourceSet
		{
			get
			{
				return (ODataDeltaResourceSet)this.Item;
			}
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00039A4B File Offset: 0x00037C4B
		public override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x00039A66 File Offset: 0x00037C66
		private void EnterScope(ODataJsonLightDeltaReader.Scope scope)
		{
			this.scopes.Push(scope);
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x00039A74 File Offset: 0x00037C74
		private void ReplaceScope(ODataJsonLightDeltaReader.Scope scope)
		{
			this.scopes.Pop();
			this.EnterScope(scope);
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x00039A89 File Offset: 0x00037C89
		private void ReplaceScope(ODataDeltaReaderState state)
		{
			this.ReplaceScope(new ODataJsonLightDeltaReader.Scope(state, this.Item, this.CurrentNavigationSource, this.CurrentEntityType, this.CurrentScope.ODataUri));
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x00039AB4 File Offset: 0x00037CB4
		private void PopScope(ODataDeltaReaderState state)
		{
			this.scopes.Pop();
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00039AC2 File Offset: 0x00037CC2
		private void VerifyCanRead(bool synchronousCall)
		{
			this.jsonLightInputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataDeltaReaderState.Exception || this.State == ODataDeltaReaderState.Completed)
			{
				throw new ODataException(Strings.ODataReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00039AFF File Offset: 0x00037CFF
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.jsonLightInputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00039B1C File Offset: 0x00037D1C
		private T InterceptException<T>(Func<T> action)
		{
			T t;
			try
			{
				t = action.Invoke();
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex))
				{
					this.EnterScope(ODataJsonLightDeltaReader.CreateExceptionScope());
				}
				throw;
			}
			return t;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00039B5C File Offset: 0x00037D5C
		private Uri ReadUriValue()
		{
			return new Uri(this.ReadStringValue(), 0);
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00039B6A File Offset: 0x00037D6A
		private string ReadStringValue()
		{
			return this.jsonLightResourceDeserializer.JsonReader.ReadStringValue();
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00039B7C File Offset: 0x00037D7C
		private bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00039B84 File Offset: 0x00037D84
		private bool ReadImplementation()
		{
			bool flag;
			switch (this.State)
			{
			case ODataDeltaReaderState.Start:
				flag = this.ReadAtStartImplementation();
				break;
			case ODataDeltaReaderState.DeltaResourceSetStart:
				flag = this.ReadAtDeltaResourceSetStartImplementation();
				break;
			case ODataDeltaReaderState.DeltaResourceSetEnd:
				flag = this.ReadAtDeltaResourceSetEndImplementation();
				break;
			case ODataDeltaReaderState.DeltaResourceStart:
				flag = this.ReadAtDeltaResourceStartImplementation();
				break;
			case ODataDeltaReaderState.DeltaResourceEnd:
				flag = this.ReadAtDeltaResourceEndImplementation();
				break;
			case ODataDeltaReaderState.DeltaDeletedEntry:
			case ODataDeltaReaderState.DeltaLink:
			case ODataDeltaReaderState.DeltaDeletedLink:
				this.scopes.Pop();
				flag = this.ReadAtDeltaResourceSetStartImplementation();
				break;
			case ODataDeltaReaderState.Exception:
			case ODataDeltaReaderState.Completed:
				throw new ODataException(Strings.ODataReaderCore_NoReadCallsAllowed(this.State));
			case ODataDeltaReaderState.NestedResource:
				flag = this.ReadAtNestedResourceInfoImplementation();
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataReaderCore_ReadImplementation));
			}
			return flag;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00039C40 File Offset: 0x00037E40
		private void PreReadAtStartImplementation(out PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			propertyAndAnnotationCollector = this.jsonLightInputContext.CreatePropertyAndAnnotationCollector();
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00039C50 File Offset: 0x00037E50
		private bool ReadAtStartImplementation()
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector;
			this.PreReadAtStartImplementation(out propertyAndAnnotationCollector);
			this.jsonLightResourceDeserializer.ReadPayloadStart(ODataPayloadKind.Delta, propertyAndAnnotationCollector, false, false);
			return this.ReadAtStartImplementationSynchronously(propertyAndAnnotationCollector);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00039C7C File Offset: 0x00037E7C
		private bool ReadAtDeltaResourceSetStartImplementation()
		{
			return this.ReadAtDeltaResourceSetStartImplementationSynchronously();
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00039C84 File Offset: 0x00037E84
		private bool ReadAtDeltaResourceSetEndImplementation()
		{
			return this.ReadAtDeltaResourceSetEndImplementationSynchronously();
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00039C8C File Offset: 0x00037E8C
		private bool ReadAtDeltaResourceStartImplementation()
		{
			return this.ReadAtDeltaResourceStartImplementationSynchronously();
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00039C94 File Offset: 0x00037E94
		private bool ReadAtDeltaResourceEndImplementation()
		{
			return this.ReadAtDeltaResourceEndImplementationSynchronously();
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00039C9C File Offset: 0x00037E9C
		private bool ReadAtNestedResourceInfoImplementation()
		{
			return this.ReadAtNestedResourceInfoImplementationSynchronously();
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00039CA4 File Offset: 0x00037EA4
		private bool ReadAtStartImplementationSynchronously(PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			string text = ((this.jsonLightResourceDeserializer.ContextUriParseResult == null) ? null : this.jsonLightResourceDeserializer.ContextUriParseResult.SelectQueryOption);
			SelectedPropertiesNode selectedPropertiesNode = SelectedPropertiesNode.Create(text);
			this.topLevelScope.PropertyAndAnnotationCollector = propertyAndAnnotationCollector;
			bool flag = this.jsonLightInputContext.JsonReader is ReorderingJsonReader;
			ODataDeltaResourceSet odataDeltaResourceSet = new ODataDeltaResourceSet();
			this.jsonLightResourceDeserializer.ReadTopLevelResourceSetAnnotations(odataDeltaResourceSet, propertyAndAnnotationCollector, true, flag);
			this.ReadDeltaResourceSetStart(odataDeltaResourceSet, selectedPropertiesNode);
			return true;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00039D18 File Offset: 0x00037F18
		private bool ReadAtDeltaResourceSetStartImplementationSynchronously()
		{
			JsonNodeType nodeType = this.jsonLightResourceDeserializer.JsonReader.NodeType;
			if (nodeType != JsonNodeType.StartObject)
			{
				if (nodeType != JsonNodeType.EndArray)
				{
					throw new ODataException(Strings.ODataJsonReader_CannotReadResourcesOfResourceSet(this.jsonLightResourceDeserializer.JsonReader.NodeType));
				}
				this.ReadResourceSetEnd();
			}
			else
			{
				this.ReadDeltaStart(null, this.CurrentJsonLightDeltaResourceSetScope.SelectedProperties);
			}
			return true;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00039D7D File Offset: 0x00037F7D
		private bool ReadAtDeltaResourceSetEndImplementationSynchronously()
		{
			this.PopScope(ODataDeltaReaderState.DeltaResourceSetEnd);
			this.jsonLightResourceDeserializer.JsonReader.Read();
			this.jsonLightResourceDeserializer.ReadPayloadEnd(false);
			this.ReplaceScope(ODataDeltaReaderState.Completed);
			return false;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00039DAC File Offset: 0x00037FAC
		private bool ReadAtDeltaResourceStartImplementationSynchronously()
		{
			this.jsonLightResourceDeserializer.ReadResourceTypeName(this.CurrentDeltaResourceState);
			this.ReadDeltaResourceId();
			this.ApplyEntityTypeNameFromPayload(this.CurrentDeltaResource.TypeName);
			ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo;
			do
			{
				odataJsonLightReaderNestedResourceInfo = this.jsonLightResourceDeserializer.ReadResourceContent(this.CurrentDeltaResourceState);
				if (odataJsonLightReaderNestedResourceInfo == null)
				{
					goto IL_0070;
				}
			}
			while (!odataJsonLightReaderNestedResourceInfo.HasValue);
			this.EnterScope(new ODataJsonLightDeltaReader.JsonLightNestedResourceInfoScope(odataJsonLightReaderNestedResourceInfo, this.CurrentNavigationSource, odataJsonLightReaderNestedResourceInfo.NestedResourceType, this.CurrentScope.ODataUri, this.jsonLightInputContext));
			return true;
			IL_0070:
			this.EndDeltaResource(new ODataJsonLightDeltaReader.JsonLightDeltaResourceScope(ODataDeltaReaderState.DeltaResourceEnd, this.Item, this.CurrentNavigationSource, this.CurrentEntityType, this.CurrentDeltaResourceState.PropertyAndAnnotationCollector, this.CurrentDeltaResourceState.SelectedProperties, this.CurrentScope.ODataUri));
			return true;
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00039E69 File Offset: 0x00038069
		private void EndDeltaResource(ODataJsonLightDeltaReader.Scope scope)
		{
			this.PopScope(ODataDeltaReaderState.DeltaResourceStart);
			this.EnterScope(scope);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00039E79 File Offset: 0x00038079
		private bool ReadAtDeltaResourceEndImplementationSynchronously()
		{
			this.jsonLightResourceDeserializer.JsonReader.Read();
			this.PopScope(ODataDeltaReaderState.DeltaResourceEnd);
			return this.ReadAtDeltaResourceSetStartImplementationSynchronously();
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00039E99 File Offset: 0x00038099
		private bool ReadAtNestedResourceInfoImplementationSynchronously()
		{
			if (this.SubState == ODataReaderState.Completed)
			{
				this.PopScope(ODataDeltaReaderState.NestedResource);
				return true;
			}
			if (this.SubState == ODataReaderState.Exception)
			{
				this.EnterScope(ODataJsonLightDeltaReader.CreateExceptionScope());
				return false;
			}
			this.CurrentJsonLightNestedResourceInfoScope.NestedResourceInfoReader.Read();
			return true;
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00039ED7 File Offset: 0x000380D7
		private void ReadAtDeltaDeletedEntryImplementationSynchronously()
		{
			this.ReadDeltaDeletedEntryId();
			this.ReadDeltaDeletedEntryReason();
			this.jsonLightResourceDeserializer.JsonReader.Read();
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00039EF6 File Offset: 0x000380F6
		private void ReadAtDeltaLinkImplementationSynchronously()
		{
			this.ReadDeltaLinkSource();
			this.ReadDeltaLinkRelationship();
			this.ReadDeltaLinkTarget();
			this.jsonLightResourceDeserializer.JsonReader.Read();
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00039F1B File Offset: 0x0003811B
		private void ReadAtDeltaDeletedLinkImplementationSynchronously()
		{
			this.ReadDeltaDeletedLinkSource();
			this.ReadDeltaDeletedLinkRelationship();
			this.ReadDeltaDeletedLinkTarget();
			this.jsonLightResourceDeserializer.JsonReader.Read();
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00039F40 File Offset: 0x00038140
		private void ReadDeltaResourceSetStart(ODataDeltaResourceSet resourceSet, SelectedPropertiesNode selectedProperties)
		{
			this.jsonLightResourceDeserializer.ReadResourceSetContentStart();
			IJsonReader jsonReader = this.jsonLightResourceDeserializer.JsonReader;
			if (jsonReader.NodeType != JsonNodeType.EndArray && jsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_InvalidNodeTypeForItemsInResourceSet(jsonReader.NodeType));
			}
			this.EnterScope(new ODataJsonLightDeltaReader.JsonLightDeltaResourceSetScope(resourceSet, this.CurrentNavigationSource, this.CurrentEntityType, selectedProperties, this.CurrentScope.ODataUri));
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00039FB0 File Offset: 0x000381B0
		private void ReadResourceSetEnd()
		{
			this.jsonLightResourceDeserializer.ReadResourceSetContentEnd();
			this.jsonLightResourceDeserializer.ReadNextLinkAnnotationAtResourceSetEnd(this.CurrentDeltaResourceSet, null, this.topLevelScope.PropertyAndAnnotationCollector);
			this.ReplaceScope(ODataDeltaReaderState.DeltaResourceSetEnd);
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x00039FE4 File Offset: 0x000381E4
		private void ReadDeltaStart(PropertyAndAnnotationCollector propertyAndAnnotationCollector, SelectedPropertiesNode selectedProperties)
		{
			if (this.jsonLightResourceDeserializer.JsonReader.NodeType == JsonNodeType.StartObject)
			{
				this.jsonLightResourceDeserializer.JsonReader.Read();
			}
			ODataDeltaKind odataDeltaKind = ODataDeltaKind.Resource;
			IEdmEntityType edmEntityType = null;
			string text = this.jsonLightResourceDeserializer.ReadContextUriAnnotation(ODataPayloadKind.Delta, propertyAndAnnotationCollector, false);
			if (!string.IsNullOrEmpty(text))
			{
				ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = ODataJsonLightContextUriParser.Parse(this.jsonLightInputContext.Model, text, ODataPayloadKind.Delta, null, true, true);
				odataDeltaKind = odataJsonLightContextUriParseResult.DeltaKind;
				edmEntityType = odataJsonLightContextUriParseResult.EdmType as IEdmEntityType;
			}
			switch (odataDeltaKind)
			{
			case ODataDeltaKind.Resource:
				this.StartDeltaResource(ODataDeltaReaderState.DeltaResourceStart, propertyAndAnnotationCollector, selectedProperties, edmEntityType);
				return;
			case ODataDeltaKind.DeletedEntry:
				this.StartDeltaResource(ODataDeltaReaderState.DeltaDeletedEntry, propertyAndAnnotationCollector, selectedProperties, null);
				this.ReadAtDeltaDeletedEntryImplementationSynchronously();
				return;
			case ODataDeltaKind.Link:
				this.StartDeltaLink(ODataDeltaReaderState.DeltaLink, propertyAndAnnotationCollector, selectedProperties);
				this.ReadAtDeltaLinkImplementationSynchronously();
				return;
			case ODataDeltaKind.DeletedLink:
				this.StartDeltaLink(ODataDeltaReaderState.DeltaDeletedLink, propertyAndAnnotationCollector, selectedProperties);
				this.ReadAtDeltaDeletedLinkImplementationSynchronously();
				return;
			default:
				return;
			}
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0003A0B0 File Offset: 0x000382B0
		private void ReadDeltaResourceId()
		{
			if (this.jsonLightResourceDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("@odata.id", this.jsonLightResourceDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightResourceDeserializer.JsonReader.Read();
				this.CurrentDeltaResource.Id = this.jsonLightResourceDeserializer.ReadEntryInstanceAnnotation("odata.id", false, false, this.CurrentDeltaResourceState.PropertyAndAnnotationCollector) as Uri;
			}
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0003A12C File Offset: 0x0003832C
		private void ReadDeltaDeletedEntryId()
		{
			if (this.jsonLightResourceDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("id", this.jsonLightResourceDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightResourceDeserializer.JsonReader.Read();
				this.CurrentDeltaDeletedEntry.Id = this.ReadStringValue();
			}
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0003A18C File Offset: 0x0003838C
		private void ReadDeltaDeletedEntryReason()
		{
			if (this.jsonLightResourceDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("reason", this.jsonLightResourceDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightResourceDeserializer.JsonReader.Read();
				string text = this.ReadStringValue();
				if (string.CompareOrdinal(text, "changed") == 0)
				{
					this.CurrentDeltaDeletedEntry.Reason = new DeltaDeletedEntryReason?(DeltaDeletedEntryReason.Changed);
					return;
				}
				if (string.CompareOrdinal(text, "deleted") == 0)
				{
					this.CurrentDeltaDeletedEntry.Reason = new DeltaDeletedEntryReason?(DeltaDeletedEntryReason.Deleted);
					return;
				}
				this.CurrentDeltaDeletedEntry.Reason = default(DeltaDeletedEntryReason?);
			}
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0003A238 File Offset: 0x00038438
		private void ReadDeltaLinkSource()
		{
			if (this.jsonLightResourceDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("source", this.jsonLightResourceDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightResourceDeserializer.JsonReader.Read();
				this.CurrentDeltaLink.Source = this.ReadUriValue();
			}
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0003A298 File Offset: 0x00038498
		private void ReadDeltaLinkRelationship()
		{
			if (this.jsonLightResourceDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("relationship", this.jsonLightResourceDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightResourceDeserializer.JsonReader.Read();
				this.CurrentDeltaLink.Relationship = this.ReadStringValue();
			}
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0003A2F8 File Offset: 0x000384F8
		private void ReadDeltaLinkTarget()
		{
			if (this.jsonLightResourceDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("target", this.jsonLightResourceDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightResourceDeserializer.JsonReader.Read();
				this.CurrentDeltaLink.Target = this.ReadUriValue();
			}
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0003A358 File Offset: 0x00038558
		private void ReadDeltaDeletedLinkSource()
		{
			if (this.jsonLightResourceDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("source", this.jsonLightResourceDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightResourceDeserializer.JsonReader.Read();
				this.CurrentDeltaDeletedLink.Source = this.ReadUriValue();
			}
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0003A3B8 File Offset: 0x000385B8
		private void ReadDeltaDeletedLinkRelationship()
		{
			if (this.jsonLightResourceDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("relationship", this.jsonLightResourceDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightResourceDeserializer.JsonReader.Read();
				this.CurrentDeltaDeletedLink.Relationship = this.ReadStringValue();
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0003A418 File Offset: 0x00038618
		private void ReadDeltaDeletedLinkTarget()
		{
			if (this.jsonLightResourceDeserializer.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("target", this.jsonLightResourceDeserializer.JsonReader.GetPropertyName()) == 0)
			{
				this.jsonLightResourceDeserializer.JsonReader.Read();
				this.CurrentDeltaDeletedLink.Target = this.ReadUriValue();
			}
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0003A478 File Offset: 0x00038678
		private void StartDeltaResource(ODataDeltaReaderState state, PropertyAndAnnotationCollector propertyAndAnnotationCollector, SelectedPropertiesNode selectedProperties, IEdmEntityType entityTypeFromContextUri = null)
		{
			this.EnterScope(new ODataJsonLightDeltaReader.JsonLightDeltaResourceScope(state, ODataJsonLightDeltaReader.CreateNewDeltaResource(state), this.CurrentNavigationSource, entityTypeFromContextUri ?? this.CurrentEntityType, propertyAndAnnotationCollector ?? this.jsonLightInputContext.CreatePropertyAndAnnotationCollector(), selectedProperties, this.CurrentScope.ODataUri));
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0003A4C5 File Offset: 0x000386C5
		private void StartDeltaLink(ODataDeltaReaderState state, PropertyAndAnnotationCollector propertyAndAnnotationCollector, SelectedPropertiesNode selectedProperties)
		{
			this.EnterScope(new ODataJsonLightDeltaReader.JsonLightDeltaLinkScope(state, ODataJsonLightDeltaReader.CreateNewDeltaLink(state), this.CurrentNavigationSource, this.CurrentEntityType, this.CurrentScope.ODataUri));
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0003A4F0 File Offset: 0x000386F0
		private void ApplyEntityTypeNameFromPayload(string entityTypeNameFromPayload)
		{
			EdmTypeKind edmTypeKind;
			ODataTypeAnnotation odataTypeAnnotation;
			IEdmEntityTypeReference edmEntityTypeReference = (IEdmEntityTypeReference)this.jsonLightInputContext.MessageReaderSettings.Validator.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.Entity, new bool?(true), null, this.CurrentEntityType.ToTypeReference(), entityTypeNameFromPayload, this.jsonLightInputContext.Model, () => EdmTypeKind.Entity, out edmTypeKind, out odataTypeAnnotation);
			IEdmEntityType edmEntityType = null;
			ODataResource currentDeltaResource = this.CurrentDeltaResource;
			if (edmEntityTypeReference != null)
			{
				edmEntityType = edmEntityTypeReference.EntityDefinition();
				currentDeltaResource.TypeName = edmEntityType.FullTypeName();
				if (odataTypeAnnotation != null)
				{
					currentDeltaResource.TypeAnnotation = odataTypeAnnotation;
				}
			}
			else if (entityTypeNameFromPayload != null)
			{
				currentDeltaResource.TypeName = entityTypeNameFromPayload;
			}
			this.CurrentEntityType = edmEntityType;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0003A59A File Offset: 0x0003879A
		private static ODataItem CreateNewDeltaResource(ODataDeltaReaderState state)
		{
			if (state == ODataDeltaReaderState.DeltaResourceStart)
			{
				return new ODataResource
				{
					Properties = new ReadOnlyEnumerable<ODataProperty>()
				};
			}
			if (state == ODataDeltaReaderState.DeltaDeletedEntry)
			{
				return new ODataDeltaDeletedEntry(null, DeltaDeletedEntryReason.Deleted);
			}
			return null;
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0003A5BE File Offset: 0x000387BE
		private static ODataDeltaLinkBase CreateNewDeltaLink(ODataDeltaReaderState state)
		{
			if (state == ODataDeltaReaderState.DeltaLink)
			{
				return new ODataDeltaLink(null, null, null);
			}
			if (state == ODataDeltaReaderState.DeltaDeletedLink)
			{
				return new ODataDeltaDeletedLink(null, null, null);
			}
			return null;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0003A5DB File Offset: 0x000387DB
		private static ODataJsonLightDeltaReader.Scope CreateExceptionScope()
		{
			return new ODataJsonLightDeltaReader.Scope(ODataDeltaReaderState.Exception, null, null, null, null);
		}

		// Token: 0x04000A0D RID: 2573
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000A0E RID: 2574
		private readonly ODataJsonLightResourceDeserializer jsonLightResourceDeserializer;

		// Token: 0x04000A0F RID: 2575
		private readonly ODataJsonLightDeltaReader.JsonLightTopLevelScope topLevelScope;

		// Token: 0x04000A10 RID: 2576
		private readonly Stack<ODataJsonLightDeltaReader.Scope> scopes = new Stack<ODataJsonLightDeltaReader.Scope>();

		// Token: 0x0200031D RID: 797
		private class Scope
		{
			// Token: 0x06001A36 RID: 6710 RVA: 0x0004B18B File Offset: 0x0004938B
			public Scope(ODataDeltaReaderState state, ODataItem item, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, ODataUri odataUri)
			{
				this.state = state;
				this.item = item;
				this.EntityType = expectedEntityType;
				this.NavigationSource = navigationSource;
				this.odataUri = odataUri;
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x06001A37 RID: 6711 RVA: 0x0004B1B8 File Offset: 0x000493B8
			public ODataDeltaReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x170005A5 RID: 1445
			// (get) Token: 0x06001A38 RID: 6712 RVA: 0x0004B1C0 File Offset: 0x000493C0
			public ODataItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x170005A6 RID: 1446
			// (get) Token: 0x06001A39 RID: 6713 RVA: 0x0004B1C8 File Offset: 0x000493C8
			public ODataUri ODataUri
			{
				get
				{
					return this.odataUri;
				}
			}

			// Token: 0x170005A7 RID: 1447
			// (get) Token: 0x06001A3A RID: 6714 RVA: 0x0004B1D0 File Offset: 0x000493D0
			// (set) Token: 0x06001A3B RID: 6715 RVA: 0x0004B1D8 File Offset: 0x000493D8
			public IEdmNavigationSource NavigationSource { get; private set; }

			// Token: 0x170005A8 RID: 1448
			// (get) Token: 0x06001A3C RID: 6716 RVA: 0x0004B1E1 File Offset: 0x000493E1
			// (set) Token: 0x06001A3D RID: 6717 RVA: 0x0004B1E9 File Offset: 0x000493E9
			public IEdmEntityType EntityType { get; set; }

			// Token: 0x04000CF6 RID: 3318
			private readonly ODataDeltaReaderState state;

			// Token: 0x04000CF7 RID: 3319
			private readonly ODataItem item;

			// Token: 0x04000CF8 RID: 3320
			private readonly ODataUri odataUri;
		}

		// Token: 0x0200031E RID: 798
		private sealed class JsonLightDeltaResourceSetScope : ODataJsonLightDeltaReader.Scope
		{
			// Token: 0x06001A3E RID: 6718 RVA: 0x0004B1F2 File Offset: 0x000493F2
			public JsonLightDeltaResourceSetScope(ODataDeltaResourceSet resourceSet, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(ODataDeltaReaderState.DeltaResourceSetStart, resourceSet, navigationSource, expectedEntityType, odataUri)
			{
				this.SelectedProperties = selectedProperties;
			}

			// Token: 0x170005A9 RID: 1449
			// (get) Token: 0x06001A3F RID: 6719 RVA: 0x0004B208 File Offset: 0x00049408
			// (set) Token: 0x06001A40 RID: 6720 RVA: 0x0004B210 File Offset: 0x00049410
			public SelectedPropertiesNode SelectedProperties { get; private set; }
		}

		// Token: 0x0200031F RID: 799
		private sealed class JsonLightDeltaLinkScope : ODataJsonLightDeltaReader.Scope
		{
			// Token: 0x06001A41 RID: 6721 RVA: 0x0004B219 File Offset: 0x00049419
			public JsonLightDeltaLinkScope(ODataDeltaReaderState state, ODataDeltaLinkBase link, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, ODataUri odataUri)
				: base(state, link, navigationSource, expectedEntityType, odataUri)
			{
			}
		}

		// Token: 0x02000320 RID: 800
		private sealed class JsonLightDeltaResourceScope : ODataJsonLightDeltaReader.Scope, IODataJsonLightReaderResourceState
		{
			// Token: 0x06001A42 RID: 6722 RVA: 0x0004B228 File Offset: 0x00049428
			public JsonLightDeltaResourceScope(ODataDeltaReaderState readerState, ODataItem resource, IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType, PropertyAndAnnotationCollector propertyAndAnnotationCollector, SelectedPropertiesNode selectedProperties, ODataUri odataUri)
				: base(readerState, resource, navigationSource, expectedEntityType, odataUri)
			{
				this.PropertyAndAnnotationCollector = propertyAndAnnotationCollector;
				this.SelectedProperties = selectedProperties;
			}

			// Token: 0x170005AA RID: 1450
			// (get) Token: 0x06001A43 RID: 6723 RVA: 0x0004B247 File Offset: 0x00049447
			// (set) Token: 0x06001A44 RID: 6724 RVA: 0x0004B24F File Offset: 0x0004944F
			public ODataResourceMetadataBuilder MetadataBuilder { get; set; }

			// Token: 0x170005AB RID: 1451
			// (get) Token: 0x06001A45 RID: 6725 RVA: 0x0004B258 File Offset: 0x00049458
			// (set) Token: 0x06001A46 RID: 6726 RVA: 0x0004B260 File Offset: 0x00049460
			public bool AnyPropertyFound { get; set; }

			// Token: 0x170005AC RID: 1452
			// (get) Token: 0x06001A47 RID: 6727 RVA: 0x0004B269 File Offset: 0x00049469
			// (set) Token: 0x06001A48 RID: 6728 RVA: 0x0004B271 File Offset: 0x00049471
			public ODataJsonLightReaderNestedResourceInfo FirstNestedResourceInfo { get; set; }

			// Token: 0x170005AD RID: 1453
			// (get) Token: 0x06001A49 RID: 6729 RVA: 0x0004B27A File Offset: 0x0004947A
			// (set) Token: 0x06001A4A RID: 6730 RVA: 0x0004B282 File Offset: 0x00049482
			public PropertyAndAnnotationCollector PropertyAndAnnotationCollector { get; private set; }

			// Token: 0x170005AE RID: 1454
			// (get) Token: 0x06001A4B RID: 6731 RVA: 0x0004B28B File Offset: 0x0004948B
			// (set) Token: 0x06001A4C RID: 6732 RVA: 0x0004B293 File Offset: 0x00049493
			public SelectedPropertiesNode SelectedProperties { get; private set; }

			// Token: 0x170005AF RID: 1455
			// (get) Token: 0x06001A4D RID: 6733 RVA: 0x0004B29C File Offset: 0x0004949C
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

			// Token: 0x170005B0 RID: 1456
			// (get) Token: 0x06001A4E RID: 6734 RVA: 0x0004B2C1 File Offset: 0x000494C1
			// (set) Token: 0x06001A4F RID: 6735 RVA: 0x0004B2C9 File Offset: 0x000494C9
			public bool ProcessingMissingProjectedNestedResourceInfos { get; set; }

			// Token: 0x170005B1 RID: 1457
			// (get) Token: 0x06001A50 RID: 6736 RVA: 0x0004B2D2 File Offset: 0x000494D2
			ODataResource IODataJsonLightReaderResourceState.Resource
			{
				get
				{
					return base.Item as ODataResource;
				}
			}

			// Token: 0x170005B2 RID: 1458
			// (get) Token: 0x06001A51 RID: 6737 RVA: 0x0004B2DF File Offset: 0x000494DF
			IEdmStructuredType IODataJsonLightReaderResourceState.ResourceType
			{
				get
				{
					return base.EntityType;
				}
			}

			// Token: 0x170005B3 RID: 1459
			// (get) Token: 0x06001A52 RID: 6738 RVA: 0x0004B2E7 File Offset: 0x000494E7
			// (set) Token: 0x06001A53 RID: 6739 RVA: 0x0004B2EF File Offset: 0x000494EF
			public IEdmStructuredType ResourceTypeFromMetadata { get; set; }

			// Token: 0x04000CFC RID: 3324
			private List<string> navigationPropertiesRead;
		}

		// Token: 0x02000321 RID: 801
		private sealed class JsonLightNestedResourceInfoScope : ODataJsonLightDeltaReader.Scope
		{
			// Token: 0x06001A54 RID: 6740 RVA: 0x0004B2F8 File Offset: 0x000494F8
			public JsonLightNestedResourceInfoScope(ODataJsonLightReaderNestedResourceInfo nestedResourceInfo, IEdmNavigationSource parentNavigationSource, IEdmStructuredType expectedResourceType, ODataUri odataUri, ODataJsonLightInputContext jsonLightInputContext)
				: base(ODataDeltaReaderState.NestedResource, nestedResourceInfo.NestedResourceInfo, parentNavigationSource, null, odataUri)
			{
				bool flag = nestedResourceInfo.NestedResourceSet != null;
				IEdmNavigationSource edmNavigationSource = null;
				if (nestedResourceInfo.NavigationProperty != null)
				{
					edmNavigationSource = parentNavigationSource.FindNavigationTarget(nestedResourceInfo.NavigationProperty);
				}
				this.nestedResourceInfoReader = new ODataJsonLightReader(jsonLightInputContext, edmNavigationSource, expectedResourceType, flag, false, true, null);
			}

			// Token: 0x170005B4 RID: 1460
			// (get) Token: 0x06001A55 RID: 6741 RVA: 0x0004B34B File Offset: 0x0004954B
			public ODataReaderState SubState
			{
				get
				{
					return this.nestedResourceInfoReader.State;
				}
			}

			// Token: 0x170005B5 RID: 1461
			// (get) Token: 0x06001A56 RID: 6742 RVA: 0x0004B358 File Offset: 0x00049558
			public new ODataItem Item
			{
				get
				{
					if (base.State != ODataDeltaReaderState.NestedResource || (this.SubState != ODataReaderState.Start && this.SubState != ODataReaderState.Completed))
					{
						return this.nestedResourceInfoReader.Item;
					}
					return base.Item;
				}
			}

			// Token: 0x170005B6 RID: 1462
			// (get) Token: 0x06001A57 RID: 6743 RVA: 0x0004B388 File Offset: 0x00049588
			public ODataReader NestedResourceInfoReader
			{
				get
				{
					return this.nestedResourceInfoReader;
				}
			}

			// Token: 0x04000D04 RID: 3332
			private readonly ODataReader nestedResourceInfoReader;
		}

		// Token: 0x02000322 RID: 802
		private sealed class JsonLightTopLevelScope : ODataJsonLightDeltaReader.Scope
		{
			// Token: 0x06001A58 RID: 6744 RVA: 0x0004B390 File Offset: 0x00049590
			public JsonLightTopLevelScope(IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType)
				: base(ODataDeltaReaderState.Start, null, navigationSource, expectedEntityType, null)
			{
			}

			// Token: 0x170005B7 RID: 1463
			// (get) Token: 0x06001A59 RID: 6745 RVA: 0x0004B39D File Offset: 0x0004959D
			// (set) Token: 0x06001A5A RID: 6746 RVA: 0x0004B3A5 File Offset: 0x000495A5
			public PropertyAndAnnotationCollector PropertyAndAnnotationCollector { get; set; }
		}
	}
}
