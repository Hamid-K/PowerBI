using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B66 RID: 11110
	[ChildElementInfo(typeof(GroupMember))]
	[GeneratedCode("DomGen", "2.0")]
	internal class GroupMembers : OpenXmlCompositeElement
	{
		// Token: 0x1700790A RID: 30986
		// (get) Token: 0x06016DB9 RID: 93625 RVA: 0x0032FE0A File Offset: 0x0032E00A
		public override string LocalName
		{
			get
			{
				return "groupMembers";
			}
		}

		// Token: 0x1700790B RID: 30987
		// (get) Token: 0x06016DBA RID: 93626 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700790C RID: 30988
		// (get) Token: 0x06016DBB RID: 93627 RVA: 0x0032FE11 File Offset: 0x0032E011
		internal override int ElementTypeId
		{
			get
			{
				return 11089;
			}
		}

		// Token: 0x06016DBC RID: 93628 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700790D RID: 30989
		// (get) Token: 0x06016DBD RID: 93629 RVA: 0x0032FE18 File Offset: 0x0032E018
		internal override string[] AttributeTagNames
		{
			get
			{
				return GroupMembers.attributeTagNames;
			}
		}

		// Token: 0x1700790E RID: 30990
		// (get) Token: 0x06016DBE RID: 93630 RVA: 0x0032FE1F File Offset: 0x0032E01F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GroupMembers.attributeNamespaceIds;
			}
		}

		// Token: 0x1700790F RID: 30991
		// (get) Token: 0x06016DBF RID: 93631 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016DC0 RID: 93632 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06016DC1 RID: 93633 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupMembers()
		{
		}

		// Token: 0x06016DC2 RID: 93634 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupMembers(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016DC3 RID: 93635 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupMembers(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016DC4 RID: 93636 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupMembers(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016DC5 RID: 93637 RVA: 0x0032FE26 File Offset: 0x0032E026
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "groupMember" == name)
			{
				return new GroupMember();
			}
			return null;
		}

		// Token: 0x06016DC6 RID: 93638 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016DC7 RID: 93639 RVA: 0x0032FE41 File Offset: 0x0032E041
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupMembers>(deep);
		}

		// Token: 0x06016DC8 RID: 93640 RVA: 0x0032FE4C File Offset: 0x0032E04C
		// Note: this type is marked as 'beforefieldinit'.
		static GroupMembers()
		{
			byte[] array = new byte[1];
			GroupMembers.attributeNamespaceIds = array;
		}

		// Token: 0x04009A32 RID: 39474
		private const string tagName = "groupMembers";

		// Token: 0x04009A33 RID: 39475
		private const byte tagNsId = 22;

		// Token: 0x04009A34 RID: 39476
		internal const int ElementTypeIdConst = 11089;

		// Token: 0x04009A35 RID: 39477
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009A36 RID: 39478
		private static byte[] attributeNamespaceIds;
	}
}
