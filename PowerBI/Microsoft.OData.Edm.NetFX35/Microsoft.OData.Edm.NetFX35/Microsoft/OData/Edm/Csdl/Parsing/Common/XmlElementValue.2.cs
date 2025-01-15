using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x0200019E RID: 414
	internal class XmlElementValue<TValue> : XmlElementValue
	{
		// Token: 0x0600080D RID: 2061 RVA: 0x00013C5F File Offset: 0x00011E5F
		internal XmlElementValue(string name, CsdlLocation location, TValue newValue)
			: base(name, location)
		{
			this.value = newValue;
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00013C70 File Offset: 0x00011E70
		internal override bool IsText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00013C73 File Offset: 0x00011E73
		internal override bool IsUsed
		{
			get
			{
				return this.isUsed;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00013C7B File Offset: 0x00011E7B
		internal override object UntypedValue
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00013C88 File Offset: 0x00011E88
		internal TValue Value
		{
			get
			{
				this.isUsed = true;
				return this.value;
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00013C97 File Offset: 0x00011E97
		internal override T ValueAs<T>()
		{
			return this.Value as T;
		}

		// Token: 0x0400041E RID: 1054
		private readonly TValue value;

		// Token: 0x0400041F RID: 1055
		private bool isUsed;
	}
}
