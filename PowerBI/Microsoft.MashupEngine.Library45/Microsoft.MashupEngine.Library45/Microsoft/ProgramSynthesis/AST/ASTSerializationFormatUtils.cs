using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008C6 RID: 2246
	public static class ASTSerializationFormatUtils
	{
		// Token: 0x06003049 RID: 12361 RVA: 0x0008E618 File Offset: 0x0008C818
		public static ASTSerializationSettings AsASTSerializationSettings(this ASTSerializationFormat format)
		{
			if (format == ASTSerializationFormat.XML)
			{
				return ASTSerializationSettings.Xml;
			}
			if (format != ASTSerializationFormat.HumanReadable)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid ASTSerializationFormat enum value: {0}", new object[] { format })), "format");
			}
			return ASTSerializationSettings.HumanReadable;
		}
	}
}
