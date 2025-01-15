using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x02000401 RID: 1025
	public abstract class ConfigurationProviderBase : IConfigurationProvider, IShuttable, IIdentifiable
	{
		// Token: 0x06001F5B RID: 8027 RVA: 0x00075C3B File Offset: 0x00073E3B
		protected ConfigurationProviderBase()
		{
			this.m_name = base.GetType().Name;
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x00075C54 File Offset: 0x00073E54
		public virtual Dictionary<Type, IConfigurationClass> Start(IConfigurationProviderOwner owner)
		{
			this.Owner = owner;
			return null;
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x00075C5E File Offset: 0x00073E5E
		public virtual IEnumerable<ConfigurationSection> GetInitialConfiguration()
		{
			return Enumerable.Empty<ConfigurationSection>();
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001F5E RID: 8030 RVA: 0x00075C65 File Offset: 0x00073E65
		// (set) Token: 0x06001F5F RID: 8031 RVA: 0x00075C6D File Offset: 0x00073E6D
		private protected IConfigurationProviderOwner Owner { protected get; private set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001F60 RID: 8032 RVA: 0x00075C76 File Offset: 0x00073E76
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00009B3B File Offset: 0x00007D3B
		public virtual void Stop()
		{
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x00009B3B File Offset: 0x00007D3B
		public virtual void WaitForStopToComplete()
		{
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x00009B3B File Offset: 0x00007D3B
		public virtual void Shutdown()
		{
		}

		// Token: 0x04000B03 RID: 2819
		private readonly string m_name;
	}
}
