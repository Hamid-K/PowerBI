using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2010;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000075 RID: 117
	internal sealed class ItemSearchCondition
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x00014050 File Offset: 0x00012250
		public void BindToSqlCommand(InstrumentedSqlCommand command, string parameterPrefix, bool singleValue)
		{
			RSTrace.CatalogTrace.Assert(command != null, "command");
			RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(parameterPrefix), "parameterPrefix");
			RSTrace.CatalogTrace.Assert(!parameterPrefix.StartsWith("@", StringComparison.Ordinal), "parameterPrefix starts with @");
			if (singleValue)
			{
				RSTrace.CatalogTrace.Assert(this.Values.Count == 1, "Values.Length mismatch w/singleValue");
				command.AddParameter("@" + parameterPrefix, this.DbType, this[0]);
				return;
			}
			for (int i = 0; i < this.Values.Count; i++)
			{
				command.AddParameter(string.Format(CultureInfo.InvariantCulture, "@{0}{1}", parameterPrefix, i.ToString(CultureInfo.InvariantCulture)), this.DbType, this[i]);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0001412C File Offset: 0x0001232C
		private SqlDbType DbType
		{
			get
			{
				switch (this.Domain)
				{
				case ComparisonDomain.String:
					return SqlDbType.NVarChar;
				case ComparisonDomain.DateTime:
					return SqlDbType.DateTime;
				case ComparisonDomain.UniqueIdentifier:
					return SqlDbType.UniqueIdentifier;
				default:
					throw new InternalCatalogException("unknown domain");
				}
			}
		}

		// Token: 0x17000150 RID: 336
		private object this[int index]
		{
			get
			{
				switch (this.Domain)
				{
				case ComparisonDomain.String:
					return this.Values[index];
				case ComparisonDomain.DateTime:
					return this.DateTimeValues[index].LocalDateTime;
				case ComparisonDomain.UniqueIdentifier:
					try
					{
						return new Guid(this.Values[index]);
					}
					catch (ArgumentException ex)
					{
						ItemSearchCondition.LogGuidParseError(ex);
						return DBNull.Value;
					}
					catch (OverflowException ex2)
					{
						ItemSearchCondition.LogGuidParseError(ex2);
						return DBNull.Value;
					}
					catch (FormatException ex3)
					{
						ItemSearchCondition.LogGuidParseError(ex3);
						return DBNull.Value;
					}
					break;
				}
				throw new InternalCatalogException("unknown domain");
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001422C File Offset: 0x0001242C
		private static void LogGuidParseError(Exception ex)
		{
			if (RSTrace.CatalogTrace.TraceVerbose)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Error parsing Guid value: {0}", new object[] { ex.ToString() });
			}
		}

		// Token: 0x04000244 RID: 580
		public ConditionEnum Condition;

		// Token: 0x04000245 RID: 581
		public ComparisonDomain Domain;

		// Token: 0x04000246 RID: 582
		public IList<string> Values;

		// Token: 0x04000247 RID: 583
		public IList<DateTimeOffset> DateTimeValues;
	}
}
