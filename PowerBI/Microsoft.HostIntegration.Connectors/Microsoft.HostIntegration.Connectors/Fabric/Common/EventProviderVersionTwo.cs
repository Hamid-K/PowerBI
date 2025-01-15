using System;
using System.Diagnostics.Eventing;
using System.Runtime.InteropServices;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200043B RID: 1083
	internal class EventProviderVersionTwo : EventProvider
	{
		// Token: 0x060025B3 RID: 9651 RVA: 0x000333B8 File Offset: 0x000315B8
		internal EventProviderVersionTwo(Guid id)
			: base(id)
		{
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x00073754 File Offset: 0x00071954
		internal unsafe bool Templatessss(ref EventDescriptor eventDescriptor, string param1, string param2, string param3, string param4)
		{
			int num = 4;
			bool flag = true;
			if (base.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				byte* ptr = stackalloc byte[(UIntPtr)(sizeof(EventProviderVersionTwo.EventData) * num)];
				EventProviderVersionTwo.EventData* ptr2 = (EventProviderVersionTwo.EventData*)ptr;
				ptr2->Size = (uint)((param1.Length + 1) * 2);
				ptr2[1].Size = (uint)((param2.Length + 1) * 2);
				ptr2[2].Size = (uint)((param3.Length + 1) * 2);
				ptr2[3].Size = (uint)((param4.Length + 1) * 2);
				fixed (char* ptr3 = param1, ptr4 = param2, ptr5 = param3, ptr6 = param4)
				{
					ptr2->DataPointer = ptr3;
					ptr2[1].DataPointer = ptr4;
					ptr2[2].DataPointer = ptr5;
					ptr2[3].DataPointer = ptr6;
					flag = base.WriteEvent(ref eventDescriptor, num, (IntPtr)((void*)ptr));
				}
			}
			return flag;
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x00073888 File Offset: 0x00071A88
		internal unsafe bool Templatess(ref EventDescriptor eventDescriptor, string param1, string param2)
		{
			int num = 2;
			bool flag = true;
			if (base.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				byte* ptr = stackalloc byte[(UIntPtr)(sizeof(EventProviderVersionTwo.EventData) * num)];
				EventProviderVersionTwo.EventData* ptr2 = (EventProviderVersionTwo.EventData*)ptr;
				ptr2->Size = (uint)((param1.Length + 1) * 2);
				ptr2[1].Size = (uint)((param2.Length + 1) * 2);
				fixed (char* ptr3 = param1, ptr4 = param2)
				{
					ptr2->DataPointer = ptr3;
					ptr2[1].DataPointer = ptr4;
					flag = base.WriteEvent(ref eventDescriptor, num, (IntPtr)((void*)ptr));
				}
			}
			return flag;
		}

		// Token: 0x0200043C RID: 1084
		[StructLayout(LayoutKind.Explicit, Size = 16)]
		private struct EventData
		{
			// Token: 0x040016CA RID: 5834
			[FieldOffset(0)]
			internal ulong DataPointer;

			// Token: 0x040016CB RID: 5835
			[FieldOffset(8)]
			internal uint Size;

			// Token: 0x040016CC RID: 5836
			[FieldOffset(12)]
			internal int Reserved;
		}
	}
}
