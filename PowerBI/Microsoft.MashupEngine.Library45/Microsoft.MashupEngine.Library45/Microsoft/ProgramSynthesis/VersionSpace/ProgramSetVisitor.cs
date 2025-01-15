using System;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x020002BC RID: 700
	public abstract class ProgramSetVisitor<T>
	{
		// Token: 0x06000F3B RID: 3899
		public abstract T VisitJoin(JoinProgramSet set);

		// Token: 0x06000F3C RID: 3900
		public abstract T VisitDirect(DirectProgramSet set);

		// Token: 0x06000F3D RID: 3901
		public abstract T VisitUnion(UnionProgramSet set);
	}
}
