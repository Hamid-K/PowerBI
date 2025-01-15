using System;
using System.Runtime.CompilerServices;

namespace Microsoft.SqlServer.XEvent.Internal
{
	// Token: 0x02000054 RID: 84
	internal static class GenericInteropEvent
	{
		// Token: 0x060001BD RID: 445 RVA: 0x00004020 File Offset: 0x00004020
		public unsafe static GeneratedEvent* ConstructEvent(byte* where, int whereSize, ushort nVlds, void* data, uint length, void* sessionContext)
		{
			GeneratedEvent* ptr2;
			if ((long)whereSize >= 616L)
			{
				GeneratedEvent* ptr = <Module>.@new(616UL, (void*)where);
				<Module>.GenericEvent.{ctor}(ptr, nVlds, data, length);
				*(long*)(ptr + 608L / (long)sizeof(GeneratedEvent)) = sessionContext;
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
			return ptr2;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00004068 File Offset: 0x00004068
		public unsafe static GeneratedEvent* ConstructEmptyEvent(byte* where, int whereSize, void* sessionContext)
		{
			GeneratedEvent* ptr2;
			if ((long)whereSize >= 616L)
			{
				GeneratedEvent* ptr = <Module>.@new(616UL, (void*)where);
				<Module>.GenericEvent.{ctor}(ptr);
				*(long*)(ptr + 608L / (long)sizeof(GeneratedEvent)) = sessionContext;
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
			return ptr2;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000040A8 File Offset: 0x000040A8
		public unsafe static void Set(void* nativeEventPtr, void* data, uint length, ushort dataAttrIdx, ushort eddIndex)
		{
			<Module>.XE_BufferCollector.Set((byte*)nativeEventPtr + 8L, data, length, dataAttrIdx, eddIndex);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000040CC File Offset: 0x000040CC
		public static ref char GetInteriorPtr(string s)
		{
			ref byte ptr = s;
			if ((ref ptr) != null)
			{
				ptr = (long)RuntimeHelpers.OffsetToStringData + (ref ptr);
			}
			return ref ptr;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000040F0 File Offset: 0x000040F0
		public unsafe static void Publish(void* nativeEventPtr)
		{
			<Module>.Microsoft.SqlServer.XEvent.Internal.GeneratedEvent.Publish(nativeEventPtr);
		}
	}
}
