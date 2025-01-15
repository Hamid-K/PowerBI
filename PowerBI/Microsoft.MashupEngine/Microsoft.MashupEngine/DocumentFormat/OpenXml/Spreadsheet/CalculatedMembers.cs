using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CCE RID: 11470
	[ChildElementInfo(typeof(CalculatedMember))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CalculatedMembers : OpenXmlCompositeElement
	{
		// Token: 0x17008554 RID: 34132
		// (get) Token: 0x0601893B RID: 100667 RVA: 0x002E93C7 File Offset: 0x002E75C7
		public override string LocalName
		{
			get
			{
				return "calculatedMembers";
			}
		}

		// Token: 0x17008555 RID: 34133
		// (get) Token: 0x0601893C RID: 100668 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008556 RID: 34134
		// (get) Token: 0x0601893D RID: 100669 RVA: 0x00342C13 File Offset: 0x00340E13
		internal override int ElementTypeId
		{
			get
			{
				return 11451;
			}
		}

		// Token: 0x0601893E RID: 100670 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008557 RID: 34135
		// (get) Token: 0x0601893F RID: 100671 RVA: 0x00342C1A File Offset: 0x00340E1A
		internal override string[] AttributeTagNames
		{
			get
			{
				return CalculatedMembers.attributeTagNames;
			}
		}

		// Token: 0x17008558 RID: 34136
		// (get) Token: 0x06018940 RID: 100672 RVA: 0x00342C21 File Offset: 0x00340E21
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CalculatedMembers.attributeNamespaceIds;
			}
		}

		// Token: 0x17008559 RID: 34137
		// (get) Token: 0x06018941 RID: 100673 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018942 RID: 100674 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018943 RID: 100675 RVA: 0x00293ECF File Offset: 0x002920CF
		public CalculatedMembers()
		{
		}

		// Token: 0x06018944 RID: 100676 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CalculatedMembers(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018945 RID: 100677 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CalculatedMembers(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018946 RID: 100678 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CalculatedMembers(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018947 RID: 100679 RVA: 0x002E93E3 File Offset: 0x002E75E3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "calculatedMember" == name)
			{
				return new CalculatedMember();
			}
			return null;
		}

		// Token: 0x06018948 RID: 100680 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018949 RID: 100681 RVA: 0x00342C28 File Offset: 0x00340E28
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CalculatedMembers>(deep);
		}

		// Token: 0x0601894A RID: 100682 RVA: 0x00342C34 File Offset: 0x00340E34
		// Note: this type is marked as 'beforefieldinit'.
		static CalculatedMembers()
		{
			byte[] array = new byte[1];
			CalculatedMembers.attributeNamespaceIds = array;
		}

		// Token: 0x0400A0E5 RID: 41189
		private const string tagName = "calculatedMembers";

		// Token: 0x0400A0E6 RID: 41190
		private const byte tagNsId = 22;

		// Token: 0x0400A0E7 RID: 41191
		internal const int ElementTypeIdConst = 11451;

		// Token: 0x0400A0E8 RID: 41192
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A0E9 RID: 41193
		private static byte[] attributeNamespaceIds;
	}
}
