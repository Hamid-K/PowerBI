using System;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000711 RID: 1809
	[DataContract]
	[Serializable]
	public class CustomTransportHeaders
	{
		// Token: 0x0600394B RID: 14667 RVA: 0x000BF6BE File Offset: 0x000BD8BE
		public CustomTransportHeaders()
		{
			this.headerToBeforeDataSent = null;
			this.headerFromBeforeDataSent = null;
			this.headerFromAfterDataSent = null;
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x0600394C RID: 14668 RVA: 0x000BF6DB File Offset: 0x000BD8DB
		// (set) Token: 0x0600394D RID: 14669 RVA: 0x000BF6E3 File Offset: 0x000BD8E3
		[DataMember]
		public object HeaderToBeforeDataSent
		{
			get
			{
				return this.headerToBeforeDataSent;
			}
			set
			{
				this.headerToBeforeDataSent = value;
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x0600394E RID: 14670 RVA: 0x000BF6EC File Offset: 0x000BD8EC
		// (set) Token: 0x0600394F RID: 14671 RVA: 0x000BF6F4 File Offset: 0x000BD8F4
		[DataMember]
		public object HeaderFromBeforeDataSent
		{
			get
			{
				return this.headerFromBeforeDataSent;
			}
			set
			{
				this.headerFromBeforeDataSent = value;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06003950 RID: 14672 RVA: 0x000BF6FD File Offset: 0x000BD8FD
		// (set) Token: 0x06003951 RID: 14673 RVA: 0x000BF705 File Offset: 0x000BD905
		[DataMember]
		public object HeaderFromAfterDataSent
		{
			get
			{
				return this.headerFromAfterDataSent;
			}
			set
			{
				this.headerFromAfterDataSent = value;
			}
		}

		// Token: 0x0400214C RID: 8524
		private object headerToBeforeDataSent;

		// Token: 0x0400214D RID: 8525
		private object headerFromBeforeDataSent;

		// Token: 0x0400214E RID: 8526
		private object headerFromAfterDataSent;
	}
}
