using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023F0 RID: 9200
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class DatastoreItem : OpenXmlPartRootElement
	{
		// Token: 0x17004DF1 RID: 19953
		// (get) Token: 0x06010C40 RID: 68672 RVA: 0x002E6DDB File Offset: 0x002E4FDB
		public override string LocalName
		{
			get
			{
				return "datastoreItem";
			}
		}

		// Token: 0x17004DF2 RID: 19954
		// (get) Token: 0x06010C41 RID: 68673 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004DF3 RID: 19955
		// (get) Token: 0x06010C42 RID: 68674 RVA: 0x002E6DE2 File Offset: 0x002E4FE2
		internal override int ElementTypeId
		{
			get
			{
				return 12926;
			}
		}

		// Token: 0x06010C43 RID: 68675 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004DF4 RID: 19956
		// (get) Token: 0x06010C44 RID: 68676 RVA: 0x002E6DE9 File Offset: 0x002E4FE9
		internal override string[] AttributeTagNames
		{
			get
			{
				return DatastoreItem.attributeTagNames;
			}
		}

		// Token: 0x17004DF5 RID: 19957
		// (get) Token: 0x06010C45 RID: 68677 RVA: 0x002E6DF0 File Offset: 0x002E4FF0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DatastoreItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17004DF6 RID: 19958
		// (get) Token: 0x06010C46 RID: 68678 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010C47 RID: 68679 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06010C48 RID: 68680 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal DatastoreItem(CustomDataPropertiesPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06010C49 RID: 68681 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(CustomDataPropertiesPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17004DF7 RID: 19959
		// (get) Token: 0x06010C4A RID: 68682 RVA: 0x002E6DF7 File Offset: 0x002E4FF7
		// (set) Token: 0x06010C4B RID: 68683 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public CustomDataPropertiesPart CustomDataPropertiesPart
		{
			get
			{
				return base.OpenXmlPart as CustomDataPropertiesPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06010C4C RID: 68684 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public DatastoreItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010C4D RID: 68685 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public DatastoreItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010C4E RID: 68686 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public DatastoreItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010C4F RID: 68687 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public DatastoreItem()
		{
		}

		// Token: 0x06010C50 RID: 68688 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(CustomDataPropertiesPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06010C51 RID: 68689 RVA: 0x002E6E04 File Offset: 0x002E5004
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17004DF8 RID: 19960
		// (get) Token: 0x06010C52 RID: 68690 RVA: 0x002E6E1F File Offset: 0x002E501F
		internal override string[] ElementTagNames
		{
			get
			{
				return DatastoreItem.eleTagNames;
			}
		}

		// Token: 0x17004DF9 RID: 19961
		// (get) Token: 0x06010C53 RID: 68691 RVA: 0x002E6E26 File Offset: 0x002E5026
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DatastoreItem.eleNamespaceIds;
			}
		}

		// Token: 0x17004DFA RID: 19962
		// (get) Token: 0x06010C54 RID: 68692 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004DFB RID: 19963
		// (get) Token: 0x06010C55 RID: 68693 RVA: 0x002E6E2D File Offset: 0x002E502D
		// (set) Token: 0x06010C56 RID: 68694 RVA: 0x002E6E36 File Offset: 0x002E5036
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06010C57 RID: 68695 RVA: 0x002BFD13 File Offset: 0x002BDF13
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010C58 RID: 68696 RVA: 0x002E6E40 File Offset: 0x002E5040
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DatastoreItem>(deep);
		}

		// Token: 0x06010C59 RID: 68697 RVA: 0x002E6E4C File Offset: 0x002E504C
		// Note: this type is marked as 'beforefieldinit'.
		static DatastoreItem()
		{
			byte[] array = new byte[1];
			DatastoreItem.attributeNamespaceIds = array;
			DatastoreItem.eleTagNames = new string[] { "extLst" };
			DatastoreItem.eleNamespaceIds = new byte[] { 53 };
		}

		// Token: 0x04007649 RID: 30281
		private const string tagName = "datastoreItem";

		// Token: 0x0400764A RID: 30282
		private const byte tagNsId = 53;

		// Token: 0x0400764B RID: 30283
		internal const int ElementTypeIdConst = 12926;

		// Token: 0x0400764C RID: 30284
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x0400764D RID: 30285
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400764E RID: 30286
		private static readonly string[] eleTagNames;

		// Token: 0x0400764F RID: 30287
		private static readonly byte[] eleNamespaceIds;
	}
}
