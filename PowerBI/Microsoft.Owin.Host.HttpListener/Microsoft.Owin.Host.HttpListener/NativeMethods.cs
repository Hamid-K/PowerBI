using System;
using System.ComponentModel;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace Microsoft.Owin.Host.HttpListener
{
	// Token: 0x02000006 RID: 6
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		// Token: 0x0600000E RID: 14
		[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal static extern uint HttpSetRequestQueueProperty(CriticalHandle requestQueueHandle, NativeMethods.HTTP_SERVER_PROPERTY serverProperty, IntPtr pPropertyInfo, uint propertyInfoLength, uint reserved, IntPtr pReserved);

		// Token: 0x0600000F RID: 15 RVA: 0x000023A4 File Offset: 0x000005A4
		internal unsafe static void SetRequestQueueLength(HttpListener listener, long length)
		{
			Type listenerType = typeof(HttpListener);
			PropertyInfo requestQueueHandleProperty = listenerType.GetProperty("RequestQueueHandle", BindingFlags.Instance | BindingFlags.NonPublic);
			if (requestQueueHandleProperty == null || requestQueueHandleProperty.PropertyType != typeof(CriticalHandle))
			{
				return;
			}
			CriticalHandle requestQueueHandle = (CriticalHandle)requestQueueHandleProperty.GetValue(listener, null);
			uint result = NativeMethods.HttpSetRequestQueueProperty(requestQueueHandle, NativeMethods.HTTP_SERVER_PROPERTY.HttpServerQueueLengthProperty, new IntPtr((void*)(&length)), (uint)Marshal.SizeOf(length), 0U, IntPtr.Zero);
			if (result != 0U)
			{
				throw new Win32Exception((int)result);
			}
		}

		// Token: 0x06000010 RID: 16
		[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern uint HttpWaitForDisconnectEx(CriticalHandle requestQueueHandle, ulong connectionId, uint reserved, NativeOverlapped* pOverlapped);

		// Token: 0x0200001A RID: 26
		internal enum HTTP_SERVER_PROPERTY
		{
			// Token: 0x040000A7 RID: 167
			HttpServerAuthenticationProperty,
			// Token: 0x040000A8 RID: 168
			HttpServerLoggingProperty,
			// Token: 0x040000A9 RID: 169
			HttpServerQosProperty,
			// Token: 0x040000AA RID: 170
			HttpServerTimeoutsProperty,
			// Token: 0x040000AB RID: 171
			HttpServerQueueLengthProperty,
			// Token: 0x040000AC RID: 172
			HttpServerStateProperty,
			// Token: 0x040000AD RID: 173
			HttpServer503VerbosityProperty,
			// Token: 0x040000AE RID: 174
			HttpServerBindingProperty,
			// Token: 0x040000AF RID: 175
			HttpServerExtendedAuthenticationProperty,
			// Token: 0x040000B0 RID: 176
			HttpServerListenEndpointProperty,
			// Token: 0x040000B1 RID: 177
			HttpServerChannelBindProperty,
			// Token: 0x040000B2 RID: 178
			HttpServerProtectionLevelProperty
		}

		// Token: 0x0200001B RID: 27
		internal static class HttpErrors
		{
			// Token: 0x040000B3 RID: 179
			public const int NO_ERROR = 0;

			// Token: 0x040000B4 RID: 180
			public const int ERROR_IO_PENDING = 997;
		}
	}
}
