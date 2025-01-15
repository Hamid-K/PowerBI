﻿using System;
using System.Text;

namespace Microsoft.Data.Common
{
	// Token: 0x0200017F RID: 383
	internal class MultipartIdentifier
	{
		// Token: 0x06001CCA RID: 7370 RVA: 0x00075592 File Offset: 0x00073792
		internal static string[] ParseMultipartIdentifier(string name, string leftQuote, string rightQuote, string property, bool ThrowOnEmptyMultipartName)
		{
			return MultipartIdentifier.ParseMultipartIdentifier(name, leftQuote, rightQuote, '.', 4, true, property, ThrowOnEmptyMultipartName);
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x000755A4 File Offset: 0x000737A4
		private static void IncrementStringCount(string name, string[] ary, ref int position, string property)
		{
			position++;
			int num = ary.Length;
			if (position >= num)
			{
				throw ADP.InvalidMultipartNameToManyParts(property, name, num);
			}
			ary[position] = string.Empty;
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x000755D2 File Offset: 0x000737D2
		private static bool IsWhitespace(char ch)
		{
			return char.IsWhiteSpace(ch);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x000755DC File Offset: 0x000737DC
		internal static string[] ParseMultipartIdentifier(string name, string leftQuote, string rightQuote, char separator, int limit, bool removequotes, string property, bool ThrowOnEmptyMultipartName)
		{
			if (limit <= 0)
			{
				throw ADP.InvalidMultipartNameToManyParts(property, name, limit);
			}
			if (-1 != leftQuote.IndexOf(separator) || -1 != rightQuote.IndexOf(separator) || leftQuote.Length != rightQuote.Length)
			{
				throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
			}
			string[] array = new string[limit];
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
							array[num] = string.Empty;
							MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
						}
						else if (-1 != (num2 = leftQuote.IndexOf(c2)))
						{
							c = rightQuote[num2];
							stringBuilder.Length = 0;
							if (!removequotes)
							{
								stringBuilder.Append(c2);
							}
							mpistate = MultipartIdentifier.MPIState.MPI_ParseQuote;
						}
						else
						{
							if (-1 != rightQuote.IndexOf(c2))
							{
								throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
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
						array[num] = stringBuilder.ToString();
						MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					else
					{
						if (-1 != rightQuote.IndexOf(c2))
						{
							throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
						}
						if (-1 != leftQuote.IndexOf(c2))
						{
							throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
						}
						if (MultipartIdentifier.IsWhitespace(c2))
						{
							array[num] = stringBuilder.ToString();
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
							throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
						}
						MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					break;
				case MultipartIdentifier.MPIState.MPI_LookForNextCharOrSeparator:
					if (!MultipartIdentifier.IsWhitespace(c2))
					{
						if (c2 == separator)
						{
							MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
							mpistate = MultipartIdentifier.MPIState.MPI_Value;
						}
						else
						{
							stringBuilder.Append(stringBuilder2);
							stringBuilder.Append(c2);
							array[num] = stringBuilder.ToString();
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
						if (!removequotes)
						{
							stringBuilder.Append(c2);
						}
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
						array[num] = stringBuilder.ToString();
						MultipartIdentifier.IncrementStringCount(name, array, ref num, property);
						mpistate = MultipartIdentifier.MPIState.MPI_Value;
					}
					else
					{
						if (!MultipartIdentifier.IsWhitespace(c2))
						{
							throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
						}
						array[num] = stringBuilder.ToString();
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
				goto IL_02D4;
			case MultipartIdentifier.MPIState.MPI_ParseNonQuote:
			case MultipartIdentifier.MPIState.MPI_RightQuote:
				array[num] = stringBuilder.ToString();
				goto IL_02D4;
			}
			throw ADP.InvalidMultipartNameIncorrectUsageOfQuotes(property, name);
			IL_02D4:
			if (array[0] == null)
			{
				if (ThrowOnEmptyMultipartName)
				{
					throw ADP.InvalidMultipartName(property, name);
				}
			}
			else
			{
				int num3 = limit - num - 1;
				if (num3 > 0)
				{
					for (int j = limit - 1; j >= num3; j--)
					{
						array[j] = array[j - num3];
						array[j - num3] = null;
					}
				}
			}
			return array;
		}

		// Token: 0x04000C13 RID: 3091
		private const int MaxParts = 4;

		// Token: 0x04000C14 RID: 3092
		internal const int ServerIndex = 0;

		// Token: 0x04000C15 RID: 3093
		internal const int CatalogIndex = 1;

		// Token: 0x04000C16 RID: 3094
		internal const int SchemaIndex = 2;

		// Token: 0x04000C17 RID: 3095
		internal const int TableIndex = 3;

		// Token: 0x0200027D RID: 637
		private enum MPIState
		{
			// Token: 0x040017A6 RID: 6054
			MPI_Value,
			// Token: 0x040017A7 RID: 6055
			MPI_ParseNonQuote,
			// Token: 0x040017A8 RID: 6056
			MPI_LookForSeparator,
			// Token: 0x040017A9 RID: 6057
			MPI_LookForNextCharOrSeparator,
			// Token: 0x040017AA RID: 6058
			MPI_ParseQuote,
			// Token: 0x040017AB RID: 6059
			MPI_RightQuote
		}
	}
}
