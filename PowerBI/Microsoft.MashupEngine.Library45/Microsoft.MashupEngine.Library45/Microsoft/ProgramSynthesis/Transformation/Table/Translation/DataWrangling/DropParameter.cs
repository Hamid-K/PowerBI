using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B48 RID: 6984
	public class DropParameter : IParameters, IEquatable<DropParameter>
	{
		// Token: 0x0600E53C RID: 58684 RVA: 0x00309A17 File Offset: 0x00307C17
		public DropParameter(DropCondition DropCondition)
		{
			this.DropCondition = DropCondition;
			base..ctor();
		}

		// Token: 0x17002630 RID: 9776
		// (get) Token: 0x0600E53D RID: 58685 RVA: 0x00309A26 File Offset: 0x00307C26
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DropParameter);
			}
		}

		// Token: 0x17002631 RID: 9777
		// (get) Token: 0x0600E53E RID: 58686 RVA: 0x00309A32 File Offset: 0x00307C32
		// (set) Token: 0x0600E53F RID: 58687 RVA: 0x00309A3A File Offset: 0x00307C3A
		public DropCondition DropCondition { get; set; }

		// Token: 0x0600E540 RID: 58688 RVA: 0x00309A43 File Offset: 0x00307C43
		public override string ToString()
		{
			return string.Format("DropCondition={0}", this.DropCondition);
		}

		// Token: 0x0600E541 RID: 58689 RVA: 0x00309A55 File Offset: 0x00307C55
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("DropCondition = ");
			builder.Append(this.DropCondition);
			return true;
		}

		// Token: 0x0600E542 RID: 58690 RVA: 0x00309A76 File Offset: 0x00307C76
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DropParameter left, DropParameter right)
		{
			return !(left == right);
		}

		// Token: 0x0600E543 RID: 58691 RVA: 0x00309A82 File Offset: 0x00307C82
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DropParameter left, DropParameter right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E544 RID: 58692 RVA: 0x00309A96 File Offset: 0x00307C96
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<DropCondition>.Default.GetHashCode(this.<DropCondition>k__BackingField);
		}

		// Token: 0x0600E545 RID: 58693 RVA: 0x00309ABF File Offset: 0x00307CBF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DropParameter);
		}

		// Token: 0x0600E546 RID: 58694 RVA: 0x00309ACD File Offset: 0x00307CCD
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DropParameter other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<DropCondition>.Default.Equals(this.<DropCondition>k__BackingField, other.<DropCondition>k__BackingField));
		}

		// Token: 0x0600E548 RID: 58696 RVA: 0x00309B0B File Offset: 0x00307D0B
		[CompilerGenerated]
		protected DropParameter([Nullable(1)] DropParameter original)
		{
			this.DropCondition = original.<DropCondition>k__BackingField;
		}

		// Token: 0x0600E549 RID: 58697 RVA: 0x00309B1F File Offset: 0x00307D1F
		[CompilerGenerated]
		public void Deconstruct(out DropCondition DropCondition)
		{
			DropCondition = this.DropCondition;
		}
	}
}
