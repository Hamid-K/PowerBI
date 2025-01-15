using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016E1 RID: 5857
	public class RowNumberLinearTransformDescriptor : IEquatable<RowNumberLinearTransformDescriptor>
	{
		// Token: 0x1700213F RID: 8511
		// (get) Token: 0x0600C34B RID: 49995 RVA: 0x002A0A51 File Offset: 0x0029EC51
		// (set) Token: 0x0600C34C RID: 49996 RVA: 0x002A0A59 File Offset: 0x0029EC59
		public decimal Gradient { get; set; }

		// Token: 0x17002140 RID: 8512
		// (get) Token: 0x0600C34D RID: 49997 RVA: 0x002A0A62 File Offset: 0x0029EC62
		// (set) Token: 0x0600C34E RID: 49998 RVA: 0x002A0A6A File Offset: 0x0029EC6A
		public decimal Intercept { get; set; }

		// Token: 0x0600C34F RID: 49999 RVA: 0x002A0A73 File Offset: 0x0029EC73
		public bool Equals(RowNumberLinearTransformDescriptor other)
		{
			return other != null && this.Gradient == other.Gradient && this.Intercept == other.Intercept;
		}

		// Token: 0x0600C350 RID: 50000 RVA: 0x002A0A9E File Offset: 0x0029EC9E
		public override bool Equals(object other)
		{
			return this.Equals(other as RowNumberLinearTransformDescriptor);
		}

		// Token: 0x0600C351 RID: 50001 RVA: 0x002A0AAC File Offset: 0x0029ECAC
		public override int GetHashCode()
		{
			return new Record<decimal, decimal>(this.Gradient, this.Intercept).GetHashCode();
		}

		// Token: 0x0600C352 RID: 50002 RVA: 0x002A0AD8 File Offset: 0x0029ECD8
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = string.Format("{{{0}, {1}}}", this.Gradient, this.Intercept));
			}
			return text;
		}

		// Token: 0x04004C08 RID: 19464
		private string _toString;
	}
}
