using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E2C RID: 11820
	[GeneratedCode("DomGen", "2.0")]
	internal class TextDirection : OpenXmlLeafElement
	{
		// Token: 0x17008967 RID: 35175
		// (get) Token: 0x06019196 RID: 102806 RVA: 0x003465E0 File Offset: 0x003447E0
		public override string LocalName
		{
			get
			{
				return "textDirection";
			}
		}

		// Token: 0x17008968 RID: 35176
		// (get) Token: 0x06019197 RID: 102807 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008969 RID: 35177
		// (get) Token: 0x06019198 RID: 102808 RVA: 0x003465E7 File Offset: 0x003447E7
		internal override int ElementTypeId
		{
			get
			{
				return 11519;
			}
		}

		// Token: 0x06019199 RID: 102809 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700896A RID: 35178
		// (get) Token: 0x0601919A RID: 102810 RVA: 0x003465EE File Offset: 0x003447EE
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextDirection.attributeTagNames;
			}
		}

		// Token: 0x1700896B RID: 35179
		// (get) Token: 0x0601919B RID: 102811 RVA: 0x003465F5 File Offset: 0x003447F5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextDirection.attributeNamespaceIds;
			}
		}

		// Token: 0x1700896C RID: 35180
		// (get) Token: 0x0601919C RID: 102812 RVA: 0x003465FC File Offset: 0x003447FC
		// (set) Token: 0x0601919D RID: 102813 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<TextDirectionValues> Val
		{
			get
			{
				return (EnumValue<TextDirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601919F RID: 102815 RVA: 0x0034660B File Offset: 0x0034480B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<TextDirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060191A0 RID: 102816 RVA: 0x0034662D File Offset: 0x0034482D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextDirection>(deep);
		}

		// Token: 0x0400A6FD RID: 42749
		private const string tagName = "textDirection";

		// Token: 0x0400A6FE RID: 42750
		private const byte tagNsId = 23;

		// Token: 0x0400A6FF RID: 42751
		internal const int ElementTypeIdConst = 11519;

		// Token: 0x0400A700 RID: 42752
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A701 RID: 42753
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
