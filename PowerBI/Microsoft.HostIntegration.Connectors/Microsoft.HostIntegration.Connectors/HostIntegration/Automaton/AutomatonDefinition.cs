using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.Automaton
{
	// Token: 0x020004C2 RID: 1218
	public class AutomatonDefinition
	{
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x0600298B RID: 10635 RVA: 0x0007D1FB File Offset: 0x0007B3FB
		// (set) Token: 0x0600298C RID: 10636 RVA: 0x0007D203 File Offset: 0x0007B403
		public List<StateAsCodeDriver> StatesAsCode { get; private set; }

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x0600298D RID: 10637 RVA: 0x0007D20C File Offset: 0x0007B40C
		// (set) Token: 0x0600298E RID: 10638 RVA: 0x0007D214 File Offset: 0x0007B414
		public string Name { get; private set; }

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x0600298F RID: 10639 RVA: 0x0007D21D File Offset: 0x0007B41D
		// (set) Token: 0x06002990 RID: 10640 RVA: 0x0007D225 File Offset: 0x0007B425
		public string InstanceName { get; private set; }

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002991 RID: 10641 RVA: 0x0007D22E File Offset: 0x0007B42E
		// (set) Token: 0x06002992 RID: 10642 RVA: 0x0007D236 File Offset: 0x0007B436
		public AutomatonContext Context { get; private set; }

		// Token: 0x06002993 RID: 10643 RVA: 0x0007D23F File Offset: 0x0007B43F
		public AutomatonDefinition(string name, string instanceName, AutomatonContext context, List<StateAsCodeDriver> statesAsCode, FlagBasedTracePoint tracePoint)
			: this(name, instanceName, context, statesAsCode, tracePoint, 0)
		{
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x0007D250 File Offset: 0x0007B450
		public AutomatonDefinition(string name, string instanceName, AutomatonContext context, List<StateAsCodeDriver> statesAsCode, FlagBasedTracePoint tracePoint, int numberOfSubAutomatons)
		{
			this.Name = name;
			this.InstanceName = instanceName;
			this.Context = context;
			this.Context.CurrentState = new StateAsCodeDriver[numberOfSubAutomatons + 1];
			this.Context.CurrentState[0] = statesAsCode[0];
			this.Context.AutomatonTracePoint = tracePoint;
			this.StatesAsCode = statesAsCode;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x0007D2B8 File Offset: 0x0007B4B8
		public AutomatonDefinition(string name, AutomatonDefinition primaryAutomatonDefinition, List<StateAsCodeDriver> statesAsCode, int subAutomatonIndex)
		{
			this.Name = name;
			this.InstanceName = primaryAutomatonDefinition.InstanceName;
			this.Context = primaryAutomatonDefinition.Context;
			this.Context.CurrentState[subAutomatonIndex] = statesAsCode[0];
			this.StatesAsCode = statesAsCode;
		}
	}
}
