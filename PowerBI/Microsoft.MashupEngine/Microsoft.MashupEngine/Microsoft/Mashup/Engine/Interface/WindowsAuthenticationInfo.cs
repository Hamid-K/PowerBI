using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200000C RID: 12
	public sealed class WindowsAuthenticationInfo : AuthenticationInfo
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002391 File Offset: 0x00000591
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002399 File Offset: 0x00000599
		public string UsernameLabel { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000023A2 File Offset: 0x000005A2
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000023AA File Offset: 0x000005AA
		public string PasswordLabel { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000023B3 File Offset: 0x000005B3
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000023BB File Offset: 0x000005BB
		public bool SupportsAlternateCredentials { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023C4 File Offset: 0x000005C4
		public override AuthenticationKind AuthenticationKind
		{
			get
			{
				return AuthenticationKind.Windows;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023C7 File Offset: 0x000005C7
		public override string Name
		{
			get
			{
				return "Windows";
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023CE File Offset: 0x000005CE
		public override string GetPropertyLabel(string propertyName)
		{
			if (propertyName == "Username")
			{
				return this.UsernameLabel;
			}
			if (!(propertyName == "Password"))
			{
				return null;
			}
			return this.PasswordLabel;
		}
	}
}
