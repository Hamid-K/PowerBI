using System;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Tmdl.Schema;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Tmdl.Converters
{
	// Token: 0x02000163 RID: 355
	internal sealed class PropertyConverter : IPropertyConverter
	{
		// Token: 0x06001637 RID: 5687 RVA: 0x00093E54 File Offset: 0x00092054
		public PropertyConverter(MetadataObjectConverter parent, string propertyName)
		{
			this.parent = parent;
			this.PropertyName = propertyName;
			this.propertyType = PropertyConverter.IdentifyProperty(parent.Schema, propertyName, out this.propertyInfo);
			if (this.propertyType == PropertyConverter.PropertyType.Unknown)
			{
				throw new ArgumentException(TomSR.Exception_TmdlPropertyUnknownNature(propertyName), "propertyName");
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x00093EA6 File Offset: 0x000920A6
		public ObjectType ObjectType
		{
			get
			{
				return this.parent.ObjectType;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00093EB3 File Offset: 0x000920B3
		public string PropertyName { get; }

		// Token: 0x0600163A RID: 5690 RVA: 0x00093EBC File Offset: 0x000920BC
		public TmdlProperty GetProperty(MetadataObject instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (instance.ObjectType != this.parent.ObjectType)
			{
				throw new ArgumentException(TomSR.Exception_ComverterMismatchTypeInstance(this.parent.ObjectType.ToString("G"), instance.ObjectType.ToString("G")), "instance");
			}
			PropertyConverter.PropertyType propertyType = this.propertyType;
			IMetadataFilter metadataFilter;
			if (propertyType - PropertyConverter.PropertyType.Struct <= 1)
			{
				metadataFilter = new PropertyConverter.SingleChildPropertyFilter(this.parent.ObjectType, this.PropertyName);
			}
			else
			{
				metadataFilter = new PropertyConverter.SinglePropertyFilter(this.parent.ObjectType, this.PropertyName);
			}
			TmdlObjectWriter tmdlObjectWriter = new TmdlObjectWriter(new TmdlSerializationConfiguration(metadataFilter), this.parent.ObjectType);
			instance.SaveMetadata(new SerializationActivityContext(MetadataSerializationMode.Tmdl, CompatibilityMode.PowerBI, 1000000, false, false), tmdlObjectWriter);
			TmdlObject tmdlObject = tmdlObjectWriter.ExtractObject();
			switch (this.propertyType)
			{
			case PropertyConverter.PropertyType.Name:
				if (tmdlObject.Name.IsEmpty)
				{
					return null;
				}
				return new TmdlProperty("name", new TmdlStringValue(tmdlObject.Name.Name, null, false));
			case PropertyConverter.PropertyType.Description:
				if (tmdlObject.Description == null || tmdlObject.Description.IsEmpty())
				{
					return null;
				}
				return new TmdlProperty("description", new TmdlStringValue(tmdlObject.Description, TmdlStringFormat.Block, false));
			case PropertyConverter.PropertyType.Ordinal:
				if (tmdlObject.Ordinal == null)
				{
					return null;
				}
				return new TmdlProperty(this.PropertyName, TmdlValue.FromScalar<int>(tmdlObject.Ordinal.Value));
			case PropertyConverter.PropertyType.Default:
				if (tmdlObject.DefaultProperty != null)
				{
					return tmdlObject.DefaultProperty;
				}
				break;
			case PropertyConverter.PropertyType.Struct:
			{
				if (!tmdlObject.HasAnyChild(true))
				{
					return null;
				}
				TmdlObject tmdlObject2 = tmdlObject.Children.FirstOrDefault((TmdlObject c) => c.ObjectType == ObjectType.Null && string.Compare(c.Name.Name, this.PropertyName, StringComparison.InvariantCultureIgnoreCase) == 0);
				if (tmdlObject2 == null || !tmdlObject2.HasAnyProperty(false))
				{
					return null;
				}
				TmdlStructValue tmdlStructValue = new TmdlStructValue();
				if (tmdlObject2.HasAnyProperty(false))
				{
					foreach (TmdlProperty tmdlProperty in tmdlObject2.Properties)
					{
						tmdlStructValue.Properties.Add(tmdlProperty);
					}
				}
				return new TmdlProperty(this.PropertyName, tmdlStructValue);
			}
			case PropertyConverter.PropertyType.SingleChild:
			{
				if (!tmdlObject.HasAnyChild(false))
				{
					return null;
				}
				TmdlObject tmdlObject3 = tmdlObject.Children.FirstOrDefault((TmdlObject c) => c.ObjectType == this.propertyInfo.MetadataObjectType.Value && string.Compare(c.Name.Name, this.PropertyName, StringComparison.InvariantCultureIgnoreCase) == 0);
				if (tmdlObject3 == null || !tmdlObject3.HasAnyProperty(false))
				{
					return null;
				}
				TmdlMetadataObjectValue tmdlMetadataObjectValue = new TmdlMetadataObjectValue(this.propertyInfo.MetadataObjectType.Value);
				if (tmdlObject3.HasAnyProperty(false))
				{
					foreach (TmdlProperty tmdlProperty2 in tmdlObject3.Properties)
					{
						tmdlMetadataObjectValue.Object.Properties.Add(tmdlProperty2);
					}
				}
				return new TmdlProperty(this.PropertyName, tmdlMetadataObjectValue);
			}
			}
			return tmdlObject.GetPropertyByName(this.PropertyName, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000941D8 File Offset: 0x000923D8
		public void SetProperty(MetadataObject instance, TmdlProperty property)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			if (property.Value == null)
			{
				throw new ArgumentNullException("Value");
			}
			if (instance.ObjectType != this.parent.ObjectType)
			{
				throw new ArgumentException(TomSR.Exception_ComverterMismatchTypeInstance(this.parent.ObjectType.ToString("G"), instance.ObjectType.ToString("G")), "instance");
			}
			if (!this.PropertyName.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new ArgumentException(TomSR.Exception_ComverterMismatchPropertyName(this.PropertyName, property.Name), "property");
			}
			if (this.propertyInfo != null && !this.propertyInfo.Name.Equals(property.Name, StringComparison.Ordinal))
			{
				property = new TmdlProperty(this.propertyInfo.Name, property.Value);
			}
			TmdlObject tmdlObject = new TmdlObject(this.parent.ObjectType);
			switch (this.propertyType)
			{
			case PropertyConverter.PropertyType.Name:
			{
				TmdlStringValue tmdlStringValue = property.Value as TmdlStringValue;
				if (tmdlStringValue == null || tmdlStringValue.Lines == null || tmdlStringValue.Lines.Count == 0)
				{
					throw new ArgumentException(TomSR.Exception_TmdlPropertyMismatchValueType(property.Name), "Value");
				}
				tmdlObject.Name = new ObjectName(new string[] { tmdlStringValue.Lines[0] });
				break;
			}
			case PropertyConverter.PropertyType.Description:
			{
				TmdlStringValue tmdlStringValue2 = property.Value as TmdlStringValue;
				if (tmdlStringValue2 == null || tmdlStringValue2.Lines == null || tmdlStringValue2.Lines.Count == 0)
				{
					throw new ArgumentException(TomSR.Exception_TmdlPropertyMismatchValueType(property.Name), "Value");
				}
				tmdlObject.Description = tmdlStringValue2.Lines.ToArray<string>();
				break;
			}
			case PropertyConverter.PropertyType.Ordinal:
			{
				TmdlScalarValue<int> tmdlScalarValue = property.Value as TmdlScalarValue<int>;
				if (tmdlScalarValue == null || tmdlScalarValue.GetValue() == null)
				{
					throw new ArgumentException(TomSR.Exception_TmdlPropertyMismatchValueType(property.Name), "Value");
				}
				tmdlObject.Ordinal = new int?(tmdlScalarValue.GetValue().Value);
				break;
			}
			case PropertyConverter.PropertyType.Default:
				tmdlObject.DefaultProperty = property;
				break;
			case PropertyConverter.PropertyType.Regular:
				if (this.propertyInfo.IsDeprecated)
				{
					tmdlObject.AddDeprecatedProperty(property);
				}
				else
				{
					tmdlObject.Properties.Add(property);
				}
				break;
			case PropertyConverter.PropertyType.Struct:
				tmdlObject.AddDeprecatedProperty(property);
				break;
			case PropertyConverter.PropertyType.SingleChild:
			{
				TmdlMetadataObjectValue tmdlMetadataObjectValue = property.Value as TmdlMetadataObjectValue;
				if (tmdlMetadataObjectValue == null || tmdlMetadataObjectValue.Object == null)
				{
					throw new ArgumentException(TomSR.Exception_TmdlPropertyMismatchValueType(property.Name), "Value");
				}
				tmdlObject.Children.Add(tmdlMetadataObjectValue.Object);
				break;
			}
			default:
				throw TomInternalException.Create("How can that be? we covered all the valid options! - propertyType={0}", new object[] { this.propertyType });
			}
			TmdlObjectReader tmdlObjectReader = new TmdlObjectReader(tmdlObject);
			instance.LoadMetadata(new SerializationActivityContext(MetadataSerializationMode.Tmdl, CompatibilityMode.PowerBI, 1000000, false, false), tmdlObjectReader);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000944D2 File Offset: 0x000926D2
		internal TmdlPropertyInfo GetSchema()
		{
			return this.propertyInfo;
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000944DC File Offset: 0x000926DC
		private static PropertyConverter.PropertyType IdentifyProperty(TmdlObjectInfo objectInfo, string propertyName, out TmdlPropertyInfo propertyInfo)
		{
			propertyInfo = objectInfo.FindProperty(propertyName);
			if (propertyInfo != null)
			{
				if (propertyInfo.IsDefaultProperty)
				{
					return PropertyConverter.PropertyType.Default;
				}
				if (propertyInfo.IsDeprecated && propertyInfo.Type == TmdlValueType.Struct)
				{
					return PropertyConverter.PropertyType.Struct;
				}
				if (propertyInfo.IsDeprecated && propertyInfo.Type == TmdlValueType.MetadataObject)
				{
					return PropertyConverter.PropertyType.SingleChild;
				}
				return PropertyConverter.PropertyType.Regular;
			}
			else
			{
				if (string.Compare(propertyName, "name", StringComparison.InvariantCultureIgnoreCase) == 0 && objectInfo.RequiresName)
				{
					return PropertyConverter.PropertyType.Name;
				}
				if (string.Compare(propertyName, "description", StringComparison.InvariantCultureIgnoreCase) == 0 && objectInfo.HasDescription)
				{
					return PropertyConverter.PropertyType.Description;
				}
				ObjectType objectType = objectInfo.ObjectType;
				if (objectType != ObjectType.Level)
				{
					if (objectType != ObjectType.CalculationItem)
					{
						if (ObjectTreeHelper.IsKeyedObject(objectInfo.ObjectType) && string.Compare(propertyName, ObjectTreeHelper.GetKeyedObjectKeyPropertyName(objectInfo.ObjectType), StringComparison.InvariantCultureIgnoreCase) == 0)
						{
							return PropertyConverter.PropertyType.Name;
						}
					}
					else if (string.Compare(propertyName, "ordinal", StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						return PropertyConverter.PropertyType.Ordinal;
					}
				}
				else if (string.Compare(propertyName, "ordinal", StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return PropertyConverter.PropertyType.Ordinal;
				}
				return PropertyConverter.PropertyType.Unknown;
			}
		}

		// Token: 0x04000414 RID: 1044
		private readonly MetadataObjectConverter parent;

		// Token: 0x04000415 RID: 1045
		private readonly TmdlPropertyInfo propertyInfo;

		// Token: 0x04000416 RID: 1046
		private readonly PropertyConverter.PropertyType propertyType;

		// Token: 0x02000345 RID: 837
		private enum PropertyType
		{
			// Token: 0x04000E49 RID: 3657
			Unknown,
			// Token: 0x04000E4A RID: 3658
			Name,
			// Token: 0x04000E4B RID: 3659
			Description,
			// Token: 0x04000E4C RID: 3660
			Ordinal,
			// Token: 0x04000E4D RID: 3661
			Default,
			// Token: 0x04000E4E RID: 3662
			Regular,
			// Token: 0x04000E4F RID: 3663
			Struct,
			// Token: 0x04000E50 RID: 3664
			SingleChild
		}

		// Token: 0x02000346 RID: 838
		private sealed class SinglePropertyFilter : IMetadataFilter
		{
			// Token: 0x06002586 RID: 9606 RVA: 0x000E869D File Offset: 0x000E689D
			public SinglePropertyFilter(ObjectType owner, string propertyName)
			{
				this.owner = owner;
				this.propertyName = propertyName;
			}

			// Token: 0x06002587 RID: 9607 RVA: 0x000E86B4 File Offset: 0x000E68B4
			public bool IgnoreProperty(ObjectType owner, string propertyName, MetadataPropertyNature propertyNature)
			{
				MetadataPropertyNature metadataPropertyNature = propertyNature & MetadataPropertyNature.PropertyCategoryMask;
				return (metadataPropertyNature - MetadataPropertyNature.TypeProperty > 2 && metadataPropertyNature != MetadataPropertyNature.CrossLinkProperty) || owner != this.owner || string.Compare(propertyName, this.propertyName, StringComparison.InvariantCultureIgnoreCase) != 0;
			}

			// Token: 0x06002588 RID: 9608 RVA: 0x000E86ED File Offset: 0x000E68ED
			public bool IgnoreChild(ObjectType owner, string propertyName, MetadataPropertyNature propertyNature, MetadataObject @object)
			{
				return true;
			}

			// Token: 0x04000E51 RID: 3665
			private ObjectType owner;

			// Token: 0x04000E52 RID: 3666
			private string propertyName;
		}

		// Token: 0x02000347 RID: 839
		private sealed class SingleChildPropertyFilter : IMetadataFilter
		{
			// Token: 0x06002589 RID: 9609 RVA: 0x000E86F0 File Offset: 0x000E68F0
			public SingleChildPropertyFilter(ObjectType owner, string propertyName)
			{
				this.owner = owner;
				this.propertyName = propertyName;
			}

			// Token: 0x0600258A RID: 9610 RVA: 0x000E8706 File Offset: 0x000E6906
			public bool IgnoreProperty(ObjectType owner, string propertyName, MetadataPropertyNature propertyNature)
			{
				return (propertyNature & MetadataPropertyNature.PropertyCategoryMask) != MetadataPropertyNature.ChildProperty || owner != this.owner || string.Compare(propertyName, this.propertyName, StringComparison.InvariantCultureIgnoreCase) != 0;
			}

			// Token: 0x0600258B RID: 9611 RVA: 0x000E872C File Offset: 0x000E692C
			public bool IgnoreChild(ObjectType owner, string propertyName, MetadataPropertyNature propertyNature, MetadataObject @object)
			{
				return owner != this.owner || string.Compare(propertyName, this.propertyName, StringComparison.InvariantCultureIgnoreCase) != 0;
			}

			// Token: 0x04000E53 RID: 3667
			private ObjectType owner;

			// Token: 0x04000E54 RID: 3668
			private string propertyName;
		}
	}
}
