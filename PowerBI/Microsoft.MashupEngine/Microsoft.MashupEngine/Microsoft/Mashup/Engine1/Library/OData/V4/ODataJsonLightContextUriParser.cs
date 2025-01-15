using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.OData.Core;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200086A RID: 2154
	internal class ODataJsonLightContextUriParser
	{
		// Token: 0x06003E04 RID: 15876 RVA: 0x000CA9CE File Offset: 0x000C8BCE
		public ODataJsonLightContextUriParser(Microsoft.OData.Edm.IEdmModel model, Uri contextUri)
		{
			this.model = model;
			this.contextUri = contextUri;
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x000CA9E4 File Offset: 0x000C8BE4
		public static ODataJsonLightContextUriParseResult Parse(Microsoft.OData.Edm.IEdmModel model, string contextUriFromPayload, bool needParseFragment)
		{
			Uri uri = new Uri(contextUriFromPayload, UriKind.Absolute);
			ODataJsonLightContextUriParser odataJsonLightContextUriParser = new ODataJsonLightContextUriParser(model, uri);
			odataJsonLightContextUriParser.TokenizeContextUri();
			if (needParseFragment)
			{
				odataJsonLightContextUriParser.ParseContextUri();
			}
			return odataJsonLightContextUriParser.parseResult;
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x000CAA18 File Offset: 0x000C8C18
		private void ParseContextUri()
		{
			ODataPayloadKind odataPayloadKind = this.ParseContextUriFragment(this.parseResult.Fragment);
			if (odataPayloadKind == ODataPayloadKind.Collection)
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					ODataPayloadKind.Collection,
					ODataPayloadKind.Property
				};
			}
			else if (odataPayloadKind == ODataPayloadKind.Entry)
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[]
				{
					ODataPayloadKind.Entry,
					ODataPayloadKind.Delta
				};
			}
			else
			{
				this.parseResult.DetectedPayloadKinds = new ODataPayloadKind[] { odataPayloadKind };
			}
			if (this.parseResult.SelectQueryOption != null && odataPayloadKind != ODataPayloadKind.Feed && odataPayloadKind != ODataPayloadKind.Entry && odataPayloadKind != ODataPayloadKind.Delta)
			{
				throw new ODataException(Strings.ODataJsonLightContextUriInvalid(ODataJsonLightContextUriParser.UriToString(this.contextUri)));
			}
			this.parseResult.PayloadKind = this.ResolveToPayloadKind();
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x000CAAD0 File Offset: 0x000C8CD0
		private void TokenizeContextUri()
		{
			this.parseResult = new ODataJsonLightContextUriParseResult(this.contextUri);
			Uri uri = this.parseResult.ContextUri;
			UriBuilder uriBuilder = new UriBuilder(this.parseResult.ContextUri)
			{
				Fragment = null
			};
			this.parseResult.MetadataDocumentUri = uriBuilder.Uri;
			this.parseResult.Fragment = uri.GetComponents(UriComponents.Fragment, UriFormat.Unescaped);
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x000CAB38 File Offset: 0x000C8D38
		private ODataPayloadKind ResolveToPayloadKind()
		{
			ODataPayloadKind[] detectedPayloadKinds = this.parseResult.DetectedPayloadKinds;
			if (detectedPayloadKinds.Length == 1)
			{
				return detectedPayloadKinds[0];
			}
			if (detectedPayloadKinds.Length == 2)
			{
				if (detectedPayloadKinds.Any((ODataPayloadKind p) => p == ODataPayloadKind.Collection))
				{
					if (detectedPayloadKinds.Any((ODataPayloadKind p) => p == ODataPayloadKind.Property))
					{
						return ODataPayloadKind.Collection;
					}
				}
			}
			if (detectedPayloadKinds.Length == 2)
			{
				if (detectedPayloadKinds.Any((ODataPayloadKind p) => p == ODataPayloadKind.Entry))
				{
					if (detectedPayloadKinds.Any((ODataPayloadKind p) => p == ODataPayloadKind.Delta))
					{
						return ODataPayloadKind.Entry;
					}
				}
			}
			return ODataPayloadKind.Unsupported;
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x000CAC0C File Offset: 0x000C8E0C
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
				odataDeltaKind = ODataDeltaKind.Feed;
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
				else if (fragment.Equals("Edm.Null", StringComparison.Ordinal))
				{
					odataPayloadKind = ODataPayloadKind.Property;
					this.parseResult.IsNullProperty = true;
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
					Microsoft.OData.Edm.IEdmNavigationSource edmNavigationSource = this.model.FindDeclaredNavigationSource(fragment);
					if (edmNavigationSource != null)
					{
						this.parseResult.EdmType = edmNavigationSource.EntityType();
						odataPayloadKind = ((edmNavigationSource is Microsoft.OData.Edm.IEdmSingleton) ? ODataPayloadKind.Entry : ODataPayloadKind.Feed);
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
						odataPayloadKind = (flag ? ODataPayloadKind.Entry : ODataPayloadKind.Feed);
					}
					if (this.parseResult.EdmType is Microsoft.OData.Edm.IEdmCollectionType)
					{
						Microsoft.OData.Edm.IEdmCollectionTypeReference edmCollectionTypeReference = ODataJsonLightContextUriParser.ToTypeReference(this.parseResult.EdmType, false).AsCollection();
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
					if (!ODataJsonLightContextUriParser.IsIndividualProperty(odataPath))
					{
						throw new ODataException(Strings.ODataJsonLightContextUriInvalid(ODataJsonLightContextUriParser.UriToString(this.contextUri)));
					}
					odataPayloadKind = ODataPayloadKind.Property;
					if (this.parseResult.EdmType is Microsoft.OData.Edm.IEdmCollectionType)
					{
						odataPayloadKind = ODataPayloadKind.Collection;
					}
				}
			}
			return odataPayloadKind;
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x000CB05C File Offset: 0x000C925C
		private static ODataPath TrimEndingTypeSegment(ODataPath path)
		{
			ODataJsonLightContextUriParser.SplitEndingSegmentOfTypeHandler<TypeSegment> splitEndingSegmentOfTypeHandler = new ODataJsonLightContextUriParser.SplitEndingSegmentOfTypeHandler<TypeSegment>();
			path.WalkWith(splitEndingSegmentOfTypeHandler);
			return splitEndingSegmentOfTypeHandler.FirstPart;
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x000CB07C File Offset: 0x000C927C
		private static bool IsIndividualProperty(ODataPath path)
		{
			ODataPathSegment lastSegment = ODataJsonLightContextUriParser.TrimEndingTypeSegment(path).LastSegment;
			return lastSegment is PropertySegment || lastSegment is OpenPropertySegment;
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x000C414C File Offset: 0x000C234C
		private static string UriToString(Uri uri)
		{
			if (!uri.IsAbsoluteUri)
			{
				return uri.OriginalString;
			}
			return uri.AbsoluteUri;
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		private static string ExtractSelectQueryOption(string fragment)
		{
			return fragment;
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x000CB0A8 File Offset: 0x000C92A8
		private static Microsoft.OData.Edm.IEdmTypeReference ToTypeReference(Microsoft.OData.Edm.IEdmType type, bool nullable)
		{
			if (type == null)
			{
				return null;
			}
			switch (type.TypeKind)
			{
			case Microsoft.OData.Edm.EdmTypeKind.Primitive:
				return ODataJsonLightContextUriParser.ToTypeReference((Microsoft.OData.Edm.IEdmPrimitiveType)type, nullable);
			case Microsoft.OData.Edm.EdmTypeKind.Entity:
				return new Microsoft.OData.Edm.Library.EdmEntityTypeReference((Microsoft.OData.Edm.IEdmEntityType)type, nullable);
			case Microsoft.OData.Edm.EdmTypeKind.Complex:
				return new Microsoft.OData.Edm.Library.EdmComplexTypeReference((Microsoft.OData.Edm.IEdmComplexType)type, nullable);
			case Microsoft.OData.Edm.EdmTypeKind.Collection:
				return new Microsoft.OData.Edm.Library.EdmCollectionTypeReference((Microsoft.OData.Edm.IEdmCollectionType)type);
			case Microsoft.OData.Edm.EdmTypeKind.EntityReference:
				return new Microsoft.OData.Edm.Library.EdmEntityReferenceTypeReference((Microsoft.OData.Edm.IEdmEntityReferenceType)type, nullable);
			case Microsoft.OData.Edm.EdmTypeKind.Enum:
				return new Microsoft.OData.Edm.Library.EdmEnumTypeReference((Microsoft.OData.Edm.IEdmEnumType)type, nullable);
			case Microsoft.OData.Edm.EdmTypeKind.TypeDefinition:
				return new Microsoft.OData.Edm.Library.EdmTypeDefinitionReference((Microsoft.OData.Edm.IEdmTypeDefinition)type, nullable);
			default:
				throw new ODataException(Strings.ODataJsonLightContextUriParserUnableToConvertEdmTypeToTypeReference(type.FullTypeName()));
			}
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x000CB158 File Offset: 0x000C9358
		private static Microsoft.OData.Edm.Library.EdmPrimitiveTypeReference ToTypeReference(Microsoft.OData.Edm.IEdmPrimitiveType primitiveType, bool nullable)
		{
			switch (primitiveType.PrimitiveKind)
			{
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Binary:
				return new Microsoft.OData.Edm.Library.EdmBinaryTypeReference(primitiveType, nullable);
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Boolean:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Byte:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Double:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Guid:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int16:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int32:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Int64:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.SByte:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Single:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Stream:
				return new Microsoft.OData.Edm.Library.EdmPrimitiveTypeReference(primitiveType, nullable);
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.DateTimeOffset:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Duration:
				return new Microsoft.OData.Edm.Library.EdmTemporalTypeReference(primitiveType, nullable);
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Decimal:
				return new Microsoft.OData.Edm.Library.EdmDecimalTypeReference(primitiveType, nullable);
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.String:
				return new Microsoft.OData.Edm.Library.EdmStringTypeReference(primitiveType, nullable);
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Geography:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyPoint:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyLineString:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyPolygon:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyCollection:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyMultiLineString:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeographyMultiPoint:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.Geometry:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryPoint:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryLineString:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryPolygon:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryCollection:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryMultiLineString:
			case Microsoft.OData.Edm.EdmPrimitiveTypeKind.GeometryMultiPoint:
				return new Microsoft.OData.Edm.Library.EdmSpatialTypeReference(primitiveType, nullable);
			default:
				return null;
			}
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x000CB224 File Offset: 0x000C9424
		private ODataPayloadKind ResolveType(string typeName)
		{
			Microsoft.OData.Edm.IEdmType edmType = ODataTypeServices.FindEdmType(this.model, typeName);
			if (edmType == null)
			{
				throw new ODataException(Strings.ODataJsonLightContextUriParserUnableToResolveByTypeName(typeName));
			}
			bool flag = edmType is Microsoft.OData.Edm.IEdmCollectionType;
			if (edmType.TypeKind == Microsoft.OData.Edm.EdmTypeKind.Entity)
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
				this.parseResult.EdmType = edmType;
				if (!flag)
				{
					return ODataPayloadKind.Property;
				}
				return ODataPayloadKind.Collection;
			}
		}

		// Token: 0x040020A6 RID: 8358
		private const string EntityFragmentIdentifier = "/$entity";

		// Token: 0x040020A7 RID: 8359
		private static readonly string DeltaFragmentIdentifier = "/$delta";

		// Token: 0x040020A8 RID: 8360
		private static readonly string DeletedEntityFragmentIdentifier = "/$deletedEntity";

		// Token: 0x040020A9 RID: 8361
		private static readonly string LinkFragmentIdentifier = "/$link";

		// Token: 0x040020AA RID: 8362
		private static readonly string DeletedLinkFragmentIdentifier = "/$deletedLink";

		// Token: 0x040020AB RID: 8363
		private static readonly Regex KeyPattern = new Regex("^(?:-{0,1}\\d+?|\\w*'.+?'|[A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-F0-9]{12}|.+?=.+?)$", RegexOptions.IgnoreCase);

		// Token: 0x040020AC RID: 8364
		private readonly Microsoft.OData.Edm.IEdmModel model;

		// Token: 0x040020AD RID: 8365
		private readonly Uri contextUri;

		// Token: 0x040020AE RID: 8366
		private ODataJsonLightContextUriParseResult parseResult;

		// Token: 0x0200086B RID: 2155
		private sealed class SplitEndingSegmentOfTypeHandler<T> : PathSegmentHandler where T : ODataPathSegment
		{
			// Token: 0x17001466 RID: 5222
			// (get) Token: 0x06003E12 RID: 15890 RVA: 0x000CB2C4 File Offset: 0x000C94C4
			public ODataPath FirstPart
			{
				get
				{
					return new ODataPath(this.first);
				}
			}

			// Token: 0x17001467 RID: 5223
			// (get) Token: 0x06003E13 RID: 15891 RVA: 0x000CB2D1 File Offset: 0x000C94D1
			public ODataPath LastPart
			{
				get
				{
					return new ODataPath(this.last);
				}
			}

			// Token: 0x06003E14 RID: 15892 RVA: 0x000CB2DE File Offset: 0x000C94DE
			public SplitEndingSegmentOfTypeHandler()
			{
				this.first = new Queue<ODataPathSegment>();
				this.last = new Queue<ODataPathSegment>();
			}

			// Token: 0x06003E15 RID: 15893 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(TypeSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E16 RID: 15894 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(NavigationPropertySegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E17 RID: 15895 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(EntitySetSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E18 RID: 15896 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(SingletonSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E19 RID: 15897 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(KeySegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E1A RID: 15898 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(PropertySegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E1B RID: 15899 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(OperationImportSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E1C RID: 15900 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(OperationSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E1D RID: 15901 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(OpenPropertySegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E1E RID: 15902 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(CountSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E1F RID: 15903 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(NavigationPropertyLinkSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E20 RID: 15904 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(ValueSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E21 RID: 15905 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(BatchSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E22 RID: 15906 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(BatchReferenceSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E23 RID: 15907 RVA: 0x000CB2FC File Offset: 0x000C94FC
			public override void Handle(MetadataSegment segment)
			{
				this.CommonHandler(segment);
			}

			// Token: 0x06003E24 RID: 15908 RVA: 0x000CB308 File Offset: 0x000C9508
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

			// Token: 0x040020AF RID: 8367
			private readonly Queue<ODataPathSegment> first;

			// Token: 0x040020B0 RID: 8368
			private readonly Queue<ODataPathSegment> last;
		}
	}
}
