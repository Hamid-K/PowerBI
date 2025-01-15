using System;
using NLog.Config;

namespace NLog.Layouts
{
	// Token: 0x020000A7 RID: 167
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class LayoutAttribute : NameBaseAttribute
	{
		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001BEE7 File Offset: 0x0001A0E7
		public LayoutAttribute(string name)
			: base(name)
		{
		}
	}
}
