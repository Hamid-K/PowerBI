using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000478 RID: 1144
	[Serializable]
	internal class TargetDeclaration : TSqlFragment
	{
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x001709B0 File Offset: 0x0016EBB0
		// (set) Token: 0x060032DB RID: 13019 RVA: 0x001709B8 File Offset: 0x0016EBB8
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

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060032DC RID: 13020 RVA: 0x001709C8 File Offset: 0x0016EBC8
		public IList<EventDeclarationSetParameter> TargetDeclarationParameters
		{
			get
			{
				return this._targetDeclarationParameters;
			}
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x001709D0 File Offset: 0x0016EBD0
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x001709DC File Offset: 0x0016EBDC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ObjectName != null)
			{
				this.ObjectName.Accept(visitor);
			}
			int i = 0;
			int count = this.TargetDeclarationParameters.Count;
			while (i < count)
			{
				this.TargetDeclarationParameters[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001ECA RID: 7882
		private EventSessionObjectName _objectName;

		// Token: 0x04001ECB RID: 7883
		private List<EventDeclarationSetParameter> _targetDeclarationParameters = new List<EventDeclarationSetParameter>();
	}
}
