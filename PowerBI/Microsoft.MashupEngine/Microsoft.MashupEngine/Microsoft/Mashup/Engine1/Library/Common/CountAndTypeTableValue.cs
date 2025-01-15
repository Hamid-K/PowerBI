using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001043 RID: 4163
	internal class CountAndTypeTableValue : CountTableValue
	{
		// Token: 0x06006C99 RID: 27801 RVA: 0x0017616B File Offset: 0x0017436B
		public CountAndTypeTableValue(long count, TableTypeValue type)
			: base(count)
		{
			this.type = type;
		}

		// Token: 0x17001EE5 RID: 7909
		// (get) Token: 0x06006C9A RID: 27802 RVA: 0x0017617B File Offset: 0x0017437B
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04003C67 RID: 15463
		private readonly TableTypeValue type;
	}
}
