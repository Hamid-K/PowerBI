using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000361 RID: 865
	[SuppressUnmanagedCodeSecurity]
	internal static class EventEtwNativeMethods
	{
		// Token: 0x06001E7C RID: 7804
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegisterTraceGuidsW", ExactSpelling = true)]
		internal unsafe static extern uint RegisterTraceGuids(EventEtwNativeMethods.EtwEnableCallback requestAddress, void* requestContext, ref Guid controlGuid, uint guidCount, ref EventEtwNativeMethods.TRACE_GUID_REGISTRATION traceGuidReg, string mofImagePath, string mofResourceName, out ulong registrationHandle);

		// Token: 0x06001E7D RID: 7805
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int UnregisterTraceGuids(ulong registrationHandle);

		// Token: 0x06001E7E RID: 7806
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern ulong GetTraceLoggerHandle(byte* Buffer);

		// Token: 0x06001E7F RID: 7807
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern uint GetTraceEnableFlags(ulong TraceHandle);

		// Token: 0x06001E80 RID: 7808
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern byte GetTraceEnableLevel(ulong TraceHandle);

		// Token: 0x06001E81 RID: 7809
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint TraceEvent(ulong traceHandle, byte* eventTrace);

		// Token: 0x04001139 RID: 4409
		internal const string ADVAPI32 = "advapi32.dll";

		// Token: 0x02000362 RID: 866
		[StructLayout(LayoutKind.Explicit, Size = 48)]
		internal struct EVENT_TRACE_HEADER
		{
			// Token: 0x0400113A RID: 4410
			[FieldOffset(0)]
			internal short Size;

			// Token: 0x0400113B RID: 4411
			[FieldOffset(2)]
			internal ushort FieldTypeFlags;

			// Token: 0x0400113C RID: 4412
			[FieldOffset(4)]
			internal byte Type;

			// Token: 0x0400113D RID: 4413
			[FieldOffset(5)]
			internal byte Level;

			// Token: 0x0400113E RID: 4414
			[FieldOffset(6)]
			internal short Version;

			// Token: 0x0400113F RID: 4415
			[FieldOffset(8)]
			internal uint ThreadId;

			// Token: 0x04001140 RID: 4416
			[FieldOffset(12)]
			internal uint ProcessId;

			// Token: 0x04001141 RID: 4417
			[FieldOffset(16)]
			internal long TimeStamp;

			// Token: 0x04001142 RID: 4418
			[FieldOffset(24)]
			internal Guid GuidPtr;

			// Token: 0x04001143 RID: 4419
			[FieldOffset(24)]
			internal Guid Guid;

			// Token: 0x04001144 RID: 4420
			[FieldOffset(40)]
			internal uint ClientContext;

			// Token: 0x04001145 RID: 4421
			[FieldOffset(44)]
			internal uint Flags;
		}

		// Token: 0x02000363 RID: 867
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
		internal struct TRACE_GUID_REGISTRATION
		{
			// Token: 0x04001146 RID: 4422
			internal IntPtr Id;

			// Token: 0x04001147 RID: 4423
			internal IntPtr RegHandle;
		}

		// Token: 0x02000364 RID: 868
		internal enum WMIDPREQUESTCODE : uint
		{
			// Token: 0x04001149 RID: 4425
			WMI_ENABLE_EVENTS = 4U,
			// Token: 0x0400114A RID: 4426
			WMI_DISABLE_EVENTS
		}

		// Token: 0x02000365 RID: 869
		// (Invoke) Token: 0x06001E83 RID: 7811
		internal unsafe delegate uint EtwEnableCallback(EventEtwNativeMethods.WMIDPREQUESTCODE requestCode, IntPtr requestContext, uint* bufferSize, byte* buffer);
	}
}
