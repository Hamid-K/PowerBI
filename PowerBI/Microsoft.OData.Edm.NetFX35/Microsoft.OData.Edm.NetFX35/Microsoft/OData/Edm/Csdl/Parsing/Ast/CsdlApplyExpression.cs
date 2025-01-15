using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000024 RID: 36
	internal class CsdlApplyExpression : CsdlExpressionBase
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00003550 File Offset: 0x00001750
		public CsdlApplyExpression(string function, IEnumerable<CsdlExpressionBase> arguments, CsdlLocation location)
			: base(location)
		{
			this.function = function;
			this.arguments = new List<CsdlExpressionBase>(arguments);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000356C File Offset: 0x0000176C
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.OperationApplication;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003570 File Offset: 0x00001770
		public string Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003578 File Offset: 0x00001778
		public IEnumerable<CsdlExpressionBase> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x04000034 RID: 52
		private readonly string function;

		// Token: 0x04000035 RID: 53
		private readonly List<CsdlExpressionBase> arguments;
	}
}
