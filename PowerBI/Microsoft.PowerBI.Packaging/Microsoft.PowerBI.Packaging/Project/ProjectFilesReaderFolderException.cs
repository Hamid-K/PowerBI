using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public class ProjectFilesReaderFolderException : ProjectFilesReaderException
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x000080A6 File Offset: 0x000062A6
		public ProjectFilesReaderFolderException(string folderName, string message, Exception innerException)
			: base(message, innerException)
		{
			this.FolderName = folderName;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x000080B7 File Offset: 0x000062B7
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x000080BF File Offset: 0x000062BF
		public string FolderName { get; private set; }

		// Token: 0x060002C8 RID: 712 RVA: 0x000080C8 File Offset: 0x000062C8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderName", this.FolderName);
		}

		// Token: 0x04000168 RID: 360
		private const string folderNameTag = "folderName";
	}
}
