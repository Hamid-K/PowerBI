using System;
using System.Text;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BD9 RID: 3033
	public class QueueManagerInformation
	{
		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x06005E72 RID: 24178 RVA: 0x001811E7 File Offset: 0x0017F3E7
		// (set) Token: 0x06005E73 RID: 24179 RVA: 0x001811EF File Offset: 0x0017F3EF
		public string Alias { get; private set; }

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x06005E74 RID: 24180 RVA: 0x001811F8 File Offset: 0x0017F3F8
		// (set) Token: 0x06005E75 RID: 24181 RVA: 0x00181200 File Offset: 0x0017F400
		public string Name { get; private set; }

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x06005E76 RID: 24182 RVA: 0x00181209 File Offset: 0x0017F409
		// (set) Token: 0x06005E77 RID: 24183 RVA: 0x00181211 File Offset: 0x0017F411
		public string Channel { get; private set; }

		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x06005E78 RID: 24184 RVA: 0x0018121A File Offset: 0x0017F41A
		// (set) Token: 0x06005E79 RID: 24185 RVA: 0x00181222 File Offset: 0x0017F422
		public string Host { get; private set; }

		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x06005E7A RID: 24186 RVA: 0x0018122B File Offset: 0x0017F42B
		// (set) Token: 0x06005E7B RID: 24187 RVA: 0x00181233 File Offset: 0x0017F433
		public int Port { get; private set; }

		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x06005E7C RID: 24188 RVA: 0x0018123C File Offset: 0x0017F43C
		// (set) Token: 0x06005E7D RID: 24189 RVA: 0x00181244 File Offset: 0x0017F444
		public bool UseSsl { get; private set; }

		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x06005E7E RID: 24190 RVA: 0x0018124D File Offset: 0x0017F44D
		// (set) Token: 0x06005E7F RID: 24191 RVA: 0x00181255 File Offset: 0x0017F455
		public string ConnectAs { get; private set; }

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x06005E80 RID: 24192 RVA: 0x0018125E File Offset: 0x0017F45E
		// (set) Token: 0x06005E81 RID: 24193 RVA: 0x00181266 File Offset: 0x0017F466
		public string DynamicQueueNamePrefix { get; private set; }

		// Token: 0x06005E82 RID: 24194 RVA: 0x00181270 File Offset: 0x0017F470
		public QueueManagerInformation(string alias, string name, string channel, string host, int port, bool useSsl, string connectAs, string dynamicQueueNamePrefix)
		{
			this.Alias = alias;
			this.Name = name;
			this.Channel = channel;
			this.Host = host;
			this.Port = port;
			this.UseSsl = useSsl;
			this.ConnectAs = connectAs;
			this.DynamicQueueNamePrefix = dynamicQueueNamePrefix;
		}

		// Token: 0x06005E83 RID: 24195 RVA: 0x001812C0 File Offset: 0x0017F4C0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.AppendFormat("Queue Manager Information: Alias '{0}', Name '{1}', Channel '{2}', Host '{3}', Port {4}", new object[]
			{
				this.Alias,
				this.Name,
				this.Channel,
				this.Host,
				this.Port.ToString()
			});
			if (this.UseSsl)
			{
				stringBuilder.Append(", UseSsl");
			}
			if (!string.IsNullOrWhiteSpace(this.ConnectAs))
			{
				stringBuilder.AppendFormat(", ConnectAs '{0}'", this.ConnectAs);
			}
			if (this.DynamicQueueNamePrefix != "AMQ.*")
			{
				stringBuilder.AppendFormat(", DynamicQueueNamePrefix '{0}'", this.DynamicQueueNamePrefix);
			}
			return stringBuilder.ToString();
		}
	}
}
