using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200019F RID: 415
	public sealed class ChunkedTrafficParser
	{
		// Token: 0x06000AA2 RID: 2722 RVA: 0x00024A70 File Offset: 0x00022C70
		public static byte[] StitchChunkedBody(byte[] body, bool newlineTerminatedChunks)
		{
			Ensure.IsNotNull<byte[]>(body, "body");
			List<byte[]> list = ChunkedTrafficParser.SplitChunkedResponse(body, newlineTerminatedChunks);
			int num = 0;
			foreach (byte[] array in list)
			{
				num += array.Length;
			}
			body = new byte[num];
			using (Stream stream = new MemoryStream(body))
			{
				foreach (byte[] array2 in list)
				{
					stream.Write(array2, 0, array2.Length);
				}
			}
			return body;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00024B40 File Offset: 0x00022D40
		public static List<byte[]> SplitChunkedResponse(byte[] body, bool newlineTerminatedChunks)
		{
			Ensure.IsNotNull<byte[]>(body, "body");
			Stream stream = new MemoryStream(body);
			List<byte[]> list = new List<byte[]>();
			for (int i = ChunkedTrafficParser.GetLengthOfNextChunk(stream); i > 0; i = ChunkedTrafficParser.GetLengthOfNextChunk(stream))
			{
				byte[] array = new byte[i];
				int num = stream.Read(array, 0, i);
				if (num != i)
				{
					TraceSourceBase<CommonTrace>.Tracer.TraceInformation("Parsing error hit- bytes read not equal to chunk size. Dumping next 50 bytes in the stream : " + ChunkedTrafficParser.GetRemainderOfStreamMax50Bytes(stream));
					throw new FormatException(string.Concat(new object[] { "Parse failed. Length of next chunk should be ", i, ". Actual bytes read: ", num }));
				}
				list.Add(array);
				if (newlineTerminatedChunks)
				{
					string text = stream.ReadUntil(ChunkedTrafficParser.c_chunkedProtocolNewline);
					if (text != string.Empty)
					{
						TraceSourceBase<CommonTrace>.Tracer.TraceInformation("Parsing error hit- length should be on its own line. Dumping next 50 bytes in the stream : " + ChunkedTrafficParser.GetRemainderOfStreamMax50Bytes(stream));
						throw new FormatException("Parse sanity check failed during chunked response parsing. Length should be on its own line; content must have been read instead.\n Expected new line (\r\n). Received: '" + text + "'");
					}
				}
				else
				{
					int num2 = stream.ReadByte();
					if (num2 != 0)
					{
						TraceSourceBase<CommonTrace>.Tracer.TraceInformation("Parsing error hit- invalid chunk-terminating character. Dumping next 50 bytes in the stream : " + ChunkedTrafficParser.GetRemainderOfStreamMax50Bytes(stream));
						throw new FormatException("Parse sanity check failed during chunked response parsing. The byte following chunk != '\\0' nor '\\r'. #char recieved:" + num2);
					}
				}
			}
			return list;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00024C78 File Offset: 0x00022E78
		private static string GetRemainderOfStreamMax50Bytes(Stream responseStream)
		{
			Ensure.IsNotNull<Stream>(responseStream, "responseStream");
			byte[] array = new byte[50];
			int num = responseStream.Read(array, 0, 50);
			return Encoding.UTF8.GetString(array, 0, num);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00024CB0 File Offset: 0x00022EB0
		private static int GetLengthOfNextChunk(Stream responseStream)
		{
			Ensure.IsNotNull<Stream>(responseStream, "responseStream");
			if (!responseStream.CanRead)
			{
				return 0;
			}
			string text = responseStream.ReadUntil(ChunkedTrafficParser.c_chunkedProtocolNewline).TrimStart(new char[0]);
			if (text.Length >= 100)
			{
				TraceSourceBase<CommonTrace>.Tracer.TraceInformation("Parsing error hit- attempt to determine next chunk length failed. Dumping next 50 bytes in the stream : " + ChunkedTrafficParser.GetRemainderOfStreamMax50Bytes(responseStream));
				throw new FormatException("Parse sanity check failed during response parsing. Next-chunk-length should have < 100 characters. Content read as length: " + text);
			}
			if (string.IsNullOrEmpty(text))
			{
				return 0;
			}
			return int.Parse(text, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
		}

		// Token: 0x04000435 RID: 1077
		private static readonly string c_chunkedProtocolNewline = new string(new char[] { '\r', '\n' });
	}
}
