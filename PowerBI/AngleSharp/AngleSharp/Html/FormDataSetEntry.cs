using System;
using System.Text;

namespace AngleSharp.Html
{
	// Token: 0x020000B6 RID: 182
	public abstract class FormDataSetEntry
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x00020972 File Offset: 0x0001EB72
		public FormDataSetEntry(string name, string type)
		{
			this._name = name;
			this._type = type;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00020988 File Offset: 0x0001EB88
		public bool HasName
		{
			get
			{
				return this._name != null;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00020993 File Offset: 0x0001EB93
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x000209A4 File Offset: 0x0001EBA4
		public string Type
		{
			get
			{
				return this._type ?? InputTypeNames.Text;
			}
		}

		// Token: 0x0600054F RID: 1359
		public abstract void Accept(IFormDataSetVisitor visitor);

		// Token: 0x06000550 RID: 1360
		public abstract bool Contains(string boundary, Encoding encoding);

		// Token: 0x040004E2 RID: 1250
		private readonly string _name;

		// Token: 0x040004E3 RID: 1251
		private readonly string _type;
	}
}
