using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library.Values;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x02000086 RID: 134
	internal sealed class ODataEdmStructuredValue : EdmValue, IEdmStructuredValue, IEdmValue, IEdmElement
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x00013852 File Offset: 0x00011A52
		internal ODataEdmStructuredValue(ODataEntry entry)
			: base(entry.GetEdmType())
		{
			this.properties = entry.NonComputedProperties;
			this.structuredType = ((base.Type == null) ? null : base.Type.AsStructured());
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00013888 File Offset: 0x00011A88
		internal ODataEdmStructuredValue(ODataComplexValue complexValue)
			: base(complexValue.GetEdmType())
		{
			this.properties = complexValue.Properties;
			this.structuredType = ((base.Type == null) ? null : base.Type.AsStructured());
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00013A70 File Offset: 0x00011C70
		public IEnumerable<IEdmPropertyValue> PropertyValues
		{
			get
			{
				if (this.properties != null)
				{
					foreach (ODataProperty property in this.properties)
					{
						yield return property.GetEdmPropertyValue(this.structuredType);
					}
				}
				yield break;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00013A8D File Offset: 0x00011C8D
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Structured;
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00013AAC File Offset: 0x00011CAC
		public IEdmPropertyValue FindPropertyValue(string propertyName)
		{
			ODataProperty odataProperty = ((this.properties == null) ? null : Enumerable.FirstOrDefault<ODataProperty>(Enumerable.Where<ODataProperty>(this.properties, (ODataProperty p) => p.Name == propertyName)));
			if (odataProperty == null)
			{
				return null;
			}
			return odataProperty.GetEdmPropertyValue(this.structuredType);
		}

		// Token: 0x04000241 RID: 577
		private readonly IEnumerable<ODataProperty> properties;

		// Token: 0x04000242 RID: 578
		private readonly IEdmStructuredTypeReference structuredType;
	}
}
