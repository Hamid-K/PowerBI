using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001C6 RID: 454
	internal class XmlElementValue<TValue> : XmlElementValue
	{
		// Token: 0x06000D11 RID: 3345 RVA: 0x0002592D File Offset: 0x00023B2D
		internal XmlElementValue(string name, CsdlLocation location, TValue newValue)
			: base(name, location)
		{
			this.value = newValue;
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x000026A6 File Offset: 0x000008A6
		internal override bool IsText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0002593E File Offset: 0x00023B3E
		internal override bool IsUsed
		{
			get
			{
				return this.isUsed;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00025946 File Offset: 0x00023B46
		internal override object UntypedValue
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00025953 File Offset: 0x00023B53
		internal TValue Value
		{
			get
			{
				this.isUsed = true;
				return this.value;
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00025962 File Offset: 0x00023B62
		internal override T ValueAs<T>()
		{
			return this.Value as T;
		}

		// Token: 0x0400073C RID: 1852
		private readonly TValue value;

		// Token: 0x0400073D RID: 1853
		private bool isUsed;
	}
}
