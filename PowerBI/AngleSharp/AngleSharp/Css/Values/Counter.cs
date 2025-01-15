using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000114 RID: 276
	public sealed class Counter
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x0003DCAE File Offset: 0x0003BEAE
		public Counter(string identifier, string listStyle, string separator)
		{
			this._identifier = identifier;
			this._listStyle = listStyle;
			this._separator = separator;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0003DCCB File Offset: 0x0003BECB
		public string CounterIdentifier
		{
			get
			{
				return this._identifier;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0003DCD3 File Offset: 0x0003BED3
		public string ListStyle
		{
			get
			{
				return this._listStyle;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0003DCDB File Offset: 0x0003BEDB
		public string DefinedSeparator
		{
			get
			{
				return this._separator;
			}
		}

		// Token: 0x04000898 RID: 2200
		private readonly string _identifier;

		// Token: 0x04000899 RID: 2201
		private readonly string _listStyle;

		// Token: 0x0400089A RID: 2202
		private readonly string _separator;
	}
}
