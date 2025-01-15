using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D1 RID: 465
	internal abstract class CsdlExpressionBase : CsdlElement
	{
		// Token: 0x06000CAE RID: 3246 RVA: 0x00023B5B File Offset: 0x00021D5B
		public CsdlExpressionBase(CsdlLocation location)
			: base(location)
		{
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000CAF RID: 3247
		public abstract EdmExpressionKind ExpressionKind { get; }
	}
}
