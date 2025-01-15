using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CBF RID: 11455
	[ChildElementInfo(typeof(FieldUsage))]
	[GeneratedCode("DomGen", "2.0")]
	internal class FieldsUsage : OpenXmlCompositeElement
	{
		// Token: 0x170084E1 RID: 34017
		// (get) Token: 0x06018825 RID: 100389 RVA: 0x00342071 File Offset: 0x00340271
		public override string LocalName
		{
			get
			{
				return "fieldsUsage";
			}
		}

		// Token: 0x170084E2 RID: 34018
		// (get) Token: 0x06018826 RID: 100390 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084E3 RID: 34019
		// (get) Token: 0x06018827 RID: 100391 RVA: 0x00342078 File Offset: 0x00340278
		internal override int ElementTypeId
		{
			get
			{
				return 11435;
			}
		}

		// Token: 0x06018828 RID: 100392 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170084E4 RID: 34020
		// (get) Token: 0x06018829 RID: 100393 RVA: 0x0034207F File Offset: 0x0034027F
		internal override string[] AttributeTagNames
		{
			get
			{
				return FieldsUsage.attributeTagNames;
			}
		}

		// Token: 0x170084E5 RID: 34021
		// (get) Token: 0x0601882A RID: 100394 RVA: 0x00342086 File Offset: 0x00340286
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FieldsUsage.attributeNamespaceIds;
			}
		}

		// Token: 0x170084E6 RID: 34022
		// (get) Token: 0x0601882B RID: 100395 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601882C RID: 100396 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601882D RID: 100397 RVA: 0x00293ECF File Offset: 0x002920CF
		public FieldsUsage()
		{
		}

		// Token: 0x0601882E RID: 100398 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FieldsUsage(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601882F RID: 100399 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FieldsUsage(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018830 RID: 100400 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FieldsUsage(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018831 RID: 100401 RVA: 0x0034208D File Offset: 0x0034028D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "fieldUsage" == name)
			{
				return new FieldUsage();
			}
			return null;
		}

		// Token: 0x06018832 RID: 100402 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018833 RID: 100403 RVA: 0x003420A8 File Offset: 0x003402A8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FieldsUsage>(deep);
		}

		// Token: 0x06018834 RID: 100404 RVA: 0x003420B4 File Offset: 0x003402B4
		// Note: this type is marked as 'beforefieldinit'.
		static FieldsUsage()
		{
			byte[] array = new byte[1];
			FieldsUsage.attributeNamespaceIds = array;
		}

		// Token: 0x0400A09C RID: 41116
		private const string tagName = "fieldsUsage";

		// Token: 0x0400A09D RID: 41117
		private const byte tagNsId = 22;

		// Token: 0x0400A09E RID: 41118
		internal const int ElementTypeIdConst = 11435;

		// Token: 0x0400A09F RID: 41119
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A0A0 RID: 41120
		private static byte[] attributeNamespaceIds;
	}
}
