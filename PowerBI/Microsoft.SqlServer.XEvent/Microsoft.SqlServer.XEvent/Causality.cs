using System;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x020000A6 RID: 166
	public static class Causality
	{
		// Token: 0x060001ED RID: 493 RVA: 0x0000B418 File Offset: 0x0000B418
		public unsafe static ActivityId GetTransferActivityId()
		{
			XEActivityId xeactivityId;
			if (<Module>.XE_API.IsEnginePresent() != null && calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)(*((ref <Module>.?sm_ClientAPI@XE_API@@2UXEEngineClientAPI@@A) + 40))) != null && calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(XEActivityId* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), ref xeactivityId, (IntPtr)(*((ref <Module>.?sm_ClientAPI@XE_API@@2UXEEngineClientAPI@@A) + 360))) != null)
			{
				return Marshal.PtrToStructure((IntPtr)((void*)(&xeactivityId)), typeof(ActivityId));
			}
			return Marshal.PtrToStructure((IntPtr)((void*)(&<Module>.XEAID_FAIL)), typeof(ActivityId));
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000B4D8 File Offset: 0x0000B4D8
		public unsafe static void SetTransferActivityId(ValueType transferId)
		{
			if (<Module>.XE_API.IsEnginePresent() != null && calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)(*((ref <Module>.?sm_ClientAPI@XE_API@@2UXEEngineClientAPI@@A) + 40))) != null)
			{
				XEActivityId xeactivityId;
				IntPtr intPtr = (IntPtr)((void*)(&xeactivityId));
				Marshal.StructureToPtr(transferId, intPtr, false);
				*((ref xeactivityId) + 16) = 0;
				int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(XEActivityId modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), ref xeactivityId, (IntPtr)(*((ref <Module>.?sm_ClientAPI@XE_API@@2UXEEngineClientAPI@@A) + 368)));
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000B488 File Offset: 0x0000B488
		public unsafe static void SetTransferActivityId(ActivityId transferId)
		{
			if (<Module>.XE_API.IsEnginePresent() != null && calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)(*((ref <Module>.?sm_ClientAPI@XE_API@@2UXEEngineClientAPI@@A) + 40))) != null)
			{
				XEActivityId xeactivityId;
				IntPtr intPtr = (IntPtr)((void*)(&xeactivityId));
				Marshal.StructureToPtr<ActivityId>(transferId, intPtr, false);
				int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(XEActivityId modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), ref xeactivityId, (IntPtr)(*((ref <Module>.?sm_ClientAPI@XE_API@@2UXEEngineClientAPI@@A) + 368)));
			}
		}
	}
}
