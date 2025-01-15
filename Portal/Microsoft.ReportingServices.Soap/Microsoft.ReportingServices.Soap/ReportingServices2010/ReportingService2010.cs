using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000002 RID: 2
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "ReportingService2010Soap", Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[XmlInclude(typeof(ExpirationDefinition))]
	[XmlInclude(typeof(RecurrencePattern))]
	[XmlInclude(typeof(ScheduleDefinitionOrReference))]
	[XmlInclude(typeof(DataSourceDefinitionOrReference))]
	public class ReportingService2010 : SoapHttpClientProtocol
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ReportingService2010()
		{
			base.Url = "http://localhost/ReportServer/ReportService2010.asmx";
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002063 File Offset: 0x00000263
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000206B File Offset: 0x0000026B
		public TrustedUserHeader TrustedUserHeaderValue
		{
			get
			{
				return this.trustedUserHeaderValueField;
			}
			set
			{
				this.trustedUserHeaderValueField = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002074 File Offset: 0x00000274
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000207C File Offset: 0x0000027C
		public ServerInfoHeader ServerInfoHeaderValue
		{
			get
			{
				return this.serverInfoHeaderValueField;
			}
			set
			{
				this.serverInfoHeaderValueField = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002085 File Offset: 0x00000285
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000208D File Offset: 0x0000028D
		public ItemNamespaceHeader ItemNamespaceHeaderValue
		{
			get
			{
				return this.itemNamespaceHeaderValueField;
			}
			set
			{
				this.itemNamespaceHeaderValueField = value;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000008 RID: 8 RVA: 0x00002098 File Offset: 0x00000298
		// (remove) Token: 0x06000009 RID: 9 RVA: 0x000020D0 File Offset: 0x000002D0
		public event CreateCatalogItemCompletedEventHandler CreateCatalogItemCompleted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600000A RID: 10 RVA: 0x00002108 File Offset: 0x00000308
		// (remove) Token: 0x0600000B RID: 11 RVA: 0x00002140 File Offset: 0x00000340
		public event SetItemDefinitionCompletedEventHandler SetItemDefinitionCompleted;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600000C RID: 12 RVA: 0x00002178 File Offset: 0x00000378
		// (remove) Token: 0x0600000D RID: 13 RVA: 0x000021B0 File Offset: 0x000003B0
		public event GetItemDefinitionCompletedEventHandler GetItemDefinitionCompleted;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600000E RID: 14 RVA: 0x000021E8 File Offset: 0x000003E8
		// (remove) Token: 0x0600000F RID: 15 RVA: 0x00002220 File Offset: 0x00000420
		public event GetItemTypeCompletedEventHandler GetItemTypeCompleted;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000010 RID: 16 RVA: 0x00002258 File Offset: 0x00000458
		// (remove) Token: 0x06000011 RID: 17 RVA: 0x00002290 File Offset: 0x00000490
		public event DeleteItemCompletedEventHandler DeleteItemCompleted;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000012 RID: 18 RVA: 0x000022C8 File Offset: 0x000004C8
		// (remove) Token: 0x06000013 RID: 19 RVA: 0x00002300 File Offset: 0x00000500
		public event MoveItemCompletedEventHandler MoveItemCompleted;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000014 RID: 20 RVA: 0x00002338 File Offset: 0x00000538
		// (remove) Token: 0x06000015 RID: 21 RVA: 0x00002370 File Offset: 0x00000570
		public event InheritParentSecurityCompletedEventHandler InheritParentSecurityCompleted;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000016 RID: 22 RVA: 0x000023A8 File Offset: 0x000005A8
		// (remove) Token: 0x06000017 RID: 23 RVA: 0x000023E0 File Offset: 0x000005E0
		public event ListItemHistoryCompletedEventHandler ListItemHistoryCompleted;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000018 RID: 24 RVA: 0x00002418 File Offset: 0x00000618
		// (remove) Token: 0x06000019 RID: 25 RVA: 0x00002450 File Offset: 0x00000650
		public event ListChildrenCompletedEventHandler ListChildrenCompleted;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600001A RID: 26 RVA: 0x00002488 File Offset: 0x00000688
		// (remove) Token: 0x0600001B RID: 27 RVA: 0x000024C0 File Offset: 0x000006C0
		public event ListDependentItemsCompletedEventHandler ListDependentItemsCompleted;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600001C RID: 28 RVA: 0x000024F8 File Offset: 0x000006F8
		// (remove) Token: 0x0600001D RID: 29 RVA: 0x00002530 File Offset: 0x00000730
		public event FindItemsCompletedEventHandler FindItemsCompleted;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600001E RID: 30 RVA: 0x00002568 File Offset: 0x00000768
		// (remove) Token: 0x0600001F RID: 31 RVA: 0x000025A0 File Offset: 0x000007A0
		public event ListParentsCompletedEventHandler ListParentsCompleted;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000020 RID: 32 RVA: 0x000025D8 File Offset: 0x000007D8
		// (remove) Token: 0x06000021 RID: 33 RVA: 0x00002610 File Offset: 0x00000810
		public event CreateFolderCompletedEventHandler CreateFolderCompleted;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000022 RID: 34 RVA: 0x00002648 File Offset: 0x00000848
		// (remove) Token: 0x06000023 RID: 35 RVA: 0x00002680 File Offset: 0x00000880
		public event SetPropertiesCompletedEventHandler SetPropertiesCompleted;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000024 RID: 36 RVA: 0x000026B8 File Offset: 0x000008B8
		// (remove) Token: 0x06000025 RID: 37 RVA: 0x000026F0 File Offset: 0x000008F0
		public event GetPropertiesCompletedEventHandler GetPropertiesCompleted;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000026 RID: 38 RVA: 0x00002728 File Offset: 0x00000928
		// (remove) Token: 0x06000027 RID: 39 RVA: 0x00002760 File Offset: 0x00000960
		public event SetItemReferencesCompletedEventHandler SetItemReferencesCompleted;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000028 RID: 40 RVA: 0x00002798 File Offset: 0x00000998
		// (remove) Token: 0x06000029 RID: 41 RVA: 0x000027D0 File Offset: 0x000009D0
		public event GetItemReferencesCompletedEventHandler GetItemReferencesCompleted;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600002A RID: 42 RVA: 0x00002808 File Offset: 0x00000A08
		// (remove) Token: 0x0600002B RID: 43 RVA: 0x00002840 File Offset: 0x00000A40
		public event ListItemTypesCompletedEventHandler ListItemTypesCompleted;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600002C RID: 44 RVA: 0x00002878 File Offset: 0x00000A78
		// (remove) Token: 0x0600002D RID: 45 RVA: 0x000028B0 File Offset: 0x00000AB0
		public event SetSubscriptionPropertiesCompletedEventHandler SetSubscriptionPropertiesCompleted;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600002E RID: 46 RVA: 0x000028E8 File Offset: 0x00000AE8
		// (remove) Token: 0x0600002F RID: 47 RVA: 0x00002920 File Offset: 0x00000B20
		public event GetSubscriptionPropertiesCompletedEventHandler GetSubscriptionPropertiesCompleted;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000030 RID: 48 RVA: 0x00002958 File Offset: 0x00000B58
		// (remove) Token: 0x06000031 RID: 49 RVA: 0x00002990 File Offset: 0x00000B90
		public event SetDataDrivenSubscriptionPropertiesCompletedEventHandler SetDataDrivenSubscriptionPropertiesCompleted;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000032 RID: 50 RVA: 0x000029C8 File Offset: 0x00000BC8
		// (remove) Token: 0x06000033 RID: 51 RVA: 0x00002A00 File Offset: 0x00000C00
		public event GetDataDrivenSubscriptionPropertiesCompletedEventHandler GetDataDrivenSubscriptionPropertiesCompleted;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000034 RID: 52 RVA: 0x00002A38 File Offset: 0x00000C38
		// (remove) Token: 0x06000035 RID: 53 RVA: 0x00002A70 File Offset: 0x00000C70
		public event DisableSubscriptionCompletedEventHandler DisableSubscriptionCompleted;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000036 RID: 54 RVA: 0x00002AA8 File Offset: 0x00000CA8
		// (remove) Token: 0x06000037 RID: 55 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public event EnableSubscriptionCompletedEventHandler EnableSubscriptionCompleted;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000038 RID: 56 RVA: 0x00002B18 File Offset: 0x00000D18
		// (remove) Token: 0x06000039 RID: 57 RVA: 0x00002B50 File Offset: 0x00000D50
		public event DeleteSubscriptionCompletedEventHandler DeleteSubscriptionCompleted;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600003A RID: 58 RVA: 0x00002B88 File Offset: 0x00000D88
		// (remove) Token: 0x0600003B RID: 59 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public event CreateSubscriptionCompletedEventHandler CreateSubscriptionCompleted;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600003C RID: 60 RVA: 0x00002BF8 File Offset: 0x00000DF8
		// (remove) Token: 0x0600003D RID: 61 RVA: 0x00002C30 File Offset: 0x00000E30
		public event CreateDataDrivenSubscriptionCompletedEventHandler CreateDataDrivenSubscriptionCompleted;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x0600003E RID: 62 RVA: 0x00002C68 File Offset: 0x00000E68
		// (remove) Token: 0x0600003F RID: 63 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public event GetExtensionSettingsCompletedEventHandler GetExtensionSettingsCompleted;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000040 RID: 64 RVA: 0x00002CD8 File Offset: 0x00000ED8
		// (remove) Token: 0x06000041 RID: 65 RVA: 0x00002D10 File Offset: 0x00000F10
		public event ValidateExtensionSettingsCompletedEventHandler ValidateExtensionSettingsCompleted;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000042 RID: 66 RVA: 0x00002D48 File Offset: 0x00000F48
		// (remove) Token: 0x06000043 RID: 67 RVA: 0x00002D80 File Offset: 0x00000F80
		public event ListSubscriptionsCompletedEventHandler ListSubscriptionsCompleted;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000044 RID: 68 RVA: 0x00002DB8 File Offset: 0x00000FB8
		// (remove) Token: 0x06000045 RID: 69 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public event ListMySubscriptionsCompletedEventHandler ListMySubscriptionsCompleted;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000046 RID: 70 RVA: 0x00002E28 File Offset: 0x00001028
		// (remove) Token: 0x06000047 RID: 71 RVA: 0x00002E60 File Offset: 0x00001060
		public event ListSubscriptionsUsingDataSourceCompletedEventHandler ListSubscriptionsUsingDataSourceCompleted;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000048 RID: 72 RVA: 0x00002E98 File Offset: 0x00001098
		// (remove) Token: 0x06000049 RID: 73 RVA: 0x00002ED0 File Offset: 0x000010D0
		public event ChangeSubscriptionOwnerCompletedEventHandler ChangeSubscriptionOwnerCompleted;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600004A RID: 74 RVA: 0x00002F08 File Offset: 0x00001108
		// (remove) Token: 0x0600004B RID: 75 RVA: 0x00002F40 File Offset: 0x00001140
		public event CreateDataSourceCompletedEventHandler CreateDataSourceCompleted;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600004C RID: 76 RVA: 0x00002F78 File Offset: 0x00001178
		// (remove) Token: 0x0600004D RID: 77 RVA: 0x00002FB0 File Offset: 0x000011B0
		public event PrepareQueryCompletedEventHandler PrepareQueryCompleted;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600004E RID: 78 RVA: 0x00002FE8 File Offset: 0x000011E8
		// (remove) Token: 0x0600004F RID: 79 RVA: 0x00003020 File Offset: 0x00001220
		public event EnableDataSourceCompletedEventHandler EnableDataSourceCompleted;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000050 RID: 80 RVA: 0x00003058 File Offset: 0x00001258
		// (remove) Token: 0x06000051 RID: 81 RVA: 0x00003090 File Offset: 0x00001290
		public event DisableDataSourceCompletedEventHandler DisableDataSourceCompleted;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000052 RID: 82 RVA: 0x000030C8 File Offset: 0x000012C8
		// (remove) Token: 0x06000053 RID: 83 RVA: 0x00003100 File Offset: 0x00001300
		public event SetDataSourceContentsCompletedEventHandler SetDataSourceContentsCompleted;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000054 RID: 84 RVA: 0x00003138 File Offset: 0x00001338
		// (remove) Token: 0x06000055 RID: 85 RVA: 0x00003170 File Offset: 0x00001370
		public event GetDataSourceContentsCompletedEventHandler GetDataSourceContentsCompleted;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000056 RID: 86 RVA: 0x000031A8 File Offset: 0x000013A8
		// (remove) Token: 0x06000057 RID: 87 RVA: 0x000031E0 File Offset: 0x000013E0
		public event ListDatabaseCredentialRetrievalOptionsCompletedEventHandler ListDatabaseCredentialRetrievalOptionsCompleted;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000058 RID: 88 RVA: 0x00003218 File Offset: 0x00001418
		// (remove) Token: 0x06000059 RID: 89 RVA: 0x00003250 File Offset: 0x00001450
		public event SetItemDataSourcesCompletedEventHandler SetItemDataSourcesCompleted;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600005A RID: 90 RVA: 0x00003288 File Offset: 0x00001488
		// (remove) Token: 0x0600005B RID: 91 RVA: 0x000032C0 File Offset: 0x000014C0
		public event GetItemDataSourcesCompletedEventHandler GetItemDataSourcesCompleted;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600005C RID: 92 RVA: 0x000032F8 File Offset: 0x000014F8
		// (remove) Token: 0x0600005D RID: 93 RVA: 0x00003330 File Offset: 0x00001530
		public event TestConnectForDataSourceDefinitionCompletedEventHandler TestConnectForDataSourceDefinitionCompleted;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600005E RID: 94 RVA: 0x00003368 File Offset: 0x00001568
		// (remove) Token: 0x0600005F RID: 95 RVA: 0x000033A0 File Offset: 0x000015A0
		public event TestConnectForItemDataSourceCompletedEventHandler TestConnectForItemDataSourceCompleted;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000060 RID: 96 RVA: 0x000033D8 File Offset: 0x000015D8
		// (remove) Token: 0x06000061 RID: 97 RVA: 0x00003410 File Offset: 0x00001610
		public event CreateRoleCompletedEventHandler CreateRoleCompleted;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000062 RID: 98 RVA: 0x00003448 File Offset: 0x00001648
		// (remove) Token: 0x06000063 RID: 99 RVA: 0x00003480 File Offset: 0x00001680
		public event SetRolePropertiesCompletedEventHandler SetRolePropertiesCompleted;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000064 RID: 100 RVA: 0x000034B8 File Offset: 0x000016B8
		// (remove) Token: 0x06000065 RID: 101 RVA: 0x000034F0 File Offset: 0x000016F0
		public event GetRolePropertiesCompletedEventHandler GetRolePropertiesCompleted;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000066 RID: 102 RVA: 0x00003528 File Offset: 0x00001728
		// (remove) Token: 0x06000067 RID: 103 RVA: 0x00003560 File Offset: 0x00001760
		public event DeleteRoleCompletedEventHandler DeleteRoleCompleted;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000068 RID: 104 RVA: 0x00003598 File Offset: 0x00001798
		// (remove) Token: 0x06000069 RID: 105 RVA: 0x000035D0 File Offset: 0x000017D0
		public event ListRolesCompletedEventHandler ListRolesCompleted;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x0600006A RID: 106 RVA: 0x00003608 File Offset: 0x00001808
		// (remove) Token: 0x0600006B RID: 107 RVA: 0x00003640 File Offset: 0x00001840
		public event ListTasksCompletedEventHandler ListTasksCompleted;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x0600006C RID: 108 RVA: 0x00003678 File Offset: 0x00001878
		// (remove) Token: 0x0600006D RID: 109 RVA: 0x000036B0 File Offset: 0x000018B0
		public event SetPoliciesCompletedEventHandler SetPoliciesCompleted;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x0600006E RID: 110 RVA: 0x000036E8 File Offset: 0x000018E8
		// (remove) Token: 0x0600006F RID: 111 RVA: 0x00003720 File Offset: 0x00001920
		public event GetPoliciesCompletedEventHandler GetPoliciesCompleted;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06000070 RID: 112 RVA: 0x00003758 File Offset: 0x00001958
		// (remove) Token: 0x06000071 RID: 113 RVA: 0x00003790 File Offset: 0x00001990
		public event GetItemDataSourcePromptsCompletedEventHandler GetItemDataSourcePromptsCompleted;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06000072 RID: 114 RVA: 0x000037C8 File Offset: 0x000019C8
		// (remove) Token: 0x06000073 RID: 115 RVA: 0x00003800 File Offset: 0x00001A00
		public event GenerateModelCompletedEventHandler GenerateModelCompleted;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06000074 RID: 116 RVA: 0x00003838 File Offset: 0x00001A38
		// (remove) Token: 0x06000075 RID: 117 RVA: 0x00003870 File Offset: 0x00001A70
		public event GetModelItemPermissionsCompletedEventHandler GetModelItemPermissionsCompleted;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000076 RID: 118 RVA: 0x000038A8 File Offset: 0x00001AA8
		// (remove) Token: 0x06000077 RID: 119 RVA: 0x000038E0 File Offset: 0x00001AE0
		public event SetModelItemPoliciesCompletedEventHandler SetModelItemPoliciesCompleted;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000078 RID: 120 RVA: 0x00003918 File Offset: 0x00001B18
		// (remove) Token: 0x06000079 RID: 121 RVA: 0x00003950 File Offset: 0x00001B50
		public event GetModelItemPoliciesCompletedEventHandler GetModelItemPoliciesCompleted;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x0600007A RID: 122 RVA: 0x00003988 File Offset: 0x00001B88
		// (remove) Token: 0x0600007B RID: 123 RVA: 0x000039C0 File Offset: 0x00001BC0
		public event GetUserModelCompletedEventHandler GetUserModelCompleted;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x0600007C RID: 124 RVA: 0x000039F8 File Offset: 0x00001BF8
		// (remove) Token: 0x0600007D RID: 125 RVA: 0x00003A30 File Offset: 0x00001C30
		public event InheritModelItemParentSecurityCompletedEventHandler InheritModelItemParentSecurityCompleted;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x0600007E RID: 126 RVA: 0x00003A68 File Offset: 0x00001C68
		// (remove) Token: 0x0600007F RID: 127 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public event SetModelDrillthroughReportsCompletedEventHandler SetModelDrillthroughReportsCompleted;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000080 RID: 128 RVA: 0x00003AD8 File Offset: 0x00001CD8
		// (remove) Token: 0x06000081 RID: 129 RVA: 0x00003B10 File Offset: 0x00001D10
		public event ListModelDrillthroughReportsCompletedEventHandler ListModelDrillthroughReportsCompleted;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000082 RID: 130 RVA: 0x00003B48 File Offset: 0x00001D48
		// (remove) Token: 0x06000083 RID: 131 RVA: 0x00003B80 File Offset: 0x00001D80
		public event ListModelItemChildrenCompletedEventHandler ListModelItemChildrenCompleted;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000084 RID: 132 RVA: 0x00003BB8 File Offset: 0x00001DB8
		// (remove) Token: 0x06000085 RID: 133 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public event ListModelItemTypesCompletedEventHandler ListModelItemTypesCompleted;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000086 RID: 134 RVA: 0x00003C28 File Offset: 0x00001E28
		// (remove) Token: 0x06000087 RID: 135 RVA: 0x00003C60 File Offset: 0x00001E60
		public event ListModelPerspectivesCompletedEventHandler ListModelPerspectivesCompleted;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06000088 RID: 136 RVA: 0x00003C98 File Offset: 0x00001E98
		// (remove) Token: 0x06000089 RID: 137 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public event RegenerateModelCompletedEventHandler RegenerateModelCompleted;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x0600008A RID: 138 RVA: 0x00003D08 File Offset: 0x00001F08
		// (remove) Token: 0x0600008B RID: 139 RVA: 0x00003D40 File Offset: 0x00001F40
		public event RemoveAllModelItemPoliciesCompletedEventHandler RemoveAllModelItemPoliciesCompleted;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x0600008C RID: 140 RVA: 0x00003D78 File Offset: 0x00001F78
		// (remove) Token: 0x0600008D RID: 141 RVA: 0x00003DB0 File Offset: 0x00001FB0
		public event CreateScheduleCompletedEventHandler CreateScheduleCompleted;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x0600008E RID: 142 RVA: 0x00003DE8 File Offset: 0x00001FE8
		// (remove) Token: 0x0600008F RID: 143 RVA: 0x00003E20 File Offset: 0x00002020
		public event DeleteScheduleCompletedEventHandler DeleteScheduleCompleted;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06000090 RID: 144 RVA: 0x00003E58 File Offset: 0x00002058
		// (remove) Token: 0x06000091 RID: 145 RVA: 0x00003E90 File Offset: 0x00002090
		public event ListSchedulesCompletedEventHandler ListSchedulesCompleted;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06000092 RID: 146 RVA: 0x00003EC8 File Offset: 0x000020C8
		// (remove) Token: 0x06000093 RID: 147 RVA: 0x00003F00 File Offset: 0x00002100
		public event GetSchedulePropertiesCompletedEventHandler GetSchedulePropertiesCompleted;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06000094 RID: 148 RVA: 0x00003F38 File Offset: 0x00002138
		// (remove) Token: 0x06000095 RID: 149 RVA: 0x00003F70 File Offset: 0x00002170
		public event ListScheduleStatesCompletedEventHandler ListScheduleStatesCompleted;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06000096 RID: 150 RVA: 0x00003FA8 File Offset: 0x000021A8
		// (remove) Token: 0x06000097 RID: 151 RVA: 0x00003FE0 File Offset: 0x000021E0
		public event PauseScheduleCompletedEventHandler PauseScheduleCompleted;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06000098 RID: 152 RVA: 0x00004018 File Offset: 0x00002218
		// (remove) Token: 0x06000099 RID: 153 RVA: 0x00004050 File Offset: 0x00002250
		public event ResumeScheduleCompletedEventHandler ResumeScheduleCompleted;

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x0600009A RID: 154 RVA: 0x00004088 File Offset: 0x00002288
		// (remove) Token: 0x0600009B RID: 155 RVA: 0x000040C0 File Offset: 0x000022C0
		public event SetSchedulePropertiesCompletedEventHandler SetSchedulePropertiesCompleted;

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x0600009C RID: 156 RVA: 0x000040F8 File Offset: 0x000022F8
		// (remove) Token: 0x0600009D RID: 157 RVA: 0x00004130 File Offset: 0x00002330
		public event ListScheduledItemsCompletedEventHandler ListScheduledItemsCompleted;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x0600009E RID: 158 RVA: 0x00004168 File Offset: 0x00002368
		// (remove) Token: 0x0600009F RID: 159 RVA: 0x000041A0 File Offset: 0x000023A0
		public event SetItemParametersCompletedEventHandler SetItemParametersCompleted;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060000A0 RID: 160 RVA: 0x000041D8 File Offset: 0x000023D8
		// (remove) Token: 0x060000A1 RID: 161 RVA: 0x00004210 File Offset: 0x00002410
		public event GetItemParametersCompletedEventHandler GetItemParametersCompleted;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x060000A2 RID: 162 RVA: 0x00004248 File Offset: 0x00002448
		// (remove) Token: 0x060000A3 RID: 163 RVA: 0x00004280 File Offset: 0x00002480
		public event ListParameterTypesCompletedEventHandler ListParameterTypesCompleted;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x060000A4 RID: 164 RVA: 0x000042B8 File Offset: 0x000024B8
		// (remove) Token: 0x060000A5 RID: 165 RVA: 0x000042F0 File Offset: 0x000024F0
		public event ListParameterStatesCompletedEventHandler ListParameterStatesCompleted;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x060000A6 RID: 166 RVA: 0x00004328 File Offset: 0x00002528
		// (remove) Token: 0x060000A7 RID: 167 RVA: 0x00004360 File Offset: 0x00002560
		public event CreateReportEditSessionCompletedEventHandler CreateReportEditSessionCompleted;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x060000A8 RID: 168 RVA: 0x00004398 File Offset: 0x00002598
		// (remove) Token: 0x060000A9 RID: 169 RVA: 0x000043D0 File Offset: 0x000025D0
		public event CreateLinkedItemCompletedEventHandler CreateLinkedItemCompleted;

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x060000AA RID: 170 RVA: 0x00004408 File Offset: 0x00002608
		// (remove) Token: 0x060000AB RID: 171 RVA: 0x00004440 File Offset: 0x00002640
		public event SetItemLinkCompletedEventHandler SetItemLinkCompleted;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x060000AC RID: 172 RVA: 0x00004478 File Offset: 0x00002678
		// (remove) Token: 0x060000AD RID: 173 RVA: 0x000044B0 File Offset: 0x000026B0
		public event GetItemLinkCompletedEventHandler GetItemLinkCompleted;

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x060000AE RID: 174 RVA: 0x000044E8 File Offset: 0x000026E8
		// (remove) Token: 0x060000AF RID: 175 RVA: 0x00004520 File Offset: 0x00002720
		public event ListExecutionSettingsCompletedEventHandler ListExecutionSettingsCompleted;

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x060000B0 RID: 176 RVA: 0x00004558 File Offset: 0x00002758
		// (remove) Token: 0x060000B1 RID: 177 RVA: 0x00004590 File Offset: 0x00002790
		public event SetExecutionOptionsCompletedEventHandler SetExecutionOptionsCompleted;

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x060000B2 RID: 178 RVA: 0x000045C8 File Offset: 0x000027C8
		// (remove) Token: 0x060000B3 RID: 179 RVA: 0x00004600 File Offset: 0x00002800
		public event GetExecutionOptionsCompletedEventHandler GetExecutionOptionsCompleted;

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x060000B4 RID: 180 RVA: 0x00004638 File Offset: 0x00002838
		// (remove) Token: 0x060000B5 RID: 181 RVA: 0x00004670 File Offset: 0x00002870
		public event UpdateItemExecutionSnapshotCompletedEventHandler UpdateItemExecutionSnapshotCompleted;

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060000B6 RID: 182 RVA: 0x000046A8 File Offset: 0x000028A8
		// (remove) Token: 0x060000B7 RID: 183 RVA: 0x000046E0 File Offset: 0x000028E0
		public event SetCacheOptionsCompletedEventHandler SetCacheOptionsCompleted;

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060000B8 RID: 184 RVA: 0x00004718 File Offset: 0x00002918
		// (remove) Token: 0x060000B9 RID: 185 RVA: 0x00004750 File Offset: 0x00002950
		public event GetCacheOptionsCompletedEventHandler GetCacheOptionsCompleted;

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x060000BA RID: 186 RVA: 0x00004788 File Offset: 0x00002988
		// (remove) Token: 0x060000BB RID: 187 RVA: 0x000047C0 File Offset: 0x000029C0
		public event FlushCacheCompletedEventHandler FlushCacheCompleted;

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x060000BC RID: 188 RVA: 0x000047F8 File Offset: 0x000029F8
		// (remove) Token: 0x060000BD RID: 189 RVA: 0x00004830 File Offset: 0x00002A30
		public event CreateItemHistorySnapshotCompletedEventHandler CreateItemHistorySnapshotCompleted;

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060000BE RID: 190 RVA: 0x00004868 File Offset: 0x00002A68
		// (remove) Token: 0x060000BF RID: 191 RVA: 0x000048A0 File Offset: 0x00002AA0
		public event DeleteItemHistorySnapshotCompletedEventHandler DeleteItemHistorySnapshotCompleted;

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x060000C0 RID: 192 RVA: 0x000048D8 File Offset: 0x00002AD8
		// (remove) Token: 0x060000C1 RID: 193 RVA: 0x00004910 File Offset: 0x00002B10
		public event SetItemHistoryLimitCompletedEventHandler SetItemHistoryLimitCompleted;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x060000C2 RID: 194 RVA: 0x00004948 File Offset: 0x00002B48
		// (remove) Token: 0x060000C3 RID: 195 RVA: 0x00004980 File Offset: 0x00002B80
		public event GetItemHistoryLimitCompletedEventHandler GetItemHistoryLimitCompleted;

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x060000C4 RID: 196 RVA: 0x000049B8 File Offset: 0x00002BB8
		// (remove) Token: 0x060000C5 RID: 197 RVA: 0x000049F0 File Offset: 0x00002BF0
		public event SetItemHistoryOptionsCompletedEventHandler SetItemHistoryOptionsCompleted;

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x060000C6 RID: 198 RVA: 0x00004A28 File Offset: 0x00002C28
		// (remove) Token: 0x060000C7 RID: 199 RVA: 0x00004A60 File Offset: 0x00002C60
		public event GetItemHistoryOptionsCompletedEventHandler GetItemHistoryOptionsCompleted;

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x060000C8 RID: 200 RVA: 0x00004A98 File Offset: 0x00002C98
		// (remove) Token: 0x060000C9 RID: 201 RVA: 0x00004AD0 File Offset: 0x00002CD0
		public event GetReportServerConfigInfoCompletedEventHandler GetReportServerConfigInfoCompleted;

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x060000CA RID: 202 RVA: 0x00004B08 File Offset: 0x00002D08
		// (remove) Token: 0x060000CB RID: 203 RVA: 0x00004B40 File Offset: 0x00002D40
		public event IsSSLRequiredCompletedEventHandler IsSSLRequiredCompleted;

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x060000CC RID: 204 RVA: 0x00004B78 File Offset: 0x00002D78
		// (remove) Token: 0x060000CD RID: 205 RVA: 0x00004BB0 File Offset: 0x00002DB0
		public event SetSystemPropertiesCompletedEventHandler SetSystemPropertiesCompleted;

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x060000CE RID: 206 RVA: 0x00004BE8 File Offset: 0x00002DE8
		// (remove) Token: 0x060000CF RID: 207 RVA: 0x00004C20 File Offset: 0x00002E20
		public event GetSystemPropertiesCompletedEventHandler GetSystemPropertiesCompleted;

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x060000D0 RID: 208 RVA: 0x00004C58 File Offset: 0x00002E58
		// (remove) Token: 0x060000D1 RID: 209 RVA: 0x00004C90 File Offset: 0x00002E90
		public event SetUserSettingsCompletedEventHandler SetUserSettingsCompleted;

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x060000D2 RID: 210 RVA: 0x00004CC8 File Offset: 0x00002EC8
		// (remove) Token: 0x060000D3 RID: 211 RVA: 0x00004D00 File Offset: 0x00002F00
		public event GetUserSettingsCompletedEventHandler GetUserSettingsCompleted;

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x060000D4 RID: 212 RVA: 0x00004D38 File Offset: 0x00002F38
		// (remove) Token: 0x060000D5 RID: 213 RVA: 0x00004D70 File Offset: 0x00002F70
		public event SetSystemPoliciesCompletedEventHandler SetSystemPoliciesCompleted;

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x060000D6 RID: 214 RVA: 0x00004DA8 File Offset: 0x00002FA8
		// (remove) Token: 0x060000D7 RID: 215 RVA: 0x00004DE0 File Offset: 0x00002FE0
		public event GetSystemPoliciesCompletedEventHandler GetSystemPoliciesCompleted;

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x060000D8 RID: 216 RVA: 0x00004E18 File Offset: 0x00003018
		// (remove) Token: 0x060000D9 RID: 217 RVA: 0x00004E50 File Offset: 0x00003050
		public event ListExtensionsCompletedEventHandler ListExtensionsCompleted;

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x060000DA RID: 218 RVA: 0x00004E88 File Offset: 0x00003088
		// (remove) Token: 0x060000DB RID: 219 RVA: 0x00004EC0 File Offset: 0x000030C0
		public event ListExtensionTypesCompletedEventHandler ListExtensionTypesCompleted;

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x060000DC RID: 220 RVA: 0x00004EF8 File Offset: 0x000030F8
		// (remove) Token: 0x060000DD RID: 221 RVA: 0x00004F30 File Offset: 0x00003130
		public event ListEventsCompletedEventHandler ListEventsCompleted;

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x060000DE RID: 222 RVA: 0x00004F68 File Offset: 0x00003168
		// (remove) Token: 0x060000DF RID: 223 RVA: 0x00004FA0 File Offset: 0x000031A0
		public event FireEventCompletedEventHandler FireEventCompleted;

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x060000E0 RID: 224 RVA: 0x00004FD8 File Offset: 0x000031D8
		// (remove) Token: 0x060000E1 RID: 225 RVA: 0x00005010 File Offset: 0x00003210
		public event ListJobsCompletedEventHandler ListJobsCompleted;

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x060000E2 RID: 226 RVA: 0x00005048 File Offset: 0x00003248
		// (remove) Token: 0x060000E3 RID: 227 RVA: 0x00005080 File Offset: 0x00003280
		public event ListJobTypesCompletedEventHandler ListJobTypesCompleted;

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x060000E4 RID: 228 RVA: 0x000050B8 File Offset: 0x000032B8
		// (remove) Token: 0x060000E5 RID: 229 RVA: 0x000050F0 File Offset: 0x000032F0
		public event ListJobActionsCompletedEventHandler ListJobActionsCompleted;

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x060000E6 RID: 230 RVA: 0x00005128 File Offset: 0x00003328
		// (remove) Token: 0x060000E7 RID: 231 RVA: 0x00005160 File Offset: 0x00003360
		public event ListJobStatesCompletedEventHandler ListJobStatesCompleted;

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x060000E8 RID: 232 RVA: 0x00005198 File Offset: 0x00003398
		// (remove) Token: 0x060000E9 RID: 233 RVA: 0x000051D0 File Offset: 0x000033D0
		public event CancelJobCompletedEventHandler CancelJobCompleted;

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x060000EA RID: 234 RVA: 0x00005208 File Offset: 0x00003408
		// (remove) Token: 0x060000EB RID: 235 RVA: 0x00005240 File Offset: 0x00003440
		public event CreateCacheRefreshPlanCompletedEventHandler CreateCacheRefreshPlanCompleted;

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x060000EC RID: 236 RVA: 0x00005278 File Offset: 0x00003478
		// (remove) Token: 0x060000ED RID: 237 RVA: 0x000052B0 File Offset: 0x000034B0
		public event SetCacheRefreshPlanPropertiesCompletedEventHandler SetCacheRefreshPlanPropertiesCompleted;

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x060000EE RID: 238 RVA: 0x000052E8 File Offset: 0x000034E8
		// (remove) Token: 0x060000EF RID: 239 RVA: 0x00005320 File Offset: 0x00003520
		public event GetCacheRefreshPlanPropertiesCompletedEventHandler GetCacheRefreshPlanPropertiesCompleted;

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x060000F0 RID: 240 RVA: 0x00005358 File Offset: 0x00003558
		// (remove) Token: 0x060000F1 RID: 241 RVA: 0x00005390 File Offset: 0x00003590
		public event DeleteCacheRefreshPlanCompletedEventHandler DeleteCacheRefreshPlanCompleted;

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x060000F2 RID: 242 RVA: 0x000053C8 File Offset: 0x000035C8
		// (remove) Token: 0x060000F3 RID: 243 RVA: 0x00005400 File Offset: 0x00003600
		public event ListCacheRefreshPlansCompletedEventHandler ListCacheRefreshPlansCompleted;

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x060000F4 RID: 244 RVA: 0x00005438 File Offset: 0x00003638
		// (remove) Token: 0x060000F5 RID: 245 RVA: 0x00005470 File Offset: 0x00003670
		public event LogonUserCompletedEventHandler LogonUserCompleted;

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x060000F6 RID: 246 RVA: 0x000054A8 File Offset: 0x000036A8
		// (remove) Token: 0x060000F7 RID: 247 RVA: 0x000054E0 File Offset: 0x000036E0
		public event LogoffCompletedEventHandler LogoffCompleted;

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x060000F8 RID: 248 RVA: 0x00005518 File Offset: 0x00003718
		// (remove) Token: 0x060000F9 RID: 249 RVA: 0x00005550 File Offset: 0x00003750
		public event GetPermissionsCompletedEventHandler GetPermissionsCompleted;

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x060000FA RID: 250 RVA: 0x00005588 File Offset: 0x00003788
		// (remove) Token: 0x060000FB RID: 251 RVA: 0x000055C0 File Offset: 0x000037C0
		public event GetSystemPermissionsCompletedEventHandler GetSystemPermissionsCompleted;

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x060000FC RID: 252 RVA: 0x000055F8 File Offset: 0x000037F8
		// (remove) Token: 0x060000FD RID: 253 RVA: 0x00005630 File Offset: 0x00003830
		public event ListSecurityScopesCompletedEventHandler ListSecurityScopesCompleted;

		// Token: 0x060000FE RID: 254 RVA: 0x00005668 File Offset: 0x00003868
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/CreateCatalogItem", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ItemInfo")]
		public CatalogItem CreateCatalogItem(string ItemType, string Name, string Parent, bool Overwrite, [XmlElement(DataType = "base64Binary")] byte[] Definition, Property[] Properties, out Warning[] Warnings)
		{
			object[] array = base.Invoke("CreateCatalogItem", new object[] { ItemType, Name, Parent, Overwrite, Definition, Properties });
			Warnings = (Warning[])array[1];
			return (CatalogItem)array[0];
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000056BA File Offset: 0x000038BA
		public IAsyncResult BeginCreateCatalogItem(string ItemType, string Name, string Parent, bool Overwrite, byte[] Definition, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateCatalogItem", new object[] { ItemType, Name, Parent, Overwrite, Definition, Properties }, callback, asyncState);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000056F4 File Offset: 0x000038F4
		public CatalogItem EndCreateCatalogItem(IAsyncResult asyncResult, out Warning[] Warnings)
		{
			object[] array = base.EndInvoke(asyncResult);
			Warnings = (Warning[])array[1];
			return (CatalogItem)array[0];
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000571B File Offset: 0x0000391B
		public void CreateCatalogItemAsync(string ItemType, string Name, string Parent, bool Overwrite, byte[] Definition, Property[] Properties)
		{
			this.CreateCatalogItemAsync(ItemType, Name, Parent, Overwrite, Definition, Properties, null);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005730 File Offset: 0x00003930
		public void CreateCatalogItemAsync(string ItemType, string Name, string Parent, bool Overwrite, byte[] Definition, Property[] Properties, object userState)
		{
			if (this.CreateCatalogItemOperationCompleted == null)
			{
				this.CreateCatalogItemOperationCompleted = new SendOrPostCallback(this.OnCreateCatalogItemOperationCompleted);
			}
			base.InvokeAsync("CreateCatalogItem", new object[] { ItemType, Name, Parent, Overwrite, Definition, Properties }, this.CreateCatalogItemOperationCompleted, userState);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005790 File Offset: 0x00003990
		private void OnCreateCatalogItemOperationCompleted(object arg)
		{
			if (this.CreateCatalogItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateCatalogItemCompleted(this, new CreateCatalogItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000057D5 File Offset: 0x000039D5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetItemDefinition", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Warnings")]
		public Warning[] SetItemDefinition(string ItemPath, [XmlElement(DataType = "base64Binary")] byte[] Definition, Property[] Properties)
		{
			return (Warning[])base.Invoke("SetItemDefinition", new object[] { ItemPath, Definition, Properties })[0];
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000057FB File Offset: 0x000039FB
		public IAsyncResult BeginSetItemDefinition(string ItemPath, byte[] Definition, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetItemDefinition", new object[] { ItemPath, Definition, Properties }, callback, asyncState);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000581E File Offset: 0x00003A1E
		public Warning[] EndSetItemDefinition(IAsyncResult asyncResult)
		{
			return (Warning[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000582E File Offset: 0x00003A2E
		public void SetItemDefinitionAsync(string ItemPath, byte[] Definition, Property[] Properties)
		{
			this.SetItemDefinitionAsync(ItemPath, Definition, Properties, null);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000583C File Offset: 0x00003A3C
		public void SetItemDefinitionAsync(string ItemPath, byte[] Definition, Property[] Properties, object userState)
		{
			if (this.SetItemDefinitionOperationCompleted == null)
			{
				this.SetItemDefinitionOperationCompleted = new SendOrPostCallback(this.OnSetItemDefinitionOperationCompleted);
			}
			base.InvokeAsync("SetItemDefinition", new object[] { ItemPath, Definition, Properties }, this.SetItemDefinitionOperationCompleted, userState);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005888 File Offset: 0x00003A88
		private void OnSetItemDefinitionOperationCompleted(object arg)
		{
			if (this.SetItemDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetItemDefinitionCompleted(this, new SetItemDefinitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000058CD File Offset: 0x00003ACD
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetItemDefinition", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Definition", DataType = "base64Binary")]
		public byte[] GetItemDefinition(string ItemPath)
		{
			return (byte[])base.Invoke("GetItemDefinition", new object[] { ItemPath })[0];
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000058EB File Offset: 0x00003AEB
		public IAsyncResult BeginGetItemDefinition(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemDefinition", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005904 File Offset: 0x00003B04
		public byte[] EndGetItemDefinition(IAsyncResult asyncResult)
		{
			return (byte[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005914 File Offset: 0x00003B14
		public void GetItemDefinitionAsync(string ItemPath)
		{
			this.GetItemDefinitionAsync(ItemPath, null);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000591E File Offset: 0x00003B1E
		public void GetItemDefinitionAsync(string ItemPath, object userState)
		{
			if (this.GetItemDefinitionOperationCompleted == null)
			{
				this.GetItemDefinitionOperationCompleted = new SendOrPostCallback(this.OnGetItemDefinitionOperationCompleted);
			}
			base.InvokeAsync("GetItemDefinition", new object[] { ItemPath }, this.GetItemDefinitionOperationCompleted, userState);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005958 File Offset: 0x00003B58
		private void OnGetItemDefinitionOperationCompleted(object arg)
		{
			if (this.GetItemDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemDefinitionCompleted(this, new GetItemDefinitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000599D File Offset: 0x00003B9D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetItemType", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Type")]
		public string GetItemType(string ItemPath)
		{
			return (string)base.Invoke("GetItemType", new object[] { ItemPath })[0];
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000059BB File Offset: 0x00003BBB
		public IAsyncResult BeginGetItemType(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemType", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000059D4 File Offset: 0x00003BD4
		public string EndGetItemType(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000059E4 File Offset: 0x00003BE4
		public void GetItemTypeAsync(string ItemPath)
		{
			this.GetItemTypeAsync(ItemPath, null);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000059EE File Offset: 0x00003BEE
		public void GetItemTypeAsync(string ItemPath, object userState)
		{
			if (this.GetItemTypeOperationCompleted == null)
			{
				this.GetItemTypeOperationCompleted = new SendOrPostCallback(this.OnGetItemTypeOperationCompleted);
			}
			base.InvokeAsync("GetItemType", new object[] { ItemPath }, this.GetItemTypeOperationCompleted, userState);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005A28 File Offset: 0x00003C28
		private void OnGetItemTypeOperationCompleted(object arg)
		{
			if (this.GetItemTypeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemTypeCompleted(this, new GetItemTypeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005A6D File Offset: 0x00003C6D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/DeleteItem", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteItem(string ItemPath)
		{
			base.Invoke("DeleteItem", new object[] { ItemPath });
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005A85 File Offset: 0x00003C85
		public IAsyncResult BeginDeleteItem(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteItem", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005A9E File Offset: 0x00003C9E
		public void EndDeleteItem(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005AA8 File Offset: 0x00003CA8
		public void DeleteItemAsync(string ItemPath)
		{
			this.DeleteItemAsync(ItemPath, null);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005AB2 File Offset: 0x00003CB2
		public void DeleteItemAsync(string ItemPath, object userState)
		{
			if (this.DeleteItemOperationCompleted == null)
			{
				this.DeleteItemOperationCompleted = new SendOrPostCallback(this.OnDeleteItemOperationCompleted);
			}
			base.InvokeAsync("DeleteItem", new object[] { ItemPath }, this.DeleteItemOperationCompleted, userState);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005AEC File Offset: 0x00003CEC
		private void OnDeleteItemOperationCompleted(object arg)
		{
			if (this.DeleteItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteItemCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005B2B File Offset: 0x00003D2B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/MoveItem", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void MoveItem(string ItemPath, string Target)
		{
			base.Invoke("MoveItem", new object[] { ItemPath, Target });
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005B47 File Offset: 0x00003D47
		public IAsyncResult BeginMoveItem(string ItemPath, string Target, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("MoveItem", new object[] { ItemPath, Target }, callback, asyncState);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005B65 File Offset: 0x00003D65
		public void EndMoveItem(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005B6F File Offset: 0x00003D6F
		public void MoveItemAsync(string ItemPath, string Target)
		{
			this.MoveItemAsync(ItemPath, Target, null);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005B7A File Offset: 0x00003D7A
		public void MoveItemAsync(string ItemPath, string Target, object userState)
		{
			if (this.MoveItemOperationCompleted == null)
			{
				this.MoveItemOperationCompleted = new SendOrPostCallback(this.OnMoveItemOperationCompleted);
			}
			base.InvokeAsync("MoveItem", new object[] { ItemPath, Target }, this.MoveItemOperationCompleted, userState);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005BB8 File Offset: 0x00003DB8
		private void OnMoveItemOperationCompleted(object arg)
		{
			if (this.MoveItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MoveItemCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005BF7 File Offset: 0x00003DF7
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/InheritParentSecurity", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void InheritParentSecurity(string ItemPath)
		{
			base.Invoke("InheritParentSecurity", new object[] { ItemPath });
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005C0F File Offset: 0x00003E0F
		public IAsyncResult BeginInheritParentSecurity(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("InheritParentSecurity", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005C28 File Offset: 0x00003E28
		public void EndInheritParentSecurity(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005C32 File Offset: 0x00003E32
		public void InheritParentSecurityAsync(string ItemPath)
		{
			this.InheritParentSecurityAsync(ItemPath, null);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005C3C File Offset: 0x00003E3C
		public void InheritParentSecurityAsync(string ItemPath, object userState)
		{
			if (this.InheritParentSecurityOperationCompleted == null)
			{
				this.InheritParentSecurityOperationCompleted = new SendOrPostCallback(this.OnInheritParentSecurityOperationCompleted);
			}
			base.InvokeAsync("InheritParentSecurity", new object[] { ItemPath }, this.InheritParentSecurityOperationCompleted, userState);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005C74 File Offset: 0x00003E74
		private void OnInheritParentSecurityOperationCompleted(object arg)
		{
			if (this.InheritParentSecurityCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.InheritParentSecurityCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005CB3 File Offset: 0x00003EB3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListItemHistory", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ItemHistory")]
		public ItemHistorySnapshot[] ListItemHistory(string ItemPath)
		{
			return (ItemHistorySnapshot[])base.Invoke("ListItemHistory", new object[] { ItemPath })[0];
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005CD1 File Offset: 0x00003ED1
		public IAsyncResult BeginListItemHistory(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListItemHistory", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005CEA File Offset: 0x00003EEA
		public ItemHistorySnapshot[] EndListItemHistory(IAsyncResult asyncResult)
		{
			return (ItemHistorySnapshot[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005CFA File Offset: 0x00003EFA
		public void ListItemHistoryAsync(string ItemPath)
		{
			this.ListItemHistoryAsync(ItemPath, null);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005D04 File Offset: 0x00003F04
		public void ListItemHistoryAsync(string ItemPath, object userState)
		{
			if (this.ListItemHistoryOperationCompleted == null)
			{
				this.ListItemHistoryOperationCompleted = new SendOrPostCallback(this.OnListItemHistoryOperationCompleted);
			}
			base.InvokeAsync("ListItemHistory", new object[] { ItemPath }, this.ListItemHistoryOperationCompleted, userState);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005D3C File Offset: 0x00003F3C
		private void OnListItemHistoryOperationCompleted(object arg)
		{
			if (this.ListItemHistoryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListItemHistoryCompleted(this, new ListItemHistoryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005D81 File Offset: 0x00003F81
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListChildren", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("CatalogItems")]
		public CatalogItem[] ListChildren(string ItemPath, bool Recursive)
		{
			return (CatalogItem[])base.Invoke("ListChildren", new object[] { ItemPath, Recursive })[0];
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public IAsyncResult BeginListChildren(string ItemPath, bool Recursive, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListChildren", new object[] { ItemPath, Recursive }, callback, asyncState);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005DCB File Offset: 0x00003FCB
		public CatalogItem[] EndListChildren(IAsyncResult asyncResult)
		{
			return (CatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005DDB File Offset: 0x00003FDB
		public void ListChildrenAsync(string ItemPath, bool Recursive)
		{
			this.ListChildrenAsync(ItemPath, Recursive, null);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005DE8 File Offset: 0x00003FE8
		public void ListChildrenAsync(string ItemPath, bool Recursive, object userState)
		{
			if (this.ListChildrenOperationCompleted == null)
			{
				this.ListChildrenOperationCompleted = new SendOrPostCallback(this.OnListChildrenOperationCompleted);
			}
			base.InvokeAsync("ListChildren", new object[] { ItemPath, Recursive }, this.ListChildrenOperationCompleted, userState);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005E34 File Offset: 0x00004034
		private void OnListChildrenOperationCompleted(object arg)
		{
			if (this.ListChildrenCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListChildrenCompleted(this, new ListChildrenCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005E79 File Offset: 0x00004079
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListDependentItems", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("CatalogItems")]
		public CatalogItem[] ListDependentItems(string ItemPath)
		{
			return (CatalogItem[])base.Invoke("ListDependentItems", new object[] { ItemPath })[0];
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005E97 File Offset: 0x00004097
		public IAsyncResult BeginListDependentItems(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListDependentItems", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005EB0 File Offset: 0x000040B0
		public CatalogItem[] EndListDependentItems(IAsyncResult asyncResult)
		{
			return (CatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005EC0 File Offset: 0x000040C0
		public void ListDependentItemsAsync(string ItemPath)
		{
			this.ListDependentItemsAsync(ItemPath, null);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005ECA File Offset: 0x000040CA
		public void ListDependentItemsAsync(string ItemPath, object userState)
		{
			if (this.ListDependentItemsOperationCompleted == null)
			{
				this.ListDependentItemsOperationCompleted = new SendOrPostCallback(this.OnListDependentItemsOperationCompleted);
			}
			base.InvokeAsync("ListDependentItems", new object[] { ItemPath }, this.ListDependentItemsOperationCompleted, userState);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005F04 File Offset: 0x00004104
		private void OnListDependentItemsOperationCompleted(object arg)
		{
			if (this.ListDependentItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListDependentItemsCompleted(this, new ListDependentItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005F49 File Offset: 0x00004149
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/FindItems", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Items")]
		public CatalogItem[] FindItems(string Folder, BooleanOperatorEnum BooleanOperator, Property[] SearchOptions, SearchCondition[] SearchConditions)
		{
			return (CatalogItem[])base.Invoke("FindItems", new object[] { Folder, BooleanOperator, SearchOptions, SearchConditions })[0];
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005F79 File Offset: 0x00004179
		public IAsyncResult BeginFindItems(string Folder, BooleanOperatorEnum BooleanOperator, Property[] SearchOptions, SearchCondition[] SearchConditions, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindItems", new object[] { Folder, BooleanOperator, SearchOptions, SearchConditions }, callback, asyncState);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005FA6 File Offset: 0x000041A6
		public CatalogItem[] EndFindItems(IAsyncResult asyncResult)
		{
			return (CatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005FB6 File Offset: 0x000041B6
		public void FindItemsAsync(string Folder, BooleanOperatorEnum BooleanOperator, Property[] SearchOptions, SearchCondition[] SearchConditions)
		{
			this.FindItemsAsync(Folder, BooleanOperator, SearchOptions, SearchConditions, null);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005FC4 File Offset: 0x000041C4
		public void FindItemsAsync(string Folder, BooleanOperatorEnum BooleanOperator, Property[] SearchOptions, SearchCondition[] SearchConditions, object userState)
		{
			if (this.FindItemsOperationCompleted == null)
			{
				this.FindItemsOperationCompleted = new SendOrPostCallback(this.OnFindItemsOperationCompleted);
			}
			base.InvokeAsync("FindItems", new object[] { Folder, BooleanOperator, SearchOptions, SearchConditions }, this.FindItemsOperationCompleted, userState);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000601C File Offset: 0x0000421C
		private void OnFindItemsOperationCompleted(object arg)
		{
			if (this.FindItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindItemsCompleted(this, new FindItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006061 File Offset: 0x00004261
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListParents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public CatalogItem[] ListParents(string ItemPath)
		{
			return (CatalogItem[])base.Invoke("ListParents", new object[] { ItemPath })[0];
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000607F File Offset: 0x0000427F
		public IAsyncResult BeginListParents(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListParents", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006098 File Offset: 0x00004298
		public CatalogItem[] EndListParents(IAsyncResult asyncResult)
		{
			return (CatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000060A8 File Offset: 0x000042A8
		public void ListParentsAsync(string ItemPath)
		{
			this.ListParentsAsync(ItemPath, null);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000060B2 File Offset: 0x000042B2
		public void ListParentsAsync(string ItemPath, object userState)
		{
			if (this.ListParentsOperationCompleted == null)
			{
				this.ListParentsOperationCompleted = new SendOrPostCallback(this.OnListParentsOperationCompleted);
			}
			base.InvokeAsync("ListParents", new object[] { ItemPath }, this.ListParentsOperationCompleted, userState);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000060EC File Offset: 0x000042EC
		private void OnListParentsOperationCompleted(object arg)
		{
			if (this.ListParentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListParentsCompleted(this, new ListParentsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006131 File Offset: 0x00004331
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/CreateFolder", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ItemInfo")]
		public CatalogItem CreateFolder(string Folder, string Parent, Property[] Properties)
		{
			return (CatalogItem)base.Invoke("CreateFolder", new object[] { Folder, Parent, Properties })[0];
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006157 File Offset: 0x00004357
		public IAsyncResult BeginCreateFolder(string Folder, string Parent, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateFolder", new object[] { Folder, Parent, Properties }, callback, asyncState);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000617A File Offset: 0x0000437A
		public CatalogItem EndCreateFolder(IAsyncResult asyncResult)
		{
			return (CatalogItem)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000618A File Offset: 0x0000438A
		public void CreateFolderAsync(string Folder, string Parent, Property[] Properties)
		{
			this.CreateFolderAsync(Folder, Parent, Properties, null);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006198 File Offset: 0x00004398
		public void CreateFolderAsync(string Folder, string Parent, Property[] Properties, object userState)
		{
			if (this.CreateFolderOperationCompleted == null)
			{
				this.CreateFolderOperationCompleted = new SendOrPostCallback(this.OnCreateFolderOperationCompleted);
			}
			base.InvokeAsync("CreateFolder", new object[] { Folder, Parent, Properties }, this.CreateFolderOperationCompleted, userState);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000061E4 File Offset: 0x000043E4
		private void OnCreateFolderOperationCompleted(object arg)
		{
			if (this.CreateFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateFolderCompleted(this, new CreateFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006229 File Offset: 0x00004429
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetProperties(string ItemPath, Property[] Properties)
		{
			base.Invoke("SetProperties", new object[] { ItemPath, Properties });
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006245 File Offset: 0x00004445
		public IAsyncResult BeginSetProperties(string ItemPath, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetProperties", new object[] { ItemPath, Properties }, callback, asyncState);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006263 File Offset: 0x00004463
		public void EndSetProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000626D File Offset: 0x0000446D
		public void SetPropertiesAsync(string ItemPath, Property[] Properties)
		{
			this.SetPropertiesAsync(ItemPath, Properties, null);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006278 File Offset: 0x00004478
		public void SetPropertiesAsync(string ItemPath, Property[] Properties, object userState)
		{
			if (this.SetPropertiesOperationCompleted == null)
			{
				this.SetPropertiesOperationCompleted = new SendOrPostCallback(this.OnSetPropertiesOperationCompleted);
			}
			base.InvokeAsync("SetProperties", new object[] { ItemPath, Properties }, this.SetPropertiesOperationCompleted, userState);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000062B4 File Offset: 0x000044B4
		private void OnSetPropertiesOperationCompleted(object arg)
		{
			if (this.SetPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000062F3 File Offset: 0x000044F3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ItemNamespaceHeaderValue")]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Values")]
		public Property[] GetProperties(string ItemPath, Property[] Properties)
		{
			return (Property[])base.Invoke("GetProperties", new object[] { ItemPath, Properties })[0];
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006315 File Offset: 0x00004515
		public IAsyncResult BeginGetProperties(string ItemPath, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetProperties", new object[] { ItemPath, Properties }, callback, asyncState);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006333 File Offset: 0x00004533
		public Property[] EndGetProperties(IAsyncResult asyncResult)
		{
			return (Property[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006343 File Offset: 0x00004543
		public void GetPropertiesAsync(string ItemPath, Property[] Properties)
		{
			this.GetPropertiesAsync(ItemPath, Properties, null);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000634E File Offset: 0x0000454E
		public void GetPropertiesAsync(string ItemPath, Property[] Properties, object userState)
		{
			if (this.GetPropertiesOperationCompleted == null)
			{
				this.GetPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetProperties", new object[] { ItemPath, Properties }, this.GetPropertiesOperationCompleted, userState);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000638C File Offset: 0x0000458C
		private void OnGetPropertiesOperationCompleted(object arg)
		{
			if (this.GetPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPropertiesCompleted(this, new GetPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000063D1 File Offset: 0x000045D1
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetItemReferences", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetItemReferences(string ItemPath, ItemReference[] ItemReferences)
		{
			base.Invoke("SetItemReferences", new object[] { ItemPath, ItemReferences });
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000063ED File Offset: 0x000045ED
		public IAsyncResult BeginSetItemReferences(string ItemPath, ItemReference[] ItemReferences, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetItemReferences", new object[] { ItemPath, ItemReferences }, callback, asyncState);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000640B File Offset: 0x0000460B
		public void EndSetItemReferences(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006415 File Offset: 0x00004615
		public void SetItemReferencesAsync(string ItemPath, ItemReference[] ItemReferences)
		{
			this.SetItemReferencesAsync(ItemPath, ItemReferences, null);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006420 File Offset: 0x00004620
		public void SetItemReferencesAsync(string ItemPath, ItemReference[] ItemReferences, object userState)
		{
			if (this.SetItemReferencesOperationCompleted == null)
			{
				this.SetItemReferencesOperationCompleted = new SendOrPostCallback(this.OnSetItemReferencesOperationCompleted);
			}
			base.InvokeAsync("SetItemReferences", new object[] { ItemPath, ItemReferences }, this.SetItemReferencesOperationCompleted, userState);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000645C File Offset: 0x0000465C
		private void OnSetItemReferencesOperationCompleted(object arg)
		{
			if (this.SetItemReferencesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetItemReferencesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000649B File Offset: 0x0000469B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetItemReferences", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ItemReferences")]
		public ItemReferenceData[] GetItemReferences(string ItemPath, string ReferenceItemType)
		{
			return (ItemReferenceData[])base.Invoke("GetItemReferences", new object[] { ItemPath, ReferenceItemType })[0];
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000064BD File Offset: 0x000046BD
		public IAsyncResult BeginGetItemReferences(string ItemPath, string ReferenceItemType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemReferences", new object[] { ItemPath, ReferenceItemType }, callback, asyncState);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000064DB File Offset: 0x000046DB
		public ItemReferenceData[] EndGetItemReferences(IAsyncResult asyncResult)
		{
			return (ItemReferenceData[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000064EB File Offset: 0x000046EB
		public void GetItemReferencesAsync(string ItemPath, string ReferenceItemType)
		{
			this.GetItemReferencesAsync(ItemPath, ReferenceItemType, null);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000064F6 File Offset: 0x000046F6
		public void GetItemReferencesAsync(string ItemPath, string ReferenceItemType, object userState)
		{
			if (this.GetItemReferencesOperationCompleted == null)
			{
				this.GetItemReferencesOperationCompleted = new SendOrPostCallback(this.OnGetItemReferencesOperationCompleted);
			}
			base.InvokeAsync("GetItemReferences", new object[] { ItemPath, ReferenceItemType }, this.GetItemReferencesOperationCompleted, userState);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006534 File Offset: 0x00004734
		private void OnGetItemReferencesOperationCompleted(object arg)
		{
			if (this.GetItemReferencesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemReferencesCompleted(this, new GetItemReferencesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006579 File Offset: 0x00004779
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListItemTypes", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListItemTypes()
		{
			return (string[])base.Invoke("ListItemTypes", new object[0])[0];
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006593 File Offset: 0x00004793
		public IAsyncResult BeginListItemTypes(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListItemTypes", new object[0], callback, asyncState);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000065A8 File Offset: 0x000047A8
		public string[] EndListItemTypes(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000065B8 File Offset: 0x000047B8
		public void ListItemTypesAsync()
		{
			this.ListItemTypesAsync(null);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000065C1 File Offset: 0x000047C1
		public void ListItemTypesAsync(object userState)
		{
			if (this.ListItemTypesOperationCompleted == null)
			{
				this.ListItemTypesOperationCompleted = new SendOrPostCallback(this.OnListItemTypesOperationCompleted);
			}
			base.InvokeAsync("ListItemTypes", new object[0], this.ListItemTypesOperationCompleted, userState);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000065F8 File Offset: 0x000047F8
		private void OnListItemTypesOperationCompleted(object arg)
		{
			if (this.ListItemTypesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListItemTypesCompleted(this, new ListItemTypesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000663D File Offset: 0x0000483D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetSubscriptionProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetSubscriptionProperties(string SubscriptionID, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			base.Invoke("SetSubscriptionProperties", new object[] { SubscriptionID, ExtensionSettings, Description, EventType, MatchData, Parameters });
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000666C File Offset: 0x0000486C
		public IAsyncResult BeginSetSubscriptionProperties(string SubscriptionID, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetSubscriptionProperties", new object[] { SubscriptionID, ExtensionSettings, Description, EventType, MatchData, Parameters }, callback, asyncState);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000669E File Offset: 0x0000489E
		public void EndSetSubscriptionProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000066A8 File Offset: 0x000048A8
		public void SetSubscriptionPropertiesAsync(string SubscriptionID, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			this.SetSubscriptionPropertiesAsync(SubscriptionID, ExtensionSettings, Description, EventType, MatchData, Parameters, null);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000066BC File Offset: 0x000048BC
		public void SetSubscriptionPropertiesAsync(string SubscriptionID, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters, object userState)
		{
			if (this.SetSubscriptionPropertiesOperationCompleted == null)
			{
				this.SetSubscriptionPropertiesOperationCompleted = new SendOrPostCallback(this.OnSetSubscriptionPropertiesOperationCompleted);
			}
			base.InvokeAsync("SetSubscriptionProperties", new object[] { SubscriptionID, ExtensionSettings, Description, EventType, MatchData, Parameters }, this.SetSubscriptionPropertiesOperationCompleted, userState);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006718 File Offset: 0x00004918
		private void OnSetSubscriptionPropertiesOperationCompleted(object arg)
		{
			if (this.SetSubscriptionPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetSubscriptionPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006758 File Offset: 0x00004958
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetSubscriptionProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Owner")]
		public string GetSubscriptionProperties(string SubscriptionID, out ExtensionSettings ExtensionSettings, out string Description, out ActiveState Active, out string Status, out string EventType, out string MatchData, out ParameterValue[] Parameters)
		{
			object[] array = base.Invoke("GetSubscriptionProperties", new object[] { SubscriptionID });
			ExtensionSettings = (ExtensionSettings)array[1];
			Description = (string)array[2];
			Active = (ActiveState)array[3];
			Status = (string)array[4];
			EventType = (string)array[5];
			MatchData = (string)array[6];
			Parameters = (ParameterValue[])array[7];
			return (string)array[0];
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000067CE File Offset: 0x000049CE
		public IAsyncResult BeginGetSubscriptionProperties(string SubscriptionID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSubscriptionProperties", new object[] { SubscriptionID }, callback, asyncState);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000067E8 File Offset: 0x000049E8
		public string EndGetSubscriptionProperties(IAsyncResult asyncResult, out ExtensionSettings ExtensionSettings, out string Description, out ActiveState Active, out string Status, out string EventType, out string MatchData, out ParameterValue[] Parameters)
		{
			object[] array = base.EndInvoke(asyncResult);
			ExtensionSettings = (ExtensionSettings)array[1];
			Description = (string)array[2];
			Active = (ActiveState)array[3];
			Status = (string)array[4];
			EventType = (string)array[5];
			MatchData = (string)array[6];
			Parameters = (ParameterValue[])array[7];
			return (string)array[0];
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006850 File Offset: 0x00004A50
		public void GetSubscriptionPropertiesAsync(string SubscriptionID)
		{
			this.GetSubscriptionPropertiesAsync(SubscriptionID, null);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000685A File Offset: 0x00004A5A
		public void GetSubscriptionPropertiesAsync(string SubscriptionID, object userState)
		{
			if (this.GetSubscriptionPropertiesOperationCompleted == null)
			{
				this.GetSubscriptionPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetSubscriptionPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetSubscriptionProperties", new object[] { SubscriptionID }, this.GetSubscriptionPropertiesOperationCompleted, userState);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006894 File Offset: 0x00004A94
		private void OnGetSubscriptionPropertiesOperationCompleted(object arg)
		{
			if (this.GetSubscriptionPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSubscriptionPropertiesCompleted(this, new GetSubscriptionPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000068D9 File Offset: 0x00004AD9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetDataDrivenSubscriptionProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetDataDrivenSubscriptionProperties(string DataDrivenSubscriptionID, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters)
		{
			base.Invoke("SetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters });
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000690D File Offset: 0x00004B0D
		public IAsyncResult BeginSetDataDrivenSubscriptionProperties(string DataDrivenSubscriptionID, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters }, callback, asyncState);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006944 File Offset: 0x00004B44
		public void EndSetDataDrivenSubscriptionProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006950 File Offset: 0x00004B50
		public void SetDataDrivenSubscriptionPropertiesAsync(string DataDrivenSubscriptionID, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters)
		{
			this.SetDataDrivenSubscriptionPropertiesAsync(DataDrivenSubscriptionID, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters, null);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006970 File Offset: 0x00004B70
		public void SetDataDrivenSubscriptionPropertiesAsync(string DataDrivenSubscriptionID, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, object userState)
		{
			if (this.SetDataDrivenSubscriptionPropertiesOperationCompleted == null)
			{
				this.SetDataDrivenSubscriptionPropertiesOperationCompleted = new SendOrPostCallback(this.OnSetDataDrivenSubscriptionPropertiesOperationCompleted);
			}
			base.InvokeAsync("SetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters }, this.SetDataDrivenSubscriptionPropertiesOperationCompleted, userState);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000069D0 File Offset: 0x00004BD0
		private void OnSetDataDrivenSubscriptionPropertiesOperationCompleted(object arg)
		{
			if (this.SetDataDrivenSubscriptionPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetDataDrivenSubscriptionPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006A10 File Offset: 0x00004C10
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetDataDrivenSubscriptionProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Owner")]
		public string GetDataDrivenSubscriptionProperties(string DataDrivenSubscriptionID, out ExtensionSettings ExtensionSettings, out DataRetrievalPlan DataRetrievalPlan, out string Description, out ActiveState Active, out string Status, out string EventType, out string MatchData, out ParameterValueOrFieldReference[] Parameters)
		{
			object[] array = base.Invoke("GetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID });
			ExtensionSettings = (ExtensionSettings)array[1];
			DataRetrievalPlan = (DataRetrievalPlan)array[2];
			Description = (string)array[3];
			Active = (ActiveState)array[4];
			Status = (string)array[5];
			EventType = (string)array[6];
			MatchData = (string)array[7];
			Parameters = (ParameterValueOrFieldReference[])array[8];
			return (string)array[0];
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006A91 File Offset: 0x00004C91
		public IAsyncResult BeginGetDataDrivenSubscriptionProperties(string DataDrivenSubscriptionID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID }, callback, asyncState);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006AAC File Offset: 0x00004CAC
		public string EndGetDataDrivenSubscriptionProperties(IAsyncResult asyncResult, out ExtensionSettings ExtensionSettings, out DataRetrievalPlan DataRetrievalPlan, out string Description, out ActiveState Active, out string Status, out string EventType, out string MatchData, out ParameterValueOrFieldReference[] Parameters)
		{
			object[] array = base.EndInvoke(asyncResult);
			ExtensionSettings = (ExtensionSettings)array[1];
			DataRetrievalPlan = (DataRetrievalPlan)array[2];
			Description = (string)array[3];
			Active = (ActiveState)array[4];
			Status = (string)array[5];
			EventType = (string)array[6];
			MatchData = (string)array[7];
			Parameters = (ParameterValueOrFieldReference[])array[8];
			return (string)array[0];
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006B1F File Offset: 0x00004D1F
		public void GetDataDrivenSubscriptionPropertiesAsync(string DataDrivenSubscriptionID)
		{
			this.GetDataDrivenSubscriptionPropertiesAsync(DataDrivenSubscriptionID, null);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006B29 File Offset: 0x00004D29
		public void GetDataDrivenSubscriptionPropertiesAsync(string DataDrivenSubscriptionID, object userState)
		{
			if (this.GetDataDrivenSubscriptionPropertiesOperationCompleted == null)
			{
				this.GetDataDrivenSubscriptionPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetDataDrivenSubscriptionPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetDataDrivenSubscriptionProperties", new object[] { DataDrivenSubscriptionID }, this.GetDataDrivenSubscriptionPropertiesOperationCompleted, userState);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006B64 File Offset: 0x00004D64
		private void OnGetDataDrivenSubscriptionPropertiesOperationCompleted(object arg)
		{
			if (this.GetDataDrivenSubscriptionPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDataDrivenSubscriptionPropertiesCompleted(this, new GetDataDrivenSubscriptionPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006BA9 File Offset: 0x00004DA9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/DisableSubscription", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DisableSubscription(string SubscriptionID)
		{
			base.Invoke("DisableSubscription", new object[] { SubscriptionID });
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006BC1 File Offset: 0x00004DC1
		public IAsyncResult BeginDisableSubscription(string SubscriptionID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DisableSubscription", new object[] { SubscriptionID }, callback, asyncState);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006BDA File Offset: 0x00004DDA
		public void EndDisableSubscription(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006BE4 File Offset: 0x00004DE4
		public void DisableSubscriptionAsync(string SubscriptionID)
		{
			this.DisableSubscriptionAsync(SubscriptionID, null);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00006BEE File Offset: 0x00004DEE
		public void DisableSubscriptionAsync(string SubscriptionID, object userState)
		{
			if (this.DisableSubscriptionOperationCompleted == null)
			{
				this.DisableSubscriptionOperationCompleted = new SendOrPostCallback(this.OnDisableSubscriptionOperationCompleted);
			}
			base.InvokeAsync("DisableSubscription", new object[] { SubscriptionID }, this.DisableSubscriptionOperationCompleted, userState);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006C28 File Offset: 0x00004E28
		private void OnDisableSubscriptionOperationCompleted(object arg)
		{
			if (this.DisableSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DisableSubscriptionCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006C67 File Offset: 0x00004E67
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/EnableSubscription", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void EnableSubscription(string SubscriptionID)
		{
			base.Invoke("EnableSubscription", new object[] { SubscriptionID });
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006C7F File Offset: 0x00004E7F
		public IAsyncResult BeginEnableSubscription(string SubscriptionID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("EnableSubscription", new object[] { SubscriptionID }, callback, asyncState);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006C98 File Offset: 0x00004E98
		public void EndEnableSubscription(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006CA2 File Offset: 0x00004EA2
		public void EnableSubscriptionAsync(string SubscriptionID)
		{
			this.EnableSubscriptionAsync(SubscriptionID, null);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006CAC File Offset: 0x00004EAC
		public void EnableSubscriptionAsync(string SubscriptionID, object userState)
		{
			if (this.EnableSubscriptionOperationCompleted == null)
			{
				this.EnableSubscriptionOperationCompleted = new SendOrPostCallback(this.OnEnableSubscriptionOperationCompleted);
			}
			base.InvokeAsync("EnableSubscription", new object[] { SubscriptionID }, this.EnableSubscriptionOperationCompleted, userState);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006CE4 File Offset: 0x00004EE4
		private void OnEnableSubscriptionOperationCompleted(object arg)
		{
			if (this.EnableSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.EnableSubscriptionCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006D23 File Offset: 0x00004F23
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/DeleteSubscription", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteSubscription(string SubscriptionID)
		{
			base.Invoke("DeleteSubscription", new object[] { SubscriptionID });
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006D3B File Offset: 0x00004F3B
		public IAsyncResult BeginDeleteSubscription(string SubscriptionID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteSubscription", new object[] { SubscriptionID }, callback, asyncState);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006D54 File Offset: 0x00004F54
		public void EndDeleteSubscription(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006D5E File Offset: 0x00004F5E
		public void DeleteSubscriptionAsync(string SubscriptionID)
		{
			this.DeleteSubscriptionAsync(SubscriptionID, null);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006D68 File Offset: 0x00004F68
		public void DeleteSubscriptionAsync(string SubscriptionID, object userState)
		{
			if (this.DeleteSubscriptionOperationCompleted == null)
			{
				this.DeleteSubscriptionOperationCompleted = new SendOrPostCallback(this.OnDeleteSubscriptionOperationCompleted);
			}
			base.InvokeAsync("DeleteSubscription", new object[] { SubscriptionID }, this.DeleteSubscriptionOperationCompleted, userState);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006DA0 File Offset: 0x00004FA0
		private void OnDeleteSubscriptionOperationCompleted(object arg)
		{
			if (this.DeleteSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteSubscriptionCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006DDF File Offset: 0x00004FDF
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/CreateSubscription", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("SubscriptionID")]
		public string CreateSubscription(string ItemPath, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			return (string)base.Invoke("CreateSubscription", new object[] { ItemPath, ExtensionSettings, Description, EventType, MatchData, Parameters })[0];
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006E14 File Offset: 0x00005014
		public IAsyncResult BeginCreateSubscription(string ItemPath, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateSubscription", new object[] { ItemPath, ExtensionSettings, Description, EventType, MatchData, Parameters }, callback, asyncState);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006E46 File Offset: 0x00005046
		public string EndCreateSubscription(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006E56 File Offset: 0x00005056
		public void CreateSubscriptionAsync(string ItemPath, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			this.CreateSubscriptionAsync(ItemPath, ExtensionSettings, Description, EventType, MatchData, Parameters, null);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006E68 File Offset: 0x00005068
		public void CreateSubscriptionAsync(string ItemPath, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData, ParameterValue[] Parameters, object userState)
		{
			if (this.CreateSubscriptionOperationCompleted == null)
			{
				this.CreateSubscriptionOperationCompleted = new SendOrPostCallback(this.OnCreateSubscriptionOperationCompleted);
			}
			base.InvokeAsync("CreateSubscription", new object[] { ItemPath, ExtensionSettings, Description, EventType, MatchData, Parameters }, this.CreateSubscriptionOperationCompleted, userState);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006EC4 File Offset: 0x000050C4
		private void OnCreateSubscriptionOperationCompleted(object arg)
		{
			if (this.CreateSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateSubscriptionCompleted(this, new CreateSubscriptionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006F09 File Offset: 0x00005109
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/CreateDataDrivenSubscription", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("SubscriptionID")]
		public string CreateDataDrivenSubscription(string ItemPath, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters)
		{
			return (string)base.Invoke("CreateDataDrivenSubscription", new object[] { ItemPath, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters })[0];
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006F43 File Offset: 0x00005143
		public IAsyncResult BeginCreateDataDrivenSubscription(string ItemPath, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateDataDrivenSubscription", new object[] { ItemPath, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters }, callback, asyncState);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006F7A File Offset: 0x0000517A
		public string EndCreateDataDrivenSubscription(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006F8C File Offset: 0x0000518C
		public void CreateDataDrivenSubscriptionAsync(string ItemPath, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters)
		{
			this.CreateDataDrivenSubscriptionAsync(ItemPath, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters, null);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006FAC File Offset: 0x000051AC
		public void CreateDataDrivenSubscriptionAsync(string ItemPath, ExtensionSettings ExtensionSettings, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, object userState)
		{
			if (this.CreateDataDrivenSubscriptionOperationCompleted == null)
			{
				this.CreateDataDrivenSubscriptionOperationCompleted = new SendOrPostCallback(this.OnCreateDataDrivenSubscriptionOperationCompleted);
			}
			base.InvokeAsync("CreateDataDrivenSubscription", new object[] { ItemPath, ExtensionSettings, DataRetrievalPlan, Description, EventType, MatchData, Parameters }, this.CreateDataDrivenSubscriptionOperationCompleted, userState);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000700C File Offset: 0x0000520C
		private void OnCreateDataDrivenSubscriptionOperationCompleted(object arg)
		{
			if (this.CreateDataDrivenSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateDataDrivenSubscriptionCompleted(this, new CreateDataDrivenSubscriptionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007051 File Offset: 0x00005251
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetExtensionSettings", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ExtensionParameters")]
		public ExtensionParameter[] GetExtensionSettings(string Extension)
		{
			return (ExtensionParameter[])base.Invoke("GetExtensionSettings", new object[] { Extension })[0];
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000706F File Offset: 0x0000526F
		public IAsyncResult BeginGetExtensionSettings(string Extension, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetExtensionSettings", new object[] { Extension }, callback, asyncState);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007088 File Offset: 0x00005288
		public ExtensionParameter[] EndGetExtensionSettings(IAsyncResult asyncResult)
		{
			return (ExtensionParameter[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007098 File Offset: 0x00005298
		public void GetExtensionSettingsAsync(string Extension)
		{
			this.GetExtensionSettingsAsync(Extension, null);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000070A2 File Offset: 0x000052A2
		public void GetExtensionSettingsAsync(string Extension, object userState)
		{
			if (this.GetExtensionSettingsOperationCompleted == null)
			{
				this.GetExtensionSettingsOperationCompleted = new SendOrPostCallback(this.OnGetExtensionSettingsOperationCompleted);
			}
			base.InvokeAsync("GetExtensionSettings", new object[] { Extension }, this.GetExtensionSettingsOperationCompleted, userState);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000070DC File Offset: 0x000052DC
		private void OnGetExtensionSettingsOperationCompleted(object arg)
		{
			if (this.GetExtensionSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetExtensionSettingsCompleted(this, new GetExtensionSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007121 File Offset: 0x00005321
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ValidateExtensionSettings", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ParameterErrors")]
		public ExtensionParameter[] ValidateExtensionSettings(string Extension, ParameterValueOrFieldReference[] ParameterValues, string SiteUrl)
		{
			return (ExtensionParameter[])base.Invoke("ValidateExtensionSettings", new object[] { Extension, ParameterValues, SiteUrl })[0];
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007147 File Offset: 0x00005347
		public IAsyncResult BeginValidateExtensionSettings(string Extension, ParameterValueOrFieldReference[] ParameterValues, string SiteUrl, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ValidateExtensionSettings", new object[] { Extension, ParameterValues, SiteUrl }, callback, asyncState);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000716A File Offset: 0x0000536A
		public ExtensionParameter[] EndValidateExtensionSettings(IAsyncResult asyncResult)
		{
			return (ExtensionParameter[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000717A File Offset: 0x0000537A
		public void ValidateExtensionSettingsAsync(string Extension, ParameterValueOrFieldReference[] ParameterValues, string SiteUrl)
		{
			this.ValidateExtensionSettingsAsync(Extension, ParameterValues, SiteUrl, null);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007188 File Offset: 0x00005388
		public void ValidateExtensionSettingsAsync(string Extension, ParameterValueOrFieldReference[] ParameterValues, string SiteUrl, object userState)
		{
			if (this.ValidateExtensionSettingsOperationCompleted == null)
			{
				this.ValidateExtensionSettingsOperationCompleted = new SendOrPostCallback(this.OnValidateExtensionSettingsOperationCompleted);
			}
			base.InvokeAsync("ValidateExtensionSettings", new object[] { Extension, ParameterValues, SiteUrl }, this.ValidateExtensionSettingsOperationCompleted, userState);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000071D4 File Offset: 0x000053D4
		private void OnValidateExtensionSettingsOperationCompleted(object arg)
		{
			if (this.ValidateExtensionSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ValidateExtensionSettingsCompleted(this, new ValidateExtensionSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007219 File Offset: 0x00005419
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListSubscriptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("SubscriptionItems")]
		public Subscription[] ListSubscriptions(string ItemPathOrSiteURL)
		{
			return (Subscription[])base.Invoke("ListSubscriptions", new object[] { ItemPathOrSiteURL })[0];
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007237 File Offset: 0x00005437
		public IAsyncResult BeginListSubscriptions(string ItemPathOrSiteURL, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListSubscriptions", new object[] { ItemPathOrSiteURL }, callback, asyncState);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007250 File Offset: 0x00005450
		public Subscription[] EndListSubscriptions(IAsyncResult asyncResult)
		{
			return (Subscription[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007260 File Offset: 0x00005460
		public void ListSubscriptionsAsync(string ItemPathOrSiteURL)
		{
			this.ListSubscriptionsAsync(ItemPathOrSiteURL, null);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000726A File Offset: 0x0000546A
		public void ListSubscriptionsAsync(string ItemPathOrSiteURL, object userState)
		{
			if (this.ListSubscriptionsOperationCompleted == null)
			{
				this.ListSubscriptionsOperationCompleted = new SendOrPostCallback(this.OnListSubscriptionsOperationCompleted);
			}
			base.InvokeAsync("ListSubscriptions", new object[] { ItemPathOrSiteURL }, this.ListSubscriptionsOperationCompleted, userState);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000072A4 File Offset: 0x000054A4
		private void OnListSubscriptionsOperationCompleted(object arg)
		{
			if (this.ListSubscriptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListSubscriptionsCompleted(this, new ListSubscriptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000072E9 File Offset: 0x000054E9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListMySubscriptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("SubscriptionItems")]
		public Subscription[] ListMySubscriptions(string ItemPathOrSiteURL)
		{
			return (Subscription[])base.Invoke("ListMySubscriptions", new object[] { ItemPathOrSiteURL })[0];
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007307 File Offset: 0x00005507
		public IAsyncResult BeginListMySubscriptions(string ItemPathOrSiteURL, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListMySubscriptions", new object[] { ItemPathOrSiteURL }, callback, asyncState);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007320 File Offset: 0x00005520
		public Subscription[] EndListMySubscriptions(IAsyncResult asyncResult)
		{
			return (Subscription[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007330 File Offset: 0x00005530
		public void ListMySubscriptionsAsync(string ItemPathOrSiteURL)
		{
			this.ListMySubscriptionsAsync(ItemPathOrSiteURL, null);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000733A File Offset: 0x0000553A
		public void ListMySubscriptionsAsync(string ItemPathOrSiteURL, object userState)
		{
			if (this.ListMySubscriptionsOperationCompleted == null)
			{
				this.ListMySubscriptionsOperationCompleted = new SendOrPostCallback(this.OnListMySubscriptionsOperationCompleted);
			}
			base.InvokeAsync("ListMySubscriptions", new object[] { ItemPathOrSiteURL }, this.ListMySubscriptionsOperationCompleted, userState);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007374 File Offset: 0x00005574
		private void OnListMySubscriptionsOperationCompleted(object arg)
		{
			if (this.ListMySubscriptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListMySubscriptionsCompleted(this, new ListMySubscriptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000073B9 File Offset: 0x000055B9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListSubscriptionsUsingDataSource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("SubscriptionItems")]
		public Subscription[] ListSubscriptionsUsingDataSource(string DataSource)
		{
			return (Subscription[])base.Invoke("ListSubscriptionsUsingDataSource", new object[] { DataSource })[0];
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000073D7 File Offset: 0x000055D7
		public IAsyncResult BeginListSubscriptionsUsingDataSource(string DataSource, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListSubscriptionsUsingDataSource", new object[] { DataSource }, callback, asyncState);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000073F0 File Offset: 0x000055F0
		public Subscription[] EndListSubscriptionsUsingDataSource(IAsyncResult asyncResult)
		{
			return (Subscription[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007400 File Offset: 0x00005600
		public void ListSubscriptionsUsingDataSourceAsync(string DataSource)
		{
			this.ListSubscriptionsUsingDataSourceAsync(DataSource, null);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000740A File Offset: 0x0000560A
		public void ListSubscriptionsUsingDataSourceAsync(string DataSource, object userState)
		{
			if (this.ListSubscriptionsUsingDataSourceOperationCompleted == null)
			{
				this.ListSubscriptionsUsingDataSourceOperationCompleted = new SendOrPostCallback(this.OnListSubscriptionsUsingDataSourceOperationCompleted);
			}
			base.InvokeAsync("ListSubscriptionsUsingDataSource", new object[] { DataSource }, this.ListSubscriptionsUsingDataSourceOperationCompleted, userState);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007444 File Offset: 0x00005644
		private void OnListSubscriptionsUsingDataSourceOperationCompleted(object arg)
		{
			if (this.ListSubscriptionsUsingDataSourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListSubscriptionsUsingDataSourceCompleted(this, new ListSubscriptionsUsingDataSourceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007489 File Offset: 0x00005689
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ChangeSubscriptionOwner", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void ChangeSubscriptionOwner(string SubscriptionID, string NewOwner)
		{
			base.Invoke("ChangeSubscriptionOwner", new object[] { SubscriptionID, NewOwner });
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000074A5 File Offset: 0x000056A5
		public IAsyncResult BeginChangeSubscriptionOwner(string SubscriptionID, string NewOwner, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ChangeSubscriptionOwner", new object[] { SubscriptionID, NewOwner }, callback, asyncState);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000074C3 File Offset: 0x000056C3
		public void EndChangeSubscriptionOwner(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000074CD File Offset: 0x000056CD
		public void ChangeSubscriptionOwnerAsync(string SubscriptionID, string NewOwner)
		{
			this.ChangeSubscriptionOwnerAsync(SubscriptionID, NewOwner, null);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000074D8 File Offset: 0x000056D8
		public void ChangeSubscriptionOwnerAsync(string SubscriptionID, string NewOwner, object userState)
		{
			if (this.ChangeSubscriptionOwnerOperationCompleted == null)
			{
				this.ChangeSubscriptionOwnerOperationCompleted = new SendOrPostCallback(this.OnChangeSubscriptionOwnerOperationCompleted);
			}
			base.InvokeAsync("ChangeSubscriptionOwner", new object[] { SubscriptionID, NewOwner }, this.ChangeSubscriptionOwnerOperationCompleted, userState);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007514 File Offset: 0x00005714
		private void OnChangeSubscriptionOwnerOperationCompleted(object arg)
		{
			if (this.ChangeSubscriptionOwnerCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ChangeSubscriptionOwnerCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007553 File Offset: 0x00005753
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/CreateDataSource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ItemInfo")]
		public CatalogItem CreateDataSource(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties)
		{
			return (CatalogItem)base.Invoke("CreateDataSource", new object[] { DataSource, Parent, Overwrite, Definition, Properties })[0];
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007588 File Offset: 0x00005788
		public IAsyncResult BeginCreateDataSource(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateDataSource", new object[] { DataSource, Parent, Overwrite, Definition, Properties }, callback, asyncState);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000075BA File Offset: 0x000057BA
		public CatalogItem EndCreateDataSource(IAsyncResult asyncResult)
		{
			return (CatalogItem)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000075CA File Offset: 0x000057CA
		public void CreateDataSourceAsync(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties)
		{
			this.CreateDataSourceAsync(DataSource, Parent, Overwrite, Definition, Properties, null);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000075DC File Offset: 0x000057DC
		public void CreateDataSourceAsync(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties, object userState)
		{
			if (this.CreateDataSourceOperationCompleted == null)
			{
				this.CreateDataSourceOperationCompleted = new SendOrPostCallback(this.OnCreateDataSourceOperationCompleted);
			}
			base.InvokeAsync("CreateDataSource", new object[] { DataSource, Parent, Overwrite, Definition, Properties }, this.CreateDataSourceOperationCompleted, userState);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007638 File Offset: 0x00005838
		private void OnCreateDataSourceOperationCompleted(object arg)
		{
			if (this.CreateDataSourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateDataSourceCompleted(this, new CreateDataSourceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007680 File Offset: 0x00005880
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/PrepareQuery", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("DataSettings")]
		public DataSetDefinition PrepareQuery(DataSource DataSource, DataSetDefinition DataSet, out bool Changed, out string[] ParameterNames)
		{
			object[] array = base.Invoke("PrepareQuery", new object[] { DataSource, DataSet });
			Changed = (bool)array[1];
			ParameterNames = (string[])array[2];
			return (DataSetDefinition)array[0];
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000076C4 File Offset: 0x000058C4
		public IAsyncResult BeginPrepareQuery(DataSource DataSource, DataSetDefinition DataSet, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("PrepareQuery", new object[] { DataSource, DataSet }, callback, asyncState);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000076E4 File Offset: 0x000058E4
		public DataSetDefinition EndPrepareQuery(IAsyncResult asyncResult, out bool Changed, out string[] ParameterNames)
		{
			object[] array = base.EndInvoke(asyncResult);
			Changed = (bool)array[1];
			ParameterNames = (string[])array[2];
			return (DataSetDefinition)array[0];
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007715 File Offset: 0x00005915
		public void PrepareQueryAsync(DataSource DataSource, DataSetDefinition DataSet)
		{
			this.PrepareQueryAsync(DataSource, DataSet, null);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007720 File Offset: 0x00005920
		public void PrepareQueryAsync(DataSource DataSource, DataSetDefinition DataSet, object userState)
		{
			if (this.PrepareQueryOperationCompleted == null)
			{
				this.PrepareQueryOperationCompleted = new SendOrPostCallback(this.OnPrepareQueryOperationCompleted);
			}
			base.InvokeAsync("PrepareQuery", new object[] { DataSource, DataSet }, this.PrepareQueryOperationCompleted, userState);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000775C File Offset: 0x0000595C
		private void OnPrepareQueryOperationCompleted(object arg)
		{
			if (this.PrepareQueryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.PrepareQueryCompleted(this, new PrepareQueryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000077A1 File Offset: 0x000059A1
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/EnableDataSource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void EnableDataSource(string DataSource)
		{
			base.Invoke("EnableDataSource", new object[] { DataSource });
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000077B9 File Offset: 0x000059B9
		public IAsyncResult BeginEnableDataSource(string DataSource, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("EnableDataSource", new object[] { DataSource }, callback, asyncState);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000077D2 File Offset: 0x000059D2
		public void EndEnableDataSource(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000077DC File Offset: 0x000059DC
		public void EnableDataSourceAsync(string DataSource)
		{
			this.EnableDataSourceAsync(DataSource, null);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000077E6 File Offset: 0x000059E6
		public void EnableDataSourceAsync(string DataSource, object userState)
		{
			if (this.EnableDataSourceOperationCompleted == null)
			{
				this.EnableDataSourceOperationCompleted = new SendOrPostCallback(this.OnEnableDataSourceOperationCompleted);
			}
			base.InvokeAsync("EnableDataSource", new object[] { DataSource }, this.EnableDataSourceOperationCompleted, userState);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007820 File Offset: 0x00005A20
		private void OnEnableDataSourceOperationCompleted(object arg)
		{
			if (this.EnableDataSourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.EnableDataSourceCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000785F File Offset: 0x00005A5F
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/DisableDataSource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DisableDataSource(string DataSource)
		{
			base.Invoke("DisableDataSource", new object[] { DataSource });
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00007877 File Offset: 0x00005A77
		public IAsyncResult BeginDisableDataSource(string DataSource, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DisableDataSource", new object[] { DataSource }, callback, asyncState);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007890 File Offset: 0x00005A90
		public void EndDisableDataSource(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000789A File Offset: 0x00005A9A
		public void DisableDataSourceAsync(string DataSource)
		{
			this.DisableDataSourceAsync(DataSource, null);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000078A4 File Offset: 0x00005AA4
		public void DisableDataSourceAsync(string DataSource, object userState)
		{
			if (this.DisableDataSourceOperationCompleted == null)
			{
				this.DisableDataSourceOperationCompleted = new SendOrPostCallback(this.OnDisableDataSourceOperationCompleted);
			}
			base.InvokeAsync("DisableDataSource", new object[] { DataSource }, this.DisableDataSourceOperationCompleted, userState);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000078DC File Offset: 0x00005ADC
		private void OnDisableDataSourceOperationCompleted(object arg)
		{
			if (this.DisableDataSourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DisableDataSourceCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000791B File Offset: 0x00005B1B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetDataSourceContents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetDataSourceContents(string DataSource, DataSourceDefinition Definition)
		{
			base.Invoke("SetDataSourceContents", new object[] { DataSource, Definition });
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00007937 File Offset: 0x00005B37
		public IAsyncResult BeginSetDataSourceContents(string DataSource, DataSourceDefinition Definition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetDataSourceContents", new object[] { DataSource, Definition }, callback, asyncState);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00007955 File Offset: 0x00005B55
		public void EndSetDataSourceContents(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000795F File Offset: 0x00005B5F
		public void SetDataSourceContentsAsync(string DataSource, DataSourceDefinition Definition)
		{
			this.SetDataSourceContentsAsync(DataSource, Definition, null);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000796A File Offset: 0x00005B6A
		public void SetDataSourceContentsAsync(string DataSource, DataSourceDefinition Definition, object userState)
		{
			if (this.SetDataSourceContentsOperationCompleted == null)
			{
				this.SetDataSourceContentsOperationCompleted = new SendOrPostCallback(this.OnSetDataSourceContentsOperationCompleted);
			}
			base.InvokeAsync("SetDataSourceContents", new object[] { DataSource, Definition }, this.SetDataSourceContentsOperationCompleted, userState);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000079A8 File Offset: 0x00005BA8
		private void OnSetDataSourceContentsOperationCompleted(object arg)
		{
			if (this.SetDataSourceContentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetDataSourceContentsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000079E7 File Offset: 0x00005BE7
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetDataSourceContents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Definition")]
		public DataSourceDefinition GetDataSourceContents(string DataSource)
		{
			return (DataSourceDefinition)base.Invoke("GetDataSourceContents", new object[] { DataSource })[0];
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00007A05 File Offset: 0x00005C05
		public IAsyncResult BeginGetDataSourceContents(string DataSource, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDataSourceContents", new object[] { DataSource }, callback, asyncState);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007A1E File Offset: 0x00005C1E
		public DataSourceDefinition EndGetDataSourceContents(IAsyncResult asyncResult)
		{
			return (DataSourceDefinition)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007A2E File Offset: 0x00005C2E
		public void GetDataSourceContentsAsync(string DataSource)
		{
			this.GetDataSourceContentsAsync(DataSource, null);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007A38 File Offset: 0x00005C38
		public void GetDataSourceContentsAsync(string DataSource, object userState)
		{
			if (this.GetDataSourceContentsOperationCompleted == null)
			{
				this.GetDataSourceContentsOperationCompleted = new SendOrPostCallback(this.OnGetDataSourceContentsOperationCompleted);
			}
			base.InvokeAsync("GetDataSourceContents", new object[] { DataSource }, this.GetDataSourceContentsOperationCompleted, userState);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00007A70 File Offset: 0x00005C70
		private void OnGetDataSourceContentsOperationCompleted(object arg)
		{
			if (this.GetDataSourceContentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDataSourceContentsCompleted(this, new GetDataSourceContentsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007AB5 File Offset: 0x00005CB5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListDatabaseCredentialRetrievalOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListDatabaseCredentialRetrievalOptions()
		{
			return (string[])base.Invoke("ListDatabaseCredentialRetrievalOptions", new object[0])[0];
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007ACF File Offset: 0x00005CCF
		public IAsyncResult BeginListDatabaseCredentialRetrievalOptions(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListDatabaseCredentialRetrievalOptions", new object[0], callback, asyncState);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007AE4 File Offset: 0x00005CE4
		public string[] EndListDatabaseCredentialRetrievalOptions(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007AF4 File Offset: 0x00005CF4
		public void ListDatabaseCredentialRetrievalOptionsAsync()
		{
			this.ListDatabaseCredentialRetrievalOptionsAsync(null);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007AFD File Offset: 0x00005CFD
		public void ListDatabaseCredentialRetrievalOptionsAsync(object userState)
		{
			if (this.ListDatabaseCredentialRetrievalOptionsOperationCompleted == null)
			{
				this.ListDatabaseCredentialRetrievalOptionsOperationCompleted = new SendOrPostCallback(this.OnListDatabaseCredentialRetrievalOptionsOperationCompleted);
			}
			base.InvokeAsync("ListDatabaseCredentialRetrievalOptions", new object[0], this.ListDatabaseCredentialRetrievalOptionsOperationCompleted, userState);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007B34 File Offset: 0x00005D34
		private void OnListDatabaseCredentialRetrievalOptionsOperationCompleted(object arg)
		{
			if (this.ListDatabaseCredentialRetrievalOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListDatabaseCredentialRetrievalOptionsCompleted(this, new ListDatabaseCredentialRetrievalOptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00007B79 File Offset: 0x00005D79
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetItemDataSources", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetItemDataSources(string ItemPath, DataSource[] DataSources)
		{
			base.Invoke("SetItemDataSources", new object[] { ItemPath, DataSources });
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00007B95 File Offset: 0x00005D95
		public IAsyncResult BeginSetItemDataSources(string ItemPath, DataSource[] DataSources, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetItemDataSources", new object[] { ItemPath, DataSources }, callback, asyncState);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007BB3 File Offset: 0x00005DB3
		public void EndSetItemDataSources(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007BBD File Offset: 0x00005DBD
		public void SetItemDataSourcesAsync(string ItemPath, DataSource[] DataSources)
		{
			this.SetItemDataSourcesAsync(ItemPath, DataSources, null);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007BC8 File Offset: 0x00005DC8
		public void SetItemDataSourcesAsync(string ItemPath, DataSource[] DataSources, object userState)
		{
			if (this.SetItemDataSourcesOperationCompleted == null)
			{
				this.SetItemDataSourcesOperationCompleted = new SendOrPostCallback(this.OnSetItemDataSourcesOperationCompleted);
			}
			base.InvokeAsync("SetItemDataSources", new object[] { ItemPath, DataSources }, this.SetItemDataSourcesOperationCompleted, userState);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007C04 File Offset: 0x00005E04
		private void OnSetItemDataSourcesOperationCompleted(object arg)
		{
			if (this.SetItemDataSourcesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetItemDataSourcesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007C43 File Offset: 0x00005E43
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetItemDataSources", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("DataSources")]
		public DataSource[] GetItemDataSources(string ItemPath)
		{
			return (DataSource[])base.Invoke("GetItemDataSources", new object[] { ItemPath })[0];
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007C61 File Offset: 0x00005E61
		public IAsyncResult BeginGetItemDataSources(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemDataSources", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007C7A File Offset: 0x00005E7A
		public DataSource[] EndGetItemDataSources(IAsyncResult asyncResult)
		{
			return (DataSource[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007C8A File Offset: 0x00005E8A
		public void GetItemDataSourcesAsync(string ItemPath)
		{
			this.GetItemDataSourcesAsync(ItemPath, null);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007C94 File Offset: 0x00005E94
		public void GetItemDataSourcesAsync(string ItemPath, object userState)
		{
			if (this.GetItemDataSourcesOperationCompleted == null)
			{
				this.GetItemDataSourcesOperationCompleted = new SendOrPostCallback(this.OnGetItemDataSourcesOperationCompleted);
			}
			base.InvokeAsync("GetItemDataSources", new object[] { ItemPath }, this.GetItemDataSourcesOperationCompleted, userState);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007CCC File Offset: 0x00005ECC
		private void OnGetItemDataSourcesOperationCompleted(object arg)
		{
			if (this.GetItemDataSourcesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemDataSourcesCompleted(this, new GetItemDataSourcesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007D14 File Offset: 0x00005F14
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/TestConnectForDataSourceDefinition", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool TestConnectForDataSourceDefinition(DataSourceDefinition DataSourceDefinition, string UserName, string Password, out string ConnectError)
		{
			object[] array = base.Invoke("TestConnectForDataSourceDefinition", new object[] { DataSourceDefinition, UserName, Password });
			ConnectError = (string)array[1];
			return (bool)array[0];
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007D52 File Offset: 0x00005F52
		public IAsyncResult BeginTestConnectForDataSourceDefinition(DataSourceDefinition DataSourceDefinition, string UserName, string Password, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("TestConnectForDataSourceDefinition", new object[] { DataSourceDefinition, UserName, Password }, callback, asyncState);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007D78 File Offset: 0x00005F78
		public bool EndTestConnectForDataSourceDefinition(IAsyncResult asyncResult, out string ConnectError)
		{
			object[] array = base.EndInvoke(asyncResult);
			ConnectError = (string)array[1];
			return (bool)array[0];
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007D9F File Offset: 0x00005F9F
		public void TestConnectForDataSourceDefinitionAsync(DataSourceDefinition DataSourceDefinition, string UserName, string Password)
		{
			this.TestConnectForDataSourceDefinitionAsync(DataSourceDefinition, UserName, Password, null);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007DAC File Offset: 0x00005FAC
		public void TestConnectForDataSourceDefinitionAsync(DataSourceDefinition DataSourceDefinition, string UserName, string Password, object userState)
		{
			if (this.TestConnectForDataSourceDefinitionOperationCompleted == null)
			{
				this.TestConnectForDataSourceDefinitionOperationCompleted = new SendOrPostCallback(this.OnTestConnectForDataSourceDefinitionOperationCompleted);
			}
			base.InvokeAsync("TestConnectForDataSourceDefinition", new object[] { DataSourceDefinition, UserName, Password }, this.TestConnectForDataSourceDefinitionOperationCompleted, userState);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007DF8 File Offset: 0x00005FF8
		private void OnTestConnectForDataSourceDefinitionOperationCompleted(object arg)
		{
			if (this.TestConnectForDataSourceDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.TestConnectForDataSourceDefinitionCompleted(this, new TestConnectForDataSourceDefinitionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007E40 File Offset: 0x00006040
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/TestConnectForItemDataSource", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool TestConnectForItemDataSource(string ItemPath, string DataSourceName, string UserName, string Password, out string ConnectError)
		{
			object[] array = base.Invoke("TestConnectForItemDataSource", new object[] { ItemPath, DataSourceName, UserName, Password });
			ConnectError = (string)array[1];
			return (bool)array[0];
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007E83 File Offset: 0x00006083
		public IAsyncResult BeginTestConnectForItemDataSource(string ItemPath, string DataSourceName, string UserName, string Password, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("TestConnectForItemDataSource", new object[] { ItemPath, DataSourceName, UserName, Password }, callback, asyncState);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00007EAC File Offset: 0x000060AC
		public bool EndTestConnectForItemDataSource(IAsyncResult asyncResult, out string ConnectError)
		{
			object[] array = base.EndInvoke(asyncResult);
			ConnectError = (string)array[1];
			return (bool)array[0];
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007ED3 File Offset: 0x000060D3
		public void TestConnectForItemDataSourceAsync(string ItemPath, string DataSourceName, string UserName, string Password)
		{
			this.TestConnectForItemDataSourceAsync(ItemPath, DataSourceName, UserName, Password, null);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007EE4 File Offset: 0x000060E4
		public void TestConnectForItemDataSourceAsync(string ItemPath, string DataSourceName, string UserName, string Password, object userState)
		{
			if (this.TestConnectForItemDataSourceOperationCompleted == null)
			{
				this.TestConnectForItemDataSourceOperationCompleted = new SendOrPostCallback(this.OnTestConnectForItemDataSourceOperationCompleted);
			}
			base.InvokeAsync("TestConnectForItemDataSource", new object[] { ItemPath, DataSourceName, UserName, Password }, this.TestConnectForItemDataSourceOperationCompleted, userState);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007F38 File Offset: 0x00006138
		private void OnTestConnectForItemDataSourceOperationCompleted(object arg)
		{
			if (this.TestConnectForItemDataSourceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.TestConnectForItemDataSourceCompleted(this, new TestConnectForItemDataSourceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007F7D File Offset: 0x0000617D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/CreateRole", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void CreateRole(string Name, string Description, string[] TaskIDs)
		{
			base.Invoke("CreateRole", new object[] { Name, Description, TaskIDs });
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007F9D File Offset: 0x0000619D
		public IAsyncResult BeginCreateRole(string Name, string Description, string[] TaskIDs, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateRole", new object[] { Name, Description, TaskIDs }, callback, asyncState);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007FC0 File Offset: 0x000061C0
		public void EndCreateRole(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007FCA File Offset: 0x000061CA
		public void CreateRoleAsync(string Name, string Description, string[] TaskIDs)
		{
			this.CreateRoleAsync(Name, Description, TaskIDs, null);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007FD8 File Offset: 0x000061D8
		public void CreateRoleAsync(string Name, string Description, string[] TaskIDs, object userState)
		{
			if (this.CreateRoleOperationCompleted == null)
			{
				this.CreateRoleOperationCompleted = new SendOrPostCallback(this.OnCreateRoleOperationCompleted);
			}
			base.InvokeAsync("CreateRole", new object[] { Name, Description, TaskIDs }, this.CreateRoleOperationCompleted, userState);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008024 File Offset: 0x00006224
		private void OnCreateRoleOperationCompleted(object arg)
		{
			if (this.CreateRoleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateRoleCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008063 File Offset: 0x00006263
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetRoleProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetRoleProperties(string Name, string Description, string[] TaskIDs)
		{
			base.Invoke("SetRoleProperties", new object[] { Name, Description, TaskIDs });
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008083 File Offset: 0x00006283
		public IAsyncResult BeginSetRoleProperties(string Name, string Description, string[] TaskIDs, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetRoleProperties", new object[] { Name, Description, TaskIDs }, callback, asyncState);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000080A6 File Offset: 0x000062A6
		public void EndSetRoleProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000080B0 File Offset: 0x000062B0
		public void SetRolePropertiesAsync(string Name, string Description, string[] TaskIDs)
		{
			this.SetRolePropertiesAsync(Name, Description, TaskIDs, null);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000080BC File Offset: 0x000062BC
		public void SetRolePropertiesAsync(string Name, string Description, string[] TaskIDs, object userState)
		{
			if (this.SetRolePropertiesOperationCompleted == null)
			{
				this.SetRolePropertiesOperationCompleted = new SendOrPostCallback(this.OnSetRolePropertiesOperationCompleted);
			}
			base.InvokeAsync("SetRoleProperties", new object[] { Name, Description, TaskIDs }, this.SetRolePropertiesOperationCompleted, userState);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008108 File Offset: 0x00006308
		private void OnSetRolePropertiesOperationCompleted(object arg)
		{
			if (this.SetRolePropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetRolePropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008148 File Offset: 0x00006348
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetRoleProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("TaskIDs")]
		public string[] GetRoleProperties(string Name, string SiteUrl, out string Description)
		{
			object[] array = base.Invoke("GetRoleProperties", new object[] { Name, SiteUrl });
			Description = (string)array[1];
			return (string[])array[0];
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008181 File Offset: 0x00006381
		public IAsyncResult BeginGetRoleProperties(string Name, string SiteUrl, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetRoleProperties", new object[] { Name, SiteUrl }, callback, asyncState);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000081A0 File Offset: 0x000063A0
		public string[] EndGetRoleProperties(IAsyncResult asyncResult, out string Description)
		{
			object[] array = base.EndInvoke(asyncResult);
			Description = (string)array[1];
			return (string[])array[0];
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000081C7 File Offset: 0x000063C7
		public void GetRolePropertiesAsync(string Name, string SiteUrl)
		{
			this.GetRolePropertiesAsync(Name, SiteUrl, null);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000081D2 File Offset: 0x000063D2
		public void GetRolePropertiesAsync(string Name, string SiteUrl, object userState)
		{
			if (this.GetRolePropertiesOperationCompleted == null)
			{
				this.GetRolePropertiesOperationCompleted = new SendOrPostCallback(this.OnGetRolePropertiesOperationCompleted);
			}
			base.InvokeAsync("GetRoleProperties", new object[] { Name, SiteUrl }, this.GetRolePropertiesOperationCompleted, userState);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008210 File Offset: 0x00006410
		private void OnGetRolePropertiesOperationCompleted(object arg)
		{
			if (this.GetRolePropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetRolePropertiesCompleted(this, new GetRolePropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008255 File Offset: 0x00006455
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/DeleteRole", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteRole(string Name)
		{
			base.Invoke("DeleteRole", new object[] { Name });
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000826D File Offset: 0x0000646D
		public IAsyncResult BeginDeleteRole(string Name, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteRole", new object[] { Name }, callback, asyncState);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008286 File Offset: 0x00006486
		public void EndDeleteRole(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008290 File Offset: 0x00006490
		public void DeleteRoleAsync(string Name)
		{
			this.DeleteRoleAsync(Name, null);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000829A File Offset: 0x0000649A
		public void DeleteRoleAsync(string Name, object userState)
		{
			if (this.DeleteRoleOperationCompleted == null)
			{
				this.DeleteRoleOperationCompleted = new SendOrPostCallback(this.OnDeleteRoleOperationCompleted);
			}
			base.InvokeAsync("DeleteRole", new object[] { Name }, this.DeleteRoleOperationCompleted, userState);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000082D4 File Offset: 0x000064D4
		private void OnDeleteRoleOperationCompleted(object arg)
		{
			if (this.DeleteRoleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteRoleCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008313 File Offset: 0x00006513
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListRoles", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Roles")]
		public Role[] ListRoles(string SecurityScope, string SiteUrl)
		{
			return (Role[])base.Invoke("ListRoles", new object[] { SecurityScope, SiteUrl })[0];
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008335 File Offset: 0x00006535
		public IAsyncResult BeginListRoles(string SecurityScope, string SiteUrl, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListRoles", new object[] { SecurityScope, SiteUrl }, callback, asyncState);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008353 File Offset: 0x00006553
		public Role[] EndListRoles(IAsyncResult asyncResult)
		{
			return (Role[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008363 File Offset: 0x00006563
		public void ListRolesAsync(string SecurityScope, string SiteUrl)
		{
			this.ListRolesAsync(SecurityScope, SiteUrl, null);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000836E File Offset: 0x0000656E
		public void ListRolesAsync(string SecurityScope, string SiteUrl, object userState)
		{
			if (this.ListRolesOperationCompleted == null)
			{
				this.ListRolesOperationCompleted = new SendOrPostCallback(this.OnListRolesOperationCompleted);
			}
			base.InvokeAsync("ListRoles", new object[] { SecurityScope, SiteUrl }, this.ListRolesOperationCompleted, userState);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000083AC File Offset: 0x000065AC
		private void OnListRolesOperationCompleted(object arg)
		{
			if (this.ListRolesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListRolesCompleted(this, new ListRolesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000083F1 File Offset: 0x000065F1
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListTasks", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Tasks")]
		public Task[] ListTasks(string SecurityScope)
		{
			return (Task[])base.Invoke("ListTasks", new object[] { SecurityScope })[0];
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000840F File Offset: 0x0000660F
		public IAsyncResult BeginListTasks(string SecurityScope, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListTasks", new object[] { SecurityScope }, callback, asyncState);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00008428 File Offset: 0x00006628
		public Task[] EndListTasks(IAsyncResult asyncResult)
		{
			return (Task[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008438 File Offset: 0x00006638
		public void ListTasksAsync(string SecurityScope)
		{
			this.ListTasksAsync(SecurityScope, null);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00008442 File Offset: 0x00006642
		public void ListTasksAsync(string SecurityScope, object userState)
		{
			if (this.ListTasksOperationCompleted == null)
			{
				this.ListTasksOperationCompleted = new SendOrPostCallback(this.OnListTasksOperationCompleted);
			}
			base.InvokeAsync("ListTasks", new object[] { SecurityScope }, this.ListTasksOperationCompleted, userState);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000847C File Offset: 0x0000667C
		private void OnListTasksOperationCompleted(object arg)
		{
			if (this.ListTasksCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListTasksCompleted(this, new ListTasksCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000084C1 File Offset: 0x000066C1
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetPolicies(string ItemPath, Policy[] Policies)
		{
			base.Invoke("SetPolicies", new object[] { ItemPath, Policies });
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000084DD File Offset: 0x000066DD
		public IAsyncResult BeginSetPolicies(string ItemPath, Policy[] Policies, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetPolicies", new object[] { ItemPath, Policies }, callback, asyncState);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000084FB File Offset: 0x000066FB
		public void EndSetPolicies(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00008505 File Offset: 0x00006705
		public void SetPoliciesAsync(string ItemPath, Policy[] Policies)
		{
			this.SetPoliciesAsync(ItemPath, Policies, null);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008510 File Offset: 0x00006710
		public void SetPoliciesAsync(string ItemPath, Policy[] Policies, object userState)
		{
			if (this.SetPoliciesOperationCompleted == null)
			{
				this.SetPoliciesOperationCompleted = new SendOrPostCallback(this.OnSetPoliciesOperationCompleted);
			}
			base.InvokeAsync("SetPolicies", new object[] { ItemPath, Policies }, this.SetPoliciesOperationCompleted, userState);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000854C File Offset: 0x0000674C
		private void OnSetPoliciesOperationCompleted(object arg)
		{
			if (this.SetPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetPoliciesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000858C File Offset: 0x0000678C
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Policies")]
		public Policy[] GetPolicies(string ItemPath, out bool InheritParent)
		{
			object[] array = base.Invoke("GetPolicies", new object[] { ItemPath });
			InheritParent = (bool)array[1];
			return (Policy[])array[0];
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000085C1 File Offset: 0x000067C1
		public IAsyncResult BeginGetPolicies(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPolicies", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000085DC File Offset: 0x000067DC
		public Policy[] EndGetPolicies(IAsyncResult asyncResult, out bool InheritParent)
		{
			object[] array = base.EndInvoke(asyncResult);
			InheritParent = (bool)array[1];
			return (Policy[])array[0];
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00008603 File Offset: 0x00006803
		public void GetPoliciesAsync(string ItemPath)
		{
			this.GetPoliciesAsync(ItemPath, null);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000860D File Offset: 0x0000680D
		public void GetPoliciesAsync(string ItemPath, object userState)
		{
			if (this.GetPoliciesOperationCompleted == null)
			{
				this.GetPoliciesOperationCompleted = new SendOrPostCallback(this.OnGetPoliciesOperationCompleted);
			}
			base.InvokeAsync("GetPolicies", new object[] { ItemPath }, this.GetPoliciesOperationCompleted, userState);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008648 File Offset: 0x00006848
		private void OnGetPoliciesOperationCompleted(object arg)
		{
			if (this.GetPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPoliciesCompleted(this, new GetPoliciesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000868D File Offset: 0x0000688D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetItemDataSourcePrompts", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("DataSourcePrompts")]
		public DataSourcePrompt[] GetItemDataSourcePrompts(string ItemPath)
		{
			return (DataSourcePrompt[])base.Invoke("GetItemDataSourcePrompts", new object[] { ItemPath })[0];
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000086AB File Offset: 0x000068AB
		public IAsyncResult BeginGetItemDataSourcePrompts(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemDataSourcePrompts", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000086C4 File Offset: 0x000068C4
		public DataSourcePrompt[] EndGetItemDataSourcePrompts(IAsyncResult asyncResult)
		{
			return (DataSourcePrompt[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000086D4 File Offset: 0x000068D4
		public void GetItemDataSourcePromptsAsync(string ItemPath)
		{
			this.GetItemDataSourcePromptsAsync(ItemPath, null);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000086DE File Offset: 0x000068DE
		public void GetItemDataSourcePromptsAsync(string ItemPath, object userState)
		{
			if (this.GetItemDataSourcePromptsOperationCompleted == null)
			{
				this.GetItemDataSourcePromptsOperationCompleted = new SendOrPostCallback(this.OnGetItemDataSourcePromptsOperationCompleted);
			}
			base.InvokeAsync("GetItemDataSourcePrompts", new object[] { ItemPath }, this.GetItemDataSourcePromptsOperationCompleted, userState);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00008718 File Offset: 0x00006918
		private void OnGetItemDataSourcePromptsOperationCompleted(object arg)
		{
			if (this.GetItemDataSourcePromptsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemDataSourcePromptsCompleted(this, new GetItemDataSourcePromptsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00008760 File Offset: 0x00006960
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GenerateModel", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ItemInfo")]
		public CatalogItem GenerateModel(string DataSource, string Model, string Parent, Property[] Properties, out Warning[] Warnings)
		{
			object[] array = base.Invoke("GenerateModel", new object[] { DataSource, Model, Parent, Properties });
			Warnings = (Warning[])array[1];
			return (CatalogItem)array[0];
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000087A3 File Offset: 0x000069A3
		public IAsyncResult BeginGenerateModel(string DataSource, string Model, string Parent, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GenerateModel", new object[] { DataSource, Model, Parent, Properties }, callback, asyncState);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000087CC File Offset: 0x000069CC
		public CatalogItem EndGenerateModel(IAsyncResult asyncResult, out Warning[] Warnings)
		{
			object[] array = base.EndInvoke(asyncResult);
			Warnings = (Warning[])array[1];
			return (CatalogItem)array[0];
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000087F3 File Offset: 0x000069F3
		public void GenerateModelAsync(string DataSource, string Model, string Parent, Property[] Properties)
		{
			this.GenerateModelAsync(DataSource, Model, Parent, Properties, null);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00008804 File Offset: 0x00006A04
		public void GenerateModelAsync(string DataSource, string Model, string Parent, Property[] Properties, object userState)
		{
			if (this.GenerateModelOperationCompleted == null)
			{
				this.GenerateModelOperationCompleted = new SendOrPostCallback(this.OnGenerateModelOperationCompleted);
			}
			base.InvokeAsync("GenerateModel", new object[] { DataSource, Model, Parent, Properties }, this.GenerateModelOperationCompleted, userState);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00008858 File Offset: 0x00006A58
		private void OnGenerateModelOperationCompleted(object arg)
		{
			if (this.GenerateModelCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GenerateModelCompleted(this, new GenerateModelCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000889D File Offset: 0x00006A9D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetModelItemPermissions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Permissions")]
		public string[] GetModelItemPermissions(string Model, string ModelItemID)
		{
			return (string[])base.Invoke("GetModelItemPermissions", new object[] { Model, ModelItemID })[0];
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000088BF File Offset: 0x00006ABF
		public IAsyncResult BeginGetModelItemPermissions(string Model, string ModelItemID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetModelItemPermissions", new object[] { Model, ModelItemID }, callback, asyncState);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000088DD File Offset: 0x00006ADD
		public string[] EndGetModelItemPermissions(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000088ED File Offset: 0x00006AED
		public void GetModelItemPermissionsAsync(string Model, string ModelItemID)
		{
			this.GetModelItemPermissionsAsync(Model, ModelItemID, null);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000088F8 File Offset: 0x00006AF8
		public void GetModelItemPermissionsAsync(string Model, string ModelItemID, object userState)
		{
			if (this.GetModelItemPermissionsOperationCompleted == null)
			{
				this.GetModelItemPermissionsOperationCompleted = new SendOrPostCallback(this.OnGetModelItemPermissionsOperationCompleted);
			}
			base.InvokeAsync("GetModelItemPermissions", new object[] { Model, ModelItemID }, this.GetModelItemPermissionsOperationCompleted, userState);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008934 File Offset: 0x00006B34
		private void OnGetModelItemPermissionsOperationCompleted(object arg)
		{
			if (this.GetModelItemPermissionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetModelItemPermissionsCompleted(this, new GetModelItemPermissionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00008979 File Offset: 0x00006B79
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetModelItemPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetModelItemPolicies(string Model, string ModelItemID, Policy[] Policies)
		{
			base.Invoke("SetModelItemPolicies", new object[] { Model, ModelItemID, Policies });
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008999 File Offset: 0x00006B99
		public IAsyncResult BeginSetModelItemPolicies(string Model, string ModelItemID, Policy[] Policies, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetModelItemPolicies", new object[] { Model, ModelItemID, Policies }, callback, asyncState);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000089BC File Offset: 0x00006BBC
		public void EndSetModelItemPolicies(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000089C6 File Offset: 0x00006BC6
		public void SetModelItemPoliciesAsync(string Model, string ModelItemID, Policy[] Policies)
		{
			this.SetModelItemPoliciesAsync(Model, ModelItemID, Policies, null);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000089D4 File Offset: 0x00006BD4
		public void SetModelItemPoliciesAsync(string Model, string ModelItemID, Policy[] Policies, object userState)
		{
			if (this.SetModelItemPoliciesOperationCompleted == null)
			{
				this.SetModelItemPoliciesOperationCompleted = new SendOrPostCallback(this.OnSetModelItemPoliciesOperationCompleted);
			}
			base.InvokeAsync("SetModelItemPolicies", new object[] { Model, ModelItemID, Policies }, this.SetModelItemPoliciesOperationCompleted, userState);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00008A20 File Offset: 0x00006C20
		private void OnSetModelItemPoliciesOperationCompleted(object arg)
		{
			if (this.SetModelItemPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetModelItemPoliciesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008A60 File Offset: 0x00006C60
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetModelItemPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Policies")]
		public Policy[] GetModelItemPolicies(string Model, string ModelItemID, out bool InheritParent)
		{
			object[] array = base.Invoke("GetModelItemPolicies", new object[] { Model, ModelItemID });
			InheritParent = (bool)array[1];
			return (Policy[])array[0];
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008A99 File Offset: 0x00006C99
		public IAsyncResult BeginGetModelItemPolicies(string Model, string ModelItemID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetModelItemPolicies", new object[] { Model, ModelItemID }, callback, asyncState);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008AB8 File Offset: 0x00006CB8
		public Policy[] EndGetModelItemPolicies(IAsyncResult asyncResult, out bool InheritParent)
		{
			object[] array = base.EndInvoke(asyncResult);
			InheritParent = (bool)array[1];
			return (Policy[])array[0];
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00008ADF File Offset: 0x00006CDF
		public void GetModelItemPoliciesAsync(string Model, string ModelItemID)
		{
			this.GetModelItemPoliciesAsync(Model, ModelItemID, null);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008AEA File Offset: 0x00006CEA
		public void GetModelItemPoliciesAsync(string Model, string ModelItemID, object userState)
		{
			if (this.GetModelItemPoliciesOperationCompleted == null)
			{
				this.GetModelItemPoliciesOperationCompleted = new SendOrPostCallback(this.OnGetModelItemPoliciesOperationCompleted);
			}
			base.InvokeAsync("GetModelItemPolicies", new object[] { Model, ModelItemID }, this.GetModelItemPoliciesOperationCompleted, userState);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00008B28 File Offset: 0x00006D28
		private void OnGetModelItemPoliciesOperationCompleted(object arg)
		{
			if (this.GetModelItemPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetModelItemPoliciesCompleted(this, new GetModelItemPoliciesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008B6D File Offset: 0x00006D6D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetUserModel", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Definition", DataType = "base64Binary")]
		public byte[] GetUserModel(string Model, string Perspective)
		{
			return (byte[])base.Invoke("GetUserModel", new object[] { Model, Perspective })[0];
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008B8F File Offset: 0x00006D8F
		public IAsyncResult BeginGetUserModel(string Model, string Perspective, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserModel", new object[] { Model, Perspective }, callback, asyncState);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00008BAD File Offset: 0x00006DAD
		public byte[] EndGetUserModel(IAsyncResult asyncResult)
		{
			return (byte[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008BBD File Offset: 0x00006DBD
		public void GetUserModelAsync(string Model, string Perspective)
		{
			this.GetUserModelAsync(Model, Perspective, null);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00008BC8 File Offset: 0x00006DC8
		public void GetUserModelAsync(string Model, string Perspective, object userState)
		{
			if (this.GetUserModelOperationCompleted == null)
			{
				this.GetUserModelOperationCompleted = new SendOrPostCallback(this.OnGetUserModelOperationCompleted);
			}
			base.InvokeAsync("GetUserModel", new object[] { Model, Perspective }, this.GetUserModelOperationCompleted, userState);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00008C04 File Offset: 0x00006E04
		private void OnGetUserModelOperationCompleted(object arg)
		{
			if (this.GetUserModelCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserModelCompleted(this, new GetUserModelCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00008C49 File Offset: 0x00006E49
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/InheritModelItemParentSecurity", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void InheritModelItemParentSecurity(string Model, string ModelItemID)
		{
			base.Invoke("InheritModelItemParentSecurity", new object[] { Model, ModelItemID });
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00008C65 File Offset: 0x00006E65
		public IAsyncResult BeginInheritModelItemParentSecurity(string Model, string ModelItemID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("InheritModelItemParentSecurity", new object[] { Model, ModelItemID }, callback, asyncState);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008C83 File Offset: 0x00006E83
		public void EndInheritModelItemParentSecurity(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00008C8D File Offset: 0x00006E8D
		public void InheritModelItemParentSecurityAsync(string Model, string ModelItemID)
		{
			this.InheritModelItemParentSecurityAsync(Model, ModelItemID, null);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008C98 File Offset: 0x00006E98
		public void InheritModelItemParentSecurityAsync(string Model, string ModelItemID, object userState)
		{
			if (this.InheritModelItemParentSecurityOperationCompleted == null)
			{
				this.InheritModelItemParentSecurityOperationCompleted = new SendOrPostCallback(this.OnInheritModelItemParentSecurityOperationCompleted);
			}
			base.InvokeAsync("InheritModelItemParentSecurity", new object[] { Model, ModelItemID }, this.InheritModelItemParentSecurityOperationCompleted, userState);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00008CD4 File Offset: 0x00006ED4
		private void OnInheritModelItemParentSecurityOperationCompleted(object arg)
		{
			if (this.InheritModelItemParentSecurityCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.InheritModelItemParentSecurityCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00008D13 File Offset: 0x00006F13
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetModelDrillthroughReports", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetModelDrillthroughReports(string Model, string ModelItemID, ModelDrillthroughReport[] Reports)
		{
			base.Invoke("SetModelDrillthroughReports", new object[] { Model, ModelItemID, Reports });
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00008D33 File Offset: 0x00006F33
		public IAsyncResult BeginSetModelDrillthroughReports(string Model, string ModelItemID, ModelDrillthroughReport[] Reports, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetModelDrillthroughReports", new object[] { Model, ModelItemID, Reports }, callback, asyncState);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00008D56 File Offset: 0x00006F56
		public void EndSetModelDrillthroughReports(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00008D60 File Offset: 0x00006F60
		public void SetModelDrillthroughReportsAsync(string Model, string ModelItemID, ModelDrillthroughReport[] Reports)
		{
			this.SetModelDrillthroughReportsAsync(Model, ModelItemID, Reports, null);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00008D6C File Offset: 0x00006F6C
		public void SetModelDrillthroughReportsAsync(string Model, string ModelItemID, ModelDrillthroughReport[] Reports, object userState)
		{
			if (this.SetModelDrillthroughReportsOperationCompleted == null)
			{
				this.SetModelDrillthroughReportsOperationCompleted = new SendOrPostCallback(this.OnSetModelDrillthroughReportsOperationCompleted);
			}
			base.InvokeAsync("SetModelDrillthroughReports", new object[] { Model, ModelItemID, Reports }, this.SetModelDrillthroughReportsOperationCompleted, userState);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00008DB8 File Offset: 0x00006FB8
		private void OnSetModelDrillthroughReportsOperationCompleted(object arg)
		{
			if (this.SetModelDrillthroughReportsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetModelDrillthroughReportsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00008DF7 File Offset: 0x00006FF7
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListModelDrillthroughReports", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Reports")]
		public ModelDrillthroughReport[] ListModelDrillthroughReports(string Model, string ModelItemID)
		{
			return (ModelDrillthroughReport[])base.Invoke("ListModelDrillthroughReports", new object[] { Model, ModelItemID })[0];
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008E19 File Offset: 0x00007019
		public IAsyncResult BeginListModelDrillthroughReports(string Model, string ModelItemID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListModelDrillthroughReports", new object[] { Model, ModelItemID }, callback, asyncState);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00008E37 File Offset: 0x00007037
		public ModelDrillthroughReport[] EndListModelDrillthroughReports(IAsyncResult asyncResult)
		{
			return (ModelDrillthroughReport[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00008E47 File Offset: 0x00007047
		public void ListModelDrillthroughReportsAsync(string Model, string ModelItemID)
		{
			this.ListModelDrillthroughReportsAsync(Model, ModelItemID, null);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00008E52 File Offset: 0x00007052
		public void ListModelDrillthroughReportsAsync(string Model, string ModelItemID, object userState)
		{
			if (this.ListModelDrillthroughReportsOperationCompleted == null)
			{
				this.ListModelDrillthroughReportsOperationCompleted = new SendOrPostCallback(this.OnListModelDrillthroughReportsOperationCompleted);
			}
			base.InvokeAsync("ListModelDrillthroughReports", new object[] { Model, ModelItemID }, this.ListModelDrillthroughReportsOperationCompleted, userState);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00008E90 File Offset: 0x00007090
		private void OnListModelDrillthroughReportsOperationCompleted(object arg)
		{
			if (this.ListModelDrillthroughReportsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListModelDrillthroughReportsCompleted(this, new ListModelDrillthroughReportsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008ED5 File Offset: 0x000070D5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListModelItemChildren", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ModelItems")]
		public ModelItem[] ListModelItemChildren(string Model, string ModelItemID, bool Recursive)
		{
			return (ModelItem[])base.Invoke("ListModelItemChildren", new object[] { Model, ModelItemID, Recursive })[0];
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008F00 File Offset: 0x00007100
		public IAsyncResult BeginListModelItemChildren(string Model, string ModelItemID, bool Recursive, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListModelItemChildren", new object[] { Model, ModelItemID, Recursive }, callback, asyncState);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008F28 File Offset: 0x00007128
		public ModelItem[] EndListModelItemChildren(IAsyncResult asyncResult)
		{
			return (ModelItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008F38 File Offset: 0x00007138
		public void ListModelItemChildrenAsync(string Model, string ModelItemID, bool Recursive)
		{
			this.ListModelItemChildrenAsync(Model, ModelItemID, Recursive, null);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008F44 File Offset: 0x00007144
		public void ListModelItemChildrenAsync(string Model, string ModelItemID, bool Recursive, object userState)
		{
			if (this.ListModelItemChildrenOperationCompleted == null)
			{
				this.ListModelItemChildrenOperationCompleted = new SendOrPostCallback(this.OnListModelItemChildrenOperationCompleted);
			}
			base.InvokeAsync("ListModelItemChildren", new object[] { Model, ModelItemID, Recursive }, this.ListModelItemChildrenOperationCompleted, userState);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00008F98 File Offset: 0x00007198
		private void OnListModelItemChildrenOperationCompleted(object arg)
		{
			if (this.ListModelItemChildrenCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListModelItemChildrenCompleted(this, new ListModelItemChildrenCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008FDD File Offset: 0x000071DD
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListModelItemTypes", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListModelItemTypes()
		{
			return (string[])base.Invoke("ListModelItemTypes", new object[0])[0];
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008FF7 File Offset: 0x000071F7
		public IAsyncResult BeginListModelItemTypes(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListModelItemTypes", new object[0], callback, asyncState);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000900C File Offset: 0x0000720C
		public string[] EndListModelItemTypes(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000901C File Offset: 0x0000721C
		public void ListModelItemTypesAsync()
		{
			this.ListModelItemTypesAsync(null);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009025 File Offset: 0x00007225
		public void ListModelItemTypesAsync(object userState)
		{
			if (this.ListModelItemTypesOperationCompleted == null)
			{
				this.ListModelItemTypesOperationCompleted = new SendOrPostCallback(this.OnListModelItemTypesOperationCompleted);
			}
			base.InvokeAsync("ListModelItemTypes", new object[0], this.ListModelItemTypesOperationCompleted, userState);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000905C File Offset: 0x0000725C
		private void OnListModelItemTypesOperationCompleted(object arg)
		{
			if (this.ListModelItemTypesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListModelItemTypesCompleted(this, new ListModelItemTypesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000090A1 File Offset: 0x000072A1
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListModelPerspectives", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ModelCatalogItems")]
		public ModelCatalogItem[] ListModelPerspectives(string Model)
		{
			return (ModelCatalogItem[])base.Invoke("ListModelPerspectives", new object[] { Model })[0];
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000090BF File Offset: 0x000072BF
		public IAsyncResult BeginListModelPerspectives(string Model, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListModelPerspectives", new object[] { Model }, callback, asyncState);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000090D8 File Offset: 0x000072D8
		public ModelCatalogItem[] EndListModelPerspectives(IAsyncResult asyncResult)
		{
			return (ModelCatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000090E8 File Offset: 0x000072E8
		public void ListModelPerspectivesAsync(string Model)
		{
			this.ListModelPerspectivesAsync(Model, null);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000090F2 File Offset: 0x000072F2
		public void ListModelPerspectivesAsync(string Model, object userState)
		{
			if (this.ListModelPerspectivesOperationCompleted == null)
			{
				this.ListModelPerspectivesOperationCompleted = new SendOrPostCallback(this.OnListModelPerspectivesOperationCompleted);
			}
			base.InvokeAsync("ListModelPerspectives", new object[] { Model }, this.ListModelPerspectivesOperationCompleted, userState);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000912C File Offset: 0x0000732C
		private void OnListModelPerspectivesOperationCompleted(object arg)
		{
			if (this.ListModelPerspectivesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListModelPerspectivesCompleted(this, new ListModelPerspectivesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00009171 File Offset: 0x00007371
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/RegenerateModel", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Warnings")]
		public Warning[] RegenerateModel(string Model)
		{
			return (Warning[])base.Invoke("RegenerateModel", new object[] { Model })[0];
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000918F File Offset: 0x0000738F
		public IAsyncResult BeginRegenerateModel(string Model, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RegenerateModel", new object[] { Model }, callback, asyncState);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000091A8 File Offset: 0x000073A8
		public Warning[] EndRegenerateModel(IAsyncResult asyncResult)
		{
			return (Warning[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000091B8 File Offset: 0x000073B8
		public void RegenerateModelAsync(string Model)
		{
			this.RegenerateModelAsync(Model, null);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000091C2 File Offset: 0x000073C2
		public void RegenerateModelAsync(string Model, object userState)
		{
			if (this.RegenerateModelOperationCompleted == null)
			{
				this.RegenerateModelOperationCompleted = new SendOrPostCallback(this.OnRegenerateModelOperationCompleted);
			}
			base.InvokeAsync("RegenerateModel", new object[] { Model }, this.RegenerateModelOperationCompleted, userState);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000091FC File Offset: 0x000073FC
		private void OnRegenerateModelOperationCompleted(object arg)
		{
			if (this.RegenerateModelCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RegenerateModelCompleted(this, new RegenerateModelCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009241 File Offset: 0x00007441
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/RemoveAllModelItemPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void RemoveAllModelItemPolicies(string Model)
		{
			base.Invoke("RemoveAllModelItemPolicies", new object[] { Model });
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009259 File Offset: 0x00007459
		public IAsyncResult BeginRemoveAllModelItemPolicies(string Model, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveAllModelItemPolicies", new object[] { Model }, callback, asyncState);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00009272 File Offset: 0x00007472
		public void EndRemoveAllModelItemPolicies(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000927C File Offset: 0x0000747C
		public void RemoveAllModelItemPoliciesAsync(string Model)
		{
			this.RemoveAllModelItemPoliciesAsync(Model, null);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00009286 File Offset: 0x00007486
		public void RemoveAllModelItemPoliciesAsync(string Model, object userState)
		{
			if (this.RemoveAllModelItemPoliciesOperationCompleted == null)
			{
				this.RemoveAllModelItemPoliciesOperationCompleted = new SendOrPostCallback(this.OnRemoveAllModelItemPoliciesOperationCompleted);
			}
			base.InvokeAsync("RemoveAllModelItemPolicies", new object[] { Model }, this.RemoveAllModelItemPoliciesOperationCompleted, userState);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000092C0 File Offset: 0x000074C0
		private void OnRemoveAllModelItemPoliciesOperationCompleted(object arg)
		{
			if (this.RemoveAllModelItemPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveAllModelItemPoliciesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000092FF File Offset: 0x000074FF
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/CreateSchedule", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ScheduleID")]
		public string CreateSchedule(string Name, ScheduleDefinition ScheduleDefinition, string SiteUrl)
		{
			return (string)base.Invoke("CreateSchedule", new object[] { Name, ScheduleDefinition, SiteUrl })[0];
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009325 File Offset: 0x00007525
		public IAsyncResult BeginCreateSchedule(string Name, ScheduleDefinition ScheduleDefinition, string SiteUrl, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateSchedule", new object[] { Name, ScheduleDefinition, SiteUrl }, callback, asyncState);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00009348 File Offset: 0x00007548
		public string EndCreateSchedule(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009358 File Offset: 0x00007558
		public void CreateScheduleAsync(string Name, ScheduleDefinition ScheduleDefinition, string SiteUrl)
		{
			this.CreateScheduleAsync(Name, ScheduleDefinition, SiteUrl, null);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009364 File Offset: 0x00007564
		public void CreateScheduleAsync(string Name, ScheduleDefinition ScheduleDefinition, string SiteUrl, object userState)
		{
			if (this.CreateScheduleOperationCompleted == null)
			{
				this.CreateScheduleOperationCompleted = new SendOrPostCallback(this.OnCreateScheduleOperationCompleted);
			}
			base.InvokeAsync("CreateSchedule", new object[] { Name, ScheduleDefinition, SiteUrl }, this.CreateScheduleOperationCompleted, userState);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000093B0 File Offset: 0x000075B0
		private void OnCreateScheduleOperationCompleted(object arg)
		{
			if (this.CreateScheduleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateScheduleCompleted(this, new CreateScheduleCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000093F5 File Offset: 0x000075F5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/DeleteSchedule", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteSchedule(string ScheduleID)
		{
			base.Invoke("DeleteSchedule", new object[] { ScheduleID });
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000940D File Offset: 0x0000760D
		public IAsyncResult BeginDeleteSchedule(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteSchedule", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009426 File Offset: 0x00007626
		public void EndDeleteSchedule(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009430 File Offset: 0x00007630
		public void DeleteScheduleAsync(string ScheduleID)
		{
			this.DeleteScheduleAsync(ScheduleID, null);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000943A File Offset: 0x0000763A
		public void DeleteScheduleAsync(string ScheduleID, object userState)
		{
			if (this.DeleteScheduleOperationCompleted == null)
			{
				this.DeleteScheduleOperationCompleted = new SendOrPostCallback(this.OnDeleteScheduleOperationCompleted);
			}
			base.InvokeAsync("DeleteSchedule", new object[] { ScheduleID }, this.DeleteScheduleOperationCompleted, userState);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00009474 File Offset: 0x00007674
		private void OnDeleteScheduleOperationCompleted(object arg)
		{
			if (this.DeleteScheduleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteScheduleCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000094B3 File Offset: 0x000076B3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListSchedules", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Schedules")]
		public Schedule[] ListSchedules(string SiteUrl)
		{
			return (Schedule[])base.Invoke("ListSchedules", new object[] { SiteUrl })[0];
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000094D1 File Offset: 0x000076D1
		public IAsyncResult BeginListSchedules(string SiteUrl, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListSchedules", new object[] { SiteUrl }, callback, asyncState);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000094EA File Offset: 0x000076EA
		public Schedule[] EndListSchedules(IAsyncResult asyncResult)
		{
			return (Schedule[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000094FA File Offset: 0x000076FA
		public void ListSchedulesAsync(string SiteUrl)
		{
			this.ListSchedulesAsync(SiteUrl, null);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00009504 File Offset: 0x00007704
		public void ListSchedulesAsync(string SiteUrl, object userState)
		{
			if (this.ListSchedulesOperationCompleted == null)
			{
				this.ListSchedulesOperationCompleted = new SendOrPostCallback(this.OnListSchedulesOperationCompleted);
			}
			base.InvokeAsync("ListSchedules", new object[] { SiteUrl }, this.ListSchedulesOperationCompleted, userState);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000953C File Offset: 0x0000773C
		private void OnListSchedulesOperationCompleted(object arg)
		{
			if (this.ListSchedulesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListSchedulesCompleted(this, new ListSchedulesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009581 File Offset: 0x00007781
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetScheduleProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Schedule")]
		public Schedule GetScheduleProperties(string ScheduleID)
		{
			return (Schedule)base.Invoke("GetScheduleProperties", new object[] { ScheduleID })[0];
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000959F File Offset: 0x0000779F
		public IAsyncResult BeginGetScheduleProperties(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetScheduleProperties", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000095B8 File Offset: 0x000077B8
		public Schedule EndGetScheduleProperties(IAsyncResult asyncResult)
		{
			return (Schedule)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000095C8 File Offset: 0x000077C8
		public void GetSchedulePropertiesAsync(string ScheduleID)
		{
			this.GetSchedulePropertiesAsync(ScheduleID, null);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000095D2 File Offset: 0x000077D2
		public void GetSchedulePropertiesAsync(string ScheduleID, object userState)
		{
			if (this.GetSchedulePropertiesOperationCompleted == null)
			{
				this.GetSchedulePropertiesOperationCompleted = new SendOrPostCallback(this.OnGetSchedulePropertiesOperationCompleted);
			}
			base.InvokeAsync("GetScheduleProperties", new object[] { ScheduleID }, this.GetSchedulePropertiesOperationCompleted, userState);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000960C File Offset: 0x0000780C
		private void OnGetSchedulePropertiesOperationCompleted(object arg)
		{
			if (this.GetSchedulePropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSchedulePropertiesCompleted(this, new GetSchedulePropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00009651 File Offset: 0x00007851
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListScheduleStates", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListScheduleStates()
		{
			return (string[])base.Invoke("ListScheduleStates", new object[0])[0];
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000966B File Offset: 0x0000786B
		public IAsyncResult BeginListScheduleStates(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListScheduleStates", new object[0], callback, asyncState);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00009680 File Offset: 0x00007880
		public string[] EndListScheduleStates(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00009690 File Offset: 0x00007890
		public void ListScheduleStatesAsync()
		{
			this.ListScheduleStatesAsync(null);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00009699 File Offset: 0x00007899
		public void ListScheduleStatesAsync(object userState)
		{
			if (this.ListScheduleStatesOperationCompleted == null)
			{
				this.ListScheduleStatesOperationCompleted = new SendOrPostCallback(this.OnListScheduleStatesOperationCompleted);
			}
			base.InvokeAsync("ListScheduleStates", new object[0], this.ListScheduleStatesOperationCompleted, userState);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000096D0 File Offset: 0x000078D0
		private void OnListScheduleStatesOperationCompleted(object arg)
		{
			if (this.ListScheduleStatesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListScheduleStatesCompleted(this, new ListScheduleStatesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00009715 File Offset: 0x00007915
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/PauseSchedule", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void PauseSchedule(string ScheduleID)
		{
			base.Invoke("PauseSchedule", new object[] { ScheduleID });
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000972D File Offset: 0x0000792D
		public IAsyncResult BeginPauseSchedule(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("PauseSchedule", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00009746 File Offset: 0x00007946
		public void EndPauseSchedule(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00009750 File Offset: 0x00007950
		public void PauseScheduleAsync(string ScheduleID)
		{
			this.PauseScheduleAsync(ScheduleID, null);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000975A File Offset: 0x0000795A
		public void PauseScheduleAsync(string ScheduleID, object userState)
		{
			if (this.PauseScheduleOperationCompleted == null)
			{
				this.PauseScheduleOperationCompleted = new SendOrPostCallback(this.OnPauseScheduleOperationCompleted);
			}
			base.InvokeAsync("PauseSchedule", new object[] { ScheduleID }, this.PauseScheduleOperationCompleted, userState);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00009794 File Offset: 0x00007994
		private void OnPauseScheduleOperationCompleted(object arg)
		{
			if (this.PauseScheduleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.PauseScheduleCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000097D3 File Offset: 0x000079D3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ResumeSchedule", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void ResumeSchedule(string ScheduleID)
		{
			base.Invoke("ResumeSchedule", new object[] { ScheduleID });
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000097EB File Offset: 0x000079EB
		public IAsyncResult BeginResumeSchedule(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ResumeSchedule", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00009804 File Offset: 0x00007A04
		public void EndResumeSchedule(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000980E File Offset: 0x00007A0E
		public void ResumeScheduleAsync(string ScheduleID)
		{
			this.ResumeScheduleAsync(ScheduleID, null);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00009818 File Offset: 0x00007A18
		public void ResumeScheduleAsync(string ScheduleID, object userState)
		{
			if (this.ResumeScheduleOperationCompleted == null)
			{
				this.ResumeScheduleOperationCompleted = new SendOrPostCallback(this.OnResumeScheduleOperationCompleted);
			}
			base.InvokeAsync("ResumeSchedule", new object[] { ScheduleID }, this.ResumeScheduleOperationCompleted, userState);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00009850 File Offset: 0x00007A50
		private void OnResumeScheduleOperationCompleted(object arg)
		{
			if (this.ResumeScheduleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ResumeScheduleCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000988F File Offset: 0x00007A8F
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetScheduleProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetScheduleProperties(string Name, string ScheduleID, ScheduleDefinition ScheduleDefinition)
		{
			base.Invoke("SetScheduleProperties", new object[] { Name, ScheduleID, ScheduleDefinition });
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000098AF File Offset: 0x00007AAF
		public IAsyncResult BeginSetScheduleProperties(string Name, string ScheduleID, ScheduleDefinition ScheduleDefinition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetScheduleProperties", new object[] { Name, ScheduleID, ScheduleDefinition }, callback, asyncState);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000098D2 File Offset: 0x00007AD2
		public void EndSetScheduleProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000098DC File Offset: 0x00007ADC
		public void SetSchedulePropertiesAsync(string Name, string ScheduleID, ScheduleDefinition ScheduleDefinition)
		{
			this.SetSchedulePropertiesAsync(Name, ScheduleID, ScheduleDefinition, null);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000098E8 File Offset: 0x00007AE8
		public void SetSchedulePropertiesAsync(string Name, string ScheduleID, ScheduleDefinition ScheduleDefinition, object userState)
		{
			if (this.SetSchedulePropertiesOperationCompleted == null)
			{
				this.SetSchedulePropertiesOperationCompleted = new SendOrPostCallback(this.OnSetSchedulePropertiesOperationCompleted);
			}
			base.InvokeAsync("SetScheduleProperties", new object[] { Name, ScheduleID, ScheduleDefinition }, this.SetSchedulePropertiesOperationCompleted, userState);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00009934 File Offset: 0x00007B34
		private void OnSetSchedulePropertiesOperationCompleted(object arg)
		{
			if (this.SetSchedulePropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetSchedulePropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00009973 File Offset: 0x00007B73
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListScheduledItems", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Items")]
		public CatalogItem[] ListScheduledItems(string ScheduleID)
		{
			return (CatalogItem[])base.Invoke("ListScheduledItems", new object[] { ScheduleID })[0];
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00009991 File Offset: 0x00007B91
		public IAsyncResult BeginListScheduledItems(string ScheduleID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListScheduledItems", new object[] { ScheduleID }, callback, asyncState);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000099AA File Offset: 0x00007BAA
		public CatalogItem[] EndListScheduledItems(IAsyncResult asyncResult)
		{
			return (CatalogItem[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000099BA File Offset: 0x00007BBA
		public void ListScheduledItemsAsync(string ScheduleID)
		{
			this.ListScheduledItemsAsync(ScheduleID, null);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000099C4 File Offset: 0x00007BC4
		public void ListScheduledItemsAsync(string ScheduleID, object userState)
		{
			if (this.ListScheduledItemsOperationCompleted == null)
			{
				this.ListScheduledItemsOperationCompleted = new SendOrPostCallback(this.OnListScheduledItemsOperationCompleted);
			}
			base.InvokeAsync("ListScheduledItems", new object[] { ScheduleID }, this.ListScheduledItemsOperationCompleted, userState);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000099FC File Offset: 0x00007BFC
		private void OnListScheduledItemsOperationCompleted(object arg)
		{
			if (this.ListScheduledItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListScheduledItemsCompleted(this, new ListScheduledItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00009A41 File Offset: 0x00007C41
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetItemParameters", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetItemParameters(string ItemPath, ItemParameter[] Parameters)
		{
			base.Invoke("SetItemParameters", new object[] { ItemPath, Parameters });
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00009A5D File Offset: 0x00007C5D
		public IAsyncResult BeginSetItemParameters(string ItemPath, ItemParameter[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetItemParameters", new object[] { ItemPath, Parameters }, callback, asyncState);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00009A7B File Offset: 0x00007C7B
		public void EndSetItemParameters(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00009A85 File Offset: 0x00007C85
		public void SetItemParametersAsync(string ItemPath, ItemParameter[] Parameters)
		{
			this.SetItemParametersAsync(ItemPath, Parameters, null);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00009A90 File Offset: 0x00007C90
		public void SetItemParametersAsync(string ItemPath, ItemParameter[] Parameters, object userState)
		{
			if (this.SetItemParametersOperationCompleted == null)
			{
				this.SetItemParametersOperationCompleted = new SendOrPostCallback(this.OnSetItemParametersOperationCompleted);
			}
			base.InvokeAsync("SetItemParameters", new object[] { ItemPath, Parameters }, this.SetItemParametersOperationCompleted, userState);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00009ACC File Offset: 0x00007CCC
		private void OnSetItemParametersOperationCompleted(object arg)
		{
			if (this.SetItemParametersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetItemParametersCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00009B0B File Offset: 0x00007D0B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetItemParameters", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Parameters")]
		public ItemParameter[] GetItemParameters(string ItemPath, string HistoryID, bool ForRendering, ParameterValue[] Values, DataSourceCredentials[] Credentials)
		{
			return (ItemParameter[])base.Invoke("GetItemParameters", new object[] { ItemPath, HistoryID, ForRendering, Values, Credentials })[0];
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00009B40 File Offset: 0x00007D40
		public IAsyncResult BeginGetItemParameters(string ItemPath, string HistoryID, bool ForRendering, ParameterValue[] Values, DataSourceCredentials[] Credentials, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemParameters", new object[] { ItemPath, HistoryID, ForRendering, Values, Credentials }, callback, asyncState);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00009B72 File Offset: 0x00007D72
		public ItemParameter[] EndGetItemParameters(IAsyncResult asyncResult)
		{
			return (ItemParameter[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00009B82 File Offset: 0x00007D82
		public void GetItemParametersAsync(string ItemPath, string HistoryID, bool ForRendering, ParameterValue[] Values, DataSourceCredentials[] Credentials)
		{
			this.GetItemParametersAsync(ItemPath, HistoryID, ForRendering, Values, Credentials, null);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00009B94 File Offset: 0x00007D94
		public void GetItemParametersAsync(string ItemPath, string HistoryID, bool ForRendering, ParameterValue[] Values, DataSourceCredentials[] Credentials, object userState)
		{
			if (this.GetItemParametersOperationCompleted == null)
			{
				this.GetItemParametersOperationCompleted = new SendOrPostCallback(this.OnGetItemParametersOperationCompleted);
			}
			base.InvokeAsync("GetItemParameters", new object[] { ItemPath, HistoryID, ForRendering, Values, Credentials }, this.GetItemParametersOperationCompleted, userState);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00009BF0 File Offset: 0x00007DF0
		private void OnGetItemParametersOperationCompleted(object arg)
		{
			if (this.GetItemParametersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemParametersCompleted(this, new GetItemParametersCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00009C35 File Offset: 0x00007E35
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListParameterTypes", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListParameterTypes()
		{
			return (string[])base.Invoke("ListParameterTypes", new object[0])[0];
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00009C4F File Offset: 0x00007E4F
		public IAsyncResult BeginListParameterTypes(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListParameterTypes", new object[0], callback, asyncState);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00009C64 File Offset: 0x00007E64
		public string[] EndListParameterTypes(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00009C74 File Offset: 0x00007E74
		public void ListParameterTypesAsync()
		{
			this.ListParameterTypesAsync(null);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00009C7D File Offset: 0x00007E7D
		public void ListParameterTypesAsync(object userState)
		{
			if (this.ListParameterTypesOperationCompleted == null)
			{
				this.ListParameterTypesOperationCompleted = new SendOrPostCallback(this.OnListParameterTypesOperationCompleted);
			}
			base.InvokeAsync("ListParameterTypes", new object[0], this.ListParameterTypesOperationCompleted, userState);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00009CB4 File Offset: 0x00007EB4
		private void OnListParameterTypesOperationCompleted(object arg)
		{
			if (this.ListParameterTypesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListParameterTypesCompleted(this, new ListParameterTypesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00009CF9 File Offset: 0x00007EF9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListParameterStates", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListParameterStates()
		{
			return (string[])base.Invoke("ListParameterStates", new object[0])[0];
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00009D13 File Offset: 0x00007F13
		public IAsyncResult BeginListParameterStates(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListParameterStates", new object[0], callback, asyncState);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00009D28 File Offset: 0x00007F28
		public string[] EndListParameterStates(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00009D38 File Offset: 0x00007F38
		public void ListParameterStatesAsync()
		{
			this.ListParameterStatesAsync(null);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00009D41 File Offset: 0x00007F41
		public void ListParameterStatesAsync(object userState)
		{
			if (this.ListParameterStatesOperationCompleted == null)
			{
				this.ListParameterStatesOperationCompleted = new SendOrPostCallback(this.OnListParameterStatesOperationCompleted);
			}
			base.InvokeAsync("ListParameterStates", new object[0], this.ListParameterStatesOperationCompleted, userState);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00009D78 File Offset: 0x00007F78
		private void OnListParameterStatesOperationCompleted(object arg)
		{
			if (this.ListParameterStatesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListParameterStatesCompleted(this, new ListParameterStatesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00009DC0 File Offset: 0x00007FC0
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/CreateReportEditSession", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("EditSessionID")]
		public string CreateReportEditSession(string Report, string Parent, [XmlElement(DataType = "base64Binary")] byte[] Definition, out Warning[] Warnings)
		{
			object[] array = base.Invoke("CreateReportEditSession", new object[] { Report, Parent, Definition });
			Warnings = (Warning[])array[1];
			return (string)array[0];
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00009DFE File Offset: 0x00007FFE
		public IAsyncResult BeginCreateReportEditSession(string Report, string Parent, byte[] Definition, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateReportEditSession", new object[] { Report, Parent, Definition }, callback, asyncState);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00009E24 File Offset: 0x00008024
		public string EndCreateReportEditSession(IAsyncResult asyncResult, out Warning[] Warnings)
		{
			object[] array = base.EndInvoke(asyncResult);
			Warnings = (Warning[])array[1];
			return (string)array[0];
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00009E4B File Offset: 0x0000804B
		public void CreateReportEditSessionAsync(string Report, string Parent, byte[] Definition)
		{
			this.CreateReportEditSessionAsync(Report, Parent, Definition, null);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00009E58 File Offset: 0x00008058
		public void CreateReportEditSessionAsync(string Report, string Parent, byte[] Definition, object userState)
		{
			if (this.CreateReportEditSessionOperationCompleted == null)
			{
				this.CreateReportEditSessionOperationCompleted = new SendOrPostCallback(this.OnCreateReportEditSessionOperationCompleted);
			}
			base.InvokeAsync("CreateReportEditSession", new object[] { Report, Parent, Definition }, this.CreateReportEditSessionOperationCompleted, userState);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00009EA4 File Offset: 0x000080A4
		private void OnCreateReportEditSessionOperationCompleted(object arg)
		{
			if (this.CreateReportEditSessionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateReportEditSessionCompleted(this, new CreateReportEditSessionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00009EE9 File Offset: 0x000080E9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/CreateLinkedItem", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void CreateLinkedItem(string ItemPath, string Parent, string Link, Property[] Properties)
		{
			base.Invoke("CreateLinkedItem", new object[] { ItemPath, Parent, Link, Properties });
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00009F0E File Offset: 0x0000810E
		public IAsyncResult BeginCreateLinkedItem(string ItemPath, string Parent, string Link, Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateLinkedItem", new object[] { ItemPath, Parent, Link, Properties }, callback, asyncState);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00009F36 File Offset: 0x00008136
		public void EndCreateLinkedItem(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00009F40 File Offset: 0x00008140
		public void CreateLinkedItemAsync(string ItemPath, string Parent, string Link, Property[] Properties)
		{
			this.CreateLinkedItemAsync(ItemPath, Parent, Link, Properties, null);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009F50 File Offset: 0x00008150
		public void CreateLinkedItemAsync(string ItemPath, string Parent, string Link, Property[] Properties, object userState)
		{
			if (this.CreateLinkedItemOperationCompleted == null)
			{
				this.CreateLinkedItemOperationCompleted = new SendOrPostCallback(this.OnCreateLinkedItemOperationCompleted);
			}
			base.InvokeAsync("CreateLinkedItem", new object[] { ItemPath, Parent, Link, Properties }, this.CreateLinkedItemOperationCompleted, userState);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00009FA4 File Offset: 0x000081A4
		private void OnCreateLinkedItemOperationCompleted(object arg)
		{
			if (this.CreateLinkedItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateLinkedItemCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00009FE3 File Offset: 0x000081E3
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetItemLink", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetItemLink(string ItemPath, string Link)
		{
			base.Invoke("SetItemLink", new object[] { ItemPath, Link });
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00009FFF File Offset: 0x000081FF
		public IAsyncResult BeginSetItemLink(string ItemPath, string Link, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetItemLink", new object[] { ItemPath, Link }, callback, asyncState);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000A01D File Offset: 0x0000821D
		public void EndSetItemLink(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000A027 File Offset: 0x00008227
		public void SetItemLinkAsync(string ItemPath, string Link)
		{
			this.SetItemLinkAsync(ItemPath, Link, null);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000A032 File Offset: 0x00008232
		public void SetItemLinkAsync(string ItemPath, string Link, object userState)
		{
			if (this.SetItemLinkOperationCompleted == null)
			{
				this.SetItemLinkOperationCompleted = new SendOrPostCallback(this.OnSetItemLinkOperationCompleted);
			}
			base.InvokeAsync("SetItemLink", new object[] { ItemPath, Link }, this.SetItemLinkOperationCompleted, userState);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000A070 File Offset: 0x00008270
		private void OnSetItemLinkOperationCompleted(object arg)
		{
			if (this.SetItemLinkCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetItemLinkCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000A0AF File Offset: 0x000082AF
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetItemLink", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Link")]
		public string GetItemLink(string ItemPath)
		{
			return (string)base.Invoke("GetItemLink", new object[] { ItemPath })[0];
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000A0CD File Offset: 0x000082CD
		public IAsyncResult BeginGetItemLink(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemLink", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000A0E6 File Offset: 0x000082E6
		public string EndGetItemLink(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000A0F6 File Offset: 0x000082F6
		public void GetItemLinkAsync(string ItemPath)
		{
			this.GetItemLinkAsync(ItemPath, null);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000A100 File Offset: 0x00008300
		public void GetItemLinkAsync(string ItemPath, object userState)
		{
			if (this.GetItemLinkOperationCompleted == null)
			{
				this.GetItemLinkOperationCompleted = new SendOrPostCallback(this.OnGetItemLinkOperationCompleted);
			}
			base.InvokeAsync("GetItemLink", new object[] { ItemPath }, this.GetItemLinkOperationCompleted, userState);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000A138 File Offset: 0x00008338
		private void OnGetItemLinkOperationCompleted(object arg)
		{
			if (this.GetItemLinkCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemLinkCompleted(this, new GetItemLinkCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000A17D File Offset: 0x0000837D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListExecutionSettings", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListExecutionSettings()
		{
			return (string[])base.Invoke("ListExecutionSettings", new object[0])[0];
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000A197 File Offset: 0x00008397
		public IAsyncResult BeginListExecutionSettings(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListExecutionSettings", new object[0], callback, asyncState);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000A1AC File Offset: 0x000083AC
		public string[] EndListExecutionSettings(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000A1BC File Offset: 0x000083BC
		public void ListExecutionSettingsAsync()
		{
			this.ListExecutionSettingsAsync(null);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000A1C5 File Offset: 0x000083C5
		public void ListExecutionSettingsAsync(object userState)
		{
			if (this.ListExecutionSettingsOperationCompleted == null)
			{
				this.ListExecutionSettingsOperationCompleted = new SendOrPostCallback(this.OnListExecutionSettingsOperationCompleted);
			}
			base.InvokeAsync("ListExecutionSettings", new object[0], this.ListExecutionSettingsOperationCompleted, userState);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000A1FC File Offset: 0x000083FC
		private void OnListExecutionSettingsOperationCompleted(object arg)
		{
			if (this.ListExecutionSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListExecutionSettingsCompleted(this, new ListExecutionSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000A241 File Offset: 0x00008441
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetExecutionOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetExecutionOptions(string ItemPath, string ExecutionSetting, [XmlElement("NoSchedule", typeof(NoSchedule))] [XmlElement("ScheduleDefinition", typeof(ScheduleDefinition))] [XmlElement("ScheduleReference", typeof(ScheduleReference))] ScheduleDefinitionOrReference Item)
		{
			base.Invoke("SetExecutionOptions", new object[] { ItemPath, ExecutionSetting, Item });
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000A261 File Offset: 0x00008461
		public IAsyncResult BeginSetExecutionOptions(string ItemPath, string ExecutionSetting, ScheduleDefinitionOrReference Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetExecutionOptions", new object[] { ItemPath, ExecutionSetting, Item }, callback, asyncState);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000A284 File Offset: 0x00008484
		public void EndSetExecutionOptions(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000A28E File Offset: 0x0000848E
		public void SetExecutionOptionsAsync(string ItemPath, string ExecutionSetting, ScheduleDefinitionOrReference Item)
		{
			this.SetExecutionOptionsAsync(ItemPath, ExecutionSetting, Item, null);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000A29C File Offset: 0x0000849C
		public void SetExecutionOptionsAsync(string ItemPath, string ExecutionSetting, ScheduleDefinitionOrReference Item, object userState)
		{
			if (this.SetExecutionOptionsOperationCompleted == null)
			{
				this.SetExecutionOptionsOperationCompleted = new SendOrPostCallback(this.OnSetExecutionOptionsOperationCompleted);
			}
			base.InvokeAsync("SetExecutionOptions", new object[] { ItemPath, ExecutionSetting, Item }, this.SetExecutionOptionsOperationCompleted, userState);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000A2E8 File Offset: 0x000084E8
		private void OnSetExecutionOptionsOperationCompleted(object arg)
		{
			if (this.SetExecutionOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetExecutionOptionsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000A328 File Offset: 0x00008528
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetExecutionOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ExecutionSetting")]
		public string GetExecutionOptions(string ItemPath, [XmlElement("NoSchedule", typeof(NoSchedule))] [XmlElement("ScheduleDefinition", typeof(ScheduleDefinition))] [XmlElement("ScheduleReference", typeof(ScheduleReference))] out ScheduleDefinitionOrReference Item)
		{
			object[] array = base.Invoke("GetExecutionOptions", new object[] { ItemPath });
			Item = (ScheduleDefinitionOrReference)array[1];
			return (string)array[0];
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000A35D File Offset: 0x0000855D
		public IAsyncResult BeginGetExecutionOptions(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetExecutionOptions", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000A378 File Offset: 0x00008578
		public string EndGetExecutionOptions(IAsyncResult asyncResult, out ScheduleDefinitionOrReference Item)
		{
			object[] array = base.EndInvoke(asyncResult);
			Item = (ScheduleDefinitionOrReference)array[1];
			return (string)array[0];
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000A39F File Offset: 0x0000859F
		public void GetExecutionOptionsAsync(string ItemPath)
		{
			this.GetExecutionOptionsAsync(ItemPath, null);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000A3A9 File Offset: 0x000085A9
		public void GetExecutionOptionsAsync(string ItemPath, object userState)
		{
			if (this.GetExecutionOptionsOperationCompleted == null)
			{
				this.GetExecutionOptionsOperationCompleted = new SendOrPostCallback(this.OnGetExecutionOptionsOperationCompleted);
			}
			base.InvokeAsync("GetExecutionOptions", new object[] { ItemPath }, this.GetExecutionOptionsOperationCompleted, userState);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000A3E4 File Offset: 0x000085E4
		private void OnGetExecutionOptionsOperationCompleted(object arg)
		{
			if (this.GetExecutionOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetExecutionOptionsCompleted(this, new GetExecutionOptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000A429 File Offset: 0x00008629
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/UpdateItemExecutionSnapshot", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void UpdateItemExecutionSnapshot(string ItemPath)
		{
			base.Invoke("UpdateItemExecutionSnapshot", new object[] { ItemPath });
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000A441 File Offset: 0x00008641
		public IAsyncResult BeginUpdateItemExecutionSnapshot(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateItemExecutionSnapshot", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000A45A File Offset: 0x0000865A
		public void EndUpdateItemExecutionSnapshot(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000A464 File Offset: 0x00008664
		public void UpdateItemExecutionSnapshotAsync(string ItemPath)
		{
			this.UpdateItemExecutionSnapshotAsync(ItemPath, null);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000A46E File Offset: 0x0000866E
		public void UpdateItemExecutionSnapshotAsync(string ItemPath, object userState)
		{
			if (this.UpdateItemExecutionSnapshotOperationCompleted == null)
			{
				this.UpdateItemExecutionSnapshotOperationCompleted = new SendOrPostCallback(this.OnUpdateItemExecutionSnapshotOperationCompleted);
			}
			base.InvokeAsync("UpdateItemExecutionSnapshot", new object[] { ItemPath }, this.UpdateItemExecutionSnapshotOperationCompleted, userState);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000A4A8 File Offset: 0x000086A8
		private void OnUpdateItemExecutionSnapshotOperationCompleted(object arg)
		{
			if (this.UpdateItemExecutionSnapshotCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateItemExecutionSnapshotCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000A4E7 File Offset: 0x000086E7
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetCacheOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetCacheOptions(string ItemPath, bool CacheItem, [XmlElement("ScheduleExpiration", typeof(ScheduleExpiration))] [XmlElement("TimeExpiration", typeof(TimeExpiration))] ExpirationDefinition Item)
		{
			base.Invoke("SetCacheOptions", new object[] { ItemPath, CacheItem, Item });
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000A50C File Offset: 0x0000870C
		public IAsyncResult BeginSetCacheOptions(string ItemPath, bool CacheItem, ExpirationDefinition Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetCacheOptions", new object[] { ItemPath, CacheItem, Item }, callback, asyncState);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000A534 File Offset: 0x00008734
		public void EndSetCacheOptions(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000A53E File Offset: 0x0000873E
		public void SetCacheOptionsAsync(string ItemPath, bool CacheItem, ExpirationDefinition Item)
		{
			this.SetCacheOptionsAsync(ItemPath, CacheItem, Item, null);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000A54C File Offset: 0x0000874C
		public void SetCacheOptionsAsync(string ItemPath, bool CacheItem, ExpirationDefinition Item, object userState)
		{
			if (this.SetCacheOptionsOperationCompleted == null)
			{
				this.SetCacheOptionsOperationCompleted = new SendOrPostCallback(this.OnSetCacheOptionsOperationCompleted);
			}
			base.InvokeAsync("SetCacheOptions", new object[] { ItemPath, CacheItem, Item }, this.SetCacheOptionsOperationCompleted, userState);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000A5A0 File Offset: 0x000087A0
		private void OnSetCacheOptionsOperationCompleted(object arg)
		{
			if (this.SetCacheOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetCacheOptionsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000A5E0 File Offset: 0x000087E0
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetCacheOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("CacheItem")]
		public bool GetCacheOptions(string ItemPath, [XmlElement("ScheduleExpiration", typeof(ScheduleExpiration))] [XmlElement("TimeExpiration", typeof(TimeExpiration))] out ExpirationDefinition Item)
		{
			object[] array = base.Invoke("GetCacheOptions", new object[] { ItemPath });
			Item = (ExpirationDefinition)array[1];
			return (bool)array[0];
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000A615 File Offset: 0x00008815
		public IAsyncResult BeginGetCacheOptions(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCacheOptions", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000A630 File Offset: 0x00008830
		public bool EndGetCacheOptions(IAsyncResult asyncResult, out ExpirationDefinition Item)
		{
			object[] array = base.EndInvoke(asyncResult);
			Item = (ExpirationDefinition)array[1];
			return (bool)array[0];
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000A657 File Offset: 0x00008857
		public void GetCacheOptionsAsync(string ItemPath)
		{
			this.GetCacheOptionsAsync(ItemPath, null);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000A661 File Offset: 0x00008861
		public void GetCacheOptionsAsync(string ItemPath, object userState)
		{
			if (this.GetCacheOptionsOperationCompleted == null)
			{
				this.GetCacheOptionsOperationCompleted = new SendOrPostCallback(this.OnGetCacheOptionsOperationCompleted);
			}
			base.InvokeAsync("GetCacheOptions", new object[] { ItemPath }, this.GetCacheOptionsOperationCompleted, userState);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000A69C File Offset: 0x0000889C
		private void OnGetCacheOptionsOperationCompleted(object arg)
		{
			if (this.GetCacheOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCacheOptionsCompleted(this, new GetCacheOptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000A6E1 File Offset: 0x000088E1
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/FlushCache", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void FlushCache(string ItemPath)
		{
			base.Invoke("FlushCache", new object[] { ItemPath });
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000A6F9 File Offset: 0x000088F9
		public IAsyncResult BeginFlushCache(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FlushCache", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000A712 File Offset: 0x00008912
		public void EndFlushCache(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000A71C File Offset: 0x0000891C
		public void FlushCacheAsync(string ItemPath)
		{
			this.FlushCacheAsync(ItemPath, null);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000A726 File Offset: 0x00008926
		public void FlushCacheAsync(string ItemPath, object userState)
		{
			if (this.FlushCacheOperationCompleted == null)
			{
				this.FlushCacheOperationCompleted = new SendOrPostCallback(this.OnFlushCacheOperationCompleted);
			}
			base.InvokeAsync("FlushCache", new object[] { ItemPath }, this.FlushCacheOperationCompleted, userState);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000A760 File Offset: 0x00008960
		private void OnFlushCacheOperationCompleted(object arg)
		{
			if (this.FlushCacheCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FlushCacheCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000A7A0 File Offset: 0x000089A0
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/CreateItemHistorySnapshot", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("HistoryID")]
		public string CreateItemHistorySnapshot(string ItemPath, out Warning[] Warnings)
		{
			object[] array = base.Invoke("CreateItemHistorySnapshot", new object[] { ItemPath });
			Warnings = (Warning[])array[1];
			return (string)array[0];
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000A7D5 File Offset: 0x000089D5
		public IAsyncResult BeginCreateItemHistorySnapshot(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateItemHistorySnapshot", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000A7F0 File Offset: 0x000089F0
		public string EndCreateItemHistorySnapshot(IAsyncResult asyncResult, out Warning[] Warnings)
		{
			object[] array = base.EndInvoke(asyncResult);
			Warnings = (Warning[])array[1];
			return (string)array[0];
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000A817 File Offset: 0x00008A17
		public void CreateItemHistorySnapshotAsync(string ItemPath)
		{
			this.CreateItemHistorySnapshotAsync(ItemPath, null);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000A821 File Offset: 0x00008A21
		public void CreateItemHistorySnapshotAsync(string ItemPath, object userState)
		{
			if (this.CreateItemHistorySnapshotOperationCompleted == null)
			{
				this.CreateItemHistorySnapshotOperationCompleted = new SendOrPostCallback(this.OnCreateItemHistorySnapshotOperationCompleted);
			}
			base.InvokeAsync("CreateItemHistorySnapshot", new object[] { ItemPath }, this.CreateItemHistorySnapshotOperationCompleted, userState);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000A85C File Offset: 0x00008A5C
		private void OnCreateItemHistorySnapshotOperationCompleted(object arg)
		{
			if (this.CreateItemHistorySnapshotCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateItemHistorySnapshotCompleted(this, new CreateItemHistorySnapshotCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000A8A1 File Offset: 0x00008AA1
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/DeleteItemHistorySnapshot", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteItemHistorySnapshot(string ItemPath, string HistoryID)
		{
			base.Invoke("DeleteItemHistorySnapshot", new object[] { ItemPath, HistoryID });
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000A8BD File Offset: 0x00008ABD
		public IAsyncResult BeginDeleteItemHistorySnapshot(string ItemPath, string HistoryID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteItemHistorySnapshot", new object[] { ItemPath, HistoryID }, callback, asyncState);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000A8DB File Offset: 0x00008ADB
		public void EndDeleteItemHistorySnapshot(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000A8E5 File Offset: 0x00008AE5
		public void DeleteItemHistorySnapshotAsync(string ItemPath, string HistoryID)
		{
			this.DeleteItemHistorySnapshotAsync(ItemPath, HistoryID, null);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		public void DeleteItemHistorySnapshotAsync(string ItemPath, string HistoryID, object userState)
		{
			if (this.DeleteItemHistorySnapshotOperationCompleted == null)
			{
				this.DeleteItemHistorySnapshotOperationCompleted = new SendOrPostCallback(this.OnDeleteItemHistorySnapshotOperationCompleted);
			}
			base.InvokeAsync("DeleteItemHistorySnapshot", new object[] { ItemPath, HistoryID }, this.DeleteItemHistorySnapshotOperationCompleted, userState);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000A92C File Offset: 0x00008B2C
		private void OnDeleteItemHistorySnapshotOperationCompleted(object arg)
		{
			if (this.DeleteItemHistorySnapshotCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteItemHistorySnapshotCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000A96B File Offset: 0x00008B6B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetItemHistoryLimit", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetItemHistoryLimit(string ItemPath, bool UseSystem, int HistoryLimit)
		{
			base.Invoke("SetItemHistoryLimit", new object[] { ItemPath, UseSystem, HistoryLimit });
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000A995 File Offset: 0x00008B95
		public IAsyncResult BeginSetItemHistoryLimit(string ItemPath, bool UseSystem, int HistoryLimit, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetItemHistoryLimit", new object[] { ItemPath, UseSystem, HistoryLimit }, callback, asyncState);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000A9C2 File Offset: 0x00008BC2
		public void EndSetItemHistoryLimit(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000A9CC File Offset: 0x00008BCC
		public void SetItemHistoryLimitAsync(string ItemPath, bool UseSystem, int HistoryLimit)
		{
			this.SetItemHistoryLimitAsync(ItemPath, UseSystem, HistoryLimit, null);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000A9D8 File Offset: 0x00008BD8
		public void SetItemHistoryLimitAsync(string ItemPath, bool UseSystem, int HistoryLimit, object userState)
		{
			if (this.SetItemHistoryLimitOperationCompleted == null)
			{
				this.SetItemHistoryLimitOperationCompleted = new SendOrPostCallback(this.OnSetItemHistoryLimitOperationCompleted);
			}
			base.InvokeAsync("SetItemHistoryLimit", new object[] { ItemPath, UseSystem, HistoryLimit }, this.SetItemHistoryLimitOperationCompleted, userState);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000AA30 File Offset: 0x00008C30
		private void OnSetItemHistoryLimitOperationCompleted(object arg)
		{
			if (this.SetItemHistoryLimitCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetItemHistoryLimitCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000AA70 File Offset: 0x00008C70
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetItemHistoryLimit", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("HistoryLimit")]
		public int GetItemHistoryLimit(string ItemPath, out bool IsSystem, out int SystemLimit)
		{
			object[] array = base.Invoke("GetItemHistoryLimit", new object[] { ItemPath });
			IsSystem = (bool)array[1];
			SystemLimit = (int)array[2];
			return (int)array[0];
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000AAAF File Offset: 0x00008CAF
		public IAsyncResult BeginGetItemHistoryLimit(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemHistoryLimit", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		public int EndGetItemHistoryLimit(IAsyncResult asyncResult, out bool IsSystem, out int SystemLimit)
		{
			object[] array = base.EndInvoke(asyncResult);
			IsSystem = (bool)array[1];
			SystemLimit = (int)array[2];
			return (int)array[0];
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000AAF9 File Offset: 0x00008CF9
		public void GetItemHistoryLimitAsync(string ItemPath)
		{
			this.GetItemHistoryLimitAsync(ItemPath, null);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000AB03 File Offset: 0x00008D03
		public void GetItemHistoryLimitAsync(string ItemPath, object userState)
		{
			if (this.GetItemHistoryLimitOperationCompleted == null)
			{
				this.GetItemHistoryLimitOperationCompleted = new SendOrPostCallback(this.OnGetItemHistoryLimitOperationCompleted);
			}
			base.InvokeAsync("GetItemHistoryLimit", new object[] { ItemPath }, this.GetItemHistoryLimitOperationCompleted, userState);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000AB3C File Offset: 0x00008D3C
		private void OnGetItemHistoryLimitOperationCompleted(object arg)
		{
			if (this.GetItemHistoryLimitCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemHistoryLimitCompleted(this, new GetItemHistoryLimitCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000AB81 File Offset: 0x00008D81
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetItemHistoryOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetItemHistoryOptions(string ItemPath, bool EnableManualSnapshotCreation, bool KeepExecutionSnapshots, [XmlElement("NoSchedule", typeof(NoSchedule))] [XmlElement("ScheduleDefinition", typeof(ScheduleDefinition))] [XmlElement("ScheduleReference", typeof(ScheduleReference))] ScheduleDefinitionOrReference Item)
		{
			base.Invoke("SetItemHistoryOptions", new object[] { ItemPath, EnableManualSnapshotCreation, KeepExecutionSnapshots, Item });
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000ABB0 File Offset: 0x00008DB0
		public IAsyncResult BeginSetItemHistoryOptions(string ItemPath, bool EnableManualSnapshotCreation, bool KeepExecutionSnapshots, ScheduleDefinitionOrReference Item, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetItemHistoryOptions", new object[] { ItemPath, EnableManualSnapshotCreation, KeepExecutionSnapshots, Item }, callback, asyncState);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000ABE2 File Offset: 0x00008DE2
		public void EndSetItemHistoryOptions(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000ABEC File Offset: 0x00008DEC
		public void SetItemHistoryOptionsAsync(string ItemPath, bool EnableManualSnapshotCreation, bool KeepExecutionSnapshots, ScheduleDefinitionOrReference Item)
		{
			this.SetItemHistoryOptionsAsync(ItemPath, EnableManualSnapshotCreation, KeepExecutionSnapshots, Item, null);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000ABFC File Offset: 0x00008DFC
		public void SetItemHistoryOptionsAsync(string ItemPath, bool EnableManualSnapshotCreation, bool KeepExecutionSnapshots, ScheduleDefinitionOrReference Item, object userState)
		{
			if (this.SetItemHistoryOptionsOperationCompleted == null)
			{
				this.SetItemHistoryOptionsOperationCompleted = new SendOrPostCallback(this.OnSetItemHistoryOptionsOperationCompleted);
			}
			base.InvokeAsync("SetItemHistoryOptions", new object[] { ItemPath, EnableManualSnapshotCreation, KeepExecutionSnapshots, Item }, this.SetItemHistoryOptionsOperationCompleted, userState);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000AC58 File Offset: 0x00008E58
		private void OnSetItemHistoryOptionsOperationCompleted(object arg)
		{
			if (this.SetItemHistoryOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetItemHistoryOptionsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000AC98 File Offset: 0x00008E98
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetItemHistoryOptions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("EnableManualSnapshotCreation")]
		public bool GetItemHistoryOptions(string ItemPath, out bool KeepExecutionSnapshots, [XmlElement("NoSchedule", typeof(NoSchedule))] [XmlElement("ScheduleDefinition", typeof(ScheduleDefinition))] [XmlElement("ScheduleReference", typeof(ScheduleReference))] out ScheduleDefinitionOrReference Item)
		{
			object[] array = base.Invoke("GetItemHistoryOptions", new object[] { ItemPath });
			KeepExecutionSnapshots = (bool)array[1];
			Item = (ScheduleDefinitionOrReference)array[2];
			return (bool)array[0];
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000ACD7 File Offset: 0x00008ED7
		public IAsyncResult BeginGetItemHistoryOptions(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItemHistoryOptions", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000ACF0 File Offset: 0x00008EF0
		public bool EndGetItemHistoryOptions(IAsyncResult asyncResult, out bool KeepExecutionSnapshots, out ScheduleDefinitionOrReference Item)
		{
			object[] array = base.EndInvoke(asyncResult);
			KeepExecutionSnapshots = (bool)array[1];
			Item = (ScheduleDefinitionOrReference)array[2];
			return (bool)array[0];
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000AD21 File Offset: 0x00008F21
		public void GetItemHistoryOptionsAsync(string ItemPath)
		{
			this.GetItemHistoryOptionsAsync(ItemPath, null);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000AD2B File Offset: 0x00008F2B
		public void GetItemHistoryOptionsAsync(string ItemPath, object userState)
		{
			if (this.GetItemHistoryOptionsOperationCompleted == null)
			{
				this.GetItemHistoryOptionsOperationCompleted = new SendOrPostCallback(this.OnGetItemHistoryOptionsOperationCompleted);
			}
			base.InvokeAsync("GetItemHistoryOptions", new object[] { ItemPath }, this.GetItemHistoryOptionsOperationCompleted, userState);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000AD64 File Offset: 0x00008F64
		private void OnGetItemHistoryOptionsOperationCompleted(object arg)
		{
			if (this.GetItemHistoryOptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemHistoryOptionsCompleted(this, new GetItemHistoryOptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000ADA9 File Offset: 0x00008FA9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetReportServerConfigInfo", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("ServerConfigInfo")]
		public string GetReportServerConfigInfo(bool ScaleOut)
		{
			return (string)base.Invoke("GetReportServerConfigInfo", new object[] { ScaleOut })[0];
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000ADCC File Offset: 0x00008FCC
		public IAsyncResult BeginGetReportServerConfigInfo(bool ScaleOut, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetReportServerConfigInfo", new object[] { ScaleOut }, callback, asyncState);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000ADEA File Offset: 0x00008FEA
		public string EndGetReportServerConfigInfo(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000ADFA File Offset: 0x00008FFA
		public void GetReportServerConfigInfoAsync(bool ScaleOut)
		{
			this.GetReportServerConfigInfoAsync(ScaleOut, null);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000AE04 File Offset: 0x00009004
		public void GetReportServerConfigInfoAsync(bool ScaleOut, object userState)
		{
			if (this.GetReportServerConfigInfoOperationCompleted == null)
			{
				this.GetReportServerConfigInfoOperationCompleted = new SendOrPostCallback(this.OnGetReportServerConfigInfoOperationCompleted);
			}
			base.InvokeAsync("GetReportServerConfigInfo", new object[] { ScaleOut }, this.GetReportServerConfigInfoOperationCompleted, userState);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000AE44 File Offset: 0x00009044
		private void OnGetReportServerConfigInfoOperationCompleted(object arg)
		{
			if (this.GetReportServerConfigInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetReportServerConfigInfoCompleted(this, new GetReportServerConfigInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000AE89 File Offset: 0x00009089
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/IsSSLRequired", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool IsSSLRequired()
		{
			return (bool)base.Invoke("IsSSLRequired", new object[0])[0];
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000AEA3 File Offset: 0x000090A3
		public IAsyncResult BeginIsSSLRequired(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("IsSSLRequired", new object[0], callback, asyncState);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000AEB8 File Offset: 0x000090B8
		public bool EndIsSSLRequired(IAsyncResult asyncResult)
		{
			return (bool)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000AEC8 File Offset: 0x000090C8
		public void IsSSLRequiredAsync()
		{
			this.IsSSLRequiredAsync(null);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000AED1 File Offset: 0x000090D1
		public void IsSSLRequiredAsync(object userState)
		{
			if (this.IsSSLRequiredOperationCompleted == null)
			{
				this.IsSSLRequiredOperationCompleted = new SendOrPostCallback(this.OnIsSSLRequiredOperationCompleted);
			}
			base.InvokeAsync("IsSSLRequired", new object[0], this.IsSSLRequiredOperationCompleted, userState);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000AF08 File Offset: 0x00009108
		private void OnIsSSLRequiredOperationCompleted(object arg)
		{
			if (this.IsSSLRequiredCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.IsSSLRequiredCompleted(this, new IsSSLRequiredCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000AF4D File Offset: 0x0000914D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetSystemProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetSystemProperties(Property[] Properties)
		{
			base.Invoke("SetSystemProperties", new object[] { Properties });
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000AF65 File Offset: 0x00009165
		public IAsyncResult BeginSetSystemProperties(Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetSystemProperties", new object[] { Properties }, callback, asyncState);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000AF7E File Offset: 0x0000917E
		public void EndSetSystemProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000AF88 File Offset: 0x00009188
		public void SetSystemPropertiesAsync(Property[] Properties)
		{
			this.SetSystemPropertiesAsync(Properties, null);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000AF92 File Offset: 0x00009192
		public void SetSystemPropertiesAsync(Property[] Properties, object userState)
		{
			if (this.SetSystemPropertiesOperationCompleted == null)
			{
				this.SetSystemPropertiesOperationCompleted = new SendOrPostCallback(this.OnSetSystemPropertiesOperationCompleted);
			}
			base.InvokeAsync("SetSystemProperties", new object[] { Properties }, this.SetSystemPropertiesOperationCompleted, userState);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000AFCC File Offset: 0x000091CC
		private void OnSetSystemPropertiesOperationCompleted(object arg)
		{
			if (this.SetSystemPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetSystemPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000B00B File Offset: 0x0000920B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetSystemProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Values")]
		public Property[] GetSystemProperties(Property[] Properties)
		{
			return (Property[])base.Invoke("GetSystemProperties", new object[] { Properties })[0];
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000B029 File Offset: 0x00009229
		public IAsyncResult BeginGetSystemProperties(Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSystemProperties", new object[] { Properties }, callback, asyncState);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000B042 File Offset: 0x00009242
		public Property[] EndGetSystemProperties(IAsyncResult asyncResult)
		{
			return (Property[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000B052 File Offset: 0x00009252
		public void GetSystemPropertiesAsync(Property[] Properties)
		{
			this.GetSystemPropertiesAsync(Properties, null);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000B05C File Offset: 0x0000925C
		public void GetSystemPropertiesAsync(Property[] Properties, object userState)
		{
			if (this.GetSystemPropertiesOperationCompleted == null)
			{
				this.GetSystemPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetSystemPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetSystemProperties", new object[] { Properties }, this.GetSystemPropertiesOperationCompleted, userState);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000B094 File Offset: 0x00009294
		private void OnGetSystemPropertiesOperationCompleted(object arg)
		{
			if (this.GetSystemPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSystemPropertiesCompleted(this, new GetSystemPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000B0D9 File Offset: 0x000092D9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetUserSettings", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetUserSettings(Property[] Properties)
		{
			base.Invoke("SetUserSettings", new object[] { Properties });
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000B0F1 File Offset: 0x000092F1
		public IAsyncResult BeginSetUserSettings(Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetUserSettings", new object[] { Properties }, callback, asyncState);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000B10A File Offset: 0x0000930A
		public void EndSetUserSettings(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000B114 File Offset: 0x00009314
		public void SetUserSettingsAsync(Property[] Properties)
		{
			this.SetUserSettingsAsync(Properties, null);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000B11E File Offset: 0x0000931E
		public void SetUserSettingsAsync(Property[] Properties, object userState)
		{
			if (this.SetUserSettingsOperationCompleted == null)
			{
				this.SetUserSettingsOperationCompleted = new SendOrPostCallback(this.OnSetUserSettingsOperationCompleted);
			}
			base.InvokeAsync("SetUserSettings", new object[] { Properties }, this.SetUserSettingsOperationCompleted, userState);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000B158 File Offset: 0x00009358
		private void OnSetUserSettingsOperationCompleted(object arg)
		{
			if (this.SetUserSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetUserSettingsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000B197 File Offset: 0x00009397
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetUserSettings", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Values")]
		public Property[] GetUserSettings(Property[] Properties)
		{
			return (Property[])base.Invoke("GetUserSettings", new object[] { Properties })[0];
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000B1B5 File Offset: 0x000093B5
		public IAsyncResult BeginGetUserSettings(Property[] Properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserSettings", new object[] { Properties }, callback, asyncState);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000B1CE File Offset: 0x000093CE
		public Property[] EndGetUserSettings(IAsyncResult asyncResult)
		{
			return (Property[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000B1DE File Offset: 0x000093DE
		public void GetUserSettingsAsync(Property[] Properties)
		{
			this.GetUserSettingsAsync(Properties, null);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000B1E8 File Offset: 0x000093E8
		public void GetUserSettingsAsync(Property[] Properties, object userState)
		{
			if (this.GetUserSettingsOperationCompleted == null)
			{
				this.GetUserSettingsOperationCompleted = new SendOrPostCallback(this.OnGetUserSettingsOperationCompleted);
			}
			base.InvokeAsync("GetUserSettings", new object[] { Properties }, this.GetUserSettingsOperationCompleted, userState);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000B220 File Offset: 0x00009420
		private void OnGetUserSettingsOperationCompleted(object arg)
		{
			if (this.GetUserSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserSettingsCompleted(this, new GetUserSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000B265 File Offset: 0x00009465
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetSystemPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetSystemPolicies(Policy[] Policies)
		{
			base.Invoke("SetSystemPolicies", new object[] { Policies });
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000B27D File Offset: 0x0000947D
		public IAsyncResult BeginSetSystemPolicies(Policy[] Policies, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetSystemPolicies", new object[] { Policies }, callback, asyncState);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000B296 File Offset: 0x00009496
		public void EndSetSystemPolicies(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000B2A0 File Offset: 0x000094A0
		public void SetSystemPoliciesAsync(Policy[] Policies)
		{
			this.SetSystemPoliciesAsync(Policies, null);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000B2AA File Offset: 0x000094AA
		public void SetSystemPoliciesAsync(Policy[] Policies, object userState)
		{
			if (this.SetSystemPoliciesOperationCompleted == null)
			{
				this.SetSystemPoliciesOperationCompleted = new SendOrPostCallback(this.OnSetSystemPoliciesOperationCompleted);
			}
			base.InvokeAsync("SetSystemPolicies", new object[] { Policies }, this.SetSystemPoliciesOperationCompleted, userState);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000B2E4 File Offset: 0x000094E4
		private void OnSetSystemPoliciesOperationCompleted(object arg)
		{
			if (this.SetSystemPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetSystemPoliciesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000B323 File Offset: 0x00009523
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetSystemPolicies", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Policies")]
		public Policy[] GetSystemPolicies()
		{
			return (Policy[])base.Invoke("GetSystemPolicies", new object[0])[0];
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000B33D File Offset: 0x0000953D
		public IAsyncResult BeginGetSystemPolicies(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSystemPolicies", new object[0], callback, asyncState);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000B352 File Offset: 0x00009552
		public Policy[] EndGetSystemPolicies(IAsyncResult asyncResult)
		{
			return (Policy[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000B362 File Offset: 0x00009562
		public void GetSystemPoliciesAsync()
		{
			this.GetSystemPoliciesAsync(null);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000B36B File Offset: 0x0000956B
		public void GetSystemPoliciesAsync(object userState)
		{
			if (this.GetSystemPoliciesOperationCompleted == null)
			{
				this.GetSystemPoliciesOperationCompleted = new SendOrPostCallback(this.OnGetSystemPoliciesOperationCompleted);
			}
			base.InvokeAsync("GetSystemPolicies", new object[0], this.GetSystemPoliciesOperationCompleted, userState);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000B3A0 File Offset: 0x000095A0
		private void OnGetSystemPoliciesOperationCompleted(object arg)
		{
			if (this.GetSystemPoliciesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSystemPoliciesCompleted(this, new GetSystemPoliciesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000B3E5 File Offset: 0x000095E5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListExtensions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Extensions")]
		public Extension[] ListExtensions(string ExtensionType)
		{
			return (Extension[])base.Invoke("ListExtensions", new object[] { ExtensionType })[0];
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000B403 File Offset: 0x00009603
		public IAsyncResult BeginListExtensions(string ExtensionType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListExtensions", new object[] { ExtensionType }, callback, asyncState);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000B41C File Offset: 0x0000961C
		public Extension[] EndListExtensions(IAsyncResult asyncResult)
		{
			return (Extension[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000B42C File Offset: 0x0000962C
		public void ListExtensionsAsync(string ExtensionType)
		{
			this.ListExtensionsAsync(ExtensionType, null);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000B436 File Offset: 0x00009636
		public void ListExtensionsAsync(string ExtensionType, object userState)
		{
			if (this.ListExtensionsOperationCompleted == null)
			{
				this.ListExtensionsOperationCompleted = new SendOrPostCallback(this.OnListExtensionsOperationCompleted);
			}
			base.InvokeAsync("ListExtensions", new object[] { ExtensionType }, this.ListExtensionsOperationCompleted, userState);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000B470 File Offset: 0x00009670
		private void OnListExtensionsOperationCompleted(object arg)
		{
			if (this.ListExtensionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListExtensionsCompleted(this, new ListExtensionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000B4B5 File Offset: 0x000096B5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListExtensionTypes", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListExtensionTypes()
		{
			return (string[])base.Invoke("ListExtensionTypes", new object[0])[0];
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000B4CF File Offset: 0x000096CF
		public IAsyncResult BeginListExtensionTypes(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListExtensionTypes", new object[0], callback, asyncState);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000B4E4 File Offset: 0x000096E4
		public string[] EndListExtensionTypes(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000B4F4 File Offset: 0x000096F4
		public void ListExtensionTypesAsync()
		{
			this.ListExtensionTypesAsync(null);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000B4FD File Offset: 0x000096FD
		public void ListExtensionTypesAsync(object userState)
		{
			if (this.ListExtensionTypesOperationCompleted == null)
			{
				this.ListExtensionTypesOperationCompleted = new SendOrPostCallback(this.OnListExtensionTypesOperationCompleted);
			}
			base.InvokeAsync("ListExtensionTypes", new object[0], this.ListExtensionTypesOperationCompleted, userState);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000B534 File Offset: 0x00009734
		private void OnListExtensionTypesOperationCompleted(object arg)
		{
			if (this.ListExtensionTypesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListExtensionTypesCompleted(this, new ListExtensionTypesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000B579 File Offset: 0x00009779
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListEvents", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Events")]
		public Event[] ListEvents()
		{
			return (Event[])base.Invoke("ListEvents", new object[0])[0];
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000B593 File Offset: 0x00009793
		public IAsyncResult BeginListEvents(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListEvents", new object[0], callback, asyncState);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000B5A8 File Offset: 0x000097A8
		public Event[] EndListEvents(IAsyncResult asyncResult)
		{
			return (Event[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000B5B8 File Offset: 0x000097B8
		public void ListEventsAsync()
		{
			this.ListEventsAsync(null);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000B5C1 File Offset: 0x000097C1
		public void ListEventsAsync(object userState)
		{
			if (this.ListEventsOperationCompleted == null)
			{
				this.ListEventsOperationCompleted = new SendOrPostCallback(this.OnListEventsOperationCompleted);
			}
			base.InvokeAsync("ListEvents", new object[0], this.ListEventsOperationCompleted, userState);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000B5F8 File Offset: 0x000097F8
		private void OnListEventsOperationCompleted(object arg)
		{
			if (this.ListEventsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListEventsCompleted(this, new ListEventsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000B63D File Offset: 0x0000983D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/FireEvent", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void FireEvent(string EventType, string EventData, string SiteUrl)
		{
			base.Invoke("FireEvent", new object[] { EventType, EventData, SiteUrl });
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000B65D File Offset: 0x0000985D
		public IAsyncResult BeginFireEvent(string EventType, string EventData, string SiteUrl, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FireEvent", new object[] { EventType, EventData, SiteUrl }, callback, asyncState);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000B680 File Offset: 0x00009880
		public void EndFireEvent(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000B68A File Offset: 0x0000988A
		public void FireEventAsync(string EventType, string EventData, string SiteUrl)
		{
			this.FireEventAsync(EventType, EventData, SiteUrl, null);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000B698 File Offset: 0x00009898
		public void FireEventAsync(string EventType, string EventData, string SiteUrl, object userState)
		{
			if (this.FireEventOperationCompleted == null)
			{
				this.FireEventOperationCompleted = new SendOrPostCallback(this.OnFireEventOperationCompleted);
			}
			base.InvokeAsync("FireEvent", new object[] { EventType, EventData, SiteUrl }, this.FireEventOperationCompleted, userState);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000B6E4 File Offset: 0x000098E4
		private void OnFireEventOperationCompleted(object arg)
		{
			if (this.FireEventCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FireEventCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000B723 File Offset: 0x00009923
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListJobs", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Jobs")]
		public Job[] ListJobs()
		{
			return (Job[])base.Invoke("ListJobs", new object[0])[0];
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000B73D File Offset: 0x0000993D
		public IAsyncResult BeginListJobs(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListJobs", new object[0], callback, asyncState);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000B752 File Offset: 0x00009952
		public Job[] EndListJobs(IAsyncResult asyncResult)
		{
			return (Job[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000B762 File Offset: 0x00009962
		public void ListJobsAsync()
		{
			this.ListJobsAsync(null);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000B76B File Offset: 0x0000996B
		public void ListJobsAsync(object userState)
		{
			if (this.ListJobsOperationCompleted == null)
			{
				this.ListJobsOperationCompleted = new SendOrPostCallback(this.OnListJobsOperationCompleted);
			}
			base.InvokeAsync("ListJobs", new object[0], this.ListJobsOperationCompleted, userState);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000B7A0 File Offset: 0x000099A0
		private void OnListJobsOperationCompleted(object arg)
		{
			if (this.ListJobsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListJobsCompleted(this, new ListJobsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000B7E5 File Offset: 0x000099E5
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListJobTypes", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListJobTypes()
		{
			return (string[])base.Invoke("ListJobTypes", new object[0])[0];
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000B7FF File Offset: 0x000099FF
		public IAsyncResult BeginListJobTypes(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListJobTypes", new object[0], callback, asyncState);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000B814 File Offset: 0x00009A14
		public string[] EndListJobTypes(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000B824 File Offset: 0x00009A24
		public void ListJobTypesAsync()
		{
			this.ListJobTypesAsync(null);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000B82D File Offset: 0x00009A2D
		public void ListJobTypesAsync(object userState)
		{
			if (this.ListJobTypesOperationCompleted == null)
			{
				this.ListJobTypesOperationCompleted = new SendOrPostCallback(this.OnListJobTypesOperationCompleted);
			}
			base.InvokeAsync("ListJobTypes", new object[0], this.ListJobTypesOperationCompleted, userState);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000B864 File Offset: 0x00009A64
		private void OnListJobTypesOperationCompleted(object arg)
		{
			if (this.ListJobTypesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListJobTypesCompleted(this, new ListJobTypesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000B8A9 File Offset: 0x00009AA9
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListJobActions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListJobActions()
		{
			return (string[])base.Invoke("ListJobActions", new object[0])[0];
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000B8C3 File Offset: 0x00009AC3
		public IAsyncResult BeginListJobActions(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListJobActions", new object[0], callback, asyncState);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000B8D8 File Offset: 0x00009AD8
		public string[] EndListJobActions(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		public void ListJobActionsAsync()
		{
			this.ListJobActionsAsync(null);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000B8F1 File Offset: 0x00009AF1
		public void ListJobActionsAsync(object userState)
		{
			if (this.ListJobActionsOperationCompleted == null)
			{
				this.ListJobActionsOperationCompleted = new SendOrPostCallback(this.OnListJobActionsOperationCompleted);
			}
			base.InvokeAsync("ListJobActions", new object[0], this.ListJobActionsOperationCompleted, userState);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000B928 File Offset: 0x00009B28
		private void OnListJobActionsOperationCompleted(object arg)
		{
			if (this.ListJobActionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListJobActionsCompleted(this, new ListJobActionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000B96D File Offset: 0x00009B6D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListJobStates", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListJobStates()
		{
			return (string[])base.Invoke("ListJobStates", new object[0])[0];
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000B987 File Offset: 0x00009B87
		public IAsyncResult BeginListJobStates(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListJobStates", new object[0], callback, asyncState);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000B99C File Offset: 0x00009B9C
		public string[] EndListJobStates(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000B9AC File Offset: 0x00009BAC
		public void ListJobStatesAsync()
		{
			this.ListJobStatesAsync(null);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000B9B5 File Offset: 0x00009BB5
		public void ListJobStatesAsync(object userState)
		{
			if (this.ListJobStatesOperationCompleted == null)
			{
				this.ListJobStatesOperationCompleted = new SendOrPostCallback(this.OnListJobStatesOperationCompleted);
			}
			base.InvokeAsync("ListJobStates", new object[0], this.ListJobStatesOperationCompleted, userState);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000B9EC File Offset: 0x00009BEC
		private void OnListJobStatesOperationCompleted(object arg)
		{
			if (this.ListJobStatesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListJobStatesCompleted(this, new ListJobStatesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000BA31 File Offset: 0x00009C31
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/CancelJob", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool CancelJob(string JobID)
		{
			return (bool)base.Invoke("CancelJob", new object[] { JobID })[0];
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000BA4F File Offset: 0x00009C4F
		public IAsyncResult BeginCancelJob(string JobID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CancelJob", new object[] { JobID }, callback, asyncState);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000BA68 File Offset: 0x00009C68
		public bool EndCancelJob(IAsyncResult asyncResult)
		{
			return (bool)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000BA78 File Offset: 0x00009C78
		public void CancelJobAsync(string JobID)
		{
			this.CancelJobAsync(JobID, null);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000BA82 File Offset: 0x00009C82
		public void CancelJobAsync(string JobID, object userState)
		{
			if (this.CancelJobOperationCompleted == null)
			{
				this.CancelJobOperationCompleted = new SendOrPostCallback(this.OnCancelJobOperationCompleted);
			}
			base.InvokeAsync("CancelJob", new object[] { JobID }, this.CancelJobOperationCompleted, userState);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000BABC File Offset: 0x00009CBC
		private void OnCancelJobOperationCompleted(object arg)
		{
			if (this.CancelJobCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CancelJobCompleted(this, new CancelJobCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000BB01 File Offset: 0x00009D01
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/CreateCacheRefreshPlan", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("CacheRefreshPlanID")]
		public string CreateCacheRefreshPlan(string ItemPath, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			return (string)base.Invoke("CreateCacheRefreshPlan", new object[] { ItemPath, Description, EventType, MatchData, Parameters })[0];
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000BB31 File Offset: 0x00009D31
		public IAsyncResult BeginCreateCacheRefreshPlan(string ItemPath, string Description, string EventType, string MatchData, ParameterValue[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateCacheRefreshPlan", new object[] { ItemPath, Description, EventType, MatchData, Parameters }, callback, asyncState);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000BB5E File Offset: 0x00009D5E
		public string EndCreateCacheRefreshPlan(IAsyncResult asyncResult)
		{
			return (string)base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000BB6E File Offset: 0x00009D6E
		public void CreateCacheRefreshPlanAsync(string ItemPath, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			this.CreateCacheRefreshPlanAsync(ItemPath, Description, EventType, MatchData, Parameters, null);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000BB80 File Offset: 0x00009D80
		public void CreateCacheRefreshPlanAsync(string ItemPath, string Description, string EventType, string MatchData, ParameterValue[] Parameters, object userState)
		{
			if (this.CreateCacheRefreshPlanOperationCompleted == null)
			{
				this.CreateCacheRefreshPlanOperationCompleted = new SendOrPostCallback(this.OnCreateCacheRefreshPlanOperationCompleted);
			}
			base.InvokeAsync("CreateCacheRefreshPlan", new object[] { ItemPath, Description, EventType, MatchData, Parameters }, this.CreateCacheRefreshPlanOperationCompleted, userState);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000BBD8 File Offset: 0x00009DD8
		private void OnCreateCacheRefreshPlanOperationCompleted(object arg)
		{
			if (this.CreateCacheRefreshPlanCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateCacheRefreshPlanCompleted(this, new CreateCacheRefreshPlanCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000BC1D File Offset: 0x00009E1D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/SetCacheRefreshPlanProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetCacheRefreshPlanProperties(string CacheRefreshPlanID, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			base.Invoke("SetCacheRefreshPlanProperties", new object[] { CacheRefreshPlanID, Description, EventType, MatchData, Parameters });
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000BC47 File Offset: 0x00009E47
		public IAsyncResult BeginSetCacheRefreshPlanProperties(string CacheRefreshPlanID, string Description, string EventType, string MatchData, ParameterValue[] Parameters, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetCacheRefreshPlanProperties", new object[] { CacheRefreshPlanID, Description, EventType, MatchData, Parameters }, callback, asyncState);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000BC74 File Offset: 0x00009E74
		public void EndSetCacheRefreshPlanProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000BC7E File Offset: 0x00009E7E
		public void SetCacheRefreshPlanPropertiesAsync(string CacheRefreshPlanID, string Description, string EventType, string MatchData, ParameterValue[] Parameters)
		{
			this.SetCacheRefreshPlanPropertiesAsync(CacheRefreshPlanID, Description, EventType, MatchData, Parameters, null);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000BC90 File Offset: 0x00009E90
		public void SetCacheRefreshPlanPropertiesAsync(string CacheRefreshPlanID, string Description, string EventType, string MatchData, ParameterValue[] Parameters, object userState)
		{
			if (this.SetCacheRefreshPlanPropertiesOperationCompleted == null)
			{
				this.SetCacheRefreshPlanPropertiesOperationCompleted = new SendOrPostCallback(this.OnSetCacheRefreshPlanPropertiesOperationCompleted);
			}
			base.InvokeAsync("SetCacheRefreshPlanProperties", new object[] { CacheRefreshPlanID, Description, EventType, MatchData, Parameters }, this.SetCacheRefreshPlanPropertiesOperationCompleted, userState);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		private void OnSetCacheRefreshPlanPropertiesOperationCompleted(object arg)
		{
			if (this.SetCacheRefreshPlanPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetCacheRefreshPlanPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000BD28 File Offset: 0x00009F28
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetCacheRefreshPlanProperties", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Description")]
		public string GetCacheRefreshPlanProperties(string CacheRefreshPlanID, out string LastRunStatus, out CacheRefreshPlanState State, out string EventType, out string MatchData, out ParameterValue[] Parameters)
		{
			object[] array = base.Invoke("GetCacheRefreshPlanProperties", new object[] { CacheRefreshPlanID });
			LastRunStatus = (string)array[1];
			State = (CacheRefreshPlanState)array[2];
			EventType = (string)array[3];
			MatchData = (string)array[4];
			Parameters = (ParameterValue[])array[5];
			return (string)array[0];
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000BD88 File Offset: 0x00009F88
		public IAsyncResult BeginGetCacheRefreshPlanProperties(string CacheRefreshPlanID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCacheRefreshPlanProperties", new object[] { CacheRefreshPlanID }, callback, asyncState);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		public string EndGetCacheRefreshPlanProperties(IAsyncResult asyncResult, out string LastRunStatus, out CacheRefreshPlanState State, out string EventType, out string MatchData, out ParameterValue[] Parameters)
		{
			object[] array = base.EndInvoke(asyncResult);
			LastRunStatus = (string)array[1];
			State = (CacheRefreshPlanState)array[2];
			EventType = (string)array[3];
			MatchData = (string)array[4];
			Parameters = (ParameterValue[])array[5];
			return (string)array[0];
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000BDF6 File Offset: 0x00009FF6
		public void GetCacheRefreshPlanPropertiesAsync(string CacheRefreshPlanID)
		{
			this.GetCacheRefreshPlanPropertiesAsync(CacheRefreshPlanID, null);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000BE00 File Offset: 0x0000A000
		public void GetCacheRefreshPlanPropertiesAsync(string CacheRefreshPlanID, object userState)
		{
			if (this.GetCacheRefreshPlanPropertiesOperationCompleted == null)
			{
				this.GetCacheRefreshPlanPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetCacheRefreshPlanPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetCacheRefreshPlanProperties", new object[] { CacheRefreshPlanID }, this.GetCacheRefreshPlanPropertiesOperationCompleted, userState);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000BE38 File Offset: 0x0000A038
		private void OnGetCacheRefreshPlanPropertiesOperationCompleted(object arg)
		{
			if (this.GetCacheRefreshPlanPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCacheRefreshPlanPropertiesCompleted(this, new GetCacheRefreshPlanPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000BE7D File Offset: 0x0000A07D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/DeleteCacheRefreshPlan", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteCacheRefreshPlan(string CacheRefreshPlanID)
		{
			base.Invoke("DeleteCacheRefreshPlan", new object[] { CacheRefreshPlanID });
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000BE95 File Offset: 0x0000A095
		public IAsyncResult BeginDeleteCacheRefreshPlan(string CacheRefreshPlanID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteCacheRefreshPlan", new object[] { CacheRefreshPlanID }, callback, asyncState);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000BEAE File Offset: 0x0000A0AE
		public void EndDeleteCacheRefreshPlan(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		public void DeleteCacheRefreshPlanAsync(string CacheRefreshPlanID)
		{
			this.DeleteCacheRefreshPlanAsync(CacheRefreshPlanID, null);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000BEC2 File Offset: 0x0000A0C2
		public void DeleteCacheRefreshPlanAsync(string CacheRefreshPlanID, object userState)
		{
			if (this.DeleteCacheRefreshPlanOperationCompleted == null)
			{
				this.DeleteCacheRefreshPlanOperationCompleted = new SendOrPostCallback(this.OnDeleteCacheRefreshPlanOperationCompleted);
			}
			base.InvokeAsync("DeleteCacheRefreshPlan", new object[] { CacheRefreshPlanID }, this.DeleteCacheRefreshPlanOperationCompleted, userState);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000BEFC File Offset: 0x0000A0FC
		private void OnDeleteCacheRefreshPlanOperationCompleted(object arg)
		{
			if (this.DeleteCacheRefreshPlanCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteCacheRefreshPlanCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000BF3B File Offset: 0x0000A13B
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListCacheRefreshPlans", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("CacheRefreshPlans")]
		public CacheRefreshPlan[] ListCacheRefreshPlans(string ItemPath)
		{
			return (CacheRefreshPlan[])base.Invoke("ListCacheRefreshPlans", new object[] { ItemPath })[0];
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000BF59 File Offset: 0x0000A159
		public IAsyncResult BeginListCacheRefreshPlans(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListCacheRefreshPlans", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000BF72 File Offset: 0x0000A172
		public CacheRefreshPlan[] EndListCacheRefreshPlans(IAsyncResult asyncResult)
		{
			return (CacheRefreshPlan[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000BF82 File Offset: 0x0000A182
		public void ListCacheRefreshPlansAsync(string ItemPath)
		{
			this.ListCacheRefreshPlansAsync(ItemPath, null);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000BF8C File Offset: 0x0000A18C
		public void ListCacheRefreshPlansAsync(string ItemPath, object userState)
		{
			if (this.ListCacheRefreshPlansOperationCompleted == null)
			{
				this.ListCacheRefreshPlansOperationCompleted = new SendOrPostCallback(this.OnListCacheRefreshPlansOperationCompleted);
			}
			base.InvokeAsync("ListCacheRefreshPlans", new object[] { ItemPath }, this.ListCacheRefreshPlansOperationCompleted, userState);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000BFC4 File Offset: 0x0000A1C4
		private void OnListCacheRefreshPlansOperationCompleted(object arg)
		{
			if (this.ListCacheRefreshPlansCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListCacheRefreshPlansCompleted(this, new ListCacheRefreshPlansCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000C009 File Offset: 0x0000A209
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/LogonUser", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void LogonUser(string userName, string password, string authority)
		{
			base.Invoke("LogonUser", new object[] { userName, password, authority });
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000C029 File Offset: 0x0000A229
		public IAsyncResult BeginLogonUser(string userName, string password, string authority, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("LogonUser", new object[] { userName, password, authority }, callback, asyncState);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000C04C File Offset: 0x0000A24C
		public void EndLogonUser(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000C056 File Offset: 0x0000A256
		public void LogonUserAsync(string userName, string password, string authority)
		{
			this.LogonUserAsync(userName, password, authority, null);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000C064 File Offset: 0x0000A264
		public void LogonUserAsync(string userName, string password, string authority, object userState)
		{
			if (this.LogonUserOperationCompleted == null)
			{
				this.LogonUserOperationCompleted = new SendOrPostCallback(this.OnLogonUserOperationCompleted);
			}
			base.InvokeAsync("LogonUser", new object[] { userName, password, authority }, this.LogonUserOperationCompleted, userState);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000C0B0 File Offset: 0x0000A2B0
		private void OnLogonUserOperationCompleted(object arg)
		{
			if (this.LogonUserCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LogonUserCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000C0EF File Offset: 0x0000A2EF
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/Logoff", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void Logoff()
		{
			base.Invoke("Logoff", new object[0]);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000C103 File Offset: 0x0000A303
		public IAsyncResult BeginLogoff(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Logoff", new object[0], callback, asyncState);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000C118 File Offset: 0x0000A318
		public void EndLogoff(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000C122 File Offset: 0x0000A322
		public void LogoffAsync()
		{
			this.LogoffAsync(null);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000C12B File Offset: 0x0000A32B
		public void LogoffAsync(object userState)
		{
			if (this.LogoffOperationCompleted == null)
			{
				this.LogoffOperationCompleted = new SendOrPostCallback(this.OnLogoffOperationCompleted);
			}
			base.InvokeAsync("Logoff", new object[0], this.LogoffOperationCompleted, userState);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000C160 File Offset: 0x0000A360
		private void OnLogoffOperationCompleted(object arg)
		{
			if (this.LogoffCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.LogoffCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000C19F File Offset: 0x0000A39F
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetPermissions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Permissions")]
		[return: XmlArrayItem("Operation")]
		public string[] GetPermissions(string ItemPath)
		{
			return (string[])base.Invoke("GetPermissions", new object[] { ItemPath })[0];
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000C1BD File Offset: 0x0000A3BD
		public IAsyncResult BeginGetPermissions(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPermissions", new object[] { ItemPath }, callback, asyncState);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000C1D6 File Offset: 0x0000A3D6
		public string[] EndGetPermissions(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000C1E6 File Offset: 0x0000A3E6
		public void GetPermissionsAsync(string ItemPath)
		{
			this.GetPermissionsAsync(ItemPath, null);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000C1F0 File Offset: 0x0000A3F0
		public void GetPermissionsAsync(string ItemPath, object userState)
		{
			if (this.GetPermissionsOperationCompleted == null)
			{
				this.GetPermissionsOperationCompleted = new SendOrPostCallback(this.OnGetPermissionsOperationCompleted);
			}
			base.InvokeAsync("GetPermissions", new object[] { ItemPath }, this.GetPermissionsOperationCompleted, userState);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000C228 File Offset: 0x0000A428
		private void OnGetPermissionsOperationCompleted(object arg)
		{
			if (this.GetPermissionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPermissionsCompleted(this, new GetPermissionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000C26D File Offset: 0x0000A46D
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/GetSystemPermissions", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Permissions")]
		[return: XmlArrayItem("Operation")]
		public string[] GetSystemPermissions()
		{
			return (string[])base.Invoke("GetSystemPermissions", new object[0])[0];
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000C287 File Offset: 0x0000A487
		public IAsyncResult BeginGetSystemPermissions(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSystemPermissions", new object[0], callback, asyncState);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000C29C File Offset: 0x0000A49C
		public string[] EndGetSystemPermissions(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000C2AC File Offset: 0x0000A4AC
		public void GetSystemPermissionsAsync()
		{
			this.GetSystemPermissionsAsync(null);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000C2B5 File Offset: 0x0000A4B5
		public void GetSystemPermissionsAsync(object userState)
		{
			if (this.GetSystemPermissionsOperationCompleted == null)
			{
				this.GetSystemPermissionsOperationCompleted = new SendOrPostCallback(this.OnGetSystemPermissionsOperationCompleted);
			}
			base.InvokeAsync("GetSystemPermissions", new object[0], this.GetSystemPermissionsOperationCompleted, userState);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000C2EC File Offset: 0x0000A4EC
		private void OnGetSystemPermissionsOperationCompleted(object arg)
		{
			if (this.GetSystemPermissionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSystemPermissionsCompleted(this, new GetSystemPermissionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000C331 File Offset: 0x0000A531
		[SoapHeader("ServerInfoHeaderValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer/ListSecurityScopes", RequestNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", ResponseNamespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListSecurityScopes()
		{
			return (string[])base.Invoke("ListSecurityScopes", new object[0])[0];
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000C34B File Offset: 0x0000A54B
		public IAsyncResult BeginListSecurityScopes(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListSecurityScopes", new object[0], callback, asyncState);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000C360 File Offset: 0x0000A560
		public string[] EndListSecurityScopes(IAsyncResult asyncResult)
		{
			return (string[])base.EndInvoke(asyncResult)[0];
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000C370 File Offset: 0x0000A570
		public void ListSecurityScopesAsync()
		{
			this.ListSecurityScopesAsync(null);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000C379 File Offset: 0x0000A579
		public void ListSecurityScopesAsync(object userState)
		{
			if (this.ListSecurityScopesOperationCompleted == null)
			{
				this.ListSecurityScopesOperationCompleted = new SendOrPostCallback(this.OnListSecurityScopesOperationCompleted);
			}
			base.InvokeAsync("ListSecurityScopes", new object[0], this.ListSecurityScopesOperationCompleted, userState);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000C3B0 File Offset: 0x0000A5B0
		private void OnListSecurityScopesOperationCompleted(object arg)
		{
			if (this.ListSecurityScopesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListSecurityScopesCompleted(this, new ListSecurityScopesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000C3F5 File Offset: 0x0000A5F5
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x04000001 RID: 1
		private TrustedUserHeader trustedUserHeaderValueField;

		// Token: 0x04000002 RID: 2
		private ServerInfoHeader serverInfoHeaderValueField;

		// Token: 0x04000003 RID: 3
		private SendOrPostCallback CreateCatalogItemOperationCompleted;

		// Token: 0x04000004 RID: 4
		private SendOrPostCallback SetItemDefinitionOperationCompleted;

		// Token: 0x04000005 RID: 5
		private SendOrPostCallback GetItemDefinitionOperationCompleted;

		// Token: 0x04000006 RID: 6
		private SendOrPostCallback GetItemTypeOperationCompleted;

		// Token: 0x04000007 RID: 7
		private SendOrPostCallback DeleteItemOperationCompleted;

		// Token: 0x04000008 RID: 8
		private SendOrPostCallback MoveItemOperationCompleted;

		// Token: 0x04000009 RID: 9
		private SendOrPostCallback InheritParentSecurityOperationCompleted;

		// Token: 0x0400000A RID: 10
		private SendOrPostCallback ListItemHistoryOperationCompleted;

		// Token: 0x0400000B RID: 11
		private SendOrPostCallback ListChildrenOperationCompleted;

		// Token: 0x0400000C RID: 12
		private SendOrPostCallback ListDependentItemsOperationCompleted;

		// Token: 0x0400000D RID: 13
		private SendOrPostCallback FindItemsOperationCompleted;

		// Token: 0x0400000E RID: 14
		private SendOrPostCallback ListParentsOperationCompleted;

		// Token: 0x0400000F RID: 15
		private SendOrPostCallback CreateFolderOperationCompleted;

		// Token: 0x04000010 RID: 16
		private SendOrPostCallback SetPropertiesOperationCompleted;

		// Token: 0x04000011 RID: 17
		private ItemNamespaceHeader itemNamespaceHeaderValueField;

		// Token: 0x04000012 RID: 18
		private SendOrPostCallback GetPropertiesOperationCompleted;

		// Token: 0x04000013 RID: 19
		private SendOrPostCallback SetItemReferencesOperationCompleted;

		// Token: 0x04000014 RID: 20
		private SendOrPostCallback GetItemReferencesOperationCompleted;

		// Token: 0x04000015 RID: 21
		private SendOrPostCallback ListItemTypesOperationCompleted;

		// Token: 0x04000016 RID: 22
		private SendOrPostCallback SetSubscriptionPropertiesOperationCompleted;

		// Token: 0x04000017 RID: 23
		private SendOrPostCallback GetSubscriptionPropertiesOperationCompleted;

		// Token: 0x04000018 RID: 24
		private SendOrPostCallback SetDataDrivenSubscriptionPropertiesOperationCompleted;

		// Token: 0x04000019 RID: 25
		private SendOrPostCallback GetDataDrivenSubscriptionPropertiesOperationCompleted;

		// Token: 0x0400001A RID: 26
		private SendOrPostCallback DisableSubscriptionOperationCompleted;

		// Token: 0x0400001B RID: 27
		private SendOrPostCallback EnableSubscriptionOperationCompleted;

		// Token: 0x0400001C RID: 28
		private SendOrPostCallback DeleteSubscriptionOperationCompleted;

		// Token: 0x0400001D RID: 29
		private SendOrPostCallback CreateSubscriptionOperationCompleted;

		// Token: 0x0400001E RID: 30
		private SendOrPostCallback CreateDataDrivenSubscriptionOperationCompleted;

		// Token: 0x0400001F RID: 31
		private SendOrPostCallback GetExtensionSettingsOperationCompleted;

		// Token: 0x04000020 RID: 32
		private SendOrPostCallback ValidateExtensionSettingsOperationCompleted;

		// Token: 0x04000021 RID: 33
		private SendOrPostCallback ListSubscriptionsOperationCompleted;

		// Token: 0x04000022 RID: 34
		private SendOrPostCallback ListMySubscriptionsOperationCompleted;

		// Token: 0x04000023 RID: 35
		private SendOrPostCallback ListSubscriptionsUsingDataSourceOperationCompleted;

		// Token: 0x04000024 RID: 36
		private SendOrPostCallback ChangeSubscriptionOwnerOperationCompleted;

		// Token: 0x04000025 RID: 37
		private SendOrPostCallback CreateDataSourceOperationCompleted;

		// Token: 0x04000026 RID: 38
		private SendOrPostCallback PrepareQueryOperationCompleted;

		// Token: 0x04000027 RID: 39
		private SendOrPostCallback EnableDataSourceOperationCompleted;

		// Token: 0x04000028 RID: 40
		private SendOrPostCallback DisableDataSourceOperationCompleted;

		// Token: 0x04000029 RID: 41
		private SendOrPostCallback SetDataSourceContentsOperationCompleted;

		// Token: 0x0400002A RID: 42
		private SendOrPostCallback GetDataSourceContentsOperationCompleted;

		// Token: 0x0400002B RID: 43
		private SendOrPostCallback ListDatabaseCredentialRetrievalOptionsOperationCompleted;

		// Token: 0x0400002C RID: 44
		private SendOrPostCallback SetItemDataSourcesOperationCompleted;

		// Token: 0x0400002D RID: 45
		private SendOrPostCallback GetItemDataSourcesOperationCompleted;

		// Token: 0x0400002E RID: 46
		private SendOrPostCallback TestConnectForDataSourceDefinitionOperationCompleted;

		// Token: 0x0400002F RID: 47
		private SendOrPostCallback TestConnectForItemDataSourceOperationCompleted;

		// Token: 0x04000030 RID: 48
		private SendOrPostCallback CreateRoleOperationCompleted;

		// Token: 0x04000031 RID: 49
		private SendOrPostCallback SetRolePropertiesOperationCompleted;

		// Token: 0x04000032 RID: 50
		private SendOrPostCallback GetRolePropertiesOperationCompleted;

		// Token: 0x04000033 RID: 51
		private SendOrPostCallback DeleteRoleOperationCompleted;

		// Token: 0x04000034 RID: 52
		private SendOrPostCallback ListRolesOperationCompleted;

		// Token: 0x04000035 RID: 53
		private SendOrPostCallback ListTasksOperationCompleted;

		// Token: 0x04000036 RID: 54
		private SendOrPostCallback SetPoliciesOperationCompleted;

		// Token: 0x04000037 RID: 55
		private SendOrPostCallback GetPoliciesOperationCompleted;

		// Token: 0x04000038 RID: 56
		private SendOrPostCallback GetItemDataSourcePromptsOperationCompleted;

		// Token: 0x04000039 RID: 57
		private SendOrPostCallback GenerateModelOperationCompleted;

		// Token: 0x0400003A RID: 58
		private SendOrPostCallback GetModelItemPermissionsOperationCompleted;

		// Token: 0x0400003B RID: 59
		private SendOrPostCallback SetModelItemPoliciesOperationCompleted;

		// Token: 0x0400003C RID: 60
		private SendOrPostCallback GetModelItemPoliciesOperationCompleted;

		// Token: 0x0400003D RID: 61
		private SendOrPostCallback GetUserModelOperationCompleted;

		// Token: 0x0400003E RID: 62
		private SendOrPostCallback InheritModelItemParentSecurityOperationCompleted;

		// Token: 0x0400003F RID: 63
		private SendOrPostCallback SetModelDrillthroughReportsOperationCompleted;

		// Token: 0x04000040 RID: 64
		private SendOrPostCallback ListModelDrillthroughReportsOperationCompleted;

		// Token: 0x04000041 RID: 65
		private SendOrPostCallback ListModelItemChildrenOperationCompleted;

		// Token: 0x04000042 RID: 66
		private SendOrPostCallback ListModelItemTypesOperationCompleted;

		// Token: 0x04000043 RID: 67
		private SendOrPostCallback ListModelPerspectivesOperationCompleted;

		// Token: 0x04000044 RID: 68
		private SendOrPostCallback RegenerateModelOperationCompleted;

		// Token: 0x04000045 RID: 69
		private SendOrPostCallback RemoveAllModelItemPoliciesOperationCompleted;

		// Token: 0x04000046 RID: 70
		private SendOrPostCallback CreateScheduleOperationCompleted;

		// Token: 0x04000047 RID: 71
		private SendOrPostCallback DeleteScheduleOperationCompleted;

		// Token: 0x04000048 RID: 72
		private SendOrPostCallback ListSchedulesOperationCompleted;

		// Token: 0x04000049 RID: 73
		private SendOrPostCallback GetSchedulePropertiesOperationCompleted;

		// Token: 0x0400004A RID: 74
		private SendOrPostCallback ListScheduleStatesOperationCompleted;

		// Token: 0x0400004B RID: 75
		private SendOrPostCallback PauseScheduleOperationCompleted;

		// Token: 0x0400004C RID: 76
		private SendOrPostCallback ResumeScheduleOperationCompleted;

		// Token: 0x0400004D RID: 77
		private SendOrPostCallback SetSchedulePropertiesOperationCompleted;

		// Token: 0x0400004E RID: 78
		private SendOrPostCallback ListScheduledItemsOperationCompleted;

		// Token: 0x0400004F RID: 79
		private SendOrPostCallback SetItemParametersOperationCompleted;

		// Token: 0x04000050 RID: 80
		private SendOrPostCallback GetItemParametersOperationCompleted;

		// Token: 0x04000051 RID: 81
		private SendOrPostCallback ListParameterTypesOperationCompleted;

		// Token: 0x04000052 RID: 82
		private SendOrPostCallback ListParameterStatesOperationCompleted;

		// Token: 0x04000053 RID: 83
		private SendOrPostCallback CreateReportEditSessionOperationCompleted;

		// Token: 0x04000054 RID: 84
		private SendOrPostCallback CreateLinkedItemOperationCompleted;

		// Token: 0x04000055 RID: 85
		private SendOrPostCallback SetItemLinkOperationCompleted;

		// Token: 0x04000056 RID: 86
		private SendOrPostCallback GetItemLinkOperationCompleted;

		// Token: 0x04000057 RID: 87
		private SendOrPostCallback ListExecutionSettingsOperationCompleted;

		// Token: 0x04000058 RID: 88
		private SendOrPostCallback SetExecutionOptionsOperationCompleted;

		// Token: 0x04000059 RID: 89
		private SendOrPostCallback GetExecutionOptionsOperationCompleted;

		// Token: 0x0400005A RID: 90
		private SendOrPostCallback UpdateItemExecutionSnapshotOperationCompleted;

		// Token: 0x0400005B RID: 91
		private SendOrPostCallback SetCacheOptionsOperationCompleted;

		// Token: 0x0400005C RID: 92
		private SendOrPostCallback GetCacheOptionsOperationCompleted;

		// Token: 0x0400005D RID: 93
		private SendOrPostCallback FlushCacheOperationCompleted;

		// Token: 0x0400005E RID: 94
		private SendOrPostCallback CreateItemHistorySnapshotOperationCompleted;

		// Token: 0x0400005F RID: 95
		private SendOrPostCallback DeleteItemHistorySnapshotOperationCompleted;

		// Token: 0x04000060 RID: 96
		private SendOrPostCallback SetItemHistoryLimitOperationCompleted;

		// Token: 0x04000061 RID: 97
		private SendOrPostCallback GetItemHistoryLimitOperationCompleted;

		// Token: 0x04000062 RID: 98
		private SendOrPostCallback SetItemHistoryOptionsOperationCompleted;

		// Token: 0x04000063 RID: 99
		private SendOrPostCallback GetItemHistoryOptionsOperationCompleted;

		// Token: 0x04000064 RID: 100
		private SendOrPostCallback GetReportServerConfigInfoOperationCompleted;

		// Token: 0x04000065 RID: 101
		private SendOrPostCallback IsSSLRequiredOperationCompleted;

		// Token: 0x04000066 RID: 102
		private SendOrPostCallback SetSystemPropertiesOperationCompleted;

		// Token: 0x04000067 RID: 103
		private SendOrPostCallback GetSystemPropertiesOperationCompleted;

		// Token: 0x04000068 RID: 104
		private SendOrPostCallback SetUserSettingsOperationCompleted;

		// Token: 0x04000069 RID: 105
		private SendOrPostCallback GetUserSettingsOperationCompleted;

		// Token: 0x0400006A RID: 106
		private SendOrPostCallback SetSystemPoliciesOperationCompleted;

		// Token: 0x0400006B RID: 107
		private SendOrPostCallback GetSystemPoliciesOperationCompleted;

		// Token: 0x0400006C RID: 108
		private SendOrPostCallback ListExtensionsOperationCompleted;

		// Token: 0x0400006D RID: 109
		private SendOrPostCallback ListExtensionTypesOperationCompleted;

		// Token: 0x0400006E RID: 110
		private SendOrPostCallback ListEventsOperationCompleted;

		// Token: 0x0400006F RID: 111
		private SendOrPostCallback FireEventOperationCompleted;

		// Token: 0x04000070 RID: 112
		private SendOrPostCallback ListJobsOperationCompleted;

		// Token: 0x04000071 RID: 113
		private SendOrPostCallback ListJobTypesOperationCompleted;

		// Token: 0x04000072 RID: 114
		private SendOrPostCallback ListJobActionsOperationCompleted;

		// Token: 0x04000073 RID: 115
		private SendOrPostCallback ListJobStatesOperationCompleted;

		// Token: 0x04000074 RID: 116
		private SendOrPostCallback CancelJobOperationCompleted;

		// Token: 0x04000075 RID: 117
		private SendOrPostCallback CreateCacheRefreshPlanOperationCompleted;

		// Token: 0x04000076 RID: 118
		private SendOrPostCallback SetCacheRefreshPlanPropertiesOperationCompleted;

		// Token: 0x04000077 RID: 119
		private SendOrPostCallback GetCacheRefreshPlanPropertiesOperationCompleted;

		// Token: 0x04000078 RID: 120
		private SendOrPostCallback DeleteCacheRefreshPlanOperationCompleted;

		// Token: 0x04000079 RID: 121
		private SendOrPostCallback ListCacheRefreshPlansOperationCompleted;

		// Token: 0x0400007A RID: 122
		private SendOrPostCallback LogonUserOperationCompleted;

		// Token: 0x0400007B RID: 123
		private SendOrPostCallback LogoffOperationCompleted;

		// Token: 0x0400007C RID: 124
		private SendOrPostCallback GetPermissionsOperationCompleted;

		// Token: 0x0400007D RID: 125
		private SendOrPostCallback GetSystemPermissionsOperationCompleted;

		// Token: 0x0400007E RID: 126
		private SendOrPostCallback ListSecurityScopesOperationCompleted;
	}
}
