using System;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000005 RID: 5
	public abstract class AuthenticationOptions
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020C9 File Offset: 0x000002C9
		protected AuthenticationOptions(string authenticationType)
		{
			this.Description = new AuthenticationDescription();
			this.AuthenticationType = authenticationType;
			this.AuthenticationMode = AuthenticationMode.Active;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020EA File Offset: 0x000002EA
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020F2 File Offset: 0x000002F2
		public string AuthenticationType
		{
			get
			{
				return this._authenticationType;
			}
			set
			{
				this._authenticationType = value;
				this.Description.AuthenticationType = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002107 File Offset: 0x00000307
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000210F File Offset: 0x0000030F
		public AuthenticationMode AuthenticationMode { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002118 File Offset: 0x00000318
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002120 File Offset: 0x00000320
		public AuthenticationDescription Description { get; set; }

		// Token: 0x04000006 RID: 6
		private string _authenticationType;
	}
}
