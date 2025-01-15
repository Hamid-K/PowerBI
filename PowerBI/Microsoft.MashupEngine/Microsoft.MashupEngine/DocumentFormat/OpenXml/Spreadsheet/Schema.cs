using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B43 RID: 11075
	[GeneratedCode("DomGen", "2.0")]
	internal class Schema : OpenXmlCompositeElement
	{
		// Token: 0x170077B0 RID: 30640
		// (get) Token: 0x06016ABB RID: 92859 RVA: 0x0032DAE4 File Offset: 0x0032BCE4
		public override string LocalName
		{
			get
			{
				return "Schema";
			}
		}

		// Token: 0x170077B1 RID: 30641
		// (get) Token: 0x06016ABC RID: 92860 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170077B2 RID: 30642
		// (get) Token: 0x06016ABD RID: 92861 RVA: 0x0032DAEB File Offset: 0x0032BCEB
		internal override int ElementTypeId
		{
			get
			{
				return 11058;
			}
		}

		// Token: 0x06016ABE RID: 92862 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170077B3 RID: 30643
		// (get) Token: 0x06016ABF RID: 92863 RVA: 0x0032DAF2 File Offset: 0x0032BCF2
		internal override string[] AttributeTagNames
		{
			get
			{
				return Schema.attributeTagNames;
			}
		}

		// Token: 0x170077B4 RID: 30644
		// (get) Token: 0x06016AC0 RID: 92864 RVA: 0x0032DAF9 File Offset: 0x0032BCF9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Schema.attributeNamespaceIds;
			}
		}

		// Token: 0x170077B5 RID: 30645
		// (get) Token: 0x06016AC1 RID: 92865 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016AC2 RID: 92866 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ID")]
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

		// Token: 0x170077B6 RID: 30646
		// (get) Token: 0x06016AC3 RID: 92867 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06016AC4 RID: 92868 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "SchemaRef")]
		public StringValue SchemaReference
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170077B7 RID: 30647
		// (get) Token: 0x06016AC5 RID: 92869 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06016AC6 RID: 92870 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "Namespace")]
		public StringValue Namespace
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06016AC7 RID: 92871 RVA: 0x00293ECF File Offset: 0x002920CF
		public Schema()
		{
		}

		// Token: 0x06016AC8 RID: 92872 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Schema(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016AC9 RID: 92873 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Schema(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016ACA RID: 92874 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Schema(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016ACB RID: 92875 RVA: 0x000020FA File Offset: 0x000002FA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x06016ACC RID: 92876 RVA: 0x0032DB00 File Offset: 0x0032BD00
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ID" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "SchemaRef" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "Namespace" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016ACD RID: 92877 RVA: 0x0032DB57 File Offset: 0x0032BD57
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Schema>(deep);
		}

		// Token: 0x06016ACE RID: 92878 RVA: 0x0032DB60 File Offset: 0x0032BD60
		// Note: this type is marked as 'beforefieldinit'.
		static Schema()
		{
			byte[] array = new byte[3];
			Schema.attributeNamespaceIds = array;
		}

		// Token: 0x04009989 RID: 39305
		private const string tagName = "Schema";

		// Token: 0x0400998A RID: 39306
		private const byte tagNsId = 22;

		// Token: 0x0400998B RID: 39307
		internal const int ElementTypeIdConst = 11058;

		// Token: 0x0400998C RID: 39308
		private static string[] attributeTagNames = new string[] { "ID", "SchemaRef", "Namespace" };

		// Token: 0x0400998D RID: 39309
		private static byte[] attributeNamespaceIds;
	}
}
