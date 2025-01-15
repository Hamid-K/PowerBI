using System;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x02000019 RID: 25
	internal enum SecDescType
	{
		// Token: 0x04000090 RID: 144
		Catalog = 1,
		// Token: 0x04000091 RID: 145
		Folder,
		// Token: 0x04000092 RID: 146
		ReportPrimary,
		// Token: 0x04000093 RID: 147
		ReportSecondary,
		// Token: 0x04000094 RID: 148
		Resource,
		// Token: 0x04000095 RID: 149
		Datasource,
		// Token: 0x04000096 RID: 150
		Model,
		// Token: 0x04000097 RID: 151
		ModelItem,
		// Token: 0x04000098 RID: 152
		Invalid = 255
	}
}
