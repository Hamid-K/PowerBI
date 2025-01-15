using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000397 RID: 919
	[Serializable]
	internal class AlgorithmKeyOption : KeyOption
	{
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06002DD1 RID: 11729 RVA: 0x0016B937 File Offset: 0x00169B37
		// (set) Token: 0x06002DD2 RID: 11730 RVA: 0x0016B93F File Offset: 0x00169B3F
		public EncryptionAlgorithm Algorithm
		{
			get
			{
				return this._algorithm;
			}
			set
			{
				this._algorithm = value;
			}
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x0016B948 File Offset: 0x00169B48
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x0016B954 File Offset: 0x00169B54
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D71 RID: 7537
		private EncryptionAlgorithm _algorithm;
	}
}
