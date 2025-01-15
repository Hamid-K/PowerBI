using System;
using System.IO;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C22 RID: 7202
	public static class StreamExtensions
	{
		// Token: 0x0600B3C4 RID: 46020 RVA: 0x0024811B File Offset: 0x0024631B
		public static long CopyTo(this Stream stream, Stream destination)
		{
			return stream.CopyTo(destination, 4096);
		}

		// Token: 0x0600B3C5 RID: 46021 RVA: 0x00248129 File Offset: 0x00246329
		public static long CopyTo(this Stream stream, Stream destination, int bufferSize)
		{
			return stream.CopyTo(destination, new byte[bufferSize]);
		}

		// Token: 0x0600B3C6 RID: 46022 RVA: 0x00248138 File Offset: 0x00246338
		public static long CopyTo(this Stream stream, Stream destination, byte[] buffer)
		{
			long num = 0L;
			int num2;
			while ((num2 = stream.Read(buffer, 0, buffer.Length)) > 0)
			{
				destination.Write(buffer, 0, num2);
				num += (long)num2;
			}
			return num;
		}

		// Token: 0x0600B3C7 RID: 46023 RVA: 0x0024816C File Offset: 0x0024636C
		public static byte[] ReadBlock(this Stream stream, int count)
		{
			byte[] array = new byte[count];
			stream.ReadBlock(array, 0, count);
			return array;
		}

		// Token: 0x0600B3C8 RID: 46024 RVA: 0x0024818C File Offset: 0x0024638C
		public static void ReadBlock(this Stream stream, byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				int num = stream.Read(buffer, offset, count);
				if (num == 0)
				{
					throw new EndOfStreamException();
				}
				offset += num;
				count -= num;
			}
		}

		// Token: 0x0600B3C9 RID: 46025 RVA: 0x002481BC File Offset: 0x002463BC
		public static void WriteZeros(this Stream stream, long count)
		{
			byte[] array = new byte[4096];
			while (count > 0L)
			{
				int num = (int)Math.Min((long)array.Length, count);
				stream.Write(array, 0, num);
				count -= (long)num;
			}
		}

		// Token: 0x0600B3CA RID: 46026 RVA: 0x002481F6 File Offset: 0x002463F6
		public static Stream Concat(params Stream[] streams)
		{
			return new StreamExtensions.ConcatStream(streams);
		}

		// Token: 0x0600B3CB RID: 46027 RVA: 0x002481FE File Offset: 0x002463FE
		public static Stream Concat(this Stream stream1, Stream stream2)
		{
			return StreamExtensions.Concat(new Stream[] { stream1, stream2 });
		}

		// Token: 0x0600B3CC RID: 46028 RVA: 0x00248214 File Offset: 0x00246414
		public static Stream Skip(this Stream stream, long skip)
		{
			if (skip > 0L)
			{
				if (stream.CanSeek)
				{
					stream = new OffsetStream(stream, stream.Position + skip);
					if (stream.Position != 0L)
					{
						stream.Position = 0L;
					}
				}
				else
				{
					byte[] array = new byte[4096];
					while (skip > 0L)
					{
						int num = stream.Read(array, 0, (int)Math.Min(skip, (long)array.Length));
						if (num == 0)
						{
							break;
						}
						skip -= (long)num;
					}
				}
			}
			return stream;
		}

		// Token: 0x0600B3CD RID: 46029 RVA: 0x00248280 File Offset: 0x00246480
		public static Stream Take(this Stream stream, long take)
		{
			if (take < 9223372036854775807L)
			{
				stream = new StreamExtensions.TakeStream(stream, take);
			}
			return stream;
		}

		// Token: 0x0600B3CE RID: 46030 RVA: 0x00248298 File Offset: 0x00246498
		public static Stream OnDispose(this Stream stream, Action action)
		{
			return new StreamExtensions.NotifyingStream(stream, action);
		}

		// Token: 0x0600B3CF RID: 46031 RVA: 0x002482A4 File Offset: 0x002464A4
		public static Stream AfterDispose(this Stream stream, Action action)
		{
			return new StreamExtensions.NotifyingStream(stream, delegate
			{
				try
				{
					stream.Dispose();
				}
				finally
				{
					action();
				}
			});
		}

		// Token: 0x0600B3D0 RID: 46032 RVA: 0x002482DC File Offset: 0x002464DC
		public static Stream TranslateErrors(this Stream stream, Func<Exception, Exception> translateError)
		{
			return new StreamExtensions.ErrorTranslatingStream(stream, translateError);
		}

		// Token: 0x0600B3D1 RID: 46033 RVA: 0x002482E5 File Offset: 0x002464E5
		public static Stream NonDisposable(this Stream stream)
		{
			return new StreamExtensions.NonDisposableStream(stream);
		}

		// Token: 0x02001C23 RID: 7203
		private class TakeStream : DelegatingStream
		{
			// Token: 0x0600B3D2 RID: 46034 RVA: 0x002482ED File Offset: 0x002464ED
			public TakeStream(Stream stream, long take)
				: base(stream)
			{
				this.take = take;
			}

			// Token: 0x17002D04 RID: 11524
			// (get) Token: 0x0600B3D3 RID: 46035 RVA: 0x002482FD File Offset: 0x002464FD
			public override long Length
			{
				get
				{
					return Math.Min(this.take, base.Length);
				}
			}

			// Token: 0x17002D05 RID: 11525
			// (get) Token: 0x0600B3D4 RID: 46036 RVA: 0x00248310 File Offset: 0x00246510
			// (set) Token: 0x0600B3D5 RID: 46037 RVA: 0x00248318 File Offset: 0x00246518
			public override long Position
			{
				get
				{
					return this.position;
				}
				set
				{
					if (value > this.take)
					{
						throw new ArgumentOutOfRangeException();
					}
					this.position = value;
					base.Position = value;
				}
			}

			// Token: 0x0600B3D6 RID: 46038 RVA: 0x00248338 File Offset: 0x00246538
			public override int Read(byte[] buffer, int offset, int count)
			{
				long num = Math.Min(this.take - this.position, (long)count);
				int num2 = base.Read(buffer, offset, (int)num);
				this.position += (long)num2;
				return num2;
			}

			// Token: 0x0600B3D7 RID: 46039 RVA: 0x00248375 File Offset: 0x00246575
			public override int ReadByte()
			{
				if (this.position >= this.take)
				{
					return -1;
				}
				int num = base.ReadByte();
				if (num != -1)
				{
					this.position += 1L;
				}
				return num;
			}

			// Token: 0x0600B3D8 RID: 46040 RVA: 0x002483A0 File Offset: 0x002465A0
			public override long Seek(long offset, SeekOrigin origin)
			{
				long num = this.Position;
				long num2 = base.Seek(offset, origin);
				if (num2 > this.take)
				{
					this.Position = num;
					throw new ArgumentOutOfRangeException();
				}
				this.position = num2;
				return num2;
			}

			// Token: 0x0600B3D9 RID: 46041 RVA: 0x000033E7 File Offset: 0x000015E7
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600B3DA RID: 46042 RVA: 0x002483DB File Offset: 0x002465DB
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (this.position + (long)count > this.take)
				{
					throw new ArgumentOutOfRangeException();
				}
				base.Write(buffer, offset, count);
				this.position += (long)count;
			}

			// Token: 0x0600B3DB RID: 46043 RVA: 0x0024840C File Offset: 0x0024660C
			public override void WriteByte(byte value)
			{
				if (this.position >= this.take)
				{
					throw new ArgumentOutOfRangeException();
				}
				base.WriteByte(value);
				this.position += 1L;
			}

			// Token: 0x04005B9C RID: 23452
			private readonly long take;

			// Token: 0x04005B9D RID: 23453
			private long position;
		}

		// Token: 0x02001C24 RID: 7204
		private class ConcatStream : Stream
		{
			// Token: 0x0600B3DC RID: 46044 RVA: 0x00248438 File Offset: 0x00246638
			public ConcatStream(Stream[] streams)
			{
				this.streams = streams;
				this.length = StreamExtensions.ConcatStream.GetLength(streams);
				this.currentStream = 0;
				this.position = 0L;
			}

			// Token: 0x0600B3DD RID: 46045 RVA: 0x00248464 File Offset: 0x00246664
			private static long GetLength(Stream[] streams)
			{
				long num = 0L;
				for (int i = 0; i < streams.Length; i++)
				{
					if (!streams[i].CanSeek)
					{
						return -1L;
					}
					num += streams[i].Length;
				}
				return num;
			}

			// Token: 0x17002D06 RID: 11526
			// (get) Token: 0x0600B3DE RID: 46046 RVA: 0x00002139 File Offset: 0x00000339
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17002D07 RID: 11527
			// (get) Token: 0x0600B3DF RID: 46047 RVA: 0x0024849B File Offset: 0x0024669B
			public override bool CanSeek
			{
				get
				{
					return this.length != -1L;
				}
			}

			// Token: 0x17002D08 RID: 11528
			// (get) Token: 0x0600B3E0 RID: 46048 RVA: 0x00002105 File Offset: 0x00000305
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600B3E1 RID: 46049 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override void Flush()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x0600B3E2 RID: 46050 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override void SetLength(long value)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x0600B3E3 RID: 46051 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x0600B3E4 RID: 46052 RVA: 0x002484AC File Offset: 0x002466AC
			public override void Close()
			{
				for (int i = 0; i < this.streams.Length; i++)
				{
					if (this.streams[i] != null)
					{
						this.streams[i].Close();
					}
				}
			}

			// Token: 0x17002D09 RID: 11529
			// (get) Token: 0x0600B3E5 RID: 46053 RVA: 0x002484E3 File Offset: 0x002466E3
			public override long Length
			{
				get
				{
					if (!this.CanSeek)
					{
						throw new InvalidOperationException();
					}
					return this.length;
				}
			}

			// Token: 0x17002D0A RID: 11530
			// (get) Token: 0x0600B3E6 RID: 46054 RVA: 0x002484F9 File Offset: 0x002466F9
			// (set) Token: 0x0600B3E7 RID: 46055 RVA: 0x00248510 File Offset: 0x00246710
			public override long Position
			{
				get
				{
					if (!this.CanSeek)
					{
						throw new InvalidOperationException();
					}
					return this.position;
				}
				set
				{
					if (!this.CanSeek)
					{
						throw new InvalidOperationException();
					}
					this.currentStream = 0;
					while (this.currentStream < this.streams.Length)
					{
						long num = this.streams[this.currentStream].Length;
						if (value < num)
						{
							this.position += value;
							this.streams[this.currentStream].Position = value;
							return;
						}
						value -= num;
						this.position += num;
						this.currentStream++;
					}
				}
			}

			// Token: 0x0600B3E8 RID: 46056 RVA: 0x002485A0 File Offset: 0x002467A0
			public override int ReadByte()
			{
				while (this.currentStream < this.streams.Length)
				{
					int num = this.streams[this.currentStream].ReadByte();
					if (num != -1)
					{
						this.position += 1L;
						return num;
					}
					this.NextCurrentStream();
				}
				return -1;
			}

			// Token: 0x0600B3E9 RID: 46057 RVA: 0x002485F0 File Offset: 0x002467F0
			public override int Read(byte[] buffer, int offset, int count)
			{
				while (this.currentStream < this.streams.Length)
				{
					int num = this.streams[this.currentStream].Read(buffer, offset, count);
					if (num > 0)
					{
						this.position += (long)num;
						return num;
					}
					this.NextCurrentStream();
				}
				return 0;
			}

			// Token: 0x0600B3EA RID: 46058 RVA: 0x00248644 File Offset: 0x00246844
			public override long Seek(long offset, SeekOrigin origin)
			{
				switch (origin)
				{
				case SeekOrigin.Begin:
					this.Position = offset;
					break;
				case SeekOrigin.Current:
					this.Position += offset;
					break;
				case SeekOrigin.End:
					this.Position = this.Length - offset;
					break;
				default:
					throw new InvalidOperationException();
				}
				return this.Position;
			}

			// Token: 0x0600B3EB RID: 46059 RVA: 0x0024869A File Offset: 0x0024689A
			private void NextCurrentStream()
			{
				if (!this.CanSeek)
				{
					this.streams[this.currentStream].Close();
					this.streams[this.currentStream] = null;
				}
				this.currentStream++;
			}

			// Token: 0x04005B9E RID: 23454
			private Stream[] streams;

			// Token: 0x04005B9F RID: 23455
			private long length;

			// Token: 0x04005BA0 RID: 23456
			private long position;

			// Token: 0x04005BA1 RID: 23457
			private int currentStream;
		}

		// Token: 0x02001C25 RID: 7205
		private sealed class NotifyingStream : DelegatingStream
		{
			// Token: 0x0600B3EC RID: 46060 RVA: 0x002486D2 File Offset: 0x002468D2
			public NotifyingStream(Stream stream, Action callback)
				: base(stream)
			{
				this.callback = callback;
			}

			// Token: 0x0600B3ED RID: 46061 RVA: 0x002486E2 File Offset: 0x002468E2
			public override void Close()
			{
				this.HandleCallback();
				base.Close();
			}

			// Token: 0x0600B3EE RID: 46062 RVA: 0x002486F0 File Offset: 0x002468F0
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.HandleCallback();
				}
				base.Dispose(disposing);
			}

			// Token: 0x0600B3EF RID: 46063 RVA: 0x00248702 File Offset: 0x00246902
			private void HandleCallback()
			{
				if (this.callback != null)
				{
					Action action = this.callback;
					this.callback = null;
					action();
				}
			}

			// Token: 0x04005BA2 RID: 23458
			private Action callback;
		}

		// Token: 0x02001C26 RID: 7206
		private class ErrorTranslatingStream : Stream
		{
			// Token: 0x0600B3F0 RID: 46064 RVA: 0x0024871E File Offset: 0x0024691E
			public ErrorTranslatingStream(Stream stream, Func<Exception, Exception> translateError)
			{
				this.stream = stream;
				this.translateError = translateError;
			}

			// Token: 0x17002D0B RID: 11531
			// (get) Token: 0x0600B3F1 RID: 46065 RVA: 0x00248734 File Offset: 0x00246934
			public override bool CanRead
			{
				get
				{
					bool canRead;
					try
					{
						canRead = this.stream.CanRead;
					}
					catch (Exception ex)
					{
						this.TranslateException(ex);
						throw;
					}
					return canRead;
				}
			}

			// Token: 0x17002D0C RID: 11532
			// (get) Token: 0x0600B3F2 RID: 46066 RVA: 0x0024876C File Offset: 0x0024696C
			public override bool CanSeek
			{
				get
				{
					bool canSeek;
					try
					{
						canSeek = this.stream.CanSeek;
					}
					catch (Exception ex)
					{
						this.TranslateException(ex);
						throw;
					}
					return canSeek;
				}
			}

			// Token: 0x17002D0D RID: 11533
			// (get) Token: 0x0600B3F3 RID: 46067 RVA: 0x002487A4 File Offset: 0x002469A4
			public override bool CanWrite
			{
				get
				{
					bool canWrite;
					try
					{
						canWrite = this.stream.CanWrite;
					}
					catch (Exception ex)
					{
						this.TranslateException(ex);
						throw;
					}
					return canWrite;
				}
			}

			// Token: 0x17002D0E RID: 11534
			// (get) Token: 0x0600B3F4 RID: 46068 RVA: 0x002487DC File Offset: 0x002469DC
			public override long Length
			{
				get
				{
					long length;
					try
					{
						length = this.stream.Length;
					}
					catch (Exception ex)
					{
						this.TranslateException(ex);
						throw;
					}
					return length;
				}
			}

			// Token: 0x17002D0F RID: 11535
			// (get) Token: 0x0600B3F5 RID: 46069 RVA: 0x00248814 File Offset: 0x00246A14
			// (set) Token: 0x0600B3F6 RID: 46070 RVA: 0x0024884C File Offset: 0x00246A4C
			public override long Position
			{
				get
				{
					long position;
					try
					{
						position = this.stream.Position;
					}
					catch (Exception ex)
					{
						this.TranslateException(ex);
						throw;
					}
					return position;
				}
				set
				{
					try
					{
						this.stream.Position = value;
					}
					catch (Exception ex)
					{
						this.TranslateException(ex);
						throw;
					}
				}
			}

			// Token: 0x0600B3F7 RID: 46071 RVA: 0x00248884 File Offset: 0x00246A84
			public override void Close()
			{
				try
				{
					this.stream.Close();
				}
				catch (Exception ex)
				{
					this.TranslateException(ex);
					throw;
				}
			}

			// Token: 0x0600B3F8 RID: 46072 RVA: 0x002488B8 File Offset: 0x00246AB8
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					try
					{
						this.stream.Dispose();
					}
					catch (Exception ex)
					{
						this.TranslateException(ex);
						throw;
					}
				}
			}

			// Token: 0x0600B3F9 RID: 46073 RVA: 0x002488F0 File Offset: 0x00246AF0
			public override void Flush()
			{
				try
				{
					this.stream.Flush();
				}
				catch (Exception ex)
				{
					this.TranslateException(ex);
					throw;
				}
			}

			// Token: 0x0600B3FA RID: 46074 RVA: 0x00248924 File Offset: 0x00246B24
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num;
				try
				{
					num = this.stream.Read(buffer, offset, count);
				}
				catch (Exception ex)
				{
					this.TranslateException(ex);
					throw;
				}
				return num;
			}

			// Token: 0x0600B3FB RID: 46075 RVA: 0x00248960 File Offset: 0x00246B60
			public override int ReadByte()
			{
				int num;
				try
				{
					num = this.stream.ReadByte();
				}
				catch (Exception ex)
				{
					this.TranslateException(ex);
					throw;
				}
				return num;
			}

			// Token: 0x0600B3FC RID: 46076 RVA: 0x00248998 File Offset: 0x00246B98
			public override long Seek(long offset, SeekOrigin origin)
			{
				long num;
				try
				{
					num = this.stream.Seek(offset, origin);
				}
				catch (Exception ex)
				{
					this.TranslateException(ex);
					throw;
				}
				return num;
			}

			// Token: 0x0600B3FD RID: 46077 RVA: 0x002489D0 File Offset: 0x00246BD0
			public override void SetLength(long value)
			{
				try
				{
					this.stream.SetLength(value);
				}
				catch (Exception ex)
				{
					this.TranslateException(ex);
					throw;
				}
			}

			// Token: 0x0600B3FE RID: 46078 RVA: 0x00248A08 File Offset: 0x00246C08
			public override void Write(byte[] buffer, int offset, int count)
			{
				try
				{
					this.stream.Write(buffer, offset, count);
				}
				catch (Exception ex)
				{
					this.TranslateException(ex);
					throw;
				}
			}

			// Token: 0x0600B3FF RID: 46079 RVA: 0x00248A40 File Offset: 0x00246C40
			public override void WriteByte(byte value)
			{
				try
				{
					this.stream.WriteByte(value);
				}
				catch (Exception ex)
				{
					this.TranslateException(ex);
					throw;
				}
			}

			// Token: 0x0600B400 RID: 46080 RVA: 0x00248A78 File Offset: 0x00246C78
			private void TranslateException(Exception e)
			{
				if (SafeExceptions.IsSafeException(e))
				{
					Exception ex = this.translateError(e);
					if (e != ex)
					{
						throw ex;
					}
				}
			}

			// Token: 0x04005BA3 RID: 23459
			private readonly Stream stream;

			// Token: 0x04005BA4 RID: 23460
			private readonly Func<Exception, Exception> translateError;
		}

		// Token: 0x02001C27 RID: 7207
		private sealed class NonDisposableStream : DelegatingStream
		{
			// Token: 0x0600B401 RID: 46081 RVA: 0x0000FF57 File Offset: 0x0000E157
			public NonDisposableStream(Stream stream)
				: base(stream)
			{
			}

			// Token: 0x0600B402 RID: 46082 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Close()
			{
			}

			// Token: 0x0600B403 RID: 46083 RVA: 0x0000336E File Offset: 0x0000156E
			protected override void Dispose(bool disposing)
			{
			}
		}
	}
}
