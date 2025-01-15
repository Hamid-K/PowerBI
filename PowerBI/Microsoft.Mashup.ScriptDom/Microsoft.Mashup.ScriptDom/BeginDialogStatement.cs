using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200041D RID: 1053
	[Serializable]
	internal class BeginDialogStatement : TSqlStatement
	{
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x0016ECBB File Offset: 0x0016CEBB
		// (set) Token: 0x060030F2 RID: 12530 RVA: 0x0016ECC3 File Offset: 0x0016CEC3
		public bool IsConversation
		{
			get
			{
				return this._isConversation;
			}
			set
			{
				this._isConversation = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060030F3 RID: 12531 RVA: 0x0016ECCC File Offset: 0x0016CECC
		// (set) Token: 0x060030F4 RID: 12532 RVA: 0x0016ECD4 File Offset: 0x0016CED4
		public VariableReference Handle
		{
			get
			{
				return this._handle;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._handle = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060030F5 RID: 12533 RVA: 0x0016ECE4 File Offset: 0x0016CEE4
		// (set) Token: 0x060030F6 RID: 12534 RVA: 0x0016ECEC File Offset: 0x0016CEEC
		public IdentifierOrValueExpression InitiatorServiceName
		{
			get
			{
				return this._initiatorServiceName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._initiatorServiceName = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060030F7 RID: 12535 RVA: 0x0016ECFC File Offset: 0x0016CEFC
		// (set) Token: 0x060030F8 RID: 12536 RVA: 0x0016ED04 File Offset: 0x0016CF04
		public ValueExpression TargetServiceName
		{
			get
			{
				return this._targetServiceName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._targetServiceName = value;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060030F9 RID: 12537 RVA: 0x0016ED14 File Offset: 0x0016CF14
		// (set) Token: 0x060030FA RID: 12538 RVA: 0x0016ED1C File Offset: 0x0016CF1C
		public ValueExpression InstanceSpec
		{
			get
			{
				return this._instanceSpec;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._instanceSpec = value;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060030FB RID: 12539 RVA: 0x0016ED2C File Offset: 0x0016CF2C
		// (set) Token: 0x060030FC RID: 12540 RVA: 0x0016ED34 File Offset: 0x0016CF34
		public IdentifierOrValueExpression ContractName
		{
			get
			{
				return this._contractName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._contractName = value;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060030FD RID: 12541 RVA: 0x0016ED44 File Offset: 0x0016CF44
		public IList<DialogOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x0016ED4C File Offset: 0x0016CF4C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x0016ED58 File Offset: 0x0016CF58
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Handle != null)
			{
				this.Handle.Accept(visitor);
			}
			if (this.InitiatorServiceName != null)
			{
				this.InitiatorServiceName.Accept(visitor);
			}
			if (this.TargetServiceName != null)
			{
				this.TargetServiceName.Accept(visitor);
			}
			if (this.InstanceSpec != null)
			{
				this.InstanceSpec.Accept(visitor);
			}
			if (this.ContractName != null)
			{
				this.ContractName.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E4E RID: 7758
		private bool _isConversation;

		// Token: 0x04001E4F RID: 7759
		private VariableReference _handle;

		// Token: 0x04001E50 RID: 7760
		private IdentifierOrValueExpression _initiatorServiceName;

		// Token: 0x04001E51 RID: 7761
		private ValueExpression _targetServiceName;

		// Token: 0x04001E52 RID: 7762
		private ValueExpression _instanceSpec;

		// Token: 0x04001E53 RID: 7763
		private IdentifierOrValueExpression _contractName;

		// Token: 0x04001E54 RID: 7764
		private List<DialogOption> _options = new List<DialogOption>();
	}
}
