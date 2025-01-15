using System;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Tabular.Tmdl;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001D4 RID: 468
	internal static class TmdlWriterExtensions
	{
		// Token: 0x06001C02 RID: 7170 RVA: 0x000C3AE0 File Offset: 0x000C1CE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteLineWithNoIndentation(this ITmdlWriter writer, object value)
		{
			using (writer.Indent(-1))
			{
				writer.WriteLine(value);
			}
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x000C3B18 File Offset: 0x000C1D18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteLineWithNoIndentation(this ITmdlWriter writer, string format, params object[] args)
		{
			using (writer.Indent(-1))
			{
				writer.WriteLine(format, args);
			}
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000C3B54 File Offset: 0x000C1D54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteLineWithAdditionalIndentation(this ITmdlWriter writer, int additionalIndentation, object value)
		{
			using (writer.Indent(additionalIndentation))
			{
				writer.WriteLine(value);
			}
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x000C3B8C File Offset: 0x000C1D8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteLineWithAdditionalIndentation(this ITmdlWriter writer, int additionalIndentation, string format, params object[] args)
		{
			using (writer.Indent(additionalIndentation))
			{
				writer.WriteLine(format, args);
			}
		}
	}
}
