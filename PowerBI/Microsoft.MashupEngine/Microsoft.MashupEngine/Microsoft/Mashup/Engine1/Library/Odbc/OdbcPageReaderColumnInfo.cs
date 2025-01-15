using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005A5 RID: 1445
	internal class OdbcPageReaderColumnInfo
	{
		// Token: 0x06002DB9 RID: 11705 RVA: 0x0008BA5C File Offset: 0x00089C5C
		public OdbcPageReaderColumnInfo(string name, OdbcTypeMap typeMap, int boundColumnOffset, int boundCellLength, int boundColumnLength, int boundColumnIndex)
		{
			this.Name = name;
			this.TypeMap = typeMap;
			this.loaderFactory = OdbcPageReaderColumnInfo.LoaderFactory.New(typeMap.OleDbType);
			this.BoundColumnOffset = boundColumnOffset;
			this.BoundCellLength = boundCellLength;
			this.BoundColumnLength = boundColumnLength;
			this.BoundColumnIndex = boundColumnIndex;
			if (this.IsColumnBound)
			{
				this.MaxBoundColumnFetch = this.BoundCellLength;
				if (this.TypeMap.CType == Odbc32.SQL_C.WCHAR)
				{
					this.MaxBoundColumnFetch -= 2;
				}
			}
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x0008BADE File Offset: 0x00089CDE
		public bool IsColumnBound
		{
			get
			{
				return this.BoundColumnIndex >= 0;
			}
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x0008BAEC File Offset: 0x00089CEC
		public Loader NewLoader(OdbcPageReaderColumnInfo columnInfo, Column column)
		{
			return this.loaderFactory.NewLoader(columnInfo, column);
		}

		// Token: 0x040013F4 RID: 5108
		private readonly OdbcPageReaderColumnInfo.LoaderFactory loaderFactory;

		// Token: 0x040013F5 RID: 5109
		public readonly string Name;

		// Token: 0x040013F6 RID: 5110
		public readonly OdbcTypeMap TypeMap;

		// Token: 0x040013F7 RID: 5111
		public readonly int BoundColumnOffset;

		// Token: 0x040013F8 RID: 5112
		public readonly int BoundCellLength;

		// Token: 0x040013F9 RID: 5113
		public readonly int BoundColumnLength;

		// Token: 0x040013FA RID: 5114
		public readonly int BoundColumnIndex;

		// Token: 0x040013FB RID: 5115
		public readonly int MaxBoundColumnFetch;

		// Token: 0x020005A6 RID: 1446
		private abstract class LoaderFactory
		{
			// Token: 0x06002DBC RID: 11708
			public abstract Loader NewLoader(OdbcPageReaderColumnInfo columnInfo, Column column);

			// Token: 0x06002DBD RID: 11709 RVA: 0x0008BAFB File Offset: 0x00089CFB
			public static OdbcPageReaderColumnInfo.LoaderFactory New(DBTYPE dbType)
			{
				switch (dbType)
				{
				case DBTYPE.BOOL:
					return OdbcPageReaderColumnInfo.LoaderFactory.boolLoaderFactory;
				case DBTYPE.VARIANT:
					return OdbcPageReaderColumnInfo.LoaderFactory.variantLoaderFactory;
				case DBTYPE.DECIMAL:
					return OdbcPageReaderColumnInfo.LoaderFactory.decimalLoaderFactory;
				}
				return OdbcPageReaderColumnInfo.LoaderFactory.directLoaderFactory;
			}

			// Token: 0x040013FC RID: 5116
			private static readonly OdbcPageReaderColumnInfo.LoaderFactory decimalLoaderFactory = new OdbcPageReaderColumnInfo.LoaderFactory.DecimalLoaderFactory();

			// Token: 0x040013FD RID: 5117
			private static readonly OdbcPageReaderColumnInfo.LoaderFactory boolLoaderFactory = new OdbcPageReaderColumnInfo.LoaderFactory.BoolLoaderFactory();

			// Token: 0x040013FE RID: 5118
			private static readonly OdbcPageReaderColumnInfo.LoaderFactory directLoaderFactory = new OdbcPageReaderColumnInfo.LoaderFactory.DirectLoaderFactory();

			// Token: 0x040013FF RID: 5119
			private static readonly OdbcPageReaderColumnInfo.LoaderFactory variantLoaderFactory = new OdbcPageReaderColumnInfo.LoaderFactory.VariantLoaderFactory();

			// Token: 0x020005A7 RID: 1447
			private class DirectLoaderFactory : OdbcPageReaderColumnInfo.LoaderFactory
			{
				// Token: 0x06002DC0 RID: 11712 RVA: 0x0008BB59 File Offset: 0x00089D59
				public override Loader NewLoader(OdbcPageReaderColumnInfo columnInfo, Column column)
				{
					return new OdbcPageReaderColumnInfo.LoaderFactory.DirectLoaderFactory.DirectLoader(columnInfo, column);
				}

				// Token: 0x020005A8 RID: 1448
				private sealed class DirectLoader : Loader
				{
					// Token: 0x06002DC2 RID: 11714 RVA: 0x0008BB6A File Offset: 0x00089D6A
					public DirectLoader(OdbcPageReaderColumnInfo columnInfo, Column column)
						: base(columnInfo, column)
					{
					}

					// Token: 0x06002DC3 RID: 11715 RVA: 0x0008BB74 File Offset: 0x00089D74
					public unsafe override bool TryLoad(DBTYPE dbType, byte* buffer, int length, out string error)
					{
						this.column.AddValue(dbType, (void*)buffer, length);
						error = null;
						return true;
					}
				}
			}

			// Token: 0x020005A9 RID: 1449
			private class DecimalLoaderFactory : OdbcPageReaderColumnInfo.LoaderFactory
			{
				// Token: 0x06002DC4 RID: 11716 RVA: 0x0008BB89 File Offset: 0x00089D89
				public override Loader NewLoader(OdbcPageReaderColumnInfo columnInfo, Column column)
				{
					return new OdbcPageReaderColumnInfo.LoaderFactory.DecimalLoaderFactory.DecimalLoader(columnInfo, column);
				}

				// Token: 0x020005AA RID: 1450
				private sealed class DecimalLoader : Loader
				{
					// Token: 0x06002DC6 RID: 11718 RVA: 0x0008BB6A File Offset: 0x00089D6A
					public DecimalLoader(OdbcPageReaderColumnInfo columnInfo, Column column)
						: base(columnInfo, column)
					{
					}

					// Token: 0x06002DC7 RID: 11719 RVA: 0x0008BB94 File Offset: 0x00089D94
					public unsafe override bool TryLoad(DBTYPE dbType, byte* buffer, int length, out string error)
					{
						decimal num;
						if (decimal.TryParse(new string((char*)buffer, 0, length / 2), NumberStyles.Number, CultureInfo.InvariantCulture, out num))
						{
							this.column.AddValue(dbType, (void*)(&num), 16);
							error = null;
							return true;
						}
						error = Strings.OdbcInvalidDecimalValue(this.columnInfo.Name);
						return false;
					}
				}
			}

			// Token: 0x020005AB RID: 1451
			private class BoolLoaderFactory : OdbcPageReaderColumnInfo.LoaderFactory
			{
				// Token: 0x06002DC8 RID: 11720 RVA: 0x0008BBEB File Offset: 0x00089DEB
				public override Loader NewLoader(OdbcPageReaderColumnInfo columnInfo, Column column)
				{
					return new OdbcPageReaderColumnInfo.LoaderFactory.BoolLoaderFactory.BoolLoader(columnInfo, column);
				}

				// Token: 0x020005AC RID: 1452
				private sealed class BoolLoader : Loader
				{
					// Token: 0x06002DCA RID: 11722 RVA: 0x0008BB6A File Offset: 0x00089D6A
					public BoolLoader(OdbcPageReaderColumnInfo columnInfo, Column column)
						: base(columnInfo, column)
					{
					}

					// Token: 0x06002DCB RID: 11723 RVA: 0x0008BBF4 File Offset: 0x00089DF4
					public unsafe override bool TryLoad(DBTYPE dbType, byte* buffer, int length, out string error)
					{
						ushort num;
						if (*buffer == 0)
						{
							num = 0;
						}
						else
						{
							num = ushort.MaxValue;
						}
						this.column.AddValue(dbType, (void*)(&num), 2);
						error = null;
						return true;
					}
				}
			}

			// Token: 0x020005AD RID: 1453
			private class VariantLoaderFactory : OdbcPageReaderColumnInfo.LoaderFactory
			{
				// Token: 0x06002DCC RID: 11724 RVA: 0x0008BC24 File Offset: 0x00089E24
				public override Loader NewLoader(OdbcPageReaderColumnInfo columnInfo, Column column)
				{
					return new OdbcPageReaderColumnInfo.LoaderFactory.VariantLoaderFactory.VariantLoader(columnInfo, column);
				}

				// Token: 0x020005AE RID: 1454
				private sealed class VariantLoader : Loader
				{
					// Token: 0x06002DCE RID: 11726 RVA: 0x0008BC2D File Offset: 0x00089E2D
					public VariantLoader(OdbcPageReaderColumnInfo columnInfo, Column column)
						: base(columnInfo, column)
					{
					}

					// Token: 0x06002DCF RID: 11727 RVA: 0x0008BC44 File Offset: 0x00089E44
					public unsafe override bool TryLoad(DBTYPE dbType, byte* buffer, int length, out string error)
					{
						Loader loader;
						if (!this.loaders.TryGetValue(dbType, out loader))
						{
							loader = OdbcPageReaderColumnInfo.LoaderFactory.New(dbType).NewLoader(this.columnInfo, this.column);
							this.loaders[dbType] = loader;
						}
						return loader.TryLoad(dbType, buffer, length, out error);
					}

					// Token: 0x04001400 RID: 5120
					private readonly Dictionary<DBTYPE, Loader> loaders = new Dictionary<DBTYPE, Loader>();
				}
			}
		}
	}
}
