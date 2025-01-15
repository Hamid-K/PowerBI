using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Split.Translation
{
	// Token: 0x020013FF RID: 5119
	public class Metadata : IEquatable<Metadata>
	{
		// Token: 0x06009E19 RID: 40473 RVA: 0x0021886E File Offset: 0x00216A6E
		public Metadata(IEnumerable<string> OutputColumnNames)
		{
			this.OutputColumnNames = OutputColumnNames;
			base..ctor();
		}

		// Token: 0x17001AC6 RID: 6854
		// (get) Token: 0x06009E1A RID: 40474 RVA: 0x0021887D File Offset: 0x00216A7D
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

		// Token: 0x17001AC7 RID: 6855
		// (get) Token: 0x06009E1B RID: 40475 RVA: 0x00218889 File Offset: 0x00216A89
		// (set) Token: 0x06009E1C RID: 40476 RVA: 0x00218891 File Offset: 0x00216A91
		public IEnumerable<string> OutputColumnNames { get; set; }

		// Token: 0x06009E1D RID: 40477 RVA: 0x0021889C File Offset: 0x00216A9C
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

		// Token: 0x06009E1E RID: 40478 RVA: 0x002188E8 File Offset: 0x00216AE8
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("OutputColumnNames = ");
			builder.Append(this.OutputColumnNames);
			return true;
		}

		// Token: 0x06009E1F RID: 40479 RVA: 0x00218909 File Offset: 0x00216B09
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(Metadata left, Metadata right)
		{
			return !(left == right);
		}

		// Token: 0x06009E20 RID: 40480 RVA: 0x00218915 File Offset: 0x00216B15
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(Metadata left, Metadata right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06009E21 RID: 40481 RVA: 0x00218929 File Offset: 0x00216B29
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IEnumerable<string>>.Default.GetHashCode(this.<OutputColumnNames>k__BackingField);
		}

		// Token: 0x06009E22 RID: 40482 RVA: 0x00218952 File Offset: 0x00216B52
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Metadata);
		}

		// Token: 0x06009E23 RID: 40483 RVA: 0x00218960 File Offset: 0x00216B60
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(Metadata other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IEnumerable<string>>.Default.Equals(this.<OutputColumnNames>k__BackingField, other.<OutputColumnNames>k__BackingField));
		}

		// Token: 0x06009E25 RID: 40485 RVA: 0x0021899E File Offset: 0x00216B9E
		[CompilerGenerated]
		protected Metadata([Nullable(1)] Metadata original)
		{
			this.OutputColumnNames = original.<OutputColumnNames>k__BackingField;
		}

		// Token: 0x06009E26 RID: 40486 RVA: 0x002189B2 File Offset: 0x00216BB2
		[CompilerGenerated]
		public void Deconstruct(out IEnumerable<string> OutputColumnNames)
		{
			OutputColumnNames = this.OutputColumnNames;
		}
	}
}
