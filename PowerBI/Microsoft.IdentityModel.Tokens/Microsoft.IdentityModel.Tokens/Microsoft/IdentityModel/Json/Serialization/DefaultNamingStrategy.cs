using System;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x02000077 RID: 119
	internal class DefaultNamingStrategy : NamingStrategy
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x0001BB3F File Offset: 0x00019D3F
		protected override string ResolvePropertyName(string name)
		{
			return name;
		}
	}
}
