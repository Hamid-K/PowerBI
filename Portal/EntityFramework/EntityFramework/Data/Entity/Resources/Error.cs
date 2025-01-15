using System;
using System.CodeDom.Compiler;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations.Infrastructure;

namespace System.Data.Entity.Resources
{
	// Token: 0x0200009B RID: 155
	[GeneratedCode("Resources.tt", "1.0.0.0")]
	internal static class Error
	{
		// Token: 0x06000D4F RID: 3407 RVA: 0x0001B7DE File Offset: 0x000199DE
		internal static Exception AutomaticDataLoss()
		{
			return new AutomaticDataLossException(Strings.AutomaticDataLoss);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0001B7EA File Offset: 0x000199EA
		internal static Exception MetadataOutOfDate()
		{
			return new MigrationsException(Strings.MetadataOutOfDate);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0001B7F6 File Offset: 0x000199F6
		internal static Exception MigrationNotFound(object p0)
		{
			return new MigrationsException(Strings.MigrationNotFound(p0));
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0001B803 File Offset: 0x00019A03
		internal static Exception PartialFkOperation(object p0, object p1)
		{
			return new MigrationsException(Strings.PartialFkOperation(p0, p1));
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0001B811 File Offset: 0x00019A11
		internal static Exception AutoNotValidTarget(object p0)
		{
			return new MigrationsException(Strings.AutoNotValidTarget(p0));
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0001B81E File Offset: 0x00019A1E
		internal static Exception AutoNotValidForScriptWindows(object p0)
		{
			return new MigrationsException(Strings.AutoNotValidForScriptWindows(p0));
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0001B82B File Offset: 0x00019A2B
		internal static Exception ContextNotConstructible(object p0)
		{
			return new MigrationsException(Strings.ContextNotConstructible(p0));
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0001B838 File Offset: 0x00019A38
		internal static Exception AmbiguousMigrationName(object p0)
		{
			return new MigrationsException(Strings.AmbiguousMigrationName(p0));
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0001B845 File Offset: 0x00019A45
		internal static Exception AutomaticDisabledException()
		{
			return new AutomaticMigrationsDisabledException(Strings.AutomaticDisabledException);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0001B851 File Offset: 0x00019A51
		internal static Exception DownScriptWindowsNotSupported()
		{
			return new MigrationsException(Strings.DownScriptWindowsNotSupported);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0001B85D File Offset: 0x00019A5D
		internal static Exception AssemblyMigrator_NoConfigurationWithName(object p0, object p1)
		{
			return new MigrationsException(Strings.AssemblyMigrator_NoConfigurationWithName(p0, p1));
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0001B86B File Offset: 0x00019A6B
		internal static Exception AssemblyMigrator_MultipleConfigurationsWithName(object p0, object p1)
		{
			return new MigrationsException(Strings.AssemblyMigrator_MultipleConfigurationsWithName(p0, p1));
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0001B879 File Offset: 0x00019A79
		internal static Exception AssemblyMigrator_NoConfiguration(object p0)
		{
			return new MigrationsException(Strings.AssemblyMigrator_NoConfiguration(p0));
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0001B886 File Offset: 0x00019A86
		internal static Exception AssemblyMigrator_MultipleConfigurations(object p0)
		{
			return new MigrationsException(Strings.AssemblyMigrator_MultipleConfigurations(p0));
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0001B893 File Offset: 0x00019A93
		internal static Exception MigrationsNamespaceNotUnderRootNamespace(object p0, object p1)
		{
			return new MigrationsException(Strings.MigrationsNamespaceNotUnderRootNamespace(p0, p1));
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0001B8A1 File Offset: 0x00019AA1
		internal static Exception UnableToDispatchAddOrUpdate(object p0)
		{
			return new InvalidOperationException(Strings.UnableToDispatchAddOrUpdate(p0));
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0001B8AE File Offset: 0x00019AAE
		internal static Exception NoSqlGeneratorForProvider(object p0)
		{
			return new MigrationsException(Strings.NoSqlGeneratorForProvider(p0));
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0001B8BB File Offset: 0x00019ABB
		internal static Exception EntityTypeConfigurationMismatch(object p0)
		{
			return new InvalidOperationException(Strings.EntityTypeConfigurationMismatch(p0));
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0001B8C8 File Offset: 0x00019AC8
		internal static Exception ComplexTypeConfigurationMismatch(object p0)
		{
			return new InvalidOperationException(Strings.ComplexTypeConfigurationMismatch(p0));
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0001B8D5 File Offset: 0x00019AD5
		internal static Exception KeyPropertyNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.KeyPropertyNotFound(p0, p1));
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0001B8E3 File Offset: 0x00019AE3
		internal static Exception ForeignKeyPropertyNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ForeignKeyPropertyNotFound(p0, p1));
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0001B8F1 File Offset: 0x00019AF1
		internal static Exception PropertyNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.PropertyNotFound(p0, p1));
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0001B8FF File Offset: 0x00019AFF
		internal static Exception NavigationPropertyNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.NavigationPropertyNotFound(p0, p1));
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0001B90D File Offset: 0x00019B0D
		internal static Exception InvalidPropertyExpression(object p0)
		{
			return new InvalidOperationException(Strings.InvalidPropertyExpression(p0));
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0001B91A File Offset: 0x00019B1A
		internal static Exception InvalidComplexPropertyExpression(object p0)
		{
			return new InvalidOperationException(Strings.InvalidComplexPropertyExpression(p0));
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0001B927 File Offset: 0x00019B27
		internal static Exception InvalidPropertiesExpression(object p0)
		{
			return new InvalidOperationException(Strings.InvalidPropertiesExpression(p0));
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0001B934 File Offset: 0x00019B34
		internal static Exception InvalidComplexPropertiesExpression(object p0)
		{
			return new InvalidOperationException(Strings.InvalidComplexPropertiesExpression(p0));
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0001B941 File Offset: 0x00019B41
		internal static Exception DuplicateStructuralTypeConfiguration(object p0)
		{
			return new InvalidOperationException(Strings.DuplicateStructuralTypeConfiguration(p0));
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0001B94E File Offset: 0x00019B4E
		internal static Exception ConflictingPropertyConfiguration(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.ConflictingPropertyConfiguration(p0, p1, p2));
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0001B95D File Offset: 0x00019B5D
		internal static Exception ConflictingTypeAnnotation(object p0, object p1, object p2, object p3)
		{
			return new InvalidOperationException(Strings.ConflictingTypeAnnotation(p0, p1, p2, p3));
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0001B96D File Offset: 0x00019B6D
		internal static Exception ConflictingColumnConfiguration(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.ConflictingColumnConfiguration(p0, p1, p2));
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0001B97C File Offset: 0x00019B7C
		internal static Exception CodeFirstInvalidComplexType(object p0)
		{
			return new InvalidOperationException(Strings.CodeFirstInvalidComplexType(p0));
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0001B989 File Offset: 0x00019B89
		internal static Exception InvalidEntityType(object p0)
		{
			return new InvalidOperationException(Strings.InvalidEntityType(p0));
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0001B996 File Offset: 0x00019B96
		internal static Exception NavigationInverseItself(object p0, object p1)
		{
			return new InvalidOperationException(Strings.NavigationInverseItself(p0, p1));
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0001B9A4 File Offset: 0x00019BA4
		internal static Exception ConflictingConstraint(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ConflictingConstraint(p0, p1));
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0001B9B2 File Offset: 0x00019BB2
		internal static Exception ConflictingInferredColumnType(object p0, object p1, object p2)
		{
			return new MappingException(Strings.ConflictingInferredColumnType(p0, p1, p2));
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0001B9C1 File Offset: 0x00019BC1
		internal static Exception ConflictingMapping(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ConflictingMapping(p0, p1));
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0001B9CF File Offset: 0x00019BCF
		internal static Exception ConflictingCascadeDeleteOperation(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ConflictingCascadeDeleteOperation(p0, p1));
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0001B9DD File Offset: 0x00019BDD
		internal static Exception ConflictingMultiplicities(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ConflictingMultiplicities(p0, p1));
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0001B9EB File Offset: 0x00019BEB
		internal static Exception MaxLengthAttributeConvention_InvalidMaxLength(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MaxLengthAttributeConvention_InvalidMaxLength(p0, p1));
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0001B9F9 File Offset: 0x00019BF9
		internal static Exception StringLengthAttributeConvention_InvalidMaximumLength(object p0, object p1)
		{
			return new InvalidOperationException(Strings.StringLengthAttributeConvention_InvalidMaximumLength(p0, p1));
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0001BA07 File Offset: 0x00019C07
		internal static Exception ModelGeneration_UnableToDetermineKeyOrder(object p0)
		{
			return new InvalidOperationException(Strings.ModelGeneration_UnableToDetermineKeyOrder(p0));
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0001BA14 File Offset: 0x00019C14
		internal static Exception ForeignKeyAttributeConvention_EmptyKey(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ForeignKeyAttributeConvention_EmptyKey(p0, p1));
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0001BA22 File Offset: 0x00019C22
		internal static Exception ForeignKeyAttributeConvention_InvalidKey(object p0, object p1, object p2, object p3)
		{
			return new InvalidOperationException(Strings.ForeignKeyAttributeConvention_InvalidKey(p0, p1, p2, p3));
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0001BA32 File Offset: 0x00019C32
		internal static Exception ForeignKeyAttributeConvention_InvalidNavigationProperty(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.ForeignKeyAttributeConvention_InvalidNavigationProperty(p0, p1, p2));
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0001BA41 File Offset: 0x00019C41
		internal static Exception ForeignKeyAttributeConvention_OrderRequired(object p0)
		{
			return new InvalidOperationException(Strings.ForeignKeyAttributeConvention_OrderRequired(p0));
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0001BA4E File Offset: 0x00019C4E
		internal static Exception InversePropertyAttributeConvention_PropertyNotFound(object p0, object p1, object p2, object p3)
		{
			return new InvalidOperationException(Strings.InversePropertyAttributeConvention_PropertyNotFound(p0, p1, p2, p3));
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0001BA5E File Offset: 0x00019C5E
		internal static Exception InversePropertyAttributeConvention_SelfInverseDetected(object p0, object p1)
		{
			return new InvalidOperationException(Strings.InversePropertyAttributeConvention_SelfInverseDetected(p0, p1));
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0001BA6C File Offset: 0x00019C6C
		internal static Exception KeyRegisteredOnDerivedType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.KeyRegisteredOnDerivedType(p0, p1));
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0001BA7A File Offset: 0x00019C7A
		internal static Exception InvalidTableMapping(object p0, object p1)
		{
			return new InvalidOperationException(Strings.InvalidTableMapping(p0, p1));
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0001BA88 File Offset: 0x00019C88
		internal static Exception InvalidTableMapping_NoTableName(object p0)
		{
			return new InvalidOperationException(Strings.InvalidTableMapping_NoTableName(p0));
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0001BA95 File Offset: 0x00019C95
		internal static Exception InvalidChainedMappingSyntax(object p0)
		{
			return new InvalidOperationException(Strings.InvalidChainedMappingSyntax(p0));
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0001BAA2 File Offset: 0x00019CA2
		internal static Exception InvalidNotNullCondition(object p0, object p1)
		{
			return new InvalidOperationException(Strings.InvalidNotNullCondition(p0, p1));
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0001BAB0 File Offset: 0x00019CB0
		internal static Exception InvalidDiscriminatorType(object p0)
		{
			return new ArgumentException(Strings.InvalidDiscriminatorType(p0));
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0001BABD File Offset: 0x00019CBD
		internal static Exception ConventionNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ConventionNotFound(p0, p1));
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0001BACB File Offset: 0x00019CCB
		internal static Exception InvalidEntitySplittingProperties(object p0)
		{
			return new InvalidOperationException(Strings.InvalidEntitySplittingProperties(p0));
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0001BAD8 File Offset: 0x00019CD8
		internal static Exception InvalidDatabaseName(object p0)
		{
			return new ArgumentException(Strings.InvalidDatabaseName(p0));
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0001BAE5 File Offset: 0x00019CE5
		internal static Exception EntityMappingConfiguration_DuplicateMapInheritedProperties(object p0)
		{
			return new InvalidOperationException(Strings.EntityMappingConfiguration_DuplicateMapInheritedProperties(p0));
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0001BAF2 File Offset: 0x00019CF2
		internal static Exception EntityMappingConfiguration_DuplicateMappedProperties(object p0)
		{
			return new InvalidOperationException(Strings.EntityMappingConfiguration_DuplicateMappedProperties(p0));
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0001BAFF File Offset: 0x00019CFF
		internal static Exception EntityMappingConfiguration_DuplicateMappedProperty(object p0, object p1)
		{
			return new InvalidOperationException(Strings.EntityMappingConfiguration_DuplicateMappedProperty(p0, p1));
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0001BB0D File Offset: 0x00019D0D
		internal static Exception EntityMappingConfiguration_CannotMapIgnoredProperty(object p0, object p1)
		{
			return new InvalidOperationException(Strings.EntityMappingConfiguration_CannotMapIgnoredProperty(p0, p1));
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0001BB1B File Offset: 0x00019D1B
		internal static Exception EntityMappingConfiguration_InvalidTableSharing(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.EntityMappingConfiguration_InvalidTableSharing(p0, p1, p2));
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0001BB2A File Offset: 0x00019D2A
		internal static Exception EntityMappingConfiguration_TPCWithIAsOnNonLeafType(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.EntityMappingConfiguration_TPCWithIAsOnNonLeafType(p0, p1, p2));
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0001BB39 File Offset: 0x00019D39
		internal static Exception CannotIgnoreMappedBaseProperty(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.CannotIgnoreMappedBaseProperty(p0, p1, p2));
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0001BB48 File Offset: 0x00019D48
		internal static Exception ModelBuilder_KeyPropertiesMustBePrimitive(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ModelBuilder_KeyPropertiesMustBePrimitive(p0, p1));
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0001BB56 File Offset: 0x00019D56
		internal static Exception TableNotFound(object p0)
		{
			return new InvalidOperationException(Strings.TableNotFound(p0));
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0001BB63 File Offset: 0x00019D63
		internal static Exception IncorrectColumnCount(object p0)
		{
			return new InvalidOperationException(Strings.IncorrectColumnCount(p0));
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0001BB70 File Offset: 0x00019D70
		internal static Exception CircularComplexTypeHierarchy()
		{
			return new InvalidOperationException(Strings.CircularComplexTypeHierarchy);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0001BB7C File Offset: 0x00019D7C
		internal static Exception UnableToDeterminePrincipal(object p0, object p1)
		{
			return new InvalidOperationException(Strings.UnableToDeterminePrincipal(p0, p1));
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0001BB8A File Offset: 0x00019D8A
		internal static Exception UnmappedAbstractType(object p0)
		{
			return new InvalidOperationException(Strings.UnmappedAbstractType(p0));
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0001BB97 File Offset: 0x00019D97
		internal static Exception UnsupportedHybridInheritanceMapping(object p0)
		{
			return new NotSupportedException(Strings.UnsupportedHybridInheritanceMapping(p0));
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0001BBA4 File Offset: 0x00019DA4
		internal static Exception OrphanedConfiguredTableDetected(object p0)
		{
			return new InvalidOperationException(Strings.OrphanedConfiguredTableDetected(p0));
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0001BBB1 File Offset: 0x00019DB1
		internal static Exception DuplicateConfiguredColumnOrder(object p0)
		{
			return new InvalidOperationException(Strings.DuplicateConfiguredColumnOrder(p0));
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0001BBBE File Offset: 0x00019DBE
		internal static Exception UnsupportedUseOfV3Type(object p0, object p1)
		{
			return new NotSupportedException(Strings.UnsupportedUseOfV3Type(p0, p1));
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0001BBCC File Offset: 0x00019DCC
		internal static Exception MultiplePropertiesMatchedAsKeys(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MultiplePropertiesMatchedAsKeys(p0, p1));
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0001BBDA File Offset: 0x00019DDA
		internal static Exception DbPropertyEntry_CannotGetCurrentValue(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyEntry_CannotGetCurrentValue(p0, p1));
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0001BBE8 File Offset: 0x00019DE8
		internal static Exception DbPropertyEntry_CannotSetCurrentValue(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyEntry_CannotSetCurrentValue(p0, p1));
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0001BBF6 File Offset: 0x00019DF6
		internal static Exception DbPropertyEntry_NotSupportedForDetached(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.DbPropertyEntry_NotSupportedForDetached(p0, p1, p2));
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0001BC05 File Offset: 0x00019E05
		internal static Exception DbPropertyEntry_SettingEntityRefNotSupported(object p0, object p1, object p2)
		{
			return new NotSupportedException(Strings.DbPropertyEntry_SettingEntityRefNotSupported(p0, p1, p2));
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0001BC14 File Offset: 0x00019E14
		internal static Exception DbPropertyEntry_NotSupportedForPropertiesNotInTheModel(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.DbPropertyEntry_NotSupportedForPropertiesNotInTheModel(p0, p1, p2));
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0001BC23 File Offset: 0x00019E23
		internal static Exception DbEntityEntry_NotSupportedForDetached(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbEntityEntry_NotSupportedForDetached(p0, p1));
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0001BC31 File Offset: 0x00019E31
		internal static Exception DbSet_BadTypeForAddAttachRemove(object p0, object p1, object p2)
		{
			return new ArgumentException(Strings.DbSet_BadTypeForAddAttachRemove(p0, p1, p2));
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0001BC40 File Offset: 0x00019E40
		internal static Exception DbSet_BadTypeForCreate(object p0, object p1)
		{
			return new ArgumentException(Strings.DbSet_BadTypeForCreate(p0, p1));
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0001BC4E File Offset: 0x00019E4E
		internal static Exception DbEntity_BadTypeForCast(object p0, object p1, object p2)
		{
			return new InvalidCastException(Strings.DbEntity_BadTypeForCast(p0, p1, p2));
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0001BC5D File Offset: 0x00019E5D
		internal static Exception DbMember_BadTypeForCast(object p0, object p1, object p2, object p3, object p4)
		{
			return new InvalidCastException(Strings.DbMember_BadTypeForCast(p0, p1, p2, p3, p4));
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0001BC6F File Offset: 0x00019E6F
		internal static Exception DbEntityEntry_UsedReferenceForCollectionProp(object p0, object p1)
		{
			return new ArgumentException(Strings.DbEntityEntry_UsedReferenceForCollectionProp(p0, p1));
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0001BC7D File Offset: 0x00019E7D
		internal static Exception DbEntityEntry_UsedCollectionForReferenceProp(object p0, object p1)
		{
			return new ArgumentException(Strings.DbEntityEntry_UsedCollectionForReferenceProp(p0, p1));
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0001BC8B File Offset: 0x00019E8B
		internal static Exception DbEntityEntry_NotANavigationProperty(object p0, object p1)
		{
			return new ArgumentException(Strings.DbEntityEntry_NotANavigationProperty(p0, p1));
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0001BC99 File Offset: 0x00019E99
		internal static Exception DbEntityEntry_NotAScalarProperty(object p0, object p1)
		{
			return new ArgumentException(Strings.DbEntityEntry_NotAScalarProperty(p0, p1));
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0001BCA7 File Offset: 0x00019EA7
		internal static Exception DbEntityEntry_NotAComplexProperty(object p0, object p1)
		{
			return new ArgumentException(Strings.DbEntityEntry_NotAComplexProperty(p0, p1));
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0001BCB5 File Offset: 0x00019EB5
		internal static Exception DbEntityEntry_NotAProperty(object p0, object p1)
		{
			return new ArgumentException(Strings.DbEntityEntry_NotAProperty(p0, p1));
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0001BCC3 File Offset: 0x00019EC3
		internal static Exception DbEntityEntry_DottedPartNotComplex(object p0, object p1, object p2)
		{
			return new ArgumentException(Strings.DbEntityEntry_DottedPartNotComplex(p0, p1, p2));
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0001BCD2 File Offset: 0x00019ED2
		internal static Exception DbEntityEntry_DottedPathMustBeProperty(object p0)
		{
			return new ArgumentException(Strings.DbEntityEntry_DottedPathMustBeProperty(p0));
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0001BCDF File Offset: 0x00019EDF
		internal static Exception DbEntityEntry_WrongGenericForNavProp(object p0, object p1, object p2, object p3)
		{
			return new ArgumentException(Strings.DbEntityEntry_WrongGenericForNavProp(p0, p1, p2, p3));
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0001BCEF File Offset: 0x00019EEF
		internal static Exception DbEntityEntry_WrongGenericForCollectionNavProp(object p0, object p1, object p2, object p3)
		{
			return new ArgumentException(Strings.DbEntityEntry_WrongGenericForCollectionNavProp(p0, p1, p2, p3));
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0001BCFF File Offset: 0x00019EFF
		internal static Exception DbEntityEntry_WrongGenericForProp(object p0, object p1, object p2, object p3)
		{
			return new ArgumentException(Strings.DbEntityEntry_WrongGenericForProp(p0, p1, p2, p3));
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0001BD0F File Offset: 0x00019F0F
		internal static Exception DbPropertyValues_CannotGetValuesForState(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_CannotGetValuesForState(p0, p1));
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0001BD1D File Offset: 0x00019F1D
		internal static Exception DbPropertyValues_CannotSetNullValue(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_CannotSetNullValue(p0, p1, p2));
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0001BD2C File Offset: 0x00019F2C
		internal static Exception DbPropertyValues_CannotGetStoreValuesWhenComplexPropertyIsNull(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_CannotGetStoreValuesWhenComplexPropertyIsNull(p0, p1));
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0001BD3A File Offset: 0x00019F3A
		internal static Exception DbPropertyValues_WrongTypeForAssignment(object p0, object p1, object p2, object p3)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_WrongTypeForAssignment(p0, p1, p2, p3));
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0001BD4A File Offset: 0x00019F4A
		internal static Exception DbPropertyValues_PropertyValueNamesAreReadonly()
		{
			return new NotSupportedException(Strings.DbPropertyValues_PropertyValueNamesAreReadonly);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0001BD56 File Offset: 0x00019F56
		internal static Exception DbPropertyValues_PropertyDoesNotExist(object p0, object p1)
		{
			return new ArgumentException(Strings.DbPropertyValues_PropertyDoesNotExist(p0, p1));
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0001BD64 File Offset: 0x00019F64
		internal static Exception DbPropertyValues_AttemptToSetValuesFromWrongObject(object p0, object p1)
		{
			return new ArgumentException(Strings.DbPropertyValues_AttemptToSetValuesFromWrongObject(p0, p1));
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0001BD72 File Offset: 0x00019F72
		internal static Exception DbPropertyValues_AttemptToSetValuesFromWrongType(object p0, object p1)
		{
			return new ArgumentException(Strings.DbPropertyValues_AttemptToSetValuesFromWrongType(p0, p1));
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0001BD80 File Offset: 0x00019F80
		internal static Exception DbPropertyValues_AttemptToSetNonValuesOnComplexProperty()
		{
			return new ArgumentException(Strings.DbPropertyValues_AttemptToSetNonValuesOnComplexProperty);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0001BD8C File Offset: 0x00019F8C
		internal static Exception DbPropertyValues_ComplexObjectCannotBeNull(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_ComplexObjectCannotBeNull(p0, p1));
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0001BD9A File Offset: 0x00019F9A
		internal static Exception DbPropertyValues_NestedPropertyValuesNull(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_NestedPropertyValuesNull(p0, p1));
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0001BDA8 File Offset: 0x00019FA8
		internal static Exception DbPropertyValues_CannotSetPropertyOnNullCurrentValue(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_CannotSetPropertyOnNullCurrentValue(p0, p1));
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0001BDB6 File Offset: 0x00019FB6
		internal static Exception DbPropertyValues_CannotSetPropertyOnNullOriginalValue(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbPropertyValues_CannotSetPropertyOnNullOriginalValue(p0, p1));
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0001BDC4 File Offset: 0x00019FC4
		internal static Exception DatabaseInitializationStrategy_ModelMismatch(object p0)
		{
			return new InvalidOperationException(Strings.DatabaseInitializationStrategy_ModelMismatch(p0));
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0001BDD1 File Offset: 0x00019FD1
		internal static Exception Database_DatabaseAlreadyExists(object p0)
		{
			return new InvalidOperationException(Strings.Database_DatabaseAlreadyExists(p0));
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0001BDDE File Offset: 0x00019FDE
		internal static Exception Database_NonCodeFirstCompatibilityCheck()
		{
			return new NotSupportedException(Strings.Database_NonCodeFirstCompatibilityCheck);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0001BDEA File Offset: 0x00019FEA
		internal static Exception Database_NoDatabaseMetadata()
		{
			return new NotSupportedException(Strings.Database_NoDatabaseMetadata);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0001BDF6 File Offset: 0x00019FF6
		internal static Exception ContextConfiguredMultipleTimes(object p0)
		{
			return new InvalidOperationException(Strings.ContextConfiguredMultipleTimes(p0));
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0001BE03 File Offset: 0x0001A003
		internal static Exception DbContext_ContextUsedInModelCreating()
		{
			return new InvalidOperationException(Strings.DbContext_ContextUsedInModelCreating);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0001BE0F File Offset: 0x0001A00F
		internal static Exception DbContext_MESTNotSupported()
		{
			return new InvalidOperationException(Strings.DbContext_MESTNotSupported);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0001BE1B File Offset: 0x0001A01B
		internal static Exception DbContext_Disposed()
		{
			return new InvalidOperationException(Strings.DbContext_Disposed);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0001BE27 File Offset: 0x0001A027
		internal static Exception DbContext_ProviderReturnedNullConnection()
		{
			return new InvalidOperationException(Strings.DbContext_ProviderReturnedNullConnection);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0001BE33 File Offset: 0x0001A033
		internal static Exception DbContext_ProviderNameMissing(object p0)
		{
			return new InvalidOperationException(Strings.DbContext_ProviderNameMissing(p0));
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0001BE40 File Offset: 0x0001A040
		internal static Exception DbContext_ConnectionFactoryReturnedNullConnection()
		{
			return new InvalidOperationException(Strings.DbContext_ConnectionFactoryReturnedNullConnection);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0001BE4C File Offset: 0x0001A04C
		internal static Exception DbSet_WrongEntityTypeFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DbSet_WrongEntityTypeFound(p0, p1));
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0001BE5A File Offset: 0x0001A05A
		internal static Exception DbSet_MultipleAddedEntitiesFound()
		{
			return new InvalidOperationException(Strings.DbSet_MultipleAddedEntitiesFound);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0001BE66 File Offset: 0x0001A066
		internal static Exception DbSet_DbSetUsedWithComplexType(object p0)
		{
			return new InvalidOperationException(Strings.DbSet_DbSetUsedWithComplexType(p0));
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0001BE73 File Offset: 0x0001A073
		internal static Exception DbSet_PocoAndNonPocoMixedInSameAssembly(object p0)
		{
			return new InvalidOperationException(Strings.DbSet_PocoAndNonPocoMixedInSameAssembly(p0));
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0001BE80 File Offset: 0x0001A080
		internal static Exception DbSet_EntityTypeNotInModel(object p0)
		{
			return new InvalidOperationException(Strings.DbSet_EntityTypeNotInModel(p0));
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0001BE8D File Offset: 0x0001A08D
		internal static Exception DbQuery_BindingToDbQueryNotSupported()
		{
			return new NotSupportedException(Strings.DbQuery_BindingToDbQueryNotSupported);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0001BE99 File Offset: 0x0001A099
		internal static Exception DbContext_ConnectionStringNotFound(object p0)
		{
			return new InvalidOperationException(Strings.DbContext_ConnectionStringNotFound(p0));
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0001BEA6 File Offset: 0x0001A0A6
		internal static Exception DbContext_ConnectionHasModel()
		{
			return new InvalidOperationException(Strings.DbContext_ConnectionHasModel);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0001BEB2 File Offset: 0x0001A0B2
		internal static Exception DbCollectionEntry_CannotSetCollectionProp(object p0, object p1)
		{
			return new NotSupportedException(Strings.DbCollectionEntry_CannotSetCollectionProp(p0, p1));
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0001BEC0 File Offset: 0x0001A0C0
		internal static Exception CodeFirstCachedMetadataWorkspace_SameModelDifferentProvidersNotSupported()
		{
			return new NotSupportedException(Strings.CodeFirstCachedMetadataWorkspace_SameModelDifferentProvidersNotSupported);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0001BECC File Offset: 0x0001A0CC
		internal static Exception Mapping_MESTNotSupported(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.Mapping_MESTNotSupported(p0, p1, p2));
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0001BEDB File Offset: 0x0001A0DB
		internal static Exception DbModelBuilder_MissingRequiredCtor(object p0)
		{
			return new InvalidOperationException(Strings.DbModelBuilder_MissingRequiredCtor(p0));
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0001BEE8 File Offset: 0x0001A0E8
		internal static Exception SqlConnectionFactory_MdfNotSupported(object p0)
		{
			return new NotSupportedException(Strings.SqlConnectionFactory_MdfNotSupported(p0));
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0001BEF5 File Offset: 0x0001A0F5
		internal static Exception EdmxWriter_EdmxFromObjectContextNotSupported()
		{
			return new NotSupportedException(Strings.EdmxWriter_EdmxFromObjectContextNotSupported);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0001BF01 File Offset: 0x0001A101
		internal static Exception EdmxWriter_EdmxFromModelFirstNotSupported()
		{
			return new NotSupportedException(Strings.EdmxWriter_EdmxFromModelFirstNotSupported);
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0001BF0D File Offset: 0x0001A10D
		internal static Exception EdmxWriter_EdmxFromRawCompiledModelNotSupported()
		{
			return new NotSupportedException(Strings.EdmxWriter_EdmxFromRawCompiledModelNotSupported);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0001BF19 File Offset: 0x0001A119
		internal static Exception DbContextServices_MissingDefaultCtor(object p0)
		{
			return new InvalidOperationException(Strings.DbContextServices_MissingDefaultCtor(p0));
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0001BF26 File Offset: 0x0001A126
		internal static Exception CannotCallGenericSetWithProxyType()
		{
			return new InvalidOperationException(Strings.CannotCallGenericSetWithProxyType);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0001BF32 File Offset: 0x0001A132
		internal static Exception MaxLengthAttribute_InvalidMaxLength()
		{
			return new InvalidOperationException(Strings.MaxLengthAttribute_InvalidMaxLength);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0001BF3E File Offset: 0x0001A13E
		internal static Exception MinLengthAttribute_InvalidMinLength()
		{
			return new InvalidOperationException(Strings.MinLengthAttribute_InvalidMinLength);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0001BF4A File Offset: 0x0001A14A
		internal static Exception DbConnectionInfo_ConnectionStringNotFound(object p0)
		{
			return new InvalidOperationException(Strings.DbConnectionInfo_ConnectionStringNotFound(p0));
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0001BF57 File Offset: 0x0001A157
		internal static Exception EagerInternalContext_CannotSetConnectionInfo()
		{
			return new InvalidOperationException(Strings.EagerInternalContext_CannotSetConnectionInfo);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0001BF63 File Offset: 0x0001A163
		internal static Exception LazyInternalContext_CannotReplaceEfConnectionWithDbConnection()
		{
			return new InvalidOperationException(Strings.LazyInternalContext_CannotReplaceEfConnectionWithDbConnection);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0001BF6F File Offset: 0x0001A16F
		internal static Exception LazyInternalContext_CannotReplaceDbConnectionWithEfConnection()
		{
			return new InvalidOperationException(Strings.LazyInternalContext_CannotReplaceDbConnectionWithEfConnection);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0001BF7B File Offset: 0x0001A17B
		internal static Exception EntityKey_UnexpectedNull()
		{
			return new InvalidOperationException(Strings.EntityKey_UnexpectedNull);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0001BF87 File Offset: 0x0001A187
		internal static Exception EntityClient_ConnectionStringNeededBeforeOperation()
		{
			return new InvalidOperationException(Strings.EntityClient_ConnectionStringNeededBeforeOperation);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0001BF93 File Offset: 0x0001A193
		internal static Exception EntityClient_ConnectionNotOpen()
		{
			return new InvalidOperationException(Strings.EntityClient_ConnectionNotOpen);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0001BF9F File Offset: 0x0001A19F
		internal static Exception EntityClient_NoConnectionForAdapter()
		{
			return new InvalidOperationException(Strings.EntityClient_NoConnectionForAdapter);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0001BFAB File Offset: 0x0001A1AB
		internal static Exception EntityClient_ClosedConnectionForUpdate()
		{
			return new InvalidOperationException(Strings.EntityClient_ClosedConnectionForUpdate);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0001BFB7 File Offset: 0x0001A1B7
		internal static Exception EntityClient_NoStoreConnectionForUpdate()
		{
			return new InvalidOperationException(Strings.EntityClient_NoStoreConnectionForUpdate);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0001BFC3 File Offset: 0x0001A1C3
		internal static Exception Mapping_Default_OCMapping_Member_Type_Mismatch(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7)
		{
			return new MappingException(Strings.Mapping_Default_OCMapping_Member_Type_Mismatch(p0, p1, p2, p3, p4, p5, p6, p7));
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0001BFDB File Offset: 0x0001A1DB
		internal static Exception ObjectStateManager_ConflictingChangesOfRelationshipDetected(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ObjectStateManager_ConflictingChangesOfRelationshipDetected(p0, p1));
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0001BFE9 File Offset: 0x0001A1E9
		internal static Exception RelatedEnd_InvalidOwnerStateForAttach()
		{
			return new InvalidOperationException(Strings.RelatedEnd_InvalidOwnerStateForAttach);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0001BFF5 File Offset: 0x0001A1F5
		internal static Exception RelatedEnd_InvalidNthElementNullForAttach(object p0)
		{
			return new InvalidOperationException(Strings.RelatedEnd_InvalidNthElementNullForAttach(p0));
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0001C002 File Offset: 0x0001A202
		internal static Exception RelatedEnd_InvalidNthElementContextForAttach(object p0)
		{
			return new InvalidOperationException(Strings.RelatedEnd_InvalidNthElementContextForAttach(p0));
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0001C00F File Offset: 0x0001A20F
		internal static Exception RelatedEnd_InvalidNthElementStateForAttach(object p0)
		{
			return new InvalidOperationException(Strings.RelatedEnd_InvalidNthElementStateForAttach(p0));
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0001C01C File Offset: 0x0001A21C
		internal static Exception RelatedEnd_InvalidEntityContextForAttach()
		{
			return new InvalidOperationException(Strings.RelatedEnd_InvalidEntityContextForAttach);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0001C028 File Offset: 0x0001A228
		internal static Exception RelatedEnd_InvalidEntityStateForAttach()
		{
			return new InvalidOperationException(Strings.RelatedEnd_InvalidEntityStateForAttach);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0001C034 File Offset: 0x0001A234
		internal static Exception RelatedEnd_UnableToAddRelationshipWithDeletedEntity()
		{
			return new InvalidOperationException(Strings.RelatedEnd_UnableToAddRelationshipWithDeletedEntity);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0001C040 File Offset: 0x0001A240
		internal static Exception Collections_NoRelationshipSetMatched(object p0)
		{
			return new InvalidOperationException(Strings.Collections_NoRelationshipSetMatched(p0));
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0001C04D File Offset: 0x0001A24D
		internal static Exception Collections_InvalidEntityStateSource()
		{
			return new InvalidOperationException(Strings.Collections_InvalidEntityStateSource);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0001C059 File Offset: 0x0001A259
		internal static Exception Collections_InvalidEntityStateLoad(object p0)
		{
			return new InvalidOperationException(Strings.Collections_InvalidEntityStateLoad(p0));
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0001C066 File Offset: 0x0001A266
		internal static Exception EntityReference_LessThanExpectedRelatedEntitiesFound()
		{
			return new InvalidOperationException(Strings.EntityReference_LessThanExpectedRelatedEntitiesFound);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0001C072 File Offset: 0x0001A272
		internal static Exception EntityReference_MoreThanExpectedRelatedEntitiesFound()
		{
			return new InvalidOperationException(Strings.EntityReference_MoreThanExpectedRelatedEntitiesFound);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0001C07E File Offset: 0x0001A27E
		internal static Exception EntityReference_CannotSetSpecialKeys()
		{
			return new InvalidOperationException(Strings.EntityReference_CannotSetSpecialKeys);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0001C08A File Offset: 0x0001A28A
		internal static Exception RelatedEnd_RelatedEndNotFound()
		{
			return new InvalidOperationException(Strings.RelatedEnd_RelatedEndNotFound);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0001C096 File Offset: 0x0001A296
		internal static Exception RelatedEnd_RelatedEndNotAttachedToContext(object p0)
		{
			return new InvalidOperationException(Strings.RelatedEnd_RelatedEndNotAttachedToContext(p0));
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0001C0A3 File Offset: 0x0001A2A3
		internal static Exception RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd()
		{
			return new InvalidOperationException(Strings.RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd);
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0001C0AF File Offset: 0x0001A2AF
		internal static Exception RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd()
		{
			return new InvalidOperationException(Strings.RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd);
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0001C0BB File Offset: 0x0001A2BB
		internal static Exception RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(object p0)
		{
			return new InvalidOperationException(Strings.RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(p0));
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0001C0C8 File Offset: 0x0001A2C8
		internal static Exception RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts()
		{
			return new InvalidOperationException(Strings.RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0001C0D4 File Offset: 0x0001A2D4
		internal static Exception RelatedEnd_MismatchedMergeOptionOnLoad(object p0)
		{
			return new InvalidOperationException(Strings.RelatedEnd_MismatchedMergeOptionOnLoad(p0));
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0001C0E1 File Offset: 0x0001A2E1
		internal static Exception RelatedEnd_EntitySetIsNotValidForRelationship(object p0, object p1, object p2, object p3, object p4)
		{
			return new InvalidOperationException(Strings.RelatedEnd_EntitySetIsNotValidForRelationship(p0, p1, p2, p3, p4));
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0001C0F3 File Offset: 0x0001A2F3
		internal static Exception RelatedEnd_OwnerIsNull()
		{
			return new InvalidOperationException(Strings.RelatedEnd_OwnerIsNull);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0001C0FF File Offset: 0x0001A2FF
		internal static Exception RelationshipManager_NavigationPropertyNotFound(object p0)
		{
			return new InvalidOperationException(Strings.RelationshipManager_NavigationPropertyNotFound(p0));
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0001C10C File Offset: 0x0001A30C
		internal static Exception ADP_ClosedDataReaderError()
		{
			return new InvalidOperationException(Strings.ADP_ClosedDataReaderError);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0001C118 File Offset: 0x0001A318
		internal static Exception ADP_DataReaderClosed(object p0)
		{
			return new InvalidOperationException(Strings.ADP_DataReaderClosed(p0));
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0001C125 File Offset: 0x0001A325
		internal static Exception ADP_ImplicitlyClosedDataReaderError()
		{
			return new InvalidOperationException(Strings.ADP_ImplicitlyClosedDataReaderError);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0001C131 File Offset: 0x0001A331
		internal static Exception ADP_NoData()
		{
			return new InvalidOperationException(Strings.ADP_NoData);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0001C13D File Offset: 0x0001A33D
		internal static Exception InvalidEdmMemberInstance()
		{
			return new ArgumentException(Strings.InvalidEdmMemberInstance);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0001C149 File Offset: 0x0001A349
		internal static Exception EnableMigrations_MultipleContextsWithName(object p0, object p1)
		{
			return new MigrationsException(Strings.EnableMigrations_MultipleContextsWithName(p0, p1));
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0001C157 File Offset: 0x0001A357
		internal static Exception EnableMigrations_NoContext(object p0)
		{
			return new MigrationsException(Strings.EnableMigrations_NoContext(p0));
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0001C164 File Offset: 0x0001A364
		internal static Exception EnableMigrations_NoContextWithName(object p0, object p1)
		{
			return new MigrationsException(Strings.EnableMigrations_NoContextWithName(p0, p1));
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0001C172 File Offset: 0x0001A372
		internal static Exception MoreThanOneElement()
		{
			return new InvalidOperationException(Strings.MoreThanOneElement);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0001C17E File Offset: 0x0001A37E
		internal static Exception IQueryable_Not_Async(object p0)
		{
			return new InvalidOperationException(Strings.IQueryable_Not_Async(p0));
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0001C18B File Offset: 0x0001A38B
		internal static Exception IQueryable_Provider_Not_Async()
		{
			return new InvalidOperationException(Strings.IQueryable_Provider_Not_Async);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0001C197 File Offset: 0x0001A397
		internal static Exception EmptySequence()
		{
			return new InvalidOperationException(Strings.EmptySequence);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0001C1A3 File Offset: 0x0001A3A3
		internal static Exception UnableToMoveHistoryTableWithAuto()
		{
			return new MigrationsException(Strings.UnableToMoveHistoryTableWithAuto);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0001C1AF File Offset: 0x0001A3AF
		internal static Exception NoMatch()
		{
			return new InvalidOperationException(Strings.NoMatch);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0001C1BB File Offset: 0x0001A3BB
		internal static Exception MoreThanOneMatch()
		{
			return new InvalidOperationException(Strings.MoreThanOneMatch);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0001C1C7 File Offset: 0x0001A3C7
		internal static Exception ModelBuilder_PropertyFilterTypeMustBePrimitive(object p0)
		{
			return new InvalidOperationException(Strings.ModelBuilder_PropertyFilterTypeMustBePrimitive(p0));
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0001C1D4 File Offset: 0x0001A3D4
		internal static Exception MigrationsPendingException(object p0)
		{
			return new MigrationsPendingException(Strings.MigrationsPendingException(p0));
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0001C1E1 File Offset: 0x0001A3E1
		internal static Exception BaseTypeNotMappedToFunctions(object p0, object p1)
		{
			return new InvalidOperationException(Strings.BaseTypeNotMappedToFunctions(p0, p1));
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0001C1EF File Offset: 0x0001A3EF
		internal static Exception InvalidResourceName(object p0)
		{
			return new ArgumentException(Strings.InvalidResourceName(p0));
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0001C1FC File Offset: 0x0001A3FC
		internal static Exception ModificationFunctionParameterNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ModificationFunctionParameterNotFound(p0, p1));
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0001C20A File Offset: 0x0001A40A
		internal static Exception EntityClient_CannotOpenBrokenConnection()
		{
			return new InvalidOperationException(Strings.EntityClient_CannotOpenBrokenConnection);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0001C216 File Offset: 0x0001A416
		internal static Exception ModificationFunctionParameterNotFoundOriginal(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ModificationFunctionParameterNotFoundOriginal(p0, p1));
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0001C224 File Offset: 0x0001A424
		internal static Exception ResultBindingNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ResultBindingNotFound(p0, p1));
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0001C232 File Offset: 0x0001A432
		internal static Exception ConflictingFunctionsMapping(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ConflictingFunctionsMapping(p0, p1));
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0001C240 File Offset: 0x0001A440
		internal static Exception AutomaticStaleFunctions(object p0)
		{
			return new MigrationsException(Strings.AutomaticStaleFunctions(p0));
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0001C24D File Offset: 0x0001A44D
		internal static Exception UnableToUpgradeHistoryWhenCustomFactory()
		{
			return new MigrationsException(Strings.UnableToUpgradeHistoryWhenCustomFactory);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0001C259 File Offset: 0x0001A459
		internal static Exception StoreTypeNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.StoreTypeNotFound(p0, p1));
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0001C267 File Offset: 0x0001A467
		internal static Exception IndexPropertyNotFound(object p0, object p1)
		{
			return new InvalidOperationException(Strings.IndexPropertyNotFound(p0, p1));
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0001C275 File Offset: 0x0001A475
		internal static Exception ConflictingIndexAttributeMatches(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ConflictingIndexAttributeMatches(p0, p1));
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0001C283 File Offset: 0x0001A483
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0001C28B File Offset: 0x0001A48B
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0001C292 File Offset: 0x0001A492
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
