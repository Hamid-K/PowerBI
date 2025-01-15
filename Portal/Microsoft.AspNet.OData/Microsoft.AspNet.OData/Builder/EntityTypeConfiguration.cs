using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000132 RID: 306
	public class EntityTypeConfiguration : StructuralTypeConfiguration
	{
		// Token: 0x06000A82 RID: 2690 RVA: 0x0002AD92 File Offset: 0x00028F92
		public EntityTypeConfiguration()
		{
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0002ADB0 File Offset: 0x00028FB0
		public EntityTypeConfiguration(ODataModelBuilder modelBuilder, Type clrType)
			: base(modelBuilder, clrType)
		{
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0000623D File Offset: 0x0000443D
		public override EdmTypeKind Kind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0002ADD0 File Offset: 0x00028FD0
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x0002ADD8 File Offset: 0x00028FD8
		public virtual bool HasStream { get; set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002ADE1 File Offset: 0x00028FE1
		public virtual IEnumerable<PrimitivePropertyConfiguration> Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0002ADE9 File Offset: 0x00028FE9
		public virtual IEnumerable<EnumPropertyConfiguration> EnumKeys
		{
			get
			{
				return this._enumKeys;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0002ADF1 File Offset: 0x00028FF1
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0002ADFE File Offset: 0x00028FFE
		public virtual EntityTypeConfiguration BaseType
		{
			get
			{
				return this.BaseTypeInternal as EntityTypeConfiguration;
			}
			set
			{
				this.DerivesFrom(value);
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00029D02 File Offset: 0x00027F02
		public virtual EntityTypeConfiguration Abstract()
		{
			this.AbstractImpl();
			return this;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002AE08 File Offset: 0x00029008
		public virtual EntityTypeConfiguration MediaType()
		{
			this.HasStream = true;
			return this;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002AE14 File Offset: 0x00029014
		public virtual EntityTypeConfiguration HasKey(PropertyInfo keyProperty)
		{
			if (this.BaseType != null && this.BaseType.Keys().Any<PropertyConfiguration>())
			{
				throw Error.InvalidOperation(SRResources.CannotDefineKeysOnDerivedTypes, new object[]
				{
					this.FullName,
					this.BaseType.FullName
				});
			}
			if (TypeHelper.IsEnum(keyProperty.PropertyType))
			{
				this.ModelBuilder.AddEnumType(keyProperty.PropertyType);
				EnumPropertyConfiguration enumPropertyConfiguration = this.AddEnumProperty(keyProperty);
				enumPropertyConfiguration.IsRequired();
				if (!this._enumKeys.Contains(enumPropertyConfiguration))
				{
					this._enumKeys.Add(enumPropertyConfiguration);
				}
			}
			else
			{
				PrimitivePropertyConfiguration primitivePropertyConfiguration = this.AddProperty(keyProperty);
				primitivePropertyConfiguration.IsRequired();
				if (!this._keys.Contains(primitivePropertyConfiguration))
				{
					this._keys.Add(primitivePropertyConfiguration);
				}
			}
			return this;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002AED7 File Offset: 0x000290D7
		public virtual void RemoveKey(PrimitivePropertyConfiguration keyProperty)
		{
			if (keyProperty == null)
			{
				throw Error.ArgumentNull("keyProperty");
			}
			this._keys.Remove(keyProperty);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002AEF4 File Offset: 0x000290F4
		public virtual void RemoveKey(EnumPropertyConfiguration enumKeyProperty)
		{
			if (enumKeyProperty == null)
			{
				throw Error.ArgumentNull("enumKeyProperty");
			}
			this._enumKeys.Remove(enumKeyProperty);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00029D0B File Offset: 0x00027F0B
		public virtual EntityTypeConfiguration DerivesFromNothing()
		{
			this.DerivesFromNothingImpl();
			return this;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002AF14 File Offset: 0x00029114
		public virtual EntityTypeConfiguration DerivesFrom(EntityTypeConfiguration baseType)
		{
			if ((this.Keys.Any<PrimitivePropertyConfiguration>() || this.EnumKeys.Any<EnumPropertyConfiguration>()) && baseType.Keys().Any<PropertyConfiguration>())
			{
				throw Error.InvalidOperation(SRResources.CannotDefineKeysOnDerivedTypes, new object[] { this.FullName, baseType.FullName });
			}
			this.DerivesFromImpl(baseType);
			return this;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002AF74 File Offset: 0x00029174
		public override void RemoveProperty(PropertyInfo propertyInfo)
		{
			base.RemoveProperty(propertyInfo);
			this._keys.RemoveAll((PrimitivePropertyConfiguration p) => p.PropertyInfo == propertyInfo);
			this._enumKeys.RemoveAll((EnumPropertyConfiguration p) => p.PropertyInfo == propertyInfo);
		}

		// Token: 0x04000347 RID: 839
		private List<PrimitivePropertyConfiguration> _keys = new List<PrimitivePropertyConfiguration>();

		// Token: 0x04000348 RID: 840
		private List<EnumPropertyConfiguration> _enumKeys = new List<EnumPropertyConfiguration>();
	}
}
