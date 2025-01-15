using System;
using NLog.Config;

namespace NLog.Filters
{
	// Token: 0x02000172 RID: 370
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class FilterAttribute : NameBaseAttribute
	{
		// Token: 0x06001145 RID: 4421 RVA: 0x0002CCA2 File Offset: 0x0002AEA2
		public FilterAttribute(string name)
			: base(name)
		{
		}
	}
}
