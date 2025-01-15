using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000152 RID: 338
	internal sealed class SetDrillthroughReportsActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0002FBA4 File Offset: 0x0002DDA4
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x0002FBAC File Offset: 0x0002DDAC
		public string ModelPath
		{
			get
			{
				return this.m_modelPath;
			}
			set
			{
				this.m_modelPath = value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0002FBB5 File Offset: 0x0002DDB5
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x0002FBBD File Offset: 0x0002DDBD
		public string ModelItemID
		{
			get
			{
				return this.m_modelItemID;
			}
			set
			{
				this.m_modelItemID = value;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0002FBC6 File Offset: 0x0002DDC6
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x0002FBCE File Offset: 0x0002DDCE
		public ModelDrillthroughReport[] Reports
		{
			get
			{
				return this.m_reports;
			}
			set
			{
				this.m_reports = value;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0002FBD7 File Offset: 0x0002DDD7
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.ModelPath, this.ModelItemID);
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0002FBF4 File Offset: 0x0002DDF4
		internal override void Validate()
		{
			if (this.ModelPath == null)
			{
				throw new MissingParameterException("Model");
			}
			if (this.ModelItemID == null)
			{
				throw new MissingParameterException("ModelItemID");
			}
			if (this.Reports == null)
			{
				throw new MissingParameterException("Reports");
			}
			if (this.Reports.Length > 2)
			{
				throw new InvalidParameterException("Reports");
			}
			if (this.Reports.Length == 2 && this.Reports[1] != null && this.Reports[1].Type == this.Reports[0].Type)
			{
				throw new InvalidParameterException("Reports");
			}
		}

		// Token: 0x04000532 RID: 1330
		private string m_modelPath;

		// Token: 0x04000533 RID: 1331
		private string m_modelItemID;

		// Token: 0x04000534 RID: 1332
		private ModelDrillthroughReport[] m_reports;
	}
}
