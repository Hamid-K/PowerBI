using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003C7 RID: 967
	[DataContract(Name = "VersionRange", Namespace = "http://schemas.microsoft.com/2008/casdata")]
	internal class VersionRange
	{
		// Token: 0x0600220F RID: 8719 RVA: 0x00068EF1 File Offset: 0x000670F1
		public VersionRange(long startVersion, long endVersion)
		{
			this.StartVersion = startVersion;
			this.EndVersion = endVersion;
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x00068F07 File Offset: 0x00067107
		// (set) Token: 0x06002211 RID: 8721 RVA: 0x00068F0F File Offset: 0x0006710F
		[DataMember]
		public long StartVersion
		{
			get
			{
				return this.m_startVersion;
			}
			private set
			{
				this.m_startVersion = value;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x00068F18 File Offset: 0x00067118
		// (set) Token: 0x06002213 RID: 8723 RVA: 0x00068F20 File Offset: 0x00067120
		[DataMember]
		public long EndVersion
		{
			get
			{
				return this.m_endVersion;
			}
			private set
			{
				this.m_endVersion = value;
			}
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x00068F2C File Offset: 0x0006712C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", new object[] { this.m_startVersion, this.m_endVersion });
		}

		// Token: 0x04001599 RID: 5529
		private long m_startVersion;

		// Token: 0x0400159A RID: 5530
		private long m_endVersion;
	}
}
