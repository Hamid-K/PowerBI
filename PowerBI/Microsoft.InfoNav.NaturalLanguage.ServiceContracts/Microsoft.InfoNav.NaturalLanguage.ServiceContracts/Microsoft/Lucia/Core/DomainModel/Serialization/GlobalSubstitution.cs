using System;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001A3 RID: 419
	public sealed class GlobalSubstitution : SingletonMapping<GlobalSubstitutionProperties>
	{
		// Token: 0x060008A2 RID: 2210 RVA: 0x00011697 File Offset: 0x0000F897
		public GlobalSubstitution()
		{
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001169F File Offset: 0x0000F89F
		public GlobalSubstitution(string original, GlobalSubstitutionProperties properties)
			: base(original, properties)
		{
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x000116A9 File Offset: 0x0000F8A9
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x000116B1 File Offset: 0x0000F8B1
		public string Value
		{
			get
			{
				return base.UnderlyingKey;
			}
			set
			{
				base.UnderlyingKey = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x000116BA File Offset: 0x0000F8BA
		public GlobalSubstitutionProperties Properties
		{
			get
			{
				return base.UnderlyingValue;
			}
		}
	}
}
