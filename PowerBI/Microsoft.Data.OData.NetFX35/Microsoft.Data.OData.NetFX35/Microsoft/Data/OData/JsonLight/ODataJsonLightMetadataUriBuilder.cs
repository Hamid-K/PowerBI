using System;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200016A RID: 362
	internal abstract class ODataJsonLightMetadataUriBuilder
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060009CD RID: 2509
		internal abstract Uri BaseUri { get; }

		// Token: 0x060009CE RID: 2510 RVA: 0x00020474 File Offset: 0x0001E674
		internal static ODataJsonLightMetadataUriBuilder CreateFromSettings(JsonLightMetadataLevel metadataLevel, bool writingResponse, ODataMessageWriterSettings writerSettings, IEdmModel model)
		{
			if (!metadataLevel.ShouldWriteODataMetadataUri())
			{
				return ODataJsonLightMetadataUriBuilder.NullMetadataUriBuilder.Instance;
			}
			ODataMetadataDocumentUri metadataDocumentUri = writerSettings.MetadataDocumentUri;
			if (metadataDocumentUri != null)
			{
				return ODataJsonLightMetadataUriBuilder.CreateDirectlyFromUri(metadataDocumentUri, model, writingResponse);
			}
			if (writingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightOutputContext_MetadataDocumentUriMissing);
			}
			return ODataJsonLightMetadataUriBuilder.NullMetadataUriBuilder.Instance;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000204B5 File Offset: 0x0001E6B5
		internal static ODataJsonLightMetadataUriBuilder CreateDirectlyFromUri(ODataMetadataDocumentUri metadataDocumentUri, IEdmModel model, bool writingResponse)
		{
			return new ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder(metadataDocumentUri, model, writingResponse);
		}

		// Token: 0x060009D0 RID: 2512
		internal abstract bool TryBuildFeedMetadataUri(ODataFeedAndEntryTypeContext typeContext, out Uri metadataUri);

		// Token: 0x060009D1 RID: 2513
		internal abstract bool TryBuildEntryMetadataUri(ODataFeedAndEntryTypeContext typeContext, out Uri metadataUri);

		// Token: 0x060009D2 RID: 2514
		internal abstract bool TryBuildMetadataUriForValue(ODataProperty property, out Uri metadataUri);

		// Token: 0x060009D3 RID: 2515
		internal abstract bool TryBuildEntityReferenceLinkMetadataUri(ODataEntityReferenceLinkSerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty, out Uri metadataUri);

		// Token: 0x060009D4 RID: 2516
		internal abstract bool TryBuildEntityReferenceLinksMetadataUri(ODataEntityReferenceLinksSerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty, out Uri metadataUri);

		// Token: 0x060009D5 RID: 2517
		internal abstract bool TryBuildCollectionMetadataUri(ODataCollectionStartSerializationInfo serializationInfo, IEdmTypeReference itemTypeReference, out Uri metadataUri);

		// Token: 0x060009D6 RID: 2518
		internal abstract bool TryBuildServiceDocumentMetadataUri(out Uri metadataUri);

		// Token: 0x0200016B RID: 363
		private sealed class DefaultMetadataUriBuilder : ODataJsonLightMetadataUriBuilder
		{
			// Token: 0x060009D8 RID: 2520 RVA: 0x000204C7 File Offset: 0x0001E6C7
			internal DefaultMetadataUriBuilder(ODataMetadataDocumentUri metadataDocumentUri, IEdmModel model, bool writingResponse)
			{
				this.metadataDocumentUri = metadataDocumentUri;
				this.model = model;
				this.writingResponse = writingResponse;
			}

			// Token: 0x17000262 RID: 610
			// (get) Token: 0x060009D9 RID: 2521 RVA: 0x000204E4 File Offset: 0x0001E6E4
			internal override Uri BaseUri
			{
				get
				{
					return this.metadataDocumentUri.BaseUri;
				}
			}

			// Token: 0x060009DA RID: 2522 RVA: 0x000204F1 File Offset: 0x0001E6F1
			internal override bool TryBuildFeedMetadataUri(ODataFeedAndEntryTypeContext typeContext, out Uri metadataUri)
			{
				metadataUri = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.CreateFeedOrEntryMetadataUri(this.metadataDocumentUri, this.model, typeContext, false, this.writingResponse);
				return metadataUri != null;
			}

			// Token: 0x060009DB RID: 2523 RVA: 0x00020516 File Offset: 0x0001E716
			internal override bool TryBuildEntryMetadataUri(ODataFeedAndEntryTypeContext typeContext, out Uri metadataUri)
			{
				metadataUri = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.CreateFeedOrEntryMetadataUri(this.metadataDocumentUri, this.model, typeContext, true, this.writingResponse);
				return metadataUri != null;
			}

			// Token: 0x060009DC RID: 2524 RVA: 0x0002053C File Offset: 0x0001E73C
			internal override bool TryBuildMetadataUriForValue(ODataProperty property, out Uri metadataUri)
			{
				string metadataUriTypeNameForValue = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.GetMetadataUriTypeNameForValue(property);
				if (string.IsNullOrEmpty(metadataUriTypeNameForValue))
				{
					throw new ODataException(Strings.WriterValidationUtils_MissingTypeNameWithMetadata);
				}
				metadataUri = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.CreateTypeMetadataUri(this.metadataDocumentUri, metadataUriTypeNameForValue);
				return metadataUri != null;
			}

			// Token: 0x060009DD RID: 2525 RVA: 0x0002057C File Offset: 0x0001E77C
			internal override bool TryBuildEntityReferenceLinkMetadataUri(ODataEntityReferenceLinkSerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty, out Uri metadataUri)
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				bool flag = false;
				if (serializationInfo != null)
				{
					text = serializationInfo.SourceEntitySetName;
					text2 = serializationInfo.Typecast;
					text3 = serializationInfo.NavigationPropertyName;
					flag = serializationInfo.IsCollectionNavigationProperty;
				}
				else if (navigationProperty != null)
				{
					text = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.GetEntitySetName(entitySet, this.model);
					text2 = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.GetTypecast(entitySet, navigationProperty.DeclaringEntityType());
					text3 = navigationProperty.Name;
					flag = navigationProperty.Type.IsCollection();
				}
				metadataUri = ((text3 == null) ? null : ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.CreateEntityContainerElementMetadataUri(this.metadataDocumentUri, text, text2, text3, flag));
				if (this.writingResponse && metadataUri == null)
				{
					throw new ODataException(Strings.ODataJsonLightMetadataUriBuilder_EntitySetOrNavigationPropertyMissingForTopLevelEntityReferenceLinkResponse);
				}
				return metadataUri != null;
			}

			// Token: 0x060009DE RID: 2526 RVA: 0x00020624 File Offset: 0x0001E824
			internal override bool TryBuildEntityReferenceLinksMetadataUri(ODataEntityReferenceLinksSerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty, out Uri metadataUri)
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				if (serializationInfo != null)
				{
					text = serializationInfo.SourceEntitySetName;
					text2 = serializationInfo.Typecast;
					text3 = serializationInfo.NavigationPropertyName;
				}
				else if (navigationProperty != null)
				{
					text = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.GetEntitySetName(entitySet, this.model);
					text2 = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.GetTypecast(entitySet, navigationProperty.DeclaringEntityType());
					text3 = navigationProperty.Name;
				}
				metadataUri = ((text3 == null) ? null : ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.CreateEntityContainerElementMetadataUri(this.metadataDocumentUri, text, text2, text3, false));
				if (this.writingResponse && metadataUri == null)
				{
					throw new ODataException(Strings.ODataJsonLightMetadataUriBuilder_EntitySetOrNavigationPropertyMissingForTopLevelEntityReferenceLinksResponse);
				}
				return metadataUri != null;
			}

			// Token: 0x060009DF RID: 2527 RVA: 0x000206B4 File Offset: 0x0001E8B4
			internal override bool TryBuildCollectionMetadataUri(ODataCollectionStartSerializationInfo serializationInfo, IEdmTypeReference itemTypeReference, out Uri metadataUri)
			{
				string text = null;
				if (serializationInfo != null)
				{
					text = serializationInfo.CollectionTypeName;
				}
				else if (itemTypeReference != null)
				{
					text = EdmLibraryExtensions.GetCollectionTypeName(itemTypeReference.ODataFullName());
				}
				metadataUri = ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.CreateTypeMetadataUri(this.metadataDocumentUri, text);
				if (this.writingResponse && metadataUri == null)
				{
					throw new ODataException(Strings.ODataJsonLightMetadataUriBuilder_TypeNameMissingForTopLevelCollectionWhenWritingResponsePayload);
				}
				return metadataUri != null;
			}

			// Token: 0x060009E0 RID: 2528 RVA: 0x00020711 File Offset: 0x0001E911
			internal override bool TryBuildServiceDocumentMetadataUri(out Uri metadataUri)
			{
				metadataUri = this.metadataDocumentUri.BaseUri;
				return true;
			}

			// Token: 0x060009E1 RID: 2529 RVA: 0x00020724 File Offset: 0x0001E924
			private static string GetMetadataUriTypeNameForValue(ODataProperty property)
			{
				ODataValue odataValue = property.ODataValue;
				if (odataValue.IsNullValue)
				{
					return "Edm.Null";
				}
				SerializationTypeNameAnnotation annotation = odataValue.GetAnnotation<SerializationTypeNameAnnotation>();
				if (annotation != null && !string.IsNullOrEmpty(annotation.TypeName))
				{
					return annotation.TypeName;
				}
				ODataComplexValue odataComplexValue = odataValue as ODataComplexValue;
				if (odataComplexValue != null)
				{
					return odataComplexValue.TypeName;
				}
				ODataCollectionValue odataCollectionValue = odataValue as ODataCollectionValue;
				if (odataCollectionValue != null)
				{
					return odataCollectionValue.TypeName;
				}
				ODataPrimitiveValue odataPrimitiveValue = odataValue as ODataPrimitiveValue;
				if (odataPrimitiveValue == null)
				{
					throw new ODataException(Strings.ODataWriter_StreamPropertiesMustBePropertiesOfODataEntry(property.Name));
				}
				return EdmLibraryExtensions.GetPrimitiveTypeReference(odataPrimitiveValue.Value.GetType()).ODataFullName();
			}

			// Token: 0x060009E2 RID: 2530 RVA: 0x000207BC File Offset: 0x0001E9BC
			private static string GetEntitySetName(IEdmEntitySet entitySet, IEdmModel edmModel)
			{
				if (entitySet == null)
				{
					return null;
				}
				IEdmEntityContainer container = entitySet.Container;
				string text;
				if (edmModel.IsDefaultEntityContainer(container))
				{
					text = entitySet.Name;
				}
				else
				{
					text = string.Concat(new string[] { container.Namespace, ".", container.Name, ".", entitySet.Name });
				}
				return text;
			}

			// Token: 0x060009E3 RID: 2531 RVA: 0x00020824 File Offset: 0x0001EA24
			private static string GetTypecast(IEdmEntitySet entitySet, IEdmEntityType entityType)
			{
				if (entitySet == null || entityType == null)
				{
					return null;
				}
				IEdmEntityType elementType = EdmTypeWriterResolver.Instance.GetElementType(entitySet);
				if (elementType.IsEquivalentTo(entityType))
				{
					return null;
				}
				if (!elementType.IsAssignableFrom(entityType))
				{
					throw new ODataException(Strings.ODataJsonLightMetadataUriBuilder_ValidateDerivedType(elementType.FullName(), entityType.FullName()));
				}
				return entityType.ODataFullName();
			}

			// Token: 0x060009E4 RID: 2532 RVA: 0x00020876 File Offset: 0x0001EA76
			private static Uri CreateTypeMetadataUri(ODataMetadataDocumentUri metadataDocumentUri, string fullTypeName)
			{
				if (fullTypeName != null)
				{
					return new Uri(metadataDocumentUri.BaseUri, '#' + fullTypeName);
				}
				return null;
			}

			// Token: 0x060009E5 RID: 2533 RVA: 0x00020898 File Offset: 0x0001EA98
			private static Uri CreateFeedOrEntryMetadataUri(ODataMetadataDocumentUri metadataDocumentUri, IEdmModel model, ODataFeedAndEntryTypeContext typeContext, bool isEntry, bool writingResponse)
			{
				string text = ((typeContext.EntitySetElementTypeName == typeContext.ExpectedEntityTypeName) ? null : typeContext.ExpectedEntityTypeName);
				return ODataJsonLightMetadataUriBuilder.DefaultMetadataUriBuilder.CreateEntityContainerElementMetadataUri(metadataDocumentUri, typeContext.EntitySetName, text, null, isEntry);
			}

			// Token: 0x060009E6 RID: 2534 RVA: 0x000208D4 File Offset: 0x0001EAD4
			private static Uri CreateEntityContainerElementMetadataUri(ODataMetadataDocumentUri metadataDocumentUri, string entitySetName, string typecast, string navigationPropertyName, bool appendItemSelector)
			{
				if (entitySetName == null)
				{
					return null;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append('#');
				stringBuilder.Append(entitySetName);
				if (typecast != null)
				{
					stringBuilder.Append('/');
					stringBuilder.Append(typecast);
				}
				if (navigationPropertyName != null)
				{
					stringBuilder.Append('/');
					stringBuilder.Append("$links");
					stringBuilder.Append('/');
					stringBuilder.Append(navigationPropertyName);
				}
				if (appendItemSelector)
				{
					stringBuilder.Append('/');
					stringBuilder.Append("@Element");
				}
				string selectClause = metadataDocumentUri.SelectClause;
				if (selectClause != null)
				{
					stringBuilder.Append('&');
					stringBuilder.Append("$select");
					stringBuilder.Append('=');
					stringBuilder.Append(selectClause);
				}
				return new Uri(metadataDocumentUri.BaseUri, stringBuilder.ToString());
			}

			// Token: 0x040003BA RID: 954
			private readonly ODataMetadataDocumentUri metadataDocumentUri;

			// Token: 0x040003BB RID: 955
			private readonly IEdmModel model;

			// Token: 0x040003BC RID: 956
			private readonly bool writingResponse;
		}

		// Token: 0x0200016C RID: 364
		private sealed class NullMetadataUriBuilder : ODataJsonLightMetadataUriBuilder
		{
			// Token: 0x060009E7 RID: 2535 RVA: 0x00020994 File Offset: 0x0001EB94
			private NullMetadataUriBuilder()
			{
			}

			// Token: 0x17000263 RID: 611
			// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0002099C File Offset: 0x0001EB9C
			internal override Uri BaseUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060009E9 RID: 2537 RVA: 0x0002099F File Offset: 0x0001EB9F
			internal override bool TryBuildFeedMetadataUri(ODataFeedAndEntryTypeContext typeContext, out Uri metadataUri)
			{
				metadataUri = null;
				return false;
			}

			// Token: 0x060009EA RID: 2538 RVA: 0x000209A5 File Offset: 0x0001EBA5
			internal override bool TryBuildEntryMetadataUri(ODataFeedAndEntryTypeContext typeContext, out Uri metadataUri)
			{
				metadataUri = null;
				return false;
			}

			// Token: 0x060009EB RID: 2539 RVA: 0x000209AB File Offset: 0x0001EBAB
			internal override bool TryBuildMetadataUriForValue(ODataProperty property, out Uri metadataUri)
			{
				metadataUri = null;
				return false;
			}

			// Token: 0x060009EC RID: 2540 RVA: 0x000209B1 File Offset: 0x0001EBB1
			internal override bool TryBuildEntityReferenceLinkMetadataUri(ODataEntityReferenceLinkSerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty, out Uri metadataUri)
			{
				metadataUri = null;
				return false;
			}

			// Token: 0x060009ED RID: 2541 RVA: 0x000209B8 File Offset: 0x0001EBB8
			internal override bool TryBuildEntityReferenceLinksMetadataUri(ODataEntityReferenceLinksSerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty, out Uri metadataUri)
			{
				metadataUri = null;
				return false;
			}

			// Token: 0x060009EE RID: 2542 RVA: 0x000209BF File Offset: 0x0001EBBF
			internal override bool TryBuildCollectionMetadataUri(ODataCollectionStartSerializationInfo serializationInfo, IEdmTypeReference itemTypeReference, out Uri metadataUri)
			{
				metadataUri = null;
				return false;
			}

			// Token: 0x060009EF RID: 2543 RVA: 0x000209C5 File Offset: 0x0001EBC5
			internal override bool TryBuildServiceDocumentMetadataUri(out Uri metadataUri)
			{
				metadataUri = null;
				return false;
			}

			// Token: 0x040003BD RID: 957
			internal static readonly ODataJsonLightMetadataUriBuilder.NullMetadataUriBuilder Instance = new ODataJsonLightMetadataUriBuilder.NullMetadataUriBuilder();
		}
	}
}
