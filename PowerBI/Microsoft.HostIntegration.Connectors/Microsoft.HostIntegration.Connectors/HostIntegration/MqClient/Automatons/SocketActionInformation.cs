using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AFD RID: 2813
	public class SocketActionInformation
	{
		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x06005952 RID: 22866 RVA: 0x00170DE0 File Offset: 0x0016EFE0
		// (set) Token: 0x06005953 RID: 22867 RVA: 0x00170DE8 File Offset: 0x0016EFE8
		public int ConversationId { get; set; }

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x06005954 RID: 22868 RVA: 0x00170DF1 File Offset: 0x0016EFF1
		// (set) Token: 0x06005955 RID: 22869 RVA: 0x00170DF9 File Offset: 0x0016EFF9
		public SocketActionType SocketActionType { get; set; }
	}
}
