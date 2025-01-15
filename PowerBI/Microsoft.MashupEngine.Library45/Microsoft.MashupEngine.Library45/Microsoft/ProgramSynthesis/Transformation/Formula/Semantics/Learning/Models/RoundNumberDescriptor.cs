using System;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016E0 RID: 5856
	public class RoundNumberDescriptor : IEquatable<RoundNumberDescriptor>
	{
		// Token: 0x1700213D RID: 8509
		// (get) Token: 0x0600C33F RID: 49983 RVA: 0x002A096C File Offset: 0x0029EB6C
		// (set) Token: 0x0600C340 RID: 49984 RVA: 0x002A0974 File Offset: 0x0029EB74
		public double Delta { get; set; }

		// Token: 0x1700213E RID: 8510
		// (get) Token: 0x0600C341 RID: 49985 RVA: 0x002A097D File Offset: 0x0029EB7D
		// (set) Token: 0x0600C342 RID: 49986 RVA: 0x002A0985 File Offset: 0x0029EB85
		public RoundingMode Mode { get; set; }

		// Token: 0x0600C343 RID: 49987 RVA: 0x002A098E File Offset: 0x0029EB8E
		public bool Equals(RoundNumberDescriptor other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600C344 RID: 49988 RVA: 0x002A09AC File Offset: 0x0029EBAC
		public override bool Equals(object other)
		{
			return this.Equals(other as RoundNumberDescriptor);
		}

		// Token: 0x0600C345 RID: 49989 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C346 RID: 49990 RVA: 0x002A09BA File Offset: 0x0029EBBA
		public static bool operator ==(RoundNumberDescriptor left, RoundNumberDescriptor right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C347 RID: 49991 RVA: 0x002A09D0 File Offset: 0x0029EBD0
		public static bool operator !=(RoundNumberDescriptor left, RoundNumberDescriptor right)
		{
			return !(left == right);
		}

		// Token: 0x0600C348 RID: 49992 RVA: 0x002A09DC File Offset: 0x0029EBDC
		public string ToFormatString()
		{
			string text;
			if ((text = this._toFormatString) == null)
			{
				text = (this._toFormatString = string.Format("{0},{1}", this.Mode, this.Delta));
			}
			return text;
		}

		// Token: 0x0600C349 RID: 49993 RVA: 0x002A0A1C File Offset: 0x0029EC1C
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = "{" + this.ToFormatString() + "}");
			}
			return text;
		}

		// Token: 0x04004C04 RID: 19460
		private string _toFormatString;

		// Token: 0x04004C05 RID: 19461
		private string _toString;
	}
}
