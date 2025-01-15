using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001EB RID: 491
	[Serializable]
	internal class InPredicate : BooleanExpression
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x001608C4 File Offset: 0x0015EAC4
		// (set) Token: 0x06002375 RID: 9077 RVA: 0x001608CC File Offset: 0x0015EACC
		public ScalarExpression Expression
		{
			get
			{
				return this._expression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._expression = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x001608DC File Offset: 0x0015EADC
		// (set) Token: 0x06002377 RID: 9079 RVA: 0x001608E4 File Offset: 0x0015EAE4
		public ScalarSubquery Subquery
		{
			get
			{
				return this._subquery;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._subquery = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06002378 RID: 9080 RVA: 0x001608F4 File Offset: 0x0015EAF4
		// (set) Token: 0x06002379 RID: 9081 RVA: 0x001608FC File Offset: 0x0015EAFC
		public bool NotDefined
		{
			get
			{
				return this._notDefined;
			}
			set
			{
				this._notDefined = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x00160905 File Offset: 0x0015EB05
		public IList<ScalarExpression> Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x0016090D File Offset: 0x0015EB0D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x0016091C File Offset: 0x0015EB1C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Expression != null)
			{
				this.Expression.Accept(visitor);
			}
			if (this.Subquery != null)
			{
				this.Subquery.Accept(visitor);
			}
			int i = 0;
			int count = this.Values.Count;
			while (i < count)
			{
				this.Values[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A70 RID: 6768
		private ScalarExpression _expression;

		// Token: 0x04001A71 RID: 6769
		private ScalarSubquery _subquery;

		// Token: 0x04001A72 RID: 6770
		private bool _notDefined;

		// Token: 0x04001A73 RID: 6771
		private List<ScalarExpression> _values = new List<ScalarExpression>();
	}
}
