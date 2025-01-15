using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x0200059D RID: 1437
	[ConfigurationCollection(typeof(RemoteEnvironment), AddItemName = "remoteEnvironment", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class RemoteEnvironmentCollection : ConfigurationElementCollection
	{
		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060031D6 RID: 12758 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060031D7 RID: 12759 RVA: 0x000A6FF8 File Offset: 0x000A51F8
		protected override string ElementName
		{
			get
			{
				return "remoteEnvironment";
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x000A6FFF File Offset: 0x000A51FF
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RemoteEnvironmentCollection.s_properties;
			}
		}

		// Token: 0x17000A92 RID: 2706
		public RemoteEnvironment this[int index]
		{
			get
			{
				return (RemoteEnvironment)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				base.BaseAdd(index, value);
			}
		}

		// Token: 0x17000A93 RID: 2707
		public RemoteEnvironment this[string name]
		{
			get
			{
				return (RemoteEnvironment)base.BaseGet(name);
			}
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000A7022 File Offset: 0x000A5222
		protected override ConfigurationElement CreateNewElement()
		{
			return new RemoteEnvironment();
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000A7029 File Offset: 0x000A5229
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as RemoteEnvironment).GetElementKey();
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddRemoteEnvironment(RemoteEnvironment remoteEnvironment)
		{
			base.BaseAdd(remoteEnvironment, true);
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000A7036 File Offset: 0x000A5236
		public void RemoveRemoteEnvironment(RemoteEnvironment remoteEnvironment)
		{
			base.BaseRemove(remoteEnvironment.GetElementKey());
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000A7044 File Offset: 0x000A5244
		public bool Contains(string name)
		{
			object[] array = base.BaseGetAllKeys();
			for (int i = 0; i < array.Length; i++)
			{
				if ((string)array[i] == name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001C6E RID: 7278
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
