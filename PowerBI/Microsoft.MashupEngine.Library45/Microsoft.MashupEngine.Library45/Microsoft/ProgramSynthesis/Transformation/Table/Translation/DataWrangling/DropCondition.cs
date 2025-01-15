using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B43 RID: 6979
	[NullableContext(1)]
	[Nullable(0)]
	public class DropCondition : IEquatable<DropCondition>
	{
		// Token: 0x0600E4FE RID: 58622 RVA: 0x003093ED File Offset: 0x003075ED
		public DropCondition(DropReason DropReason)
		{
			this.DropReason = DropReason;
			base..ctor();
		}

		// Token: 0x17002627 RID: 9767
		// (get) Token: 0x0600E4FF RID: 58623 RVA: 0x003093FC File Offset: 0x003075FC
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DropCondition);
			}
		}

		// Token: 0x17002628 RID: 9768
		// (get) Token: 0x0600E500 RID: 58624 RVA: 0x00309408 File Offset: 0x00307608
		// (set) Token: 0x0600E501 RID: 58625 RVA: 0x00309410 File Offset: 0x00307610
		public DropReason DropReason { get; set; }

		// Token: 0x0600E502 RID: 58626 RVA: 0x0030941C File Offset: 0x0030761C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DropCondition");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600E503 RID: 58627 RVA: 0x00309468 File Offset: 0x00307668
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("DropReason = ");
			builder.Append(this.DropReason.ToString());
			return true;
		}

		// Token: 0x0600E504 RID: 58628 RVA: 0x003094A2 File Offset: 0x003076A2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DropCondition left, DropCondition right)
		{
			return !(left == right);
		}

		// Token: 0x0600E505 RID: 58629 RVA: 0x003094AE File Offset: 0x003076AE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DropCondition left, DropCondition right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E506 RID: 58630 RVA: 0x003094C2 File Offset: 0x003076C2
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<DropReason>.Default.GetHashCode(this.<DropReason>k__BackingField);
		}

		// Token: 0x0600E507 RID: 58631 RVA: 0x003094EB File Offset: 0x003076EB
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DropCondition);
		}

		// Token: 0x0600E508 RID: 58632 RVA: 0x003094F9 File Offset: 0x003076F9
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DropCondition other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<DropReason>.Default.Equals(this.<DropReason>k__BackingField, other.<DropReason>k__BackingField));
		}

		// Token: 0x0600E50A RID: 58634 RVA: 0x00309537 File Offset: 0x00307737
		[CompilerGenerated]
		protected DropCondition(DropCondition original)
		{
			this.DropReason = original.<DropReason>k__BackingField;
		}

		// Token: 0x0600E50B RID: 58635 RVA: 0x0030954B File Offset: 0x0030774B
		[CompilerGenerated]
		public void Deconstruct(out DropReason DropReason)
		{
			DropReason = this.DropReason;
		}
	}
}
