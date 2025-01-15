using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B4E RID: 6990
	public class FillNaParameters : IParameters, IEquatable<FillNaParameters>
	{
		// Token: 0x0600E56C RID: 58732 RVA: 0x00309F7F File Offset: 0x0030817F
		public FillNaParameters(object FillValue, IEnumerable<object> MissingValueMarkers, FillMethod FillMethod)
		{
			this.FillValue = FillValue;
			this.MissingValueMarkers = MissingValueMarkers;
			this.FillMethod = FillMethod;
			base..ctor();
		}

		// Token: 0x17002639 RID: 9785
		// (get) Token: 0x0600E56D RID: 58733 RVA: 0x00309F9C File Offset: 0x0030819C
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(FillNaParameters);
			}
		}

		// Token: 0x1700263A RID: 9786
		// (get) Token: 0x0600E56E RID: 58734 RVA: 0x00309FA8 File Offset: 0x003081A8
		// (set) Token: 0x0600E56F RID: 58735 RVA: 0x00309FB0 File Offset: 0x003081B0
		public object FillValue { get; set; }

		// Token: 0x1700263B RID: 9787
		// (get) Token: 0x0600E570 RID: 58736 RVA: 0x00309FB9 File Offset: 0x003081B9
		// (set) Token: 0x0600E571 RID: 58737 RVA: 0x00309FC1 File Offset: 0x003081C1
		public IEnumerable<object> MissingValueMarkers { get; set; }

		// Token: 0x1700263C RID: 9788
		// (get) Token: 0x0600E572 RID: 58738 RVA: 0x00309FCA File Offset: 0x003081CA
		// (set) Token: 0x0600E573 RID: 58739 RVA: 0x00309FD2 File Offset: 0x003081D2
		public FillMethod FillMethod { get; set; }

		// Token: 0x0600E574 RID: 58740 RVA: 0x00309FDC File Offset: 0x003081DC
		public override string ToString()
		{
			return string.Format("{{FillValue={0}, MissingValueMarkers=[{1}], FillMethod={2}}}", this.FillValue, string.Join(", ", this.MissingValueMarkers.Select((object c) => ((c != null) ? c.ToString() : null) ?? string.Empty)), this.FillMethod);
		}

		// Token: 0x0600E575 RID: 58741 RVA: 0x0030A038 File Offset: 0x00308238
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("FillValue = ");
			builder.Append(this.FillValue);
			builder.Append(", MissingValueMarkers = ");
			builder.Append(this.MissingValueMarkers);
			builder.Append(", FillMethod = ");
			builder.Append(this.FillMethod.ToString());
			return true;
		}

		// Token: 0x0600E576 RID: 58742 RVA: 0x0030A0A4 File Offset: 0x003082A4
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FillNaParameters left, FillNaParameters right)
		{
			return !(left == right);
		}

		// Token: 0x0600E577 RID: 58743 RVA: 0x0030A0B0 File Offset: 0x003082B0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FillNaParameters left, FillNaParameters right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600E578 RID: 58744 RVA: 0x0030A0C4 File Offset: 0x003082C4
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<object>.Default.GetHashCode(this.<FillValue>k__BackingField)) * -1521134295 + EqualityComparer<IEnumerable<object>>.Default.GetHashCode(this.<MissingValueMarkers>k__BackingField)) * -1521134295 + EqualityComparer<FillMethod>.Default.GetHashCode(this.<FillMethod>k__BackingField);
		}

		// Token: 0x0600E579 RID: 58745 RVA: 0x0030A126 File Offset: 0x00308326
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FillNaParameters);
		}

		// Token: 0x0600E57A RID: 58746 RVA: 0x0030A134 File Offset: 0x00308334
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FillNaParameters other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<object>.Default.Equals(this.<FillValue>k__BackingField, other.<FillValue>k__BackingField) && EqualityComparer<IEnumerable<object>>.Default.Equals(this.<MissingValueMarkers>k__BackingField, other.<MissingValueMarkers>k__BackingField) && EqualityComparer<FillMethod>.Default.Equals(this.<FillMethod>k__BackingField, other.<FillMethod>k__BackingField));
		}

		// Token: 0x0600E57C RID: 58748 RVA: 0x0030A1AD File Offset: 0x003083AD
		[CompilerGenerated]
		protected FillNaParameters([Nullable(1)] FillNaParameters original)
		{
			this.FillValue = original.<FillValue>k__BackingField;
			this.MissingValueMarkers = original.<MissingValueMarkers>k__BackingField;
			this.FillMethod = original.<FillMethod>k__BackingField;
		}

		// Token: 0x0600E57D RID: 58749 RVA: 0x0030A1D9 File Offset: 0x003083D9
		[CompilerGenerated]
		public void Deconstruct(out object FillValue, out IEnumerable<object> MissingValueMarkers, out FillMethod FillMethod)
		{
			FillValue = this.FillValue;
			MissingValueMarkers = this.MissingValueMarkers;
			FillMethod = this.FillMethod;
		}
	}
}
