using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000DA RID: 218
	[CompilerGenerated]
	internal class XmlaSR
	{
		// Token: 0x060009E0 RID: 2528 RVA: 0x0002B2AF File Offset: 0x000294AF
		protected XmlaSR()
		{
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0002B2B7 File Offset: 0x000294B7
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x0002B2BE File Offset: 0x000294BE
		public static CultureInfo Culture
		{
			get
			{
				return XmlaSR.Keys.Culture;
			}
			set
			{
				XmlaSR.Keys.Culture = value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0002B2C6 File Offset: 0x000294C6
		public static string AlreadyConnected
		{
			get
			{
				return XmlaSR.Keys.GetString("AlreadyConnected");
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0002B2D2 File Offset: 0x000294D2
		public static string NotConnected
		{
			get
			{
				return XmlaSR.Keys.GetString("NotConnected");
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0002B2DE File Offset: 0x000294DE
		public static string CannotConnect
		{
			get
			{
				return XmlaSR.Keys.GetString("CannotConnect");
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002B2EA File Offset: 0x000294EA
		public static string CannotConnectToRedirector
		{
			get
			{
				return XmlaSR.Keys.GetString("CannotConnectToRedirector");
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0002B2F6 File Offset: 0x000294F6
		public static string ConnectionBroken
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionBroken");
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0002B302 File Offset: 0x00029502
		public static string Reconnect_ConnectionInfoIsMissing
		{
			get
			{
				return XmlaSR.Keys.GetString("Reconnect_ConnectionInfoIsMissing");
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0002B30E File Offset: 0x0002950E
		public static string Reconnect_SessionIDIsMissing
		{
			get
			{
				return XmlaSR.Keys.GetString("Reconnect_SessionIDIsMissing");
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0002B31A File Offset: 0x0002951A
		public static string ServerDidNotProvideErrorInfo
		{
			get
			{
				return XmlaSR.Keys.GetString("ServerDidNotProvideErrorInfo");
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0002B326 File Offset: 0x00029526
		public static string ConnectionCannotBeUsedWhileXmlReaderOpened
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionCannotBeUsedWhileXmlReaderOpened");
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0002B332 File Offset: 0x00029532
		public static string Connect_RedirectorDidntReturnDatabaseInfo
		{
			get
			{
				return XmlaSR.Keys.GetString("Connect_RedirectorDidntReturnDatabaseInfo");
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0002B33E File Offset: 0x0002953E
		public static string Connection_WorkbookIsOutdated
		{
			get
			{
				return XmlaSR.Keys.GetString("Connection_WorkbookIsOutdated");
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0002B34A File Offset: 0x0002954A
		public static string Connection_AnalysisServicesInstanceWasMoved
		{
			get
			{
				return XmlaSR.Keys.GetString("Connection_AnalysisServicesInstanceWasMoved");
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0002B356 File Offset: 0x00029556
		public static string NetCore_NotSupportedFeature
		{
			get
			{
				return XmlaSR.Keys.GetString("NetCore_NotSupportedFeature");
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0002B362 File Offset: 0x00029562
		public static string NetCore_WindowsOnlySupportedFeature
		{
			get
			{
				return XmlaSR.Keys.GetString("NetCore_WindowsOnlySupportedFeature");
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0002B36E File Offset: 0x0002956E
		public static string NetCore_WindowsDesktopOnlySupportedFeature
		{
			get
			{
				return XmlaSR.Keys.GetString("NetCore_WindowsDesktopOnlySupportedFeature");
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0002B37A File Offset: 0x0002957A
		public static string FailedToResolveCluster
		{
			get
			{
				return XmlaSR.Keys.GetString("FailedToResolveCluster");
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0002B386 File Offset: 0x00029586
		public static string SoapFormatter_ResponseIsNotRowset
		{
			get
			{
				return XmlaSR.Keys.GetString("SoapFormatter_ResponseIsNotRowset");
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0002B392 File Offset: 0x00029592
		public static string SoapFormatter_ResponseIsNotDataset
		{
			get
			{
				return XmlaSR.Keys.GetString("SoapFormatter_ResponseIsNotDataset");
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0002B39E File Offset: 0x0002959E
		public static string Cancel_SessionIDNotSpecified
		{
			get
			{
				return XmlaSR.Keys.GetString("Cancel_SessionIDNotSpecified");
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0002B3AA File Offset: 0x000295AA
		public static string ConnectionString_Invalid
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_Invalid");
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0002B3B6 File Offset: 0x000295B6
		public static string ConnectionString_DataSourceNotSpecified
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_DataSourceNotSpecified");
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0002B3C2 File Offset: 0x000295C2
		public static string ConnectionString_MissingIdentityProviderForIntegratedSecurityFederated
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_MissingIdentityProviderForIntegratedSecurityFederated");
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0002B3CE File Offset: 0x000295CE
		public static string ConnectionString_InvalidIdentityProviderForIntegratedSecurityFederated
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_InvalidIdentityProviderForIntegratedSecurityFederated");
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0002B3DA File Offset: 0x000295DA
		public static string ConnectionString_DataSourceTypeDoesntSupportQuery
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_DataSourceTypeDoesntSupportQuery");
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0002B3E6 File Offset: 0x000295E6
		public static string ConnectionString_LinkFileInvalidServer
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_LinkFileInvalidServer");
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002B3F2 File Offset: 0x000295F2
		public static string ConnectionString_LinkFileMissingServer
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_LinkFileMissingServer");
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0002B3FE File Offset: 0x000295FE
		public static string ConnectionString_LinkFileDupEffectiveUsername
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_LinkFileDupEffectiveUsername");
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0002B40A File Offset: 0x0002960A
		public static string ConnectionString_LinkFileCannotRevert
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_LinkFileCannotRevert");
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0002B416 File Offset: 0x00029616
		public static string ConnectionString_LinkFileCannotDelegate
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_LinkFileCannotDelegate");
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0002B422 File Offset: 0x00029622
		public static string ConnectionString_MissingPassword
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_MissingPassword");
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0002B42E File Offset: 0x0002962E
		public static string ConnectionString_AsAzure_DataSourcePathMoreThanOneSegment
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_AsAzure_DataSourcePathMoreThanOneSegment");
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0002B43A File Offset: 0x0002963A
		public static string ConnectionString_PbiDedicated_MissingInitialCatalog
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_PbiDedicated_MissingInitialCatalog");
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0002B446 File Offset: 0x00029646
		public static string ConnectionString_PbiDedicated_MissingRestrictCatalog
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_PbiDedicated_MissingRestrictCatalog");
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0002B452 File Offset: 0x00029652
		public static string ConnectionString_AsAzure_SspiAndUseAdalCacheTogetherNotSupported
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_AsAzure_SspiAndUseAdalCacheTogetherNotSupported");
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0002B45E File Offset: 0x0002965E
		public static string ConnectionString_Untrusted_Endpoint
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_Untrusted_Endpoint");
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0002B46A File Offset: 0x0002966A
		public static string ConnectionString_SPN_Profile_Not_Supported
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_SPN_Profile_Not_Supported");
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0002B476 File Offset: 0x00029676
		public static string UnknownServerResponseFormat
		{
			get
			{
				return XmlaSR.Keys.GetString("UnknownServerResponseFormat");
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0002B482 File Offset: 0x00029682
		public static string AfterExceptionAllTagsShouldCloseUntilMessagesSection
		{
			get
			{
				return XmlaSR.Keys.GetString("AfterExceptionAllTagsShouldCloseUntilMessagesSection");
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0002B48E File Offset: 0x0002968E
		public static string ErrorCodeIsMissingFromRowsetError
		{
			get
			{
				return XmlaSR.Keys.GetString("ErrorCodeIsMissingFromRowsetError");
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0002B49A File Offset: 0x0002969A
		public static string ErrorCodeIsMissingFromDatasetError
		{
			get
			{
				return XmlaSR.Keys.GetString("ErrorCodeIsMissingFromDatasetError");
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002B4A6 File Offset: 0x000296A6
		public static string ExceptionRequiresXmlaErrorsInMessagesSection
		{
			get
			{
				return XmlaSR.Keys.GetString("ExceptionRequiresXmlaErrorsInMessagesSection");
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002B4B2 File Offset: 0x000296B2
		public static string MessagesSectionIsEmpty
		{
			get
			{
				return XmlaSR.Keys.GetString("MessagesSectionIsEmpty");
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0002B4BE File Offset: 0x000296BE
		public static string EmptyRootIsNotEmpty
		{
			get
			{
				return XmlaSR.Keys.GetString("EmptyRootIsNotEmpty");
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0002B4CA File Offset: 0x000296CA
		public static string Resultset_IsNotRowset
		{
			get
			{
				return XmlaSR.Keys.GetString("Resultset_IsNotRowset");
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0002B4D6 File Offset: 0x000296D6
		public static string DataReaderClosedError
		{
			get
			{
				return XmlaSR.Keys.GetString("DataReaderClosedError");
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0002B4E2 File Offset: 0x000296E2
		public static string DataReaderInvalidRowError
		{
			get
			{
				return XmlaSR.Keys.GetString("DataReaderInvalidRowError");
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0002B4EE File Offset: 0x000296EE
		public static string NonSequentialColumnAccessError
		{
			get
			{
				return XmlaSR.Keys.GetString("NonSequentialColumnAccessError");
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0002B4FA File Offset: 0x000296FA
		public static string DataReader_IndexOutOfRange
		{
			get
			{
				return XmlaSR.Keys.GetString("DataReader_IndexOutOfRange");
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0002B506 File Offset: 0x00029706
		public static string Authentication_Failed
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_Failed");
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0002B512 File Offset: 0x00029712
		public static string Authentication_Sspi_SchannelCantDelegate
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_Sspi_SchannelCantDelegate");
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0002B51E File Offset: 0x0002971E
		public static string Authentication_Sspi_SchannelSupportsOnlyPrivacyLevel
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_Sspi_SchannelSupportsOnlyPrivacyLevel");
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0002B52A File Offset: 0x0002972A
		public static string Authentication_Sspi_SchannelUnsupportedImpersonationLevel
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_Sspi_SchannelUnsupportedImpersonationLevel");
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0002B536 File Offset: 0x00029736
		public static string Authentication_Sspi_SchannelUnsupportedProtectionLevel
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_Sspi_SchannelUnsupportedProtectionLevel");
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0002B542 File Offset: 0x00029742
		public static string Authentication_Sspi_SchannelAnonymousAmbiguity
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_Sspi_SchannelAnonymousAmbiguity");
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0002B54E File Offset: 0x0002974E
		public static string Authentication_MsoID_MissingSignInAssistant
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_MsoID_MissingSignInAssistant");
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0002B55A File Offset: 0x0002975A
		public static string Authentication_MsoID_InternalError
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_MsoID_InternalError");
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0002B566 File Offset: 0x00029766
		public static string Authentication_MsoID_InvalidCredentials
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_MsoID_InvalidCredentials");
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0002B572 File Offset: 0x00029772
		public static string Authentication_MsoID_SsoFailed
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_MsoID_SsoFailed");
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0002B57E File Offset: 0x0002977E
		public static string Authentication_MsoID_SsoFailedNonDomainUser
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_MsoID_SsoFailedNonDomainUser");
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0002B58A File Offset: 0x0002978A
		public static string Authentication_ClaimsToken_AuthorityNotFound
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_ClaimsToken_AuthorityNotFound");
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0002B596 File Offset: 0x00029796
		public static string Authentication_ClaimsToken_UserIdAndPasswordRequired
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_ClaimsToken_UserIdAndPasswordRequired");
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0002B5A2 File Offset: 0x000297A2
		public static string Authentication_ClaimsToken_IdentityProviderFormatInvalid
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_ClaimsToken_IdentityProviderFormatInvalid");
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0002B5AE File Offset: 0x000297AE
		public static string Authentication_AsAzure_OnlySspiOrClaimsTokenSupported
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_AsAzure_OnlySspiOrClaimsTokenSupported");
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0002B5BA File Offset: 0x000297BA
		public static string Authentication_PbiDedicated_OnlyClaimsTokenSupported
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_PbiDedicated_OnlyClaimsTokenSupported");
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0002B5C6 File Offset: 0x000297C6
		public static string DimeReader_CannotReadFromStream
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeReader_CannotReadFromStream");
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002B5D2 File Offset: 0x000297D2
		public static string DimeReader_IsClosed
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeReader_IsClosed");
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0002B5DE File Offset: 0x000297DE
		public static string DimeReader_PreviousRecordStreamStillOpened
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeReader_PreviousRecordStreamStillOpened");
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0002B5EA File Offset: 0x000297EA
		public static string DimeRecord_StreamShouldBeReadable
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_StreamShouldBeReadable");
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0002B5F6 File Offset: 0x000297F6
		public static string DimeRecord_StreamShouldBeWriteable
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_StreamShouldBeWriteable");
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0002B602 File Offset: 0x00029802
		public static string DimeRecord_InvalidContentLength
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_InvalidContentLength");
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0002B60E File Offset: 0x0002980E
		public static string DimeRecord_PropertyOnlyAvailableForReadRecords
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_PropertyOnlyAvailableForReadRecords");
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0002B61A File Offset: 0x0002981A
		public static string DimeRecord_InvalidChunkSize
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_InvalidChunkSize");
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0002B626 File Offset: 0x00029826
		public static string DimeRecord_UnableToReadFromStream
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_UnableToReadFromStream");
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0002B632 File Offset: 0x00029832
		public static string DimeRecord_StreamIsClosed
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_StreamIsClosed");
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x0002B63E File Offset: 0x0002983E
		public static string DimeRecord_ReadNotAllowed
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_ReadNotAllowed");
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0002B64A File Offset: 0x0002984A
		public static string DimeRecord_WriteNotAllowed
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_WriteNotAllowed");
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0002B656 File Offset: 0x00029856
		public static string DimeRecord_TypeFormatEnumUnchangedNotAllowed
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_TypeFormatEnumUnchangedNotAllowed");
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0002B662 File Offset: 0x00029862
		public static string DimeRecord_MediaTypeNotDefined
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_MediaTypeNotDefined");
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0002B66E File Offset: 0x0002986E
		public static string DimeRecord_NameMustNotBeDefinedForFormatNone
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_NameMustNotBeDefinedForFormatNone");
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0002B67A File Offset: 0x0002987A
		public static string DimeRecord_EncodedTypeLengthExceeds8191
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_EncodedTypeLengthExceeds8191");
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0002B686 File Offset: 0x00029886
		public static string DimeRecord_OffsetAndCountShouldBePositive
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_OffsetAndCountShouldBePositive");
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0002B692 File Offset: 0x00029892
		public static string DimeRecord_ContentLengthExceeded
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_ContentLengthExceeded");
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0002B69E File Offset: 0x0002989E
		public static string DimeRecord_OnlySingleRecordMessagesAreSupported
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_OnlySingleRecordMessagesAreSupported");
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0002B6AA File Offset: 0x000298AA
		public static string DimeRecord_DataTypeShouldBeSpecifiedOnTheFirstChunk
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_DataTypeShouldBeSpecifiedOnTheFirstChunk");
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002B6B6 File Offset: 0x000298B6
		public static string DimeRecord_DataTypeIsOnlyForTheFirstChunk
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_DataTypeIsOnlyForTheFirstChunk");
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0002B6C2 File Offset: 0x000298C2
		public static string DimeRecord_IDIsOnlyForFirstChunk
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_IDIsOnlyForFirstChunk");
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002B6CE File Offset: 0x000298CE
		public static string DimeWriter_CannotWriteToStream
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeWriter_CannotWriteToStream");
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0002B6DA File Offset: 0x000298DA
		public static string DimeWriter_WriterIsClosed
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeWriter_WriterIsClosed");
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0002B6E6 File Offset: 0x000298E6
		public static string DimeWriter_InvalidDefaultChunkSize
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeWriter_InvalidDefaultChunkSize");
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0002B6F2 File Offset: 0x000298F2
		public static string TcpStream_MaxSignatureExceedsProtocolLimit
		{
			get
			{
				return XmlaSR.Keys.GetString("TcpStream_MaxSignatureExceedsProtocolLimit");
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0002B6FE File Offset: 0x000298FE
		public static string HttpStream_RequestPayloadStream_InvalidStreamOperation
		{
			get
			{
				return XmlaSR.Keys.GetString("HttpStream_RequestPayloadStream_InvalidStreamOperation");
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0002B70A File Offset: 0x0002990A
		public static string HttpStream_RequestPayloadStream_WriteAfterComplete
		{
			get
			{
				return XmlaSR.Keys.GetString("HttpStream_RequestPayloadStream_WriteAfterComplete");
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0002B716 File Offset: 0x00029916
		public static string HttpStream_RequestPayloadStream_InvalidAsyncResultType
		{
			get
			{
				return XmlaSR.Keys.GetString("HttpStream_RequestPayloadStream_InvalidAsyncResultType");
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0002B722 File Offset: 0x00029922
		public static string HttpStream_RequestPayloadStream_EndReadAlreadyCalled
		{
			get
			{
				return XmlaSR.Keys.GetString("HttpStream_RequestPayloadStream_EndReadAlreadyCalled");
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0002B72E File Offset: 0x0002992E
		public static string HttpStream_RequestPayloadStream_ErrorInCallback
		{
			get
			{
				return XmlaSR.Keys.GetString("HttpStream_RequestPayloadStream_ErrorInCallback");
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0002B73A File Offset: 0x0002993A
		public static string IXMLAInterop_OnlyZeroOffsetIsSupported
		{
			get
			{
				return XmlaSR.Keys.GetString("IXMLAInterop_OnlyZeroOffsetIsSupported");
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0002B746 File Offset: 0x00029946
		public static string IXMLAInterop_StreamDoesNotSupportReverting
		{
			get
			{
				return XmlaSR.Keys.GetString("IXMLAInterop_StreamDoesNotSupportReverting");
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0002B752 File Offset: 0x00029952
		public static string IXMLAInterop_StreamDoesNotSupportLocking
		{
			get
			{
				return XmlaSR.Keys.GetString("IXMLAInterop_StreamDoesNotSupportLocking");
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0002B75E File Offset: 0x0002995E
		public static string IXMLAInterop_StreamDoesNotSupportUnlocking
		{
			get
			{
				return XmlaSR.Keys.GetString("IXMLAInterop_StreamDoesNotSupportUnlocking");
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0002B76A File Offset: 0x0002996A
		public static string IXMLAInterop_StreamCannotBeCloned
		{
			get
			{
				return XmlaSR.Keys.GetString("IXMLAInterop_StreamCannotBeCloned");
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0002B776 File Offset: 0x00029976
		public static string XmlaClient_StartRequest_ThereIsAnotherPendingRequest
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_StartRequest_ThereIsAnotherPendingRequest");
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0002B782 File Offset: 0x00029982
		public static string XmlaClient_StartRequest_ThereIsAnotherPendingResponse
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_StartRequest_ThereIsAnotherPendingResponse");
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0002B78E File Offset: 0x0002998E
		public static string XmlaClient_SendRequest_RequestStreamCannotBeRead
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_SendRequest_RequestStreamCannotBeRead");
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0002B79A File Offset: 0x0002999A
		public static string XmlaClient_SendRequest_NoRequestWasCreated
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_SendRequest_NoRequestWasCreated");
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0002B7A6 File Offset: 0x000299A6
		public static string XmlaClient_ConnectTimedOut
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_ConnectTimedOut");
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0002B7B2 File Offset: 0x000299B2
		public static string XmlaClient_SendRequest_ThereIsAnotherPendingResponse
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_SendRequest_ThereIsAnotherPendingResponse");
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0002B7BE File Offset: 0x000299BE
		public static string XmlaClient_CannotConnectToLocalCubeWithRestictedClient
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_CannotConnectToLocalCubeWithRestictedClient");
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0002B7CA File Offset: 0x000299CA
		public static string XmlaClient_PbiPublicXmla_DatasetNotSpecified
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmla_DatasetNotSpecified");
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0002B7D6 File Offset: 0x000299D6
		public static string XmlaClient_PbiPublicXmlaWithEmbedToken_ToPBIShared_NotSupported
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmlaWithEmbedToken_ToPBIShared_NotSupported");
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0002B7E2 File Offset: 0x000299E2
		public static string XmlaClient_PbiPublicXmlaWithEmbedToken_UserId_Prop_NotSupported
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmlaWithEmbedToken_UserId_Prop_NotSupported");
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0002B7EE File Offset: 0x000299EE
		public static string XmlaClient_PbiPublicXmlaWithEmbedToken_Missing_InitialCatalog
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmlaWithEmbedToken_Missing_InitialCatalog");
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0002B7FA File Offset: 0x000299FA
		public static string XmlaClient_ASAzureRedirectionResolutionFailedWithThrottling
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_ASAzureRedirectionResolutionFailedWithThrottling");
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0002B806 File Offset: 0x00029A06
		public static string Decompression_InitializationFailed
		{
			get
			{
				return XmlaSR.Keys.GetString("Decompression_InitializationFailed");
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0002B812 File Offset: 0x00029A12
		public static string Compression_InitializationFailed
		{
			get
			{
				return XmlaSR.Keys.GetString("Compression_InitializationFailed");
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0002B81E File Offset: 0x00029A1E
		public static string InvalidArgument
		{
			get
			{
				return XmlaSR.Keys.GetString("InvalidArgument");
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0002B82A File Offset: 0x00029A2A
		public static string ProvidePath
		{
			get
			{
				return XmlaSR.Keys.GetString("ProvidePath");
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0002B836 File Offset: 0x00029A36
		public static string InternalError
		{
			get
			{
				return XmlaSR.Keys.GetString("InternalError");
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0002B842 File Offset: 0x00029A42
		public static string InternalErrorAndInvalidBufferType
		{
			get
			{
				return XmlaSR.Keys.GetString("InternalErrorAndInvalidBufferType");
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002B84E File Offset: 0x00029A4E
		public static string LocalCube_FileNotOpened(string cubeFile)
		{
			return XmlaSR.Keys.GetString("LocalCube_FileNotOpened", cubeFile);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002B85B File Offset: 0x00029A5B
		public static string Instance_NotFound(string instance, string server)
		{
			return XmlaSR.Keys.GetString("Instance_NotFound", instance, server);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002B869 File Offset: 0x00029A69
		public static string UnexpectedXsiType(string type)
		{
			return XmlaSR.Keys.GetString("UnexpectedXsiType", type);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002B876 File Offset: 0x00029A76
		public static string ConnectionString_InvalidPropertyNameFormat(string propertyName)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidPropertyNameFormat", propertyName);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002B883 File Offset: 0x00029A83
		public static string ConnectionString_UnsupportedPropertyValue(string propertyName, string value)
		{
			return XmlaSR.Keys.GetString("ConnectionString_UnsupportedPropertyValue", propertyName, value);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002B891 File Offset: 0x00029A91
		public static string ConnectionString_OpenedQuoteIsNotClosed(char openQuoteChar, int index)
		{
			return XmlaSR.Keys.GetString("ConnectionString_OpenedQuoteIsNotClosed", openQuoteChar, index);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002B8A9 File Offset: 0x00029AA9
		public static string ConnectionString_ExpectedSemicolonNotFound(int index)
		{
			return XmlaSR.Keys.GetString("ConnectionString_ExpectedSemicolonNotFound", index);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002B8BB File Offset: 0x00029ABB
		public static string ConnectionString_ExpectedEqualSignNotFound(int fromIndex)
		{
			return XmlaSR.Keys.GetString("ConnectionString_ExpectedEqualSignNotFound", fromIndex);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002B8CD File Offset: 0x00029ACD
		public static string ConnectionString_InvalidCharInPropertyName(char invalidChar, int index)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidCharInPropertyName", invalidChar, index);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002B8E5 File Offset: 0x00029AE5
		public static string ConnectionString_InvalidCharInUnquotedPropertyValue(char invalidChar, int index)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidCharInUnquotedPropertyValue", invalidChar, index);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002B8FD File Offset: 0x00029AFD
		public static string ConnectionString_PropertyNameNotDefined(int equalIndex)
		{
			return XmlaSR.Keys.GetString("ConnectionString_PropertyNameNotDefined", equalIndex);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002B90F File Offset: 0x00029B0F
		public static string ConnectionString_InvalidIntegratedSecurityForNative(string integratedSecurity)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidIntegratedSecurityForNative", integratedSecurity);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002B91C File Offset: 0x00029B1C
		public static string ConnectionString_InvalidProtectionLevelForHttp(string protectionLevel)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidProtectionLevelForHttp", protectionLevel);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002B929 File Offset: 0x00029B29
		public static string ConnectionString_InvalidProtectionLevelForHttps(string protectionLevel)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidProtectionLevelForHttps", protectionLevel);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002B936 File Offset: 0x00029B36
		public static string ConnectionString_InvalidImpersonationLevelForHttp(string impersonationLevel)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidImpersonationLevelForHttp", impersonationLevel);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002B943 File Offset: 0x00029B43
		public static string ConnectionString_InvalidImpersonationLevelForHttps(string impersonationLevel)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidImpersonationLevelForHttps", impersonationLevel);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002B950 File Offset: 0x00029B50
		public static string ConnectionString_InvalidIntegratedSecurityForHttpOrHttps(string integratedSecurity)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidIntegratedSecurityForHttpOrHttps", integratedSecurity);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002B95D File Offset: 0x00029B5D
		public static string ConnectionString_PropertyNotApplicableWithTheDataSourceType(string propertyName)
		{
			return XmlaSR.Keys.GetString("ConnectionString_PropertyNotApplicableWithTheDataSourceType", propertyName);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002B96A File Offset: 0x00029B6A
		public static string ConnectionString_LinkFileParseError(int size)
		{
			return XmlaSR.Keys.GetString("ConnectionString_LinkFileParseError", size);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002B97C File Offset: 0x00029B7C
		public static string ConnectionString_LinkFileDownloadError(string linkFileName)
		{
			return XmlaSR.Keys.GetString("ConnectionString_LinkFileDownloadError", linkFileName);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002B989 File Offset: 0x00029B89
		public static string ConnectionString_ExternalConnectionIsIncomplete(string missingPropertyName)
		{
			return XmlaSR.Keys.GetString("ConnectionString_ExternalConnectionIsIncomplete", missingPropertyName);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002B996 File Offset: 0x00029B96
		public static string ConnectionString_ASAzure_FetchLinkReferenceFailed(string linkFileUri)
		{
			return XmlaSR.Keys.GetString("ConnectionString_ASAzure_FetchLinkReferenceFailed", linkFileUri);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002B9A3 File Offset: 0x00029BA3
		public static string ConnectionString_ASAzure_InvalidLinkReferenceUri(string linkFileUri)
		{
			return XmlaSR.Keys.GetString("ConnectionString_ASAzure_InvalidLinkReferenceUri", linkFileUri);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002B9B0 File Offset: 0x00029BB0
		public static string ConnectionString_ASAzure_InvalidLinkReferenceCustomPort(string linkFileUri)
		{
			return XmlaSR.Keys.GetString("ConnectionString_ASAzure_InvalidLinkReferenceCustomPort", linkFileUri);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002B9BD File Offset: 0x00029BBD
		public static string ConnectionString_PbiDataset_Missing_Metadata(string missingParam)
		{
			return XmlaSR.Keys.GetString("ConnectionString_PbiDataset_Missing_Metadata", missingParam);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002B9CA File Offset: 0x00029BCA
		public static string ConnectionString_ShilohIsNoLongerSupported(string propertyName)
		{
			return XmlaSR.Keys.GetString("ConnectionString_ShilohIsNoLongerSupported", propertyName);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002B9D7 File Offset: 0x00029BD7
		public static string UnrecognizedElementInMessagesSection(string elementName)
		{
			return XmlaSR.Keys.GetString("UnrecognizedElementInMessagesSection", elementName);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002B9E4 File Offset: 0x00029BE4
		public static string UnexpectedElement(string elementName, string namespaceName)
		{
			return XmlaSR.Keys.GetString("UnexpectedElement", elementName, namespaceName);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002B9F2 File Offset: 0x00029BF2
		public static string MissingElement(string elementName, string namespaceName)
		{
			return XmlaSR.Keys.GetString("MissingElement", elementName, namespaceName);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002BA00 File Offset: 0x00029C00
		public static string Authentication_Sspi_PackageNotFound(string packageName)
		{
			return XmlaSR.Keys.GetString("Authentication_Sspi_PackageNotFound", packageName);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002BA0D File Offset: 0x00029C0D
		public static string Authentication_Sspi_PackageDoesntSupportCapability(string package, string capability)
		{
			return XmlaSR.Keys.GetString("Authentication_Sspi_PackageDoesntSupportCapability", package, capability);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002BA1B File Offset: 0x00029C1B
		public static string Authentication_Sspi_FlagNotEstablished(string flagName)
		{
			return XmlaSR.Keys.GetString("Authentication_Sspi_FlagNotEstablished", flagName);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002BA28 File Offset: 0x00029C28
		public static string Authentication_ClaimsToken_AdalLoadingError(string component)
		{
			return XmlaSR.Keys.GetString("Authentication_ClaimsToken_AdalLoadingError", component);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002BA35 File Offset: 0x00029C35
		public static string Authentication_ClaimsToken_AdalError(string message)
		{
			return XmlaSR.Keys.GetString("Authentication_ClaimsToken_AdalError", message);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002BA42 File Offset: 0x00029C42
		public static string DimeRecord_InvalidUriFormat(string uri)
		{
			return XmlaSR.Keys.GetString("DimeRecord_InvalidUriFormat", uri);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002BA4F File Offset: 0x00029C4F
		public static string DimeRecord_VersionNotSupported(int version)
		{
			return XmlaSR.Keys.GetString("DimeRecord_VersionNotSupported", version);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002BA61 File Offset: 0x00029C61
		public static string DimeRecord_TypeFormatShouldBeMedia(string value)
		{
			return XmlaSR.Keys.GetString("DimeRecord_TypeFormatShouldBeMedia", value);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002BA6E File Offset: 0x00029C6E
		public static string DimeRecord_TypeFormatShouldBeUnchanged(string value)
		{
			return XmlaSR.Keys.GetString("DimeRecord_TypeFormatShouldBeUnchanged", value);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002BA7B File Offset: 0x00029C7B
		public static string DimeRecord_ReservedFlagShouldBeZero(byte value)
		{
			return XmlaSR.Keys.GetString("DimeRecord_ReservedFlagShouldBeZero", value);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002BA8D File Offset: 0x00029C8D
		public static string DimeRecord_DataTypeNotSupported(string value)
		{
			return XmlaSR.Keys.GetString("DimeRecord_DataTypeNotSupported", value);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002BA9A File Offset: 0x00029C9A
		public static string DimeRecord_InvalidHeaderFlags(int begin, int end, int chunked)
		{
			return XmlaSR.Keys.GetString("DimeRecord_InvalidHeaderFlags", begin, end, chunked);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002BAB8 File Offset: 0x00029CB8
		public static string Dime_DataTypeNotSupported(string value)
		{
			return XmlaSR.Keys.GetString("Dime_DataTypeNotSupported", value);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0002BAC5 File Offset: 0x00029CC5
		public static string HttpStream_InvalidReadRequest(string state)
		{
			return XmlaSR.Keys.GetString("HttpStream_InvalidReadRequest", state);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002BAD2 File Offset: 0x00029CD2
		public static string HttpStream_ResponseWithFailedStatus(string status, string reasonPhrase)
		{
			return XmlaSR.Keys.GetString("HttpStream_ResponseWithFailedStatus", status, reasonPhrase);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002BAE0 File Offset: 0x00029CE0
		public static string HttpStream_RequestPayloadStream_ErrorInWrite(string error, string eol)
		{
			return XmlaSR.Keys.GetString("HttpStream_RequestPayloadStream_ErrorInWrite", error, eol);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002BAEE File Offset: 0x00029CEE
		public static string XmlaClient_PbiPublicXmla_InvalidDataSourceUriFormat(string uri)
		{
			return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmla_InvalidDataSourceUriFormat", uri);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002BAFB File Offset: 0x00029CFB
		public static string XmlaClient_PbiPremium_WorkspaceNotFound(string technicalDetails, string workspaceName)
		{
			return XmlaSR.Keys.GetString("XmlaClient_PbiPremium_WorkspaceNotFound", technicalDetails, workspaceName);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002BB09 File Offset: 0x00029D09
		public static string XmlaClient_PbiPremium_WorkspaceNotOnPremium(string technicalDetails, string workspaceName)
		{
			return XmlaSR.Keys.GetString("XmlaClient_PbiPremium_WorkspaceNotOnPremium", technicalDetails, workspaceName);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002BB17 File Offset: 0x00029D17
		public static string XmlaClient_PbiPremium_WorkspaceNameDuplicated(string technicalDetails, string workspaceName)
		{
			return XmlaSR.Keys.GetString("XmlaClient_PbiPremium_WorkspaceNameDuplicated", technicalDetails, workspaceName);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002BB25 File Offset: 0x00029D25
		public static string XmlaClient_PbiPublicXmla_DatasetNotFound(string datasetFriendlyName, string technicalDetails)
		{
			return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmla_DatasetNotFound", datasetFriendlyName, technicalDetails);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002BB33 File Offset: 0x00029D33
		public static string XmlaClient_PbiPublicXmla_DatasetNameDuplicated(string datasetFriendlyName, string technicalDetails)
		{
			return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmla_DatasetNameDuplicated", datasetFriendlyName, technicalDetails);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002BB41 File Offset: 0x00029D41
		public static string XmlaClient_PbiPublicXmla_UnsupportedIdentityProvider(string identityProvider)
		{
			return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmla_UnsupportedIdentityProvider", identityProvider);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002BB4E File Offset: 0x00029D4E
		public static string XmlaClient_ASAzureRedirectionResolutionFailedWithError(string aasInstance, string httpStatusCode)
		{
			return XmlaSR.Keys.GetString("XmlaClient_ASAzureRedirectionResolutionFailedWithError", aasInstance, httpStatusCode);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002BB5C File Offset: 0x00029D5C
		public static string XmlaClient_ASAzureRedirectionResolutionMissingTenantId(string aasInstance)
		{
			return XmlaSR.Keys.GetString("XmlaClient_ASAzureRedirectionResolutionMissingTenantId", aasInstance);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002BB69 File Offset: 0x00029D69
		public static string Decompression_Failed(int compressedSize, int expectedDecompressedSize, int actualDecompressedSize)
		{
			return XmlaSR.Keys.GetString("Decompression_Failed", compressedSize, expectedDecompressedSize, actualDecompressedSize);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002BB87 File Offset: 0x00029D87
		public static string UnsupportedDataFormat(string format)
		{
			return XmlaSR.Keys.GetString("UnsupportedDataFormat", format);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002BB94 File Offset: 0x00029D94
		public static string UnsupportedMethod(string name)
		{
			return XmlaSR.Keys.GetString("UnsupportedMethod", name);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002BBA1 File Offset: 0x00029DA1
		public static string DirectoryNotFound(string path)
		{
			return XmlaSR.Keys.GetString("DirectoryNotFound", path);
		}

		// Token: 0x0200019F RID: 415
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x0600131F RID: 4895 RVA: 0x0004334A File Offset: 0x0004154A
			private Keys()
			{
			}

			// Token: 0x1700062F RID: 1583
			// (get) Token: 0x06001320 RID: 4896 RVA: 0x00043352 File Offset: 0x00041552
			// (set) Token: 0x06001321 RID: 4897 RVA: 0x00043359 File Offset: 0x00041559
			public static CultureInfo Culture
			{
				get
				{
					return XmlaSR.Keys._culture;
				}
				set
				{
					XmlaSR.Keys._culture = value;
				}
			}

			// Token: 0x06001322 RID: 4898 RVA: 0x00043361 File Offset: 0x00041561
			public static string GetString(string key)
			{
				return XmlaSR.Keys.resourceManager.GetString(key, XmlaSR.Keys._culture);
			}

			// Token: 0x06001323 RID: 4899 RVA: 0x00043373 File Offset: 0x00041573
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, XmlaSR.Keys.resourceManager.GetString(key, XmlaSR.Keys._culture), arg0);
			}

			// Token: 0x06001324 RID: 4900 RVA: 0x00043390 File Offset: 0x00041590
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, XmlaSR.Keys.resourceManager.GetString(key, XmlaSR.Keys._culture), arg0, arg1);
			}

			// Token: 0x06001325 RID: 4901 RVA: 0x000433AE File Offset: 0x000415AE
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, XmlaSR.Keys.resourceManager.GetString(key, XmlaSR.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x04000C7C RID: 3196
			private static ResourceManager resourceManager = new ResourceManager(typeof(XmlaSR).FullName, typeof(XmlaSR).Module.Assembly);

			// Token: 0x04000C7D RID: 3197
			private static CultureInfo _culture = null;

			// Token: 0x04000C7E RID: 3198
			public const string AlreadyConnected = "AlreadyConnected";

			// Token: 0x04000C7F RID: 3199
			public const string NotConnected = "NotConnected";

			// Token: 0x04000C80 RID: 3200
			public const string LocalCube_FileNotOpened = "LocalCube_FileNotOpened";

			// Token: 0x04000C81 RID: 3201
			public const string CannotConnect = "CannotConnect";

			// Token: 0x04000C82 RID: 3202
			public const string CannotConnectToRedirector = "CannotConnectToRedirector";

			// Token: 0x04000C83 RID: 3203
			public const string ConnectionBroken = "ConnectionBroken";

			// Token: 0x04000C84 RID: 3204
			public const string Instance_NotFound = "Instance_NotFound";

			// Token: 0x04000C85 RID: 3205
			public const string Reconnect_ConnectionInfoIsMissing = "Reconnect_ConnectionInfoIsMissing";

			// Token: 0x04000C86 RID: 3206
			public const string Reconnect_SessionIDIsMissing = "Reconnect_SessionIDIsMissing";

			// Token: 0x04000C87 RID: 3207
			public const string ServerDidNotProvideErrorInfo = "ServerDidNotProvideErrorInfo";

			// Token: 0x04000C88 RID: 3208
			public const string UnexpectedXsiType = "UnexpectedXsiType";

			// Token: 0x04000C89 RID: 3209
			public const string ConnectionCannotBeUsedWhileXmlReaderOpened = "ConnectionCannotBeUsedWhileXmlReaderOpened";

			// Token: 0x04000C8A RID: 3210
			public const string Connect_RedirectorDidntReturnDatabaseInfo = "Connect_RedirectorDidntReturnDatabaseInfo";

			// Token: 0x04000C8B RID: 3211
			public const string Connection_WorkbookIsOutdated = "Connection_WorkbookIsOutdated";

			// Token: 0x04000C8C RID: 3212
			public const string Connection_AnalysisServicesInstanceWasMoved = "Connection_AnalysisServicesInstanceWasMoved";

			// Token: 0x04000C8D RID: 3213
			public const string NetCore_NotSupportedFeature = "NetCore_NotSupportedFeature";

			// Token: 0x04000C8E RID: 3214
			public const string NetCore_WindowsOnlySupportedFeature = "NetCore_WindowsOnlySupportedFeature";

			// Token: 0x04000C8F RID: 3215
			public const string NetCore_WindowsDesktopOnlySupportedFeature = "NetCore_WindowsDesktopOnlySupportedFeature";

			// Token: 0x04000C90 RID: 3216
			public const string FailedToResolveCluster = "FailedToResolveCluster";

			// Token: 0x04000C91 RID: 3217
			public const string SoapFormatter_ResponseIsNotRowset = "SoapFormatter_ResponseIsNotRowset";

			// Token: 0x04000C92 RID: 3218
			public const string SoapFormatter_ResponseIsNotDataset = "SoapFormatter_ResponseIsNotDataset";

			// Token: 0x04000C93 RID: 3219
			public const string Cancel_SessionIDNotSpecified = "Cancel_SessionIDNotSpecified";

			// Token: 0x04000C94 RID: 3220
			public const string ConnectionString_Invalid = "ConnectionString_Invalid";

			// Token: 0x04000C95 RID: 3221
			public const string ConnectionString_InvalidPropertyNameFormat = "ConnectionString_InvalidPropertyNameFormat";

			// Token: 0x04000C96 RID: 3222
			public const string ConnectionString_DataSourceNotSpecified = "ConnectionString_DataSourceNotSpecified";

			// Token: 0x04000C97 RID: 3223
			public const string ConnectionString_UnsupportedPropertyValue = "ConnectionString_UnsupportedPropertyValue";

			// Token: 0x04000C98 RID: 3224
			public const string ConnectionString_OpenedQuoteIsNotClosed = "ConnectionString_OpenedQuoteIsNotClosed";

			// Token: 0x04000C99 RID: 3225
			public const string ConnectionString_ExpectedSemicolonNotFound = "ConnectionString_ExpectedSemicolonNotFound";

			// Token: 0x04000C9A RID: 3226
			public const string ConnectionString_ExpectedEqualSignNotFound = "ConnectionString_ExpectedEqualSignNotFound";

			// Token: 0x04000C9B RID: 3227
			public const string ConnectionString_InvalidCharInPropertyName = "ConnectionString_InvalidCharInPropertyName";

			// Token: 0x04000C9C RID: 3228
			public const string ConnectionString_InvalidCharInUnquotedPropertyValue = "ConnectionString_InvalidCharInUnquotedPropertyValue";

			// Token: 0x04000C9D RID: 3229
			public const string ConnectionString_PropertyNameNotDefined = "ConnectionString_PropertyNameNotDefined";

			// Token: 0x04000C9E RID: 3230
			public const string ConnectionString_InvalidIntegratedSecurityForNative = "ConnectionString_InvalidIntegratedSecurityForNative";

			// Token: 0x04000C9F RID: 3231
			public const string ConnectionString_InvalidProtectionLevelForHttp = "ConnectionString_InvalidProtectionLevelForHttp";

			// Token: 0x04000CA0 RID: 3232
			public const string ConnectionString_InvalidProtectionLevelForHttps = "ConnectionString_InvalidProtectionLevelForHttps";

			// Token: 0x04000CA1 RID: 3233
			public const string ConnectionString_InvalidImpersonationLevelForHttp = "ConnectionString_InvalidImpersonationLevelForHttp";

			// Token: 0x04000CA2 RID: 3234
			public const string ConnectionString_InvalidImpersonationLevelForHttps = "ConnectionString_InvalidImpersonationLevelForHttps";

			// Token: 0x04000CA3 RID: 3235
			public const string ConnectionString_InvalidIntegratedSecurityForHttpOrHttps = "ConnectionString_InvalidIntegratedSecurityForHttpOrHttps";

			// Token: 0x04000CA4 RID: 3236
			public const string ConnectionString_MissingIdentityProviderForIntegratedSecurityFederated = "ConnectionString_MissingIdentityProviderForIntegratedSecurityFederated";

			// Token: 0x04000CA5 RID: 3237
			public const string ConnectionString_InvalidIdentityProviderForIntegratedSecurityFederated = "ConnectionString_InvalidIdentityProviderForIntegratedSecurityFederated";

			// Token: 0x04000CA6 RID: 3238
			public const string ConnectionString_PropertyNotApplicableWithTheDataSourceType = "ConnectionString_PropertyNotApplicableWithTheDataSourceType";

			// Token: 0x04000CA7 RID: 3239
			public const string ConnectionString_DataSourceTypeDoesntSupportQuery = "ConnectionString_DataSourceTypeDoesntSupportQuery";

			// Token: 0x04000CA8 RID: 3240
			public const string ConnectionString_LinkFileInvalidServer = "ConnectionString_LinkFileInvalidServer";

			// Token: 0x04000CA9 RID: 3241
			public const string ConnectionString_LinkFileMissingServer = "ConnectionString_LinkFileMissingServer";

			// Token: 0x04000CAA RID: 3242
			public const string ConnectionString_LinkFileParseError = "ConnectionString_LinkFileParseError";

			// Token: 0x04000CAB RID: 3243
			public const string ConnectionString_LinkFileDupEffectiveUsername = "ConnectionString_LinkFileDupEffectiveUsername";

			// Token: 0x04000CAC RID: 3244
			public const string ConnectionString_LinkFileDownloadError = "ConnectionString_LinkFileDownloadError";

			// Token: 0x04000CAD RID: 3245
			public const string ConnectionString_LinkFileCannotRevert = "ConnectionString_LinkFileCannotRevert";

			// Token: 0x04000CAE RID: 3246
			public const string ConnectionString_LinkFileCannotDelegate = "ConnectionString_LinkFileCannotDelegate";

			// Token: 0x04000CAF RID: 3247
			public const string ConnectionString_MissingPassword = "ConnectionString_MissingPassword";

			// Token: 0x04000CB0 RID: 3248
			public const string ConnectionString_ExternalConnectionIsIncomplete = "ConnectionString_ExternalConnectionIsIncomplete";

			// Token: 0x04000CB1 RID: 3249
			public const string ConnectionString_AsAzure_DataSourcePathMoreThanOneSegment = "ConnectionString_AsAzure_DataSourcePathMoreThanOneSegment";

			// Token: 0x04000CB2 RID: 3250
			public const string ConnectionString_PbiDedicated_MissingInitialCatalog = "ConnectionString_PbiDedicated_MissingInitialCatalog";

			// Token: 0x04000CB3 RID: 3251
			public const string ConnectionString_PbiDedicated_MissingRestrictCatalog = "ConnectionString_PbiDedicated_MissingRestrictCatalog";

			// Token: 0x04000CB4 RID: 3252
			public const string ConnectionString_AsAzure_SspiAndUseAdalCacheTogetherNotSupported = "ConnectionString_AsAzure_SspiAndUseAdalCacheTogetherNotSupported";

			// Token: 0x04000CB5 RID: 3253
			public const string ConnectionString_ASAzure_FetchLinkReferenceFailed = "ConnectionString_ASAzure_FetchLinkReferenceFailed";

			// Token: 0x04000CB6 RID: 3254
			public const string ConnectionString_ASAzure_InvalidLinkReferenceUri = "ConnectionString_ASAzure_InvalidLinkReferenceUri";

			// Token: 0x04000CB7 RID: 3255
			public const string ConnectionString_ASAzure_InvalidLinkReferenceCustomPort = "ConnectionString_ASAzure_InvalidLinkReferenceCustomPort";

			// Token: 0x04000CB8 RID: 3256
			public const string ConnectionString_Untrusted_Endpoint = "ConnectionString_Untrusted_Endpoint";

			// Token: 0x04000CB9 RID: 3257
			public const string ConnectionString_PbiDataset_Missing_Metadata = "ConnectionString_PbiDataset_Missing_Metadata";

			// Token: 0x04000CBA RID: 3258
			public const string ConnectionString_ShilohIsNoLongerSupported = "ConnectionString_ShilohIsNoLongerSupported";

			// Token: 0x04000CBB RID: 3259
			public const string ConnectionString_SPN_Profile_Not_Supported = "ConnectionString_SPN_Profile_Not_Supported";

			// Token: 0x04000CBC RID: 3260
			public const string UnknownServerResponseFormat = "UnknownServerResponseFormat";

			// Token: 0x04000CBD RID: 3261
			public const string AfterExceptionAllTagsShouldCloseUntilMessagesSection = "AfterExceptionAllTagsShouldCloseUntilMessagesSection";

			// Token: 0x04000CBE RID: 3262
			public const string UnrecognizedElementInMessagesSection = "UnrecognizedElementInMessagesSection";

			// Token: 0x04000CBF RID: 3263
			public const string ErrorCodeIsMissingFromRowsetError = "ErrorCodeIsMissingFromRowsetError";

			// Token: 0x04000CC0 RID: 3264
			public const string ErrorCodeIsMissingFromDatasetError = "ErrorCodeIsMissingFromDatasetError";

			// Token: 0x04000CC1 RID: 3265
			public const string ExceptionRequiresXmlaErrorsInMessagesSection = "ExceptionRequiresXmlaErrorsInMessagesSection";

			// Token: 0x04000CC2 RID: 3266
			public const string MessagesSectionIsEmpty = "MessagesSectionIsEmpty";

			// Token: 0x04000CC3 RID: 3267
			public const string EmptyRootIsNotEmpty = "EmptyRootIsNotEmpty";

			// Token: 0x04000CC4 RID: 3268
			public const string UnexpectedElement = "UnexpectedElement";

			// Token: 0x04000CC5 RID: 3269
			public const string MissingElement = "MissingElement";

			// Token: 0x04000CC6 RID: 3270
			public const string Resultset_IsNotRowset = "Resultset_IsNotRowset";

			// Token: 0x04000CC7 RID: 3271
			public const string DataReaderClosedError = "DataReaderClosedError";

			// Token: 0x04000CC8 RID: 3272
			public const string DataReaderInvalidRowError = "DataReaderInvalidRowError";

			// Token: 0x04000CC9 RID: 3273
			public const string NonSequentialColumnAccessError = "NonSequentialColumnAccessError";

			// Token: 0x04000CCA RID: 3274
			public const string DataReader_IndexOutOfRange = "DataReader_IndexOutOfRange";

			// Token: 0x04000CCB RID: 3275
			public const string Authentication_Failed = "Authentication_Failed";

			// Token: 0x04000CCC RID: 3276
			public const string Authentication_Sspi_PackageNotFound = "Authentication_Sspi_PackageNotFound";

			// Token: 0x04000CCD RID: 3277
			public const string Authentication_Sspi_PackageDoesntSupportCapability = "Authentication_Sspi_PackageDoesntSupportCapability";

			// Token: 0x04000CCE RID: 3278
			public const string Authentication_Sspi_FlagNotEstablished = "Authentication_Sspi_FlagNotEstablished";

			// Token: 0x04000CCF RID: 3279
			public const string Authentication_Sspi_SchannelCantDelegate = "Authentication_Sspi_SchannelCantDelegate";

			// Token: 0x04000CD0 RID: 3280
			public const string Authentication_Sspi_SchannelSupportsOnlyPrivacyLevel = "Authentication_Sspi_SchannelSupportsOnlyPrivacyLevel";

			// Token: 0x04000CD1 RID: 3281
			public const string Authentication_Sspi_SchannelUnsupportedImpersonationLevel = "Authentication_Sspi_SchannelUnsupportedImpersonationLevel";

			// Token: 0x04000CD2 RID: 3282
			public const string Authentication_Sspi_SchannelUnsupportedProtectionLevel = "Authentication_Sspi_SchannelUnsupportedProtectionLevel";

			// Token: 0x04000CD3 RID: 3283
			public const string Authentication_Sspi_SchannelAnonymousAmbiguity = "Authentication_Sspi_SchannelAnonymousAmbiguity";

			// Token: 0x04000CD4 RID: 3284
			public const string Authentication_MsoID_MissingSignInAssistant = "Authentication_MsoID_MissingSignInAssistant";

			// Token: 0x04000CD5 RID: 3285
			public const string Authentication_MsoID_InternalError = "Authentication_MsoID_InternalError";

			// Token: 0x04000CD6 RID: 3286
			public const string Authentication_MsoID_InvalidCredentials = "Authentication_MsoID_InvalidCredentials";

			// Token: 0x04000CD7 RID: 3287
			public const string Authentication_MsoID_SsoFailed = "Authentication_MsoID_SsoFailed";

			// Token: 0x04000CD8 RID: 3288
			public const string Authentication_MsoID_SsoFailedNonDomainUser = "Authentication_MsoID_SsoFailedNonDomainUser";

			// Token: 0x04000CD9 RID: 3289
			public const string Authentication_ClaimsToken_AuthorityNotFound = "Authentication_ClaimsToken_AuthorityNotFound";

			// Token: 0x04000CDA RID: 3290
			public const string Authentication_ClaimsToken_UserIdAndPasswordRequired = "Authentication_ClaimsToken_UserIdAndPasswordRequired";

			// Token: 0x04000CDB RID: 3291
			public const string Authentication_ClaimsToken_IdentityProviderFormatInvalid = "Authentication_ClaimsToken_IdentityProviderFormatInvalid";

			// Token: 0x04000CDC RID: 3292
			public const string Authentication_ClaimsToken_AdalLoadingError = "Authentication_ClaimsToken_AdalLoadingError";

			// Token: 0x04000CDD RID: 3293
			public const string Authentication_ClaimsToken_AdalError = "Authentication_ClaimsToken_AdalError";

			// Token: 0x04000CDE RID: 3294
			public const string Authentication_AsAzure_OnlySspiOrClaimsTokenSupported = "Authentication_AsAzure_OnlySspiOrClaimsTokenSupported";

			// Token: 0x04000CDF RID: 3295
			public const string Authentication_PbiDedicated_OnlyClaimsTokenSupported = "Authentication_PbiDedicated_OnlyClaimsTokenSupported";

			// Token: 0x04000CE0 RID: 3296
			public const string DimeReader_CannotReadFromStream = "DimeReader_CannotReadFromStream";

			// Token: 0x04000CE1 RID: 3297
			public const string DimeReader_IsClosed = "DimeReader_IsClosed";

			// Token: 0x04000CE2 RID: 3298
			public const string DimeReader_PreviousRecordStreamStillOpened = "DimeReader_PreviousRecordStreamStillOpened";

			// Token: 0x04000CE3 RID: 3299
			public const string DimeRecord_StreamShouldBeReadable = "DimeRecord_StreamShouldBeReadable";

			// Token: 0x04000CE4 RID: 3300
			public const string DimeRecord_StreamShouldBeWriteable = "DimeRecord_StreamShouldBeWriteable";

			// Token: 0x04000CE5 RID: 3301
			public const string DimeRecord_InvalidContentLength = "DimeRecord_InvalidContentLength";

			// Token: 0x04000CE6 RID: 3302
			public const string DimeRecord_PropertyOnlyAvailableForReadRecords = "DimeRecord_PropertyOnlyAvailableForReadRecords";

			// Token: 0x04000CE7 RID: 3303
			public const string DimeRecord_InvalidChunkSize = "DimeRecord_InvalidChunkSize";

			// Token: 0x04000CE8 RID: 3304
			public const string DimeRecord_UnableToReadFromStream = "DimeRecord_UnableToReadFromStream";

			// Token: 0x04000CE9 RID: 3305
			public const string DimeRecord_StreamIsClosed = "DimeRecord_StreamIsClosed";

			// Token: 0x04000CEA RID: 3306
			public const string DimeRecord_ReadNotAllowed = "DimeRecord_ReadNotAllowed";

			// Token: 0x04000CEB RID: 3307
			public const string DimeRecord_WriteNotAllowed = "DimeRecord_WriteNotAllowed";

			// Token: 0x04000CEC RID: 3308
			public const string DimeRecord_TypeFormatEnumUnchangedNotAllowed = "DimeRecord_TypeFormatEnumUnchangedNotAllowed";

			// Token: 0x04000CED RID: 3309
			public const string DimeRecord_MediaTypeNotDefined = "DimeRecord_MediaTypeNotDefined";

			// Token: 0x04000CEE RID: 3310
			public const string DimeRecord_InvalidUriFormat = "DimeRecord_InvalidUriFormat";

			// Token: 0x04000CEF RID: 3311
			public const string DimeRecord_NameMustNotBeDefinedForFormatNone = "DimeRecord_NameMustNotBeDefinedForFormatNone";

			// Token: 0x04000CF0 RID: 3312
			public const string DimeRecord_EncodedTypeLengthExceeds8191 = "DimeRecord_EncodedTypeLengthExceeds8191";

			// Token: 0x04000CF1 RID: 3313
			public const string DimeRecord_OffsetAndCountShouldBePositive = "DimeRecord_OffsetAndCountShouldBePositive";

			// Token: 0x04000CF2 RID: 3314
			public const string DimeRecord_ContentLengthExceeded = "DimeRecord_ContentLengthExceeded";

			// Token: 0x04000CF3 RID: 3315
			public const string DimeRecord_VersionNotSupported = "DimeRecord_VersionNotSupported";

			// Token: 0x04000CF4 RID: 3316
			public const string DimeRecord_OnlySingleRecordMessagesAreSupported = "DimeRecord_OnlySingleRecordMessagesAreSupported";

			// Token: 0x04000CF5 RID: 3317
			public const string DimeRecord_TypeFormatShouldBeMedia = "DimeRecord_TypeFormatShouldBeMedia";

			// Token: 0x04000CF6 RID: 3318
			public const string DimeRecord_TypeFormatShouldBeUnchanged = "DimeRecord_TypeFormatShouldBeUnchanged";

			// Token: 0x04000CF7 RID: 3319
			public const string DimeRecord_ReservedFlagShouldBeZero = "DimeRecord_ReservedFlagShouldBeZero";

			// Token: 0x04000CF8 RID: 3320
			public const string DimeRecord_DataTypeShouldBeSpecifiedOnTheFirstChunk = "DimeRecord_DataTypeShouldBeSpecifiedOnTheFirstChunk";

			// Token: 0x04000CF9 RID: 3321
			public const string DimeRecord_DataTypeIsOnlyForTheFirstChunk = "DimeRecord_DataTypeIsOnlyForTheFirstChunk";

			// Token: 0x04000CFA RID: 3322
			public const string DimeRecord_DataTypeNotSupported = "DimeRecord_DataTypeNotSupported";

			// Token: 0x04000CFB RID: 3323
			public const string DimeRecord_InvalidHeaderFlags = "DimeRecord_InvalidHeaderFlags";

			// Token: 0x04000CFC RID: 3324
			public const string DimeRecord_IDIsOnlyForFirstChunk = "DimeRecord_IDIsOnlyForFirstChunk";

			// Token: 0x04000CFD RID: 3325
			public const string DimeWriter_CannotWriteToStream = "DimeWriter_CannotWriteToStream";

			// Token: 0x04000CFE RID: 3326
			public const string DimeWriter_WriterIsClosed = "DimeWriter_WriterIsClosed";

			// Token: 0x04000CFF RID: 3327
			public const string DimeWriter_InvalidDefaultChunkSize = "DimeWriter_InvalidDefaultChunkSize";

			// Token: 0x04000D00 RID: 3328
			public const string Dime_DataTypeNotSupported = "Dime_DataTypeNotSupported";

			// Token: 0x04000D01 RID: 3329
			public const string TcpStream_MaxSignatureExceedsProtocolLimit = "TcpStream_MaxSignatureExceedsProtocolLimit";

			// Token: 0x04000D02 RID: 3330
			public const string HttpStream_InvalidReadRequest = "HttpStream_InvalidReadRequest";

			// Token: 0x04000D03 RID: 3331
			public const string HttpStream_ResponseWithFailedStatus = "HttpStream_ResponseWithFailedStatus";

			// Token: 0x04000D04 RID: 3332
			public const string HttpStream_RequestPayloadStream_InvalidStreamOperation = "HttpStream_RequestPayloadStream_InvalidStreamOperation";

			// Token: 0x04000D05 RID: 3333
			public const string HttpStream_RequestPayloadStream_WriteAfterComplete = "HttpStream_RequestPayloadStream_WriteAfterComplete";

			// Token: 0x04000D06 RID: 3334
			public const string HttpStream_RequestPayloadStream_InvalidAsyncResultType = "HttpStream_RequestPayloadStream_InvalidAsyncResultType";

			// Token: 0x04000D07 RID: 3335
			public const string HttpStream_RequestPayloadStream_EndReadAlreadyCalled = "HttpStream_RequestPayloadStream_EndReadAlreadyCalled";

			// Token: 0x04000D08 RID: 3336
			public const string HttpStream_RequestPayloadStream_ErrorInCallback = "HttpStream_RequestPayloadStream_ErrorInCallback";

			// Token: 0x04000D09 RID: 3337
			public const string HttpStream_RequestPayloadStream_ErrorInWrite = "HttpStream_RequestPayloadStream_ErrorInWrite";

			// Token: 0x04000D0A RID: 3338
			public const string IXMLAInterop_OnlyZeroOffsetIsSupported = "IXMLAInterop_OnlyZeroOffsetIsSupported";

			// Token: 0x04000D0B RID: 3339
			public const string IXMLAInterop_StreamDoesNotSupportReverting = "IXMLAInterop_StreamDoesNotSupportReverting";

			// Token: 0x04000D0C RID: 3340
			public const string IXMLAInterop_StreamDoesNotSupportLocking = "IXMLAInterop_StreamDoesNotSupportLocking";

			// Token: 0x04000D0D RID: 3341
			public const string IXMLAInterop_StreamDoesNotSupportUnlocking = "IXMLAInterop_StreamDoesNotSupportUnlocking";

			// Token: 0x04000D0E RID: 3342
			public const string IXMLAInterop_StreamCannotBeCloned = "IXMLAInterop_StreamCannotBeCloned";

			// Token: 0x04000D0F RID: 3343
			public const string XmlaClient_StartRequest_ThereIsAnotherPendingRequest = "XmlaClient_StartRequest_ThereIsAnotherPendingRequest";

			// Token: 0x04000D10 RID: 3344
			public const string XmlaClient_StartRequest_ThereIsAnotherPendingResponse = "XmlaClient_StartRequest_ThereIsAnotherPendingResponse";

			// Token: 0x04000D11 RID: 3345
			public const string XmlaClient_SendRequest_RequestStreamCannotBeRead = "XmlaClient_SendRequest_RequestStreamCannotBeRead";

			// Token: 0x04000D12 RID: 3346
			public const string XmlaClient_SendRequest_NoRequestWasCreated = "XmlaClient_SendRequest_NoRequestWasCreated";

			// Token: 0x04000D13 RID: 3347
			public const string XmlaClient_ConnectTimedOut = "XmlaClient_ConnectTimedOut";

			// Token: 0x04000D14 RID: 3348
			public const string XmlaClient_SendRequest_ThereIsAnotherPendingResponse = "XmlaClient_SendRequest_ThereIsAnotherPendingResponse";

			// Token: 0x04000D15 RID: 3349
			public const string XmlaClient_CannotConnectToLocalCubeWithRestictedClient = "XmlaClient_CannotConnectToLocalCubeWithRestictedClient";

			// Token: 0x04000D16 RID: 3350
			public const string XmlaClient_PbiPublicXmla_InvalidDataSourceUriFormat = "XmlaClient_PbiPublicXmla_InvalidDataSourceUriFormat";

			// Token: 0x04000D17 RID: 3351
			public const string XmlaClient_PbiPremium_WorkspaceNotFound = "XmlaClient_PbiPremium_WorkspaceNotFound";

			// Token: 0x04000D18 RID: 3352
			public const string XmlaClient_PbiPremium_WorkspaceNotOnPremium = "XmlaClient_PbiPremium_WorkspaceNotOnPremium";

			// Token: 0x04000D19 RID: 3353
			public const string XmlaClient_PbiPremium_WorkspaceNameDuplicated = "XmlaClient_PbiPremium_WorkspaceNameDuplicated";

			// Token: 0x04000D1A RID: 3354
			public const string XmlaClient_PbiPublicXmla_DatasetNotFound = "XmlaClient_PbiPublicXmla_DatasetNotFound";

			// Token: 0x04000D1B RID: 3355
			public const string XmlaClient_PbiPublicXmla_DatasetNameDuplicated = "XmlaClient_PbiPublicXmla_DatasetNameDuplicated";

			// Token: 0x04000D1C RID: 3356
			public const string XmlaClient_PbiPublicXmla_DatasetNotSpecified = "XmlaClient_PbiPublicXmla_DatasetNotSpecified";

			// Token: 0x04000D1D RID: 3357
			public const string XmlaClient_PbiPublicXmlaWithEmbedToken_ToPBIShared_NotSupported = "XmlaClient_PbiPublicXmlaWithEmbedToken_ToPBIShared_NotSupported";

			// Token: 0x04000D1E RID: 3358
			public const string XmlaClient_PbiPublicXmlaWithEmbedToken_UserId_Prop_NotSupported = "XmlaClient_PbiPublicXmlaWithEmbedToken_UserId_Prop_NotSupported";

			// Token: 0x04000D1F RID: 3359
			public const string XmlaClient_PbiPublicXmlaWithEmbedToken_Missing_InitialCatalog = "XmlaClient_PbiPublicXmlaWithEmbedToken_Missing_InitialCatalog";

			// Token: 0x04000D20 RID: 3360
			public const string XmlaClient_PbiPublicXmla_UnsupportedIdentityProvider = "XmlaClient_PbiPublicXmla_UnsupportedIdentityProvider";

			// Token: 0x04000D21 RID: 3361
			public const string XmlaClient_ASAzureRedirectionResolutionFailedWithError = "XmlaClient_ASAzureRedirectionResolutionFailedWithError";

			// Token: 0x04000D22 RID: 3362
			public const string XmlaClient_ASAzureRedirectionResolutionMissingTenantId = "XmlaClient_ASAzureRedirectionResolutionMissingTenantId";

			// Token: 0x04000D23 RID: 3363
			public const string XmlaClient_ASAzureRedirectionResolutionFailedWithThrottling = "XmlaClient_ASAzureRedirectionResolutionFailedWithThrottling";

			// Token: 0x04000D24 RID: 3364
			public const string Decompression_InitializationFailed = "Decompression_InitializationFailed";

			// Token: 0x04000D25 RID: 3365
			public const string Decompression_Failed = "Decompression_Failed";

			// Token: 0x04000D26 RID: 3366
			public const string Compression_InitializationFailed = "Compression_InitializationFailed";

			// Token: 0x04000D27 RID: 3367
			public const string InvalidArgument = "InvalidArgument";

			// Token: 0x04000D28 RID: 3368
			public const string UnsupportedDataFormat = "UnsupportedDataFormat";

			// Token: 0x04000D29 RID: 3369
			public const string UnsupportedMethod = "UnsupportedMethod";

			// Token: 0x04000D2A RID: 3370
			public const string ProvidePath = "ProvidePath";

			// Token: 0x04000D2B RID: 3371
			public const string DirectoryNotFound = "DirectoryNotFound";

			// Token: 0x04000D2C RID: 3372
			public const string InternalError = "InternalError";

			// Token: 0x04000D2D RID: 3373
			public const string InternalErrorAndInvalidBufferType = "InternalErrorAndInvalidBufferType";
		}
	}
}
