using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002F0 RID: 752
	public class SSAStep
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0002EC90 File Offset: 0x0002CE90
		// (set) Token: 0x06001045 RID: 4165 RVA: 0x0002EC98 File Offset: 0x0002CE98
		public SSARegister LValue { get; set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0002ECA1 File Offset: 0x0002CEA1
		// (set) Token: 0x06001047 RID: 4167 RVA: 0x0002ECA9 File Offset: 0x0002CEA9
		public SSARValue RValue { get; set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0002ECB2 File Offset: 0x0002CEB2
		// (set) Token: 0x06001049 RID: 4169 RVA: 0x0002ECBA File Offset: 0x0002CEBA
		public string Comment { get; set; }

		// Token: 0x0600104A RID: 4170 RVA: 0x0002ECC4 File Offset: 0x0002CEC4
		public SSAStep(SSARegister lValue, SSARValue rValue, string comment = "")
		{
			this.LValue = lValue;
			this.RValue = rValue;
			this.Comment = comment;
			this.LValue.StepWhereDefined = this;
			this.LValue.ImmediateUpLinks.Add(rValue);
			rValue.ImmediateDownLinks.Add(lValue);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0002ED18 File Offset: 0x0002CF18
		public void ReplaceRValueInPlace(SSARValue rValueOld, SSARValue rValueNew)
		{
			this.RValue = rValueNew;
			this.LValue.ImmediateUpLinks.Add(rValueNew);
			this.LValue.ImmediateUpLinks.Remove(rValueOld);
			rValueNew.ImmediateDownLinks.Add(this.LValue);
			rValueOld.ImmediateDownLinks.Remove(this.LValue);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0002ED74 File Offset: 0x0002CF74
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0} <- {1}", new object[]
			{
				this.LValue.Name,
				this.RValue.ToString()
			}));
		}
	}
}
