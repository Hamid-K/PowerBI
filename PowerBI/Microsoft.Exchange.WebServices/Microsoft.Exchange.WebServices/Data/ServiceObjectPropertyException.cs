using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000263 RID: 611
	[Serializable]
	public class ServiceObjectPropertyException : PropertyException
	{
		// Token: 0x060015BD RID: 5565 RVA: 0x0003CEE2 File Offset: 0x0003BEE2
		public ServiceObjectPropertyException(PropertyDefinitionBase propertyDefinition)
			: base(propertyDefinition.GetPrintableName())
		{
			this.propertyDefinition = propertyDefinition;
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x0003CEF7 File Offset: 0x0003BEF7
		public ServiceObjectPropertyException(string message, PropertyDefinitionBase propertyDefinition)
			: base(message, propertyDefinition.GetPrintableName())
		{
			this.propertyDefinition = propertyDefinition;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0003CF0D File Offset: 0x0003BF0D
		public ServiceObjectPropertyException(string message, PropertyDefinitionBase propertyDefinition, Exception innerException)
			: base(message, propertyDefinition.GetPrintableName(), innerException)
		{
			this.propertyDefinition = propertyDefinition;
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x0003CF24 File Offset: 0x0003BF24
		public PropertyDefinitionBase PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
		}

		// Token: 0x040012BA RID: 4794
		private PropertyDefinitionBase propertyDefinition;
	}
}
