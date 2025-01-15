using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000CF RID: 207
	[DataContract]
	internal sealed class IntermediateTableSchemaKey : IIntermediateTableSchemaItem
	{
		// Token: 0x06000769 RID: 1897 RVA: 0x0001C0C8 File Offset: 0x0001A2C8
		internal IntermediateTableSchemaKey(string valueCalculationId, IConceptualColumn source)
		{
			this.ValueCalculationId = valueCalculationId;
			this.Source = source;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001C0DE File Offset: 0x0001A2DE
		[DataMember(Name = "ValueCalculationId", Order = 1)]
		public string ValueCalculationId { get; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001C0E6 File Offset: 0x0001A2E6
		internal IConceptualColumn Source { get; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001C0EE File Offset: 0x0001A2EE
		[DataMember(Name = "Source", EmitDefaultValue = false, Order = 2)]
		private string SourceForSerialization
		{
			get
			{
				return IntermediateTableSchemaSerializationUtils.Serialize(this.Source);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0001C0FB File Offset: 0x0001A2FB
		public string FormatString
		{
			get
			{
				IConceptualColumn source = this.Source;
				if (source == null)
				{
					return null;
				}
				return source.FormatString;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001C10E File Offset: 0x0001A30E
		public IConceptualProperty LineageProperty
		{
			get
			{
				return this.Source;
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001C116 File Offset: 0x0001A316
		public bool TryGetRelatedItem(IConceptualProperty lineageProperty, out IIntermediateTableSchemaItem item)
		{
			if (lineageProperty == this.Source)
			{
				item = this;
				return true;
			}
			item = null;
			return false;
		}
	}
}
