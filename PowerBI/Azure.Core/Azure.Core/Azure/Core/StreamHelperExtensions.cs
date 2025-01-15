using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core
{
	// Token: 0x02000053 RID: 83
	[NullableContext(1)]
	[Nullable(0)]
	internal static class StreamHelperExtensions
	{
		// Token: 0x0600028D RID: 653 RVA: 0x00007E28 File Offset: 0x00006028
		public static Task DrainAsync(this Stream stream, CancellationToken cancellationToken)
		{
			return stream.DrainAsync(ArrayPool<byte>.Shared, null, cancellationToken);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00007E4A File Offset: 0x0000604A
		public static Task DrainAsync(this Stream stream, long? limit, CancellationToken cancellationToken)
		{
			return stream.DrainAsync(ArrayPool<byte>.Shared, limit, cancellationToken);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00007E5C File Offset: 0x0000605C
		public static async Task DrainAsync(this Stream stream, ArrayPool<byte> bytePool, long? limit, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			byte[] buffer = bytePool.Rent(4096);
			long total = 0L;
			try
			{
				for (int i = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false); i > 0; i = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false))
				{
					cancellationToken.ThrowIfCancellationRequested();
					if (limit != null && limit.GetValueOrDefault() - total < (long)i)
					{
						throw new InvalidDataException(string.Format("The stream exceeded the data limit {0}.", limit.GetValueOrDefault()));
					}
					total += (long)i;
				}
			}
			finally
			{
				bytePool.Return(buffer, false);
			}
		}

		// Token: 0x04000121 RID: 289
		private const int _maxReadBufferSize = 4096;
	}
}
