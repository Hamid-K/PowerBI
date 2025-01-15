using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation
{
	// Token: 0x02001B31 RID: 6961
	public class Metadata : IEquatable<Metadata>
	{
		// Token: 0x0600E4A6 RID: 58534 RVA: 0x00307AC0 File Offset: 0x00305CC0
		public Metadata(IEnumerable<string> OutputColumnNames)
		{
			this.OutputColumnNames = OutputColumnNames;
			base..ctor();
		}

		// Token: 0x17002615 RID: 9749
		// (get) Token: 0x0600E4A7 RID: 58535 RVA: 0x00307ACF File Offset: 0x00305CCF
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(Metadata);
			}
		}

		// Token: 0x17002616 RID: 9750
		// (get) Token: 0x0600E4A8 RID: 58536 RVA: 0x00307ADB File Offset: 0x00305CDB
		// (set) Token: 0x0600E4A9 RID: 58537 RVA: 0x00307AE3 File Offset: 0x00305CE3
		public IEnumerable<string> OutputColumnNames { get; set; }

		// Token: 0x0600E4AA RID: 58538 RVA: 0x00307AEC File Offset: 0x00305CEC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Metadata");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600E4AB RID: 58539 RVA: 0x00307B38 File Offset: 0x00305D38
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("OutputColumnNames = ");
			builder.Append(this.OutputColumnNames);
			return true;
		}

		// Token: 0x0600E4AC RID: 58540 RVA: 0x00307B59 File Offset: 0x00305D59
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(Metadata left, Metadata right)
		{
			return !(left == right);
		}

		// Token: 0x0600E4AD RID: 58541 RVA: 0x00307B65 File Offset: 0x00305D65
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(Metadata left, Metadata right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E4AE RID: 58542 RVA: 0x00307B79 File Offset: 0x00305D79
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IEnumerable<string>>.Default.GetHashCode(this.<OutputColumnNames>k__BackingField);
		}

		// Token: 0x0600E4AF RID: 58543 RVA: 0x00307BA2 File Offset: 0x00305DA2
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Metadata);
		}

		// Token: 0x0600E4B0 RID: 58544 RVA: 0x00307BB0 File Offset: 0x00305DB0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(Metadata other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IEnumerable<string>>.Default.Equals(this.<OutputColumnNames>k__BackingField, other.<OutputColumnNames>k__BackingField));
		}

		// Token: 0x0600E4B2 RID: 58546 RVA: 0x00307BEE File Offset: 0x00305DEE
		[CompilerGenerated]
		protected Metadata([Nullable(1)] Metadata original)
		{
			this.OutputColumnNames = original.<OutputColumnNames>k__BackingField;
		}

		// Token: 0x0600E4B3 RID: 58547 RVA: 0x00307C02 File Offset: 0x00305E02
		[CompilerGenerated]
		public void Deconstruct(out IEnumerable<string> OutputColumnNames)
		{
			OutputColumnNames = this.OutputColumnNames;
		}
	}
}
