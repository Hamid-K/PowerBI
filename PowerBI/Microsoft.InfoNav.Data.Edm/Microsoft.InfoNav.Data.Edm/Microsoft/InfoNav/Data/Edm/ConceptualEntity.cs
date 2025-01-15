using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x0200000F RID: 15
	[DebuggerDisplay("{EntityContainerName}.{EdmName}")]
	[ImmutableObject(true)]
	internal sealed class ConceptualEntity : IConceptualEntity, IConceptualDisplayItem, IEquatable<IConceptualEntity>
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002BF8 File Offset: 0x00000DF8
		internal ConceptualEntity(string name, string edmName, string displayName, string description, string edmContainerName, ConceptualEntityVisibilityType visibility, bool isDateTable, int? rowCount, IEnumerable<ConceptualProperty> properties, IEnumerable<ConceptualHierarchy> hierarchies, IEnumerable<ConceptualDisplayFolder> displayFolders, IDictionary<string, IConceptualProperty> edmNamesToProps, IEnumerable<ConceptualColumn> keyColumns, IEnumerable<ConceptualProperty> defaultFieldProperties, ConceptualColumn defaultLabel, ConceptualColumn defaultImage, string stableName)
		{
			this._name = name;
			this._edmName = edmName;
			this._displayName = displayName;
			this._description = description;
			this._edmContainerName = edmContainerName;
			this._visibility = visibility;
			this._isDateTable = isDateTable;
			this._rowCount = rowCount;
			this._properties = properties.AsReadOnlyList<ConceptualProperty>();
			this._hierarchies = hierarchies.AsReadOnlyList<ConceptualHierarchy>();
			this._displayFolders = displayFolders.AsReadOnlyList<ConceptualDisplayFolder>();
			ConceptualEntity.BuildHierarchiesByName(this._hierarchies, out this._hierarchyNamesToHierarchies, out this._hierarchyEdmNamesToHierarchies);
			this._edmNamesToProps = edmNamesToProps.AsReadOnlyDictionary<string, IConceptualProperty>();
			this._propNamesToProps = ConceptualEntity.BuildPropertiesByName(edmNamesToProps.Values);
			this._keyColumns = keyColumns.AsReadOnlyList<ConceptualColumn>();
			this._defaultFieldProperties = defaultFieldProperties.AsReadOnlyList<ConceptualProperty>();
			this._defaultLabel = defaultLabel;
			this._defaultImage = defaultImage;
			this._stableName = stableName;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002CD7 File Offset: 0x00000ED7
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002CDF File Offset: 0x00000EDF
		public string EdmName
		{
			get
			{
				return this._edmName;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002CE7 File Offset: 0x00000EE7
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002CEF File Offset: 0x00000EEF
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002CF7 File Offset: 0x00000EF7
		public string EntityContainerName
		{
			get
			{
				return this._edmContainerName;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002CFF File Offset: 0x00000EFF
		public IConceptualSchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002D07 File Offset: 0x00000F07
		public string Extends
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002D0A File Offset: 0x00000F0A
		public ConceptualEntityVisibilityType Visibility
		{
			get
			{
				return this._visibility;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002D12 File Offset: 0x00000F12
		public bool IsDateTable
		{
			get
			{
				return this._isDateTable;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002D1A File Offset: 0x00000F1A
		public IReadOnlyList<IConceptualProperty> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002D22 File Offset: 0x00000F22
		public IReadOnlyList<IConceptualNavigationProperty> NavigationProperties
		{
			get
			{
				return this._navigationProperties;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002D2A File Offset: 0x00000F2A
		public IReadOnlyList<IConceptualHierarchy> Hierarchies
		{
			get
			{
				return this._hierarchies;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002D32 File Offset: 0x00000F32
		public IReadOnlyList<IConceptualDisplayFolder> DisplayFolders
		{
			get
			{
				return this._displayFolders;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D3A File Offset: 0x00000F3A
		public IReadOnlyList<IConceptualColumn> KeyColumns
		{
			get
			{
				return this._keyColumns;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002D42 File Offset: 0x00000F42
		public IReadOnlyList<IConceptualProperty> DefaultFieldProperties
		{
			get
			{
				return this._defaultFieldProperties;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002D4A File Offset: 0x00000F4A
		public IConceptualColumn DefaultLabelColumn
		{
			get
			{
				return this._defaultLabel;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002D52 File Offset: 0x00000F52
		public IConceptualColumn DefaultImageColumn
		{
			get
			{
				return this._defaultImage;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002D5A File Offset: 0x00000F5A
		public ConceptualEntityStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002D62 File Offset: 0x00000F62
		public ConceptualTableType ConceptualResultType
		{
			get
			{
				if (this._conceptualResultType == null)
				{
					this._conceptualResultType = this.BuildConceptualResultType();
				}
				return this._conceptualResultType;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002D7E File Offset: 0x00000F7E
		internal int? RowCount
		{
			get
			{
				return this._rowCount;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002D86 File Offset: 0x00000F86
		public string StableName
		{
			get
			{
				return this._stableName;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D8E File Offset: 0x00000F8E
		public override string ToString()
		{
			return this.GetFullName();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D96 File Offset: 0x00000F96
		public string GetFullName()
		{
			return this._edmContainerName + "." + this._edmName;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002DAE File Offset: 0x00000FAE
		public bool Equals(IConceptualEntity other)
		{
			return this == other;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public bool TryGetPropertyByEdmName(string edmName, out IConceptualProperty conceptualProperty)
		{
			return this._edmNamesToProps.TryGetValue(edmName, out conceptualProperty);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002DC3 File Offset: 0x00000FC3
		public bool TryGetProperty(string referenceName, out IConceptualProperty conceptualProp)
		{
			return this._propNamesToProps.TryGetValue(referenceName, out conceptualProp);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002DD2 File Offset: 0x00000FD2
		public bool TryGetHierarchy(string name, out IConceptualHierarchy conceptualHierarchy)
		{
			return this._hierarchyNamesToHierarchies.TryGetValue(name, out conceptualHierarchy);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002DE1 File Offset: 0x00000FE1
		public bool TryGetHierarchyByEdmName(string edmName, out IConceptualHierarchy conceptualHierarchy)
		{
			return this._hierarchyEdmNamesToHierarchies.TryGetValue(edmName, out conceptualHierarchy);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002DF0 File Offset: 0x00000FF0
		internal void CompleteInitialization(IConceptualSchema schema, IReadOnlyList<IConceptualNavigationProperty> navigationProperties, ConceptualEntityStatistics statistics)
		{
			this._schema = schema;
			this._navigationProperties = navigationProperties;
			this._statistics = statistics;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E08 File Offset: 0x00001008
		private static ReadOnlyDictionary<string, IConceptualProperty> BuildPropertiesByName(ICollection<IConceptualProperty> props)
		{
			new List<ConceptualTypeColumn>(props.Count);
			Dictionary<string, IConceptualProperty> dictionary = new Dictionary<string, IConceptualProperty>(props.Count, ConceptualNameComparer.Instance);
			foreach (IConceptualProperty conceptualProperty in props)
			{
				dictionary.Add(conceptualProperty.Name, conceptualProperty);
			}
			return dictionary.AsReadOnlyDictionary<string, IConceptualProperty>();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E7C File Offset: 0x0000107C
		private static void BuildHierarchiesByName(IReadOnlyList<IConceptualHierarchy> hierarchies, out ReadOnlyDictionary<string, IConceptualHierarchy> hierarchiesByName, out ReadOnlyDictionary<string, IConceptualHierarchy> hierarchiesByEdmName)
		{
			Dictionary<string, IConceptualHierarchy> dictionary = new Dictionary<string, IConceptualHierarchy>(hierarchies.Count, ConceptualNameComparer.Instance);
			Dictionary<string, IConceptualHierarchy> dictionary2 = new Dictionary<string, IConceptualHierarchy>(hierarchies.Count, EdmNameComparer.Instance);
			for (int i = 0; i < hierarchies.Count; i++)
			{
				IConceptualHierarchy conceptualHierarchy = hierarchies[i];
				dictionary.Add(conceptualHierarchy.Name, conceptualHierarchy);
				dictionary2.Add(conceptualHierarchy.EdmName, conceptualHierarchy);
			}
			hierarchiesByName = dictionary.AsReadOnlyDictionary<string, IConceptualHierarchy>();
			hierarchiesByEdmName = dictionary2.AsReadOnlyDictionary<string, IConceptualHierarchy>();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002EF0 File Offset: 0x000010F0
		private ConceptualTableType BuildConceptualResultType()
		{
			List<ConceptualTypeColumn> list = new List<ConceptualTypeColumn>(this.Properties.Count);
			foreach (IConceptualProperty conceptualProperty in this.Properties)
			{
				IConceptualColumn conceptualColumn = conceptualProperty as IConceptualColumn;
				if (conceptualColumn != null)
				{
					list.Add(conceptualColumn.ConceptualTypeColumn);
				}
			}
			return list.Table();
		}

		// Token: 0x0400004A RID: 74
		private readonly string _name;

		// Token: 0x0400004B RID: 75
		private readonly string _edmName;

		// Token: 0x0400004C RID: 76
		private readonly string _displayName;

		// Token: 0x0400004D RID: 77
		private readonly string _description;

		// Token: 0x0400004E RID: 78
		private readonly string _edmContainerName;

		// Token: 0x0400004F RID: 79
		private readonly ConceptualEntityVisibilityType _visibility;

		// Token: 0x04000050 RID: 80
		private readonly bool _isDateTable;

		// Token: 0x04000051 RID: 81
		private readonly int? _rowCount;

		// Token: 0x04000052 RID: 82
		private readonly IReadOnlyList<IConceptualProperty> _properties;

		// Token: 0x04000053 RID: 83
		private readonly IReadOnlyList<IConceptualHierarchy> _hierarchies;

		// Token: 0x04000054 RID: 84
		private readonly IReadOnlyList<IConceptualDisplayFolder> _displayFolders;

		// Token: 0x04000055 RID: 85
		private readonly IReadOnlyList<IConceptualColumn> _keyColumns;

		// Token: 0x04000056 RID: 86
		private readonly IConceptualColumn _defaultImage;

		// Token: 0x04000057 RID: 87
		private readonly IConceptualColumn _defaultLabel;

		// Token: 0x04000058 RID: 88
		private readonly string _stableName;

		// Token: 0x04000059 RID: 89
		private readonly IReadOnlyList<IConceptualProperty> _defaultFieldProperties;

		// Token: 0x0400005A RID: 90
		private readonly ReadOnlyDictionary<string, IConceptualProperty> _edmNamesToProps;

		// Token: 0x0400005B RID: 91
		private readonly ReadOnlyDictionary<string, IConceptualProperty> _propNamesToProps;

		// Token: 0x0400005C RID: 92
		private readonly ReadOnlyDictionary<string, IConceptualHierarchy> _hierarchyNamesToHierarchies;

		// Token: 0x0400005D RID: 93
		private readonly ReadOnlyDictionary<string, IConceptualHierarchy> _hierarchyEdmNamesToHierarchies;

		// Token: 0x0400005E RID: 94
		private IConceptualSchema _schema;

		// Token: 0x0400005F RID: 95
		private IReadOnlyList<IConceptualNavigationProperty> _navigationProperties;

		// Token: 0x04000060 RID: 96
		private ConceptualEntityStatistics _statistics;

		// Token: 0x04000061 RID: 97
		private ConceptualTableType _conceptualResultType;
	}
}
