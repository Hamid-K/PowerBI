using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000CE RID: 206
	public class WindowsModelRoleMember : ModelRoleMember
	{
		// Token: 0x06000CF6 RID: 3318 RVA: 0x0006C3CC File Offset: 0x0006A5CC
		public WindowsModelRoleMember()
		{
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0006C3D4 File Offset: 0x0006A5D4
		internal WindowsModelRoleMember(IEqualityComparer<string> comparer)
			: base(comparer)
		{
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0006C3DD File Offset: 0x0006A5DD
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new WindowsModelRoleMember();
		}
	}
}
