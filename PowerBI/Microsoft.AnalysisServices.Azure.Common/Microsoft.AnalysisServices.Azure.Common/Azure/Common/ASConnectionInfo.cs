using System;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000047 RID: 71
	[DataContract]
	public class ASConnectionInfo
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x0000F09C File Offset: 0x0000D29C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{'DataSourceID': '",
				this.DataSourceID,
				"', 'DataSourceName': '",
				this.DataSourceName,
				"', 'ConnectionString': '",
				this.ConnectionString.MarkAsInternal(),
				"', 'ConnectionProvider': '",
				this.ConnectionProvider,
				"'}"
			});
		}

		// Token: 0x040000FB RID: 251
		[DataMember]
		public string DataSourceID;

		// Token: 0x040000FC RID: 252
		[DataMember]
		public string DataSourceName;

		// Token: 0x040000FD RID: 253
		[DataMember]
		public string ConnectionString;

		// Token: 0x040000FE RID: 254
		[DataMember]
		public string ConnectionProvider;
	}
}
