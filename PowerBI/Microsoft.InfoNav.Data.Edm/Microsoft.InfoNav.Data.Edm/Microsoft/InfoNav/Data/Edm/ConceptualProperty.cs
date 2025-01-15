using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000016 RID: 22
	[DebuggerDisplay("{Name}")]
	[ImmutableObject(true)]
	internal abstract class ConceptualProperty : IConceptualProperty, IConceptualDisplayItem, IEquatable<IConceptualProperty>
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x0000355C File Offset: 0x0000175C
		protected ConceptualProperty(ParsedEdmStructuralProperty parsedEdmStructuralProperty, DataType type, ConceptualPrimitiveType conceptualPrimitiveType, int ordinal, string description)
		{
			this._parsedEdmStructuralProperty = parsedEdmStructuralProperty;
			this._dataCategory = parsedEdmStructuralProperty.PropertyDataCategory;
			this._type = type;
			this._ordinal = ordinal;
			this._description = description;
			this._name = parsedEdmStructuralProperty.ReferenceName;
			this._fullName = parsedEdmStructuralProperty.FullName;
			this._displayName = parsedEdmStructuralProperty.DisplayName;
			this._isHidden = parsedEdmStructuralProperty.IsHidden;
			this._isPrivate = parsedEdmStructuralProperty.IsPrivate;
			this._formatString = parsedEdmStructuralProperty.FormatString;
			this._keepUniqueRows = parsedEdmStructuralProperty.ShouldKeepUniqueRows;
			this._isStable = parsedEdmStructuralProperty.IsStable;
			this._conceptualDataCategory = parsedEdmStructuralProperty.ConceptualDataCategory;
			this._conceptualDataType = conceptualPrimitiveType;
			this._stableName = this.ParsedEdmStructuralProperty.StableName;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000361D File Offset: 0x0000181D
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003625 File Offset: 0x00001825
		public string EdmName
		{
			get
			{
				return this._parsedEdmStructuralProperty.EdmName;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003632 File Offset: 0x00001832
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000363A File Offset: 0x0000183A
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003642 File Offset: 0x00001842
		public IConceptualEntity Entity
		{
			get
			{
				return this._parentEntity;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000364A File Offset: 0x0000184A
		public DataType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003652 File Offset: 0x00001852
		public PropertyDataCategory DataCategory
		{
			get
			{
				return this._dataCategory;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000365A File Offset: 0x0000185A
		public int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003662 File Offset: 0x00001862
		public bool IsHidden
		{
			get
			{
				return this._isHidden;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000366A File Offset: 0x0000186A
		public bool IsPrivate
		{
			get
			{
				return this._isPrivate;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003672 File Offset: 0x00001872
		public string FormatString
		{
			get
			{
				return this._formatString;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000367A File Offset: 0x0000187A
		public ConceptualPrimitiveType ConceptualDataType
		{
			get
			{
				return this._conceptualDataType;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003682 File Offset: 0x00001882
		public ConceptualDataCategory ConceptualDataCategory
		{
			get
			{
				return this._conceptualDataCategory;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000368A File Offset: 0x0000188A
		internal bool KeepUniqueRows
		{
			get
			{
				return this._keepUniqueRows;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003692 File Offset: 0x00001892
		public bool IsStable
		{
			get
			{
				return this._isStable;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000369A File Offset: 0x0000189A
		internal ParsedEdmStructuralProperty ParsedEdmStructuralProperty
		{
			get
			{
				return this._parsedEdmStructuralProperty;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000036A2 File Offset: 0x000018A2
		public string StableName
		{
			get
			{
				return this._stableName;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000036AA File Offset: 0x000018AA
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000036B2 File Offset: 0x000018B2
		public bool Equals(IConceptualProperty other)
		{
			return this == other;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000036B8 File Offset: 0x000018B8
		public string GetFullName()
		{
			return this._fullName;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000036C0 File Offset: 0x000018C0
		protected void CompleteInitialization(IConceptualEntity parentEntity)
		{
			this._parentEntity = parentEntity;
		}

		// Token: 0x0400008A RID: 138
		private readonly ParsedEdmStructuralProperty _parsedEdmStructuralProperty;

		// Token: 0x0400008B RID: 139
		private readonly DataType _type;

		// Token: 0x0400008C RID: 140
		private readonly PropertyDataCategory _dataCategory;

		// Token: 0x0400008D RID: 141
		private readonly int _ordinal;

		// Token: 0x0400008E RID: 142
		private readonly string _name;

		// Token: 0x0400008F RID: 143
		private readonly string _fullName;

		// Token: 0x04000090 RID: 144
		private readonly string _displayName;

		// Token: 0x04000091 RID: 145
		private readonly string _description;

		// Token: 0x04000092 RID: 146
		private readonly string _formatString;

		// Token: 0x04000093 RID: 147
		private readonly bool _isHidden;

		// Token: 0x04000094 RID: 148
		private readonly bool _isPrivate;

		// Token: 0x04000095 RID: 149
		private readonly bool _keepUniqueRows;

		// Token: 0x04000096 RID: 150
		private readonly bool _isStable;

		// Token: 0x04000097 RID: 151
		private readonly ConceptualPrimitiveType _conceptualDataType;

		// Token: 0x04000098 RID: 152
		private readonly ConceptualDataCategory _conceptualDataCategory;

		// Token: 0x04000099 RID: 153
		private readonly string _stableName;

		// Token: 0x0400009A RID: 154
		private IConceptualEntity _parentEntity;
	}
}
