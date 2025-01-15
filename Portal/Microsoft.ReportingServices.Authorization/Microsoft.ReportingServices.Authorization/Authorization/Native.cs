using System;
using System.Collections;
using System.Diagnostics;
using System.DirectoryServices;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x02000017 RID: 23
	internal sealed class Native
	{
		// Token: 0x0600004C RID: 76 RVA: 0x000020FD File Offset: 0x000002FD
		private Native()
		{
		}

		// Token: 0x0600004D RID: 77
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private unsafe static extern void CopyMemory(void* dest, void* src, int lengthInBytes);

		// Token: 0x0600004E RID: 78
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private unsafe static extern void ZeroMemory(void* pDest, int lengthInBytes);

		// Token: 0x0600004F RID: 79
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern uint SetEntriesInAcl(uint cCountOfExplicitEntries, SafeLocalFree pListOfExplicitEntries, SafeLocalFree pOldAcl, out SafeLocalFree pNewAcl);

		// Token: 0x06000050 RID: 80
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool InitializeAcl(SafeLocalFree pAcl, uint AclLength, uint Revision);

		// Token: 0x06000051 RID: 81
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetTokenInformation(IntPtr tokenHandle, Native.TOKEN_INFORMATION_CLASS tokenInfoClass, IntPtr tokenInfo, uint tokenInfoLen, out uint returnedLen);

		// Token: 0x06000052 RID: 82
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool InitializeSecurityDescriptor(SafeLocalFree pSecurityDescriptor, uint dwRevision);

		// Token: 0x06000053 RID: 83
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool IsValidAcl(SafeLocalFree pAcl);

		// Token: 0x06000054 RID: 84
		[DllImport("advapi32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		private static extern bool IsValidSecurityDescriptor(SafeLocalFree pSecDesc);

		// Token: 0x06000055 RID: 85
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern uint GetSecurityDescriptorLength(IntPtr pSecDesc);

		// Token: 0x06000056 RID: 86
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool MakeSelfRelativeSD(SafeLocalFree pAbsoluteSecDesc, SafeLocalFree pSelfRelSecDesc, out uint BufferLength);

		// Token: 0x06000057 RID: 87
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetSecurityDescriptorDacl(SafeLocalFree pSecurityDescriptor, bool bDaclPresent, SafeLocalFree pDacl, bool bDaclDefaulted);

		// Token: 0x06000058 RID: 88
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool LookupAccountName(string lpSystemName, string lpAccountName, SafeLocalFree Sid, [In] [Out] ref uint cbSid, SafeLocalFree DomainName, [In] [Out] ref uint cbDomainName, out uint peUse);

		// Token: 0x06000059 RID: 89
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool LookupAccountSid(string lpSystemName, byte[] pSid, SafeLocalFree lpAccountName, [In] [Out] ref uint cchName, SafeLocalFree DomainName, [In] [Out] ref uint cchDomainName, out uint peUse);

		// Token: 0x0600005A RID: 90
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool ConvertSecurityDescriptorToStringSecurityDescriptor(SafeLocalFree pSecurityDescriptor, uint RequestedStringSDRevision, uint SecurityInformation, out SafeLocalFree StringSecurityDescriptor, out uint nStrSecDescLen);

		// Token: 0x0600005B RID: 91
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool ConvertStringSecurityDescriptorToSecurityDescriptor(IntPtr StringSecurityDescriptor, uint StringSDRevision, out IntPtr pSecurityDescriptor, out uint SecurityDescriptorSize);

		// Token: 0x0600005C RID: 92
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private unsafe static extern void MapGenericMask(uint* AccessMask, Native.GENERIC_MAPPING* pGenericMapping);

		// Token: 0x0600005D RID: 93
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private unsafe static extern bool AccessCheck(SafeLocalFree pSecurityDescriptor, IntPtr clientToken, uint desiredAccess, SafeLocalFree pGenericMapping, SafeLocalFree pPrivilegeSet, uint* pPrivilegeSetLength, out uint GrantedAccess, out bool Result);

		// Token: 0x0600005E RID: 94
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetSecurityDescriptorOwner(SafeLocalFree pSecurityDescriptor, SafeSidPtr pSid, int OwnerDefaulted);

		// Token: 0x0600005F RID: 95
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetSecurityDescriptorGroup(SafeLocalFree pSecurityDescriptor, SafeSidPtr pSid, int GroupDefaulted);

		// Token: 0x06000060 RID: 96
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern uint GetEffectiveRightsFromAcl(IntPtr pAcl, IntPtr pTrustee, out uint pAccessRights);

		// Token: 0x06000061 RID: 97
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetSecurityDescriptorDacl(IntPtr pSecDesc, out uint AclPresent, out IntPtr pDacl, out uint pDaclDefaulted);

		// Token: 0x06000062 RID: 98
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x06000063 RID: 99
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr LocalAlloc(uint Flags, uint Size);

		// Token: 0x06000064 RID: 100
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int CloseHandle(IntPtr hDevice);

		// Token: 0x06000065 RID: 101
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern IntPtr GetCurrentThread();

		// Token: 0x06000066 RID: 102
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool AllocateAndInitializeSid(SafeLocalFree pSidAuthPtr, byte nSubAuthorityCount, uint nSubAuthority0, uint nSubAuthority1, uint nSubAuthority2, uint nSubAuthority3, uint nSubAuthority4, uint nSubAuthority5, uint nSubAuthority6, uint nSubAuthority7, out SafeSidPtr pSid);

		// Token: 0x06000067 RID: 103
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool FreeSid(IntPtr pSid);

		// Token: 0x06000068 RID: 104
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool RevertToSelf();

		// Token: 0x06000069 RID: 105
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool CheckTokenMembership(IntPtr token, SafeSidPtr sidToCheck, out bool isMember);

		// Token: 0x0600006A RID: 106
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int DuplicateToken(IntPtr hToken, Native.SECURITY_IMPERSONATION_LEVEL impersonationLevel, ref IntPtr hNewToken);

		// Token: 0x0600006B RID: 107
		[DllImport("authz.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int AuthzInitializeResourceManager(int flags, IntPtr accessCallback, IntPtr computeCallback, IntPtr freeCallback, string managerName, out Native.SafeAuthzResourceManagerHandle phResourceManager);

		// Token: 0x0600006C RID: 108
		[DllImport("authz.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool AuthzFreeResourceManager(IntPtr resourceManager);

		// Token: 0x0600006D RID: 109
		[DllImport("authz.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int AuthzInitializeContextFromSid(int flags, SafeLocalFree sid, Native.SafeAuthzResourceManagerHandle resourceManager, IntPtr largeInteger, Native.LUID identifier, IntPtr dynamicGroupArgs, out Native.SafeAuthzContextHandle context);

		// Token: 0x0600006E RID: 110
		[DllImport("authz.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool AuthzFreeContext(IntPtr context);

		// Token: 0x0600006F RID: 111
		[DllImport("authz.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int AuthzGetInformationFromContext(Native.SafeAuthzContextHandle context, int infoClass, int bufferSize, ref int outBufferSize, SafeLocalFree authInfo);

		// Token: 0x06000070 RID: 112
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int EqualSid(IntPtr sid1, IntPtr sid2);

		// Token: 0x06000071 RID: 113
		[DllImport("authz.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int AuthzAccessCheck(int flags, Native.SafeAuthzContextHandle context, SafeLocalFree request, IntPtr auditInfo, SafeLocalFree securityDescriptor, IntPtr optionSecurityDescriptors, int numOptionalSecDesc, SafeLocalFree reply, IntPtr cachedResults);

		// Token: 0x06000072 RID: 114 RVA: 0x00002961 File Offset: 0x00000B61
		private static bool ExpectedLookupAccountError(int err)
		{
			return err == 1332 || err == 1788 || err == 1789 || err == 87 || err == 1727;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000298C File Offset: 0x00000B8C
		private static Native.SafeAuthzContextHandle GetAuthzContextForUser(SafeLocalFree userSid, string userName)
		{
			Native.SafeAuthzResourceManagerHandle safeAuthzResourceManagerHandle = null;
			Native.SafeAuthzContextHandle safeAuthzContextHandle2;
			try
			{
				if (Native.AuthzInitializeResourceManager(1, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, null, out safeAuthzResourceManagerHandle) == 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "AuthzInitializeResourceManager: Win32 error:{0}", lastWin32Error));
				}
				Native.LUID luid = default(Native.LUID);
				luid.HighPart = 0U;
				luid.LowPart = 0U;
				Native.SafeAuthzContextHandle safeAuthzContextHandle = null;
				if (Native.AuthzInitializeContextFromSid(0, userSid, safeAuthzResourceManagerHandle, IntPtr.Zero, luid, IntPtr.Zero, out safeAuthzContextHandle) == 0)
				{
					int lastWin32Error2 = Marshal.GetLastWin32Error();
					if (lastWin32Error2 == 5)
					{
						throw new WindowsAuthz5ApiException("AuthzInitializeContextFromSid", userName);
					}
					if (lastWin32Error2 == 1355)
					{
						throw new WindowsAuthz1355ApiException("AuthzInitializeContextFromSid", userName);
					}
					throw new WindowsAuthzApiException("AuthzInitializeContextFromSid", lastWin32Error2.ToString(), userName);
				}
				else
				{
					safeAuthzContextHandle2 = safeAuthzContextHandle;
				}
			}
			finally
			{
				if (safeAuthzResourceManagerHandle != null)
				{
					safeAuthzResourceManagerHandle.Close();
				}
			}
			return safeAuthzContextHandle2;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002A68 File Offset: 0x00000C68
		internal static bool IsAdmin(string userName)
		{
			return Native.IsUserMemberOfGroup(userName, Native.GetAdminSid().DangerousGetHandle());
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002A7C File Offset: 0x00000C7C
		internal unsafe static bool IsUserMemberOfGroup(string userName, IntPtr groupSid)
		{
			SafeLocalFree safeLocalFree = null;
			Native.SafeAuthzContextHandle safeAuthzContextHandle = null;
			SafeLocalFree safeLocalFree2 = null;
			SafeSidPtr safeSidPtr = null;
			bool flag;
			try
			{
				safeLocalFree = Native.GetSid(userName);
				if (safeLocalFree == null || safeLocalFree.IsInvalid)
				{
					flag = false;
				}
				else
				{
					safeAuthzContextHandle = Native.GetAuthzContextForUser(safeLocalFree, userName);
					int num = 0;
					if (Native.AuthzGetInformationFromContext(safeAuthzContextHandle, 2, 0, ref num, SafeLocalFree.Zero) == 0)
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error != 122)
						{
							throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "AuthzGetInformationFromContext: Win32 error:{0}", lastWin32Error));
						}
					}
					safeLocalFree2 = SafeLocalFree.LocalAlloc(num);
					int num2 = num;
					if (Native.AuthzGetInformationFromContext(safeAuthzContextHandle, 2, num2, ref num, safeLocalFree2) == 0)
					{
						int lastWin32Error2 = Marshal.GetLastWin32Error();
						throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "AuthzGetInformationFromContext: Win32 error:{0}", lastWin32Error2));
					}
					int num3 = Marshal.ReadInt32(safeLocalFree2.DangerousGetHandle());
					if (num3 == 0)
					{
						if (RSTrace.CatalogTrace.TraceWarning)
						{
							RSTrace.CatalogTrace.Trace(TraceLevel.Warning, "IsAdmin check for the account {0} returns false because this account has no membership. It is possibly a group account.", new object[] { userName });
						}
						flag = false;
					}
					else
					{
						Native.TOKEN_GROUPS token_GROUPS = (Native.TOKEN_GROUPS)Marshal.PtrToStructure(safeLocalFree2.DangerousGetHandle(), typeof(Native.TOKEN_GROUPS));
						RSTrace.CatalogTrace.Assert(num3 == token_GROUPS.GroupCount, "groupCount == tokenGroups.GroupCount");
						if (num3 > 0)
						{
							Native.SID_AND_ATTRIBUTES sid_AND_ATTRIBUTES = token_GROUPS.GroupContents;
							int num4 = 0;
							Native.SID_AND_ATTRIBUTES* ptr = safeLocalFree2.DangerousGetHandle().ToInt64() / (long)sizeof(Native.SID_AND_ATTRIBUTES) + sizeof(Native.TOKEN_GROUPS);
							while (Native.EqualSid(sid_AND_ATTRIBUTES.sid, groupSid) == 0 || (sid_AND_ATTRIBUTES.attributes.ToInt64() & 4L) != 4L)
							{
								if (++num4 >= num3)
								{
									goto IL_01A6;
								}
								sid_AND_ATTRIBUTES = (Native.SID_AND_ATTRIBUTES)Marshal.PtrToStructure((IntPtr)((void*)ptr), typeof(Native.SID_AND_ATTRIBUTES));
								ptr++;
							}
							return true;
						}
						IL_01A6:
						flag = false;
					}
				}
			}
			finally
			{
				if (safeLocalFree != null)
				{
					safeLocalFree.Close();
				}
				if (safeAuthzContextHandle != null)
				{
					safeAuthzContextHandle.Close();
				}
				if (safeLocalFree2 != null)
				{
					safeLocalFree2.Close();
				}
				if (safeSidPtr != null)
				{
					safeSidPtr.Close();
				}
			}
			return flag;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002C78 File Offset: 0x00000E78
		internal static bool IsAdmin(IntPtr userToken)
		{
			SafeSidPtr safeSidPtr = null;
			bool flag = false;
			bool flag2;
			try
			{
				safeSidPtr = Native.GetAdminSid();
				if (!Native.CheckTokenMembership(userToken, safeSidPtr, out flag))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "CheckTokenMembership: Win32 error:{0}", lastWin32Error));
				}
				flag2 = flag;
			}
			finally
			{
				if (safeSidPtr != null)
				{
					safeSidPtr.Close();
				}
			}
			return flag2;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002CDC File Offset: 0x00000EDC
		internal static bool IsValidPrincipalName(string name)
		{
			int num = 0;
			uint num2 = 0U;
			SafeLocalFree sid = Native.GetSid(name, out num, out num2);
			if (sid != null)
			{
				sid.Close();
				return 6U != num2 && 7U != num2 && !Native.IsDistributionGroup(name, num2);
			}
			return false;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002D1C File Offset: 0x00000F1C
		private static SafeLocalFree GetSid(string name)
		{
			int num = 0;
			uint num2 = 0U;
			return Native.GetSid(name, out num, out num2);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002D38 File Offset: 0x00000F38
		internal static byte[] NameToSid(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw new UnknownUserNameException(name);
			}
			SafeLocalFree safeLocalFree = null;
			int num = 0;
			uint num2 = 0U;
			byte[] array2;
			try
			{
				safeLocalFree = Native.GetSid(name, out num, out num2);
				if (safeLocalFree == null)
				{
					throw new UnknownUserNameException(name);
				}
				byte[] array = new byte[num];
				Marshal.Copy(safeLocalFree.DangerousGetHandle(), array, 0, num);
				array2 = array;
			}
			finally
			{
				if (safeLocalFree != null)
				{
					safeLocalFree.Close();
				}
			}
			return array2;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002DAC File Offset: 0x00000FAC
		internal static string GetUserNameFromSid(byte[] sid)
		{
			SafeLocalFree safeLocalFree = null;
			SafeLocalFree safeLocalFree2 = null;
			string text;
			try
			{
				uint num = 0U;
				uint num2 = 0U;
				uint num3 = 0U;
				if (!Native.LookupAccountSid(null, sid, SafeLocalFree.Zero, ref num2, SafeLocalFree.Zero, ref num, out num3))
				{
					int num4 = Marshal.GetLastWin32Error();
					if (num4 != 122 && !Native.ExpectedLookupAccountError(num4))
					{
						throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "LookupAccountSid: Win32 error:{0}", num4));
					}
					if (Native.ExpectedLookupAccountError(num4))
					{
						return null;
					}
				}
				safeLocalFree = SafeLocalFree.LocalAlloc((int)(num * 2U));
				safeLocalFree2 = SafeLocalFree.LocalAlloc((int)(num2 * 2U));
				if (!Native.LookupAccountSid(null, sid, safeLocalFree2, ref num2, safeLocalFree, ref num, out num3))
				{
					int num4 = Marshal.GetLastWin32Error();
					if (!Native.ExpectedLookupAccountError(num4))
					{
						throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "LookupAccountName: Win32 error:{0}", num4));
					}
					text = null;
				}
				else
				{
					string text2 = Marshal.PtrToStringUni(safeLocalFree2.DangerousGetHandle());
					string text3 = Marshal.PtrToStringUni(safeLocalFree.DangerousGetHandle());
					if (string.IsNullOrEmpty(text3))
					{
						text = text2;
					}
					else
					{
						text = text3 + "\\" + text2;
					}
				}
			}
			finally
			{
				if (safeLocalFree != null)
				{
					safeLocalFree.Close();
				}
				if (safeLocalFree2 != null)
				{
					safeLocalFree2.Close();
				}
			}
			return text;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002ED0 File Offset: 0x000010D0
		private static SafeLocalFree GetSid(string name, out int length, out uint sidType)
		{
			length = 0;
			SafeLocalFree safeLocalFree = null;
			SafeLocalFree safeLocalFree3;
			try
			{
				uint num = 0U;
				uint num2 = 0U;
				if (!Native.LookupAccountName(null, name, SafeLocalFree.Zero, ref num, SafeLocalFree.Zero, ref num2, out sidType))
				{
					int num3 = Marshal.GetLastWin32Error();
					if (num3 != 122 && !Native.ExpectedLookupAccountError(num3))
					{
						throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "LookupAccountName: Win32 error:{0}", num3));
					}
					if (Native.ExpectedLookupAccountError(num3))
					{
						return null;
					}
				}
				safeLocalFree = SafeLocalFree.LocalAlloc((int)(num2 * 2U));
				SafeLocalFree safeLocalFree2 = SafeLocalFree.LocalAlloc((int)num);
				if (!Native.LookupAccountName(null, name, safeLocalFree2, ref num, safeLocalFree, ref num2, out sidType))
				{
					int num3 = Marshal.GetLastWin32Error();
					if (safeLocalFree2 != null)
					{
						safeLocalFree2.Close();
					}
					if (!Native.ExpectedLookupAccountError(num3))
					{
						throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "LookupAccountName: Win32 error:{0}", num3));
					}
					safeLocalFree3 = null;
				}
				else
				{
					length = (int)num;
					safeLocalFree3 = safeLocalFree2;
				}
			}
			finally
			{
				if (safeLocalFree != null)
				{
					safeLocalFree.Close();
				}
			}
			return safeLocalFree3;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002FBC File Offset: 0x000011BC
		internal static void BuildAcl(WindowsAcl acl, SecurityItemType itemType, out byte[] realSecDesc, out string stringSecDesc)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			if (itemType == SecurityItemType.Catalog)
			{
				byte[] array;
				string text;
				Native.BuildSecurityDescriptor(acl, SecDescType.Catalog, out array, out text);
				arrayList.Add(new SdAndType(SecDescType.Catalog, array));
				arrayList2.Add(new SdStrAndType(SecurityItemType.Catalog, text));
			}
			if (itemType == SecurityItemType.ModelItem)
			{
				byte[] array2;
				string text2;
				Native.BuildSecurityDescriptor(acl, SecDescType.ModelItem, out array2, out text2);
				arrayList.Add(new SdAndType(SecDescType.ModelItem, array2));
				arrayList2.Add(new SdStrAndType(SecurityItemType.ModelItem, text2));
			}
			if (itemType == SecurityItemType.Folder)
			{
				byte[] array3;
				string text3;
				Native.BuildSecurityDescriptor(acl, SecDescType.Folder, out array3, out text3);
				arrayList.Add(new SdAndType(SecDescType.Folder, array3));
				arrayList2.Add(new SdStrAndType(SecurityItemType.Folder, text3));
			}
			if (itemType == SecurityItemType.Folder || itemType == SecurityItemType.Report)
			{
				byte[] array4;
				string text4;
				Native.BuildSecurityDescriptor(acl, SecDescType.ReportPrimary, out array4, out text4);
				arrayList.Add(new SdAndType(SecDescType.ReportPrimary, array4));
				arrayList2.Add(new SdStrAndType(SecurityItemType.Report, text4));
				byte[] array5 = null;
				string text5 = null;
				Native.BuildSecurityDescriptor(acl, SecDescType.ReportSecondary, out array5, out text5);
				arrayList.Add(new SdAndType(SecDescType.ReportSecondary, array5));
				arrayList2.Add(new SdStrAndType(SecurityItemType.Report, text5));
			}
			if (itemType == SecurityItemType.Folder || itemType == SecurityItemType.Resource)
			{
				byte[] array6;
				string text6;
				Native.BuildSecurityDescriptor(acl, SecDescType.Resource, out array6, out text6);
				arrayList.Add(new SdAndType(SecDescType.Resource, array6));
				arrayList2.Add(new SdStrAndType(SecurityItemType.Resource, text6));
			}
			if (itemType == SecurityItemType.Folder || itemType == SecurityItemType.Datasource)
			{
				byte[] array7;
				string text7;
				Native.BuildSecurityDescriptor(acl, SecDescType.Datasource, out array7, out text7);
				arrayList.Add(new SdAndType(SecDescType.Datasource, array7));
				arrayList2.Add(new SdStrAndType(SecurityItemType.Datasource, text7));
			}
			if (itemType == SecurityItemType.Folder || itemType == SecurityItemType.Model)
			{
				byte[] array8;
				string text8;
				Native.BuildSecurityDescriptor(acl, SecDescType.Model, out array8, out text8);
				arrayList.Add(new SdAndType(SecDescType.Model, array8));
				arrayList2.Add(new SdStrAndType(SecurityItemType.Model, text8));
			}
			SdAndType.PrepareToStoreBinaryPolicy(arrayList, out realSecDesc);
			SdAndType.PrepareToStoreStringPolicy(arrayList2, out stringSecDesc);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000315C File Offset: 0x0000135C
		internal unsafe static void BuildSecurityDescriptor(WindowsAcl acl, SecDescType secDescType, out byte[] secDesc, out string strSecDesc)
		{
			SafeLocalFree safeLocalFree = null;
			SafeLocalFree safeLocalFree2 = null;
			SafeLocalFree safeLocalFree3 = null;
			SafeLocalFree safeLocalFree4 = null;
			SafeLocalFree safeLocalFree5 = null;
			SafeSidPtr safeSidPtr = null;
			Native.EXPLICIT_ACCESS[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeLocalFree = SafeLocalFree.LocalAlloc(sizeof(Native.SECURITY_DESCRIPTOR));
				Native.ZeroMemory(safeLocalFree.DangerousGetHandle().ToPointer(), sizeof(Native.SECURITY_DESCRIPTOR));
				uint num = 1U;
				if (!Native.InitializeSecurityDescriptor(safeLocalFree, num))
				{
					int num2 = Marshal.GetLastWin32Error();
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "InitializeSecurityDescriptor: Win32 error:{0}", num2));
				}
				uint count = (uint)acl.Count;
				if (count > 0U)
				{
					array = new Native.EXPLICIT_ACCESS[count];
					int num3 = 0;
					while ((long)num3 < (long)((ulong)count))
					{
						WindowsAce windowsAce = acl[num3];
						array[num3].grfAccessMode = Native.ACCESS_MODE.SET_ACCESS;
						array[num3].grfAccessPermissions = windowsAce.GetMask(secDescType);
						array[num3].grfInheritance = 4U;
						array[num3].Trustee.MultipleTrusteeOperation = Native.MULTIPLE_TRUSTEE_OPERATION.NO_MULTIPLE_TRUSTEE;
						array[num3].Trustee.pMultipleTrustee = IntPtr.Zero;
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
						}
						finally
						{
							array[num3].Trustee.ptstrName = Marshal.StringToHGlobalUni(windowsAce.PrincipalName);
						}
						array[num3].Trustee.TrusteeForm = Native.TRUSTEE_FORM.TRUSTEE_IS_NAME;
						array[num3].Trustee.TrusteeType = Native.TRUSTEE_TYPE.TRUSTEE_IS_UNKNOWN;
						num3++;
					}
					int num4 = (int)(count * (uint)sizeof(Native.EXPLICIT_ACCESS));
					safeLocalFree5 = SafeLocalFree.LocalAlloc(num4);
					try
					{
						fixed (Native.EXPLICIT_ACCESS* ptr = &array[0])
						{
							void* ptr2 = (void*)ptr;
							Native.CopyMemory(safeLocalFree5.DangerousGetHandle().ToPointer(), ptr2, num4);
						}
					}
					finally
					{
						Native.EXPLICIT_ACCESS* ptr = null;
					}
					if (Native.SetEntriesInAcl(count, safeLocalFree5, SafeLocalFree.Zero, out safeLocalFree2) != 0U)
					{
						int num2 = Marshal.GetLastWin32Error();
						throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "SetEntriesInAcl: Win32 error:{0}", num2));
					}
				}
				else
				{
					int num5 = 1024;
					safeLocalFree2 = SafeLocalFree.LocalAlloc(64, num5);
					if (!Native.InitializeAcl(safeLocalFree2, (uint)num5, 2U))
					{
						int num2 = Marshal.GetLastWin32Error();
						throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "InitializeAcl: Win32 error:{0}", num2));
					}
				}
				if (!Native.IsValidAcl(safeLocalFree2))
				{
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "IsValidAcl: false", Array.Empty<object>()));
				}
				if (!Native.SetSecurityDescriptorDacl(safeLocalFree, true, safeLocalFree2, false))
				{
					int num2 = Marshal.GetLastWin32Error();
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "SetSecurityDescriptorDacl: Win32 error:{0}", num2));
				}
				if (!Native.IsValidSecurityDescriptor(safeLocalFree))
				{
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "IsValidSecurityDescriptor: false", Array.Empty<object>()));
				}
				safeSidPtr = Native.GetAdminSid();
				if (!Native.SetSecurityDescriptorOwner(safeLocalFree, safeSidPtr, 0))
				{
					int num2 = Marshal.GetLastWin32Error();
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "SetSecurityDescriptorOwner: Win32 error:{0}", num2));
				}
				if (!Native.SetSecurityDescriptorGroup(safeLocalFree, safeSidPtr, 0))
				{
					int num2 = Marshal.GetLastWin32Error();
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "SetSecurityDescriptorGroup: Win32 error:{0}", num2));
				}
				if (!Native.IsValidSecurityDescriptor(safeLocalFree))
				{
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "IsValidSecurityDescriptor: false", Array.Empty<object>()));
				}
				uint num6 = 0U;
				bool flag = Native.MakeSelfRelativeSD(safeLocalFree, SafeLocalFree.Zero, out num6);
				if (!flag)
				{
					int num2 = Marshal.GetLastWin32Error();
					if (num2 == 122)
					{
						safeLocalFree3 = SafeLocalFree.LocalAlloc((int)num6);
						flag = Native.MakeSelfRelativeSD(safeLocalFree, safeLocalFree3, out num6);
					}
					if (!flag || num2 != 122)
					{
						num2 = Marshal.GetLastWin32Error();
						throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "MakeSelfRelativeSD: Win32 error:{0}", num2));
					}
				}
				if (!Native.IsValidSecurityDescriptor(safeLocalFree3))
				{
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "IsValidSecurityDescriptor: false", Array.Empty<object>()));
				}
				secDesc = new byte[num6];
				Marshal.Copy(safeLocalFree3.DangerousGetHandle(), secDesc, 0, (int)num6);
				uint num7 = 15U;
				uint num8 = 0U;
				if (!Native.ConvertSecurityDescriptorToStringSecurityDescriptor(safeLocalFree3, 1U, num7, out safeLocalFree4, out num8))
				{
					int num2 = Marshal.GetLastWin32Error();
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "ConvertSecurityDescriptorToStringSecurityDescriptor: Win32 error:{0}", num2));
				}
				strSecDesc = Marshal.PtrToStringUni(safeLocalFree4.DangerousGetHandle());
			}
			finally
			{
				if (safeLocalFree != null)
				{
					safeLocalFree.Close();
				}
				if (safeLocalFree5 != null)
				{
					safeLocalFree5.Close();
				}
				if (safeLocalFree2 != null)
				{
					safeLocalFree2.Close();
				}
				if (safeSidPtr != null)
				{
					safeSidPtr.Close();
				}
				if (safeLocalFree3 != null)
				{
					safeLocalFree3.Close();
				}
				if (safeLocalFree4 != null)
				{
					safeLocalFree4.Close();
				}
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						Marshal.FreeHGlobal(array[i].Trustee.ptstrName);
					}
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003618 File Offset: 0x00001818
		internal static void GetNewEffectiveRights(byte[] secDesc, IntPtr userToken, out uint mask)
		{
			mask = 33554432U;
			Native.CheckAccess(SecurityItemType.Catalog, secDesc, ref mask, userToken);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000362B File Offset: 0x0000182B
		internal static void GetNewEffectiveRights(byte[] secDesc, string userName, out uint mask)
		{
			mask = 33554432U;
			Native.CheckAccess(SecurityItemType.Catalog, secDesc, ref mask, userName);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003640 File Offset: 0x00001840
		private unsafe static SafeLocalFree MarshalSecurityDescriptor(byte[] secDesc)
		{
			int num = secDesc.Length;
			SafeLocalFree safeLocalFree = SafeLocalFree.LocalAlloc(num);
			fixed (byte* ptr = &secDesc[0])
			{
				byte* ptr2 = ptr;
				Native.CopyMemory(safeLocalFree.DangerousGetHandle().ToPointer(), (void*)ptr2, num);
			}
			if (!Native.IsValidSecurityDescriptor(safeLocalFree))
			{
				throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "IsValidSecurityDescriptor: false", Array.Empty<object>()));
			}
			return safeLocalFree;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000369C File Offset: 0x0000189C
		internal unsafe static bool CheckAccess(SecurityItemType itemType, byte[] secDesc, ref uint rightsMask, string userName)
		{
			SafeLocalFree safeLocalFree = null;
			Native.SafeAuthzContextHandle safeAuthzContextHandle = null;
			SafeLocalFree safeLocalFree2 = null;
			SafeLocalFree safeLocalFree3 = null;
			SafeLocalFree safeLocalFree4 = null;
			SafeLocalFree safeLocalFree5 = null;
			SafeLocalFree safeLocalFree6 = null;
			bool flag;
			try
			{
				safeLocalFree = Native.GetSid(userName);
				if (safeLocalFree == null || safeLocalFree.IsInvalid)
				{
					flag = false;
				}
				else
				{
					safeAuthzContextHandle = Native.GetAuthzContextForUser(safeLocalFree, userName);
					Native.AUTHZ_ACCESS_REQUEST authz_ACCESS_REQUEST = default(Native.AUTHZ_ACCESS_REQUEST);
					authz_ACCESS_REQUEST.accessMask = rightsMask;
					authz_ACCESS_REQUEST.sid = safeLocalFree.DangerousGetHandle();
					authz_ACCESS_REQUEST.objectTypeList = IntPtr.Zero;
					authz_ACCESS_REQUEST.objectTypeListLength = 0;
					authz_ACCESS_REQUEST.optionalArgs = IntPtr.Zero;
					safeLocalFree2 = SafeLocalFree.LocalAlloc(sizeof(Native.AUTHZ_ACCESS_REQUEST));
					Marshal.StructureToPtr<Native.AUTHZ_ACCESS_REQUEST>(authz_ACCESS_REQUEST, safeLocalFree2.DangerousGetHandle(), false);
					safeLocalFree3 = Native.MarshalSecurityDescriptor(secDesc);
					Native.AUTHZ_ACCESS_REPLY authz_ACCESS_REPLY = default(Native.AUTHZ_ACCESS_REPLY);
					authz_ACCESS_REPLY.resultListLength = 1;
					safeLocalFree4 = SafeLocalFree.LocalAlloc(4);
					authz_ACCESS_REPLY.grantedAccessMask = safeLocalFree4.DangerousGetHandle();
					authz_ACCESS_REPLY.saclEvaluationResults = IntPtr.Zero;
					safeLocalFree5 = SafeLocalFree.LocalAlloc(4);
					authz_ACCESS_REPLY.error = safeLocalFree5.DangerousGetHandle();
					safeLocalFree6 = SafeLocalFree.LocalAlloc(sizeof(Native.AUTHZ_ACCESS_REPLY));
					Marshal.StructureToPtr<Native.AUTHZ_ACCESS_REPLY>(authz_ACCESS_REPLY, safeLocalFree6.DangerousGetHandle(), false);
					int num = 0;
					if (Native.AuthzAccessCheck(1, safeAuthzContextHandle, safeLocalFree2, IntPtr.Zero, safeLocalFree3, IntPtr.Zero, num, safeLocalFree6, IntPtr.Zero) == 0)
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "AuthzAccessCheck: Win32 error:{0}", lastWin32Error));
					}
					uint* ptr = (uint*)authz_ACCESS_REPLY.grantedAccessMask.ToPointer();
					rightsMask = *ptr;
					uint* ptr2 = (uint*)authz_ACCESS_REPLY.error.ToPointer();
					flag = *ptr2 == 0U;
				}
			}
			finally
			{
				if (safeLocalFree != null)
				{
					safeLocalFree.Close();
				}
				if (safeAuthzContextHandle != null)
				{
					safeAuthzContextHandle.Close();
				}
				if (safeLocalFree2 != null)
				{
					safeLocalFree2.Close();
				}
				if (safeLocalFree3 != null)
				{
					safeLocalFree3.Close();
				}
				if (safeLocalFree4 != null)
				{
					safeLocalFree4.Close();
				}
				if (safeLocalFree5 != null)
				{
					safeLocalFree5.Close();
				}
				if (safeLocalFree6 != null)
				{
					safeLocalFree6.Close();
				}
			}
			return flag;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003874 File Offset: 0x00001A74
		internal unsafe static bool CheckAccess(SecurityItemType itemType, byte[] secDesc, ref uint rightsMask, IntPtr userToken)
		{
			SafeLocalFree safeLocalFree = null;
			SafeLocalFree safeLocalFree2 = null;
			SafeLocalFree safeLocalFree3 = null;
			bool flag2;
			try
			{
				Native.GENERIC_MAPPING genericMapping = Native.GetGenericMapping(itemType);
				safeLocalFree = Native.MarshalSecurityDescriptor(secDesc);
				uint num = rightsMask;
				Native.MapGenericMask(&num, &genericMapping);
				Native.PRIVILEGE_SET privilege_SET = default(Native.PRIVILEGE_SET);
				uint num2 = (uint)sizeof(Native.PRIVILEGE_SET);
				uint num3 = 0U;
				bool flag = false;
				safeLocalFree2 = SafeLocalFree.LocalAlloc((int)num2);
				Marshal.StructureToPtr<Native.PRIVILEGE_SET>(privilege_SET, safeLocalFree2.DangerousGetHandle(), true);
				safeLocalFree3 = SafeLocalFree.LocalAlloc(sizeof(Native.GENERIC_MAPPING));
				Marshal.StructureToPtr<Native.GENERIC_MAPPING>(genericMapping, safeLocalFree3.DangerousGetHandle(), true);
				if (!Native.AccessCheck(safeLocalFree, userToken, rightsMask, safeLocalFree3, safeLocalFree2, &num2, out num3, out flag))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					string text = string.Format(CultureInfo.InvariantCulture, "AccessCheck: Win32 error:{0}", lastWin32Error);
					if (lastWin32Error == 6)
					{
						if (RSTrace.CatalogTrace.TraceWarning)
						{
							RSTrace.CatalogTrace.Trace(TraceLevel.Warning, text);
						}
						throw new AccessDeniedException("", ErrorCode.rsAccessDenied);
					}
					throw new InternalCatalogException(text);
				}
				else
				{
					rightsMask = num3;
					flag2 = flag;
				}
			}
			finally
			{
				if (safeLocalFree != null)
				{
					safeLocalFree.Close();
				}
				if (safeLocalFree2 != null)
				{
					safeLocalFree2.Close();
				}
				if (safeLocalFree3 != null)
				{
					safeLocalFree3.Close();
				}
			}
			return flag2;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003988 File Offset: 0x00001B88
		private static Native.GENERIC_MAPPING GetGenericMapping(SecurityItemType itemType)
		{
			Native.GENERIC_MAPPING generic_MAPPING = default(Native.GENERIC_MAPPING);
			switch (itemType)
			{
			case SecurityItemType.Catalog:
				generic_MAPPING.GenericRead = 0U;
				generic_MAPPING.GenericWrite = 0U;
				generic_MAPPING.GenericExecute = 0U;
				generic_MAPPING.GenericAll = 0U;
				break;
			case SecurityItemType.Folder:
				generic_MAPPING.GenericRead = 0U;
				generic_MAPPING.GenericWrite = 0U;
				generic_MAPPING.GenericExecute = 0U;
				generic_MAPPING.GenericAll = 0U;
				break;
			case SecurityItemType.Report:
				generic_MAPPING.GenericRead = 0U;
				generic_MAPPING.GenericWrite = 2097152U;
				generic_MAPPING.GenericExecute = 0U;
				generic_MAPPING.GenericAll = 2097152U;
				break;
			case SecurityItemType.Resource:
				generic_MAPPING.GenericRead = 0U;
				generic_MAPPING.GenericWrite = 0U;
				generic_MAPPING.GenericExecute = 0U;
				generic_MAPPING.GenericAll = 0U;
				break;
			}
			return generic_MAPPING;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003A4C File Offset: 0x00001C4C
		private static SafeSidPtr GetAdminSid()
		{
			SafeSidPtr safeSidPtr = null;
			SafeLocalFree safeLocalFree = null;
			SafeSidPtr safeSidPtr2;
			try
			{
				Native.SID_IDENTIFIER_AUTHORITY sid_IDENTIFIER_AUTHORITY = new Native.SID_IDENTIFIER_AUTHORITY
				{
					m_Value0 = Native.SECURITY_NT_AUTHORITY[0],
					m_Value1 = Native.SECURITY_NT_AUTHORITY[1],
					m_Value2 = Native.SECURITY_NT_AUTHORITY[2],
					m_Value3 = Native.SECURITY_NT_AUTHORITY[3],
					m_Value4 = Native.SECURITY_NT_AUTHORITY[4],
					m_Value5 = Native.SECURITY_NT_AUTHORITY[5]
				};
				safeLocalFree = SafeLocalFree.LocalAlloc(Marshal.SizeOf<Native.SID_IDENTIFIER_AUTHORITY>(sid_IDENTIFIER_AUTHORITY));
				Marshal.StructureToPtr<Native.SID_IDENTIFIER_AUTHORITY>(sid_IDENTIFIER_AUTHORITY, safeLocalFree.DangerousGetHandle(), true);
				if (!Native.AllocateAndInitializeSid(safeLocalFree, 2, 32U, 544U, 0U, 0U, 0U, 0U, 0U, 0U, out safeSidPtr))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "AllocateAndInitializeSid: Win32 error:{0}", lastWin32Error));
				}
				safeSidPtr2 = safeSidPtr;
			}
			finally
			{
				if (safeLocalFree != null)
				{
					safeLocalFree.Close();
				}
			}
			return safeSidPtr2;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003B2C File Offset: 0x00001D2C
		private static bool IsDistributionGroup(string name, uint sidType)
		{
			bool flag = false;
			DirectorySearcher searcher = null;
			try
			{
				if (2U != sidType)
				{
					return flag;
				}
				int num = name.IndexOf('\\');
				string text = string.Empty;
				if (-1 == num)
				{
					searcher = new DirectorySearcher();
					text = name;
				}
				else
				{
					string text2 = name.Substring(0, num);
					if (name.Length > num + 2)
					{
						text = name.Substring(num + 1);
					}
					DirectoryEntry directoryEntry = new DirectoryEntry(string.Format(CultureInfo.InvariantCulture, "LDAP://{0}", text2));
					searcher = new DirectorySearcher(directoryEntry);
				}
				searcher.Filter = string.Format(CultureInfo.InvariantCulture, "(&(objectCategory=group)(samaccountname={0}))", text);
				SearchResult result = null;
				RevertImpersonationContext.Run(delegate
				{
					result = searcher.FindOne();
				});
				if (result != null)
				{
					int num2 = (int)result.Properties["grouptype"][0];
					flag = ((long)num2 & (long)((ulong)int.MinValue)) == 0L;
				}
			}
			catch
			{
			}
			finally
			{
				if (searcher != null)
				{
					searcher.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x04000054 RID: 84
		private const int ERROR_SUCCESS = 0;

		// Token: 0x04000055 RID: 85
		private const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x04000056 RID: 86
		private const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x04000057 RID: 87
		private const int ERROR_TRUSTED_DOMAIN_FAILURE = 1788;

		// Token: 0x04000058 RID: 88
		private const int ERROR_TRUSTED_RELATIONSHIP_FAILURE = 1789;

		// Token: 0x04000059 RID: 89
		private const int ERROR_NONE_MAPPED = 1332;

		// Token: 0x0400005A RID: 90
		private const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x0400005B RID: 91
		private const int RPC_S_CALL_FAILED_DNE = 1727;

		// Token: 0x0400005C RID: 92
		internal const int ERROR_NO_TOKEN = 1008;

		// Token: 0x0400005D RID: 93
		private const int OWNER_SECURITY_INFORMATION = 1;

		// Token: 0x0400005E RID: 94
		private const int GROUP_SECURITY_INFORMATION = 2;

		// Token: 0x0400005F RID: 95
		private const uint DACL_SECURITY_INFORMATION = 4U;

		// Token: 0x04000060 RID: 96
		private const uint SACL_SECURITY_INFORMATION = 8U;

		// Token: 0x04000061 RID: 97
		private const uint ACL_REVISION = 2U;

		// Token: 0x04000062 RID: 98
		private const uint OBJECT_INHERIT_ACE = 1U;

		// Token: 0x04000063 RID: 99
		private const uint CONTAINER_INHERIT_ACE = 2U;

		// Token: 0x04000064 RID: 100
		private const uint NO_PROPAGATE_INHERIT_ACE = 4U;

		// Token: 0x04000065 RID: 101
		private const uint INHERIT_ONLY_ACE = 8U;

		// Token: 0x04000066 RID: 102
		private const uint SID_TYPE_GROUP = 2U;

		// Token: 0x04000067 RID: 103
		private const uint SID_TYPE_ALIAS = 4U;

		// Token: 0x04000068 RID: 104
		private const uint SID_TYPE_DELETED_ACCOUNT = 6U;

		// Token: 0x04000069 RID: 105
		private const uint SID_TYPE_INVALID = 7U;

		// Token: 0x0400006A RID: 106
		private const uint SDDL_REVISION_1 = 1U;

		// Token: 0x0400006B RID: 107
		internal const uint TOKEN_QUERY = 8U;

		// Token: 0x0400006C RID: 108
		internal const uint TOKEN_IMPERSONATE = 4U;

		// Token: 0x0400006D RID: 109
		private const uint DESIRE_ALL_ACCESS = 5242879U;

		// Token: 0x0400006E RID: 110
		private const uint MAXIMUM_ALLOWED = 33554432U;

		// Token: 0x0400006F RID: 111
		private const uint GENERIC_CATALOG_READ_DATA = 0U;

		// Token: 0x04000070 RID: 112
		private const uint GENERIC_CATALOG_WRITE_DATA = 0U;

		// Token: 0x04000071 RID: 113
		private const uint GENERIC_CATALOG_EXECUTE_DATA = 0U;

		// Token: 0x04000072 RID: 114
		private const uint GENERIC_CATALOG_ALL_DATA = 0U;

		// Token: 0x04000073 RID: 115
		private const uint GENERIC_FOLDER_READ_DATA = 0U;

		// Token: 0x04000074 RID: 116
		private const uint GENERIC_FOLDER_WRITE_DATA = 0U;

		// Token: 0x04000075 RID: 117
		private const uint GENERIC_FOLDER_EXECUTE_DATA = 0U;

		// Token: 0x04000076 RID: 118
		private const uint GENERIC_FOLDER_ALL_DATA = 0U;

		// Token: 0x04000077 RID: 119
		private const uint GENERIC_REPORT_READ_DATA = 0U;

		// Token: 0x04000078 RID: 120
		private const uint GENERIC_REPORT_WRITE_DATA = 2097152U;

		// Token: 0x04000079 RID: 121
		private const uint GENERIC_REPORT_EXECUTE_DATA = 0U;

		// Token: 0x0400007A RID: 122
		private const uint GENERIC_REPORT_ALL_DATA = 2097152U;

		// Token: 0x0400007B RID: 123
		private const uint GENERIC_RESOURCE_READ_DATA = 0U;

		// Token: 0x0400007C RID: 124
		private const uint GENERIC_RESOURCE_WRITE_DATA = 0U;

		// Token: 0x0400007D RID: 125
		private const uint GENERIC_RESOURCE_EXECUTE_DATA = 0U;

		// Token: 0x0400007E RID: 126
		private const uint GENERIC_RESOURCE_ALL_DATA = 0U;

		// Token: 0x0400007F RID: 127
		private const uint LMEM_FIXED = 0U;

		// Token: 0x04000080 RID: 128
		private const uint LMEM_ZEROINIT = 64U;

		// Token: 0x04000081 RID: 129
		private const uint LPTR = 64U;

		// Token: 0x04000082 RID: 130
		private const uint SECURITY_BUILTIN_DOMAIN_RID = 32U;

		// Token: 0x04000083 RID: 131
		private const uint DOMAIN_ALIAS_RID_ADMINS = 544U;

		// Token: 0x04000084 RID: 132
		private static byte[] SECURITY_NT_AUTHORITY = new byte[] { 0, 0, 0, 0, 0, 5 };

		// Token: 0x04000085 RID: 133
		internal static readonly int SqlDeleteForeignKeyViolationCode = 547;

		// Token: 0x04000086 RID: 134
		internal static readonly int SqlUniqueIndexViolationCode = 2601;

		// Token: 0x04000087 RID: 135
		internal static readonly int SqlTransactionAbortedCode = 3903;

		// Token: 0x04000088 RID: 136
		internal static readonly int SqlAdHocErrorCode = 50000;

		// Token: 0x04000089 RID: 137
		private const int AUTHZ_RM_FLAG_NO_AUDIT = 1;

		// Token: 0x0400008A RID: 138
		private const int AuthzContextInfoGroupsSids = 2;

		// Token: 0x0400008B RID: 139
		private const long SE_GROUP_ENABLED = 4L;

		// Token: 0x0400008C RID: 140
		private const int AUTHZ_ACCESS_CHECK_NO_DEEP_COPY_SD = 1;

		// Token: 0x0400008D RID: 141
		private const string AuthzInitializeContextFromSidMethodName = "AuthzInitializeContextFromSid";

		// Token: 0x02000033 RID: 51
		internal enum SECURITY_IMPERSONATION_LEVEL
		{
			// Token: 0x0400016D RID: 365
			SecurityAnonymous,
			// Token: 0x0400016E RID: 366
			SecurityIdentification,
			// Token: 0x0400016F RID: 367
			SecurityImpersonation,
			// Token: 0x04000170 RID: 368
			SecurityDelegation
		}

		// Token: 0x02000034 RID: 52
		private struct ACL
		{
			// Token: 0x04000171 RID: 369
			private byte AclRevision;

			// Token: 0x04000172 RID: 370
			private byte Sbz1;

			// Token: 0x04000173 RID: 371
			private ushort AclSize;

			// Token: 0x04000174 RID: 372
			private ushort AceCount;

			// Token: 0x04000175 RID: 373
			private ushort Sbz2;
		}

		// Token: 0x02000035 RID: 53
		private struct SECURITY_DESCRIPTOR
		{
			// Token: 0x04000176 RID: 374
			private byte Revision;

			// Token: 0x04000177 RID: 375
			private byte Sbz1;

			// Token: 0x04000178 RID: 376
			private ushort Control;

			// Token: 0x04000179 RID: 377
			private IntPtr Owner;

			// Token: 0x0400017A RID: 378
			private IntPtr Group;

			// Token: 0x0400017B RID: 379
			private unsafe Native.ACL* Sacl;

			// Token: 0x0400017C RID: 380
			private unsafe Native.ACL* Dacl;
		}

		// Token: 0x02000036 RID: 54
		private enum ACCESS_MODE
		{
			// Token: 0x0400017E RID: 382
			NOT_USED_ACCESS,
			// Token: 0x0400017F RID: 383
			GRANT_ACCESS,
			// Token: 0x04000180 RID: 384
			SET_ACCESS,
			// Token: 0x04000181 RID: 385
			DENY_ACCESS,
			// Token: 0x04000182 RID: 386
			REVOKE_ACCESS,
			// Token: 0x04000183 RID: 387
			SET_AUDIT_SUCCESS,
			// Token: 0x04000184 RID: 388
			SET_AUDIT_FAILURE
		}

		// Token: 0x02000037 RID: 55
		private enum MULTIPLE_TRUSTEE_OPERATION
		{
			// Token: 0x04000186 RID: 390
			NO_MULTIPLE_TRUSTEE
		}

		// Token: 0x02000038 RID: 56
		private enum TRUSTEE_FORM
		{
			// Token: 0x04000188 RID: 392
			TRUSTEE_IS_SID,
			// Token: 0x04000189 RID: 393
			TRUSTEE_IS_NAME
		}

		// Token: 0x02000039 RID: 57
		private enum TRUSTEE_TYPE
		{
			// Token: 0x0400018B RID: 395
			TRUSTEE_IS_UNKNOWN,
			// Token: 0x0400018C RID: 396
			TRUSTEE_IS_USER,
			// Token: 0x0400018D RID: 397
			TRUSTEE_IS_GROUP,
			// Token: 0x0400018E RID: 398
			TRUSTEE_IS_DOMAIN,
			// Token: 0x0400018F RID: 399
			TRUSTEE_IS_ALIAS,
			// Token: 0x04000190 RID: 400
			TRUSTEE_IS_WELL_KNOWN_GROUP,
			// Token: 0x04000191 RID: 401
			TRUSTEE_IS_DELETED,
			// Token: 0x04000192 RID: 402
			TRUSTEE_IS_INVALID,
			// Token: 0x04000193 RID: 403
			TRUSTEE_IS_COMPUTER
		}

		// Token: 0x0200003A RID: 58
		private struct TRUSTEE
		{
			// Token: 0x04000194 RID: 404
			internal IntPtr pMultipleTrustee;

			// Token: 0x04000195 RID: 405
			internal Native.MULTIPLE_TRUSTEE_OPERATION MultipleTrusteeOperation;

			// Token: 0x04000196 RID: 406
			internal Native.TRUSTEE_FORM TrusteeForm;

			// Token: 0x04000197 RID: 407
			internal Native.TRUSTEE_TYPE TrusteeType;

			// Token: 0x04000198 RID: 408
			internal IntPtr ptstrName;
		}

		// Token: 0x0200003B RID: 59
		private struct EXPLICIT_ACCESS
		{
			// Token: 0x04000199 RID: 409
			internal uint grfAccessPermissions;

			// Token: 0x0400019A RID: 410
			internal Native.ACCESS_MODE grfAccessMode;

			// Token: 0x0400019B RID: 411
			internal uint grfInheritance;

			// Token: 0x0400019C RID: 412
			internal Native.TRUSTEE Trustee;
		}

		// Token: 0x0200003C RID: 60
		private struct GENERIC_MAPPING
		{
			// Token: 0x0400019D RID: 413
			internal uint GenericRead;

			// Token: 0x0400019E RID: 414
			internal uint GenericWrite;

			// Token: 0x0400019F RID: 415
			internal uint GenericExecute;

			// Token: 0x040001A0 RID: 416
			internal uint GenericAll;
		}

		// Token: 0x0200003D RID: 61
		private struct LUID
		{
			// Token: 0x040001A1 RID: 417
			internal uint LowPart;

			// Token: 0x040001A2 RID: 418
			internal uint HighPart;
		}

		// Token: 0x0200003E RID: 62
		private struct LUID_AND_ATTRIBUTES
		{
			// Token: 0x040001A3 RID: 419
			internal Native.LUID Luid;

			// Token: 0x040001A4 RID: 420
			internal uint Attributes;
		}

		// Token: 0x0200003F RID: 63
		private struct PRIVILEGE_SET
		{
			// Token: 0x040001A5 RID: 421
			internal uint PrivilegeCount;

			// Token: 0x040001A6 RID: 422
			internal uint Control;

			// Token: 0x040001A7 RID: 423
			internal Native.LUID_AND_ATTRIBUTES Privilege;
		}

		// Token: 0x02000040 RID: 64
		private enum TOKEN_INFORMATION_CLASS
		{
			// Token: 0x040001A9 RID: 425
			TokenUser = 1,
			// Token: 0x040001AA RID: 426
			TokenGroups,
			// Token: 0x040001AB RID: 427
			TokenPrivileges,
			// Token: 0x040001AC RID: 428
			TokenOwner,
			// Token: 0x040001AD RID: 429
			TokenPrimaryGroup,
			// Token: 0x040001AE RID: 430
			TokenDefaultDacl,
			// Token: 0x040001AF RID: 431
			TokenSource,
			// Token: 0x040001B0 RID: 432
			TokenType,
			// Token: 0x040001B1 RID: 433
			TokenImpersonationLevel,
			// Token: 0x040001B2 RID: 434
			TokenStatistics,
			// Token: 0x040001B3 RID: 435
			TokenRestrictedSids,
			// Token: 0x040001B4 RID: 436
			TokenSessionId,
			// Token: 0x040001B5 RID: 437
			TokenGroupsAndPrivileges,
			// Token: 0x040001B6 RID: 438
			TokenSessionReference,
			// Token: 0x040001B7 RID: 439
			TokenSandBoxInert
		}

		// Token: 0x02000041 RID: 65
		private struct SID_IDENTIFIER_AUTHORITY
		{
			// Token: 0x040001B8 RID: 440
			internal byte m_Value0;

			// Token: 0x040001B9 RID: 441
			internal byte m_Value1;

			// Token: 0x040001BA RID: 442
			internal byte m_Value2;

			// Token: 0x040001BB RID: 443
			internal byte m_Value3;

			// Token: 0x040001BC RID: 444
			internal byte m_Value4;

			// Token: 0x040001BD RID: 445
			internal byte m_Value5;
		}

		// Token: 0x02000042 RID: 66
		private struct SID_AND_ATTRIBUTES
		{
			// Token: 0x040001BE RID: 446
			internal IntPtr sid;

			// Token: 0x040001BF RID: 447
			internal IntPtr attributes;
		}

		// Token: 0x02000043 RID: 67
		private struct TOKEN_GROUPS
		{
			// Token: 0x040001C0 RID: 448
			internal int GroupCount;

			// Token: 0x040001C1 RID: 449
			internal Native.SID_AND_ATTRIBUTES GroupContents;
		}

		// Token: 0x02000044 RID: 68
		private struct AUTHZ_ACCESS_REQUEST
		{
			// Token: 0x040001C2 RID: 450
			internal uint accessMask;

			// Token: 0x040001C3 RID: 451
			internal IntPtr sid;

			// Token: 0x040001C4 RID: 452
			internal IntPtr objectTypeList;

			// Token: 0x040001C5 RID: 453
			internal int objectTypeListLength;

			// Token: 0x040001C6 RID: 454
			internal IntPtr optionalArgs;
		}

		// Token: 0x02000045 RID: 69
		private struct AUTHZ_ACCESS_REPLY
		{
			// Token: 0x040001C7 RID: 455
			internal int resultListLength;

			// Token: 0x040001C8 RID: 456
			internal IntPtr grantedAccessMask;

			// Token: 0x040001C9 RID: 457
			internal IntPtr saclEvaluationResults;

			// Token: 0x040001CA RID: 458
			internal IntPtr error;
		}

		// Token: 0x02000046 RID: 70
		internal sealed class SafeAuthzContextHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x060000C3 RID: 195 RVA: 0x000065A4 File Offset: 0x000047A4
			private SafeAuthzContextHandle()
				: base(true)
			{
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x000065AD File Offset: 0x000047AD
			private SafeAuthzContextHandle(bool ownsHandle)
				: base(ownsHandle)
			{
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x000065B6 File Offset: 0x000047B6
			protected override bool ReleaseHandle()
			{
				return Native.AuthzFreeContext(this.handle);
			}
		}

		// Token: 0x02000047 RID: 71
		internal sealed class SafeAuthzResourceManagerHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x060000C6 RID: 198 RVA: 0x000065A4 File Offset: 0x000047A4
			private SafeAuthzResourceManagerHandle()
				: base(true)
			{
			}

			// Token: 0x060000C7 RID: 199 RVA: 0x000065AD File Offset: 0x000047AD
			private SafeAuthzResourceManagerHandle(bool ownsHandle)
				: base(ownsHandle)
			{
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x000065C3 File Offset: 0x000047C3
			protected override bool ReleaseHandle()
			{
				return Native.AuthzFreeResourceManager(this.handle);
			}
		}
	}
}
