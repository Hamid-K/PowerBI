using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BD8 RID: 3032
	public class QueueInformation
	{
		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x06005E68 RID: 24168 RVA: 0x00181131 File Offset: 0x0017F331
		// (set) Token: 0x06005E69 RID: 24169 RVA: 0x00181139 File Offset: 0x0017F339
		public string Alias { get; private set; }

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x06005E6A RID: 24170 RVA: 0x00181142 File Offset: 0x0017F342
		// (set) Token: 0x06005E6B RID: 24171 RVA: 0x0018114A File Offset: 0x0017F34A
		public string Name { get; private set; }

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x06005E6C RID: 24172 RVA: 0x00181153 File Offset: 0x0017F353
		// (set) Token: 0x06005E6D RID: 24173 RVA: 0x0018115B File Offset: 0x0017F35B
		internal string QueueManagerAlias { get; private set; }

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x06005E6E RID: 24174 RVA: 0x00181164 File Offset: 0x0017F364
		// (set) Token: 0x06005E6F RID: 24175 RVA: 0x0018116C File Offset: 0x0017F36C
		public QueueManagerInformation QueueManager { get; internal set; }

		// Token: 0x06005E70 RID: 24176 RVA: 0x00181175 File Offset: 0x0017F375
		public QueueInformation(string alias, string name, string queueManagerAlias)
		{
			this.Alias = alias;
			this.Name = name;
			this.QueueManagerAlias = queueManagerAlias;
		}

		// Token: 0x06005E71 RID: 24177 RVA: 0x00181194 File Offset: 0x0017F394
		public override string ToString()
		{
			return string.Concat(new string[] { "Queue Information: Alias '", this.Alias, "', Name '", this.Name, "', Queue Manager Alias '", this.QueueManagerAlias, "'" });
		}
	}
}
