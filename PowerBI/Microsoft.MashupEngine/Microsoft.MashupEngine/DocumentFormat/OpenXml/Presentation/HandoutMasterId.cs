using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A53 RID: 10835
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class HandoutMasterId : OpenXmlCompositeElement
	{
		// Token: 0x170071F2 RID: 29170
		// (get) Token: 0x06015D9A RID: 89498 RVA: 0x00323B87 File Offset: 0x00321D87
		public override string LocalName
		{
			get
			{
				return "handoutMasterId";
			}
		}

		// Token: 0x170071F3 RID: 29171
		// (get) Token: 0x06015D9B RID: 89499 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170071F4 RID: 29172
		// (get) Token: 0x06015D9C RID: 89500 RVA: 0x00323B8E File Offset: 0x00321D8E
		internal override int ElementTypeId
		{
			get
			{
				return 12254;
			}
		}

		// Token: 0x06015D9D RID: 89501 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170071F5 RID: 29173
		// (get) Token: 0x06015D9E RID: 89502 RVA: 0x00323B95 File Offset: 0x00321D95
		internal override string[] AttributeTagNames
		{
			get
			{
				return HandoutMasterId.attributeTagNames;
			}
		}

		// Token: 0x170071F6 RID: 29174
		// (get) Token: 0x06015D9F RID: 89503 RVA: 0x00323B9C File Offset: 0x00321D9C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HandoutMasterId.attributeNamespaceIds;
			}
		}

		// Token: 0x170071F7 RID: 29175
		// (get) Token: 0x06015DA0 RID: 89504 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015DA1 RID: 89505 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
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

		// Token: 0x06015DA2 RID: 89506 RVA: 0x00293ECF File Offset: 0x002920CF
		public HandoutMasterId()
		{
		}

		// Token: 0x06015DA3 RID: 89507 RVA: 0x00293ED7 File Offset: 0x002920D7
		public HandoutMasterId(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015DA4 RID: 89508 RVA: 0x00293EE0 File Offset: 0x002920E0
		public HandoutMasterId(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015DA5 RID: 89509 RVA: 0x00293EE9 File Offset: 0x002920E9
		public HandoutMasterId(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015DA6 RID: 89510 RVA: 0x0031FDA2 File Offset: 0x0031DFA2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170071F8 RID: 29176
		// (get) Token: 0x06015DA7 RID: 89511 RVA: 0x00323BA3 File Offset: 0x00321DA3
		internal override string[] ElementTagNames
		{
			get
			{
				return HandoutMasterId.eleTagNames;
			}
		}

		// Token: 0x170071F9 RID: 29177
		// (get) Token: 0x06015DA8 RID: 89512 RVA: 0x00323BAA File Offset: 0x00321DAA
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return HandoutMasterId.eleNamespaceIds;
			}
		}

		// Token: 0x170071FA RID: 29178
		// (get) Token: 0x06015DA9 RID: 89513 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170071FB RID: 29179
		// (get) Token: 0x06015DAA RID: 89514 RVA: 0x0031FDCB File Offset: 0x0031DFCB
		// (set) Token: 0x06015DAB RID: 89515 RVA: 0x0031FDD4 File Offset: 0x0031DFD4
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

		// Token: 0x06015DAC RID: 89516 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015DAD RID: 89517 RVA: 0x00323BB1 File Offset: 0x00321DB1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HandoutMasterId>(deep);
		}

		// Token: 0x04009518 RID: 38168
		private const string tagName = "handoutMasterId";

		// Token: 0x04009519 RID: 38169
		private const byte tagNsId = 24;

		// Token: 0x0400951A RID: 38170
		internal const int ElementTypeIdConst = 12254;

		// Token: 0x0400951B RID: 38171
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x0400951C RID: 38172
		private static byte[] attributeNamespaceIds = new byte[] { 19 };

		// Token: 0x0400951D RID: 38173
		private static readonly string[] eleTagNames = new string[] { "extLst" };

		// Token: 0x0400951E RID: 38174
		private static readonly byte[] eleNamespaceIds = new byte[] { 24 };
	}
}
