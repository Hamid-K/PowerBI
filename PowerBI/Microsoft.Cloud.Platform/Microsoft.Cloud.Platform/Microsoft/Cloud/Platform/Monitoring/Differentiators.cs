using System;
using System.Globalization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200009A RID: 154
	internal class Differentiators
	{
		// Token: 0x170000BC RID: 188
		internal string this[int index]
		{
			get
			{
				return this.Values[index];
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x000104FA File Offset: 0x0000E6FA
		internal int NumOfDifferentiators
		{
			get
			{
				return this.Values.Length;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00010504 File Offset: 0x0000E704
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0001050C File Offset: 0x0000E70C
		internal string[] Values { get; private set; }

		// Token: 0x06000455 RID: 1109 RVA: 0x00010515 File Offset: 0x0000E715
		internal Differentiators(string[] values)
		{
			this.Values = new string[values.Length];
			values.CopyTo(this.Values, 0);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00010538 File Offset: 0x0000E738
		public override string ToString()
		{
			if (this.NumOfDifferentiators == 0)
			{
				return "Differentiators are empty";
			}
			int i = 0;
			return this.Values.StringJoin(",", delegate(string value)
			{
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				string text = "Diff{0}={1}";
				object[] array = new object[2];
				int num = 0;
				int j = i;
				i = j + 1;
				array[num] = j;
				array[1] = value ?? "Empty";
				return string.Format(invariantCulture, text, array);
			});
		}
	}
}
