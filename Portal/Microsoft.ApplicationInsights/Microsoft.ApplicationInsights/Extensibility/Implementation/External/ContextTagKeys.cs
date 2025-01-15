using System;
using System.CodeDom.Compiler;
using System.Threading;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000B2 RID: 178
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class ContextTagKeys
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0001605B File Offset: 0x0001425B
		internal static ContextTagKeys Keys
		{
			get
			{
				return LazyInitializer.EnsureInitialized<ContextTagKeys>(ref ContextTagKeys.keys);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00016067 File Offset: 0x00014267
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x0001606F File Offset: 0x0001426F
		public string ApplicationVersion { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00016078 File Offset: 0x00014278
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00016080 File Offset: 0x00014280
		public string DeviceId { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00016089 File Offset: 0x00014289
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x00016091 File Offset: 0x00014291
		public string DeviceLocale { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0001609A File Offset: 0x0001429A
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x000160A2 File Offset: 0x000142A2
		public string DeviceModel { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x000160AB File Offset: 0x000142AB
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x000160B3 File Offset: 0x000142B3
		public string DeviceOEMName { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x000160BC File Offset: 0x000142BC
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x000160C4 File Offset: 0x000142C4
		public string DeviceOSVersion { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x000160CD File Offset: 0x000142CD
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x000160D5 File Offset: 0x000142D5
		public string DeviceType { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x000160DE File Offset: 0x000142DE
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x000160E6 File Offset: 0x000142E6
		public string LocationIp { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000160EF File Offset: 0x000142EF
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x000160F7 File Offset: 0x000142F7
		public string LocationCountry { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00016100 File Offset: 0x00014300
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00016108 File Offset: 0x00014308
		public string LocationProvince { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00016111 File Offset: 0x00014311
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x00016119 File Offset: 0x00014319
		public string LocationCity { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00016122 File Offset: 0x00014322
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x0001612A File Offset: 0x0001432A
		public string OperationId { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00016133 File Offset: 0x00014333
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x0001613B File Offset: 0x0001433B
		public string OperationName { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00016144 File Offset: 0x00014344
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x0001614C File Offset: 0x0001434C
		public string OperationParentId { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00016155 File Offset: 0x00014355
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0001615D File Offset: 0x0001435D
		public string OperationSyntheticSource { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00016166 File Offset: 0x00014366
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x0001616E File Offset: 0x0001436E
		public string OperationCorrelationVector { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00016177 File Offset: 0x00014377
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x0001617F File Offset: 0x0001437F
		public string SessionId { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00016188 File Offset: 0x00014388
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x00016190 File Offset: 0x00014390
		public string SessionIsFirst { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00016199 File Offset: 0x00014399
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x000161A1 File Offset: 0x000143A1
		public string UserAccountId { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x000161AA File Offset: 0x000143AA
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x000161B2 File Offset: 0x000143B2
		public string UserId { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x000161BB File Offset: 0x000143BB
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x000161C3 File Offset: 0x000143C3
		public string UserAuthUserId { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x000161CC File Offset: 0x000143CC
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x000161D4 File Offset: 0x000143D4
		public string CloudRole { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x000161DD File Offset: 0x000143DD
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x000161E5 File Offset: 0x000143E5
		public string CloudRoleInstance { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x000161EE File Offset: 0x000143EE
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x000161F6 File Offset: 0x000143F6
		public string InternalSdkVersion { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x000161FF File Offset: 0x000143FF
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x00016207 File Offset: 0x00014407
		public string InternalAgentVersion { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00016210 File Offset: 0x00014410
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x00016218 File Offset: 0x00014418
		public string InternalNodeName { get; set; }

		// Token: 0x0600059A RID: 1434 RVA: 0x00016221 File Offset: 0x00014421
		public ContextTagKeys()
			: this("AI.ContextTagKeys", "ContextTagKeys")
		{
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00016234 File Offset: 0x00014434
		protected ContextTagKeys(string fullName, string name)
		{
			this.ApplicationVersion = "ai.application.ver";
			this.DeviceId = "ai.device.id";
			this.DeviceLocale = "ai.device.locale";
			this.DeviceModel = "ai.device.model";
			this.DeviceOEMName = "ai.device.oemName";
			this.DeviceOSVersion = "ai.device.osVersion";
			this.DeviceType = "ai.device.type";
			this.LocationIp = "ai.location.ip";
			this.LocationCountry = "ai.location.country";
			this.LocationProvince = "ai.location.province";
			this.LocationCity = "ai.location.city";
			this.OperationId = "ai.operation.id";
			this.OperationName = "ai.operation.name";
			this.OperationParentId = "ai.operation.parentId";
			this.OperationSyntheticSource = "ai.operation.syntheticSource";
			this.OperationCorrelationVector = "ai.operation.correlationVector";
			this.SessionId = "ai.session.id";
			this.SessionIsFirst = "ai.session.isFirst";
			this.UserAccountId = "ai.user.accountId";
			this.UserId = "ai.user.id";
			this.UserAuthUserId = "ai.user.authUserId";
			this.CloudRole = "ai.cloud.role";
			this.CloudRoleInstance = "ai.cloud.roleInstance";
			this.InternalSdkVersion = "ai.internal.sdkVersion";
			this.InternalAgentVersion = "ai.internal.agentVersion";
			this.InternalNodeName = "ai.internal.nodeName";
		}

		// Token: 0x04000226 RID: 550
		private static ContextTagKeys keys;
	}
}
