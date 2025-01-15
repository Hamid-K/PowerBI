using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000471 RID: 1137
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class EdmRelationshipAttribute : Attribute
	{
		// Token: 0x0600378B RID: 14219 RVA: 0x000B5F44 File Offset: 0x000B4144
		public EdmRelationshipAttribute(string relationshipNamespaceName, string relationshipName, string role1Name, RelationshipMultiplicity role1Multiplicity, Type role1Type, string role2Name, RelationshipMultiplicity role2Multiplicity, Type role2Type)
		{
			this._relationshipNamespaceName = relationshipNamespaceName;
			this._relationshipName = relationshipName;
			this._role1Name = role1Name;
			this._role1Multiplicity = role1Multiplicity;
			this._role1Type = role1Type;
			this._role2Name = role2Name;
			this._role2Multiplicity = role2Multiplicity;
			this._role2Type = role2Type;
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000B5F94 File Offset: 0x000B4194
		public EdmRelationshipAttribute(string relationshipNamespaceName, string relationshipName, string role1Name, RelationshipMultiplicity role1Multiplicity, Type role1Type, string role2Name, RelationshipMultiplicity role2Multiplicity, Type role2Type, bool isForeignKey)
		{
			this._relationshipNamespaceName = relationshipNamespaceName;
			this._relationshipName = relationshipName;
			this._role1Name = role1Name;
			this._role1Multiplicity = role1Multiplicity;
			this._role1Type = role1Type;
			this._role2Name = role2Name;
			this._role2Multiplicity = role2Multiplicity;
			this._role2Type = role2Type;
			this._isForeignKey = isForeignKey;
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600378D RID: 14221 RVA: 0x000B5FEC File Offset: 0x000B41EC
		public string RelationshipNamespaceName
		{
			get
			{
				return this._relationshipNamespaceName;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x0600378E RID: 14222 RVA: 0x000B5FF4 File Offset: 0x000B41F4
		public string RelationshipName
		{
			get
			{
				return this._relationshipName;
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x0600378F RID: 14223 RVA: 0x000B5FFC File Offset: 0x000B41FC
		public string Role1Name
		{
			get
			{
				return this._role1Name;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06003790 RID: 14224 RVA: 0x000B6004 File Offset: 0x000B4204
		public RelationshipMultiplicity Role1Multiplicity
		{
			get
			{
				return this._role1Multiplicity;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06003791 RID: 14225 RVA: 0x000B600C File Offset: 0x000B420C
		public Type Role1Type
		{
			get
			{
				return this._role1Type;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x000B6014 File Offset: 0x000B4214
		public string Role2Name
		{
			get
			{
				return this._role2Name;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000B601C File Offset: 0x000B421C
		public RelationshipMultiplicity Role2Multiplicity
		{
			get
			{
				return this._role2Multiplicity;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06003794 RID: 14228 RVA: 0x000B6024 File Offset: 0x000B4224
		public Type Role2Type
		{
			get
			{
				return this._role2Type;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x000B602C File Offset: 0x000B422C
		public bool IsForeignKey
		{
			get
			{
				return this._isForeignKey;
			}
		}

		// Token: 0x040012CB RID: 4811
		private readonly string _relationshipNamespaceName;

		// Token: 0x040012CC RID: 4812
		private readonly string _relationshipName;

		// Token: 0x040012CD RID: 4813
		private readonly string _role1Name;

		// Token: 0x040012CE RID: 4814
		private readonly string _role2Name;

		// Token: 0x040012CF RID: 4815
		private readonly RelationshipMultiplicity _role1Multiplicity;

		// Token: 0x040012D0 RID: 4816
		private readonly RelationshipMultiplicity _role2Multiplicity;

		// Token: 0x040012D1 RID: 4817
		private readonly Type _role1Type;

		// Token: 0x040012D2 RID: 4818
		private readonly Type _role2Type;

		// Token: 0x040012D3 RID: 4819
		private readonly bool _isForeignKey;
	}
}
