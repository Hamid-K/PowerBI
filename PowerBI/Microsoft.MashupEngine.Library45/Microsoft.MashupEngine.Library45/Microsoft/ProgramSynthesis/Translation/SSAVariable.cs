using System;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002F3 RID: 755
	public class SSAVariable : SSARValue
	{
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x0002E4D5 File Offset: 0x0002C6D5
		public string VariableName
		{
			get
			{
				return base.LanguageSpecificString;
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0002E91C File Offset: 0x0002CB1C
		public SSAVariable(Type variableType, string variableName)
			: base(variableType, variableName)
		{
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0002EECE File Offset: 0x0002D0CE
		public bool Equals(SSAVariable other)
		{
			return other == this || (other != null && this.VariableName == other.VariableName);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0002EEEC File Offset: 0x0002D0EC
		public override bool Equals(SSAValue other)
		{
			return this == other || (other != null && this.Equals(other as SSAVariable));
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0002EF05 File Offset: 0x0002D105
		public override int GetHashCode()
		{
			return this.VariableName.GetHashCode() * 21713;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0002EF18 File Offset: 0x0002D118
		public override string ToString()
		{
			return this.VariableName;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00004FAE File Offset: 0x000031AE
		public override SSARValue Substitute(SSARegister register, SSAValue newValue)
		{
			return this;
		}
	}
}
