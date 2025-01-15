using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003B9 RID: 953
	[DataContract(Name = "GenerationNumber", Namespace = "http://schemas.microsoft.com/2008/casdata")]
	internal class GenerationNumber : IComparable<GenerationNumber>
	{
		// Token: 0x060021A5 RID: 8613 RVA: 0x00067E96 File Offset: 0x00066096
		public GenerationNumber(long generation, string owner)
		{
			this.m_generation = generation;
			this.m_owner = owner;
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x00067EAC File Offset: 0x000660AC
		public GenerationNumber(GenerationNumber oldGeneration, string owner)
			: this(oldGeneration.m_generation + 1L, owner)
		{
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x00067EC0 File Offset: 0x000660C0
		public int CompareTo(GenerationNumber other)
		{
			int num = this.m_generation.CompareTo(other.m_generation);
			if (num == 0)
			{
				num = string.Compare(this.m_owner, other.m_owner, StringComparison.Ordinal);
			}
			return num;
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x00067EF8 File Offset: 0x000660F8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[] { this.m_generation, this.m_owner });
		}

		// Token: 0x04001570 RID: 5488
		public const string HeaderName = "GpmGenerationNumber";

		// Token: 0x04001571 RID: 5489
		[DataMember]
		private long m_generation;

		// Token: 0x04001572 RID: 5490
		[DataMember]
		private string m_owner;

		// Token: 0x04001573 RID: 5491
		public static readonly GenerationNumber Zero = new GenerationNumber(0L, string.Empty);
	}
}
