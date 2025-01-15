using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Tmdl.Schema;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Tmdl.Converters
{
	// Token: 0x02000162 RID: 354
	internal sealed class MetadataObjectConverter : IMetadataObjectConverter
	{
		// Token: 0x06001630 RID: 5680 RVA: 0x00093B91 File Offset: 0x00091D91
		public MetadataObjectConverter(TmdlObjectInfo objectInfo)
		{
			this.ObjectType = objectInfo.ObjectType;
			this.Schema = objectInfo;
			this.propertyConevters = new Dictionary<string, PropertyConverter>(StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x00093BBC File Offset: 0x00091DBC
		public ObjectType ObjectType { get; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x00093BC4 File Offset: 0x00091DC4
		internal TmdlObjectInfo Schema { get; }

		// Token: 0x06001633 RID: 5683 RVA: 0x00093BCC File Offset: 0x00091DCC
		public MetadataObject FromTMDL(TmdlObject tmdlObject)
		{
			if (tmdlObject == null)
			{
				throw new ArgumentNullException("tmdlObject");
			}
			if (tmdlObject.ObjectType != this.ObjectType)
			{
				throw new ArgumentException(TomSR.Exception_ComverterMismatchType(this.ObjectType.ToString("G"), tmdlObject.ObjectType.ToString("G")), "tmdlObject");
			}
			return TmdlSerializationHelper.DeserializeMetadataObjectFromTmdlObject<MetadataObject>(tmdlObject, null);
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00093C38 File Offset: 0x00091E38
		public MetadataObject FromTMDL(TmdlObject tmdlObject, MetadataObject parent, MetadataObject existingInstance)
		{
			if (tmdlObject == null)
			{
				throw new ArgumentNullException("tmdlObject");
			}
			if (tmdlObject.ObjectType != this.ObjectType)
			{
				throw new ArgumentException(TomSR.Exception_ComverterMismatchType(this.ObjectType.ToString("G"), tmdlObject.ObjectType.ToString("G")), "tmdlObject");
			}
			if (existingInstance != null && existingInstance.ObjectType != tmdlObject.ObjectType)
			{
				throw new ArgumentException(TomSR.Exception_ComverterMismatchTypeInstance(existingInstance.ObjectType.ToString("G"), tmdlObject.ObjectType.ToString("G")), "existingInstance");
			}
			bool flag;
			if (parent != null)
			{
				if (!ObjectTreeHelper.IsChildObject(parent.ObjectType, tmdlObject.ObjectType, true, out flag))
				{
					throw new ArgumentException(TomSR.Exception_ComverterMismatchChildTypeParent(parent.ObjectType.ToString("G"), tmdlObject.ObjectType.ToString("G")), "parent");
				}
			}
			else
			{
				flag = false;
			}
			if (parent == null && existingInstance == null)
			{
				return TmdlSerializationHelper.DeserializeMetadataObjectFromTmdlObject<MetadataObject>(tmdlObject, null);
			}
			if (existingInstance != null)
			{
				TmdlObjectReader tmdlObjectReader = new TmdlObjectReader(tmdlObject);
				existingInstance.LoadMetadata(new SerializationActivityContext(MetadataSerializationMode.Tmdl, CompatibilityMode.PowerBI, 1000000, false, false), tmdlObjectReader);
				existingInstance.TryResolveAllCrossLinksInTreeByObjectPath(null);
			}
			else
			{
				existingInstance = TmdlSerializationHelper.DeserializeMetadataObjectFromTmdlObject<MetadataObject>(tmdlObject, null);
			}
			if (parent != null)
			{
				if (flag)
				{
					parent.SetDirectChild(existingInstance);
				}
				else
				{
					existingInstance.GetParentCollection(parent).Add(existingInstance);
				}
			}
			return existingInstance;
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x00093D98 File Offset: 0x00091F98
		public TmdlObject ToTMDL(MetadataObject metadataObject)
		{
			if (metadataObject == null)
			{
				throw new ArgumentNullException("metadataObject");
			}
			if (metadataObject.ObjectType != this.ObjectType)
			{
				throw new ArgumentException(TomSR.Exception_ComverterMismatchInstanceType(this.ObjectType.ToString("G"), metadataObject.ObjectType.ToString("G")), "metadataObject");
			}
			return TmdlSerializationHelper.SerializeMetadataObjectToTmdlObject(new TmdlSerializationConfiguration(TmdlSerializationHelper.DefaultFilter), metadataObject, null);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00093E0C File Offset: 0x0009200C
		public IPropertyConverter GetPropertyConverter(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ArgumentNullException("propertyName");
			}
			PropertyConverter propertyConverter;
			if (!this.propertyConevters.TryGetValue(propertyName, out propertyConverter))
			{
				propertyConverter = new PropertyConverter(this, propertyName);
				this.propertyConevters.Add(propertyName, propertyConverter);
			}
			return propertyConverter;
		}

		// Token: 0x04000411 RID: 1041
		private Dictionary<string, PropertyConverter> propertyConevters;
	}
}
