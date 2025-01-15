using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CA6 RID: 11430
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MemberProperty))]
	internal class MemberProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700844E RID: 33870
		// (get) Token: 0x060186A5 RID: 100005 RVA: 0x003415A6 File Offset: 0x0033F7A6
		public override string LocalName
		{
			get
			{
				return "mps";
			}
		}

		// Token: 0x1700844F RID: 33871
		// (get) Token: 0x060186A6 RID: 100006 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008450 RID: 33872
		// (get) Token: 0x060186A7 RID: 100007 RVA: 0x003415AD File Offset: 0x0033F7AD
		internal override int ElementTypeId
		{
			get
			{
				return 11410;
			}
		}

		// Token: 0x060186A8 RID: 100008 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008451 RID: 33873
		// (get) Token: 0x060186A9 RID: 100009 RVA: 0x003415B4 File Offset: 0x0033F7B4
		internal override string[] AttributeTagNames
		{
			get
			{
				return MemberProperties.attributeTagNames;
			}
		}

		// Token: 0x17008452 RID: 33874
		// (get) Token: 0x060186AA RID: 100010 RVA: 0x003415BB File Offset: 0x0033F7BB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MemberProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17008453 RID: 33875
		// (get) Token: 0x060186AB RID: 100011 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060186AC RID: 100012 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x060186AD RID: 100013 RVA: 0x00293ECF File Offset: 0x002920CF
		public MemberProperties()
		{
		}

		// Token: 0x060186AE RID: 100014 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MemberProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060186AF RID: 100015 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MemberProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060186B0 RID: 100016 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MemberProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060186B1 RID: 100017 RVA: 0x003415C2 File Offset: 0x0033F7C2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "mp" == name)
			{
				return new MemberProperty();
			}
			return null;
		}

		// Token: 0x060186B2 RID: 100018 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060186B3 RID: 100019 RVA: 0x003415DD File Offset: 0x0033F7DD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MemberProperties>(deep);
		}

		// Token: 0x060186B4 RID: 100020 RVA: 0x003415E8 File Offset: 0x0033F7E8
		// Note: this type is marked as 'beforefieldinit'.
		static MemberProperties()
		{
			byte[] array = new byte[1];
			MemberProperties.attributeNamespaceIds = array;
		}

		// Token: 0x0400A029 RID: 41001
		private const string tagName = "mps";

		// Token: 0x0400A02A RID: 41002
		private const byte tagNsId = 22;

		// Token: 0x0400A02B RID: 41003
		internal const int ElementTypeIdConst = 11410;

		// Token: 0x0400A02C RID: 41004
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A02D RID: 41005
		private static byte[] attributeNamespaceIds;
	}
}
