using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000F0 RID: 240
	[Serializable]
	internal class VowelFoldingTokenizer : IRecordTokenizer
	{
		// Token: 0x060009AB RID: 2475 RVA: 0x0002C158 File Offset: 0x0002A358
		public VowelFoldingTokenizer(string delimiters)
		{
			this.m_delimiters = delimiters;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002C168 File Offset: 0x0002A368
		public void Prepare(DataTable schemaTable, DomainBinding domainBinding, out TokenizerContext context)
		{
			List<Column> columns = domainBinding.Columns;
			int[] array = new int[columns.Count];
			bool[] array2 = new bool[columns.Count];
			for (int i = 0; i < columns.Count; i++)
			{
				if (columns[i].Ordinal >= 0)
				{
					array[i] = columns[i].Ordinal;
					if (schemaTable != null)
					{
						if (SchemaUtils.FindColumnSchemaRow(schemaTable, columns[i].Ordinal) == null)
						{
							throw new Exception(string.Format("Column with ordinal '{0}' not present.", columns[i].Ordinal));
						}
						if (columns[i].Type != null)
						{
							array2[i] = columns[i].Type == typeof(string);
						}
					}
				}
				else
				{
					if (string.IsNullOrEmpty(domainBinding.Columns[i].Name))
					{
						throw new Exception(string.Format("Neither the column Name or column Ordinal was specified for a column in the domain binding for domain {0}", domainBinding.DomainName));
					}
					DataRow dataRow = SchemaUtils.FindColumnSchemaRow(schemaTable, columns[i].Name, true);
					Type type = (Type)dataRow[SchemaTableColumn.DataType];
					array2[i] = type == typeof(string);
					array[i] = (int)dataRow[SchemaTableColumn.ColumnOrdinal];
				}
			}
			context = new VowelFoldingTokenizerContext(this.m_delimiters, array);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002C2C1 File Offset: 0x0002A4C1
		public IEnumerable<StringExtent> Tokenize(TokenizerContext tokenizerContext, IDataRecord record)
		{
			tokenizerContext.Reset();
			return ((VowelFoldingTokenizerContext)tokenizerContext).Tokenize(record);
		}

		// Token: 0x040003B4 RID: 948
		private string m_delimiters;
	}
}
