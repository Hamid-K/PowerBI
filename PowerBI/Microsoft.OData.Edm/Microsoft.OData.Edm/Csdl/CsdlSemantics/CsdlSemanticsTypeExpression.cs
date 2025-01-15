using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001B0 RID: 432
	internal abstract class CsdlSemanticsTypeExpression : CsdlSemanticsElement, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000C26 RID: 3110 RVA: 0x00021E2F File Offset: 0x0002002F
		protected CsdlSemanticsTypeExpression(CsdlExpressionTypeReference expressionUsage, CsdlSemanticsTypeDefinition type)
			: base(expressionUsage)
		{
			this.expressionUsage = expressionUsage;
			this.type = type;
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00021E46 File Offset: 0x00020046
		public IEdmType Definition
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00021E4E File Offset: 0x0002004E
		public bool IsNullable
		{
			get
			{
				return this.expressionUsage.IsNullable;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00021E5B File Offset: 0x0002005B
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.type.Model;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00021E68 File Offset: 0x00020068
		public override CsdlElement Element
		{
			get
			{
				return this.expressionUsage;
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0000C065 File Offset: 0x0000A265
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x0400070C RID: 1804
		private readonly CsdlExpressionTypeReference expressionUsage;

		// Token: 0x0400070D RID: 1805
		private readonly CsdlSemanticsTypeDefinition type;
	}
}
