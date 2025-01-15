using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000043 RID: 67
	[DataContract]
	public sealed class AggregatedServiceMetrics
	{
		// Token: 0x0600037A RID: 890 RVA: 0x0000ED4F File Offset: 0x0000CF4F
		public AggregatedServiceMetrics(int countOfBoundServices, int countOfUnBoundServices, DatabaseType databaseType)
		{
			this.CountOfBoundServices = countOfBoundServices;
			this.CountOfUnBoundServices = countOfUnBoundServices;
			this.DatabaseType = databaseType;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000ED74 File Offset: 0x0000CF74
		[DataMember]
		public int CountOfBoundServices { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000ED7D File Offset: 0x0000CF7D
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000ED85 File Offset: 0x0000CF85
		[DataMember]
		public int CountOfUnBoundServices { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000ED8E File Offset: 0x0000CF8E
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000ED96 File Offset: 0x0000CF96
		[DataMember]
		public DatabaseType DatabaseType { get; set; }
	}
}
