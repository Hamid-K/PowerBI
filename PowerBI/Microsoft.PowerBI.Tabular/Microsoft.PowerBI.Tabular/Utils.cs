using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200011B RID: 283
	public static class Utils
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x0007EA64 File Offset: 0x0007CC64
		public static string GetSyntacticallyValidName(string baseName, ObjectType type)
		{
			if (!ObjectTreeHelper.IsNamedObject(type))
			{
				throw new ArgumentOutOfRangeException("type", TomSR.Exception_ObjectDoesNotHaveName(type.ToString()));
			}
			if (baseName == null)
			{
				return Utils.GetDefaultNameForObjectType(type);
			}
			string text = Utils.RemoveInvalidXmlChars(baseName);
			int maxNameLengthForObjectType = Utils.GetMaxNameLengthForObjectType(type);
			if (text.Length > maxNameLengthForObjectType)
			{
				text = text.Substring(0, maxNameLengthForObjectType);
			}
			if (string.IsNullOrEmpty(text))
			{
				return Utils.GetDefaultNameForObjectType(type);
			}
			if (Utils.IsReservedName(text, type))
			{
				return TomSR.ValidNameForReservedStringPattern(text);
			}
			return text;
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0007EAE4 File Offset: 0x0007CCE4
		internal static bool IsSyntacticallyValidName(string name, ObjectType type, char[] invalidCharacters, out string error)
		{
			error = null;
			if (string.IsNullOrEmpty(name))
			{
				error = TomSR.Error_NameIsRequired;
				return false;
			}
			if (!Utils.IsValidXmlChars(name))
			{
				error = TomSR.Error_NameHasInvalidXmlCharacters(name);
				return false;
			}
			int maxNameLengthForObjectType = Utils.GetMaxNameLengthForObjectType(type);
			if (name.Length > maxNameLengthForObjectType)
			{
				error = TomSR.Error_NameIsTooLong(name, Utils.GetUserFriendlyNameOfObjectType(type), name.Length.ToString(), maxNameLengthForObjectType.ToString());
				return false;
			}
			if (Utils.IsReservedName(name, type))
			{
				error = TomSR.Error_NameCannotBeReservedString(Utils.GetUserFriendlyNameOfObjectType(type), name);
				return false;
			}
			if (invalidCharacters != null && invalidCharacters.Length != 0 && name.IndexOfAny(invalidCharacters) != -1)
			{
				error = TomSR.Error_ValueHasInvalidCharacters(new string(invalidCharacters));
				return false;
			}
			return true;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0007EB88 File Offset: 0x0007CD88
		public static bool IsSyntacticallyValidName(string name, ObjectType type, out string error)
		{
			switch (type)
			{
			case ObjectType.Model:
			case ObjectType.DataSource:
			case ObjectType.Table:
			case ObjectType.Column:
			case ObjectType.Partition:
			case ObjectType.Relationship:
			case ObjectType.Measure:
			case ObjectType.Hierarchy:
			case ObjectType.Level:
			case ObjectType.Annotation:
			case ObjectType.Culture:
			case ObjectType.Perspective:
			case ObjectType.PerspectiveTable:
			case ObjectType.PerspectiveColumn:
			case ObjectType.PerspectiveHierarchy:
			case ObjectType.PerspectiveMeasure:
			case ObjectType.RoleMembership:
			case ObjectType.TablePermission:
			case ObjectType.Variation:
			case ObjectType.Set:
			case ObjectType.PerspectiveSet:
			case ObjectType.ExtendedProperty:
			case ObjectType.Expression:
			case ObjectType.ColumnPermission:
			case ObjectType.CalculationItem:
			case ObjectType.QueryGroup:
			case ObjectType.AnalyticsAIMetadata:
				break;
			case ObjectType.AttributeHierarchy:
			case ObjectType.KPI:
			case ObjectType.ObjectTranslation:
			case ObjectType.LinguisticMetadata:
			case (ObjectType)16:
			case (ObjectType)17:
			case (ObjectType)18:
			case (ObjectType)19:
			case (ObjectType)20:
			case (ObjectType)21:
			case (ObjectType)22:
			case (ObjectType)23:
			case (ObjectType)24:
			case (ObjectType)25:
			case (ObjectType)26:
			case (ObjectType)27:
			case (ObjectType)28:
			case ObjectType.DetailRowsDefinition:
			case ObjectType.RelatedColumnDetails:
			case ObjectType.GroupByColumn:
			case ObjectType.CalculationGroup:
			case ObjectType.AlternateOf:
			case ObjectType.RefreshPolicy:
			case ObjectType.FormatStringDefinition:
				goto IL_00FF;
			case ObjectType.Role:
				return Utils.IsSyntacticallyValidName(name, ObjectType.Role, Utils.RoleNameInvalidCharacters, out error);
			default:
				if (type != ObjectType.Calendar && type - ObjectType.CalendarColumnReference > 2)
				{
					goto IL_00FF;
				}
				break;
			}
			return Utils.IsSyntacticallyValidName(name, type, null, out error);
			IL_00FF:
			throw new ArgumentOutOfRangeException("type");
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0007ECA0 File Offset: 0x0007CEA0
		private static bool IsReservedName(string name, ObjectType type)
		{
			return type == ObjectType.Table && Utils.tableReservedNames.Any((string rn) => string.Equals(name, rn, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0007ECD8 File Offset: 0x0007CED8
		private static bool IsValidXmlChars(string s)
		{
			try
			{
				XmlConvert.VerifyXmlChars(s);
			}
			catch (XmlException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0007ED08 File Offset: 0x0007CF08
		private static string RemoveInvalidXmlChars(string s)
		{
			string text;
			try
			{
				text = XmlConvert.VerifyXmlChars(s);
			}
			catch (XmlException)
			{
				text = Utils.RemoveInvalidXmlCharsImpl(s);
			}
			return text;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0007ED3C File Offset: 0x0007CF3C
		private static string RemoveInvalidXmlCharsImpl(string s)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < s.Length)
			{
				bool flag = true;
				int num = i;
				do
				{
					char c = s[num];
					if (Utils.IsValidNonSurrogateXmlChar(c))
					{
						num++;
					}
					else if (char.IsHighSurrogate(c))
					{
						if (num == s.Length - 1 || !char.IsLowSurrogate(s, num + 1))
						{
							flag = false;
						}
						else
						{
							num += 2;
						}
					}
					else
					{
						flag = false;
					}
				}
				while (flag && num < s.Length);
				stringBuilder.Append(s, i, num - i);
				if (flag)
				{
					i = s.Length;
				}
				else
				{
					i = num + 1;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0007EDD1 File Offset: 0x0007CFD1
		private static bool IsValidNonSurrogateXmlChar(char c)
		{
			return (' ' <= c && c <= '\ud7ff') || c == '\t' || c == '\n' || c == '\r' || ('\ue000' <= c && c <= '\ufffd');
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0007EE08 File Offset: 0x0007D008
		internal static string GetNamePropertyForObjectType(ObjectType type)
		{
			if (type <= ObjectType.Expression)
			{
				switch (type)
				{
				case ObjectType.Model:
					return "Name";
				case ObjectType.DataSource:
					return "Name";
				case ObjectType.Table:
					return "Name";
				case ObjectType.Column:
					return "ExplicitName";
				case ObjectType.AttributeHierarchy:
				case ObjectType.KPI:
					break;
				case ObjectType.Partition:
					return "Name";
				case ObjectType.Relationship:
					return "Name";
				case ObjectType.Measure:
					return "Name";
				case ObjectType.Hierarchy:
					return "Name";
				case ObjectType.Level:
					return "Name";
				case ObjectType.Annotation:
					return "Name";
				case ObjectType.Culture:
					return "Name";
				default:
					if (type == ObjectType.Perspective)
					{
						return "Name";
					}
					switch (type)
					{
					case ObjectType.Role:
						return "Name";
					case ObjectType.Variation:
						return "Name";
					case ObjectType.Set:
						return "Name";
					case ObjectType.ExtendedProperty:
						return "Name";
					case ObjectType.Expression:
						return "Name";
					}
					break;
				}
			}
			else if (type <= ObjectType.QueryGroup)
			{
				if (type == ObjectType.CalculationItem)
				{
					return "Name";
				}
				if (type == ObjectType.QueryGroup)
				{
					return "Folder";
				}
			}
			else
			{
				if (type == ObjectType.AnalyticsAIMetadata)
				{
					return "Name";
				}
				switch (type)
				{
				case ObjectType.Calendar:
					return "Name";
				case ObjectType.Function:
					return "Name";
				case ObjectType.BindingInfo:
					return "Name";
				}
			}
			return "";
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0007EF60 File Offset: 0x0007D160
		internal static int GetMaxNameLengthForObjectType(ObjectType type)
		{
			switch (type)
			{
			case ObjectType.Model:
				return 512;
			case ObjectType.DataSource:
				return 512;
			case ObjectType.Table:
				return 512;
			case ObjectType.Column:
				return 512;
			case ObjectType.AttributeHierarchy:
			case ObjectType.KPI:
			case ObjectType.ObjectTranslation:
			case ObjectType.LinguisticMetadata:
			case (ObjectType)16:
			case (ObjectType)17:
			case (ObjectType)18:
			case (ObjectType)19:
			case (ObjectType)20:
			case (ObjectType)21:
			case (ObjectType)22:
			case (ObjectType)23:
			case (ObjectType)24:
			case (ObjectType)25:
			case (ObjectType)26:
			case (ObjectType)27:
			case (ObjectType)28:
			case ObjectType.DetailRowsDefinition:
			case ObjectType.RelatedColumnDetails:
			case ObjectType.GroupByColumn:
			case ObjectType.CalculationGroup:
			case ObjectType.AlternateOf:
			case ObjectType.RefreshPolicy:
			case ObjectType.FormatStringDefinition:
				break;
			case ObjectType.Partition:
				return 512;
			case ObjectType.Relationship:
				return 512;
			case ObjectType.Measure:
				return 512;
			case ObjectType.Hierarchy:
				return 512;
			case ObjectType.Level:
				return 512;
			case ObjectType.Annotation:
				return 512;
			case ObjectType.Culture:
				return 512;
			case ObjectType.Perspective:
				return 512;
			case ObjectType.PerspectiveTable:
				return 512;
			case ObjectType.PerspectiveColumn:
				return 512;
			case ObjectType.PerspectiveHierarchy:
				return 512;
			case ObjectType.PerspectiveMeasure:
				return 512;
			case ObjectType.Role:
				return 512;
			case ObjectType.RoleMembership:
				return 512;
			case ObjectType.TablePermission:
				return 512;
			case ObjectType.Variation:
				return 512;
			case ObjectType.Set:
				return 512;
			case ObjectType.PerspectiveSet:
				return 512;
			case ObjectType.ExtendedProperty:
				return 512;
			case ObjectType.Expression:
				return 512;
			case ObjectType.ColumnPermission:
				return 512;
			case ObjectType.CalculationItem:
				return 512;
			case ObjectType.QueryGroup:
				return 512;
			case ObjectType.AnalyticsAIMetadata:
				return 512;
			default:
				switch (type)
				{
				case ObjectType.Calendar:
					return 512;
				case ObjectType.CalendarColumnReference:
					return 512;
				case ObjectType.Function:
					return 512;
				case ObjectType.BindingInfo:
					return 512;
				}
				break;
			}
			throw new ArgumentOutOfRangeException("type");
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0007F134 File Offset: 0x0007D334
		internal static string GetUserFriendlyNameOfObjectType(ObjectType type)
		{
			switch (type)
			{
			case ObjectType.Model:
				return TomSR.ObjectType_Model;
			case ObjectType.DataSource:
				return TomSR.ObjectType_DataSource;
			case ObjectType.Table:
				return TomSR.ObjectType_Table;
			case ObjectType.Column:
				return TomSR.ObjectType_Column;
			case ObjectType.AttributeHierarchy:
				return TomSR.ObjectType_AttributeHierarchy;
			case ObjectType.Partition:
				return TomSR.ObjectType_Partition;
			case ObjectType.Relationship:
				return TomSR.ObjectType_Relationship;
			case ObjectType.Measure:
				return TomSR.ObjectType_Measure;
			case ObjectType.Hierarchy:
				return TomSR.ObjectType_Hierarchy;
			case ObjectType.Level:
				return TomSR.ObjectType_Level;
			case ObjectType.Annotation:
				return TomSR.ObjectType_Annotation;
			case ObjectType.KPI:
				return TomSR.ObjectType_KPI;
			case ObjectType.Culture:
				return TomSR.ObjectType_Culture;
			case ObjectType.ObjectTranslation:
				return TomSR.ObjectType_ObjectTranslation;
			case ObjectType.LinguisticMetadata:
				return TomSR.ObjectType_LinguisticMetadata;
			case (ObjectType)16:
			case (ObjectType)17:
			case (ObjectType)18:
			case (ObjectType)19:
			case (ObjectType)20:
			case (ObjectType)21:
			case (ObjectType)22:
			case (ObjectType)23:
			case (ObjectType)24:
			case (ObjectType)25:
			case (ObjectType)26:
			case (ObjectType)27:
			case (ObjectType)28:
			case (ObjectType)55:
			case (ObjectType)56:
			case (ObjectType)57:
				break;
			case ObjectType.Perspective:
				return TomSR.ObjectType_Perspective;
			case ObjectType.PerspectiveTable:
				return TomSR.ObjectType_PerspectiveTable;
			case ObjectType.PerspectiveColumn:
				return TomSR.ObjectType_PerspectiveColumn;
			case ObjectType.PerspectiveHierarchy:
				return TomSR.ObjectType_PerspectiveHierarchy;
			case ObjectType.PerspectiveMeasure:
				return TomSR.ObjectType_PerspectiveMeasure;
			case ObjectType.Role:
				return TomSR.ObjectType_ModelRole;
			case ObjectType.RoleMembership:
				return TomSR.ObjectType_ModelRoleMember;
			case ObjectType.TablePermission:
				return TomSR.ObjectType_TablePermission;
			case ObjectType.Variation:
				return TomSR.ObjectType_Variation;
			case ObjectType.Set:
				return TomSR.ObjectType_Set;
			case ObjectType.PerspectiveSet:
				return TomSR.ObjectType_PerspectiveSet;
			case ObjectType.ExtendedProperty:
				return TomSR.ObjectType_ExtendedProperty;
			case ObjectType.Expression:
				return TomSR.ObjectType_NamedExpression;
			case ObjectType.ColumnPermission:
				return TomSR.ObjectType_ColumnPermission;
			case ObjectType.DetailRowsDefinition:
				return TomSR.ObjectType_DetailRowsDefinition;
			case ObjectType.RelatedColumnDetails:
				return TomSR.ObjectType_RelatedColumnDetails;
			case ObjectType.GroupByColumn:
				return TomSR.ObjectType_GroupByColumn;
			case ObjectType.CalculationGroup:
				return TomSR.ObjectType_CalculationGroup;
			case ObjectType.CalculationItem:
				return TomSR.ObjectType_CalculationItem;
			case ObjectType.AlternateOf:
				return TomSR.ObjectType_AlternateOf;
			case ObjectType.RefreshPolicy:
				return TomSR.ObjectType_RefreshPolicy;
			case ObjectType.FormatStringDefinition:
				return TomSR.ObjectType_FormatStringDefinition;
			case ObjectType.QueryGroup:
				return TomSR.ObjectType_QueryGroup;
			case ObjectType.AnalyticsAIMetadata:
				return TomSR.ObjectType_AnalyticsAIMetadata;
			case ObjectType.ChangedProperty:
				return TomSR.ObjectType_ChangedProperty;
			case ObjectType.ExcludedArtifact:
				return TomSR.ObjectType_ExcludedArtifact;
			case ObjectType.DataCoverageDefinition:
				return TomSR.ObjectType_DataCoverageDefinition;
			case ObjectType.CalculationExpression:
				return TomSR.ObjectType_CalculationGroupExpression;
			case ObjectType.Calendar:
				return TomSR.ObjectType_Calendar;
			case ObjectType.TimeUnitColumnAssociation:
				return TomSR.ObjectType_TimeUnitColumnAssociation;
			case ObjectType.CalendarColumnReference:
				return TomSR.ObjectType_CalendarColumnReference;
			case ObjectType.Function:
				return TomSR.ObjectType_Function;
			case ObjectType.BindingInfo:
				return TomSR.ObjectType_BindingInfo;
			default:
				if (type == ObjectType.Database)
				{
					return TomSR.ObjectType_Database;
				}
				break;
			}
			throw new ArgumentOutOfRangeException("type");
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0007F384 File Offset: 0x0007D584
		internal static string GetDefaultNameForObjectType(ObjectType type)
		{
			switch (type)
			{
			case ObjectType.Model:
				return TomSR.DefaultNameForObject_Model;
			case ObjectType.DataSource:
				return TomSR.DefaultNameForObject_DataSource;
			case ObjectType.Table:
				return TomSR.DefaultNameForObject_Table;
			case ObjectType.Column:
				return TomSR.DefaultNameForObject_Column;
			case ObjectType.AttributeHierarchy:
			case ObjectType.KPI:
			case ObjectType.ObjectTranslation:
			case ObjectType.LinguisticMetadata:
			case (ObjectType)16:
			case (ObjectType)17:
			case (ObjectType)18:
			case (ObjectType)19:
			case (ObjectType)20:
			case (ObjectType)21:
			case (ObjectType)22:
			case (ObjectType)23:
			case (ObjectType)24:
			case (ObjectType)25:
			case (ObjectType)26:
			case (ObjectType)27:
			case (ObjectType)28:
			case ObjectType.DetailRowsDefinition:
			case ObjectType.RelatedColumnDetails:
			case ObjectType.GroupByColumn:
			case ObjectType.CalculationGroup:
			case ObjectType.AlternateOf:
			case ObjectType.RefreshPolicy:
			case ObjectType.FormatStringDefinition:
				break;
			case ObjectType.Partition:
				return TomSR.DefaultNameForObject_Partition;
			case ObjectType.Relationship:
				return TomSR.DefaultNameForObject_Relationship;
			case ObjectType.Measure:
				return TomSR.DefaultNameForObject_Measure;
			case ObjectType.Hierarchy:
				return TomSR.DefaultNameForObject_Hierarchy;
			case ObjectType.Level:
				return TomSR.DefaultNameForObject_Level;
			case ObjectType.Annotation:
				return TomSR.DefaultNameForObject_Annotation;
			case ObjectType.Culture:
				return TomSR.DefaultNameForObject_Culture;
			case ObjectType.Perspective:
				return TomSR.DefaultNameForObject_Perspective;
			case ObjectType.PerspectiveTable:
				return TomSR.DefaultNameForObject_PerspectiveTable;
			case ObjectType.PerspectiveColumn:
				return TomSR.DefaultNameForObject_PerspectiveColumn;
			case ObjectType.PerspectiveHierarchy:
				return TomSR.DefaultNameForObject_PerspectiveHierarchy;
			case ObjectType.PerspectiveMeasure:
				return TomSR.DefaultNameForObject_PerspectiveMeasure;
			case ObjectType.Role:
				return TomSR.DefaultNameForObject_ModelRole;
			case ObjectType.RoleMembership:
				return TomSR.DefaultNameForObject_ModelRoleMember;
			case ObjectType.TablePermission:
				return TomSR.DefaultNameForObject_TablePermission;
			case ObjectType.Variation:
				return TomSR.DefaultNameForObject_Variation;
			case ObjectType.Set:
				return TomSR.DefaultNameForObject_Set;
			case ObjectType.PerspectiveSet:
				return TomSR.DefaultNameForObject_PerspectiveSet;
			case ObjectType.ExtendedProperty:
				return TomSR.DefaultNameForObject_ExtendedProperty;
			case ObjectType.Expression:
				return TomSR.DefaultNameForObject_NamedExpression;
			case ObjectType.ColumnPermission:
				return TomSR.DefaultNameForObject_ColumnPermission;
			case ObjectType.CalculationItem:
				return TomSR.DefaultNameForObject_CalculationItem;
			case ObjectType.QueryGroup:
				return TomSR.DefaultNameForObject_QueryGroup;
			case ObjectType.AnalyticsAIMetadata:
				return TomSR.DefaultNameForObject_AnalyticsAIMetadata;
			default:
				switch (type)
				{
				case ObjectType.Calendar:
					return TomSR.DefaultNameForObject_Calendar;
				case ObjectType.CalendarColumnReference:
					return TomSR.DefaultNameForObject_CalendarColumnReference;
				case ObjectType.Function:
					return TomSR.DefaultNameForObject_Function;
				case ObjectType.BindingInfo:
					return TomSR.DefaultNameForObject_BindingInfo;
				}
				break;
			}
			throw new ArgumentOutOfRangeException("type");
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0007F558 File Offset: 0x0007D758
		internal static bool TryGetObjectTypeByType(Type type, out ObjectType objectType)
		{
			if (type == typeof(Model))
			{
				objectType = ObjectType.Model;
				return true;
			}
			if (type == typeof(ProviderDataSource))
			{
				objectType = ObjectType.DataSource;
				return true;
			}
			if (type == typeof(StructuredDataSource))
			{
				objectType = ObjectType.DataSource;
				return true;
			}
			if (type == typeof(DataSource))
			{
				objectType = ObjectType.DataSource;
				return true;
			}
			if (type == typeof(Table))
			{
				objectType = ObjectType.Table;
				return true;
			}
			if (type == typeof(DataColumn))
			{
				objectType = ObjectType.Column;
				return true;
			}
			if (type == typeof(RowNumberColumn))
			{
				objectType = ObjectType.Column;
				return true;
			}
			if (type == typeof(CalculatedTableColumn))
			{
				objectType = ObjectType.Column;
				return true;
			}
			if (type == typeof(CalculatedColumn))
			{
				objectType = ObjectType.Column;
				return true;
			}
			if (type == typeof(Column))
			{
				objectType = ObjectType.Column;
				return true;
			}
			if (type == typeof(AttributeHierarchy))
			{
				objectType = ObjectType.AttributeHierarchy;
				return true;
			}
			if (type == typeof(Partition))
			{
				objectType = ObjectType.Partition;
				return true;
			}
			if (type == typeof(SingleColumnRelationship))
			{
				objectType = ObjectType.Relationship;
				return true;
			}
			if (type == typeof(Relationship))
			{
				objectType = ObjectType.Relationship;
				return true;
			}
			if (type == typeof(Measure))
			{
				objectType = ObjectType.Measure;
				return true;
			}
			if (type == typeof(Hierarchy))
			{
				objectType = ObjectType.Hierarchy;
				return true;
			}
			if (type == typeof(Level))
			{
				objectType = ObjectType.Level;
				return true;
			}
			if (type == typeof(Annotation))
			{
				objectType = ObjectType.Annotation;
				return true;
			}
			if (type == typeof(KPI))
			{
				objectType = ObjectType.KPI;
				return true;
			}
			if (type == typeof(Culture))
			{
				objectType = ObjectType.Culture;
				return true;
			}
			if (type == typeof(ObjectTranslation))
			{
				objectType = ObjectType.ObjectTranslation;
				return true;
			}
			if (type == typeof(LinguisticMetadata))
			{
				objectType = ObjectType.LinguisticMetadata;
				return true;
			}
			if (type == typeof(Perspective))
			{
				objectType = ObjectType.Perspective;
				return true;
			}
			if (type == typeof(PerspectiveTable))
			{
				objectType = ObjectType.PerspectiveTable;
				return true;
			}
			if (type == typeof(PerspectiveColumn))
			{
				objectType = ObjectType.PerspectiveColumn;
				return true;
			}
			if (type == typeof(PerspectiveHierarchy))
			{
				objectType = ObjectType.PerspectiveHierarchy;
				return true;
			}
			if (type == typeof(PerspectiveMeasure))
			{
				objectType = ObjectType.PerspectiveMeasure;
				return true;
			}
			if (type == typeof(ModelRole))
			{
				objectType = ObjectType.Role;
				return true;
			}
			if (type == typeof(WindowsModelRoleMember))
			{
				objectType = ObjectType.RoleMembership;
				return true;
			}
			if (type == typeof(ExternalModelRoleMember))
			{
				objectType = ObjectType.RoleMembership;
				return true;
			}
			if (type == typeof(ModelRoleMember))
			{
				objectType = ObjectType.RoleMembership;
				return true;
			}
			if (type == typeof(TablePermission))
			{
				objectType = ObjectType.TablePermission;
				return true;
			}
			if (type == typeof(Variation))
			{
				objectType = ObjectType.Variation;
				return true;
			}
			if (type == typeof(Set))
			{
				objectType = ObjectType.Set;
				return true;
			}
			if (type == typeof(PerspectiveSet))
			{
				objectType = ObjectType.PerspectiveSet;
				return true;
			}
			if (type == typeof(StringExtendedProperty))
			{
				objectType = ObjectType.ExtendedProperty;
				return true;
			}
			if (type == typeof(JsonExtendedProperty))
			{
				objectType = ObjectType.ExtendedProperty;
				return true;
			}
			if (type == typeof(ExtendedProperty))
			{
				objectType = ObjectType.ExtendedProperty;
				return true;
			}
			if (type == typeof(NamedExpression))
			{
				objectType = ObjectType.Expression;
				return true;
			}
			if (type == typeof(ColumnPermission))
			{
				objectType = ObjectType.ColumnPermission;
				return true;
			}
			if (type == typeof(DetailRowsDefinition))
			{
				objectType = ObjectType.DetailRowsDefinition;
				return true;
			}
			if (type == typeof(RelatedColumnDetails))
			{
				objectType = ObjectType.RelatedColumnDetails;
				return true;
			}
			if (type == typeof(GroupByColumn))
			{
				objectType = ObjectType.GroupByColumn;
				return true;
			}
			if (type == typeof(CalculationGroup))
			{
				objectType = ObjectType.CalculationGroup;
				return true;
			}
			if (type == typeof(CalculationItem))
			{
				objectType = ObjectType.CalculationItem;
				return true;
			}
			if (type == typeof(AlternateOf))
			{
				objectType = ObjectType.AlternateOf;
				return true;
			}
			if (type == typeof(BasicRefreshPolicy))
			{
				objectType = ObjectType.RefreshPolicy;
				return true;
			}
			if (type == typeof(RefreshPolicy))
			{
				objectType = ObjectType.RefreshPolicy;
				return true;
			}
			if (type == typeof(FormatStringDefinition))
			{
				objectType = ObjectType.FormatStringDefinition;
				return true;
			}
			if (type == typeof(QueryGroup))
			{
				objectType = ObjectType.QueryGroup;
				return true;
			}
			if (type == typeof(AnalyticsAIMetadata))
			{
				objectType = ObjectType.AnalyticsAIMetadata;
				return true;
			}
			if (type == typeof(ChangedProperty))
			{
				objectType = ObjectType.ChangedProperty;
				return true;
			}
			if (type == typeof(ExcludedArtifact))
			{
				objectType = ObjectType.ExcludedArtifact;
				return true;
			}
			if (type == typeof(DataCoverageDefinition))
			{
				objectType = ObjectType.DataCoverageDefinition;
				return true;
			}
			if (type == typeof(CalculationGroupExpression))
			{
				objectType = ObjectType.CalculationExpression;
				return true;
			}
			if (type == typeof(Calendar))
			{
				objectType = ObjectType.Calendar;
				return true;
			}
			if (type == typeof(TimeUnitColumnAssociation))
			{
				objectType = ObjectType.TimeUnitColumnAssociation;
				return true;
			}
			if (type == typeof(CalendarColumnReference))
			{
				objectType = ObjectType.CalendarColumnReference;
				return true;
			}
			if (type == typeof(Function))
			{
				objectType = ObjectType.Function;
				return true;
			}
			if (type == typeof(DataBindingHint))
			{
				objectType = ObjectType.BindingInfo;
				return true;
			}
			if (type == typeof(BindingInfo))
			{
				objectType = ObjectType.BindingInfo;
				return true;
			}
			objectType = ObjectType.Null;
			return false;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0007FB14 File Offset: 0x0007DD14
		internal static bool HasVirtualName(ObjectType type)
		{
			switch (type)
			{
			case ObjectType.Model:
			case ObjectType.DataSource:
			case ObjectType.Table:
			case ObjectType.Partition:
			case ObjectType.Relationship:
			case ObjectType.Measure:
			case ObjectType.Hierarchy:
			case ObjectType.Level:
			case ObjectType.Annotation:
			case ObjectType.Culture:
			case ObjectType.Perspective:
			case ObjectType.Role:
			case ObjectType.Variation:
			case ObjectType.Set:
			case ObjectType.ExtendedProperty:
			case ObjectType.Expression:
			case ObjectType.CalculationItem:
			case ObjectType.QueryGroup:
			case ObjectType.AnalyticsAIMetadata:
				return false;
			case ObjectType.Column:
			case ObjectType.PerspectiveTable:
			case ObjectType.PerspectiveColumn:
			case ObjectType.PerspectiveHierarchy:
			case ObjectType.PerspectiveMeasure:
			case ObjectType.RoleMembership:
			case ObjectType.TablePermission:
			case ObjectType.PerspectiveSet:
			case ObjectType.ColumnPermission:
				break;
			case ObjectType.AttributeHierarchy:
			case ObjectType.KPI:
			case ObjectType.ObjectTranslation:
			case ObjectType.LinguisticMetadata:
			case (ObjectType)16:
			case (ObjectType)17:
			case (ObjectType)18:
			case (ObjectType)19:
			case (ObjectType)20:
			case (ObjectType)21:
			case (ObjectType)22:
			case (ObjectType)23:
			case (ObjectType)24:
			case (ObjectType)25:
			case (ObjectType)26:
			case (ObjectType)27:
			case (ObjectType)28:
			case ObjectType.DetailRowsDefinition:
			case ObjectType.RelatedColumnDetails:
			case ObjectType.GroupByColumn:
			case ObjectType.CalculationGroup:
			case ObjectType.AlternateOf:
			case ObjectType.RefreshPolicy:
			case ObjectType.FormatStringDefinition:
				goto IL_00FB;
			default:
				switch (type)
				{
				case ObjectType.Calendar:
				case ObjectType.Function:
				case ObjectType.BindingInfo:
					return false;
				case ObjectType.TimeUnitColumnAssociation:
					goto IL_00FB;
				case ObjectType.CalendarColumnReference:
					break;
				default:
					goto IL_00FB;
				}
				break;
			}
			return true;
			IL_00FB:
			throw new ArgumentOutOfRangeException("type");
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0007FC28 File Offset: 0x0007DE28
		internal static RefreshTypeMask ConvertRefreshTypeToMask(RefreshType refreshType)
		{
			switch (refreshType)
			{
			case RefreshType.Full:
				return RefreshTypeMask.Full;
			case RefreshType.ClearValues:
				return RefreshTypeMask.ClearValues;
			case RefreshType.Calculate:
				return RefreshTypeMask.Calculate;
			case RefreshType.DataOnly:
				return RefreshTypeMask.DataOnly;
			case RefreshType.Automatic:
				return RefreshTypeMask.Automatic;
			case RefreshType.Add:
				return RefreshTypeMask.Add;
			case RefreshType.Defragment:
				return RefreshTypeMask.Defragment;
			}
			throw new ArgumentOutOfRangeException("refreshType");
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0007FC7A File Offset: 0x0007DE7A
		internal static IEnumerable<RefreshType> ConvertRefreshMaskToType(RefreshTypeMask mask)
		{
			if ((mask & RefreshTypeMask.Full) != RefreshTypeMask.None)
			{
				yield return RefreshType.Full;
				mask ^= RefreshTypeMask.Full;
				if (mask == RefreshTypeMask.None)
				{
					yield break;
				}
			}
			if ((mask & RefreshTypeMask.ClearValues) != RefreshTypeMask.None)
			{
				yield return RefreshType.ClearValues;
				mask ^= RefreshTypeMask.ClearValues;
				if (mask == RefreshTypeMask.None)
				{
					yield break;
				}
			}
			if ((mask & RefreshTypeMask.Calculate) != RefreshTypeMask.None)
			{
				yield return RefreshType.Calculate;
				mask ^= RefreshTypeMask.Calculate;
				if (mask == RefreshTypeMask.None)
				{
					yield break;
				}
			}
			if ((mask & RefreshTypeMask.DataOnly) != RefreshTypeMask.None)
			{
				yield return RefreshType.DataOnly;
				mask ^= RefreshTypeMask.DataOnly;
				if (mask == RefreshTypeMask.None)
				{
					yield break;
				}
			}
			if ((mask & RefreshTypeMask.Automatic) != RefreshTypeMask.None)
			{
				yield return RefreshType.Automatic;
				mask ^= RefreshTypeMask.Automatic;
				if (mask == RefreshTypeMask.None)
				{
					yield break;
				}
			}
			if ((mask & RefreshTypeMask.Add) != RefreshTypeMask.None)
			{
				yield return RefreshType.Add;
				mask ^= RefreshTypeMask.Add;
				if (mask == RefreshTypeMask.None)
				{
					yield break;
				}
			}
			if ((mask & RefreshTypeMask.Defragment) != RefreshTypeMask.None)
			{
				yield return RefreshType.Defragment;
				mask ^= RefreshTypeMask.Defragment;
				yield break;
			}
			yield break;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0007FC8A File Offset: 0x0007DE8A
		internal static void Verify(bool condition)
		{
			if (!condition)
			{
				throw new TomInternalException();
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0007FC95 File Offset: 0x0007DE95
		internal static void Verify(bool condition, string message)
		{
			if (!condition)
			{
				throw new TomInternalException(message);
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0007FCA1 File Offset: 0x0007DEA1
		internal static void Verify(bool condition, string format, params KeyValuePair<InfoRestrictionType, object>[] args)
		{
			if (!condition)
			{
				throw TomInternalException.CreateWithRestrictedInfo(format, args);
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0007FCB0 File Offset: 0x0007DEB0
		internal static bool IsSafeException(Exception e)
		{
			return !(e is StackOverflowException) && !(e is OutOfMemoryException) && !(e is ThreadAbortException) && !(e is AccessViolationException) && !(e is SEHException) && !typeof(SecurityException).IsAssignableFrom(e.GetType());
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0007FD00 File Offset: 0x0007DF00
		internal static void AdjustLineageObjectCollectionsComparison<T>(CopyContext context, IList<T> removedItems, IList<T> addedItems, IList<KeyValuePair<T, T>> matchedItems) where T : NamedMetadataObject, IMetadataObjectWithLineage
		{
			if ((context.Flags & CopyFlags.IncludeObjectIds) == CopyFlags.IncludeObjectIds || addedItems.Count == 0 || removedItems.Count == 0)
			{
				return;
			}
			int i = 0;
			while (i < addedItems.Count)
			{
				string lineageTag = addedItems[i].LineageTag;
				if (string.IsNullOrEmpty(lineageTag))
				{
					i++;
				}
				else
				{
					int indexOfObjectWithLineageTag = Utils.GetIndexOfObjectWithLineageTag<T>(removedItems, lineageTag);
					if (indexOfObjectWithLineageTag == -1)
					{
						i++;
					}
					else
					{
						matchedItems.Add(new KeyValuePair<T, T>(removedItems[indexOfObjectWithLineageTag], addedItems[i]));
						removedItems.RemoveAt(indexOfObjectWithLineageTag);
						addedItems.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0007FD90 File Offset: 0x0007DF90
		private static int GetIndexOfObjectWithLineageTag<T>(IList<T> items, string tag) where T : NamedMetadataObject, IMetadataObjectWithLineage
		{
			for (int i = 0; i < items.Count; i++)
			{
				string lineageTag = items[i].LineageTag;
				if (string.Compare(tag, lineageTag, StringComparison.Ordinal) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0007FDD0 File Offset: 0x0007DFD0
		internal static void AdjustTypedNamedObjectCollectionsComparison<T>(IList<T> removedItems, IList<T> addedItems, IList<KeyValuePair<T, T>> matchedItems, Func<T, T, bool> isSameType) where T : NamedMetadataObject
		{
			int i = 0;
			while (i < matchedItems.Count)
			{
				if (isSameType(matchedItems[i].Key, matchedItems[i].Value))
				{
					i++;
				}
				else
				{
					removedItems.Add(matchedItems[i].Key);
					addedItems.Add(matchedItems[i].Value);
					matchedItems.RemoveAt(i);
				}
			}
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0007FE48 File Offset: 0x0007E048
		internal static void CompareLinkedObjectCollections<TObject>(IEnumerable<TObject> current, IEnumerable<TObject> other, CopyContext context, bool isNameUniqueInScope, IList<TObject> removedItems, IList<TObject> addedItems, IList<KeyValuePair<TObject, TObject>> matchedItems) where TObject : MetadataObject, ILinkedMetadataObject
		{
			Dictionary<string, TObject> dictionary = new Dictionary<string, TObject>();
			List<TObject> list = new List<TObject>();
			if ((context.Flags & CopyFlags.Incremental) != CopyFlags.Incremental)
			{
				HashSet<string> hashSet = new HashSet<string>();
				using (IEnumerator<TObject> enumerator = other.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IList<string> list2;
						if (Utils.BuildLinkedObjectMappingKeys<TObject>(enumerator.Current, context, isNameUniqueInScope, out list2))
						{
							for (int i = 0; i < list2.Count; i++)
							{
								Utils.Verify(hashSet.Add(list2[i]), "Can't have duplicates in the current set of objects, or the set of referenced targets!");
							}
						}
					}
				}
				using (IEnumerator<TObject> enumerator = current.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TObject tobject = enumerator.Current;
						IList<string> list3;
						if (!Utils.BuildLinkedObjectMappingKeys<TObject>(tobject, context, isNameUniqueInScope, out list3))
						{
							list.Add(tobject);
						}
						else if (!Utils.AddLinkedObjectToMapping<TObject>(tobject, dictionary, list3, hashSet))
						{
							removedItems.Add(tobject);
						}
					}
					goto IL_0117;
				}
			}
			foreach (TObject tobject2 in current)
			{
				IList<string> list4;
				if (!Utils.BuildLinkedObjectMappingKeys<TObject>(tobject2, context, isNameUniqueInScope, out list4))
				{
					list.Add(tobject2);
				}
				else
				{
					Utils.AddLinkedObjectToMapping<TObject>(tobject2, dictionary, list4, null);
				}
			}
			IL_0117:
			foreach (TObject tobject3 in other)
			{
				IList<string> list5;
				if (Utils.BuildLinkedObjectMappingKeys<TObject>(tobject3, context, isNameUniqueInScope, out list5))
				{
					TObject tobject4 = default(TObject);
					int num = 0;
					while (num < list5.Count && tobject4 == null)
					{
						dictionary.TryGetValue(list5[num], out tobject4);
						num++;
					}
					if (tobject4 != null)
					{
						matchedItems.Add(new KeyValuePair<TObject, TObject>(tobject4, tobject3));
					}
					else
					{
						addedItems.Add(tobject3);
					}
				}
				else if (list.Count > 0)
				{
					matchedItems.Add(new KeyValuePair<TObject, TObject>(list[0], tobject3));
					list.RemoveAt(0);
				}
				else
				{
					addedItems.Add(tobject3);
				}
			}
			if ((context.Flags & CopyFlags.Incremental) != CopyFlags.Incremental)
			{
				for (int j = 0; j < list.Count; j++)
				{
					removedItems.Add(list[j]);
				}
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00080098 File Offset: 0x0007E298
		private static bool BuildLinkedObjectMappingKeys<TObject>(TObject @object, CopyContext context, bool isNameUniqueInScope, out IList<string> keys) where TObject : MetadataObject, ILinkedMetadataObject
		{
			keys = new List<string>(3);
			if ((context.Flags & CopyFlags.IncludeObjectIds) == CopyFlags.IncludeObjectIds && @object.Id != ObjectId.Null)
			{
				keys.Add(string.Format("Id:{0}", @object.Id));
			}
			ObjectId objectId;
			ObjectPath path;
			MetadataObject metadataObject;
			string text;
			@object.GetLinkedObjectTarget(out objectId, out path, out metadataObject, out text);
			if (path == null && metadataObject == null && objectId != ObjectId.Null && context.IncrementalUpdateOriginalObjectMap != null)
			{
				context.IncrementalUpdateOriginalObjectMap.TryGetValue(objectId, out metadataObject);
			}
			if ((context.Flags & CopyFlags.IncludeObjectIds) == CopyFlags.IncludeObjectIds && objectId != ObjectId.Null)
			{
				keys.Add(string.IsNullOrEmpty(text) ? string.Format("TargetId:{0}", objectId) : string.Format("TargetId:{0}.{1}", objectId, text));
			}
			if (path != null || metadataObject != null)
			{
				if (isNameUniqueInScope)
				{
					string text2;
					if (metadataObject != null)
					{
						text2 = Utils.NormalizeLinkedObjectNameForMappingKey(((NamedMetadataObject)metadataObject).Name);
					}
					else
					{
						text2 = Utils.NormalizeLinkedObjectNameForMappingKey(path[path.Count - 1].Value);
					}
					keys.Add(string.IsNullOrEmpty(text) ? string.Format("TargetName:{0}", text2) : string.Format("TargetName:{0}.{1}", text2, text));
				}
				else
				{
					if (path == null)
					{
						path = metadataObject.GetPath(context.IncrementalUpdateOriginalObjectMap);
					}
					StringBuilder stringBuilder = new StringBuilder("TargetPath:");
					for (int i = 0; i < path.Count; i++)
					{
						stringBuilder.Append('[');
						stringBuilder.Append(path[i].Key.ToString());
						if (!string.IsNullOrEmpty(path[i].Value))
						{
							stringBuilder.Append(':');
							stringBuilder.Append(Utils.NormalizeLinkedObjectNameForMappingKey(path[i].Value));
						}
						stringBuilder.Append(']');
					}
					if (!string.IsNullOrEmpty(text))
					{
						stringBuilder.Append('.');
						stringBuilder.Append(text);
					}
					keys.Add(stringBuilder.ToString());
				}
			}
			return keys.Count > 0;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x000802D0 File Offset: 0x0007E4D0
		private static bool AddLinkedObjectToMapping<TObject>(TObject @object, IDictionary<string, TObject> mapping, IList<string> keys, ICollection<string> filter) where TObject : MetadataObject, ILinkedMetadataObject
		{
			bool flag = false;
			for (int i = 0; i < keys.Count; i++)
			{
				if (filter == null || filter.Contains(keys[i]))
				{
					mapping.Add(keys[i], @object);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00080314 File Offset: 0x0007E514
		private static string NormalizeLinkedObjectNameForMappingKey(string name)
		{
			int num = 0;
			int i = name.IndexOfAny(Utils.invalidCharForLinkedObjectMappingKey, num);
			if (i < 0)
			{
				return name;
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (i >= 0)
			{
				stringBuilder.Append(name.Substring(num, i - num));
				stringBuilder.AppendFormat("${0,2:X}", (int)name[i]);
				num = i + 1;
				i = name.IndexOfAny(Utils.invalidCharForLinkedObjectMappingKey, num);
			}
			stringBuilder.Append(name.Substring(num));
			return stringBuilder.ToString();
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00080390 File Offset: 0x0007E590
		internal static void CompareUniqueObjectCollections<T>(IMetadataObjectCollection<T> current, IMetadataObjectCollection<T> other, CopyContext context, IList<T> removedItems, IList<T> addedItems, IList<KeyValuePair<T, T>> matchedItems, Func<T, T, bool> isEquivalentItem) where T : MetadataObject
		{
			bool flag = (context.Flags & CopyFlags.IncludeObjectIds) == CopyFlags.IncludeObjectIds;
			if ((context.Flags & CopyFlags.Incremental) != CopyFlags.Incremental)
			{
				foreach (T t in current)
				{
					T t2;
					if (flag && !t.Id.IsNull)
					{
						if (other.FindById(t.Id) == null)
						{
							removedItems.Add(t);
						}
					}
					else if (!Utils.TryFindEquivalentUniqueObject<T>(other, t, isEquivalentItem, out t2))
					{
						removedItems.Add(t);
					}
				}
			}
			foreach (T t3 in other)
			{
				Utils.Verify(!flag || !t3.Id.IsNull, "If we compare collections using Ids, then objects in 'other' collection must have Ids");
				T t4;
				if (!flag)
				{
					T t2 = default(T);
					t4 = t2;
				}
				else
				{
					t4 = current.FindById(t3.Id);
				}
				T t5 = t4;
				if (t5 == null && Utils.TryFindEquivalentUniqueObject<T>(current, t3, isEquivalentItem, out t5) && flag && !t5.Id.IsNull)
				{
					t5 = default(T);
				}
				if (t5 != null)
				{
					matchedItems.Add(new KeyValuePair<T, T>(t5, t3));
				}
				else
				{
					addedItems.Add(t3);
				}
			}
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00080514 File Offset: 0x0007E714
		internal static bool TryFindEquivalentUniqueObject<T>(IEnumerable<T> items, T item, Func<T, T, bool> isEquivalent, out T equivalentItem) where T : MetadataObject
		{
			foreach (T t in items)
			{
				if (isEquivalent(item, t))
				{
					equivalentItem = t;
					return true;
				}
			}
			equivalentItem = default(T);
			return false;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00080574 File Offset: 0x0007E774
		public static bool CanRefreshWithOverrides(RefreshType type)
		{
			return type == RefreshType.DataOnly || type == RefreshType.Full || type == RefreshType.Add || type == RefreshType.Automatic;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00080588 File Offset: 0x0007E788
		internal static bool CanApplyRefreshPolicies(RefreshType type)
		{
			return type == RefreshType.Automatic || type == RefreshType.DataOnly || type == RefreshType.Full;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00080598 File Offset: 0x0007E798
		internal static string ConvertXmlaToString(XElement xmla)
		{
			string text;
			try
			{
				text = xmla.ToString();
			}
			catch (ArgumentException ex)
			{
				throw new InvalidOperationException(TomSR.Exception_FailedToGenerateXmlaRequest(ex.Message), ex);
			}
			return text;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x000805D4 File Offset: 0x0007E7D4
		internal static void CombineXmlaResults(ref XmlaResultCollection xmlaResults, XmlaResultCollection additionalResults)
		{
			if (additionalResults == null || additionalResults.Count == 0)
			{
				return;
			}
			if (xmlaResults != null)
			{
				using (IEnumerator enumerator = ((IEnumerable)additionalResults).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						XmlaResult xmlaResult = (XmlaResult)obj;
						xmlaResults.Add(xmlaResult);
					}
					return;
				}
			}
			xmlaResults = additionalResults;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0008063C File Offset: 0x0007E83C
		internal static JsonObject SerializeSparseCollection(IEnumerable<MetadataObject> sparseCollection, SparseSerializerSettings settings)
		{
			Utils.Verify(sparseCollection.Any<MetadataObject>(), "Sparse collection must be non-empty");
			HashSet<MetadataObject> hashSet = new HashSet<MetadataObject>();
			HashSet<MetadataObject> objectsToVisit = new HashSet<MetadataObject>();
			HashSet<ObjectType> objectTypesToVisit = new HashSet<ObjectType>();
			Model model = null;
			foreach (MetadataObject metadataObject in sparseCollection)
			{
				Utils.Verify(metadataObject.Model != null, "Can't serialize sparse collection when object does not have parent Model");
				Utils.Verify(model == null || model == metadataObject.Model, "Can't serialize sparse collection when objects belong to different Models");
				model = metadataObject.Model;
				hashSet.Add(metadataObject);
				foreach (MetadataObject metadataObject2 in metadataObject.GetSelfAndAncestors())
				{
					objectsToVisit.Add(metadataObject2);
					objectTypesToVisit.Add(metadataObject2.ObjectType);
				}
			}
			Utils.Verify(model != null);
			CompatibilityMode compatibilityMode;
			if (!model.TryGetCurrentMode(out compatibilityMode))
			{
				compatibilityMode = CompatibilityMode.PowerBI;
			}
			Func<MetadataObject, bool> <>9__3;
			JsonObjectTreeWriterSettings jsonObjectTreeWriterSettings = new JsonObjectTreeWriterSettings(delegate(MetadataObject obj, JsonObject jsonObj, CompatibilityMode _mode, int dbCompatibilityLevel)
			{
				settings.SerializeObject(obj, jsonObj, _mode, dbCompatibilityLevel);
			}, compatibilityMode, int.MaxValue)
			{
				WriteCollectionFilter = delegate(IMetadataObjectCollection collection)
				{
					if (objectTypesToVisit.Contains(collection.ItemType))
					{
						IEnumerable<MetadataObject> objects = collection.GetObjects();
						Func<MetadataObject, bool> func;
						if ((func = <>9__3) == null)
						{
							func = (<>9__3 = (MetadataObject o) => objectsToVisit.Contains(o));
						}
						return objects.Any(func);
					}
					return false;
				},
				WriteObjectFilter = (MetadataObject obj) => objectsToVisit.Contains(obj)
			};
			JsonObject jsonObject = new JsonObject();
			JsonObjectTreeWriter.WriteModel(model, jsonObject, jsonObjectTreeWriterSettings);
			JsonObject jsonObject2 = new JsonObject();
			jsonObject2["model", TomPropCategory.Regular, 0, false] = jsonObject.ToDictObject();
			return jsonObject2;
		}

		// Token: 0x040002DF RID: 735
		private static readonly string[] tableReservedNames = new string[] { "Measures" };

		// Token: 0x040002E0 RID: 736
		private static readonly char[] RoleNameInvalidCharacters = ",".ToCharArray();

		// Token: 0x040002E1 RID: 737
		private static char[] invalidCharForLinkedObjectMappingKey = new char[] { '.', '[', ']', ':', '$' };

		// Token: 0x040002E2 RID: 738
		internal static Func<ChangedProperty, ChangedProperty, bool> IsEquivalentChangedProperty = (ChangedProperty property1, ChangedProperty property2) => string.Compare(property1.Property, property2.Property, StringComparison.OrdinalIgnoreCase) == 0;

		// Token: 0x040002E3 RID: 739
		internal static Func<ExcludedArtifact, ExcludedArtifact, bool> IsEquivalentExcludedArtifact = (ExcludedArtifact artifact1, ExcludedArtifact artifact2) => artifact1.ArtifactType == artifact2.ArtifactType && artifact1.Reference == artifact2.Reference;
	}
}
