using System;
using Microsoft.AnalysisServices.Azure.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.CommonUtilities
{
	// Token: 0x02000031 RID: 49
	public sealed class SampleModelProperty
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000E3C5 File Offset: 0x0000C5C5
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000E3CD File Offset: 0x0000C5CD
		public string FileName { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000E3D6 File Offset: 0x0000C5D6
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000E3DE File Offset: 0x0000C5DE
		public string ModelName { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000E3E7 File Offset: 0x0000C5E7
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000E3EF File Offset: 0x0000C5EF
		public string ResourcePoolName { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000E3F8 File Offset: 0x0000C5F8
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000E400 File Offset: 0x0000C600
		public string ResourceGroupName { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000E409 File Offset: 0x0000C609
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0000E411 File Offset: 0x0000C611
		public int ModelMaxMemoryMB { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000E41A File Offset: 0x0000C61A
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0000E422 File Offset: 0x0000C622
		public int ModelMaxCPU { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000E42B File Offset: 0x0000C62B
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000E433 File Offset: 0x0000C633
		public int ModelProcessLimitMB { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000E43C File Offset: 0x0000C63C
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000E444 File Offset: 0x0000C644
		public string TestQuery { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000E44D File Offset: 0x0000C64D
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000E455 File Offset: 0x0000C655
		public string TestQueryResult { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000E45E File Offset: 0x0000C65E
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000E466 File Offset: 0x0000C666
		public string TestUtterance { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000E46F File Offset: 0x0000C66F
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000E477 File Offset: 0x0000C677
		public DatabaseMoniker DatabaseMoniker { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000E480 File Offset: 0x0000C680
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000E488 File Offset: 0x0000C688
		public bool ShouldRestoreDatabase { get; set; }

		// Token: 0x06000337 RID: 823 RVA: 0x0000E494 File Offset: 0x0000C694
		public SampleModelProperty(string fileName, string modelName, int modelMaxMemoryMB, int modelMaxCPU, int modelProcessLimitMB, string testQuery, string testQueryResult, string testUtterance)
		{
			this.FileName = fileName;
			this.ModelName = modelName;
			this.ResourcePoolName = "{0}-ResourcePool".FormatWithInvariantCulture(new object[] { this.ModelName });
			this.ResourceGroupName = "{0}-ResourceGroup".FormatWithInvariantCulture(new object[] { this.ModelName });
			this.ModelMaxMemoryMB = modelMaxMemoryMB;
			this.ModelMaxCPU = modelMaxCPU;
			this.ModelProcessLimitMB = modelProcessLimitMB;
			this.TestQuery = testQuery;
			this.TestQueryResult = testQueryResult;
			this.TestUtterance = testUtterance;
			this.ShouldRestoreDatabase = true;
			this.DatabaseMoniker = new DatabaseMoniker(SampleModelConstants.VIRTUAL_SERVER_NAME, this.ModelName);
		}
	}
}
