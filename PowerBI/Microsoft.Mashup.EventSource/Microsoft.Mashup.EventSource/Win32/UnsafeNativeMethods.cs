using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Win32
{
	// Token: 0x02000005 RID: 5
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x04000004 RID: 4
		private const string EventingProviderApiSet = "advapi32.dll";

		// Token: 0x04000005 RID: 5
		private const string EventingControllerApiSet = "advapi32.dll";

		// Token: 0x0200007D RID: 125
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		internal static class ManifestEtw
		{
			// Token: 0x060002E7 RID: 743
			[SecurityCritical]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal unsafe static extern uint EventRegister([In] ref Guid providerId, [In] UnsafeNativeMethods.ManifestEtw.EtwEnableCallback enableCallback, [In] void* callbackContext, [In] [Out] ref long registrationHandle);

			// Token: 0x060002E8 RID: 744
			[SecurityCritical]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal static extern uint EventUnregister([In] long registrationHandle);

			// Token: 0x060002E9 RID: 745 RVA: 0x0000E63C File Offset: 0x0000C83C
			internal unsafe static int EventWriteTransferWrapper(long registrationHandle, ref EventDescriptor eventDescriptor, Guid* activityId, Guid* relatedActivityId, int userDataCount, EventProvider.EventData* userData)
			{
				int num = UnsafeNativeMethods.ManifestEtw.EventWriteTransfer(registrationHandle, ref eventDescriptor, activityId, relatedActivityId, userDataCount, userData);
				if (num == 87 && relatedActivityId == null)
				{
					Guid empty = Guid.Empty;
					num = UnsafeNativeMethods.ManifestEtw.EventWriteTransfer(registrationHandle, ref eventDescriptor, activityId, &empty, userDataCount, userData);
				}
				return num;
			}

			// Token: 0x060002EA RID: 746
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			private unsafe static extern int EventWriteTransfer([In] long registrationHandle, [In] ref EventDescriptor eventDescriptor, [In] Guid* activityId, [In] Guid* relatedActivityId, [In] int userDataCount, [In] EventProvider.EventData* userData);

			// Token: 0x060002EB RID: 747
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal static extern int EventActivityIdControl([In] UnsafeNativeMethods.ManifestEtw.ActivityControl ControlCode, [In] [Out] ref Guid ActivityId);

			// Token: 0x060002EC RID: 748
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal unsafe static extern int EventSetInformation([In] long registrationHandle, [In] UnsafeNativeMethods.ManifestEtw.EVENT_INFO_CLASS informationClass, [In] void* eventInformation, [In] int informationLength);

			// Token: 0x0400017E RID: 382
			internal const int ERROR_ARITHMETIC_OVERFLOW = 534;

			// Token: 0x0400017F RID: 383
			internal const int ERROR_NOT_ENOUGH_MEMORY = 8;

			// Token: 0x04000180 RID: 384
			internal const int ERROR_MORE_DATA = 234;

			// Token: 0x04000181 RID: 385
			internal const int ERROR_NOT_SUPPORTED = 50;

			// Token: 0x04000182 RID: 386
			internal const int ERROR_INVALID_PARAMETER = 87;

			// Token: 0x04000183 RID: 387
			internal const int EVENT_CONTROL_CODE_DISABLE_PROVIDER = 0;

			// Token: 0x04000184 RID: 388
			internal const int EVENT_CONTROL_CODE_ENABLE_PROVIDER = 1;

			// Token: 0x04000185 RID: 389
			internal const int EVENT_CONTROL_CODE_CAPTURE_STATE = 2;

			// Token: 0x02000093 RID: 147
			// (Invoke) Token: 0x06000325 RID: 805
			[SecurityCritical]
			internal unsafe delegate void EtwEnableCallback([In] ref Guid sourceId, [In] int isEnabled, [In] byte level, [In] long matchAnyKeywords, [In] long matchAllKeywords, [In] UnsafeNativeMethods.ManifestEtw.EVENT_FILTER_DESCRIPTOR* filterData, [In] void* callbackContext);

			// Token: 0x02000094 RID: 148
			internal struct EVENT_FILTER_DESCRIPTOR
			{
				// Token: 0x040001C2 RID: 450
				public long Ptr;

				// Token: 0x040001C3 RID: 451
				public int Size;

				// Token: 0x040001C4 RID: 452
				public int Type;
			}

			// Token: 0x02000095 RID: 149
			internal enum ActivityControl : uint
			{
				// Token: 0x040001C6 RID: 454
				EVENT_ACTIVITY_CTRL_GET_ID = 1U,
				// Token: 0x040001C7 RID: 455
				EVENT_ACTIVITY_CTRL_SET_ID,
				// Token: 0x040001C8 RID: 456
				EVENT_ACTIVITY_CTRL_CREATE_ID,
				// Token: 0x040001C9 RID: 457
				EVENT_ACTIVITY_CTRL_GET_SET_ID,
				// Token: 0x040001CA RID: 458
				EVENT_ACTIVITY_CTRL_CREATE_SET_ID
			}

			// Token: 0x02000096 RID: 150
			internal enum EVENT_INFO_CLASS
			{
				// Token: 0x040001CC RID: 460
				BinaryTrackInfo,
				// Token: 0x040001CD RID: 461
				SetEnableAllKeywords,
				// Token: 0x040001CE RID: 462
				SetTraits
			}
		}
	}
}
