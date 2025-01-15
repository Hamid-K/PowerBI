using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200042E RID: 1070
	[Serializable]
	internal class IdentifierSnippet : Identifier
	{
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x0016F15C File Offset: 0x0016D35C
		// (set) Token: 0x06003150 RID: 12624 RVA: 0x0016F164 File Offset: 0x0016D364
		public string Script
		{
			get
			{
				return this._script;
			}
			set
			{
				this._script = value;
			}
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x0016F16D File Offset: 0x0016D36D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x0016F179 File Offset: 0x0016D379
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E65 RID: 7781
		private string _script;
	}
}
