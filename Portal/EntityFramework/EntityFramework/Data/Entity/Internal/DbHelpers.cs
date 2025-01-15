using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000F6 RID: 246
	internal static class DbHelpers
	{
		// Token: 0x06001225 RID: 4645 RVA: 0x0002F1EC File Offset: 0x0002D3EC
		public static bool KeyValuesEqual(object x, object y)
		{
			if (x is DBNull)
			{
				x = null;
			}
			if (y is DBNull)
			{
				y = null;
			}
			if (object.Equals(x, y))
			{
				return true;
			}
			byte[] array = x as byte[];
			byte[] array2 = y as byte[];
			if (array == null || array2 == null || array.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0002F254 File Offset: 0x0002D454
		public static bool PropertyValuesEqual(object x, object y)
		{
			if (x is DBNull)
			{
				x = null;
			}
			if (y is DBNull)
			{
				y = null;
			}
			if (x == null)
			{
				return y == null;
			}
			if (x.GetType().IsValueType() && object.Equals(x, y))
			{
				return true;
			}
			string text = x as string;
			if (text != null)
			{
				return text.Equals(y as string, StringComparison.Ordinal);
			}
			byte[] array = x as byte[];
			if (array == null)
			{
				return x == y;
			}
			byte[] array2 = y as byte[];
			if (array2 == null || array.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0002F2EB File Offset: 0x0002D4EB
		public static string QuoteIdentifier(string identifier)
		{
			return "[" + identifier.Replace("]", "]]") + "]";
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0002F30C File Offset: 0x0002D50C
		public static bool TreatAsConnectionString(string nameOrConnectionString)
		{
			return nameOrConnectionString.IndexOf('=') >= 0;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0002F31C File Offset: 0x0002D51C
		public static bool TryGetConnectionName(string nameOrConnectionString, out string name)
		{
			int num = nameOrConnectionString.IndexOf('=');
			if (num < 0)
			{
				name = nameOrConnectionString;
				return true;
			}
			if (nameOrConnectionString.IndexOf('=', num + 1) >= 0)
			{
				name = null;
				return false;
			}
			if (nameOrConnectionString.Substring(0, num).Trim().Equals("name", StringComparison.OrdinalIgnoreCase))
			{
				name = nameOrConnectionString.Substring(num + 1).Trim();
				return true;
			}
			name = null;
			return false;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0002F380 File Offset: 0x0002D580
		public static bool IsFullEFConnectionString(string nameOrConnectionString)
		{
			IEnumerable<string> enumerable = from t in nameOrConnectionString.ToUpperInvariant().Split(new char[] { '=', ';' })
				select t.Trim();
			return enumerable.Contains("PROVIDER") && enumerable.Contains("PROVIDER CONNECTION STRING") && enumerable.Contains("METADATA");
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0002F3F4 File Offset: 0x0002D5F4
		public static string ParsePropertySelector<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property, string methodName, string paramName)
		{
			string text;
			if (!DbHelpers.TryParsePath(property.Body, out text) || text == null)
			{
				throw new ArgumentException(Strings.DbEntityEntry_BadPropertyExpression(methodName, typeof(TEntity).Name), paramName);
			}
			return text;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0002F430 File Offset: 0x0002D630
		public static bool TryParsePath(Expression expression, out string path)
		{
			path = null;
			Expression expression2 = expression.RemoveConvert();
			MemberExpression memberExpression = expression2 as MemberExpression;
			MethodCallExpression methodCallExpression = expression2 as MethodCallExpression;
			if (memberExpression != null)
			{
				string name = memberExpression.Member.Name;
				string text;
				if (!DbHelpers.TryParsePath(memberExpression.Expression, out text))
				{
					return false;
				}
				path = ((text == null) ? name : (text + "." + name));
			}
			else if (methodCallExpression != null)
			{
				if (methodCallExpression.Method.Name == "Select" && methodCallExpression.Arguments.Count == 2)
				{
					string text2;
					if (!DbHelpers.TryParsePath(methodCallExpression.Arguments[0], out text2))
					{
						return false;
					}
					if (text2 != null)
					{
						LambdaExpression lambdaExpression = methodCallExpression.Arguments[1] as LambdaExpression;
						if (lambdaExpression != null)
						{
							string text3;
							if (!DbHelpers.TryParsePath(lambdaExpression.Body, out text3))
							{
								return false;
							}
							if (text3 != null)
							{
								path = text2 + "." + text3;
								return true;
							}
						}
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0002F514 File Offset: 0x0002D714
		public static IDictionary<string, Type> GetPropertyTypes(Type type)
		{
			IDictionary<string, Type> dictionary;
			if (!DbHelpers._propertyTypes.TryGetValue(type, out dictionary))
			{
				IEnumerable<PropertyInfo> enumerable = from p in type.GetInstanceProperties()
					where p.GetIndexParameters().Length == 0
					select p;
				dictionary = new Dictionary<string, Type>(enumerable.Count<PropertyInfo>());
				foreach (PropertyInfo propertyInfo in enumerable)
				{
					dictionary[propertyInfo.Name] = propertyInfo.PropertyType;
				}
				DbHelpers._propertyTypes.TryAdd(type, dictionary);
			}
			return dictionary;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0002F5BC File Offset: 0x0002D7BC
		public static IDictionary<string, Action<object, object>> GetPropertySetters(Type type)
		{
			IDictionary<string, Action<object, object>> dictionary;
			if (!DbHelpers._propertySetters.TryGetValue(type, out dictionary))
			{
				IEnumerable<PropertyInfo> enumerable = from p in type.GetInstanceProperties()
					where p.GetIndexParameters().Length == 0
					select p;
				dictionary = new Dictionary<string, Action<object, object>>(enumerable.Count<PropertyInfo>());
				foreach (PropertyInfo propertyInfo in enumerable.Select((PropertyInfo p) => p.GetPropertyInfoForSet()))
				{
					MethodInfo methodInfo = propertyInfo.Setter();
					if (methodInfo != null)
					{
						ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "value");
						ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "instance");
						MethodCallExpression methodCallExpression = Expression.Call(Expression.Convert(parameterExpression2, type), methodInfo, new Expression[] { Expression.Convert(parameterExpression, propertyInfo.PropertyType) });
						Action<object, object> setter = Expression.Lambda<Action<object, object>>(methodCallExpression, new ParameterExpression[] { parameterExpression2, parameterExpression }).Compile();
						MethodInfo methodInfo2 = DbHelpers.ConvertAndSetMethod.MakeGenericMethod(new Type[] { propertyInfo.PropertyType });
						Action<object, object, Action<object, object>, string, string> convertAndSet = (Action<object, object, Action<object, object>, string, string>)Delegate.CreateDelegate(typeof(Action<object, object, Action<object, object>, string, string>), methodInfo2);
						string propertyName = propertyInfo.Name;
						dictionary[propertyInfo.Name] = delegate(object i, object v)
						{
							convertAndSet(i, v, setter, propertyName, type.Name);
						};
					}
				}
				DbHelpers._propertySetters.TryAdd(type, dictionary);
			}
			return dictionary;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0002F7A8 File Offset: 0x0002D9A8
		private static void ConvertAndSet<T>(object instance, object value, Action<object, object> setter, string propertyName, string typeName)
		{
			if (value == null && typeof(T).IsValueType() && Nullable.GetUnderlyingType(typeof(T)) == null)
			{
				throw Error.DbPropertyValues_CannotSetNullValue(propertyName, typeof(T).Name, typeName);
			}
			setter(instance, (T)((object)value));
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0002F80C File Offset: 0x0002DA0C
		public static IDictionary<string, Func<object, object>> GetPropertyGetters(Type type)
		{
			IDictionary<string, Func<object, object>> dictionary;
			if (!DbHelpers._propertyGetters.TryGetValue(type, out dictionary))
			{
				IEnumerable<PropertyInfo> enumerable = from p in type.GetInstanceProperties()
					where p.GetIndexParameters().Length == 0
					select p;
				dictionary = new Dictionary<string, Func<object, object>>(enumerable.Count<PropertyInfo>());
				foreach (PropertyInfo propertyInfo in enumerable)
				{
					MethodInfo methodInfo = propertyInfo.Getter();
					if (methodInfo != null)
					{
						ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "instance");
						UnaryExpression unaryExpression = Expression.Convert(Expression.Call(Expression.Convert(parameterExpression, type), methodInfo), typeof(object));
						dictionary[propertyInfo.Name] = Expression.Lambda<Func<object, object>>(unaryExpression, new ParameterExpression[] { parameterExpression }).Compile();
					}
				}
				DbHelpers._propertyGetters.TryAdd(type, dictionary);
			}
			return dictionary;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0002F90C File Offset: 0x0002DB0C
		public static IQueryable CreateNoTrackingQuery(ObjectQuery query)
		{
			ObjectQuery objectQuery = (ObjectQuery)((IQueryable)query).Provider.CreateQuery(((IQueryable)query).Expression);
			objectQuery.ExecutionStrategy = query.ExecutionStrategy;
			objectQuery.MergeOption = MergeOption.NoTracking;
			objectQuery.Streaming = query.Streaming;
			return objectQuery;
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0002F950 File Offset: 0x0002DB50
		public static IQueryable CreateStreamingQuery(ObjectQuery query)
		{
			ObjectQuery objectQuery = (ObjectQuery)((IQueryable)query).Provider.CreateQuery(((IQueryable)query).Expression);
			objectQuery.ExecutionStrategy = query.ExecutionStrategy;
			objectQuery.Streaming = true;
			objectQuery.MergeOption = query.MergeOption;
			return objectQuery;
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0002F994 File Offset: 0x0002DB94
		public static IQueryable CreateQueryWithExecutionStrategy(ObjectQuery query, IDbExecutionStrategy executionStrategy)
		{
			ObjectQuery objectQuery = (ObjectQuery)((IQueryable)query).Provider.CreateQuery(((IQueryable)query).Expression);
			objectQuery.ExecutionStrategy = executionStrategy;
			objectQuery.MergeOption = query.MergeOption;
			objectQuery.Streaming = query.Streaming;
			return objectQuery;
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0002F9D8 File Offset: 0x0002DBD8
		public static IEnumerable<DbValidationError> SplitValidationResults(string propertyName, IEnumerable<ValidationResult> validationResults)
		{
			foreach (ValidationResult validationResult in validationResults)
			{
				if (validationResult != null)
				{
					IEnumerable<string> enumerable;
					if (validationResult.MemberNames != null && validationResult.MemberNames.Any<string>())
					{
						enumerable = validationResult.MemberNames;
					}
					else
					{
						IEnumerable<string> enumerable2 = new string[1];
						enumerable = enumerable2;
					}
					IEnumerable<string> enumerable3 = enumerable;
					foreach (string text in enumerable3)
					{
						yield return new DbValidationError(text ?? propertyName, validationResult.ErrorMessage);
					}
					IEnumerator<string> enumerator2 = null;
					validationResult = null;
				}
			}
			IEnumerator<ValidationResult> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0002F9EF File Offset: 0x0002DBEF
		public static string GetPropertyPath(InternalMemberEntry property)
		{
			return string.Join(".", DbHelpers.GetPropertyPathSegments(property).Reverse<string>());
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0002FA06 File Offset: 0x0002DC06
		private static IEnumerable<string> GetPropertyPathSegments(InternalMemberEntry property)
		{
			do
			{
				yield return property.Name;
				property = ((property is InternalNestedPropertyEntry) ? ((InternalNestedPropertyEntry)property).ParentPropertyEntry : null);
			}
			while (property != null);
			yield break;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0002FA16 File Offset: 0x0002DC16
		public static Type CollectionType(Type elementType)
		{
			return DbHelpers._collectionTypes.GetOrAdd(elementType, (Type t) => typeof(ICollection<>).MakeGenericType(new Type[] { t }));
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0002FA42 File Offset: 0x0002DC42
		public static string DatabaseName(this Type contextType)
		{
			return contextType.ToString();
		}

		// Token: 0x0400090A RID: 2314
		public static readonly MethodInfo ConvertAndSetMethod = typeof(DbHelpers).GetOnlyDeclaredMethod("ConvertAndSet");

		// Token: 0x0400090B RID: 2315
		private static readonly ConcurrentDictionary<Type, IDictionary<string, Type>> _propertyTypes = new ConcurrentDictionary<Type, IDictionary<string, Type>>();

		// Token: 0x0400090C RID: 2316
		private static readonly ConcurrentDictionary<Type, IDictionary<string, Action<object, object>>> _propertySetters = new ConcurrentDictionary<Type, IDictionary<string, Action<object, object>>>();

		// Token: 0x0400090D RID: 2317
		private static readonly ConcurrentDictionary<Type, IDictionary<string, Func<object, object>>> _propertyGetters = new ConcurrentDictionary<Type, IDictionary<string, Func<object, object>>>();

		// Token: 0x0400090E RID: 2318
		private static readonly ConcurrentDictionary<Type, Type> _collectionTypes = new ConcurrentDictionary<Type, Type>();
	}
}
