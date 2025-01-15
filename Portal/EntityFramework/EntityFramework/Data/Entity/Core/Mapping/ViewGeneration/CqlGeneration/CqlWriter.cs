using System;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x020005BB RID: 1467
	internal static class CqlWriter
	{
		// Token: 0x06004727 RID: 18215 RVA: 0x000FB5DD File Offset: 0x000F97DD
		internal static string GetQualifiedName(string blockName, string field)
		{
			return StringUtil.FormatInvariant("{0}.{1}", new object[] { blockName, field });
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x000FB5F7 File Offset: 0x000F97F7
		internal static void AppendEscapedTypeName(StringBuilder builder, EdmType type)
		{
			CqlWriter.AppendEscapedName(builder, CqlWriter.GetQualifiedName(type.NamespaceName, type.Name));
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x000FB610 File Offset: 0x000F9810
		internal static void AppendEscapedQualifiedName(StringBuilder builder, string name1, string name2)
		{
			CqlWriter.AppendEscapedName(builder, name1);
			builder.Append('.');
			CqlWriter.AppendEscapedName(builder, name2);
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x000FB62C File Offset: 0x000F982C
		internal static void AppendEscapedName(StringBuilder builder, string name)
		{
			if (CqlWriter._wordIdentifierRegex.IsMatch(name) && !ExternalCalls.IsReservedKeyword(name))
			{
				builder.Append(name);
				return;
			}
			string text = name.Replace("]", "]]");
			builder.Append('[').Append(text).Append(']');
		}

		// Token: 0x0400193F RID: 6463
		private static readonly Regex _wordIdentifierRegex = new Regex("^[_A-Za-z]\\w*$", RegexOptions.Compiled | RegexOptions.ECMAScript);
	}
}
