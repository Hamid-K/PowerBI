using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Excel
{
	// Token: 0x02000C5E RID: 3166
	internal class ExcelTableValue : TableValue
	{
		// Token: 0x06005607 RID: 22023 RVA: 0x0012A6B7 File Offset: 0x001288B7
		public ExcelTableValue(TableValue navigationTable)
		{
			this.navigationTable = navigationTable;
		}

		// Token: 0x17001A13 RID: 6675
		// (get) Token: 0x06005608 RID: 22024 RVA: 0x0012A6C6 File Offset: 0x001288C6
		public override TypeValue Type
		{
			get
			{
				return this.navigationTable.Type;
			}
		}

		// Token: 0x06005609 RID: 22025 RVA: 0x0012A6D3 File Offset: 0x001288D3
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return this.navigationTable.GetEnumerator();
		}

		// Token: 0x17001A14 RID: 6676
		public override Value this[Value key]
		{
			get
			{
				Value value;
				if (this.navigationTable.TryGetValue(key, out value))
				{
					return value;
				}
				return base[key];
			}
		}

		// Token: 0x04003076 RID: 12406
		private TableValue navigationTable;
	}
}
