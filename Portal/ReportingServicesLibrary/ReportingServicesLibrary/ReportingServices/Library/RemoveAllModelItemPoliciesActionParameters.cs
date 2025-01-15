using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000150 RID: 336
	internal sealed class RemoveAllModelItemPoliciesActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x0002FA21 File Offset: 0x0002DC21
		// (set) Token: 0x06000CEC RID: 3308 RVA: 0x0002FA29 File Offset: 0x0002DC29
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

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0002FA32 File Offset: 0x0002DC32
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002FA3A File Offset: 0x0002DC3A
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("Model");
			}
		}

		// Token: 0x04000531 RID: 1329
		private string m_itemPath;
	}
}
