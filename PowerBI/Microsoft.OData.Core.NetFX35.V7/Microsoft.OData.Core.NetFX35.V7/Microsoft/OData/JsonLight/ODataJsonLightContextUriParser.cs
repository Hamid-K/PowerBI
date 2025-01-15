using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000204 RID: 516
	internal sealed class ODataJsonLightContextUriParser
	{
		// Token: 0x060013F6 RID: 5110 RVA: 0x00038FCE File Offset: 0x000371CE
		private ODataJsonLightContextUriParser(IEdmModel model, Uri contextUriFromPayload)
		{
			if (!model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonLightContextUriParser_NoModel);
			}
			this.model = model;
			this.parseResult = new ODataJsonLightContextUriParseResult(contextUriFromPayload);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00038FFC File Offset: 0x000371FC
		internal static ODataJsonLightContextUriParseResult Parse(IEdmModel model, string contextUriFromPayload, ODataPayloadKind payloadKind, Func<IEdmType, string, IEdmType> clientCustomTypeResolver, bool needParseFragment, bool throwIfMetadataConflict = true)
		{
			if (contextUriFromPayload == null)
			{
				throw new ODataException(Strings.ODataJsonLightContextUriParser_NullMetadataDocumentUri);
			}
			Uri uri;
			if (!Uri.TryCreate(contextUriFromPayload, 1, ref uri))
			{
				throw new ODataException(Strings.ODataJsonLightContextUriParser_TopLevelContextUrlShouldBeAbsolute(contextUriFromPayload));
			}
			ODataJsonLightContextUriParser odataJsonLightContextUriParser = new ODataJsonLightContextUriParser(model, uri);
			odataJsonLightContextUriParser.TokenizeContextUri();
			if (needParseFragment)
			{
				odataJsonLightContextUriParser.ParseContextUri(payloadKind, clientCustomTypeResolver, throwIfMetadataConflict);
			}
			return odataJsonLightContextUriParser.parseResult;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00014BAB File Offset: 0x00012DAB
		private static string ExtractSelectQueryOption(string fragment)
		{
			return fragment;
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00039050 File Offset: 0x00037250
		private void TokenizeContextUri()
		{
			Uri contextUri = this.parseResult.ContextUri;
			UriBuilder uriBuilder = new UriBuilder(contextUri)
			{
				Fragment = null
			};
			this.parseResult.MetadataDocumentUri = uriBuilder.Uri;
			this.parseResult.Fragment = contextUri.GetComponents(64, 3);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0003909C File Offset: 0x0003729C
		private void ParseContextUri(ODataPayloadKind expectedPayloadKind, Func<IEdmType, string, IEdmType> clientCustomTypeResolver, bool throwIfMetadataConflict)
		{
			bool flag;
			ODataPayloadKind odataPayloadKind = this.ParseContextUriFragment(this.parseResult.Fragment, clientCustomTypeResolver, throwIfMetadataConflict, out flag);
			bool flag2 = odataPayloadKind == expectedPayloadKind || expectedPayloadKind == ODataPayloadKind.Unsupported;
			IEdmType edmType = this.parseResult.EdmType;
			if (edmType != null && edmType.TypeKind == EdmTypeKind.Untyped)
			{
				if (string.Equals(edmType.FullTypeName(), "Edm.Untyped", 4))
				{
					this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
					{
						ODataPayloadKind.ResourceSet,
						ODataPayloadKind.Property,
						ODataPayloadKind.Collection,
						ODataPayloadKind.Resource
					};
					flag2 = true;
				}
				else if (expectedPayloadKind == ODataPayloadKind.Property || expectedPayloadKind == ODataPayloadKind.Resource)
				{
					this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
					{
						ODataPayloadKind.Property,
						ODataPayloadKind.Resource
					};
					flag2 = true;
				}
			}
			else if (edmType != null && edmType.TypeKind == EdmTypeKind.Collection && ((IEdmCollectionType)edmType).ElementType.TypeKind() == EdmTypeKind.Untyped)
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					ODataPayloadKind.ResourceSet,
					ODataPayloadKind.Property,
					ODataPayloadKind.Collection
				};
				if (expectedPayloadKind == ODataPayloadKind.ResourceSet || expectedPayloadKind == ODataPayloadKind.Property || expectedPayloadKind == ODataPayloadKind.Collection)
				{
					flag2 = true;
				}
			}
			else if (odataPayloadKind == ODataPayloadKind.ResourceSet && edmType.IsODataComplexTypeKind())
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					ODataPayloadKind.ResourceSet,
					ODataPayloadKind.Property,
					ODataPayloadKind.Collection
				};
				if (expectedPayloadKind == ODataPayloadKind.Property || expectedPayloadKind == ODataPayloadKind.Collection)
				{
					flag2 = true;
				}
			}
			else if (odataPayloadKind == ODataPayloadKind.Resource && edmType.IsODataComplexTypeKind())
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					ODataPayloadKind.Resource,
					ODataPayloadKind.Property
				};
				if (expectedPayloadKind == ODataPayloadKind.Property)
				{
					flag2 = true;
				}
			}
			else if (odataPayloadKind == ODataPayloadKind.Collection)
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					ODataPayloadKind.Collection,
					ODataPayloadKind.Property
				};
				if (expectedPayloadKind == ODataPayloadKind.Property)
				{
					flag2 = true;
				}
			}
			else if (odataPayloadKind == ODataPayloadKind.Resource)
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					ODataPayloadKind.Resource,
					ODataPayloadKind.Delta
				};
				if (expectedPayloadKind == ODataPayloadKind.Delta)
				{
					this.parseResult.DeltaKind = ODataDeltaKind.Resource;
					flag2 = true;
				}
			}
			else if (odataPayloadKind == ODataPayloadKind.Property && flag && (expectedPayloadKind == ODataPayloadKind.Resource || expectedPayloadKind == ODataPayloadKind.ResourceSet))
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					expectedPayloadKind,
					ODataPayloadKind.Property
				};
				flag2 = true;
			}
			else
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[] { odataPayloadKind };
			}
			if (!flag2)
			{
				throw new ODataException(Strings.ODataJsonLightContextUriParser_ContextUriDoesNotMatchExpectedPayloadKind(UriUtils.UriToString(this.parseResult.ContextUri), expectedPayloadKind.ToString()));
			}
			string selectQueryOption = this.parseResult.SelectQueryOption;
			if (selectQueryOption != null && odataPayloadKind != ODataPayloadKind.ResourceSet && odataPayloadKind != ODataPayloadKind.Resource && odataPayloadKind != ODataPayloadKind.Delta)
			{
				throw new ODataException(Strings.ODataJsonLightContextUriParser_InvalidPayloadKindWithSelectQueryOption(expectedPayloadKind.ToString()));
			}
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x000392F8 File Offset: 0x000374F8
		[SuppressMessage("Microsoft.Maintainability", "CA1502", Justification = "Will be moving to non case statements later, no point in investing in reducing this now")]
		private ODataPayloadKind ParseContextUriFragment(string fragment, Func<IEdmType, string, IEdmType> clientCustomTypeResolver, bool throwIfMetadataConflict, out bool isUndeclared)
		{
			bool flag = false;
			ODataDeltaKind odataDeltaKind = ODataDeltaKind.None;
			isUndeclared = false;
			if (fragment.EndsWith("/$entity", 4))
			{
				flag = true;
				fragment = fragment.Substring(0, fragment.Length - "/$entity".Length);
			}
			else if (fragment.EndsWith("/$delta", 4))
			{
				odataDeltaKind = ODataDeltaKind.ResourceSet;
				fragment = fragment.Substring(0, fragment.Length - "/$delta".Length);
			}
			else if (fragment.EndsWith("/$deletedEntity", 4))
			{
				odataDeltaKind = ODataDeltaKind.DeletedEntry;
				fragment = fragment.Substring(0, fragment.Length - "/$deletedEntity".Length);
			}
			else if (fragment.EndsWith("/$link", 4))
			{
				odataDeltaKind = ODataDeltaKind.Link;
				fragment = fragment.Substring(0, fragment.Length - "/$link".Length);
			}
			else if (fragment.EndsWith("/$deletedLink", 4))
			{
				odataDeltaKind = ODataDeltaKind.DeletedLink;
				fragment = fragment.Substring(0, fragment.Length - "/$deletedLink".Length);
			}
			this.parseResult.DeltaKind = odataDeltaKind;
			if (fragment.EndsWith(")", 4))
			{
				int num = fragment.Length - 2;
				int num2 = 1;
				while (num2 > 0 && num > 0)
				{
					char c = fragment.get_Chars(num);
					if (c != '(')
					{
						if (c == ')')
						{
							num2++;
						}
					}
					else
					{
						num2--;
					}
					num--;
				}
				if (num == 0)
				{
					throw new ODataException(Strings.ODataJsonLightContextUriParser_InvalidContextUrl(UriUtils.UriToString(this.parseResult.ContextUri)));
				}
				string text = fragment.Substring(0, num + 1);
				if (!text.Equals("Collection"))
				{
					string text2 = fragment.Substring(num + 2);
					text2 = text2.Substring(0, text2.Length - 1);
					if (ODataJsonLightContextUriParser.KeyPattern.IsMatch(text2))
					{
						throw new ODataException(Strings.ODataJsonLightContextUriParser_LastSegmentIsKeySegment(UriUtils.UriToString(this.parseResult.ContextUri)));
					}
					this.parseResult.SelectQueryOption = ODataJsonLightContextUriParser.ExtractSelectQueryOption(text2);
					fragment = text;
				}
			}
			EdmTypeResolver edmTypeResolver = new EdmTypeReaderResolver(this.model, clientCustomTypeResolver);
			ODataPayloadKind odataPayloadKind;
			if (!fragment.Contains("/") && !flag && odataDeltaKind == ODataDeltaKind.None)
			{
				if (fragment.Length == 0)
				{
					odataPayloadKind = ODataPayloadKind.ServiceDocument;
				}
				else if (fragment.Equals("Collection($ref)"))
				{
					odataPayloadKind = ODataPayloadKind.EntityReferenceLinks;
				}
				else if (fragment.Equals("$ref"))
				{
					odataPayloadKind = ODataPayloadKind.EntityReferenceLink;
				}
				else
				{
					IEdmNavigationSource edmNavigationSource = this.model.FindDeclaredNavigationSource(fragment);
					if (edmNavigationSource != null)
					{
						this.parseResult.NavigationSource = edmNavigationSource;
						this.parseResult.EdmType = edmTypeResolver.GetElementType(edmNavigationSource);
						odataPayloadKind = ((edmNavigationSource is IEdmSingleton) ? ODataPayloadKind.Resource : ODataPayloadKind.ResourceSet);
					}
					else
					{
						odataPayloadKind = this.ResolveType(fragment, clientCustomTypeResolver, throwIfMetadataConflict);
					}
				}
			}
			else
			{
				string text3 = UriUtils.UriToString(this.parseResult.MetadataDocumentUri);
				if (!text3.EndsWith("$metadata", 4))
				{
					throw new ODataException(Strings.ODataJsonLightContextUriParser_InvalidContextUrl(UriUtils.UriToString(this.parseResult.ContextUri)));
				}
				Uri uri = new Uri(text3.Substring(0, text3.Length - "$metadata".Length));
				ODataUriParser odataUriParser = new ODataUriParser(this.model, uri, new Uri(uri, fragment));
				ODataPath odataPath;
				try
				{
					odataPath = odataUriParser.ParsePath();
				}
				catch (ODataException)
				{
					throw new ODataException(Strings.ODataJsonLightContextUriParser_InvalidContextUrl(UriUtils.UriToString(this.parseResult.ContextUri)));
				}
				if (odataPath.Count == 0)
				{
					throw new ODataException(Strings.ODataJsonLightContextUriParser_InvalidContextUrl(UriUtils.UriToString(this.parseResult.ContextUri)));
				}
				this.parseResult.Path = odataPath;
				this.parseResult.NavigationSource = odataPath.NavigationSource();
				this.parseResult.EdmType = odataPath.LastSegment.EdmType;
				ODataPathSegment lastSegment = odataPath.TrimEndingTypeSegment().LastSegment;
				if (lastSegment is EntitySetSegment || lastSegment is NavigationPropertySegment)
				{
					if (odataDeltaKind != ODataDeltaKind.None)
					{
						odataPayloadKind = ODataPayloadKind.Delta;
					}
					else
					{
						odataPayloadKind = (flag ? ODataPayloadKind.Resource : ODataPayloadKind.ResourceSet);
					}
					if (this.parseResult.EdmType is IEdmCollectionType)
					{
						IEdmCollectionTypeReference edmCollectionTypeReference = this.parseResult.EdmType.ToTypeReference().AsCollection();
						if (edmCollectionTypeReference != null)
						{
							this.parseResult.EdmType = edmCollectionTypeReference.ElementType().Definition;
						}
					}
				}
				else if (lastSegment is SingletonSegment)
				{
					odataPayloadKind = ODataPayloadKind.Resource;
				}
				else
				{
					if (!odataPath.IsIndividualProperty())
					{
						throw new ODataException(Strings.ODataJsonLightContextUriParser_InvalidContextUrl(UriUtils.UriToString(this.parseResult.ContextUri)));
					}
					isUndeclared = odataPath.IsUndeclared();
					odataPayloadKind = ODataPayloadKind.Property;
					IEdmComplexType edmComplexType = this.parseResult.EdmType as IEdmComplexType;
					if (edmComplexType != null)
					{
						odataPayloadKind = ODataPayloadKind.Resource;
					}
					else
					{
						IEdmCollectionType edmCollectionType = this.parseResult.EdmType as IEdmCollectionType;
						if (edmCollectionType != null)
						{
							if (edmCollectionType.ElementType.IsComplex())
							{
								this.parseResult.EdmType = edmCollectionType.ElementType.Definition;
								odataPayloadKind = ODataPayloadKind.ResourceSet;
							}
							else
							{
								odataPayloadKind = ODataPayloadKind.Collection;
							}
						}
					}
				}
			}
			return odataPayloadKind;
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x000397C4 File Offset: 0x000379C4
		private ODataPayloadKind ResolveType(string typeName, Func<IEdmType, string, IEdmType> clientCustomTypeResolver, bool throwIfMetadataConflict)
		{
			string text = EdmLibraryExtensions.GetCollectionItemTypeName(typeName) ?? typeName;
			bool flag = text != typeName;
			EdmTypeKind edmTypeKind;
			IEdmType edmType = MetadataUtils.ResolveTypeNameForRead(this.model, null, text, clientCustomTypeResolver, out edmTypeKind);
			if (edmType == null && !throwIfMetadataConflict)
			{
				string text2;
				string text3;
				TypeUtils.ParseQualifiedTypeName(typeName, out text2, out text3, out flag);
				edmType = new EdmUntypedStructuredType(text2, text3);
			}
			if (edmType == null || (edmType.TypeKind != EdmTypeKind.Primitive && edmType.TypeKind != EdmTypeKind.Enum && edmType.TypeKind != EdmTypeKind.Complex && edmType.TypeKind != EdmTypeKind.Entity && edmType.TypeKind != EdmTypeKind.TypeDefinition && edmType.TypeKind != EdmTypeKind.Untyped))
			{
				throw new ODataException(Strings.ODataJsonLightContextUriParser_InvalidEntitySetNameOrTypeName(UriUtils.UriToString(this.parseResult.ContextUri), typeName));
			}
			if (edmType.TypeKind == EdmTypeKind.Entity || edmType.TypeKind == EdmTypeKind.Complex)
			{
				this.parseResult.EdmType = edmType;
				if (!flag)
				{
					return ODataPayloadKind.Resource;
				}
				return ODataPayloadKind.ResourceSet;
			}
			else
			{
				IEdmType edmType2;
				if (!flag)
				{
					edmType2 = edmType;
				}
				else
				{
					IEdmType collectionType = EdmLibraryExtensions.GetCollectionType(edmType.ToTypeReference(true));
					edmType2 = collectionType;
				}
				edmType = edmType2;
				this.parseResult.EdmType = edmType;
				if (!flag)
				{
					return ODataPayloadKind.Property;
				}
				return ODataPayloadKind.Collection;
			}
		}

		// Token: 0x04000A0A RID: 2570
		private static readonly Regex KeyPattern = new Regex("^(?:-{0,1}\\d+?|\\w*'.+?'|[A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-F0-9]{12}|.+?=.+?)$", 1);

		// Token: 0x04000A0B RID: 2571
		private readonly IEdmModel model;

		// Token: 0x04000A0C RID: 2572
		private readonly ODataJsonLightContextUriParseResult parseResult;
	}
}
