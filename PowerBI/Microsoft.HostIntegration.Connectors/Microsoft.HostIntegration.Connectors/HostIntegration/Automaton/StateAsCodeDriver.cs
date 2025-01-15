using System;

namespace Microsoft.HostIntegration.Automaton
{
	// Token: 0x020004CA RID: 1226
	public abstract class StateAsCodeDriver
	{
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060029E2 RID: 10722
		public abstract string Name { get; }

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060029E3 RID: 10723 RVA: 0x0007E437 File Offset: 0x0007C637
		// (set) Token: 0x060029E4 RID: 10724 RVA: 0x0007E43F File Offset: 0x0007C63F
		private protected AutomatonDriverAsCode AutomatonDriver { protected get; private set; }

		// Token: 0x060029E5 RID: 10725 RVA: 0x0007E448 File Offset: 0x0007C648
		public StateAsCodeDriver(AutomatonDriverAsCode driver)
		{
			this.AutomatonDriver = driver;
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x0007E457 File Offset: 0x0007C657
		public DynamicDataBuffer GetBuffer()
		{
			return this.GetBuffer(0);
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x0007E460 File Offset: 0x0007C660
		public DynamicDataBuffer GetBuffer(int capacity)
		{
			return this.AutomatonDriver.GetBuffer(capacity);
		}

		// Token: 0x060029E8 RID: 10728
		public abstract int Process(ref int eventToProcess);
	}
}
