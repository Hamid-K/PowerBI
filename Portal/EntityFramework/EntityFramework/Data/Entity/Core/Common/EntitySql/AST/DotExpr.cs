using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200067E RID: 1662
	internal sealed class DotExpr : Node
	{
		// Token: 0x06004F16 RID: 20246 RVA: 0x0011F81C File Offset: 0x0011DA1C
		internal DotExpr(Node leftExpr, Identifier id)
		{
			this._leftExpr = leftExpr;
			this._identifier = id;
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06004F17 RID: 20247 RVA: 0x0011F832 File Offset: 0x0011DA32
		internal Node Left
		{
			get
			{
				return this._leftExpr;
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06004F18 RID: 20248 RVA: 0x0011F83A File Offset: 0x0011DA3A
		internal Identifier Identifier
		{
			get
			{
				return this._identifier;
			}
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x0011F844 File Offset: 0x0011DA44
		internal bool IsMultipartIdentifier(out string[] names)
		{
			if (this._isMultipartIdentifierComputed != null)
			{
				names = this._names;
				return this._isMultipartIdentifierComputed.Value;
			}
			this._names = null;
			Identifier identifier = this._leftExpr as Identifier;
			if (identifier != null)
			{
				this._names = new string[]
				{
					identifier.Name,
					this._identifier.Name
				};
			}
			DotExpr dotExpr = this._leftExpr as DotExpr;
			string[] array;
			if (dotExpr != null && dotExpr.IsMultipartIdentifier(out array))
			{
				this._names = new string[array.Length + 1];
				array.CopyTo(this._names, 0);
				this._names[this._names.Length - 1] = this._identifier.Name;
			}
			this._isMultipartIdentifierComputed = new bool?(this._names != null);
			names = this._names;
			return this._isMultipartIdentifierComputed.Value;
		}

		// Token: 0x04001CCB RID: 7371
		private readonly Node _leftExpr;

		// Token: 0x04001CCC RID: 7372
		private readonly Identifier _identifier;

		// Token: 0x04001CCD RID: 7373
		private bool? _isMultipartIdentifierComputed;

		// Token: 0x04001CCE RID: 7374
		private string[] _names;
	}
}
