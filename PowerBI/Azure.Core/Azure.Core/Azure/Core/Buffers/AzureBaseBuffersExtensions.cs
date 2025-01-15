using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core.Buffers
{
	// Token: 0x020000AA RID: 170
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AzureBaseBuffersExtensions
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x000102C0 File Offset: 0x0000E4C0
		public static async Task WriteAsync(this Stream stream, [Nullable(0)] ReadOnlyMemory<byte> buffer, CancellationToken cancellation = default(CancellationToken))
		{
			Argument.AssertNotNull<Stream>(stream, "stream");
			if (buffer.Length != 0)
			{
				byte[] array = null;
				try
				{
					ArraySegment<byte> arraySegment;
					if (MemoryMarshal.TryGetArray<byte>(buffer, ref arraySegment))
					{
						await stream.WriteAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count, cancellation).ConfigureAwait(false);
					}
					else
					{
						array = ArrayPool<byte>.Shared.Rent(buffer.Length);
						if (!buffer.TryCopyTo(array))
						{
							throw new Exception("could not rent large enough buffer.");
						}
						await stream.WriteAsync(array, 0, buffer.Length, cancellation).ConfigureAwait(false);
					}
				}
				finally
				{
					if (array != null)
					{
						ArrayPool<byte>.Shared.Return(array, false);
					}
				}
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00010314 File Offset: 0x0000E514
		public static async Task WriteAsync(this Stream stream, [Nullable(0)] ReadOnlySequence<byte> buffer, CancellationToken cancellation = default(CancellationToken))
		{
			Argument.AssertNotNull<Stream>(stream, "stream");
			if (buffer.Length != 0L)
			{
				byte[] array = null;
				try
				{
					foreach (ReadOnlyMemory<byte> readOnlyMemory in buffer)
					{
						ArraySegment<byte> arraySegment;
						if (MemoryMarshal.TryGetArray<byte>(readOnlyMemory, ref arraySegment))
						{
							await stream.WriteAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count, cancellation).ConfigureAwait(false);
						}
						else
						{
							if (array == null || array.Length < readOnlyMemory.Length)
							{
								if (array != null)
								{
									ArrayPool<byte>.Shared.Return(array, false);
								}
								array = ArrayPool<byte>.Shared.Rent(readOnlyMemory.Length);
							}
							if (!readOnlyMemory.TryCopyTo(array))
							{
								throw new Exception("could not rent large enough buffer.");
							}
							await stream.WriteAsync(array, 0, readOnlyMemory.Length, cancellation).ConfigureAwait(false);
						}
					}
					ReadOnlySequence<byte>.Enumerator enumerator = default(ReadOnlySequence<byte>.Enumerator);
				}
				finally
				{
					if (array != null)
					{
						ArrayPool<byte>.Shared.Return(array, false);
					}
				}
			}
		}
	}
}
