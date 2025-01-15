using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000385 RID: 901
	[Serializable]
	internal class ListenerIPEndpointProtocolOption : EndpointProtocolOption
	{
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06002D54 RID: 11604 RVA: 0x0016B215 File Offset: 0x00169415
		// (set) Token: 0x06002D55 RID: 11605 RVA: 0x0016B21D File Offset: 0x0016941D
		public bool IsAll
		{
			get
			{
				return this._isAll;
			}
			set
			{
				this._isAll = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x0016B226 File Offset: 0x00169426
		// (set) Token: 0x06002D57 RID: 11607 RVA: 0x0016B22E File Offset: 0x0016942E
		public Literal IPv6
		{
			get
			{
				return this._iPv6;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._iPv6 = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x0016B23E File Offset: 0x0016943E
		// (set) Token: 0x06002D59 RID: 11609 RVA: 0x0016B246 File Offset: 0x00169446
		public IPv4 IPv4PartOne
		{
			get
			{
				return this._iPv4PartOne;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._iPv4PartOne = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x0016B256 File Offset: 0x00169456
		// (set) Token: 0x06002D5B RID: 11611 RVA: 0x0016B25E File Offset: 0x0016945E
		public IPv4 IPv4PartTwo
		{
			get
			{
				return this._iPv4PartTwo;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._iPv4PartTwo = value;
			}
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x0016B26E File Offset: 0x0016946E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x0016B27C File Offset: 0x0016947C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.IPv6 != null)
			{
				this.IPv6.Accept(visitor);
			}
			if (this.IPv4PartOne != null)
			{
				this.IPv4PartOne.Accept(visitor);
			}
			if (this.IPv4PartTwo != null)
			{
				this.IPv4PartTwo.Accept(visitor);
			}
		}

		// Token: 0x04001D4B RID: 7499
		private bool _isAll;

		// Token: 0x04001D4C RID: 7500
		private Literal _iPv6;

		// Token: 0x04001D4D RID: 7501
		private IPv4 _iPv4PartOne;

		// Token: 0x04001D4E RID: 7502
		private IPv4 _iPv4PartTwo;
	}
}
