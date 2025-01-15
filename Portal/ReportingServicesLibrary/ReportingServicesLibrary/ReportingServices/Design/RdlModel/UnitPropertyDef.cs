using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003D5 RID: 981
	public class UnitPropertyDef : PropertyDef
	{
		// Token: 0x06001F58 RID: 8024 RVA: 0x0007E43A File Offset: 0x0007C63A
		public UnitPropertyDef(string name, Unit min, Unit max, Unit defaultValue)
			: base(name)
		{
			this.Minimum = min;
			this.Maximum = max;
			this.Default = defaultValue;
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x0007E45C File Offset: 0x0007C65C
		public void Constrain(ref Unit value)
		{
			if (!this.Minimum.IsEmpty && value < this.Minimum)
			{
				value = this.Minimum;
				return;
			}
			if (!this.Maximum.IsEmpty && value > this.Maximum)
			{
				value = this.Maximum;
			}
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x0007E4C8 File Offset: 0x0007C6C8
		public ValidationResult Validate(Unit value)
		{
			return this.Validate(value, null);
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x00005C88 File Offset: 0x00003E88
		public ValidationResult Validate(Unit value, ValidationContext vc)
		{
			return null;
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x0007E4D2 File Offset: 0x0007C6D2
		public override ValidationResult Validate(object obj, ValidationContext vc)
		{
			if (obj is Unit)
			{
				return this.Validate((Unit)obj, vc);
			}
			throw new ArgumentException();
		}

		// Token: 0x04000DB6 RID: 3510
		public readonly Unit Minimum;

		// Token: 0x04000DB7 RID: 3511
		public readonly Unit Maximum;

		// Token: 0x04000DB8 RID: 3512
		public readonly Unit Default;
	}
}
