using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000030 RID: 48
	internal class CsdlParameterReferenceExpression : CsdlExpressionBase
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00003763 File Offset: 0x00001963
		public CsdlParameterReferenceExpression(string parameter, CsdlLocation location)
			: base(location)
		{
			this.parameter = parameter;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003773 File Offset: 0x00001973
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.ParameterReference;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003777 File Offset: 0x00001977
		public string Parameter
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x04000049 RID: 73
		private readonly string parameter;
	}
}
