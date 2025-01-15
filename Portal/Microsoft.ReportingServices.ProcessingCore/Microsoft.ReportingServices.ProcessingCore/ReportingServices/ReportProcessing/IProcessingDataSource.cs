using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000646 RID: 1606
	public interface IProcessingDataSource
	{
		// Token: 0x17002005 RID: 8197
		// (get) Token: 0x06005786 RID: 22406
		Guid ID { get; }

		// Token: 0x17002006 RID: 8198
		// (get) Token: 0x06005787 RID: 22407
		string Name { get; }

		// Token: 0x17002007 RID: 8199
		// (get) Token: 0x06005788 RID: 22408
		// (set) Token: 0x06005789 RID: 22409
		string Type { get; set; }

		// Token: 0x17002008 RID: 8200
		// (set) Token: 0x0600578A RID: 22410
		string SharedDataSourceReferencePath { set; }

		// Token: 0x17002009 RID: 8201
		// (get) Token: 0x0600578B RID: 22411
		string DataSourceReference { get; }

		// Token: 0x1700200A RID: 8202
		// (get) Token: 0x0600578C RID: 22412
		bool IntegratedSecurity { get; }
	}
}
