using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200042B RID: 1067
	[Serializable]
	internal class SchemaObjectNameSnippet : SchemaObjectName
	{
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x0016F0D2 File Offset: 0x0016D2D2
		// (set) Token: 0x06003141 RID: 12609 RVA: 0x0016F0DA File Offset: 0x0016D2DA
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

		// Token: 0x06003142 RID: 12610 RVA: 0x0016F0E3 File Offset: 0x0016D2E3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x0016F0EF File Offset: 0x0016D2EF
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E62 RID: 7778
		private string _script;
	}
}
