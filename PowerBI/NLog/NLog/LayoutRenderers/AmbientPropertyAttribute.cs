using System;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000B1 RID: 177
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class AmbientPropertyAttribute : NameBaseAttribute
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x0001E25D File Offset: 0x0001C45D
		public AmbientPropertyAttribute(string name)
			: base(name)
		{
		}
	}
}
