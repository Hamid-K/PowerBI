using System;
using System.Collections.Generic;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001FB RID: 507
	public struct EdmFieldInstance : IEdmFieldInstance, IEquatable<IEdmFieldInstance>, IEquatable<EdmFieldInstance>, ICheckable
	{
		// Token: 0x06001801 RID: 6145 RVA: 0x00042422 File Offset: 0x00040622
		internal EdmFieldInstance(EntitySet entity, EdmField field)
		{
			this._field = ArgumentValidation.CheckNotNull<EdmField>(field, "field");
			this._entity = ArgumentValidation.CheckNotNull<EntitySet>(entity, "entity");
			ArgumentValidation.CheckCondition(entity.ElementType.Members.Contains(field), "field");
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x00042464 File Offset: 0x00040664
		public static EdmFieldInstance Empty
		{
			get
			{
				return default(EdmFieldInstance);
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0004247A File Offset: 0x0004067A
		public bool IsValid
		{
			get
			{
				return this.Field != null;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00042485 File Offset: 0x00040685
		public EdmField Field
		{
			get
			{
				return this._field;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0004248D File Offset: 0x0004068D
		public EntitySet Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00042498 File Offset: 0x00040698
		public QualifiedName QualifiedName
		{
			get
			{
				return this.QualifiedName;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x000424B8 File Offset: 0x000406B8
		string IEdmFieldInstance.Caption
		{
			get
			{
				if (this.IsValid)
				{
					return this.Field.Caption;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x000424D3 File Offset: 0x000406D3
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

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x000424EE File Offset: 0x000406EE
		IEnumerable<EdmDisplayFolder> IEdmFieldInstance.DisplayFolderParents
		{
			get
			{
				return this.Field.DisplayFolderParents;
			}
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x000424FB File Offset: 0x000406FB
		EdmPropertyInstance IEdmFieldInstance.ToPropertyInstance()
		{
			return this;
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00042508 File Offset: 0x00040708
		public static implicit operator EdmPropertyInstance(EdmFieldInstance fieldInstance)
		{
			if (fieldInstance == EdmFieldInstance.Empty)
			{
				return EdmPropertyInstance.Empty;
			}
			return fieldInstance.Entity.PropertyInstance(fieldInstance.Field);
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00042530 File Offset: 0x00040730
		public static bool operator ==(EdmFieldInstance left, EdmFieldInstance right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0004253A File Offset: 0x0004073A
		public static bool operator !=(EdmFieldInstance left, EdmFieldInstance right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00042548 File Offset: 0x00040748
		public override bool Equals(object value)
		{
			EdmFieldInstance? edmFieldInstance = value as EdmFieldInstance?;
			return edmFieldInstance != null && this.Equals(edmFieldInstance.Value);
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00042579 File Offset: 0x00040779
		public override int GetHashCode()
		{
			if (this.Field == null && this.Entity == null)
			{
				return base.GetType().GetHashCode();
			}
			return this.Field.GetHashCode() ^ this.Entity.GetHashCode();
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x000425B8 File Offset: 0x000407B8
		public bool Equals(EdmFieldInstance other)
		{
			return object.Equals(this.Field, other.Field) && object.Equals(this.Entity, other.Entity);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x000425E2 File Offset: 0x000407E2
		public bool Equals(IEdmFieldInstance other)
		{
			return other is EdmFieldInstance && this.Equals((EdmFieldInstance)other);
		}

		// Token: 0x04000CE0 RID: 3296
		private readonly EdmField _field;

		// Token: 0x04000CE1 RID: 3297
		private readonly EntitySet _entity;
	}
}
