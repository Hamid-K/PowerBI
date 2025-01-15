using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B42 RID: 2882
	internal abstract class FuzzyMatcher
	{
		// Token: 0x06005002 RID: 20482 RVA: 0x0010BE44 File Offset: 0x0010A044
		public static FuzzyMatcher New(TableTypeAlgebra.JoinKind joinKind, bool isFuzzyNestedJoin)
		{
			switch (joinKind)
			{
			case TableTypeAlgebra.JoinKind.Inner:
			case TableTypeAlgebra.JoinKind.FullOuter:
			case TableTypeAlgebra.JoinKind.RightOuter:
			case TableTypeAlgebra.JoinKind.LeftAnti:
			case TableTypeAlgebra.JoinKind.RightAnti:
				return new FuzzyMatcher.NonStreamingFuzzyMatcher();
			case TableTypeAlgebra.JoinKind.LeftOuter:
				if (isFuzzyNestedJoin)
				{
					return new FuzzyMatcher.NonStreamingFuzzyMatcher();
				}
				return new FuzzyMatcher.StreamingFuzzyMatcher();
			case TableTypeAlgebra.JoinKind.LeftSemi:
			case TableTypeAlgebra.JoinKind.RightSemi:
				throw ValueException.NewExpressionError<Message0>(Strings.UnsupportedJoinKindForFuzzyJoins, null, null);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06005003 RID: 20483
		public abstract IEnumerator<IValueReference> GetJoinValueReferenceEnumerator(IEngineHost host, FuzzyJoinParameters parameters);

		// Token: 0x06005004 RID: 20484
		public abstract TableValue GetNestedJoinTableValue(IEngineHost host, RecordValue inputTableValue, int[] inputTableKeys, FuzzyMatchingReferenceTable referenceTable, Lazy<DataTable> transformationTable, FuzzyNestedJoinParameters parameters, Guid sessionId, bool isFirst, out bool joinStarted);

		// Token: 0x06005005 RID: 20485
		public abstract void FinishNestedJoin(Guid sessionid);

		// Token: 0x06005006 RID: 20486 RVA: 0x0010BEA0 File Offset: 0x0010A0A0
		private static void ValidateKeyColumnCount(int[] leftColumnKeys, int[] rightColumnKeys)
		{
			if (leftColumnKeys.Length != rightColumnKeys.Length)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.FuzzyUtilInvalidColumnKeysCount, null, null);
			}
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x0010BEB7 File Offset: 0x0010A0B7
		private static void ValidateForStreamingConcurrentRequests(int concurrentRequests)
		{
			if (concurrentRequests > 1)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.UnsupportedConcurrentRequestsForLeftOuterJoin, null, null);
			}
		}

		// Token: 0x04002AEF RID: 10991
		public const string InputTableName = "Input";

		// Token: 0x04002AF0 RID: 10992
		public const string ReferenceTableName = "Reference";

		// Token: 0x02000B43 RID: 2883
		private sealed class NonStreamingFuzzyMatcher : FuzzyMatcher
		{
			// Token: 0x06005009 RID: 20489 RVA: 0x0010BECC File Offset: 0x0010A0CC
			public override IEnumerator<IValueReference> GetJoinValueReferenceEnumerator(IEngineHost host, FuzzyJoinParameters parameters)
			{
				FuzzyMatcher.ValidateKeyColumnCount(parameters.LeftKeyColumns, parameters.RightKeyColumns);
				FuzzyUtils.ValidateTextColumns(parameters.LeftQuery, parameters.LeftKeyColumns);
				FuzzyUtils.ValidateTextColumns(parameters.RightQuery, parameters.RightKeyColumns);
				FuzzyJoinOptions joinOptions = parameters.JoinOptions;
				List<RecordValue> list;
				DataTable dataTable = FuzzyDataTableCreator.CreateFromRecords("Input", parameters.LeftQuery.GetRows(), parameters.LeftKeyColumns, out list);
				List<RecordValue> list2;
				DataTable dataTable2 = FuzzyDataTableCreator.CreateFromRecords("Reference", parameters.RightQuery.GetRows(), parameters.RightKeyColumns, out list2);
				DataTable dataTable3 = FuzzyDataTableCreator.CreateTransformationDataTableFromTableValue(joinOptions.TransformationTable);
				return FuzzyDataTableCreator.ToRecordsEnumerator(ExternalFuzzyMatcher.ExecuteFuzzyJoin(host, dataTable, dataTable2, dataTable3, joinOptions, parameters.JoinKind, default(Guid), false), parameters, list, list2);
			}

			// Token: 0x0600500A RID: 20490 RVA: 0x0010BF84 File Offset: 0x0010A184
			public override TableValue GetNestedJoinTableValue(IEngineHost host, RecordValue inputTableValue, int[] inputTableKeys, FuzzyMatchingReferenceTable referenceTable, Lazy<DataTable> transformationTable, FuzzyNestedJoinParameters parameters, Guid sessionId, bool isFirst, out bool joinStarted)
			{
				FuzzyMatcher.ValidateKeyColumnCount(inputTableKeys, referenceTable.Keys);
				FuzzyUtils.ValidateTextColumns(inputTableValue, inputTableKeys);
				FuzzyUtils.ValidateTextColumns(referenceTable.Table.Query, referenceTable.Keys);
				FuzzyJoinOptions joinOptions = parameters.JoinOptions;
				List<RecordValue> list;
				DataTable dataTable = FuzzyDataTableCreator.CreateFromRecord("Input", inputTableValue, inputTableKeys, out list);
				DataTable dataTable2 = ExternalFuzzyMatcher.ExecuteFuzzyJoin(host, dataTable, referenceTable.DataTable, transformationTable.Value, joinOptions, parameters.JoinKind, sessionId, isFirst);
				joinStarted = true;
				int num = parameters.LeftKeyColumns.Length;
				int count = dataTable2.Rows.Count;
				List<RecordValue> list2 = new List<RecordValue>();
				for (int i = 0; i < count; i++)
				{
					if (dataTable2.Rows[i][0] != DBNull.Value)
					{
						int num2 = (int)dataTable2.Rows[i][0];
						RecordValue recordValue = list[num2];
					}
					if (dataTable2.Rows[i][num + 1] != DBNull.Value)
					{
						int num3 = (int)dataTable2.Rows[i][num + 1];
						RecordValue recordValue2 = referenceTable.Records[num3].AsRecord;
						if (parameters.JoinOptions.SimilarityColumnName != null)
						{
							object obj = dataTable2.Rows[i][(num + 1) * 2];
							Value value = ((obj != DBNull.Value) ? NumberValue.New(FuzzyUtils.TruncateSimilarity((double)obj)) : Value.Null);
							recordValue2 = Library.Record.AddField.Invoke(recordValue2, TextValue.New(parameters.JoinOptions.SimilarityColumnName), value).AsRecord;
						}
						list2.Add(recordValue2);
					}
				}
				ListValue listValue;
				if (list2.Count != 0)
				{
					Value[] array = list2.ToArray();
					listValue = ListValue.New(array);
				}
				else
				{
					listValue = ListValue.Empty;
				}
				ListValue listValue2 = listValue;
				return TableModule.Table.FromRecords.Invoke(listValue2).AsTable;
			}

			// Token: 0x0600500B RID: 20491 RVA: 0x0010C16F File Offset: 0x0010A36F
			public override void FinishNestedJoin(Guid sessionId)
			{
				ExternalFuzzyMatcher.FinishJoin(sessionId);
			}
		}

		// Token: 0x02000B44 RID: 2884
		private sealed class StreamingFuzzyMatcher : FuzzyMatcher
		{
			// Token: 0x0600500D RID: 20493 RVA: 0x0010C180 File Offset: 0x0010A380
			public override IEnumerator<IValueReference> GetJoinValueReferenceEnumerator(IEngineHost host, FuzzyJoinParameters parameters)
			{
				FuzzyMatcher.ValidateForStreamingConcurrentRequests(parameters.JoinOptions.ConcurrentRequests);
				FuzzyMatcher.ValidateKeyColumnCount(parameters.LeftKeyColumns, parameters.RightKeyColumns);
				FuzzyUtils.ValidateTextColumns(parameters.LeftQuery, parameters.LeftKeyColumns);
				FuzzyUtils.ValidateTextColumns(parameters.RightQuery, parameters.RightKeyColumns);
				FuzzyJoinOptions joinOptions = parameters.JoinOptions;
				IDataReader dataReader = new FuzzyInputDataReader("Input", parameters.LeftQuery, parameters.LeftKeyColumns);
				List<RecordValue> list;
				DataTable dataTable = FuzzyDataTableCreator.CreateFromRecords("Reference", parameters.RightQuery.GetRows(), parameters.RightKeyColumns, out list);
				DataTable dataTable2 = FuzzyDataTableCreator.CreateTransformationDataTableFromTableValue(joinOptions.TransformationTable);
				return new FuzzyOutputEnumerator(ExternalFuzzyMatcher.ExecuteFuzzyJoinStreaming(host, dataReader, dataTable, dataTable2, joinOptions, parameters.JoinKind, default(Guid), false), parameters, dataReader, list);
			}

			// Token: 0x0600500E RID: 20494 RVA: 0x000091AE File Offset: 0x000073AE
			public override TableValue GetNestedJoinTableValue(IEngineHost host, RecordValue inputTableValue, int[] inputTableKeys, FuzzyMatchingReferenceTable referenceTable, Lazy<DataTable> transformationTable, FuzzyNestedJoinParameters parameters, Guid sessionId, bool isFirst, out bool joinStarted)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600500F RID: 20495 RVA: 0x0010C23D File Offset: 0x0010A43D
			public override void FinishNestedJoin(Guid sessionId)
			{
				ExternalFuzzyMatcher.FinishJoinStreaming(sessionId);
			}
		}
	}
}
