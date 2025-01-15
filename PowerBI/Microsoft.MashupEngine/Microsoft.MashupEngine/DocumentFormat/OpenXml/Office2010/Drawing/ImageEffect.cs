using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002377 RID: 9079
	[ChildElementInfo(typeof(ArtisticWatercolorSponge), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticGlass), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticGlowEdges), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticLightScreen), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticLineDrawing), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticMarker), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticMosaicBubbles), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticPaintStrokes), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticPaintBrush), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticPastelsSmooth), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticPencilGrayscale), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticPencilSketch), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticPhotocopy), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticPlasticWrap), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticTexturizer), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackgroundRemoval), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BrightnessContrast), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ColorTemperature), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Saturation), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SharpenSoften), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticGlowDiffused), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticBlur), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticCement), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticChalkSketch), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticCrisscrossEtching), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticCutout), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ArtisticFilmGrain), FileFormatVersions.Office2010)]
	internal class ImageEffect : OpenXmlCompositeElement
	{
		// Token: 0x17004AF2 RID: 19186
		// (get) Token: 0x06010599 RID: 66969 RVA: 0x002E25CF File Offset: 0x002E07CF
		public override string LocalName
		{
			get
			{
				return "imgEffect";
			}
		}

		// Token: 0x17004AF3 RID: 19187
		// (get) Token: 0x0601059A RID: 66970 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004AF4 RID: 19188
		// (get) Token: 0x0601059B RID: 66971 RVA: 0x002E25D6 File Offset: 0x002E07D6
		internal override int ElementTypeId
		{
			get
			{
				return 12762;
			}
		}

		// Token: 0x0601059C RID: 66972 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004AF5 RID: 19189
		// (get) Token: 0x0601059D RID: 66973 RVA: 0x002E25DD File Offset: 0x002E07DD
		internal override string[] AttributeTagNames
		{
			get
			{
				return ImageEffect.attributeTagNames;
			}
		}

		// Token: 0x17004AF6 RID: 19190
		// (get) Token: 0x0601059E RID: 66974 RVA: 0x002E25E4 File Offset: 0x002E07E4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ImageEffect.attributeNamespaceIds;
			}
		}

		// Token: 0x17004AF7 RID: 19191
		// (get) Token: 0x0601059F RID: 66975 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060105A0 RID: 66976 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x060105A1 RID: 66977 RVA: 0x00293ECF File Offset: 0x002920CF
		public ImageEffect()
		{
		}

		// Token: 0x060105A2 RID: 66978 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ImageEffect(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060105A3 RID: 66979 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ImageEffect(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060105A4 RID: 66980 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ImageEffect(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060105A5 RID: 66981 RVA: 0x002E25EC File Offset: 0x002E07EC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "artisticBlur" == name)
			{
				return new ArtisticBlur();
			}
			if (48 == namespaceId && "artisticCement" == name)
			{
				return new ArtisticCement();
			}
			if (48 == namespaceId && "artisticChalkSketch" == name)
			{
				return new ArtisticChalkSketch();
			}
			if (48 == namespaceId && "artisticCrisscrossEtching" == name)
			{
				return new ArtisticCrisscrossEtching();
			}
			if (48 == namespaceId && "artisticCutout" == name)
			{
				return new ArtisticCutout();
			}
			if (48 == namespaceId && "artisticFilmGrain" == name)
			{
				return new ArtisticFilmGrain();
			}
			if (48 == namespaceId && "artisticGlass" == name)
			{
				return new ArtisticGlass();
			}
			if (48 == namespaceId && "artisticGlowDiffused" == name)
			{
				return new ArtisticGlowDiffused();
			}
			if (48 == namespaceId && "artisticGlowEdges" == name)
			{
				return new ArtisticGlowEdges();
			}
			if (48 == namespaceId && "artisticLightScreen" == name)
			{
				return new ArtisticLightScreen();
			}
			if (48 == namespaceId && "artisticLineDrawing" == name)
			{
				return new ArtisticLineDrawing();
			}
			if (48 == namespaceId && "artisticMarker" == name)
			{
				return new ArtisticMarker();
			}
			if (48 == namespaceId && "artisticMosiaicBubbles" == name)
			{
				return new ArtisticMosaicBubbles();
			}
			if (48 == namespaceId && "artisticPaintStrokes" == name)
			{
				return new ArtisticPaintStrokes();
			}
			if (48 == namespaceId && "artisticPaintBrush" == name)
			{
				return new ArtisticPaintBrush();
			}
			if (48 == namespaceId && "artisticPastelsSmooth" == name)
			{
				return new ArtisticPastelsSmooth();
			}
			if (48 == namespaceId && "artisticPencilGrayscale" == name)
			{
				return new ArtisticPencilGrayscale();
			}
			if (48 == namespaceId && "artisticPencilSketch" == name)
			{
				return new ArtisticPencilSketch();
			}
			if (48 == namespaceId && "artisticPhotocopy" == name)
			{
				return new ArtisticPhotocopy();
			}
			if (48 == namespaceId && "artisticPlasticWrap" == name)
			{
				return new ArtisticPlasticWrap();
			}
			if (48 == namespaceId && "artisticTexturizer" == name)
			{
				return new ArtisticTexturizer();
			}
			if (48 == namespaceId && "artisticWatercolorSponge" == name)
			{
				return new ArtisticWatercolorSponge();
			}
			if (48 == namespaceId && "backgroundRemoval" == name)
			{
				return new BackgroundRemoval();
			}
			if (48 == namespaceId && "brightnessContrast" == name)
			{
				return new BrightnessContrast();
			}
			if (48 == namespaceId && "colorTemperature" == name)
			{
				return new ColorTemperature();
			}
			if (48 == namespaceId && "saturation" == name)
			{
				return new Saturation();
			}
			if (48 == namespaceId && "sharpenSoften" == name)
			{
				return new SharpenSoften();
			}
			return null;
		}

		// Token: 0x17004AF8 RID: 19192
		// (get) Token: 0x060105A6 RID: 66982 RVA: 0x002E2882 File Offset: 0x002E0A82
		internal override string[] ElementTagNames
		{
			get
			{
				return ImageEffect.eleTagNames;
			}
		}

		// Token: 0x17004AF9 RID: 19193
		// (get) Token: 0x060105A7 RID: 66983 RVA: 0x002E2889 File Offset: 0x002E0A89
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ImageEffect.eleNamespaceIds;
			}
		}

		// Token: 0x17004AFA RID: 19194
		// (get) Token: 0x060105A8 RID: 66984 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17004AFB RID: 19195
		// (get) Token: 0x060105A9 RID: 66985 RVA: 0x002E2890 File Offset: 0x002E0A90
		// (set) Token: 0x060105AA RID: 66986 RVA: 0x002E2899 File Offset: 0x002E0A99
		public ArtisticBlur ArtisticBlur
		{
			get
			{
				return base.GetElement<ArtisticBlur>(0);
			}
			set
			{
				base.SetElement<ArtisticBlur>(0, value);
			}
		}

		// Token: 0x17004AFC RID: 19196
		// (get) Token: 0x060105AB RID: 66987 RVA: 0x002E28A3 File Offset: 0x002E0AA3
		// (set) Token: 0x060105AC RID: 66988 RVA: 0x002E28AC File Offset: 0x002E0AAC
		public ArtisticCement ArtisticCement
		{
			get
			{
				return base.GetElement<ArtisticCement>(1);
			}
			set
			{
				base.SetElement<ArtisticCement>(1, value);
			}
		}

		// Token: 0x17004AFD RID: 19197
		// (get) Token: 0x060105AD RID: 66989 RVA: 0x002E28B6 File Offset: 0x002E0AB6
		// (set) Token: 0x060105AE RID: 66990 RVA: 0x002E28BF File Offset: 0x002E0ABF
		public ArtisticChalkSketch ArtisticChalkSketch
		{
			get
			{
				return base.GetElement<ArtisticChalkSketch>(2);
			}
			set
			{
				base.SetElement<ArtisticChalkSketch>(2, value);
			}
		}

		// Token: 0x17004AFE RID: 19198
		// (get) Token: 0x060105AF RID: 66991 RVA: 0x002E28C9 File Offset: 0x002E0AC9
		// (set) Token: 0x060105B0 RID: 66992 RVA: 0x002E28D2 File Offset: 0x002E0AD2
		public ArtisticCrisscrossEtching ArtisticCrisscrossEtching
		{
			get
			{
				return base.GetElement<ArtisticCrisscrossEtching>(3);
			}
			set
			{
				base.SetElement<ArtisticCrisscrossEtching>(3, value);
			}
		}

		// Token: 0x17004AFF RID: 19199
		// (get) Token: 0x060105B1 RID: 66993 RVA: 0x002E28DC File Offset: 0x002E0ADC
		// (set) Token: 0x060105B2 RID: 66994 RVA: 0x002E28E5 File Offset: 0x002E0AE5
		public ArtisticCutout ArtisticCutout
		{
			get
			{
				return base.GetElement<ArtisticCutout>(4);
			}
			set
			{
				base.SetElement<ArtisticCutout>(4, value);
			}
		}

		// Token: 0x17004B00 RID: 19200
		// (get) Token: 0x060105B3 RID: 66995 RVA: 0x002E28EF File Offset: 0x002E0AEF
		// (set) Token: 0x060105B4 RID: 66996 RVA: 0x002E28F8 File Offset: 0x002E0AF8
		public ArtisticFilmGrain ArtisticFilmGrain
		{
			get
			{
				return base.GetElement<ArtisticFilmGrain>(5);
			}
			set
			{
				base.SetElement<ArtisticFilmGrain>(5, value);
			}
		}

		// Token: 0x17004B01 RID: 19201
		// (get) Token: 0x060105B5 RID: 66997 RVA: 0x002E2902 File Offset: 0x002E0B02
		// (set) Token: 0x060105B6 RID: 66998 RVA: 0x002E290B File Offset: 0x002E0B0B
		public ArtisticGlass ArtisticGlass
		{
			get
			{
				return base.GetElement<ArtisticGlass>(6);
			}
			set
			{
				base.SetElement<ArtisticGlass>(6, value);
			}
		}

		// Token: 0x17004B02 RID: 19202
		// (get) Token: 0x060105B7 RID: 66999 RVA: 0x002E2915 File Offset: 0x002E0B15
		// (set) Token: 0x060105B8 RID: 67000 RVA: 0x002E291E File Offset: 0x002E0B1E
		public ArtisticGlowDiffused ArtisticGlowDiffused
		{
			get
			{
				return base.GetElement<ArtisticGlowDiffused>(7);
			}
			set
			{
				base.SetElement<ArtisticGlowDiffused>(7, value);
			}
		}

		// Token: 0x17004B03 RID: 19203
		// (get) Token: 0x060105B9 RID: 67001 RVA: 0x002E2928 File Offset: 0x002E0B28
		// (set) Token: 0x060105BA RID: 67002 RVA: 0x002E2931 File Offset: 0x002E0B31
		public ArtisticGlowEdges ArtisticGlowEdges
		{
			get
			{
				return base.GetElement<ArtisticGlowEdges>(8);
			}
			set
			{
				base.SetElement<ArtisticGlowEdges>(8, value);
			}
		}

		// Token: 0x17004B04 RID: 19204
		// (get) Token: 0x060105BB RID: 67003 RVA: 0x002E293B File Offset: 0x002E0B3B
		// (set) Token: 0x060105BC RID: 67004 RVA: 0x002E2945 File Offset: 0x002E0B45
		public ArtisticLightScreen ArtisticLightScreen
		{
			get
			{
				return base.GetElement<ArtisticLightScreen>(9);
			}
			set
			{
				base.SetElement<ArtisticLightScreen>(9, value);
			}
		}

		// Token: 0x17004B05 RID: 19205
		// (get) Token: 0x060105BD RID: 67005 RVA: 0x002E2950 File Offset: 0x002E0B50
		// (set) Token: 0x060105BE RID: 67006 RVA: 0x002E295A File Offset: 0x002E0B5A
		public ArtisticLineDrawing ArtisticLineDrawing
		{
			get
			{
				return base.GetElement<ArtisticLineDrawing>(10);
			}
			set
			{
				base.SetElement<ArtisticLineDrawing>(10, value);
			}
		}

		// Token: 0x17004B06 RID: 19206
		// (get) Token: 0x060105BF RID: 67007 RVA: 0x002E2965 File Offset: 0x002E0B65
		// (set) Token: 0x060105C0 RID: 67008 RVA: 0x002E296F File Offset: 0x002E0B6F
		public ArtisticMarker ArtisticMarker
		{
			get
			{
				return base.GetElement<ArtisticMarker>(11);
			}
			set
			{
				base.SetElement<ArtisticMarker>(11, value);
			}
		}

		// Token: 0x17004B07 RID: 19207
		// (get) Token: 0x060105C1 RID: 67009 RVA: 0x002E297A File Offset: 0x002E0B7A
		// (set) Token: 0x060105C2 RID: 67010 RVA: 0x002E2984 File Offset: 0x002E0B84
		public ArtisticMosaicBubbles ArtisticMosaicBubbles
		{
			get
			{
				return base.GetElement<ArtisticMosaicBubbles>(12);
			}
			set
			{
				base.SetElement<ArtisticMosaicBubbles>(12, value);
			}
		}

		// Token: 0x17004B08 RID: 19208
		// (get) Token: 0x060105C3 RID: 67011 RVA: 0x002E298F File Offset: 0x002E0B8F
		// (set) Token: 0x060105C4 RID: 67012 RVA: 0x002E2999 File Offset: 0x002E0B99
		public ArtisticPaintStrokes ArtisticPaintStrokes
		{
			get
			{
				return base.GetElement<ArtisticPaintStrokes>(13);
			}
			set
			{
				base.SetElement<ArtisticPaintStrokes>(13, value);
			}
		}

		// Token: 0x17004B09 RID: 19209
		// (get) Token: 0x060105C5 RID: 67013 RVA: 0x002E29A4 File Offset: 0x002E0BA4
		// (set) Token: 0x060105C6 RID: 67014 RVA: 0x002E29AE File Offset: 0x002E0BAE
		public ArtisticPaintBrush ArtisticPaintBrush
		{
			get
			{
				return base.GetElement<ArtisticPaintBrush>(14);
			}
			set
			{
				base.SetElement<ArtisticPaintBrush>(14, value);
			}
		}

		// Token: 0x17004B0A RID: 19210
		// (get) Token: 0x060105C7 RID: 67015 RVA: 0x002E29B9 File Offset: 0x002E0BB9
		// (set) Token: 0x060105C8 RID: 67016 RVA: 0x002E29C3 File Offset: 0x002E0BC3
		public ArtisticPastelsSmooth ArtisticPastelsSmooth
		{
			get
			{
				return base.GetElement<ArtisticPastelsSmooth>(15);
			}
			set
			{
				base.SetElement<ArtisticPastelsSmooth>(15, value);
			}
		}

		// Token: 0x17004B0B RID: 19211
		// (get) Token: 0x060105C9 RID: 67017 RVA: 0x002E29CE File Offset: 0x002E0BCE
		// (set) Token: 0x060105CA RID: 67018 RVA: 0x002E29D8 File Offset: 0x002E0BD8
		public ArtisticPencilGrayscale ArtisticPencilGrayscale
		{
			get
			{
				return base.GetElement<ArtisticPencilGrayscale>(16);
			}
			set
			{
				base.SetElement<ArtisticPencilGrayscale>(16, value);
			}
		}

		// Token: 0x17004B0C RID: 19212
		// (get) Token: 0x060105CB RID: 67019 RVA: 0x002E29E3 File Offset: 0x002E0BE3
		// (set) Token: 0x060105CC RID: 67020 RVA: 0x002E29ED File Offset: 0x002E0BED
		public ArtisticPencilSketch ArtisticPencilSketch
		{
			get
			{
				return base.GetElement<ArtisticPencilSketch>(17);
			}
			set
			{
				base.SetElement<ArtisticPencilSketch>(17, value);
			}
		}

		// Token: 0x17004B0D RID: 19213
		// (get) Token: 0x060105CD RID: 67021 RVA: 0x002E29F8 File Offset: 0x002E0BF8
		// (set) Token: 0x060105CE RID: 67022 RVA: 0x002E2A02 File Offset: 0x002E0C02
		public ArtisticPhotocopy ArtisticPhotocopy
		{
			get
			{
				return base.GetElement<ArtisticPhotocopy>(18);
			}
			set
			{
				base.SetElement<ArtisticPhotocopy>(18, value);
			}
		}

		// Token: 0x17004B0E RID: 19214
		// (get) Token: 0x060105CF RID: 67023 RVA: 0x002E2A0D File Offset: 0x002E0C0D
		// (set) Token: 0x060105D0 RID: 67024 RVA: 0x002E2A17 File Offset: 0x002E0C17
		public ArtisticPlasticWrap ArtisticPlasticWrap
		{
			get
			{
				return base.GetElement<ArtisticPlasticWrap>(19);
			}
			set
			{
				base.SetElement<ArtisticPlasticWrap>(19, value);
			}
		}

		// Token: 0x17004B0F RID: 19215
		// (get) Token: 0x060105D1 RID: 67025 RVA: 0x002E2A22 File Offset: 0x002E0C22
		// (set) Token: 0x060105D2 RID: 67026 RVA: 0x002E2A2C File Offset: 0x002E0C2C
		public ArtisticTexturizer ArtisticTexturizer
		{
			get
			{
				return base.GetElement<ArtisticTexturizer>(20);
			}
			set
			{
				base.SetElement<ArtisticTexturizer>(20, value);
			}
		}

		// Token: 0x17004B10 RID: 19216
		// (get) Token: 0x060105D3 RID: 67027 RVA: 0x002E2A37 File Offset: 0x002E0C37
		// (set) Token: 0x060105D4 RID: 67028 RVA: 0x002E2A41 File Offset: 0x002E0C41
		public ArtisticWatercolorSponge ArtisticWatercolorSponge
		{
			get
			{
				return base.GetElement<ArtisticWatercolorSponge>(21);
			}
			set
			{
				base.SetElement<ArtisticWatercolorSponge>(21, value);
			}
		}

		// Token: 0x17004B11 RID: 19217
		// (get) Token: 0x060105D5 RID: 67029 RVA: 0x002E2A4C File Offset: 0x002E0C4C
		// (set) Token: 0x060105D6 RID: 67030 RVA: 0x002E2A56 File Offset: 0x002E0C56
		public BackgroundRemoval BackgroundRemoval
		{
			get
			{
				return base.GetElement<BackgroundRemoval>(22);
			}
			set
			{
				base.SetElement<BackgroundRemoval>(22, value);
			}
		}

		// Token: 0x17004B12 RID: 19218
		// (get) Token: 0x060105D7 RID: 67031 RVA: 0x002E2A61 File Offset: 0x002E0C61
		// (set) Token: 0x060105D8 RID: 67032 RVA: 0x002E2A6B File Offset: 0x002E0C6B
		public BrightnessContrast BrightnessContrast
		{
			get
			{
				return base.GetElement<BrightnessContrast>(23);
			}
			set
			{
				base.SetElement<BrightnessContrast>(23, value);
			}
		}

		// Token: 0x17004B13 RID: 19219
		// (get) Token: 0x060105D9 RID: 67033 RVA: 0x002E2A76 File Offset: 0x002E0C76
		// (set) Token: 0x060105DA RID: 67034 RVA: 0x002E2A80 File Offset: 0x002E0C80
		public ColorTemperature ColorTemperature
		{
			get
			{
				return base.GetElement<ColorTemperature>(24);
			}
			set
			{
				base.SetElement<ColorTemperature>(24, value);
			}
		}

		// Token: 0x17004B14 RID: 19220
		// (get) Token: 0x060105DB RID: 67035 RVA: 0x002E2A8B File Offset: 0x002E0C8B
		// (set) Token: 0x060105DC RID: 67036 RVA: 0x002E2A95 File Offset: 0x002E0C95
		public Saturation Saturation
		{
			get
			{
				return base.GetElement<Saturation>(25);
			}
			set
			{
				base.SetElement<Saturation>(25, value);
			}
		}

		// Token: 0x17004B15 RID: 19221
		// (get) Token: 0x060105DD RID: 67037 RVA: 0x002E2AA0 File Offset: 0x002E0CA0
		// (set) Token: 0x060105DE RID: 67038 RVA: 0x002E2AAA File Offset: 0x002E0CAA
		public SharpenSoften SharpenSoften
		{
			get
			{
				return base.GetElement<SharpenSoften>(26);
			}
			set
			{
				base.SetElement<SharpenSoften>(26, value);
			}
		}

		// Token: 0x060105DF RID: 67039 RVA: 0x002E2AB5 File Offset: 0x002E0CB5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060105E0 RID: 67040 RVA: 0x002E2AD5 File Offset: 0x002E0CD5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ImageEffect>(deep);
		}

		// Token: 0x060105E1 RID: 67041 RVA: 0x002E2AE0 File Offset: 0x002E0CE0
		// Note: this type is marked as 'beforefieldinit'.
		static ImageEffect()
		{
			byte[] array = new byte[1];
			ImageEffect.attributeNamespaceIds = array;
			ImageEffect.eleTagNames = new string[]
			{
				"artisticBlur", "artisticCement", "artisticChalkSketch", "artisticCrisscrossEtching", "artisticCutout", "artisticFilmGrain", "artisticGlass", "artisticGlowDiffused", "artisticGlowEdges", "artisticLightScreen",
				"artisticLineDrawing", "artisticMarker", "artisticMosiaicBubbles", "artisticPaintStrokes", "artisticPaintBrush", "artisticPastelsSmooth", "artisticPencilGrayscale", "artisticPencilSketch", "artisticPhotocopy", "artisticPlasticWrap",
				"artisticTexturizer", "artisticWatercolorSponge", "backgroundRemoval", "brightnessContrast", "colorTemperature", "saturation", "sharpenSoften"
			};
			ImageEffect.eleNamespaceIds = new byte[]
			{
				48, 48, 48, 48, 48, 48, 48, 48, 48, 48,
				48, 48, 48, 48, 48, 48, 48, 48, 48, 48,
				48, 48, 48, 48, 48, 48, 48
			};
		}

		// Token: 0x04007446 RID: 29766
		private const string tagName = "imgEffect";

		// Token: 0x04007447 RID: 29767
		private const byte tagNsId = 48;

		// Token: 0x04007448 RID: 29768
		internal const int ElementTypeIdConst = 12762;

		// Token: 0x04007449 RID: 29769
		private static string[] attributeTagNames = new string[] { "visible" };

		// Token: 0x0400744A RID: 29770
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400744B RID: 29771
		private static readonly string[] eleTagNames;

		// Token: 0x0400744C RID: 29772
		private static readonly byte[] eleNamespaceIds;
	}
}
