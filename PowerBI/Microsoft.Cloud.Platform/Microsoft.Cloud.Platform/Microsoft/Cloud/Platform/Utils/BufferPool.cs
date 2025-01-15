using System;
using System.Collections.Concurrent;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200019E RID: 414
	public sealed class BufferPool : IIdentifiable
	{
		// Token: 0x06000A9E RID: 2718 RVA: 0x00024848 File Offset: 0x00022A48
		public BufferPool([NotNull] string name, int segmentSize, int numSegments)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			ExtendedDiagnostics.EnsureArgumentIsPositive(segmentSize, "segmentSize");
			ExtendedDiagnostics.EnsureArgumentIsPositive(numSegments, "numSegments");
			this.m_name = name;
			this.m_segmentSize = segmentSize;
			this.m_numSegments = numSegments;
			this.m_buffer = new byte[segmentSize * numSegments];
			this.m_segmentOwners = new IIdentifiable[numSegments];
			this.m_freeSegments = new ConcurrentBag<ArraySegment<byte>>();
			for (int i = 0; i < this.m_numSegments; i++)
			{
				this.m_freeSegments.Add(new ArraySegment<byte>(this.m_buffer, i * this.m_segmentSize, this.m_segmentSize));
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000248EC File Offset: 0x00022AEC
		public ArraySegment<byte> GetBuffer([NotNull] IIdentifiable owner)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IIdentifiable>(owner, "owner");
			ArraySegment<byte> arraySegment;
			if (!this.m_freeSegments.TryTake(out arraySegment))
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceWarning("Buffer Pool {0} - capacity exceeded returning buffer from regular heap", new object[] { this.Name });
				arraySegment = new ArraySegment<byte>(new byte[this.m_segmentSize]);
			}
			else
			{
				IIdentifiable identifiable = Interlocked.Exchange<IIdentifiable>(ref this.m_segmentOwners[arraySegment.Offset / this.m_segmentSize], owner);
				if (identifiable != null)
				{
					ExtendedEnvironment.FailSlow(this, "buffer retrieved from pool {0} that was already owned by {1}, it must have been added to the pool twice".FormatWithInvariantCulture(new object[] { this.Name, identifiable.Name }));
				}
			}
			return arraySegment;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00024994 File Offset: 0x00022B94
		public void ReturnBuffer(ArraySegment<byte> buffer)
		{
			if (buffer.Array != this.m_buffer)
			{
				return;
			}
			int num2;
			int num = Math.DivRem(buffer.Offset, this.m_segmentSize, out num2);
			if (num2 != 0)
			{
				ExtendedEnvironment.FailSlow(this, "buffer returned to pool {0} with an invalid offset".FormatWithInvariantCulture(new object[] { this.Name }));
			}
			if (num >= this.m_numSegments)
			{
				ExtendedEnvironment.FailSlow(this, "buffer returned to pool {0} with an offset outside the bounds of the pool".FormatWithInvariantCulture(new object[] { this.Name }));
			}
			if (Interlocked.Exchange<IIdentifiable>(ref this.m_segmentOwners[num], null) == null)
			{
				ExtendedEnvironment.FailSlow(this, "buffer returned to pool {0} that was not listed in owners array, it must already be in the pool".FormatWithInvariantCulture(new object[] { this.Name }));
			}
			this.m_freeSegments.Add(new ArraySegment<byte>(buffer.Array, buffer.Offset, this.m_segmentSize));
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00024A67 File Offset: 0x00022C67
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x0400042F RID: 1071
		private readonly byte[] m_buffer;

		// Token: 0x04000430 RID: 1072
		private readonly ConcurrentBag<ArraySegment<byte>> m_freeSegments;

		// Token: 0x04000431 RID: 1073
		private readonly IIdentifiable[] m_segmentOwners;

		// Token: 0x04000432 RID: 1074
		private readonly string m_name;

		// Token: 0x04000433 RID: 1075
		private readonly int m_segmentSize;

		// Token: 0x04000434 RID: 1076
		private readonly int m_numSegments;
	}
}
