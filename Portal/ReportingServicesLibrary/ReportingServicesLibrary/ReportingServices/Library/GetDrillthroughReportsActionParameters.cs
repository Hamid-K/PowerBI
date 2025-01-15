using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000142 RID: 322
	internal sealed class GetDrillthroughReportsActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0002F044 File Offset: 0x0002D244
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x0002F04C File Offset: 0x0002D24C
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

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0002F055 File Offset: 0x0002D255
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x0002F05D File Offset: 0x0002D25D
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

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0002F066 File Offset: 0x0002D266
		// (set) Token: 0x06000C9C RID: 3228 RVA: 0x0002F06E File Offset: 0x0002D26E
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

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0002F077 File Offset: 0x0002D277
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.ModelPath, this.ModelItemID);
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0002F094 File Offset: 0x0002D294
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
		}

		// Token: 0x0400051A RID: 1306
		private string m_modelPath;

		// Token: 0x0400051B RID: 1307
		private string m_modelItemID;

		// Token: 0x0400051C RID: 1308
		private ModelDrillthroughReport[] m_reports;
	}
}
