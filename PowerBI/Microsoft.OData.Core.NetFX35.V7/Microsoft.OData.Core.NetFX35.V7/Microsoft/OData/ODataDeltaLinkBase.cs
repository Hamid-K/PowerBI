using System;

namespace Microsoft.OData
{
	// Token: 0x02000053 RID: 83
	public abstract class ODataDeltaLinkBase : ODataItem
	{
		// Token: 0x0600029B RID: 667 RVA: 0x00009BAA File Offset: 0x00007DAA
		protected ODataDeltaLinkBase(Uri source, Uri target, string relationship)
		{
			this.Source = source;
			this.Target = target;
			this.Relationship = relationship;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00009BC7 File Offset: 0x00007DC7
		// (set) Token: 0x0600029D RID: 669 RVA: 0x00009BCF File Offset: 0x00007DCF
		public Uri Source { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00009BD8 File Offset: 0x00007DD8
		// (set) Token: 0x0600029F RID: 671 RVA: 0x00009BE0 File Offset: 0x00007DE0
		public Uri Target { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00009BE9 File Offset: 0x00007DE9
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x00009BF1 File Offset: 0x00007DF1
		public string Relationship { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00009BFA File Offset: 0x00007DFA
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x00009C02 File Offset: 0x00007E02
		internal ODataDeltaSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataDeltaSerializationInfo.Validate(value);
			}
		}

		// Token: 0x04000180 RID: 384
		private ODataDeltaSerializationInfo serializationInfo;
	}
}
