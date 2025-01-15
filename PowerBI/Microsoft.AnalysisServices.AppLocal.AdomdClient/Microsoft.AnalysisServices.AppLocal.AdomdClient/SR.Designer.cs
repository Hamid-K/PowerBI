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
		// Token: 0x06000E5E RID: 3678 RVA: 0x000316BD File Offset: 0x0002F8BD
		protected SR()
		{
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x000316C5 File Offset: 0x0002F8C5
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x000316CC File Offset: 0x0002F8CC
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

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x000316D4 File Offset: 0x0002F8D4
		public static string Server_IsAlreadyConnected
		{
			get
			{
				return SR.Keys.GetString("Server_IsAlreadyConnected");
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x000316E0 File Offset: 0x0002F8E0
		public static string Server_IsNotConnected
		{
			get
			{
				return SR.Keys.GetString("Server_IsNotConnected");
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x000316EC File Offset: 0x0002F8EC
		public static string Server_NoServerName
		{
			get
			{
				return SR.Keys.GetString("Server_NoServerName");
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x000316F8 File Offset: 0x0002F8F8
		public static string Connection_SessionID_SessionIsAlreadyOpen
		{
			get
			{
				return SR.Keys.GetString("Connection_SessionID_SessionIsAlreadyOpen");
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00031704 File Offset: 0x0002F904
		public static string Connection_ShowHiddenObjects_ConnectionAlreadyOpen
		{
			get
			{
				return SR.Keys.GetString("Connection_ShowHiddenObjects_ConnectionAlreadyOpen");
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x00031710 File Offset: 0x0002F910
		public static string Connection_ConnectionString_NotInitialized
		{
			get
			{
				return SR.Keys.GetString("Connection_ConnectionString_NotInitialized");
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0003171C File Offset: 0x0002F91C
		public static string Connection_ConnectionToLocalServerNotSupported
		{
			get
			{
				return SR.Keys.GetString("Connection_ConnectionToLocalServerNotSupported");
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00031728 File Offset: 0x0002F928
		public static string Command_ConnectionIsNotSet
		{
			get
			{
				return SR.Keys.GetString("Command_ConnectionIsNotSet");
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x00031734 File Offset: 0x0002F934
		public static string Command_ConnectionIsNotOpened
		{
			get
			{
				return SR.Keys.GetString("Command_ConnectionIsNotOpened");
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00031740 File Offset: 0x0002F940
		public static string Resultset_IsNotDataset
		{
			get
			{
				return SR.Keys.GetString("Resultset_IsNotDataset");
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0003174C File Offset: 0x0002F94C
		public static string Schema_InvalidGuid
		{
			get
			{
				return SR.Keys.GetString("Schema_InvalidGuid");
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00031758 File Offset: 0x0002F958
		public static string Schema_RestOutOfRange
		{
			get
			{
				return SR.Keys.GetString("Schema_RestOutOfRange");
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x00031764 File Offset: 0x0002F964
		public static string Parameter_Parent_Mismatch
		{
			get
			{
				return SR.Keys.GetString("Parameter_Parent_Mismatch");
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00031770 File Offset: 0x0002F970
		public static string Parameter_Value_Wrong_Type
		{
			get
			{
				return SR.Keys.GetString("Parameter_Value_Wrong_Type");
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0003177C File Offset: 0x0002F97C
		public static string Property_Parent_Mismatch
		{
			get
			{
				return SR.Keys.GetString("Property_Parent_Mismatch");
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00031788 File Offset: 0x0002F988
		public static string Property_Already_Exists
		{
			get
			{
				return SR.Keys.GetString("Property_Already_Exists");
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00031794 File Offset: 0x0002F994
		public static string Property_Value_Wrong_Type
		{
			get
			{
				return SR.Keys.GetString("Property_Value_Wrong_Type");
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x000317A0 File Offset: 0x0002F9A0
		public static string Property_DoesNotExist
		{
			get
			{
				return SR.Keys.GetString("Property_DoesNotExist");
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x000317AC File Offset: 0x0002F9AC
		public static string Property_Key_Wrong_Type
		{
			get
			{
				return SR.Keys.GetString("Property_Key_Wrong_Type");
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x000317B8 File Offset: 0x0002F9B8
		public static string Member_MissingDisplayInfo
		{
			get
			{
				return SR.Keys.GetString("Member_MissingDisplayInfo");
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x000317C4 File Offset: 0x0002F9C4
		public static string Member_MissingLevelName
		{
			get
			{
				return SR.Keys.GetString("Member_MissingLevelName");
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x000317D0 File Offset: 0x0002F9D0
		public static string Member_MissingLevelDepth
		{
			get
			{
				return SR.Keys.GetString("Member_MissingLevelDepth");
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x000317DC File Offset: 0x0002F9DC
		public static string Metadata_CubesCollectionHasbeenUpdated
		{
			get
			{
				return SR.Keys.GetString("Metadata_CubesCollectionHasbeenUpdated");
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x000317E8 File Offset: 0x0002F9E8
		public static string Metadata_MiningServicesCollectionHasbeenUpdated
		{
			get
			{
				return SR.Keys.GetString("Metadata_MiningServicesCollectionHasbeenUpdated");
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x000317F4 File Offset: 0x0002F9F4
		public static string NotSupportedByProvider
		{
			get
			{
				return SR.Keys.GetString("NotSupportedByProvider");
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00031800 File Offset: 0x0002FA00
		public static string NotSupportedWhenConnectionMissing
		{
			get
			{
				return SR.Keys.GetString("NotSupportedWhenConnectionMissing");
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0003180C File Offset: 0x0002FA0C
		public static string NotSupportedOnNonCellsetMember
		{
			get
			{
				return SR.Keys.GetString("NotSupportedOnNonCellsetMember");
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x00031818 File Offset: 0x0002FA18
		public static string Command_CommandStreamDoesNotSupportReadingFrom
		{
			get
			{
				return SR.Keys.GetString("Command_CommandStreamDoesNotSupportReadingFrom");
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x00031824 File Offset: 0x0002FA24
		public static string Command_CommandTextCommandStreamNotSet
		{
			get
			{
				return SR.Keys.GetString("Command_CommandTextCommandStreamNotSet");
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00031830 File Offset: 0x0002FA30
		public static string Command_CommandTextCommandStreamBothSet
		{
			get
			{
				return SR.Keys.GetString("Command_CommandTextCommandStreamBothSet");
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0003183C File Offset: 0x0002FA3C
		public static string Connection_DatabaseNameEmpty
		{
			get
			{
				return SR.Keys.GetString("Connection_DatabaseNameEmpty");
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00031848 File Offset: 0x0002FA48
		public static string Connection_NoInformationAboutDataSourcesReturned
		{
			get
			{
				return SR.Keys.GetString("Connection_NoInformationAboutDataSourcesReturned");
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x00031854 File Offset: 0x0002FA54
		public static string Connection_PropertyNameEmpty
		{
			get
			{
				return SR.Keys.GetString("Connection_PropertyNameEmpty");
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00031860 File Offset: 0x0002FA60
		public static string ICollection_CannotCopyToMultidimensionalArray
		{
			get
			{
				return SR.Keys.GetString("ICollection_CannotCopyToMultidimensionalArray");
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0003186C File Offset: 0x0002FA6C
		public static string ICollection_ItemWithThisNameDoesNotExistInTheCollection
		{
			get
			{
				return SR.Keys.GetString("ICollection_ItemWithThisNameDoesNotExistInTheCollection");
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00031878 File Offset: 0x0002FA78
		public static string Connection_EffectiveUserNameEmpty
		{
			get
			{
				return SR.Keys.GetString("Connection_EffectiveUserNameEmpty");
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00031884 File Offset: 0x0002FA84
		public static string TransactionAlreadyComplete
		{
			get
			{
				return SR.Keys.GetString("TransactionAlreadyComplete");
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00031890 File Offset: 0x0002FA90
		public static string Command_OnlyAdomdTransactionObjectIsSupported
		{
			get
			{
				return SR.Keys.GetString("Command_OnlyAdomdTransactionObjectIsSupported");
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0003189C File Offset: 0x0002FA9C
		public static string Command_OnlyActiveTransactionCanBeAssigned
		{
			get
			{
				return SR.Keys.GetString("Command_OnlyActiveTransactionCanBeAssigned");
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x000318A8 File Offset: 0x0002FAA8
		public static string Command_OnlyTransactionAssociatedWithTheSameConnectionCanBeAssigned
		{
			get
			{
				return SR.Keys.GetString("Command_OnlyTransactionAssociatedWithTheSameConnectionCanBeAssigned");
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x000318B4 File Offset: 0x0002FAB4
		public static string InvalidOperationPriorToFetchAllProperties
		{
			get
			{
				return SR.Keys.GetString("InvalidOperationPriorToFetchAllProperties");
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x000318C0 File Offset: 0x0002FAC0
		public static string MetadataCache_Abandoned
		{
			get
			{
				return SR.Keys.GetString("MetadataCache_Abandoned");
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x000318CC File Offset: 0x0002FACC
		public static string ArgumentErrorUniqueNameEmpty
		{
			get
			{
				return SR.Keys.GetString("ArgumentErrorUniqueNameEmpty");
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x000318D8 File Offset: 0x0002FAD8
		public static string ArgumentErrorInvalidSchemaObjectType
		{
			get
			{
				return SR.Keys.GetString("ArgumentErrorInvalidSchemaObjectType");
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x000318E4 File Offset: 0x0002FAE4
		public static string Collection_IsReadOnly
		{
			get
			{
				return SR.Keys.GetString("Collection_IsReadOnly");
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x000318F0 File Offset: 0x0002FAF0
		public static string InvalidArgument
		{
			get
			{
				return SR.Keys.GetString("InvalidArgument");
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000318FC File Offset: 0x0002FAFC
		public static string Command_InvalidTimeout(string timeout)
		{
			return SR.Keys.GetString("Command_InvalidTimeout", timeout);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00031909 File Offset: 0x0002FB09
		public static string Cellset_propertyIsUnknown(string propertyName)
		{
			return SR.Keys.GetString("Cellset_propertyIsUnknown", propertyName);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00031916 File Offset: 0x0002FB16
		public static string DatasetResponse_HierarchyWithSameNameOnSameAxis(string hierarchyName, string axisName)
		{
			return SR.Keys.GetString("DatasetResponse_HierarchyWithSameNameOnSameAxis", hierarchyName, axisName);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00031924 File Offset: 0x0002FB24
		public static string Schema_UnexpectedResponseForSchema(string schemaName)
		{
			return SR.Keys.GetString("Schema_UnexpectedResponseForSchema", schemaName);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00031931 File Offset: 0x0002FB31
		public static string Schema_PropertyIsMissingOrOfAnUnexpectedType(string schemaName, string propertyName)
		{
			return SR.Keys.GetString("Schema_PropertyIsMissingOrOfAnUnexpectedType", schemaName, propertyName);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003193F File Offset: 0x0002FB3F
		public static string Connection_InvalidProperty(string propertyName)
		{
			return SR.Keys.GetString("Connection_InvalidProperty", propertyName);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003194C File Offset: 0x0002FB4C
		public static string Parameter_Already_Exists(string parameterName)
		{
			return SR.Keys.GetString("Parameter_Already_Exists", parameterName);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00031959 File Offset: 0x0002FB59
		public static string Indexer_ObjectNotFound(string objectName)
		{
			return SR.Keys.GetString("Indexer_ObjectNotFound", objectName);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00031966 File Offset: 0x0002FB66
		public static string Metadata_CubeHasbeenUpdated(string objectName)
		{
			return SR.Keys.GetString("Metadata_CubeHasbeenUpdated", objectName);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00031973 File Offset: 0x0002FB73
		public static string Property_UnknownProperty(string propertyName)
		{
			return SR.Keys.GetString("Property_UnknownProperty", propertyName);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00031980 File Offset: 0x0002FB80
		public static string CellIndexer_InvalidNumberOfAxesIndexers(int numberPresent, int numberProvided)
		{
			return SR.Keys.GetString("CellIndexer_InvalidNumberOfAxesIndexers", numberPresent, numberProvided);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00031998 File Offset: 0x0002FB98
		public static string CellIndexer_IndexOutOfRange(int numberAxis, int maxIndexForAxis)
		{
			return SR.Keys.GetString("CellIndexer_IndexOutOfRange", numberAxis, maxIndexForAxis);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x000319B0 File Offset: 0x0002FBB0
		public static string CellIndexer_InvalidIndexType(int numberAxis)
		{
			return SR.Keys.GetString("CellIndexer_InvalidIndexType", numberAxis);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x000319C2 File Offset: 0x0002FBC2
		public static string CellSet_InvalidStateOfReader(string state)
		{
			return SR.Keys.GetString("CellSet_InvalidStateOfReader", state);
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x000319CF File Offset: 0x0002FBCF
		public static string Connection_FailedToSetProperty(string propName, string propValue)
		{
			return SR.Keys.GetString("Connection_FailedToSetProperty", propName, propValue);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x000319DD File Offset: 0x0002FBDD
		public static string Property_PropertyNotFound(string name)
		{
			return SR.Keys.GetString("Property_PropertyNotFound", name);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x000319EA File Offset: 0x0002FBEA
		public static string Restrictions_TypesMismatch(string restrictionName, string expectedType, string actualType)
		{
			return SR.Keys.GetString("Restrictions_TypesMismatch", restrictionName, expectedType, actualType);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x000319F9 File Offset: 0x0002FBF9
		public static string ICollection_NotEnoughSpaceToCopyTo(int available, int needed)
		{
			return SR.Keys.GetString("ICollection_NotEnoughSpaceToCopyTo", available, needed);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00031A11 File Offset: 0x0002FC11
		public static string ArgumentErrorUnsupportedParameterType(string parameterType)
		{
			return SR.Keys.GetString("ArgumentErrorUnsupportedParameterType", parameterType);
		}

		// Token: 0x020001CE RID: 462
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x060013DA RID: 5082 RVA: 0x00045416 File Offset: 0x00043616
			private Keys()
			{
			}

			// Token: 0x170006EC RID: 1772
			// (get) Token: 0x060013DB RID: 5083 RVA: 0x0004541E File Offset: 0x0004361E
			// (set) Token: 0x060013DC RID: 5084 RVA: 0x00045425 File Offset: 0x00043625
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

			// Token: 0x060013DD RID: 5085 RVA: 0x0004542D File Offset: 0x0004362D
			public static string GetString(string key)
			{
				return SR.Keys.resourceManager.GetString(key, SR.Keys._culture);
			}

			// Token: 0x060013DE RID: 5086 RVA: 0x0004543F File Offset: 0x0004363F
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, SR.Keys.resourceManager.GetString(key, SR.Keys._culture), arg0);
			}

			// Token: 0x060013DF RID: 5087 RVA: 0x0004545C File Offset: 0x0004365C
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, SR.Keys.resourceManager.GetString(key, SR.Keys._culture), arg0, arg1);
			}

			// Token: 0x060013E0 RID: 5088 RVA: 0x0004547A File Offset: 0x0004367A
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, SR.Keys.resourceManager.GetString(key, SR.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x04000DEA RID: 3562
			private static ResourceManager resourceManager = new ResourceManager(typeof(SR).FullName, typeof(SR).Module.Assembly);

			// Token: 0x04000DEB RID: 3563
			private static CultureInfo _culture = null;

			// Token: 0x04000DEC RID: 3564
			public const string Server_IsAlreadyConnected = "Server_IsAlreadyConnected";

			// Token: 0x04000DED RID: 3565
			public const string Server_IsNotConnected = "Server_IsNotConnected";

			// Token: 0x04000DEE RID: 3566
			public const string Server_NoServerName = "Server_NoServerName";

			// Token: 0x04000DEF RID: 3567
			public const string Connection_SessionID_SessionIsAlreadyOpen = "Connection_SessionID_SessionIsAlreadyOpen";

			// Token: 0x04000DF0 RID: 3568
			public const string Connection_ShowHiddenObjects_ConnectionAlreadyOpen = "Connection_ShowHiddenObjects_ConnectionAlreadyOpen";

			// Token: 0x04000DF1 RID: 3569
			public const string Connection_ConnectionString_NotInitialized = "Connection_ConnectionString_NotInitialized";

			// Token: 0x04000DF2 RID: 3570
			public const string Connection_ConnectionToLocalServerNotSupported = "Connection_ConnectionToLocalServerNotSupported";

			// Token: 0x04000DF3 RID: 3571
			public const string Command_ConnectionIsNotSet = "Command_ConnectionIsNotSet";

			// Token: 0x04000DF4 RID: 3572
			public const string Command_ConnectionIsNotOpened = "Command_ConnectionIsNotOpened";

			// Token: 0x04000DF5 RID: 3573
			public const string Command_InvalidTimeout = "Command_InvalidTimeout";

			// Token: 0x04000DF6 RID: 3574
			public const string Cellset_propertyIsUnknown = "Cellset_propertyIsUnknown";

			// Token: 0x04000DF7 RID: 3575
			public const string Resultset_IsNotDataset = "Resultset_IsNotDataset";

			// Token: 0x04000DF8 RID: 3576
			public const string DatasetResponse_HierarchyWithSameNameOnSameAxis = "DatasetResponse_HierarchyWithSameNameOnSameAxis";

			// Token: 0x04000DF9 RID: 3577
			public const string Schema_InvalidGuid = "Schema_InvalidGuid";

			// Token: 0x04000DFA RID: 3578
			public const string Schema_RestOutOfRange = "Schema_RestOutOfRange";

			// Token: 0x04000DFB RID: 3579
			public const string Schema_UnexpectedResponseForSchema = "Schema_UnexpectedResponseForSchema";

			// Token: 0x04000DFC RID: 3580
			public const string Schema_PropertyIsMissingOrOfAnUnexpectedType = "Schema_PropertyIsMissingOrOfAnUnexpectedType";

			// Token: 0x04000DFD RID: 3581
			public const string Connection_InvalidProperty = "Connection_InvalidProperty";

			// Token: 0x04000DFE RID: 3582
			public const string Parameter_Parent_Mismatch = "Parameter_Parent_Mismatch";

			// Token: 0x04000DFF RID: 3583
			public const string Parameter_Already_Exists = "Parameter_Already_Exists";

			// Token: 0x04000E00 RID: 3584
			public const string Parameter_Value_Wrong_Type = "Parameter_Value_Wrong_Type";

			// Token: 0x04000E01 RID: 3585
			public const string Property_Parent_Mismatch = "Property_Parent_Mismatch";

			// Token: 0x04000E02 RID: 3586
			public const string Property_Already_Exists = "Property_Already_Exists";

			// Token: 0x04000E03 RID: 3587
			public const string Property_Value_Wrong_Type = "Property_Value_Wrong_Type";

			// Token: 0x04000E04 RID: 3588
			public const string Property_DoesNotExist = "Property_DoesNotExist";

			// Token: 0x04000E05 RID: 3589
			public const string Property_Key_Wrong_Type = "Property_Key_Wrong_Type";

			// Token: 0x04000E06 RID: 3590
			public const string Member_MissingDisplayInfo = "Member_MissingDisplayInfo";

			// Token: 0x04000E07 RID: 3591
			public const string Member_MissingLevelName = "Member_MissingLevelName";

			// Token: 0x04000E08 RID: 3592
			public const string Member_MissingLevelDepth = "Member_MissingLevelDepth";

			// Token: 0x04000E09 RID: 3593
			public const string Indexer_ObjectNotFound = "Indexer_ObjectNotFound";

			// Token: 0x04000E0A RID: 3594
			public const string Metadata_CubeHasbeenUpdated = "Metadata_CubeHasbeenUpdated";

			// Token: 0x04000E0B RID: 3595
			public const string Metadata_CubesCollectionHasbeenUpdated = "Metadata_CubesCollectionHasbeenUpdated";

			// Token: 0x04000E0C RID: 3596
			public const string Metadata_MiningServicesCollectionHasbeenUpdated = "Metadata_MiningServicesCollectionHasbeenUpdated";

			// Token: 0x04000E0D RID: 3597
			public const string NotSupportedByProvider = "NotSupportedByProvider";

			// Token: 0x04000E0E RID: 3598
			public const string NotSupportedWhenConnectionMissing = "NotSupportedWhenConnectionMissing";

			// Token: 0x04000E0F RID: 3599
			public const string NotSupportedOnNonCellsetMember = "NotSupportedOnNonCellsetMember";

			// Token: 0x04000E10 RID: 3600
			public const string Property_UnknownProperty = "Property_UnknownProperty";

			// Token: 0x04000E11 RID: 3601
			public const string CellIndexer_InvalidNumberOfAxesIndexers = "CellIndexer_InvalidNumberOfAxesIndexers";

			// Token: 0x04000E12 RID: 3602
			public const string CellIndexer_IndexOutOfRange = "CellIndexer_IndexOutOfRange";

			// Token: 0x04000E13 RID: 3603
			public const string CellIndexer_InvalidIndexType = "CellIndexer_InvalidIndexType";

			// Token: 0x04000E14 RID: 3604
			public const string CellSet_InvalidStateOfReader = "CellSet_InvalidStateOfReader";

			// Token: 0x04000E15 RID: 3605
			public const string Command_CommandStreamDoesNotSupportReadingFrom = "Command_CommandStreamDoesNotSupportReadingFrom";

			// Token: 0x04000E16 RID: 3606
			public const string Command_CommandTextCommandStreamNotSet = "Command_CommandTextCommandStreamNotSet";

			// Token: 0x04000E17 RID: 3607
			public const string Command_CommandTextCommandStreamBothSet = "Command_CommandTextCommandStreamBothSet";

			// Token: 0x04000E18 RID: 3608
			public const string Connection_DatabaseNameEmpty = "Connection_DatabaseNameEmpty";

			// Token: 0x04000E19 RID: 3609
			public const string Connection_NoInformationAboutDataSourcesReturned = "Connection_NoInformationAboutDataSourcesReturned";

			// Token: 0x04000E1A RID: 3610
			public const string Connection_PropertyNameEmpty = "Connection_PropertyNameEmpty";

			// Token: 0x04000E1B RID: 3611
			public const string Connection_FailedToSetProperty = "Connection_FailedToSetProperty";

			// Token: 0x04000E1C RID: 3612
			public const string Property_PropertyNotFound = "Property_PropertyNotFound";

			// Token: 0x04000E1D RID: 3613
			public const string Restrictions_TypesMismatch = "Restrictions_TypesMismatch";

			// Token: 0x04000E1E RID: 3614
			public const string ICollection_CannotCopyToMultidimensionalArray = "ICollection_CannotCopyToMultidimensionalArray";

			// Token: 0x04000E1F RID: 3615
			public const string ICollection_NotEnoughSpaceToCopyTo = "ICollection_NotEnoughSpaceToCopyTo";

			// Token: 0x04000E20 RID: 3616
			public const string ICollection_ItemWithThisNameDoesNotExistInTheCollection = "ICollection_ItemWithThisNameDoesNotExistInTheCollection";

			// Token: 0x04000E21 RID: 3617
			public const string Connection_EffectiveUserNameEmpty = "Connection_EffectiveUserNameEmpty";

			// Token: 0x04000E22 RID: 3618
			public const string TransactionAlreadyComplete = "TransactionAlreadyComplete";

			// Token: 0x04000E23 RID: 3619
			public const string Command_OnlyAdomdTransactionObjectIsSupported = "Command_OnlyAdomdTransactionObjectIsSupported";

			// Token: 0x04000E24 RID: 3620
			public const string Command_OnlyActiveTransactionCanBeAssigned = "Command_OnlyActiveTransactionCanBeAssigned";

			// Token: 0x04000E25 RID: 3621
			public const string Command_OnlyTransactionAssociatedWithTheSameConnectionCanBeAssigned = "Command_OnlyTransactionAssociatedWithTheSameConnectionCanBeAssigned";

			// Token: 0x04000E26 RID: 3622
			public const string InvalidOperationPriorToFetchAllProperties = "InvalidOperationPriorToFetchAllProperties";

			// Token: 0x04000E27 RID: 3623
			public const string MetadataCache_Abandoned = "MetadataCache_Abandoned";

			// Token: 0x04000E28 RID: 3624
			public const string ArgumentErrorUniqueNameEmpty = "ArgumentErrorUniqueNameEmpty";

			// Token: 0x04000E29 RID: 3625
			public const string ArgumentErrorInvalidSchemaObjectType = "ArgumentErrorInvalidSchemaObjectType";

			// Token: 0x04000E2A RID: 3626
			public const string ArgumentErrorUnsupportedParameterType = "ArgumentErrorUnsupportedParameterType";

			// Token: 0x04000E2B RID: 3627
			public const string Collection_IsReadOnly = "Collection_IsReadOnly";

			// Token: 0x04000E2C RID: 3628
			public const string InvalidArgument = "InvalidArgument";
		}
	}
}
