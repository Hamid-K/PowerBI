using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.InfoNav.Data.Library
{
	// Token: 0x02000077 RID: 119
	[DebuggerDisplay("{Name}")]
	[ImmutableObject(true)]
	public abstract class ConceptualProperty : IConceptualProperty, IConceptualDisplayItem, IEquatable<IConceptualProperty>, IBuiltConceptualType
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x000074A5 File Offset: 0x000056A5
		protected ConceptualProperty(ConceptualProperty.ConceptualPropertyInfo propertyInfo)
		{
			this._propertyInfo = propertyInfo;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x000074B4 File Offset: 0x000056B4
		public string Name
		{
			get
			{
				return this._propertyInfo.Name;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000074C1 File Offset: 0x000056C1
		public string EdmName
		{
			get
			{
				return this._propertyInfo.EdmName;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x000074CE File Offset: 0x000056CE
		public string DisplayName
		{
			get
			{
				return this._propertyInfo.DisplayName;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x000074DB File Offset: 0x000056DB
		public IConceptualEntity Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000074E3 File Offset: 0x000056E3
		public DataType Type
		{
			get
			{
				return this._propertyInfo.Type;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000074F0 File Offset: 0x000056F0
		public PropertyDataCategory DataCategory
		{
			get
			{
				return PropertyDataCategory.None;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000074F3 File Offset: 0x000056F3
		public bool IsHidden
		{
			get
			{
				return this._propertyInfo.IsHidden;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00007500 File Offset: 0x00005700
		public bool IsPrivate
		{
			get
			{
				return this._propertyInfo.IsPrivate;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000750D File Offset: 0x0000570D
		public int Ordinal
		{
			get
			{
				return this._propertyInfo.Ordinal;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000751A File Offset: 0x0000571A
		public string FormatString
		{
			get
			{
				return this._propertyInfo.FormatString;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00007527 File Offset: 0x00005727
		public ConceptualPrimitiveType ConceptualDataType
		{
			get
			{
				return this._propertyInfo.ConceptualDataType;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00007534 File Offset: 0x00005734
		public ConceptualDataCategory ConceptualDataCategory
		{
			get
			{
				return this._propertyInfo.ConceptualDataCategory;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00007541 File Offset: 0x00005741
		public string Description
		{
			get
			{
				return this._propertyInfo.Description;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000754E File Offset: 0x0000574E
		public string StableName
		{
			get
			{
				return this._propertyInfo.StableName;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000755B File Offset: 0x0000575B
		public bool IsStable
		{
			get
			{
				return this._propertyInfo.IsStable;
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00007568 File Offset: 0x00005768
		public bool Equals(IConceptualProperty other)
		{
			return this == other;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000756E File Offset: 0x0000576E
		public string GetFullName()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000195 RID: 405
		private readonly ConceptualProperty.ConceptualPropertyInfo _propertyInfo;

		// Token: 0x04000196 RID: 406
		private IConceptualEntity _entity;

		// Token: 0x020002FD RID: 765
		public abstract class Builder<T> : ConceptualBuilderBase<T> where T : ConceptualProperty
		{
			// Token: 0x06001938 RID: 6456 RVA: 0x0002D62A File Offset: 0x0002B82A
			protected Builder(T conceptualProperty)
				: base(conceptualProperty)
			{
			}

			// Token: 0x06001939 RID: 6457 RVA: 0x0002D633 File Offset: 0x0002B833
			protected T CompletePropertyInitialization(IConceptualEntity entity)
			{
				Contract.CheckValue<IConceptualEntity>(entity, "entity");
				base.ActiveObject._entity = entity;
				return base.CompleteBaseInitialization();
			}
		}

		// Token: 0x020002FE RID: 766
		protected sealed class ConceptualPropertyInfo
		{
			// Token: 0x0600193A RID: 6458 RVA: 0x0002D658 File Offset: 0x0002B858
			internal ConceptualPropertyInfo(string name, string edmName, string displayName, string description, DataType type, bool isHidden, bool isPrivate, string formatString, ConceptualPrimitiveType conceptualDataType, ConceptualDataCategory conceptualDataCategory, int ordinal, string stableName, bool isStable)
			{
				if (string.IsNullOrEmpty(edmName))
				{
					edmName = name;
				}
				this.Name = name;
				this.EdmName = edmName;
				this.DisplayName = displayName ?? name;
				this.Description = description;
				this.Type = type;
				this.IsHidden = isHidden;
				this.IsPrivate = isPrivate;
				this.FormatString = formatString;
				this.ConceptualDataType = conceptualDataType;
				this.ConceptualDataCategory = conceptualDataCategory;
				this.Ordinal = ordinal;
				this.StableName = stableName;
				this.IsStable = isStable;
			}

			// Token: 0x04000944 RID: 2372
			internal readonly string Name;

			// Token: 0x04000945 RID: 2373
			internal readonly string EdmName;

			// Token: 0x04000946 RID: 2374
			internal readonly string DisplayName;

			// Token: 0x04000947 RID: 2375
			internal readonly string Description;

			// Token: 0x04000948 RID: 2376
			internal readonly bool IsHidden;

			// Token: 0x04000949 RID: 2377
			internal readonly bool IsPrivate;

			// Token: 0x0400094A RID: 2378
			internal readonly string FormatString;

			// Token: 0x0400094B RID: 2379
			internal readonly DataType Type;

			// Token: 0x0400094C RID: 2380
			internal readonly ConceptualPrimitiveType ConceptualDataType;

			// Token: 0x0400094D RID: 2381
			internal readonly ConceptualDataCategory ConceptualDataCategory;

			// Token: 0x0400094E RID: 2382
			internal readonly int Ordinal;

			// Token: 0x0400094F RID: 2383
			internal readonly string StableName;

			// Token: 0x04000950 RID: 2384
			internal readonly bool IsStable;
		}
	}
}
