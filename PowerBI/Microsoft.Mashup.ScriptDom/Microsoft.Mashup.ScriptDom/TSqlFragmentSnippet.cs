using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200042C RID: 1068
	[Serializable]
	internal class TSqlFragmentSnippet : TSqlFragment
	{
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x0016F100 File Offset: 0x0016D300
		// (set) Token: 0x06003146 RID: 12614 RVA: 0x0016F108 File Offset: 0x0016D308
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

		// Token: 0x06003147 RID: 12615 RVA: 0x0016F111 File Offset: 0x0016D311
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x0016F11D File Offset: 0x0016D31D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E63 RID: 7779
		private string _script;
	}
}
