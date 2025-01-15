using System;
using System.Diagnostics.Eventing;
using System.Runtime.InteropServices;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001CA RID: 458
	internal class EventProviderVersionTwo : EventProvider
	{
		// Token: 0x06000F0D RID: 3853 RVA: 0x000333B8 File Offset: 0x000315B8
		internal EventProviderVersionTwo(Guid id)
			: base(id)
		{
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x000333C4 File Offset: 0x000315C4
		internal unsafe bool TemplateStringTemplate(ref EventDescriptor eventDescriptor, string Source, string Param)
		{
			int num = 2;
			bool flag = true;
			if (base.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				byte* ptr = stackalloc byte[(UIntPtr)(sizeof(EventProviderVersionTwo.EventData) * num)];
				EventProviderVersionTwo.EventData* ptr2 = (EventProviderVersionTwo.EventData*)ptr;
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

		// Token: 0x020001CB RID: 459
		[StructLayout(LayoutKind.Explicit, Size = 16)]
		private struct EventData
		{
			// Token: 0x04000A54 RID: 2644
			[FieldOffset(0)]
			internal ulong DataPointer;

			// Token: 0x04000A55 RID: 2645
			[FieldOffset(8)]
			internal uint Size;

			// Token: 0x04000A56 RID: 2646
			[FieldOffset(12)]
			internal int Reserved;
		}
	}
}
