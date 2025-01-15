using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000682 RID: 1666
	internal sealed class FunctionDefinition : Node
	{
		// Token: 0x06004F21 RID: 20257 RVA: 0x0011F98D File Offset: 0x0011DB8D
		internal FunctionDefinition(Identifier name, NodeList<PropDefinition> argDefList, Node body, int startPosition, int endPosition)
		{
			this._name = name;
			this._paramDefList = argDefList;
			this._body = body;
			this._startPosition = startPosition;
			this._endPosition = endPosition;
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06004F22 RID: 20258 RVA: 0x0011F9BA File Offset: 0x0011DBBA
		internal string Name
		{
			get
			{
				return this._name.Name;
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06004F23 RID: 20259 RVA: 0x0011F9C7 File Offset: 0x0011DBC7
		internal NodeList<PropDefinition> Parameters
		{
			get
			{
				return this._paramDefList;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06004F24 RID: 20260 RVA: 0x0011F9CF File Offset: 0x0011DBCF
		internal Node Body
		{
			get
			{
				return this._body;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06004F25 RID: 20261 RVA: 0x0011F9D7 File Offset: 0x0011DBD7
		internal int StartPosition
		{
			get
			{
				return this._startPosition;
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06004F26 RID: 20262 RVA: 0x0011F9DF File Offset: 0x0011DBDF
		internal int EndPosition
		{
			get
			{
				return this._endPosition;
			}
		}

		// Token: 0x04001CD6 RID: 7382
		private readonly Identifier _name;

		// Token: 0x04001CD7 RID: 7383
		private readonly NodeList<PropDefinition> _paramDefList;

		// Token: 0x04001CD8 RID: 7384
		private readonly Node _body;

		// Token: 0x04001CD9 RID: 7385
		private readonly int _startPosition;

		// Token: 0x04001CDA RID: 7386
		private readonly int _endPosition;
	}
}
