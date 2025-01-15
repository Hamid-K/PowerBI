using System;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x02000101 RID: 257
	internal class ODataMaterializerContext : IODataMaterializerContext
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x000291A4 File Offset: 0x000273A4
		internal ODataMaterializerContext(ResponseInfo responseInfo)
		{
			this.ResponseInfo = responseInfo;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x000291B3 File Offset: 0x000273B3
		public DataServiceContext Context
		{
			get
			{
				return this.ResponseInfo.Context;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x000291C0 File Offset: 0x000273C0
		public UndeclaredPropertyBehavior UndeclaredPropertyBehavior
		{
			get
			{
				return this.ResponseInfo.Context.UndeclaredPropertyBehavior;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x000291D2 File Offset: 0x000273D2
		public ClientEdmModel Model
		{
			get
			{
				return this.ResponseInfo.Model;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x000291DF File Offset: 0x000273DF
		public DataServiceClientResponsePipelineConfiguration ResponsePipeline
		{
			get
			{
				return this.ResponseInfo.ResponsePipeline;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x000291EC File Offset: 0x000273EC
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x000291F4 File Offset: 0x000273F4
		private protected ResponseInfo ResponseInfo { protected get; private set; }

		// Token: 0x06000ADE RID: 2782 RVA: 0x000291FD File Offset: 0x000273FD
		public ClientTypeAnnotation ResolveTypeForMaterialization(Type expectedType, string wireTypeName)
		{
			return this.ResponseInfo.TypeResolver.ResolveTypeForMaterialization(expectedType, wireTypeName);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00029211 File Offset: 0x00027411
		public IEdmType ResolveExpectedTypeForReading(Type expectedType)
		{
			return this.ResponseInfo.TypeResolver.ResolveExpectedTypeForReading(expectedType);
		}
	}
}
