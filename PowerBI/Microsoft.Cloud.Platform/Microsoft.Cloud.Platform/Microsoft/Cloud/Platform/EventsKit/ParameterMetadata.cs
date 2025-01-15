using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200036C RID: 876
	public class ParameterMetadata : VariableMetadata
	{
		// Token: 0x06001A11 RID: 6673 RVA: 0x00060675 File Offset: 0x0005E875
		public ParameterMetadata(Type type, string name, IList<WireFieldMetadata> wireFields, PropertyMetadata property)
			: base(type, name)
		{
			this.m_wireFields = wireFields;
			this.Property = property;
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x0006068E File Offset: 0x0005E88E
		public ReadOnlyCollection<WireFieldMetadata> WireFields
		{
			get
			{
				return new ReadOnlyCollection<WireFieldMetadata>(this.m_wireFields);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x0006069B File Offset: 0x0005E89B
		// (set) Token: 0x06001A14 RID: 6676 RVA: 0x000606A3 File Offset: 0x0005E8A3
		public PropertyMetadata Property { get; private set; }

		// Token: 0x04000900 RID: 2304
		private readonly IList<WireFieldMetadata> m_wireFields;
	}
}
