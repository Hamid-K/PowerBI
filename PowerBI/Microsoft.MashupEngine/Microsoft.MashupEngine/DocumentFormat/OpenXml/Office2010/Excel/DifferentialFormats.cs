using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023E6 RID: 9190
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DifferentialFormat))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DifferentialFormats : OpenXmlCompositeElement
	{
		// Token: 0x17004DAA RID: 19882
		// (get) Token: 0x06010BA7 RID: 68519 RVA: 0x002E67A3 File Offset: 0x002E49A3
		public override string LocalName
		{
			get
			{
				return "dxfs";
			}
		}

		// Token: 0x17004DAB RID: 19883
		// (get) Token: 0x06010BA8 RID: 68520 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004DAC RID: 19884
		// (get) Token: 0x06010BA9 RID: 68521 RVA: 0x002E67AA File Offset: 0x002E49AA
		internal override int ElementTypeId
		{
			get
			{
				return 12916;
			}
		}

		// Token: 0x06010BAA RID: 68522 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004DAD RID: 19885
		// (get) Token: 0x06010BAB RID: 68523 RVA: 0x002E67B1 File Offset: 0x002E49B1
		internal override string[] AttributeTagNames
		{
			get
			{
				return DifferentialFormats.attributeTagNames;
			}
		}

		// Token: 0x17004DAE RID: 19886
		// (get) Token: 0x06010BAC RID: 68524 RVA: 0x002E67B8 File Offset: 0x002E49B8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DifferentialFormats.attributeNamespaceIds;
			}
		}

		// Token: 0x17004DAF RID: 19887
		// (get) Token: 0x06010BAD RID: 68525 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06010BAE RID: 68526 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06010BAF RID: 68527 RVA: 0x00293ECF File Offset: 0x002920CF
		public DifferentialFormats()
		{
		}

		// Token: 0x06010BB0 RID: 68528 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DifferentialFormats(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010BB1 RID: 68529 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DifferentialFormats(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010BB2 RID: 68530 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DifferentialFormats(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010BB3 RID: 68531 RVA: 0x002E67BF File Offset: 0x002E49BF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "dxf" == name)
			{
				return new DifferentialFormat();
			}
			return null;
		}

		// Token: 0x06010BB4 RID: 68532 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010BB5 RID: 68533 RVA: 0x002E67FA File Offset: 0x002E49FA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DifferentialFormats>(deep);
		}

		// Token: 0x06010BB6 RID: 68534 RVA: 0x002E6804 File Offset: 0x002E4A04
		// Note: this type is marked as 'beforefieldinit'.
		static DifferentialFormats()
		{
			byte[] array = new byte[1];
			DifferentialFormats.attributeNamespaceIds = array;
		}

		// Token: 0x04007617 RID: 30231
		private const string tagName = "dxfs";

		// Token: 0x04007618 RID: 30232
		private const byte tagNsId = 53;

		// Token: 0x04007619 RID: 30233
		internal const int ElementTypeIdConst = 12916;

		// Token: 0x0400761A RID: 30234
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400761B RID: 30235
		private static byte[] attributeNamespaceIds;
	}
}
