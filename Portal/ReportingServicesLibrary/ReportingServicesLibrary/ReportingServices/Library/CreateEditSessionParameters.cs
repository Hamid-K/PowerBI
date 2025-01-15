using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001C2 RID: 450
	internal class CreateEditSessionParameters : CreateReportActionParameters
	{
		// Token: 0x06000FE1 RID: 4065 RVA: 0x00038795 File Offset: 0x00036995
		public CreateEditSessionParameters()
		{
			base.Overwrite = true;
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x000387A4 File Offset: 0x000369A4
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x000387AC File Offset: 0x000369AC
		public string EditSessionID
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_editSessionID;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_editSessionID = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x000387B5 File Offset: 0x000369B5
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "Parent={0}, Report={1}", base.ParentPath, base.ItemName);
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x000387D2 File Offset: 0x000369D2
		internal override string OutputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "Edit SessionID: {0}", this.m_editSessionID);
			}
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x000387EC File Offset: 0x000369EC
		internal override void Validate()
		{
			if (base.ItemName == null)
			{
				throw new MissingParameterException("Report");
			}
			if (base.ReportDefinition == null)
			{
				throw new MissingParameterException("Definition");
			}
			if (!base.Overwrite)
			{
				throw new InternalCatalogException("Unexpected value for Overwrite");
			}
			if (base.Properties != null)
			{
				throw new InternalCatalogException("Unexpected value for Properties");
			}
		}

		// Token: 0x04000643 RID: 1603
		private string m_editSessionID;
	}
}
