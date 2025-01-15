using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D1 RID: 465
	internal class CsdlCollectionExpression : CsdlExpressionBase
	{
		// Token: 0x06000D2F RID: 3375 RVA: 0x00025A61 File Offset: 0x00023C61
		public CsdlCollectionExpression(CsdlTypeReference type, IEnumerable<CsdlExpressionBase> elementValues, CsdlLocation location)
			: base(location)
		{
			this.type = type;
			this.elementValues = new List<CsdlExpressionBase>(elementValues);
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00011F07 File Offset: 0x00010107
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Collection;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x00025A7D File Offset: 0x00023C7D
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00025A85 File Offset: 0x00023C85
		public IEnumerable<CsdlExpressionBase> ElementValues
		{
			get
			{
				return this.elementValues;
			}
		}

		// Token: 0x04000746 RID: 1862
		private readonly CsdlTypeReference type;

		// Token: 0x04000747 RID: 1863
		private readonly List<CsdlExpressionBase> elementValues;
	}
}
