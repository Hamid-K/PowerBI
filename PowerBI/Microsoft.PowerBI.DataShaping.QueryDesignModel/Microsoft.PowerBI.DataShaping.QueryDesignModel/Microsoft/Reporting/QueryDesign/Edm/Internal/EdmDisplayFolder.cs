using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001EF RID: 495
	public sealed class EdmDisplayFolder : ISupportsDisplayFolders
	{
		// Token: 0x0600179A RID: 6042 RVA: 0x00041131 File Offset: 0x0003F331
		internal EdmDisplayFolder(XElement element, EntityType entity)
			: this(element, entity, entity)
		{
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0004113C File Offset: 0x0003F33C
		internal EdmDisplayFolder(XElement element, EntityType entity, ISupportsDisplayFolders parent)
		{
			ArgumentValidation.CheckNotNull<XElement>(element, "element");
			this._name = element.GetStringAttributeOrDefault(Extensions.NameAttr, null);
			this._parent = ArgumentValidation.CheckNotNull<ISupportsDisplayFolders>(parent, "parent");
			this._caption = element.GetStringAttributeOrDefault(Extensions.CaptionAttr, null);
			this._fullPath = this.CreatePath(this.Caption);
			this._displayFolderId = this.CreatePath(this.Name);
			List<EdmDisplayFolder> list = new List<EdmDisplayFolder>();
			foreach (XElement xelement in element.Elements(Extensions.DisplayFolderElem))
			{
				list.Add(new EdmDisplayFolder(xelement, entity, this));
			}
			this._displayFolders = new EdmDisplayFolderCollection(list);
			this._folderItems = this.GetReadOnlyDisplayFolderMemberCollection(element, entity);
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x00041220 File Offset: 0x0003F420
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x00041228 File Offset: 0x0003F428
		public string FullPath
		{
			get
			{
				return this._fullPath;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x00041230 File Offset: 0x0003F430
		public EdmDisplayFolderCollection DisplayFolders
		{
			get
			{
				return this._displayFolders;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x00041238 File Offset: 0x0003F438
		public ReadOnlyCollection<IEntityMemberItem> FolderItems
		{
			get
			{
				return this._folderItems;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x00041240 File Offset: 0x0003F440
		public string Caption
		{
			get
			{
				return this._caption ?? this.Name;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x00041252 File Offset: 0x0003F452
		internal string DisplayFolderId
		{
			get
			{
				return this._displayFolderId;
			}
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0004125A File Offset: 0x0003F45A
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00041264 File Offset: 0x0003F464
		private ReadOnlyCollection<IEntityMemberItem> GetReadOnlyDisplayFolderMemberCollection(XElement element, EntityType entity)
		{
			IEnumerable<IEntityMemberItem> properties = from e in element.Elements(Extensions.PropertyRefElem)
				select EdmDisplayFolder.GetEntityMemberItem(e, entity, this);
			IEnumerable<IEntityMemberItem> enumerable = from item in element.Elements(Extensions.KpiRefElem)
				let entityMemberItem = EdmDisplayFolder.GetEntityMemberItem(item, entity, this)
				where entityMemberItem != null && !properties.Contains(entityMemberItem)
				select entityMemberItem;
			IEnumerable<IEntityMemberItem> enumerable2 = from e in element.Elements(Extensions.HierarchyRefElem)
				select EdmDisplayFolder.GetHierarchyItem(e, entity, this);
			return properties.Concat(enumerable).Concat(enumerable2).ToReadOnlyCollection<IEntityMemberItem>();
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0004132D File Offset: 0x0003F52D
		private string CreatePath(string identifier)
		{
			if (this._parent is EntityType)
			{
				return this.Caption;
			}
			return SR.DisplayFolderPath(((EdmDisplayFolder)this._parent).FullPath, identifier);
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0004135C File Offset: 0x0003F55C
		private static IEntityMemberItem GetEntityMemberItem(XElement element, EntityType entity, EdmDisplayFolder displayFolder)
		{
			IEntityMemberItem entityMemberItemFromRef = entity.GetEntityMemberItemFromRef(element);
			EdmProperty edmProperty = entityMemberItemFromRef as EdmProperty;
			if (edmProperty != null)
			{
				edmProperty.AddDisplayFolderParent(displayFolder);
			}
			return entityMemberItemFromRef;
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00041384 File Offset: 0x0003F584
		private static IEntityMemberItem GetHierarchyItem(XElement element, EntityType entity, EdmDisplayFolder displayFolder)
		{
			IEntityMemberItem entityMemberItemFromRef = entity.GetEntityMemberItemFromRef(element);
			EdmHierarchy edmHierarchy = entityMemberItemFromRef as EdmHierarchy;
			if (edmHierarchy != null)
			{
				edmHierarchy.AddDisplayFolderParent(displayFolder);
			}
			return entityMemberItemFromRef;
		}

		// Token: 0x04000CAA RID: 3242
		private readonly string _name;

		// Token: 0x04000CAB RID: 3243
		private readonly string _fullPath;

		// Token: 0x04000CAC RID: 3244
		private readonly string _caption;

		// Token: 0x04000CAD RID: 3245
		private readonly string _displayFolderId;

		// Token: 0x04000CAE RID: 3246
		private readonly ISupportsDisplayFolders _parent;

		// Token: 0x04000CAF RID: 3247
		private readonly EdmDisplayFolderCollection _displayFolders;

		// Token: 0x04000CB0 RID: 3248
		private readonly ReadOnlyCollection<IEntityMemberItem> _folderItems;
	}
}
