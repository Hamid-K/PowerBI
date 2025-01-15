using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000287 RID: 647
	[Serializable]
	internal class FunctionReportParameter : BaseInternalExpression, IInternalNamedExpression
	{
		// Token: 0x0600145C RID: 5212 RVA: 0x0002FEF7 File Offset: 0x0002E0F7
		public FunctionReportParameter(ReportParameter reportParameter, string property)
		{
			this._ReportParameter = reportParameter;
			this._Property = property;
			base.IsArray = reportParameter.MultiValue;
			if (string.IsNullOrEmpty(this._Property))
			{
				this._Property = "Value";
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0002FF31 File Offset: 0x0002E131
		public FunctionReportParameter(IInternalExpression nameExpr)
		{
			this._NameExpr = nameExpr;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0002FF40 File Offset: 0x0002E140
		public FunctionReportParameter(IInternalExpression nameExpr, string property)
		{
			this._NameExpr = nameExpr;
			this._Property = property;
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0002FF56 File Offset: 0x0002E156
		public string Name
		{
			get
			{
				return this._NameExpr.EvaluateString();
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0002FF63 File Offset: 0x0002E163
		public string DisplayName
		{
			get
			{
				return this._NameExpr.EvaluateString();
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x0002FF70 File Offset: 0x0002E170
		public ReportParameter Parameter
		{
			get
			{
				return this._ReportParameter;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0002FF78 File Offset: 0x0002E178
		public bool IsPropertyValue
		{
			get
			{
				return string.Equals(this._Property, "Value", StringComparison.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0002FF8B File Offset: 0x0002E18B
		public bool IsPropertyLabel
		{
			get
			{
				return string.Equals(this._Property, "Label", StringComparison.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0002FF9E File Offset: 0x0002E19E
		public bool IsPropertyCount
		{
			get
			{
				return string.Equals(this._Property, "Count", StringComparison.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0002FFB1 File Offset: 0x0002E1B1
		public bool IsPropertyIsMultiValue
		{
			get
			{
				return string.Equals(this._Property, "IsMultiValue", StringComparison.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0002FFC4 File Offset: 0x0002E1C4
		public override TypeCode TypeCode()
		{
			TypeCode typeCode;
			if (this.IsPropertyValue)
			{
				typeCode = this.GetValueTypeCode();
			}
			else if (this.IsPropertyLabel)
			{
				typeCode = global::System.TypeCode.String;
			}
			else if (this.IsPropertyCount)
			{
				typeCode = global::System.TypeCode.Int32;
			}
			else if (this.IsPropertyIsMultiValue)
			{
				typeCode = global::System.TypeCode.Boolean;
			}
			else
			{
				typeCode = this.GetValueTypeCode();
			}
			return typeCode;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00030010 File Offset: 0x0002E210
		public override string WriteSource(NameChanges nameChanges)
		{
			StringBuilder stringBuilder = new StringBuilder("Parameters!");
			stringBuilder.Append(nameChanges.GetNewName(NameChanges.EntryType.ReportParameter, (string)this._NameExpr.Evaluate()));
			stringBuilder.Append(".");
			stringBuilder.Append(this._Property ?? "Value");
			return stringBuilder.ToString();
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0003006C File Offset: 0x0002E26C
		protected override void TraverseChildren(ProcessInternalExpressionHandler callback)
		{
			base.TraverseChildren(callback);
			if (this._NameExpr != null)
			{
				this._NameExpr.Traverse(callback);
			}
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0003008C File Offset: 0x0002E28C
		public override object Evaluate()
		{
			IList<object> list = this.EvaluateAsList();
			int? num;
			if (this._ReportParameter.MultiValue)
			{
				num = null;
			}
			else
			{
				num = new int?(0);
			}
			if (num == null)
			{
				return list;
			}
			if (num.Value < list.Count)
			{
				return list[num.Value];
			}
			return RDLUtil.DefaultDataType(this._ReportParameter.DataType);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x000300F7 File Offset: 0x0002E2F7
		private IList<object> EvaluateAsList()
		{
			return null;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x000300FC File Offset: 0x0002E2FC
		private TypeCode GetValueTypeCode()
		{
			switch (this._ReportParameter.DataType)
			{
			case DataTypes.String:
				return global::System.TypeCode.String;
			case DataTypes.Boolean:
				return global::System.TypeCode.Boolean;
			case DataTypes.DateTime:
				return global::System.TypeCode.DateTime;
			case DataTypes.Integer:
				return global::System.TypeCode.Int32;
			case DataTypes.Float:
				return global::System.TypeCode.Double;
			default:
				return global::System.TypeCode.String;
			}
		}

		// Token: 0x040006AB RID: 1707
		private readonly ReportParameter _ReportParameter;

		// Token: 0x040006AC RID: 1708
		private readonly IInternalExpression _NameExpr;

		// Token: 0x040006AD RID: 1709
		private readonly string _Property;
	}
}
