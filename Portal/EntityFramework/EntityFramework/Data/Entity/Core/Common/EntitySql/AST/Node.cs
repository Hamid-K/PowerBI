using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000674 RID: 1652
	internal abstract class Node
	{
		// Token: 0x06004EF6 RID: 20214 RVA: 0x0011F5DE File Offset: 0x0011D7DE
		internal Node()
		{
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x0011F5F1 File Offset: 0x0011D7F1
		internal Node(string commandText, int inputPosition)
		{
			this._errCtx.CommandText = commandText;
			this._errCtx.InputPosition = inputPosition;
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x0011F61C File Offset: 0x0011D81C
		// (set) Token: 0x06004EF9 RID: 20217 RVA: 0x0011F624 File Offset: 0x0011D824
		internal ErrorContext ErrCtx
		{
			get
			{
				return this._errCtx;
			}
			set
			{
				this._errCtx = value;
			}
		}

		// Token: 0x04001C8F RID: 7311
		private ErrorContext _errCtx = new ErrorContext();
	}
}
