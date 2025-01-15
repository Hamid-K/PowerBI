using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C9C RID: 11420
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DataValidation))]
	internal class DataValidations : OpenXmlCompositeElement
	{
		// Token: 0x17008427 RID: 33831
		// (get) Token: 0x0601862F RID: 99887 RVA: 0x002E5AF1 File Offset: 0x002E3CF1
		public override string LocalName
		{
			get
			{
				return "dataValidations";
			}
		}

		// Token: 0x17008428 RID: 33832
		// (get) Token: 0x06018630 RID: 99888 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008429 RID: 33833
		// (get) Token: 0x06018631 RID: 99889 RVA: 0x003412BB File Offset: 0x0033F4BB
		internal override int ElementTypeId
		{
			get
			{
				return 11400;
			}
		}

		// Token: 0x06018632 RID: 99890 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700842A RID: 33834
		// (get) Token: 0x06018633 RID: 99891 RVA: 0x003412C2 File Offset: 0x0033F4C2
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataValidations.attributeTagNames;
			}
		}

		// Token: 0x1700842B RID: 33835
		// (get) Token: 0x06018634 RID: 99892 RVA: 0x003412C9 File Offset: 0x0033F4C9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataValidations.attributeNamespaceIds;
			}
		}

		// Token: 0x1700842C RID: 33836
		// (get) Token: 0x06018635 RID: 99893 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06018636 RID: 99894 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "disablePrompts")]
		public BooleanValue DisablePrompts
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

		// Token: 0x1700842D RID: 33837
		// (get) Token: 0x06018637 RID: 99895 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06018638 RID: 99896 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "xWindow")]
		public UInt32Value XWindow
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700842E RID: 33838
		// (get) Token: 0x06018639 RID: 99897 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x0601863A RID: 99898 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "yWindow")]
		public UInt32Value YWindow
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700842F RID: 33839
		// (get) Token: 0x0601863B RID: 99899 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x0601863C RID: 99900 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0601863D RID: 99901 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataValidations()
		{
		}

		// Token: 0x0601863E RID: 99902 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataValidations(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601863F RID: 99903 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataValidations(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018640 RID: 99904 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataValidations(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018641 RID: 99905 RVA: 0x003412D0 File Offset: 0x0033F4D0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "dataValidation" == name)
			{
				return new DataValidation();
			}
			return null;
		}

		// Token: 0x06018642 RID: 99906 RVA: 0x003412EC File Offset: 0x0033F4EC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "disablePrompts" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "xWindow" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "yWindow" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018643 RID: 99907 RVA: 0x00341359 File Offset: 0x0033F559
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataValidations>(deep);
		}

		// Token: 0x06018644 RID: 99908 RVA: 0x00341364 File Offset: 0x0033F564
		// Note: this type is marked as 'beforefieldinit'.
		static DataValidations()
		{
			byte[] array = new byte[4];
			DataValidations.attributeNamespaceIds = array;
		}

		// Token: 0x0400A007 RID: 40967
		private const string tagName = "dataValidations";

		// Token: 0x0400A008 RID: 40968
		private const byte tagNsId = 22;

		// Token: 0x0400A009 RID: 40969
		internal const int ElementTypeIdConst = 11400;

		// Token: 0x0400A00A RID: 40970
		private static string[] attributeTagNames = new string[] { "disablePrompts", "xWindow", "yWindow", "count" };

		// Token: 0x0400A00B RID: 40971
		private static byte[] attributeNamespaceIds;
	}
}
