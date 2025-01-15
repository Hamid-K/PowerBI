using System;
using System.Text;
using Microsoft.OData.Client.ALinq.UriParser;

namespace Microsoft.OData.Client
{
	// Token: 0x02000019 RID: 25
	internal class ExpandOnlyPathToStringVisitor : IPathSegmentTokenVisitor<string>
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004B12 File Offset: 0x00002D12
		public string Visit(SystemToken tokenIn)
		{
			throw new NotSupportedException(Strings.ALinq_IllegalSystemQueryOption(tokenIn.Identifier));
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004B24 File Offset: 0x00002D24
		public string Visit(NonSystemToken tokenIn)
		{
			if (tokenIn.IsNamespaceOrContainerQualified())
			{
				if (tokenIn.NextToken != null)
				{
					return tokenIn.Identifier + "/" + tokenIn.NextToken.Accept<string>(this);
				}
				throw new NotSupportedException(Strings.ALinq_TypeTokenWithNoTrailingNavProp(tokenIn.Identifier));
			}
			else
			{
				if (tokenIn.NextToken == null)
				{
					return tokenIn.Identifier;
				}
				return tokenIn.Identifier + this.subExpandStartingText + tokenIn.NextToken.Accept<string>(this) + ")";
			}
		}

		// Token: 0x0400003E RID: 62
		private readonly string subExpandStartingText = new StringBuilder().Append('(').Append('$').Append("expand")
			.Append('=')
			.ToString();
	}
}
