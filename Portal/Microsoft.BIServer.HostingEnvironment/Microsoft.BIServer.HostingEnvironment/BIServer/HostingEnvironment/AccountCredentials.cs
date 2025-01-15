using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.BIServer.HostingEnvironment.Exceptions;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000004 RID: 4
	public class AccountCredentials
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AccountCredentials(string domain, string userId, string password, AccountType accountType)
		{
			this._accountType = accountType;
			this._domain = domain;
			this._userId = userId;
			this._password = password;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002075 File Offset: 0x00000275
		public string Domain
		{
			get
			{
				return this._domain;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000207D File Offset: 0x0000027D
		public string UserId
		{
			get
			{
				return this._userId;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002085 File Offset: 0x00000285
		public string Password
		{
			get
			{
				return this._password;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000208D File Offset: 0x0000028D
		public string DomainUser
		{
			get
			{
				return string.Format("{0}\\{1}", this.Domain, this.UserId);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020A5 File Offset: 0x000002A5
		public AccountType AccountType
		{
			get
			{
				return this._accountType;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020AD File Offset: 0x000002AD
		public NTAccount GetNtAccount()
		{
			return new NTAccount(this.Domain, this.UserId);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020C0 File Offset: 0x000002C0
		public virtual SecurityIdentifier GetSecurityIdentifier()
		{
			SecurityIdentifier securityIdentifierWithoutCheck;
			try
			{
				securityIdentifierWithoutCheck = this.GetSecurityIdentifierWithoutCheck();
			}
			catch (IdentityNotMappedException)
			{
				throw new AccountDoesNotExistException(this);
			}
			return securityIdentifierWithoutCheck;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020F0 File Offset: 0x000002F0
		public bool Exists()
		{
			bool flag;
			try
			{
				this.GetSecurityIdentifierWithoutCheck();
				flag = true;
			}
			catch (IdentityNotMappedException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002120 File Offset: 0x00000320
		public virtual string GetSecurityDescriptor()
		{
			RawAcl rawAcl = new RawAcl(0, 1);
			SecurityIdentifier securityIdentifier = this.GetSecurityIdentifier();
			rawAcl.InsertAce(0, new CommonAce(AceFlags.None, AceQualifier.AccessAllowed, 536870912, securityIdentifier, false, null));
			return new RawSecurityDescriptor(ControlFlags.DiscretionaryAclPresent | ControlFlags.SelfRelative, null, null, null, rawAcl).GetSddlForm(AccessControlSections.All);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002167 File Offset: 0x00000367
		private SecurityIdentifier GetSecurityIdentifierWithoutCheck()
		{
			return (SecurityIdentifier)this.GetNtAccount().Translate(typeof(SecurityIdentifier));
		}

		// Token: 0x0400002B RID: 43
		private readonly string _domain;

		// Token: 0x0400002C RID: 44
		private readonly string _userId;

		// Token: 0x0400002D RID: 45
		private readonly string _password;

		// Token: 0x0400002E RID: 46
		private readonly AccountType _accountType;
	}
}
