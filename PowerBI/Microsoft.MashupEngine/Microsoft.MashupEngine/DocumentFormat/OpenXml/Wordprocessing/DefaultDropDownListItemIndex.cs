using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F3B RID: 12091
	[GeneratedCode("DomGen", "2.0")]
	internal class DefaultDropDownListItemIndex : OpenXmlLeafElement
	{
		// Token: 0x17008FA2 RID: 36770
		// (get) Token: 0x06019F1F RID: 106271 RVA: 0x00344DAC File Offset: 0x00342FAC
		public override string LocalName
		{
			get
			{
				return "default";
			}
		}

		// Token: 0x17008FA3 RID: 36771
		// (get) Token: 0x06019F20 RID: 106272 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FA4 RID: 36772
		// (get) Token: 0x06019F21 RID: 106273 RVA: 0x0035A2B4 File Offset: 0x003584B4
		internal override int ElementTypeId
		{
			get
			{
				return 11741;
			}
		}

		// Token: 0x06019F22 RID: 106274 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008FA5 RID: 36773
		// (get) Token: 0x06019F23 RID: 106275 RVA: 0x0035A2BB File Offset: 0x003584BB
		internal override string[] AttributeTagNames
		{
			get
			{
				return DefaultDropDownListItemIndex.attributeTagNames;
			}
		}

		// Token: 0x17008FA6 RID: 36774
		// (get) Token: 0x06019F24 RID: 106276 RVA: 0x0035A2C2 File Offset: 0x003584C2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DefaultDropDownListItemIndex.attributeNamespaceIds;
			}
		}

		// Token: 0x17008FA7 RID: 36775
		// (get) Token: 0x06019F25 RID: 106277 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06019F26 RID: 106278 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public Int32Value Val
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

		// Token: 0x06019F28 RID: 106280 RVA: 0x00346792 File Offset: 0x00344992
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019F29 RID: 106281 RVA: 0x0035A2C9 File Offset: 0x003584C9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefaultDropDownListItemIndex>(deep);
		}

		// Token: 0x0400AB04 RID: 43780
		private const string tagName = "default";

		// Token: 0x0400AB05 RID: 43781
		private const byte tagNsId = 23;

		// Token: 0x0400AB06 RID: 43782
		internal const int ElementTypeIdConst = 11741;

		// Token: 0x0400AB07 RID: 43783
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AB08 RID: 43784
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
