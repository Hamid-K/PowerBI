using System;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002125 RID: 8485
	internal enum ElementState
	{
		// Token: 0x0400696E RID: 26990
		Null,
		// Token: 0x0400696F RID: 26991
		Start,
		// Token: 0x04006970 RID: 26992
		End,
		// Token: 0x04006971 RID: 26993
		LeafStart,
		// Token: 0x04006972 RID: 26994
		LeafEnd,
		// Token: 0x04006973 RID: 26995
		LoadEnd,
		// Token: 0x04006974 RID: 26996
		MiscNode,
		// Token: 0x04006975 RID: 26997
		EOF
	}
}
