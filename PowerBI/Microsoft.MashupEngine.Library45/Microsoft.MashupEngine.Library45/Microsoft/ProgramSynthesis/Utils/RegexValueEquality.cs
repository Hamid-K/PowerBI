using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000501 RID: 1281
	public class RegexValueEquality : IEqualityComparer, IEqualityComparer<Regex>
	{
		// Token: 0x06001C97 RID: 7319 RVA: 0x00002130 File Offset: 0x00000330
		private RegexValueEquality()
		{
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x000557A3 File Offset: 0x000539A3
		public static RegexValueEquality Comparer
		{
			get
			{
				return RegexValueEquality.Lazy.Value;
			}
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x000557AF File Offset: 0x000539AF
		bool IEqualityComparer.Equals(object x, object y)
		{
			if (!(x is Regex) || !(y is Regex))
			{
				return x.Equals(y);
			}
			return this.Equals((Regex)x, (Regex)y);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000557DB File Offset: 0x000539DB
		public int GetHashCode(object obj)
		{
			if (!(obj is Regex))
			{
				return obj.GetHashCode();
			}
			return this.GetHashCode((Regex)obj);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x000557F8 File Offset: 0x000539F8
		public bool Equals(Regex x, Regex y)
		{
			return x.ToString().Equals(y.ToString());
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x0005580B File Offset: 0x00053A0B
		public int GetHashCode(Regex obj)
		{
			return obj.ToString().GetHashCode();
		}

		// Token: 0x04000DF3 RID: 3571
		private static readonly Lazy<RegexValueEquality> Lazy = new Lazy<RegexValueEquality>(() => new RegexValueEquality());
	}
}
