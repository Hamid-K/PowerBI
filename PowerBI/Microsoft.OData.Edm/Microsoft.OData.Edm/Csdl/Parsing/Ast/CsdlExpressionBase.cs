using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E0 RID: 480
	internal abstract class CsdlExpressionBase : CsdlElement
	{
		// Token: 0x06000D63 RID: 3427 RVA: 0x00025D23 File Offset: 0x00023F23
		public CsdlExpressionBase(CsdlLocation location)
			: base(location)
		{
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000D64 RID: 3428
		public abstract EdmExpressionKind ExpressionKind { get; }
	}
}
