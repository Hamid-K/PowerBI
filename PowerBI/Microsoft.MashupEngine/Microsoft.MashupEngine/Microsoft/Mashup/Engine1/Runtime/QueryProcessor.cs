using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015DF RID: 5599
	public abstract class QueryProcessor
	{
		// Token: 0x06008CC7 RID: 36039 RVA: 0x001D8364 File Offset: 0x001D6564
		public bool TryFold(Query originalQuery, IExpression expression, out IExpression foldedExpression)
		{
			foldedExpression = this.Invoke(originalQuery, expression);
			return foldedExpression != null;
		}

		// Token: 0x06008CC8 RID: 36040
		public abstract IExpression Invoke(Query originalQuery, IExpression expression);
	}
}
