using System;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002ED RID: 749
	public class SSALiteral : SSARValue
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0002E4D5 File Offset: 0x0002C6D5
		public string LiteralString
		{
			get
			{
				return base.LanguageSpecificString;
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0002E91C File Offset: 0x0002CB1C
		public SSALiteral(Type valueType, string literalString)
			: base(valueType, literalString)
		{
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0002E926 File Offset: 0x0002CB26
		public bool Equals(SSALiteral other)
		{
			return other == this || (other != null && other.LiteralString == this.LiteralString && other.ValueType == base.ValueType);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0002E959 File Offset: 0x0002CB59
		public override bool Equals(SSAValue other)
		{
			return this == other || (other != null && this.Equals(other as SSALiteral));
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0002E972 File Offset: 0x0002CB72
		public override int GetHashCode()
		{
			return (this.LiteralString.GetHashCode() * 22679) ^ base.ValueType.GetHashCode();
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0002E4D5 File Offset: 0x0002C6D5
		public override string ToString()
		{
			return base.LanguageSpecificString;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00004FAE File Offset: 0x000031AE
		public override SSARValue Substitute(SSARegister register, SSAValue newValue)
		{
			return this;
		}
	}
}
