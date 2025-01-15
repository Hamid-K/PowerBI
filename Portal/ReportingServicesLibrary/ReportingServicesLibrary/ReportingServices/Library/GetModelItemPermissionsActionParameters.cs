using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000146 RID: 326
	internal sealed class GetModelItemPermissionsActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0002F231 File Offset: 0x0002D431
		// (set) Token: 0x06000CAD RID: 3245 RVA: 0x0002F239 File Offset: 0x0002D439
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

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0002F242 File Offset: 0x0002D442
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x0002F24A File Offset: 0x0002D44A
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

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0002F253 File Offset: 0x0002D453
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x0002F25B File Offset: 0x0002D45B
		public string[] Permissions
		{
			get
			{
				return this.m_permissions;
			}
			set
			{
				this.m_permissions = value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0002F264 File Offset: 0x0002D464
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.ItemPath, this.ModelItemID);
			}
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002F281 File Offset: 0x0002D481
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("Model");
			}
		}

		// Token: 0x0400051F RID: 1311
		private string m_itemPath;

		// Token: 0x04000520 RID: 1312
		private string m_modelItemID;

		// Token: 0x04000521 RID: 1313
		private string[] m_permissions;
	}
}
