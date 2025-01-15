using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000012 RID: 18
	public sealed class KeyAuthenticationInfo : AuthenticationInfo
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002464 File Offset: 0x00000664
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000246C File Offset: 0x0000066C
		public string KeyLabel { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002475 File Offset: 0x00000675
		public override AuthenticationKind AuthenticationKind
		{
			get
			{
				return AuthenticationKind.Key;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002478 File Offset: 0x00000678
		public override string Name
		{
			get
			{
				return "Key";
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002480 File Offset: 0x00000680
		public override IResourceCredential Normalize(string resourceKind, IResourceCredential credential)
		{
			BasicAuthCredential basicAuthCredential = (BasicAuthCredential)credential;
			if (resourceKind == "AzureTables")
			{
				return new SharedKeyAuthCredential(basicAuthCredential.Password);
			}
			return new FeedKeyCredential(basicAuthCredential.Password);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000024B8 File Offset: 0x000006B8
		public override string GetPropertyLabel(string propertyName)
		{
			if (propertyName == "Key")
			{
				return this.KeyLabel;
			}
			return null;
		}
	}
}
