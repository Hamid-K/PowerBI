using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B8C RID: 7052
	public class NullRangeSyntaxNode : SyntaxNode, ISyntaxNode
	{
		// Token: 0x17002C16 RID: 11286
		// (get) Token: 0x0600B0A8 RID: 45224 RVA: 0x00002E8B File Offset: 0x0000108B
		TokenRange ISyntaxNode.Range
		{
			get
			{
				return TokenRange.Null;
			}
		}
	}
}
