using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200032A RID: 810
	[Serializable]
	internal class HadrAvailabilityGroupDatabaseOption : HadrDatabaseOption
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x001688B5 File Offset: 0x00166AB5
		// (set) Token: 0x06002AED RID: 10989 RVA: 0x001688BD File Offset: 0x00166ABD
		public Identifier GroupName
		{
			get
			{
				return this._groupName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._groupName = value;
			}
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x001688CD File Offset: 0x00166ACD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x001688D9 File Offset: 0x00166AD9
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.GroupName != null)
			{
				this.GroupName.Accept(visitor);
			}
		}

		// Token: 0x04001C8A RID: 7306
		private Identifier _groupName;
	}
}
