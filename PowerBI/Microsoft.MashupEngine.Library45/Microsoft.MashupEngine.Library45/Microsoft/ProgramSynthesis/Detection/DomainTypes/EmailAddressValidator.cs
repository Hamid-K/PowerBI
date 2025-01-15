using System;

namespace Microsoft.ProgramSynthesis.Detection.DomainTypes
{
	// Token: 0x02000AEA RID: 2794
	public class EmailAddressValidator
	{
		// Token: 0x060045E8 RID: 17896 RVA: 0x00002130 File Offset: 0x00000330
		private EmailAddressValidator()
		{
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x060045E9 RID: 17897 RVA: 0x000DA048 File Offset: 0x000D8248
		public static EmailAddressValidator Instance { get; } = new EmailAddressValidator();

		// Token: 0x060045EA RID: 17898 RVA: 0x000DA050 File Offset: 0x000D8250
		public bool TrySplit(string v, out int? displayNameStart, out int? displayNameEnd, out int localPartStart, out int localPartEnd, out int domainStart, out int domainEnd)
		{
			int num;
			return v.ParseMailbox(0, out num, out displayNameStart, out displayNameEnd, out localPartStart, out localPartEnd, out domainStart, out domainEnd) && num == v.Length;
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x000DA080 File Offset: 0x000D8280
		public bool TrySplit(string v, out string displayName, out string localPart, out string domain)
		{
			int? num;
			int? num2;
			int num3;
			int num4;
			int num5;
			int num6;
			if (!this.TrySplit(v, out num, out num2, out num3, out num4, out num5, out num6))
			{
				string text;
				domain = (text = null);
				localPart = (text = text);
				displayName = text;
				return false;
			}
			if (num != null && num2 != null)
			{
				displayName = v.Substring(num.Value, num2.Value - num.Value);
			}
			else
			{
				displayName = null;
			}
			localPart = v.Substring(num3, num4 - num3);
			domain = v.Substring(num5, num6 - num5);
			return true;
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x000DA108 File Offset: 0x000D8308
		public bool IsValid(string v)
		{
			int? num;
			int? num2;
			int num3;
			int num4;
			int num5;
			int num6;
			return this.TrySplit(v, out num, out num2, out num3, out num4, out num5, out num6);
		}
	}
}
