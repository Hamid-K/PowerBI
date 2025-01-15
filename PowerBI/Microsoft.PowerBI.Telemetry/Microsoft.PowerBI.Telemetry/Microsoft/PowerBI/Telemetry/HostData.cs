using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200002E RID: 46
	public class HostData : BaseTelemetryEvent
	{
		// Token: 0x06000109 RID: 265 RVA: 0x00004358 File Offset: 0x00002558
		public HostData(string deviceId, string sessionId, string client, string build, string cluster, string userId, bool isInternalUser, bool isReturningUser, string culture, string uiCulture, string authenticatedUserId, AppType appType, string tenantId)
			: base("HostData", TelemetryUse.Verbose)
		{
			this.deviceId = deviceId;
			this.sessionId = sessionId;
			this.client = client;
			this.build = build;
			this.cluster = cluster;
			this.userId = userId;
			this.isInternalUser = isInternalUser;
			this.isReturningUser = isReturningUser;
			this.culture = culture;
			this.uiCulture = uiCulture;
			this.authenticatedUserId = authenticatedUserId;
			this.appType = appType;
			this.tenantId = tenantId;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000043D6 File Offset: 0x000025D6
		// (set) Token: 0x0600010B RID: 267 RVA: 0x000043DE File Offset: 0x000025DE
		public string deviceId { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000043E7 File Offset: 0x000025E7
		// (set) Token: 0x0600010D RID: 269 RVA: 0x000043EF File Offset: 0x000025EF
		public string sessionId { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000043F8 File Offset: 0x000025F8
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00004400 File Offset: 0x00002600
		public string client { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004409 File Offset: 0x00002609
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00004411 File Offset: 0x00002611
		public string build { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000441A File Offset: 0x0000261A
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00004422 File Offset: 0x00002622
		public string cluster { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000442B File Offset: 0x0000262B
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00004433 File Offset: 0x00002633
		public string userId { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000443C File Offset: 0x0000263C
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00004444 File Offset: 0x00002644
		public bool isInternalUser { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000444D File Offset: 0x0000264D
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00004455 File Offset: 0x00002655
		public bool isReturningUser { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000445E File Offset: 0x0000265E
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00004466 File Offset: 0x00002666
		public string culture { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000446F File Offset: 0x0000266F
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00004477 File Offset: 0x00002677
		public string uiCulture { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004480 File Offset: 0x00002680
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00004488 File Offset: 0x00002688
		public string authenticatedUserId { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004491 File Offset: 0x00002691
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00004499 File Offset: 0x00002699
		public AppType appType { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000044A2 File Offset: 0x000026A2
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000044AA File Offset: 0x000026AA
		public string tenantId { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000044B4 File Offset: 0x000026B4
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string deviceId = this.deviceId;
				string text = ((deviceId == null) ? string.Empty : deviceId.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("deviceId", text);
				string sessionId = this.sessionId;
				string text2 = ((sessionId == null) ? string.Empty : sessionId.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("sessionId", text2);
				string client = this.client;
				string text3 = ((client == null) ? string.Empty : client.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("client", text3);
				string build = this.build;
				string text4 = ((build == null) ? string.Empty : build.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("build", text4);
				string cluster = this.cluster;
				string text5 = ((cluster == null) ? string.Empty : cluster.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("cluster", text5);
				string userId = this.userId;
				string text6 = ((userId == null) ? string.Empty : userId.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("userId", text6);
				bool isInternalUser = this.isInternalUser;
				string text7 = ((isInternalUser == null) ? string.Empty : isInternalUser.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("isInternalUser", text7);
				bool isReturningUser = this.isReturningUser;
				string text8 = ((isReturningUser == null) ? string.Empty : isReturningUser.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("isReturningUser", text8);
				string culture = this.culture;
				string text9 = ((culture == null) ? string.Empty : culture.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("culture", text9);
				string uiCulture = this.uiCulture;
				string text10 = ((uiCulture == null) ? string.Empty : uiCulture.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("uiCulture", text10);
				string authenticatedUserId = this.authenticatedUserId;
				string text11 = ((authenticatedUserId == null) ? string.Empty : authenticatedUserId.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("authenticatedUserId", text11);
				AppType appType = this.appType;
				string text12 = ((appType == null) ? string.Empty : appType.ToString());
				dictionary.Add("appType", text12);
				string tenantId = this.tenantId;
				string text13 = ((tenantId == null) ? string.Empty : tenantId.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("tenantId", text13);
				return dictionary;
			}
		}
	}
}
