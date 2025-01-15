using System;
using Microsoft.DataShaping.Engine;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ExploreHost
{
	// Token: 0x02000025 RID: 37
	public class ASQueryLimits
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00003A72 File Offset: 0x00001C72
		public ASQueryLimits(TimeSpan asCommandTimeoutSpan, int asCommandMemoryLimitInKiB)
		{
			this.TimeoutSpan = asCommandTimeoutSpan;
			this.MemoryLimitsKB = asCommandMemoryLimitInKiB;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003A88 File Offset: 0x00001C88
		public int MemoryLimitsKB { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003A90 File Offset: 0x00001C90
		public TimeSpan TimeoutSpan { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00003A98 File Offset: 0x00001C98
		public int TimeoutInSeconds
		{
			get
			{
				return (int)Math.Round(this.TimeoutSpan.TotalSeconds, MidpointRounding.AwayFromZero);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003ABA File Offset: 0x00001CBA
		internal DbCommandOptions ToDBCommandOptions(RequestPriorityKind requestPriorityKind)
		{
			return new DbCommandOptions(this.MemoryLimitsKB, this.TimeoutInSeconds, requestPriorityKind, null);
		}

		// Token: 0x04000088 RID: 136
		public static readonly ASQueryLimits Unlimited = new ASQueryLimits(default(TimeSpan), 0);
	}
}
