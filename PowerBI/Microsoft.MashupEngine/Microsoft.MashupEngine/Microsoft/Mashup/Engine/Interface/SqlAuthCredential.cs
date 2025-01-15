using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000091 RID: 145
	public sealed class SqlAuthCredential : UsernamePasswordCredential
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0000343B File Offset: 0x0000163B
		public SqlAuthCredential(string username, string password)
			: base(username, password)
		{
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00003445 File Offset: 0x00001645
		public override int GetHashCode()
		{
			return base.Username.GetHashCode() ^ base.Password.GetHashCode();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000345E File Offset: 0x0000165E
		public override bool Equals(object other)
		{
			return this.Equals(other as SqlAuthCredential);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000345E File Offset: 0x0000165E
		public override bool Equals(IResourceCredential other)
		{
			return this.Equals(other as SqlAuthCredential);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000346C File Offset: 0x0000166C
		private bool Equals(SqlAuthCredential other)
		{
			return other != null && base.Username.Equals(other.Username) && base.Password.Equals(other.Password);
		}
	}
}
