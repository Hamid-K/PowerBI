using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200014C RID: 332
	internal sealed class ListModelItemChildrenActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0002F72F File Offset: 0x0002D92F
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x0002F737 File Offset: 0x0002D937
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

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0002F740 File Offset: 0x0002D940
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x0002F748 File Offset: 0x0002D948
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

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0002F751 File Offset: 0x0002D951
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x0002F759 File Offset: 0x0002D959
		public bool Recursive
		{
			get
			{
				return this.m_recursive;
			}
			set
			{
				this.m_recursive = value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0002F762 File Offset: 0x0002D962
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x0002F76A File Offset: 0x0002D96A
		public ModelItem[] ModelItemChildren
		{
			get
			{
				return this.m_modelItemChildren;
			}
			set
			{
				this.m_modelItemChildren = value;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0002F773 File Offset: 0x0002D973
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", this.ItemPath, this.ModelItemID, this.Recursive);
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0002F79B File Offset: 0x0002D99B
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("Model");
			}
		}

		// Token: 0x0400052B RID: 1323
		private string m_itemPath;

		// Token: 0x0400052C RID: 1324
		private string m_modelItemID;

		// Token: 0x0400052D RID: 1325
		private bool m_recursive;

		// Token: 0x0400052E RID: 1326
		private ModelItem[] m_modelItemChildren;
	}
}
