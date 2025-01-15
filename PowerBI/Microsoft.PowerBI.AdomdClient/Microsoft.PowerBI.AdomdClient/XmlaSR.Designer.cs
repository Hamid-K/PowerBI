using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000F7 RID: 247
	[CompilerGenerated]
	internal class XmlaSR
	{
		// Token: 0x06000D9E RID: 3486 RVA: 0x00030A8E File Offset: 0x0002EC8E
		protected XmlaSR()
		{
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00030A96 File Offset: 0x0002EC96
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x00030A9D File Offset: 0x0002EC9D
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

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00030AA5 File Offset: 0x0002ECA5
		public static string AlreadyConnected
		{
			get
			{
				return XmlaSR.Keys.GetString("AlreadyConnected");
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00030AB1 File Offset: 0x0002ECB1
		public static string NotConnected
		{
			get
			{
				return XmlaSR.Keys.GetString("NotConnected");
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00030ABD File Offset: 0x0002ECBD
		public static string CannotConnect
		{
			get
			{
				return XmlaSR.Keys.GetString("CannotConnect");
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00030AC9 File Offset: 0x0002ECC9
		public static string CannotConnectToRedirector
		{
			get
			{
				return XmlaSR.Keys.GetString("CannotConnectToRedirector");
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00030AD5 File Offset: 0x0002ECD5
		public static string ConnectionBroken
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionBroken");
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00030AE1 File Offset: 0x0002ECE1
		public static string Reconnect_ConnectionInfoIsMissing
		{
			get
			{
				return XmlaSR.Keys.GetString("Reconnect_ConnectionInfoIsMissing");
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00030AED File Offset: 0x0002ECED
		public static string Reconnect_SessionIDIsMissing
		{
			get
			{
				return XmlaSR.Keys.GetString("Reconnect_SessionIDIsMissing");
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x00030AF9 File Offset: 0x0002ECF9
		public static string ServerDidNotProvideErrorInfo
		{
			get
			{
				return XmlaSR.Keys.GetString("ServerDidNotProvideErrorInfo");
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00030B05 File Offset: 0x0002ED05
		public static string ConnectionCannotBeUsedWhileXmlReaderOpened
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionCannotBeUsedWhileXmlReaderOpened");
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00030B11 File Offset: 0x0002ED11
		public static string Connect_RedirectorDidntReturnDatabaseInfo
		{
			get
			{
				return XmlaSR.Keys.GetString("Connect_RedirectorDidntReturnDatabaseInfo");
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00030B1D File Offset: 0x0002ED1D
		public static string Connection_WorkbookIsOutdated
		{
			get
			{
				return XmlaSR.Keys.GetString("Connection_WorkbookIsOutdated");
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x00030B29 File Offset: 0x0002ED29
		public static string Connection_AnalysisServicesInstanceWasMoved
		{
			get
			{
				return XmlaSR.Keys.GetString("Connection_AnalysisServicesInstanceWasMoved");
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00030B35 File Offset: 0x0002ED35
		public static string NetCore_NotSupportedFeature
		{
			get
			{
				return XmlaSR.Keys.GetString("NetCore_NotSupportedFeature");
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00030B41 File Offset: 0x0002ED41
		public static string NetCore_WindowsOnlySupportedFeature
		{
			get
			{
				return XmlaSR.Keys.GetString("NetCore_WindowsOnlySupportedFeature");
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00030B4D File Offset: 0x0002ED4D
		public static string NetCore_WindowsDesktopOnlySupportedFeature
		{
			get
			{
				return XmlaSR.Keys.GetString("NetCore_WindowsDesktopOnlySupportedFeature");
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00030B59 File Offset: 0x0002ED59
		public static string FailedToResolveCluster
		{
			get
			{
				return XmlaSR.Keys.GetString("FailedToResolveCluster");
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00030B65 File Offset: 0x0002ED65
		public static string SoapFormatter_ResponseIsNotRowset
		{
			get
			{
				return XmlaSR.Keys.GetString("SoapFormatter_ResponseIsNotRowset");
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x00030B71 File Offset: 0x0002ED71
		public static string SoapFormatter_ResponseIsNotDataset
		{
			get
			{
				return XmlaSR.Keys.GetString("SoapFormatter_ResponseIsNotDataset");
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x00030B7D File Offset: 0x0002ED7D
		public static string Cancel_SessionIDNotSpecified
		{
			get
			{
				return XmlaSR.Keys.GetString("Cancel_SessionIDNotSpecified");
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00030B89 File Offset: 0x0002ED89
		public static string ConnectionString_Invalid
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_Invalid");
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00030B95 File Offset: 0x0002ED95
		public static string ConnectionString_DataSourceNotSpecified
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_DataSourceNotSpecified");
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00030BA1 File Offset: 0x0002EDA1
		public static string ConnectionString_MissingIdentityProviderForIntegratedSecurityFederated
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_MissingIdentityProviderForIntegratedSecurityFederated");
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00030BAD File Offset: 0x0002EDAD
		public static string ConnectionString_InvalidIdentityProviderForIntegratedSecurityFederated
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_InvalidIdentityProviderForIntegratedSecurityFederated");
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00030BB9 File Offset: 0x0002EDB9
		public static string ConnectionString_DataSourceTypeDoesntSupportQuery
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_DataSourceTypeDoesntSupportQuery");
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x00030BC5 File Offset: 0x0002EDC5
		public static string ConnectionString_LinkFileInvalidServer
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_LinkFileInvalidServer");
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00030BD1 File Offset: 0x0002EDD1
		public static string ConnectionString_LinkFileMissingServer
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_LinkFileMissingServer");
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00030BDD File Offset: 0x0002EDDD
		public static string ConnectionString_LinkFileDupEffectiveUsername
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_LinkFileDupEffectiveUsername");
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00030BE9 File Offset: 0x0002EDE9
		public static string ConnectionString_LinkFileCannotRevert
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_LinkFileCannotRevert");
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x00030BF5 File Offset: 0x0002EDF5
		public static string ConnectionString_LinkFileCannotDelegate
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_LinkFileCannotDelegate");
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x00030C01 File Offset: 0x0002EE01
		public static string ConnectionString_MissingPassword
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_MissingPassword");
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x00030C0D File Offset: 0x0002EE0D
		public static string ConnectionString_AsAzure_DataSourcePathMoreThanOneSegment
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_AsAzure_DataSourcePathMoreThanOneSegment");
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x00030C19 File Offset: 0x0002EE19
		public static string ConnectionString_PbiDedicated_MissingInitialCatalog
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_PbiDedicated_MissingInitialCatalog");
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00030C25 File Offset: 0x0002EE25
		public static string ConnectionString_PbiDedicated_MissingRestrictCatalog
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_PbiDedicated_MissingRestrictCatalog");
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x00030C31 File Offset: 0x0002EE31
		public static string ConnectionString_AsAzure_SspiAndUseAdalCacheTogetherNotSupported
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_AsAzure_SspiAndUseAdalCacheTogetherNotSupported");
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00030C3D File Offset: 0x0002EE3D
		public static string ConnectionString_Untrusted_Endpoint
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_Untrusted_Endpoint");
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00030C49 File Offset: 0x0002EE49
		public static string ConnectionString_SPN_Profile_Not_Supported
		{
			get
			{
				return XmlaSR.Keys.GetString("ConnectionString_SPN_Profile_Not_Supported");
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00030C55 File Offset: 0x0002EE55
		public static string UnknownServerResponseFormat
		{
			get
			{
				return XmlaSR.Keys.GetString("UnknownServerResponseFormat");
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00030C61 File Offset: 0x0002EE61
		public static string AfterExceptionAllTagsShouldCloseUntilMessagesSection
		{
			get
			{
				return XmlaSR.Keys.GetString("AfterExceptionAllTagsShouldCloseUntilMessagesSection");
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00030C6D File Offset: 0x0002EE6D
		public static string ErrorCodeIsMissingFromRowsetError
		{
			get
			{
				return XmlaSR.Keys.GetString("ErrorCodeIsMissingFromRowsetError");
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00030C79 File Offset: 0x0002EE79
		public static string ErrorCodeIsMissingFromDatasetError
		{
			get
			{
				return XmlaSR.Keys.GetString("ErrorCodeIsMissingFromDatasetError");
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00030C85 File Offset: 0x0002EE85
		public static string ExceptionRequiresXmlaErrorsInMessagesSection
		{
			get
			{
				return XmlaSR.Keys.GetString("ExceptionRequiresXmlaErrorsInMessagesSection");
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00030C91 File Offset: 0x0002EE91
		public static string MessagesSectionIsEmpty
		{
			get
			{
				return XmlaSR.Keys.GetString("MessagesSectionIsEmpty");
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00030C9D File Offset: 0x0002EE9D
		public static string EmptyRootIsNotEmpty
		{
			get
			{
				return XmlaSR.Keys.GetString("EmptyRootIsNotEmpty");
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00030CA9 File Offset: 0x0002EEA9
		public static string Resultset_IsNotRowset
		{
			get
			{
				return XmlaSR.Keys.GetString("Resultset_IsNotRowset");
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00030CB5 File Offset: 0x0002EEB5
		public static string DataReaderClosedError
		{
			get
			{
				return XmlaSR.Keys.GetString("DataReaderClosedError");
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00030CC1 File Offset: 0x0002EEC1
		public static string DataReaderInvalidRowError
		{
			get
			{
				return XmlaSR.Keys.GetString("DataReaderInvalidRowError");
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00030CCD File Offset: 0x0002EECD
		public static string NonSequentialColumnAccessError
		{
			get
			{
				return XmlaSR.Keys.GetString("NonSequentialColumnAccessError");
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00030CD9 File Offset: 0x0002EED9
		public static string DataReader_IndexOutOfRange
		{
			get
			{
				return XmlaSR.Keys.GetString("DataReader_IndexOutOfRange");
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00030CE5 File Offset: 0x0002EEE5
		public static string Authentication_Failed
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_Failed");
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00030CF1 File Offset: 0x0002EEF1
		public static string Authentication_Sspi_SchannelCantDelegate
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_Sspi_SchannelCantDelegate");
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x00030CFD File Offset: 0x0002EEFD
		public static string Authentication_Sspi_SchannelSupportsOnlyPrivacyLevel
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_Sspi_SchannelSupportsOnlyPrivacyLevel");
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00030D09 File Offset: 0x0002EF09
		public static string Authentication_Sspi_SchannelUnsupportedImpersonationLevel
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_Sspi_SchannelUnsupportedImpersonationLevel");
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00030D15 File Offset: 0x0002EF15
		public static string Authentication_Sspi_SchannelUnsupportedProtectionLevel
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_Sspi_SchannelUnsupportedProtectionLevel");
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00030D21 File Offset: 0x0002EF21
		public static string Authentication_Sspi_SchannelAnonymousAmbiguity
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_Sspi_SchannelAnonymousAmbiguity");
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00030D2D File Offset: 0x0002EF2D
		public static string Authentication_MsoID_MissingSignInAssistant
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_MsoID_MissingSignInAssistant");
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00030D39 File Offset: 0x0002EF39
		public static string Authentication_MsoID_InternalError
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_MsoID_InternalError");
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00030D45 File Offset: 0x0002EF45
		public static string Authentication_MsoID_InvalidCredentials
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_MsoID_InvalidCredentials");
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00030D51 File Offset: 0x0002EF51
		public static string Authentication_MsoID_SsoFailed
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_MsoID_SsoFailed");
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00030D5D File Offset: 0x0002EF5D
		public static string Authentication_MsoID_SsoFailedNonDomainUser
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_MsoID_SsoFailedNonDomainUser");
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00030D69 File Offset: 0x0002EF69
		public static string Authentication_ClaimsToken_AuthorityNotFound
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_ClaimsToken_AuthorityNotFound");
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00030D75 File Offset: 0x0002EF75
		public static string Authentication_ClaimsToken_UserIdAndPasswordRequired
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_ClaimsToken_UserIdAndPasswordRequired");
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00030D81 File Offset: 0x0002EF81
		public static string Authentication_ClaimsToken_IdentityProviderFormatInvalid
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_ClaimsToken_IdentityProviderFormatInvalid");
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00030D8D File Offset: 0x0002EF8D
		public static string Authentication_AsAzure_OnlySspiOrClaimsTokenSupported
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_AsAzure_OnlySspiOrClaimsTokenSupported");
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00030D99 File Offset: 0x0002EF99
		public static string Authentication_PbiDedicated_OnlyClaimsTokenSupported
		{
			get
			{
				return XmlaSR.Keys.GetString("Authentication_PbiDedicated_OnlyClaimsTokenSupported");
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x00030DA5 File Offset: 0x0002EFA5
		public static string DimeReader_CannotReadFromStream
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeReader_CannotReadFromStream");
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00030DB1 File Offset: 0x0002EFB1
		public static string DimeReader_IsClosed
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeReader_IsClosed");
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00030DBD File Offset: 0x0002EFBD
		public static string DimeReader_PreviousRecordStreamStillOpened
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeReader_PreviousRecordStreamStillOpened");
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x00030DC9 File Offset: 0x0002EFC9
		public static string DimeRecord_StreamShouldBeReadable
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_StreamShouldBeReadable");
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x00030DD5 File Offset: 0x0002EFD5
		public static string DimeRecord_StreamShouldBeWriteable
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_StreamShouldBeWriteable");
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x00030DE1 File Offset: 0x0002EFE1
		public static string DimeRecord_InvalidContentLength
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_InvalidContentLength");
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00030DED File Offset: 0x0002EFED
		public static string DimeRecord_PropertyOnlyAvailableForReadRecords
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_PropertyOnlyAvailableForReadRecords");
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00030DF9 File Offset: 0x0002EFF9
		public static string DimeRecord_InvalidChunkSize
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_InvalidChunkSize");
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00030E05 File Offset: 0x0002F005
		public static string DimeRecord_UnableToReadFromStream
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_UnableToReadFromStream");
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00030E11 File Offset: 0x0002F011
		public static string DimeRecord_StreamIsClosed
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_StreamIsClosed");
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00030E1D File Offset: 0x0002F01D
		public static string DimeRecord_ReadNotAllowed
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_ReadNotAllowed");
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00030E29 File Offset: 0x0002F029
		public static string DimeRecord_WriteNotAllowed
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_WriteNotAllowed");
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00030E35 File Offset: 0x0002F035
		public static string DimeRecord_TypeFormatEnumUnchangedNotAllowed
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_TypeFormatEnumUnchangedNotAllowed");
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00030E41 File Offset: 0x0002F041
		public static string DimeRecord_MediaTypeNotDefined
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_MediaTypeNotDefined");
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00030E4D File Offset: 0x0002F04D
		public static string DimeRecord_NameMustNotBeDefinedForFormatNone
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_NameMustNotBeDefinedForFormatNone");
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00030E59 File Offset: 0x0002F059
		public static string DimeRecord_EncodedTypeLengthExceeds8191
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_EncodedTypeLengthExceeds8191");
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00030E65 File Offset: 0x0002F065
		public static string DimeRecord_OffsetAndCountShouldBePositive
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_OffsetAndCountShouldBePositive");
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00030E71 File Offset: 0x0002F071
		public static string DimeRecord_ContentLengthExceeded
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_ContentLengthExceeded");
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00030E7D File Offset: 0x0002F07D
		public static string DimeRecord_OnlySingleRecordMessagesAreSupported
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_OnlySingleRecordMessagesAreSupported");
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00030E89 File Offset: 0x0002F089
		public static string DimeRecord_DataTypeShouldBeSpecifiedOnTheFirstChunk
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_DataTypeShouldBeSpecifiedOnTheFirstChunk");
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x00030E95 File Offset: 0x0002F095
		public static string DimeRecord_DataTypeIsOnlyForTheFirstChunk
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_DataTypeIsOnlyForTheFirstChunk");
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00030EA1 File Offset: 0x0002F0A1
		public static string DimeRecord_IDIsOnlyForFirstChunk
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeRecord_IDIsOnlyForFirstChunk");
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00030EAD File Offset: 0x0002F0AD
		public static string DimeWriter_CannotWriteToStream
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeWriter_CannotWriteToStream");
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00030EB9 File Offset: 0x0002F0B9
		public static string DimeWriter_WriterIsClosed
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeWriter_WriterIsClosed");
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x00030EC5 File Offset: 0x0002F0C5
		public static string DimeWriter_InvalidDefaultChunkSize
		{
			get
			{
				return XmlaSR.Keys.GetString("DimeWriter_InvalidDefaultChunkSize");
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00030ED1 File Offset: 0x0002F0D1
		public static string TcpStream_MaxSignatureExceedsProtocolLimit
		{
			get
			{
				return XmlaSR.Keys.GetString("TcpStream_MaxSignatureExceedsProtocolLimit");
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x00030EDD File Offset: 0x0002F0DD
		public static string HttpStream_RequestPayloadStream_InvalidStreamOperation
		{
			get
			{
				return XmlaSR.Keys.GetString("HttpStream_RequestPayloadStream_InvalidStreamOperation");
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x00030EE9 File Offset: 0x0002F0E9
		public static string HttpStream_RequestPayloadStream_WriteAfterComplete
		{
			get
			{
				return XmlaSR.Keys.GetString("HttpStream_RequestPayloadStream_WriteAfterComplete");
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00030EF5 File Offset: 0x0002F0F5
		public static string HttpStream_RequestPayloadStream_InvalidAsyncResultType
		{
			get
			{
				return XmlaSR.Keys.GetString("HttpStream_RequestPayloadStream_InvalidAsyncResultType");
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x00030F01 File Offset: 0x0002F101
		public static string HttpStream_RequestPayloadStream_EndReadAlreadyCalled
		{
			get
			{
				return XmlaSR.Keys.GetString("HttpStream_RequestPayloadStream_EndReadAlreadyCalled");
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x00030F0D File Offset: 0x0002F10D
		public static string HttpStream_RequestPayloadStream_ErrorInCallback
		{
			get
			{
				return XmlaSR.Keys.GetString("HttpStream_RequestPayloadStream_ErrorInCallback");
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x00030F19 File Offset: 0x0002F119
		public static string IXMLAInterop_OnlyZeroOffsetIsSupported
		{
			get
			{
				return XmlaSR.Keys.GetString("IXMLAInterop_OnlyZeroOffsetIsSupported");
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00030F25 File Offset: 0x0002F125
		public static string IXMLAInterop_StreamDoesNotSupportReverting
		{
			get
			{
				return XmlaSR.Keys.GetString("IXMLAInterop_StreamDoesNotSupportReverting");
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00030F31 File Offset: 0x0002F131
		public static string IXMLAInterop_StreamDoesNotSupportLocking
		{
			get
			{
				return XmlaSR.Keys.GetString("IXMLAInterop_StreamDoesNotSupportLocking");
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00030F3D File Offset: 0x0002F13D
		public static string IXMLAInterop_StreamDoesNotSupportUnlocking
		{
			get
			{
				return XmlaSR.Keys.GetString("IXMLAInterop_StreamDoesNotSupportUnlocking");
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x00030F49 File Offset: 0x0002F149
		public static string IXMLAInterop_StreamCannotBeCloned
		{
			get
			{
				return XmlaSR.Keys.GetString("IXMLAInterop_StreamCannotBeCloned");
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00030F55 File Offset: 0x0002F155
		public static string XmlaClient_StartRequest_ThereIsAnotherPendingRequest
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_StartRequest_ThereIsAnotherPendingRequest");
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x00030F61 File Offset: 0x0002F161
		public static string XmlaClient_StartRequest_ThereIsAnotherPendingResponse
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_StartRequest_ThereIsAnotherPendingResponse");
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00030F6D File Offset: 0x0002F16D
		public static string XmlaClient_SendRequest_RequestStreamCannotBeRead
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_SendRequest_RequestStreamCannotBeRead");
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00030F79 File Offset: 0x0002F179
		public static string XmlaClient_SendRequest_NoRequestWasCreated
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_SendRequest_NoRequestWasCreated");
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00030F85 File Offset: 0x0002F185
		public static string XmlaClient_ConnectTimedOut
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_ConnectTimedOut");
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00030F91 File Offset: 0x0002F191
		public static string XmlaClient_SendRequest_ThereIsAnotherPendingResponse
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_SendRequest_ThereIsAnotherPendingResponse");
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00030F9D File Offset: 0x0002F19D
		public static string XmlaClient_CannotConnectToLocalCubeWithRestictedClient
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_CannotConnectToLocalCubeWithRestictedClient");
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00030FA9 File Offset: 0x0002F1A9
		public static string XmlaClient_PbiPublicXmla_DatasetNotSpecified
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmla_DatasetNotSpecified");
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x00030FB5 File Offset: 0x0002F1B5
		public static string XmlaClient_PbiPublicXmlaWithEmbedToken_ToPBIShared_NotSupported
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmlaWithEmbedToken_ToPBIShared_NotSupported");
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00030FC1 File Offset: 0x0002F1C1
		public static string XmlaClient_PbiPublicXmlaWithEmbedToken_UserId_Prop_NotSupported
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmlaWithEmbedToken_UserId_Prop_NotSupported");
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x00030FCD File Offset: 0x0002F1CD
		public static string XmlaClient_PbiPublicXmlaWithEmbedToken_Missing_InitialCatalog
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmlaWithEmbedToken_Missing_InitialCatalog");
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00030FD9 File Offset: 0x0002F1D9
		public static string XmlaClient_ASAzureRedirectionResolutionFailedWithThrottling
		{
			get
			{
				return XmlaSR.Keys.GetString("XmlaClient_ASAzureRedirectionResolutionFailedWithThrottling");
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00030FE5 File Offset: 0x0002F1E5
		public static string Decompression_InitializationFailed
		{
			get
			{
				return XmlaSR.Keys.GetString("Decompression_InitializationFailed");
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00030FF1 File Offset: 0x0002F1F1
		public static string Compression_InitializationFailed
		{
			get
			{
				return XmlaSR.Keys.GetString("Compression_InitializationFailed");
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x00030FFD File Offset: 0x0002F1FD
		public static string InvalidArgument
		{
			get
			{
				return XmlaSR.Keys.GetString("InvalidArgument");
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00031009 File Offset: 0x0002F209
		public static string ProvidePath
		{
			get
			{
				return XmlaSR.Keys.GetString("ProvidePath");
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x00031015 File Offset: 0x0002F215
		public static string InternalError
		{
			get
			{
				return XmlaSR.Keys.GetString("InternalError");
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00031021 File Offset: 0x0002F221
		public static string InternalErrorAndInvalidBufferType
		{
			get
			{
				return XmlaSR.Keys.GetString("InternalErrorAndInvalidBufferType");
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0003102D File Offset: 0x0002F22D
		public static string LocalCube_FileNotOpened(string cubeFile)
		{
			return XmlaSR.Keys.GetString("LocalCube_FileNotOpened", cubeFile);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0003103A File Offset: 0x0002F23A
		public static string Instance_NotFound(string instance, string server)
		{
			return XmlaSR.Keys.GetString("Instance_NotFound", instance, server);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00031048 File Offset: 0x0002F248
		public static string UnexpectedXsiType(string type)
		{
			return XmlaSR.Keys.GetString("UnexpectedXsiType", type);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00031055 File Offset: 0x0002F255
		public static string ConnectionString_InvalidPropertyNameFormat(string propertyName)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidPropertyNameFormat", propertyName);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00031062 File Offset: 0x0002F262
		public static string ConnectionString_UnsupportedPropertyValue(string propertyName, string value)
		{
			return XmlaSR.Keys.GetString("ConnectionString_UnsupportedPropertyValue", propertyName, value);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00031070 File Offset: 0x0002F270
		public static string ConnectionString_OpenedQuoteIsNotClosed(char openQuoteChar, int index)
		{
			return XmlaSR.Keys.GetString("ConnectionString_OpenedQuoteIsNotClosed", openQuoteChar, index);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00031088 File Offset: 0x0002F288
		public static string ConnectionString_ExpectedSemicolonNotFound(int index)
		{
			return XmlaSR.Keys.GetString("ConnectionString_ExpectedSemicolonNotFound", index);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003109A File Offset: 0x0002F29A
		public static string ConnectionString_ExpectedEqualSignNotFound(int fromIndex)
		{
			return XmlaSR.Keys.GetString("ConnectionString_ExpectedEqualSignNotFound", fromIndex);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x000310AC File Offset: 0x0002F2AC
		public static string ConnectionString_InvalidCharInPropertyName(char invalidChar, int index)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidCharInPropertyName", invalidChar, index);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x000310C4 File Offset: 0x0002F2C4
		public static string ConnectionString_InvalidCharInUnquotedPropertyValue(char invalidChar, int index)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidCharInUnquotedPropertyValue", invalidChar, index);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000310DC File Offset: 0x0002F2DC
		public static string ConnectionString_PropertyNameNotDefined(int equalIndex)
		{
			return XmlaSR.Keys.GetString("ConnectionString_PropertyNameNotDefined", equalIndex);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x000310EE File Offset: 0x0002F2EE
		public static string ConnectionString_InvalidIntegratedSecurityForNative(string integratedSecurity)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidIntegratedSecurityForNative", integratedSecurity);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x000310FB File Offset: 0x0002F2FB
		public static string ConnectionString_InvalidProtectionLevelForHttp(string protectionLevel)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidProtectionLevelForHttp", protectionLevel);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00031108 File Offset: 0x0002F308
		public static string ConnectionString_InvalidProtectionLevelForHttps(string protectionLevel)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidProtectionLevelForHttps", protectionLevel);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00031115 File Offset: 0x0002F315
		public static string ConnectionString_InvalidImpersonationLevelForHttp(string impersonationLevel)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidImpersonationLevelForHttp", impersonationLevel);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00031122 File Offset: 0x0002F322
		public static string ConnectionString_InvalidImpersonationLevelForHttps(string impersonationLevel)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidImpersonationLevelForHttps", impersonationLevel);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0003112F File Offset: 0x0002F32F
		public static string ConnectionString_InvalidIntegratedSecurityForHttpOrHttps(string integratedSecurity)
		{
			return XmlaSR.Keys.GetString("ConnectionString_InvalidIntegratedSecurityForHttpOrHttps", integratedSecurity);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0003113C File Offset: 0x0002F33C
		public static string ConnectionString_PropertyNotApplicableWithTheDataSourceType(string propertyName)
		{
			return XmlaSR.Keys.GetString("ConnectionString_PropertyNotApplicableWithTheDataSourceType", propertyName);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00031149 File Offset: 0x0002F349
		public static string ConnectionString_LinkFileParseError(int size)
		{
			return XmlaSR.Keys.GetString("ConnectionString_LinkFileParseError", size);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0003115B File Offset: 0x0002F35B
		public static string ConnectionString_LinkFileDownloadError(string linkFileName)
		{
			return XmlaSR.Keys.GetString("ConnectionString_LinkFileDownloadError", linkFileName);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00031168 File Offset: 0x0002F368
		public static string ConnectionString_ExternalConnectionIsIncomplete(string missingPropertyName)
		{
			return XmlaSR.Keys.GetString("ConnectionString_ExternalConnectionIsIncomplete", missingPropertyName);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00031175 File Offset: 0x0002F375
		public static string ConnectionString_ASAzure_FetchLinkReferenceFailed(string linkFileUri)
		{
			return XmlaSR.Keys.GetString("ConnectionString_ASAzure_FetchLinkReferenceFailed", linkFileUri);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00031182 File Offset: 0x0002F382
		public static string ConnectionString_ASAzure_InvalidLinkReferenceUri(string linkFileUri)
		{
			return XmlaSR.Keys.GetString("ConnectionString_ASAzure_InvalidLinkReferenceUri", linkFileUri);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003118F File Offset: 0x0002F38F
		public static string ConnectionString_ASAzure_InvalidLinkReferenceCustomPort(string linkFileUri)
		{
			return XmlaSR.Keys.GetString("ConnectionString_ASAzure_InvalidLinkReferenceCustomPort", linkFileUri);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0003119C File Offset: 0x0002F39C
		public static string ConnectionString_PbiDataset_Missing_Metadata(string missingParam)
		{
			return XmlaSR.Keys.GetString("ConnectionString_PbiDataset_Missing_Metadata", missingParam);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x000311A9 File Offset: 0x0002F3A9
		public static string ConnectionString_ShilohIsNoLongerSupported(string propertyName)
		{
			return XmlaSR.Keys.GetString("ConnectionString_ShilohIsNoLongerSupported", propertyName);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x000311B6 File Offset: 0x0002F3B6
		public static string UnrecognizedElementInMessagesSection(string elementName)
		{
			return XmlaSR.Keys.GetString("UnrecognizedElementInMessagesSection", elementName);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x000311C3 File Offset: 0x0002F3C3
		public static string UnexpectedElement(string elementName, string namespaceName)
		{
			return XmlaSR.Keys.GetString("UnexpectedElement", elementName, namespaceName);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000311D1 File Offset: 0x0002F3D1
		public static string MissingElement(string elementName, string namespaceName)
		{
			return XmlaSR.Keys.GetString("MissingElement", elementName, namespaceName);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000311DF File Offset: 0x0002F3DF
		public static string Authentication_Sspi_PackageNotFound(string packageName)
		{
			return XmlaSR.Keys.GetString("Authentication_Sspi_PackageNotFound", packageName);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x000311EC File Offset: 0x0002F3EC
		public static string Authentication_Sspi_PackageDoesntSupportCapability(string package, string capability)
		{
			return XmlaSR.Keys.GetString("Authentication_Sspi_PackageDoesntSupportCapability", package, capability);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x000311FA File Offset: 0x0002F3FA
		public static string Authentication_Sspi_FlagNotEstablished(string flagName)
		{
			return XmlaSR.Keys.GetString("Authentication_Sspi_FlagNotEstablished", flagName);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00031207 File Offset: 0x0002F407
		public static string Authentication_ClaimsToken_AdalLoadingError(string component)
		{
			return XmlaSR.Keys.GetString("Authentication_ClaimsToken_AdalLoadingError", component);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00031214 File Offset: 0x0002F414
		public static string Authentication_ClaimsToken_AdalError(string message)
		{
			return XmlaSR.Keys.GetString("Authentication_ClaimsToken_AdalError", message);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00031221 File Offset: 0x0002F421
		public static string DimeRecord_InvalidUriFormat(string uri)
		{
			return XmlaSR.Keys.GetString("DimeRecord_InvalidUriFormat", uri);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0003122E File Offset: 0x0002F42E
		public static string DimeRecord_VersionNotSupported(int version)
		{
			return XmlaSR.Keys.GetString("DimeRecord_VersionNotSupported", version);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00031240 File Offset: 0x0002F440
		public static string DimeRecord_TypeFormatShouldBeMedia(string value)
		{
			return XmlaSR.Keys.GetString("DimeRecord_TypeFormatShouldBeMedia", value);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003124D File Offset: 0x0002F44D
		public static string DimeRecord_TypeFormatShouldBeUnchanged(string value)
		{
			return XmlaSR.Keys.GetString("DimeRecord_TypeFormatShouldBeUnchanged", value);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003125A File Offset: 0x0002F45A
		public static string DimeRecord_ReservedFlagShouldBeZero(byte value)
		{
			return XmlaSR.Keys.GetString("DimeRecord_ReservedFlagShouldBeZero", value);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003126C File Offset: 0x0002F46C
		public static string DimeRecord_DataTypeNotSupported(string value)
		{
			return XmlaSR.Keys.GetString("DimeRecord_DataTypeNotSupported", value);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00031279 File Offset: 0x0002F479
		public static string DimeRecord_InvalidHeaderFlags(int begin, int end, int chunked)
		{
			return XmlaSR.Keys.GetString("DimeRecord_InvalidHeaderFlags", begin, end, chunked);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00031297 File Offset: 0x0002F497
		public static string Dime_DataTypeNotSupported(string value)
		{
			return XmlaSR.Keys.GetString("Dime_DataTypeNotSupported", value);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000312A4 File Offset: 0x0002F4A4
		public static string HttpStream_InvalidReadRequest(string state)
		{
			return XmlaSR.Keys.GetString("HttpStream_InvalidReadRequest", state);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x000312B1 File Offset: 0x0002F4B1
		public static string HttpStream_ResponseWithFailedStatus(string status, string reasonPhrase)
		{
			return XmlaSR.Keys.GetString("HttpStream_ResponseWithFailedStatus", status, reasonPhrase);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x000312BF File Offset: 0x0002F4BF
		public static string HttpStream_RequestPayloadStream_ErrorInWrite(string error, string eol)
		{
			return XmlaSR.Keys.GetString("HttpStream_RequestPayloadStream_ErrorInWrite", error, eol);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x000312CD File Offset: 0x0002F4CD
		public static string XmlaClient_PbiPublicXmla_InvalidDataSourceUriFormat(string uri)
		{
			return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmla_InvalidDataSourceUriFormat", uri);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000312DA File Offset: 0x0002F4DA
		public static string XmlaClient_PbiPremium_WorkspaceNotFound(string technicalDetails, string workspaceName)
		{
			return XmlaSR.Keys.GetString("XmlaClient_PbiPremium_WorkspaceNotFound", technicalDetails, workspaceName);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x000312E8 File Offset: 0x0002F4E8
		public static string XmlaClient_PbiPremium_WorkspaceNotOnPremium(string technicalDetails, string workspaceName)
		{
			return XmlaSR.Keys.GetString("XmlaClient_PbiPremium_WorkspaceNotOnPremium", technicalDetails, workspaceName);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x000312F6 File Offset: 0x0002F4F6
		public static string XmlaClient_PbiPremium_WorkspaceNameDuplicated(string technicalDetails, string workspaceName)
		{
			return XmlaSR.Keys.GetString("XmlaClient_PbiPremium_WorkspaceNameDuplicated", technicalDetails, workspaceName);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00031304 File Offset: 0x0002F504
		public static string XmlaClient_PbiPublicXmla_DatasetNotFound(string datasetFriendlyName, string technicalDetails)
		{
			return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmla_DatasetNotFound", datasetFriendlyName, technicalDetails);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00031312 File Offset: 0x0002F512
		public static string XmlaClient_PbiPublicXmla_DatasetNameDuplicated(string datasetFriendlyName, string technicalDetails)
		{
			return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmla_DatasetNameDuplicated", datasetFriendlyName, technicalDetails);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00031320 File Offset: 0x0002F520
		public static string XmlaClient_PbiPublicXmla_UnsupportedIdentityProvider(string identityProvider)
		{
			return XmlaSR.Keys.GetString("XmlaClient_PbiPublicXmla_UnsupportedIdentityProvider", identityProvider);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0003132D File Offset: 0x0002F52D
		public static string XmlaClient_ASAzureRedirectionResolutionFailedWithError(string aasInstance, string httpStatusCode)
		{
			return XmlaSR.Keys.GetString("XmlaClient_ASAzureRedirectionResolutionFailedWithError", aasInstance, httpStatusCode);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0003133B File Offset: 0x0002F53B
		public static string XmlaClient_ASAzureRedirectionResolutionMissingTenantId(string aasInstance)
		{
			return XmlaSR.Keys.GetString("XmlaClient_ASAzureRedirectionResolutionMissingTenantId", aasInstance);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00031348 File Offset: 0x0002F548
		public static string Decompression_Failed(int compressedSize, int expectedDecompressedSize, int actualDecompressedSize)
		{
			return XmlaSR.Keys.GetString("Decompression_Failed", compressedSize, expectedDecompressedSize, actualDecompressedSize);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00031366 File Offset: 0x0002F566
		public static string UnsupportedDataFormat(string format)
		{
			return XmlaSR.Keys.GetString("UnsupportedDataFormat", format);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00031373 File Offset: 0x0002F573
		public static string UnsupportedMethod(string name)
		{
			return XmlaSR.Keys.GetString("UnsupportedMethod", name);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00031380 File Offset: 0x0002F580
		public static string DirectoryNotFound(string path)
		{
			return XmlaSR.Keys.GetString("DirectoryNotFound", path);
		}

		// Token: 0x020001CD RID: 461
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x060013C5 RID: 5061 RVA: 0x00044E22 File Offset: 0x00043022
			private Keys()
			{
			}

			// Token: 0x170006E5 RID: 1765
			// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00044E2A File Offset: 0x0004302A
			// (set) Token: 0x060013C7 RID: 5063 RVA: 0x00044E31 File Offset: 0x00043031
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

			// Token: 0x060013C8 RID: 5064 RVA: 0x00044E39 File Offset: 0x00043039
			public static string GetString(string key)
			{
				return XmlaSR.Keys.resourceManager.GetString(key, XmlaSR.Keys._culture);
			}

			// Token: 0x060013C9 RID: 5065 RVA: 0x00044E4B File Offset: 0x0004304B
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, XmlaSR.Keys.resourceManager.GetString(key, XmlaSR.Keys._culture), arg0);
			}

			// Token: 0x060013CA RID: 5066 RVA: 0x00044E68 File Offset: 0x00043068
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, XmlaSR.Keys.resourceManager.GetString(key, XmlaSR.Keys._culture), arg0, arg1);
			}

			// Token: 0x060013CB RID: 5067 RVA: 0x00044E86 File Offset: 0x00043086
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, XmlaSR.Keys.resourceManager.GetString(key, XmlaSR.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x04000D27 RID: 3367
			private static ResourceManager resourceManager = new ResourceManager(typeof(XmlaSR).FullName, typeof(XmlaSR).Module.Assembly);

			// Token: 0x04000D28 RID: 3368
			private static CultureInfo _culture = null;

			// Token: 0x04000D29 RID: 3369
			public const string AlreadyConnected = "AlreadyConnected";

			// Token: 0x04000D2A RID: 3370
			public const string NotConnected = "NotConnected";

			// Token: 0x04000D2B RID: 3371
			public const string LocalCube_FileNotOpened = "LocalCube_FileNotOpened";

			// Token: 0x04000D2C RID: 3372
			public const string CannotConnect = "CannotConnect";

			// Token: 0x04000D2D RID: 3373
			public const string CannotConnectToRedirector = "CannotConnectToRedirector";

			// Token: 0x04000D2E RID: 3374
			public const string ConnectionBroken = "ConnectionBroken";

			// Token: 0x04000D2F RID: 3375
			public const string Instance_NotFound = "Instance_NotFound";

			// Token: 0x04000D30 RID: 3376
			public const string Reconnect_ConnectionInfoIsMissing = "Reconnect_ConnectionInfoIsMissing";

			// Token: 0x04000D31 RID: 3377
			public const string Reconnect_SessionIDIsMissing = "Reconnect_SessionIDIsMissing";

			// Token: 0x04000D32 RID: 3378
			public const string ServerDidNotProvideErrorInfo = "ServerDidNotProvideErrorInfo";

			// Token: 0x04000D33 RID: 3379
			public const string UnexpectedXsiType = "UnexpectedXsiType";

			// Token: 0x04000D34 RID: 3380
			public const string ConnectionCannotBeUsedWhileXmlReaderOpened = "ConnectionCannotBeUsedWhileXmlReaderOpened";

			// Token: 0x04000D35 RID: 3381
			public const string Connect_RedirectorDidntReturnDatabaseInfo = "Connect_RedirectorDidntReturnDatabaseInfo";

			// Token: 0x04000D36 RID: 3382
			public const string Connection_WorkbookIsOutdated = "Connection_WorkbookIsOutdated";

			// Token: 0x04000D37 RID: 3383
			public const string Connection_AnalysisServicesInstanceWasMoved = "Connection_AnalysisServicesInstanceWasMoved";

			// Token: 0x04000D38 RID: 3384
			public const string NetCore_NotSupportedFeature = "NetCore_NotSupportedFeature";

			// Token: 0x04000D39 RID: 3385
			public const string NetCore_WindowsOnlySupportedFeature = "NetCore_WindowsOnlySupportedFeature";

			// Token: 0x04000D3A RID: 3386
			public const string NetCore_WindowsDesktopOnlySupportedFeature = "NetCore_WindowsDesktopOnlySupportedFeature";

			// Token: 0x04000D3B RID: 3387
			public const string FailedToResolveCluster = "FailedToResolveCluster";

			// Token: 0x04000D3C RID: 3388
			public const string SoapFormatter_ResponseIsNotRowset = "SoapFormatter_ResponseIsNotRowset";

			// Token: 0x04000D3D RID: 3389
			public const string SoapFormatter_ResponseIsNotDataset = "SoapFormatter_ResponseIsNotDataset";

			// Token: 0x04000D3E RID: 3390
			public const string Cancel_SessionIDNotSpecified = "Cancel_SessionIDNotSpecified";

			// Token: 0x04000D3F RID: 3391
			public const string ConnectionString_Invalid = "ConnectionString_Invalid";

			// Token: 0x04000D40 RID: 3392
			public const string ConnectionString_InvalidPropertyNameFormat = "ConnectionString_InvalidPropertyNameFormat";

			// Token: 0x04000D41 RID: 3393
			public const string ConnectionString_DataSourceNotSpecified = "ConnectionString_DataSourceNotSpecified";

			// Token: 0x04000D42 RID: 3394
			public const string ConnectionString_UnsupportedPropertyValue = "ConnectionString_UnsupportedPropertyValue";

			// Token: 0x04000D43 RID: 3395
			public const string ConnectionString_OpenedQuoteIsNotClosed = "ConnectionString_OpenedQuoteIsNotClosed";

			// Token: 0x04000D44 RID: 3396
			public const string ConnectionString_ExpectedSemicolonNotFound = "ConnectionString_ExpectedSemicolonNotFound";

			// Token: 0x04000D45 RID: 3397
			public const string ConnectionString_ExpectedEqualSignNotFound = "ConnectionString_ExpectedEqualSignNotFound";

			// Token: 0x04000D46 RID: 3398
			public const string ConnectionString_InvalidCharInPropertyName = "ConnectionString_InvalidCharInPropertyName";

			// Token: 0x04000D47 RID: 3399
			public const string ConnectionString_InvalidCharInUnquotedPropertyValue = "ConnectionString_InvalidCharInUnquotedPropertyValue";

			// Token: 0x04000D48 RID: 3400
			public const string ConnectionString_PropertyNameNotDefined = "ConnectionString_PropertyNameNotDefined";

			// Token: 0x04000D49 RID: 3401
			public const string ConnectionString_InvalidIntegratedSecurityForNative = "ConnectionString_InvalidIntegratedSecurityForNative";

			// Token: 0x04000D4A RID: 3402
			public const string ConnectionString_InvalidProtectionLevelForHttp = "ConnectionString_InvalidProtectionLevelForHttp";

			// Token: 0x04000D4B RID: 3403
			public const string ConnectionString_InvalidProtectionLevelForHttps = "ConnectionString_InvalidProtectionLevelForHttps";

			// Token: 0x04000D4C RID: 3404
			public const string ConnectionString_InvalidImpersonationLevelForHttp = "ConnectionString_InvalidImpersonationLevelForHttp";

			// Token: 0x04000D4D RID: 3405
			public const string ConnectionString_InvalidImpersonationLevelForHttps = "ConnectionString_InvalidImpersonationLevelForHttps";

			// Token: 0x04000D4E RID: 3406
			public const string ConnectionString_InvalidIntegratedSecurityForHttpOrHttps = "ConnectionString_InvalidIntegratedSecurityForHttpOrHttps";

			// Token: 0x04000D4F RID: 3407
			public const string ConnectionString_MissingIdentityProviderForIntegratedSecurityFederated = "ConnectionString_MissingIdentityProviderForIntegratedSecurityFederated";

			// Token: 0x04000D50 RID: 3408
			public const string ConnectionString_InvalidIdentityProviderForIntegratedSecurityFederated = "ConnectionString_InvalidIdentityProviderForIntegratedSecurityFederated";

			// Token: 0x04000D51 RID: 3409
			public const string ConnectionString_PropertyNotApplicableWithTheDataSourceType = "ConnectionString_PropertyNotApplicableWithTheDataSourceType";

			// Token: 0x04000D52 RID: 3410
			public const string ConnectionString_DataSourceTypeDoesntSupportQuery = "ConnectionString_DataSourceTypeDoesntSupportQuery";

			// Token: 0x04000D53 RID: 3411
			public const string ConnectionString_LinkFileInvalidServer = "ConnectionString_LinkFileInvalidServer";

			// Token: 0x04000D54 RID: 3412
			public const string ConnectionString_LinkFileMissingServer = "ConnectionString_LinkFileMissingServer";

			// Token: 0x04000D55 RID: 3413
			public const string ConnectionString_LinkFileParseError = "ConnectionString_LinkFileParseError";

			// Token: 0x04000D56 RID: 3414
			public const string ConnectionString_LinkFileDupEffectiveUsername = "ConnectionString_LinkFileDupEffectiveUsername";

			// Token: 0x04000D57 RID: 3415
			public const string ConnectionString_LinkFileDownloadError = "ConnectionString_LinkFileDownloadError";

			// Token: 0x04000D58 RID: 3416
			public const string ConnectionString_LinkFileCannotRevert = "ConnectionString_LinkFileCannotRevert";

			// Token: 0x04000D59 RID: 3417
			public const string ConnectionString_LinkFileCannotDelegate = "ConnectionString_LinkFileCannotDelegate";

			// Token: 0x04000D5A RID: 3418
			public const string ConnectionString_MissingPassword = "ConnectionString_MissingPassword";

			// Token: 0x04000D5B RID: 3419
			public const string ConnectionString_ExternalConnectionIsIncomplete = "ConnectionString_ExternalConnectionIsIncomplete";

			// Token: 0x04000D5C RID: 3420
			public const string ConnectionString_AsAzure_DataSourcePathMoreThanOneSegment = "ConnectionString_AsAzure_DataSourcePathMoreThanOneSegment";

			// Token: 0x04000D5D RID: 3421
			public const string ConnectionString_PbiDedicated_MissingInitialCatalog = "ConnectionString_PbiDedicated_MissingInitialCatalog";

			// Token: 0x04000D5E RID: 3422
			public const string ConnectionString_PbiDedicated_MissingRestrictCatalog = "ConnectionString_PbiDedicated_MissingRestrictCatalog";

			// Token: 0x04000D5F RID: 3423
			public const string ConnectionString_AsAzure_SspiAndUseAdalCacheTogetherNotSupported = "ConnectionString_AsAzure_SspiAndUseAdalCacheTogetherNotSupported";

			// Token: 0x04000D60 RID: 3424
			public const string ConnectionString_ASAzure_FetchLinkReferenceFailed = "ConnectionString_ASAzure_FetchLinkReferenceFailed";

			// Token: 0x04000D61 RID: 3425
			public const string ConnectionString_ASAzure_InvalidLinkReferenceUri = "ConnectionString_ASAzure_InvalidLinkReferenceUri";

			// Token: 0x04000D62 RID: 3426
			public const string ConnectionString_ASAzure_InvalidLinkReferenceCustomPort = "ConnectionString_ASAzure_InvalidLinkReferenceCustomPort";

			// Token: 0x04000D63 RID: 3427
			public const string ConnectionString_Untrusted_Endpoint = "ConnectionString_Untrusted_Endpoint";

			// Token: 0x04000D64 RID: 3428
			public const string ConnectionString_PbiDataset_Missing_Metadata = "ConnectionString_PbiDataset_Missing_Metadata";

			// Token: 0x04000D65 RID: 3429
			public const string ConnectionString_ShilohIsNoLongerSupported = "ConnectionString_ShilohIsNoLongerSupported";

			// Token: 0x04000D66 RID: 3430
			public const string ConnectionString_SPN_Profile_Not_Supported = "ConnectionString_SPN_Profile_Not_Supported";

			// Token: 0x04000D67 RID: 3431
			public const string UnknownServerResponseFormat = "UnknownServerResponseFormat";

			// Token: 0x04000D68 RID: 3432
			public const string AfterExceptionAllTagsShouldCloseUntilMessagesSection = "AfterExceptionAllTagsShouldCloseUntilMessagesSection";

			// Token: 0x04000D69 RID: 3433
			public const string UnrecognizedElementInMessagesSection = "UnrecognizedElementInMessagesSection";

			// Token: 0x04000D6A RID: 3434
			public const string ErrorCodeIsMissingFromRowsetError = "ErrorCodeIsMissingFromRowsetError";

			// Token: 0x04000D6B RID: 3435
			public const string ErrorCodeIsMissingFromDatasetError = "ErrorCodeIsMissingFromDatasetError";

			// Token: 0x04000D6C RID: 3436
			public const string ExceptionRequiresXmlaErrorsInMessagesSection = "ExceptionRequiresXmlaErrorsInMessagesSection";

			// Token: 0x04000D6D RID: 3437
			public const string MessagesSectionIsEmpty = "MessagesSectionIsEmpty";

			// Token: 0x04000D6E RID: 3438
			public const string EmptyRootIsNotEmpty = "EmptyRootIsNotEmpty";

			// Token: 0x04000D6F RID: 3439
			public const string UnexpectedElement = "UnexpectedElement";

			// Token: 0x04000D70 RID: 3440
			public const string MissingElement = "MissingElement";

			// Token: 0x04000D71 RID: 3441
			public const string Resultset_IsNotRowset = "Resultset_IsNotRowset";

			// Token: 0x04000D72 RID: 3442
			public const string DataReaderClosedError = "DataReaderClosedError";

			// Token: 0x04000D73 RID: 3443
			public const string DataReaderInvalidRowError = "DataReaderInvalidRowError";

			// Token: 0x04000D74 RID: 3444
			public const string NonSequentialColumnAccessError = "NonSequentialColumnAccessError";

			// Token: 0x04000D75 RID: 3445
			public const string DataReader_IndexOutOfRange = "DataReader_IndexOutOfRange";

			// Token: 0x04000D76 RID: 3446
			public const string Authentication_Failed = "Authentication_Failed";

			// Token: 0x04000D77 RID: 3447
			public const string Authentication_Sspi_PackageNotFound = "Authentication_Sspi_PackageNotFound";

			// Token: 0x04000D78 RID: 3448
			public const string Authentication_Sspi_PackageDoesntSupportCapability = "Authentication_Sspi_PackageDoesntSupportCapability";

			// Token: 0x04000D79 RID: 3449
			public const string Authentication_Sspi_FlagNotEstablished = "Authentication_Sspi_FlagNotEstablished";

			// Token: 0x04000D7A RID: 3450
			public const string Authentication_Sspi_SchannelCantDelegate = "Authentication_Sspi_SchannelCantDelegate";

			// Token: 0x04000D7B RID: 3451
			public const string Authentication_Sspi_SchannelSupportsOnlyPrivacyLevel = "Authentication_Sspi_SchannelSupportsOnlyPrivacyLevel";

			// Token: 0x04000D7C RID: 3452
			public const string Authentication_Sspi_SchannelUnsupportedImpersonationLevel = "Authentication_Sspi_SchannelUnsupportedImpersonationLevel";

			// Token: 0x04000D7D RID: 3453
			public const string Authentication_Sspi_SchannelUnsupportedProtectionLevel = "Authentication_Sspi_SchannelUnsupportedProtectionLevel";

			// Token: 0x04000D7E RID: 3454
			public const string Authentication_Sspi_SchannelAnonymousAmbiguity = "Authentication_Sspi_SchannelAnonymousAmbiguity";

			// Token: 0x04000D7F RID: 3455
			public const string Authentication_MsoID_MissingSignInAssistant = "Authentication_MsoID_MissingSignInAssistant";

			// Token: 0x04000D80 RID: 3456
			public const string Authentication_MsoID_InternalError = "Authentication_MsoID_InternalError";

			// Token: 0x04000D81 RID: 3457
			public const string Authentication_MsoID_InvalidCredentials = "Authentication_MsoID_InvalidCredentials";

			// Token: 0x04000D82 RID: 3458
			public const string Authentication_MsoID_SsoFailed = "Authentication_MsoID_SsoFailed";

			// Token: 0x04000D83 RID: 3459
			public const string Authentication_MsoID_SsoFailedNonDomainUser = "Authentication_MsoID_SsoFailedNonDomainUser";

			// Token: 0x04000D84 RID: 3460
			public const string Authentication_ClaimsToken_AuthorityNotFound = "Authentication_ClaimsToken_AuthorityNotFound";

			// Token: 0x04000D85 RID: 3461
			public const string Authentication_ClaimsToken_UserIdAndPasswordRequired = "Authentication_ClaimsToken_UserIdAndPasswordRequired";

			// Token: 0x04000D86 RID: 3462
			public const string Authentication_ClaimsToken_IdentityProviderFormatInvalid = "Authentication_ClaimsToken_IdentityProviderFormatInvalid";

			// Token: 0x04000D87 RID: 3463
			public const string Authentication_ClaimsToken_AdalLoadingError = "Authentication_ClaimsToken_AdalLoadingError";

			// Token: 0x04000D88 RID: 3464
			public const string Authentication_ClaimsToken_AdalError = "Authentication_ClaimsToken_AdalError";

			// Token: 0x04000D89 RID: 3465
			public const string Authentication_AsAzure_OnlySspiOrClaimsTokenSupported = "Authentication_AsAzure_OnlySspiOrClaimsTokenSupported";

			// Token: 0x04000D8A RID: 3466
			public const string Authentication_PbiDedicated_OnlyClaimsTokenSupported = "Authentication_PbiDedicated_OnlyClaimsTokenSupported";

			// Token: 0x04000D8B RID: 3467
			public const string DimeReader_CannotReadFromStream = "DimeReader_CannotReadFromStream";

			// Token: 0x04000D8C RID: 3468
			public const string DimeReader_IsClosed = "DimeReader_IsClosed";

			// Token: 0x04000D8D RID: 3469
			public const string DimeReader_PreviousRecordStreamStillOpened = "DimeReader_PreviousRecordStreamStillOpened";

			// Token: 0x04000D8E RID: 3470
			public const string DimeRecord_StreamShouldBeReadable = "DimeRecord_StreamShouldBeReadable";

			// Token: 0x04000D8F RID: 3471
			public const string DimeRecord_StreamShouldBeWriteable = "DimeRecord_StreamShouldBeWriteable";

			// Token: 0x04000D90 RID: 3472
			public const string DimeRecord_InvalidContentLength = "DimeRecord_InvalidContentLength";

			// Token: 0x04000D91 RID: 3473
			public const string DimeRecord_PropertyOnlyAvailableForReadRecords = "DimeRecord_PropertyOnlyAvailableForReadRecords";

			// Token: 0x04000D92 RID: 3474
			public const string DimeRecord_InvalidChunkSize = "DimeRecord_InvalidChunkSize";

			// Token: 0x04000D93 RID: 3475
			public const string DimeRecord_UnableToReadFromStream = "DimeRecord_UnableToReadFromStream";

			// Token: 0x04000D94 RID: 3476
			public const string DimeRecord_StreamIsClosed = "DimeRecord_StreamIsClosed";

			// Token: 0x04000D95 RID: 3477
			public const string DimeRecord_ReadNotAllowed = "DimeRecord_ReadNotAllowed";

			// Token: 0x04000D96 RID: 3478
			public const string DimeRecord_WriteNotAllowed = "DimeRecord_WriteNotAllowed";

			// Token: 0x04000D97 RID: 3479
			public const string DimeRecord_TypeFormatEnumUnchangedNotAllowed = "DimeRecord_TypeFormatEnumUnchangedNotAllowed";

			// Token: 0x04000D98 RID: 3480
			public const string DimeRecord_MediaTypeNotDefined = "DimeRecord_MediaTypeNotDefined";

			// Token: 0x04000D99 RID: 3481
			public const string DimeRecord_InvalidUriFormat = "DimeRecord_InvalidUriFormat";

			// Token: 0x04000D9A RID: 3482
			public const string DimeRecord_NameMustNotBeDefinedForFormatNone = "DimeRecord_NameMustNotBeDefinedForFormatNone";

			// Token: 0x04000D9B RID: 3483
			public const string DimeRecord_EncodedTypeLengthExceeds8191 = "DimeRecord_EncodedTypeLengthExceeds8191";

			// Token: 0x04000D9C RID: 3484
			public const string DimeRecord_OffsetAndCountShouldBePositive = "DimeRecord_OffsetAndCountShouldBePositive";

			// Token: 0x04000D9D RID: 3485
			public const string DimeRecord_ContentLengthExceeded = "DimeRecord_ContentLengthExceeded";

			// Token: 0x04000D9E RID: 3486
			public const string DimeRecord_VersionNotSupported = "DimeRecord_VersionNotSupported";

			// Token: 0x04000D9F RID: 3487
			public const string DimeRecord_OnlySingleRecordMessagesAreSupported = "DimeRecord_OnlySingleRecordMessagesAreSupported";

			// Token: 0x04000DA0 RID: 3488
			public const string DimeRecord_TypeFormatShouldBeMedia = "DimeRecord_TypeFormatShouldBeMedia";

			// Token: 0x04000DA1 RID: 3489
			public const string DimeRecord_TypeFormatShouldBeUnchanged = "DimeRecord_TypeFormatShouldBeUnchanged";

			// Token: 0x04000DA2 RID: 3490
			public const string DimeRecord_ReservedFlagShouldBeZero = "DimeRecord_ReservedFlagShouldBeZero";

			// Token: 0x04000DA3 RID: 3491
			public const string DimeRecord_DataTypeShouldBeSpecifiedOnTheFirstChunk = "DimeRecord_DataTypeShouldBeSpecifiedOnTheFirstChunk";

			// Token: 0x04000DA4 RID: 3492
			public const string DimeRecord_DataTypeIsOnlyForTheFirstChunk = "DimeRecord_DataTypeIsOnlyForTheFirstChunk";

			// Token: 0x04000DA5 RID: 3493
			public const string DimeRecord_DataTypeNotSupported = "DimeRecord_DataTypeNotSupported";

			// Token: 0x04000DA6 RID: 3494
			public const string DimeRecord_InvalidHeaderFlags = "DimeRecord_InvalidHeaderFlags";

			// Token: 0x04000DA7 RID: 3495
			public const string DimeRecord_IDIsOnlyForFirstChunk = "DimeRecord_IDIsOnlyForFirstChunk";

			// Token: 0x04000DA8 RID: 3496
			public const string DimeWriter_CannotWriteToStream = "DimeWriter_CannotWriteToStream";

			// Token: 0x04000DA9 RID: 3497
			public const string DimeWriter_WriterIsClosed = "DimeWriter_WriterIsClosed";

			// Token: 0x04000DAA RID: 3498
			public const string DimeWriter_InvalidDefaultChunkSize = "DimeWriter_InvalidDefaultChunkSize";

			// Token: 0x04000DAB RID: 3499
			public const string Dime_DataTypeNotSupported = "Dime_DataTypeNotSupported";

			// Token: 0x04000DAC RID: 3500
			public const string TcpStream_MaxSignatureExceedsProtocolLimit = "TcpStream_MaxSignatureExceedsProtocolLimit";

			// Token: 0x04000DAD RID: 3501
			public const string HttpStream_InvalidReadRequest = "HttpStream_InvalidReadRequest";

			// Token: 0x04000DAE RID: 3502
			public const string HttpStream_ResponseWithFailedStatus = "HttpStream_ResponseWithFailedStatus";

			// Token: 0x04000DAF RID: 3503
			public const string HttpStream_RequestPayloadStream_InvalidStreamOperation = "HttpStream_RequestPayloadStream_InvalidStreamOperation";

			// Token: 0x04000DB0 RID: 3504
			public const string HttpStream_RequestPayloadStream_WriteAfterComplete = "HttpStream_RequestPayloadStream_WriteAfterComplete";

			// Token: 0x04000DB1 RID: 3505
			public const string HttpStream_RequestPayloadStream_InvalidAsyncResultType = "HttpStream_RequestPayloadStream_InvalidAsyncResultType";

			// Token: 0x04000DB2 RID: 3506
			public const string HttpStream_RequestPayloadStream_EndReadAlreadyCalled = "HttpStream_RequestPayloadStream_EndReadAlreadyCalled";

			// Token: 0x04000DB3 RID: 3507
			public const string HttpStream_RequestPayloadStream_ErrorInCallback = "HttpStream_RequestPayloadStream_ErrorInCallback";

			// Token: 0x04000DB4 RID: 3508
			public const string HttpStream_RequestPayloadStream_ErrorInWrite = "HttpStream_RequestPayloadStream_ErrorInWrite";

			// Token: 0x04000DB5 RID: 3509
			public const string IXMLAInterop_OnlyZeroOffsetIsSupported = "IXMLAInterop_OnlyZeroOffsetIsSupported";

			// Token: 0x04000DB6 RID: 3510
			public const string IXMLAInterop_StreamDoesNotSupportReverting = "IXMLAInterop_StreamDoesNotSupportReverting";

			// Token: 0x04000DB7 RID: 3511
			public const string IXMLAInterop_StreamDoesNotSupportLocking = "IXMLAInterop_StreamDoesNotSupportLocking";

			// Token: 0x04000DB8 RID: 3512
			public const string IXMLAInterop_StreamDoesNotSupportUnlocking = "IXMLAInterop_StreamDoesNotSupportUnlocking";

			// Token: 0x04000DB9 RID: 3513
			public const string IXMLAInterop_StreamCannotBeCloned = "IXMLAInterop_StreamCannotBeCloned";

			// Token: 0x04000DBA RID: 3514
			public const string XmlaClient_StartRequest_ThereIsAnotherPendingRequest = "XmlaClient_StartRequest_ThereIsAnotherPendingRequest";

			// Token: 0x04000DBB RID: 3515
			public const string XmlaClient_StartRequest_ThereIsAnotherPendingResponse = "XmlaClient_StartRequest_ThereIsAnotherPendingResponse";

			// Token: 0x04000DBC RID: 3516
			public const string XmlaClient_SendRequest_RequestStreamCannotBeRead = "XmlaClient_SendRequest_RequestStreamCannotBeRead";

			// Token: 0x04000DBD RID: 3517
			public const string XmlaClient_SendRequest_NoRequestWasCreated = "XmlaClient_SendRequest_NoRequestWasCreated";

			// Token: 0x04000DBE RID: 3518
			public const string XmlaClient_ConnectTimedOut = "XmlaClient_ConnectTimedOut";

			// Token: 0x04000DBF RID: 3519
			public const string XmlaClient_SendRequest_ThereIsAnotherPendingResponse = "XmlaClient_SendRequest_ThereIsAnotherPendingResponse";

			// Token: 0x04000DC0 RID: 3520
			public const string XmlaClient_CannotConnectToLocalCubeWithRestictedClient = "XmlaClient_CannotConnectToLocalCubeWithRestictedClient";

			// Token: 0x04000DC1 RID: 3521
			public const string XmlaClient_PbiPublicXmla_InvalidDataSourceUriFormat = "XmlaClient_PbiPublicXmla_InvalidDataSourceUriFormat";

			// Token: 0x04000DC2 RID: 3522
			public const string XmlaClient_PbiPremium_WorkspaceNotFound = "XmlaClient_PbiPremium_WorkspaceNotFound";

			// Token: 0x04000DC3 RID: 3523
			public const string XmlaClient_PbiPremium_WorkspaceNotOnPremium = "XmlaClient_PbiPremium_WorkspaceNotOnPremium";

			// Token: 0x04000DC4 RID: 3524
			public const string XmlaClient_PbiPremium_WorkspaceNameDuplicated = "XmlaClient_PbiPremium_WorkspaceNameDuplicated";

			// Token: 0x04000DC5 RID: 3525
			public const string XmlaClient_PbiPublicXmla_DatasetNotFound = "XmlaClient_PbiPublicXmla_DatasetNotFound";

			// Token: 0x04000DC6 RID: 3526
			public const string XmlaClient_PbiPublicXmla_DatasetNameDuplicated = "XmlaClient_PbiPublicXmla_DatasetNameDuplicated";

			// Token: 0x04000DC7 RID: 3527
			public const string XmlaClient_PbiPublicXmla_DatasetNotSpecified = "XmlaClient_PbiPublicXmla_DatasetNotSpecified";

			// Token: 0x04000DC8 RID: 3528
			public const string XmlaClient_PbiPublicXmlaWithEmbedToken_ToPBIShared_NotSupported = "XmlaClient_PbiPublicXmlaWithEmbedToken_ToPBIShared_NotSupported";

			// Token: 0x04000DC9 RID: 3529
			public const string XmlaClient_PbiPublicXmlaWithEmbedToken_UserId_Prop_NotSupported = "XmlaClient_PbiPublicXmlaWithEmbedToken_UserId_Prop_NotSupported";

			// Token: 0x04000DCA RID: 3530
			public const string XmlaClient_PbiPublicXmlaWithEmbedToken_Missing_InitialCatalog = "XmlaClient_PbiPublicXmlaWithEmbedToken_Missing_InitialCatalog";

			// Token: 0x04000DCB RID: 3531
			public const string XmlaClient_PbiPublicXmla_UnsupportedIdentityProvider = "XmlaClient_PbiPublicXmla_UnsupportedIdentityProvider";

			// Token: 0x04000DCC RID: 3532
			public const string XmlaClient_ASAzureRedirectionResolutionFailedWithError = "XmlaClient_ASAzureRedirectionResolutionFailedWithError";

			// Token: 0x04000DCD RID: 3533
			public const string XmlaClient_ASAzureRedirectionResolutionMissingTenantId = "XmlaClient_ASAzureRedirectionResolutionMissingTenantId";

			// Token: 0x04000DCE RID: 3534
			public const string XmlaClient_ASAzureRedirectionResolutionFailedWithThrottling = "XmlaClient_ASAzureRedirectionResolutionFailedWithThrottling";

			// Token: 0x04000DCF RID: 3535
			public const string Decompression_InitializationFailed = "Decompression_InitializationFailed";

			// Token: 0x04000DD0 RID: 3536
			public const string Decompression_Failed = "Decompression_Failed";

			// Token: 0x04000DD1 RID: 3537
			public const string Compression_InitializationFailed = "Compression_InitializationFailed";

			// Token: 0x04000DD2 RID: 3538
			public const string InvalidArgument = "InvalidArgument";

			// Token: 0x04000DD3 RID: 3539
			public const string UnsupportedDataFormat = "UnsupportedDataFormat";

			// Token: 0x04000DD4 RID: 3540
			public const string UnsupportedMethod = "UnsupportedMethod";

			// Token: 0x04000DD5 RID: 3541
			public const string ProvidePath = "ProvidePath";

			// Token: 0x04000DD6 RID: 3542
			public const string DirectoryNotFound = "DirectoryNotFound";

			// Token: 0x04000DD7 RID: 3543
			public const string InternalError = "InternalError";

			// Token: 0x04000DD8 RID: 3544
			public const string InternalErrorAndInvalidBufferType = "InternalErrorAndInvalidBufferType";
		}
	}
}
