using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000153 RID: 339
	[Serializable]
	internal class PersistenceProviderElement : ConfigurationElement, ISerializable
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x00015607 File Offset: 0x00013807
		public PersistenceProviderElement()
		{
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000234CE File Offset: 0x000216CE
		protected PersistenceProviderElement(SerializationInfo info, StreamingContext context)
		{
			this.Name = info.GetString("name");
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000234E7 File Offset: 0x000216E7
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("name", this.Name);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x000234FA File Offset: 0x000216FA
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0400074C RID: 1868
		internal const string NAME = "name";

		// Token: 0x0400074D RID: 1869
		internal const string PERSISTENCE_PROVIDER = "persistenceProvider";
	}
}
