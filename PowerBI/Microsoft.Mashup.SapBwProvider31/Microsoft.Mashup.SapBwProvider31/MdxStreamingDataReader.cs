using System;
using System.Collections.Generic;
using System.Linq;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000027 RID: 39
	internal sealed class MdxStreamingDataReader : MdxDataReader
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00008758 File Offset: 0x00006958
		public MdxStreamingDataReader(SapBwCommand command, MdxCommand mdxCommand, MdxColumnProvider columnProvider)
			: base(command, mdxCommand, columnProvider, 1)
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008764 File Offset: 0x00006964
		protected override void EnsureInitialized()
		{
			if (this.rowEnumerator == null)
			{
				IRfcFunction function = this.connection.GetFunction("BAPI_MDDATASET_GET_STREAMDATA", true);
				function.SetValue("DATASETID", this.mdxCommand.DataSetId);
				function.SetValue("NUM_ROWS", this.connection.BatchSize);
				this.connection.InvokeFunction(function, true, this.command, true);
				this.rowEnumerator = this.ParseDataStream(function.GetString("STREAM"), ((MdxStreamingColumnProvider)this.ColumnProvider).SubGroups).GetEnumerator();
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000087F7 File Offset: 0x000069F7
		private IEnumerable<object[]> ParseDataStream(string stream, IRfcTable subgroups)
		{
			if (string.IsNullOrWhiteSpace(stream))
			{
				yield break;
			}
			List<MdxColumn> columns = this.ColumnProvider.Columns.Where(delegate(MdxColumn c)
			{
				ValueProviderKind? valueProviderKind = c.ValueProviderKind;
				ValueProviderKind valueProviderKind2 = ValueProviderKind.FormatStringProvider;
				return !((valueProviderKind.GetValueOrDefault() == valueProviderKind2) & (valueProviderKind != null));
			}).ToList<MdxColumn>();
			MdxStreamScanner scanner = new MdxStreamScanner(stream);
			while (scanner.HasMore)
			{
				object[] array = this.columnProvider.StartBuildingRecord();
				int num = 0;
				using (List<MdxColumn>.Enumerator enumerator = columns.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj;
						if (enumerator.Current.TryExtractValue(scanner.ParseNext(), out obj))
						{
							array[num] = obj;
						}
						num++;
					}
				}
				this.columnProvider.FinishBuildingRecord(array);
				yield return array;
			}
			yield break;
		}
	}
}
