using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200040A RID: 1034
	internal sealed class FieldDescriptor : PropertyDescriptor
	{
		// Token: 0x06003100 RID: 12544 RVA: 0x0009C447 File Offset: 0x0009A647
		internal FieldDescriptor(string propertyName)
			: base(propertyName, null)
		{
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x0009C451 File Offset: 0x0009A651
		internal FieldDescriptor(Type itemType, bool isReadOnly, EdmProperty property)
			: base(property.Name, null)
		{
			this._itemType = itemType;
			this._property = property;
			this._isReadOnly = isReadOnly;
			this._fieldType = this.DetermineClrType(this._property.TypeUsage);
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x0009C48C File Offset: 0x0009A68C
		private Type DetermineClrType(TypeUsage typeUsage)
		{
			Type type = null;
			EdmType edmType = typeUsage.EdmType;
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.EntityType)
			{
				if (builtInTypeKind != BuiltInTypeKind.CollectionType)
				{
					if (builtInTypeKind == BuiltInTypeKind.ComplexType || builtInTypeKind == BuiltInTypeKind.EntityType)
					{
						type = edmType.ClrType;
					}
				}
				else
				{
					TypeUsage typeUsage2 = ((CollectionType)edmType).TypeUsage;
					type = this.DetermineClrType(typeUsage2);
					type = typeof(IEnumerable<>).MakeGenericType(new Type[] { type });
				}
			}
			else if (builtInTypeKind <= BuiltInTypeKind.PrimitiveType)
			{
				if (builtInTypeKind == BuiltInTypeKind.EnumType || builtInTypeKind == BuiltInTypeKind.PrimitiveType)
				{
					type = edmType.ClrType;
					Facet facet;
					if (type.IsValueType() && typeUsage.Facets.TryGetValue("Nullable", false, out facet) && (bool)facet.Value)
					{
						type = typeof(Nullable<>).MakeGenericType(new Type[] { type });
					}
				}
			}
			else if (builtInTypeKind != BuiltInTypeKind.RefType)
			{
				if (builtInTypeKind == BuiltInTypeKind.RowType)
				{
					type = typeof(IDataRecord);
				}
			}
			else
			{
				type = typeof(EntityKey);
			}
			return type;
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x0009C595 File Offset: 0x0009A795
		internal EdmProperty EdmProperty
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x0009C59D File Offset: 0x0009A79D
		public override Type ComponentType
		{
			get
			{
				return this._itemType;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x0009C5A5 File Offset: 0x0009A7A5
		public override bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x0009C5AD File Offset: 0x0009A7AD
		public override Type PropertyType
		{
			get
			{
				return this._fieldType;
			}
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x0009C5B5 File Offset: 0x0009A7B5
		public override bool CanResetValue(object item)
		{
			return false;
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x0009C5B8 File Offset: 0x0009A7B8
		public override object GetValue(object item)
		{
			Check.NotNull<object>(item, "item");
			if (!this._itemType.IsAssignableFrom(item.GetType()))
			{
				throw new ArgumentException(Strings.ObjectView_IncompatibleArgument);
			}
			DbDataRecord dbDataRecord = item as DbDataRecord;
			object obj;
			if (dbDataRecord != null)
			{
				obj = dbDataRecord.GetValue(dbDataRecord.GetOrdinal(this._property.Name));
			}
			else
			{
				obj = DelegateFactory.GetValue(this._property, item);
			}
			return obj;
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x0009C621 File Offset: 0x0009A821
		public override void ResetValue(object item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x0009C628 File Offset: 0x0009A828
		public override void SetValue(object item, object value)
		{
			Check.NotNull<object>(item, "item");
			if (!this._itemType.IsAssignableFrom(item.GetType()))
			{
				throw new ArgumentException(Strings.ObjectView_IncompatibleArgument);
			}
			if (!this._isReadOnly)
			{
				DelegateFactory.SetValue(this._property, item, value);
				return;
			}
			throw new InvalidOperationException(Strings.ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList);
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x0009C67F File Offset: 0x0009A87F
		public override bool ShouldSerializeValue(object item)
		{
			return false;
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x0009C682 File Offset: 0x0009A882
		public override bool IsBrowsable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400102B RID: 4139
		private readonly EdmProperty _property;

		// Token: 0x0400102C RID: 4140
		private readonly Type _fieldType;

		// Token: 0x0400102D RID: 4141
		private readonly Type _itemType;

		// Token: 0x0400102E RID: 4142
		private readonly bool _isReadOnly;
	}
}
