using System;
using System.ComponentModel;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200009C RID: 156
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class LocalizedDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000756 RID: 1878 RVA: 0x00024F0C File Offset: 0x0002310C
		internal LocalizedDescriptionAttribute(string descriptionId)
			: base(descriptionId)
		{
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00024F15 File Offset: 0x00023115
		public override string Description
		{
			get
			{
				return SR.Keys.GetString(base.Description);
			}
		}
	}
}
