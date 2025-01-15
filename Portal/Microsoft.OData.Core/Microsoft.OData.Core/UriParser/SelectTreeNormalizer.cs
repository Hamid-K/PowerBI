using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000136 RID: 310
	internal static class SelectTreeNormalizer
	{
		// Token: 0x06001051 RID: 4177 RVA: 0x0002B7ED File Offset: 0x000299ED
		public static SelectToken NormalizeSelectTree(SelectToken selectToken)
		{
			SelectTreeNormalizer.VerifySelectToken(selectToken);
			selectToken = SelectTreeNormalizer.NormalizeSelectPaths(selectToken);
			return selectToken;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0002B800 File Offset: 0x00029A00
		private static void VerifySelectToken(SelectToken selectToken)
		{
			if (selectToken == null)
			{
				return;
			}
			HashSet<PathSegmentToken> hashSet = new HashSet<PathSegmentToken>(new PathSegmentTokenEqualityComparer());
			foreach (SelectTermToken selectTermToken in selectToken.SelectTerms)
			{
				if (hashSet.Contains(selectTermToken.PathToProperty))
				{
					throw new ODataException(Strings.SelectTreeNormalizer_MultipleSelecTermWithSamePathFound(SelectTreeNormalizer.ToPathString(selectTermToken.PathToProperty)));
				}
				hashSet.Add(selectTermToken.PathToProperty);
				if (selectTermToken.SelectOption != null)
				{
					SelectTreeNormalizer.VerifySelectToken(selectTermToken.SelectOption);
				}
			}
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0002B89C File Offset: 0x00029A9C
		private static string ToPathString(PathSegmentToken head)
		{
			StringBuilder stringBuilder = new StringBuilder();
			PathSegmentToken pathSegmentToken = head;
			while (pathSegmentToken != null)
			{
				stringBuilder.Append(pathSegmentToken.Identifier);
				NonSystemToken nonSystemToken = pathSegmentToken as NonSystemToken;
				if (nonSystemToken != null && nonSystemToken.NamedValues != null)
				{
					stringBuilder.Append("(");
					bool flag = true;
					foreach (NamedValue namedValue in nonSystemToken.NamedValues)
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append(namedValue.Name).Append("=").Append(namedValue.Value.Value);
					}
					stringBuilder.Append(")");
				}
				pathSegmentToken = pathSegmentToken.NextToken;
				if (pathSegmentToken != null)
				{
					stringBuilder.Append("/");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0002B994 File Offset: 0x00029B94
		private static SelectToken NormalizeSelectPaths(SelectToken selectToken)
		{
			if (selectToken != null)
			{
				foreach (SelectTermToken selectTermToken in selectToken.SelectTerms)
				{
					selectTermToken.PathToProperty = selectTermToken.PathToProperty.Reverse();
					if (selectTermToken.SelectOption != null)
					{
						selectTermToken.SelectOption = SelectTreeNormalizer.NormalizeSelectPaths(selectTermToken.SelectOption);
					}
				}
			}
			return selectToken;
		}
	}
}
