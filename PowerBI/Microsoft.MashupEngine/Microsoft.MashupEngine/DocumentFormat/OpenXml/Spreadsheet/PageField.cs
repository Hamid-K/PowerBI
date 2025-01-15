using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B8B RID: 11147
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class PageField : OpenXmlCompositeElement
	{
		// Token: 0x17007AB5 RID: 31413
		// (get) Token: 0x06017148 RID: 94536 RVA: 0x003328C3 File Offset: 0x00330AC3
		public override string LocalName
		{
			get
			{
				return "pageField";
			}
		}

		// Token: 0x17007AB6 RID: 31414
		// (get) Token: 0x06017149 RID: 94537 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007AB7 RID: 31415
		// (get) Token: 0x0601714A RID: 94538 RVA: 0x003328CA File Offset: 0x00330ACA
		internal override int ElementTypeId
		{
			get
			{
				return 11125;
			}
		}

		// Token: 0x0601714B RID: 94539 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007AB8 RID: 31416
		// (get) Token: 0x0601714C RID: 94540 RVA: 0x003328D1 File Offset: 0x00330AD1
		internal override string[] AttributeTagNames
		{
			get
			{
				return PageField.attributeTagNames;
			}
		}

		// Token: 0x17007AB9 RID: 31417
		// (get) Token: 0x0601714D RID: 94541 RVA: 0x003328D8 File Offset: 0x00330AD8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PageField.attributeNamespaceIds;
			}
		}

		// Token: 0x17007ABA RID: 31418
		// (get) Token: 0x0601714E RID: 94542 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601714F RID: 94543 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "fld")]
		public Int32Value Field
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007ABB RID: 31419
		// (get) Token: 0x06017150 RID: 94544 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017151 RID: 94545 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "item")]
		public UInt32Value Item
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007ABC RID: 31420
		// (get) Token: 0x06017152 RID: 94546 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06017153 RID: 94547 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "hier")]
		public Int32Value Hierarchy
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007ABD RID: 31421
		// (get) Token: 0x06017154 RID: 94548 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06017155 RID: 94549 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007ABE RID: 31422
		// (get) Token: 0x06017156 RID: 94550 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06017157 RID: 94551 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "cap")]
		public StringValue Caption
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06017158 RID: 94552 RVA: 0x00293ECF File Offset: 0x002920CF
		public PageField()
		{
		}

		// Token: 0x06017159 RID: 94553 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PageField(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601715A RID: 94554 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PageField(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601715B RID: 94555 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PageField(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601715C RID: 94556 RVA: 0x003328DF File Offset: 0x00330ADF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007ABF RID: 31423
		// (get) Token: 0x0601715D RID: 94557 RVA: 0x003328FA File Offset: 0x00330AFA
		internal override string[] ElementTagNames
		{
			get
			{
				return PageField.eleTagNames;
			}
		}

		// Token: 0x17007AC0 RID: 31424
		// (get) Token: 0x0601715E RID: 94558 RVA: 0x00332901 File Offset: 0x00330B01
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PageField.eleNamespaceIds;
			}
		}

		// Token: 0x17007AC1 RID: 31425
		// (get) Token: 0x0601715F RID: 94559 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007AC2 RID: 31426
		// (get) Token: 0x06017160 RID: 94560 RVA: 0x00332908 File Offset: 0x00330B08
		// (set) Token: 0x06017161 RID: 94561 RVA: 0x00332911 File Offset: 0x00330B11
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06017162 RID: 94562 RVA: 0x0033291C File Offset: 0x00330B1C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "fld" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "item" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "hier" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cap" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017163 RID: 94563 RVA: 0x0033299F File Offset: 0x00330B9F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PageField>(deep);
		}

		// Token: 0x06017164 RID: 94564 RVA: 0x003329A8 File Offset: 0x00330BA8
		// Note: this type is marked as 'beforefieldinit'.
		static PageField()
		{
			byte[] array = new byte[5];
			PageField.attributeNamespaceIds = array;
			PageField.eleTagNames = new string[] { "extLst" };
			PageField.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009AFC RID: 39676
		private const string tagName = "pageField";

		// Token: 0x04009AFD RID: 39677
		private const byte tagNsId = 22;

		// Token: 0x04009AFE RID: 39678
		internal const int ElementTypeIdConst = 11125;

		// Token: 0x04009AFF RID: 39679
		private static string[] attributeTagNames = new string[] { "fld", "item", "hier", "name", "cap" };

		// Token: 0x04009B00 RID: 39680
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009B01 RID: 39681
		private static readonly string[] eleTagNames;

		// Token: 0x04009B02 RID: 39682
		private static readonly byte[] eleNamespaceIds;
	}
}
