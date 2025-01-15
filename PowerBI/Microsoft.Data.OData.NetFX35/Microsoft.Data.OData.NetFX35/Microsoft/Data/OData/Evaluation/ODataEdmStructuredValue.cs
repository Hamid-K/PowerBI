using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000180 RID: 384
	internal sealed class ODataEdmStructuredValue : EdmValue, IEdmStructuredValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000A7E RID: 2686 RVA: 0x000234E0 File Offset: 0x000216E0
		internal ODataEdmStructuredValue(ODataEntry entry)
			: base(entry.GetEdmType())
		{
			this.properties = entry.NonComputedProperties;
			this.structuredType = ((base.Type == null) ? null : base.Type.AsStructured());
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00023516 File Offset: 0x00021716
		internal ODataEdmStructuredValue(ODataComplexValue complexValue)
			: base(complexValue.GetEdmType())
		{
			this.properties = complexValue.Properties;
			this.structuredType = ((base.Type == null) ? null : base.Type.AsStructured());
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x000236FC File Offset: 0x000218FC
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

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00023719 File Offset: 0x00021919
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Structured;
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00023738 File Offset: 0x00021938
		public IEdmPropertyValue FindPropertyValue(string propertyName)
		{
			ODataProperty odataProperty = ((this.properties == null) ? null : Enumerable.FirstOrDefault<ODataProperty>(Enumerable.Where<ODataProperty>(this.properties, (ODataProperty p) => p.Name == propertyName)));
			if (odataProperty == null)
			{
				return null;
			}
			return odataProperty.GetEdmPropertyValue(this.structuredType);
		}

		// Token: 0x040003FF RID: 1023
		private readonly IEnumerable<ODataProperty> properties;

		// Token: 0x04000400 RID: 1024
		private readonly IEdmStructuredTypeReference structuredType;
	}
}
