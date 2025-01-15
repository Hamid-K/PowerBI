using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000148 RID: 328
	internal sealed class GetModelItemPoliciesActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0002F3A8 File Offset: 0x0002D5A8
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x0002F3B0 File Offset: 0x0002D5B0
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

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0002F3B9 File Offset: 0x0002D5B9
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x0002F3C1 File Offset: 0x0002D5C1
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

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0002F3CA File Offset: 0x0002D5CA
		// (set) Token: 0x06000CBD RID: 3261 RVA: 0x0002F3D2 File Offset: 0x0002D5D2
		public string Policy
		{
			get
			{
				return this.m_policy;
			}
			set
			{
				this.m_policy = value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0002F3DB File Offset: 0x0002D5DB
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x0002F3E3 File Offset: 0x0002D5E3
		public bool InheritParent
		{
			get
			{
				return this.m_inheritParent;
			}
			set
			{
				this.m_inheritParent = value;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0002F3EC File Offset: 0x0002D5EC
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.ItemPath, this.ModelItemID);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0002F409 File Offset: 0x0002D609
		internal override string OutputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.Policy, this.InheritParent);
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002F42B File Offset: 0x0002D62B
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("Model");
			}
		}

		// Token: 0x04000522 RID: 1314
		private string m_itemPath;

		// Token: 0x04000523 RID: 1315
		private string m_modelItemID;

		// Token: 0x04000524 RID: 1316
		private string m_policy;

		// Token: 0x04000525 RID: 1317
		private bool m_inheritParent;
	}
}
