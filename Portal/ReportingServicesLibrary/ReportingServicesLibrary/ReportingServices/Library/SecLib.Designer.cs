using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002E7 RID: 743
	[CompilerGenerated]
	internal class SecLib
	{
		// Token: 0x06001A8E RID: 6798 RVA: 0x000025F4 File Offset: 0x000007F4
		protected SecLib()
		{
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0006BDEE File Offset: 0x00069FEE
		// (set) Token: 0x06001A90 RID: 6800 RVA: 0x0006BDF5 File Offset: 0x00069FF5
		public static CultureInfo Culture
		{
			get
			{
				return SecLib.Keys.Culture;
			}
			set
			{
				SecLib.Keys.Culture = value;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x0006BDFD File Offset: 0x00069FFD
		public static string ConfigureAccessName
		{
			get
			{
				return SecLib.Keys.GetString("ConfigureAccessName");
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x0006BE09 File Offset: 0x0006A009
		public static string CreateLinkedReportsName
		{
			get
			{
				return SecLib.Keys.GetString("CreateLinkedReportsName");
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x0006BE15 File Offset: 0x0006A015
		public static string ViewReportsName
		{
			get
			{
				return SecLib.Keys.GetString("ViewReportsName");
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x0006BE21 File Offset: 0x0006A021
		public static string ManageReportsName
		{
			get
			{
				return SecLib.Keys.GetString("ManageReportsName");
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x0006BE2D File Offset: 0x0006A02D
		public static string ViewResourcesName
		{
			get
			{
				return SecLib.Keys.GetString("ViewResourcesName");
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x0006BE39 File Offset: 0x0006A039
		public static string ManageResourcesName
		{
			get
			{
				return SecLib.Keys.GetString("ManageResourcesName");
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x0006BE45 File Offset: 0x0006A045
		public static string ViewFoldersName
		{
			get
			{
				return SecLib.Keys.GetString("ViewFoldersName");
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x0006BE51 File Offset: 0x0006A051
		public static string ManageFoldersName
		{
			get
			{
				return SecLib.Keys.GetString("ManageFoldersName");
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0006BE5D File Offset: 0x0006A05D
		public static string ManageReportHistoryName
		{
			get
			{
				return SecLib.Keys.GetString("ManageReportHistoryName");
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x0006BE69 File Offset: 0x0006A069
		public static string SubscribeName
		{
			get
			{
				return SecLib.Keys.GetString("SubscribeName");
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x0006BE75 File Offset: 0x0006A075
		public static string ManageAnySubscriptionName
		{
			get
			{
				return SecLib.Keys.GetString("ManageAnySubscriptionName");
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x0006BE81 File Offset: 0x0006A081
		public static string ViewDatasourceName
		{
			get
			{
				return SecLib.Keys.GetString("ViewDatasourceName");
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x0006BE8D File Offset: 0x0006A08D
		public static string ManageDatasourceName
		{
			get
			{
				return SecLib.Keys.GetString("ManageDatasourceName");
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x0006BE99 File Offset: 0x0006A099
		public static string ViewModelsName
		{
			get
			{
				return SecLib.Keys.GetString("ViewModelsName");
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x0006BEA5 File Offset: 0x0006A0A5
		public static string ManageModelsName
		{
			get
			{
				return SecLib.Keys.GetString("ManageModelsName");
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x0006BEB1 File Offset: 0x0006A0B1
		public static string ConsumeReportsName
		{
			get
			{
				return SecLib.Keys.GetString("ConsumeReportsName");
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x0006BEBD File Offset: 0x0006A0BD
		public static string CommentName
		{
			get
			{
				return SecLib.Keys.GetString("CommentName");
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x0006BEC9 File Offset: 0x0006A0C9
		public static string ManageCommentsName
		{
			get
			{
				return SecLib.Keys.GetString("ManageCommentsName");
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x0006BED5 File Offset: 0x0006A0D5
		public static string ConfigureAccessDescription
		{
			get
			{
				return SecLib.Keys.GetString("ConfigureAccessDescription");
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x0006BEE1 File Offset: 0x0006A0E1
		public static string CreateLinkedReportsDescription
		{
			get
			{
				return SecLib.Keys.GetString("CreateLinkedReportsDescription");
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x0006BEED File Offset: 0x0006A0ED
		public static string ViewReportsDescription
		{
			get
			{
				return SecLib.Keys.GetString("ViewReportsDescription");
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x0006BEF9 File Offset: 0x0006A0F9
		public static string ManageReportsDescription
		{
			get
			{
				return SecLib.Keys.GetString("ManageReportsDescription");
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x0006BF05 File Offset: 0x0006A105
		public static string ViewResourcesDescription
		{
			get
			{
				return SecLib.Keys.GetString("ViewResourcesDescription");
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x0006BF11 File Offset: 0x0006A111
		public static string ManageResourcesDescription
		{
			get
			{
				return SecLib.Keys.GetString("ManageResourcesDescription");
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x0006BF1D File Offset: 0x0006A11D
		public static string ViewFoldersDescription
		{
			get
			{
				return SecLib.Keys.GetString("ViewFoldersDescription");
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x0006BF29 File Offset: 0x0006A129
		public static string ManageFoldersDescription
		{
			get
			{
				return SecLib.Keys.GetString("ManageFoldersDescription");
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0006BF35 File Offset: 0x0006A135
		public static string ManageReportHistoryDescription
		{
			get
			{
				return SecLib.Keys.GetString("ManageReportHistoryDescription");
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x0006BF41 File Offset: 0x0006A141
		public static string SubscribeDescription
		{
			get
			{
				return SecLib.Keys.GetString("SubscribeDescription");
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0006BF4D File Offset: 0x0006A14D
		public static string ManageAnySubscriptionDescription
		{
			get
			{
				return SecLib.Keys.GetString("ManageAnySubscriptionDescription");
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x0006BF59 File Offset: 0x0006A159
		public static string ViewDatasourceDescription
		{
			get
			{
				return SecLib.Keys.GetString("ViewDatasourceDescription");
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0006BF65 File Offset: 0x0006A165
		public static string ManageDatasourceDescription
		{
			get
			{
				return SecLib.Keys.GetString("ManageDatasourceDescription");
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x0006BF71 File Offset: 0x0006A171
		public static string ViewModelsDescription
		{
			get
			{
				return SecLib.Keys.GetString("ViewModelsDescription");
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0006BF7D File Offset: 0x0006A17D
		public static string ManageModelsDescription
		{
			get
			{
				return SecLib.Keys.GetString("ManageModelsDescription");
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x0006BF89 File Offset: 0x0006A189
		public static string ConsumeReportsDescription
		{
			get
			{
				return SecLib.Keys.GetString("ConsumeReportsDescription");
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x0006BF95 File Offset: 0x0006A195
		public static string CommentDescription
		{
			get
			{
				return SecLib.Keys.GetString("CommentDescription");
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x0006BFA1 File Offset: 0x0006A1A1
		public static string ManageCommentsDescription
		{
			get
			{
				return SecLib.Keys.GetString("ManageCommentsDescription");
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0006BFAD File Offset: 0x0006A1AD
		public static string ManageRolesName
		{
			get
			{
				return SecLib.Keys.GetString("ManageRolesName");
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x0006BFB9 File Offset: 0x0006A1B9
		public static string ViewSystemPropertiesName
		{
			get
			{
				return SecLib.Keys.GetString("ViewSystemPropertiesName");
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0006BFC5 File Offset: 0x0006A1C5
		public static string ManageSystemPropertiesName
		{
			get
			{
				return SecLib.Keys.GetString("ManageSystemPropertiesName");
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x0006BFD1 File Offset: 0x0006A1D1
		public static string ViewSharedSchedulesName
		{
			get
			{
				return SecLib.Keys.GetString("ViewSharedSchedulesName");
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x0006BFDD File Offset: 0x0006A1DD
		public static string ManageSharedSchedulesName
		{
			get
			{
				return SecLib.Keys.GetString("ManageSharedSchedulesName");
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x0006BFE9 File Offset: 0x0006A1E9
		public static string ManageSystemSecurityName
		{
			get
			{
				return SecLib.Keys.GetString("ManageSystemSecurityName");
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x0006BFF5 File Offset: 0x0006A1F5
		public static string GenerateEventsName
		{
			get
			{
				return SecLib.Keys.GetString("GenerateEventsName");
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x0006C001 File Offset: 0x0006A201
		public static string ManageJobsName
		{
			get
			{
				return SecLib.Keys.GetString("ManageJobsName");
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x0006C00D File Offset: 0x0006A20D
		public static string ExecuteReportDefinitionsName
		{
			get
			{
				return SecLib.Keys.GetString("ExecuteReportDefinitionsName");
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x0006C019 File Offset: 0x0006A219
		public static string ManageRolesDescription
		{
			get
			{
				return SecLib.Keys.GetString("ManageRolesDescription");
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x0006C025 File Offset: 0x0006A225
		public static string ViewSystemPropertiesDescription
		{
			get
			{
				return SecLib.Keys.GetString("ViewSystemPropertiesDescription");
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x0006C031 File Offset: 0x0006A231
		public static string ManageSystemPropertiesDescription
		{
			get
			{
				return SecLib.Keys.GetString("ManageSystemPropertiesDescription");
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x0006C03D File Offset: 0x0006A23D
		public static string ViewSharedSchedulesDescription
		{
			get
			{
				return SecLib.Keys.GetString("ViewSharedSchedulesDescription");
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x0006C049 File Offset: 0x0006A249
		public static string ManageSharedSchedulesDescription
		{
			get
			{
				return SecLib.Keys.GetString("ManageSharedSchedulesDescription");
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x0006C055 File Offset: 0x0006A255
		public static string ManageSystemSecurityDescription
		{
			get
			{
				return SecLib.Keys.GetString("ManageSystemSecurityDescription");
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x0006C061 File Offset: 0x0006A261
		public static string GenerateEventsDescription
		{
			get
			{
				return SecLib.Keys.GetString("GenerateEventsDescription");
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x0006C06D File Offset: 0x0006A26D
		public static string ManageJobsDescription
		{
			get
			{
				return SecLib.Keys.GetString("ManageJobsDescription");
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x0006C079 File Offset: 0x0006A279
		public static string ExecuteReportDefinitionsDescription
		{
			get
			{
				return SecLib.Keys.GetString("ExecuteReportDefinitionsDescription");
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x0006C085 File Offset: 0x0006A285
		public static string ViewModelItemsName
		{
			get
			{
				return SecLib.Keys.GetString("ViewModelItemsName");
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x0006C091 File Offset: 0x0006A291
		public static string ViewModelItemsDescription
		{
			get
			{
				return SecLib.Keys.GetString("ViewModelItemsDescription");
			}
		}

		// Token: 0x020004F0 RID: 1264
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x060024B6 RID: 9398 RVA: 0x000025F4 File Offset: 0x000007F4
			private Keys()
			{
			}

			// Token: 0x17000AAB RID: 2731
			// (get) Token: 0x060024B7 RID: 9399 RVA: 0x00086DB0 File Offset: 0x00084FB0
			// (set) Token: 0x060024B8 RID: 9400 RVA: 0x00086DB7 File Offset: 0x00084FB7
			public static CultureInfo Culture
			{
				get
				{
					return SecLib.Keys._culture;
				}
				set
				{
					SecLib.Keys._culture = value;
				}
			}

			// Token: 0x060024B9 RID: 9401 RVA: 0x00086DBF File Offset: 0x00084FBF
			public static string GetString(string key)
			{
				return SecLib.Keys.resourceManager.GetString(key, SecLib.Keys._culture);
			}

			// Token: 0x0400116A RID: 4458
			private static ResourceManager resourceManager = new ResourceManager(typeof(SecLib).FullName, typeof(SecLib).Module.Assembly);

			// Token: 0x0400116B RID: 4459
			private static CultureInfo _culture = null;

			// Token: 0x0400116C RID: 4460
			public const string ConfigureAccessName = "ConfigureAccessName";

			// Token: 0x0400116D RID: 4461
			public const string CreateLinkedReportsName = "CreateLinkedReportsName";

			// Token: 0x0400116E RID: 4462
			public const string ViewReportsName = "ViewReportsName";

			// Token: 0x0400116F RID: 4463
			public const string ManageReportsName = "ManageReportsName";

			// Token: 0x04001170 RID: 4464
			public const string ViewResourcesName = "ViewResourcesName";

			// Token: 0x04001171 RID: 4465
			public const string ManageResourcesName = "ManageResourcesName";

			// Token: 0x04001172 RID: 4466
			public const string ViewFoldersName = "ViewFoldersName";

			// Token: 0x04001173 RID: 4467
			public const string ManageFoldersName = "ManageFoldersName";

			// Token: 0x04001174 RID: 4468
			public const string ManageReportHistoryName = "ManageReportHistoryName";

			// Token: 0x04001175 RID: 4469
			public const string SubscribeName = "SubscribeName";

			// Token: 0x04001176 RID: 4470
			public const string ManageAnySubscriptionName = "ManageAnySubscriptionName";

			// Token: 0x04001177 RID: 4471
			public const string ViewDatasourceName = "ViewDatasourceName";

			// Token: 0x04001178 RID: 4472
			public const string ManageDatasourceName = "ManageDatasourceName";

			// Token: 0x04001179 RID: 4473
			public const string ViewModelsName = "ViewModelsName";

			// Token: 0x0400117A RID: 4474
			public const string ManageModelsName = "ManageModelsName";

			// Token: 0x0400117B RID: 4475
			public const string ConsumeReportsName = "ConsumeReportsName";

			// Token: 0x0400117C RID: 4476
			public const string CommentName = "CommentName";

			// Token: 0x0400117D RID: 4477
			public const string ManageCommentsName = "ManageCommentsName";

			// Token: 0x0400117E RID: 4478
			public const string ConfigureAccessDescription = "ConfigureAccessDescription";

			// Token: 0x0400117F RID: 4479
			public const string CreateLinkedReportsDescription = "CreateLinkedReportsDescription";

			// Token: 0x04001180 RID: 4480
			public const string ViewReportsDescription = "ViewReportsDescription";

			// Token: 0x04001181 RID: 4481
			public const string ManageReportsDescription = "ManageReportsDescription";

			// Token: 0x04001182 RID: 4482
			public const string ViewResourcesDescription = "ViewResourcesDescription";

			// Token: 0x04001183 RID: 4483
			public const string ManageResourcesDescription = "ManageResourcesDescription";

			// Token: 0x04001184 RID: 4484
			public const string ViewFoldersDescription = "ViewFoldersDescription";

			// Token: 0x04001185 RID: 4485
			public const string ManageFoldersDescription = "ManageFoldersDescription";

			// Token: 0x04001186 RID: 4486
			public const string ManageReportHistoryDescription = "ManageReportHistoryDescription";

			// Token: 0x04001187 RID: 4487
			public const string SubscribeDescription = "SubscribeDescription";

			// Token: 0x04001188 RID: 4488
			public const string ManageAnySubscriptionDescription = "ManageAnySubscriptionDescription";

			// Token: 0x04001189 RID: 4489
			public const string ViewDatasourceDescription = "ViewDatasourceDescription";

			// Token: 0x0400118A RID: 4490
			public const string ManageDatasourceDescription = "ManageDatasourceDescription";

			// Token: 0x0400118B RID: 4491
			public const string ViewModelsDescription = "ViewModelsDescription";

			// Token: 0x0400118C RID: 4492
			public const string ManageModelsDescription = "ManageModelsDescription";

			// Token: 0x0400118D RID: 4493
			public const string ConsumeReportsDescription = "ConsumeReportsDescription";

			// Token: 0x0400118E RID: 4494
			public const string CommentDescription = "CommentDescription";

			// Token: 0x0400118F RID: 4495
			public const string ManageCommentsDescription = "ManageCommentsDescription";

			// Token: 0x04001190 RID: 4496
			public const string ManageRolesName = "ManageRolesName";

			// Token: 0x04001191 RID: 4497
			public const string ViewSystemPropertiesName = "ViewSystemPropertiesName";

			// Token: 0x04001192 RID: 4498
			public const string ManageSystemPropertiesName = "ManageSystemPropertiesName";

			// Token: 0x04001193 RID: 4499
			public const string ViewSharedSchedulesName = "ViewSharedSchedulesName";

			// Token: 0x04001194 RID: 4500
			public const string ManageSharedSchedulesName = "ManageSharedSchedulesName";

			// Token: 0x04001195 RID: 4501
			public const string ManageSystemSecurityName = "ManageSystemSecurityName";

			// Token: 0x04001196 RID: 4502
			public const string GenerateEventsName = "GenerateEventsName";

			// Token: 0x04001197 RID: 4503
			public const string ManageJobsName = "ManageJobsName";

			// Token: 0x04001198 RID: 4504
			public const string ExecuteReportDefinitionsName = "ExecuteReportDefinitionsName";

			// Token: 0x04001199 RID: 4505
			public const string ManageRolesDescription = "ManageRolesDescription";

			// Token: 0x0400119A RID: 4506
			public const string ViewSystemPropertiesDescription = "ViewSystemPropertiesDescription";

			// Token: 0x0400119B RID: 4507
			public const string ManageSystemPropertiesDescription = "ManageSystemPropertiesDescription";

			// Token: 0x0400119C RID: 4508
			public const string ViewSharedSchedulesDescription = "ViewSharedSchedulesDescription";

			// Token: 0x0400119D RID: 4509
			public const string ManageSharedSchedulesDescription = "ManageSharedSchedulesDescription";

			// Token: 0x0400119E RID: 4510
			public const string ManageSystemSecurityDescription = "ManageSystemSecurityDescription";

			// Token: 0x0400119F RID: 4511
			public const string GenerateEventsDescription = "GenerateEventsDescription";

			// Token: 0x040011A0 RID: 4512
			public const string ManageJobsDescription = "ManageJobsDescription";

			// Token: 0x040011A1 RID: 4513
			public const string ExecuteReportDefinitionsDescription = "ExecuteReportDefinitionsDescription";

			// Token: 0x040011A2 RID: 4514
			public const string ViewModelItemsName = "ViewModelItemsName";

			// Token: 0x040011A3 RID: 4515
			public const string ViewModelItemsDescription = "ViewModelItemsDescription";
		}
	}
}
