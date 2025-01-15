using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.ExecutionMetadata;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.ExploreHost.ServiceContracts;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x0200003D RID: 61
	internal sealed class ExploreExecuteSemanticQueryResultWriter : IExploreExecuteSemanticQueryResultWriter, IDisposable, IQueryResultsWriter, IQueryResultDataWriter, IExecuteSemanticQueryResultWriter
	{
		// Token: 0x060001EA RID: 490 RVA: 0x0000627B File Offset: 0x0000447B
		private ExploreExecuteSemanticQueryResultWriter()
		{
			this.Open();
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000062A3 File Offset: 0x000044A3
		private bool IsResultFinalized
		{
			get
			{
				return this.finalizedResult != null;
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000062AE File Offset: 0x000044AE
		public static IExploreExecuteSemanticQueryResultWriter Create()
		{
			return new ExploreExecuteSemanticQueryResultWriter();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000062B5 File Offset: 0x000044B5
		private byte[] GetJobIdByteArray(int jobId)
		{
			return Encoding.UTF8.GetBytes("\"" + jobId.ToString(CultureInfo.InvariantCulture) + "\"");
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000062DC File Offset: 0x000044DC
		public string GetResult()
		{
			if (!this.IsResultFinalized)
			{
				if (this.jobId > -1)
				{
					this.WriteEndArray();
				}
				this.WriteEndObject();
				this.serializedResultStream.SetLength(this.serializedResultStream.Position);
				this.serializedResultStream.Position = 0L;
				this.finalizedResult = new StreamReader(this.serializedResultStream).ReadToEnd();
			}
			this.Dispose();
			return this.finalizedResult;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000634B File Offset: 0x0000454B
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.serializedResultStream.Dispose();
				this.isDisposed = true;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006367 File Offset: 0x00004567
		public void WriteRequestError(ServiceError error)
		{
			this.EnsureTopLevelOfResultActive();
			this.WriteEmptyJobIds();
			this.WriteEmptyResults();
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.Separator);
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.RequestErrorPropertyTag);
			this.SerializeAndWriteStructure(error);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006398 File Offset: 0x00004598
		public IQueryResultsWriter BeginResults(ExecuteSemanticQueryRequest request)
		{
			this.EnsureTopLevelOfResultActive();
			this.WriteJobIds(request);
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.Separator);
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.ResultsPropertyTag);
			this.WriteStartArray();
			this.jobId = 0;
			return this;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000063CB File Offset: 0x000045CB
		public Stream GetDataShapeResultStream()
		{
			this.EnsureQueryResultDataInProgress();
			if (this.queryBindingDescriptorWritten)
			{
				this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.Separator);
			}
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.DataShapeResultPropertyTag);
			this.dataWritten = true;
			return this.serializedResultStream;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000063FE File Offset: 0x000045FE
		public Stream GetRawDataStream()
		{
			throw new InvalidOperationException("RawData is not supported");
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000640C File Offset: 0x0000460C
		public IQueryResultDataWriter BeginQueryResultData()
		{
			this.queryResultInProgressStartPosition = this.serializedResultStream.Position;
			this.queryBindingDescriptorWritten = false;
			this.dataWritten = false;
			if (this.hasCompletedResultWrapper)
			{
				this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.Separator);
			}
			this.StartResultWrapperWithJobId();
			this.WriteStartObject();
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.DataPropertyTag);
			this.WriteStartObject();
			return this;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006469 File Offset: 0x00004669
		public void EndQueryResultData()
		{
			this.WriteEndObject();
			this.WriteEndObject();
			this.WriteEndObject();
			this.hasCompletedResultWrapper = true;
			this.queryResultInProgressStartPosition = -1L;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000648C File Offset: 0x0000468C
		public void WriteFailedQueryResult(ServiceError error)
		{
			if (this.hasCompletedResultWrapper)
			{
				this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.Separator);
			}
			this.StartResultWrapperWithJobId();
			this.WriteServiceErrorInWrapper(error);
			this.WriteEndObject();
			this.hasCompletedResultWrapper = true;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000064BB File Offset: 0x000046BB
		public void DiscardQueryResultDataProgress()
		{
			this.serializedResultStream.Position = this.queryResultInProgressStartPosition;
			this.queryResultInProgressStartPosition = -1L;
			this.jobId--;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000064E4 File Offset: 0x000046E4
		public void WriteQueryBindingDescriptor(QueryBindingDescriptor descriptor)
		{
			this.EnsureQueryResultDataInProgress();
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.DescriptorPropertyTag);
			new DataContractJsonSerializer(descriptor.GetType()).WriteObject(this.serializedResultStream, descriptor);
			this.queryBindingDescriptorWritten = true;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006515 File Offset: 0x00004715
		public void WriteExecutionMetrics(ExecutionMetrics metrics)
		{
			this.EnsureQueryResultDataInProgress();
			if (this.queryBindingDescriptorWritten || this.dataWritten)
			{
				this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.Separator);
			}
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.MetricsPropertyTag);
			ExecutionMetricsSerializer.Serialize(metrics, this.serializedResultStream);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000654F File Offset: 0x0000474F
		private Stream GetBaseStreamToWriteOdataError()
		{
			this.EnsureQueryResultDataInProgress();
			return this.serializedResultStream;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000655D File Offset: 0x0000475D
		private void EnsureQueryResultDataInProgress()
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000655F File Offset: 0x0000475F
		private void EnsureTopLevelOfResultActive()
		{
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006561 File Offset: 0x00004761
		private void Open()
		{
			this.WriteStartObject();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006569 File Offset: 0x00004769
		private void WriteEmptyJobIds()
		{
			this.EnsureTopLevelOfResultActive();
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.JobIdsPropertyTag);
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.NullJsonValue);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006588 File Offset: 0x00004788
		private void WriteJobIds(ExecuteSemanticQueryRequest request)
		{
			this.EnsureTopLevelOfResultActive();
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.JobIdsPropertyTag);
			bool flag = true;
			this.WriteStartArray();
			IList<DataQueryRequest> queries = request.Queries;
			int num = ((queries != null) ? queries.Count : 0);
			for (int i = 0; i < num; i++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.Separator);
				}
				this.WriteFullByteArray(this.GetJobIdByteArray(i));
			}
			this.WriteEndArray();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000065F2 File Offset: 0x000047F2
		private void WriteEmptyResults()
		{
			this.EnsureTopLevelOfResultActive();
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.Separator);
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.ResultsPropertyTag);
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.NullJsonValue);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000661B File Offset: 0x0000481B
		private void WriteEmptyBindingDescriptor()
		{
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.DescriptorPropertyTag);
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.NullJsonValue);
			this.queryBindingDescriptorWritten = true;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000663C File Offset: 0x0000483C
		private void SerializeAndWriteStructure(ServiceError error)
		{
			string text = JsonConvert.SerializeObject(error);
			this.Write(text);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00006658 File Offset: 0x00004858
		private void StartResultWrapperWithJobId()
		{
			this.WriteStartObject();
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.JobIdPropertyTag);
			this.WriteFullByteArray(this.GetJobIdByteArray(this.jobId));
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.Separator);
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.ResultPropertyTag);
			this.jobId++;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000066AC File Offset: 0x000048AC
		private void WriteServiceErrorInWrapper(ServiceError error)
		{
			this.WriteStartObject();
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.ErrorPropertyTag);
			this.SerializeAndWriteStructure(error);
			this.WriteEndObject();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000066CC File Offset: 0x000048CC
		private void WriteFullByteArray(byte[] byteArray)
		{
			this.serializedResultStream.Write(byteArray, 0, byteArray.Length);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000066DE File Offset: 0x000048DE
		private void Write(string s)
		{
			this.WriteFullByteArray(Encoding.UTF8.GetBytes(s));
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000066F1 File Offset: 0x000048F1
		private void WriteStartObject()
		{
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.ObjectStart);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000066FE File Offset: 0x000048FE
		private void WriteEndObject()
		{
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.ObjectEnd);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000670B File Offset: 0x0000490B
		private void WriteStartArray()
		{
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.ArrayStart);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006718 File Offset: 0x00004918
		private void WriteEndArray()
		{
			this.WriteFullByteArray(ExploreExecuteSemanticQueryResultWriter.ArrayEnd);
		}

		// Token: 0x040000AC RID: 172
		private static readonly byte[] DescriptorPropertyTag = Encoding.UTF8.GetBytes("\"descriptor\":");

		// Token: 0x040000AD RID: 173
		private static readonly byte[] DataShapeResultPropertyTag = Encoding.UTF8.GetBytes("\"dsr\":");

		// Token: 0x040000AE RID: 174
		private static readonly byte[] DataPropertyTag = Encoding.UTF8.GetBytes("\"data\":");

		// Token: 0x040000AF RID: 175
		private static readonly byte[] ErrorPropertyTag = Encoding.UTF8.GetBytes("\"error\":");

		// Token: 0x040000B0 RID: 176
		private static readonly byte[] RequestErrorPropertyTag = Encoding.UTF8.GetBytes("\"requestError\":");

		// Token: 0x040000B1 RID: 177
		private static readonly byte[] JobIdsPropertyTag = Encoding.UTF8.GetBytes("\"jobIds\":");

		// Token: 0x040000B2 RID: 178
		private static readonly byte[] ResultsPropertyTag = Encoding.UTF8.GetBytes("\"results\":");

		// Token: 0x040000B3 RID: 179
		private static readonly byte[] ResultPropertyTag = Encoding.UTF8.GetBytes("\"result\":");

		// Token: 0x040000B4 RID: 180
		private static readonly byte[] JobIdPropertyTag = Encoding.UTF8.GetBytes("\"jobId\":");

		// Token: 0x040000B5 RID: 181
		private static readonly byte[] MetricsPropertyTag = Encoding.UTF8.GetBytes("\"metrics\":");

		// Token: 0x040000B6 RID: 182
		private static readonly byte[] Separator = Encoding.UTF8.GetBytes(",");

		// Token: 0x040000B7 RID: 183
		private static readonly byte[] ArrayStart = Encoding.UTF8.GetBytes("[");

		// Token: 0x040000B8 RID: 184
		private static readonly byte[] ArrayEnd = Encoding.UTF8.GetBytes("]");

		// Token: 0x040000B9 RID: 185
		private static readonly byte[] ObjectStart = Encoding.UTF8.GetBytes("{");

		// Token: 0x040000BA RID: 186
		private static readonly byte[] ObjectEnd = Encoding.UTF8.GetBytes("}");

		// Token: 0x040000BB RID: 187
		private static readonly byte[] NullJsonValue = Encoding.UTF8.GetBytes("null");

		// Token: 0x040000BC RID: 188
		private readonly MemoryStream serializedResultStream = new MemoryStream();

		// Token: 0x040000BD RID: 189
		private string finalizedResult;

		// Token: 0x040000BE RID: 190
		private long queryResultInProgressStartPosition = -1L;

		// Token: 0x040000BF RID: 191
		private bool hasCompletedResultWrapper;

		// Token: 0x040000C0 RID: 192
		private int jobId = -1;

		// Token: 0x040000C1 RID: 193
		private bool isDisposed;

		// Token: 0x040000C2 RID: 194
		private bool queryBindingDescriptorWritten;

		// Token: 0x040000C3 RID: 195
		private bool dataWritten;
	}
}
