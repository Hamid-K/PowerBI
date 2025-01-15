using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B48 RID: 2888
	public class Rf2hPropertiesFolder : Rf2hFolderWithFieldsAndProperties
	{
		// Token: 0x06005B4B RID: 23371 RVA: 0x00177A72 File Offset: 0x00175C72
		public Rf2hPropertiesFolder(string completeString)
			: this(Rf2hFolder.GetFolderName(completeString), completeString)
		{
		}

		// Token: 0x06005B4C RID: 23372 RVA: 0x00177A81 File Offset: 0x00175C81
		public Rf2hPropertiesFolder(string folderTag, string completeString)
			: base(Rf2hFolderType.Properties, folderTag, null, completeString)
		{
		}
	}
}
