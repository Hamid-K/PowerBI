using System;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x020008F1 RID: 2289
	internal class Node
	{
		// Token: 0x06004877 RID: 18551 RVA: 0x001093E0 File Offset: 0x001075E0
		protected int GetIndexFromChar(char c)
		{
			char c2 = char.ToUpperInvariant(c);
			if (c2 == ' ')
			{
				return 26;
			}
			if (c2 == '_')
			{
				return 27;
			}
			int num = (int)(c2 - 'A');
			if (num >= 0 && num < 27)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06004878 RID: 18552 RVA: 0x00006F04 File Offset: 0x00005104
		internal virtual bool IsLeaf()
		{
			return false;
		}

		// Token: 0x06004879 RID: 18553 RVA: 0x00109415 File Offset: 0x00107615
		internal static bool IsDelimiter(char c)
		{
			return char.IsWhiteSpace(c) || c == ',' || c == ')' || c == '(' || c == ':' || c == ';' || c == '+' || c == '-';
		}

		// Token: 0x04003521 RID: 13601
		internal const int CharNum = 28;
	}
}
