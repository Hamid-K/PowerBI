using System;
using System.Collections.Generic;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000212 RID: 530
	public struct EdmPropertyInstance : IEdmItemInstance, IEquatable<EdmPropertyInstance>, ICheckable
	{
		// Token: 0x060018B0 RID: 6320 RVA: 0x0004372D File Offset: 0x0004192D
		internal EdmPropertyInstance(EntitySet entity, EdmHierarchyLevel hierarchyLevel)
		{
			this = new EdmPropertyInstance(entity, hierarchyLevel.Source);
			this._hierarchyLevel = hierarchyLevel;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00043744 File Offset: 0x00041944
		internal EdmPropertyInstance(EntitySet entity, EdmProperty property)
		{
			this._hierarchyLevel = null;
			this._property = ArgumentValidation.CheckNotNull<EdmProperty>(property, "property");
			this._entity = ArgumentValidation.CheckNotNull<EntitySet>(entity, "entity");
			ArgumentValidation.CheckCondition(entity.ElementType == property.DeclaringType, "property");
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x00043794 File Offset: 0x00041994
		public static EdmPropertyInstance Empty
		{
			get
			{
				return default(EdmPropertyInstance);
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x000437AA File Offset: 0x000419AA
		public bool IsValid
		{
			get
			{
				return this.Property != null;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x000437B5 File Offset: 0x000419B5
		public EdmProperty Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x000437BD File Offset: 0x000419BD
		public EntitySet Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x000437C8 File Offset: 0x000419C8
		public QualifiedName QualifiedName
		{
			get
			{
				if (!this.IsValid)
				{
					return QualifiedName.Root;
				}
				if (this._hierarchyLevel != null)
				{
					return this.ToEdmHierarchyLevelInstance().QualifiedName;
				}
				return this.Entity.QualifiedName.Append(new Name(this.Property.Name));
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x00043820 File Offset: 0x00041A20
		public string Caption
		{
			get
			{
				IEdmFieldInstance edmFieldInstance = this.ToIEdmFieldInstance();
				if (edmFieldInstance.IsValid)
				{
					return edmFieldInstance.Caption;
				}
				return this.Property.Caption;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0004384E File Offset: 0x00041A4E
		internal bool IsHierarchyLevel
		{
			get
			{
				return this._hierarchyLevel != null;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x00043859 File Offset: 0x00041A59
		public IEnumerable<EdmDisplayFolder> DisplayFolderParents
		{
			get
			{
				return this.Property.DisplayFolderParents;
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00043866 File Offset: 0x00041A66
		public static bool operator ==(EdmPropertyInstance left, EdmPropertyInstance right)
		{
			return left.Equals(right);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00043870 File Offset: 0x00041A70
		public static bool operator !=(EdmPropertyInstance left, EdmPropertyInstance right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00043880 File Offset: 0x00041A80
		public EdmMeasureInstance ToEdmMeasureInstance()
		{
			EdmMeasure edmMeasure = this.Property as EdmMeasure;
			if (edmMeasure == null)
			{
				return EdmMeasureInstance.Empty;
			}
			return this.Entity.MeasureInstance(edmMeasure);
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x000438B0 File Offset: 0x00041AB0
		public EdmFieldInstance ToEdmFieldInstance()
		{
			if (this._hierarchyLevel != null)
			{
				return EdmFieldInstance.Empty;
			}
			EdmField edmField = this.Property as EdmField;
			if (edmField == null)
			{
				return EdmFieldInstance.Empty;
			}
			return this.Entity.FieldInstance(edmField);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000438EC File Offset: 0x00041AEC
		public EdmHierarchyLevelInstance ToEdmHierarchyLevelInstance()
		{
			if (this._hierarchyLevel == null)
			{
				return EdmHierarchyLevelInstance.Empty;
			}
			return this.Entity.HierarchyLevelInstance(this._hierarchyLevel);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0004390D File Offset: 0x00041B0D
		internal IEdmFieldInstance ToIEdmFieldInstance()
		{
			if (this._hierarchyLevel == null)
			{
				return this.ToEdmFieldInstance();
			}
			return this.ToEdmHierarchyLevelInstance();
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00043930 File Offset: 0x00041B30
		public override bool Equals(object value)
		{
			EdmPropertyInstance? edmPropertyInstance = value as EdmPropertyInstance?;
			return edmPropertyInstance != null && this.Equals(edmPropertyInstance.Value);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00043964 File Offset: 0x00041B64
		public override int GetHashCode()
		{
			if (this.Property == null && this.Entity == null)
			{
				return base.GetType().GetHashCode();
			}
			if (this._hierarchyLevel != null)
			{
				return this.ToEdmHierarchyLevelInstance().GetHashCode();
			}
			return this.Property.GetHashCode() ^ this.Entity.GetHashCode();
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x000439CB File Offset: 0x00041BCB
		public bool Equals(EdmPropertyInstance other)
		{
			return object.Equals(this._hierarchyLevel, other._hierarchyLevel) && object.Equals(this.Property, other.Property) && object.Equals(this.Entity, other.Entity);
		}

		// Token: 0x04000D22 RID: 3362
		private readonly EdmProperty _property;

		// Token: 0x04000D23 RID: 3363
		private readonly EdmHierarchyLevel _hierarchyLevel;

		// Token: 0x04000D24 RID: 3364
		private readonly EntitySet _entity;
	}
}
