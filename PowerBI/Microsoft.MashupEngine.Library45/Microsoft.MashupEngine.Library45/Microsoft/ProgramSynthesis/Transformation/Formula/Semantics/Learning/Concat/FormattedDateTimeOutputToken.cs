using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Concat
{
	// Token: 0x02001769 RID: 5993
	public class FormattedDateTimeOutputToken : DynamicOutputToken, IEquatable<FormattedDateTimeOutputToken>
	{
		// Token: 0x170021CB RID: 8651
		// (get) Token: 0x0600C6C5 RID: 50885 RVA: 0x002ABE7E File Offset: 0x002AA07E
		// (set) Token: 0x0600C6C6 RID: 50886 RVA: 0x002ABE86 File Offset: 0x002AA086
		public IReadOnlyList<DateTimeDescriptor> Descriptors { get; set; }

		// Token: 0x0600C6C7 RID: 50887 RVA: 0x002ABE8F File Offset: 0x002AA08F
		public override bool Equals(object other)
		{
			return this.Equals(other as FormattedDateTimeOutputToken);
		}

		// Token: 0x0600C6C8 RID: 50888 RVA: 0x002ABE9D File Offset: 0x002AA09D
		public bool Equals(FormattedDateTimeOutputToken other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600C6C9 RID: 50889 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C6CA RID: 50890 RVA: 0x002ABEBC File Offset: 0x002AA0BC
		public override bool IsCompatible(OutputToken other)
		{
			FormattedDateTimeOutputToken otherToken = other as FormattedDateTimeOutputToken;
			if (otherToken == null)
			{
				return false;
			}
			return (from localDescriptor in this.Descriptors
				from otherDescriptor in otherToken.Descriptors
				where localDescriptor.IsCompatible(otherDescriptor)
				select localDescriptor).Any<DateTimeDescriptor>();
		}

		// Token: 0x0600C6CB RID: 50891 RVA: 0x002ABF68 File Offset: 0x002AA168
		public static bool operator ==(FormattedDateTimeOutputToken left, FormattedDateTimeOutputToken right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C6CC RID: 50892 RVA: 0x002ABF7E File Offset: 0x002AA17E
		public static bool operator !=(FormattedDateTimeOutputToken left, FormattedDateTimeOutputToken right)
		{
			return !(left == right);
		}

		// Token: 0x0600C6CD RID: 50893 RVA: 0x002ABF8C File Offset: 0x002AA18C
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
				array[4] = this.Descriptors.Select((DateTimeDescriptor d) => d.ToString()).ToJoinString(", ");
				text = (this._toString = string.Concat(array));
			}
			return text;
		}

		// Token: 0x04004E1B RID: 19995
		private string _toString;
	}
}
