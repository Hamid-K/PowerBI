using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000239 RID: 569
	internal sealed class ExpressionToken
	{
		// Token: 0x0600130F RID: 4879 RVA: 0x0002C497 File Offset: 0x0002A697
		internal ExpressionToken(TokenTypes tokenType, string value, int line, int startCol, int endCol)
		{
			this._Value = value;
			this._Line = line;
			this._StartColumn = startCol;
			this._EndColumn = endCol;
			this._TokenType = tokenType;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0002C4C4 File Offset: 0x0002A6C4
		internal ExpressionToken(TokenTypes type, string value)
			: this(type, value, 0, 0, 0)
		{
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0002C4D1 File Offset: 0x0002A6D1
		internal ExpressionToken(TokenTypes type)
			: this(type, null, 0, 0, 0)
		{
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0002C4DE File Offset: 0x0002A6DE
		public override string ToString()
		{
			return "<" + this._TokenType.ToString() + "> " + this._Value;
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x0002C506 File Offset: 0x0002A706
		public int StartColumn
		{
			get
			{
				return this._StartColumn - 2;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0002C510 File Offset: 0x0002A710
		public int EndColumn
		{
			get
			{
				return this._EndColumn - 2;
			}
		}

		// Token: 0x0400061B RID: 1563
		internal string _Value;

		// Token: 0x0400061C RID: 1564
		internal int _Line;

		// Token: 0x0400061D RID: 1565
		internal int _StartColumn;

		// Token: 0x0400061E RID: 1566
		internal int _EndColumn;

		// Token: 0x0400061F RID: 1567
		internal TokenTypes _TokenType;
	}
}
