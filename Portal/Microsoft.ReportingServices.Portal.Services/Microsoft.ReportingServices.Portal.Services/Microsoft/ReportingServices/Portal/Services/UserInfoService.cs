using System;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.ReportingServices.Portal.Interfaces.Services;

namespace Microsoft.ReportingServices.Portal.Services
{
	// Token: 0x0200001F RID: 31
	internal sealed class UserInfoService : IUserInfoService
	{
		// Token: 0x0600018A RID: 394 RVA: 0x0000C5E2 File Offset: 0x0000A7E2
		public UserInfoService()
			: this(new UserInfoService.UserInfoWrapper())
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000C5EF File Offset: 0x0000A7EF
		internal UserInfoService(UserInfoService.IUserInfoWrapper userInfoWrapper)
		{
			if (userInfoWrapper == null)
			{
				throw new ArgumentNullException("userInfoWrapper");
			}
			this._userInfoWrapper = userInfoWrapper;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000C60C File Offset: 0x0000A80C
		public string GetUserDisplayName(IIdentity identity)
		{
			string text = identity.Name;
			if (identity is WindowsIdentity || identity.AuthenticationType.Equals("basic", StringComparison.OrdinalIgnoreCase))
			{
				text = this.GetWindowsDisplayName(identity);
			}
			else if (identity is ClaimsIdentity)
			{
				text = this.GetClaimsDisplayName((ClaimsIdentity)identity);
			}
			return text;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000C65C File Offset: 0x0000A85C
		private string GetClaimsDisplayName(ClaimsIdentity identity)
		{
			Claim claim = identity.FindFirst("name");
			if (claim != null)
			{
				return claim.Value;
			}
			return identity.Name;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000C688 File Offset: 0x0000A888
		private string GetWindowsDisplayName(IIdentity identity)
		{
			ContextType[] array = new ContextType[2];
			array[0] = ContextType.Domain;
			foreach (ContextType contextType in array)
			{
				try
				{
					string displayName = this._userInfoWrapper.GetDisplayName(contextType, identity);
					if (!string.IsNullOrEmpty(displayName))
					{
						return displayName;
					}
				}
				catch (PrincipalServerDownException)
				{
				}
				catch (MultipleMatchesException)
				{
					Trace.TraceWarning("Multiple matches found for given user name - {0}", new object[] { identity.Name });
				}
				catch (Exception ex)
				{
					Trace.TraceWarning(ex.Message);
				}
			}
			return identity.Name;
		}

		// Token: 0x0400007C RID: 124
		private readonly UserInfoService.IUserInfoWrapper _userInfoWrapper;

		// Token: 0x02000142 RID: 322
		internal interface IUserInfoWrapper
		{
			// Token: 0x06000863 RID: 2147
			string GetDisplayName(ContextType type, IIdentity identity);
		}

		// Token: 0x02000143 RID: 323
		private class UserInfoWrapper : UserInfoService.IUserInfoWrapper
		{
			// Token: 0x06000864 RID: 2148 RVA: 0x0001FE9C File Offset: 0x0001E09C
			public string GetDisplayName(ContextType type, IIdentity identity)
			{
				string identityDomain = this.GetIdentityDomain(identity);
				using (PrincipalContext principalContext = ((identityDomain != null) ? new PrincipalContext(type, identityDomain) : new PrincipalContext(type)))
				{
					UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, identity.Name);
					if (userPrincipal != null)
					{
						return userPrincipal.DisplayName ?? userPrincipal.Name;
					}
				}
				return string.Empty;
			}

			// Token: 0x06000865 RID: 2149 RVA: 0x0001FF0C File Offset: 0x0001E10C
			private string GetIdentityDomain(IIdentity identity)
			{
				string[] array = identity.Name.Split(new char[] { '\\' });
				if (array.Length == 2)
				{
					return array[0];
				}
				return null;
			}
		}
	}
}
