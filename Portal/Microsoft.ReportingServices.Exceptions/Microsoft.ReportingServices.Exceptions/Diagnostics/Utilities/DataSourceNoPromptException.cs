using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000058 RID: 88
	[Serializable]
	internal sealed class DataSourceNoPromptException : ReportCatalogException
	{
		// Token: 0x060001DB RID: 475 RVA: 0x00004637 File Offset: 0x00002837
		public DataSourceNoPromptException(string dataSource)
			: base(ErrorCode.rsDataSourceNoPrompt, ErrorStringsWrapper.rsDataSourceNoPromptException(dataSource), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000464E File Offset: 0x0000284E
		private DataSourceNoPromptException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
