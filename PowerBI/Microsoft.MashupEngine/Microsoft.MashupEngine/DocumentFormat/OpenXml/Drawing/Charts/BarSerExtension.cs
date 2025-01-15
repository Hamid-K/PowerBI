using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing.Charts;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025FD RID: 9725
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(InvertSolidFillFormat), FileFormatVersions.Office2010)]
	internal class BarSerExtension : OpenXmlCompositeElement
	{
		// Token: 0x1700597C RID: 22908
		// (get) Token: 0x06012611 RID: 75281 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x1700597D RID: 22909
		// (get) Token: 0x06012612 RID: 75282 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700597E RID: 22910
		// (get) Token: 0x06012613 RID: 75283 RVA: 0x002FA557 File Offset: 0x002F8757
		internal override int ElementTypeId
		{
			get
			{
				return 10570;
			}
		}

		// Token: 0x06012614 RID: 75284 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700597F RID: 22911
		// (get) Token: 0x06012615 RID: 75285 RVA: 0x002FA55E File Offset: 0x002F875E
		internal override string[] AttributeTagNames
		{
			get
			{
				return BarSerExtension.attributeTagNames;
			}
		}

		// Token: 0x17005980 RID: 22912
		// (get) Token: 0x06012616 RID: 75286 RVA: 0x002FA565 File Offset: 0x002F8765
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BarSerExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x17005981 RID: 22913
		// (get) Token: 0x06012617 RID: 75287 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012618 RID: 75288 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012619 RID: 75289 RVA: 0x00293ECF File Offset: 0x002920CF
		public BarSerExtension()
		{
		}

		// Token: 0x0601261A RID: 75290 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BarSerExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601261B RID: 75291 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BarSerExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601261C RID: 75292 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BarSerExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601261D RID: 75293 RVA: 0x002FA504 File Offset: 0x002F8704
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (46 == namespaceId && "invertSolidFillFmt" == name)
			{
				return new InvertSolidFillFormat();
			}
			return null;
		}

		// Token: 0x0601261E RID: 75294 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601261F RID: 75295 RVA: 0x002FA56C File Offset: 0x002F876C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BarSerExtension>(deep);
		}

		// Token: 0x06012620 RID: 75296 RVA: 0x002FA578 File Offset: 0x002F8778
		// Note: this type is marked as 'beforefieldinit'.
		static BarSerExtension()
		{
			byte[] array = new byte[1];
			BarSerExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04007F55 RID: 32597
		private const string tagName = "ext";

		// Token: 0x04007F56 RID: 32598
		private const byte tagNsId = 11;

		// Token: 0x04007F57 RID: 32599
		internal const int ElementTypeIdConst = 10570;

		// Token: 0x04007F58 RID: 32600
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04007F59 RID: 32601
		private static byte[] attributeNamespaceIds;
	}
}
