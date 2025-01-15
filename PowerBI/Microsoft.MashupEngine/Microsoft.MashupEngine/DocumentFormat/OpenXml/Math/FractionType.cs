using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029AA RID: 10666
	[GeneratedCode("DomGen", "2.0")]
	internal class FractionType : OpenXmlLeafElement
	{
		// Token: 0x17006D4C RID: 27980
		// (get) Token: 0x06015359 RID: 86873 RVA: 0x0031CE60 File Offset: 0x0031B060
		public override string LocalName
		{
			get
			{
				return "type";
			}
		}

		// Token: 0x17006D4D RID: 27981
		// (get) Token: 0x0601535A RID: 86874 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D4E RID: 27982
		// (get) Token: 0x0601535B RID: 86875 RVA: 0x0031CE67 File Offset: 0x0031B067
		internal override int ElementTypeId
		{
			get
			{
				return 10901;
			}
		}

		// Token: 0x0601535C RID: 86876 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006D4F RID: 27983
		// (get) Token: 0x0601535D RID: 86877 RVA: 0x0031CE6E File Offset: 0x0031B06E
		internal override string[] AttributeTagNames
		{
			get
			{
				return FractionType.attributeTagNames;
			}
		}

		// Token: 0x17006D50 RID: 27984
		// (get) Token: 0x0601535E RID: 86878 RVA: 0x0031CE75 File Offset: 0x0031B075
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FractionType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006D51 RID: 27985
		// (get) Token: 0x0601535F RID: 86879 RVA: 0x0031CE7C File Offset: 0x0031B07C
		// (set) Token: 0x06015360 RID: 86880 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public EnumValue<FractionTypeValues> Val
		{
			get
			{
				return (EnumValue<FractionTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06015362 RID: 86882 RVA: 0x0031CE8B File Offset: 0x0031B08B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new EnumValue<FractionTypeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015363 RID: 86883 RVA: 0x0031CEAD File Offset: 0x0031B0AD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FractionType>(deep);
		}

		// Token: 0x0400921B RID: 37403
		private const string tagName = "type";

		// Token: 0x0400921C RID: 37404
		private const byte tagNsId = 21;

		// Token: 0x0400921D RID: 37405
		internal const int ElementTypeIdConst = 10901;

		// Token: 0x0400921E RID: 37406
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400921F RID: 37407
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
