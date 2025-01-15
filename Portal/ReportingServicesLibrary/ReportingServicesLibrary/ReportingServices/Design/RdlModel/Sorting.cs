using System;
using System.Collections;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003DE RID: 990
	public sealed class Sorting : ArrayList
	{
		// Token: 0x06001F93 RID: 8083 RVA: 0x000566D2 File Offset: 0x000548D2
		public Sorting()
		{
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x0007E909 File Offset: 0x0007CB09
		internal Sorting(Sorting sorting)
		{
			this.AddRange(sorting);
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0001FB54 File Offset: 0x0001DD54
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x170008D7 RID: 2263
		public SortBy this[int index]
		{
			get
			{
				return (SortBy)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x02000519 RID: 1305
		public enum Direction
		{
			// Token: 0x04001280 RID: 4736
			Ascending,
			// Token: 0x04001281 RID: 4737
			Descending
		}
	}
}
