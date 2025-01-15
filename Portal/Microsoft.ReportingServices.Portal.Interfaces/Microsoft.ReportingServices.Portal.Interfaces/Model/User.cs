using System;

namespace Model
{
	// Token: 0x02000067 RID: 103
	public class User
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000038F3 File Offset: 0x00001AF3
		// (set) Token: 0x060002DE RID: 734 RVA: 0x000038FB File Offset: 0x00001AFB
		public Guid Id { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00003904 File Offset: 0x00001B04
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000390C File Offset: 0x00001B0C
		public string Username { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00003915 File Offset: 0x00001B15
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000391D File Offset: 0x00001B1D
		public string DisplayName { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00003926 File Offset: 0x00001B26
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000392E File Offset: 0x00001B2E
		public bool HasFavoriteItems { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00003937 File Offset: 0x00001B37
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0000393F File Offset: 0x00001B3F
		public string MyReportsPath { get; set; }
	}
}
