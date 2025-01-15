using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011DE RID: 4574
	internal static class IScriptableExtensions
	{
		// Token: 0x060078A4 RID: 30884 RVA: 0x001A18B8 File Offset: 0x0019FAB8
		public static string ToScript(this IScriptable scriptable, SqlSettings settings)
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				ScriptWriter scriptWriter = new ScriptWriter(stringWriter, settings);
				scriptable.WriteCreateScript(scriptWriter);
				text = stringWriter.ToString();
			}
			return text;
		}
	}
}
