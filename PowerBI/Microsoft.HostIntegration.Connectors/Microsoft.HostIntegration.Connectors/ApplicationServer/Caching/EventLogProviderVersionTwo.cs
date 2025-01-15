using System;
using System.Diagnostics.Eventing;
using System.Runtime.InteropServices;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200035D RID: 861
	internal class EventLogProviderVersionTwo : EventProvider
	{
		// Token: 0x06001E5E RID: 7774 RVA: 0x000333B8 File Offset: 0x000315B8
		internal EventLogProviderVersionTwo(Guid id)
			: base(id)
		{
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0005B90C File Offset: 0x00059B0C
		internal unsafe bool TemplateStringTemplate(ref EventDescriptor eventDescriptor, string Source, string Param)
		{
			int num = 2;
			bool flag = true;
			if (base.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				byte* ptr = stackalloc byte[(UIntPtr)(sizeof(EventLogProviderVersionTwo.EventData) * num)];
				EventLogProviderVersionTwo.EventData* ptr2 = (EventLogProviderVersionTwo.EventData*)ptr;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].Size = (uint)((Param.Length + 1) * 2);
				fixed (char* ptr3 = Source, ptr4 = Param)
				{
					ptr2->DataPointer = ptr3;
					ptr2[1].DataPointer = ptr4;
					flag = base.WriteEvent(ref eventDescriptor, num, (IntPtr)((void*)ptr));
				}
			}
			return flag;
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0005B9BC File Offset: 0x00059BBC
		internal unsafe bool TemplateStringStringTemplate(ref EventDescriptor eventDescriptor, string Source, string Param1, string Param2)
		{
			int num = 3;
			bool flag = true;
			if (base.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				byte* ptr = stackalloc byte[(UIntPtr)(sizeof(EventLogProviderVersionTwo.EventData) * num)];
				EventLogProviderVersionTwo.EventData* ptr2 = (EventLogProviderVersionTwo.EventData*)ptr;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].Size = (uint)((Param1.Length + 1) * 2);
				ptr2[2].Size = (uint)((Param2.Length + 1) * 2);
				fixed (char* ptr3 = Source, ptr4 = Param1, ptr5 = Param2)
				{
					ptr2->DataPointer = ptr3;
					ptr2[1].DataPointer = ptr4;
					ptr2[2].DataPointer = ptr5;
					flag = base.WriteEvent(ref eventDescriptor, num, (IntPtr)((void*)ptr));
				}
			}
			return flag;
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0005BAB0 File Offset: 0x00059CB0
		internal unsafe bool TemplateIntIntIntIntIntIntTemplate(ref EventDescriptor eventDescriptor, string Source, int Param1, int Param2, int Param3, int Param4, int Param5, int Param6)
		{
			int num = 7;
			bool flag = true;
			if (base.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				byte* ptr = stackalloc byte[(UIntPtr)(sizeof(EventLogProviderVersionTwo.EventData) * num)];
				EventLogProviderVersionTwo.EventData* ptr2 = (EventLogProviderVersionTwo.EventData*)ptr;
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
				fixed (char* ptr3 = Source)
				{
					ptr2->DataPointer = ptr3;
					flag = base.WriteEvent(ref eventDescriptor, num, (IntPtr)((void*)ptr));
				}
			}
			return flag;
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x0005BBFC File Offset: 0x00059DFC
		internal unsafe bool TemplateString8Int5LongTemplate(ref EventDescriptor eventDescriptor, string Source, string Param1, int Param2, int Param3, int Param4, int Param5, int Param6, int Param7, int Param8, int Param9, long Param10, long Param11, long Param12, long Param13, long Param14)
		{
			int num = 15;
			bool flag = true;
			if (base.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				byte* ptr = stackalloc byte[(UIntPtr)(sizeof(EventLogProviderVersionTwo.EventData) * num)];
				EventLogProviderVersionTwo.EventData* ptr2 = (EventLogProviderVersionTwo.EventData*)ptr;
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
				fixed (char* ptr3 = Source, ptr4 = Param1)
				{
					ptr2->DataPointer = ptr3;
					ptr2[1].DataPointer = ptr4;
					flag = base.WriteEvent(ref eventDescriptor, num, (IntPtr)((void*)ptr));
				}
			}
			return flag;
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x0005BE98 File Offset: 0x0005A098
		internal unsafe bool Template8Int6LongTemplate(ref EventDescriptor eventDescriptor, string Source, int Param1, int Param2, int Param3, int Param4, int Param5, int Param6, int Param7, int Param8, long Param9, long Param10, long Param11, long Param12, long Param13, long Param14)
		{
			int num = 15;
			bool flag = true;
			if (base.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				byte* ptr = stackalloc byte[(UIntPtr)(sizeof(EventLogProviderVersionTwo.EventData) * num)];
				EventLogProviderVersionTwo.EventData* ptr2 = (EventLogProviderVersionTwo.EventData*)ptr;
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
				fixed (char* ptr3 = Source)
				{
					ptr2->DataPointer = ptr3;
					flag = base.WriteEvent(ref eventDescriptor, num, (IntPtr)((void*)ptr));
				}
			}
			return flag;
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x0005C118 File Offset: 0x0005A318
		internal unsafe bool TemplateStringLongLongLongTemplate(ref EventDescriptor eventDescriptor, string Source, string Param1, long Param2, long Param3, long Param4)
		{
			int num = 5;
			bool flag = true;
			if (base.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				byte* ptr = stackalloc byte[(UIntPtr)(sizeof(EventLogProviderVersionTwo.EventData) * num)];
				EventLogProviderVersionTwo.EventData* ptr2 = (EventLogProviderVersionTwo.EventData*)ptr;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].Size = (uint)((Param1.Length + 1) * 2);
				ptr2[2].DataPointer = &Param2;
				ptr2[2].Size = 8U;
				ptr2[3].DataPointer = &Param3;
				ptr2[3].Size = 8U;
				ptr2[4].DataPointer = &Param4;
				ptr2[4].Size = 8U;
				fixed (char* ptr3 = Source, ptr4 = Param1)
				{
					ptr2->DataPointer = ptr3;
					ptr2[1].DataPointer = ptr4;
					flag = base.WriteEvent(ref eventDescriptor, num, (IntPtr)((void*)ptr));
				}
			}
			return flag;
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0005C238 File Offset: 0x0005A438
		internal unsafe bool TemplateLongTemplate(ref EventDescriptor eventDescriptor, string Source, long Param)
		{
			int num = 2;
			bool flag = true;
			if (base.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				byte* ptr = stackalloc byte[(UIntPtr)(sizeof(EventLogProviderVersionTwo.EventData) * num)];
				EventLogProviderVersionTwo.EventData* ptr2 = (EventLogProviderVersionTwo.EventData*)ptr;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].DataPointer = &Param;
				ptr2[1].Size = 8U;
				fixed (char* ptr3 = Source)
				{
					ptr2->DataPointer = ptr3;
					flag = base.WriteEvent(ref eventDescriptor, num, (IntPtr)((void*)ptr));
				}
			}
			return flag;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0005C2C8 File Offset: 0x0005A4C8
		internal unsafe bool TemplateLongLongTemplate(ref EventDescriptor eventDescriptor, string Source, long Param1, long Param2)
		{
			int num = 3;
			bool flag = true;
			if (base.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				byte* ptr = stackalloc byte[(UIntPtr)(sizeof(EventLogProviderVersionTwo.EventData) * num)];
				EventLogProviderVersionTwo.EventData* ptr2 = (EventLogProviderVersionTwo.EventData*)ptr;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].DataPointer = &Param1;
				ptr2[1].Size = 8U;
				ptr2[2].DataPointer = &Param2;
				ptr2[2].Size = 8U;
				fixed (char* ptr3 = Source)
				{
					ptr2->DataPointer = ptr3;
					flag = base.WriteEvent(ref eventDescriptor, num, (IntPtr)((void*)ptr));
				}
			}
			return flag;
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0005C380 File Offset: 0x0005A580
		internal unsafe bool TemplateLongLongLongTemplate(ref EventDescriptor eventDescriptor, string Source, long Param2, long Param3, long Param4)
		{
			int num = 4;
			bool flag = true;
			if (base.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				byte* ptr = stackalloc byte[(UIntPtr)(sizeof(EventLogProviderVersionTwo.EventData) * num)];
				EventLogProviderVersionTwo.EventData* ptr2 = (EventLogProviderVersionTwo.EventData*)ptr;
				ptr2->Size = (uint)((Source.Length + 1) * 2);
				ptr2[1].DataPointer = &Param2;
				ptr2[1].Size = 8U;
				ptr2[2].DataPointer = &Param3;
				ptr2[2].Size = 8U;
				ptr2[3].DataPointer = &Param4;
				ptr2[3].Size = 8U;
				fixed (char* ptr3 = Source)
				{
					ptr2->DataPointer = ptr3;
					flag = base.WriteEvent(ref eventDescriptor, num, (IntPtr)((void*)ptr));
				}
			}
			return flag;
		}

		// Token: 0x0200035E RID: 862
		[StructLayout(LayoutKind.Explicit, Size = 16)]
		private struct EventData
		{
			// Token: 0x0400112A RID: 4394
			[FieldOffset(0)]
			internal ulong DataPointer;

			// Token: 0x0400112B RID: 4395
			[FieldOffset(8)]
			internal uint Size;

			// Token: 0x0400112C RID: 4396
			[FieldOffset(12)]
			internal int Reserved;
		}
	}
}
