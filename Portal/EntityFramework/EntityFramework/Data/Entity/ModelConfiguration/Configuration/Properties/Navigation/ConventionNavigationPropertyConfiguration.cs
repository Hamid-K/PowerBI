using System;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation
{
	// Token: 0x02000209 RID: 521
	internal class ConventionNavigationPropertyConfiguration
	{
		// Token: 0x06001B92 RID: 7058 RVA: 0x0004C068 File Offset: 0x0004A268
		internal ConventionNavigationPropertyConfiguration(NavigationPropertyConfiguration configuration, ModelConfiguration modelConfiguration)
		{
			this._configuration = configuration;
			this._modelConfiguration = modelConfiguration;
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x0004C07E File Offset: 0x0004A27E
		public virtual PropertyInfo ClrPropertyInfo
		{
			get
			{
				if (this._configuration == null)
				{
					return null;
				}
				return this._configuration.NavigationProperty;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x0004C095 File Offset: 0x0004A295
		internal NavigationPropertyConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0004C09D File Offset: 0x0004A29D
		public virtual void HasConstraint<T>() where T : ConstraintConfiguration
		{
			this.HasConstraintInternal<T>(null);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0004C0A6 File Offset: 0x0004A2A6
		public virtual void HasConstraint<T>(Action<T> constraintConfigurationAction) where T : ConstraintConfiguration
		{
			Check.NotNull<Action<T>>(constraintConfigurationAction, "constraintConfigurationAction");
			this.HasConstraintInternal<T>(constraintConfigurationAction);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0004C0BC File Offset: 0x0004A2BC
		private void HasConstraintInternal<T>(Action<T> constraintConfigurationAction) where T : ConstraintConfiguration
		{
			if (this._configuration != null && !this.HasConfiguredConstraint())
			{
				Type typeFromHandle = typeof(T);
				if (this._configuration.Constraint == null)
				{
					if (typeFromHandle == typeof(IndependentConstraintConfiguration))
					{
						this._configuration.Constraint = IndependentConstraintConfiguration.Instance;
					}
					else
					{
						this._configuration.Constraint = (ConstraintConfiguration)Activator.CreateInstance(typeFromHandle);
					}
				}
				else if (this._configuration.Constraint.GetType() != typeFromHandle)
				{
					return;
				}
				if (constraintConfigurationAction != null)
				{
					constraintConfigurationAction((T)((object)this._configuration.Constraint));
				}
			}
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x0004C168 File Offset: 0x0004A368
		private bool HasConfiguredConstraint()
		{
			if (this._configuration != null && this._configuration.Constraint != null && this._configuration.Constraint.IsFullySpecified)
			{
				return true;
			}
			if (this._configuration != null && this._configuration.InverseNavigationProperty != null)
			{
				Type targetType = this._configuration.NavigationProperty.PropertyType.GetTargetType();
				if (this._modelConfiguration.Entities.Contains(targetType))
				{
					EntityTypeConfiguration entityTypeConfiguration = this._modelConfiguration.Entity(targetType);
					if (entityTypeConfiguration.IsNavigationPropertyConfigured(this._configuration.InverseNavigationProperty))
					{
						return entityTypeConfiguration.Navigation(this._configuration.InverseNavigationProperty).Constraint != null;
					}
				}
			}
			return false;
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x0004C220 File Offset: 0x0004A420
		public virtual ConventionNavigationPropertyConfiguration HasInverseNavigationProperty(Func<PropertyInfo, PropertyInfo> inverseNavigationPropertyGetter)
		{
			Check.NotNull<Func<PropertyInfo, PropertyInfo>>(inverseNavigationPropertyGetter, "inverseNavigationPropertyGetter");
			if (this._configuration != null && this._configuration.InverseNavigationProperty == null)
			{
				PropertyInfo propertyInfo = inverseNavigationPropertyGetter(this.ClrPropertyInfo);
				Check.NotNull<PropertyInfo>(propertyInfo, "inverseNavigationProperty");
				if (!propertyInfo.IsValidEdmNavigationProperty())
				{
					throw new InvalidOperationException(Strings.LightweightEntityConfiguration_InvalidNavigationProperty(propertyInfo.Name));
				}
				if (!propertyInfo.DeclaringType.IsAssignableFrom(this._configuration.NavigationProperty.PropertyType.GetTargetType()))
				{
					throw new InvalidOperationException(Strings.LightweightEntityConfiguration_MismatchedInverseNavigationProperty(this._configuration.NavigationProperty.PropertyType.GetTargetType().FullName, this._configuration.NavigationProperty.Name, propertyInfo.DeclaringType.FullName, propertyInfo.Name));
				}
				if (!this._configuration.NavigationProperty.DeclaringType.IsAssignableFrom(propertyInfo.PropertyType.GetTargetType()))
				{
					throw new InvalidOperationException(Strings.LightweightEntityConfiguration_InvalidInverseNavigationProperty(this._configuration.NavigationProperty.DeclaringType.FullName, this._configuration.NavigationProperty.Name, propertyInfo.PropertyType.GetTargetType().FullName, propertyInfo.Name));
				}
				if (this._configuration.InverseEndKind != null)
				{
					ConventionNavigationPropertyConfiguration.VerifyMultiplicityCompatibility(this._configuration.InverseEndKind.Value, propertyInfo);
				}
				this._modelConfiguration.Entity(this._configuration.NavigationProperty.PropertyType.GetTargetType()).Navigation(propertyInfo);
				this._configuration.InverseNavigationProperty = propertyInfo;
			}
			return this;
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0004C3C0 File Offset: 0x0004A5C0
		public virtual ConventionNavigationPropertyConfiguration HasInverseEndMultiplicity(RelationshipMultiplicity multiplicity)
		{
			if (this._configuration != null && this._configuration.InverseEndKind == null)
			{
				if (this._configuration.InverseNavigationProperty != null)
				{
					ConventionNavigationPropertyConfiguration.VerifyMultiplicityCompatibility(multiplicity, this._configuration.InverseNavigationProperty);
				}
				this._configuration.InverseEndKind = new RelationshipMultiplicity?(multiplicity);
			}
			return this;
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0004C420 File Offset: 0x0004A620
		public virtual ConventionNavigationPropertyConfiguration IsDeclaringTypePrincipal(bool isPrincipal)
		{
			if (this._configuration != null && this._configuration.IsNavigationPropertyDeclaringTypePrincipal == null)
			{
				this._configuration.IsNavigationPropertyDeclaringTypePrincipal = new bool?(isPrincipal);
			}
			return this;
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0004C45C File Offset: 0x0004A65C
		public virtual ConventionNavigationPropertyConfiguration HasDeleteAction(OperationAction deleteAction)
		{
			if (this._configuration != null && this._configuration.DeleteAction == null)
			{
				this._configuration.DeleteAction = new OperationAction?(deleteAction);
			}
			return this;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0004C498 File Offset: 0x0004A698
		public virtual ConventionNavigationPropertyConfiguration HasRelationshipMultiplicity(RelationshipMultiplicity multiplicity)
		{
			if (this._configuration != null && this._configuration.RelationshipMultiplicity == null)
			{
				ConventionNavigationPropertyConfiguration.VerifyMultiplicityCompatibility(multiplicity, this._configuration.NavigationProperty);
				this._configuration.RelationshipMultiplicity = new RelationshipMultiplicity?(multiplicity);
			}
			return this;
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x0004C4E8 File Offset: 0x0004A6E8
		private static void VerifyMultiplicityCompatibility(RelationshipMultiplicity multiplicity, PropertyInfo propertyInfo)
		{
			bool flag;
			if (multiplicity > RelationshipMultiplicity.One)
			{
				if (multiplicity != RelationshipMultiplicity.Many)
				{
					throw new InvalidOperationException(Strings.LightweightNavigationPropertyConfiguration_InvalidMultiplicity(multiplicity));
				}
				flag = propertyInfo.PropertyType.IsCollection();
			}
			else
			{
				flag = !propertyInfo.PropertyType.IsCollection();
			}
			if (!flag)
			{
				throw new InvalidOperationException(Strings.LightweightNavigationPropertyConfiguration_IncompatibleMultiplicity(RelationshipMultiplicityConverter.MultiplicityToString(multiplicity), propertyInfo.DeclaringType.Name + "." + propertyInfo.Name, propertyInfo.PropertyType));
			}
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0004C564 File Offset: 0x0004A764
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0004C56C File Offset: 0x0004A76C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0004C575 File Offset: 0x0004A775
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0004C57D File Offset: 0x0004A77D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000AC9 RID: 2761
		private readonly NavigationPropertyConfiguration _configuration;

		// Token: 0x04000ACA RID: 2762
		private readonly ModelConfiguration _modelConfiguration;
	}
}
