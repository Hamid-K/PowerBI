using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x02003072 RID: 12402
	[GeneratedCode("DomGen", "2.0")]
	internal class Info : OpenXmlCompositeElement
	{
		// Token: 0x1700967C RID: 38524
		// (get) Token: 0x0601ADD4 RID: 110036 RVA: 0x003688DB File Offset: 0x00366ADB
		public override string LocalName
		{
			get
			{
				return "info";
			}
		}

		// Token: 0x1700967D RID: 38525
		// (get) Token: 0x0601ADD5 RID: 110037 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x1700967E RID: 38526
		// (get) Token: 0x0601ADD6 RID: 110038 RVA: 0x003688E2 File Offset: 0x00366AE2
		internal override int ElementTypeId
		{
			get
			{
				return 12671;
			}
		}

		// Token: 0x0601ADD7 RID: 110039 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700967F RID: 38527
		// (get) Token: 0x0601ADD8 RID: 110040 RVA: 0x003688E9 File Offset: 0x00366AE9
		internal override string[] AttributeTagNames
		{
			get
			{
				return Info.attributeTagNames;
			}
		}

		// Token: 0x17009680 RID: 38528
		// (get) Token: 0x0601ADD9 RID: 110041 RVA: 0x003688F0 File Offset: 0x00366AF0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Info.attributeNamespaceIds;
			}
		}

		// Token: 0x17009681 RID: 38529
		// (get) Token: 0x0601ADDA RID: 110042 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601ADDB RID: 110043 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
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

		// Token: 0x0601ADDC RID: 110044 RVA: 0x00293ECF File Offset: 0x002920CF
		public Info()
		{
		}

		// Token: 0x0601ADDD RID: 110045 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Info(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601ADDE RID: 110046 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Info(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601ADDF RID: 110047 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Info(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601ADE0 RID: 110048 RVA: 0x000020FA File Offset: 0x000002FA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x0601ADE1 RID: 110049 RVA: 0x002BFD13 File Offset: 0x002BDF13
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601ADE2 RID: 110050 RVA: 0x003688F7 File Offset: 0x00366AF7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Info>(deep);
		}

		// Token: 0x0601ADE3 RID: 110051 RVA: 0x00368900 File Offset: 0x00366B00
		// Note: this type is marked as 'beforefieldinit'.
		static Info()
		{
			byte[] array = new byte[1];
			Info.attributeNamespaceIds = array;
		}

		// Token: 0x0400B210 RID: 45584
		private const string tagName = "info";

		// Token: 0x0400B211 RID: 45585
		private const byte tagNsId = 44;

		// Token: 0x0400B212 RID: 45586
		internal const int ElementTypeIdConst = 12671;

		// Token: 0x0400B213 RID: 45587
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x0400B214 RID: 45588
		private static byte[] attributeNamespaceIds;
	}
}
