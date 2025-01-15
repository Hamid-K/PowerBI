using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C08 RID: 11272
	[ChildElementInfo(typeof(GradientStop))]
	[GeneratedCode("DomGen", "2.0")]
	internal class GradientFill : OpenXmlCompositeElement
	{
		// Token: 0x17007F8B RID: 32651
		// (get) Token: 0x06017B9E RID: 97182 RVA: 0x0033A60B File Offset: 0x0033880B
		public override string LocalName
		{
			get
			{
				return "gradientFill";
			}
		}

		// Token: 0x17007F8C RID: 32652
		// (get) Token: 0x06017B9F RID: 97183 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F8D RID: 32653
		// (get) Token: 0x06017BA0 RID: 97184 RVA: 0x0033A612 File Offset: 0x00338812
		internal override int ElementTypeId
		{
			get
			{
				return 11251;
			}
		}

		// Token: 0x06017BA1 RID: 97185 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007F8E RID: 32654
		// (get) Token: 0x06017BA2 RID: 97186 RVA: 0x0033A619 File Offset: 0x00338819
		internal override string[] AttributeTagNames
		{
			get
			{
				return GradientFill.attributeTagNames;
			}
		}

		// Token: 0x17007F8F RID: 32655
		// (get) Token: 0x06017BA3 RID: 97187 RVA: 0x0033A620 File Offset: 0x00338820
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GradientFill.attributeNamespaceIds;
			}
		}

		// Token: 0x17007F90 RID: 32656
		// (get) Token: 0x06017BA4 RID: 97188 RVA: 0x0033A627 File Offset: 0x00338827
		// (set) Token: 0x06017BA5 RID: 97189 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<GradientValues> Type
		{
			get
			{
				return (EnumValue<GradientValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007F91 RID: 32657
		// (get) Token: 0x06017BA6 RID: 97190 RVA: 0x002E7DD4 File Offset: 0x002E5FD4
		// (set) Token: 0x06017BA7 RID: 97191 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "degree")]
		public DoubleValue Degree
		{
			get
			{
				return (DoubleValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007F92 RID: 32658
		// (get) Token: 0x06017BA8 RID: 97192 RVA: 0x002E7DE3 File Offset: 0x002E5FE3
		// (set) Token: 0x06017BA9 RID: 97193 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "left")]
		public DoubleValue Left
		{
			get
			{
				return (DoubleValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007F93 RID: 32659
		// (get) Token: 0x06017BAA RID: 97194 RVA: 0x002F66C2 File Offset: 0x002F48C2
		// (set) Token: 0x06017BAB RID: 97195 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "right")]
		public DoubleValue Right
		{
			get
			{
				return (DoubleValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007F94 RID: 32660
		// (get) Token: 0x06017BAC RID: 97196 RVA: 0x002E82DC File Offset: 0x002E64DC
		// (set) Token: 0x06017BAD RID: 97197 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "top")]
		public DoubleValue Top
		{
			get
			{
				return (DoubleValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007F95 RID: 32661
		// (get) Token: 0x06017BAE RID: 97198 RVA: 0x002F66D1 File Offset: 0x002F48D1
		// (set) Token: 0x06017BAF RID: 97199 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "bottom")]
		public DoubleValue Bottom
		{
			get
			{
				return (DoubleValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x06017BB0 RID: 97200 RVA: 0x00293ECF File Offset: 0x002920CF
		public GradientFill()
		{
		}

		// Token: 0x06017BB1 RID: 97201 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GradientFill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017BB2 RID: 97202 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GradientFill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017BB3 RID: 97203 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GradientFill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017BB4 RID: 97204 RVA: 0x0033A636 File Offset: 0x00338836
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "stop" == name)
			{
				return new GradientStop();
			}
			return null;
		}

		// Token: 0x06017BB5 RID: 97205 RVA: 0x0033A654 File Offset: 0x00338854
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<GradientValues>();
			}
			if (namespaceId == 0 && "degree" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "left" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "right" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "top" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "bottom" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017BB6 RID: 97206 RVA: 0x0033A6ED File Offset: 0x003388ED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GradientFill>(deep);
		}

		// Token: 0x06017BB7 RID: 97207 RVA: 0x0033A6F8 File Offset: 0x003388F8
		// Note: this type is marked as 'beforefieldinit'.
		static GradientFill()
		{
			byte[] array = new byte[6];
			GradientFill.attributeNamespaceIds = array;
		}

		// Token: 0x04009D57 RID: 40279
		private const string tagName = "gradientFill";

		// Token: 0x04009D58 RID: 40280
		private const byte tagNsId = 22;

		// Token: 0x04009D59 RID: 40281
		internal const int ElementTypeIdConst = 11251;

		// Token: 0x04009D5A RID: 40282
		private static string[] attributeTagNames = new string[] { "type", "degree", "left", "right", "top", "bottom" };

		// Token: 0x04009D5B RID: 40283
		private static byte[] attributeNamespaceIds;
	}
}
