using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000136 RID: 310
	[DataContract]
	internal sealed class ResultEncodingHints
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00010254 File Offset: 0x0000E454
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x0001025C File Offset: 0x0000E45C
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 0)]
		internal bool DisableDictionaryEncoding { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00010265 File Offset: 0x0000E465
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x0001026D File Offset: 0x0000E46D
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 1)]
		internal IList<IList<string>> CalculationsWithSharedValues { get; set; }

		// Token: 0x06000853 RID: 2131 RVA: 0x00010276 File Offset: 0x0000E476
		public static ResultEncodingHints Create(IList<IList<string>> calculationsWithSharedValues, bool disableDictionaryEncoding)
		{
			if (disableDictionaryEncoding)
			{
				return new ResultEncodingHints
				{
					DisableDictionaryEncoding = disableDictionaryEncoding
				};
			}
			if (!calculationsWithSharedValues.IsNullOrEmpty<IList<string>>())
			{
				return new ResultEncodingHints
				{
					CalculationsWithSharedValues = calculationsWithSharedValues
				};
			}
			return null;
		}
	}
}
