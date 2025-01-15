using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Oracle
{
	// Token: 0x0200056D RID: 1389
	internal class OracleOutputParameterCollection : OutputParameterCollection
	{
		// Token: 0x06002C44 RID: 11332 RVA: 0x00086DF4 File Offset: 0x00084FF4
		public OracleOutputParameterCollection(Query targetTable, IList<Alias> columnList, IList<Alias> outputParameters, OracleEnvironment environment)
			: base(targetTable, columnList, outputParameters, environment)
		{
			this.propertyName = "OracleDbType";
			this.intervalName = "IntervalDS";
			this.intervalValueTypeName = "Oracle.DataAccess.Types.OracleIntervalDS";
			this.blobName = "Blob";
			this.blobValueTypeName = "Oracle.DataAccess.Types.OracleBlob";
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x00086E44 File Offset: 0x00085044
		protected override void SetParameterType(DbParameter parameter, TypeValue columnType)
		{
			if (columnType.TypeKind == ValueKind.Duration || columnType.TypeKind == ValueKind.Binary)
			{
				PropertyInfo property = parameter.GetType().GetProperty(this.propertyName);
				Type propertyType = property.PropertyType;
				ValueKind typeKind = columnType.TypeKind;
				string text;
				if (typeKind != ValueKind.Duration)
				{
					if (typeKind != ValueKind.Binary)
					{
						throw new InvalidOperationException();
					}
					text = this.blobName;
				}
				else
				{
					text = this.intervalName;
				}
				int num = (int)propertyType.GetField(text).GetValue(propertyType);
				property.SetValue(parameter, Enum.ToObject(propertyType, num), null);
				return;
			}
			base.SetParameterType(parameter, columnType);
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x00086ED8 File Offset: 0x000850D8
		protected override object GetParameterValue(object value)
		{
			Type type = value.GetType();
			if (type.FullName == this.intervalValueTypeName || type.FullName == this.blobValueTypeName)
			{
				return type.GetProperty("Value").GetValue(value, null);
			}
			return base.GetParameterValue(value);
		}

		// Token: 0x0400133B RID: 4923
		private const string ValueName = "Value";

		// Token: 0x0400133C RID: 4924
		private const string OdpNetPropertyName = "OracleDbType";

		// Token: 0x0400133D RID: 4925
		private const string OdpNetIntervalName = "IntervalDS";

		// Token: 0x0400133E RID: 4926
		private const string OdpNetBlobName = "Blob";

		// Token: 0x0400133F RID: 4927
		private const string OdpNetIntervalValueTypeName = "Oracle.DataAccess.Types.OracleIntervalDS";

		// Token: 0x04001340 RID: 4928
		private const string OdpNetBlobValueTypeName = "Oracle.DataAccess.Types.OracleBlob";

		// Token: 0x04001341 RID: 4929
		private readonly string propertyName;

		// Token: 0x04001342 RID: 4930
		private readonly string intervalName;

		// Token: 0x04001343 RID: 4931
		private readonly string intervalValueTypeName;

		// Token: 0x04001344 RID: 4932
		private readonly string blobName;

		// Token: 0x04001345 RID: 4933
		private readonly string blobValueTypeName;
	}
}
