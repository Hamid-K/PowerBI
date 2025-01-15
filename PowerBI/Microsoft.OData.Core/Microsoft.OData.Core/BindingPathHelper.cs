using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x02000023 RID: 35
	internal static class BindingPathHelper
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00003EB8 File Offset: 0x000020B8
		public static bool MatchBindingPath(IEdmPathExpression bindingPath, List<ODataPathSegment> parsedSegments)
		{
			List<string> list = bindingPath.PathSegments.ToList<string>();
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
