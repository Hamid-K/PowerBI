using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000009 RID: 9
	internal abstract class CsdlSemanticsExpression : CsdlSemanticsElement, IEdmExpression, IEdmElement
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000023DF File Offset: 0x000005DF
		protected CsdlSemanticsExpression(CsdlSemanticsSchema schema, CsdlExpressionBase element)
			: base(element)
		{
			this.schema = schema;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000021 RID: 33
		public abstract EdmExpressionKind ExpressionKind { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023EF File Offset: 0x000005EF
		public CsdlSemanticsSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023F7 File Offset: 0x000005F7
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x0400000B RID: 11
		private readonly CsdlSemanticsSchema schema;
	}
}
