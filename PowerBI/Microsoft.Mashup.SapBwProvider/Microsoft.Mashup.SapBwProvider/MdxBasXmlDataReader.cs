using System;
using System.IO;
using System.Linq;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000022 RID: 34
	internal class MdxBasXmlDataReader : MdxDataReader
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0000786F File Offset: 0x00005A6F
		public MdxBasXmlDataReader(SapBwCommand command, MdxCommand mdxCommand, MdxColumnProvider columnProvider)
			: base(command, mdxCommand, columnProvider, 1)
		{
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000787B File Offset: 0x00005A7B
		protected virtual string GetDataBapi
		{
			get
			{
				return "RSR_MDX_BXML_GET_DATA";
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007882 File Offset: 0x00005A82
		protected virtual Stream GetStream(byte[] xmlData)
		{
			return new MemoryStream(xmlData, false);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000788C File Offset: 0x00005A8C
		protected override void EnsureInitialized()
		{
			if (this.rowEnumerator == null && this.columnProvider != null)
			{
				IRfcFunction function = this.connection.GetFunction(this.GetDataBapi, true);
				function.SetValue("DATASETID", this.mdxCommand.DataSetId);
				function.SetValue("START_ROW", base.StartRow);
				function.SetValue("END_ROW", base.EndRow);
				this.connection.InvokeFunction(function, true, this.command, true);
				byte[] array = function.GetValue("XML") as byte[];
				if (array == null || array.Length == 0)
				{
					this.rowEnumerator = Enumerable.Empty<object[]>().GetEnumerator();
					return;
				}
				this.basXmlReader = new BasXmlReader(this.columnProvider);
				this.rowEnumerator = this.basXmlReader.Read(this.GetStream(array));
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007960 File Offset: 0x00005B60
		public override void Close()
		{
			base.Close();
			if (this.basXmlReader != null)
			{
				this.basXmlReader.Dispose();
				this.basXmlReader = null;
			}
		}

		// Token: 0x040000A3 RID: 163
		private BasXmlReader basXmlReader;
	}
}
