using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000EF RID: 239
	public class EdmApplyExpression : EdmElement, IEdmApplyExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x00013946 File Offset: 0x00011B46
		public EdmApplyExpression(IEdmFunction appliedFunction, params IEdmExpression[] arguments)
			: this(appliedFunction, arguments)
		{
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00013950 File Offset: 0x00011B50
		public EdmApplyExpression(IEdmFunction appliedFunction, IEnumerable<IEdmExpression> arguments)
		{
			EdmUtil.CheckArgumentNull<IEdmFunction>(appliedFunction, "appliedFunction");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmExpression>>(arguments, "arguments");
			this.appliedFunction = appliedFunction;
			this.arguments = arguments;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001397E File Offset: 0x00011B7E
		public IEdmFunction AppliedFunction
		{
			get
			{
				return this.appliedFunction;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00013986 File Offset: 0x00011B86
		public IEnumerable<IEdmExpression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001398E File Offset: 0x00011B8E
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FunctionApplication;
			}
		}

		// Token: 0x0400040F RID: 1039
		private readonly IEdmFunction appliedFunction;

		// Token: 0x04000410 RID: 1040
		private readonly IEnumerable<IEdmExpression> arguments;
	}
}
