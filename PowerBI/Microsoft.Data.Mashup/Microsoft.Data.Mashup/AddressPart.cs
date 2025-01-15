using System;
using System.Globalization;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000016 RID: 22
	public struct AddressPart
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00005C08 File Offset: 0x00003E08
		internal AddressPart(string name, string label, string value)
		{
			this.name = name;
			this.label = label;
			this.value = value;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005C1F File Offset: 0x00003E1F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005C27 File Offset: 0x00003E27
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005C2F File Offset: 0x00003E2F
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005C37 File Offset: 0x00003E37
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} ({1}): {2}", this.Name, this.Label, this.Value);
		}

		// Token: 0x04000072 RID: 114
		private readonly string name;

		// Token: 0x04000073 RID: 115
		private readonly string label;

		// Token: 0x04000074 RID: 116
		private readonly string value;
	}
}
