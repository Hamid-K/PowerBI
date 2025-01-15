using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000F8 RID: 248
	[CompilerGenerated]
	internal class SR
	{
		// Token: 0x06000E51 RID: 3665 RVA: 0x0003138D File Offset: 0x0002F58D
		protected SR()
		{
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x00031395 File Offset: 0x0002F595
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x0003139C File Offset: 0x0002F59C
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

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x000313A4 File Offset: 0x0002F5A4
		public static string Server_IsAlreadyConnected
		{
			get
			{
				return SR.Keys.GetString("Server_IsAlreadyConnected");
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x000313B0 File Offset: 0x0002F5B0
		public static string Server_IsNotConnected
		{
			get
			{
				return SR.Keys.GetString("Server_IsNotConnected");
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x000313BC File Offset: 0x0002F5BC
		public static string Server_NoServerName
		{
			get
			{
				return SR.Keys.GetString("Server_NoServerName");
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x000313C8 File Offset: 0x0002F5C8
		public static string Connection_SessionID_SessionIsAlreadyOpen
		{
			get
			{
				return SR.Keys.GetString("Connection_SessionID_SessionIsAlreadyOpen");
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x000313D4 File Offset: 0x0002F5D4
		public static string Connection_ShowHiddenObjects_ConnectionAlreadyOpen
		{
			get
			{
				return SR.Keys.GetString("Connection_ShowHiddenObjects_ConnectionAlreadyOpen");
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x000313E0 File Offset: 0x0002F5E0
		public static string Connection_ConnectionString_NotInitialized
		{
			get
			{
				return SR.Keys.GetString("Connection_ConnectionString_NotInitialized");
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x000313EC File Offset: 0x0002F5EC
		public static string Connection_ConnectionToLocalServerNotSupported
		{
			get
			{
				return SR.Keys.GetString("Connection_ConnectionToLocalServerNotSupported");
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x000313F8 File Offset: 0x0002F5F8
		public static string Command_ConnectionIsNotSet
		{
			get
			{
				return SR.Keys.GetString("Command_ConnectionIsNotSet");
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x00031404 File Offset: 0x0002F604
		public static string Command_ConnectionIsNotOpened
		{
			get
			{
				return SR.Keys.GetString("Command_ConnectionIsNotOpened");
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00031410 File Offset: 0x0002F610
		public static string Resultset_IsNotDataset
		{
			get
			{
				return SR.Keys.GetString("Resultset_IsNotDataset");
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0003141C File Offset: 0x0002F61C
		public static string Schema_InvalidGuid
		{
			get
			{
				return SR.Keys.GetString("Schema_InvalidGuid");
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00031428 File Offset: 0x0002F628
		public static string Schema_RestOutOfRange
		{
			get
			{
				return SR.Keys.GetString("Schema_RestOutOfRange");
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x00031434 File Offset: 0x0002F634
		public static string Parameter_Parent_Mismatch
		{
			get
			{
				return SR.Keys.GetString("Parameter_Parent_Mismatch");
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x00031440 File Offset: 0x0002F640
		public static string Parameter_Value_Wrong_Type
		{
			get
			{
				return SR.Keys.GetString("Parameter_Value_Wrong_Type");
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0003144C File Offset: 0x0002F64C
		public static string Property_Parent_Mismatch
		{
			get
			{
				return SR.Keys.GetString("Property_Parent_Mismatch");
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x00031458 File Offset: 0x0002F658
		public static string Property_Already_Exists
		{
			get
			{
				return SR.Keys.GetString("Property_Already_Exists");
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00031464 File Offset: 0x0002F664
		public static string Property_Value_Wrong_Type
		{
			get
			{
				return SR.Keys.GetString("Property_Value_Wrong_Type");
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00031470 File Offset: 0x0002F670
		public static string Property_DoesNotExist
		{
			get
			{
				return SR.Keys.GetString("Property_DoesNotExist");
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0003147C File Offset: 0x0002F67C
		public static string Property_Key_Wrong_Type
		{
			get
			{
				return SR.Keys.GetString("Property_Key_Wrong_Type");
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x00031488 File Offset: 0x0002F688
		public static string Member_MissingDisplayInfo
		{
			get
			{
				return SR.Keys.GetString("Member_MissingDisplayInfo");
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00031494 File Offset: 0x0002F694
		public static string Member_MissingLevelName
		{
			get
			{
				return SR.Keys.GetString("Member_MissingLevelName");
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x000314A0 File Offset: 0x0002F6A0
		public static string Member_MissingLevelDepth
		{
			get
			{
				return SR.Keys.GetString("Member_MissingLevelDepth");
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x000314AC File Offset: 0x0002F6AC
		public static string Metadata_CubesCollectionHasbeenUpdated
		{
			get
			{
				return SR.Keys.GetString("Metadata_CubesCollectionHasbeenUpdated");
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x000314B8 File Offset: 0x0002F6B8
		public static string Metadata_MiningServicesCollectionHasbeenUpdated
		{
			get
			{
				return SR.Keys.GetString("Metadata_MiningServicesCollectionHasbeenUpdated");
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x000314C4 File Offset: 0x0002F6C4
		public static string NotSupportedByProvider
		{
			get
			{
				return SR.Keys.GetString("NotSupportedByProvider");
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x000314D0 File Offset: 0x0002F6D0
		public static string NotSupportedWhenConnectionMissing
		{
			get
			{
				return SR.Keys.GetString("NotSupportedWhenConnectionMissing");
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x000314DC File Offset: 0x0002F6DC
		public static string NotSupportedOnNonCellsetMember
		{
			get
			{
				return SR.Keys.GetString("NotSupportedOnNonCellsetMember");
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x000314E8 File Offset: 0x0002F6E8
		public static string Command_CommandStreamDoesNotSupportReadingFrom
		{
			get
			{
				return SR.Keys.GetString("Command_CommandStreamDoesNotSupportReadingFrom");
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x000314F4 File Offset: 0x0002F6F4
		public static string Command_CommandTextCommandStreamNotSet
		{
			get
			{
				return SR.Keys.GetString("Command_CommandTextCommandStreamNotSet");
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00031500 File Offset: 0x0002F700
		public static string Command_CommandTextCommandStreamBothSet
		{
			get
			{
				return SR.Keys.GetString("Command_CommandTextCommandStreamBothSet");
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0003150C File Offset: 0x0002F70C
		public static string Connection_DatabaseNameEmpty
		{
			get
			{
				return SR.Keys.GetString("Connection_DatabaseNameEmpty");
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00031518 File Offset: 0x0002F718
		public static string Connection_NoInformationAboutDataSourcesReturned
		{
			get
			{
				return SR.Keys.GetString("Connection_NoInformationAboutDataSourcesReturned");
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x00031524 File Offset: 0x0002F724
		public static string Connection_PropertyNameEmpty
		{
			get
			{
				return SR.Keys.GetString("Connection_PropertyNameEmpty");
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00031530 File Offset: 0x0002F730
		public static string ICollection_CannotCopyToMultidimensionalArray
		{
			get
			{
				return SR.Keys.GetString("ICollection_CannotCopyToMultidimensionalArray");
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x0003153C File Offset: 0x0002F73C
		public static string ICollection_ItemWithThisNameDoesNotExistInTheCollection
		{
			get
			{
				return SR.Keys.GetString("ICollection_ItemWithThisNameDoesNotExistInTheCollection");
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x00031548 File Offset: 0x0002F748
		public static string Connection_EffectiveUserNameEmpty
		{
			get
			{
				return SR.Keys.GetString("Connection_EffectiveUserNameEmpty");
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x00031554 File Offset: 0x0002F754
		public static string TransactionAlreadyComplete
		{
			get
			{
				return SR.Keys.GetString("TransactionAlreadyComplete");
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x00031560 File Offset: 0x0002F760
		public static string Command_OnlyAdomdTransactionObjectIsSupported
		{
			get
			{
				return SR.Keys.GetString("Command_OnlyAdomdTransactionObjectIsSupported");
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0003156C File Offset: 0x0002F76C
		public static string Command_OnlyActiveTransactionCanBeAssigned
		{
			get
			{
				return SR.Keys.GetString("Command_OnlyActiveTransactionCanBeAssigned");
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00031578 File Offset: 0x0002F778
		public static string Command_OnlyTransactionAssociatedWithTheSameConnectionCanBeAssigned
		{
			get
			{
				return SR.Keys.GetString("Command_OnlyTransactionAssociatedWithTheSameConnectionCanBeAssigned");
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x00031584 File Offset: 0x0002F784
		public static string InvalidOperationPriorToFetchAllProperties
		{
			get
			{
				return SR.Keys.GetString("InvalidOperationPriorToFetchAllProperties");
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x00031590 File Offset: 0x0002F790
		public static string MetadataCache_Abandoned
		{
			get
			{
				return SR.Keys.GetString("MetadataCache_Abandoned");
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0003159C File Offset: 0x0002F79C
		public static string ArgumentErrorUniqueNameEmpty
		{
			get
			{
				return SR.Keys.GetString("ArgumentErrorUniqueNameEmpty");
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x000315A8 File Offset: 0x0002F7A8
		public static string ArgumentErrorInvalidSchemaObjectType
		{
			get
			{
				return SR.Keys.GetString("ArgumentErrorInvalidSchemaObjectType");
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x000315B4 File Offset: 0x0002F7B4
		public static string Collection_IsReadOnly
		{
			get
			{
				return SR.Keys.GetString("Collection_IsReadOnly");
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x000315C0 File Offset: 0x0002F7C0
		public static string InvalidArgument
		{
			get
			{
				return SR.Keys.GetString("InvalidArgument");
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x000315CC File Offset: 0x0002F7CC
		public static string Command_InvalidTimeout(string timeout)
		{
			return SR.Keys.GetString("Command_InvalidTimeout", timeout);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000315D9 File Offset: 0x0002F7D9
		public static string Cellset_propertyIsUnknown(string propertyName)
		{
			return SR.Keys.GetString("Cellset_propertyIsUnknown", propertyName);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x000315E6 File Offset: 0x0002F7E6
		public static string DatasetResponse_HierarchyWithSameNameOnSameAxis(string hierarchyName, string axisName)
		{
			return SR.Keys.GetString("DatasetResponse_HierarchyWithSameNameOnSameAxis", hierarchyName, axisName);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x000315F4 File Offset: 0x0002F7F4
		public static string Schema_UnexpectedResponseForSchema(string schemaName)
		{
			return SR.Keys.GetString("Schema_UnexpectedResponseForSchema", schemaName);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00031601 File Offset: 0x0002F801
		public static string Schema_PropertyIsMissingOrOfAnUnexpectedType(string schemaName, string propertyName)
		{
			return SR.Keys.GetString("Schema_PropertyIsMissingOrOfAnUnexpectedType", schemaName, propertyName);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0003160F File Offset: 0x0002F80F
		public static string Connection_InvalidProperty(string propertyName)
		{
			return SR.Keys.GetString("Connection_InvalidProperty", propertyName);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003161C File Offset: 0x0002F81C
		public static string Parameter_Already_Exists(string parameterName)
		{
			return SR.Keys.GetString("Parameter_Already_Exists", parameterName);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00031629 File Offset: 0x0002F829
		public static string Indexer_ObjectNotFound(string objectName)
		{
			return SR.Keys.GetString("Indexer_ObjectNotFound", objectName);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00031636 File Offset: 0x0002F836
		public static string Metadata_CubeHasbeenUpdated(string objectName)
		{
			return SR.Keys.GetString("Metadata_CubeHasbeenUpdated", objectName);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00031643 File Offset: 0x0002F843
		public static string Property_UnknownProperty(string propertyName)
		{
			return SR.Keys.GetString("Property_UnknownProperty", propertyName);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00031650 File Offset: 0x0002F850
		public static string CellIndexer_InvalidNumberOfAxesIndexers(int numberPresent, int numberProvided)
		{
			return SR.Keys.GetString("CellIndexer_InvalidNumberOfAxesIndexers", numberPresent, numberProvided);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00031668 File Offset: 0x0002F868
		public static string CellIndexer_IndexOutOfRange(int numberAxis, int maxIndexForAxis)
		{
			return SR.Keys.GetString("CellIndexer_IndexOutOfRange", numberAxis, maxIndexForAxis);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00031680 File Offset: 0x0002F880
		public static string CellIndexer_InvalidIndexType(int numberAxis)
		{
			return SR.Keys.GetString("CellIndexer_InvalidIndexType", numberAxis);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00031692 File Offset: 0x0002F892
		public static string CellSet_InvalidStateOfReader(string state)
		{
			return SR.Keys.GetString("CellSet_InvalidStateOfReader", state);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003169F File Offset: 0x0002F89F
		public static string Connection_FailedToSetProperty(string propName, string propValue)
		{
			return SR.Keys.GetString("Connection_FailedToSetProperty", propName, propValue);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000316AD File Offset: 0x0002F8AD
		public static string Property_PropertyNotFound(string name)
		{
			return SR.Keys.GetString("Property_PropertyNotFound", name);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000316BA File Offset: 0x0002F8BA
		public static string Restrictions_TypesMismatch(string restrictionName, string expectedType, string actualType)
		{
			return SR.Keys.GetString("Restrictions_TypesMismatch", restrictionName, expectedType, actualType);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x000316C9 File Offset: 0x0002F8C9
		public static string ICollection_NotEnoughSpaceToCopyTo(int available, int needed)
		{
			return SR.Keys.GetString("ICollection_NotEnoughSpaceToCopyTo", available, needed);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x000316E1 File Offset: 0x0002F8E1
		public static string ArgumentErrorUnsupportedParameterType(string parameterType)
		{
			return SR.Keys.GetString("ArgumentErrorUnsupportedParameterType", parameterType);
		}

		// Token: 0x020001CE RID: 462
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x060013CD RID: 5069 RVA: 0x00044EDA File Offset: 0x000430DA
			private Keys()
			{
			}

			// Token: 0x170006E6 RID: 1766
			// (get) Token: 0x060013CE RID: 5070 RVA: 0x00044EE2 File Offset: 0x000430E2
			// (set) Token: 0x060013CF RID: 5071 RVA: 0x00044EE9 File Offset: 0x000430E9
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

			// Token: 0x060013D0 RID: 5072 RVA: 0x00044EF1 File Offset: 0x000430F1
			public static string GetString(string key)
			{
				return SR.Keys.resourceManager.GetString(key, SR.Keys._culture);
			}

			// Token: 0x060013D1 RID: 5073 RVA: 0x00044F03 File Offset: 0x00043103
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, SR.Keys.resourceManager.GetString(key, SR.Keys._culture), arg0);
			}

			// Token: 0x060013D2 RID: 5074 RVA: 0x00044F20 File Offset: 0x00043120
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, SR.Keys.resourceManager.GetString(key, SR.Keys._culture), arg0, arg1);
			}

			// Token: 0x060013D3 RID: 5075 RVA: 0x00044F3E File Offset: 0x0004313E
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, SR.Keys.resourceManager.GetString(key, SR.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x04000DD9 RID: 3545
			private static ResourceManager resourceManager = new ResourceManager(typeof(SR).FullName, typeof(SR).Module.Assembly);

			// Token: 0x04000DDA RID: 3546
			private static CultureInfo _culture = null;

			// Token: 0x04000DDB RID: 3547
			public const string Server_IsAlreadyConnected = "Server_IsAlreadyConnected";

			// Token: 0x04000DDC RID: 3548
			public const string Server_IsNotConnected = "Server_IsNotConnected";

			// Token: 0x04000DDD RID: 3549
			public const string Server_NoServerName = "Server_NoServerName";

			// Token: 0x04000DDE RID: 3550
			public const string Connection_SessionID_SessionIsAlreadyOpen = "Connection_SessionID_SessionIsAlreadyOpen";

			// Token: 0x04000DDF RID: 3551
			public const string Connection_ShowHiddenObjects_ConnectionAlreadyOpen = "Connection_ShowHiddenObjects_ConnectionAlreadyOpen";

			// Token: 0x04000DE0 RID: 3552
			public const string Connection_ConnectionString_NotInitialized = "Connection_ConnectionString_NotInitialized";

			// Token: 0x04000DE1 RID: 3553
			public const string Connection_ConnectionToLocalServerNotSupported = "Connection_ConnectionToLocalServerNotSupported";

			// Token: 0x04000DE2 RID: 3554
			public const string Command_ConnectionIsNotSet = "Command_ConnectionIsNotSet";

			// Token: 0x04000DE3 RID: 3555
			public const string Command_ConnectionIsNotOpened = "Command_ConnectionIsNotOpened";

			// Token: 0x04000DE4 RID: 3556
			public const string Command_InvalidTimeout = "Command_InvalidTimeout";

			// Token: 0x04000DE5 RID: 3557
			public const string Cellset_propertyIsUnknown = "Cellset_propertyIsUnknown";

			// Token: 0x04000DE6 RID: 3558
			public const string Resultset_IsNotDataset = "Resultset_IsNotDataset";

			// Token: 0x04000DE7 RID: 3559
			public const string DatasetResponse_HierarchyWithSameNameOnSameAxis = "DatasetResponse_HierarchyWithSameNameOnSameAxis";

			// Token: 0x04000DE8 RID: 3560
			public const string Schema_InvalidGuid = "Schema_InvalidGuid";

			// Token: 0x04000DE9 RID: 3561
			public const string Schema_RestOutOfRange = "Schema_RestOutOfRange";

			// Token: 0x04000DEA RID: 3562
			public const string Schema_UnexpectedResponseForSchema = "Schema_UnexpectedResponseForSchema";

			// Token: 0x04000DEB RID: 3563
			public const string Schema_PropertyIsMissingOrOfAnUnexpectedType = "Schema_PropertyIsMissingOrOfAnUnexpectedType";

			// Token: 0x04000DEC RID: 3564
			public const string Connection_InvalidProperty = "Connection_InvalidProperty";

			// Token: 0x04000DED RID: 3565
			public const string Parameter_Parent_Mismatch = "Parameter_Parent_Mismatch";

			// Token: 0x04000DEE RID: 3566
			public const string Parameter_Already_Exists = "Parameter_Already_Exists";

			// Token: 0x04000DEF RID: 3567
			public const string Parameter_Value_Wrong_Type = "Parameter_Value_Wrong_Type";

			// Token: 0x04000DF0 RID: 3568
			public const string Property_Parent_Mismatch = "Property_Parent_Mismatch";

			// Token: 0x04000DF1 RID: 3569
			public const string Property_Already_Exists = "Property_Already_Exists";

			// Token: 0x04000DF2 RID: 3570
			public const string Property_Value_Wrong_Type = "Property_Value_Wrong_Type";

			// Token: 0x04000DF3 RID: 3571
			public const string Property_DoesNotExist = "Property_DoesNotExist";

			// Token: 0x04000DF4 RID: 3572
			public const string Property_Key_Wrong_Type = "Property_Key_Wrong_Type";

			// Token: 0x04000DF5 RID: 3573
			public const string Member_MissingDisplayInfo = "Member_MissingDisplayInfo";

			// Token: 0x04000DF6 RID: 3574
			public const string Member_MissingLevelName = "Member_MissingLevelName";

			// Token: 0x04000DF7 RID: 3575
			public const string Member_MissingLevelDepth = "Member_MissingLevelDepth";

			// Token: 0x04000DF8 RID: 3576
			public const string Indexer_ObjectNotFound = "Indexer_ObjectNotFound";

			// Token: 0x04000DF9 RID: 3577
			public const string Metadata_CubeHasbeenUpdated = "Metadata_CubeHasbeenUpdated";

			// Token: 0x04000DFA RID: 3578
			public const string Metadata_CubesCollectionHasbeenUpdated = "Metadata_CubesCollectionHasbeenUpdated";

			// Token: 0x04000DFB RID: 3579
			public const string Metadata_MiningServicesCollectionHasbeenUpdated = "Metadata_MiningServicesCollectionHasbeenUpdated";

			// Token: 0x04000DFC RID: 3580
			public const string NotSupportedByProvider = "NotSupportedByProvider";

			// Token: 0x04000DFD RID: 3581
			public const string NotSupportedWhenConnectionMissing = "NotSupportedWhenConnectionMissing";

			// Token: 0x04000DFE RID: 3582
			public const string NotSupportedOnNonCellsetMember = "NotSupportedOnNonCellsetMember";

			// Token: 0x04000DFF RID: 3583
			public const string Property_UnknownProperty = "Property_UnknownProperty";

			// Token: 0x04000E00 RID: 3584
			public const string CellIndexer_InvalidNumberOfAxesIndexers = "CellIndexer_InvalidNumberOfAxesIndexers";

			// Token: 0x04000E01 RID: 3585
			public const string CellIndexer_IndexOutOfRange = "CellIndexer_IndexOutOfRange";

			// Token: 0x04000E02 RID: 3586
			public const string CellIndexer_InvalidIndexType = "CellIndexer_InvalidIndexType";

			// Token: 0x04000E03 RID: 3587
			public const string CellSet_InvalidStateOfReader = "CellSet_InvalidStateOfReader";

			// Token: 0x04000E04 RID: 3588
			public const string Command_CommandStreamDoesNotSupportReadingFrom = "Command_CommandStreamDoesNotSupportReadingFrom";

			// Token: 0x04000E05 RID: 3589
			public const string Command_CommandTextCommandStreamNotSet = "Command_CommandTextCommandStreamNotSet";

			// Token: 0x04000E06 RID: 3590
			public const string Command_CommandTextCommandStreamBothSet = "Command_CommandTextCommandStreamBothSet";

			// Token: 0x04000E07 RID: 3591
			public const string Connection_DatabaseNameEmpty = "Connection_DatabaseNameEmpty";

			// Token: 0x04000E08 RID: 3592
			public const string Connection_NoInformationAboutDataSourcesReturned = "Connection_NoInformationAboutDataSourcesReturned";

			// Token: 0x04000E09 RID: 3593
			public const string Connection_PropertyNameEmpty = "Connection_PropertyNameEmpty";

			// Token: 0x04000E0A RID: 3594
			public const string Connection_FailedToSetProperty = "Connection_FailedToSetProperty";

			// Token: 0x04000E0B RID: 3595
			public const string Property_PropertyNotFound = "Property_PropertyNotFound";

			// Token: 0x04000E0C RID: 3596
			public const string Restrictions_TypesMismatch = "Restrictions_TypesMismatch";

			// Token: 0x04000E0D RID: 3597
			public const string ICollection_CannotCopyToMultidimensionalArray = "ICollection_CannotCopyToMultidimensionalArray";

			// Token: 0x04000E0E RID: 3598
			public const string ICollection_NotEnoughSpaceToCopyTo = "ICollection_NotEnoughSpaceToCopyTo";

			// Token: 0x04000E0F RID: 3599
			public const string ICollection_ItemWithThisNameDoesNotExistInTheCollection = "ICollection_ItemWithThisNameDoesNotExistInTheCollection";

			// Token: 0x04000E10 RID: 3600
			public const string Connection_EffectiveUserNameEmpty = "Connection_EffectiveUserNameEmpty";

			// Token: 0x04000E11 RID: 3601
			public const string TransactionAlreadyComplete = "TransactionAlreadyComplete";

			// Token: 0x04000E12 RID: 3602
			public const string Command_OnlyAdomdTransactionObjectIsSupported = "Command_OnlyAdomdTransactionObjectIsSupported";

			// Token: 0x04000E13 RID: 3603
			public const string Command_OnlyActiveTransactionCanBeAssigned = "Command_OnlyActiveTransactionCanBeAssigned";

			// Token: 0x04000E14 RID: 3604
			public const string Command_OnlyTransactionAssociatedWithTheSameConnectionCanBeAssigned = "Command_OnlyTransactionAssociatedWithTheSameConnectionCanBeAssigned";

			// Token: 0x04000E15 RID: 3605
			public const string InvalidOperationPriorToFetchAllProperties = "InvalidOperationPriorToFetchAllProperties";

			// Token: 0x04000E16 RID: 3606
			public const string MetadataCache_Abandoned = "MetadataCache_Abandoned";

			// Token: 0x04000E17 RID: 3607
			public const string ArgumentErrorUniqueNameEmpty = "ArgumentErrorUniqueNameEmpty";

			// Token: 0x04000E18 RID: 3608
			public const string ArgumentErrorInvalidSchemaObjectType = "ArgumentErrorInvalidSchemaObjectType";

			// Token: 0x04000E19 RID: 3609
			public const string ArgumentErrorUnsupportedParameterType = "ArgumentErrorUnsupportedParameterType";

			// Token: 0x04000E1A RID: 3610
			public const string Collection_IsReadOnly = "Collection_IsReadOnly";

			// Token: 0x04000E1B RID: 3611
			public const string InvalidArgument = "InvalidArgument";
		}
	}
}
