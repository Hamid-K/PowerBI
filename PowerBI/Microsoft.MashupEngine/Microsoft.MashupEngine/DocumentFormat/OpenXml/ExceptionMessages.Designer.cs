using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020020FA RID: 8442
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	internal class ExceptionMessages
	{
		// Token: 0x0600CFAD RID: 53165 RVA: 0x000020FD File Offset: 0x000002FD
		internal ExceptionMessages()
		{
		}

		// Token: 0x170031E7 RID: 12775
		// (get) Token: 0x0600CFAE RID: 53166 RVA: 0x00295008 File Offset: 0x00293208
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(ExceptionMessages.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("DocumentFormat.OpenXml.ExceptionMessages", typeof(ExceptionMessages).Assembly);
					ExceptionMessages.resourceMan = resourceManager;
				}
				return ExceptionMessages.resourceMan;
			}
		}

		// Token: 0x170031E8 RID: 12776
		// (get) Token: 0x0600CFAF RID: 53167 RVA: 0x00295047 File Offset: 0x00293247
		// (set) Token: 0x0600CFB0 RID: 53168 RVA: 0x0029504E File Offset: 0x0029324E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return ExceptionMessages.resourceCulture;
			}
			set
			{
				ExceptionMessages.resourceCulture = value;
			}
		}

		// Token: 0x170031E9 RID: 12777
		// (get) Token: 0x0600CFB1 RID: 53169 RVA: 0x00295056 File Offset: 0x00293256
		internal static string AddedPartIsNotAllowed
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("AddedPartIsNotAllowed", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031EA RID: 12778
		// (get) Token: 0x0600CFB2 RID: 53170 RVA: 0x0029506C File Offset: 0x0029326C
		internal static string CannotChangeDocumentType
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("CannotChangeDocumentType", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031EB RID: 12779
		// (get) Token: 0x0600CFB3 RID: 53171 RVA: 0x00295082 File Offset: 0x00293282
		internal static string CannotChangeDocumentTypeSerious
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("CannotChangeDocumentTypeSerious", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031EC RID: 12780
		// (get) Token: 0x0600CFB4 RID: 53172 RVA: 0x00295098 File Offset: 0x00293298
		internal static string CannotFindAttribute
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("CannotFindAttribute", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031ED RID: 12781
		// (get) Token: 0x0600CFB5 RID: 53173 RVA: 0x002950AE File Offset: 0x002932AE
		internal static string CannotLoadRootElement
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("CannotLoadRootElement", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031EE RID: 12782
		// (get) Token: 0x0600CFB6 RID: 53174 RVA: 0x002950C4 File Offset: 0x002932C4
		internal static string CannotReloadDomTreeWithoutAssociatedPart
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("CannotReloadDomTreeWithoutAssociatedPart", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031EF RID: 12783
		// (get) Token: 0x0600CFB7 RID: 53175 RVA: 0x002950DA File Offset: 0x002932DA
		internal static string CannotSaveDomTreeWithoutAssociatedPart
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("CannotSaveDomTreeWithoutAssociatedPart", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031F0 RID: 12784
		// (get) Token: 0x0600CFB8 RID: 53176 RVA: 0x002950F0 File Offset: 0x002932F0
		internal static string CannotSetAttribute
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("CannotSetAttribute", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031F1 RID: 12785
		// (get) Token: 0x0600CFB9 RID: 53177 RVA: 0x00295106 File Offset: 0x00293306
		internal static string CannotValidateAcbElement
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("CannotValidateAcbElement", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031F2 RID: 12786
		// (get) Token: 0x0600CFBA RID: 53178 RVA: 0x0029511C File Offset: 0x0029331C
		internal static string CannotValidateMiscNode
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("CannotValidateMiscNode", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031F3 RID: 12787
		// (get) Token: 0x0600CFBB RID: 53179 RVA: 0x00295132 File Offset: 0x00293332
		internal static string CannotValidateUnknownElement
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("CannotValidateUnknownElement", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031F4 RID: 12788
		// (get) Token: 0x0600CFBC RID: 53180 RVA: 0x00295148 File Offset: 0x00293348
		internal static string CycleReference
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("CycleReference", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031F5 RID: 12789
		// (get) Token: 0x0600CFBD RID: 53181 RVA: 0x0029515E File Offset: 0x0029335E
		internal static string DataPartIsInUse
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("DataPartIsInUse", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031F6 RID: 12790
		// (get) Token: 0x0600CFBE RID: 53182 RVA: 0x00295174 File Offset: 0x00293374
		internal static string DataPartReferenceIsNotAllowed
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("DataPartReferenceIsNotAllowed", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031F7 RID: 12791
		// (get) Token: 0x0600CFBF RID: 53183 RVA: 0x0029518A File Offset: 0x0029338A
		internal static string DocumentFileFormatVersionMismatch
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("DocumentFileFormatVersionMismatch", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031F8 RID: 12792
		// (get) Token: 0x0600CFC0 RID: 53184 RVA: 0x002951A0 File Offset: 0x002933A0
		internal static string DocumentTooBig
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("DocumentTooBig", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031F9 RID: 12793
		// (get) Token: 0x0600CFC1 RID: 53185 RVA: 0x002951B6 File Offset: 0x002933B6
		internal static string DuplicatedPrefix
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("DuplicatedPrefix", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031FA RID: 12794
		// (get) Token: 0x0600CFC2 RID: 53186 RVA: 0x002951CC File Offset: 0x002933CC
		internal static string ElementIsNotChild
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ElementIsNotChild", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031FB RID: 12795
		// (get) Token: 0x0600CFC3 RID: 53187 RVA: 0x002951E2 File Offset: 0x002933E2
		internal static string ElementIsNotInOffice2007
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ElementIsNotInOffice2007", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031FC RID: 12796
		// (get) Token: 0x0600CFC4 RID: 53188 RVA: 0x002951F8 File Offset: 0x002933F8
		internal static string ElementIsNotInOffice2010
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ElementIsNotInOffice2010", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031FD RID: 12797
		// (get) Token: 0x0600CFC5 RID: 53189 RVA: 0x0029520E File Offset: 0x0029340E
		internal static string ElementIsPartOfTree
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ElementIsPartOfTree", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031FE RID: 12798
		// (get) Token: 0x0600CFC6 RID: 53190 RVA: 0x00295224 File Offset: 0x00293424
		internal static string EmptyCollection
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("EmptyCollection", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x170031FF RID: 12799
		// (get) Token: 0x0600CFC7 RID: 53191 RVA: 0x0029523A File Offset: 0x0029343A
		internal static string ErrorContentType
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ErrorContentType", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003200 RID: 12800
		// (get) Token: 0x0600CFC8 RID: 53192 RVA: 0x00295250 File Offset: 0x00293450
		internal static string ExtendedPartIsOpenXmlPart
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ExtendedPartIsOpenXmlPart", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003201 RID: 12801
		// (get) Token: 0x0600CFC9 RID: 53193 RVA: 0x00295266 File Offset: 0x00293466
		internal static string ExtendedPartNotAllowed
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ExtendedPartNotAllowed", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003202 RID: 12802
		// (get) Token: 0x0600CFCA RID: 53194 RVA: 0x0029527C File Offset: 0x0029347C
		internal static string ExternalRelationshipIsNotReferenced
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ExternalRelationshipIsNotReferenced", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003203 RID: 12803
		// (get) Token: 0x0600CFCB RID: 53195 RVA: 0x00295292 File Offset: 0x00293492
		internal static string FileFormatShouldBe2007Or2010
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("FileFormatShouldBe2007Or2010", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003204 RID: 12804
		// (get) Token: 0x0600CFCC RID: 53196 RVA: 0x002952A8 File Offset: 0x002934A8
		internal static string Fmt_PartRootIsInvalid
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("Fmt_PartRootIsInvalid", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003205 RID: 12805
		// (get) Token: 0x0600CFCD RID: 53197 RVA: 0x002952BE File Offset: 0x002934BE
		internal static string ForeignDataPart
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ForeignDataPart", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003206 RID: 12806
		// (get) Token: 0x0600CFCE RID: 53198 RVA: 0x002952D4 File Offset: 0x002934D4
		internal static string ForeignMediaDataPart
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ForeignMediaDataPart", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003207 RID: 12807
		// (get) Token: 0x0600CFCF RID: 53199 RVA: 0x002952EA File Offset: 0x002934EA
		internal static string ForeignOpenXmlPart
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ForeignOpenXmlPart", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003208 RID: 12808
		// (get) Token: 0x0600CFD0 RID: 53200 RVA: 0x00295300 File Offset: 0x00293500
		internal static string HyperlinkRelationshipIsNotReferenced
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("HyperlinkRelationshipIsNotReferenced", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003209 RID: 12809
		// (get) Token: 0x0600CFD1 RID: 53201 RVA: 0x00295316 File Offset: 0x00293516
		internal static string ImplicitConversionExceptionOnNull
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ImplicitConversionExceptionOnNull", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700320A RID: 12810
		// (get) Token: 0x0600CFD2 RID: 53202 RVA: 0x0029532C File Offset: 0x0029352C
		internal static string InnerXmlCannotBeSet
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("InnerXmlCannotBeSet", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700320B RID: 12811
		// (get) Token: 0x0600CFD3 RID: 53203 RVA: 0x00295342 File Offset: 0x00293542
		internal static string InvalidContentTypePart
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("InvalidContentTypePart", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700320C RID: 12812
		// (get) Token: 0x0600CFD4 RID: 53204 RVA: 0x00295358 File Offset: 0x00293558
		internal static string InvalidEnumValue
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("InvalidEnumValue", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700320D RID: 12813
		// (get) Token: 0x0600CFD5 RID: 53205 RVA: 0x0029536E File Offset: 0x0029356E
		internal static string InvalidMainPartContentType
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("InvalidMainPartContentType", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700320E RID: 12814
		// (get) Token: 0x0600CFD6 RID: 53206 RVA: 0x00295384 File Offset: 0x00293584
		internal static string InvalidMCMode
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("InvalidMCMode", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700320F RID: 12815
		// (get) Token: 0x0600CFD7 RID: 53207 RVA: 0x0029539A File Offset: 0x0029359A
		internal static string InvalidOuterXml
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("InvalidOuterXml", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003210 RID: 12816
		// (get) Token: 0x0600CFD8 RID: 53208 RVA: 0x002953B0 File Offset: 0x002935B0
		internal static string InvalidOuterXmlForMiscNode
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("InvalidOuterXmlForMiscNode", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003211 RID: 12817
		// (get) Token: 0x0600CFD9 RID: 53209 RVA: 0x002953C6 File Offset: 0x002935C6
		internal static string InvalidPackageType
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("InvalidPackageType", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003212 RID: 12818
		// (get) Token: 0x0600CFDA RID: 53210 RVA: 0x002953DC File Offset: 0x002935DC
		internal static string InvalidPartContentType
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("InvalidPartContentType", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003213 RID: 12819
		// (get) Token: 0x0600CFDB RID: 53211 RVA: 0x002953F2 File Offset: 0x002935F2
		internal static string InvalidWriteStringCall
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("InvalidWriteStringCall", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003214 RID: 12820
		// (get) Token: 0x0600CFDC RID: 53212 RVA: 0x00295408 File Offset: 0x00293608
		internal static string InvalidXmlIDStringException
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("InvalidXmlIDStringException", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003215 RID: 12821
		// (get) Token: 0x0600CFDD RID: 53213 RVA: 0x0029541E File Offset: 0x0029361E
		internal static string LeafElementInnerXmlCannotBeSet
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("LeafElementInnerXmlCannotBeSet", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003216 RID: 12822
		// (get) Token: 0x0600CFDE RID: 53214 RVA: 0x00295434 File Offset: 0x00293634
		internal static string LocalNameIsNull
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("LocalNameIsNull", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003217 RID: 12823
		// (get) Token: 0x0600CFDF RID: 53215 RVA: 0x0029544A File Offset: 0x0029364A
		internal static string MainPartIsDifferent
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("MainPartIsDifferent", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003218 RID: 12824
		// (get) Token: 0x0600CFE0 RID: 53216 RVA: 0x00295460 File Offset: 0x00293660
		internal static string MediaDataPartShouldNotReferenceOtherParts
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("MediaDataPartShouldNotReferenceOtherParts", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003219 RID: 12825
		// (get) Token: 0x0600CFE1 RID: 53217 RVA: 0x00295476 File Offset: 0x00293676
		internal static string MultipleRelationshipsToSamePart
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("MultipleRelationshipsToSamePart", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700321A RID: 12826
		// (get) Token: 0x0600CFE2 RID: 53218 RVA: 0x0029548C File Offset: 0x0029368C
		internal static string NoMainPart
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("NoMainPart", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700321B RID: 12827
		// (get) Token: 0x0600CFE3 RID: 53219 RVA: 0x002954A2 File Offset: 0x002936A2
		internal static string NonCompositeNoChild
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("NonCompositeNoChild", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700321C RID: 12828
		// (get) Token: 0x0600CFE4 RID: 53220 RVA: 0x002954B8 File Offset: 0x002936B8
		internal static string NonImplemented
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("NonImplemented", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700321D RID: 12829
		// (get) Token: 0x0600CFE5 RID: 53221 RVA: 0x002954CE File Offset: 0x002936CE
		internal static string NoSpecifiedExternalRelationship
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("NoSpecifiedExternalRelationship", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700321E RID: 12830
		// (get) Token: 0x0600CFE6 RID: 53222 RVA: 0x002954E4 File Offset: 0x002936E4
		internal static string NoSpecifiedHyperlinkRelationship
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("NoSpecifiedHyperlinkRelationship", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700321F RID: 12831
		// (get) Token: 0x0600CFE7 RID: 53223 RVA: 0x002954FA File Offset: 0x002936FA
		internal static string NoSpecifiedReferenceRelationship
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("NoSpecifiedReferenceRelationship", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003220 RID: 12832
		// (get) Token: 0x0600CFE8 RID: 53224 RVA: 0x00295510 File Offset: 0x00293710
		internal static string NsNotUnderStand
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("NsNotUnderStand", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003221 RID: 12833
		// (get) Token: 0x0600CFE9 RID: 53225 RVA: 0x00295526 File Offset: 0x00293726
		internal static string OnlyOnePartAllowed
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("OnlyOnePartAllowed", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003222 RID: 12834
		// (get) Token: 0x0600CFEA RID: 53226 RVA: 0x0029553C File Offset: 0x0029373C
		internal static string PackageAccessModeIsReadonly
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("PackageAccessModeIsReadonly", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003223 RID: 12835
		// (get) Token: 0x0600CFEB RID: 53227 RVA: 0x00295552 File Offset: 0x00293752
		internal static string PackageMustCanBeRead
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("PackageMustCanBeRead", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003224 RID: 12836
		// (get) Token: 0x0600CFEC RID: 53228 RVA: 0x00295568 File Offset: 0x00293768
		internal static string PackageRelatedArgumentNullException
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("PackageRelatedArgumentNullException", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003225 RID: 12837
		// (get) Token: 0x0600CFED RID: 53229 RVA: 0x0029557E File Offset: 0x0029377E
		internal static string ParentIsNull
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ParentIsNull", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003226 RID: 12838
		// (get) Token: 0x0600CFEE RID: 53230 RVA: 0x00295594 File Offset: 0x00293794
		internal static string PartExistsWithDifferentRelationshipId
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("PartExistsWithDifferentRelationshipId", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003227 RID: 12839
		// (get) Token: 0x0600CFEF RID: 53231 RVA: 0x002955AA File Offset: 0x002937AA
		internal static string PartIsDestroyed
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("PartIsDestroyed", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003228 RID: 12840
		// (get) Token: 0x0600CFF0 RID: 53232 RVA: 0x002955C0 File Offset: 0x002937C0
		internal static string PartIsEmpty
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("PartIsEmpty", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003229 RID: 12841
		// (get) Token: 0x0600CFF1 RID: 53233 RVA: 0x002955D6 File Offset: 0x002937D6
		internal static string PartIsNotAllowed
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("PartIsNotAllowed", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700322A RID: 12842
		// (get) Token: 0x0600CFF2 RID: 53234 RVA: 0x002955EC File Offset: 0x002937EC
		internal static string PartIsNotInOffice2007
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("PartIsNotInOffice2007", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700322B RID: 12843
		// (get) Token: 0x0600CFF3 RID: 53235 RVA: 0x00295602 File Offset: 0x00293802
		internal static string PartNotInSamePackage
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("PartNotInSamePackage", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700322C RID: 12844
		// (get) Token: 0x0600CFF4 RID: 53236 RVA: 0x00295618 File Offset: 0x00293818
		internal static string PartNotInVersion
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("PartNotInVersion", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700322D RID: 12845
		// (get) Token: 0x0600CFF5 RID: 53237 RVA: 0x0029562E File Offset: 0x0029382E
		internal static string PartRootAlreadyHasAssociation
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("PartRootAlreadyHasAssociation", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700322E RID: 12846
		// (get) Token: 0x0600CFF6 RID: 53238 RVA: 0x00295644 File Offset: 0x00293844
		internal static string PartUnknown
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("PartUnknown", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700322F RID: 12847
		// (get) Token: 0x0600CFF7 RID: 53239 RVA: 0x0029565A File Offset: 0x0029385A
		internal static string PropertyMutualExclusive
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("PropertyMutualExclusive", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003230 RID: 12848
		// (get) Token: 0x0600CFF8 RID: 53240 RVA: 0x00295670 File Offset: 0x00293870
		internal static string ReaderInEndState
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ReaderInEndState", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003231 RID: 12849
		// (get) Token: 0x0600CFF9 RID: 53241 RVA: 0x00295686 File Offset: 0x00293886
		internal static string ReaderInEofState
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ReaderInEofState", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003232 RID: 12850
		// (get) Token: 0x0600CFFA RID: 53242 RVA: 0x0029569C File Offset: 0x0029389C
		internal static string ReaderInNullState
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ReaderInNullState", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003233 RID: 12851
		// (get) Token: 0x0600CFFB RID: 53243 RVA: 0x002956B2 File Offset: 0x002938B2
		internal static string ReferenceRelationshipIsNotReferenced
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ReferenceRelationshipIsNotReferenced", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003234 RID: 12852
		// (get) Token: 0x0600CFFC RID: 53244 RVA: 0x002956C8 File Offset: 0x002938C8
		internal static string RelationshipIdConflict
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("RelationshipIdConflict", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003235 RID: 12853
		// (get) Token: 0x0600CFFD RID: 53245 RVA: 0x002956DE File Offset: 0x002938DE
		internal static string RequiredPartDoNotExist
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("RequiredPartDoNotExist", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003236 RID: 12854
		// (get) Token: 0x0600CFFE RID: 53246 RVA: 0x002956F4 File Offset: 0x002938F4
		internal static string SamePartWithDifferentRelationshipType
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("SamePartWithDifferentRelationshipType", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003237 RID: 12855
		// (get) Token: 0x0600CFFF RID: 53247 RVA: 0x0029570A File Offset: 0x0029390A
		internal static string StreamAccessModeShouldBeWrite
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("StreamAccessModeShouldBeWrite", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003238 RID: 12856
		// (get) Token: 0x0600D000 RID: 53248 RVA: 0x00295720 File Offset: 0x00293920
		internal static string StringArgumentEmptyException
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("StringArgumentEmptyException", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003239 RID: 12857
		// (get) Token: 0x0600D001 RID: 53249 RVA: 0x00295736 File Offset: 0x00293936
		internal static string StringIsEmpty
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("StringIsEmpty", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700323A RID: 12858
		// (get) Token: 0x0600D002 RID: 53250 RVA: 0x0029574C File Offset: 0x0029394C
		internal static string TextIsInvalidEnumValue
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("TextIsInvalidEnumValue", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700323B RID: 12859
		// (get) Token: 0x0600D003 RID: 53251 RVA: 0x00295762 File Offset: 0x00293962
		internal static string TextIsInvalidOnOffValue
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("TextIsInvalidOnOffValue", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700323C RID: 12860
		// (get) Token: 0x0600D004 RID: 53252 RVA: 0x00295778 File Offset: 0x00293978
		internal static string TextIsInvalidTrueFalseBlankValue
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("TextIsInvalidTrueFalseBlankValue", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700323D RID: 12861
		// (get) Token: 0x0600D005 RID: 53253 RVA: 0x0029578E File Offset: 0x0029398E
		internal static string TextIsInvalidTrueFalseValue
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("TextIsInvalidTrueFalseValue", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700323E RID: 12862
		// (get) Token: 0x0600D006 RID: 53254 RVA: 0x002957A4 File Offset: 0x002939A4
		internal static string UnknowMCContent
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("UnknowMCContent", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x1700323F RID: 12863
		// (get) Token: 0x0600D007 RID: 53255 RVA: 0x002957BA File Offset: 0x002939BA
		internal static string UnknownPackage
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("UnknownPackage", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003240 RID: 12864
		// (get) Token: 0x0600D008 RID: 53256 RVA: 0x002957D0 File Offset: 0x002939D0
		internal static string UseAddHyperlinkRelationship
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("UseAddHyperlinkRelationship", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x17003241 RID: 12865
		// (get) Token: 0x0600D009 RID: 53257 RVA: 0x002957E6 File Offset: 0x002939E6
		internal static string ValidationException
		{
			get
			{
				return ExceptionMessages.ResourceManager.GetString("ValidationException", ExceptionMessages.resourceCulture);
			}
		}

		// Token: 0x040068BA RID: 26810
		private static ResourceManager resourceMan;

		// Token: 0x040068BB RID: 26811
		private static CultureInfo resourceCulture;
	}
}
