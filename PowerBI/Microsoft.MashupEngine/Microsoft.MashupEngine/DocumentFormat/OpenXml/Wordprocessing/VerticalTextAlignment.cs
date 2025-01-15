using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EAB RID: 11947
	[GeneratedCode("DomGen", "2.0")]
	internal class VerticalTextAlignment : OpenXmlLeafElement
	{
		// Token: 0x17008BA0 RID: 35744
		// (get) Token: 0x06019632 RID: 103986 RVA: 0x003334AB File Offset: 0x003316AB
		public override string LocalName
		{
			get
			{
				return "vertAlign";
			}
		}

		// Token: 0x17008BA1 RID: 35745
		// (get) Token: 0x06019633 RID: 103987 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008BA2 RID: 35746
		// (get) Token: 0x06019634 RID: 103988 RVA: 0x00349199 File Offset: 0x00347399
		internal override int ElementTypeId
		{
			get
			{
				return 11604;
			}
		}

		// Token: 0x06019635 RID: 103989 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008BA3 RID: 35747
		// (get) Token: 0x06019636 RID: 103990 RVA: 0x003491A0 File Offset: 0x003473A0
		internal override string[] AttributeTagNames
		{
			get
			{
				return VerticalTextAlignment.attributeTagNames;
			}
		}

		// Token: 0x17008BA4 RID: 35748
		// (get) Token: 0x06019637 RID: 103991 RVA: 0x003491A7 File Offset: 0x003473A7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VerticalTextAlignment.attributeNamespaceIds;
			}
		}

		// Token: 0x17008BA5 RID: 35749
		// (get) Token: 0x06019638 RID: 103992 RVA: 0x003491AE File Offset: 0x003473AE
		// (set) Token: 0x06019639 RID: 103993 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<VerticalPositionValues> Val
		{
			get
			{
				return (EnumValue<VerticalPositionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601963B RID: 103995 RVA: 0x003491BD File Offset: 0x003473BD
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<VerticalPositionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601963C RID: 103996 RVA: 0x003491DF File Offset: 0x003473DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VerticalTextAlignment>(deep);
		}

		// Token: 0x0400A8B9 RID: 43193
		private const string tagName = "vertAlign";

		// Token: 0x0400A8BA RID: 43194
		private const byte tagNsId = 23;

		// Token: 0x0400A8BB RID: 43195
		internal const int ElementTypeIdConst = 11604;

		// Token: 0x0400A8BC RID: 43196
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A8BD RID: 43197
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
