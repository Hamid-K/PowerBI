using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B44 RID: 6980
	[NullableContext(1)]
	[Nullable(0)]
	public class MissingCondition : DropCondition, IEquatable<MissingCondition>
	{
		// Token: 0x0600E50C RID: 58636 RVA: 0x00309555 File Offset: 0x00307755
		public MissingCondition(double MissingValueFraction, MissingValueType MissingValueTypes)
		{
			this.MissingValueFraction = MissingValueFraction;
			this.MissingValueTypes = MissingValueTypes;
			base..ctor(DropReason.Empty);
		}

		// Token: 0x17002629 RID: 9769
		// (get) Token: 0x0600E50D RID: 58637 RVA: 0x0030956C File Offset: 0x0030776C
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MissingCondition);
			}
		}

		// Token: 0x1700262A RID: 9770
		// (get) Token: 0x0600E50E RID: 58638 RVA: 0x00309578 File Offset: 0x00307778
		// (set) Token: 0x0600E50F RID: 58639 RVA: 0x00309580 File Offset: 0x00307780
		public double MissingValueFraction { get; set; }

		// Token: 0x1700262B RID: 9771
		// (get) Token: 0x0600E510 RID: 58640 RVA: 0x00309589 File Offset: 0x00307789
		// (set) Token: 0x0600E511 RID: 58641 RVA: 0x00309591 File Offset: 0x00307791
		public MissingValueType MissingValueTypes { get; set; }

		// Token: 0x0600E512 RID: 58642 RVA: 0x0030959C File Offset: 0x0030779C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MissingCondition");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600E513 RID: 58643 RVA: 0x003095E8 File Offset: 0x003077E8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MissingValueFraction = ");
			builder.Append(this.MissingValueFraction.ToString());
			builder.Append(", MissingValueTypes = ");
			builder.Append(this.MissingValueTypes.ToString());
			return true;
		}

		// Token: 0x0600E514 RID: 58644 RVA: 0x00309659 File Offset: 0x00307859
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MissingCondition left, MissingCondition right)
		{
			return !(left == right);
		}

		// Token: 0x0600E515 RID: 58645 RVA: 0x00309665 File Offset: 0x00307865
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MissingCondition left, MissingCondition right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E516 RID: 58646 RVA: 0x00309679 File Offset: 0x00307879
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<MissingValueFraction>k__BackingField)) * -1521134295 + EqualityComparer<MissingValueType>.Default.GetHashCode(this.<MissingValueTypes>k__BackingField);
		}

		// Token: 0x0600E517 RID: 58647 RVA: 0x003096AF File Offset: 0x003078AF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MissingCondition);
		}

		// Token: 0x0600E518 RID: 58648 RVA: 0x00024CEC File Offset: 0x00022EEC
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(DropCondition other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600E519 RID: 58649 RVA: 0x003096C0 File Offset: 0x003078C0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MissingCondition other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<double>.Default.Equals(this.<MissingValueFraction>k__BackingField, other.<MissingValueFraction>k__BackingField) && EqualityComparer<MissingValueType>.Default.Equals(this.<MissingValueTypes>k__BackingField, other.<MissingValueTypes>k__BackingField));
		}

		// Token: 0x0600E51B RID: 58651 RVA: 0x00309714 File Offset: 0x00307914
		[CompilerGenerated]
		protected MissingCondition(MissingCondition original)
			: base(original)
		{
			this.MissingValueFraction = original.<MissingValueFraction>k__BackingField;
			this.MissingValueTypes = original.<MissingValueTypes>k__BackingField;
		}

		// Token: 0x0600E51C RID: 58652 RVA: 0x00309735 File Offset: 0x00307935
		[CompilerGenerated]
		public void Deconstruct(out double MissingValueFraction, out MissingValueType MissingValueTypes)
		{
			MissingValueFraction = this.MissingValueFraction;
			MissingValueTypes = this.MissingValueTypes;
		}
	}
}
