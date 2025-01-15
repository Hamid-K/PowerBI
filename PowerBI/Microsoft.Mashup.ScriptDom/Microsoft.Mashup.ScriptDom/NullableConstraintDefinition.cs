using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000348 RID: 840
	[Serializable]
	internal class NullableConstraintDefinition : ConstraintDefinition
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x00169632 File Offset: 0x00167832
		// (set) Token: 0x06002BCB RID: 11211 RVA: 0x0016963A File Offset: 0x0016783A
		public bool Nullable
		{
			get
			{
				return this._nullable;
			}
			set
			{
				this._nullable = value;
			}
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x00169643 File Offset: 0x00167843
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x0016964F File Offset: 0x0016784F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CD0 RID: 7376
		private bool _nullable;
	}
}
