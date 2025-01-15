using System;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001E1 RID: 481
	public abstract class EntityToken : StringToken, IEquatable<EntityToken>
	{
		// Token: 0x06000A77 RID: 2679 RVA: 0x0001FEA6 File Offset: 0x0001E0A6
		protected EntityToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A78 RID: 2680
		public abstract double ScoreMultiplier { get; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A79 RID: 2681
		public abstract string EntityName { get; }

		// Token: 0x06000A7A RID: 2682
		public abstract bool Equals(EntityToken other);

		// Token: 0x06000A7B RID: 2683
		public abstract void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false);

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001FEB1 File Offset: 0x0001E0B1
		public virtual bool ValueBasedEquality(EntityToken other)
		{
			return other == this || (other != null && base.GetType() == other.GetType() && base.Value == other.Value);
		}

		// Token: 0x06000A7D RID: 2685
		public abstract override int GetHashCode();

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001FEE4 File Offset: 0x0001E0E4
		public virtual int ValueBasedHashCode()
		{
			return base.GetType().GetHashCode() ^ (base.Value.GetHashCode() * 1217);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001FF03 File Offset: 0x0001E103
		public override string ToString()
		{
			return base.Value;
		}
	}
}
