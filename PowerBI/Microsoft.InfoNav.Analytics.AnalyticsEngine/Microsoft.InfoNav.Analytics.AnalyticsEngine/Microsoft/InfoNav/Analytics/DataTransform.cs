using System;
using System.Threading.Tasks;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x02000008 RID: 8
	internal abstract class DataTransform : IDataTransform, IMetadataTransform
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002341 File Offset: 0x00000541
		protected DataTransform(ServiceRuntimeContext context)
		{
			this.ServiceRuntimeContext = context;
		}

		// Token: 0x06000010 RID: 16
		public abstract Task<DataTransformResult> ExecuteAsync(DataTransformExecutionContext context);

		// Token: 0x06000011 RID: 17
		public abstract SchemaTransformResult GetSchema(SchemaTransformContext context);

		// Token: 0x04000030 RID: 48
		protected readonly ServiceRuntimeContext ServiceRuntimeContext;
	}
}
