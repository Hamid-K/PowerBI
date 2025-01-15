using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000547 RID: 1351
	public class MappingFragment : StructuralTypeMapping
	{
		// Token: 0x06004204 RID: 16900 RVA: 0x000E0248 File Offset: 0x000DE448
		public MappingFragment(EntitySet storeEntitySet, TypeMapping typeMapping, bool makeColumnsDistinct)
		{
			Check.NotNull<EntitySet>(storeEntitySet, "storeEntitySet");
			Check.NotNull<TypeMapping>(typeMapping, "typeMapping");
			this.m_tableExtent = storeEntitySet;
			this.m_typeMapping = typeMapping;
			this.m_isSQueryDistinct = makeColumnsDistinct;
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06004205 RID: 16901 RVA: 0x000E02AE File Offset: 0x000DE4AE
		internal IEnumerable<ColumnMappingBuilder> ColumnMappings
		{
			get
			{
				return this._columnMappings;
			}
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x000E02B8 File Offset: 0x000DE4B8
		internal void AddColumnMapping(ColumnMappingBuilder columnMappingBuilder)
		{
			Check.NotNull<ColumnMappingBuilder>(columnMappingBuilder, "columnMappingBuilder");
			if (!columnMappingBuilder.PropertyPath.Any<EdmProperty>() || this._columnMappings.Contains(columnMappingBuilder))
			{
				throw new ArgumentException(Strings.InvalidColumnBuilderArgument("columnBuilderMapping"));
			}
			this._columnMappings.Add(columnMappingBuilder);
			StructuralTypeMapping structuralTypeMapping = this;
			EdmProperty property;
			Func<ComplexPropertyMapping, bool> <>9__1;
			int i;
			for (i = 0; i < columnMappingBuilder.PropertyPath.Count - 1; i++)
			{
				property = columnMappingBuilder.PropertyPath[i];
				IEnumerable<ComplexPropertyMapping> enumerable = structuralTypeMapping.PropertyMappings.OfType<ComplexPropertyMapping>();
				Func<ComplexPropertyMapping, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (ComplexPropertyMapping pm) => pm.Property == property);
				}
				ComplexPropertyMapping complexPropertyMapping = enumerable.SingleOrDefault(func);
				ComplexTypeMapping complexTypeMapping = null;
				if (complexPropertyMapping == null)
				{
					complexTypeMapping = new ComplexTypeMapping(false);
					complexTypeMapping.AddType(property.ComplexType);
					complexPropertyMapping = new ComplexPropertyMapping(property);
					complexPropertyMapping.AddTypeMapping(complexTypeMapping);
					structuralTypeMapping.AddPropertyMapping(complexPropertyMapping);
				}
				structuralTypeMapping = complexTypeMapping ?? complexPropertyMapping.TypeMappings.Single<ComplexTypeMapping>();
			}
			property = columnMappingBuilder.PropertyPath[i];
			ScalarPropertyMapping scalarPropertyMapping = structuralTypeMapping.PropertyMappings.OfType<ScalarPropertyMapping>().SingleOrDefault((ScalarPropertyMapping pm) => pm.Property == property);
			if (scalarPropertyMapping == null)
			{
				scalarPropertyMapping = new ScalarPropertyMapping(property, columnMappingBuilder.ColumnProperty);
				structuralTypeMapping.AddPropertyMapping(scalarPropertyMapping);
				columnMappingBuilder.SetTarget(scalarPropertyMapping);
				return;
			}
			scalarPropertyMapping.Column = columnMappingBuilder.ColumnProperty;
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x000E0424 File Offset: 0x000DE624
		internal void RemoveColumnMapping(ColumnMappingBuilder columnMappingBuilder)
		{
			this._columnMappings.Remove(columnMappingBuilder);
			MappingFragment.RemoveColumnMapping(this, columnMappingBuilder.PropertyPath);
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x000E0440 File Offset: 0x000DE640
		private static void RemoveColumnMapping(StructuralTypeMapping structuralTypeMapping, IEnumerable<EdmProperty> propertyPath)
		{
			PropertyMapping propertyMapping = structuralTypeMapping.PropertyMappings.Single((PropertyMapping pm) => pm.Property == propertyPath.First<EdmProperty>());
			if (propertyMapping is ScalarPropertyMapping)
			{
				structuralTypeMapping.RemovePropertyMapping(propertyMapping);
				return;
			}
			ComplexPropertyMapping complexPropertyMapping = (ComplexPropertyMapping)propertyMapping;
			ComplexTypeMapping complexTypeMapping = complexPropertyMapping.TypeMappings.Single<ComplexTypeMapping>();
			MappingFragment.RemoveColumnMapping(complexTypeMapping, propertyPath.Skip(1));
			if (!complexTypeMapping.PropertyMappings.Any<PropertyMapping>())
			{
				structuralTypeMapping.RemovePropertyMapping(complexPropertyMapping);
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06004209 RID: 16905 RVA: 0x000E04B9 File Offset: 0x000DE6B9
		// (set) Token: 0x0600420A RID: 16906 RVA: 0x000E04C1 File Offset: 0x000DE6C1
		public EntitySet StoreEntitySet
		{
			get
			{
				return this.m_tableExtent;
			}
			internal set
			{
				this.m_tableExtent = value;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x000E04CA File Offset: 0x000DE6CA
		// (set) Token: 0x0600420C RID: 16908 RVA: 0x000E04D2 File Offset: 0x000DE6D2
		internal EntitySet TableSet
		{
			get
			{
				return this.StoreEntitySet;
			}
			set
			{
				this.StoreEntitySet = value;
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x0600420D RID: 16909 RVA: 0x000E04DB File Offset: 0x000DE6DB
		internal EntityType Table
		{
			get
			{
				return this.m_tableExtent.ElementType;
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x0600420E RID: 16910 RVA: 0x000E04E8 File Offset: 0x000DE6E8
		public TypeMapping TypeMapping
		{
			get
			{
				return this.m_typeMapping;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x000E04F0 File Offset: 0x000DE6F0
		public bool MakeColumnsDistinct
		{
			get
			{
				return this.m_isSQueryDistinct;
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06004210 RID: 16912 RVA: 0x000E04F8 File Offset: 0x000DE6F8
		internal bool IsSQueryDistinct
		{
			get
			{
				return this.MakeColumnsDistinct;
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06004211 RID: 16913 RVA: 0x000E0500 File Offset: 0x000DE700
		internal ReadOnlyCollection<PropertyMapping> AllProperties
		{
			get
			{
				List<PropertyMapping> list = new List<PropertyMapping>();
				list.AddRange(this.m_properties);
				list.AddRange(this.m_conditionProperties.Values);
				return new ReadOnlyCollection<PropertyMapping>(list);
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06004212 RID: 16914 RVA: 0x000E0529 File Offset: 0x000DE729
		public override ReadOnlyCollection<PropertyMapping> PropertyMappings
		{
			get
			{
				return new ReadOnlyCollection<PropertyMapping>(this.m_properties);
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06004213 RID: 16915 RVA: 0x000E0536 File Offset: 0x000DE736
		public override ReadOnlyCollection<ConditionPropertyMapping> Conditions
		{
			get
			{
				return new ReadOnlyCollection<ConditionPropertyMapping>(new List<ConditionPropertyMapping>(this.m_conditionProperties.Values));
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06004214 RID: 16916 RVA: 0x000E054D File Offset: 0x000DE74D
		internal IEnumerable<ColumnMappingBuilder> FlattenedProperties
		{
			get
			{
				return MappingFragment.GetFlattenedProperties(this.m_properties, new List<EdmProperty>());
			}
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x000E055F File Offset: 0x000DE75F
		private static IEnumerable<ColumnMappingBuilder> GetFlattenedProperties(IEnumerable<PropertyMapping> propertyMappings, List<EdmProperty> propertyPath)
		{
			foreach (PropertyMapping propertyMapping in propertyMappings)
			{
				propertyPath.Add(propertyMapping.Property);
				ComplexPropertyMapping complexPropertyMapping = propertyMapping as ComplexPropertyMapping;
				if (complexPropertyMapping != null)
				{
					foreach (ColumnMappingBuilder columnMappingBuilder in MappingFragment.GetFlattenedProperties(complexPropertyMapping.TypeMappings.Single<ComplexTypeMapping>().PropertyMappings, propertyPath))
					{
						yield return columnMappingBuilder;
					}
					IEnumerator<ColumnMappingBuilder> enumerator2 = null;
				}
				else
				{
					ScalarPropertyMapping scalarPropertyMapping = propertyMapping as ScalarPropertyMapping;
					if (scalarPropertyMapping != null)
					{
						yield return new ColumnMappingBuilder(scalarPropertyMapping.Column, propertyPath.ToList<EdmProperty>());
					}
				}
				propertyPath.Remove(propertyMapping.Property);
				propertyMapping = null;
			}
			IEnumerator<PropertyMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06004216 RID: 16918 RVA: 0x000E0576 File Offset: 0x000DE776
		internal IEnumerable<ConditionPropertyMapping> ColumnConditions
		{
			get
			{
				return this.m_conditionProperties.Values;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06004217 RID: 16919 RVA: 0x000E0583 File Offset: 0x000DE783
		// (set) Token: 0x06004218 RID: 16920 RVA: 0x000E058B File Offset: 0x000DE78B
		internal int StartLineNumber { get; set; }

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06004219 RID: 16921 RVA: 0x000E0594 File Offset: 0x000DE794
		// (set) Token: 0x0600421A RID: 16922 RVA: 0x000E059C File Offset: 0x000DE79C
		internal int StartLinePosition { get; set; }

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x0600421B RID: 16923 RVA: 0x000E05A5 File Offset: 0x000DE7A5
		internal string SourceLocation
		{
			get
			{
				return this.m_typeMapping.SetMapping.EntityContainerMapping.SourceLocation;
			}
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x000E05BC File Offset: 0x000DE7BC
		public override void AddPropertyMapping(PropertyMapping propertyMapping)
		{
			Check.NotNull<PropertyMapping>(propertyMapping, "propertyMapping");
			base.ThrowIfReadOnly();
			this.m_properties.Add(propertyMapping);
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x000E05DC File Offset: 0x000DE7DC
		public override void RemovePropertyMapping(PropertyMapping propertyMapping)
		{
			Check.NotNull<PropertyMapping>(propertyMapping, "propertyMapping");
			base.ThrowIfReadOnly();
			this.m_properties.Remove(propertyMapping);
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x000E05FD File Offset: 0x000DE7FD
		public override void AddCondition(ConditionPropertyMapping condition)
		{
			Check.NotNull<ConditionPropertyMapping>(condition, "condition");
			base.ThrowIfReadOnly();
			this.AddConditionProperty(condition);
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x000E0618 File Offset: 0x000DE818
		public override void RemoveCondition(ConditionPropertyMapping condition)
		{
			Check.NotNull<ConditionPropertyMapping>(condition, "condition");
			base.ThrowIfReadOnly();
			this.RemoveConditionProperty(condition);
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x000E0633 File Offset: 0x000DE833
		internal void ClearConditions()
		{
			this.m_conditionProperties.Clear();
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x000E0640 File Offset: 0x000DE840
		internal override void SetReadOnly()
		{
			this.m_properties.TrimExcess();
			MappingItem.SetReadOnly(this.m_properties);
			MappingItem.SetReadOnly(this.m_conditionProperties.Values);
			base.SetReadOnly();
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x000E0670 File Offset: 0x000DE870
		internal void RemoveConditionProperty(ConditionPropertyMapping condition)
		{
			EdmProperty edmProperty = condition.Property ?? condition.Column;
			this.m_conditionProperties.Remove(edmProperty);
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x000E069B File Offset: 0x000DE89B
		internal void AddConditionProperty(ConditionPropertyMapping conditionPropertyMap)
		{
			this.AddConditionProperty(conditionPropertyMap, delegate(EdmMember _)
			{
			});
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x000E06C4 File Offset: 0x000DE8C4
		internal void AddConditionProperty(ConditionPropertyMapping conditionPropertyMap, Action<EdmMember> duplicateMemberConditionError)
		{
			EdmProperty edmProperty = conditionPropertyMap.Property ?? conditionPropertyMap.Column;
			if (!this.m_conditionProperties.ContainsKey(edmProperty))
			{
				this.m_conditionProperties.Add(edmProperty, conditionPropertyMap);
				return;
			}
			duplicateMemberConditionError(edmProperty);
		}

		// Token: 0x04001759 RID: 5977
		private readonly List<ColumnMappingBuilder> _columnMappings = new List<ColumnMappingBuilder>();

		// Token: 0x0400175A RID: 5978
		private EntitySet m_tableExtent;

		// Token: 0x0400175B RID: 5979
		private readonly TypeMapping m_typeMapping;

		// Token: 0x0400175C RID: 5980
		private readonly Dictionary<EdmProperty, ConditionPropertyMapping> m_conditionProperties = new Dictionary<EdmProperty, ConditionPropertyMapping>(EqualityComparer<EdmProperty>.Default);

		// Token: 0x0400175D RID: 5981
		private readonly List<PropertyMapping> m_properties = new List<PropertyMapping>();

		// Token: 0x0400175E RID: 5982
		private readonly bool m_isSQueryDistinct;
	}
}
