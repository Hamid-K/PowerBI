using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000BD RID: 189
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class EntitySetAttribute : Attribute
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x0001BDA3 File Offset: 0x00019FA3
		public EntitySetAttribute(string entitySet)
		{
			this.entitySet = entitySet;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001BDB2 File Offset: 0x00019FB2
		public string EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x040002CF RID: 719
		private readonly string entitySet;
	}
}
