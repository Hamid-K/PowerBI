using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200007B RID: 123
	internal sealed class SystemProperties : PropertyCollection
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x00014A8E File Offset: 0x00012C8E
		internal SystemProperties()
		{
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00015260 File Offset: 0x00013460
		internal SystemProperties(Property[] properties)
			: base(properties)
		{
			for (int i = 0; i < base.Count; i++)
			{
				string name = base.GetName(i);
				if (base.GetValue(i) == null && SystemPropertyNames.SystemNonNullableProperties.Contains(name))
				{
					throw new MissingElementException(name);
				}
			}
			string siteName = this.SiteName;
			if (siteName != null && siteName.Length > 100)
			{
				throw new InvalidElementException("SiteName");
			}
			base.ValidateReportTimeoutIfPresent("SystemReportTimeout", ItemType.Unknown);
			base.ValidateBooleanProperty("EnableExecutionLogging", this.EnableExecutionLogging, false);
			base.ValidateIntegerProperty("ExecutionLogDaysKept", this.ExecutionLogDaysKept, false);
			base.ValidateBooleanProperty("UseSessionCookies", this.UseSessionCookies, false);
			base.ValidateIntegerProperty("SessionTimeout", this.SessionTimeout, false);
			base.ValidateIntegerProperty("RDLXReportTimeout", this.RdlxReportTimeout, false);
			base.ValidateIntegerProperty("SessionAccessTimeout", this.SessionAccessTimeout, false);
			base.ValidateIntegerProperty("ResponseBufferSizeKb", this.ResponseBufferSizeKb, false);
			base.ValidateIntegerProperty("SqlStreamingBufferSize", this.SqlStreamingBufferSize, false);
			base.ValidateIntegerProperty("EditSessionTimeout", this.EditSessionTimeout, false);
			base.ValidateIntegerProperty("EditSessionCacheLimit", this.EditSessionCacheLimit, false);
			base.ValidateIntegerProperty("MaxFileSizeMb", this.MaxFileSizeMb, false);
			base.ValidateBooleanProperty("EnableClientPrinting", this.EnableClientPrinting, false);
			base.ValidateBooleanProperty("EnableTestConnectionDetailedErrors", this.EnableTestConnectionDetailedErrors, false);
			base.ValidateEnumProperty<ExecutionLogLevel>("ExecutionLogLevel", this.ExecutionLogLevel);
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000153D0 File Offset: 0x000135D0
		internal string EnableMyReports
		{
			get
			{
				return base["EnableMyReports"];
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x000153DD File Offset: 0x000135DD
		internal string MyReportsRole
		{
			get
			{
				return base["MyReportsRole"];
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x000153EA File Offset: 0x000135EA
		internal string SystemSnapshotLimit
		{
			get
			{
				return base["SystemSnapshotLimit"];
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x000153F7 File Offset: 0x000135F7
		internal string EnableClientPrinting
		{
			get
			{
				return base["EnableClientPrinting"];
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00015404 File Offset: 0x00013604
		internal string SystemReportTimeout
		{
			get
			{
				return base["SystemReportTimeout"];
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00015411 File Offset: 0x00013611
		internal string SiteName
		{
			get
			{
				return base["SiteName"];
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0001541E File Offset: 0x0001361E
		internal string EnableExecutionLogging
		{
			get
			{
				return base["EnableExecutionLogging"];
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001542B File Offset: 0x0001362B
		internal string ExecutionLogDaysKept
		{
			get
			{
				return base["ExecutionLogDaysKept"];
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00015438 File Offset: 0x00013638
		internal string ExecutionLogLevel
		{
			get
			{
				return base["ExecutionLogLevel"];
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00015445 File Offset: 0x00013645
		internal string UseSessionCookies
		{
			get
			{
				return base["UseSessionCookies"];
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00015452 File Offset: 0x00013652
		internal string SessionTimeout
		{
			get
			{
				return base["SessionTimeout"];
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0001545F File Offset: 0x0001365F
		internal string RdlxReportTimeout
		{
			get
			{
				return base["RDLXReportTimeout"];
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0001546C File Offset: 0x0001366C
		internal string SessionAccessTimeout
		{
			get
			{
				return base["SessionAccessTimeout"];
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00015479 File Offset: 0x00013679
		internal string ResponseBufferSizeKb
		{
			get
			{
				return base["ResponseBufferSizeKb"];
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00015486 File Offset: 0x00013686
		internal string SqlStreamingBufferSize
		{
			get
			{
				return base["SqlStreamingBufferSize"];
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00015493 File Offset: 0x00013693
		internal string SharePointIntegrated
		{
			get
			{
				return base["SharePointIntegrated"];
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x000154A0 File Offset: 0x000136A0
		internal string EnableTestConnectionDetailedErrors
		{
			get
			{
				return base["EnableTestConnectionDetailedErrors"];
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x000154AD File Offset: 0x000136AD
		internal string EditSessionTimeout
		{
			get
			{
				return base["EditSessionTimeout"];
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x000154BA File Offset: 0x000136BA
		internal string EditSessionCacheLimit
		{
			get
			{
				return base["EditSessionCacheLimit"];
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x000154C7 File Offset: 0x000136C7
		internal string MaxFileSizeMb
		{
			get
			{
				return base["MaxFileSizeMb"];
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x000154D4 File Offset: 0x000136D4
		internal string ClientId
		{
			get
			{
				return base["ClientId"];
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x000154E1 File Offset: 0x000136E1
		internal string ClientSecret
		{
			get
			{
				return base["ClientSecret"];
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x000154EE File Offset: 0x000136EE
		internal string AppObjectId
		{
			get
			{
				return base["AppObjectId"];
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x000154FB File Offset: 0x000136FB
		internal string TenantName
		{
			get
			{
				return base["TenantName"];
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00015508 File Offset: 0x00013708
		internal string TenantId
		{
			get
			{
				return base["TenantId"];
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00015515 File Offset: 0x00013715
		internal string ResourceUrl
		{
			get
			{
				return base["ResourceUrl"];
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00015522 File Offset: 0x00013722
		internal string AuthorizationUrl
		{
			get
			{
				return base["AuthorizationUrl"];
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0001552F File Offset: 0x0001372F
		internal string RedirectUrls
		{
			get
			{
				return base["RedirectUrls"];
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0001553C File Offset: 0x0001373C
		internal string PowerBIEndpoint
		{
			get
			{
				return base["PowerBIEndpoint"];
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00015549 File Offset: 0x00013749
		internal string LogoutUrl
		{
			get
			{
				return base["LogoutUrl"];
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00015556 File Offset: 0x00013756
		internal string TokenUrl
		{
			get
			{
				return base["TokenUrl"];
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00015563 File Offset: 0x00013763
		internal string OAuthClientId
		{
			get
			{
				return base["OAuthClientId"];
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00015570 File Offset: 0x00013770
		internal string OAuthClientSecret
		{
			get
			{
				return base["OAuthClientSecret"];
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001557D File Offset: 0x0001377D
		internal string OAuthTenant
		{
			get
			{
				return base["OAuthTenant"];
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0001558A File Offset: 0x0001378A
		internal string OAuthTokenUrl
		{
			get
			{
				return base["OAuthTokenUrl"];
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00015597 File Offset: 0x00013797
		internal string OAuthAuthorizationUrl
		{
			get
			{
				return base["OAuthAuthorizationUrl"];
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x000155A4 File Offset: 0x000137A4
		internal string OAuthFederationMetadataUrl
		{
			get
			{
				return base["OAuthFederationMetadataUrl"];
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x000155B1 File Offset: 0x000137B1
		internal string OAuthResourceUrl
		{
			get
			{
				return base["OAuthResourceUrl"];
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x000155BE File Offset: 0x000137BE
		internal string OAuthNativeClientId
		{
			get
			{
				return base["OAuthNativeClientId"];
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x000155CB File Offset: 0x000137CB
		internal string OAuthGraphUrl
		{
			get
			{
				return base["OAuthGraphUrl"];
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x000155D8 File Offset: 0x000137D8
		internal string OAuthSessionCookieName
		{
			get
			{
				return base["OAuthSessionCookieName"];
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x000155E5 File Offset: 0x000137E5
		internal string OAuthLogoutUrl
		{
			get
			{
				return base["OAuthLogoutUrl"];
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x000155F2 File Offset: 0x000137F2
		internal string AccessControlAllowCredentials
		{
			get
			{
				return base["AccessControlAllowCredentials"];
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x000155FF File Offset: 0x000137FF
		internal string AccessControlAllowHeaders
		{
			get
			{
				return base["AccessControlAllowHeaders"];
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0001560C File Offset: 0x0001380C
		internal string AccessControlAllowMethods
		{
			get
			{
				return base["AccessControlAllowMethods"];
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00015619 File Offset: 0x00013819
		internal string AccessControlAllowOrigin
		{
			get
			{
				return base["AccessControlAllowOrigin"];
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00015626 File Offset: 0x00013826
		internal string AccessControlExposeHeaders
		{
			get
			{
				return base["AccessControlExposeHeaders"];
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00015633 File Offset: 0x00013833
		internal string AccessControlMaxAge
		{
			get
			{
				return base["AccessControlMaxAge"];
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00015640 File Offset: 0x00013840
		internal string CustomHeaders
		{
			get
			{
				return base["CustomHeaders"];
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001564D File Offset: 0x0001384D
		internal string CleanupBatchSize
		{
			get
			{
				return base["CleanupBatchSize"];
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001565A File Offset: 0x0001385A
		internal string CleanupMaxLimit
		{
			get
			{
				return base["CleanupMaxLimit"];
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00015667 File Offset: 0x00013867
		internal static bool IsNonPermissionedProperty(string propertyName)
		{
			return SystemPropertyNames.SystemNonPermissionedProperties.ContainsKey(propertyName);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00015674 File Offset: 0x00013874
		internal static bool AreAllNonPermissionedProperties(Property[] requestedProperties)
		{
			bool flag = true;
			if (requestedProperties != null)
			{
				flag = false;
				int i;
				for (i = 0; i < requestedProperties.Length; i++)
				{
					Property property = requestedProperties[i];
					if (property == null || property.Name == null || !SystemProperties.IsNonPermissionedProperty(property.Name))
					{
						break;
					}
				}
				if (i == requestedProperties.Length)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000156C0 File Offset: 0x000138C0
		internal Property[] GetNonPermissionedProperties()
		{
			Property[] array = new Property[SystemPropertyNames.SystemNonPermissionedPropertyNames.Length];
			for (int i = 0; i < SystemPropertyNames.SystemNonPermissionedPropertyNames.Length; i++)
			{
				Property property = new Property();
				property.Name = SystemPropertyNames.SystemNonPermissionedPropertyNames[i];
				property.Value = base.GetValue(property.Name);
				array[i] = property;
			}
			return array;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00015716 File Offset: 0x00013916
		protected override bool IsReadOnlyProperty(string propertyName)
		{
			return SystemPropertyNames.SystemReadOnlyProperties.Contains(propertyName);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00015724 File Offset: 0x00013924
		internal static Property[] FilterSystemPropertiesOnMode(Property[] props, bool isSharePointMode)
		{
			if (props == null)
			{
				return null;
			}
			List<Property> list = new List<Property>();
			foreach (Property property in props)
			{
				string name = property.Name;
				if (isSharePointMode)
				{
					if (!SystemProperties.IsNotSupportedSharePointMode(name))
					{
						list.Add(property);
					}
				}
				else if (!SystemProperties.IsNotSupportedNativeMode(name))
				{
					list.Add(property);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00015784 File Offset: 0x00013984
		internal static void ValidatePropertiesSupportedInMode(Property[] properties, bool isSharePointMode)
		{
			foreach (Property property in properties)
			{
				if (property != null)
				{
					string name = property.Name;
					if (name != null)
					{
						if (isSharePointMode)
						{
							if (SystemProperties.IsNotSupportedSharePointMode(name))
							{
								throw new SharePointPropertyDisabledException();
							}
						}
						else if (SystemProperties.IsNotSupportedNativeMode(name))
						{
							throw new NativeModePropertyDisabledException();
						}
					}
				}
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000157D1 File Offset: 0x000139D1
		private static bool IsNotSupportedNativeMode(string propertyName)
		{
			return SystemPropertyNames.SystemNativeModeNotSupportedProperties.Contains(propertyName);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000157DE File Offset: 0x000139DE
		private static bool IsNotSupportedSharePointMode(string propertyName)
		{
			return SystemPropertyNames.SystemSharePointModeNotSupportedProperties.Contains(propertyName);
		}
	}
}
