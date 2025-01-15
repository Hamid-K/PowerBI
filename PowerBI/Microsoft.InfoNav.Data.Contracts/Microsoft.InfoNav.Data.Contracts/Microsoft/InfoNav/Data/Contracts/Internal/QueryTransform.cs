using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002DB RID: 731
	[DataContract(Name = "Transform", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryTransform
	{
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x0002BD0E File Offset: 0x00029F0E
		// (set) Token: 0x06001871 RID: 6257 RVA: 0x0002BD16 File Offset: 0x00029F16
		[DataMember(IsRequired = true, Order = 1)]
		public string Name { get; set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x0002BD1F File Offset: 0x00029F1F
		// (set) Token: 0x06001873 RID: 6259 RVA: 0x0002BD27 File Offset: 0x00029F27
		[DataMember(IsRequired = true, Order = 2)]
		public string Algorithm { get; set; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0002BD30 File Offset: 0x00029F30
		// (set) Token: 0x06001875 RID: 6261 RVA: 0x0002BD38 File Offset: 0x00029F38
		[DataMember(IsRequired = true, Order = 3)]
		public QueryTransformInput Input { get; set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x0002BD41 File Offset: 0x00029F41
		// (set) Token: 0x06001877 RID: 6263 RVA: 0x0002BD49 File Offset: 0x00029F49
		[DataMember(IsRequired = true, Order = 4)]
		public QueryTransformOutput Output { get; set; }

		// Token: 0x06001878 RID: 6264 RVA: 0x0002BD54 File Offset: 0x00029F54
		internal void WriteQueryString(QueryStringWriter w)
		{
			using (w.NewClauseScope("transform", QueryStringWriter.Separator.Newline))
			{
				w.WriteFormat("via '{0}' as ", new object[] { this.Algorithm });
				w.WriteIdentifierCustomerContent(this.Name);
				w.WriteLine();
				using (w.NewClauseScope("with", QueryStringWriter.Separator.CommaAndNewline))
				{
					this.WriteParameters(w, this.Input.Parameters);
					this.WriteTable("inputtable", w, this.Input.Table);
					this.WriteTable("outputtable", w, this.Output.Table);
				}
			}
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0002BE1C File Offset: 0x0002A01C
		private void WriteParameters(QueryStringWriter w, List<QueryExpressionContainer> parameters)
		{
			if (parameters == null || parameters.Count == 0)
			{
				return;
			}
			w.WriteSeparator();
			QueryStringWriterUtils.WriteFunction<QueryExpressionContainer>("inputparameters", parameters, QueryStringWriter.Separator.Comma, delegate(QueryExpressionContainer param, QueryStringWriter writer)
			{
				writer.WriteExpressionAndName(param);
			}, w);
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0002BE5C File Offset: 0x0002A05C
		private void WriteTable(string functionName, QueryStringWriter w, QueryTransformTable table)
		{
			if (table == null)
			{
				return;
			}
			w.WriteSeparator();
			QueryStringWriterUtils.WriteFunction<QueryTransformTableColumn>(functionName, table.Columns, QueryStringWriter.Separator.Comma, delegate(QueryTransformTableColumn column, QueryStringWriter writer)
			{
				writer.WriteExpressionAndName(column.Expression);
				if (!string.IsNullOrEmpty(column.Role))
				{
					w.WriteFormat(" with role '{0}'", new object[] { column.Role });
				}
			}, w);
			QueryStringWriterUtils.WriteName(table.Name, w);
		}
	}
}
