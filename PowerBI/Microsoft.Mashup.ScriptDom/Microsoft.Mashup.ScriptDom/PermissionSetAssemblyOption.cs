using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000278 RID: 632
	[Serializable]
	internal class PermissionSetAssemblyOption : AssemblyOption
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x001647D6 File Offset: 0x001629D6
		// (set) Token: 0x060026DF RID: 9951 RVA: 0x001647DE File Offset: 0x001629DE
		public PermissionSetOption PermissionSetOption
		{
			get
			{
				return this._permissionSetOption;
			}
			set
			{
				this._permissionSetOption = value;
			}
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x001647E7 File Offset: 0x001629E7
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x001647F3 File Offset: 0x001629F3
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B70 RID: 7024
		private PermissionSetOption _permissionSetOption;
	}
}
