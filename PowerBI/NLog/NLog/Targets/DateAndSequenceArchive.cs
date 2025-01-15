using System;

namespace NLog.Targets
{
	// Token: 0x02000034 RID: 52
	internal class DateAndSequenceArchive
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000C7AB File Offset: 0x0000A9AB
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0000C7B3 File Offset: 0x0000A9B3
		public string FileName { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000C7BC File Offset: 0x0000A9BC
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0000C7C4 File Offset: 0x0000A9C4
		public DateTime Date { get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000C7CD File Offset: 0x0000A9CD
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0000C7D5 File Offset: 0x0000A9D5
		public int Sequence { get; private set; }

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		public bool HasSameFormattedDate(DateTime date)
		{
			return string.Equals(date.ToString(this._dateFormat), this.Date.ToString(this._dateFormat), StringComparison.Ordinal);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000C814 File Offset: 0x0000AA14
		public DateAndSequenceArchive(string fileName, DateTime date, string dateFormat, int sequence)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (dateFormat == null)
			{
				throw new ArgumentNullException("dateFormat");
			}
			this.Date = date;
			this._dateFormat = dateFormat;
			this.Sequence = sequence;
			this.FileName = fileName;
		}

		// Token: 0x040000BA RID: 186
		private readonly string _dateFormat;
	}
}
