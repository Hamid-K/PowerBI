using System;

namespace Microsoft.Data.Serialization
{
	// Token: 0x02000153 RID: 339
	public interface IPageReader : IDisposable
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060005EF RID: 1519
		TableSchema Schema { get; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060005F0 RID: 1520
		IProgress Progress { get; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060005F1 RID: 1521
		int MaxPageRowCount { get; }

		// Token: 0x060005F2 RID: 1522
		IPage CreatePage();

		// Token: 0x060005F3 RID: 1523
		void Read(IPage page);

		// Token: 0x060005F4 RID: 1524
		IPageReader NextResult();
	}
}
