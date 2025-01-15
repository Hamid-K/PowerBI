using System;
using System.Collections.Generic;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000207 RID: 519
	public struct EdmHierarchyLevelInstance : IEdmFieldInstance, IEquatable<IEdmFieldInstance>, IEquatable<EdmHierarchyLevelInstance>, ICheckable
	{
		// Token: 0x0600184B RID: 6219 RVA: 0x00042D48 File Offset: 0x00040F48
		internal EdmHierarchyLevelInstance(EntitySet entity, EdmHierarchyLevel hierarchyLevel)
		{
			this._entity = ArgumentValidation.CheckNotNull<EntitySet>(entity, "entity");
			this._hierarchyLevel = ArgumentValidation.CheckNotNull<EdmHierarchyLevel>(hierarchyLevel, "hierarchyLevel");
			ArgumentValidation.CheckCondition(this._entity.ElementType.Hierarchies.Contains(hierarchyLevel.ParentHierarchy), "hierarchyLevel");
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x00042D9C File Offset: 0x00040F9C
		public static EdmHierarchyLevelInstance Empty
		{
			get
			{
				return default(EdmHierarchyLevelInstance);
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x00042DB2 File Offset: 0x00040FB2
		public bool IsValid
		{
			get
			{
				return this.HierarchyLevel != null;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x00042DBD File Offset: 0x00040FBD
		public EntitySet Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x00042DC5 File Offset: 0x00040FC5
		public EdmHierarchyLevel HierarchyLevel
		{
			get
			{
				return this._hierarchyLevel;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x00042DCD File Offset: 0x00040FCD
		public EdmHierarchy Hierarchy
		{
			get
			{
				if (this._hierarchyLevel == null)
				{
					return null;
				}
				return this._hierarchyLevel.ParentHierarchy;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x00042DE4 File Offset: 0x00040FE4
		public QualifiedName QualifiedName
		{
			get
			{
				if (!this.IsValid)
				{
					return QualifiedName.Root;
				}
				return this.Entity.QualifiedName.Append(new Name(this.Hierarchy.Name)).Append(new Name(this.HierarchyLevel.Name));
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x00042E3A File Offset: 0x0004103A
		EdmField IEdmFieldInstance.Field
		{
			get
			{
				if (this.IsValid)
				{
					return this.HierarchyLevel.Source;
				}
				return null;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x00042E51 File Offset: 0x00041051
		string IEdmFieldInstance.Caption
		{
			get
			{
				if (this.IsValid)
				{
					return this.HierarchyLevel.Caption;
				}
				return string.Empty;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x00042E6C File Offset: 0x0004106C
		string IEdmFieldInstance.ParentCaption
		{
			get
			{
				if (this.IsValid)
				{
					return this.Entity.Caption;
				}
				return string.Empty;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x00042E87 File Offset: 0x00041087
		public IEnumerable<EdmDisplayFolder> DisplayFolderParents
		{
			get
			{
				return this.Hierarchy.DisplayFolderParents;
			}
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00042E94 File Offset: 0x00041094
		EdmPropertyInstance IEdmFieldInstance.ToPropertyInstance()
		{
			return this;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00042EA1 File Offset: 0x000410A1
		public static implicit operator EdmPropertyInstance(EdmHierarchyLevelInstance hierarchyLevelInstance)
		{
			if (hierarchyLevelInstance == EdmHierarchyLevelInstance.Empty)
			{
				return EdmPropertyInstance.Empty;
			}
			return hierarchyLevelInstance.Entity.PropertyInstance(hierarchyLevelInstance.HierarchyLevel);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00042EC9 File Offset: 0x000410C9
		public static bool operator ==(EdmHierarchyLevelInstance left, EdmHierarchyLevelInstance right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x00042ED3 File Offset: 0x000410D3
		public static bool operator !=(EdmHierarchyLevelInstance left, EdmHierarchyLevelInstance right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00042EE0 File Offset: 0x000410E0
		public override bool Equals(object value)
		{
			EdmHierarchyLevelInstance? edmHierarchyLevelInstance = value as EdmHierarchyLevelInstance?;
			return edmHierarchyLevelInstance != null && this.Equals(edmHierarchyLevelInstance.Value);
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00042F14 File Offset: 0x00041114
		public override int GetHashCode()
		{
			if (this.HierarchyLevel == null && this.Hierarchy == null)
			{
				return base.GetType().GetHashCode();
			}
			return this.HierarchyLevel.GetHashCode() ^ this.Hierarchy.GetHashCode() ^ this.Entity.GetHashCode();
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00042F6A File Offset: 0x0004116A
		public bool Equals(EdmHierarchyLevelInstance other)
		{
			return object.Equals(this.HierarchyLevel, other.HierarchyLevel) && object.Equals(this.Hierarchy, other.Hierarchy) && object.Equals(this.Entity, other.Entity);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00042FA8 File Offset: 0x000411A8
		public bool Equals(IEdmFieldInstance other)
		{
			return other is EdmHierarchyLevelInstance && this.Equals((EdmHierarchyLevelInstance)other);
		}

		// Token: 0x04000D05 RID: 3333
		private readonly EntitySet _entity;

		// Token: 0x04000D06 RID: 3334
		private readonly EdmHierarchyLevel _hierarchyLevel;
	}
}
