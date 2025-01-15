using System;
using System.CodeDom.Compiler;

namespace System.Data.Entity.Resources
{
	// Token: 0x0200009A RID: 154
	[GeneratedCode("Resources.tt", "1.0.0.0")]
	internal static class Strings
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00012EA3 File Offset: 0x000110A3
		internal static string AutomaticMigration
		{
			get
			{
				return EntityRes.GetString("AutomaticMigration");
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00012EAF File Offset: 0x000110AF
		internal static string BootstrapMigration
		{
			get
			{
				return EntityRes.GetString("BootstrapMigration");
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00012EBB File Offset: 0x000110BB
		internal static string InitialCreate
		{
			get
			{
				return EntityRes.GetString("InitialCreate");
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00012EC7 File Offset: 0x000110C7
		internal static string AutomaticDataLoss
		{
			get
			{
				return EntityRes.GetString("AutomaticDataLoss");
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00012ED3 File Offset: 0x000110D3
		internal static string LoggingAutoMigrate(object p0)
		{
			return EntityRes.GetString("LoggingAutoMigrate", new object[] { p0 });
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00012EE9 File Offset: 0x000110E9
		internal static string LoggingRevertAutoMigrate(object p0)
		{
			return EntityRes.GetString("LoggingRevertAutoMigrate", new object[] { p0 });
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00012EFF File Offset: 0x000110FF
		internal static string LoggingApplyMigration(object p0)
		{
			return EntityRes.GetString("LoggingApplyMigration", new object[] { p0 });
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00012F15 File Offset: 0x00011115
		internal static string LoggingRevertMigration(object p0)
		{
			return EntityRes.GetString("LoggingRevertMigration", new object[] { p0 });
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00012F2B File Offset: 0x0001112B
		internal static string LoggingSeedingDatabase
		{
			get
			{
				return EntityRes.GetString("LoggingSeedingDatabase");
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00012F37 File Offset: 0x00011137
		internal static string LoggingPendingMigrations(object p0, object p1)
		{
			return EntityRes.GetString("LoggingPendingMigrations", new object[] { p0, p1 });
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00012F51 File Offset: 0x00011151
		internal static string LoggingPendingMigrationsDown(object p0, object p1)
		{
			return EntityRes.GetString("LoggingPendingMigrationsDown", new object[] { p0, p1 });
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00012F6B File Offset: 0x0001116B
		internal static string LoggingNoExplicitMigrations
		{
			get
			{
				return EntityRes.GetString("LoggingNoExplicitMigrations");
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00012F77 File Offset: 0x00011177
		internal static string LoggingAlreadyAtTarget(object p0)
		{
			return EntityRes.GetString("LoggingAlreadyAtTarget", new object[] { p0 });
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00012F8D File Offset: 0x0001118D
		internal static string LoggingTargetDatabase(object p0)
		{
			return EntityRes.GetString("LoggingTargetDatabase", new object[] { p0 });
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00012FA3 File Offset: 0x000111A3
		internal static string LoggingTargetDatabaseFormat(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("LoggingTargetDatabaseFormat", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00012FC5 File Offset: 0x000111C5
		internal static string LoggingExplicit
		{
			get
			{
				return EntityRes.GetString("LoggingExplicit");
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00012FD1 File Offset: 0x000111D1
		internal static string UpgradingHistoryTable
		{
			get
			{
				return EntityRes.GetString("UpgradingHistoryTable");
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00012FDD File Offset: 0x000111DD
		internal static string MetadataOutOfDate
		{
			get
			{
				return EntityRes.GetString("MetadataOutOfDate");
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00012FE9 File Offset: 0x000111E9
		internal static string MigrationNotFound(object p0)
		{
			return EntityRes.GetString("MigrationNotFound", new object[] { p0 });
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00012FFF File Offset: 0x000111FF
		internal static string PartialFkOperation(object p0, object p1)
		{
			return EntityRes.GetString("PartialFkOperation", new object[] { p0, p1 });
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00013019 File Offset: 0x00011219
		internal static string AutoNotValidTarget(object p0)
		{
			return EntityRes.GetString("AutoNotValidTarget", new object[] { p0 });
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001302F File Offset: 0x0001122F
		internal static string AutoNotValidForScriptWindows(object p0)
		{
			return EntityRes.GetString("AutoNotValidForScriptWindows", new object[] { p0 });
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00013045 File Offset: 0x00011245
		internal static string ContextNotConstructible(object p0)
		{
			return EntityRes.GetString("ContextNotConstructible", new object[] { p0 });
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001305B File Offset: 0x0001125B
		internal static string AmbiguousMigrationName(object p0)
		{
			return EntityRes.GetString("AmbiguousMigrationName", new object[] { p0 });
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00013071 File Offset: 0x00011271
		internal static string AutomaticDisabledException
		{
			get
			{
				return EntityRes.GetString("AutomaticDisabledException");
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001307D File Offset: 0x0001127D
		internal static string DownScriptWindowsNotSupported
		{
			get
			{
				return EntityRes.GetString("DownScriptWindowsNotSupported");
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00013089 File Offset: 0x00011289
		internal static string AssemblyMigrator_NoConfigurationWithName(object p0, object p1)
		{
			return EntityRes.GetString("AssemblyMigrator_NoConfigurationWithName", new object[] { p0, p1 });
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000130A3 File Offset: 0x000112A3
		internal static string AssemblyMigrator_MultipleConfigurationsWithName(object p0, object p1)
		{
			return EntityRes.GetString("AssemblyMigrator_MultipleConfigurationsWithName", new object[] { p0, p1 });
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000130BD File Offset: 0x000112BD
		internal static string AssemblyMigrator_NoConfiguration(object p0)
		{
			return EntityRes.GetString("AssemblyMigrator_NoConfiguration", new object[] { p0 });
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000130D3 File Offset: 0x000112D3
		internal static string AssemblyMigrator_MultipleConfigurations(object p0)
		{
			return EntityRes.GetString("AssemblyMigrator_MultipleConfigurations", new object[] { p0 });
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000130E9 File Offset: 0x000112E9
		internal static string MigrationsNamespaceNotUnderRootNamespace(object p0, object p1)
		{
			return EntityRes.GetString("MigrationsNamespaceNotUnderRootNamespace", new object[] { p0, p1 });
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00013103 File Offset: 0x00011303
		internal static string UnableToDispatchAddOrUpdate(object p0)
		{
			return EntityRes.GetString("UnableToDispatchAddOrUpdate", new object[] { p0 });
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00013119 File Offset: 0x00011319
		internal static string NoSqlGeneratorForProvider(object p0)
		{
			return EntityRes.GetString("NoSqlGeneratorForProvider", new object[] { p0 });
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001312F File Offset: 0x0001132F
		internal static string ToolingFacade_AssemblyNotFound(object p0)
		{
			return EntityRes.GetString("ToolingFacade_AssemblyNotFound", new object[] { p0 });
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00013145 File Offset: 0x00011345
		internal static string ArgumentIsNullOrWhitespace(object p0)
		{
			return EntityRes.GetString("ArgumentIsNullOrWhitespace", new object[] { p0 });
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001315B File Offset: 0x0001135B
		internal static string EntityTypeConfigurationMismatch(object p0)
		{
			return EntityRes.GetString("EntityTypeConfigurationMismatch", new object[] { p0 });
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00013171 File Offset: 0x00011371
		internal static string ComplexTypeConfigurationMismatch(object p0)
		{
			return EntityRes.GetString("ComplexTypeConfigurationMismatch", new object[] { p0 });
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00013187 File Offset: 0x00011387
		internal static string KeyPropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("KeyPropertyNotFound", new object[] { p0, p1 });
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000131A1 File Offset: 0x000113A1
		internal static string ForeignKeyPropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("ForeignKeyPropertyNotFound", new object[] { p0, p1 });
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000131BB File Offset: 0x000113BB
		internal static string PropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("PropertyNotFound", new object[] { p0, p1 });
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x000131D5 File Offset: 0x000113D5
		internal static string NavigationPropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("NavigationPropertyNotFound", new object[] { p0, p1 });
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000131EF File Offset: 0x000113EF
		internal static string InvalidPropertyExpression(object p0)
		{
			return EntityRes.GetString("InvalidPropertyExpression", new object[] { p0 });
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00013205 File Offset: 0x00011405
		internal static string InvalidComplexPropertyExpression(object p0)
		{
			return EntityRes.GetString("InvalidComplexPropertyExpression", new object[] { p0 });
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001321B File Offset: 0x0001141B
		internal static string InvalidPropertiesExpression(object p0)
		{
			return EntityRes.GetString("InvalidPropertiesExpression", new object[] { p0 });
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00013231 File Offset: 0x00011431
		internal static string InvalidComplexPropertiesExpression(object p0)
		{
			return EntityRes.GetString("InvalidComplexPropertiesExpression", new object[] { p0 });
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00013247 File Offset: 0x00011447
		internal static string DuplicateStructuralTypeConfiguration(object p0)
		{
			return EntityRes.GetString("DuplicateStructuralTypeConfiguration", new object[] { p0 });
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001325D File Offset: 0x0001145D
		internal static string ConflictingPropertyConfiguration(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConflictingPropertyConfiguration", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001327B File Offset: 0x0001147B
		internal static string ConflictingTypeAnnotation(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ConflictingTypeAnnotation", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001329D File Offset: 0x0001149D
		internal static string ConflictingColumnConfiguration(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConflictingColumnConfiguration", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000132BB File Offset: 0x000114BB
		internal static string ConflictingConfigurationValue(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ConflictingConfigurationValue", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000132DD File Offset: 0x000114DD
		internal static string ConflictingAnnotationValue(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConflictingAnnotationValue", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x000132FB File Offset: 0x000114FB
		internal static string ConflictingIndexAttributeProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConflictingIndexAttributeProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00013319 File Offset: 0x00011519
		internal static string ConflictingIndexAttribute(object p0, object p1)
		{
			return EntityRes.GetString("ConflictingIndexAttribute", new object[] { p0, p1 });
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00013333 File Offset: 0x00011533
		internal static string ConflictingIndexAttributesOnProperty(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ConflictingIndexAttributesOnProperty", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00013355 File Offset: 0x00011555
		internal static string IncompatibleTypes(object p0, object p1)
		{
			return EntityRes.GetString("IncompatibleTypes", new object[] { p0, p1 });
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001336F File Offset: 0x0001156F
		internal static string AnnotationSerializeWrongType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("AnnotationSerializeWrongType", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001338D File Offset: 0x0001158D
		internal static string AnnotationSerializeBadFormat(object p0, object p1, object p2)
		{
			return EntityRes.GetString("AnnotationSerializeBadFormat", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000133AB File Offset: 0x000115AB
		internal static string ConflictWhenConsolidating(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConflictWhenConsolidating", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000133C9 File Offset: 0x000115C9
		internal static string OrderConflictWhenConsolidating(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("OrderConflictWhenConsolidating", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000133F0 File Offset: 0x000115F0
		internal static string CodeFirstInvalidComplexType(object p0)
		{
			return EntityRes.GetString("CodeFirstInvalidComplexType", new object[] { p0 });
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00013406 File Offset: 0x00011606
		internal static string InvalidEntityType(object p0)
		{
			return EntityRes.GetString("InvalidEntityType", new object[] { p0 });
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001341C File Offset: 0x0001161C
		internal static string SimpleNameCollision(object p0, object p1, object p2)
		{
			return EntityRes.GetString("SimpleNameCollision", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001343A File Offset: 0x0001163A
		internal static string NavigationInverseItself(object p0, object p1)
		{
			return EntityRes.GetString("NavigationInverseItself", new object[] { p0, p1 });
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00013454 File Offset: 0x00011654
		internal static string ConflictingConstraint(object p0, object p1)
		{
			return EntityRes.GetString("ConflictingConstraint", new object[] { p0, p1 });
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001346E File Offset: 0x0001166E
		internal static string ConflictingInferredColumnType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConflictingInferredColumnType", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001348C File Offset: 0x0001168C
		internal static string ConflictingMapping(object p0, object p1)
		{
			return EntityRes.GetString("ConflictingMapping", new object[] { p0, p1 });
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000134A6 File Offset: 0x000116A6
		internal static string ConflictingCascadeDeleteOperation(object p0, object p1)
		{
			return EntityRes.GetString("ConflictingCascadeDeleteOperation", new object[] { p0, p1 });
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000134C0 File Offset: 0x000116C0
		internal static string ConflictingMultiplicities(object p0, object p1)
		{
			return EntityRes.GetString("ConflictingMultiplicities", new object[] { p0, p1 });
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000134DA File Offset: 0x000116DA
		internal static string MaxLengthAttributeConvention_InvalidMaxLength(object p0, object p1)
		{
			return EntityRes.GetString("MaxLengthAttributeConvention_InvalidMaxLength", new object[] { p0, p1 });
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000134F4 File Offset: 0x000116F4
		internal static string StringLengthAttributeConvention_InvalidMaximumLength(object p0, object p1)
		{
			return EntityRes.GetString("StringLengthAttributeConvention_InvalidMaximumLength", new object[] { p0, p1 });
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001350E File Offset: 0x0001170E
		internal static string ModelGeneration_UnableToDetermineKeyOrder(object p0)
		{
			return EntityRes.GetString("ModelGeneration_UnableToDetermineKeyOrder", new object[] { p0 });
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00013524 File Offset: 0x00011724
		internal static string ForeignKeyAttributeConvention_EmptyKey(object p0, object p1)
		{
			return EntityRes.GetString("ForeignKeyAttributeConvention_EmptyKey", new object[] { p0, p1 });
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001353E File Offset: 0x0001173E
		internal static string ForeignKeyAttributeConvention_InvalidKey(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ForeignKeyAttributeConvention_InvalidKey", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00013560 File Offset: 0x00011760
		internal static string ForeignKeyAttributeConvention_InvalidNavigationProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ForeignKeyAttributeConvention_InvalidNavigationProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001357E File Offset: 0x0001177E
		internal static string ForeignKeyAttributeConvention_OrderRequired(object p0)
		{
			return EntityRes.GetString("ForeignKeyAttributeConvention_OrderRequired", new object[] { p0 });
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00013594 File Offset: 0x00011794
		internal static string InversePropertyAttributeConvention_PropertyNotFound(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InversePropertyAttributeConvention_PropertyNotFound", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000135B6 File Offset: 0x000117B6
		internal static string InversePropertyAttributeConvention_SelfInverseDetected(object p0, object p1)
		{
			return EntityRes.GetString("InversePropertyAttributeConvention_SelfInverseDetected", new object[] { p0, p1 });
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x000135D0 File Offset: 0x000117D0
		internal static string ValidationHeader
		{
			get
			{
				return EntityRes.GetString("ValidationHeader");
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000135DC File Offset: 0x000117DC
		internal static string ValidationItemFormat(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ValidationItemFormat", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000135FA File Offset: 0x000117FA
		internal static string KeyRegisteredOnDerivedType(object p0, object p1)
		{
			return EntityRes.GetString("KeyRegisteredOnDerivedType", new object[] { p0, p1 });
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00013614 File Offset: 0x00011814
		internal static string InvalidTableMapping(object p0, object p1)
		{
			return EntityRes.GetString("InvalidTableMapping", new object[] { p0, p1 });
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001362E File Offset: 0x0001182E
		internal static string InvalidTableMapping_NoTableName(object p0)
		{
			return EntityRes.GetString("InvalidTableMapping_NoTableName", new object[] { p0 });
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00013644 File Offset: 0x00011844
		internal static string InvalidChainedMappingSyntax(object p0)
		{
			return EntityRes.GetString("InvalidChainedMappingSyntax", new object[] { p0 });
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001365A File Offset: 0x0001185A
		internal static string InvalidNotNullCondition(object p0, object p1)
		{
			return EntityRes.GetString("InvalidNotNullCondition", new object[] { p0, p1 });
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00013674 File Offset: 0x00011874
		internal static string InvalidDiscriminatorType(object p0)
		{
			return EntityRes.GetString("InvalidDiscriminatorType", new object[] { p0 });
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001368A File Offset: 0x0001188A
		internal static string ConventionNotFound(object p0, object p1)
		{
			return EntityRes.GetString("ConventionNotFound", new object[] { p0, p1 });
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000136A4 File Offset: 0x000118A4
		internal static string InvalidEntitySplittingProperties(object p0)
		{
			return EntityRes.GetString("InvalidEntitySplittingProperties", new object[] { p0 });
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000136BA File Offset: 0x000118BA
		internal static string ProviderNameNotFound(object p0)
		{
			return EntityRes.GetString("ProviderNameNotFound", new object[] { p0 });
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000136D0 File Offset: 0x000118D0
		internal static string ProviderNotFound(object p0)
		{
			return EntityRes.GetString("ProviderNotFound", new object[] { p0 });
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000136E6 File Offset: 0x000118E6
		internal static string InvalidDatabaseName(object p0)
		{
			return EntityRes.GetString("InvalidDatabaseName", new object[] { p0 });
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000136FC File Offset: 0x000118FC
		internal static string EntityMappingConfiguration_DuplicateMapInheritedProperties(object p0)
		{
			return EntityRes.GetString("EntityMappingConfiguration_DuplicateMapInheritedProperties", new object[] { p0 });
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00013712 File Offset: 0x00011912
		internal static string EntityMappingConfiguration_DuplicateMappedProperties(object p0)
		{
			return EntityRes.GetString("EntityMappingConfiguration_DuplicateMappedProperties", new object[] { p0 });
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00013728 File Offset: 0x00011928
		internal static string EntityMappingConfiguration_DuplicateMappedProperty(object p0, object p1)
		{
			return EntityRes.GetString("EntityMappingConfiguration_DuplicateMappedProperty", new object[] { p0, p1 });
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00013742 File Offset: 0x00011942
		internal static string EntityMappingConfiguration_CannotMapIgnoredProperty(object p0, object p1)
		{
			return EntityRes.GetString("EntityMappingConfiguration_CannotMapIgnoredProperty", new object[] { p0, p1 });
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001375C File Offset: 0x0001195C
		internal static string EntityMappingConfiguration_InvalidTableSharing(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityMappingConfiguration_InvalidTableSharing", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001377A File Offset: 0x0001197A
		internal static string EntityMappingConfiguration_TPCWithIAsOnNonLeafType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityMappingConfiguration_TPCWithIAsOnNonLeafType", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00013798 File Offset: 0x00011998
		internal static string CannotIgnoreMappedBaseProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("CannotIgnoreMappedBaseProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000137B6 File Offset: 0x000119B6
		internal static string ModelBuilder_KeyPropertiesMustBePrimitive(object p0, object p1)
		{
			return EntityRes.GetString("ModelBuilder_KeyPropertiesMustBePrimitive", new object[] { p0, p1 });
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000137D0 File Offset: 0x000119D0
		internal static string TableNotFound(object p0)
		{
			return EntityRes.GetString("TableNotFound", new object[] { p0 });
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000137E6 File Offset: 0x000119E6
		internal static string IncorrectColumnCount(object p0)
		{
			return EntityRes.GetString("IncorrectColumnCount", new object[] { p0 });
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000137FC File Offset: 0x000119FC
		internal static string BadKeyNameForAnnotation(object p0, object p1)
		{
			return EntityRes.GetString("BadKeyNameForAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00013816 File Offset: 0x00011A16
		internal static string BadAnnotationName(object p0)
		{
			return EntityRes.GetString("BadAnnotationName", new object[] { p0 });
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001382C File Offset: 0x00011A2C
		internal static string CircularComplexTypeHierarchy
		{
			get
			{
				return EntityRes.GetString("CircularComplexTypeHierarchy");
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00013838 File Offset: 0x00011A38
		internal static string UnableToDeterminePrincipal(object p0, object p1)
		{
			return EntityRes.GetString("UnableToDeterminePrincipal", new object[] { p0, p1 });
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00013852 File Offset: 0x00011A52
		internal static string UnmappedAbstractType(object p0)
		{
			return EntityRes.GetString("UnmappedAbstractType", new object[] { p0 });
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00013868 File Offset: 0x00011A68
		internal static string UnsupportedHybridInheritanceMapping(object p0)
		{
			return EntityRes.GetString("UnsupportedHybridInheritanceMapping", new object[] { p0 });
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001387E File Offset: 0x00011A7E
		internal static string OrphanedConfiguredTableDetected(object p0)
		{
			return EntityRes.GetString("OrphanedConfiguredTableDetected", new object[] { p0 });
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00013894 File Offset: 0x00011A94
		internal static string BadTphMappingToSharedColumn(object p0, object p1, object p2, object p3, object p4, object p5, object p6)
		{
			return EntityRes.GetString("BadTphMappingToSharedColumn", new object[] { p0, p1, p2, p3, p4, p5, p6 });
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000138C5 File Offset: 0x00011AC5
		internal static string DuplicateConfiguredColumnOrder(object p0)
		{
			return EntityRes.GetString("DuplicateConfiguredColumnOrder", new object[] { p0 });
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000138DB File Offset: 0x00011ADB
		internal static string UnsupportedUseOfV3Type(object p0, object p1)
		{
			return EntityRes.GetString("UnsupportedUseOfV3Type", new object[] { p0, p1 });
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000138F5 File Offset: 0x00011AF5
		internal static string MultiplePropertiesMatchedAsKeys(object p0, object p1)
		{
			return EntityRes.GetString("MultiplePropertiesMatchedAsKeys", new object[] { p0, p1 });
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001390F File Offset: 0x00011B0F
		internal static string FailedToGetProviderInformation
		{
			get
			{
				return EntityRes.GetString("FailedToGetProviderInformation");
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001391B File Offset: 0x00011B1B
		internal static string DbPropertyEntry_CannotGetCurrentValue(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyEntry_CannotGetCurrentValue", new object[] { p0, p1 });
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00013935 File Offset: 0x00011B35
		internal static string DbPropertyEntry_CannotSetCurrentValue(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyEntry_CannotSetCurrentValue", new object[] { p0, p1 });
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001394F File Offset: 0x00011B4F
		internal static string DbPropertyEntry_NotSupportedForDetached(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DbPropertyEntry_NotSupportedForDetached", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001396D File Offset: 0x00011B6D
		internal static string DbPropertyEntry_SettingEntityRefNotSupported(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DbPropertyEntry_SettingEntityRefNotSupported", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001398B File Offset: 0x00011B8B
		internal static string DbPropertyEntry_NotSupportedForPropertiesNotInTheModel(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DbPropertyEntry_NotSupportedForPropertiesNotInTheModel", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x000139A9 File Offset: 0x00011BA9
		internal static string DbEntityEntry_NotSupportedForDetached(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_NotSupportedForDetached", new object[] { p0, p1 });
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000139C3 File Offset: 0x00011BC3
		internal static string DbSet_BadTypeForAddAttachRemove(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DbSet_BadTypeForAddAttachRemove", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000139E1 File Offset: 0x00011BE1
		internal static string DbSet_BadTypeForCreate(object p0, object p1)
		{
			return EntityRes.GetString("DbSet_BadTypeForCreate", new object[] { p0, p1 });
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000139FB File Offset: 0x00011BFB
		internal static string DbEntity_BadTypeForCast(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DbEntity_BadTypeForCast", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00013A19 File Offset: 0x00011C19
		internal static string DbMember_BadTypeForCast(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("DbMember_BadTypeForCast", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00013A40 File Offset: 0x00011C40
		internal static string DbEntityEntry_UsedReferenceForCollectionProp(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_UsedReferenceForCollectionProp", new object[] { p0, p1 });
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00013A5A File Offset: 0x00011C5A
		internal static string DbEntityEntry_UsedCollectionForReferenceProp(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_UsedCollectionForReferenceProp", new object[] { p0, p1 });
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00013A74 File Offset: 0x00011C74
		internal static string DbEntityEntry_NotANavigationProperty(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_NotANavigationProperty", new object[] { p0, p1 });
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00013A8E File Offset: 0x00011C8E
		internal static string DbEntityEntry_NotAScalarProperty(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_NotAScalarProperty", new object[] { p0, p1 });
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00013AA8 File Offset: 0x00011CA8
		internal static string DbEntityEntry_NotAComplexProperty(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_NotAComplexProperty", new object[] { p0, p1 });
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00013AC2 File Offset: 0x00011CC2
		internal static string DbEntityEntry_NotAProperty(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_NotAProperty", new object[] { p0, p1 });
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00013ADC File Offset: 0x00011CDC
		internal static string DbEntityEntry_DottedPartNotComplex(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DbEntityEntry_DottedPartNotComplex", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00013AFA File Offset: 0x00011CFA
		internal static string DbEntityEntry_DottedPathMustBeProperty(object p0)
		{
			return EntityRes.GetString("DbEntityEntry_DottedPathMustBeProperty", new object[] { p0 });
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00013B10 File Offset: 0x00011D10
		internal static string DbEntityEntry_WrongGenericForNavProp(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("DbEntityEntry_WrongGenericForNavProp", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00013B32 File Offset: 0x00011D32
		internal static string DbEntityEntry_WrongGenericForCollectionNavProp(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("DbEntityEntry_WrongGenericForCollectionNavProp", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00013B54 File Offset: 0x00011D54
		internal static string DbEntityEntry_WrongGenericForProp(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("DbEntityEntry_WrongGenericForProp", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00013B76 File Offset: 0x00011D76
		internal static string DbEntityEntry_BadPropertyExpression(object p0, object p1)
		{
			return EntityRes.GetString("DbEntityEntry_BadPropertyExpression", new object[] { p0, p1 });
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00013B90 File Offset: 0x00011D90
		internal static string DbContext_IndependentAssociationUpdateException
		{
			get
			{
				return EntityRes.GetString("DbContext_IndependentAssociationUpdateException");
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00013B9C File Offset: 0x00011D9C
		internal static string DbPropertyValues_CannotGetValuesForState(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_CannotGetValuesForState", new object[] { p0, p1 });
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00013BB6 File Offset: 0x00011DB6
		internal static string DbPropertyValues_CannotSetNullValue(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DbPropertyValues_CannotSetNullValue", new object[] { p0, p1, p2 });
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00013BD4 File Offset: 0x00011DD4
		internal static string DbPropertyValues_CannotGetStoreValuesWhenComplexPropertyIsNull(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_CannotGetStoreValuesWhenComplexPropertyIsNull", new object[] { p0, p1 });
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00013BEE File Offset: 0x00011DEE
		internal static string DbPropertyValues_WrongTypeForAssignment(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("DbPropertyValues_WrongTypeForAssignment", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00013C10 File Offset: 0x00011E10
		internal static string DbPropertyValues_PropertyValueNamesAreReadonly
		{
			get
			{
				return EntityRes.GetString("DbPropertyValues_PropertyValueNamesAreReadonly");
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00013C1C File Offset: 0x00011E1C
		internal static string DbPropertyValues_PropertyDoesNotExist(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_PropertyDoesNotExist", new object[] { p0, p1 });
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00013C36 File Offset: 0x00011E36
		internal static string DbPropertyValues_AttemptToSetValuesFromWrongObject(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_AttemptToSetValuesFromWrongObject", new object[] { p0, p1 });
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00013C50 File Offset: 0x00011E50
		internal static string DbPropertyValues_AttemptToSetValuesFromWrongType(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_AttemptToSetValuesFromWrongType", new object[] { p0, p1 });
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x00013C6A File Offset: 0x00011E6A
		internal static string DbPropertyValues_AttemptToSetNonValuesOnComplexProperty
		{
			get
			{
				return EntityRes.GetString("DbPropertyValues_AttemptToSetNonValuesOnComplexProperty");
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00013C76 File Offset: 0x00011E76
		internal static string DbPropertyValues_ComplexObjectCannotBeNull(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_ComplexObjectCannotBeNull", new object[] { p0, p1 });
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00013C90 File Offset: 0x00011E90
		internal static string DbPropertyValues_NestedPropertyValuesNull(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_NestedPropertyValuesNull", new object[] { p0, p1 });
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00013CAA File Offset: 0x00011EAA
		internal static string DbPropertyValues_CannotSetPropertyOnNullCurrentValue(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_CannotSetPropertyOnNullCurrentValue", new object[] { p0, p1 });
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00013CC4 File Offset: 0x00011EC4
		internal static string DbPropertyValues_CannotSetPropertyOnNullOriginalValue(object p0, object p1)
		{
			return EntityRes.GetString("DbPropertyValues_CannotSetPropertyOnNullOriginalValue", new object[] { p0, p1 });
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00013CDE File Offset: 0x00011EDE
		internal static string DatabaseInitializationStrategy_ModelMismatch(object p0)
		{
			return EntityRes.GetString("DatabaseInitializationStrategy_ModelMismatch", new object[] { p0 });
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00013CF4 File Offset: 0x00011EF4
		internal static string Database_DatabaseAlreadyExists(object p0)
		{
			return EntityRes.GetString("Database_DatabaseAlreadyExists", new object[] { p0 });
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00013D0A File Offset: 0x00011F0A
		internal static string Database_NonCodeFirstCompatibilityCheck
		{
			get
			{
				return EntityRes.GetString("Database_NonCodeFirstCompatibilityCheck");
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00013D16 File Offset: 0x00011F16
		internal static string Database_NoDatabaseMetadata
		{
			get
			{
				return EntityRes.GetString("Database_NoDatabaseMetadata");
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00013D22 File Offset: 0x00011F22
		internal static string Database_BadLegacyInitializerEntry(object p0, object p1)
		{
			return EntityRes.GetString("Database_BadLegacyInitializerEntry", new object[] { p0, p1 });
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00013D3C File Offset: 0x00011F3C
		internal static string Database_InitializeFromLegacyConfigFailed(object p0, object p1)
		{
			return EntityRes.GetString("Database_InitializeFromLegacyConfigFailed", new object[] { p0, p1 });
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00013D56 File Offset: 0x00011F56
		internal static string Database_InitializeFromConfigFailed(object p0, object p1)
		{
			return EntityRes.GetString("Database_InitializeFromConfigFailed", new object[] { p0, p1 });
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00013D70 File Offset: 0x00011F70
		internal static string ContextConfiguredMultipleTimes(object p0)
		{
			return EntityRes.GetString("ContextConfiguredMultipleTimes", new object[] { p0 });
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00013D86 File Offset: 0x00011F86
		internal static string SetConnectionFactoryFromConfigFailed(object p0)
		{
			return EntityRes.GetString("SetConnectionFactoryFromConfigFailed", new object[] { p0 });
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00013D9C File Offset: 0x00011F9C
		internal static string DbContext_ContextUsedInModelCreating
		{
			get
			{
				return EntityRes.GetString("DbContext_ContextUsedInModelCreating");
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x00013DA8 File Offset: 0x00011FA8
		internal static string DbContext_MESTNotSupported
		{
			get
			{
				return EntityRes.GetString("DbContext_MESTNotSupported");
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00013DB4 File Offset: 0x00011FB4
		internal static string DbContext_Disposed
		{
			get
			{
				return EntityRes.GetString("DbContext_Disposed");
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x00013DC0 File Offset: 0x00011FC0
		internal static string DbContext_ProviderReturnedNullConnection
		{
			get
			{
				return EntityRes.GetString("DbContext_ProviderReturnedNullConnection");
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00013DCC File Offset: 0x00011FCC
		internal static string DbContext_ProviderNameMissing(object p0)
		{
			return EntityRes.GetString("DbContext_ProviderNameMissing", new object[] { p0 });
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00013DE2 File Offset: 0x00011FE2
		internal static string DbContext_ConnectionFactoryReturnedNullConnection
		{
			get
			{
				return EntityRes.GetString("DbContext_ConnectionFactoryReturnedNullConnection");
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00013DEE File Offset: 0x00011FEE
		internal static string DbSet_WrongNumberOfKeyValuesPassed
		{
			get
			{
				return EntityRes.GetString("DbSet_WrongNumberOfKeyValuesPassed");
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00013DFA File Offset: 0x00011FFA
		internal static string DbSet_WrongKeyValueType
		{
			get
			{
				return EntityRes.GetString("DbSet_WrongKeyValueType");
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00013E06 File Offset: 0x00012006
		internal static string DbSet_WrongEntityTypeFound(object p0, object p1)
		{
			return EntityRes.GetString("DbSet_WrongEntityTypeFound", new object[] { p0, p1 });
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00013E20 File Offset: 0x00012020
		internal static string DbSet_MultipleAddedEntitiesFound
		{
			get
			{
				return EntityRes.GetString("DbSet_MultipleAddedEntitiesFound");
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00013E2C File Offset: 0x0001202C
		internal static string DbSet_DbSetUsedWithComplexType(object p0)
		{
			return EntityRes.GetString("DbSet_DbSetUsedWithComplexType", new object[] { p0 });
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00013E42 File Offset: 0x00012042
		internal static string DbSet_PocoAndNonPocoMixedInSameAssembly(object p0)
		{
			return EntityRes.GetString("DbSet_PocoAndNonPocoMixedInSameAssembly", new object[] { p0 });
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00013E58 File Offset: 0x00012058
		internal static string DbSet_EntityTypeNotInModel(object p0)
		{
			return EntityRes.GetString("DbSet_EntityTypeNotInModel", new object[] { p0 });
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00013E6E File Offset: 0x0001206E
		internal static string DbQuery_BindingToDbQueryNotSupported
		{
			get
			{
				return EntityRes.GetString("DbQuery_BindingToDbQueryNotSupported");
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00013E7A File Offset: 0x0001207A
		internal static string DbExtensions_InvalidIncludePathExpression
		{
			get
			{
				return EntityRes.GetString("DbExtensions_InvalidIncludePathExpression");
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00013E86 File Offset: 0x00012086
		internal static string DbContext_ConnectionStringNotFound(object p0)
		{
			return EntityRes.GetString("DbContext_ConnectionStringNotFound", new object[] { p0 });
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00013E9C File Offset: 0x0001209C
		internal static string DbContext_ConnectionHasModel
		{
			get
			{
				return EntityRes.GetString("DbContext_ConnectionHasModel");
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00013EA8 File Offset: 0x000120A8
		internal static string DbCollectionEntry_CannotSetCollectionProp(object p0, object p1)
		{
			return EntityRes.GetString("DbCollectionEntry_CannotSetCollectionProp", new object[] { p0, p1 });
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00013EC2 File Offset: 0x000120C2
		internal static string CodeFirstCachedMetadataWorkspace_SameModelDifferentProvidersNotSupported
		{
			get
			{
				return EntityRes.GetString("CodeFirstCachedMetadataWorkspace_SameModelDifferentProvidersNotSupported");
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00013ECE File Offset: 0x000120CE
		internal static string Mapping_MESTNotSupported(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_MESTNotSupported", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00013EEC File Offset: 0x000120EC
		internal static string DbModelBuilder_MissingRequiredCtor(object p0)
		{
			return EntityRes.GetString("DbModelBuilder_MissingRequiredCtor", new object[] { p0 });
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00013F02 File Offset: 0x00012102
		internal static string DbEntityValidationException_ValidationFailed
		{
			get
			{
				return EntityRes.GetString("DbEntityValidationException_ValidationFailed");
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00013F0E File Offset: 0x0001210E
		internal static string DbUnexpectedValidationException_ValidationAttribute(object p0, object p1)
		{
			return EntityRes.GetString("DbUnexpectedValidationException_ValidationAttribute", new object[] { p0, p1 });
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00013F28 File Offset: 0x00012128
		internal static string DbUnexpectedValidationException_IValidatableObject(object p0, object p1)
		{
			return EntityRes.GetString("DbUnexpectedValidationException_IValidatableObject", new object[] { p0, p1 });
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00013F42 File Offset: 0x00012142
		internal static string SqlConnectionFactory_MdfNotSupported(object p0)
		{
			return EntityRes.GetString("SqlConnectionFactory_MdfNotSupported", new object[] { p0 });
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00013F58 File Offset: 0x00012158
		internal static string Database_InitializationException
		{
			get
			{
				return EntityRes.GetString("Database_InitializationException");
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x00013F64 File Offset: 0x00012164
		internal static string EdmxWriter_EdmxFromObjectContextNotSupported
		{
			get
			{
				return EntityRes.GetString("EdmxWriter_EdmxFromObjectContextNotSupported");
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x00013F70 File Offset: 0x00012170
		internal static string EdmxWriter_EdmxFromModelFirstNotSupported
		{
			get
			{
				return EntityRes.GetString("EdmxWriter_EdmxFromModelFirstNotSupported");
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00013F7C File Offset: 0x0001217C
		internal static string EdmxWriter_EdmxFromRawCompiledModelNotSupported
		{
			get
			{
				return EntityRes.GetString("EdmxWriter_EdmxFromRawCompiledModelNotSupported");
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00013F88 File Offset: 0x00012188
		internal static string UnintentionalCodeFirstException_Message
		{
			get
			{
				return EntityRes.GetString("UnintentionalCodeFirstException_Message");
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00013F94 File Offset: 0x00012194
		internal static string DbContextServices_MissingDefaultCtor(object p0)
		{
			return EntityRes.GetString("DbContextServices_MissingDefaultCtor", new object[] { p0 });
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00013FAA File Offset: 0x000121AA
		internal static string CannotCallGenericSetWithProxyType
		{
			get
			{
				return EntityRes.GetString("CannotCallGenericSetWithProxyType");
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00013FB6 File Offset: 0x000121B6
		internal static string EdmModel_Validator_Semantic_SystemNamespaceEncountered(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SystemNamespaceEncountered", new object[] { p0 });
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00013FCC File Offset: 0x000121CC
		internal static string EdmModel_Validator_Semantic_SimilarRelationshipEnd(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SimilarRelationshipEnd", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00013FF3 File Offset: 0x000121F3
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetNameReference(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetNameReference", new object[] { p0, p1 });
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001400D File Offset: 0x0001220D
		internal static string EdmModel_Validator_Semantic_ConcurrencyRedefinedOnSubTypeOfEntitySetType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ConcurrencyRedefinedOnSubTypeOfEntitySetType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001402B File Offset: 0x0001222B
		internal static string EdmModel_Validator_Semantic_EntitySetTypeHasNoKeys(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetTypeHasNoKeys", new object[] { p0, p1 });
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00014045 File Offset: 0x00012245
		internal static string EdmModel_Validator_Semantic_DuplicateEndName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEndName", new object[] { p0 });
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001405B File Offset: 0x0001225B
		internal static string EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey", new object[] { p0, p1 });
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00014075 File Offset: 0x00012275
		internal static string EdmModel_Validator_Semantic_InvalidCollectionKindNotCollection(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidCollectionKindNotCollection", new object[] { p0 });
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001408B File Offset: 0x0001228B
		internal static string EdmModel_Validator_Semantic_InvalidCollectionKindNotV1_1(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidCollectionKindNotV1_1", new object[] { p0 });
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x000140A1 File Offset: 0x000122A1
		internal static string EdmModel_Validator_Semantic_InvalidComplexTypeAbstract(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidComplexTypeAbstract", new object[] { p0 });
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x000140B7 File Offset: 0x000122B7
		internal static string EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic", new object[] { p0 });
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x000140CD File Offset: 0x000122CD
		internal static string EdmModel_Validator_Semantic_InvalidKeyNullablePart(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidKeyNullablePart", new object[] { p0, p1 });
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x000140E7 File Offset: 0x000122E7
		internal static string EdmModel_Validator_Semantic_EntityKeyMustBeScalar(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntityKeyMustBeScalar", new object[] { p0, p1 });
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00014101 File Offset: 0x00012301
		internal static string EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass", new object[] { p0, p1 });
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001411B File Offset: 0x0001231B
		internal static string EdmModel_Validator_Semantic_KeyMissingOnEntityType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_KeyMissingOnEntityType", new object[] { p0 });
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00014131 File Offset: 0x00012331
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001414F File Offset: 0x0001234F
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame");
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0001415B File Offset: 0x0001235B
		internal static string EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation");
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00014167 File Offset: 0x00012367
		internal static string EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified", new object[] { p0, p1 });
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00014181 File Offset: 0x00012381
		internal static string EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00014197 File Offset: 0x00012397
		internal static string EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint", new object[] { p0 });
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000141AD File Offset: 0x000123AD
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleUpperBoundMustBeOne(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleUpperBoundMustBeOne", new object[] { p0, p1 });
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000141C7 File Offset: 0x000123C7
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNullableV1(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNullableV1", new object[] { p0, p1 });
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000141E1 File Offset: 0x000123E1
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV1(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV1", new object[] { p0, p1 });
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000141FB File Offset: 0x000123FB
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV2(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV2", new object[] { p0, p1 });
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00014215 File Offset: 0x00012415
		internal static string EdmModel_Validator_Semantic_InvalidToPropertyInRelationshipConstraint(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidToPropertyInRelationshipConstraint", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00014233 File Offset: 0x00012433
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeOne(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeOne", new object[] { p0, p1 });
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001424D File Offset: 0x0001244D
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeMany(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeMany", new object[] { p0, p1 });
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00014267 File Offset: 0x00012467
		internal static string EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint");
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00014273 File Offset: 0x00012473
		internal static string EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001429A File Offset: 0x0001249A
		internal static string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraint", new object[] { p0, p1 });
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000142B4 File Offset: 0x000124B4
		internal static string EdmModel_Validator_Semantic_NullableComplexType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NullableComplexType", new object[] { p0 });
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000142CA File Offset: 0x000124CA
		internal static string EdmModel_Validator_Semantic_InvalidPropertyType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyType", new object[] { p0 });
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000142E0 File Offset: 0x000124E0
		internal static string EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName", new object[] { p0 });
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000142F6 File Offset: 0x000124F6
		internal static string EdmModel_Validator_Semantic_TypeNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001430C File Offset: 0x0001250C
		internal static string EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00014326 File Offset: 0x00012526
		internal static string EdmModel_Validator_Semantic_PropertyNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PropertyNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001433C File Offset: 0x0001253C
		internal static string EdmModel_Validator_Semantic_CycleInTypeHierarchy(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_CycleInTypeHierarchy", new object[] { p0 });
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00014352 File Offset: 0x00012552
		internal static string EdmModel_Validator_Semantic_InvalidPropertyType_V1_1(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyType_V1_1", new object[] { p0 });
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00014368 File Offset: 0x00012568
		internal static string EdmModel_Validator_Semantic_InvalidPropertyType_V3(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyType_V3", new object[] { p0 });
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001437E File Offset: 0x0001257E
		internal static string EdmModel_Validator_Semantic_ComposableFunctionImportsNotSupportedForSchemaVersion
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ComposableFunctionImportsNotSupportedForSchemaVersion");
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001438A File Offset: 0x0001258A
		internal static string EdmModel_Validator_Syntactic_MissingName
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_MissingName");
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00014396 File Offset: 0x00012596
		internal static string EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong", new object[] { p0 });
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000143AC File Offset: 0x000125AC
		internal static string EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed", new object[] { p0 });
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x000143C2 File Offset: 0x000125C2
		internal static string EdmModel_Validator_Syntactic_EdmAssociationType_AssociationEndMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmAssociationType_AssociationEndMustNotBeNull");
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x000143CE File Offset: 0x000125CE
		internal static string EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentEndMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentEndMustNotBeNull");
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x000143DA File Offset: 0x000125DA
		internal static string EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentPropertiesMustNotBeEmpty
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmAssociationConstraint_DependentPropertiesMustNotBeEmpty");
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x000143E6 File Offset: 0x000125E6
		internal static string EdmModel_Validator_Syntactic_EdmNavigationProperty_AssociationMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmNavigationProperty_AssociationMustNotBeNull");
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x000143F2 File Offset: 0x000125F2
		internal static string EdmModel_Validator_Syntactic_EdmNavigationProperty_ResultEndMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmNavigationProperty_ResultEndMustNotBeNull");
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x000143FE File Offset: 0x000125FE
		internal static string EdmModel_Validator_Syntactic_EdmAssociationEnd_EntityTypeMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmAssociationEnd_EntityTypeMustNotBeNull");
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0001440A File Offset: 0x0001260A
		internal static string EdmModel_Validator_Syntactic_EdmEntitySet_ElementTypeMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmEntitySet_ElementTypeMustNotBeNull");
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00014416 File Offset: 0x00012616
		internal static string EdmModel_Validator_Syntactic_EdmAssociationSet_ElementTypeMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmAssociationSet_ElementTypeMustNotBeNull");
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00014422 File Offset: 0x00012622
		internal static string EdmModel_Validator_Syntactic_EdmAssociationSet_SourceSetMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmAssociationSet_SourceSetMustNotBeNull");
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0001442E File Offset: 0x0001262E
		internal static string EdmModel_Validator_Syntactic_EdmAssociationSet_TargetSetMustNotBeNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmAssociationSet_TargetSetMustNotBeNull");
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0001443A File Offset: 0x0001263A
		internal static string EdmModel_Validator_Syntactic_EdmTypeReferenceNotValid
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmTypeReferenceNotValid");
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00014446 File Offset: 0x00012646
		internal static string MetadataItem_InvalidDataSpace(object p0, object p1)
		{
			return EntityRes.GetString("MetadataItem_InvalidDataSpace", new object[] { p0, p1 });
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00014460 File Offset: 0x00012660
		internal static string EdmModel_AddItem_NonMatchingNamespace
		{
			get
			{
				return EntityRes.GetString("EdmModel_AddItem_NonMatchingNamespace");
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0001446C File Offset: 0x0001266C
		internal static string Serializer_OneNamespaceAndOneContainer
		{
			get
			{
				return EntityRes.GetString("Serializer_OneNamespaceAndOneContainer");
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00014478 File Offset: 0x00012678
		internal static string MaxLengthAttribute_ValidationError(object p0, object p1)
		{
			return EntityRes.GetString("MaxLengthAttribute_ValidationError", new object[] { p0, p1 });
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00014492 File Offset: 0x00012692
		internal static string MaxLengthAttribute_InvalidMaxLength
		{
			get
			{
				return EntityRes.GetString("MaxLengthAttribute_InvalidMaxLength");
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001449E File Offset: 0x0001269E
		internal static string MinLengthAttribute_ValidationError(object p0, object p1)
		{
			return EntityRes.GetString("MinLengthAttribute_ValidationError", new object[] { p0, p1 });
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x000144B8 File Offset: 0x000126B8
		internal static string MinLengthAttribute_InvalidMinLength
		{
			get
			{
				return EntityRes.GetString("MinLengthAttribute_InvalidMinLength");
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000144C4 File Offset: 0x000126C4
		internal static string DbConnectionInfo_ConnectionStringNotFound(object p0)
		{
			return EntityRes.GetString("DbConnectionInfo_ConnectionStringNotFound", new object[] { p0 });
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x000144DA File Offset: 0x000126DA
		internal static string EagerInternalContext_CannotSetConnectionInfo
		{
			get
			{
				return EntityRes.GetString("EagerInternalContext_CannotSetConnectionInfo");
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x000144E6 File Offset: 0x000126E6
		internal static string LazyInternalContext_CannotReplaceEfConnectionWithDbConnection
		{
			get
			{
				return EntityRes.GetString("LazyInternalContext_CannotReplaceEfConnectionWithDbConnection");
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x000144F2 File Offset: 0x000126F2
		internal static string LazyInternalContext_CannotReplaceDbConnectionWithEfConnection
		{
			get
			{
				return EntityRes.GetString("LazyInternalContext_CannotReplaceDbConnectionWithEfConnection");
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x000144FE File Offset: 0x000126FE
		internal static string EntityKey_EntitySetDoesNotMatch(object p0)
		{
			return EntityRes.GetString("EntityKey_EntitySetDoesNotMatch", new object[] { p0 });
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00014514 File Offset: 0x00012714
		internal static string EntityKey_IncorrectNumberOfKeyValuePairs(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityKey_IncorrectNumberOfKeyValuePairs", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00014532 File Offset: 0x00012732
		internal static string EntityKey_IncorrectValueType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityKey_IncorrectValueType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00014550 File Offset: 0x00012750
		internal static string EntityKey_NoCorrespondingOSpaceTypeForEnumKeyMember(object p0, object p1)
		{
			return EntityRes.GetString("EntityKey_NoCorrespondingOSpaceTypeForEnumKeyMember", new object[] { p0, p1 });
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001456A File Offset: 0x0001276A
		internal static string EntityKey_MissingKeyValue(object p0, object p1)
		{
			return EntityRes.GetString("EntityKey_MissingKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00014584 File Offset: 0x00012784
		internal static string EntityKey_NoNullsAllowedInKeyValuePairs
		{
			get
			{
				return EntityRes.GetString("EntityKey_NoNullsAllowedInKeyValuePairs");
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x00014590 File Offset: 0x00012790
		internal static string EntityKey_UnexpectedNull
		{
			get
			{
				return EntityRes.GetString("EntityKey_UnexpectedNull");
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001459C File Offset: 0x0001279C
		internal static string EntityKey_DoesntMatchKeyOnEntity(object p0)
		{
			return EntityRes.GetString("EntityKey_DoesntMatchKeyOnEntity", new object[] { p0 });
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x000145B2 File Offset: 0x000127B2
		internal static string EntityKey_EntityKeyMustHaveValues
		{
			get
			{
				return EntityRes.GetString("EntityKey_EntityKeyMustHaveValues");
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x000145BE File Offset: 0x000127BE
		internal static string EntityKey_InvalidQualifiedEntitySetName
		{
			get
			{
				return EntityRes.GetString("EntityKey_InvalidQualifiedEntitySetName");
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x000145CA File Offset: 0x000127CA
		internal static string EntityKey_MissingEntitySetName
		{
			get
			{
				return EntityRes.GetString("EntityKey_MissingEntitySetName");
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000145D6 File Offset: 0x000127D6
		internal static string EntityKey_InvalidName(object p0)
		{
			return EntityRes.GetString("EntityKey_InvalidName", new object[] { p0 });
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x000145EC File Offset: 0x000127EC
		internal static string EntityKey_CannotChangeKey
		{
			get
			{
				return EntityRes.GetString("EntityKey_CannotChangeKey");
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x000145F8 File Offset: 0x000127F8
		internal static string EntityTypesDoNotAgree
		{
			get
			{
				return EntityRes.GetString("EntityTypesDoNotAgree");
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00014604 File Offset: 0x00012804
		internal static string EntityKey_NullKeyValue(object p0, object p1)
		{
			return EntityRes.GetString("EntityKey_NullKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x0001461E File Offset: 0x0001281E
		internal static string EdmMembersDefiningTypeDoNotAgreeWithMetadataType
		{
			get
			{
				return EntityRes.GetString("EdmMembersDefiningTypeDoNotAgreeWithMetadataType");
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001462A File Offset: 0x0001282A
		internal static string CannotCallNoncomposableFunction(object p0)
		{
			return EntityRes.GetString("CannotCallNoncomposableFunction", new object[] { p0 });
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00014640 File Offset: 0x00012840
		internal static string EntityClient_ConnectionStringMissingInfo(object p0)
		{
			return EntityRes.GetString("EntityClient_ConnectionStringMissingInfo", new object[] { p0 });
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00014656 File Offset: 0x00012856
		internal static string EntityClient_ValueNotString
		{
			get
			{
				return EntityRes.GetString("EntityClient_ValueNotString");
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00014662 File Offset: 0x00012862
		internal static string EntityClient_KeywordNotSupported(object p0)
		{
			return EntityRes.GetString("EntityClient_KeywordNotSupported", new object[] { p0 });
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00014678 File Offset: 0x00012878
		internal static string EntityClient_NoCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoCommandText");
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00014684 File Offset: 0x00012884
		internal static string EntityClient_ConnectionStringNeededBeforeOperation
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionStringNeededBeforeOperation");
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00014690 File Offset: 0x00012890
		internal static string EntityClient_ConnectionNotOpen
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionNotOpen");
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001469C File Offset: 0x0001289C
		internal static string EntityClient_DuplicateParameterNames(object p0)
		{
			return EntityRes.GetString("EntityClient_DuplicateParameterNames", new object[] { p0 });
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x000146B2 File Offset: 0x000128B2
		internal static string EntityClient_NoConnectionForCommand
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoConnectionForCommand");
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x000146BE File Offset: 0x000128BE
		internal static string EntityClient_NoConnectionForAdapter
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoConnectionForAdapter");
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x000146CA File Offset: 0x000128CA
		internal static string EntityClient_ClosedConnectionForUpdate
		{
			get
			{
				return EntityRes.GetString("EntityClient_ClosedConnectionForUpdate");
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x000146D6 File Offset: 0x000128D6
		internal static string EntityClient_InvalidNamedConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_InvalidNamedConnection");
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000146E2 File Offset: 0x000128E2
		internal static string EntityClient_NestedNamedConnection(object p0)
		{
			return EntityRes.GetString("EntityClient_NestedNamedConnection", new object[] { p0 });
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000146F8 File Offset: 0x000128F8
		internal static string EntityClient_InvalidStoreProvider(object p0)
		{
			return EntityRes.GetString("EntityClient_InvalidStoreProvider", new object[] { p0 });
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0001470E File Offset: 0x0001290E
		internal static string EntityClient_DataReaderIsStillOpen
		{
			get
			{
				return EntityRes.GetString("EntityClient_DataReaderIsStillOpen");
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0001471A File Offset: 0x0001291A
		internal static string EntityClient_SettingsCannotBeChangedOnOpenConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_SettingsCannotBeChangedOnOpenConnection");
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00014726 File Offset: 0x00012926
		internal static string EntityClient_ExecutingOnClosedConnection(object p0)
		{
			return EntityRes.GetString("EntityClient_ExecutingOnClosedConnection", new object[] { p0 });
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0001473C File Offset: 0x0001293C
		internal static string EntityClient_ConnectionStateClosed
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionStateClosed");
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00014748 File Offset: 0x00012948
		internal static string EntityClient_ConnectionStateBroken
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionStateBroken");
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00014754 File Offset: 0x00012954
		internal static string EntityClient_CannotCloneStoreProvider
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotCloneStoreProvider");
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00014760 File Offset: 0x00012960
		internal static string EntityClient_UnsupportedCommandType
		{
			get
			{
				return EntityRes.GetString("EntityClient_UnsupportedCommandType");
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0001476C File Offset: 0x0001296C
		internal static string EntityClient_ErrorInClosingConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_ErrorInClosingConnection");
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00014778 File Offset: 0x00012978
		internal static string EntityClient_ErrorInBeginningTransaction
		{
			get
			{
				return EntityRes.GetString("EntityClient_ErrorInBeginningTransaction");
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00014784 File Offset: 0x00012984
		internal static string EntityClient_ExtraParametersWithNamedConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_ExtraParametersWithNamedConnection");
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00014790 File Offset: 0x00012990
		internal static string EntityClient_CommandDefinitionPreparationFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandDefinitionPreparationFailed");
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0001479C File Offset: 0x0001299C
		internal static string EntityClient_CommandDefinitionExecutionFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandDefinitionExecutionFailed");
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x000147A8 File Offset: 0x000129A8
		internal static string EntityClient_CommandExecutionFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandExecutionFailed");
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x000147B4 File Offset: 0x000129B4
		internal static string EntityClient_StoreReaderFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_StoreReaderFailed");
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000147C0 File Offset: 0x000129C0
		internal static string EntityClient_FailedToGetInformation(object p0)
		{
			return EntityRes.GetString("EntityClient_FailedToGetInformation", new object[] { p0 });
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x000147D6 File Offset: 0x000129D6
		internal static string EntityClient_TooFewColumns
		{
			get
			{
				return EntityRes.GetString("EntityClient_TooFewColumns");
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000147E2 File Offset: 0x000129E2
		internal static string EntityClient_InvalidParameterName(object p0)
		{
			return EntityRes.GetString("EntityClient_InvalidParameterName", new object[] { p0 });
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x000147F8 File Offset: 0x000129F8
		internal static string EntityClient_EmptyParameterName
		{
			get
			{
				return EntityRes.GetString("EntityClient_EmptyParameterName");
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00014804 File Offset: 0x00012A04
		internal static string EntityClient_ReturnedNullOnProviderMethod(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_ReturnedNullOnProviderMethod", new object[] { p0, p1 });
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0001481E File Offset: 0x00012A1E
		internal static string EntityClient_CannotDeduceDbType
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotDeduceDbType");
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001482A File Offset: 0x00012A2A
		internal static string EntityClient_InvalidParameterDirection(object p0)
		{
			return EntityRes.GetString("EntityClient_InvalidParameterDirection", new object[] { p0 });
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00014840 File Offset: 0x00012A40
		internal static string EntityClient_UnknownParameterType(object p0)
		{
			return EntityRes.GetString("EntityClient_UnknownParameterType", new object[] { p0 });
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00014856 File Offset: 0x00012A56
		internal static string EntityClient_UnsupportedDbType(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_UnsupportedDbType", new object[] { p0, p1 });
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00014870 File Offset: 0x00012A70
		internal static string EntityClient_IncompatibleNavigationPropertyResult(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_IncompatibleNavigationPropertyResult", new object[] { p0, p1 });
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0001488A File Offset: 0x00012A8A
		internal static string EntityClient_TransactionAlreadyStarted
		{
			get
			{
				return EntityRes.GetString("EntityClient_TransactionAlreadyStarted");
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00014896 File Offset: 0x00012A96
		internal static string EntityClient_InvalidTransactionForCommand
		{
			get
			{
				return EntityRes.GetString("EntityClient_InvalidTransactionForCommand");
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x000148A2 File Offset: 0x00012AA2
		internal static string EntityClient_NoStoreConnectionForUpdate
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoStoreConnectionForUpdate");
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x000148AE File Offset: 0x00012AAE
		internal static string EntityClient_CommandTreeMetadataIncompatible
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandTreeMetadataIncompatible");
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x000148BA File Offset: 0x00012ABA
		internal static string EntityClient_ProviderGeneralError
		{
			get
			{
				return EntityRes.GetString("EntityClient_ProviderGeneralError");
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000148C6 File Offset: 0x00012AC6
		internal static string EntityClient_ProviderSpecificError(object p0)
		{
			return EntityRes.GetString("EntityClient_ProviderSpecificError", new object[] { p0 });
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x000148DC File Offset: 0x00012ADC
		internal static string EntityClient_FunctionImportEmptyCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_FunctionImportEmptyCommandText");
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x000148E8 File Offset: 0x00012AE8
		internal static string EntityClient_UnableToFindFunctionImportContainer(object p0)
		{
			return EntityRes.GetString("EntityClient_UnableToFindFunctionImportContainer", new object[] { p0 });
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000148FE File Offset: 0x00012AFE
		internal static string EntityClient_UnableToFindFunctionImport(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_UnableToFindFunctionImport", new object[] { p0, p1 });
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00014918 File Offset: 0x00012B18
		internal static string EntityClient_FunctionImportMustBeNonComposable(object p0)
		{
			return EntityRes.GetString("EntityClient_FunctionImportMustBeNonComposable", new object[] { p0 });
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001492E File Offset: 0x00012B2E
		internal static string EntityClient_UnmappedFunctionImport(object p0)
		{
			return EntityRes.GetString("EntityClient_UnmappedFunctionImport", new object[] { p0 });
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00014944 File Offset: 0x00012B44
		internal static string EntityClient_InvalidStoredProcedureCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_InvalidStoredProcedureCommandText");
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00014950 File Offset: 0x00012B50
		internal static string EntityClient_ItemCollectionsNotRegisteredInWorkspace(object p0)
		{
			return EntityRes.GetString("EntityClient_ItemCollectionsNotRegisteredInWorkspace", new object[] { p0 });
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00014966 File Offset: 0x00012B66
		internal static string EntityClient_DbConnectionHasNoProvider(object p0)
		{
			return EntityRes.GetString("EntityClient_DbConnectionHasNoProvider", new object[] { p0 });
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0001497C File Offset: 0x00012B7C
		internal static string EntityClient_RequiresNonStoreCommandTree
		{
			get
			{
				return EntityRes.GetString("EntityClient_RequiresNonStoreCommandTree");
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00014988 File Offset: 0x00012B88
		internal static string EntityClient_CannotReprepareCommandDefinitionBasedCommand
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotReprepareCommandDefinitionBasedCommand");
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00014994 File Offset: 0x00012B94
		internal static string EntityClient_EntityParameterEdmTypeNotScalar(object p0)
		{
			return EntityRes.GetString("EntityClient_EntityParameterEdmTypeNotScalar", new object[] { p0 });
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000149AA File Offset: 0x00012BAA
		internal static string EntityClient_EntityParameterInconsistentEdmType(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_EntityParameterInconsistentEdmType", new object[] { p0, p1 });
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x000149C4 File Offset: 0x00012BC4
		internal static string EntityClient_CannotGetCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotGetCommandText");
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x000149D0 File Offset: 0x00012BD0
		internal static string EntityClient_CannotSetCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotSetCommandText");
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x000149DC File Offset: 0x00012BDC
		internal static string EntityClient_CannotGetCommandTree
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotGetCommandTree");
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x000149E8 File Offset: 0x00012BE8
		internal static string EntityClient_CannotSetCommandTree
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotSetCommandTree");
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x000149F4 File Offset: 0x00012BF4
		internal static string ELinq_ExpressionMustBeIQueryable
		{
			get
			{
				return EntityRes.GetString("ELinq_ExpressionMustBeIQueryable");
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00014A00 File Offset: 0x00012C00
		internal static string ELinq_UnsupportedExpressionType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedExpressionType", new object[] { p0 });
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00014A16 File Offset: 0x00012C16
		internal static string ELinq_UnsupportedUseOfContextParameter(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedUseOfContextParameter", new object[] { p0 });
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00014A2C File Offset: 0x00012C2C
		internal static string ELinq_UnboundParameterExpression(object p0)
		{
			return EntityRes.GetString("ELinq_UnboundParameterExpression", new object[] { p0 });
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00014A42 File Offset: 0x00012C42
		internal static string ELinq_UnsupportedConstructor
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedConstructor");
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00014A4E File Offset: 0x00012C4E
		internal static string ELinq_UnsupportedInitializers
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedInitializers");
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00014A5A File Offset: 0x00012C5A
		internal static string ELinq_UnsupportedBinding
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedBinding");
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00014A66 File Offset: 0x00012C66
		internal static string ELinq_UnsupportedMethod(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedMethod", new object[] { p0 });
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00014A7C File Offset: 0x00012C7C
		internal static string ELinq_UnsupportedMethodSuggestedAlternative(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedMethodSuggestedAlternative", new object[] { p0, p1 });
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00014A96 File Offset: 0x00012C96
		internal static string ELinq_ThenByDoesNotFollowOrderBy
		{
			get
			{
				return EntityRes.GetString("ELinq_ThenByDoesNotFollowOrderBy");
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00014AA2 File Offset: 0x00012CA2
		internal static string ELinq_UnrecognizedMember(object p0)
		{
			return EntityRes.GetString("ELinq_UnrecognizedMember", new object[] { p0 });
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00014AB8 File Offset: 0x00012CB8
		internal static string ELinq_UnresolvableFunctionForMethod(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMethod", new object[] { p0, p1 });
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00014AD2 File Offset: 0x00012CD2
		internal static string ELinq_UnresolvableFunctionForMethodAmbiguousMatch(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMethodAmbiguousMatch", new object[] { p0, p1 });
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00014AEC File Offset: 0x00012CEC
		internal static string ELinq_UnresolvableFunctionForMethodNotFound(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMethodNotFound", new object[] { p0, p1 });
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00014B06 File Offset: 0x00012D06
		internal static string ELinq_UnresolvableFunctionForMember(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMember", new object[] { p0, p1 });
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00014B20 File Offset: 0x00012D20
		internal static string ELinq_UnresolvableStoreFunctionForMember(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableStoreFunctionForMember", new object[] { p0, p1 });
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00014B3A File Offset: 0x00012D3A
		internal static string ELinq_UnresolvableFunctionForExpression(object p0)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForExpression", new object[] { p0 });
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00014B50 File Offset: 0x00012D50
		internal static string ELinq_UnresolvableStoreFunctionForExpression(object p0)
		{
			return EntityRes.GetString("ELinq_UnresolvableStoreFunctionForExpression", new object[] { p0 });
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00014B66 File Offset: 0x00012D66
		internal static string ELinq_UnsupportedType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedType", new object[] { p0 });
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00014B7C File Offset: 0x00012D7C
		internal static string ELinq_UnsupportedNullConstant(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedNullConstant", new object[] { p0 });
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00014B92 File Offset: 0x00012D92
		internal static string ELinq_UnsupportedConstant(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedConstant", new object[] { p0 });
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00014BA8 File Offset: 0x00012DA8
		internal static string ELinq_UnsupportedCast(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedCast", new object[] { p0, p1 });
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00014BC2 File Offset: 0x00012DC2
		internal static string ELinq_UnsupportedIsOrAs(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ELinq_UnsupportedIsOrAs", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00014BE0 File Offset: 0x00012DE0
		internal static string ELinq_UnsupportedQueryableMethod
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedQueryableMethod");
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00014BEC File Offset: 0x00012DEC
		internal static string ELinq_InvalidOfTypeResult(object p0)
		{
			return EntityRes.GetString("ELinq_InvalidOfTypeResult", new object[] { p0 });
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00014C02 File Offset: 0x00012E02
		internal static string ELinq_UnsupportedNominalType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedNominalType", new object[] { p0 });
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00014C18 File Offset: 0x00012E18
		internal static string ELinq_UnsupportedEnumerableType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedEnumerableType", new object[] { p0 });
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00014C2E File Offset: 0x00012E2E
		internal static string ELinq_UnsupportedHeterogeneousInitializers(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedHeterogeneousInitializers", new object[] { p0 });
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00014C44 File Offset: 0x00012E44
		internal static string ELinq_UnsupportedDifferentContexts
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedDifferentContexts");
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00014C50 File Offset: 0x00012E50
		internal static string ELinq_UnsupportedCastToDecimal
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedCastToDecimal");
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00014C5C File Offset: 0x00012E5C
		internal static string ELinq_UnsupportedKeySelector(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedKeySelector", new object[] { p0 });
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00014C72 File Offset: 0x00012E72
		internal static string ELinq_CreateOrderedEnumerableNotSupported
		{
			get
			{
				return EntityRes.GetString("ELinq_CreateOrderedEnumerableNotSupported");
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00014C7E File Offset: 0x00012E7E
		internal static string ELinq_UnsupportedPassthrough(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedPassthrough", new object[] { p0, p1 });
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00014C98 File Offset: 0x00012E98
		internal static string ELinq_UnexpectedTypeForNavigationProperty(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ELinq_UnexpectedTypeForNavigationProperty", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00014CBA File Offset: 0x00012EBA
		internal static string ELinq_SkipWithoutOrder
		{
			get
			{
				return EntityRes.GetString("ELinq_SkipWithoutOrder");
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00014CC6 File Offset: 0x00012EC6
		internal static string ELinq_PropertyIndexNotSupported
		{
			get
			{
				return EntityRes.GetString("ELinq_PropertyIndexNotSupported");
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00014CD2 File Offset: 0x00012ED2
		internal static string ELinq_NotPropertyOrField(object p0)
		{
			return EntityRes.GetString("ELinq_NotPropertyOrField", new object[] { p0 });
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00014CE8 File Offset: 0x00012EE8
		internal static string ELinq_UnsupportedStringRemoveCase(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedStringRemoveCase", new object[] { p0, p1 });
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00014D02 File Offset: 0x00012F02
		internal static string ELinq_UnsupportedTrimStartTrimEndCase(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedTrimStartTrimEndCase", new object[] { p0 });
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00014D18 File Offset: 0x00012F18
		internal static string ELinq_UnsupportedVBDatePartNonConstantInterval(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedVBDatePartNonConstantInterval", new object[] { p0, p1 });
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00014D32 File Offset: 0x00012F32
		internal static string ELinq_UnsupportedVBDatePartInvalidInterval(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ELinq_UnsupportedVBDatePartInvalidInterval", new object[] { p0, p1, p2 });
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00014D50 File Offset: 0x00012F50
		internal static string ELinq_UnsupportedAsUnicodeAndAsNonUnicode(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedAsUnicodeAndAsNonUnicode", new object[] { p0 });
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00014D66 File Offset: 0x00012F66
		internal static string ELinq_UnsupportedComparison(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedComparison", new object[] { p0 });
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00014D7C File Offset: 0x00012F7C
		internal static string ELinq_UnsupportedRefComparison(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedRefComparison", new object[] { p0, p1 });
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00014D96 File Offset: 0x00012F96
		internal static string ELinq_UnsupportedRowComparison(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedRowComparison", new object[] { p0 });
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00014DAC File Offset: 0x00012FAC
		internal static string ELinq_UnsupportedRowMemberComparison(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedRowMemberComparison", new object[] { p0 });
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00014DC2 File Offset: 0x00012FC2
		internal static string ELinq_UnsupportedRowTypeComparison(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedRowTypeComparison", new object[] { p0 });
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x00014DD8 File Offset: 0x00012FD8
		internal static string ELinq_AnonymousType
		{
			get
			{
				return EntityRes.GetString("ELinq_AnonymousType");
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00014DE4 File Offset: 0x00012FE4
		internal static string ELinq_ClosureType
		{
			get
			{
				return EntityRes.GetString("ELinq_ClosureType");
			}
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00014DF0 File Offset: 0x00012FF0
		internal static string ELinq_UnhandledExpressionType(object p0)
		{
			return EntityRes.GetString("ELinq_UnhandledExpressionType", new object[] { p0 });
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00014E06 File Offset: 0x00013006
		internal static string ELinq_UnhandledBindingType(object p0)
		{
			return EntityRes.GetString("ELinq_UnhandledBindingType", new object[] { p0 });
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00014E1C File Offset: 0x0001301C
		internal static string ELinq_UnsupportedNestedFirst
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedNestedFirst");
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00014E28 File Offset: 0x00013028
		internal static string ELinq_UnsupportedNestedSingle
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedNestedSingle");
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00014E34 File Offset: 0x00013034
		internal static string ELinq_UnsupportedInclude
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedInclude");
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00014E40 File Offset: 0x00013040
		internal static string ELinq_UnsupportedMergeAs
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedMergeAs");
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00014E4C File Offset: 0x0001304C
		internal static string ELinq_MethodNotDirectlyCallable
		{
			get
			{
				return EntityRes.GetString("ELinq_MethodNotDirectlyCallable");
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00014E58 File Offset: 0x00013058
		internal static string ELinq_CycleDetected
		{
			get
			{
				return EntityRes.GetString("ELinq_CycleDetected");
			}
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00014E64 File Offset: 0x00013064
		internal static string ELinq_DbFunctionAttributedFunctionWithWrongReturnType(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_DbFunctionAttributedFunctionWithWrongReturnType", new object[] { p0, p1 });
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00014E7E File Offset: 0x0001307E
		internal static string ELinq_DbFunctionDirectCall
		{
			get
			{
				return EntityRes.GetString("ELinq_DbFunctionDirectCall");
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00014E8A File Offset: 0x0001308A
		internal static string ELinq_HasFlagArgumentAndSourceTypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_HasFlagArgumentAndSourceTypeMismatch", new object[] { p0, p1 });
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00014EA4 File Offset: 0x000130A4
		internal static string Elinq_ToStringNotSupportedForType(object p0)
		{
			return EntityRes.GetString("Elinq_ToStringNotSupportedForType", new object[] { p0 });
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00014EBA File Offset: 0x000130BA
		internal static string Elinq_ToStringNotSupportedForEnumsWithFlags
		{
			get
			{
				return EntityRes.GetString("Elinq_ToStringNotSupportedForEnumsWithFlags");
			}
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00014EC6 File Offset: 0x000130C6
		internal static string CompiledELinq_UnsupportedParameterTypes(object p0)
		{
			return EntityRes.GetString("CompiledELinq_UnsupportedParameterTypes", new object[] { p0 });
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00014EDC File Offset: 0x000130DC
		internal static string CompiledELinq_UnsupportedNamedParameterType(object p0, object p1)
		{
			return EntityRes.GetString("CompiledELinq_UnsupportedNamedParameterType", new object[] { p0, p1 });
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00014EF6 File Offset: 0x000130F6
		internal static string CompiledELinq_UnsupportedNamedParameterUseAsType(object p0, object p1)
		{
			return EntityRes.GetString("CompiledELinq_UnsupportedNamedParameterUseAsType", new object[] { p0, p1 });
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00014F10 File Offset: 0x00013110
		internal static string Update_UnsupportedExpressionKind(object p0, object p1)
		{
			return EntityRes.GetString("Update_UnsupportedExpressionKind", new object[] { p0, p1 });
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00014F2A File Offset: 0x0001312A
		internal static string Update_UnsupportedCastArgument(object p0)
		{
			return EntityRes.GetString("Update_UnsupportedCastArgument", new object[] { p0 });
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00014F40 File Offset: 0x00013140
		internal static string Update_UnsupportedExtentType(object p0, object p1)
		{
			return EntityRes.GetString("Update_UnsupportedExtentType", new object[] { p0, p1 });
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00014F5A File Offset: 0x0001315A
		internal static string Update_ConstraintCycle
		{
			get
			{
				return EntityRes.GetString("Update_ConstraintCycle");
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00014F66 File Offset: 0x00013166
		internal static string Update_UnsupportedJoinType(object p0)
		{
			return EntityRes.GetString("Update_UnsupportedJoinType", new object[] { p0 });
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00014F7C File Offset: 0x0001317C
		internal static string Update_UnsupportedProjection(object p0)
		{
			return EntityRes.GetString("Update_UnsupportedProjection", new object[] { p0 });
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00014F92 File Offset: 0x00013192
		internal static string Update_ConcurrencyError(object p0)
		{
			return EntityRes.GetString("Update_ConcurrencyError", new object[] { p0 });
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00014FA8 File Offset: 0x000131A8
		internal static string Update_MissingEntity(object p0, object p1)
		{
			return EntityRes.GetString("Update_MissingEntity", new object[] { p0, p1 });
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00014FC2 File Offset: 0x000131C2
		internal static string Update_RelationshipCardinalityConstraintViolation(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Update_RelationshipCardinalityConstraintViolation", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00014FEE File Offset: 0x000131EE
		internal static string Update_GeneralExecutionException
		{
			get
			{
				return EntityRes.GetString("Update_GeneralExecutionException");
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00014FFA File Offset: 0x000131FA
		internal static string Update_MissingRequiredEntity(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Update_MissingRequiredEntity", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00015018 File Offset: 0x00013218
		internal static string Update_RelationshipCardinalityViolation(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Update_RelationshipCardinalityViolation", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00015044 File Offset: 0x00013244
		internal static string Update_NotSupportedComputedKeyColumn(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("Update_NotSupportedComputedKeyColumn", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0001506B File Offset: 0x0001326B
		internal static string Update_AmbiguousServerGenIdentifier
		{
			get
			{
				return EntityRes.GetString("Update_AmbiguousServerGenIdentifier");
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00015077 File Offset: 0x00013277
		internal static string Update_WorkspaceMismatch
		{
			get
			{
				return EntityRes.GetString("Update_WorkspaceMismatch");
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00015083 File Offset: 0x00013283
		internal static string Update_MissingRequiredRelationshipValue(object p0, object p1)
		{
			return EntityRes.GetString("Update_MissingRequiredRelationshipValue", new object[] { p0, p1 });
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001509D File Offset: 0x0001329D
		internal static string Update_MissingResultColumn(object p0)
		{
			return EntityRes.GetString("Update_MissingResultColumn", new object[] { p0 });
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000150B3 File Offset: 0x000132B3
		internal static string Update_NullReturnValueForNonNullableMember(object p0, object p1)
		{
			return EntityRes.GetString("Update_NullReturnValueForNonNullableMember", new object[] { p0, p1 });
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000150CD File Offset: 0x000132CD
		internal static string Update_ReturnValueHasUnexpectedType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Update_ReturnValueHasUnexpectedType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000150EF File Offset: 0x000132EF
		internal static string Update_UnableToConvertRowsAffectedParameter(object p0, object p1)
		{
			return EntityRes.GetString("Update_UnableToConvertRowsAffectedParameter", new object[] { p0, p1 });
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00015109 File Offset: 0x00013309
		internal static string Update_MappingNotFound(object p0)
		{
			return EntityRes.GetString("Update_MappingNotFound", new object[] { p0 });
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001511F File Offset: 0x0001331F
		internal static string Update_ModifyingIdentityColumn(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Update_ModifyingIdentityColumn", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001513D File Offset: 0x0001333D
		internal static string Update_GeneratedDependent(object p0)
		{
			return EntityRes.GetString("Update_GeneratedDependent", new object[] { p0 });
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00015153 File Offset: 0x00013353
		internal static string Update_ReferentialConstraintIntegrityViolation
		{
			get
			{
				return EntityRes.GetString("Update_ReferentialConstraintIntegrityViolation");
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0001515F File Offset: 0x0001335F
		internal static string Update_ErrorLoadingRecord
		{
			get
			{
				return EntityRes.GetString("Update_ErrorLoadingRecord");
			}
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001516B File Offset: 0x0001336B
		internal static string Update_NullValue(object p0)
		{
			return EntityRes.GetString("Update_NullValue", new object[] { p0 });
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00015181 File Offset: 0x00013381
		internal static string Update_CircularRelationships
		{
			get
			{
				return EntityRes.GetString("Update_CircularRelationships");
			}
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001518D File Offset: 0x0001338D
		internal static string Update_RelationshipCardinalityConstraintViolationSingleValue(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("Update_RelationshipCardinalityConstraintViolationSingleValue", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x000151B4 File Offset: 0x000133B4
		internal static string Update_MissingFunctionMapping(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Update_MissingFunctionMapping", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x000151D2 File Offset: 0x000133D2
		internal static string Update_InvalidChanges
		{
			get
			{
				return EntityRes.GetString("Update_InvalidChanges");
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x000151DE File Offset: 0x000133DE
		internal static string Update_DuplicateKeys
		{
			get
			{
				return EntityRes.GetString("Update_DuplicateKeys");
			}
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000151EA File Offset: 0x000133EA
		internal static string Update_AmbiguousForeignKey(object p0)
		{
			return EntityRes.GetString("Update_AmbiguousForeignKey", new object[] { p0 });
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00015200 File Offset: 0x00013400
		internal static string Update_InsertingOrUpdatingReferenceToDeletedEntity(object p0)
		{
			return EntityRes.GetString("Update_InsertingOrUpdatingReferenceToDeletedEntity", new object[] { p0 });
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00015216 File Offset: 0x00013416
		internal static string ViewGen_Extent
		{
			get
			{
				return EntityRes.GetString("ViewGen_Extent");
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00015222 File Offset: 0x00013422
		internal static string ViewGen_Null
		{
			get
			{
				return EntityRes.GetString("ViewGen_Null");
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001522E File Offset: 0x0001342E
		internal static string ViewGen_CommaBlank
		{
			get
			{
				return EntityRes.GetString("ViewGen_CommaBlank");
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0001523A File Offset: 0x0001343A
		internal static string ViewGen_Entities
		{
			get
			{
				return EntityRes.GetString("ViewGen_Entities");
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00015246 File Offset: 0x00013446
		internal static string ViewGen_Tuples
		{
			get
			{
				return EntityRes.GetString("ViewGen_Tuples");
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00015252 File Offset: 0x00013452
		internal static string ViewGen_NotNull
		{
			get
			{
				return EntityRes.GetString("ViewGen_NotNull");
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001525E File Offset: 0x0001345E
		internal static string ViewGen_NegatedCellConstant(object p0)
		{
			return EntityRes.GetString("ViewGen_NegatedCellConstant", new object[] { p0 });
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00015274 File Offset: 0x00013474
		internal static string ViewGen_Error
		{
			get
			{
				return EntityRes.GetString("ViewGen_Error");
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00015280 File Offset: 0x00013480
		internal static string Viewgen_CannotGenerateQueryViewUnderNoValidation(object p0)
		{
			return EntityRes.GetString("Viewgen_CannotGenerateQueryViewUnderNoValidation", new object[] { p0 });
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00015296 File Offset: 0x00013496
		internal static string ViewGen_Missing_Sets_Mapping(object p0)
		{
			return EntityRes.GetString("ViewGen_Missing_Sets_Mapping", new object[] { p0 });
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000152AC File Offset: 0x000134AC
		internal static string ViewGen_Missing_Type_Mapping(object p0)
		{
			return EntityRes.GetString("ViewGen_Missing_Type_Mapping", new object[] { p0 });
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000152C2 File Offset: 0x000134C2
		internal static string ViewGen_Missing_Set_Mapping(object p0)
		{
			return EntityRes.GetString("ViewGen_Missing_Set_Mapping", new object[] { p0 });
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000152D8 File Offset: 0x000134D8
		internal static string ViewGen_Concurrency_Derived_Class(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Concurrency_Derived_Class", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000152F6 File Offset: 0x000134F6
		internal static string ViewGen_Concurrency_Invalid_Condition(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Concurrency_Invalid_Condition", new object[] { p0, p1 });
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00015310 File Offset: 0x00013510
		internal static string ViewGen_TableKey_Missing(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_TableKey_Missing", new object[] { p0, p1 });
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001532A File Offset: 0x0001352A
		internal static string ViewGen_EntitySetKey_Missing(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_EntitySetKey_Missing", new object[] { p0, p1 });
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00015344 File Offset: 0x00013544
		internal static string ViewGen_AssociationSetKey_Missing(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_AssociationSetKey_Missing", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00015362 File Offset: 0x00013562
		internal static string ViewGen_Cannot_Recover_Attributes(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Cannot_Recover_Attributes", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00015380 File Offset: 0x00013580
		internal static string ViewGen_Cannot_Recover_Types(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Cannot_Recover_Types", new object[] { p0, p1 });
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001539A File Offset: 0x0001359A
		internal static string ViewGen_Cannot_Disambiguate_MultiConstant(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Cannot_Disambiguate_MultiConstant", new object[] { p0, p1 });
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000153B4 File Offset: 0x000135B4
		internal static string ViewGen_No_Default_Value(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_No_Default_Value", new object[] { p0, p1 });
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x000153CE File Offset: 0x000135CE
		internal static string ViewGen_No_Default_Value_For_Configuration(object p0)
		{
			return EntityRes.GetString("ViewGen_No_Default_Value_For_Configuration", new object[] { p0 });
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000153E4 File Offset: 0x000135E4
		internal static string ViewGen_KeyConstraint_Violation(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("ViewGen_KeyConstraint_Violation", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00015410 File Offset: 0x00013610
		internal static string ViewGen_KeyConstraint_Update_Violation_EntitySet(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ViewGen_KeyConstraint_Update_Violation_EntitySet", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00015432 File Offset: 0x00013632
		internal static string ViewGen_KeyConstraint_Update_Violation_AssociationSet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_KeyConstraint_Update_Violation_AssociationSet", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00015450 File Offset: 0x00013650
		internal static string ViewGen_AssociationEndShouldBeMappedToKey(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_AssociationEndShouldBeMappedToKey", new object[] { p0, p1 });
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001546A File Offset: 0x0001366A
		internal static string ViewGen_Duplicate_CProperties(object p0)
		{
			return EntityRes.GetString("ViewGen_Duplicate_CProperties", new object[] { p0 });
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00015480 File Offset: 0x00013680
		internal static string ViewGen_Duplicate_CProperties_IsMapped(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Duplicate_CProperties_IsMapped", new object[] { p0, p1 });
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001549A File Offset: 0x0001369A
		internal static string ViewGen_NotNull_No_Projected_Slot(object p0)
		{
			return EntityRes.GetString("ViewGen_NotNull_No_Projected_Slot", new object[] { p0 });
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000154B0 File Offset: 0x000136B0
		internal static string ViewGen_InvalidCondition(object p0)
		{
			return EntityRes.GetString("ViewGen_InvalidCondition", new object[] { p0 });
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000154C6 File Offset: 0x000136C6
		internal static string ViewGen_NonKeyProjectedWithOverlappingPartitions(object p0)
		{
			return EntityRes.GetString("ViewGen_NonKeyProjectedWithOverlappingPartitions", new object[] { p0 });
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000154DC File Offset: 0x000136DC
		internal static string ViewGen_CQ_PartitionConstraint(object p0)
		{
			return EntityRes.GetString("ViewGen_CQ_PartitionConstraint", new object[] { p0 });
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000154F2 File Offset: 0x000136F2
		internal static string ViewGen_CQ_DomainConstraint(object p0)
		{
			return EntityRes.GetString("ViewGen_CQ_DomainConstraint", new object[] { p0 });
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00015508 File Offset: 0x00013708
		internal static string ViewGen_ErrorLog(object p0)
		{
			return EntityRes.GetString("ViewGen_ErrorLog", new object[] { p0 });
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001551E File Offset: 0x0001371E
		internal static string ViewGen_ErrorLog2(object p0)
		{
			return EntityRes.GetString("ViewGen_ErrorLog2", new object[] { p0 });
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00015534 File Offset: 0x00013734
		internal static string ViewGen_Foreign_Key_Missing_Table_Mapping(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_Missing_Table_Mapping", new object[] { p0, p1 });
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001554E File Offset: 0x0001374E
		internal static string ViewGen_Foreign_Key_ParentTable_NotMappedToEnd(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_ParentTable_NotMappedToEnd", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001557A File Offset: 0x0001377A
		internal static string ViewGen_Foreign_Key(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000155A1 File Offset: 0x000137A1
		internal static string ViewGen_Foreign_Key_UpperBound_MustBeOne(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_UpperBound_MustBeOne", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000155BF File Offset: 0x000137BF
		internal static string ViewGen_Foreign_Key_LowerBound_MustBeOne(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_LowerBound_MustBeOne", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000155DD File Offset: 0x000137DD
		internal static string ViewGen_Foreign_Key_Missing_Relationship_Mapping(object p0)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_Missing_Relationship_Mapping", new object[] { p0 });
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000155F3 File Offset: 0x000137F3
		internal static string ViewGen_Foreign_Key_Not_Guaranteed_InCSpace(object p0)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_Not_Guaranteed_InCSpace", new object[] { p0 });
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00015609 File Offset: 0x00013809
		internal static string ViewGen_Foreign_Key_ColumnOrder_Incorrect(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_ColumnOrder_Incorrect", new object[] { p0, p1, p2, p3, p4, p5, p6, p7, p8 });
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00015645 File Offset: 0x00013845
		internal static string ViewGen_AssociationSet_AsUserString(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_AssociationSet_AsUserString", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00015663 File Offset: 0x00013863
		internal static string ViewGen_AssociationSet_AsUserString_Negated(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_AssociationSet_AsUserString_Negated", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00015681 File Offset: 0x00013881
		internal static string ViewGen_EntitySet_AsUserString(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_EntitySet_AsUserString", new object[] { p0, p1 });
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001569B File Offset: 0x0001389B
		internal static string ViewGen_EntitySet_AsUserString_Negated(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_EntitySet_AsUserString_Negated", new object[] { p0, p1 });
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x000156B5 File Offset: 0x000138B5
		internal static string ViewGen_EntityInstanceToken
		{
			get
			{
				return EntityRes.GetString("ViewGen_EntityInstanceToken");
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000156C1 File Offset: 0x000138C1
		internal static string Viewgen_ConfigurationErrorMsg(object p0)
		{
			return EntityRes.GetString("Viewgen_ConfigurationErrorMsg", new object[] { p0 });
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000156D7 File Offset: 0x000138D7
		internal static string ViewGen_HashOnMappingClosure_Not_Matching(object p0)
		{
			return EntityRes.GetString("ViewGen_HashOnMappingClosure_Not_Matching", new object[] { p0 });
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000156ED File Offset: 0x000138ED
		internal static string Viewgen_RightSideNotDisjoint(object p0)
		{
			return EntityRes.GetString("Viewgen_RightSideNotDisjoint", new object[] { p0 });
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00015703 File Offset: 0x00013903
		internal static string Viewgen_QV_RewritingNotFound(object p0)
		{
			return EntityRes.GetString("Viewgen_QV_RewritingNotFound", new object[] { p0 });
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00015719 File Offset: 0x00013919
		internal static string Viewgen_NullableMappingForNonNullableColumn(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_NullableMappingForNonNullableColumn", new object[] { p0, p1 });
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00015733 File Offset: 0x00013933
		internal static string Viewgen_ErrorPattern_ConditionMemberIsMapped(object p0)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_ConditionMemberIsMapped", new object[] { p0 });
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00015749 File Offset: 0x00013949
		internal static string Viewgen_ErrorPattern_DuplicateConditionValue(object p0)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_DuplicateConditionValue", new object[] { p0 });
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001575F File Offset: 0x0001395F
		internal static string Viewgen_ErrorPattern_TableMappedToMultipleES(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_TableMappedToMultipleES", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0001577D File Offset: 0x0001397D
		internal static string Viewgen_ErrorPattern_Partition_Disj_Eq
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Eq");
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00015789 File Offset: 0x00013989
		internal static string Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember", new object[] { p0, p1 });
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000157A3 File Offset: 0x000139A3
		internal static string Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition", new object[] { p0, p1 });
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x000157BD File Offset: 0x000139BD
		internal static string Viewgen_ErrorPattern_Partition_Disj_Subs_Ref
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Subs_Ref");
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x000157C9 File Offset: 0x000139C9
		internal static string Viewgen_ErrorPattern_Partition_Disj_Subs
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Subs");
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x000157D5 File Offset: 0x000139D5
		internal static string Viewgen_ErrorPattern_Partition_Disj_Unk
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Unk");
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x000157E1 File Offset: 0x000139E1
		internal static string Viewgen_ErrorPattern_Partition_Eq_Disj
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Disj");
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x000157ED File Offset: 0x000139ED
		internal static string Viewgen_ErrorPattern_Partition_Eq_Subs_Ref
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Subs_Ref");
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x000157F9 File Offset: 0x000139F9
		internal static string Viewgen_ErrorPattern_Partition_Eq_Subs
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Subs");
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00015805 File Offset: 0x00013A05
		internal static string Viewgen_ErrorPattern_Partition_Eq_Unk
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Unk");
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00015811 File Offset: 0x00013A11
		internal static string Viewgen_ErrorPattern_Partition_Eq_Unk_Association
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Unk_Association");
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0001581D File Offset: 0x00013A1D
		internal static string Viewgen_ErrorPattern_Partition_Sub_Disj
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Disj");
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00015829 File Offset: 0x00013A29
		internal static string Viewgen_ErrorPattern_Partition_Sub_Eq
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Eq");
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x00015835 File Offset: 0x00013A35
		internal static string Viewgen_ErrorPattern_Partition_Sub_Eq_Ref
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Eq_Ref");
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x00015841 File Offset: 0x00013A41
		internal static string Viewgen_ErrorPattern_Partition_Sub_Unk
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Unk");
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0001584D File Offset: 0x00013A4D
		internal static string Viewgen_NoJoinKeyOrFK
		{
			get
			{
				return EntityRes.GetString("Viewgen_NoJoinKeyOrFK");
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00015859 File Offset: 0x00013A59
		internal static string Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct", new object[] { p0, p1 });
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x00015873 File Offset: 0x00013A73
		internal static string Validator_EmptyIdentity
		{
			get
			{
				return EntityRes.GetString("Validator_EmptyIdentity");
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0001587F File Offset: 0x00013A7F
		internal static string Validator_CollectionHasNoTypeUsage
		{
			get
			{
				return EntityRes.GetString("Validator_CollectionHasNoTypeUsage");
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001588B File Offset: 0x00013A8B
		internal static string Validator_NoKeyMembers(object p0)
		{
			return EntityRes.GetString("Validator_NoKeyMembers", new object[] { p0 });
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x000158A1 File Offset: 0x00013AA1
		internal static string Validator_FacetTypeIsNull
		{
			get
			{
				return EntityRes.GetString("Validator_FacetTypeIsNull");
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x000158AD File Offset: 0x00013AAD
		internal static string Validator_MemberHasNullDeclaringType
		{
			get
			{
				return EntityRes.GetString("Validator_MemberHasNullDeclaringType");
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x000158B9 File Offset: 0x00013AB9
		internal static string Validator_MemberHasNullTypeUsage
		{
			get
			{
				return EntityRes.GetString("Validator_MemberHasNullTypeUsage");
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x000158C5 File Offset: 0x00013AC5
		internal static string Validator_ItemAttributeHasNullTypeUsage
		{
			get
			{
				return EntityRes.GetString("Validator_ItemAttributeHasNullTypeUsage");
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x000158D1 File Offset: 0x00013AD1
		internal static string Validator_RefTypeHasNullEntityType
		{
			get
			{
				return EntityRes.GetString("Validator_RefTypeHasNullEntityType");
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x000158DD File Offset: 0x00013ADD
		internal static string Validator_TypeUsageHasNullEdmType
		{
			get
			{
				return EntityRes.GetString("Validator_TypeUsageHasNullEdmType");
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x000158E9 File Offset: 0x00013AE9
		internal static string Validator_BaseTypeHasMemberOfSameName
		{
			get
			{
				return EntityRes.GetString("Validator_BaseTypeHasMemberOfSameName");
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x000158F5 File Offset: 0x00013AF5
		internal static string Validator_CollectionTypesCannotHaveBaseType
		{
			get
			{
				return EntityRes.GetString("Validator_CollectionTypesCannotHaveBaseType");
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00015901 File Offset: 0x00013B01
		internal static string Validator_RefTypesCannotHaveBaseType
		{
			get
			{
				return EntityRes.GetString("Validator_RefTypesCannotHaveBaseType");
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0001590D File Offset: 0x00013B0D
		internal static string Validator_TypeHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_TypeHasNoName");
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00015919 File Offset: 0x00013B19
		internal static string Validator_TypeHasNoNamespace
		{
			get
			{
				return EntityRes.GetString("Validator_TypeHasNoNamespace");
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00015925 File Offset: 0x00013B25
		internal static string Validator_FacetHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_FacetHasNoName");
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x00015931 File Offset: 0x00013B31
		internal static string Validator_MemberHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_MemberHasNoName");
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0001593D File Offset: 0x00013B3D
		internal static string Validator_MetadataPropertyHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_MetadataPropertyHasNoName");
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00015949 File Offset: 0x00013B49
		internal static string Validator_NullableEntityKeyProperty(object p0, object p1)
		{
			return EntityRes.GetString("Validator_NullableEntityKeyProperty", new object[] { p0, p1 });
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00015963 File Offset: 0x00013B63
		internal static string Validator_OSpace_InvalidNavPropReturnType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_InvalidNavPropReturnType", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00015981 File Offset: 0x00013B81
		internal static string Validator_OSpace_ScalarPropertyNotPrimitive(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_ScalarPropertyNotPrimitive", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001599F File Offset: 0x00013B9F
		internal static string Validator_OSpace_ComplexPropertyNotComplex(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_ComplexPropertyNotComplex", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x000159BD File Offset: 0x00013BBD
		internal static string Validator_OSpace_Convention_MultipleTypesWithSameName(object p0)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_MultipleTypesWithSameName", new object[] { p0 });
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000159D3 File Offset: 0x00013BD3
		internal static string Validator_OSpace_Convention_NonPrimitiveTypeProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_NonPrimitiveTypeProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000159F1 File Offset: 0x00013BF1
		internal static string Validator_OSpace_Convention_MissingRequiredProperty(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_MissingRequiredProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00015A0B File Offset: 0x00013C0B
		internal static string Validator_OSpace_Convention_BaseTypeIncompatible(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_BaseTypeIncompatible", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00015A29 File Offset: 0x00013C29
		internal static string Validator_OSpace_Convention_MissingOSpaceType(object p0)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_MissingOSpaceType", new object[] { p0 });
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00015A3F File Offset: 0x00013C3F
		internal static string Validator_OSpace_Convention_RelationshipNotLoaded(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_RelationshipNotLoaded", new object[] { p0, p1 });
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00015A59 File Offset: 0x00013C59
		internal static string Validator_OSpace_Convention_AttributeAssemblyReferenced(object p0)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_AttributeAssemblyReferenced", new object[] { p0 });
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00015A6F File Offset: 0x00013C6F
		internal static string Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00015A8D File Offset: 0x00013C8D
		internal static string Validator_OSpace_Convention_AmbiguousClrType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_AmbiguousClrType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00015AAB File Offset: 0x00013CAB
		internal static string Validator_OSpace_Convention_Struct(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_Struct", new object[] { p0, p1 });
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00015AC5 File Offset: 0x00013CC5
		internal static string Validator_OSpace_Convention_BaseTypeNotLoaded(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_BaseTypeNotLoaded", new object[] { p0, p1 });
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00015ADF File Offset: 0x00013CDF
		internal static string Validator_OSpace_Convention_SSpaceOSpaceTypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_SSpaceOSpaceTypeMismatch", new object[] { p0, p1 });
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00015AF9 File Offset: 0x00013CF9
		internal static string Validator_OSpace_Convention_NonMatchingUnderlyingTypes
		{
			get
			{
				return EntityRes.GetString("Validator_OSpace_Convention_NonMatchingUnderlyingTypes");
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00015B05 File Offset: 0x00013D05
		internal static string Validator_UnsupportedEnumUnderlyingType(object p0)
		{
			return EntityRes.GetString("Validator_UnsupportedEnumUnderlyingType", new object[] { p0 });
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00015B1B File Offset: 0x00013D1B
		internal static string ExtraInfo
		{
			get
			{
				return EntityRes.GetString("ExtraInfo");
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00015B27 File Offset: 0x00013D27
		internal static string Metadata_General_Error
		{
			get
			{
				return EntityRes.GetString("Metadata_General_Error");
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00015B33 File Offset: 0x00013D33
		internal static string InvalidNumberOfParametersForAggregateFunction(object p0)
		{
			return EntityRes.GetString("InvalidNumberOfParametersForAggregateFunction", new object[] { p0 });
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00015B49 File Offset: 0x00013D49
		internal static string InvalidParameterTypeForAggregateFunction(object p0, object p1)
		{
			return EntityRes.GetString("InvalidParameterTypeForAggregateFunction", new object[] { p0, p1 });
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00015B63 File Offset: 0x00013D63
		internal static string InvalidSchemaEncountered(object p0)
		{
			return EntityRes.GetString("InvalidSchemaEncountered", new object[] { p0 });
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00015B79 File Offset: 0x00013D79
		internal static string SystemNamespaceEncountered(object p0)
		{
			return EntityRes.GetString("SystemNamespaceEncountered", new object[] { p0 });
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00015B8F File Offset: 0x00013D8F
		internal static string NoCollectionForSpace(object p0)
		{
			return EntityRes.GetString("NoCollectionForSpace", new object[] { p0 });
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00015BA5 File Offset: 0x00013DA5
		internal static string OperationOnReadOnlyCollection
		{
			get
			{
				return EntityRes.GetString("OperationOnReadOnlyCollection");
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x00015BB1 File Offset: 0x00013DB1
		internal static string OperationOnReadOnlyItem
		{
			get
			{
				return EntityRes.GetString("OperationOnReadOnlyItem");
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00015BBD File Offset: 0x00013DBD
		internal static string EntitySetInAnotherContainer
		{
			get
			{
				return EntityRes.GetString("EntitySetInAnotherContainer");
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00015BC9 File Offset: 0x00013DC9
		internal static string InvalidKeyMember(object p0)
		{
			return EntityRes.GetString("InvalidKeyMember", new object[] { p0 });
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00015BDF File Offset: 0x00013DDF
		internal static string InvalidFileExtension(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidFileExtension", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00015BFD File Offset: 0x00013DFD
		internal static string NewTypeConflictsWithExistingType(object p0, object p1)
		{
			return EntityRes.GetString("NewTypeConflictsWithExistingType", new object[] { p0, p1 });
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00015C17 File Offset: 0x00013E17
		internal static string NotValidInputPath
		{
			get
			{
				return EntityRes.GetString("NotValidInputPath");
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00015C23 File Offset: 0x00013E23
		internal static string UnableToDetermineApplicationContext
		{
			get
			{
				return EntityRes.GetString("UnableToDetermineApplicationContext");
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00015C2F File Offset: 0x00013E2F
		internal static string WildcardEnumeratorReturnedNull
		{
			get
			{
				return EntityRes.GetString("WildcardEnumeratorReturnedNull");
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00015C3B File Offset: 0x00013E3B
		internal static string InvalidUseOfWebPath(object p0)
		{
			return EntityRes.GetString("InvalidUseOfWebPath", new object[] { p0 });
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00015C51 File Offset: 0x00013E51
		internal static string UnableToFindReflectedType(object p0, object p1)
		{
			return EntityRes.GetString("UnableToFindReflectedType", new object[] { p0, p1 });
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00015C6B File Offset: 0x00013E6B
		internal static string AssemblyMissingFromAssembliesToConsider(object p0)
		{
			return EntityRes.GetString("AssemblyMissingFromAssembliesToConsider", new object[] { p0 });
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00015C81 File Offset: 0x00013E81
		internal static string UnableToLoadResource
		{
			get
			{
				return EntityRes.GetString("UnableToLoadResource");
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00015C8D File Offset: 0x00013E8D
		internal static string EdmVersionNotSupportedByRuntime(object p0, object p1)
		{
			return EntityRes.GetString("EdmVersionNotSupportedByRuntime", new object[] { p0, p1 });
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00015CA7 File Offset: 0x00013EA7
		internal static string AtleastOneSSDLNeeded
		{
			get
			{
				return EntityRes.GetString("AtleastOneSSDLNeeded");
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00015CB3 File Offset: 0x00013EB3
		internal static string InvalidMetadataPath
		{
			get
			{
				return EntityRes.GetString("InvalidMetadataPath");
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00015CBF File Offset: 0x00013EBF
		internal static string UnableToResolveAssembly(object p0)
		{
			return EntityRes.GetString("UnableToResolveAssembly", new object[] { p0 });
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00015CD5 File Offset: 0x00013ED5
		internal static string DuplicatedFunctionoverloads(object p0, object p1)
		{
			return EntityRes.GetString("DuplicatedFunctionoverloads", new object[] { p0, p1 });
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00015CEF File Offset: 0x00013EEF
		internal static string EntitySetNotInCSPace(object p0)
		{
			return EntityRes.GetString("EntitySetNotInCSPace", new object[] { p0 });
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00015D05 File Offset: 0x00013F05
		internal static string TypeNotInEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeNotInEntitySet", new object[] { p0, p1, p2 });
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00015D23 File Offset: 0x00013F23
		internal static string TypeNotInAssociationSet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeNotInAssociationSet", new object[] { p0, p1, p2 });
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00015D41 File Offset: 0x00013F41
		internal static string DifferentSchemaVersionInCollection(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DifferentSchemaVersionInCollection", new object[] { p0, p1, p2 });
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00015D5F File Offset: 0x00013F5F
		internal static string InvalidCollectionForMapping(object p0)
		{
			return EntityRes.GetString("InvalidCollectionForMapping", new object[] { p0 });
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00015D75 File Offset: 0x00013F75
		internal static string OnlyStoreConnectionsSupported
		{
			get
			{
				return EntityRes.GetString("OnlyStoreConnectionsSupported");
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00015D81 File Offset: 0x00013F81
		internal static string StoreItemCollectionMustHaveOneArtifact(object p0)
		{
			return EntityRes.GetString("StoreItemCollectionMustHaveOneArtifact", new object[] { p0 });
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00015D97 File Offset: 0x00013F97
		internal static string CheckArgumentContainsNullFailed(object p0)
		{
			return EntityRes.GetString("CheckArgumentContainsNullFailed", new object[] { p0 });
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00015DAD File Offset: 0x00013FAD
		internal static string InvalidRelationshipSetName(object p0)
		{
			return EntityRes.GetString("InvalidRelationshipSetName", new object[] { p0 });
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00015DC3 File Offset: 0x00013FC3
		internal static string InvalidEntitySetName(object p0)
		{
			return EntityRes.GetString("InvalidEntitySetName", new object[] { p0 });
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00015DD9 File Offset: 0x00013FD9
		internal static string OnlyFunctionImportsCanBeAddedToEntityContainer(object p0)
		{
			return EntityRes.GetString("OnlyFunctionImportsCanBeAddedToEntityContainer", new object[] { p0 });
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00015DEF File Offset: 0x00013FEF
		internal static string ItemInvalidIdentity(object p0)
		{
			return EntityRes.GetString("ItemInvalidIdentity", new object[] { p0 });
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00015E05 File Offset: 0x00014005
		internal static string ItemDuplicateIdentity(object p0)
		{
			return EntityRes.GetString("ItemDuplicateIdentity", new object[] { p0 });
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00015E1B File Offset: 0x0001401B
		internal static string NotStringTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotStringTypeForTypeUsage");
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00015E27 File Offset: 0x00014027
		internal static string NotBinaryTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotBinaryTypeForTypeUsage");
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00015E33 File Offset: 0x00014033
		internal static string NotDateTimeTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotDateTimeTypeForTypeUsage");
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00015E3F File Offset: 0x0001403F
		internal static string NotDateTimeOffsetTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotDateTimeOffsetTypeForTypeUsage");
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00015E4B File Offset: 0x0001404B
		internal static string NotTimeTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotTimeTypeForTypeUsage");
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00015E57 File Offset: 0x00014057
		internal static string NotDecimalTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotDecimalTypeForTypeUsage");
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00015E63 File Offset: 0x00014063
		internal static string ArrayTooSmall
		{
			get
			{
				return EntityRes.GetString("ArrayTooSmall");
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00015E6F File Offset: 0x0001406F
		internal static string MoreThanOneItemMatchesIdentity(object p0)
		{
			return EntityRes.GetString("MoreThanOneItemMatchesIdentity", new object[] { p0 });
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00015E85 File Offset: 0x00014085
		internal static string MissingDefaultValueForConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("MissingDefaultValueForConstantFacet", new object[] { p0, p1 });
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00015E9F File Offset: 0x0001409F
		internal static string MinAndMaxValueMustBeSameForConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("MinAndMaxValueMustBeSameForConstantFacet", new object[] { p0, p1 });
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00015EB9 File Offset: 0x000140B9
		internal static string BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet", new object[] { p0, p1 });
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00015ED3 File Offset: 0x000140D3
		internal static string MinAndMaxValueMustBeDifferentForNonConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("MinAndMaxValueMustBeDifferentForNonConstantFacet", new object[] { p0, p1 });
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00015EED File Offset: 0x000140ED
		internal static string MinAndMaxMustBePositive(object p0, object p1)
		{
			return EntityRes.GetString("MinAndMaxMustBePositive", new object[] { p0, p1 });
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00015F07 File Offset: 0x00014107
		internal static string MinMustBeLessThanMax(object p0, object p1, object p2)
		{
			return EntityRes.GetString("MinMustBeLessThanMax", new object[] { p0, p1, p2 });
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00015F25 File Offset: 0x00014125
		internal static string SameRoleNameOnRelationshipAttribute(object p0, object p1)
		{
			return EntityRes.GetString("SameRoleNameOnRelationshipAttribute", new object[] { p0, p1 });
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00015F3F File Offset: 0x0001413F
		internal static string RoleTypeInEdmRelationshipAttributeIsInvalidType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("RoleTypeInEdmRelationshipAttributeIsInvalidType", new object[] { p0, p1, p2 });
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00015F5D File Offset: 0x0001415D
		internal static string TargetRoleNameInNavigationPropertyNotValid(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("TargetRoleNameInNavigationPropertyNotValid", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00015F7F File Offset: 0x0001417F
		internal static string RelationshipNameInNavigationPropertyNotValid(object p0, object p1, object p2)
		{
			return EntityRes.GetString("RelationshipNameInNavigationPropertyNotValid", new object[] { p0, p1, p2 });
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00015F9D File Offset: 0x0001419D
		internal static string NestedClassNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("NestedClassNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00015FB7 File Offset: 0x000141B7
		internal static string NullParameterForEdmRelationshipAttribute(object p0, object p1)
		{
			return EntityRes.GetString("NullParameterForEdmRelationshipAttribute", new object[] { p0, p1 });
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00015FD1 File Offset: 0x000141D1
		internal static string NullRelationshipNameforEdmRelationshipAttribute(object p0)
		{
			return EntityRes.GetString("NullRelationshipNameforEdmRelationshipAttribute", new object[] { p0 });
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00015FE7 File Offset: 0x000141E7
		internal static string NavigationPropertyRelationshipEndTypeMismatch(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("NavigationPropertyRelationshipEndTypeMismatch", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001600E File Offset: 0x0001420E
		internal static string AllArtifactsMustTargetSameProvider_InvariantName(object p0, object p1)
		{
			return EntityRes.GetString("AllArtifactsMustTargetSameProvider_InvariantName", new object[] { p0, p1 });
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00016028 File Offset: 0x00014228
		internal static string AllArtifactsMustTargetSameProvider_ManifestToken(object p0, object p1)
		{
			return EntityRes.GetString("AllArtifactsMustTargetSameProvider_ManifestToken", new object[] { p0, p1 });
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00016042 File Offset: 0x00014242
		internal static string ProviderManifestTokenNotFound
		{
			get
			{
				return EntityRes.GetString("ProviderManifestTokenNotFound");
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0001604E File Offset: 0x0001424E
		internal static string FailedToRetrieveProviderManifest
		{
			get
			{
				return EntityRes.GetString("FailedToRetrieveProviderManifest");
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0001605A File Offset: 0x0001425A
		internal static string InvalidMaxLengthSize
		{
			get
			{
				return EntityRes.GetString("InvalidMaxLengthSize");
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00016066 File Offset: 0x00014266
		internal static string ArgumentMustBeCSpaceType
		{
			get
			{
				return EntityRes.GetString("ArgumentMustBeCSpaceType");
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00016072 File Offset: 0x00014272
		internal static string ArgumentMustBeOSpaceType
		{
			get
			{
				return EntityRes.GetString("ArgumentMustBeOSpaceType");
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001607E File Offset: 0x0001427E
		internal static string FailedToFindOSpaceTypeMapping(object p0)
		{
			return EntityRes.GetString("FailedToFindOSpaceTypeMapping", new object[] { p0 });
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00016094 File Offset: 0x00014294
		internal static string FailedToFindCSpaceTypeMapping(object p0)
		{
			return EntityRes.GetString("FailedToFindCSpaceTypeMapping", new object[] { p0 });
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x000160AA File Offset: 0x000142AA
		internal static string FailedToFindClrTypeMapping(object p0)
		{
			return EntityRes.GetString("FailedToFindClrTypeMapping", new object[] { p0 });
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x000160C0 File Offset: 0x000142C0
		internal static string GenericTypeNotSupported(object p0)
		{
			return EntityRes.GetString("GenericTypeNotSupported", new object[] { p0 });
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000160D6 File Offset: 0x000142D6
		internal static string InvalidEDMVersion(object p0)
		{
			return EntityRes.GetString("InvalidEDMVersion", new object[] { p0 });
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x000160EC File Offset: 0x000142EC
		internal static string Mapping_General_Error
		{
			get
			{
				return EntityRes.GetString("Mapping_General_Error");
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x000160F8 File Offset: 0x000142F8
		internal static string Mapping_InvalidContent_General
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_General");
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00016104 File Offset: 0x00014304
		internal static string Mapping_InvalidContent_EntityContainer(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_EntityContainer", new object[] { p0 });
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001611A File Offset: 0x0001431A
		internal static string Mapping_InvalidContent_StorageEntityContainer(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_StorageEntityContainer", new object[] { p0 });
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00016130 File Offset: 0x00014330
		internal static string Mapping_AlreadyMapped_StorageEntityContainer(object p0)
		{
			return EntityRes.GetString("Mapping_AlreadyMapped_StorageEntityContainer", new object[] { p0 });
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00016146 File Offset: 0x00014346
		internal static string Mapping_InvalidContent_Entity_Set(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Entity_Set", new object[] { p0 });
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001615C File Offset: 0x0001435C
		internal static string Mapping_InvalidContent_Entity_Type(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Entity_Type", new object[] { p0 });
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00016172 File Offset: 0x00014372
		internal static string Mapping_InvalidContent_AbstractEntity_FunctionMapping(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AbstractEntity_FunctionMapping", new object[] { p0 });
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00016188 File Offset: 0x00014388
		internal static string Mapping_InvalidContent_AbstractEntity_Type(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AbstractEntity_Type", new object[] { p0 });
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001619E File Offset: 0x0001439E
		internal static string Mapping_InvalidContent_AbstractEntity_IsOfType(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AbstractEntity_IsOfType", new object[] { p0 });
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x000161B4 File Offset: 0x000143B4
		internal static string Mapping_InvalidContent_Entity_Type_For_Entity_Set(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Entity_Type_For_Entity_Set", new object[] { p0, p1, p2 });
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000161D2 File Offset: 0x000143D2
		internal static string Mapping_Invalid_Association_Type_For_Association_Set(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Invalid_Association_Type_For_Association_Set", new object[] { p0, p1, p2 });
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000161F0 File Offset: 0x000143F0
		internal static string Mapping_InvalidContent_Table(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Table", new object[] { p0 });
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00016206 File Offset: 0x00014406
		internal static string Mapping_InvalidContent_Complex_Type(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Complex_Type", new object[] { p0 });
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001621C File Offset: 0x0001441C
		internal static string Mapping_InvalidContent_Association_Set(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Association_Set", new object[] { p0 });
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00016232 File Offset: 0x00014432
		internal static string Mapping_InvalidContent_AssociationSet_Condition(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AssociationSet_Condition", new object[] { p0 });
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00016248 File Offset: 0x00014448
		internal static string Mapping_InvalidContent_ForeignKey_Association_Set(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ForeignKey_Association_Set", new object[] { p0 });
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001625E File Offset: 0x0001445E
		internal static string Mapping_InvalidContent_ForeignKey_Association_Set_PKtoPK(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ForeignKey_Association_Set_PKtoPK", new object[] { p0 });
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00016274 File Offset: 0x00014474
		internal static string Mapping_InvalidContent_Association_Type(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Association_Type", new object[] { p0 });
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001628A File Offset: 0x0001448A
		internal static string Mapping_InvalidContent_EndProperty(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_EndProperty", new object[] { p0 });
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x000162A0 File Offset: 0x000144A0
		internal static string Mapping_InvalidContent_Association_Type_Empty
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_Association_Type_Empty");
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x000162AC File Offset: 0x000144AC
		internal static string Mapping_InvalidContent_Table_Expected
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_Table_Expected");
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x000162B8 File Offset: 0x000144B8
		internal static string Mapping_InvalidContent_Cdm_Member(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Cdm_Member", new object[] { p0 });
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000162CE File Offset: 0x000144CE
		internal static string Mapping_InvalidContent_Column(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Column", new object[] { p0 });
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x000162E4 File Offset: 0x000144E4
		internal static string Mapping_InvalidContent_End(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_End", new object[] { p0 });
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x000162FA File Offset: 0x000144FA
		internal static string Mapping_InvalidContent_Container_SubElement
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_Container_SubElement");
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00016306 File Offset: 0x00014506
		internal static string Mapping_InvalidContent_Duplicate_Cdm_Member(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Duplicate_Cdm_Member", new object[] { p0 });
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001631C File Offset: 0x0001451C
		internal static string Mapping_InvalidContent_Duplicate_Condition_Member(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Duplicate_Condition_Member", new object[] { p0 });
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x00016332 File Offset: 0x00014532
		internal static string Mapping_InvalidContent_ConditionMapping_Both_Members
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Both_Members");
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0001633E File Offset: 0x0001453E
		internal static string Mapping_InvalidContent_ConditionMapping_Either_Members
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Either_Members");
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0001634A File Offset: 0x0001454A
		internal static string Mapping_InvalidContent_ConditionMapping_Both_Values
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Both_Values");
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00016356 File Offset: 0x00014556
		internal static string Mapping_InvalidContent_ConditionMapping_Either_Values
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Either_Values");
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x00016362 File Offset: 0x00014562
		internal static string Mapping_InvalidContent_ConditionMapping_NonScalar
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_NonScalar");
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001636E File Offset: 0x0001456E
		internal static string Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind", new object[] { p0, p1 });
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00016388 File Offset: 0x00014588
		internal static string Mapping_InvalidContent_ConditionMapping_InvalidMember(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_InvalidMember", new object[] { p0 });
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001639E File Offset: 0x0001459E
		internal static string Mapping_InvalidContent_ConditionMapping_Computed(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Computed", new object[] { p0 });
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000163B4 File Offset: 0x000145B4
		internal static string Mapping_InvalidContent_Emtpty_SetMap(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Emtpty_SetMap", new object[] { p0 });
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x000163CA File Offset: 0x000145CA
		internal static string Mapping_InvalidContent_TypeMapping_QueryView
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_TypeMapping_QueryView");
			}
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000163D6 File Offset: 0x000145D6
		internal static string Mapping_Default_OCMapping_Clr_Member(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Clr_Member", new object[] { p0, p1, p2 });
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x000163F4 File Offset: 0x000145F4
		internal static string Mapping_Default_OCMapping_Clr_Member2(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Clr_Member2", new object[] { p0, p1, p2 });
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00016412 File Offset: 0x00014612
		internal static string Mapping_Default_OCMapping_Invalid_MemberType(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Invalid_MemberType", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001643E File Offset: 0x0001463E
		internal static string Mapping_Default_OCMapping_MemberKind_Mismatch(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_MemberKind_Mismatch", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001646A File Offset: 0x0001466A
		internal static string Mapping_Default_OCMapping_MultiplicityMismatch(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_MultiplicityMismatch", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00016496 File Offset: 0x00014696
		internal static string Mapping_Default_OCMapping_Member_Count_Mismatch(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Member_Count_Mismatch", new object[] { p0, p1 });
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x000164B0 File Offset: 0x000146B0
		internal static string Mapping_Default_OCMapping_Member_Type_Mismatch(object p0, object p1, object p2, object p3, object p4, object p5, object p6, object p7)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Member_Type_Mismatch", new object[] { p0, p1, p2, p3, p4, p5, p6, p7 });
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x000164E6 File Offset: 0x000146E6
		internal static string Mapping_Enum_OCMapping_UnderlyingTypesMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_Enum_OCMapping_UnderlyingTypesMismatch", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00016508 File Offset: 0x00014708
		internal static string Mapping_Enum_OCMapping_MemberMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_Enum_OCMapping_MemberMismatch", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001652A File Offset: 0x0001472A
		internal static string Mapping_NotFound_EntityContainer(object p0)
		{
			return EntityRes.GetString("Mapping_NotFound_EntityContainer", new object[] { p0 });
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00016540 File Offset: 0x00014740
		internal static string Mapping_Duplicate_CdmAssociationSet_StorageMap(object p0)
		{
			return EntityRes.GetString("Mapping_Duplicate_CdmAssociationSet_StorageMap", new object[] { p0 });
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00016556 File Offset: 0x00014756
		internal static string Mapping_Invalid_CSRootElementMissing(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Invalid_CSRootElementMissing", new object[] { p0, p1, p2 });
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00016574 File Offset: 0x00014774
		internal static string Mapping_ConditionValueTypeMismatch
		{
			get
			{
				return EntityRes.GetString("Mapping_ConditionValueTypeMismatch");
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00016580 File Offset: 0x00014780
		internal static string Mapping_Storage_InvalidSpace(object p0)
		{
			return EntityRes.GetString("Mapping_Storage_InvalidSpace", new object[] { p0 });
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00016596 File Offset: 0x00014796
		internal static string Mapping_Invalid_Member_Mapping(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Invalid_Member_Mapping", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x000165C2 File Offset: 0x000147C2
		internal static string Mapping_Invalid_CSide_ScalarProperty(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_CSide_ScalarProperty", new object[] { p0 });
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x000165D8 File Offset: 0x000147D8
		internal static string Mapping_Duplicate_Type(object p0)
		{
			return EntityRes.GetString("Mapping_Duplicate_Type", new object[] { p0 });
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000165EE File Offset: 0x000147EE
		internal static string Mapping_Duplicate_PropertyMap_CaseInsensitive(object p0)
		{
			return EntityRes.GetString("Mapping_Duplicate_PropertyMap_CaseInsensitive", new object[] { p0 });
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00016604 File Offset: 0x00014804
		internal static string Mapping_Enum_EmptyValue(object p0)
		{
			return EntityRes.GetString("Mapping_Enum_EmptyValue", new object[] { p0 });
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001661A File Offset: 0x0001481A
		internal static string Mapping_Enum_InvalidValue(object p0)
		{
			return EntityRes.GetString("Mapping_Enum_InvalidValue", new object[] { p0 });
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00016630 File Offset: 0x00014830
		internal static string Mapping_InvalidMappingSchema_Parsing(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidMappingSchema_Parsing", new object[] { p0 });
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00016646 File Offset: 0x00014846
		internal static string Mapping_InvalidMappingSchema_validation(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidMappingSchema_validation", new object[] { p0 });
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001665C File Offset: 0x0001485C
		internal static string Mapping_Object_InvalidType(object p0)
		{
			return EntityRes.GetString("Mapping_Object_InvalidType", new object[] { p0 });
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00016672 File Offset: 0x00014872
		internal static string Mapping_Provider_WrongConnectionType(object p0)
		{
			return EntityRes.GetString("Mapping_Provider_WrongConnectionType", new object[] { p0 });
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00016688 File Offset: 0x00014888
		internal static string Mapping_Views_For_Extent_Not_Generated(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Views_For_Extent_Not_Generated", new object[] { p0, p1 });
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x000166A2 File Offset: 0x000148A2
		internal static string Mapping_TableName_QueryView(object p0)
		{
			return EntityRes.GetString("Mapping_TableName_QueryView", new object[] { p0 });
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000166B8 File Offset: 0x000148B8
		internal static string Mapping_Empty_QueryView(object p0)
		{
			return EntityRes.GetString("Mapping_Empty_QueryView", new object[] { p0 });
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x000166CE File Offset: 0x000148CE
		internal static string Mapping_Empty_QueryView_OfType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Empty_QueryView_OfType", new object[] { p0, p1 });
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000166E8 File Offset: 0x000148E8
		internal static string Mapping_Empty_QueryView_OfTypeOnly(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Empty_QueryView_OfTypeOnly", new object[] { p0, p1 });
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00016702 File Offset: 0x00014902
		internal static string Mapping_QueryView_PropertyMaps(object p0)
		{
			return EntityRes.GetString("Mapping_QueryView_PropertyMaps", new object[] { p0 });
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00016718 File Offset: 0x00014918
		internal static string Mapping_Invalid_QueryView(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Invalid_QueryView", new object[] { p0, p1 });
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00016732 File Offset: 0x00014932
		internal static string Mapping_Invalid_QueryView2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Invalid_QueryView2", new object[] { p0, p1 });
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001674C File Offset: 0x0001494C
		internal static string Mapping_Invalid_QueryView_Type(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_QueryView_Type", new object[] { p0 });
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00016762 File Offset: 0x00014962
		internal static string Mapping_TypeName_For_First_QueryView
		{
			get
			{
				return EntityRes.GetString("Mapping_TypeName_For_First_QueryView");
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001676E File Offset: 0x0001496E
		internal static string Mapping_AllQueryViewAtCompileTime(object p0)
		{
			return EntityRes.GetString("Mapping_AllQueryViewAtCompileTime", new object[] { p0 });
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00016784 File Offset: 0x00014984
		internal static string Mapping_QueryViewMultipleTypeInTypeName(object p0)
		{
			return EntityRes.GetString("Mapping_QueryViewMultipleTypeInTypeName", new object[] { p0 });
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001679A File Offset: 0x0001499A
		internal static string Mapping_QueryView_Duplicate_OfType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_QueryView_Duplicate_OfType", new object[] { p0, p1 });
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000167B4 File Offset: 0x000149B4
		internal static string Mapping_QueryView_Duplicate_OfTypeOnly(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_QueryView_Duplicate_OfTypeOnly", new object[] { p0, p1 });
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000167CE File Offset: 0x000149CE
		internal static string Mapping_QueryView_TypeName_Not_Defined(object p0)
		{
			return EntityRes.GetString("Mapping_QueryView_TypeName_Not_Defined", new object[] { p0 });
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x000167E4 File Offset: 0x000149E4
		internal static string Mapping_QueryView_For_Base_Type(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_QueryView_For_Base_Type", new object[] { p0, p1 });
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000167FE File Offset: 0x000149FE
		internal static string Mapping_UnsupportedExpressionKind_QueryView(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_UnsupportedExpressionKind_QueryView", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001681C File Offset: 0x00014A1C
		internal static string Mapping_UnsupportedFunctionCall_QueryView(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_UnsupportedFunctionCall_QueryView", new object[] { p0, p1 });
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00016836 File Offset: 0x00014A36
		internal static string Mapping_UnsupportedScanTarget_QueryView(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_UnsupportedScanTarget_QueryView", new object[] { p0, p1 });
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00016850 File Offset: 0x00014A50
		internal static string Mapping_UnsupportedPropertyKind_QueryView(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_UnsupportedPropertyKind_QueryView", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001686E File Offset: 0x00014A6E
		internal static string Mapping_UnsupportedInitialization_QueryView(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_UnsupportedInitialization_QueryView", new object[] { p0, p1 });
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00016888 File Offset: 0x00014A88
		internal static string Mapping_EntitySetMismatchOnAssociationSetEnd_QueryView(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_EntitySetMismatchOnAssociationSetEnd_QueryView", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000168AA File Offset: 0x00014AAA
		internal static string Mapping_Invalid_Query_Views_MissingSetClosure(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Query_Views_MissingSetClosure", new object[] { p0 });
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000168C0 File Offset: 0x00014AC0
		internal static string DbMappingViewCacheTypeAttribute_InvalidContextType(object p0)
		{
			return EntityRes.GetString("DbMappingViewCacheTypeAttribute_InvalidContextType", new object[] { p0 });
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x000168D6 File Offset: 0x00014AD6
		internal static string DbMappingViewCacheTypeAttribute_CacheTypeNotFound(object p0)
		{
			return EntityRes.GetString("DbMappingViewCacheTypeAttribute_CacheTypeNotFound", new object[] { p0 });
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000168EC File Offset: 0x00014AEC
		internal static string DbMappingViewCacheTypeAttribute_MultipleInstancesWithSameContextType(object p0)
		{
			return EntityRes.GetString("DbMappingViewCacheTypeAttribute_MultipleInstancesWithSameContextType", new object[] { p0 });
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00016902 File Offset: 0x00014B02
		internal static string DbMappingViewCacheFactory_CreateFailure
		{
			get
			{
				return EntityRes.GetString("DbMappingViewCacheFactory_CreateFailure");
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001690E File Offset: 0x00014B0E
		internal static string Generated_View_Type_Super_Class(object p0)
		{
			return EntityRes.GetString("Generated_View_Type_Super_Class", new object[] { p0 });
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00016924 File Offset: 0x00014B24
		internal static string Generated_Views_Invalid_Extent(object p0)
		{
			return EntityRes.GetString("Generated_Views_Invalid_Extent", new object[] { p0 });
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0001693A File Offset: 0x00014B3A
		internal static string MappingViewCacheFactory_MustNotChange
		{
			get
			{
				return EntityRes.GetString("MappingViewCacheFactory_MustNotChange");
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00016946 File Offset: 0x00014B46
		internal static string Mapping_ItemWithSameNameExistsBothInCSpaceAndSSpace(object p0)
		{
			return EntityRes.GetString("Mapping_ItemWithSameNameExistsBothInCSpaceAndSSpace", new object[] { p0 });
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001695C File Offset: 0x00014B5C
		internal static string Mapping_AbstractTypeMappingToNonAbstractType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_AbstractTypeMappingToNonAbstractType", new object[] { p0, p1 });
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00016976 File Offset: 0x00014B76
		internal static string Mapping_EnumTypeMappingToNonEnumType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_EnumTypeMappingToNonEnumType", new object[] { p0, p1 });
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00016990 File Offset: 0x00014B90
		internal static string StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping(object p0, object p1, object p2)
		{
			return EntityRes.GetString("StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping", new object[] { p0, p1, p2 });
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x000169AE File Offset: 0x00014BAE
		internal static string Mapping_InvalidContent_IsTypeOfNotTerminated
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_IsTypeOfNotTerminated");
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000169BA File Offset: 0x00014BBA
		internal static string Mapping_CannotMapCLRTypeMultipleTimes(object p0)
		{
			return EntityRes.GetString("Mapping_CannotMapCLRTypeMultipleTimes", new object[] { p0 });
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x000169D0 File Offset: 0x00014BD0
		internal static string Mapping_ModificationFunction_In_Table_Context
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_In_Table_Context");
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x000169DC File Offset: 0x00014BDC
		internal static string Mapping_ModificationFunction_Multiple_Types
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_Multiple_Types");
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000169E8 File Offset: 0x00014BE8
		internal static string Mapping_ModificationFunction_UnknownFunction(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_UnknownFunction", new object[] { p0 });
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000169FE File Offset: 0x00014BFE
		internal static string Mapping_ModificationFunction_AmbiguousFunction(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AmbiguousFunction", new object[] { p0 });
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00016A14 File Offset: 0x00014C14
		internal static string Mapping_ModificationFunction_NotValidFunction(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_NotValidFunction", new object[] { p0 });
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00016A2A File Offset: 0x00014C2A
		internal static string Mapping_ModificationFunction_NotValidFunctionParameter(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_NotValidFunctionParameter", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00016A48 File Offset: 0x00014C48
		internal static string Mapping_ModificationFunction_MissingParameter(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_MissingParameter", new object[] { p0, p1 });
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00016A62 File Offset: 0x00014C62
		internal static string Mapping_ModificationFunction_AssociationSetDoesNotExist(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetDoesNotExist", new object[] { p0 });
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00016A78 File Offset: 0x00014C78
		internal static string Mapping_ModificationFunction_AssociationSetRoleDoesNotExist(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetRoleDoesNotExist", new object[] { p0 });
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00016A8E File Offset: 0x00014C8E
		internal static string Mapping_ModificationFunction_AssociationSetFromRoleIsNotEntitySet(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetFromRoleIsNotEntitySet", new object[] { p0 });
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00016AA4 File Offset: 0x00014CA4
		internal static string Mapping_ModificationFunction_AssociationSetCardinality(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetCardinality", new object[] { p0 });
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00016ABA File Offset: 0x00014CBA
		internal static string Mapping_ModificationFunction_ComplexTypeNotFound(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_ComplexTypeNotFound", new object[] { p0 });
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00016AD0 File Offset: 0x00014CD0
		internal static string Mapping_ModificationFunction_WrongComplexType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_WrongComplexType", new object[] { p0, p1 });
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00016AEA File Offset: 0x00014CEA
		internal static string Mapping_ModificationFunction_MissingVersion
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_MissingVersion");
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00016AF6 File Offset: 0x00014CF6
		internal static string Mapping_ModificationFunction_VersionMustBeOriginal
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_VersionMustBeOriginal");
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00016B02 File Offset: 0x00014D02
		internal static string Mapping_ModificationFunction_VersionMustBeCurrent
		{
			get
			{
				return EntityRes.GetString("Mapping_ModificationFunction_VersionMustBeCurrent");
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00016B0E File Offset: 0x00014D0E
		internal static string Mapping_ModificationFunction_ParameterNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_ParameterNotFound", new object[] { p0, p1 });
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00016B28 File Offset: 0x00014D28
		internal static string Mapping_ModificationFunction_PropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_PropertyNotFound", new object[] { p0, p1 });
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00016B42 File Offset: 0x00014D42
		internal static string Mapping_ModificationFunction_PropertyNotKey(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_PropertyNotKey", new object[] { p0, p1 });
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00016B5C File Offset: 0x00014D5C
		internal static string Mapping_ModificationFunction_ParameterBoundTwice(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_ParameterBoundTwice", new object[] { p0 });
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00016B72 File Offset: 0x00014D72
		internal static string Mapping_ModificationFunction_RedundantEntityTypeMapping(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_RedundantEntityTypeMapping", new object[] { p0 });
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00016B88 File Offset: 0x00014D88
		internal static string Mapping_ModificationFunction_MissingSetClosure(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_MissingSetClosure", new object[] { p0 });
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00016B9E File Offset: 0x00014D9E
		internal static string Mapping_ModificationFunction_MissingEntityType(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_MissingEntityType", new object[] { p0 });
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00016BB4 File Offset: 0x00014DB4
		internal static string Mapping_ModificationFunction_PropertyParameterTypeMismatch(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_PropertyParameterTypeMismatch", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00016BE0 File Offset: 0x00014DE0
		internal static string Mapping_ModificationFunction_AssociationSetAmbiguous(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetAmbiguous", new object[] { p0 });
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00016BF6 File Offset: 0x00014DF6
		internal static string Mapping_ModificationFunction_MultipleEndsOfAssociationMapped(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_MultipleEndsOfAssociationMapped", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00016C14 File Offset: 0x00014E14
		internal static string Mapping_ModificationFunction_AmbiguousResultBinding(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AmbiguousResultBinding", new object[] { p0, p1 });
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00016C2E File Offset: 0x00014E2E
		internal static string Mapping_ModificationFunction_AssociationSetNotMappedForOperation(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationSetNotMappedForOperation", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00016C50 File Offset: 0x00014E50
		internal static string Mapping_ModificationFunction_AssociationEndMappingInvalidForEntityType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationEndMappingInvalidForEntityType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00016C6E File Offset: 0x00014E6E
		internal static string Mapping_ModificationFunction_AssociationEndMappingForeignKeyAssociation(object p0)
		{
			return EntityRes.GetString("Mapping_ModificationFunction_AssociationEndMappingForeignKeyAssociation", new object[] { p0 });
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00016C84 File Offset: 0x00014E84
		internal static string Mapping_StoreTypeMismatch_ScalarPropertyMapping(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_StoreTypeMismatch_ScalarPropertyMapping", new object[] { p0, p1 });
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x00016C9E File Offset: 0x00014E9E
		internal static string Mapping_DistinctFlagInReadWriteContainer
		{
			get
			{
				return EntityRes.GetString("Mapping_DistinctFlagInReadWriteContainer");
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00016CAA File Offset: 0x00014EAA
		internal static string Mapping_ProviderReturnsNullType(object p0)
		{
			return EntityRes.GetString("Mapping_ProviderReturnsNullType", new object[] { p0 });
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00016CC0 File Offset: 0x00014EC0
		internal static string Mapping_DifferentEdmStoreVersion
		{
			get
			{
				return EntityRes.GetString("Mapping_DifferentEdmStoreVersion");
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00016CCC File Offset: 0x00014ECC
		internal static string Mapping_DifferentMappingEdmStoreVersion
		{
			get
			{
				return EntityRes.GetString("Mapping_DifferentMappingEdmStoreVersion");
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00016CD8 File Offset: 0x00014ED8
		internal static string Mapping_FunctionImport_StoreFunctionDoesNotExist(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_StoreFunctionDoesNotExist", new object[] { p0 });
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00016CEE File Offset: 0x00014EEE
		internal static string Mapping_FunctionImport_FunctionImportDoesNotExist(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_FunctionImportDoesNotExist", new object[] { p0, p1 });
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00016D08 File Offset: 0x00014F08
		internal static string Mapping_FunctionImport_FunctionImportMappedMultipleTimes(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_FunctionImportMappedMultipleTimes", new object[] { p0 });
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00016D1E File Offset: 0x00014F1E
		internal static string Mapping_FunctionImport_TargetFunctionMustBeNonComposable(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_TargetFunctionMustBeNonComposable", new object[] { p0, p1 });
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00016D38 File Offset: 0x00014F38
		internal static string Mapping_FunctionImport_TargetFunctionMustBeComposable(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_TargetFunctionMustBeComposable", new object[] { p0, p1 });
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00016D52 File Offset: 0x00014F52
		internal static string Mapping_FunctionImport_TargetParameterHasNoCorrespondingImportParameter(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_TargetParameterHasNoCorrespondingImportParameter", new object[] { p0 });
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00016D68 File Offset: 0x00014F68
		internal static string Mapping_FunctionImport_ImportParameterHasNoCorrespondingTargetParameter(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ImportParameterHasNoCorrespondingTargetParameter", new object[] { p0 });
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00016D7E File Offset: 0x00014F7E
		internal static string Mapping_FunctionImport_IncompatibleParameterMode(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_IncompatibleParameterMode", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00016D9C File Offset: 0x00014F9C
		internal static string Mapping_FunctionImport_IncompatibleParameterType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_IncompatibleParameterType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00016DBA File Offset: 0x00014FBA
		internal static string Mapping_FunctionImport_IncompatibleEnumParameterType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_FunctionImport_IncompatibleEnumParameterType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00016DDC File Offset: 0x00014FDC
		internal static string Mapping_FunctionImport_RowsAffectedParameterDoesNotExist(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_RowsAffectedParameterDoesNotExist", new object[] { p0, p1 });
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00016DF6 File Offset: 0x00014FF6
		internal static string Mapping_FunctionImport_RowsAffectedParameterHasWrongType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_RowsAffectedParameterHasWrongType", new object[] { p0, p1 });
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00016E10 File Offset: 0x00015010
		internal static string Mapping_FunctionImport_RowsAffectedParameterHasWrongMode(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_FunctionImport_RowsAffectedParameterHasWrongMode", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00016E32 File Offset: 0x00015032
		internal static string Mapping_FunctionImport_EntityTypeMappingForFunctionNotReturningEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_EntityTypeMappingForFunctionNotReturningEntitySet", new object[] { p0, p1 });
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00016E4C File Offset: 0x0001504C
		internal static string Mapping_FunctionImport_InvalidContentEntityTypeForEntitySet(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_FunctionImport_InvalidContentEntityTypeForEntitySet", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00016E6E File Offset: 0x0001506E
		internal static string Mapping_FunctionImport_ConditionValueTypeMismatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ConditionValueTypeMismatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00016E8C File Offset: 0x0001508C
		internal static string Mapping_FunctionImport_UnsupportedType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_UnsupportedType", new object[] { p0, p1 });
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00016EA6 File Offset: 0x000150A6
		internal static string Mapping_FunctionImport_ResultMappingCountDoesNotMatchResultCount(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMappingCountDoesNotMatchResultCount", new object[] { p0 });
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00016EBC File Offset: 0x000150BC
		internal static string Mapping_FunctionImport_ResultMapping_MappedTypeDoesNotMatchReturnType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMapping_MappedTypeDoesNotMatchReturnType", new object[] { p0, p1 });
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00016ED6 File Offset: 0x000150D6
		internal static string Mapping_FunctionImport_ResultMapping_InvalidCTypeCTExpected(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMapping_InvalidCTypeCTExpected", new object[] { p0 });
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00016EEC File Offset: 0x000150EC
		internal static string Mapping_FunctionImport_ResultMapping_InvalidCTypeETExpected(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMapping_InvalidCTypeETExpected", new object[] { p0 });
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00016F02 File Offset: 0x00015102
		internal static string Mapping_FunctionImport_ResultMapping_InvalidSType(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ResultMapping_InvalidSType", new object[] { p0 });
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00016F18 File Offset: 0x00015118
		internal static string Mapping_FunctionImport_PropertyNotMapped(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_PropertyNotMapped", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00016F36 File Offset: 0x00015136
		internal static string Mapping_FunctionImport_ImplicitMappingForAbstractReturnType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ImplicitMappingForAbstractReturnType", new object[] { p0, p1 });
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00016F50 File Offset: 0x00015150
		internal static string Mapping_FunctionImport_ScalarMappingToMulticolumnTVF(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ScalarMappingToMulticolumnTVF", new object[] { p0, p1 });
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00016F6A File Offset: 0x0001516A
		internal static string Mapping_FunctionImport_ScalarMappingTypeMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ScalarMappingTypeMismatch", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00016F8C File Offset: 0x0001518C
		internal static string Mapping_FunctionImport_UnreachableType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_UnreachableType", new object[] { p0, p1 });
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00016FA6 File Offset: 0x000151A6
		internal static string Mapping_FunctionImport_UnreachableIsTypeOf(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_UnreachableIsTypeOf", new object[] { p0, p1 });
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00016FC0 File Offset: 0x000151C0
		internal static string Mapping_FunctionImport_FunctionAmbiguous(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_FunctionAmbiguous", new object[] { p0 });
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00016FD6 File Offset: 0x000151D6
		internal static string Mapping_FunctionImport_CannotInferTargetFunctionKeys(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_CannotInferTargetFunctionKeys", new object[] { p0 });
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00016FEC File Offset: 0x000151EC
		internal static string Entity_EntityCantHaveMultipleChangeTrackers
		{
			get
			{
				return EntityRes.GetString("Entity_EntityCantHaveMultipleChangeTrackers");
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00016FF8 File Offset: 0x000151F8
		internal static string ComplexObject_NullableComplexTypesNotSupported(object p0)
		{
			return EntityRes.GetString("ComplexObject_NullableComplexTypesNotSupported", new object[] { p0 });
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0001700E File Offset: 0x0001520E
		internal static string ComplexObject_ComplexObjectAlreadyAttachedToParent
		{
			get
			{
				return EntityRes.GetString("ComplexObject_ComplexObjectAlreadyAttachedToParent");
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001701A File Offset: 0x0001521A
		internal static string ComplexObject_ComplexChangeRequestedOnScalarProperty(object p0)
		{
			return EntityRes.GetString("ComplexObject_ComplexChangeRequestedOnScalarProperty", new object[] { p0 });
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00017030 File Offset: 0x00015230
		internal static string ObjectStateEntry_SetModifiedOnInvalidProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetModifiedOnInvalidProperty", new object[] { p0 });
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00017046 File Offset: 0x00015246
		internal static string ObjectStateEntry_OriginalValuesDoesNotExist
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_OriginalValuesDoesNotExist");
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00017052 File Offset: 0x00015252
		internal static string ObjectStateEntry_CurrentValuesDoesNotExist
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CurrentValuesDoesNotExist");
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0001705E File Offset: 0x0001525E
		internal static string ObjectStateEntry_InvalidState
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_InvalidState");
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001706A File Offset: 0x0001526A
		internal static string ObjectStateEntry_CannotModifyKeyProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_CannotModifyKeyProperty", new object[] { p0 });
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00017080 File Offset: 0x00015280
		internal static string ObjectStateEntry_CantModifyRelationValues
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantModifyRelationValues");
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0001708C File Offset: 0x0001528C
		internal static string ObjectStateEntry_CantModifyRelationState
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantModifyRelationState");
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00017098 File Offset: 0x00015298
		internal static string ObjectStateEntry_CantModifyDetachedDeletedEntries
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantModifyDetachedDeletedEntries");
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000170A4 File Offset: 0x000152A4
		internal static string ObjectStateEntry_SetModifiedStates(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetModifiedStates", new object[] { p0 });
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x000170BA File Offset: 0x000152BA
		internal static string ObjectStateEntry_CantSetEntityKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantSetEntityKey");
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x000170C6 File Offset: 0x000152C6
		internal static string ObjectStateEntry_CannotAccessKeyEntryValues
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CannotAccessKeyEntryValues");
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x000170D2 File Offset: 0x000152D2
		internal static string ObjectStateEntry_CannotModifyKeyEntryState
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CannotModifyKeyEntryState");
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x000170DE File Offset: 0x000152DE
		internal static string ObjectStateEntry_CannotDeleteOnKeyEntry
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CannotDeleteOnKeyEntry");
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x000170EA File Offset: 0x000152EA
		internal static string ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging");
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000170F6 File Offset: 0x000152F6
		internal static string ObjectStateEntry_ChangeOnUnmappedProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_ChangeOnUnmappedProperty", new object[] { p0 });
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001710C File Offset: 0x0001530C
		internal static string ObjectStateEntry_ChangeOnUnmappedComplexProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_ChangeOnUnmappedComplexProperty", new object[] { p0 });
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00017122 File Offset: 0x00015322
		internal static string ObjectStateEntry_ChangedInDifferentStateFromChanging(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateEntry_ChangedInDifferentStateFromChanging", new object[] { p0, p1 });
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001713C File Offset: 0x0001533C
		internal static string ObjectStateEntry_UnableToEnumerateCollection(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateEntry_UnableToEnumerateCollection", new object[] { p0, p1 });
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00017156 File Offset: 0x00015356
		internal static string ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers");
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00017162 File Offset: 0x00015362
		internal static string ObjectStateEntry_InvalidTypeForComplexTypeProperty
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_InvalidTypeForComplexTypeProperty");
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001716E File Offset: 0x0001536E
		internal static string ObjectStateEntry_ComplexObjectUsedMultipleTimes(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateEntry_ComplexObjectUsedMultipleTimes", new object[] { p0, p1 });
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00017188 File Offset: 0x00015388
		internal static string ObjectStateEntry_SetOriginalComplexProperties(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetOriginalComplexProperties", new object[] { p0 });
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001719E File Offset: 0x0001539E
		internal static string ObjectStateEntry_NullOriginalValueForNonNullableProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ObjectStateEntry_NullOriginalValueForNonNullableProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x000171BC File Offset: 0x000153BC
		internal static string ObjectStateEntry_SetOriginalPrimaryKey(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetOriginalPrimaryKey", new object[] { p0 });
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x000171D2 File Offset: 0x000153D2
		internal static string ObjectStateManager_NoEntryExistForEntityKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_NoEntryExistForEntityKey");
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000171DE File Offset: 0x000153DE
		internal static string ObjectStateManager_NoEntryExistsForObject(object p0)
		{
			return EntityRes.GetString("ObjectStateManager_NoEntryExistsForObject", new object[] { p0 });
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x000171F4 File Offset: 0x000153F4
		internal static string ObjectStateManager_EntityNotTracked
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_EntityNotTracked");
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00017200 File Offset: 0x00015400
		internal static string ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager");
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001720C File Offset: 0x0001540C
		internal static string ObjectStateManager_ObjectStateManagerContainsThisEntityKey(object p0)
		{
			return EntityRes.GetString("ObjectStateManager_ObjectStateManagerContainsThisEntityKey", new object[] { p0 });
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00017222 File Offset: 0x00015422
		internal static string ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity(object p0)
		{
			return EntityRes.GetString("ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity", new object[] { p0 });
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00017238 File Offset: 0x00015438
		internal static string ObjectStateManager_CannotFixUpKeyToExistingValues(object p0)
		{
			return EntityRes.GetString("ObjectStateManager_CannotFixUpKeyToExistingValues", new object[] { p0 });
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0001724E File Offset: 0x0001544E
		internal static string ObjectStateManager_KeyPropertyDoesntMatchValueInKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_KeyPropertyDoesntMatchValueInKey");
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0001725A File Offset: 0x0001545A
		internal static string ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach");
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00017266 File Offset: 0x00015466
		internal static string ObjectStateManager_InvalidKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_InvalidKey");
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00017272 File Offset: 0x00015472
		internal static string ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType", new object[] { p0, p1 });
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0001728C File Offset: 0x0001548C
		internal static string ObjectStateManager_AcceptChangesEntityKeyIsNotValid
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_AcceptChangesEntityKeyIsNotValid");
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00017298 File Offset: 0x00015498
		internal static string ObjectStateManager_EntityConflictsWithKeyEntry
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_EntityConflictsWithKeyEntry");
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x000172A4 File Offset: 0x000154A4
		internal static string ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity");
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x000172B0 File Offset: 0x000154B0
		internal static string ObjectStateManager_CannotChangeRelationshipStateEntityDeleted
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotChangeRelationshipStateEntityDeleted");
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x000172BC File Offset: 0x000154BC
		internal static string ObjectStateManager_CannotChangeRelationshipStateEntityAdded
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotChangeRelationshipStateEntityAdded");
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x000172C8 File Offset: 0x000154C8
		internal static string ObjectStateManager_CannotChangeRelationshipStateKeyEntry
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotChangeRelationshipStateKeyEntry");
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x000172D4 File Offset: 0x000154D4
		internal static string ObjectStateManager_ConflictingChangesOfRelationshipDetected(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateManager_ConflictingChangesOfRelationshipDetected", new object[] { p0, p1 });
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x000172EE File Offset: 0x000154EE
		internal static string ObjectStateManager_ChangeRelationshipStateNotSupportedForForeignKeyAssociations
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_ChangeRelationshipStateNotSupportedForForeignKeyAssociations");
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x000172FA File Offset: 0x000154FA
		internal static string ObjectStateManager_ChangeStateFromAddedWithNullKeyIsInvalid
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_ChangeStateFromAddedWithNullKeyIsInvalid");
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00017306 File Offset: 0x00015506
		internal static string ObjectContext_ClientEntityRemovedFromStore(object p0)
		{
			return EntityRes.GetString("ObjectContext_ClientEntityRemovedFromStore", new object[] { p0 });
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0001731C File Offset: 0x0001551C
		internal static string ObjectContext_StoreEntityNotPresentInClient
		{
			get
			{
				return EntityRes.GetString("ObjectContext_StoreEntityNotPresentInClient");
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00017328 File Offset: 0x00015528
		internal static string ObjectContext_InvalidConnectionString
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidConnectionString");
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00017334 File Offset: 0x00015534
		internal static string ObjectContext_InvalidConnection
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidConnection");
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00017340 File Offset: 0x00015540
		internal static string ObjectContext_InvalidDefaultContainerName(object p0)
		{
			return EntityRes.GetString("ObjectContext_InvalidDefaultContainerName", new object[] { p0 });
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00017356 File Offset: 0x00015556
		internal static string ObjectContext_NthElementInAddedState(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementInAddedState", new object[] { p0 });
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001736C File Offset: 0x0001556C
		internal static string ObjectContext_NthElementIsDuplicate(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementIsDuplicate", new object[] { p0 });
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00017382 File Offset: 0x00015582
		internal static string ObjectContext_NthElementIsNull(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementIsNull", new object[] { p0 });
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00017398 File Offset: 0x00015598
		internal static string ObjectContext_NthElementNotInObjectStateManager(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementNotInObjectStateManager", new object[] { p0 });
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x000173AE File Offset: 0x000155AE
		internal static string ObjectContext_ObjectNotFound
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ObjectNotFound");
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x000173BA File Offset: 0x000155BA
		internal static string ObjectContext_CannotDeleteEntityNotInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotDeleteEntityNotInObjectStateManager");
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x000173C6 File Offset: 0x000155C6
		internal static string ObjectContext_CannotDetachEntityNotInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotDetachEntityNotInObjectStateManager");
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000173D2 File Offset: 0x000155D2
		internal static string ObjectContext_EntitySetNotFoundForName(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntitySetNotFoundForName", new object[] { p0 });
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000173E8 File Offset: 0x000155E8
		internal static string ObjectContext_EntityContainerNotFoundForName(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntityContainerNotFoundForName", new object[] { p0 });
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x000173FE File Offset: 0x000155FE
		internal static string ObjectContext_InvalidCommandTimeout
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidCommandTimeout");
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0001740A File Offset: 0x0001560A
		internal static string ObjectContext_NoMappingForEntityType(object p0)
		{
			return EntityRes.GetString("ObjectContext_NoMappingForEntityType", new object[] { p0 });
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00017420 File Offset: 0x00015620
		internal static string ObjectContext_EntityAlreadyExistsInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectContext_EntityAlreadyExistsInObjectStateManager");
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001742C File Offset: 0x0001562C
		internal static string ObjectContext_InvalidEntitySetInKey(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetInKey", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0001744E File Offset: 0x0001564E
		internal static string ObjectContext_CannotAttachEntityWithoutKey
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotAttachEntityWithoutKey");
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0001745A File Offset: 0x0001565A
		internal static string ObjectContext_CannotAttachEntityWithTemporaryKey
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotAttachEntityWithTemporaryKey");
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00017466 File Offset: 0x00015666
		internal static string ObjectContext_EntitySetNameOrEntityKeyRequired
		{
			get
			{
				return EntityRes.GetString("ObjectContext_EntitySetNameOrEntityKeyRequired");
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00017472 File Offset: 0x00015672
		internal static string ObjectContext_ExecuteFunctionTypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionTypeMismatch", new object[] { p0, p1 });
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001748C File Offset: 0x0001568C
		internal static string ObjectContext_ExecuteFunctionCalledWithScalarFunction(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionCalledWithScalarFunction", new object[] { p0, p1 });
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x000174A6 File Offset: 0x000156A6
		internal static string ObjectContext_ExecuteFunctionCalledWithNonQueryFunction(object p0)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionCalledWithNonQueryFunction", new object[] { p0 });
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x000174BC File Offset: 0x000156BC
		internal static string ObjectContext_ExecuteFunctionCalledWithNullParameter(object p0)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionCalledWithNullParameter", new object[] { p0 });
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000174D2 File Offset: 0x000156D2
		internal static string ObjectContext_ContainerQualifiedEntitySetNameRequired
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ContainerQualifiedEntitySetNameRequired");
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000174DE File Offset: 0x000156DE
		internal static string ObjectContext_CannotSetDefaultContainerName
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotSetDefaultContainerName");
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x000174EA File Offset: 0x000156EA
		internal static string ObjectContext_QualfiedEntitySetName
		{
			get
			{
				return EntityRes.GetString("ObjectContext_QualfiedEntitySetName");
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000174F6 File Offset: 0x000156F6
		internal static string ObjectContext_EntitiesHaveDifferentType(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_EntitiesHaveDifferentType", new object[] { p0, p1 });
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00017510 File Offset: 0x00015710
		internal static string ObjectContext_EntityMustBeUnchangedOrModified(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntityMustBeUnchangedOrModified", new object[] { p0 });
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00017526 File Offset: 0x00015726
		internal static string ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted", new object[] { p0 });
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0001753C File Offset: 0x0001573C
		internal static string ObjectContext_AcceptAllChangesFailure(object p0)
		{
			return EntityRes.GetString("ObjectContext_AcceptAllChangesFailure", new object[] { p0 });
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00017552 File Offset: 0x00015752
		internal static string ObjectContext_CommitWithConceptualNull
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CommitWithConceptualNull");
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001755E File Offset: 0x0001575E
		internal static string ObjectContext_InvalidEntitySetOnEntity(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetOnEntity", new object[] { p0, p1 });
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00017578 File Offset: 0x00015778
		internal static string ObjectContext_InvalidObjectSetTypeForEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ObjectContext_InvalidObjectSetTypeForEntitySet", new object[] { p0, p1, p2 });
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00017596 File Offset: 0x00015796
		internal static string ObjectContext_InvalidEntitySetInKeyFromName(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetInKeyFromName", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x000175BD File Offset: 0x000157BD
		internal static string ObjectContext_ObjectDisposed
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ObjectDisposed");
			}
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x000175C9 File Offset: 0x000157C9
		internal static string ObjectContext_CannotExplicitlyLoadDetachedRelationships(object p0)
		{
			return EntityRes.GetString("ObjectContext_CannotExplicitlyLoadDetachedRelationships", new object[] { p0 });
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x000175DF File Offset: 0x000157DF
		internal static string ObjectContext_CannotLoadReferencesUsingDifferentContext(object p0)
		{
			return EntityRes.GetString("ObjectContext_CannotLoadReferencesUsingDifferentContext", new object[] { p0 });
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x000175F5 File Offset: 0x000157F5
		internal static string ObjectContext_SelectorExpressionMustBeMemberAccess
		{
			get
			{
				return EntityRes.GetString("ObjectContext_SelectorExpressionMustBeMemberAccess");
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00017601 File Offset: 0x00015801
		internal static string ObjectContext_MultipleEntitySetsFoundInSingleContainer(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_MultipleEntitySetsFoundInSingleContainer", new object[] { p0, p1 });
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001761B File Offset: 0x0001581B
		internal static string ObjectContext_MultipleEntitySetsFoundInAllContainers(object p0)
		{
			return EntityRes.GetString("ObjectContext_MultipleEntitySetsFoundInAllContainers", new object[] { p0 });
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00017631 File Offset: 0x00015831
		internal static string ObjectContext_NoEntitySetFoundForType(object p0)
		{
			return EntityRes.GetString("ObjectContext_NoEntitySetFoundForType", new object[] { p0 });
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00017647 File Offset: 0x00015847
		internal static string ObjectContext_EntityNotInObjectSet_Delete(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ObjectContext_EntityNotInObjectSet_Delete", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00017669 File Offset: 0x00015869
		internal static string ObjectContext_EntityNotInObjectSet_Detach(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ObjectContext_EntityNotInObjectSet_Detach", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0001768B File Offset: 0x0001588B
		internal static string ObjectContext_InvalidEntityState
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidEntityState");
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00017697 File Offset: 0x00015897
		internal static string ObjectContext_InvalidRelationshipState
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidRelationshipState");
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x000176A3 File Offset: 0x000158A3
		internal static string ObjectContext_EntityNotTrackedOrHasTempKey
		{
			get
			{
				return EntityRes.GetString("ObjectContext_EntityNotTrackedOrHasTempKey");
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x000176AF File Offset: 0x000158AF
		internal static string ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues");
			}
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000176BB File Offset: 0x000158BB
		internal static string ObjectContext_InvalidEntitySetForStoreQuery(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetForStoreQuery", new object[] { p0, p1, p2 });
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000176D9 File Offset: 0x000158D9
		internal static string ObjectContext_InvalidTypeForStoreQuery(object p0)
		{
			return EntityRes.GetString("ObjectContext_InvalidTypeForStoreQuery", new object[] { p0 });
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000176EF File Offset: 0x000158EF
		internal static string ObjectContext_TwoPropertiesMappedToSameColumn(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_TwoPropertiesMappedToSameColumn", new object[] { p0, p1 });
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00017709 File Offset: 0x00015909
		internal static string RelatedEnd_InvalidOwnerStateForAttach
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_InvalidOwnerStateForAttach");
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00017715 File Offset: 0x00015915
		internal static string RelatedEnd_InvalidNthElementNullForAttach(object p0)
		{
			return EntityRes.GetString("RelatedEnd_InvalidNthElementNullForAttach", new object[] { p0 });
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001772B File Offset: 0x0001592B
		internal static string RelatedEnd_InvalidNthElementContextForAttach(object p0)
		{
			return EntityRes.GetString("RelatedEnd_InvalidNthElementContextForAttach", new object[] { p0 });
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00017741 File Offset: 0x00015941
		internal static string RelatedEnd_InvalidNthElementStateForAttach(object p0)
		{
			return EntityRes.GetString("RelatedEnd_InvalidNthElementStateForAttach", new object[] { p0 });
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00017757 File Offset: 0x00015957
		internal static string RelatedEnd_InvalidEntityContextForAttach
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_InvalidEntityContextForAttach");
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00017763 File Offset: 0x00015963
		internal static string RelatedEnd_InvalidEntityStateForAttach
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_InvalidEntityStateForAttach");
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0001776F File Offset: 0x0001596F
		internal static string RelatedEnd_UnableToAddEntity
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_UnableToAddEntity");
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0001777B File Offset: 0x0001597B
		internal static string RelatedEnd_UnableToRemoveEntity
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_UnableToRemoveEntity");
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00017787 File Offset: 0x00015987
		internal static string RelatedEnd_UnableToAddRelationshipWithDeletedEntity
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_UnableToAddRelationshipWithDeletedEntity");
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00017793 File Offset: 0x00015993
		internal static string RelatedEnd_CannotSerialize(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotSerialize", new object[] { p0 });
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000177A9 File Offset: 0x000159A9
		internal static string RelatedEnd_CannotAddToFixedSizeArray(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotAddToFixedSizeArray", new object[] { p0 });
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000177BF File Offset: 0x000159BF
		internal static string RelatedEnd_CannotRemoveFromFixedSizeArray(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotRemoveFromFixedSizeArray", new object[] { p0 });
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x000177D5 File Offset: 0x000159D5
		internal static string Materializer_PropertyIsNotNullable
		{
			get
			{
				return EntityRes.GetString("Materializer_PropertyIsNotNullable");
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x000177E1 File Offset: 0x000159E1
		internal static string Materializer_PropertyIsNotNullableWithName(object p0)
		{
			return EntityRes.GetString("Materializer_PropertyIsNotNullableWithName", new object[] { p0 });
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000177F7 File Offset: 0x000159F7
		internal static string Materializer_SetInvalidValue(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Materializer_SetInvalidValue", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00017819 File Offset: 0x00015A19
		internal static string Materializer_InvalidCastReference(object p0, object p1)
		{
			return EntityRes.GetString("Materializer_InvalidCastReference", new object[] { p0, p1 });
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00017833 File Offset: 0x00015A33
		internal static string Materializer_InvalidCastNullable(object p0, object p1)
		{
			return EntityRes.GetString("Materializer_InvalidCastNullable", new object[] { p0, p1 });
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0001784D File Offset: 0x00015A4D
		internal static string Materializer_NullReferenceCast(object p0)
		{
			return EntityRes.GetString("Materializer_NullReferenceCast", new object[] { p0 });
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00017863 File Offset: 0x00015A63
		internal static string Materializer_RecyclingEntity(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Materializer_RecyclingEntity", new object[] { p0, p1, p2 });
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00017881 File Offset: 0x00015A81
		internal static string Materializer_AddedEntityAlreadyExists(object p0)
		{
			return EntityRes.GetString("Materializer_AddedEntityAlreadyExists", new object[] { p0 });
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00017897 File Offset: 0x00015A97
		internal static string Materializer_CannotReEnumerateQueryResults
		{
			get
			{
				return EntityRes.GetString("Materializer_CannotReEnumerateQueryResults");
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x000178A3 File Offset: 0x00015AA3
		internal static string Materializer_UnsupportedType
		{
			get
			{
				return EntityRes.GetString("Materializer_UnsupportedType");
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x000178AF File Offset: 0x00015AAF
		internal static string Collections_NoRelationshipSetMatched(object p0)
		{
			return EntityRes.GetString("Collections_NoRelationshipSetMatched", new object[] { p0 });
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x000178C5 File Offset: 0x00015AC5
		internal static string Collections_ExpectedCollectionGotReference(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Collections_ExpectedCollectionGotReference", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x000178E3 File Offset: 0x00015AE3
		internal static string Collections_InvalidEntityStateSource
		{
			get
			{
				return EntityRes.GetString("Collections_InvalidEntityStateSource");
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x000178EF File Offset: 0x00015AEF
		internal static string Collections_InvalidEntityStateLoad(object p0)
		{
			return EntityRes.GetString("Collections_InvalidEntityStateLoad", new object[] { p0 });
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00017905 File Offset: 0x00015B05
		internal static string Collections_CannotFillTryDifferentMergeOption(object p0, object p1)
		{
			return EntityRes.GetString("Collections_CannotFillTryDifferentMergeOption", new object[] { p0, p1 });
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0001791F File Offset: 0x00015B1F
		internal static string Collections_UnableToMergeCollections
		{
			get
			{
				return EntityRes.GetString("Collections_UnableToMergeCollections");
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0001792B File Offset: 0x00015B2B
		internal static string EntityReference_ExpectedReferenceGotCollection(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityReference_ExpectedReferenceGotCollection", new object[] { p0, p1, p2 });
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00017949 File Offset: 0x00015B49
		internal static string EntityReference_CannotAddMoreThanOneEntityToEntityReference(object p0, object p1)
		{
			return EntityRes.GetString("EntityReference_CannotAddMoreThanOneEntityToEntityReference", new object[] { p0, p1 });
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00017963 File Offset: 0x00015B63
		internal static string EntityReference_LessThanExpectedRelatedEntitiesFound
		{
			get
			{
				return EntityRes.GetString("EntityReference_LessThanExpectedRelatedEntitiesFound");
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0001796F File Offset: 0x00015B6F
		internal static string EntityReference_MoreThanExpectedRelatedEntitiesFound
		{
			get
			{
				return EntityRes.GetString("EntityReference_MoreThanExpectedRelatedEntitiesFound");
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0001797B File Offset: 0x00015B7B
		internal static string EntityReference_CannotChangeReferentialConstraintProperty
		{
			get
			{
				return EntityRes.GetString("EntityReference_CannotChangeReferentialConstraintProperty");
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x00017987 File Offset: 0x00015B87
		internal static string EntityReference_CannotSetSpecialKeys
		{
			get
			{
				return EntityRes.GetString("EntityReference_CannotSetSpecialKeys");
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00017993 File Offset: 0x00015B93
		internal static string EntityReference_EntityKeyValueMismatch
		{
			get
			{
				return EntityRes.GetString("EntityReference_EntityKeyValueMismatch");
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0001799F File Offset: 0x00015B9F
		internal static string RelatedEnd_RelatedEndNotFound
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_RelatedEndNotFound");
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000179AB File Offset: 0x00015BAB
		internal static string RelatedEnd_RelatedEndNotAttachedToContext(object p0)
		{
			return EntityRes.GetString("RelatedEnd_RelatedEndNotAttachedToContext", new object[] { p0 });
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x000179C1 File Offset: 0x00015BC1
		internal static string RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd");
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x000179CD File Offset: 0x00015BCD
		internal static string RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd");
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000179D9 File Offset: 0x00015BD9
		internal static string RelatedEnd_InvalidContainedType_Collection(object p0, object p1)
		{
			return EntityRes.GetString("RelatedEnd_InvalidContainedType_Collection", new object[] { p0, p1 });
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x000179F3 File Offset: 0x00015BF3
		internal static string RelatedEnd_InvalidContainedType_Reference(object p0, object p1)
		{
			return EntityRes.GetString("RelatedEnd_InvalidContainedType_Reference", new object[] { p0, p1 });
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00017A0D File Offset: 0x00015C0D
		internal static string RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities", new object[] { p0 });
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00017A23 File Offset: 0x00015C23
		internal static string RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts");
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00017A2F File Offset: 0x00015C2F
		internal static string RelatedEnd_MismatchedMergeOptionOnLoad(object p0)
		{
			return EntityRes.GetString("RelatedEnd_MismatchedMergeOptionOnLoad", new object[] { p0 });
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00017A45 File Offset: 0x00015C45
		internal static string RelatedEnd_EntitySetIsNotValidForRelationship(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("RelatedEnd_EntitySetIsNotValidForRelationship", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00017A6C File Offset: 0x00015C6C
		internal static string RelatedEnd_OwnerIsNull
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_OwnerIsNull");
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00017A78 File Offset: 0x00015C78
		internal static string RelationshipManager_UnableToRetrieveReferentialConstraintProperties
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_UnableToRetrieveReferentialConstraintProperties");
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00017A84 File Offset: 0x00015C84
		internal static string RelationshipManager_InconsistentReferentialConstraintProperties(object p0, object p1)
		{
			return EntityRes.GetString("RelationshipManager_InconsistentReferentialConstraintProperties", new object[] { p0, p1 });
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00017A9E File Offset: 0x00015C9E
		internal static string RelationshipManager_CircularRelationshipsWithReferentialConstraints
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_CircularRelationshipsWithReferentialConstraints");
			}
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00017AAA File Offset: 0x00015CAA
		internal static string RelationshipManager_UnableToFindRelationshipTypeInMetadata(object p0)
		{
			return EntityRes.GetString("RelationshipManager_UnableToFindRelationshipTypeInMetadata", new object[] { p0 });
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00017AC0 File Offset: 0x00015CC0
		internal static string RelationshipManager_InvalidTargetRole(object p0, object p1)
		{
			return EntityRes.GetString("RelationshipManager_InvalidTargetRole", new object[] { p0, p1 });
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00017ADA File Offset: 0x00015CDA
		internal static string RelationshipManager_UnexpectedNull
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_UnexpectedNull");
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00017AE6 File Offset: 0x00015CE6
		internal static string RelationshipManager_InvalidRelationshipManagerOwner
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_InvalidRelationshipManagerOwner");
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00017AF2 File Offset: 0x00015CF2
		internal static string RelationshipManager_OwnerIsNotSourceType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("RelationshipManager_OwnerIsNotSourceType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00017B14 File Offset: 0x00015D14
		internal static string RelationshipManager_UnexpectedNullContext
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_UnexpectedNullContext");
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00017B20 File Offset: 0x00015D20
		internal static string RelationshipManager_ReferenceAlreadyInitialized(object p0)
		{
			return EntityRes.GetString("RelationshipManager_ReferenceAlreadyInitialized", new object[] { p0 });
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00017B36 File Offset: 0x00015D36
		internal static string RelationshipManager_RelationshipManagerAttached(object p0)
		{
			return EntityRes.GetString("RelationshipManager_RelationshipManagerAttached", new object[] { p0 });
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00017B4C File Offset: 0x00015D4C
		internal static string RelationshipManager_InitializeIsForDeserialization
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_InitializeIsForDeserialization");
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00017B58 File Offset: 0x00015D58
		internal static string RelationshipManager_CollectionAlreadyInitialized(object p0)
		{
			return EntityRes.GetString("RelationshipManager_CollectionAlreadyInitialized", new object[] { p0 });
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00017B6E File Offset: 0x00015D6E
		internal static string RelationshipManager_CollectionRelationshipManagerAttached(object p0)
		{
			return EntityRes.GetString("RelationshipManager_CollectionRelationshipManagerAttached", new object[] { p0 });
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00017B84 File Offset: 0x00015D84
		internal static string RelationshipManager_CollectionInitializeIsForDeserialization
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_CollectionInitializeIsForDeserialization");
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00017B90 File Offset: 0x00015D90
		internal static string RelationshipManager_NavigationPropertyNotFound(object p0)
		{
			return EntityRes.GetString("RelationshipManager_NavigationPropertyNotFound", new object[] { p0 });
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x00017BA6 File Offset: 0x00015DA6
		internal static string RelationshipManager_CannotGetRelatEndForDetachedPocoEntity
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_CannotGetRelatEndForDetachedPocoEntity");
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00017BB2 File Offset: 0x00015DB2
		internal static string ObjectView_CannotReplacetheEntityorRow
		{
			get
			{
				return EntityRes.GetString("ObjectView_CannotReplacetheEntityorRow");
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00017BBE File Offset: 0x00015DBE
		internal static string ObjectView_IndexBasedInsertIsNotSupported
		{
			get
			{
				return EntityRes.GetString("ObjectView_IndexBasedInsertIsNotSupported");
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x00017BCA File Offset: 0x00015DCA
		internal static string ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList
		{
			get
			{
				return EntityRes.GetString("ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList");
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00017BD6 File Offset: 0x00015DD6
		internal static string ObjectView_AddNewOperationNotAllowedOnAbstractBindingList
		{
			get
			{
				return EntityRes.GetString("ObjectView_AddNewOperationNotAllowedOnAbstractBindingList");
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00017BE2 File Offset: 0x00015DE2
		internal static string ObjectView_IncompatibleArgument
		{
			get
			{
				return EntityRes.GetString("ObjectView_IncompatibleArgument");
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00017BEE File Offset: 0x00015DEE
		internal static string ObjectView_CannotResolveTheEntitySet(object p0)
		{
			return EntityRes.GetString("ObjectView_CannotResolveTheEntitySet", new object[] { p0 });
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00017C04 File Offset: 0x00015E04
		internal static string CodeGen_ConstructorNoParameterless(object p0)
		{
			return EntityRes.GetString("CodeGen_ConstructorNoParameterless", new object[] { p0 });
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00017C1A File Offset: 0x00015E1A
		internal static string CodeGen_PropertyDeclaringTypeIsValueType
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyDeclaringTypeIsValueType");
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00017C26 File Offset: 0x00015E26
		internal static string CodeGen_PropertyUnsupportedType
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyUnsupportedType");
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00017C32 File Offset: 0x00015E32
		internal static string CodeGen_PropertyIsIndexed
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyIsIndexed");
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00017C3E File Offset: 0x00015E3E
		internal static string CodeGen_PropertyIsStatic
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyIsStatic");
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00017C4A File Offset: 0x00015E4A
		internal static string CodeGen_PropertyNoGetter
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyNoGetter");
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00017C56 File Offset: 0x00015E56
		internal static string CodeGen_PropertyNoSetter
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyNoSetter");
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00017C62 File Offset: 0x00015E62
		internal static string PocoEntityWrapper_UnableToSetFieldOrProperty(object p0, object p1)
		{
			return EntityRes.GetString("PocoEntityWrapper_UnableToSetFieldOrProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00017C7C File Offset: 0x00015E7C
		internal static string PocoEntityWrapper_UnexpectedTypeForNavigationProperty(object p0, object p1)
		{
			return EntityRes.GetString("PocoEntityWrapper_UnexpectedTypeForNavigationProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00017C96 File Offset: 0x00015E96
		internal static string PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType(object p0, object p1)
		{
			return EntityRes.GetString("PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType", new object[] { p0, p1 });
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00017CB0 File Offset: 0x00015EB0
		internal static string GeneralQueryError
		{
			get
			{
				return EntityRes.GetString("GeneralQueryError");
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00017CBC File Offset: 0x00015EBC
		internal static string CtxAlias
		{
			get
			{
				return EntityRes.GetString("CtxAlias");
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00017CC8 File Offset: 0x00015EC8
		internal static string CtxAliasedNamespaceImport
		{
			get
			{
				return EntityRes.GetString("CtxAliasedNamespaceImport");
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00017CD4 File Offset: 0x00015ED4
		internal static string CtxAnd
		{
			get
			{
				return EntityRes.GetString("CtxAnd");
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00017CE0 File Offset: 0x00015EE0
		internal static string CtxAnyElement
		{
			get
			{
				return EntityRes.GetString("CtxAnyElement");
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00017CEC File Offset: 0x00015EEC
		internal static string CtxApplyClause
		{
			get
			{
				return EntityRes.GetString("CtxApplyClause");
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00017CF8 File Offset: 0x00015EF8
		internal static string CtxBetween
		{
			get
			{
				return EntityRes.GetString("CtxBetween");
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00017D04 File Offset: 0x00015F04
		internal static string CtxCase
		{
			get
			{
				return EntityRes.GetString("CtxCase");
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00017D10 File Offset: 0x00015F10
		internal static string CtxCaseElse
		{
			get
			{
				return EntityRes.GetString("CtxCaseElse");
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00017D1C File Offset: 0x00015F1C
		internal static string CtxCaseWhenThen
		{
			get
			{
				return EntityRes.GetString("CtxCaseWhenThen");
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x00017D28 File Offset: 0x00015F28
		internal static string CtxCast
		{
			get
			{
				return EntityRes.GetString("CtxCast");
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00017D34 File Offset: 0x00015F34
		internal static string CtxCollatedOrderByClauseItem
		{
			get
			{
				return EntityRes.GetString("CtxCollatedOrderByClauseItem");
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00017D40 File Offset: 0x00015F40
		internal static string CtxCollectionTypeDefinition
		{
			get
			{
				return EntityRes.GetString("CtxCollectionTypeDefinition");
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00017D4C File Offset: 0x00015F4C
		internal static string CtxCommandExpression
		{
			get
			{
				return EntityRes.GetString("CtxCommandExpression");
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00017D58 File Offset: 0x00015F58
		internal static string CtxCreateRef
		{
			get
			{
				return EntityRes.GetString("CtxCreateRef");
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00017D64 File Offset: 0x00015F64
		internal static string CtxDeref
		{
			get
			{
				return EntityRes.GetString("CtxDeref");
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00017D70 File Offset: 0x00015F70
		internal static string CtxDivide
		{
			get
			{
				return EntityRes.GetString("CtxDivide");
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00017D7C File Offset: 0x00015F7C
		internal static string CtxElement
		{
			get
			{
				return EntityRes.GetString("CtxElement");
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00017D88 File Offset: 0x00015F88
		internal static string CtxEquals
		{
			get
			{
				return EntityRes.GetString("CtxEquals");
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00017D94 File Offset: 0x00015F94
		internal static string CtxEscapedIdentifier
		{
			get
			{
				return EntityRes.GetString("CtxEscapedIdentifier");
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00017DA0 File Offset: 0x00015FA0
		internal static string CtxExcept
		{
			get
			{
				return EntityRes.GetString("CtxExcept");
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00017DAC File Offset: 0x00015FAC
		internal static string CtxExists
		{
			get
			{
				return EntityRes.GetString("CtxExists");
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00017DB8 File Offset: 0x00015FB8
		internal static string CtxExpressionList
		{
			get
			{
				return EntityRes.GetString("CtxExpressionList");
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00017DC4 File Offset: 0x00015FC4
		internal static string CtxFlatten
		{
			get
			{
				return EntityRes.GetString("CtxFlatten");
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00017DD0 File Offset: 0x00015FD0
		internal static string CtxFromApplyClause
		{
			get
			{
				return EntityRes.GetString("CtxFromApplyClause");
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00017DDC File Offset: 0x00015FDC
		internal static string CtxFromClause
		{
			get
			{
				return EntityRes.GetString("CtxFromClause");
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00017DE8 File Offset: 0x00015FE8
		internal static string CtxFromClauseItem
		{
			get
			{
				return EntityRes.GetString("CtxFromClauseItem");
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00017DF4 File Offset: 0x00015FF4
		internal static string CtxFromClauseList
		{
			get
			{
				return EntityRes.GetString("CtxFromClauseList");
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00017E00 File Offset: 0x00016000
		internal static string CtxFromJoinClause
		{
			get
			{
				return EntityRes.GetString("CtxFromJoinClause");
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00017E0C File Offset: 0x0001600C
		internal static string CtxFunction(object p0)
		{
			return EntityRes.GetString("CtxFunction", new object[] { p0 });
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00017E22 File Offset: 0x00016022
		internal static string CtxFunctionDefinition
		{
			get
			{
				return EntityRes.GetString("CtxFunctionDefinition");
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00017E2E File Offset: 0x0001602E
		internal static string CtxGreaterThan
		{
			get
			{
				return EntityRes.GetString("CtxGreaterThan");
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00017E3A File Offset: 0x0001603A
		internal static string CtxGreaterThanEqual
		{
			get
			{
				return EntityRes.GetString("CtxGreaterThanEqual");
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00017E46 File Offset: 0x00016046
		internal static string CtxGroupByClause
		{
			get
			{
				return EntityRes.GetString("CtxGroupByClause");
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00017E52 File Offset: 0x00016052
		internal static string CtxGroupPartition
		{
			get
			{
				return EntityRes.GetString("CtxGroupPartition");
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00017E5E File Offset: 0x0001605E
		internal static string CtxHavingClause
		{
			get
			{
				return EntityRes.GetString("CtxHavingClause");
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00017E6A File Offset: 0x0001606A
		internal static string CtxIdentifier
		{
			get
			{
				return EntityRes.GetString("CtxIdentifier");
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00017E76 File Offset: 0x00016076
		internal static string CtxIn
		{
			get
			{
				return EntityRes.GetString("CtxIn");
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00017E82 File Offset: 0x00016082
		internal static string CtxIntersect
		{
			get
			{
				return EntityRes.GetString("CtxIntersect");
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00017E8E File Offset: 0x0001608E
		internal static string CtxIsNotNull
		{
			get
			{
				return EntityRes.GetString("CtxIsNotNull");
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00017E9A File Offset: 0x0001609A
		internal static string CtxIsNotOf
		{
			get
			{
				return EntityRes.GetString("CtxIsNotOf");
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00017EA6 File Offset: 0x000160A6
		internal static string CtxIsNull
		{
			get
			{
				return EntityRes.GetString("CtxIsNull");
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00017EB2 File Offset: 0x000160B2
		internal static string CtxIsOf
		{
			get
			{
				return EntityRes.GetString("CtxIsOf");
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00017EBE File Offset: 0x000160BE
		internal static string CtxJoinClause
		{
			get
			{
				return EntityRes.GetString("CtxJoinClause");
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00017ECA File Offset: 0x000160CA
		internal static string CtxJoinOnClause
		{
			get
			{
				return EntityRes.GetString("CtxJoinOnClause");
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00017ED6 File Offset: 0x000160D6
		internal static string CtxKey
		{
			get
			{
				return EntityRes.GetString("CtxKey");
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00017EE2 File Offset: 0x000160E2
		internal static string CtxLessThan
		{
			get
			{
				return EntityRes.GetString("CtxLessThan");
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00017EEE File Offset: 0x000160EE
		internal static string CtxLessThanEqual
		{
			get
			{
				return EntityRes.GetString("CtxLessThanEqual");
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00017EFA File Offset: 0x000160FA
		internal static string CtxLike
		{
			get
			{
				return EntityRes.GetString("CtxLike");
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00017F06 File Offset: 0x00016106
		internal static string CtxLimitSubClause
		{
			get
			{
				return EntityRes.GetString("CtxLimitSubClause");
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00017F12 File Offset: 0x00016112
		internal static string CtxLiteral
		{
			get
			{
				return EntityRes.GetString("CtxLiteral");
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00017F1E File Offset: 0x0001611E
		internal static string CtxMemberAccess
		{
			get
			{
				return EntityRes.GetString("CtxMemberAccess");
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00017F2A File Offset: 0x0001612A
		internal static string CtxMethod
		{
			get
			{
				return EntityRes.GetString("CtxMethod");
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00017F36 File Offset: 0x00016136
		internal static string CtxMinus
		{
			get
			{
				return EntityRes.GetString("CtxMinus");
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00017F42 File Offset: 0x00016142
		internal static string CtxModulus
		{
			get
			{
				return EntityRes.GetString("CtxModulus");
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00017F4E File Offset: 0x0001614E
		internal static string CtxMultiply
		{
			get
			{
				return EntityRes.GetString("CtxMultiply");
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00017F5A File Offset: 0x0001615A
		internal static string CtxMultisetCtor
		{
			get
			{
				return EntityRes.GetString("CtxMultisetCtor");
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00017F66 File Offset: 0x00016166
		internal static string CtxNamespaceImport
		{
			get
			{
				return EntityRes.GetString("CtxNamespaceImport");
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00017F72 File Offset: 0x00016172
		internal static string CtxNamespaceImportList
		{
			get
			{
				return EntityRes.GetString("CtxNamespaceImportList");
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00017F7E File Offset: 0x0001617E
		internal static string CtxNavigate
		{
			get
			{
				return EntityRes.GetString("CtxNavigate");
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00017F8A File Offset: 0x0001618A
		internal static string CtxNot
		{
			get
			{
				return EntityRes.GetString("CtxNot");
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00017F96 File Offset: 0x00016196
		internal static string CtxNotBetween
		{
			get
			{
				return EntityRes.GetString("CtxNotBetween");
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00017FA2 File Offset: 0x000161A2
		internal static string CtxNotEqual
		{
			get
			{
				return EntityRes.GetString("CtxNotEqual");
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00017FAE File Offset: 0x000161AE
		internal static string CtxNotIn
		{
			get
			{
				return EntityRes.GetString("CtxNotIn");
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00017FBA File Offset: 0x000161BA
		internal static string CtxNotLike
		{
			get
			{
				return EntityRes.GetString("CtxNotLike");
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00017FC6 File Offset: 0x000161C6
		internal static string CtxNullLiteral
		{
			get
			{
				return EntityRes.GetString("CtxNullLiteral");
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00017FD2 File Offset: 0x000161D2
		internal static string CtxOfType
		{
			get
			{
				return EntityRes.GetString("CtxOfType");
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00017FDE File Offset: 0x000161DE
		internal static string CtxOfTypeOnly
		{
			get
			{
				return EntityRes.GetString("CtxOfTypeOnly");
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00017FEA File Offset: 0x000161EA
		internal static string CtxOr
		{
			get
			{
				return EntityRes.GetString("CtxOr");
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00017FF6 File Offset: 0x000161F6
		internal static string CtxOrderByClause
		{
			get
			{
				return EntityRes.GetString("CtxOrderByClause");
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00018002 File Offset: 0x00016202
		internal static string CtxOrderByClauseItem
		{
			get
			{
				return EntityRes.GetString("CtxOrderByClauseItem");
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0001800E File Offset: 0x0001620E
		internal static string CtxOverlaps
		{
			get
			{
				return EntityRes.GetString("CtxOverlaps");
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0001801A File Offset: 0x0001621A
		internal static string CtxParen
		{
			get
			{
				return EntityRes.GetString("CtxParen");
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00018026 File Offset: 0x00016226
		internal static string CtxPlus
		{
			get
			{
				return EntityRes.GetString("CtxPlus");
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00018032 File Offset: 0x00016232
		internal static string CtxTypeNameWithTypeSpec
		{
			get
			{
				return EntityRes.GetString("CtxTypeNameWithTypeSpec");
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0001803E File Offset: 0x0001623E
		internal static string CtxQueryExpression
		{
			get
			{
				return EntityRes.GetString("CtxQueryExpression");
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0001804A File Offset: 0x0001624A
		internal static string CtxQueryStatement
		{
			get
			{
				return EntityRes.GetString("CtxQueryStatement");
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00018056 File Offset: 0x00016256
		internal static string CtxRef
		{
			get
			{
				return EntityRes.GetString("CtxRef");
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00018062 File Offset: 0x00016262
		internal static string CtxRefTypeDefinition
		{
			get
			{
				return EntityRes.GetString("CtxRefTypeDefinition");
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0001806E File Offset: 0x0001626E
		internal static string CtxRelationship
		{
			get
			{
				return EntityRes.GetString("CtxRelationship");
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0001807A File Offset: 0x0001627A
		internal static string CtxRelationshipList
		{
			get
			{
				return EntityRes.GetString("CtxRelationshipList");
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00018086 File Offset: 0x00016286
		internal static string CtxRowCtor
		{
			get
			{
				return EntityRes.GetString("CtxRowCtor");
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00018092 File Offset: 0x00016292
		internal static string CtxRowTypeDefinition
		{
			get
			{
				return EntityRes.GetString("CtxRowTypeDefinition");
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0001809E File Offset: 0x0001629E
		internal static string CtxSelectRowClause
		{
			get
			{
				return EntityRes.GetString("CtxSelectRowClause");
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x000180AA File Offset: 0x000162AA
		internal static string CtxSelectValueClause
		{
			get
			{
				return EntityRes.GetString("CtxSelectValueClause");
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x000180B6 File Offset: 0x000162B6
		internal static string CtxSet
		{
			get
			{
				return EntityRes.GetString("CtxSet");
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x000180C2 File Offset: 0x000162C2
		internal static string CtxSimpleIdentifier
		{
			get
			{
				return EntityRes.GetString("CtxSimpleIdentifier");
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x000180CE File Offset: 0x000162CE
		internal static string CtxSkipSubClause
		{
			get
			{
				return EntityRes.GetString("CtxSkipSubClause");
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x000180DA File Offset: 0x000162DA
		internal static string CtxTopSubClause
		{
			get
			{
				return EntityRes.GetString("CtxTopSubClause");
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000180E6 File Offset: 0x000162E6
		internal static string CtxTreat
		{
			get
			{
				return EntityRes.GetString("CtxTreat");
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x000180F2 File Offset: 0x000162F2
		internal static string CtxTypeCtor(object p0)
		{
			return EntityRes.GetString("CtxTypeCtor", new object[] { p0 });
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00018108 File Offset: 0x00016308
		internal static string CtxTypeName
		{
			get
			{
				return EntityRes.GetString("CtxTypeName");
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00018114 File Offset: 0x00016314
		internal static string CtxUnaryMinus
		{
			get
			{
				return EntityRes.GetString("CtxUnaryMinus");
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00018120 File Offset: 0x00016320
		internal static string CtxUnaryPlus
		{
			get
			{
				return EntityRes.GetString("CtxUnaryPlus");
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0001812C File Offset: 0x0001632C
		internal static string CtxUnion
		{
			get
			{
				return EntityRes.GetString("CtxUnion");
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00018138 File Offset: 0x00016338
		internal static string CtxUnionAll
		{
			get
			{
				return EntityRes.GetString("CtxUnionAll");
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x00018144 File Offset: 0x00016344
		internal static string CtxWhereClause
		{
			get
			{
				return EntityRes.GetString("CtxWhereClause");
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00018150 File Offset: 0x00016350
		internal static string CannotConvertNumericLiteral(object p0, object p1)
		{
			return EntityRes.GetString("CannotConvertNumericLiteral", new object[] { p0, p1 });
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0001816A File Offset: 0x0001636A
		internal static string GenericSyntaxError
		{
			get
			{
				return EntityRes.GetString("GenericSyntaxError");
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00018176 File Offset: 0x00016376
		internal static string InFromClause
		{
			get
			{
				return EntityRes.GetString("InFromClause");
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00018182 File Offset: 0x00016382
		internal static string InGroupClause
		{
			get
			{
				return EntityRes.GetString("InGroupClause");
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0001818E File Offset: 0x0001638E
		internal static string InRowCtor
		{
			get
			{
				return EntityRes.GetString("InRowCtor");
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0001819A File Offset: 0x0001639A
		internal static string InSelectProjectionList
		{
			get
			{
				return EntityRes.GetString("InSelectProjectionList");
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x000181A6 File Offset: 0x000163A6
		internal static string InvalidAliasName(object p0)
		{
			return EntityRes.GetString("InvalidAliasName", new object[] { p0 });
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x000181BC File Offset: 0x000163BC
		internal static string InvalidEmptyIdentifier
		{
			get
			{
				return EntityRes.GetString("InvalidEmptyIdentifier");
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x000181C8 File Offset: 0x000163C8
		internal static string InvalidEmptyQuery
		{
			get
			{
				return EntityRes.GetString("InvalidEmptyQuery");
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000181D4 File Offset: 0x000163D4
		internal static string InvalidEscapedIdentifier(object p0)
		{
			return EntityRes.GetString("InvalidEscapedIdentifier", new object[] { p0 });
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000181EA File Offset: 0x000163EA
		internal static string InvalidEscapedIdentifierUnbalanced(object p0)
		{
			return EntityRes.GetString("InvalidEscapedIdentifierUnbalanced", new object[] { p0 });
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x00018200 File Offset: 0x00016400
		internal static string InvalidOperatorSymbol
		{
			get
			{
				return EntityRes.GetString("InvalidOperatorSymbol");
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0001820C File Offset: 0x0001640C
		internal static string InvalidPunctuatorSymbol
		{
			get
			{
				return EntityRes.GetString("InvalidPunctuatorSymbol");
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00018218 File Offset: 0x00016418
		internal static string InvalidSimpleIdentifier(object p0)
		{
			return EntityRes.GetString("InvalidSimpleIdentifier", new object[] { p0 });
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0001822E File Offset: 0x0001642E
		internal static string InvalidSimpleIdentifierNonASCII(object p0)
		{
			return EntityRes.GetString("InvalidSimpleIdentifierNonASCII", new object[] { p0 });
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00018244 File Offset: 0x00016444
		internal static string LocalizedCollection
		{
			get
			{
				return EntityRes.GetString("LocalizedCollection");
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00018250 File Offset: 0x00016450
		internal static string LocalizedColumn
		{
			get
			{
				return EntityRes.GetString("LocalizedColumn");
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0001825C File Offset: 0x0001645C
		internal static string LocalizedComplex
		{
			get
			{
				return EntityRes.GetString("LocalizedComplex");
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00018268 File Offset: 0x00016468
		internal static string LocalizedEntity
		{
			get
			{
				return EntityRes.GetString("LocalizedEntity");
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00018274 File Offset: 0x00016474
		internal static string LocalizedEntityContainerExpression
		{
			get
			{
				return EntityRes.GetString("LocalizedEntityContainerExpression");
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00018280 File Offset: 0x00016480
		internal static string LocalizedFunction
		{
			get
			{
				return EntityRes.GetString("LocalizedFunction");
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0001828C File Offset: 0x0001648C
		internal static string LocalizedInlineFunction
		{
			get
			{
				return EntityRes.GetString("LocalizedInlineFunction");
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00018298 File Offset: 0x00016498
		internal static string LocalizedKeyword
		{
			get
			{
				return EntityRes.GetString("LocalizedKeyword");
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x000182A4 File Offset: 0x000164A4
		internal static string LocalizedLeft
		{
			get
			{
				return EntityRes.GetString("LocalizedLeft");
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x000182B0 File Offset: 0x000164B0
		internal static string LocalizedLine
		{
			get
			{
				return EntityRes.GetString("LocalizedLine");
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x000182BC File Offset: 0x000164BC
		internal static string LocalizedMetadataMemberExpression
		{
			get
			{
				return EntityRes.GetString("LocalizedMetadataMemberExpression");
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x000182C8 File Offset: 0x000164C8
		internal static string LocalizedNamespace
		{
			get
			{
				return EntityRes.GetString("LocalizedNamespace");
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x000182D4 File Offset: 0x000164D4
		internal static string LocalizedNear
		{
			get
			{
				return EntityRes.GetString("LocalizedNear");
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x000182E0 File Offset: 0x000164E0
		internal static string LocalizedPrimitive
		{
			get
			{
				return EntityRes.GetString("LocalizedPrimitive");
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x000182EC File Offset: 0x000164EC
		internal static string LocalizedReference
		{
			get
			{
				return EntityRes.GetString("LocalizedReference");
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x000182F8 File Offset: 0x000164F8
		internal static string LocalizedRight
		{
			get
			{
				return EntityRes.GetString("LocalizedRight");
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00018304 File Offset: 0x00016504
		internal static string LocalizedRow
		{
			get
			{
				return EntityRes.GetString("LocalizedRow");
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00018310 File Offset: 0x00016510
		internal static string LocalizedTerm
		{
			get
			{
				return EntityRes.GetString("LocalizedTerm");
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0001831C File Offset: 0x0001651C
		internal static string LocalizedType
		{
			get
			{
				return EntityRes.GetString("LocalizedType");
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x00018328 File Offset: 0x00016528
		internal static string LocalizedEnumMember
		{
			get
			{
				return EntityRes.GetString("LocalizedEnumMember");
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00018334 File Offset: 0x00016534
		internal static string LocalizedValueExpression
		{
			get
			{
				return EntityRes.GetString("LocalizedValueExpression");
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00018340 File Offset: 0x00016540
		internal static string AliasNameAlreadyUsed(object p0)
		{
			return EntityRes.GetString("AliasNameAlreadyUsed", new object[] { p0 });
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00018356 File Offset: 0x00016556
		internal static string AmbiguousFunctionArguments
		{
			get
			{
				return EntityRes.GetString("AmbiguousFunctionArguments");
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00018362 File Offset: 0x00016562
		internal static string AmbiguousMetadataMemberName(object p0, object p1, object p2)
		{
			return EntityRes.GetString("AmbiguousMetadataMemberName", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00018380 File Offset: 0x00016580
		internal static string ArgumentTypesAreIncompatible(object p0, object p1)
		{
			return EntityRes.GetString("ArgumentTypesAreIncompatible", new object[] { p0, p1 });
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0001839A File Offset: 0x0001659A
		internal static string BetweenLimitsCannotBeUntypedNulls
		{
			get
			{
				return EntityRes.GetString("BetweenLimitsCannotBeUntypedNulls");
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000183A6 File Offset: 0x000165A6
		internal static string BetweenLimitsTypesAreNotCompatible(object p0, object p1)
		{
			return EntityRes.GetString("BetweenLimitsTypesAreNotCompatible", new object[] { p0, p1 });
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000183C0 File Offset: 0x000165C0
		internal static string BetweenLimitsTypesAreNotOrderComparable(object p0, object p1)
		{
			return EntityRes.GetString("BetweenLimitsTypesAreNotOrderComparable", new object[] { p0, p1 });
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000183DA File Offset: 0x000165DA
		internal static string BetweenValueIsNotOrderComparable(object p0, object p1)
		{
			return EntityRes.GetString("BetweenValueIsNotOrderComparable", new object[] { p0, p1 });
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x000183F4 File Offset: 0x000165F4
		internal static string CannotCreateEmptyMultiset
		{
			get
			{
				return EntityRes.GetString("CannotCreateEmptyMultiset");
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00018400 File Offset: 0x00016600
		internal static string CannotCreateMultisetofNulls
		{
			get
			{
				return EntityRes.GetString("CannotCreateMultisetofNulls");
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001840C File Offset: 0x0001660C
		internal static string CannotInstantiateAbstractType(object p0)
		{
			return EntityRes.GetString("CannotInstantiateAbstractType", new object[] { p0 });
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00018422 File Offset: 0x00016622
		internal static string CannotResolveNameToTypeOrFunction(object p0)
		{
			return EntityRes.GetString("CannotResolveNameToTypeOrFunction", new object[] { p0 });
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00018438 File Offset: 0x00016638
		internal static string ConcatBuiltinNotSupported
		{
			get
			{
				return EntityRes.GetString("ConcatBuiltinNotSupported");
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00018444 File Offset: 0x00016644
		internal static string CouldNotResolveIdentifier(object p0)
		{
			return EntityRes.GetString("CouldNotResolveIdentifier", new object[] { p0 });
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0001845A File Offset: 0x0001665A
		internal static string CreateRefTypeIdentifierMustBeASubOrSuperType(object p0, object p1)
		{
			return EntityRes.GetString("CreateRefTypeIdentifierMustBeASubOrSuperType", new object[] { p0, p1 });
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00018474 File Offset: 0x00016674
		internal static string CreateRefTypeIdentifierMustSpecifyAnEntityType(object p0, object p1)
		{
			return EntityRes.GetString("CreateRefTypeIdentifierMustSpecifyAnEntityType", new object[] { p0, p1 });
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001848E File Offset: 0x0001668E
		internal static string DeRefArgIsNotOfRefType(object p0)
		{
			return EntityRes.GetString("DeRefArgIsNotOfRefType", new object[] { p0 });
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000184A4 File Offset: 0x000166A4
		internal static string DuplicatedInlineFunctionOverload(object p0)
		{
			return EntityRes.GetString("DuplicatedInlineFunctionOverload", new object[] { p0 });
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x000184BA File Offset: 0x000166BA
		internal static string ElementOperatorIsNotSupported
		{
			get
			{
				return EntityRes.GetString("ElementOperatorIsNotSupported");
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000184C6 File Offset: 0x000166C6
		internal static string MemberDoesNotBelongToEntityContainer(object p0, object p1)
		{
			return EntityRes.GetString("MemberDoesNotBelongToEntityContainer", new object[] { p0, p1 });
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x000184E0 File Offset: 0x000166E0
		internal static string ExpressionCannotBeNull
		{
			get
			{
				return EntityRes.GetString("ExpressionCannotBeNull");
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000184EC File Offset: 0x000166EC
		internal static string OfTypeExpressionElementTypeMustBeEntityType(object p0, object p1)
		{
			return EntityRes.GetString("OfTypeExpressionElementTypeMustBeEntityType", new object[] { p0, p1 });
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00018506 File Offset: 0x00016706
		internal static string OfTypeExpressionElementTypeMustBeNominalType(object p0, object p1)
		{
			return EntityRes.GetString("OfTypeExpressionElementTypeMustBeNominalType", new object[] { p0, p1 });
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00018520 File Offset: 0x00016720
		internal static string ExpressionMustBeCollection
		{
			get
			{
				return EntityRes.GetString("ExpressionMustBeCollection");
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0001852C File Offset: 0x0001672C
		internal static string ExpressionMustBeNumericType
		{
			get
			{
				return EntityRes.GetString("ExpressionMustBeNumericType");
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00018538 File Offset: 0x00016738
		internal static string ExpressionTypeMustBeBoolean
		{
			get
			{
				return EntityRes.GetString("ExpressionTypeMustBeBoolean");
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00018544 File Offset: 0x00016744
		internal static string ExpressionTypeMustBeEqualComparable
		{
			get
			{
				return EntityRes.GetString("ExpressionTypeMustBeEqualComparable");
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00018550 File Offset: 0x00016750
		internal static string ExpressionTypeMustBeEntityType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ExpressionTypeMustBeEntityType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001856E File Offset: 0x0001676E
		internal static string ExpressionTypeMustBeNominalType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ExpressionTypeMustBeNominalType", new object[] { p0, p1, p2 });
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0001858C File Offset: 0x0001678C
		internal static string ExpressionTypeMustNotBeCollection
		{
			get
			{
				return EntityRes.GetString("ExpressionTypeMustNotBeCollection");
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00018598 File Offset: 0x00016798
		internal static string ExprIsNotValidEntitySetForCreateRef
		{
			get
			{
				return EntityRes.GetString("ExprIsNotValidEntitySetForCreateRef");
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000185A4 File Offset: 0x000167A4
		internal static string FailedToResolveAggregateFunction(object p0)
		{
			return EntityRes.GetString("FailedToResolveAggregateFunction", new object[] { p0 });
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x000185BA File Offset: 0x000167BA
		internal static string GeneralExceptionAsQueryInnerException(object p0)
		{
			return EntityRes.GetString("GeneralExceptionAsQueryInnerException", new object[] { p0 });
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x000185D0 File Offset: 0x000167D0
		internal static string GroupingKeysMustBeEqualComparable
		{
			get
			{
				return EntityRes.GetString("GroupingKeysMustBeEqualComparable");
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x000185DC File Offset: 0x000167DC
		internal static string GroupPartitionOutOfContext
		{
			get
			{
				return EntityRes.GetString("GroupPartitionOutOfContext");
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x000185E8 File Offset: 0x000167E8
		internal static string HavingRequiresGroupClause
		{
			get
			{
				return EntityRes.GetString("HavingRequiresGroupClause");
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x000185F4 File Offset: 0x000167F4
		internal static string ImcompatibleCreateRefKeyElementType
		{
			get
			{
				return EntityRes.GetString("ImcompatibleCreateRefKeyElementType");
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00018600 File Offset: 0x00016800
		internal static string ImcompatibleCreateRefKeyType
		{
			get
			{
				return EntityRes.GetString("ImcompatibleCreateRefKeyType");
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0001860C File Offset: 0x0001680C
		internal static string InnerJoinMustHaveOnPredicate
		{
			get
			{
				return EntityRes.GetString("InnerJoinMustHaveOnPredicate");
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00018618 File Offset: 0x00016818
		internal static string InvalidAssociationTypeForUnion(object p0)
		{
			return EntityRes.GetString("InvalidAssociationTypeForUnion", new object[] { p0 });
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0001862E File Offset: 0x0001682E
		internal static string InvalidCaseResultTypes
		{
			get
			{
				return EntityRes.GetString("InvalidCaseResultTypes");
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0001863A File Offset: 0x0001683A
		internal static string InvalidCaseWhenThenNullType
		{
			get
			{
				return EntityRes.GetString("InvalidCaseWhenThenNullType");
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00018646 File Offset: 0x00016846
		internal static string InvalidCast(object p0, object p1)
		{
			return EntityRes.GetString("InvalidCast", new object[] { p0, p1 });
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00018660 File Offset: 0x00016860
		internal static string InvalidCastExpressionType
		{
			get
			{
				return EntityRes.GetString("InvalidCastExpressionType");
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0001866C File Offset: 0x0001686C
		internal static string InvalidCastType
		{
			get
			{
				return EntityRes.GetString("InvalidCastType");
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00018678 File Offset: 0x00016878
		internal static string InvalidComplexType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidComplexType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0001869A File Offset: 0x0001689A
		internal static string InvalidCreateRefKeyType
		{
			get
			{
				return EntityRes.GetString("InvalidCreateRefKeyType");
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000186A6 File Offset: 0x000168A6
		internal static string InvalidCtorArgumentType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidCtorArgumentType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x000186C4 File Offset: 0x000168C4
		internal static string InvalidCtorUseOnType(object p0)
		{
			return EntityRes.GetString("InvalidCtorUseOnType", new object[] { p0 });
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000186DA File Offset: 0x000168DA
		internal static string InvalidDateTimeOffsetLiteral(object p0)
		{
			return EntityRes.GetString("InvalidDateTimeOffsetLiteral", new object[] { p0 });
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x000186F0 File Offset: 0x000168F0
		internal static string InvalidDay(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDay", new object[] { p0, p1 });
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0001870A File Offset: 0x0001690A
		internal static string InvalidDayInMonth(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDayInMonth", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00018728 File Offset: 0x00016928
		internal static string InvalidDeRefProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDeRefProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00018746 File Offset: 0x00016946
		internal static string InvalidDistinctArgumentInCtor
		{
			get
			{
				return EntityRes.GetString("InvalidDistinctArgumentInCtor");
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00018752 File Offset: 0x00016952
		internal static string InvalidDistinctArgumentInNonAggFunction
		{
			get
			{
				return EntityRes.GetString("InvalidDistinctArgumentInNonAggFunction");
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001875E File Offset: 0x0001695E
		internal static string InvalidEntityRootTypeArgument(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEntityRootTypeArgument", new object[] { p0, p1 });
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00018778 File Offset: 0x00016978
		internal static string InvalidEntityTypeArgument(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidEntityTypeArgument", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001879A File Offset: 0x0001699A
		internal static string InvalidExpressionResolutionClass(object p0, object p1)
		{
			return EntityRes.GetString("InvalidExpressionResolutionClass", new object[] { p0, p1 });
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x000187B4 File Offset: 0x000169B4
		internal static string InvalidFlattenArgument
		{
			get
			{
				return EntityRes.GetString("InvalidFlattenArgument");
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000187C0 File Offset: 0x000169C0
		internal static string InvalidGroupIdentifierReference(object p0)
		{
			return EntityRes.GetString("InvalidGroupIdentifierReference", new object[] { p0 });
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000187D6 File Offset: 0x000169D6
		internal static string InvalidHour(object p0, object p1)
		{
			return EntityRes.GetString("InvalidHour", new object[] { p0, p1 });
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000187F0 File Offset: 0x000169F0
		internal static string InvalidImplicitRelationshipFromEnd(object p0)
		{
			return EntityRes.GetString("InvalidImplicitRelationshipFromEnd", new object[] { p0 });
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00018806 File Offset: 0x00016A06
		internal static string InvalidImplicitRelationshipToEnd(object p0)
		{
			return EntityRes.GetString("InvalidImplicitRelationshipToEnd", new object[] { p0 });
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001881C File Offset: 0x00016A1C
		internal static string InvalidInExprArgs(object p0, object p1)
		{
			return EntityRes.GetString("InvalidInExprArgs", new object[] { p0, p1 });
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00018836 File Offset: 0x00016A36
		internal static string InvalidJoinLeftCorrelation
		{
			get
			{
				return EntityRes.GetString("InvalidJoinLeftCorrelation");
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00018842 File Offset: 0x00016A42
		internal static string InvalidKeyArgument(object p0)
		{
			return EntityRes.GetString("InvalidKeyArgument", new object[] { p0 });
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00018858 File Offset: 0x00016A58
		internal static string InvalidKeyTypeForCollation(object p0)
		{
			return EntityRes.GetString("InvalidKeyTypeForCollation", new object[] { p0 });
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0001886E File Offset: 0x00016A6E
		internal static string InvalidLiteralFormat(object p0, object p1)
		{
			return EntityRes.GetString("InvalidLiteralFormat", new object[] { p0, p1 });
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00018888 File Offset: 0x00016A88
		internal static string InvalidMetadataMemberName
		{
			get
			{
				return EntityRes.GetString("InvalidMetadataMemberName");
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00018894 File Offset: 0x00016A94
		internal static string InvalidMinute(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMinute", new object[] { p0, p1 });
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x000188AE File Offset: 0x00016AAE
		internal static string InvalidModeForWithRelationshipClause
		{
			get
			{
				return EntityRes.GetString("InvalidModeForWithRelationshipClause");
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000188BA File Offset: 0x00016ABA
		internal static string InvalidMonth(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMonth", new object[] { p0, p1 });
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x000188D4 File Offset: 0x00016AD4
		internal static string InvalidNamespaceAlias
		{
			get
			{
				return EntityRes.GetString("InvalidNamespaceAlias");
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x000188E0 File Offset: 0x00016AE0
		internal static string InvalidNullArithmetic
		{
			get
			{
				return EntityRes.GetString("InvalidNullArithmetic");
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x000188EC File Offset: 0x00016AEC
		internal static string InvalidNullComparison
		{
			get
			{
				return EntityRes.GetString("InvalidNullComparison");
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000188F8 File Offset: 0x00016AF8
		internal static string InvalidNullLiteralForNonNullableMember(object p0, object p1)
		{
			return EntityRes.GetString("InvalidNullLiteralForNonNullableMember", new object[] { p0, p1 });
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00018912 File Offset: 0x00016B12
		internal static string InvalidParameterFormat(object p0)
		{
			return EntityRes.GetString("InvalidParameterFormat", new object[] { p0 });
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00018928 File Offset: 0x00016B28
		internal static string InvalidPlaceholderRootTypeArgument(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidPlaceholderRootTypeArgument", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0001894A File Offset: 0x00016B4A
		internal static string InvalidPlaceholderTypeArgument(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("InvalidPlaceholderTypeArgument", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00018976 File Offset: 0x00016B76
		internal static string InvalidPredicateForCrossJoin
		{
			get
			{
				return EntityRes.GetString("InvalidPredicateForCrossJoin");
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00018982 File Offset: 0x00016B82
		internal static string InvalidRelationshipMember(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRelationshipMember", new object[] { p0, p1 });
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0001899C File Offset: 0x00016B9C
		internal static string InvalidMetadataMemberClassResolution(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidMetadataMemberClassResolution", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000189BA File Offset: 0x00016BBA
		internal static string InvalidRootComplexType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRootComplexType", new object[] { p0, p1 });
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000189D4 File Offset: 0x00016BD4
		internal static string InvalidRootRowType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRootRowType", new object[] { p0, p1 });
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000189EE File Offset: 0x00016BEE
		internal static string InvalidRowType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidRowType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00018A10 File Offset: 0x00016C10
		internal static string InvalidSecond(object p0, object p1)
		{
			return EntityRes.GetString("InvalidSecond", new object[] { p0, p1 });
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00018A2A File Offset: 0x00016C2A
		internal static string InvalidSelectValueAliasedExpression
		{
			get
			{
				return EntityRes.GetString("InvalidSelectValueAliasedExpression");
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00018A36 File Offset: 0x00016C36
		internal static string InvalidSelectValueList
		{
			get
			{
				return EntityRes.GetString("InvalidSelectValueList");
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00018A42 File Offset: 0x00016C42
		internal static string InvalidTypeForWithRelationshipClause
		{
			get
			{
				return EntityRes.GetString("InvalidTypeForWithRelationshipClause");
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00018A4E File Offset: 0x00016C4E
		internal static string InvalidUnarySetOpArgument(object p0)
		{
			return EntityRes.GetString("InvalidUnarySetOpArgument", new object[] { p0 });
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00018A64 File Offset: 0x00016C64
		internal static string InvalidUnsignedTypeForUnaryMinusOperation(object p0)
		{
			return EntityRes.GetString("InvalidUnsignedTypeForUnaryMinusOperation", new object[] { p0 });
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00018A7A File Offset: 0x00016C7A
		internal static string InvalidYear(object p0, object p1)
		{
			return EntityRes.GetString("InvalidYear", new object[] { p0, p1 });
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00018A94 File Offset: 0x00016C94
		internal static string InvalidWithRelationshipTargetEndMultiplicity(object p0, object p1)
		{
			return EntityRes.GetString("InvalidWithRelationshipTargetEndMultiplicity", new object[] { p0, p1 });
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00018AAE File Offset: 0x00016CAE
		internal static string InvalidQueryResultType(object p0)
		{
			return EntityRes.GetString("InvalidQueryResultType", new object[] { p0 });
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00018AC4 File Offset: 0x00016CC4
		internal static string IsNullInvalidType
		{
			get
			{
				return EntityRes.GetString("IsNullInvalidType");
			}
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00018AD0 File Offset: 0x00016CD0
		internal static string KeyMustBeCorrelated(object p0)
		{
			return EntityRes.GetString("KeyMustBeCorrelated", new object[] { p0 });
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00018AE6 File Offset: 0x00016CE6
		internal static string LeftSetExpressionArgsMustBeCollection
		{
			get
			{
				return EntityRes.GetString("LeftSetExpressionArgsMustBeCollection");
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00018AF2 File Offset: 0x00016CF2
		internal static string LikeArgMustBeStringType
		{
			get
			{
				return EntityRes.GetString("LikeArgMustBeStringType");
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00018AFE File Offset: 0x00016CFE
		internal static string LiteralTypeNotFoundInMetadata(object p0)
		{
			return EntityRes.GetString("LiteralTypeNotFoundInMetadata", new object[] { p0 });
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00018B14 File Offset: 0x00016D14
		internal static string MalformedSingleQuotePayload
		{
			get
			{
				return EntityRes.GetString("MalformedSingleQuotePayload");
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00018B20 File Offset: 0x00016D20
		internal static string MalformedStringLiteralPayload
		{
			get
			{
				return EntityRes.GetString("MalformedStringLiteralPayload");
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00018B2C File Offset: 0x00016D2C
		internal static string MethodInvocationNotSupported
		{
			get
			{
				return EntityRes.GetString("MethodInvocationNotSupported");
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00018B38 File Offset: 0x00016D38
		internal static string MultipleDefinitionsOfParameter(object p0)
		{
			return EntityRes.GetString("MultipleDefinitionsOfParameter", new object[] { p0 });
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00018B4E File Offset: 0x00016D4E
		internal static string MultipleDefinitionsOfVariable(object p0)
		{
			return EntityRes.GetString("MultipleDefinitionsOfVariable", new object[] { p0 });
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00018B64 File Offset: 0x00016D64
		internal static string MultisetElemsAreNotTypeCompatible
		{
			get
			{
				return EntityRes.GetString("MultisetElemsAreNotTypeCompatible");
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00018B70 File Offset: 0x00016D70
		internal static string NamespaceAliasAlreadyUsed(object p0)
		{
			return EntityRes.GetString("NamespaceAliasAlreadyUsed", new object[] { p0 });
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00018B86 File Offset: 0x00016D86
		internal static string NamespaceAlreadyImported(object p0)
		{
			return EntityRes.GetString("NamespaceAlreadyImported", new object[] { p0 });
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00018B9C File Offset: 0x00016D9C
		internal static string NestedAggregateCannotBeUsedInAggregate(object p0, object p1)
		{
			return EntityRes.GetString("NestedAggregateCannotBeUsedInAggregate", new object[] { p0, p1 });
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00018BB6 File Offset: 0x00016DB6
		internal static string NoAggrFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoAggrFunctionOverloadMatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00018BD4 File Offset: 0x00016DD4
		internal static string NoCanonicalAggrFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoCanonicalAggrFunctionOverloadMatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00018BF2 File Offset: 0x00016DF2
		internal static string NoCanonicalFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoCanonicalFunctionOverloadMatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00018C10 File Offset: 0x00016E10
		internal static string NoFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoFunctionOverloadMatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00018C2E File Offset: 0x00016E2E
		internal static string NotAMemberOfCollection(object p0, object p1)
		{
			return EntityRes.GetString("NotAMemberOfCollection", new object[] { p0, p1 });
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00018C48 File Offset: 0x00016E48
		internal static string NotAMemberOfType(object p0, object p1)
		{
			return EntityRes.GetString("NotAMemberOfType", new object[] { p0, p1 });
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00018C62 File Offset: 0x00016E62
		internal static string NotASuperOrSubType(object p0, object p1)
		{
			return EntityRes.GetString("NotASuperOrSubType", new object[] { p0, p1 });
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00018C7C File Offset: 0x00016E7C
		internal static string NullLiteralCannotBePromotedToCollectionOfNulls
		{
			get
			{
				return EntityRes.GetString("NullLiteralCannotBePromotedToCollectionOfNulls");
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00018C88 File Offset: 0x00016E88
		internal static string NumberOfTypeCtorIsLessThenFormalSpec(object p0)
		{
			return EntityRes.GetString("NumberOfTypeCtorIsLessThenFormalSpec", new object[] { p0 });
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00018C9E File Offset: 0x00016E9E
		internal static string NumberOfTypeCtorIsMoreThenFormalSpec(object p0)
		{
			return EntityRes.GetString("NumberOfTypeCtorIsMoreThenFormalSpec", new object[] { p0 });
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00018CB4 File Offset: 0x00016EB4
		internal static string OrderByKeyIsNotOrderComparable
		{
			get
			{
				return EntityRes.GetString("OrderByKeyIsNotOrderComparable");
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00018CC0 File Offset: 0x00016EC0
		internal static string OfTypeOnlyTypeArgumentCannotBeAbstract(object p0)
		{
			return EntityRes.GetString("OfTypeOnlyTypeArgumentCannotBeAbstract", new object[] { p0 });
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00018CD6 File Offset: 0x00016ED6
		internal static string ParameterTypeNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("ParameterTypeNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00018CF0 File Offset: 0x00016EF0
		internal static string ParameterWasNotDefined(object p0)
		{
			return EntityRes.GetString("ParameterWasNotDefined", new object[] { p0 });
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00018D06 File Offset: 0x00016F06
		internal static string PlaceholderExpressionMustBeCompatibleWithEdm64(object p0, object p1)
		{
			return EntityRes.GetString("PlaceholderExpressionMustBeCompatibleWithEdm64", new object[] { p0, p1 });
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00018D20 File Offset: 0x00016F20
		internal static string PlaceholderExpressionMustBeConstant(object p0)
		{
			return EntityRes.GetString("PlaceholderExpressionMustBeConstant", new object[] { p0 });
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00018D36 File Offset: 0x00016F36
		internal static string PlaceholderExpressionMustBeGreaterThanOrEqualToZero(object p0)
		{
			return EntityRes.GetString("PlaceholderExpressionMustBeGreaterThanOrEqualToZero", new object[] { p0 });
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00018D4C File Offset: 0x00016F4C
		internal static string PlaceholderSetArgTypeIsNotEqualComparable(object p0, object p1, object p2)
		{
			return EntityRes.GetString("PlaceholderSetArgTypeIsNotEqualComparable", new object[] { p0, p1, p2 });
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00018D6A File Offset: 0x00016F6A
		internal static string PlusLeftExpressionInvalidType
		{
			get
			{
				return EntityRes.GetString("PlusLeftExpressionInvalidType");
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00018D76 File Offset: 0x00016F76
		internal static string PlusRightExpressionInvalidType
		{
			get
			{
				return EntityRes.GetString("PlusRightExpressionInvalidType");
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00018D82 File Offset: 0x00016F82
		internal static string PrecisionMustBeGreaterThanScale(object p0, object p1)
		{
			return EntityRes.GetString("PrecisionMustBeGreaterThanScale", new object[] { p0, p1 });
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00018D9C File Offset: 0x00016F9C
		internal static string RefArgIsNotOfEntityType(object p0)
		{
			return EntityRes.GetString("RefArgIsNotOfEntityType", new object[] { p0 });
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00018DB2 File Offset: 0x00016FB2
		internal static string RefTypeIdentifierMustSpecifyAnEntityType(object p0, object p1)
		{
			return EntityRes.GetString("RefTypeIdentifierMustSpecifyAnEntityType", new object[] { p0, p1 });
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00018DCC File Offset: 0x00016FCC
		internal static string RelatedEndExprTypeMustBeReference
		{
			get
			{
				return EntityRes.GetString("RelatedEndExprTypeMustBeReference");
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00018DD8 File Offset: 0x00016FD8
		internal static string RelatedEndExprTypeMustBePromotoableToToEnd(object p0, object p1)
		{
			return EntityRes.GetString("RelatedEndExprTypeMustBePromotoableToToEnd", new object[] { p0, p1 });
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00018DF2 File Offset: 0x00016FF2
		internal static string RelationshipFromEndIsAmbiguos
		{
			get
			{
				return EntityRes.GetString("RelationshipFromEndIsAmbiguos");
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00018DFE File Offset: 0x00016FFE
		internal static string RelationshipTypeExpected(object p0)
		{
			return EntityRes.GetString("RelationshipTypeExpected", new object[] { p0 });
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00018E14 File Offset: 0x00017014
		internal static string RelationshipToEndIsAmbiguos
		{
			get
			{
				return EntityRes.GetString("RelationshipToEndIsAmbiguos");
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00018E20 File Offset: 0x00017020
		internal static string RelationshipTargetMustBeUnique(object p0)
		{
			return EntityRes.GetString("RelationshipTargetMustBeUnique", new object[] { p0 });
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00018E36 File Offset: 0x00017036
		internal static string ResultingExpressionTypeCannotBeNull
		{
			get
			{
				return EntityRes.GetString("ResultingExpressionTypeCannotBeNull");
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00018E42 File Offset: 0x00017042
		internal static string RightSetExpressionArgsMustBeCollection
		{
			get
			{
				return EntityRes.GetString("RightSetExpressionArgsMustBeCollection");
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00018E4E File Offset: 0x0001704E
		internal static string RowCtorElementCannotBeNull
		{
			get
			{
				return EntityRes.GetString("RowCtorElementCannotBeNull");
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00018E5A File Offset: 0x0001705A
		internal static string SelectDistinctMustBeEqualComparable
		{
			get
			{
				return EntityRes.GetString("SelectDistinctMustBeEqualComparable");
			}
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00018E66 File Offset: 0x00017066
		internal static string SourceTypeMustBePromotoableToFromEndRelationType(object p0, object p1)
		{
			return EntityRes.GetString("SourceTypeMustBePromotoableToFromEndRelationType", new object[] { p0, p1 });
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00018E80 File Offset: 0x00017080
		internal static string TopAndLimitCannotCoexist
		{
			get
			{
				return EntityRes.GetString("TopAndLimitCannotCoexist");
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00018E8C File Offset: 0x0001708C
		internal static string TopAndSkipCannotCoexist
		{
			get
			{
				return EntityRes.GetString("TopAndSkipCannotCoexist");
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00018E98 File Offset: 0x00017098
		internal static string TypeDoesNotSupportSpec(object p0)
		{
			return EntityRes.GetString("TypeDoesNotSupportSpec", new object[] { p0 });
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00018EAE File Offset: 0x000170AE
		internal static string TypeDoesNotSupportFacet(object p0, object p1)
		{
			return EntityRes.GetString("TypeDoesNotSupportFacet", new object[] { p0, p1 });
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00018EC8 File Offset: 0x000170C8
		internal static string TypeArgumentCountMismatch(object p0, object p1)
		{
			return EntityRes.GetString("TypeArgumentCountMismatch", new object[] { p0, p1 });
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00018EE2 File Offset: 0x000170E2
		internal static string TypeArgumentMustBeLiteral
		{
			get
			{
				return EntityRes.GetString("TypeArgumentMustBeLiteral");
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00018EEE File Offset: 0x000170EE
		internal static string TypeArgumentBelowMin(object p0)
		{
			return EntityRes.GetString("TypeArgumentBelowMin", new object[] { p0 });
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00018F04 File Offset: 0x00017104
		internal static string TypeArgumentExceedsMax(object p0)
		{
			return EntityRes.GetString("TypeArgumentExceedsMax", new object[] { p0 });
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00018F1A File Offset: 0x0001711A
		internal static string TypeArgumentIsNotValid
		{
			get
			{
				return EntityRes.GetString("TypeArgumentIsNotValid");
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00018F26 File Offset: 0x00017126
		internal static string TypeKindMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("TypeKindMismatch", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00018F48 File Offset: 0x00017148
		internal static string TypeMustBeInheritableType
		{
			get
			{
				return EntityRes.GetString("TypeMustBeInheritableType");
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00018F54 File Offset: 0x00017154
		internal static string TypeMustBeEntityType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeMustBeEntityType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00018F72 File Offset: 0x00017172
		internal static string TypeMustBeNominalType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeMustBeNominalType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00018F90 File Offset: 0x00017190
		internal static string TypeNameNotFound(object p0)
		{
			return EntityRes.GetString("TypeNameNotFound", new object[] { p0 });
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00018FA6 File Offset: 0x000171A6
		internal static string GroupVarNotFoundInScope
		{
			get
			{
				return EntityRes.GetString("GroupVarNotFoundInScope");
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00018FB2 File Offset: 0x000171B2
		internal static string InvalidArgumentTypeForAggregateFunction
		{
			get
			{
				return EntityRes.GetString("InvalidArgumentTypeForAggregateFunction");
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00018FBE File Offset: 0x000171BE
		internal static string InvalidSavePoint
		{
			get
			{
				return EntityRes.GetString("InvalidSavePoint");
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00018FCA File Offset: 0x000171CA
		internal static string InvalidScopeIndex
		{
			get
			{
				return EntityRes.GetString("InvalidScopeIndex");
			}
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00018FD6 File Offset: 0x000171D6
		internal static string LiteralTypeNotSupported(object p0)
		{
			return EntityRes.GetString("LiteralTypeNotSupported", new object[] { p0 });
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00018FEC File Offset: 0x000171EC
		internal static string ParserFatalError
		{
			get
			{
				return EntityRes.GetString("ParserFatalError");
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00018FF8 File Offset: 0x000171F8
		internal static string ParserInputError
		{
			get
			{
				return EntityRes.GetString("ParserInputError");
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00019004 File Offset: 0x00017204
		internal static string StackOverflowInParser
		{
			get
			{
				return EntityRes.GetString("StackOverflowInParser");
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00019010 File Offset: 0x00017210
		internal static string UnknownAstCommandExpression
		{
			get
			{
				return EntityRes.GetString("UnknownAstCommandExpression");
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0001901C File Offset: 0x0001721C
		internal static string UnknownAstExpressionType
		{
			get
			{
				return EntityRes.GetString("UnknownAstExpressionType");
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00019028 File Offset: 0x00017228
		internal static string UnknownBuiltInAstExpressionType
		{
			get
			{
				return EntityRes.GetString("UnknownBuiltInAstExpressionType");
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00019034 File Offset: 0x00017234
		internal static string UnknownExpressionResolutionClass(object p0)
		{
			return EntityRes.GetString("UnknownExpressionResolutionClass", new object[] { p0 });
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0001904A File Offset: 0x0001724A
		internal static string Cqt_General_UnsupportedExpression(object p0)
		{
			return EntityRes.GetString("Cqt_General_UnsupportedExpression", new object[] { p0 });
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00019060 File Offset: 0x00017260
		internal static string Cqt_General_PolymorphicTypeRequired(object p0)
		{
			return EntityRes.GetString("Cqt_General_PolymorphicTypeRequired", new object[] { p0 });
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00019076 File Offset: 0x00017276
		internal static string Cqt_General_PolymorphicArgRequired(object p0)
		{
			return EntityRes.GetString("Cqt_General_PolymorphicArgRequired", new object[] { p0 });
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0001908C File Offset: 0x0001728C
		internal static string Cqt_General_MetadataNotReadOnly
		{
			get
			{
				return EntityRes.GetString("Cqt_General_MetadataNotReadOnly");
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00019098 File Offset: 0x00017298
		internal static string Cqt_General_NoProviderBooleanType
		{
			get
			{
				return EntityRes.GetString("Cqt_General_NoProviderBooleanType");
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x000190A4 File Offset: 0x000172A4
		internal static string Cqt_General_NoProviderIntegerType
		{
			get
			{
				return EntityRes.GetString("Cqt_General_NoProviderIntegerType");
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x000190B0 File Offset: 0x000172B0
		internal static string Cqt_General_NoProviderStringType
		{
			get
			{
				return EntityRes.GetString("Cqt_General_NoProviderStringType");
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x000190BC File Offset: 0x000172BC
		internal static string Cqt_Metadata_EdmMemberIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EdmMemberIncorrectSpace");
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x000190C8 File Offset: 0x000172C8
		internal static string Cqt_Metadata_EntitySetEntityContainerNull
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntitySetEntityContainerNull");
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x000190D4 File Offset: 0x000172D4
		internal static string Cqt_Metadata_EntitySetIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntitySetIncorrectSpace");
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x000190E0 File Offset: 0x000172E0
		internal static string Cqt_Metadata_EntityTypeNullKeyMembersInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntityTypeNullKeyMembersInvalid");
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x000190EC File Offset: 0x000172EC
		internal static string Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid");
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x000190F8 File Offset: 0x000172F8
		internal static string Cqt_Metadata_FunctionReturnParameterNull
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_FunctionReturnParameterNull");
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00019104 File Offset: 0x00017304
		internal static string Cqt_Metadata_FunctionIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_FunctionIncorrectSpace");
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00019110 File Offset: 0x00017310
		internal static string Cqt_Metadata_FunctionParameterIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_FunctionParameterIncorrectSpace");
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0001911C File Offset: 0x0001731C
		internal static string Cqt_Metadata_TypeUsageIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_TypeUsageIncorrectSpace");
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x00019128 File Offset: 0x00017328
		internal static string Cqt_Exceptions_InvalidCommandTree
		{
			get
			{
				return EntityRes.GetString("Cqt_Exceptions_InvalidCommandTree");
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00019134 File Offset: 0x00017334
		internal static string Cqt_Util_CheckListEmptyInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Util_CheckListEmptyInvalid");
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00019140 File Offset: 0x00017340
		internal static string Cqt_Util_CheckListDuplicateName(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_Util_CheckListDuplicateName", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0001915E File Offset: 0x0001735E
		internal static string Cqt_ExpressionLink_TypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_ExpressionLink_TypeMismatch", new object[] { p0, p1 });
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00019178 File Offset: 0x00017378
		internal static string Cqt_ExpressionList_IncorrectElementCount
		{
			get
			{
				return EntityRes.GetString("Cqt_ExpressionList_IncorrectElementCount");
			}
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00019184 File Offset: 0x00017384
		internal static string Cqt_Copier_EntityContainerNotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Copier_EntityContainerNotFound", new object[] { p0 });
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0001919A File Offset: 0x0001739A
		internal static string Cqt_Copier_EntitySetNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_EntitySetNotFound", new object[] { p0, p1 });
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x000191B4 File Offset: 0x000173B4
		internal static string Cqt_Copier_FunctionNotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Copier_FunctionNotFound", new object[] { p0 });
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000191CA File Offset: 0x000173CA
		internal static string Cqt_Copier_PropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_PropertyNotFound", new object[] { p0, p1 });
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x000191E4 File Offset: 0x000173E4
		internal static string Cqt_Copier_NavPropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_NavPropertyNotFound", new object[] { p0, p1 });
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x000191FE File Offset: 0x000173FE
		internal static string Cqt_Copier_EndNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_EndNotFound", new object[] { p0, p1 });
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00019218 File Offset: 0x00017418
		internal static string Cqt_Copier_TypeNotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Copier_TypeNotFound", new object[] { p0 });
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0001922E File Offset: 0x0001742E
		internal static string Cqt_CommandTree_InvalidDataSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_CommandTree_InvalidDataSpace");
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0001923A File Offset: 0x0001743A
		internal static string Cqt_CommandTree_InvalidParameterName(object p0)
		{
			return EntityRes.GetString("Cqt_CommandTree_InvalidParameterName", new object[] { p0 });
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00019250 File Offset: 0x00017450
		internal static string Cqt_Validator_InvalidIncompatibleParameterReferences(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_InvalidIncompatibleParameterReferences", new object[] { p0 });
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00019266 File Offset: 0x00017466
		internal static string Cqt_Validator_InvalidOtherWorkspaceMetadata(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_InvalidOtherWorkspaceMetadata", new object[] { p0 });
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0001927C File Offset: 0x0001747C
		internal static string Cqt_Validator_InvalidIncorrectDataSpaceMetadata(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Validator_InvalidIncorrectDataSpaceMetadata", new object[] { p0, p1 });
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00019296 File Offset: 0x00017496
		internal static string Cqt_Factory_NewCollectionInvalidCommonType
		{
			get
			{
				return EntityRes.GetString("Cqt_Factory_NewCollectionInvalidCommonType");
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000192A2 File Offset: 0x000174A2
		internal static string NoSuchProperty(object p0, object p1)
		{
			return EntityRes.GetString("NoSuchProperty", new object[] { p0, p1 });
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x000192BC File Offset: 0x000174BC
		internal static string Cqt_Factory_NoSuchRelationEnd
		{
			get
			{
				return EntityRes.GetString("Cqt_Factory_NoSuchRelationEnd");
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x000192C8 File Offset: 0x000174C8
		internal static string Cqt_Factory_IncompatibleRelationEnds
		{
			get
			{
				return EntityRes.GetString("Cqt_Factory_IncompatibleRelationEnds");
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000192D4 File Offset: 0x000174D4
		internal static string Cqt_Factory_MethodResultTypeNotSupported(object p0)
		{
			return EntityRes.GetString("Cqt_Factory_MethodResultTypeNotSupported", new object[] { p0 });
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x000192EA File Offset: 0x000174EA
		internal static string Cqt_Aggregate_InvalidFunction
		{
			get
			{
				return EntityRes.GetString("Cqt_Aggregate_InvalidFunction");
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x000192F6 File Offset: 0x000174F6
		internal static string Cqt_Binding_CollectionRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Binding_CollectionRequired");
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x00019302 File Offset: 0x00017502
		internal static string Cqt_GroupBinding_CollectionRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_GroupBinding_CollectionRequired");
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0001930E File Offset: 0x0001750E
		internal static string Cqt_Binary_CollectionsRequired(object p0)
		{
			return EntityRes.GetString("Cqt_Binary_CollectionsRequired", new object[] { p0 });
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00019324 File Offset: 0x00017524
		internal static string Cqt_Unary_CollectionRequired(object p0)
		{
			return EntityRes.GetString("Cqt_Unary_CollectionRequired", new object[] { p0 });
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0001933A File Offset: 0x0001753A
		internal static string Cqt_And_BooleanArgumentsRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_And_BooleanArgumentsRequired");
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00019346 File Offset: 0x00017546
		internal static string Cqt_Apply_DuplicateVariableNames
		{
			get
			{
				return EntityRes.GetString("Cqt_Apply_DuplicateVariableNames");
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00019352 File Offset: 0x00017552
		internal static string Cqt_Arithmetic_NumericCommonType
		{
			get
			{
				return EntityRes.GetString("Cqt_Arithmetic_NumericCommonType");
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0001935E File Offset: 0x0001755E
		internal static string Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus(object p0)
		{
			return EntityRes.GetString("Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus", new object[] { p0 });
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x00019374 File Offset: 0x00017574
		internal static string Cqt_Case_WhensMustEqualThens
		{
			get
			{
				return EntityRes.GetString("Cqt_Case_WhensMustEqualThens");
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x00019380 File Offset: 0x00017580
		internal static string Cqt_Case_InvalidResultType
		{
			get
			{
				return EntityRes.GetString("Cqt_Case_InvalidResultType");
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0001938C File Offset: 0x0001758C
		internal static string Cqt_Cast_InvalidCast(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Cast_InvalidCast", new object[] { p0, p1 });
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x000193A6 File Offset: 0x000175A6
		internal static string Cqt_Comparison_ComparableRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Comparison_ComparableRequired");
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x000193B2 File Offset: 0x000175B2
		internal static string Cqt_Constant_InvalidType
		{
			get
			{
				return EntityRes.GetString("Cqt_Constant_InvalidType");
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x000193BE File Offset: 0x000175BE
		internal static string Cqt_Constant_InvalidValueForType(object p0)
		{
			return EntityRes.GetString("Cqt_Constant_InvalidValueForType", new object[] { p0 });
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000193D4 File Offset: 0x000175D4
		internal static string Cqt_Constant_InvalidConstantType(object p0)
		{
			return EntityRes.GetString("Cqt_Constant_InvalidConstantType", new object[] { p0 });
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x000193EA File Offset: 0x000175EA
		internal static string Cqt_Constant_ClrEnumTypeDoesNotMatchEdmEnumType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_Constant_ClrEnumTypeDoesNotMatchEdmEnumType", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00019408 File Offset: 0x00017608
		internal static string Cqt_Distinct_InvalidCollection
		{
			get
			{
				return EntityRes.GetString("Cqt_Distinct_InvalidCollection");
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x00019414 File Offset: 0x00017614
		internal static string Cqt_DeRef_RefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_DeRef_RefRequired");
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00019420 File Offset: 0x00017620
		internal static string Cqt_Element_InvalidArgumentForUnwrapSingleProperty
		{
			get
			{
				return EntityRes.GetString("Cqt_Element_InvalidArgumentForUnwrapSingleProperty");
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0001942C File Offset: 0x0001762C
		internal static string Cqt_Function_VoidResultInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Function_VoidResultInvalid");
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00019438 File Offset: 0x00017638
		internal static string Cqt_Function_NonComposableInExpression
		{
			get
			{
				return EntityRes.GetString("Cqt_Function_NonComposableInExpression");
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00019444 File Offset: 0x00017644
		internal static string Cqt_Function_CommandTextInExpression
		{
			get
			{
				return EntityRes.GetString("Cqt_Function_CommandTextInExpression");
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00019450 File Offset: 0x00017650
		internal static string Cqt_Function_CanonicalFunction_NotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Function_CanonicalFunction_NotFound", new object[] { p0 });
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00019466 File Offset: 0x00017666
		internal static string Cqt_Function_CanonicalFunction_AmbiguousMatch(object p0)
		{
			return EntityRes.GetString("Cqt_Function_CanonicalFunction_AmbiguousMatch", new object[] { p0 });
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0001947C File Offset: 0x0001767C
		internal static string Cqt_GetEntityRef_EntityRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_GetEntityRef_EntityRequired");
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x00019488 File Offset: 0x00017688
		internal static string Cqt_GetRefKey_RefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_GetRefKey_RefRequired");
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00019494 File Offset: 0x00017694
		internal static string Cqt_GroupBy_AtLeastOneKeyOrAggregate
		{
			get
			{
				return EntityRes.GetString("Cqt_GroupBy_AtLeastOneKeyOrAggregate");
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x000194A0 File Offset: 0x000176A0
		internal static string Cqt_GroupBy_KeyNotEqualityComparable(object p0)
		{
			return EntityRes.GetString("Cqt_GroupBy_KeyNotEqualityComparable", new object[] { p0 });
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000194B6 File Offset: 0x000176B6
		internal static string Cqt_GroupBy_AggregateColumnExistsAsGroupColumn(object p0)
		{
			return EntityRes.GetString("Cqt_GroupBy_AggregateColumnExistsAsGroupColumn", new object[] { p0 });
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x000194CC File Offset: 0x000176CC
		internal static string Cqt_GroupBy_MoreThanOneGroupAggregate
		{
			get
			{
				return EntityRes.GetString("Cqt_GroupBy_MoreThanOneGroupAggregate");
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x000194D8 File Offset: 0x000176D8
		internal static string Cqt_CrossJoin_AtLeastTwoInputs
		{
			get
			{
				return EntityRes.GetString("Cqt_CrossJoin_AtLeastTwoInputs");
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000194E4 File Offset: 0x000176E4
		internal static string Cqt_CrossJoin_DuplicateVariableNames(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_CrossJoin_DuplicateVariableNames", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00019502 File Offset: 0x00017702
		internal static string Cqt_IsNull_CollectionNotAllowed
		{
			get
			{
				return EntityRes.GetString("Cqt_IsNull_CollectionNotAllowed");
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0001950E File Offset: 0x0001770E
		internal static string Cqt_IsNull_InvalidType
		{
			get
			{
				return EntityRes.GetString("Cqt_IsNull_InvalidType");
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0001951A File Offset: 0x0001771A
		internal static string Cqt_InvalidTypeForSetOperation(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_InvalidTypeForSetOperation", new object[] { p0, p1 });
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00019534 File Offset: 0x00017734
		internal static string Cqt_Join_DuplicateVariableNames
		{
			get
			{
				return EntityRes.GetString("Cqt_Join_DuplicateVariableNames");
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00019540 File Offset: 0x00017740
		internal static string Cqt_Limit_ConstantOrParameterRefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Limit_ConstantOrParameterRefRequired");
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0001954C File Offset: 0x0001774C
		internal static string Cqt_Limit_IntegerRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Limit_IntegerRequired");
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00019558 File Offset: 0x00017758
		internal static string Cqt_Limit_NonNegativeLimitRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Limit_NonNegativeLimitRequired");
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00019564 File Offset: 0x00017764
		internal static string Cqt_NewInstance_CollectionTypeRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_NewInstance_CollectionTypeRequired");
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x00019570 File Offset: 0x00017770
		internal static string Cqt_NewInstance_StructuralTypeRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_NewInstance_StructuralTypeRequired");
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0001957C File Offset: 0x0001777C
		internal static string Cqt_NewInstance_CannotInstantiateMemberlessType(object p0)
		{
			return EntityRes.GetString("Cqt_NewInstance_CannotInstantiateMemberlessType", new object[] { p0 });
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00019592 File Offset: 0x00017792
		internal static string Cqt_NewInstance_CannotInstantiateAbstractType(object p0)
		{
			return EntityRes.GetString("Cqt_NewInstance_CannotInstantiateAbstractType", new object[] { p0 });
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x000195A8 File Offset: 0x000177A8
		internal static string Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid
		{
			get
			{
				return EntityRes.GetString("Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid");
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x000195B4 File Offset: 0x000177B4
		internal static string Cqt_Not_BooleanArgumentRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Not_BooleanArgumentRequired");
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x000195C0 File Offset: 0x000177C0
		internal static string Cqt_Or_BooleanArgumentsRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Or_BooleanArgumentsRequired");
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x000195CC File Offset: 0x000177CC
		internal static string Cqt_In_SameResultTypeRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_In_SameResultTypeRequired");
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x000195D8 File Offset: 0x000177D8
		internal static string Cqt_Property_InstanceRequiredForInstance
		{
			get
			{
				return EntityRes.GetString("Cqt_Property_InstanceRequiredForInstance");
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x000195E4 File Offset: 0x000177E4
		internal static string Cqt_Ref_PolymorphicArgRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Ref_PolymorphicArgRequired");
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x000195F0 File Offset: 0x000177F0
		internal static string Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship");
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x000195FC File Offset: 0x000177FC
		internal static string Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne");
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x00019608 File Offset: 0x00017808
		internal static string Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd");
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00019614 File Offset: 0x00017814
		internal static string Cqt_RelatedEntityRef_TargetEntityNotRef
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEntityNotRef");
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x00019620 File Offset: 0x00017820
		internal static string Cqt_RelatedEntityRef_TargetEntityNotCompatible
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEntityNotCompatible");
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0001962C File Offset: 0x0001782C
		internal static string Cqt_RelNav_NoCompositions
		{
			get
			{
				return EntityRes.GetString("Cqt_RelNav_NoCompositions");
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00019638 File Offset: 0x00017838
		internal static string Cqt_RelNav_WrongSourceType(object p0)
		{
			return EntityRes.GetString("Cqt_RelNav_WrongSourceType", new object[] { p0 });
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0001964E File Offset: 0x0001784E
		internal static string Cqt_Skip_ConstantOrParameterRefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Skip_ConstantOrParameterRefRequired");
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0001965A File Offset: 0x0001785A
		internal static string Cqt_Skip_IntegerRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Skip_IntegerRequired");
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x00019666 File Offset: 0x00017866
		internal static string Cqt_Skip_NonNegativeCountRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Skip_NonNegativeCountRequired");
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00019672 File Offset: 0x00017872
		internal static string Cqt_Sort_NonStringCollationInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Sort_NonStringCollationInvalid");
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0001967E File Offset: 0x0001787E
		internal static string Cqt_Sort_OrderComparable
		{
			get
			{
				return EntityRes.GetString("Cqt_Sort_OrderComparable");
			}
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0001968A File Offset: 0x0001788A
		internal static string Cqt_UDF_FunctionDefinitionGenerationFailed(object p0)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionDefinitionGenerationFailed", new object[] { p0 });
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000196A0 File Offset: 0x000178A0
		internal static string Cqt_UDF_FunctionDefinitionWithCircularReference(object p0)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionDefinitionWithCircularReference", new object[] { p0 });
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000196B6 File Offset: 0x000178B6
		internal static string Cqt_UDF_FunctionDefinitionResultTypeMismatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionDefinitionResultTypeMismatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x000196D4 File Offset: 0x000178D4
		internal static string Cqt_UDF_FunctionHasNoDefinition(object p0)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionHasNoDefinition", new object[] { p0 });
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000196EA File Offset: 0x000178EA
		internal static string Cqt_Validator_VarRefInvalid(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_VarRefInvalid", new object[] { p0 });
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00019700 File Offset: 0x00017900
		internal static string Cqt_Validator_VarRefTypeMismatch(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_VarRefTypeMismatch", new object[] { p0 });
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00019716 File Offset: 0x00017916
		internal static string Iqt_General_UnsupportedOp(object p0)
		{
			return EntityRes.GetString("Iqt_General_UnsupportedOp", new object[] { p0 });
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0001972C File Offset: 0x0001792C
		internal static string Iqt_CTGen_UnexpectedAggregate
		{
			get
			{
				return EntityRes.GetString("Iqt_CTGen_UnexpectedAggregate");
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00019738 File Offset: 0x00017938
		internal static string Iqt_CTGen_UnexpectedVarDefList
		{
			get
			{
				return EntityRes.GetString("Iqt_CTGen_UnexpectedVarDefList");
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00019744 File Offset: 0x00017944
		internal static string Iqt_CTGen_UnexpectedVarDef
		{
			get
			{
				return EntityRes.GetString("Iqt_CTGen_UnexpectedVarDef");
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00019750 File Offset: 0x00017950
		internal static string ADP_MustUseSequentialAccess
		{
			get
			{
				return EntityRes.GetString("ADP_MustUseSequentialAccess");
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0001975C File Offset: 0x0001795C
		internal static string ADP_ProviderDoesNotSupportCommandTrees
		{
			get
			{
				return EntityRes.GetString("ADP_ProviderDoesNotSupportCommandTrees");
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00019768 File Offset: 0x00017968
		internal static string ADP_ClosedDataReaderError
		{
			get
			{
				return EntityRes.GetString("ADP_ClosedDataReaderError");
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00019774 File Offset: 0x00017974
		internal static string ADP_DataReaderClosed(object p0)
		{
			return EntityRes.GetString("ADP_DataReaderClosed", new object[] { p0 });
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0001978A File Offset: 0x0001798A
		internal static string ADP_ImplicitlyClosedDataReaderError
		{
			get
			{
				return EntityRes.GetString("ADP_ImplicitlyClosedDataReaderError");
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00019796 File Offset: 0x00017996
		internal static string ADP_NoData
		{
			get
			{
				return EntityRes.GetString("ADP_NoData");
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x000197A2 File Offset: 0x000179A2
		internal static string ADP_GetSchemaTableIsNotSupported
		{
			get
			{
				return EntityRes.GetString("ADP_GetSchemaTableIsNotSupported");
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x000197AE File Offset: 0x000179AE
		internal static string ADP_InvalidDataReaderFieldCountForScalarType
		{
			get
			{
				return EntityRes.GetString("ADP_InvalidDataReaderFieldCountForScalarType");
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000197BA File Offset: 0x000179BA
		internal static string ADP_InvalidDataReaderMissingColumnForType(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDataReaderMissingColumnForType", new object[] { p0, p1 });
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x000197D4 File Offset: 0x000179D4
		internal static string ADP_InvalidDataReaderMissingDiscriminatorColumn(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDataReaderMissingDiscriminatorColumn", new object[] { p0, p1 });
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x000197EE File Offset: 0x000179EE
		internal static string ADP_InvalidDataReaderUnableToDetermineType
		{
			get
			{
				return EntityRes.GetString("ADP_InvalidDataReaderUnableToDetermineType");
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000197FA File Offset: 0x000179FA
		internal static string ADP_InvalidDataReaderUnableToMaterializeNonScalarType(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDataReaderUnableToMaterializeNonScalarType", new object[] { p0, p1 });
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00019814 File Offset: 0x00017A14
		internal static string ADP_KeysRequiredForJoinOverNest(object p0)
		{
			return EntityRes.GetString("ADP_KeysRequiredForJoinOverNest", new object[] { p0 });
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0001982A File Offset: 0x00017A2A
		internal static string ADP_KeysRequiredForNesting
		{
			get
			{
				return EntityRes.GetString("ADP_KeysRequiredForNesting");
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00019836 File Offset: 0x00017A36
		internal static string ADP_NestingNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("ADP_NestingNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00019850 File Offset: 0x00017A50
		internal static string ADP_NoQueryMappingView(object p0, object p1)
		{
			return EntityRes.GetString("ADP_NoQueryMappingView", new object[] { p0, p1 });
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0001986A File Offset: 0x00017A6A
		internal static string ADP_InternalProviderError(object p0)
		{
			return EntityRes.GetString("ADP_InternalProviderError", new object[] { p0 });
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00019880 File Offset: 0x00017A80
		internal static string ADP_InvalidEnumerationValue(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidEnumerationValue", new object[] { p0, p1 });
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0001989A File Offset: 0x00017A9A
		internal static string ADP_InvalidBufferSizeOrIndex(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidBufferSizeOrIndex", new object[] { p0, p1 });
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000198B4 File Offset: 0x00017AB4
		internal static string ADP_InvalidDataLength(object p0)
		{
			return EntityRes.GetString("ADP_InvalidDataLength", new object[] { p0 });
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x000198CA File Offset: 0x00017ACA
		internal static string ADP_InvalidDataType(object p0)
		{
			return EntityRes.GetString("ADP_InvalidDataType", new object[] { p0 });
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x000198E0 File Offset: 0x00017AE0
		internal static string ADP_InvalidDestinationBufferIndex(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDestinationBufferIndex", new object[] { p0, p1 });
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000198FA File Offset: 0x00017AFA
		internal static string ADP_InvalidSourceBufferIndex(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidSourceBufferIndex", new object[] { p0, p1 });
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00019914 File Offset: 0x00017B14
		internal static string ADP_NonSequentialChunkAccess(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ADP_NonSequentialChunkAccess", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00019932 File Offset: 0x00017B32
		internal static string ADP_NonSequentialColumnAccess(object p0, object p1)
		{
			return EntityRes.GetString("ADP_NonSequentialColumnAccess", new object[] { p0, p1 });
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0001994C File Offset: 0x00017B4C
		internal static string ADP_UnknownDataTypeCode(object p0, object p1)
		{
			return EntityRes.GetString("ADP_UnknownDataTypeCode", new object[] { p0, p1 });
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00019966 File Offset: 0x00017B66
		internal static string DataCategory_Data
		{
			get
			{
				return EntityRes.GetString("DataCategory_Data");
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00019972 File Offset: 0x00017B72
		internal static string DbParameter_Direction
		{
			get
			{
				return EntityRes.GetString("DbParameter_Direction");
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0001997E File Offset: 0x00017B7E
		internal static string DbParameter_Size
		{
			get
			{
				return EntityRes.GetString("DbParameter_Size");
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0001998A File Offset: 0x00017B8A
		internal static string DataCategory_Update
		{
			get
			{
				return EntityRes.GetString("DataCategory_Update");
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00019996 File Offset: 0x00017B96
		internal static string DbParameter_SourceColumn
		{
			get
			{
				return EntityRes.GetString("DbParameter_SourceColumn");
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x000199A2 File Offset: 0x00017BA2
		internal static string DbParameter_SourceVersion
		{
			get
			{
				return EntityRes.GetString("DbParameter_SourceVersion");
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x000199AE File Offset: 0x00017BAE
		internal static string ADP_CollectionParameterElementIsNull(object p0)
		{
			return EntityRes.GetString("ADP_CollectionParameterElementIsNull", new object[] { p0 });
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000199C4 File Offset: 0x00017BC4
		internal static string ADP_CollectionParameterElementIsNullOrEmpty(object p0)
		{
			return EntityRes.GetString("ADP_CollectionParameterElementIsNullOrEmpty", new object[] { p0 });
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x000199DA File Offset: 0x00017BDA
		internal static string NonReturnParameterInReturnParameterCollection
		{
			get
			{
				return EntityRes.GetString("NonReturnParameterInReturnParameterCollection");
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x000199E6 File Offset: 0x00017BE6
		internal static string ReturnParameterInInputParameterCollection
		{
			get
			{
				return EntityRes.GetString("ReturnParameterInInputParameterCollection");
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x000199F2 File Offset: 0x00017BF2
		internal static string NullEntitySetsForFunctionReturningMultipleResultSets
		{
			get
			{
				return EntityRes.GetString("NullEntitySetsForFunctionReturningMultipleResultSets");
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x000199FE File Offset: 0x00017BFE
		internal static string NumberOfEntitySetsDoesNotMatchNumberOfReturnParameters
		{
			get
			{
				return EntityRes.GetString("NumberOfEntitySetsDoesNotMatchNumberOfReturnParameters");
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00019A0A File Offset: 0x00017C0A
		internal static string EntityParameterCollectionInvalidParameterName(object p0)
		{
			return EntityRes.GetString("EntityParameterCollectionInvalidParameterName", new object[] { p0 });
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00019A20 File Offset: 0x00017C20
		internal static string EntityParameterCollectionInvalidIndex(object p0, object p1)
		{
			return EntityRes.GetString("EntityParameterCollectionInvalidIndex", new object[] { p0, p1 });
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00019A3A File Offset: 0x00017C3A
		internal static string InvalidEntityParameterType(object p0)
		{
			return EntityRes.GetString("InvalidEntityParameterType", new object[] { p0 });
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00019A50 File Offset: 0x00017C50
		internal static string EntityParameterContainedByAnotherCollection
		{
			get
			{
				return EntityRes.GetString("EntityParameterContainedByAnotherCollection");
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00019A5C File Offset: 0x00017C5C
		internal static string EntityParameterCollectionRemoveInvalidObject
		{
			get
			{
				return EntityRes.GetString("EntityParameterCollectionRemoveInvalidObject");
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00019A68 File Offset: 0x00017C68
		internal static string ADP_ConnectionStringSyntax(object p0)
		{
			return EntityRes.GetString("ADP_ConnectionStringSyntax", new object[] { p0 });
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00019A7E File Offset: 0x00017C7E
		internal static string ExpandingDataDirectoryFailed
		{
			get
			{
				return EntityRes.GetString("ExpandingDataDirectoryFailed");
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00019A8A File Offset: 0x00017C8A
		internal static string ADP_InvalidDataDirectory
		{
			get
			{
				return EntityRes.GetString("ADP_InvalidDataDirectory");
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00019A96 File Offset: 0x00017C96
		internal static string ADP_InvalidMultipartNameDelimiterUsage
		{
			get
			{
				return EntityRes.GetString("ADP_InvalidMultipartNameDelimiterUsage");
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00019AA2 File Offset: 0x00017CA2
		internal static string ADP_InvalidSizeValue(object p0)
		{
			return EntityRes.GetString("ADP_InvalidSizeValue", new object[] { p0 });
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00019AB8 File Offset: 0x00017CB8
		internal static string ADP_KeywordNotSupported(object p0)
		{
			return EntityRes.GetString("ADP_KeywordNotSupported", new object[] { p0 });
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00019ACE File Offset: 0x00017CCE
		internal static string ConstantFacetSpecifiedInSchema(object p0, object p1)
		{
			return EntityRes.GetString("ConstantFacetSpecifiedInSchema", new object[] { p0, p1 });
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00019AE8 File Offset: 0x00017CE8
		internal static string DuplicateAnnotation(object p0, object p1)
		{
			return EntityRes.GetString("DuplicateAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00019B02 File Offset: 0x00017D02
		internal static string EmptyFile(object p0)
		{
			return EntityRes.GetString("EmptyFile", new object[] { p0 });
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00019B18 File Offset: 0x00017D18
		internal static string EmptySchemaTextReader
		{
			get
			{
				return EntityRes.GetString("EmptySchemaTextReader");
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00019B24 File Offset: 0x00017D24
		internal static string EmptyName(object p0)
		{
			return EntityRes.GetString("EmptyName", new object[] { p0 });
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00019B3A File Offset: 0x00017D3A
		internal static string InvalidName(object p0, object p1)
		{
			return EntityRes.GetString("InvalidName", new object[] { p0, p1 });
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00019B54 File Offset: 0x00017D54
		internal static string MissingName
		{
			get
			{
				return EntityRes.GetString("MissingName");
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00019B60 File Offset: 0x00017D60
		internal static string UnexpectedXmlAttribute(object p0)
		{
			return EntityRes.GetString("UnexpectedXmlAttribute", new object[] { p0 });
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00019B76 File Offset: 0x00017D76
		internal static string UnexpectedXmlElement(object p0)
		{
			return EntityRes.GetString("UnexpectedXmlElement", new object[] { p0 });
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00019B8C File Offset: 0x00017D8C
		internal static string TextNotAllowed(object p0)
		{
			return EntityRes.GetString("TextNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00019BA2 File Offset: 0x00017DA2
		internal static string UnexpectedXmlNodeType(object p0)
		{
			return EntityRes.GetString("UnexpectedXmlNodeType", new object[] { p0 });
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00019BB8 File Offset: 0x00017DB8
		internal static string MalformedXml(object p0, object p1)
		{
			return EntityRes.GetString("MalformedXml", new object[] { p0, p1 });
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00019BD2 File Offset: 0x00017DD2
		internal static string ValueNotUnderstood(object p0, object p1)
		{
			return EntityRes.GetString("ValueNotUnderstood", new object[] { p0, p1 });
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00019BEC File Offset: 0x00017DEC
		internal static string EntityContainerAlreadyExists(object p0)
		{
			return EntityRes.GetString("EntityContainerAlreadyExists", new object[] { p0 });
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00019C02 File Offset: 0x00017E02
		internal static string TypeNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("TypeNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00019C18 File Offset: 0x00017E18
		internal static string PropertyNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("PropertyNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00019C2E File Offset: 0x00017E2E
		internal static string DuplicateMemberNameInExtendedEntityContainer(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DuplicateMemberNameInExtendedEntityContainer", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00019C4C File Offset: 0x00017E4C
		internal static string DuplicateEntityContainerMemberName(object p0)
		{
			return EntityRes.GetString("DuplicateEntityContainerMemberName", new object[] { p0 });
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00019C62 File Offset: 0x00017E62
		internal static string PropertyTypeAlreadyDefined(object p0)
		{
			return EntityRes.GetString("PropertyTypeAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00019C78 File Offset: 0x00017E78
		internal static string InvalidSize(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidSize", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00019C9A File Offset: 0x00017E9A
		internal static string InvalidSystemReferenceId(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidSystemReferenceId", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00019CBC File Offset: 0x00017EBC
		internal static string BadNamespaceOrAlias(object p0)
		{
			return EntityRes.GetString("BadNamespaceOrAlias", new object[] { p0 });
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00019CD2 File Offset: 0x00017ED2
		internal static string MissingNamespaceAttribute
		{
			get
			{
				return EntityRes.GetString("MissingNamespaceAttribute");
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00019CDE File Offset: 0x00017EDE
		internal static string InvalidBaseTypeForStructuredType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidBaseTypeForStructuredType", new object[] { p0, p1 });
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00019CF8 File Offset: 0x00017EF8
		internal static string InvalidPropertyType(object p0)
		{
			return EntityRes.GetString("InvalidPropertyType", new object[] { p0 });
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00019D0E File Offset: 0x00017F0E
		internal static string InvalidBaseTypeForItemType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidBaseTypeForItemType", new object[] { p0, p1 });
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00019D28 File Offset: 0x00017F28
		internal static string InvalidBaseTypeForNestedType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidBaseTypeForNestedType", new object[] { p0, p1 });
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00019D42 File Offset: 0x00017F42
		internal static string DefaultNotAllowed
		{
			get
			{
				return EntityRes.GetString("DefaultNotAllowed");
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00019D4E File Offset: 0x00017F4E
		internal static string FacetNotAllowed(object p0, object p1)
		{
			return EntityRes.GetString("FacetNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00019D68 File Offset: 0x00017F68
		internal static string RequiredFacetMissing(object p0, object p1)
		{
			return EntityRes.GetString("RequiredFacetMissing", new object[] { p0, p1 });
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00019D82 File Offset: 0x00017F82
		internal static string InvalidDefaultBinaryWithNoMaxLength(object p0)
		{
			return EntityRes.GetString("InvalidDefaultBinaryWithNoMaxLength", new object[] { p0 });
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00019D98 File Offset: 0x00017F98
		internal static string InvalidDefaultIntegral(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDefaultIntegral", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00019DB6 File Offset: 0x00017FB6
		internal static string InvalidDefaultDateTime(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDefaultDateTime", new object[] { p0, p1 });
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00019DD0 File Offset: 0x00017FD0
		internal static string InvalidDefaultTime(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDefaultTime", new object[] { p0, p1 });
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00019DEA File Offset: 0x00017FEA
		internal static string InvalidDefaultDateTimeOffset(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDefaultDateTimeOffset", new object[] { p0, p1 });
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00019E04 File Offset: 0x00018004
		internal static string InvalidDefaultDecimal(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDefaultDecimal", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00019E22 File Offset: 0x00018022
		internal static string InvalidDefaultFloatingPoint(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDefaultFloatingPoint", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00019E40 File Offset: 0x00018040
		internal static string InvalidDefaultGuid(object p0)
		{
			return EntityRes.GetString("InvalidDefaultGuid", new object[] { p0 });
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00019E56 File Offset: 0x00018056
		internal static string InvalidDefaultBoolean(object p0)
		{
			return EntityRes.GetString("InvalidDefaultBoolean", new object[] { p0 });
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00019E6C File Offset: 0x0001806C
		internal static string DuplicateMemberName(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DuplicateMemberName", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00019E8A File Offset: 0x0001808A
		internal static string GeneratorErrorSeverityError
		{
			get
			{
				return EntityRes.GetString("GeneratorErrorSeverityError");
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x00019E96 File Offset: 0x00018096
		internal static string GeneratorErrorSeverityWarning
		{
			get
			{
				return EntityRes.GetString("GeneratorErrorSeverityWarning");
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00019EA2 File Offset: 0x000180A2
		internal static string GeneratorErrorSeverityUnknown
		{
			get
			{
				return EntityRes.GetString("GeneratorErrorSeverityUnknown");
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x00019EAE File Offset: 0x000180AE
		internal static string SourceUriUnknown
		{
			get
			{
				return EntityRes.GetString("SourceUriUnknown");
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00019EBA File Offset: 0x000180BA
		internal static string BadPrecisionAndScale(object p0, object p1)
		{
			return EntityRes.GetString("BadPrecisionAndScale", new object[] { p0, p1 });
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00019ED4 File Offset: 0x000180D4
		internal static string InvalidNamespaceInUsing(object p0)
		{
			return EntityRes.GetString("InvalidNamespaceInUsing", new object[] { p0 });
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00019EEA File Offset: 0x000180EA
		internal static string BadNavigationPropertyRelationshipNotRelationship(object p0)
		{
			return EntityRes.GetString("BadNavigationPropertyRelationshipNotRelationship", new object[] { p0 });
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x00019F00 File Offset: 0x00018100
		internal static string BadNavigationPropertyRolesCannotBeTheSame
		{
			get
			{
				return EntityRes.GetString("BadNavigationPropertyRolesCannotBeTheSame");
			}
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00019F0C File Offset: 0x0001810C
		internal static string BadNavigationPropertyUndefinedRole(object p0, object p1)
		{
			return EntityRes.GetString("BadNavigationPropertyUndefinedRole", new object[] { p0, p1 });
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00019F26 File Offset: 0x00018126
		internal static string BadNavigationPropertyBadFromRoleType(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("BadNavigationPropertyBadFromRoleType", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00019F4D File Offset: 0x0001814D
		internal static string InvalidMemberNameMatchesTypeName(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMemberNameMatchesTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00019F67 File Offset: 0x00018167
		internal static string InvalidKeyKeyDefinedInBaseClass(object p0, object p1)
		{
			return EntityRes.GetString("InvalidKeyKeyDefinedInBaseClass", new object[] { p0, p1 });
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00019F81 File Offset: 0x00018181
		internal static string InvalidKeyNullablePart(object p0, object p1)
		{
			return EntityRes.GetString("InvalidKeyNullablePart", new object[] { p0, p1 });
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00019F9B File Offset: 0x0001819B
		internal static string InvalidKeyNoProperty(object p0, object p1)
		{
			return EntityRes.GetString("InvalidKeyNoProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00019FB5 File Offset: 0x000181B5
		internal static string KeyMissingOnEntityType(object p0)
		{
			return EntityRes.GetString("KeyMissingOnEntityType", new object[] { p0 });
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x00019FCB File Offset: 0x000181CB
		internal static string InvalidDocumentationBothTextAndStructure
		{
			get
			{
				return EntityRes.GetString("InvalidDocumentationBothTextAndStructure");
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00019FD7 File Offset: 0x000181D7
		internal static string ArgumentOutOfRangeExpectedPostiveNumber(object p0)
		{
			return EntityRes.GetString("ArgumentOutOfRangeExpectedPostiveNumber", new object[] { p0 });
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00019FED File Offset: 0x000181ED
		internal static string ArgumentOutOfRange(object p0)
		{
			return EntityRes.GetString("ArgumentOutOfRange", new object[] { p0 });
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0001A003 File Offset: 0x00018203
		internal static string UnacceptableUri(object p0)
		{
			return EntityRes.GetString("UnacceptableUri", new object[] { p0 });
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0001A019 File Offset: 0x00018219
		internal static string UnexpectedTypeInCollection(object p0, object p1)
		{
			return EntityRes.GetString("UnexpectedTypeInCollection", new object[] { p0, p1 });
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0001A033 File Offset: 0x00018233
		internal static string AllElementsMustBeInSchema
		{
			get
			{
				return EntityRes.GetString("AllElementsMustBeInSchema");
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0001A03F File Offset: 0x0001823F
		internal static string AliasNameIsAlreadyDefined(object p0)
		{
			return EntityRes.GetString("AliasNameIsAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0001A055 File Offset: 0x00018255
		internal static string NeedNotUseSystemNamespaceInUsing(object p0)
		{
			return EntityRes.GetString("NeedNotUseSystemNamespaceInUsing", new object[] { p0 });
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0001A06B File Offset: 0x0001826B
		internal static string CannotUseSystemNamespaceAsAlias(object p0)
		{
			return EntityRes.GetString("CannotUseSystemNamespaceAsAlias", new object[] { p0 });
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0001A081 File Offset: 0x00018281
		internal static string EntitySetTypeHasNoKeys(object p0, object p1)
		{
			return EntityRes.GetString("EntitySetTypeHasNoKeys", new object[] { p0, p1 });
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0001A09B File Offset: 0x0001829B
		internal static string TableAndSchemaAreMutuallyExclusiveWithDefiningQuery(object p0)
		{
			return EntityRes.GetString("TableAndSchemaAreMutuallyExclusiveWithDefiningQuery", new object[] { p0 });
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0001A0B1 File Offset: 0x000182B1
		internal static string UnexpectedRootElement(object p0, object p1, object p2)
		{
			return EntityRes.GetString("UnexpectedRootElement", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0001A0CF File Offset: 0x000182CF
		internal static string UnexpectedRootElementNoNamespace(object p0, object p1, object p2)
		{
			return EntityRes.GetString("UnexpectedRootElementNoNamespace", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0001A0ED File Offset: 0x000182ED
		internal static string ParameterNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("ParameterNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0001A103 File Offset: 0x00018303
		internal static string FunctionWithNonPrimitiveTypeNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("FunctionWithNonPrimitiveTypeNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0001A11D File Offset: 0x0001831D
		internal static string FunctionWithNonEdmPrimitiveTypeNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("FunctionWithNonEdmPrimitiveTypeNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0001A137 File Offset: 0x00018337
		internal static string FunctionImportWithUnsupportedReturnTypeV1(object p0)
		{
			return EntityRes.GetString("FunctionImportWithUnsupportedReturnTypeV1", new object[] { p0 });
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0001A14D File Offset: 0x0001834D
		internal static string FunctionImportWithUnsupportedReturnTypeV1_1(object p0)
		{
			return EntityRes.GetString("FunctionImportWithUnsupportedReturnTypeV1_1", new object[] { p0 });
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0001A163 File Offset: 0x00018363
		internal static string FunctionImportWithUnsupportedReturnTypeV2(object p0)
		{
			return EntityRes.GetString("FunctionImportWithUnsupportedReturnTypeV2", new object[] { p0 });
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0001A179 File Offset: 0x00018379
		internal static string FunctionImportUnknownEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("FunctionImportUnknownEntitySet", new object[] { p0, p1 });
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0001A193 File Offset: 0x00018393
		internal static string FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet(object p0)
		{
			return EntityRes.GetString("FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet", new object[] { p0 });
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0001A1A9 File Offset: 0x000183A9
		internal static string FunctionImportEntityTypeDoesNotMatchEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("FunctionImportEntityTypeDoesNotMatchEntitySet", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0001A1C7 File Offset: 0x000183C7
		internal static string FunctionImportSpecifiesEntitySetButNotEntityType(object p0)
		{
			return EntityRes.GetString("FunctionImportSpecifiesEntitySetButNotEntityType", new object[] { p0 });
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0001A1DD File Offset: 0x000183DD
		internal static string FunctionImportEntitySetAndEntitySetPathDeclared(object p0)
		{
			return EntityRes.GetString("FunctionImportEntitySetAndEntitySetPathDeclared", new object[] { p0 });
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0001A1F3 File Offset: 0x000183F3
		internal static string FunctionImportComposableAndSideEffectingNotAllowed(object p0)
		{
			return EntityRes.GetString("FunctionImportComposableAndSideEffectingNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0001A209 File Offset: 0x00018409
		internal static string FunctionImportCollectionAndRefParametersNotAllowed(object p0)
		{
			return EntityRes.GetString("FunctionImportCollectionAndRefParametersNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0001A21F File Offset: 0x0001841F
		internal static string FunctionImportNonNullableParametersNotAllowed(object p0)
		{
			return EntityRes.GetString("FunctionImportNonNullableParametersNotAllowed", new object[] { p0 });
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0001A235 File Offset: 0x00018435
		internal static string TVFReturnTypeRowHasNonScalarProperty
		{
			get
			{
				return EntityRes.GetString("TVFReturnTypeRowHasNonScalarProperty");
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0001A241 File Offset: 0x00018441
		internal static string DuplicateEntitySetTable(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DuplicateEntitySetTable", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0001A25F File Offset: 0x0001845F
		internal static string ConcurrencyRedefinedOnSubTypeOfEntitySetType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConcurrencyRedefinedOnSubTypeOfEntitySetType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0001A27D File Offset: 0x0001847D
		internal static string SimilarRelationshipEnd(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("SimilarRelationshipEnd", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0001A2A4 File Offset: 0x000184A4
		internal static string InvalidRelationshipEndMultiplicity(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRelationshipEndMultiplicity", new object[] { p0, p1 });
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0001A2BE File Offset: 0x000184BE
		internal static string EndNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EndNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0001A2D4 File Offset: 0x000184D4
		internal static string InvalidRelationshipEndType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRelationshipEndType", new object[] { p0, p1 });
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0001A2EE File Offset: 0x000184EE
		internal static string BadParameterDirection(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("BadParameterDirection", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0001A310 File Offset: 0x00018510
		internal static string BadParameterDirectionForComposableFunctions(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("BadParameterDirectionForComposableFunctions", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0001A332 File Offset: 0x00018532
		internal static string InvalidOperationMultipleEndsInAssociation
		{
			get
			{
				return EntityRes.GetString("InvalidOperationMultipleEndsInAssociation");
			}
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0001A33E File Offset: 0x0001853E
		internal static string InvalidAction(object p0, object p1)
		{
			return EntityRes.GetString("InvalidAction", new object[] { p0, p1 });
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0001A358 File Offset: 0x00018558
		internal static string DuplicationOperation(object p0)
		{
			return EntityRes.GetString("DuplicationOperation", new object[] { p0 });
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0001A36E File Offset: 0x0001856E
		internal static string NotInNamespaceAlias(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NotInNamespaceAlias", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0001A38C File Offset: 0x0001858C
		internal static string NotNamespaceQualified(object p0)
		{
			return EntityRes.GetString("NotNamespaceQualified", new object[] { p0 });
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0001A3A2 File Offset: 0x000185A2
		internal static string NotInNamespaceNoAlias(object p0, object p1)
		{
			return EntityRes.GetString("NotInNamespaceNoAlias", new object[] { p0, p1 });
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0001A3BC File Offset: 0x000185BC
		internal static string InvalidValueForParameterTypeSemanticsAttribute(object p0)
		{
			return EntityRes.GetString("InvalidValueForParameterTypeSemanticsAttribute", new object[] { p0 });
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0001A3D2 File Offset: 0x000185D2
		internal static string DuplicatePropertyNameSpecifiedInEntityKey(object p0, object p1)
		{
			return EntityRes.GetString("DuplicatePropertyNameSpecifiedInEntityKey", new object[] { p0, p1 });
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0001A3EC File Offset: 0x000185EC
		internal static string InvalidEntitySetType(object p0)
		{
			return EntityRes.GetString("InvalidEntitySetType", new object[] { p0 });
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0001A402 File Offset: 0x00018602
		internal static string InvalidRelationshipSetType(object p0)
		{
			return EntityRes.GetString("InvalidRelationshipSetType", new object[] { p0 });
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0001A418 File Offset: 0x00018618
		internal static string InvalidEntityContainerNameInExtends(object p0)
		{
			return EntityRes.GetString("InvalidEntityContainerNameInExtends", new object[] { p0 });
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0001A42E File Offset: 0x0001862E
		internal static string InvalidNamespaceOrAliasSpecified(object p0)
		{
			return EntityRes.GetString("InvalidNamespaceOrAliasSpecified", new object[] { p0 });
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0001A444 File Offset: 0x00018644
		internal static string PrecisionOutOfRange(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("PrecisionOutOfRange", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0001A466 File Offset: 0x00018666
		internal static string ScaleOutOfRange(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ScaleOutOfRange", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0001A488 File Offset: 0x00018688
		internal static string InvalidEntitySetNameReference(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEntitySetNameReference", new object[] { p0, p1 });
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0001A4A2 File Offset: 0x000186A2
		internal static string InvalidEntityEndName(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEntityEndName", new object[] { p0, p1 });
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0001A4BC File Offset: 0x000186BC
		internal static string DuplicateEndName(object p0)
		{
			return EntityRes.GetString("DuplicateEndName", new object[] { p0 });
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0001A4D2 File Offset: 0x000186D2
		internal static string AmbiguousEntityContainerEnd(object p0, object p1)
		{
			return EntityRes.GetString("AmbiguousEntityContainerEnd", new object[] { p0, p1 });
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0001A4EC File Offset: 0x000186EC
		internal static string MissingEntityContainerEnd(object p0, object p1)
		{
			return EntityRes.GetString("MissingEntityContainerEnd", new object[] { p0, p1 });
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0001A506 File Offset: 0x00018706
		internal static string InvalidEndEntitySetTypeMismatch(object p0)
		{
			return EntityRes.GetString("InvalidEndEntitySetTypeMismatch", new object[] { p0 });
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0001A51C File Offset: 0x0001871C
		internal static string InferRelationshipEndFailedNoEntitySetMatch(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("InferRelationshipEndFailedNoEntitySetMatch", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0001A543 File Offset: 0x00018743
		internal static string InferRelationshipEndAmbiguous(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("InferRelationshipEndAmbiguous", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0001A56A File Offset: 0x0001876A
		internal static string InferRelationshipEndGivesAlreadyDefinedEnd(object p0, object p1)
		{
			return EntityRes.GetString("InferRelationshipEndGivesAlreadyDefinedEnd", new object[] { p0, p1 });
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0001A584 File Offset: 0x00018784
		internal static string TooManyAssociationEnds(object p0)
		{
			return EntityRes.GetString("TooManyAssociationEnds", new object[] { p0 });
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0001A59A File Offset: 0x0001879A
		internal static string InvalidEndRoleInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEndRoleInRelationshipConstraint", new object[] { p0, p1 });
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0001A5B4 File Offset: 0x000187B4
		internal static string InvalidFromPropertyInRelationshipConstraint(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidFromPropertyInRelationshipConstraint", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0001A5D2 File Offset: 0x000187D2
		internal static string InvalidToPropertyInRelationshipConstraint(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidToPropertyInRelationshipConstraint", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0001A5F0 File Offset: 0x000187F0
		internal static string InvalidPropertyInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("InvalidPropertyInRelationshipConstraint", new object[] { p0, p1 });
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0001A60A File Offset: 0x0001880A
		internal static string TypeMismatchRelationshipConstraint(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("TypeMismatchRelationshipConstraint", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0001A631 File Offset: 0x00018831
		internal static string InvalidMultiplicityFromRoleUpperBoundMustBeOne(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleUpperBoundMustBeOne", new object[] { p0, p1 });
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0001A64B File Offset: 0x0001884B
		internal static string InvalidMultiplicityFromRoleToPropertyNonNullableV1(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleToPropertyNonNullableV1", new object[] { p0, p1 });
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0001A665 File Offset: 0x00018865
		internal static string InvalidMultiplicityFromRoleToPropertyNonNullableV2(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleToPropertyNonNullableV2", new object[] { p0, p1 });
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0001A67F File Offset: 0x0001887F
		internal static string InvalidMultiplicityFromRoleToPropertyNullableV1(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleToPropertyNullableV1", new object[] { p0, p1 });
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0001A699 File Offset: 0x00018899
		internal static string InvalidMultiplicityToRoleLowerBoundMustBeZero(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityToRoleLowerBoundMustBeZero", new object[] { p0, p1 });
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0001A6B3 File Offset: 0x000188B3
		internal static string InvalidMultiplicityToRoleUpperBoundMustBeOne(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityToRoleUpperBoundMustBeOne", new object[] { p0, p1 });
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0001A6CD File Offset: 0x000188CD
		internal static string InvalidMultiplicityToRoleUpperBoundMustBeMany(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityToRoleUpperBoundMustBeMany", new object[] { p0, p1 });
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0001A6E7 File Offset: 0x000188E7
		internal static string MismatchNumberOfPropertiesinRelationshipConstraint
		{
			get
			{
				return EntityRes.GetString("MismatchNumberOfPropertiesinRelationshipConstraint");
			}
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0001A6F3 File Offset: 0x000188F3
		internal static string MissingConstraintOnRelationshipType(object p0)
		{
			return EntityRes.GetString("MissingConstraintOnRelationshipType", new object[] { p0 });
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0001A709 File Offset: 0x00018909
		internal static string SameRoleReferredInReferentialConstraint(object p0)
		{
			return EntityRes.GetString("SameRoleReferredInReferentialConstraint", new object[] { p0 });
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0001A71F File Offset: 0x0001891F
		internal static string InvalidPrimitiveTypeKind(object p0)
		{
			return EntityRes.GetString("InvalidPrimitiveTypeKind", new object[] { p0 });
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0001A735 File Offset: 0x00018935
		internal static string EntityKeyMustBeScalar(object p0, object p1)
		{
			return EntityRes.GetString("EntityKeyMustBeScalar", new object[] { p0, p1 });
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0001A74F File Offset: 0x0001894F
		internal static string EntityKeyTypeCurrentlyNotSupportedInSSDL(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("EntityKeyTypeCurrentlyNotSupportedInSSDL", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0001A776 File Offset: 0x00018976
		internal static string EntityKeyTypeCurrentlyNotSupported(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityKeyTypeCurrentlyNotSupported", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0001A794 File Offset: 0x00018994
		internal static string MissingFacetDescription(object p0, object p1, object p2)
		{
			return EntityRes.GetString("MissingFacetDescription", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0001A7B2 File Offset: 0x000189B2
		internal static string EndWithManyMultiplicityCannotHaveOperationsSpecified(object p0, object p1)
		{
			return EntityRes.GetString("EndWithManyMultiplicityCannotHaveOperationsSpecified", new object[] { p0, p1 });
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0001A7CC File Offset: 0x000189CC
		internal static string EndWithoutMultiplicity(object p0, object p1)
		{
			return EntityRes.GetString("EndWithoutMultiplicity", new object[] { p0, p1 });
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0001A7E6 File Offset: 0x000189E6
		internal static string EntityContainerCannotExtendItself(object p0)
		{
			return EntityRes.GetString("EntityContainerCannotExtendItself", new object[] { p0 });
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0001A7FC File Offset: 0x000189FC
		internal static string ComposableFunctionOrFunctionImportMustDeclareReturnType
		{
			get
			{
				return EntityRes.GetString("ComposableFunctionOrFunctionImportMustDeclareReturnType");
			}
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0001A808 File Offset: 0x00018A08
		internal static string NonComposableFunctionCannotBeMappedAsComposable(object p0)
		{
			return EntityRes.GetString("NonComposableFunctionCannotBeMappedAsComposable", new object[] { p0 });
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0001A81E File Offset: 0x00018A1E
		internal static string ComposableFunctionImportsReturningEntitiesNotSupported
		{
			get
			{
				return EntityRes.GetString("ComposableFunctionImportsReturningEntitiesNotSupported");
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0001A82A File Offset: 0x00018A2A
		internal static string StructuralTypeMappingsMustNotBeNullForFunctionImportsReturningNonScalarValues
		{
			get
			{
				return EntityRes.GetString("StructuralTypeMappingsMustNotBeNullForFunctionImportsReturningNonScalarValues");
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0001A836 File Offset: 0x00018A36
		internal static string InvalidReturnTypeForComposableFunction
		{
			get
			{
				return EntityRes.GetString("InvalidReturnTypeForComposableFunction");
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0001A842 File Offset: 0x00018A42
		internal static string NonComposableFunctionMustNotDeclareReturnType
		{
			get
			{
				return EntityRes.GetString("NonComposableFunctionMustNotDeclareReturnType");
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0001A84E File Offset: 0x00018A4E
		internal static string CommandTextFunctionsNotComposable
		{
			get
			{
				return EntityRes.GetString("CommandTextFunctionsNotComposable");
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0001A85A File Offset: 0x00018A5A
		internal static string CommandTextFunctionsCannotDeclareStoreFunctionName
		{
			get
			{
				return EntityRes.GetString("CommandTextFunctionsCannotDeclareStoreFunctionName");
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0001A866 File Offset: 0x00018A66
		internal static string NonComposableFunctionHasDisallowedAttribute
		{
			get
			{
				return EntityRes.GetString("NonComposableFunctionHasDisallowedAttribute");
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0001A872 File Offset: 0x00018A72
		internal static string EmptyDefiningQuery
		{
			get
			{
				return EntityRes.GetString("EmptyDefiningQuery");
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0001A87E File Offset: 0x00018A7E
		internal static string EmptyCommandText
		{
			get
			{
				return EntityRes.GetString("EmptyCommandText");
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0001A88A File Offset: 0x00018A8A
		internal static string AmbiguousFunctionOverload(object p0, object p1)
		{
			return EntityRes.GetString("AmbiguousFunctionOverload", new object[] { p0, p1 });
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0001A8A4 File Offset: 0x00018AA4
		internal static string AmbiguousFunctionAndType(object p0, object p1)
		{
			return EntityRes.GetString("AmbiguousFunctionAndType", new object[] { p0, p1 });
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0001A8BE File Offset: 0x00018ABE
		internal static string CycleInTypeHierarchy(object p0)
		{
			return EntityRes.GetString("CycleInTypeHierarchy", new object[] { p0 });
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0001A8D4 File Offset: 0x00018AD4
		internal static string IncorrectProviderManifest
		{
			get
			{
				return EntityRes.GetString("IncorrectProviderManifest");
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0001A8E0 File Offset: 0x00018AE0
		internal static string ComplexTypeAsReturnTypeAndDefinedEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("ComplexTypeAsReturnTypeAndDefinedEntitySet", new object[] { p0, p1 });
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0001A8FA File Offset: 0x00018AFA
		internal static string ComplexTypeAsReturnTypeAndNestedComplexProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ComplexTypeAsReturnTypeAndNestedComplexProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0001A918 File Offset: 0x00018B18
		internal static string FacetsOnNonScalarType(object p0)
		{
			return EntityRes.GetString("FacetsOnNonScalarType", new object[] { p0 });
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0001A92E File Offset: 0x00018B2E
		internal static string FacetDeclarationRequiresTypeAttribute
		{
			get
			{
				return EntityRes.GetString("FacetDeclarationRequiresTypeAttribute");
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0001A93A File Offset: 0x00018B3A
		internal static string TypeMustBeDeclared
		{
			get
			{
				return EntityRes.GetString("TypeMustBeDeclared");
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0001A946 File Offset: 0x00018B46
		internal static string RowTypeWithoutProperty
		{
			get
			{
				return EntityRes.GetString("RowTypeWithoutProperty");
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0001A952 File Offset: 0x00018B52
		internal static string TypeDeclaredAsAttributeAndElement
		{
			get
			{
				return EntityRes.GetString("TypeDeclaredAsAttributeAndElement");
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0001A95E File Offset: 0x00018B5E
		internal static string ReferenceToNonEntityType(object p0)
		{
			return EntityRes.GetString("ReferenceToNonEntityType", new object[] { p0 });
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0001A974 File Offset: 0x00018B74
		internal static string NoCodeGenNamespaceInStructuralAnnotation(object p0)
		{
			return EntityRes.GetString("NoCodeGenNamespaceInStructuralAnnotation", new object[] { p0 });
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0001A98A File Offset: 0x00018B8A
		internal static string CannotLoadDifferentVersionOfSchemaInTheSameItemCollection
		{
			get
			{
				return EntityRes.GetString("CannotLoadDifferentVersionOfSchemaInTheSameItemCollection");
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0001A996 File Offset: 0x00018B96
		internal static string InvalidEnumUnderlyingType
		{
			get
			{
				return EntityRes.GetString("InvalidEnumUnderlyingType");
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0001A9A2 File Offset: 0x00018BA2
		internal static string DuplicateEnumMember
		{
			get
			{
				return EntityRes.GetString("DuplicateEnumMember");
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0001A9AE File Offset: 0x00018BAE
		internal static string CalculatedEnumValueOutOfRange
		{
			get
			{
				return EntityRes.GetString("CalculatedEnumValueOutOfRange");
			}
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0001A9BA File Offset: 0x00018BBA
		internal static string EnumMemberValueOutOfItsUnderylingTypeRange(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EnumMemberValueOutOfItsUnderylingTypeRange", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0001A9D8 File Offset: 0x00018BD8
		internal static string SpatialWithUseStrongSpatialTypesFalse
		{
			get
			{
				return EntityRes.GetString("SpatialWithUseStrongSpatialTypesFalse");
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0001A9E4 File Offset: 0x00018BE4
		internal static string ObjectQuery_QueryBuilder_InvalidResultType(object p0)
		{
			return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidResultType", new object[] { p0 });
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0001A9FA File Offset: 0x00018BFA
		internal static string ObjectQuery_QueryBuilder_InvalidQueryArgument
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidQueryArgument");
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0001AA06 File Offset: 0x00018C06
		internal static string ObjectQuery_QueryBuilder_NotSupportedLinqSource
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_NotSupportedLinqSource");
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0001AA12 File Offset: 0x00018C12
		internal static string ObjectQuery_InvalidConnection
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_InvalidConnection");
			}
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0001AA1E File Offset: 0x00018C1E
		internal static string ObjectQuery_InvalidQueryName(object p0)
		{
			return EntityRes.GetString("ObjectQuery_InvalidQueryName", new object[] { p0 });
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0001AA34 File Offset: 0x00018C34
		internal static string ObjectQuery_UnableToMapResultType
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_UnableToMapResultType");
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0001AA40 File Offset: 0x00018C40
		internal static string ObjectQuery_UnableToMaterializeArray(object p0, object p1)
		{
			return EntityRes.GetString("ObjectQuery_UnableToMaterializeArray", new object[] { p0, p1 });
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0001AA5A File Offset: 0x00018C5A
		internal static string ObjectQuery_UnableToMaterializeArbitaryProjectionType(object p0)
		{
			return EntityRes.GetString("ObjectQuery_UnableToMaterializeArbitaryProjectionType", new object[] { p0 });
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0001AA70 File Offset: 0x00018C70
		internal static string ObjectParameter_InvalidParameterName(object p0)
		{
			return EntityRes.GetString("ObjectParameter_InvalidParameterName", new object[] { p0 });
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0001AA86 File Offset: 0x00018C86
		internal static string ObjectParameter_InvalidParameterType(object p0)
		{
			return EntityRes.GetString("ObjectParameter_InvalidParameterType", new object[] { p0 });
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0001AA9C File Offset: 0x00018C9C
		internal static string ObjectParameterCollection_ParameterNameNotFound(object p0)
		{
			return EntityRes.GetString("ObjectParameterCollection_ParameterNameNotFound", new object[] { p0 });
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0001AAB2 File Offset: 0x00018CB2
		internal static string ObjectParameterCollection_ParameterAlreadyExists(object p0)
		{
			return EntityRes.GetString("ObjectParameterCollection_ParameterAlreadyExists", new object[] { p0 });
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0001AAC8 File Offset: 0x00018CC8
		internal static string ObjectParameterCollection_DuplicateParameterName(object p0)
		{
			return EntityRes.GetString("ObjectParameterCollection_DuplicateParameterName", new object[] { p0 });
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0001AADE File Offset: 0x00018CDE
		internal static string ObjectParameterCollection_ParametersLocked
		{
			get
			{
				return EntityRes.GetString("ObjectParameterCollection_ParametersLocked");
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0001AAEA File Offset: 0x00018CEA
		internal static string ProviderReturnedNullForGetDbInformation(object p0)
		{
			return EntityRes.GetString("ProviderReturnedNullForGetDbInformation", new object[] { p0 });
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0001AB00 File Offset: 0x00018D00
		internal static string ProviderReturnedNullForCreateCommandDefinition
		{
			get
			{
				return EntityRes.GetString("ProviderReturnedNullForCreateCommandDefinition");
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0001AB0C File Offset: 0x00018D0C
		internal static string ProviderDidNotReturnAProviderManifest
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotReturnAProviderManifest");
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0001AB18 File Offset: 0x00018D18
		internal static string ProviderDidNotReturnAProviderManifestToken
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotReturnAProviderManifestToken");
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0001AB24 File Offset: 0x00018D24
		internal static string ProviderDidNotReturnSpatialServices
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotReturnSpatialServices");
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0001AB30 File Offset: 0x00018D30
		internal static string SpatialProviderNotUsable
		{
			get
			{
				return EntityRes.GetString("SpatialProviderNotUsable");
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0001AB3C File Offset: 0x00018D3C
		internal static string ProviderRequiresStoreCommandTree
		{
			get
			{
				return EntityRes.GetString("ProviderRequiresStoreCommandTree");
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0001AB48 File Offset: 0x00018D48
		internal static string ProviderShouldOverrideEscapeLikeArgument
		{
			get
			{
				return EntityRes.GetString("ProviderShouldOverrideEscapeLikeArgument");
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0001AB54 File Offset: 0x00018D54
		internal static string ProviderEscapeLikeArgumentReturnedNull
		{
			get
			{
				return EntityRes.GetString("ProviderEscapeLikeArgumentReturnedNull");
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0001AB60 File Offset: 0x00018D60
		internal static string ProviderDidNotCreateACommandDefinition
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotCreateACommandDefinition");
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0001AB6C File Offset: 0x00018D6C
		internal static string ProviderDoesNotSupportCreateDatabaseScript
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportCreateDatabaseScript");
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0001AB78 File Offset: 0x00018D78
		internal static string ProviderDoesNotSupportCreateDatabase
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportCreateDatabase");
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0001AB84 File Offset: 0x00018D84
		internal static string ProviderDoesNotSupportDatabaseExists
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportDatabaseExists");
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0001AB90 File Offset: 0x00018D90
		internal static string ProviderDoesNotSupportDeleteDatabase
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportDeleteDatabase");
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0001AB9C File Offset: 0x00018D9C
		internal static string Spatial_GeographyValueNotCompatibleWithSpatialServices
		{
			get
			{
				return EntityRes.GetString("Spatial_GeographyValueNotCompatibleWithSpatialServices");
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0001ABA8 File Offset: 0x00018DA8
		internal static string Spatial_GeometryValueNotCompatibleWithSpatialServices
		{
			get
			{
				return EntityRes.GetString("Spatial_GeometryValueNotCompatibleWithSpatialServices");
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0001ABB4 File Offset: 0x00018DB4
		internal static string Spatial_ProviderValueNotCompatibleWithSpatialServices
		{
			get
			{
				return EntityRes.GetString("Spatial_ProviderValueNotCompatibleWithSpatialServices");
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0001ABC0 File Offset: 0x00018DC0
		internal static string Spatial_WellKnownValueSerializationPropertyNotDirectlySettable
		{
			get
			{
				return EntityRes.GetString("Spatial_WellKnownValueSerializationPropertyNotDirectlySettable");
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0001ABCC File Offset: 0x00018DCC
		internal static string EntityConnectionString_Name
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_Name");
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0001ABD8 File Offset: 0x00018DD8
		internal static string EntityConnectionString_Provider
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_Provider");
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0001ABE4 File Offset: 0x00018DE4
		internal static string EntityConnectionString_Metadata
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_Metadata");
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0001ABF0 File Offset: 0x00018DF0
		internal static string EntityConnectionString_ProviderConnectionString
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_ProviderConnectionString");
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0001ABFC File Offset: 0x00018DFC
		internal static string EntityDataCategory_Context
		{
			get
			{
				return EntityRes.GetString("EntityDataCategory_Context");
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0001AC08 File Offset: 0x00018E08
		internal static string EntityDataCategory_NamedConnectionString
		{
			get
			{
				return EntityRes.GetString("EntityDataCategory_NamedConnectionString");
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0001AC14 File Offset: 0x00018E14
		internal static string EntityDataCategory_Source
		{
			get
			{
				return EntityRes.GetString("EntityDataCategory_Source");
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0001AC20 File Offset: 0x00018E20
		internal static string ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection");
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0001AC2C File Offset: 0x00018E2C
		internal static string ObjectQuery_Span_NoNavProp(object p0, object p1)
		{
			return EntityRes.GetString("ObjectQuery_Span_NoNavProp", new object[] { p0, p1 });
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0001AC46 File Offset: 0x00018E46
		internal static string ObjectQuery_Span_SpanPathSyntaxError
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_Span_SpanPathSyntaxError");
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0001AC52 File Offset: 0x00018E52
		internal static string EntityProxyTypeInfo_ProxyHasWrongWrapper
		{
			get
			{
				return EntityRes.GetString("EntityProxyTypeInfo_ProxyHasWrongWrapper");
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0001AC5E File Offset: 0x00018E5E
		internal static string EntityProxyTypeInfo_CannotSetEntityCollectionProperty(object p0, object p1)
		{
			return EntityRes.GetString("EntityProxyTypeInfo_CannotSetEntityCollectionProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0001AC78 File Offset: 0x00018E78
		internal static string EntityProxyTypeInfo_ProxyMetadataIsUnavailable(object p0)
		{
			return EntityRes.GetString("EntityProxyTypeInfo_ProxyMetadataIsUnavailable", new object[] { p0 });
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0001AC8E File Offset: 0x00018E8E
		internal static string EntityProxyTypeInfo_DuplicateOSpaceType(object p0)
		{
			return EntityRes.GetString("EntityProxyTypeInfo_DuplicateOSpaceType", new object[] { p0 });
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0001ACA4 File Offset: 0x00018EA4
		internal static string InvalidEdmMemberInstance
		{
			get
			{
				return EntityRes.GetString("InvalidEdmMemberInstance");
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0001ACB0 File Offset: 0x00018EB0
		internal static string EF6Providers_NoProviderFound(object p0)
		{
			return EntityRes.GetString("EF6Providers_NoProviderFound", new object[] { p0 });
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0001ACC6 File Offset: 0x00018EC6
		internal static string EF6Providers_ProviderTypeMissing(object p0, object p1)
		{
			return EntityRes.GetString("EF6Providers_ProviderTypeMissing", new object[] { p0, p1 });
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0001ACE0 File Offset: 0x00018EE0
		internal static string EF6Providers_InstanceMissing(object p0)
		{
			return EntityRes.GetString("EF6Providers_InstanceMissing", new object[] { p0 });
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0001ACF6 File Offset: 0x00018EF6
		internal static string EF6Providers_NotDbProviderServices(object p0)
		{
			return EntityRes.GetString("EF6Providers_NotDbProviderServices", new object[] { p0 });
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0001AD0C File Offset: 0x00018F0C
		internal static string ProviderInvariantRepeatedInConfig(object p0)
		{
			return EntityRes.GetString("ProviderInvariantRepeatedInConfig", new object[] { p0 });
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0001AD22 File Offset: 0x00018F22
		internal static string DbDependencyResolver_NoProviderInvariantName(object p0)
		{
			return EntityRes.GetString("DbDependencyResolver_NoProviderInvariantName", new object[] { p0 });
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0001AD38 File Offset: 0x00018F38
		internal static string DbDependencyResolver_InvalidKey(object p0, object p1)
		{
			return EntityRes.GetString("DbDependencyResolver_InvalidKey", new object[] { p0, p1 });
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0001AD52 File Offset: 0x00018F52
		internal static string DefaultConfigurationUsedBeforeSet(object p0)
		{
			return EntityRes.GetString("DefaultConfigurationUsedBeforeSet", new object[] { p0 });
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0001AD68 File Offset: 0x00018F68
		internal static string AddHandlerToInUseConfiguration
		{
			get
			{
				return EntityRes.GetString("AddHandlerToInUseConfiguration");
			}
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0001AD74 File Offset: 0x00018F74
		internal static string ConfigurationSetTwice(object p0, object p1)
		{
			return EntityRes.GetString("ConfigurationSetTwice", new object[] { p0, p1 });
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0001AD8E File Offset: 0x00018F8E
		internal static string ConfigurationNotDiscovered(object p0)
		{
			return EntityRes.GetString("ConfigurationNotDiscovered", new object[] { p0 });
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0001ADA4 File Offset: 0x00018FA4
		internal static string SetConfigurationNotDiscovered(object p0, object p1)
		{
			return EntityRes.GetString("SetConfigurationNotDiscovered", new object[] { p0, p1 });
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0001ADBE File Offset: 0x00018FBE
		internal static string MultipleConfigsInAssembly(object p0, object p1)
		{
			return EntityRes.GetString("MultipleConfigsInAssembly", new object[] { p0, p1 });
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0001ADD8 File Offset: 0x00018FD8
		internal static string CreateInstance_BadMigrationsConfigurationType(object p0, object p1)
		{
			return EntityRes.GetString("CreateInstance_BadMigrationsConfigurationType", new object[] { p0, p1 });
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0001ADF2 File Offset: 0x00018FF2
		internal static string CreateInstance_BadSqlGeneratorType(object p0, object p1)
		{
			return EntityRes.GetString("CreateInstance_BadSqlGeneratorType", new object[] { p0, p1 });
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0001AE0C File Offset: 0x0001900C
		internal static string CreateInstance_BadDbConfigurationType(object p0, object p1)
		{
			return EntityRes.GetString("CreateInstance_BadDbConfigurationType", new object[] { p0, p1 });
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0001AE26 File Offset: 0x00019026
		internal static string DbConfigurationTypeNotFound(object p0)
		{
			return EntityRes.GetString("DbConfigurationTypeNotFound", new object[] { p0 });
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0001AE3C File Offset: 0x0001903C
		internal static string DbConfigurationTypeInAttributeNotFound(object p0)
		{
			return EntityRes.GetString("DbConfigurationTypeInAttributeNotFound", new object[] { p0 });
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0001AE52 File Offset: 0x00019052
		internal static string CreateInstance_NoParameterlessConstructor(object p0)
		{
			return EntityRes.GetString("CreateInstance_NoParameterlessConstructor", new object[] { p0 });
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0001AE68 File Offset: 0x00019068
		internal static string CreateInstance_AbstractType(object p0)
		{
			return EntityRes.GetString("CreateInstance_AbstractType", new object[] { p0 });
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0001AE7E File Offset: 0x0001907E
		internal static string CreateInstance_GenericType(object p0)
		{
			return EntityRes.GetString("CreateInstance_GenericType", new object[] { p0 });
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0001AE94 File Offset: 0x00019094
		internal static string ConfigurationLocked(object p0)
		{
			return EntityRes.GetString("ConfigurationLocked", new object[] { p0 });
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0001AEAA File Offset: 0x000190AA
		internal static string EnableMigrationsForContext(object p0)
		{
			return EntityRes.GetString("EnableMigrationsForContext", new object[] { p0 });
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0001AEC0 File Offset: 0x000190C0
		internal static string EnableMigrations_MultipleContexts(object p0)
		{
			return EntityRes.GetString("EnableMigrations_MultipleContexts", new object[] { p0 });
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0001AED6 File Offset: 0x000190D6
		internal static string EnableMigrations_MultipleContextsWithName(object p0, object p1)
		{
			return EntityRes.GetString("EnableMigrations_MultipleContextsWithName", new object[] { p0, p1 });
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0001AEF0 File Offset: 0x000190F0
		internal static string EnableMigrations_NoContext(object p0)
		{
			return EntityRes.GetString("EnableMigrations_NoContext", new object[] { p0 });
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0001AF06 File Offset: 0x00019106
		internal static string EnableMigrations_NoContextWithName(object p0, object p1)
		{
			return EntityRes.GetString("EnableMigrations_NoContextWithName", new object[] { p0, p1 });
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0001AF20 File Offset: 0x00019120
		internal static string MoreThanOneElement
		{
			get
			{
				return EntityRes.GetString("MoreThanOneElement");
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0001AF2C File Offset: 0x0001912C
		internal static string IQueryable_Not_Async(object p0)
		{
			return EntityRes.GetString("IQueryable_Not_Async", new object[] { p0 });
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0001AF42 File Offset: 0x00019142
		internal static string IQueryable_Provider_Not_Async
		{
			get
			{
				return EntityRes.GetString("IQueryable_Provider_Not_Async");
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x0001AF4E File Offset: 0x0001914E
		internal static string EmptySequence
		{
			get
			{
				return EntityRes.GetString("EmptySequence");
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0001AF5A File Offset: 0x0001915A
		internal static string UnableToMoveHistoryTableWithAuto
		{
			get
			{
				return EntityRes.GetString("UnableToMoveHistoryTableWithAuto");
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0001AF66 File Offset: 0x00019166
		internal static string NoMatch
		{
			get
			{
				return EntityRes.GetString("NoMatch");
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0001AF72 File Offset: 0x00019172
		internal static string MoreThanOneMatch
		{
			get
			{
				return EntityRes.GetString("MoreThanOneMatch");
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0001AF7E File Offset: 0x0001917E
		internal static string CreateConfigurationType_NoParameterlessConstructor(object p0)
		{
			return EntityRes.GetString("CreateConfigurationType_NoParameterlessConstructor", new object[] { p0 });
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0001AF94 File Offset: 0x00019194
		internal static string CollectionEmpty(object p0, object p1)
		{
			return EntityRes.GetString("CollectionEmpty", new object[] { p0, p1 });
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0001AFAE File Offset: 0x000191AE
		internal static string DbMigrationsConfiguration_ContextType(object p0)
		{
			return EntityRes.GetString("DbMigrationsConfiguration_ContextType", new object[] { p0 });
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0001AFC4 File Offset: 0x000191C4
		internal static string ContextFactoryContextType(object p0)
		{
			return EntityRes.GetString("ContextFactoryContextType", new object[] { p0 });
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0001AFDA File Offset: 0x000191DA
		internal static string DbMigrationsConfiguration_RootedPath(object p0)
		{
			return EntityRes.GetString("DbMigrationsConfiguration_RootedPath", new object[] { p0 });
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0001AFF0 File Offset: 0x000191F0
		internal static string ModelBuilder_PropertyFilterTypeMustBePrimitive(object p0)
		{
			return EntityRes.GetString("ModelBuilder_PropertyFilterTypeMustBePrimitive", new object[] { p0 });
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0001B006 File Offset: 0x00019206
		internal static string LightweightEntityConfiguration_NonScalarProperty(object p0)
		{
			return EntityRes.GetString("LightweightEntityConfiguration_NonScalarProperty", new object[] { p0 });
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0001B01C File Offset: 0x0001921C
		internal static string MigrationsPendingException(object p0)
		{
			return EntityRes.GetString("MigrationsPendingException", new object[] { p0 });
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0001B032 File Offset: 0x00019232
		internal static string ExecutionStrategy_ExistingTransaction(object p0)
		{
			return EntityRes.GetString("ExecutionStrategy_ExistingTransaction", new object[] { p0 });
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0001B048 File Offset: 0x00019248
		internal static string ExecutionStrategy_MinimumMustBeLessThanMaximum(object p0, object p1)
		{
			return EntityRes.GetString("ExecutionStrategy_MinimumMustBeLessThanMaximum", new object[] { p0, p1 });
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0001B062 File Offset: 0x00019262
		internal static string ExecutionStrategy_NegativeDelay(object p0)
		{
			return EntityRes.GetString("ExecutionStrategy_NegativeDelay", new object[] { p0 });
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0001B078 File Offset: 0x00019278
		internal static string ExecutionStrategy_RetryLimitExceeded(object p0, object p1)
		{
			return EntityRes.GetString("ExecutionStrategy_RetryLimitExceeded", new object[] { p0, p1 });
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0001B092 File Offset: 0x00019292
		internal static string BaseTypeNotMappedToFunctions(object p0, object p1)
		{
			return EntityRes.GetString("BaseTypeNotMappedToFunctions", new object[] { p0, p1 });
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0001B0AC File Offset: 0x000192AC
		internal static string InvalidResourceName(object p0)
		{
			return EntityRes.GetString("InvalidResourceName", new object[] { p0 });
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0001B0C2 File Offset: 0x000192C2
		internal static string ModificationFunctionParameterNotFound(object p0, object p1)
		{
			return EntityRes.GetString("ModificationFunctionParameterNotFound", new object[] { p0, p1 });
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0001B0DC File Offset: 0x000192DC
		internal static string EntityClient_CannotOpenBrokenConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotOpenBrokenConnection");
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0001B0E8 File Offset: 0x000192E8
		internal static string ModificationFunctionParameterNotFoundOriginal(object p0, object p1)
		{
			return EntityRes.GetString("ModificationFunctionParameterNotFoundOriginal", new object[] { p0, p1 });
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0001B102 File Offset: 0x00019302
		internal static string ResultBindingNotFound(object p0, object p1)
		{
			return EntityRes.GetString("ResultBindingNotFound", new object[] { p0, p1 });
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0001B11C File Offset: 0x0001931C
		internal static string ConflictingFunctionsMapping(object p0, object p1)
		{
			return EntityRes.GetString("ConflictingFunctionsMapping", new object[] { p0, p1 });
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0001B136 File Offset: 0x00019336
		internal static string DbContext_InvalidTransactionForConnection
		{
			get
			{
				return EntityRes.GetString("DbContext_InvalidTransactionForConnection");
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0001B142 File Offset: 0x00019342
		internal static string DbContext_InvalidTransactionNoConnection
		{
			get
			{
				return EntityRes.GetString("DbContext_InvalidTransactionNoConnection");
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0001B14E File Offset: 0x0001934E
		internal static string DbContext_TransactionAlreadyStarted
		{
			get
			{
				return EntityRes.GetString("DbContext_TransactionAlreadyStarted");
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0001B15A File Offset: 0x0001935A
		internal static string DbContext_TransactionAlreadyEnlistedInUserTransaction
		{
			get
			{
				return EntityRes.GetString("DbContext_TransactionAlreadyEnlistedInUserTransaction");
			}
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0001B166 File Offset: 0x00019366
		internal static string ExecutionStrategy_StreamingNotSupported(object p0)
		{
			return EntityRes.GetString("ExecutionStrategy_StreamingNotSupported", new object[] { p0 });
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0001B17C File Offset: 0x0001937C
		internal static string EdmProperty_InvalidPropertyType(object p0)
		{
			return EntityRes.GetString("EdmProperty_InvalidPropertyType", new object[] { p0 });
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0001B192 File Offset: 0x00019392
		internal static string ConcurrentMethodInvocation
		{
			get
			{
				return EntityRes.GetString("ConcurrentMethodInvocation");
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0001B19E File Offset: 0x0001939E
		internal static string AssociationSet_EndEntityTypeMismatch
		{
			get
			{
				return EntityRes.GetString("AssociationSet_EndEntityTypeMismatch");
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0001B1AA File Offset: 0x000193AA
		internal static string VisitDbInExpressionNotImplemented
		{
			get
			{
				return EntityRes.GetString("VisitDbInExpressionNotImplemented");
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0001B1B6 File Offset: 0x000193B6
		internal static string InvalidColumnBuilderArgument(object p0)
		{
			return EntityRes.GetString("InvalidColumnBuilderArgument", new object[] { p0 });
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x0001B1CC File Offset: 0x000193CC
		internal static string StorageScalarPropertyMapping_OnlyScalarPropertiesAllowed
		{
			get
			{
				return EntityRes.GetString("StorageScalarPropertyMapping_OnlyScalarPropertiesAllowed");
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0001B1D8 File Offset: 0x000193D8
		internal static string StorageComplexPropertyMapping_OnlyComplexPropertyAllowed
		{
			get
			{
				return EntityRes.GetString("StorageComplexPropertyMapping_OnlyComplexPropertyAllowed");
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0001B1E4 File Offset: 0x000193E4
		internal static string MetadataItemErrorsFoundDuringGeneration
		{
			get
			{
				return EntityRes.GetString("MetadataItemErrorsFoundDuringGeneration");
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0001B1F0 File Offset: 0x000193F0
		internal static string AutomaticStaleFunctions(object p0)
		{
			return EntityRes.GetString("AutomaticStaleFunctions", new object[] { p0 });
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0001B206 File Offset: 0x00019406
		internal static string ScaffoldSprocInDownNotSupported
		{
			get
			{
				return EntityRes.GetString("ScaffoldSprocInDownNotSupported");
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0001B212 File Offset: 0x00019412
		internal static string LightweightEntityConfiguration_ConfigurationConflict_ComplexType(object p0, object p1)
		{
			return EntityRes.GetString("LightweightEntityConfiguration_ConfigurationConflict_ComplexType", new object[] { p0, p1 });
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0001B22C File Offset: 0x0001942C
		internal static string LightweightEntityConfiguration_ConfigurationConflict_IgnoreType(object p0, object p1)
		{
			return EntityRes.GetString("LightweightEntityConfiguration_ConfigurationConflict_IgnoreType", new object[] { p0, p1 });
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0001B246 File Offset: 0x00019446
		internal static string AttemptToAddEdmMemberFromWrongDataSpace(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("AttemptToAddEdmMemberFromWrongDataSpace", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0001B268 File Offset: 0x00019468
		internal static string LightweightEntityConfiguration_InvalidNavigationProperty(object p0)
		{
			return EntityRes.GetString("LightweightEntityConfiguration_InvalidNavigationProperty", new object[] { p0 });
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0001B27E File Offset: 0x0001947E
		internal static string LightweightEntityConfiguration_InvalidInverseNavigationProperty(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("LightweightEntityConfiguration_InvalidInverseNavigationProperty", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0001B2A0 File Offset: 0x000194A0
		internal static string LightweightEntityConfiguration_MismatchedInverseNavigationProperty(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("LightweightEntityConfiguration_MismatchedInverseNavigationProperty", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0001B2C2 File Offset: 0x000194C2
		internal static string DuplicateParameterName(object p0)
		{
			return EntityRes.GetString("DuplicateParameterName", new object[] { p0 });
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0001B2D8 File Offset: 0x000194D8
		internal static string CommandLogFailed(object p0, object p1, object p2)
		{
			return EntityRes.GetString("CommandLogFailed", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0001B2F6 File Offset: 0x000194F6
		internal static string CommandLogCanceled(object p0, object p1)
		{
			return EntityRes.GetString("CommandLogCanceled", new object[] { p0, p1 });
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0001B310 File Offset: 0x00019510
		internal static string CommandLogComplete(object p0, object p1, object p2)
		{
			return EntityRes.GetString("CommandLogComplete", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0001B32E File Offset: 0x0001952E
		internal static string CommandLogAsync(object p0, object p1)
		{
			return EntityRes.GetString("CommandLogAsync", new object[] { p0, p1 });
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0001B348 File Offset: 0x00019548
		internal static string CommandLogNonAsync(object p0, object p1)
		{
			return EntityRes.GetString("CommandLogNonAsync", new object[] { p0, p1 });
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0001B362 File Offset: 0x00019562
		internal static string SuppressionAfterExecution
		{
			get
			{
				return EntityRes.GetString("SuppressionAfterExecution");
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0001B36E File Offset: 0x0001956E
		internal static string BadContextTypeForDiscovery(object p0)
		{
			return EntityRes.GetString("BadContextTypeForDiscovery", new object[] { p0 });
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0001B384 File Offset: 0x00019584
		internal static string ErrorGeneratingCommandTree(object p0, object p1)
		{
			return EntityRes.GetString("ErrorGeneratingCommandTree", new object[] { p0, p1 });
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0001B39E File Offset: 0x0001959E
		internal static string LightweightNavigationPropertyConfiguration_IncompatibleMultiplicity(object p0, object p1, object p2)
		{
			return EntityRes.GetString("LightweightNavigationPropertyConfiguration_IncompatibleMultiplicity", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0001B3BC File Offset: 0x000195BC
		internal static string LightweightNavigationPropertyConfiguration_InvalidMultiplicity(object p0)
		{
			return EntityRes.GetString("LightweightNavigationPropertyConfiguration_InvalidMultiplicity", new object[] { p0 });
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0001B3D2 File Offset: 0x000195D2
		internal static string LightweightPrimitivePropertyConfiguration_NonNullableProperty(object p0, object p1)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_NonNullableProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0001B3EC File Offset: 0x000195EC
		internal static string TestDoubleNotImplemented(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TestDoubleNotImplemented", new object[] { p0, p1, p2 });
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0001B40A File Offset: 0x0001960A
		internal static string TestDoublesCannotBeConverted
		{
			get
			{
				return EntityRes.GetString("TestDoublesCannotBeConverted");
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0001B416 File Offset: 0x00019616
		internal static string InvalidNavigationPropertyComplexType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidNavigationPropertyComplexType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0001B434 File Offset: 0x00019634
		internal static string ConventionsConfiguration_InvalidConventionType(object p0)
		{
			return EntityRes.GetString("ConventionsConfiguration_InvalidConventionType", new object[] { p0 });
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0001B44A File Offset: 0x0001964A
		internal static string ConventionsConfiguration_ConventionTypeMissmatch(object p0, object p1)
		{
			return EntityRes.GetString("ConventionsConfiguration_ConventionTypeMissmatch", new object[] { p0, p1 });
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0001B464 File Offset: 0x00019664
		internal static string LightweightPrimitivePropertyConfiguration_DateTimeScale(object p0)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_DateTimeScale", new object[] { p0 });
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0001B47A File Offset: 0x0001967A
		internal static string LightweightPrimitivePropertyConfiguration_DecimalNoScale(object p0)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_DecimalNoScale", new object[] { p0 });
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0001B490 File Offset: 0x00019690
		internal static string LightweightPrimitivePropertyConfiguration_HasPrecisionNonDateTime(object p0)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_HasPrecisionNonDateTime", new object[] { p0 });
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0001B4A6 File Offset: 0x000196A6
		internal static string LightweightPrimitivePropertyConfiguration_HasPrecisionNonDecimal(object p0)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_HasPrecisionNonDecimal", new object[] { p0 });
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0001B4BC File Offset: 0x000196BC
		internal static string LightweightPrimitivePropertyConfiguration_IsRowVersionNonBinary(object p0)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_IsRowVersionNonBinary", new object[] { p0 });
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0001B4D2 File Offset: 0x000196D2
		internal static string LightweightPrimitivePropertyConfiguration_IsUnicodeNonString(object p0)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_IsUnicodeNonString", new object[] { p0 });
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0001B4E8 File Offset: 0x000196E8
		internal static string LightweightPrimitivePropertyConfiguration_NonLength(object p0)
		{
			return EntityRes.GetString("LightweightPrimitivePropertyConfiguration_NonLength", new object[] { p0 });
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0001B4FE File Offset: 0x000196FE
		internal static string UnableToUpgradeHistoryWhenCustomFactory
		{
			get
			{
				return EntityRes.GetString("UnableToUpgradeHistoryWhenCustomFactory");
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0001B50A File Offset: 0x0001970A
		internal static string CommitFailed
		{
			get
			{
				return EntityRes.GetString("CommitFailed");
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0001B516 File Offset: 0x00019716
		internal static string InterceptorTypeNotFound(object p0)
		{
			return EntityRes.GetString("InterceptorTypeNotFound", new object[] { p0 });
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0001B52C File Offset: 0x0001972C
		internal static string InterceptorTypeNotInterceptor(object p0)
		{
			return EntityRes.GetString("InterceptorTypeNotInterceptor", new object[] { p0 });
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0001B542 File Offset: 0x00019742
		internal static string ViewGenContainersNotFound(object p0, object p1)
		{
			return EntityRes.GetString("ViewGenContainersNotFound", new object[] { p0, p1 });
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0001B55C File Offset: 0x0001975C
		internal static string HashCalcContainersNotFound(object p0, object p1)
		{
			return EntityRes.GetString("HashCalcContainersNotFound", new object[] { p0, p1 });
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0001B576 File Offset: 0x00019776
		internal static string ViewGenMultipleContainers
		{
			get
			{
				return EntityRes.GetString("ViewGenMultipleContainers");
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0001B582 File Offset: 0x00019782
		internal static string HashCalcMultipleContainers
		{
			get
			{
				return EntityRes.GetString("HashCalcMultipleContainers");
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0001B58E File Offset: 0x0001978E
		internal static string BadConnectionWrapping
		{
			get
			{
				return EntityRes.GetString("BadConnectionWrapping");
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0001B59A File Offset: 0x0001979A
		internal static string ConnectionClosedLog(object p0, object p1)
		{
			return EntityRes.GetString("ConnectionClosedLog", new object[] { p0, p1 });
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0001B5B4 File Offset: 0x000197B4
		internal static string ConnectionCloseErrorLog(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConnectionCloseErrorLog", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0001B5D2 File Offset: 0x000197D2
		internal static string ConnectionOpenedLog(object p0, object p1)
		{
			return EntityRes.GetString("ConnectionOpenedLog", new object[] { p0, p1 });
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0001B5EC File Offset: 0x000197EC
		internal static string ConnectionOpenErrorLog(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConnectionOpenErrorLog", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0001B60A File Offset: 0x0001980A
		internal static string ConnectionOpenedLogAsync(object p0, object p1)
		{
			return EntityRes.GetString("ConnectionOpenedLogAsync", new object[] { p0, p1 });
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0001B624 File Offset: 0x00019824
		internal static string ConnectionOpenErrorLogAsync(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConnectionOpenErrorLogAsync", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0001B642 File Offset: 0x00019842
		internal static string TransactionStartedLog(object p0, object p1)
		{
			return EntityRes.GetString("TransactionStartedLog", new object[] { p0, p1 });
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0001B65C File Offset: 0x0001985C
		internal static string TransactionStartErrorLog(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TransactionStartErrorLog", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0001B67A File Offset: 0x0001987A
		internal static string TransactionCommittedLog(object p0, object p1)
		{
			return EntityRes.GetString("TransactionCommittedLog", new object[] { p0, p1 });
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0001B694 File Offset: 0x00019894
		internal static string TransactionCommitErrorLog(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TransactionCommitErrorLog", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0001B6B2 File Offset: 0x000198B2
		internal static string TransactionRolledBackLog(object p0, object p1)
		{
			return EntityRes.GetString("TransactionRolledBackLog", new object[] { p0, p1 });
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0001B6CC File Offset: 0x000198CC
		internal static string TransactionRollbackErrorLog(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TransactionRollbackErrorLog", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0001B6EA File Offset: 0x000198EA
		internal static string ConnectionOpenCanceledLog(object p0, object p1)
		{
			return EntityRes.GetString("ConnectionOpenCanceledLog", new object[] { p0, p1 });
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0001B704 File Offset: 0x00019904
		internal static string TransactionHandler_AlreadyInitialized
		{
			get
			{
				return EntityRes.GetString("TransactionHandler_AlreadyInitialized");
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0001B710 File Offset: 0x00019910
		internal static string ConnectionDisposedLog(object p0, object p1)
		{
			return EntityRes.GetString("ConnectionDisposedLog", new object[] { p0, p1 });
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0001B72A File Offset: 0x0001992A
		internal static string TransactionDisposedLog(object p0, object p1)
		{
			return EntityRes.GetString("TransactionDisposedLog", new object[] { p0, p1 });
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0001B744 File Offset: 0x00019944
		internal static string UnableToLoadEmbeddedResource(object p0, object p1)
		{
			return EntityRes.GetString("UnableToLoadEmbeddedResource", new object[] { p0, p1 });
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0001B75E File Offset: 0x0001995E
		internal static string CannotSetBaseTypeCyclicInheritance(object p0, object p1)
		{
			return EntityRes.GetString("CannotSetBaseTypeCyclicInheritance", new object[] { p0, p1 });
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0001B778 File Offset: 0x00019978
		internal static string CannotDefineKeysOnBothBaseAndDerivedTypes
		{
			get
			{
				return EntityRes.GetString("CannotDefineKeysOnBothBaseAndDerivedTypes");
			}
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0001B784 File Offset: 0x00019984
		internal static string StoreTypeNotFound(object p0, object p1)
		{
			return EntityRes.GetString("StoreTypeNotFound", new object[] { p0, p1 });
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x0001B79E File Offset: 0x0001999E
		internal static string ProviderDoesNotSupportEscapingLikeArgument
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportEscapingLikeArgument");
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0001B7AA File Offset: 0x000199AA
		internal static string IndexPropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("IndexPropertyNotFound", new object[] { p0, p1 });
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0001B7C4 File Offset: 0x000199C4
		internal static string ConflictingIndexAttributeMatches(object p0, object p1)
		{
			return EntityRes.GetString("ConflictingIndexAttributeMatches", new object[] { p0, p1 });
		}
	}
}
