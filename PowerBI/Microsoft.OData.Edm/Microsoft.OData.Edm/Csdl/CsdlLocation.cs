using System;
using System.Globalization;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x0200014C RID: 332
	public class CsdlLocation : EdmLocation
	{
		// Token: 0x06000852 RID: 2130 RVA: 0x0001610C File Offset: 0x0001430C
		internal CsdlLocation(int number, int position)
			: this(null, number, position)
		{
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00016117 File Offset: 0x00014317
		internal CsdlLocation(string source, int number, int position)
		{
			this.Source = source;
			this.LineNumber = number;
			this.LinePosition = position;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00016134 File Offset: 0x00014334
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0001613C File Offset: 0x0001433C
		public string Source { get; private set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00016145 File Offset: 0x00014345
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0001614D File Offset: 0x0001434D
		public int LineNumber { get; private set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00016156 File Offset: 0x00014356
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0001615E File Offset: 0x0001435E
		public int LinePosition { get; private set; }

		// Token: 0x0600085A RID: 2138 RVA: 0x00016168 File Offset: 0x00014368
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
