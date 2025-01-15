using System;
using System.Data;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.PrimitiveValues;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001D5 RID: 469
	[DataContract(Name = "ScriptInputParameter", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ScriptInputParameter : IDataParameter, IEquatable<ScriptInputParameter>
	{
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00018731 File Offset: 0x00016931
		// (set) Token: 0x06000C70 RID: 3184 RVA: 0x00018739 File Offset: 0x00016939
		[DataMember(IsRequired = true, Order = 10)]
		public string ObjectName { get; set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00018742 File Offset: 0x00016942
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x0001874A File Offset: 0x0001694A
		[DataMember(IsRequired = true, Order = 20)]
		public string PropertyName { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00018753 File Offset: 0x00016953
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x0001875B File Offset: 0x0001695B
		[DataMember(IsRequired = true, Order = 30, Name = "Value")]
		public string ParameterValue { get; set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00018764 File Offset: 0x00016964
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x000187DD File Offset: 0x000169DD
		public DbType DbType
		{
			get
			{
				switch (this.PrimitiveValue.Type)
				{
				case ConceptualPrimitiveType.Null:
					return DbType.Object;
				case ConceptualPrimitiveType.Text:
					return DbType.String;
				case ConceptualPrimitiveType.Decimal:
					return DbType.Decimal;
				case ConceptualPrimitiveType.Double:
					return DbType.Double;
				case ConceptualPrimitiveType.Integer:
					return DbType.Int64;
				case ConceptualPrimitiveType.Boolean:
					return DbType.Boolean;
				case ConceptualPrimitiveType.Date:
					return DbType.Date;
				case ConceptualPrimitiveType.DateTime:
					return DbType.DateTime;
				case ConceptualPrimitiveType.DateTimeZone:
					return DbType.DateTime2;
				case ConceptualPrimitiveType.Time:
					return DbType.Time;
				case ConceptualPrimitiveType.Duration:
					return DbType.DateTimeOffset;
				case ConceptualPrimitiveType.Binary:
					return DbType.Binary;
				default:
					throw new ArgumentException("ScriptInputParameterValue returned Unknown or Unsupported Type");
				}
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x000187E4 File Offset: 0x000169E4
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x000187EC File Offset: 0x000169EC
		public ParameterDirection Direction { get; set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x000187F5 File Offset: 0x000169F5
		public bool IsNullable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x000187F8 File Offset: 0x000169F8
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x00018800 File Offset: 0x00016A00
		public string ParameterName { get; set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00018809 File Offset: 0x00016A09
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x00018811 File Offset: 0x00016A11
		public string SourceColumn { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0001881A File Offset: 0x00016A1A
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x00018822 File Offset: 0x00016A22
		public DataRowVersion SourceVersion { get; set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0001882B File Offset: 0x00016A2B
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x00018838 File Offset: 0x00016A38
		public object Value
		{
			get
			{
				return this.PrimitiveValue.GetValueAsObject();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0001883F File Offset: 0x00016A3F
		private PrimitiveValue PrimitiveValue
		{
			get
			{
				if (this.m_primitiveValue == null && !PrimitiveValueEncoding.TryParseTypeEncodedString(this.ParameterValue, out this.m_primitiveValue))
				{
					throw new ArgumentException("Invalid parameter value " + this.ParameterValue);
				}
				return this.m_primitiveValue;
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00018878 File Offset: 0x00016A78
		public bool Equals(ScriptInputParameter other)
		{
			return string.Equals(this.ObjectName, other.ObjectName, StringComparison.Ordinal) && string.Equals(this.PropertyName, other.PropertyName, StringComparison.Ordinal) && string.Equals(this.ParameterValue, other.ParameterValue, StringComparison.Ordinal);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x000188B6 File Offset: 0x00016AB6
		public override bool Equals(object other)
		{
			return this.Equals(other as ScriptInputParameter);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x000188C4 File Offset: 0x00016AC4
		public override int GetHashCode()
		{
			return Hashing.CombineHash((this.ObjectName != null) ? this.ObjectName.GetHashCode() : 0, (this.PropertyName != null) ? this.PropertyName.GetHashCode() : 0, (this.ParameterValue != null) ? this.ParameterValue.GetHashCode() : 0);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00018918 File Offset: 0x00016B18
		public static bool operator ==(ScriptInputParameter left, ScriptInputParameter right)
		{
			bool? flag = Util.AreEqual<ScriptInputParameter>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00018945 File Offset: 0x00016B45
		public static bool operator !=(ScriptInputParameter left, ScriptInputParameter right)
		{
			return !(left == right);
		}

		// Token: 0x0400068A RID: 1674
		private PrimitiveValue m_primitiveValue;
	}
}
