using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001D0 RID: 464
	public class EdmApplyExpression : EdmElement, IEdmApplyExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060009B4 RID: 2484 RVA: 0x0001991F File Offset: 0x00017B1F
		public EdmApplyExpression(IEdmOperation appliedOperation, params IEdmExpression[] arguments)
			: this(appliedOperation, (IEnumerable<IEdmExpression>)arguments)
		{
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001992E File Offset: 0x00017B2E
		public EdmApplyExpression(IEdmOperation appliedOperation, IEnumerable<IEdmExpression> arguments)
			: this(new EdmOperationReferenceExpression(EdmUtil.CheckArgumentNull<IEdmOperation>(appliedOperation, "appliedFunction")), arguments)
		{
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00019947 File Offset: 0x00017B47
		public EdmApplyExpression(IEdmExpression appliedOperation, IEnumerable<IEdmExpression> arguments)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(appliedOperation, "appliedFunction");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmExpression>>(arguments, "arguments");
			this.appliedOperation = appliedOperation;
			this.arguments = arguments;
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00019975 File Offset: 0x00017B75
		public IEdmExpression AppliedOperation
		{
			get
			{
				return this.appliedOperation;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0001997D File Offset: 0x00017B7D
		public IEnumerable<IEdmExpression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00019985 File Offset: 0x00017B85
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.OperationApplication;
			}
		}

		// Token: 0x040004BB RID: 1211
		private readonly IEdmExpression appliedOperation;

		// Token: 0x040004BC RID: 1212
		private readonly IEnumerable<IEdmExpression> arguments;
	}
}
