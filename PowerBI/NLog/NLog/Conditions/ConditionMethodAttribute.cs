using System;
using NLog.Config;

namespace NLog.Conditions
{
	// Token: 0x020001AA RID: 426
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class ConditionMethodAttribute : NameBaseAttribute
	{
		// Token: 0x0600130F RID: 4879 RVA: 0x00033A10 File Offset: 0x00031C10
		public ConditionMethodAttribute(string name)
			: base(name)
		{
		}
	}
}
