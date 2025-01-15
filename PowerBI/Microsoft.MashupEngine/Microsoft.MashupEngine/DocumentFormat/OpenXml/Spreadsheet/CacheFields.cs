using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CC9 RID: 11465
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CacheField))]
	internal class CacheFields : OpenXmlCompositeElement
	{
		// Token: 0x17008531 RID: 34097
		// (get) Token: 0x060188E3 RID: 100579 RVA: 0x003428FE File Offset: 0x00340AFE
		public override string LocalName
		{
			get
			{
				return "cacheFields";
			}
		}

		// Token: 0x17008532 RID: 34098
		// (get) Token: 0x060188E4 RID: 100580 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008533 RID: 34099
		// (get) Token: 0x060188E5 RID: 100581 RVA: 0x00342905 File Offset: 0x00340B05
		internal override int ElementTypeId
		{
			get
			{
				return 11446;
			}
		}

		// Token: 0x060188E6 RID: 100582 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008534 RID: 34100
		// (get) Token: 0x060188E7 RID: 100583 RVA: 0x0034290C File Offset: 0x00340B0C
		internal override string[] AttributeTagNames
		{
			get
			{
				return CacheFields.attributeTagNames;
			}
		}

		// Token: 0x17008535 RID: 34101
		// (get) Token: 0x060188E8 RID: 100584 RVA: 0x00342913 File Offset: 0x00340B13
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CacheFields.attributeNamespaceIds;
			}
		}

		// Token: 0x17008536 RID: 34102
		// (get) Token: 0x060188E9 RID: 100585 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060188EA RID: 100586 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060188EB RID: 100587 RVA: 0x00293ECF File Offset: 0x002920CF
		public CacheFields()
		{
		}

		// Token: 0x060188EC RID: 100588 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CacheFields(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060188ED RID: 100589 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CacheFields(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060188EE RID: 100590 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CacheFields(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060188EF RID: 100591 RVA: 0x0034291A File Offset: 0x00340B1A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "cacheField" == name)
			{
				return new CacheField();
			}
			return null;
		}

		// Token: 0x060188F0 RID: 100592 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060188F1 RID: 100593 RVA: 0x00342935 File Offset: 0x00340B35
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CacheFields>(deep);
		}

		// Token: 0x060188F2 RID: 100594 RVA: 0x00342940 File Offset: 0x00340B40
		// Note: this type is marked as 'beforefieldinit'.
		static CacheFields()
		{
			byte[] array = new byte[1];
			CacheFields.attributeNamespaceIds = array;
		}

		// Token: 0x0400A0CC RID: 41164
		private const string tagName = "cacheFields";

		// Token: 0x0400A0CD RID: 41165
		private const byte tagNsId = 22;

		// Token: 0x0400A0CE RID: 41166
		internal const int ElementTypeIdConst = 11446;

		// Token: 0x0400A0CF RID: 41167
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A0D0 RID: 41168
		private static byte[] attributeNamespaceIds;
	}
}
