using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.ContextUrl
{
	// Token: 0x02000832 RID: 2098
	internal class ODataJsonLightContextUriParser
	{
		// Token: 0x06003C58 RID: 15448 RVA: 0x000C3A24 File Offset: 0x000C1C24
		public ODataJsonLightContextUriParser(Microsoft.OData.Edm.IEdmModel model, Uri contextUri)
		{
			this.model = model;
			this.contextUri = contextUri;
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x000C3A3C File Offset: 0x000C1C3C
		public static ODataJsonLightContextUriParseResult Parse(Microsoft.OData.Edm.IEdmModel model, string contextUriFromPayload, bool needParseFragment)
		{
			Uri uri = new Uri(contextUriFromPayload, UriKind.Absolute);
			ODataJsonLightContextUriParser odataJsonLightContextUriParser = new ODataJsonLightContextUriParser(model, uri);
			odataJsonLightContextUriParser.TokenizeContextUri();
			if (needParseFragment)
			{
				odataJsonLightContextUriParser.ParseContextUri();
			}
			if (odataJsonLightContextUriParser.parseResult.SelectQueryOption != null)
			{
				odataJsonLightContextUriParser.parseResult.SelectItems = ODataContextUrlSelectListParser.Parse("(" + odataJsonLightContextUriParser.parseResult.SelectQueryOption + ")", contextUriFromPayload);
			}
			return odataJsonLightContextUriParser.parseResult;
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x000C3AA8 File Offset: 0x000C1CA8
		private void ParseContextUri()
		{
			ODataPayloadKind odataPayloadKind = this.ParseContextUriFragment(this.parseResult.Fragment);
			Microsoft.OData.Edm.IEdmType edmType = this.parseResult.EdmType;
			if (odataPayloadKind == ODataPayloadKind.ResourceSet && edmType.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Complex)
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					ODataPayloadKind.ResourceSet,
					ODataPayloadKind.Property,
					ODataPayloadKind.Collection
				};
			}
			else if (odataPayloadKind == ODataPayloadKind.Resource && edmType.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Complex)
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					ODataPayloadKind.Resource,
					ODataPayloadKind.Property
				};
			}
			else if (odataPayloadKind == ODataPayloadKind.Collection)
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					ODataPayloadKind.Collection,
					ODataPayloadKind.Property
				};
			}
			else if (odataPayloadKind == ODataPayloadKind.Resource)
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					ODataPayloadKind.Resource,
					ODataPayloadKind.Delta
				};
			}
			else
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[] { odataPayloadKind };
			}
			if (this.parseResult.SelectQueryOption != null && odataPayloadKind != ODataPayloadKind.ResourceSet && odataPayloadKind != ODataPayloadKind.Resource && odataPayloadKind != ODataPayloadKind.Delta)
			{
				throw new ODataException(Strings.ODataJsonLightContextUriInvalid(ODataJsonLightContextUriParser.UriToString(this.contextUri)));
			}
			this.parseResult.PayloadKind = this.ResolveToPayloadKind();
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x000C3BBC File Offset: 0x000C1DBC
		private void TokenizeContextUri()
		{
			this.parseResult = new ODataJsonLightContextUriParseResult(this.contextUri);
			Uri uri = this.parseResult.ContextUri;
			UriBuilder uriBuilder = new UriBuilder(this.parseResult.ContextUri)
			{
				Fragment = null
			};
			this.parseResult.MetadataDocumentUri = uriBuilder.Uri;
			this.parseResult.Fragment = uri.GetComponents(UriComponents.Fragment, UriFormat.SafeUnescaped);
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x000C3C24 File Offset: 0x000C1E24
		private ODataPayloadKind ResolveToPayloadKind()
		{
			ODataPayloadKind[] detectedPayloadKinds = this.parseResult.DetectedPayloadKinds;
			if (detectedPayloadKinds.Length >= 1)
			{
				return detectedPayloadKinds[0];
			}
			return ODataPayloadKind.Unsupported;
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x000C3C4C File Offset: 0x000C1E4C
		private ODataPayloadKind ParseContextUriFragment(string fragment)
		{
			bool flag = false;
			ODataDeltaKind odataDeltaKind = ODataDeltaKind.None;
			if (fragment.EndsWith("/$entity", StringComparison.Ordinal))
			{
				flag = true;
				fragment = fragment.Substring(0, fragment.Length - "/$entity".Length);
			}
			else if (fragment.EndsWith(ODataJsonLightContextUriParser.DeltaFragmentIdentifier, StringComparison.Ordinal))
			{
				odataDeltaKind = ODataDeltaKind.ResourceSet;
				fragment = fragment.Substring(0, fragment.Length - ODataJsonLightContextUriParser.DeltaFragmentIdentifier.Length);
			}
			else if (fragment.EndsWith(ODataJsonLightContextUriParser.DeletedEntityFragmentIdentifier, StringComparison.Ordinal))
			{
				odataDeltaKind = ODataDeltaKind.DeletedEntry;
				fragment = fragment.Substring(0, fragment.Length - ODataJsonLightContextUriParser.DeletedEntityFragmentIdentifier.Length);
			}
			else if (fragment.EndsWith(ODataJsonLightContextUriParser.LinkFragmentIdentifier, StringComparison.Ordinal))
			{
				odataDeltaKind = ODataDeltaKind.Link;
				fragment = fragment.Substring(0, fragment.Length - ODataJsonLightContextUriParser.LinkFragmentIdentifier.Length);
			}
			else if (fragment.EndsWith(ODataJsonLightContextUriParser.DeletedLinkFragmentIdentifier, StringComparison.Ordinal))
			{
				odataDeltaKind = ODataDeltaKind.DeletedLink;
				fragment = fragment.Substring(0, fragment.Length - ODataJsonLightContextUriParser.DeletedLinkFragmentIdentifier.Length);
			}
			this.parseResult.DeltaKind = odataDeltaKind;
			if (fragment.EndsWith(")", StringComparison.Ordinal))
			{
				int num = fragment.Length - 2;
				int num2 = 1;
				while (num2 > 0 && num > 0)
				{
					char c = fragment[num];
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
					throw new ODataException(Strings.ODataJsonLightContextUriInvalid(ODataJsonLightContextUriParser.UriToString(this.contextUri)));
				}
				string text = fragment.Substring(0, num + 1);
				if (!text.Equals("Collection", StringComparison.Ordinal))
				{
					string text2 = fragment.Substring(num + 2);
					text2 = text2.Substring(0, text2.Length - 1);
					if (ODataJsonLightContextUriParser.KeyPattern.IsMatch(text2))
					{
						throw new ODataException(Strings.ODataJsonLightContextUriInvalid(ODataJsonLightContextUriParser.UriToString(this.contextUri)));
					}
					this.parseResult.SelectQueryOption = ODataJsonLightContextUriParser.ExtractSelectQueryOption(text2);
					fragment = text;
				}
			}
			ODataPayloadKind odataPayloadKind;
			if (!fragment.Contains("/") && !flag && odataDeltaKind == ODataDeltaKind.None)
			{
				if (fragment.Length == 0)
				{
					odataPayloadKind = ODataPayloadKind.ServiceDocument;
				}
				else if (fragment.Equals("Collection($ref)", StringComparison.Ordinal))
				{
					odataPayloadKind = ODataPayloadKind.EntityReferenceLinks;
				}
				else if (fragment.Equals("$ref", StringComparison.Ordinal))
				{
					odataPayloadKind = ODataPayloadKind.EntityReferenceLink;
				}
				else
				{
					this.parseResult.NavigationSource = this.model.FindDeclaredNavigationSource(fragment);
					if (this.parseResult.NavigationSource != null)
					{
						this.parseResult.EdmType = this.parseResult.NavigationSource.EntityType();
						odataPayloadKind = ((this.parseResult.NavigationSource is Microsoft.OData.Edm.IEdmSingleton) ? ODataPayloadKind.Resource : ODataPayloadKind.ResourceSet);
					}
					else
					{
						odataPayloadKind = this.ResolveType(fragment);
					}
				}
			}
			else
			{
				string text3 = ODataJsonLightContextUriParser.UriToString(this.parseResult.MetadataDocumentUri);
				if (!text3.EndsWith("$metadata", StringComparison.Ordinal))
				{
					throw new ODataException(Strings.ODataJsonLightContextUriInvalid(ODataJsonLightContextUriParser.UriToString(this.contextUri)));
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
					throw new ODataException(Strings.ODataJsonLightContextUriInvalid(ODataJsonLightContextUriParser.UriToString(this.contextUri)));
				}
				if (odataPath.Count == 0)
				{
					throw new ODataException(Strings.ODataJsonLightContextUriInvalid(ODataJsonLightContextUriParser.UriToString(this.contextUri)));
				}
				this.parseResult.Path = odataPath;
				this.parseResult.NavigationSource = odataPath.NavigationSource();
				this.parseResult.EdmType = odataPath.LastSegment.EdmType;
				ODataPathSegment lastSegment = ODataJsonLightContextUriParser.TrimEndingTypeSegment(odataPath).LastSegment;
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
					if (this.parseResult.EdmType is Microsoft.OData.Edm.IEdmCollectionType)
					{
						Microsoft.OData.Edm.IEdmCollectionTypeReference edmCollectionTypeReference = this.parseResult.EdmType.ToTypeReference(false).AsCollection();
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
					if (!ODataJsonLightContextUriParser.IsIndividualProperty(odataPath))
					{
						throw new ODataException(Strings.ODataJsonLightContextUriInvalid(ODataJsonLightContextUriParser.UriToString(this.contextUri)));
					}
					odataPayloadKind = ODataPayloadKind.Property;
					if (this.parseResult.EdmType is Microsoft.OData.Edm.IEdmComplexType)
					{
						odataPayloadKind = ODataPayloadKind.Resource;
					}
					else
					{
						Microsoft.OData.Edm.IEdmCollectionType edmCollectionType = this.parseResult.EdmType as Microsoft.OData.Edm.IEdmCollectionType;
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

		// Token: 0x06003C5E RID: 15454 RVA: 0x000C4100 File Offset: 0x000C2300
		private static ODataPath TrimEndingTypeSegment(ODataPath path)
		{
			ODataJsonLightContextUriParser.SplitEndingSegmentOfTypeHandler<TypeSegment> splitEndingSegmentOfTypeHandler = new ODataJsonLightContextUriParser.SplitEndingSegmentOfTypeHandler<TypeSegment>();
			path.WalkWith(splitEndingSegmentOfTypeHandler);
			return splitEndingSegmentOfTypeHandler.FirstPart;
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x000C4120 File Offset: 0x000C2320
		private static bool IsIndividualProperty(ODataPath path)
		{
			ODataPathSegment lastSegment = ODataJsonLightContextUriParser.TrimEndingTypeSegment(path).LastSegment;
			return lastSegment is PropertySegment || lastSegment is DynamicPathSegment;
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x000C414C File Offset: 0x000C234C
		private static string UriToString(Uri uri)
		{
			if (!uri.IsAbsoluteUri)
			{
				return uri.OriginalString;
			}
			return uri.AbsoluteUri;
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		private static string ExtractSelectQueryOption(string fragment)
		{
			return fragment;
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x000C4164 File Offset: 0x000C2364
		private ODataPayloadKind ResolveType(string typeName)
		{
			Microsoft.OData.Edm.IEdmType edmType = ODataTypeServices.FindEdmType(this.model, typeName);
			if (edmType == null)
			{
				throw new ODataException(Strings.ODataJsonLightContextUriParserUnableToResolveByTypeName(typeName));
			}
			bool flag = edmType is Microsoft.OData.Edm.IEdmCollectionType;
			edmType = edmType.AsElementType();
			if (edmType == null || (edmType.TypeKind != Microsoft.OData.Edm.EdmTypeKind.Primitive && edmType.TypeKind != Microsoft.OData.Edm.EdmTypeKind.Enum && edmType.TypeKind != Microsoft.OData.Edm.EdmTypeKind.Complex && edmType.TypeKind != Microsoft.OData.Edm.EdmTypeKind.Entity && edmType.TypeKind != Microsoft.OData.Edm.EdmTypeKind.TypeDefinition))
			{
				throw new ODataException(Strings.ODataJsonLightContextUriParserUnableToResolveByTypeName(typeName));
			}
			if (edmType.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Entity || edmType.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Complex)
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
				if (!flag)
				{
					this.parseResult.EdmType = edmType;
					return ODataPayloadKind.Property;
				}
				this.parseResult.EdmType = new EdmCollectionType(edmType.ToTypeReference(true));
				return ODataPayloadKind.Collection;
			}
		}

		// Token: 0x04001F90 RID: 8080
		private const string EntityFragmentIdentifier = "/$entity";

		// Token: 0x04001F91 RID: 8081
		private static readonly string DeltaFragmentIdentifier = "/$delta";

		// Token: 0x04001F92 RID: 8082
		private static readonly string DeletedEntityFragmentIdentifier = "/$deletedEntity";

		// Token: 0x04001F93 RID: 8083
		private static readonly string LinkFragmentIdentifier = "/$link";

		// Token: 0x04001F94 RID: 8084
		private static readonly string DeletedLinkFragmentIdentifier = "/$deletedLink";

		// Token: 0x04001F95 RID: 8085
		private static readonly Regex KeyPattern = new Regex("^(?:-{0,1}\\d+?|\\w*'.+?'|[A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-F0-9]{12}|.+?=.+?)$", RegexOptions.IgnoreCase);

		// Token: 0x04001F96 RID: 8086
		private readonly Microsoft.OData.Edm.IEdmModel model;

		// Token: 0x04001F97 RID: 8087
		private readonly Uri contextUri;

		// Token: 0x04001F98 RID: 8088
		private ODataJsonLightContextUriParseResult parseResult;

		// Token: 0x02000833 RID: 2099
		private sealed class SplitEndingSegmentOfTypeHandler<T> : PathSegmentHandler where T : ODataPathSegment
		{
			// Token: 0x17001407 RID: 5127
			// (get) Token: 0x06003C64 RID: 15460 RVA: 0x000C426C File Offset: 0x000C246C
			public ODataPath FirstPart
			{
				get
				{
					return new ODataPath(this.first);
				}
			}

			// Token: 0x17001408 RID: 5128
			// (get) Token: 0x06003C65 RID: 15461 RVA: 0x000C4279 File Offset: 0x000C2479
			public ODataPath LastPart
			{
				get
				{
					return new ODataPath(this.last);
				}
			}

			// Token: 0x06003C66 RID: 15462 RVA: 0x000C4286 File Offset: 0x000C2486
			public SplitEndingSegmentOfTypeHandler()
			{
				this.first = new Queue<ODataPathSegment>();
				this.last = new Queue<ODataPathSegment>();
			}

			// Token: 0x06003C67 RID: 15463 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(TypeSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C68 RID: 15464 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(NavigationPropertySegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C69 RID: 15465 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(EntitySetSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C6A RID: 15466 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(SingletonSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C6B RID: 15467 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(KeySegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C6C RID: 15468 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(PropertySegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C6D RID: 15469 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(OperationImportSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C6E RID: 15470 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(OperationSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C6F RID: 15471 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(DynamicPathSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C70 RID: 15472 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(CountSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C71 RID: 15473 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(NavigationPropertyLinkSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C72 RID: 15474 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(ValueSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C73 RID: 15475 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(BatchSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C74 RID: 15476 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(BatchReferenceSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C75 RID: 15477 RVA: 0x000C42A4 File Offset: 0x000C24A4
			public override void Handle(MetadataSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003C76 RID: 15478 RVA: 0x000C42B0 File Offset: 0x000C24B0
			private void CommonHandler(ODataPathSegment segment)
			{
				if (segment is T)
				{
					this.last.Enqueue(segment);
					return;
				}
				while (this.last.Any<ODataPathSegment>())
				{
					this.first.Enqueue(this.last.Dequeue());
				}
				this.first.Enqueue(segment);
			}

			// Token: 0x04001F99 RID: 8089
			private readonly Queue<ODataPathSegment> first;

			// Token: 0x04001F9A RID: 8090
			private readonly Queue<ODataPathSegment> last;
		}
	}
}
