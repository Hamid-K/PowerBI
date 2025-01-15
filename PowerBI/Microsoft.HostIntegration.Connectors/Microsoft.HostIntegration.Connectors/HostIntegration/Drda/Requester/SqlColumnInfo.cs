using System;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200090C RID: 2316
	internal class SqlColumnInfo : IColumnInfo
	{
		// Token: 0x06004911 RID: 18705 RVA: 0x0010D1FC File Offset: 0x0010B3FC
		public SqlColumnInfo()
		{
			this.ColumnName = null;
			this.DrdaType = DrdaClientType.Int;
			this.Length = 0;
			this.Precision = 0;
			this.Scale = 0;
			this.SqlType = 0;
			this.Ccsid = 0;
			this.IsNullable = false;
			this.IsLob = false;
			this.DrdaServerType = 2;
			this.DataLength = 0;
			this.GeneratedIdType = 0;
			this.BaseTable = null;
			this.Schema = null;
			this.Catalog = null;
			this.IsKey = false;
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06004912 RID: 18706 RVA: 0x0010D27F File Offset: 0x0010B47F
		// (set) Token: 0x06004913 RID: 18707 RVA: 0x0010D287 File Offset: 0x0010B487
		public string ColumnName { get; internal set; }

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x0010D290 File Offset: 0x0010B490
		// (set) Token: 0x06004915 RID: 18709 RVA: 0x0010D298 File Offset: 0x0010B498
		public DrdaClientType DrdaType { get; internal set; }

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06004916 RID: 18710 RVA: 0x0010D2A1 File Offset: 0x0010B4A1
		// (set) Token: 0x06004917 RID: 18711 RVA: 0x0010D2A9 File Offset: 0x0010B4A9
		public int Length { get; internal set; }

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06004918 RID: 18712 RVA: 0x0010D2B2 File Offset: 0x0010B4B2
		// (set) Token: 0x06004919 RID: 18713 RVA: 0x0010D2BA File Offset: 0x0010B4BA
		public short Precision { get; internal set; }

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x0600491A RID: 18714 RVA: 0x0010D2C3 File Offset: 0x0010B4C3
		// (set) Token: 0x0600491B RID: 18715 RVA: 0x0010D2CB File Offset: 0x0010B4CB
		public short Scale { get; internal set; }

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x0600491C RID: 18716 RVA: 0x0010D2D4 File Offset: 0x0010B4D4
		// (set) Token: 0x0600491D RID: 18717 RVA: 0x0010D2DC File Offset: 0x0010B4DC
		public short SqlType { get; internal set; }

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x0600491E RID: 18718 RVA: 0x0010D2E5 File Offset: 0x0010B4E5
		// (set) Token: 0x0600491F RID: 18719 RVA: 0x0010D2ED File Offset: 0x0010B4ED
		public ushort Ccsid { get; internal set; }

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x06004920 RID: 18720 RVA: 0x0010D2F6 File Offset: 0x0010B4F6
		// (set) Token: 0x06004921 RID: 18721 RVA: 0x0010D2FE File Offset: 0x0010B4FE
		public bool IsNullable { get; internal set; }

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06004922 RID: 18722 RVA: 0x0010D307 File Offset: 0x0010B507
		// (set) Token: 0x06004923 RID: 18723 RVA: 0x0010D30F File Offset: 0x0010B50F
		public bool IsLob { get; internal set; }

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06004924 RID: 18724 RVA: 0x0010D318 File Offset: 0x0010B518
		// (set) Token: 0x06004925 RID: 18725 RVA: 0x0010D320 File Offset: 0x0010B520
		public string BaseTable { get; internal set; }

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06004926 RID: 18726 RVA: 0x0010D329 File Offset: 0x0010B529
		// (set) Token: 0x06004927 RID: 18727 RVA: 0x0010D331 File Offset: 0x0010B531
		public string Schema { get; internal set; }

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06004928 RID: 18728 RVA: 0x0010D33A File Offset: 0x0010B53A
		// (set) Token: 0x06004929 RID: 18729 RVA: 0x0010D342 File Offset: 0x0010B542
		public string Catalog { get; internal set; }

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x0600492A RID: 18730 RVA: 0x0010D34B File Offset: 0x0010B54B
		// (set) Token: 0x0600492B RID: 18731 RVA: 0x0010D353 File Offset: 0x0010B553
		public bool IsKey { get; internal set; }

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x0600492C RID: 18732 RVA: 0x0010D35C File Offset: 0x0010B55C
		// (set) Token: 0x0600492D RID: 18733 RVA: 0x0010D364 File Offset: 0x0010B564
		public short GeneratedIdType { get; internal set; }

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x0600492E RID: 18734 RVA: 0x0010D36D File Offset: 0x0010B56D
		// (set) Token: 0x0600492F RID: 18735 RVA: 0x0010D375 File Offset: 0x0010B575
		internal byte DrdaServerType { get; set; }

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06004930 RID: 18736 RVA: 0x0010D37E File Offset: 0x0010B57E
		// (set) Token: 0x06004931 RID: 18737 RVA: 0x0010D386 File Offset: 0x0010B586
		internal int DataLength { get; set; }
	}
}
