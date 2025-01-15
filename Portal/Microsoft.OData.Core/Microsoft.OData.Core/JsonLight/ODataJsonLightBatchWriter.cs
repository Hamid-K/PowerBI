using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200022A RID: 554
	internal sealed class ODataJsonLightBatchWriter : ODataBatchWriter
	{
		// Token: 0x06001829 RID: 6185 RVA: 0x00045874 File Offset: 0x00043A74
		internal ODataJsonLightBatchWriter(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext)
		{
			this.jsonWriter = this.JsonLightOutputContext.JsonWriter;
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x000458A4 File Offset: 0x00043AA4
		private ODataJsonLightOutputContext JsonLightOutputContext
		{
			get
			{
				return base.OutputContext as ODataJsonLightOutputContext;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x00040B5D File Offset: 0x0003ED5D
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

		// Token: 0x0600182C RID: 6188 RVA: 0x000458B1 File Offset: 0x00043AB1
		public override void StreamRequested()
		{
			this.StartBatchOperationContent();
			this.JsonLightOutputContext.FlushBuffers();
			base.SetState(ODataBatchWriter.BatchWriterState.OperationStreamRequested);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x000458CB File Offset: 0x00043ACB
		public override Task StreamRequestedAsync()
		{
			this.StartBatchOperationContent();
			return this.JsonLightOutputContext.FlushBuffersAsync().FollowOnSuccessWith(delegate(Task task)
			{
				base.SetState(ODataBatchWriter.BatchWriterState.OperationStreamRequested);
			});
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x000458EF File Offset: 0x00043AEF
		public override void StreamDisposed()
		{
			base.SetState(ODataBatchWriter.BatchWriterState.OperationStreamDisposed);
			base.CurrentOperationRequestMessage = null;
			base.CurrentOperationResponseMessage = null;
			this.EnsurePreceedingMessageIsClosed();
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0004590C File Offset: 0x00043B0C
		public override void OnInStreamError()
		{
			this.JsonLightOutputContext.VerifyNotDisposed();
			base.SetState(ODataBatchWriter.BatchWriterState.Error);
			this.JsonLightOutputContext.JsonWriter.Flush();
			throw new ODataException(Strings.ODataBatchWriter_CannotWriteInStreamErrorForBatch);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0004593A File Offset: 0x00043B3A
		protected override void FlushSynchronously()
		{
			this.JsonLightOutputContext.Flush();
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00045947 File Offset: 0x00043B47
		protected override Task FlushAsynchronously()
		{
			return this.JsonLightOutputContext.FlushAsync();
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00045954 File Offset: 0x00043B54
		protected override void WriteStartBatchImplementation()
		{
			this.WriteBatchEnvelope();
			base.SetState(ODataBatchWriter.BatchWriterState.BatchStarted);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00045964 File Offset: 0x00043B64
		protected override IEnumerable<string> GetDependsOnRequestIds(IEnumerable<string> dependsOnIds)
		{
			List<string> list = new List<string>();
			foreach (string text in dependsOnIds)
			{
				if (this.atomicityGroupIdToRequestId.ContainsKey(text))
				{
					list.AddRange(this.atomicityGroupIdToRequestId[text]);
				}
				else
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x000459D8 File Offset: 0x00043BD8
		protected override void WriteEndBatchImplementation()
		{
			this.WritePendingMessageData(true);
			base.SetState(ODataBatchWriter.BatchWriterState.BatchCompleted);
			this.jsonWriter.EndArrayScope();
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x000459FE File Offset: 0x00043BFE
		protected override void WriteStartChangesetImplementation(string groupId)
		{
			this.WritePendingMessageData(true);
			base.SetState(ODataBatchWriter.BatchWriterState.ChangesetStarted);
			this.atomicityGroupId = groupId;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00045A15 File Offset: 0x00043C15
		protected override void WriteEndChangesetImplementation()
		{
			this.WritePendingMessageData(true);
			base.SetState(ODataBatchWriter.BatchWriterState.ChangesetCompleted);
			this.atomicityGroupId = null;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00045A2C File Offset: 0x00043C2C
		protected override ODataBatchOperationRequestMessage CreateOperationRequestMessageImplementation(string method, Uri uri, string contentId, BatchPayloadUriOption payloadUriOption, IEnumerable<string> dependsOnIds)
		{
			this.WritePendingMessageData(true);
			if (contentId == null)
			{
				contentId = Guid.NewGuid().ToString();
			}
			this.AddGroupIdLookup(contentId);
			base.CurrentOperationRequestMessage = base.BuildOperationRequestMessage(this.JsonLightOutputContext.GetOutputStream(), method, uri, contentId, this.atomicityGroupId, dependsOnIds ?? Enumerable.Empty<string>());
			base.SetState(ODataBatchWriter.BatchWriterState.OperationCreated);
			this.WriteStartBoundaryForOperation();
			this.jsonWriter.WriteName("id");
			this.jsonWriter.WriteValue(contentId);
			if (this.atomicityGroupId != null)
			{
				this.jsonWriter.WriteName("atomicityGroup");
				this.jsonWriter.WriteValue(this.atomicityGroupId);
			}
			if (base.CurrentOperationRequestMessage.DependsOnIds != null && base.CurrentOperationRequestMessage.DependsOnIds.Any<string>())
			{
				this.jsonWriter.WriteName("dependsOn");
				this.jsonWriter.StartArrayScope();
				foreach (string text in base.CurrentOperationRequestMessage.DependsOnIds)
				{
					this.ValidateDependsOnId(contentId, text);
					this.jsonWriter.WriteValue(text);
				}
				this.jsonWriter.EndArrayScope();
			}
			this.jsonWriter.WriteName("method");
			this.jsonWriter.WriteValue(method);
			this.jsonWriter.WriteName("url");
			this.jsonWriter.WriteValue(UriUtils.UriToString(uri));
			return base.CurrentOperationRequestMessage;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00045BB8 File Offset: 0x00043DB8
		protected override ODataBatchOperationResponseMessage CreateOperationResponseMessageImplementation(string contentId)
		{
			this.WritePendingMessageData(true);
			base.CurrentOperationResponseMessage = base.BuildOperationResponseMessage(this.JsonLightOutputContext.GetOutputStream(), contentId, this.atomicityGroupId);
			base.SetState(ODataBatchWriter.BatchWriterState.OperationCreated);
			this.WriteStartBoundaryForOperation();
			return base.CurrentOperationResponseMessage;
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00045BF2 File Offset: 0x00043DF2
		protected override void VerifyNotDisposed()
		{
			this.JsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00045C00 File Offset: 0x00043E00
		private void ValidateDependsOnId(string requestId, string dependsOnId)
		{
			if (this.atomicityGroupIdToRequestId.ContainsKey(dependsOnId))
			{
				string text;
				this.requestIdToAtomicGroupId.TryGetValue(requestId, out text);
				if (dependsOnId.Equals(text))
				{
					throw new ODataException(Strings.ODataBatchReader_DependsOnRequestIdIsPartOfAtomicityGroupNotAllowed(requestId, dependsOnId));
				}
			}
			else
			{
				string text2 = null;
				if (!this.requestIdToAtomicGroupId.TryGetValue(dependsOnId, out text2))
				{
					throw new ODataException(Strings.ODataBatchReader_DependsOnIdNotFound(dependsOnId, requestId));
				}
				if (text2 != null)
				{
					string text3;
					this.requestIdToAtomicGroupId.TryGetValue(requestId, out text3);
					if (!text2.Equals(text3))
					{
						throw new ODataException(Strings.ODataBatchReader_DependsOnRequestIdIsPartOfAtomicityGroupNotAllowed(requestId, text2));
					}
				}
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00045C88 File Offset: 0x00043E88
		private void AddGroupIdLookup(string contentId)
		{
			try
			{
				this.requestIdToAtomicGroupId.Add(contentId, this.atomicityGroupId);
			}
			catch (ArgumentException ex)
			{
				throw new ODataException(Strings.ODataBatchWriter_DuplicateContentIDsNotAllowed(contentId), ex);
			}
			if (this.atomicityGroupId != null)
			{
				if (!this.atomicityGroupIdToRequestId.ContainsKey(this.atomicityGroupId))
				{
					this.atomicityGroupIdToRequestId.Add(this.atomicityGroupId, new List<string>());
				}
				this.atomicityGroupIdToRequestId[this.atomicityGroupId].Add(contentId);
			}
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00045D10 File Offset: 0x00043F10
		private void WriteStartBoundaryForOperation()
		{
			this.jsonWriter.StartObjectScope();
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00045D20 File Offset: 0x00043F20
		private void StartBatchOperationContent()
		{
			this.WritePendingMessageData(false);
			this.jsonWriter.WriteRawValue(string.Format(CultureInfo.InvariantCulture, "{0} \"{1}\" {2}", new object[] { ",", "body", ":" }));
			this.JsonLightOutputContext.JsonWriter.Flush();
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00045D7C File Offset: 0x00043F7C
		private void WritePendingMessageData(bool reportMessageCompleted)
		{
			if (this.CurrentOperationMessage != null)
			{
				if (base.CurrentOperationRequestMessage != null)
				{
					this.WritePendingRequestMessageData();
				}
				else
				{
					this.WritePendingResponseMessageData();
				}
				if (reportMessageCompleted)
				{
					this.CurrentOperationMessage.PartHeaderProcessingCompleted();
					base.CurrentOperationRequestMessage = null;
					base.CurrentOperationResponseMessage = null;
					this.EnsurePreceedingMessageIsClosed();
				}
			}
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00045DC9 File Offset: 0x00043FC9
		private void EnsurePreceedingMessageIsClosed()
		{
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00045DD6 File Offset: 0x00043FD6
		private void WriteBatchEnvelope()
		{
			this.jsonWriter.StartObjectScope();
			this.jsonWriter.WriteName(this.JsonLightOutputContext.WritingResponse ? "responses" : "requests");
			this.jsonWriter.StartArrayScope();
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00045E14 File Offset: 0x00044014
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "need to use lower characters for header key")]
		private void WritePendingRequestMessageData()
		{
			this.jsonWriter.WriteName("headers");
			this.jsonWriter.StartObjectScope();
			IEnumerable<KeyValuePair<string, string>> headers = base.CurrentOperationRequestMessage.Headers;
			if (headers != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in headers)
				{
					this.jsonWriter.WriteName(keyValuePair.Key.ToLowerInvariant());
					this.jsonWriter.WriteValue(keyValuePair.Value);
				}
			}
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00045EB4 File Offset: 0x000440B4
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "need to use lower characters for header key")]
		private void WritePendingResponseMessageData()
		{
			this.jsonWriter.WriteName("id");
			this.jsonWriter.WriteValue(base.CurrentOperationResponseMessage.ContentId);
			if (this.atomicityGroupId != null)
			{
				this.jsonWriter.WriteName("atomicityGroup");
				this.jsonWriter.WriteValue(this.atomicityGroupId);
			}
			this.jsonWriter.WriteName("status");
			this.jsonWriter.WriteValue(base.CurrentOperationResponseMessage.StatusCode);
			this.jsonWriter.WriteName("headers");
			this.jsonWriter.StartObjectScope();
			IEnumerable<KeyValuePair<string, string>> headers = this.CurrentOperationMessage.Headers;
			if (headers != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in headers)
				{
					this.jsonWriter.WriteName(keyValuePair.Key.ToLowerInvariant());
					this.jsonWriter.WriteValue(keyValuePair.Value);
				}
			}
			this.jsonWriter.EndObjectScope();
		}

		// Token: 0x04000AD0 RID: 2768
		private const string PropertyId = "id";

		// Token: 0x04000AD1 RID: 2769
		private const string PropertyAtomicityGroup = "atomicityGroup";

		// Token: 0x04000AD2 RID: 2770
		private const string PropertyHeaders = "headers";

		// Token: 0x04000AD3 RID: 2771
		private const string PropertyBody = "body";

		// Token: 0x04000AD4 RID: 2772
		private const string PropertyRequests = "requests";

		// Token: 0x04000AD5 RID: 2773
		private const string PropertyDependsOn = "dependsOn";

		// Token: 0x04000AD6 RID: 2774
		private const string PropertyMethod = "method";

		// Token: 0x04000AD7 RID: 2775
		private const string PropertyUrl = "url";

		// Token: 0x04000AD8 RID: 2776
		private const string PropertyResponses = "responses";

		// Token: 0x04000AD9 RID: 2777
		private const string PropertyStatus = "status";

		// Token: 0x04000ADA RID: 2778
		private readonly IJsonWriter jsonWriter;

		// Token: 0x04000ADB RID: 2779
		private string atomicityGroupId;

		// Token: 0x04000ADC RID: 2780
		private Dictionary<string, string> requestIdToAtomicGroupId = new Dictionary<string, string>();

		// Token: 0x04000ADD RID: 2781
		private Dictionary<string, IList<string>> atomicityGroupIdToRequestId = new Dictionary<string, IList<string>>();
	}
}
