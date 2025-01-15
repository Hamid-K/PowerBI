using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001D9 RID: 473
	public struct ReportExpression : IExpression, IXmlSerializable, IFormattable
	{
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00025105 File Offset: 0x00023305
		// (set) Token: 0x06000F70 RID: 3952 RVA: 0x00025116 File Offset: 0x00023316
		public string Value
		{
			get
			{
				return this.m_value ?? "";
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0002511F File Offset: 0x0002331F
		// (set) Token: 0x06000F72 RID: 3954 RVA: 0x00025127 File Offset: 0x00023327
		public DataTypes DataType
		{
			get
			{
				return this.m_dataType;
			}
			set
			{
				this.m_dataType = value;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x00025130 File Offset: 0x00023330
		// (set) Token: 0x06000F74 RID: 3956 RVA: 0x00025138 File Offset: 0x00023338
		object IExpression.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (string)value;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00025146 File Offset: 0x00023346
		// (set) Token: 0x06000F76 RID: 3958 RVA: 0x0002514E File Offset: 0x0002334E
		public string Expression
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x00025157 File Offset: 0x00023357
		// (set) Token: 0x06000F78 RID: 3960 RVA: 0x0002515F File Offset: 0x0002335F
		public EvaluationMode EvaluationMode
		{
			get
			{
				return this.m_evaluationMode;
			}
			set
			{
				this.m_evaluationMode = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x00025168 File Offset: 0x00023368
		public bool IsExpression
		{
			get
			{
				return this.EvaluationMode == EvaluationMode.Auto && ReportExpression.IsExpressionString(this.m_value);
			}
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0002517F File Offset: 0x0002337F
		public ReportExpression(string value)
		{
			this.m_value = value;
			this.m_dataType = DataTypes.String;
			this.m_evaluationMode = EvaluationMode.Auto;
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00025196 File Offset: 0x00023396
		public ReportExpression(string value, EvaluationMode evaluationMode)
		{
			this.m_value = value;
			this.m_dataType = DataTypes.String;
			this.m_evaluationMode = evaluationMode;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x000251AD File Offset: 0x000233AD
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x000251B5 File Offset: 0x000233B5
		public string ToString(string format, IFormatProvider provider)
		{
			return this.Value;
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x000251BD File Offset: 0x000233BD
		public void GetDependencies(IList<ReportObject> dependencies, ReportObject parent)
		{
			ReportExpressionUtils.GetDependencies(dependencies, parent, this.Expression);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x000251CC File Offset: 0x000233CC
		internal ReportExpression UpdateNamedReferences(NameChanges RenameList)
		{
			this.Expression = ReportExpressionUtils.UpdateNamedReferences(this.Expression, RenameList);
			return this;
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x000251E6 File Offset: 0x000233E6
		public static bool IsExpressionString(string value)
		{
			return value != null && ReportExpression.m_nonConstantRegex.IsMatch(value);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x000251F8 File Offset: 0x000233F8
		public override bool Equals(object value)
		{
			if (value is ReportExpression)
			{
				ReportExpression reportExpression = (ReportExpression)value;
				return this.Value == reportExpression.Value && this.IsExpression == reportExpression.IsExpression && this.DataType == reportExpression.DataType;
			}
			if (value is string)
			{
				return this.Equals(new ReportExpression(((string)value) ?? ""));
			}
			return value == null && this.Value == "";
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0002528C File Offset: 0x0002348C
		public static bool operator ==(ReportExpression left, ReportExpression right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x000252A1 File Offset: 0x000234A1
		public static bool operator ==(ReportExpression left, string right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x000252B1 File Offset: 0x000234B1
		public static bool operator ==(string left, ReportExpression right)
		{
			return right.Equals(left);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x000252C1 File Offset: 0x000234C1
		public static bool operator !=(ReportExpression left, ReportExpression right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x000252D9 File Offset: 0x000234D9
		public static bool operator !=(ReportExpression left, string right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x000252EC File Offset: 0x000234EC
		public static bool operator !=(string left, ReportExpression right)
		{
			return !right.Equals(left);
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x000252FF File Offset: 0x000234FF
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0002530C File Offset: 0x0002350C
		public static explicit operator string(ReportExpression value)
		{
			return value.Value;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00025315 File Offset: 0x00023515
		public static implicit operator ReportExpression(string value)
		{
			return new ReportExpression(value);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0002531D File Offset: 0x0002351D
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00025320 File Offset: 0x00023520
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("DataType");
			if (attribute != null)
			{
				this.DataType = (DataTypes)ReportExpression.ParseEnum(typeof(DataTypes), attribute);
			}
			string attribute2 = reader.GetAttribute("EvaluationMode");
			if (attribute2 != null)
			{
				this.EvaluationMode = (EvaluationMode)ReportExpression.ParseEnum(typeof(EvaluationMode), attribute2);
			}
			this.m_value = reader.ReadString();
			reader.Skip();
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00025394 File Offset: 0x00023594
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.DataType != DataTypes.String)
			{
				writer.WriteAttributeString("DataType", this.DataType.ToString());
			}
			if (this.EvaluationMode != EvaluationMode.Auto)
			{
				writer.WriteAttributeString("EvaluationMode", this.EvaluationMode.ToString());
			}
			if (this.Value.Length > 0)
			{
				if (this.Value.Trim().Length == 0)
				{
					writer.WriteAttributeString("xml", "space", null, "preserve");
				}
				writer.WriteString(this.Value);
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00025434 File Offset: 0x00023634
		internal static object ParseEnum(Type type, string value)
		{
			int num = Array.IndexOf<string>(Enum.GetNames(type), value);
			if (num < 0)
			{
				throw new ArgumentException(SRErrorsWrapper.InvalidValue(value));
			}
			return Enum.GetValues(type).GetValue(num);
		}

		// Token: 0x04000561 RID: 1377
		private string m_value;

		// Token: 0x04000562 RID: 1378
		private DataTypes m_dataType;

		// Token: 0x04000563 RID: 1379
		private EvaluationMode m_evaluationMode;

		// Token: 0x04000564 RID: 1380
		private static readonly Regex m_nonConstantRegex = new Regex("^\\s*=", RegexOptions.Compiled);
	}
}
