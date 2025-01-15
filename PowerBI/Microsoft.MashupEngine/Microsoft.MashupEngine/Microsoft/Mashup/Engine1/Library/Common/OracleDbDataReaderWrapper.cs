using System;
using System.Data.Common;
using System.Globalization;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Oracle;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001162 RID: 4450
	internal class OracleDbDataReaderWrapper : DelegatingDbDataReaderWithTableSchema
	{
		// Token: 0x06007494 RID: 29844 RVA: 0x001901B0 File Offset: 0x0018E3B0
		public OracleDbDataReaderWrapper(DbDataReaderWithTableSchema reader, OracleEnvironment environment)
			: base(reader)
		{
			this.environment = environment;
			this.resource = environment.Resource;
		}

		// Token: 0x06007495 RID: 29845 RVA: 0x001901CC File Offset: 0x0018E3CC
		public OracleDbDataReaderWrapper(DbDataReaderWithTableSchema reader, string decimalGroupSeparator)
			: base(reader)
		{
			this.decimalGroupSeparator = decimalGroupSeparator;
		}

		// Token: 0x06007496 RID: 29846 RVA: 0x001901DC File Offset: 0x0018E3DC
		public override decimal GetDecimal(int ordinal)
		{
			decimal @decimal;
			try
			{
				@decimal = base.GetDecimal(ordinal);
			}
			catch (Exception ex)
			{
				if (this.IsOverFlowException(ex))
				{
					throw new OverflowException(ex.Message, ex);
				}
				this.WrapThrowException(ex, ordinal);
				throw;
			}
			return @decimal;
		}

		// Token: 0x06007497 RID: 29847 RVA: 0x00190228 File Offset: 0x0018E428
		public override object GetValue(int ordinal)
		{
			object obj;
			try
			{
				obj = base.GetValue(ordinal);
			}
			catch (Exception ex)
			{
				object obj2;
				if (!this.IsOverFlowException(ex) || !this.TryParseNumber(ordinal, out obj2))
				{
					this.WrapThrowException(ex, ordinal);
					throw;
				}
				obj = obj2;
			}
			return obj;
		}

		// Token: 0x06007498 RID: 29848 RVA: 0x00190274 File Offset: 0x0018E474
		public override int GetValues(object[] values)
		{
			int values2;
			try
			{
				values2 = base.GetValues(values);
			}
			catch (Exception ex)
			{
				this.WrapThrowException(ex, -1);
				throw;
			}
			return values2;
		}

		// Token: 0x17002062 RID: 8290
		public override object this[string name]
		{
			get
			{
				object obj;
				try
				{
					obj = base[name];
				}
				catch (Exception ex)
				{
					object obj2;
					if (!this.IsOverFlowException(ex) || !this.TryParseNumber(this.GetOrdinal(name), out obj2))
					{
						this.WrapThrowException(ex, this.GetOrdinal(name));
						throw;
					}
					obj = obj2;
				}
				return obj;
			}
		}

		// Token: 0x17002063 RID: 8291
		public override object this[int ordinal]
		{
			get
			{
				object obj;
				try
				{
					obj = base[ordinal];
				}
				catch (Exception ex)
				{
					object obj2;
					if (!this.IsOverFlowException(ex) || !this.TryParseNumber(ordinal, out obj2))
					{
						this.WrapThrowException(ex, ordinal);
						throw;
					}
					obj = obj2;
				}
				return obj;
			}
		}

		// Token: 0x17002064 RID: 8292
		// (get) Token: 0x0600749B RID: 29851 RVA: 0x0019034C File Offset: 0x0018E54C
		public override bool HasRows
		{
			get
			{
				bool hasRows;
				try
				{
					hasRows = base.HasRows;
				}
				catch (Exception ex)
				{
					this.WrapThrowException(ex, -1);
					throw;
				}
				return hasRows;
			}
		}

		// Token: 0x17002065 RID: 8293
		// (get) Token: 0x0600749C RID: 29852 RVA: 0x00190380 File Offset: 0x0018E580
		private NumberFormatInfo ClientNumberFormat
		{
			get
			{
				if (this.clientNumberFormat == null)
				{
					this.clientNumberFormat = new CultureInfo(CultureInfo.InvariantCulture.Name).NumberFormat;
					this.clientNumberFormat.NumberDecimalSeparator = this.DecimalGroupSeparator[0].ToString();
					this.clientNumberFormat.NumberGroupSeparator = this.DecimalGroupSeparator[1].ToString();
				}
				return this.clientNumberFormat;
			}
		}

		// Token: 0x17002066 RID: 8294
		// (get) Token: 0x0600749D RID: 29853 RVA: 0x001903F3 File Offset: 0x0018E5F3
		private string DecimalGroupSeparator
		{
			get
			{
				if (this.decimalGroupSeparator == null)
				{
					this.decimalGroupSeparator = this.environment.GetSessionSeparator();
				}
				return this.decimalGroupSeparator;
			}
		}

		// Token: 0x0600749E RID: 29854 RVA: 0x00190414 File Offset: 0x0018E614
		private bool TryParseNumber(int index, out object number)
		{
			string text = base.GetProviderSpecificValue(index).ToString();
			try
			{
				decimal num;
				if (decimal.TryParse(text, NumberStyles.Any, this.ClientNumberFormat, out num))
				{
					number = num;
					return true;
				}
				double num2;
				if (double.TryParse(text, NumberStyles.Any, this.ClientNumberFormat, out num2))
				{
					number = num2;
					return true;
				}
			}
			catch (Exception ex)
			{
				if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
			}
			number = text;
			return false;
		}

		// Token: 0x0600749F RID: 29855 RVA: 0x00190494 File Offset: 0x0018E694
		private bool IsOverFlowException(Exception originalException)
		{
			if (originalException is OverflowException || originalException is InvalidCastException || originalException is ArgumentOutOfRangeException)
			{
				return true;
			}
			DbException ex = originalException as DbException;
			int num;
			return ex != null && OracleExceptionHelper.TryGetErrorCode(ex, out num, null) && num == 22053;
		}

		// Token: 0x060074A0 RID: 29856 RVA: 0x001904DC File Offset: 0x0018E6DC
		private void WrapThrowException(Exception originalException, int ordinal = -1)
		{
			DbException ex = originalException as DbException;
			IEngineHost engineHost = ((this.environment != null) ? this.environment.Host : null);
			if (ex != null)
			{
				Value value = Value.Null;
				TypeValue typeValue = TypeValue.Null;
				int num;
				if (OracleExceptionHelper.TryGetErrorCode(ex, out num, null))
				{
					value = NumberValue.New(num);
					typeValue = TypeValue.Number;
				}
				throw DataSourceException.NewDataSourceError(engineHost, originalException.Message, this.resource, "ErrorCode", value, typeValue, ex);
			}
			if (originalException is ArgumentOutOfRangeException && ordinal != -1 && this.GetFieldType(ordinal) == typeof(DateTime))
			{
				throw DataSourceException.NewDataSourceError<Message0>(engineHost, Strings.Date_OutOfRangeError, this.resource, null, originalException);
			}
		}

		// Token: 0x0400401B RID: 16411
		private readonly OracleEnvironment environment;

		// Token: 0x0400401C RID: 16412
		private readonly IResource resource;

		// Token: 0x0400401D RID: 16413
		private NumberFormatInfo clientNumberFormat;

		// Token: 0x0400401E RID: 16414
		private string decimalGroupSeparator;
	}
}
