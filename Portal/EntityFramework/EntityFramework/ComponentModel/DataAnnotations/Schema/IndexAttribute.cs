using System;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Utilities;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.DataAnnotations.Schema
{
	// Token: 0x02000053 RID: 83
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class IndexAttribute : Attribute
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00008380 File Offset: 0x00006580
		public IndexAttribute()
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000838F File Offset: 0x0000658F
		public IndexAttribute(string name)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000083B1 File Offset: 0x000065B1
		public IndexAttribute(string name, int order)
		{
			Check.NotEmpty(name, "name");
			if (order < 0)
			{
				throw new ArgumentOutOfRangeException("order");
			}
			this._name = name;
			this._order = order;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000083E9 File Offset: 0x000065E9
		internal IndexAttribute(string name, bool? isClustered, bool? isUnique)
		{
			this._name = name;
			this._isClustered = isClustered;
			this._isUnique = isUnique;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000840D File Offset: 0x0000660D
		internal IndexAttribute(string name, int order, bool? isClustered, bool? isUnique)
		{
			this._name = name;
			this._order = order;
			this._isClustered = isClustered;
			this._isUnique = isUnique;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00008439 File Offset: 0x00006639
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00008441 File Offset: 0x00006641
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			internal set
			{
				this._name = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000844A File Offset: 0x0000664A
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00008452 File Offset: 0x00006652
		public virtual int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._order = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000846A File Offset: 0x0000666A
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00008486 File Offset: 0x00006686
		public virtual bool IsClustered
		{
			get
			{
				return this._isClustered != null && this._isClustered.Value;
			}
			set
			{
				this._isClustered = new bool?(value);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00008494 File Offset: 0x00006694
		public virtual bool IsClusteredConfigured
		{
			get
			{
				return this._isClustered != null;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000084A1 File Offset: 0x000066A1
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x000084BD File Offset: 0x000066BD
		public virtual bool IsUnique
		{
			get
			{
				return this._isUnique != null && this._isUnique.Value;
			}
			set
			{
				this._isUnique = new bool?(value);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x000084CB File Offset: 0x000066CB
		public virtual bool IsUniqueConfigured
		{
			get
			{
				return this._isUnique != null;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000084D8 File Offset: 0x000066D8
		public override object TypeId
		{
			get
			{
				return RuntimeHelpers.GetHashCode(this);
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000084E8 File Offset: 0x000066E8
		protected virtual bool Equals(IndexAttribute other)
		{
			return this._name == other._name && this._order == other._order && this._isClustered.Equals(other._isClustered) && this._isUnique.Equals(other._isUnique);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008552 File Offset: 0x00006752
		public override string ToString()
		{
			return IndexAnnotationSerializer.SerializeIndexAttribute(this);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000855A File Offset: 0x0000675A
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((IndexAttribute)obj)));
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008588 File Offset: 0x00006788
		public override int GetHashCode()
		{
			return (((((((base.GetHashCode() * 397) ^ ((this._name != null) ? this._name.GetHashCode() : 0)) * 397) ^ this._order) * 397) ^ this._isClustered.GetHashCode()) * 397) ^ this._isUnique.GetHashCode();
		}

		// Token: 0x040000A4 RID: 164
		private string _name;

		// Token: 0x040000A5 RID: 165
		private int _order = -1;

		// Token: 0x040000A6 RID: 166
		private bool? _isClustered;

		// Token: 0x040000A7 RID: 167
		private bool? _isUnique;
	}
}
