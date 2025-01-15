using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B46 RID: 6982
	public class ConstantCondition : DropCondition, IEquatable<ConstantCondition>
	{
		// Token: 0x0600E52D RID: 58669 RVA: 0x0030989A File Offset: 0x00307A9A
		public ConstantCondition(object ConstantValue)
		{
			this.ConstantValue = ConstantValue;
			base..ctor(DropReason.Constant);
		}

		// Token: 0x1700262E RID: 9774
		// (get) Token: 0x0600E52E RID: 58670 RVA: 0x003098AA File Offset: 0x00307AAA
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

		// Token: 0x1700262F RID: 9775
		// (get) Token: 0x0600E52F RID: 58671 RVA: 0x003098B6 File Offset: 0x00307AB6
		// (set) Token: 0x0600E530 RID: 58672 RVA: 0x003098BE File Offset: 0x00307ABE
		public object ConstantValue { get; set; }

		// Token: 0x0600E531 RID: 58673 RVA: 0x003098C8 File Offset: 0x00307AC8
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
			return string.Concat(new string[] { "ConstantCondition { ", text, ", ConstantValue = ", text2, "}" });
		}

		// Token: 0x0600E532 RID: 58674 RVA: 0x00309949 File Offset: 0x00307B49
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

		// Token: 0x0600E533 RID: 58675 RVA: 0x0030997A File Offset: 0x00307B7A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ConstantCondition left, ConstantCondition right)
		{
			return !(left == right);
		}

		// Token: 0x0600E534 RID: 58676 RVA: 0x00309986 File Offset: 0x00307B86
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ConstantCondition left, ConstantCondition right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E535 RID: 58677 RVA: 0x0030999A File Offset: 0x00307B9A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<object>.Default.GetHashCode(this.<ConstantValue>k__BackingField);
		}

		// Token: 0x0600E536 RID: 58678 RVA: 0x003099B9 File Offset: 0x00307BB9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ConstantCondition);
		}

		// Token: 0x0600E537 RID: 58679 RVA: 0x00024CEC File Offset: 0x00022EEC
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(DropCondition other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600E538 RID: 58680 RVA: 0x003099C7 File Offset: 0x00307BC7
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ConstantCondition other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<object>.Default.Equals(this.<ConstantValue>k__BackingField, other.<ConstantValue>k__BackingField));
		}

		// Token: 0x0600E53A RID: 58682 RVA: 0x003099F8 File Offset: 0x00307BF8
		[CompilerGenerated]
		protected ConstantCondition([Nullable(1)] ConstantCondition original)
			: base(original)
		{
			this.ConstantValue = original.<ConstantValue>k__BackingField;
		}

		// Token: 0x0600E53B RID: 58683 RVA: 0x00309A0D File Offset: 0x00307C0D
		[CompilerGenerated]
		public void Deconstruct(out object ConstantValue)
		{
			ConstantValue = this.ConstantValue;
		}
	}
}
