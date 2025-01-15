using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B45 RID: 2885
	public class Rf2hMqUsrFolder : Rf2hFolderWithFieldsAndProperties
	{
		// Token: 0x06005B3B RID: 23355 RVA: 0x00177923 File Offset: 0x00175B23
		public Rf2hMqUsrFolder()
			: this(null)
		{
		}

		// Token: 0x06005B3C RID: 23356 RVA: 0x0017792C File Offset: 0x00175B2C
		public Rf2hMqUsrFolder(string completeString)
			: base(Rf2hFolderType.Mq_Usr, Rf2hFolderType.Mq_Usr.ToString().ToLowerInvariant(), null, completeString)
		{
		}
	}
}
