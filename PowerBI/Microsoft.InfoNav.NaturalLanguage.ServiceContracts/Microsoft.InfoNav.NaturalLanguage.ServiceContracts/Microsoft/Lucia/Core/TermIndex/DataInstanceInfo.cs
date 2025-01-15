using System;
using System.ComponentModel;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000160 RID: 352
	[ImmutableObject(true)]
	public sealed class DataInstanceInfo : EntityTermInfo, IEquatable<DataInstanceInfo>
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x0000C0FB File Offset: 0x0000A2FB
		public DataInstanceInfo(EdmPropertyRef entity)
		{
			this.Entity = entity;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0000C10A File Offset: 0x0000A30A
		public DataInstanceInfo(EdmPropertyRef entity, EdmPropertyRef targetEntity, string synonymTarget)
			: this(entity)
		{
			this.Entity = entity;
			this.TargetEntity = targetEntity;
			this.SynonymTarget = synonymTarget;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0000C128 File Offset: 0x0000A328
		public EdmPropertyRef Entity { get; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0000C130 File Offset: 0x0000A330
		public EdmPropertyRef TargetEntity { get; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0000C138 File Offset: 0x0000A338
		public string SynonymTarget { get; }

		// Token: 0x060006FD RID: 1789 RVA: 0x0000C140 File Offset: 0x0000A340
		public bool Equals(DataInstanceInfo other)
		{
			bool? flag = Util.AreEqual<DataInstanceInfo>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return object.Equals(this.Entity, other.Entity) && object.Equals(this.TargetEntity, other.TargetEntity) && ConceptualNameComparer.Instance.Equals(this.SynonymTarget, other.SynonymTarget);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		public override int GetHashCode()
		{
			int num = Hashing.CombineHash(this.Entity.GetHashCode(), this.TargetEntity.GetHashCode());
			if (!string.IsNullOrEmpty(this.SynonymTarget))
			{
				num = Hashing.CombineHash(num, this.SynonymTarget.GetHashCode());
			}
			return num;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0000C214 File Offset: 0x0000A414
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}{1}", this.Entity.ToString(), (!string.IsNullOrEmpty(this.SynonymTarget)) ? StringUtil.FormatInvariant(" ({0}:{1})", this.TargetEntity, this.SynonymTarget) : null);
		}
	}
}
