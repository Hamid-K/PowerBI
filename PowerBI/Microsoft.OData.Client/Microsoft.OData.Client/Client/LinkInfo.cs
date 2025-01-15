using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000054 RID: 84
	public sealed class LinkInfo
	{
		// Token: 0x06000299 RID: 665 RVA: 0x0000A114 File Offset: 0x00008314
		internal LinkInfo(string propertyName)
		{
			this.name = propertyName;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000A123 File Offset: 0x00008323
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000A12B File Offset: 0x0000832B
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000A133 File Offset: 0x00008333
		public Uri NavigationLink
		{
			get
			{
				return this.navigationLink;
			}
			internal set
			{
				this.navigationLink = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000A13C File Offset: 0x0000833C
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000A144 File Offset: 0x00008344
		public Uri AssociationLink
		{
			get
			{
				return this.associationLink;
			}
			internal set
			{
				this.associationLink = value;
			}
		}

		// Token: 0x040000E0 RID: 224
		private Uri navigationLink;

		// Token: 0x040000E1 RID: 225
		private Uri associationLink;

		// Token: 0x040000E2 RID: 226
		private string name;
	}
}
