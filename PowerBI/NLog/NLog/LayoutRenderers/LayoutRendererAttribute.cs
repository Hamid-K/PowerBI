using System;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000CD RID: 205
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class LayoutRendererAttribute : NameBaseAttribute
	{
		// Token: 0x06000C93 RID: 3219 RVA: 0x000202EE File Offset: 0x0001E4EE
		public LayoutRendererAttribute(string name)
			: base(name)
		{
		}
	}
}
