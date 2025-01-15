using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.OData.Core
{
	// Token: 0x02000138 RID: 312
	public sealed class ODataAsynchronousReader
	{
		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002CC4A File Offset: 0x0002AE4A
		internal ODataAsynchronousReader(ODataRawInputContext rawInputContext, Encoding encoding)
		{
			if (encoding != null)
			{
				ReaderValidationUtils.ValidateEncodingSupportedInAsync(encoding);
			}
			this.rawInputContext = rawInputContext;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002CC62 File Offset: 0x0002AE62
		public ODataAsynchronousResponseMessage CreateResponseMessage()
		{
			this.VerifyCanCreateResponseMessage(true);
			return this.CreateResponseMessageImplementation();
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002CC71 File Offset: 0x0002AE71
		private void ValidateReaderNotDisposed()
		{
			this.rawInputContext.VerifyNotDisposed();
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0002CC7E File Offset: 0x0002AE7E
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.rawInputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataAsyncReader_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002CC9B File Offset: 0x0002AE9B
		private void VerifyCanCreateResponseMessage(bool synchronousCall)
		{
			this.ValidateReaderNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (!this.rawInputContext.ReadingResponse)
			{
				throw new ODataException(Strings.ODataAsyncReader_CannotCreateResponseWhenNotReadingResponse);
			}
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002CCC4 File Offset: 0x0002AEC4
		private ODataAsynchronousResponseMessage CreateResponseMessageImplementation()
		{
			int num;
			IDictionary<string, string> dictionary;
			this.ReadInnerEnvelope(out num, out dictionary);
			return ODataAsynchronousResponseMessage.CreateMessageForReading(this.rawInputContext.Stream, num, dictionary);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002CCF0 File Offset: 0x0002AEF0
		private void ReadInnerEnvelope(out int statusCode, out IDictionary<string, string> headers)
		{
			string text = this.ReadFirstNonEmptyLine();
			statusCode = this.ParseResponseLine(text);
			headers = this.ReadHeaders();
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002CD18 File Offset: 0x0002AF18
		private string ReadFirstNonEmptyLine()
		{
			string text = this.ReadLine();
			while (text.Length == 0)
			{
				text = this.ReadLine();
			}
			return text;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002CD40 File Offset: 0x0002AF40
		private int ParseResponseLine(string responseLine)
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

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002CE04 File Offset: 0x0002B004
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

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002CE5C File Offset: 0x0002B05C
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

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002CEA4 File Offset: 0x0002B0A4
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

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002CF1F File Offset: 0x0002B11F
		private int ReadByte()
		{
			return this.rawInputContext.Stream.ReadByte();
		}

		// Token: 0x040004F5 RID: 1269
		private readonly ODataRawInputContext rawInputContext;
	}
}
