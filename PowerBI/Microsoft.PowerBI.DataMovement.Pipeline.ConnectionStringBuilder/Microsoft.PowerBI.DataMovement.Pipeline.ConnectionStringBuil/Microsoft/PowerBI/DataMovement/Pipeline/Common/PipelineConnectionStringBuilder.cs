using System;
using System.Data.Common;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;
using Microsoft.PowerBI.DataMovement.Pipeline.ExceptionUtilities;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x02000007 RID: 7
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class PipelineConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020B3 File Offset: 0x000002B3
		internal PipelineConnectionStringBuilder(bool useOdbcRules = false)
			: this(null, useOdbcRules)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020BD File Offset: 0x000002BD
		internal PipelineConnectionStringBuilder(string connectionString, bool useOdbcRules = false)
			: base(useOdbcRules)
		{
			base.ConnectionString = connectionString;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020CD File Offset: 0x000002CD
		internal string ToTraceableConnectionString()
		{
			if (!this.ContainsKey("DMTSConnectionDetails"))
			{
				return base.ConnectionString;
			}
			PipelineConnectionStringBuilder pipelineConnectionStringBuilder = new PipelineConnectionStringBuilder(base.ConnectionString, false);
			pipelineConnectionStringBuilder["DMTSConnectionDetails"] = "<removed>";
			return pipelineConnectionStringBuilder.ConnectionString;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002104 File Offset: 0x00000304
		internal TValue GetRequiredValue<[global::System.Runtime.CompilerServices.Nullable(2)] TValue>(string keyword)
		{
			object obj;
			if (!this.TryGetTypedValue<object>(keyword, out obj))
			{
				throw new MissingRequiredConnectionStringPropertyException(keyword, string.Empty, Array.Empty<PowerBIErrorDetail>());
			}
			TValue tvalue;
			if (!this.TryConvertValue<TValue>(obj, out tvalue))
			{
				throw new MismatchedConnectionStringPropertyTypeException(keyword, typeof(TValue).FullName, Array.Empty<PowerBIErrorDetail>());
			}
			return tvalue;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002154 File Offset: 0x00000354
		internal TValue GetValueWithDefault<[global::System.Runtime.CompilerServices.Nullable(2)] TValue>(string keyword)
		{
			return this.GetValueWithDefault<TValue>(keyword, default(TValue));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002174 File Offset: 0x00000374
		internal TValue GetValueWithDefault<[global::System.Runtime.CompilerServices.Nullable(2)] TValue>(string keyword, TValue defaultValue)
		{
			object obj;
			if (!this.TryGetTypedValue<object>(keyword, out obj))
			{
				return defaultValue;
			}
			TValue tvalue;
			if (!this.TryConvertValue<TValue>(obj, out tvalue))
			{
				throw new MismatchedConnectionStringPropertyTypeException(keyword, typeof(TValue).FullName, Array.Empty<PowerBIErrorDetail>());
			}
			return tvalue;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021B8 File Offset: 0x000003B8
		internal bool TryGetTypedValue<[global::System.Runtime.CompilerServices.Nullable(2)] TValue>(string keyword, out TValue value)
		{
			object obj;
			if (!this.TryGetValue(keyword, out obj))
			{
				value = default(TValue);
				return false;
			}
			return this.TryConvertValue<TValue>(obj, out value);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021E4 File Offset: 0x000003E4
		private bool TryConvertValue<[global::System.Runtime.CompilerServices.Nullable(2)] TValue>(object value, out TValue convertedValue)
		{
			if (value is TValue)
			{
				convertedValue = (TValue)((object)value);
				return true;
			}
			if (value is IConvertible && typeof(TValue).IsPrimitive)
			{
				try
				{
					convertedValue = (TValue)((object)Convert.ChangeType(value, typeof(TValue), CultureInfo.InvariantCulture));
					return true;
				}
				catch (Exception ex)
				{
					if (ex.IsFatal())
					{
						throw;
					}
				}
			}
			try
			{
				byte[] array = Convert.FromBase64String((string)value);
				string @string = Encoding.UTF8.GetString(array);
				convertedValue = JsonConvert.DeserializeObject<TValue>(@string);
				return true;
			}
			catch (Exception ex2)
			{
				if (ex2.IsFatal())
				{
					throw;
				}
			}
			convertedValue = default(TValue);
			return false;
		}

		// Token: 0x04000011 RID: 17
		internal const string DMTSConnectionDetails = "DMTSConnectionDetails";
	}
}
