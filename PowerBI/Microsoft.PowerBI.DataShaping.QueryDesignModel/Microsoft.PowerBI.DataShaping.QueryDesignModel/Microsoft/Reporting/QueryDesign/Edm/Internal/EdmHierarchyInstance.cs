using System;
using System.Collections.Generic;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000206 RID: 518
	public struct EdmHierarchyInstance : IEdmItemInstance, IEquatable<EdmHierarchyInstance>, ICheckable
	{
		// Token: 0x0600183E RID: 6206 RVA: 0x00042BBF File Offset: 0x00040DBF
		internal EdmHierarchyInstance(EntitySet entity, EdmHierarchy hierarchy)
		{
			this._hierarchy = ArgumentValidation.CheckNotNull<EdmHierarchy>(hierarchy, "hierarchy");
			this._entity = ArgumentValidation.CheckNotNull<EntitySet>(entity, "entity");
			ArgumentValidation.CheckCondition(entity.ElementType.Hierarchies.Contains(hierarchy), "hierarchy");
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x00042C00 File Offset: 0x00040E00
		public static EdmHierarchyInstance Empty
		{
			get
			{
				return default(EdmHierarchyInstance);
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x00042C16 File Offset: 0x00040E16
		public bool IsValid
		{
			get
			{
				return this.Hierarchy != null;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x00042C21 File Offset: 0x00040E21
		public EdmHierarchy Hierarchy
		{
			get
			{
				return this._hierarchy;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x00042C29 File Offset: 0x00040E29
		public EntitySet Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x00042C34 File Offset: 0x00040E34
		public QualifiedName QualifiedName
		{
			get
			{
				if (!this.IsValid)
				{
					return QualifiedName.Root;
				}
				return this.Entity.QualifiedName.Append(new Name(this.Hierarchy.Name));
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00042C72 File Offset: 0x00040E72
		public IEnumerable<EdmDisplayFolder> DisplayFolderParents
		{
			get
			{
				return this.Hierarchy.DisplayFolderParents;
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00042C7F File Offset: 0x00040E7F
		public static bool operator ==(EdmHierarchyInstance left, EdmHierarchyInstance right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00042C89 File Offset: 0x00040E89
		public static bool operator !=(EdmHierarchyInstance left, EdmHierarchyInstance right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00042C98 File Offset: 0x00040E98
		public override bool Equals(object value)
		{
			EdmHierarchyInstance? edmHierarchyInstance = value as EdmHierarchyInstance?;
			return edmHierarchyInstance != null && this.Equals(edmHierarchyInstance.Value);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00042CC9 File Offset: 0x00040EC9
		public override int GetHashCode()
		{
			if (this.Hierarchy == null && this.Entity == null)
			{
				return base.GetType().GetHashCode();
			}
			return this.Hierarchy.GetHashCode() ^ this.Entity.GetHashCode();
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00042D08 File Offset: 0x00040F08
		public IEnumerable<EdmPropertyInstance> GetLevelInstances()
		{
			foreach (EdmHierarchyLevel edmHierarchyLevel in this.Hierarchy.Levels)
			{
				yield return this.Entity.PropertyInstance(edmHierarchyLevel);
			}
			IEnumerator<EdmHierarchyLevel> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00042D1D File Offset: 0x00040F1D
		public bool Equals(EdmHierarchyInstance other)
		{
			return object.Equals(this.Hierarchy, other.Hierarchy) && object.Equals(this.Entity, other.Entity);
		}

		// Token: 0x04000D03 RID: 3331
		private readonly EdmHierarchy _hierarchy;

		// Token: 0x04000D04 RID: 3332
		private readonly EntitySet _entity;
	}
}
