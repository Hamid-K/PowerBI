using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Azure.Identity
{
	// Token: 0x0200006D RID: 109
	internal static class MacosNativeMethods
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x0000AFB4 File Offset: 0x000091B4
		public static void SecKeychainFindGenericPassword(IntPtr keychainOrArray, string serviceName, string accountName, out int passwordLength, out IntPtr credentialsPtr, out IntPtr itemRef)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(serviceName);
			byte[] bytes2 = Encoding.UTF8.GetBytes(accountName);
			MacosNativeMethods.ThrowIfError(MacosNativeMethods.Imports.SecKeychainFindGenericPassword(keychainOrArray, bytes.Length, bytes, bytes2.Length, bytes2, out passwordLength, out credentialsPtr, out itemRef));
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000AFF4 File Offset: 0x000091F4
		public static void SecKeychainAddGenericPassword(IntPtr keychainOrArray, string serviceName, string accountName, string password, out IntPtr itemRef)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(serviceName);
			byte[] bytes2 = Encoding.UTF8.GetBytes(accountName);
			byte[] bytes3 = Encoding.UTF8.GetBytes(password);
			MacosNativeMethods.ThrowIfError(MacosNativeMethods.Imports.SecKeychainAddGenericPassword(keychainOrArray, bytes.Length, bytes, bytes2.Length, bytes2, password.Length, bytes3, out itemRef));
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000B041 File Offset: 0x00009241
		public static void SecKeychainItemDelete(IntPtr itemRef)
		{
			MacosNativeMethods.ThrowIfError(MacosNativeMethods.Imports.SecKeychainItemDelete(itemRef));
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000B04E File Offset: 0x0000924E
		public static void SecKeychainItemFreeContent(IntPtr attrList, IntPtr data)
		{
			MacosNativeMethods.ThrowIfError(MacosNativeMethods.Imports.SecKeychainItemFreeContent(attrList, data));
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000B05C File Offset: 0x0000925C
		public static void CFRelease(IntPtr cfRef)
		{
			if (cfRef != IntPtr.Zero)
			{
				MacosNativeMethods.Imports.CFRelease(cfRef);
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000B071 File Offset: 0x00009271
		private static void ThrowIfError(int status)
		{
			if (status != 0)
			{
				throw new InvalidOperationException(MacosNativeMethods.GetErrorMessageString(status));
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000B084 File Offset: 0x00009284
		private static string GetErrorMessageString(int status)
		{
			string text;
			if (status != -25315)
			{
				if (status != -25308)
				{
					switch (status)
					{
					case -25303:
						return string.Format("The attribute does not exist. [{0}]", status);
					case -25300:
						return string.Format("The item cannot be found. [{0}]", status);
					case -25299:
						return string.Format("The item already exists. [{0}]", status);
					case -25295:
						return string.Format("The keychain is not valid. [{0}]", status);
					case -25294:
						return string.Format("The keychain does not exist. [{0}]", status);
					case -25293:
						return string.Format("Authorization/Authentication failed. [{0}]", status);
					}
					text = string.Format("Unknown error. [{0}]", status);
				}
				else
				{
					text = string.Format("Interaction with the Security Server is not allowed. [{0}]", status);
				}
			}
			else
			{
				text = string.Format("User interaction is required. [{0}]", status);
			}
			return text;
		}

		// Token: 0x04000222 RID: 546
		public const int SecStatusCodeSuccess = 0;

		// Token: 0x04000223 RID: 547
		public const int SecStatusCodeNoSuchKeychain = -25294;

		// Token: 0x04000224 RID: 548
		public const int SecStatusCodeInvalidKeychain = -25295;

		// Token: 0x04000225 RID: 549
		public const int SecStatusCodeAuthFailed = -25293;

		// Token: 0x04000226 RID: 550
		public const int SecStatusCodeDuplicateItem = -25299;

		// Token: 0x04000227 RID: 551
		public const int SecStatusCodeItemNotFound = -25300;

		// Token: 0x04000228 RID: 552
		public const int SecStatusCodeInteractionNotAllowed = -25308;

		// Token: 0x04000229 RID: 553
		public const int SecStatusCodeInteractionRequired = -25315;

		// Token: 0x0400022A RID: 554
		public const int SecStatusCodeNoSuchAttr = -25303;

		// Token: 0x020000F5 RID: 245
		public static class Imports
		{
			// Token: 0x0600059D RID: 1437
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
			[DllImport("/System/Library/Frameworks/CoreFoundation.framework/Versions/A/CoreFoundation", CharSet = CharSet.Unicode)]
			public static extern void CFRelease(IntPtr cfRef);

			// Token: 0x0600059E RID: 1438
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
			[DllImport("/System/Library/Frameworks/Security.framework/Security")]
			public static extern int SecKeychainFindGenericPassword(IntPtr keychainOrArray, int serviceNameLength, byte[] serviceName, int accountNameLength, byte[] accountName, out int passwordLength, out IntPtr passwordData, out IntPtr itemRef);

			// Token: 0x0600059F RID: 1439
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
			[DllImport("/System/Library/Frameworks/Security.framework/Security")]
			public static extern int SecKeychainAddGenericPassword(IntPtr keychain, int serviceNameLength, byte[] serviceName, int accountNameLength, byte[] accountName, int passwordLength, byte[] passwordData, out IntPtr itemRef);

			// Token: 0x060005A0 RID: 1440
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
			[DllImport("/System/Library/Frameworks/Security.framework/Security")]
			public static extern int SecKeychainItemDelete(IntPtr itemRef);

			// Token: 0x060005A1 RID: 1441
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
			[DllImport("/System/Library/Frameworks/Security.framework/Security")]
			public static extern int SecKeychainItemFreeContent(IntPtr attrList, IntPtr data);

			// Token: 0x060005A2 RID: 1442
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories | DllImportSearchPath.AssemblyDirectory)]
			[DllImport("/System/Library/Frameworks/Security.framework/Security")]
			public static extern IntPtr SecCopyErrorMessageString(int status, IntPtr reserved);

			// Token: 0x040004DA RID: 1242
			private const string CoreFoundationLibrary = "/System/Library/Frameworks/CoreFoundation.framework/Versions/A/CoreFoundation";

			// Token: 0x040004DB RID: 1243
			private const string SecurityLibrary = "/System/Library/Frameworks/Security.framework/Security";
		}
	}
}
