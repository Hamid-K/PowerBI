using System;

namespace Microsoft.BIServer.Owin.Common.Middleware
{
	// Token: 0x02000019 RID: 25
	internal sealed class LogonUserParametersProvider
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002FEB File Offset: 0x000011EB
		public LogonUserParametersProvider(string credentialStr, string defaultDomain)
		{
			this.ParseCredential(credentialStr);
			this.DefaultDomain = (string.IsNullOrEmpty(defaultDomain) ? "." : defaultDomain);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003010 File Offset: 0x00001210
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003018 File Offset: 0x00001218
		public string DefaultDomain { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003021 File Offset: 0x00001221
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003047 File Offset: 0x00001247
		public string Domain
		{
			get
			{
				if (!string.IsNullOrEmpty(this._domain))
				{
					return this._domain;
				}
				if (!this.IsUsernameUPN)
				{
					return this.DefaultDomain;
				}
				return null;
			}
			private set
			{
				this._domain = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003050 File Offset: 0x00001250
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003058 File Offset: 0x00001258
		public string Username { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003061 File Offset: 0x00001261
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003069 File Offset: 0x00001269
		public bool IsUsernameUPN { get; private set; }

		// Token: 0x06000073 RID: 115 RVA: 0x00003072 File Offset: 0x00001272
		internal static bool IsUPNFormat(string accountName)
		{
			return LogonUserParametersProvider.StringContainsChar(accountName, '@');
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000307C File Offset: 0x0000127C
		internal static bool IsDomainUserFormat(string accountName)
		{
			return LogonUserParametersProvider.StringContainsChar(accountName, '\\');
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003086 File Offset: 0x00001286
		private static bool StringContainsChar(string str, char c)
		{
			return !string.IsNullOrEmpty(str) && str.IndexOf(c) >= 0;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000030A0 File Offset: 0x000012A0
		private void ParseCredential(string credentialStr)
		{
			if (credentialStr == null)
			{
				throw new ArgumentNullException("credentialStr");
			}
			if (string.IsNullOrEmpty(credentialStr))
			{
				this.Username = credentialStr;
			}
			else
			{
				string[] array = credentialStr.Split(new char[] { '\\' });
				if (array.Length == 2)
				{
					this.Domain = array[0];
					this.Username = array[1];
				}
				else
				{
					this.Username = credentialStr;
				}
			}
			this.IsUsernameUPN = LogonUserParametersProvider.IsUPNFormat(this.Username);
		}

		// Token: 0x04000045 RID: 69
		private const char UPNSeparator = '@';

		// Token: 0x04000046 RID: 70
		private const char DownLevelSeparator = '\\';

		// Token: 0x04000047 RID: 71
		private const string DefaultDomainDefault = ".";

		// Token: 0x04000048 RID: 72
		private string _domain;
	}
}
