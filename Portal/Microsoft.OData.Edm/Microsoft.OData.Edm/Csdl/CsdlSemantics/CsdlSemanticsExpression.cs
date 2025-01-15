using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000172 RID: 370
	internal abstract class CsdlSemanticsExpression : CsdlSemanticsElement, IEdmExpression, IEdmElement
	{
		// Token: 0x060009FB RID: 2555 RVA: 0x0001BE4B File Offset: 0x0001A04B
		protected CsdlSemanticsExpression(CsdlSemanticsSchema schema, CsdlExpressionBase element)
			: base(element)
		{
			this.schema = schema;
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060009FC RID: 2556
		public abstract EdmExpressionKind ExpressionKind { get; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0001BE5B File Offset: 0x0001A05B
		public CsdlSemanticsSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0001BE63 File Offset: 0x0001A063
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x0400060D RID: 1549
		private readonly CsdlSemanticsSchema schema;
	}
}
