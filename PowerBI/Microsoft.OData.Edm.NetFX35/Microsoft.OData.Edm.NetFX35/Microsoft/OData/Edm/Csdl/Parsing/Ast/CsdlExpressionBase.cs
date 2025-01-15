using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000003 RID: 3
	internal abstract class CsdlExpressionBase : CsdlElement
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021C0 File Offset: 0x000003C0
		public CsdlExpressionBase(CsdlLocation location)
			: base(location)
		{
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13
		public abstract EdmExpressionKind ExpressionKind { get; }
	}
}
