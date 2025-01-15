using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.OData.MultipartMixed
{
	// Token: 0x02000208 RID: 520
	internal sealed class ODataMultipartMixedBatchWriter : ODataBatchWriter
	{
		// Token: 0x060016E6 RID: 5862 RVA: 0x00040B1E File Offset: 0x0003ED1E
		internal ODataMultipartMixedBatchWriter(ODataMultipartMixedBatchOutputContext rawOutputContext, string batchBoundary)
			: base(rawOutputContext)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(batchBoundary, "batchBoundary is null");
			this.batchBoundary = batchBoundary;
			this.RawOutputContext.InitializeRawValueWriter();
			this.dependsOnIdsTracker = new DependsOnIdsTracker();
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x00040B50 File Offset: 0x0003ED50
		private ODataMultipartMixedBatchOutputContext RawOutputContext
		{
			get
			{
				return base.OutputContext as ODataMultipartMixedBatchOutputContext;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x00040B5D File Offset: 0x0003ED5D
		private ODataBatchOperationMessage CurrentOperationMessage
		{
			get
			{
				if (base.CurrentOperationRequestMessage != null)
				{
					return base.CurrentOperationRequestMessage.OperationMessage;
				}
				if (base.CurrentOperationResponseMessage != null)
				{
					return base.CurrentOperationResponseMessage.OperationMessage;
				}
				return null;
			}
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00040B88 File Offset: 0x0003ED88
		public override void StreamRequested()
		{
			this.StartBatchOperationContent();
			this.RawOutputContext.FlushBuffers();
			this.DisposeBatchWriterAndSetContentStreamRequestedState();
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00040BA1 File Offset: 0x0003EDA1
		public override Task StreamRequestedAsync()
		{
			this.StartBatchOperationContent();
			return this.RawOutputContext.FlushBuffersAsync().FollowOnSuccessWith(delegate(Task task)
			{
				this.DisposeBatchWriterAndSetContentStreamRequestedState();
			});
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00040BC5 File Offset: 0x0003EDC5
		public override void StreamDisposed()
		{
			base.SetState(ODataBatchWriter.BatchWriterState.OperationStreamDisposed);
			base.CurrentOperationRequestMessage = null;
			base.CurrentOperationResponseMessage = null;
			this.RawOutputContext.InitializeRawValueWriter();
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00040BE7 File Offset: 0x0003EDE7
		public override void OnInStreamError()
		{
			this.RawOutputContext.VerifyNotDisposed();
			base.SetState(ODataBatchWriter.BatchWriterState.Error);
			this.RawOutputContext.TextWriter.Flush();
			throw new ODataException(Strings.ODataBatchWriter_CannotWriteInStreamErrorForBatch);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00040C15 File Offset: 0x0003EE15
		protected override void FlushSynchronously()
		{
			this.RawOutputContext.Flush();
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00040C22 File Offset: 0x0003EE22
		protected override Task FlushAsynchronously()
		{
			return this.RawOutputContext.FlushAsync();
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00040C30 File Offset: 0x0003EE30
		protected override void WriteStartChangesetImplementation(string changeSetId)
		{
			this.WritePendingMessageData(true);
			base.SetState(ODataBatchWriter.BatchWriterState.ChangesetStarted);
			this.changeSetBoundary = ODataMultipartMixedBatchWriterUtils.CreateChangeSetBoundary(this.RawOutputContext.WritingResponse, changeSetId);
			ODataMultipartMixedBatchWriterUtils.WriteStartBoundary(this.RawOutputContext.TextWriter, this.batchBoundary, !this.batchStartBoundaryWritten);
			this.batchStartBoundaryWritten = true;
			ODataMultipartMixedBatchWriterUtils.WriteChangeSetPreamble(this.RawOutputContext.TextWriter, this.changeSetBoundary);
			this.changesetStartBoundaryWritten = false;
			this.dependsOnIdsTracker.ChangeSetStarted();
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00040CB0 File Offset: 0x0003EEB0
		protected override IEnumerable<string> GetDependsOnRequestIds(IEnumerable<string> dependsOnIds)
		{
			return dependsOnIds ?? this.dependsOnIdsTracker.GetDependsOnIds();
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00040CC4 File Offset: 0x0003EEC4
		protected override ODataBatchOperationRequestMessage CreateOperationRequestMessageImplementation(string method, Uri uri, string contentId, BatchPayloadUriOption payloadUriOption, IEnumerable<string> dependsOnIds)
		{
			this.WritePendingMessageData(true);
			ODataBatchOperationRequestMessage odataBatchOperationRequestMessage = base.BuildOperationRequestMessage(this.RawOutputContext.OutputStream, method, uri, contentId, this.changeSetBoundary, dependsOnIds);
			base.SetState(ODataBatchWriter.BatchWriterState.OperationCreated);
			this.WriteStartBoundaryForOperation();
			if (contentId != null)
			{
				this.dependsOnIdsTracker.AddDependsOnId(contentId);
			}
			ODataMultipartMixedBatchWriterUtils.WriteRequestPreamble(this.RawOutputContext.TextWriter, method, uri, this.RawOutputContext.MessageWriterSettings.BaseUri, this.changeSetBoundary != null, contentId, payloadUriOption);
			return odataBatchOperationRequestMessage;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00040D40 File Offset: 0x0003EF40
		protected override void WriteEndBatchImplementation()
		{
			this.WritePendingMessageData(true);
			base.SetState(ODataBatchWriter.BatchWriterState.BatchCompleted);
			ODataMultipartMixedBatchWriterUtils.WriteEndBoundary(this.RawOutputContext.TextWriter, this.batchBoundary, !this.batchStartBoundaryWritten);
			this.RawOutputContext.TextWriter.WriteLine();
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00040D80 File Offset: 0x0003EF80
		protected override void WriteEndChangesetImplementation()
		{
			this.WritePendingMessageData(true);
			string text = this.changeSetBoundary;
			base.SetState(ODataBatchWriter.BatchWriterState.ChangesetCompleted);
			this.dependsOnIdsTracker.ChangeSetEnded();
			this.changeSetBoundary = null;
			ODataMultipartMixedBatchWriterUtils.WriteEndBoundary(this.RawOutputContext.TextWriter, text, !this.changesetStartBoundaryWritten);
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00040DD0 File Offset: 0x0003EFD0
		protected override ODataBatchOperationResponseMessage CreateOperationResponseMessageImplementation(string contentId)
		{
			this.WritePendingMessageData(true);
			base.CurrentOperationResponseMessage = base.BuildOperationResponseMessage(this.RawOutputContext.OutputStream, contentId, this.changeSetBoundary);
			base.SetState(ODataBatchWriter.BatchWriterState.OperationCreated);
			this.WriteStartBoundaryForOperation();
			ODataMultipartMixedBatchWriterUtils.WriteResponsePreamble(this.RawOutputContext.TextWriter, this.changeSetBoundary != null, contentId);
			return base.CurrentOperationResponseMessage;
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00040E2F File Offset: 0x0003F02F
		protected override void VerifyNotDisposed()
		{
			this.RawOutputContext.VerifyNotDisposed();
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00040E3C File Offset: 0x0003F03C
		protected override void WriteStartBatchImplementation()
		{
			base.SetState(ODataBatchWriter.BatchWriterState.BatchStarted);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00040E45 File Offset: 0x0003F045
		private void StartBatchOperationContent()
		{
			this.WritePendingMessageData(false);
			this.RawOutputContext.TextWriter.Flush();
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00040E5E File Offset: 0x0003F05E
		private void DisposeBatchWriterAndSetContentStreamRequestedState()
		{
			this.RawOutputContext.CloseWriter();
			base.SetState(ODataBatchWriter.BatchWriterState.OperationStreamRequested);
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00040E74 File Offset: 0x0003F074
		private void WriteStartBoundaryForOperation()
		{
			if (this.changeSetBoundary == null)
			{
				ODataMultipartMixedBatchWriterUtils.WriteStartBoundary(this.RawOutputContext.TextWriter, this.batchBoundary, !this.batchStartBoundaryWritten);
				this.batchStartBoundaryWritten = true;
				return;
			}
			ODataMultipartMixedBatchWriterUtils.WriteStartBoundary(this.RawOutputContext.TextWriter, this.changeSetBoundary, !this.changesetStartBoundaryWritten);
			this.changesetStartBoundaryWritten = true;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00040ED8 File Offset: 0x0003F0D8
		private void WritePendingMessageData(bool reportMessageCompleted)
		{
			if (this.CurrentOperationMessage != null)
			{
				if (base.CurrentOperationResponseMessage != null)
				{
					int statusCode = base.CurrentOperationResponseMessage.StatusCode;
					string statusMessage = HttpUtils.GetStatusMessage(statusCode);
					this.RawOutputContext.TextWriter.WriteLine("{0} {1} {2}", new object[] { "HTTP/1.1", statusCode, statusMessage });
				}
				IEnumerable<KeyValuePair<string, string>> headers = this.CurrentOperationMessage.Headers;
				if (headers != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in headers)
					{
						string key = keyValuePair.Key;
						string value = keyValuePair.Value;
						this.RawOutputContext.TextWriter.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[] { key, value }));
					}
				}
				this.RawOutputContext.TextWriter.WriteLine();
				if (reportMessageCompleted)
				{
					this.CurrentOperationMessage.PartHeaderProcessingCompleted();
					base.CurrentOperationRequestMessage = null;
					base.CurrentOperationResponseMessage = null;
				}
			}
		}

		// Token: 0x04000A58 RID: 2648
		private readonly string batchBoundary;

		// Token: 0x04000A59 RID: 2649
		private readonly DependsOnIdsTracker dependsOnIdsTracker;

		// Token: 0x04000A5A RID: 2650
		private string changeSetBoundary;

		// Token: 0x04000A5B RID: 2651
		private bool batchStartBoundaryWritten;

		// Token: 0x04000A5C RID: 2652
		private bool changesetStartBoundaryWritten;
	}
}
