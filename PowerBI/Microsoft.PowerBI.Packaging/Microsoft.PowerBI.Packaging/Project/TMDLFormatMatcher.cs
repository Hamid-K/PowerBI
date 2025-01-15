using System;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x0200005E RID: 94
	public class TMDLFormatMatcher : IFileFormatMatcher
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00007FE4 File Offset: 0x000061E4
		public bool IsMatch(string relativePath)
		{
			return relativePath.EndsWith(".tmdl", StringComparison.OrdinalIgnoreCase);
		}
	}
}
