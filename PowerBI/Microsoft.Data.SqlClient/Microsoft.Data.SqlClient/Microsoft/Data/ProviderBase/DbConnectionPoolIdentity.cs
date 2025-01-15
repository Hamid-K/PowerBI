using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Data.Common;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000175 RID: 373
	[Serializable]
	internal sealed class DbConnectionPoolIdentity
	{
		// Token: 0x06001B97 RID: 7063 RVA: 0x00071343 File Offset: 0x0006F543
		private DbConnectionPoolIdentity(string sidString, bool isRestricted, bool isNetwork)
		{
			this._sidString = sidString;
			this._isRestricted = isRestricted;
			this._isNetwork = isNetwork;
			this._hashCode = ((sidString == null) ? 0 : sidString.GetHashCode());
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x00071372 File Offset: 0x0006F572
		internal bool IsRestricted
		{
			get
			{
				return this._isRestricted;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x0007137A File Offset: 0x0006F57A
		internal bool IsNetwork
		{
			get
			{
				return this._isNetwork;
			}
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x00071384 File Offset: 0x0006F584
		private static byte[] CreateWellKnownSid(WellKnownSidType sidType)
		{
			uint maxBinaryLength = (uint)SecurityIdentifier.MaxBinaryLength;
			byte[] array = new byte[maxBinaryLength];
			if (UnsafeNativeMethods.CreateWellKnownSid((int)sidType, null, array, ref maxBinaryLength) == 0)
			{
				DbConnectionPoolIdentity.IntegratedSecurityError(5);
			}
			return array;
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000713B4 File Offset: 0x0006F5B4
		public override bool Equals(object value)
		{
			bool flag = this == DbConnectionPoolIdentity.NoIdentity || this == value;
			if (!flag && value != null)
			{
				DbConnectionPoolIdentity dbConnectionPoolIdentity = (DbConnectionPoolIdentity)value;
				flag = this._sidString == dbConnectionPoolIdentity._sidString && this._isRestricted == dbConnectionPoolIdentity._isRestricted && this._isNetwork == dbConnectionPoolIdentity._isNetwork;
			}
			return flag;
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x00071412 File Offset: 0x0006F612
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		internal static WindowsIdentity GetCurrentWindowsIdentity()
		{
			return WindowsIdentity.GetCurrent();
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x00071419 File Offset: 0x0006F619
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private static IntPtr GetWindowsIdentityToken(WindowsIdentity identity)
		{
			return identity.Token;
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x00071424 File Offset: 0x0006F624
		internal static DbConnectionPoolIdentity GetCurrent()
		{
			if (!ADP.s_isWindowsNT)
			{
				return DbConnectionPoolIdentity.NoIdentity;
			}
			WindowsIdentity currentWindowsIdentity = DbConnectionPoolIdentity.GetCurrentWindowsIdentity();
			IntPtr windowsIdentityToken = DbConnectionPoolIdentity.GetWindowsIdentityToken(currentWindowsIdentity);
			uint num = 2048U;
			uint num2 = 0U;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			bool flag = Win32NativeMethods.IsTokenRestrictedWrapper(windowsIdentityToken);
			DbConnectionPoolIdentity dbConnectionPoolIdentity = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				bool flag2;
				if (!UnsafeNativeMethods.CheckTokenMembership(windowsIdentityToken, DbConnectionPoolIdentity.NetworkSid, out flag2))
				{
					DbConnectionPoolIdentity.IntegratedSecurityError(1);
				}
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					intPtr = SafeNativeMethods.LocalAlloc(0, (IntPtr)((long)((ulong)num)));
				}
				if (IntPtr.Zero == intPtr)
				{
					throw new OutOfMemoryException();
				}
				if (!UnsafeNativeMethods.GetTokenInformation(windowsIdentityToken, 1U, intPtr, num, ref num2))
				{
					if (num2 > num)
					{
						num = num2;
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
						}
						finally
						{
							SafeNativeMethods.LocalFree(intPtr);
							intPtr = IntPtr.Zero;
							intPtr = SafeNativeMethods.LocalAlloc(0, (IntPtr)((long)((ulong)num)));
						}
						if (IntPtr.Zero == intPtr)
						{
							throw new OutOfMemoryException();
						}
						if (!UnsafeNativeMethods.GetTokenInformation(windowsIdentityToken, 1U, intPtr, num, ref num2))
						{
							DbConnectionPoolIdentity.IntegratedSecurityError(2);
						}
					}
					else
					{
						DbConnectionPoolIdentity.IntegratedSecurityError(3);
					}
				}
				currentWindowsIdentity.Dispose();
				IntPtr intPtr3 = Marshal.ReadIntPtr(intPtr, 0);
				if (!UnsafeNativeMethods.ConvertSidToStringSidW(intPtr3, out intPtr2))
				{
					DbConnectionPoolIdentity.IntegratedSecurityError(4);
				}
				if (IntPtr.Zero == intPtr2)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.ConvertSidToStringSidWReturnedNull);
				}
				string text = Marshal.PtrToStringUni(intPtr2);
				DbConnectionPoolIdentity lastIdentity = DbConnectionPoolIdentity._lastIdentity;
				if (lastIdentity != null && lastIdentity._sidString == text && lastIdentity._isRestricted == flag && lastIdentity._isNetwork == flag2)
				{
					dbConnectionPoolIdentity = lastIdentity;
				}
				else
				{
					dbConnectionPoolIdentity = new DbConnectionPoolIdentity(text, flag, flag2);
				}
			}
			finally
			{
				if (IntPtr.Zero != intPtr)
				{
					SafeNativeMethods.LocalFree(intPtr);
					intPtr = IntPtr.Zero;
				}
				if (IntPtr.Zero != intPtr2)
				{
					SafeNativeMethods.LocalFree(intPtr2);
					intPtr2 = IntPtr.Zero;
				}
			}
			DbConnectionPoolIdentity._lastIdentity = dbConnectionPoolIdentity;
			return dbConnectionPoolIdentity;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00071634 File Offset: 0x0006F834
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0007163C File Offset: 0x0006F83C
		private static void IntegratedSecurityError(int caller)
		{
			int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
			if (1 != caller || -2147023587 != hrforLastWin32Error)
			{
				Marshal.ThrowExceptionForHR(hrforLastWin32Error);
			}
		}

		// Token: 0x04000B45 RID: 2885
		private const int E_NotImpersonationToken = -2147023587;

		// Token: 0x04000B46 RID: 2886
		private const int Win32_CheckTokenMembership = 1;

		// Token: 0x04000B47 RID: 2887
		private const int Win32_GetTokenInformation_1 = 2;

		// Token: 0x04000B48 RID: 2888
		private const int Win32_GetTokenInformation_2 = 3;

		// Token: 0x04000B49 RID: 2889
		private const int Win32_ConvertSidToStringSidW = 4;

		// Token: 0x04000B4A RID: 2890
		private const int Win32_CreateWellKnownSid = 5;

		// Token: 0x04000B4B RID: 2891
		public static readonly DbConnectionPoolIdentity NoIdentity = new DbConnectionPoolIdentity(string.Empty, false, true);

		// Token: 0x04000B4C RID: 2892
		private static readonly byte[] NetworkSid = (ADP.s_isWindowsNT ? DbConnectionPoolIdentity.CreateWellKnownSid(WellKnownSidType.NetworkSid) : null);

		// Token: 0x04000B4D RID: 2893
		private static DbConnectionPoolIdentity _lastIdentity = null;

		// Token: 0x04000B4E RID: 2894
		private readonly string _sidString;

		// Token: 0x04000B4F RID: 2895
		private readonly bool _isRestricted;

		// Token: 0x04000B50 RID: 2896
		private readonly bool _isNetwork;

		// Token: 0x04000B51 RID: 2897
		private readonly int _hashCode;
	}
}
