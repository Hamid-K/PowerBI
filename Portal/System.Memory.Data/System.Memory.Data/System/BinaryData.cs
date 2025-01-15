using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
	// Token: 0x02000005 RID: 5
	[NullableContext(1)]
	[Nullable(0)]
	public class BinaryData
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000208E File Offset: 0x0000028E
		public BinaryData(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._bytes = data;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020B0 File Offset: 0x000002B0
		[NullableContext(2)]
		public BinaryData(object jsonSerializable, JsonSerializerOptions options = null, Type type = null)
		{
			this._bytes = JsonSerializer.SerializeToUtf8Bytes(jsonSerializable, type ?? ((jsonSerializable != null) ? jsonSerializable.GetType() : null) ?? typeof(object), options);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E9 File Offset: 0x000002E9
		[NullableContext(0)]
		public BinaryData(ReadOnlyMemory<byte> data)
		{
			this._bytes = data;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020F8 File Offset: 0x000002F8
		public BinaryData(string data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._bytes = Encoding.UTF8.GetBytes(data);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002124 File Offset: 0x00000324
		[NullableContext(0)]
		[return: Nullable(1)]
		public static BinaryData FromBytes(ReadOnlyMemory<byte> data)
		{
			return new BinaryData(data);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000212C File Offset: 0x0000032C
		public static BinaryData FromBytes(byte[] data)
		{
			return new BinaryData(data);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002134 File Offset: 0x00000334
		public static BinaryData FromString(string data)
		{
			return new BinaryData(data);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000213C File Offset: 0x0000033C
		public static BinaryData FromStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return BinaryData.FromStreamAsync(stream, false, default(CancellationToken)).GetAwaiter().GetResult();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002174 File Offset: 0x00000374
		public static Task<BinaryData> FromStreamAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return BinaryData.FromStreamAsync(stream, true, cancellationToken);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000218C File Offset: 0x0000038C
		private static async Task<BinaryData> FromStreamAsync(Stream stream, bool async, CancellationToken cancellationToken = default(CancellationToken))
		{
			int num = 0;
			if (stream.CanSeek)
			{
				long num2 = stream.Length - stream.Position;
				if (num2 > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("stream", "Stream length must be less than Int32.MaxValue");
				}
				num = (int)num2;
			}
			BinaryData binaryData;
			using (MemoryStream memoryStream = (stream.CanSeek ? new MemoryStream(num) : new MemoryStream()))
			{
				if (async)
				{
					await stream.CopyToAsync(memoryStream, 81920, cancellationToken).ConfigureAwait(false);
				}
				else
				{
					stream.CopyTo(memoryStream);
				}
				binaryData = new BinaryData(MemoryExtensions.AsMemory<byte>(memoryStream.GetBuffer(), 0, (int)memoryStream.Position));
			}
			return binaryData;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021DF File Offset: 0x000003DF
		public static BinaryData FromObjectAsJson<[Nullable(2)] T>(T jsonSerializable, [Nullable(2)] JsonSerializerOptions options = null)
		{
			return new BinaryData(JsonSerializer.SerializeToUtf8Bytes(jsonSerializable, typeof(T), options));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021FC File Offset: 0x000003FC
		public override string ToString()
		{
			ArraySegment<byte> arraySegment;
			if (MemoryMarshal.TryGetArray<byte>(this._bytes, ref arraySegment))
			{
				return Encoding.UTF8.GetString(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
			return Encoding.UTF8.GetString(this._bytes.ToArray());
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000224D File Offset: 0x0000044D
		public Stream ToStream()
		{
			return new ReadOnlyMemoryStream(this._bytes);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000225A File Offset: 0x0000045A
		[NullableContext(0)]
		public ReadOnlyMemory<byte> ToMemory()
		{
			return this._bytes;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002262 File Offset: 0x00000462
		public byte[] ToArray()
		{
			return this._bytes.ToArray();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000226F File Offset: 0x0000046F
		[NullableContext(2)]
		[return: Nullable(1)]
		public T ToObjectFromJson<T>(JsonSerializerOptions options = null)
		{
			return (T)((object)JsonSerializer.Deserialize(this._bytes.Span, typeof(T), options));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002294 File Offset: 0x00000494
		[NullableContext(0)]
		public static implicit operator ReadOnlyMemory<byte>([Nullable(2)] BinaryData data)
		{
			if (data == null)
			{
				return default(ReadOnlyMemory<byte>);
			}
			return data._bytes;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022B4 File Offset: 0x000004B4
		[NullableContext(0)]
		public static implicit operator ReadOnlySpan<byte>([Nullable(2)] BinaryData data)
		{
			if (data == null)
			{
				return default(ReadOnlySpan<byte>);
			}
			return data._bytes.Span;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022D9 File Offset: 0x000004D9
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022DF File Offset: 0x000004DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000003 RID: 3
		private const int CopyToBufferSize = 81920;

		// Token: 0x04000004 RID: 4
		[Nullable(0)]
		private readonly ReadOnlyMemory<byte> _bytes;
	}
}
