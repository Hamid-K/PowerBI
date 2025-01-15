using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000095 RID: 149
	public sealed class FeedKeyCredential : BasicAuthCredential
	{
		// Token: 0x0600023A RID: 570 RVA: 0x0000362B File Offset: 0x0000182B
		public FeedKeyCredential(string feedKey)
			: base("FeedKey", feedKey)
		{
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00003639 File Offset: 0x00001839
		public string FeedKey
		{
			get
			{
				return base.Password;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00003641 File Offset: 0x00001841
		public override int GetHashCode()
		{
			return this.FeedKey.GetHashCode();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000364E File Offset: 0x0000184E
		public override bool Equals(object other)
		{
			return this.Equals(other as FeedKeyCredential);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000364E File Offset: 0x0000184E
		public override bool Equals(IResourceCredential other)
		{
			return this.Equals(other as FeedKeyCredential);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000365C File Offset: 0x0000185C
		private bool Equals(FeedKeyCredential other)
		{
			return other != null && this.FeedKey.Equals(other.FeedKey);
		}

		// Token: 0x04000179 RID: 377
		private const string DummyUsername = "FeedKey";
	}
}
