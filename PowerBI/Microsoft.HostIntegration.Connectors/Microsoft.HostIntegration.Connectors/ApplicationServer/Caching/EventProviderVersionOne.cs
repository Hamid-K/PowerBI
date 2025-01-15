using System;
using System.Diagnostics.Eventing;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001CC RID: 460
	internal class EventProviderVersionOne : IDisposable
	{
		// Token: 0x06000F0F RID: 3855 RVA: 0x00033472 File Offset: 0x00031672
		internal EventProviderVersionOne(Guid providerId)
		{
			this.m_providerId = providerId;
			this.Register();
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00033488 File Offset: 0x00031688
		private unsafe uint Register()
		{
			this.m_etwCallback = new EtwNativeMethods.EtwEnableCallback(this.EtwEnableCallBack);
			Guid guid = new Guid("{b4955bf0-3af1-4740-b475-99055d3fe9aa}");
			EtwNativeMethods.TRACE_GUID_REGISTRATION trace_GUID_REGISTRATION;
			trace_GUID_REGISTRATION.Id = (IntPtr)((void*)(&guid));
			trace_GUID_REGISTRATION.RegHandle = IntPtr.Zero;
			return EtwNativeMethods.RegisterTraceGuids(this.m_etwCallback, null, ref this.m_providerId, 1U, ref trace_GUID_REGISTRATION, null, null, out this.m_regHandle);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x000334F4 File Offset: 0x000316F4
		internal unsafe uint EtwEnableCallBack(EtwNativeMethods.WMIDPREQUESTCODE requestCode, IntPtr context, uint* bufferSize, byte* byteBuffer)
		{
			switch (requestCode)
			{
			case EtwNativeMethods.WMIDPREQUESTCODE.WMI_ENABLE_EVENTS:
				this.m_logger = EtwNativeMethods.GetTraceLoggerHandle(byteBuffer);
				this.m_flags = (long)((ulong)EtwNativeMethods.GetTraceEnableFlags(this.m_logger));
				this.m_level = EtwNativeMethods.GetTraceEnableLevel(this.m_logger);
				this.m_enabled = 1;
				break;
			case EtwNativeMethods.WMIDPREQUESTCODE.WMI_DISABLE_EVENTS:
				this.m_enabled = 0;
				this.m_logger = 0UL;
				this.m_level = 0;
				this.m_flags = 0L;
				break;
			default:
				this.m_enabled = 0;
				this.m_logger = 0UL;
				break;
			}
			return 0U;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0003357E File Offset: 0x0003177E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0003358D File Offset: 0x0003178D
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_disposed == 1)
			{
				return;
			}
			if (Interlocked.Exchange(ref this.m_disposed, 1) != 0)
			{
				return;
			}
			this.m_enabled = 0;
			this.Deregister();
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x000335B5 File Offset: 0x000317B5
		public virtual void Close()
		{
			this.Dispose();
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x000335C0 File Offset: 0x000317C0
		~EventProviderVersionOne()
		{
			this.Dispose(false);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000335F0 File Offset: 0x000317F0
		private void Deregister()
		{
			if (this.m_regHandle != 0UL)
			{
				EtwNativeMethods.UnregisterTraceGuids(this.m_regHandle);
				this.m_regHandle = 0UL;
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00033610 File Offset: 0x00031810
		public bool IsEnabled(byte level, uint flags)
		{
			return this.m_enabled != 0 && ((level <= this.m_level || this.m_level == 0) && (flags == 0U || ((ulong)flags & (ulong)this.m_flags) != 0UL));
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00033640 File Offset: 0x00031840
		public bool IsEnabled()
		{
			return this.m_enabled != 0;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00033650 File Offset: 0x00031850
		internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, ref Guid eventGuid, string data)
		{
			if (this.IsEnabled(eventDescriptor.Level, (uint)eventDescriptor.Keywords))
			{
				int num = sizeof(EtwNativeMethods.EVENT_TRACE_HEADER) + sizeof(EventProviderVersionOne.EventData);
				byte* ptr = stackalloc byte[(UIntPtr)num];
				EventProviderVersionOne.EventData* ptr2 = (EventProviderVersionOne.EventData*)(ptr + sizeof(EtwNativeMethods.EVENT_TRACE_HEADER));
				EtwNativeMethods.EVENT_TRACE_HEADER* ptr3 = (EtwNativeMethods.EVENT_TRACE_HEADER*)ptr;
				ptr3->Guid = eventGuid;
				ptr3->Flags = 1310720U;
				ptr3->Version = (short)eventDescriptor.Version;
				ptr3->Level = eventDescriptor.Level;
				ptr3->Type = eventDescriptor.Opcode;
				ptr3->Size = (short)num;
				ptr2->Size = (uint)((data.Length + 1) * 2);
				fixed (char* ptr4 = data)
				{
					ptr2->DataPointer = ptr4;
					EtwNativeMethods.TraceEvent(this.m_logger, ptr);
				}
			}
			return true;
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00033714 File Offset: 0x00031914
		internal unsafe bool TemplateStringTemplate(ref EventDescriptor eventDescriptor, ref Guid eventGuid, string Source, string Param)
		{
			int num = 2;
			if (this.IsEnabled(eventDescriptor.Level, (uint)eventDescriptor.Keywords))
			{
				int num2 = sizeof(EtwNativeMethods.EVENT_TRACE_HEADER) + sizeof(EventProviderVersionOne.EventData) * num;
				byte* ptr = stackalloc byte[(UIntPtr)num2];
				EventProviderVersionOne.EventData* ptr2 = (EventProviderVersionOne.EventData*)(ptr + sizeof(EtwNativeMethods.EVENT_TRACE_HEADER));
				EtwNativeMethods.EVENT_TRACE_HEADER* ptr3 = (EtwNativeMethods.EVENT_TRACE_HEADER*)ptr;
				ptr3->Guid = eventGuid;
				ptr3->Flags = 1310720U;
				ptr3->Version = (short)eventDescriptor.Version;
				ptr3->Level = eventDescriptor.Level;
				ptr3->Type = eventDescriptor.Opcode;
				ptr3->Size = (short)num2;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].Size = (uint)((Param.Length + 1) * 2);
				fixed (char* ptr4 = Source, ptr5 = Param)
				{
					ptr2->DataPointer = ptr4;
					ptr2[1].DataPointer = ptr5;
					EtwNativeMethods.TraceEvent(this.m_logger, ptr);
				}
			}
			return true;
		}

		// Token: 0x04000A57 RID: 2647
		internal const uint s_eventFlags = 1310720U;

		// Token: 0x04000A58 RID: 2648
		private EtwNativeMethods.EtwEnableCallback m_etwCallback;

		// Token: 0x04000A59 RID: 2649
		private ulong m_regHandle;

		// Token: 0x04000A5A RID: 2650
		private byte m_level;

		// Token: 0x04000A5B RID: 2651
		private long m_flags;

		// Token: 0x04000A5C RID: 2652
		private int m_enabled;

		// Token: 0x04000A5D RID: 2653
		private Guid m_providerId;

		// Token: 0x04000A5E RID: 2654
		private ulong m_logger;

		// Token: 0x04000A5F RID: 2655
		private int m_disposed;

		// Token: 0x020001CD RID: 461
		[StructLayout(LayoutKind.Explicit, Size = 16)]
		private struct EventData
		{
			// Token: 0x04000A60 RID: 2656
			[FieldOffset(0)]
			internal ulong DataPointer;

			// Token: 0x04000A61 RID: 2657
			[FieldOffset(8)]
			internal uint Size;

			// Token: 0x04000A62 RID: 2658
			[FieldOffset(12)]
			internal int Reserved;
		}
	}
}
