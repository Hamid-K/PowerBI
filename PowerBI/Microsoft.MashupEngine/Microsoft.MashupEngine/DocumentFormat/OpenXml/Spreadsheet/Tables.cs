using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B47 RID: 11079
	[ChildElementInfo(typeof(MissingTable))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CharacterValue))]
	[ChildElementInfo(typeof(FieldItem))]
	internal class Tables : OpenXmlCompositeElement
	{
		// Token: 0x170077F6 RID: 30710
		// (get) Token: 0x06016B51 RID: 93009 RVA: 0x0032E2CF File Offset: 0x0032C4CF
		public override string LocalName
		{
			get
			{
				return "tables";
			}
		}

		// Token: 0x170077F7 RID: 30711
		// (get) Token: 0x06016B52 RID: 93010 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170077F8 RID: 30712
		// (get) Token: 0x06016B53 RID: 93011 RVA: 0x0032E2D6 File Offset: 0x0032C4D6
		internal override int ElementTypeId
		{
			get
			{
				return 11062;
			}
		}

		// Token: 0x06016B54 RID: 93012 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170077F9 RID: 30713
		// (get) Token: 0x06016B55 RID: 93013 RVA: 0x0032E2DD File Offset: 0x0032C4DD
		internal override string[] AttributeTagNames
		{
			get
			{
				return Tables.attributeTagNames;
			}
		}

		// Token: 0x170077FA RID: 30714
		// (get) Token: 0x06016B56 RID: 93014 RVA: 0x0032E2E4 File Offset: 0x0032C4E4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Tables.attributeNamespaceIds;
			}
		}

		// Token: 0x170077FB RID: 30715
		// (get) Token: 0x06016B57 RID: 93015 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016B58 RID: 93016 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06016B59 RID: 93017 RVA: 0x00293ECF File Offset: 0x002920CF
		public Tables()
		{
		}

		// Token: 0x06016B5A RID: 93018 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Tables(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016B5B RID: 93019 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Tables(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016B5C RID: 93020 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Tables(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016B5D RID: 93021 RVA: 0x0032E2EC File Offset: 0x0032C4EC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "m" == name)
			{
				return new MissingTable();
			}
			if (22 == namespaceId && "s" == name)
			{
				return new CharacterValue();
			}
			if (22 == namespaceId && "x" == name)
			{
				return new FieldItem();
			}
			return null;
		}

		// Token: 0x06016B5E RID: 93022 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016B5F RID: 93023 RVA: 0x0032E342 File Offset: 0x0032C542
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Tables>(deep);
		}

		// Token: 0x06016B60 RID: 93024 RVA: 0x0032E34C File Offset: 0x0032C54C
		// Note: this type is marked as 'beforefieldinit'.
		static Tables()
		{
			byte[] array = new byte[1];
			Tables.attributeNamespaceIds = array;
		}

		// Token: 0x040099A1 RID: 39329
		private const string tagName = "tables";

		// Token: 0x040099A2 RID: 39330
		private const byte tagNsId = 22;

		// Token: 0x040099A3 RID: 39331
		internal const int ElementTypeIdConst = 11062;

		// Token: 0x040099A4 RID: 39332
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x040099A5 RID: 39333
		private static byte[] attributeNamespaceIds;
	}
}
