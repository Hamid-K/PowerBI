using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001A0 RID: 416
	public static class CodeGenerationUtils
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x00024D54 File Offset: 0x00022F54
		public static IEnumerable<string> GetTypeReferencedAssemblyLocationsFromAppDomain(Type type)
		{
			return (from a in (from ra in type.Assembly.GetReferencedAssemblies()
					from pa in AppDomain.CurrentDomain.GetAssemblies()
					where ra.FullName.Equals(pa.FullName, StringComparison.Ordinal)
					select pa).Distinct((Assembly a) => a.FullName)
				select a.Location).Materialize<string>();
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00024E44 File Offset: 0x00023044
		public static string ConvertToValidCSharpClassName([NotNull] string name)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			string text = CodeGenerationUtils.s_invalidCSharpCharactersRegex.Replace(name, "");
			int num = 0;
			while (char.IsDigit(text, num))
			{
				num++;
			}
			return text.Substring(num);
		}

		// Token: 0x04000436 RID: 1078
		private static readonly Regex s_invalidCSharpCharactersRegex = new Regex("[^\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Nd}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Cf}\\p{Pc}\\p{Lm}]", RegexOptions.Compiled);
	}
}
