using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001C0 RID: 448
	internal class CsdlApplyExpression : CsdlExpressionBase
	{
		// Token: 0x06000C72 RID: 3186 RVA: 0x00023841 File Offset: 0x00021A41
		public CsdlApplyExpression(string function, IEnumerable<CsdlExpressionBase> arguments, CsdlLocation location)
			: base(location)
		{
			this.function = function;
			this.arguments = new List<CsdlExpressionBase>(arguments);
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0001398E File Offset: 0x00011B8E
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FunctionApplication;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0002385D File Offset: 0x00021A5D
		public string Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00023865 File Offset: 0x00021A65
		public IEnumerable<CsdlExpressionBase> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x040006C9 RID: 1737
		private readonly string function;

		// Token: 0x040006CA RID: 1738
		private readonly List<CsdlExpressionBase> arguments;
	}
}
