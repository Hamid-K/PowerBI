using System;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x02000076 RID: 118
	internal class DefaultNamingStrategy : NamingStrategy
	{
		// Token: 0x06000653 RID: 1619 RVA: 0x0001B56B File Offset: 0x0001976B
		protected override string ResolvePropertyName(string name)
		{
			return name;
		}
	}
}
