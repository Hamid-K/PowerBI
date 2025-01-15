using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001A1 RID: 417
	internal sealed class SetReportDefinitionActionParameters : UpdateItemActionParameters
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00036C41 File Offset: 0x00034E41
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x00036C49 File Offset: 0x00034E49
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

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00036C52 File Offset: 0x00034E52
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x00036C5A File Offset: 0x00034E5A
		public byte[] Definition
		{
			get
			{
				return this.m_definition;
			}
			set
			{
				this.m_definition = value;
			}
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00036C63 File Offset: 0x00034E63
		internal override void Validate()
		{
			if (base.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
			if (this.Definition == null)
			{
				throw new MissingParameterException("Definition");
			}
		}

		// Token: 0x04000620 RID: 1568
		private Warning[] m_warnings;

		// Token: 0x04000621 RID: 1569
		private byte[] m_definition;
	}
}
