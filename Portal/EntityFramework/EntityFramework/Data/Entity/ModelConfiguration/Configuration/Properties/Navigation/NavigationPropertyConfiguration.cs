using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Edm.Services;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation
{
	// Token: 0x0200020C RID: 524
	internal class NavigationPropertyConfiguration : PropertyConfiguration
	{
		// Token: 0x06001BB3 RID: 7091 RVA: 0x0004C956 File Offset: 0x0004AB56
		internal NavigationPropertyConfiguration(PropertyInfo navigationProperty)
		{
			this._navigationProperty = navigationProperty;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0004C968 File Offset: 0x0004AB68
		private NavigationPropertyConfiguration(NavigationPropertyConfiguration source)
		{
			this._navigationProperty = source._navigationProperty;
			this._endKind = source._endKind;
			this._inverseNavigationProperty = source._inverseNavigationProperty;
			this._inverseEndKind = source._inverseEndKind;
			this._constraint = ((source._constraint == null) ? null : source._constraint.Clone());
			this._associationMappingConfiguration = ((source._associationMappingConfiguration == null) ? null : source._associationMappingConfiguration.Clone());
			this.DeleteAction = source.DeleteAction;
			this.IsNavigationPropertyDeclaringTypePrincipal = source.IsNavigationPropertyDeclaringTypePrincipal;
			this._modificationStoredProceduresConfiguration = ((source._modificationStoredProceduresConfiguration == null) ? null : source._modificationStoredProceduresConfiguration.Clone());
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0004CA17 File Offset: 0x0004AC17
		internal virtual NavigationPropertyConfiguration Clone()
		{
			return new NavigationPropertyConfiguration(this);
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x0004CA1F File Offset: 0x0004AC1F
		// (set) Token: 0x06001BB7 RID: 7095 RVA: 0x0004CA27 File Offset: 0x0004AC27
		public OperationAction? DeleteAction { get; set; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0004CA30 File Offset: 0x0004AC30
		internal PropertyInfo NavigationProperty
		{
			get
			{
				return this._navigationProperty;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x0004CA38 File Offset: 0x0004AC38
		// (set) Token: 0x06001BBA RID: 7098 RVA: 0x0004CA40 File Offset: 0x0004AC40
		public RelationshipMultiplicity? RelationshipMultiplicity
		{
			get
			{
				return this._endKind;
			}
			set
			{
				Check.NotNull<RelationshipMultiplicity>(value, "value");
				this._endKind = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x0004CA55 File Offset: 0x0004AC55
		// (set) Token: 0x06001BBC RID: 7100 RVA: 0x0004CA5D File Offset: 0x0004AC5D
		internal PropertyInfo InverseNavigationProperty
		{
			get
			{
				return this._inverseNavigationProperty;
			}
			set
			{
				if (value == this._navigationProperty)
				{
					throw Error.NavigationInverseItself(value.Name, value.ReflectedType);
				}
				this._inverseNavigationProperty = value;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x0004CA86 File Offset: 0x0004AC86
		// (set) Token: 0x06001BBE RID: 7102 RVA: 0x0004CA8E File Offset: 0x0004AC8E
		internal RelationshipMultiplicity? InverseEndKind
		{
			get
			{
				return this._inverseEndKind;
			}
			set
			{
				this._inverseEndKind = value;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x0004CA97 File Offset: 0x0004AC97
		// (set) Token: 0x06001BC0 RID: 7104 RVA: 0x0004CA9F File Offset: 0x0004AC9F
		public ConstraintConfiguration Constraint
		{
			get
			{
				return this._constraint;
			}
			set
			{
				Check.NotNull<ConstraintConfiguration>(value, "value");
				this._constraint = value;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x0004CAB4 File Offset: 0x0004ACB4
		// (set) Token: 0x06001BC2 RID: 7106 RVA: 0x0004CABC File Offset: 0x0004ACBC
		internal bool? IsNavigationPropertyDeclaringTypePrincipal { get; set; }

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x0004CAC5 File Offset: 0x0004ACC5
		// (set) Token: 0x06001BC4 RID: 7108 RVA: 0x0004CACD File Offset: 0x0004ACCD
		internal AssociationMappingConfiguration AssociationMappingConfiguration
		{
			get
			{
				return this._associationMappingConfiguration;
			}
			set
			{
				this._associationMappingConfiguration = value;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x0004CAD6 File Offset: 0x0004ACD6
		// (set) Token: 0x06001BC6 RID: 7110 RVA: 0x0004CADE File Offset: 0x0004ACDE
		internal ModificationStoredProceduresConfiguration ModificationStoredProceduresConfiguration
		{
			get
			{
				return this._modificationStoredProceduresConfiguration;
			}
			set
			{
				this._modificationStoredProceduresConfiguration = value;
			}
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x0004CAE8 File Offset: 0x0004ACE8
		internal void Configure(NavigationProperty navigationProperty, EdmModel model, EntityTypeConfiguration entityTypeConfiguration)
		{
			navigationProperty.SetConfiguration(this);
			AssociationType association = navigationProperty.Association;
			NavigationPropertyConfiguration navigationPropertyConfiguration = association.GetConfiguration() as NavigationPropertyConfiguration;
			if (navigationPropertyConfiguration == null)
			{
				association.SetConfiguration(this);
			}
			else
			{
				this.EnsureConsistency(navigationPropertyConfiguration);
			}
			this.ConfigureInverse(association, model);
			this.ConfigureEndKinds(association, navigationPropertyConfiguration);
			this.ConfigureDependentBehavior(association, model, entityTypeConfiguration);
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0004CB3C File Offset: 0x0004AD3C
		internal void Configure(AssociationSetMapping associationSetMapping, DbDatabaseMapping databaseMapping, DbProviderManifest providerManifest)
		{
			if (this.AssociationMappingConfiguration != null)
			{
				associationSetMapping.SetConfiguration(this);
				this.AssociationMappingConfiguration.Configure(associationSetMapping, databaseMapping.Database, this._navigationProperty);
			}
			if (this._modificationStoredProceduresConfiguration != null)
			{
				if (associationSetMapping.ModificationFunctionMapping == null)
				{
					new ModificationFunctionMappingGenerator(providerManifest).Generate(associationSetMapping, databaseMapping);
				}
				this._modificationStoredProceduresConfiguration.Configure(associationSetMapping.ModificationFunctionMapping, providerManifest);
			}
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0004CBA0 File Offset: 0x0004ADA0
		private void ConfigureInverse(AssociationType associationType, EdmModel model)
		{
			if (this._inverseNavigationProperty == null)
			{
				return;
			}
			NavigationProperty navigationProperty = model.GetNavigationProperty(this._inverseNavigationProperty);
			if (navigationProperty != null && navigationProperty.Association != associationType)
			{
				associationType.SourceEnd.RelationshipMultiplicity = navigationProperty.Association.TargetEnd.RelationshipMultiplicity;
				if (associationType.Constraint == null && this._constraint == null && navigationProperty.Association.Constraint != null)
				{
					associationType.Constraint = navigationProperty.Association.Constraint;
					associationType.Constraint.FromRole = associationType.SourceEnd;
					associationType.Constraint.ToRole = associationType.TargetEnd;
				}
				model.RemoveAssociationType(navigationProperty.Association);
				navigationProperty.RelationshipType = associationType;
				navigationProperty.FromEndMember = associationType.TargetEnd;
				navigationProperty.ToEndMember = associationType.SourceEnd;
			}
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x0004CC74 File Offset: 0x0004AE74
		private void ConfigureEndKinds(AssociationType associationType, NavigationPropertyConfiguration configuration)
		{
			AssociationEndMember associationEndMember = associationType.SourceEnd;
			AssociationEndMember associationEndMember2 = associationType.TargetEnd;
			if (configuration != null && configuration.InverseNavigationProperty != null)
			{
				associationEndMember = associationType.TargetEnd;
				associationEndMember2 = associationType.SourceEnd;
			}
			if (this._inverseEndKind != null)
			{
				associationEndMember.RelationshipMultiplicity = this._inverseEndKind.Value;
			}
			if (this._endKind != null)
			{
				associationEndMember2.RelationshipMultiplicity = this._endKind.Value;
			}
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x0004CCEC File Offset: 0x0004AEEC
		private void EnsureConsistency(NavigationPropertyConfiguration navigationPropertyConfiguration)
		{
			if (this.RelationshipMultiplicity != null)
			{
				if (navigationPropertyConfiguration.InverseEndKind == null)
				{
					navigationPropertyConfiguration.InverseEndKind = this.RelationshipMultiplicity;
				}
				else
				{
					RelationshipMultiplicity? relationshipMultiplicity = navigationPropertyConfiguration.InverseEndKind;
					RelationshipMultiplicity? relationshipMultiplicity2 = this.RelationshipMultiplicity;
					if (!((relationshipMultiplicity.GetValueOrDefault() == relationshipMultiplicity2.GetValueOrDefault()) & (relationshipMultiplicity != null == (relationshipMultiplicity2 != null))))
					{
						throw Error.ConflictingMultiplicities(this.NavigationProperty.Name, this.NavigationProperty.ReflectedType);
					}
				}
			}
			if (this.InverseEndKind != null)
			{
				if (navigationPropertyConfiguration.RelationshipMultiplicity == null)
				{
					navigationPropertyConfiguration.RelationshipMultiplicity = this.InverseEndKind;
				}
				else
				{
					RelationshipMultiplicity? relationshipMultiplicity2 = navigationPropertyConfiguration.RelationshipMultiplicity;
					RelationshipMultiplicity? relationshipMultiplicity = this.InverseEndKind;
					if (!((relationshipMultiplicity2.GetValueOrDefault() == relationshipMultiplicity.GetValueOrDefault()) & (relationshipMultiplicity2 != null == (relationshipMultiplicity != null))))
					{
						if (this.InverseNavigationProperty == null)
						{
							throw Error.ConflictingMultiplicities(this.NavigationProperty.Name, this.NavigationProperty.ReflectedType);
						}
						throw Error.ConflictingMultiplicities(this.InverseNavigationProperty.Name, this.InverseNavigationProperty.ReflectedType);
					}
				}
			}
			if (this.DeleteAction != null)
			{
				if (navigationPropertyConfiguration.DeleteAction == null)
				{
					navigationPropertyConfiguration.DeleteAction = this.DeleteAction;
				}
				else
				{
					OperationAction? deleteAction = navigationPropertyConfiguration.DeleteAction;
					OperationAction? deleteAction2 = this.DeleteAction;
					if (!((deleteAction.GetValueOrDefault() == deleteAction2.GetValueOrDefault()) & (deleteAction != null == (deleteAction2 != null))))
					{
						throw Error.ConflictingCascadeDeleteOperation(this.NavigationProperty.Name, this.NavigationProperty.ReflectedType);
					}
				}
			}
			if (this.Constraint != null)
			{
				if (navigationPropertyConfiguration.Constraint == null)
				{
					navigationPropertyConfiguration.Constraint = this.Constraint;
				}
				else if (!object.Equals(navigationPropertyConfiguration.Constraint, this.Constraint))
				{
					throw Error.ConflictingConstraint(this.NavigationProperty.Name, this.NavigationProperty.ReflectedType);
				}
			}
			if (this.IsNavigationPropertyDeclaringTypePrincipal != null)
			{
				if (navigationPropertyConfiguration.IsNavigationPropertyDeclaringTypePrincipal == null)
				{
					navigationPropertyConfiguration.IsNavigationPropertyDeclaringTypePrincipal = !this.IsNavigationPropertyDeclaringTypePrincipal;
				}
				else
				{
					bool? isNavigationPropertyDeclaringTypePrincipal = navigationPropertyConfiguration.IsNavigationPropertyDeclaringTypePrincipal;
					bool? isNavigationPropertyDeclaringTypePrincipal2 = this.IsNavigationPropertyDeclaringTypePrincipal;
					if ((isNavigationPropertyDeclaringTypePrincipal.GetValueOrDefault() == isNavigationPropertyDeclaringTypePrincipal2.GetValueOrDefault()) & (isNavigationPropertyDeclaringTypePrincipal != null == (isNavigationPropertyDeclaringTypePrincipal2 != null)))
					{
						throw Error.ConflictingConstraint(this.NavigationProperty.Name, this.NavigationProperty.ReflectedType);
					}
				}
			}
			if (this.AssociationMappingConfiguration != null)
			{
				if (navigationPropertyConfiguration.AssociationMappingConfiguration == null)
				{
					navigationPropertyConfiguration.AssociationMappingConfiguration = this.AssociationMappingConfiguration;
				}
				else if (!object.Equals(navigationPropertyConfiguration.AssociationMappingConfiguration, this.AssociationMappingConfiguration))
				{
					throw Error.ConflictingMapping(this.NavigationProperty.Name, this.NavigationProperty.ReflectedType);
				}
			}
			if (this.ModificationStoredProceduresConfiguration != null)
			{
				if (navigationPropertyConfiguration.ModificationStoredProceduresConfiguration == null)
				{
					navigationPropertyConfiguration.ModificationStoredProceduresConfiguration = this.ModificationStoredProceduresConfiguration;
					return;
				}
				if (!navigationPropertyConfiguration.ModificationStoredProceduresConfiguration.IsCompatibleWith(this.ModificationStoredProceduresConfiguration))
				{
					throw Error.ConflictingFunctionsMapping(this.NavigationProperty.Name, this.NavigationProperty.ReflectedType);
				}
			}
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x0004D028 File Offset: 0x0004B228
		private void ConfigureDependentBehavior(AssociationType associationType, EdmModel model, EntityTypeConfiguration entityTypeConfiguration)
		{
			AssociationEndMember associationEndMember;
			AssociationEndMember associationEndMember2;
			if (!associationType.TryGuessPrincipalAndDependentEnds(out associationEndMember, out associationEndMember2))
			{
				if (this.IsNavigationPropertyDeclaringTypePrincipal != null)
				{
					associationType.MarkPrincipalConfigured();
					NavigationProperty navigationProperty = model.EntityTypes.SelectMany((EntityType et) => et.DeclaredNavigationProperties).Single((NavigationProperty np) => np.RelationshipType.Equals(associationType) && np.GetClrPropertyInfo().IsSameAs(this.NavigationProperty));
					associationEndMember = (this.IsNavigationPropertyDeclaringTypePrincipal.Value ? associationType.GetOtherEnd(navigationProperty.ResultEnd) : navigationProperty.ResultEnd);
					associationEndMember2 = associationType.GetOtherEnd(associationEndMember);
					if (associationType.SourceEnd != associationEndMember)
					{
						associationType.SourceEnd = associationEndMember;
						associationType.TargetEnd = associationEndMember2;
						AssociationSet associationSet = model.Containers.SelectMany((EntityContainer ct) => ct.AssociationSets).Single((AssociationSet aset) => aset.ElementType == associationType);
						EntitySet sourceSet = associationSet.SourceSet;
						associationSet.SourceSet = associationSet.TargetSet;
						associationSet.TargetSet = sourceSet;
					}
				}
				if (associationEndMember == null)
				{
					associationEndMember2 = associationType.TargetEnd;
				}
			}
			this.ConfigureConstraint(associationType, associationEndMember2, entityTypeConfiguration);
			this.ConfigureDeleteAction(associationType.GetOtherEnd(associationEndMember2));
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x0004D19C File Offset: 0x0004B39C
		private void ConfigureConstraint(AssociationType associationType, AssociationEndMember dependentEnd, EntityTypeConfiguration entityTypeConfiguration)
		{
			if (this._constraint != null)
			{
				this._constraint.Configure(associationType, dependentEnd, entityTypeConfiguration);
				ReferentialConstraint constraint = associationType.Constraint;
				if (constraint != null && constraint.ToProperties.SequenceEqual(constraint.ToRole.GetEntityType().KeyProperties) && this._inverseEndKind == null && associationType.SourceEnd.IsMany())
				{
					associationType.SourceEnd.RelationshipMultiplicity = global::System.Data.Entity.Core.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne;
					associationType.TargetEnd.RelationshipMultiplicity = global::System.Data.Entity.Core.Metadata.Edm.RelationshipMultiplicity.One;
				}
			}
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x0004D218 File Offset: 0x0004B418
		private void ConfigureDeleteAction(AssociationEndMember principalEnd)
		{
			if (this.DeleteAction != null)
			{
				principalEnd.DeleteBehavior = this.DeleteAction.Value;
			}
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0004D249 File Offset: 0x0004B449
		internal void Reset()
		{
			this._endKind = null;
			this._inverseNavigationProperty = null;
			this._inverseEndKind = null;
			this._constraint = null;
			this._associationMappingConfiguration = null;
		}

		// Token: 0x04000ACE RID: 2766
		private readonly PropertyInfo _navigationProperty;

		// Token: 0x04000ACF RID: 2767
		private RelationshipMultiplicity? _endKind;

		// Token: 0x04000AD0 RID: 2768
		private PropertyInfo _inverseNavigationProperty;

		// Token: 0x04000AD1 RID: 2769
		private RelationshipMultiplicity? _inverseEndKind;

		// Token: 0x04000AD2 RID: 2770
		private ConstraintConfiguration _constraint;

		// Token: 0x04000AD3 RID: 2771
		private AssociationMappingConfiguration _associationMappingConfiguration;

		// Token: 0x04000AD4 RID: 2772
		private ModificationStoredProceduresConfiguration _modificationStoredProceduresConfiguration;
	}
}
