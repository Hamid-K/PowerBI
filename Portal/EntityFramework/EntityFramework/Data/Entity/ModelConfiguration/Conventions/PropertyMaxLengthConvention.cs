using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001B3 RID: 435
	public class PropertyMaxLengthConvention : IConceptualModelConvention<EntityType>, IConvention, IConceptualModelConvention<ComplexType>, IConceptualModelConvention<AssociationType>
	{
		// Token: 0x0600178D RID: 6029 RVA: 0x0003FBBE File Offset: 0x0003DDBE
		public PropertyMaxLengthConvention()
			: this(128)
		{
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0003FBCB File Offset: 0x0003DDCB
		public PropertyMaxLengthConvention(int length)
		{
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length", Strings.InvalidMaxLengthSize);
			}
			this._length = length;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0003FBEE File Offset: 0x0003DDEE
		public virtual void Apply(EntityType item, DbModel model)
		{
			Check.NotNull<EntityType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			this.SetLength(item.DeclaredProperties, item.KeyProperties);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0003FC1A File Offset: 0x0003DE1A
		public virtual void Apply(ComplexType item, DbModel model)
		{
			Check.NotNull<ComplexType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			this.SetLength(item.Properties, new List<EdmProperty>());
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0003FC48 File Offset: 0x0003DE48
		private void SetLength(IEnumerable<EdmProperty> properties, ICollection<EdmProperty> keyProperties)
		{
			foreach (EdmProperty edmProperty in properties)
			{
				if (edmProperty.IsPrimitiveType)
				{
					if (edmProperty.PrimitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.String))
					{
						this.SetStringDefaults(edmProperty, keyProperties.Contains(edmProperty));
					}
					if (edmProperty.PrimitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Binary))
					{
						this.SetBinaryDefaults(edmProperty, keyProperties.Contains(edmProperty));
					}
				}
			}
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0003FCCC File Offset: 0x0003DECC
		public virtual void Apply(AssociationType item, DbModel model)
		{
			Check.NotNull<AssociationType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.Constraint == null)
			{
				return;
			}
			IEnumerable<EdmProperty> enumerable = item.GetOtherEnd(item.Constraint.DependentEnd).GetEntityType().KeyProperties();
			if (enumerable.Count<EdmProperty>() != item.Constraint.ToProperties.Count)
			{
				return;
			}
			for (int i = 0; i < item.Constraint.ToProperties.Count; i++)
			{
				EdmProperty edmProperty = item.Constraint.ToProperties[i];
				EdmProperty edmProperty2 = enumerable.ElementAt(i);
				if (edmProperty.PrimitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.String) || edmProperty.PrimitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Binary))
				{
					edmProperty.IsUnicode = edmProperty2.IsUnicode;
					edmProperty.IsFixedLength = edmProperty2.IsFixedLength;
					edmProperty.MaxLength = edmProperty2.MaxLength;
					edmProperty.IsMaxLength = edmProperty2.IsMaxLength;
				}
			}
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0003FDB4 File Offset: 0x0003DFB4
		private void SetStringDefaults(EdmProperty property, bool isKey)
		{
			if (property.IsUnicode == null)
			{
				property.IsUnicode = new bool?(true);
			}
			this.SetBinaryDefaults(property, isKey);
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0003FDE8 File Offset: 0x0003DFE8
		private void SetBinaryDefaults(EdmProperty property, bool isKey)
		{
			if (property.IsFixedLength == null)
			{
				property.IsFixedLength = new bool?(false);
			}
			if (property.MaxLength == null && !property.IsMaxLength)
			{
				if (!isKey)
				{
					bool? isFixedLength = property.IsFixedLength;
					bool flag = true;
					if (!((isFixedLength.GetValueOrDefault() == flag) & (isFixedLength != null)))
					{
						property.IsMaxLength = true;
						return;
					}
				}
				property.MaxLength = new int?(this._length);
				return;
			}
		}

		// Token: 0x04000A37 RID: 2615
		private const int DefaultLength = 128;

		// Token: 0x04000A38 RID: 2616
		private readonly int _length;
	}
}
