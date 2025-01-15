using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x02000211 RID: 529
	internal static class SegmentKeyHandler
	{
		// Token: 0x0600134B RID: 4939 RVA: 0x000464A8 File Offset: 0x000446A8
		internal static bool TryCreateKeySegmentFromParentheses(ODataPathSegment previous, KeySegment previousKeySegment, string parenthesisExpression, out ODataPathSegment keySegment, bool enableUriTemplateParsing = false, ODataUriResolver resolver = null)
		{
			if (resolver == null)
			{
				resolver = ODataUriResolver.Default;
			}
			if (previous.SingleResult)
			{
				throw ExceptionUtil.CreateSyntaxError();
			}
			SegmentArgumentParser segmentArgumentParser;
			if (!SegmentArgumentParser.TryParseKeysFromUri(parenthesisExpression, out segmentArgumentParser, enableUriTemplateParsing))
			{
				throw ExceptionUtil.CreateSyntaxError();
			}
			if (segmentArgumentParser.IsEmpty)
			{
				keySegment = null;
				return false;
			}
			keySegment = SegmentKeyHandler.CreateKeySegment(previous, previousKeySegment, segmentArgumentParser, resolver);
			return true;
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x000464FC File Offset: 0x000446FC
		internal static bool TryHandleSegmentAsKey(string segmentText, ODataPathSegment previous, KeySegment previousKeySegment, UrlConvention urlConvention, out KeySegment keySegment, bool enableUriTemplateParsing = false, ODataUriResolver resolver = null)
		{
			if (resolver == null)
			{
				resolver = ODataUriResolver.Default;
			}
			keySegment = null;
			if (!urlConvention.GenerateKeyAsSegment && !urlConvention.ODataSimplified)
			{
				return false;
			}
			if (previous.SingleResult)
			{
				return false;
			}
			if (SegmentKeyHandler.IsSystemSegment(segmentText))
			{
				return false;
			}
			IEdmEntityType edmEntityType;
			if (previous.TargetEdmType == null || !previous.TargetEdmType.IsEntityOrEntityCollectionType(out edmEntityType))
			{
				return false;
			}
			keySegment = SegmentKeyHandler.CreateKeySegment(previous, previousKeySegment, SegmentArgumentParser.FromSegment(segmentText, enableUriTemplateParsing), resolver);
			return true;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0004656C File Offset: 0x0004476C
		private static bool IsSystemSegment(string segmentText)
		{
			return segmentText.get_Chars(0) == '$' && (segmentText.Length < 2 || segmentText.get_Chars(1) != '$');
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00046594 File Offset: 0x00044794
		private static KeySegment CreateKeySegment(ODataPathSegment segment, KeySegment previousKeySegment, SegmentArgumentParser key, ODataUriResolver resolver)
		{
			IEdmEntityType edmEntityType = null;
			if (segment.TargetEdmType == null || !segment.TargetEdmType.IsEntityOrEntityCollectionType(out edmEntityType))
			{
				throw ExceptionUtil.CreateSyntaxError();
			}
			List<IEdmStructuralProperty> list = Enumerable.ToList<IEdmStructuralProperty>(edmEntityType.Key());
			if (list.Count != key.ValueCount)
			{
				NavigationPropertySegment navigationPropertySegment = segment as NavigationPropertySegment;
				if (navigationPropertySegment != null)
				{
					key = KeyFinder.FindAndUseKeysFromRelatedSegment(key, list, navigationPropertySegment.NavigationProperty, previousKeySegment);
				}
				if (list.Count != key.ValueCount && resolver.GetType() == typeof(ODataUriResolver))
				{
					throw ExceptionUtil.CreateBadRequestError(Strings.BadRequest_KeyCountMismatch(edmEntityType.FullName()));
				}
			}
			if (!key.AreValuesNamed && key.ValueCount > 1 && resolver.GetType() == typeof(ODataUriResolver))
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_KeysMustBeNamed);
			}
			IEnumerable<KeyValuePair<string, object>> enumerable;
			if (!key.TryConvertValues(edmEntityType, out enumerable, resolver))
			{
				throw ExceptionUtil.CreateSyntaxError();
			}
			IEdmEntityType edmEntityType2;
			segment.TargetEdmType.IsEntityOrEntityCollectionType(out edmEntityType2);
			KeySegment keySegment = new KeySegment(enumerable, edmEntityType2, segment.TargetEdmNavigationSource);
			keySegment.CopyValuesFrom(segment);
			keySegment.SingleResult = true;
			return keySegment;
		}
	}
}
