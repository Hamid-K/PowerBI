using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001C9 RID: 457
	[DebuggerDisplay("{ItemPath}")]
	internal sealed class UpdateCacheActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x00038F27 File Offset: 0x00037127
		// (set) Token: 0x06001007 RID: 4103 RVA: 0x00038F2F File Offset: 0x0003712F
		public string ItemPath
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x00038F38 File Offset: 0x00037138
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x00038F40 File Offset: 0x00037140
		public NameValueCollection Parameters
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x00038F49 File Offset: 0x00037149
		// (set) Token: 0x0600100B RID: 4107 RVA: 0x00038F51 File Offset: 0x00037151
		public JobTypeEnum JobType
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_jobType;
			}
			set
			{
				this.m_jobType = value;
			}
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00038F5A File Offset: 0x0003715A
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.ItemPath))
			{
				throw new MissingParameterException("ItemPath");
			}
			if (this.Parameters == null)
			{
				throw new MissingParameterException("Parameters");
			}
		}

		// Token: 0x04000649 RID: 1609
		private string m_itemPath;

		// Token: 0x0400064A RID: 1610
		private NameValueCollection m_parameters;

		// Token: 0x0400064B RID: 1611
		private JobTypeEnum m_jobType;
	}
}
