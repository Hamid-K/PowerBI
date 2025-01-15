using System;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FC8 RID: 4040
	internal class ActiveDirectoryClassSchema
	{
		// Token: 0x06006A13 RID: 27155 RVA: 0x0016D6EB File Offset: 0x0016B8EB
		public ActiveDirectoryClassSchema(string className, string[] immediateParents, string[] attributeNames)
		{
			this.className = className;
			this.parents = immediateParents;
			this.attributeNames = attributeNames;
		}

		// Token: 0x17001E72 RID: 7794
		// (get) Token: 0x06006A14 RID: 27156 RVA: 0x0016D708 File Offset: 0x0016B908
		public string Name
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x17001E73 RID: 7795
		// (get) Token: 0x06006A15 RID: 27157 RVA: 0x0016D710 File Offset: 0x0016B910
		public string[] AttributeNames
		{
			get
			{
				return this.attributeNames;
			}
		}

		// Token: 0x17001E74 RID: 7796
		// (get) Token: 0x06006A16 RID: 27158 RVA: 0x0016D718 File Offset: 0x0016B918
		public string[] ImmediateParentNames
		{
			get
			{
				return this.parents;
			}
		}

		// Token: 0x04003ADF RID: 15071
		private readonly string className;

		// Token: 0x04003AE0 RID: 15072
		private readonly string[] parents;

		// Token: 0x04003AE1 RID: 15073
		private readonly string[] attributeNames;
	}
}
