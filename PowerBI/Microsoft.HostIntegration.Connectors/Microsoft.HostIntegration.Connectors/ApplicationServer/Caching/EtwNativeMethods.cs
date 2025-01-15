using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001CE RID: 462
	[SuppressUnmanagedCodeSecurity]
	internal static class EtwNativeMethods
	{
		// Token: 0x06000F1B RID: 3867
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegisterTraceGuidsW", ExactSpelling = true)]
		internal unsafe static extern uint RegisterTraceGuids(EtwNativeMethods.EtwEnableCallback requestAddress, void* requestContext, ref Guid controlGuid, uint guidCount, ref EtwNativeMethods.TRACE_GUID_REGISTRATION traceGuidReg, string mofImagePath, string mofResourceName, out ulong registrationHandle);

		// Token: 0x06000F1C RID: 3868
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int UnregisterTraceGuids(ulong registrationHandle);

		// Token: 0x06000F1D RID: 3869
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern ulong GetTraceLoggerHandle(byte* Buffer);

		// Token: 0x06000F1E RID: 3870
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern uint GetTraceEnableFlags(ulong TraceHandle);

		// Token: 0x06000F1F RID: 3871
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern byte GetTraceEnableLevel(ulong TraceHandle);

		// Token: 0x06000F20 RID: 3872
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint TraceEvent(ulong traceHandle, byte* eventTrace);

		// Token: 0x04000A63 RID: 2659
		internal const string ADVAPI32 = "advapi32.dll";

		// Token: 0x020001CF RID: 463
		[StructLayout(LayoutKind.Explicit, Size = 48)]
		internal struct EVENT_TRACE_HEADER
		{
			// Token: 0x04000A64 RID: 2660
			[FieldOffset(0)]
			internal short Size;

			// Token: 0x04000A65 RID: 2661
			[FieldOffset(2)]
			internal ushort FieldTypeFlags;

			// Token: 0x04000A66 RID: 2662
			[FieldOffset(4)]
			internal byte Type;

			// Token: 0x04000A67 RID: 2663
			[FieldOffset(5)]
			internal byte Level;

			// Token: 0x04000A68 RID: 2664
			[FieldOffset(6)]
			internal short Version;

			// Token: 0x04000A69 RID: 2665
			[FieldOffset(8)]
			internal uint ThreadId;

			// Token: 0x04000A6A RID: 2666
			[FieldOffset(12)]
			internal uint ProcessId;

			// Token: 0x04000A6B RID: 2667
			[FieldOffset(16)]
			internal long TimeStamp;

			// Token: 0x04000A6C RID: 2668
			[FieldOffset(24)]
			internal Guid GuidPtr;

			// Token: 0x04000A6D RID: 2669
			[FieldOffset(24)]
			internal Guid Guid;

			// Token: 0x04000A6E RID: 2670
			[FieldOffset(40)]
			internal uint ClientContext;

			// Token: 0x04000A6F RID: 2671
			[FieldOffset(44)]
			internal uint Flags;
		}

		// Token: 0x020001D0 RID: 464
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
		internal struct TRACE_GUID_REGISTRATION
		{
			// Token: 0x04000A70 RID: 2672
			internal IntPtr Id;

			// Token: 0x04000A71 RID: 2673
			internal IntPtr RegHandle;
		}

		// Token: 0x020001D1 RID: 465
		internal enum WMIDPREQUESTCODE : uint
		{
			// Token: 0x04000A73 RID: 2675
			WMI_ENABLE_EVENTS = 4U,
			// Token: 0x04000A74 RID: 2676
			WMI_DISABLE_EVENTS
		}

		// Token: 0x020001D2 RID: 466
		// (Invoke) Token: 0x06000F22 RID: 3874
		internal unsafe delegate uint EtwEnableCallback(EtwNativeMethods.WMIDPREQUESTCODE requestCode, IntPtr requestContext, uint* bufferSize, byte* buffer);
	}
}
