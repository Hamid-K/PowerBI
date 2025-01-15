using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.AdomdClient.Interop;
using Microsoft.AnalysisServices.AdomdClient.Network;

namespace Microsoft.AnalysisServices.AdomdClient.MsoId
{
	// Token: 0x02000129 RID: 297
	internal static class MsoIdAuthenticationProvider
	{
		// Token: 0x06000FA6 RID: 4006 RVA: 0x00035E24 File Offset: 0x00034024
		static MsoIdAuthenticationProvider()
		{
			MsoIdClient.Initialize(new Guid("CB0AA5A2-A483-499d-9521-C3CEB6E5AB23"), 1, UPDATE_FLAG.NO_UI, Array.Empty<KeyValuePair<IDCRL_OPTION_ID, object>>());
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00035E40 File Offset: 0x00034040
		public static string Authenticate(string userName, string password)
		{
			bool flag = string.IsNullOrEmpty(userName);
			if (flag)
			{
				try
				{
					userName = NetworkHelper.GetUserName(ExtendedNameFormat.NameUserPrincipal);
					goto IL_002C;
				}
				catch (Win32Exception ex)
				{
					throw new MsoIdAuthenticationException(MsoIdAuthenticationError.SsoWithNonDomainUser, ex);
				}
			}
			if (string.IsNullOrEmpty(password))
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.MissingPassword);
			}
			IL_002C:
			IntPtr zero = IntPtr.Zero;
			string text;
			try
			{
				MsoIdClient.CreateIdentity(userName, IDENTITY_FLAG.IDENTITY_SHARE_ALL, out zero);
				if (!flag)
				{
					MsoIdClient.SetCredentials(zero, "ps:password", password);
				}
				MsoIdClient.LogonIdentity(zero, null, LOGON_FLAG.LOGONIDENTITY_NONE, new RSTParams[]
				{
					new RSTParams
					{
						cbSize = (uint)Marshal.SizeOf(typeof(RSTParams)),
						wzServiceTarget = "analysis.windows.net",
						wzServicePolicy = "MBI_SSL"
					}
				});
				int num;
				int num2;
				int num3;
				MsoIdClient.GetAuthenticationState(zero, out num, out num2, out num3, out text);
				if (num != 296963)
				{
					throw new MsoIdAuthenticationException(flag ? MsoIdAuthenticationError.SsoAuthenticationFailed : MsoIdAuthenticationError.AuthenticationFailed);
				}
				string text2;
				uint num4;
				byte[] array;
				MsoIdClient.GetAuthenticationServiceToken(zero, "analysis.windows.net", "MBI_SSL", SERVICETOKENFLAGS.SERVICE_TOKEN_FROM_CACHE, out text2, out num4, out array);
				text = text2;
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					MsoIdClient.CloseIdentity(zero);
				}
			}
			return text;
		}

		// Token: 0x04000A66 RID: 2662
		private const string XmlaClientApplicationID = "CB0AA5A2-A483-499d-9521-C3CEB6E5AB23";

		// Token: 0x04000A67 RID: 2663
		private const string AzureAnalysisServicesTargetName = "analysis.windows.net";

		// Token: 0x04000A68 RID: 2664
		private const string AzureAnalysisServicesPolicy = "MBI_SSL";
	}
}
