using System;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.SqlTranslator
{
	// Token: 0x0200203C RID: 8252
	public static class SqlHelper
	{
		// Token: 0x0600C9DF RID: 51679 RVA: 0x002865CC File Offset: 0x002847CC
		public static string CreateSelectAllCommand(string resourceName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}", "SELECT * FROM ", SqlHelper.QuoteName(resourceName));
		}

		// Token: 0x0600C9E0 RID: 51680 RVA: 0x002865E8 File Offset: 0x002847E8
		public static bool TryParseSelectAllCommand(IEngine engine, string commandText, out string resourceName)
		{
			SqlParseResult sqlParseResult;
			try
			{
				sqlParseResult = SqlParser.Parse(commandText);
			}
			catch (NotSupportedException)
			{
				resourceName = null;
				return false;
			}
			if (sqlParseResult.IsPassthrough && sqlParseResult.ResourceNames.Count<string>() == 1)
			{
				resourceName = sqlParseResult.ResourceNames.Single<string>();
				return true;
			}
			resourceName = null;
			return false;
		}

		// Token: 0x0600C9E1 RID: 51681 RVA: 0x00286644 File Offset: 0x00284844
		public static string EscapeResourceName(string resourceName)
		{
			if (resourceName == string.Empty)
			{
				return "#\"\"";
			}
			return resourceName.Replace("#\"\"", "#\"\"#\"\"");
		}

		// Token: 0x0600C9E2 RID: 51682 RVA: 0x00286669 File Offset: 0x00284869
		public static string UnEscapeResourceName(string resourceName)
		{
			if (resourceName == "#\"\"")
			{
				return string.Empty;
			}
			return resourceName.Replace("#\"\"#\"\"", "#\"\"");
		}

		// Token: 0x0600C9E3 RID: 51683 RVA: 0x0017BE47 File Offset: 0x0017A047
		private static string QuoteName(string identifier)
		{
			return "[" + identifier.Replace("]", "]]") + "]";
		}

		// Token: 0x040066C5 RID: 26309
		private const string sqlCommandPrefix = "SELECT * FROM ";

		// Token: 0x040066C6 RID: 26310
		private const string emptyResourceName = "#\"\"";

		// Token: 0x040066C7 RID: 26311
		private const string escapeString = "#\"\"#\"\"";
	}
}
