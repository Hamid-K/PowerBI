using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000392 RID: 914
	[Serializable]
	internal class EncryptionPayloadOption : PayloadOption
	{
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x0016B6FE File Offset: 0x001698FE
		// (set) Token: 0x06002DB3 RID: 11699 RVA: 0x0016B706 File Offset: 0x00169906
		public EndpointEncryptionSupport EncryptionSupport
		{
			get
			{
				return this._encryptionSupport;
			}
			set
			{
				this._encryptionSupport = value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x0016B70F File Offset: 0x0016990F
		// (set) Token: 0x06002DB5 RID: 11701 RVA: 0x0016B717 File Offset: 0x00169917
		public EncryptionAlgorithmPreference AlgorithmPartOne
		{
			get
			{
				return this._algorithmPartOne;
			}
			set
			{
				this._algorithmPartOne = value;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x0016B720 File Offset: 0x00169920
		// (set) Token: 0x06002DB7 RID: 11703 RVA: 0x0016B728 File Offset: 0x00169928
		public EncryptionAlgorithmPreference AlgorithmPartTwo
		{
			get
			{
				return this._algorithmPartTwo;
			}
			set
			{
				this._algorithmPartTwo = value;
			}
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x0016B731 File Offset: 0x00169931
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x0016B73D File Offset: 0x0016993D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D67 RID: 7527
		private EndpointEncryptionSupport _encryptionSupport;

		// Token: 0x04001D68 RID: 7528
		private EncryptionAlgorithmPreference _algorithmPartOne;

		// Token: 0x04001D69 RID: 7529
		private EncryptionAlgorithmPreference _algorithmPartTwo;
	}
}
