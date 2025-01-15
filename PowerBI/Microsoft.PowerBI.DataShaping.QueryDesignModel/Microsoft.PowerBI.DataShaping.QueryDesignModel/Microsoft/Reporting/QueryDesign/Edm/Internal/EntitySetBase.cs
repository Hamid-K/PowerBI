using System;
using Microsoft.Data.Metadata.Edm;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200023E RID: 574
	public abstract class EntitySetBase : EdmItem, IEquatable<EntitySetBase>
	{
		// Token: 0x06001949 RID: 6473 RVA: 0x00044EB0 File Offset: 0x000430B0
		protected EntitySetBase(EntitySetBase entitySetBase)
		{
			this._entitySetBase = ArgumentValidation.CheckNotNull<EntitySetBase>(entitySetBase, "entitySetBase");
			this._fullName = new Name(EntitySetBase.GetFullName(entitySetBase));
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x00044EDA File Offset: 0x000430DA
		public string Name
		{
			get
			{
				return this._entitySetBase.Name;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x00044EE7 File Offset: 0x000430E7
		public Name FullName
		{
			get
			{
				return this._fullName;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x00044EEF File Offset: 0x000430EF
		internal EntitySetBase InternalEntitySetBase
		{
			get
			{
				return this._entitySetBase;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x00044EF7 File Offset: 0x000430F7
		internal sealed override MetadataItem InternalEdmItem
		{
			get
			{
				return this._entitySetBase;
			}
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00044EFF File Offset: 0x000430FF
		internal static string GetFullName(EntitySetBase entitySetBase)
		{
			return entitySetBase.EntityContainer.Name + "." + entitySetBase.Name;
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00044F1C File Offset: 0x0004311C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EntitySetBase);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00044F2C File Offset: 0x0004312C
		public override int GetHashCode()
		{
			return this.FullName.GetHashCode();
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00044F50 File Offset: 0x00043150
		public bool Equals(EntitySetBase other)
		{
			return this == other || (other != null && base.GetType() == other.GetType() && string.Equals(this.FullName, other.FullName, EdmItem.IdentityComparison));
		}

		// Token: 0x04000DCB RID: 3531
		private readonly EntitySetBase _entitySetBase;

		// Token: 0x04000DCC RID: 3532
		private readonly Name _fullName;
	}
}
