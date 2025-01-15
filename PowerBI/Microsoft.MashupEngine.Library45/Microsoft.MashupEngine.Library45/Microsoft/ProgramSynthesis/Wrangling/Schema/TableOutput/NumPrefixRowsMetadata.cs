using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput
{
	// Token: 0x02000143 RID: 323
	[NullableContext(1)]
	[Nullable(0)]
	public class NumPrefixRowsMetadata : ITableMetadata, IEquatable<NumPrefixRowsMetadata>
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00016C8A File Offset: 0x00014E8A
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(NumPrefixRowsMetadata);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00016C96 File Offset: 0x00014E96
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x00016C9E File Offset: 0x00014E9E
		public int NumPrefixRows { get; set; }

		// Token: 0x06000728 RID: 1832 RVA: 0x00016CA8 File Offset: 0x00014EA8
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NumPrefixRowsMetadata");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00016CF4 File Offset: 0x00014EF4
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("NumPrefixRows = ");
			builder.Append(this.NumPrefixRows.ToString());
			return true;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00016D2E File Offset: 0x00014F2E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(NumPrefixRowsMetadata left, NumPrefixRowsMetadata right)
		{
			return !(left == right);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00016D3A File Offset: 0x00014F3A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(NumPrefixRowsMetadata left, NumPrefixRowsMetadata right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00016D4E File Offset: 0x00014F4E
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<NumPrefixRows>k__BackingField);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00016D77 File Offset: 0x00014F77
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NumPrefixRowsMetadata);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00016D85 File Offset: 0x00014F85
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(NumPrefixRowsMetadata other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<NumPrefixRows>k__BackingField, other.<NumPrefixRows>k__BackingField));
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00016DC3 File Offset: 0x00014FC3
		[CompilerGenerated]
		protected NumPrefixRowsMetadata(NumPrefixRowsMetadata original)
		{
			this.NumPrefixRows = original.<NumPrefixRows>k__BackingField;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00002130 File Offset: 0x00000330
		public NumPrefixRowsMetadata()
		{
		}
	}
}
