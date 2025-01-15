using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000F5 RID: 245
	internal class CreateDataSetActionParameters : CreateItemActionParameters
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x00027189 File Offset: 0x00025389
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x00027191 File Offset: 0x00025391
		public byte[] DataSetDefinition
		{
			get
			{
				return this.m_dataSetDefinition;
			}
			set
			{
				this.m_dataSetDefinition = value;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0002719A File Offset: 0x0002539A
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x000271A2 File Offset: 0x000253A2
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

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0000FB45 File Offset: 0x0000DD45
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", base.ItemName, base.ParentPath, base.Overwrite);
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000271AB File Offset: 0x000253AB
		internal override void Validate()
		{
			if (base.ItemName == null)
			{
				throw new MissingParameterException("Name");
			}
			if (base.ParentPath == null)
			{
				throw new MissingParameterException("Parent");
			}
			if (this.DataSetDefinition == null)
			{
				throw new MissingParameterException("Definition");
			}
		}

		// Token: 0x04000481 RID: 1153
		private byte[] m_dataSetDefinition;

		// Token: 0x04000482 RID: 1154
		private Warning[] m_warnings;
	}
}
