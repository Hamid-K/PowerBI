using System;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000DA RID: 218
	internal class VersionedModel
	{
		// Token: 0x060010B4 RID: 4276 RVA: 0x00026308 File Offset: 0x00024508
		public VersionedModel(XDocument model, string version = null)
		{
			this._model = model;
			this._version = version;
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x0002631E File Offset: 0x0002451E
		public XDocument Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x00026326 File Offset: 0x00024526
		public string Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x040008AE RID: 2222
		private readonly XDocument _model;

		// Token: 0x040008AF RID: 2223
		private readonly string _version;
	}
}
