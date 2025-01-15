using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000043 RID: 67
	public class FuzzyLookupEntry
	{
		// Token: 0x06000293 RID: 659 RVA: 0x0000C520 File Offset: 0x0000A720
		static FuzzyLookupEntry()
		{
			FuzzyLookupEntry.emptyTransformationTable.Columns.Add("From", typeof(string));
			FuzzyLookupEntry.emptyTransformationTable.Columns.Add("To", typeof(string));
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000C58C File Offset: 0x0000A78C
		public static FuzzyLookupBuilder CreateFuzzyLookupBuilder(DataTable transformTable, FuzzyLookupEntry.FuzzyLookupParameters parameters)
		{
			return new FuzzyLookupBuilder
			{
				MatchAcrossColumns = false,
				TransformationsDataTable = transformTable,
				EditThreshold = 0.65,
				MaxEditRulesPerToken = 10,
				IgnoreCase = parameters.IgnoreCase,
				IgnoreNonSpacing = true,
				MinQueryThreshold = parameters.SimilarityThreshold,
				EnableTokenMergeTransformations = parameters.IgnoreSpace,
				ContainmentBias = 0.5
			};
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C600 File Offset: 0x0000A800
		private static void Dispose(FuzzyLookupEntry.MultiThreadingTestThread[] threads, WaitHandle[] waitHandles)
		{
			if (threads != null)
			{
				int num = threads.Length;
				for (int i = 0; i < num; i++)
				{
					if (threads[i] != null)
					{
						threads[i].Dispose();
						threads[i] = null;
					}
				}
			}
			if (waitHandles != null)
			{
				int num2 = waitHandles.Length;
				for (int j = 0; j < num2; j++)
				{
					if (waitHandles[j] != null)
					{
						waitHandles[j].Close();
						waitHandles[j] = null;
					}
				}
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C658 File Offset: 0x0000A858
		public static DataTable Join(DataTable inputTable, DataTable referenceTable, DataTable transformationsTable, FuzzyLookupEntry.FuzzyLookupParameters parameters, FuzzyLookupEntry.JoinType joinType)
		{
			if (inputTable == null)
			{
				throw new ArgumentNullException("Input Table");
			}
			if (referenceTable == null)
			{
				throw new ArgumentNullException("Reference Table");
			}
			if (inputTable.Columns.Count != referenceTable.Columns.Count)
			{
				throw new ArgumentException("Inconsistent number of columns of input table and reference table");
			}
			if (inputTable.Columns.Count < 2)
			{
				throw new ArgumentException("Table must have 2 or more columns: ID column and data columns");
			}
			if (inputTable.Columns[0].DataType != typeof(int))
			{
				throw new ArgumentException("The 1st column of input table must be IDs (int)");
			}
			if (referenceTable.Columns[0].DataType != typeof(int))
			{
				throw new ArgumentException("The 1st column of reference table must be IDs (int)");
			}
			for (int i = 1; i < inputTable.Columns.Count; i++)
			{
				if (inputTable.Columns[i].DataType != typeof(string))
				{
					throw new ArgumentException("The data column [" + i + "] of input table must be strings");
				}
			}
			for (int j = 1; j < referenceTable.Columns.Count; j++)
			{
				if (referenceTable.Columns[j].DataType != typeof(string))
				{
					throw new ArgumentException("The data column [" + j + "] of reference table must be strings");
				}
			}
			FuzzyLookupBuilder fuzzyLookupBuilder = FuzzyLookupEntry.CreateFuzzyLookupBuilder(transformationsTable, parameters);
			List<string> list = new List<string>();
			int count = referenceTable.Columns.Count;
			for (int k = 1; k < count; k++)
			{
				list.Add(referenceTable.Columns[k].ColumnName);
			}
			FuzzyLookup fuzzyLookup = fuzzyLookupBuilder.CreateFuzzyLookup(referenceTable, parameters, list.ToArray());
			int num = Math.Min(parameters.Concurrency, inputTable.Rows.Count);
			num = Math.Max(num, 1);
			FuzzyLookupEntry.MultiThreadingTestThread[] array = new FuzzyLookupEntry.MultiThreadingTestThread[num];
			WaitHandle[] array2 = new WaitHandle[num];
			int num2 = inputTable.Rows.Count / num;
			int num3 = inputTable.Rows.Count % num;
			int num4 = 0;
			for (int l = 0; l < num; l++)
			{
				int num5 = num4 + num2;
				if (l < num3)
				{
					num5++;
				}
				FuzzyQuery fuzzyQuery = fuzzyLookupBuilder.CreateFuzzyQuery(fuzzyLookup, parameters);
				array[l] = new FuzzyLookupEntry.MultiThreadingTestThread(joinType, fuzzyQuery, inputTable, num4, num5);
				array2[l] = array[l].CompletionEvent;
				array[l].Thread = new Thread(new ThreadStart(array[l].PerformMatch));
				array[l].Thread.Start();
				num4 = num5;
			}
			WaitHandle.WaitAll(array2);
			HashSet<int> hashSet = new HashSet<int>();
			DataTable dataTable = array[0].m_outputTable.Clone();
			dataTable.TableName = "Output Table";
			dataTable.Rows.Clear();
			foreach (FuzzyLookupEntry.MultiThreadingTestThread multiThreadingTestThread in array)
			{
				foreach (object obj in multiThreadingTestThread.m_outputTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataTable.Rows.Add(dataRow.ItemArray);
				}
				hashSet.UnionWith(multiThreadingTestThread.matchedRightIdSet);
			}
			if (joinType == FuzzyLookupEntry.JoinType.RightOuter || joinType == FuzzyLookupEntry.JoinType.FullOuter || joinType == FuzzyLookupEntry.JoinType.RightAnti)
			{
				int count2 = inputTable.Columns.Count;
				int count3 = dataTable.Columns.Count;
				foreach (object obj2 in referenceTable.Rows)
				{
					DataRow dataRow2 = (DataRow)obj2;
					if (!hashSet.Contains((int)dataRow2[0]))
					{
						object[] array4 = new object[count3];
						for (int n = 0; n < count2; n++)
						{
							array4[n + count2] = dataRow2[n];
						}
						dataTable.Rows.Add(array4);
					}
				}
			}
			if (parameters.SortByScore)
			{
				dataTable.DefaultView.Sort = "[Similarity] DESC";
				dataTable = dataTable.DefaultView.ToTable();
			}
			FuzzyLookupEntry.Dispose(array, array2);
			array = null;
			array2 = null;
			return dataTable;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000CAAC File Offset: 0x0000ACAC
		public static DataTable Join(DataTable inputTable, DataTable referenceTable, FuzzyLookupEntry.FuzzyLookupParameters parameters, FuzzyLookupEntry.JoinType joinType)
		{
			return FuzzyLookupEntry.Join(inputTable, referenceTable, FuzzyLookupEntry.emptyTransformationTable, parameters, joinType);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000CABC File Offset: 0x0000ACBC
		private static IEnumerable<object[]> Join(IDataReader inputDataReader, DataTable referenceTable, FuzzyQuery fuzzyQuery, FuzzyLookupEntry.JoinType joinType)
		{
			HashSet<int> matchedRightIdSet = new HashSet<int>();
			bool keepUnmatchedLeft = joinType == FuzzyLookupEntry.JoinType.LeftOuter || joinType == FuzzyLookupEntry.JoinType.FullOuter || joinType == FuzzyLookupEntry.JoinType.LeftAnti;
			bool collectMatchedRightID = joinType == FuzzyLookupEntry.JoinType.RightOuter || joinType == FuzzyLookupEntry.JoinType.FullOuter || joinType == FuzzyLookupEntry.JoinType.RightAnti;
			int inputColumnCount = inputDataReader.GetSchemaTable().Rows.Count;
			int outputColumnCount = (inputColumnCount << 1) + 1;
			while (inputDataReader.Read())
			{
				bool matchesFound = false;
				using (MatchResultsReader resultsReader = fuzzyQuery.Match(inputDataReader))
				{
					while (resultsReader.Read())
					{
						object[] values = new object[outputColumnCount];
						resultsReader.GetValues(values);
						values[values.Length - 1] = resultsReader.ComparisonResult.Similarity;
						matchesFound = true;
						if (joinType != FuzzyLookupEntry.JoinType.LeftAnti && joinType != FuzzyLookupEntry.JoinType.RightAnti)
						{
							yield return values;
						}
						if (collectMatchedRightID)
						{
							object obj = values[inputColumnCount];
							matchedRightIdSet.Add((int)obj);
						}
						values = null;
					}
					if (!matchesFound && keepUnmatchedLeft)
					{
						object[] array = new object[outputColumnCount];
						resultsReader.InputRecord.GetValues(array);
						for (int i = inputColumnCount; i < outputColumnCount; i++)
						{
							array[i] = DBNull.Value;
						}
						yield return array;
					}
				}
				MatchResultsReader resultsReader = null;
			}
			if (collectMatchedRightID)
			{
				foreach (object obj2 in referenceTable.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					if (!matchedRightIdSet.Contains((int)dataRow[0]))
					{
						object[] array2 = new object[outputColumnCount];
						for (int j = 0; j < inputColumnCount; j++)
						{
							array2[j + inputColumnCount] = dataRow[j];
						}
						for (int k = 0; k < inputColumnCount; k++)
						{
							array2[k] = DBNull.Value;
						}
						array2[outputColumnCount - 1] = DBNull.Value;
						yield return array2;
					}
				}
				IEnumerator enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		public static IDataReader Join(IDataReader inputDataReader, DataTable referenceTable, DataTable transformationsTable, FuzzyLookupEntry.FuzzyLookupParameters parameters, FuzzyLookupEntry.JoinType joinType)
		{
			if (inputDataReader == null)
			{
				throw new ArgumentNullException("Input Table");
			}
			if (referenceTable == null)
			{
				throw new ArgumentNullException("Reference Table");
			}
			if (parameters.Concurrency != 1)
			{
				throw new ArgumentException("Streaming join should be single threaded");
			}
			if (parameters.SortByScore)
			{
				throw new ArgumentException("Streaming join cannot sort by score");
			}
			DataTable schemaTable = inputDataReader.GetSchemaTable();
			if (schemaTable.Rows.Count != referenceTable.Columns.Count)
			{
				throw new ArgumentException("Inconsistent number of columns of input table and reference table");
			}
			if (schemaTable.Rows.Count < 2)
			{
				throw new ArgumentException("Table must have 2 or more columns: ID column and data columns");
			}
			if ((Type)schemaTable.Rows[0][SchemaTableColumn.DataType] != typeof(int))
			{
				throw new ArgumentException("The 1st column of input table must be IDs (int)");
			}
			if (referenceTable.Columns[0].DataType != typeof(int))
			{
				throw new ArgumentException("The 1st column of reference table must be IDs (int)");
			}
			for (int i = 1; i < schemaTable.Rows.Count; i++)
			{
				if ((Type)schemaTable.Rows[i][SchemaTableColumn.DataType] != typeof(string))
				{
					throw new ArgumentException("The data column [" + i + "] of input table must be strings");
				}
			}
			for (int j = 1; j < referenceTable.Columns.Count; j++)
			{
				if (referenceTable.Columns[j].DataType != typeof(string))
				{
					throw new ArgumentException("The data column [" + j + "] of reference table must be strings");
				}
			}
			FuzzyLookupBuilder fuzzyLookupBuilder = FuzzyLookupEntry.CreateFuzzyLookupBuilder(transformationsTable, parameters);
			List<string> list = new List<string>();
			int count = schemaTable.Rows.Count;
			for (int k = 1; k < count; k++)
			{
				list.Add(referenceTable.Columns[k].ColumnName);
			}
			FuzzyLookup fuzzyLookup = fuzzyLookupBuilder.CreateFuzzyLookup(referenceTable, parameters, list.ToArray());
			FuzzyQuery fuzzyQuery = fuzzyLookupBuilder.CreateFuzzyQuery(fuzzyLookup, parameters);
			DataTable dataTable = new DataTable("Output Table");
			foreach (object obj in fuzzyQuery.MatchResultSchema.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataTable.Columns.Add(dataRow[SchemaTableColumn.ColumnName] as string, dataRow[SchemaTableColumn.DataType] as Type);
			}
			return new FuzzyLookupEntry.OutputDataReader(FuzzyLookupEntry.Join(inputDataReader, referenceTable, fuzzyQuery, joinType).GetEnumerator(), dataTable.CreateDataReader().GetSchemaTable());
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000CD90 File Offset: 0x0000AF90
		private static IEnumerable<object[]> StartJoin(IDataReader inputDataReader, DataTable referenceDataTable, FuzzyLookupEntry.StateStream state)
		{
			int inputColumnCount = state.singleTableColumnCount;
			int outputColumnCount = (state.singleTableColumnCount << 1) + 1;
			while (inputDataReader.Read())
			{
				bool matchesFound = false;
				using (MatchResultsReader resultsReader = state.fuzzyQuery.Match(inputDataReader))
				{
					while (resultsReader.Read())
					{
						object[] array = new object[outputColumnCount];
						resultsReader.GetValues(array);
						array[array.Length - 1] = resultsReader.ComparisonResult.Similarity;
						matchesFound = true;
						if (state.joinType != FuzzyLookupEntry.JoinType.LeftAnti && state.joinType != FuzzyLookupEntry.JoinType.RightAnti)
						{
							yield return array;
						}
					}
					if (!matchesFound)
					{
						object[] array2 = new object[outputColumnCount];
						resultsReader.InputRecord.GetValues(array2);
						for (int i = inputColumnCount; i < outputColumnCount; i++)
						{
							array2[i] = DBNull.Value;
						}
						yield return array2;
					}
				}
				MatchResultsReader resultsReader = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000CDA8 File Offset: 0x0000AFA8
		public static IDataReader StartJoin(Guid sessionGuid, IDataReader inputDataReader, DataTable referenceDataTable, DataTable transformationsTable, FuzzyLookupEntry.FuzzyLookupParameters parameters, FuzzyLookupEntry.JoinType joinType)
		{
			if (joinType != FuzzyLookupEntry.JoinType.LeftOuter)
			{
				throw new ArgumentException("Session-based streaming join is designed only for Left Outer");
			}
			if (inputDataReader == null)
			{
				throw new ArgumentNullException("Input Table");
			}
			if (referenceDataTable == null)
			{
				throw new ArgumentNullException("Reference Table");
			}
			if (parameters.Concurrency != 1)
			{
				throw new ArgumentException("Streaming join should be single threaded");
			}
			if (parameters.SortByScore)
			{
				throw new ArgumentException("Streaming join cannot sort by score");
			}
			DataTable schemaTable = inputDataReader.GetSchemaTable();
			if (schemaTable.Rows.Count != referenceDataTable.Columns.Count)
			{
				throw new ArgumentException("Inconsistent numbers of columns of input table and reference table");
			}
			if (schemaTable.Rows.Count < 2)
			{
				throw new ArgumentException("Table must have 2 or more columns: ID column and data columns");
			}
			if (referenceDataTable.Columns[0].DataType != typeof(int))
			{
				throw new ArgumentException("The 1st column of reference table must be IDs (int)");
			}
			if ((Type)schemaTable.Rows[0][SchemaTableColumn.DataType] != typeof(int))
			{
				throw new ArgumentException("The 1st column of input table must be IDs (int)");
			}
			for (int i = 1; i < schemaTable.Rows.Count; i++)
			{
				if ((Type)schemaTable.Rows[i][SchemaTableColumn.DataType] != typeof(string))
				{
					throw new ArgumentException("The data column [" + i + "] of input table must be strings");
				}
			}
			for (int j = 1; j < referenceDataTable.Columns.Count; j++)
			{
				if (referenceDataTable.Columns[j].DataType != typeof(string))
				{
					throw new ArgumentException("The data column [" + j + "] of reference table must be strings");
				}
			}
			if (FuzzyLookupEntry.streamStates.ContainsKey(sessionGuid))
			{
				throw new ArgumentException(string.Format("Session {0} has already started", sessionGuid.ToString()));
			}
			FuzzyLookupEntry.StateStream stateStream = new FuzzyLookupEntry.StateStream();
			FuzzyLookupEntry.streamStates[sessionGuid] = stateStream;
			stateStream.guid = sessionGuid;
			stateStream.joinType = joinType;
			stateStream.parameters = new FuzzyLookupEntry.FuzzyLookupParameters(parameters);
			stateStream.singleTableColumnCount = schemaTable.Rows.Count;
			stateStream.builder = FuzzyLookupEntry.CreateFuzzyLookupBuilder(transformationsTable, parameters);
			List<string> list = new List<string>();
			int count = schemaTable.Rows.Count;
			for (int k = 1; k < count; k++)
			{
				list.Add(referenceDataTable.Columns[k].ColumnName);
			}
			stateStream.fuzzyLookup = stateStream.builder.CreateFuzzyLookup(referenceDataTable, parameters, list.ToArray());
			stateStream.fuzzyQuery = stateStream.builder.CreateFuzzyQuery(stateStream.fuzzyLookup, parameters);
			DataTable dataTable = new DataTable("Output Table");
			foreach (object obj in stateStream.fuzzyQuery.MatchResultSchema.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataTable.Columns.Add(dataRow[SchemaTableColumn.ColumnName] as string, dataRow[SchemaTableColumn.DataType] as Type);
			}
			stateStream.outputSchemaTable = dataTable.CreateDataReader().GetSchemaTable();
			return new FuzzyLookupEntry.OutputDataReader(FuzzyLookupEntry.StartJoin(inputDataReader, referenceDataTable, stateStream).GetEnumerator(), stateStream.outputSchemaTable);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		private static IEnumerable<object[]> ContinueJoin(IDataReader inputDataReader, FuzzyLookupEntry.StateStream state)
		{
			int inputColumnCount = state.singleTableColumnCount;
			int outputColumnCount = (state.singleTableColumnCount << 1) + 1;
			while (inputDataReader.Read())
			{
				bool matchesFound = false;
				using (MatchResultsReader resultsReader = state.fuzzyQuery.Match(inputDataReader))
				{
					while (resultsReader.Read())
					{
						object[] array = new object[outputColumnCount];
						resultsReader.GetValues(array);
						array[array.Length - 1] = resultsReader.ComparisonResult.Similarity;
						matchesFound = true;
						yield return array;
					}
					if (!matchesFound)
					{
						object[] array2 = new object[outputColumnCount];
						resultsReader.InputRecord.GetValues(array2);
						for (int i = inputColumnCount; i < outputColumnCount; i++)
						{
							array2[i] = DBNull.Value;
						}
						yield return array2;
					}
				}
				MatchResultsReader resultsReader = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000D114 File Offset: 0x0000B314
		public static IDataReader ContinueJoin(Guid sessionGuid, IDataReader inputDataReader)
		{
			FuzzyLookupEntry.StateStream stateStream = null;
			if (!FuzzyLookupEntry.streamStates.TryGetValue(sessionGuid, ref stateStream))
			{
				throw new ArgumentException(string.Format("Session guid {0} not found", sessionGuid.ToString()));
			}
			if (inputDataReader == null)
			{
				throw new ArgumentNullException("Input Table");
			}
			return new FuzzyLookupEntry.OutputDataReader(FuzzyLookupEntry.ContinueJoin(inputDataReader, stateStream).GetEnumerator(), stateStream.outputSchemaTable);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000D174 File Offset: 0x0000B374
		public static void FinishJoinStreaming(Guid sessionGuid)
		{
			FuzzyLookupEntry.StateStream stateStream = null;
			if (!FuzzyLookupEntry.streamStates.TryGetValue(sessionGuid, ref stateStream))
			{
				throw new ArgumentException(string.Format("Session guid {0} not found", sessionGuid.ToString()));
			}
			stateStream.builder = null;
			if (stateStream.fuzzyQuery != null)
			{
				stateStream.fuzzyQuery.Dispose();
				stateStream.fuzzyQuery = null;
			}
			if (stateStream.fuzzyLookup != null)
			{
				stateStream.fuzzyLookup.Dispose();
				stateStream.fuzzyLookup = null;
			}
			FuzzyLookupEntry.streamStates.Remove(sessionGuid);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		public static DataTable StartJoin(Guid sessionGuid, DataTable inputTable, DataTable referenceTable, DataTable transformationsTable, FuzzyLookupEntry.FuzzyLookupParameters parameters, FuzzyLookupEntry.JoinType joinType)
		{
			if (joinType != FuzzyLookupEntry.JoinType.LeftOuter)
			{
				throw new ArgumentException("Session-based join is only designed for Left Outer");
			}
			if (inputTable == null)
			{
				throw new ArgumentNullException("Input Table");
			}
			if (referenceTable == null)
			{
				throw new ArgumentNullException("Reference Table");
			}
			if (inputTable.Columns.Count != referenceTable.Columns.Count)
			{
				throw new ArgumentException("Inconsistent number of columns of input table and reference table");
			}
			if (inputTable.Columns.Count < 2)
			{
				throw new ArgumentException("Table must have 2 or more columns: ID column and data columns");
			}
			if (inputTable.Columns[0].DataType != typeof(int))
			{
				throw new ArgumentException("The 1st column of input table must be IDs (int)");
			}
			if (referenceTable.Columns[0].DataType != typeof(int))
			{
				throw new ArgumentException("The 1st column of reference table must be IDs (int)");
			}
			for (int i = 1; i < inputTable.Columns.Count; i++)
			{
				if (inputTable.Columns[i].DataType != typeof(string))
				{
					throw new ArgumentException("The data column [" + i + "] of input table must be strings");
				}
			}
			for (int j = 1; j < referenceTable.Columns.Count; j++)
			{
				if (referenceTable.Columns[j].DataType != typeof(string))
				{
					throw new ArgumentException("The data column [" + j + "] of reference table must be strings");
				}
			}
			if (FuzzyLookupEntry.batchStates.ContainsKey(sessionGuid))
			{
				throw new ArgumentException(string.Format("Session {0} has already started", sessionGuid.ToString()));
			}
			FuzzyLookupEntry.StateBatch stateBatch = new FuzzyLookupEntry.StateBatch();
			FuzzyLookupEntry.batchStates[sessionGuid] = stateBatch;
			stateBatch.guid = sessionGuid;
			stateBatch.joinType = joinType;
			stateBatch.parameters = new FuzzyLookupEntry.FuzzyLookupParameters(parameters);
			stateBatch.singleTableColumnCount = inputTable.Columns.Count;
			stateBatch.builder = FuzzyLookupEntry.CreateFuzzyLookupBuilder(transformationsTable, parameters);
			List<string> list = new List<string>();
			int count = referenceTable.Columns.Count;
			for (int k = 1; k < count; k++)
			{
				list.Add(referenceTable.Columns[k].ColumnName);
			}
			stateBatch.fuzzyLookup = stateBatch.builder.CreateFuzzyLookup(referenceTable, parameters, list.ToArray());
			int num = Math.Min(parameters.Concurrency, inputTable.Rows.Count);
			num = Math.Max(num, 1);
			stateBatch.threads = new FuzzyLookupEntry.MultiThreadingTestThread[num];
			stateBatch.waitHandles = new WaitHandle[num];
			int num2 = inputTable.Rows.Count / num;
			int num3 = inputTable.Rows.Count % num;
			int num4 = 0;
			for (int l = 0; l < num; l++)
			{
				int num5 = num4 + num2;
				if (l < num3)
				{
					num5++;
				}
				FuzzyQuery fuzzyQuery = stateBatch.builder.CreateFuzzyQuery(stateBatch.fuzzyLookup, parameters);
				stateBatch.threads[l] = new FuzzyLookupEntry.MultiThreadingTestThread(joinType, fuzzyQuery, inputTable, num4, num5);
				stateBatch.waitHandles[l] = stateBatch.threads[l].CompletionEvent;
				stateBatch.threads[l].Thread = new Thread(new ThreadStart(stateBatch.threads[l].PerformMatch));
				stateBatch.threads[l].Thread.Start();
				num4 = num5;
			}
			WaitHandle.WaitAll(stateBatch.waitHandles);
			stateBatch.outputTableSchema = stateBatch.threads[0].m_outputTable.Clone();
			DataTable dataTable = stateBatch.outputTableSchema.Clone();
			dataTable.TableName = "Output Table";
			FuzzyLookupEntry.MultiThreadingTestThread[] threads = stateBatch.threads;
			for (int m = 0; m < threads.Length; m++)
			{
				foreach (object obj in threads[m].m_outputTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataTable.Rows.Add(dataRow.ItemArray);
				}
			}
			if (stateBatch.parameters.SortByScore)
			{
				dataTable.DefaultView.Sort = "[Similarity] DESC";
				dataTable = dataTable.DefaultView.ToTable();
			}
			return dataTable;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000D628 File Offset: 0x0000B828
		public static DataTable ContinueJoin(Guid sessionGuid, DataTable inputTable)
		{
			FuzzyLookupEntry.StateBatch stateBatch = null;
			if (!FuzzyLookupEntry.batchStates.TryGetValue(sessionGuid, ref stateBatch))
			{
				throw new ArgumentException(string.Format("Session guid {0} not found", sessionGuid.ToString()));
			}
			if (inputTable == null)
			{
				throw new ArgumentNullException("Input Table");
			}
			int num = stateBatch.threads.Length;
			int num2 = inputTable.Rows.Count / num;
			int num3 = inputTable.Rows.Count % num;
			int num4 = 0;
			for (int i = 0; i < num; i++)
			{
				int num5 = num4 + num2;
				if (i < num3)
				{
					num5++;
				}
				stateBatch.threads[i].ResetInputTable(inputTable, num4, num5);
				stateBatch.threads[i].Thread = new Thread(new ThreadStart(stateBatch.threads[i].PerformMatch));
				stateBatch.threads[i].Thread.Start();
				num4 = num5;
			}
			WaitHandle.WaitAll(stateBatch.waitHandles);
			DataTable dataTable = stateBatch.outputTableSchema.Clone();
			dataTable.TableName = "Output Table";
			FuzzyLookupEntry.MultiThreadingTestThread[] threads = stateBatch.threads;
			for (int j = 0; j < threads.Length; j++)
			{
				foreach (object obj in threads[j].m_outputTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataTable.Rows.Add(dataRow.ItemArray);
				}
			}
			if (stateBatch.parameters.SortByScore)
			{
				dataTable.DefaultView.Sort = "[Similarity] DESC";
				dataTable = dataTable.DefaultView.ToTable();
			}
			return dataTable;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000D7E8 File Offset: 0x0000B9E8
		public static void FinishJoin(Guid sessionGuid)
		{
			FuzzyLookupEntry.StateBatch stateBatch = null;
			if (!FuzzyLookupEntry.batchStates.TryGetValue(sessionGuid, ref stateBatch))
			{
				throw new ArgumentException(string.Format("Session guid {0} not found", sessionGuid.ToString()));
			}
			FuzzyLookupEntry.Dispose(stateBatch.threads, stateBatch.waitHandles);
			stateBatch.threads = null;
			stateBatch.waitHandles = null;
			stateBatch.builder = null;
			if (stateBatch.fuzzyLookup != null)
			{
				stateBatch.fuzzyLookup.Dispose();
				stateBatch.fuzzyLookup = null;
			}
			FuzzyLookupEntry.batchStates.Remove(sessionGuid);
		}

		// Token: 0x040000D1 RID: 209
		private static DataTable emptyTransformationTable = new DataTable();

		// Token: 0x040000D2 RID: 210
		private static Dictionary<Guid, FuzzyLookupEntry.StateBatch> batchStates = new Dictionary<Guid, FuzzyLookupEntry.StateBatch>();

		// Token: 0x040000D3 RID: 211
		private static Dictionary<Guid, FuzzyLookupEntry.StateStream> streamStates = new Dictionary<Guid, FuzzyLookupEntry.StateStream>();

		// Token: 0x02000133 RID: 307
		public class FuzzyLookupParameters
		{
			// Token: 0x06000BFB RID: 3067 RVA: 0x00033E30 File Offset: 0x00032030
			public FuzzyLookupParameters(double similarityThreshold = 0.8, bool ignoreCase = true, bool ignoreSpace = false, int numberOfMatches = 2147483647, string culture = "en-US", int concurrency = 1, bool sortByScore = false)
			{
				if (concurrency < 1 || concurrency > 8)
				{
					throw new ArgumentException("Concurrency should be within [1..8].");
				}
				if (numberOfMatches < 0)
				{
					throw new ArgumentException("NumberOfMatches should be non-negative.");
				}
				this.LocaleId = new CultureInfo(culture, false).LCID;
				this.SimilarityThreshold = similarityThreshold;
				this.IgnoreCase = ignoreCase;
				this.IgnoreSpace = ignoreSpace;
				this.NumberOfMatches = numberOfMatches;
				this.Concurrency = concurrency;
				this.SortByScore = sortByScore;
			}

			// Token: 0x06000BFC RID: 3068 RVA: 0x00033ED8 File Offset: 0x000320D8
			public FuzzyLookupParameters(FuzzyLookupEntry.FuzzyLookupParameters other)
			{
				this.SimilarityThreshold = other.SimilarityThreshold;
				this.IgnoreCase = other.IgnoreCase;
				this.IgnoreSpace = other.IgnoreSpace;
				this.NumberOfMatches = other.NumberOfMatches;
				this.LocaleId = other.LocaleId;
				this.Concurrency = other.Concurrency;
				this.SortByScore = other.SortByScore;
			}

			// Token: 0x06000BFD RID: 3069 RVA: 0x00033F70 File Offset: 0x00032170
			public FuzzyLookupParameters(double similarityThreshold, bool ignoreCase, bool ignoreSpace, int numberOfMatches, int localeId, int concurrency, bool sortByScore)
			{
				this.LocaleId = localeId;
				this.SimilarityThreshold = similarityThreshold;
				this.IgnoreCase = ignoreCase;
				this.IgnoreSpace = ignoreSpace;
				this.NumberOfMatches = numberOfMatches;
				this.Concurrency = concurrency;
				this.SortByScore = sortByScore;
			}

			// Token: 0x040004BE RID: 1214
			public readonly double SimilarityThreshold = 0.8;

			// Token: 0x040004BF RID: 1215
			public readonly bool IgnoreCase = true;

			// Token: 0x040004C0 RID: 1216
			public readonly bool IgnoreSpace = true;

			// Token: 0x040004C1 RID: 1217
			public readonly int NumberOfMatches = int.MaxValue;

			// Token: 0x040004C2 RID: 1218
			public readonly int LocaleId;

			// Token: 0x040004C3 RID: 1219
			public readonly int Concurrency = 1;

			// Token: 0x040004C4 RID: 1220
			public readonly bool SortByScore;
		}

		// Token: 0x02000134 RID: 308
		internal static class Stats
		{
			// Token: 0x040004C5 RID: 1221
			internal static long memoryAfterIndexing;

			// Token: 0x040004C6 RID: 1222
			internal static long memoryDuringJoin;

			// Token: 0x040004C7 RID: 1223
			internal static long memoryBeforeDisposal;

			// Token: 0x040004C8 RID: 1224
			internal static long memoryAfterDisposal;
		}

		// Token: 0x02000135 RID: 309
		public class OutputDataReader : IDataReader, IDisposable, IDataRecord
		{
			// Token: 0x06000BFE RID: 3070 RVA: 0x00033FE8 File Offset: 0x000321E8
			public OutputDataReader(IEnumerator<object[]> rowEnumerator, DataTable schemaTable)
			{
				if (rowEnumerator == null)
				{
					throw new ArgumentNullException("rowEnumerator");
				}
				this.rowEnumerator = rowEnumerator;
				if (schemaTable == null)
				{
					throw new ArgumentNullException("schemaTable");
				}
				this.schemaTable = schemaTable;
				this.IsClosed = false;
				this.nameToOrdinal = new Dictionary<string, int>();
				for (int i = 0; i < schemaTable.Rows.Count; i++)
				{
					this.nameToOrdinal[schemaTable.Rows[i]["ColumnName"].ToString()] = i;
				}
			}

			// Token: 0x17000247 RID: 583
			public object this[int i]
			{
				get
				{
					if (this.IsClosed)
					{
						throw new InvalidOperationException("Invalid attempt to call Item when reader is closed.");
					}
					if (i >= 0 && i < this.schemaTable.Rows.Count)
					{
						return this.rowEnumerator.Current[i];
					}
					throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
				}
			}

			// Token: 0x17000248 RID: 584
			public object this[string name]
			{
				get
				{
					if (this.IsClosed)
					{
						throw new InvalidOperationException("Invalid attempt to call Item when reader is closed.");
					}
					int num = -1;
					if (!this.nameToOrdinal.TryGetValue(name, ref num))
					{
						throw new ArgumentException(string.Format("Column '{0}' does not belong to table Output Table.", name));
					}
					return this.rowEnumerator.Current[num];
				}
			}

			// Token: 0x17000249 RID: 585
			// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0003411C File Offset: 0x0003231C
			public int Depth
			{
				get
				{
					if (!this.IsClosed)
					{
						return 0;
					}
					throw new InvalidOperationException("Invalid attempt to call Depth when reader is closed.");
				}
			}

			// Token: 0x1700024A RID: 586
			// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00034132 File Offset: 0x00032332
			// (set) Token: 0x06000C03 RID: 3075 RVA: 0x0003413A File Offset: 0x0003233A
			public bool IsClosed { get; private set; }

			// Token: 0x1700024B RID: 587
			// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00034143 File Offset: 0x00032343
			public int RecordsAffected
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x1700024C RID: 588
			// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00034146 File Offset: 0x00032346
			public int FieldCount
			{
				get
				{
					if (!this.IsClosed)
					{
						return this.schemaTable.Rows.Count;
					}
					throw new InvalidOperationException("Invalid attempt to call FieldCount when reader is closed.");
				}
			}

			// Token: 0x06000C06 RID: 3078 RVA: 0x0003416B File Offset: 0x0003236B
			public void Close()
			{
				this.rowEnumerator.Dispose();
				this.IsClosed = true;
			}

			// Token: 0x06000C07 RID: 3079 RVA: 0x0003417F File Offset: 0x0003237F
			public void Dispose()
			{
				this.rowEnumerator.Dispose();
			}

			// Token: 0x06000C08 RID: 3080 RVA: 0x0003418C File Offset: 0x0003238C
			public bool GetBoolean(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetBoolean when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return (bool)this.rowEnumerator.Current[i];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C09 RID: 3081 RVA: 0x000341E8 File Offset: 0x000323E8
			public byte GetByte(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetByte when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return (byte)this.rowEnumerator.Current[i];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C0A RID: 3082 RVA: 0x00034244 File Offset: 0x00032444
			public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetBytes when reader is closed.");
				}
				if (i < 0 || i >= this.schemaTable.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
				}
				if (typeof(byte[]) == this.rowEnumerator.Current[i].GetType())
				{
					return 0L;
				}
				throw new InvalidCastException(string.Format("Unable to cast object of type '{0}' to type 'System.Byte[]'.", this.rowEnumerator.Current[i].GetType()));
			}

			// Token: 0x06000C0B RID: 3083 RVA: 0x000342D0 File Offset: 0x000324D0
			public char GetChar(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetChar when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return (char)this.rowEnumerator.Current[i];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C0C RID: 3084 RVA: 0x0003432C File Offset: 0x0003252C
			public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetChars when reader is closed.");
				}
				if (i < 0 || i >= this.schemaTable.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
				}
				throw new InvalidCastException(string.Format("Unable to cast object of type '{0}' to type 'System.Char[]'.", this.rowEnumerator.Current[i].GetType()));
			}

			// Token: 0x06000C0D RID: 3085 RVA: 0x00034394 File Offset: 0x00032594
			public IDataReader GetData(int i)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000C0E RID: 3086 RVA: 0x0003439C File Offset: 0x0003259C
			public string GetDataTypeName(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetDataTypeName when reader is closed.");
				}
				if (i < 0 || i >= this.schemaTable.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
				}
				return ((Type)this.schemaTable.Rows[i]["DataType"]).Name;
			}

			// Token: 0x06000C0F RID: 3087 RVA: 0x00034408 File Offset: 0x00032608
			public DateTime GetDateTime(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetDateTime when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return (DateTime)this.rowEnumerator.Current[i];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C10 RID: 3088 RVA: 0x00034464 File Offset: 0x00032664
			public decimal GetDecimal(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetDecimal when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return (decimal)this.rowEnumerator.Current[i];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C11 RID: 3089 RVA: 0x000344C0 File Offset: 0x000326C0
			public double GetDouble(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetDouble when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return (double)this.rowEnumerator.Current[i];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C12 RID: 3090 RVA: 0x0003451C File Offset: 0x0003271C
			public Type GetFieldType(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetFieldType when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return (Type)this.schemaTable.Rows[i]["DataType"];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C13 RID: 3091 RVA: 0x00034584 File Offset: 0x00032784
			public float GetFloat(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetFloat when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return (float)this.rowEnumerator.Current[i];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C14 RID: 3092 RVA: 0x000345E0 File Offset: 0x000327E0
			public Guid GetGuid(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetGuid when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return (Guid)this.rowEnumerator.Current[i];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C15 RID: 3093 RVA: 0x0003463C File Offset: 0x0003283C
			public short GetInt16(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetInt16 when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return (short)this.rowEnumerator.Current[i];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C16 RID: 3094 RVA: 0x00034698 File Offset: 0x00032898
			public int GetInt32(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetInt32 when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return (int)this.rowEnumerator.Current[i];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C17 RID: 3095 RVA: 0x000346F4 File Offset: 0x000328F4
			public long GetInt64(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetInt64 when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return (long)this.rowEnumerator.Current[i];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C18 RID: 3096 RVA: 0x00034750 File Offset: 0x00032950
			public string GetName(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetName when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return this.schemaTable.Rows[i]["ColumnName"].ToString();
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C19 RID: 3097 RVA: 0x000347B8 File Offset: 0x000329B8
			public int GetOrdinal(string name)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetOrdinal when reader is closed.");
				}
				int num = -1;
				if (!this.nameToOrdinal.TryGetValue(name, ref num))
				{
					throw new ArgumentException(string.Format("Column '{0}' does not belong to table Output Table.", name));
				}
				return num;
			}

			// Token: 0x06000C1A RID: 3098 RVA: 0x000347FC File Offset: 0x000329FC
			public DataTable GetSchemaTable()
			{
				if (!this.IsClosed)
				{
					return this.schemaTable;
				}
				throw new InvalidOperationException("Invalid attempt to call GetSchemaTable when reader is closed.");
			}

			// Token: 0x06000C1B RID: 3099 RVA: 0x00034818 File Offset: 0x00032A18
			public string GetString(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetString when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return (string)this.rowEnumerator.Current[i];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C1C RID: 3100 RVA: 0x00034874 File Offset: 0x00032A74
			public object GetValue(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetValue when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return this.rowEnumerator.Current[i];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C1D RID: 3101 RVA: 0x000348C8 File Offset: 0x00032AC8
			public int GetValues(object[] values)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call GetValues when reader is closed.");
				}
				if (values == null)
				{
					return 0;
				}
				object[] array = this.rowEnumerator.Current;
				int num = Math.Min(values.Length, array.Length);
				for (int i = 0; i < num; i++)
				{
					values[i] = array[i];
				}
				return num;
			}

			// Token: 0x06000C1E RID: 3102 RVA: 0x00034918 File Offset: 0x00032B18
			public bool IsDBNull(int i)
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("Invalid attempt to call IsDBNull when reader is closed.");
				}
				if (i >= 0 && i < this.schemaTable.Rows.Count)
				{
					return DBNull.Value == this.rowEnumerator.Current[i];
				}
				throw new ArgumentOutOfRangeException("ordinal", "'ordinal' argument is out of range.");
			}

			// Token: 0x06000C1F RID: 3103 RVA: 0x00034973 File Offset: 0x00032B73
			public bool NextResult()
			{
				return false;
			}

			// Token: 0x06000C20 RID: 3104 RVA: 0x00034976 File Offset: 0x00032B76
			public bool Read()
			{
				return this.rowEnumerator.MoveNext();
			}

			// Token: 0x040004C9 RID: 1225
			private IEnumerator<object[]> rowEnumerator;

			// Token: 0x040004CA RID: 1226
			private DataTable schemaTable;

			// Token: 0x040004CB RID: 1227
			private Dictionary<string, int> nameToOrdinal;
		}

		// Token: 0x02000136 RID: 310
		private class MultiThreadingTestThread
		{
			// Token: 0x06000C21 RID: 3105 RVA: 0x00034984 File Offset: 0x00032B84
			public MultiThreadingTestThread(FuzzyLookupEntry.JoinType joinType, FuzzyQuery fq, DataTable inputTable, int partitionStart, int partitionEnd)
			{
				this.joinType = joinType;
				this.m_fq = fq;
				this.m_inputTable = inputTable;
				this.partitionStart = partitionStart;
				this.partitionEnd = partitionEnd;
				this.m_outputTable = new DataTable(string.Concat(new object[] { "Output Table ", partitionStart, "--", partitionEnd }));
				this.matchedRightIdSet = new HashSet<int>();
				foreach (object obj in this.m_fq.MatchResultSchema.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					this.m_outputTable.Columns.Add(dataRow[SchemaTableColumn.ColumnName] as string, dataRow[SchemaTableColumn.DataType] as Type);
				}
				this.CompletionEvent = new EventWaitHandle(false, 1);
			}

			// Token: 0x06000C22 RID: 3106 RVA: 0x00034A90 File Offset: 0x00032C90
			public void Dispose()
			{
				this.m_fq.Dispose();
				this.m_fq = null;
				this.m_inputTable = null;
				this.Thread.Abort();
				this.Thread = null;
				this.CompletionEvent.Close();
				this.CompletionEvent = null;
				this.m_outputTable = null;
				this.matchedRightIdSet = null;
			}

			// Token: 0x06000C23 RID: 3107 RVA: 0x00034AE8 File Offset: 0x00032CE8
			public void ResetInputTable(DataTable newInputTable, int partitionStart, int partitionEnd)
			{
				this.Thread.Abort();
				this.Thread = null;
				this.m_inputTable = newInputTable;
				this.partitionStart = partitionStart;
				this.partitionEnd = partitionEnd;
				this.m_outputTable.Rows.Clear();
				this.matchedRightIdSet.Clear();
				this.CompletionEvent.Reset();
			}

			// Token: 0x06000C24 RID: 3108 RVA: 0x00034B44 File Offset: 0x00032D44
			public void PerformMatch()
			{
				this.CompletionEvent.Reset();
				bool flag = this.joinType == FuzzyLookupEntry.JoinType.LeftOuter || this.joinType == FuzzyLookupEntry.JoinType.FullOuter || this.joinType == FuzzyLookupEntry.JoinType.LeftAnti;
				bool flag2 = this.joinType == FuzzyLookupEntry.JoinType.RightOuter || this.joinType == FuzzyLookupEntry.JoinType.FullOuter || this.joinType == FuzzyLookupEntry.JoinType.RightAnti;
				int count = this.m_inputTable.Columns.Count;
				using (IDataReader dataReader = this.m_inputTable.CreateDataReader())
				{
					int num = -1;
					while (dataReader.Read())
					{
						num++;
						if (num >= this.partitionStart && num < this.partitionEnd)
						{
							bool flag3 = false;
							using (MatchResultsReader matchResultsReader = this.m_fq.Match(dataReader))
							{
								while (matchResultsReader.Read())
								{
									object[] array = new object[matchResultsReader.FieldCount];
									matchResultsReader.GetValues(array);
									array[array.Length - 1] = matchResultsReader.ComparisonResult.Similarity;
									flag3 = true;
									if (this.joinType != FuzzyLookupEntry.JoinType.LeftAnti && this.joinType != FuzzyLookupEntry.JoinType.RightAnti)
									{
										this.m_outputTable.Rows.Add(array);
									}
									if (flag2)
									{
										object obj = array[count];
										this.matchedRightIdSet.Add((int)obj);
									}
								}
								if (!flag3 && flag)
								{
									object[] array2 = new object[matchResultsReader.FieldCount];
									matchResultsReader.InputRecord.GetValues(array2);
									this.m_outputTable.Rows.Add(array2);
								}
							}
						}
					}
				}
				this.CompletionEvent.Set();
			}

			// Token: 0x040004CD RID: 1229
			private readonly FuzzyLookupEntry.JoinType joinType;

			// Token: 0x040004CE RID: 1230
			private FuzzyQuery m_fq;

			// Token: 0x040004CF RID: 1231
			private DataTable m_inputTable;

			// Token: 0x040004D0 RID: 1232
			private int partitionStart;

			// Token: 0x040004D1 RID: 1233
			private int partitionEnd;

			// Token: 0x040004D2 RID: 1234
			public Thread Thread;

			// Token: 0x040004D3 RID: 1235
			public EventWaitHandle CompletionEvent;

			// Token: 0x040004D4 RID: 1236
			public DataTable m_outputTable;

			// Token: 0x040004D5 RID: 1237
			public HashSet<int> matchedRightIdSet;
		}

		// Token: 0x02000137 RID: 311
		private class StateBatch
		{
			// Token: 0x040004D6 RID: 1238
			internal Guid guid;

			// Token: 0x040004D7 RID: 1239
			internal FuzzyLookupBuilder builder;

			// Token: 0x040004D8 RID: 1240
			internal FuzzyLookupEntry.FuzzyLookupParameters parameters;

			// Token: 0x040004D9 RID: 1241
			internal FuzzyLookupEntry.JoinType joinType;

			// Token: 0x040004DA RID: 1242
			internal FuzzyLookup fuzzyLookup;

			// Token: 0x040004DB RID: 1243
			internal int singleTableColumnCount;

			// Token: 0x040004DC RID: 1244
			internal DataTable outputTableSchema;

			// Token: 0x040004DD RID: 1245
			internal FuzzyLookupEntry.MultiThreadingTestThread[] threads;

			// Token: 0x040004DE RID: 1246
			internal WaitHandle[] waitHandles;
		}

		// Token: 0x02000138 RID: 312
		private class StateStream
		{
			// Token: 0x040004DF RID: 1247
			internal Guid guid;

			// Token: 0x040004E0 RID: 1248
			internal FuzzyLookupBuilder builder;

			// Token: 0x040004E1 RID: 1249
			internal FuzzyLookupEntry.FuzzyLookupParameters parameters;

			// Token: 0x040004E2 RID: 1250
			internal FuzzyLookupEntry.JoinType joinType;

			// Token: 0x040004E3 RID: 1251
			internal FuzzyLookup fuzzyLookup;

			// Token: 0x040004E4 RID: 1252
			internal FuzzyQuery fuzzyQuery;

			// Token: 0x040004E5 RID: 1253
			internal int singleTableColumnCount;

			// Token: 0x040004E6 RID: 1254
			internal DataTable outputSchemaTable;
		}

		// Token: 0x02000139 RID: 313
		public enum JoinType
		{
			// Token: 0x040004E8 RID: 1256
			Inner,
			// Token: 0x040004E9 RID: 1257
			LeftOuter,
			// Token: 0x040004EA RID: 1258
			RightOuter,
			// Token: 0x040004EB RID: 1259
			FullOuter,
			// Token: 0x040004EC RID: 1260
			LeftAnti,
			// Token: 0x040004ED RID: 1261
			RightAnti
		}
	}
}
