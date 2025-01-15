using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000098 RID: 152
	public sealed class SharedKeyAuthCredential : BasicAuthCredential
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00003827 File Offset: 0x00001A27
		public SharedKeyAuthCredential(string sharedKey)
			: base("SharedKey", sharedKey)
		{
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00003639 File Offset: 0x00001839
		public string SharedKey
		{
			get
			{
				return base.Password;
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00003835 File Offset: 0x00001A35
		public override int GetHashCode()
		{
			return this.SharedKey.GetHashCode();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00003842 File Offset: 0x00001A42
		public override bool Equals(object other)
		{
			return this.Equals(other as SharedKeyAuthCredential);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00003842 File Offset: 0x00001A42
		public override bool Equals(IResourceCredential other)
		{
			return this.Equals(other as SharedKeyAuthCredential);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00003850 File Offset: 0x00001A50
		private bool Equals(SharedKeyAuthCredential other)
		{
			return other != null && this.SharedKey.Equals(other.SharedKey);
		}

		// Token: 0x04000181 RID: 385
		private const string DummyUsername = "SharedKey";
	}
}
