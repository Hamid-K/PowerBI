using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001AFB RID: 6907
	[NullableContext(1)]
	[Nullable(0)]
	public class MissingCondition : DropCondition, IEquatable<MissingCondition>
	{
		// Token: 0x0600E3C0 RID: 58304 RVA: 0x003053F9 File Offset: 0x003035F9
		public MissingCondition(double MissingValueFraction, MissingValueType MissingValueTypes)
		{
			this.MissingValueFraction = MissingValueFraction;
			this.MissingValueTypes = MissingValueTypes;
			base..ctor(DropReason.Empty);
		}

		// Token: 0x1700260B RID: 9739
		// (get) Token: 0x0600E3C1 RID: 58305 RVA: 0x00305410 File Offset: 0x00303610
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(MissingCondition);
			}
		}

		// Token: 0x1700260C RID: 9740
		// (get) Token: 0x0600E3C2 RID: 58306 RVA: 0x0030541C File Offset: 0x0030361C
		// (set) Token: 0x0600E3C3 RID: 58307 RVA: 0x00305424 File Offset: 0x00303624
		public double MissingValueFraction { get; set; }

		// Token: 0x1700260D RID: 9741
		// (get) Token: 0x0600E3C4 RID: 58308 RVA: 0x0030542D File Offset: 0x0030362D
		// (set) Token: 0x0600E3C5 RID: 58309 RVA: 0x00305435 File Offset: 0x00303635
		public MissingValueType MissingValueTypes { get; set; }

		// Token: 0x0600E3C6 RID: 58310 RVA: 0x00305440 File Offset: 0x00303640
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

		// Token: 0x0600E3C7 RID: 58311 RVA: 0x0030548C File Offset: 0x0030368C
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

		// Token: 0x0600E3C8 RID: 58312 RVA: 0x003054FD File Offset: 0x003036FD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MissingCondition left, MissingCondition right)
		{
			return !(left == right);
		}

		// Token: 0x0600E3C9 RID: 58313 RVA: 0x00305509 File Offset: 0x00303709
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MissingCondition left, MissingCondition right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E3CA RID: 58314 RVA: 0x0030551D File Offset: 0x0030371D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<MissingValueFraction>k__BackingField)) * -1521134295 + EqualityComparer<MissingValueType>.Default.GetHashCode(this.<MissingValueTypes>k__BackingField);
		}

		// Token: 0x0600E3CB RID: 58315 RVA: 0x00305553 File Offset: 0x00303753
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MissingCondition);
		}

		// Token: 0x0600E3CC RID: 58316 RVA: 0x00024CEC File Offset: 0x00022EEC
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(DropCondition other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600E3CD RID: 58317 RVA: 0x00305564 File Offset: 0x00303764
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MissingCondition other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<double>.Default.Equals(this.<MissingValueFraction>k__BackingField, other.<MissingValueFraction>k__BackingField) && EqualityComparer<MissingValueType>.Default.Equals(this.<MissingValueTypes>k__BackingField, other.<MissingValueTypes>k__BackingField));
		}

		// Token: 0x0600E3CF RID: 58319 RVA: 0x003055B8 File Offset: 0x003037B8
		[CompilerGenerated]
		protected MissingCondition(MissingCondition original)
			: base(original)
		{
			this.MissingValueFraction = original.<MissingValueFraction>k__BackingField;
			this.MissingValueTypes = original.<MissingValueTypes>k__BackingField;
		}

		// Token: 0x0600E3D0 RID: 58320 RVA: 0x003055D9 File Offset: 0x003037D9
		[CompilerGenerated]
		public void Deconstruct(out double MissingValueFraction, out MissingValueType MissingValueTypes)
		{
			MissingValueFraction = this.MissingValueFraction;
			MissingValueTypes = this.MissingValueTypes;
		}
	}
}
