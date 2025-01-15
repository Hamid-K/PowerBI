using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000DF RID: 223
	public sealed class PropertySet : ISelfValidate, IEnumerable<PropertyDefinitionBase>, IEnumerable
	{
		// Token: 0x06000B49 RID: 2889 RVA: 0x00025B1C File Offset: 0x00024B1C
		public PropertySet(BasePropertySet basePropertySet, params PropertyDefinitionBase[] additionalProperties)
			: this(basePropertySet, (IEnumerable<PropertyDefinitionBase>)additionalProperties)
		{
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00025B2B File Offset: 0x00024B2B
		public PropertySet(BasePropertySet basePropertySet, IEnumerable<PropertyDefinitionBase> additionalProperties)
		{
			this.basePropertySet = basePropertySet;
			if (additionalProperties != null)
			{
				this.additionalProperties.AddRange(additionalProperties);
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00025B54 File Offset: 0x00024B54
		public PropertySet()
			: this(BasePropertySet.IdOnly, null)
		{
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00025B5E File Offset: 0x00024B5E
		public PropertySet(BasePropertySet basePropertySet)
			: this(basePropertySet, null)
		{
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00025B68 File Offset: 0x00024B68
		public PropertySet(params PropertyDefinitionBase[] additionalProperties)
			: this(BasePropertySet.IdOnly, additionalProperties)
		{
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00025B72 File Offset: 0x00024B72
		public PropertySet(IEnumerable<PropertyDefinitionBase> additionalProperties)
			: this(BasePropertySet.IdOnly, additionalProperties)
		{
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00025B7C File Offset: 0x00024B7C
		public static implicit operator PropertySet(BasePropertySet basePropertySet)
		{
			return new PropertySet(basePropertySet);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00025B84 File Offset: 0x00024B84
		public void Add(PropertyDefinitionBase property)
		{
			this.ThrowIfReadonly();
			EwsUtilities.ValidateParam(property, "property");
			if (!this.additionalProperties.Contains(property))
			{
				this.additionalProperties.Add(property);
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00025BB4 File Offset: 0x00024BB4
		public void AddRange(IEnumerable<PropertyDefinitionBase> properties)
		{
			this.ThrowIfReadonly();
			EwsUtilities.ValidateParamCollection(properties, "properties");
			foreach (PropertyDefinitionBase propertyDefinitionBase in properties)
			{
				this.Add(propertyDefinitionBase);
			}
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00025C10 File Offset: 0x00024C10
		public void Clear()
		{
			this.ThrowIfReadonly();
			this.additionalProperties.Clear();
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00025C24 File Offset: 0x00024C24
		private static PropertySet CreateReadonlyPropertySet(BasePropertySet basePropertySet)
		{
			return new PropertySet(basePropertySet)
			{
				isReadOnly = true
			};
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00025C40 File Offset: 0x00024C40
		private static string GetShapeName(ServiceObjectType serviceObjectType)
		{
			switch (serviceObjectType)
			{
			case ServiceObjectType.Folder:
				return "FolderShape";
			case ServiceObjectType.Item:
				return "ItemShape";
			case ServiceObjectType.Conversation:
				return "ConversationShape";
			default:
				EwsUtilities.Assert(false, "PropertySet.GetShapeName", string.Format("An unexpected object type {0} for property shape. This code path should never be reached.", serviceObjectType));
				return string.Empty;
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00025C95 File Offset: 0x00024C95
		private void ThrowIfReadonly()
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException(Strings.PropertySetCannotBeModified);
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00025CAF File Offset: 0x00024CAF
		public bool Contains(PropertyDefinitionBase property)
		{
			return this.additionalProperties.Contains(property);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00025CBD File Offset: 0x00024CBD
		public bool Remove(PropertyDefinitionBase property)
		{
			this.ThrowIfReadonly();
			return this.additionalProperties.Remove(property);
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00025CD1 File Offset: 0x00024CD1
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x00025CD9 File Offset: 0x00024CD9
		public BasePropertySet BasePropertySet
		{
			get
			{
				return this.basePropertySet;
			}
			set
			{
				this.ThrowIfReadonly();
				this.basePropertySet = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00025CE8 File Offset: 0x00024CE8
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x00025CF0 File Offset: 0x00024CF0
		public BodyType? RequestedBodyType
		{
			get
			{
				return this.requestedBodyType;
			}
			set
			{
				this.ThrowIfReadonly();
				this.requestedBodyType = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00025CFF File Offset: 0x00024CFF
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x00025D07 File Offset: 0x00024D07
		public BodyType? RequestedUniqueBodyType
		{
			get
			{
				return this.requestedUniqueBodyType;
			}
			set
			{
				this.ThrowIfReadonly();
				this.requestedUniqueBodyType = value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00025D16 File Offset: 0x00024D16
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00025D1E File Offset: 0x00024D1E
		public BodyType? RequestedNormalizedBodyType
		{
			get
			{
				return this.requestedNormalizedBodyType;
			}
			set
			{
				this.ThrowIfReadonly();
				this.requestedNormalizedBodyType = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x00025D2D File Offset: 0x00024D2D
		public int Count
		{
			get
			{
				return this.additionalProperties.Count;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00025D3A File Offset: 0x00024D3A
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x00025D42 File Offset: 0x00024D42
		public bool? FilterHtmlContent
		{
			get
			{
				return this.filterHtml;
			}
			set
			{
				this.ThrowIfReadonly();
				this.filterHtml = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00025D51 File Offset: 0x00024D51
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x00025D59 File Offset: 0x00024D59
		public bool? ConvertHtmlCodePageToUTF8
		{
			get
			{
				return this.convertHtmlCodePageToUTF8;
			}
			set
			{
				this.ThrowIfReadonly();
				this.convertHtmlCodePageToUTF8 = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00025D68 File Offset: 0x00024D68
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x00025D70 File Offset: 0x00024D70
		public string InlineImageUrlTemplate
		{
			get
			{
				return this.inlineImageUrlTemplate;
			}
			set
			{
				this.ThrowIfReadonly();
				this.inlineImageUrlTemplate = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x00025D7F File Offset: 0x00024D7F
		// (set) Token: 0x06000B68 RID: 2920 RVA: 0x00025D87 File Offset: 0x00024D87
		public bool? BlockExternalImages
		{
			get
			{
				return this.blockExternalImages;
			}
			set
			{
				this.ThrowIfReadonly();
				this.blockExternalImages = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00025D96 File Offset: 0x00024D96
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x00025D9E File Offset: 0x00024D9E
		public bool? AddBlankTargetToLinks
		{
			get
			{
				return this.addTargetToLinks;
			}
			set
			{
				this.ThrowIfReadonly();
				this.addTargetToLinks = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00025DAD File Offset: 0x00024DAD
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x00025DB5 File Offset: 0x00024DB5
		public int? MaximumBodySize
		{
			get
			{
				return this.maximumBodySize;
			}
			set
			{
				this.ThrowIfReadonly();
				this.maximumBodySize = value;
			}
		}

		// Token: 0x17000283 RID: 643
		public PropertyDefinitionBase this[int index]
		{
			get
			{
				return this.additionalProperties[index];
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00025DD2 File Offset: 0x00024DD2
		void ISelfValidate.Validate()
		{
			this.InternalValidate();
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x00025DDA File Offset: 0x00024DDA
		internal static LazyMember<Dictionary<BasePropertySet, string>> DefaultPropertySetMap
		{
			get
			{
				return PropertySet.defaultPropertySetMap;
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00025DE4 File Offset: 0x00024DE4
		internal static void WriteAdditionalPropertiesToXml(EwsServiceXmlWriter writer, IEnumerable<PropertyDefinitionBase> propertyDefinitions)
		{
			writer.WriteStartElement(XmlNamespace.Types, "AdditionalProperties");
			foreach (PropertyDefinitionBase propertyDefinitionBase in propertyDefinitions)
			{
				propertyDefinitionBase.WriteToXml(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00025E40 File Offset: 0x00024E40
		internal static void WriteAdditionalPropertiesToJson(JsonObject jsonItemShape, ExchangeService service, IEnumerable<PropertyDefinitionBase> propertyDefinitions)
		{
			List<object> list = new List<object>();
			foreach (PropertyDefinitionBase propertyDefinitionBase in propertyDefinitions)
			{
				list.Add(((IJsonSerializable)propertyDefinitionBase).ToJson(service));
			}
			jsonItemShape.Add("AdditionalProperties", list.ToArray());
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00025EA8 File Offset: 0x00024EA8
		internal void InternalValidate()
		{
			for (int i = 0; i < this.additionalProperties.Count; i++)
			{
				if (this.additionalProperties[i] == null)
				{
					throw new ServiceValidationException(string.Format(Strings.AdditionalPropertyIsNull, i));
				}
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00025EF4 File Offset: 0x00024EF4
		internal void ValidateForRequest(ServiceRequestBase request, bool summaryPropertiesOnly)
		{
			foreach (PropertyDefinitionBase propertyDefinitionBase in this.additionalProperties)
			{
				PropertyDefinition propertyDefinition = propertyDefinitionBase as PropertyDefinition;
				if (propertyDefinition != null)
				{
					if (propertyDefinition.Version > request.Service.RequestedServerVersion)
					{
						throw new ServiceVersionException(string.Format(Strings.PropertyIncompatibleWithRequestVersion, propertyDefinition.Name, propertyDefinition.Version));
					}
					if (summaryPropertiesOnly && !propertyDefinition.HasFlag(PropertyDefinitionFlags.CanFind, new ExchangeVersion?(request.Service.RequestedServerVersion)))
					{
						throw new ServiceValidationException(string.Format(Strings.NonSummaryPropertyCannotBeUsed, propertyDefinition.Name, request.GetXmlElementName()));
					}
				}
			}
			if (this.FilterHtmlContent != null && request.Service.RequestedServerVersion < ExchangeVersion.Exchange2010)
			{
				throw new ServiceVersionException(string.Format(Strings.PropertyIncompatibleWithRequestVersion, "FilterHtmlContent", ExchangeVersion.Exchange2010));
			}
			if (this.ConvertHtmlCodePageToUTF8 != null && request.Service.RequestedServerVersion < ExchangeVersion.Exchange2010_SP1)
			{
				throw new ServiceVersionException(string.Format(Strings.PropertyIncompatibleWithRequestVersion, "ConvertHtmlCodePageToUTF8", ExchangeVersion.Exchange2010_SP1));
			}
			if (!string.IsNullOrEmpty(this.InlineImageUrlTemplate) && request.Service.RequestedServerVersion < ExchangeVersion.Exchange2013)
			{
				throw new ServiceVersionException(string.Format(Strings.PropertyIncompatibleWithRequestVersion, "InlineImageUrlTemplate", ExchangeVersion.Exchange2013));
			}
			if (this.BlockExternalImages != null && request.Service.RequestedServerVersion < ExchangeVersion.Exchange2013)
			{
				throw new ServiceVersionException(string.Format(Strings.PropertyIncompatibleWithRequestVersion, "BlockExternalImages", ExchangeVersion.Exchange2013));
			}
			if (this.AddBlankTargetToLinks != null && request.Service.RequestedServerVersion < ExchangeVersion.Exchange2013)
			{
				throw new ServiceVersionException(string.Format(Strings.PropertyIncompatibleWithRequestVersion, "AddTargetToLinks", ExchangeVersion.Exchange2013));
			}
			if (this.MaximumBodySize != null && request.Service.RequestedServerVersion < ExchangeVersion.Exchange2013)
			{
				throw new ServiceVersionException(string.Format(Strings.PropertyIncompatibleWithRequestVersion, "MaximumBodySize", ExchangeVersion.Exchange2013));
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002613C File Offset: 0x0002513C
		internal void WriteToXml(EwsServiceXmlWriter writer, ServiceObjectType serviceObjectType)
		{
			string shapeName = PropertySet.GetShapeName(serviceObjectType);
			writer.WriteStartElement(XmlNamespace.Messages, shapeName);
			writer.WriteElementValue(XmlNamespace.Types, "BaseShape", PropertySet.defaultPropertySetMap.Member[this.BasePropertySet]);
			if (serviceObjectType == ServiceObjectType.Item)
			{
				if (this.RequestedBodyType != null)
				{
					writer.WriteElementValue(XmlNamespace.Types, "BodyType", this.RequestedBodyType.Value);
				}
				if (this.RequestedUniqueBodyType != null)
				{
					writer.WriteElementValue(XmlNamespace.Types, "UniqueBodyType", this.RequestedUniqueBodyType.Value);
				}
				if (this.RequestedNormalizedBodyType != null)
				{
					writer.WriteElementValue(XmlNamespace.Types, "NormalizedBodyType", this.RequestedNormalizedBodyType.Value);
				}
				if (this.FilterHtmlContent != null)
				{
					writer.WriteElementValue(XmlNamespace.Types, "FilterHtmlContent", this.FilterHtmlContent.Value);
				}
				if (this.ConvertHtmlCodePageToUTF8 != null && writer.Service.RequestedServerVersion >= ExchangeVersion.Exchange2010_SP1)
				{
					writer.WriteElementValue(XmlNamespace.Types, "ConvertHtmlCodePageToUTF8", this.ConvertHtmlCodePageToUTF8.Value);
				}
				if (!string.IsNullOrEmpty(this.InlineImageUrlTemplate) && writer.Service.RequestedServerVersion >= ExchangeVersion.Exchange2013)
				{
					writer.WriteElementValue(XmlNamespace.Types, "InlineImageUrlTemplate", this.InlineImageUrlTemplate);
				}
				if (this.BlockExternalImages != null && writer.Service.RequestedServerVersion >= ExchangeVersion.Exchange2013)
				{
					writer.WriteElementValue(XmlNamespace.Types, "BlockExternalImages", this.BlockExternalImages.Value);
				}
				if (this.AddBlankTargetToLinks != null && writer.Service.RequestedServerVersion >= ExchangeVersion.Exchange2013)
				{
					writer.WriteElementValue(XmlNamespace.Types, "AddBlankTargetToLinks", this.AddBlankTargetToLinks.Value);
				}
				if (this.MaximumBodySize != null && writer.Service.RequestedServerVersion >= ExchangeVersion.Exchange2013)
				{
					writer.WriteElementValue(XmlNamespace.Types, "MaximumBodySize", this.MaximumBodySize.Value);
				}
			}
			if (this.additionalProperties.Count > 0)
			{
				PropertySet.WriteAdditionalPropertiesToXml(writer, this.additionalProperties);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002638C File Offset: 0x0002538C
		internal void WriteGetShapeToJson(JsonObject jsonRequest, ExchangeService service, ServiceObjectType serviceObjectType)
		{
			string shapeName = PropertySet.GetShapeName(serviceObjectType);
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("BaseShape", PropertySet.defaultPropertySetMap.Member[this.BasePropertySet]);
			if (serviceObjectType == ServiceObjectType.Item)
			{
				if (this.RequestedBodyType != null)
				{
					jsonObject.Add("BodyType", this.RequestedBodyType.Value);
				}
				if (this.FilterHtmlContent != null)
				{
					jsonObject.Add("FilterHtmlContent", this.FilterHtmlContent.Value);
				}
				if (this.ConvertHtmlCodePageToUTF8 != null && service.RequestedServerVersion >= ExchangeVersion.Exchange2010_SP1)
				{
					jsonObject.Add("ConvertHtmlCodePageToUTF8", this.ConvertHtmlCodePageToUTF8.Value);
				}
				if (!string.IsNullOrEmpty(this.InlineImageUrlTemplate) && service.RequestedServerVersion >= ExchangeVersion.Exchange2013)
				{
					jsonObject.Add("InlineImageUrlTemplate", this.InlineImageUrlTemplate);
				}
				if (this.BlockExternalImages != null && service.RequestedServerVersion >= ExchangeVersion.Exchange2013)
				{
					jsonObject.Add("BlockExternalImages", this.BlockExternalImages.Value);
				}
				if (this.AddBlankTargetToLinks != null && service.RequestedServerVersion >= ExchangeVersion.Exchange2013)
				{
					jsonObject.Add("AddBlankTargetToLinks", this.AddBlankTargetToLinks.Value);
				}
				if (this.MaximumBodySize != null && service.RequestedServerVersion >= ExchangeVersion.Exchange2013)
				{
					jsonObject.Add("MaximumBodySize", this.MaximumBodySize.Value);
				}
			}
			if (this.additionalProperties.Count > 0)
			{
				PropertySet.WriteAdditionalPropertiesToJson(jsonObject, service, this.additionalProperties);
			}
			jsonRequest.Add(shapeName, jsonObject);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00026540 File Offset: 0x00025540
		public IEnumerator<PropertyDefinitionBase> GetEnumerator()
		{
			return this.additionalProperties.GetEnumerator();
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00026552 File Offset: 0x00025552
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.additionalProperties.GetEnumerator();
		}

		// Token: 0x0400035F RID: 863
		public static readonly PropertySet IdOnly = PropertySet.CreateReadonlyPropertySet(BasePropertySet.IdOnly);

		// Token: 0x04000360 RID: 864
		public static readonly PropertySet FirstClassProperties = PropertySet.CreateReadonlyPropertySet(BasePropertySet.FirstClassProperties);

		// Token: 0x04000361 RID: 865
		private static LazyMember<Dictionary<BasePropertySet, string>> defaultPropertySetMap = new LazyMember<Dictionary<BasePropertySet, string>>(delegate
		{
			Dictionary<BasePropertySet, string> dictionary = new Dictionary<BasePropertySet, string>();
			dictionary.Add(BasePropertySet.IdOnly, "IdOnly");
			dictionary.Add(BasePropertySet.FirstClassProperties, "AllProperties");
			return dictionary;
		});

		// Token: 0x04000362 RID: 866
		private BasePropertySet basePropertySet;

		// Token: 0x04000363 RID: 867
		private List<PropertyDefinitionBase> additionalProperties = new List<PropertyDefinitionBase>();

		// Token: 0x04000364 RID: 868
		private BodyType? requestedBodyType;

		// Token: 0x04000365 RID: 869
		private BodyType? requestedUniqueBodyType;

		// Token: 0x04000366 RID: 870
		private BodyType? requestedNormalizedBodyType;

		// Token: 0x04000367 RID: 871
		private bool? filterHtml;

		// Token: 0x04000368 RID: 872
		private bool? convertHtmlCodePageToUTF8;

		// Token: 0x04000369 RID: 873
		private string inlineImageUrlTemplate;

		// Token: 0x0400036A RID: 874
		private bool? blockExternalImages;

		// Token: 0x0400036B RID: 875
		private bool? addTargetToLinks;

		// Token: 0x0400036C RID: 876
		private bool isReadOnly;

		// Token: 0x0400036D RID: 877
		private int? maximumBodySize;
	}
}
