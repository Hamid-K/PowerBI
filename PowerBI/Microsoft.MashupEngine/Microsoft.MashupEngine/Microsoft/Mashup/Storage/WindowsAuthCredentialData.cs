using System;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200205C RID: 8284
	[XmlType("WindowsAuth")]
	public class WindowsAuthCredentialData : UsernamePasswordCredentialData
	{
		// Token: 0x0600CAA9 RID: 51881 RVA: 0x002876B2 File Offset: 0x002858B2
		public WindowsAuthCredentialData()
		{
		}

		// Token: 0x170030C2 RID: 12482
		// (get) Token: 0x0600CAAA RID: 51882 RVA: 0x002879BD File Offset: 0x00285BBD
		// (set) Token: 0x0600CAAB RID: 51883 RVA: 0x002879C5 File Offset: 0x00285BC5
		public string IdentitySource
		{
			get
			{
				return this.identitySource;
			}
			set
			{
				this.identitySource = value;
			}
		}

		// Token: 0x0600CAAC RID: 51884 RVA: 0x002879CE File Offset: 0x00285BCE
		public WindowsAuthCredentialData(string username, string password)
			: this(null, username, password)
		{
		}

		// Token: 0x0600CAAD RID: 51885 RVA: 0x002879D9 File Offset: 0x00285BD9
		public WindowsAuthCredentialData(string identitySource, string username, string password)
		{
			this.identitySource = identitySource;
			base.Username = username;
			base.Password = password;
		}

		// Token: 0x170030C3 RID: 12483
		// (get) Token: 0x0600CAAE RID: 51886 RVA: 0x00002461 File Offset: 0x00000661
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.WindowsAuth;
			}
		}

		// Token: 0x170030C4 RID: 12484
		// (get) Token: 0x0600CAAF RID: 51887 RVA: 0x002879F6 File Offset: 0x00285BF6
		public bool OverrideCurrentUser
		{
			get
			{
				return base.Username != null;
			}
		}

		// Token: 0x0600CAB0 RID: 51888 RVA: 0x0000336E File Offset: 0x0000156E
		public override void InitializeWithDefaults()
		{
		}

		// Token: 0x0600CAB1 RID: 51889 RVA: 0x00287A04 File Offset: 0x00285C04
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			if (this.IdentitySource == "Thread" && context != null && context.ThreadIdentity != null)
			{
				return new WindowsCredential(context.ThreadIdentity, context.Username);
			}
			if (this.IdentitySource == "Service")
			{
				return new WindowsCredential(base.Username, null);
			}
			if (this.OverrideCurrentUser)
			{
				return new WindowsCredential(base.Username, base.Password);
			}
			return new WindowsCredential();
		}

		// Token: 0x0600CAB2 RID: 51890 RVA: 0x00287A80 File Offset: 0x00285C80
		public override bool TryMergeWith(CredentialData credentialData)
		{
			WindowsAuthCredentialData windowsAuthCredentialData = credentialData as WindowsAuthCredentialData;
			if (windowsAuthCredentialData != null)
			{
				bool overrideCurrentUser = this.OverrideCurrentUser;
				this.IdentitySource = windowsAuthCredentialData.IdentitySource;
				base.Username = windowsAuthCredentialData.Username;
				if (windowsAuthCredentialData.Password != null && this.OverrideCurrentUser)
				{
					base.Password = windowsAuthCredentialData.Password;
				}
				else if (overrideCurrentUser && !this.OverrideCurrentUser)
				{
					base.Password = null;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600CAB3 RID: 51891 RVA: 0x00287AEC File Offset: 0x00285CEC
		public override void Validate()
		{
			if (!string.IsNullOrEmpty(this.identitySource))
			{
				string text = this.identitySource;
				if (!(text == "Thread") && !(text == "Process"))
				{
					if (!(text == "Service"))
					{
						if (!(text == "Explicit"))
						{
							throw StorageExceptions.CredentialValidationException(Strings.IdentitySource_Not_Valid, null);
						}
						base.Validate();
						return;
					}
					else if (string.IsNullOrEmpty(base.Username) || !string.IsNullOrEmpty(base.Password))
					{
						throw StorageExceptions.CredentialValidationException(Strings.User_Name_Not_Valid, null);
					}
				}
				else if (base.Username != null || base.Password != null)
				{
					throw StorageExceptions.CredentialValidationException(Strings.User_Name_Not_Valid, null);
				}
			}
			else if (this.OverrideCurrentUser)
			{
				base.Validate();
			}
		}

		// Token: 0x04006704 RID: 26372
		private string identitySource;
	}
}
