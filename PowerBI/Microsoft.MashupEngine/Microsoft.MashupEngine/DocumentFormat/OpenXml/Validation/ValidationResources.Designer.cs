using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace DocumentFormat.OpenXml.Validation
{
	// Token: 0x0200311F RID: 12575
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	[DebuggerNonUserCode]
	internal class ValidationResources
	{
		// Token: 0x0601B430 RID: 111664 RVA: 0x000020FD File Offset: 0x000002FD
		internal ValidationResources()
		{
		}

		// Token: 0x170098E6 RID: 39142
		// (get) Token: 0x0601B431 RID: 111665 RVA: 0x003749B0 File Offset: 0x00372BB0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(ValidationResources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("DocumentFormat.OpenXml.Validation.ValidationResources", typeof(ValidationResources).Assembly);
					ValidationResources.resourceMan = resourceManager;
				}
				return ValidationResources.resourceMan;
			}
		}

		// Token: 0x170098E7 RID: 39143
		// (get) Token: 0x0601B432 RID: 111666 RVA: 0x003749EF File Offset: 0x00372BEF
		// (set) Token: 0x0601B433 RID: 111667 RVA: 0x003749F6 File Offset: 0x00372BF6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return ValidationResources.resourceCulture;
			}
			set
			{
				ValidationResources.resourceCulture = value;
			}
		}

		// Token: 0x170098E8 RID: 39144
		// (get) Token: 0x0601B434 RID: 111668 RVA: 0x003749FE File Offset: 0x00372BFE
		internal static string ExceptionError
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("ExceptionError", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098E9 RID: 39145
		// (get) Token: 0x0601B435 RID: 111669 RVA: 0x00374A14 File Offset: 0x00372C14
		internal static string Fmt_AnyElementInNamespace
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Fmt_AnyElementInNamespace", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098EA RID: 39146
		// (get) Token: 0x0601B436 RID: 111670 RVA: 0x00374A2A File Offset: 0x00372C2A
		internal static string Fmt_ElementName
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Fmt_ElementName", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098EB RID: 39147
		// (get) Token: 0x0601B437 RID: 111671 RVA: 0x00374A40 File Offset: 0x00372C40
		internal static string Fmt_ElementNameSeparator
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Fmt_ElementNameSeparator", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098EC RID: 39148
		// (get) Token: 0x0601B438 RID: 111672 RVA: 0x00374A56 File Offset: 0x00372C56
		internal static string Fmt_ListOfPossibleElements
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Fmt_ListOfPossibleElements", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098ED RID: 39149
		// (get) Token: 0x0601B439 RID: 111673 RVA: 0x00374A6C File Offset: 0x00372C6C
		internal static string MC_ErrorOnUnprefixedAttributeName
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("MC_ErrorOnUnprefixedAttributeName", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098EE RID: 39150
		// (get) Token: 0x0601B43A RID: 111674 RVA: 0x00374A82 File Offset: 0x00372C82
		internal static string MC_InvalidIgnorableAttribute
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("MC_InvalidIgnorableAttribute", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098EF RID: 39151
		// (get) Token: 0x0601B43B RID: 111675 RVA: 0x00374A98 File Offset: 0x00372C98
		internal static string MC_InvalidMustUnderstandAttribute
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("MC_InvalidMustUnderstandAttribute", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098F0 RID: 39152
		// (get) Token: 0x0601B43C RID: 111676 RVA: 0x00374AAE File Offset: 0x00372CAE
		internal static string MC_InvalidPreserveAttributesAttribute
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("MC_InvalidPreserveAttributesAttribute", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098F1 RID: 39153
		// (get) Token: 0x0601B43D RID: 111677 RVA: 0x00374AC4 File Offset: 0x00372CC4
		internal static string MC_InvalidPreserveElementsAttribute
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("MC_InvalidPreserveElementsAttribute", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098F2 RID: 39154
		// (get) Token: 0x0601B43E RID: 111678 RVA: 0x00374ADA File Offset: 0x00372CDA
		internal static string MC_InvalidProcessContentAttribute
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("MC_InvalidProcessContentAttribute", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098F3 RID: 39155
		// (get) Token: 0x0601B43F RID: 111679 RVA: 0x00374AF0 File Offset: 0x00372CF0
		internal static string MC_InvalidRequiresAttribute
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("MC_InvalidRequiresAttribute", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098F4 RID: 39156
		// (get) Token: 0x0601B440 RID: 111680 RVA: 0x00374B06 File Offset: 0x00372D06
		internal static string MC_InvalidXmlAttribute
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("MC_InvalidXmlAttribute", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098F5 RID: 39157
		// (get) Token: 0x0601B441 RID: 111681 RVA: 0x00374B1C File Offset: 0x00372D1C
		internal static string MC_InvalidXmlAttributeWithProcessContent
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("MC_InvalidXmlAttributeWithProcessContent", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098F6 RID: 39158
		// (get) Token: 0x0601B442 RID: 111682 RVA: 0x00374B32 File Offset: 0x00372D32
		internal static string MC_MissedRequiresAttribute
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("MC_MissedRequiresAttribute", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098F7 RID: 39159
		// (get) Token: 0x0601B443 RID: 111683 RVA: 0x00374B48 File Offset: 0x00372D48
		internal static string MC_ShallContainChoice
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("MC_ShallContainChoice", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098F8 RID: 39160
		// (get) Token: 0x0601B444 RID: 111684 RVA: 0x00374B5E File Offset: 0x00372D5E
		internal static string MC_ShallNotContainAlternateContent
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("MC_ShallNotContainAlternateContent", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098F9 RID: 39161
		// (get) Token: 0x0601B445 RID: 111685 RVA: 0x00374B74 File Offset: 0x00372D74
		internal static byte[] O12SchemaConstraintDatas
		{
			get
			{
				object @object = ValidationResources.ResourceManager.GetObject("O12SchemaConstraintDatas", ValidationResources.resourceCulture);
				return (byte[])@object;
			}
		}

		// Token: 0x170098FA RID: 39162
		// (get) Token: 0x0601B446 RID: 111686 RVA: 0x00374B9C File Offset: 0x00372D9C
		internal static byte[] O14SchemaConstraintDatas
		{
			get
			{
				object @object = ValidationResources.ResourceManager.GetObject("O14SchemaConstraintDatas", ValidationResources.resourceCulture);
				return (byte[])@object;
			}
		}

		// Token: 0x170098FB RID: 39163
		// (get) Token: 0x0601B447 RID: 111687 RVA: 0x00374BC4 File Offset: 0x00372DC4
		internal static string Pkg_DataPartReferenceIsNotAllowed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Pkg_DataPartReferenceIsNotAllowed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098FC RID: 39164
		// (get) Token: 0x0601B448 RID: 111688 RVA: 0x00374BDA File Offset: 0x00372DDA
		internal static string Pkg_ExtendedPartIsOpenXmlPart
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Pkg_ExtendedPartIsOpenXmlPart", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098FD RID: 39165
		// (get) Token: 0x0601B449 RID: 111689 RVA: 0x00374BF0 File Offset: 0x00372DF0
		internal static string Pkg_OnlyOnePartAllowed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Pkg_OnlyOnePartAllowed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098FE RID: 39166
		// (get) Token: 0x0601B44A RID: 111690 RVA: 0x00374C06 File Offset: 0x00372E06
		internal static string Pkg_PartIsNotAllowed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Pkg_PartIsNotAllowed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x170098FF RID: 39167
		// (get) Token: 0x0601B44B RID: 111691 RVA: 0x00374C1C File Offset: 0x00372E1C
		internal static string Pkg_RequiredPartDoNotExist
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Pkg_RequiredPartDoNotExist", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009900 RID: 39168
		// (get) Token: 0x0601B44C RID: 111692 RVA: 0x00374C32 File Offset: 0x00372E32
		internal static string Sch_AllElement
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_AllElement", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009901 RID: 39169
		// (get) Token: 0x0601B44D RID: 111693 RVA: 0x00374C48 File Offset: 0x00372E48
		internal static string Sch_AttributeUnionFailedEx
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_AttributeUnionFailedEx", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009902 RID: 39170
		// (get) Token: 0x0601B44E RID: 111694 RVA: 0x00374C5E File Offset: 0x00372E5E
		internal static string Sch_AttributeValueDataTypeDetailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_AttributeValueDataTypeDetailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009903 RID: 39171
		// (get) Token: 0x0601B44F RID: 111695 RVA: 0x00374C74 File Offset: 0x00372E74
		internal static string Sch_ElementUnionFailedEx
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_ElementUnionFailedEx", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009904 RID: 39172
		// (get) Token: 0x0601B450 RID: 111696 RVA: 0x00374C8A File Offset: 0x00372E8A
		internal static string Sch_ElementValueDataTypeDetailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_ElementValueDataTypeDetailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009905 RID: 39173
		// (get) Token: 0x0601B451 RID: 111697 RVA: 0x00374CA0 File Offset: 0x00372EA0
		internal static string Sch_EmptyAttributeValue
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_EmptyAttributeValue", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009906 RID: 39174
		// (get) Token: 0x0601B452 RID: 111698 RVA: 0x00374CB6 File Offset: 0x00372EB6
		internal static string Sch_EmptyElementValue
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_EmptyElementValue", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009907 RID: 39175
		// (get) Token: 0x0601B453 RID: 111699 RVA: 0x00374CCC File Offset: 0x00372ECC
		internal static string Sch_EnumerationConstraintFailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_EnumerationConstraintFailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009908 RID: 39176
		// (get) Token: 0x0601B454 RID: 111700 RVA: 0x00374CE2 File Offset: 0x00372EE2
		internal static string Sch_IncompleteContentExpectingComplex
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_IncompleteContentExpectingComplex", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009909 RID: 39177
		// (get) Token: 0x0601B455 RID: 111701 RVA: 0x00374CF8 File Offset: 0x00372EF8
		internal static string Sch_InvalidChildinLeafElement
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_InvalidChildinLeafElement", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700990A RID: 39178
		// (get) Token: 0x0601B456 RID: 111702 RVA: 0x00374D0E File Offset: 0x00372F0E
		internal static string Sch_InvalidElementContentExpectingComplex
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_InvalidElementContentExpectingComplex", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700990B RID: 39179
		// (get) Token: 0x0601B457 RID: 111703 RVA: 0x00374D24 File Offset: 0x00372F24
		internal static string Sch_InvalidElementContentWrongType
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_InvalidElementContentWrongType", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700990C RID: 39180
		// (get) Token: 0x0601B458 RID: 111704 RVA: 0x00374D3A File Offset: 0x00372F3A
		internal static string Sch_LengthConstraintFailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_LengthConstraintFailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700990D RID: 39181
		// (get) Token: 0x0601B459 RID: 111705 RVA: 0x00374D50 File Offset: 0x00372F50
		internal static string Sch_MaxExclusiveConstraintFailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_MaxExclusiveConstraintFailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700990E RID: 39182
		// (get) Token: 0x0601B45A RID: 111706 RVA: 0x00374D66 File Offset: 0x00372F66
		internal static string Sch_MaxInclusiveConstraintFailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_MaxInclusiveConstraintFailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700990F RID: 39183
		// (get) Token: 0x0601B45B RID: 111707 RVA: 0x00374D7C File Offset: 0x00372F7C
		internal static string Sch_MaxLengthConstraintFailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_MaxLengthConstraintFailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009910 RID: 39184
		// (get) Token: 0x0601B45C RID: 111708 RVA: 0x00374D92 File Offset: 0x00372F92
		internal static string Sch_MinExclusiveConstraintFailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_MinExclusiveConstraintFailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009911 RID: 39185
		// (get) Token: 0x0601B45D RID: 111709 RVA: 0x00374DA8 File Offset: 0x00372FA8
		internal static string Sch_MinInclusiveConstraintFailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_MinInclusiveConstraintFailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009912 RID: 39186
		// (get) Token: 0x0601B45E RID: 111710 RVA: 0x00374DBE File Offset: 0x00372FBE
		internal static string Sch_MinLengthConstraintFailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_MinLengthConstraintFailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009913 RID: 39187
		// (get) Token: 0x0601B45F RID: 111711 RVA: 0x00374DD4 File Offset: 0x00372FD4
		internal static string Sch_MissRequiredAttribute
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_MissRequiredAttribute", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009914 RID: 39188
		// (get) Token: 0x0601B460 RID: 111712 RVA: 0x00374DEA File Offset: 0x00372FEA
		internal static string Sch_PatternConstraintFailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_PatternConstraintFailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009915 RID: 39189
		// (get) Token: 0x0601B461 RID: 111713 RVA: 0x00374E00 File Offset: 0x00373000
		internal static string Sch_StringIsNotValidValue
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_StringIsNotValidValue", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009916 RID: 39190
		// (get) Token: 0x0601B462 RID: 111714 RVA: 0x00374E16 File Offset: 0x00373016
		internal static string Sch_TotalDigitsConstraintFailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_TotalDigitsConstraintFailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009917 RID: 39191
		// (get) Token: 0x0601B463 RID: 111715 RVA: 0x00374E2C File Offset: 0x0037302C
		internal static string Sch_UndeclaredAttribute
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_UndeclaredAttribute", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009918 RID: 39192
		// (get) Token: 0x0601B464 RID: 111716 RVA: 0x00374E42 File Offset: 0x00373042
		internal static string Sch_UnexpectedElementContentExpectingComplex
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sch_UnexpectedElementContentExpectingComplex", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009919 RID: 39193
		// (get) Token: 0x0601B465 RID: 111717 RVA: 0x00374E58 File Offset: 0x00373058
		internal static string Sem_AttributeAbsentConditionToNonValue
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_AttributeAbsentConditionToNonValue", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700991A RID: 39194
		// (get) Token: 0x0601B466 RID: 111718 RVA: 0x00374E6E File Offset: 0x0037306E
		internal static string Sem_AttributeAbsentConditionToValue
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_AttributeAbsentConditionToValue", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700991B RID: 39195
		// (get) Token: 0x0601B467 RID: 111719 RVA: 0x00374E84 File Offset: 0x00373084
		internal static string Sem_AttributeMutualExclusive
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_AttributeMutualExclusive", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700991C RID: 39196
		// (get) Token: 0x0601B468 RID: 111720 RVA: 0x00374E9A File Offset: 0x0037309A
		internal static string Sem_AttributeRequiredConditionToValue
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_AttributeRequiredConditionToValue", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700991D RID: 39197
		// (get) Token: 0x0601B469 RID: 111721 RVA: 0x00374EB0 File Offset: 0x003730B0
		internal static string Sem_AttributeValueConditionToAnother
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_AttributeValueConditionToAnother", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700991E RID: 39198
		// (get) Token: 0x0601B46A RID: 111722 RVA: 0x00374EC6 File Offset: 0x003730C6
		internal static string Sem_AttributeValueDataTypeDetailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_AttributeValueDataTypeDetailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700991F RID: 39199
		// (get) Token: 0x0601B46B RID: 111723 RVA: 0x00374EDC File Offset: 0x003730DC
		internal static string Sem_AttributeValueLessEqualToAnother
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_AttributeValueLessEqualToAnother", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009920 RID: 39200
		// (get) Token: 0x0601B46C RID: 111724 RVA: 0x00374EF2 File Offset: 0x003730F2
		internal static string Sem_AttributeValueLessEqualToAnotherEx
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_AttributeValueLessEqualToAnotherEx", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009921 RID: 39201
		// (get) Token: 0x0601B46D RID: 111725 RVA: 0x00374F08 File Offset: 0x00373108
		internal static string Sem_AttributeValueUniqueInDocument
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_AttributeValueUniqueInDocument", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009922 RID: 39202
		// (get) Token: 0x0601B46E RID: 111726 RVA: 0x00374F1E File Offset: 0x0037311E
		internal static string Sem_IncorrectRelationshipType
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_IncorrectRelationshipType", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009923 RID: 39203
		// (get) Token: 0x0601B46F RID: 111727 RVA: 0x00374F34 File Offset: 0x00373134
		internal static string Sem_InvalidRelationshipId
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_InvalidRelationshipId", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009924 RID: 39204
		// (get) Token: 0x0601B470 RID: 111728 RVA: 0x00374F4A File Offset: 0x0037314A
		internal static string Sem_MaxLengthConstraintFailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_MaxLengthConstraintFailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009925 RID: 39205
		// (get) Token: 0x0601B471 RID: 111729 RVA: 0x00374F60 File Offset: 0x00373160
		internal static string Sem_MinLengthConstraintFailed
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_MinLengthConstraintFailed", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009926 RID: 39206
		// (get) Token: 0x0601B472 RID: 111730 RVA: 0x00374F76 File Offset: 0x00373176
		internal static string Sem_MissingIndexedElement
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_MissingIndexedElement", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009927 RID: 39207
		// (get) Token: 0x0601B473 RID: 111731 RVA: 0x00374F8C File Offset: 0x0037318C
		internal static string Sem_MissingReferenceElement
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_MissingReferenceElement", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009928 RID: 39208
		// (get) Token: 0x0601B474 RID: 111732 RVA: 0x00374FA2 File Offset: 0x003731A2
		internal static string Sem_UniqueAttributeValue
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("Sem_UniqueAttributeValue", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009929 RID: 39209
		// (get) Token: 0x0601B475 RID: 111733 RVA: 0x00374FB8 File Offset: 0x003731B8
		internal static string TypeName_base64Binary
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("TypeName_base64Binary", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700992A RID: 39210
		// (get) Token: 0x0601B476 RID: 111734 RVA: 0x00374FCE File Offset: 0x003731CE
		internal static string TypeName_hexBinary
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("TypeName_hexBinary", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700992B RID: 39211
		// (get) Token: 0x0601B477 RID: 111735 RVA: 0x00374FE4 File Offset: 0x003731E4
		internal static string TypeName_ID
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("TypeName_ID", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700992C RID: 39212
		// (get) Token: 0x0601B478 RID: 111736 RVA: 0x00374FFA File Offset: 0x003731FA
		internal static string TypeName_Integer
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("TypeName_Integer", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700992D RID: 39213
		// (get) Token: 0x0601B479 RID: 111737 RVA: 0x00375010 File Offset: 0x00373210
		internal static string TypeName_language
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("TypeName_language", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700992E RID: 39214
		// (get) Token: 0x0601B47A RID: 111738 RVA: 0x00375026 File Offset: 0x00373226
		internal static string TypeName_NCName
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("TypeName_NCName", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x1700992F RID: 39215
		// (get) Token: 0x0601B47B RID: 111739 RVA: 0x0037503C File Offset: 0x0037323C
		internal static string TypeName_nonNegativeInteger
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("TypeName_nonNegativeInteger", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009930 RID: 39216
		// (get) Token: 0x0601B47C RID: 111740 RVA: 0x00375052 File Offset: 0x00373252
		internal static string TypeName_positiveInteger
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("TypeName_positiveInteger", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009931 RID: 39217
		// (get) Token: 0x0601B47D RID: 111741 RVA: 0x00375068 File Offset: 0x00373268
		internal static string TypeName_QName
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("TypeName_QName", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x17009932 RID: 39218
		// (get) Token: 0x0601B47E RID: 111742 RVA: 0x0037507E File Offset: 0x0037327E
		internal static string TypeName_token
		{
			get
			{
				return ValidationResources.ResourceManager.GetString("TypeName_token", ValidationResources.resourceCulture);
			}
		}

		// Token: 0x0400B4CB RID: 46283
		private static ResourceManager resourceMan;

		// Token: 0x0400B4CC RID: 46284
		private static CultureInfo resourceCulture;
	}
}
