using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000293 RID: 659
	[Serializable]
	internal class EnableDisableTriggerStatement : TSqlStatement
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x001651C2 File Offset: 0x001633C2
		// (set) Token: 0x0600277D RID: 10109 RVA: 0x001651CA File Offset: 0x001633CA
		public TriggerEnforcement TriggerEnforcement
		{
			get
			{
				return this._triggerEnforcement;
			}
			set
			{
				this._triggerEnforcement = value;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600277E RID: 10110 RVA: 0x001651D3 File Offset: 0x001633D3
		// (set) Token: 0x0600277F RID: 10111 RVA: 0x001651DB File Offset: 0x001633DB
		public bool All
		{
			get
			{
				return this._all;
			}
			set
			{
				this._all = value;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x001651E4 File Offset: 0x001633E4
		public IList<SchemaObjectName> TriggerNames
		{
			get
			{
				return this._triggerNames;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x001651EC File Offset: 0x001633EC
		// (set) Token: 0x06002782 RID: 10114 RVA: 0x001651F4 File Offset: 0x001633F4
		public TriggerObject TriggerObject
		{
			get
			{
				return this._triggerObject;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._triggerObject = value;
			}
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x00165204 File Offset: 0x00163404
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x00165210 File Offset: 0x00163410
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.TriggerNames.Count;
			while (i < count)
			{
				this.TriggerNames[i].Accept(visitor);
				i++;
			}
			if (this.TriggerObject != null)
			{
				this.TriggerObject.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B9B RID: 7067
		private TriggerEnforcement _triggerEnforcement;

		// Token: 0x04001B9C RID: 7068
		private bool _all;

		// Token: 0x04001B9D RID: 7069
		private List<SchemaObjectName> _triggerNames = new List<SchemaObjectName>();

		// Token: 0x04001B9E RID: 7070
		private TriggerObject _triggerObject;
	}
}
