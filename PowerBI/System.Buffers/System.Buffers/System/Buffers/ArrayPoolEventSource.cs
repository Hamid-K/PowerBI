using System;
using System.Diagnostics.Tracing;

namespace System.Buffers
{
	// Token: 0x02000005 RID: 5
	[EventSource(Name = "System.Buffers.ArrayPoolEventSource")]
	internal sealed class ArrayPoolEventSource : EventSource
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000021D0 File Offset: 0x000003D0
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

		// Token: 0x06000014 RID: 20 RVA: 0x000022A8 File Offset: 0x000004A8
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

		// Token: 0x06000015 RID: 21 RVA: 0x000023AD File Offset: 0x000005AD
		[Event(3, Level = EventLevel.Verbose)]
		internal void BufferReturned(int bufferId, int bufferSize, int poolId)
		{
			base.WriteEvent(3, bufferId, bufferSize, poolId);
		}

		// Token: 0x04000004 RID: 4
		internal static readonly ArrayPoolEventSource Log = new ArrayPoolEventSource();

		// Token: 0x02000008 RID: 8
		internal enum BufferAllocatedReason
		{
			// Token: 0x0400000A RID: 10
			Pooled,
			// Token: 0x0400000B RID: 11
			OverMaximumSize,
			// Token: 0x0400000C RID: 12
			PoolExhausted
		}
	}
}
