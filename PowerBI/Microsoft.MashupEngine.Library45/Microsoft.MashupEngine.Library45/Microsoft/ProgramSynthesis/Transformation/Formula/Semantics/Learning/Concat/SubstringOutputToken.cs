using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Concat
{
	// Token: 0x0200176C RID: 5996
	public class SubstringOutputToken : DynamicOutputToken, IEquatable<SubstringOutputToken>
	{
		// Token: 0x170021CC RID: 8652
		// (get) Token: 0x0600C6D7 RID: 50903 RVA: 0x002AC052 File Offset: 0x002AA252
		// (set) Token: 0x0600C6D8 RID: 50904 RVA: 0x002AC05A File Offset: 0x002AA25A
		public IReadOnlyList<SubstringDescriptor> Descriptors { get; set; }

		// Token: 0x0600C6D9 RID: 50905 RVA: 0x002AC063 File Offset: 0x002AA263
		public override bool Equals(object other)
		{
			return this.Equals(other as SubstringOutputToken);
		}

		// Token: 0x0600C6DA RID: 50906 RVA: 0x002AC071 File Offset: 0x002AA271
		public bool Equals(SubstringOutputToken other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600C6DB RID: 50907 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C6DC RID: 50908 RVA: 0x002AC090 File Offset: 0x002AA290
		public override bool IsCompatible(OutputToken other)
		{
			SubstringOutputToken otherToken = other as SubstringOutputToken;
			if (otherToken == null)
			{
				return false;
			}
			return (from localDescriptor in this.Descriptors
				from otherDescriptor in otherToken.Descriptors
				where localDescriptor.IsCompatible(otherDescriptor)
				select localDescriptor).Any<SubstringDescriptor>();
		}

		// Token: 0x0600C6DD RID: 50909 RVA: 0x002AC13C File Offset: 0x002AA33C
		public static bool operator ==(SubstringOutputToken left, SubstringOutputToken right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C6DE RID: 50910 RVA: 0x002AC152 File Offset: 0x002AA352
		public static bool operator !=(SubstringOutputToken left, SubstringOutputToken right)
		{
			return !(left == right);
		}

		// Token: 0x0600C6DF RID: 50911 RVA: 0x002AC160 File Offset: 0x002AA360
		public override string ToString()
		{
			if (this._toString != null)
			{
				return this._toString;
			}
			string text;
			if (!this.Descriptors.Any<SubstringDescriptor>())
			{
				text = string.Empty;
			}
			else
			{
				text = Environment.NewLine + this.Descriptors.Select((SubstringDescriptor d) => d.ToString()).ToJoinNewlineString().Indent(7, false);
			}
			string text2 = text;
			return this._toString = base.ToString() + text2;
		}

		// Token: 0x04004E23 RID: 20003
		private string _toString;
	}
}
