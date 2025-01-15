using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x02000052 RID: 82
	public sealed class ODataAsynchronousReader
	{
		// Token: 0x06000283 RID: 643 RVA: 0x00007D5A File Offset: 0x00005F5A
		internal ODataAsynchronousReader(ODataRawInputContext rawInputContext, Encoding encoding)
		{
			if (encoding != null)
			{
				ReaderValidationUtils.ValidateEncodingSupportedInAsync(encoding);
			}
			this.rawInputContext = rawInputContext;
			this.container = rawInputContext.Container;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007D7E File Offset: 0x00005F7E
		public ODataAsynchronousResponseMessage CreateResponseMessage()
		{
			this.VerifyCanCreateResponseMessage(true);
			return this.CreateResponseMessageImplementation();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00007D8D File Offset: 0x00005F8D
		public Task<ODataAsynchronousResponseMessage> CreateResponseMessageAsync()
		{
			this.VerifyCanCreateResponseMessage(false);
			return TaskUtils.GetTaskForSynchronousOperation<ODataAsynchronousResponseMessage>(() => this.CreateResponseMessageImplementation());
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00007DA7 File Offset: 0x00005FA7
		private void ValidateReaderNotDisposed()
		{
			this.rawInputContext.VerifyNotDisposed();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007DB4 File Offset: 0x00005FB4
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				if (!this.rawInputContext.Synchronous)
				{
					throw new ODataException(Strings.ODataAsyncReader_SyncCallOnAsyncReader);
				}
			}
			else if (this.rawInputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataAsyncReader_AsyncCallOnSyncReader);
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00007DE9 File Offset: 0x00005FE9
		private void VerifyCanCreateResponseMessage(bool synchronousCall)
		{
			this.ValidateReaderNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (!this.rawInputContext.ReadingResponse)
			{
				throw new ODataException(Strings.ODataAsyncReader_CannotCreateResponseWhenNotReadingResponse);
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00007E10 File Offset: 0x00006010
		private ODataAsynchronousResponseMessage CreateResponseMessageImplementation()
		{
			int num;
			IDictionary<string, string> dictionary;
			this.ReadInnerEnvelope(out num, out dictionary);
			return ODataAsynchronousResponseMessage.CreateMessageForReading(this.rawInputContext.Stream, num, dictionary, this.container);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00007E40 File Offset: 0x00006040
		private void ReadInnerEnvelope(out int statusCode, out IDictionary<string, string> headers)
		{
			string text = this.ReadFirstNonEmptyLine();
			statusCode = ODataAsynchronousReader.ParseResponseLine(text);
			headers = this.ReadHeaders();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00007E64 File Offset: 0x00006064
		private string ReadFirstNonEmptyLine()
		{
			string text = this.ReadLine();
			while (text.Length == 0)
			{
				text = this.ReadLine();
			}
			return text;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007E8C File Offset: 0x0000608C
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
			if (!int.TryParse(text2, out num3))
			{
				throw new ODataException(Strings.ODataAsyncReader_NonIntegerHttpStatusCode(text2));
			}
			return num3;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00007F50 File Offset: 0x00006150
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

		// Token: 0x0600028E RID: 654 RVA: 0x00007FA8 File Offset: 0x000061A8
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

		// Token: 0x0600028F RID: 655 RVA: 0x00007FF0 File Offset: 0x000061F0
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

		// Token: 0x06000290 RID: 656 RVA: 0x0000806B File Offset: 0x0000626B
		private int ReadByte()
		{
			return this.rawInputContext.Stream.ReadByte();
		}

		// Token: 0x0400012E RID: 302
		private readonly ODataRawInputContext rawInputContext;

		// Token: 0x0400012F RID: 303
		private readonly IServiceProvider container;
	}
}
