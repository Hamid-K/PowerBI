using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002BC RID: 700
	internal sealed class FunctionSystem : FunctionMultiArgument
	{
		// Token: 0x06001596 RID: 5526 RVA: 0x000320B8 File Offset: 0x000302B8
		internal FunctionSystem(IReportLink container, Type classType, string functionName, IInternalExpression[] args, Dictionary<string, List<MemberInfo>> functionsMap)
			: this(container, classType, functionName, args, null, functionsMap)
		{
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x000320C8 File Offset: 0x000302C8
		internal FunctionSystem(IReportLink container, Type classType, string functionName, IInternalExpression[] args, IInternalExpression owningObject, Dictionary<string, List<MemberInfo>> functionsMap)
			: base(args)
		{
			this._ClassType = classType;
			this._FunctionName = functionName;
			FunctionSystem._FunctionsMap = functionsMap;
			this._ReturnTypeCode = global::System.TypeCode.Boolean;
			base.IsArray = false;
			this._ObjectExpression = owningObject;
			this._ArgTypes = new Type[base.Arguments.Length];
			for (int i = 0; i < this._ArgTypes.Length; i++)
			{
				if (base.Arguments[i].IsArray && base.Arguments[i].TypeCode() != global::System.TypeCode.Object)
				{
					this._ArgTypes[i] = Type.GetType("System." + base.Arguments[i].TypeCode().ToString() + "[]");
				}
				else
				{
					this._ArgTypes[i] = Type.GetType("System." + base.Arguments[i].TypeCode().ToString());
				}
			}
			List<MemberInfo> list;
			if (this._ObjectExpression != null)
			{
				list = this.SearchMembers(this._ObjectExpression.TypeCode(), this._FunctionName);
			}
			else
			{
				list = this.SearchMembers(classType, this._FunctionName);
			}
			if (list.Count == 0)
			{
				throw new NotSupportedException("Method " + this._FunctionName + " is not supported.");
			}
			if (!this.DesignTimeValidate(list))
			{
				throw new ArgumentException(this.ParamDescription());
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x00032226 File Offset: 0x00030426
		// (set) Token: 0x06001599 RID: 5529 RVA: 0x0003222E File Offset: 0x0003042E
		public Type ClassType
		{
			get
			{
				return this._ClassType;
			}
			internal set
			{
				this._ClassType = value;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x00032237 File Offset: 0x00030437
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x0003223F File Offset: 0x0003043F
		public string Func
		{
			get
			{
				return this._FunctionName;
			}
			internal set
			{
				this._FunctionName = value;
			}
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00032248 File Offset: 0x00030448
		public override TypeCode TypeCode()
		{
			return this._ReturnTypeCode;
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00032250 File Offset: 0x00030450
		public override string WriteSource(NameChanges nameChanges)
		{
			string text = "";
			if (this._ObjectExpression != null)
			{
				text = text + this._ObjectExpression.WriteSource(nameChanges) + ".";
			}
			text += ((this._ClassType == null) ? string.Empty : (this._ClassType.FullName + "."));
			string[] array = new string[base.Arguments.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = base.Arguments[i].WriteSource(nameChanges);
			}
			text += this._MemberInfo.Name;
			if (this._MemberInfo.MemberType == MemberTypes.Method || array.Length != 0)
			{
				text = text + "(" + string.Join(",", array) + ")";
			}
			return text;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00032324 File Offset: 0x00030524
		public override object Evaluate()
		{
			if (this._ObjectExpression != null)
			{
				this._CallObject = this._ObjectExpression.Evaluate();
			}
			if (this._MemberInfo is FieldInfo)
			{
				return ((FieldInfo)this._MemberInfo).GetValue(this._CallObject);
			}
			if (this._MemberInfo is PropertyInfo)
			{
				return ((PropertyInfo)this._MemberInfo).GetValue(this._CallObject, null);
			}
			if (!(this._MemberInfo is MethodInfo))
			{
				return "";
			}
			if (this._ObjectExpression == null && Attribute.IsDefined(this._ParameterInfos[this._ParameterInfos.Length - 1], typeof(ParamArrayAttribute)))
			{
				return this.InvokeWithParamArray();
			}
			if (this._ParameterInfos.Length > base.Arguments.Length)
			{
				return this.InvokeWithOptionalParam();
			}
			return this.InvokeWithRegularParam();
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x000323F8 File Offset: 0x000305F8
		private List<MemberInfo> SearchMembers(Type type, string memberName)
		{
			List<MemberInfo> list = new List<MemberInfo>();
			if (type == null)
			{
				if (!FunctionSystem._FunctionsMap.ContainsKey(memberName))
				{
					throw new NotSupportedException("Method " + this._FunctionName + " is not supported.");
				}
				list = FunctionSystem._FunctionsMap[memberName];
			}
			else
			{
				foreach (MemberInfo memberInfo in (type.Name == "Code") ? type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public) : type.GetMembers(BindingFlags.Static | BindingFlags.Public))
				{
					if (string.Equals(memberName, memberInfo.Name, StringComparison.OrdinalIgnoreCase))
					{
						list.Add(memberInfo);
					}
				}
			}
			return list;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00032498 File Offset: 0x00030698
		private List<MemberInfo> SearchMembers(TypeCode typeCode, string memberName)
		{
			List<MemberInfo> list = new List<MemberInfo>();
			foreach (Type type in Assembly.GetAssembly(typeof(string)).GetTypes())
			{
				if (type.Name == this.GetObjectName(typeCode))
				{
					foreach (MemberInfo memberInfo in type.GetMembers())
					{
						if (string.Equals(memberName, memberInfo.Name, StringComparison.OrdinalIgnoreCase))
						{
							list.Add(memberInfo);
						}
					}
					return list;
				}
			}
			return list;
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00032523 File Offset: 0x00030723
		private bool MatchMethod(MethodInfo methodInfo)
		{
			this._MemberInfo = methodInfo;
			this._ReturnTypeCode = Type.GetTypeCode(methodInfo.ReturnType);
			base.IsArray = methodInfo.ReturnType.IsArray;
			return this.ArgumentValidation(methodInfo, false) || this.ArgumentValidation(methodInfo, true);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00032562 File Offset: 0x00030762
		private bool MatchProperty(PropertyInfo propertyInfo)
		{
			this._MemberInfo = propertyInfo;
			this._ReturnTypeCode = Type.GetTypeCode(propertyInfo.PropertyType);
			base.IsArray = propertyInfo.PropertyType.IsArray;
			if (this._ArgTypes.Length != 0)
			{
				throw new ArgumentException();
			}
			return true;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0003259D File Offset: 0x0003079D
		private bool MatchField(FieldInfo fieldInfo)
		{
			this._MemberInfo = fieldInfo;
			this._ReturnTypeCode = Type.GetTypeCode(fieldInfo.DeclaringType);
			base.IsArray = fieldInfo.DeclaringType.IsArray;
			if (this._ArgTypes.Length != 0)
			{
				throw new ArgumentException();
			}
			return true;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x000325D8 File Offset: 0x000307D8
		private bool DesignTimeValidate(List<MemberInfo> members)
		{
			foreach (MemberInfo memberInfo in members)
			{
				bool flag = memberInfo is PropertyInfo && this.MatchProperty(memberInfo as PropertyInfo);
				bool flag2 = memberInfo is MethodInfo && this.MatchMethod(memberInfo as MethodInfo);
				bool flag3 = memberInfo is FieldInfo && this.MatchField(memberInfo as FieldInfo);
				if (flag || flag2 || flag3)
				{
					return true;
				}
			}
			if (members[0] is MethodInfo)
			{
				this.MatchMethod(members[0] as MethodInfo);
			}
			if (members[0] is PropertyInfo)
			{
				this.MatchProperty(members[0] as PropertyInfo);
			}
			if (members[0] is FieldInfo)
			{
				this.MatchField(members[0] as FieldInfo);
			}
			return false;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x000326D8 File Offset: 0x000308D8
		private void DesignTimeValidate()
		{
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x000326DC File Offset: 0x000308DC
		private string ParamDescription()
		{
			string[] array = new string[this._ParameterInfos.Length];
			for (int i = 0; i < this._ParameterInfos.Length; i++)
			{
				array[i] = "ByVal ";
				if (Attribute.IsDefined(this._ParameterInfos[i], typeof(ParamArrayAttribute)))
				{
					array[i] = "ParamArray ";
				}
				else if (this._ParameterInfos[i].IsOptional)
				{
					array[i] = "Optional ";
				}
				string[] array2 = array;
				int num = i;
				array2[num] = array2[num] + this._ParameterInfos[i].Name + " As " + this._ParameterInfos[i].ParameterType.Name;
			}
			return string.Concat(new string[]
			{
				"function ",
				this._FunctionName,
				"(",
				string.Join(",", array),
				")"
			});
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x000327BC File Offset: 0x000309BC
		private bool ArgumentValidation(MethodInfo methodInfo, bool allowTypeConvert)
		{
			this._ParameterInfos = methodInfo.GetParameters();
			if (this._ParameterInfos.Length == 0 && base.Arguments.Length != 0)
			{
				return false;
			}
			if ((this._ParameterInfos.Length < base.Arguments.Length && !Attribute.IsDefined(this._ParameterInfos[this._ParameterInfos.Length - 1], typeof(ParamArrayAttribute))) || (this._ParameterInfos.Length > base.Arguments.Length && !this._ParameterInfos[base.Arguments.Length].IsOptional))
			{
				return false;
			}
			for (int i = 0; i < base.Arguments.Length; i++)
			{
				if ((!(this._ParameterInfos[i].ParameterType == typeof(bool)) || !(this._ArgTypes[i] == typeof(int))) && !(this._ParameterInfos[i].ParameterType == typeof(object)) && (!(this._ParameterInfos[i].ParameterType == typeof(object[])) || !this._ArgTypes[i].IsArray) && this._ParameterInfos[i].ParameterType != this._ArgTypes[i])
				{
					if (Attribute.IsDefined(this._ParameterInfos[i], typeof(ParamArrayAttribute)) && (this._ParameterInfos[i].ParameterType.GetElementType() == typeof(object) || this._ParameterInfos[i].ParameterType.GetElementType() == this._ArgTypes[i]))
					{
						break;
					}
					try
					{
						object obj = base.Arguments[i].Evaluate();
						if (this._ParameterInfos[i].Attributes == ParameterAttributes.Out || this._ParameterInfos[i].Attributes == ParameterAttributes.In)
						{
							if (!allowTypeConvert)
							{
								return obj == null || obj.GetType() == this._ParameterInfos[i].ParameterType.GetElementType();
							}
							if (!(obj is IConvertible))
							{
								return false;
							}
							Convert.ChangeType(obj, this._ParameterInfos[i].ParameterType.GetElementType(), CultureInfo.CurrentCulture);
						}
						else
						{
							if (!allowTypeConvert)
							{
								return obj == null || obj.GetType() == this._ParameterInfos[i].ParameterType;
							}
							if (!(obj is IConvertible))
							{
								return false;
							}
							Convert.ChangeType(obj, this._ParameterInfos[i].ParameterType, CultureInfo.CurrentCulture);
							if (RDLUtil.IsNarrowingConversion(obj.GetType(), this._ParameterInfos[i].ParameterType))
							{
								return false;
							}
						}
					}
					catch
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x00032A7C File Offset: 0x00030C7C
		private object[] PrepareArgumentType()
		{
			object[] array = new object[base.Arguments.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = base.Arguments[i].Evaluate();
				if (this._ParameterInfos[i].ParameterType.IsArray && !array[i].GetType().IsArray)
				{
					Array array2 = Array.CreateInstance(array[i].GetType(), 1);
					array2.SetValue(array[i], 0);
					array[i] = array2;
				}
				if (array[i] != null)
				{
					array[i] = Convert.ChangeType(array[i], this._ParameterInfos[i].ParameterType, CultureInfo.CurrentCulture);
				}
			}
			return array;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00032B18 File Offset: 0x00030D18
		private object InvokeWithRegularParam()
		{
			object[] array = this.PrepareArgumentType();
			object obj = null;
			try
			{
				obj = ((MethodInfo)this._MemberInfo).Invoke(this._CallObject, array);
			}
			catch
			{
				throw new ExpressionParserException("HELP");
			}
			return obj;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00032B68 File Offset: 0x00030D68
		private object InvokeWithParamArray()
		{
			object[] array = new object[this._ParameterInfos.Length];
			object[] array2 = new object[base.Arguments.Length - this._ParameterInfos.Length + 1];
			Type elementType = this._ParameterInfos[this._ParameterInfos.Length - 1].ParameterType.GetElementType();
			for (int i = 0; i < base.Arguments.Length; i++)
			{
				if (i < this._ParameterInfos.Length - 1)
				{
					array[i] = base.Arguments[i].Evaluate();
					array[i] = Convert.ChangeType(array[i], this._ParameterInfos[i].ParameterType, CultureInfo.CurrentCulture);
				}
				else
				{
					array2[i - this._ParameterInfos.Length + 1] = base.Arguments[i].Evaluate();
					array2[i - this._ParameterInfos.Length + 1] = Convert.ChangeType(array2[i - this._ParameterInfos.Length + 1], elementType, CultureInfo.CurrentCulture);
				}
			}
			array[this._ParameterInfos.Length - 1] = array2;
			object obj = null;
			try
			{
				obj = ((MethodInfo)this._MemberInfo).Invoke(this._CallObject, array);
			}
			catch
			{
				throw new ExpressionParserException("HELP");
			}
			return obj;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00032CA4 File Offset: 0x00030EA4
		private object InvokeWithOptionalParam()
		{
			object[] array = new object[this._ParameterInfos.Length];
			for (int i = 0; i < base.Arguments.Length; i++)
			{
				array[i] = base.Arguments[i].Evaluate();
				array[i] = Convert.ChangeType(array[i], this._ParameterInfos[i].ParameterType, CultureInfo.CurrentCulture);
			}
			for (int j = base.Arguments.Length; j < array.Length; j++)
			{
				array[j] = this._ParameterInfos[j].DefaultValue;
			}
			object obj = null;
			try
			{
				obj = ((MethodInfo)this._MemberInfo).Invoke(this._CallObject, array);
			}
			catch
			{
				throw new ExpressionParserException("HELP");
			}
			return obj;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00032D60 File Offset: 0x00030F60
		private string GetObjectName(TypeCode typeCode)
		{
			FunctionSystem functionSystem = this._ObjectExpression as FunctionSystem;
			if (functionSystem != null && functionSystem.IsArray)
			{
				return "Array";
			}
			switch (typeCode)
			{
			case global::System.TypeCode.Empty:
				return "Empty";
			case global::System.TypeCode.Object:
				return "Object";
			case global::System.TypeCode.DBNull:
				return "DBNull";
			case global::System.TypeCode.Boolean:
				return "Boolean";
			case global::System.TypeCode.Char:
				return "Char";
			case global::System.TypeCode.SByte:
				return "SByte";
			case global::System.TypeCode.Byte:
				return "Byte";
			case global::System.TypeCode.Int16:
				return "Int16";
			case global::System.TypeCode.UInt16:
				return "UInt16";
			case global::System.TypeCode.Int32:
				return "Int32";
			case global::System.TypeCode.UInt32:
				return "UInt32";
			case global::System.TypeCode.Int64:
				return "Int64";
			case global::System.TypeCode.UInt64:
				return "UInt64";
			case global::System.TypeCode.Single:
				return "Single";
			case global::System.TypeCode.Double:
				return "Double";
			case global::System.TypeCode.Decimal:
				return "Decimal";
			case global::System.TypeCode.DateTime:
				return "DateTime";
			case global::System.TypeCode.String:
				return "String";
			}
			return "String";
		}

		// Token: 0x040006D6 RID: 1750
		private Type _ClassType;

		// Token: 0x040006D7 RID: 1751
		private static Dictionary<string, List<MemberInfo>> _FunctionsMap = new Dictionary<string, List<MemberInfo>>(StringUtil.CaseInsensitiveComparer);

		// Token: 0x040006D8 RID: 1752
		private string _FunctionName;

		// Token: 0x040006D9 RID: 1753
		private TypeCode _ReturnTypeCode;

		// Token: 0x040006DA RID: 1754
		private readonly Type[] _ArgTypes;

		// Token: 0x040006DB RID: 1755
		private MemberInfo _MemberInfo;

		// Token: 0x040006DC RID: 1756
		private ParameterInfo[] _ParameterInfos;

		// Token: 0x040006DD RID: 1757
		private readonly IInternalExpression _ObjectExpression;

		// Token: 0x040006DE RID: 1758
		private object _CallObject;
	}
}
