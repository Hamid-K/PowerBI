using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200041E RID: 1054
	public class TableRow
	{
		// Token: 0x0600217B RID: 8571 RVA: 0x0008103D File Offset: 0x0007F23D
		public TableRow()
		{
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x0008105B File Offset: 0x0007F25B
		public TableRow(int cellCount, Unit rowHeight)
		{
			this.m_Height = rowHeight;
			while (cellCount > 0)
			{
				this.m_TableCells.Add(new TableCell());
				cellCount--;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x0008109B File Offset: 0x0007F29B
		// (set) Token: 0x0600217E RID: 8574 RVA: 0x000810A3 File Offset: 0x0007F2A3
		public List<TableCell> TableCells
		{
			get
			{
				return this.m_TableCells;
			}
			set
			{
				this.m_TableCells = value;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x000810AC File Offset: 0x0007F2AC
		// (set) Token: 0x06002180 RID: 8576 RVA: 0x000810B4 File Offset: 0x0007F2B4
		public Visibility Visibility
		{
			get
			{
				return this.m_Visibility;
			}
			set
			{
				this.m_Visibility = value;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x000810BD File Offset: 0x0007F2BD
		// (set) Token: 0x06002182 RID: 8578 RVA: 0x000810C5 File Offset: 0x0007F2C5
		public Unit Height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x04000EAF RID: 3759
		private List<TableCell> m_TableCells = new List<TableCell>();

		// Token: 0x04000EB0 RID: 3760
		private Visibility m_Visibility = new Visibility();

		// Token: 0x04000EB1 RID: 3761
		private Unit m_Height;
	}
}
