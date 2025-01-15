using System;

namespace Microsoft.ProgramSynthesis.Translation.PowerQuery
{
	// Token: 0x0200032F RID: 815
	public readonly struct MValueFunctionName
	{
		// Token: 0x060011FF RID: 4607 RVA: 0x0003510D File Offset: 0x0003330D
		public MValueFunctionName(string name)
		{
			this.Name = name;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x00035116 File Offset: 0x00033316
		public string Name { get; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x0003511E File Offset: 0x0003331E
		public string QualifiedName
		{
			get
			{
				return "Value." + this.Name;
			}
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00035130 File Offset: 0x00033330
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00035138 File Offset: 0x00033338
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00035148 File Offset: 0x00033348
		public override bool Equals(object obj)
		{
			return obj is MValueFunctionName && ((MValueFunctionName)obj).Name == this.Name;
		}

		// Token: 0x040008E7 RID: 2279
		public static readonly MValueFunctionName Is = new MValueFunctionName("Is");
	}
}
