using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl.Schema
{
	// Token: 0x0200015D RID: 349
	internal abstract class TmdlSchemaElement
	{
		// Token: 0x060015FA RID: 5626 RVA: 0x0009267B File Offset: 0x0009087B
		private protected TmdlSchemaElement()
		{
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x00092683 File Offset: 0x00090883
		// (set) Token: 0x060015FC RID: 5628 RVA: 0x0009268B File Offset: 0x0009088B
		public bool IsReadOnly { get; private set; }

		// Token: 0x060015FD RID: 5629 RVA: 0x00092694 File Offset: 0x00090894
		public void MakeReadOnly()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(TomSR.Exception_ObjectIsAlreadyMarkedAsReadOnly(base.GetType().FullName));
			}
			this.MakeReadOnlyImpl();
			this.IsReadOnly = true;
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x000926C1 File Offset: 0x000908C1
		private protected void EnsureNotReadOnly()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(TomSR.Exception_ObjectMarkedAsReadOnly(base.GetType().FullName));
			}
		}

		// Token: 0x060015FF RID: 5631
		private protected abstract void MakeReadOnlyImpl();
	}
}
