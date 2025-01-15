using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200245B RID: 9307
	[ChildElementInfo(typeof(AllocatedCommands))]
	[ChildElementInfo(typeof(MismatchedKeyMapCustomization))]
	[ChildElementInfo(typeof(KeyMapCustomizations))]
	[ChildElementInfo(typeof(Toolbars))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TemplateCommandGroup : OpenXmlPartRootElement
	{
		// Token: 0x17005099 RID: 20633
		// (get) Token: 0x06011240 RID: 70208 RVA: 0x002EAFC7 File Offset: 0x002E91C7
		public override string LocalName
		{
			get
			{
				return "tcg";
			}
		}

		// Token: 0x1700509A RID: 20634
		// (get) Token: 0x06011241 RID: 70209 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x1700509B RID: 20635
		// (get) Token: 0x06011242 RID: 70210 RVA: 0x002EAFD2 File Offset: 0x002E91D2
		internal override int ElementTypeId
		{
			get
			{
				return 12537;
			}
		}

		// Token: 0x06011243 RID: 70211 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011244 RID: 70212 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal TemplateCommandGroup(CustomizationPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06011245 RID: 70213 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(CustomizationPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x1700509C RID: 20636
		// (get) Token: 0x06011246 RID: 70214 RVA: 0x002EAFD9 File Offset: 0x002E91D9
		// (set) Token: 0x06011247 RID: 70215 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public CustomizationPart CustomizationPart
		{
			get
			{
				return base.OpenXmlPart as CustomizationPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06011248 RID: 70216 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public TemplateCommandGroup(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011249 RID: 70217 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public TemplateCommandGroup(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601124A RID: 70218 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public TemplateCommandGroup(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601124B RID: 70219 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public TemplateCommandGroup()
		{
		}

		// Token: 0x0601124C RID: 70220 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(CustomizationPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601124D RID: 70221 RVA: 0x002EAFE8 File Offset: 0x002E91E8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "keymaps" == name)
			{
				return new KeyMapCustomizations();
			}
			if (33 == namespaceId && "keymapsBad" == name)
			{
				return new MismatchedKeyMapCustomization();
			}
			if (33 == namespaceId && "toolbars" == name)
			{
				return new Toolbars();
			}
			if (33 == namespaceId && "acds" == name)
			{
				return new AllocatedCommands();
			}
			return null;
		}

		// Token: 0x0601124E RID: 70222 RVA: 0x002EB056 File Offset: 0x002E9256
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TemplateCommandGroup>(deep);
		}

		// Token: 0x04007859 RID: 30809
		private const string tagName = "tcg";

		// Token: 0x0400785A RID: 30810
		private const byte tagNsId = 33;

		// Token: 0x0400785B RID: 30811
		internal const int ElementTypeIdConst = 12537;
	}
}
