using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x0200014C RID: 332
	internal sealed class TmdlStructValue : TmdlValue
	{
		// Token: 0x06001560 RID: 5472 RVA: 0x0008FA52 File Offset: 0x0008DC52
		public TmdlStructValue()
			: base(TmdlValueType.Struct, string.Empty, true, true)
		{
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0008FA62 File Offset: 0x0008DC62
		public bool IsEmpty
		{
			get
			{
				return this.properties == null || this.properties.Count == 0;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0008FA7C File Offset: 0x0008DC7C
		public ICollection<TmdlProperty> Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new List<TmdlProperty>();
				}
				return this.properties;
			}
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0008FA98 File Offset: 0x0008DC98
		private protected override void WriteBody(ITmdlWriter writer)
		{
			if (this.properties != null)
			{
				foreach (TmdlProperty tmdlProperty in this.properties)
				{
					tmdlProperty.WriteTo(writer);
				}
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0008FAF4 File Offset: 0x0008DCF4
		internal TValue ValuesAs<TValue>(string name) where TValue : TmdlValue
		{
			return this.Properties.First((TmdlProperty p) => p.Name == name).ValueAs<TValue>();
		}

		// Token: 0x040003B5 RID: 949
		private List<TmdlProperty> properties;
	}
}
