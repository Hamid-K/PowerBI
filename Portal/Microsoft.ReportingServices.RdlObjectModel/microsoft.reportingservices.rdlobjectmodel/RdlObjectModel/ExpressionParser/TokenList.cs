using System;
using System.Collections;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200023C RID: 572
	internal sealed class TokenList : IEnumerable
	{
		// Token: 0x06001322 RID: 4898 RVA: 0x0002DA1A File Offset: 0x0002BC1A
		internal TokenList()
		{
			this.tokens = new ArrayList();
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0002DA2D File Offset: 0x0002BC2D
		internal void Add(ExpressionToken ExpressionToken)
		{
			this.tokens.Add(ExpressionToken);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0002DA3C File Offset: 0x0002BC3C
		internal void Push(ExpressionToken ExpressionToken)
		{
			this.tokens.Insert(0, ExpressionToken);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0002DA4B File Offset: 0x0002BC4B
		internal ExpressionToken Peek()
		{
			return (ExpressionToken)this.tokens[0];
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0002DA5E File Offset: 0x0002BC5E
		internal ExpressionToken Extract()
		{
			ExpressionToken expressionToken = (ExpressionToken)this.tokens[0];
			this.tokens.RemoveAt(0);
			return expressionToken;
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0002DA7D File Offset: 0x0002BC7D
		internal int Count
		{
			get
			{
				return this.tokens.Count;
			}
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0002DA8A File Offset: 0x0002BC8A
		public IEnumerator GetEnumerator()
		{
			return this.tokens.GetEnumerator();
		}

		// Token: 0x0400065A RID: 1626
		private readonly ArrayList tokens;
	}
}
