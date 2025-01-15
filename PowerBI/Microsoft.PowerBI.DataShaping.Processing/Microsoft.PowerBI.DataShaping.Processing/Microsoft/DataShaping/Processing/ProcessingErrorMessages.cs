using System;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x02000011 RID: 17
	internal static class ProcessingErrorMessages
	{
		// Token: 0x0600007A RID: 122 RVA: 0x000029DE File Offset: 0x00000BDE
		public static string QueryExtensionMeasureError(string measureName, int lineNumber, int position)
		{
			return ProcessingErrorMessages.QueryExtensionError("measure", measureName, lineNumber, position);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000029ED File Offset: 0x00000BED
		public static string QueryExtensionColumnError(string columnName, int lineNumber, int position)
		{
			return ProcessingErrorMessages.QueryExtensionError("column", columnName, lineNumber, position);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000029FC File Offset: 0x00000BFC
		public static string WrongCalculationValueType(string calculationId, Type expectedType, Type encounteredType)
		{
			return ProcessingErrorMessages.CreateMessage("Calculation {0} with type {1} encountered a value with incorrect type {2}. This is probably due to a bug in a Data Transform.", new object[] { calculationId, expectedType, encounteredType });
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002A1A File Offset: 0x00000C1A
		public static string QueryExtensionMeasureUnexpectedEndOfUserInput(string measureName)
		{
			return ProcessingErrorMessages.QueryExtensionUnexpectedEndOfUserInput("measure", measureName);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002A27 File Offset: 0x00000C27
		public static string QueryExtensionColumnUnexpectedEndOfUserInput(string columnName)
		{
			return ProcessingErrorMessages.QueryExtensionUnexpectedEndOfUserInput("column", columnName);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002A34 File Offset: 0x00000C34
		private static string QueryExtensionError(string type, string propertyName, int lineNumber, int position)
		{
			return ProcessingErrorMessages.CreateMessage("The user-supplied DAX {0} {1} had a syntax or semantic error at line {2}, position {3}.", new object[] { type, propertyName, lineNumber, position });
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002A60 File Offset: 0x00000C60
		private static string QueryExtensionUnexpectedEndOfUserInput(string type, string propertyName)
		{
			return ProcessingErrorMessages.CreateMessage("The user-supplied DAX {0} {1} ended unexpectedly. The expression is most likely missing an argument to an arithmetic or semantic operator.", new object[] { type, propertyName });
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002A7A File Offset: 0x00000C7A
		public static string DataExtensionMissingResultSet(int index)
		{
			return ProcessingErrorMessages.CreateMessage("The data extension did not return the result set at index {0}. This is an issue often seen with old on premise gateways that return only a subset of the results.", new object[] { index });
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002A98 File Offset: 0x00000C98
		private static string CreateMessage(string messageTemplate, params object[] args)
		{
			object[] array = ProcessingErrorMessages.StringifyArguments(args);
			return StringUtil.FormatInvariant(messageTemplate, array);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002AB4 File Offset: 0x00000CB4
		private static string[] StringifyArguments(object[] arguments)
		{
			if (arguments == null)
			{
				return new string[0];
			}
			string[] array = new string[arguments.Length];
			for (int i = 0; i < arguments.Length; i++)
			{
				object obj = arguments[i];
				string text = obj as string;
				if (text != null)
				{
					array[i] = text;
				}
				else
				{
					array[i] = obj.ToString();
				}
			}
			return array;
		}
	}
}
