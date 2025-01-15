using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200009F RID: 159
	internal abstract class CsdlSemanticsTypeExpression : CsdlSemanticsElement, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060002BA RID: 698 RVA: 0x00006A57 File Offset: 0x00004C57
		protected CsdlSemanticsTypeExpression(CsdlExpressionTypeReference expressionUsage, CsdlSemanticsTypeDefinition type)
			: base(expressionUsage)
		{
			this.expressionUsage = expressionUsage;
			this.type = type;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00006A6E File Offset: 0x00004C6E
		public IEdmType Definition
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00006A76 File Offset: 0x00004C76
		public bool IsNullable
		{
			get
			{
				return this.expressionUsage.IsNullable;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00006A83 File Offset: 0x00004C83
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.type.Model;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00006A90 File Offset: 0x00004C90
		public override CsdlElement Element
		{
			get
			{
				return this.expressionUsage;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00006A98 File Offset: 0x00004C98
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x0400011C RID: 284
		private readonly CsdlExpressionTypeReference expressionUsage;

		// Token: 0x0400011D RID: 285
		private readonly CsdlSemanticsTypeDefinition type;
	}
}
