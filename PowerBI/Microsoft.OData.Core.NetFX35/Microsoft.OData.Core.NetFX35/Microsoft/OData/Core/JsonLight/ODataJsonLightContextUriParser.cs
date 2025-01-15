using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000C2 RID: 194
	internal sealed class ODataJsonLightContextUriParser
	{
		// Token: 0x06000702 RID: 1794 RVA: 0x00018E29 File Offset: 0x00017029
		private ODataJsonLightContextUriParser(IEdmModel model, Uri contextUriFromPayload)
		{
			if (!model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonLightContextUriParser_NoModel);
			}
			this.model = model;
			this.parseResult = new ODataJsonLightContextUriParseResult(contextUriFromPayload);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00018E58 File Offset: 0x00017058
		internal static ODataJsonLightContextUriParseResult Parse(IEdmModel model, string contextUriFromPayload, ODataPayloadKind payloadKind, ODataReaderBehavior readerBehavior, bool needParseFragment)
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
				odataJsonLightContextUriParser.ParseContextUri(payloadKind, readerBehavior);
			}
			return odataJsonLightContextUriParser.parseResult;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00018EAA File Offset: 0x000170AA
		private static string ExtractSelectQueryOption(string fragment)
		{
			return fragment;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00018EB0 File Offset: 0x000170B0
		private void TokenizeContextUri()
		{
			Uri contextUri = this.parseResult.ContextUri;
			UriBuilder uriBuilder = new UriBuilder(contextUri)
			{
				Fragment = null
			};
			this.parseResult.MetadataDocumentUri = uriBuilder.Uri;
			this.parseResult.Fragment = contextUri.GetComponents(64, 2);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00018F00 File Offset: 0x00017100
		private void ParseContextUri(ODataPayloadKind expectedPayloadKind, ODataReaderBehavior readerBehavior)
		{
			ODataPayloadKind odataPayloadKind = this.ParseContextUriFragment(this.parseResult.Fragment, readerBehavior);
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
			else if (odataPayloadKind == ODataPayloadKind.Entry)
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					ODataPayloadKind.Entry,
					ODataPayloadKind.Delta
				};
				if (expectedPayloadKind == ODataPayloadKind.Delta)
				{
					this.parseResult.DeltaKind = ODataDeltaKind.Entry;
					flag = true;
				}
			}
			else
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[] { odataPayloadKind };
			}
			if (!flag)
			{
				throw new ODataException(Strings.ODataJsonLightContextUriParser_ContextUriDoesNotMatchExpectedPayloadKind(UriUtils.UriToString(this.parseResult.ContextUri), expectedPayloadKind.ToString()));
			}
			string selectQueryOption = this.parseResult.SelectQueryOption;
			if (selectQueryOption != null && odataPayloadKind != ODataPayloadKind.Feed && odataPayloadKind != ODataPayloadKind.Entry && odataPayloadKind != ODataPayloadKind.Delta)
			{
				throw new ODataException(Strings.ODataJsonLightContextUriParser_InvalidPayloadKindWithSelectQueryOption(expectedPayloadKind.ToString()));
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00019004 File Offset: 0x00017204
		[SuppressMessage("Microsoft.Maintainability", "CA1502", Justification = "Will be moving to non case statements later, no point in investing in reducing this now")]
		private ODataPayloadKind ParseContextUriFragment(string fragment, ODataReaderBehavior readerBehavior)
		{
			bool flag = false;
			ODataDeltaKind odataDeltaKind = ODataDeltaKind.None;
			if (fragment.EndsWith("/$entity", 4))
			{
				flag = true;
				fragment = fragment.Substring(0, fragment.Length - "/$entity".Length);
			}
			else if (fragment.EndsWith("/$delta", 4))
			{
				odataDeltaKind = ODataDeltaKind.Feed;
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
					switch (fragment.get_Chars(num))
					{
					case '(':
						num2--;
						break;
					case ')':
						num2++;
						break;
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
			EdmTypeResolver edmTypeResolver = new EdmTypeReaderResolver(this.model, readerBehavior);
			ODataPayloadKind odataPayloadKind;
			if (!fragment.Contains("/") && !flag && odataDeltaKind == ODataDeltaKind.None)
			{
				if (fragment.Length == 0)
				{
					odataPayloadKind = ODataPayloadKind.ServiceDocument;
				}
				else if (fragment.Equals("Edm.Null", 5))
				{
					odataPayloadKind = ODataPayloadKind.Property;
					this.parseResult.IsNullProperty = true;
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
						odataPayloadKind = ((edmNavigationSource is IEdmSingleton) ? ODataPayloadKind.Entry : ODataPayloadKind.Feed);
					}
					else
					{
						odataPayloadKind = this.ResolveType(fragment, readerBehavior);
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
						odataPayloadKind = (flag ? ODataPayloadKind.Entry : ODataPayloadKind.Feed);
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
					odataPayloadKind = ODataPayloadKind.Entry;
				}
				else
				{
					if (!odataPath.IsIndividualProperty())
					{
						throw new ODataException(Strings.ODataJsonLightContextUriParser_InvalidContextUrl(UriUtils.UriToString(this.parseResult.ContextUri)));
					}
					odataPayloadKind = ODataPayloadKind.Property;
					IEdmCollectionType edmCollectionType = this.parseResult.EdmType as IEdmCollectionType;
					if (edmCollectionType != null)
					{
						odataPayloadKind = ODataPayloadKind.Collection;
					}
				}
			}
			return odataPayloadKind;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001949C File Offset: 0x0001769C
		private ODataPayloadKind ResolveType(string typeName, ODataReaderBehavior readerBehavior)
		{
			string text = EdmLibraryExtensions.GetCollectionItemTypeName(typeName) ?? typeName;
			bool flag = text != typeName;
			EdmTypeKind edmTypeKind;
			IEdmType edmType = MetadataUtils.ResolveTypeNameForRead(this.model, null, text, readerBehavior, out edmTypeKind);
			if (edmType == null || (edmType.TypeKind != EdmTypeKind.Primitive && edmType.TypeKind != EdmTypeKind.Enum && edmType.TypeKind != EdmTypeKind.Complex && edmType.TypeKind != EdmTypeKind.Entity && edmType.TypeKind != EdmTypeKind.TypeDefinition))
			{
				throw new ODataException(Strings.ODataJsonLightContextUriParser_InvalidEntitySetNameOrTypeName(UriUtils.UriToString(this.parseResult.ContextUri), typeName));
			}
			if (edmType.TypeKind == EdmTypeKind.Entity)
			{
				this.parseResult.EdmType = edmType;
				if (!flag)
				{
					return ODataPayloadKind.Entry;
				}
				return ODataPayloadKind.Feed;
			}
			else
			{
				edmType = (flag ? EdmLibraryExtensions.GetCollectionType(edmType.ToTypeReference(true)) : edmType);
				this.parseResult.EdmType = edmType;
				if (!flag)
				{
					return ODataPayloadKind.Property;
				}
				return ODataPayloadKind.Collection;
			}
		}

		// Token: 0x04000339 RID: 825
		private static readonly Regex KeyPattern = new Regex("^(?:-{0,1}\\d+?|\\w*'.+?'|[A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-F0-9]{12}|.+?=.+?)$", 1);

		// Token: 0x0400033A RID: 826
		private readonly IEdmModel model;

		// Token: 0x0400033B RID: 827
		private readonly ODataJsonLightContextUriParseResult parseResult;
	}
}
