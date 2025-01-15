using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B50 RID: 6992
	public class SplitParameters : IParameters, IEquatable<SplitParameters>
	{
		// Token: 0x0600E581 RID: 58753 RVA: 0x0030A216 File Offset: 0x00308416
		public SplitParameters(string Pattern, int N, bool Regex)
		{
			this.Pattern = Pattern;
			this.N = N;
			this.Regex = Regex;
			base..ctor();
		}

		// Token: 0x1700263D RID: 9789
		// (get) Token: 0x0600E582 RID: 58754 RVA: 0x0030A233 File Offset: 0x00308433
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(SplitParameters);
			}
		}

		// Token: 0x1700263E RID: 9790
		// (get) Token: 0x0600E583 RID: 58755 RVA: 0x0030A23F File Offset: 0x0030843F
		// (set) Token: 0x0600E584 RID: 58756 RVA: 0x0030A247 File Offset: 0x00308447
		public string Pattern { get; set; }

		// Token: 0x1700263F RID: 9791
		// (get) Token: 0x0600E585 RID: 58757 RVA: 0x0030A250 File Offset: 0x00308450
		// (set) Token: 0x0600E586 RID: 58758 RVA: 0x0030A258 File Offset: 0x00308458
		public int N { get; set; }

		// Token: 0x17002640 RID: 9792
		// (get) Token: 0x0600E587 RID: 58759 RVA: 0x0030A261 File Offset: 0x00308461
		// (set) Token: 0x0600E588 RID: 58760 RVA: 0x0030A269 File Offset: 0x00308469
		public bool Regex { get; set; }

		// Token: 0x0600E589 RID: 58761 RVA: 0x0030A274 File Offset: 0x00308474
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SplitParameters");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600E58A RID: 58762 RVA: 0x0030A2C0 File Offset: 0x003084C0
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Pattern = ");
			builder.Append(this.Pattern);
			builder.Append(", N = ");
			builder.Append(this.N.ToString());
			builder.Append(", Regex = ");
			builder.Append(this.Regex.ToString());
			return true;
		}

		// Token: 0x0600E58B RID: 58763 RVA: 0x0030A33A File Offset: 0x0030853A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SplitParameters left, SplitParameters right)
		{
			return !(left == right);
		}

		// Token: 0x0600E58C RID: 58764 RVA: 0x0030A346 File Offset: 0x00308546
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SplitParameters left, SplitParameters right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E58D RID: 58765 RVA: 0x0030A35C File Offset: 0x0030855C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Pattern>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<N>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Regex>k__BackingField);
		}

		// Token: 0x0600E58E RID: 58766 RVA: 0x0030A3BE File Offset: 0x003085BE
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SplitParameters);
		}

		// Token: 0x0600E58F RID: 58767 RVA: 0x0030A3CC File Offset: 0x003085CC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(SplitParameters other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Pattern>k__BackingField, other.<Pattern>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<N>k__BackingField, other.<N>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Regex>k__BackingField, other.<Regex>k__BackingField));
		}

		// Token: 0x0600E591 RID: 58769 RVA: 0x0030A445 File Offset: 0x00308645
		[CompilerGenerated]
		protected SplitParameters([Nullable(1)] SplitParameters original)
		{
			this.Pattern = original.<Pattern>k__BackingField;
			this.N = original.<N>k__BackingField;
			this.Regex = original.<Regex>k__BackingField;
		}

		// Token: 0x0600E592 RID: 58770 RVA: 0x0030A471 File Offset: 0x00308671
		[CompilerGenerated]
		public void Deconstruct(out string Pattern, out int N, out bool Regex)
		{
			Pattern = this.Pattern;
			N = this.N;
			Regex = this.Regex;
		}
	}
}
