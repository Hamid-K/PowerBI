using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B51 RID: 6993
	[NullableContext(1)]
	[Nullable(0)]
	public class CustomSplitParameters : IParameters, IEquatable<CustomSplitParameters>
	{
		// Token: 0x0600E593 RID: 58771 RVA: 0x00002130 File Offset: 0x00000330
		public CustomSplitParameters()
		{
		}

		// Token: 0x17002641 RID: 9793
		// (get) Token: 0x0600E594 RID: 58772 RVA: 0x0030A48B File Offset: 0x0030868B
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(CustomSplitParameters);
			}
		}

		// Token: 0x0600E595 RID: 58773 RVA: 0x0030A498 File Offset: 0x00308698
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CustomSplitParameters");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600E596 RID: 58774 RVA: 0x0000FA11 File Offset: 0x0000DC11
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			return false;
		}

		// Token: 0x0600E597 RID: 58775 RVA: 0x0030A4E4 File Offset: 0x003086E4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CustomSplitParameters left, CustomSplitParameters right)
		{
			return !(left == right);
		}

		// Token: 0x0600E598 RID: 58776 RVA: 0x0030A4F0 File Offset: 0x003086F0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CustomSplitParameters left, CustomSplitParameters right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E599 RID: 58777 RVA: 0x0030A504 File Offset: 0x00308704
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
		}

		// Token: 0x0600E59A RID: 58778 RVA: 0x0030A516 File Offset: 0x00308716
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CustomSplitParameters);
		}

		// Token: 0x0600E59B RID: 58779 RVA: 0x0030A524 File Offset: 0x00308724
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CustomSplitParameters other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract);
		}

		// Token: 0x0600E59D RID: 58781 RVA: 0x00002130 File Offset: 0x00000330
		[CompilerGenerated]
		protected CustomSplitParameters(CustomSplitParameters original)
		{
		}
	}
}
