using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001CF RID: 463
	internal class CsdlApplyExpression : CsdlExpressionBase
	{
		// Token: 0x06000D27 RID: 3367 RVA: 0x00025A0E File Offset: 0x00023C0E
		public CsdlApplyExpression(string function, IEnumerable<CsdlExpressionBase> arguments, CsdlLocation location)
			: base(location)
		{
			this.function = function;
			this.arguments = new List<CsdlExpressionBase>(arguments);
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00011E72 File Offset: 0x00010072
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FunctionApplication;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00025A2A File Offset: 0x00023C2A
		public string Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00025A32 File Offset: 0x00023C32
		public IEnumerable<CsdlExpressionBase> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x04000742 RID: 1858
		private readonly string function;

		// Token: 0x04000743 RID: 1859
		private readonly List<CsdlExpressionBase> arguments;
	}
}
