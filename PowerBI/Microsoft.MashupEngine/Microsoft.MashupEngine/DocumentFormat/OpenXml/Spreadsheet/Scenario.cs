using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BE1 RID: 11233
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(InputCells))]
	internal class Scenario : OpenXmlCompositeElement
	{
		// Token: 0x17007DCC RID: 32204
		// (get) Token: 0x060177E3 RID: 96227 RVA: 0x00337862 File Offset: 0x00335A62
		public override string LocalName
		{
			get
			{
				return "scenario";
			}
		}

		// Token: 0x17007DCD RID: 32205
		// (get) Token: 0x060177E4 RID: 96228 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007DCE RID: 32206
		// (get) Token: 0x060177E5 RID: 96229 RVA: 0x00337869 File Offset: 0x00335A69
		internal override int ElementTypeId
		{
			get
			{
				return 11205;
			}
		}

		// Token: 0x060177E6 RID: 96230 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007DCF RID: 32207
		// (get) Token: 0x060177E7 RID: 96231 RVA: 0x00337870 File Offset: 0x00335A70
		internal override string[] AttributeTagNames
		{
			get
			{
				return Scenario.attributeTagNames;
			}
		}

		// Token: 0x17007DD0 RID: 32208
		// (get) Token: 0x060177E8 RID: 96232 RVA: 0x00337877 File Offset: 0x00335A77
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Scenario.attributeNamespaceIds;
			}
		}

		// Token: 0x17007DD1 RID: 32209
		// (get) Token: 0x060177E9 RID: 96233 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060177EA RID: 96234 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x17007DD2 RID: 32210
		// (get) Token: 0x060177EB RID: 96235 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060177EC RID: 96236 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "locked")]
		public BooleanValue Locked
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007DD3 RID: 32211
		// (get) Token: 0x060177ED RID: 96237 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060177EE RID: 96238 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "hidden")]
		public BooleanValue Hidden
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007DD4 RID: 32212
		// (get) Token: 0x060177EF RID: 96239 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x060177F0 RID: 96240 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17007DD5 RID: 32213
		// (get) Token: 0x060177F1 RID: 96241 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x060177F2 RID: 96242 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "user")]
		public StringValue User
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

		// Token: 0x17007DD6 RID: 32214
		// (get) Token: 0x060177F3 RID: 96243 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x060177F4 RID: 96244 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "comment")]
		public StringValue Comment
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

		// Token: 0x060177F5 RID: 96245 RVA: 0x00293ECF File Offset: 0x002920CF
		public Scenario()
		{
		}

		// Token: 0x060177F6 RID: 96246 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Scenario(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060177F7 RID: 96247 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Scenario(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060177F8 RID: 96248 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Scenario(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060177F9 RID: 96249 RVA: 0x0033787E File Offset: 0x00335A7E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "inputCells" == name)
			{
				return new InputCells();
			}
			return null;
		}

		// Token: 0x060177FA RID: 96250 RVA: 0x0033789C File Offset: 0x00335A9C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "locked" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "hidden" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "user" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "comment" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060177FB RID: 96251 RVA: 0x00337935 File Offset: 0x00335B35
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Scenario>(deep);
		}

		// Token: 0x060177FC RID: 96252 RVA: 0x00337940 File Offset: 0x00335B40
		// Note: this type is marked as 'beforefieldinit'.
		static Scenario()
		{
			byte[] array = new byte[6];
			Scenario.attributeNamespaceIds = array;
		}

		// Token: 0x04009C85 RID: 40069
		private const string tagName = "scenario";

		// Token: 0x04009C86 RID: 40070
		private const byte tagNsId = 22;

		// Token: 0x04009C87 RID: 40071
		internal const int ElementTypeIdConst = 11205;

		// Token: 0x04009C88 RID: 40072
		private static string[] attributeTagNames = new string[] { "name", "locked", "hidden", "count", "user", "comment" };

		// Token: 0x04009C89 RID: 40073
		private static byte[] attributeNamespaceIds;
	}
}
