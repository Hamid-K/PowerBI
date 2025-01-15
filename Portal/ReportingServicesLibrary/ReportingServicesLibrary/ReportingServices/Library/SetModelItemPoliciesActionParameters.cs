using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000154 RID: 340
	internal sealed class SetModelItemPoliciesActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0002FEE5 File Offset: 0x0002E0E5
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x0002FEED File Offset: 0x0002E0ED
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

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0002FEF6 File Offset: 0x0002E0F6
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x0002FEFE File Offset: 0x0002E0FE
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

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0002FF07 File Offset: 0x0002E107
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x0002FF0F File Offset: 0x0002E10F
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

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0002FF18 File Offset: 0x0002E118
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x0002FF20 File Offset: 0x0002E120
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

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0002FF29 File Offset: 0x0002E129
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", this.ItemPath, this.ModelItemID, this.InheritParent);
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0002FF51 File Offset: 0x0002E151
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("Model");
			}
			if (!this.InheritParent && this.Policy == null)
			{
				throw new MissingParameterException("Policies");
			}
		}

		// Token: 0x04000535 RID: 1333
		private string m_itemPath;

		// Token: 0x04000536 RID: 1334
		private string m_modelItemID;

		// Token: 0x04000537 RID: 1335
		private string m_policy;

		// Token: 0x04000538 RID: 1336
		private bool m_inheritParent;
	}
}
