using System;
using System.Globalization;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x0200013F RID: 319
	public class CsdlLocation : EdmLocation
	{
		// Token: 0x060007B2 RID: 1970 RVA: 0x00014350 File Offset: 0x00012550
		internal CsdlLocation(int number, int position)
			: this(null, number, position)
		{
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001435B File Offset: 0x0001255B
		internal CsdlLocation(string source, int number, int position)
		{
			this.Source = source;
			this.LineNumber = number;
			this.LinePosition = position;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00014378 File Offset: 0x00012578
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x00014380 File Offset: 0x00012580
		public string Source { get; private set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00014389 File Offset: 0x00012589
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x00014391 File Offset: 0x00012591
		public int LineNumber { get; private set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001439A File Offset: 0x0001259A
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x000143A2 File Offset: 0x000125A2
		public int LinePosition { get; private set; }

		// Token: 0x060007BA RID: 1978 RVA: 0x000143AC File Offset: 0x000125AC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"(",
				Convert.ToString(this.LineNumber, CultureInfo.InvariantCulture),
				", ",
				Convert.ToString(this.LinePosition, CultureInfo.InvariantCulture),
				")"
			});
		}
	}
}
