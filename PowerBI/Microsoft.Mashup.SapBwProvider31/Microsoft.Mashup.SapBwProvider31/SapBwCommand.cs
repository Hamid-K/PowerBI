using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000029 RID: 41
	public sealed class SapBwCommand : DbCommand
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00008DDE File Offset: 0x00006FDE
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00008DE6 File Offset: 0x00006FE6
		public override string CommandText { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00008DF0 File Offset: 0x00006FF0
		public string TraceKey
		{
			get
			{
				object obj;
				if (this.traceKey == null && this.TryGetParameterValue("TRACEKEY", out obj))
				{
					this.traceKey = obj as string;
				}
				return this.traceKey;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00008E26 File Offset: 0x00007026
		public string TraceHash
		{
			get
			{
				if (this.traceHash == null && this.TraceKey != null)
				{
					this.traceHash = Utils.GetStableHash(this.traceKey);
				}
				return this.traceHash;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00008E4F File Offset: 0x0000704F
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00008E56 File Offset: 0x00007056
		public override int CommandTimeout
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00008E5D File Offset: 0x0000705D
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00008E65 File Offset: 0x00007065
		public override CommandType CommandType
		{
			get
			{
				return (CommandType)this.commandType;
			}
			set
			{
				this.commandType = (SapBwCommandType)value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00008E6E File Offset: 0x0000706E
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00008E75 File Offset: 0x00007075
		public override bool DesignTimeVisible
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00008E7C File Offset: 0x0000707C
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00008E83 File Offset: 0x00007083
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00008E8A File Offset: 0x0000708A
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00008E92 File Offset: 0x00007092
		protected override DbConnection DbConnection
		{
			get
			{
				return this.connection;
			}
			set
			{
				this.connection = (SapBwConnection)value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00008EA0 File Offset: 0x000070A0
		public SapBwConnection SapBwConnection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00008EA8 File Offset: 0x000070A8
		public SapBwCommandType SapBwCommandType
		{
			get
			{
				return this.commandType;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00008EB0 File Offset: 0x000070B0
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				if (this.parameterCollection == null)
				{
					this.parameterCollection = new SapBwParameterCollection();
				}
				return this.parameterCollection;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00008ECB File Offset: 0x000070CB
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00008ED2 File Offset: 0x000070D2
		protected override DbTransaction DbTransaction
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00008ED9 File Offset: 0x000070D9
		private bool IsMdx
		{
			get
			{
				return this.commandType == SapBwCommandType.MdxBasXml || this.commandType == SapBwCommandType.MdxBasXmlGzip || this.commandType == SapBwCommandType.MdxDataStream || this.commandType == SapBwCommandType.MdxFlattening || this.commandType == SapBwCommandType.MdxMultidimensional;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00008F0F File Offset: 0x0000710F
		public int Step
		{
			get
			{
				return this.step;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008F17 File Offset: 0x00007117
		public void IncrementStep()
		{
			this.step++;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008F27 File Offset: 0x00007127
		public override void Cancel()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008F2E File Offset: 0x0000712E
		public override int ExecuteNonQuery()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008F35 File Offset: 0x00007135
		public override object ExecuteScalar()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008F3C File Offset: 0x0000713C
		public override void Prepare()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008F43 File Offset: 0x00007143
		protected override DbParameter CreateDbParameter()
		{
			return new SapBwParameter();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008F4C File Offset: 0x0000714C
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			this.connection.EnsureHelper(this);
			SapBwCommandType sapBwCommandType = this.commandType;
			if (sapBwCommandType == SapBwCommandType.Bapi)
			{
				return new BapiCommand(this).ExecuteReader();
			}
			if (sapBwCommandType == SapBwCommandType.Table)
			{
				return new TableDataReader(this);
			}
			if (this.IsMdx)
			{
				this.mdxCommand = new MdxCommand(this);
				this.mdxCommand.ExecuteMdx();
				MdxColumnProvider mdxColumnProvider = this.mdxCommand.GetColumnProvider();
				if (this.commandType == SapBwCommandType.MdxDataStream && mdxColumnProvider == null)
				{
					this.commandType = SapBwCommandType.MdxBasXml;
					mdxColumnProvider = this.mdxCommand.GetColumnProvider();
				}
				return mdxColumnProvider.GetReader();
			}
			throw this.connection.Helper.NewDataSourceError(Resources.InvalidCommandType);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008FFE File Offset: 0x000071FE
		public bool GetParameterValueOrDefault(string parameterName, bool defaultValue)
		{
			if (this.parameterCollection != null)
			{
				return this.parameterCollection.GetBoolValueOrDefault(parameterName, defaultValue);
			}
			return defaultValue;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00009017 File Offset: 0x00007217
		public bool TryGetParameterValue(string parameterName, out object value)
		{
			if (this.parameterCollection == null)
			{
				value = null;
				return false;
			}
			return this.parameterCollection.TryGetValue(parameterName, out value);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00009033 File Offset: 0x00007233
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.mdxCommand != null)
			{
				this.mdxCommand.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x040000DE RID: 222
		public const string HelperParameter = "HELPER";

		// Token: 0x040000DF RID: 223
		public const string TraceKeyParameter = "TRACEKEY";

		// Token: 0x040000E0 RID: 224
		public const string ColumnInfosParameter = "COLUMNINFOS";

		// Token: 0x040000E1 RID: 225
		public const string ScaleMeasuresParameter = "SCALEMEASURES";

		// Token: 0x040000E2 RID: 226
		public const string EnhancedMetadata = "ENHANCEDMETADATA";

		// Token: 0x040000E3 RID: 227
		public const string CubeNameParameter = "CUBENAME";

		// Token: 0x040000E4 RID: 228
		private int step;

		// Token: 0x040000E5 RID: 229
		private string traceKey;

		// Token: 0x040000E6 RID: 230
		private string traceHash;

		// Token: 0x040000E7 RID: 231
		private SapBwConnection connection;

		// Token: 0x040000E8 RID: 232
		private SapBwParameterCollection parameterCollection;

		// Token: 0x040000E9 RID: 233
		private SapBwCommandType commandType;

		// Token: 0x040000EA RID: 234
		private MdxCommand mdxCommand;
	}
}
