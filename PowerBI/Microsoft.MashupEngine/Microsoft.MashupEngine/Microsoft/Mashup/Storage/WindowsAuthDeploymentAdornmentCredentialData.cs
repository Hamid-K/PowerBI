using System;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002069 RID: 8297
	[XmlType("WindowsAuthDeploymentAdornment")]
	public class WindowsAuthDeploymentAdornmentCredentialData : UserAnnotationAdornmentCredentialData
	{
		// Token: 0x0600CB26 RID: 52006 RVA: 0x00288300 File Offset: 0x00286500
		public WindowsAuthDeploymentAdornmentCredentialData()
		{
		}

		// Token: 0x0600CB27 RID: 52007 RVA: 0x00288308 File Offset: 0x00286508
		public WindowsAuthDeploymentAdornmentCredentialData(string impersonationMode, string username = null)
		{
			this.impersonationMode = impersonationMode;
			this.username = username;
		}

		// Token: 0x170030EA RID: 12522
		// (get) Token: 0x0600CB28 RID: 52008 RVA: 0x00227072 File Offset: 0x00225272
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.WindowsAuthDeployment;
			}
		}

		// Token: 0x170030EB RID: 12523
		// (get) Token: 0x0600CB29 RID: 52009 RVA: 0x0028831E File Offset: 0x0028651E
		// (set) Token: 0x0600CB2A RID: 52010 RVA: 0x00288326 File Offset: 0x00286526
		public string ImpersonationMode
		{
			get
			{
				return this.impersonationMode;
			}
			set
			{
				this.impersonationMode = value;
			}
		}

		// Token: 0x170030EC RID: 12524
		// (get) Token: 0x0600CB2B RID: 52011 RVA: 0x0028832F File Offset: 0x0028652F
		// (set) Token: 0x0600CB2C RID: 52012 RVA: 0x00288337 File Offset: 0x00286537
		public string Username
		{
			get
			{
				return this.username;
			}
			set
			{
				this.username = value;
			}
		}

		// Token: 0x0600CB2D RID: 52013 RVA: 0x00288340 File Offset: 0x00286540
		public override bool TryMergeWith(CredentialData credentialData)
		{
			WindowsAuthDeploymentAdornmentCredentialData windowsAuthDeploymentAdornmentCredentialData = credentialData as WindowsAuthDeploymentAdornmentCredentialData;
			if (windowsAuthDeploymentAdornmentCredentialData != null)
			{
				this.impersonationMode = windowsAuthDeploymentAdornmentCredentialData.impersonationMode;
				this.username = windowsAuthDeploymentAdornmentCredentialData.username;
				return true;
			}
			return false;
		}

		// Token: 0x0600CB2E RID: 52014 RVA: 0x0000336E File Offset: 0x0000156E
		public override void Validate()
		{
		}

		// Token: 0x04006717 RID: 26391
		private string impersonationMode;

		// Token: 0x04006718 RID: 26392
		private string username;
	}
}
