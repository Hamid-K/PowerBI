using System;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002EF RID: 751
	public abstract class SSARValue : SSAValue
	{
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0002EB74 File Offset: 0x0002CD74
		protected string LanguageSpecificString { get; }

		// Token: 0x06001040 RID: 4160 RVA: 0x0002EB7C File Offset: 0x0002CD7C
		protected SSARValue(Type valueType, string languageSpecificString)
			: base(valueType)
		{
			this.LanguageSpecificString = languageSpecificString;
		}

		// Token: 0x06001041 RID: 4161
		public abstract SSARValue Substitute(SSARegister register, SSAValue newValue);

		// Token: 0x06001042 RID: 4162 RVA: 0x0002EB8C File Offset: 0x0002CD8C
		public virtual void SubstituteInPlace(SSARegister register, SSAValue newValue)
		{
			if (!base.ImmediateUpLinks.Contains(register))
			{
				return;
			}
			register.ImmediateDownLinks.Remove(this);
			newValue.ImmediateDownLinks.Add(this);
			base.ImmediateUpLinks.Remove(register);
			base.ImmediateUpLinks.Add(newValue);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0002EBDC File Offset: 0x0002CDDC
		public void Delete()
		{
			foreach (SSAValue ssavalue in base.ImmediateUpLinks)
			{
				ssavalue.ImmediateDownLinks.Remove(this);
			}
			foreach (SSAValue ssavalue2 in base.ImmediateDownLinks)
			{
				ssavalue2.ImmediateUpLinks.Remove(this);
			}
			base.ImmediateUpLinks.Clear();
			base.ImmediateDownLinks.Clear();
		}
	}
}
