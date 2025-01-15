using System;
using System.IO;
using System.IO.Compression;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012A4 RID: 4772
	internal static class Compression
	{
		// Token: 0x06007D4B RID: 32075 RVA: 0x001AD9E8 File Offset: 0x001ABBE8
		public static BinaryValue Compress(BinaryValue value, NumberValue compressionType)
		{
			CompressionKind compressionKind = Library.CompressionType.Type.GetValue(compressionType);
			if (compressionKind == CompressionKind.None)
			{
				return value;
			}
			Func<Stream, Stream> <>9__1;
			return new WrappedStreamBinaryValue(value, delegate(Stream s)
			{
				Func<Stream, Stream> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (Stream w) => Compression.Compress(compressionKind, w));
				}
				return new ErrorWrappingReadStream(new Compression.ReadableCompressionStream(s, func));
			});
		}

		// Token: 0x06007D4C RID: 32076 RVA: 0x001ADA2C File Offset: 0x001ABC2C
		public static BinaryValue Decompress(BinaryValue value, NumberValue compressionType)
		{
			CompressionKind compressionKind = Library.CompressionType.Type.GetValue(compressionType);
			if (compressionKind == CompressionKind.None)
			{
				return value;
			}
			return new WrappedStreamBinaryValue(value, (Stream s) => new ErrorWrappingReadStream(Compression.Decompress(compressionKind, s)));
		}

		// Token: 0x06007D4D RID: 32077 RVA: 0x001ADA70 File Offset: 0x001ABC70
		public static Stream Decompress(CompressionKind compressionKind, Stream stream)
		{
			if (compressionKind >= CompressionKind.GZip && compressionKind <= CompressionKind.Zstandard)
			{
				Func<Stream, Stream> func = Compression.decompressionCtors[(int)compressionKind];
				if (func != null)
				{
					return func(stream);
				}
			}
			throw ValueException.NewExpressionError<Message0>(Strings.Compression_InvalidType, NumberValue.New((int)compressionKind), null);
		}

		// Token: 0x06007D4E RID: 32078 RVA: 0x001ADAAC File Offset: 0x001ABCAC
		public static Stream Compress(CompressionKind compressionKind, Stream stream)
		{
			if (compressionKind >= CompressionKind.GZip && compressionKind <= CompressionKind.Zstandard)
			{
				Func<Stream, Stream> func = Compression.compressionCtors[(int)compressionKind];
				if (func != null)
				{
					return func(stream);
				}
			}
			throw ValueException.NewExpressionError<Message0>(Strings.Compression_InvalidType, NumberValue.New((int)compressionKind), null);
		}

		// Token: 0x06007D4F RID: 32079 RVA: 0x001ADAE8 File Offset: 0x001ABCE8
		// Note: this type is marked as 'beforefieldinit'.
		static Compression()
		{
			Func<Stream, Stream>[] array = new Func<Stream, Stream>[6];
			array[0] = (Stream s) => new GZipStream(s, CompressionMode.Compress);
			array[1] = (Stream s) => new DeflateStream(s, CompressionMode.Compress);
			Compression.compressionCtors = array;
			Func<Stream, Stream>[] array2 = new Func<Stream, Stream>[6];
			array2[0] = (Stream s) => new GZipStream(s, CompressionMode.Decompress);
			array2[1] = (Stream s) => new DeflateStream(s, CompressionMode.Decompress);
			Compression.decompressionCtors = array2;
		}

		// Token: 0x0400450B RID: 17675
		private static Func<Stream, Stream>[] compressionCtors;

		// Token: 0x0400450C RID: 17676
		private static Func<Stream, Stream>[] decompressionCtors;

		// Token: 0x020012A5 RID: 4773
		private sealed class ReadableCompressionStream : Stream
		{
			// Token: 0x06007D50 RID: 32080 RVA: 0x001ADB57 File Offset: 0x001ABD57
			public ReadableCompressionStream(Stream inputStream, Func<Stream, Stream> writeStreamConstructor)
			{
				this.inputBuffer = new byte[4096];
				this.input = inputStream;
				this.buffer = new MemoryStream();
				this.adapter = writeStreamConstructor(this.buffer);
			}

			// Token: 0x17002211 RID: 8721
			// (get) Token: 0x06007D51 RID: 32081 RVA: 0x00002139 File Offset: 0x00000339
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17002212 RID: 8722
			// (get) Token: 0x06007D52 RID: 32082 RVA: 0x00002105 File Offset: 0x00000305
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002213 RID: 8723
			// (get) Token: 0x06007D53 RID: 32083 RVA: 0x00002105 File Offset: 0x00000305
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002214 RID: 8724
			// (get) Token: 0x06007D54 RID: 32084 RVA: 0x000091AE File Offset: 0x000073AE
			public override long Length
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17002215 RID: 8725
			// (get) Token: 0x06007D55 RID: 32085 RVA: 0x000091AE File Offset: 0x000073AE
			// (set) Token: 0x06007D56 RID: 32086 RVA: 0x000091AE File Offset: 0x000073AE
			public override long Position
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17002216 RID: 8726
			// (get) Token: 0x06007D57 RID: 32087 RVA: 0x001ADB93 File Offset: 0x001ABD93
			private long Available
			{
				get
				{
					return this.buffer.Length - this.buffer.Position;
				}
			}

			// Token: 0x06007D58 RID: 32088 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Flush()
			{
			}

			// Token: 0x06007D59 RID: 32089 RVA: 0x001ADBAC File Offset: 0x001ABDAC
			public override int ReadByte()
			{
				if (this.Available == 0L)
				{
					this.FillBuffer();
				}
				return this.buffer.ReadByte();
			}

			// Token: 0x06007D5A RID: 32090 RVA: 0x001ADBC7 File Offset: 0x001ABDC7
			public override int Read(byte[] buffer, int offset, int count)
			{
				if (this.Available == 0L)
				{
					this.FillBuffer();
				}
				return this.buffer.Read(buffer, offset, count);
			}

			// Token: 0x06007D5B RID: 32091 RVA: 0x000091AE File Offset: 0x000073AE
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06007D5C RID: 32092 RVA: 0x000091AE File Offset: 0x000073AE
			public override void SetLength(long value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06007D5D RID: 32093 RVA: 0x000091AE File Offset: 0x000073AE
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06007D5E RID: 32094 RVA: 0x001ADBE5 File Offset: 0x001ABDE5
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.input != null)
				{
					this.input.Dispose();
					this.buffer.Dispose();
					this.input = null;
					this.buffer = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x06007D5F RID: 32095 RVA: 0x001ADC20 File Offset: 0x001ABE20
			private void FillBuffer()
			{
				this.buffer.Position = 0L;
				this.buffer.SetLength(0L);
				while (this.adapter != null && this.Available == 0L)
				{
					int num = this.input.Read(this.inputBuffer, 0, this.inputBuffer.Length);
					if (num == 0)
					{
						this.adapter.Flush();
						this.adapter.Close();
						this.adapter.Dispose();
						this.adapter = null;
						this.buffer = new MemoryStream(this.buffer.ToArray());
					}
					else
					{
						this.adapter.Write(this.inputBuffer, 0, num);
					}
				}
			}

			// Token: 0x0400450D RID: 17677
			private const int inputChunkSize = 4096;

			// Token: 0x0400450E RID: 17678
			private byte[] inputBuffer;

			// Token: 0x0400450F RID: 17679
			private Stream input;

			// Token: 0x04004510 RID: 17680
			private Stream adapter;

			// Token: 0x04004511 RID: 17681
			private MemoryStream buffer;
		}
	}
}
