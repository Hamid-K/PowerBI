using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F75 RID: 12149
	[GeneratedCode("DomGen", "2.0")]
	internal class Destination : OpenXmlLeafElement
	{
		// Token: 0x1700910D RID: 37133
		// (get) Token: 0x0601A23B RID: 107067 RVA: 0x0035DE7C File Offset: 0x0035C07C
		public override string LocalName
		{
			get
			{
				return "destination";
			}
		}

		// Token: 0x1700910E RID: 37134
		// (get) Token: 0x0601A23C RID: 107068 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700910F RID: 37135
		// (get) Token: 0x0601A23D RID: 107069 RVA: 0x0035DE83 File Offset: 0x0035C083
		internal override int ElementTypeId
		{
			get
			{
				return 11820;
			}
		}

		// Token: 0x0601A23E RID: 107070 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009110 RID: 37136
		// (get) Token: 0x0601A23F RID: 107071 RVA: 0x0035DE8A File Offset: 0x0035C08A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Destination.attributeTagNames;
			}
		}

		// Token: 0x17009111 RID: 37137
		// (get) Token: 0x0601A240 RID: 107072 RVA: 0x0035DE91 File Offset: 0x0035C091
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Destination.attributeNamespaceIds;
			}
		}

		// Token: 0x17009112 RID: 37138
		// (get) Token: 0x0601A241 RID: 107073 RVA: 0x0035DE98 File Offset: 0x0035C098
		// (set) Token: 0x0601A242 RID: 107074 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<MailMergeDestinationValues> Val
		{
			get
			{
				return (EnumValue<MailMergeDestinationValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A244 RID: 107076 RVA: 0x0035DEA7 File Offset: 0x0035C0A7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<MailMergeDestinationValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A245 RID: 107077 RVA: 0x0035DEC9 File Offset: 0x0035C0C9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Destination>(deep);
		}

		// Token: 0x0400ABFE RID: 44030
		private const string tagName = "destination";

		// Token: 0x0400ABFF RID: 44031
		private const byte tagNsId = 23;

		// Token: 0x0400AC00 RID: 44032
		internal const int ElementTypeIdConst = 11820;

		// Token: 0x0400AC01 RID: 44033
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AC02 RID: 44034
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
