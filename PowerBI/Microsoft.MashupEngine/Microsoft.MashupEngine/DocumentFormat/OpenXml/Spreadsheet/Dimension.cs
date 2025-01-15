using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B82 RID: 11138
	[GeneratedCode("DomGen", "2.0")]
	internal class Dimension : OpenXmlLeafElement
	{
		// Token: 0x17007A49 RID: 31305
		// (get) Token: 0x06017062 RID: 94306 RVA: 0x00331D07 File Offset: 0x0032FF07
		public override string LocalName
		{
			get
			{
				return "dimension";
			}
		}

		// Token: 0x17007A4A RID: 31306
		// (get) Token: 0x06017063 RID: 94307 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007A4B RID: 31307
		// (get) Token: 0x06017064 RID: 94308 RVA: 0x00331D0E File Offset: 0x0032FF0E
		internal override int ElementTypeId
		{
			get
			{
				return 11116;
			}
		}

		// Token: 0x06017065 RID: 94309 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007A4C RID: 31308
		// (get) Token: 0x06017066 RID: 94310 RVA: 0x00331D15 File Offset: 0x0032FF15
		internal override string[] AttributeTagNames
		{
			get
			{
				return Dimension.attributeTagNames;
			}
		}

		// Token: 0x17007A4D RID: 31309
		// (get) Token: 0x06017067 RID: 94311 RVA: 0x00331D1C File Offset: 0x0032FF1C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Dimension.attributeNamespaceIds;
			}
		}

		// Token: 0x17007A4E RID: 31310
		// (get) Token: 0x06017068 RID: 94312 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06017069 RID: 94313 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "measure")]
		public BooleanValue Measure
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007A4F RID: 31311
		// (get) Token: 0x0601706A RID: 94314 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601706B RID: 94315 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x17007A50 RID: 31312
		// (get) Token: 0x0601706C RID: 94316 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601706D RID: 94317 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "uniqueName")]
		public StringValue UniqueName
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

		// Token: 0x17007A51 RID: 31313
		// (get) Token: 0x0601706E RID: 94318 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601706F RID: 94319 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "caption")]
		public StringValue Caption
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06017071 RID: 94321 RVA: 0x00331D24 File Offset: 0x0032FF24
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "measure" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "uniqueName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "caption" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017072 RID: 94322 RVA: 0x00331D91 File Offset: 0x0032FF91
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Dimension>(deep);
		}

		// Token: 0x06017073 RID: 94323 RVA: 0x00331D9C File Offset: 0x0032FF9C
		// Note: this type is marked as 'beforefieldinit'.
		static Dimension()
		{
			byte[] array = new byte[4];
			Dimension.attributeNamespaceIds = array;
		}

		// Token: 0x04009ACB RID: 39627
		private const string tagName = "dimension";

		// Token: 0x04009ACC RID: 39628
		private const byte tagNsId = 22;

		// Token: 0x04009ACD RID: 39629
		internal const int ElementTypeIdConst = 11116;

		// Token: 0x04009ACE RID: 39630
		private static string[] attributeTagNames = new string[] { "measure", "name", "uniqueName", "caption" };

		// Token: 0x04009ACF RID: 39631
		private static byte[] attributeNamespaceIds;
	}
}
