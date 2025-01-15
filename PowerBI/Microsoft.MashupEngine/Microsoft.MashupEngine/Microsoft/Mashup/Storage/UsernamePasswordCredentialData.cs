using System;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002053 RID: 8275
	public abstract class UsernamePasswordCredentialData : CredentialData
	{
		// Token: 0x0600CA73 RID: 51827 RVA: 0x00287642 File Offset: 0x00285842
		public UsernamePasswordCredentialData()
		{
		}

		// Token: 0x0600CA74 RID: 51828 RVA: 0x0028764A File Offset: 0x0028584A
		public UsernamePasswordCredentialData(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		// Token: 0x170030B2 RID: 12466
		// (get) Token: 0x0600CA75 RID: 51829 RVA: 0x00287660 File Offset: 0x00285860
		// (set) Token: 0x0600CA76 RID: 51830 RVA: 0x00287668 File Offset: 0x00285868
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

		// Token: 0x170030B3 RID: 12467
		// (get) Token: 0x0600CA77 RID: 51831 RVA: 0x00287671 File Offset: 0x00285871
		// (set) Token: 0x0600CA78 RID: 51832 RVA: 0x00287679 File Offset: 0x00285879
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x170030B4 RID: 12468
		// (get) Token: 0x0600CA79 RID: 51833 RVA: 0x00002105 File Offset: 0x00000305
		public override CredentialDataKind Kind
		{
			get
			{
				return CredentialDataKind.Credential;
			}
		}

		// Token: 0x0600CA7A RID: 51834 RVA: 0x00287682 File Offset: 0x00285882
		public override void Validate()
		{
			if (string.IsNullOrEmpty(this.username))
			{
				throw StorageExceptions.CredentialValidationException(Strings.User_Name_Not_Specified, null);
			}
		}

		// Token: 0x0600CA7B RID: 51835 RVA: 0x0028769D File Offset: 0x0028589D
		public override void InitializeWithDefaults()
		{
			if (this.Password == null)
			{
				this.Password = "";
			}
		}

		// Token: 0x040066FD RID: 26365
		private string username;

		// Token: 0x040066FE RID: 26366
		private string password;
	}
}
