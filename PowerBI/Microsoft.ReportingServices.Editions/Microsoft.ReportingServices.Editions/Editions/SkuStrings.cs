using System;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x02000010 RID: 16
	public class SkuStrings : Attribute
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002F28 File Offset: 0x00001128
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002F30 File Offset: 0x00001130
		public string ShortName { get; set; }

		// Token: 0x0600004A RID: 74 RVA: 0x00002F39 File Offset: 0x00001139
		public SkuStrings()
		{
			this.FullName = string.Empty;
			this.ShortName = string.Empty;
			this.CommandLineName = string.Empty;
			this.PkConfigName = string.Empty;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F6D File Offset: 0x0000116D
		public SkuStrings(string fullName, string shortName, string commandLineName, string pkConfigName, string editionName)
		{
			this.FullName = fullName;
			this.ShortName = shortName;
			this.CommandLineName = commandLineName;
			this.PkConfigName = pkConfigName;
		}

		// Token: 0x04000051 RID: 81
		public string FullName;

		// Token: 0x04000053 RID: 83
		public string CommandLineName;

		// Token: 0x04000054 RID: 84
		public string PkConfigName;
	}
}
