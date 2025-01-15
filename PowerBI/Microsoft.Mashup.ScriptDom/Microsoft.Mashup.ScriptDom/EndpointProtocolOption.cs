using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000380 RID: 896
	[Serializable]
	internal abstract class EndpointProtocolOption : TSqlFragment
	{
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06002D3C RID: 11580 RVA: 0x0016B120 File Offset: 0x00169320
		// (set) Token: 0x06002D3D RID: 11581 RVA: 0x0016B128 File Offset: 0x00169328
		public EndpointProtocolOptions Kind
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

		// Token: 0x06002D3E RID: 11582 RVA: 0x0016B131 File Offset: 0x00169331
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D46 RID: 7494
		private EndpointProtocolOptions _kind;
	}
}
