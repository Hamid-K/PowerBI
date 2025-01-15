using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001C8 RID: 456
	public class EdmValueTermReferenceExpression : EdmElement, IEdmValueTermReferenceExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000990 RID: 2448 RVA: 0x000196F4 File Offset: 0x000178F4
		public EdmValueTermReferenceExpression(IEdmExpression baseExpression, IEdmValueTerm term)
			: this(baseExpression, term, null)
		{
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000196FF File Offset: 0x000178FF
		public EdmValueTermReferenceExpression(IEdmExpression baseExpression, IEdmValueTerm term, string qualifier)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(baseExpression, "baseExpression");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			this.baseExpression = baseExpression;
			this.term = term;
			this.qualifier = qualifier;
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00019734 File Offset: 0x00017934
		public IEdmExpression Base
		{
			get
			{
				return this.baseExpression;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0001973C File Offset: 0x0001793C
		public IEdmValueTerm Term
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00019744 File Offset: 0x00017944
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0001974C File Offset: 0x0001794C
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.ValueTermReference;
			}
		}

		// Token: 0x040004AC RID: 1196
		private readonly IEdmExpression baseExpression;

		// Token: 0x040004AD RID: 1197
		private readonly IEdmValueTerm term;

		// Token: 0x040004AE RID: 1198
		private readonly string qualifier;
	}
}
