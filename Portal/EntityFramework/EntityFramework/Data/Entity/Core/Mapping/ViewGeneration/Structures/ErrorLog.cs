using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005A2 RID: 1442
	internal class ErrorLog : InternalBase
	{
		// Token: 0x060045E9 RID: 17897 RVA: 0x000F6933 File Offset: 0x000F4B33
		internal ErrorLog()
		{
			this.m_log = new List<ErrorLog.Record>();
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060045EA RID: 17898 RVA: 0x000F6946 File Offset: 0x000F4B46
		internal int Count
		{
			get
			{
				return this.m_log.Count;
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060045EB RID: 17899 RVA: 0x000F6953 File Offset: 0x000F4B53
		internal IEnumerable<EdmSchemaError> Errors
		{
			get
			{
				foreach (ErrorLog.Record record in this.m_log)
				{
					yield return record.Error;
				}
				List<ErrorLog.Record>.Enumerator enumerator = default(List<ErrorLog.Record>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x000F6963 File Offset: 0x000F4B63
		internal void AddEntry(ErrorLog.Record record)
		{
			this.m_log.Add(record);
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x000F6974 File Offset: 0x000F4B74
		internal void Merge(ErrorLog log)
		{
			foreach (ErrorLog.Record record in log.m_log)
			{
				this.m_log.Add(record);
			}
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x000F69CC File Offset: 0x000F4BCC
		internal void PrintTrace()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToCompactString(stringBuilder);
			Helpers.StringTraceLine(stringBuilder.ToString());
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x000F69F4 File Offset: 0x000F4BF4
		internal override void ToCompactString(StringBuilder builder)
		{
			foreach (ErrorLog.Record record in this.m_log)
			{
				record.ToCompactString(builder);
			}
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x000F6A48 File Offset: 0x000F4C48
		internal string ToUserString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ErrorLog.Record record in this.m_log)
			{
				string text = record.ToUserString();
				stringBuilder.AppendLine(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001901 RID: 6401
		private readonly List<ErrorLog.Record> m_log;

		// Token: 0x02000BCA RID: 3018
		internal class Record : InternalBase
		{
			// Token: 0x060067EA RID: 26602 RVA: 0x00162740 File Offset: 0x00160940
			internal Record(ViewGenErrorCode errorCode, string message, IEnumerable<LeftCellWrapper> wrappers, string debugMessage)
			{
				IEnumerable<Cell> inputCellsForWrappers = LeftCellWrapper.GetInputCellsForWrappers(wrappers);
				this.Init(errorCode, message, inputCellsForWrappers, debugMessage);
			}

			// Token: 0x060067EB RID: 26603 RVA: 0x00162765 File Offset: 0x00160965
			internal Record(ViewGenErrorCode errorCode, string message, Cell sourceCell, string debugMessage)
			{
				this.Init(errorCode, message, new Cell[] { sourceCell }, debugMessage);
			}

			// Token: 0x060067EC RID: 26604 RVA: 0x00162781 File Offset: 0x00160981
			internal Record(ViewGenErrorCode errorCode, string message, IEnumerable<Cell> sourceCells, string debugMessage)
			{
				this.Init(errorCode, message, sourceCells, debugMessage);
			}

			// Token: 0x060067ED RID: 26605 RVA: 0x00162794 File Offset: 0x00160994
			internal Record(EdmSchemaError error)
			{
				this.m_debugMessage = error.ToString();
				this.m_mappingError = error;
			}

			// Token: 0x060067EE RID: 26606 RVA: 0x001627B0 File Offset: 0x001609B0
			private void Init(ViewGenErrorCode errorCode, string message, IEnumerable<Cell> sourceCells, string debugMessage)
			{
				this.m_sourceCells = new List<Cell>(sourceCells);
				CellLabel cellLabel = this.m_sourceCells[0].CellLabel;
				string sourceLocation = cellLabel.SourceLocation;
				int startLineNumber = cellLabel.StartLineNumber;
				int startLinePosition = cellLabel.StartLinePosition;
				string text = ErrorLog.Record.InternalToString(message, debugMessage, this.m_sourceCells, errorCode, false);
				this.m_debugMessage = ErrorLog.Record.InternalToString(message, debugMessage, this.m_sourceCells, errorCode, true);
				this.m_mappingError = new EdmSchemaError(text, (int)errorCode, EdmSchemaErrorSeverity.Error, sourceLocation, startLineNumber, startLinePosition);
			}

			// Token: 0x17001121 RID: 4385
			// (get) Token: 0x060067EF RID: 26607 RVA: 0x00162826 File Offset: 0x00160A26
			internal EdmSchemaError Error
			{
				get
				{
					return this.m_mappingError;
				}
			}

			// Token: 0x060067F0 RID: 26608 RVA: 0x0016282E File Offset: 0x00160A2E
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append(this.m_debugMessage);
			}

			// Token: 0x060067F1 RID: 26609 RVA: 0x00162840 File Offset: 0x00160A40
			private static void GetUserLinesFromCells(IEnumerable<Cell> sourceCells, StringBuilder lineBuilder, bool isInvariant)
			{
				IEnumerable<Cell> enumerable = sourceCells.OrderBy((Cell cell) => cell.CellLabel.StartLineNumber, Comparer<int>.Default);
				bool flag = true;
				foreach (Cell cell2 in enumerable)
				{
					if (!flag)
					{
						lineBuilder.Append(isInvariant ? EntityRes.GetString("ViewGen_CommaBlank") : ", ");
					}
					flag = false;
					lineBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}", new object[] { cell2.CellLabel.StartLineNumber });
				}
			}

			// Token: 0x060067F2 RID: 26610 RVA: 0x001628F8 File Offset: 0x00160AF8
			private static string InternalToString(string message, string debugMessage, List<Cell> sourceCells, ViewGenErrorCode errorCode, bool isInvariant)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (isInvariant)
				{
					stringBuilder.AppendLine(debugMessage);
					stringBuilder.Append(isInvariant ? "ERROR" : Strings.ViewGen_Error);
					StringUtil.FormatStringBuilder(stringBuilder, " ({0}): ", new object[] { (int)errorCode });
				}
				StringBuilder stringBuilder2 = new StringBuilder();
				ErrorLog.Record.GetUserLinesFromCells(sourceCells, stringBuilder2, isInvariant);
				if (isInvariant)
				{
					if (sourceCells.Count > 1)
					{
						StringUtil.FormatStringBuilder(stringBuilder, "Problem in Mapping Fragments starting at lines {0}: ", new object[] { stringBuilder2.ToString() });
					}
					else
					{
						StringUtil.FormatStringBuilder(stringBuilder, "Problem in Mapping Fragment starting at line {0}: ", new object[] { stringBuilder2.ToString() });
					}
				}
				else if (sourceCells.Count > 1)
				{
					stringBuilder.Append(Strings.ViewGen_ErrorLog2(stringBuilder2.ToString()));
				}
				else
				{
					stringBuilder.Append(Strings.ViewGen_ErrorLog(stringBuilder2.ToString()));
				}
				stringBuilder.AppendLine(message);
				return stringBuilder.ToString();
			}

			// Token: 0x060067F3 RID: 26611 RVA: 0x001629DC File Offset: 0x00160BDC
			internal string ToUserString()
			{
				return this.m_mappingError.ToString();
			}

			// Token: 0x04002EB5 RID: 11957
			private EdmSchemaError m_mappingError;

			// Token: 0x04002EB6 RID: 11958
			private List<Cell> m_sourceCells;

			// Token: 0x04002EB7 RID: 11959
			private string m_debugMessage;
		}
	}
}
