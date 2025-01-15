using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200026F RID: 623
	public sealed class ExtendedAttribute
	{
		// Token: 0x060015EA RID: 5610 RVA: 0x0003D38D File Offset: 0x0003C38D
		public ExtendedAttribute()
		{
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0003D395 File Offset: 0x0003C395
		public ExtendedAttribute(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x0003D3AB File Offset: 0x0003C3AB
		// (set) Token: 0x060015ED RID: 5613 RVA: 0x0003D3B3 File Offset: 0x0003C3B3
		public string Name { get; set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x0003D3BC File Offset: 0x0003C3BC
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x0003D3C4 File Offset: 0x0003C3C4
		public string Value { get; set; }
	}
}
