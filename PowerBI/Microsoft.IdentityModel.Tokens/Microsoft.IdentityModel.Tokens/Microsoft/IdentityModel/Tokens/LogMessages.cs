using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000162 RID: 354
	internal static class LogMessages
	{
		// Token: 0x04000562 RID: 1378
		public const string IDX10101 = "IDX10101: MaximumTokenSizeInBytes must be greater than zero. value: '{0}'";

		// Token: 0x04000563 RID: 1379
		public const string IDX10100 = "IDX10100: ClockSkew must be greater than TimeSpan.Zero. value: '{0}'";

		// Token: 0x04000564 RID: 1380
		public const string IDX10102 = "IDX10102: NameClaimType cannot be null or whitespace.";

		// Token: 0x04000565 RID: 1381
		public const string IDX10103 = "IDX10103: RoleClaimType cannot be null or whitespace.";

		// Token: 0x04000566 RID: 1382
		public const string IDX10104 = "IDX10104: TokenLifetimeInMinutes must be greater than zero. value: '{0}'";

		// Token: 0x04000567 RID: 1383
		public const string IDX10105 = "IDX10105: ClaimValue that is a collection of collections is not supported. Such ClaimValue is found for ClaimType : '{0}'";

		// Token: 0x04000568 RID: 1384
		public const string IDX10107 = "IDX10107: When setting RefreshInterval, the value must be greater than MinimumRefreshInterval: '{0}'. value: '{1}'.";

		// Token: 0x04000569 RID: 1385
		public const string IDX10108 = "IDX10108: When setting AutomaticRefreshInterval, the value must be greater than MinimumAutomaticRefreshInterval: '{0}'. value: '{1}'.";

		// Token: 0x0400056A RID: 1386
		public const string IDX10109 = "IDX10109: Warning: Claims is being accessed without first reading the properties TokenValidationResult.IsValid or TokenValidationResult.Exception. This could be a potential security issue.";

		// Token: 0x0400056B RID: 1387
		public const string IDX10110 = "IDX10110: When setting LastKnownGoodLifetime, the value must be greater than or equal to zero. value: '{0}'.";

		// Token: 0x0400056C RID: 1388
		public const string IDX10204 = "IDX10204: Unable to validate issuer. validationParameters.ValidIssuer is null or whitespace AND validationParameters.ValidIssuers is null or empty.";

		// Token: 0x0400056D RID: 1389
		public const string IDX10205 = "IDX10205: Issuer validation failed. Issuer: '{0}'. Did not match: validationParameters.ValidIssuer: '{1}' or validationParameters.ValidIssuers: '{2}' or validationParameters.ConfigurationManager.CurrentConfiguration.Issuer: '{3}'. For more details, see https://aka.ms/IdentityModel/issuer-validation. ";

		// Token: 0x0400056E RID: 1390
		public const string IDX10206 = "IDX10206: Unable to validate audience. The 'audiences' parameter is empty.";

		// Token: 0x0400056F RID: 1391
		public const string IDX10207 = "IDX10207: Unable to validate audience. The 'audiences' parameter is null.";

		// Token: 0x04000570 RID: 1392
		public const string IDX10208 = "IDX10208: Unable to validate audience. validationParameters.ValidAudience is null or whitespace and validationParameters.ValidAudiences is null.";

		// Token: 0x04000571 RID: 1393
		public const string IDX10209 = "IDX10209: Token has length: '{0}' which is larger than the MaximumTokenSizeInBytes: '{1}'.";

		// Token: 0x04000572 RID: 1394
		public const string IDX10211 = "IDX10211: Unable to validate issuer. The 'issuer' parameter is null or whitespace";

		// Token: 0x04000573 RID: 1395
		public const string IDX10214 = "IDX10214: Audience validation failed. Audiences: '{0}'. Did not match: validationParameters.ValidAudience: '{1}' or validationParameters.ValidAudiences: '{2}'.";

		// Token: 0x04000574 RID: 1396
		public const string IDX10222 = "IDX10222: Lifetime validation failed. The token is not yet valid. ValidFrom (UTC): '{0}', Current time (UTC): '{1}'.";

		// Token: 0x04000575 RID: 1397
		public const string IDX10223 = "IDX10223: Lifetime validation failed. The token is expired. ValidTo (UTC): '{0}', Current time (UTC): '{1}'.";

		// Token: 0x04000576 RID: 1398
		public const string IDX10224 = "IDX10224: Lifetime validation failed. The NotBefore (UTC): '{0}' is after Expires (UTC): '{1}'.";

		// Token: 0x04000577 RID: 1399
		public const string IDX10225 = "IDX10225: Lifetime validation failed. The token is missing an Expiration Time. Tokentype: '{0}'.";

		// Token: 0x04000578 RID: 1400
		public const string IDX10227 = "IDX10227: TokenValidationParameters.TokenReplayCache is not null, indicating to check for token replay but the security token has no expiration time: token '{0}'.";

		// Token: 0x04000579 RID: 1401
		public const string IDX10228 = "IDX10228: The securityToken has previously been validated, securityToken: '{0}'.";

		// Token: 0x0400057A RID: 1402
		public const string IDX10229 = "IDX10229: TokenValidationParameters.TokenReplayCache was unable to add the securityToken: '{0}'.";

		// Token: 0x0400057B RID: 1403
		public const string IDX10230 = "IDX10230: Lifetime validation failed. Delegate returned false, securitytoken: '{0}'.";

		// Token: 0x0400057C RID: 1404
		public const string IDX10231 = "IDX10231: Audience validation failed. Delegate returned false, securitytoken: '{0}'.";

		// Token: 0x0400057D RID: 1405
		public const string IDX10232 = "IDX10232: IssuerSigningKey validation failed. Delegate returned false, securityKey: '{0}'.";

		// Token: 0x0400057E RID: 1406
		public const string IDX10233 = "IDX10233: ValidateAudience property on ValidationParameters is set to false. Exiting without validating the audience.";

		// Token: 0x0400057F RID: 1407
		public const string IDX10234 = "IDX10234: Audience Validated.Audience: '{0}'";

		// Token: 0x04000580 RID: 1408
		public const string IDX10235 = "IDX10235: ValidateIssuer property on ValidationParameters is set to false. Exiting without validating the issuer.";

		// Token: 0x04000581 RID: 1409
		public const string IDX10236 = "IDX10236: Issuer Validated.Issuer: '{0}'";

		// Token: 0x04000582 RID: 1410
		public const string IDX10237 = "IDX10237: ValidateIssuerSigningKey property on ValidationParameters is set to false. Exiting without validating the issuer signing key.";

		// Token: 0x04000583 RID: 1411
		public const string IDX10238 = "IDX10238: ValidateLifetime property on ValidationParameters is set to false. Exiting without validating the lifetime.";

		// Token: 0x04000584 RID: 1412
		public const string IDX10239 = "IDX10239: Lifetime of the token is valid.";

		// Token: 0x04000585 RID: 1413
		public const string IDX10240 = "IDX10240: No token replay is detected.";

		// Token: 0x04000586 RID: 1414
		public const string IDX10241 = "IDX10241: Security token validated. token: '{0}'.";

		// Token: 0x04000587 RID: 1415
		public const string IDX10242 = "IDX10242: Security token: '{0}' has a valid signature.";

		// Token: 0x04000588 RID: 1416
		public const string IDX10243 = "IDX10243: Reading issuer signing keys from validation parameters.";

		// Token: 0x04000589 RID: 1417
		public const string IDX10244 = "IDX10244: Issuer is null or empty. Using runtime default for creating claims '{0}'.";

		// Token: 0x0400058A RID: 1418
		public const string IDX10245 = "IDX10245: Creating claims identity from the validated token: '{0}'.";

		// Token: 0x0400058B RID: 1419
		public const string IDX10246 = "IDX10246: ValidateTokenReplay property on ValidationParameters is set to false. Exiting without validating the token replay.";

		// Token: 0x0400058C RID: 1420
		public const string IDX10248 = "IDX10248: X509SecurityKey validation failed. The associated certificate is not yet valid. ValidFrom (UTC): '{0}', Current time (UTC): '{1}'.";

		// Token: 0x0400058D RID: 1421
		public const string IDX10249 = "IDX10249: X509SecurityKey validation failed. The associated certificate has expired. ValidTo (UTC): '{0}', Current time (UTC): '{1}'.";

		// Token: 0x0400058E RID: 1422
		public const string IDX10250 = "IDX10250: The associated certificate is valid. ValidFrom (UTC): '{0}', Current time (UTC): '{1}'.";

		// Token: 0x0400058F RID: 1423
		public const string IDX10251 = "IDX10251: The associated certificate is valid. ValidTo (UTC): '{0}', Current time (UTC): '{1}'.";

		// Token: 0x04000590 RID: 1424
		public const string IDX10252 = "IDX10252: RequireSignedTokens property on ValidationParameters is set to false and the issuer signing key is null. Exiting without validating the issuer signing key.";

		// Token: 0x04000591 RID: 1425
		public const string IDX10253 = "IDX10253: RequireSignedTokens property on ValidationParameters is set to true, but the issuer signing key is null.";

		// Token: 0x04000592 RID: 1426
		public const string IDX10254 = "IDX10254: '{0}.{1}' failed. The virtual method '{2}.{3}' returned null. If this method was overridden, ensure a valid '{4}' is returned.";

		// Token: 0x04000593 RID: 1427
		public const string IDX10255 = "IDX10255: TypeValidator property on ValidationParameters is null and ValidTypes is either null or empty. Exiting without validating the token type.";

		// Token: 0x04000594 RID: 1428
		public const string IDX10256 = "IDX10256: Unable to validate the token type. TokenValidationParameters.ValidTypes is set, but the 'typ' header claim is null or empty.";

		// Token: 0x04000595 RID: 1429
		public const string IDX10257 = "IDX10257: Token type validation failed. Type: '{0}'. Did not match: validationParameters.TokenTypes: '{1}'.";

		// Token: 0x04000596 RID: 1430
		public const string IDX10258 = "IDX10258: Token type validated. Type: '{0}'.";

		// Token: 0x04000597 RID: 1431
		public const string IDX10261 = "IDX10261: Unable to retrieve configuration from authority: '{0}'. \nProceeding with token validation in case the relevant properties have been set manually on the TokenValidationParameters. Exception caught: \n {1}. See https://aka.ms/validate-using-configuration-manager for additional information.";

		// Token: 0x04000598 RID: 1432
		public const string IDX10262 = "IDX10262: One of the issuers in TokenValidationParameters.ValidIssuers was null or an empty string. See https://aka.ms/wilson/tokenvalidation for details.";

		// Token: 0x04000599 RID: 1433
		public const string IDX10264 = "IDX10264: Reading issuer signing keys from validation parameters and configuration.";

		// Token: 0x0400059A RID: 1434
		public const string IDX10265 = "IDX10265: Reading issuer signing keys from configuration.";

		// Token: 0x0400059B RID: 1435
		public const string IDX10500 = "IDX10500: Signature validation failed. No security keys were provided to validate the signature.";

		// Token: 0x0400059C RID: 1436
		public const string IDX10503 = "IDX10503: Signature validation failed. Token does not have a kid. Keys tried: '{0}'. Number of keys in TokenValidationParameters: '{1}'. \nNumber of keys in Configuration: '{2}'. \nExceptions caught:\n '{3}'.\ntoken: '{4}'. See https://aka.ms/IDX10503 for details.";

		// Token: 0x0400059D RID: 1437
		public const string IDX10504 = "IDX10504: Unable to validate signature, token does not have a signature: '{0}'.";

		// Token: 0x0400059E RID: 1438
		public const string IDX10505 = "IDX10505: Signature validation failed. The user defined 'Delegate' specified on TokenValidationParameters returned null when validating token: '{0}'.";

		// Token: 0x0400059F RID: 1439
		public const string IDX10506 = "IDX10506: Signature validation failed. The user defined 'Delegate' specified on TokenValidationParameters did not return a '{0}', but returned a '{1}' when validating token: '{2}'.";

		// Token: 0x040005A0 RID: 1440
		public const string IDX10508 = "IDX10508: Signature validation failed. Signature is improperly formatted.";

		// Token: 0x040005A1 RID: 1441
		public const string IDX10509 = "IDX10509: Token validation failed. The user defined 'Delegate' set on TokenValidationParameters.TokenReader did not return a '{0}', but returned a '{1}' when reading token: '{2}'.";

		// Token: 0x040005A2 RID: 1442
		public const string IDX10510 = "IDX10510: Token validation failed. The user defined 'Delegate' set on TokenValidationParameters.TokenReader returned null when reading token: '{0}'.";

		// Token: 0x040005A3 RID: 1443
		public const string IDX10511 = "IDX10511: Signature validation failed. Keys tried: '{0}'. \nNumber of keys in TokenValidationParameters: '{1}'. \nNumber of keys in Configuration: '{2}'. \nMatched key was in '{3}'. \nkid: '{4}'. \nExceptions caught:\n '{5}'.\ntoken: '{6}'. See https://aka.ms/IDX10511 for details.";

		// Token: 0x040005A4 RID: 1444
		public const string IDX10512 = "IDX10512: Signature validation failed. Token does not have KeyInfo. Keys tried: '{0}'.\nExceptions caught:\n '{1}'.\ntoken: '{2}'.";

		// Token: 0x040005A5 RID: 1445
		public const string IDX10514 = "IDX10514: Signature validation failed. Keys tried: '{0}'. \nKeyInfo: '{1}'. \nExceptions caught:\n '{2}'.\ntoken: '{3}'.";

		// Token: 0x040005A6 RID: 1446
		public const string IDX10603 = "IDX10603: Decryption failed. Keys tried: '{0}'.\nExceptions caught:\n '{1}'.\ntoken: '{2}'";

		// Token: 0x040005A7 RID: 1447
		public const string IDX10607 = "IDX10607: Decryption skipping key: '{0}', both validationParameters.CryptoProviderFactory and key.CryptoProviderFactory are null.";

		// Token: 0x040005A8 RID: 1448
		public const string IDX10609 = "IDX10609: Decryption failed. No Keys tried: token: '{0}'.";

		// Token: 0x040005A9 RID: 1449
		public const string IDX10610 = "IDX10610: Decryption failed. Could not create decryption provider. Key: '{0}', Algorithm: '{1}'.";

		// Token: 0x040005AA RID: 1450
		public const string IDX10611 = "IDX10611: Decryption failed. Encryption is not supported for: Algorithm: '{0}', SecurityKey: '{1}'.";

		// Token: 0x040005AB RID: 1451
		public const string IDX10612 = "IDX10612: Decryption failed. Header.Enc is null or empty, it must be specified.";

		// Token: 0x040005AC RID: 1452
		public const string IDX10615 = "IDX10615: Encryption failed. No support for: Algorithm: '{0}', SecurityKey: '{1}'.";

		// Token: 0x040005AD RID: 1453
		public const string IDX10616 = "IDX10616: Encryption failed. EncryptionProvider failed for: Algorithm: '{0}', SecurityKey: '{1}'. See inner exception.";

		// Token: 0x040005AE RID: 1454
		public const string IDX10617 = "IDX10617: Encryption failed. Keywrap is only supported for: '{0}', '{1}' and '{2}'. The content encryption specified is: '{3}'.";

		// Token: 0x040005AF RID: 1455
		public const string IDX10618 = "IDX10618: Key unwrap failed using decryption Keys: '{0}'.\nExceptions caught:\n '{1}'.\ntoken: '{2}'.";

		// Token: 0x040005B0 RID: 1456
		public const string IDX10619 = "IDX10619: Decryption failed. Algorithm: '{0}'. Either the Encryption Algorithm: '{1}' or none of the Security Keys are supported by the CryptoProviderFactory.";

		// Token: 0x040005B1 RID: 1457
		public const string IDX10620 = "IDX10620: Unable to obtain a CryptoProviderFactory, both EncryptingCredentials.CryptoProviderFactory and EncryptingCredentials.Key.CrypoProviderFactory are null.";

		// Token: 0x040005B2 RID: 1458
		public const string IDX10904 = "IDX10904: Token decryption key : '{0}' found in TokenValidationParameters.";

		// Token: 0x040005B3 RID: 1459
		public const string IDX10905 = "IDX10905: Token decryption key : '{0}' found in Configuration/Metadata.";

		// Token: 0x040005B4 RID: 1460
		public const string IDX10400 = "IDX10400: Unable to decode: '{0}' as Base64url encoded string.";

		// Token: 0x040005B5 RID: 1461
		public const string IDX10401 = "IDX10401: Invalid requested key size. Valid key sizes are: 256, 384, and 512.";

		// Token: 0x040005B6 RID: 1462
		public const string IDX10621 = "IDX10621: '{0}' supports: '{1}' of types: '{2}' or '{3}'. SecurityKey received was of type '{4}'.";

		// Token: 0x040005B7 RID: 1463
		public const string IDX10628 = "IDX10628: Cannot set the MinimumSymmetricKeySizeInBits to less than '{0}'.";

		// Token: 0x040005B8 RID: 1464
		public const string IDX10630 = "IDX10630: The '{0}' for signing cannot be smaller than '{1}' bits. KeySize: '{2}'.";

		// Token: 0x040005B9 RID: 1465
		public const string IDX10631 = "IDX10631: The '{0}' for verifying cannot be smaller than '{1}' bits. KeySize: '{2}'.";

		// Token: 0x040005BA RID: 1466
		public const string IDX10634 = "IDX10634: Unable to create the SignatureProvider.\nAlgorithm: '{0}', SecurityKey: '{1}'\n is not supported. The list of supported algorithms is available here: https://aka.ms/IdentityModel/supported-algorithms";

		// Token: 0x040005BB RID: 1467
		public const string IDX10636 = "IDX10636: CryptoProviderFactory.CreateForVerifying returned null for key: '{0}', signatureAlgorithm: '{1}'.";

		// Token: 0x040005BC RID: 1468
		public const string IDX10637 = "IDX10637: CryptoProviderFactory.CreateForSigning returned null for key: '{0}', signatureAlgorithm: '{1}'.";

		// Token: 0x040005BD RID: 1469
		public const string IDX10638 = "IDX10638: Cannot create the SignatureProvider, 'key.HasPrivateKey' is false, cannot create signatures. Key: {0}.";

		// Token: 0x040005BE RID: 1470
		public const string IDX10640 = "IDX10640: Algorithm is not supported: '{0}'.";

		// Token: 0x040005BF RID: 1471
		public const string IDX10642 = "IDX10642: Creating signature using the input: '{0}'.";

		// Token: 0x040005C0 RID: 1472
		public const string IDX10643 = "IDX10643: Comparing the signature created over the input with the token signature: '{0}'.";

		// Token: 0x040005C1 RID: 1473
		public const string IDX10645 = "IDX10645: Elliptical Curve not supported for curveId: '{0}'";

		// Token: 0x040005C2 RID: 1474
		public const string IDX10646 = "IDX10646: A CustomCryptoProvider was set and returned 'true' for IsSupportedAlgorithm(Algorithm: '{0}', Key: '{1}'), but Create.(algorithm, args) as '{2}' == NULL.";

		// Token: 0x040005C3 RID: 1475
		public const string IDX10647 = "IDX10647: A CustomCryptoProvider was set and returned 'true' for IsSupportedAlgorithm(Algorithm: '{0}'), but Create.(algorithm, args) as '{1}' == NULL.";

		// Token: 0x040005C4 RID: 1476
		public const string IDX10649 = "IDX10649: Failed to create a SymmetricSignatureProvider for the algorithm '{0}'.";

		// Token: 0x040005C5 RID: 1477
		public const string IDX10650 = "IDX10650: Failed to verify ciphertext with aad '{0}'; iv '{1}'; and authenticationTag '{2}'.";

		// Token: 0x040005C6 RID: 1478
		public const string IDX10652 = "IDX10652: The algorithm '{0}' is not supported.";

		// Token: 0x040005C7 RID: 1479
		public const string IDX10653 = "IDX10653: The encryption algorithm '{0}' requires a key size of at least '{1}' bits. Key '{2}', is of size: '{3}'.";

		// Token: 0x040005C8 RID: 1480
		public const string IDX10654 = "IDX10654: Decryption failed. Cryptographic operation exception: '{0}'.";

		// Token: 0x040005C9 RID: 1481
		public const string IDX10655 = "IDX10655: '{0}' must be greater than 1, was: '{1}'";

		// Token: 0x040005CA RID: 1482
		public const string IDX10657 = "IDX10657: The SecurityKey provided for the symmetric key wrap algorithm cannot be converted to byte array. Type is: '{0}'.";

		// Token: 0x040005CB RID: 1483
		public const string IDX10658 = "IDX10658: WrapKey failed, exception from cryptographic operation: '{0}'";

		// Token: 0x040005CC RID: 1484
		public const string IDX10659 = "IDX10659: UnwrapKey failed, exception from cryptographic operation: '{0}'";

		// Token: 0x040005CD RID: 1485
		public const string IDX10661 = "IDX10661: Unable to create the KeyWrapProvider.\nKeyWrapAlgorithm: '{0}', SecurityKey: '{1}'\n is not supported.";

		// Token: 0x040005CE RID: 1486
		public const string IDX10662 = "IDX10662: The KeyWrap algorithm '{0}' requires a key size of '{1}' bits. Key '{2}', is of size:'{3}'.";

		// Token: 0x040005CF RID: 1487
		public const string IDX10663 = "IDX10663: Failed to create symmetric algorithm with SecurityKey: '{0}', KeyWrapAlgorithm: '{1}'.";

		// Token: 0x040005D0 RID: 1488
		public const string IDX10664 = "IDX10664: The length of input must be a multiple of 64 bits. The input size is: '{0}' bits.";

		// Token: 0x040005D1 RID: 1489
		public const string IDX10665 = "IDX10665: Data is not authentic";

		// Token: 0x040005D2 RID: 1490
		public const string IDX10666 = "IDX10666: Unable to create KeyedHashAlgorithm for algorithm '{0}'.";

		// Token: 0x040005D3 RID: 1491
		public const string IDX10667 = "IDX10667: Unable to obtain required byte array for KeyHashAlgorithm from SecurityKey: '{0}'.";

		// Token: 0x040005D4 RID: 1492
		public const string IDX10668 = "IDX10668: Unable to create '{0}', algorithm '{1}'; key: '{2}' is not supported.";

		// Token: 0x040005D5 RID: 1493
		public const string IDX10669 = "IDX10669: Failed to create symmetric algorithm.";

		// Token: 0x040005D6 RID: 1494
		public const string IDX10674 = "IDX10674: JsonWebKeyConverter does not support SecurityKey of type: {0}";

		// Token: 0x040005D7 RID: 1495
		public const string IDX10675 = "IDX10675: Cannot create a ECDsa object from the '{0}', the bytes from the decoded value of '{1}' must be less than the size associated with the curve: '{2}'. Size was: '{3}'.";

		// Token: 0x040005D8 RID: 1496
		public const string IDX10679 = "IDX10679: Failed to decompress using algorithm '{0}'.";

		// Token: 0x040005D9 RID: 1497
		public const string IDX10680 = "IDX10680: Failed to compress using algorithm '{0}'.";

		// Token: 0x040005DA RID: 1498
		public const string IDX10682 = "IDX10682: Compression algorithm '{0}' is not supported.";

		// Token: 0x040005DB RID: 1499
		public const string IDX10684 = "IDX10684: Unable to convert the JsonWebKey to an AsymmetricSecurityKey. Algorithm: '{0}', Key: '{1}'.";

		// Token: 0x040005DC RID: 1500
		public const string IDX10685 = "IDX10685: Unable to Sign, Internal SignFunction is not available.";

		// Token: 0x040005DD RID: 1501
		public const string IDX10686 = "IDX10686: Unable to Verify, Internal VerifyFunction is not available.";

		// Token: 0x040005DE RID: 1502
		public const string IDX10687 = "IDX10687: Unable to create a AsymmetricAdapter. For NET45 only types: '{0}' or '{1}' are supported. RSA is of type: '{2}'..";

		// Token: 0x040005DF RID: 1503
		public const string IDX10689 = "IDX10689: Unable to create an ECDsa object. See inner exception for more details.";

		// Token: 0x040005E0 RID: 1504
		public const string IDX10690 = "IDX10690: ECDsa creation is not supported by the current platform. For more details, see https://aka.ms/IdentityModel/create-ecdsa";

		// Token: 0x040005E1 RID: 1505
		public const string IDX10692 = "IDX10692: The RSASS-PSS signature algorithm is not available on the .NET 4.5 target. The list of supported algorithms is available here: https://aka.ms/IdentityModel/supported-algorithms";

		// Token: 0x040005E2 RID: 1506
		public const string IDX10693 = "IDX10693: RSACryptoServiceProvider doesn't support the RSASSA-PSS signature algorithm. The list of supported algorithms is available here: https://aka.ms/IdentityModel/supported-algorithms";

		// Token: 0x040005E3 RID: 1507
		public const string IDX10694 = "IDX10694: JsonWebKeyConverter threw attempting to convert JsonWebKey: '{0}'. Exception: '{1}'.";

		// Token: 0x040005E4 RID: 1508
		public const string IDX10695 = "IDX10695: Unable to create a JsonWebKey from an ECDsa object. Required ECParameters structure is not supported by .NET Framework < 4.7.";

		// Token: 0x040005E5 RID: 1509
		public const string IDX10696 = "IDX10696: The algorithm '{0}' is not in the user-defined accepted list of algorithms.";

		// Token: 0x040005E6 RID: 1510
		public const string IDX10697 = "IDX10697: The user defined 'Delegate' AlgorithmValidator specified on TokenValidationParameters returned false when validating Algorithm: '{0}', SecurityKey: '{1}'.";

		// Token: 0x040005E7 RID: 1511
		public const string IDX10698 = "IDX10698: The SignatureProviderObjectPoolCacheSize must be greater than 0. Value: '{0}'.";

		// Token: 0x040005E8 RID: 1512
		public const string IDX10699 = "IDX10699: Unable to remove SignatureProvider with cache key: {0} from the InMemoryCryptoProviderCache. Exception: '{1}'.";

		// Token: 0x040005E9 RID: 1513
		public const string IDX10700 = "IDX10700: {0} is unable to use 'rsaParameters'. {1} is null.";

		// Token: 0x040005EA RID: 1514
		public const string IDX10703 = "IDX10703: Cannot create a '{0}', key length is zero.";

		// Token: 0x040005EB RID: 1515
		public const string IDX10704 = "IDX10704: Cannot verify the key size. The SecurityKey is not or cannot be converted to an AsymmetricSecuritKey. SecurityKey: '{0}'.";

		// Token: 0x040005EC RID: 1516
		public const string IDX10705 = "IDX10705: Cannot create a JWK thumbprint, '{0}' is null or empty.";

		// Token: 0x040005ED RID: 1517
		public const string IDX10706 = "IDX10706: Cannot create a JWK thumbprint, '{0}' must be one of the following: '{1}'.";

		// Token: 0x040005EE RID: 1518
		public const string IDX10707 = "IDX10707: Cannot create a JSON representation of an asymmetric public key, '{0}' must be one of the following: '{1}'.";

		// Token: 0x040005EF RID: 1519
		public const string IDX10708 = "IDX10708: Cannot create a JSON representation of an EC public key, '{0}' is null or empty.";

		// Token: 0x040005F0 RID: 1520
		public const string IDX10709 = "IDX10709: Cannot create a JSON representation of an RSA public key, '{0}' is null or empty.";

		// Token: 0x040005F1 RID: 1521
		public const string IDX10710 = "IDX10710: Computing a JWK thumbprint is supported only on SymmetricSecurityKey, JsonWebKey, RsaSecurityKey, X509SecurityKey, and ECDsaSecurityKey.";

		// Token: 0x040005F2 RID: 1522
		public const string IDX10711 = "IDX10711: Unable to Decrypt, Internal DecryptionFunction is not available.";

		// Token: 0x040005F3 RID: 1523
		public const string IDX10712 = "IDX10712: Unable to Encrypt, Internal EncryptionFunction is not available.";

		// Token: 0x040005F4 RID: 1524
		public const string IDX10713 = "IDX10713: Encrytion/Decryption using algorithm '{0}' is only supported on Windows platform.";

		// Token: 0x040005F5 RID: 1525
		public const string IDX10714 = "IDX10714: Unable to perform the decryption. There is a authentication tag mismatch.";

		// Token: 0x040005F6 RID: 1526
		public const string IDX10715 = "IDX10715: Encryption using algorithm: '{0}' is not supported.";

		// Token: 0x040005F7 RID: 1527
		public const string IDX10716 = "IDX10716: '{0}' must be greater than 0, was: '{1}'";

		// Token: 0x040005F8 RID: 1528
		public const string IDX10717 = "IDX10717: '{0} + {1}' must not be greater than {2}, '{3} + {4} > {5}'.";

		// Token: 0x040005F9 RID: 1529
		public const string IDX10718 = "IDX10718: AlgorithmToValidate is not supported: '{0}'. Algorithm '{1}'.";

		// Token: 0x040005FA RID: 1530
		public const string IDX10719 = "IDX10719: SignatureSize (in bytes) was expected to be '{0}', was '{1}'.";

		// Token: 0x040005FB RID: 1531
		public const string IDX10720 = "IDX10720: Unable to create KeyedHashAlgorithm for algorithm '{0}', the key size must be greater than: '{1}' bits, key has '{2}' bits. See https://aka.ms/IdentityModel/UnsafeRelaxHmacKeySizeValidation";

		// Token: 0x040005FC RID: 1532
		public const string IDX10805 = "IDX10805: Error deserializing json: '{0}' into '{1}'.";

		// Token: 0x040005FD RID: 1533
		public const string IDX10806 = "IDX10806: Deserializing json: '{0}' into '{1}'.";

		// Token: 0x040005FE RID: 1534
		public const string IDX10808 = "IDX10808: The 'use' parameter of a JsonWebKey: '{0}' was expected to be 'sig' or empty, but was '{1}'.";

		// Token: 0x040005FF RID: 1535
		public const string IDX10810 = "IDX10810: Unable to convert the JsonWebKey: '{0}' to a X509SecurityKey, RsaSecurityKey or ECDSASecurityKey.";

		// Token: 0x04000600 RID: 1536
		public const string IDX10812 = "IDX10812: Unable to create a {0} from the properties found in the JsonWebKey: '{1}'.";

		// Token: 0x04000601 RID: 1537
		public const string IDX10813 = "IDX10813: Unable to create a {0} from the properties found in the JsonWebKey: '{1}', Exception '{2}'.";

		// Token: 0x04000602 RID: 1538
		public const string IDX10814 = "IDX10814: Unable to create a {0} from the properties found in the JsonWebKey: '{1}'. Missing: '{2}'.";

		// Token: 0x04000603 RID: 1539
		public const string IDX10816 = "IDX10816: Decompressing would result in a token with a size greater than allowed. Maximum size allowed: '{0}'.";

		// Token: 0x04000604 RID: 1540
		public const string IDX10820 = "IDX10820: Invalid character found in Base64UrlEncoding. Character: '{0}', Encoding: '{1}'.";

		// Token: 0x04000605 RID: 1541
		public const string IDX10821 = "IDX10821: Incorrect padding detected in Base64UrlEncoding. Encoding: '{0}'.";

		// Token: 0x04000606 RID: 1542
		public const string IDX10900 = "IDX10900: EventBasedLRUCache._eventQueue encountered an error while processing a cache operation. Exception '{0}'.";

		// Token: 0x04000607 RID: 1543
		public const string IDX10901 = "IDX10901: CryptoProviderCacheOptions.SizeLimit must be greater than 10. Value: '{0}'";

		// Token: 0x04000608 RID: 1544
		public const string IDX10902 = "IDX10902: Object disposed exception in '{0}': '{1}'";

		// Token: 0x04000609 RID: 1545
		public const string IDX11000 = "IDX11000: Cannot create EcdhKeyExchangeProvider. '{0}''s Curve '{1}' does not match with '{2}''s curve '{3}'.";

		// Token: 0x0400060A RID: 1546
		public const string IDX11001 = "IDX11001: Cannot generate KDF. '{0}':'{1}' and '{2}':'{3}' must be different.";

		// Token: 0x0400060B RID: 1547
		public const string IDX11002 = "IDX11002: Cannot create the EcdhKeyExchangeProvider. Unable to obtain ECParameters from {0}. Verify the SecurityKey is an ECDsaSecurityKey or JsonWebKey and that properties Crv, X, Y, and D (if used for a private key) are contained in the provided SecurityKey.";
	}
}
