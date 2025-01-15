using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32;

namespace Microsoft.BIServer.Configuration.LSA
{
	// Token: 0x02000035 RID: 53
	public static class LocalSecurityAuthority
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00007A68 File Offset: 0x00005C68
		public static void AddPrivilegeToAccount(SecurityIdentifier sid, string privilegeName)
		{
			byte[] array = new byte[SecurityIdentifier.MaxBinaryLength];
			sid.GetBinaryForm(array, 0);
			LocalSecurityAuthorityNative.LSA_ACCESS_POLICY lsa_ACCESS_POLICY = LocalSecurityAuthorityNative.LSA_ACCESS_POLICY.POLICY_CREATE_ACCOUNT | LocalSecurityAuthorityNative.LSA_ACCESS_POLICY.POLICY_LOOKUP_NAMES;
			LocalSecurityAuthorityNative.LSA_OBJECT_ATTRIBUTES lsa_OBJECT_ATTRIBUTES = new LocalSecurityAuthorityNative.LSA_OBJECT_ATTRIBUTES
			{
				Length = 0,
				RootDirectory = IntPtr.Zero,
				Attributes = 0U,
				SecurityDescriptor = IntPtr.Zero,
				SecurityQualityOfService = IntPtr.Zero
			};
			IntPtr intPtr;
			uint num = LocalSecurityAuthorityNative.LsaNtStatusToWinError(LocalSecurityAuthorityNative.LsaOpenPolicy(IntPtr.Zero, ref lsa_OBJECT_ATTRIBUTES, (uint)lsa_ACCESS_POLICY, out intPtr));
			if (num != 0U)
			{
				throw new Win32Exception((int)num);
			}
			LocalSecurityAuthorityNative.LSA_UNICODE_STRING[] array2 = new LocalSecurityAuthorityNative.LSA_UNICODE_STRING[]
			{
				new LocalSecurityAuthorityNative.LSA_UNICODE_STRING
				{
					Buffer = Marshal.StringToHGlobalUni(privilegeName),
					Length = (ushort)(privilegeName.Length * 2),
					MaximumLength = (ushort)((privilegeName.Length + 1) * 2)
				}
			};
			num = LocalSecurityAuthorityNative.LsaNtStatusToWinError(LocalSecurityAuthorityNative.LsaAddAccountRights(intPtr, array, array2, 1U));
			if (num != 0U)
			{
				throw new Win32Exception((int)num);
			}
			LocalSecurityAuthorityNative.LsaClose(intPtr);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007B5C File Offset: 0x00005D5C
		public static void AddRegistryReadPermission(SecurityIdentifier sid)
		{
			RegistryKey registryKey = Registry.Users.OpenSubKey(sid.Value + "\\Software\\Microsoft", true);
			if (registryKey == null)
			{
				throw new Exception(string.Format("Registry root does not exist for SID {0}", sid.Value));
			}
			RegistryKey registryKey2 = registryKey.OpenSubKey("Avalon.Graphics", true);
			if (registryKey2 == null)
			{
				registryKey.CreateSubKey("Avalon.Graphics", true);
				registryKey2 = Registry.Users.OpenSubKey(sid.Value + "\\Software\\Microsoft\\Avalon.Graphics", true);
			}
			RegistrySecurity registrySecurity = new RegistrySecurity();
			registrySecurity = registryKey2.GetAccessControl();
			registrySecurity.AddAccessRule(new RegistryAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), RegistryRights.ExecuteKey, AccessControlType.Allow));
			registryKey2.SetAccessControl(registrySecurity);
		}
	}
}
