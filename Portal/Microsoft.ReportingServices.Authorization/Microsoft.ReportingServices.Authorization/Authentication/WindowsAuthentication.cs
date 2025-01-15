using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web;
using Microsoft.ReportingServices.Authorization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Authentication
{
	// Token: 0x02000005 RID: 5
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class WindowsAuthentication : IWindowsAuthenticationExtension2, IAuthenticationExtension2, IExtension
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public void SetConfiguration(string configuration)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002052 File Offset: 0x00000252
		public string LocalizedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
		public void GetUserInfo(out IIdentity userIdentity, out IntPtr userId)
		{
			string currentWindowsUserName = UserUtil.GetCurrentWindowsUserName();
			userId = IntPtr.Zero;
			if (HttpContext.Current != null)
			{
				HttpWorkerRequest httpWorkerRequest = (HttpWorkerRequest)((IServiceProvider)HttpContext.Current).GetService(typeof(HttpWorkerRequest));
				userId = httpWorkerRequest.GetUserToken();
				userIdentity = HttpContext.Current.User.Identity;
				if (!userIdentity.IsAuthenticated)
				{
					userIdentity = new WindowsIdentity(userId);
					return;
				}
			}
			else
			{
				userIdentity = new GenericIdentity(currentWindowsUserName);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C7 File Offset: 0x000002C7
		public void GetUserInfo(IRSRequestContext requestContext, out IIdentity userIdentity, out IntPtr userId)
		{
			this.GetUserInfo(out userIdentity, out userId);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D1 File Offset: 0x000002D1
		public bool IsValidPrincipalName(string principalName)
		{
			return Native.IsValidPrincipalName(principalName);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020D9 File Offset: 0x000002D9
		public byte[] PrincipalNameToSid(string userName)
		{
			return Native.NameToSid(userName);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E1 File Offset: 0x000002E1
		public string SidToPrincipalName(byte[] sid)
		{
			return Native.GetUserNameFromSid(sid);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E9 File Offset: 0x000002E9
		public bool LogonUser(string userName, string password, string authority)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.CustomAuth);
			return false;
		}
	}
}
