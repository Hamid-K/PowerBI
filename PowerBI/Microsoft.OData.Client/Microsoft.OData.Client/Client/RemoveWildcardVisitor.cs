using System;
using Microsoft.OData.Client.ALinq.UriParser;

namespace Microsoft.OData.Client
{
	// Token: 0x0200001D RID: 29
	internal class RemoveWildcardVisitor : IPathSegmentTokenVisitor
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00004B12 File Offset: 0x00002D12
		public void Visit(SystemToken tokenIn)
		{
			throw new NotSupportedException(Strings.ALinq_IllegalSystemQueryOption(tokenIn.Identifier));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004D78 File Offset: 0x00002F78
		public void Visit(NonSystemToken tokenIn)
		{
			if (!(tokenIn.Identifier != '*'.ToString()))
			{
				this.previous.NextToken = null;
				return;
			}
			if (tokenIn.NextToken == null)
			{
				return;
			}
			this.previous = tokenIn;
			tokenIn.NextToken.Accept(this);
		}

		// Token: 0x04000043 RID: 67
		private PathSegmentToken previous;
	}
}
