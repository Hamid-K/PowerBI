using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022C0 RID: 8896
	[ChildElementInfo(typeof(VisibleButton), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(VisibleToggleButton), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MenuRegular), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class SplitButtonRegular : OpenXmlCompositeElement
	{
		// Token: 0x17004250 RID: 16976
		// (get) Token: 0x0600F331 RID: 62257 RVA: 0x002C9F5F File Offset: 0x002C815F
		public override string LocalName
		{
			get
			{
				return "splitButton";
			}
		}

		// Token: 0x17004251 RID: 16977
		// (get) Token: 0x0600F332 RID: 62258 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004252 RID: 16978
		// (get) Token: 0x0600F333 RID: 62259 RVA: 0x002D2E61 File Offset: 0x002D1061
		internal override int ElementTypeId
		{
			get
			{
				return 13041;
			}
		}

		// Token: 0x0600F334 RID: 62260 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004253 RID: 16979
		// (get) Token: 0x0600F335 RID: 62261 RVA: 0x002D2E68 File Offset: 0x002D1068
		internal override string[] AttributeTagNames
		{
			get
			{
				return SplitButtonRegular.attributeTagNames;
			}
		}

		// Token: 0x17004254 RID: 16980
		// (get) Token: 0x0600F336 RID: 62262 RVA: 0x002D2E6F File Offset: 0x002D106F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SplitButtonRegular.attributeNamespaceIds;
			}
		}

		// Token: 0x17004255 RID: 16981
		// (get) Token: 0x0600F337 RID: 62263 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0600F338 RID: 62264 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x17004256 RID: 16982
		// (get) Token: 0x0600F339 RID: 62265 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F33A RID: 62266 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17004257 RID: 16983
		// (get) Token: 0x0600F33B RID: 62267 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F33C RID: 62268 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17004258 RID: 16984
		// (get) Token: 0x0600F33D RID: 62269 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F33E RID: 62270 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x17004259 RID: 16985
		// (get) Token: 0x0600F33F RID: 62271 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F340 RID: 62272 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x1700425A RID: 16986
		// (get) Token: 0x0600F341 RID: 62273 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F342 RID: 62274 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x1700425B RID: 16987
		// (get) Token: 0x0600F343 RID: 62275 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F344 RID: 62276 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700425C RID: 16988
		// (get) Token: 0x0600F345 RID: 62277 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F346 RID: 62278 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x1700425D RID: 16989
		// (get) Token: 0x0600F347 RID: 62279 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F348 RID: 62280 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x1700425E RID: 16990
		// (get) Token: 0x0600F349 RID: 62281 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F34A RID: 62282 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x1700425F RID: 16991
		// (get) Token: 0x0600F34B RID: 62283 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0600F34C RID: 62284 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17004260 RID: 16992
		// (get) Token: 0x0600F34D RID: 62285 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F34E RID: 62286 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17004261 RID: 16993
		// (get) Token: 0x0600F34F RID: 62287 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F350 RID: 62288 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17004262 RID: 16994
		// (get) Token: 0x0600F351 RID: 62289 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F352 RID: 62290 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17004263 RID: 16995
		// (get) Token: 0x0600F353 RID: 62291 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x0600F354 RID: 62292 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17004264 RID: 16996
		// (get) Token: 0x0600F355 RID: 62293 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F356 RID: 62294 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x0600F357 RID: 62295 RVA: 0x00293ECF File Offset: 0x002920CF
		public SplitButtonRegular()
		{
		}

		// Token: 0x0600F358 RID: 62296 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SplitButtonRegular(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F359 RID: 62297 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SplitButtonRegular(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F35A RID: 62298 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SplitButtonRegular(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F35B RID: 62299 RVA: 0x002D2E78 File Offset: 0x002D1078
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "button" == name)
			{
				return new VisibleButton();
			}
			if (57 == namespaceId && "toggleButton" == name)
			{
				return new VisibleToggleButton();
			}
			if (57 == namespaceId && "menu" == name)
			{
				return new MenuRegular();
			}
			return null;
		}

		// Token: 0x0600F35C RID: 62300 RVA: 0x002D2ED0 File Offset: 0x002D10D0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "enabled" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getEnabled" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "keytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getKeytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showLabel" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getShowLabel" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F35D RID: 62301 RVA: 0x002D3045 File Offset: 0x002D1245
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SplitButtonRegular>(deep);
		}

		// Token: 0x0600F35E RID: 62302 RVA: 0x002D3050 File Offset: 0x002D1250
		// Note: this type is marked as 'beforefieldinit'.
		static SplitButtonRegular()
		{
			byte[] array = new byte[16];
			SplitButtonRegular.attributeNamespaceIds = array;
		}

		// Token: 0x040070ED RID: 28909
		private const string tagName = "splitButton";

		// Token: 0x040070EE RID: 28910
		private const byte tagNsId = 57;

		// Token: 0x040070EF RID: 28911
		internal const int ElementTypeIdConst = 13041;

		// Token: 0x040070F0 RID: 28912
		private static string[] attributeTagNames = new string[]
		{
			"enabled", "getEnabled", "id", "idQ", "tag", "idMso", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ",
			"visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel"
		};

		// Token: 0x040070F1 RID: 28913
		private static byte[] attributeNamespaceIds;
	}
}
