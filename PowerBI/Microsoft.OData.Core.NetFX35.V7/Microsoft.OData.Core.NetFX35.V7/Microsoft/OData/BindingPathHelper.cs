using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x020000BA RID: 186
	internal static class BindingPathHelper
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x00015068 File Offset: 0x00013268
		public static bool MatchBindingPath(IEdmPathExpression bindingPath, List<ODataPathSegment> parsedSegments)
		{
			List<string> list = Enumerable.ToList<string>(bindingPath.PathSegments);
			if (list.Count == 1)
			{
				return true;
			}
			int num = list.Count - 2;
			for (int i = parsedSegments.Count - 1; i >= 0; i--)
			{
				ODataPathSegment odataPathSegment = parsedSegments[i];
				bool flag = odataPathSegment is NavigationPropertySegment;
				if (odataPathSegment is PropertySegment || (flag && odataPathSegment.TargetEdmNavigationSource is IEdmContainedEntitySet))
				{
					if (num < 0 || string.CompareOrdinal(list[num], odataPathSegment.Identifier) != 0)
					{
						return false;
					}
					num--;
				}
				else if (odataPathSegment is TypeSegment)
				{
					if (num >= 0 && list[num].Contains("."))
					{
						if (string.CompareOrdinal(list[num], odataPathSegment.EdmType.AsElementType().FullTypeName()) != 0)
						{
							return false;
						}
						num--;
					}
				}
				else if (odataPathSegment is EntitySetSegment || odataPathSegment is SingletonSegment || flag)
				{
					break;
				}
			}
			return num == -1;
		}
	}
}
