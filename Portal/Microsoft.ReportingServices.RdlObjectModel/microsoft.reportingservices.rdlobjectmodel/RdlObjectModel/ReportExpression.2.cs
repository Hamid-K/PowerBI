using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001DA RID: 474
	public struct ReportExpression<T> : IExpression, IXmlSerializable, IFormattable, IShouldSerialize where T : struct
	{
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0002547C File Offset: 0x0002367C
		// (set) Token: 0x06000F91 RID: 3985 RVA: 0x00025484 File Offset: 0x00023684
		public T Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
				this.m_expression = null;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x00025494 File Offset: 0x00023694
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x000254A1 File Offset: 0x000236A1
		object IExpression.Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = (T)((object)value);
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x000254AF File Offset: 0x000236AF
		// (set) Token: 0x06000F95 RID: 3989 RVA: 0x000254B7 File Offset: 0x000236B7
		public string Expression
		{
			get
			{
				return this.m_expression;
			}
			set
			{
				this.m_expression = value;
				this.m_value = default(T);
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x000254CC File Offset: 0x000236CC
		public bool IsExpression
		{
			get
			{
				return this.m_expression != null;
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000254D7 File Offset: 0x000236D7
		public ReportExpression(T value)
		{
			this.m_value = value;
			this.m_expression = null;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000254E7 File Offset: 0x000236E7
		public ReportExpression(string value)
		{
			this = new ReportExpression<T>(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000254F5 File Offset: 0x000236F5
		public ReportExpression(string value, IFormatProvider provider)
		{
			this.m_value = default(T);
			this.m_expression = null;
			if (!string.IsNullOrEmpty(value))
			{
				this.Init(value, provider);
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0002551C File Offset: 0x0002371C
		private void Init(string value, IFormatProvider provider)
		{
			if (ReportExpression.IsExpressionString(value))
			{
				this.Expression = value;
				return;
			}
			if (typeof(T).IsSubclassOf(typeof(Enum)))
			{
				this.Value = (T)((object)ReportExpression.ParseEnum(typeof(T), value));
				return;
			}
			if (typeof(T) == typeof(ReportSize))
			{
				this.Value = (T)((object)ReportSize.Parse(value, provider));
				return;
			}
			if (typeof(T) == typeof(ReportColor))
			{
				this.Value = (T)((object)ReportColor.Parse(value, provider));
				return;
			}
			try
			{
				if (typeof(T) == typeof(bool))
				{
					this.Value = (T)((object)XmlConvert.ToBoolean(value.ToLowerInvariant()));
				}
				else
				{
					MethodBase parseMethod = this.GetParseMethod();
					object obj = null;
					object[] array2;
					if (ReportExpression<T>.m_parseMethodArgs != 1)
					{
						object[] array = new object[2];
						array[0] = value;
						array2 = array;
						array[1] = provider;
					}
					else
					{
						(array2 = new object[1])[0] = value;
					}
					this.Value = (T)((object)parseMethod.Invoke(obj, array2));
				}
			}
			catch (TargetInvocationException ex)
			{
				if (ex.InnerException != null)
				{
					throw ex.InnerException;
				}
				throw ex;
			}
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0002566C File Offset: 0x0002386C
		public static ReportExpression<T> Parse(string value, IFormatProvider provider)
		{
			return new ReportExpression<T>(value, provider);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00025678 File Offset: 0x00023878
		private MethodInfo GetParseMethod()
		{
			if (ReportExpression<T>.m_parseMethodArgs == 0)
			{
				ReportExpression<T>.m_parseMethod = typeof(T).GetMethod("Parse", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
				{
					typeof(string),
					typeof(IFormatProvider)
				}, null);
				ReportExpression<T>.m_parseMethodArgs = 2;
				if (ReportExpression<T>.m_parseMethod == null)
				{
					ReportExpression<T>.m_parseMethod = typeof(T).GetMethod("Parse", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(string) }, null);
					ReportExpression<T>.m_parseMethodArgs = 1;
				}
			}
			return ReportExpression<T>.m_parseMethod;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0002571A File Offset: 0x0002391A
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00025728 File Offset: 0x00023928
		public string ToString(string format, IFormatProvider provider)
		{
			if (this.IsExpression)
			{
				return this.m_expression;
			}
			if (typeof(T) == typeof(bool) && provider == CultureInfo.InvariantCulture)
			{
				if (!true.Equals(this.m_value))
				{
					return "false";
				}
				return "true";
			}
			else
			{
				if (typeof(IFormattable).IsAssignableFrom(typeof(T)))
				{
					return ((IFormattable)((object)this.m_value)).ToString(format, provider);
				}
				return this.m_value.ToString();
			}
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x000257D0 File Offset: 0x000239D0
		public override bool Equals(object value)
		{
			if (value is ReportExpression<T>)
			{
				return this.m_value.Equals(((ReportExpression<T>)value).Value) && this.m_expression == ((ReportExpression<T>)value).Expression;
			}
			if (this.IsExpression)
			{
				return value is string && this.m_expression == (string)value;
			}
			return this.m_value.Equals(value);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00025860 File Offset: 0x00023A60
		public override int GetHashCode()
		{
			int num = this.m_value.GetHashCode();
			if (this.m_expression != null)
			{
				num ^= this.m_expression.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00025896 File Offset: 0x00023A96
		public void GetDependencies(IList<ReportObject> dependencies, ReportObject parent)
		{
			ReportExpressionUtils.GetDependencies(dependencies, parent, this.Expression);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x000258A5 File Offset: 0x00023AA5
		internal ReportExpression<T> UpdateNamedReferences(NameChanges RenameList)
		{
			this.Expression = ReportExpressionUtils.UpdateNamedReferences(this.Expression, RenameList);
			return this;
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x000258C0 File Offset: 0x00023AC0
		public static bool operator ==(ReportExpression<T> left, ReportExpression<T> right)
		{
			T value = left.Value;
			return value.Equals(right.Value) && left.Expression == right.Expression;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00025908 File Offset: 0x00023B08
		public static bool operator ==(ReportExpression<T> left, T right)
		{
			if (!left.IsExpression)
			{
				T value = left.Value;
				return value.Equals(right);
			}
			return false;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0002593C File Offset: 0x00023B3C
		public static bool operator ==(T left, ReportExpression<T> right)
		{
			if (!right.IsExpression)
			{
				T value = right.Value;
				return value.Equals(left);
			}
			return false;
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0002596F File Offset: 0x00023B6F
		public static bool operator ==(ReportExpression<T> left, string right)
		{
			return left.IsExpression && left.Expression == right;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00025989 File Offset: 0x00023B89
		public static bool operator ==(string left, ReportExpression<T> right)
		{
			return right.IsExpression && right.Expression == left;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x000259A3 File Offset: 0x00023BA3
		public static bool operator !=(ReportExpression<T> left, ReportExpression<T> right)
		{
			return !(left == right);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000259AF File Offset: 0x00023BAF
		public static bool operator !=(ReportExpression<T> left, T right)
		{
			return !(left == right);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x000259BB File Offset: 0x00023BBB
		public static bool operator !=(T left, ReportExpression<T> right)
		{
			return !(left == right);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x000259C7 File Offset: 0x00023BC7
		public static bool operator !=(ReportExpression<T> left, string right)
		{
			return !(left == right);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000259D3 File Offset: 0x00023BD3
		public static bool operator !=(string left, ReportExpression<T> right)
		{
			return !(left == right);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000259DF File Offset: 0x00023BDF
		public static explicit operator T(ReportExpression<T> value)
		{
			return value.Value;
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000259E8 File Offset: 0x00023BE8
		public static implicit operator ReportExpression<T>(T value)
		{
			return new ReportExpression<T>(value);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x000259F0 File Offset: 0x00023BF0
		public static implicit operator ReportExpression<T>(T? value)
		{
			if (value != null)
			{
				return new ReportExpression<T>(value.Value);
			}
			return new ReportExpression<T>(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00025A13 File Offset: 0x00023C13
		public static explicit operator string(ReportExpression<T> value)
		{
			return value.ToString();
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00025A22 File Offset: 0x00023C22
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00025A28 File Offset: 0x00023C28
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.ReadString();
			this.Init(text, CultureInfo.InvariantCulture);
			reader.Skip();
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00025A4E File Offset: 0x00023C4E
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteString(this.ToString(null, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00025A62 File Offset: 0x00023C62
		bool IShouldSerialize.ShouldSerializeThis()
		{
			return this.IsExpression || !typeof(IShouldSerialize).IsAssignableFrom(typeof(T)) || ((IShouldSerialize)((object)this.m_value)).ShouldSerializeThis();
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00025A9E File Offset: 0x00023C9E
		SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
		{
			return SerializationMethod.Auto;
		}

		// Token: 0x04000565 RID: 1381
		private T m_value;

		// Token: 0x04000566 RID: 1382
		private string m_expression;

		// Token: 0x04000567 RID: 1383
		private static MethodInfo m_parseMethod;

		// Token: 0x04000568 RID: 1384
		private static int m_parseMethodArgs;
	}
}
