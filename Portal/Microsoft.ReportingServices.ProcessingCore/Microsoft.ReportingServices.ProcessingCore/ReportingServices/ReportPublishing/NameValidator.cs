using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003A9 RID: 937
	internal abstract class NameValidator
	{
		// Token: 0x06002669 RID: 9833 RVA: 0x000B86AC File Offset: 0x000B68AC
		protected NameValidator(bool caseInsensitiveComparison)
		{
			if (caseInsensitiveComparison)
			{
				this.m_dictionaryCaseInsensitive = new Hashtable(StringComparer.OrdinalIgnoreCase);
			}
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000B86D2 File Offset: 0x000B68D2
		protected static bool IsCLSCompliant(string name)
		{
			return NameValidator.m_clsIdentifierRegex.Match(name).Success;
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x000B86E4 File Offset: 0x000B68E4
		protected bool IsUnique(string name)
		{
			if (this.m_dictionary.ContainsKey(name))
			{
				return false;
			}
			this.m_dictionary.Add(name, null);
			return true;
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x000B8704 File Offset: 0x000B6904
		protected bool IsCaseInsensitiveDuplicate(string name)
		{
			if (this.m_dictionaryCaseInsensitive == null)
			{
				return false;
			}
			if (this.m_dictionaryCaseInsensitive.ContainsKey(name))
			{
				return true;
			}
			this.m_dictionaryCaseInsensitive.Add(name, null);
			return false;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000B872E File Offset: 0x000B692E
		internal virtual bool Validate(string name)
		{
			return NameValidator.IsCLSCompliant(name) && this.IsUnique(name);
		}

		// Token: 0x04001632 RID: 5682
		protected const int MAX_NAME_LENGTH = 256;

		// Token: 0x04001633 RID: 5683
		protected const string MAX_NAME_LENGTH_STRING = "256";

		// Token: 0x04001634 RID: 5684
		private const string m_identifierStart = "\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}";

		// Token: 0x04001635 RID: 5685
		private const string m_identifierExtend = "\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}";

		// Token: 0x04001636 RID: 5686
		private static Regex m_clsIdentifierRegex = new Regex("^[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]*$", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04001637 RID: 5687
		protected Hashtable m_dictionary = new Hashtable();

		// Token: 0x04001638 RID: 5688
		private Hashtable m_dictionaryCaseInsensitive;
	}
}
