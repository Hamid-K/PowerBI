using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs
{
	// Token: 0x02000189 RID: 393
	internal static class SecurityFramework
	{
		// Token: 0x060012D3 RID: 4819
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SessionGetInfo(int session, out int sessionId, out SessionAttributeBits attributes);

		// Token: 0x060012D4 RID: 4820
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecAccessCreate(IntPtr descriptor, IntPtr trustedList, out IntPtr accessRef);

		// Token: 0x060012D5 RID: 4821
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemCreateFromContent(IntPtr itemClass, IntPtr attrList, uint length, IntPtr data, IntPtr keychainRef, IntPtr initialAccess, out IntPtr itemRef);

		// Token: 0x060012D6 RID: 4822
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainAddGenericPassword(IntPtr keychain, uint serviceNameLength, string serviceName, uint accountNameLength, string accountName, uint passwordLength, byte[] passwordData, out IntPtr itemRef);

		// Token: 0x060012D7 RID: 4823
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainFindGenericPassword(IntPtr keychainOrArray, uint serviceNameLength, string serviceName, uint accountNameLength, string accountName, out uint passwordLength, out IntPtr passwordData, out IntPtr itemRef);

		// Token: 0x060012D8 RID: 4824
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public unsafe static extern int SecKeychainItemCopyAttributesAndData(IntPtr itemRef, IntPtr info, IntPtr itemClass, SecKeychainAttributeList** attrList, uint* dataLength, void** data);

		// Token: 0x060012D9 RID: 4825
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemModifyAttributesAndData(IntPtr itemRef, IntPtr attrList, uint length, byte[] data);

		// Token: 0x060012DA RID: 4826
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemDelete(IntPtr itemRef);

		// Token: 0x060012DB RID: 4827
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemFreeContent(IntPtr attrList, IntPtr data);

		// Token: 0x060012DC RID: 4828
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemFreeAttributesAndData(IntPtr attrList, IntPtr data);

		// Token: 0x060012DD RID: 4829
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecItemCopyMatching(IntPtr query, out IntPtr result);

		// Token: 0x060012DE RID: 4830
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemCopyFromPersistentReference(IntPtr persistentItemRef, out IntPtr itemRef);

		// Token: 0x060012DF RID: 4831
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemCopyContent(IntPtr itemRef, IntPtr itemClass, IntPtr attrList, out uint length, out IntPtr outData);

		// Token: 0x040006FB RID: 1787
		private const string SecurityFrameworkLib = "/System/Library/Frameworks/Security.framework/Security";

		// Token: 0x040006FC RID: 1788
		public static readonly IntPtr Handle = LibSystem.dlopen("/System/Library/Frameworks/Security.framework/Security", 0);

		// Token: 0x040006FD RID: 1789
		public static readonly IntPtr kSecClass = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecClass");

		// Token: 0x040006FE RID: 1790
		public static readonly IntPtr kSecMatchLimit = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecMatchLimit");

		// Token: 0x040006FF RID: 1791
		public static readonly IntPtr kSecReturnAttributes = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecReturnAttributes");

		// Token: 0x04000700 RID: 1792
		public static readonly IntPtr kSecReturnRef = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecReturnRef");

		// Token: 0x04000701 RID: 1793
		public static readonly IntPtr kSecReturnPersistentRef = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecReturnPersistentRef");

		// Token: 0x04000702 RID: 1794
		public static readonly IntPtr kSecClassGenericPassword = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecClassGenericPassword");

		// Token: 0x04000703 RID: 1795
		public static readonly IntPtr kSecMatchLimitOne = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecMatchLimitOne");

		// Token: 0x04000704 RID: 1796
		public static readonly IntPtr kSecMatchItemList = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecMatchItemList");

		// Token: 0x04000705 RID: 1797
		public static readonly IntPtr kSecAttrLabel = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecAttrLabel");

		// Token: 0x04000706 RID: 1798
		public static readonly IntPtr kSecAttrAccount = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecAttrAccount");

		// Token: 0x04000707 RID: 1799
		public static readonly IntPtr kSecAttrService = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecAttrService");

		// Token: 0x04000708 RID: 1800
		public static readonly IntPtr kSecValueRef = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecValueRef");

		// Token: 0x04000709 RID: 1801
		public static readonly IntPtr kSecValueData = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecValueData");

		// Token: 0x0400070A RID: 1802
		public static readonly IntPtr kSecReturnData = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecReturnData");

		// Token: 0x0400070B RID: 1803
		public const int CallerSecuritySession = -1;

		// Token: 0x0400070C RID: 1804
		public const int OK = 0;

		// Token: 0x0400070D RID: 1805
		public const int ErrorSecNoSuchKeychain = -25294;

		// Token: 0x0400070E RID: 1806
		public const int ErrorSecInvalidKeychain = -25295;

		// Token: 0x0400070F RID: 1807
		public const int ErrorSecAuthFailed = -25293;

		// Token: 0x04000710 RID: 1808
		public const int ErrorSecDuplicateItem = -25299;

		// Token: 0x04000711 RID: 1809
		public const int ErrorSecItemNotFound = -25300;

		// Token: 0x04000712 RID: 1810
		public const int ErrorSecInteractionNotAllowed = -25308;

		// Token: 0x04000713 RID: 1811
		public const int ErrorSecInteractionRequired = -25315;

		// Token: 0x04000714 RID: 1812
		public const int ErrorSecNoSuchAttr = -25303;
	}
}
