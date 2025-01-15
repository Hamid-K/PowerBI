using System;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.Engine.DaxQueryExecution
{
	// Token: 0x02000025 RID: 37
	public sealed class ExecuteDaxQueryContext
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x000037F8 File Offset: 0x000019F8
		public ExecuteDaxQueryContext(HostServices hostServices, string daxQuery, int maxRowCount, int maxNumberOfValues, int? maxNumberOfBytes, IStreamingStructureWriter resultWriter, IDataShapingDataSourceInfo dataSourceInfo, DbCommandOptions dbCommandOptions, QueryExecutionOptions queryExecutionOptions, ExecuteDaxQuerySerializerSettings serializerSettings)
		{
			this.HostServices = hostServices;
			this.DaxQuery = daxQuery;
			this.MaxRowCount = maxRowCount;
			this.MaxNumberOfValues = maxNumberOfValues;
			this.MaxNumberOfBytes = maxNumberOfBytes;
			this.ResultWriter = resultWriter;
			this.DataSourceInfo = dataSourceInfo;
			this.DbCommandOptions = dbCommandOptions;
			this.QueryExecutionOptions = queryExecutionOptions;
			this.SerializerSettings = serializerSettings;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003858 File Offset: 0x00001A58
		public HostServices HostServices { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003860 File Offset: 0x00001A60
		public string DaxQuery { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003868 File Offset: 0x00001A68
		public int MaxRowCount { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003870 File Offset: 0x00001A70
		public int MaxNumberOfValues { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003878 File Offset: 0x00001A78
		internal int? MaxNumberOfBytes { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003880 File Offset: 0x00001A80
		public IStreamingStructureWriter ResultWriter { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003888 File Offset: 0x00001A88
		public IDataShapingDataSourceInfo DataSourceInfo { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003890 File Offset: 0x00001A90
		public DbCommandOptions DbCommandOptions { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003898 File Offset: 0x00001A98
		public QueryExecutionOptions QueryExecutionOptions { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000038A0 File Offset: 0x00001AA0
		public ExecuteDaxQuerySerializerSettings SerializerSettings { get; }
	}
}
