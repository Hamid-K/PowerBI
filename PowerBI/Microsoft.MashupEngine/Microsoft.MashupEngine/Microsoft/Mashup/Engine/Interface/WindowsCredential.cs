using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200009F RID: 159
	public sealed class WindowsCredential : IResourceCredential, IEquatable<IResourceCredential>
	{
		// Token: 0x06000282 RID: 642 RVA: 0x000020FD File Offset: 0x000002FD
		public WindowsCredential()
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00003D4F File Offset: 0x00001F4F
		public WindowsCredential(SafeHandle token, string username)
		{
			if (string.IsNullOrEmpty(username))
			{
				throw new ArgumentNullException("username");
			}
			this.token = token;
			this.username = username;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00003D78 File Offset: 0x00001F78
		public WindowsCredential(string username, string password)
		{
			if (string.IsNullOrEmpty(username))
			{
				throw new ArgumentNullException("username");
			}
			this.username = username;
			this.password = password;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00003DA4 File Offset: 0x00001FA4
		public override int GetHashCode()
		{
			if (this.username != null && this.password != null)
			{
				return 31415927 ^ this.username.GetHashCode() ^ this.password.GetHashCode();
			}
			if (this.username != null)
			{
				return 31415927 ^ this.username.GetHashCode();
			}
			return 27182818;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00003DFE File Offset: 0x00001FFE
		public override bool Equals(object other)
		{
			return this.Equals(other as WindowsCredential);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00003DFE File Offset: 0x00001FFE
		public bool Equals(IResourceCredential other)
		{
			return this.Equals(other as WindowsCredential);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00003E0C File Offset: 0x0000200C
		private bool Equals(WindowsCredential other)
		{
			return other != null && other.username == this.username && other.password == this.password && other.token == this.token;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00003E47 File Offset: 0x00002047
		public IEnumerable<string> GetCacheParts()
		{
			if (this.username != null)
			{
				yield return 31415927.ToString(CultureInfo.InvariantCulture);
				yield return this.username;
				yield return this.password;
			}
			else
			{
				yield return 27182818.ToString(CultureInfo.InvariantCulture);
			}
			yield break;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00003E57 File Offset: 0x00002057
		public bool OverrideCurrentUser
		{
			get
			{
				return this.username != null;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00003E62 File Offset: 0x00002062
		public string Username
		{
			get
			{
				return this.username;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00003E6A File Offset: 0x0000206A
		public string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00003E72 File Offset: 0x00002072
		public SafeHandle Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x04000193 RID: 403
		private const int defaultHashCode = 27182818;

		// Token: 0x04000194 RID: 404
		private const int overrideHashCode = 31415927;

		// Token: 0x04000195 RID: 405
		private readonly string username;

		// Token: 0x04000196 RID: 406
		private readonly string password;

		// Token: 0x04000197 RID: 407
		private readonly SafeHandle token;
	}
}
