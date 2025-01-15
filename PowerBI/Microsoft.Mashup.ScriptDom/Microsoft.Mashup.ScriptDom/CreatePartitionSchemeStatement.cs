using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000364 RID: 868
	[Serializable]
	internal class CreatePartitionSchemeStatement : TSqlStatement
	{
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x0016A40E File Offset: 0x0016860E
		// (set) Token: 0x06002C8A RID: 11402 RVA: 0x0016A416 File Offset: 0x00168616
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

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x0016A426 File Offset: 0x00168626
		// (set) Token: 0x06002C8C RID: 11404 RVA: 0x0016A42E File Offset: 0x0016862E
		public Identifier PartitionFunction
		{
			get
			{
				return this._partitionFunction;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._partitionFunction = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x0016A43E File Offset: 0x0016863E
		// (set) Token: 0x06002C8E RID: 11406 RVA: 0x0016A446 File Offset: 0x00168646
		public bool IsAll
		{
			get
			{
				return this._isAll;
			}
			set
			{
				this._isAll = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06002C8F RID: 11407 RVA: 0x0016A44F File Offset: 0x0016864F
		public IList<IdentifierOrValueExpression> FileGroups
		{
			get
			{
				return this._fileGroups;
			}
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x0016A457 File Offset: 0x00168657
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x0016A464 File Offset: 0x00168664
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.PartitionFunction != null)
			{
				this.PartitionFunction.Accept(visitor);
			}
			int i = 0;
			int count = this.FileGroups.Count;
			while (i < count)
			{
				this.FileGroups[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D0F RID: 7439
		private Identifier _name;

		// Token: 0x04001D10 RID: 7440
		private Identifier _partitionFunction;

		// Token: 0x04001D11 RID: 7441
		private bool _isAll;

		// Token: 0x04001D12 RID: 7442
		private List<IdentifierOrValueExpression> _fileGroups = new List<IdentifierOrValueExpression>();
	}
}
