using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200025A RID: 602
	internal class ProviderInvariantName : IProviderInvariantName
	{
		// Token: 0x06001ED6 RID: 7894 RVA: 0x00055B44 File Offset: 0x00053D44
		public ProviderInvariantName(string name)
		{
			this.Name = name;
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x00055B53 File Offset: 0x00053D53
		// (set) Token: 0x06001ED8 RID: 7896 RVA: 0x00055B5B File Offset: 0x00053D5B
		public string Name { get; private set; }
	}
}
