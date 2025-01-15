using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000006 RID: 6
	public sealed class AnalysisServicesEvent
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003901 File Offset: 0x00001B01
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00003909 File Offset: 0x00001B09
		public string Name { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003912 File Offset: 0x00001B12
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000391A File Offset: 0x00001B1A
		public string Use { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003923 File Offset: 0x00001B23
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000392B File Offset: 0x00001B2B
		public string Id { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003934 File Offset: 0x00001B34
		// (set) Token: 0x0600003B RID: 59 RVA: 0x0000393C File Offset: 0x00001B3C
		public DateTime Time { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003945 File Offset: 0x00001B45
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000394D File Offset: 0x00001B4D
		public Dictionary<string, string> Properties
		{
			get
			{
				return this._properties;
			}
			set
			{
				this._properties = value;
			}
		}

		// Token: 0x0400003E RID: 62
		public const string AnalysisServicesEventPrefix = "PBIRS.AS.";

		// Token: 0x0400003F RID: 63
		private Dictionary<string, string> _properties = new Dictionary<string, string>();
	}
}
