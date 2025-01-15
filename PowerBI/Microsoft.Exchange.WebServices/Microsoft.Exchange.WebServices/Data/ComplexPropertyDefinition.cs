using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002C6 RID: 710
	internal class ComplexPropertyDefinition<TComplexProperty> : ComplexPropertyDefinitionBase where TComplexProperty : ComplexProperty
	{
		// Token: 0x06001951 RID: 6481 RVA: 0x00044CE8 File Offset: 0x00043CE8
		internal ComplexPropertyDefinition(string xmlElementName, PropertyDefinitionFlags flags, ExchangeVersion version, CreateComplexPropertyDelegate<TComplexProperty> propertyCreationDelegate)
			: base(xmlElementName, flags, version)
		{
			EwsUtilities.Assert(propertyCreationDelegate != null, "ComplexPropertyDefinition ctor", "CreateComplexPropertyDelegate cannot be null");
			this.propertyCreationDelegate = propertyCreationDelegate;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00044D12 File Offset: 0x00043D12
		internal ComplexPropertyDefinition(string xmlElementName, string uri, ExchangeVersion version, CreateComplexPropertyDelegate<TComplexProperty> propertyCreationDelegate)
			: base(xmlElementName, uri, version)
		{
			this.propertyCreationDelegate = propertyCreationDelegate;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00044D25 File Offset: 0x00043D25
		internal ComplexPropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version, CreateComplexPropertyDelegate<TComplexProperty> propertyCreationDelegate)
			: base(xmlElementName, uri, flags, version)
		{
			this.propertyCreationDelegate = propertyCreationDelegate;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00044D3C File Offset: 0x00043D3C
		internal override ComplexProperty CreatePropertyInstance(ServiceObject owner)
		{
			TComplexProperty tcomplexProperty = this.propertyCreationDelegate();
			IOwnedProperty ownedProperty = tcomplexProperty as IOwnedProperty;
			if (ownedProperty != null)
			{
				ownedProperty.Owner = owner;
			}
			return tcomplexProperty;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x00044D71 File Offset: 0x00043D71
		public override Type Type
		{
			get
			{
				return typeof(TComplexProperty);
			}
		}

		// Token: 0x040013F2 RID: 5106
		private CreateComplexPropertyDelegate<TComplexProperty> propertyCreationDelegate;
	}
}
