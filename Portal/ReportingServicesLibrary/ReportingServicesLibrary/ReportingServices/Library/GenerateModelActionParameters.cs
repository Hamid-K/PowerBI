using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200013E RID: 318
	internal sealed class GenerateModelActionParameters : CreateItemActionParameters
	{
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0002EA4C File Offset: 0x0002CC4C
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x0002EA54 File Offset: 0x0002CC54
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

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0002EA5D File Offset: 0x0002CC5D
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x0002EA65 File Offset: 0x0002CC65
		public Warning[] Warnings
		{
			get
			{
				return this.m_warnings;
			}
			set
			{
				this.m_warnings = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0002EA6E File Offset: 0x0002CC6E
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", this.DataSourcePath, base.ItemName, base.ParentPath);
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002EA91 File Offset: 0x0002CC91
		internal override void Validate()
		{
			if (this.DataSourcePath == null)
			{
				throw new MissingParameterException("DataSource");
			}
			if (base.ItemName == null)
			{
				throw new MissingParameterException("Model");
			}
			if (base.ParentPath == null)
			{
				throw new MissingParameterException("Parent");
			}
		}

		// Token: 0x04000518 RID: 1304
		private string m_dataSourcePath;

		// Token: 0x04000519 RID: 1305
		private Warning[] m_warnings;
	}
}
