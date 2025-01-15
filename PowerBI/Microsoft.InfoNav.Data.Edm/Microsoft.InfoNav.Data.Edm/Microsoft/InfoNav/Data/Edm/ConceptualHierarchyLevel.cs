using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000012 RID: 18
	[DebuggerDisplay("{Name}")]
	[ImmutableObject(true)]
	internal sealed class ConceptualHierarchyLevel : IConceptualHierarchyLevel, IConceptualDisplayItem, IEquatable<IConceptualHierarchyLevel>
	{
		// Token: 0x0600006E RID: 110 RVA: 0x0000324A File Offset: 0x0000144A
		internal ConceptualHierarchyLevel(string name, string edmName, string displayName, string description, IConceptualProperty source, string stableName)
		{
			this._name = name;
			this._edmName = edmName;
			this._displayName = displayName;
			this._description = description;
			this._source = source;
			this._stableName = stableName;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000327F File Offset: 0x0000147F
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003287 File Offset: 0x00001487
		public string EdmName
		{
			get
			{
				return this._edmName;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000328F File Offset: 0x0000148F
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003297 File Offset: 0x00001497
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000329F File Offset: 0x0000149F
		public IConceptualProperty Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000032A7 File Offset: 0x000014A7
		public IConceptualHierarchy Hierarchy
		{
			get
			{
				return this._hierarchy;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000032AF File Offset: 0x000014AF
		public string StableName
		{
			get
			{
				return this._stableName;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000032B7 File Offset: 0x000014B7
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000032BF File Offset: 0x000014BF
		public bool Equals(IConceptualHierarchyLevel other)
		{
			return this == other;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000032C5 File Offset: 0x000014C5
		internal void CompleteInitialization(IConceptualHierarchy hierarchy)
		{
			this._hierarchy = hierarchy;
		}

		// Token: 0x0400006B RID: 107
		private readonly string _name;

		// Token: 0x0400006C RID: 108
		private readonly string _edmName;

		// Token: 0x0400006D RID: 109
		private readonly string _displayName;

		// Token: 0x0400006E RID: 110
		private readonly string _description;

		// Token: 0x0400006F RID: 111
		private readonly string _stableName;

		// Token: 0x04000070 RID: 112
		private readonly IConceptualProperty _source;

		// Token: 0x04000071 RID: 113
		private IConceptualHierarchy _hierarchy;
	}
}
