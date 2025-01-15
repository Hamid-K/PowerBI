using System;
using System.Diagnostics.Eventing;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200035F RID: 863
	internal class EventLogProviderVersionOne : IDisposable
	{
		// Token: 0x06001E68 RID: 7784 RVA: 0x0005C45C File Offset: 0x0005A65C
		internal EventLogProviderVersionOne(Guid providerId)
		{
			this._providerId = providerId;
			this.Register();
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0005C474 File Offset: 0x0005A674
		private unsafe uint Register()
		{
			this.m_etwCallback = new EventEtwNativeMethods.EtwEnableCallback(this.EtwEnableCallBack);
			Guid guid = new Guid("{b4955bf0-3af1-4740-b475-99055d3fe9aa}");
			EventEtwNativeMethods.TRACE_GUID_REGISTRATION trace_GUID_REGISTRATION;
			trace_GUID_REGISTRATION.Id = (IntPtr)((void*)(&guid));
			trace_GUID_REGISTRATION.RegHandle = IntPtr.Zero;
			return EventEtwNativeMethods.RegisterTraceGuids(this.m_etwCallback, null, ref this._providerId, 1U, ref trace_GUID_REGISTRATION, null, null, out this.m_regHandle);
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0005C4E0 File Offset: 0x0005A6E0
		internal unsafe uint EtwEnableCallBack(EventEtwNativeMethods.WMIDPREQUESTCODE requestCode, IntPtr context, uint* bufferSize, byte* byteBuffer)
		{
			switch (requestCode)
			{
			case EventEtwNativeMethods.WMIDPREQUESTCODE.WMI_ENABLE_EVENTS:
				this.m_logger = EventEtwNativeMethods.GetTraceLoggerHandle(byteBuffer);
				this.m_flags = (long)((ulong)EventEtwNativeMethods.GetTraceEnableFlags(this.m_logger));
				this.m_level = EventEtwNativeMethods.GetTraceEnableLevel(this.m_logger);
				this.m_enabled = 1;
				break;
			case EventEtwNativeMethods.WMIDPREQUESTCODE.WMI_DISABLE_EVENTS:
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

		// Token: 0x06001E6B RID: 7787 RVA: 0x0005C56A File Offset: 0x0005A76A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0005C579 File Offset: 0x0005A779
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

		// Token: 0x06001E6D RID: 7789 RVA: 0x0005C5A1 File Offset: 0x0005A7A1
		public virtual void Close()
		{
			this.Dispose();
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0005C5AC File Offset: 0x0005A7AC
		~EventLogProviderVersionOne()
		{
			this.Dispose(false);
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0005C5DC File Offset: 0x0005A7DC
		private void Deregister()
		{
			if (this.m_regHandle != 0UL)
			{
				EventEtwNativeMethods.UnregisterTraceGuids(this.m_regHandle);
				this.m_regHandle = 0UL;
			}
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x0005C5FC File Offset: 0x0005A7FC
		public bool IsEnabled(byte level, uint flags)
		{
			return this.m_enabled != 0 && ((level <= this.m_level || this.m_level == 0) && (flags == 0U || ((ulong)flags & (ulong)this.m_flags) != 0UL));
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0005C62C File Offset: 0x0005A82C
		public bool IsEnabled()
		{
			return this.m_enabled != 0;
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x0005C63C File Offset: 0x0005A83C
		internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, ref Guid eventGuid, string data)
		{
			if (this.IsEnabled(eventDescriptor.Level, (uint)eventDescriptor.Keywords))
			{
				int num = sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER) + sizeof(EventLogProviderVersionOne.EventData);
				byte* ptr = stackalloc byte[(UIntPtr)num];
				EventLogProviderVersionOne.EventData* ptr2 = (EventLogProviderVersionOne.EventData*)(ptr + sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER));
				EventEtwNativeMethods.EVENT_TRACE_HEADER* ptr3 = (EventEtwNativeMethods.EVENT_TRACE_HEADER*)ptr;
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
					EventEtwNativeMethods.TraceEvent(this.m_logger, ptr);
				}
			}
			return true;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0005C700 File Offset: 0x0005A900
		internal unsafe bool TemplateStringTemplate(ref EventDescriptor eventDescriptor, ref Guid eventGuid, string Source, string Param)
		{
			int num = 2;
			if (this.IsEnabled(eventDescriptor.Level, (uint)eventDescriptor.Keywords))
			{
				int num2 = sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER) + sizeof(EventLogProviderVersionOne.EventData) * num;
				byte* ptr = stackalloc byte[(UIntPtr)num2];
				EventLogProviderVersionOne.EventData* ptr2 = (EventLogProviderVersionOne.EventData*)(ptr + sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER));
				EventEtwNativeMethods.EVENT_TRACE_HEADER* ptr3 = (EventEtwNativeMethods.EVENT_TRACE_HEADER*)ptr;
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
					EventEtwNativeMethods.TraceEvent(this.m_logger, ptr);
				}
			}
			return true;
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x0005C80C File Offset: 0x0005AA0C
		internal unsafe bool TemplateStringStringTemplate(ref EventDescriptor eventDescriptor, ref Guid eventGuid, string Source, string Param1, string Param2)
		{
			int num = 3;
			if (this.IsEnabled(eventDescriptor.Level, (uint)eventDescriptor.Keywords))
			{
				int num2 = sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER) + sizeof(EventLogProviderVersionOne.EventData) * num;
				byte* ptr = stackalloc byte[(UIntPtr)num2];
				EventLogProviderVersionOne.EventData* ptr2 = (EventLogProviderVersionOne.EventData*)(ptr + sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER));
				EventEtwNativeMethods.EVENT_TRACE_HEADER* ptr3 = (EventEtwNativeMethods.EVENT_TRACE_HEADER*)ptr;
				ptr3->Guid = eventGuid;
				ptr3->Flags = 1310720U;
				ptr3->Version = (short)eventDescriptor.Version;
				ptr3->Level = eventDescriptor.Level;
				ptr3->Type = eventDescriptor.Opcode;
				ptr3->Size = (short)num2;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].Size = (uint)((Param1.Length + 1) * 2);
				ptr2[2].Size = (uint)((Param2.Length + 1) * 2);
				fixed (char* ptr4 = Source, ptr5 = Param1, ptr6 = Param2)
				{
					ptr2->DataPointer = ptr4;
					ptr2[1].DataPointer = ptr5;
					ptr2[1].DataPointer = ptr6;
					EventEtwNativeMethods.TraceEvent(this.m_logger, ptr);
				}
			}
			return true;
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x0005C958 File Offset: 0x0005AB58
		internal unsafe bool TemplateIntIntIntIntIntIntTemplate(ref EventDescriptor eventDescriptor, ref Guid eventGuid, string Source, int Param1, int Param2, int Param3, int Param4, int Param5, int Param6)
		{
			int num = 7;
			if (this.IsEnabled(eventDescriptor.Level, (uint)eventDescriptor.Keywords))
			{
				int num2 = sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER) + sizeof(EventLogProviderVersionOne.EventData) * num;
				byte* ptr = stackalloc byte[(UIntPtr)num2];
				EventLogProviderVersionOne.EventData* ptr2 = (EventLogProviderVersionOne.EventData*)(ptr + sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER));
				EventEtwNativeMethods.EVENT_TRACE_HEADER* ptr3 = (EventEtwNativeMethods.EVENT_TRACE_HEADER*)ptr;
				ptr3->Guid = eventGuid;
				ptr3->Flags = 1310720U;
				ptr3->Version = (short)eventDescriptor.Version;
				ptr3->Level = eventDescriptor.Level;
				ptr3->Type = eventDescriptor.Opcode;
				ptr3->Size = (short)num2;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].DataPointer = &Param1;
				ptr2[1].Size = 4U;
				ptr2[2].DataPointer = &Param2;
				ptr2[2].Size = 4U;
				ptr2[3].DataPointer = &Param3;
				ptr2[3].Size = 4U;
				ptr2[4].DataPointer = &Param4;
				ptr2[4].Size = 4U;
				ptr2[5].DataPointer = &Param5;
				ptr2[5].Size = 4U;
				ptr2[6].DataPointer = &Param6;
				ptr2[6].Size = 4U;
				fixed (char* ptr4 = Source)
				{
					ptr2->DataPointer = ptr4;
					EventEtwNativeMethods.TraceEvent(this.m_logger, ptr);
				}
			}
			return true;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0005CAFC File Offset: 0x0005ACFC
		internal unsafe bool TemplateString8Int5LongTemplate(ref EventDescriptor eventDescriptor, ref Guid eventGuid, string Source, string Param1, int Param2, int Param3, int Param4, int Param5, int Param6, int Param7, int Param8, int Param9, long Param10, long Param11, long Param12, long Param13, long Param14)
		{
			int num = 15;
			if (this.IsEnabled(eventDescriptor.Level, (uint)eventDescriptor.Keywords))
			{
				int num2 = sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER) + sizeof(EventLogProviderVersionOne.EventData) * num;
				byte* ptr = stackalloc byte[(UIntPtr)num2];
				EventLogProviderVersionOne.EventData* ptr2 = (EventLogProviderVersionOne.EventData*)(ptr + sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER));
				EventEtwNativeMethods.EVENT_TRACE_HEADER* ptr3 = (EventEtwNativeMethods.EVENT_TRACE_HEADER*)ptr;
				ptr3->Guid = eventGuid;
				ptr3->Flags = 1310720U;
				ptr3->Version = (short)eventDescriptor.Version;
				ptr3->Level = eventDescriptor.Level;
				ptr3->Type = eventDescriptor.Opcode;
				ptr3->Size = (short)num2;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].Size = (uint)((Param1.Length + 1) * 2);
				ptr2[2].DataPointer = &Param2;
				ptr2[2].Size = 4U;
				ptr2[3].DataPointer = &Param3;
				ptr2[3].Size = 4U;
				ptr2[4].DataPointer = &Param4;
				ptr2[4].Size = 4U;
				ptr2[5].DataPointer = &Param5;
				ptr2[5].Size = 4U;
				ptr2[6].DataPointer = &Param6;
				ptr2[6].Size = 4U;
				ptr2[7].DataPointer = &Param7;
				ptr2[7].Size = 4U;
				ptr2[8].DataPointer = &Param8;
				ptr2[8].Size = 4U;
				ptr2[9].DataPointer = &Param9;
				ptr2[9].Size = 4U;
				ptr2[10].DataPointer = &Param10;
				ptr2[10].Size = 8U;
				ptr2[11].DataPointer = &Param11;
				ptr2[11].Size = 8U;
				ptr2[12].DataPointer = &Param12;
				ptr2[12].Size = 8U;
				ptr2[13].DataPointer = &Param13;
				ptr2[13].Size = 8U;
				ptr2[14].DataPointer = &Param14;
				ptr2[14].Size = 8U;
				fixed (char* ptr4 = Source, ptr5 = Param1)
				{
					ptr2->DataPointer = ptr4;
					ptr2[1].DataPointer = ptr5;
					EventEtwNativeMethods.TraceEvent(this.m_logger, ptr);
				}
			}
			return true;
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0005CDF4 File Offset: 0x0005AFF4
		internal unsafe bool Template8Int6LongTemplate(ref EventDescriptor eventDescriptor, ref Guid eventGuid, string Source, int Param1, int Param2, int Param3, int Param4, int Param5, int Param6, int Param7, int Param8, long Param9, long Param10, long Param11, long Param12, long Param13, long Param14)
		{
			int num = 15;
			if (this.IsEnabled(eventDescriptor.Level, (uint)eventDescriptor.Keywords))
			{
				int num2 = sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER) + sizeof(EventLogProviderVersionOne.EventData) * num;
				byte* ptr = stackalloc byte[(UIntPtr)num2];
				EventLogProviderVersionOne.EventData* ptr2 = (EventLogProviderVersionOne.EventData*)(ptr + sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER));
				EventEtwNativeMethods.EVENT_TRACE_HEADER* ptr3 = (EventEtwNativeMethods.EVENT_TRACE_HEADER*)ptr;
				ptr3->Guid = eventGuid;
				ptr3->Flags = 1310720U;
				ptr3->Version = (short)eventDescriptor.Version;
				ptr3->Level = eventDescriptor.Level;
				ptr3->Type = eventDescriptor.Opcode;
				ptr3->Size = (short)num2;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].DataPointer = &Param1;
				ptr2[1].Size = 4U;
				ptr2[2].DataPointer = &Param2;
				ptr2[2].Size = 4U;
				ptr2[3].DataPointer = &Param3;
				ptr2[3].Size = 4U;
				ptr2[4].DataPointer = &Param4;
				ptr2[4].Size = 4U;
				ptr2[5].DataPointer = &Param5;
				ptr2[5].Size = 4U;
				ptr2[6].DataPointer = &Param6;
				ptr2[6].Size = 4U;
				ptr2[7].DataPointer = &Param7;
				ptr2[7].Size = 4U;
				ptr2[8].DataPointer = &Param8;
				ptr2[8].Size = 4U;
				ptr2[9].DataPointer = &Param9;
				ptr2[9].Size = 8U;
				ptr2[10].DataPointer = &Param10;
				ptr2[10].Size = 8U;
				ptr2[11].DataPointer = &Param11;
				ptr2[11].Size = 8U;
				ptr2[12].DataPointer = &Param12;
				ptr2[12].Size = 8U;
				ptr2[13].DataPointer = &Param13;
				ptr2[13].Size = 8U;
				ptr2[14].DataPointer = &Param14;
				ptr2[14].Size = 8U;
				fixed (char* ptr4 = Source)
				{
					ptr2->DataPointer = ptr4;
					EventEtwNativeMethods.TraceEvent(this.m_logger, ptr);
				}
			}
			return true;
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0005D0D0 File Offset: 0x0005B2D0
		internal unsafe bool TemplateStringLongLongLongTemplate(ref EventDescriptor eventDescriptor, ref Guid eventGuid, string Source, string Param1, long Param2, long Param3, long Param4)
		{
			int num = 5;
			if (this.IsEnabled(eventDescriptor.Level, (uint)eventDescriptor.Keywords))
			{
				int num2 = sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER) + sizeof(EventLogProviderVersionOne.EventData) * num;
				byte* ptr = stackalloc byte[(UIntPtr)num2];
				EventLogProviderVersionOne.EventData* ptr2 = (EventLogProviderVersionOne.EventData*)(ptr + sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER));
				EventEtwNativeMethods.EVENT_TRACE_HEADER* ptr3 = (EventEtwNativeMethods.EVENT_TRACE_HEADER*)ptr;
				ptr3->Guid = eventGuid;
				ptr3->Flags = 1310720U;
				ptr3->Version = (short)eventDescriptor.Version;
				ptr3->Level = eventDescriptor.Level;
				ptr3->Type = eventDescriptor.Opcode;
				ptr3->Size = (short)num2;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].Size = (uint)((Param1.Length + 1) * 2);
				ptr2[2].DataPointer = &Param2;
				ptr2[2].Size = 8U;
				ptr2[3].DataPointer = &Param3;
				ptr2[3].Size = 8U;
				ptr2[4].DataPointer = &Param4;
				ptr2[4].Size = 8U;
				fixed (char* ptr4 = Source, ptr5 = Param1)
				{
					ptr2->DataPointer = ptr4;
					ptr2[1].DataPointer = ptr5;
					EventEtwNativeMethods.TraceEvent(this.m_logger, ptr);
				}
			}
			return true;
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x0005D248 File Offset: 0x0005B448
		internal unsafe bool TemplateLongTemplate(ref EventDescriptor eventDescriptor, ref Guid eventGuid, string Source, long Param)
		{
			int num = 2;
			if (this.IsEnabled(eventDescriptor.Level, (uint)eventDescriptor.Keywords))
			{
				int num2 = sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER) + sizeof(EventLogProviderVersionOne.EventData) * num;
				byte* ptr = stackalloc byte[(UIntPtr)num2];
				EventLogProviderVersionOne.EventData* ptr2 = (EventLogProviderVersionOne.EventData*)(ptr + sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER));
				EventEtwNativeMethods.EVENT_TRACE_HEADER* ptr3 = (EventEtwNativeMethods.EVENT_TRACE_HEADER*)ptr;
				ptr3->Guid = eventGuid;
				ptr3->Flags = 1310720U;
				ptr3->Version = (short)eventDescriptor.Version;
				ptr3->Level = eventDescriptor.Level;
				ptr3->Type = eventDescriptor.Opcode;
				ptr3->Size = (short)num2;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].DataPointer = &Param;
				ptr2[1].Size = 8U;
				fixed (char* ptr4 = Source)
				{
					ptr2->DataPointer = ptr4;
					EventEtwNativeMethods.TraceEvent(this.m_logger, ptr);
				}
			}
			return true;
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0005D334 File Offset: 0x0005B534
		internal unsafe bool TemplateLongLongTemplate(ref EventDescriptor eventDescriptor, ref Guid eventGuid, string Source, long Param1, long Param2)
		{
			int num = 3;
			if (this.IsEnabled(eventDescriptor.Level, (uint)eventDescriptor.Keywords))
			{
				int num2 = sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER) + sizeof(EventLogProviderVersionOne.EventData) * num;
				byte* ptr = stackalloc byte[(UIntPtr)num2];
				EventLogProviderVersionOne.EventData* ptr2 = (EventLogProviderVersionOne.EventData*)(ptr + sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER));
				EventEtwNativeMethods.EVENT_TRACE_HEADER* ptr3 = (EventEtwNativeMethods.EVENT_TRACE_HEADER*)ptr;
				ptr3->Guid = eventGuid;
				ptr3->Flags = 1310720U;
				ptr3->Version = (short)eventDescriptor.Version;
				ptr3->Level = eventDescriptor.Level;
				ptr3->Type = eventDescriptor.Opcode;
				ptr3->Size = (short)num2;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].DataPointer = &Param1;
				ptr2[1].Size = 8U;
				ptr2[2].DataPointer = &Param2;
				ptr2[2].Size = 8U;
				fixed (char* ptr4 = Source)
				{
					ptr2->DataPointer = ptr4;
					EventEtwNativeMethods.TraceEvent(this.m_logger, ptr);
				}
			}
			return true;
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x0005D444 File Offset: 0x0005B644
		internal unsafe bool TemplateLongLongLongTemplate(ref EventDescriptor eventDescriptor, ref Guid eventGuid, string Source, long Param2, long Param3, long Param4)
		{
			int num = 4;
			if (this.IsEnabled(eventDescriptor.Level, (uint)eventDescriptor.Keywords))
			{
				int num2 = sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER) + sizeof(EventLogProviderVersionOne.EventData) * num;
				byte* ptr = stackalloc byte[(UIntPtr)num2];
				EventLogProviderVersionOne.EventData* ptr2 = (EventLogProviderVersionOne.EventData*)(ptr + sizeof(EventEtwNativeMethods.EVENT_TRACE_HEADER));
				EventEtwNativeMethods.EVENT_TRACE_HEADER* ptr3 = (EventEtwNativeMethods.EVENT_TRACE_HEADER*)ptr;
				ptr3->Guid = eventGuid;
				ptr3->Flags = 1310720U;
				ptr3->Version = (short)eventDescriptor.Version;
				ptr3->Level = eventDescriptor.Level;
				ptr3->Type = eventDescriptor.Opcode;
				ptr3->Size = (short)num2;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].DataPointer = &Param2;
				ptr2[1].Size = 8U;
				ptr2[2].DataPointer = &Param3;
				ptr2[2].Size = 8U;
				ptr2[3].DataPointer = &Param4;
				ptr2[3].Size = 8U;
				fixed (char* ptr4 = Source)
				{
					ptr2->DataPointer = ptr4;
					EventEtwNativeMethods.TraceEvent(this.m_logger, ptr);
				}
			}
			return true;
		}

		// Token: 0x0400112D RID: 4397
		internal const uint s_eventFlags = 1310720U;

		// Token: 0x0400112E RID: 4398
		private EventEtwNativeMethods.EtwEnableCallback m_etwCallback;

		// Token: 0x0400112F RID: 4399
		private ulong m_regHandle;

		// Token: 0x04001130 RID: 4400
		private byte m_level;

		// Token: 0x04001131 RID: 4401
		private long m_flags;

		// Token: 0x04001132 RID: 4402
		private int m_enabled;

		// Token: 0x04001133 RID: 4403
		private Guid _providerId;

		// Token: 0x04001134 RID: 4404
		private ulong m_logger;

		// Token: 0x04001135 RID: 4405
		private int m_disposed;

		// Token: 0x02000360 RID: 864
		[StructLayout(LayoutKind.Explicit, Size = 16)]
		private struct EventData
		{
			// Token: 0x04001136 RID: 4406
			[FieldOffset(0)]
			internal ulong DataPointer;

			// Token: 0x04001137 RID: 4407
			[FieldOffset(8)]
			internal uint Size;

			// Token: 0x04001138 RID: 4408
			[FieldOffset(12)]
			internal int Reserved;
		}
	}
}
