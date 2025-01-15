using System;
using System.Diagnostics.Tracing;

namespace System.Buffers
{
	// Token: 0x020000F3 RID: 243
	[EventSource(Name = "System.Buffers.ArrayPoolEventSource")]
	internal sealed class ArrayPoolEventSource : EventSource
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x0002B144 File Offset: 0x00029344
		[Event(1, Level = EventLevel.Verbose)]
		internal unsafe void BufferRented(int bufferId, int bufferSize, int poolId, int bucketId)
		{
			EventSource.EventData* ptr;
			checked
			{
				ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
			}
			*ptr = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&bufferId))
			};
			ptr[1] = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&bufferSize))
			};
			ptr[2] = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&poolId))
			};
			ptr[3] = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&bucketId))
			};
			base.WriteEventCore(1, 4, ptr);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0002B220 File Offset: 0x00029420
		[Event(2, Level = EventLevel.Informational)]
		internal unsafe void BufferAllocated(int bufferId, int bufferSize, int poolId, int bucketId, ArrayPoolEventSource.BufferAllocatedReason reason)
		{
			EventSource.EventData* ptr;
			checked
			{
				ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData)];
			}
			*ptr = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&bufferId))
			};
			ptr[1] = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&bufferSize))
			};
			ptr[2] = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&poolId))
			};
			ptr[3] = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&bucketId))
			};
			ptr[4] = new EventSource.EventData
			{
				Size = 4,
				DataPointer = (IntPtr)((void*)(&reason))
			};
			base.WriteEventCore(2, 5, ptr);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002B32C File Offset: 0x0002952C
		[Event(3, Level = EventLevel.Verbose)]
		internal void BufferReturned(int bufferId, int bufferSize, int poolId)
		{
			base.WriteEvent(3, bufferId, bufferSize, poolId);
		}

		// Token: 0x040002B1 RID: 689
		internal static readonly ArrayPoolEventSource Log = new ArrayPoolEventSource();

		// Token: 0x0200015E RID: 350
		internal enum BufferAllocatedReason
		{
			// Token: 0x0400035E RID: 862
			Pooled,
			// Token: 0x0400035F RID: 863
			OverMaximumSize,
			// Token: 0x04000360 RID: 864
			PoolExhausted
		}
	}
}
