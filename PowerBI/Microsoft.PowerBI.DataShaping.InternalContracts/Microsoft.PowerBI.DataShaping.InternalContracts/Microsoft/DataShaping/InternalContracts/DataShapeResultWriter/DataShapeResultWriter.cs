using System;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000043 RID: 67
	internal sealed class DataShapeResultWriter : DsrObjectWriterBase
	{
		// Token: 0x0600016C RID: 364 RVA: 0x0000490E File Offset: 0x00002B0E
		internal CollectionWriter<DataShapeWriter> BeginDataShapes()
		{
			base.Writer.BeginProperty(base.DsrNames.DataShapes);
			base.CreateAndBeginChild<DataShapeWriter>(ref this._dataShapesWriter);
			return this._dataShapesWriter;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004939 File Offset: 0x00002B39
		internal void WriteVersion(DsrVersion version)
		{
			base.Writer.WriteProperty(base.DsrNames.Version, (int)version);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004952 File Offset: 0x00002B52
		internal void WriteMinorVersion(DsrMinorVersion minorVersion)
		{
			base.Writer.WriteProperty(base.DsrNames.MinorVersion, (int)minorVersion);
		}

		// Token: 0x040000A5 RID: 165
		private CollectionWriter<DataShapeWriter> _dataShapesWriter;
	}
}
