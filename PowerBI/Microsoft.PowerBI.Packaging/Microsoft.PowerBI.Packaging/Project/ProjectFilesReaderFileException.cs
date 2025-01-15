using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public class ProjectFilesReaderFileException : ProjectFilesReaderException
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x00008069 File Offset: 0x00006269
		public ProjectFilesReaderFileException(string fileName, string message, Exception innerException)
			: base(message, innerException)
		{
			this.FileName = fileName;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000807A File Offset: 0x0000627A
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00008082 File Offset: 0x00006282
		public string FileName { get; private set; }

		// Token: 0x060002C4 RID: 708 RVA: 0x0000808B File Offset: 0x0000628B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fileName", this.FileName);
		}

		// Token: 0x04000166 RID: 358
		private const string fileNameTag = "fileName";
	}
}
