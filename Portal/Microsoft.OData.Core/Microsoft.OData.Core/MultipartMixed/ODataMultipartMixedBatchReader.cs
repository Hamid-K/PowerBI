using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Microsoft.OData.MultipartMixed
{
	// Token: 0x02000206 RID: 518
	internal sealed class ODataMultipartMixedBatchReader : ODataBatchReader
	{
		// Token: 0x060016C7 RID: 5831 RVA: 0x0003FB99 File Offset: 0x0003DD99
		internal ODataMultipartMixedBatchReader(ODataMultipartMixedBatchInputContext inputContext, string batchBoundary, Encoding batchEncoding, bool synchronous)
			: base(inputContext, synchronous)
		{
			this.batchStream = new ODataMultipartMixedBatchReaderStream(this.MultipartMixedBatchInputContext, batchBoundary, batchEncoding);
			this.dependsOnIdsTracker = new DependsOnIdsTracker();
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x0003FBC2 File Offset: 0x0003DDC2
		private ODataMultipartMixedBatchInputContext MultipartMixedBatchInputContext
		{
			get
			{
				return base.InputContext as ODataMultipartMixedBatchInputContext;
			}
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0003FBD0 File Offset: 0x0003DDD0
		protected override ODataBatchOperationRequestMessage CreateOperationRequestMessageImplementation()
		{
			string text = this.batchStream.ReadFirstNonEmptyLine();
			string text2;
			Uri uri;
			this.ParseRequestLine(text, out text2, out uri);
			ODataBatchOperationHeaders headers = this.batchStream.ReadHeaders();
			if (this.batchStream.ChangeSetBoundary != null)
			{
				if (this.currentContentId == null)
				{
					headers.TryGetValue("Content-ID", out this.currentContentId);
					if (this.currentContentId == null)
					{
						throw new ODataException(Strings.ODataBatchOperationHeaderDictionary_KeyNotFound("Content-ID"));
					}
				}
			}
			else if (base.InputContext.MessageReaderSettings.Version <= ODataVersion.V4)
			{
				this.PayloadUriConverter.Reset();
			}
			ODataBatchOperationRequestMessage odataBatchOperationRequestMessage = base.BuildOperationRequestMessage(() => ODataBatchUtils.CreateBatchOperationReadStream(this.batchStream, headers, this), text2, uri, headers, this.currentContentId, this.batchStream.ChangeSetBoundary, this.dependsOnIdsTracker.GetDependsOnIds(), false);
			if (this.currentContentId != null)
			{
				this.dependsOnIdsTracker.AddDependsOnId(this.currentContentId);
			}
			this.currentContentId = null;
			return odataBatchOperationRequestMessage;
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0003FCE8 File Offset: 0x0003DEE8
		protected override ODataBatchOperationResponseMessage CreateOperationResponseMessageImplementation()
		{
			string text = this.batchStream.ReadFirstNonEmptyLine();
			int num = this.ParseResponseLine(text);
			ODataBatchOperationHeaders headers = this.batchStream.ReadHeaders();
			if (this.currentContentId == null)
			{
				headers.TryGetValue("Content-ID", out this.currentContentId);
			}
			ODataBatchOperationResponseMessage odataBatchOperationResponseMessage = base.BuildOperationResponseMessage(() => ODataBatchUtils.CreateBatchOperationReadStream(this.batchStream, headers, this), num, headers, this.currentContentId, null);
			this.currentContentId = null;
			return odataBatchOperationResponseMessage;
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x0003FD6F File Offset: 0x0003DF6F
		protected override ODataBatchReaderState ReadAtStartImplementation()
		{
			return this.SkipToNextPartAndReadHeaders();
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x0003FD6F File Offset: 0x0003DF6F
		protected override ODataBatchReaderState ReadAtOperationImplementation()
		{
			return this.SkipToNextPartAndReadHeaders();
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0003FD77 File Offset: 0x0003DF77
		protected override ODataBatchReaderState ReadAtChangesetStartImplementation()
		{
			if (this.batchStream.ChangeSetBoundary == null)
			{
				base.ThrowODataException(Strings.ODataBatchReader_ReaderStreamChangesetBoundaryCannotBeNull);
			}
			this.dependsOnIdsTracker.ChangeSetStarted();
			return this.SkipToNextPartAndReadHeaders();
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0003FDA2 File Offset: 0x0003DFA2
		protected override ODataBatchReaderState ReadAtChangesetEndImplementation()
		{
			this.batchStream.ResetChangeSetBoundary();
			this.dependsOnIdsTracker.ChangeSetEnded();
			return this.SkipToNextPartAndReadHeaders();
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0003FDC0 File Offset: 0x0003DFC0
		private ODataBatchReaderState GetEndBoundaryState()
		{
			switch (base.State)
			{
			case ODataBatchReaderState.Initial:
				return ODataBatchReaderState.Completed;
			case ODataBatchReaderState.Operation:
				if (this.batchStream.ChangeSetBoundary != null)
				{
					return ODataBatchReaderState.ChangesetEnd;
				}
				return ODataBatchReaderState.Completed;
			case ODataBatchReaderState.ChangesetStart:
				return ODataBatchReaderState.ChangesetEnd;
			case ODataBatchReaderState.ChangesetEnd:
				return ODataBatchReaderState.Completed;
			case ODataBatchReaderState.Completed:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_GetEndBoundary_Completed));
			case ODataBatchReaderState.Exception:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_GetEndBoundary_Exception));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_GetEndBoundary_UnknownValue));
			}
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0003FE40 File Offset: 0x0003E040
		private void ParseRequestLine(string requestLine, out string httpMethod, out Uri requestUri)
		{
			int num = requestLine.IndexOf(' ');
			if (num <= 0 || requestLine.Length - 3 <= num)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidRequestLine(requestLine));
			}
			int num2 = requestLine.LastIndexOf(' ');
			if (num2 < 0 || num2 - num - 1 <= 0 || requestLine.Length - 1 <= num2)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidRequestLine(requestLine));
			}
			httpMethod = requestLine.Substring(0, num);
			string text = requestLine.Substring(num + 1, num2 - num - 1);
			string text2 = requestLine.Substring(num2 + 1);
			if (string.CompareOrdinal("HTTP/1.1", text2) != 0)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidHttpVersionSpecified(text2, "HTTP/1.1"));
			}
			HttpUtils.ValidateHttpMethod(httpMethod);
			if (this.batchStream.ChangeSetBoundary != null && HttpUtils.IsQueryMethod(httpMethod))
			{
				throw new ODataException(Strings.ODataBatch_InvalidHttpMethodForChangeSetRequest(httpMethod));
			}
			requestUri = new Uri(text, UriKind.RelativeOrAbsolute);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x0003FF14 File Offset: 0x0003E114
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "'this' is used when built in debug")]
		private int ParseResponseLine(string responseLine)
		{
			int num = responseLine.IndexOf(' ');
			if (num <= 0 || responseLine.Length - 3 <= num)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidResponseLine(responseLine));
			}
			int num2 = responseLine.IndexOf(' ', num + 1);
			if (num2 < 0 || num2 - num - 1 <= 0 || responseLine.Length - 1 <= num2)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidResponseLine(responseLine));
			}
			string text = responseLine.Substring(0, num);
			string text2 = responseLine.Substring(num + 1, num2 - num - 1);
			if (string.CompareOrdinal("HTTP/1.1", text) != 0)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidHttpVersionSpecified(text, "HTTP/1.1"));
			}
			int num3;
			if (!int.TryParse(text2, out num3))
			{
				throw new ODataException(Strings.ODataBatchReaderStream_NonIntegerHttpStatusCode(text2));
			}
			return num3;
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0003FFC4 File Offset: 0x0003E1C4
		private ODataBatchReaderState SkipToNextPartAndReadHeaders()
		{
			bool flag;
			bool flag2;
			if (this.batchStream.SkipToBoundary(out flag, out flag2))
			{
				ODataBatchReaderState odataBatchReaderState;
				if (flag || flag2)
				{
					odataBatchReaderState = this.GetEndBoundaryState();
				}
				else
				{
					bool flag3 = this.batchStream.ChangeSetBoundary != null;
					bool flag4 = this.batchStream.ProcessPartHeader(out this.currentContentId);
					if (flag3)
					{
						odataBatchReaderState = ODataBatchReaderState.Operation;
					}
					else
					{
						odataBatchReaderState = (flag4 ? ODataBatchReaderState.ChangesetStart : ODataBatchReaderState.Operation);
					}
				}
				return odataBatchReaderState;
			}
			if (this.batchStream.ChangeSetBoundary == null)
			{
				return ODataBatchReaderState.Completed;
			}
			return ODataBatchReaderState.ChangesetEnd;
		}

		// Token: 0x04000A4D RID: 2637
		private readonly ODataMultipartMixedBatchReaderStream batchStream;

		// Token: 0x04000A4E RID: 2638
		private readonly DependsOnIdsTracker dependsOnIdsTracker;

		// Token: 0x04000A4F RID: 2639
		private string currentContentId;
	}
}
