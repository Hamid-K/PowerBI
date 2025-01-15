using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000167 RID: 359
	internal sealed class ODataJsonLightMetadataUriParser
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x0001FA36 File Offset: 0x0001DC36
		private ODataJsonLightMetadataUriParser(IEdmModel model, Uri metadataUriFromPayload)
		{
			if (!model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_NoModel);
			}
			this.model = model;
			this.parseResult = new ODataJsonLightMetadataUriParseResult(metadataUriFromPayload);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001FA64 File Offset: 0x0001DC64
		internal static ODataJsonLightMetadataUriParseResult Parse(IEdmModel model, string metadataUriFromPayload, ODataPayloadKind payloadKind, ODataVersion version, ODataReaderBehavior readerBehavior)
		{
			if (metadataUriFromPayload == null)
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_NullMetadataDocumentUri);
			}
			Uri uri = new Uri(metadataUriFromPayload, 1);
			ODataJsonLightMetadataUriParser odataJsonLightMetadataUriParser = new ODataJsonLightMetadataUriParser(model, uri);
			odataJsonLightMetadataUriParser.TokenizeMetadataUri();
			odataJsonLightMetadataUriParser.ParseMetadataUri(payloadKind, readerBehavior, version);
			return odataJsonLightMetadataUriParser.parseResult;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001FAA8 File Offset: 0x0001DCA8
		private static string ExtractSelectQueryOption(string fragment)
		{
			int num = fragment.IndexOf(ODataJsonLightMetadataUriParser.SelectQueryOptionStart, 4);
			if (num < 0)
			{
				return null;
			}
			int num2 = num + ODataJsonLightMetadataUriParser.SelectQueryOptionStart.Length;
			int num3 = fragment.IndexOf('&', num2);
			string text;
			if (num3 < 0)
			{
				text = fragment.Substring(num2);
			}
			else
			{
				text = fragment.Substring(num2, num3 - num2);
			}
			return text.Trim();
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0001FB00 File Offset: 0x0001DD00
		private void TokenizeMetadataUri()
		{
			Uri metadataUri = this.parseResult.MetadataUri;
			UriBuilder uriBuilder = new UriBuilder(metadataUri)
			{
				Fragment = null
			};
			this.parseResult.MetadataDocumentUri = uriBuilder.Uri;
			this.parseResult.Fragment = metadataUri.GetComponents(64, 2);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0001FB50 File Offset: 0x0001DD50
		private void ParseMetadataUri(ODataPayloadKind expectedPayloadKind, ODataReaderBehavior readerBehavior, ODataVersion version)
		{
			ODataPayloadKind odataPayloadKind = this.ParseMetadataUriFragment(this.parseResult.Fragment, readerBehavior, version);
			bool flag = odataPayloadKind == expectedPayloadKind || expectedPayloadKind == ODataPayloadKind.Unsupported;
			if (odataPayloadKind == ODataPayloadKind.Collection)
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					ODataPayloadKind.Collection,
					ODataPayloadKind.Property
				};
				if (expectedPayloadKind == ODataPayloadKind.Property)
				{
					flag = true;
				}
			}
			else
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[] { odataPayloadKind };
			}
			if (!flag)
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_MetadataUriDoesNotMatchExpectedPayloadKind(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), expectedPayloadKind.ToString()));
			}
			string selectQueryOption = this.parseResult.SelectQueryOption;
			if (selectQueryOption != null && odataPayloadKind != ODataPayloadKind.Feed && odataPayloadKind != ODataPayloadKind.Entry)
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidPayloadKindWithSelectQueryOption(expectedPayloadKind.ToString()));
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0001FCF4 File Offset: 0x0001DEF4
		private ODataPayloadKind ParseMetadataUriFragment(string fragment, ODataReaderBehavior readerBehavior, ODataVersion version)
		{
			int num = fragment.IndexOf('&');
			if (num > 0)
			{
				string text = fragment.Substring(num);
				this.parseResult.SelectQueryOption = ODataJsonLightMetadataUriParser.ExtractSelectQueryOption(text);
				fragment = fragment.Substring(0, num);
			}
			string[] parts = fragment.Split(new char[] { '/' });
			int num2 = parts.Length;
			EdmTypeResolver edmTypeResolver = new EdmTypeReaderResolver(this.model, readerBehavior, version);
			int num3 = fragment.IndexOf("$links", 4);
			ODataPayloadKind odataPayloadKind;
			if (num3 > -1)
			{
				odataPayloadKind = this.ParseAssociationLinks(edmTypeResolver, num2, parts, readerBehavior, version);
			}
			else
			{
				switch (num2)
				{
				case 1:
					if (fragment.Length == 0)
					{
						odataPayloadKind = ODataPayloadKind.ServiceDocument;
					}
					else if (parts[0].Equals("Edm.Null", 5))
					{
						odataPayloadKind = ODataPayloadKind.Property;
						this.parseResult.IsNullProperty = true;
					}
					else
					{
						IEdmEntitySet edmEntitySet = this.model.ResolveEntitySet(parts[0]);
						if (edmEntitySet != null)
						{
							this.parseResult.EntitySet = edmEntitySet;
							this.parseResult.EdmType = edmTypeResolver.GetElementType(edmEntitySet);
							odataPayloadKind = ODataPayloadKind.Feed;
						}
						else
						{
							this.parseResult.EdmType = this.ResolveType(parts[0], readerBehavior, version);
							odataPayloadKind = ((this.parseResult.EdmType is IEdmCollectionType) ? ODataPayloadKind.Collection : ODataPayloadKind.Property);
						}
					}
					break;
				case 2:
					odataPayloadKind = this.ResolveEntitySet(parts[0], delegate(IEdmEntitySet resolvedEntitySet)
					{
						IEdmEntityType elementType = edmTypeResolver.GetElementType(resolvedEntitySet);
						if (string.CompareOrdinal("@Element", parts[1]) == 0)
						{
							this.parseResult.EdmType = elementType;
							return ODataPayloadKind.Entry;
						}
						this.parseResult.EdmType = this.ResolveTypeCast(resolvedEntitySet, parts[1], readerBehavior, version, elementType);
						return ODataPayloadKind.Feed;
					});
					break;
				case 3:
					odataPayloadKind = this.ResolveEntitySet(parts[0], delegate(IEdmEntitySet resolvedEntitySet)
					{
						IEdmEntityType elementType2 = edmTypeResolver.GetElementType(resolvedEntitySet);
						this.parseResult.EdmType = this.ResolveTypeCast(resolvedEntitySet, parts[1], readerBehavior, version, elementType2);
						this.ValidateMetadataUriFragmentItemSelector(parts[2]);
						return ODataPayloadKind.Entry;
					});
					break;
				default:
					throw new ODataException(Strings.ODataJsonLightMetadataUriParser_FragmentWithInvalidNumberOfParts(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), num2, 3));
				}
			}
			return odataPayloadKind;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002016C File Offset: 0x0001E36C
		private ODataPayloadKind ParseAssociationLinks(EdmTypeResolver edmTypeResolver, int partCount, string[] parts, ODataReaderBehavior readerBehavior, ODataVersion version)
		{
			return this.ResolveEntitySet(parts[0], delegate(IEdmEntitySet resolvedEntitySet)
			{
				ODataPayloadKind odataPayloadKind;
				switch (partCount)
				{
				case 3:
				{
					if (string.CompareOrdinal("$links", parts[1]) != 0)
					{
						throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidAssociationLink(UriUtilsCommon.UriToString(this.parseResult.MetadataUri)));
					}
					IEdmNavigationProperty edmNavigationProperty = this.ResolveEntityReferenceLinkMetadataFragment(edmTypeResolver, resolvedEntitySet, null, parts[2], readerBehavior, version);
					odataPayloadKind = this.SetEntityLinkParseResults(edmNavigationProperty, null);
					break;
				}
				case 4:
					if (string.CompareOrdinal("$links", parts[1]) == 0)
					{
						IEdmNavigationProperty edmNavigationProperty = this.ResolveEntityReferenceLinkMetadataFragment(edmTypeResolver, resolvedEntitySet, null, parts[2], readerBehavior, version);
						this.ValidateLinkMetadataUriFragmentItemSelector(parts[3]);
						odataPayloadKind = this.SetEntityLinkParseResults(edmNavigationProperty, parts[3]);
					}
					else
					{
						if (string.CompareOrdinal("$links", parts[2]) != 0)
						{
							throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidAssociationLink(UriUtilsCommon.UriToString(this.parseResult.MetadataUri)));
						}
						IEdmNavigationProperty edmNavigationProperty = this.ResolveEntityReferenceLinkMetadataFragment(edmTypeResolver, resolvedEntitySet, parts[1], parts[3], readerBehavior, version);
						odataPayloadKind = this.SetEntityLinkParseResults(edmNavigationProperty, null);
					}
					break;
				case 5:
				{
					if (string.CompareOrdinal("$links", parts[2]) != 0)
					{
						throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidAssociationLink(UriUtilsCommon.UriToString(this.parseResult.MetadataUri)));
					}
					IEdmNavigationProperty edmNavigationProperty = this.ResolveEntityReferenceLinkMetadataFragment(edmTypeResolver, resolvedEntitySet, parts[1], parts[3], readerBehavior, version);
					this.ValidateLinkMetadataUriFragmentItemSelector(parts[2]);
					odataPayloadKind = this.SetEntityLinkParseResults(edmNavigationProperty, parts[4]);
					break;
				}
				default:
					throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidAssociationLink(UriUtilsCommon.UriToString(this.parseResult.MetadataUri)));
				}
				return odataPayloadKind;
			});
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000201C8 File Offset: 0x0001E3C8
		private ODataPayloadKind SetEntityLinkParseResults(IEdmNavigationProperty navigationProperty, string singleElement)
		{
			this.parseResult.NavigationProperty = navigationProperty;
			ODataPayloadKind odataPayloadKind = (navigationProperty.Type.IsCollection() ? ODataPayloadKind.EntityReferenceLinks : ODataPayloadKind.EntityReferenceLink);
			if (singleElement != null && string.CompareOrdinal("@Element", singleElement) == 0)
			{
				if (!navigationProperty.Type.IsCollection())
				{
					throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidSingletonNavPropertyForEntityReferenceLinkUri(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), navigationProperty.Name, singleElement));
				}
				odataPayloadKind = ODataPayloadKind.EntityReferenceLink;
			}
			return odataPayloadKind;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00020240 File Offset: 0x0001E440
		private IEdmNavigationProperty ResolveEntityReferenceLinkMetadataFragment(EdmTypeResolver edmTypeResolver, IEdmEntitySet entitySet, string typeName, string propertyName, ODataReaderBehavior readerBehavior, ODataVersion version)
		{
			IEdmEntityType edmEntityType = edmTypeResolver.GetElementType(entitySet);
			if (typeName != null)
			{
				edmEntityType = this.ResolveTypeCast(entitySet, typeName, readerBehavior, version, edmEntityType);
			}
			return this.ResolveNavigationProperty(edmEntityType, propertyName);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00020271 File Offset: 0x0001E471
		private void ValidateLinkMetadataUriFragmentItemSelector(string elementSelector)
		{
			if (string.CompareOrdinal("@Element", elementSelector) != 0)
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidEntityReferenceLinkUriSuffix(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), elementSelector, "@Element"));
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x000202A1 File Offset: 0x0001E4A1
		private void ValidateMetadataUriFragmentItemSelector(string elementSelector)
		{
			if (string.CompareOrdinal("@Element", elementSelector) != 0)
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidEntityWithTypeCastUriSuffix(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), elementSelector, "@Element"));
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x000202D4 File Offset: 0x0001E4D4
		private IEdmNavigationProperty ResolveNavigationProperty(IEdmEntityType entityType, string navigationPropertyName)
		{
			IEdmProperty edmProperty = entityType.FindProperty(navigationPropertyName);
			IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
			if (edmNavigationProperty == null)
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidPropertyForEntityReferenceLinkUri(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), navigationPropertyName));
			}
			return edmNavigationProperty;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00020314 File Offset: 0x0001E514
		private ODataPayloadKind ResolveEntitySet(string entitySetPart, Func<IEdmEntitySet, ODataPayloadKind> resolvedEntitySet)
		{
			IEdmEntitySet edmEntitySet = this.model.ResolveEntitySet(entitySetPart);
			if (edmEntitySet != null)
			{
				this.parseResult.EntitySet = edmEntitySet;
				return resolvedEntitySet.Invoke(edmEntitySet);
			}
			throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidEntitySetName(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), entitySetPart));
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00020360 File Offset: 0x0001E560
		private IEdmEntityType ResolveTypeCast(IEdmEntitySet entitySet, string typeCast, ODataReaderBehavior readerBehavior, ODataVersion version, IEdmEntityType entitySetElementType)
		{
			IEdmEntityType edmEntityType = entitySetElementType;
			if (!string.IsNullOrEmpty(typeCast))
			{
				EdmTypeKind edmTypeKind;
				edmEntityType = MetadataUtils.ResolveTypeNameForRead(this.model, null, typeCast, readerBehavior, version, out edmTypeKind) as IEdmEntityType;
				if (edmEntityType == null)
				{
					throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidEntityTypeInTypeCast(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), typeCast));
				}
				if (!entitySetElementType.IsAssignableFrom(edmEntityType))
				{
					throw new ODataException(Strings.ODataJsonLightMetadataUriParser_IncompatibleEntityTypeInTypeCast(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), typeCast, entitySetElementType.FullName(), entitySet.FullName()));
				}
			}
			return edmEntityType;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000203E4 File Offset: 0x0001E5E4
		private IEdmType ResolveType(string typeName, ODataReaderBehavior readerBehavior, ODataVersion version)
		{
			string text = EdmLibraryExtensions.GetCollectionItemTypeName(typeName) ?? typeName;
			EdmTypeKind edmTypeKind;
			IEdmType edmType = MetadataUtils.ResolveTypeNameForRead(this.model, null, text, readerBehavior, version, out edmTypeKind);
			if (edmType == null || (edmType.TypeKind != EdmTypeKind.Primitive && edmType.TypeKind != EdmTypeKind.Complex))
			{
				throw new ODataException(Strings.ODataJsonLightMetadataUriParser_InvalidEntitySetNameOrTypeName(UriUtilsCommon.UriToString(this.parseResult.MetadataUri), typeName));
			}
			return (text == typeName) ? edmType : EdmLibraryExtensions.GetCollectionType(edmType.ToTypeReference(true));
		}

		// Token: 0x040003A5 RID: 933
		private static readonly string SelectQueryOptionStart = "$select" + '=';

		// Token: 0x040003A6 RID: 934
		private readonly IEdmModel model;

		// Token: 0x040003A7 RID: 935
		private readonly ODataJsonLightMetadataUriParseResult parseResult;
	}
}
