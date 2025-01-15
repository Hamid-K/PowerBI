using System;

namespace Microsoft.ProgramSynthesis.Detection.DomainTypes
{
	// Token: 0x02000AF1 RID: 2801
	public class Ipv6AddressValidator
	{
		// Token: 0x06004629 RID: 17961 RVA: 0x000DB2D8 File Offset: 0x000D94D8
		private static bool PopulateOctets(byte[] octets, ref int octetIndex, long value)
		{
			if (octets == null)
			{
				return true;
			}
			if (value > 65535L)
			{
				return false;
			}
			int num = octetIndex;
			octetIndex = num + 1;
			octets[num] = (byte)(value >> 8);
			num = octetIndex;
			octetIndex = num + 1;
			octets[num] = (byte)(value % 256L);
			return true;
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x000DB31C File Offset: 0x000D951C
		public bool TrySplit(string v, byte[] octets)
		{
			if (string.IsNullOrEmpty(v))
			{
				return false;
			}
			int num = 0;
			while (num < 16 && octets != null)
			{
				octets[num] = 0;
				num++;
			}
			int num2 = 0;
			int num3 = 0;
			int num4 = -1;
			v.ParseWhitespace(num2, out num2);
			long num5;
			int num6;
			if (v.ParseHexDigits(num2, out num5, out num6))
			{
				if (!Ipv6AddressValidator.PopulateOctets(octets, ref num3, num5))
				{
					return false;
				}
				num2 = num6;
			}
			for (int i = 0; i < 7; i++)
			{
				if (v.ParseString(num2, "::", out num6))
				{
					if (num4 >= 0)
					{
						return false;
					}
					num4 = num3;
					num2 = num6;
					if (Ipv6AddressValidator.ParseIpv6Suffix(v, num2, out num6))
					{
						return true;
					}
				}
				else
				{
					if (!v.ParseCharacter(num2, ':', out num6))
					{
						return false;
					}
					num2 = num6;
				}
				if (!v.ParseHexDigits(num2, out num5, out num6))
				{
					return false;
				}
				num2 = num6;
				if (!Ipv6AddressValidator.PopulateOctets(octets, ref num3, num5))
				{
					return false;
				}
				if (num4 >= 0 && Ipv6AddressValidator.ParseIpv6Suffix(v, num2, out num2))
				{
					break;
				}
			}
			if (num4 >= 0 && num3 >= 16)
			{
				return false;
			}
			if (!Ipv6AddressValidator.ParseIpv6Suffix(v, num2, out num2))
			{
				return false;
			}
			if (num4 >= 0 && octets != null)
			{
				int num7 = 15;
				int j = num3 - 1;
				while (j >= num4)
				{
					octets[num7--] = octets[j];
					octets[j--] = 0;
				}
			}
			return true;
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x000DB43C File Offset: 0x000D963C
		private static bool ParseIpv6Suffix(string v, int index, out int nextIndex)
		{
			nextIndex = index;
			if (v.ParseCharacter(index, '%', out index))
			{
				v.ParseWhile(index, out index, (char c) => char.IsLetterOrDigit(c) || c == '_');
			}
			bool flag = v.ParseWhitespaceAtEnd(index, out index);
			if (flag)
			{
				nextIndex = index;
			}
			return flag;
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x000DB491 File Offset: 0x000D9691
		public bool IsValid(string v)
		{
			return this.TrySplit(v, null);
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x00002130 File Offset: 0x00000330
		private Ipv6AddressValidator()
		{
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x0600462E RID: 17966 RVA: 0x000DB49B File Offset: 0x000D969B
		public static Ipv6AddressValidator Instance { get; } = new Ipv6AddressValidator();
	}
}
