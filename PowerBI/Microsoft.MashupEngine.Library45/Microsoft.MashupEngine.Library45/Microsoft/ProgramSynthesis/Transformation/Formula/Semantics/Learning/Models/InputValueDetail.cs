using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016D6 RID: 5846
	public abstract class InputValueDetail<T> : IEquatable<InputValueDetail<T>>
	{
		// Token: 0x0600C301 RID: 49921 RVA: 0x002A0276 File Offset: 0x0029E476
		protected InputValueDetail(string columnName, T value)
		{
			this.ColumnName = columnName;
			this.Value = value;
		}

		// Token: 0x1700212D RID: 8493
		// (get) Token: 0x0600C302 RID: 49922 RVA: 0x002A028C File Offset: 0x0029E48C
		// (set) Token: 0x0600C303 RID: 49923 RVA: 0x002A0294 File Offset: 0x0029E494
		public string ColumnName { get; set; }

		// Token: 0x1700212E RID: 8494
		// (get) Token: 0x0600C304 RID: 49924 RVA: 0x002A029D File Offset: 0x0029E49D
		// (set) Token: 0x0600C305 RID: 49925 RVA: 0x002A02A5 File Offset: 0x0029E4A5
		public T Value { get; set; }

		// Token: 0x0600C306 RID: 49926 RVA: 0x002A02AE File Offset: 0x0029E4AE
		public bool Equals(InputValueDetail<T> other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600C307 RID: 49927 RVA: 0x002A02CC File Offset: 0x0029E4CC
		public override bool Equals(object other)
		{
			return this.Equals(other as InputValueDetail<T>);
		}

		// Token: 0x0600C308 RID: 49928 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C309 RID: 49929 RVA: 0x002A02DA File Offset: 0x0029E4DA
		public static bool operator ==(InputValueDetail<T> left, InputValueDetail<T> right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C30A RID: 49930 RVA: 0x002A02F0 File Offset: 0x0029E4F0
		public static bool operator !=(InputValueDetail<T> left, InputValueDetail<T> right)
		{
			return !(left == right);
		}

		// Token: 0x0600C30B RID: 49931 RVA: 0x002A02FC File Offset: 0x0029E4FC
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = this.ColumnName + ": " + this.Value.ToCSharpLiteral());
			}
			return text;
		}

		// Token: 0x04004BD6 RID: 19414
		private string _toString;
	}
}
