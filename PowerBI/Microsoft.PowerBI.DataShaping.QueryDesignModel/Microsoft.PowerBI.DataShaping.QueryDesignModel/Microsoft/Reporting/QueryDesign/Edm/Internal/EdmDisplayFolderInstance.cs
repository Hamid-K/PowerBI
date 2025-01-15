using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001F1 RID: 497
	internal struct EdmDisplayFolderInstance : IEdmItemInstance, IEquatable<EdmDisplayFolderInstance>, ICheckable
	{
		// Token: 0x060017A9 RID: 6057 RVA: 0x000413BF File Offset: 0x0003F5BF
		internal EdmDisplayFolderInstance(EntitySet entity, EdmDisplayFolder displayFolder)
		{
			this._displayFolder = ArgumentValidation.CheckNotNull<EdmDisplayFolder>(displayFolder, "displayFolder");
			this._entity = ArgumentValidation.CheckNotNull<EntitySet>(entity, "entity");
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x000413E4 File Offset: 0x0003F5E4
		public static EdmDisplayFolderInstance Empty
		{
			get
			{
				return default(EdmDisplayFolderInstance);
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x000413FA File Offset: 0x0003F5FA
		public bool IsValid
		{
			get
			{
				return this.DisplayFolder != null;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x00041405 File Offset: 0x0003F605
		public string Caption
		{
			get
			{
				return this.DisplayFolder.Caption;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x00041412 File Offset: 0x0003F612
		public EdmDisplayFolder DisplayFolder
		{
			get
			{
				return this._displayFolder;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x0004141A File Offset: 0x0003F61A
		public EntitySet Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x00041424 File Offset: 0x0003F624
		public QualifiedName QualifiedName
		{
			get
			{
				if (!this.IsValid)
				{
					return QualifiedName.Root;
				}
				return this.Entity.QualifiedName.Append(new Name(this.DisplayFolder.FullPath));
			}
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00041462 File Offset: 0x0003F662
		public static bool operator ==(EdmDisplayFolderInstance left, EdmDisplayFolderInstance right)
		{
			return left.Equals(right);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0004146C File Offset: 0x0003F66C
		public static bool operator !=(EdmDisplayFolderInstance left, EdmDisplayFolderInstance right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0004147C File Offset: 0x0003F67C
		public override bool Equals(object value)
		{
			EdmDisplayFolderInstance? edmDisplayFolderInstance = value as EdmDisplayFolderInstance?;
			return edmDisplayFolderInstance != null && this.Equals(edmDisplayFolderInstance.Value);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000414AD File Offset: 0x0003F6AD
		public override int GetHashCode()
		{
			if (this.DisplayFolder == null || this.Entity == null)
			{
				return base.GetType().GetHashCode();
			}
			return this.DisplayFolder.GetHashCode() ^ this.Entity.GetHashCode();
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x000414EC File Offset: 0x0003F6EC
		public IEnumerable<EdmDisplayFolderInstance> GetDisplayFolderInstances()
		{
			foreach (EdmDisplayFolder edmDisplayFolder in this.DisplayFolder.DisplayFolders)
			{
				yield return this.Entity.DisplayFolderInstance(edmDisplayFolder);
			}
			IEnumerator<EdmDisplayFolder> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00041501 File Offset: 0x0003F701
		public IEnumerable<EdmPropertyInstance> GetDisplayFolderItemPropertyInstances()
		{
			foreach (EdmProperty edmProperty in this.DisplayFolder.FolderItems.OfType<EdmProperty>())
			{
				yield return this.Entity.PropertyInstance(edmProperty);
			}
			IEnumerator<EdmProperty> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00041516 File Offset: 0x0003F716
		public IEnumerable<EdmHierarchyInstance> GetDisplayFolderItemHierarchyInstances()
		{
			foreach (EdmHierarchy edmHierarchy in this.DisplayFolder.FolderItems.OfType<EdmHierarchy>())
			{
				yield return this.Entity.HierarchyInstance(edmHierarchy);
			}
			IEnumerator<EdmHierarchy> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0004152B File Offset: 0x0003F72B
		public bool Equals(EdmDisplayFolderInstance other)
		{
			return object.Equals(this.DisplayFolder, other.DisplayFolder) && object.Equals(this.Entity, other.Entity);
		}

		// Token: 0x04000CB1 RID: 3249
		private readonly EdmDisplayFolder _displayFolder;

		// Token: 0x04000CB2 RID: 3250
		private readonly EntitySet _entity;
	}
}
