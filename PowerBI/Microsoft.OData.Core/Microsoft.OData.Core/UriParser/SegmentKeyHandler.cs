using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200015E RID: 350
	internal static class SegmentKeyHandler
	{
		// Token: 0x060011DB RID: 4571 RVA: 0x0003442C File Offset: 0x0003262C
		internal static bool TryCreateKeySegmentFromParentheses(ODataPathSegment previous, KeySegment previousKeySegment, string parenthesisExpression, ODataUriResolver resolver, out ODataPathSegment keySegment, bool enableUriTemplateParsing = false)
		{
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

		// Token: 0x060011DC RID: 4572 RVA: 0x00034474 File Offset: 0x00032674
		internal static bool TryHandleSegmentAsKey(string segmentText, ODataPathSegment previous, KeySegment previousKeySegment, ODataUrlKeyDelimiter odataUrlKeyDelimiter, ODataUriResolver resolver, out KeySegment keySegment, bool enableUriTemplateParsing = false)
		{
			keySegment = null;
			if (!odataUrlKeyDelimiter.EnableKeyAsSegment)
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

		// Token: 0x060011DD RID: 4573 RVA: 0x000344D1 File Offset: 0x000326D1
		private static bool IsSystemSegment(string segmentText)
		{
			return segmentText[0] == '$' && (segmentText.Length < 2 || segmentText[1] != '$');
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000344FC File Offset: 0x000326FC
		private static KeySegment CreateKeySegment(ODataPathSegment segment, KeySegment previousKeySegment, SegmentArgumentParser key, ODataUriResolver resolver)
		{
			IEdmEntityType edmEntityType = null;
			if (segment.TargetEdmType == null || !segment.TargetEdmType.IsEntityOrEntityCollectionType(out edmEntityType))
			{
				throw ExceptionUtil.CreateSyntaxError();
			}
			List<IEdmStructuralProperty> list = edmEntityType.Key().ToList<IEdmStructuralProperty>();
			if (list.Count != key.ValueCount)
			{
				NavigationPropertySegment navigationPropertySegment = segment as NavigationPropertySegment;
				if (navigationPropertySegment != null)
				{
					key = KeyFinder.FindAndUseKeysFromRelatedSegment(key, list, navigationPropertySegment.NavigationProperty, previousKeySegment);
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
			return new KeySegment(segment, enumerable, edmEntityType, segment.TargetEdmNavigationSource);
		}
	}
}
