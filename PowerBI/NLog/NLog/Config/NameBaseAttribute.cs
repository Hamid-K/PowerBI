using System;
using JetBrains.Annotations;

namespace NLog.Config
{
	// Token: 0x02000197 RID: 407
	[MeansImplicitUse]
	public abstract class NameBaseAttribute : Attribute
	{
		// Token: 0x060012A2 RID: 4770 RVA: 0x00032979 File Offset: 0x00030B79
		protected NameBaseAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00032988 File Offset: 0x00030B88
		// (set) Token: 0x060012A4 RID: 4772 RVA: 0x00032990 File Offset: 0x00030B90
		public string Name { get; private set; }
	}
}
