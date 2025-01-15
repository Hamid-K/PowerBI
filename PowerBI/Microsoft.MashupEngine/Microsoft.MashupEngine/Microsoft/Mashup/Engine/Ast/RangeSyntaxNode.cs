using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B8B RID: 7051
	public class RangeSyntaxNode : SyntaxNode, ISyntaxNode
	{
		// Token: 0x0600B0A6 RID: 45222 RVA: 0x00243401 File Offset: 0x00241601
		public RangeSyntaxNode(TokenRange range)
		{
			this.range = range;
		}

		// Token: 0x17002C15 RID: 11285
		// (get) Token: 0x0600B0A7 RID: 45223 RVA: 0x00243410 File Offset: 0x00241610
		TokenRange ISyntaxNode.Range
		{
			get
			{
				return this.range;
			}
		}

		// Token: 0x04005ACD RID: 23245
		private TokenRange range;
	}
}
