using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Identity.Extensions;
using Microsoft.Identity.Extensions.Mac;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x0200001E RID: 30
	internal class MacOSKeychain
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00003790 File Offset: 0x00001990
		public MacOSKeychain(string @namespace = null)
		{
			this._namespace = @namespace;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000037A0 File Offset: 0x000019A0
		public MacOSKeychainCredential Get(string service, string account)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			MacOSKeychainCredential macOSKeychainCredential;
			try
			{
				intPtr = CoreFoundation.CFDictionaryCreateMutable(IntPtr.Zero, 0L, IntPtr.Zero, IntPtr.Zero);
				CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecClass, SecurityFramework.kSecClassGenericPassword);
				CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecMatchLimit, SecurityFramework.kSecMatchLimitOne);
				CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecReturnData, CoreFoundation.kCFBooleanTrue);
				CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecReturnAttributes, CoreFoundation.kCFBooleanTrue);
				if (!string.IsNullOrWhiteSpace(service))
				{
					intPtr2 = MacOSKeychain.CreateCFStringUtf8(this.CreateServiceName(service));
					CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecAttrService, intPtr2);
				}
				if (!string.IsNullOrWhiteSpace(account))
				{
					intPtr3 = MacOSKeychain.CreateCFStringUtf8(account);
					CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecAttrAccount, intPtr3);
				}
				int num = SecurityFramework.SecItemCopyMatching(intPtr, out zero);
				if (num != -25300)
				{
					if (num == 0)
					{
						int num2 = CoreFoundation.CFGetTypeID(zero);
						if (num2 != CoreFoundation.CFDictionaryGetTypeID())
						{
							throw new InteropException(string.Format("Unknown keychain search result type CFTypeID: {0}.", num2), -1);
						}
						macOSKeychainCredential = MacOSKeychain.CreateCredentialFromAttributes(zero);
					}
					else
					{
						SecurityFramework.ThrowIfError(num, "Unknown error.");
						macOSKeychainCredential = null;
					}
				}
				else
				{
					macOSKeychainCredential = null;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(intPtr2);
				}
				if (intPtr3 != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(intPtr3);
				}
				if (zero != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(zero);
				}
			}
			return macOSKeychainCredential;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003918 File Offset: 0x00001B18
		public void AddOrUpdate(string service, string account, byte[] secretBytes)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			string text = this.CreateServiceName(service);
			uint length = (uint)text.Length;
			uint num = (uint)((account != null) ? account.Length : 0);
			try
			{
				uint num3;
				int num2 = SecurityFramework.SecKeychainFindGenericPassword(IntPtr.Zero, length, text, num, account, out num3, out zero, out zero2);
				if (num2 != -25300)
				{
					if (num2 == 0)
					{
						SecurityFramework.ThrowIfError(SecurityFramework.SecKeychainItemModifyAttributesAndData(zero2, IntPtr.Zero, (uint)secretBytes.Length, secretBytes), "Could not update existing item");
					}
					else
					{
						SecurityFramework.ThrowIfError(num2, "Unknown error.");
					}
				}
				else
				{
					SecurityFramework.ThrowIfError(SecurityFramework.SecKeychainAddGenericPassword(IntPtr.Zero, length, text, num, account, (uint)secretBytes.Length, secretBytes, out zero2), "Could not create new item");
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					SecurityFramework.SecKeychainItemFreeContent(IntPtr.Zero, zero);
				}
				if (zero2 != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(zero2);
				}
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000039FC File Offset: 0x00001BFC
		public bool Remove(string service, string account)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			bool flag;
			try
			{
				intPtr = CoreFoundation.CFDictionaryCreateMutable(IntPtr.Zero, 0L, IntPtr.Zero, IntPtr.Zero);
				CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecClass, SecurityFramework.kSecClassGenericPassword);
				CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecMatchLimit, SecurityFramework.kSecMatchLimitOne);
				CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecReturnRef, CoreFoundation.kCFBooleanTrue);
				if (!string.IsNullOrWhiteSpace(service))
				{
					intPtr2 = MacOSKeychain.CreateCFStringUtf8(this.CreateServiceName(service));
					CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecAttrService, intPtr2);
				}
				if (!string.IsNullOrWhiteSpace(account))
				{
					intPtr3 = MacOSKeychain.CreateCFStringUtf8(account);
					CoreFoundation.CFDictionaryAddValue(intPtr, SecurityFramework.kSecAttrAccount, intPtr3);
				}
				int num = SecurityFramework.SecItemCopyMatching(intPtr, out zero);
				if (num != -25300)
				{
					if (num == 0)
					{
						SecurityFramework.ThrowIfError(SecurityFramework.SecKeychainItemDelete(zero), "Unknown error.");
						flag = true;
					}
					else
					{
						SecurityFramework.ThrowIfError(num, "Unknown error.");
						flag = false;
					}
				}
				else
				{
					flag = false;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(intPtr);
				}
				if (zero != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(zero);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(intPtr2);
				}
				if (intPtr3 != IntPtr.Zero)
				{
					CoreFoundation.CFRelease(intPtr3);
				}
			}
			return flag;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003B48 File Offset: 0x00001D48
		private static IntPtr CreateCFStringUtf8(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			return CoreFoundation.CFStringCreateWithBytes(IntPtr.Zero, bytes, (long)bytes.Length, CFStringEncoding.kCFStringEncodingUTF8, false);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003B78 File Offset: 0x00001D78
		private static MacOSKeychainCredential CreateCredentialFromAttributes(IntPtr attributes)
		{
			string stringAttribute = MacOSKeychain.GetStringAttribute(attributes, SecurityFramework.kSecAttrService);
			string stringAttribute2 = MacOSKeychain.GetStringAttribute(attributes, SecurityFramework.kSecAttrAccount);
			byte[] byteArrayAtrribute = MacOSKeychain.GetByteArrayAtrribute(attributes, SecurityFramework.kSecValueData);
			string stringAttribute3 = MacOSKeychain.GetStringAttribute(attributes, SecurityFramework.kSecAttrLabel);
			return new MacOSKeychainCredential(stringAttribute, stringAttribute2, byteArrayAtrribute, stringAttribute3);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003BBC File Offset: 0x00001DBC
		private static byte[] GetByteArrayAtrribute(IntPtr dict, IntPtr key)
		{
			if (dict == IntPtr.Zero)
			{
				return null;
			}
			IntPtr intPtr;
			if (CoreFoundation.CFDictionaryGetValueIfPresent(dict, key, out intPtr) && intPtr != IntPtr.Zero && CoreFoundation.CFGetTypeID(intPtr) == CoreFoundation.CFDataGetTypeID())
			{
				int num = CoreFoundation.CFDataGetLength(intPtr);
				if (num > 0)
				{
					IntPtr intPtr2 = CoreFoundation.CFDataGetBytePtr(intPtr);
					byte[] array = new byte[num];
					Marshal.Copy(intPtr2, array, 0, num);
					return array;
				}
			}
			return null;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003C20 File Offset: 0x00001E20
		private static string GetStringAttribute(IntPtr dict, IntPtr key)
		{
			if (dict == IntPtr.Zero)
			{
				return null;
			}
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				IntPtr intPtr2;
				if (CoreFoundation.CFDictionaryGetValueIfPresent(dict, key, out intPtr2) && intPtr2 != IntPtr.Zero)
				{
					if (CoreFoundation.CFGetTypeID(intPtr2) == CoreFoundation.CFStringGetTypeID())
					{
						int num = (int)CoreFoundation.CFStringGetLength(intPtr2);
						int num2 = num + 1;
						intPtr = Marshal.AllocHGlobal(num2);
						if (CoreFoundation.CFStringGetCString(intPtr2, intPtr, (long)num2, CFStringEncoding.kCFStringEncodingUTF8))
						{
							return Marshal.PtrToStringAuto(intPtr, num);
						}
					}
					if (CoreFoundation.CFGetTypeID(intPtr2) == CoreFoundation.CFDataGetTypeID())
					{
						int num3 = CoreFoundation.CFDataGetLength(intPtr2);
						if (num3 > 0)
						{
							return Marshal.PtrToStringAuto(CoreFoundation.CFDataGetBytePtr(intPtr2), num3);
						}
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return null;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003CEC File Offset: 0x00001EEC
		private string CreateServiceName(string service)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrWhiteSpace(this._namespace))
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}:", this._namespace);
			}
			stringBuilder.Append(service);
			return stringBuilder.ToString();
		}

		// Token: 0x0400006F RID: 111
		private readonly string _namespace;
	}
}
