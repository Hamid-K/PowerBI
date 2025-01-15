using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree
{
	// Token: 0x020000EF RID: 239
	public abstract class NodeVisitor<T>
	{
		// Token: 0x0600057B RID: 1403
		public abstract T VisitStruct(StructNode node);

		// Token: 0x0600057C RID: 1404
		public abstract T VisitSequence(SequenceNode node);
	}
}
