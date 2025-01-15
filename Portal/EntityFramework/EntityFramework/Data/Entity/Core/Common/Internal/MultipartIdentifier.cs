using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.Core.Common.Internal
{
	// Token: 0x02000631 RID: 1585
	internal static class MultipartIdentifier
	{
		// Token: 0x06004C45 RID: 19525 RVA: 0x0010CC12 File Offset: 0x0010AE12
		private static void IncrementStringCount(List<string> ary, ref int position)
		{
			position++;
			ary.Add(string.Empty);
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x0010CC25 File Offset: 0x0010AE25
		private static bool IsWhitespace(char ch)
		{
			return char.IsWhiteSpace(ch);
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x0010CC30 File Offset: 0x0010AE30
		internal static List<string> ParseMultipartIdentifier(string name, string leftQuote, string rightQuote, char separator)
		{
			List<string> list = new List<string>();
			list.Add(null);
			int num = 0;
			MultipartIdentifier.MPIState mpistate = MultipartIdentifier.MPIState.MPI_Value;
			StringBuilder stringBuilder = new StringBuilder(name.Length);
			StringBuilder stringBuilder2 = null;
			char c = ' ';
			foreach (char c2 in name)
			{
				switch (mpistate)
				{
				case MultipartIdentifier.MPIState.MPI_Value:
					if (!MultipartIdentifier.IsWhitespace(c2))
					{
						int num2;
						if (c2 == separator)
						{
							list[num] = string.Empty;
							MultipartIdentifier.IncrementStringCount(list, ref num);
						}
						else if (-1 != (num2 = leftQuote.IndexOf(c2)))
						{
							c = rightQuote[num2];
							stringBuilder.Length = 0;
							mpistate = MultipartIdentifier.MPIState.MPI_ParseQuote;
						}
						else
						{
							if (-1 != rightQuote.IndexOf(c2))
							{
								throw new ArgumentException(Strings.ADP_InvalidMultipartNameDelimiterUsage, "path");
							}
							stringBuilder.Length = 0;
							stringBuilder.Append(c2);
							mpistate = MultipartIdentifier.MPIState.MPI_ParseNonQuote;
						}
					}
					break;
				case MultipartIdentifier.MPIState.MPI_ParseNonQuote:
					if (c2 == separator)
					{
						list[num] = stringBuilder.ToString();
						MultipartIdentifier.IncrementStringCount(list, ref num);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					else
					{
						if (-1 != rightQuote.IndexOf(c2))
						{
							throw new ArgumentException(Strings.ADP_InvalidMultipartNameDelimiterUsage, "path");
						}
						if (-1 != leftQuote.IndexOf(c2))
						{
							throw new ArgumentException(Strings.ADP_InvalidMultipartNameDelimiterUsage, "path");
						}
						if (MultipartIdentifier.IsWhitespace(c2))
						{
							list[num] = stringBuilder.ToString();
							if (stringBuilder2 == null)
							{
								stringBuilder2 = new StringBuilder();
							}
							stringBuilder2.Length = 0;
							stringBuilder2.Append(c2);
							mpistate = MultipartIdentifier.MPIState.MPI_LookForNextCharOrSeparator;
						}
						else
						{
							stringBuilder.Append(c2);
						}
					}
					break;
				case MultipartIdentifier.MPIState.MPI_LookForSeparator:
					if (!MultipartIdentifier.IsWhitespace(c2))
					{
						if (c2 != separator)
						{
							throw new ArgumentException(Strings.ADP_InvalidMultipartNameDelimiterUsage, "path");
						}
						MultipartIdentifier.IncrementStringCount(list, ref num);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					break;
				case MultipartIdentifier.MPIState.MPI_LookForNextCharOrSeparator:
					if (!MultipartIdentifier.IsWhitespace(c2))
					{
						if (c2 == separator)
						{
							MultipartIdentifier.IncrementStringCount(list, ref num);
							mpistate = MultipartIdentifier.MPIState.MPI_Value;
						}
						else
						{
							stringBuilder.Append(stringBuilder2);
							stringBuilder.Append(c2);
							list[num] = stringBuilder.ToString();
							mpistate = MultipartIdentifier.MPIState.MPI_ParseNonQuote;
						}
					}
					else
					{
						stringBuilder2.Append(c2);
					}
					break;
				case MultipartIdentifier.MPIState.MPI_ParseQuote:
					if (c2 == c)
					{
						mpistate = MultipartIdentifier.MPIState.MPI_RightQuote;
					}
					else
					{
						stringBuilder.Append(c2);
					}
					break;
				case MultipartIdentifier.MPIState.MPI_RightQuote:
					if (c2 == c)
					{
						stringBuilder.Append(c2);
						mpistate = MultipartIdentifier.MPIState.MPI_ParseQuote;
					}
					else if (c2 == separator)
					{
						list[num] = stringBuilder.ToString();
						MultipartIdentifier.IncrementStringCount(list, ref num);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					else
					{
						if (!MultipartIdentifier.IsWhitespace(c2))
						{
							throw new ArgumentException(Strings.ADP_InvalidMultipartNameDelimiterUsage, "path");
						}
						list[num] = stringBuilder.ToString();
						mpistate = MultipartIdentifier.MPIState.MPI_LookForSeparator;
					}
					break;
				}
			}
			switch (mpistate)
			{
			case MultipartIdentifier.MPIState.MPI_Value:
			case MultipartIdentifier.MPIState.MPI_LookForSeparator:
			case MultipartIdentifier.MPIState.MPI_LookForNextCharOrSeparator:
				return list;
			case MultipartIdentifier.MPIState.MPI_ParseNonQuote:
			case MultipartIdentifier.MPIState.MPI_RightQuote:
				list[num] = stringBuilder.ToString();
				return list;
			}
			throw new ArgumentException(Strings.ADP_InvalidMultipartNameDelimiterUsage, "path");
		}

		// Token: 0x04001ACB RID: 6859
		private const int MaxParts = 4;

		// Token: 0x04001ACC RID: 6860
		internal const int ServerIndex = 0;

		// Token: 0x04001ACD RID: 6861
		internal const int CatalogIndex = 1;

		// Token: 0x04001ACE RID: 6862
		internal const int SchemaIndex = 2;

		// Token: 0x04001ACF RID: 6863
		internal const int TableIndex = 3;

		// Token: 0x02000C5A RID: 3162
		private enum MPIState
		{
			// Token: 0x040030DE RID: 12510
			MPI_Value,
			// Token: 0x040030DF RID: 12511
			MPI_ParseNonQuote,
			// Token: 0x040030E0 RID: 12512
			MPI_LookForSeparator,
			// Token: 0x040030E1 RID: 12513
			MPI_LookForNextCharOrSeparator,
			// Token: 0x040030E2 RID: 12514
			MPI_ParseQuote,
			// Token: 0x040030E3 RID: 12515
			MPI_RightQuote
		}
	}
}
