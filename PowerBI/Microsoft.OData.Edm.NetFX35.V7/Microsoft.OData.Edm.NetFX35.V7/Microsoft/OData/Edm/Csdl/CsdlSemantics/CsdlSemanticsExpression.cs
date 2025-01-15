using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000163 RID: 355
	internal abstract class CsdlSemanticsExpression : CsdlSemanticsElement, IEdmExpression, IEdmElement
	{
		// Token: 0x06000940 RID: 2368 RVA: 0x00019D4B File Offset: 0x00017F4B
		protected CsdlSemanticsExpression(CsdlSemanticsSchema schema, CsdlExpressionBase element)
			: base(element)
		{
			this.schema = schema;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000941 RID: 2369
		public abstract EdmExpressionKind ExpressionKind { get; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00019D5B File Offset: 0x00017F5B
		public CsdlSemanticsSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00019D63 File Offset: 0x00017F63
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x04000592 RID: 1426
		private readonly CsdlSemanticsSchema schema;
	}
}
