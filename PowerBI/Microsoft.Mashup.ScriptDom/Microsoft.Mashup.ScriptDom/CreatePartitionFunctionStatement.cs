using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000362 RID: 866
	[Serializable]
	internal class CreatePartitionFunctionStatement : TSqlStatement
	{
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06002C78 RID: 11384 RVA: 0x0016A2C8 File Offset: 0x001684C8
		// (set) Token: 0x06002C79 RID: 11385 RVA: 0x0016A2D0 File Offset: 0x001684D0
		public Identifier Name
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

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x0016A2E0 File Offset: 0x001684E0
		// (set) Token: 0x06002C7B RID: 11387 RVA: 0x0016A2E8 File Offset: 0x001684E8
		public PartitionParameterType ParameterType
		{
			get
			{
				return this._parameterType;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._parameterType = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x0016A2F8 File Offset: 0x001684F8
		// (set) Token: 0x06002C7D RID: 11389 RVA: 0x0016A300 File Offset: 0x00168500
		public PartitionFunctionRange Range
		{
			get
			{
				return this._range;
			}
			set
			{
				this._range = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x0016A309 File Offset: 0x00168509
		public IList<ScalarExpression> BoundaryValues
		{
			get
			{
				return this._boundaryValues;
			}
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x0016A311 File Offset: 0x00168511
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x0016A320 File Offset: 0x00168520
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.ParameterType != null)
			{
				this.ParameterType.Accept(visitor);
			}
			int i = 0;
			int count = this.BoundaryValues.Count;
			while (i < count)
			{
				this.BoundaryValues[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D09 RID: 7433
		private Identifier _name;

		// Token: 0x04001D0A RID: 7434
		private PartitionParameterType _parameterType;

		// Token: 0x04001D0B RID: 7435
		private PartitionFunctionRange _range;

		// Token: 0x04001D0C RID: 7436
		private List<ScalarExpression> _boundaryValues = new List<ScalarExpression>();
	}
}
