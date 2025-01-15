using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000093 RID: 147
	public class SapBasicAuthCredential : UsernamePasswordCredential
	{
		// Token: 0x0600022B RID: 555 RVA: 0x000034A5 File Offset: 0x000016A5
		public SapBasicAuthCredential(string username, string password, string authentication)
			: base(username, password)
		{
			this.authentication = authentication;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000034B6 File Offset: 0x000016B6
		public string Authentication
		{
			get
			{
				return this.authentication;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000034BE File Offset: 0x000016BE
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.authentication.GetHashCode();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000034D2 File Offset: 0x000016D2
		public override bool Equals(object other)
		{
			return this.Equals(other as SapBasicAuthCredential);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000034D2 File Offset: 0x000016D2
		public override bool Equals(IResourceCredential other)
		{
			return this.Equals(other as SapBasicAuthCredential);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000034E0 File Offset: 0x000016E0
		private bool Equals(SapBasicAuthCredential other)
		{
			return other != null && other.Username == base.Username && other.Password == base.Password && other.Authentication == this.Authentication;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000351E File Offset: 0x0000171E
		public override IEnumerable<string> GetCacheParts()
		{
			yield return base.Username;
			yield return base.Password;
			yield return this.authentication;
			yield break;
		}

		// Token: 0x04000174 RID: 372
		private readonly string authentication;
	}
}
