using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.ExtractByEntity;

namespace Microsoft.ProgramSynthesis.Transformation.Text.ExtractByEntity
{
	// Token: 0x02001DDA RID: 7642
	public struct EntityInstance : IEquatable<EntityInstance>
	{
		// Token: 0x17002A86 RID: 10886
		// (get) Token: 0x06010034 RID: 65588 RVA: 0x00370AAB File Offset: 0x0036ECAB
		public readonly EntityType EntityType { get; }

		// Token: 0x17002A87 RID: 10887
		// (get) Token: 0x06010035 RID: 65589 RVA: 0x00370AB3 File Offset: 0x0036ECB3
		public readonly int InstanceNumber { get; }

		// Token: 0x06010036 RID: 65590 RVA: 0x00370ABB File Offset: 0x0036ECBB
		public EntityInstance(EntityType entityType, int instanceNumber)
		{
			this.EntityType = entityType;
			this.InstanceNumber = instanceNumber;
		}

		// Token: 0x06010037 RID: 65591 RVA: 0x00370ACB File Offset: 0x0036ECCB
		public bool Equals(EntityInstance other)
		{
			return other.EntityType == this.EntityType && other.InstanceNumber == this.InstanceNumber;
		}

		// Token: 0x06010038 RID: 65592 RVA: 0x00370AED File Offset: 0x0036ECED
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((EntityInstance)obj);
		}

		// Token: 0x06010039 RID: 65593 RVA: 0x00370B20 File Offset: 0x0036ED20
		public override int GetHashCode()
		{
			return ((this.EntityType.GetHashCode() * 16889) ^ this.InstanceNumber.GetHashCode()) * 14431;
		}

		// Token: 0x0601003A RID: 65594 RVA: 0x00370B5C File Offset: 0x0036ED5C
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0} #{1}", new object[] { this.EntityType, this.InstanceNumber }));
		}
	}
}
