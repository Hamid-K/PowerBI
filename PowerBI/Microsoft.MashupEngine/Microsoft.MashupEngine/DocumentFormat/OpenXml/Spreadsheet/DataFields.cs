using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CB4 RID: 11444
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DataField))]
	internal class DataFields : OpenXmlCompositeElement
	{
		// Token: 0x170084A0 RID: 33952
		// (get) Token: 0x0601877B RID: 100219 RVA: 0x00341B8B File Offset: 0x0033FD8B
		public override string LocalName
		{
			get
			{
				return "dataFields";
			}
		}

		// Token: 0x170084A1 RID: 33953
		// (get) Token: 0x0601877C RID: 100220 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084A2 RID: 33954
		// (get) Token: 0x0601877D RID: 100221 RVA: 0x00341B92 File Offset: 0x0033FD92
		internal override int ElementTypeId
		{
			get
			{
				return 11424;
			}
		}

		// Token: 0x0601877E RID: 100222 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170084A3 RID: 33955
		// (get) Token: 0x0601877F RID: 100223 RVA: 0x00341B99 File Offset: 0x0033FD99
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataFields.attributeTagNames;
			}
		}

		// Token: 0x170084A4 RID: 33956
		// (get) Token: 0x06018780 RID: 100224 RVA: 0x00341BA0 File Offset: 0x0033FDA0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataFields.attributeNamespaceIds;
			}
		}

		// Token: 0x170084A5 RID: 33957
		// (get) Token: 0x06018781 RID: 100225 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018782 RID: 100226 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018783 RID: 100227 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataFields()
		{
		}

		// Token: 0x06018784 RID: 100228 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataFields(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018785 RID: 100229 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataFields(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018786 RID: 100230 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataFields(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018787 RID: 100231 RVA: 0x00341BA7 File Offset: 0x0033FDA7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "dataField" == name)
			{
				return new DataField();
			}
			return null;
		}

		// Token: 0x06018788 RID: 100232 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018789 RID: 100233 RVA: 0x00341BC2 File Offset: 0x0033FDC2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataFields>(deep);
		}

		// Token: 0x0601878A RID: 100234 RVA: 0x00341BCC File Offset: 0x0033FDCC
		// Note: this type is marked as 'beforefieldinit'.
		static DataFields()
		{
			byte[] array = new byte[1];
			DataFields.attributeNamespaceIds = array;
		}

		// Token: 0x0400A069 RID: 41065
		private const string tagName = "dataFields";

		// Token: 0x0400A06A RID: 41066
		private const byte tagNsId = 22;

		// Token: 0x0400A06B RID: 41067
		internal const int ElementTypeIdConst = 11424;

		// Token: 0x0400A06C RID: 41068
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A06D RID: 41069
		private static byte[] attributeNamespaceIds;
	}
}
