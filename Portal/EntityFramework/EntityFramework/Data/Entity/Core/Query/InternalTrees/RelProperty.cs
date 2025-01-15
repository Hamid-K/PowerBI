using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003D5 RID: 981
	internal sealed class RelProperty
	{
		// Token: 0x06002EBC RID: 11964 RVA: 0x00094D76 File Offset: 0x00092F76
		internal RelProperty(RelationshipType relationshipType, RelationshipEndMember fromEnd, RelationshipEndMember toEnd)
		{
			this.m_relationshipType = relationshipType;
			this.m_fromEnd = fromEnd;
			this.m_toEnd = toEnd;
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002EBD RID: 11965 RVA: 0x00094D93 File Offset: 0x00092F93
		public RelationshipType Relationship
		{
			get
			{
				return this.m_relationshipType;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002EBE RID: 11966 RVA: 0x00094D9B File Offset: 0x00092F9B
		public RelationshipEndMember FromEnd
		{
			get
			{
				return this.m_fromEnd;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002EBF RID: 11967 RVA: 0x00094DA3 File Offset: 0x00092FA3
		public RelationshipEndMember ToEnd
		{
			get
			{
				return this.m_toEnd;
			}
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x00094DAC File Offset: 0x00092FAC
		public override bool Equals(object obj)
		{
			RelProperty relProperty = obj as RelProperty;
			return relProperty != null && this.Relationship.EdmEquals(relProperty.Relationship) && this.FromEnd.EdmEquals(relProperty.FromEnd) && this.ToEnd.EdmEquals(relProperty.ToEnd);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x00094DFC File Offset: 0x00092FFC
		public override int GetHashCode()
		{
			return this.ToEnd.Identity.GetHashCode();
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x00094E10 File Offset: 0x00093010
		[DebuggerNonUserCode]
		public override string ToString()
		{
			string[] array = new string[5];
			int num = 0;
			RelationshipType relationshipType = this.m_relationshipType;
			array[num] = ((relationshipType != null) ? relationshipType.ToString() : null);
			array[1] = ":";
			int num2 = 2;
			RelationshipEndMember fromEnd = this.m_fromEnd;
			array[num2] = ((fromEnd != null) ? fromEnd.ToString() : null);
			array[3] = ":";
			int num3 = 4;
			RelationshipEndMember toEnd = this.m_toEnd;
			array[num3] = ((toEnd != null) ? toEnd.ToString() : null);
			return string.Concat(array);
		}

		// Token: 0x04000FC1 RID: 4033
		private readonly RelationshipType m_relationshipType;

		// Token: 0x04000FC2 RID: 4034
		private readonly RelationshipEndMember m_fromEnd;

		// Token: 0x04000FC3 RID: 4035
		private readonly RelationshipEndMember m_toEnd;
	}
}
