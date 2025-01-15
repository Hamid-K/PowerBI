using System;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm
{
	// Token: 0x020000A6 RID: 166
	public sealed class ExtensionEntityDataModel
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x0001D55F File Offset: 0x0001B75F
		internal ExtensionEntityDataModel(string name, IReadOnlyExtensionEdmItemCollection<EdmMeasure> measures, IReadOnlyExtensionEdmItemCollection<EdmField> fields)
		{
			this.m_name = name;
			this.m_measures = measures;
			this.m_fields = fields;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0001D57C File Offset: 0x0001B77C
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001D584 File Offset: 0x0001B784
		public bool TryGetMeasure(string entitySetReferenceName, string measureReferenceName, out EdmMeasure measure)
		{
			return this.m_measures.TryGetItem(entitySetReferenceName, measureReferenceName, out measure);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001D594 File Offset: 0x0001B794
		public bool TryGetField(string entitySetReferenceName, string fieldReferenceName, out EdmField field)
		{
			return this.m_fields.TryGetItem(entitySetReferenceName, fieldReferenceName, out field);
		}

		// Token: 0x040003C7 RID: 967
		private readonly string m_name;

		// Token: 0x040003C8 RID: 968
		private readonly IReadOnlyExtensionEdmItemCollection<EdmMeasure> m_measures;

		// Token: 0x040003C9 RID: 969
		private readonly IReadOnlyExtensionEdmItemCollection<EdmField> m_fields;
	}
}
