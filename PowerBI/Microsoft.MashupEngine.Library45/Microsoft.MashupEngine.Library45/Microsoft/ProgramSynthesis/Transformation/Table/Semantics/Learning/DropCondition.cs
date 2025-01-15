using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001AFA RID: 6906
	[NullableContext(1)]
	[Nullable(0)]
	public class DropCondition : IEquatable<DropCondition>
	{
		// Token: 0x0600E3B2 RID: 58290 RVA: 0x00305294 File Offset: 0x00303494
		public DropCondition(DropReason DropReason)
		{
			this.DropReason = DropReason;
			base..ctor();
		}

		// Token: 0x17002609 RID: 9737
		// (get) Token: 0x0600E3B3 RID: 58291 RVA: 0x003052A3 File Offset: 0x003034A3
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DropCondition);
			}
		}

		// Token: 0x1700260A RID: 9738
		// (get) Token: 0x0600E3B4 RID: 58292 RVA: 0x003052AF File Offset: 0x003034AF
		// (set) Token: 0x0600E3B5 RID: 58293 RVA: 0x003052B7 File Offset: 0x003034B7
		public DropReason DropReason { get; set; }

		// Token: 0x0600E3B6 RID: 58294 RVA: 0x003052C0 File Offset: 0x003034C0
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

		// Token: 0x0600E3B7 RID: 58295 RVA: 0x0030530C File Offset: 0x0030350C
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("DropReason = ");
			builder.Append(this.DropReason.ToString());
			return true;
		}

		// Token: 0x0600E3B8 RID: 58296 RVA: 0x00305346 File Offset: 0x00303546
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DropCondition left, DropCondition right)
		{
			return !(left == right);
		}

		// Token: 0x0600E3B9 RID: 58297 RVA: 0x00305352 File Offset: 0x00303552
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DropCondition left, DropCondition right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E3BA RID: 58298 RVA: 0x00305366 File Offset: 0x00303566
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<DropReason>.Default.GetHashCode(this.<DropReason>k__BackingField);
		}

		// Token: 0x0600E3BB RID: 58299 RVA: 0x0030538F File Offset: 0x0030358F
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DropCondition);
		}

		// Token: 0x0600E3BC RID: 58300 RVA: 0x0030539D File Offset: 0x0030359D
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DropCondition other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<DropReason>.Default.Equals(this.<DropReason>k__BackingField, other.<DropReason>k__BackingField));
		}

		// Token: 0x0600E3BE RID: 58302 RVA: 0x003053DB File Offset: 0x003035DB
		[CompilerGenerated]
		protected DropCondition(DropCondition original)
		{
			this.DropReason = original.<DropReason>k__BackingField;
		}

		// Token: 0x0600E3BF RID: 58303 RVA: 0x003053EF File Offset: 0x003035EF
		[CompilerGenerated]
		public void Deconstruct(out DropReason DropReason)
		{
			DropReason = this.DropReason;
		}
	}
}
