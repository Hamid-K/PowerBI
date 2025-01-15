using System;
using NLog.Config;

namespace NLog.Targets
{
	// Token: 0x02000054 RID: 84
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class TargetAttribute : NameBaseAttribute
	{
		// Token: 0x060007CF RID: 1999 RVA: 0x00014010 File Offset: 0x00012210
		public TargetAttribute(string name)
			: base(name)
		{
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00014019 File Offset: 0x00012219
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x00014021 File Offset: 0x00012221
		public bool IsWrapper { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0001402A File Offset: 0x0001222A
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00014032 File Offset: 0x00012232
		public bool IsCompound { get; set; }
	}
}
