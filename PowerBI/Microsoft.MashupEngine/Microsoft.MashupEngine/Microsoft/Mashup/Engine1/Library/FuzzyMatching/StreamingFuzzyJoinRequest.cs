using System;
using System.Data;
using System.Threading;
using Microsoft.DataIntegration.FuzzyMatching;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B41 RID: 2881
	internal sealed class StreamingFuzzyJoinRequest : FuzzyJoinRequest
	{
		// Token: 0x06004FFD RID: 20477 RVA: 0x0010BDA5 File Offset: 0x00109FA5
		private StreamingFuzzyJoinRequest(IDataReader inputDataReader, DataTable referenceDataTable, DataTable transformationDataTable, FuzzyLookupEntry.FuzzyLookupParameters fuzzyJoinParameters, FuzzyLookupEntry.JoinType fuzzyJoinType)
			: base(referenceDataTable, transformationDataTable, fuzzyJoinParameters, fuzzyJoinType)
		{
			this.inputDataReader = inputDataReader;
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x0010BDBC File Offset: 0x00109FBC
		public static FuzzyJoinRequest New(IThreadPoolService threadPool, IDataReader inputDataReader, DataTable referenceDataTable, DataTable transformationDataTable, FuzzyLookupEntry.FuzzyLookupParameters fuzzyJoinParameters, FuzzyLookupEntry.JoinType fuzzyJoinType)
		{
			FuzzyJoinRequest fuzzyJoinRequest = new StreamingFuzzyJoinRequest(inputDataReader, referenceDataTable, transformationDataTable, fuzzyJoinParameters, fuzzyJoinType);
			threadPool.QueueUserWorkItem(new WaitCallback(StreamingFuzzyJoinRequest.ExecuteInternal), fuzzyJoinRequest);
			return fuzzyJoinRequest;
		}

		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x06004FFF RID: 20479 RVA: 0x0010BDEA File Offset: 0x00109FEA
		public IDataReader OutputTable
		{
			get
			{
				return this.outputDataReader;
			}
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x0010BDF4 File Offset: 0x00109FF4
		private void Execute()
		{
			this.outputDataReader = ExternalFuzzyMatcher.ExecuteJoinStreaming(this.inputDataReader, this.referenceDataTable, this.transformationDataTable, this.fuzzyJoinParameters, this.fuzzyJoinType, default(Guid), false);
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x0010BE34 File Offset: 0x0010A034
		private static void ExecuteInternal(object request)
		{
			((StreamingFuzzyJoinRequest)request).Execute();
		}

		// Token: 0x04002AED RID: 10989
		private readonly IDataReader inputDataReader;

		// Token: 0x04002AEE RID: 10990
		private IDataReader outputDataReader;
	}
}
