using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A34 RID: 10804
	[GeneratedCode("DomGen", "2.0")]
	internal class HslColor : OpenXmlLeafElement
	{
		// Token: 0x170070E5 RID: 28901
		// (get) Token: 0x06015B4B RID: 88907 RVA: 0x00304859 File Offset: 0x00302A59
		public override string LocalName
		{
			get
			{
				return "hsl";
			}
		}

		// Token: 0x170070E6 RID: 28902
		// (get) Token: 0x06015B4C RID: 88908 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170070E7 RID: 28903
		// (get) Token: 0x06015B4D RID: 88909 RVA: 0x00322357 File Offset: 0x00320557
		internal override int ElementTypeId
		{
			get
			{
				return 12224;
			}
		}

		// Token: 0x06015B4E RID: 88910 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170070E8 RID: 28904
		// (get) Token: 0x06015B4F RID: 88911 RVA: 0x0032235E File Offset: 0x0032055E
		internal override string[] AttributeTagNames
		{
			get
			{
				return HslColor.attributeTagNames;
			}
		}

		// Token: 0x170070E9 RID: 28905
		// (get) Token: 0x06015B50 RID: 88912 RVA: 0x00322365 File Offset: 0x00320565
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HslColor.attributeNamespaceIds;
			}
		}

		// Token: 0x170070EA RID: 28906
		// (get) Token: 0x06015B51 RID: 88913 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06015B52 RID: 88914 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "h")]
		public Int32Value Hue
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

		// Token: 0x170070EB RID: 28907
		// (get) Token: 0x06015B53 RID: 88915 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06015B54 RID: 88916 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "s")]
		public Int32Value Saturation
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170070EC RID: 28908
		// (get) Token: 0x06015B55 RID: 88917 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06015B56 RID: 88918 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "l")]
		public Int32Value Lightness
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

		// Token: 0x06015B58 RID: 88920 RVA: 0x0032236C File Offset: 0x0032056C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "h" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "s" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "l" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015B59 RID: 88921 RVA: 0x003223C3 File Offset: 0x003205C3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HslColor>(deep);
		}

		// Token: 0x06015B5A RID: 88922 RVA: 0x003223CC File Offset: 0x003205CC
		// Note: this type is marked as 'beforefieldinit'.
		static HslColor()
		{
			byte[] array = new byte[3];
			HslColor.attributeNamespaceIds = array;
		}

		// Token: 0x0400947B RID: 38011
		private const string tagName = "hsl";

		// Token: 0x0400947C RID: 38012
		private const byte tagNsId = 24;

		// Token: 0x0400947D RID: 38013
		internal const int ElementTypeIdConst = 12224;

		// Token: 0x0400947E RID: 38014
		private static string[] attributeTagNames = new string[] { "h", "s", "l" };

		// Token: 0x0400947F RID: 38015
		private static byte[] attributeNamespaceIds;
	}
}
