using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001AFC RID: 6908
	public class OutlierCondition : DropCondition, IEquatable<OutlierCondition>
	{
		// Token: 0x0600E3D1 RID: 58321 RVA: 0x003055EB File Offset: 0x003037EB
		public OutlierCondition(string SourceColumnName, Tuple<double, double> ValidBoundExclusive)
		{
			this.SourceColumnName = SourceColumnName;
			this.ValidBoundExclusive = ValidBoundExclusive;
			base..ctor(DropReason.Outlier);
		}

		// Token: 0x1700260E RID: 9742
		// (get) Token: 0x0600E3D2 RID: 58322 RVA: 0x00305602 File Offset: 0x00303802
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(OutlierCondition);
			}
		}

		// Token: 0x1700260F RID: 9743
		// (get) Token: 0x0600E3D3 RID: 58323 RVA: 0x0030560E File Offset: 0x0030380E
		// (set) Token: 0x0600E3D4 RID: 58324 RVA: 0x00305616 File Offset: 0x00303816
		public string SourceColumnName { get; set; }

		// Token: 0x17002610 RID: 9744
		// (get) Token: 0x0600E3D5 RID: 58325 RVA: 0x0030561F File Offset: 0x0030381F
		// (set) Token: 0x0600E3D6 RID: 58326 RVA: 0x00305627 File Offset: 0x00303827
		public Tuple<double, double> ValidBoundExclusive { get; set; }

		// Token: 0x0600E3D7 RID: 58327 RVA: 0x00305630 File Offset: 0x00303830
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("OutlierCondition");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600E3D8 RID: 58328 RVA: 0x0030567C File Offset: 0x0030387C
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SourceColumnName = ");
			builder.Append(this.SourceColumnName);
			builder.Append(", ValidBoundExclusive = ");
			builder.Append(this.ValidBoundExclusive);
			return true;
		}

		// Token: 0x0600E3D9 RID: 58329 RVA: 0x003056D1 File Offset: 0x003038D1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(OutlierCondition left, OutlierCondition right)
		{
			return !(left == right);
		}

		// Token: 0x0600E3DA RID: 58330 RVA: 0x003056DD File Offset: 0x003038DD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(OutlierCondition left, OutlierCondition right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E3DB RID: 58331 RVA: 0x003056F1 File Offset: 0x003038F1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SourceColumnName>k__BackingField)) * -1521134295 + EqualityComparer<Tuple<double, double>>.Default.GetHashCode(this.<ValidBoundExclusive>k__BackingField);
		}

		// Token: 0x0600E3DC RID: 58332 RVA: 0x00305727 File Offset: 0x00303927
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as OutlierCondition);
		}

		// Token: 0x0600E3DD RID: 58333 RVA: 0x00024CEC File Offset: 0x00022EEC
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(DropCondition other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600E3DE RID: 58334 RVA: 0x00305738 File Offset: 0x00303938
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(OutlierCondition other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<SourceColumnName>k__BackingField, other.<SourceColumnName>k__BackingField) && EqualityComparer<Tuple<double, double>>.Default.Equals(this.<ValidBoundExclusive>k__BackingField, other.<ValidBoundExclusive>k__BackingField));
		}

		// Token: 0x0600E3E0 RID: 58336 RVA: 0x0030578C File Offset: 0x0030398C
		[CompilerGenerated]
		protected OutlierCondition([Nullable(1)] OutlierCondition original)
			: base(original)
		{
			this.SourceColumnName = original.<SourceColumnName>k__BackingField;
			this.ValidBoundExclusive = original.<ValidBoundExclusive>k__BackingField;
		}

		// Token: 0x0600E3E1 RID: 58337 RVA: 0x003057AD File Offset: 0x003039AD
		[CompilerGenerated]
		public void Deconstruct(out string SourceColumnName, out Tuple<double, double> ValidBoundExclusive)
		{
			SourceColumnName = this.SourceColumnName;
			ValidBoundExclusive = this.ValidBoundExclusive;
		}
	}
}
