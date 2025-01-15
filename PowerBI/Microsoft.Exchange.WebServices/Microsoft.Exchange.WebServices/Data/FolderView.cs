using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002F9 RID: 761
	public sealed class FolderView : PagedView
	{
		// Token: 0x06001AF4 RID: 6900 RVA: 0x00048726 File Offset: 0x00047726
		internal override string GetViewXmlElementName()
		{
			return "IndexedPageFolderView";
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0004872D File Offset: 0x0004772D
		internal override string GetViewJsonTypeName()
		{
			return "IndexedPageView";
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00048734 File Offset: 0x00047734
		internal override ServiceObjectType GetServiceObjectType()
		{
			return ServiceObjectType.Folder;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00048737 File Offset: 0x00047737
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("Traversal", this.Traversal);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0004874F File Offset: 0x0004774F
		internal override void AddJsonProperties(JsonObject jsonRequest, ExchangeService service)
		{
			jsonRequest.Add("Traversal", this.Traversal);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00048767 File Offset: 0x00047767
		public FolderView(int pageSize)
			: base(pageSize)
		{
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00048770 File Offset: 0x00047770
		public FolderView(int pageSize, int offset)
			: base(pageSize, offset)
		{
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0004877A File Offset: 0x0004777A
		public FolderView(int pageSize, int offset, OffsetBasePoint offsetBasePoint)
			: base(pageSize, offset, offsetBasePoint)
		{
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x00048785 File Offset: 0x00047785
		// (set) Token: 0x06001AFD RID: 6909 RVA: 0x0004878D File Offset: 0x0004778D
		public FolderTraversal Traversal
		{
			get
			{
				return this.traversal;
			}
			set
			{
				this.traversal = value;
			}
		}

		// Token: 0x04001435 RID: 5173
		private FolderTraversal traversal;
	}
}
