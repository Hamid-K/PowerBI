using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DCA RID: 7626
	public static class DocumentEvaluationParametersExtensions
	{
		// Token: 0x0600BCE8 RID: 48360 RVA: 0x00265798 File Offset: 0x00263998
		public static DocumentEvaluationParameters ReplacePartition(this DocumentEvaluationParameters parameters, IPartitionKey partitionKey, SegmentedString expression)
		{
			IEnumerable<PackageEdit> enumerable = parameters.document.ReplacePartition(partitionKey, expression);
			parameters = parameters.Clone();
			parameters.document = parameters.document.ApplyEdits(enumerable);
			return parameters;
		}

		// Token: 0x0600BCE9 RID: 48361 RVA: 0x002657D0 File Offset: 0x002639D0
		public static DocumentEvaluationParameters ReferencePartition(this DocumentEvaluationParameters parameters, IPartitionKey partitionKey)
		{
			string text;
			IEnumerable<PackageEdit> enumerable = parameters.document.ReferencePartition(partitionKey, out text);
			parameters = parameters.Clone();
			parameters.document = parameters.document.ApplyEdits(enumerable);
			parameters.partitionKey = partitionKey;
			parameters.expression = text;
			return parameters;
		}

		// Token: 0x0600BCEA RID: 48362 RVA: 0x00265815 File Offset: 0x00263A15
		public static DocumentEvaluationParameters SkipAndTake(this DocumentEvaluationParameters parameters, int? skipCount, int? takeCount)
		{
			parameters = parameters.Clone();
			parameters.expression = parameters.expression.SkipAndTake(skipCount, takeCount);
			return parameters;
		}

		// Token: 0x0600BCEB RID: 48363 RVA: 0x00265833 File Offset: 0x00263A33
		public static DocumentEvaluationParameters PartitionValues(this DocumentEvaluationParameters parameters, bool partition = true)
		{
			if (partition)
			{
				parameters = parameters.Clone();
				parameters.expression = string.Format(CultureInfo.InvariantCulture, "Table.PartitionValues({0})", parameters.expression);
			}
			return parameters;
		}

		// Token: 0x0600BCEC RID: 48364 RVA: 0x0026585C File Offset: 0x00263A5C
		public static DocumentEvaluationParameters FilterWithDataTable(this DocumentEvaluationParameters parameters, IEngine engine, IVariableService variableService, DataTable dataTable)
		{
			if (dataTable != null)
			{
				string text = Guid.NewGuid().ToString();
				variableService.Add(text, dataTable);
				parameters = parameters.Clone();
				parameters.expression = string.Format(CultureInfo.InvariantCulture, "Table.FilterWithDataTable({0}, {1})", parameters.expression, engine.EscapeString(text));
			}
			return parameters;
		}

		// Token: 0x0600BCED RID: 48365 RVA: 0x002658B4 File Offset: 0x00263AB4
		public static DocumentEvaluationParameters InvokeWithArguments(this DocumentEvaluationParameters parameters, IEngine engine, IVariableService variableService, object[] arguments)
		{
			if (arguments != null)
			{
				string text = Guid.NewGuid().ToString("N");
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(");
				stringBuilder.Append(parameters.expression);
				stringBuilder.Append(")(");
				for (int i = 0; i < arguments.Length; i++)
				{
					object obj = arguments[i];
					string text2 = text + "." + i.ToString();
					variableService.Add(text2, obj);
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "Variable.Value({0})", engine.EscapeString(text2));
				}
				stringBuilder.Append(")");
				parameters = parameters.Clone();
				parameters.expression = stringBuilder.ToString();
			}
			return parameters;
		}
	}
}
