using System;
using NLog.Config;

namespace NLog.Layouts
{
	// Token: 0x020000A0 RID: 160
	[NLogConfigurationItem]
	[ThreadAgnostic]
	[ThreadSafe]
	public class CsvColumn
	{
		// Token: 0x06000A67 RID: 2663 RVA: 0x0001AEED File Offset: 0x000190ED
		public CsvColumn()
			: this(null, null)
		{
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001AEF7 File Offset: 0x000190F7
		public CsvColumn(string name, Layout layout)
		{
			this.Name = name;
			this.Layout = layout;
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0001AF0D File Offset: 0x0001910D
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x0001AF15 File Offset: 0x00019115
		public string Name { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0001AF1E File Offset: 0x0001911E
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x0001AF26 File Offset: 0x00019126
		[RequiredParameter]
		public Layout Layout { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0001AF30 File Offset: 0x00019130
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x0001AF56 File Offset: 0x00019156
		public CsvQuotingMode Quoting
		{
			get
			{
				CsvQuotingMode? quoting = this._quoting;
				if (quoting == null)
				{
					return CsvQuotingMode.Auto;
				}
				return quoting.GetValueOrDefault();
			}
			set
			{
				this._quoting = new CsvQuotingMode?(value);
			}
		}

		// Token: 0x04000265 RID: 613
		internal CsvQuotingMode? _quoting;
	}
}
