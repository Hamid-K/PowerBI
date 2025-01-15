using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002423 RID: 9251
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CalculatedMember))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CalculatedMembers : OpenXmlCompositeElement
	{
		// Token: 0x17004F6C RID: 20332
		// (get) Token: 0x06010F9A RID: 69530 RVA: 0x002E93C7 File Offset: 0x002E75C7
		public override string LocalName
		{
			get
			{
				return "calculatedMembers";
			}
		}

		// Token: 0x17004F6D RID: 20333
		// (get) Token: 0x06010F9B RID: 69531 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F6E RID: 20334
		// (get) Token: 0x06010F9C RID: 69532 RVA: 0x002E93CE File Offset: 0x002E75CE
		internal override int ElementTypeId
		{
			get
			{
				return 12975;
			}
		}

		// Token: 0x06010F9D RID: 69533 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004F6F RID: 20335
		// (get) Token: 0x06010F9E RID: 69534 RVA: 0x002E93D5 File Offset: 0x002E75D5
		internal override string[] AttributeTagNames
		{
			get
			{
				return CalculatedMembers.attributeTagNames;
			}
		}

		// Token: 0x17004F70 RID: 20336
		// (get) Token: 0x06010F9F RID: 69535 RVA: 0x002E93DC File Offset: 0x002E75DC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CalculatedMembers.attributeNamespaceIds;
			}
		}

		// Token: 0x17004F71 RID: 20337
		// (get) Token: 0x06010FA0 RID: 69536 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06010FA1 RID: 69537 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06010FA2 RID: 69538 RVA: 0x00293ECF File Offset: 0x002920CF
		public CalculatedMembers()
		{
		}

		// Token: 0x06010FA3 RID: 69539 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CalculatedMembers(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010FA4 RID: 69540 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CalculatedMembers(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010FA5 RID: 69541 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CalculatedMembers(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010FA6 RID: 69542 RVA: 0x002E93E3 File Offset: 0x002E75E3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "calculatedMember" == name)
			{
				return new CalculatedMember();
			}
			return null;
		}

		// Token: 0x06010FA7 RID: 69543 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010FA8 RID: 69544 RVA: 0x002E93FE File Offset: 0x002E75FE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CalculatedMembers>(deep);
		}

		// Token: 0x06010FA9 RID: 69545 RVA: 0x002E9408 File Offset: 0x002E7608
		// Note: this type is marked as 'beforefieldinit'.
		static CalculatedMembers()
		{
			byte[] array = new byte[1];
			CalculatedMembers.attributeNamespaceIds = array;
		}

		// Token: 0x04007720 RID: 30496
		private const string tagName = "calculatedMembers";

		// Token: 0x04007721 RID: 30497
		private const byte tagNsId = 53;

		// Token: 0x04007722 RID: 30498
		internal const int ElementTypeIdConst = 12975;

		// Token: 0x04007723 RID: 30499
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04007724 RID: 30500
		private static byte[] attributeNamespaceIds;
	}
}
