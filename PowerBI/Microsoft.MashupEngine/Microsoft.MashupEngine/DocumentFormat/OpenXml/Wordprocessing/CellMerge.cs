using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D3F RID: 11583
	[GeneratedCode("DomGen", "2.0")]
	internal class CellMerge : OpenXmlLeafElement
	{
		// Token: 0x17008644 RID: 34372
		// (get) Token: 0x06018B43 RID: 101187 RVA: 0x0034416F File Offset: 0x0034236F
		public override string LocalName
		{
			get
			{
				return "cellMerge";
			}
		}

		// Token: 0x17008645 RID: 34373
		// (get) Token: 0x06018B44 RID: 101188 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008646 RID: 34374
		// (get) Token: 0x06018B45 RID: 101189 RVA: 0x00344176 File Offset: 0x00342376
		internal override int ElementTypeId
		{
			get
			{
				return 11475;
			}
		}

		// Token: 0x06018B46 RID: 101190 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008647 RID: 34375
		// (get) Token: 0x06018B47 RID: 101191 RVA: 0x0034417D File Offset: 0x0034237D
		internal override string[] AttributeTagNames
		{
			get
			{
				return CellMerge.attributeTagNames;
			}
		}

		// Token: 0x17008648 RID: 34376
		// (get) Token: 0x06018B48 RID: 101192 RVA: 0x00344184 File Offset: 0x00342384
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CellMerge.attributeNamespaceIds;
			}
		}

		// Token: 0x17008649 RID: 34377
		// (get) Token: 0x06018B49 RID: 101193 RVA: 0x0034418B File Offset: 0x0034238B
		// (set) Token: 0x06018B4A RID: 101194 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "vMerge")]
		public EnumValue<VerticalMergeRevisionValues> VerticalMerge
		{
			get
			{
				return (EnumValue<VerticalMergeRevisionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700864A RID: 34378
		// (get) Token: 0x06018B4B RID: 101195 RVA: 0x0034419A File Offset: 0x0034239A
		// (set) Token: 0x06018B4C RID: 101196 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "vMergeOrig")]
		public EnumValue<VerticalMergeRevisionValues> VerticalMergeOriginal
		{
			get
			{
				return (EnumValue<VerticalMergeRevisionValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700864B RID: 34379
		// (get) Token: 0x06018B4D RID: 101197 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06018B4E RID: 101198 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "author")]
		public StringValue Author
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700864C RID: 34380
		// (get) Token: 0x06018B4F RID: 101199 RVA: 0x00335DB2 File Offset: 0x00333FB2
		// (set) Token: 0x06018B50 RID: 101200 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "date")]
		public DateTimeValue Date
		{
			get
			{
				return (DateTimeValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700864D RID: 34381
		// (get) Token: 0x06018B51 RID: 101201 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06018B52 RID: 101202 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "id")]
		public StringValue Id
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

		// Token: 0x06018B54 RID: 101204 RVA: 0x003441AC File Offset: 0x003423AC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "vMerge" == name)
			{
				return new EnumValue<VerticalMergeRevisionValues>();
			}
			if (23 == namespaceId && "vMergeOrig" == name)
			{
				return new EnumValue<VerticalMergeRevisionValues>();
			}
			if (23 == namespaceId && "author" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "date" == name)
			{
				return new DateTimeValue();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018B55 RID: 101205 RVA: 0x00344239 File Offset: 0x00342439
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellMerge>(deep);
		}

		// Token: 0x0400A429 RID: 42025
		private const string tagName = "cellMerge";

		// Token: 0x0400A42A RID: 42026
		private const byte tagNsId = 23;

		// Token: 0x0400A42B RID: 42027
		internal const int ElementTypeIdConst = 11475;

		// Token: 0x0400A42C RID: 42028
		private static string[] attributeTagNames = new string[] { "vMerge", "vMergeOrig", "author", "date", "id" };

		// Token: 0x0400A42D RID: 42029
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23 };
	}
}
