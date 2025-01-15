using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Library.Expressions
{
	// Token: 0x02000196 RID: 406
	public class EdmValueTermReferenceExpression : EdmElement, IEdmValueTermReferenceExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x00018495 File Offset: 0x00016695
		public EdmValueTermReferenceExpression(IEdmExpression baseExpression, IEdmValueTerm term)
			: this(baseExpression, term, null)
		{
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000184A0 File Offset: 0x000166A0
		public EdmValueTermReferenceExpression(IEdmExpression baseExpression, IEdmValueTerm term, string qualifier)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(baseExpression, "baseExpression");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			this.baseExpression = baseExpression;
			this.term = term;
			this.qualifier = qualifier;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x000184D5 File Offset: 0x000166D5
		public IEdmExpression Base
		{
			get
			{
				return this.baseExpression;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x000184DD File Offset: 0x000166DD
		public IEdmValueTerm Term
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x000184E5 File Offset: 0x000166E5
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x000184ED File Offset: 0x000166ED
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.ValueTermReference;
			}
		}

		// Token: 0x0400045B RID: 1115
		private readonly IEdmExpression baseExpression;

		// Token: 0x0400045C RID: 1116
		private readonly IEdmValueTerm term;

		// Token: 0x0400045D RID: 1117
		private readonly string qualifier;
	}
}
