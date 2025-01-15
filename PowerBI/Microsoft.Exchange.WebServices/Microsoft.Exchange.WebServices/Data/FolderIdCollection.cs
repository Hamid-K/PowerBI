using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000061 RID: 97
	[EditorBrowsable(1)]
	public sealed class FolderIdCollection : ComplexPropertyCollection<FolderId>
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x0000FED1 File Offset: 0x0000EED1
		internal FolderIdCollection()
		{
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000FEE4 File Offset: 0x0000EEE4
		internal FolderIdCollection(IEnumerable<FolderId> folderIds)
		{
			if (folderIds != null)
			{
				folderIds.ForEach(delegate(FolderId folderId)
				{
					base.InternalAdd(folderId);
				});
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000FF13 File Offset: 0x0000EF13
		internal override FolderId CreateComplexProperty(string xmlElementName)
		{
			return new FolderId();
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000FF1A File Offset: 0x0000EF1A
		internal override FolderId CreateDefaultComplexProperty()
		{
			return new FolderId();
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000FF21 File Offset: 0x0000EF21
		internal override string GetCollectionItemXmlElementName(FolderId complexProperty)
		{
			return complexProperty.GetXmlElementName();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000FF29 File Offset: 0x0000EF29
		public void Add(FolderId folderId)
		{
			EwsUtilities.ValidateParam(folderId, "folderId");
			if (base.Contains(folderId))
			{
				throw new ArgumentException(Strings.IdAlreadyInList, "folderId");
			}
			base.InternalAdd(folderId);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000FF5C File Offset: 0x0000EF5C
		public FolderId Add(WellKnownFolderName folderName)
		{
			if (base.Contains(folderName))
			{
				throw new ArgumentException(Strings.IdAlreadyInList, "folderName");
			}
			FolderId folderId = new FolderId(folderName);
			base.InternalAdd(folderId);
			return folderId;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000FF9B File Offset: 0x0000EF9B
		public void Clear()
		{
			base.InternalClear();
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000FFA3 File Offset: 0x0000EFA3
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= base.Count)
			{
				throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
			}
			base.InternalRemoveAt(index);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000FFCE File Offset: 0x0000EFCE
		public bool Remove(FolderId folderId)
		{
			EwsUtilities.ValidateParam(folderId, "folderId");
			return base.InternalRemove(folderId);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000FFE2 File Offset: 0x0000EFE2
		public bool Remove(WellKnownFolderName folderName)
		{
			return base.InternalRemove(folderName);
		}
	}
}
