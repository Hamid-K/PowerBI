using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002249 RID: 8777
	[GeneratedCode("DomGen", "2.0")]
	internal class TextPath : OpenXmlLeafElement
	{
		// Token: 0x170039FA RID: 14842
		// (get) Token: 0x0600E1B2 RID: 57778 RVA: 0x002C0F54 File Offset: 0x002BF154
		public override string LocalName
		{
			get
			{
				return "textpath";
			}
		}

		// Token: 0x170039FB RID: 14843
		// (get) Token: 0x0600E1B3 RID: 57779 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x170039FC RID: 14844
		// (get) Token: 0x0600E1B4 RID: 57780 RVA: 0x002C0F5B File Offset: 0x002BF15B
		internal override int ElementTypeId
		{
			get
			{
				return 12513;
			}
		}

		// Token: 0x0600E1B5 RID: 57781 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170039FD RID: 14845
		// (get) Token: 0x0600E1B6 RID: 57782 RVA: 0x002C0F62 File Offset: 0x002BF162
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextPath.attributeTagNames;
			}
		}

		// Token: 0x170039FE RID: 14846
		// (get) Token: 0x0600E1B7 RID: 57783 RVA: 0x002C0F69 File Offset: 0x002BF169
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextPath.attributeNamespaceIds;
			}
		}

		// Token: 0x170039FF RID: 14847
		// (get) Token: 0x0600E1B8 RID: 57784 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E1B9 RID: 57785 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17003A00 RID: 14848
		// (get) Token: 0x0600E1BA RID: 57786 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E1BB RID: 57787 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "style")]
		public StringValue Style
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

		// Token: 0x17003A01 RID: 14849
		// (get) Token: 0x0600E1BC RID: 57788 RVA: 0x002BDE2B File Offset: 0x002BC02B
		// (set) Token: 0x0600E1BD RID: 57789 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "on")]
		public TrueFalseValue On
		{
			get
			{
				return (TrueFalseValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003A02 RID: 14850
		// (get) Token: 0x0600E1BE RID: 57790 RVA: 0x002BD49F File Offset: 0x002BB69F
		// (set) Token: 0x0600E1BF RID: 57791 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "fitshape")]
		public TrueFalseValue FitShape
		{
			get
			{
				return (TrueFalseValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17003A03 RID: 14851
		// (get) Token: 0x0600E1C0 RID: 57792 RVA: 0x002BDAE9 File Offset: 0x002BBCE9
		// (set) Token: 0x0600E1C1 RID: 57793 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "fitpath")]
		public TrueFalseValue FitPath
		{
			get
			{
				return (TrueFalseValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17003A04 RID: 14852
		// (get) Token: 0x0600E1C2 RID: 57794 RVA: 0x002BD4D3 File Offset: 0x002BB6D3
		// (set) Token: 0x0600E1C3 RID: 57795 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "trim")]
		public TrueFalseValue Trim
		{
			get
			{
				return (TrueFalseValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17003A05 RID: 14853
		// (get) Token: 0x0600E1C4 RID: 57796 RVA: 0x002BDAF8 File Offset: 0x002BBCF8
		// (set) Token: 0x0600E1C5 RID: 57797 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "xscale")]
		public TrueFalseValue XScale
		{
			get
			{
				return (TrueFalseValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17003A06 RID: 14854
		// (get) Token: 0x0600E1C6 RID: 57798 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E1C7 RID: 57799 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "string")]
		public StringValue String
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x0600E1C9 RID: 57801 RVA: 0x002C0F70 File Offset: 0x002BF170
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "style" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "on" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "fitshape" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "fitpath" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "trim" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "xscale" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "string" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E1CA RID: 57802 RVA: 0x002C1035 File Offset: 0x002BF235
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextPath>(deep);
		}

		// Token: 0x0600E1CB RID: 57803 RVA: 0x002C1040 File Offset: 0x002BF240
		// Note: this type is marked as 'beforefieldinit'.
		static TextPath()
		{
			byte[] array = new byte[8];
			TextPath.attributeNamespaceIds = array;
		}

		// Token: 0x04006EB3 RID: 28339
		private const string tagName = "textpath";

		// Token: 0x04006EB4 RID: 28340
		private const byte tagNsId = 26;

		// Token: 0x04006EB5 RID: 28341
		internal const int ElementTypeIdConst = 12513;

		// Token: 0x04006EB6 RID: 28342
		private static string[] attributeTagNames = new string[] { "id", "style", "on", "fitshape", "fitpath", "trim", "xscale", "string" };

		// Token: 0x04006EB7 RID: 28343
		private static byte[] attributeNamespaceIds;
	}
}
