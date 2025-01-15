using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.CustomXmlDataProperties
{
	// Token: 0x0200290A RID: 10506
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SchemaReferences))]
	internal class DataStoreItem : OpenXmlPartRootElement
	{
		// Token: 0x17006A3F RID: 27199
		// (get) Token: 0x06014C4D RID: 85069 RVA: 0x002E6DDB File Offset: 0x002E4FDB
		public override string LocalName
		{
			get
			{
				return "datastoreItem";
			}
		}

		// Token: 0x17006A40 RID: 27200
		// (get) Token: 0x06014C4E RID: 85070 RVA: 0x002435AE File Offset: 0x002417AE
		internal override byte NamespaceId
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x17006A41 RID: 27201
		// (get) Token: 0x06014C4F RID: 85071 RVA: 0x00316E99 File Offset: 0x00315099
		internal override int ElementTypeId
		{
			get
			{
				return 10834;
			}
		}

		// Token: 0x06014C50 RID: 85072 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006A42 RID: 27202
		// (get) Token: 0x06014C51 RID: 85073 RVA: 0x00316EA0 File Offset: 0x003150A0
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataStoreItem.attributeTagNames;
			}
		}

		// Token: 0x17006A43 RID: 27203
		// (get) Token: 0x06014C52 RID: 85074 RVA: 0x00316EA7 File Offset: 0x003150A7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataStoreItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17006A44 RID: 27204
		// (get) Token: 0x06014C53 RID: 85075 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06014C54 RID: 85076 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(20, "itemID")]
		public StringValue ItemId
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

		// Token: 0x06014C55 RID: 85077 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal DataStoreItem(CustomXmlPropertiesPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06014C56 RID: 85078 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(CustomXmlPropertiesPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006A45 RID: 27205
		// (get) Token: 0x06014C57 RID: 85079 RVA: 0x00316EAE File Offset: 0x003150AE
		// (set) Token: 0x06014C58 RID: 85080 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public CustomXmlPropertiesPart CustomXmlPropertiesPart
		{
			get
			{
				return base.OpenXmlPart as CustomXmlPropertiesPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06014C59 RID: 85081 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public DataStoreItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014C5A RID: 85082 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public DataStoreItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014C5B RID: 85083 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public DataStoreItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014C5C RID: 85084 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public DataStoreItem()
		{
		}

		// Token: 0x06014C5D RID: 85085 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(CustomXmlPropertiesPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06014C5E RID: 85086 RVA: 0x00316EBB File Offset: 0x003150BB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (20 == namespaceId && "schemaRefs" == name)
			{
				return new SchemaReferences();
			}
			return null;
		}

		// Token: 0x17006A46 RID: 27206
		// (get) Token: 0x06014C5F RID: 85087 RVA: 0x00316ED6 File Offset: 0x003150D6
		internal override string[] ElementTagNames
		{
			get
			{
				return DataStoreItem.eleTagNames;
			}
		}

		// Token: 0x17006A47 RID: 27207
		// (get) Token: 0x06014C60 RID: 85088 RVA: 0x00316EDD File Offset: 0x003150DD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DataStoreItem.eleNamespaceIds;
			}
		}

		// Token: 0x17006A48 RID: 27208
		// (get) Token: 0x06014C61 RID: 85089 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006A49 RID: 27209
		// (get) Token: 0x06014C62 RID: 85090 RVA: 0x00316EE4 File Offset: 0x003150E4
		// (set) Token: 0x06014C63 RID: 85091 RVA: 0x00316EED File Offset: 0x003150ED
		public SchemaReferences SchemaReferences
		{
			get
			{
				return base.GetElement<SchemaReferences>(0);
			}
			set
			{
				base.SetElement<SchemaReferences>(0, value);
			}
		}

		// Token: 0x06014C64 RID: 85092 RVA: 0x00316EF7 File Offset: 0x003150F7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (20 == namespaceId && "itemID" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014C65 RID: 85093 RVA: 0x00316F19 File Offset: 0x00315119
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataStoreItem>(deep);
		}

		// Token: 0x04008FC4 RID: 36804
		private const string tagName = "datastoreItem";

		// Token: 0x04008FC5 RID: 36805
		private const byte tagNsId = 20;

		// Token: 0x04008FC6 RID: 36806
		internal const int ElementTypeIdConst = 10834;

		// Token: 0x04008FC7 RID: 36807
		private static string[] attributeTagNames = new string[] { "itemID" };

		// Token: 0x04008FC8 RID: 36808
		private static byte[] attributeNamespaceIds = new byte[] { 20 };

		// Token: 0x04008FC9 RID: 36809
		private static readonly string[] eleTagNames = new string[] { "schemaRefs" };

		// Token: 0x04008FCA RID: 36810
		private static readonly byte[] eleNamespaceIds = new byte[] { 20 };
	}
}
