using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000133 RID: 307
	internal static class SelectExpandPathBinder
	{
		// Token: 0x06001049 RID: 4169 RVA: 0x0002B288 File Offset: 0x00029488
		public static IEnumerable<ODataPathSegment> FollowTypeSegments(PathSegmentToken firstTypeToken, IEdmModel model, int maxDepth, ODataUriResolver resolver, ref IEdmStructuredType currentLevelType, out PathSegmentToken firstNonTypeToken)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentToken>(firstTypeToken, "firstTypeToken");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (!firstTypeToken.IsNamespaceOrContainerQualified())
			{
				throw new ODataException(Strings.SelectExpandPathBinder_FollowNonTypeSegment(firstTypeToken.Identifier));
			}
			int num = 0;
			List<ODataPathSegment> list = new List<ODataPathSegment>();
			PathSegmentToken pathSegmentToken = firstTypeToken;
			while (pathSegmentToken.IsNamespaceOrContainerQualified() && pathSegmentToken.NextToken != null)
			{
				IEdmType edmType = currentLevelType;
				currentLevelType = UriEdmHelpers.FindTypeFromModel(model, pathSegmentToken.Identifier, resolver) as IEdmStructuredType;
				if (currentLevelType == null)
				{
					throw new ODataException(Strings.ExpandItemBinder_CannotFindType(pathSegmentToken.Identifier));
				}
				UriEdmHelpers.CheckRelatedTo(edmType, currentLevelType);
				list.Add(new TypeSegment(currentLevelType, null));
				num++;
				pathSegmentToken = pathSegmentToken.NextToken;
				if (num >= maxDepth)
				{
					throw new ODataException(Strings.ExpandItemBinder_PathTooDeep);
				}
			}
			firstNonTypeToken = pathSegmentToken;
			return list;
		}
	}
}
