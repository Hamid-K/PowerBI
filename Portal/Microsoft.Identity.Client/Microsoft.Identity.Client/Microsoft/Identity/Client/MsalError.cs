using System;
using System.ComponentModel;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200016C RID: 364
	public static class MsalError
	{
		// Token: 0x04000558 RID: 1368
		public const string InvalidGrantError = "invalid_grant";

		// Token: 0x04000559 RID: 1369
		public const string InteractionRequired = "interaction_required";

		// Token: 0x0400055A RID: 1370
		public const string NoTokensFoundError = "no_tokens_found";

		// Token: 0x0400055B RID: 1371
		public const string UserNullError = "user_null";

		// Token: 0x0400055C RID: 1372
		public const string UserAssertionNullError = "user_assertion_null";

		// Token: 0x0400055D RID: 1373
		public const string CurrentBrokerAccount = "current_broker_account";

		// Token: 0x0400055E RID: 1374
		public const string NoAccountForLoginHint = "no_account_for_login_hint";

		// Token: 0x0400055F RID: 1375
		public const string MultipleAccountsForLoginHint = "multiple_accounts_for_login_hint";

		// Token: 0x04000560 RID: 1376
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This error code is not in use")]
		public const string TokenCacheNullError = "token_cache_null";

		// Token: 0x04000561 RID: 1377
		public const string NoPromptFailedError = "no_prompt_failed";

		// Token: 0x04000562 RID: 1378
		public const string ServiceNotAvailable = "service_not_available";

		// Token: 0x04000563 RID: 1379
		public const string RequestTimeout = "request_timeout";

		// Token: 0x04000564 RID: 1380
		public const string RequestThrottled = "request_throttled";

		// Token: 0x04000565 RID: 1381
		public const string UpnRequired = "upn_required";

		// Token: 0x04000566 RID: 1382
		public const string MissingPassiveAuthEndpoint = "missing_passive_auth_endpoint";

		// Token: 0x04000567 RID: 1383
		public const string InvalidAuthority = "invalid_authority";

		// Token: 0x04000568 RID: 1384
		public const string InvalidAuthorityType = "invalid_authority_type";

		// Token: 0x04000569 RID: 1385
		public const string UnauthorizedClient = "unauthorized_client";

		// Token: 0x0400056A RID: 1386
		public const string UnknownError = "unknown_error";

		// Token: 0x0400056B RID: 1387
		public const string UnknownBrokerError = "unknown_broker_error";

		// Token: 0x0400056C RID: 1388
		public const string WamFailedToSignout = "wam_failed_to_signout";

		// Token: 0x0400056D RID: 1389
		public const string AuthenticationFailed = "authentication_failed";

		// Token: 0x0400056E RID: 1390
		public const string AuthorityValidationFailed = "authority_validation_failed";

		// Token: 0x0400056F RID: 1391
		public const string InvalidOwnerWindowType = "invalid_owner_window_type";

		// Token: 0x04000570 RID: 1392
		public const string EncodedTokenTooLong = "encoded_token_too_long";

		// Token: 0x04000571 RID: 1393
		public const string UserMismatch = "user_mismatch";

		// Token: 0x04000572 RID: 1394
		public const string FailedToRefreshToken = "failed_to_refresh_token";

		// Token: 0x04000573 RID: 1395
		public const string FailedToAcquireTokenSilentlyFromBroker = "failed_to_acquire_token_silently_from_broker";

		// Token: 0x04000574 RID: 1396
		public const string RedirectUriValidationFailed = "redirect_uri_validation_failed";

		// Token: 0x04000575 RID: 1397
		public const string AuthenticationUiFailed = "authentication_ui_failed";

		// Token: 0x04000576 RID: 1398
		public const string InternalError = "internal_error";

		// Token: 0x04000577 RID: 1399
		public const string AccessingWsMetadataExchangeFailed = "accessing_ws_metadata_exchange_failed";

		// Token: 0x04000578 RID: 1400
		public const string FederatedServiceReturnedError = "federated_service_returned_error";

		// Token: 0x04000579 RID: 1401
		public const string UserRealmDiscoveryFailed = "user_realm_discovery_failed";

		// Token: 0x0400057A RID: 1402
		public const string RopcDoesNotSupportMsaAccounts = "ropc_not_supported_for_msa";

		// Token: 0x0400057B RID: 1403
		public const string MissingFederationMetadataUrl = "missing_federation_metadata_url";

		// Token: 0x0400057C RID: 1404
		public const string ParsingWsMetadataExchangeFailed = "parsing_ws_metadata_exchange_failed";

		// Token: 0x0400057D RID: 1405
		public const string WsTrustEndpointNotFoundInMetadataDocument = "wstrust_endpoint_not_found";

		// Token: 0x0400057E RID: 1406
		public const string ParsingWsTrustResponseFailed = "parsing_wstrust_response_failed";

		// Token: 0x0400057F RID: 1407
		public const string IntegratedWindowsAuthenticationFailed = "integrated_windows_authentication_failed";

		// Token: 0x04000580 RID: 1408
		public const string UnknownUserType = "unknown_user_type";

		// Token: 0x04000581 RID: 1409
		public const string UnknownUser = "unknown_user";

		// Token: 0x04000582 RID: 1410
		public const string GetUserNameFailed = "get_user_name_failed";

		// Token: 0x04000583 RID: 1411
		public const string PasswordRequiredForManagedUserError = "password_required_for_managed_user";

		// Token: 0x04000584 RID: 1412
		public const string InvalidRequest = "invalid_request";

		// Token: 0x04000585 RID: 1413
		public const string UapCannotFindDomainUser = "user_information_access_failed";

		// Token: 0x04000586 RID: 1414
		public const string UapCannotFindUpn = "uap_cannot_find_upn";

		// Token: 0x04000587 RID: 1415
		public const string NonParsableOAuthError = "non_parsable_oauth_error";

		// Token: 0x04000588 RID: 1416
		public const string CodeExpired = "code_expired";

		// Token: 0x04000589 RID: 1417
		public const string IntegratedWindowsAuthNotSupportedForManagedUser = "integrated_windows_auth_not_supported_managed_user";

		// Token: 0x0400058A RID: 1418
		public const string ActivityRequired = "activity_required";

		// Token: 0x0400058B RID: 1419
		public const string BrokerResponseHashMismatch = "broker_response_hash_mismatch";

		// Token: 0x0400058C RID: 1420
		public const string BrokerResponseReturnedError = "broker_response_returned_error";

		// Token: 0x0400058D RID: 1421
		public const string BrokerNonceMismatch = "broker_nonce_mismatch";

		// Token: 0x0400058E RID: 1422
		public const string CannotInvokeBroker = "cannot_invoke_broker";

		// Token: 0x0400058F RID: 1423
		public const string NoAndroidBrokerAccountFound = "no_broker_account_found";

		// Token: 0x04000590 RID: 1424
		public const string NoAndroidBrokerInstalledOnDevice = "No_Broker_Installed_On_Device";

		// Token: 0x04000591 RID: 1425
		public const string NullIntentReturnedFromAndroidBroker = "null_intent_returned_from_broker";

		// Token: 0x04000592 RID: 1426
		public const string AndroidBrokerSignatureVerificationFailed = "broker_signature_verification_failed";

		// Token: 0x04000593 RID: 1427
		public const string AndroidBrokerOperationFailed = "android_broker_operation_failed";

		// Token: 0x04000594 RID: 1428
		public const string NoUsernameOrAccountIDProvidedForSilentAndroidBrokerAuthentication = "no_username_or_accountid_provided_for_silent_android_broker_authentication";

		// Token: 0x04000595 RID: 1429
		public const string HttpStatusNotFound = "not_found";

		// Token: 0x04000596 RID: 1430
		public const string HttpStatusCodeNotOk = "http_status_not_200";

		// Token: 0x04000597 RID: 1431
		public const string CustomWebUiReturnedInvalidUri = "custom_webui_returned_invalid_uri";

		// Token: 0x04000598 RID: 1432
		public const string CustomWebUiRedirectUriMismatch = "custom_webui_invalid_mismatch";

		// Token: 0x04000599 RID: 1433
		public const string AccessDenied = "access_denied";

		// Token: 0x0400059A RID: 1434
		public const string CannotAccessUserInformationOrUserNotDomainJoined = "user_information_access_failed";

		// Token: 0x0400059B RID: 1435
		public const string DefaultRedirectUriIsInvalid = "redirect_uri_validation_failed";

		// Token: 0x0400059C RID: 1436
		public const string NoRedirectUri = "no_redirect_uri";

		// Token: 0x0400059D RID: 1437
		public const string MultipleTokensMatchedError = "multiple_matching_tokens_detected";

		// Token: 0x0400059E RID: 1438
		public const string NonHttpsRedirectNotSupported = "non_https_redirect_failed";

		// Token: 0x0400059F RID: 1439
		[Obsolete("MSAL no longer throws this error - it will allow the HttpClient exceptions to propagate. App developers may write their own logic for detecting access to the network issues, for example by using Xamarin.Essentials. ")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public const string NetworkNotAvailableError = "network_not_available";

		// Token: 0x040005A0 RID: 1440
		public const string B2CAuthorityHostMismatch = "B2C_authority_host_mismatch";

		// Token: 0x040005A1 RID: 1441
		public const string AuthorityHostMismatch = "authority_host_mismatch";

		// Token: 0x040005A2 RID: 1442
		public const string DuplicateQueryParameterError = "duplicate_query_parameter";

		// Token: 0x040005A3 RID: 1443
		public const string AuthenticationUiFailedError = "authentication_ui_failed";

		// Token: 0x040005A4 RID: 1444
		public const string AuthenticationCanceledError = "authentication_canceled";

		// Token: 0x040005A5 RID: 1445
		public const string JsonParseError = "json_parse_failed";

		// Token: 0x040005A6 RID: 1446
		public const string InvalidJwtError = "invalid_jwt";

		// Token: 0x040005A7 RID: 1447
		public const string StateMismatchError = "state_mismatch";

		// Token: 0x040005A8 RID: 1448
		public const string TenantDiscoveryFailedError = "tenant_discovery_failed";

		// Token: 0x040005A9 RID: 1449
		public const string PlatformNotSupported = "platform_not_supported";

		// Token: 0x040005AA RID: 1450
		public const string InvalidAuthorizationUri = "invalid_authorization_uri";

		// Token: 0x040005AB RID: 1451
		public const string LoopbackRedirectUri = "loopback_redirect_uri";

		// Token: 0x040005AC RID: 1452
		public const string LoopbackResponseUriMismatch = "loopback_response_uri_mismatch";

		// Token: 0x040005AD RID: 1453
		public const string LinuxXdgOpen = "linux_xdg_open_failed";

		// Token: 0x040005AE RID: 1454
		public const string WebviewUnavailable = "no_system_webview";

		// Token: 0x040005AF RID: 1455
		public const string SystemWebviewOptionsNotApplicable = "embedded_webview_not_compatible_default_browser";

		// Token: 0x040005B0 RID: 1456
		public const string ClientCredentialAuthenticationTypesAreMutuallyExclusive = "Client_Credential_Authentication_Types_Are_Mutually_Exclusive";

		// Token: 0x040005B1 RID: 1457
		public const string ClientCredentialAuthenticationTypeMustBeDefined = "Client_Credentials_Required_In_Confidential_Client_Application";

		// Token: 0x040005B2 RID: 1458
		internal const string BasicAction = "basic_action";

		// Token: 0x040005B3 RID: 1459
		internal const string AdditionalAction = "additional_action";

		// Token: 0x040005B4 RID: 1460
		internal const string MessageOnly = "message_only";

		// Token: 0x040005B5 RID: 1461
		internal const string UserPasswordExpired = "user_password_expired";

		// Token: 0x040005B6 RID: 1462
		internal const string ConsentRequired = "consent_required";

		// Token: 0x040005B7 RID: 1463
		internal const string BadToken = "bad_token";

		// Token: 0x040005B8 RID: 1464
		internal const string TokenExpired = "token_expired";

		// Token: 0x040005B9 RID: 1465
		internal const string ProtectionPolicyRequired = "protection_policy_required";

		// Token: 0x040005BA RID: 1466
		internal const string ClientMismatch = "client_mismatch";

		// Token: 0x040005BB RID: 1467
		internal const string DeviceAuthenticationFailed = "device_authentication_failed";

		// Token: 0x040005BC RID: 1468
		public const string InvalidInstance = "invalid_instance";

		// Token: 0x040005BD RID: 1469
		public const string InvalidUserInstanceMetadata = "invalid-custom-instance-metadata";

		// Token: 0x040005BE RID: 1470
		public const string ValidateAuthorityOrCustomMetadata = "validate_authority_or_custom_instance_metadata";

		// Token: 0x040005BF RID: 1471
		public const string NoClientId = "no_client_id";

		// Token: 0x040005C0 RID: 1472
		public const string TelemetryConfigOrTelemetryCallback = "telemetry_config_or_telemetry_callback";

		// Token: 0x040005C1 RID: 1473
		public const string InvalidClient = "invalid_client";

		// Token: 0x040005C2 RID: 1474
		public const string SSHCertUsedAsHttpHeader = "ssh_cert_used_as_http_header";

		// Token: 0x040005C3 RID: 1475
		public const string WABError = "wab_error";

		// Token: 0x040005C4 RID: 1476
		public const string TokenTypeMismatch = "token_type_mismatch";

		// Token: 0x040005C5 RID: 1477
		public const string AccessTokenTypeMissing = "token_type_missing";

		// Token: 0x040005C6 RID: 1478
		public const string ExperimentalFeature = "experimental_feature";

		// Token: 0x040005C7 RID: 1479
		public const string BrokerApplicationRequired = "broker_application_required";

		// Token: 0x040005C8 RID: 1480
		public const string FailedToGetBrokerResponse = "failed_to_get_broker_response";

		// Token: 0x040005C9 RID: 1481
		public const string InvalidJsonClaimsFormat = "invalid_json_claims_format";

		// Token: 0x040005CA RID: 1482
		public const string AuthorityTypeMismatch = "authority_type_mismatch";

		// Token: 0x040005CB RID: 1483
		public const string AuthorityTenantSpecifiedTwice = "authority_tenant_specified_twice";

		// Token: 0x040005CC RID: 1484
		public const string CustomMetadataInstanceOrUri = "custom_metadata_instance_or_uri";

		// Token: 0x040005CD RID: 1485
		public const string ScopesRequired = "scopes_required_client_credentials";

		// Token: 0x040005CE RID: 1486
		public const string CertWithoutPrivateKey = "cert_without_private_key";

		// Token: 0x040005CF RID: 1487
		public const string CertificateNotRsa = "certificate_not_rsa";

		// Token: 0x040005D0 RID: 1488
		public const string DeviceCertificateNotFound = "device_certificate_not_found";

		// Token: 0x040005D1 RID: 1489
		public const string InvalidAdalCacheMultipleRTs = "invalid_adal_cache";

		// Token: 0x040005D2 RID: 1490
		public const string RegionDiscoveryFailed = "region_discovery_failed";

		// Token: 0x040005D3 RID: 1491
		public const string RegionDiscoveryNotEnabled = "region_discovery_unavailable";

		// Token: 0x040005D4 RID: 1492
		public const string BrokerDoesNotSupportPop = "broker_does_not_support_pop";

		// Token: 0x040005D5 RID: 1493
		public const string BrokerRequiredForPop = "broker_required_for_pop";

		// Token: 0x040005D6 RID: 1494
		public const string AdfsNotSupportedWithBroker = "adfs_not_supported_with_broker";

		// Token: 0x040005D7 RID: 1495
		public const string NonceRequiredForPopOnPCA = "nonce_required_for_pop_on_pca";

		// Token: 0x040005D8 RID: 1496
		public const string WamUiThread = "wam_ui_thread_only";

		// Token: 0x040005D9 RID: 1497
		public const string WamNoB2C = "wam_no_b2c";

		// Token: 0x040005DA RID: 1498
		public const string WamInteractiveError = "wam_interactive_error";

		// Token: 0x040005DB RID: 1499
		public const string WamPickerError = "wam_interactive_picker_error";

		// Token: 0x040005DC RID: 1500
		public const string WamScopesRequired = "scopes_required_wam";

		// Token: 0x040005DD RID: 1501
		public const string WebView2NotInstalled = "webview2_runtime_not_installed";

		// Token: 0x040005DE RID: 1502
		public const string WebView2LoaderNotFound = "webview2loader_not_found";

		// Token: 0x040005DF RID: 1503
		public const string RegionalAuthorityValidation = "regional_authority_validation";

		// Token: 0x040005E0 RID: 1504
		public const string RegionDiscoveryWithCustomInstanceMetadata = "region_discovery_with_custom_instance_metadata";

		// Token: 0x040005E1 RID: 1505
		public const string HttpListenerError = "http_listener_error";

		// Token: 0x040005E2 RID: 1506
		public const string InitializeProcessSecurityError = "initialize_process_security_error";

		// Token: 0x040005E3 RID: 1507
		public const string StaticCacheWithExternalSerialization = "static_cache_with_external_serialization";

		// Token: 0x040005E4 RID: 1508
		public const string TenantOverrideNonAad = "tenant_override_non_aad";

		// Token: 0x040005E5 RID: 1509
		public const string RegionalAndAuthorityOverride = "authority_override_regional";

		// Token: 0x040005E6 RID: 1510
		public const string OboCacheKeyNotInCacheError = "obo_cache_key_not_in_cache_error";

		// Token: 0x040005E7 RID: 1511
		public const string InvalidTokenProviderResponseValue = "invalid_token_provider_response_value";

		// Token: 0x040005E8 RID: 1512
		public const string UnableToParseAuthenticationHeader = "unable_to_parse_authentication_header";

		// Token: 0x040005E9 RID: 1513
		public const string InvalidManagedIdentityResponse = "invalid_managed_identity_response";

		// Token: 0x040005EA RID: 1514
		public const string ManagedIdentityRequestFailed = "managed_identity_request_failed";

		// Token: 0x040005EB RID: 1515
		public const string ManagedIdentityUnreachableNetwork = "managed_identity_unreachable_network";

		// Token: 0x040005EC RID: 1516
		public const string UnknownManagedIdentityError = "unknown_managed_identity_error";

		// Token: 0x040005ED RID: 1517
		public const string InvalidManagedIdentityEndpoint = "invalid_managed_identity_endpoint";

		// Token: 0x040005EE RID: 1518
		public const string ExactlyOneScopeExpected = "exactly_one_scope_expected";

		// Token: 0x040005EF RID: 1519
		public const string UserAssignedManagedIdentityNotSupported = "user_assigned_managed_identity_not_supported";

		// Token: 0x040005F0 RID: 1520
		public const string UserAssignedManagedIdentityNotConfigurableAtRuntime = "user_assigned_managed_identity_not_configurable_at_runtime";

		// Token: 0x040005F1 RID: 1521
		public const string CombinedUserAppCacheNotSupported = "combined_user_app_cache_not_supported";

		// Token: 0x040005F2 RID: 1522
		public const string SetCiamAuthorityAtRequestLevelNotSupported = "set_ciam_authority_at_request_level_not_supported";

		// Token: 0x040005F3 RID: 1523
		public const string CryptographicError = "cryptographic_error";
	}
}
