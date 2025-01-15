using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000295 RID: 661
	public static class ExtendedStream
	{
		// Token: 0x060011B5 RID: 4533 RVA: 0x0003DF10 File Offset: 0x0003C110
		public static bool StartsWith<T>(this T[] lhs, int position, T[] rhs) where T : struct, IEquatable<T>
		{
			if (lhs.Length - position < rhs.Length)
			{
				return false;
			}
			int i = 0;
			while (i < rhs.Length)
			{
				if (!lhs[position].Equals(rhs[i]))
				{
					return false;
				}
				i++;
				position++;
			}
			return true;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0003DF5C File Offset: 0x0003C15C
		public static Encoding GuessEncoding([NotNull] byte[] buffer, int position, out int posFirstNonBomByte)
		{
			Ensure.ArgNotNull<byte[]>(buffer, "buffer");
			Ensure.ArgIsInRange<int>(position, 0, buffer.Length - 2, "position", 0);
			posFirstNonBomByte = position;
			Encoding utf = Encoding.UTF8;
			byte b = buffer[position];
			byte b2 = buffer[position + 1];
			if (buffer.StartsWith(position, ExtendedStream.s_bomUTF16LE))
			{
				posFirstNonBomByte += ExtendedStream.s_bomUTF16LE.Length;
				return Encoding.Unicode;
			}
			if (buffer.StartsWith(position, ExtendedStream.s_bomUTF16BE))
			{
				posFirstNonBomByte += ExtendedStream.s_bomUTF16BE.Length;
				return Encoding.BigEndianUnicode;
			}
			if (buffer.StartsWith(position, ExtendedStream.s_bomUTF8))
			{
				posFirstNonBomByte += ExtendedStream.s_bomUTF8.Length;
				return Encoding.UTF8;
			}
			if (b == 0 && b2 != 0)
			{
				return Encoding.BigEndianUnicode;
			}
			if (b != 0 && b2 == 0)
			{
				return Encoding.Unicode;
			}
			if (b == 0 && b2 == 0)
			{
				ExtendedDiagnostics.EnsureArgument("buffer", false, "buffer encoding detected as UTF-32BE, which is not supported");
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				if (buffer[i] > 127)
				{
					return Encoding.UTF8;
				}
			}
			return Encoding.UTF8;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0003E049 File Offset: 0x0003C249
		public static byte[] GetByteOrderMark([NotNull] this Encoding encoding)
		{
			Ensure.ArgNotNull<Encoding>(encoding, "encoding");
			return encoding.GetPreamble();
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0003E05C File Offset: 0x0003C25C
		public static void WriteBomToStream([NotNull] Stream stream, [NotNull] Encoding encoding)
		{
			Ensure.ArgNotNull<Stream>(stream, "stream");
			Ensure.ArgNotNull<Encoding>(encoding, "encoding");
			byte[] preamble = encoding.GetPreamble();
			if (preamble != null && preamble.Length != 0)
			{
				stream.Write(preamble, 0, preamble.Length);
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0003E098 File Offset: 0x0003C298
		public static bool AreEqual([NotNull] Stream stream1, [NotNull] Stream stream2)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Stream>(stream1, "stream1");
			ExtendedDiagnostics.EnsureArgumentNotNull<Stream>(stream2, "stream2");
			byte[] array = new byte[4096];
			byte[] array2 = new byte[4096];
			for (;;)
			{
				int num = stream1.Read(array, 0, 4096);
				int num2 = stream2.Read(array2, 0, 4096);
				if (num != num2)
				{
					break;
				}
				if (num == 0)
				{
					return true;
				}
				if (!array.Take(num).SequenceEqual(array2.Take(num2)))
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0003E10E File Offset: 0x0003C30E
		public static string ReadAll<T_Stream>(this T_Stream streamToRead, bool fromBeginning) where T_Stream : Stream
		{
			if (fromBeginning)
			{
				streamToRead.Position = 0L;
			}
			return new StreamReader(streamToRead).ReadToEnd();
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0003E130 File Offset: 0x0003C330
		public static Stream AsReadableStream(this string stringToStream)
		{
			return new MemoryStream(Encoding.UTF8.GetBytes(stringToStream), false);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0003E144 File Offset: 0x0003C344
		public static byte[] ReadAllBytes<T_Stream>(this T_Stream streamToRead, bool fromBeginning) where T_Stream : Stream
		{
			if (fromBeginning)
			{
				if (!streamToRead.CanSeek)
				{
					throw new InvalidOperationException("Stream to read cannot seek; reset cursor to beginning will fail.");
				}
				streamToRead.Position = 0L;
			}
			MemoryStream memoryStream = new MemoryStream();
			streamToRead.CopyTo(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0003E191 File Offset: 0x0003C391
		public static string ReadLine<T_Stream>(this T_Stream streamToRead, out bool newLineFound) where T_Stream : Stream
		{
			return streamToRead.ReadUntil(Environment.NewLine, out newLineFound);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0003E1A0 File Offset: 0x0003C3A0
		public static string ReadLine<T_Stream>(this T_Stream streamToRead) where T_Stream : Stream
		{
			bool flag;
			return streamToRead.ReadLine(out flag);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0003E1B8 File Offset: 0x0003C3B8
		public static string ReadUntil<T_Stream>(this T_Stream streamToRead, string delimiter, out bool delimiterFound) where T_Stream : Stream
		{
			StringBuilder substring = new StringBuilder();
			streamToRead.ReadUntil(delimiter, delegate(int c)
			{
				substring.Append((char)c);
			}, out delimiterFound);
			return substring.ToString();
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0003E1F8 File Offset: 0x0003C3F8
		public static string ReadUntil<T_Stream>(this T_Stream streamToRead, string delimiter) where T_Stream : Stream
		{
			StringBuilder substring = new StringBuilder();
			bool flag;
			streamToRead.ReadUntil(delimiter, delegate(int c)
			{
				substring.Append((char)c);
			}, out flag);
			return substring.ToString();
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0003E238 File Offset: 0x0003C438
		public static int ReadUntil<T_Stream>(this T_Stream streamToRead, string delimiter, T_Stream destinationStream, out bool delimiterFound) where T_Stream : Stream
		{
			Ensure.IsNotNull<T_Stream>(destinationStream, "destinationStream");
			return streamToRead.ReadUntil(delimiter, delegate(int c)
			{
				destinationStream.WriteByte((byte)c);
			}, out delimiterFound);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0003E278 File Offset: 0x0003C478
		public static int ReadUntil<T_Stream>(this T_Stream streamToRead, string delimiter, T_Stream destinationStream) where T_Stream : Stream
		{
			Ensure.IsNotNull<T_Stream>(destinationStream, "destinationStream");
			bool flag;
			return streamToRead.ReadUntil(delimiter, delegate(int c)
			{
				destinationStream.WriteByte((byte)c);
			}, out flag);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0003E2B7 File Offset: 0x0003C4B7
		private static int ReadUntil<T_Stream>(this T_Stream streamToRead, string delimiter, Action<int> writeByteToReceiver, out bool delimiterFound) where T_Stream : Stream
		{
			Ensure.IsNotNull<T_Stream>(streamToRead, "streamToRead");
			return streamToRead.ReadUntil(Encoding.UTF8.GetBytes(delimiter), delimiter.Length - 1, writeByteToReceiver, out delimiterFound);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0003E2E0 File Offset: 0x0003C4E0
		private static int ReadUntil<T_Stream>(this T_Stream streamToRead, [NotNull] byte[] delimiter, int delimiterIndex, Action<int> writeByteToReceiver, out bool delimiterFound) where T_Stream : Stream
		{
			Ensure.ArgNotNull<byte[]>(delimiter, "delimiter");
			Ensure.ArgIsPositive((long)delimiter.Length, "delimiter.length", 0);
			Ensure.ArgIsInRange<int>(delimiterIndex, 0, delimiter.Length - 1, "delimiterIndex", 0);
			int num = 0;
			delimiterFound = false;
			while (streamToRead.CanRead)
			{
				if (delimiterIndex > 0)
				{
					num += streamToRead.ReadUntil(delimiter, delimiterIndex - 1, writeByteToReceiver, out delimiterFound);
				}
				int num2 = streamToRead.ReadByte();
				if (num2 == (int)delimiter[delimiterIndex])
				{
					delimiterFound = true;
					break;
				}
				if (delimiterFound)
				{
					delimiterFound = false;
					for (int i = 0; i < delimiterIndex; i++)
					{
						writeByteToReceiver((int)delimiter[i]);
					}
					num += delimiterIndex;
				}
				if (num2 == -1)
				{
					break;
				}
				writeByteToReceiver(num2);
				num++;
			}
			return num;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0003E38C File Offset: 0x0003C58C
		public static async Task<string> ReadToEndAsync(this Stream stream)
		{
			return await stream.ReadToEndAsync(Encoding.UTF8);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0003E3D4 File Offset: 0x0003C5D4
		public static async Task<string> ReadToEndAsync(this Stream stream, Encoding encoding)
		{
			string text2;
			using (StreamReader textReader = new StreamReader(stream, encoding, false, 1024, true))
			{
				string text = await textReader.ReadToEndAsync();
				textReader.DiscardBufferedData();
				text2 = text;
			}
			return text2;
		}

		// Token: 0x0400069A RID: 1690
		private const int c_localReadBufferSize = 4096;

		// Token: 0x0400069B RID: 1691
		private static readonly byte[] s_bomUTF8 = Encoding.UTF8.GetPreamble();

		// Token: 0x0400069C RID: 1692
		private static readonly byte[] s_bomUTF16BE = Encoding.BigEndianUnicode.GetPreamble();

		// Token: 0x0400069D RID: 1693
		private static readonly byte[] s_bomUTF16LE = Encoding.Unicode.GetPreamble();
	}
}
