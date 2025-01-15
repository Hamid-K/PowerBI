using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200008F RID: 143
	public abstract class UsernamePasswordCredential : IResourceCredential, IEquatable<IResourceCredential>
	{
		// Token: 0x06000213 RID: 531 RVA: 0x00003305 File Offset: 0x00001505
		protected UsernamePasswordCredential(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000331B File Offset: 0x0000151B
		public string Username
		{
			get
			{
				return this.username;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00003323 File Offset: 0x00001523
		public string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000332B File Offset: 0x0000152B
		public override int GetHashCode()
		{
			return this.username.GetHashCode() ^ this.password.GetHashCode();
		}

		// Token: 0x06000217 RID: 535
		public abstract bool Equals(IResourceCredential other);

		// Token: 0x06000218 RID: 536 RVA: 0x00003344 File Offset: 0x00001544
		public virtual IEnumerable<string> GetCacheParts()
		{
			yield return this.Username;
			yield return this.Password;
			yield break;
		}

		// Token: 0x0400016E RID: 366
		private readonly string username;

		// Token: 0x0400016F RID: 367
		private readonly string password;
	}
}
