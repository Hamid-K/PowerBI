using System;
using System.Globalization;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000013 RID: 19
	public class CsdlLocation : EdmLocation
	{
		// Token: 0x06000057 RID: 87 RVA: 0x0000291D File Offset: 0x00000B1D
		internal CsdlLocation(int number, int position)
			: this(null, number, position)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002928 File Offset: 0x00000B28
		internal CsdlLocation(string source, int number, int position)
		{
			this.Source = source;
			this.LineNumber = number;
			this.LinePosition = position;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002945 File Offset: 0x00000B45
		// (set) Token: 0x0600005A RID: 90 RVA: 0x0000294D File Offset: 0x00000B4D
		public string Source { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002956 File Offset: 0x00000B56
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000295E File Offset: 0x00000B5E
		public int LineNumber { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002967 File Offset: 0x00000B67
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000296F File Offset: 0x00000B6F
		public int LinePosition { get; private set; }

		// Token: 0x0600005F RID: 95 RVA: 0x00002978 File Offset: 0x00000B78
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
