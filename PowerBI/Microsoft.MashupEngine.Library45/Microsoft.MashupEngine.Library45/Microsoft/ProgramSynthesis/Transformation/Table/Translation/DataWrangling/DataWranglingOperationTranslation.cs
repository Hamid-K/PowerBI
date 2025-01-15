using System;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B53 RID: 6995
	public class DataWranglingOperationTranslation : IEquatable<DataWranglingOperationTranslation>
	{
		// Token: 0x0600E5AB RID: 58795 RVA: 0x0030A714 File Offset: 0x00308914
		public DataWranglingOperationTranslation(Program program, DataWranglingOperation operation)
		{
			this.Program = program;
			this.TranslatedExpression = operation;
		}

		// Token: 0x17002646 RID: 9798
		// (get) Token: 0x0600E5AC RID: 58796 RVA: 0x0030A72A File Offset: 0x0030892A
		public Program Program { get; }

		// Token: 0x17002647 RID: 9799
		// (get) Token: 0x0600E5AD RID: 58797 RVA: 0x0030A732 File Offset: 0x00308932
		public DataWranglingOperation TranslatedExpression { get; }

		// Token: 0x0600E5AE RID: 58798 RVA: 0x0030A73A File Offset: 0x0030893A
		public override string ToString()
		{
			return this.TranslatedExpression.ToJson();
		}

		// Token: 0x0600E5AF RID: 58799 RVA: 0x0030A747 File Offset: 0x00308947
		public override bool Equals(object other)
		{
			return this.Equals(other as ITranslation<Program, DataWranglingOperation>);
		}

		// Token: 0x0600E5B0 RID: 58800 RVA: 0x0030A755 File Offset: 0x00308955
		public bool Equals(ITranslation<Program, DataWranglingOperation> other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600E5B1 RID: 58801 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600E5B2 RID: 58802 RVA: 0x0030A76D File Offset: 0x0030896D
		public bool Equals(DataWranglingOperationTranslation other)
		{
			return this == other;
		}

		// Token: 0x0600E5B3 RID: 58803 RVA: 0x0030A776 File Offset: 0x00308976
		public static bool operator ==(DataWranglingOperationTranslation left, DataWranglingOperationTranslation right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600E5B4 RID: 58804 RVA: 0x0030A78C File Offset: 0x0030898C
		public static bool operator !=(DataWranglingOperationTranslation left, DataWranglingOperationTranslation right)
		{
			return !(left == right);
		}
	}
}
