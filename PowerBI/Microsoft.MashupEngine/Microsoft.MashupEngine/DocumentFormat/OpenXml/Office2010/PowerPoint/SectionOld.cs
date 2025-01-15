using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023C1 RID: 9153
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SectionOld : OpenXmlCompositeElement
	{
		// Token: 0x17004CCD RID: 19661
		// (get) Token: 0x060109B6 RID: 68022 RVA: 0x002E5401 File Offset: 0x002E3601
		public override string LocalName
		{
			get
			{
				return "section";
			}
		}

		// Token: 0x17004CCE RID: 19662
		// (get) Token: 0x060109B7 RID: 68023 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004CCF RID: 19663
		// (get) Token: 0x060109B8 RID: 68024 RVA: 0x002E5408 File Offset: 0x002E3608
		internal override int ElementTypeId
		{
			get
			{
				return 12807;
			}
		}

		// Token: 0x060109B9 RID: 68025 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004CD0 RID: 19664
		// (get) Token: 0x060109BA RID: 68026 RVA: 0x002E540F File Offset: 0x002E360F
		internal override string[] AttributeTagNames
		{
			get
			{
				return SectionOld.attributeTagNames;
			}
		}

		// Token: 0x17004CD1 RID: 19665
		// (get) Token: 0x060109BB RID: 68027 RVA: 0x002E5416 File Offset: 0x002E3616
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SectionOld.attributeNamespaceIds;
			}
		}

		// Token: 0x17004CD2 RID: 19666
		// (get) Token: 0x060109BC RID: 68028 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060109BD RID: 68029 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x17004CD3 RID: 19667
		// (get) Token: 0x060109BE RID: 68030 RVA: 0x002E541D File Offset: 0x002E361D
		// (set) Token: 0x060109BF RID: 68031 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "slideIdLst")]
		public ListValue<UInt32Value> SlideIdList
		{
			get
			{
				return (ListValue<UInt32Value>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004CD4 RID: 19668
		// (get) Token: 0x060109C0 RID: 68032 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060109C1 RID: 68033 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x060109C2 RID: 68034 RVA: 0x00293ECF File Offset: 0x002920CF
		public SectionOld()
		{
		}

		// Token: 0x060109C3 RID: 68035 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SectionOld(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060109C4 RID: 68036 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SectionOld(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060109C5 RID: 68037 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SectionOld(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060109C6 RID: 68038 RVA: 0x002E542C File Offset: 0x002E362C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17004CD5 RID: 19669
		// (get) Token: 0x060109C7 RID: 68039 RVA: 0x002E5447 File Offset: 0x002E3647
		internal override string[] ElementTagNames
		{
			get
			{
				return SectionOld.eleTagNames;
			}
		}

		// Token: 0x17004CD6 RID: 19670
		// (get) Token: 0x060109C8 RID: 68040 RVA: 0x002E544E File Offset: 0x002E364E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SectionOld.eleNamespaceIds;
			}
		}

		// Token: 0x17004CD7 RID: 19671
		// (get) Token: 0x060109C9 RID: 68041 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004CD8 RID: 19672
		// (get) Token: 0x060109CA RID: 68042 RVA: 0x002E5455 File Offset: 0x002E3655
		// (set) Token: 0x060109CB RID: 68043 RVA: 0x002E545E File Offset: 0x002E365E
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

		// Token: 0x060109CC RID: 68044 RVA: 0x002E5468 File Offset: 0x002E3668
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "slideIdLst" == name)
			{
				return new ListValue<UInt32Value>();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060109CD RID: 68045 RVA: 0x002E54BF File Offset: 0x002E36BF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SectionOld>(deep);
		}

		// Token: 0x060109CE RID: 68046 RVA: 0x002E54C8 File Offset: 0x002E36C8
		// Note: this type is marked as 'beforefieldinit'.
		static SectionOld()
		{
			byte[] array = new byte[3];
			SectionOld.attributeNamespaceIds = array;
			SectionOld.eleTagNames = new string[] { "extLst" };
			SectionOld.eleNamespaceIds = new byte[] { 49 };
		}

		// Token: 0x0400757A RID: 30074
		private const string tagName = "section";

		// Token: 0x0400757B RID: 30075
		private const byte tagNsId = 49;

		// Token: 0x0400757C RID: 30076
		internal const int ElementTypeIdConst = 12807;

		// Token: 0x0400757D RID: 30077
		private static string[] attributeTagNames = new string[] { "name", "slideIdLst", "id" };

		// Token: 0x0400757E RID: 30078
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400757F RID: 30079
		private static readonly string[] eleTagNames;

		// Token: 0x04007580 RID: 30080
		private static readonly byte[] eleNamespaceIds;
	}
}
