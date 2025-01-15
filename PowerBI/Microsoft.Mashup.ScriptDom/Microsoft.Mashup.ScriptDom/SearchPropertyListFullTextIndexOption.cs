using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002B4 RID: 692
	[Serializable]
	internal class SearchPropertyListFullTextIndexOption : FullTextIndexOption
	{
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06002852 RID: 10322 RVA: 0x001660E4 File Offset: 0x001642E4
		// (set) Token: 0x06002853 RID: 10323 RVA: 0x001660EC File Offset: 0x001642EC
		public bool IsOff
		{
			get
			{
				return this._isOff;
			}
			set
			{
				this._isOff = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x001660F5 File Offset: 0x001642F5
		// (set) Token: 0x06002855 RID: 10325 RVA: 0x001660FD File Offset: 0x001642FD
		public Identifier PropertyListName
		{
			get
			{
				return this._propertyListName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._propertyListName = value;
			}
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x0016610D File Offset: 0x0016430D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x00166119 File Offset: 0x00164319
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.PropertyListName != null)
			{
				this.PropertyListName.Accept(visitor);
			}
		}

		// Token: 0x04001BDC RID: 7132
		private bool _isOff;

		// Token: 0x04001BDD RID: 7133
		private Identifier _propertyListName;
	}
}
