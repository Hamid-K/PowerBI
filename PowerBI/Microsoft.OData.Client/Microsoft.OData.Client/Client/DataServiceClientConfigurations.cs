using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000030 RID: 48
	public class DataServiceClientConfigurations
	{
		// Token: 0x06000187 RID: 391 RVA: 0x000080B2 File Offset: 0x000062B2
		internal DataServiceClientConfigurations(object sender)
		{
			this.ResponsePipeline = new DataServiceClientResponsePipelineConfiguration(sender);
			this.RequestPipeline = new DataServiceClientRequestPipelineConfiguration();
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000080D1 File Offset: 0x000062D1
		// (set) Token: 0x06000189 RID: 393 RVA: 0x000080D9 File Offset: 0x000062D9
		public DataServiceClientResponsePipelineConfiguration ResponsePipeline { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000080E2 File Offset: 0x000062E2
		// (set) Token: 0x0600018B RID: 395 RVA: 0x000080EA File Offset: 0x000062EA
		public DataServiceClientRequestPipelineConfiguration RequestPipeline { get; private set; }
	}
}
