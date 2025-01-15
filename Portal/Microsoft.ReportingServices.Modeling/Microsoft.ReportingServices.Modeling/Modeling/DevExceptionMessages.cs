using System;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200004A RID: 74
	internal static class DevExceptionMessages
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000AB7A File Offset: 0x00008D7A
		internal static string ReadOnly
		{
			get
			{
				return "The object is read-only.";
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000AB81 File Offset: 0x00008D81
		internal static string ExistingOwner
		{
			get
			{
				return "The object has already been assigned to an owner; must be removed from existing owner before assigning a new owner.";
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000AB88 File Offset: 0x00008D88
		internal static string XmlNamespacePrefixCollection_DefaultNamespaceNotAllowed
		{
			get
			{
				return "A default namespace cannot be added to the XmlNamespacePrefixCollection.";
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000AB8F File Offset: 0x00008D8F
		internal static string ModelItem_BaseItemMustBeCompiled
		{
			get
			{
				return "Base item must be compiled.";
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000AB96 File Offset: 0x00008D96
		internal static string ModelEntity_NoIdentifyingAttributes
		{
			get
			{
				return "ModelEntity.IdentifyingAttributes must contain at least one AttributeReference.";
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000AB9D File Offset: 0x00008D9D
		internal static string ModelEntity_LinguisticsSet
		{
			get
			{
				return "ModelEntity.Linguistics properties cannot be set. They are simple wrappers for the Name and CollectionName properties.";
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000ABA4 File Offset: 0x00008DA4
		internal static string ModelEntity_InvalidBindingSet
		{
			get
			{
				return "ModelEntity.Binding may only be set to a TableBinding or ColumnBinding object.";
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000ABAB File Offset: 0x00008DAB
		internal static string ModelAttribute_DataTypeNull
		{
			get
			{
				return "ModelAttribute.DataType cannot be set to DataType.Null.";
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000ABB2 File Offset: 0x00008DB2
		internal static string ModelAttribute_WidthLessThanZero
		{
			get
			{
				return "ModelAttribute.Width cannot be less than zero.";
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000ABB9 File Offset: 0x00008DB9
		internal static string AttributeRefNode_UnexpectedIQueryAttribute
		{
			get
			{
				return "The specified value is not an expected implementation of IQueryAttribute.";
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		internal static string EntityRefNode_UnexpectedIQueryEntity
		{
			get
			{
				return "The specified value is not an expected implementation of IQueryEntity.";
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000ABC7 File Offset: 0x00008DC7
		internal static string TargetEntityInternal_UnexpectedIQueryEntityInternal
		{
			get
			{
				return "The TargetEntityInternal value is not an expected implementation of IQueryEntityInternal.";
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000ABCE File Offset: 0x00008DCE
		internal static string LiteralNode_DataTypeMismatch
		{
			get
			{
				return "Literal data type does not match the requested data type.";
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000ABD5 File Offset: 0x00008DD5
		internal static string LiteralNode_ScalarRequired
		{
			get
			{
				return "A single-value literal is required.";
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000ABDC File Offset: 0x00008DDC
		internal static string LiteralNode_SetRequired
		{
			get
			{
				return "A literal set is required.";
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000ABE3 File Offset: 0x00008DE3
		internal static string Parameter_DataTypeBinaryOrNull
		{
			get
			{
				return "Parameter.DataType cannot be set to DataType.Binary or DataType.Null.";
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000ABEA File Offset: 0x00008DEA
		internal static string SemanticQuery_NullModel
		{
			get
			{
				return "The SemanticQuery.Model must be set.";
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000ABF1 File Offset: 0x00008DF1
		internal static string SemanticQuery_ModelMustBeCompiled
		{
			get
			{
				return "The SemanticQuery.Model must be compiled.";
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		internal static string XmlReaderCheckCharsTrue
		{
			get
			{
				return "XmlReader.Settings.CheckCharacters must be false.";
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000ABFF File Offset: 0x00008DFF
		internal static string XmlWriterCheckCharsTrue
		{
			get
			{
				return "XmlWriter.Settings.CheckCharacters must be false.";
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000AC06 File Offset: 0x00008E06
		internal static string Xml_UnexpectedValueType(Type type)
		{
			return StringUtil.FormatInvariant("The type \"{0}\" does not have an appropriate mapping to an XML data type.", new object[] { type });
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000AC1C File Offset: 0x00008E1C
		internal static string Xml_ReferenceToUnnamedObject(SRObjectDescriptor objectTypeAndName)
		{
			return StringUtil.FormatInvariant("Cannot write a reference to an unnamed {0}.", new object[] { objectTypeAndName });
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000AC37 File Offset: 0x00008E37
		internal static string SemanticModel_InvalidPerspectiveItem(SRObjectDescriptor referencedItem)
		{
			return StringUtil.FormatInvariant("The referenced {0} is not a perspective.", new object[] { referencedItem });
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000AC52 File Offset: 0x00008E52
		internal static string ModelItem_InvalidParentItemType(string itemType, string parentType)
		{
			return StringUtil.FormatInvariant("{0} items cannot be contained within {1} items.", new object[] { itemType, parentType });
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000AC6C File Offset: 0x00008E6C
		internal static string EntityKey_InvalidKeyPartType(Type type)
		{
			return StringUtil.FormatInvariant("The type \"{0}\" is not supported for an EntityKey part.", new object[] { type });
		}
	}
}
