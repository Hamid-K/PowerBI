using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E8 RID: 232
	public class EdmApplyExpression : EdmElement, IEdmApplyExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000721 RID: 1825 RVA: 0x00011E2A File Offset: 0x0001002A
		public EdmApplyExpression(IEdmFunction appliedFunction, params IEdmExpression[] arguments)
			: this(appliedFunction, arguments)
		{
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00011E34 File Offset: 0x00010034
		public EdmApplyExpression(IEdmFunction appliedFunction, IEnumerable<IEdmExpression> arguments)
		{
			EdmUtil.CheckArgumentNull<IEdmFunction>(appliedFunction, "appliedFunction");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmExpression>>(arguments, "arguments");
			this.appliedFunction = appliedFunction;
			this.arguments = arguments;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x00011E62 File Offset: 0x00010062
		public IEdmFunction AppliedFunction
		{
			get
			{
				return this.appliedFunction;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00011E6A File Offset: 0x0001006A
		public IEnumerable<IEdmExpression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00011E72 File Offset: 0x00010072
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FunctionApplication;
			}
		}

		// Token: 0x04000303 RID: 771
		private readonly IEdmFunction appliedFunction;

		// Token: 0x04000304 RID: 772
		private readonly IEnumerable<IEdmExpression> arguments;
	}
}
