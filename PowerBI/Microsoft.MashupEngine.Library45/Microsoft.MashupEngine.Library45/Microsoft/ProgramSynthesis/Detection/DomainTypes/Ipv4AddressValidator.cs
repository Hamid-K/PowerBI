using System;

namespace Microsoft.ProgramSynthesis.Detection.DomainTypes
{
	// Token: 0x02000AF0 RID: 2800
	public class Ipv4AddressValidator
	{
		// Token: 0x06004624 RID: 17956 RVA: 0x000DB20F File Offset: 0x000D940F
		public bool IsValid(string v)
		{
			return this.TrySplit(v, null);
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x000DB21C File Offset: 0x000D941C
		public bool TrySplit(string v, byte[] octets)
		{
			if (string.IsNullOrEmpty(v))
			{
				return false;
			}
			int num = 0;
			int num2 = 0;
			v.ParseWhitespace(num, out num);
			long num3;
			if (!v.ParseDecimalDigits(num, out num3, out num, null) || num3 > 255L)
			{
				return false;
			}
			if (octets != null)
			{
				octets[num2++] = (byte)num3;
			}
			for (int i = 0; i < 3; i++)
			{
				if (!v.ParseCharacter(num, '.', out num))
				{
					return false;
				}
				if (!v.ParseDecimalDigits(num, out num3, out num, null) || num3 > 255L)
				{
					return false;
				}
				if (octets != null)
				{
					octets[num2++] = (byte)num3;
				}
			}
			int num4;
			return v.ParseWhitespaceAtEnd(num, out num4);
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x00002130 File Offset: 0x00000330
		private Ipv4AddressValidator()
		{
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06004627 RID: 17959 RVA: 0x000DB2C3 File Offset: 0x000D94C3
		public static Ipv4AddressValidator Instance { get; } = new Ipv4AddressValidator();
	}
}
