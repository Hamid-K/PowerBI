using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000076 RID: 118
	public class DefaultNamingStrategy : NamingStrategy
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x0001BB13 File Offset: 0x00019D13
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return name;
		}
	}
}
