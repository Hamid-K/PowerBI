using System;

namespace Microsoft.ProgramSynthesis.Detection.DomainTypes
{
	// Token: 0x02000AF5 RID: 2805
	public class UsPhoneNumberValidator
	{
		// Token: 0x06004643 RID: 17987 RVA: 0x00002130 File Offset: 0x00000330
		private UsPhoneNumberValidator()
		{
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004644 RID: 17988 RVA: 0x000DB973 File Offset: 0x000D9B73
		public static UsPhoneNumberValidator Instance { get; } = new UsPhoneNumberValidator();

		// Token: 0x06004645 RID: 17989 RVA: 0x000DB97C File Offset: 0x000D9B7C
		public bool IsValid(string v)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			return this.TrySplit(v, out num, out num2, out num3, out num4, out num5, out num6);
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x000DB99C File Offset: 0x000D9B9C
		public bool TrySplit(string v, out int npaStart, out int npaEnd, out int nxxStart, out int nxxEnd, out int xxxxStart, out int xxxxEnd)
		{
			int num = 0;
			npaStart = (npaEnd = (nxxStart = (nxxEnd = (xxxxStart = (xxxxEnd = -1)))));
			if (string.IsNullOrEmpty(v))
			{
				return false;
			}
			v.ParseWhitespace(num, out num);
			if (!v.ParseParenthesized(num, out num, out npaStart, out npaEnd, out nxxStart, out nxxEnd, out xxxxStart, out xxxxEnd) && !v.ParseInternational(num, out num, out npaStart, out npaEnd, out nxxStart, out nxxEnd, out xxxxStart, out xxxxEnd) && !v.ParseWithLeadingZeros(num, out num, out npaStart, out npaEnd, out nxxStart, out nxxEnd, out xxxxStart, out xxxxEnd))
			{
				return false;
			}
			v.ParseWhitespace(num, out num);
			if (num < v.Length)
			{
				npaStart = (npaEnd = (nxxStart = (nxxEnd = (xxxxStart = (xxxxEnd = -1)))));
				return false;
			}
			return true;
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x000DBA58 File Offset: 0x000D9C58
		public bool TrySplit(string v, out string npa, out string nxx, out string xxxx)
		{
			string text;
			xxxx = (text = null);
			nxx = (text = text);
			npa = text;
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			if (!this.TrySplit(v, out num, out num2, out num3, out num4, out num5, out num6))
			{
				return false;
			}
			npa = v.Substring(num, num2 - num);
			nxx = v.Substring(num3, num4 - num3);
			xxxx = v.Substring(num5, num6 - num5);
			return true;
		}
	}
}
