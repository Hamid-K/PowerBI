using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200011B RID: 283
	internal static class SegmentKeyHandler
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x00026200 File Offset: 0x00024400
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

		// Token: 0x06000D2B RID: 3371 RVA: 0x00026248 File Offset: 0x00024448
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

		// Token: 0x06000D2C RID: 3372 RVA: 0x000262A5 File Offset: 0x000244A5
		private static bool IsSystemSegment(string segmentText)
		{
			return segmentText.get_Chars(0) == '$' && (segmentText.Length < 2 || segmentText.get_Chars(1) != '$');
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x000262D0 File Offset: 0x000244D0
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
			return new KeySegment(segment, enumerable, edmEntityType, segment.TargetEdmNavigationSource);
		}
	}
}
