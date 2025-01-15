using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Http.Properties;
using System.Web.Http.Routing.Constraints;

namespace System.Web.Http.Routing
{
	// Token: 0x02000153 RID: 339
	public class DefaultInlineConstraintResolver : IInlineConstraintResolver
	{
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x000172FB File Offset: 0x000154FB
		public IDictionary<string, Type> ConstraintMap
		{
			get
			{
				return this._inlineConstraintMap;
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00017304 File Offset: 0x00015504
		private static IDictionary<string, Type> GetDefaultConstraintMap()
		{
			return new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
			{
				{
					"bool",
					typeof(BoolRouteConstraint)
				},
				{
					"datetime",
					typeof(DateTimeRouteConstraint)
				},
				{
					"decimal",
					typeof(DecimalRouteConstraint)
				},
				{
					"double",
					typeof(DoubleRouteConstraint)
				},
				{
					"float",
					typeof(FloatRouteConstraint)
				},
				{
					"guid",
					typeof(GuidRouteConstraint)
				},
				{
					"int",
					typeof(IntRouteConstraint)
				},
				{
					"long",
					typeof(LongRouteConstraint)
				},
				{
					"minlength",
					typeof(MinLengthRouteConstraint)
				},
				{
					"maxlength",
					typeof(MaxLengthRouteConstraint)
				},
				{
					"length",
					typeof(LengthRouteConstraint)
				},
				{
					"min",
					typeof(MinRouteConstraint)
				},
				{
					"max",
					typeof(MaxRouteConstraint)
				},
				{
					"range",
					typeof(RangeRouteConstraint)
				},
				{
					"alpha",
					typeof(AlphaRouteConstraint)
				},
				{
					"regex",
					typeof(RegexRouteConstraint)
				}
			};
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001746C File Offset: 0x0001566C
		public virtual IHttpRouteConstraint ResolveConstraint(string inlineConstraint)
		{
			if (inlineConstraint == null)
			{
				throw Error.ArgumentNull("inlineConstraint");
			}
			int num = inlineConstraint.IndexOf('(');
			string text;
			string text2;
			if (num >= 0 && inlineConstraint.EndsWith(")", StringComparison.Ordinal))
			{
				text = inlineConstraint.Substring(0, num);
				text2 = inlineConstraint.Substring(num + 1, inlineConstraint.Length - num - 2);
			}
			else
			{
				text = inlineConstraint;
				text2 = null;
			}
			Type type;
			if (!this._inlineConstraintMap.TryGetValue(text, out type))
			{
				return null;
			}
			if (!typeof(IHttpRouteConstraint).IsAssignableFrom(type))
			{
				throw Error.InvalidOperation(SRResources.DefaultInlineConstraintResolver_TypeNotConstraint, new object[] { type.Name, text });
			}
			return (IHttpRouteConstraint)DefaultInlineConstraintResolver.CreateConstraint(type, text2);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00017514 File Offset: 0x00015714
		private static object CreateConstraint(Type constraintType, string argumentString)
		{
			if (argumentString == null)
			{
				return Activator.CreateInstance(constraintType);
			}
			ConstructorInfo[] constructors = constraintType.GetConstructors();
			ConstructorInfo constructorInfo;
			object[] array;
			if (constructors.Length == 1 && constructors[0].GetParameters().Length == 1)
			{
				constructorInfo = constructors[0];
				array = DefaultInlineConstraintResolver.ConvertArguments(constructorInfo.GetParameters(), new string[] { argumentString });
			}
			else
			{
				string[] arguments = (from argument in argumentString.Split(new char[] { ',' })
					select argument.Trim()).ToArray<string>();
				ConstructorInfo[] array2 = constructors.Where((ConstructorInfo ci) => ci.GetParameters().Length == arguments.Length).ToArray<ConstructorInfo>();
				int num = array2.Length;
				if (num == 0)
				{
					throw Error.InvalidOperation(SRResources.DefaultInlineConstraintResolver_CouldNotFindCtor, new object[] { constraintType.Name, argumentString.Length });
				}
				if (num != 1)
				{
					throw Error.InvalidOperation(SRResources.DefaultInlineConstraintResolver_AmbiguousCtors, new object[] { constraintType.Name, argumentString.Length });
				}
				constructorInfo = array2[0];
				array = DefaultInlineConstraintResolver.ConvertArguments(constructorInfo.GetParameters(), arguments);
			}
			return constructorInfo.Invoke(array);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00017648 File Offset: 0x00015848
		private static object[] ConvertArguments(ParameterInfo[] parameterInfos, string[] arguments)
		{
			object[] array = new object[parameterInfos.Length];
			for (int i = 0; i < parameterInfos.Length; i++)
			{
				Type parameterType = parameterInfos[i].ParameterType;
				array[i] = Convert.ChangeType(arguments[i], parameterType, CultureInfo.InvariantCulture);
			}
			return array;
		}

		// Token: 0x0400027B RID: 635
		private readonly IDictionary<string, Type> _inlineConstraintMap = DefaultInlineConstraintResolver.GetDefaultConstraintMap();
	}
}
