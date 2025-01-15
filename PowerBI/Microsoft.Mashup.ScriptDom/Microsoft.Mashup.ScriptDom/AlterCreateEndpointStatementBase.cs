using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200037C RID: 892
	[Serializable]
	internal abstract class AlterCreateEndpointStatementBase : TSqlStatement
	{
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x0016AEBD File Offset: 0x001690BD
		// (set) Token: 0x06002D20 RID: 11552 RVA: 0x0016AEC5 File Offset: 0x001690C5
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x0016AED5 File Offset: 0x001690D5
		// (set) Token: 0x06002D22 RID: 11554 RVA: 0x0016AEDD File Offset: 0x001690DD
		public EndpointState State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x0016AEE6 File Offset: 0x001690E6
		// (set) Token: 0x06002D24 RID: 11556 RVA: 0x0016AEEE File Offset: 0x001690EE
		public EndpointAffinity Affinity
		{
			get
			{
				return this._affinity;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._affinity = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06002D25 RID: 11557 RVA: 0x0016AEFE File Offset: 0x001690FE
		// (set) Token: 0x06002D26 RID: 11558 RVA: 0x0016AF06 File Offset: 0x00169106
		public EndpointProtocol Protocol
		{
			get
			{
				return this._protocol;
			}
			set
			{
				this._protocol = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06002D27 RID: 11559 RVA: 0x0016AF0F File Offset: 0x0016910F
		public IList<EndpointProtocolOption> ProtocolOptions
		{
			get
			{
				return this._protocolOptions;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x0016AF17 File Offset: 0x00169117
		// (set) Token: 0x06002D29 RID: 11561 RVA: 0x0016AF1F File Offset: 0x0016911F
		public EndpointType EndpointType
		{
			get
			{
				return this._endpointType;
			}
			set
			{
				this._endpointType = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x0016AF28 File Offset: 0x00169128
		public IList<PayloadOption> PayloadOptions
		{
			get
			{
				return this._payloadOptions;
			}
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x0016AF30 File Offset: 0x00169130
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.Affinity != null)
			{
				this.Affinity.Accept(visitor);
			}
			int i = 0;
			int count = this.ProtocolOptions.Count;
			while (i < count)
			{
				this.ProtocolOptions[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.PayloadOptions.Count;
			while (j < count2)
			{
				this.PayloadOptions[j].Accept(visitor);
				j++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D3C RID: 7484
		private Identifier _name;

		// Token: 0x04001D3D RID: 7485
		private EndpointState _state;

		// Token: 0x04001D3E RID: 7486
		private EndpointAffinity _affinity;

		// Token: 0x04001D3F RID: 7487
		private EndpointProtocol _protocol;

		// Token: 0x04001D40 RID: 7488
		private List<EndpointProtocolOption> _protocolOptions = new List<EndpointProtocolOption>();

		// Token: 0x04001D41 RID: 7489
		private EndpointType _endpointType;

		// Token: 0x04001D42 RID: 7490
		private List<PayloadOption> _payloadOptions = new List<PayloadOption>();
	}
}
