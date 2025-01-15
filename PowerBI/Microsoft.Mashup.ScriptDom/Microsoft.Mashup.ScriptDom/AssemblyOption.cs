using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000276 RID: 630
	[Serializable]
	internal class AssemblyOption : TSqlFragment
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x0016477A File Offset: 0x0016297A
		// (set) Token: 0x060026D5 RID: 9941 RVA: 0x00164782 File Offset: 0x00162982
		public AssemblyOptionKind OptionKind
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

		// Token: 0x060026D6 RID: 9942 RVA: 0x0016478B File Offset: 0x0016298B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x00164797 File Offset: 0x00162997
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B6E RID: 7022
		private AssemblyOptionKind _optionKind;
	}
}
