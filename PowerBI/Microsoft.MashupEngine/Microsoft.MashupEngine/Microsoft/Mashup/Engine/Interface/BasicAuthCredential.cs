using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000092 RID: 146
	public class BasicAuthCredential : UsernamePasswordCredential
	{
		// Token: 0x06000226 RID: 550 RVA: 0x0000343B File Offset: 0x0000163B
		public BasicAuthCredential(string username, string password)
			: base(username, password)
		{
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00003445 File Offset: 0x00001645
		public override int GetHashCode()
		{
			return base.Username.GetHashCode() ^ base.Password.GetHashCode();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00003497 File Offset: 0x00001697
		public override bool Equals(object other)
		{
			return this.Equals(other as BasicAuthCredential);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00003497 File Offset: 0x00001697
		public override bool Equals(IResourceCredential other)
		{
			return this.Equals(other as BasicAuthCredential);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000346C File Offset: 0x0000166C
		private bool Equals(BasicAuthCredential other)
		{
			return other != null && base.Username.Equals(other.Username) && base.Password.Equals(other.Password);
		}
	}
}
