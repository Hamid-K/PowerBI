using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003D3 RID: 979
	[Serializable]
	internal class BinaryQueryExpression : QueryExpression
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06002F4D RID: 12109 RVA: 0x0016D3F6 File Offset: 0x0016B5F6
		// (set) Token: 0x06002F4E RID: 12110 RVA: 0x0016D3FE File Offset: 0x0016B5FE
		public BinaryQueryExpressionType BinaryQueryExpressionType
		{
			get
			{
				return this._binaryQueryExpressionType;
			}
			set
			{
				this._binaryQueryExpressionType = value;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06002F4F RID: 12111 RVA: 0x0016D407 File Offset: 0x0016B607
		// (set) Token: 0x06002F50 RID: 12112 RVA: 0x0016D40F File Offset: 0x0016B60F
		public bool All
		{
			get
			{
				return this._all;
			}
			set
			{
				this._all = value;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06002F51 RID: 12113 RVA: 0x0016D418 File Offset: 0x0016B618
		// (set) Token: 0x06002F52 RID: 12114 RVA: 0x0016D420 File Offset: 0x0016B620
		public QueryExpression FirstQueryExpression
		{
			get
			{
				return this._firstQueryExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._firstQueryExpression = value;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x0016D430 File Offset: 0x0016B630
		// (set) Token: 0x06002F54 RID: 12116 RVA: 0x0016D438 File Offset: 0x0016B638
		public QueryExpression SecondQueryExpression
		{
			get
			{
				return this._secondQueryExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._secondQueryExpression = value;
			}
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x0016D448 File Offset: 0x0016B648
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x0016D454 File Offset: 0x0016B654
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.FirstQueryExpression != null)
			{
				this.FirstQueryExpression.Accept(visitor);
			}
			if (this.SecondQueryExpression != null)
			{
				this.SecondQueryExpression.Accept(visitor);
			}
		}

		// Token: 0x04001DE3 RID: 7651
		private BinaryQueryExpressionType _binaryQueryExpressionType;

		// Token: 0x04001DE4 RID: 7652
		private bool _all;

		// Token: 0x04001DE5 RID: 7653
		private QueryExpression _firstQueryExpression;

		// Token: 0x04001DE6 RID: 7654
		private QueryExpression _secondQueryExpression;
	}
}
