using System;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatching;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B35 RID: 2869
	internal class ExternalFuzzyMatcher
	{
		// Token: 0x06004F89 RID: 20361 RVA: 0x0010A8BC File Offset: 0x00108ABC
		public static DataTable ExecuteFuzzyJoin(IEngineHost host, DataTable inputDataTable, DataTable referenceDataTable, DataTable transformationDataTable, FuzzyJoinOptions fuzzyJoinOptions, TableTypeAlgebra.JoinKind joinKind, Guid sessionId = default(Guid), bool isFirst = false)
		{
			FuzzyLookupEntry.FuzzyLookupParameters fuzzyLookupParameters = ExternalFuzzyMatcher.ToFuzzyLookupJoinParameters(fuzzyJoinOptions, false);
			FuzzyLookupEntry.JoinType joinType = ExternalFuzzyMatcher.ToFuzzyLookupJoinType(joinKind);
			if (fuzzyJoinOptions.ConcurrentRequests > 1 && inputDataTable.Rows.Count > 1)
			{
				DataTable dataTable = null;
				using (NonStreamingFuzzyJoinRequest nonStreamingFuzzyJoinRequest = NonStreamingFuzzyJoinRequest.New(host.QueryService<IThreadPoolService>(), inputDataTable, referenceDataTable, transformationDataTable, fuzzyLookupParameters, joinType))
				{
					nonStreamingFuzzyJoinRequest.Complete.WaitOne();
					dataTable = nonStreamingFuzzyJoinRequest.OutputTable;
				}
				return dataTable;
			}
			return ExternalFuzzyMatcher.ExecuteJoin(inputDataTable, referenceDataTable, transformationDataTable, fuzzyLookupParameters, joinType, sessionId, isFirst);
		}

		// Token: 0x06004F8A RID: 20362 RVA: 0x0010A944 File Offset: 0x00108B44
		public static DataTable ExecuteJoin(DataTable inputDataTable, DataTable referenceDataTable, DataTable transformationDataTable, FuzzyLookupEntry.FuzzyLookupParameters fuzzyJoinParameters, FuzzyLookupEntry.JoinType fuzzyJoinType, Guid sessionId = default(Guid), bool isFirst = false)
		{
			DataTable dataTable;
			if (sessionId != Guid.Empty && fuzzyJoinType == FuzzyLookupEntry.JoinType.LeftOuter)
			{
				if (isFirst)
				{
					dataTable = FuzzyLookupEntry.StartJoin(sessionId, inputDataTable, referenceDataTable, transformationDataTable, fuzzyJoinParameters, fuzzyJoinType);
				}
				else
				{
					dataTable = FuzzyLookupEntry.ContinueJoin(sessionId, inputDataTable);
				}
			}
			else
			{
				dataTable = FuzzyLookupEntry.Join(inputDataTable, referenceDataTable, transformationDataTable, fuzzyJoinParameters, fuzzyJoinType);
			}
			return dataTable;
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x0010A994 File Offset: 0x00108B94
		public static IDataReader ExecuteFuzzyJoinStreaming(IEngineHost host, IDataReader inputDataReader, DataTable referenceDataTable, DataTable transformationDataTable, FuzzyJoinOptions fuzzyJoinOptions, TableTypeAlgebra.JoinKind joinKind, Guid sessionId = default(Guid), bool isFirst = false)
		{
			FuzzyLookupEntry.FuzzyLookupParameters fuzzyLookupParameters = ExternalFuzzyMatcher.ToFuzzyLookupJoinParameters(fuzzyJoinOptions, true);
			FuzzyLookupEntry.JoinType joinType = ExternalFuzzyMatcher.ToFuzzyLookupJoinType(joinKind);
			return ExternalFuzzyMatcher.ExecuteJoinStreaming(inputDataReader, referenceDataTable, transformationDataTable, fuzzyLookupParameters, joinType, sessionId, isFirst);
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x0010A9C0 File Offset: 0x00108BC0
		public static IDataReader ExecuteJoinStreaming(IDataReader inputDataReader, DataTable referenceDataTable, DataTable transformationDataTable, FuzzyLookupEntry.FuzzyLookupParameters fuzzyJoinParameters, FuzzyLookupEntry.JoinType fuzzyJoinType, Guid sessionId = default(Guid), bool isFirst = false)
		{
			IDataReader dataReader;
			if (sessionId != Guid.Empty && fuzzyJoinType == FuzzyLookupEntry.JoinType.LeftOuter)
			{
				if (isFirst)
				{
					dataReader = FuzzyLookupEntry.StartJoin(sessionId, inputDataReader, referenceDataTable, transformationDataTable, fuzzyJoinParameters, fuzzyJoinType);
				}
				else
				{
					dataReader = FuzzyLookupEntry.ContinueJoin(sessionId, inputDataReader);
				}
			}
			else
			{
				dataReader = FuzzyLookupEntry.Join(inputDataReader, referenceDataTable, transformationDataTable, fuzzyJoinParameters, fuzzyJoinType);
			}
			return dataReader;
		}

		// Token: 0x06004F8D RID: 20365 RVA: 0x0010AA0E File Offset: 0x00108C0E
		public static void FinishJoin(Guid sessionGuid)
		{
			FuzzyLookupEntry.FinishJoin(sessionGuid);
		}

		// Token: 0x06004F8E RID: 20366 RVA: 0x0010AA16 File Offset: 0x00108C16
		public static void FinishJoinStreaming(Guid sessionGuid)
		{
			FuzzyLookupEntry.FinishJoinStreaming(sessionGuid);
		}

		// Token: 0x06004F8F RID: 20367 RVA: 0x0010AA20 File Offset: 0x00108C20
		private static FuzzyLookupEntry.JoinType ToFuzzyLookupJoinType(TableTypeAlgebra.JoinKind joinKind)
		{
			FuzzyLookupEntry.JoinType joinType;
			switch (joinKind)
			{
			case TableTypeAlgebra.JoinKind.Inner:
				joinType = FuzzyLookupEntry.JoinType.Inner;
				break;
			case TableTypeAlgebra.JoinKind.LeftOuter:
				joinType = FuzzyLookupEntry.JoinType.LeftOuter;
				break;
			case TableTypeAlgebra.JoinKind.FullOuter:
				joinType = FuzzyLookupEntry.JoinType.FullOuter;
				break;
			case TableTypeAlgebra.JoinKind.RightOuter:
				joinType = FuzzyLookupEntry.JoinType.RightOuter;
				break;
			case TableTypeAlgebra.JoinKind.LeftAnti:
				joinType = FuzzyLookupEntry.JoinType.LeftAnti;
				break;
			case TableTypeAlgebra.JoinKind.RightAnti:
				joinType = FuzzyLookupEntry.JoinType.RightAnti;
				break;
			case TableTypeAlgebra.JoinKind.LeftSemi:
			case TableTypeAlgebra.JoinKind.RightSemi:
				throw ValueException.NewExpressionError<Message0>(Strings.UnsupportedJoinKindForFuzzyJoins, null, null);
			default:
				throw new InvalidOperationException();
			}
			return joinType;
		}

		// Token: 0x06004F90 RID: 20368 RVA: 0x0010AA84 File Offset: 0x00108C84
		private static FuzzyLookupEntry.FuzzyLookupParameters ToFuzzyLookupJoinParameters(FuzzyJoinOptions fuzzyJoinOptions, bool isStreaming)
		{
			double threshold = fuzzyJoinOptions.Threshold;
			int concurrentRequests = fuzzyJoinOptions.ConcurrentRequests;
			string cultureKey = fuzzyJoinOptions.CultureKey;
			return new FuzzyLookupEntry.FuzzyLookupParameters(threshold, fuzzyJoinOptions.IgnoreCase, fuzzyJoinOptions.IgnoreSpace, fuzzyJoinOptions.NumberOfMatches, cultureKey, concurrentRequests, !isStreaming);
		}
	}
}
