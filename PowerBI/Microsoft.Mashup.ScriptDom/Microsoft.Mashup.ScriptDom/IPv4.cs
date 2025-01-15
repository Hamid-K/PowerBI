using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000386 RID: 902
	[Serializable]
	internal class IPv4 : TSqlFragment
	{
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x0016B2D4 File Offset: 0x001694D4
		// (set) Token: 0x06002D60 RID: 11616 RVA: 0x0016B2DC File Offset: 0x001694DC
		public Literal OctetOne
		{
			get
			{
				return this._octetOne;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._octetOne = value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x0016B2EC File Offset: 0x001694EC
		// (set) Token: 0x06002D62 RID: 11618 RVA: 0x0016B2F4 File Offset: 0x001694F4
		public Literal OctetTwo
		{
			get
			{
				return this._octetTwo;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._octetTwo = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06002D63 RID: 11619 RVA: 0x0016B304 File Offset: 0x00169504
		// (set) Token: 0x06002D64 RID: 11620 RVA: 0x0016B30C File Offset: 0x0016950C
		public Literal OctetThree
		{
			get
			{
				return this._octetThree;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._octetThree = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06002D65 RID: 11621 RVA: 0x0016B31C File Offset: 0x0016951C
		// (set) Token: 0x06002D66 RID: 11622 RVA: 0x0016B324 File Offset: 0x00169524
		public Literal OctetFour
		{
			get
			{
				return this._octetFour;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._octetFour = value;
			}
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x0016B334 File Offset: 0x00169534
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x0016B340 File Offset: 0x00169540
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.OctetOne != null)
			{
				this.OctetOne.Accept(visitor);
			}
			if (this.OctetTwo != null)
			{
				this.OctetTwo.Accept(visitor);
			}
			if (this.OctetThree != null)
			{
				this.OctetThree.Accept(visitor);
			}
			if (this.OctetFour != null)
			{
				this.OctetFour.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D4F RID: 7503
		private Literal _octetOne;

		// Token: 0x04001D50 RID: 7504
		private Literal _octetTwo;

		// Token: 0x04001D51 RID: 7505
		private Literal _octetThree;

		// Token: 0x04001D52 RID: 7506
		private Literal _octetFour;
	}
}
