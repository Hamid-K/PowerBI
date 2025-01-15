using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200011E RID: 286
	[CompilerGenerated]
	internal class TomSR
	{
		// Token: 0x06001249 RID: 4681 RVA: 0x000808C3 File Offset: 0x0007EAC3
		protected TomSR()
		{
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x000808CB File Offset: 0x0007EACB
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x000808D2 File Offset: 0x0007EAD2
		public static CultureInfo Culture
		{
			get
			{
				return TomSR.Keys.Culture;
			}
			set
			{
				TomSR.Keys.Culture = value;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x000808DA File Offset: 0x0007EADA
		public static string Exception_InternalErrorOccured
		{
			get
			{
				return TomSR.Keys.GetString("Exception_InternalErrorOccured");
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x000808E6 File Offset: 0x0007EAE6
		public static string Exception_NoVersionColumnInRowset
		{
			get
			{
				return TomSR.Keys.GetString("Exception_NoVersionColumnInRowset");
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x000808F2 File Offset: 0x0007EAF2
		public static string Exception_ObjectRemovedCannotBeModifiedAttachedToModel
		{
			get
			{
				return TomSR.Keys.GetString("Exception_ObjectRemovedCannotBeModifiedAttachedToModel");
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x000808FE File Offset: 0x0007EAFE
		public static string Exception_RenameAlreadyRequested
		{
			get
			{
				return TomSR.Keys.GetString("Exception_RenameAlreadyRequested");
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x0008090A File Offset: 0x0007EB0A
		public static string Exception_DisconnectedObjectCannotBeRefreshed
		{
			get
			{
				return TomSR.Keys.GetString("Exception_DisconnectedObjectCannotBeRefreshed");
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x00080916 File Offset: 0x0007EB16
		public static string Exception_DisconnectedObjectCannotBeAnalyzeRefreshPolicyImpact
		{
			get
			{
				return TomSR.Keys.GetString("Exception_DisconnectedObjectCannotBeAnalyzeRefreshPolicyImpact");
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x00080922 File Offset: 0x0007EB22
		public static string Exception_ModelCannotBeMotified_RenameRequested
		{
			get
			{
				return TomSR.Keys.GetString("Exception_ModelCannotBeMotified_RenameRequested");
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0008092E File Offset: 0x0007EB2E
		public static string Exception_ModelCannotBeModifiedAnotherModelInActiveTransaction
		{
			get
			{
				return TomSR.Keys.GetString("Exception_ModelCannotBeModifiedAnotherModelInActiveTransaction");
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0008093A File Offset: 0x0007EB3A
		public static string Exception_NoActiveTransactionInSession
		{
			get
			{
				return TomSR.Keys.GetString("Exception_NoActiveTransactionInSession");
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00080946 File Offset: 0x0007EB46
		public static string Exception_ValidationFlagsSimultaneousUse
		{
			get
			{
				return TomSR.Keys.GetString("Exception_ValidationFlagsSimultaneousUse");
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00080952 File Offset: 0x0007EB52
		public static string Exception_ValidationFlagsOutsideTransaction
		{
			get
			{
				return TomSR.Keys.GetString("Exception_ValidationFlagsOutsideTransaction");
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0008095E File Offset: 0x0007EB5E
		public static string Exception_CannotStartTransactionLocalchanges
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotStartTransactionLocalchanges");
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x0008096A File Offset: 0x0007EB6A
		public static string Exception_CannotExceuteJsonWithLocalchanges
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotExceuteJsonWithLocalchanges");
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00080976 File Offset: 0x0007EB76
		public static string Exception_CannotSyncModelDisconnected
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotSyncModelDisconnected");
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x00080982 File Offset: 0x0007EB82
		public static string Exception_CannotSyncNewModel
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotSyncNewModel");
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0008098E File Offset: 0x0007EB8E
		public static string Exception_CannotSyncModelModified
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotSyncModelModified");
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0008099A File Offset: 0x0007EB9A
		public static string Exception_CannotSaveChangesDisconnectedModel
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotSaveChangesDisconnectedModel");
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x000809A6 File Offset: 0x0007EBA6
		public static string Exception_CannotSaveChangeAnotherModelInTransaction
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotSaveChangeAnotherModelInTransaction");
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x000809B2 File Offset: 0x0007EBB2
		public static string Exception_CannotUndoChangesDisconnectedModel
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotUndoChangesDisconnectedModel");
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x000809BE File Offset: 0x0007EBBE
		public static string Exception_CannotExecuteXmlaDisconnectedModel
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotExecuteXmlaDisconnectedModel");
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x000809CA File Offset: 0x0007EBCA
		public static string Exception_CannotExecuteXmlaAnotherModelInTransaction
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotExecuteXmlaAnotherModelInTransaction");
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x000809D6 File Offset: 0x0007EBD6
		public static string Exception_PartitionSourceAlreadyAttached
		{
			get
			{
				return TomSR.Keys.GetString("Exception_PartitionSourceAlreadyAttached");
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x000809E2 File Offset: 0x0007EBE2
		public static string Exception_PartitionSourceAlreadyRemoved
		{
			get
			{
				return TomSR.Keys.GetString("Exception_PartitionSourceAlreadyRemoved");
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x000809EE File Offset: 0x0007EBEE
		public static string Exception_CannotUpdateCultureCollation
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotUpdateCultureCollation");
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x000809FA File Offset: 0x0007EBFA
		public static string Exception_CannotUpdateCultureInfoOfChildCollection
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotUpdateCultureInfoOfChildCollection");
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00080A06 File Offset: 0x0007EC06
		public static string Exception_CannotCopyCollectionCultureConflict
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotCopyCollectionCultureConflict");
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x00080A12 File Offset: 0x0007EC12
		public static string Exception_CannotRetrieveAdaptiveCachingRecommendations
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotRetrieveAdaptiveCachingRecommendations");
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00080A1E File Offset: 0x0007EC1E
		public static string Exception_UboNotAvailable
		{
			get
			{
				return TomSR.Keys.GetString("Exception_UboNotAvailable");
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x00080A2A File Offset: 0x0007EC2A
		public static string Exception_InvalidArrayIndex
		{
			get
			{
				return TomSR.Keys.GetString("Exception_InvalidArrayIndex");
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00080A36 File Offset: 0x0007EC36
		public static string Exception_CannotModifyReadOnlyFormatOptions
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotModifyReadOnlyFormatOptions");
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x00080A42 File Offset: 0x0007EC42
		public static string DefaultNameForObject_Model
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Model");
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x00080A4E File Offset: 0x0007EC4E
		public static string DefaultNameForObject_DataSource
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_DataSource");
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x00080A5A File Offset: 0x0007EC5A
		public static string DefaultNameForObject_Table
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Table");
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00080A66 File Offset: 0x0007EC66
		public static string DefaultNameForObject_Column
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Column");
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x00080A72 File Offset: 0x0007EC72
		public static string DefaultNameForObject_AttributeHierarchy
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_AttributeHierarchy");
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x00080A7E File Offset: 0x0007EC7E
		public static string DefaultNameForObject_Partition
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Partition");
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00080A8A File Offset: 0x0007EC8A
		public static string DefaultNameForObject_Relationship
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Relationship");
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x00080A96 File Offset: 0x0007EC96
		public static string DefaultNameForObject_Measure
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Measure");
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00080AA2 File Offset: 0x0007ECA2
		public static string DefaultNameForObject_Hierarchy
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Hierarchy");
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x00080AAE File Offset: 0x0007ECAE
		public static string DefaultNameForObject_Level
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Level");
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00080ABA File Offset: 0x0007ECBA
		public static string DefaultNameForObject_Annotation
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Annotation");
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x00080AC6 File Offset: 0x0007ECC6
		public static string DefaultNameForObject_KPI
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_KPI");
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00080AD2 File Offset: 0x0007ECD2
		public static string DefaultNameForObject_Culture
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Culture");
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x00080ADE File Offset: 0x0007ECDE
		public static string DefaultNameForObject_LinguisticMetadata
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_LinguisticMetadata");
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00080AEA File Offset: 0x0007ECEA
		public static string DefaultNameForObject_Perspective
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Perspective");
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x00080AF6 File Offset: 0x0007ECF6
		public static string DefaultNameForObject_PerspectiveTable
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_PerspectiveTable");
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00080B02 File Offset: 0x0007ED02
		public static string DefaultNameForObject_PerspectiveColumn
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_PerspectiveColumn");
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x00080B0E File Offset: 0x0007ED0E
		public static string DefaultNameForObject_PerspectiveHierarchy
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_PerspectiveHierarchy");
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x00080B1A File Offset: 0x0007ED1A
		public static string DefaultNameForObject_PerspectiveMeasure
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_PerspectiveMeasure");
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x00080B26 File Offset: 0x0007ED26
		public static string DefaultNameForObject_ModelRole
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_ModelRole");
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x00080B32 File Offset: 0x0007ED32
		public static string DefaultNameForObject_ModelRoleMember
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_ModelRoleMember");
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x00080B3E File Offset: 0x0007ED3E
		public static string DefaultNameForObject_TablePermission
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_TablePermission");
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x00080B4A File Offset: 0x0007ED4A
		public static string DefaultNameForObject_Variation
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Variation");
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x00080B56 File Offset: 0x0007ED56
		public static string DefaultNameForObject_Set
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Set");
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00080B62 File Offset: 0x0007ED62
		public static string DefaultNameForObject_PerspectiveSet
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_PerspectiveSet");
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x00080B6E File Offset: 0x0007ED6E
		public static string DefaultNameForObject_ExtendedProperty
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_ExtendedProperty");
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00080B7A File Offset: 0x0007ED7A
		public static string DefaultNameForObject_NamedExpression
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_NamedExpression");
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x00080B86 File Offset: 0x0007ED86
		public static string DefaultNameForObject_ColumnPermission
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_ColumnPermission");
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00080B92 File Offset: 0x0007ED92
		public static string DefaultNameForObject_DetailRowsDefinition
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_DetailRowsDefinition");
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x00080B9E File Offset: 0x0007ED9E
		public static string DefaultNameForObject_RelatedColumnDetails
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_RelatedColumnDetails");
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x00080BAA File Offset: 0x0007EDAA
		public static string DefaultNameForObject_GroupByColumn
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_GroupByColumn");
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x00080BB6 File Offset: 0x0007EDB6
		public static string DefaultNameForObject_CalculationGroup
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_CalculationGroup");
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x00080BC2 File Offset: 0x0007EDC2
		public static string DefaultNameForObject_CalculationItem
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_CalculationItem");
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00080BCE File Offset: 0x0007EDCE
		public static string DefaultNameForObject_QueryGroup
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_QueryGroup");
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00080BDA File Offset: 0x0007EDDA
		public static string DefaultNameForObject_AnalyticsAIMetadata
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_AnalyticsAIMetadata");
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x00080BE6 File Offset: 0x0007EDE6
		public static string DefaultNameForObject_ChangedProperty
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_ChangedProperty");
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x00080BF2 File Offset: 0x0007EDF2
		public static string DefaultNameForObject_ExcludedArtifact
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_ExcludedArtifact");
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x00080BFE File Offset: 0x0007EDFE
		public static string DefaultNameForObject_DataCoverageDefinition
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_DataCoverageDefinition");
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00080C0A File Offset: 0x0007EE0A
		public static string DefaultNameForObject_CalculationGroupExpression
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_CalculationGroupExpression");
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x00080C16 File Offset: 0x0007EE16
		public static string DefaultNameForObject_Calendar
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Calendar");
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x00080C22 File Offset: 0x0007EE22
		public static string DefaultNameForObject_TimeUnitColumnAssociation
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_TimeUnitColumnAssociation");
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x00080C2E File Offset: 0x0007EE2E
		public static string DefaultNameForObject_CalendarColumnReference
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_CalendarColumnReference");
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00080C3A File Offset: 0x0007EE3A
		public static string DefaultNameForObject_Function
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_Function");
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x00080C46 File Offset: 0x0007EE46
		public static string DefaultNameForObject_BindingInfo
		{
			get
			{
				return TomSR.Keys.GetString("DefaultNameForObject_BindingInfo");
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00080C52 File Offset: 0x0007EE52
		public static string ObjectType_Model
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Model");
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x00080C5E File Offset: 0x0007EE5E
		public static string ObjectType_DataSource
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_DataSource");
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00080C6A File Offset: 0x0007EE6A
		public static string ObjectType_Table
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Table");
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x00080C76 File Offset: 0x0007EE76
		public static string ObjectType_Column
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Column");
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x00080C82 File Offset: 0x0007EE82
		public static string ObjectType_AttributeHierarchy
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_AttributeHierarchy");
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x00080C8E File Offset: 0x0007EE8E
		public static string ObjectType_Partition
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Partition");
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x00080C9A File Offset: 0x0007EE9A
		public static string ObjectType_Relationship
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Relationship");
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x00080CA6 File Offset: 0x0007EEA6
		public static string ObjectType_Measure
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Measure");
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00080CB2 File Offset: 0x0007EEB2
		public static string ObjectType_Hierarchy
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Hierarchy");
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x00080CBE File Offset: 0x0007EEBE
		public static string ObjectType_Level
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Level");
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x00080CCA File Offset: 0x0007EECA
		public static string ObjectType_Annotation
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Annotation");
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x00080CD6 File Offset: 0x0007EED6
		public static string ObjectType_KPI
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_KPI");
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x00080CE2 File Offset: 0x0007EEE2
		public static string ObjectType_Culture
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Culture");
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00080CEE File Offset: 0x0007EEEE
		public static string ObjectType_LinguisticMetadata
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_LinguisticMetadata");
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x00080CFA File Offset: 0x0007EEFA
		public static string ObjectType_ObjectTranslation
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_ObjectTranslation");
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x00080D06 File Offset: 0x0007EF06
		public static string ObjectType_Perspective
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Perspective");
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00080D12 File Offset: 0x0007EF12
		public static string ObjectType_PerspectiveTable
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_PerspectiveTable");
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x00080D1E File Offset: 0x0007EF1E
		public static string ObjectType_PerspectiveColumn
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_PerspectiveColumn");
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x00080D2A File Offset: 0x0007EF2A
		public static string ObjectType_PerspectiveHierarchy
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_PerspectiveHierarchy");
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x00080D36 File Offset: 0x0007EF36
		public static string ObjectType_PerspectiveMeasure
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_PerspectiveMeasure");
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00080D42 File Offset: 0x0007EF42
		public static string ObjectType_ModelRole
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_ModelRole");
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x00080D4E File Offset: 0x0007EF4E
		public static string ObjectType_ModelRoleMember
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_ModelRoleMember");
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x00080D5A File Offset: 0x0007EF5A
		public static string ObjectType_TablePermission
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_TablePermission");
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x00080D66 File Offset: 0x0007EF66
		public static string ObjectType_Variation
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Variation");
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x00080D72 File Offset: 0x0007EF72
		public static string ObjectType_Set
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Set");
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00080D7E File Offset: 0x0007EF7E
		public static string ObjectType_PerspectiveSet
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_PerspectiveSet");
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x00080D8A File Offset: 0x0007EF8A
		public static string ObjectType_ExtendedProperty
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_ExtendedProperty");
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00080D96 File Offset: 0x0007EF96
		public static string ObjectType_NamedExpression
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_NamedExpression");
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00080DA2 File Offset: 0x0007EFA2
		public static string ObjectType_ColumnPermission
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_ColumnPermission");
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00080DAE File Offset: 0x0007EFAE
		public static string ObjectType_DetailRowsDefinition
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_DetailRowsDefinition");
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00080DBA File Offset: 0x0007EFBA
		public static string ObjectType_RelatedColumnDetails
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_RelatedColumnDetails");
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00080DC6 File Offset: 0x0007EFC6
		public static string ObjectType_GroupByColumn
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_GroupByColumn");
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x00080DD2 File Offset: 0x0007EFD2
		public static string ObjectType_CalculationGroup
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_CalculationGroup");
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00080DDE File Offset: 0x0007EFDE
		public static string ObjectType_CalculationItem
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_CalculationItem");
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x00080DEA File Offset: 0x0007EFEA
		public static string ObjectType_AlternateOf
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_AlternateOf");
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x00080DF6 File Offset: 0x0007EFF6
		public static string ObjectType_RefreshPolicy
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_RefreshPolicy");
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x00080E02 File Offset: 0x0007F002
		public static string ObjectType_FormatStringDefinition
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_FormatStringDefinition");
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x00080E0E File Offset: 0x0007F00E
		public static string ObjectType_QueryGroup
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_QueryGroup");
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00080E1A File Offset: 0x0007F01A
		public static string ObjectType_AnalyticsAIMetadata
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_AnalyticsAIMetadata");
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x00080E26 File Offset: 0x0007F026
		public static string ObjectType_ChangedProperty
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_ChangedProperty");
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x00080E32 File Offset: 0x0007F032
		public static string ObjectType_ExcludedArtifact
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_ExcludedArtifact");
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x00080E3E File Offset: 0x0007F03E
		public static string ObjectType_DataCoverageDefinition
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_DataCoverageDefinition");
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00080E4A File Offset: 0x0007F04A
		public static string ObjectType_CalculationGroupExpression
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_CalculationGroupExpression");
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x00080E56 File Offset: 0x0007F056
		public static string ObjectType_Calendar
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Calendar");
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00080E62 File Offset: 0x0007F062
		public static string ObjectType_TimeUnitColumnAssociation
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_TimeUnitColumnAssociation");
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00080E6E File Offset: 0x0007F06E
		public static string ObjectType_CalendarColumnReference
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_CalendarColumnReference");
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00080E7A File Offset: 0x0007F07A
		public static string ObjectType_Function
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Function");
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x00080E86 File Offset: 0x0007F086
		public static string ObjectType_BindingInfo
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_BindingInfo");
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00080E92 File Offset: 0x0007F092
		public static string ObjectType_Database
		{
			get
			{
				return TomSR.Keys.GetString("ObjectType_Database");
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x00080E9E File Offset: 0x0007F09E
		public static string ObjectPath_AttributeHierarchy_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_AttributeHierarchy_0Args");
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00080EAA File Offset: 0x0007F0AA
		public static string ObjectPath_KPI_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_KPI_0Args");
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00080EB6 File Offset: 0x0007F0B6
		public static string ObjectPath_LinguisticMetadata_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_LinguisticMetadata_0Args");
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00080EC2 File Offset: 0x0007F0C2
		public static string ObjectPath_ObjectTranslation_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_ObjectTranslation_0Args");
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x00080ECE File Offset: 0x0007F0CE
		public static string ObjectPath_PerspectiveTable_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_PerspectiveTable_0Args");
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00080EDA File Offset: 0x0007F0DA
		public static string ObjectPath_PerspectiveColumn_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_PerspectiveColumn_0Args");
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x00080EE6 File Offset: 0x0007F0E6
		public static string ObjectPath_PerspectiveHierarchy_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_PerspectiveHierarchy_0Args");
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x00080EF2 File Offset: 0x0007F0F2
		public static string ObjectPath_PerspectiveMeasure_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_PerspectiveMeasure_0Args");
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x00080EFE File Offset: 0x0007F0FE
		public static string ObjectPath_PerspectiveSet_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_PerspectiveSet_0Args");
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00080F0A File Offset: 0x0007F10A
		public static string ObjectPath_DetailRowsDefinition_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_DetailRowsDefinition_0Args");
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00080F16 File Offset: 0x0007F116
		public static string ObjectPath_RelatedColumnDetails_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_RelatedColumnDetails_0Args");
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00080F22 File Offset: 0x0007F122
		public static string ObjectPath_GroupByColumn_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_GroupByColumn_0Args");
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00080F2E File Offset: 0x0007F12E
		public static string ObjectPath_AlternateOf_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_AlternateOf_0Args");
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00080F3A File Offset: 0x0007F13A
		public static string ObjectPath_CalculationGroup_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_CalculationGroup_0Args");
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00080F46 File Offset: 0x0007F146
		public static string ObjectPath_CalculationItem_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_CalculationItem_0Args");
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00080F52 File Offset: 0x0007F152
		public static string ObjectPath_RefreshPolicy_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_RefreshPolicy_0Args");
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00080F5E File Offset: 0x0007F15E
		public static string ObjectPath_FormatStringDefinition_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_FormatStringDefinition_0Args");
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00080F6A File Offset: 0x0007F16A
		public static string ObjectPath_QueryGroup_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_QueryGroup_0Args");
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00080F76 File Offset: 0x0007F176
		public static string ObjectPath_AnalyticsAIMetadata_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_AnalyticsAIMetadata_0Args");
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00080F82 File Offset: 0x0007F182
		public static string ObjectPath_ChangedProperty_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_ChangedProperty_0Args");
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x00080F8E File Offset: 0x0007F18E
		public static string ObjectPath_ExcludedArtifact_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_ExcludedArtifact_0Args");
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00080F9A File Offset: 0x0007F19A
		public static string ObjectPath_DataCoverageDefinition_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_DataCoverageDefinition_0Args");
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x00080FA6 File Offset: 0x0007F1A6
		public static string ObjectPath_CalculationGroupExpression_0Args
		{
			get
			{
				return TomSR.Keys.GetString("ObjectPath_CalculationGroupExpression_0Args");
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x00080FB2 File Offset: 0x0007F1B2
		public static string Error_NameIsRequired
		{
			get
			{
				return TomSR.Keys.GetString("Error_NameIsRequired");
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x00080FBE File Offset: 0x0007F1BE
		public static string Exception_Json_GenerateSchemaWithTranslatablePropertiesNotSupported
		{
			get
			{
				return TomSR.Keys.GetString("Exception_Json_GenerateSchemaWithTranslatablePropertiesNotSupported");
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00080FCA File Offset: 0x0007F1CA
		public static string Exception_CannotDeserializeObjectPathMalformedInput
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotDeserializeObjectPathMalformedInput");
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x00080FD6 File Offset: 0x0007F1D6
		public static string Exception_CannotDeserializeObjectPath
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotDeserializeObjectPath");
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x00080FE2 File Offset: 0x0007F1E2
		public static string Exception_CannotProbeMetadataObjectCollectionFromJson
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotProbeMetadataObjectCollectionFromJson");
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00080FEE File Offset: 0x0007F1EE
		public static string Exception_TranslatedObjectNull
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TranslatedObjectNull");
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x00080FFA File Offset: 0x0007F1FA
		public static string Exception_InvalidJsonScript
		{
			get
			{
				return TomSR.Keys.GetString("Exception_InvalidJsonScript");
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x00081006 File Offset: 0x0007F206
		public static string Exception_NameCannotBeSetForReferencedObjects
		{
			get
			{
				return TomSR.Keys.GetString("Exception_NameCannotBeSetForReferencedObjects");
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x00081012 File Offset: 0x0007F212
		public static string Exception_NameCannotBeSetForReadOnlyNamedObjects
		{
			get
			{
				return TomSR.Keys.GetString("Exception_NameCannotBeSetForReadOnlyNamedObjects");
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x0008101E File Offset: 0x0007F21E
		public static string Exception_ExternalRoleMemberEmptyIdentityProvider
		{
			get
			{
				return TomSR.Keys.GetString("Exception_ExternalRoleMemberEmptyIdentityProvider");
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x0008102A File Offset: 0x0007F22A
		public static string Exception_RemovedDatabaseCannotBeAttached
		{
			get
			{
				return TomSR.Keys.GetString("Exception_RemovedDatabaseCannotBeAttached");
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00081036 File Offset: 0x0007F236
		public static string Exception_RemovedModelCannotBeAttached
		{
			get
			{
				return TomSR.Keys.GetString("Exception_RemovedModelCannotBeAttached");
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x00081042 File Offset: 0x0007F242
		public static string Exception_ModelAlreadyBelongsToAnotherDatabase
		{
			get
			{
				return TomSR.Keys.GetString("Exception_ModelAlreadyBelongsToAnotherDatabase");
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0008104E File Offset: 0x0007F24E
		public static string Exception_OverridesIncompatibleWithRefreshType
		{
			get
			{
				return TomSR.Keys.GetString("Exception_OverridesIncompatibleWithRefreshType");
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x0008105A File Offset: 0x0007F25A
		public static string Exception_ApplyRefreshPoliciesIncompatibleWithRefreshType
		{
			get
			{
				return TomSR.Keys.GetString("Exception_ApplyRefreshPoliciesIncompatibleWithRefreshType");
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00081066 File Offset: 0x0007F266
		public static string Exception_ApplyRefreshPoliciesSaveInOfflineMode
		{
			get
			{
				return TomSR.Keys.GetString("Exception_ApplyRefreshPoliciesSaveInOfflineMode");
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x00081072 File Offset: 0x0007F272
		public static string Exception_ApplyAutomaticAggregationsInOfflineMode
		{
			get
			{
				return TomSR.Keys.GetString("Exception_ApplyAutomaticAggregationsInOfflineMode");
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x0008107E File Offset: 0x0007F27E
		public static string Exception_OverridesScopeObjectIsEmpty
		{
			get
			{
				return TomSR.Keys.GetString("Exception_OverridesScopeObjectIsEmpty");
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x0008108A File Offset: 0x0007F28A
		public static string Exception_OverridesDatasourceCannotBeFound
		{
			get
			{
				return TomSR.Keys.GetString("Exception_OverridesDatasourceCannotBeFound");
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00081096 File Offset: 0x0007F296
		public static string Exception_UnexpectedTokensAfterJsonCommand
		{
			get
			{
				return TomSR.Keys.GetString("Exception_UnexpectedTokensAfterJsonCommand");
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x000810A2 File Offset: 0x0007F2A2
		public static string Exception_JsonScriptCannotExecuteWhenPendingChanges
		{
			get
			{
				return TomSR.Keys.GetString("Exception_JsonScriptCannotExecuteWhenPendingChanges");
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x000810AE File Offset: 0x0007F2AE
		public static string Exception_JsonScriptCannotScriptOutObjectWithoutDatabase
		{
			get
			{
				return TomSR.Keys.GetString("Exception_JsonScriptCannotScriptOutObjectWithoutDatabase");
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x000810BA File Offset: 0x0007F2BA
		public static string Exception_JsonScriptCannotScriptOutNonTMDatabase
		{
			get
			{
				return TomSR.Keys.GetString("Exception_JsonScriptCannotScriptOutNonTMDatabase");
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x000810C6 File Offset: 0x0007F2C6
		public static string Exception_JsonCommandRefreshMultipleDbsNotSupported
		{
			get
			{
				return TomSR.Keys.GetString("Exception_JsonCommandRefreshMultipleDbsNotSupported");
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x000810D2 File Offset: 0x0007F2D2
		public static string Exception_JsonCommandSequenceMultipleDbsNotSupported
		{
			get
			{
				return TomSR.Keys.GetString("Exception_JsonCommandSequenceMultipleDbsNotSupported");
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x000810DE File Offset: 0x0007F2DE
		public static string Exception_JsonCommandRefreshScriptOutObjectsNotSpecified
		{
			get
			{
				return TomSR.Keys.GetString("Exception_JsonCommandRefreshScriptOutObjectsNotSpecified");
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x000810EA File Offset: 0x0007F2EA
		public static string Exception_JsonCommandScriptOutMultipleDbsNotSupported
		{
			get
			{
				return TomSR.Keys.GetString("Exception_JsonCommandScriptOutMultipleDbsNotSupported");
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x000810F6 File Offset: 0x0007F2F6
		public static string Exception_JsonCommandSequenceNestedNotSupported
		{
			get
			{
				return TomSR.Keys.GetString("Exception_JsonCommandSequenceNestedNotSupported");
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00081102 File Offset: 0x0007F302
		public static string Exception_JsonCommandSequenceMultipleDatabasesNotSupported
		{
			get
			{
				return TomSR.Keys.GetString("Exception_JsonCommandSequenceMultipleDatabasesNotSupported");
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x0008110E File Offset: 0x0007F30E
		public static string Exception_JsonCommandSequenceFinishedExecuting
		{
			get
			{
				return TomSR.Keys.GetString("Exception_JsonCommandSequenceFinishedExecuting");
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x0008111A File Offset: 0x0007F31A
		public static string Exception_ModelCannotBeMotified_MergePartitionsRequested
		{
			get
			{
				return TomSR.Keys.GetString("Exception_ModelCannotBeMotified_MergePartitionsRequested");
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x00081126 File Offset: 0x0007F326
		public static string Exception_MergePartitionsForTableAlreadyRequested
		{
			get
			{
				return TomSR.Keys.GetString("Exception_MergePartitionsForTableAlreadyRequested");
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x00081132 File Offset: 0x0007F332
		public static string Exception_PartitionsFromDifferentTablesCannotBeMerged
		{
			get
			{
				return TomSR.Keys.GetString("Exception_PartitionsFromDifferentTablesCannotBeMerged");
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x0008113E File Offset: 0x0007F33E
		public static string Exception_DisconnectedPartitionCannotBeMerged
		{
			get
			{
				return TomSR.Keys.GetString("Exception_DisconnectedPartitionCannotBeMerged");
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x0008114A File Offset: 0x0007F34A
		public static string Exception_NonEmptyModelCannotBeReplaced
		{
			get
			{
				return TomSR.Keys.GetString("Exception_NonEmptyModelCannotBeReplaced");
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x00081156 File Offset: 0x0007F356
		public static string Exception_CannotApplyRefreshPolicyDisconnectedModel
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotApplyRefreshPolicyDisconnectedModel");
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00081162 File Offset: 0x0007F362
		public static string Exception_CannotApplyRefreshPolicyModifiedModel
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotApplyRefreshPolicyModifiedModel");
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0008116E File Offset: 0x0007F36E
		public static string Exception_CannotApplyRefreshPolicyDisconnectedTable
		{
			get
			{
				return TomSR.Keys.GetString("Exception_CannotApplyRefreshPolicyDisconnectedTable");
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x0008117A File Offset: 0x0007F37A
		public static string Exception_TmdlReaderEOF
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TmdlReaderEOF");
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x00081186 File Offset: 0x0007F386
		public static string Exception_InvalidStreamNoWrite
		{
			get
			{
				return TomSR.Keys.GetString("Exception_InvalidStreamNoWrite");
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x00081192 File Offset: 0x0007F392
		public static string Exception_InvalidStreamNoRead
		{
			get
			{
				return TomSR.Keys.GetString("Exception_InvalidStreamNoRead");
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x0008119E File Offset: 0x0007F39E
		public static string Exception_NoDocsInContext
		{
			get
			{
				return TomSR.Keys.GetString("Exception_NoDocsInContext");
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x000811AA File Offset: 0x0007F3AA
		public static string Exception_TmdlSerializationSupport
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TmdlSerializationSupport");
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x000811B6 File Offset: 0x0007F3B6
		public static string Exception_JsonSerializationSupport
		{
			get
			{
				return TomSR.Keys.GetString("Exception_JsonSerializationSupport");
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x000811C2 File Offset: 0x0007F3C2
		public static string Exception_ModelObjectInModelUpdateFromContext
		{
			get
			{
				return TomSR.Keys.GetString("Exception_ModelObjectInModelUpdateFromContext");
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x000811CE File Offset: 0x0007F3CE
		public static string Exception_UnnamedObjectInTmdlObjectSerialization
		{
			get
			{
				return TomSR.Keys.GetString("Exception_UnnamedObjectInTmdlObjectSerialization");
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x000811DA File Offset: 0x0007F3DA
		public static string Exception_InvalidCompatModeOnTmdlSerialization
		{
			get
			{
				return TomSR.Keys.GetString("Exception_InvalidCompatModeOnTmdlSerialization");
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x000811E6 File Offset: 0x0007F3E6
		public static string Exception_InvalidOpContextOnZipController
		{
			get
			{
				return TomSR.Keys.GetString("Exception_InvalidOpContextOnZipController");
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x000811F2 File Offset: 0x0007F3F2
		public static string Exception_TmdlParserInvalidStructPropertyDefaultPropertyValueType
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TmdlParserInvalidStructPropertyDefaultPropertyValueType");
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x000811FE File Offset: 0x0007F3FE
		public static string Exception_TmdlParserSingleCharQuoteString
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TmdlParserSingleCharQuoteString");
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x0008120A File Offset: 0x0007F40A
		public static string Exception_TmdlParserMultiStringLine
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TmdlParserMultiStringLine");
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x00081216 File Offset: 0x0007F416
		public static string Exception_TmdlFormatIndentation
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TmdlFormatIndentation");
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x00081222 File Offset: 0x0007F422
		public static string Exception_TmdlFormatNoCollectionItem
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TmdlFormatNoCollectionItem");
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x0008122E File Offset: 0x0007F42E
		public static string Exception_TmdlFormatNoCollectionExpression
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TmdlFormatNoCollectionExpression");
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0008123A File Offset: 0x0007F43A
		public static string Exception_TmdlFormatNoTranslationExpression
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TmdlFormatNoTranslationExpression");
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x00081246 File Offset: 0x0007F446
		public static string Exception_TmdlFormatDefaultPropertyNotInline
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TmdlFormatDefaultPropertyNotInline");
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00081252 File Offset: 0x0007F452
		public static string Exception_TmdlFormatMissingExpressionValue
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TmdlFormatMissingExpressionValue");
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x0008125E File Offset: 0x0007F45E
		public static string Exception_TmdlSerializerInvalidSingleObjectTmdl_DB
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TmdlSerializerInvalidSingleObjectTmdl_DB");
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0008126A File Offset: 0x0007F46A
		public static string Exception_TmdlSerializerInvalidSingleObjectTmdl_Multi
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TmdlSerializerInvalidSingleObjectTmdl_Multi");
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x00081276 File Offset: 0x0007F476
		public static string Exception_TmdlRefObjectCopy
		{
			get
			{
				return TomSR.Keys.GetString("Exception_TmdlRefObjectCopy");
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x00081282 File Offset: 0x0007F482
		public static string ObjetNameParseError_NotFullyConsumed
		{
			get
			{
				return TomSR.Keys.GetString("ObjetNameParseError_NotFullyConsumed");
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x0008128E File Offset: 0x0007F48E
		public static string ObjetNameParseError_EndWithDot
		{
			get
			{
				return TomSR.Keys.GetString("ObjetNameParseError_EndWithDot");
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x0008129A File Offset: 0x0007F49A
		public static string ObjetNameParseError_Empty
		{
			get
			{
				return TomSR.Keys.GetString("ObjetNameParseError_Empty");
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x000812A6 File Offset: 0x0007F4A6
		public static string ObjetNameParseError_EmptyPart
		{
			get
			{
				return TomSR.Keys.GetString("ObjetNameParseError_EmptyPart");
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x000812B2 File Offset: 0x0007F4B2
		public static string ObjetNameParseError_ControlChar
		{
			get
			{
				return TomSR.Keys.GetString("ObjetNameParseError_ControlChar");
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x000812BE File Offset: 0x0007F4BE
		public static string ObjetNameParseError_SingleQuoteNeedEscape
		{
			get
			{
				return TomSR.Keys.GetString("ObjetNameParseError_SingleQuoteNeedEscape");
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x000812CA File Offset: 0x0007F4CA
		public static string ObjetNameParseError_NameIsFollowedByInvalidToken
		{
			get
			{
				return TomSR.Keys.GetString("ObjetNameParseError_NameIsFollowedByInvalidToken");
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x000812D6 File Offset: 0x0007F4D6
		public static string TmdlAmbiguousSourceError_DuplicateDescription
		{
			get
			{
				return TomSR.Keys.GetString("TmdlAmbiguousSourceError_DuplicateDescription");
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x000812E2 File Offset: 0x0007F4E2
		public static string TmdlAmbiguousSourceError_DuplicateOrdinal
		{
			get
			{
				return TomSR.Keys.GetString("TmdlAmbiguousSourceError_DuplicateOrdinal");
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x000812EE File Offset: 0x0007F4EE
		public static string TmdlAmbiguousSourceError_RoleMembers
		{
			get
			{
				return TomSR.Keys.GetString("TmdlAmbiguousSourceError_RoleMembers");
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x000812FA File Offset: 0x0007F4FA
		public static string TmdlFormatError_DescriptionWithoutObject
		{
			get
			{
				return TomSR.Keys.GetString("TmdlFormatError_DescriptionWithoutObject");
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x00081306 File Offset: 0x0007F506
		public static string TmdlFormatError_RefAfterDescription
		{
			get
			{
				return TomSR.Keys.GetString("TmdlFormatError_RefAfterDescription");
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00081312 File Offset: 0x0007F512
		public static string TmdlFormatError_RefWithDefaultProperty
		{
			get
			{
				return TomSR.Keys.GetString("TmdlFormatError_RefWithDefaultProperty");
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0008131E File Offset: 0x0007F51E
		public static string TmdlFormatError_RefWithProperty
		{
			get
			{
				return TomSR.Keys.GetString("TmdlFormatError_RefWithProperty");
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0008132A File Offset: 0x0007F52A
		public static string TmdlFormatError_NotPropertyLine
		{
			get
			{
				return TomSR.Keys.GetString("TmdlFormatError_NotPropertyLine");
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x00081336 File Offset: 0x0007F536
		public static string TmdlFormatError_NotStringPropertyLine
		{
			get
			{
				return TomSR.Keys.GetString("TmdlFormatError_NotStringPropertyLine");
			}
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00081342 File Offset: 0x0007F542
		public static string Exception_InternalErrorOccured_Detailed(string details)
		{
			return TomSR.Keys.GetString("Exception_InternalErrorOccured_Detailed", details);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0008134F File Offset: 0x0007F54F
		public static string Exception_UnrecognizedValueOfType(string Type, string Value)
		{
			return TomSR.Keys.GetString("Exception_UnrecognizedValueOfType", Type, Value);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0008135D File Offset: 0x0007F55D
		public static string Exception_CompatLevelOutOfRange(string compatLevel, string minCompatLevel)
		{
			return TomSR.Keys.GetString("Exception_CompatLevelOutOfRange", compatLevel, minCompatLevel);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0008136B File Offset: 0x0007F56B
		public static string Exception_CompatRestriction_UnsupportedForMode(string mode, string requestingPath)
		{
			return TomSR.Keys.GetString("Exception_CompatRestriction_UnsupportedForMode", mode, requestingPath);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00081379 File Offset: 0x0007F579
		public static string Exception_CompatRestriction_ViolationForMode(string mode, string compatLevel, string minCompatLevel, string requestingPath)
		{
			return TomSR.Keys.GetString("Exception_CompatRestriction_ViolationForMode", mode, compatLevel, minCompatLevel, requestingPath);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00081389 File Offset: 0x0007F589
		public static string Exception_CompatRestriction_InvalidForAllModes(string requestingPath)
		{
			return TomSR.Keys.GetString("Exception_CompatRestriction_InvalidForAllModes", requestingPath);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00081396 File Offset: 0x0007F596
		public static string Exception_CanNotReadFindTypePropObject(string objectType)
		{
			return TomSR.Keys.GetString("Exception_CanNotReadFindTypePropObject", objectType);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x000813A3 File Offset: 0x0007F5A3
		public static string Exception_ObjectIsAlreadyMarkedAsReadOnly(string type)
		{
			return TomSR.Keys.GetString("Exception_ObjectIsAlreadyMarkedAsReadOnly", type);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x000813B0 File Offset: 0x0007F5B0
		public static string Exception_ObjectMarkedAsReadOnly(string type)
		{
			return TomSR.Keys.GetString("Exception_ObjectMarkedAsReadOnly", type);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x000813BD File Offset: 0x0007F5BD
		public static string Exception_CannotFindItemWithId(string id)
		{
			return TomSR.Keys.GetString("Exception_CannotFindItemWithId", id);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000813CA File Offset: 0x0007F5CA
		public static string Exception_ObjectWithNameNotExistInCollection(string name)
		{
			return TomSR.Keys.GetString("Exception_ObjectWithNameNotExistInCollection", name);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x000813D7 File Offset: 0x0007F5D7
		public static string Exception_CollectionAlreadyContainsObjectWithName(string name)
		{
			return TomSR.Keys.GetString("Exception_CollectionAlreadyContainsObjectWithName", name);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000813E4 File Offset: 0x0007F5E4
		public static string Exception_CollectionAlreadyContainsObjectWithTag(string tag)
		{
			return TomSR.Keys.GetString("Exception_CollectionAlreadyContainsObjectWithTag", tag);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000813F1 File Offset: 0x0007F5F1
		public static string Exception_ItemAlreadyPresentInCollection(string name)
		{
			return TomSR.Keys.GetString("Exception_ItemAlreadyPresentInCollection", name);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000813FE File Offset: 0x0007F5FE
		public static string Exception_CannotRemoveItemByNameDuringCopy(string name)
		{
			return TomSR.Keys.GetString("Exception_CannotRemoveItemByNameDuringCopy", name);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0008140B File Offset: 0x0007F60B
		public static string Exception_CannotConvert(string value, string FromType, string ToType)
		{
			return TomSR.Keys.GetString("Exception_CannotConvert", value, FromType, ToType);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0008141A File Offset: 0x0007F61A
		public static string Exception_CommitTransactionFailed(string error)
		{
			return TomSR.Keys.GetString("Exception_CommitTransactionFailed", error);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00081427 File Offset: 0x0007F627
		public static string Exception_SaveModelChangesFailed(string error)
		{
			return TomSR.Keys.GetString("Exception_SaveModelChangesFailed", error);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00081434 File Offset: 0x0007F634
		public static string Exception_FailedToGenerateXmlaRequest(string error)
		{
			return TomSR.Keys.GetString("Exception_FailedToGenerateXmlaRequest", error);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00081441 File Offset: 0x0007F641
		public static string Exception_ExecuteXmlaFailed(string error)
		{
			return TomSR.Keys.GetString("Exception_ExecuteXmlaFailed", error);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0008144E File Offset: 0x0007F64E
		public static string Exception_DiscoverModelFailed(string error)
		{
			return TomSR.Keys.GetString("Exception_DiscoverModelFailed", error);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0008145B File Offset: 0x0007F65B
		public static string Exception_DiscoverModelFailedBadDb(string dbName)
		{
			return TomSR.Keys.GetString("Exception_DiscoverModelFailedBadDb", dbName);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00081468 File Offset: 0x0007F668
		public static string Exception_DatabaseModelHasLocalChanges(string dbName)
		{
			return TomSR.Keys.GetString("Exception_DatabaseModelHasLocalChanges", dbName);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00081475 File Offset: 0x0007F675
		public static string Exception_DatabaseDoesNotExist(string dbName)
		{
			return TomSR.Keys.GetString("Exception_DatabaseDoesNotExist", dbName);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00081482 File Offset: 0x0007F682
		public static string Exception_CannotSyncModelOfDirtyDatabase(string dbName)
		{
			return TomSR.Keys.GetString("Exception_CannotSyncModelOfDirtyDatabase", dbName);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0008148F File Offset: 0x0007F68F
		public static string Exception_UnknownLinkType(string linkType)
		{
			return TomSR.Keys.GetString("Exception_UnknownLinkType", linkType);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0008149C File Offset: 0x0007F69C
		public static string Exception_ObjectDoesNotHaveName(string objectType)
		{
			return TomSR.Keys.GetString("Exception_ObjectDoesNotHaveName", objectType);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000814A9 File Offset: 0x0007F6A9
		public static string Exception_CustomPropertyAssignedToMultipleObjects(string propName)
		{
			return TomSR.Keys.GetString("Exception_CustomPropertyAssignedToMultipleObjects", propName);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000814B6 File Offset: 0x0007F6B6
		public static string Exception_InvalidIndentationLength(string length)
		{
			return TomSR.Keys.GetString("Exception_InvalidIndentationLength", length);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x000814C3 File Offset: 0x0007F6C3
		public static string Exception_InvalidBaseIndentationLevel(string level)
		{
			return TomSR.Keys.GetString("Exception_InvalidBaseIndentationLevel", level);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000814D0 File Offset: 0x0007F6D0
		public static string ObjectPath_Model_1Arg(string modelName)
		{
			return TomSR.Keys.GetString("ObjectPath_Model_1Arg", modelName);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x000814DD File Offset: 0x0007F6DD
		public static string ObjectPath_DataSource_1Arg(string dataSourceName)
		{
			return TomSR.Keys.GetString("ObjectPath_DataSource_1Arg", dataSourceName);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x000814EA File Offset: 0x0007F6EA
		public static string ObjectPath_Function_1Arg(string functionName)
		{
			return TomSR.Keys.GetString("ObjectPath_Function_1Arg", functionName);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x000814F7 File Offset: 0x0007F6F7
		public static string ObjectPath_Table_1Arg(string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_Table_1Arg", tableName);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00081504 File Offset: 0x0007F704
		public static string ObjectPath_Perspective_1Arg(string perspectiveName)
		{
			return TomSR.Keys.GetString("ObjectPath_Perspective_1Arg", perspectiveName);
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00081511 File Offset: 0x0007F711
		public static string ObjectPath_Column_1Arg(string columnName)
		{
			return TomSR.Keys.GetString("ObjectPath_Column_1Arg", columnName);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0008151E File Offset: 0x0007F71E
		public static string ObjectPath_Column_2Args(string columnName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_Column_2Args", columnName, tableName);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0008152C File Offset: 0x0007F72C
		public static string ObjectPath_AttributeHierarchy_1Arg(string columnName)
		{
			return TomSR.Keys.GetString("ObjectPath_AttributeHierarchy_1Arg", columnName);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00081539 File Offset: 0x0007F739
		public static string ObjectPath_AttributeHierarchy_2Args(string columnName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_AttributeHierarchy_2Args", columnName, tableName);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00081547 File Offset: 0x0007F747
		public static string ObjectPath_Partition_1Arg(string partitionName)
		{
			return TomSR.Keys.GetString("ObjectPath_Partition_1Arg", partitionName);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00081554 File Offset: 0x0007F754
		public static string ObjectPath_Partition_2Args(string partitionName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_Partition_2Args", partitionName, tableName);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00081562 File Offset: 0x0007F762
		public static string ObjectPath_Relationship_1Arg(string relationshipName)
		{
			return TomSR.Keys.GetString("ObjectPath_Relationship_1Arg", relationshipName);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0008156F File Offset: 0x0007F76F
		public static string ObjectPath_Measure_1Arg(string measureName)
		{
			return TomSR.Keys.GetString("ObjectPath_Measure_1Arg", measureName);
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0008157C File Offset: 0x0007F77C
		public static string ObjectPath_Measure_2Args(string measureName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_Measure_2Args", measureName, tableName);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0008158A File Offset: 0x0007F78A
		public static string ObjectPath_Hierarchy_1Arg(string hierarchyName)
		{
			return TomSR.Keys.GetString("ObjectPath_Hierarchy_1Arg", hierarchyName);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00081597 File Offset: 0x0007F797
		public static string ObjectPath_Hierarchy_2Args(string hierarchyName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_Hierarchy_2Args", hierarchyName, tableName);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x000815A5 File Offset: 0x0007F7A5
		public static string ObjectPath_Level_1Arg(string levelName)
		{
			return TomSR.Keys.GetString("ObjectPath_Level_1Arg", levelName);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x000815B2 File Offset: 0x0007F7B2
		public static string ObjectPath_Level_2Args(string levelName, string hierarchyName)
		{
			return TomSR.Keys.GetString("ObjectPath_Level_2Args", levelName, hierarchyName);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x000815C0 File Offset: 0x0007F7C0
		public static string ObjectPath_Level_3Args(string levelName, string hierarchyName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_Level_3Args", levelName, hierarchyName, tableName);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x000815CF File Offset: 0x0007F7CF
		public static string ObjectPath_Annotation_1Arg(string annotationName)
		{
			return TomSR.Keys.GetString("ObjectPath_Annotation_1Arg", annotationName);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000815DC File Offset: 0x0007F7DC
		public static string ObjectPath_KPI_1Arg(string measureName)
		{
			return TomSR.Keys.GetString("ObjectPath_KPI_1Arg", measureName);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000815E9 File Offset: 0x0007F7E9
		public static string ObjectPath_KPI_2Args(string measureName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_KPI_2Args", measureName, tableName);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x000815F7 File Offset: 0x0007F7F7
		public static string ObjectPath_Culture_1Arg(string cultureName)
		{
			return TomSR.Keys.GetString("ObjectPath_Culture_1Arg", cultureName);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00081604 File Offset: 0x0007F804
		public static string ObjectPath_LinguisticMetadata_1Arg(string cultureName)
		{
			return TomSR.Keys.GetString("ObjectPath_LinguisticMetadata_1Arg", cultureName);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00081611 File Offset: 0x0007F811
		public static string ObjectPath_ObjectTranslation_1Arg(string cultureName)
		{
			return TomSR.Keys.GetString("ObjectPath_ObjectTranslation_1Arg", cultureName);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0008161E File Offset: 0x0007F81E
		public static string ObjectPath_PerspectiveTable_1Args(string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_PerspectiveTable_1Args", tableName);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0008162B File Offset: 0x0007F82B
		public static string ObjectPath_PerspectiveTable_2Args(string tableName, string perspectiveName)
		{
			return TomSR.Keys.GetString("ObjectPath_PerspectiveTable_2Args", tableName, perspectiveName);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00081639 File Offset: 0x0007F839
		public static string ObjectPath_PerspectiveColumn_1Args(string columnName)
		{
			return TomSR.Keys.GetString("ObjectPath_PerspectiveColumn_1Args", columnName);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00081646 File Offset: 0x0007F846
		public static string ObjectPath_PerspectiveColumn_2Args(string columnName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_PerspectiveColumn_2Args", columnName, tableName);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00081654 File Offset: 0x0007F854
		public static string ObjectPath_PerspectiveColumn_3Args(string columnName, string tableName, string perspectiveName)
		{
			return TomSR.Keys.GetString("ObjectPath_PerspectiveColumn_3Args", columnName, tableName, perspectiveName);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00081663 File Offset: 0x0007F863
		public static string ObjectPath_PerspectiveHierarchy_1Args(string hierarchyName)
		{
			return TomSR.Keys.GetString("ObjectPath_PerspectiveHierarchy_1Args", hierarchyName);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00081670 File Offset: 0x0007F870
		public static string ObjectPath_PerspectiveHierarchy_2Args(string hierarchyName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_PerspectiveHierarchy_2Args", hierarchyName, tableName);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0008167E File Offset: 0x0007F87E
		public static string ObjectPath_PerspectiveHierarchy_3Args(string hierarchyName, string tableName, string perspectiveName)
		{
			return TomSR.Keys.GetString("ObjectPath_PerspectiveHierarchy_3Args", hierarchyName, tableName, perspectiveName);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0008168D File Offset: 0x0007F88D
		public static string ObjectPath_PerspectiveMeasure_1Args(string measureName)
		{
			return TomSR.Keys.GetString("ObjectPath_PerspectiveMeasure_1Args", measureName);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0008169A File Offset: 0x0007F89A
		public static string ObjectPath_PerspectiveMeasure_2Args(string measureName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_PerspectiveMeasure_2Args", measureName, tableName);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x000816A8 File Offset: 0x0007F8A8
		public static string ObjectPath_PerspectiveMeasure_3Args(string measureName, string tableName, string perspectiveName)
		{
			return TomSR.Keys.GetString("ObjectPath_PerspectiveMeasure_3Args", measureName, tableName, perspectiveName);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x000816B7 File Offset: 0x0007F8B7
		public static string ObjectPath_Role_1Arg(string roleName)
		{
			return TomSR.Keys.GetString("ObjectPath_Role_1Arg", roleName);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000816C4 File Offset: 0x0007F8C4
		public static string ObjectPath_RoleMembership_2Args(string roleMemberName, string roleName)
		{
			return TomSR.Keys.GetString("ObjectPath_RoleMembership_2Args", roleMemberName, roleName);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x000816D2 File Offset: 0x0007F8D2
		public static string ObjectPath_RoleMembership_1Arg(string roleMemberName)
		{
			return TomSR.Keys.GetString("ObjectPath_RoleMembership_1Arg", roleMemberName);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x000816DF File Offset: 0x0007F8DF
		public static string ObjectPath_TablePermission_2Args(string tableName, string roleName)
		{
			return TomSR.Keys.GetString("ObjectPath_TablePermission_2Args", tableName, roleName);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x000816ED File Offset: 0x0007F8ED
		public static string ObjectPath_TablePermission_1Arg(string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_TablePermission_1Arg", tableName);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x000816FA File Offset: 0x0007F8FA
		public static string ObjectPath_Variation_1Arg(string variationName)
		{
			return TomSR.Keys.GetString("ObjectPath_Variation_1Arg", variationName);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00081707 File Offset: 0x0007F907
		public static string ObjectPath_Variation_2Args(string variationName, string columnName)
		{
			return TomSR.Keys.GetString("ObjectPath_Variation_2Args", variationName, columnName);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00081715 File Offset: 0x0007F915
		public static string ObjectPath_Variation_3Args(string variationName, string columnName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_Variation_3Args", variationName, columnName, tableName);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00081724 File Offset: 0x0007F924
		public static string ObjectPath_Set_1Arg(string setName)
		{
			return TomSR.Keys.GetString("ObjectPath_Set_1Arg", setName);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00081731 File Offset: 0x0007F931
		public static string ObjectPath_Set_2Args(string setName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_Set_2Args", setName, tableName);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0008173F File Offset: 0x0007F93F
		public static string ObjectPath_PerspectiveSet_1Args(string setName)
		{
			return TomSR.Keys.GetString("ObjectPath_PerspectiveSet_1Args", setName);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0008174C File Offset: 0x0007F94C
		public static string ObjectPath_PerspectiveSet_2Args(string setName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_PerspectiveSet_2Args", setName, tableName);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0008175A File Offset: 0x0007F95A
		public static string ObjectPath_PerspectiveSet_3Args(string setName, string tableName, string perspectiveName)
		{
			return TomSR.Keys.GetString("ObjectPath_PerspectiveSet_3Args", setName, tableName, perspectiveName);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00081769 File Offset: 0x0007F969
		public static string ObjectPath_ExtendedProperty_1Arg(string annotationName)
		{
			return TomSR.Keys.GetString("ObjectPath_ExtendedProperty_1Arg", annotationName);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00081776 File Offset: 0x0007F976
		public static string ObjectPath_Expression_1Arg(string expressionName)
		{
			return TomSR.Keys.GetString("ObjectPath_Expression_1Arg", expressionName);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00081783 File Offset: 0x0007F983
		public static string ObjectPath_ColumnPermission_2Args(string columnName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_ColumnPermission_2Args", columnName, tableName);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00081791 File Offset: 0x0007F991
		public static string ObjectPath_ColumnPermission_1Arg(string columnName)
		{
			return TomSR.Keys.GetString("ObjectPath_ColumnPermission_1Arg", columnName);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0008179E File Offset: 0x0007F99E
		public static string ObjectPath_DetailRowsDefinition_1Arg_Measure(string measureName)
		{
			return TomSR.Keys.GetString("ObjectPath_DetailRowsDefinition_1Arg_Measure", measureName);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000817AB File Offset: 0x0007F9AB
		public static string ObjectPath_DetailRowsDefinition_1Arg_Table(string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_DetailRowsDefinition_1Arg_Table", tableName);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000817B8 File Offset: 0x0007F9B8
		public static string ObjectPath_DetailRowsDefinition_2Args(string measureName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_DetailRowsDefinition_2Args", measureName, tableName);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x000817C6 File Offset: 0x0007F9C6
		public static string ObjectPath_RelatedColumnDetails_1Args(string columnName)
		{
			return TomSR.Keys.GetString("ObjectPath_RelatedColumnDetails_1Args", columnName);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000817D3 File Offset: 0x0007F9D3
		public static string ObjectPath_RelatedColumnDetails_2Args(string columnName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_RelatedColumnDetails_2Args", columnName, tableName);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x000817E1 File Offset: 0x0007F9E1
		public static string ObjectPath_GroupByColumn_1Args(string columnName)
		{
			return TomSR.Keys.GetString("ObjectPath_GroupByColumn_1Args", columnName);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000817EE File Offset: 0x0007F9EE
		public static string ObjectPath_GroupByColumn_2Args(string columnName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_GroupByColumn_2Args", columnName, tableName);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000817FC File Offset: 0x0007F9FC
		public static string ObjectPath_AlternateOf_1Arg_Table(string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_AlternateOf_1Arg_Table", tableName);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00081809 File Offset: 0x0007FA09
		public static string ObjectPath_AlternateOf_2Args_Column(string columnName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_AlternateOf_2Args_Column", columnName, tableName);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00081817 File Offset: 0x0007FA17
		public static string ObjectPath_CalculationGroup_1Args(string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_CalculationGroup_1Args", tableName);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00081824 File Offset: 0x0007FA24
		public static string ObjectPath_CalculationItem_1Args(string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_CalculationItem_1Args", tableName);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00081831 File Offset: 0x0007FA31
		public static string ObjectPath_RefreshPolicy_1Args_Table(string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_RefreshPolicy_1Args_Table", tableName);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0008183E File Offset: 0x0007FA3E
		public static string ObjectPath_FormatStringDefinition_1Args_CalculationItem(string CalculationItemName)
		{
			return TomSR.Keys.GetString("ObjectPath_FormatStringDefinition_1Args_CalculationItem", CalculationItemName);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0008184B File Offset: 0x0007FA4B
		public static string ObjectPath_FormatStringDefinition_1Args_Measure(string measureName)
		{
			return TomSR.Keys.GetString("ObjectPath_FormatStringDefinition_1Args_Measure", measureName);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00081858 File Offset: 0x0007FA58
		public static string ObjectPath_FormatStringDefinition_1Args_CalculationGroupExpression(string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_FormatStringDefinition_1Args_CalculationGroupExpression", tableName);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00081865 File Offset: 0x0007FA65
		public static string ObjectPath_QueryGroup_1Args_Folder(string folder)
		{
			return TomSR.Keys.GetString("ObjectPath_QueryGroup_1Args_Folder", folder);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00081872 File Offset: 0x0007FA72
		public static string ObjectPath_AnalyticsAIMetadata_1Args(string analyticsAIMetadataName)
		{
			return TomSR.Keys.GetString("ObjectPath_AnalyticsAIMetadata_1Args", analyticsAIMetadataName);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0008187F File Offset: 0x0007FA7F
		public static string ObjectPath_ChangedProperty_1Args(string ChangedPropertyName)
		{
			return TomSR.Keys.GetString("ObjectPath_ChangedProperty_1Args", ChangedPropertyName);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0008188C File Offset: 0x0007FA8C
		public static string ObjectPath_ExcludedArtifact_1Args(string ExcludedArtifactName)
		{
			return TomSR.Keys.GetString("ObjectPath_ExcludedArtifact_1Args", ExcludedArtifactName);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00081899 File Offset: 0x0007FA99
		public static string ObjectPath_DataCoverageDefinition_1Args(string partitionName)
		{
			return TomSR.Keys.GetString("ObjectPath_DataCoverageDefinition_1Args", partitionName);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x000818A6 File Offset: 0x0007FAA6
		public static string ObjectPath_CalculationGroupExpression_1Args_CalculationGroup(string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_CalculationGroupExpression_1Args_CalculationGroup", tableName);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x000818B3 File Offset: 0x0007FAB3
		public static string ObjectPath_Calendar_1Arg(string calendarName)
		{
			return TomSR.Keys.GetString("ObjectPath_Calendar_1Arg", calendarName);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x000818C0 File Offset: 0x0007FAC0
		public static string ObjectPath_Calendar_2Args(string calendarName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_Calendar_2Args", calendarName, tableName);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x000818CE File Offset: 0x0007FACE
		public static string ObjectPath_TimeUnitColumnAssociation_1Arg(string timeUnit)
		{
			return TomSR.Keys.GetString("ObjectPath_TimeUnitColumnAssociation_1Arg", timeUnit);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x000818DB File Offset: 0x0007FADB
		public static string ObjectPath_TimeUnitColumnAssociation_2Args(string timeUnit, string calendarName)
		{
			return TomSR.Keys.GetString("ObjectPath_TimeUnitColumnAssociation_2Args", timeUnit, calendarName);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000818E9 File Offset: 0x0007FAE9
		public static string ObjectPath_TimeUnitColumnAssociation_3Args(string timeUnit, string calendarName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_TimeUnitColumnAssociation_3Args", timeUnit, calendarName, tableName);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x000818F8 File Offset: 0x0007FAF8
		public static string ObjectPath_CalendarColumnReference_1Arg(string columnName)
		{
			return TomSR.Keys.GetString("ObjectPath_CalendarColumnReference_1Arg", columnName);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00081905 File Offset: 0x0007FB05
		public static string ObjectPath_CalendarColumnReference_2Args(string columnName, string timeUnit)
		{
			return TomSR.Keys.GetString("ObjectPath_CalendarColumnReference_2Args", columnName, timeUnit);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00081913 File Offset: 0x0007FB13
		public static string ObjectPath_CalendarColumnReference_3Args(string columnName, string timeUnit, string calendarName)
		{
			return TomSR.Keys.GetString("ObjectPath_CalendarColumnReference_3Args", columnName, timeUnit, calendarName);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00081922 File Offset: 0x0007FB22
		public static string ObjectPath_CalendarColumnReference_4Args(string columnName, string timeUnit, string calendarName, string tableName)
		{
			return TomSR.Keys.GetString("ObjectPath_CalendarColumnReference_4Args", columnName, timeUnit, calendarName, tableName);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00081932 File Offset: 0x0007FB32
		public static string ObjectPath_BindingInfo_1Arg(string bindingInfoName)
		{
			return TomSR.Keys.GetString("ObjectPath_BindingInfo_1Arg", bindingInfoName);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0008193F File Offset: 0x0007FB3F
		public static string Error_NameIsTooLong(string name, string objectType, string length, string maxLength)
		{
			return TomSR.Keys.GetString("Error_NameIsTooLong", name, objectType, length, maxLength);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0008194F File Offset: 0x0007FB4F
		public static string Error_NameCannotBeReservedString(string objectType, string reservedString)
		{
			return TomSR.Keys.GetString("Error_NameCannotBeReservedString", objectType, reservedString);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0008195D File Offset: 0x0007FB5D
		public static string Error_NameHasInvalidXmlCharacters(string invalidName)
		{
			return TomSR.Keys.GetString("Error_NameHasInvalidXmlCharacters", invalidName);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0008196A File Offset: 0x0007FB6A
		public static string ValidNameForReservedStringPattern(string reservedName)
		{
			return TomSR.Keys.GetString("ValidNameForReservedStringPattern", reservedName);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00081977 File Offset: 0x0007FB77
		public static string Error_ValueHasInvalidCharacters(string invalidChar)
		{
			return TomSR.Keys.GetString("Error_ValueHasInvalidCharacters", invalidChar);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00081984 File Offset: 0x0007FB84
		public static string Exception_Json_TableMustHaveSinglePartitionForMergedMode(string tableName)
		{
			return TomSR.Keys.GetString("Exception_Json_TableMustHaveSinglePartitionForMergedMode", tableName);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00081991 File Offset: 0x0007FB91
		public static string Exception_Json_PartitionMustHaveSameNameAsParentTableForMergedMode(string tableName)
		{
			return TomSR.Keys.GetString("Exception_Json_PartitionMustHaveSameNameAsParentTableForMergedMode", tableName);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0008199E File Offset: 0x0007FB9E
		public static string Exception_Json_PartitionMustHaveEmptyDescriptionForMergedMode(string tableName)
		{
			return TomSR.Keys.GetString("Exception_Json_PartitionMustHaveEmptyDescriptionForMergedMode", tableName);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x000819AB File Offset: 0x0007FBAB
		public static string Exception_Json_PartitionMustHaveNoAnnotationsForMergedMode(string tableName)
		{
			return TomSR.Keys.GetString("Exception_Json_PartitionMustHaveNoAnnotationsForMergedMode", tableName);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x000819B8 File Offset: 0x0007FBB8
		public static string Exception_CannotSerializeObject(string objectType)
		{
			return TomSR.Keys.GetString("Exception_CannotSerializeObject", objectType);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x000819C5 File Offset: 0x0007FBC5
		public static string Exception_CannotDeserializeObjectMalformedInput(string objectType)
		{
			return TomSR.Keys.GetString("Exception_CannotDeserializeObjectMalformedInput", objectType);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000819D2 File Offset: 0x0007FBD2
		public static string Exception_CannotDeserializeObject(string objectType)
		{
			return TomSR.Keys.GetString("Exception_CannotDeserializeObject", objectType);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x000819DF File Offset: 0x0007FBDF
		public static string Exception_CannotDeserializeObjectWithDetails(string objectType, string details)
		{
			return TomSR.Keys.GetString("Exception_CannotDeserializeObjectWithDetails", objectType, details);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x000819ED File Offset: 0x0007FBED
		public static string Exception_CannotDeserializeObjectWrongType(string realType, string requiredType)
		{
			return TomSR.Keys.GetString("Exception_CannotDeserializeObjectWrongType", realType, requiredType);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x000819FB File Offset: 0x0007FBFB
		public static string Exception_CannotDeserializeObjectUnknownType(string type)
		{
			return TomSR.Keys.GetString("Exception_CannotDeserializeObjectUnknownType", type);
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00081A08 File Offset: 0x0007FC08
		public static string Exception_CannotReadPropertyObjectExpected(string type, string propName)
		{
			return TomSR.Keys.GetString("Exception_CannotReadPropertyObjectExpected", type, propName);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00081A16 File Offset: 0x0007FC16
		public static string Exception_CannotConvertToType(string value, string ToType)
		{
			return TomSR.Keys.GetString("Exception_CannotConvertToType", value, ToType);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00081A24 File Offset: 0x0007FC24
		public static string Exception_Json_MissingRequiredProperty(string propName)
		{
			return TomSR.Keys.GetString("Exception_Json_MissingRequiredProperty", propName);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00081A31 File Offset: 0x0007FC31
		public static string Exception_ErrorWithPathAndLineInfo(string errorMessage, string path, string lineNumber, string positionNumber)
		{
			return TomSR.Keys.GetString("Exception_ErrorWithPathAndLineInfo", errorMessage, path, lineNumber, positionNumber);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00081A41 File Offset: 0x0007FC41
		public static string Exception_CannotGenerateSchemaForUnknownType(string type)
		{
			return TomSR.Keys.GetString("Exception_CannotGenerateSchemaForUnknownType", type);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00081A4E File Offset: 0x0007FC4E
		public static string Exception_CannotDeserializeObjectResolvePathsFailed(string objectType)
		{
			return TomSR.Keys.GetString("Exception_CannotDeserializeObjectResolvePathsFailed", objectType);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00081A5B File Offset: 0x0007FC5B
		public static string Exception_CannotDeserializeObjectResolvePathsFailedWithList(string objectType, string LinkName)
		{
			return TomSR.Keys.GetString("Exception_CannotDeserializeObjectResolvePathsFailedWithList", objectType, LinkName);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00081A69 File Offset: 0x0007FC69
		public static string Exception_PropertyHasInvalidJsonContent(string propName, string details)
		{
			return TomSR.Keys.GetString("Exception_PropertyHasInvalidJsonContent", propName, details);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00081A77 File Offset: 0x0007FC77
		public static string Exception_UnsupportedJsonPrimitiveType(string propName)
		{
			return TomSR.Keys.GetString("Exception_UnsupportedJsonPrimitiveType", propName);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00081A84 File Offset: 0x0007FC84
		public static string Exception_CustomPropertyValueHasInvalidType(string propName, string expectedType, string actualType)
		{
			return TomSR.Keys.GetString("Exception_CustomPropertyValueHasInvalidType", propName, expectedType, actualType);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00081A93 File Offset: 0x0007FC93
		public static string Validation_UnresolvedLink(string linkPropName, string sourceObject)
		{
			return TomSR.Keys.GetString("Validation_UnresolvedLink", linkPropName, sourceObject);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00081AA1 File Offset: 0x0007FCA1
		public static string Validation_LinkToRemovedObject(string linkPropName, string sourceObject)
		{
			return TomSR.Keys.GetString("Validation_LinkToRemovedObject", linkPropName, sourceObject);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00081AAF File Offset: 0x0007FCAF
		public static string Validation_LinkToAnotherModel(string linkPropName, string sourceObject)
		{
			return TomSR.Keys.GetString("Validation_LinkToAnotherModel", linkPropName, sourceObject);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00081ABD File Offset: 0x0007FCBD
		public static string Exception_CannotReadMetadataObjectCollectionWithTypeFromJson(string type)
		{
			return TomSR.Keys.GetString("Exception_CannotReadMetadataObjectCollectionWithTypeFromJson", type);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00081ACA File Offset: 0x0007FCCA
		public static string Exception_ObjectTranslationAlreadyContainsTranslation(string property, string translatedObjectType)
		{
			return TomSR.Keys.GetString("Exception_ObjectTranslationAlreadyContainsTranslation", property, translatedObjectType);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00081AD8 File Offset: 0x0007FCD8
		public static string Exception_CantChangeImmutableProperty(string propName, string className)
		{
			return TomSR.Keys.GetString("Exception_CantChangeImmutableProperty", propName, className);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00081AE6 File Offset: 0x0007FCE6
		public static string Exception_ModifyDirtyDatabase(string dbName)
		{
			return TomSR.Keys.GetString("Exception_ModifyDirtyDatabase", dbName);
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00081AF3 File Offset: 0x0007FCF3
		public static string Exception_PartialRefreshDirtyDatabase(string dbName)
		{
			return TomSR.Keys.GetString("Exception_PartialRefreshDirtyDatabase", dbName);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00081B00 File Offset: 0x0007FD00
		public static string Exception_ApplyAutomaticAggregationsInvalidCompatLevel(string level, string minLevel)
		{
			return TomSR.Keys.GetString("Exception_ApplyAutomaticAggregationsInvalidCompatLevel", level, minLevel);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00081B0E File Offset: 0x0007FD0E
		public static string Exception_OverridesScopeObjectDoesntSupportRefresh(string scopeObjectType)
		{
			return TomSR.Keys.GetString("Exception_OverridesScopeObjectDoesntSupportRefresh", scopeObjectType);
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00081B1B File Offset: 0x0007FD1B
		public static string Exception_OverridesScopeObjectCannotBeFound(string scopeObjectType)
		{
			return TomSR.Keys.GetString("Exception_OverridesScopeObjectCannotBeFound", scopeObjectType);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00081B28 File Offset: 0x0007FD28
		public static string Exception_OverridesOriginalObjectCannotBeFound(string originalObjectType)
		{
			return TomSR.Keys.GetString("Exception_OverridesOriginalObjectCannotBeFound", originalObjectType);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00081B35 File Offset: 0x0007FD35
		public static string Exception_OverridesOriginalObjectPathIsNull(string originalObjectType)
		{
			return TomSR.Keys.GetString("Exception_OverridesOriginalObjectPathIsNull", originalObjectType);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00081B42 File Offset: 0x0007FD42
		public static string Exception_UnexpectedJsonToken(string expected, string actual)
		{
			return TomSR.Keys.GetString("Exception_UnexpectedJsonToken", expected, actual);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00081B50 File Offset: 0x0007FD50
		public static string Exception_UnexpectedJsonTag(string tag)
		{
			return TomSR.Keys.GetString("Exception_UnexpectedJsonTag", tag);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00081B5D File Offset: 0x0007FD5D
		public static string Exception_UnrecognizedJsonCommand(string command)
		{
			return TomSR.Keys.GetString("Exception_UnrecognizedJsonCommand", command);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00081B6A File Offset: 0x0007FD6A
		public static string Exception_UnexpectedJsonProperty(string propName)
		{
			return TomSR.Keys.GetString("Exception_UnexpectedJsonProperty", propName);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00081B77 File Offset: 0x0007FD77
		public static string Exception_MissingJsonProperty(string propName)
		{
			return TomSR.Keys.GetString("Exception_MissingJsonProperty", propName);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00081B84 File Offset: 0x0007FD84
		public static string Exception_JsonDeserializeObjectInvalidType(string type)
		{
			return TomSR.Keys.GetString("Exception_JsonDeserializeObjectInvalidType", type);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00081B91 File Offset: 0x0007FD91
		public static string Exception_JsonSerializationNotSupportedForObjectType(string objectType)
		{
			return TomSR.Keys.GetString("Exception_JsonSerializationNotSupportedForObjectType", objectType);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00081B9E File Offset: 0x0007FD9E
		public static string Exception_JsonScriptFailedToExecute(string message)
		{
			return TomSR.Keys.GetString("Exception_JsonScriptFailedToExecute", message);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00081BAB File Offset: 0x0007FDAB
		public static string Exception_JsonCommandMustContainExactlyOneObjectDefinition(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandMustContainExactlyOneObjectDefinition", commandName);
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00081BB8 File Offset: 0x0007FDB8
		public static string Exception_JsonCommandObjectNotSpecified(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandObjectNotSpecified", commandName);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00081BC5 File Offset: 0x0007FDC5
		public static string Exception_JsonCommandObjectDefinitionNotSpecified(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandObjectDefinitionNotSpecified", commandName);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00081BD2 File Offset: 0x0007FDD2
		public static string Exception_JsonCommandInvalidObjectSpecified(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandInvalidObjectSpecified", commandName);
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00081BDF File Offset: 0x0007FDDF
		public static string Exception_JsonCommandDatabaseNotSpecified(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandDatabaseNotSpecified", commandName);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00081BEC File Offset: 0x0007FDEC
		public static string Exception_JsonCommandDatabaseNotFound(string commandName, string dbName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandDatabaseNotFound", commandName, dbName);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00081BFA File Offset: 0x0007FDFA
		public static string Exception_JsonCommandNonTabularDatabase(string commandName, string dbName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandNonTabularDatabase", commandName, dbName);
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00081C08 File Offset: 0x0007FE08
		public static string Exception_JsonCommandCannotFindObject(string commandName, string objectName, string path)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandCannotFindObject", commandName, objectName, path);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00081C17 File Offset: 0x0007FE17
		public static string Exception_JsonCommandCannotFindParentObject(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandCannotFindParentObject", commandName);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00081C24 File Offset: 0x0007FE24
		public static string Exception_JsonCommandAlterNotSupportedForObjectType(string objectType)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandAlterNotSupportedForObjectType", objectType);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00081C31 File Offset: 0x0007FE31
		public static string Exception_JsonCommandCreateOrReplaceNotSupportedForObjectType(string objectType)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandCreateOrReplaceNotSupportedForObjectType", objectType);
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00081C3E File Offset: 0x0007FE3E
		public static string Exception_JsonCommandCreateNotSupportedForObjectType(string objectType)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandCreateNotSupportedForObjectType", objectType);
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x00081C4B File Offset: 0x0007FE4B
		public static string Exception_JsonCommandDeleteNotSupportedForObjectType(string objectType)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandDeleteNotSupportedForObjectType", objectType);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00081C58 File Offset: 0x0007FE58
		public static string Exception_JsonCommandExportNotSupportedForObjectType(string objectType)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandExportNotSupportedForObjectType", objectType);
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00081C65 File Offset: 0x0007FE65
		public static string Exception_JsonCommandRefreshNotSupportedForObjectType(string objectType)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandRefreshNotSupportedForObjectType", objectType);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00081C72 File Offset: 0x0007FE72
		public static string Exception_JsonCommandRefreshPolicyNotSupportedForObjectType(string objectType)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandRefreshPolicyNotSupportedForObjectType", objectType);
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00081C7F File Offset: 0x0007FE7F
		public static string Exception_JsonCommandRefreshPolicyNotSupportMoreThanOneObjects(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandRefreshPolicyNotSupportMoreThanOneObjects", commandName);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00081C8C File Offset: 0x0007FE8C
		public static string Exception_JsonCommandRefreshPolicyParameterMissing(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandRefreshPolicyParameterMissing", commandName);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00081C99 File Offset: 0x0007FE99
		public static string Exception_JsonCommandRefreshPolicyNotSupportForRefreshType(string commandName, string refreshType)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandRefreshPolicyNotSupportForRefreshType", commandName, refreshType);
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00081CA7 File Offset: 0x0007FEA7
		public static string Exception_JsonCommandObjectReferenceAndDefinitionMismatch(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandObjectReferenceAndDefinitionMismatch", commandName);
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00081CB4 File Offset: 0x0007FEB4
		public static string Exception_JsonCommandParentObjectNotSpecified(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandParentObjectNotSpecified", commandName);
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00081CC1 File Offset: 0x0007FEC1
		public static string Exception_JsonCommandParentObjectNotNeededForDatabase(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandParentObjectNotNeededForDatabase", commandName);
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00081CCE File Offset: 0x0007FECE
		public static string Exception_JsonCommandInvalidDatabaseName(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandInvalidDatabaseName", commandName);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00081CDB File Offset: 0x0007FEDB
		public static string Exception_JsonCommandChildCollectionNotFound(string commandName, string childType)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandChildCollectionNotFound", commandName, childType);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00081CE9 File Offset: 0x0007FEE9
		public static string Exception_JsonCommandTypeNotSpecified(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandTypeNotSpecified", commandName);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00081CF6 File Offset: 0x0007FEF6
		public static string Exception_JsonCommandParameterIsMissing(string command, string parameter)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandParameterIsMissing", command, parameter);
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00081D04 File Offset: 0x0007FF04
		public static string Exception_JsonCommandMergePartitionsSourceNameIsEmpty(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandMergePartitionsSourceNameIsEmpty", commandName);
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00081D11 File Offset: 0x0007FF11
		public static string Exception_JsonCommandMergePartitionsNoSourcePartitions(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandMergePartitionsNoSourcePartitions", commandName);
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00081D1E File Offset: 0x0007FF1E
		public static string Exception_JsonCommandMergePartitionsNoTargetPartition(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandMergePartitionsNoTargetPartition", commandName);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00081D2B File Offset: 0x0007FF2B
		public static string Exception_JsonCommandMergePartitionsCantFindTargetPartition(string commandName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandMergePartitionsCantFindTargetPartition", commandName);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00081D38 File Offset: 0x0007FF38
		public static string Exception_JsonCommandMergePartitionsCantFindSourcePartition(string commandName, string sourcePartitionName)
		{
			return TomSR.Keys.GetString("Exception_JsonCommandMergePartitionsCantFindSourcePartition", commandName, sourcePartitionName);
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00081D46 File Offset: 0x0007FF46
		public static string Exception_JsonScriptCannotScriptOutMergePartitionsNotSameTable(string sourcePartitionName)
		{
			return TomSR.Keys.GetString("Exception_JsonScriptCannotScriptOutMergePartitionsNotSameTable", sourcePartitionName);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00081D53 File Offset: 0x0007FF53
		public static string Exception_TableRefreshPolicyIsMissing(string tableName)
		{
			return TomSR.Keys.GetString("Exception_TableRefreshPolicyIsMissing", tableName);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x00081D60 File Offset: 0x0007FF60
		public static string Exception_PartitionDoesNotHavePolicyRangePartitionSource(string partitionName, string tableName)
		{
			return TomSR.Keys.GetString("Exception_PartitionDoesNotHavePolicyRangePartitionSource", partitionName, tableName);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00081D6E File Offset: 0x0007FF6E
		public static string Exception_RefreshPolicyInvalidSourceExpression(string tableName)
		{
			return TomSR.Keys.GetString("Exception_RefreshPolicyInvalidSourceExpression", tableName);
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00081D7B File Offset: 0x0007FF7B
		public static string Exception_FailedAddDeserializedObject(string objectType, string error)
		{
			return TomSR.Keys.GetString("Exception_FailedAddDeserializedObject", objectType, error);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x00081D89 File Offset: 0x0007FF89
		public static string Exception_FailedAddDeserializedNamedObject(string objectType, string name, string error)
		{
			return TomSR.Keys.GetString("Exception_FailedAddDeserializedNamedObject", objectType, name, error);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00081D98 File Offset: 0x0007FF98
		public static string Exception_InvalidEscapedString(string s, string c, string index)
		{
			return TomSR.Keys.GetString("Exception_InvalidEscapedString", s, c, index);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00081DA7 File Offset: 0x0007FFA7
		public static string Exception_InvalidChildCollection(string parent, string child)
		{
			return TomSR.Keys.GetString("Exception_InvalidChildCollection", parent, child);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00081DB5 File Offset: 0x0007FFB5
		public static string Exception_TmdlPropertyUnknown(string property)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyUnknown", property);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00081DC2 File Offset: 0x0007FFC2
		public static string Exception_TmdlPropertyIncompatible(string property, string mode, string level)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyIncompatible", property, mode, level);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00081DD1 File Offset: 0x0007FFD1
		public static string Exception_TmdlPropertyIncompatibleValue(string property, string mode, string level, string value)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyIncompatibleValue", property, mode, level, value);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00081DE1 File Offset: 0x0007FFE1
		public static string Exception_TmdlPropertyUnexpected(string property)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyUnexpected", property);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00081DEE File Offset: 0x0007FFEE
		public static string Exception_TmdlPropertyNotExist(string property)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyNotExist", property);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00081DFB File Offset: 0x0007FFFB
		public static string Exception_TmdlPropertyMismatchScalarType(string property, string expectedType, string actualType, string typeCode)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyMismatchScalarType", property, expectedType, actualType, typeCode);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00081E0B File Offset: 0x0008000B
		public static string Exception_TmdlPropertyMismatchEnumType(string property, string expectedType, string actualType)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyMismatchEnumType", property, expectedType, actualType);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00081E1A File Offset: 0x0008001A
		public static string Exception_TmdlPropertyInvalidObjectKeyValue(string objectType, string key)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyInvalidObjectKeyValue", objectType, key);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00081E28 File Offset: 0x00080028
		public static string Exception_TmdlPropertyInvalidEnumValue(string property, string enumType, string enumValue)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyInvalidEnumValue", property, enumType, enumValue);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00081E37 File Offset: 0x00080037
		public static string Exception_TmdlPropertyMismatchValueType(string property)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyMismatchValueType", property);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00081E44 File Offset: 0x00080044
		public static string Exception_TmdlPropertyMismatchNature(string property)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyMismatchNature", property);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00081E51 File Offset: 0x00080051
		public static string Exception_TmdlPropertyUnknownNature(string property)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyUnknownNature", property);
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00081E5E File Offset: 0x0008005E
		public static string Exception_TmdlPropertyMismatchType(string property, string type)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyMismatchType", property, type);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00081E6C File Offset: 0x0008006C
		public static string Exception_TmdlPropertyRequiresTableOnRead(string property)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyRequiresTableOnRead", property);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x00081E79 File Offset: 0x00080079
		public static string Exception_TmdlPropertyNoComplexProperty(string property)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyNoComplexProperty", property);
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00081E86 File Offset: 0x00080086
		public static string Exception_TmdlPropertyMismatchTarget(string target, string property, string path)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyMismatchTarget", target, property, path);
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00081E95 File Offset: 0x00080095
		public static string Exception_TmdlPropertyInvalidTarget(string target, string property)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyInvalidTarget", target, property);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00081EA3 File Offset: 0x000800A3
		public static string Exception_TmdlPropertyUnknownTarget(string property)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyUnknownTarget", property);
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00081EB0 File Offset: 0x000800B0
		public static string Exception_TmdlPropertyMismatchCollectionElements(string property)
		{
			return TomSR.Keys.GetString("Exception_TmdlPropertyMismatchCollectionElements", property);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00081EBD File Offset: 0x000800BD
		public static string Exception_TmdlObjectInvalidChild(string child, string parent)
		{
			return TomSR.Keys.GetString("Exception_TmdlObjectInvalidChild", child, parent);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00081ECB File Offset: 0x000800CB
		public static string Exception_TmdlObjectNoNameForChild(string child, string parent)
		{
			return TomSR.Keys.GetString("Exception_TmdlObjectNoNameForChild", child, parent);
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00081ED9 File Offset: 0x000800D9
		public static string Exception_TmdlObjectInvalidNameForChild(string child, string parent, string name)
		{
			return TomSR.Keys.GetString("Exception_TmdlObjectInvalidNameForChild", child, parent, name);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00081EE8 File Offset: 0x000800E8
		public static string Exception_TmdlObjectInvalidDefaultProperty(string objectType)
		{
			return TomSR.Keys.GetString("Exception_TmdlObjectInvalidDefaultProperty", objectType);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00081EF5 File Offset: 0x000800F5
		public static string Exception_TmdlObjectInvalidCustomJsonProperty(string objectType, string property)
		{
			return TomSR.Keys.GetString("Exception_TmdlObjectInvalidCustomJsonProperty", objectType, property);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00081F03 File Offset: 0x00080103
		public static string Exception_InvalidLogicalPath(string path)
		{
			return TomSR.Keys.GetString("Exception_InvalidLogicalPath", path);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00081F10 File Offset: 0x00080110
		public static string Exception_MissingDocInContext(string logicalPath)
		{
			return TomSR.Keys.GetString("Exception_MissingDocInContext", logicalPath);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00081F1D File Offset: 0x0008011D
		public static string Exception_DuplicateDocInContext(string logicalPath)
		{
			return TomSR.Keys.GetString("Exception_DuplicateDocInContext", logicalPath);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00081F2A File Offset: 0x0008012A
		public static string Exception_FailureInModelUpdateFromContext(string type, string obj)
		{
			return TomSR.Keys.GetString("Exception_FailureInModelUpdateFromContext", type, obj);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x00081F38 File Offset: 0x00080138
		public static string Exception_FailureInModelUpdateFromContext2(string parentType, string parent, string type, string obj)
		{
			return TomSR.Keys.GetString("Exception_FailureInModelUpdateFromContext2", parentType, parent, type, obj);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00081F48 File Offset: 0x00080148
		public static string Exception_FailureInModelUpdateFromContext3(string parentType, string parent, string type, string obj)
		{
			return TomSR.Keys.GetString("Exception_FailureInModelUpdateFromContext3", parentType, parent, type, obj);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00081F58 File Offset: 0x00080158
		public static string Exception_InvalidContentStyle(string style)
		{
			return TomSR.Keys.GetString("Exception_InvalidContentStyle", style);
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x00081F65 File Offset: 0x00080165
		public static string Exception_InvalidFormatBuilderContentStyleForSetOption(string style)
		{
			return TomSR.Keys.GetString("Exception_InvalidFormatBuilderContentStyleForSetOption", style);
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x00081F72 File Offset: 0x00080172
		public static string Exception_InvalidSerializationBuilderContentStyleForSetOption(string style)
		{
			return TomSR.Keys.GetString("Exception_InvalidSerializationBuilderContentStyleForSetOption", style);
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x00081F7F File Offset: 0x0008017F
		public static string Exception_InvalidCompatRequestForTmdlSerialization(string mode, string level)
		{
			return TomSR.Keys.GetString("Exception_InvalidCompatRequestForTmdlSerialization", mode, level);
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x00081F8D File Offset: 0x0008018D
		public static string Exception_InvalidCompatRequestForTmdlSerialization2(string level)
		{
			return TomSR.Keys.GetString("Exception_InvalidCompatRequestForTmdlSerialization2", level);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00081F9A File Offset: 0x0008019A
		public static string Exception_MismatchTypeOnTmdlSerialization(string expectedType, string actualType)
		{
			return TomSR.Keys.GetString("Exception_MismatchTypeOnTmdlSerialization", expectedType, actualType);
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00081FA8 File Offset: 0x000801A8
		public static string Exception_ComverterMismatchType(string type, string tmdlType)
		{
			return TomSR.Keys.GetString("Exception_ComverterMismatchType", type, tmdlType);
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00081FB6 File Offset: 0x000801B6
		public static string Exception_ComverterMismatchInstanceType(string type, string tmdlType)
		{
			return TomSR.Keys.GetString("Exception_ComverterMismatchInstanceType", type, tmdlType);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00081FC4 File Offset: 0x000801C4
		public static string Exception_ComverterMismatchTypeInstance(string type, string tmdlType)
		{
			return TomSR.Keys.GetString("Exception_ComverterMismatchTypeInstance", type, tmdlType);
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00081FD2 File Offset: 0x000801D2
		public static string Exception_ComverterMismatchChildTypeParent(string type, string tmdlType)
		{
			return TomSR.Keys.GetString("Exception_ComverterMismatchChildTypeParent", type, tmdlType);
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00081FE0 File Offset: 0x000801E0
		public static string Exception_ComverterMismatchPropertyName(string name, string propertyName)
		{
			return TomSR.Keys.GetString("Exception_ComverterMismatchPropertyName", name, propertyName);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00081FEE File Offset: 0x000801EE
		public static string Exception_MetadataConfigUnsupportedType(string type)
		{
			return TomSR.Keys.GetString("Exception_MetadataConfigUnsupportedType", type);
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00081FFB File Offset: 0x000801FB
		public static string Exception_ObjetNameParsingError(string name, string error)
		{
			return TomSR.Keys.GetString("Exception_ObjetNameParsingError", name, error);
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0008200C File Offset: 0x0008020C
		public static string Exception_TmdlAmbiguousSource(string message, string eol, string type1, string name1, string path1, string type2, string name2, string path2)
		{
			return TomSR.Keys.GetString("Exception_TmdlAmbiguousSource", message, eol, type1, name1, path1, type2, name2, path2);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00082030 File Offset: 0x00080230
		public static string Exception_TmdlAmbiguousSource2(string message, string eol, string type1, string name1, string path1, string type2, string name2, string path2)
		{
			return TomSR.Keys.GetString("Exception_TmdlAmbiguousSource2", message, eol, type1, name1, path1, type2, name2, path2);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00082053 File Offset: 0x00080253
		public static string Exception_TmdlAmbiguousSource3(string message, string eol, string type1, string name1, string path1, string property, string path2)
		{
			return TomSR.Keys.GetString("Exception_TmdlAmbiguousSource3", message, eol, type1, name1, path1, property, path2);
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00082069 File Offset: 0x00080269
		public static string Exception_TmdlFormatException_Title(string errorCode, string eol)
		{
			return TomSR.Keys.GetString("Exception_TmdlFormatException_Title", errorCode, eol);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00082077 File Offset: 0x00080277
		public static string Exception_TmdlFormatException_DetailedError(string error, string eol)
		{
			return TomSR.Keys.GetString("Exception_TmdlFormatException_DetailedError", error, eol);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00082085 File Offset: 0x00080285
		public static string Exception_TmdlFormatException_SourceInfo(string path, string line, string eol)
		{
			return TomSR.Keys.GetString("Exception_TmdlFormatException_SourceInfo", path, line, eol);
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00082094 File Offset: 0x00080294
		public static string Exception_TmdlFormatException_Line(string line, string eol)
		{
			return TomSR.Keys.GetString("Exception_TmdlFormatException_Line", line, eol);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x000820A2 File Offset: 0x000802A2
		public static string Exception_TmdlParserInvalidScalarType(string type)
		{
			return TomSR.Keys.GetString("Exception_TmdlParserInvalidScalarType", type);
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x000820AF File Offset: 0x000802AF
		public static string Exception_TmdlParserInlineValueForStruct(string line)
		{
			return TomSR.Keys.GetString("Exception_TmdlParserInlineValueForStruct", line);
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x000820BC File Offset: 0x000802BC
		public static string Exception_TmdlParserStringPropertyWithNoValue(string property)
		{
			return TomSR.Keys.GetString("Exception_TmdlParserStringPropertyWithNoValue", property);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x000820C9 File Offset: 0x000802C9
		public static string Exception_TmdlParserPropertyWithUnsupportedType(string property, string type)
		{
			return TomSR.Keys.GetString("Exception_TmdlParserPropertyWithUnsupportedType", property, type);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x000820D7 File Offset: 0x000802D7
		public static string Exception_TmdlParserConvertValue(string value, string type)
		{
			return TomSR.Keys.GetString("Exception_TmdlParserConvertValue", value, type);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x000820E5 File Offset: 0x000802E5
		public static string Exception_TmdlFormatUnexpectedLineType(string type)
		{
			return TomSR.Keys.GetString("Exception_TmdlFormatUnexpectedLineType", type);
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x000820F2 File Offset: 0x000802F2
		public static string Exception_TmdlFormatUnexpectedLineType2(string type, string error)
		{
			return TomSR.Keys.GetString("Exception_TmdlFormatUnexpectedLineType2", type, error);
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00082100 File Offset: 0x00080300
		public static string Exception_TmdlFormatPropertyMismatch(string expected, string actual)
		{
			return TomSR.Keys.GetString("Exception_TmdlFormatPropertyMismatch", expected, actual);
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0008210E File Offset: 0x0008030E
		public static string Exception_TmdlFormatPropertyUnsupported(string property)
		{
			return TomSR.Keys.GetString("Exception_TmdlFormatPropertyUnsupported", property);
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0008211B File Offset: 0x0008031B
		public static string Exception_TmdlFormatChildUnsupported(string child)
		{
			return TomSR.Keys.GetString("Exception_TmdlFormatChildUnsupported", child);
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00082128 File Offset: 0x00080328
		public static string Exception_TmdlFormatObjectMismatch(string expected, string actual)
		{
			return TomSR.Keys.GetString("Exception_TmdlFormatObjectMismatch", expected, actual);
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x00082136 File Offset: 0x00080336
		public static string Exception_TmdlFormatObjectUnsupported(string obj)
		{
			return TomSR.Keys.GetString("Exception_TmdlFormatObjectUnsupported", obj);
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x00082143 File Offset: 0x00080343
		public static string Exception_TmdlFormatObjectNameParseError(string name, string error)
		{
			return TomSR.Keys.GetString("Exception_TmdlFormatObjectNameParseError", name, error);
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x00082151 File Offset: 0x00080351
		public static string Exception_TmdlFormatUnknownKeyword(string keyword)
		{
			return TomSR.Keys.GetString("Exception_TmdlFormatUnknownKeyword", keyword);
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0008215E File Offset: 0x0008035E
		public static string Exception_TmdlFormatObjectDefaultProperty(string type)
		{
			return TomSR.Keys.GetString("Exception_TmdlFormatObjectDefaultProperty", type);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0008216B File Offset: 0x0008036B
		public static string Exception_TmdlSerializerInvalidPath(string path)
		{
			return TomSR.Keys.GetString("Exception_TmdlSerializerInvalidPath", path);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00082178 File Offset: 0x00080378
		public static string Exception_TmdlInvalidValueCast(string type, string target)
		{
			return TomSR.Keys.GetString("Exception_TmdlInvalidValueCast", type, target);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00082186 File Offset: 0x00080386
		public static string ObjetNameParseError_OpenPart(string part)
		{
			return TomSR.Keys.GetString("ObjetNameParseError_OpenPart", part);
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00082193 File Offset: 0x00080393
		public static string ObjetNameParseError_NeedQuote(string @char, string index)
		{
			return TomSR.Keys.GetString("ObjetNameParseError_NeedQuote", @char, index);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x000821A1 File Offset: 0x000803A1
		public static string TmdlAmbiguousSourceError_DuplicateProperty(string property)
		{
			return TomSR.Keys.GetString("TmdlAmbiguousSourceError_DuplicateProperty", property);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x000821AE File Offset: 0x000803AE
		public static string TmdlFormatError_TypeNotIndicateName(string objectType)
		{
			return TomSR.Keys.GetString("TmdlFormatError_TypeNotIndicateName", objectType);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x000821BB File Offset: 0x000803BB
		public static string TmdlFormatError_TypeIndicateName(string objectType)
		{
			return TomSR.Keys.GetString("TmdlFormatError_TypeIndicateName", objectType);
		}

		// Token: 0x02000313 RID: 787
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06002495 RID: 9365 RVA: 0x000E4927 File Offset: 0x000E2B27
			private Keys()
			{
			}

			// Token: 0x17000782 RID: 1922
			// (get) Token: 0x06002496 RID: 9366 RVA: 0x000E492F File Offset: 0x000E2B2F
			// (set) Token: 0x06002497 RID: 9367 RVA: 0x000E4936 File Offset: 0x000E2B36
			public static CultureInfo Culture
			{
				get
				{
					return TomSR.Keys._culture;
				}
				set
				{
					TomSR.Keys._culture = value;
				}
			}

			// Token: 0x06002498 RID: 9368 RVA: 0x000E493E File Offset: 0x000E2B3E
			public static string GetString(string key)
			{
				return TomSR.Keys.resourceManager.GetString(key, TomSR.Keys._culture);
			}

			// Token: 0x06002499 RID: 9369 RVA: 0x000E4950 File Offset: 0x000E2B50
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, TomSR.Keys.resourceManager.GetString(key, TomSR.Keys._culture), arg0);
			}

			// Token: 0x0600249A RID: 9370 RVA: 0x000E496D File Offset: 0x000E2B6D
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, TomSR.Keys.resourceManager.GetString(key, TomSR.Keys._culture), arg0, arg1);
			}

			// Token: 0x0600249B RID: 9371 RVA: 0x000E498B File Offset: 0x000E2B8B
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, TomSR.Keys.resourceManager.GetString(key, TomSR.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x0600249C RID: 9372 RVA: 0x000E49AA File Offset: 0x000E2BAA
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3)
			{
				return string.Format(CultureInfo.CurrentCulture, TomSR.Keys.resourceManager.GetString(key, TomSR.Keys._culture), new object[] { arg0, arg1, arg2, arg3 });
			}

			// Token: 0x0600249D RID: 9373 RVA: 0x000E49E0 File Offset: 0x000E2BE0
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
			{
				return string.Format(CultureInfo.CurrentCulture, TomSR.Keys.resourceManager.GetString(key, TomSR.Keys._culture), new object[] { arg0, arg1, arg2, arg3, arg4, arg5, arg6 });
			}

			// Token: 0x0600249E RID: 9374 RVA: 0x000E4A30 File Offset: 0x000E2C30
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7)
			{
				return string.Format(CultureInfo.CurrentCulture, TomSR.Keys.resourceManager.GetString(key, TomSR.Keys._culture), new object[] { arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
			}

			// Token: 0x04000B50 RID: 2896
			private static ResourceManager resourceManager = new ResourceManager(typeof(TomSR).FullName, typeof(TomSR).Module.Assembly);

			// Token: 0x04000B51 RID: 2897
			private static CultureInfo _culture = null;

			// Token: 0x04000B52 RID: 2898
			public const string Exception_InternalErrorOccured = "Exception_InternalErrorOccured";

			// Token: 0x04000B53 RID: 2899
			public const string Exception_InternalErrorOccured_Detailed = "Exception_InternalErrorOccured_Detailed";

			// Token: 0x04000B54 RID: 2900
			public const string Exception_NoVersionColumnInRowset = "Exception_NoVersionColumnInRowset";

			// Token: 0x04000B55 RID: 2901
			public const string Exception_UnrecognizedValueOfType = "Exception_UnrecognizedValueOfType";

			// Token: 0x04000B56 RID: 2902
			public const string Exception_CompatLevelOutOfRange = "Exception_CompatLevelOutOfRange";

			// Token: 0x04000B57 RID: 2903
			public const string Exception_CompatRestriction_UnsupportedForMode = "Exception_CompatRestriction_UnsupportedForMode";

			// Token: 0x04000B58 RID: 2904
			public const string Exception_CompatRestriction_ViolationForMode = "Exception_CompatRestriction_ViolationForMode";

			// Token: 0x04000B59 RID: 2905
			public const string Exception_CompatRestriction_InvalidForAllModes = "Exception_CompatRestriction_InvalidForAllModes";

			// Token: 0x04000B5A RID: 2906
			public const string Exception_CanNotReadFindTypePropObject = "Exception_CanNotReadFindTypePropObject";

			// Token: 0x04000B5B RID: 2907
			public const string Exception_ObjectIsAlreadyMarkedAsReadOnly = "Exception_ObjectIsAlreadyMarkedAsReadOnly";

			// Token: 0x04000B5C RID: 2908
			public const string Exception_ObjectMarkedAsReadOnly = "Exception_ObjectMarkedAsReadOnly";

			// Token: 0x04000B5D RID: 2909
			public const string Exception_CannotFindItemWithId = "Exception_CannotFindItemWithId";

			// Token: 0x04000B5E RID: 2910
			public const string Exception_ObjectWithNameNotExistInCollection = "Exception_ObjectWithNameNotExistInCollection";

			// Token: 0x04000B5F RID: 2911
			public const string Exception_CollectionAlreadyContainsObjectWithName = "Exception_CollectionAlreadyContainsObjectWithName";

			// Token: 0x04000B60 RID: 2912
			public const string Exception_CollectionAlreadyContainsObjectWithTag = "Exception_CollectionAlreadyContainsObjectWithTag";

			// Token: 0x04000B61 RID: 2913
			public const string Exception_ItemAlreadyPresentInCollection = "Exception_ItemAlreadyPresentInCollection";

			// Token: 0x04000B62 RID: 2914
			public const string Exception_ObjectRemovedCannotBeModifiedAttachedToModel = "Exception_ObjectRemovedCannotBeModifiedAttachedToModel";

			// Token: 0x04000B63 RID: 2915
			public const string Exception_RenameAlreadyRequested = "Exception_RenameAlreadyRequested";

			// Token: 0x04000B64 RID: 2916
			public const string Exception_DisconnectedObjectCannotBeRefreshed = "Exception_DisconnectedObjectCannotBeRefreshed";

			// Token: 0x04000B65 RID: 2917
			public const string Exception_DisconnectedObjectCannotBeAnalyzeRefreshPolicyImpact = "Exception_DisconnectedObjectCannotBeAnalyzeRefreshPolicyImpact";

			// Token: 0x04000B66 RID: 2918
			public const string Exception_ModelCannotBeMotified_RenameRequested = "Exception_ModelCannotBeMotified_RenameRequested";

			// Token: 0x04000B67 RID: 2919
			public const string Exception_ModelCannotBeModifiedAnotherModelInActiveTransaction = "Exception_ModelCannotBeModifiedAnotherModelInActiveTransaction";

			// Token: 0x04000B68 RID: 2920
			public const string Exception_CannotRemoveItemByNameDuringCopy = "Exception_CannotRemoveItemByNameDuringCopy";

			// Token: 0x04000B69 RID: 2921
			public const string Exception_CannotConvert = "Exception_CannotConvert";

			// Token: 0x04000B6A RID: 2922
			public const string Exception_CommitTransactionFailed = "Exception_CommitTransactionFailed";

			// Token: 0x04000B6B RID: 2923
			public const string Exception_SaveModelChangesFailed = "Exception_SaveModelChangesFailed";

			// Token: 0x04000B6C RID: 2924
			public const string Exception_FailedToGenerateXmlaRequest = "Exception_FailedToGenerateXmlaRequest";

			// Token: 0x04000B6D RID: 2925
			public const string Exception_ExecuteXmlaFailed = "Exception_ExecuteXmlaFailed";

			// Token: 0x04000B6E RID: 2926
			public const string Exception_DiscoverModelFailed = "Exception_DiscoverModelFailed";

			// Token: 0x04000B6F RID: 2927
			public const string Exception_DiscoverModelFailedBadDb = "Exception_DiscoverModelFailedBadDb";

			// Token: 0x04000B70 RID: 2928
			public const string Exception_NoActiveTransactionInSession = "Exception_NoActiveTransactionInSession";

			// Token: 0x04000B71 RID: 2929
			public const string Exception_DatabaseModelHasLocalChanges = "Exception_DatabaseModelHasLocalChanges";

			// Token: 0x04000B72 RID: 2930
			public const string Exception_DatabaseDoesNotExist = "Exception_DatabaseDoesNotExist";

			// Token: 0x04000B73 RID: 2931
			public const string Exception_ValidationFlagsSimultaneousUse = "Exception_ValidationFlagsSimultaneousUse";

			// Token: 0x04000B74 RID: 2932
			public const string Exception_ValidationFlagsOutsideTransaction = "Exception_ValidationFlagsOutsideTransaction";

			// Token: 0x04000B75 RID: 2933
			public const string Exception_CannotStartTransactionLocalchanges = "Exception_CannotStartTransactionLocalchanges";

			// Token: 0x04000B76 RID: 2934
			public const string Exception_CannotExceuteJsonWithLocalchanges = "Exception_CannotExceuteJsonWithLocalchanges";

			// Token: 0x04000B77 RID: 2935
			public const string Exception_CannotSyncModelDisconnected = "Exception_CannotSyncModelDisconnected";

			// Token: 0x04000B78 RID: 2936
			public const string Exception_CannotSyncNewModel = "Exception_CannotSyncNewModel";

			// Token: 0x04000B79 RID: 2937
			public const string Exception_CannotSyncModelModified = "Exception_CannotSyncModelModified";

			// Token: 0x04000B7A RID: 2938
			public const string Exception_CannotSyncModelOfDirtyDatabase = "Exception_CannotSyncModelOfDirtyDatabase";

			// Token: 0x04000B7B RID: 2939
			public const string Exception_CannotSaveChangesDisconnectedModel = "Exception_CannotSaveChangesDisconnectedModel";

			// Token: 0x04000B7C RID: 2940
			public const string Exception_CannotSaveChangeAnotherModelInTransaction = "Exception_CannotSaveChangeAnotherModelInTransaction";

			// Token: 0x04000B7D RID: 2941
			public const string Exception_CannotUndoChangesDisconnectedModel = "Exception_CannotUndoChangesDisconnectedModel";

			// Token: 0x04000B7E RID: 2942
			public const string Exception_CannotExecuteXmlaDisconnectedModel = "Exception_CannotExecuteXmlaDisconnectedModel";

			// Token: 0x04000B7F RID: 2943
			public const string Exception_CannotExecuteXmlaAnotherModelInTransaction = "Exception_CannotExecuteXmlaAnotherModelInTransaction";

			// Token: 0x04000B80 RID: 2944
			public const string Exception_PartitionSourceAlreadyAttached = "Exception_PartitionSourceAlreadyAttached";

			// Token: 0x04000B81 RID: 2945
			public const string Exception_PartitionSourceAlreadyRemoved = "Exception_PartitionSourceAlreadyRemoved";

			// Token: 0x04000B82 RID: 2946
			public const string Exception_UnknownLinkType = "Exception_UnknownLinkType";

			// Token: 0x04000B83 RID: 2947
			public const string Exception_CannotUpdateCultureCollation = "Exception_CannotUpdateCultureCollation";

			// Token: 0x04000B84 RID: 2948
			public const string Exception_CannotUpdateCultureInfoOfChildCollection = "Exception_CannotUpdateCultureInfoOfChildCollection";

			// Token: 0x04000B85 RID: 2949
			public const string Exception_CannotCopyCollectionCultureConflict = "Exception_CannotCopyCollectionCultureConflict";

			// Token: 0x04000B86 RID: 2950
			public const string Exception_ObjectDoesNotHaveName = "Exception_ObjectDoesNotHaveName";

			// Token: 0x04000B87 RID: 2951
			public const string Exception_CustomPropertyAssignedToMultipleObjects = "Exception_CustomPropertyAssignedToMultipleObjects";

			// Token: 0x04000B88 RID: 2952
			public const string Exception_CannotRetrieveAdaptiveCachingRecommendations = "Exception_CannotRetrieveAdaptiveCachingRecommendations";

			// Token: 0x04000B89 RID: 2953
			public const string Exception_UboNotAvailable = "Exception_UboNotAvailable";

			// Token: 0x04000B8A RID: 2954
			public const string Exception_InvalidArrayIndex = "Exception_InvalidArrayIndex";

			// Token: 0x04000B8B RID: 2955
			public const string Exception_CannotModifyReadOnlyFormatOptions = "Exception_CannotModifyReadOnlyFormatOptions";

			// Token: 0x04000B8C RID: 2956
			public const string Exception_InvalidIndentationLength = "Exception_InvalidIndentationLength";

			// Token: 0x04000B8D RID: 2957
			public const string Exception_InvalidBaseIndentationLevel = "Exception_InvalidBaseIndentationLevel";

			// Token: 0x04000B8E RID: 2958
			public const string DefaultNameForObject_Model = "DefaultNameForObject_Model";

			// Token: 0x04000B8F RID: 2959
			public const string DefaultNameForObject_DataSource = "DefaultNameForObject_DataSource";

			// Token: 0x04000B90 RID: 2960
			public const string DefaultNameForObject_Table = "DefaultNameForObject_Table";

			// Token: 0x04000B91 RID: 2961
			public const string DefaultNameForObject_Column = "DefaultNameForObject_Column";

			// Token: 0x04000B92 RID: 2962
			public const string DefaultNameForObject_AttributeHierarchy = "DefaultNameForObject_AttributeHierarchy";

			// Token: 0x04000B93 RID: 2963
			public const string DefaultNameForObject_Partition = "DefaultNameForObject_Partition";

			// Token: 0x04000B94 RID: 2964
			public const string DefaultNameForObject_Relationship = "DefaultNameForObject_Relationship";

			// Token: 0x04000B95 RID: 2965
			public const string DefaultNameForObject_Measure = "DefaultNameForObject_Measure";

			// Token: 0x04000B96 RID: 2966
			public const string DefaultNameForObject_Hierarchy = "DefaultNameForObject_Hierarchy";

			// Token: 0x04000B97 RID: 2967
			public const string DefaultNameForObject_Level = "DefaultNameForObject_Level";

			// Token: 0x04000B98 RID: 2968
			public const string DefaultNameForObject_Annotation = "DefaultNameForObject_Annotation";

			// Token: 0x04000B99 RID: 2969
			public const string DefaultNameForObject_KPI = "DefaultNameForObject_KPI";

			// Token: 0x04000B9A RID: 2970
			public const string DefaultNameForObject_Culture = "DefaultNameForObject_Culture";

			// Token: 0x04000B9B RID: 2971
			public const string DefaultNameForObject_LinguisticMetadata = "DefaultNameForObject_LinguisticMetadata";

			// Token: 0x04000B9C RID: 2972
			public const string DefaultNameForObject_Perspective = "DefaultNameForObject_Perspective";

			// Token: 0x04000B9D RID: 2973
			public const string DefaultNameForObject_PerspectiveTable = "DefaultNameForObject_PerspectiveTable";

			// Token: 0x04000B9E RID: 2974
			public const string DefaultNameForObject_PerspectiveColumn = "DefaultNameForObject_PerspectiveColumn";

			// Token: 0x04000B9F RID: 2975
			public const string DefaultNameForObject_PerspectiveHierarchy = "DefaultNameForObject_PerspectiveHierarchy";

			// Token: 0x04000BA0 RID: 2976
			public const string DefaultNameForObject_PerspectiveMeasure = "DefaultNameForObject_PerspectiveMeasure";

			// Token: 0x04000BA1 RID: 2977
			public const string DefaultNameForObject_ModelRole = "DefaultNameForObject_ModelRole";

			// Token: 0x04000BA2 RID: 2978
			public const string DefaultNameForObject_ModelRoleMember = "DefaultNameForObject_ModelRoleMember";

			// Token: 0x04000BA3 RID: 2979
			public const string DefaultNameForObject_TablePermission = "DefaultNameForObject_TablePermission";

			// Token: 0x04000BA4 RID: 2980
			public const string DefaultNameForObject_Variation = "DefaultNameForObject_Variation";

			// Token: 0x04000BA5 RID: 2981
			public const string DefaultNameForObject_Set = "DefaultNameForObject_Set";

			// Token: 0x04000BA6 RID: 2982
			public const string DefaultNameForObject_PerspectiveSet = "DefaultNameForObject_PerspectiveSet";

			// Token: 0x04000BA7 RID: 2983
			public const string DefaultNameForObject_ExtendedProperty = "DefaultNameForObject_ExtendedProperty";

			// Token: 0x04000BA8 RID: 2984
			public const string DefaultNameForObject_NamedExpression = "DefaultNameForObject_NamedExpression";

			// Token: 0x04000BA9 RID: 2985
			public const string DefaultNameForObject_ColumnPermission = "DefaultNameForObject_ColumnPermission";

			// Token: 0x04000BAA RID: 2986
			public const string DefaultNameForObject_DetailRowsDefinition = "DefaultNameForObject_DetailRowsDefinition";

			// Token: 0x04000BAB RID: 2987
			public const string DefaultNameForObject_RelatedColumnDetails = "DefaultNameForObject_RelatedColumnDetails";

			// Token: 0x04000BAC RID: 2988
			public const string DefaultNameForObject_GroupByColumn = "DefaultNameForObject_GroupByColumn";

			// Token: 0x04000BAD RID: 2989
			public const string DefaultNameForObject_CalculationGroup = "DefaultNameForObject_CalculationGroup";

			// Token: 0x04000BAE RID: 2990
			public const string DefaultNameForObject_CalculationItem = "DefaultNameForObject_CalculationItem";

			// Token: 0x04000BAF RID: 2991
			public const string DefaultNameForObject_QueryGroup = "DefaultNameForObject_QueryGroup";

			// Token: 0x04000BB0 RID: 2992
			public const string DefaultNameForObject_AnalyticsAIMetadata = "DefaultNameForObject_AnalyticsAIMetadata";

			// Token: 0x04000BB1 RID: 2993
			public const string DefaultNameForObject_ChangedProperty = "DefaultNameForObject_ChangedProperty";

			// Token: 0x04000BB2 RID: 2994
			public const string DefaultNameForObject_ExcludedArtifact = "DefaultNameForObject_ExcludedArtifact";

			// Token: 0x04000BB3 RID: 2995
			public const string DefaultNameForObject_DataCoverageDefinition = "DefaultNameForObject_DataCoverageDefinition";

			// Token: 0x04000BB4 RID: 2996
			public const string DefaultNameForObject_CalculationGroupExpression = "DefaultNameForObject_CalculationGroupExpression";

			// Token: 0x04000BB5 RID: 2997
			public const string DefaultNameForObject_Calendar = "DefaultNameForObject_Calendar";

			// Token: 0x04000BB6 RID: 2998
			public const string DefaultNameForObject_TimeUnitColumnAssociation = "DefaultNameForObject_TimeUnitColumnAssociation";

			// Token: 0x04000BB7 RID: 2999
			public const string DefaultNameForObject_CalendarColumnReference = "DefaultNameForObject_CalendarColumnReference";

			// Token: 0x04000BB8 RID: 3000
			public const string DefaultNameForObject_Function = "DefaultNameForObject_Function";

			// Token: 0x04000BB9 RID: 3001
			public const string DefaultNameForObject_BindingInfo = "DefaultNameForObject_BindingInfo";

			// Token: 0x04000BBA RID: 3002
			public const string ObjectType_Model = "ObjectType_Model";

			// Token: 0x04000BBB RID: 3003
			public const string ObjectType_DataSource = "ObjectType_DataSource";

			// Token: 0x04000BBC RID: 3004
			public const string ObjectType_Table = "ObjectType_Table";

			// Token: 0x04000BBD RID: 3005
			public const string ObjectType_Column = "ObjectType_Column";

			// Token: 0x04000BBE RID: 3006
			public const string ObjectType_AttributeHierarchy = "ObjectType_AttributeHierarchy";

			// Token: 0x04000BBF RID: 3007
			public const string ObjectType_Partition = "ObjectType_Partition";

			// Token: 0x04000BC0 RID: 3008
			public const string ObjectType_Relationship = "ObjectType_Relationship";

			// Token: 0x04000BC1 RID: 3009
			public const string ObjectType_Measure = "ObjectType_Measure";

			// Token: 0x04000BC2 RID: 3010
			public const string ObjectType_Hierarchy = "ObjectType_Hierarchy";

			// Token: 0x04000BC3 RID: 3011
			public const string ObjectType_Level = "ObjectType_Level";

			// Token: 0x04000BC4 RID: 3012
			public const string ObjectType_Annotation = "ObjectType_Annotation";

			// Token: 0x04000BC5 RID: 3013
			public const string ObjectType_KPI = "ObjectType_KPI";

			// Token: 0x04000BC6 RID: 3014
			public const string ObjectType_Culture = "ObjectType_Culture";

			// Token: 0x04000BC7 RID: 3015
			public const string ObjectType_LinguisticMetadata = "ObjectType_LinguisticMetadata";

			// Token: 0x04000BC8 RID: 3016
			public const string ObjectType_ObjectTranslation = "ObjectType_ObjectTranslation";

			// Token: 0x04000BC9 RID: 3017
			public const string ObjectType_Perspective = "ObjectType_Perspective";

			// Token: 0x04000BCA RID: 3018
			public const string ObjectType_PerspectiveTable = "ObjectType_PerspectiveTable";

			// Token: 0x04000BCB RID: 3019
			public const string ObjectType_PerspectiveColumn = "ObjectType_PerspectiveColumn";

			// Token: 0x04000BCC RID: 3020
			public const string ObjectType_PerspectiveHierarchy = "ObjectType_PerspectiveHierarchy";

			// Token: 0x04000BCD RID: 3021
			public const string ObjectType_PerspectiveMeasure = "ObjectType_PerspectiveMeasure";

			// Token: 0x04000BCE RID: 3022
			public const string ObjectType_ModelRole = "ObjectType_ModelRole";

			// Token: 0x04000BCF RID: 3023
			public const string ObjectType_ModelRoleMember = "ObjectType_ModelRoleMember";

			// Token: 0x04000BD0 RID: 3024
			public const string ObjectType_TablePermission = "ObjectType_TablePermission";

			// Token: 0x04000BD1 RID: 3025
			public const string ObjectType_Variation = "ObjectType_Variation";

			// Token: 0x04000BD2 RID: 3026
			public const string ObjectType_Set = "ObjectType_Set";

			// Token: 0x04000BD3 RID: 3027
			public const string ObjectType_PerspectiveSet = "ObjectType_PerspectiveSet";

			// Token: 0x04000BD4 RID: 3028
			public const string ObjectType_ExtendedProperty = "ObjectType_ExtendedProperty";

			// Token: 0x04000BD5 RID: 3029
			public const string ObjectType_NamedExpression = "ObjectType_NamedExpression";

			// Token: 0x04000BD6 RID: 3030
			public const string ObjectType_ColumnPermission = "ObjectType_ColumnPermission";

			// Token: 0x04000BD7 RID: 3031
			public const string ObjectType_DetailRowsDefinition = "ObjectType_DetailRowsDefinition";

			// Token: 0x04000BD8 RID: 3032
			public const string ObjectType_RelatedColumnDetails = "ObjectType_RelatedColumnDetails";

			// Token: 0x04000BD9 RID: 3033
			public const string ObjectType_GroupByColumn = "ObjectType_GroupByColumn";

			// Token: 0x04000BDA RID: 3034
			public const string ObjectType_CalculationGroup = "ObjectType_CalculationGroup";

			// Token: 0x04000BDB RID: 3035
			public const string ObjectType_CalculationItem = "ObjectType_CalculationItem";

			// Token: 0x04000BDC RID: 3036
			public const string ObjectType_AlternateOf = "ObjectType_AlternateOf";

			// Token: 0x04000BDD RID: 3037
			public const string ObjectType_RefreshPolicy = "ObjectType_RefreshPolicy";

			// Token: 0x04000BDE RID: 3038
			public const string ObjectType_FormatStringDefinition = "ObjectType_FormatStringDefinition";

			// Token: 0x04000BDF RID: 3039
			public const string ObjectType_QueryGroup = "ObjectType_QueryGroup";

			// Token: 0x04000BE0 RID: 3040
			public const string ObjectType_AnalyticsAIMetadata = "ObjectType_AnalyticsAIMetadata";

			// Token: 0x04000BE1 RID: 3041
			public const string ObjectType_ChangedProperty = "ObjectType_ChangedProperty";

			// Token: 0x04000BE2 RID: 3042
			public const string ObjectType_ExcludedArtifact = "ObjectType_ExcludedArtifact";

			// Token: 0x04000BE3 RID: 3043
			public const string ObjectType_DataCoverageDefinition = "ObjectType_DataCoverageDefinition";

			// Token: 0x04000BE4 RID: 3044
			public const string ObjectType_CalculationGroupExpression = "ObjectType_CalculationGroupExpression";

			// Token: 0x04000BE5 RID: 3045
			public const string ObjectType_Calendar = "ObjectType_Calendar";

			// Token: 0x04000BE6 RID: 3046
			public const string ObjectType_TimeUnitColumnAssociation = "ObjectType_TimeUnitColumnAssociation";

			// Token: 0x04000BE7 RID: 3047
			public const string ObjectType_CalendarColumnReference = "ObjectType_CalendarColumnReference";

			// Token: 0x04000BE8 RID: 3048
			public const string ObjectType_Function = "ObjectType_Function";

			// Token: 0x04000BE9 RID: 3049
			public const string ObjectType_BindingInfo = "ObjectType_BindingInfo";

			// Token: 0x04000BEA RID: 3050
			public const string ObjectType_Database = "ObjectType_Database";

			// Token: 0x04000BEB RID: 3051
			public const string ObjectPath_Model_1Arg = "ObjectPath_Model_1Arg";

			// Token: 0x04000BEC RID: 3052
			public const string ObjectPath_DataSource_1Arg = "ObjectPath_DataSource_1Arg";

			// Token: 0x04000BED RID: 3053
			public const string ObjectPath_Function_1Arg = "ObjectPath_Function_1Arg";

			// Token: 0x04000BEE RID: 3054
			public const string ObjectPath_Table_1Arg = "ObjectPath_Table_1Arg";

			// Token: 0x04000BEF RID: 3055
			public const string ObjectPath_Perspective_1Arg = "ObjectPath_Perspective_1Arg";

			// Token: 0x04000BF0 RID: 3056
			public const string ObjectPath_Column_1Arg = "ObjectPath_Column_1Arg";

			// Token: 0x04000BF1 RID: 3057
			public const string ObjectPath_Column_2Args = "ObjectPath_Column_2Args";

			// Token: 0x04000BF2 RID: 3058
			public const string ObjectPath_AttributeHierarchy_0Args = "ObjectPath_AttributeHierarchy_0Args";

			// Token: 0x04000BF3 RID: 3059
			public const string ObjectPath_AttributeHierarchy_1Arg = "ObjectPath_AttributeHierarchy_1Arg";

			// Token: 0x04000BF4 RID: 3060
			public const string ObjectPath_AttributeHierarchy_2Args = "ObjectPath_AttributeHierarchy_2Args";

			// Token: 0x04000BF5 RID: 3061
			public const string ObjectPath_Partition_1Arg = "ObjectPath_Partition_1Arg";

			// Token: 0x04000BF6 RID: 3062
			public const string ObjectPath_Partition_2Args = "ObjectPath_Partition_2Args";

			// Token: 0x04000BF7 RID: 3063
			public const string ObjectPath_Relationship_1Arg = "ObjectPath_Relationship_1Arg";

			// Token: 0x04000BF8 RID: 3064
			public const string ObjectPath_Measure_1Arg = "ObjectPath_Measure_1Arg";

			// Token: 0x04000BF9 RID: 3065
			public const string ObjectPath_Measure_2Args = "ObjectPath_Measure_2Args";

			// Token: 0x04000BFA RID: 3066
			public const string ObjectPath_Hierarchy_1Arg = "ObjectPath_Hierarchy_1Arg";

			// Token: 0x04000BFB RID: 3067
			public const string ObjectPath_Hierarchy_2Args = "ObjectPath_Hierarchy_2Args";

			// Token: 0x04000BFC RID: 3068
			public const string ObjectPath_Level_1Arg = "ObjectPath_Level_1Arg";

			// Token: 0x04000BFD RID: 3069
			public const string ObjectPath_Level_2Args = "ObjectPath_Level_2Args";

			// Token: 0x04000BFE RID: 3070
			public const string ObjectPath_Level_3Args = "ObjectPath_Level_3Args";

			// Token: 0x04000BFF RID: 3071
			public const string ObjectPath_Annotation_1Arg = "ObjectPath_Annotation_1Arg";

			// Token: 0x04000C00 RID: 3072
			public const string ObjectPath_KPI_0Args = "ObjectPath_KPI_0Args";

			// Token: 0x04000C01 RID: 3073
			public const string ObjectPath_KPI_1Arg = "ObjectPath_KPI_1Arg";

			// Token: 0x04000C02 RID: 3074
			public const string ObjectPath_KPI_2Args = "ObjectPath_KPI_2Args";

			// Token: 0x04000C03 RID: 3075
			public const string ObjectPath_Culture_1Arg = "ObjectPath_Culture_1Arg";

			// Token: 0x04000C04 RID: 3076
			public const string ObjectPath_LinguisticMetadata_0Args = "ObjectPath_LinguisticMetadata_0Args";

			// Token: 0x04000C05 RID: 3077
			public const string ObjectPath_LinguisticMetadata_1Arg = "ObjectPath_LinguisticMetadata_1Arg";

			// Token: 0x04000C06 RID: 3078
			public const string ObjectPath_ObjectTranslation_0Args = "ObjectPath_ObjectTranslation_0Args";

			// Token: 0x04000C07 RID: 3079
			public const string ObjectPath_ObjectTranslation_1Arg = "ObjectPath_ObjectTranslation_1Arg";

			// Token: 0x04000C08 RID: 3080
			public const string ObjectPath_PerspectiveTable_0Args = "ObjectPath_PerspectiveTable_0Args";

			// Token: 0x04000C09 RID: 3081
			public const string ObjectPath_PerspectiveTable_1Args = "ObjectPath_PerspectiveTable_1Args";

			// Token: 0x04000C0A RID: 3082
			public const string ObjectPath_PerspectiveTable_2Args = "ObjectPath_PerspectiveTable_2Args";

			// Token: 0x04000C0B RID: 3083
			public const string ObjectPath_PerspectiveColumn_0Args = "ObjectPath_PerspectiveColumn_0Args";

			// Token: 0x04000C0C RID: 3084
			public const string ObjectPath_PerspectiveColumn_1Args = "ObjectPath_PerspectiveColumn_1Args";

			// Token: 0x04000C0D RID: 3085
			public const string ObjectPath_PerspectiveColumn_2Args = "ObjectPath_PerspectiveColumn_2Args";

			// Token: 0x04000C0E RID: 3086
			public const string ObjectPath_PerspectiveColumn_3Args = "ObjectPath_PerspectiveColumn_3Args";

			// Token: 0x04000C0F RID: 3087
			public const string ObjectPath_PerspectiveHierarchy_0Args = "ObjectPath_PerspectiveHierarchy_0Args";

			// Token: 0x04000C10 RID: 3088
			public const string ObjectPath_PerspectiveHierarchy_1Args = "ObjectPath_PerspectiveHierarchy_1Args";

			// Token: 0x04000C11 RID: 3089
			public const string ObjectPath_PerspectiveHierarchy_2Args = "ObjectPath_PerspectiveHierarchy_2Args";

			// Token: 0x04000C12 RID: 3090
			public const string ObjectPath_PerspectiveHierarchy_3Args = "ObjectPath_PerspectiveHierarchy_3Args";

			// Token: 0x04000C13 RID: 3091
			public const string ObjectPath_PerspectiveMeasure_0Args = "ObjectPath_PerspectiveMeasure_0Args";

			// Token: 0x04000C14 RID: 3092
			public const string ObjectPath_PerspectiveMeasure_1Args = "ObjectPath_PerspectiveMeasure_1Args";

			// Token: 0x04000C15 RID: 3093
			public const string ObjectPath_PerspectiveMeasure_2Args = "ObjectPath_PerspectiveMeasure_2Args";

			// Token: 0x04000C16 RID: 3094
			public const string ObjectPath_PerspectiveMeasure_3Args = "ObjectPath_PerspectiveMeasure_3Args";

			// Token: 0x04000C17 RID: 3095
			public const string ObjectPath_Role_1Arg = "ObjectPath_Role_1Arg";

			// Token: 0x04000C18 RID: 3096
			public const string ObjectPath_RoleMembership_2Args = "ObjectPath_RoleMembership_2Args";

			// Token: 0x04000C19 RID: 3097
			public const string ObjectPath_RoleMembership_1Arg = "ObjectPath_RoleMembership_1Arg";

			// Token: 0x04000C1A RID: 3098
			public const string ObjectPath_TablePermission_2Args = "ObjectPath_TablePermission_2Args";

			// Token: 0x04000C1B RID: 3099
			public const string ObjectPath_TablePermission_1Arg = "ObjectPath_TablePermission_1Arg";

			// Token: 0x04000C1C RID: 3100
			public const string ObjectPath_Variation_1Arg = "ObjectPath_Variation_1Arg";

			// Token: 0x04000C1D RID: 3101
			public const string ObjectPath_Variation_2Args = "ObjectPath_Variation_2Args";

			// Token: 0x04000C1E RID: 3102
			public const string ObjectPath_Variation_3Args = "ObjectPath_Variation_3Args";

			// Token: 0x04000C1F RID: 3103
			public const string ObjectPath_Set_1Arg = "ObjectPath_Set_1Arg";

			// Token: 0x04000C20 RID: 3104
			public const string ObjectPath_Set_2Args = "ObjectPath_Set_2Args";

			// Token: 0x04000C21 RID: 3105
			public const string ObjectPath_PerspectiveSet_0Args = "ObjectPath_PerspectiveSet_0Args";

			// Token: 0x04000C22 RID: 3106
			public const string ObjectPath_PerspectiveSet_1Args = "ObjectPath_PerspectiveSet_1Args";

			// Token: 0x04000C23 RID: 3107
			public const string ObjectPath_PerspectiveSet_2Args = "ObjectPath_PerspectiveSet_2Args";

			// Token: 0x04000C24 RID: 3108
			public const string ObjectPath_PerspectiveSet_3Args = "ObjectPath_PerspectiveSet_3Args";

			// Token: 0x04000C25 RID: 3109
			public const string ObjectPath_ExtendedProperty_1Arg = "ObjectPath_ExtendedProperty_1Arg";

			// Token: 0x04000C26 RID: 3110
			public const string ObjectPath_Expression_1Arg = "ObjectPath_Expression_1Arg";

			// Token: 0x04000C27 RID: 3111
			public const string ObjectPath_ColumnPermission_2Args = "ObjectPath_ColumnPermission_2Args";

			// Token: 0x04000C28 RID: 3112
			public const string ObjectPath_ColumnPermission_1Arg = "ObjectPath_ColumnPermission_1Arg";

			// Token: 0x04000C29 RID: 3113
			public const string ObjectPath_DetailRowsDefinition_0Args = "ObjectPath_DetailRowsDefinition_0Args";

			// Token: 0x04000C2A RID: 3114
			public const string ObjectPath_DetailRowsDefinition_1Arg_Measure = "ObjectPath_DetailRowsDefinition_1Arg_Measure";

			// Token: 0x04000C2B RID: 3115
			public const string ObjectPath_DetailRowsDefinition_1Arg_Table = "ObjectPath_DetailRowsDefinition_1Arg_Table";

			// Token: 0x04000C2C RID: 3116
			public const string ObjectPath_DetailRowsDefinition_2Args = "ObjectPath_DetailRowsDefinition_2Args";

			// Token: 0x04000C2D RID: 3117
			public const string ObjectPath_RelatedColumnDetails_0Args = "ObjectPath_RelatedColumnDetails_0Args";

			// Token: 0x04000C2E RID: 3118
			public const string ObjectPath_RelatedColumnDetails_1Args = "ObjectPath_RelatedColumnDetails_1Args";

			// Token: 0x04000C2F RID: 3119
			public const string ObjectPath_RelatedColumnDetails_2Args = "ObjectPath_RelatedColumnDetails_2Args";

			// Token: 0x04000C30 RID: 3120
			public const string ObjectPath_GroupByColumn_0Args = "ObjectPath_GroupByColumn_0Args";

			// Token: 0x04000C31 RID: 3121
			public const string ObjectPath_GroupByColumn_1Args = "ObjectPath_GroupByColumn_1Args";

			// Token: 0x04000C32 RID: 3122
			public const string ObjectPath_GroupByColumn_2Args = "ObjectPath_GroupByColumn_2Args";

			// Token: 0x04000C33 RID: 3123
			public const string ObjectPath_AlternateOf_0Args = "ObjectPath_AlternateOf_0Args";

			// Token: 0x04000C34 RID: 3124
			public const string ObjectPath_AlternateOf_1Arg_Table = "ObjectPath_AlternateOf_1Arg_Table";

			// Token: 0x04000C35 RID: 3125
			public const string ObjectPath_AlternateOf_2Args_Column = "ObjectPath_AlternateOf_2Args_Column";

			// Token: 0x04000C36 RID: 3126
			public const string ObjectPath_CalculationGroup_0Args = "ObjectPath_CalculationGroup_0Args";

			// Token: 0x04000C37 RID: 3127
			public const string ObjectPath_CalculationGroup_1Args = "ObjectPath_CalculationGroup_1Args";

			// Token: 0x04000C38 RID: 3128
			public const string ObjectPath_CalculationItem_0Args = "ObjectPath_CalculationItem_0Args";

			// Token: 0x04000C39 RID: 3129
			public const string ObjectPath_CalculationItem_1Args = "ObjectPath_CalculationItem_1Args";

			// Token: 0x04000C3A RID: 3130
			public const string ObjectPath_RefreshPolicy_0Args = "ObjectPath_RefreshPolicy_0Args";

			// Token: 0x04000C3B RID: 3131
			public const string ObjectPath_RefreshPolicy_1Args_Table = "ObjectPath_RefreshPolicy_1Args_Table";

			// Token: 0x04000C3C RID: 3132
			public const string ObjectPath_FormatStringDefinition_0Args = "ObjectPath_FormatStringDefinition_0Args";

			// Token: 0x04000C3D RID: 3133
			public const string ObjectPath_FormatStringDefinition_1Args_CalculationItem = "ObjectPath_FormatStringDefinition_1Args_CalculationItem";

			// Token: 0x04000C3E RID: 3134
			public const string ObjectPath_FormatStringDefinition_1Args_Measure = "ObjectPath_FormatStringDefinition_1Args_Measure";

			// Token: 0x04000C3F RID: 3135
			public const string ObjectPath_FormatStringDefinition_1Args_CalculationGroupExpression = "ObjectPath_FormatStringDefinition_1Args_CalculationGroupExpression";

			// Token: 0x04000C40 RID: 3136
			public const string ObjectPath_QueryGroup_0Args = "ObjectPath_QueryGroup_0Args";

			// Token: 0x04000C41 RID: 3137
			public const string ObjectPath_QueryGroup_1Args_Folder = "ObjectPath_QueryGroup_1Args_Folder";

			// Token: 0x04000C42 RID: 3138
			public const string ObjectPath_AnalyticsAIMetadata_0Args = "ObjectPath_AnalyticsAIMetadata_0Args";

			// Token: 0x04000C43 RID: 3139
			public const string ObjectPath_AnalyticsAIMetadata_1Args = "ObjectPath_AnalyticsAIMetadata_1Args";

			// Token: 0x04000C44 RID: 3140
			public const string ObjectPath_ChangedProperty_0Args = "ObjectPath_ChangedProperty_0Args";

			// Token: 0x04000C45 RID: 3141
			public const string ObjectPath_ChangedProperty_1Args = "ObjectPath_ChangedProperty_1Args";

			// Token: 0x04000C46 RID: 3142
			public const string ObjectPath_ExcludedArtifact_0Args = "ObjectPath_ExcludedArtifact_0Args";

			// Token: 0x04000C47 RID: 3143
			public const string ObjectPath_ExcludedArtifact_1Args = "ObjectPath_ExcludedArtifact_1Args";

			// Token: 0x04000C48 RID: 3144
			public const string ObjectPath_DataCoverageDefinition_0Args = "ObjectPath_DataCoverageDefinition_0Args";

			// Token: 0x04000C49 RID: 3145
			public const string ObjectPath_DataCoverageDefinition_1Args = "ObjectPath_DataCoverageDefinition_1Args";

			// Token: 0x04000C4A RID: 3146
			public const string ObjectPath_CalculationGroupExpression_0Args = "ObjectPath_CalculationGroupExpression_0Args";

			// Token: 0x04000C4B RID: 3147
			public const string ObjectPath_CalculationGroupExpression_1Args_CalculationGroup = "ObjectPath_CalculationGroupExpression_1Args_CalculationGroup";

			// Token: 0x04000C4C RID: 3148
			public const string ObjectPath_Calendar_1Arg = "ObjectPath_Calendar_1Arg";

			// Token: 0x04000C4D RID: 3149
			public const string ObjectPath_Calendar_2Args = "ObjectPath_Calendar_2Args";

			// Token: 0x04000C4E RID: 3150
			public const string ObjectPath_TimeUnitColumnAssociation_1Arg = "ObjectPath_TimeUnitColumnAssociation_1Arg";

			// Token: 0x04000C4F RID: 3151
			public const string ObjectPath_TimeUnitColumnAssociation_2Args = "ObjectPath_TimeUnitColumnAssociation_2Args";

			// Token: 0x04000C50 RID: 3152
			public const string ObjectPath_TimeUnitColumnAssociation_3Args = "ObjectPath_TimeUnitColumnAssociation_3Args";

			// Token: 0x04000C51 RID: 3153
			public const string ObjectPath_CalendarColumnReference_1Arg = "ObjectPath_CalendarColumnReference_1Arg";

			// Token: 0x04000C52 RID: 3154
			public const string ObjectPath_CalendarColumnReference_2Args = "ObjectPath_CalendarColumnReference_2Args";

			// Token: 0x04000C53 RID: 3155
			public const string ObjectPath_CalendarColumnReference_3Args = "ObjectPath_CalendarColumnReference_3Args";

			// Token: 0x04000C54 RID: 3156
			public const string ObjectPath_CalendarColumnReference_4Args = "ObjectPath_CalendarColumnReference_4Args";

			// Token: 0x04000C55 RID: 3157
			public const string ObjectPath_BindingInfo_1Arg = "ObjectPath_BindingInfo_1Arg";

			// Token: 0x04000C56 RID: 3158
			public const string Error_NameIsRequired = "Error_NameIsRequired";

			// Token: 0x04000C57 RID: 3159
			public const string Error_NameIsTooLong = "Error_NameIsTooLong";

			// Token: 0x04000C58 RID: 3160
			public const string Error_NameCannotBeReservedString = "Error_NameCannotBeReservedString";

			// Token: 0x04000C59 RID: 3161
			public const string Error_NameHasInvalidXmlCharacters = "Error_NameHasInvalidXmlCharacters";

			// Token: 0x04000C5A RID: 3162
			public const string ValidNameForReservedStringPattern = "ValidNameForReservedStringPattern";

			// Token: 0x04000C5B RID: 3163
			public const string Error_ValueHasInvalidCharacters = "Error_ValueHasInvalidCharacters";

			// Token: 0x04000C5C RID: 3164
			public const string Exception_Json_TableMustHaveSinglePartitionForMergedMode = "Exception_Json_TableMustHaveSinglePartitionForMergedMode";

			// Token: 0x04000C5D RID: 3165
			public const string Exception_Json_PartitionMustHaveSameNameAsParentTableForMergedMode = "Exception_Json_PartitionMustHaveSameNameAsParentTableForMergedMode";

			// Token: 0x04000C5E RID: 3166
			public const string Exception_Json_PartitionMustHaveEmptyDescriptionForMergedMode = "Exception_Json_PartitionMustHaveEmptyDescriptionForMergedMode";

			// Token: 0x04000C5F RID: 3167
			public const string Exception_Json_PartitionMustHaveNoAnnotationsForMergedMode = "Exception_Json_PartitionMustHaveNoAnnotationsForMergedMode";

			// Token: 0x04000C60 RID: 3168
			public const string Exception_Json_GenerateSchemaWithTranslatablePropertiesNotSupported = "Exception_Json_GenerateSchemaWithTranslatablePropertiesNotSupported";

			// Token: 0x04000C61 RID: 3169
			public const string Exception_CannotSerializeObject = "Exception_CannotSerializeObject";

			// Token: 0x04000C62 RID: 3170
			public const string Exception_CannotDeserializeObjectMalformedInput = "Exception_CannotDeserializeObjectMalformedInput";

			// Token: 0x04000C63 RID: 3171
			public const string Exception_CannotDeserializeObject = "Exception_CannotDeserializeObject";

			// Token: 0x04000C64 RID: 3172
			public const string Exception_CannotDeserializeObjectWithDetails = "Exception_CannotDeserializeObjectWithDetails";

			// Token: 0x04000C65 RID: 3173
			public const string Exception_CannotDeserializeObjectWrongType = "Exception_CannotDeserializeObjectWrongType";

			// Token: 0x04000C66 RID: 3174
			public const string Exception_CannotDeserializeObjectUnknownType = "Exception_CannotDeserializeObjectUnknownType";

			// Token: 0x04000C67 RID: 3175
			public const string Exception_CannotReadPropertyObjectExpected = "Exception_CannotReadPropertyObjectExpected";

			// Token: 0x04000C68 RID: 3176
			public const string Exception_CannotConvertToType = "Exception_CannotConvertToType";

			// Token: 0x04000C69 RID: 3177
			public const string Exception_Json_MissingRequiredProperty = "Exception_Json_MissingRequiredProperty";

			// Token: 0x04000C6A RID: 3178
			public const string Exception_ErrorWithPathAndLineInfo = "Exception_ErrorWithPathAndLineInfo";

			// Token: 0x04000C6B RID: 3179
			public const string Exception_CannotGenerateSchemaForUnknownType = "Exception_CannotGenerateSchemaForUnknownType";

			// Token: 0x04000C6C RID: 3180
			public const string Exception_CannotDeserializeObjectPathMalformedInput = "Exception_CannotDeserializeObjectPathMalformedInput";

			// Token: 0x04000C6D RID: 3181
			public const string Exception_CannotDeserializeObjectPath = "Exception_CannotDeserializeObjectPath";

			// Token: 0x04000C6E RID: 3182
			public const string Exception_CannotDeserializeObjectResolvePathsFailed = "Exception_CannotDeserializeObjectResolvePathsFailed";

			// Token: 0x04000C6F RID: 3183
			public const string Exception_CannotDeserializeObjectResolvePathsFailedWithList = "Exception_CannotDeserializeObjectResolvePathsFailedWithList";

			// Token: 0x04000C70 RID: 3184
			public const string Exception_PropertyHasInvalidJsonContent = "Exception_PropertyHasInvalidJsonContent";

			// Token: 0x04000C71 RID: 3185
			public const string Exception_UnsupportedJsonPrimitiveType = "Exception_UnsupportedJsonPrimitiveType";

			// Token: 0x04000C72 RID: 3186
			public const string Exception_CustomPropertyValueHasInvalidType = "Exception_CustomPropertyValueHasInvalidType";

			// Token: 0x04000C73 RID: 3187
			public const string Validation_UnresolvedLink = "Validation_UnresolvedLink";

			// Token: 0x04000C74 RID: 3188
			public const string Validation_LinkToRemovedObject = "Validation_LinkToRemovedObject";

			// Token: 0x04000C75 RID: 3189
			public const string Validation_LinkToAnotherModel = "Validation_LinkToAnotherModel";

			// Token: 0x04000C76 RID: 3190
			public const string Exception_CannotReadMetadataObjectCollectionWithTypeFromJson = "Exception_CannotReadMetadataObjectCollectionWithTypeFromJson";

			// Token: 0x04000C77 RID: 3191
			public const string Exception_CannotProbeMetadataObjectCollectionFromJson = "Exception_CannotProbeMetadataObjectCollectionFromJson";

			// Token: 0x04000C78 RID: 3192
			public const string Exception_TranslatedObjectNull = "Exception_TranslatedObjectNull";

			// Token: 0x04000C79 RID: 3193
			public const string Exception_ObjectTranslationAlreadyContainsTranslation = "Exception_ObjectTranslationAlreadyContainsTranslation";

			// Token: 0x04000C7A RID: 3194
			public const string Exception_InvalidJsonScript = "Exception_InvalidJsonScript";

			// Token: 0x04000C7B RID: 3195
			public const string Exception_NameCannotBeSetForReferencedObjects = "Exception_NameCannotBeSetForReferencedObjects";

			// Token: 0x04000C7C RID: 3196
			public const string Exception_CantChangeImmutableProperty = "Exception_CantChangeImmutableProperty";

			// Token: 0x04000C7D RID: 3197
			public const string Exception_NameCannotBeSetForReadOnlyNamedObjects = "Exception_NameCannotBeSetForReadOnlyNamedObjects";

			// Token: 0x04000C7E RID: 3198
			public const string Exception_ExternalRoleMemberEmptyIdentityProvider = "Exception_ExternalRoleMemberEmptyIdentityProvider";

			// Token: 0x04000C7F RID: 3199
			public const string Exception_RemovedDatabaseCannotBeAttached = "Exception_RemovedDatabaseCannotBeAttached";

			// Token: 0x04000C80 RID: 3200
			public const string Exception_RemovedModelCannotBeAttached = "Exception_RemovedModelCannotBeAttached";

			// Token: 0x04000C81 RID: 3201
			public const string Exception_ModelAlreadyBelongsToAnotherDatabase = "Exception_ModelAlreadyBelongsToAnotherDatabase";

			// Token: 0x04000C82 RID: 3202
			public const string Exception_ModifyDirtyDatabase = "Exception_ModifyDirtyDatabase";

			// Token: 0x04000C83 RID: 3203
			public const string Exception_PartialRefreshDirtyDatabase = "Exception_PartialRefreshDirtyDatabase";

			// Token: 0x04000C84 RID: 3204
			public const string Exception_OverridesIncompatibleWithRefreshType = "Exception_OverridesIncompatibleWithRefreshType";

			// Token: 0x04000C85 RID: 3205
			public const string Exception_ApplyRefreshPoliciesIncompatibleWithRefreshType = "Exception_ApplyRefreshPoliciesIncompatibleWithRefreshType";

			// Token: 0x04000C86 RID: 3206
			public const string Exception_ApplyRefreshPoliciesSaveInOfflineMode = "Exception_ApplyRefreshPoliciesSaveInOfflineMode";

			// Token: 0x04000C87 RID: 3207
			public const string Exception_ApplyAutomaticAggregationsInOfflineMode = "Exception_ApplyAutomaticAggregationsInOfflineMode";

			// Token: 0x04000C88 RID: 3208
			public const string Exception_ApplyAutomaticAggregationsInvalidCompatLevel = "Exception_ApplyAutomaticAggregationsInvalidCompatLevel";

			// Token: 0x04000C89 RID: 3209
			public const string Exception_OverridesScopeObjectDoesntSupportRefresh = "Exception_OverridesScopeObjectDoesntSupportRefresh";

			// Token: 0x04000C8A RID: 3210
			public const string Exception_OverridesScopeObjectIsEmpty = "Exception_OverridesScopeObjectIsEmpty";

			// Token: 0x04000C8B RID: 3211
			public const string Exception_OverridesScopeObjectCannotBeFound = "Exception_OverridesScopeObjectCannotBeFound";

			// Token: 0x04000C8C RID: 3212
			public const string Exception_OverridesOriginalObjectCannotBeFound = "Exception_OverridesOriginalObjectCannotBeFound";

			// Token: 0x04000C8D RID: 3213
			public const string Exception_OverridesDatasourceCannotBeFound = "Exception_OverridesDatasourceCannotBeFound";

			// Token: 0x04000C8E RID: 3214
			public const string Exception_OverridesOriginalObjectPathIsNull = "Exception_OverridesOriginalObjectPathIsNull";

			// Token: 0x04000C8F RID: 3215
			public const string Exception_UnexpectedJsonToken = "Exception_UnexpectedJsonToken";

			// Token: 0x04000C90 RID: 3216
			public const string Exception_UnexpectedJsonTag = "Exception_UnexpectedJsonTag";

			// Token: 0x04000C91 RID: 3217
			public const string Exception_UnrecognizedJsonCommand = "Exception_UnrecognizedJsonCommand";

			// Token: 0x04000C92 RID: 3218
			public const string Exception_UnexpectedJsonProperty = "Exception_UnexpectedJsonProperty";

			// Token: 0x04000C93 RID: 3219
			public const string Exception_UnexpectedTokensAfterJsonCommand = "Exception_UnexpectedTokensAfterJsonCommand";

			// Token: 0x04000C94 RID: 3220
			public const string Exception_MissingJsonProperty = "Exception_MissingJsonProperty";

			// Token: 0x04000C95 RID: 3221
			public const string Exception_JsonDeserializeObjectInvalidType = "Exception_JsonDeserializeObjectInvalidType";

			// Token: 0x04000C96 RID: 3222
			public const string Exception_JsonSerializationNotSupportedForObjectType = "Exception_JsonSerializationNotSupportedForObjectType";

			// Token: 0x04000C97 RID: 3223
			public const string Exception_JsonScriptCannotExecuteWhenPendingChanges = "Exception_JsonScriptCannotExecuteWhenPendingChanges";

			// Token: 0x04000C98 RID: 3224
			public const string Exception_JsonScriptFailedToExecute = "Exception_JsonScriptFailedToExecute";

			// Token: 0x04000C99 RID: 3225
			public const string Exception_JsonScriptCannotScriptOutObjectWithoutDatabase = "Exception_JsonScriptCannotScriptOutObjectWithoutDatabase";

			// Token: 0x04000C9A RID: 3226
			public const string Exception_JsonScriptCannotScriptOutNonTMDatabase = "Exception_JsonScriptCannotScriptOutNonTMDatabase";

			// Token: 0x04000C9B RID: 3227
			public const string Exception_JsonCommandMustContainExactlyOneObjectDefinition = "Exception_JsonCommandMustContainExactlyOneObjectDefinition";

			// Token: 0x04000C9C RID: 3228
			public const string Exception_JsonCommandObjectNotSpecified = "Exception_JsonCommandObjectNotSpecified";

			// Token: 0x04000C9D RID: 3229
			public const string Exception_JsonCommandObjectDefinitionNotSpecified = "Exception_JsonCommandObjectDefinitionNotSpecified";

			// Token: 0x04000C9E RID: 3230
			public const string Exception_JsonCommandInvalidObjectSpecified = "Exception_JsonCommandInvalidObjectSpecified";

			// Token: 0x04000C9F RID: 3231
			public const string Exception_JsonCommandDatabaseNotSpecified = "Exception_JsonCommandDatabaseNotSpecified";

			// Token: 0x04000CA0 RID: 3232
			public const string Exception_JsonCommandDatabaseNotFound = "Exception_JsonCommandDatabaseNotFound";

			// Token: 0x04000CA1 RID: 3233
			public const string Exception_JsonCommandNonTabularDatabase = "Exception_JsonCommandNonTabularDatabase";

			// Token: 0x04000CA2 RID: 3234
			public const string Exception_JsonCommandCannotFindObject = "Exception_JsonCommandCannotFindObject";

			// Token: 0x04000CA3 RID: 3235
			public const string Exception_JsonCommandCannotFindParentObject = "Exception_JsonCommandCannotFindParentObject";

			// Token: 0x04000CA4 RID: 3236
			public const string Exception_JsonCommandAlterNotSupportedForObjectType = "Exception_JsonCommandAlterNotSupportedForObjectType";

			// Token: 0x04000CA5 RID: 3237
			public const string Exception_JsonCommandCreateOrReplaceNotSupportedForObjectType = "Exception_JsonCommandCreateOrReplaceNotSupportedForObjectType";

			// Token: 0x04000CA6 RID: 3238
			public const string Exception_JsonCommandCreateNotSupportedForObjectType = "Exception_JsonCommandCreateNotSupportedForObjectType";

			// Token: 0x04000CA7 RID: 3239
			public const string Exception_JsonCommandDeleteNotSupportedForObjectType = "Exception_JsonCommandDeleteNotSupportedForObjectType";

			// Token: 0x04000CA8 RID: 3240
			public const string Exception_JsonCommandExportNotSupportedForObjectType = "Exception_JsonCommandExportNotSupportedForObjectType";

			// Token: 0x04000CA9 RID: 3241
			public const string Exception_JsonCommandRefreshNotSupportedForObjectType = "Exception_JsonCommandRefreshNotSupportedForObjectType";

			// Token: 0x04000CAA RID: 3242
			public const string Exception_JsonCommandRefreshPolicyNotSupportedForObjectType = "Exception_JsonCommandRefreshPolicyNotSupportedForObjectType";

			// Token: 0x04000CAB RID: 3243
			public const string Exception_JsonCommandRefreshPolicyNotSupportMoreThanOneObjects = "Exception_JsonCommandRefreshPolicyNotSupportMoreThanOneObjects";

			// Token: 0x04000CAC RID: 3244
			public const string Exception_JsonCommandRefreshPolicyParameterMissing = "Exception_JsonCommandRefreshPolicyParameterMissing";

			// Token: 0x04000CAD RID: 3245
			public const string Exception_JsonCommandRefreshPolicyNotSupportForRefreshType = "Exception_JsonCommandRefreshPolicyNotSupportForRefreshType";

			// Token: 0x04000CAE RID: 3246
			public const string Exception_JsonCommandObjectReferenceAndDefinitionMismatch = "Exception_JsonCommandObjectReferenceAndDefinitionMismatch";

			// Token: 0x04000CAF RID: 3247
			public const string Exception_JsonCommandParentObjectNotSpecified = "Exception_JsonCommandParentObjectNotSpecified";

			// Token: 0x04000CB0 RID: 3248
			public const string Exception_JsonCommandParentObjectNotNeededForDatabase = "Exception_JsonCommandParentObjectNotNeededForDatabase";

			// Token: 0x04000CB1 RID: 3249
			public const string Exception_JsonCommandInvalidDatabaseName = "Exception_JsonCommandInvalidDatabaseName";

			// Token: 0x04000CB2 RID: 3250
			public const string Exception_JsonCommandChildCollectionNotFound = "Exception_JsonCommandChildCollectionNotFound";

			// Token: 0x04000CB3 RID: 3251
			public const string Exception_JsonCommandTypeNotSpecified = "Exception_JsonCommandTypeNotSpecified";

			// Token: 0x04000CB4 RID: 3252
			public const string Exception_JsonCommandRefreshMultipleDbsNotSupported = "Exception_JsonCommandRefreshMultipleDbsNotSupported";

			// Token: 0x04000CB5 RID: 3253
			public const string Exception_JsonCommandSequenceMultipleDbsNotSupported = "Exception_JsonCommandSequenceMultipleDbsNotSupported";

			// Token: 0x04000CB6 RID: 3254
			public const string Exception_JsonCommandRefreshScriptOutObjectsNotSpecified = "Exception_JsonCommandRefreshScriptOutObjectsNotSpecified";

			// Token: 0x04000CB7 RID: 3255
			public const string Exception_JsonCommandScriptOutMultipleDbsNotSupported = "Exception_JsonCommandScriptOutMultipleDbsNotSupported";

			// Token: 0x04000CB8 RID: 3256
			public const string Exception_JsonCommandSequenceNestedNotSupported = "Exception_JsonCommandSequenceNestedNotSupported";

			// Token: 0x04000CB9 RID: 3257
			public const string Exception_JsonCommandSequenceMultipleDatabasesNotSupported = "Exception_JsonCommandSequenceMultipleDatabasesNotSupported";

			// Token: 0x04000CBA RID: 3258
			public const string Exception_JsonCommandParameterIsMissing = "Exception_JsonCommandParameterIsMissing";

			// Token: 0x04000CBB RID: 3259
			public const string Exception_JsonCommandSequenceFinishedExecuting = "Exception_JsonCommandSequenceFinishedExecuting";

			// Token: 0x04000CBC RID: 3260
			public const string Exception_JsonCommandMergePartitionsSourceNameIsEmpty = "Exception_JsonCommandMergePartitionsSourceNameIsEmpty";

			// Token: 0x04000CBD RID: 3261
			public const string Exception_JsonCommandMergePartitionsNoSourcePartitions = "Exception_JsonCommandMergePartitionsNoSourcePartitions";

			// Token: 0x04000CBE RID: 3262
			public const string Exception_JsonCommandMergePartitionsNoTargetPartition = "Exception_JsonCommandMergePartitionsNoTargetPartition";

			// Token: 0x04000CBF RID: 3263
			public const string Exception_JsonCommandMergePartitionsCantFindTargetPartition = "Exception_JsonCommandMergePartitionsCantFindTargetPartition";

			// Token: 0x04000CC0 RID: 3264
			public const string Exception_JsonCommandMergePartitionsCantFindSourcePartition = "Exception_JsonCommandMergePartitionsCantFindSourcePartition";

			// Token: 0x04000CC1 RID: 3265
			public const string Exception_JsonScriptCannotScriptOutMergePartitionsNotSameTable = "Exception_JsonScriptCannotScriptOutMergePartitionsNotSameTable";

			// Token: 0x04000CC2 RID: 3266
			public const string Exception_ModelCannotBeMotified_MergePartitionsRequested = "Exception_ModelCannotBeMotified_MergePartitionsRequested";

			// Token: 0x04000CC3 RID: 3267
			public const string Exception_MergePartitionsForTableAlreadyRequested = "Exception_MergePartitionsForTableAlreadyRequested";

			// Token: 0x04000CC4 RID: 3268
			public const string Exception_PartitionsFromDifferentTablesCannotBeMerged = "Exception_PartitionsFromDifferentTablesCannotBeMerged";

			// Token: 0x04000CC5 RID: 3269
			public const string Exception_DisconnectedPartitionCannotBeMerged = "Exception_DisconnectedPartitionCannotBeMerged";

			// Token: 0x04000CC6 RID: 3270
			public const string Exception_NonEmptyModelCannotBeReplaced = "Exception_NonEmptyModelCannotBeReplaced";

			// Token: 0x04000CC7 RID: 3271
			public const string Exception_CannotApplyRefreshPolicyDisconnectedModel = "Exception_CannotApplyRefreshPolicyDisconnectedModel";

			// Token: 0x04000CC8 RID: 3272
			public const string Exception_CannotApplyRefreshPolicyModifiedModel = "Exception_CannotApplyRefreshPolicyModifiedModel";

			// Token: 0x04000CC9 RID: 3273
			public const string Exception_CannotApplyRefreshPolicyDisconnectedTable = "Exception_CannotApplyRefreshPolicyDisconnectedTable";

			// Token: 0x04000CCA RID: 3274
			public const string Exception_TableRefreshPolicyIsMissing = "Exception_TableRefreshPolicyIsMissing";

			// Token: 0x04000CCB RID: 3275
			public const string Exception_PartitionDoesNotHavePolicyRangePartitionSource = "Exception_PartitionDoesNotHavePolicyRangePartitionSource";

			// Token: 0x04000CCC RID: 3276
			public const string Exception_RefreshPolicyInvalidSourceExpression = "Exception_RefreshPolicyInvalidSourceExpression";

			// Token: 0x04000CCD RID: 3277
			public const string Exception_FailedAddDeserializedObject = "Exception_FailedAddDeserializedObject";

			// Token: 0x04000CCE RID: 3278
			public const string Exception_FailedAddDeserializedNamedObject = "Exception_FailedAddDeserializedNamedObject";

			// Token: 0x04000CCF RID: 3279
			public const string Exception_InvalidEscapedString = "Exception_InvalidEscapedString";

			// Token: 0x04000CD0 RID: 3280
			public const string Exception_InvalidChildCollection = "Exception_InvalidChildCollection";

			// Token: 0x04000CD1 RID: 3281
			public const string Exception_TmdlPropertyUnknown = "Exception_TmdlPropertyUnknown";

			// Token: 0x04000CD2 RID: 3282
			public const string Exception_TmdlPropertyIncompatible = "Exception_TmdlPropertyIncompatible";

			// Token: 0x04000CD3 RID: 3283
			public const string Exception_TmdlPropertyIncompatibleValue = "Exception_TmdlPropertyIncompatibleValue";

			// Token: 0x04000CD4 RID: 3284
			public const string Exception_TmdlPropertyUnexpected = "Exception_TmdlPropertyUnexpected";

			// Token: 0x04000CD5 RID: 3285
			public const string Exception_TmdlPropertyNotExist = "Exception_TmdlPropertyNotExist";

			// Token: 0x04000CD6 RID: 3286
			public const string Exception_TmdlPropertyMismatchScalarType = "Exception_TmdlPropertyMismatchScalarType";

			// Token: 0x04000CD7 RID: 3287
			public const string Exception_TmdlPropertyMismatchEnumType = "Exception_TmdlPropertyMismatchEnumType";

			// Token: 0x04000CD8 RID: 3288
			public const string Exception_TmdlPropertyInvalidObjectKeyValue = "Exception_TmdlPropertyInvalidObjectKeyValue";

			// Token: 0x04000CD9 RID: 3289
			public const string Exception_TmdlPropertyInvalidEnumValue = "Exception_TmdlPropertyInvalidEnumValue";

			// Token: 0x04000CDA RID: 3290
			public const string Exception_TmdlPropertyMismatchValueType = "Exception_TmdlPropertyMismatchValueType";

			// Token: 0x04000CDB RID: 3291
			public const string Exception_TmdlPropertyMismatchNature = "Exception_TmdlPropertyMismatchNature";

			// Token: 0x04000CDC RID: 3292
			public const string Exception_TmdlPropertyUnknownNature = "Exception_TmdlPropertyUnknownNature";

			// Token: 0x04000CDD RID: 3293
			public const string Exception_TmdlPropertyMismatchType = "Exception_TmdlPropertyMismatchType";

			// Token: 0x04000CDE RID: 3294
			public const string Exception_TmdlPropertyRequiresTableOnRead = "Exception_TmdlPropertyRequiresTableOnRead";

			// Token: 0x04000CDF RID: 3295
			public const string Exception_TmdlPropertyNoComplexProperty = "Exception_TmdlPropertyNoComplexProperty";

			// Token: 0x04000CE0 RID: 3296
			public const string Exception_TmdlPropertyMismatchTarget = "Exception_TmdlPropertyMismatchTarget";

			// Token: 0x04000CE1 RID: 3297
			public const string Exception_TmdlPropertyInvalidTarget = "Exception_TmdlPropertyInvalidTarget";

			// Token: 0x04000CE2 RID: 3298
			public const string Exception_TmdlPropertyUnknownTarget = "Exception_TmdlPropertyUnknownTarget";

			// Token: 0x04000CE3 RID: 3299
			public const string Exception_TmdlPropertyMismatchCollectionElements = "Exception_TmdlPropertyMismatchCollectionElements";

			// Token: 0x04000CE4 RID: 3300
			public const string Exception_TmdlObjectInvalidChild = "Exception_TmdlObjectInvalidChild";

			// Token: 0x04000CE5 RID: 3301
			public const string Exception_TmdlObjectNoNameForChild = "Exception_TmdlObjectNoNameForChild";

			// Token: 0x04000CE6 RID: 3302
			public const string Exception_TmdlObjectInvalidNameForChild = "Exception_TmdlObjectInvalidNameForChild";

			// Token: 0x04000CE7 RID: 3303
			public const string Exception_TmdlObjectInvalidDefaultProperty = "Exception_TmdlObjectInvalidDefaultProperty";

			// Token: 0x04000CE8 RID: 3304
			public const string Exception_TmdlObjectInvalidCustomJsonProperty = "Exception_TmdlObjectInvalidCustomJsonProperty";

			// Token: 0x04000CE9 RID: 3305
			public const string Exception_TmdlReaderEOF = "Exception_TmdlReaderEOF";

			// Token: 0x04000CEA RID: 3306
			public const string Exception_InvalidLogicalPath = "Exception_InvalidLogicalPath";

			// Token: 0x04000CEB RID: 3307
			public const string Exception_InvalidStreamNoWrite = "Exception_InvalidStreamNoWrite";

			// Token: 0x04000CEC RID: 3308
			public const string Exception_InvalidStreamNoRead = "Exception_InvalidStreamNoRead";

			// Token: 0x04000CED RID: 3309
			public const string Exception_MissingDocInContext = "Exception_MissingDocInContext";

			// Token: 0x04000CEE RID: 3310
			public const string Exception_DuplicateDocInContext = "Exception_DuplicateDocInContext";

			// Token: 0x04000CEF RID: 3311
			public const string Exception_NoDocsInContext = "Exception_NoDocsInContext";

			// Token: 0x04000CF0 RID: 3312
			public const string Exception_TmdlSerializationSupport = "Exception_TmdlSerializationSupport";

			// Token: 0x04000CF1 RID: 3313
			public const string Exception_JsonSerializationSupport = "Exception_JsonSerializationSupport";

			// Token: 0x04000CF2 RID: 3314
			public const string Exception_ModelObjectInModelUpdateFromContext = "Exception_ModelObjectInModelUpdateFromContext";

			// Token: 0x04000CF3 RID: 3315
			public const string Exception_FailureInModelUpdateFromContext = "Exception_FailureInModelUpdateFromContext";

			// Token: 0x04000CF4 RID: 3316
			public const string Exception_FailureInModelUpdateFromContext2 = "Exception_FailureInModelUpdateFromContext2";

			// Token: 0x04000CF5 RID: 3317
			public const string Exception_FailureInModelUpdateFromContext3 = "Exception_FailureInModelUpdateFromContext3";

			// Token: 0x04000CF6 RID: 3318
			public const string Exception_UnnamedObjectInTmdlObjectSerialization = "Exception_UnnamedObjectInTmdlObjectSerialization";

			// Token: 0x04000CF7 RID: 3319
			public const string Exception_InvalidContentStyle = "Exception_InvalidContentStyle";

			// Token: 0x04000CF8 RID: 3320
			public const string Exception_InvalidFormatBuilderContentStyleForSetOption = "Exception_InvalidFormatBuilderContentStyleForSetOption";

			// Token: 0x04000CF9 RID: 3321
			public const string Exception_InvalidSerializationBuilderContentStyleForSetOption = "Exception_InvalidSerializationBuilderContentStyleForSetOption";

			// Token: 0x04000CFA RID: 3322
			public const string Exception_InvalidCompatModeOnTmdlSerialization = "Exception_InvalidCompatModeOnTmdlSerialization";

			// Token: 0x04000CFB RID: 3323
			public const string Exception_InvalidCompatRequestForTmdlSerialization = "Exception_InvalidCompatRequestForTmdlSerialization";

			// Token: 0x04000CFC RID: 3324
			public const string Exception_InvalidCompatRequestForTmdlSerialization2 = "Exception_InvalidCompatRequestForTmdlSerialization2";

			// Token: 0x04000CFD RID: 3325
			public const string Exception_MismatchTypeOnTmdlSerialization = "Exception_MismatchTypeOnTmdlSerialization";

			// Token: 0x04000CFE RID: 3326
			public const string Exception_InvalidOpContextOnZipController = "Exception_InvalidOpContextOnZipController";

			// Token: 0x04000CFF RID: 3327
			public const string Exception_ComverterMismatchType = "Exception_ComverterMismatchType";

			// Token: 0x04000D00 RID: 3328
			public const string Exception_ComverterMismatchInstanceType = "Exception_ComverterMismatchInstanceType";

			// Token: 0x04000D01 RID: 3329
			public const string Exception_ComverterMismatchTypeInstance = "Exception_ComverterMismatchTypeInstance";

			// Token: 0x04000D02 RID: 3330
			public const string Exception_ComverterMismatchChildTypeParent = "Exception_ComverterMismatchChildTypeParent";

			// Token: 0x04000D03 RID: 3331
			public const string Exception_ComverterMismatchPropertyName = "Exception_ComverterMismatchPropertyName";

			// Token: 0x04000D04 RID: 3332
			public const string Exception_MetadataConfigUnsupportedType = "Exception_MetadataConfigUnsupportedType";

			// Token: 0x04000D05 RID: 3333
			public const string Exception_ObjetNameParsingError = "Exception_ObjetNameParsingError";

			// Token: 0x04000D06 RID: 3334
			public const string Exception_TmdlAmbiguousSource = "Exception_TmdlAmbiguousSource";

			// Token: 0x04000D07 RID: 3335
			public const string Exception_TmdlAmbiguousSource2 = "Exception_TmdlAmbiguousSource2";

			// Token: 0x04000D08 RID: 3336
			public const string Exception_TmdlAmbiguousSource3 = "Exception_TmdlAmbiguousSource3";

			// Token: 0x04000D09 RID: 3337
			public const string Exception_TmdlFormatException_Title = "Exception_TmdlFormatException_Title";

			// Token: 0x04000D0A RID: 3338
			public const string Exception_TmdlFormatException_DetailedError = "Exception_TmdlFormatException_DetailedError";

			// Token: 0x04000D0B RID: 3339
			public const string Exception_TmdlFormatException_SourceInfo = "Exception_TmdlFormatException_SourceInfo";

			// Token: 0x04000D0C RID: 3340
			public const string Exception_TmdlFormatException_Line = "Exception_TmdlFormatException_Line";

			// Token: 0x04000D0D RID: 3341
			public const string Exception_TmdlParserInvalidScalarType = "Exception_TmdlParserInvalidScalarType";

			// Token: 0x04000D0E RID: 3342
			public const string Exception_TmdlParserInlineValueForStruct = "Exception_TmdlParserInlineValueForStruct";

			// Token: 0x04000D0F RID: 3343
			public const string Exception_TmdlParserStringPropertyWithNoValue = "Exception_TmdlParserStringPropertyWithNoValue";

			// Token: 0x04000D10 RID: 3344
			public const string Exception_TmdlParserPropertyWithUnsupportedType = "Exception_TmdlParserPropertyWithUnsupportedType";

			// Token: 0x04000D11 RID: 3345
			public const string Exception_TmdlParserInvalidStructPropertyDefaultPropertyValueType = "Exception_TmdlParserInvalidStructPropertyDefaultPropertyValueType";

			// Token: 0x04000D12 RID: 3346
			public const string Exception_TmdlParserConvertValue = "Exception_TmdlParserConvertValue";

			// Token: 0x04000D13 RID: 3347
			public const string Exception_TmdlParserSingleCharQuoteString = "Exception_TmdlParserSingleCharQuoteString";

			// Token: 0x04000D14 RID: 3348
			public const string Exception_TmdlParserMultiStringLine = "Exception_TmdlParserMultiStringLine";

			// Token: 0x04000D15 RID: 3349
			public const string Exception_TmdlFormatUnexpectedLineType = "Exception_TmdlFormatUnexpectedLineType";

			// Token: 0x04000D16 RID: 3350
			public const string Exception_TmdlFormatUnexpectedLineType2 = "Exception_TmdlFormatUnexpectedLineType2";

			// Token: 0x04000D17 RID: 3351
			public const string Exception_TmdlFormatPropertyMismatch = "Exception_TmdlFormatPropertyMismatch";

			// Token: 0x04000D18 RID: 3352
			public const string Exception_TmdlFormatPropertyUnsupported = "Exception_TmdlFormatPropertyUnsupported";

			// Token: 0x04000D19 RID: 3353
			public const string Exception_TmdlFormatChildUnsupported = "Exception_TmdlFormatChildUnsupported";

			// Token: 0x04000D1A RID: 3354
			public const string Exception_TmdlFormatIndentation = "Exception_TmdlFormatIndentation";

			// Token: 0x04000D1B RID: 3355
			public const string Exception_TmdlFormatNoCollectionItem = "Exception_TmdlFormatNoCollectionItem";

			// Token: 0x04000D1C RID: 3356
			public const string Exception_TmdlFormatNoCollectionExpression = "Exception_TmdlFormatNoCollectionExpression";

			// Token: 0x04000D1D RID: 3357
			public const string Exception_TmdlFormatNoTranslationExpression = "Exception_TmdlFormatNoTranslationExpression";

			// Token: 0x04000D1E RID: 3358
			public const string Exception_TmdlFormatObjectMismatch = "Exception_TmdlFormatObjectMismatch";

			// Token: 0x04000D1F RID: 3359
			public const string Exception_TmdlFormatObjectUnsupported = "Exception_TmdlFormatObjectUnsupported";

			// Token: 0x04000D20 RID: 3360
			public const string Exception_TmdlFormatObjectNameParseError = "Exception_TmdlFormatObjectNameParseError";

			// Token: 0x04000D21 RID: 3361
			public const string Exception_TmdlFormatUnknownKeyword = "Exception_TmdlFormatUnknownKeyword";

			// Token: 0x04000D22 RID: 3362
			public const string Exception_TmdlFormatObjectDefaultProperty = "Exception_TmdlFormatObjectDefaultProperty";

			// Token: 0x04000D23 RID: 3363
			public const string Exception_TmdlFormatDefaultPropertyNotInline = "Exception_TmdlFormatDefaultPropertyNotInline";

			// Token: 0x04000D24 RID: 3364
			public const string Exception_TmdlFormatMissingExpressionValue = "Exception_TmdlFormatMissingExpressionValue";

			// Token: 0x04000D25 RID: 3365
			public const string Exception_TmdlSerializerInvalidPath = "Exception_TmdlSerializerInvalidPath";

			// Token: 0x04000D26 RID: 3366
			public const string Exception_TmdlSerializerInvalidSingleObjectTmdl_DB = "Exception_TmdlSerializerInvalidSingleObjectTmdl_DB";

			// Token: 0x04000D27 RID: 3367
			public const string Exception_TmdlSerializerInvalidSingleObjectTmdl_Multi = "Exception_TmdlSerializerInvalidSingleObjectTmdl_Multi";

			// Token: 0x04000D28 RID: 3368
			public const string Exception_TmdlRefObjectCopy = "Exception_TmdlRefObjectCopy";

			// Token: 0x04000D29 RID: 3369
			public const string Exception_TmdlInvalidValueCast = "Exception_TmdlInvalidValueCast";

			// Token: 0x04000D2A RID: 3370
			public const string ObjetNameParseError_NotFullyConsumed = "ObjetNameParseError_NotFullyConsumed";

			// Token: 0x04000D2B RID: 3371
			public const string ObjetNameParseError_EndWithDot = "ObjetNameParseError_EndWithDot";

			// Token: 0x04000D2C RID: 3372
			public const string ObjetNameParseError_Empty = "ObjetNameParseError_Empty";

			// Token: 0x04000D2D RID: 3373
			public const string ObjetNameParseError_EmptyPart = "ObjetNameParseError_EmptyPart";

			// Token: 0x04000D2E RID: 3374
			public const string ObjetNameParseError_OpenPart = "ObjetNameParseError_OpenPart";

			// Token: 0x04000D2F RID: 3375
			public const string ObjetNameParseError_ControlChar = "ObjetNameParseError_ControlChar";

			// Token: 0x04000D30 RID: 3376
			public const string ObjetNameParseError_NeedQuote = "ObjetNameParseError_NeedQuote";

			// Token: 0x04000D31 RID: 3377
			public const string ObjetNameParseError_SingleQuoteNeedEscape = "ObjetNameParseError_SingleQuoteNeedEscape";

			// Token: 0x04000D32 RID: 3378
			public const string ObjetNameParseError_NameIsFollowedByInvalidToken = "ObjetNameParseError_NameIsFollowedByInvalidToken";

			// Token: 0x04000D33 RID: 3379
			public const string TmdlAmbiguousSourceError_DuplicateDescription = "TmdlAmbiguousSourceError_DuplicateDescription";

			// Token: 0x04000D34 RID: 3380
			public const string TmdlAmbiguousSourceError_DuplicateOrdinal = "TmdlAmbiguousSourceError_DuplicateOrdinal";

			// Token: 0x04000D35 RID: 3381
			public const string TmdlAmbiguousSourceError_DuplicateProperty = "TmdlAmbiguousSourceError_DuplicateProperty";

			// Token: 0x04000D36 RID: 3382
			public const string TmdlAmbiguousSourceError_RoleMembers = "TmdlAmbiguousSourceError_RoleMembers";

			// Token: 0x04000D37 RID: 3383
			public const string TmdlFormatError_DescriptionWithoutObject = "TmdlFormatError_DescriptionWithoutObject";

			// Token: 0x04000D38 RID: 3384
			public const string TmdlFormatError_RefAfterDescription = "TmdlFormatError_RefAfterDescription";

			// Token: 0x04000D39 RID: 3385
			public const string TmdlFormatError_RefWithDefaultProperty = "TmdlFormatError_RefWithDefaultProperty";

			// Token: 0x04000D3A RID: 3386
			public const string TmdlFormatError_RefWithProperty = "TmdlFormatError_RefWithProperty";

			// Token: 0x04000D3B RID: 3387
			public const string TmdlFormatError_TypeNotIndicateName = "TmdlFormatError_TypeNotIndicateName";

			// Token: 0x04000D3C RID: 3388
			public const string TmdlFormatError_TypeIndicateName = "TmdlFormatError_TypeIndicateName";

			// Token: 0x04000D3D RID: 3389
			public const string TmdlFormatError_NotPropertyLine = "TmdlFormatError_NotPropertyLine";

			// Token: 0x04000D3E RID: 3390
			public const string TmdlFormatError_NotStringPropertyLine = "TmdlFormatError_NotStringPropertyLine";
		}
	}
}
