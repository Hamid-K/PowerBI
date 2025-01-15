using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000429 RID: 1065
	[Serializable]
	internal class StatementListSnippet : StatementList
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x0016F076 File Offset: 0x0016D276
		// (set) Token: 0x06003137 RID: 12599 RVA: 0x0016F07E File Offset: 0x0016D27E
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

		// Token: 0x06003138 RID: 12600 RVA: 0x0016F087 File Offset: 0x0016D287
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x0016F093 File Offset: 0x0016D293
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E60 RID: 7776
		private string _script;
	}
}
