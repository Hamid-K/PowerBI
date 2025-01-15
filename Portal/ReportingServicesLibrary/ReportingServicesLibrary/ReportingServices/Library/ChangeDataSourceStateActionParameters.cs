using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000E1 RID: 225
	internal sealed class ChangeDataSourceStateActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00025E06 File Offset: 0x00024006
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x00025E0E File Offset: 0x0002400E
		public string DataSourcePath
		{
			get
			{
				return this.m_dataSourcePath;
			}
			set
			{
				this.m_dataSourcePath = value;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00025E17 File Offset: 0x00024017
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x00025E1F File Offset: 0x0002401F
		public bool Enable
		{
			get
			{
				return this.m_enable;
			}
			set
			{
				this.m_enable = value;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00025E28 File Offset: 0x00024028
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.DataSourcePath, this.Enable);
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00025E4A File Offset: 0x0002404A
		internal override void Validate()
		{
			if (this.DataSourcePath == null)
			{
				throw new MissingParameterException("DataSource");
			}
		}

		// Token: 0x0400046A RID: 1130
		private string m_dataSourcePath;

		// Token: 0x0400046B RID: 1131
		private bool m_enable;
	}
}
