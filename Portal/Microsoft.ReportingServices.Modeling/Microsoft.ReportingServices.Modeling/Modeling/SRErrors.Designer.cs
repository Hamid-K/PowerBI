using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000BC RID: 188
	[CompilerGenerated]
	internal class SRErrors
	{
		// Token: 0x06000A6A RID: 2666 RVA: 0x00023DDE File Offset: 0x00021FDE
		protected SRErrors()
		{
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00023DE6 File Offset: 0x00021FE6
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x00023DED File Offset: 0x00021FED
		public static CultureInfo Culture
		{
			get
			{
				return SRErrors.Keys.Culture;
			}
			set
			{
				SRErrors.Keys.Culture = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00023DF5 File Offset: 0x00021FF5
		public static string InternalError
		{
			get
			{
				return SRErrors.Keys.GetString("InternalError");
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00023E01 File Offset: 0x00022001
		public static string InvalidGuid_NoContext
		{
			get
			{
				return SRErrors.Keys.GetString("InvalidGuid_NoContext");
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00023E0D File Offset: 0x0002200D
		public static string InvalidDrillSelectedItems
		{
			get
			{
				return SRErrors.Keys.GetString("InvalidDrillSelectedItems");
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00023E19 File Offset: 0x00022019
		public static string ValidationException_MessageWithAdditionalMessages(string message, int numAdditionalMessages)
		{
			return SRErrors.Keys.GetString("ValidationException_MessageWithAdditionalMessages", message, numAdditionalMessages);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00023E2C File Offset: 0x0002202C
		public static string XmlValidationException_MessageWithLineInfo(string message, int lineNumber, int linePosition)
		{
			return SRErrors.Keys.GetString("XmlValidationException_MessageWithLineInfo", message, lineNumber, linePosition);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00023E45 File Offset: 0x00022045
		public static string InvalidDataSourceView(string detailMessage)
		{
			return SRErrors.Keys.GetString("InvalidDataSourceView", detailMessage);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00023E52 File Offset: 0x00022052
		public static string InvalidSemanticModel(string detailMessage)
		{
			return SRErrors.Keys.GetString("InvalidSemanticModel", detailMessage);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00023E5F File Offset: 0x0002205F
		public static string InvalidSemanticQuery(string detailMessage)
		{
			return SRErrors.Keys.GetString("InvalidSemanticQuery", detailMessage);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00023E6C File Offset: 0x0002206C
		public static string InvalidDrillthroughContext(string detailMessage)
		{
			return SRErrors.Keys.GetString("InvalidDrillthroughContext", detailMessage);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00023E79 File Offset: 0x00022079
		public static string InvalidModelGenerationRules(string detailMessage)
		{
			return SRErrors.Keys.GetString("InvalidModelGenerationRules", detailMessage);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00023E86 File Offset: 0x00022086
		public static string Xml_NodeMismatch(XmlNodeType expectedType, string expectedName, string expectedNamespace, XmlNodeType actualType, string actualName, string actualNamespace)
		{
			return SRErrors.Keys.GetString("Xml_NodeMismatch", expectedType, expectedName, expectedNamespace, actualType, actualName, actualNamespace);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00023EA4 File Offset: 0x000220A4
		public static string ObjectDescriptor_TypeAndName(string type, string name)
		{
			return SRErrors.Keys.GetString("ObjectDescriptor_TypeAndName", type, name);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00023EB2 File Offset: 0x000220B2
		public static string InvalidCulture(string propertyName, SRObjectDescriptor objectTypeAndName, string value)
		{
			return SRErrors.Keys.GetString("InvalidCulture", propertyName, objectTypeAndName, value);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00023EC6 File Offset: 0x000220C6
		public static string DuplicateItemID(QName id)
		{
			return SRErrors.Keys.GetString("DuplicateItemID", id);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00023ED8 File Offset: 0x000220D8
		public static string InvalidEntityBinding(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidEntityBinding", objectTypeAndName);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00023EEA File Offset: 0x000220EA
		public static string NestedVariations(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("NestedVariations", objectTypeAndName);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00023EFC File Offset: 0x000220FC
		public static string InvalidLinguistics(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidLinguistics", objectTypeAndName);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00023F0E File Offset: 0x0002210E
		public static string MissingRelationEnd(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingRelationEnd", objectTypeAndName);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00023F20 File Offset: 0x00022120
		public static string InvalidExpression(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidExpression", objectTypeAndName);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00023F32 File Offset: 0x00022132
		public static string InvalidExpression_TopLevel(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidExpression_TopLevel", objectTypeAndName);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00023F44 File Offset: 0x00022144
		public static string InvalidFunctionName(SRObjectDescriptor objectTypeAndName, string functionName)
		{
			return SRErrors.Keys.GetString("InvalidFunctionName", objectTypeAndName, functionName);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00023F57 File Offset: 0x00022157
		public static string InvalidAttributeRef(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidAttributeRef", objectTypeAndName);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00023F69 File Offset: 0x00022169
		public static string InvalidLiteral(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidLiteral", objectTypeAndName);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00023F7B File Offset: 0x0002217B
		public static string InvalidLiteralValue(SRObjectDescriptor objectTypeAndName, string value, DataType dataType, string errorDetails)
		{
			return SRErrors.Keys.GetString("InvalidLiteralValue", objectTypeAndName, value, dataType, errorDetails);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00023F95 File Offset: 0x00022195
		public static string ItemNotFound(string propertyName, SRObjectDescriptor objectTypeAndName, string reference)
		{
			return SRErrors.Keys.GetString("ItemNotFound", propertyName, objectTypeAndName, reference);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00023FA9 File Offset: 0x000221A9
		public static string ItemNotFound_MultipleProperties(string propertyName, SRObjectDescriptor objectTypeAndName, string reference)
		{
			return SRErrors.Keys.GetString("ItemNotFound_MultipleProperties", propertyName, objectTypeAndName, reference);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00023FBD File Offset: 0x000221BD
		public static string InvalidReferencedItem(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor referencedTypeAndName, string expectedTypes)
		{
			return SRErrors.Keys.GetString("InvalidReferencedItem", propertyName, objectTypeAndName, referencedTypeAndName, expectedTypes);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00023FD7 File Offset: 0x000221D7
		public static string InvalidReferencedItem_MultipleProperties(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor referencedTypeAndName, string expectedTypes)
		{
			return SRErrors.Keys.GetString("InvalidReferencedItem_MultipleProperties", propertyName, objectTypeAndName, referencedTypeAndName, expectedTypes);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00023FF1 File Offset: 0x000221F1
		public static string CircularInheritance(string propertyName, SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("CircularInheritance", propertyName, objectTypeAndName);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00024004 File Offset: 0x00022204
		public static string SelfReferentialRole(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("SelfReferentialRole", objectTypeAndName);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00024016 File Offset: 0x00022216
		public static string GroupingNotFound(string propertyName, SRObjectDescriptor objectTypeAndName, string reference)
		{
			return SRErrors.Keys.GetString("GroupingNotFound", propertyName, objectTypeAndName, reference);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002402A File Offset: 0x0002222A
		public static string GroupingNotFound_MultipleProperties(string propertyName, SRObjectDescriptor objectTypeAndName, string reference)
		{
			return SRErrors.Keys.GetString("GroupingNotFound_MultipleProperties", propertyName, objectTypeAndName, reference);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002403E File Offset: 0x0002223E
		public static string MeasureNotFound(string propertyName, SRObjectDescriptor objectTypeAndName, string reference)
		{
			return SRErrors.Keys.GetString("MeasureNotFound", propertyName, objectTypeAndName, reference);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00024052 File Offset: 0x00022252
		public static string MeasureNotFound_MultipleProperties(string propertyName, SRObjectDescriptor objectTypeAndName, string reference)
		{
			return SRErrors.Keys.GetString("MeasureNotFound_MultipleProperties", propertyName, objectTypeAndName, reference);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00024066 File Offset: 0x00022266
		public static string CalculatedAttributeNotFound(string propertyName, SRObjectDescriptor objectTypeAndName, string reference)
		{
			return SRErrors.Keys.GetString("CalculatedAttributeNotFound", propertyName, objectTypeAndName, reference);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002407A File Offset: 0x0002227A
		public static string CalculatedAttributeNotFound_MultipleProperties(string propertyName, SRObjectDescriptor objectTypeAndName, string reference)
		{
			return SRErrors.Keys.GetString("CalculatedAttributeNotFound_MultipleProperties", propertyName, objectTypeAndName, reference);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002408E File Offset: 0x0002228E
		public static string ParameterNotFound(string propertyName, SRObjectDescriptor objectTypeAndName, string reference)
		{
			return SRErrors.Keys.GetString("ParameterNotFound", propertyName, objectTypeAndName, reference);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000240A2 File Offset: 0x000222A2
		public static string ParameterNotFound_MultipleProperties(string propertyName, SRObjectDescriptor objectTypeAndName, string reference)
		{
			return SRErrors.Keys.GetString("ParameterNotFound_MultipleProperties", propertyName, objectTypeAndName, reference);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000240B6 File Offset: 0x000222B6
		public static string ResultExpressionNotFound(string propertyName, SRObjectDescriptor objectTypeAndName, string reference)
		{
			return SRErrors.Keys.GetString("ResultExpressionNotFound", propertyName, objectTypeAndName, reference);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000240CA File Offset: 0x000222CA
		public static string ResultExpressionNotFound_MultipleProperties(string propertyName, SRObjectDescriptor objectTypeAndName, string reference)
		{
			return SRErrors.Keys.GetString("ResultExpressionNotFound_MultipleProperties", propertyName, objectTypeAndName, reference);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000240DE File Offset: 0x000222DE
		public static string MissingItemName(SRObjectDescriptor objectTypeAndID)
		{
			return SRErrors.Keys.GetString("MissingItemName", objectTypeAndID);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x000240F0 File Offset: 0x000222F0
		public static string IDLocalNameLengthExceeded(SRObjectDescriptor objectTypeAndName, int maxLength)
		{
			return SRErrors.Keys.GetString("IDLocalNameLengthExceeded", objectTypeAndName, maxLength);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00024108 File Offset: 0x00022308
		public static string IDLocalNameLengthExceeded_NoContext(int maxLength)
		{
			return SRErrors.Keys.GetString("IDLocalNameLengthExceeded_NoContext", maxLength);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002411A File Offset: 0x0002231A
		public static string IDNamespaceLengthExceeded(SRObjectDescriptor objectTypeAndName, int maxLength)
		{
			return SRErrors.Keys.GetString("IDNamespaceLengthExceeded", objectTypeAndName, maxLength);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00024132 File Offset: 0x00022332
		public static string IDNamespaceLengthExceeded_NoContext(int maxLength)
		{
			return SRErrors.Keys.GetString("IDNamespaceLengthExceeded_NoContext", maxLength);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00024144 File Offset: 0x00022344
		public static string InvalidGuid(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidGuid", objectTypeAndName);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00024156 File Offset: 0x00022356
		public static string DuplicateItemName(SRObjectDescriptor objectTypeAndName, string itemName)
		{
			return SRErrors.Keys.GetString("DuplicateItemName", objectTypeAndName, itemName);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00024169 File Offset: 0x00022369
		public static string DuplicateEntityName(SRObjectDescriptor objectTypeAndName, string entityName)
		{
			return SRErrors.Keys.GetString("DuplicateEntityName", objectTypeAndName, entityName);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002417C File Offset: 0x0002237C
		public static string DuplicateFieldName(SRObjectDescriptor objectTypeAndName, string fieldName)
		{
			return SRErrors.Keys.GetString("DuplicateFieldName", objectTypeAndName, fieldName);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002418F File Offset: 0x0002238F
		public static string MissingIdentifyingAttributes(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingIdentifyingAttributes", objectTypeAndName);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000241A1 File Offset: 0x000223A1
		public static string InvalidSetAttributeReference(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor attributeTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidSetAttributeReference", propertyName, objectTypeAndName, attributeTypeAndName);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000241BA File Offset: 0x000223BA
		public static string InvalidAggregateAttributeReference(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor attributeTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidAggregateAttributeReference", propertyName, objectTypeAndName, attributeTypeAndName);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000241D3 File Offset: 0x000223D3
		public static string InvalidScalarAttributeReference(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor attributeTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidScalarAttributeReference", propertyName, objectTypeAndName, attributeTypeAndName);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x000241EC File Offset: 0x000223EC
		public static string InvalidNonFilterAttributeReference(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor attributeTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidNonFilterAttributeReference", propertyName, objectTypeAndName, attributeTypeAndName);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00024205 File Offset: 0x00022405
		public static string InvalidHiddenAttributeReference(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor attributeTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidHiddenAttributeReference", propertyName, objectTypeAndName, attributeTypeAndName);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002421E File Offset: 0x0002241E
		public static string ExpressionDataTypeMismatch(SRObjectDescriptor objectTypeAndName, string objectName, DataType attributeDataType, DataType expressionDataType)
		{
			return SRErrors.Keys.GetString("ExpressionDataTypeMismatch", objectTypeAndName, objectName, attributeDataType, expressionDataType);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002423D File Offset: 0x0002243D
		public static string ExpressionNullableMismatch(SRObjectDescriptor objectTypeAndName, string objectName)
		{
			return SRErrors.Keys.GetString("ExpressionNullableMismatch", objectTypeAndName, objectName);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00024250 File Offset: 0x00022450
		public static string MissingMimeType(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingMimeType", objectTypeAndName);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00024262 File Offset: 0x00022462
		public static string IsAggregateWithDefaultAggregate(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("IsAggregateWithDefaultAggregate", objectTypeAndName);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00024274 File Offset: 0x00022474
		public static string NonAggregateAsDefaultAggregate(SRObjectDescriptor objectTypeAndName, SRObjectDescriptor defaultAggregateTypeAndName)
		{
			return SRErrors.Keys.GetString("NonAggregateAsDefaultAggregate", objectTypeAndName, defaultAggregateTypeAndName);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002428C File Offset: 0x0002248C
		public static string NonVariationAsDefaultAggregate(SRObjectDescriptor objectTypeAndName, string objectName, SRObjectDescriptor defaultAggregateTypeAndName)
		{
			return SRErrors.Keys.GetString("NonVariationAsDefaultAggregate", objectTypeAndName, objectName, defaultAggregateTypeAndName);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000242A5 File Offset: 0x000224A5
		public static string MissingRelatedRole(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingRelatedRole", objectTypeAndName);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000242B7 File Offset: 0x000224B7
		public static string RelatedRolesMismatch(SRObjectDescriptor objectTypeAndName, string objectName, SRObjectDescriptor relatedRoleTypeAndName, string relatedRoleName)
		{
			return SRErrors.Keys.GetString("RelatedRolesMismatch", objectTypeAndName, objectName, relatedRoleTypeAndName, relatedRoleName);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000242D1 File Offset: 0x000224D1
		public static string InvalidOptionalityOfRoleForColumnBoundEntity(SRObjectDescriptor objectTypeAndName, SRObjectDescriptor roleEntityTypeAndName, SRObjectDescriptor relatedEntityTypeAndName, SRObjectDescriptor columnBoundEntityTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidOptionalityOfRoleForColumnBoundEntity", objectTypeAndName, roleEntityTypeAndName, relatedEntityTypeAndName, columnBoundEntityTypeAndName);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000242F5 File Offset: 0x000224F5
		public static string InvalidModelItemInPerspective(SRObjectDescriptor perspective, SRObjectDescriptor modelItem)
		{
			return SRErrors.Keys.GetString("InvalidModelItemInPerspective", perspective, modelItem);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002430D File Offset: 0x0002250D
		public static string MissingDataSourceView(SRObjectDescriptor objectTypeAndID)
		{
			return SRErrors.Keys.GetString("MissingDataSourceView", objectTypeAndID);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002431F File Offset: 0x0002251F
		public static string MissingBinding_Entity(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingBinding_Entity", objectTypeAndName);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00024331 File Offset: 0x00022531
		public static string MissingBinding_Inheritance(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingBinding_Inheritance", objectTypeAndName);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00024343 File Offset: 0x00022543
		public static string MissingBinding_Attribute(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingBinding_Attribute", objectTypeAndName);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00024355 File Offset: 0x00022555
		public static string MissingBinding_Role(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingBinding_Role", objectTypeAndName);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00024367 File Offset: 0x00022567
		public static string InvalidBinding(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor dsvItemTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidBinding", propertyName, objectTypeAndName, dsvItemTypeAndName);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00024380 File Offset: 0x00022580
		public static string InvalidColumnReferenceInColumnEntity(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor columnTypeAndName, SRObjectDescriptor boundColumnTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidColumnReferenceInColumnEntity", propertyName, objectTypeAndName, columnTypeAndName, boundColumnTypeAndName);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002439F File Offset: 0x0002259F
		public static string MissingColumnTableName(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingColumnTableName", objectTypeAndName);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000243B1 File Offset: 0x000225B1
		public static string InvalidColumnTableName(SRObjectDescriptor objectTypeAndName, SRObjectDescriptor columnTableTypeAndName, SRObjectDescriptor entityTableTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidColumnTableName", objectTypeAndName, columnTableTypeAndName, entityTableTypeAndName);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000243CF File Offset: 0x000225CF
		public static string InvalidColumnDataType(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor columnTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidColumnDataType", propertyName, objectTypeAndName, columnTypeAndName);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000243E8 File Offset: 0x000225E8
		public static string NonPrimaryDataSource(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor tableTypeAndName)
		{
			return SRErrors.Keys.GetString("NonPrimaryDataSource", propertyName, objectTypeAndName, tableTypeAndName);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00024401 File Offset: 0x00022601
		public static string MissingPrimaryKey(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor tableTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingPrimaryKey", propertyName, objectTypeAndName, tableTypeAndName);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002441A File Offset: 0x0002261A
		public static string BinaryEntityColumn(SRObjectDescriptor objectTypeAndName, SRObjectDescriptor columnTypeAndName)
		{
			return SRErrors.Keys.GetString("BinaryEntityColumn", objectTypeAndName, columnTypeAndName);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00024432 File Offset: 0x00022632
		public static string InvalidInheritanceRelationTable(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor relationTypeAndName, SRObjectDescriptor sourceTableTypeAndName, SRObjectDescriptor targetTableTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidInheritanceRelationTable", propertyName, objectTypeAndName, relationTypeAndName, sourceTableTypeAndName, targetTableTypeAndName);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00024458 File Offset: 0x00022658
		public static string NonUniqueInheritanceRelationColumns(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor relationTypeAndName, SRObjectDescriptor tableTypeAndName)
		{
			return SRErrors.Keys.GetString("NonUniqueInheritanceRelationColumns", propertyName, objectTypeAndName, relationTypeAndName, tableTypeAndName);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00024477 File Offset: 0x00022677
		public static string ColumnDataTypeMismatch(SRObjectDescriptor objectTypeAndName, SRObjectDescriptor columnTypeAndName, DataType attributeDataType, DataType columnDataType)
		{
			return SRErrors.Keys.GetString("ColumnDataTypeMismatch", objectTypeAndName, columnTypeAndName, attributeDataType, columnDataType);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002449B File Offset: 0x0002269B
		public static string ColumnNullableMismatch(SRObjectDescriptor objectTypeAndName, SRObjectDescriptor columnTypeAndName)
		{
			return SRErrors.Keys.GetString("ColumnNullableMismatch", objectTypeAndName, columnTypeAndName);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x000244B3 File Offset: 0x000226B3
		public static string IsAggregateWithColumn(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("IsAggregateWithColumn", objectTypeAndName);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000244C5 File Offset: 0x000226C5
		public static string PromoteLookupForNonLookupEntity(SRObjectDescriptor objectTypeAndName, SRObjectDescriptor entityTypeAndName)
		{
			return SRErrors.Keys.GetString("PromoteLookupForNonLookupEntity", objectTypeAndName, entityTypeAndName);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000244DD File Offset: 0x000226DD
		public static string RoleRelationsMismatch(SRObjectDescriptor objectTypeAndName, string relatedRoleName, SRObjectDescriptor relationTypeAndName, SRObjectDescriptor relatedRelationTypeAndName)
		{
			return SRErrors.Keys.GetString("RoleRelationsMismatch", objectTypeAndName, relatedRoleName, relationTypeAndName, relatedRelationTypeAndName);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000244FC File Offset: 0x000226FC
		public static string RoleRelationEndsMismatch(SRObjectDescriptor objectTypeAndName, string relatedRoleName, RelationEnd relationEnd, SRObjectDescriptor relationTypeAndName)
		{
			return SRErrors.Keys.GetString("RoleRelationEndsMismatch", objectTypeAndName, relatedRoleName, relationEnd, relationTypeAndName);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002451B File Offset: 0x0002271B
		public static string InvalidRoleRelationTable(SRObjectDescriptor objectTypeAndName, RelationEnd relationEnd, SRObjectDescriptor relationTypeAndName, SRObjectDescriptor tableTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidRoleRelationTable", objectTypeAndName, relationEnd, relationTypeAndName, tableTypeAndName);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002453F File Offset: 0x0002273F
		public static string NonUniqueRoleRelationColumns(SRObjectDescriptor objectTypeAndName, RelationEnd relationEnd, SRObjectDescriptor relationTypeAndName, SRObjectDescriptor tableTypeAndName)
		{
			return SRErrors.Keys.GetString("NonUniqueRoleRelationColumns", objectTypeAndName, relationEnd, relationTypeAndName, tableTypeAndName);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00024563 File Offset: 0x00022763
		public static string NonBooleanFilterAttribute(SRObjectDescriptor objectTypeAndName, DataType dataType)
		{
			return SRErrors.Keys.GetString("NonBooleanFilterAttribute", objectTypeAndName, dataType);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002457B File Offset: 0x0002277B
		public static string CyclicExpression(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("CyclicExpression", objectTypeAndName);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002458D File Offset: 0x0002278D
		public static string CyclicExpression_ExpressionObject(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("CyclicExpression_ExpressionObject", objectTypeAndName);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002459F File Offset: 0x0002279F
		public static string FieldReferenceOutOfContext(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor fieldTypeAndName, SRObjectDescriptor contextEntityTypeAndName)
		{
			return SRErrors.Keys.GetString("FieldReferenceOutOfContext", propertyName, objectTypeAndName, fieldTypeAndName, contextEntityTypeAndName);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000245BE File Offset: 0x000227BE
		public static string FieldReferenceOutOfContext_MultipleProperties(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor fieldTypeAndName, SRObjectDescriptor contextEntityTypeAndName)
		{
			return SRErrors.Keys.GetString("FieldReferenceOutOfContext_MultipleProperties", propertyName, objectTypeAndName, fieldTypeAndName, contextEntityTypeAndName);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000245DD File Offset: 0x000227DD
		public static string EntityReferenceOutOfContext(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor entityTypeAndName, SRObjectDescriptor contextEntityTypeAndName)
		{
			return SRErrors.Keys.GetString("EntityReferenceOutOfContext", propertyName, objectTypeAndName, entityTypeAndName, contextEntityTypeAndName);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000245FC File Offset: 0x000227FC
		public static string EntityReferenceOutOfContext_MultipleProperties(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor entityTypeAndName, SRObjectDescriptor contextEntityTypeAndName)
		{
			return SRErrors.Keys.GetString("EntityReferenceOutOfContext_MultipleProperties", propertyName, objectTypeAndName, entityTypeAndName, contextEntityTypeAndName);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002461B File Offset: 0x0002281B
		public static string NonAggregateExpression_Attribute(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("NonAggregateExpression_Attribute", objectTypeAndName);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002462D File Offset: 0x0002282D
		public static string NonAggregateExpression_Measure(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("NonAggregateExpression_Measure", objectTypeAndName);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002463F File Offset: 0x0002283F
		public static string AggregateWithNonAggregateArgument(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("AggregateWithNonAggregateArgument", objectTypeAndName);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00024651 File Offset: 0x00022851
		public static string WrongNumberOfArguments(SRObjectDescriptor objectTypeAndName, FunctionName functionName)
		{
			return SRErrors.Keys.GetString("WrongNumberOfArguments", objectTypeAndName, functionName);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00024669 File Offset: 0x00022869
		public static string ArgumentDataTypeMismatch(SRObjectDescriptor objectTypeAndName, FunctionName functionName, int argumentIndex, DataType inputDataType)
		{
			return SRErrors.Keys.GetString("ArgumentDataTypeMismatch", objectTypeAndName, functionName, argumentIndex, inputDataType);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002468D File Offset: 0x0002288D
		public static string ArgumentCardinalityMismatch(SRObjectDescriptor objectTypeAndName, FunctionName functionName, int argumentIndex)
		{
			return SRErrors.Keys.GetString("ArgumentCardinalityMismatch", objectTypeAndName, functionName, argumentIndex);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000246AB File Offset: 0x000228AB
		public static string ArgumentValueOutOfRange(SRObjectDescriptor objectTypeAndName, FunctionName functionName, int argumentIndex)
		{
			return SRErrors.Keys.GetString("ArgumentValueOutOfRange", objectTypeAndName, functionName, argumentIndex);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x000246C9 File Offset: 0x000228C9
		public static string InvalidDateIntervalArgument(SRObjectDescriptor objectTypeAndName, FunctionName functionName, int argumentIndex)
		{
			return SRErrors.Keys.GetString("InvalidDateIntervalArgument", objectTypeAndName, functionName, argumentIndex);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x000246E7 File Offset: 0x000228E7
		public static string InvalidDateIntervalValue(SRObjectDescriptor objectTypeAndName, FunctionName functionName, int argumentIndex, string actualValue, string expectedValues)
		{
			return SRErrors.Keys.GetString("InvalidDateIntervalValue", objectTypeAndName, functionName, argumentIndex, actualValue, expectedValues);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00024708 File Offset: 0x00022908
		public static string InvalidInSetArgument(SRObjectDescriptor objectTypeAndName, FunctionName functionName, int argumentIndex)
		{
			return SRErrors.Keys.GetString("InvalidInSetArgument", objectTypeAndName, functionName, argumentIndex);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00024726 File Offset: 0x00022926
		public static string InvalidLiteralSetArgument(SRObjectDescriptor objectTypeAndName, FunctionName functionName, int argumentIndex)
		{
			return SRErrors.Keys.GetString("InvalidLiteralSetArgument", objectTypeAndName, functionName, argumentIndex);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00024744 File Offset: 0x00022944
		public static string ImplicitDecimalCastToFloat(SRObjectDescriptor objectTypeAndName, FunctionName functionName, int argumentIndex)
		{
			return SRErrors.Keys.GetString("ImplicitDecimalCastToFloat", objectTypeAndName, functionName, argumentIndex);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00024762 File Offset: 0x00022962
		public static string EntityKeyTypeMismatch(SRObjectDescriptor objectTypeAndName, FunctionName functionName, int argumentIndex, string entity1Name, string entity2Name)
		{
			return SRErrors.Keys.GetString("EntityKeyTypeMismatch", objectTypeAndName, functionName, argumentIndex, entity1Name, entity2Name);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00024783 File Offset: 0x00022983
		public static string MissingExpressionName(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingExpressionName", objectTypeAndName);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00024795 File Offset: 0x00022995
		public static string TopLevelSetExpression(SRObjectDescriptor expressionTypeAndName, SRObjectDescriptor entityTypeAndName)
		{
			return SRErrors.Keys.GetString("TopLevelSetExpression", expressionTypeAndName, entityTypeAndName);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000247AD File Offset: 0x000229AD
		public static string TopLevelSetExpression_Attribute(SRObjectDescriptor objectTypeAndName, SRObjectDescriptor entityTypeAndName)
		{
			return SRErrors.Keys.GetString("TopLevelSetExpression_Attribute", objectTypeAndName, entityTypeAndName);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000247C5 File Offset: 0x000229C5
		public static string TopLevelSetExpression_Filter(SRObjectDescriptor objectTypeAndName, SRObjectDescriptor entityTypeAndName)
		{
			return SRErrors.Keys.GetString("TopLevelSetExpression_Filter", objectTypeAndName, entityTypeAndName);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000247DD File Offset: 0x000229DD
		public static string EmptySemanticQuery(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("EmptySemanticQuery", objectTypeAndName);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000247EF File Offset: 0x000229EF
		public static string MultipleHierarchies(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MultipleHierarchies", objectTypeAndName);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00024801 File Offset: 0x00022A01
		public static string MultipleMeasureGroups(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MultipleMeasureGroups", objectTypeAndName);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00024813 File Offset: 0x00022A13
		public static string DuplicateGroupingName(SRObjectDescriptor objectTypeAndName, string groupingName)
		{
			return SRErrors.Keys.GetString("DuplicateGroupingName", objectTypeAndName, groupingName);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00024826 File Offset: 0x00022A26
		public static string DuplicateExpressionName(SRObjectDescriptor objectTypeAndName, string expressionName)
		{
			return SRErrors.Keys.GetString("DuplicateExpressionName", objectTypeAndName, expressionName);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00024839 File Offset: 0x00022A39
		public static string MissingBaseEntity(string propertyName, SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingBaseEntity", propertyName, objectTypeAndName);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002484C File Offset: 0x00022A4C
		public static string MissingBaseEntity_MultipleProperties(string propertyName, SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingBaseEntity_MultipleProperties", propertyName, objectTypeAndName);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002485F File Offset: 0x00022A5F
		public static string MissingGroupingName(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingGroupingName", objectTypeAndName);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00024871 File Offset: 0x00022A71
		public static string MissingGroupingExpression(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingGroupingExpression", objectTypeAndName);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00024883 File Offset: 0x00022A83
		public static string BinaryGroupingExpression(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("BinaryGroupingExpression", objectTypeAndName);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00024895 File Offset: 0x00022A95
		public static string NonEntityGroupingWithDetails(SRObjectDescriptor objectTypeAndName, SRObjectDescriptor expressionTypeAndName)
		{
			return SRErrors.Keys.GetString("NonEntityGroupingWithDetails", objectTypeAndName, expressionTypeAndName);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000248AD File Offset: 0x00022AAD
		public static string InvalidFilter(SRObjectDescriptor objectTypeAndName, DataType dataType)
		{
			return SRErrors.Keys.GetString("InvalidFilter", objectTypeAndName, dataType);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000248C5 File Offset: 0x00022AC5
		public static string BaseEntityMismatch(string propertyName, SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("BaseEntityMismatch", propertyName, objectTypeAndName);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000248D8 File Offset: 0x00022AD8
		public static string MissingMeasures(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingMeasures", objectTypeAndName);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000248EA File Offset: 0x00022AEA
		public static string DuplicateParameterName(SRObjectDescriptor objectTypeAndName, string parameterName)
		{
			return SRErrors.Keys.GetString("DuplicateParameterName", objectTypeAndName, parameterName);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000248FD File Offset: 0x00022AFD
		public static string MissingParameterName(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("MissingParameterName", objectTypeAndName);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002490F File Offset: 0x00022B0F
		public static string InvalidParameterName(SRObjectDescriptor objectTypeAndName, string parameterName, string reservedName)
		{
			return SRErrors.Keys.GetString("InvalidParameterName", objectTypeAndName, parameterName, reservedName);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00024923 File Offset: 0x00022B23
		public static string InvalidParameterExpression(SRObjectDescriptor objectTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidParameterExpression", objectTypeAndName);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00024935 File Offset: 0x00022B35
		public static string ParameterExpressionDataTypeMismatch(SRObjectDescriptor objectTypeAndName, string objectName, DataType parameterDataType, DataType expressionDataType)
		{
			return SRErrors.Keys.GetString("ParameterExpressionDataTypeMismatch", objectTypeAndName, objectName, parameterDataType, expressionDataType);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00024954 File Offset: 0x00022B54
		public static string ParameterExpressionNullableMismatch(SRObjectDescriptor objectTypeAndName, string objectName)
		{
			return SRErrors.Keys.GetString("ParameterExpressionNullableMismatch", objectTypeAndName, objectName);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00024967 File Offset: 0x00022B67
		public static string ParameterExpressionCardinalityMismatch(SRObjectDescriptor objectTypeAndName, string objectName)
		{
			return SRErrors.Keys.GetString("ParameterExpressionCardinalityMismatch", objectTypeAndName, objectName);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002497A File Offset: 0x00022B7A
		public static string InvalidParameterValueType(string parameterName, DataType dataType)
		{
			return SRErrors.Keys.GetString("InvalidParameterValueType", parameterName, dataType);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002498D File Offset: 0x00022B8D
		public static string InvalidParameterValueType_MultipleValues(string parameterName, DataType dataType)
		{
			return SRErrors.Keys.GetString("InvalidParameterValueType_MultipleValues", parameterName, dataType);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000249A0 File Offset: 0x00022BA0
		public static string InvalidParameterValueCardinality(string parameterName)
		{
			return SRErrors.Keys.GetString("InvalidParameterValueCardinality", parameterName);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000249AD File Offset: 0x00022BAD
		public static string NullParameterValue(string parameterName)
		{
			return SRErrors.Keys.GetString("NullParameterValue", parameterName);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000249BA File Offset: 0x00022BBA
		public static string MissingParameterValue(string parameterName)
		{
			return SRErrors.Keys.GetString("MissingParameterValue", parameterName);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000249C7 File Offset: 0x00022BC7
		public static string UnusedParameterValue(SRObjectDescriptor objectTypeAndName, string parameterName)
		{
			return SRErrors.Keys.GetString("UnusedParameterValue", objectTypeAndName, parameterName);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000249DA File Offset: 0x00022BDA
		public static string InvalidDrillSelectedPath(ExpressionPath selectedPath, ExpressionPath maxAllowedPath)
		{
			return SRErrors.Keys.GetString("InvalidDrillSelectedPath", selectedPath, maxAllowedPath);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000249E8 File Offset: 0x00022BE8
		public static string InvalidDrillTargetEntity(SRObjectDescriptor objectTypeAndName, SRObjectDescriptor queryBaseEntity, SRObjectDescriptor pathTargetEntity)
		{
			return SRErrors.Keys.GetString("InvalidDrillTargetEntity", objectTypeAndName, queryBaseEntity, pathTargetEntity);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00024A06 File Offset: 0x00022C06
		public static string LoopInSecurityFilters(SRObjectDescriptor entityTypeAndName)
		{
			return SRErrors.Keys.GetString("LoopInSecurityFilters", entityTypeAndName);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00024A18 File Offset: 0x00022C18
		public static string SemanticModel_PerspectiveNotFound(QName perspectiveID)
		{
			return SRErrors.Keys.GetString("SemanticModel_PerspectiveNotFound", perspectiveID);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00024A2A File Offset: 0x00022C2A
		public static string WrongSemanticModel_ModelItem(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor referencedTypeAndName)
		{
			return SRErrors.Keys.GetString("WrongSemanticModel_ModelItem", propertyName, objectTypeAndName, referencedTypeAndName);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00024A43 File Offset: 0x00022C43
		public static string WrongSemanticModel_QueryItem(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor referencedTypeAndName)
		{
			return SRErrors.Keys.GetString("WrongSemanticModel_QueryItem", propertyName, objectTypeAndName, referencedTypeAndName);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00024A5C File Offset: 0x00022C5C
		public static string WrongSemanticModel_QueryItemMultipleProperties(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor referencedTypeAndName)
		{
			return SRErrors.Keys.GetString("WrongSemanticModel_QueryItemMultipleProperties", propertyName, objectTypeAndName, referencedTypeAndName);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00024A75 File Offset: 0x00022C75
		public static string WrongSemanticQuery(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor referencedTypeAndName)
		{
			return SRErrors.Keys.GetString("WrongSemanticQuery", propertyName, objectTypeAndName, referencedTypeAndName);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00024A8E File Offset: 0x00022C8E
		public static string WrongSemanticQuery_MultipleProperties(string propertyName, SRObjectDescriptor objectTypeAndName, SRObjectDescriptor referencedTypeAndName)
		{
			return SRErrors.Keys.GetString("WrongSemanticQuery_MultipleProperties", propertyName, objectTypeAndName, referencedTypeAndName);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00024AA7 File Offset: 0x00022CA7
		public static string InvalidEntityKeyValue(string base64Value, SRObjectDescriptor entityTypeAndName)
		{
			return SRErrors.Keys.GetString("InvalidEntityKeyValue", base64Value, entityTypeAndName);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00024ABA File Offset: 0x00022CBA
		public static string InvalidEntityKeyPart(object partValue, Type expectedType)
		{
			return SRErrors.Keys.GetString("InvalidEntityKeyPart", partValue, expectedType);
		}

		// Token: 0x020001BB RID: 443
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06001130 RID: 4400 RVA: 0x00035EC7 File Offset: 0x000340C7
			private Keys()
			{
			}

			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x06001131 RID: 4401 RVA: 0x00035ECF File Offset: 0x000340CF
			// (set) Token: 0x06001132 RID: 4402 RVA: 0x00035ED6 File Offset: 0x000340D6
			public static CultureInfo Culture
			{
				get
				{
					return SRErrors.Keys._culture;
				}
				set
				{
					SRErrors.Keys._culture = value;
				}
			}

			// Token: 0x06001133 RID: 4403 RVA: 0x00035EDE File Offset: 0x000340DE
			public static string GetString(string key)
			{
				return SRErrors.Keys.resourceManager.GetString(key, SRErrors.Keys._culture);
			}

			// Token: 0x06001134 RID: 4404 RVA: 0x00035EF0 File Offset: 0x000340F0
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, SRErrors.Keys.resourceManager.GetString(key, SRErrors.Keys._culture), arg0);
			}

			// Token: 0x06001135 RID: 4405 RVA: 0x00035F0D File Offset: 0x0003410D
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, SRErrors.Keys.resourceManager.GetString(key, SRErrors.Keys._culture), arg0, arg1);
			}

			// Token: 0x06001136 RID: 4406 RVA: 0x00035F2B File Offset: 0x0003412B
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, SRErrors.Keys.resourceManager.GetString(key, SRErrors.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x06001137 RID: 4407 RVA: 0x00035F4A File Offset: 0x0003414A
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3)
			{
				return string.Format(CultureInfo.CurrentCulture, SRErrors.Keys.resourceManager.GetString(key, SRErrors.Keys._culture), new object[] { arg0, arg1, arg2, arg3 });
			}

			// Token: 0x06001138 RID: 4408 RVA: 0x00035F7D File Offset: 0x0003417D
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3, object arg4)
			{
				return string.Format(CultureInfo.CurrentCulture, SRErrors.Keys.resourceManager.GetString(key, SRErrors.Keys._culture), new object[] { arg0, arg1, arg2, arg3, arg4 });
			}

			// Token: 0x06001139 RID: 4409 RVA: 0x00035FB5 File Offset: 0x000341B5
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5)
			{
				return string.Format(CultureInfo.CurrentCulture, SRErrors.Keys.resourceManager.GetString(key, SRErrors.Keys._culture), new object[] { arg0, arg1, arg2, arg3, arg4, arg5 });
			}

			// Token: 0x04000728 RID: 1832
			private static ResourceManager resourceManager = new ResourceManager(typeof(SRErrors).FullName, typeof(SRErrors).Module.Assembly);

			// Token: 0x04000729 RID: 1833
			private static CultureInfo _culture = null;

			// Token: 0x0400072A RID: 1834
			public const string InternalError = "InternalError";

			// Token: 0x0400072B RID: 1835
			public const string ValidationException_MessageWithAdditionalMessages = "ValidationException_MessageWithAdditionalMessages";

			// Token: 0x0400072C RID: 1836
			public const string XmlValidationException_MessageWithLineInfo = "XmlValidationException_MessageWithLineInfo";

			// Token: 0x0400072D RID: 1837
			public const string InvalidDataSourceView = "InvalidDataSourceView";

			// Token: 0x0400072E RID: 1838
			public const string InvalidSemanticModel = "InvalidSemanticModel";

			// Token: 0x0400072F RID: 1839
			public const string InvalidSemanticQuery = "InvalidSemanticQuery";

			// Token: 0x04000730 RID: 1840
			public const string InvalidDrillthroughContext = "InvalidDrillthroughContext";

			// Token: 0x04000731 RID: 1841
			public const string InvalidModelGenerationRules = "InvalidModelGenerationRules";

			// Token: 0x04000732 RID: 1842
			public const string Xml_NodeMismatch = "Xml_NodeMismatch";

			// Token: 0x04000733 RID: 1843
			public const string ObjectDescriptor_TypeAndName = "ObjectDescriptor_TypeAndName";

			// Token: 0x04000734 RID: 1844
			public const string InvalidCulture = "InvalidCulture";

			// Token: 0x04000735 RID: 1845
			public const string DuplicateItemID = "DuplicateItemID";

			// Token: 0x04000736 RID: 1846
			public const string InvalidEntityBinding = "InvalidEntityBinding";

			// Token: 0x04000737 RID: 1847
			public const string NestedVariations = "NestedVariations";

			// Token: 0x04000738 RID: 1848
			public const string InvalidLinguistics = "InvalidLinguistics";

			// Token: 0x04000739 RID: 1849
			public const string MissingRelationEnd = "MissingRelationEnd";

			// Token: 0x0400073A RID: 1850
			public const string InvalidExpression = "InvalidExpression";

			// Token: 0x0400073B RID: 1851
			public const string InvalidExpression_TopLevel = "InvalidExpression_TopLevel";

			// Token: 0x0400073C RID: 1852
			public const string InvalidFunctionName = "InvalidFunctionName";

			// Token: 0x0400073D RID: 1853
			public const string InvalidAttributeRef = "InvalidAttributeRef";

			// Token: 0x0400073E RID: 1854
			public const string InvalidLiteral = "InvalidLiteral";

			// Token: 0x0400073F RID: 1855
			public const string InvalidLiteralValue = "InvalidLiteralValue";

			// Token: 0x04000740 RID: 1856
			public const string ItemNotFound = "ItemNotFound";

			// Token: 0x04000741 RID: 1857
			public const string ItemNotFound_MultipleProperties = "ItemNotFound_MultipleProperties";

			// Token: 0x04000742 RID: 1858
			public const string InvalidReferencedItem = "InvalidReferencedItem";

			// Token: 0x04000743 RID: 1859
			public const string InvalidReferencedItem_MultipleProperties = "InvalidReferencedItem_MultipleProperties";

			// Token: 0x04000744 RID: 1860
			public const string CircularInheritance = "CircularInheritance";

			// Token: 0x04000745 RID: 1861
			public const string SelfReferentialRole = "SelfReferentialRole";

			// Token: 0x04000746 RID: 1862
			public const string GroupingNotFound = "GroupingNotFound";

			// Token: 0x04000747 RID: 1863
			public const string GroupingNotFound_MultipleProperties = "GroupingNotFound_MultipleProperties";

			// Token: 0x04000748 RID: 1864
			public const string MeasureNotFound = "MeasureNotFound";

			// Token: 0x04000749 RID: 1865
			public const string MeasureNotFound_MultipleProperties = "MeasureNotFound_MultipleProperties";

			// Token: 0x0400074A RID: 1866
			public const string CalculatedAttributeNotFound = "CalculatedAttributeNotFound";

			// Token: 0x0400074B RID: 1867
			public const string CalculatedAttributeNotFound_MultipleProperties = "CalculatedAttributeNotFound_MultipleProperties";

			// Token: 0x0400074C RID: 1868
			public const string ParameterNotFound = "ParameterNotFound";

			// Token: 0x0400074D RID: 1869
			public const string ParameterNotFound_MultipleProperties = "ParameterNotFound_MultipleProperties";

			// Token: 0x0400074E RID: 1870
			public const string ResultExpressionNotFound = "ResultExpressionNotFound";

			// Token: 0x0400074F RID: 1871
			public const string ResultExpressionNotFound_MultipleProperties = "ResultExpressionNotFound_MultipleProperties";

			// Token: 0x04000750 RID: 1872
			public const string MissingItemName = "MissingItemName";

			// Token: 0x04000751 RID: 1873
			public const string IDLocalNameLengthExceeded = "IDLocalNameLengthExceeded";

			// Token: 0x04000752 RID: 1874
			public const string IDLocalNameLengthExceeded_NoContext = "IDLocalNameLengthExceeded_NoContext";

			// Token: 0x04000753 RID: 1875
			public const string IDNamespaceLengthExceeded = "IDNamespaceLengthExceeded";

			// Token: 0x04000754 RID: 1876
			public const string IDNamespaceLengthExceeded_NoContext = "IDNamespaceLengthExceeded_NoContext";

			// Token: 0x04000755 RID: 1877
			public const string InvalidGuid = "InvalidGuid";

			// Token: 0x04000756 RID: 1878
			public const string InvalidGuid_NoContext = "InvalidGuid_NoContext";

			// Token: 0x04000757 RID: 1879
			public const string DuplicateItemName = "DuplicateItemName";

			// Token: 0x04000758 RID: 1880
			public const string DuplicateEntityName = "DuplicateEntityName";

			// Token: 0x04000759 RID: 1881
			public const string DuplicateFieldName = "DuplicateFieldName";

			// Token: 0x0400075A RID: 1882
			public const string MissingIdentifyingAttributes = "MissingIdentifyingAttributes";

			// Token: 0x0400075B RID: 1883
			public const string InvalidSetAttributeReference = "InvalidSetAttributeReference";

			// Token: 0x0400075C RID: 1884
			public const string InvalidAggregateAttributeReference = "InvalidAggregateAttributeReference";

			// Token: 0x0400075D RID: 1885
			public const string InvalidScalarAttributeReference = "InvalidScalarAttributeReference";

			// Token: 0x0400075E RID: 1886
			public const string InvalidNonFilterAttributeReference = "InvalidNonFilterAttributeReference";

			// Token: 0x0400075F RID: 1887
			public const string InvalidHiddenAttributeReference = "InvalidHiddenAttributeReference";

			// Token: 0x04000760 RID: 1888
			public const string ExpressionDataTypeMismatch = "ExpressionDataTypeMismatch";

			// Token: 0x04000761 RID: 1889
			public const string ExpressionNullableMismatch = "ExpressionNullableMismatch";

			// Token: 0x04000762 RID: 1890
			public const string MissingMimeType = "MissingMimeType";

			// Token: 0x04000763 RID: 1891
			public const string IsAggregateWithDefaultAggregate = "IsAggregateWithDefaultAggregate";

			// Token: 0x04000764 RID: 1892
			public const string NonAggregateAsDefaultAggregate = "NonAggregateAsDefaultAggregate";

			// Token: 0x04000765 RID: 1893
			public const string NonVariationAsDefaultAggregate = "NonVariationAsDefaultAggregate";

			// Token: 0x04000766 RID: 1894
			public const string MissingRelatedRole = "MissingRelatedRole";

			// Token: 0x04000767 RID: 1895
			public const string RelatedRolesMismatch = "RelatedRolesMismatch";

			// Token: 0x04000768 RID: 1896
			public const string InvalidOptionalityOfRoleForColumnBoundEntity = "InvalidOptionalityOfRoleForColumnBoundEntity";

			// Token: 0x04000769 RID: 1897
			public const string InvalidModelItemInPerspective = "InvalidModelItemInPerspective";

			// Token: 0x0400076A RID: 1898
			public const string MissingDataSourceView = "MissingDataSourceView";

			// Token: 0x0400076B RID: 1899
			public const string MissingBinding_Entity = "MissingBinding_Entity";

			// Token: 0x0400076C RID: 1900
			public const string MissingBinding_Inheritance = "MissingBinding_Inheritance";

			// Token: 0x0400076D RID: 1901
			public const string MissingBinding_Attribute = "MissingBinding_Attribute";

			// Token: 0x0400076E RID: 1902
			public const string MissingBinding_Role = "MissingBinding_Role";

			// Token: 0x0400076F RID: 1903
			public const string InvalidBinding = "InvalidBinding";

			// Token: 0x04000770 RID: 1904
			public const string InvalidColumnReferenceInColumnEntity = "InvalidColumnReferenceInColumnEntity";

			// Token: 0x04000771 RID: 1905
			public const string MissingColumnTableName = "MissingColumnTableName";

			// Token: 0x04000772 RID: 1906
			public const string InvalidColumnTableName = "InvalidColumnTableName";

			// Token: 0x04000773 RID: 1907
			public const string InvalidColumnDataType = "InvalidColumnDataType";

			// Token: 0x04000774 RID: 1908
			public const string NonPrimaryDataSource = "NonPrimaryDataSource";

			// Token: 0x04000775 RID: 1909
			public const string MissingPrimaryKey = "MissingPrimaryKey";

			// Token: 0x04000776 RID: 1910
			public const string BinaryEntityColumn = "BinaryEntityColumn";

			// Token: 0x04000777 RID: 1911
			public const string InvalidInheritanceRelationTable = "InvalidInheritanceRelationTable";

			// Token: 0x04000778 RID: 1912
			public const string NonUniqueInheritanceRelationColumns = "NonUniqueInheritanceRelationColumns";

			// Token: 0x04000779 RID: 1913
			public const string ColumnDataTypeMismatch = "ColumnDataTypeMismatch";

			// Token: 0x0400077A RID: 1914
			public const string ColumnNullableMismatch = "ColumnNullableMismatch";

			// Token: 0x0400077B RID: 1915
			public const string IsAggregateWithColumn = "IsAggregateWithColumn";

			// Token: 0x0400077C RID: 1916
			public const string PromoteLookupForNonLookupEntity = "PromoteLookupForNonLookupEntity";

			// Token: 0x0400077D RID: 1917
			public const string RoleRelationsMismatch = "RoleRelationsMismatch";

			// Token: 0x0400077E RID: 1918
			public const string RoleRelationEndsMismatch = "RoleRelationEndsMismatch";

			// Token: 0x0400077F RID: 1919
			public const string InvalidRoleRelationTable = "InvalidRoleRelationTable";

			// Token: 0x04000780 RID: 1920
			public const string NonUniqueRoleRelationColumns = "NonUniqueRoleRelationColumns";

			// Token: 0x04000781 RID: 1921
			public const string NonBooleanFilterAttribute = "NonBooleanFilterAttribute";

			// Token: 0x04000782 RID: 1922
			public const string CyclicExpression = "CyclicExpression";

			// Token: 0x04000783 RID: 1923
			public const string CyclicExpression_ExpressionObject = "CyclicExpression_ExpressionObject";

			// Token: 0x04000784 RID: 1924
			public const string FieldReferenceOutOfContext = "FieldReferenceOutOfContext";

			// Token: 0x04000785 RID: 1925
			public const string FieldReferenceOutOfContext_MultipleProperties = "FieldReferenceOutOfContext_MultipleProperties";

			// Token: 0x04000786 RID: 1926
			public const string EntityReferenceOutOfContext = "EntityReferenceOutOfContext";

			// Token: 0x04000787 RID: 1927
			public const string EntityReferenceOutOfContext_MultipleProperties = "EntityReferenceOutOfContext_MultipleProperties";

			// Token: 0x04000788 RID: 1928
			public const string NonAggregateExpression_Attribute = "NonAggregateExpression_Attribute";

			// Token: 0x04000789 RID: 1929
			public const string NonAggregateExpression_Measure = "NonAggregateExpression_Measure";

			// Token: 0x0400078A RID: 1930
			public const string AggregateWithNonAggregateArgument = "AggregateWithNonAggregateArgument";

			// Token: 0x0400078B RID: 1931
			public const string WrongNumberOfArguments = "WrongNumberOfArguments";

			// Token: 0x0400078C RID: 1932
			public const string ArgumentDataTypeMismatch = "ArgumentDataTypeMismatch";

			// Token: 0x0400078D RID: 1933
			public const string ArgumentCardinalityMismatch = "ArgumentCardinalityMismatch";

			// Token: 0x0400078E RID: 1934
			public const string ArgumentValueOutOfRange = "ArgumentValueOutOfRange";

			// Token: 0x0400078F RID: 1935
			public const string InvalidDateIntervalArgument = "InvalidDateIntervalArgument";

			// Token: 0x04000790 RID: 1936
			public const string InvalidDateIntervalValue = "InvalidDateIntervalValue";

			// Token: 0x04000791 RID: 1937
			public const string InvalidInSetArgument = "InvalidInSetArgument";

			// Token: 0x04000792 RID: 1938
			public const string InvalidLiteralSetArgument = "InvalidLiteralSetArgument";

			// Token: 0x04000793 RID: 1939
			public const string ImplicitDecimalCastToFloat = "ImplicitDecimalCastToFloat";

			// Token: 0x04000794 RID: 1940
			public const string EntityKeyTypeMismatch = "EntityKeyTypeMismatch";

			// Token: 0x04000795 RID: 1941
			public const string MissingExpressionName = "MissingExpressionName";

			// Token: 0x04000796 RID: 1942
			public const string TopLevelSetExpression = "TopLevelSetExpression";

			// Token: 0x04000797 RID: 1943
			public const string TopLevelSetExpression_Attribute = "TopLevelSetExpression_Attribute";

			// Token: 0x04000798 RID: 1944
			public const string TopLevelSetExpression_Filter = "TopLevelSetExpression_Filter";

			// Token: 0x04000799 RID: 1945
			public const string EmptySemanticQuery = "EmptySemanticQuery";

			// Token: 0x0400079A RID: 1946
			public const string MultipleHierarchies = "MultipleHierarchies";

			// Token: 0x0400079B RID: 1947
			public const string MultipleMeasureGroups = "MultipleMeasureGroups";

			// Token: 0x0400079C RID: 1948
			public const string DuplicateGroupingName = "DuplicateGroupingName";

			// Token: 0x0400079D RID: 1949
			public const string DuplicateExpressionName = "DuplicateExpressionName";

			// Token: 0x0400079E RID: 1950
			public const string MissingBaseEntity = "MissingBaseEntity";

			// Token: 0x0400079F RID: 1951
			public const string MissingBaseEntity_MultipleProperties = "MissingBaseEntity_MultipleProperties";

			// Token: 0x040007A0 RID: 1952
			public const string MissingGroupingName = "MissingGroupingName";

			// Token: 0x040007A1 RID: 1953
			public const string MissingGroupingExpression = "MissingGroupingExpression";

			// Token: 0x040007A2 RID: 1954
			public const string BinaryGroupingExpression = "BinaryGroupingExpression";

			// Token: 0x040007A3 RID: 1955
			public const string NonEntityGroupingWithDetails = "NonEntityGroupingWithDetails";

			// Token: 0x040007A4 RID: 1956
			public const string InvalidFilter = "InvalidFilter";

			// Token: 0x040007A5 RID: 1957
			public const string BaseEntityMismatch = "BaseEntityMismatch";

			// Token: 0x040007A6 RID: 1958
			public const string MissingMeasures = "MissingMeasures";

			// Token: 0x040007A7 RID: 1959
			public const string DuplicateParameterName = "DuplicateParameterName";

			// Token: 0x040007A8 RID: 1960
			public const string MissingParameterName = "MissingParameterName";

			// Token: 0x040007A9 RID: 1961
			public const string InvalidParameterName = "InvalidParameterName";

			// Token: 0x040007AA RID: 1962
			public const string InvalidParameterExpression = "InvalidParameterExpression";

			// Token: 0x040007AB RID: 1963
			public const string ParameterExpressionDataTypeMismatch = "ParameterExpressionDataTypeMismatch";

			// Token: 0x040007AC RID: 1964
			public const string ParameterExpressionNullableMismatch = "ParameterExpressionNullableMismatch";

			// Token: 0x040007AD RID: 1965
			public const string ParameterExpressionCardinalityMismatch = "ParameterExpressionCardinalityMismatch";

			// Token: 0x040007AE RID: 1966
			public const string InvalidParameterValueType = "InvalidParameterValueType";

			// Token: 0x040007AF RID: 1967
			public const string InvalidParameterValueType_MultipleValues = "InvalidParameterValueType_MultipleValues";

			// Token: 0x040007B0 RID: 1968
			public const string InvalidParameterValueCardinality = "InvalidParameterValueCardinality";

			// Token: 0x040007B1 RID: 1969
			public const string NullParameterValue = "NullParameterValue";

			// Token: 0x040007B2 RID: 1970
			public const string MissingParameterValue = "MissingParameterValue";

			// Token: 0x040007B3 RID: 1971
			public const string UnusedParameterValue = "UnusedParameterValue";

			// Token: 0x040007B4 RID: 1972
			public const string InvalidDrillSelectedItems = "InvalidDrillSelectedItems";

			// Token: 0x040007B5 RID: 1973
			public const string InvalidDrillSelectedPath = "InvalidDrillSelectedPath";

			// Token: 0x040007B6 RID: 1974
			public const string InvalidDrillTargetEntity = "InvalidDrillTargetEntity";

			// Token: 0x040007B7 RID: 1975
			public const string LoopInSecurityFilters = "LoopInSecurityFilters";

			// Token: 0x040007B8 RID: 1976
			public const string SemanticModel_PerspectiveNotFound = "SemanticModel_PerspectiveNotFound";

			// Token: 0x040007B9 RID: 1977
			public const string WrongSemanticModel_ModelItem = "WrongSemanticModel_ModelItem";

			// Token: 0x040007BA RID: 1978
			public const string WrongSemanticModel_QueryItem = "WrongSemanticModel_QueryItem";

			// Token: 0x040007BB RID: 1979
			public const string WrongSemanticModel_QueryItemMultipleProperties = "WrongSemanticModel_QueryItemMultipleProperties";

			// Token: 0x040007BC RID: 1980
			public const string WrongSemanticQuery = "WrongSemanticQuery";

			// Token: 0x040007BD RID: 1981
			public const string WrongSemanticQuery_MultipleProperties = "WrongSemanticQuery_MultipleProperties";

			// Token: 0x040007BE RID: 1982
			public const string InvalidEntityKeyValue = "InvalidEntityKeyValue";

			// Token: 0x040007BF RID: 1983
			public const string InvalidEntityKeyPart = "InvalidEntityKeyPart";
		}
	}
}
