using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000021 RID: 33
	internal static class NativeLoggingMethods
	{
		// Token: 0x06000109 RID: 265
		[DllImport("ReportingServicesService.exe", CharSet = CharSet.Unicode)]
		public static extern SafeNativeLoggingPointer CreateNativeLoggingObject(string AppDomain);

		// Token: 0x0600010A RID: 266
		[DllImport("ReportingServicesService.exe")]
		public static extern void ReleaseNativeLoggingObject(IntPtr nativeLoggingObject);

		// Token: 0x0600010B RID: 267
		[DllImport("ReportingServicesService.exe", CharSet = CharSet.Unicode)]
		public static extern void NativeLoggingTrace(SafeNativeLoggingPointer nativeLoggingObject, TraceLevel traceLevel, string componentName, string message, bool isAssert, bool isException, bool allowEventLogWrite);

		// Token: 0x0600010C RID: 268
		[DllImport("ReportingServicesService.exe", CharSet = CharSet.Unicode)]
		public static extern IntPtr GetNativeLoggingTraceDirectory(SafeNativeLoggingPointer nativeLoggingObject);

		// Token: 0x0600010D RID: 269
		[DllImport("ReportingServicesService.exe", CharSet = CharSet.Unicode)]
		public static extern IntPtr GetNativeLoggingTracePath(SafeNativeLoggingPointer nativeLoggingObject);

		// Token: 0x0600010E RID: 270
		[DllImport("ReportingServicesService.exe", CharSet = CharSet.Unicode)]
		public static extern bool GetNativeTraceLevel(SafeNativeLoggingPointer nativeLoggingObject, string component, out TraceLevel traceLevel);

		// Token: 0x0600010F RID: 271
		[DllImport("ReportingServicesService.exe", CharSet = CharSet.Unicode)]
		public static extern IntPtr GetDefaultTraceLevel(SafeNativeLoggingPointer nativeLoggingObject);

		// Token: 0x04000094 RID: 148
		private const string ReportingServiceExe = "ReportingServicesService.exe";
	}
}
