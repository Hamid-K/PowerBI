using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000C5 RID: 197
	[Serializable]
	internal class AllowElement : ConfigurationElement, ISerializable
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x00015607 File Offset: 0x00013807
		public AllowElement()
		{
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00016E56 File Offset: 0x00015056
		public AllowElement(string users)
		{
			this.Users = users;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00016E65 File Offset: 0x00015065
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x00016E77 File Offset: 0x00015077
		[ConfigurationProperty("users", IsRequired = true)]
		public string Users
		{
			get
			{
				return (string)base["users"];
			}
			set
			{
				base["users"] = value;
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00016E85 File Offset: 0x00015085
		protected AllowElement(SerializationInfo info, StreamingContext context)
		{
			this.Users = (string)info.GetValue("users", typeof(string));
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00016EAD File Offset: 0x000150AD
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("users", this.Users);
		}

		// Token: 0x0400038F RID: 911
		internal const string USERS = "users";
	}
}
