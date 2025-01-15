using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000439 RID: 1081
	[Serializable]
	internal abstract class AuditSpecificationStatement : TSqlStatement
	{
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x0016F582 File Offset: 0x0016D782
		// (set) Token: 0x06003189 RID: 12681 RVA: 0x0016F58A File Offset: 0x0016D78A
		public OptionState AuditState
		{
			get
			{
				return this._auditState;
			}
			set
			{
				this._auditState = value;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600318A RID: 12682 RVA: 0x0016F593 File Offset: 0x0016D793
		public IList<AuditSpecificationPart> Parts
		{
			get
			{
				return this._parts;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600318B RID: 12683 RVA: 0x0016F59B File Offset: 0x0016D79B
		// (set) Token: 0x0600318C RID: 12684 RVA: 0x0016F5A3 File Offset: 0x0016D7A3
		public Identifier SpecificationName
		{
			get
			{
				return this._specificationName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._specificationName = value;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x0016F5B3 File Offset: 0x0016D7B3
		// (set) Token: 0x0600318E RID: 12686 RVA: 0x0016F5BB File Offset: 0x0016D7BB
		public Identifier AuditName
		{
			get
			{
				return this._auditName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._auditName = value;
			}
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x0016F5CC File Offset: 0x0016D7CC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Parts.Count;
			while (i < count)
			{
				this.Parts[i].Accept(visitor);
				i++;
			}
			if (this.SpecificationName != null)
			{
				this.SpecificationName.Accept(visitor);
			}
			if (this.AuditName != null)
			{
				this.AuditName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E74 RID: 7796
		private OptionState _auditState;

		// Token: 0x04001E75 RID: 7797
		private List<AuditSpecificationPart> _parts = new List<AuditSpecificationPart>();

		// Token: 0x04001E76 RID: 7798
		private Identifier _specificationName;

		// Token: 0x04001E77 RID: 7799
		private Identifier _auditName;
	}
}
