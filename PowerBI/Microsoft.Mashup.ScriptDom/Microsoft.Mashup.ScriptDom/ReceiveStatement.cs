using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000417 RID: 1047
	[Serializable]
	internal class ReceiveStatement : WaitForSupportedStatement
	{
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x0016E86B File Offset: 0x0016CA6B
		// (set) Token: 0x060030B6 RID: 12470 RVA: 0x0016E873 File Offset: 0x0016CA73
		public ScalarExpression Top
		{
			get
			{
				return this._top;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._top = value;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x0016E883 File Offset: 0x0016CA83
		public IList<SelectElement> SelectElements
		{
			get
			{
				return this._selectElements;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x0016E88B File Offset: 0x0016CA8B
		// (set) Token: 0x060030B9 RID: 12473 RVA: 0x0016E893 File Offset: 0x0016CA93
		public SchemaObjectName Queue
		{
			get
			{
				return this._queue;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._queue = value;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x0016E8A3 File Offset: 0x0016CAA3
		// (set) Token: 0x060030BB RID: 12475 RVA: 0x0016E8AB File Offset: 0x0016CAAB
		public VariableTableReference Into
		{
			get
			{
				return this._into;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._into = value;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x0016E8BB File Offset: 0x0016CABB
		// (set) Token: 0x060030BD RID: 12477 RVA: 0x0016E8C3 File Offset: 0x0016CAC3
		public ValueExpression Where
		{
			get
			{
				return this._where;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._where = value;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x0016E8D3 File Offset: 0x0016CAD3
		// (set) Token: 0x060030BF RID: 12479 RVA: 0x0016E8DB File Offset: 0x0016CADB
		public bool IsConversationGroupIdWhere
		{
			get
			{
				return this._isConversationGroupIdWhere;
			}
			set
			{
				this._isConversationGroupIdWhere = value;
			}
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x0016E8E4 File Offset: 0x0016CAE4
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x0016E8F0 File Offset: 0x0016CAF0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Top != null)
			{
				this.Top.Accept(visitor);
			}
			int i = 0;
			int count = this.SelectElements.Count;
			while (i < count)
			{
				this.SelectElements[i].Accept(visitor);
				i++;
			}
			if (this.Queue != null)
			{
				this.Queue.Accept(visitor);
			}
			if (this.Into != null)
			{
				this.Into.Accept(visitor);
			}
			if (this.Where != null)
			{
				this.Where.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E38 RID: 7736
		private ScalarExpression _top;

		// Token: 0x04001E39 RID: 7737
		private List<SelectElement> _selectElements = new List<SelectElement>();

		// Token: 0x04001E3A RID: 7738
		private SchemaObjectName _queue;

		// Token: 0x04001E3B RID: 7739
		private VariableTableReference _into;

		// Token: 0x04001E3C RID: 7740
		private ValueExpression _where;

		// Token: 0x04001E3D RID: 7741
		private bool _isConversationGroupIdWhere;
	}
}
