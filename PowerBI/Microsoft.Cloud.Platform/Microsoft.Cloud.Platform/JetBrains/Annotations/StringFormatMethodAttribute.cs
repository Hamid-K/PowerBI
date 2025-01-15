using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000559 RID: 1369
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class StringFormatMethodAttribute : Attribute
	{
		// Token: 0x06002A1A RID: 10778 RVA: 0x0009818B File Offset: 0x0009638B
		public StringFormatMethodAttribute(string formatParameterName)
		{
			this.FormatParameterName = formatParameterName;
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x0009819A File Offset: 0x0009639A
		// (set) Token: 0x06002A1C RID: 10780 RVA: 0x000981A2 File Offset: 0x000963A2
		[UsedImplicitly]
		public string FormatParameterName { get; private set; }
	}
}
