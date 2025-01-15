using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029F8 RID: 10744
	[ChildElementInfo(typeof(EffectDag))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(EffectList))]
	internal class BackgroundProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006EA5 RID: 28325
		// (get) Token: 0x06015641 RID: 87617 RVA: 0x0031E785 File Offset: 0x0031C985
		public override string LocalName
		{
			get
			{
				return "bgPr";
			}
		}

		// Token: 0x17006EA6 RID: 28326
		// (get) Token: 0x06015642 RID: 87618 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006EA7 RID: 28327
		// (get) Token: 0x06015643 RID: 87619 RVA: 0x0031E78C File Offset: 0x0031C98C
		internal override int ElementTypeId
		{
			get
			{
				return 12171;
			}
		}

		// Token: 0x06015644 RID: 87620 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006EA8 RID: 28328
		// (get) Token: 0x06015645 RID: 87621 RVA: 0x0031E793 File Offset: 0x0031C993
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackgroundProperties.attributeTagNames;
			}
		}

		// Token: 0x17006EA9 RID: 28329
		// (get) Token: 0x06015646 RID: 87622 RVA: 0x0031E79A File Offset: 0x0031C99A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackgroundProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17006EAA RID: 28330
		// (get) Token: 0x06015647 RID: 87623 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06015648 RID: 87624 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "shadeToTitle")]
		public BooleanValue ShadeToTitle
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06015649 RID: 87625 RVA: 0x00293ECF File Offset: 0x002920CF
		public BackgroundProperties()
		{
		}

		// Token: 0x0601564A RID: 87626 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BackgroundProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601564B RID: 87627 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BackgroundProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601564C RID: 87628 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BackgroundProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601564D RID: 87629 RVA: 0x0031E7A4 File Offset: 0x0031C9A4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "noFill" == name)
			{
				return new NoFill();
			}
			if (10 == namespaceId && "solidFill" == name)
			{
				return new SolidFill();
			}
			if (10 == namespaceId && "gradFill" == name)
			{
				return new GradientFill();
			}
			if (10 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (10 == namespaceId && "pattFill" == name)
			{
				return new PatternFill();
			}
			if (10 == namespaceId && "effectLst" == name)
			{
				return new EffectList();
			}
			if (10 == namespaceId && "effectDag" == name)
			{
				return new EffectDag();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x0601564E RID: 87630 RVA: 0x0031E872 File Offset: 0x0031CA72
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "shadeToTitle" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601564F RID: 87631 RVA: 0x0031E892 File Offset: 0x0031CA92
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackgroundProperties>(deep);
		}

		// Token: 0x06015650 RID: 87632 RVA: 0x0031E89C File Offset: 0x0031CA9C
		// Note: this type is marked as 'beforefieldinit'.
		static BackgroundProperties()
		{
			byte[] array = new byte[1];
			BackgroundProperties.attributeNamespaceIds = array;
		}

		// Token: 0x0400934F RID: 37711
		private const string tagName = "bgPr";

		// Token: 0x04009350 RID: 37712
		private const byte tagNsId = 24;

		// Token: 0x04009351 RID: 37713
		internal const int ElementTypeIdConst = 12171;

		// Token: 0x04009352 RID: 37714
		private static string[] attributeTagNames = new string[] { "shadeToTitle" };

		// Token: 0x04009353 RID: 37715
		private static byte[] attributeNamespaceIds;
	}
}
