using System;
using System.Collections.Generic;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Analytics
{
	// Token: 0x020000BA RID: 186
	internal sealed class SchemaRowFactory : ISchemaRowFactory
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x0000E2B7 File Offset: 0x0000C4B7
		private SchemaRowFactory()
		{
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000E2BF File Offset: 0x0000C4BF
		public ISchemaRow CreateSchemaRow(IReadOnlyList<IColumn> columns)
		{
			return new SchemaRow(columns);
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000E2C7 File Offset: 0x0000C4C7
		internal static SchemaRowFactory Instance
		{
			get
			{
				return SchemaRowFactory.PrivateInstance;
			}
		}

		// Token: 0x04000266 RID: 614
		private static readonly SchemaRowFactory PrivateInstance = new SchemaRowFactory();
	}
}
