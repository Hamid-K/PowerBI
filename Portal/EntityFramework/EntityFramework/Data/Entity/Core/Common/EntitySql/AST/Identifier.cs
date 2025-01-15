using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000687 RID: 1671
	internal sealed class Identifier : Node
	{
		// Token: 0x06004F2F RID: 20271 RVA: 0x0011FA50 File Offset: 0x0011DC50
		internal Identifier(string name, bool isEscaped, string query, int inputPos)
			: base(query, inputPos)
		{
			if (!isEscaped)
			{
				bool flag = true;
				if (!CqlLexer.IsLetterOrDigitOrUnderscore(name, out flag))
				{
					if (flag)
					{
						ErrorContext errCtx = base.ErrCtx;
						string text = Strings.InvalidSimpleIdentifier(name);
						throw EntitySqlException.Create(errCtx, text, null);
					}
					ErrorContext errCtx2 = base.ErrCtx;
					string text2 = Strings.InvalidSimpleIdentifierNonASCII(name);
					throw EntitySqlException.Create(errCtx2, text2, null);
				}
			}
			this._name = name;
			this._isEscaped = isEscaped;
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06004F30 RID: 20272 RVA: 0x0011FAB0 File Offset: 0x0011DCB0
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06004F31 RID: 20273 RVA: 0x0011FAB8 File Offset: 0x0011DCB8
		internal bool IsEscaped
		{
			get
			{
				return this._isEscaped;
			}
		}

		// Token: 0x04001CE1 RID: 7393
		private readonly string _name;

		// Token: 0x04001CE2 RID: 7394
		private readonly bool _isEscaped;
	}
}
