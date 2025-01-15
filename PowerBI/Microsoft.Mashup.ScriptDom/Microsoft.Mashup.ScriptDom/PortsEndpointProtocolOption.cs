using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000383 RID: 899
	[Serializable]
	internal class PortsEndpointProtocolOption : EndpointProtocolOption
	{
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06002D4A RID: 11594 RVA: 0x0016B1B9 File Offset: 0x001693B9
		// (set) Token: 0x06002D4B RID: 11595 RVA: 0x0016B1C1 File Offset: 0x001693C1
		public PortTypes PortTypes
		{
			get
			{
				return this._portTypes;
			}
			set
			{
				this._portTypes = value;
			}
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x0016B1CA File Offset: 0x001693CA
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x0016B1D6 File Offset: 0x001693D6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D49 RID: 7497
		private PortTypes _portTypes;
	}
}
