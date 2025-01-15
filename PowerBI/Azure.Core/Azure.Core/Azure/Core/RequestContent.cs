using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Buffers;
using Azure.Core.Serialization;

namespace Azure.Core
{
	// Token: 0x02000058 RID: 88
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class RequestContent : IDisposable
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x00008755 File Offset: 0x00006955
		public static RequestContent Create(Stream stream)
		{
			return new RequestContent.StreamContent(stream);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000875D File Offset: 0x0000695D
		public static RequestContent Create(byte[] bytes)
		{
			return new RequestContent.ArrayContent(bytes, 0, bytes.Length);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008769 File Offset: 0x00006969
		public static RequestContent Create(byte[] bytes, int index, int length)
		{
			return new RequestContent.ArrayContent(bytes, index, length);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00008773 File Offset: 0x00006973
		[NullableContext(0)]
		[return: Nullable(1)]
		public static RequestContent Create(ReadOnlyMemory<byte> bytes)
		{
			return new RequestContent.MemoryContent(bytes);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000877B File Offset: 0x0000697B
		[NullableContext(0)]
		[return: Nullable(1)]
		public static RequestContent Create(ReadOnlySequence<byte> bytes)
		{
			return new RequestContent.ReadOnlySequenceContent(bytes);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00008783 File Offset: 0x00006983
		public static RequestContent Create(string content)
		{
			return RequestContent.Create(RequestContent.s_UTF8NoBomEncoding.GetBytes(content));
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00008795 File Offset: 0x00006995
		public static RequestContent Create(BinaryData content)
		{
			return new RequestContent.MemoryContent(content.ToMemory());
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000087A2 File Offset: 0x000069A2
		public static RequestContent Create(DynamicData content)
		{
			return new RequestContent.DynamicDataContent(content);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000087AA File Offset: 0x000069AA
		[RequiresUnreferencedCode("This method uses reflection-based serialization which is incompatible with trimming. Try using one of the 'Create' overloads that doesn't wrap a serialized version of an object.")]
		[RequiresDynamicCode("This method uses reflection-based serialization which is incompatible with trimming. Try using one of the 'Create' overloads that doesn't wrap a serialized version of an object.")]
		public static RequestContent Create(object serializable)
		{
			return RequestContent.Create(serializable, JsonObjectSerializer.Default);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000087B8 File Offset: 0x000069B8
		[RequiresUnreferencedCode("This method uses reflection-based serialization which is incompatible with trimming. Try using one of the 'Create' overloads that doesn't wrap a serialized version of an object.")]
		[RequiresDynamicCode("This method uses reflection-based serialization which is incompatible with trimming. Try using one of the 'Create' overloads that doesn't wrap a serialized version of an object.")]
		public static RequestContent Create(object serializable, [Nullable(2)] ObjectSerializer serializer)
		{
			return RequestContent.Create((serializer ?? JsonObjectSerializer.Default).Serialize(serializable, null, default(CancellationToken)));
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000087E4 File Offset: 0x000069E4
		[RequiresUnreferencedCode("This method uses reflection-based serialization which is incompatible with trimming. Try using one of the 'Create' overloads that doesn't wrap a serialized version of an object.")]
		[RequiresDynamicCode("This method uses reflection-based serialization which is incompatible with trimming. Try using one of the 'Create' overloads that doesn't wrap a serialized version of an object.")]
		public static RequestContent Create(object serializable, JsonPropertyNames propertyNameFormat, string dateTimeFormat = "o")
		{
			return RequestContent.Create(new JsonObjectSerializer(DynamicDataOptions.ToSerializerOptions(new DynamicDataOptions
			{
				PropertyNameFormat = propertyNameFormat,
				DateTimeFormat = dateTimeFormat
			})).Serialize(serializable, null, default(CancellationToken)));
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00008823 File Offset: 0x00006A23
		public static implicit operator RequestContent(string content)
		{
			return RequestContent.Create(content);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000882B File Offset: 0x00006A2B
		public static implicit operator RequestContent(BinaryData content)
		{
			return RequestContent.Create(content);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00008833 File Offset: 0x00006A33
		public static implicit operator RequestContent(DynamicData content)
		{
			return RequestContent.Create(content);
		}

		// Token: 0x060002CF RID: 719
		public abstract Task WriteToAsync(Stream stream, CancellationToken cancellation);

		// Token: 0x060002D0 RID: 720
		public abstract void WriteTo(Stream stream, CancellationToken cancellation);

		// Token: 0x060002D1 RID: 721
		public abstract bool TryComputeLength(out long length);

		// Token: 0x060002D2 RID: 722
		public abstract void Dispose();

		// Token: 0x04000131 RID: 305
		internal const string SerializationRequiresUnreferencedCode = "This method uses reflection-based serialization which is incompatible with trimming. Try using one of the 'Create' overloads that doesn't wrap a serialized version of an object.";

		// Token: 0x04000132 RID: 306
		private static readonly Encoding s_UTF8NoBomEncoding = new UTF8Encoding(false);

		// Token: 0x020000F1 RID: 241
		[Nullable(0)]
		private sealed class StreamContent : RequestContent
		{
			// Token: 0x0600074F RID: 1871 RVA: 0x00019DBE File Offset: 0x00017FBE
			public StreamContent(Stream stream)
			{
				if (!stream.CanSeek)
				{
					throw new ArgumentException("stream must be seekable", "stream");
				}
				this._origin = stream.Position;
				this._stream = stream;
			}

			// Token: 0x06000750 RID: 1872 RVA: 0x00019DF4 File Offset: 0x00017FF4
			public override void WriteTo(Stream stream, CancellationToken cancellationToken)
			{
				this._stream.Seek(this._origin, SeekOrigin.Begin);
				byte[] array = ArrayPool<byte>.Shared.Rent(81920);
				try
				{
					for (;;)
					{
						CancellationHelper.ThrowIfCancellationRequested(cancellationToken);
						int num = this._stream.Read(array, 0, array.Length);
						if (num == 0)
						{
							break;
						}
						CancellationHelper.ThrowIfCancellationRequested(cancellationToken);
						stream.Write(array, 0, num);
					}
				}
				finally
				{
					stream.Flush();
					ArrayPool<byte>.Shared.Return(array, true);
				}
			}

			// Token: 0x06000751 RID: 1873 RVA: 0x00019E74 File Offset: 0x00018074
			public override bool TryComputeLength(out long length)
			{
				if (this._stream.CanSeek)
				{
					length = this._stream.Length - this._origin;
					return true;
				}
				length = 0L;
				return false;
			}

			// Token: 0x06000752 RID: 1874 RVA: 0x00019EA0 File Offset: 0x000180A0
			public override async Task WriteToAsync(Stream stream, CancellationToken cancellation)
			{
				this._stream.Seek(this._origin, SeekOrigin.Begin);
				await this._stream.CopyToAsync(stream, 81920, cancellation).ConfigureAwait(false);
			}

			// Token: 0x06000753 RID: 1875 RVA: 0x00019EF3 File Offset: 0x000180F3
			public override void Dispose()
			{
				this._stream.Dispose();
			}

			// Token: 0x04000366 RID: 870
			private const int CopyToBufferSize = 81920;

			// Token: 0x04000367 RID: 871
			private readonly Stream _stream;

			// Token: 0x04000368 RID: 872
			private readonly long _origin;
		}

		// Token: 0x020000F2 RID: 242
		[Nullable(0)]
		private sealed class ArrayContent : RequestContent
		{
			// Token: 0x06000754 RID: 1876 RVA: 0x00019F00 File Offset: 0x00018100
			public ArrayContent(byte[] bytes, int index, int length)
			{
				this._bytes = bytes;
				this._contentStart = index;
				this._contentLength = length;
			}

			// Token: 0x06000755 RID: 1877 RVA: 0x00019F1D File Offset: 0x0001811D
			public override void Dispose()
			{
			}

			// Token: 0x06000756 RID: 1878 RVA: 0x00019F1F File Offset: 0x0001811F
			public override void WriteTo(Stream stream, CancellationToken cancellation)
			{
				stream.Write(this._bytes, this._contentStart, this._contentLength);
			}

			// Token: 0x06000757 RID: 1879 RVA: 0x00019F39 File Offset: 0x00018139
			public override bool TryComputeLength(out long length)
			{
				length = (long)this._contentLength;
				return true;
			}

			// Token: 0x06000758 RID: 1880 RVA: 0x00019F48 File Offset: 0x00018148
			public override async Task WriteToAsync(Stream stream, CancellationToken cancellation)
			{
				await stream.WriteAsync(this._bytes, this._contentStart, this._contentLength, cancellation).ConfigureAwait(false);
			}

			// Token: 0x04000369 RID: 873
			private readonly byte[] _bytes;

			// Token: 0x0400036A RID: 874
			private readonly int _contentStart;

			// Token: 0x0400036B RID: 875
			private readonly int _contentLength;
		}

		// Token: 0x020000F3 RID: 243
		[NullableContext(0)]
		private sealed class MemoryContent : RequestContent
		{
			// Token: 0x06000759 RID: 1881 RVA: 0x00019F9B File Offset: 0x0001819B
			public MemoryContent(ReadOnlyMemory<byte> bytes)
			{
				this._bytes = bytes;
			}

			// Token: 0x0600075A RID: 1882 RVA: 0x00019FAA File Offset: 0x000181AA
			public override void Dispose()
			{
			}

			// Token: 0x0600075B RID: 1883 RVA: 0x00019FAC File Offset: 0x000181AC
			[NullableContext(1)]
			public override void WriteTo(Stream stream, CancellationToken cancellation)
			{
				byte[] array = this._bytes.ToArray();
				stream.Write(array, 0, array.Length);
			}

			// Token: 0x0600075C RID: 1884 RVA: 0x00019FD0 File Offset: 0x000181D0
			public override bool TryComputeLength(out long length)
			{
				length = (long)this._bytes.Length;
				return true;
			}

			// Token: 0x0600075D RID: 1885 RVA: 0x00019FE4 File Offset: 0x000181E4
			[NullableContext(1)]
			public override async Task WriteToAsync(Stream stream, CancellationToken cancellation)
			{
				await stream.WriteAsync(this._bytes, cancellation).ConfigureAwait(false);
			}

			// Token: 0x0400036C RID: 876
			private readonly ReadOnlyMemory<byte> _bytes;
		}

		// Token: 0x020000F4 RID: 244
		[NullableContext(0)]
		private sealed class ReadOnlySequenceContent : RequestContent
		{
			// Token: 0x0600075E RID: 1886 RVA: 0x0001A037 File Offset: 0x00018237
			public ReadOnlySequenceContent(ReadOnlySequence<byte> bytes)
			{
				this._bytes = bytes;
			}

			// Token: 0x0600075F RID: 1887 RVA: 0x0001A046 File Offset: 0x00018246
			public override void Dispose()
			{
			}

			// Token: 0x06000760 RID: 1888 RVA: 0x0001A048 File Offset: 0x00018248
			[NullableContext(1)]
			public override void WriteTo(Stream stream, CancellationToken cancellation)
			{
				byte[] array = BuffersExtensions.ToArray<byte>(ref this._bytes);
				stream.Write(array, 0, array.Length);
			}

			// Token: 0x06000761 RID: 1889 RVA: 0x0001A06C File Offset: 0x0001826C
			public override bool TryComputeLength(out long length)
			{
				length = this._bytes.Length;
				return true;
			}

			// Token: 0x06000762 RID: 1890 RVA: 0x0001A07C File Offset: 0x0001827C
			[NullableContext(1)]
			public override async Task WriteToAsync(Stream stream, CancellationToken cancellation)
			{
				await stream.WriteAsync(this._bytes, cancellation).ConfigureAwait(false);
			}

			// Token: 0x0400036D RID: 877
			private readonly ReadOnlySequence<byte> _bytes;
		}

		// Token: 0x020000F5 RID: 245
		[Nullable(0)]
		private sealed class DynamicDataContent : RequestContent
		{
			// Token: 0x06000763 RID: 1891 RVA: 0x0001A0CF File Offset: 0x000182CF
			public DynamicDataContent(DynamicData data)
			{
				this._data = data;
			}

			// Token: 0x06000764 RID: 1892 RVA: 0x0001A0DE File Offset: 0x000182DE
			public override void Dispose()
			{
				this._data.Dispose();
			}

			// Token: 0x06000765 RID: 1893 RVA: 0x0001A0EB File Offset: 0x000182EB
			public override void WriteTo(Stream stream, CancellationToken cancellation)
			{
				this._data.WriteTo(stream);
			}

			// Token: 0x06000766 RID: 1894 RVA: 0x0001A0F9 File Offset: 0x000182F9
			public override bool TryComputeLength(out long length)
			{
				length = 0L;
				return false;
			}

			// Token: 0x06000767 RID: 1895 RVA: 0x0001A100 File Offset: 0x00018300
			public override Task WriteToAsync(Stream stream, CancellationToken cancellation)
			{
				this._data.WriteTo(stream);
				return Task.CompletedTask;
			}

			// Token: 0x0400036E RID: 878
			private readonly DynamicData _data;
		}
	}
}
