using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000328 RID: 808
	[Serializable]
	internal class ContainmentDatabaseOption : DatabaseOption
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x00168859 File Offset: 0x00166A59
		// (set) Token: 0x06002AE3 RID: 10979 RVA: 0x00168861 File Offset: 0x00166A61
		public ContainmentOptionKind Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x0016886A File Offset: 0x00166A6A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x00168876 File Offset: 0x00166A76
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C88 RID: 7304
		private ContainmentOptionKind _value;
	}
}
