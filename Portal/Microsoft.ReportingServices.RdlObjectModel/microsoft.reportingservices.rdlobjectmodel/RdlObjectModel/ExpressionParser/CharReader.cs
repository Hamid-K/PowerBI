using System;
using System.IO;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000218 RID: 536
	internal sealed class CharReader
	{
		// Token: 0x06001220 RID: 4640 RVA: 0x00028CB8 File Offset: 0x00026EB8
		internal CharReader(TextReader file)
		{
			this._TextRun = file.ReadToEnd();
			file.Close();
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00028CE7 File Offset: 0x00026EE7
		internal bool FileEnd()
		{
			return this._CurrentPosition >= this._TextRun.Length;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00028D00 File Offset: 0x00026F00
		internal char Get()
		{
			if (this.FileEnd())
			{
				return '\0';
			}
			this._Column++;
			if (this._TextRun[this._CurrentPosition] == '\n')
			{
				this._Line++;
				this._Column = 1;
				this._TempColumn = this._Column;
			}
			this._CurrentPosition++;
			return this._TextRun[this._CurrentPosition - 1];
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00028D7C File Offset: 0x00026F7C
		internal void PutBack()
		{
			this._CurrentPosition--;
			if (this._CurrentPosition < 0)
			{
				throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.EndExpected", "UnGet before first character", 1, 1);
			}
			if (this._TextRun[this._CurrentPosition] == '\n')
			{
				this._Column = this._TempColumn;
				this._Line--;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00028DE1 File Offset: 0x00026FE1
		internal char Peek
		{
			get
			{
				if (this.FileEnd())
				{
					return '\0';
				}
				return this._TextRun[this._CurrentPosition];
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00028DFE File Offset: 0x00026FFE
		internal char Peek2
		{
			get
			{
				this._CurrentPosition++;
				char peek = this.Peek;
				this._CurrentPosition--;
				return peek;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x00028E22 File Offset: 0x00027022
		internal int Line
		{
			get
			{
				return this._Line;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00028E2A File Offset: 0x0002702A
		internal int Column
		{
			get
			{
				return this._Column;
			}
		}

		// Token: 0x040005BF RID: 1471
		private readonly string _TextRun;

		// Token: 0x040005C0 RID: 1472
		private int _Column = 1;

		// Token: 0x040005C1 RID: 1473
		private int _CurrentPosition;

		// Token: 0x040005C2 RID: 1474
		private int _Line = 1;

		// Token: 0x040005C3 RID: 1475
		private int _TempColumn = 1;
	}
}
