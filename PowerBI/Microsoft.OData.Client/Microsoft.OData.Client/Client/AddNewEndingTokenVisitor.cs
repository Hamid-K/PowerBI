using System;
using Microsoft.OData.Client.ALinq.UriParser;

namespace Microsoft.OData.Client
{
	// Token: 0x0200001B RID: 27
	internal class AddNewEndingTokenVisitor : IPathSegmentTokenVisitor
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00004D02 File Offset: 0x00002F02
		public AddNewEndingTokenVisitor(PathSegmentToken newTokenToAdd)
		{
			this.newTokenToAdd = newTokenToAdd;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004B12 File Offset: 0x00002D12
		public void Visit(SystemToken tokenIn)
		{
			throw new NotSupportedException(Strings.ALinq_IllegalSystemQueryOption(tokenIn.Identifier));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004D11 File Offset: 0x00002F11
		public void Visit(NonSystemToken tokenIn)
		{
			if (tokenIn.NextToken == null)
			{
				if (this.newTokenToAdd != null)
				{
					tokenIn.NextToken = this.newTokenToAdd;
					return;
				}
			}
			else
			{
				tokenIn.NextToken.Accept(this);
			}
		}

		// Token: 0x04000042 RID: 66
		private readonly PathSegmentToken newTokenToAdd;
	}
}
