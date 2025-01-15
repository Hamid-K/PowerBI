using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000474 RID: 1140
	[Serializable]
	internal class EventDeclaration : TSqlFragment
	{
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060032BC RID: 12988 RVA: 0x0017074B File Offset: 0x0016E94B
		// (set) Token: 0x060032BD RID: 12989 RVA: 0x00170753 File Offset: 0x0016E953
		public EventSessionObjectName ObjectName
		{
			get
			{
				return this._objectName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._objectName = value;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060032BE RID: 12990 RVA: 0x00170763 File Offset: 0x0016E963
		public IList<EventDeclarationSetParameter> EventDeclarationSetParameters
		{
			get
			{
				return this._eventDeclarationSetParameters;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060032BF RID: 12991 RVA: 0x0017076B File Offset: 0x0016E96B
		public IList<EventSessionObjectName> EventDeclarationActionParameters
		{
			get
			{
				return this._eventDeclarationActionParameters;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x00170773 File Offset: 0x0016E973
		// (set) Token: 0x060032C1 RID: 12993 RVA: 0x0017077B File Offset: 0x0016E97B
		public BooleanExpression EventDeclarationPredicateParameter
		{
			get
			{
				return this._eventDeclarationPredicateParameter;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._eventDeclarationPredicateParameter = value;
			}
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x0017078B File Offset: 0x0016E98B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x00170798 File Offset: 0x0016E998
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ObjectName != null)
			{
				this.ObjectName.Accept(visitor);
			}
			int i = 0;
			int count = this.EventDeclarationSetParameters.Count;
			while (i < count)
			{
				this.EventDeclarationSetParameters[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.EventDeclarationActionParameters.Count;
			while (j < count2)
			{
				this.EventDeclarationActionParameters[j].Accept(visitor);
				j++;
			}
			if (this.EventDeclarationPredicateParameter != null)
			{
				this.EventDeclarationPredicateParameter.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EC0 RID: 7872
		private EventSessionObjectName _objectName;

		// Token: 0x04001EC1 RID: 7873
		private List<EventDeclarationSetParameter> _eventDeclarationSetParameters = new List<EventDeclarationSetParameter>();

		// Token: 0x04001EC2 RID: 7874
		private List<EventSessionObjectName> _eventDeclarationActionParameters = new List<EventSessionObjectName>();

		// Token: 0x04001EC3 RID: 7875
		private BooleanExpression _eventDeclarationPredicateParameter;
	}
}
