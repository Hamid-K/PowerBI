using System;

namespace AngleSharp.Html
{
	// Token: 0x020000B4 RID: 180
	internal sealed class FormControlState
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x0002060B File Offset: 0x0001E80B
		public FormControlState(string name, string type, string value)
		{
			this._name = name;
			this._type = type;
			this._value = value;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00020628 File Offset: 0x0001E828
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00020630 File Offset: 0x0001E830
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00020638 File Offset: 0x0001E838
		public string Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x040004DD RID: 1245
		private readonly string _name;

		// Token: 0x040004DE RID: 1246
		private readonly string _type;

		// Token: 0x040004DF RID: 1247
		private readonly string _value;
	}
}
