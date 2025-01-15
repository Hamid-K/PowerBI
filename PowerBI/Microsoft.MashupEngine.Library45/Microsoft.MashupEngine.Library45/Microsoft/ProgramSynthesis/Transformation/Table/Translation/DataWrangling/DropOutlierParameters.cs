using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B4C RID: 6988
	public class DropOutlierParameters : IParameters, IEquatable<DropOutlierParameters>
	{
		// Token: 0x0600E55C RID: 58716 RVA: 0x00309DB3 File Offset: 0x00307FB3
		public DropOutlierParameters(string SourceColumnName, Tuple<double, double> ValidBoundExclusive)
		{
			this.SourceColumnName = SourceColumnName;
			this.ValidBoundExclusive = ValidBoundExclusive;
			base..ctor();
		}

		// Token: 0x17002636 RID: 9782
		// (get) Token: 0x0600E55D RID: 58717 RVA: 0x00309DC9 File Offset: 0x00307FC9
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DropOutlierParameters);
			}
		}

		// Token: 0x17002637 RID: 9783
		// (get) Token: 0x0600E55E RID: 58718 RVA: 0x00309DD5 File Offset: 0x00307FD5
		// (set) Token: 0x0600E55F RID: 58719 RVA: 0x00309DDD File Offset: 0x00307FDD
		public string SourceColumnName { get; set; }

		// Token: 0x17002638 RID: 9784
		// (get) Token: 0x0600E560 RID: 58720 RVA: 0x00309DE6 File Offset: 0x00307FE6
		// (set) Token: 0x0600E561 RID: 58721 RVA: 0x00309DEE File Offset: 0x00307FEE
		public Tuple<double, double> ValidBoundExclusive { get; set; }

		// Token: 0x0600E562 RID: 58722 RVA: 0x00309DF8 File Offset: 0x00307FF8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DropOutlierParameters");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600E563 RID: 58723 RVA: 0x00309E44 File Offset: 0x00308044
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("SourceColumnName = ");
			builder.Append(this.SourceColumnName);
			builder.Append(", ValidBoundExclusive = ");
			builder.Append(this.ValidBoundExclusive);
			return true;
		}

		// Token: 0x0600E564 RID: 58724 RVA: 0x00309E7E File Offset: 0x0030807E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DropOutlierParameters left, DropOutlierParameters right)
		{
			return !(left == right);
		}

		// Token: 0x0600E565 RID: 58725 RVA: 0x00309E8A File Offset: 0x0030808A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DropOutlierParameters left, DropOutlierParameters right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E566 RID: 58726 RVA: 0x00309E9E File Offset: 0x0030809E
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SourceColumnName>k__BackingField)) * -1521134295 + EqualityComparer<Tuple<double, double>>.Default.GetHashCode(this.<ValidBoundExclusive>k__BackingField);
		}

		// Token: 0x0600E567 RID: 58727 RVA: 0x00309EDE File Offset: 0x003080DE
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DropOutlierParameters);
		}

		// Token: 0x0600E568 RID: 58728 RVA: 0x00309EEC File Offset: 0x003080EC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DropOutlierParameters other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<SourceColumnName>k__BackingField, other.<SourceColumnName>k__BackingField) && EqualityComparer<Tuple<double, double>>.Default.Equals(this.<ValidBoundExclusive>k__BackingField, other.<ValidBoundExclusive>k__BackingField));
		}

		// Token: 0x0600E56A RID: 58730 RVA: 0x00309F4D File Offset: 0x0030814D
		[CompilerGenerated]
		protected DropOutlierParameters([Nullable(1)] DropOutlierParameters original)
		{
			this.SourceColumnName = original.<SourceColumnName>k__BackingField;
			this.ValidBoundExclusive = original.<ValidBoundExclusive>k__BackingField;
		}

		// Token: 0x0600E56B RID: 58731 RVA: 0x00309F6D File Offset: 0x0030816D
		[CompilerGenerated]
		public void Deconstruct(out string SourceColumnName, out Tuple<double, double> ValidBoundExclusive)
		{
			SourceColumnName = this.SourceColumnName;
			ValidBoundExclusive = this.ValidBoundExclusive;
		}
	}
}
