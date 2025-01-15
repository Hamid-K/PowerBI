using System;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000EF RID: 239
	internal abstract class LimitOperator
	{
		// Token: 0x06000E0F RID: 3599 RVA: 0x00023BD8 File Offset: 0x00021DD8
		protected LimitOperator(int count)
		{
			this.Count = count;
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00023BE7 File Offset: 0x00021DE7
		public int Count { get; }
	}
}
