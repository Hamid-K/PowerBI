using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001C2 RID: 450
	internal class CsdlCollectionExpression : CsdlExpressionBase
	{
		// Token: 0x06000C7A RID: 3194 RVA: 0x00023894 File Offset: 0x00021A94
		public CsdlCollectionExpression(CsdlTypeReference type, IEnumerable<CsdlExpressionBase> elementValues, CsdlLocation location)
			: base(location)
		{
			this.type = type;
			this.elementValues = new List<CsdlExpressionBase>(elementValues);
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00013A23 File Offset: 0x00011C23
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Collection;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x000238B0 File Offset: 0x00021AB0
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x000238B8 File Offset: 0x00021AB8
		public IEnumerable<CsdlExpressionBase> ElementValues
		{
			get
			{
				return this.elementValues;
			}
		}

		// Token: 0x040006CD RID: 1741
		private readonly CsdlTypeReference type;

		// Token: 0x040006CE RID: 1742
		private readonly List<CsdlExpressionBase> elementValues;
	}
}
