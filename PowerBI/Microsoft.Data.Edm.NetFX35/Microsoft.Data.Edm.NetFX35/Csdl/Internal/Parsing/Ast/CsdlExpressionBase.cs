using System;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000009 RID: 9
	internal abstract class CsdlExpressionBase : CsdlElement
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000029D8 File Offset: 0x00000BD8
		public CsdlExpressionBase(CsdlLocation location)
			: base(location)
		{
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000036 RID: 54
		public abstract EdmExpressionKind ExpressionKind { get; }
	}
}
