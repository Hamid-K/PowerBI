using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002207 RID: 8711
	[GeneratedCode("DomGen", "2.0")]
	internal class Extrusion : OpenXmlLeafElement
	{
		// Token: 0x17003867 RID: 14439
		// (get) Token: 0x0600DE65 RID: 56933 RVA: 0x002BE190 File Offset: 0x002BC390
		public override string LocalName
		{
			get
			{
				return "extrusion";
			}
		}

		// Token: 0x17003868 RID: 14440
		// (get) Token: 0x0600DE66 RID: 56934 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x17003869 RID: 14441
		// (get) Token: 0x0600DE67 RID: 56935 RVA: 0x002BE197 File Offset: 0x002BC397
		internal override int ElementTypeId
		{
			get
			{
				return 12405;
			}
		}

		// Token: 0x0600DE68 RID: 56936 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700386A RID: 14442
		// (get) Token: 0x0600DE69 RID: 56937 RVA: 0x002BE19E File Offset: 0x002BC39E
		internal override string[] AttributeTagNames
		{
			get
			{
				return Extrusion.attributeTagNames;
			}
		}

		// Token: 0x1700386B RID: 14443
		// (get) Token: 0x0600DE6A RID: 56938 RVA: 0x002BE1A5 File Offset: 0x002BC3A5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Extrusion.attributeNamespaceIds;
			}
		}

		// Token: 0x1700386C RID: 14444
		// (get) Token: 0x0600DE6B RID: 56939 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DE6C RID: 56940 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(26, "ext")]
		public EnumValue<ExtensionHandlingBehaviorValues> Extension
		{
			get
			{
				return (EnumValue<ExtensionHandlingBehaviorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700386D RID: 14445
		// (get) Token: 0x0600DE6D RID: 56941 RVA: 0x002BDACB File Offset: 0x002BBCCB
		// (set) Token: 0x0600DE6E RID: 56942 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "on")]
		public TrueFalseValue On
		{
			get
			{
				return (TrueFalseValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700386E RID: 14446
		// (get) Token: 0x0600DE6F RID: 56943 RVA: 0x002BE1AC File Offset: 0x002BC3AC
		// (set) Token: 0x0600DE70 RID: 56944 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "type")]
		public EnumValue<ExtrusionValues> Type
		{
			get
			{
				return (EnumValue<ExtrusionValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700386F RID: 14447
		// (get) Token: 0x0600DE71 RID: 56945 RVA: 0x002BE1BB File Offset: 0x002BC3BB
		// (set) Token: 0x0600DE72 RID: 56946 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "render")]
		public EnumValue<ExtrusionRenderValues> Render
		{
			get
			{
				return (EnumValue<ExtrusionRenderValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17003870 RID: 14448
		// (get) Token: 0x0600DE73 RID: 56947 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600DE74 RID: 56948 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "viewpointorigin")]
		public StringValue ViewpointOrigin
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

		// Token: 0x17003871 RID: 14449
		// (get) Token: 0x0600DE75 RID: 56949 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600DE76 RID: 56950 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "viewpoint")]
		public StringValue Viewpoint
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

		// Token: 0x17003872 RID: 14450
		// (get) Token: 0x0600DE77 RID: 56951 RVA: 0x002BE1CA File Offset: 0x002BC3CA
		// (set) Token: 0x0600DE78 RID: 56952 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "skewangle")]
		public SingleValue SkewAngle
		{
			get
			{
				return (SingleValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17003873 RID: 14451
		// (get) Token: 0x0600DE79 RID: 56953 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600DE7A RID: 56954 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "skewamt")]
		public StringValue SkewAmount
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

		// Token: 0x17003874 RID: 14452
		// (get) Token: 0x0600DE7B RID: 56955 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600DE7C RID: 56956 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "foredepth")]
		public StringValue ForceDepth
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

		// Token: 0x17003875 RID: 14453
		// (get) Token: 0x0600DE7D RID: 56957 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600DE7E RID: 56958 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "backdepth")]
		public StringValue BackDepth
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

		// Token: 0x17003876 RID: 14454
		// (get) Token: 0x0600DE7F RID: 56959 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600DE80 RID: 56960 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "orientation")]
		public StringValue Orientation
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

		// Token: 0x17003877 RID: 14455
		// (get) Token: 0x0600DE81 RID: 56961 RVA: 0x002BE1D9 File Offset: 0x002BC3D9
		// (set) Token: 0x0600DE82 RID: 56962 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "orientationangle")]
		public SingleValue OrientationAngle
		{
			get
			{
				return (SingleValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17003878 RID: 14456
		// (get) Token: 0x0600DE83 RID: 56963 RVA: 0x002BE1E9 File Offset: 0x002BC3E9
		// (set) Token: 0x0600DE84 RID: 56964 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "lockrotationcenter")]
		public TrueFalseValue LockRotationCenter
		{
			get
			{
				return (TrueFalseValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17003879 RID: 14457
		// (get) Token: 0x0600DE85 RID: 56965 RVA: 0x002BE1F9 File Offset: 0x002BC3F9
		// (set) Token: 0x0600DE86 RID: 56966 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "autorotationcenter")]
		public TrueFalseValue AutoRotationCenter
		{
			get
			{
				return (TrueFalseValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x1700387A RID: 14458
		// (get) Token: 0x0600DE87 RID: 56967 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600DE88 RID: 56968 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "rotationcenter")]
		public StringValue RotationCenter
		{
			get
			{
				return (StringValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x1700387B RID: 14459
		// (get) Token: 0x0600DE89 RID: 56969 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600DE8A RID: 56970 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "rotationangle")]
		public StringValue RotationAngle
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

		// Token: 0x1700387C RID: 14460
		// (get) Token: 0x0600DE8B RID: 56971 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600DE8C RID: 56972 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "color")]
		public StringValue Color
		{
			get
			{
				return (StringValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x1700387D RID: 14461
		// (get) Token: 0x0600DE8D RID: 56973 RVA: 0x002BE269 File Offset: 0x002BC469
		// (set) Token: 0x0600DE8E RID: 56974 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "shininess")]
		public SingleValue Shininess
		{
			get
			{
				return (SingleValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x1700387E RID: 14462
		// (get) Token: 0x0600DE8F RID: 56975 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600DE90 RID: 56976 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "specularity")]
		public StringValue Specularity
		{
			get
			{
				return (StringValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x1700387F RID: 14463
		// (get) Token: 0x0600DE91 RID: 56977 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600DE92 RID: 56978 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "diffusity")]
		public StringValue Diffusity
		{
			get
			{
				return (StringValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17003880 RID: 14464
		// (get) Token: 0x0600DE93 RID: 56979 RVA: 0x002BE2BD File Offset: 0x002BC4BD
		// (set) Token: 0x0600DE94 RID: 56980 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "metal")]
		public TrueFalseValue Metal
		{
			get
			{
				return (TrueFalseValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x17003881 RID: 14465
		// (get) Token: 0x0600DE95 RID: 56981 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600DE96 RID: 56982 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "edge")]
		public StringValue Edge
		{
			get
			{
				return (StringValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x17003882 RID: 14466
		// (get) Token: 0x0600DE97 RID: 56983 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600DE98 RID: 56984 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "facet")]
		public StringValue Facet
		{
			get
			{
				return (StringValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17003883 RID: 14467
		// (get) Token: 0x0600DE99 RID: 56985 RVA: 0x002BE311 File Offset: 0x002BC511
		// (set) Token: 0x0600DE9A RID: 56986 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "lightface")]
		public TrueFalseValue LightFace
		{
			get
			{
				return (TrueFalseValue)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x17003884 RID: 14468
		// (get) Token: 0x0600DE9B RID: 56987 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600DE9C RID: 56988 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "brightness")]
		public StringValue Brightness
		{
			get
			{
				return (StringValue)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x17003885 RID: 14469
		// (get) Token: 0x0600DE9D RID: 56989 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600DE9E RID: 56990 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "lightposition")]
		public StringValue LightPosition
		{
			get
			{
				return (StringValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x17003886 RID: 14470
		// (get) Token: 0x0600DE9F RID: 56991 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600DEA0 RID: 56992 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "lightlevel")]
		public StringValue LightLevel
		{
			get
			{
				return (StringValue)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x17003887 RID: 14471
		// (get) Token: 0x0600DEA1 RID: 56993 RVA: 0x002BE381 File Offset: 0x002BC581
		// (set) Token: 0x0600DEA2 RID: 56994 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "lightharsh")]
		public TrueFalseValue LightHarsh
		{
			get
			{
				return (TrueFalseValue)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x17003888 RID: 14472
		// (get) Token: 0x0600DEA3 RID: 56995 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600DEA4 RID: 56996 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "lightposition2")]
		public StringValue LightPosition2
		{
			get
			{
				return (StringValue)base.Attributes[28];
			}
			set
			{
				base.Attributes[28] = value;
			}
		}

		// Token: 0x17003889 RID: 14473
		// (get) Token: 0x0600DEA5 RID: 56997 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600DEA6 RID: 56998 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "lightlevel2")]
		public StringValue LightLevel2
		{
			get
			{
				return (StringValue)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x1700388A RID: 14474
		// (get) Token: 0x0600DEA7 RID: 56999 RVA: 0x002BE3D5 File Offset: 0x002BC5D5
		// (set) Token: 0x0600DEA8 RID: 57000 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "lightharsh2")]
		public TrueFalseValue LightHarsh2
		{
			get
			{
				return (TrueFalseValue)base.Attributes[30];
			}
			set
			{
				base.Attributes[30] = value;
			}
		}

		// Token: 0x0600DEAA RID: 57002 RVA: 0x002BE3F4 File Offset: 0x002BC5F4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			if (namespaceId == 0 && "on" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<ExtrusionValues>();
			}
			if (namespaceId == 0 && "render" == name)
			{
				return new EnumValue<ExtrusionRenderValues>();
			}
			if (namespaceId == 0 && "viewpointorigin" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "viewpoint" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "skewangle" == name)
			{
				return new SingleValue();
			}
			if (namespaceId == 0 && "skewamt" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "foredepth" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "backdepth" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "orientation" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "orientationangle" == name)
			{
				return new SingleValue();
			}
			if (namespaceId == 0 && "lockrotationcenter" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "autorotationcenter" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "rotationcenter" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "rotationangle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "color" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "shininess" == name)
			{
				return new SingleValue();
			}
			if (namespaceId == 0 && "specularity" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "diffusity" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "metal" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "edge" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "facet" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "lightface" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "brightness" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "lightposition" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "lightlevel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "lightharsh" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "lightposition2" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "lightlevel2" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "lightharsh2" == name)
			{
				return new TrueFalseValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DEAB RID: 57003 RVA: 0x002BE6B5 File Offset: 0x002BC8B5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Extrusion>(deep);
		}

		// Token: 0x0600DEAC RID: 57004 RVA: 0x002BE6C0 File Offset: 0x002BC8C0
		// Note: this type is marked as 'beforefieldinit'.
		static Extrusion()
		{
			byte[] array = new byte[31];
			array[0] = 26;
			Extrusion.attributeNamespaceIds = array;
		}

		// Token: 0x04006D72 RID: 28018
		private const string tagName = "extrusion";

		// Token: 0x04006D73 RID: 28019
		private const byte tagNsId = 27;

		// Token: 0x04006D74 RID: 28020
		internal const int ElementTypeIdConst = 12405;

		// Token: 0x04006D75 RID: 28021
		private static string[] attributeTagNames = new string[]
		{
			"ext", "on", "type", "render", "viewpointorigin", "viewpoint", "skewangle", "skewamt", "foredepth", "backdepth",
			"orientation", "orientationangle", "lockrotationcenter", "autorotationcenter", "rotationcenter", "rotationangle", "color", "shininess", "specularity", "diffusity",
			"metal", "edge", "facet", "lightface", "brightness", "lightposition", "lightlevel", "lightharsh", "lightposition2", "lightlevel2",
			"lightharsh2"
		};

		// Token: 0x04006D76 RID: 28022
		private static byte[] attributeNamespaceIds;
	}
}
