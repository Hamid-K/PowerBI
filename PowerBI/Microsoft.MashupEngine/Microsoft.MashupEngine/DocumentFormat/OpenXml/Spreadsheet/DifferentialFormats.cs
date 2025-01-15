using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C78 RID: 11384
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DifferentialFormat))]
	internal class DifferentialFormats : OpenXmlCompositeElement
	{
		// Token: 0x170082F9 RID: 33529
		// (get) Token: 0x0601837F RID: 99199 RVA: 0x002E67A3 File Offset: 0x002E49A3
		public override string LocalName
		{
			get
			{
				return "dxfs";
			}
		}

		// Token: 0x170082FA RID: 33530
		// (get) Token: 0x06018380 RID: 99200 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082FB RID: 33531
		// (get) Token: 0x06018381 RID: 99201 RVA: 0x0033F6CF File Offset: 0x0033D8CF
		internal override int ElementTypeId
		{
			get
			{
				return 11364;
			}
		}

		// Token: 0x06018382 RID: 99202 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170082FC RID: 33532
		// (get) Token: 0x06018383 RID: 99203 RVA: 0x0033F6D6 File Offset: 0x0033D8D6
		internal override string[] AttributeTagNames
		{
			get
			{
				return DifferentialFormats.attributeTagNames;
			}
		}

		// Token: 0x170082FD RID: 33533
		// (get) Token: 0x06018384 RID: 99204 RVA: 0x0033F6DD File Offset: 0x0033D8DD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DifferentialFormats.attributeNamespaceIds;
			}
		}

		// Token: 0x170082FE RID: 33534
		// (get) Token: 0x06018385 RID: 99205 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018386 RID: 99206 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018387 RID: 99207 RVA: 0x00293ECF File Offset: 0x002920CF
		public DifferentialFormats()
		{
		}

		// Token: 0x06018388 RID: 99208 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DifferentialFormats(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018389 RID: 99209 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DifferentialFormats(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601838A RID: 99210 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DifferentialFormats(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601838B RID: 99211 RVA: 0x002E67BF File Offset: 0x002E49BF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "dxf" == name)
			{
				return new DifferentialFormat();
			}
			return null;
		}

		// Token: 0x0601838C RID: 99212 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601838D RID: 99213 RVA: 0x0033F6E4 File Offset: 0x0033D8E4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DifferentialFormats>(deep);
		}

		// Token: 0x0601838E RID: 99214 RVA: 0x0033F6F0 File Offset: 0x0033D8F0
		// Note: this type is marked as 'beforefieldinit'.
		static DifferentialFormats()
		{
			byte[] array = new byte[1];
			DifferentialFormats.attributeNamespaceIds = array;
		}

		// Token: 0x04009F64 RID: 40804
		private const string tagName = "dxfs";

		// Token: 0x04009F65 RID: 40805
		private const byte tagNsId = 22;

		// Token: 0x04009F66 RID: 40806
		internal const int ElementTypeIdConst = 11364;

		// Token: 0x04009F67 RID: 40807
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009F68 RID: 40808
		private static byte[] attributeNamespaceIds;
	}
}
