using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000782 RID: 1922
	internal abstract class NameValidator
	{
		// Token: 0x06006B6B RID: 27499 RVA: 0x001B30A9 File Offset: 0x001B12A9
		protected static bool IsCLSCompliant(string name)
		{
			return NameValidator.m_clsIdentifierRegex.Match(name).Success;
		}

		// Token: 0x06006B6C RID: 27500 RVA: 0x001B30BB File Offset: 0x001B12BB
		protected bool IsUnique(string name)
		{
			if (this.m_dictionary.ContainsKey(name))
			{
				return false;
			}
			this.m_dictionary.Add(name, null);
			return true;
		}

		// Token: 0x0400360D RID: 13837
		private const string m_identifierStart = "\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}";

		// Token: 0x0400360E RID: 13838
		private const string m_identifierExtend = "\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}";

		// Token: 0x0400360F RID: 13839
		private static Regex m_clsIdentifierRegex = new Regex("^[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]*$", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04003610 RID: 13840
		private Hashtable m_dictionary = new Hashtable();
	}
}
