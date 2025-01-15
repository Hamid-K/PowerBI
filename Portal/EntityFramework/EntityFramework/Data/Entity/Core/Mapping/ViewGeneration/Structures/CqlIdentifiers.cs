using System;
using System.Data.Entity.Core.Common.Utils;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000593 RID: 1427
	internal class CqlIdentifiers : InternalBase
	{
		// Token: 0x060044EE RID: 17646 RVA: 0x000F3B8A File Offset: 0x000F1D8A
		internal CqlIdentifiers()
		{
			this.m_identifiers = new Set<string>(StringComparer.Ordinal);
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x000F3BA2 File Offset: 0x000F1DA2
		internal string GetFromVariable(int num)
		{
			return this.GetNonConflictingName("_from", num);
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x000F3BB0 File Offset: 0x000F1DB0
		internal string GetBlockAlias(int num)
		{
			return this.GetNonConflictingName("T", num);
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x000F3BBE File Offset: 0x000F1DBE
		internal string GetBlockAlias()
		{
			return this.GetNonConflictingName("T", -1);
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x000F3BCC File Offset: 0x000F1DCC
		internal void AddIdentifier(string identifier)
		{
			this.m_identifiers.Add(identifier.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x000F3BE4 File Offset: 0x000F1DE4
		private string GetNonConflictingName(string prefix, int number)
		{
			string text = ((number < 0) ? prefix : StringUtil.FormatInvariant("{0}{1}", new object[] { prefix, number }));
			if (!this.m_identifiers.Contains(text.ToLower(CultureInfo.InvariantCulture)))
			{
				return text;
			}
			for (int i = 0; i < 2147483647; i++)
			{
				if (number < 0)
				{
					text = StringUtil.FormatInvariant("{0}_{1}", new object[] { prefix, i });
				}
				else
				{
					text = StringUtil.FormatInvariant("{0}_{1}_{2}", new object[] { prefix, i, number });
				}
				if (!this.m_identifiers.Contains(text.ToLower(CultureInfo.InvariantCulture)))
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x060044F4 RID: 17652 RVA: 0x000F3CA6 File Offset: 0x000F1EA6
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_identifiers.ToCompactString(builder);
		}

		// Token: 0x040018CF RID: 6351
		private readonly Set<string> m_identifiers;
	}
}
