using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000EE RID: 238
	public sealed class NameFilter : PropertyFilter
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x000281CC File Offset: 0x000263CC
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x000281D3 File Offset: 0x000263D3
		public override string Property
		{
			get
			{
				return "Name";
			}
			set
			{
				if (value != "Name")
				{
					throw new ArgumentException();
				}
			}
		}
	}
}
