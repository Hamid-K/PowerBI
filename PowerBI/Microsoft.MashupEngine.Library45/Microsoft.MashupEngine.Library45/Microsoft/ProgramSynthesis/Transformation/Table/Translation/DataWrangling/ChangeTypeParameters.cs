using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B41 RID: 6977
	[NullableContext(1)]
	[Nullable(0)]
	public class ChangeTypeParameters : IParameters, IEquatable<ChangeTypeParameters>
	{
		// Token: 0x0600E4F0 RID: 58608 RVA: 0x00309285 File Offset: 0x00307485
		public ChangeTypeParameters(DataType DataType)
		{
			this.DataType = DataType;
			base..ctor();
		}

		// Token: 0x17002625 RID: 9765
		// (get) Token: 0x0600E4F1 RID: 58609 RVA: 0x00309294 File Offset: 0x00307494
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ChangeTypeParameters);
			}
		}

		// Token: 0x17002626 RID: 9766
		// (get) Token: 0x0600E4F2 RID: 58610 RVA: 0x003092A0 File Offset: 0x003074A0
		// (set) Token: 0x0600E4F3 RID: 58611 RVA: 0x003092A8 File Offset: 0x003074A8
		public DataType DataType { get; set; }

		// Token: 0x0600E4F4 RID: 58612 RVA: 0x003092B4 File Offset: 0x003074B4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ChangeTypeParameters");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600E4F5 RID: 58613 RVA: 0x00309300 File Offset: 0x00307500
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("DataType = ");
			builder.Append(this.DataType.ToString());
			return true;
		}

		// Token: 0x0600E4F6 RID: 58614 RVA: 0x0030933A File Offset: 0x0030753A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ChangeTypeParameters left, ChangeTypeParameters right)
		{
			return !(left == right);
		}

		// Token: 0x0600E4F7 RID: 58615 RVA: 0x00309346 File Offset: 0x00307546
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ChangeTypeParameters left, ChangeTypeParameters right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E4F8 RID: 58616 RVA: 0x0030935A File Offset: 0x0030755A
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<DataType>.Default.GetHashCode(this.<DataType>k__BackingField);
		}

		// Token: 0x0600E4F9 RID: 58617 RVA: 0x00309383 File Offset: 0x00307583
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ChangeTypeParameters);
		}

		// Token: 0x0600E4FA RID: 58618 RVA: 0x00309391 File Offset: 0x00307591
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ChangeTypeParameters other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<DataType>.Default.Equals(this.<DataType>k__BackingField, other.<DataType>k__BackingField));
		}

		// Token: 0x0600E4FC RID: 58620 RVA: 0x003093CF File Offset: 0x003075CF
		[CompilerGenerated]
		protected ChangeTypeParameters(ChangeTypeParameters original)
		{
			this.DataType = original.<DataType>k__BackingField;
		}

		// Token: 0x0600E4FD RID: 58621 RVA: 0x003093E3 File Offset: 0x003075E3
		[CompilerGenerated]
		public void Deconstruct(out DataType DataType)
		{
			DataType = this.DataType;
		}
	}
}
