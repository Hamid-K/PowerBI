using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001C6 RID: 454
	[Serializable]
	internal abstract class FunctionStatementBody : ProcedureStatementBodyBase
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x0015FB94 File Offset: 0x0015DD94
		// (set) Token: 0x060022B1 RID: 8881 RVA: 0x0015FB9C File Offset: 0x0015DD9C
		public SchemaObjectName Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x0015FBAC File Offset: 0x0015DDAC
		// (set) Token: 0x060022B3 RID: 8883 RVA: 0x0015FBB4 File Offset: 0x0015DDB4
		public FunctionReturnType ReturnType
		{
			get
			{
				return this._returnType;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._returnType = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x0015FBC4 File Offset: 0x0015DDC4
		public IList<FunctionOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x0015FBCC File Offset: 0x0015DDCC
		// (set) Token: 0x060022B6 RID: 8886 RVA: 0x0015FBD4 File Offset: 0x0015DDD4
		public OrderBulkInsertOption OrderHint
		{
			get
			{
				return this._orderHint;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._orderHint = value;
			}
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0015FBE4 File Offset: 0x0015DDE4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.ReturnType != null)
			{
				this.ReturnType.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			if (this.OrderHint != null)
			{
				this.OrderHint.Accept(visitor);
			}
		}

		// Token: 0x04001A3B RID: 6715
		private SchemaObjectName _name;

		// Token: 0x04001A3C RID: 6716
		private FunctionReturnType _returnType;

		// Token: 0x04001A3D RID: 6717
		private List<FunctionOption> _options = new List<FunctionOption>();

		// Token: 0x04001A3E RID: 6718
		private OrderBulkInsertOption _orderHint;
	}
}
