using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B4B RID: 6987
	[NullableContext(1)]
	[Nullable(0)]
	public class DropNaParameters : IParameters, IEquatable<DropNaParameters>
	{
		// Token: 0x0600E54A RID: 58698 RVA: 0x00309B29 File Offset: 0x00307D29
		public DropNaParameters(Axis Axis, DropNaHow? How, int? Threshold)
		{
			this.Axis = Axis;
			this.How = How;
			this.Threshold = Threshold;
			base..ctor();
		}

		// Token: 0x17002632 RID: 9778
		// (get) Token: 0x0600E54B RID: 58699 RVA: 0x00309B46 File Offset: 0x00307D46
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DropNaParameters);
			}
		}

		// Token: 0x17002633 RID: 9779
		// (get) Token: 0x0600E54C RID: 58700 RVA: 0x00309B52 File Offset: 0x00307D52
		// (set) Token: 0x0600E54D RID: 58701 RVA: 0x00309B5A File Offset: 0x00307D5A
		public Axis Axis { get; set; }

		// Token: 0x17002634 RID: 9780
		// (get) Token: 0x0600E54E RID: 58702 RVA: 0x00309B63 File Offset: 0x00307D63
		// (set) Token: 0x0600E54F RID: 58703 RVA: 0x00309B6B File Offset: 0x00307D6B
		public DropNaHow? How { get; set; }

		// Token: 0x17002635 RID: 9781
		// (get) Token: 0x0600E550 RID: 58704 RVA: 0x00309B74 File Offset: 0x00307D74
		// (set) Token: 0x0600E551 RID: 58705 RVA: 0x00309B7C File Offset: 0x00307D7C
		public int? Threshold { get; set; }

		// Token: 0x0600E552 RID: 58706 RVA: 0x00309B88 File Offset: 0x00307D88
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DropNaParameters");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600E553 RID: 58707 RVA: 0x00309BD4 File Offset: 0x00307DD4
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Axis = ");
			builder.Append(this.Axis.ToString());
			builder.Append(", How = ");
			builder.Append(this.How.ToString());
			builder.Append(", Threshold = ");
			builder.Append(this.Threshold.ToString());
			return true;
		}

		// Token: 0x0600E554 RID: 58708 RVA: 0x00309C5C File Offset: 0x00307E5C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DropNaParameters left, DropNaParameters right)
		{
			return !(left == right);
		}

		// Token: 0x0600E555 RID: 58709 RVA: 0x00309C68 File Offset: 0x00307E68
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DropNaParameters left, DropNaParameters right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E556 RID: 58710 RVA: 0x00309C7C File Offset: 0x00307E7C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Axis>.Default.GetHashCode(this.<Axis>k__BackingField)) * -1521134295 + EqualityComparer<DropNaHow?>.Default.GetHashCode(this.<How>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<Threshold>k__BackingField);
		}

		// Token: 0x0600E557 RID: 58711 RVA: 0x00309CDE File Offset: 0x00307EDE
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DropNaParameters);
		}

		// Token: 0x0600E558 RID: 58712 RVA: 0x00309CEC File Offset: 0x00307EEC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DropNaParameters other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Axis>.Default.Equals(this.<Axis>k__BackingField, other.<Axis>k__BackingField) && EqualityComparer<DropNaHow?>.Default.Equals(this.<How>k__BackingField, other.<How>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<Threshold>k__BackingField, other.<Threshold>k__BackingField));
		}

		// Token: 0x0600E55A RID: 58714 RVA: 0x00309D65 File Offset: 0x00307F65
		[CompilerGenerated]
		protected DropNaParameters(DropNaParameters original)
		{
			this.Axis = original.<Axis>k__BackingField;
			this.How = original.<How>k__BackingField;
			this.Threshold = original.<Threshold>k__BackingField;
		}

		// Token: 0x0600E55B RID: 58715 RVA: 0x00309D91 File Offset: 0x00307F91
		[CompilerGenerated]
		public void Deconstruct(out Axis Axis, out DropNaHow? How, out int? Threshold)
		{
			Axis = this.Axis;
			How = this.How;
			Threshold = this.Threshold;
		}
	}
}
