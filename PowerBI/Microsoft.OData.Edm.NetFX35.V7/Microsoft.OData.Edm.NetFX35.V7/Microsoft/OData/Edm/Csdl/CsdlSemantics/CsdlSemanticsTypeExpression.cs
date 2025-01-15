using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200019F RID: 415
	internal abstract class CsdlSemanticsTypeExpression : CsdlSemanticsElement, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000B4F RID: 2895 RVA: 0x0001F6A3 File Offset: 0x0001D8A3
		protected CsdlSemanticsTypeExpression(CsdlExpressionTypeReference expressionUsage, CsdlSemanticsTypeDefinition type)
			: base(expressionUsage)
		{
			this.expressionUsage = expressionUsage;
			this.type = type;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0001F6BA File Offset: 0x0001D8BA
		public IEdmType Definition
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0001F6C2 File Offset: 0x0001D8C2
		public bool IsNullable
		{
			get
			{
				return this.expressionUsage.IsNullable;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0001F6CF File Offset: 0x0001D8CF
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.type.Model;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0001F6DC File Offset: 0x0001D8DC
		public override CsdlElement Element
		{
			get
			{
				return this.expressionUsage;
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0000C768 File Offset: 0x0000A968
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x04000681 RID: 1665
		private readonly CsdlExpressionTypeReference expressionUsage;

		// Token: 0x04000682 RID: 1666
		private readonly CsdlSemanticsTypeDefinition type;
	}
}
