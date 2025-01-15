using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000032 RID: 50
	public interface IModule
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600011F RID: 287
		string Name { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000120 RID: 288
		string Version { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000121 RID: 289
		IRecordValue Metadata { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000122 RID: 290
		IKeys Exports { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000123 RID: 291
		ResourceKindInfo[] DataSources { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000124 RID: 292
		ResourceKindInfo DynamicModuleDataSource { get; }
	}
}
