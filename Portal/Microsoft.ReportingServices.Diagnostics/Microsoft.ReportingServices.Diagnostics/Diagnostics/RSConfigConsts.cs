using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200004D RID: 77
	internal static class RSConfigConsts
	{
		// Token: 0x0400022B RID: 555
		public const string Configuration = "Configuration";

		// Token: 0x0400022C RID: 556
		public const string Extensions = "Extensions";

		// Token: 0x0400022D RID: 557
		public const string Add = "Add";

		// Token: 0x0400022E RID: 558
		public const string DSN = "Dsn";

		// Token: 0x0400022F RID: 559
		public const string ConnectionString = "ConnectionString";

		// Token: 0x04000230 RID: 560
		public const string ConnectionType = "ConnectionType";

		// Token: 0x04000231 RID: 561
		public const string CatalogUser = "LogonUser";

		// Token: 0x04000232 RID: 562
		public const string CatalogDomain = "LogonDomain";

		// Token: 0x04000233 RID: 563
		public const string CatalogCred = "LogonCred";

		// Token: 0x04000234 RID: 564
		public const string DefaultViewerStyleSheet = "HTMLViewerStyleSheet";

		// Token: 0x04000235 RID: 565
		public const string Service = "Service";

		// Token: 0x04000236 RID: 566
		public const string Key = "Key";

		// Token: 0x04000237 RID: 567
		public const string Value = "Value";

		// Token: 0x04000238 RID: 568
		public const string IsSchedulingService = "IsSchedulingService";

		// Token: 0x04000239 RID: 569
		public const string IsDataModelRefreshService = "IsDataModelRefreshService";

		// Token: 0x0400023A RID: 570
		public const string IsAlertingService = "IsAlertingService";

		// Token: 0x0400023B RID: 571
		public const string IsNotificationService = "IsNotificationService";

		// Token: 0x0400023C RID: 572
		public const string IsEventService = "IsEventService";

		// Token: 0x0400023D RID: 573
		public const string PollingInterval = "PollingInterval";

		// Token: 0x0400023E RID: 574
		public const string MaxCatalogConnectionPoolSizePerProcess = "MaxCatalogConnectionPoolSizePerProcess";

		// Token: 0x0400023F RID: 575
		public const string WorkingSetMinimum = "WorkingSetMinimum";

		// Token: 0x04000240 RID: 576
		public const string WorkingSetMaximum = "WorkingSetMaximum";

		// Token: 0x04000241 RID: 577
		public const string MemorySafetyMargin = "MemorySafetyMargin";

		// Token: 0x04000242 RID: 578
		public const string MemoryThreshold = "MemoryThreshold";

		// Token: 0x04000243 RID: 579
		public const string RecycleTime = "RecycleTime";

		// Token: 0x04000244 RID: 580
		public const string WebServiceAccount = "WebServiceAccount";

		// Token: 0x04000245 RID: 581
		public const string MaxAppDomainUnloadTime = "MaxAppDomainUnloadTime";

		// Token: 0x04000246 RID: 582
		public const string MaxQueueThreads = "MaxQueueThreads";

		// Token: 0x04000247 RID: 583
		public const string DisplayErrorLink = "DisplayErrorLink";

		// Token: 0x04000248 RID: 584
		public const string DisableSecureFormsAuthenticationCookie = "DisableSecureFormsAuthenticationCookie";

		// Token: 0x04000249 RID: 585
		public const string UrlRoot = "UrlRoot";

		// Token: 0x0400024A RID: 586
		public const string PolicyLevel = "PolicyLevel";

		// Token: 0x0400024B RID: 587
		public const string WebServiceUseFileShareStorage = "WebServiceUseFileShareStorage";

		// Token: 0x0400024C RID: 588
		public const string WindowsServiceUseFileShareStorage = "WindowsServiceUseFileShareStorage";

		// Token: 0x0400024D RID: 589
		public const string FileShareStorageLocation = "FileShareStorageLocation";

		// Token: 0x0400024E RID: 590
		public const string FileShareStoragePath = "Path";

		// Token: 0x0400024F RID: 591
		public const string SharePointIntegration = "SharePointIntegration";

		// Token: 0x04000250 RID: 592
		public const string EnablePowerBIFeatures = "EnablePowerBIFeatures";

		// Token: 0x04000251 RID: 593
		public const string PowerBIConnectionConfiguration = "PowerBIConnectionConfiguration";

		// Token: 0x04000252 RID: 594
		public const string Default = "Default";

		// Token: 0x04000253 RID: 595
		public const string Extension = "Extension";

		// Token: 0x04000254 RID: 596
		public const string ReportItem = "ReportItem";

		// Token: 0x04000255 RID: 597
		public const string UserName = "UserName";

		// Token: 0x04000256 RID: 598
		public const string Type = "Type";

		// Token: 0x04000257 RID: 599
		public const string Visible = "Visible";

		// Token: 0x04000258 RID: 600
		public const string LogAllExecutionRequests = "LogAllExecutionRequests";

		// Token: 0x04000259 RID: 601
		public const string MaxRetries = "MaxRetries";

		// Token: 0x0400025A RID: 602
		public const string SecondsBeforeRetry = "SecondsBeforeRetry";

		// Token: 0x0400025B RID: 603
		public const string DefaultDeliveryExtension = "DefaultDeliveryExtension";

		// Token: 0x0400025C RID: 604
		public const string EventElement = "Event";

		// Token: 0x0400025D RID: 605
		public const string EventType = "Type";

		// Token: 0x0400025E RID: 606
		public const string CanSubscribeElement = "CanSubscribe";

		// Token: 0x0400025F RID: 607
		public const string UI = "UI";

		// Token: 0x04000260 RID: 608
		public const string DatabaseQueryTimeout = "DatabaseQueryTimeout";

		// Token: 0x04000261 RID: 609
		public const int DatabaseQueryTimeoutDefault = 30;

		// Token: 0x04000262 RID: 610
		public const string ProcessTimeout = "ProcessTimeout";

		// Token: 0x04000263 RID: 611
		public const int ProcessTimeoutDefault = 150;

		// Token: 0x04000264 RID: 612
		public const string ProcessTimeoutGcExtension = "ProcessTimeoutGcExtension";

		// Token: 0x04000265 RID: 613
		public const int ProcessTimeoutGcExtensionDefault = 30;

		// Token: 0x04000266 RID: 614
		public const string DatabaseCleanupTimeout = "DatabaseCleanupTimeout";

		// Token: 0x04000267 RID: 615
		public const int DatabaseCleanupTimeoutDefault = 420;

		// Token: 0x04000268 RID: 616
		public const int UserNameSidRefreshDatabaseTimeoutDefault = 1800;

		// Token: 0x04000269 RID: 617
		public const int UpdatePoliciesSecondsDefault = 30;

		// Token: 0x0400026A RID: 618
		public const int UpdatePoliciesChunkSizeDefault = 500;

		// Token: 0x0400026B RID: 619
		public const string ConnectionTimeout = "ConnectionTimeout";

		// Token: 0x0400026C RID: 620
		public const int ConnectionTimeoutDefault = 120;

		// Token: 0x0400026D RID: 621
		public const string DatabaseCleanupBatchFactor = "DatabaseCleanupBatchFactor";

		// Token: 0x0400026E RID: 622
		public const int DatabaseCleanupBatchFactorDefault = 1000;

		// Token: 0x0400026F RID: 623
		public const string OverrideNames = "OverrideNames";

		// Token: 0x04000270 RID: 624
		public const string DeviceInfo = "DeviceInfo";

		// Token: 0x04000271 RID: 625
		public const string Language = "Language";

		// Token: 0x04000272 RID: 626
		public const string InstallationIDConfigSection = "InstallationID";

		// Token: 0x04000273 RID: 627
		public const string InstallationIDWebAppConfigSection = "InstallationIDWebApp";

		// Token: 0x04000274 RID: 628
		public const string InstanceIDConfigSection = "InstanceId";

		// Token: 0x04000275 RID: 629
		public const string InstanceNameConfigSection = "InstanceName";

		// Token: 0x04000276 RID: 630
		public const string CleanupCycleMinutesConfigSection = "CleanupCycleMinutes";

		// Token: 0x04000277 RID: 631
		public const string DailyCleanupMinuteOfDay = "DailyCleanupMinuteOfDay";

		// Token: 0x04000278 RID: 632
		public const string AlertingCleanupCycleMinutesConfigSection = "AlertingCleanupCycleMinutes";

		// Token: 0x04000279 RID: 633
		public const string AlertingDataCleanupMinutesConfigSection = "AlertingDataCleanupMinutes";

		// Token: 0x0400027A RID: 634
		public const string AlertingExecutionLogCleanupMinutesConfigSection = "AlertingExecutionLogCleanupMinutes";

		// Token: 0x0400027B RID: 635
		public const string AlertingMaxDataRetentionDaysConfigSection = "AlertingMaxDataRetentionDays";

		// Token: 0x0400027C RID: 636
		public const string RunningRequestsScavengerCycleConfigSection = "RunningRequestsScavengerCycle";

		// Token: 0x0400027D RID: 637
		public const string RunningRequestsDBCycleConfigSection = "RunningRequestsDbCycle";

		// Token: 0x0400027E RID: 638
		public const string RunningRequestsAgeConfigSection = "RunningRequestsAge";

		// Token: 0x0400027F RID: 639
		public const string MaxActiveReqForOneUser = "MaxActiveReqForOneUser";

		// Token: 0x04000280 RID: 640
		public const string MaxScheduleWait = "MaxScheduleWait";

		// Token: 0x04000281 RID: 641
		public const string ProcessRecycleOptionsConfigSection = "ProcessRecycleOptions";

		// Token: 0x04000282 RID: 642
		public const string WatsonFlagsConfigSection = "WatsonFlags";

		// Token: 0x04000283 RID: 643
		public const string WatsonDumpOnExceptionsConfigSection = "WatsonDumpOnExceptions";

		// Token: 0x04000284 RID: 644
		public const string WatsonDumpExcludeIfContainsExceptionsConfigSection = "WatsonDumpExcludeIfContainsExceptions";

		// Token: 0x04000285 RID: 645
		public const string UnattendedExecutionAccount = "UnattendedExecutionAccount";

		// Token: 0x04000286 RID: 646
		public const string Name = "Name";

		// Token: 0x04000287 RID: 647
		public const string Domain = "Domain";

		// Token: 0x04000288 RID: 648
		public const string Password = "Password";

		// Token: 0x04000289 RID: 649
		public const string Converter = "Converter";

		// Token: 0x0400028A RID: 650
		public const string Source = "Source";

		// Token: 0x0400028B RID: 651
		public const string Target = "Target";

		// Token: 0x0400028C RID: 652
		public const string IsWebServiceEnabled = "IsWebServiceEnabled";

		// Token: 0x0400028D RID: 653
		public const string IsReportManagerEnabled = "IsReportManagerEnabled";

		// Token: 0x0400028E RID: 654
		public const string IsReportBuilderAnonymousAccessEnabled = "IsReportBuilderAnonymousAccessEnabled";

		// Token: 0x0400028F RID: 655
		public const string UrlReservations = "URLReservations";

		// Token: 0x04000290 RID: 656
		public const string Application = "Application";

		// Token: 0x04000291 RID: 657
		public const string ReportManager = "ReportManager";

		// Token: 0x04000292 RID: 658
		public const string ReportServerWebService = "ReportServerWebService";

		// Token: 0x04000293 RID: 659
		public const string ReportServerWebApp = "ReportServerWebApp";

		// Token: 0x04000294 RID: 660
		public const string Urls = "URLs";

		// Token: 0x04000295 RID: 661
		public const string Url = "URL";

		// Token: 0x04000296 RID: 662
		public const string UrlString = "UrlString";

		// Token: 0x04000297 RID: 663
		public const string VirtualDirectory = "VirtualDirectory";

		// Token: 0x04000298 RID: 664
		public const string Authentication = "Authentication";

		// Token: 0x04000299 RID: 665
		public const string AuthenticationTypes = "AuthenticationTypes";

		// Token: 0x0400029A RID: 666
		public const string RSWindowsBasic = "RSWindowsBasic";

		// Token: 0x0400029B RID: 667
		public const string RSWindowsNTLM = "RSWindowsNTLM";

		// Token: 0x0400029C RID: 668
		public const string RSWindowsKerberos = "RSWindowsKerberos";

		// Token: 0x0400029D RID: 669
		public const string RSWindowsNegotiate = "RSWindowsNegotiate";

		// Token: 0x0400029E RID: 670
		public const string Custom = "Custom";

		// Token: 0x0400029F RID: 671
		public const string RSForms = "RSForms";

		// Token: 0x040002A0 RID: 672
		public const string OAuth = "OAuth";

		// Token: 0x040002A1 RID: 673
		public const string LogonMethod = "LogonMethod";

		// Token: 0x040002A2 RID: 674
		public const string AuthRealm = "Realm";

		// Token: 0x040002A3 RID: 675
		public const string AuthDomain = "DefaultDomain";

		// Token: 0x040002A4 RID: 676
		public const string AuthTokenCacheMaxSize = "AuthTokenCacheMaxSize";

		// Token: 0x040002A5 RID: 677
		public const string AuthTokenCacheMaintenanceInterval = "AuthTokenCacheMaintenanceInterval";

		// Token: 0x040002A6 RID: 678
		public const string AuthTokenCacheLogonTimeout = "AuthTokenCacheLogonTimeout";

		// Token: 0x040002A7 RID: 679
		public const string AuthTokenCacheEntryTimeout = "AuthTokenCacheEntryTimeout";

		// Token: 0x040002A8 RID: 680
		public const string AuthPersistence = "EnableAuthPersistence";

		// Token: 0x040002A9 RID: 681
		public const string MaxUnauthenticatedRequests = "MaxUnauthenticatedRequests";

		// Token: 0x040002AA RID: 682
		public const string UnauthenticatedRequestWindow = "UnauthenticatedRequestWindow";

		// Token: 0x040002AB RID: 683
		public const string UnauthenticatedRequestLockoutTime = "UnauthenticatedRequestLockoutTime";

		// Token: 0x040002AC RID: 684
		public const string Hostname = "Hostname";

		// Token: 0x040002AD RID: 685
		public const string IsRdceEnabled = "IsRdceEnabled";

		// Token: 0x040002AE RID: 686
		public const string DefaultFileShareAccount = "DefaultFileShareAccount";

		// Token: 0x040002AF RID: 687
		internal const string RequestCacheSlots = "RequestCacheSlots";

		// Token: 0x040002B0 RID: 688
		internal const string RecordDelimiter = "RecordDelimiter";

		// Token: 0x040002B1 RID: 689
		internal const string FieldDelimiter = "FieldDelimiter";

		// Token: 0x040002B2 RID: 690
		internal const string RDLSandboxing = "RDLSandboxing";

		// Token: 0x040002B3 RID: 691
		internal const string MapTileServerConfiguration = "MapTileServerConfiguration";

		// Token: 0x040002B4 RID: 692
		public const string ExtendedProtectionLevel = "RSWindowsExtendedProtectionLevel";

		// Token: 0x040002B5 RID: 693
		public const string ExtendedProtectionScenario = "RSWindowsExtendedProtectionScenario";

		// Token: 0x040002B6 RID: 694
		public const string UsernameSIDRefreshMinutes = "UsernameSIDRefreshMinutes";

		// Token: 0x040002B7 RID: 695
		public const string UpdatePoliciesSeconds = "UpdatePoliciesSeconds";

		// Token: 0x040002B8 RID: 696
		public const string UpdatePoliciesChunkSize = "UpdatePoliciesChunkSize";
	}
}
