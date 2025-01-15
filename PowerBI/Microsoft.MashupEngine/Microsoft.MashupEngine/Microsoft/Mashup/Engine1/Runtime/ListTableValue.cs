using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200165F RID: 5727
	internal class ListTableValue : TableValue
	{
		// Token: 0x060090DE RID: 37086 RVA: 0x001E1BB0 File Offset: 0x001DFDB0
		public ListTableValue(ListValue list, TableTypeValue type)
		{
			this.list = list;
			this.type = type;
		}

		// Token: 0x060090DF RID: 37087 RVA: 0x001E1BC6 File Offset: 0x001DFDC6
		public override bool TryGetProcessor(out QueryProcessor processor)
		{
			return this.list.TryGetProcessor(out processor);
		}

		// Token: 0x170025DC RID: 9692
		// (get) Token: 0x060090E0 RID: 37088 RVA: 0x001E1BD4 File Offset: 0x001DFDD4
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170025DD RID: 9693
		// (get) Token: 0x060090E1 RID: 37089 RVA: 0x001E1BDC File Offset: 0x001DFDDC
		public override long LargeCount
		{
			get
			{
				return this.list.LargeCount;
			}
		}

		// Token: 0x060090E2 RID: 37090 RVA: 0x001E1BE9 File Offset: 0x001DFDE9
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x04004DCC RID: 19916
		private readonly ListValue list;

		// Token: 0x04004DCD RID: 19917
		private readonly TableTypeValue type;
	}
}
