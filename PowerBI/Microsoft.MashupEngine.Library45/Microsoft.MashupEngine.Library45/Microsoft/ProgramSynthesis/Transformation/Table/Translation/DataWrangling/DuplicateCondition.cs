using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B45 RID: 6981
	public class DuplicateCondition : DropCondition, IEquatable<DuplicateCondition>
	{
		// Token: 0x0600E51D RID: 58653 RVA: 0x00309747 File Offset: 0x00307947
		public DuplicateCondition(string OriginalValueId)
		{
			this.OriginalValueId = OriginalValueId;
			base..ctor(DropReason.Duplicate);
		}

		// Token: 0x1700262C RID: 9772
		// (get) Token: 0x0600E51E RID: 58654 RVA: 0x00309757 File Offset: 0x00307957
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DuplicateCondition);
			}
		}

		// Token: 0x1700262D RID: 9773
		// (get) Token: 0x0600E51F RID: 58655 RVA: 0x00309763 File Offset: 0x00307963
		// (set) Token: 0x0600E520 RID: 58656 RVA: 0x0030976B File Offset: 0x0030796B
		public string OriginalValueId { get; set; }

		// Token: 0x0600E521 RID: 58657 RVA: 0x00309774 File Offset: 0x00307974
		public DuplicateCondition()
			: this(null)
		{
		}

		// Token: 0x0600E522 RID: 58658 RVA: 0x00309780 File Offset: 0x00307980
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DuplicateCondition");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600E523 RID: 58659 RVA: 0x003097CC File Offset: 0x003079CC
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("OriginalValueId = ");
			builder.Append(this.OriginalValueId);
			return true;
		}

		// Token: 0x0600E524 RID: 58660 RVA: 0x003097FD File Offset: 0x003079FD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DuplicateCondition left, DuplicateCondition right)
		{
			return !(left == right);
		}

		// Token: 0x0600E525 RID: 58661 RVA: 0x00309809 File Offset: 0x00307A09
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DuplicateCondition left, DuplicateCondition right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E526 RID: 58662 RVA: 0x0030981D File Offset: 0x00307A1D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<OriginalValueId>k__BackingField);
		}

		// Token: 0x0600E527 RID: 58663 RVA: 0x0030983C File Offset: 0x00307A3C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DuplicateCondition);
		}

		// Token: 0x0600E528 RID: 58664 RVA: 0x00024CEC File Offset: 0x00022EEC
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(DropCondition other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600E529 RID: 58665 RVA: 0x0030984A File Offset: 0x00307A4A
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DuplicateCondition other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<OriginalValueId>k__BackingField, other.<OriginalValueId>k__BackingField));
		}

		// Token: 0x0600E52B RID: 58667 RVA: 0x0030987B File Offset: 0x00307A7B
		[CompilerGenerated]
		protected DuplicateCondition([Nullable(1)] DuplicateCondition original)
			: base(original)
		{
			this.OriginalValueId = original.<OriginalValueId>k__BackingField;
		}

		// Token: 0x0600E52C RID: 58668 RVA: 0x00309890 File Offset: 0x00307A90
		[CompilerGenerated]
		public void Deconstruct(out string OriginalValueId)
		{
			OriginalValueId = this.OriginalValueId;
		}
	}
}
