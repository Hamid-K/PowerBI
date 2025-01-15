using System;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000743 RID: 1859
	[DataContract]
	[Serializable]
	public class RECPropertyPage : IRECPropertyPage
	{
		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x000C6731 File Offset: 0x000C4931
		// (set) Token: 0x06003A6E RID: 14958 RVA: 0x000C6739 File Offset: 0x000C4939
		[DataMember]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06003A6F RID: 14959 RVA: 0x000C6742 File Offset: 0x000C4942
		// (set) Token: 0x06003A70 RID: 14960 RVA: 0x000C674A File Offset: 0x000C494A
		[DataMember]
		public int Order
		{
			get
			{
				return this.order;
			}
			set
			{
				this.order = value;
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06003A71 RID: 14961 RVA: 0x000C6753 File Offset: 0x000C4953
		// (set) Token: 0x06003A72 RID: 14962 RVA: 0x000C675B File Offset: 0x000C495B
		[DataMember]
		public Guid Identifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06003A73 RID: 14963 RVA: 0x000C6764 File Offset: 0x000C4964
		// (set) Token: 0x06003A74 RID: 14964 RVA: 0x000C6786 File Offset: 0x000C4986
		[DataMember]
		public string PropertyGUID
		{
			get
			{
				return "{" + this.identifier.ToString() + "}";
			}
			set
			{
				this.identifier = new Guid(value);
			}
		}

		// Token: 0x04002327 RID: 8999
		private Guid identifier;

		// Token: 0x04002328 RID: 9000
		private string name;

		// Token: 0x04002329 RID: 9001
		private int order;
	}
}
