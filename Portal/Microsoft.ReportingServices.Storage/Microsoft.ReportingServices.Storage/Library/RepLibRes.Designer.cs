using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000042 RID: 66
	[CompilerGenerated]
	internal class RepLibRes
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x00002A10 File Offset: 0x00000C10
		protected RepLibRes()
		{
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000A198 File Offset: 0x00008398
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000A19F File Offset: 0x0000839F
		public static CultureInfo Culture
		{
			get
			{
				return RepLibRes.Keys.Culture;
			}
			set
			{
				RepLibRes.Keys.Culture = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000A1A7 File Offset: 0x000083A7
		public static string SimpleMessage
		{
			get
			{
				return RepLibRes.Keys.GetString("SimpleMessage");
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000A1B3 File Offset: 0x000083B3
		public static string MyReportsFolderName
		{
			get
			{
				return RepLibRes.Keys.GetString("MyReportsFolderName");
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000A1BF File Offset: 0x000083BF
		public static string MyReportsFolderDescription
		{
			get
			{
				return RepLibRes.Keys.GetString("MyReportsFolderDescription");
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000A1CB File Offset: 0x000083CB
		public static string UsersFolderName
		{
			get
			{
				return RepLibRes.Keys.GetString("UsersFolderName");
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000A1D7 File Offset: 0x000083D7
		public static string AM
		{
			get
			{
				return RepLibRes.Keys.GetString("AM");
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000A1E3 File Offset: 0x000083E3
		public static string PM
		{
			get
			{
				return RepLibRes.Keys.GetString("PM");
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000A1EF File Offset: 0x000083EF
		public static string TaskComment
		{
			get
			{
				return RepLibRes.Keys.GetString("TaskComment");
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000A1FB File Offset: 0x000083FB
		public static string ReportServiceCategoryName
		{
			get
			{
				return RepLibRes.Keys.GetString("ReportServiceCategoryName");
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000A207 File Offset: 0x00008407
		public static string ServerProductName
		{
			get
			{
				return RepLibRes.Keys.GetString("ServerProductName");
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000A213 File Offset: 0x00008413
		public static string ReportHistorySnapShotCreatedHandler
		{
			get
			{
				return RepLibRes.Keys.GetString("ReportHistorySnapShotCreatedHandler");
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000A21F File Offset: 0x0000841F
		public static string ReportExecutionSnapshotUpdateHandler
		{
			get
			{
				return RepLibRes.Keys.GetString("ReportExecutionSnapshotUpdateHandler");
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000A22B File Offset: 0x0000842B
		public static string CacheInvalidateScheduleHanlder
		{
			get
			{
				return RepLibRes.Keys.GetString("CacheInvalidateScheduleHanlder");
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000A237 File Offset: 0x00008437
		public static string TimedSubscriptions
		{
			get
			{
				return RepLibRes.Keys.GetString("TimedSubscriptions");
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000A243 File Offset: 0x00008443
		public static string CacheRefreshPlans
		{
			get
			{
				return RepLibRes.Keys.GetString("CacheRefreshPlans");
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000A24F File Offset: 0x0000844F
		public static string SharedDatasetCacheUpdatePlans
		{
			get
			{
				return RepLibRes.Keys.GetString("SharedDatasetCacheUpdatePlans");
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000A25B File Offset: 0x0000845B
		public static string CanNotSubscribe
		{
			get
			{
				return RepLibRes.Keys.GetString("CanNotSubscribe");
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000A267 File Offset: 0x00008467
		public static string NewSubscription
		{
			get
			{
				return RepLibRes.Keys.GetString("NewSubscription");
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000A273 File Offset: 0x00008473
		public static string NewCacheRefreshPlan
		{
			get
			{
				return RepLibRes.Keys.GetString("NewCacheRefreshPlan");
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000A27F File Offset: 0x0000847F
		public static string NewScheduledRefreshPlan
		{
			get
			{
				return RepLibRes.Keys.GetString("NewScheduledRefreshPlan");
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000A28B File Offset: 0x0000848B
		public static string DeliveryExtensionRemoved
		{
			get
			{
				return RepLibRes.Keys.GetString("DeliveryExtensionRemoved");
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000A297 File Offset: 0x00008497
		public static string DeliveryExtensionFailedToLoad
		{
			get
			{
				return RepLibRes.Keys.GetString("DeliveryExtensionFailedToLoad");
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000A2A3 File Offset: 0x000084A3
		public static string SubscriptionDisabled
		{
			get
			{
				return RepLibRes.Keys.GetString("SubscriptionDisabled");
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000A2AF File Offset: 0x000084AF
		public static string SubscriptionReady
		{
			get
			{
				return RepLibRes.Keys.GetString("SubscriptionReady");
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000A2BB File Offset: 0x000084BB
		public static string SubscriptionExecuted
		{
			get
			{
				return RepLibRes.Keys.GetString("SubscriptionExecuted");
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000A2C7 File Offset: 0x000084C7
		public static string SubscriptionPending
		{
			get
			{
				return RepLibRes.Keys.GetString("SubscriptionPending");
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000A2D3 File Offset: 0x000084D3
		public static string ScheduleFiredEventHandlerName
		{
			get
			{
				return RepLibRes.Keys.GetString("ScheduleFiredEventHandlerName");
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000A2DF File Offset: 0x000084DF
		public static string SubscriptionParametersInvalid
		{
			get
			{
				return RepLibRes.Keys.GetString("SubscriptionParametersInvalid");
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000A2EB File Offset: 0x000084EB
		public static string RefreshCachePlanParametersInvalid
		{
			get
			{
				return RepLibRes.Keys.GetString("RefreshCachePlanParametersInvalid");
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000A2F7 File Offset: 0x000084F7
		public static string RefreshCachePlanCachingIsNotEnabled
		{
			get
			{
				return RepLibRes.Keys.GetString("RefreshCachePlanCachingIsNotEnabled");
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000A303 File Offset: 0x00008503
		public static string RefreshCachePlanSharedDataSourceRemoved
		{
			get
			{
				return RepLibRes.Keys.GetString("RefreshCachePlanSharedDataSourceRemoved");
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000A30F File Offset: 0x0000850F
		public static string RefreshCachePlanSuccess
		{
			get
			{
				return RepLibRes.Keys.GetString("RefreshCachePlanSuccess");
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000A31B File Offset: 0x0000851B
		public static string RefreshCachePlanRefreshed
		{
			get
			{
				return RepLibRes.Keys.GetString("RefreshCachePlanRefreshed");
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000A327 File Offset: 0x00008527
		public static string SubscriptionMissingEncryptedContent
		{
			get
			{
				return RepLibRes.Keys.GetString("SubscriptionMissingEncryptedContent");
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000A333 File Offset: 0x00008533
		public static string ExecutionAccountLogonError
		{
			get
			{
				return RepLibRes.Keys.GetString("ExecutionAccountLogonError");
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000A33F File Offset: 0x0000853F
		public static string ParentDirectoryLink
		{
			get
			{
				return RepLibRes.Keys.GetString("ParentDirectoryLink");
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000A34B File Offset: 0x0000854B
		public static string SharePointModeCatalogNotSupported
		{
			get
			{
				return RepLibRes.Keys.GetString("SharePointModeCatalogNotSupported");
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000A357 File Offset: 0x00008557
		public static string UnableToAllocateILockBytes
		{
			get
			{
				return RepLibRes.Keys.GetString("UnableToAllocateILockBytes");
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000A363 File Offset: 0x00008563
		public static string UnableToCreateStorage
		{
			get
			{
				return RepLibRes.Keys.GetString("UnableToCreateStorage");
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000A36F File Offset: 0x0000856F
		public static string UnableToReadData
		{
			get
			{
				return RepLibRes.Keys.GetString("UnableToReadData");
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000A37B File Offset: 0x0000857B
		public static string PreviewImageDataInvalid
		{
			get
			{
				return RepLibRes.Keys.GetString("PreviewImageDataInvalid");
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000A387 File Offset: 0x00008587
		public static string DisallowedExcelFileExtensionChange
		{
			get
			{
				return RepLibRes.Keys.GetString("DisallowedExcelFileExtensionChange");
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A393 File Offset: 0x00008593
		public static string FileOpenError(string filename, string description)
		{
			return RepLibRes.Keys.GetString("FileOpenError", filename, description);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A3A1 File Offset: 0x000085A1
		public static string ProgressMessage(int iterationCount)
		{
			return RepLibRes.Keys.GetString("ProgressMessage", iterationCount);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A3B3 File Offset: 0x000085B3
		public static string SubscriptionError(string errorMessage)
		{
			return RepLibRes.Keys.GetString("SubscriptionError", errorMessage);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A3C0 File Offset: 0x000085C0
		public static string SubscriptionProcessing(int processed, int total, int errors)
		{
			return RepLibRes.Keys.GetString("SubscriptionProcessing", processed, total, errors);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A3DE File Offset: 0x000085DE
		public static string SubscriptionDone(int processed, int total, int errors)
		{
			return RepLibRes.Keys.GetString("SubscriptionDone", processed, total, errors);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A3FC File Offset: 0x000085FC
		public static string DataDrivenSubscriptionError(string errorText)
		{
			return RepLibRes.Keys.GetString("DataDrivenSubscriptionError", errorText);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A409 File Offset: 0x00008609
		public static string RefreshCachePlanError(string errorMessage)
		{
			return RepLibRes.Keys.GetString("RefreshCachePlanError", errorMessage);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000A416 File Offset: 0x00008616
		public static string RPCErrorStringFormat(string errorMessage, string errorCode)
		{
			return RepLibRes.Keys.GetString("RPCErrorStringFormat", errorMessage, errorCode);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000A424 File Offset: 0x00008624
		public static string DisallowedResourceExtensionError(string fileExtension)
		{
			return RepLibRes.Keys.GetString("DisallowedResourceExtensionError", fileExtension);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A431 File Offset: 0x00008631
		public static string DisallowedResourceMimeType(string fileExtension)
		{
			return RepLibRes.Keys.GetString("DisallowedResourceMimeType", fileExtension);
		}

		// Token: 0x0200005A RID: 90
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x060002BA RID: 698 RVA: 0x00002A10 File Offset: 0x00000C10
			private Keys()
			{
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x060002BB RID: 699 RVA: 0x0000B2E8 File Offset: 0x000094E8
			// (set) Token: 0x060002BC RID: 700 RVA: 0x0000B2EF File Offset: 0x000094EF
			public static CultureInfo Culture
			{
				get
				{
					return RepLibRes.Keys._culture;
				}
				set
				{
					RepLibRes.Keys._culture = value;
				}
			}

			// Token: 0x060002BD RID: 701 RVA: 0x0000B2F7 File Offset: 0x000094F7
			public static string GetString(string key)
			{
				return RepLibRes.Keys.resourceManager.GetString(key, RepLibRes.Keys._culture);
			}

			// Token: 0x060002BE RID: 702 RVA: 0x0000B309 File Offset: 0x00009509
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, RepLibRes.Keys.resourceManager.GetString(key, RepLibRes.Keys._culture), arg0);
			}

			// Token: 0x060002BF RID: 703 RVA: 0x0000B326 File Offset: 0x00009526
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, RepLibRes.Keys.resourceManager.GetString(key, RepLibRes.Keys._culture), arg0, arg1);
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x0000B344 File Offset: 0x00009544
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, RepLibRes.Keys.resourceManager.GetString(key, RepLibRes.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x04000232 RID: 562
			private static ResourceManager resourceManager = new ResourceManager(typeof(RepLibRes).FullName, typeof(RepLibRes).Module.Assembly);

			// Token: 0x04000233 RID: 563
			private static CultureInfo _culture = null;

			// Token: 0x04000234 RID: 564
			public const string SimpleMessage = "SimpleMessage";

			// Token: 0x04000235 RID: 565
			public const string FileOpenError = "FileOpenError";

			// Token: 0x04000236 RID: 566
			public const string ProgressMessage = "ProgressMessage";

			// Token: 0x04000237 RID: 567
			public const string MyReportsFolderName = "MyReportsFolderName";

			// Token: 0x04000238 RID: 568
			public const string MyReportsFolderDescription = "MyReportsFolderDescription";

			// Token: 0x04000239 RID: 569
			public const string UsersFolderName = "UsersFolderName";

			// Token: 0x0400023A RID: 570
			public const string AM = "AM";

			// Token: 0x0400023B RID: 571
			public const string PM = "PM";

			// Token: 0x0400023C RID: 572
			public const string TaskComment = "TaskComment";

			// Token: 0x0400023D RID: 573
			public const string ReportServiceCategoryName = "ReportServiceCategoryName";

			// Token: 0x0400023E RID: 574
			public const string ServerProductName = "ServerProductName";

			// Token: 0x0400023F RID: 575
			public const string ReportHistorySnapShotCreatedHandler = "ReportHistorySnapShotCreatedHandler";

			// Token: 0x04000240 RID: 576
			public const string ReportExecutionSnapshotUpdateHandler = "ReportExecutionSnapshotUpdateHandler";

			// Token: 0x04000241 RID: 577
			public const string CacheInvalidateScheduleHanlder = "CacheInvalidateScheduleHanlder";

			// Token: 0x04000242 RID: 578
			public const string TimedSubscriptions = "TimedSubscriptions";

			// Token: 0x04000243 RID: 579
			public const string CacheRefreshPlans = "CacheRefreshPlans";

			// Token: 0x04000244 RID: 580
			public const string SharedDatasetCacheUpdatePlans = "SharedDatasetCacheUpdatePlans";

			// Token: 0x04000245 RID: 581
			public const string CanNotSubscribe = "CanNotSubscribe";

			// Token: 0x04000246 RID: 582
			public const string NewSubscription = "NewSubscription";

			// Token: 0x04000247 RID: 583
			public const string NewCacheRefreshPlan = "NewCacheRefreshPlan";

			// Token: 0x04000248 RID: 584
			public const string NewScheduledRefreshPlan = "NewScheduledRefreshPlan";

			// Token: 0x04000249 RID: 585
			public const string DeliveryExtensionRemoved = "DeliveryExtensionRemoved";

			// Token: 0x0400024A RID: 586
			public const string DeliveryExtensionFailedToLoad = "DeliveryExtensionFailedToLoad";

			// Token: 0x0400024B RID: 587
			public const string SubscriptionDisabled = "SubscriptionDisabled";

			// Token: 0x0400024C RID: 588
			public const string SubscriptionReady = "SubscriptionReady";

			// Token: 0x0400024D RID: 589
			public const string SubscriptionExecuted = "SubscriptionExecuted";

			// Token: 0x0400024E RID: 590
			public const string SubscriptionError = "SubscriptionError";

			// Token: 0x0400024F RID: 591
			public const string SubscriptionPending = "SubscriptionPending";

			// Token: 0x04000250 RID: 592
			public const string SubscriptionProcessing = "SubscriptionProcessing";

			// Token: 0x04000251 RID: 593
			public const string SubscriptionDone = "SubscriptionDone";

			// Token: 0x04000252 RID: 594
			public const string DataDrivenSubscriptionError = "DataDrivenSubscriptionError";

			// Token: 0x04000253 RID: 595
			public const string ScheduleFiredEventHandlerName = "ScheduleFiredEventHandlerName";

			// Token: 0x04000254 RID: 596
			public const string SubscriptionParametersInvalid = "SubscriptionParametersInvalid";

			// Token: 0x04000255 RID: 597
			public const string RefreshCachePlanParametersInvalid = "RefreshCachePlanParametersInvalid";

			// Token: 0x04000256 RID: 598
			public const string RefreshCachePlanCachingIsNotEnabled = "RefreshCachePlanCachingIsNotEnabled";

			// Token: 0x04000257 RID: 599
			public const string RefreshCachePlanSharedDataSourceRemoved = "RefreshCachePlanSharedDataSourceRemoved";

			// Token: 0x04000258 RID: 600
			public const string RefreshCachePlanSuccess = "RefreshCachePlanSuccess";

			// Token: 0x04000259 RID: 601
			public const string RefreshCachePlanRefreshed = "RefreshCachePlanRefreshed";

			// Token: 0x0400025A RID: 602
			public const string RefreshCachePlanError = "RefreshCachePlanError";

			// Token: 0x0400025B RID: 603
			public const string SubscriptionMissingEncryptedContent = "SubscriptionMissingEncryptedContent";

			// Token: 0x0400025C RID: 604
			public const string RPCErrorStringFormat = "RPCErrorStringFormat";

			// Token: 0x0400025D RID: 605
			public const string ExecutionAccountLogonError = "ExecutionAccountLogonError";

			// Token: 0x0400025E RID: 606
			public const string ParentDirectoryLink = "ParentDirectoryLink";

			// Token: 0x0400025F RID: 607
			public const string SharePointModeCatalogNotSupported = "SharePointModeCatalogNotSupported";

			// Token: 0x04000260 RID: 608
			public const string UnableToAllocateILockBytes = "UnableToAllocateILockBytes";

			// Token: 0x04000261 RID: 609
			public const string UnableToCreateStorage = "UnableToCreateStorage";

			// Token: 0x04000262 RID: 610
			public const string UnableToReadData = "UnableToReadData";

			// Token: 0x04000263 RID: 611
			public const string PreviewImageDataInvalid = "PreviewImageDataInvalid";

			// Token: 0x04000264 RID: 612
			public const string DisallowedResourceExtensionError = "DisallowedResourceExtensionError";

			// Token: 0x04000265 RID: 613
			public const string DisallowedResourceMimeType = "DisallowedResourceMimeType";

			// Token: 0x04000266 RID: 614
			public const string DisallowedExcelFileExtensionChange = "DisallowedExcelFileExtensionChange";
		}
	}
}
