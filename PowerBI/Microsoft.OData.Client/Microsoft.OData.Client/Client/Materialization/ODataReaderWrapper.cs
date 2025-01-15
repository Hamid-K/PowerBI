using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x020000FD RID: 253
	internal class ODataReaderWrapper
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x000287FE File Offset: 0x000269FE
		private ODataReaderWrapper(ODataReader reader, DataServiceClientResponsePipelineConfiguration responsePipeline)
		{
			this.reader = reader;
			this.responsePipeline = responsePipeline;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00028814 File Offset: 0x00026A14
		public ODataReaderState State
		{
			get
			{
				return this.reader.State;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x00028821 File Offset: 0x00026A21
		public ODataItem Item
		{
			get
			{
				return this.reader.Item;
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00028830 File Offset: 0x00026A30
		public bool Read()
		{
			bool flag = this.reader.Read();
			if (flag && this.responsePipeline.HasConfigurations)
			{
				switch (this.reader.State)
				{
				case ODataReaderState.ResourceSetStart:
					this.responsePipeline.ExecuteOnFeedStartActions((ODataResourceSet)this.reader.Item);
					break;
				case ODataReaderState.ResourceSetEnd:
					this.responsePipeline.ExecuteOnFeedEndActions((ODataResourceSet)this.reader.Item);
					break;
				case ODataReaderState.ResourceStart:
					this.responsePipeline.ExecuteOnEntryStartActions((ODataResource)this.reader.Item);
					break;
				case ODataReaderState.ResourceEnd:
					this.responsePipeline.ExecuteOnEntryEndActions((ODataResource)this.reader.Item);
					break;
				case ODataReaderState.NestedResourceInfoStart:
					this.responsePipeline.ExecuteOnNavigationStartActions((ODataNestedResourceInfo)this.reader.Item);
					break;
				case ODataReaderState.NestedResourceInfoEnd:
					this.responsePipeline.ExecuteOnNavigationEndActions((ODataNestedResourceInfo)this.reader.Item);
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00028940 File Offset: 0x00026B40
		internal static ODataReaderWrapper Create(ODataMessageReader messageReader, ODataPayloadKind messageType, IEdmType expectedType, DataServiceClientResponsePipelineConfiguration responsePipeline)
		{
			IEdmStructuredType edmStructuredType = expectedType as IEdmStructuredType;
			if (messageType == ODataPayloadKind.Resource)
			{
				return new ODataReaderWrapper(messageReader.CreateODataResourceReader(edmStructuredType), responsePipeline);
			}
			return new ODataReaderWrapper(messageReader.CreateODataResourceSetReader(edmStructuredType), responsePipeline);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00028973 File Offset: 0x00026B73
		internal static ODataReaderWrapper CreateForTest(ODataReader reader, DataServiceClientResponsePipelineConfiguration responsePipeline)
		{
			return new ODataReaderWrapper(reader, responsePipeline);
		}

		// Token: 0x04000617 RID: 1559
		private readonly ODataReader reader;

		// Token: 0x04000618 RID: 1560
		private readonly DataServiceClientResponsePipelineConfiguration responsePipeline;
	}
}
