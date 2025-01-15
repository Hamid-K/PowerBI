using System;

namespace NLog.Targets
{
	// Token: 0x02000028 RID: 40
	[Target("Chainsaw")]
	public class ChainsawTarget : NLogViewerTarget
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x00009FCC File Offset: 0x000081CC
		public ChainsawTarget()
		{
			base.IncludeNLogData = false;
			base.OptimizeBufferReuse = base.GetType() == typeof(ChainsawTarget);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00009FF6 File Offset: 0x000081F6
		public ChainsawTarget(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
