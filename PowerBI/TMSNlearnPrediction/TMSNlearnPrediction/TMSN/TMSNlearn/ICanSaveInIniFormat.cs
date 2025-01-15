using System;
using System.IO;
using Microsoft.MachineLearning.Data;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004BA RID: 1210
	public interface ICanSaveInIniFormat
	{
		// Token: 0x060018E8 RID: 6376
		void SaveAsIni(TextWriter writer, RoleMappedSchema schema, ICalibrator calibrator = null);
	}
}
