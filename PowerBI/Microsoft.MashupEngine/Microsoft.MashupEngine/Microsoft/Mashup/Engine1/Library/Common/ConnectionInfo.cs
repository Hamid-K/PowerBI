using System;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001039 RID: 4153
	public struct ConnectionInfo
	{
		// Token: 0x06006C53 RID: 27731 RVA: 0x001755A1 File Offset: 0x001737A1
		public ConnectionInfo(string connectionString, string alternateIdentity, Func<IDisposable> impersonate, bool requireEncryption)
		{
			this.connectionString = connectionString;
			this.alternateIdentity = alternateIdentity;
			this.impersonate = impersonate;
			this.requireEncryption = requireEncryption;
		}

		// Token: 0x17001ED2 RID: 7890
		// (get) Token: 0x06006C54 RID: 27732 RVA: 0x001755C0 File Offset: 0x001737C0
		public string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
		}

		// Token: 0x17001ED3 RID: 7891
		// (get) Token: 0x06006C55 RID: 27733 RVA: 0x001755C8 File Offset: 0x001737C8
		public string CacheKey
		{
			get
			{
				if (this.alternateIdentity == null)
				{
					return this.connectionString;
				}
				return this.connectionString + "##" + this.alternateIdentity;
			}
		}

		// Token: 0x17001ED4 RID: 7892
		// (get) Token: 0x06006C56 RID: 27734 RVA: 0x001755EF File Offset: 0x001737EF
		public Func<IDisposable> Impersonate
		{
			get
			{
				return this.impersonate;
			}
		}

		// Token: 0x17001ED5 RID: 7893
		// (get) Token: 0x06006C57 RID: 27735 RVA: 0x001755F7 File Offset: 0x001737F7
		public string AlternateIdentity
		{
			get
			{
				return this.alternateIdentity;
			}
		}

		// Token: 0x17001ED6 RID: 7894
		// (get) Token: 0x06006C58 RID: 27736 RVA: 0x001755FF File Offset: 0x001737FF
		public bool RequireEncryption
		{
			get
			{
				return this.requireEncryption;
			}
		}

		// Token: 0x04003C4B RID: 15435
		private readonly string connectionString;

		// Token: 0x04003C4C RID: 15436
		private readonly string alternateIdentity;

		// Token: 0x04003C4D RID: 15437
		private readonly Func<IDisposable> impersonate;

		// Token: 0x04003C4E RID: 15438
		private readonly bool requireEncryption;
	}
}
