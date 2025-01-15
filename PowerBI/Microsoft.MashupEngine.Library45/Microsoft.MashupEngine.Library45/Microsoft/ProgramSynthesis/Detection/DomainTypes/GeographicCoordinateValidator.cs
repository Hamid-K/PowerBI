using System;

namespace Microsoft.ProgramSynthesis.Detection.DomainTypes
{
	// Token: 0x02000AEE RID: 2798
	public class GeographicCoordinateValidator
	{
		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004612 RID: 17938 RVA: 0x000DA8DF File Offset: 0x000D8ADF
		public static GeographicCoordinateValidator Instance { get; } = new GeographicCoordinateValidator();

		// Token: 0x06004613 RID: 17939 RVA: 0x00002130 File Offset: 0x00000330
		private GeographicCoordinateValidator()
		{
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x000DA8E8 File Offset: 0x000D8AE8
		public bool IsValid(string v)
		{
			double num;
			double num2;
			return this.TrySplit(v, out num, out num2);
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x000DA900 File Offset: 0x000D8B00
		public bool TrySplit(string v, out double latitude, out double longitude)
		{
			return v.ParseLatLong(out latitude, out longitude);
		}
	}
}
