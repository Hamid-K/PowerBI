using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Parquet.Schema;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F73 RID: 8051
	internal class SchemaConformingPageReader : IPageReader, IDisposable
	{
		// Token: 0x06010E45 RID: 69189 RVA: 0x003A3BB8 File Offset: 0x003A1DB8
		private SchemaConformingPageReader(IPageReader pageReader, TableSchema schema, Dictionary<int, SchemaConformingPageReader.SchemaConformingColumn> conformingColumns)
		{
			this.pageReader = pageReader;
			this.schema = schema;
			this.conformingColumns = conformingColumns;
		}

		// Token: 0x06010E46 RID: 69190 RVA: 0x003A3BD8 File Offset: 0x003A1DD8
		public static bool TryCreate(IPageReader pageReader, SchemaElement schema, out IPageReader conformingPageReader)
		{
			TableSchema tableSchema = pageReader.Schema;
			TableSchema tableSchema2 = new TableSchema(tableSchema.ColumnCount);
			Dictionary<int, SchemaConformingPageReader.SchemaConformingColumn> dictionary = new Dictionary<int, SchemaConformingPageReader.SchemaConformingColumn>();
			GroupSchemaElement groupSchemaElement = (GroupSchemaElement)schema;
			if (tableSchema.ColumnCount != groupSchemaElement.FieldKeys.Length)
			{
				conformingPageReader = null;
				return false;
			}
			for (int i = 0; i < tableSchema.ColumnCount; i++)
			{
				SchemaColumn column = tableSchema.GetColumn(i);
				PrimitiveSchemaElement primitiveSchemaElement = groupSchemaElement.Fields[i] as PrimitiveSchemaElement;
				if (primitiveSchemaElement == null || primitiveSchemaElement.RepetitionLevel > 0 || column.Nullable != primitiveSchemaElement.DefinitionLevel > 0 || column.Name != primitiveSchemaElement.Name)
				{
					conformingPageReader = null;
					return false;
				}
				if (!primitiveSchemaElement.TypeMap.IsOleDbCompatible)
				{
					conformingPageReader = null;
					return false;
				}
				Type type = primitiveSchemaElement.TypeMap.Type;
				if (type == column.DataType)
				{
					tableSchema2.AddColumn(column);
				}
				else
				{
					SchemaConformingPageReader.SchemaConformingColumn schemaConformingColumn;
					if (!SchemaConformingPageReader.TryCreateConformingColumn(column.DataType, type, out schemaConformingColumn))
					{
						conformingPageReader = null;
						return false;
					}
					dictionary.Add(i, schemaConformingColumn);
					tableSchema2.AddColumn(column.Name, type, column.Nullable);
				}
			}
			if (dictionary.Count == 0)
			{
				conformingPageReader = pageReader;
			}
			else
			{
				conformingPageReader = new SchemaConformingPageReader(pageReader, tableSchema2, dictionary);
			}
			return true;
		}

		// Token: 0x06010E47 RID: 69191 RVA: 0x003A3D1C File Offset: 0x003A1F1C
		private static bool TryCreateConformingColumn(Type sourceType, Type destinationType, out SchemaConformingPageReader.SchemaConformingColumn column)
		{
			if (destinationType == typeof(Number))
			{
				if (sourceType == typeof(double))
				{
					column = new SchemaConformingPageReader.DoubleToNumberSchemaConformingColumn();
					return true;
				}
				if (sourceType == typeof(decimal))
				{
					column = new SchemaConformingPageReader.DecimalToNumberSchemaConformingColumn();
					return true;
				}
			}
			if (destinationType == typeof(Guid) && sourceType == typeof(string))
			{
				column = new SchemaConformingPageReader.TextToGuidSchemaConformingColumn();
				return true;
			}
			column = null;
			return false;
		}

		// Token: 0x17002CBD RID: 11453
		// (get) Token: 0x06010E48 RID: 69192 RVA: 0x003A3DA2 File Offset: 0x003A1FA2
		public TableSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x17002CBE RID: 11454
		// (get) Token: 0x06010E49 RID: 69193 RVA: 0x003A3DAA File Offset: 0x003A1FAA
		public IProgress Progress
		{
			get
			{
				return this.pageReader.Progress;
			}
		}

		// Token: 0x17002CBF RID: 11455
		// (get) Token: 0x06010E4A RID: 69194 RVA: 0x003A3DB7 File Offset: 0x003A1FB7
		public int MaxPageRowCount
		{
			get
			{
				return this.pageReader.MaxPageRowCount;
			}
		}

		// Token: 0x06010E4B RID: 69195 RVA: 0x003A3DC4 File Offset: 0x003A1FC4
		public IPage CreatePage()
		{
			return new SchemaConformingPageReader.SchemaConformingPage(this.pageReader.CreatePage(), this.conformingColumns);
		}

		// Token: 0x06010E4C RID: 69196 RVA: 0x003A3DDC File Offset: 0x003A1FDC
		public void Read(IPage page)
		{
			((SchemaConformingPageReader.SchemaConformingPage)page).Read(this.pageReader);
		}

		// Token: 0x06010E4D RID: 69197 RVA: 0x000170F6 File Offset: 0x000152F6
		public IPageReader NextResult()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06010E4E RID: 69198 RVA: 0x003A3DEF File Offset: 0x003A1FEF
		public void Dispose()
		{
			this.pageReader.Dispose();
		}

		// Token: 0x040065C3 RID: 26051
		private readonly IPageReader pageReader;

		// Token: 0x040065C4 RID: 26052
		private readonly TableSchema schema;

		// Token: 0x040065C5 RID: 26053
		private readonly Dictionary<int, SchemaConformingPageReader.SchemaConformingColumn> conformingColumns;

		// Token: 0x02001F74 RID: 8052
		private class SchemaConformingPage : IPage, IDisposable
		{
			// Token: 0x06010E4F RID: 69199 RVA: 0x003A3DFC File Offset: 0x003A1FFC
			public SchemaConformingPage(IPage page, Dictionary<int, SchemaConformingPageReader.SchemaConformingColumn> conformingColumns)
			{
				this.page = page;
				this.conformingColumns = conformingColumns;
			}

			// Token: 0x17002CC0 RID: 11456
			// (get) Token: 0x06010E50 RID: 69200 RVA: 0x003A3E12 File Offset: 0x003A2012
			public int ColumnCount
			{
				get
				{
					return this.page.ColumnCount;
				}
			}

			// Token: 0x17002CC1 RID: 11457
			// (get) Token: 0x06010E51 RID: 69201 RVA: 0x003A3E1F File Offset: 0x003A201F
			public int RowCount
			{
				get
				{
					return this.page.RowCount;
				}
			}

			// Token: 0x06010E52 RID: 69202 RVA: 0x003A3E2C File Offset: 0x003A202C
			public IColumn GetColumn(int ordinal)
			{
				SchemaConformingPageReader.SchemaConformingColumn schemaConformingColumn;
				if (this.conformingColumns.TryGetValue(ordinal, out schemaConformingColumn))
				{
					schemaConformingColumn.Column = this.page.GetColumn(ordinal);
					return schemaConformingColumn;
				}
				return this.page.GetColumn(ordinal);
			}

			// Token: 0x17002CC2 RID: 11458
			// (get) Token: 0x06010E53 RID: 69203 RVA: 0x003A3E69 File Offset: 0x003A2069
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					return this.page.ExceptionRows;
				}
			}

			// Token: 0x17002CC3 RID: 11459
			// (get) Token: 0x06010E54 RID: 69204 RVA: 0x003A3E76 File Offset: 0x003A2076
			public ISerializedException PageException
			{
				get
				{
					return this.page.PageException;
				}
			}

			// Token: 0x06010E55 RID: 69205 RVA: 0x003A3E83 File Offset: 0x003A2083
			public void Dispose()
			{
				this.page.Dispose();
			}

			// Token: 0x06010E56 RID: 69206 RVA: 0x003A3E90 File Offset: 0x003A2090
			public void Read(IPageReader pageReader)
			{
				pageReader.Read(this.page);
			}

			// Token: 0x040065C6 RID: 26054
			private readonly IPage page;

			// Token: 0x040065C7 RID: 26055
			private readonly Dictionary<int, SchemaConformingPageReader.SchemaConformingColumn> conformingColumns;
		}

		// Token: 0x02001F75 RID: 8053
		private abstract class SchemaConformingColumn : IColumn
		{
			// Token: 0x17002CC4 RID: 11460
			// (get) Token: 0x06010E57 RID: 69207 RVA: 0x003A3E9E File Offset: 0x003A209E
			// (set) Token: 0x06010E58 RID: 69208 RVA: 0x003A3EA6 File Offset: 0x003A20A6
			public IColumn Column { get; set; }

			// Token: 0x06010E59 RID: 69209 RVA: 0x003A3EAF File Offset: 0x003A20AF
			public bool IsNull(int row)
			{
				return this.Column.IsNull(row);
			}

			// Token: 0x06010E5A RID: 69210
			public abstract object GetObject(int row);

			// Token: 0x06010E5B RID: 69211 RVA: 0x003A3EBD File Offset: 0x003A20BD
			public virtual bool GetBoolean(int row)
			{
				throw this.CreateCastException("Boolean");
			}

			// Token: 0x06010E5C RID: 69212 RVA: 0x003A3ECA File Offset: 0x003A20CA
			public virtual byte GetByte(int row)
			{
				throw this.CreateCastException("Byte");
			}

			// Token: 0x06010E5D RID: 69213 RVA: 0x003A3ED7 File Offset: 0x003A20D7
			public virtual short GetInt16(int row)
			{
				throw this.CreateCastException("Int16");
			}

			// Token: 0x06010E5E RID: 69214 RVA: 0x003A3EE4 File Offset: 0x003A20E4
			public virtual int GetInt32(int row)
			{
				throw this.CreateCastException("Int32");
			}

			// Token: 0x06010E5F RID: 69215 RVA: 0x003A3EF1 File Offset: 0x003A20F1
			public virtual long GetInt64(int row)
			{
				throw this.CreateCastException("Int64");
			}

			// Token: 0x06010E60 RID: 69216 RVA: 0x003A3EFE File Offset: 0x003A20FE
			public virtual float GetFloat(int row)
			{
				throw this.CreateCastException("Float");
			}

			// Token: 0x06010E61 RID: 69217 RVA: 0x003A3F0B File Offset: 0x003A210B
			public virtual Guid GetGuid(int row)
			{
				throw this.CreateCastException("Guid");
			}

			// Token: 0x06010E62 RID: 69218 RVA: 0x003A3F18 File Offset: 0x003A2118
			public virtual double GetDouble(int row)
			{
				throw this.CreateCastException("Double");
			}

			// Token: 0x06010E63 RID: 69219 RVA: 0x003A3F25 File Offset: 0x003A2125
			public virtual decimal GetDecimal(int row)
			{
				throw this.CreateCastException("Decimal");
			}

			// Token: 0x06010E64 RID: 69220 RVA: 0x003A3F32 File Offset: 0x003A2132
			public virtual DateTime GetDateTime(int row)
			{
				throw this.CreateCastException("DateTime");
			}

			// Token: 0x06010E65 RID: 69221 RVA: 0x003A3F3F File Offset: 0x003A213F
			public virtual string GetString(int row)
			{
				throw this.CreateCastException("String");
			}

			// Token: 0x06010E66 RID: 69222 RVA: 0x003A3F4C File Offset: 0x003A214C
			private Exception CreateCastException(string toType)
			{
				return new InvalidCastException(string.Format(CultureInfo.InvariantCulture, "Unable to cast {0} to type {1}.", base.GetType().Name, toType));
			}
		}

		// Token: 0x02001F76 RID: 8054
		private sealed class DoubleToNumberSchemaConformingColumn : SchemaConformingPageReader.SchemaConformingColumn, IConformingColumn<Number>, IColumn
		{
			// Token: 0x06010E68 RID: 69224 RVA: 0x003A3F6E File Offset: 0x003A216E
			public Number GetValue(int row)
			{
				return new Number(base.Column.GetDouble(row));
			}

			// Token: 0x06010E69 RID: 69225 RVA: 0x003A3F81 File Offset: 0x003A2181
			public override object GetObject(int row)
			{
				return this.GetValue(row);
			}
		}

		// Token: 0x02001F77 RID: 8055
		private sealed class DecimalToNumberSchemaConformingColumn : SchemaConformingPageReader.SchemaConformingColumn, IConformingColumn<Number>, IColumn
		{
			// Token: 0x06010E6B RID: 69227 RVA: 0x003A3F97 File Offset: 0x003A2197
			public Number GetValue(int row)
			{
				return new Number(base.Column.GetDecimal(row));
			}

			// Token: 0x06010E6C RID: 69228 RVA: 0x003A3FAA File Offset: 0x003A21AA
			public override object GetObject(int row)
			{
				return this.GetValue(row);
			}
		}

		// Token: 0x02001F78 RID: 8056
		private sealed class TextToGuidSchemaConformingColumn : SchemaConformingPageReader.SchemaConformingColumn, IConformingColumn<Guid>, IColumn
		{
			// Token: 0x06010E6E RID: 69230 RVA: 0x003A3FB8 File Offset: 0x003A21B8
			public Guid GetValue(int row)
			{
				return this.GetGuid(row);
			}

			// Token: 0x06010E6F RID: 69231 RVA: 0x003A3FC1 File Offset: 0x003A21C1
			public override object GetObject(int row)
			{
				return this.GetGuid(row);
			}

			// Token: 0x06010E70 RID: 69232 RVA: 0x003A3FCF File Offset: 0x003A21CF
			public override Guid GetGuid(int row)
			{
				return new Guid((string)base.Column.GetObject(row));
			}
		}
	}
}
