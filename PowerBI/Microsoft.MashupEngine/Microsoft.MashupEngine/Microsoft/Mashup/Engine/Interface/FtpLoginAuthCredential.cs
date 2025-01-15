using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000099 RID: 153
	public sealed class FtpLoginAuthCredential : UsernamePasswordCredential
	{
		// Token: 0x06000258 RID: 600 RVA: 0x0000343B File Offset: 0x0000163B
		public FtpLoginAuthCredential(string username, string password)
			: base(username, password)
		{
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00003445 File Offset: 0x00001645
		public override int GetHashCode()
		{
			return base.Username.GetHashCode() ^ base.Password.GetHashCode();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00003868 File Offset: 0x00001A68
		public override bool Equals(object other)
		{
			return this.Equals(other as FtpLoginAuthCredential);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00003868 File Offset: 0x00001A68
		public override bool Equals(IResourceCredential other)
		{
			return this.Equals(other as FtpLoginAuthCredential);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000346C File Offset: 0x0000166C
		private bool Equals(FtpLoginAuthCredential other)
		{
			return other != null && base.Username.Equals(other.Username) && base.Password.Equals(other.Password);
		}
	}
}
