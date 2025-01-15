using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000384 RID: 900
	[Serializable]
	internal class CompressionEndpointProtocolOption : EndpointProtocolOption
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06002D4F RID: 11599 RVA: 0x0016B1E7 File Offset: 0x001693E7
		// (set) Token: 0x06002D50 RID: 11600 RVA: 0x0016B1EF File Offset: 0x001693EF
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				this._isEnabled = value;
			}
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x0016B1F8 File Offset: 0x001693F8
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x0016B204 File Offset: 0x00169404
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D4A RID: 7498
		private bool _isEnabled;
	}
}
