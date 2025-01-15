using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002DC RID: 732
	public static class DataSaverUtils
	{
		// Token: 0x060010AF RID: 4271 RVA: 0x0005D0F0 File Offset: 0x0005B2F0
		public static void SaveDataView(IChannel ch, IDataSaver saver, IDataView view, IFileHandle file, bool keepHidden = false)
		{
			Contracts.CheckValue<IChannel>(ch, "ch");
			Contracts.CheckValue<IDataSaver>(ch, saver, "saver");
			Contracts.CheckValue<IDataView>(ch, view, "view");
			Contracts.CheckParam(ch, file != null && file.CanWrite, "file");
			using (Stream stream = file.CreateWriteStream())
			{
				DataSaverUtils.SaveDataView(ch, saver, view, stream, keepHidden);
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0005D168 File Offset: 0x0005B368
		public static void SaveDataView(IChannel ch, IDataSaver saver, IDataView view, Stream stream, bool keepHidden = false)
		{
			Contracts.CheckValue<IChannel>(ch, "ch");
			Contracts.CheckValue<IDataSaver>(ch, saver, "saver");
			Contracts.CheckValue<IDataView>(ch, view, "view");
			Contracts.CheckValue<Stream>(ch, stream, "stream");
			List<int> list = new List<int>();
			for (int i = 0; i < view.Schema.ColumnCount; i++)
			{
				if (keepHidden || !MetadataUtils.IsHidden(view.Schema, i))
				{
					ColumnType columnType = view.Schema.GetColumnType(i);
					if (saver.IsColumnSavable(columnType))
					{
						list.Add(i);
					}
					else
					{
						ch.Info("The column '{0}' will not be written as it has unsavable column type.", new object[] { view.Schema.GetColumnName(i) });
					}
				}
			}
			Contracts.Check(ch, list.Count > 0, "No valid columns to save");
			saver.SaveData(stream, view, list.ToArray());
		}
	}
}
