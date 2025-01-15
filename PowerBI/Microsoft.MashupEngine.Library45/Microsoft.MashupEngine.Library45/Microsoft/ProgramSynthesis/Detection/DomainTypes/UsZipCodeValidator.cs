using System;

namespace Microsoft.ProgramSynthesis.Detection.DomainTypes
{
	// Token: 0x02000AF8 RID: 2808
	public class UsZipCodeValidator
	{
		// Token: 0x06004655 RID: 18005 RVA: 0x00002130 File Offset: 0x00000330
		private UsZipCodeValidator()
		{
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06004656 RID: 18006 RVA: 0x000DC00F File Offset: 0x000DA20F
		public static UsZipCodeValidator Instance { get; } = new UsZipCodeValidator();

		// Token: 0x06004657 RID: 18007 RVA: 0x000DC018 File Offset: 0x000DA218
		public bool IsValid(string v)
		{
			int num;
			int num2;
			int? num3;
			int? num4;
			return this.TrySplit(v, out num, out num2, out num3, out num4);
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x000DC034 File Offset: 0x000DA234
		public bool TrySplit(string v, out string zip, out string plusFour)
		{
			string text;
			plusFour = (text = null);
			zip = text;
			int num;
			int num2;
			int? num3;
			int? num4;
			if (!this.TrySplit(v, out num, out num2, out num3, out num4))
			{
				return false;
			}
			zip = v.Substring(num, num2 - num);
			if (num3 != null && num4 != null)
			{
				plusFour = v.Substring(num3.Value, num4.Value - num3.Value);
			}
			return true;
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x000DC09C File Offset: 0x000DA29C
		public bool TrySplit(string v, out int zipStart, out int zipEnd, out int? plusFourStart, out int? plusFourEnd)
		{
			int num = 0;
			zipStart = (zipEnd = -1);
			plusFourStart = (plusFourEnd = null);
			if (string.IsNullOrEmpty(v))
			{
				return false;
			}
			v.ParseWhitespace(num, out num);
			long num2;
			int num3;
			if (!v.ParseDecimalDigits(num, out num2, out num3, null) || num3 - num != 5)
			{
				return false;
			}
			zipStart = num;
			zipEnd = num3;
			num = num3;
			v.ParseWhitespace(num, out num);
			if (num >= v.Length)
			{
				return true;
			}
			if (!v.ParseCharacter(num, '-', out num))
			{
				zipStart = (zipEnd = -1);
				return false;
			}
			v.ParseWhitespace(num, out num);
			if (!v.ParseDecimalDigits(num, out num2, out num3, null) || num3 - num != 4)
			{
				zipStart = (zipEnd = -1);
				return false;
			}
			plusFourStart = new int?(num);
			plusFourEnd = new int?(num3);
			num = num3;
			v.ParseWhitespace(num, out num);
			if (num < v.Length)
			{
				zipStart = (zipEnd = -1);
				plusFourStart = (plusFourEnd = null);
				return false;
			}
			return true;
		}
	}
}
