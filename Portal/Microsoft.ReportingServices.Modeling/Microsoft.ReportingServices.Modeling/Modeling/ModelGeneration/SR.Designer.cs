using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x02000106 RID: 262
	[CompilerGenerated]
	internal class SR
	{
		// Token: 0x06000D11 RID: 3345 RVA: 0x0002C068 File Offset: 0x0002A268
		protected SR()
		{
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0002C070 File Offset: 0x0002A270
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x0002C077 File Offset: 0x0002A277
		public static CultureInfo Culture
		{
			get
			{
				return SR.Keys.Culture;
			}
			set
			{
				SR.Keys.Culture = value;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0002C07F File Offset: 0x0002A27F
		public static string ModelGen_AlreadyExecuting
		{
			get
			{
				return SR.Keys.GetString("ModelGen_AlreadyExecuting");
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x0002C08B File Offset: 0x0002A28B
		public static string Rules_EntityDoesNotExist
		{
			get
			{
				return SR.Keys.GetString("Rules_EntityDoesNotExist");
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0002C097 File Offset: 0x0002A297
		public static string Rules_ParentEntityDoesNotExist
		{
			get
			{
				return SR.Keys.GetString("Rules_ParentEntityDoesNotExist");
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0002C0A3 File Offset: 0x0002A2A3
		public static string Rules_AttributeDoesNotExist
		{
			get
			{
				return SR.Keys.GetString("Rules_AttributeDoesNotExist");
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0002C0AF File Offset: 0x0002A2AF
		public static string CreateEntityRule_NonPrimaryDataSource
		{
			get
			{
				return SR.Keys.GetString("CreateEntityRule_NonPrimaryDataSource");
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0002C0BB File Offset: 0x0002A2BB
		public static string CreateEntityRule_NoPrimaryKey
		{
			get
			{
				return SR.Keys.GetString("CreateEntityRule_NoPrimaryKey");
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0002C0C7 File Offset: 0x0002A2C7
		public static string CreateRoleRule_SourceOrTargetEntityDoesNotExist
		{
			get
			{
				return SR.Keys.GetString("CreateRoleRule_SourceOrTargetEntityDoesNotExist");
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x0002C0D3 File Offset: 0x0002A2D3
		public static string CreateRoleRule_SelfRelationWithMatchingColumns
		{
			get
			{
				return SR.Keys.GetString("CreateRoleRule_SelfRelationWithMatchingColumns");
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0002C0DF File Offset: 0x0002A2DF
		public static string SetEntityAttributesRule_TargetCountTooLow
		{
			get
			{
				return SR.Keys.GetString("SetEntityAttributesRule_TargetCountTooLow");
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0002C0EB File Offset: 0x0002A2EB
		public static string SqlDsvStatisticsProvider_CalculatingTableRowCounts
		{
			get
			{
				return SR.Keys.GetString("SqlDsvStatisticsProvider_CalculatingTableRowCounts");
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0002C0F7 File Offset: 0x0002A2F7
		public static string SqlDsvStatisticsProvider_CalculatingUniqueness
		{
			get
			{
				return SR.Keys.GetString("SqlDsvStatisticsProvider_CalculatingUniqueness");
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0002C103 File Offset: 0x0002A303
		public static string SqlDsvStatisticsProvider_CalculatingColumnWidths
		{
			get
			{
				return SR.Keys.GetString("SqlDsvStatisticsProvider_CalculatingColumnWidths");
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0002C10F File Offset: 0x0002A30F
		public static string ModelGen_PassHeader(int pass)
		{
			return SR.Keys.GetString("ModelGen_PassHeader", pass);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0002C121 File Offset: 0x0002A321
		public static string ModelGen_TableProgressLine1(int pass)
		{
			return SR.Keys.GetString("ModelGen_TableProgressLine1", pass);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0002C133 File Offset: 0x0002A333
		public static string ModelGen_TableProgressLine2(string table, string column)
		{
			return SR.Keys.GetString("ModelGen_TableProgressLine2", table, column);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0002C141 File Offset: 0x0002A341
		public static string ModelGen_RelationProgressLine1(int pass)
		{
			return SR.Keys.GetString("ModelGen_RelationProgressLine1", pass);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0002C153 File Offset: 0x0002A353
		public static string Rules_CreatedModelItem(SRObjectDescriptor objectTypeAndName)
		{
			return SR.Keys.GetString("Rules_CreatedModelItem", objectTypeAndName);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0002C165 File Offset: 0x0002A365
		public static string Rules_ModifiedModelItem(SRObjectDescriptor objectTypeAndName)
		{
			return SR.Keys.GetString("Rules_ModifiedModelItem", objectTypeAndName);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0002C177 File Offset: 0x0002A377
		public static string Rules_FoundExistingModelItem(SRObjectDescriptor objectTypeAndName)
		{
			return SR.Keys.GetString("Rules_FoundExistingModelItem", objectTypeAndName);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0002C189 File Offset: 0x0002A389
		public static string Rules_DependentRuleOnExistingItem(SRObjectDescriptor objectTypeAndName)
		{
			return SR.Keys.GetString("Rules_DependentRuleOnExistingItem", objectTypeAndName);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0002C19B File Offset: 0x0002A39B
		public static string Rules_UnsupportedDataType(Type type)
		{
			return SR.Keys.GetString("Rules_UnsupportedDataType", type);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0002C1A8 File Offset: 0x0002A3A8
		public static string CreateColumnEntityRule_ColumnEntityName(string parentEntityName, string columnName)
		{
			return SR.Keys.GetString("CreateColumnEntityRule_ColumnEntityName", parentEntityName, columnName);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0002C1B6 File Offset: 0x0002A3B6
		public static string CreateRoleRule_RoleNameWithEntity(string entity, string role)
		{
			return SR.Keys.GetString("CreateRoleRule_RoleNameWithEntity", entity, role);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0002C1C4 File Offset: 0x0002A3C4
		public static string SetEntityAttributesRule_AlreadySet(EntityAttributesAssignment assignedCollection, SRObjectDescriptor objectTypeAndName)
		{
			return SR.Keys.GetString("SetEntityAttributesRule_AlreadySet", assignedCollection, objectTypeAndName);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0002C1DC File Offset: 0x0002A3DC
		public static string SetEntityAttributesRule_EntityHasNoFields(SRObjectDescriptor objectTypeAndName)
		{
			return SR.Keys.GetString("SetEntityAttributesRule_EntityHasNoFields", objectTypeAndName);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0002C1EE File Offset: 0x0002A3EE
		public static string SetEntityAttributesRule_SetEntityAttributes(EntityAttributesAssignment assignedCollection, string attributeNames)
		{
			return SR.Keys.GetString("SetEntityAttributesRule_SetEntityAttributes", assignedCollection, attributeNames);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0002C201 File Offset: 0x0002A401
		public static string SqlDsvStatisticsProvider_CommandException(string message, string commandText)
		{
			return SR.Keys.GetString("SqlDsvStatisticsProvider_CommandException", message, commandText);
		}

		// Token: 0x020001DE RID: 478
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x060011C5 RID: 4549 RVA: 0x00037239 File Offset: 0x00035439
			private Keys()
			{
			}

			// Token: 0x17000415 RID: 1045
			// (get) Token: 0x060011C6 RID: 4550 RVA: 0x00037241 File Offset: 0x00035441
			// (set) Token: 0x060011C7 RID: 4551 RVA: 0x00037248 File Offset: 0x00035448
			public static CultureInfo Culture
			{
				get
				{
					return SR.Keys._culture;
				}
				set
				{
					SR.Keys._culture = value;
				}
			}

			// Token: 0x060011C8 RID: 4552 RVA: 0x00037250 File Offset: 0x00035450
			public static string GetString(string key)
			{
				return SR.Keys.resourceManager.GetString(key, SR.Keys._culture);
			}

			// Token: 0x060011C9 RID: 4553 RVA: 0x00037262 File Offset: 0x00035462
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, SR.Keys.resourceManager.GetString(key, SR.Keys._culture), arg0);
			}

			// Token: 0x060011CA RID: 4554 RVA: 0x0003727F File Offset: 0x0003547F
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, SR.Keys.resourceManager.GetString(key, SR.Keys._culture), arg0, arg1);
			}

			// Token: 0x0400080C RID: 2060
			private static ResourceManager resourceManager = new ResourceManager(typeof(SR).FullName, typeof(SR).Module.Assembly);

			// Token: 0x0400080D RID: 2061
			private static CultureInfo _culture = null;

			// Token: 0x0400080E RID: 2062
			public const string ModelGen_AlreadyExecuting = "ModelGen_AlreadyExecuting";

			// Token: 0x0400080F RID: 2063
			public const string ModelGen_PassHeader = "ModelGen_PassHeader";

			// Token: 0x04000810 RID: 2064
			public const string ModelGen_TableProgressLine1 = "ModelGen_TableProgressLine1";

			// Token: 0x04000811 RID: 2065
			public const string ModelGen_TableProgressLine2 = "ModelGen_TableProgressLine2";

			// Token: 0x04000812 RID: 2066
			public const string ModelGen_RelationProgressLine1 = "ModelGen_RelationProgressLine1";

			// Token: 0x04000813 RID: 2067
			public const string Rules_CreatedModelItem = "Rules_CreatedModelItem";

			// Token: 0x04000814 RID: 2068
			public const string Rules_ModifiedModelItem = "Rules_ModifiedModelItem";

			// Token: 0x04000815 RID: 2069
			public const string Rules_FoundExistingModelItem = "Rules_FoundExistingModelItem";

			// Token: 0x04000816 RID: 2070
			public const string Rules_DependentRuleOnExistingItem = "Rules_DependentRuleOnExistingItem";

			// Token: 0x04000817 RID: 2071
			public const string Rules_EntityDoesNotExist = "Rules_EntityDoesNotExist";

			// Token: 0x04000818 RID: 2072
			public const string Rules_ParentEntityDoesNotExist = "Rules_ParentEntityDoesNotExist";

			// Token: 0x04000819 RID: 2073
			public const string Rules_AttributeDoesNotExist = "Rules_AttributeDoesNotExist";

			// Token: 0x0400081A RID: 2074
			public const string Rules_UnsupportedDataType = "Rules_UnsupportedDataType";

			// Token: 0x0400081B RID: 2075
			public const string CreateEntityRule_NonPrimaryDataSource = "CreateEntityRule_NonPrimaryDataSource";

			// Token: 0x0400081C RID: 2076
			public const string CreateEntityRule_NoPrimaryKey = "CreateEntityRule_NoPrimaryKey";

			// Token: 0x0400081D RID: 2077
			public const string CreateColumnEntityRule_ColumnEntityName = "CreateColumnEntityRule_ColumnEntityName";

			// Token: 0x0400081E RID: 2078
			public const string CreateRoleRule_SourceOrTargetEntityDoesNotExist = "CreateRoleRule_SourceOrTargetEntityDoesNotExist";

			// Token: 0x0400081F RID: 2079
			public const string CreateRoleRule_SelfRelationWithMatchingColumns = "CreateRoleRule_SelfRelationWithMatchingColumns";

			// Token: 0x04000820 RID: 2080
			public const string CreateRoleRule_RoleNameWithEntity = "CreateRoleRule_RoleNameWithEntity";

			// Token: 0x04000821 RID: 2081
			public const string SetEntityAttributesRule_TargetCountTooLow = "SetEntityAttributesRule_TargetCountTooLow";

			// Token: 0x04000822 RID: 2082
			public const string SetEntityAttributesRule_AlreadySet = "SetEntityAttributesRule_AlreadySet";

			// Token: 0x04000823 RID: 2083
			public const string SetEntityAttributesRule_EntityHasNoFields = "SetEntityAttributesRule_EntityHasNoFields";

			// Token: 0x04000824 RID: 2084
			public const string SetEntityAttributesRule_SetEntityAttributes = "SetEntityAttributesRule_SetEntityAttributes";

			// Token: 0x04000825 RID: 2085
			public const string SqlDsvStatisticsProvider_CalculatingTableRowCounts = "SqlDsvStatisticsProvider_CalculatingTableRowCounts";

			// Token: 0x04000826 RID: 2086
			public const string SqlDsvStatisticsProvider_CalculatingUniqueness = "SqlDsvStatisticsProvider_CalculatingUniqueness";

			// Token: 0x04000827 RID: 2087
			public const string SqlDsvStatisticsProvider_CalculatingColumnWidths = "SqlDsvStatisticsProvider_CalculatingColumnWidths";

			// Token: 0x04000828 RID: 2088
			public const string SqlDsvStatisticsProvider_CommandException = "SqlDsvStatisticsProvider_CommandException";
		}
	}
}
