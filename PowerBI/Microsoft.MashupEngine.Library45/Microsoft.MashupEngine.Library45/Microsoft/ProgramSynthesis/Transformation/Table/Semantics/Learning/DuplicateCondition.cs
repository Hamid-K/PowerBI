using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001AFD RID: 6909
	public class DuplicateCondition : DropCondition, IEquatable<DuplicateCondition>
	{
		// Token: 0x0600E3E2 RID: 58338 RVA: 0x003057BF File Offset: 0x003039BF
		public DuplicateCondition(string OriginalValueId)
		{
			this.OriginalValueId = OriginalValueId;
			base..ctor(DropReason.Duplicate);
		}

		// Token: 0x17002611 RID: 9745
		// (get) Token: 0x0600E3E3 RID: 58339 RVA: 0x003057CF File Offset: 0x003039CF
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

		// Token: 0x17002612 RID: 9746
		// (get) Token: 0x0600E3E4 RID: 58340 RVA: 0x003057DB File Offset: 0x003039DB
		// (set) Token: 0x0600E3E5 RID: 58341 RVA: 0x003057E3 File Offset: 0x003039E3
		public string OriginalValueId { get; set; }

		// Token: 0x0600E3E6 RID: 58342 RVA: 0x003057EC File Offset: 0x003039EC
		public DuplicateCondition()
			: this(null)
		{
		}

		// Token: 0x0600E3E7 RID: 58343 RVA: 0x003057F8 File Offset: 0x003039F8
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

		// Token: 0x0600E3E8 RID: 58344 RVA: 0x00305844 File Offset: 0x00303A44
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

		// Token: 0x0600E3E9 RID: 58345 RVA: 0x00305875 File Offset: 0x00303A75
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DuplicateCondition left, DuplicateCondition right)
		{
			return !(left == right);
		}

		// Token: 0x0600E3EA RID: 58346 RVA: 0x00305881 File Offset: 0x00303A81
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DuplicateCondition left, DuplicateCondition right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E3EB RID: 58347 RVA: 0x00305895 File Offset: 0x00303A95
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<OriginalValueId>k__BackingField);
		}

		// Token: 0x0600E3EC RID: 58348 RVA: 0x003058B4 File Offset: 0x00303AB4
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DuplicateCondition);
		}

		// Token: 0x0600E3ED RID: 58349 RVA: 0x00024CEC File Offset: 0x00022EEC
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(DropCondition other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600E3EE RID: 58350 RVA: 0x003058C2 File Offset: 0x00303AC2
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DuplicateCondition other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<OriginalValueId>k__BackingField, other.<OriginalValueId>k__BackingField));
		}

		// Token: 0x0600E3F0 RID: 58352 RVA: 0x003058F3 File Offset: 0x00303AF3
		[CompilerGenerated]
		protected DuplicateCondition([Nullable(1)] DuplicateCondition original)
			: base(original)
		{
			this.OriginalValueId = original.<OriginalValueId>k__BackingField;
		}

		// Token: 0x0600E3F1 RID: 58353 RVA: 0x00305908 File Offset: 0x00303B08
		[CompilerGenerated]
		public void Deconstruct(out string OriginalValueId)
		{
			OriginalValueId = this.OriginalValueId;
		}
	}
}
