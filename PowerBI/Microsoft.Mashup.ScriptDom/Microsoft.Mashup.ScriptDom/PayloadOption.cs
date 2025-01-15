using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000387 RID: 903
	[Serializable]
	internal abstract class PayloadOption : TSqlFragment
	{
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x0016B3AC File Offset: 0x001695AC
		// (set) Token: 0x06002D6B RID: 11627 RVA: 0x0016B3B4 File Offset: 0x001695B4
		public PayloadOptionKinds Kind
		{
			get
			{
				return this._kind;
			}
			set
			{
				this._kind = value;
			}
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x0016B3BD File Offset: 0x001695BD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D53 RID: 7507
		private PayloadOptionKinds _kind;
	}
}
