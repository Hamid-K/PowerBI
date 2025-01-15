using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200001B RID: 27
	public class DiagnosticEventArgs : EventArgs
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00006466 File Offset: 0x00004666
		internal DiagnosticEventArgs(string channelName, string eventName, DateTime eventTime, IResource resource, IDictionary<string, object> properties)
		{
			this.channelName = channelName;
			this.eventName = eventName;
			this.eventTime = eventTime;
			this.dataSource = ((resource != null) ? new DataSource(resource) : null);
			this.properties = properties;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000649F File Offset: 0x0000469F
		public string ChannelName
		{
			get
			{
				return this.channelName;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000064A7 File Offset: 0x000046A7
		public string EventName
		{
			get
			{
				return this.eventName;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000064AF File Offset: 0x000046AF
		public DateTime EventTime
		{
			get
			{
				return this.eventTime;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000064B7 File Offset: 0x000046B7
		public DataSource DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000064BF File Offset: 0x000046BF
		public IDictionary<string, object> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x040000A0 RID: 160
		private readonly string channelName;

		// Token: 0x040000A1 RID: 161
		private readonly string eventName;

		// Token: 0x040000A2 RID: 162
		private readonly DateTime eventTime;

		// Token: 0x040000A3 RID: 163
		private readonly DataSource dataSource;

		// Token: 0x040000A4 RID: 164
		private readonly IDictionary<string, object> properties;
	}
}
