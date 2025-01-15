using System;
using System.Data;
using System.Linq;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000033 RID: 51
	internal class TableDataReader : ParsingDataReader
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x0000B4ED File Offset: 0x000096ED
		public TableDataReader(SapBwCommand command)
			: base(command, 1)
		{
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000B4F7 File Offset: 0x000096F7
		protected override ColumnProvider ColumnProvider
		{
			get
			{
				if (this.columnProvider == null)
				{
					this.EnsureInitialized();
				}
				return this.columnProvider;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000B50D File Offset: 0x0000970D
		protected override int BatchSize
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B510 File Offset: 0x00009710
		public override void Close()
		{
			if (this.rowEnumerator != null)
			{
				this.rowEnumerator.Dispose();
				this.rowEnumerator = null;
			}
			this.exhausted = true;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000B534 File Offset: 0x00009734
		protected override void EnsureInitialized()
		{
			if (this.rowEnumerator == null)
			{
				TableQuery tableQuery = new TableQuery(this.connection);
				foreach (SapBwParameter sapBwParameter in ((SapBwParameterCollection)this.command.Parameters).Where((SapBwParameter p) => p.Direction != ParameterDirection.Output))
				{
					string text = sapBwParameter.ParameterName.ToUpperInvariant();
					if (!(text == "TABLE"))
					{
						if (!(text == "ROWSKIPS"))
						{
							if (!(text == "ROWCOUNT"))
							{
								if (!(text == "WHERE"))
								{
									if (!(text == "FIELDS"))
									{
										throw this.connection.Helper.NewDataSourceError(Resources.UnknownParameterInTableCall);
									}
									tableQuery.Fields = sapBwParameter.AsTrimmedString();
								}
								else
								{
									tableQuery.Where = sapBwParameter.AsTrimmedString();
								}
							}
							else
							{
								tableQuery.RowCount = new int?(sapBwParameter.AsInt());
							}
						}
						else
						{
							tableQuery.RowSkips = new int?(sapBwParameter.AsInt());
						}
					}
					else
					{
						tableQuery.Table = sapBwParameter.AsTrimmedString();
					}
				}
				IRfcTable rfcTable = tableQuery.ExecuteQuery(this.command);
				this.columnProvider = tableQuery.ColumnProvider;
				this.rowEnumerator = TableQuery.EnumerateTable(rfcTable, this.columnProvider.ColumnCount).GetEnumerator();
			}
		}

		// Token: 0x040001C8 RID: 456
		private const string Table = "TABLE";

		// Token: 0x040001C9 RID: 457
		private const string RowSkips = "ROWSKIPS";

		// Token: 0x040001CA RID: 458
		private const string RowCount = "ROWCOUNT";

		// Token: 0x040001CB RID: 459
		private const string Where = "WHERE";

		// Token: 0x040001CC RID: 460
		private const string Fields = "FIELDS";

		// Token: 0x040001CD RID: 461
		protected ColumnProvider columnProvider;
	}
}
