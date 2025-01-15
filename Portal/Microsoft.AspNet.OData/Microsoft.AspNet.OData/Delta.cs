using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.CSharp.RuntimeBinder;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000039 RID: 57
	[NonValidatingParameterBinding]
	public class Delta<TStructuralType> : TypedDelta, IDelta where TStructuralType : class
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00006780 File Offset: 0x00004980
		public Delta()
			: this(typeof(TStructuralType))
		{
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006792 File Offset: 0x00004992
		public Delta(Type structuralType)
			: this(structuralType, null)
		{
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000679C File Offset: 0x0000499C
		public Delta(Type structuralType, IEnumerable<string> updatableProperties)
			: this(structuralType, updatableProperties, null)
		{
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000067A7 File Offset: 0x000049A7
		public Delta(Type structuralType, IEnumerable<string> updatableProperties, PropertyInfo dynamicDictionaryPropertyInfo)
		{
			this._dynamicDictionaryPropertyinfo = dynamicDictionaryPropertyInfo;
			this.Reset(structuralType);
			this.InitializeProperties(updatableProperties);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000067C4 File Offset: 0x000049C4
		public override Type StructuredType
		{
			get
			{
				return this._structuredType;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000067CC File Offset: 0x000049CC
		public override Type ExpectedClrType
		{
			get
			{
				return typeof(TStructuralType);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000067D8 File Offset: 0x000049D8
		public override void Clear()
		{
			this.Reset(this._structuredType);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000067E6 File Offset: 0x000049E6
		public override bool TrySetPropertyValue(string name, object value)
		{
			if (value is IDelta)
			{
				return this.TrySetNestedResourceInternal(name, value);
			}
			return this.TrySetPropertyValueInternal(name, value);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006804 File Offset: 0x00004A04
		public override bool TryGetPropertyValue(string name, out object value)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			if (this._dynamicDictionaryPropertyinfo != null)
			{
				if (this._dynamicDictionaryCache == null)
				{
					this._dynamicDictionaryCache = Delta<TStructuralType>.GetDynamicPropertyDictionary(this._dynamicDictionaryPropertyinfo, this._instance, false);
				}
				if (this._dynamicDictionaryCache != null && this._dynamicDictionaryCache.TryGetValue(name, out value))
				{
					return true;
				}
			}
			if (this._deltaNestedResources.ContainsKey(name))
			{
				object obj = this._deltaNestedResources[name];
				FieldInfo field = obj.GetType().GetField("_instance", BindingFlags.Instance | BindingFlags.NonPublic);
				value = field.GetValue(obj);
				return true;
			}
			PropertyAccessor<TStructuralType> propertyAccessor;
			if (this._allProperties.TryGetValue(name, out propertyAccessor))
			{
				value = propertyAccessor.GetValue(this._instance);
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000068C4 File Offset: 0x00004AC4
		public override bool TryGetPropertyType(string name, out Type type)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			if (this._dynamicDictionaryPropertyinfo != null)
			{
				if (this._dynamicDictionaryCache == null)
				{
					this._dynamicDictionaryCache = Delta<TStructuralType>.GetDynamicPropertyDictionary(this._dynamicDictionaryPropertyinfo, this._instance, false);
				}
				object obj;
				if (this._dynamicDictionaryCache != null && this._dynamicDictionaryCache.TryGetValue(name, out obj))
				{
					if (obj == null)
					{
						type = null;
						return false;
					}
					type = obj.GetType();
					return true;
				}
			}
			PropertyAccessor<TStructuralType> propertyAccessor;
			if (this._allProperties.TryGetValue(name, out propertyAccessor))
			{
				type = propertyAccessor.Property.PropertyType;
				return true;
			}
			type = null;
			return false;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000695A File Offset: 0x00004B5A
		public TStructuralType GetInstance()
		{
			return this._instance;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006962 File Offset: 0x00004B62
		public override IEnumerable<string> GetChangedPropertyNames()
		{
			return this._changedProperties.Concat(this._deltaNestedResources.Keys);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000697A File Offset: 0x00004B7A
		public override IEnumerable<string> GetUnchangedPropertyNames()
		{
			return this._updatableProperties.Except(this.GetChangedPropertyNames());
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006990 File Offset: 0x00004B90
		public void CopyChangedValues(TStructuralType original)
		{
			if (original == null)
			{
				throw Error.ArgumentNull("original");
			}
			if (!this._structuredType.IsAssignableFrom(original.GetType()))
			{
				throw Error.Argument("original", SRResources.DeltaTypeMismatch, new object[]
				{
					this._structuredType,
					original.GetType()
				});
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PropertyAccessor<TStructuralType>[] array = this._changedProperties.Select((string s) => this._allProperties[s]).ToArray<PropertyAccessor<TStructuralType>>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Copy(this._instance, original);
			}
			this.CopyChangedDynamicValues(original);
			foreach (string text in this._deltaNestedResources.Keys)
			{
				object obj = this._deltaNestedResources[text];
				object obj2 = null;
				if (!Delta<TStructuralType>.TryGetPropertyRef(original, text, out obj2))
				{
					throw Error.Argument(text, SRResources.DeltaNestedResourceNameNotFound, new object[]
					{
						text,
						original.GetType()
					});
				}
				if (Delta<TStructuralType>.<>o__25.<>p__1 == null)
				{
					Delta<TStructuralType>.<>o__25.<>p__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(Delta<TStructuralType>), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
				}
				Func<CallSite, object, bool> target = Delta<TStructuralType>.<>o__25.<>p__1.Target;
				CallSite <>p__ = Delta<TStructuralType>.<>o__25.<>p__1;
				if (Delta<TStructuralType>.<>o__25.<>p__0 == null)
				{
					Delta<TStructuralType>.<>o__25.<>p__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(Delta<TStructuralType>), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				if (target(<>p__, Delta<TStructuralType>.<>o__25.<>p__0.Target(Delta<TStructuralType>.<>o__25.<>p__0, obj2, null)))
				{
					object obj3 = this._deltaNestedResources[text];
					if (Delta<TStructuralType>.<>o__25.<>p__2 == null)
					{
						Delta<TStructuralType>.<>o__25.<>p__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetInstance", null, typeof(Delta<TStructuralType>), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					object obj4 = Delta<TStructuralType>.<>o__25.<>p__2.Target(Delta<TStructuralType>.<>o__25.<>p__2, obj3);
					if (Delta<TStructuralType>.<>o__25.<>p__3 == null)
					{
						Delta<TStructuralType>.<>o__25.<>p__3 = CallSite<Action<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "CopyChangedValues", null, typeof(Delta<TStructuralType>), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Delta<TStructuralType>.<>o__25.<>p__3.Target(Delta<TStructuralType>.<>o__25.<>p__3, obj3, obj4);
					if (Delta<TStructuralType>.<>o__25.<>p__4 == null)
					{
						Delta<TStructuralType>.<>o__25.<>p__4 = CallSite<Action<CallSite, PropertyAccessor<TStructuralType>, TStructuralType, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetValue", null, typeof(Delta<TStructuralType>), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Delta<TStructuralType>.<>o__25.<>p__4.Target(Delta<TStructuralType>.<>o__25.<>p__4, this._allProperties[text], original, obj4);
				}
				else
				{
					if (Delta<TStructuralType>.<>o__25.<>p__7 == null)
					{
						Delta<TStructuralType>.<>o__25.<>p__7 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(bool), typeof(Delta<TStructuralType>)));
					}
					Func<CallSite, object, bool> target2 = Delta<TStructuralType>.<>o__25.<>p__7.Target;
					CallSite <>p__2 = Delta<TStructuralType>.<>o__25.<>p__7;
					if (Delta<TStructuralType>.<>o__25.<>p__6 == null)
					{
						Delta<TStructuralType>.<>o__25.<>p__6 = CallSite<Func<CallSite, Type, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "IsDeltaOfT", null, typeof(Delta<TStructuralType>), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, Type, object, object> target3 = Delta<TStructuralType>.<>o__25.<>p__6.Target;
					CallSite <>p__3 = Delta<TStructuralType>.<>o__25.<>p__6;
					Type typeFromHandle = typeof(TypedDelta);
					if (Delta<TStructuralType>.<>o__25.<>p__5 == null)
					{
						Delta<TStructuralType>.<>o__25.<>p__5 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetType", null, typeof(Delta<TStructuralType>), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
					}
					target2(<>p__2, target3(<>p__3, typeFromHandle, Delta<TStructuralType>.<>o__25.<>p__5.Target(Delta<TStructuralType>.<>o__25.<>p__5, obj)));
					if (Delta<TStructuralType>.<>o__25.<>p__8 == null)
					{
						Delta<TStructuralType>.<>o__25.<>p__8 = CallSite<Action<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "CopyChangedValues", null, typeof(Delta<TStructuralType>), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Delta<TStructuralType>.<>o__25.<>p__8.Target(Delta<TStructuralType>.<>o__25.<>p__8, obj, obj2);
				}
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006DE8 File Offset: 0x00004FE8
		public void CopyUnchangedValues(TStructuralType original)
		{
			if (original == null)
			{
				throw Error.ArgumentNull("original");
			}
			if (!this._structuredType.IsInstanceOfType(original))
			{
				throw Error.Argument("original", SRResources.DeltaTypeMismatch, new object[]
				{
					this._structuredType,
					original.GetType()
				});
			}
			foreach (PropertyAccessor<TStructuralType> propertyAccessor in from s in this.GetUnchangedPropertyNames()
				select this._allProperties[s])
			{
				propertyAccessor.Copy(this._instance, original);
			}
			this.CopyUnchangedDynamicValues(original);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006EA4 File Offset: 0x000050A4
		public void Patch(TStructuralType original)
		{
			this.CopyChangedValues(original);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006EAD File Offset: 0x000050AD
		public void Put(TStructuralType original)
		{
			this.CopyChangedValues(original);
			this.CopyUnchangedValues(original);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006EC0 File Offset: 0x000050C0
		private static void CopyDynamicPropertyDictionary(IDictionary<string, object> source, IDictionary<string, object> dest, PropertyInfo dynamicPropertyInfo, TStructuralType targetEntity)
		{
			if (source.Count == 0)
			{
				if (dest != null)
				{
					dest.Clear();
					return;
				}
			}
			else
			{
				if (dest == null)
				{
					dest = Delta<TStructuralType>.GetDynamicPropertyDictionary(dynamicPropertyInfo, targetEntity, true);
				}
				else
				{
					dest.Clear();
				}
				foreach (KeyValuePair<string, object> keyValuePair in source)
				{
					dest.Add(keyValuePair);
				}
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006F30 File Offset: 0x00005130
		private static IDictionary<string, object> GetDynamicPropertyDictionary(PropertyInfo propertyInfo, TStructuralType entity, bool create)
		{
			if (propertyInfo == null)
			{
				throw Error.ArgumentNull("propertyInfo");
			}
			if (entity == null)
			{
				throw Error.ArgumentNull("entity");
			}
			object value = propertyInfo.GetValue(entity);
			if (value != null)
			{
				return (IDictionary<string, object>)value;
			}
			if (!create)
			{
				return null;
			}
			if (!propertyInfo.CanWrite)
			{
				throw Error.InvalidOperation(SRResources.CannotSetDynamicPropertyDictionary, new object[]
				{
					propertyInfo.Name,
					entity.GetType().FullName
				});
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			propertyInfo.SetValue(entity, dictionary);
			return dictionary;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006FCC File Offset: 0x000051CC
		private static bool TryGetPropertyRef(TStructuralType structural, string propertyName, out dynamic propertyRef)
		{
			propertyRef = null;
			PropertyInfo property = structural.GetType().GetProperty(propertyName);
			if (property != null)
			{
				propertyRef = property.GetValue(structural, null);
				return true;
			}
			return false;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000700C File Offset: 0x0000520C
		private void Reset(Type structuralType)
		{
			if (structuralType == null)
			{
				throw Error.ArgumentNull("structuralType");
			}
			if (!typeof(TStructuralType).IsAssignableFrom(structuralType))
			{
				throw Error.InvalidOperation(SRResources.DeltaEntityTypeNotAssignable, new object[]
				{
					structuralType,
					typeof(TStructuralType)
				});
			}
			this._instance = Activator.CreateInstance(structuralType) as TStructuralType;
			this._changedProperties = new HashSet<string>();
			this._deltaNestedResources = new Dictionary<string, object>();
			this._structuredType = structuralType;
			this._changedDynamicProperties = new HashSet<string>();
			this._dynamicDictionaryCache = null;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000070A8 File Offset: 0x000052A8
		private void InitializeProperties(IEnumerable<string> updatableProperties)
		{
			this._allProperties = Delta<TStructuralType>._propertyCache.GetOrAdd(this._structuredType, (Type backingType) => (from p in backingType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				where (p.GetSetMethod() != null || TypeHelper.IsCollection(p.PropertyType)) && p.GetGetMethod() != null
				select new FastPropertyAccessor<TStructuralType>(p)).ToDictionary((PropertyAccessor<TStructuralType> p) => p.Property.Name));
			if (updatableProperties != null)
			{
				this._updatableProperties = new HashSet<string>(updatableProperties);
				this._updatableProperties.IntersectWith(this._allProperties.Keys);
			}
			else
			{
				this._updatableProperties = new HashSet<string>(this._allProperties.Keys);
			}
			if (this._dynamicDictionaryPropertyinfo != null)
			{
				this._updatableProperties.Remove(this._dynamicDictionaryPropertyinfo.Name);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000714C File Offset: 0x0000534C
		private void CopyChangedDynamicValues(TStructuralType targetEntity)
		{
			if (this._dynamicDictionaryPropertyinfo == null)
			{
				return;
			}
			if (this._dynamicDictionaryCache == null)
			{
				this._dynamicDictionaryCache = Delta<TStructuralType>.GetDynamicPropertyDictionary(this._dynamicDictionaryPropertyinfo, this._instance, false);
			}
			IDictionary<string, object> dynamicDictionaryCache = this._dynamicDictionaryCache;
			if (dynamicDictionaryCache == null)
			{
				return;
			}
			IDictionary<string, object> dynamicPropertyDictionary = Delta<TStructuralType>.GetDynamicPropertyDictionary(this._dynamicDictionaryPropertyinfo, targetEntity, false);
			IDictionary<string, object> dictionary = ((dynamicPropertyDictionary != null) ? new Dictionary<string, object>(dynamicPropertyDictionary) : new Dictionary<string, object>());
			foreach (string text in this._changedDynamicProperties)
			{
				object obj = dynamicDictionaryCache[text];
				if (obj == null)
				{
					dictionary.Remove(text);
				}
				else
				{
					dictionary[text] = obj;
				}
			}
			Delta<TStructuralType>.CopyDynamicPropertyDictionary(dictionary, dynamicPropertyDictionary, this._dynamicDictionaryPropertyinfo, targetEntity);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007224 File Offset: 0x00005424
		private void CopyUnchangedDynamicValues(TStructuralType targetEntity)
		{
			if (this._dynamicDictionaryPropertyinfo == null)
			{
				return;
			}
			if (this._dynamicDictionaryCache == null)
			{
				this._dynamicDictionaryCache = Delta<TStructuralType>.GetDynamicPropertyDictionary(this._dynamicDictionaryPropertyinfo, this._instance, false);
			}
			IDictionary<string, object> dynamicPropertyDictionary = Delta<TStructuralType>.GetDynamicPropertyDictionary(this._dynamicDictionaryPropertyinfo, targetEntity, false);
			if (this._dynamicDictionaryCache == null)
			{
				if (dynamicPropertyDictionary != null)
				{
					dynamicPropertyDictionary.Clear();
					return;
				}
			}
			else
			{
				IDictionary<string, object> dictionary = ((dynamicPropertyDictionary != null) ? new Dictionary<string, object>(dynamicPropertyDictionary) : new Dictionary<string, object>());
				foreach (string text in dictionary.Keys.Except(this._changedDynamicProperties).ToList<string>())
				{
					dictionary.Remove(text);
				}
				Delta<TStructuralType>.CopyDynamicPropertyDictionary(dictionary, dynamicPropertyDictionary, this._dynamicDictionaryPropertyinfo, targetEntity);
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000072F8 File Offset: 0x000054F8
		private bool TrySetPropertyValueInternal(string name, object value)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			if (this._dynamicDictionaryPropertyinfo != null && (name == this._dynamicDictionaryPropertyinfo.Name || !this._allProperties.ContainsKey(name)))
			{
				if (this._dynamicDictionaryCache == null)
				{
					this._dynamicDictionaryCache = Delta<TStructuralType>.GetDynamicPropertyDictionary(this._dynamicDictionaryPropertyinfo, this._instance, true);
				}
				this._dynamicDictionaryCache[name] = value;
				this._changedDynamicProperties.Add(name);
				return true;
			}
			if (!this._updatableProperties.Contains(name))
			{
				return false;
			}
			PropertyAccessor<TStructuralType> propertyAccessor = this._allProperties[name];
			if (value == null && !EdmLibHelpers.IsNullable(propertyAccessor.Property.PropertyType))
			{
				return false;
			}
			Type propertyType = propertyAccessor.Property.PropertyType;
			if (value != null && !TypeHelper.IsCollection(propertyType) && !propertyType.IsAssignableFrom(value.GetType()))
			{
				return false;
			}
			propertyAccessor.SetValue(this._instance, value);
			this._changedProperties.Add(name);
			return true;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000073F4 File Offset: 0x000055F4
		private bool TrySetNestedResourceInternal(string name, object deltaNestedResource)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			if (!this._updatableProperties.Contains(name))
			{
				return false;
			}
			if (this._deltaNestedResources.ContainsKey(name))
			{
				return false;
			}
			this._deltaNestedResources[name] = deltaNestedResource;
			return true;
		}

		// Token: 0x0400005A RID: 90
		private static ConcurrentDictionary<Type, Dictionary<string, PropertyAccessor<TStructuralType>>> _propertyCache = new ConcurrentDictionary<Type, Dictionary<string, PropertyAccessor<TStructuralType>>>();

		// Token: 0x0400005B RID: 91
		private Dictionary<string, PropertyAccessor<TStructuralType>> _allProperties;

		// Token: 0x0400005C RID: 92
		private HashSet<string> _updatableProperties;

		// Token: 0x0400005D RID: 93
		private HashSet<string> _changedProperties;

		// Token: 0x0400005E RID: 94
		private IDictionary<string, object> _deltaNestedResources;

		// Token: 0x0400005F RID: 95
		private TStructuralType _instance;

		// Token: 0x04000060 RID: 96
		private Type _structuredType;

		// Token: 0x04000061 RID: 97
		private PropertyInfo _dynamicDictionaryPropertyinfo;

		// Token: 0x04000062 RID: 98
		private HashSet<string> _changedDynamicProperties;

		// Token: 0x04000063 RID: 99
		private IDictionary<string, object> _dynamicDictionaryCache;
	}
}
