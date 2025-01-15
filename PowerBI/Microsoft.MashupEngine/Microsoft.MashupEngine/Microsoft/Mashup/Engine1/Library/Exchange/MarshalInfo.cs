using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BDC RID: 3036
	public class MarshalInfo
	{
		// Token: 0x060052D2 RID: 21202 RVA: 0x00117E59 File Offset: 0x00116059
		public MarshalInfo(TypeValue typeValue, Func<object, Value> marshal)
		{
			this.typeValue = typeValue;
			this.marshal = marshal;
		}

		// Token: 0x17001986 RID: 6534
		// (get) Token: 0x060052D3 RID: 21203 RVA: 0x00117E6F File Offset: 0x0011606F
		public TypeValue TypeValue
		{
			get
			{
				return this.typeValue;
			}
		}

		// Token: 0x17001987 RID: 6535
		// (get) Token: 0x060052D4 RID: 21204 RVA: 0x00117E77 File Offset: 0x00116077
		public Func<object, Value> Marshal
		{
			get
			{
				return this.marshal;
			}
		}

		// Token: 0x04002DB1 RID: 11697
		private TypeValue typeValue;

		// Token: 0x04002DB2 RID: 11698
		private Func<object, Value> marshal;
	}
}
