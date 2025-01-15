using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200070D RID: 1805
	[DataContract]
	[Serializable]
	public class BaseWithSsoRemoteEnvironment : BaseRemoteEnvironment
	{
		// Token: 0x0600393C RID: 14652 RVA: 0x000BF5F5 File Offset: 0x000BD7F5
		protected BaseWithSsoRemoteEnvironment()
		{
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000BF5FD File Offset: 0x000BD7FD
		protected BaseWithSsoRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault)
		{
			this.SecurityFromClientContext = securityFromClientContext;
			this.SSOApplication = ssoApplication;
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600393E RID: 14654 RVA: 0x000BF61E File Offset: 0x000BD81E
		// (set) Token: 0x0600393F RID: 14655 RVA: 0x000BF626 File Offset: 0x000BD826
		[DataMember]
		[Description("User Name (and Password) must be provided in the Client Context")]
		[Category("Security")]
		public bool SecurityFromClientContext { get; set; }

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06003940 RID: 14656 RVA: 0x000BF62F File Offset: 0x000BD82F
		// (set) Token: 0x06003941 RID: 14657 RVA: 0x000BF637 File Offset: 0x000BD837
		[DataMember]
		[Description("SSO affilicate application")]
		[Category("Security")]
		public string SSOApplication { get; set; }
	}
}
