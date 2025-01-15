using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001A6 RID: 422
	internal sealed class SetReportParametersActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x00036F46 File Offset: 0x00035146
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x00036F4E File Offset: 0x0003514E
		public string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00036F57 File Offset: 0x00035157
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x00036F5F File Offset: 0x0003515F
		public ParameterInfoCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x00036F68 File Offset: 0x00035168
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00036F70 File Offset: 0x00035170
		internal override void Validate()
		{
			if (this.Parameters == null)
			{
				throw new MissingParameterException("Parameters");
			}
		}

		// Token: 0x04000624 RID: 1572
		private string m_itemPath;

		// Token: 0x04000625 RID: 1573
		private ParameterInfoCollection m_parameters;
	}
}
