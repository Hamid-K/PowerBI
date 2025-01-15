using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025AC RID: 9644
	[GeneratedCode("DomGen", "2.0")]
	internal class OfPieType : OpenXmlLeafElement
	{
		// Token: 0x17005713 RID: 22291
		// (get) Token: 0x060120CB RID: 73931 RVA: 0x002F50BF File Offset: 0x002F32BF
		public override string LocalName
		{
			get
			{
				return "ofPieType";
			}
		}

		// Token: 0x17005714 RID: 22292
		// (get) Token: 0x060120CC RID: 73932 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005715 RID: 22293
		// (get) Token: 0x060120CD RID: 73933 RVA: 0x002F50C6 File Offset: 0x002F32C6
		internal override int ElementTypeId
		{
			get
			{
				return 10462;
			}
		}

		// Token: 0x060120CE RID: 73934 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005716 RID: 22294
		// (get) Token: 0x060120CF RID: 73935 RVA: 0x002F50CD File Offset: 0x002F32CD
		internal override string[] AttributeTagNames
		{
			get
			{
				return OfPieType.attributeTagNames;
			}
		}

		// Token: 0x17005717 RID: 22295
		// (get) Token: 0x060120D0 RID: 73936 RVA: 0x002F50D4 File Offset: 0x002F32D4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OfPieType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005718 RID: 22296
		// (get) Token: 0x060120D1 RID: 73937 RVA: 0x002F50DB File Offset: 0x002F32DB
		// (set) Token: 0x060120D2 RID: 73938 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<OfPieValues> Val
		{
			get
			{
				return (EnumValue<OfPieValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060120D4 RID: 73940 RVA: 0x002F50EA File Offset: 0x002F32EA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<OfPieValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060120D5 RID: 73941 RVA: 0x002F510A File Offset: 0x002F330A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OfPieType>(deep);
		}

		// Token: 0x060120D6 RID: 73942 RVA: 0x002F5114 File Offset: 0x002F3314
		// Note: this type is marked as 'beforefieldinit'.
		static OfPieType()
		{
			byte[] array = new byte[1];
			OfPieType.attributeNamespaceIds = array;
		}

		// Token: 0x04007DEB RID: 32235
		private const string tagName = "ofPieType";

		// Token: 0x04007DEC RID: 32236
		private const byte tagNsId = 11;

		// Token: 0x04007DED RID: 32237
		internal const int ElementTypeIdConst = 10462;

		// Token: 0x04007DEE RID: 32238
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007DEF RID: 32239
		private static byte[] attributeNamespaceIds;
	}
}
