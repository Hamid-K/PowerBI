using System;
using Microsoft.OData.Client.ALinq.UriParser;

namespace Microsoft.OData.Client
{
	// Token: 0x0200001A RID: 26
	internal class SelectExpandPathToStringVisitor : IPathSegmentTokenVisitor<string>
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00004B12 File Offset: 0x00002D12
		public string Visit(SystemToken tokenIn)
		{
			throw new NotSupportedException(Strings.ALinq_IllegalSystemQueryOption(tokenIn.Identifier));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004BD8 File Offset: 0x00002DD8
		public string Visit(NonSystemToken tokenIn)
		{
			if (tokenIn.NextToken == null)
			{
				return tokenIn.Identifier;
			}
			if (!tokenIn.NextToken.IsStructuralProperty)
			{
				return tokenIn.Identifier + "($expand=" + tokenIn.NextToken.Accept<string>(this) + ")";
			}
			PathSegmentToken pathSegmentToken;
			string text = SelectExpandPathToStringVisitor.WriteNextStructuralProperties(tokenIn.NextToken, out pathSegmentToken);
			if (pathSegmentToken != null)
			{
				return string.Concat(new string[]
				{
					tokenIn.Identifier,
					"($select=",
					text,
					";$expand=",
					pathSegmentToken.Accept<string>(this),
					")"
				});
			}
			return tokenIn.Identifier + "($select=" + text + ")";
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004C88 File Offset: 0x00002E88
		private static string WriteNextStructuralProperties(PathSegmentToken firstStructuralProperty, out PathSegmentToken firstNonStructuralProperty)
		{
			firstNonStructuralProperty = firstStructuralProperty;
			string text = "";
			while (firstNonStructuralProperty.IsStructuralProperty)
			{
				if (firstNonStructuralProperty.NextToken == null)
				{
					text += firstNonStructuralProperty.Identifier;
					firstNonStructuralProperty = null;
					return text;
				}
				if (firstNonStructuralProperty.NextToken.IsStructuralProperty)
				{
					text = text + firstNonStructuralProperty.Identifier + ",";
				}
				else
				{
					text += firstNonStructuralProperty.Identifier;
				}
				firstNonStructuralProperty = firstNonStructuralProperty.NextToken;
			}
			return text;
		}

		// Token: 0x0400003F RID: 63
		public const string SelectClause = "($select=";

		// Token: 0x04000040 RID: 64
		public const string StartingExpandClause = "($expand=";

		// Token: 0x04000041 RID: 65
		public const string NonStartingExpandClause = "$expand=";
	}
}
