using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput
{
	// Token: 0x02000142 RID: 322
	public class ColumnTypesMetadata : ITableMetadata, IEquatable<ColumnTypesMetadata>
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00016B57 File Offset: 0x00014D57
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ColumnTypesMetadata);
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00016B63 File Offset: 0x00014D63
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x00016B6B File Offset: 0x00014D6B
		public IReadOnlyList<string> ColumnTypes { get; set; }

		// Token: 0x0600071B RID: 1819 RVA: 0x00016B74 File Offset: 0x00014D74
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ColumnTypesMetadata");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00016BC0 File Offset: 0x00014DC0
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("ColumnTypes = ");
			builder.Append(this.ColumnTypes);
			return true;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00016BE1 File Offset: 0x00014DE1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ColumnTypesMetadata left, ColumnTypesMetadata right)
		{
			return !(left == right);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00016BED File Offset: 0x00014DED
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ColumnTypesMetadata left, ColumnTypesMetadata right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00016C01 File Offset: 0x00014E01
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ColumnTypes>k__BackingField);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00016C2A File Offset: 0x00014E2A
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ColumnTypesMetadata);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00016C38 File Offset: 0x00014E38
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ColumnTypesMetadata other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ColumnTypes>k__BackingField, other.<ColumnTypes>k__BackingField));
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00016C76 File Offset: 0x00014E76
		[CompilerGenerated]
		protected ColumnTypesMetadata([Nullable(1)] ColumnTypesMetadata original)
		{
			this.ColumnTypes = original.<ColumnTypes>k__BackingField;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00002130 File Offset: 0x00000330
		public ColumnTypesMetadata()
		{
		}
	}
}
