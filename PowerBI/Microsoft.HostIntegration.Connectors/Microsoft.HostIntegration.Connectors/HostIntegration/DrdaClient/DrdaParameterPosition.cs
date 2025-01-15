using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A01 RID: 2561
	internal class DrdaParameterPosition : IComparable
	{
		// Token: 0x060050A2 RID: 20642 RVA: 0x00142CB0 File Offset: 0x00140EB0
		public DrdaParameterPosition(int index, DrdaParameter parameter)
		{
			this.Index = index;
			this.Parameter = parameter;
			this.Binding = new DrdaParameterBinding();
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x00142CD1 File Offset: 0x00140ED1
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (obj is DrdaParameterPosition)
			{
				return this.Index - ((DrdaParameterPosition)obj).Index;
			}
			throw new ArgumentException();
		}

		// Token: 0x04003F62 RID: 16226
		public int Index;

		// Token: 0x04003F63 RID: 16227
		public DrdaParameter Parameter;

		// Token: 0x04003F64 RID: 16228
		public DrdaParameterBinding Binding;
	}
}
