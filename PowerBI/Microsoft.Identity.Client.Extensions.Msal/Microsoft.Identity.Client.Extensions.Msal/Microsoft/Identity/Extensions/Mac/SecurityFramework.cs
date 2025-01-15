using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Extensions.Mac
{
	// Token: 0x02000009 RID: 9
	internal static class SecurityFramework
	{
		// Token: 0x06000027 RID: 39
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SessionGetInfo(int session, out int sessionId, out SessionAttributeBits attributes);

		// Token: 0x06000028 RID: 40
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecAccessCreate(IntPtr descriptor, IntPtr trustedList, out IntPtr accessRef);

		// Token: 0x06000029 RID: 41
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemCreateFromContent(IntPtr itemClass, IntPtr attrList, uint length, IntPtr data, IntPtr keychainRef, IntPtr initialAccess, out IntPtr itemRef);

		// Token: 0x0600002A RID: 42
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainAddGenericPassword(IntPtr keychain, uint serviceNameLength, string serviceName, uint accountNameLength, string accountName, uint passwordLength, byte[] passwordData, out IntPtr itemRef);

		// Token: 0x0600002B RID: 43
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainFindGenericPassword(IntPtr keychainOrArray, uint serviceNameLength, string serviceName, uint accountNameLength, string accountName, out uint passwordLength, out IntPtr passwordData, out IntPtr itemRef);

		// Token: 0x0600002C RID: 44
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemModifyAttributesAndData(IntPtr itemRef, IntPtr attrList, uint length, byte[] data);

		// Token: 0x0600002D RID: 45
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemDelete(IntPtr itemRef);

		// Token: 0x0600002E RID: 46
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemFreeContent(IntPtr attrList, IntPtr data);

		// Token: 0x0600002F RID: 47
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemFreeAttributesAndData(IntPtr attrList, IntPtr data);

		// Token: 0x06000030 RID: 48
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecItemCopyMatching(IntPtr query, out IntPtr result);

		// Token: 0x06000031 RID: 49
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemCopyFromPersistentReference(IntPtr persistentItemRef, out IntPtr itemRef);

		// Token: 0x06000032 RID: 50
		[DllImport("/System/Library/Frameworks/Security.framework/Security", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int SecKeychainItemCopyContent(IntPtr itemRef, IntPtr itemClass, IntPtr attrList, out uint length, out IntPtr outData);

		// Token: 0x06000033 RID: 51 RVA: 0x000022EC File Offset: 0x000004EC
		public static void ThrowIfError(int error, string defaultErrorMessage = "Unknown error.")
		{
			if (error <= -25308)
			{
				if (error == -25315)
				{
					throw new InteropException("KeyChain - user interaction is required.", error);
				}
				if (error == -25308)
				{
					throw new InteropException("KeyChain - interaction with the Security Server is not allowed.", error);
				}
			}
			else
			{
				switch (error)
				{
				case -25303:
					throw new InteropException("KeyChain - the attribute does not exist.", error);
				case -25302:
				case -25301:
				case -25298:
				case -25297:
				case -25296:
					break;
				case -25300:
					throw new InteropException("KeyChain - the item cannot be found.", error);
				case -25299:
					throw new InteropException("KeyChain - the item already exists.", error);
				case -25295:
					throw new InteropException("The keychain is not valid.", error);
				case -25294:
					throw new InteropException("The keychain does not exist.", error);
				case -25293:
					throw new InteropException("KeyChain authorization/authentication failed.", error);
				default:
					if (error == -128)
					{
						throw new InteropException("KeyChain - user cancelled the operation.", error);
					}
					if (error == 0)
					{
						return;
					}
					break;
				}
			}
			throw new InteropException(defaultErrorMessage, error);
		}

		// Token: 0x0400000E RID: 14
		private const string SecurityFrameworkLib = "/System/Library/Frameworks/Security.framework/Security";

		// Token: 0x0400000F RID: 15
		public static readonly IntPtr Handle = LibSystem.dlopen("/System/Library/Frameworks/Security.framework/Security", 0);

		// Token: 0x04000010 RID: 16
		public static readonly IntPtr kSecClass = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecClass");

		// Token: 0x04000011 RID: 17
		public static readonly IntPtr kSecMatchLimit = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecMatchLimit");

		// Token: 0x04000012 RID: 18
		public static readonly IntPtr kSecReturnAttributes = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecReturnAttributes");

		// Token: 0x04000013 RID: 19
		public static readonly IntPtr kSecReturnRef = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecReturnRef");

		// Token: 0x04000014 RID: 20
		public static readonly IntPtr kSecReturnPersistentRef = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecReturnPersistentRef");

		// Token: 0x04000015 RID: 21
		public static readonly IntPtr kSecClassGenericPassword = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecClassGenericPassword");

		// Token: 0x04000016 RID: 22
		public static readonly IntPtr kSecMatchLimitOne = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecMatchLimitOne");

		// Token: 0x04000017 RID: 23
		public static readonly IntPtr kSecMatchItemList = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecMatchItemList");

		// Token: 0x04000018 RID: 24
		public static readonly IntPtr kSecAttrLabel = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecAttrLabel");

		// Token: 0x04000019 RID: 25
		public static readonly IntPtr kSecAttrAccount = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecAttrAccount");

		// Token: 0x0400001A RID: 26
		public static readonly IntPtr kSecAttrService = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecAttrService");

		// Token: 0x0400001B RID: 27
		public static readonly IntPtr kSecValueRef = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecValueRef");

		// Token: 0x0400001C RID: 28
		public static readonly IntPtr kSecValueData = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecValueData");

		// Token: 0x0400001D RID: 29
		public static readonly IntPtr kSecReturnData = LibSystem.GetGlobal(SecurityFramework.Handle, "kSecReturnData");

		// Token: 0x0400001E RID: 30
		public const int CallerSecuritySession = -1;

		// Token: 0x0400001F RID: 31
		public const int OK = 0;

		// Token: 0x04000020 RID: 32
		public const int ErrorSecNoSuchKeychain = -25294;

		// Token: 0x04000021 RID: 33
		public const int ErrorSecInvalidKeychain = -25295;

		// Token: 0x04000022 RID: 34
		public const int ErrorSecAuthFailed = -25293;

		// Token: 0x04000023 RID: 35
		public const int ErrorSecDuplicateItem = -25299;

		// Token: 0x04000024 RID: 36
		public const int ErrorSecItemNotFound = -25300;

		// Token: 0x04000025 RID: 37
		public const int ErrorSecInteractionNotAllowed = -25308;

		// Token: 0x04000026 RID: 38
		public const int ErrorSecInteractionRequired = -25315;

		// Token: 0x04000027 RID: 39
		public const int ErrorSecNoSuchAttr = -25303;

		// Token: 0x04000028 RID: 40
		public const int ErrSecUserCanceled = -128;
	}
}
