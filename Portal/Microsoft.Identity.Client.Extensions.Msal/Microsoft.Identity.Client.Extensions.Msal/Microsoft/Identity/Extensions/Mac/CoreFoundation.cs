using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Extensions.Mac
{
	// Token: 0x02000005 RID: 5
	internal static class CoreFoundation
	{
		// Token: 0x06000009 RID: 9
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr CFArrayCreateMutable(IntPtr allocator, long capacity, IntPtr callbacks);

		// Token: 0x0600000A RID: 10
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void CFArrayInsertValueAtIndex(IntPtr theArray, long idx, IntPtr value);

		// Token: 0x0600000B RID: 11
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern long CFArrayGetCount(IntPtr theArray);

		// Token: 0x0600000C RID: 12
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr CFArrayGetValueAtIndex(IntPtr theArray, long idx);

		// Token: 0x0600000D RID: 13
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr CFDictionaryCreateMutable(IntPtr allocator, long capacity, IntPtr keyCallBacks, IntPtr valueCallBacks);

		// Token: 0x0600000E RID: 14
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void CFDictionaryAddValue(IntPtr theDict, IntPtr key, IntPtr value);

		// Token: 0x0600000F RID: 15
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr CFDictionaryGetValue(IntPtr theDict, IntPtr key);

		// Token: 0x06000010 RID: 16
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern bool CFDictionaryGetValueIfPresent(IntPtr theDict, IntPtr key, out IntPtr value);

		// Token: 0x06000011 RID: 17
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr CFStringCreateWithBytes(IntPtr alloc, byte[] bytes, long numBytes, CFStringEncoding encoding, bool isExternalRepresentation);

		// Token: 0x06000012 RID: 18
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern long CFStringGetLength(IntPtr theString);

		// Token: 0x06000013 RID: 19
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern bool CFStringGetCString(IntPtr theString, IntPtr buffer, long bufferSize, CFStringEncoding encoding);

		// Token: 0x06000014 RID: 20
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void CFRetain(IntPtr cf);

		// Token: 0x06000015 RID: 21
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void CFRelease(IntPtr cf);

		// Token: 0x06000016 RID: 22
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int CFGetTypeID(IntPtr cf);

		// Token: 0x06000017 RID: 23
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int CFStringGetTypeID();

		// Token: 0x06000018 RID: 24
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int CFDataGetTypeID();

		// Token: 0x06000019 RID: 25
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int CFDictionaryGetTypeID();

		// Token: 0x0600001A RID: 26
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int CFArrayGetTypeID();

		// Token: 0x0600001B RID: 27
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr CFDataGetBytePtr(IntPtr theData);

		// Token: 0x0600001C RID: 28
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int CFDataGetLength(IntPtr theData);

		// Token: 0x04000003 RID: 3
		private const string CoreFoundationFrameworkLib = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

		// Token: 0x04000004 RID: 4
		public static readonly IntPtr Handle = LibSystem.dlopen("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", 0);

		// Token: 0x04000005 RID: 5
		public static readonly IntPtr kCFBooleanTrue = LibSystem.GetGlobal(CoreFoundation.Handle, "kCFBooleanTrue");

		// Token: 0x04000006 RID: 6
		public static readonly IntPtr kCFBooleanFalse = LibSystem.GetGlobal(CoreFoundation.Handle, "kCFBooleanFalse");
	}
}
