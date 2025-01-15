using System;
using System.Globalization;
using System.Reflection;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.MySQL
{
	// Token: 0x0200091A RID: 2330
	internal class MySQLWrappingDbDataReader : DelegatingDbDataReaderWithTableSchema
	{
		// Token: 0x06004279 RID: 17017 RVA: 0x000E0524 File Offset: 0x000DE724
		public MySQLWrappingDbDataReader(DbDataReaderWithTableSchema reader)
			: base(reader)
		{
		}

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x000E052D File Offset: 0x000DE72D
		private MethodInfo GetMySqlDecimalMethodInfo
		{
			get
			{
				if (this.mySqlDecimalMethodInfo == null)
				{
					this.mySqlDecimalMethodInfo = this.GetMethodInfo(DelegatingDbDataReaderWithTableSchema.Unwrap(base.DataReader), "GetMySqlDecimal", MySQLWrappingDbDataReader.GetMySqlDecimalParamType);
				}
				return this.mySqlDecimalMethodInfo;
			}
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x000E0564 File Offset: 0x000DE764
		public override object GetProviderSpecificValue(int ordinal)
		{
			object providerSpecificValue;
			try
			{
				providerSpecificValue = base.GetProviderSpecificValue(ordinal);
			}
			catch (OverflowException)
			{
				if (base.GetProviderSpecificFieldType(ordinal) == typeof(decimal))
				{
					object obj = this.InvokeMethod(this.GetMySqlDecimalMethodInfo, DelegatingDbDataReaderWithTableSchema.Unwrap(base.DataReader), new object[] { ordinal });
					if (obj != null)
					{
						double num;
						if (!double.TryParse(obj.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out num))
						{
							object obj2 = this.InvokeMethod(this.GetMethodInfo(obj, "ToDouble", EmptyArray<Type>.Instance), obj, EmptyArray<object>.Instance);
							if (obj2 == null)
							{
								throw;
							}
							num = (double)obj2;
						}
						return num;
					}
				}
				throw;
			}
			return providerSpecificValue;
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x000E0620 File Offset: 0x000DE820
		public override bool IsDBNull(int ordinal)
		{
			bool flag;
			try
			{
				flag = base.IsDBNull(ordinal);
			}
			catch (OverflowException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x000E0650 File Offset: 0x000DE850
		private MethodInfo GetMethodInfo(object instance, string methodName, Type[] paramType)
		{
			try
			{
				return instance.GetType().GetMethod(methodName, paramType);
			}
			catch (Exception ex)
			{
				if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x000E068C File Offset: 0x000DE88C
		private object InvokeMethod(MethodInfo methodInfo, object instance, object[] parameters)
		{
			try
			{
				return methodInfo.Invoke(instance, parameters);
			}
			catch (Exception ex)
			{
				if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x040022DA RID: 8922
		private const string GetMySqlDecimalMethodName = "GetMySqlDecimal";

		// Token: 0x040022DB RID: 8923
		private const string ToDoubleMethodName = "ToDouble";

		// Token: 0x040022DC RID: 8924
		private static readonly Type[] GetMySqlDecimalParamType = new Type[] { typeof(int) };

		// Token: 0x040022DD RID: 8925
		private MethodInfo mySqlDecimalMethodInfo;
	}
}
