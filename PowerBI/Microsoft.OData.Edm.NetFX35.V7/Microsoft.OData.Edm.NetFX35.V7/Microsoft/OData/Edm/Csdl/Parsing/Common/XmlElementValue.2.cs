using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001B9 RID: 441
	internal class XmlElementValue<TValue> : XmlElementValue
	{
		// Token: 0x06000C5F RID: 3167 RVA: 0x00023765 File Offset: 0x00021965
		internal XmlElementValue(string name, CsdlLocation location, TValue newValue)
			: base(name, location)
		{
			this.value = newValue;
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00008EC3 File Offset: 0x000070C3
		internal override bool IsText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00023776 File Offset: 0x00021976
		internal override bool IsUsed
		{
			get
			{
				return this.isUsed;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0002377E File Offset: 0x0002197E
		internal override object UntypedValue
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0002378B File Offset: 0x0002198B
		internal TValue Value
		{
			get
			{
				this.isUsed = true;
				return this.value;
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002379A File Offset: 0x0002199A
		internal override T ValueAs<T>()
		{
			return this.Value as T;
		}

		// Token: 0x040006C3 RID: 1731
		private readonly TValue value;

		// Token: 0x040006C4 RID: 1732
		private bool isUsed;
	}
}
