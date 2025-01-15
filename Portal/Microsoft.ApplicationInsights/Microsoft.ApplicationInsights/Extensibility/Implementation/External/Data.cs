using System;
using System.CodeDom.Compiler;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000B5 RID: 181
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class Data<TDomain> : Base
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001655B File Offset: 0x0001475B
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00016563 File Offset: 0x00014763
		public TDomain baseData { get; set; }

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001656C File Offset: 0x0001476C
		public Data()
			: this("AI.Data", "Data")
		{
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001657E File Offset: 0x0001477E
		protected Data(string fullName, string name)
		{
		}
	}
}
