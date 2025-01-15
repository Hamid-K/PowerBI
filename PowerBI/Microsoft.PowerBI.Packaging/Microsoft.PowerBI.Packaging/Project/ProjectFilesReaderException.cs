using System;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	public class ProjectFilesReaderException : Exception
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000805F File Offset: 0x0000625F
		public ProjectFilesReaderException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
