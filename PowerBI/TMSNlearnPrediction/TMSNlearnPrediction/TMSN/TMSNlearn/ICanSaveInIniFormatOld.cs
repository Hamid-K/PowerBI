using System;
using System.IO;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004BB RID: 1211
	public interface ICanSaveInIniFormatOld
	{
		// Token: 0x060018E9 RID: 6377
		void SaveAsIniOld(TextWriter writer, FeatureNameCollection names, ICalibrator calibrator = null);
	}
}
