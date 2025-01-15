using System;
using Microsoft.SqlServer.XEvent.Internal;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class XEventAttribute : Attribute
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00002A90 File Offset: 0x00002A90
		public XEventAttribute(string name, string eventGuid, Channel channel, string descriptionKey, string keyword)
		{
			XEventPackageRegistrar.CheckNamingRules(name);
			this.m_name = name;
			this.m_descriptionKey = descriptionKey;
			this.m_channel = channel;
			ValueType valueType = default(Guid);
			(Guid)valueType = new Guid(eventGuid);
			this.m_guid = valueType;
			this.m_keyword = keyword;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00002A30 File Offset: 0x00002A30
		public XEventAttribute(string name, string eventGuid, Channel channel, string descriptionKey)
		{
			XEventPackageRegistrar.CheckNamingRules(name);
			this.m_name = name;
			this.m_descriptionKey = descriptionKey;
			this.m_channel = channel;
			ValueType valueType = default(Guid);
			(Guid)valueType = new Guid(eventGuid);
			this.m_guid = valueType;
			this.m_keyword = null;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00002AF0 File Offset: 0x00002AF0
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00002B0C File Offset: 0x00002B0C
		public ValueType EventGuid
		{
			get
			{
				return this.m_guid;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00002B28 File Offset: 0x00002B28
		public string DescriptionKey
		{
			get
			{
				return this.m_descriptionKey;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00002B44 File Offset: 0x00002B44
		public Channel EventChannel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00002B60 File Offset: 0x00002B60
		public string Keyword
		{
			get
			{
				return this.m_keyword;
			}
		}

		// Token: 0x040000EE RID: 238
		internal bool m_publishAttached;

		// Token: 0x040000EF RID: 239
		private string m_name;

		// Token: 0x040000F0 RID: 240
		private string m_descriptionKey;

		// Token: 0x040000F1 RID: 241
		private ValueType m_guid;

		// Token: 0x040000F2 RID: 242
		private Channel m_channel;

		// Token: 0x040000F3 RID: 243
		private string m_keyword;
	}
}
