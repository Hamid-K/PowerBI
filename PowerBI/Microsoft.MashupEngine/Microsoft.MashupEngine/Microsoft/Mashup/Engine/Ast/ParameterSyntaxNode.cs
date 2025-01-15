using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B90 RID: 7056
	public sealed class ParameterSyntaxNode : IParameter
	{
		// Token: 0x0600B0B8 RID: 45240 RVA: 0x002435BA File Offset: 0x002417BA
		public ParameterSyntaxNode(Identifier identifier, IExpression type)
		{
			this.identifier = identifier;
			this.type = type;
		}

		// Token: 0x17002C20 RID: 11296
		// (get) Token: 0x0600B0B9 RID: 45241 RVA: 0x002435D0 File Offset: 0x002417D0
		public Identifier Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17002C21 RID: 11297
		// (get) Token: 0x0600B0BA RID: 45242 RVA: 0x002435D8 File Offset: 0x002417D8
		public IExpression Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04005AD2 RID: 23250
		private Identifier identifier;

		// Token: 0x04005AD3 RID: 23251
		private IExpression type;
	}
}
