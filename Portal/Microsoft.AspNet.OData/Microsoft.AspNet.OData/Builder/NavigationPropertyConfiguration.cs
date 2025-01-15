using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000135 RID: 309
	public class NavigationPropertyConfiguration : PropertyConfiguration
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002B1D4 File Offset: 0x000293D4
		public NavigationPropertyConfiguration(PropertyInfo property, EdmMultiplicity multiplicity, StructuralTypeConfiguration declaringType)
			: base(property, declaringType)
		{
			if (property == null)
			{
				throw Error.ArgumentNull("property");
			}
			this.Multiplicity = multiplicity;
			this._relatedType = property.PropertyType;
			if (multiplicity == EdmMultiplicity.Many)
			{
				Type type;
				if (!TypeHelper.IsCollection(this._relatedType, out type))
				{
					throw Error.Argument("property", SRResources.ManyToManyNavigationPropertyMustReturnCollection, new object[]
					{
						property.Name,
						TypeHelper.GetReflectedType(property).Name
					});
				}
				this._relatedType = type;
			}
			this.OnDeleteAction = EdmOnDeleteAction.None;
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0002B269 File Offset: 0x00029469
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x0002B271 File Offset: 0x00029471
		public NavigationPropertyConfiguration Partner { get; internal set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x0002B27A File Offset: 0x0002947A
		// (set) Token: 0x06000AAC RID: 2732 RVA: 0x0002B282 File Offset: 0x00029482
		public EdmMultiplicity Multiplicity { get; private set; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0002B28B File Offset: 0x0002948B
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x0002B293 File Offset: 0x00029493
		public bool ContainsTarget { get; private set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0002B29C File Offset: 0x0002949C
		public override Type RelatedClrType
		{
			get
			{
				return this._relatedType;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00029CE8 File Offset: 0x00027EE8
		public override PropertyKind Kind
		{
			get
			{
				return PropertyKind.Navigation;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0002B2A4 File Offset: 0x000294A4
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x0002B2AC File Offset: 0x000294AC
		public EdmOnDeleteAction OnDeleteAction { get; set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0002B2B5 File Offset: 0x000294B5
		public IEnumerable<PropertyInfo> DependentProperties
		{
			get
			{
				return this._referentialConstraint.Keys;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0002B2C2 File Offset: 0x000294C2
		public IEnumerable<PropertyInfo> PrincipalProperties
		{
			get
			{
				return this._referentialConstraint.Values;
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002B2CF File Offset: 0x000294CF
		public NavigationPropertyConfiguration Optional()
		{
			if (this.Multiplicity == EdmMultiplicity.Many)
			{
				throw Error.InvalidOperation(SRResources.ManyNavigationPropertiesCannotBeChanged, new object[] { base.Name });
			}
			this.Multiplicity = EdmMultiplicity.ZeroOrOne;
			return this;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002B2FC File Offset: 0x000294FC
		public NavigationPropertyConfiguration Required()
		{
			if (this.Multiplicity == EdmMultiplicity.Many)
			{
				throw Error.InvalidOperation(SRResources.ManyNavigationPropertiesCannotBeChanged, new object[] { base.Name });
			}
			this.Multiplicity = EdmMultiplicity.One;
			return this;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002B329 File Offset: 0x00029529
		public NavigationPropertyConfiguration Contained()
		{
			this.ContainsTarget = true;
			return this;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002B333 File Offset: 0x00029533
		public NavigationPropertyConfiguration NonContained()
		{
			this.ContainsTarget = false;
			return this;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002B33D File Offset: 0x0002953D
		public NavigationPropertyConfiguration AutomaticallyExpand(bool disableWhenSelectIsPresent)
		{
			base.AutoExpand = true;
			base.DisableAutoExpandWhenSelectIsPresent = disableWhenSelectIsPresent;
			return this;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002B34E File Offset: 0x0002954E
		public NavigationPropertyConfiguration CascadeOnDelete()
		{
			this.CascadeOnDelete(true);
			return this;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002B359 File Offset: 0x00029559
		public NavigationPropertyConfiguration CascadeOnDelete(bool cascade)
		{
			this.OnDeleteAction = (cascade ? EdmOnDeleteAction.Cascade : EdmOnDeleteAction.None);
			return this;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0002B369 File Offset: 0x00029569
		public NavigationPropertyConfiguration HasConstraint(PropertyInfo dependentPropertyInfo, PropertyInfo principalPropertyInfo)
		{
			return this.HasConstraint(new KeyValuePair<PropertyInfo, PropertyInfo>(dependentPropertyInfo, principalPropertyInfo));
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0002B378 File Offset: 0x00029578
		public NavigationPropertyConfiguration HasConstraint(KeyValuePair<PropertyInfo, PropertyInfo> constraint)
		{
			if (constraint.Key == null)
			{
				throw Error.ArgumentNull("dependentPropertyInfo");
			}
			if (constraint.Value == null)
			{
				throw Error.ArgumentNull("principalPropertyInfo");
			}
			if (this.Multiplicity == EdmMultiplicity.Many)
			{
				throw Error.NotSupported(SRResources.ReferentialConstraintOnManyNavigationPropertyNotSupported, new object[]
				{
					base.Name,
					base.DeclaringType.ClrType.FullName
				});
			}
			if (this.ValidateConstraint(constraint))
			{
				return this;
			}
			PrimitivePropertyConfiguration primitivePropertyConfiguration = base.DeclaringType.ModelBuilder.StructuralTypes.OfType<EntityTypeConfiguration>().FirstOrDefault((EntityTypeConfiguration e) => e.ClrType == this.RelatedClrType).AddProperty(constraint.Value);
			PrimitivePropertyConfiguration primitivePropertyConfiguration2 = base.DeclaringType.AddProperty(constraint.Key);
			if (this.Multiplicity == EdmMultiplicity.ZeroOrOne || primitivePropertyConfiguration.OptionalProperty)
			{
				primitivePropertyConfiguration2.OptionalProperty = true;
			}
			if (this.Multiplicity == EdmMultiplicity.One && !primitivePropertyConfiguration.OptionalProperty)
			{
				primitivePropertyConfiguration2.OptionalProperty = false;
			}
			this._referentialConstraint.Add(constraint);
			return this;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002B480 File Offset: 0x00029680
		private bool ValidateConstraint(KeyValuePair<PropertyInfo, PropertyInfo> constraint)
		{
			if (this._referentialConstraint.Contains(constraint))
			{
				return true;
			}
			PropertyInfo propertyInfo;
			if (this._referentialConstraint.TryGetValue(constraint.Key, out propertyInfo))
			{
				throw Error.InvalidOperation(SRResources.ReferentialConstraintAlreadyConfigured, new object[]
				{
					"dependent",
					constraint.Key.Name,
					"principal",
					propertyInfo.Name
				});
			}
			if (this.PrincipalProperties.Any((PropertyInfo p) => p == constraint.Value))
			{
				PropertyInfo key = this._referentialConstraint.First((KeyValuePair<PropertyInfo, PropertyInfo> r) => r.Value == constraint.Value).Key;
				throw Error.InvalidOperation(SRResources.ReferentialConstraintAlreadyConfigured, new object[]
				{
					"principal",
					constraint.Value.Name,
					"dependent",
					key.Name
				});
			}
			Type type = Nullable.GetUnderlyingType(constraint.Key.PropertyType) ?? constraint.Key.PropertyType;
			Type type2 = Nullable.GetUnderlyingType(constraint.Value.PropertyType) ?? constraint.Value.PropertyType;
			if (type != type2)
			{
				throw Error.InvalidOperation(SRResources.DependentAndPrincipalTypeNotMatch, new object[]
				{
					constraint.Key.PropertyType.FullName,
					constraint.Value.PropertyType.FullName
				});
			}
			if (EdmLibHelpers.GetEdmPrimitiveTypeOrNull(constraint.Key.PropertyType) == null)
			{
				throw Error.InvalidOperation(SRResources.ReferentialConstraintPropertyTypeNotValid, new object[] { constraint.Key.PropertyType.FullName });
			}
			return false;
		}

		// Token: 0x04000350 RID: 848
		private readonly Type _relatedType;

		// Token: 0x04000351 RID: 849
		private readonly IDictionary<PropertyInfo, PropertyInfo> _referentialConstraint = new Dictionary<PropertyInfo, PropertyInfo>();
	}
}
