using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000026 RID: 38
	internal class CsdlCollectionExpression : CsdlExpressionBase
	{
		// Token: 0x060000AD RID: 173 RVA: 0x000035AB File Offset: 0x000017AB
		public CsdlCollectionExpression(CsdlTypeReference type, IEnumerable<CsdlExpressionBase> elementValues, CsdlLocation location)
			: base(location)
		{
			this.type = type;
			this.elementValues = new List<CsdlExpressionBase>(elementValues);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000035C7 File Offset: 0x000017C7
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Collection;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000035CB File Offset: 0x000017CB
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000035D3 File Offset: 0x000017D3
		public IEnumerable<CsdlExpressionBase> ElementValues
		{
			get
			{
				return this.elementValues;
			}
		}

		// Token: 0x04000038 RID: 56
		private readonly CsdlTypeReference type;

		// Token: 0x04000039 RID: 57
		private readonly List<CsdlExpressionBase> elementValues;
	}
}
