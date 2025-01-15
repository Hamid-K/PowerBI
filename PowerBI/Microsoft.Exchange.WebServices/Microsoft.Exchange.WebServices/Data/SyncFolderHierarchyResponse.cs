using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000185 RID: 389
	public sealed class SyncFolderHierarchyResponse : SyncResponse<Folder, FolderChange>
	{
		// Token: 0x06001125 RID: 4389 RVA: 0x00032416 File Offset: 0x00031416
		internal SyncFolderHierarchyResponse(PropertySet propertySet)
			: base(propertySet)
		{
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0003241F File Offset: 0x0003141F
		internal override string GetIncludesLastInRangeXmlElementName()
		{
			return "IncludesLastFolderInRange";
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00032426 File Offset: 0x00031426
		internal override FolderChange CreateChangeInstance()
		{
			return new FolderChange();
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0003242D File Offset: 0x0003142D
		internal override string GetChangeElementName()
		{
			return "Folder";
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00032434 File Offset: 0x00031434
		internal override string GetChangeIdElementName()
		{
			return "FolderId";
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0003243B File Offset: 0x0003143B
		internal override bool SummaryPropertiesOnly
		{
			get
			{
				return false;
			}
		}
	}
}
