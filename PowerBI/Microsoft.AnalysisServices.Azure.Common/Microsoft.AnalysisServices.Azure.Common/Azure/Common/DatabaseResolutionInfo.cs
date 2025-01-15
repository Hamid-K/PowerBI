using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000056 RID: 86
	[CLSCompliant(true)]
	[DataContract]
	public class DatabaseResolutionInfo
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000FEBD File Offset: 0x0000E0BD
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0000FEC5 File Offset: 0x0000E0C5
		[DataMember]
		public ConnectionType ConnectionType { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000FECE File Offset: 0x0000E0CE
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0000FED6 File Offset: 0x0000E0D6
		[DataMember]
		public string ConnectionEndpoint { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000FEDF File Offset: 0x0000E0DF
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0000FEE7 File Offset: 0x0000E0E7
		[DataMember]
		public string LocalDatabaseId { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0000FEF8 File Offset: 0x0000E0F8
		[DataMember]
		public AuthType AuthType { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000FF01 File Offset: 0x0000E101
		public bool IsDedicated
		{
			get
			{
				return this.ConnectionType == ConnectionType.Http;
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		public DatabaseResolutionInfo()
		{
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000FF0C File Offset: 0x0000E10C
		public DatabaseResolutionInfo(ConnectionType connectionType, string connectionEndpoint, string localDatabaseId, AuthType authType)
		{
			this.ConnectionType = connectionType;
			this.ConnectionEndpoint = connectionEndpoint;
			this.LocalDatabaseId = localDatabaseId;
			this.AuthType = authType;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000FF34 File Offset: 0x0000E134
		public string GetDedicatedCapacityObjectId()
		{
			string text = string.Empty;
			if (this.IsDedicated)
			{
				text = new Uri(this.ConnectionEndpoint).AbsolutePath.TrimStart(new char[] { '/' });
			}
			return text;
		}
	}
}
