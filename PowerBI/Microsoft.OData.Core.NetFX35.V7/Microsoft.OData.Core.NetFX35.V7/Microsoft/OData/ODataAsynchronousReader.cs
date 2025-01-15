using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData
{
	// Token: 0x0200002A RID: 42
	public sealed class ODataAsynchronousReader
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00005200 File Offset: 0x00003400
		internal ODataAsynchronousReader(ODataRawInputContext rawInputContext, Encoding encoding)
		{
			if (encoding != null)
			{
				ReaderValidationUtils.ValidateEncodingSupportedInAsync(encoding);
			}
			this.rawInputContext = rawInputContext;
			this.container = rawInputContext.Container;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005224 File Offset: 0x00003424
		public ODataAsynchronousResponseMessage CreateResponseMessage()
		{
			this.VerifyCanCreateResponseMessage(true);
			return this.CreateResponseMessageImplementation();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005233 File Offset: 0x00003433
		private void ValidateReaderNotDisposed()
		{
			this.rawInputContext.VerifyNotDisposed();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005240 File Offset: 0x00003440
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.rawInputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataAsyncReader_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000525D File Offset: 0x0000345D
		private void VerifyCanCreateResponseMessage(bool synchronousCall)
		{
			this.ValidateReaderNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (!this.rawInputContext.ReadingResponse)
			{
				throw new ODataException(Strings.ODataAsyncReader_CannotCreateResponseWhenNotReadingResponse);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005284 File Offset: 0x00003484
		private ODataAsynchronousResponseMessage CreateResponseMessageImplementation()
		{
			int num;
			IDictionary<string, string> dictionary;
			this.ReadInnerEnvelope(out num, out dictionary);
			return ODataAsynchronousResponseMessage.CreateMessageForReading(this.rawInputContext.Stream, num, dictionary, this.container);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000052B4 File Offset: 0x000034B4
		private void ReadInnerEnvelope(out int statusCode, out IDictionary<string, string> headers)
		{
			string text = this.ReadFirstNonEmptyLine();
			statusCode = ODataAsynchronousReader.ParseResponseLine(text);
			headers = this.ReadHeaders();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000052D8 File Offset: 0x000034D8
		private string ReadFirstNonEmptyLine()
		{
			string text = this.ReadLine();
			while (text.Length == 0)
			{
				text = this.ReadLine();
			}
			return text;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005300 File Offset: 0x00003500
		private static int ParseResponseLine(string responseLine)
		{
			if (responseLine.Length == 0)
			{
				throw new ODataException(Strings.ODataAsyncReader_InvalidResponseLine(responseLine));
			}
			int num = responseLine.IndexOf(' ');
			if (num <= 0 || responseLine.Length - 3 <= num)
			{
				throw new ODataException(Strings.ODataAsyncReader_InvalidResponseLine(responseLine));
			}
			int num2 = responseLine.IndexOf(' ', num + 1);
			if (num2 < 0 || num2 - num - 1 <= 0 || responseLine.Length - 1 <= num2)
			{
				throw new ODataException(Strings.ODataAsyncReader_InvalidResponseLine(responseLine));
			}
			string text = responseLine.Substring(0, num);
			string text2 = responseLine.Substring(num + 1, num2 - num - 1);
			if (string.CompareOrdinal("HTTP/1.1", text) != 0)
			{
				throw new ODataException(Strings.ODataAsyncReader_InvalidHttpVersionSpecified(text, "HTTP/1.1"));
			}
			int num3;
			if (!int.TryParse(text2, ref num3))
			{
				throw new ODataException(Strings.ODataAsyncReader_NonIntegerHttpStatusCode(text2));
			}
			return num3;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000053C4 File Offset: 0x000035C4
		private IDictionary<string, string> ReadHeaders()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
			string text = this.ReadLine();
			while (!string.IsNullOrEmpty(text))
			{
				string text2;
				string text3;
				ODataAsynchronousReader.ValidateHeaderLine(text, out text2, out text3);
				if (dictionary.ContainsKey(text2))
				{
					throw new ODataException(Strings.ODataAsyncReader_DuplicateHeaderFound(text2));
				}
				dictionary.Add(text2, text3);
				text = this.ReadLine();
			}
			return dictionary;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000541C File Offset: 0x0000361C
		private static void ValidateHeaderLine(string headerLine, out string headerName, out string headerValue)
		{
			int num = headerLine.IndexOf(':');
			if (num <= 0)
			{
				throw new ODataException(Strings.ODataAsyncReader_InvalidHeaderSpecified(headerLine));
			}
			headerName = headerLine.Substring(0, num).Trim();
			headerValue = headerLine.Substring(num + 1).Trim();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005464 File Offset: 0x00003664
		private string ReadLine()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.ReadByte();
			while (num != -1)
			{
				if (num == 10)
				{
					throw new ODataException(Strings.ODataAsyncReader_InvalidNewLineEncountered('\n'));
				}
				if (num == 13)
				{
					num = this.ReadByte();
					if (num != 10)
					{
						throw new ODataException(Strings.ODataAsyncReader_InvalidNewLineEncountered('\r'));
					}
					return stringBuilder.ToString();
				}
				else
				{
					stringBuilder.Append((char)num);
					num = this.ReadByte();
				}
			}
			throw new ODataException(Strings.ODataAsyncReader_UnexpectedEndOfInput);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000054DF File Offset: 0x000036DF
		private int ReadByte()
		{
			return this.rawInputContext.Stream.ReadByte();
		}

		// Token: 0x040000C4 RID: 196
		private readonly ODataRawInputContext rawInputContext;

		// Token: 0x040000C5 RID: 197
		private readonly IServiceProvider container;
	}
}
