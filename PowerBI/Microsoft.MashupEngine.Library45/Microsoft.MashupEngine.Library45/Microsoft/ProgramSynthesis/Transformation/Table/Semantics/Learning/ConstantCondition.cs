using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001AFE RID: 6910
	public class ConstantCondition : DropCondition, IEquatable<ConstantCondition>
	{
		// Token: 0x0600E3F2 RID: 58354 RVA: 0x00305912 File Offset: 0x00303B12
		public ConstantCondition(object ConstantValue)
		{
			this.ConstantValue = ConstantValue;
			base..ctor(DropReason.Constant);
		}

		// Token: 0x17002613 RID: 9747
		// (get) Token: 0x0600E3F3 RID: 58355 RVA: 0x00305922 File Offset: 0x00303B22
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ConstantCondition);
			}
		}

		// Token: 0x17002614 RID: 9748
		// (get) Token: 0x0600E3F4 RID: 58356 RVA: 0x0030592E File Offset: 0x00303B2E
		// (set) Token: 0x0600E3F5 RID: 58357 RVA: 0x00305936 File Offset: 0x00303B36
		public object ConstantValue { get; set; }

		// Token: 0x0600E3F6 RID: 58358 RVA: 0x00305940 File Offset: 0x00303B40
		public override string ToString()
		{
			string text = string.Format("DropReason = {0}", DropReason.Constant);
			object constantValue = this.ConstantValue;
			string text2;
			if (constantValue is DateTime)
			{
				text2 = ((DateTime)constantValue).ToString("yyyy-MM-dd HH:mm:ss");
			}
			else
			{
				object constantValue2 = this.ConstantValue;
				text2 = ((constantValue2 != null) ? constantValue2.ToString() : null);
			}
			return string.Concat(new string[] { "DropCondition { ", text, ", ConstantValue = ", text2, "}" });
		}

		// Token: 0x0600E3F7 RID: 58359 RVA: 0x003059C1 File Offset: 0x00303BC1
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ConstantValue = ");
			builder.Append(this.ConstantValue);
			return true;
		}

		// Token: 0x0600E3F8 RID: 58360 RVA: 0x003059F2 File Offset: 0x00303BF2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ConstantCondition left, ConstantCondition right)
		{
			return !(left == right);
		}

		// Token: 0x0600E3F9 RID: 58361 RVA: 0x003059FE File Offset: 0x00303BFE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ConstantCondition left, ConstantCondition right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E3FA RID: 58362 RVA: 0x00305A12 File Offset: 0x00303C12
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<object>.Default.GetHashCode(this.<ConstantValue>k__BackingField);
		}

		// Token: 0x0600E3FB RID: 58363 RVA: 0x00305A31 File Offset: 0x00303C31
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ConstantCondition);
		}

		// Token: 0x0600E3FC RID: 58364 RVA: 0x00024CEC File Offset: 0x00022EEC
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(DropCondition other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600E3FD RID: 58365 RVA: 0x00305A3F File Offset: 0x00303C3F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ConstantCondition other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<object>.Default.Equals(this.<ConstantValue>k__BackingField, other.<ConstantValue>k__BackingField));
		}

		// Token: 0x0600E3FF RID: 58367 RVA: 0x00305A70 File Offset: 0x00303C70
		[CompilerGenerated]
		protected ConstantCondition([Nullable(1)] ConstantCondition original)
			: base(original)
		{
			this.ConstantValue = original.<ConstantValue>k__BackingField;
		}

		// Token: 0x0600E400 RID: 58368 RVA: 0x00305A85 File Offset: 0x00303C85
		[CompilerGenerated]
		public void Deconstruct(out object ConstantValue)
		{
			ConstantValue = this.ConstantValue;
		}
	}
}
