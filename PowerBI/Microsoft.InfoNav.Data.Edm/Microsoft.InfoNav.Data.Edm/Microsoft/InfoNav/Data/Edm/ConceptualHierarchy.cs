using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000011 RID: 17
	[DebuggerDisplay("{Name}")]
	[ImmutableObject(true)]
	internal sealed class ConceptualHierarchy : IConceptualHierarchy, IConceptualDisplayItem, IEquatable<IConceptualHierarchy>
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00003108 File Offset: 0x00001308
		internal ConceptualHierarchy(string name, string edmName, string displayName, string description, bool isHidden, IEnumerable<ConceptualHierarchyLevel> levels, string stableName)
		{
			this._name = name;
			this._edmName = edmName;
			this._displayName = displayName;
			this._description = description;
			this._isHidden = isHidden;
			this._levels = levels.Cast<IConceptualHierarchyLevel>().AsReadOnlyCollection<IConceptualHierarchyLevel>();
			ConceptualHierarchy.BuildLevelsByName(this._levels, out this._levelsByName, out this._levelsByEdmName);
			this._stableName = stableName;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003171 File Offset: 0x00001371
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003179 File Offset: 0x00001379
		public string EdmName
		{
			get
			{
				return this._edmName;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003181 File Offset: 0x00001381
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003189 File Offset: 0x00001389
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003191 File Offset: 0x00001391
		public bool IsHidden
		{
			get
			{
				return this._isHidden;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003199 File Offset: 0x00001399
		public IReadOnlyList<IConceptualHierarchyLevel> Levels
		{
			get
			{
				return this._levels;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000031A1 File Offset: 0x000013A1
		public string StableName
		{
			get
			{
				return this._stableName;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000031A9 File Offset: 0x000013A9
		public bool TryGetLevel(string name, out IConceptualHierarchyLevel conceptualHierarchyLevel)
		{
			return this._levelsByName.TryGetValue(name, out conceptualHierarchyLevel);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000031B8 File Offset: 0x000013B8
		public bool TryGetLevelByEdmName(string edmName, out IConceptualHierarchyLevel conceptualHierarchyLevel)
		{
			return this._levelsByEdmName.TryGetValue(edmName, out conceptualHierarchyLevel);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000031C7 File Offset: 0x000013C7
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000031CF File Offset: 0x000013CF
		public bool Equals(IConceptualHierarchy other)
		{
			return this == other;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000031D8 File Offset: 0x000013D8
		private static void BuildLevelsByName(IReadOnlyList<IConceptualHierarchyLevel> levels, out ReadOnlyDictionary<string, IConceptualHierarchyLevel> levelsByName, out ReadOnlyDictionary<string, IConceptualHierarchyLevel> levelsByEdmName)
		{
			Dictionary<string, IConceptualHierarchyLevel> dictionary = new Dictionary<string, IConceptualHierarchyLevel>(levels.Count, ConceptualNameComparer.Instance);
			Dictionary<string, IConceptualHierarchyLevel> dictionary2 = new Dictionary<string, IConceptualHierarchyLevel>(levels.Count, EdmNameComparer.Instance);
			for (int i = 0; i < levels.Count; i++)
			{
				IConceptualHierarchyLevel conceptualHierarchyLevel = levels[i];
				dictionary.Add(conceptualHierarchyLevel.Name, conceptualHierarchyLevel);
				dictionary2.Add(conceptualHierarchyLevel.EdmName, conceptualHierarchyLevel);
			}
			levelsByName = dictionary.AsReadOnlyDictionary<string, IConceptualHierarchyLevel>();
			levelsByEdmName = dictionary2.AsReadOnlyDictionary<string, IConceptualHierarchyLevel>();
		}

		// Token: 0x04000062 RID: 98
		private readonly string _name;

		// Token: 0x04000063 RID: 99
		private readonly string _edmName;

		// Token: 0x04000064 RID: 100
		private readonly string _displayName;

		// Token: 0x04000065 RID: 101
		private readonly string _description;

		// Token: 0x04000066 RID: 102
		private readonly bool _isHidden;

		// Token: 0x04000067 RID: 103
		private readonly IReadOnlyList<IConceptualHierarchyLevel> _levels;

		// Token: 0x04000068 RID: 104
		private readonly ReadOnlyDictionary<string, IConceptualHierarchyLevel> _levelsByName;

		// Token: 0x04000069 RID: 105
		private readonly ReadOnlyDictionary<string, IConceptualHierarchyLevel> _levelsByEdmName;

		// Token: 0x0400006A RID: 106
		private readonly string _stableName;
	}
}
