using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000F1 RID: 241
	internal static class SelectExpandPathExtensions
	{
		// Token: 0x06000839 RID: 2105 RVA: 0x000200A8 File Offset: 0x0001E2A8
		public static ODataPathSegment GetFirstNonTypeCastSegment(this ODataSelectPath selectPath, out IList<ODataPathSegment> remainingSegments)
		{
			if (selectPath == null)
			{
				throw new ArgumentNullException("selectPath");
			}
			return SelectExpandPathExtensions.GetFirstNonTypeCastSegment(selectPath, (ODataPathSegment m) => m is PropertySegment || m is TypeSegment, (ODataPathSegment s) => s is NavigationPropertySegment || s is PropertySegment || s is OperationSegment || s is DynamicPathSegment, out remainingSegments);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00020108 File Offset: 0x0001E308
		public static ODataPathSegment GetFirstNonTypeCastSegment(this ODataExpandPath expandPath, out IList<ODataPathSegment> remainingSegments)
		{
			if (expandPath == null)
			{
				throw new ArgumentNullException("expandPath");
			}
			return SelectExpandPathExtensions.GetFirstNonTypeCastSegment(expandPath, (ODataPathSegment m) => m is PropertySegment || m is TypeSegment, (ODataPathSegment s) => s is NavigationPropertySegment, out remainingSegments);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00020168 File Offset: 0x0001E368
		private static ODataPathSegment GetFirstNonTypeCastSegment(ODataPath path, Func<ODataPathSegment, bool> middleSegmentPredicte, Func<ODataPathSegment, bool> lastSegmentPredicte, out IList<ODataPathSegment> remainingSegments)
		{
			remainingSegments = null;
			ODataPathSegment odataPathSegment = null;
			int num = path.Count - 1;
			int num2 = 0;
			foreach (ODataPathSegment odataPathSegment2 in path)
			{
				if (num2 == num)
				{
					if (!lastSegmentPredicte(odataPathSegment2))
					{
						throw new ODataException(Error.Format(SRResources.InvalidLastSegmentInSelectExpandPath, new object[] { odataPathSegment2.GetType().Name }));
					}
				}
				else if (!middleSegmentPredicte(odataPathSegment2))
				{
					throw new ODataException(Error.Format(SRResources.InvalidSegmentInSelectExpandPath, new object[] { odataPathSegment2.GetType().Name }));
				}
				num2++;
				if (odataPathSegment != null)
				{
					if (remainingSegments == null)
					{
						remainingSegments = new List<ODataPathSegment>();
					}
					remainingSegments.Add(odataPathSegment2);
				}
				else if (!(odataPathSegment2 is TypeSegment))
				{
					odataPathSegment = odataPathSegment2;
				}
			}
			return odataPathSegment;
		}
	}
}
