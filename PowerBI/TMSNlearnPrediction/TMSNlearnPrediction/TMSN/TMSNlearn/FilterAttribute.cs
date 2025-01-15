using System;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004D7 RID: 1239
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FilterAttribute : Attribute
	{
		// Token: 0x0600195C RID: 6492 RVA: 0x0008F491 File Offset: 0x0008D691
		public FilterAttribute(FilterSet set)
		{
			this.Set = set;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0008F4A0 File Offset: 0x0008D6A0
		public bool Contains(Command cmd)
		{
			return (this.Set & (FilterSet)(1 << (int)cmd)) != (FilterSet)0;
		}

		// Token: 0x04000F4F RID: 3919
		public readonly FilterSet Set;
	}
}
