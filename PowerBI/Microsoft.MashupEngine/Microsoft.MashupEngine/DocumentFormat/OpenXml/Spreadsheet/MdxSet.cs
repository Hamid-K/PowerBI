using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C00 RID: 11264
	[ChildElementInfo(typeof(NameIndex))]
	[GeneratedCode("DomGen", "2.0")]
	internal class MdxSet : OpenXmlCompositeElement
	{
		// Token: 0x17007F3D RID: 32573
		// (get) Token: 0x06017AFA RID: 97018 RVA: 0x00339EDA File Offset: 0x003380DA
		public override string LocalName
		{
			get
			{
				return "ms";
			}
		}

		// Token: 0x17007F3E RID: 32574
		// (get) Token: 0x06017AFB RID: 97019 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F3F RID: 32575
		// (get) Token: 0x06017AFC RID: 97020 RVA: 0x00339EE1 File Offset: 0x003380E1
		internal override int ElementTypeId
		{
			get
			{
				return 11243;
			}
		}

		// Token: 0x06017AFD RID: 97021 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007F40 RID: 32576
		// (get) Token: 0x06017AFE RID: 97022 RVA: 0x00339EE8 File Offset: 0x003380E8
		internal override string[] AttributeTagNames
		{
			get
			{
				return MdxSet.attributeTagNames;
			}
		}

		// Token: 0x17007F41 RID: 32577
		// (get) Token: 0x06017AFF RID: 97023 RVA: 0x00339EEF File Offset: 0x003380EF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MdxSet.attributeNamespaceIds;
			}
		}

		// Token: 0x17007F42 RID: 32578
		// (get) Token: 0x06017B00 RID: 97024 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017B01 RID: 97025 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ns")]
		public UInt32Value SetDefinitionIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007F43 RID: 32579
		// (get) Token: 0x06017B02 RID: 97026 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017B03 RID: 97027 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "c")]
		public UInt32Value MemberIndexCount
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007F44 RID: 32580
		// (get) Token: 0x06017B04 RID: 97028 RVA: 0x00339EF6 File Offset: 0x003380F6
		// (set) Token: 0x06017B05 RID: 97029 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "o")]
		public EnumValue<MdxSetOrderValues> SortingOrder
		{
			get
			{
				return (EnumValue<MdxSetOrderValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06017B06 RID: 97030 RVA: 0x00293ECF File Offset: 0x002920CF
		public MdxSet()
		{
		}

		// Token: 0x06017B07 RID: 97031 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MdxSet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017B08 RID: 97032 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MdxSet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017B09 RID: 97033 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MdxSet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017B0A RID: 97034 RVA: 0x00339D47 File Offset: 0x00337F47
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "n" == name)
			{
				return new NameIndex();
			}
			return null;
		}

		// Token: 0x06017B0B RID: 97035 RVA: 0x00339F08 File Offset: 0x00338108
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ns" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "c" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "o" == name)
			{
				return new EnumValue<MdxSetOrderValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017B0C RID: 97036 RVA: 0x00339F5F File Offset: 0x0033815F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MdxSet>(deep);
		}

		// Token: 0x06017B0D RID: 97037 RVA: 0x00339F68 File Offset: 0x00338168
		// Note: this type is marked as 'beforefieldinit'.
		static MdxSet()
		{
			byte[] array = new byte[3];
			MdxSet.attributeNamespaceIds = array;
		}

		// Token: 0x04009D27 RID: 40231
		private const string tagName = "ms";

		// Token: 0x04009D28 RID: 40232
		private const byte tagNsId = 22;

		// Token: 0x04009D29 RID: 40233
		internal const int ElementTypeIdConst = 11243;

		// Token: 0x04009D2A RID: 40234
		private static string[] attributeTagNames = new string[] { "ns", "c", "o" };

		// Token: 0x04009D2B RID: 40235
		private static byte[] attributeNamespaceIds;
	}
}
