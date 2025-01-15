using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002779 RID: 10105
	[ChildElementInfo(typeof(MajorFont))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MinorFont))]
	internal class FontScheme : OpenXmlCompositeElement
	{
		// Token: 0x17006189 RID: 24969
		// (get) Token: 0x0601381F RID: 79903 RVA: 0x00307E8C File Offset: 0x0030608C
		public override string LocalName
		{
			get
			{
				return "fontScheme";
			}
		}

		// Token: 0x1700618A RID: 24970
		// (get) Token: 0x06013820 RID: 79904 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700618B RID: 24971
		// (get) Token: 0x06013821 RID: 79905 RVA: 0x00307E93 File Offset: 0x00306093
		internal override int ElementTypeId
		{
			get
			{
				return 10145;
			}
		}

		// Token: 0x06013822 RID: 79906 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700618C RID: 24972
		// (get) Token: 0x06013823 RID: 79907 RVA: 0x00307E9A File Offset: 0x0030609A
		internal override string[] AttributeTagNames
		{
			get
			{
				return FontScheme.attributeTagNames;
			}
		}

		// Token: 0x1700618D RID: 24973
		// (get) Token: 0x06013824 RID: 79908 RVA: 0x00307EA1 File Offset: 0x003060A1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FontScheme.attributeNamespaceIds;
			}
		}

		// Token: 0x1700618E RID: 24974
		// (get) Token: 0x06013825 RID: 79909 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06013826 RID: 79910 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06013827 RID: 79911 RVA: 0x00293ECF File Offset: 0x002920CF
		public FontScheme()
		{
		}

		// Token: 0x06013828 RID: 79912 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FontScheme(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013829 RID: 79913 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FontScheme(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601382A RID: 79914 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FontScheme(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601382B RID: 79915 RVA: 0x00307EA8 File Offset: 0x003060A8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "majorFont" == name)
			{
				return new MajorFont();
			}
			if (10 == namespaceId && "minorFont" == name)
			{
				return new MinorFont();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700618F RID: 24975
		// (get) Token: 0x0601382C RID: 79916 RVA: 0x00307EFE File Offset: 0x003060FE
		internal override string[] ElementTagNames
		{
			get
			{
				return FontScheme.eleTagNames;
			}
		}

		// Token: 0x17006190 RID: 24976
		// (get) Token: 0x0601382D RID: 79917 RVA: 0x00307F05 File Offset: 0x00306105
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FontScheme.eleNamespaceIds;
			}
		}

		// Token: 0x17006191 RID: 24977
		// (get) Token: 0x0601382E RID: 79918 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006192 RID: 24978
		// (get) Token: 0x0601382F RID: 79919 RVA: 0x00307F0C File Offset: 0x0030610C
		// (set) Token: 0x06013830 RID: 79920 RVA: 0x00307F15 File Offset: 0x00306115
		public MajorFont MajorFont
		{
			get
			{
				return base.GetElement<MajorFont>(0);
			}
			set
			{
				base.SetElement<MajorFont>(0, value);
			}
		}

		// Token: 0x17006193 RID: 24979
		// (get) Token: 0x06013831 RID: 79921 RVA: 0x00307F1F File Offset: 0x0030611F
		// (set) Token: 0x06013832 RID: 79922 RVA: 0x00307F28 File Offset: 0x00306128
		public MinorFont MinorFont
		{
			get
			{
				return base.GetElement<MinorFont>(1);
			}
			set
			{
				base.SetElement<MinorFont>(1, value);
			}
		}

		// Token: 0x17006194 RID: 24980
		// (get) Token: 0x06013833 RID: 79923 RVA: 0x003012C6 File Offset: 0x002FF4C6
		// (set) Token: 0x06013834 RID: 79924 RVA: 0x003012CF File Offset: 0x002FF4CF
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(2);
			}
			set
			{
				base.SetElement<ExtensionList>(2, value);
			}
		}

		// Token: 0x06013835 RID: 79925 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013836 RID: 79926 RVA: 0x00307F32 File Offset: 0x00306132
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FontScheme>(deep);
		}

		// Token: 0x06013837 RID: 79927 RVA: 0x00307F3C File Offset: 0x0030613C
		// Note: this type is marked as 'beforefieldinit'.
		static FontScheme()
		{
			byte[] array = new byte[1];
			FontScheme.attributeNamespaceIds = array;
			FontScheme.eleTagNames = new string[] { "majorFont", "minorFont", "extLst" };
			FontScheme.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x04008681 RID: 34433
		private const string tagName = "fontScheme";

		// Token: 0x04008682 RID: 34434
		private const byte tagNsId = 10;

		// Token: 0x04008683 RID: 34435
		internal const int ElementTypeIdConst = 10145;

		// Token: 0x04008684 RID: 34436
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x04008685 RID: 34437
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008686 RID: 34438
		private static readonly string[] eleTagNames;

		// Token: 0x04008687 RID: 34439
		private static readonly byte[] eleNamespaceIds;
	}
}
