using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FA1 RID: 12193
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AbstractNumId))]
	[ChildElementInfo(typeof(LevelOverride))]
	internal class NumberingInstance : OpenXmlCompositeElement
	{
		// Token: 0x1700929A RID: 37530
		// (get) Token: 0x0601A57F RID: 107903 RVA: 0x0031C352 File Offset: 0x0031A552
		public override string LocalName
		{
			get
			{
				return "num";
			}
		}

		// Token: 0x1700929B RID: 37531
		// (get) Token: 0x0601A580 RID: 107904 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700929C RID: 37532
		// (get) Token: 0x0601A581 RID: 107905 RVA: 0x00360EBF File Offset: 0x0035F0BF
		internal override int ElementTypeId
		{
			get
			{
				return 11886;
			}
		}

		// Token: 0x0601A582 RID: 107906 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700929D RID: 37533
		// (get) Token: 0x0601A583 RID: 107907 RVA: 0x00360EC6 File Offset: 0x0035F0C6
		internal override string[] AttributeTagNames
		{
			get
			{
				return NumberingInstance.attributeTagNames;
			}
		}

		// Token: 0x1700929E RID: 37534
		// (get) Token: 0x0601A584 RID: 107908 RVA: 0x00360ECD File Offset: 0x0035F0CD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NumberingInstance.attributeNamespaceIds;
			}
		}

		// Token: 0x1700929F RID: 37535
		// (get) Token: 0x0601A585 RID: 107909 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601A586 RID: 107910 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "numId")]
		public Int32Value NumberID
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

		// Token: 0x0601A587 RID: 107911 RVA: 0x00293ECF File Offset: 0x002920CF
		public NumberingInstance()
		{
		}

		// Token: 0x0601A588 RID: 107912 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NumberingInstance(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A589 RID: 107913 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NumberingInstance(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A58A RID: 107914 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NumberingInstance(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A58B RID: 107915 RVA: 0x00360ED4 File Offset: 0x0035F0D4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "abstractNumId" == name)
			{
				return new AbstractNumId();
			}
			if (23 == namespaceId && "lvlOverride" == name)
			{
				return new LevelOverride();
			}
			return null;
		}

		// Token: 0x170092A0 RID: 37536
		// (get) Token: 0x0601A58C RID: 107916 RVA: 0x00360F07 File Offset: 0x0035F107
		internal override string[] ElementTagNames
		{
			get
			{
				return NumberingInstance.eleTagNames;
			}
		}

		// Token: 0x170092A1 RID: 37537
		// (get) Token: 0x0601A58D RID: 107917 RVA: 0x00360F0E File Offset: 0x0035F10E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NumberingInstance.eleNamespaceIds;
			}
		}

		// Token: 0x170092A2 RID: 37538
		// (get) Token: 0x0601A58E RID: 107918 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170092A3 RID: 37539
		// (get) Token: 0x0601A58F RID: 107919 RVA: 0x00360F15 File Offset: 0x0035F115
		// (set) Token: 0x0601A590 RID: 107920 RVA: 0x00360F1E File Offset: 0x0035F11E
		public AbstractNumId AbstractNumId
		{
			get
			{
				return base.GetElement<AbstractNumId>(0);
			}
			set
			{
				base.SetElement<AbstractNumId>(0, value);
			}
		}

		// Token: 0x0601A591 RID: 107921 RVA: 0x00360F28 File Offset: 0x0035F128
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "numId" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A592 RID: 107922 RVA: 0x00360F4A File Offset: 0x0035F14A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingInstance>(deep);
		}

		// Token: 0x0400ACB6 RID: 44214
		private const string tagName = "num";

		// Token: 0x0400ACB7 RID: 44215
		private const byte tagNsId = 23;

		// Token: 0x0400ACB8 RID: 44216
		internal const int ElementTypeIdConst = 11886;

		// Token: 0x0400ACB9 RID: 44217
		private static string[] attributeTagNames = new string[] { "numId" };

		// Token: 0x0400ACBA RID: 44218
		private static byte[] attributeNamespaceIds = new byte[] { 23 };

		// Token: 0x0400ACBB RID: 44219
		private static readonly string[] eleTagNames = new string[] { "abstractNumId", "lvlOverride" };

		// Token: 0x0400ACBC RID: 44220
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23 };
	}
}
