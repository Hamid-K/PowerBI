using System;
using System.Data;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000903 RID: 2307
	internal class SqlParameter : ISqlParameter, IDbDataParameter, IDataParameter
	{
		// Token: 0x060048CC RID: 18636 RVA: 0x0010B94C File Offset: 0x00109B4C
		internal SqlParameter()
		{
			this.DrdaType = DrdaClientType.Int;
			this.Precision = 0;
			this.Scale = 0;
			this.Size = 0;
			this.DbType = DbType.Int32;
			this.Direction = ParameterDirection.Input;
			this.IsNullable = true;
			this.ParameterName = string.Empty;
			this.SourceColumn = string.Empty;
			this.SourceVersion = DataRowVersion.Default;
			this.Value = null;
			this.Ccsid = 0;
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x060048CD RID: 18637 RVA: 0x0010B9C0 File Offset: 0x00109BC0
		// (set) Token: 0x060048CE RID: 18638 RVA: 0x0010B9C8 File Offset: 0x00109BC8
		internal byte DrdaServerType { get; set; }

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x060048CF RID: 18639 RVA: 0x0010B9D1 File Offset: 0x00109BD1
		// (set) Token: 0x060048D0 RID: 18640 RVA: 0x0010B9D9 File Offset: 0x00109BD9
		public DrdaClientType DrdaType { get; set; }

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x060048D1 RID: 18641 RVA: 0x0010B9E2 File Offset: 0x00109BE2
		// (set) Token: 0x060048D2 RID: 18642 RVA: 0x0010B9EA File Offset: 0x00109BEA
		public byte Precision { get; set; }

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x060048D3 RID: 18643 RVA: 0x0010B9F3 File Offset: 0x00109BF3
		// (set) Token: 0x060048D4 RID: 18644 RVA: 0x0010B9FB File Offset: 0x00109BFB
		public byte Scale { get; set; }

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x060048D5 RID: 18645 RVA: 0x0010BA04 File Offset: 0x00109C04
		// (set) Token: 0x060048D6 RID: 18646 RVA: 0x0010BA0C File Offset: 0x00109C0C
		public int Size { get; set; }

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x060048D7 RID: 18647 RVA: 0x0010BA15 File Offset: 0x00109C15
		// (set) Token: 0x060048D8 RID: 18648 RVA: 0x0010BA1D File Offset: 0x00109C1D
		public DbType DbType { get; set; }

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x060048D9 RID: 18649 RVA: 0x0010BA26 File Offset: 0x00109C26
		// (set) Token: 0x060048DA RID: 18650 RVA: 0x0010BA2E File Offset: 0x00109C2E
		public ParameterDirection Direction { get; set; }

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x060048DB RID: 18651 RVA: 0x0010BA37 File Offset: 0x00109C37
		// (set) Token: 0x060048DC RID: 18652 RVA: 0x0010BA3F File Offset: 0x00109C3F
		public bool IsNullable { get; set; }

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x060048DD RID: 18653 RVA: 0x0010BA48 File Offset: 0x00109C48
		// (set) Token: 0x060048DE RID: 18654 RVA: 0x0010BA50 File Offset: 0x00109C50
		public string ParameterName { get; set; }

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x060048DF RID: 18655 RVA: 0x0010BA59 File Offset: 0x00109C59
		// (set) Token: 0x060048E0 RID: 18656 RVA: 0x0010BA61 File Offset: 0x00109C61
		public string SourceColumn { get; set; }

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x060048E1 RID: 18657 RVA: 0x0010BA6A File Offset: 0x00109C6A
		// (set) Token: 0x060048E2 RID: 18658 RVA: 0x0010BA72 File Offset: 0x00109C72
		public DataRowVersion SourceVersion { get; set; }

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x060048E3 RID: 18659 RVA: 0x0010BA7B File Offset: 0x00109C7B
		// (set) Token: 0x060048E4 RID: 18660 RVA: 0x0010BA83 File Offset: 0x00109C83
		public object Value { get; set; }

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x060048E5 RID: 18661 RVA: 0x0010BA8C File Offset: 0x00109C8C
		// (set) Token: 0x060048E6 RID: 18662 RVA: 0x0010BA94 File Offset: 0x00109C94
		internal short SqlType { get; set; }

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x060048E7 RID: 18663 RVA: 0x0010BA9D File Offset: 0x00109C9D
		// (set) Token: 0x060048E8 RID: 18664 RVA: 0x0010BAA5 File Offset: 0x00109CA5
		internal ushort Ccsid { get; set; }
	}
}
