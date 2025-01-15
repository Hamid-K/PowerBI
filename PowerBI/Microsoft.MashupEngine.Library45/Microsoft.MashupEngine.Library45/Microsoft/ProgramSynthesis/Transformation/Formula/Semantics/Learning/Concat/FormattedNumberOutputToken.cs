using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Concat
{
	// Token: 0x02001766 RID: 5990
	public class FormattedNumberOutputToken : DynamicOutputToken, IEquatable<FormattedNumberOutputToken>
	{
		// Token: 0x170021CA RID: 8650
		// (get) Token: 0x0600C6B3 RID: 50867 RVA: 0x002ABCA1 File Offset: 0x002A9EA1
		// (set) Token: 0x0600C6B4 RID: 50868 RVA: 0x002ABCA9 File Offset: 0x002A9EA9
		public IReadOnlyList<FormatNumberDescriptor> Descriptors { get; set; }

		// Token: 0x0600C6B5 RID: 50869 RVA: 0x002ABCB2 File Offset: 0x002A9EB2
		public override bool Equals(object other)
		{
			return this.Equals(other as FormattedNumberOutputToken);
		}

		// Token: 0x0600C6B6 RID: 50870 RVA: 0x002ABCC0 File Offset: 0x002A9EC0
		public bool Equals(FormattedNumberOutputToken other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600C6B7 RID: 50871 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C6B8 RID: 50872 RVA: 0x002ABCE0 File Offset: 0x002A9EE0
		public override bool IsCompatible(OutputToken other)
		{
			FormattedNumberOutputToken otherToken = other as FormattedNumberOutputToken;
			if (otherToken == null)
			{
				return false;
			}
			return (from localDescriptor in this.Descriptors
				from otherDescriptor in otherToken.Descriptors
				where localDescriptor.IsCompatible(otherDescriptor)
				select localDescriptor).Any<FormatNumberDescriptor>();
		}

		// Token: 0x0600C6B9 RID: 50873 RVA: 0x002ABD8C File Offset: 0x002A9F8C
		public static bool operator ==(FormattedNumberOutputToken left, FormattedNumberOutputToken right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C6BA RID: 50874 RVA: 0x002ABDA2 File Offset: 0x002A9FA2
		public static bool operator !=(FormattedNumberOutputToken left, FormattedNumberOutputToken right)
		{
			return !(left == right);
		}

		// Token: 0x0600C6BB RID: 50875 RVA: 0x002ABDB0 File Offset: 0x002A9FB0
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				string[] array = new string[5];
				array[0] = base.ToString();
				array[1] = ": ";
				array[2] = base.Output.ToCSharpPseudoLiteral();
				array[3] = " ";
				array[4] = this.Descriptors.Select((FormatNumberDescriptor d) => d.ToString()).ToJoinString(", ");
				text = (this._toString = string.Concat(array));
			}
			return text;
		}

		// Token: 0x04004E13 RID: 19987
		private string _toString;
	}
}
