using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000061 RID: 97
	internal sealed class Candidate<T> : IEquatable<Candidate<T>>
	{
		// Token: 0x06000202 RID: 514 RVA: 0x00005A47 File Offset: 0x00003C47
		private Candidate(T value, bool isValid)
		{
			this.m_value = value;
			this.m_isValid = isValid;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00005A5D File Offset: 0x00003C5D
		public bool IsValid
		{
			get
			{
				return this.m_isValid;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00005A65 File Offset: 0x00003C65
		public T Value
		{
			get
			{
				Contract.RetailAssert(this.IsValid, "Cannot get invalid Value");
				return this.m_value;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00005A7D File Offset: 0x00003C7D
		public Candidate<U> Cast<U>() where U : T
		{
			if (!this.IsValid)
			{
				return Candidate<U>.Invalid;
			}
			return Candidate<U>.Valid((U)((object)this.Value));
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00005AA2 File Offset: 0x00003CA2
		public static implicit operator Candidate<T>(T value)
		{
			return Candidate<T>.Valid(value);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00005AAA File Offset: 0x00003CAA
		public static Candidate<T> Invalid
		{
			get
			{
				return Candidate<T>.m_invalidInstance;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00005AB1 File Offset: 0x00003CB1
		public static Candidate<T> Valid(T value)
		{
			return new Candidate<T>(value, true);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00005ABA File Offset: 0x00003CBA
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Candidate<T>);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00005AC8 File Offset: 0x00003CC8
		public bool Equals(Candidate<T> other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.IsValid != other.IsValid)
			{
				return false;
			}
			if (this.IsValid)
			{
				T value = this.Value;
				if (!value.Equals(other.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00005B16 File Offset: 0x00003D16
		public static bool operator ==(Candidate<T> candidate1, Candidate<T> candidate2)
		{
			return candidate1 == candidate2 || (candidate1 != null && candidate1.Equals(candidate2));
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00005B2A File Offset: 0x00003D2A
		public static bool operator !=(Candidate<T> candidate1, Candidate<T> candidate2)
		{
			return !(candidate1 == candidate2);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00005B38 File Offset: 0x00003D38
		public override int GetHashCode()
		{
			int num;
			if (!object.Equals(this.Value, default(T)))
			{
				T value = this.Value;
				num = value.GetHashCode();
			}
			else
			{
				num = typeof(T).GetHashCode();
			}
			return Hashing.CombineHash(num, this.IsValid.GetHashCode());
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00005BA0 File Offset: 0x00003DA0
		public override string ToString()
		{
			T t;
			if (!this.IsValid)
			{
				string text = "Candidate.Invalid: ";
				t = this.m_value;
				return text + ((t != null) ? t.ToString() : null);
			}
			t = this.m_value;
			return t.ToString();
		}

		// Token: 0x040000FD RID: 253
		private static Candidate<T> m_invalidInstance = new Candidate<T>(default(T), false);

		// Token: 0x040000FE RID: 254
		private readonly T m_value;

		// Token: 0x040000FF RID: 255
		private readonly bool m_isValid;
	}
}
