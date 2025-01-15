using System;

namespace Microsoft.ReportingServices.Design
{
	// Token: 0x0200038A RID: 906
	public class FloatPropertyDef : PropertyDef
	{
		// Token: 0x06001E00 RID: 7680 RVA: 0x0007AD7E File Offset: 0x00078F7E
		public FloatPropertyDef(string name, float? min, float? max, float? defaultValue)
			: base(name)
		{
			this.Minimum = min;
			this.Maximum = max;
			this.Default = defaultValue;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x0007ADA0 File Offset: 0x00078FA0
		public void Constrain(ref float value)
		{
			if (this.Minimum != null && value < this.Minimum.Value)
			{
				value = this.Minimum.Value;
				return;
			}
			if (this.Maximum != null && value > this.Maximum.Value)
			{
				value = this.Maximum.Value;
			}
		}

		// Token: 0x04000CB5 RID: 3253
		public readonly float? Minimum;

		// Token: 0x04000CB6 RID: 3254
		public readonly float? Maximum;

		// Token: 0x04000CB7 RID: 3255
		public readonly float? Default;
	}
}
