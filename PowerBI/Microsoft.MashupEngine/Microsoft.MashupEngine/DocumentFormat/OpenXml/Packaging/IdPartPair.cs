using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002147 RID: 8519
	internal class IdPartPair
	{
		// Token: 0x1700331E RID: 13086
		// (get) Token: 0x0600D3C9 RID: 54217 RVA: 0x002A00EF File Offset: 0x0029E2EF
		// (set) Token: 0x0600D3CA RID: 54218 RVA: 0x002A00F7 File Offset: 0x0029E2F7
		public string RelationshipId
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x1700331F RID: 13087
		// (get) Token: 0x0600D3CB RID: 54219 RVA: 0x002A0100 File Offset: 0x0029E300
		// (set) Token: 0x0600D3CC RID: 54220 RVA: 0x002A0108 File Offset: 0x0029E308
		public OpenXmlPart OpenXmlPart
		{
			get
			{
				return this._part;
			}
			set
			{
				this._part = value;
			}
		}

		// Token: 0x0600D3CD RID: 54221 RVA: 0x002A0111 File Offset: 0x0029E311
		public IdPartPair(string id, OpenXmlPart part)
		{
			this.RelationshipId = id;
			this.OpenXmlPart = part;
		}

		// Token: 0x0600D3CE RID: 54222 RVA: 0x002A0127 File Offset: 0x0029E327
		public bool Equals(IdPartPair value)
		{
			return value != null && this._id.Equals(value._id) && this._part == value._part;
		}

		// Token: 0x040069A8 RID: 27048
		private string _id;

		// Token: 0x040069A9 RID: 27049
		private OpenXmlPart _part;
	}
}
