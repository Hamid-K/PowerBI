using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E2A RID: 11818
	[GeneratedCode("DomGen", "2.0")]
	internal class Indentation : OpenXmlLeafElement
	{
		// Token: 0x17008950 RID: 35152
		// (get) Token: 0x06019168 RID: 102760 RVA: 0x0034636C File Offset: 0x0034456C
		public override string LocalName
		{
			get
			{
				return "ind";
			}
		}

		// Token: 0x17008951 RID: 35153
		// (get) Token: 0x06019169 RID: 102761 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008952 RID: 35154
		// (get) Token: 0x0601916A RID: 102762 RVA: 0x00346373 File Offset: 0x00344573
		internal override int ElementTypeId
		{
			get
			{
				return 11514;
			}
		}

		// Token: 0x0601916B RID: 102763 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008953 RID: 35155
		// (get) Token: 0x0601916C RID: 102764 RVA: 0x0034637A File Offset: 0x0034457A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Indentation.attributeTagNames;
			}
		}

		// Token: 0x17008954 RID: 35156
		// (get) Token: 0x0601916D RID: 102765 RVA: 0x00346381 File Offset: 0x00344581
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Indentation.attributeNamespaceIds;
			}
		}

		// Token: 0x17008955 RID: 35157
		// (get) Token: 0x0601916E RID: 102766 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601916F RID: 102767 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "left")]
		public StringValue Left
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

		// Token: 0x17008956 RID: 35158
		// (get) Token: 0x06019170 RID: 102768 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06019171 RID: 102769 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "start")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public StringValue Start
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008957 RID: 35159
		// (get) Token: 0x06019172 RID: 102770 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06019173 RID: 102771 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "leftChars")]
		public Int32Value LeftChars
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

		// Token: 0x17008958 RID: 35160
		// (get) Token: 0x06019174 RID: 102772 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06019175 RID: 102773 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "startChars")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public Int32Value StartCharacters
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008959 RID: 35161
		// (get) Token: 0x06019176 RID: 102774 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06019177 RID: 102775 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "right")]
		public StringValue Right
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700895A RID: 35162
		// (get) Token: 0x06019178 RID: 102776 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x06019179 RID: 102777 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "end")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public StringValue End
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700895B RID: 35163
		// (get) Token: 0x0601917A RID: 102778 RVA: 0x002ED380 File Offset: 0x002EB580
		// (set) Token: 0x0601917B RID: 102779 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(23, "rightChars")]
		public Int32Value RightChars
		{
			get
			{
				return (Int32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700895C RID: 35164
		// (get) Token: 0x0601917C RID: 102780 RVA: 0x002D14EB File Offset: 0x002CF6EB
		// (set) Token: 0x0601917D RID: 102781 RVA: 0x002BD516 File Offset: 0x002BB716
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "endChars")]
		public Int32Value EndCharacters
		{
			get
			{
				return (Int32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x1700895D RID: 35165
		// (get) Token: 0x0601917E RID: 102782 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0601917F RID: 102783 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(23, "hanging")]
		public StringValue Hanging
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x1700895E RID: 35166
		// (get) Token: 0x06019180 RID: 102784 RVA: 0x002D14FA File Offset: 0x002CF6FA
		// (set) Token: 0x06019181 RID: 102785 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(23, "hangingChars")]
		public Int32Value HangingChars
		{
			get
			{
				return (Int32Value)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x1700895F RID: 35167
		// (get) Token: 0x06019182 RID: 102786 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x06019183 RID: 102787 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(23, "firstLine")]
		public StringValue FirstLine
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17008960 RID: 35168
		// (get) Token: 0x06019184 RID: 102788 RVA: 0x002ED56A File Offset: 0x002EB76A
		// (set) Token: 0x06019185 RID: 102789 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(23, "firstLineChars")]
		public Int32Value FirstLineChars
		{
			get
			{
				return (Int32Value)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x06019187 RID: 102791 RVA: 0x00346388 File Offset: 0x00344588
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "left" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "start" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "leftChars" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "startChars" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "right" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "end" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "rightChars" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "endChars" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "hanging" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "hangingChars" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "firstLine" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "firstLineChars" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019188 RID: 102792 RVA: 0x003464BD File Offset: 0x003446BD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Indentation>(deep);
		}

		// Token: 0x0400A6F3 RID: 42739
		private const string tagName = "ind";

		// Token: 0x0400A6F4 RID: 42740
		private const byte tagNsId = 23;

		// Token: 0x0400A6F5 RID: 42741
		internal const int ElementTypeIdConst = 11514;

		// Token: 0x0400A6F6 RID: 42742
		private static string[] attributeTagNames = new string[]
		{
			"left", "start", "leftChars", "startChars", "right", "end", "rightChars", "endChars", "hanging", "hangingChars",
			"firstLine", "firstLineChars"
		};

		// Token: 0x0400A6F7 RID: 42743
		private static byte[] attributeNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23
		};
	}
}
