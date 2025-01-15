using System;
using System.Security;

namespace Microsoft.PowerBI.ReportServer.WebApi.Logon
{
	// Token: 0x02000032 RID: 50
	public class UserCredentials
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000048A8 File Offset: 0x00002AA8
		// (set) Token: 0x060000EB RID: 235 RVA: 0x000048B0 File Offset: 0x00002AB0
		public string UserAndDomainName { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000048B9 File Offset: 0x00002AB9
		// (set) Token: 0x060000ED RID: 237 RVA: 0x000048C1 File Offset: 0x00002AC1
		public SecureString Password { get; private set; }

		// Token: 0x060000EE RID: 238 RVA: 0x000048CA File Offset: 0x00002ACA
		public UserCredentials(string userAndDomainName, SecureString password)
		{
			if (userAndDomainName == null)
			{
				throw new ArgumentNullException("username");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			this.UserAndDomainName = userAndDomainName;
			this.Password = password;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000048FC File Offset: 0x00002AFC
		public string UserNameOnly
		{
			get
			{
				string[] array = this.UserAndDomainName.Split(new char[] { '\\' });
				if (array.Length == 1)
				{
					return array[0];
				}
				return array[1];
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004930 File Offset: 0x00002B30
		public string DomainName
		{
			get
			{
				string[] array = this.UserAndDomainName.Split(new char[] { '\\' });
				if (array.Length == 1)
				{
					return null;
				}
				return array[0];
			}
		}
	}
}
