using System;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200067A RID: 1658
	internal sealed class OdbcTypeInfo
	{
		// Token: 0x06003416 RID: 13334 RVA: 0x000A7524 File Offset: 0x000A5724
		public OdbcTypeInfo(Odbc32.SQL_TYPE sqlType)
		{
			this.types = null;
			this.name = null;
			this.type = sqlType;
			this.searchable = Odbc32.SQL_SEARCHABLE.UNSEARCHABLE;
			this.unsigned = false;
			this.columnSize = null;
			this.numberPrecisionRadix = null;
			this.literalPrefix = null;
			this.literalSuffix = null;
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000A7580 File Offset: 0x000A5780
		public OdbcTypeInfo(OdbcTypeInfoCollection types, string name, Odbc32.SQL_TYPE type, Odbc32.SQL_SEARCHABLE searchable, bool unsigned, int? columnSize, OdbcNumberPrecisionRadix? numberPrecisionRadix, string literalPrefix, string literalSuffix)
		{
			this.types = types;
			this.name = name;
			this.type = type;
			this.searchable = searchable;
			this.unsigned = unsigned;
			this.columnSize = columnSize;
			this.numberPrecisionRadix = numberPrecisionRadix;
			this.literalPrefix = literalPrefix;
			this.literalSuffix = literalSuffix;
		}

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06003418 RID: 13336 RVA: 0x000A75D8 File Offset: 0x000A57D8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06003419 RID: 13337 RVA: 0x000A75E0 File Offset: 0x000A57E0
		public Odbc32.SQL_SEARCHABLE Searchable
		{
			get
			{
				return this.searchable;
			}
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x0600341A RID: 13338 RVA: 0x000A75E8 File Offset: 0x000A57E8
		public Odbc32.SQL_TYPE SqlType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x0600341B RID: 13339 RVA: 0x000A75F0 File Offset: 0x000A57F0
		public bool Unsigned
		{
			get
			{
				return this.unsigned;
			}
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x0600341C RID: 13340 RVA: 0x000A75F8 File Offset: 0x000A57F8
		public int? ColumnSize
		{
			get
			{
				return this.columnSize;
			}
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x0600341D RID: 13341 RVA: 0x000A7600 File Offset: 0x000A5800
		public OdbcNumberPrecisionRadix? NumberPrecisionRadix
		{
			get
			{
				return this.numberPrecisionRadix;
			}
		}

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x0600341E RID: 13342 RVA: 0x000A7608 File Offset: 0x000A5808
		public string LiteralPrefix
		{
			get
			{
				return this.literalPrefix;
			}
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x0600341F RID: 13343 RVA: 0x000A7610 File Offset: 0x000A5810
		public string LiteralSuffix
		{
			get
			{
				return this.literalSuffix;
			}
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000A7618 File Offset: 0x000A5818
		public bool TryGetImplicitConversion(OdbcTypeInfo other, out OdbcTypeInfo result)
		{
			if (this.types == null)
			{
				result = null;
				return false;
			}
			return this.types.TryGetImplicitConversion(this, other, out result);
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000A7635 File Offset: 0x000A5835
		public override bool Equals(object obj)
		{
			return this.Equals(obj as OdbcTypeInfo);
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000A7643 File Offset: 0x000A5843
		public bool Equals(OdbcTypeInfo typeInfo)
		{
			return typeInfo != null && typeInfo.name == this.name && typeInfo.type == this.type;
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000A766C File Offset: 0x000A586C
		public override int GetHashCode()
		{
			int num = this.type.GetHashCode();
			if (this.name != null)
			{
				num ^= this.name.GetHashCode();
			}
			return num;
		}

		// Token: 0x04001748 RID: 5960
		public static readonly OdbcTypeInfo Null = new OdbcTypeInfo(Odbc32.SQL_TYPE.UNKNOWN);

		// Token: 0x04001749 RID: 5961
		private readonly OdbcTypeInfoCollection types;

		// Token: 0x0400174A RID: 5962
		private readonly string name;

		// Token: 0x0400174B RID: 5963
		private readonly Odbc32.SQL_TYPE type;

		// Token: 0x0400174C RID: 5964
		private readonly Odbc32.SQL_SEARCHABLE searchable;

		// Token: 0x0400174D RID: 5965
		private readonly bool unsigned;

		// Token: 0x0400174E RID: 5966
		private readonly int? columnSize;

		// Token: 0x0400174F RID: 5967
		private readonly OdbcNumberPrecisionRadix? numberPrecisionRadix;

		// Token: 0x04001750 RID: 5968
		private readonly string literalPrefix;

		// Token: 0x04001751 RID: 5969
		private readonly string literalSuffix;
	}
}
