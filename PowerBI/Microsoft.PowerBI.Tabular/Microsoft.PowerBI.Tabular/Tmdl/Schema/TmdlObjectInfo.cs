using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Tmdl.Schema
{
	// Token: 0x02000159 RID: 345
	[DebuggerDisplay("TmdlObjectInfo - type={ObjectType}")]
	internal sealed class TmdlObjectInfo : TmdlSchemaElement, ITmdlObjectContainer, ITmdlPropertiesContainer, IEquatable<TmdlObjectInfo>
	{
		// Token: 0x060015A6 RID: 5542 RVA: 0x00090D10 File Offset: 0x0008EF10
		public TmdlObjectInfo(ObjectType objectType)
		{
			this.ObjectType = objectType;
			this.RequiresName = ObjectTreeHelper.IsNamedObject(this.ObjectType) || (ObjectTreeHelper.IsKeyedObject(this.ObjectType) && this.ObjectType != ObjectType.CalculationExpression);
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00090D5D File Offset: 0x0008EF5D
		public TmdlObjectInfo(string propertyName)
		{
			this.ObjectType = ObjectType.Null;
			this.RequiresName = false;
			this.keyword = propertyName;
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00090D7A File Offset: 0x0008EF7A
		public ObjectType ObjectType { get; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x00090D82 File Offset: 0x0008EF82
		// (set) Token: 0x060015AA RID: 5546 RVA: 0x00090D9C File Offset: 0x0008EF9C
		public string PropertyName
		{
			get
			{
				if (this.ObjectType == ObjectType.Null || this.isSingleton)
				{
					return this.keyword;
				}
				return null;
			}
			internal set
			{
				base.EnsureNotReadOnly();
				this.keyword = value;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x00090DAB File Offset: 0x0008EFAB
		// (set) Token: 0x060015AC RID: 5548 RVA: 0x00090DB3 File Offset: 0x0008EFB3
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				base.EnsureNotReadOnly();
				this.description = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x00090DC2 File Offset: 0x0008EFC2
		// (set) Token: 0x060015AE RID: 5550 RVA: 0x00090DD7 File Offset: 0x0008EFD7
		public bool IsSingleton
		{
			get
			{
				return this.isSingleton || this.ObjectType == ObjectType.Null;
			}
			internal set
			{
				base.EnsureNotReadOnly();
				this.isSingleton = value;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x00090DE6 File Offset: 0x0008EFE6
		// (set) Token: 0x060015B0 RID: 5552 RVA: 0x00090DEE File Offset: 0x0008EFEE
		public TmdlPropertyInfo DefaultProperty
		{
			get
			{
				return this.defaultProperty;
			}
			set
			{
				base.EnsureNotReadOnly();
				this.defaultProperty = value;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x00090DFD File Offset: 0x0008EFFD
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x00090E05 File Offset: 0x0008F005
		public TmdlPropertyInfo NameProperty
		{
			get
			{
				return this.nameProperty;
			}
			set
			{
				base.EnsureNotReadOnly();
				this.nameProperty = value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x00090E14 File Offset: 0x0008F014
		// (set) Token: 0x060015B4 RID: 5556 RVA: 0x00090E1C File Offset: 0x0008F01C
		public TmdlPropertyInfo DescriptionProperty
		{
			get
			{
				return this.descriptionProperty;
			}
			set
			{
				base.EnsureNotReadOnly();
				this.descriptionProperty = value;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x00090E2C File Offset: 0x0008F02C
		public IReadOnlyCollection<TmdlPropertyInfo> Properties
		{
			get
			{
				IReadOnlyCollection<TmdlPropertyInfo> readOnlyCollection = this.properties;
				return readOnlyCollection ?? TmdlPropertyInfo.EmptyPropertyList;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x00090E4C File Offset: 0x0008F04C
		public IReadOnlyCollection<TmdlObjectInfo> ChildObjects
		{
			get
			{
				IReadOnlyCollection<TmdlObjectInfo> readOnlyCollection = this.childObjects;
				return readOnlyCollection ?? TmdlObjectInfo.EmptyObjectList;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00090E6A File Offset: 0x0008F06A
		public bool HasVariants
		{
			get
			{
				return this.variants != null && this.variants.Count > 0;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x00090E84 File Offset: 0x0008F084
		public IReadOnlyDictionary<string, TmdlObjectInfo> Variants
		{
			get
			{
				IReadOnlyDictionary<string, TmdlObjectInfo> readOnlyDictionary = this.variants;
				return readOnlyDictionary ?? TmdlObjectInfo.EmptyVariants;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x00090EA4 File Offset: 0x0008F0A4
		internal bool IsEmpty
		{
			get
			{
				return this.DefaultProperty == null && this.NameProperty == null && this.DescriptionProperty == null && (this.properties == null || this.properties.Count <= 0) && (this.childObjects == null || this.childObjects.Count <= 0) && !this.HasVariants;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00090F08 File Offset: 0x0008F108
		internal string Keyword
		{
			get
			{
				if (string.IsNullOrEmpty(this.keyword))
				{
					ObjectType objectType = this.ObjectType;
					if (objectType != ObjectType.Culture)
					{
						if (objectType != ObjectType.RoleMembership)
						{
							this.keyword = this.ObjectType.ToString("G");
						}
						else
						{
							this.keyword = "Member";
						}
					}
					else
					{
						this.keyword = "CultureInfo";
					}
				}
				return this.keyword;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x00090F70 File Offset: 0x0008F170
		internal bool RequiresName { get; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x00090F78 File Offset: 0x0008F178
		internal bool HasDescription
		{
			get
			{
				return this.DescriptionProperty != null;
			}
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00090F84 File Offset: 0x0008F184
		public TmdlPropertyInfo FindProperty(string propertyName)
		{
			if (!this.HasVariants)
			{
				if (this.DefaultProperty != null && propertyName.Equals(this.DefaultProperty.Name, StringComparison.InvariantCultureIgnoreCase))
				{
					return this.DefaultProperty;
				}
				if (this.properties == null)
				{
					return null;
				}
				return this.properties.FirstOrDefault((TmdlPropertyInfo x) => propertyName.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase));
			}
			else
			{
				TmdlObjectInfo value = this.variants.FirstOrDefault((KeyValuePair<string, TmdlObjectInfo> v) => v.Value.DefaultProperty != null).Value;
				TmdlPropertyInfo tmdlPropertyInfo = ((value != null) ? value.DefaultProperty : null);
				if (tmdlPropertyInfo != null && propertyName.Equals(tmdlPropertyInfo.Name, StringComparison.InvariantCultureIgnoreCase))
				{
					return tmdlPropertyInfo;
				}
				Func<TmdlPropertyInfo, bool> <>9__3;
				foreach (KeyValuePair<string, TmdlObjectInfo> keyValuePair in this.variants.Where((KeyValuePair<string, TmdlObjectInfo> v) => v.Value.HasAnyProperty(false, false)))
				{
					IEnumerable<TmdlPropertyInfo> enumerable = keyValuePair.Value.Properties;
					Func<TmdlPropertyInfo, bool> func;
					if ((func = <>9__3) == null)
					{
						func = (<>9__3 = (TmdlPropertyInfo x) => propertyName.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase));
					}
					TmdlPropertyInfo tmdlPropertyInfo2 = enumerable.FirstOrDefault(func);
					if (tmdlPropertyInfo2 != null)
					{
						return tmdlPropertyInfo2;
					}
				}
				return null;
			}
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x000910E8 File Offset: 0x0008F2E8
		public bool TryGetPropertyInfo(string keyword, out TmdlPropertyInfo info)
		{
			info = this.FindProperty(keyword);
			return info != null;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000910F8 File Offset: 0x0008F2F8
		public bool TryGetObjectInfo(string keyword, out TmdlObjectInfo info)
		{
			TmdlObjectInfo.AdjustDeprecatedObjectKeywordIfNeeded(ref keyword);
			if (this.HasVariants)
			{
				TmdlObjectInfo firstVariant = this.variants.Select((KeyValuePair<string, TmdlObjectInfo> v) => v.Value).First<TmdlObjectInfo>();
				info = (firstVariant.HasAnyChild(true) ? firstVariant.childObjects.FirstOrDefault((TmdlObjectInfo x) => keyword.Equals(x.Keyword, StringComparison.InvariantCultureIgnoreCase)) : null);
				if (info != null || this.variants.Count <= 1)
				{
					goto IL_017A;
				}
				IEnumerable<TmdlObjectInfo> enumerable = this.variants.Select((KeyValuePair<string, TmdlObjectInfo> v) => v.Value);
				Func<TmdlObjectInfo, bool> func;
				Func<TmdlObjectInfo, bool> <>9__4;
				if ((func = <>9__4) == null)
				{
					func = (<>9__4 = (TmdlObjectInfo v) => v != firstVariant);
				}
				using (IEnumerator<TmdlObjectInfo> enumerator = enumerable.Where(func).GetEnumerator())
				{
					Func<TmdlObjectInfo, bool> <>9__5;
					while (enumerator.MoveNext())
					{
						TmdlObjectInfo tmdlObjectInfo = enumerator.Current;
						if (tmdlObjectInfo.HasAnyChild(true))
						{
							IEnumerable<TmdlObjectInfo> enumerable2 = tmdlObjectInfo.childObjects;
							Func<TmdlObjectInfo, bool> func2;
							if ((func2 = <>9__5) == null)
							{
								func2 = (<>9__5 = (TmdlObjectInfo x) => x.ObjectType == ObjectType.Null && keyword.Equals(x.Keyword, StringComparison.InvariantCultureIgnoreCase));
							}
							info = enumerable2.FirstOrDefault(func2);
						}
						if (info != null)
						{
							break;
						}
					}
					goto IL_017A;
				}
			}
			if (this.childObjects != null)
			{
				info = this.childObjects.FirstOrDefault((TmdlObjectInfo x) => keyword.Equals(x.Keyword, StringComparison.InvariantCultureIgnoreCase));
			}
			else
			{
				info = null;
			}
			IL_017A:
			return info != null;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00091294 File Offset: 0x0008F494
		internal static void AdjustDeprecatedObjectKeywordIfNeeded(ref string keyword)
		{
			if (keyword.Equals("Culture", StringComparison.InvariantCultureIgnoreCase))
			{
				keyword = "CultureInfo";
			}
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x000912AC File Offset: 0x0008F4AC
		internal bool HasAnyProperty(bool includeDefaultProperty, bool includeEmbeddedProperties)
		{
			return (includeDefaultProperty && this.DefaultProperty != null) || (includeEmbeddedProperties && (this.NameProperty != null || this.DescriptionProperty != null)) || (this.properties != null && this.properties.Count > 0);
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x000912E8 File Offset: 0x0008F4E8
		internal void AddProperty(TmdlPropertyInfo property)
		{
			base.EnsureNotReadOnly();
			if (this.properties == null)
			{
				this.properties = new List<TmdlPropertyInfo>();
			}
			this.properties.Add(property);
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0009130F File Offset: 0x0008F50F
		internal bool RemoveProperty(TmdlPropertyInfo property)
		{
			base.EnsureNotReadOnly();
			return this.properties != null && this.properties.Remove(property);
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0009132D File Offset: 0x0008F52D
		internal void ClearProperties()
		{
			base.EnsureNotReadOnly();
			if (this.properties != null)
			{
				this.properties.Clear();
			}
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00091348 File Offset: 0x0008F548
		internal bool HasAnyChild(bool includeNoneMetadataObjects)
		{
			if (this.childObjects == null || this.childObjects.Count == 0)
			{
				return false;
			}
			if (includeNoneMetadataObjects)
			{
				return true;
			}
			return this.childObjects.Any((TmdlObjectInfo c) => c.ObjectType > ObjectType.Null);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0009139B File Offset: 0x0008F59B
		internal void AddChildObject(TmdlObjectInfo child)
		{
			base.EnsureNotReadOnly();
			if (this.childObjects == null)
			{
				this.childObjects = new List<TmdlObjectInfo>();
			}
			this.childObjects.Add(child);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x000913C2 File Offset: 0x0008F5C2
		internal bool RemoveChildObject(TmdlObjectInfo child)
		{
			base.EnsureNotReadOnly();
			return this.childObjects != null && this.childObjects.Remove(child);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x000913E0 File Offset: 0x0008F5E0
		internal void ClearChildObjects()
		{
			base.EnsureNotReadOnly();
			if (this.childObjects != null)
			{
				this.childObjects.Clear();
			}
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x000913FB File Offset: 0x0008F5FB
		internal void AddVariant(string key, TmdlObjectInfo variant)
		{
			base.EnsureNotReadOnly();
			if (this.variants == null)
			{
				this.variants = new Dictionary<string, TmdlObjectInfo>();
			}
			this.variants.Add(key, variant);
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00091423 File Offset: 0x0008F623
		internal bool RemoveVariant(string key)
		{
			base.EnsureNotReadOnly();
			return this.variants != null && this.variants.Remove(key);
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00091441 File Offset: 0x0008F641
		internal void ClearVariants()
		{
			base.EnsureNotReadOnly();
			if (this.variants != null)
			{
				this.variants.Clear();
			}
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0009145C File Offset: 0x0008F65C
		internal bool IsDefaultPropertyAllowed(out TmdlPropertyInfo effectiveDefaultProperty)
		{
			if (!this.HasVariants)
			{
				effectiveDefaultProperty = this.DefaultProperty;
			}
			else
			{
				effectiveDefaultProperty = (from v in this.variants
					where v.Value.DefaultProperty != null
					select v.Value.DefaultProperty).FirstOrDefault<TmdlPropertyInfo>();
			}
			return effectiveDefaultProperty != null;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x000914D8 File Offset: 0x0008F6D8
		internal TmdlObjectInfo Clone(string singletonPropertyName = null, bool makeReadOnly = false)
		{
			TmdlObjectInfo tmdlObjectInfo;
			if (this.ObjectType == ObjectType.Null)
			{
				tmdlObjectInfo = new TmdlObjectInfo(this.keyword)
				{
					Description = this.Description
				};
			}
			else
			{
				tmdlObjectInfo = new TmdlObjectInfo(this.ObjectType)
				{
					IsSingleton = this.IsSingleton,
					Description = this.Description
				};
				if (!string.IsNullOrEmpty(singletonPropertyName))
				{
					tmdlObjectInfo.PropertyName = singletonPropertyName;
				}
			}
			if (base.IsReadOnly && makeReadOnly)
			{
				tmdlObjectInfo.defaultProperty = this.defaultProperty;
				tmdlObjectInfo.nameProperty = this.nameProperty;
				tmdlObjectInfo.descriptionProperty = this.descriptionProperty;
				tmdlObjectInfo.properties = this.properties;
				tmdlObjectInfo.childObjects = this.childObjects;
				tmdlObjectInfo.variants = this.variants;
			}
			else
			{
				if (this.defaultProperty != null)
				{
					if (!makeReadOnly || !this.defaultProperty.IsReadOnly)
					{
						tmdlObjectInfo.defaultProperty = this.defaultProperty.Clone(makeReadOnly);
					}
					else
					{
						tmdlObjectInfo.defaultProperty = this.defaultProperty;
					}
				}
				if (this.nameProperty != null)
				{
					if (!makeReadOnly || !this.nameProperty.IsReadOnly)
					{
						tmdlObjectInfo.nameProperty = this.nameProperty.Clone(makeReadOnly);
					}
					else
					{
						tmdlObjectInfo.nameProperty = this.nameProperty;
					}
				}
				if (this.descriptionProperty != null)
				{
					if (!makeReadOnly || !this.descriptionProperty.IsReadOnly)
					{
						tmdlObjectInfo.descriptionProperty = this.descriptionProperty.Clone(makeReadOnly);
					}
					else
					{
						tmdlObjectInfo.descriptionProperty = this.descriptionProperty;
					}
				}
				if (this.properties != null)
				{
					tmdlObjectInfo.properties = new List<TmdlPropertyInfo>(this.properties.Count);
					for (int i = 0; i < this.properties.Count; i++)
					{
						if (!makeReadOnly || !this.properties[i].IsReadOnly)
						{
							tmdlObjectInfo.properties.Add(this.properties[i].Clone(makeReadOnly));
						}
						else
						{
							tmdlObjectInfo.properties.Add(this.properties[i]);
						}
					}
				}
				if (this.childObjects != null)
				{
					tmdlObjectInfo.childObjects = new List<TmdlObjectInfo>(this.childObjects.Count);
					for (int j = 0; j < this.childObjects.Count; j++)
					{
						if (!makeReadOnly || !this.childObjects[j].IsReadOnly)
						{
							tmdlObjectInfo.childObjects.Add(this.childObjects[j].Clone(null, makeReadOnly));
						}
						else
						{
							tmdlObjectInfo.childObjects.Add(this.childObjects[j]);
						}
					}
				}
				if (this.HasVariants)
				{
					tmdlObjectInfo.variants = new Dictionary<string, TmdlObjectInfo>(this.variants.Count);
					foreach (KeyValuePair<string, TmdlObjectInfo> keyValuePair in this.variants)
					{
						if (!makeReadOnly || !keyValuePair.Value.IsReadOnly)
						{
							tmdlObjectInfo.variants.Add(keyValuePair.Key, keyValuePair.Value.Clone(null, makeReadOnly));
						}
						else
						{
							tmdlObjectInfo.variants.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
			}
			if (makeReadOnly)
			{
				tmdlObjectInfo.MakeReadOnly();
			}
			return tmdlObjectInfo;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x000917F4 File Offset: 0x0008F9F4
		private protected override void MakeReadOnlyImpl()
		{
			if (this.defaultProperty != null && !this.defaultProperty.IsReadOnly)
			{
				this.defaultProperty.MakeReadOnly();
			}
			if (this.nameProperty != null && !this.nameProperty.IsReadOnly)
			{
				this.nameProperty.MakeReadOnly();
			}
			if (this.descriptionProperty != null && !this.descriptionProperty.IsReadOnly)
			{
				this.descriptionProperty.MakeReadOnly();
			}
			if (this.properties != null)
			{
				foreach (TmdlPropertyInfo tmdlPropertyInfo in this.properties.Where((TmdlPropertyInfo p) => !p.IsReadOnly))
				{
					tmdlPropertyInfo.MakeReadOnly();
				}
			}
			if (this.childObjects != null)
			{
				foreach (TmdlObjectInfo tmdlObjectInfo in this.childObjects.Where((TmdlObjectInfo c) => !c.IsReadOnly))
				{
					tmdlObjectInfo.MakeReadOnly();
				}
			}
			if (this.variants != null)
			{
				foreach (TmdlObjectInfo tmdlObjectInfo2 in from v in this.variants
					where !v.Value.IsReadOnly
					select v.Value)
				{
					tmdlObjectInfo2.MakeReadOnly();
				}
			}
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000919B8 File Offset: 0x0008FBB8
		bool IEquatable<TmdlObjectInfo>.Equals(TmdlObjectInfo other)
		{
			if (this.ObjectType != other.ObjectType)
			{
				return false;
			}
			if (string.Compare(this.PropertyName, other.PropertyName, StringComparison.InvariantCultureIgnoreCase) != 0)
			{
				return false;
			}
			if (this.properties == null || this.properties.Count == 0)
			{
				if (other.properties != null && other.properties.Count > 0)
				{
					return false;
				}
			}
			else
			{
				if (other.properties == null || other.properties.Count == 0)
				{
					return false;
				}
				if (this.properties.Count != other.properties.Count)
				{
					return false;
				}
				Dictionary<string, TmdlPropertyInfo> dictionary = new Dictionary<string, TmdlPropertyInfo>(this.properties.Count);
				foreach (TmdlPropertyInfo tmdlPropertyInfo in this.properties)
				{
					dictionary.Add(tmdlPropertyInfo.Name, tmdlPropertyInfo);
				}
				foreach (TmdlPropertyInfo tmdlPropertyInfo2 in other.properties)
				{
					TmdlPropertyInfo tmdlPropertyInfo3;
					if (!dictionary.TryGetValue(tmdlPropertyInfo2.Name, out tmdlPropertyInfo3))
					{
						return false;
					}
					if (tmdlPropertyInfo3.Type != tmdlPropertyInfo2.Type)
					{
						return false;
					}
					TmdlValueType type = tmdlPropertyInfo3.Type;
					if (type != TmdlValueType.Scalar)
					{
						if (type - TmdlValueType.MetadataObject <= 1)
						{
							if (tmdlPropertyInfo3.MetadataObjectType != null && tmdlPropertyInfo2.MetadataObjectType != null && tmdlPropertyInfo3.MetadataObjectType.Value != tmdlPropertyInfo2.MetadataObjectType.Value)
							{
								return false;
							}
						}
					}
					else if (tmdlPropertyInfo3.ScalarValueType != null && tmdlPropertyInfo2.ScalarValueType != null)
					{
						if (tmdlPropertyInfo3.ScalarValueType.Value != tmdlPropertyInfo2.ScalarValueType.Value)
						{
							return false;
						}
						if (tmdlPropertyInfo3.ScalarValueType.Value == TmdlScalarValueType.Enum && tmdlPropertyInfo3.EnumType != null && tmdlPropertyInfo2.EnumType != null && tmdlPropertyInfo3.EnumType != tmdlPropertyInfo2.EnumType)
						{
							return false;
						}
					}
				}
			}
			if (this.childObjects == null || this.childObjects.Count == 0)
			{
				if (other.childObjects != null && other.childObjects.Count > 0)
				{
					return false;
				}
			}
			else
			{
				if (other.childObjects == null || other.childObjects.Count == 0)
				{
					return false;
				}
				if (this.childObjects.Count != other.childObjects.Count)
				{
					return false;
				}
				Dictionary<string, TmdlObjectInfo> dictionary2 = new Dictionary<string, TmdlObjectInfo>(this.childObjects.Count);
				foreach (TmdlObjectInfo tmdlObjectInfo in this.childObjects)
				{
					dictionary2.Add(tmdlObjectInfo.Keyword, tmdlObjectInfo);
				}
				foreach (TmdlObjectInfo tmdlObjectInfo2 in other.childObjects)
				{
					TmdlObjectInfo tmdlObjectInfo3;
					if (!dictionary2.TryGetValue(tmdlObjectInfo2.Keyword, out tmdlObjectInfo3))
					{
						return false;
					}
					if (!((IEquatable<TmdlObjectInfo>)tmdlObjectInfo3).Equals(tmdlObjectInfo2))
					{
						return false;
					}
				}
			}
			if (this.HasVariants)
			{
				if (!other.HasVariants)
				{
					return false;
				}
				if (this.variants.Count != other.variants.Count)
				{
					return false;
				}
				using (Dictionary<string, TmdlObjectInfo>.Enumerator enumerator3 = other.variants.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						KeyValuePair<string, TmdlObjectInfo> keyValuePair = enumerator3.Current;
						TmdlObjectInfo tmdlObjectInfo4;
						if (!this.variants.TryGetValue(keyValuePair.Key, out tmdlObjectInfo4))
						{
							return false;
						}
						if (!((IEquatable<TmdlObjectInfo>)tmdlObjectInfo4).Equals(keyValuePair.Value))
						{
							return false;
						}
					}
					return true;
				}
			}
			if (other.HasVariants)
			{
				return false;
			}
			return true;
		}

		// Token: 0x040003E3 RID: 995
		private const string DeprecatedCultureObjectKeyword = "Culture";

		// Token: 0x040003E4 RID: 996
		private static readonly IReadOnlyCollection<TmdlObjectInfo> EmptyObjectList = new List<TmdlObjectInfo>(0);

		// Token: 0x040003E5 RID: 997
		private static readonly IReadOnlyDictionary<string, TmdlObjectInfo> EmptyVariants = new Dictionary<string, TmdlObjectInfo>(0);

		// Token: 0x040003E6 RID: 998
		private bool isSingleton;

		// Token: 0x040003E7 RID: 999
		private TmdlPropertyInfo defaultProperty;

		// Token: 0x040003E8 RID: 1000
		private TmdlPropertyInfo nameProperty;

		// Token: 0x040003E9 RID: 1001
		private TmdlPropertyInfo descriptionProperty;

		// Token: 0x040003EA RID: 1002
		private List<TmdlPropertyInfo> properties;

		// Token: 0x040003EB RID: 1003
		private List<TmdlObjectInfo> childObjects;

		// Token: 0x040003EC RID: 1004
		private Dictionary<string, TmdlObjectInfo> variants;

		// Token: 0x040003ED RID: 1005
		private string keyword;

		// Token: 0x040003EE RID: 1006
		private string description;
	}
}
