using System;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x0200005F RID: 95
	public class DaxQueryViewFileMatcher : IFileFormatMatcher
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x00007FFA File Offset: 0x000061FA
		public bool IsMatch(string relativePath)
		{
			relativePath = relativePath.Replace("/", "\\");
			return this.IsDaxFileAtRoot(relativePath) || this.IsDaxSettingsFile(relativePath);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00008020 File Offset: 0x00006220
		private bool IsDaxFileAtRoot(string relativePath)
		{
			return !relativePath.Contains("\\") && relativePath.EndsWith(".dax", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000803D File Offset: 0x0000623D
		private bool IsDaxSettingsFile(string relativePath)
		{
			return relativePath.Equals(this.daxSettingsFileName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000165 RID: 357
		private readonly string daxSettingsFileName = ".pbi\\daxQueries.json";
	}
}
