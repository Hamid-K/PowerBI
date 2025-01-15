using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation
{
	// Token: 0x0200020A RID: 522
	internal class ForeignKeyConstraintConfiguration : ConstraintConfiguration
	{
		// Token: 0x06001BA3 RID: 7075 RVA: 0x0004C585 File Offset: 0x0004A785
		public ForeignKeyConstraintConfiguration()
		{
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0004C598 File Offset: 0x0004A798
		internal ForeignKeyConstraintConfiguration(IEnumerable<PropertyInfo> dependentProperties)
		{
			this._dependentProperties.AddRange(dependentProperties);
			this._isFullySpecified = true;
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0004C5BE File Offset: 0x0004A7BE
		private ForeignKeyConstraintConfiguration(ForeignKeyConstraintConfiguration source)
		{
			this._dependentProperties.AddRange(source._dependentProperties);
			this._isFullySpecified = source._isFullySpecified;
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0004C5EE File Offset: 0x0004A7EE
		internal override ConstraintConfiguration Clone()
		{
			return new ForeignKeyConstraintConfiguration(this);
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x0004C5F6 File Offset: 0x0004A7F6
		public override bool IsFullySpecified
		{
			get
			{
				return this._isFullySpecified;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x0004C5FE File Offset: 0x0004A7FE
		internal IEnumerable<PropertyInfo> ToProperties
		{
			get
			{
				return this._dependentProperties;
			}
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0004C606 File Offset: 0x0004A806
		public void AddColumn(PropertyInfo propertyInfo)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			if (!this._dependentProperties.ContainsSame(propertyInfo))
			{
				this._dependentProperties.Add(propertyInfo);
			}
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0004C630 File Offset: 0x0004A830
		internal override void Configure(AssociationType associationType, AssociationEndMember dependentEnd, EntityTypeConfiguration entityTypeConfiguration)
		{
			if (!this._dependentProperties.Any<PropertyInfo>())
			{
				return;
			}
			IEnumerable<PropertyInfo> enumerable = this._dependentProperties.AsEnumerable<PropertyInfo>();
			if (!this.IsFullySpecified)
			{
				if (dependentEnd.GetEntityType().GetClrType() != entityTypeConfiguration.ClrType)
				{
					return;
				}
				var enumerable2 = this._dependentProperties.Select((PropertyInfo p) => new
				{
					PropertyInfo = p,
					ColumnOrder = entityTypeConfiguration.Property(new PropertyPath(p), null).ColumnOrder
				});
				if (this._dependentProperties.Count > 1)
				{
					if (enumerable2.Any(p => p.ColumnOrder == null))
					{
						ReadOnlyMetadataCollection<EdmProperty> dependentKeys = dependentEnd.GetEntityType().KeyProperties;
						if (dependentKeys.Count == this._dependentProperties.Count && enumerable2.All(fk => dependentKeys.Any((EdmProperty p) => p.GetClrPropertyInfo().IsSameAs(fk.PropertyInfo))))
						{
							enumerable = dependentKeys.Select((EdmProperty p) => p.GetClrPropertyInfo());
							goto IL_0177;
						}
						throw Error.ForeignKeyAttributeConvention_OrderRequired(entityTypeConfiguration.ClrType);
					}
				}
				enumerable = from p in enumerable2
					orderby p.ColumnOrder
					select p.PropertyInfo;
			}
			IL_0177:
			List<EdmProperty> list = new List<EdmProperty>();
			foreach (PropertyInfo propertyInfo in enumerable)
			{
				EdmProperty declaredPrimitiveProperty = dependentEnd.GetEntityType().GetDeclaredPrimitiveProperty(propertyInfo);
				if (declaredPrimitiveProperty == null)
				{
					throw Error.ForeignKeyPropertyNotFound(propertyInfo.Name, dependentEnd.GetEntityType().Name);
				}
				list.Add(declaredPrimitiveProperty);
			}
			AssociationEndMember otherEnd = associationType.GetOtherEnd(dependentEnd);
			ReferentialConstraint referentialConstraint = new ReferentialConstraint(otherEnd, dependentEnd, otherEnd.GetEntityType().KeyProperties, list);
			if (otherEnd.IsRequired())
			{
				referentialConstraint.ToProperties.Each((EdmProperty p) => p.Nullable = false);
			}
			associationType.Constraint = referentialConstraint;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0004C880 File Offset: 0x0004AA80
		public bool Equals(ForeignKeyConstraintConfiguration other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			return other.ToProperties.SequenceEqual(this.ToProperties, new DynamicEqualityComparer<PropertyInfo>((PropertyInfo p1, PropertyInfo p2) => p1.IsSameAs(p2)));
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0004C8CD File Offset: 0x0004AACD
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != typeof(ForeignKeyConstraintConfiguration)) && this.Equals((ForeignKeyConstraintConfiguration)obj)));
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0004C8FF File Offset: 0x0004AAFF
		public override int GetHashCode()
		{
			return this.ToProperties.Aggregate(0, (int t, PropertyInfo p) => t + p.GetHashCode());
		}

		// Token: 0x04000ACB RID: 2763
		private readonly List<PropertyInfo> _dependentProperties = new List<PropertyInfo>();

		// Token: 0x04000ACC RID: 2764
		private readonly bool _isFullySpecified;
	}
}
