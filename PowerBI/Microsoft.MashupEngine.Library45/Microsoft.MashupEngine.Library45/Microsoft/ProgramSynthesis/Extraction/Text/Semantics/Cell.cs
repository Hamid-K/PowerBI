using System;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Semantics
{
	// Token: 0x02000F93 RID: 3987
	public class Cell<T> : IEquatable<Cell<T>>
	{
		// Token: 0x06006E3B RID: 28219 RVA: 0x00168296 File Offset: 0x00166496
		public Cell(T value, bool isUserSpecified = true)
		{
			this.Value = value;
			this.IsUserSpecified = isUserSpecified;
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06006E3C RID: 28220 RVA: 0x001682AC File Offset: 0x001664AC
		public T Value { get; }

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06006E3D RID: 28221 RVA: 0x001682B4 File Offset: 0x001664B4
		public bool IsUserSpecified { get; }

		// Token: 0x06006E3E RID: 28222 RVA: 0x001682BC File Offset: 0x001664BC
		public bool Equals(Cell<T> other)
		{
			return other != null && (this == other || (object.Equals(this.Value, other.Value) && object.Equals(this.IsUserSpecified, other.IsUserSpecified)));
		}

		// Token: 0x06006E3F RID: 28223 RVA: 0x0016830E File Offset: 0x0016650E
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((Cell<T>)obj)));
		}

		// Token: 0x06006E40 RID: 28224 RVA: 0x0016833C File Offset: 0x0016653C
		public override int GetHashCode()
		{
			int num;
			if (this.Value == null)
			{
				num = 0;
			}
			else
			{
				T value = this.Value;
				num = value.GetHashCode();
			}
			return (num * 709) ^ this.IsUserSpecified.GetHashCode();
		}
	}
}
