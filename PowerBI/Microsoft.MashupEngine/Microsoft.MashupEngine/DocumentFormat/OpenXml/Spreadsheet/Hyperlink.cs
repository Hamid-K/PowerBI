using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BDF RID: 11231
	[GeneratedCode("DomGen", "2.0")]
	internal class Hyperlink : OpenXmlLeafElement
	{
		// Token: 0x17007DB6 RID: 32182
		// (get) Token: 0x060177B6 RID: 96182 RVA: 0x002D9347 File Offset: 0x002D7547
		public override string LocalName
		{
			get
			{
				return "hyperlink";
			}
		}

		// Token: 0x17007DB7 RID: 32183
		// (get) Token: 0x060177B7 RID: 96183 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007DB8 RID: 32184
		// (get) Token: 0x060177B8 RID: 96184 RVA: 0x0033766D File Offset: 0x0033586D
		internal override int ElementTypeId
		{
			get
			{
				return 11203;
			}
		}

		// Token: 0x060177B9 RID: 96185 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007DB9 RID: 32185
		// (get) Token: 0x060177BA RID: 96186 RVA: 0x00337674 File Offset: 0x00335874
		internal override string[] AttributeTagNames
		{
			get
			{
				return Hyperlink.attributeTagNames;
			}
		}

		// Token: 0x17007DBA RID: 32186
		// (get) Token: 0x060177BB RID: 96187 RVA: 0x0033767B File Offset: 0x0033587B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Hyperlink.attributeNamespaceIds;
			}
		}

		// Token: 0x17007DBB RID: 32187
		// (get) Token: 0x060177BC RID: 96188 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060177BD RID: 96189 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x17007DBC RID: 32188
		// (get) Token: 0x060177BE RID: 96190 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060177BF RID: 96191 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x17007DBD RID: 32189
		// (get) Token: 0x060177C0 RID: 96192 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060177C1 RID: 96193 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "location")]
		public StringValue Location
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

		// Token: 0x17007DBE RID: 32190
		// (get) Token: 0x060177C2 RID: 96194 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x060177C3 RID: 96195 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "tooltip")]
		public StringValue Tooltip
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

		// Token: 0x17007DBF RID: 32191
		// (get) Token: 0x060177C4 RID: 96196 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x060177C5 RID: 96197 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "display")]
		public StringValue Display
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

		// Token: 0x060177C7 RID: 96199 RVA: 0x00337684 File Offset: 0x00335884
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "location" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tooltip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "display" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060177C8 RID: 96200 RVA: 0x00337709 File Offset: 0x00335909
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Hyperlink>(deep);
		}

		// Token: 0x060177C9 RID: 96201 RVA: 0x00337714 File Offset: 0x00335914
		// Note: this type is marked as 'beforefieldinit'.
		static Hyperlink()
		{
			byte[] array = new byte[5];
			array[1] = 19;
			Hyperlink.attributeNamespaceIds = array;
		}

		// Token: 0x04009C79 RID: 40057
		private const string tagName = "hyperlink";

		// Token: 0x04009C7A RID: 40058
		private const byte tagNsId = 22;

		// Token: 0x04009C7B RID: 40059
		internal const int ElementTypeIdConst = 11203;

		// Token: 0x04009C7C RID: 40060
		private static string[] attributeTagNames = new string[] { "ref", "id", "location", "tooltip", "display" };

		// Token: 0x04009C7D RID: 40061
		private static byte[] attributeNamespaceIds;
	}
}
