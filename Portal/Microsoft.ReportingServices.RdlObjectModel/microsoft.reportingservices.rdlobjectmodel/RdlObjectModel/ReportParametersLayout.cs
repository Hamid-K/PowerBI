using System;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001E7 RID: 487
	public class ReportParametersLayout : ReportObject, IShouldSerialize
	{
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x00026481 File Offset: 0x00024681
		// (set) Token: 0x0600102E RID: 4142 RVA: 0x0002648F File Offset: 0x0002468F
		public GridLayoutDefinition GridLayoutDefinition
		{
			get
			{
				return base.PropertyStore.GetObject<GridLayoutDefinition>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0002649E File Offset: 0x0002469E
		public ReportParametersLayout()
		{
			this.GridLayoutDefinition = new GridLayoutDefinition();
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x000264B1 File Offset: 0x000246B1
		internal ReportParametersLayout(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x000264BA File Offset: 0x000246BA
		bool IShouldSerialize.ShouldSerializeThis()
		{
			return true;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x000264BD File Offset: 0x000246BD
		SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
		{
			return SerializationMethod.Auto;
		}

		// Token: 0x020003F2 RID: 1010
		internal class Definition : DefinitionStore<GridLayoutDefinition, ReportParametersLayout.Definition.Properties>
		{
			// Token: 0x060018B4 RID: 6324 RVA: 0x0003BB5F File Offset: 0x00039D5F
			private Definition()
			{
			}

			// Token: 0x02000504 RID: 1284
			internal enum Properties
			{
				// Token: 0x040010C0 RID: 4288
				GridLayoutDefinition
			}
		}
	}
}
