using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002B1 RID: 689
	[Serializable]
	internal abstract class FullTextIndexOption : TSqlFragment
	{
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x0016603A File Offset: 0x0016423A
		// (set) Token: 0x06002843 RID: 10307 RVA: 0x00166042 File Offset: 0x00164242
		public FullTextIndexOptionKind OptionKind
		{
			get
			{
				return this._optionKind;
			}
			set
			{
				this._optionKind = value;
			}
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x0016604B File Offset: 0x0016424B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BD8 RID: 7128
		private FullTextIndexOptionKind _optionKind;
	}
}
