using System;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000012 RID: 18
	public class ExplorationDeserializationWarning
	{
		// Token: 0x06000059 RID: 89 RVA: 0x000028B0 File Offset: 0x00000AB0
		public ExplorationDeserializationWarning(string filePath, string message)
		{
			this.FilePath = filePath;
			this.Message = message;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000028C6 File Offset: 0x00000AC6
		public string FilePath { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000028CE File Offset: 0x00000ACE
		public string Message { get; }
	}
}
