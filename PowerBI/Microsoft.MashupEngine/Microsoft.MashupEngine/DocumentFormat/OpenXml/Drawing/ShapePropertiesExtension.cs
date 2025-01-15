using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200282E RID: 10286
	[ChildElementInfo(typeof(HiddenScene3D), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HiddenShape3D), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HiddenLineProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HiddenEffectsProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HiddenFillProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ShadowObscured), FileFormatVersions.Office2010)]
	internal class ShapePropertiesExtension : OpenXmlCompositeElement
	{
		// Token: 0x170065F5 RID: 26101
		// (get) Token: 0x06014263 RID: 82531 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170065F6 RID: 26102
		// (get) Token: 0x06014264 RID: 82532 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065F7 RID: 26103
		// (get) Token: 0x06014265 RID: 82533 RVA: 0x0030FC43 File Offset: 0x0030DE43
		internal override int ElementTypeId
		{
			get
			{
				return 10319;
			}
		}

		// Token: 0x06014266 RID: 82534 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170065F8 RID: 26104
		// (get) Token: 0x06014267 RID: 82535 RVA: 0x0030FC4A File Offset: 0x0030DE4A
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapePropertiesExtension.attributeTagNames;
			}
		}

		// Token: 0x170065F9 RID: 26105
		// (get) Token: 0x06014268 RID: 82536 RVA: 0x0030FC51 File Offset: 0x0030DE51
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapePropertiesExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170065FA RID: 26106
		// (get) Token: 0x06014269 RID: 82537 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601426A RID: 82538 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uri")]
		public StringValue Uri
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

		// Token: 0x0601426B RID: 82539 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapePropertiesExtension()
		{
		}

		// Token: 0x0601426C RID: 82540 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapePropertiesExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601426D RID: 82541 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapePropertiesExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601426E RID: 82542 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapePropertiesExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601426F RID: 82543 RVA: 0x0030FC58 File Offset: 0x0030DE58
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "hiddenFill" == name)
			{
				return new HiddenFillProperties();
			}
			if (48 == namespaceId && "hiddenLine" == name)
			{
				return new HiddenLineProperties();
			}
			if (48 == namespaceId && "hiddenEffects" == name)
			{
				return new HiddenEffectsProperties();
			}
			if (48 == namespaceId && "hiddenScene3d" == name)
			{
				return new HiddenScene3D();
			}
			if (48 == namespaceId && "hiddenSp3d" == name)
			{
				return new HiddenShape3D();
			}
			if (48 == namespaceId && "shadowObscured" == name)
			{
				return new ShadowObscured();
			}
			return null;
		}

		// Token: 0x06014270 RID: 82544 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014271 RID: 82545 RVA: 0x0030FCF6 File Offset: 0x0030DEF6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapePropertiesExtension>(deep);
		}

		// Token: 0x06014272 RID: 82546 RVA: 0x0030FD00 File Offset: 0x0030DF00
		// Note: this type is marked as 'beforefieldinit'.
		static ShapePropertiesExtension()
		{
			byte[] array = new byte[1];
			ShapePropertiesExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04008940 RID: 35136
		private const string tagName = "ext";

		// Token: 0x04008941 RID: 35137
		private const byte tagNsId = 10;

		// Token: 0x04008942 RID: 35138
		internal const int ElementTypeIdConst = 10319;

		// Token: 0x04008943 RID: 35139
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04008944 RID: 35140
		private static byte[] attributeNamespaceIds;
	}
}
