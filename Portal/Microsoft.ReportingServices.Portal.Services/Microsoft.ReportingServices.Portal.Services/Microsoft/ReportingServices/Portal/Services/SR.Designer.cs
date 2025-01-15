using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.Portal.Services
{
	// Token: 0x02000021 RID: 33
	[CompilerGenerated]
	internal class SR
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x00002C7C File Offset: 0x00000E7C
		protected SR()
		{
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000CCD8 File Offset: 0x0000AED8
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000CCDF File Offset: 0x0000AEDF
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000CCE7 File Offset: 0x0000AEE7
		public static string Error_RSConfigContainsNoReportsApplicationUrls
		{
			get
			{
				return SR.Keys.GetString("Error_RSConfigContainsNoReportsApplicationUrls");
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000CCF3 File Offset: 0x0000AEF3
		public static string Error_CatalogItemNotFound
		{
			get
			{
				return SR.Keys.GetString("Error_CatalogItemNotFound");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000CCFF File Offset: 0x0000AEFF
		public static string Error_DataSetNotFound
		{
			get
			{
				return SR.Keys.GetString("Error_DataSetNotFound");
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000CD0B File Offset: 0x0000AF0B
		public static string Error_CatalogItemContentInvalid
		{
			get
			{
				return SR.Keys.GetString("Error_CatalogItemContentInvalid");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000CD17 File Offset: 0x0000AF17
		public static string Error_CatalogItemPropertyInvalid
		{
			get
			{
				return SR.Keys.GetString("Error_CatalogItemPropertyInvalid");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000CD23 File Offset: 0x0000AF23
		public static string Error_CatalogItemInvalid
		{
			get
			{
				return SR.Keys.GetString("Error_CatalogItemInvalid");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000CD2F File Offset: 0x0000AF2F
		public static string Error_AccessDenied
		{
			get
			{
				return SR.Keys.GetString("Error_AccessDenied");
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000CD3B File Offset: 0x0000AF3B
		public static string Error_CatalogItemAlreadyExists
		{
			get
			{
				return SR.Keys.GetString("Error_CatalogItemAlreadyExists");
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000CD47 File Offset: 0x0000AF47
		public static string Error_ExpectedUserToken
		{
			get
			{
				return SR.Keys.GetString("Error_ExpectedUserToken");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000CD53 File Offset: 0x0000AF53
		public static string Error_DataSetSoapError
		{
			get
			{
				return SR.Keys.GetString("Error_DataSetSoapError");
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000CD5F File Offset: 0x0000AF5F
		public static string Error_DataSetMaxRowsLessThanOne
		{
			get
			{
				return SR.Keys.GetString("Error_DataSetMaxRowsLessThanOne");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000CD6B File Offset: 0x0000AF6B
		public static string Error_InvalidDataSourceType
		{
			get
			{
				return SR.Keys.GetString("Error_InvalidDataSourceType");
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000CD77 File Offset: 0x0000AF77
		public static string Error_SystemResourcePackageException
		{
			get
			{
				return SR.Keys.GetString("Error_SystemResourcePackageException");
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000CD83 File Offset: 0x0000AF83
		public static string Error_SystemResourceProcessingException
		{
			get
			{
				return SR.Keys.GetString("Error_SystemResourceProcessingException");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000CD8F File Offset: 0x0000AF8F
		public static string Error_UniversalBrandInvalidColorsFile
		{
			get
			{
				return SR.Keys.GetString("Error_UniversalBrandInvalidColorsFile");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000CD9B File Offset: 0x0000AF9B
		public static string Error_InvalidDataFormat
		{
			get
			{
				return SR.Keys.GetString("Error_InvalidDataFormat");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000CDA7 File Offset: 0x0000AFA7
		public static string Error_UserPrincipalIsNotSet
		{
			get
			{
				return SR.Keys.GetString("Error_UserPrincipalIsNotSet");
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000CDB3 File Offset: 0x0000AFB3
		public static string Error_DatabaseDowngradeDetected
		{
			get
			{
				return SR.Keys.GetString("Error_DatabaseDowngradeDetected");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000CDBF File Offset: 0x0000AFBF
		public static string Error_PbiToRsAttachDetected
		{
			get
			{
				return SR.Keys.GetString("Error_PbiToRsAttachDetected");
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000CDCB File Offset: 0x0000AFCB
		public static string Error_EncryptionFailure
		{
			get
			{
				return SR.Keys.GetString("Error_EncryptionFailure");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000CDD7 File Offset: 0x0000AFD7
		public static string Error_InvalidScheduleRecurrenceObject
		{
			get
			{
				return SR.Keys.GetString("Error_InvalidScheduleRecurrenceObject");
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000CDE3 File Offset: 0x0000AFE3
		public static string Error_InvalidExcelWopiUrl
		{
			get
			{
				return SR.Keys.GetString("Error_InvalidExcelWopiUrl");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000CDEF File Offset: 0x0000AFEF
		public static string Error_InvalidExcelWopiDiscovery
		{
			get
			{
				return SR.Keys.GetString("Error_InvalidExcelWopiDiscovery");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000CDFB File Offset: 0x0000AFFB
		public static string Error_IEIsNotSupported
		{
			get
			{
				return SR.Keys.GetString("Error_IEIsNotSupported");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000CE07 File Offset: 0x0000B007
		public static string Error_InvalidPowerBIMigrateUrl
		{
			get
			{
				return SR.Keys.GetString("Error_InvalidPowerBIMigrateUrl");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000CE13 File Offset: 0x0000B013
		public static string SupportedBrowsers
		{
			get
			{
				return SR.Keys.GetString("SupportedBrowsers");
			}
		}

		// Token: 0x02000146 RID: 326
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x0600086B RID: 2155 RVA: 0x00002C7C File Offset: 0x00000E7C
			private Keys()
			{
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x0600086C RID: 2156 RVA: 0x00020048 File Offset: 0x0001E248
			// (set) Token: 0x0600086D RID: 2157 RVA: 0x0002004F File Offset: 0x0001E24F
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

			// Token: 0x0600086E RID: 2158 RVA: 0x00020057 File Offset: 0x0001E257
			public static string GetString(string key)
			{
				return SR.Keys.resourceManager.GetString(key, SR.Keys._culture);
			}

			// Token: 0x04000377 RID: 887
			private static ResourceManager resourceManager = new ResourceManager(typeof(SR).FullName, typeof(SR).Module.Assembly);

			// Token: 0x04000378 RID: 888
			private static CultureInfo _culture = null;

			// Token: 0x04000379 RID: 889
			public const string Error_RSConfigContainsNoReportsApplicationUrls = "Error_RSConfigContainsNoReportsApplicationUrls";

			// Token: 0x0400037A RID: 890
			public const string Error_CatalogItemNotFound = "Error_CatalogItemNotFound";

			// Token: 0x0400037B RID: 891
			public const string Error_DataSetNotFound = "Error_DataSetNotFound";

			// Token: 0x0400037C RID: 892
			public const string Error_CatalogItemContentInvalid = "Error_CatalogItemContentInvalid";

			// Token: 0x0400037D RID: 893
			public const string Error_CatalogItemPropertyInvalid = "Error_CatalogItemPropertyInvalid";

			// Token: 0x0400037E RID: 894
			public const string Error_CatalogItemInvalid = "Error_CatalogItemInvalid";

			// Token: 0x0400037F RID: 895
			public const string Error_AccessDenied = "Error_AccessDenied";

			// Token: 0x04000380 RID: 896
			public const string Error_CatalogItemAlreadyExists = "Error_CatalogItemAlreadyExists";

			// Token: 0x04000381 RID: 897
			public const string Error_ExpectedUserToken = "Error_ExpectedUserToken";

			// Token: 0x04000382 RID: 898
			public const string Error_DataSetSoapError = "Error_DataSetSoapError";

			// Token: 0x04000383 RID: 899
			public const string Error_DataSetMaxRowsLessThanOne = "Error_DataSetMaxRowsLessThanOne";

			// Token: 0x04000384 RID: 900
			public const string Error_InvalidDataSourceType = "Error_InvalidDataSourceType";

			// Token: 0x04000385 RID: 901
			public const string Error_SystemResourcePackageException = "Error_SystemResourcePackageException";

			// Token: 0x04000386 RID: 902
			public const string Error_SystemResourceProcessingException = "Error_SystemResourceProcessingException";

			// Token: 0x04000387 RID: 903
			public const string Error_UniversalBrandInvalidColorsFile = "Error_UniversalBrandInvalidColorsFile";

			// Token: 0x04000388 RID: 904
			public const string Error_InvalidDataFormat = "Error_InvalidDataFormat";

			// Token: 0x04000389 RID: 905
			public const string Error_UserPrincipalIsNotSet = "Error_UserPrincipalIsNotSet";

			// Token: 0x0400038A RID: 906
			public const string Error_DatabaseDowngradeDetected = "Error_DatabaseDowngradeDetected";

			// Token: 0x0400038B RID: 907
			public const string Error_PbiToRsAttachDetected = "Error_PbiToRsAttachDetected";

			// Token: 0x0400038C RID: 908
			public const string Error_EncryptionFailure = "Error_EncryptionFailure";

			// Token: 0x0400038D RID: 909
			public const string Error_InvalidScheduleRecurrenceObject = "Error_InvalidScheduleRecurrenceObject";

			// Token: 0x0400038E RID: 910
			public const string Error_InvalidExcelWopiUrl = "Error_InvalidExcelWopiUrl";

			// Token: 0x0400038F RID: 911
			public const string Error_InvalidExcelWopiDiscovery = "Error_InvalidExcelWopiDiscovery";

			// Token: 0x04000390 RID: 912
			public const string Error_IEIsNotSupported = "Error_IEIsNotSupported";

			// Token: 0x04000391 RID: 913
			public const string Error_InvalidPowerBIMigrateUrl = "Error_InvalidPowerBIMigrateUrl";

			// Token: 0x04000392 RID: 914
			public const string SupportedBrowsers = "SupportedBrowsers";
		}
	}
}
