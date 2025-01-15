using System;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x02000218 RID: 536
	public abstract class ValueBasedEntityToken : EntityToken
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x00023016 File Offset: 0x00021216
		protected ValueBasedEntityToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00023021 File Offset: 0x00021221
		public override bool Equals(EntityToken other)
		{
			return this.ValueBasedEquality(other);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002302A File Offset: 0x0002122A
		public override int GetHashCode()
		{
			return this.ValueBasedHashCode();
		}
	}
}
