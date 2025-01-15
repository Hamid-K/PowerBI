using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000202 RID: 514
	public sealed class EdmHierarchy : IEntityMemberItem
	{
		// Token: 0x06001829 RID: 6185 RVA: 0x000428CC File Offset: 0x00040ACC
		internal EdmHierarchy(XElement element, EntityType entity)
		{
			ArgumentValidation.CheckNotNull<XElement>(element, "element");
			this._caption = element.GetStringAttributeOrDefault(Extensions.CaptionAttr, null);
			XAttribute xattribute = ArgumentValidation.CheckNotNull<XAttribute>(element.Attribute(Extensions.NameAttr), "Name");
			this._name = ArgumentValidation.CheckNotNullOrEmpty(xattribute.Value, "Name");
			this._isHidden = element.GetBooleanAttributeOrDefault(Extensions.HiddenAttr, false);
			XAttribute xattribute2 = element.Attribute(Extensions.ReferenceNameAttr);
			if (xattribute2 != null)
			{
				this._referenceName = xattribute2.Value;
			}
			XElement elementOrNull = element.GetElementOrNull(Extensions.Documentation);
			if (elementOrNull != null)
			{
				XElement elementOrNull2 = elementOrNull.GetElementOrNull(Extensions.Summary);
				this._description = ((elementOrNull2 != null) ? elementOrNull2.Value : null);
			}
			List<EdmHierarchyLevel> list = new List<EdmHierarchyLevel>();
			IEnumerable<XElement> enumerable = element.Elements(Extensions.LevelElem);
			if (enumerable != null)
			{
				foreach (XElement xelement in enumerable)
				{
					list.Add(new EdmHierarchyLevel(this, xelement, entity));
				}
			}
			this._levels = new EdmHierarchyLevelCollection(list);
			this._displayFolderParents = new ObservableCollection<EdmDisplayFolder>();
			this._displayFolderParentsReadOnly = new ReadOnlyObservableCollection<EdmDisplayFolder>(this._displayFolderParents);
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x00042A10 File Offset: 0x00040C10
		public string Caption
		{
			get
			{
				return this._caption ?? this.Name;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x00042A22 File Offset: 0x00040C22
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x00042A2A File Offset: 0x00040C2A
		public string ReferenceName
		{
			get
			{
				return this._referenceName ?? this._name;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x00042A3C File Offset: 0x00040C3C
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x00042A44 File Offset: 0x00040C44
		public bool IsHidden
		{
			get
			{
				return this._isHidden;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x00042A4C File Offset: 0x00040C4C
		public EdmHierarchyLevelCollection Levels
		{
			get
			{
				return this._levels;
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00042A54 File Offset: 0x00040C54
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x00042A5C File Offset: 0x00040C5C
		public IEnumerable<EdmDisplayFolder> DisplayFolderParents
		{
			get
			{
				return this._displayFolderParentsReadOnly;
			}
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00042A64 File Offset: 0x00040C64
		internal void AddDisplayFolderParent(EdmDisplayFolder displayFolder)
		{
			ArgumentValidation.CheckNotNull<EdmDisplayFolder>(displayFolder, "displayFolder");
			this._displayFolderParents.Add(displayFolder);
		}

		// Token: 0x04000CF6 RID: 3318
		private readonly string _caption;

		// Token: 0x04000CF7 RID: 3319
		private readonly string _name;

		// Token: 0x04000CF8 RID: 3320
		private readonly string _referenceName;

		// Token: 0x04000CF9 RID: 3321
		private readonly string _description;

		// Token: 0x04000CFA RID: 3322
		private readonly bool _isHidden;

		// Token: 0x04000CFB RID: 3323
		private readonly EdmHierarchyLevelCollection _levels;

		// Token: 0x04000CFC RID: 3324
		private readonly ObservableCollection<EdmDisplayFolder> _displayFolderParents;

		// Token: 0x04000CFD RID: 3325
		private readonly ReadOnlyObservableCollection<EdmDisplayFolder> _displayFolderParentsReadOnly;
	}
}
