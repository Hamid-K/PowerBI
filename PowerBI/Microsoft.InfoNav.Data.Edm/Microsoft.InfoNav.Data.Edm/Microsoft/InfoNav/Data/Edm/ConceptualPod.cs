using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000015 RID: 21
	[DebuggerDisplay("{Name}")]
	[ImmutableObject(true)]
	internal sealed class ConceptualPod : IConceptualPod, IConceptualEntity, IConceptualDisplayItem, IEquatable<IConceptualEntity>
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000033CC File Offset: 0x000015CC
		internal ConceptualPod(string name, string edmName, string description, string entityContainerName, bool isHidden, IReadOnlyList<IConceptualPodParameter> parameters, bool cortanaEnabled, string stableName)
		{
			this._name = name;
			this._edmName = edmName;
			this._description = description;
			this._entityContainerName = entityContainerName;
			this._isHidden = isHidden;
			this._parameters = parameters;
			this._navProps = ConceptualPod.GetNavigationProperties(parameters);
			this._cortanaEnabled = cortanaEnabled;
			this._stableName = stableName;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003429 File Offset: 0x00001629
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003431 File Offset: 0x00001631
		public string EdmName
		{
			get
			{
				return this._edmName;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003439 File Offset: 0x00001639
		public string DisplayName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003441 File Offset: 0x00001641
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003449 File Offset: 0x00001649
		public string EntityContainerName
		{
			get
			{
				return this._entityContainerName;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003451 File Offset: 0x00001651
		public IConceptualSchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003459 File Offset: 0x00001659
		public string Extends
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000345C File Offset: 0x0000165C
		public ConceptualEntityVisibilityType Visibility
		{
			get
			{
				if (!this._isHidden)
				{
					return ConceptualEntityVisibilityType.AlwaysVisible;
				}
				return ConceptualEntityVisibilityType.Hidden;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003469 File Offset: 0x00001669
		public IReadOnlyList<IConceptualPodParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003471 File Offset: 0x00001671
		public bool CortanaEnabled
		{
			get
			{
				return this._cortanaEnabled;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003479 File Offset: 0x00001679
		public IReadOnlyList<IConceptualNavigationProperty> NavigationProperties
		{
			get
			{
				return this._navProps;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003481 File Offset: 0x00001681
		public bool IsDateTable
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003488 File Offset: 0x00001688
		public IReadOnlyList<IConceptualProperty> Properties
		{
			get
			{
				return Util.EmptyReadOnlyCollection<IConceptualProperty>();
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000348F File Offset: 0x0000168F
		public IReadOnlyList<IConceptualHierarchy> Hierarchies
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003496 File Offset: 0x00001696
		public IReadOnlyList<IConceptualDisplayFolder> DisplayFolders
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000349D File Offset: 0x0000169D
		public IReadOnlyList<IConceptualColumn> KeyColumns
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000034A4 File Offset: 0x000016A4
		public IConceptualColumn DefaultLabelColumn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000034AB File Offset: 0x000016AB
		public IConceptualColumn DefaultImageColumn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000034B2 File Offset: 0x000016B2
		public IReadOnlyList<IConceptualProperty> DefaultFieldProperties
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000034B9 File Offset: 0x000016B9
		public ConceptualEntityStatistics Statistics
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000034C0 File Offset: 0x000016C0
		public ConceptualTableType ConceptualResultType
		{
			get
			{
				return ConceptualTableType.Empty;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000034C7 File Offset: 0x000016C7
		public string StableName
		{
			get
			{
				return this._stableName;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000034CF File Offset: 0x000016CF
		public bool TryGetPropertyByEdmName(string propName, out IConceptualProperty conceptualProp)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000034D6 File Offset: 0x000016D6
		public bool TryGetProperty(string referenceName, out IConceptualProperty conceptualProp)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000034DD File Offset: 0x000016DD
		public bool TryGetHierarchy(string name, out IConceptualHierarchy conceptualHierarchy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000034E4 File Offset: 0x000016E4
		public bool TryGetHierarchyByEdmName(string edmName, out IConceptualHierarchy conceptualHierarchy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000034EB File Offset: 0x000016EB
		public string GetFullName()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[] { this._entityContainerName, this._edmName });
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003514 File Offset: 0x00001714
		public bool Equals(IConceptualEntity other)
		{
			return this == other;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000351A File Offset: 0x0000171A
		internal void CompleteInitialization(IConceptualSchema schema)
		{
			this._schema = schema;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003524 File Offset: 0x00001724
		private static IReadOnlyList<IConceptualNavigationProperty> GetNavigationProperties(IReadOnlyList<IConceptualPodParameter> parameters)
		{
			IConceptualNavigationProperty[] array = new IConceptualNavigationProperty[parameters.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = parameters[i].NavigationProperty;
			}
			return array;
		}

		// Token: 0x04000080 RID: 128
		private readonly string _name;

		// Token: 0x04000081 RID: 129
		private readonly string _edmName;

		// Token: 0x04000082 RID: 130
		private readonly string _description;

		// Token: 0x04000083 RID: 131
		private readonly string _entityContainerName;

		// Token: 0x04000084 RID: 132
		private readonly bool _isHidden;

		// Token: 0x04000085 RID: 133
		private readonly IReadOnlyList<IConceptualPodParameter> _parameters;

		// Token: 0x04000086 RID: 134
		private readonly IReadOnlyList<IConceptualNavigationProperty> _navProps;

		// Token: 0x04000087 RID: 135
		private readonly bool _cortanaEnabled;

		// Token: 0x04000088 RID: 136
		private readonly string _stableName;

		// Token: 0x04000089 RID: 137
		private IConceptualSchema _schema;
	}
}
